namespace Tachufind
{
    partial class FrmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOptions));
            this.chkOpenFromLastLocation = new System.Windows.Forms.CheckBox();
            this.chkOptOutOfFontChangeWarnings = new System.Windows.Forms.CheckBox();
            this.chkAlwaysUseWhiteBkgForPrint = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkOpenFromLastLocation
            // 
            this.chkOpenFromLastLocation.AutoSize = true;
            this.chkOpenFromLastLocation.Checked = true;
            this.chkOpenFromLastLocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenFromLastLocation.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chkOpenFromLastLocation.Location = new System.Drawing.Point(25, 13);
            this.chkOpenFromLastLocation.Name = "chkOpenFromLastLocation";
            this.chkOpenFromLastLocation.Size = new System.Drawing.Size(169, 23);
            this.chkOpenFromLastLocation.TabIndex = 0;
            this.chkOpenFromLastLocation.Text = "Open from last location";
            this.chkOpenFromLastLocation.UseVisualStyleBackColor = true;
            this.chkOpenFromLastLocation.CheckStateChanged += new System.EventHandler(this.ChkOpenFromLastLocation_CheckStateChanged);
            // 
            // chkOptOutOfFontChangeWarnings
            // 
            this.chkOptOutOfFontChangeWarnings.AutoSize = true;
            this.chkOptOutOfFontChangeWarnings.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chkOptOutOfFontChangeWarnings.Location = new System.Drawing.Point(25, 44);
            this.chkOptOutOfFontChangeWarnings.Name = "chkOptOutOfFontChangeWarnings";
            this.chkOptOutOfFontChangeWarnings.Size = new System.Drawing.Size(280, 23);
            this.chkOptOutOfFontChangeWarnings.TabIndex = 1;
            this.chkOptOutOfFontChangeWarnings.Text = "Opt out of all future font change warnings.";
            this.chkOptOutOfFontChangeWarnings.UseVisualStyleBackColor = true;
            this.chkOptOutOfFontChangeWarnings.CheckStateChanged += new System.EventHandler(this.ChkOptOutOfFontChangeWarnings_CheckStateChanged);
            // 
            // chkAlwaysUseWhiteBkgForPrint
            // 
            this.chkAlwaysUseWhiteBkgForPrint.AutoSize = true;
            this.chkAlwaysUseWhiteBkgForPrint.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chkAlwaysUseWhiteBkgForPrint.Location = new System.Drawing.Point(25, 75);
            this.chkAlwaysUseWhiteBkgForPrint.Name = "chkAlwaysUseWhiteBkgForPrint";
            this.chkAlwaysUseWhiteBkgForPrint.Size = new System.Drawing.Size(308, 23);
            this.chkAlwaysUseWhiteBkgForPrint.TabIndex = 2;
            this.chkAlwaysUseWhiteBkgForPrint.Text = "Always use a white background when printing.";
            this.chkAlwaysUseWhiteBkgForPrint.UseVisualStyleBackColor = true;
            this.chkAlwaysUseWhiteBkgForPrint.Visible = false;
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(464, 111);
            this.Controls.Add(this.chkAlwaysUseWhiteBkgForPrint);
            this.Controls.Add(this.chkOptOutOfFontChangeWarnings);
            this.Controls.Add(this.chkOpenFromLastLocation);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Tachufind";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOptions_FormClosing);
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkOpenFromLastLocation;
        private System.Windows.Forms.CheckBox chkOptOutOfFontChangeWarnings;
        private System.Windows.Forms.CheckBox chkAlwaysUseWhiteBkgForPrint;
    }
}