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
        List<double> X = new List<double>(); List<double> UX = new List<double>();
        List<double> Y = new List<double>(); List<double> UY = new List<double>();
        List<double> Z = new List<double>(); List<double> UZ = new List<double>();
        List<double> P = new List<double>(); List<double> T = new List<double>();
        double gridStep, maxX, maxY, maxZ;
        Int64 paX, paY, paZ;
        bool D2 = false, D3 = false;
        object[][] Ux_2d, Uy_2d, Uz_2d, P_2d, T_2d;
        double[][][] Ux_3d, Uy_3d, Uz_3d, P_3d, T_3d;
        Random rand = new Random();
        string header;

        public Form1()
        {
            InitializeComponent();
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
            D2 = D3 = false;

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

            // determine if it is 2d or 3d data
            // 2d is 7 collumns, 3d is 8 collumns

            string[] dataStruct = data[0].Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (dataStruct.Length == 7) D2 = true;
            else if (dataStruct.Length == 8) D3 = true;
            else { MessageBox.Show("Wrong file format. "); return; }

            int line_no = 0;    //used to trace line error
            show_progress(true);
            try
            {
                change_status("Busy: Resolving data.");

                for (int k = 0; k < data.Count; k++)
                {
                    line_no = k;
                    string[] temp = data[line_no].Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (temp[0] == "X") { header = data[line_no]; continue; }       // skip header, if existing

                    if (D2)
                    {
                        X.Add(Convert.ToDouble(temp[0])); Y.Add(Convert.ToDouble(temp[1]));
                        UX.Add(Convert.ToDouble(temp[2])); UY.Add(Convert.ToDouble(temp[3])); UZ.Add(Convert.ToDouble(temp[4]));
                        P.Add(Convert.ToDouble(temp[5])); T.Add(Convert.ToDouble(temp[6]));
                    }
                    else
                    {
                        X.Add(Convert.ToDouble(temp[0])); Y.Add(Convert.ToDouble(temp[1])); Z.Add(Convert.ToDouble(temp[2]));
                        UX.Add(Convert.ToDouble(temp[3])); UY.Add(Convert.ToDouble(temp[4])); UZ.Add(Convert.ToDouble(temp[5]));
                        P.Add(Convert.ToDouble(temp[6])); T.Add(Convert.ToDouble(temp[7]));
                    }

                    if (k%1000 == 0)
                        this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / data.Count));
                }
            }
            catch { MessageBox.Show("Error in parsing data. Line: " + line_no.ToString()); reset_toolStrip(); }
            data.Clear();
            reset_toolStrip();
            this.Invoke(new Action(() => calculate_data_stats()));
            this.Invoke(new Action(() => calculate_pa_stats()));
        }

        private void calculate_data_stats()
        {
            show_stats(true);

            if (D2) dim_lbl.Text = "2D data";
            else dim_lbl.Text = "3D data";

            totalParticles_lbl.Text = "Total particles: " + X.Count.ToString();

            maxX = X.Max(); //used later in grid calculations
            X_lbl.Text = "X: " + Math.Round((1000.0 * X.Min()), 2).ToString() + "__" + Math.Round((1000.0 * X.Average()), 2).ToString() + "__" + Math.Round((1000.0 * X.Max()), 2).ToString() + " mm";
            UX_lbl.Text = "UX: " + Math.Round((UX.Min()), 2).ToString() + "__" + Math.Round((UX.Average()), 2).ToString() + "__" + Math.Round((UX.Max()), 2).ToString() + " m/s";

            maxY = Y.Max(); //used later in grid calculations
            Y_lbl.Text = "Y: " + Math.Round((1000.0 * Y.Min()), 2).ToString() + "__" + Math.Round((1000.0 * Y.Average()), 2).ToString() + "__" + Math.Round((1000.0 * Y.Max()), 2).ToString() + " mm";
            UY_lbl.Text = "UY: " + Math.Round((UY.Min()), 2).ToString() + "__" + Math.Round((UY.Average()), 2).ToString() + "__" + Math.Round((UY.Max()), 2).ToString() + " m/s";

            if (D3) { maxZ = Z.Max(); Z_lbl.Text = "Z: " + Math.Round((1000.0 * Z.Min()), 2).ToString() + "__" + Math.Round((1000.0 * Z.Average()), 2).ToString() + "__" + Math.Round((1000.0 * Z.Max()), 2).ToString() + " mm"; }
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

            if (D2)
            {
                symPA_lbl.Text = "Cylindrical";
                paX = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);      // +1 is for compatibility with Simion
                paY = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);
                paZ = Convert.ToInt32(Math.Ceiling(1000.0 * maxX / gridStep));

                paX_lbl.Text = "Y -> x[gu]: " + paX.ToString(); paY_lbl.Text = "Y -> y[gu]: " + paY.ToString(); paZ_lbl.Text = "X -> z[gu]: " + paZ.ToString();
            }
            else if (D3)
            {
                symPA_lbl.Text = "3D";
                paX = Convert.ToInt32(Math.Ceiling(1000.0 * maxZ / gridStep) + 1);
                paY = Convert.ToInt32(Math.Ceiling(1000.0 * maxY / gridStep) + 1);
                paZ = Convert.ToInt32(Math.Ceiling(1000.0 * maxX / gridStep));

                paX_lbl.Text = "Z -> x[gu]: " + paX.ToString(); paY_lbl.Text = "Y -> y[gu]: " + paY.ToString(); paZ_lbl.Text = "X -> z[gu]: " + paZ.ToString();
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
            if (D2) map_particles_to_2DgridPoins();
            if (D3) map_particles_to_3DgridPoins();
            show_progress(false);

            // 2. convert (rotate) 2D -> 3D
            if (D2) { change_status("Busy: Converting from 2D to 3D."); convert_2D_to_3D(); }
            
            change_status("Idle.");
        }
                
        private void map_particles_to_2DgridPoins()
        {
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
                int xPos = Convert.ToInt32(Math.Floor(Y[k] / gridStep * 1000.0));
                int zPos = Convert.ToInt32(Math.Floor(X[k] / gridStep * 1000.0));

                ((List<double>)Ux_2d[xPos][zPos]).Add(UY[k]);
                ((List<double>)Uy_2d[xPos][zPos]).Add(UZ[k]);
                ((List<double>)Uz_2d[xPos][zPos]).Add(UX[k]);
                ((List<double>)P_2d[xPos][zPos]).Add(P[k]);
                ((List<double>)T_2d[xPos][zPos]).Add(T[k]);

                if (k % 1000 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / X.Count));
            }

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

                if (k % 1000 == 0) this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * k) / X.Count));
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

                this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * i) / Convert.ToInt32(paX)));
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
            binaryWriter("P.pa", header, P_1d);
            binaryWriter("T.pa", header, T_1d);

            change_status("Idle.");
        }

        private void binaryWriter(string name, int[] header, double[] data)
        {
            string fileName = Path.ChangeExtension(fullFileName, null) + name;
            
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
            initialize_oxyPlotWindow();
        }

        private void initialize_oxyPlotWindow()
        {
            // 1. Define holder window
            Form popUp = new Form()
            {
                Text = "Raw Data Plot",
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen, 
                Size = new Size(800, 800),
                AutoSize = true
                //TopMost = true
            };

            // 2. define additional controls
            Button plot_btn = new Button() { Text = "Plot", Size = new Size(50, 23), Location = new Point(720, 740), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right)};
            plot_btn.Click += (s, e) => { plot_data(popUp.Controls.OfType<PlotView>().FirstOrDefault(), popUp.Controls.OfType<ComboBox>().FirstOrDefault().SelectedText); };      // pass plot and data to draw
            popUp.Controls.Add(plot_btn);

            string[] items = new string[] { "Ux", "Uy", "Uz", "P", "T" };
            ComboBox axis_cmbBox = new ComboBox() { Size = new Size(40, 21), Location = new Point(650, 741), DataSource = items, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = (AnchorStyles.Bottom | AnchorStyles.Right) };
            popUp.Controls.Add(axis_cmbBox);

            // 3. define oxyplot properties
            PlotView oxyPlotView = new PlotView() { Name = "", Size = new Size(780, 720), BackColor = Color.WhiteSmoke };
            SuspendLayout();

            PlotModel oxyModelPlot = new PlotModel { PlotType = PlotType.XY, IsLegendVisible = true, LegendPosition = LegendPosition.BottomLeft, LegendFontSize = 11, TitleFontSize = 11 };

            var linearAxis1 = new OxyPlot.Axes.LinearAxis() { MajorGridlineStyle = LineStyle.Solid, Title = "Y [mm]", MinorGridlineStyle = LineStyle.Solid, Maximum = Y.Max() };
            oxyModelPlot.Axes.Add(linearAxis1);

            var linearAxis2 = new OxyPlot.Axes.LinearAxis() { MajorGridlineStyle = LineStyle.Solid, Title = "X [mm]", Position = OxyPlot.Axes.AxisPosition.Bottom, Maximum = X.Max() };
            oxyModelPlot.Axes.Add(linearAxis2);

            oxyPlotView.Model = oxyModelPlot;

            popUp.Controls.Add(oxyPlotView);
            popUp.Show();
        }

        private void plot_data(PlotView plot, string values_to_plot)
        {
            plot.Model.Series.Clear();

            //ContourSeries contour = new ContourSeries();
            show_progress(true);

            for (int i = 0; i < X.Count; i++)
            {
                PointAnnotation pnt = new PointAnnotation() { X = X[i], Y = Y[i], Size = 1, Shape = MarkerType.Circle, Fill = OxyColors.Black, Stroke = OxyColors.Red};
                plot.Model.Annotations.Add(pnt);

                this.Invoke(new Action(() => toolStripProgressBar1.ProgressBar.Value = (100 * i) / X.Count));
            }

            show_progress(false);

        }

        #endregion

        #region helpers
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
            double avg = 0, std = 0;
            if (values.Count() > 0)
            {
                avg = values.Average();
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                std = Math.Sqrt((sum) / (values.Count() - 1));
            }
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
            test();
        }
        #endregion



    }
}
