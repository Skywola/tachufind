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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOCR));
            pictureBox1 = new PictureBox();
            CmbLanguage1 = new ComboBox();
            CmbLanguage3 = new ComboBox();
            label1 = new Label();
            SpinnerLineLength = new NumericUpDown();
            PasteIntoTop = new Button();
            BtnGetImageTop = new Button();
            BtnGetTextTop = new Button();
            btnSplit = new Button();
            btnSetLengthTop = new Button();
            BtnFullMerge = new Button();
            btnStepMerge = new Button();
            btnUndo = new Button();
            btnCache = new Button();
            BtnInsertSectionMarker = new Button();
            btnGetImageBottom = new Button();
            btnPasteIntoBottom = new Button();
            BtnGetTextBottom = new Button();
            button15 = new Button();
            BtnClearAll = new Button();
            lblTop = new Label();
            lblBottom = new Label();
            ChkTTSBrackets = new CheckBox();
            btnClearTop = new Button();
            button2 = new Button();
            RtbTextTop = new RichTextBox();
            RtbTextBottom = new RichTextBox();
            pictureBox2 = new PictureBox();
            BtnClearCache = new Button();
            BtnInsertDivMarker = new Button();
            btnAcute = new Button();
            btnGrave = new Button();
            btnDotProd = new Button();
            btnShort = new Button();
            btnMacron = new Button();
            btnCircumflex = new Button();
            btnUmlaut = new Button();
            btnSingleQuotes = new Button();
            BtnBrackets = new Button();
            BtnInsertParenthesis = new Button();
            BtnInsertFrenchQuotes = new Button();
            BtnInsertFancyQuotes = new Button();
            btnSpExclaim = new Button();
            btnSpNye = new Button();
            btnCedilla = new Button();
            btnSpQuestionMk = new Button();
            btnLongLine = new Button();
            toolTip1 = new ToolTip(components);
            btnCapDeCap = new Button();
            btnSuperscript = new Button();
            btnSubscript = new Button();
            btnArrowR = new Button();
            ChkPreProcess = new CheckBox();
            CmbLanguage2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpinnerLineLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonShadow;
            pictureBox1.Location = new Point(3, 4);
            pictureBox1.MaximumSize = new Size(745, 1118);
            pictureBox1.MinimumSize = new Size(745, 1118);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(745, 1118);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // CmbLanguage1
            // 
            CmbLanguage1.BackColor = SystemColors.ControlDark;
            CmbLanguage1.Font = new Font("Times New Roman", 12F);
            CmbLanguage1.FormattingEnabled = true;
            CmbLanguage1.Items.AddRange(new object[] { "Arabic", "Chinese Simplified", "Chinese Traditional", "English", "Farsi", "French", "German", "Greek", "Italian", "Latin", "Russian", "Spanish" });
            CmbLanguage1.Location = new Point(755, 2);
            CmbLanguage1.Name = "CmbLanguage1";
            CmbLanguage1.Size = new Size(117, 30);
            CmbLanguage1.TabIndex = 1;
            CmbLanguage1.TabStop = false;
            CmbLanguage1.Text = "Greek";
            // 
            // CmbLanguage3
            // 
            CmbLanguage3.BackColor = SystemColors.ControlDark;
            CmbLanguage3.Font = new Font("Times New Roman", 12F);
            CmbLanguage3.FormattingEnabled = true;
            CmbLanguage3.Items.AddRange(new object[] { "Arabic", "Chinese Simplified", "Chinese Traditional", "English", "Farsi", "French", "German", "Greek", "Italian", "Latin", "Russian", "Spanish" });
            CmbLanguage3.Location = new Point(755, 591);
            CmbLanguage3.Name = "CmbLanguage3";
            CmbLanguage3.Size = new Size(117, 30);
            CmbLanguage3.TabIndex = 4;
            CmbLanguage3.TabStop = false;
            CmbLanguage3.Text = "English";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 10F);
            label1.Location = new Point(755, 87);
            label1.Name = "label1";
            label1.Size = new Size(56, 38);
            label1.TabIndex = 5;
            label1.Text = "Line \r\nLength";
            // 
            // SpinnerLineLength
            // 
            SpinnerLineLength.Font = new Font("Times New Roman", 14F);
            SpinnerLineLength.Location = new Point(816, 89);
            SpinnerLineLength.Margin = new Padding(5);
            SpinnerLineLength.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            SpinnerLineLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SpinnerLineLength.Name = "SpinnerLineLength";
            SpinnerLineLength.Size = new Size(53, 34);
            SpinnerLineLength.TabIndex = 6;
            SpinnerLineLength.TabStop = false;
            SpinnerLineLength.Value = new decimal(new int[] { 70, 0, 0, 0 });
            // 
            // PasteIntoTop
            // 
            PasteIntoTop.BackColor = Color.Gold;
            PasteIntoTop.Font = new Font("Times New Roman", 12F);
            PasteIntoTop.Location = new Point(753, 127);
            PasteIntoTop.Name = "PasteIntoTop";
            PasteIntoTop.Size = new Size(122, 30);
            PasteIntoTop.TabIndex = 7;
            PasteIntoTop.TabStop = false;
            PasteIntoTop.Text = "Paste Into (For screenshot, Windows button + Shift + S))";
            PasteIntoTop.UseVisualStyleBackColor = false;
            PasteIntoTop.Click += PasteIntoTop_Click;
            // 
            // BtnGetImageTop
            // 
            BtnGetImageTop.BackColor = Color.Gold;
            BtnGetImageTop.Font = new Font("Times New Roman", 12F);
            BtnGetImageTop.Location = new Point(753, 159);
            BtnGetImageTop.Name = "BtnGetImageTop";
            BtnGetImageTop.Size = new Size(122, 30);
            BtnGetImageTop.TabIndex = 8;
            BtnGetImageTop.TabStop = false;
            BtnGetImageTop.Text = "Get Image";
            BtnGetImageTop.UseVisualStyleBackColor = false;
            BtnGetImageTop.Click += BtnGetImageTop_Click;
            // 
            // BtnGetTextTop
            // 
            BtnGetTextTop.BackColor = Color.Gold;
            BtnGetTextTop.Font = new Font("Times New Roman", 12F);
            BtnGetTextTop.Location = new Point(753, 191);
            BtnGetTextTop.Name = "BtnGetTextTop";
            BtnGetTextTop.Size = new Size(122, 30);
            BtnGetTextTop.TabIndex = 9;
            BtnGetTextTop.TabStop = false;
            BtnGetTextTop.Text = "Get Text";
            BtnGetTextTop.UseVisualStyleBackColor = false;
            BtnGetTextTop.Click += BtnGetTextTop_Click;
            // 
            // btnSplit
            // 
            btnSplit.BackColor = Color.LightSkyBlue;
            btnSplit.Font = new Font("Times New Roman", 12F);
            btnSplit.Location = new Point(753, 255);
            btnSplit.Name = "btnSplit";
            btnSplit.Size = new Size(122, 30);
            btnSplit.TabIndex = 11;
            btnSplit.TabStop = false;
            btnSplit.Text = "Split";
            btnSplit.UseVisualStyleBackColor = false;
            btnSplit.Click += BtnSplit_Click;
            // 
            // btnSetLengthTop
            // 
            btnSetLengthTop.BackColor = Color.LightSkyBlue;
            btnSetLengthTop.Font = new Font("Times New Roman", 12F);
            btnSetLengthTop.Location = new Point(753, 223);
            btnSetLengthTop.Name = "btnSetLengthTop";
            btnSetLengthTop.Size = new Size(122, 30);
            btnSetLengthTop.TabIndex = 10;
            btnSetLengthTop.TabStop = false;
            btnSetLengthTop.Text = "Set Length";
            btnSetLengthTop.UseVisualStyleBackColor = false;
            btnSetLengthTop.Click += BtnSetLengthTop_Click;
            // 
            // BtnFullMerge
            // 
            BtnFullMerge.BackColor = Color.LightSkyBlue;
            BtnFullMerge.Font = new Font("Times New Roman", 12F);
            BtnFullMerge.Location = new Point(753, 319);
            BtnFullMerge.Name = "BtnFullMerge";
            BtnFullMerge.Size = new Size(122, 30);
            BtnFullMerge.TabIndex = 13;
            BtnFullMerge.TabStop = false;
            BtnFullMerge.Text = "Full Merge";
            BtnFullMerge.UseVisualStyleBackColor = false;
            BtnFullMerge.Click += BtnFullMerge_Click;
            // 
            // btnStepMerge
            // 
            btnStepMerge.BackColor = Color.LightSkyBlue;
            btnStepMerge.Font = new Font("Times New Roman", 12F);
            btnStepMerge.Location = new Point(753, 287);
            btnStepMerge.Name = "btnStepMerge";
            btnStepMerge.Size = new Size(122, 30);
            btnStepMerge.TabIndex = 12;
            btnStepMerge.TabStop = false;
            btnStepMerge.Text = "Step Merge";
            btnStepMerge.UseVisualStyleBackColor = false;
            btnStepMerge.Click += BtnStepMerge_Click;
            // 
            // btnUndo
            // 
            btnUndo.BackColor = Color.RosyBrown;
            btnUndo.Font = new Font("Times New Roman", 12F);
            btnUndo.Location = new Point(753, 383);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(122, 30);
            btnUndo.TabIndex = 15;
            btnUndo.TabStop = false;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = false;
            btnUndo.Click += BtnUndo_Click;
            // 
            // btnCache
            // 
            btnCache.BackColor = Color.Khaki;
            btnCache.Font = new Font("Times New Roman", 12F);
            btnCache.Location = new Point(753, 351);
            btnCache.Name = "btnCache";
            btnCache.Size = new Size(122, 30);
            btnCache.TabIndex = 14;
            btnCache.TabStop = false;
            btnCache.Text = "Cache";
            btnCache.UseVisualStyleBackColor = false;
            btnCache.Click += BtnCache_Click;
            // 
            // BtnInsertSectionMarker
            // 
            BtnInsertSectionMarker.BackColor = SystemColors.HotTrack;
            BtnInsertSectionMarker.Font = new Font("Times New Roman", 12F);
            BtnInsertSectionMarker.ForeColor = SystemColors.ButtonHighlight;
            BtnInsertSectionMarker.Location = new Point(753, 447);
            BtnInsertSectionMarker.Name = "BtnInsertSectionMarker";
            BtnInsertSectionMarker.Size = new Size(122, 30);
            BtnInsertSectionMarker.TabIndex = 17;
            BtnInsertSectionMarker.TabStop = false;
            BtnInsertSectionMarker.Text = "Insert §";
            BtnInsertSectionMarker.UseVisualStyleBackColor = false;
            BtnInsertSectionMarker.Click += BtnInsertSectionMarker_Click;
            // 
            // btnGetImageBottom
            // 
            btnGetImageBottom.BackColor = Color.Gold;
            btnGetImageBottom.Font = new Font("Times New Roman", 12F);
            btnGetImageBottom.Location = new Point(753, 657);
            btnGetImageBottom.Name = "btnGetImageBottom";
            btnGetImageBottom.Size = new Size(122, 30);
            btnGetImageBottom.TabIndex = 19;
            btnGetImageBottom.TabStop = false;
            btnGetImageBottom.Text = "Get Image";
            btnGetImageBottom.UseVisualStyleBackColor = false;
            btnGetImageBottom.Click += BtnGetImageBottom_Click;
            // 
            // btnPasteIntoBottom
            // 
            btnPasteIntoBottom.BackColor = Color.Gold;
            btnPasteIntoBottom.Font = new Font("Times New Roman", 12F);
            btnPasteIntoBottom.Location = new Point(753, 624);
            btnPasteIntoBottom.Name = "btnPasteIntoBottom";
            btnPasteIntoBottom.Size = new Size(122, 30);
            btnPasteIntoBottom.TabIndex = 18;
            btnPasteIntoBottom.TabStop = false;
            btnPasteIntoBottom.Text = "Paste Into (For screenshot, Windows button + Shift + S))";
            btnPasteIntoBottom.UseVisualStyleBackColor = false;
            btnPasteIntoBottom.Click += BtnPasteIntoBottom_Click;
            // 
            // BtnGetTextBottom
            // 
            BtnGetTextBottom.BackColor = Color.Gold;
            BtnGetTextBottom.Font = new Font("Times New Roman", 12F);
            BtnGetTextBottom.Location = new Point(753, 690);
            BtnGetTextBottom.Name = "BtnGetTextBottom";
            BtnGetTextBottom.Size = new Size(122, 30);
            BtnGetTextBottom.TabIndex = 20;
            BtnGetTextBottom.TabStop = false;
            BtnGetTextBottom.Text = "Get Text";
            BtnGetTextBottom.UseVisualStyleBackColor = false;
            BtnGetTextBottom.Click += BtnGetTextBottom_Click;
            // 
            // button15
            // 
            button15.BackColor = Color.DarkRed;
            button15.Font = new Font("Times New Roman", 12F);
            button15.Location = new Point(753, 1084);
            button15.Name = "button15";
            button15.Size = new Size(122, 30);
            button15.TabIndex = 23;
            button15.TabStop = false;
            button15.Text = "Close";
            button15.UseVisualStyleBackColor = false;
            button15.Click += BtnClose_Click;
            // 
            // BtnClearAll
            // 
            BtnClearAll.BackColor = Color.Chocolate;
            BtnClearAll.Font = new Font("Times New Roman", 12F);
            BtnClearAll.Location = new Point(753, 982);
            BtnClearAll.Name = "BtnClearAll";
            BtnClearAll.Size = new Size(122, 30);
            BtnClearAll.TabIndex = 22;
            BtnClearAll.TabStop = false;
            BtnClearAll.Text = "Clear All";
            BtnClearAll.UseVisualStyleBackColor = false;
            BtnClearAll.Click += BtnClearAll_Click;
            // 
            // lblTop
            // 
            lblTop.AutoSize = true;
            lblTop.Font = new Font("Times New Roman", 11F);
            lblTop.Location = new Point(758, 512);
            lblTop.Name = "lblTop";
            lblTop.Size = new Size(50, 21);
            lblTop.TabIndex = 24;
            lblTop.Text = "Top :";
            // 
            // lblBottom
            // 
            lblBottom.AutoSize = true;
            lblBottom.Font = new Font("Times New Roman", 11F);
            lblBottom.Location = new Point(760, 538);
            lblBottom.Name = "lblBottom";
            lblBottom.Size = new Size(46, 21);
            lblBottom.TabIndex = 25;
            lblBottom.Text = "Bot :";
            // 
            // ChkTTSBrackets
            // 
            ChkTTSBrackets.AutoSize = true;
            ChkTTSBrackets.Font = new Font("Times New Roman", 12F);
            ChkTTSBrackets.Location = new Point(759, 562);
            ChkTTSBrackets.Name = "ChkTTSBrackets";
            ChkTTSBrackets.Size = new Size(101, 26);
            ChkTTSBrackets.TabIndex = 26;
            ChkTTSBrackets.TabStop = false;
            ChkTTSBrackets.Text = "TTS [   ]";
            ChkTTSBrackets.UseVisualStyleBackColor = true;
            ChkTTSBrackets.CheckedChanged += ChkTTSBrackets_CheckedChanged;
            // 
            // btnClearTop
            // 
            btnClearTop.BackColor = Color.Chocolate;
            btnClearTop.Font = new Font("Times New Roman", 12F);
            btnClearTop.Location = new Point(753, 1016);
            btnClearTop.Name = "btnClearTop";
            btnClearTop.Size = new Size(122, 30);
            btnClearTop.TabIndex = 27;
            btnClearTop.TabStop = false;
            btnClearTop.Text = "Clear Top";
            btnClearTop.UseVisualStyleBackColor = false;
            btnClearTop.Click += BtnClearTop_Click_1;
            // 
            // button2
            // 
            button2.BackColor = Color.Chocolate;
            button2.Font = new Font("Times New Roman", 11F);
            button2.Location = new Point(753, 1049);
            button2.Name = "button2";
            button2.Size = new Size(122, 31);
            button2.TabIndex = 28;
            button2.TabStop = false;
            button2.Text = "Clear Bottom";
            button2.UseVisualStyleBackColor = false;
            button2.Click += BtnClearBottom_Click;
            // 
            // RtbTextTop
            // 
            RtbTextTop.BackColor = SystemColors.ButtonShadow;
            RtbTextTop.Font = new Font("Times New Roman", 20F);
            RtbTextTop.Location = new Point(881, 4);
            RtbTextTop.MaximumSize = new Size(878, 555);
            RtbTextTop.MinimumSize = new Size(878, 555);
            RtbTextTop.Name = "RtbTextTop";
            RtbTextTop.Size = new Size(878, 555);
            RtbTextTop.TabIndex = 0;
            RtbTextTop.Text = "";
            RtbTextTop.ZoomFactor = 0.8F;
            RtbTextTop.MouseClick += RtbTextTop_MouseClick;
            RtbTextTop.Enter += RtbTextTop_Enter;
            RtbTextTop.KeyDown += RtbTextTop_KeyDown;
            RtbTextTop.MouseMove += RtbTextTop_MouseMove;
            // 
            // RtbTextBottom
            // 
            RtbTextBottom.BackColor = SystemColors.ButtonShadow;
            RtbTextBottom.Font = new Font("Times New Roman", 20F);
            RtbTextBottom.Location = new Point(881, 567);
            RtbTextBottom.MaximumSize = new Size(878, 555);
            RtbTextBottom.MinimumSize = new Size(878, 555);
            RtbTextBottom.Name = "RtbTextBottom";
            RtbTextBottom.Size = new Size(878, 555);
            RtbTextBottom.TabIndex = 1;
            RtbTextBottom.Text = "";
            RtbTextBottom.ZoomFactor = 0.8F;
            RtbTextBottom.MouseClick += RtbTextBottom_MouseClick;
            RtbTextBottom.Enter += RtbTextBottom_Enter;
            RtbTextBottom.KeyDown += RtbTextBottom_KeyDown;
            RtbTextBottom.MouseMove += RtbTextBottom_MouseMove;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ButtonShadow;
            pictureBox2.Location = new Point(1765, 4);
            pictureBox2.MaximumSize = new Size(745, 1118);
            pictureBox2.MinimumSize = new Size(745, 1118);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(745, 1118);
            pictureBox2.TabIndex = 31;
            pictureBox2.TabStop = false;
            // 
            // BtnClearCache
            // 
            BtnClearCache.BackColor = Color.Khaki;
            BtnClearCache.Font = new Font("Times New Roman", 11F);
            BtnClearCache.Location = new Point(753, 479);
            BtnClearCache.Name = "BtnClearCache";
            BtnClearCache.Size = new Size(122, 30);
            BtnClearCache.TabIndex = 35;
            BtnClearCache.TabStop = false;
            BtnClearCache.Text = "Clear Cache";
            BtnClearCache.UseVisualStyleBackColor = false;
            BtnClearCache.Click += BtnClearCache_Click;
            // 
            // BtnInsertDivMarker
            // 
            BtnInsertDivMarker.BackColor = SystemColors.HotTrack;
            BtnInsertDivMarker.Font = new Font("Times New Roman", 12F);
            BtnInsertDivMarker.ForeColor = SystemColors.ButtonHighlight;
            BtnInsertDivMarker.Location = new Point(753, 415);
            BtnInsertDivMarker.Name = "BtnInsertDivMarker";
            BtnInsertDivMarker.Size = new Size(122, 30);
            BtnInsertDivMarker.TabIndex = 36;
            BtnInsertDivMarker.TabStop = false;
            BtnInsertDivMarker.Text = "Insert ↆ";
            BtnInsertDivMarker.UseVisualStyleBackColor = false;
            BtnInsertDivMarker.Click += BtnInsertDivMarker_Click;
            // 
            // btnAcute
            // 
            btnAcute.Image = Resource.acute;
            btnAcute.Location = new Point(759, 725);
            btnAcute.Name = "btnAcute";
            btnAcute.Size = new Size(33, 33);
            btnAcute.TabIndex = 37;
            btnAcute.TabStop = false;
            toolTip1.SetToolTip(btnAcute, "              Type a vowel then click this button for an acute accent (ALT right arow)");
            btnAcute.UseVisualStyleBackColor = true;
            btnAcute.Click += btnAcute_Click;
            // 
            // btnGrave
            // 
            btnGrave.Image = Resource.grv;
            btnGrave.Location = new Point(798, 725);
            btnGrave.Name = "btnGrave";
            btnGrave.Size = new Size(33, 33);
            btnGrave.TabIndex = 38;
            btnGrave.TabStop = false;
            toolTip1.SetToolTip(btnGrave, "              Type a vowel then click this button for a grave accent (ALT left arrow)");
            btnGrave.UseVisualStyleBackColor = true;
            btnGrave.Click += btnGrave_Click;
            // 
            // btnDotProd
            // 
            btnDotProd.Image = Resource.DotProd;
            btnDotProd.Location = new Point(836, 725);
            btnDotProd.Name = "btnDotProd";
            btnDotProd.Size = new Size(33, 33);
            btnDotProd.TabIndex = 42;
            btnDotProd.TabStop = false;
            btnDotProd.UseVisualStyleBackColor = true;
            btnDotProd.Click += btnDotProd_Click;
            // 
            // btnShort
            // 
            btnShort.Image = Resource.sm;
            btnShort.Location = new Point(798, 833);
            btnShort.Name = "btnShort";
            btnShort.Size = new Size(33, 33);
            btnShort.TabIndex = 44;
            btnShort.TabStop = false;
            btnShort.Text = "              Type a vowel then click this button (ALT =)";
            btnShort.UseVisualStyleBackColor = true;
            btnShort.Click += btnShort_Click;
            // 
            // btnMacron
            // 
            btnMacron.Image = Resource.mac;
            btnMacron.Location = new Point(759, 833);
            btnMacron.Name = "btnMacron";
            btnMacron.Size = new Size(33, 33);
            btnMacron.TabIndex = 43;
            btnMacron.TabStop = false;
            toolTip1.SetToolTip(btnMacron, "              Type a vowel then click this button (ALT -)");
            btnMacron.UseVisualStyleBackColor = true;
            btnMacron.Click += btnMacron_Click;
            // 
            // btnCircumflex
            // 
            btnCircumflex.Image = Resource.ciralt;
            btnCircumflex.Location = new Point(759, 761);
            btnCircumflex.Name = "btnCircumflex";
            btnCircumflex.Size = new Size(33, 33);
            btnCircumflex.TabIndex = 46;
            btnCircumflex.TabStop = false;
            btnCircumflex.Text = "              Type a vowel then click this button for a circumflex (ALT-END)";
            btnCircumflex.UseVisualStyleBackColor = true;
            btnCircumflex.Click += btnCircumflex_Click;
            // 
            // btnUmlaut
            // 
            btnUmlaut.Image = Resource.um;
            btnUmlaut.Location = new Point(759, 870);
            btnUmlaut.Name = "btnUmlaut";
            btnUmlaut.Size = new Size(33, 33);
            btnUmlaut.TabIndex = 45;
            btnUmlaut.TabStop = false;
            toolTip1.SetToolTip(btnUmlaut, "              Type a vowel then click this button for an umlaut");
            btnUmlaut.UseVisualStyleBackColor = true;
            btnUmlaut.Click += btnUmlaut_Click;
            // 
            // btnSingleQuotes
            // 
            btnSingleQuotes.Image = Resource.ApostAltr;
            btnSingleQuotes.Location = new Point(836, 761);
            btnSingleQuotes.Name = "btnSingleQuotes";
            btnSingleQuotes.Size = new Size(33, 33);
            btnSingleQuotes.TabIndex = 47;
            btnSingleQuotes.TabStop = false;
            btnSingleQuotes.UseVisualStyleBackColor = true;
            btnSingleQuotes.Click += btnSingleQuotes_Click;
            // 
            // BtnBrackets
            // 
            BtnBrackets.Font = new Font("Times New Roman", 15F);
            BtnBrackets.Image = Resource.Brakets2;
            BtnBrackets.Location = new Point(759, 797);
            BtnBrackets.Name = "BtnBrackets";
            BtnBrackets.Size = new Size(33, 33);
            BtnBrackets.TabIndex = 51;
            BtnBrackets.TabStop = false;
            BtnBrackets.UseVisualStyleBackColor = true;
            BtnBrackets.Click += BtnBrackets_Click;
            // 
            // BtnInsertParenthesis
            // 
            BtnInsertParenthesis.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnInsertParenthesis.Image = Resource.Parenthesis1;
            BtnInsertParenthesis.Location = new Point(798, 797);
            BtnInsertParenthesis.Name = "BtnInsertParenthesis";
            BtnInsertParenthesis.Size = new Size(33, 33);
            BtnInsertParenthesis.TabIndex = 50;
            BtnInsertParenthesis.TabStop = false;
            BtnInsertParenthesis.UseVisualStyleBackColor = true;
            BtnInsertParenthesis.Click += BtnInsertParenthesis_Click;
            // 
            // BtnInsertFrenchQuotes
            // 
            BtnInsertFrenchQuotes.Image = Resource.QuoteFrench;
            BtnInsertFrenchQuotes.Location = new Point(836, 797);
            BtnInsertFrenchQuotes.Name = "BtnInsertFrenchQuotes";
            BtnInsertFrenchQuotes.Size = new Size(33, 33);
            BtnInsertFrenchQuotes.TabIndex = 49;
            BtnInsertFrenchQuotes.TabStop = false;
            BtnInsertFrenchQuotes.UseVisualStyleBackColor = true;
            BtnInsertFrenchQuotes.Click += BtnInsertFrenchQuotes_Click;
            // 
            // BtnInsertFancyQuotes
            // 
            BtnInsertFancyQuotes.Image = Resource.QuoteAlt;
            BtnInsertFancyQuotes.Location = new Point(798, 761);
            BtnInsertFancyQuotes.Name = "BtnInsertFancyQuotes";
            BtnInsertFancyQuotes.Size = new Size(33, 33);
            BtnInsertFancyQuotes.TabIndex = 48;
            BtnInsertFancyQuotes.TabStop = false;
            BtnInsertFancyQuotes.UseVisualStyleBackColor = true;
            BtnInsertFancyQuotes.Click += BtnInsertFancyQuotes_Click;
            // 
            // btnSpExclaim
            // 
            btnSpExclaim.Image = Resource.Spex5;
            btnSpExclaim.Location = new Point(799, 945);
            btnSpExclaim.Name = "btnSpExclaim";
            btnSpExclaim.Size = new Size(33, 33);
            btnSpExclaim.TabIndex = 55;
            btnSpExclaim.TabStop = false;
            btnSpExclaim.UseVisualStyleBackColor = true;
            btnSpExclaim.Click += btnSpExclaim_Click;
            // 
            // btnSpNye
            // 
            btnSpNye.Location = new Point(760, 945);
            btnSpNye.Name = "btnSpNye";
            btnSpNye.Size = new Size(33, 33);
            btnSpNye.TabIndex = 54;
            btnSpNye.TabStop = false;
            btnSpNye.Text = "ñ";
            btnSpNye.UseVisualStyleBackColor = true;
            btnSpNye.Click += btnSpNye_Click;
            // 
            // btnCedilla
            // 
            btnCedilla.Image = Resource.Frced;
            btnCedilla.Location = new Point(760, 907);
            btnCedilla.Name = "btnCedilla";
            btnCedilla.Size = new Size(33, 33);
            btnCedilla.TabIndex = 53;
            btnCedilla.TabStop = false;
            btnCedilla.UseVisualStyleBackColor = true;
            btnCedilla.Click += btnCedilla_Click;
            // 
            // btnSpQuestionMk
            // 
            btnSpQuestionMk.Image = Resource.Spqu;
            btnSpQuestionMk.Location = new Point(836, 945);
            btnSpQuestionMk.Name = "btnSpQuestionMk";
            btnSpQuestionMk.Size = new Size(33, 33);
            btnSpQuestionMk.TabIndex = 52;
            btnSpQuestionMk.TabStop = false;
            btnSpQuestionMk.UseVisualStyleBackColor = true;
            btnSpQuestionMk.Click += btnSpQuestionMk_Click;
            // 
            // btnLongLine
            // 
            btnLongLine.Location = new Point(836, 870);
            btnLongLine.Name = "btnLongLine";
            btnLongLine.Size = new Size(33, 33);
            btnLongLine.TabIndex = 56;
            btnLongLine.TabStop = false;
            btnLongLine.Text = "—";
            btnLongLine.UseVisualStyleBackColor = true;
            btnLongLine.Click += btnLongLine_Click;
            // 
            // btnCapDeCap
            // 
            btnCapDeCap.Image = Resource.caplc;
            btnCapDeCap.Location = new Point(836, 833);
            btnCapDeCap.Name = "btnCapDeCap";
            btnCapDeCap.Size = new Size(33, 33);
            btnCapDeCap.TabIndex = 58;
            btnCapDeCap.TabStop = false;
            toolTip1.SetToolTip(btnCapDeCap, "              Capitalize / de-capitalize selected text.");
            btnCapDeCap.UseVisualStyleBackColor = true;
            // 
            // btnSuperscript
            // 
            btnSuperscript.Image = Resource.super;
            btnSuperscript.Location = new Point(836, 907);
            btnSuperscript.Name = "btnSuperscript";
            btnSuperscript.Size = new Size(33, 33);
            btnSuperscript.TabIndex = 61;
            toolTip1.SetToolTip(btnSuperscript, "              Select character(s), then click for SuperScript");
            btnSuperscript.UseVisualStyleBackColor = true;
            // 
            // btnSubscript
            // 
            btnSubscript.Image = Resource.sub;
            btnSubscript.Location = new Point(798, 907);
            btnSubscript.Name = "btnSubscript";
            btnSubscript.Size = new Size(33, 33);
            btnSubscript.TabIndex = 60;
            toolTip1.SetToolTip(btnSubscript, "              Select character(s), then shift click for SubScript");
            btnSubscript.UseVisualStyleBackColor = true;
            // 
            // btnArrowR
            // 
            btnArrowR.Location = new Point(798, 870);
            btnArrowR.Name = "btnArrowR";
            btnArrowR.Size = new Size(33, 33);
            btnArrowR.TabIndex = 59;
            btnArrowR.Text = "→";
            toolTip1.SetToolTip(btnArrowR, "              Insert Character");
            btnArrowR.UseVisualStyleBackColor = true;
            // 
            // ChkPreProcess
            // 
            ChkPreProcess.AutoSize = true;
            ChkPreProcess.Checked = true;
            ChkPreProcess.CheckState = CheckState.Checked;
            ChkPreProcess.Font = new Font("Times New Roman", 10F);
            ChkPreProcess.Location = new Point(757, 64);
            ChkPreProcess.Name = "ChkPreProcess";
            ChkPreProcess.Size = new Size(108, 23);
            ChkPreProcess.TabIndex = 57;
            ChkPreProcess.Text = "Preprocess";
            ChkPreProcess.UseVisualStyleBackColor = true;
            // 
            // CmbLanguage2
            // 
            CmbLanguage2.BackColor = SystemColors.ControlDark;
            CmbLanguage2.Font = new Font("Times New Roman", 12F);
            CmbLanguage2.FormattingEnabled = true;
            CmbLanguage2.Items.AddRange(new object[] { "None", "Arabic", "Chinese Simplified", "Chinese Traditional", "English", "Farsi", "French", "German", "Greek", "Italian", "Latin", "Russian", "Spanish" });
            CmbLanguage2.Location = new Point(755, 33);
            CmbLanguage2.Name = "CmbLanguage2";
            CmbLanguage2.Size = new Size(117, 30);
            CmbLanguage2.TabIndex = 62;
            CmbLanguage2.TabStop = false;
            CmbLanguage2.Text = "None";
            // 
            // FrmOCR
            // 
            AutoScaleDimensions = new SizeF(15F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RoyalBlue;
            ClientSize = new Size(2498, 1119);
            Controls.Add(CmbLanguage2);
            Controls.Add(btnSuperscript);
            Controls.Add(btnSubscript);
            Controls.Add(btnArrowR);
            Controls.Add(btnCapDeCap);
            Controls.Add(ChkPreProcess);
            Controls.Add(btnLongLine);
            Controls.Add(btnSpExclaim);
            Controls.Add(btnSpNye);
            Controls.Add(btnCedilla);
            Controls.Add(btnSpQuestionMk);
            Controls.Add(BtnBrackets);
            Controls.Add(BtnInsertParenthesis);
            Controls.Add(BtnInsertFrenchQuotes);
            Controls.Add(BtnInsertFancyQuotes);
            Controls.Add(btnSingleQuotes);
            Controls.Add(btnCircumflex);
            Controls.Add(btnUmlaut);
            Controls.Add(btnShort);
            Controls.Add(btnMacron);
            Controls.Add(btnDotProd);
            Controls.Add(btnGrave);
            Controls.Add(btnAcute);
            Controls.Add(BtnInsertDivMarker);
            Controls.Add(BtnClearCache);
            Controls.Add(pictureBox2);
            Controls.Add(RtbTextBottom);
            Controls.Add(RtbTextTop);
            Controls.Add(button2);
            Controls.Add(btnClearTop);
            Controls.Add(ChkTTSBrackets);
            Controls.Add(lblBottom);
            Controls.Add(lblTop);
            Controls.Add(button15);
            Controls.Add(BtnClearAll);
            Controls.Add(BtnGetTextBottom);
            Controls.Add(btnGetImageBottom);
            Controls.Add(btnPasteIntoBottom);
            Controls.Add(BtnInsertSectionMarker);
            Controls.Add(btnUndo);
            Controls.Add(btnCache);
            Controls.Add(BtnFullMerge);
            Controls.Add(btnStepMerge);
            Controls.Add(btnSplit);
            Controls.Add(btnSetLengthTop);
            Controls.Add(BtnGetTextTop);
            Controls.Add(BtnGetImageTop);
            Controls.Add(PasteIntoTop);
            Controls.Add(SpinnerLineLength);
            Controls.Add(label1);
            Controls.Add(CmbLanguage3);
            Controls.Add(CmbLanguage1);
            Controls.Add(pictureBox1);
            Font = new Font("Times New Roman", 16F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            MaximumSize = new Size(2516, 1166);
            MinimumSize = new Size(2516, 1166);
            Name = "FrmOCR";
            Text = "  Tachufind";
            Activated += FrmOCR_Activated;
            FormClosing += FrmOCR_FormClosing;
            Load += FrmOCR_Load;
            LocationChanged += FrmOCR_LocationChanged;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpinnerLineLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private ComboBox CmbLanguage1;
        private ComboBox CmbLanguage3;
        private Label label1;
        private NumericUpDown SpinnerLineLength;
        private Button PasteIntoTop;
        private Button BtnGetImageTop;
        private Button BtnGetTextTop;
        private Button btnSplit;
        private Button btnSetLengthTop;
        private Button BtnFullMerge;
        private Button btnStepMerge;
        private Button btnUndo;
        private Button btnCache;
        private Button BtnInsertSectionMarker;
        private Button btnGetImageBottom;
        private Button btnPasteIntoBottom;
        private Button BtnGetTextBottom;
        private Button button15;
        private Button BtnClearAll;
        private Label lblTop;
        private Label lblBottom;
        private CheckBox ChkTTSBrackets;
        private Button btnClearTop;
        private Button button2;
        private RichTextBox RtbTextTop;
        private RichTextBox RtbTextBottom;
        private PictureBox pictureBox2;
        private Button BtnClearCache;
        private Button BtnInsertDivMarker;
        private Button btnAcute;
        private Button btnGrave;
        private Button btnDotProd;
        private Button btnShort;
        private Button btnMacron;
        private Button btnCircumflex;
        private Button btnUmlaut;
        private Button btnSingleQuotes;
        private Button BtnBrackets;
        private Button BtnInsertParenthesis;
        private Button BtnInsertFrenchQuotes;
        private Button BtnInsertFancyQuotes;
        private Button btnSpExclaim;
        private Button btnSpNye;
        private Button btnCedilla;
        private Button btnSpQuestionMk;
        private Button btnLongLine;
        private ToolTip toolTip1;
        private CheckBox ChkPreProcess;
        private Button btnCapDeCap;
        private Button btnSuperscript;
        private Button btnSubscript;
        private Button btnArrowR;
        private ComboBox CmbLanguage2;
    }
}