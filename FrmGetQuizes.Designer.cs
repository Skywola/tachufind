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
            btnGetQuizesDelete = new Button();
            btnGetQuizesClose = new Button();
            CboQuizFilter = new ComboBox();
            ListBoxQuizes = new ListBox();
            SuspendLayout();
            // 
            // btnGetQuizesDelete
            // 
            btnGetQuizesDelete.BackColor = SystemColors.ActiveCaption;
            btnGetQuizesDelete.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            btnGetQuizesDelete.ForeColor = Color.Red;
            btnGetQuizesDelete.Location = new Point(22, 4);
            btnGetQuizesDelete.Name = "btnGetQuizesDelete";
            btnGetQuizesDelete.Size = new Size(109, 36);
            btnGetQuizesDelete.TabIndex = 0;
            btnGetQuizesDelete.TabStop = false;
            btnGetQuizesDelete.Text = "Delete";
            btnGetQuizesDelete.UseVisualStyleBackColor = false;
            btnGetQuizesDelete.Click += btnGetQuizesDelete_Click;
            // 
            // btnGetQuizesClose
            // 
            btnGetQuizesClose.BackColor = SystemColors.ActiveCaption;
            btnGetQuizesClose.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            btnGetQuizesClose.ForeColor = Color.Red;
            btnGetQuizesClose.Location = new Point(366, 4);
            btnGetQuizesClose.Name = "btnGetQuizesClose";
            btnGetQuizesClose.Size = new Size(109, 36);
            btnGetQuizesClose.TabIndex = 1;
            btnGetQuizesClose.TabStop = false;
            btnGetQuizesClose.Text = "Close";
            btnGetQuizesClose.UseVisualStyleBackColor = false;
            btnGetQuizesClose.Click += btnGetQuizesClose_Click;
            // 
            // CboQuizFilter
            // 
            CboQuizFilter.BackColor = SystemColors.ActiveCaption;
            CboQuizFilter.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CboQuizFilter.FormattingEnabled = true;
            CboQuizFilter.Items.AddRange(new object[] { "All", "Arabic", "English", "French", "Italian", "German", "Greek", "Latin", "Russian", "Spanish", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            CboQuizFilter.Location = new Point(161, 8);
            CboQuizFilter.Name = "CboQuizFilter";
            CboQuizFilter.Size = new Size(167, 34);
            CboQuizFilter.TabIndex = 0;
            CboQuizFilter.Text = "All";
            CboQuizFilter.SelectedIndexChanged += CboQuizFilter_SelectedIndexChanged;
            // 
            // ListBoxQuizes
            // 
            ListBoxQuizes.BackColor = SystemColors.ActiveCaption;
            ListBoxQuizes.FormattingEnabled = true;
            ListBoxQuizes.ItemHeight = 27;
            ListBoxQuizes.Location = new Point(3, 48);
            ListBoxQuizes.MaximumSize = new Size(511, 706);
            ListBoxQuizes.MinimumSize = new Size(511, 706);
            ListBoxQuizes.Name = "ListBoxQuizes";
            ListBoxQuizes.Size = new Size(511, 706);
            ListBoxQuizes.TabIndex = 1;
            ListBoxQuizes.Click += ListBoxQuizes_Click;
            ListBoxQuizes.SelectedIndexChanged += ListBoxQuizes_SelectedIndexChanged;
            // 
            // FrmGetQuizes
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(515, 762);
            Controls.Add(ListBoxQuizes);
            Controls.Add(CboQuizFilter);
            Controls.Add(btnGetQuizesClose);
            Controls.Add(btnGetQuizesDelete);
            Font = new Font("Times New Roman", 14.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(533, 809);
            MinimumSize = new Size(533, 809);
            Name = "FrmGetQuizes";
            Text = "  Tachufind";
            FormClosing += FrmGetQuizes_FormClosing;
            Load += FrmGetQuizes_Load;
            LocationChanged += FrmGetQuizes_LocationChanged;
            ResumeLayout(false);
        }

        #endregion

        private Button btnGetQuizesDelete;
        private Button btnGetQuizesClose;
        internal ComboBox CboQuizFilter;
        internal ListBox ListBoxQuizes;
    }
}