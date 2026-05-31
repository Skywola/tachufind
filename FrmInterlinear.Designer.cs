namespace Tachufind
{
    partial class FrmInterlinear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInterlinear));
            btnTop = new Button();
            btnBottom = new Button();
            txtFilePath1 = new TextBox();
            txtFilePath2 = new TextBox();
            btnCreate = new Button();
            txtResult = new TextBox();
            btnClose = new Button();
            label1 = new Label();
            chkMaxLineTimes = new CheckBox();
            LblError = new Label();
            TxtLineNumber = new TextBox();
            txtErrorList = new TextBox();
            SuspendLayout();
            // 
            // btnTop
            // 
            btnTop.BackColor = SystemColors.AppWorkspace;
            btnTop.Location = new Point(12, 11);
            btnTop.Name = "btnTop";
            btnTop.Size = new Size(213, 34);
            btnTop.TabIndex = 0;
            btnTop.Text = "Get Top Subtitle File";
            btnTop.UseVisualStyleBackColor = false;
            btnTop.Click += BtnTop_Click;
            // 
            // btnBottom
            // 
            btnBottom.BackColor = SystemColors.AppWorkspace;
            btnBottom.Location = new Point(12, 53);
            btnBottom.Name = "btnBottom";
            btnBottom.Size = new Size(213, 34);
            btnBottom.TabIndex = 1;
            btnBottom.Text = "Get Bottom Subtitle File";
            btnBottom.UseVisualStyleBackColor = false;
            btnBottom.Click += BtnBottom_Click;
            // 
            // txtFilePath1
            // 
            txtFilePath1.BackColor = SystemColors.AppWorkspace;
            txtFilePath1.Location = new Point(244, 13);
            txtFilePath1.Name = "txtFilePath1";
            txtFilePath1.Size = new Size(881, 32);
            txtFilePath1.TabIndex = 2;
            txtFilePath1.TabStop = false;
            txtFilePath1.MouseMove += txtFilePath1_MouseMove;
            // 
            // txtFilePath2
            // 
            txtFilePath2.BackColor = SystemColors.AppWorkspace;
            txtFilePath2.Location = new Point(244, 55);
            txtFilePath2.Name = "txtFilePath2";
            txtFilePath2.Size = new Size(881, 32);
            txtFilePath2.TabIndex = 3;
            txtFilePath2.TabStop = false;
            txtFilePath2.MouseMove += txtFilePath2_MouseMove;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = SystemColors.AppWorkspace;
            btnCreate.Location = new Point(12, 102);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(93, 34);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += BtnCreate_Click;
            // 
            // txtResult
            // 
            txtResult.BackColor = SystemColors.AppWorkspace;
            txtResult.Location = new Point(114, 103);
            txtResult.Name = "txtResult";
            txtResult.Size = new Size(133, 32);
            txtResult.TabIndex = 5;
            txtResult.TabStop = false;
            txtResult.MouseMove += txtResult_MouseMove;
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.AppWorkspace;
            btnClose.Location = new Point(275, 100);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(93, 34);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 15F);
            label1.Location = new Point(12, 175);
            label1.Name = "label1";
            label1.Size = new Size(207, 29);
            label1.TabIndex = 7;
            label1.Text = "Error between lines";
            // 
            // chkMaxLineTimes
            // 
            chkMaxLineTimes.AutoSize = true;
            chkMaxLineTimes.Checked = true;
            chkMaxLineTimes.CheckState = CheckState.Checked;
            chkMaxLineTimes.Font = new Font("Times New Roman", 14F);
            chkMaxLineTimes.Location = new Point(422, 102);
            chkMaxLineTimes.Name = "chkMaxLineTimes";
            chkMaxLineTimes.Size = new Size(191, 31);
            chkMaxLineTimes.TabIndex = 8;
            chkMaxLineTimes.TabStop = false;
            chkMaxLineTimes.Text = "Max Line Times";
            chkMaxLineTimes.UseVisualStyleBackColor = true;
            // 
            // LblError
            // 
            LblError.AutoSize = true;
            LblError.Font = new Font("Times New Roman", 16F);
            LblError.Location = new Point(102, 220);
            LblError.Name = "LblError";
            LblError.Size = new Size(117, 31);
            LblError.TabIndex = 10;
            LblError.Text = "ERROR :";
            // 
            // TxtLineNumber
            // 
            TxtLineNumber.BackColor = SystemColors.AppWorkspace;
            TxtLineNumber.Location = new Point(231, 172);
            TxtLineNumber.Name = "TxtLineNumber";
            TxtLineNumber.Size = new Size(356, 32);
            TxtLineNumber.TabIndex = 12;
            TxtLineNumber.TabStop = false;
            TxtLineNumber.MouseMove += TxtLineNumber_MouseMove;
            // 
            // txtErrorList
            // 
            txtErrorList.BackColor = SystemColors.AppWorkspace;
            txtErrorList.Font = new Font("Times New Roman", 18F);
            txtErrorList.Location = new Point(231, 214);
            txtErrorList.Multiline = true;
            txtErrorList.Name = "txtErrorList";
            txtErrorList.Size = new Size(609, 172);
            txtErrorList.TabIndex = 23;
            txtErrorList.TabStop = false;
            txtErrorList.MouseMove += txtErrorList_MouseMove;
            // 
            // FrmInterlinear
            // 
            AutoScaleDimensions = new SizeF(12F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1141, 408);
            Controls.Add(txtErrorList);
            Controls.Add(TxtLineNumber);
            Controls.Add(LblError);
            Controls.Add(chkMaxLineTimes);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(txtResult);
            Controls.Add(btnCreate);
            Controls.Add(txtFilePath2);
            Controls.Add(txtFilePath1);
            Controls.Add(btnBottom);
            Controls.Add(btnTop);
            Font = new Font("Times New Roman", 13F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FrmInterlinear";
            Text = "  Tachufind";
            TopMost = true;
            FormClosing += FrmInterlinear_FormClosing;
            Load += FrmInterlinear_Load;
            LocationChanged += FrmInterlinear_LocationChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTop;
        private Button btnBottom;
        private TextBox txtFilePath1;
        private TextBox txtFilePath2;
        private Button btnCreate;
        private TextBox txtResult;
        private Button btnClose;
        private Label label1;
        private CheckBox chkMaxLineTimes;
        private Label LblError;
        private TextBox TxtLineNumber;
        private TextBox txtErrorList;
    }
}