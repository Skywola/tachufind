namespace Tachufind
{
    partial class FrmSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSection));
            this.RTBSection = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RTBSection
            // 
            this.RTBSection.AcceptsTab = true;
            this.RTBSection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTBSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RTBSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.RTBSection.Location = new System.Drawing.Point(9, 9);
            this.RTBSection.Name = "RTBSection";
            this.RTBSection.Size = new System.Drawing.Size(810, 504);
            this.RTBSection.TabIndex = 0;
            this.RTBSection.Text = "";
            // 
            // FrmSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 521);
            this.Controls.Add(this.RTBSection);
            this.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "FrmSection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Tachufind";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSection_FormClosing);
            this.Load += new System.EventHandler(this.FrmSection_Load);
            this.LocationChanged += new System.EventHandler(this.FrmSection_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox RTBSection;
    }
}