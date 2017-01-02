namespace Rocky.SqliteQuery
{
    partial class SettingFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingFrom));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFontFamily = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnApplyToSys = new System.Windows.Forms.Button();
            this.btnCancleToSys = new System.Windows.Forms.Button();
            this.btnOKToSys = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 269);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tabPage1.Controls.Add(this.numFontSize);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cbFontFamily);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnApplyToSys);
            this.tabPage1.Controls.Add(this.btnCancleToSys);
            this.tabPage1.Controls.Add(this.btnOKToSys);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(408, 243);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统设置";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(64, 51);
            this.numFontSize.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(207, 20);
            this.numFontSize.TabIndex = 8;
            this.numFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "px";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "大小：";
            // 
            // cbFontFamily
            // 
            this.cbFontFamily.FormattingEnabled = true;
            this.cbFontFamily.Location = new System.Drawing.Point(64, 13);
            this.cbFontFamily.Name = "cbFontFamily";
            this.cbFontFamily.Size = new System.Drawing.Size(254, 21);
            this.cbFontFamily.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "字体：";
            // 
            // btnApplyToSys
            // 
            this.btnApplyToSys.Location = new System.Drawing.Point(325, 207);
            this.btnApplyToSys.Name = "btnApplyToSys";
            this.btnApplyToSys.Size = new System.Drawing.Size(75, 25);
            this.btnApplyToSys.TabIndex = 2;
            this.btnApplyToSys.Text = "应用";
            this.btnApplyToSys.UseVisualStyleBackColor = true;
            this.btnApplyToSys.Click += new System.EventHandler(this.btnApplyToSys_Click);
            // 
            // btnCancleToSys
            // 
            this.btnCancleToSys.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancleToSys.Location = new System.Drawing.Point(243, 208);
            this.btnCancleToSys.Name = "btnCancleToSys";
            this.btnCancleToSys.Size = new System.Drawing.Size(75, 25);
            this.btnCancleToSys.TabIndex = 1;
            this.btnCancleToSys.Text = "取消";
            this.btnCancleToSys.UseVisualStyleBackColor = true;
            this.btnCancleToSys.Click += new System.EventHandler(this.btnCancleToSys_Click);
            // 
            // btnOKToSys
            // 
            this.btnOKToSys.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKToSys.Location = new System.Drawing.Point(162, 208);
            this.btnOKToSys.Name = "btnOKToSys";
            this.btnOKToSys.Size = new System.Drawing.Size(75, 25);
            this.btnOKToSys.TabIndex = 0;
            this.btnOKToSys.Text = "确定";
            this.btnOKToSys.UseVisualStyleBackColor = true;
            this.btnOKToSys.Click += new System.EventHandler(this.btnOKToSys_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(408, 243);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // SettingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 269);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingFrom_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnOKToSys;
        private System.Windows.Forms.Button btnCancleToSys;
        private System.Windows.Forms.Button btnApplyToSys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFontFamily;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFontSize;
    }
}