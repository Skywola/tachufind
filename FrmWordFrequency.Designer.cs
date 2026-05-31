namespace Tachufind
{
    partial class FrmWordFrequency
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
            RTBWordFreq = new RichTextBox();
            RTBWordDefinition = new RichTextBox();
            BtnSave = new Button();
            BtnClear = new Button();
            BtnCreateAudio = new Button();
            LblDictCount = new Label();
            progressBarAudio = new ProgressBar();
            CmbWFVoice1 = new ComboBox();
            CmbWFVoice2 = new ComboBox();
            NumVolume1 = new NumericUpDown();
            LblVolume1 = new Label();
            LblVolume2 = new Label();
            NumVolume2 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            NumRate1 = new NumericUpDown();
            NumRate2 = new NumericUpDown();
            RTBDictionaryWord = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)NumVolume1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumVolume2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumRate1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumRate2).BeginInit();
            SuspendLayout();
            // 
            // RTBWordFreq
            // 
            RTBWordFreq.BackColor = Color.Silver;
            RTBWordFreq.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBWordFreq.Location = new Point(7, 50);
            RTBWordFreq.Margin = new Padding(3, 4, 3, 4);
            RTBWordFreq.Name = "RTBWordFreq";
            RTBWordFreq.Size = new Size(549, 764);
            RTBWordFreq.TabIndex = 0;
            RTBWordFreq.TabStop = false;
            RTBWordFreq.Text = "";
            RTBWordFreq.MouseDoubleClick += RTBWordFreq_MouseDoubleClick;
            RTBWordFreq.MouseDown += RTBWordFreq_MouseDown;
            // 
            // RTBWordDefinition
            // 
            RTBWordDefinition.Anchor = AnchorStyles.None;
            RTBWordDefinition.BackColor = Color.Silver;
            RTBWordDefinition.Font = new Font("Times New Roman", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBWordDefinition.Location = new Point(593, 432);
            RTBWordDefinition.Margin = new Padding(3, 4, 3, 4);
            RTBWordDefinition.Name = "RTBWordDefinition";
            RTBWordDefinition.Size = new Size(1187, 382);
            RTBWordDefinition.TabIndex = 2;
            RTBWordDefinition.Text = "";
            RTBWordDefinition.Enter += RTBWordDefinition_Enter;
            // 
            // BtnSave
            // 
            BtnSave.BackColor = Color.Silver;
            BtnSave.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSave.Location = new Point(593, 53);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(203, 52);
            BtnSave.TabIndex = 0;
            BtnSave.TabStop = false;
            BtnSave.Text = "Add Entry";
            BtnSave.UseVisualStyleBackColor = false;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnClear
            // 
            BtnClear.BackColor = Color.Silver;
            BtnClear.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnClear.Location = new Point(815, 53);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(159, 52);
            BtnClear.TabIndex = 0;
            BtnClear.TabStop = false;
            BtnClear.Text = "Clear";
            BtnClear.UseVisualStyleBackColor = false;
            BtnClear.Click += BtnClear_Click;
            // 
            // BtnCreateAudio
            // 
            BtnCreateAudio.BackColor = Color.Silver;
            BtnCreateAudio.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnCreateAudio.Location = new Point(994, 53);
            BtnCreateAudio.Name = "BtnCreateAudio";
            BtnCreateAudio.Size = new Size(189, 52);
            BtnCreateAudio.TabIndex = 0;
            BtnCreateAudio.TabStop = false;
            BtnCreateAudio.Text = "Create Audio";
            BtnCreateAudio.UseVisualStyleBackColor = false;
            BtnCreateAudio.Click += BtnCreateAudio_Click;
            // 
            // LblDictCount
            // 
            LblDictCount.BackColor = Color.Black;
            LblDictCount.BorderStyle = BorderStyle.Fixed3D;
            LblDictCount.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblDictCount.ForeColor = Color.LawnGreen;
            LblDictCount.Location = new Point(593, 3);
            LblDictCount.Name = "LblDictCount";
            LblDictCount.Size = new Size(153, 40);
            LblDictCount.TabIndex = 6;
            LblDictCount.Text = "0";
            LblDictCount.TextAlign = ContentAlignment.TopCenter;
            // 
            // progressBarAudio
            // 
            progressBarAudio.Location = new Point(12, 9);
            progressBarAudio.Name = "progressBarAudio";
            progressBarAudio.Size = new Size(544, 29);
            progressBarAudio.TabIndex = 0;
            // 
            // CmbWFVoice1
            // 
            CmbWFVoice1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CmbWFVoice1.FormattingEnabled = true;
            CmbWFVoice1.Location = new Point(593, 119);
            CmbWFVoice1.Name = "CmbWFVoice1";
            CmbWFVoice1.Size = new Size(336, 34);
            CmbWFVoice1.TabIndex = 0;
            CmbWFVoice1.TabStop = false;
            CmbWFVoice1.SelectedIndexChanged += CmbWFVoice1_SelectedIndexChanged;
            // 
            // CmbWFVoice2
            // 
            CmbWFVoice2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CmbWFVoice2.FormattingEnabled = true;
            CmbWFVoice2.Location = new Point(593, 385);
            CmbWFVoice2.Name = "CmbWFVoice2";
            CmbWFVoice2.Size = new Size(336, 34);
            CmbWFVoice2.TabIndex = 9;
            CmbWFVoice2.TabStop = false;
            CmbWFVoice2.SelectedIndexChanged += CmbWFVoice2_SelectedIndexChanged;
            // 
            // NumVolume1
            // 
            NumVolume1.BackColor = SystemColors.MenuText;
            NumVolume1.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NumVolume1.ForeColor = Color.Cyan;
            NumVolume1.Location = new Point(1053, 117);
            NumVolume1.Name = "NumVolume1";
            NumVolume1.Size = new Size(78, 36);
            NumVolume1.TabIndex = 0;
            NumVolume1.TabStop = false;
            NumVolume1.TextAlign = HorizontalAlignment.Center;
            NumVolume1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // LblVolume1
            // 
            LblVolume1.AutoSize = true;
            LblVolume1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblVolume1.Location = new Point(944, 118);
            LblVolume1.Name = "LblVolume1";
            LblVolume1.Size = new Size(103, 32);
            LblVolume1.TabIndex = 0;
            LblVolume1.Text = "Volume";
            // 
            // LblVolume2
            // 
            LblVolume2.AutoSize = true;
            LblVolume2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblVolume2.Location = new Point(944, 388);
            LblVolume2.Name = "LblVolume2";
            LblVolume2.Size = new Size(103, 32);
            LblVolume2.TabIndex = 0;
            LblVolume2.Text = "Volume";
            // 
            // NumVolume2
            // 
            NumVolume2.BackColor = SystemColors.MenuText;
            NumVolume2.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NumVolume2.ForeColor = Color.Cyan;
            NumVolume2.Location = new Point(1053, 387);
            NumVolume2.Name = "NumVolume2";
            NumVolume2.Size = new Size(78, 36);
            NumVolume2.TabIndex = 0;
            NumVolume2.TabStop = false;
            NumVolume2.TextAlign = HorizontalAlignment.Center;
            NumVolume2.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(1154, 388);
            label1.Name = "label1";
            label1.Size = new Size(69, 32);
            label1.TabIndex = 0;
            label1.Text = "Rate";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(1154, 121);
            label2.Name = "label2";
            label2.Size = new Size(69, 32);
            label2.TabIndex = 0;
            label2.Text = "Rate";
            // 
            // NumRate1
            // 
            NumRate1.BackColor = SystemColors.MenuText;
            NumRate1.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NumRate1.ForeColor = Color.Cyan;
            NumRate1.Location = new Point(1229, 119);
            NumRate1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumRate1.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            NumRate1.Name = "NumRate1";
            NumRate1.Size = new Size(78, 36);
            NumRate1.TabIndex = 0;
            NumRate1.TabStop = false;
            NumRate1.TextAlign = HorizontalAlignment.Center;
            NumRate1.Value = new decimal(new int[] { 2, 0, 0, int.MinValue });
            // 
            // NumRate2
            // 
            NumRate2.BackColor = SystemColors.MenuText;
            NumRate2.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NumRate2.ForeColor = Color.Cyan;
            NumRate2.Location = new Point(1229, 385);
            NumRate2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumRate2.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            NumRate2.Name = "NumRate2";
            NumRate2.Size = new Size(78, 36);
            NumRate2.TabIndex = 0;
            NumRate2.TabStop = false;
            NumRate2.TextAlign = HorizontalAlignment.Center;
            // 
            // RTBDictionaryWord
            // 
            RTBDictionaryWord.Anchor = AnchorStyles.None;
            RTBDictionaryWord.BackColor = Color.Silver;
            RTBDictionaryWord.Font = new Font("Times New Roman", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBDictionaryWord.Location = new Point(593, 164);
            RTBDictionaryWord.Margin = new Padding(3, 4, 3, 4);
            RTBDictionaryWord.Name = "RTBDictionaryWord";
            RTBDictionaryWord.Size = new Size(1184, 210);
            RTBDictionaryWord.TabIndex = 1;
            RTBDictionaryWord.Text = "";
            RTBDictionaryWord.Enter += RTBDictionaryWord_Enter;
            // 
            // FrmWordFrequency
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(1795, 833);
            Controls.Add(RTBDictionaryWord);
            Controls.Add(NumRate2);
            Controls.Add(NumRate1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LblVolume2);
            Controls.Add(NumVolume2);
            Controls.Add(LblVolume1);
            Controls.Add(NumVolume1);
            Controls.Add(CmbWFVoice2);
            Controls.Add(CmbWFVoice1);
            Controls.Add(progressBarAudio);
            Controls.Add(LblDictCount);
            Controls.Add(BtnCreateAudio);
            Controls.Add(BtnClear);
            Controls.Add(BtnSave);
            Controls.Add(RTBWordDefinition);
            Controls.Add(RTBWordFreq);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmWordFrequency";
            Text = "  Tachufind Word Frequencies";
            FormClosing += FrmWordFrequency_FormClosing;
            Load += FrmWordFrequency_Load;
            ((System.ComponentModel.ISupportInitialize)NumVolume1).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumVolume2).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumRate1).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumRate2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal RichTextBox RTBWordFreq;
        internal RichTextBox RTBWordDefinition;
        private Button BtnSave;
        private Button BtnClear;
        private Button BtnCreateAudio;
        private Label LblDictCount;
        private ProgressBar progressBarAudio;
        private ComboBox CmbWFVoice1;
        private ComboBox CmbWFVoice2;
        private NumericUpDown NumVolume1;
        private Label LblVolume1;
        private Label LblVolume2;
        private NumericUpDown NumVolume2;
        private Label label1;
        private Label label2;
        private NumericUpDown NumRate1;
        private NumericUpDown NumRate2;
        internal RichTextBox RTBDictionaryWord;
    }
}