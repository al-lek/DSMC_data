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
            this.plotRaw_btn = new System.Windows.Forms.Button();
            this.T_lbl = new System.Windows.Forms.Label();
            this.UZ_lbl = new System.Windows.Forms.Label();
            this.UY_lbl = new System.Windows.Forms.Label();
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
            this.gridPA_grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStep_nud)).BeginInit();
            this.SuspendLayout();
            // 
            // importData_btn
            // 
            this.importData_btn.Location = new System.Drawing.Point(71, 19);
            this.importData_btn.Name = "importData_btn";
            this.importData_btn.Size = new System.Drawing.Size(116, 23);
            this.importData_btn.TabIndex = 0;
            this.importData_btn.Text = "Import DSMC Data";
            this.importData_btn.UseVisualStyleBackColor = true;
            this.importData_btn.Click += new System.EventHandler(this.importData_btn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 367);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(659, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(29, 17);
            this.status.Text = "Idle.";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // data_grpbox
            // 
            this.data_grpbox.Controls.Add(this.plotRaw_btn);
            this.data_grpbox.Controls.Add(this.T_lbl);
            this.data_grpbox.Controls.Add(this.UZ_lbl);
            this.data_grpbox.Controls.Add(this.UY_lbl);
            this.data_grpbox.Controls.Add(this.P_lbl);
            this.data_grpbox.Controls.Add(this.UX_lbl);
            this.data_grpbox.Controls.Add(this.importData_btn);
            this.data_grpbox.Controls.Add(this.Z_lbl);
            this.data_grpbox.Controls.Add(this.Y_lbl);
            this.data_grpbox.Controls.Add(this.X_lbl);
            this.data_grpbox.Controls.Add(this.totalParticles_lbl);
            this.data_grpbox.Controls.Add(this.file_lbl);
            this.data_grpbox.Controls.Add(this.dim_lbl);
            this.data_grpbox.Location = new System.Drawing.Point(12, 12);
            this.data_grpbox.Name = "data_grpbox";
            this.data_grpbox.Size = new System.Drawing.Size(335, 281);
            this.data_grpbox.TabIndex = 2;
            this.data_grpbox.TabStop = false;
            this.data_grpbox.Text = "Data properties";
            // 
            // plotRaw_btn
            // 
            this.plotRaw_btn.Location = new System.Drawing.Point(83, 234);
            this.plotRaw_btn.Name = "plotRaw_btn";
            this.plotRaw_btn.Size = new System.Drawing.Size(75, 23);
            this.plotRaw_btn.TabIndex = 4;
            this.plotRaw_btn.Text = "Plot RAW";
            this.plotRaw_btn.UseVisualStyleBackColor = true;
            this.plotRaw_btn.Click += new System.EventHandler(this.plotRaw_btn_Click);
            // 
            // T_lbl
            // 
            this.T_lbl.AutoSize = true;
            this.T_lbl.Location = new System.Drawing.Point(156, 185);
            this.T_lbl.Name = "T_lbl";
            this.T_lbl.Size = new System.Drawing.Size(35, 13);
            this.T_lbl.TabIndex = 1;
            this.T_lbl.Text = "label2";
            this.T_lbl.Visible = false;
            // 
            // UZ_lbl
            // 
            this.UZ_lbl.AutoSize = true;
            this.UZ_lbl.Location = new System.Drawing.Point(156, 162);
            this.UZ_lbl.Name = "UZ_lbl";
            this.UZ_lbl.Size = new System.Drawing.Size(35, 13);
            this.UZ_lbl.TabIndex = 1;
            this.UZ_lbl.Text = "label2";
            this.UZ_lbl.Visible = false;
            // 
            // UY_lbl
            // 
            this.UY_lbl.AutoSize = true;
            this.UY_lbl.Location = new System.Drawing.Point(156, 139);
            this.UY_lbl.Name = "UY_lbl";
            this.UY_lbl.Size = new System.Drawing.Size(35, 13);
            this.UY_lbl.TabIndex = 1;
            this.UY_lbl.Text = "label2";
            this.UY_lbl.Visible = false;
            // 
            // P_lbl
            // 
            this.P_lbl.AutoSize = true;
            this.P_lbl.Location = new System.Drawing.Point(17, 185);
            this.P_lbl.Name = "P_lbl";
            this.P_lbl.Size = new System.Drawing.Size(35, 13);
            this.P_lbl.TabIndex = 1;
            this.P_lbl.Text = "label2";
            this.P_lbl.Visible = false;
            // 
            // UX_lbl
            // 
            this.UX_lbl.AutoSize = true;
            this.UX_lbl.Location = new System.Drawing.Point(156, 116);
            this.UX_lbl.Name = "UX_lbl";
            this.UX_lbl.Size = new System.Drawing.Size(35, 13);
            this.UX_lbl.TabIndex = 1;
            this.UX_lbl.Text = "label2";
            this.UX_lbl.Visible = false;
            // 
            // Z_lbl
            // 
            this.Z_lbl.AutoSize = true;
            this.Z_lbl.Location = new System.Drawing.Point(17, 162);
            this.Z_lbl.Name = "Z_lbl";
            this.Z_lbl.Size = new System.Drawing.Size(35, 13);
            this.Z_lbl.TabIndex = 1;
            this.Z_lbl.Text = "label2";
            this.Z_lbl.Visible = false;
            // 
            // Y_lbl
            // 
            this.Y_lbl.AutoSize = true;
            this.Y_lbl.Location = new System.Drawing.Point(17, 139);
            this.Y_lbl.Name = "Y_lbl";
            this.Y_lbl.Size = new System.Drawing.Size(35, 13);
            this.Y_lbl.TabIndex = 1;
            this.Y_lbl.Text = "label2";
            this.Y_lbl.Visible = false;
            // 
            // X_lbl
            // 
            this.X_lbl.AutoSize = true;
            this.X_lbl.Location = new System.Drawing.Point(17, 116);
            this.X_lbl.Name = "X_lbl";
            this.X_lbl.Size = new System.Drawing.Size(35, 13);
            this.X_lbl.TabIndex = 1;
            this.X_lbl.Text = "label2";
            this.X_lbl.Visible = false;
            // 
            // totalParticles_lbl
            // 
            this.totalParticles_lbl.AutoSize = true;
            this.totalParticles_lbl.Location = new System.Drawing.Point(17, 90);
            this.totalParticles_lbl.Name = "totalParticles_lbl";
            this.totalParticles_lbl.Size = new System.Drawing.Size(35, 13);
            this.totalParticles_lbl.TabIndex = 1;
            this.totalParticles_lbl.Text = "label2";
            this.totalParticles_lbl.Visible = false;
            // 
            // file_lbl
            // 
            this.file_lbl.AutoSize = true;
            this.file_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_lbl.Location = new System.Drawing.Point(17, 63);
            this.file_lbl.Name = "file_lbl";
            this.file_lbl.Size = new System.Drawing.Size(41, 13);
            this.file_lbl.TabIndex = 0;
            this.file_lbl.Text = "label1";
            this.file_lbl.Visible = false;
            // 
            // dim_lbl
            // 
            this.dim_lbl.AutoSize = true;
            this.dim_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dim_lbl.Location = new System.Drawing.Point(202, 63);
            this.dim_lbl.Name = "dim_lbl";
            this.dim_lbl.Size = new System.Drawing.Size(41, 13);
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
            this.gridPA_grpBox.Location = new System.Drawing.Point(353, 12);
            this.gridPA_grpBox.Name = "gridPA_grpBox";
            this.gridPA_grpBox.Size = new System.Drawing.Size(294, 281);
            this.gridPA_grpBox.TabIndex = 3;
            this.gridPA_grpBox.TabStop = false;
            this.gridPA_grpBox.Text = "PA - Grid properties";
            // 
            // exportPA_btn
            // 
            this.exportPA_btn.Location = new System.Drawing.Point(145, 19);
            this.exportPA_btn.Name = "exportPA_btn";
            this.exportPA_btn.Size = new System.Drawing.Size(75, 23);
            this.exportPA_btn.TabIndex = 5;
            this.exportPA_btn.Text = "Save PAs";
            this.exportPA_btn.UseVisualStyleBackColor = true;
            this.exportPA_btn.Click += new System.EventHandler(this.exportPA_btn_Click);
            // 
            // symPA_lbl
            // 
            this.symPA_lbl.AutoSize = true;
            this.symPA_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symPA_lbl.Location = new System.Drawing.Point(159, 63);
            this.symPA_lbl.Name = "symPA_lbl";
            this.symPA_lbl.Size = new System.Drawing.Size(41, 13);
            this.symPA_lbl.TabIndex = 2;
            this.symPA_lbl.Text = "label1";
            this.symPA_lbl.Visible = false;
            // 
            // generatePA_btn
            // 
            this.generatePA_btn.Location = new System.Drawing.Point(13, 19);
            this.generatePA_btn.Name = "generatePA_btn";
            this.generatePA_btn.Size = new System.Drawing.Size(120, 23);
            this.generatePA_btn.TabIndex = 4;
            this.generatePA_btn.Text = "Generate PA\'s";
            this.generatePA_btn.UseVisualStyleBackColor = true;
            this.generatePA_btn.Click += new System.EventHandler(this.generatePA_btn_Click);
            // 
            // epaSize_lbl
            // 
            this.epaSize_lbl.AutoSize = true;
            this.epaSize_lbl.Location = new System.Drawing.Point(119, 139);
            this.epaSize_lbl.Name = "epaSize_lbl";
            this.epaSize_lbl.Size = new System.Drawing.Size(35, 13);
            this.epaSize_lbl.TabIndex = 2;
            this.epaSize_lbl.Text = "label1";
            this.epaSize_lbl.Visible = false;
            // 
            // paSize_lbl
            // 
            this.paSize_lbl.AutoSize = true;
            this.paSize_lbl.Location = new System.Drawing.Point(119, 116);
            this.paSize_lbl.Name = "paSize_lbl";
            this.paSize_lbl.Size = new System.Drawing.Size(35, 13);
            this.paSize_lbl.TabIndex = 2;
            this.paSize_lbl.Text = "label1";
            this.paSize_lbl.Visible = false;
            // 
            // paZ_lbl
            // 
            this.paZ_lbl.AutoSize = true;
            this.paZ_lbl.Location = new System.Drawing.Point(15, 162);
            this.paZ_lbl.Name = "paZ_lbl";
            this.paZ_lbl.Size = new System.Drawing.Size(35, 13);
            this.paZ_lbl.TabIndex = 2;
            this.paZ_lbl.Text = "label1";
            this.paZ_lbl.Visible = false;
            // 
            // paY_lbl
            // 
            this.paY_lbl.AutoSize = true;
            this.paY_lbl.Location = new System.Drawing.Point(15, 139);
            this.paY_lbl.Name = "paY_lbl";
            this.paY_lbl.Size = new System.Drawing.Size(35, 13);
            this.paY_lbl.TabIndex = 2;
            this.paY_lbl.Text = "label1";
            this.paY_lbl.Visible = false;
            // 
            // paX_lbl
            // 
            this.paX_lbl.AutoSize = true;
            this.paX_lbl.Location = new System.Drawing.Point(15, 116);
            this.paX_lbl.Name = "paX_lbl";
            this.paX_lbl.Size = new System.Drawing.Size(35, 13);
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
            this.gridStep_nud.Location = new System.Drawing.Point(88, 61);
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
            this.gridStep_nud.Size = new System.Drawing.Size(50, 20);
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
            this.grid_lbl.Location = new System.Drawing.Point(15, 63);
            this.grid_lbl.Name = "grid_lbl";
            this.grid_lbl.Size = new System.Drawing.Size(69, 13);
            this.grid_lbl.TabIndex = 0;
            this.grid_lbl.Text = "grid step[mm]";
            this.grid_lbl.Visible = false;
            // 
            // test_btn
            // 
            this.test_btn.Location = new System.Drawing.Point(482, 321);
            this.test_btn.Name = "test_btn";
            this.test_btn.Size = new System.Drawing.Size(32, 23);
            this.test_btn.TabIndex = 4;
            this.test_btn.Text = "test";
            this.test_btn.UseVisualStyleBackColor = true;
            this.test_btn.Click += new System.EventHandler(this.test_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 389);
            this.Controls.Add(this.test_btn);
            this.Controls.Add(this.gridPA_grpBox);
            this.Controls.Add(this.data_grpbox);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "DSMC data tool v0.2";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.data_grpbox.ResumeLayout(false);
            this.data_grpbox.PerformLayout();
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
    }
}

