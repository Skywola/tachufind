namespace Tachufind
{
    partial class FrmGetSearches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetSearches));
            this.listOfSearches = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listOfSearches
            // 
            this.listOfSearches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listOfSearches.BackColor = System.Drawing.Color.Gainsboro;
            this.listOfSearches.FormattingEnabled = true;
            this.listOfSearches.ItemHeight = 21;
            this.listOfSearches.Location = new System.Drawing.Point(4, 42);
            this.listOfSearches.Name = "listOfSearches";
            this.listOfSearches.Size = new System.Drawing.Size(354, 424);
            this.listOfSearches.Sorted = true;
            this.listOfSearches.TabIndex = 7;
            this.listOfSearches.Click += new System.EventHandler(this.ListOfSearches_Click);
            this.listOfSearches.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListOfSearches_KeyUp);
            this.listOfSearches.MouseEnter += new System.EventHandler(this.ListOfSearches_MouseEnter);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(256, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 28);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(41, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 28);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // FrmGetSearches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(362, 474);
            this.Controls.Add(this.listOfSearches);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmGetSearches";
            this.Text = " Tachufind";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGetSearches_FormClosing);
            this.Load += new System.EventHandler(this.FrmGetSearches_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGetSearches_KeyDown);
            this.MouseEnter += new System.EventHandler(this.FrmGetSearches_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listOfSearches;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
    }
}