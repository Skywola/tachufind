namespace Tachufind
{
    partial class FrmGetQuizes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetQuizes));
            this.btnGetQuizesDelete = new System.Windows.Forms.Button();
            this.btnGetQuizesClose = new System.Windows.Forms.Button();
            this.listBoxQuizes = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnGetQuizesDelete
            // 
            this.btnGetQuizesDelete.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetQuizesDelete.ForeColor = System.Drawing.Color.Red;
            this.btnGetQuizesDelete.Location = new System.Drawing.Point(41, 7);
            this.btnGetQuizesDelete.Name = "btnGetQuizesDelete";
            this.btnGetQuizesDelete.Size = new System.Drawing.Size(70, 28);
            this.btnGetQuizesDelete.TabIndex = 1;
            this.btnGetQuizesDelete.Text = "Delete";
            this.btnGetQuizesDelete.UseVisualStyleBackColor = true;
            this.btnGetQuizesDelete.Click += new System.EventHandler(this.BtnGetQuizesDelete_Click);
            // 
            // btnGetQuizesClose
            // 
            this.btnGetQuizesClose.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetQuizesClose.ForeColor = System.Drawing.Color.Black;
            this.btnGetQuizesClose.Location = new System.Drawing.Point(256, 7);
            this.btnGetQuizesClose.Name = "btnGetQuizesClose";
            this.btnGetQuizesClose.Size = new System.Drawing.Size(70, 28);
            this.btnGetQuizesClose.TabIndex = 2;
            this.btnGetQuizesClose.Text = "Close";
            this.btnGetQuizesClose.UseVisualStyleBackColor = true;
            this.btnGetQuizesClose.Click += new System.EventHandler(this.BtnGetQuizesClose_Click);
            // 
            // listBoxQuizes
            // 
            this.listBoxQuizes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxQuizes.BackColor = System.Drawing.Color.Gainsboro;
            this.listBoxQuizes.Font = new System.Drawing.Font("Times New Roman", 18F);
            this.listBoxQuizes.ForeColor = System.Drawing.Color.Black;
            this.listBoxQuizes.FormattingEnabled = true;
            this.listBoxQuizes.ItemHeight = 27;
            this.listBoxQuizes.Location = new System.Drawing.Point(4, 42);
            this.listBoxQuizes.Name = "listBoxQuizes";
            this.listBoxQuizes.Size = new System.Drawing.Size(354, 517);
            this.listBoxQuizes.Sorted = true;
            this.listBoxQuizes.TabIndex = 3;
            this.listBoxQuizes.Click += new System.EventHandler(this.ListBoxQuizes_Click);
            // 
            // FrmGetQuizes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(362, 561);
            this.Controls.Add(this.listBoxQuizes);
            this.Controls.Add(this.btnGetQuizesClose);
            this.Controls.Add(this.btnGetQuizesDelete);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmGetQuizes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Tachufind";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGetQuizesDelete;
        private System.Windows.Forms.Button btnGetQuizesClose;
        public System.Windows.Forms.ListBox listBoxQuizes;
    }
}