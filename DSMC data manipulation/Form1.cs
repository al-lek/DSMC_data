using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;


namespace DSMC_data_manipulation
{
    public partial class Form1 : Form
    {
        string fullFileName;
        public List<double> X = new List<double>(); public List<double> UX = new List<double>();
        public List<double> Y = new List<double>(); public List<double> UY = new List<double>();
        public List<double> Z = new List<double>(); public List<double> UZ = new List<double>();
        public List<double> P = new List<double>(); public List<double> T = new List<double>();
        double gridStep, maxX, maxY, maxZ;
        Int64 paX, paY, paZ;
        bool D2 = false, D3_full = false, D3_partial = false; 
        object[][] Ux_2d, Uy_2d, Uz_2d, P_2d, T_2d;
        double[][][] Ux_3d, Uy_3d, Uz_3d, P_3d, T_3d;
        Random rand = new Random();
        int large_radius = 0;
        int large_zPos = 0;

        double[,] ionCloud;
        List<double[]> splated;
        List<double> cloudStats = new List<double>();

        public event EventHandler DataAltered;

        public Form1()
        {
            InitializeComponent();

            xTrans_cmbBox.SelectedIndex = yTrans_cmbBox.SelectedIndex = zTrans_cmbBox.SelectedIndex = 0;

            crop_btn.Click += (s, e) => { crop_data(); calculate_data_stats(); calculate_pa_stats(); };

            transform_btn.Click += (s, e) => { transform_coordinates(); calculate_data_stats(); calculate_pa_stats(); };
        }

        private void init_ctrls()
        {
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;

            gridStep_nud.Maximum = 1.0M;
            gridStep_nud.Minimum = 0.01M;
            gridStep_nud.Increment = 0.01M;
            gridStep_nud.Value = 0.2M;

            X.Clear(); Y.Clear(); Z.Clear();
            UX.Clear(); UY.Clear(); UZ.Clear();
            P.Clear(); T.Clear();

            gridStep = maxX = maxY = maxZ = 0;
            paX = paY = paZ = 0;
            D2 = D3_full = D3_partial = false;

            //Ux_2d.Initialize();

            show_stats(false);
        }

        #region import DSMC data
        private void importData_btn_Click(object sender, EventArgs e)
        {
            init_ctrls();
            import_data_array();
        }

        private void import_data_array()
        {
            List<string> data = new List<string>();

            // Set the path, and create if not existing 
            string directoryPath = "C:\\Users\\Alex\\zoltan";

            OpenFileDialog loadSettings = new OpenFileDialog() { RestoreDirectory = true, InitialDirectory = directoryPath, Filter = "Data files|*.dat|All files|*.*" };

            if (loadSettings.ShowDialog() == DialogResult.OK)
            {
                // 1. save and display filename
                fullFileName = loadSettings.FileName;
                this.Invoke(new Action(() => file_lbl.Text = loadSettings.SafeFileName));

                // 2. start a background thread to load data
                Thread worker = new Thread(() => load_data_worker(data, loadSettings.FileName));
                worker.Start();
            }            
        }

        private void load_data_worker(List<string> data, string path)
        {
            // a. open file and store all data at settings List
            change_status("Busy: Parsing data file.");

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(path);

            do { data.Add(objReader.ReadLine()); if (data.Count % 1000 == 0) change_status("Busy: Parsing data file. Loaded particles: " + data.Count.ToString()); }
            while (objReader.Peek() != -1);
            objReader.Close(); objReader.Dispose();

            // determine solver
            string solver = "";
            if (path.EndsWith(".dat") || path.EndsWith(".DAT")) solver = "sparta";
            else if (path.EndsWith(".csv") || path.EndsWith(".CSV")) solver = "cfx";

            // determine if it has a header
            // 
            int data_start_line_no = 0;
            double num;
            string[] header_line; 

            for (int k = 0; k < data.Count; k++)
            {
                header_line = data[k].Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

                // get rid of empty lines
                if (header_line.Length == 0) { continue; }          

                else if (header_line[0] == "X" || header_line[0] == "x")
                {
                    // resolve collumns assignments
                }

                // find where data start
                else if (double.TryParse(header_line[0], out num)) { data_start_line_no = k; break; }
            }

            // determine if it is 2d or 3d data
            // 2d is 7 collumns, 3d is 8 collumns

            string[] dataStruct = data[data_start_line_no].Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (dataStruct.Length == 7) D2 = true;
            else if (dataStruct.Length == 8 && solver == "cfx" && Convert.ToDouble(dataStruct[2]) == 0.0) D2 = true;
            else if (dataStruct.Length == 8 && solver == "cfx" && Convert.ToDouble(dataStruct[2]) != 0.0) D3_partial = true;
            else if (dataStruct.Length == 8) D3_full = true;
            else { MessageBox.Show("Wrong file format. "); return; }

            int line_no = 0;    //used to trace line error
            show_progress(true);
            try
            {
                change_status("Busy: Resolving data.");
                double tempX, tempY, tempZ = -1;
                double xCrop = 1e-3 * Convert.ToDouble(xCropMax_txtBox.Text), yCrop = 1e-3 * Convert.ToDouble(yCropMax_txtBox.Text), zCrop = 1e-3 * Convert.ToDouble(zCropMax_txtBox.Text);

                // karfwta oi stiles
                int x_col = -1, y_col = -1, z_col = -1, ux_col = -1, uy_col = -1, uz_col = -1, p_col = -1, t_col = -1;

                if (solver == "sparta" && D2) { x_col = 0; y_col = 1; z_col = -1; ux_col = 2; uy_col = 3; uz_col = 4; p_col = 5; t_col = 6; }
                else if (solver == "sparta" && D3_full) { x_col = 0; y_col = 1; z_col = 2; ux_col = 3; uy_col = 4; uz_col = 5; p_col = 6; t_col = 7; }
                else if (solver == "cfx" && D2) { x_col = 0; y_col = 1; z_col = -1; ux_col = 5; uy_col = 6; uz_col = 7; p_col = 3; t_col = 4; }
                else if (solver == "cfx" && D3_partial) { x_col = 0; y_col = 1; z_col = 2; ux_col = 5; uy_col = 6; uz_col = 7; p_col = 3; t_col = 4; }

                for (int k = data_start_line_no; k < data.Count; k++)
                {
                    line_no = k;
                    string[] temp = data[line_no].Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    X.Add(Convert.ToDouble(temp[x_col])); Y.Add(Convert.ToDouble(temp[y_col]));
                    UX.Add(Convert.ToDouble(temp[ux_col])); UY.Add(Convert.ToDouble(temp[uy_col])); UZ.Add(Convert.ToDouble(temp[uz_col]));
                    P.Add(Convert.ToDouble(temp[p_col])); T.Add(Convert.ToDouble(temp[t_col]));
                    if ((D3_full || D3_partial)) Z.Add(Convert.ToDouble(temp[z_col]));

                    if (k % 1000 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / data.Count));                    
                }
            }
            catch { MessageBox.Show("Error in parsing data. Line: " + line_no.ToString()); reset_toolStrip(); }
            data.Clear();
            reset_toolStrip();
            this.Invoke(new Action(() => calculate_data_stats()));
            this.Invoke(new Action(() => calculate_pa_stats()));

            change_status("Idle.");
        }

        private void transform_coordinates()
        {
            change_status("Transforming...");

            // get params
            int[] transforms = new int[3] { xTrans_cmbBox.SelectedIndex, yTrans_cmbBox.SelectedIndex, zTrans_cmbBox.SelectedIndex };
            List<double>[] data = new List<double>[3] { X, Y, Z };

            for (int i = 0; i < transforms.Length; i++)
            {
                // 0. None
                if (transforms[i] == 0) continue;
                // 1. shift
                if (transforms[i] == 1)
                {
                    double min = Math.Abs(data[i].Min());

                    for (int j = 0; j < data[i].Count; j++)
                        data[i][j] = min + data[i][j];
                }
                // 2. flip
                else
                    for (int j = 0; j < data[i].Count; j++)
                        data[i][j] = -1.0 * data[i][j];
            }

            if(DataAltered != null) DataAltered.Invoke(this, EventArgs.Empty);
            change_status("Idle.");
        }

        private void crop_data()
        {
            if (X.Count == 0) return;
            change_status("Croping...");

            // get params
            double xMin = 1e-3 * dParser(xCropMin_txtBox); double xMax = 1e-3 * dParser(xCropMax_txtBox);
            double yMin = 1e-3 * dParser(yCropMin_txtBox); double yMax = 1e-3 * dParser(yCropMax_txtBox);
            double zMin = 1e-3 * dParser(zCropMin_txtBox); double zMax = 1e-3 * dParser(zCropMax_txtBox);

            // croped data holders
            List<double> Xc = new List<double>(); List<double> UXc = new List<double>();
            List<double> Yc = new List<double>(); List<double> UYc = new List<double>();
            List<double> Zc = new List<double>(); List<double> UZc = new List<double>();
            List<double> Pc = new List<double>(); List<double> Tc = new List<double>();

            if (!(D3_full || D3_partial))
            {
                for (int i = 0; i < X.Count; i++)
                {
                    if ((X[i] >= xMin && X[i] <= xMax) && ((Y[i] >= yMin && Y[i] <= yMax)))
                    {
                        Xc.Add(X[i]); Yc.Add(Y[i]);
                        UXc.Add(UX[i]); UYc.Add(UY[i]); UZc.Add(UZ[i]);
                        Pc.Add(P[i]); Tc.Add(T[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < X.Count; i++)
                {
                    if ((X[i] >= xMin && X[i] <= xMax) && (Y[i] >= yMin && Y[i] <= yMax) && (Z[i] >= zMin && Z[i] <= zMax))
                    {
                        Xc.Add(X[i]); Yc.Add(Y[i]); Zc.Add(Z[i]);
                        UXc.Add(UX[i]); UYc.Add(UY[i]); UZc.Add(UZ[i]);
                        Pc.Add(P[i]); Tc.Add(T[i]);
                    }
                }
            }

            X.Clear(); Y.Clear(); Z.Clear();
            UX.Clear(); UY.Clear(); UZ.Clear();
            P.Clear(); T.Clear();

            // deep copy
            X = Xc.ToList(); Y = Yc.ToList(); Z = Zc.ToList();
            UX = UXc.ToList(); UY = UYc.ToList(); UZ = UZc.ToList();
            P = Pc.ToList(); T = Tc.ToList();

            if (DataAltered != null) DataAltered.Invoke(this, EventArgs.Empty);
            change_status("Idle.");
        }

        private void calculate_data_stats()
        {
            show_stats(true);

            if (D2) dim_lbl.Text = "2D data";
            else if (D3_partial) dim_lbl.Text = "3D partial data";
            else if (D3_full) dim_lbl.Text = "3D full data";

            totalParticles_lbl.Text = "Total particles: " + X.Count.ToString();

            maxX = X.Max(); //used later in grid calculations
            X_lbl.Text = "X: " + Math.Round((1000.0 * X.Min()), 2).ToString() + "__" + Math.Round((1000.0 * X.Average()), 2).ToString() + "__" + Math.Round((1000.0 * X.Max()), 2).ToString() + " mm";
            UX_lbl.Text = "UX: " + Math.Round((UX.Min()), 2).ToString() + "__" + Math.Round((UX.Average()), 2).ToString() + "__" + Math.Round((UX.Max()), 2).ToString() + " m/s";

            maxY = Y.Max(); //used later in grid calculations
            Y_lbl.Text = "Y: " + Math.Round((1000.0 * Y.Min()), 2).ToString() + "__" + Math.Round((1000.0 * Y.Average()), 2).ToString() + "__" + Math.Round((1000.0 * Y.Max()), 2).ToString() + " mm";
            UY_lbl.Text = "UY: " + Math.Round((UY.Min()), 2).ToString() + "__" + Math.Round((UY.Average()), 2).ToString() + "__" + Math.Round((UY.Max()), 2).ToString() + " m/s";

            if (D3_full || D3_partial) { maxZ = Z.Max(); Z_lbl.Text = "Z: " + Math.Round((1000.0 * Z.Min()), 2).ToString() + "__" + Math.Round((1000.0 * Z.Average()), 2).ToString() + "__" + Math.Round((1000.0 * Z.Max()), 2).ToString() + " mm"; }
            else Z_lbl.Text = "Z: no data!";
            UZ_lbl.Text = "UZ: " + Math.Round((UZ.Min()), 2).ToString() + "__" + Math.Round((UZ.Average()), 2).ToString() + "__" + Math.Round((UZ.Max()), 2).ToString() + " m/s";

            P_lbl.Text = "P: " + Math.Round((P.Min()), 2).ToString() + "__" + Math.Round((P.Average()), 2).ToString() + "__" + Math.Round((P.Max()), 2).ToString() + " mbar";
            T_lbl.Text = "T: " + Math.Round((T.Min()), 2).ToString() + "__" + Math.Round((T.Average()), 2).ToString() + "__" + Math.Round((T.Max()), 2).ToString() + " K";
        }

        #endregion

        #region construct grids, assign particles, calculate stats
        
        private void calculate_pa_stats()
        {
            gridStep = Convert.ToDouble(gridStep_nud.Value);

            if (D2 || D3_partial)
            {
                symPA_lbl.Text = "Cylindrical";
                paX = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);      // +1 is for compatibility with Simion
                paY = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);
                paZ = Convert.ToInt32(Math.Ceiling(1000.0 * maxX / gridStep));

                paX_lbl.Text = "Y -> x[gu]: " + paX.ToString(); paY_lbl.Text = "Z -> y[gu]: " + paY.ToString(); paZ_lbl.Text = "X -> z[gu]: " + paZ.ToString();
            }
            else if (D3_full)
            {
                symPA_lbl.Text = "3D";
                paX = Convert.ToInt32(Math.Ceiling(1000.0 * maxZ / gridStep) + 1);
                paY = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);
                paZ = Convert.ToInt32(Math.Ceiling(1000.0 * maxX / gridStep));

                paX_lbl.Text = "Z -> x[gu]: " + paX.ToString(); paY_lbl.Text = "Z -> y[gu]: " + paY.ToString(); paZ_lbl.Text = "X -> z[gu]: " + paZ.ToString();
            }
            
            paSize_lbl.Text = "PA size (P,T): " + Math.Round(paX * paY * paZ * 8 / 1e6, 1).ToString() + " MBytes";
            epaSize_lbl.Text = "EPA size (V): " + Math.Round(paX * paY * paZ * 8 * 4 / 1e6, 1).ToString() + " MBytes";
        }

        private void gridStep_nud_ValueChanged(object sender, EventArgs e)
        {
            calculate_pa_stats();
        }

        private void generatePA_btn_Click(object sender, EventArgs e)
        {
            // start a background thread to process data
            Thread worker = new Thread(() => generate_PAs());
            worker.Start();
        }

        private void generate_PAs()
        {
            // 1. assign particles to grid
            show_progress(true);
            change_status("Busy: Maping particles to grid.");
            if (D2 || D3_partial) map_particles_to_2DgridPoins();
            if (D3_full) map_particles_to_3DgridPoins();
            show_progress(false);

            // 2. convert (rotate) 2D -> 3D
            if (D2 || D3_partial) { change_status("Busy: Converting from 2D to 3D."); convert_2D_to_3D(); }
            
            change_status("Idle.");

            if (large_radius + large_zPos != 0) Console.WriteLine("large_radius" + large_radius.ToString() + "large_zPos" + large_zPos.ToString());
        }
                
        private void map_particles_to_2DgridPoins()
        {
            large_radius = 0; // mesures particles that fall outside maxY beacuse they have a large z. This happens because data are not a cylindrical sector, but a triangle.
            large_zPos = 0; // brute drop particles? or larger z?

            // 1. Create a 2D array of lists. Each list corresponds to a grid point,
            // and will hold all particles that are in the vicinity of the gridPoint.
            // X -> Z, Y -> X,Y

            Ux_2d = new object[paX][]; Uy_2d = new object[paX][]; Uz_2d = new object[paX][]; P_2d = new object[paX][]; T_2d = new object[paX][];

            for (int i = 0; i < paX; i++)
            {
                Ux_2d[i] = new object[paZ]; Uy_2d[i] = new object[paZ]; Uz_2d[i] = new object[paZ]; P_2d[i] = new object[paZ]; T_2d[i] = new object[paZ];

                for (int j = 0; j < paZ; j++)
                { Ux_2d[i][j] = new List<double>(); Uy_2d[i][j] = new List<double>(); Uz_2d[i][j] = new List<double>(); P_2d[i][j] = new List<double>(); T_2d[i][j] = new List<double>(); }
            }

            // 2. run through each point and find the appropriate gridPoint to map it in 2D
            for (int k = 0; k < X.Count; k++)       
            {
                int xPos = 0;
                if (D2) xPos = Convert.ToInt32(Math.Floor(Y[k] / gridStep * 1000.0));
                if (D3_partial) xPos = Convert.ToInt32(Math.Floor(Math.Sqrt(Math.Pow(Y[k] / gridStep * 1000.0, 2) + Math.Pow(Z[k] / gridStep * 1000.0, 2))));
                int zPos = Convert.ToInt32(Math.Floor(X[k] / gridStep * 1000.0));

                if (xPos >= Ux_2d.Count()) { large_radius++; continue; }
                if (zPos >= Ux_2d[0].Count()) { large_zPos++; continue; }

                ((List<double>)Ux_2d[xPos][zPos]).Add(UY[k]);
                ((List<double>)Uy_2d[xPos][zPos]).Add(UZ[k]);
                ((List<double>)Uz_2d[xPos][zPos]).Add(UX[k]);
                ((List<double>)P_2d[xPos][zPos]).Add(P[k]);
                ((List<double>)T_2d[xPos][zPos]).Add(T[k]);

                if (k % 1000 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / X.Count));
            }

            // 2.1 detect if there are any empty gridPoints. This will happen in very high resolution (100μm) and will cause errors (NaN points) in the PAs
            //correct_empty_gridPoints(Ux_2d); correct_empty_gridPoints(Uy_2d); correct_empty_gridPoints(Uz_2d); correct_empty_gridPoints(P_2d); correct_empty_gridPoints(T_2d);

            // 3. get the stats for each gridPoint (from all the assigned particles), clear particles, and assign stats to each gridPoint (avg, std) 
            double[] stats = new double[2];
            for (int i = 0; i < paX; i++)
            {
                for (int j = 0; j < paZ; j++)
                {
                    stats = Stats((List<double>)Ux_2d[i][j]); ((List<double>)Ux_2d[i][j]).Clear(); ((List<double>)Ux_2d[i][j]).AddRange(stats);
                    stats = Stats((List<double>)Uy_2d[i][j]); ((List<double>)Uy_2d[i][j]).Clear(); ((List<double>)Uy_2d[i][j]).AddRange(stats);
                    stats = Stats((List<double>)Uz_2d[i][j]); ((List<double>)Uz_2d[i][j]).Clear(); ((List<double>)Uz_2d[i][j]).AddRange(stats);
                    stats = Stats((List<double>)P_2d[i][j]); ((List<double>)P_2d[i][j]).Clear(); ((List<double>)P_2d[i][j]).AddRange(stats);
                    stats = Stats((List<double>)T_2d[i][j]); ((List<double>)T_2d[i][j]).Clear(); ((List<double>)T_2d[i][j]).AddRange(stats);
                }

                this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * i) / Convert.ToInt32(paX)));
            }
        }

        private void map_particles_to_3DgridPoins()
        {
            // Create two 3D arrays for each Ux Uy Uz P T. Each cell corresponds to a grid point,
            // and the first array will hold all particles that are in the vicinity of the gridPoint.
            // The second one will hold the number of particles assigned to the cell. 
            // It is used for calculating mean value. StDev is not required.
            // X -> z, Z -> x, Y -> y

            double[][][] Ux_3d_count, Uy_3d_count, Uz_3d_count, P_3d_count, T_3d_count;     // declare inside method to help garbage collector?

            // 1. initilize 3d arrays
            Ux_3d = new double[paX][][]; Uy_3d = new double[paX][][]; Uz_3d = new double[paX][][]; P_3d = new double[paX][][]; T_3d = new double[paX][][];
            Ux_3d_count = new double[paX][][]; Uy_3d_count = new double[paX][][]; Uz_3d_count = new double[paX][][]; P_3d_count = new double[paX][][]; T_3d_count = new double[paX][][];
            for (int i = 0; i < paX; i++)
            {
                Ux_3d[i] = new double[paY][]; Uy_3d[i] = new double[paY][]; Uz_3d[i] = new double[paY][]; P_3d[i] = new double[paY][]; T_3d[i] = new double[paY][];
                Ux_3d_count[i] = new double[paY][]; Uy_3d_count[i] = new double[paY][]; Uz_3d_count[i] = new double[paY][]; P_3d_count[i] = new double[paY][]; T_3d_count[i] = new double[paY][];
                for (int y = 0; y < paY; y++)
                {
                    Ux_3d[i][y] = new double[paZ]; Uy_3d[i][y] = new double[paZ]; Uz_3d[i][y] = new double[paZ]; P_3d[i][y] = new double[paZ]; T_3d[i][y] = new double[paZ];
                    Ux_3d_count[i][y] = new double[paZ]; Uy_3d_count[i][y] = new double[paZ]; Uz_3d_count[i][y] = new double[paZ]; P_3d_count[i][y] = new double[paZ]; T_3d_count[i][y] = new double[paZ];
                }
            }

            // 2. run through each point and find the appropriate gridPoint to map it in 3D
            // xPos, yPos, zPos, paX, paY, paZ refer to PA, not to dat!!
            for (int k = 0; k < X.Count; k++)
            {
                int xPos = Convert.ToInt32(Math.Floor(Z[k] / gridStep * 1000.0));
                int yPos = Convert.ToInt32(Math.Floor(Y[k] / gridStep * 1000.0));
                int zPos = Convert.ToInt32(Math.Floor(X[k] / gridStep * 1000.0));

                Ux_3d[xPos][yPos][zPos] += UZ[k]; Ux_3d_count[xPos][yPos][zPos] += 1;
                Uy_3d[xPos][yPos][zPos] += UY[k]; Uy_3d_count[xPos][yPos][zPos] += 1;
                Uz_3d[xPos][yPos][zPos] += UX[k]; Uz_3d_count[xPos][yPos][zPos] += 1;
                P_3d[xPos][yPos][zPos] += P[k]; P_3d_count[xPos][yPos][zPos] += 1;
                T_3d[xPos][yPos][zPos] += T[k]; T_3d_count[xPos][yPos][zPos] += 1;

                //try { if (k % 1000 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / X.Count)); }
                //catch { }
            }


            // 3. calculate mean value for each gridPoint
            for (int i = 0; i < paX; i++)
            {
                for (int j = 0; j < paY; j++)
                {
                    for (int k = 0; k < paZ; k++)
                    {
                        Ux_3d[i][j][k] = safeDiv(Ux_3d[i][j][k], Ux_3d_count[i][j][k]);
                        Uy_3d[i][j][k] = safeDiv(Uy_3d[i][j][k], Uy_3d_count[i][j][k]);
                        Uz_3d[i][j][k] = safeDiv(Uz_3d[i][j][k], Uz_3d_count[i][j][k]);
                        P_3d[i][j][k] = safeDiv(P_3d[i][j][k], P_3d_count[i][j][k]);
                        T_3d[i][j][k] = safeDiv(T_3d[i][j][k], T_3d_count[i][j][k]);
                    }
                }

                //this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * i) / Convert.ToInt32(paX)));
            }
        }

        private void convert_2D_to_3D()
        {
            // Construct the five arrays (3D-xyz) by 90deg rotation of 2D
            // 1. initilize 3d arrays
            Ux_3d = new double[paX][][]; Uy_3d = new double[paX][][]; Uz_3d = new double[paX][][]; P_3d = new double[paX][][]; T_3d = new double[paX][][];
            for (int i = 0; i < paX; i++ )
            {
                Ux_3d[i] = new double[paY][]; Uy_3d[i] = new double[paY][]; Uz_3d[i] = new double[paY][]; P_3d[i] = new double[paY][]; T_3d[i] = new double[paY][];
                for (int y = 0; y < paY; y++)
                {
                    Ux_3d[i][y] = new double[paZ]; Uy_3d[i][y] = new double[paZ]; Uz_3d[i][y] = new double[paZ]; P_3d[i][y] = new double[paZ]; T_3d[i][y] = new double[paZ];
                }
            }

            // 2. Generate the 3d by 90deg rotation
            Random rnd = new Random();
            double stdDev = 1.0;
            for (int x = 0; x < paX; x++) 
            {
                for (int y = 0; y < paY; y++)
                {
                    for (int z = 0; z < paZ; z++ )
                    {
                        int r = Convert.ToInt32(Math.Floor(Math.Sqrt(x * x + y * y)));

                        if (r < paX)
                        {
                            if (y == 0) stdDev = 0;
                            double theta = Math.Atan2(y, x);

                            Ux_3d[x][y][z] = Math.Cos(theta) * ((((List<double>)Ux_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)Ux_2d[r][z])[1])) - Math.Sin(theta) * ((((List<double>)Uy_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)Uy_2d[r][z])[1]));
                            Uy_3d[x][y][z] = Math.Cos(theta) * ((((List<double>)Uy_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)Uy_2d[r][z])[1])) + Math.Sin(theta) * ((((List<double>)Ux_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)Ux_2d[r][z])[1]));
                            Uz_3d[x][y][z] = ((((List<double>)Uz_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)Uz_2d[r][z])[1]));
                            P_3d[x][y][z] = ((((List<double>)P_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)P_2d[r][z])[1]));
                            T_3d[x][y][z] = ((((List<double>)T_2d[r][z])[0]) + normalDist(0, stdDev) * (((List<double>)T_2d[r][z])[1]));
                        }
                    }
                }
            }
            // Clear memory?
            Ux_2d = new object[0][]; Uy_2d = new object[0][]; Uz_2d = new object[0][]; P_2d = new object[0][]; T_2d = new object[0][];
        }

        private void correct_empty_gridPoints(object[][] data)
        {
            // 
            int countBlancs = 0, countNaN = 0;

            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data[i].GetLength(0); j++)
                {
                    if (((List<double>)data[i][j]).Count == 0)
                    {
                        countBlancs++;
                        ((List<double>)data[i][j]).Add(5.5);
                    }

                    if (double.IsNaN(((List<double>)data[i][j])[0]))
                        countNaN++;
                }

            Console.WriteLine("total blancs detected: " + countBlancs.ToString() + " total NaNs: " + countNaN.ToString());
        }

        private double nearestNeighboors(object map2d, int x, int y)
        {
            double res = 0.0;




            return res;
        }

        #endregion

        #region export 3D arrays to EPA and PA

        private void exportPA_btn_Click(object sender, EventArgs e)
        {
            // start a background thread to save data
            Thread worker = new Thread(() => export_to_file());
            worker.Start();
        }

        private void export_to_file()
        {
            // 1. reshape 3d arrays to 1d array
            change_status("Busy: Reshaping data format.");
            double[] Ux_1d = reshape_3d_to_1d(Ux_3d);
            double[] Uy_1d = reshape_3d_to_1d(Uy_3d);
            double[] Uz_1d = reshape_3d_to_1d(Uz_3d);
            double[] P_1d = reshape_3d_to_1d(P_3d);
            double[] T_1d = reshape_3d_to_1d(T_3d);

            double[] epa = merge_velocities(Ux_1d, Uy_1d, Uz_1d);

            check1d_integrity(Ux_1d); check1d_integrity(Uy_1d); check1d_integrity(Uz_1d); check1d_integrity(P_1d); check1d_integrity(T_1d);

            // Clear memory?
            Ux_3d = new double[1][][]; Uy_3d = new double[0][][]; Uz_3d = new double[0][][]; P_3d = new double[0][][]; T_3d = new double[0][][]; 

            // 2. generate header of the pa's
            int[] header = new int[8];
            header[0] = -1;                           // always -1
            header[1] = 1;                       // planar->1, cylindrical->0
            header[2] = 0;
            header[3] = 1090021888;
            header[4] = Convert.ToInt32(paX);
            header[5] = Convert.ToInt32(paY);
            header[6] = Convert.ToInt32(paZ);
            header[7] = 1603;                         // x,y,z,Mag -> 1,2,4,8 + 1600

            // 3. write data to files
            change_status("Busy: Saving to file.");
            binaryWriter(".epa", header, epa);
            binaryWriter("_P.pa", header, P_1d);
            binaryWriter("_T.pa", header, T_1d);

            change_status("Idle.");
        }

        private void binaryWriter(string name, int[] header, double[] data)
        {
            string gridStep = "_" + (1 / gridStep_nud.Value).ToString("0.#") + "gu";
            string fileName = Path.ChangeExtension(fullFileName, null) + gridStep + name;

            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            var byteBufHeader = GetBytesI(header); bw.Write(byteBufHeader);
            var byteBufData = GetBytesD(data); bw.Write(byteBufData);

            fs.Flush(); fs.Close(); fs.Dispose();
        }

        private double[] reshape_3d_to_1d(double[][][] array_3d)
        {
            double[] array_1d = new double[paX * paY * paZ];

            int pos = 0;
            for (int z = 0; z < paZ; z++)
                for (int y = 0; y < paY; y++)
                    for (int x = 0; x < paX; x++)
                    {
                        array_1d[pos] = array_3d[x][y][z];
                        pos++;
                    }

            return array_1d;
        }

        private double[] merge_velocities(double[] Ux, double[] Uy, double[] Uz)
        {
            int points = Convert.ToInt32(paX * paY * paZ);
            double[] merged = new double[4 * points];

            for (int i = 0; i < paX * paY * paZ; i++)
            {
                merged[points + i] = Ux[i];
                merged[2 * points + i] = Uy[i];
                merged[3 * points + i] = Uz[i];
            }
            
            return merged;
        }

        #endregion

        #region ploting

        private void plotRaw_btn_Click(object sender, EventArgs e)
        {
            Charting ch = new Charting() { mainForm = this };
            ch.Initialize_plot();
        }

        #endregion

        #region helpers
        private double dParser(TextBox txtBox)
        {
            return Convert.ToDouble(txtBox.Text);
        }
        private void change_status(string statusText)
        {
            this.Invoke(new Action(() => status.Text = statusText));
        }

        private void show_progress(bool visible)
        {
            this.Invoke(new Action(() => toolStripProgressBar1.Visible = visible));
        }

        private void reset_toolStrip()
        {
            show_progress(false);
            this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = 0));
            this.Invoke(new Action(() => status.Text = "Idle."));
        }

        private void show_stats(bool visible)
        {
            try
            {
                foreach (Control ctrl in data_grpbox.Controls.OfType<Label>())
                    this.Invoke(new Action(() => ctrl.Visible = visible));
            }
            catch { }

            try
            {
                foreach (Control ctrl in GetControls(gridPA_grpBox).Where(c => c is Label || c is NumericUpDown))
                    this.Invoke(new Action(() => ctrl.Visible = visible));
            }
            catch { }
        }

        public IEnumerable<Control> GetControls(Control c)
        {
            return new[] { c }.Concat(c.Controls.OfType<Control>().SelectMany(x => GetControls(x)));
        }

        private double[] Stats(IEnumerable<double> values)
        {
            double avg = 0.0, std = 0.0;
            if (values.Count() > 1)
            {
                avg = values.Average();
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                std = Math.Sqrt((sum) / (values.Count() - 1));
            }
            else if (values.Count() == 1) avg = values.First();

            return new double[2] {avg, std};
        }

        static byte[] GetBytesD(double[] values)
        {
            var result = new byte[values.Length * sizeof(double)];
            Buffer.BlockCopy(values, 0, result, 0, result.Length);
            return result;
        }

        static byte[] GetBytesI(int[] values)
        {
            var result = new byte[values.Length * sizeof(int)];
            Buffer.BlockCopy(values, 0, result, 0, result.Length);
            return result;
        }

        private double normalDist(double mean, double stdDev)
        {
            double randNormal = 0.0;
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        private double safeDiv(double a, double b)
        {
            double res = 0.0;

            if (b != 0) res = a / b;

            return res;
        }

        private void check2d_integrity(object[][] data)
        {
            double min = 0.0, max = 0.0;
            int errorNaN = 0, errorInf = 0;

            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data[i].GetLength(0); j++)
                {
                    if (double.IsInfinity(((List<double>)data[i][j])[0])) errorInf++;
                    if (double.IsNaN(((List<double>)data[i][j])[0])) errorNaN++;
                    if (double.IsInfinity(((List<double>)data[i][j])[1])) errorInf++;
                    if (double.IsNaN(((List<double>)data[i][j])[1])) errorNaN++;
                }

            Console.WriteLine("2D check: " + "error NaN: " + errorNaN.ToString() + "error Inf: " + errorInf.ToString());
        }

        private void check3d_integrity(double[][][] array)
        {
            double min = 0.0, max = 0.0;
            int errorNaN = 0, errorInf = 0;

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array[i].GetLength(0); j++)
                    for (int k = 0; k < array[i][j].GetLength(0); k++)
                    {
                        if (array[i][j][k] > max) max = array[i][j][k];
                        if (array[i][j][k] < min) min = array[i][j][k];
                        if (double.IsNaN(array[i][j][k])) errorNaN++;
                        if (double.IsInfinity(array[i][j][k])) errorInf++;
                    }

            Console.WriteLine("3D check: " + "min: " + min.ToString() + "\t" + "max: " + max.ToString() + "error NaN: " + errorNaN.ToString() + "error Inf: " + errorInf.ToString());
        }

        private void check1d_integrity(double[] array)
        {
            double min = 0.0, max = 0.0;
            int errorNaN = 0, errorInf = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i] > max) max = array[i];
                if (array[i] < min) min = array[i];
                if (double.IsNaN(array[i])) errorNaN++;
                if (double.IsInfinity(array[i])) errorInf++;
            }
            
            Console.WriteLine("1D check: " + "min: " + min.ToString() + "\t" + "max: " + max.ToString() + "error NaN: " + errorNaN.ToString() + "error Inf: " + errorInf.ToString());
        }

        #endregion

        #region Test
        private void test()
        {
            double[] test = new double[1000];
            for (int i = 0; i < 1000; i++) test[i] = normalDist(1, 0);
            string la = "";
            foreach (double num in test) la += num.ToString() + "\r\n";
            Clipboard.SetText(la);
        }

        private void test_btn_Click(object sender, EventArgs e)
        {
            import_ionCloud();
            detect_splatIons(2.0);
            getStats_splatIons();
            process_splatIons(0.075);
            export_splatIonsCloud("reBorn.ic8", false, false);
            export_splatIonsCloud("reBorn_noRadial.ic8", true, false);

            export_splatIonsCloud("1K_reBorn.ic8", true, true);
            export_splatIonsCloud("1K_reBorn_noRadial.ic8", true, true);
        }
        #endregion

        #region import and process ic8
        private void import_ionCloud()
        {
            List<string> data = new List<string>();

            // Set the path, and create if not existing 
            string directoryPath = "C:\\Users\\Alex\\zoltan\\splat reruns";

            OpenFileDialog loadSettings = new OpenFileDialog() { RestoreDirectory = true, InitialDirectory = directoryPath, Filter = "Ion Cloud|*.ic8|All files|*.*" };

            if (loadSettings.ShowDialog() == DialogResult.OK)
            {
                change_status("Busy: Parsing data file.");

                // 1. save and display filename
                fullFileName = loadSettings.FileName;
                this.Invoke(new Action(() => file_lbl.Text = loadSettings.SafeFileName));

                // 2. start a background thread to load data
                System.IO.StreamReader objReader;
                objReader = new System.IO.StreamReader(fullFileName);

                do { data.Add(objReader.ReadLine()); if (data.Count % 10 == 0) change_status("Busy: Parsing data file. Loaded ions: " + data.Count.ToString()); }
                while (objReader.Peek() != -1);
                objReader.Close(); objReader.Dispose();

                show_progress(true);
                int line_no = 0;
                try
                {
                    change_status("Busy: Resolving data.");
                    string[] temp = data[2].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ionCloud = new double[Convert.ToInt32(temp[2]),13];

                    for (int k = 5; k < data.Count-1; k++)      // first 5 are header, last is "end"
                    {
                        line_no = k;

                        temp = data[line_no].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int l = 2; l < 15; l++)
                            ionCloud[k - 5, l-2] = Convert.ToDouble(temp[l]);

                        if (k % 10 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / data.Count));
                    }
                }
                catch { MessageBox.Show("Error in parsing data. Line: " + line_no.ToString()); reset_toolStrip(); }
                data.Clear();
                reset_toolStrip();
            }
        }

        private void detect_splatIons(double startTime)
        {
            cloudStats.Clear(); cloudStats.Add(0.0); cloudStats.Add(0.0);

            // sort by z-pos
            double[,] sortedIonCloud = ionCloud.OrderBy(x => x[3]);
            double stopTime = sortedIonCloud[ionCloud.GetLength(0) - 1, 3] - 0.005;

            splated = new List<double[]>();

            // detect splats
            for (int i = 0; i < ionCloud.GetLength(0); i++)
                if (ionCloud[i, 3] < startTime)
                    cloudStats[0]++;

                else if (ionCloud[i, 3] > startTime && ionCloud[i, 3] < stopTime)
                {
                    double[] temp = new double[13];
                    for (int j = 0; j < 13; j++)
                        temp[j] = ionCloud[i, j];

                    splated.Add(temp);
                    cloudStats[1]++;
                }

            // sort by z-pos
            splated = splated.OrderBy(arr => arr[6]).ToList();
        }            

        private void getStats_splatIons()
        {
            double tempZ = 0.0;
            try { tempZ = Math.Round(splated[0][6], 3); }
            catch { MessageBox.Show("No splat ions on object!"); Clipboard.SetText(cloudStats[0].ToString()); return; }

            List<double> tempUz = new List<double>();
            tempUz.Add(splated[0][9]);

            for (int i = 1; i < splated.Count; i++)
            {
                if (Math.Round(splated[i][6], 3) != tempZ)
                {
                    cloudStats.Add(tempZ);
                    cloudStats.AddRange(Stats(tempUz));

                    tempZ = Math.Round(splated[i][6], 3);
                    tempUz.Clear();
                    tempUz.Add(splated[i][9]);
                }
                else
                {
                    tempUz.Add(splated[i][9]);
                }
            }
            cloudStats.Add(tempZ);
            cloudStats.AddRange(Stats(tempUz));


            string stats = "";
            foreach (double value in cloudStats)
                stats += value.ToString() + "\t";

            Clipboard.SetText(stats);
        }

        private void process_splatIons(double offset)
        {
            // shift them left by an offset and null their axial
            for (int i = 0; i < splated.Count; i++)
            {
                splated[i][6] = splated[i][6] - offset;
                splated[i][9] = 0.0;
            }
        }

        private void export_splatIonsCloud(string extension, bool chopRadial, bool shiftMass)
        {
            string path = "";
            if (shiftMass)
                path = fullFileName.Remove(fullFileName.Length - 14) + extension;
            else
                path = fullFileName.Remove(fullFileName.Length - 9) + extension;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path))
            {
                file.WriteLine("Version 2012.01.01  February 2012");
                file.WriteLine("   Title  = All ions of Simulation");
                file.WriteLine("   Nions  = " + splated.Count.ToString());
                file.WriteLine("   Ntotal = " + splated.Count.ToString());
                file.WriteLine("   Ions = ");
                for (int i = 0; i < splated.Count; i++)
                {
                    file.Write("   " + (i+1).ToString() + " = ");

                    for (int j = 0; j < 13; j++)
                    {
                        if ((shiftMass || chopRadial) && (j == 0 || j == 2 || j == 7 || j == 8 || j == 10))
                        {
                            if (shiftMass && j == 0) file.Write("1000" + ", ");         //mass
                            if (shiftMass && j == 2) file.Write("389.7144445" + ", ");  //CS
                            if (shiftMass && j == 10) file.Write("255" + ", ");         //color
                            if (chopRadial && (j == 7 || j == 8)) file.Write("0.0" + ", ");     //Ux,Uy
                        }

                        else
                            file.Write(splated[i][j].ToString() + ", ");
                    }

                    file.Write("\r\n");
                }
                file.WriteLine("   end");
            }
        }

        #endregion


    }

    public static class MultiDimensionalArrayExtensions
    {
        public static T[,] OrderBy<T>(this T[,] source, Func<T[], T> keySelector)
        {
            return source.ConvertToSingleDimension().OrderBy(keySelector).ConvertToMultiDimensional();
        }
        private static IEnumerable<T[]> ConvertToSingleDimension<T>(this T[,] source)
        {
            T[] arRow;

            for (int row = 0; row < source.GetLength(0); ++row)
            {
                arRow = new T[source.GetLength(1)];

                for (int col = 0; col < source.GetLength(1); ++col)
                    arRow[col] = source[row, col];

                yield return arRow;
            }
        }
        private static T[,] ConvertToMultiDimensional<T>(this IEnumerable<T[]> source)
        {
            T[,] twoDimensional;
            T[][] arrayOfArray;
            int numberofColumns;

            arrayOfArray = source.ToArray();
            numberofColumns = (arrayOfArray.Length > 0) ? arrayOfArray[0].Length : 0;
            twoDimensional = new T[arrayOfArray.Length, numberofColumns];

            for (int row = 0; row < arrayOfArray.GetLength(0); ++row)
                for (int col = 0; col < numberofColumns; ++col)
                    twoDimensional[row, col] = arrayOfArray[row][col];

            return twoDimensional;
        }
    }

}
