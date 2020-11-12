namespace DSMC_data_manipulation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.importData_btn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.data_grpbox = new System.Windows.Forms.GroupBox();
            this.transform_grpBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.zTrans_cmbBox = new System.Windows.Forms.ComboBox();
            this.yTrans_cmbBox = new System.Windows.Forms.ComboBox();
            this.transform_btn = new System.Windows.Forms.Button();
            this.xTrans_cmbBox = new System.Windows.Forms.ComboBox();
            this.crop_grpBox = new System.Windows.Forms.GroupBox();
            this.xCropMin_txtBox = new System.Windows.Forms.TextBox();
            this.zCropMin_txtBox = new System.Windows.Forms.TextBox();
            this.xCropMax_txtBox = new System.Windows.Forms.TextBox();
            this.zCropMax_txtBox = new System.Windows.Forms.TextBox();
            this.crop_btn = new System.Windows.Forms.Button();
            this.yCropMax_txtBox = new System.Windows.Forms.TextBox();
            this.yCropMin_txtBox = new System.Windows.Forms.TextBox();
            this.zCrop_lbl = new System.Windows.Forms.Label();
            this.xCrop_lbl = new System.Windows.Forms.Label();
            this.yCrop_lbl = new System.Windows.Forms.Label();
            this.T_lbl = new System.Windows.Forms.Label();
            this.UZ_lbl = new System.Windows.Forms.Label();
            this.UY_lbl = new System.Windows.Forms.Label();
            this.plotRaw_btn = new System.Windows.Forms.Button();
            this.P_lbl = new System.Windows.Forms.Label();
            this.UX_lbl = new System.Windows.Forms.Label();
            this.Z_lbl = new System.Windows.Forms.Label();
            this.Y_lbl = new System.Windows.Forms.Label();
            this.X_lbl = new System.Windows.Forms.Label();
            this.totalParticles_lbl = new System.Windows.Forms.Label();
            this.file_lbl = new System.Windows.Forms.Label();
            this.dim_lbl = new System.Windows.Forms.Label();
            this.gridPA_grpBox = new System.Windows.Forms.GroupBox();
            this.exportPA_btn = new System.Windows.Forms.Button();
            this.symPA_lbl = new System.Windows.Forms.Label();
            this.generatePA_btn = new System.Windows.Forms.Button();
            this.epaSize_lbl = new System.Windows.Forms.Label();
            this.paSize_lbl = new System.Windows.Forms.Label();
            this.paZ_lbl = new System.Windows.Forms.Label();
            this.paY_lbl = new System.Windows.Forms.Label();
            this.paX_lbl = new System.Windows.Forms.Label();
            this.gridStep_nud = new System.Windows.Forms.NumericUpDown();
            this.grid_lbl = new System.Windows.Forms.Label();
            this.test_btn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.data_grpbox.SuspendLayout();
            this.transform_grpBox.SuspendLayout();
            this.crop_grpBox.SuspendLayout();
            this.gridPA_grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStep_nud)).BeginInit();
            this.SuspendLayout();
            // 
            // importData_btn
            // 
            this.importData_btn.Location = new System.Drawing.Point(39, 33);
            this.importData_btn.Margin = new System.Windows.Forms.Padding(4);
            this.importData_btn.Name = "importData_btn";
            this.importData_btn.Size = new System.Drawing.Size(155, 28);
            this.importData_btn.TabIndex = 0;
            this.importData_btn.Text = "Import DSMC Data";
            this.importData_btn.UseVisualStyleBackColor = true;
            this.importData_btn.Click += new System.EventHandler(this.importData_btn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(878, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 20);
            this.status.Text = "Idle.";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(133, 18);
            this.toolStripProgressBar1.Visible = false;
            // 
            // data_grpbox
            // 
            this.data_grpbox.Controls.Add(this.transform_grpBox);
            this.data_grpbox.Controls.Add(this.crop_grpBox);
            this.data_grpbox.Controls.Add(this.T_lbl);
            this.data_grpbox.Controls.Add(this.UZ_lbl);
            this.data_grpbox.Controls.Add(this.UY_lbl);
            this.data_grpbox.Controls.Add(this.plotRaw_btn);
            this.data_grpbox.Controls.Add(this.P_lbl);
            this.data_grpbox.Controls.Add(this.UX_lbl);
            this.data_grpbox.Controls.Add(this.importData_btn);
            this.data_grpbox.Controls.Add(this.Z_lbl);
            this.data_grpbox.Controls.Add(this.Y_lbl);
            this.data_grpbox.Controls.Add(this.X_lbl);
            this.data_grpbox.Controls.Add(this.totalParticles_lbl);
            this.data_grpbox.Controls.Add(this.file_lbl);
            this.data_grpbox.Controls.Add(this.dim_lbl);
            this.data_grpbox.Location = new System.Drawing.Point(13, 15);
            this.data_grpbox.Margin = new System.Windows.Forms.Padding(4);
            this.data_grpbox.Name = "data_grpbox";
            this.data_grpbox.Padding = new System.Windows.Forms.Padding(4);
            this.data_grpbox.Size = new System.Drawing.Size(450, 511);
            this.data_grpbox.TabIndex = 2;
            this.data_grpbox.TabStop = false;
            this.data_grpbox.Text = "Data properties";
            // 
            // transform_grpBox
            // 
            this.transform_grpBox.Controls.Add(this.label3);
            this.transform_grpBox.Controls.Add(this.label2);
            this.transform_grpBox.Controls.Add(this.label1);
            this.transform_grpBox.Controls.Add(this.zTrans_cmbBox);
            this.transform_grpBox.Controls.Add(this.yTrans_cmbBox);
            this.transform_grpBox.Controls.Add(this.transform_btn);
            this.transform_grpBox.Controls.Add(this.xTrans_cmbBox);
            this.transform_grpBox.Location = new System.Drawing.Point(7, 313);
            this.transform_grpBox.Name = "transform_grpBox";
            this.transform_grpBox.Size = new System.Drawing.Size(187, 178);
            this.transform_grpBox.TabIndex = 6;
            this.transform_grpBox.TabStop = false;
            this.transform_grpBox.Text = "Transform coords";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "X ";
            // 
            // zTrans_cmbBox
            // 
            this.zTrans_cmbBox.FormattingEnabled = true;
            this.zTrans_cmbBox.Items.AddRange(new object[] {
            "None",
            "Shift",
            "Flip"});
            this.zTrans_cmbBox.Location = new System.Drawing.Point(59, 92);
            this.zTrans_cmbBox.Name = "zTrans_cmbBox";
            this.zTrans_cmbBox.Size = new System.Drawing.Size(88, 24);
            this.zTrans_cmbBox.TabIndex = 5;
            // 
            // yTrans_cmbBox
            // 
            this.yTrans_cmbBox.FormattingEnabled = true;
            this.yTrans_cmbBox.Items.AddRange(new object[] {
            "None",
            "Shift",
            "Flip"});
            this.yTrans_cmbBox.Location = new System.Drawing.Point(59, 62);
            this.yTrans_cmbBox.Name = "yTrans_cmbBox";
            this.yTrans_cmbBox.Size = new System.Drawing.Size(88, 24);
            this.yTrans_cmbBox.TabIndex = 5;
            // 
            // transform_btn
            // 
            this.transform_btn.Location = new System.Drawing.Point(59, 128);
            this.transform_btn.Margin = new System.Windows.Forms.Padding(4);
            this.transform_btn.Name = "transform_btn";
            this.transform_btn.Size = new System.Drawing.Size(88, 28);
            this.transform_btn.TabIndex = 4;
            this.transform_btn.Text = "Transform";
            this.transform_btn.UseVisualStyleBackColor = true;
            // 
            // xTrans_cmbBox
            // 
            this.xTrans_cmbBox.FormattingEnabled = true;
            this.xTrans_cmbBox.Items.AddRange(new object[] {
            "None",
            "Shift",
            "Flip"});
            this.xTrans_cmbBox.Location = new System.Drawing.Point(59, 32);
            this.xTrans_cmbBox.Name = "xTrans_cmbBox";
            this.xTrans_cmbBox.Size = new System.Drawing.Size(88, 24);
            this.xTrans_cmbBox.TabIndex = 5;
            // 
            // crop_grpBox
            // 
            this.crop_grpBox.Controls.Add(this.xCropMin_txtBox);
            this.crop_grpBox.Controls.Add(this.zCropMin_txtBox);
            this.crop_grpBox.Controls.Add(this.xCropMax_txtBox);
            this.crop_grpBox.Controls.Add(this.zCropMax_txtBox);
            this.crop_grpBox.Controls.Add(this.crop_btn);
            this.crop_grpBox.Controls.Add(this.yCropMax_txtBox);
            this.crop_grpBox.Controls.Add(this.yCropMin_txtBox);
            this.crop_grpBox.Controls.Add(this.zCrop_lbl);
            this.crop_grpBox.Controls.Add(this.xCrop_lbl);
            this.crop_grpBox.Controls.Add(this.yCrop_lbl);
            this.crop_grpBox.Location = new System.Drawing.Point(201, 313);
            this.crop_grpBox.Margin = new System.Windows.Forms.Padding(4);
            this.crop_grpBox.Name = "crop_grpBox";
            this.crop_grpBox.Padding = new System.Windows.Forms.Padding(4);
            this.crop_grpBox.Size = new System.Drawing.Size(241, 178);
            this.crop_grpBox.TabIndex = 5;
            this.crop_grpBox.TabStop = false;
            this.crop_grpBox.Text = "Crop control";
            // 
            // xCropMin_txtBox
            // 
            this.xCropMin_txtBox.Location = new System.Drawing.Point(136, 32);
            this.xCropMin_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.xCropMin_txtBox.Name = "xCropMin_txtBox";
            this.xCropMin_txtBox.Size = new System.Drawing.Size(44, 22);
            this.xCropMin_txtBox.TabIndex = 5;
            this.xCropMin_txtBox.Text = "0";
            this.xCropMin_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zCropMin_txtBox
            // 
            this.zCropMin_txtBox.Location = new System.Drawing.Point(136, 96);
            this.zCropMin_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.zCropMin_txtBox.Name = "zCropMin_txtBox";
            this.zCropMin_txtBox.Size = new System.Drawing.Size(44, 22);
            this.zCropMin_txtBox.TabIndex = 5;
            this.zCropMin_txtBox.Text = "0";
            this.zCropMin_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // xCropMax_txtBox
            // 
            this.xCropMax_txtBox.Location = new System.Drawing.Point(188, 32);
            this.xCropMax_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.xCropMax_txtBox.Name = "xCropMax_txtBox";
            this.xCropMax_txtBox.Size = new System.Drawing.Size(44, 22);
            this.xCropMax_txtBox.TabIndex = 5;
            this.xCropMax_txtBox.Text = "10";
            this.xCropMax_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zCropMax_txtBox
            // 
            this.zCropMax_txtBox.Location = new System.Drawing.Point(188, 96);
            this.zCropMax_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.zCropMax_txtBox.Name = "zCropMax_txtBox";
            this.zCropMax_txtBox.Size = new System.Drawing.Size(44, 22);
            this.zCropMax_txtBox.TabIndex = 5;
            this.zCropMax_txtBox.Text = "15";
            this.zCropMax_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // crop_btn
            // 
            this.crop_btn.Location = new System.Drawing.Point(136, 128);
            this.crop_btn.Margin = new System.Windows.Forms.Padding(4);
            this.crop_btn.Name = "crop_btn";
            this.crop_btn.Size = new System.Drawing.Size(88, 28);
            this.crop_btn.TabIndex = 4;
            this.crop_btn.Text = "Crop";
            this.crop_btn.UseVisualStyleBackColor = true;
            // 
            // yCropMax_txtBox
            // 
            this.yCropMax_txtBox.Location = new System.Drawing.Point(188, 64);
            this.yCropMax_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.yCropMax_txtBox.Name = "yCropMax_txtBox";
            this.yCropMax_txtBox.Size = new System.Drawing.Size(44, 22);
            this.yCropMax_txtBox.TabIndex = 5;
            this.yCropMax_txtBox.Text = "6";
            this.yCropMax_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yCropMin_txtBox
            // 
            this.yCropMin_txtBox.Location = new System.Drawing.Point(136, 64);
            this.yCropMin_txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.yCropMin_txtBox.Name = "yCropMin_txtBox";
            this.yCropMin_txtBox.Size = new System.Drawing.Size(44, 22);
            this.yCropMin_txtBox.TabIndex = 5;
            this.yCropMin_txtBox.Text = "0";
            this.yCropMin_txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zCrop_lbl
            // 
            this.zCrop_lbl.AutoSize = true;
            this.zCrop_lbl.Location = new System.Drawing.Point(14, 98);
            this.zCrop_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zCrop_lbl.Name = "zCrop_lbl";
            this.zCrop_lbl.Size = new System.Drawing.Size(116, 17);
            this.zCrop_lbl.TabIndex = 5;
            this.zCrop_lbl.Text = "zMin - zMax [mm]";
            // 
            // xCrop_lbl
            // 
            this.xCrop_lbl.AutoSize = true;
            this.xCrop_lbl.Location = new System.Drawing.Point(14, 34);
            this.xCrop_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xCrop_lbl.Name = "xCrop_lbl";
            this.xCrop_lbl.Size = new System.Drawing.Size(114, 17);
            this.xCrop_lbl.TabIndex = 5;
            this.xCrop_lbl.Text = "xMin - xMax [mm]";
            // 
            // yCrop_lbl
            // 
            this.yCrop_lbl.AutoSize = true;
            this.yCrop_lbl.Location = new System.Drawing.Point(14, 66);
            this.yCrop_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yCrop_lbl.Name = "yCrop_lbl";
            this.yCrop_lbl.Size = new System.Drawing.Size(116, 17);
            this.yCrop_lbl.TabIndex = 5;
            this.yCrop_lbl.Text = "yMin - yMax [mm]";
            // 
            // T_lbl
            // 
            this.T_lbl.AutoSize = true;
            this.T_lbl.Location = new System.Drawing.Point(208, 228);
            this.T_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.T_lbl.Name = "T_lbl";
            this.T_lbl.Size = new System.Drawing.Size(46, 17);
            this.T_lbl.TabIndex = 1;
            this.T_lbl.Text = "label2";
            this.T_lbl.Visible = false;
            // 
            // UZ_lbl
            // 
            this.UZ_lbl.AutoSize = true;
            this.UZ_lbl.Location = new System.Drawing.Point(208, 199);
            this.UZ_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UZ_lbl.Name = "UZ_lbl";
            this.UZ_lbl.Size = new System.Drawing.Size(46, 17);
            this.UZ_lbl.TabIndex = 1;
            this.UZ_lbl.Text = "label2";
            this.UZ_lbl.Visible = false;
            // 
            // UY_lbl
            // 
            this.UY_lbl.AutoSize = true;
            this.UY_lbl.Location = new System.Drawing.Point(208, 171);
            this.UY_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UY_lbl.Name = "UY_lbl";
            this.UY_lbl.Size = new System.Drawing.Size(46, 17);
            this.UY_lbl.TabIndex = 1;
            this.UY_lbl.Text = "label2";
            this.UY_lbl.Visible = false;
            // 
            // plotRaw_btn
            // 
            this.plotRaw_btn.Location = new System.Drawing.Point(255, 33);
            this.plotRaw_btn.Margin = new System.Windows.Forms.Padding(4);
            this.plotRaw_btn.Name = "plotRaw_btn";
            this.plotRaw_btn.Size = new System.Drawing.Size(100, 28);
            this.plotRaw_btn.TabIndex = 4;
            this.plotRaw_btn.Text = "Plot RAW";
            this.plotRaw_btn.UseVisualStyleBackColor = true;
            this.plotRaw_btn.Click += new System.EventHandler(this.plotRaw_btn_Click);
            // 
            // P_lbl
            // 
            this.P_lbl.AutoSize = true;
            this.P_lbl.Location = new System.Drawing.Point(23, 228);
            this.P_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.P_lbl.Name = "P_lbl";
            this.P_lbl.Size = new System.Drawing.Size(46, 17);
            this.P_lbl.TabIndex = 1;
            this.P_lbl.Text = "label2";
            this.P_lbl.Visible = false;
            // 
            // UX_lbl
            // 
            this.UX_lbl.AutoSize = true;
            this.UX_lbl.Location = new System.Drawing.Point(208, 143);
            this.UX_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UX_lbl.Name = "UX_lbl";
            this.UX_lbl.Size = new System.Drawing.Size(46, 17);
            this.UX_lbl.TabIndex = 1;
            this.UX_lbl.Text = "label2";
            this.UX_lbl.Visible = false;
            // 
            // Z_lbl
            // 
            this.Z_lbl.AutoSize = true;
            this.Z_lbl.Location = new System.Drawing.Point(23, 199);
            this.Z_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Z_lbl.Name = "Z_lbl";
            this.Z_lbl.Size = new System.Drawing.Size(46, 17);
            this.Z_lbl.TabIndex = 1;
            this.Z_lbl.Text = "label2";
            this.Z_lbl.Visible = false;
            // 
            // Y_lbl
            // 
            this.Y_lbl.AutoSize = true;
            this.Y_lbl.Location = new System.Drawing.Point(23, 171);
            this.Y_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Y_lbl.Name = "Y_lbl";
            this.Y_lbl.Size = new System.Drawing.Size(46, 17);
            this.Y_lbl.TabIndex = 1;
            this.Y_lbl.Text = "label2";
            this.Y_lbl.Visible = false;
            // 
            // X_lbl
            // 
            this.X_lbl.AutoSize = true;
            this.X_lbl.Location = new System.Drawing.Point(23, 143);
            this.X_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.X_lbl.Name = "X_lbl";
            this.X_lbl.Size = new System.Drawing.Size(46, 17);
            this.X_lbl.TabIndex = 1;
            this.X_lbl.Text = "label2";
            this.X_lbl.Visible = false;
            // 
            // totalParticles_lbl
            // 
            this.totalParticles_lbl.AutoSize = true;
            this.totalParticles_lbl.Location = new System.Drawing.Point(23, 111);
            this.totalParticles_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalParticles_lbl.Name = "totalParticles_lbl";
            this.totalParticles_lbl.Size = new System.Drawing.Size(46, 17);
            this.totalParticles_lbl.TabIndex = 1;
            this.totalParticles_lbl.Text = "label2";
            this.totalParticles_lbl.Visible = false;
            // 
            // file_lbl
            // 
            this.file_lbl.AutoSize = true;
            this.file_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_lbl.Location = new System.Drawing.Point(23, 78);
            this.file_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.file_lbl.Name = "file_lbl";
            this.file_lbl.Size = new System.Drawing.Size(52, 17);
            this.file_lbl.TabIndex = 0;
            this.file_lbl.Text = "label1";
            this.file_lbl.Visible = false;
            // 
            // dim_lbl
            // 
            this.dim_lbl.AutoSize = true;
            this.dim_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dim_lbl.Location = new System.Drawing.Point(269, 78);
            this.dim_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dim_lbl.Name = "dim_lbl";
            this.dim_lbl.Size = new System.Drawing.Size(52, 17);
            this.dim_lbl.TabIndex = 0;
            this.dim_lbl.Text = "label1";
            this.dim_lbl.Visible = false;
            // 
            // gridPA_grpBox
            // 
            this.gridPA_grpBox.Controls.Add(this.exportPA_btn);
            this.gridPA_grpBox.Controls.Add(this.symPA_lbl);
            this.gridPA_grpBox.Controls.Add(this.generatePA_btn);
            this.gridPA_grpBox.Controls.Add(this.epaSize_lbl);
            this.gridPA_grpBox.Controls.Add(this.paSize_lbl);
            this.gridPA_grpBox.Controls.Add(this.paZ_lbl);
            this.gridPA_grpBox.Controls.Add(this.paY_lbl);
            this.gridPA_grpBox.Controls.Add(this.paX_lbl);
            this.gridPA_grpBox.Controls.Add(this.gridStep_nud);
            this.gridPA_grpBox.Controls.Add(this.grid_lbl);
            this.gridPA_grpBox.Location = new System.Drawing.Point(471, 15);
            this.gridPA_grpBox.Margin = new System.Windows.Forms.Padding(4);
            this.gridPA_grpBox.Name = "gridPA_grpBox";
            this.gridPA_grpBox.Padding = new System.Windows.Forms.Padding(4);
            this.gridPA_grpBox.Size = new System.Drawing.Size(392, 346);
            this.gridPA_grpBox.TabIndex = 3;
            this.gridPA_grpBox.TabStop = false;
            this.gridPA_grpBox.Text = "PA - Grid properties";
            // 
            // exportPA_btn
            // 
            this.exportPA_btn.Location = new System.Drawing.Point(193, 23);
            this.exportPA_btn.Margin = new System.Windows.Forms.Padding(4);
            this.exportPA_btn.Name = "exportPA_btn";
            this.exportPA_btn.Size = new System.Drawing.Size(100, 28);
            this.exportPA_btn.TabIndex = 5;
            this.exportPA_btn.Text = "Save PAs";
            this.exportPA_btn.UseVisualStyleBackColor = true;
            this.exportPA_btn.Click += new System.EventHandler(this.exportPA_btn_Click);
            // 
            // symPA_lbl
            // 
            this.symPA_lbl.AutoSize = true;
            this.symPA_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symPA_lbl.Location = new System.Drawing.Point(212, 78);
            this.symPA_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.symPA_lbl.Name = "symPA_lbl";
            this.symPA_lbl.Size = new System.Drawing.Size(52, 17);
            this.symPA_lbl.TabIndex = 2;
            this.symPA_lbl.Text = "label1";
            this.symPA_lbl.Visible = false;
            // 
            // generatePA_btn
            // 
            this.generatePA_btn.Location = new System.Drawing.Point(17, 23);
            this.generatePA_btn.Margin = new System.Windows.Forms.Padding(4);
            this.generatePA_btn.Name = "generatePA_btn";
            this.generatePA_btn.Size = new System.Drawing.Size(160, 28);
            this.generatePA_btn.TabIndex = 4;
            this.generatePA_btn.Text = "Generate PA\'s";
            this.generatePA_btn.UseVisualStyleBackColor = true;
            this.generatePA_btn.Click += new System.EventHandler(this.generatePA_btn_Click);
            // 
            // epaSize_lbl
            // 
            this.epaSize_lbl.AutoSize = true;
            this.epaSize_lbl.Location = new System.Drawing.Point(159, 171);
            this.epaSize_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.epaSize_lbl.Name = "epaSize_lbl";
            this.epaSize_lbl.Size = new System.Drawing.Size(46, 17);
            this.epaSize_lbl.TabIndex = 2;
            this.epaSize_lbl.Text = "label1";
            this.epaSize_lbl.Visible = false;
            // 
            // paSize_lbl
            // 
            this.paSize_lbl.AutoSize = true;
            this.paSize_lbl.Location = new System.Drawing.Point(159, 143);
            this.paSize_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paSize_lbl.Name = "paSize_lbl";
            this.paSize_lbl.Size = new System.Drawing.Size(46, 17);
            this.paSize_lbl.TabIndex = 2;
            this.paSize_lbl.Text = "label1";
            this.paSize_lbl.Visible = false;
            // 
            // paZ_lbl
            // 
            this.paZ_lbl.AutoSize = true;
            this.paZ_lbl.Location = new System.Drawing.Point(20, 199);
            this.paZ_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paZ_lbl.Name = "paZ_lbl";
            this.paZ_lbl.Size = new System.Drawing.Size(46, 17);
            this.paZ_lbl.TabIndex = 2;
            this.paZ_lbl.Text = "label1";
            this.paZ_lbl.Visible = false;
            // 
            // paY_lbl
            // 
            this.paY_lbl.AutoSize = true;
            this.paY_lbl.Location = new System.Drawing.Point(20, 171);
            this.paY_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paY_lbl.Name = "paY_lbl";
            this.paY_lbl.Size = new System.Drawing.Size(46, 17);
            this.paY_lbl.TabIndex = 2;
            this.paY_lbl.Text = "label1";
            this.paY_lbl.Visible = false;
            // 
            // paX_lbl
            // 
            this.paX_lbl.AutoSize = true;
            this.paX_lbl.Location = new System.Drawing.Point(20, 143);
            this.paX_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paX_lbl.Name = "paX_lbl";
            this.paX_lbl.Size = new System.Drawing.Size(46, 17);
            this.paX_lbl.TabIndex = 2;
            this.paX_lbl.Text = "label1";
            this.paX_lbl.Visible = false;
            // 
            // gridStep_nud
            // 
            this.gridStep_nud.DecimalPlaces = 2;
            this.gridStep_nud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gridStep_nud.Location = new System.Drawing.Point(117, 75);
            this.gridStep_nud.Margin = new System.Windows.Forms.Padding(4);
            this.gridStep_nud.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gridStep_nud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gridStep_nud.Name = "gridStep_nud";
            this.gridStep_nud.Size = new System.Drawing.Size(67, 22);
            this.gridStep_nud.TabIndex = 1;
            this.gridStep_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridStep_nud.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.gridStep_nud.Visible = false;
            this.gridStep_nud.ValueChanged += new System.EventHandler(this.gridStep_nud_ValueChanged);
            // 
            // grid_lbl
            // 
            this.grid_lbl.AutoSize = true;
            this.grid_lbl.Location = new System.Drawing.Point(20, 78);
            this.grid_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.grid_lbl.Name = "grid_lbl";
            this.grid_lbl.Size = new System.Drawing.Size(93, 17);
            this.grid_lbl.TabIndex = 0;
            this.grid_lbl.Text = "grid step[mm]";
            this.grid_lbl.Visible = false;
            // 
            // test_btn
            // 
            this.test_btn.Location = new System.Drawing.Point(643, 395);
            this.test_btn.Margin = new System.Windows.Forms.Padding(4);
            this.test_btn.Name = "test_btn";
            this.test_btn.Size = new System.Drawing.Size(43, 28);
            this.test_btn.TabIndex = 4;
            this.test_btn.Text = "test";
            this.test_btn.UseVisualStyleBackColor = true;
            this.test_btn.Click += new System.EventHandler(this.test_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 574);
            this.Controls.Add(this.test_btn);
            this.Controls.Add(this.gridPA_grpBox);
            this.Controls.Add(this.data_grpbox);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "b";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.data_grpbox.ResumeLayout(false);
            this.data_grpbox.PerformLayout();
            this.transform_grpBox.ResumeLayout(false);
            this.transform_grpBox.PerformLayout();
            this.crop_grpBox.ResumeLayout(false);
            this.crop_grpBox.PerformLayout();
            this.gridPA_grpBox.ResumeLayout(false);
            this.gridPA_grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStep_nud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importData_btn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.GroupBox data_grpbox;
        private System.Windows.Forms.Label UZ_lbl;
        private System.Windows.Forms.Label UY_lbl;
        private System.Windows.Forms.Label UX_lbl;
        private System.Windows.Forms.Label Z_lbl;
        private System.Windows.Forms.Label Y_lbl;
        private System.Windows.Forms.Label X_lbl;
        private System.Windows.Forms.Label totalParticles_lbl;
        private System.Windows.Forms.Label dim_lbl;
        private System.Windows.Forms.Label T_lbl;
        private System.Windows.Forms.Label P_lbl;
        private System.Windows.Forms.GroupBox gridPA_grpBox;
        private System.Windows.Forms.NumericUpDown gridStep_nud;
        private System.Windows.Forms.Label grid_lbl;
        private System.Windows.Forms.Label epaSize_lbl;
        private System.Windows.Forms.Label paSize_lbl;
        private System.Windows.Forms.Label paZ_lbl;
        private System.Windows.Forms.Label paY_lbl;
        private System.Windows.Forms.Label paX_lbl;
        private System.Windows.Forms.Label symPA_lbl;
        private System.Windows.Forms.Button generatePA_btn;
        private System.Windows.Forms.Label file_lbl;
        private System.Windows.Forms.Button exportPA_btn;
        private System.Windows.Forms.Button plotRaw_btn;
        private System.Windows.Forms.Button test_btn;
        private System.Windows.Forms.GroupBox crop_grpBox;
        private System.Windows.Forms.TextBox xCropMax_txtBox;
        private System.Windows.Forms.TextBox yCropMax_txtBox;
        private System.Windows.Forms.Label xCrop_lbl;
        private System.Windows.Forms.Label yCrop_lbl;
        private System.Windows.Forms.TextBox zCropMax_txtBox;
        private System.Windows.Forms.Label zCrop_lbl;
        private System.Windows.Forms.TextBox xCropMin_txtBox;
        private System.Windows.Forms.TextBox zCropMin_txtBox;
        private System.Windows.Forms.TextBox yCropMin_txtBox;
        private System.Windows.Forms.GroupBox transform_grpBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox zTrans_cmbBox;
        private System.Windows.Forms.ComboBox yTrans_cmbBox;
        private System.Windows.Forms.Button transform_btn;
        private System.Windows.Forms.ComboBox xTrans_cmbBox;
        private System.Windows.Forms.Button crop_btn;
    }
}

