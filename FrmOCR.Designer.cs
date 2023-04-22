namespace Tachufind
{
    partial class FrmOCR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOCR));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.BtnGetImage = new System.Windows.Forms.Button();
            this.BtnGetText = new System.Windows.Forms.Button();
            this.RtbText = new System.Windows.Forms.RichTextBox();
            this.btnCopyAndSuspend = new System.Windows.Forms.Button();
            this.CmbLanguage = new System.Windows.Forms.ComboBox();
            this.BtnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1069, 2142);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(605, 145);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1845, 2132);
            this.button2.Margin = new System.Windows.Forms.Padding(8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(557, 152);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(24, 29);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(680, 940);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // BtnGetImage
            // 
            this.BtnGetImage.Location = new System.Drawing.Point(749, 166);
            this.BtnGetImage.Name = "BtnGetImage";
            this.BtnGetImage.Size = new System.Drawing.Size(210, 59);
            this.BtnGetImage.TabIndex = 3;
            this.BtnGetImage.Text = "Get Image";
            this.BtnGetImage.UseVisualStyleBackColor = true;
            this.BtnGetImage.Click += new System.EventHandler(this.BtnGetImageClick);
            // 
            // BtnGetText
            // 
            this.BtnGetText.Location = new System.Drawing.Point(749, 323);
            this.BtnGetText.Name = "BtnGetText";
            this.BtnGetText.Size = new System.Drawing.Size(210, 59);
            this.BtnGetText.TabIndex = 4;
            this.BtnGetText.Text = "Get Text";
            this.BtnGetText.UseVisualStyleBackColor = true;
            this.BtnGetText.Click += new System.EventHandler(this.BtnGetText_Click);
            // 
            // RtbText
            // 
            this.RtbText.BackColor = System.Drawing.Color.DarkGray;
            this.RtbText.ForeColor = System.Drawing.Color.Black;
            this.RtbText.Location = new System.Drawing.Point(1012, 42);
            this.RtbText.Name = "RtbText";
            this.RtbText.Size = new System.Drawing.Size(680, 940);
            this.RtbText.TabIndex = 5;
            this.RtbText.Text = "";
            // 
            // btnCopyAndSuspend
            // 
            this.btnCopyAndSuspend.Location = new System.Drawing.Point(749, 407);
            this.btnCopyAndSuspend.Name = "btnCopyAndSuspend";
            this.btnCopyAndSuspend.Size = new System.Drawing.Size(210, 59);
            this.btnCopyAndSuspend.TabIndex = 6;
            this.btnCopyAndSuspend.Text = "Copy and Close";
            this.btnCopyAndSuspend.UseVisualStyleBackColor = true;
            this.btnCopyAndSuspend.Click += new System.EventHandler(this.BtnCopyAndClose_Click);
            // 
            // cmbLanguage
            // 
            this.CmbLanguage.FormattingEnabled = true;
            this.CmbLanguage.Items.AddRange(new object[] {
            "French",
            "English",
            "German",
            "Greek",
            "Italian",
            "Latin",
            "Russian",
            "Spanish"});
            this.CmbLanguage.Location = new System.Drawing.Point(749, 252);
            this.CmbLanguage.Name = "cmbLanguage";
            this.CmbLanguage.Size = new System.Drawing.Size(208, 41);
            this.CmbLanguage.TabIndex = 7;
            this.CmbLanguage.Text = "English";
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(749, 500);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(210, 59);
            this.BtnClear.TabIndex = 8;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // FrmOCR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1759, 1008);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.CmbLanguage);
            this.Controls.Add(this.btnCopyAndSuspend);
            this.Controls.Add(this.RtbText);
            this.Controls.Add(this.BtnGetText);
            this.Controls.Add(this.BtnGetImage);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "FrmOCR";
            this.Text = " Tachufind  OCR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOCR_FormClosing);
            this.Load += new System.EventHandler(this.FrmOCRLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button BtnGetImage;
        private System.Windows.Forms.Button BtnGetText;
        private System.Windows.Forms.RichTextBox RtbText;
        private System.Windows.Forms.Button btnCopyAndSuspend;
        private System.Windows.Forms.ComboBox CmbLanguage;
        private System.Windows.Forms.Button BtnClear;
    }
}