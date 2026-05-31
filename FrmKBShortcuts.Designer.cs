namespace Tachufind
{
    partial class FrmKBShortcuts
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            RTBKBShortcuts = new RichTextBox();
            SuspendLayout();
            // 
            // RTBKBShortcuts
            // 
            RTBKBShortcuts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTBKBShortcuts.BackColor = SystemColors.ActiveBorder;
            RTBKBShortcuts.Location = new Point(3, 2);
            RTBKBShortcuts.Name = "RTBKBShortcuts";
            RTBKBShortcuts.Size = new Size(1157, 897);
            RTBKBShortcuts.TabIndex = 0;
            RTBKBShortcuts.Text = "";
            // 
            // FrmKBShortcuts
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1172, 911);
            Controls.Add(RTBKBShortcuts);
            Font = new Font("Times New Roman", 15F);
            Name = "FrmKBShortcuts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KB Shortcuts";
            FormClosing += FrmKBShortcuts_FormClosing;
            Load += FrmKBShortcuts_Load;
            LocationChanged += FrmKBShortcuts_LocationChanged;
            SizeChanged += FrmKBShortcuts_SizeChanged;
            KeyDown += FrmKBShortcuts_KeyDown;
            ResumeLayout(false);
        }

        public System.Windows.Forms.RichTextBox RTBKBShortcuts;
    }
}
