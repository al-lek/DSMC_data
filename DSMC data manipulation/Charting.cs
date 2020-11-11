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
        public LightningChartUltimate LC { get; set; } = new LightningChartUltimate("Licensed User/LightningChart Ultimate SDK Full Version/LightningChartUltimate/5V2D2K3JP7Y4CL32Q68CYZ5JFS25LWSZA3W3");
        public PointLineSeries rawData2D { get; set; }

        public void Initialize_plot()
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
            };

            // 2. define additional controls
            string[] items = new string[] { "Ux", "Uy", "Uz", "P", "T" };
            ComboBox axisX_cmbBox = new ComboBox() { Size = new Size(40, 21), Location = new Point(650, 741), DataSource = items, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = (AnchorStyles.Bottom | AnchorStyles.Right), Parent = popUp };
            ComboBox axisY_cmbBox = new ComboBox() { Size = new Size(40, 21), Location = new Point(10, 20), DataSource = items, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = (AnchorStyles.Bottom | AnchorStyles.Right), Parent = popUp };

            Button plot_btn = new Button() { Text = "Plot", Size = new Size(50, 23), Location = new Point(720, 740), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right), Parent = popUp };
            plot_btn.Click += (s, e) => { plot_data(LC, popUp.Controls.OfType<ComboBox>().FirstOrDefault().SelectedText); };      // pass plot and data to draw

            // 3. define LC properties
            inititalize_plotViewXY();
            initialize_pointLineSeries();

            popUp.Show();
        }

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

        private void plot_data(LightningChartUltimate chart, string values_to_plot)
        {

        }

    }
}
