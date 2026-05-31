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
            ListOfSearchTitles = new ListBox();
            cboSearchFilter = new ComboBox();
            btnGetSearchesClose = new Button();
            btnGetSearchesDelete = new Button();
            SuspendLayout();
            // 
            // ListOfSearchTitles
            // 
            ListOfSearchTitles.BackColor = Color.Sienna;
            ListOfSearchTitles.FormattingEnabled = true;
            ListOfSearchTitles.ItemHeight = 27;
            ListOfSearchTitles.Location = new Point(3, 55);
            ListOfSearchTitles.Name = "ListOfSearchTitles";
            ListOfSearchTitles.Size = new Size(485, 706);
            ListOfSearchTitles.TabIndex = 1;
            ListOfSearchTitles.MouseClick += ListOfSearches_MouseClick;
            ListOfSearchTitles.KeyUp += ListOfSearches_KeyUp;
            ListOfSearchTitles.MouseEnter += ListOfSearches_MouseEnter;
            // 
            // cboSearchFilter
            // 
            cboSearchFilter.BackColor = Color.Sienna;
            cboSearchFilter.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboSearchFilter.FormattingEnabled = true;
            cboSearchFilter.Items.AddRange(new object[] { "All", "Arabic", "English", "French", "Italian", "German", "Greek", "Latin", "Russian", "Spanish", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            cboSearchFilter.Location = new Point(153, 15);
            cboSearchFilter.Name = "cboSearchFilter";
            cboSearchFilter.Size = new Size(167, 34);
            cboSearchFilter.TabIndex = 0;
            cboSearchFilter.Text = "All";
            cboSearchFilter.SelectedIndexChanged += cboSearchFilter_SelectedIndexChanged;
            // 
            // btnGetSearchesClose
            // 
            btnGetSearchesClose.BackColor = Color.Sienna;
            btnGetSearchesClose.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            btnGetSearchesClose.ForeColor = Color.Red;
            btnGetSearchesClose.Location = new Point(353, 13);
            btnGetSearchesClose.Name = "btnGetSearchesClose";
            btnGetSearchesClose.Size = new Size(109, 36);
            btnGetSearchesClose.TabIndex = 5;
            btnGetSearchesClose.TabStop = false;
            btnGetSearchesClose.Text = "Close";
            btnGetSearchesClose.UseVisualStyleBackColor = false;
            btnGetSearchesClose.Click += BtnGetSearchesClose_Click;
            // 
            // btnGetSearchesDelete
            // 
            btnGetSearchesDelete.BackColor = Color.Sienna;
            btnGetSearchesDelete.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            btnGetSearchesDelete.ForeColor = Color.Red;
            btnGetSearchesDelete.Location = new Point(21, 13);
            btnGetSearchesDelete.Name = "btnGetSearchesDelete";
            btnGetSearchesDelete.Size = new Size(109, 36);
            btnGetSearchesDelete.TabIndex = 4;
            btnGetSearchesDelete.TabStop = false;
            btnGetSearchesDelete.Text = "Delete";
            btnGetSearchesDelete.UseVisualStyleBackColor = false;
            btnGetSearchesDelete.Click += BtnGetSearchesDelete_Click;
            // 
            // FrmGetSearches
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(492, 777);
            Controls.Add(ListOfSearchTitles);
            Controls.Add(cboSearchFilter);
            Controls.Add(btnGetSearchesClose);
            Controls.Add(btnGetSearchesDelete);
            Font = new Font("Times New Roman", 14.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(510, 824);
            MinimumSize = new Size(510, 824);
            Name = "FrmGetSearches";
            Text = "  Tachufind";
            TopMost = true;
            FormClosing += FrmGetSearches_FormClosing;
            Load += FrmGetSearches_Load;
            LocationChanged += FrmGetSearches_LocationChanged;
            KeyDown += FrmGetSearches_KeyDown;
            MouseEnter += FrmGetSearches_MouseEnter;
            ResumeLayout(false);
        }

        #endregion

        public ListBox ListOfSearchTitles;
        public ComboBox cboSearchFilter;
        private Button btnGetSearchesClose;
        private Button btnGetSearchesDelete;
    }
}