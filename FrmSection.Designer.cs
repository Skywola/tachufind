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
            RTBSection = new RichTextBox();
            SuspendLayout();
            // 
            // RTBSection
            // 
            RTBSection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTBSection.BackColor = SystemColors.ScrollBar;
            RTBSection.Location = new Point(9, 8);
            RTBSection.Margin = new Padding(5, 4, 5, 4);
            RTBSection.Name = "RTBSection";
            RTBSection.Size = new Size(1286, 748);
            RTBSection.TabIndex = 0;
            RTBSection.Text = "";
            // 
            // FrmSection
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1304, 764);
            Controls.Add(RTBSection);
            Font = new Font("Times New Roman", 15F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "FrmSection";
            Text = "  Tachufind";
            FormClosing += FrmSection_FormClosing;
            Load += FrmSection_Load;
            Shown += FrmSection_Shown;
            LocationChanged += FrmSection_LocationChanged;
            SizeChanged += FrmSection_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.RichTextBox RTBSection;

    }
}