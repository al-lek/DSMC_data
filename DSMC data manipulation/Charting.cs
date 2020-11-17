using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arction.WinForms.Charting;
using Arction.WinForms.Charting.Axes;
using Arction.WinForms.Charting.SeriesXY;
using Arction.WinForms.Charting.EventMarkers;
using Arction.WinForms.Charting.Titles;
using Arction.WinForms.Charting.Views.ViewXY;
using Arction.WinForms.Charting.Series3D;
using Arction.WinForms.Charting.Views.View3D;


namespace DSMC_data_manipulation
{
    class Charting
    {
        public Form1 mainForm { get; set; }
        public LightningChartUltimate LC { get; set; } = new LightningChartUltimate("Licensed User/LightningChart Ultimate SDK Full Version/LightningChartUltimate/5V2D2K3JP7Y4CL32Q68CYZ5JFS25LWSZA3W3") { Dock = DockStyle.Fill };
        public PointLineSeries rawData2D { get; set; }
        public FreeformPointLineSeries freePLS { get; set; }
        public PointLineSeries3D pls3D { get; set; }
        public bool plot_2D { get; set; } = true;
        public bool plot_3D { get; set; } = false;


        public void Initialize_plot()
        {
            // 1. Define holder window
            Form popUp = new Form()
            {
                Text = "Raw Data Plot",
                MaximizeBox = true,
                MinimizeBox = true,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(800, 800),
                AutoSize = true
            };

            // 2. define additional controls
            string[] items = new string[] { "X", "Y", "Z", "Ux", "Uy", "Uz", "P", "T" };
            string[] items2 = new string[] { "X", "Y", "Z", "Ux", "Uy", "Uz", "P", "T" };
            ComboBox axisX_cmbBox = new ComboBox { Size = new Size(40, 21), Location = new Point(650, 741), DataSource = items, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = (AnchorStyles.Bottom | AnchorStyles.Right), Parent = popUp };
            ComboBox axisY_cmbBox = new ComboBox { Size = new Size(40, 21), Location = new Point(10, 20), DataSource = items2, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = (AnchorStyles.Top | AnchorStyles.Left), Parent = popUp };
            Button plot_btn = new Button { Text = "Plot", Size = new Size(50, 23), Location = new Point(720, 740), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right), Parent = popUp };
            RadioButton D2_rdBtn = new RadioButton { Text = "2D", Checked = true, AutoSize = true, Location = new Point(650, 50), Anchor = (AnchorStyles.Top | AnchorStyles.Right), Parent = popUp };
            RadioButton D3_rdBtn = new RadioButton { Text = "3D", Checked = false, AutoSize = true, Location = new Point(650, 80), Anchor = (AnchorStyles.Top | AnchorStyles.Right), Parent = popUp };

            axisX_cmbBox.SelectedIndexChanged += (s, e) => { plot_btn.PerformClick(); };
            axisY_cmbBox.SelectedIndexChanged += (s, e) => { plot_btn.PerformClick(); };

            D2_rdBtn.CheckedChanged += (s, e) => { plot_2D = D2_rdBtn.Checked; };

            plot_btn.Click += (s, e) => 
            { 
                if (plot_2D) plot_data_2D(axisX_cmbBox.SelectedItem.ToString(), axisY_cmbBox.SelectedItem.ToString());
                else plot_data_3D(axisX_cmbBox.SelectedItem.ToString());
            };
            mainForm.DataAltered += (s, e) => { plot_btn.PerformClick(); };

            // 3. define LC properties
            inititalize_plotViewXY();
            initialize_freePLS();
            initialize_pls3D();            

            popUp.Controls.Add(LC);
            popUp.Show();

        }

        #region plot 2D
        private void inititalize_plotViewXY()
        {
            LC.ViewXY.LegendBox.Visible = false;
            LC.Title.Visible = false;

            LC.ViewXY.XAxes[0].MajorGrid.Color = LC.ViewXY.YAxes[0].MajorGrid.Color = Color.LightGray;
            LC.ViewXY.XAxes[0].MinorGrid.Visible = LC.ViewXY.YAxes[0].MinorGrid.Visible = false;
            LC.ViewXY.XAxes[0].MajorGrid.Pattern = LC.ViewXY.YAxes[0].MajorGrid.Pattern = LinePattern.Dot;

            LC.ViewXY.XAxes[0].AutoFormatLabels = true;
            LC.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            LC.ViewXY.XAxes[0].ScaleNibs.Size = new SizeFloatXY(20, 7);
            LC.ViewXY.XAxes[0].Title.Color = Color.DarkGreen;
            LC.ViewXY.XAxes[0].Title.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            //LC.ViewXY.YAxes[0].Title.Text = "";
            LC.ViewXY.YAxes[0].Units.Visible = false;
            LC.ViewXY.YAxes[0].AutoFormatLabels = false;
            LC.ViewXY.YAxes[0].ScaleNibs.Size = new SizeFloatXY(7, 20);
            LC.ViewXY.YAxes[0].LabelsNumberFormat = string.Format("0.0E0");
            LC.ViewXY.YAxes[0].Title.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            LC.ViewXY.GraphBackground.Style = LC.Background.Style = RectFillStyle.ColorOnly;

            LC.Background.Color = Color.LightSlateGray;
            LC.Background.GradientFill = GradientFill.Solid;

            LC.ViewXY.GraphBackground.Color = Color.GhostWhite;
            LC.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
        }

        private void initialize_pointLineSeries()
        {
            rawData2D = new PointLineSeries(LC.ViewXY, LC.ViewXY.XAxes[0], LC.ViewXY.YAxes[0]) { PointsVisible = false, MouseInteraction = false };
            rawData2D.LineStyle.Color = Color.MediumBlue;
            rawData2D.LineStyle.Width = 1;
            rawData2D.LineStyle.Pattern = LinePattern.Solid;
            rawData2D.LineStyle.AntiAliasing = LineAntialias.None;

            if (LC.ViewXY.PointLineSeries.Count == 0) LC.ViewXY.PointLineSeries.Add(rawData2D);
            else { LC.ViewXY.PointLineSeries[0].Clear(); LC.ViewXY.PointLineSeries[0] = rawData2D; }
        }

        private void initialize_freePLS()
        {
            freePLS = new FreeformPointLineSeries(LC.ViewXY, LC.ViewXY.XAxes[0], LC.ViewXY.YAxes[0]);
            freePLS.Title.Visible = false;
            freePLS.LineVisible = false;
            freePLS.PointsVisible = true;
            freePLS.PointStyle.Width = freePLS.PointStyle.Height = 1;
            freePLS.PointStyle.Shape = Shape.HollowBasic;
            freePLS.PointStyle.Color1 = Color.FromArgb(200, Color.DarkOrange);
            freePLS.PointStyle.BorderWidth = 1;
            freePLS.PointStyle.GradientFill = GradientFillPoint.Solid;
            freePLS.MouseInteraction = false;
            freePLS.MouseHighlight = MouseOverHighlight.None;

            if (LC.ViewXY.FreeformPointLineSeries.Count == 0) LC.ViewXY.FreeformPointLineSeries.Add(freePLS);
            else { LC.ViewXY.FreeformPointLineSeries[0].Clear(); LC.ViewXY.FreeformPointLineSeries[0] = freePLS; }
        }

        private void plot_data_2D(string xAxis, string yAxis)
        {
            List<double> x_points, y_points;

            if (xAxis == "X") x_points = mainForm.X;
            else if (xAxis == "Y") x_points = mainForm.Y;
            else if (xAxis == "Z") x_points = mainForm.Z;
            else if (xAxis == "Ux") x_points = mainForm.UX;
            else if (xAxis == "Uy") x_points = mainForm.UY;
            else if (xAxis == "Uz") x_points = mainForm.UZ;
            else if (xAxis == "P") x_points = mainForm.P;
            else x_points = mainForm.T;

            if (yAxis == "X") y_points = mainForm.X;
            else if (yAxis == "Y") y_points = mainForm.Y;
            else if (yAxis == "Z") y_points = mainForm.Z;
            else if (yAxis == "Ux") y_points = mainForm.UX;
            else if (yAxis == "Uy") y_points = mainForm.UY;
            else if (yAxis == "Uz") y_points = mainForm.UZ;
            else if (yAxis == "P") y_points = mainForm.P;
            else y_points = mainForm.T;

            LC.BeginUpdate();

            LC.ActiveView = ActiveView.ViewXY;

            SeriesPoint[] sp = new SeriesPoint[x_points.Count];
            for (int i = 0; i < x_points.Count; i++)
                sp[i] = new SeriesPoint(x_points[i], y_points[i]);

            LC.ViewXY.FreeformPointLineSeries[0].Points = sp;

            LC.EndUpdate();
            LC.ViewXY.ZoomToFit();
        }

        #endregion

        #region 3D
        private void initialize_view3D()
        {


        }

        private void initialize_pls3D()
        {
            pls3D = new PointLineSeries3D(LC.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
            pls3D.PointStyle.Shape3D = PointShape3D.Sphere;
            pls3D.PointStyle.Size3D.SetValues(0.5f, 0.5f, 0.5f);
            pls3D.Material.DiffuseColor = Color.DarkOrange;
            pls3D.Material.SpecularColor = Color.Red;
            pls3D.Material.SpecularPower = 20;
            pls3D.LineVisible = false;
            pls3D.PointsVisible = true;

            if (LC.View3D.PointLineSeries3D.Count == 0) LC.View3D.PointLineSeries3D.Add(pls3D);
            else { LC.View3D.PointLineSeries3D[0].Clear(); LC.View3D.PointLineSeries3D[0] = pls3D; }
        }

        private void plot_data_3D(string property)
        {
            LC.BeginUpdate();

            LC.ActiveView = ActiveView.View3D;
            var pls3D = LC.View3D.PointLineSeries3D[0];

            List<double> prop_points;
            if (property == "Ux") prop_points = mainForm.UX;
            else if (property == "Uy") prop_points = mainForm.UY;
            else if (property == "Uz") prop_points = mainForm.UZ;
            else if (property == "P") prop_points = mainForm.P;
            else prop_points = mainForm.T;

            //SeriesPoint3D[] sp3D = new SeriesPoint3D[prop_points.Count];
            //for (int i = 0; i < prop_points.Count; i++)
            //    sp3D[i] = new SeriesPoint3D { X = mainForm.X[i], Y = mainForm.Y[i], Z = mainForm.Z[i] };
            //pls3D.AddPoints(sp3D, false);            
            SeriesPoint3D[] sp3D = new SeriesPoint3D[prop_points.Count / 1000];
            for (int i = 0; i < prop_points.Count / 1000; i++)
                sp3D[i] = new SeriesPoint3D { X = mainForm.X[i * 1000], Y = mainForm.Y[i * 1000], Z = mainForm.Z[i * 1000] };
            pls3D.AddPoints(sp3D, false);

            LC.View3D.XAxisPrimary3D.SetRange(mainForm.X.Min(), mainForm.X.Max());
            LC.View3D.YAxisPrimary3D.SetRange(mainForm.Y.Min(), mainForm.Y.Max());
            LC.View3D.ZAxisPrimary3D.SetRange(mainForm.Z.Min(), mainForm.Z.Max());
            LC.EndUpdate();
        }
        #endregion
    }
}
