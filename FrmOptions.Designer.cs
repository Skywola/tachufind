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
            chkOpenFromLastLocation = new CheckBox();
            chkUseDefaultColors = new CheckBox();
            chkPreserveHighlighting = new CheckBox();
            ChkLoopTTSPlayback = new CheckBox();
            SuspendLayout();
            // 
            // chkOpenFromLastLocation
            // 
            chkOpenFromLastLocation.AutoSize = true;
            chkOpenFromLastLocation.Checked = true;
            chkOpenFromLastLocation.CheckState = CheckState.Checked;
            chkOpenFromLastLocation.Location = new Point(14, 13);
            chkOpenFromLastLocation.Name = "chkOpenFromLastLocation";
            chkOpenFromLastLocation.Size = new Size(242, 29);
            chkOpenFromLastLocation.TabIndex = 0;
            chkOpenFromLastLocation.Text = "Open last working file";
            chkOpenFromLastLocation.UseVisualStyleBackColor = true;
            chkOpenFromLastLocation.CheckStateChanged += chkOpenFromLastLocation_CheckStateChanged;
            // 
            // chkUseDefaultColors
            // 
            chkUseDefaultColors.AutoSize = true;
            chkUseDefaultColors.Checked = true;
            chkUseDefaultColors.CheckState = CheckState.Checked;
            chkUseDefaultColors.Location = new Point(14, 55);
            chkUseDefaultColors.Name = "chkUseDefaultColors";
            chkUseDefaultColors.Size = new Size(212, 29);
            chkUseDefaultColors.TabIndex = 2;
            chkUseDefaultColors.Text = "Use Default Colors";
            chkUseDefaultColors.UseVisualStyleBackColor = true;
            chkUseDefaultColors.CheckedChanged += chkUseDefaultColors_CheckedChanged;
            // 
            // chkPreserveHighlighting
            // 
            chkPreserveHighlighting.AutoSize = true;
            chkPreserveHighlighting.Location = new Point(14, 144);
            chkPreserveHighlighting.Name = "chkPreserveHighlighting";
            chkPreserveHighlighting.Size = new Size(708, 79);
            chkPreserveHighlighting.TabIndex = 3;
            chkPreserveHighlighting.Text = resources.GetString("chkPreserveHighlighting.Text");
            chkPreserveHighlighting.UseVisualStyleBackColor = true;
            chkPreserveHighlighting.CheckedChanged += chkPreserveHighlighting_CheckedChanged;
            // 
            // ChkLoopTTSPlayback
            // 
            ChkLoopTTSPlayback.AutoSize = true;
            ChkLoopTTSPlayback.Checked = true;
            ChkLoopTTSPlayback.CheckState = CheckState.Checked;
            ChkLoopTTSPlayback.Location = new Point(14, 101);
            ChkLoopTTSPlayback.Name = "ChkLoopTTSPlayback";
            ChkLoopTTSPlayback.Size = new Size(425, 29);
            ChkLoopTTSPlayback.TabIndex = 4;
            ChkLoopTTSPlayback.Text = "Default Text-To-Speech to Loop Playback";
            ChkLoopTTSPlayback.UseVisualStyleBackColor = true;
            ChkLoopTTSPlayback.CheckedChanged += ChkLoopTTSPlayback_CheckedChanged;
            // 
            // FrmOptions
            // 
            AutoScaleDimensions = new SizeF(12F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(722, 233);
            Controls.Add(ChkLoopTTSPlayback);
            Controls.Add(chkPreserveHighlighting);
            Controls.Add(chkUseDefaultColors);
            Controls.Add(chkOpenFromLastLocation);
            Font = new Font("Times New Roman", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(740, 280);
            MinimumSize = new Size(740, 280);
            Name = "FrmOptions";
            Text = "  Tachufind";
            FormClosing += FrmOptions_FormClosing;
            Load += FrmOptions_Load;
            LocationChanged += FrmOptions_LocationChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkOpenFromLastLocation;
        public CheckBox chkUseDefaultColors;
        private CheckBox chkPreserveHighlighting;
        public CheckBox ChkLoopTTSPlayback;
    }
}