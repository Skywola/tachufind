namespace Tachufind
{
    partial class FrmShortcuts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShortcuts));
            this.RTBShortcuts = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RTBShortcuts
            // 
            this.RTBShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTBShortcuts.BackColor = System.Drawing.Color.DarkGray;
            this.RTBShortcuts.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTBShortcuts.ForeColor = System.Drawing.Color.Black;
            this.RTBShortcuts.Location = new System.Drawing.Point(12, 3);
            this.RTBShortcuts.Name = "RTBShortcuts";
            this.RTBShortcuts.ReadOnly = true;
            this.RTBShortcuts.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RTBShortcuts.Size = new System.Drawing.Size(1148, 887);
            this.RTBShortcuts.TabIndex = 0;
            this.RTBShortcuts.Text = "Test is filled in from Globals.ancientGreekShortcuts for example.";
            this.RTBShortcuts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RTBShortcuts_KeyDown);
            // 
            // FrmShortcuts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1172, 911);
            this.Controls.Add(this.RTBShortcuts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmShortcuts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Keyboard Shortcuts";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmShortcuts_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox RTBShortcuts;
    }
}