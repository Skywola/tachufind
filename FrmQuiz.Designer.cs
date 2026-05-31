namespace Tachufind
{
    partial class FrmQuiz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuiz));
            FontSizeComboBox = new ComboBox();
            FontComboBox = new ComboBox();
            panel2 = new Panel();
            BtnSection = new Button();
            btnCapDeCap = new Button();
            btnItalic = new Button();
            btnBold = new Button();
            btnUnderline = new Button();
            btnImageInsert = new Button();
            panel3 = new Panel();
            btnRemHighlight = new Button();
            colorBtn4 = new Button();
            colorBtn9 = new Button();
            colorBtn5 = new Button();
            colorBtn10 = new Button();
            colorBtn2 = new Button();
            colorBtn7 = new Button();
            colorBtn3 = new Button();
            colorBtn8 = new Button();
            FrmQuizBtnBlack = new Button();
            FrmMainBtnWhite = new Button();
            colorBtn1 = new Button();
            colorBtn6 = new Button();
            panel4 = new Panel();
            btnEuler = new Button();
            btnElement = new Button();
            btnDifferential = new Button();
            btnPartialDifferential = new Button();
            btnComplex = new Button();
            btnCopyright = new Button();
            btnSqrt = new Button();
            btnOr = new Button();
            btnIntegral = new Button();
            btnAnd = new Button();
            btnFrenchLowerCase_ae = new Button();
            btnSuperscript = new Button();
            btnFrenchLowerCase_oe = new Button();
            btnFrall = new Button();
            btnSubscript = new Button();
            btnReals = new Button();
            btnApprox = new Button();
            btnUnion = new Button();
            btnPlusMinus = new Button();
            btnIntersect = new Button();
            btnDegree = new Button();
            btnDotR = new Button();
            btnSpExclaim = new Button();
            btnAngle = new Button();
            btnLongLine = new Button();
            btnInfinity = new Button();
            btnArrowR = new Button();
            btnIdenticalto = new Button();
            btnSpNye = new Button();
            btnGte = new Button();
            btnCedilla = new Button();
            btnLte = new Button();
            btnNaturals = new Button();
            btnTsus = new Button();
            btnSpQuestionMk = new Button();
            BtnBrackets = new Button();
            BtnInsertParenthesis = new Button();
            btnIntegers = new Button();
            btnDotProd = new Button();
            btnSingleQuotes = new Button();
            BtnInsertFrenchQuotes = new Button();
            BtnInsertFancyQuotes = new Button();
            btnCircumflex = new Button();
            btnShort = new Button();
            btnUmlaut = new Button();
            btnAcute = new Button();
            btnGrave = new Button();
            btnMacron = new Button();
            RTBQuestion = new RichTextBox();
            RTBAnswer = new RichTextBox();
            btnInvert = new Button();
            ControlPanel = new Panel();
            BtnQuizOpenFolder = new Button();
            chkAddClear = new CheckBox();
            chkShuffle = new CheckBox();
            ChkEnforceFontSize = new CheckBox();
            btnRevert = new Button();
            BtnClearBottom = new Button();
            BtnClearTop = new Button();
            btnDeleteQA = new Button();
            btnClearTitle = new Button();
            RTBQuizName = new RichTextBox();
            txtTTLQRemaining = new TextBox();
            txtTTLQuestions = new TextBox();
            lblRemainders = new Label();
            lblTotalQuestions = new Label();
            btnNewQuiz = new Button();
            btnGetQuiz = new Button();
            btnSaveEdit = new Button();
            btnNext = new Button();
            btnAddQuestion = new Button();
            btnClearAll = new Button();
            btnAppend = new Button();
            label1 = new Label();
            lblDebuggingTest = new Label();
            fontDialog1 = new FontDialog();
            toolTip1 = new ToolTip(components);
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ControlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // FontSizeComboBox
            // 
            FontSizeComboBox.BackColor = Color.DarkGray;
            FontSizeComboBox.Font = new Font("Times New Roman", 11F);
            FontSizeComboBox.FormattingEnabled = true;
            FontSizeComboBox.Items.AddRange(new object[] { "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "32", "36", "48", "54", "60", "66", "72", "80", "88", "96" });
            FontSizeComboBox.Location = new Point(159, 3);
            FontSizeComboBox.Name = "FontSizeComboBox";
            FontSizeComboBox.Size = new Size(52, 28);
            FontSizeComboBox.TabIndex = 6;
            FontSizeComboBox.TabStop = false;
            FontSizeComboBox.Text = "22";
            FontSizeComboBox.SelectedIndexChanged += FontComboBox_SelectedIndexChanged;
            // 
            // FontComboBox
            // 
            FontComboBox.BackColor = Color.Silver;
            FontComboBox.Font = new Font("Times New Roman", 12F);
            FontComboBox.FormattingEnabled = true;
            FontComboBox.Location = new Point(4, 3);
            FontComboBox.Name = "FontComboBox";
            FontComboBox.Size = new Size(148, 30);
            FontComboBox.TabIndex = 5;
            FontComboBox.TabStop = false;
            FontComboBox.Text = "Times New Roman";
            FontComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Gray;
            panel2.Controls.Add(BtnSection);
            panel2.Controls.Add(btnCapDeCap);
            panel2.Controls.Add(btnItalic);
            panel2.Controls.Add(btnBold);
            panel2.Controls.Add(btnUnderline);
            panel2.Location = new Point(37, 35);
            panel2.Name = "panel2";
            panel2.Size = new Size(173, 35);
            panel2.TabIndex = 46;
            // 
            // BtnSection
            // 
            BtnSection.Image = Resource.sec;
            BtnSection.Location = new Point(135, 1);
            BtnSection.Name = "BtnSection";
            BtnSection.Size = new Size(33, 33);
            BtnSection.TabIndex = 52;
            BtnSection.TabStop = false;
            toolTip1.SetToolTip(BtnSection, "Insert Section Symbol");
            BtnSection.UseVisualStyleBackColor = true;
            BtnSection.Click += BtnSection_Click;
            // 
            // btnCapDeCap
            // 
            btnCapDeCap.Image = Resource.caplc;
            btnCapDeCap.Location = new Point(102, 1);
            btnCapDeCap.Name = "btnCapDeCap";
            btnCapDeCap.Size = new Size(33, 33);
            btnCapDeCap.TabIndex = 50;
            btnCapDeCap.TabStop = false;
            toolTip1.SetToolTip(btnCapDeCap, "      Capitalise - De-Capitalise selected text");
            btnCapDeCap.UseVisualStyleBackColor = true;
            btnCapDeCap.Click += BtnCapDeCap_Click;
            // 
            // btnItalic
            // 
            btnItalic.Image = Resource.ital;
            btnItalic.Location = new Point(36, 1);
            btnItalic.Name = "btnItalic";
            btnItalic.Size = new Size(33, 33);
            btnItalic.TabIndex = 47;
            btnItalic.TabStop = false;
            btnItalic.UseVisualStyleBackColor = true;
            btnItalic.Click += BtnItalic_Click;
            // 
            // btnBold
            // 
            btnBold.Image = Resource.bold;
            btnBold.Location = new Point(3, 1);
            btnBold.Name = "btnBold";
            btnBold.Size = new Size(33, 33);
            btnBold.TabIndex = 49;
            btnBold.TabStop = false;
            btnBold.UseVisualStyleBackColor = true;
            btnBold.Click += BtnBold_Click;
            // 
            // btnUnderline
            // 
            btnUnderline.Image = Resource.ul;
            btnUnderline.Location = new Point(69, 1);
            btnUnderline.Name = "btnUnderline";
            btnUnderline.Size = new Size(33, 33);
            btnUnderline.TabIndex = 48;
            btnUnderline.TabStop = false;
            btnUnderline.UseVisualStyleBackColor = true;
            btnUnderline.Click += BtnUnderline_Click;
            // 
            // btnImageInsert
            // 
            btnImageInsert.Image = Resource.grapic;
            btnImageInsert.Location = new Point(201, 33);
            btnImageInsert.Name = "btnImageInsert";
            btnImageInsert.Size = new Size(33, 33);
            btnImageInsert.TabIndex = 51;
            btnImageInsert.TabStop = false;
            toolTip1.SetToolTip(btnImageInsert, "Insert Image");
            btnImageInsert.UseVisualStyleBackColor = true;
            btnImageInsert.Click += BtnImageInsert_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gray;
            panel3.Controls.Add(btnRemHighlight);
            panel3.Controls.Add(btnImageInsert);
            panel3.Controls.Add(colorBtn4);
            panel3.Controls.Add(colorBtn9);
            panel3.Controls.Add(colorBtn5);
            panel3.Controls.Add(colorBtn10);
            panel3.Controls.Add(colorBtn2);
            panel3.Controls.Add(colorBtn7);
            panel3.Controls.Add(colorBtn3);
            panel3.Controls.Add(colorBtn8);
            panel3.Controls.Add(FrmQuizBtnBlack);
            panel3.Controls.Add(FrmMainBtnWhite);
            panel3.Controls.Add(colorBtn1);
            panel3.Controls.Add(colorBtn6);
            panel3.Location = new Point(218, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(236, 68);
            panel3.TabIndex = 52;
            // 
            // btnRemHighlight
            // 
            btnRemHighlight.Image = Resource.Highlight;
            btnRemHighlight.Location = new Point(201, 0);
            btnRemHighlight.Name = "btnRemHighlight";
            btnRemHighlight.Size = new Size(33, 33);
            btnRemHighlight.TabIndex = 38;
            btnRemHighlight.TabStop = false;
            toolTip1.SetToolTip(btnRemHighlight, "Remove All Highlighting");
            btnRemHighlight.UseVisualStyleBackColor = true;
            btnRemHighlight.Click += BtnRemHighlight_Click;
            // 
            // colorBtn4
            // 
            colorBtn4.Location = new Point(135, 0);
            colorBtn4.Name = "colorBtn4";
            colorBtn4.Size = new Size(33, 33);
            colorBtn4.TabIndex = 36;
            colorBtn4.TabStop = false;
            colorBtn4.UseVisualStyleBackColor = true;
            colorBtn4.Click += ColorBtn4_Click;
            // 
            // colorBtn9
            // 
            colorBtn9.Location = new Point(135, 33);
            colorBtn9.Name = "colorBtn9";
            colorBtn9.Size = new Size(33, 33);
            colorBtn9.TabIndex = 35;
            colorBtn9.TabStop = false;
            colorBtn9.UseVisualStyleBackColor = true;
            colorBtn9.Click += ColorBtn9_Click;
            // 
            // colorBtn5
            // 
            colorBtn5.Location = new Point(168, 0);
            colorBtn5.Name = "colorBtn5";
            colorBtn5.Size = new Size(33, 33);
            colorBtn5.TabIndex = 34;
            colorBtn5.TabStop = false;
            colorBtn5.UseVisualStyleBackColor = true;
            colorBtn5.Click += ColorBtn5_Click;
            // 
            // colorBtn10
            // 
            colorBtn10.Location = new Point(168, 33);
            colorBtn10.Name = "colorBtn10";
            colorBtn10.Size = new Size(33, 33);
            colorBtn10.TabIndex = 33;
            colorBtn10.TabStop = false;
            colorBtn10.UseVisualStyleBackColor = true;
            colorBtn10.Click += ColorBtn10_Click;
            // 
            // colorBtn2
            // 
            colorBtn2.Location = new Point(69, 0);
            colorBtn2.Name = "colorBtn2";
            colorBtn2.Size = new Size(33, 33);
            colorBtn2.TabIndex = 32;
            colorBtn2.TabStop = false;
            colorBtn2.UseVisualStyleBackColor = true;
            colorBtn2.Click += ColorBtn2_Click;
            // 
            // colorBtn7
            // 
            colorBtn7.Location = new Point(69, 33);
            colorBtn7.Name = "colorBtn7";
            colorBtn7.Size = new Size(33, 33);
            colorBtn7.TabIndex = 31;
            colorBtn7.TabStop = false;
            colorBtn7.UseVisualStyleBackColor = true;
            colorBtn7.Click += ColorBtn7_Click;
            // 
            // colorBtn3
            // 
            colorBtn3.Location = new Point(102, 0);
            colorBtn3.Name = "colorBtn3";
            colorBtn3.Size = new Size(33, 33);
            colorBtn3.TabIndex = 30;
            colorBtn3.TabStop = false;
            colorBtn3.UseVisualStyleBackColor = true;
            colorBtn3.Click += ColorBtn3_Click;
            // 
            // colorBtn8
            // 
            colorBtn8.Location = new Point(102, 33);
            colorBtn8.Name = "colorBtn8";
            colorBtn8.Size = new Size(33, 33);
            colorBtn8.TabIndex = 29;
            colorBtn8.TabStop = false;
            colorBtn8.UseVisualStyleBackColor = true;
            colorBtn8.Click += ColorBtn8_Click;
            // 
            // FrmQuizBtnBlack
            // 
            FrmQuizBtnBlack.BackColor = Color.Black;
            FrmQuizBtnBlack.Location = new Point(3, 0);
            FrmQuizBtnBlack.Name = "FrmQuizBtnBlack";
            FrmQuizBtnBlack.Size = new Size(33, 33);
            FrmQuizBtnBlack.TabIndex = 28;
            FrmQuizBtnBlack.TabStop = false;
            FrmQuizBtnBlack.UseVisualStyleBackColor = false;
            FrmQuizBtnBlack.Click += FrmQuizBtnBlack_Click;
            // 
            // FrmMainBtnWhite
            // 
            FrmMainBtnWhite.Location = new Point(3, 33);
            FrmMainBtnWhite.Name = "FrmMainBtnWhite";
            FrmMainBtnWhite.Size = new Size(33, 33);
            FrmMainBtnWhite.TabIndex = 27;
            FrmMainBtnWhite.TabStop = false;
            FrmMainBtnWhite.UseVisualStyleBackColor = true;
            FrmMainBtnWhite.Click += FrmMainBtnWhite_Click;
            // 
            // colorBtn1
            // 
            colorBtn1.Location = new Point(36, 0);
            colorBtn1.Name = "colorBtn1";
            colorBtn1.Size = new Size(33, 33);
            colorBtn1.TabIndex = 26;
            colorBtn1.TabStop = false;
            colorBtn1.UseVisualStyleBackColor = true;
            colorBtn1.Click += ColorBtn1_Click;
            // 
            // colorBtn6
            // 
            colorBtn6.Location = new Point(36, 33);
            colorBtn6.Name = "colorBtn6";
            colorBtn6.Size = new Size(33, 33);
            colorBtn6.TabIndex = 25;
            colorBtn6.TabStop = false;
            colorBtn6.UseVisualStyleBackColor = true;
            colorBtn6.Click += ColorBtn6_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Gray;
            panel4.Controls.Add(btnEuler);
            panel4.Controls.Add(btnElement);
            panel4.Controls.Add(btnDifferential);
            panel4.Controls.Add(btnPartialDifferential);
            panel4.Controls.Add(btnComplex);
            panel4.Controls.Add(btnCopyright);
            panel4.Controls.Add(btnSqrt);
            panel4.Controls.Add(btnOr);
            panel4.Controls.Add(btnIntegral);
            panel4.Controls.Add(btnAnd);
            panel4.Controls.Add(btnFrenchLowerCase_ae);
            panel4.Controls.Add(btnSuperscript);
            panel4.Controls.Add(btnFrenchLowerCase_oe);
            panel4.Controls.Add(btnFrall);
            panel4.Controls.Add(btnSubscript);
            panel4.Controls.Add(btnReals);
            panel4.Controls.Add(btnApprox);
            panel4.Controls.Add(btnUnion);
            panel4.Controls.Add(btnPlusMinus);
            panel4.Controls.Add(btnIntersect);
            panel4.Controls.Add(btnDegree);
            panel4.Controls.Add(btnDotR);
            panel4.Controls.Add(btnSpExclaim);
            panel4.Controls.Add(btnAngle);
            panel4.Controls.Add(btnLongLine);
            panel4.Controls.Add(btnInfinity);
            panel4.Controls.Add(btnArrowR);
            panel4.Controls.Add(btnIdenticalto);
            panel4.Controls.Add(btnSpNye);
            panel4.Controls.Add(btnGte);
            panel4.Controls.Add(btnCedilla);
            panel4.Controls.Add(btnLte);
            panel4.Controls.Add(btnNaturals);
            panel4.Controls.Add(btnTsus);
            panel4.Controls.Add(btnSpQuestionMk);
            panel4.Controls.Add(BtnBrackets);
            panel4.Controls.Add(BtnInsertParenthesis);
            panel4.Controls.Add(btnIntegers);
            panel4.Controls.Add(btnDotProd);
            panel4.Controls.Add(btnSingleQuotes);
            panel4.Controls.Add(BtnInsertFrenchQuotes);
            panel4.Controls.Add(BtnInsertFancyQuotes);
            panel4.Controls.Add(btnCircumflex);
            panel4.Controls.Add(btnShort);
            panel4.Controls.Add(btnUmlaut);
            panel4.Controls.Add(btnAcute);
            panel4.Controls.Add(btnGrave);
            panel4.Controls.Add(btnMacron);
            panel4.Location = new Point(460, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(794, 68);
            panel4.TabIndex = 53;
            // 
            // btnEuler
            // 
            btnEuler.Image = Resource.Euler;
            btnEuler.Location = new Point(725, 0);
            btnEuler.Name = "btnEuler";
            btnEuler.Size = new Size(33, 33);
            btnEuler.TabIndex = 66;
            btnEuler.TabStop = false;
            btnEuler.UseVisualStyleBackColor = true;
            btnEuler.Click += BtnEuler_Click;
            // 
            // btnElement
            // 
            btnElement.Image = Resource.Element;
            btnElement.Location = new Point(725, 33);
            btnElement.Name = "btnElement";
            btnElement.Size = new Size(33, 33);
            btnElement.TabIndex = 65;
            btnElement.TabStop = false;
            btnElement.UseVisualStyleBackColor = true;
            btnElement.Click += BtnElement_Click;
            // 
            // btnDifferential
            // 
            btnDifferential.Image = Resource.Dif;
            btnDifferential.Location = new Point(693, 0);
            btnDifferential.Name = "btnDifferential";
            btnDifferential.Size = new Size(33, 33);
            btnDifferential.TabIndex = 64;
            btnDifferential.TabStop = false;
            btnDifferential.UseVisualStyleBackColor = true;
            btnDifferential.Click += BtnDifferential_Click;
            // 
            // btnPartialDifferential
            // 
            btnPartialDifferential.Image = Resource.Partdiff;
            btnPartialDifferential.Location = new Point(693, 33);
            btnPartialDifferential.Name = "btnPartialDifferential";
            btnPartialDifferential.Size = new Size(33, 33);
            btnPartialDifferential.TabIndex = 63;
            btnPartialDifferential.TabStop = false;
            btnPartialDifferential.UseVisualStyleBackColor = true;
            btnPartialDifferential.Click += BtnPartialDifferential_Click;
            // 
            // btnComplex
            // 
            btnComplex.Image = Resource.complex;
            btnComplex.Location = new Point(660, 0);
            btnComplex.Name = "btnComplex";
            btnComplex.Size = new Size(33, 33);
            btnComplex.TabIndex = 62;
            btnComplex.TabStop = false;
            btnComplex.UseVisualStyleBackColor = true;
            btnComplex.Click += BtnComplex_Click;
            // 
            // btnCopyright
            // 
            btnCopyright.Image = Resource.copyr;
            btnCopyright.Location = new Point(660, 33);
            btnCopyright.Name = "btnCopyright";
            btnCopyright.Size = new Size(33, 33);
            btnCopyright.TabIndex = 61;
            btnCopyright.TabStop = false;
            btnCopyright.UseVisualStyleBackColor = true;
            btnCopyright.Click += BtnCopyright_Click;
            // 
            // btnSqrt
            // 
            btnSqrt.Image = Resource.Sqrt;
            btnSqrt.Location = new Point(430, 0);
            btnSqrt.Name = "btnSqrt";
            btnSqrt.Size = new Size(33, 33);
            btnSqrt.TabIndex = 52;
            btnSqrt.TabStop = false;
            btnSqrt.UseVisualStyleBackColor = true;
            btnSqrt.Click += BtnSqrt_Click;
            // 
            // btnOr
            // 
            btnOr.Image = Resource.Or;
            btnOr.Location = new Point(595, 0);
            btnOr.Name = "btnOr";
            btnOr.Size = new Size(33, 33);
            btnOr.TabIndex = 60;
            btnOr.TabStop = false;
            btnOr.UseVisualStyleBackColor = true;
            btnOr.Click += BtnOr_Click;
            // 
            // btnIntegral
            // 
            btnIntegral.Image = Resource.Integral;
            btnIntegral.Location = new Point(430, 33);
            btnIntegral.Name = "btnIntegral";
            btnIntegral.Size = new Size(33, 33);
            btnIntegral.TabIndex = 51;
            btnIntegral.TabStop = false;
            btnIntegral.UseVisualStyleBackColor = true;
            btnIntegral.Click += BtnIntegral_Click;
            // 
            // btnAnd
            // 
            btnAnd.Image = Resource.And;
            btnAnd.Location = new Point(595, 33);
            btnAnd.Name = "btnAnd";
            btnAnd.Size = new Size(33, 33);
            btnAnd.TabIndex = 59;
            btnAnd.TabStop = false;
            btnAnd.UseVisualStyleBackColor = true;
            btnAnd.Click += BtnAnd_Click;
            // 
            // btnFrenchLowerCase_ae
            // 
            btnFrenchLowerCase_ae.Image = Resource.frae;
            btnFrenchLowerCase_ae.Location = new Point(299, 0);
            btnFrenchLowerCase_ae.Name = "btnFrenchLowerCase_ae";
            btnFrenchLowerCase_ae.Size = new Size(33, 33);
            btnFrenchLowerCase_ae.TabIndex = 30;
            btnFrenchLowerCase_ae.TabStop = false;
            btnFrenchLowerCase_ae.UseVisualStyleBackColor = true;
            btnFrenchLowerCase_ae.Click += BtnFrenchLowerCase_ae_Click;
            // 
            // btnSuperscript
            // 
            btnSuperscript.Image = Resource.super;
            btnSuperscript.Location = new Point(365, 0);
            btnSuperscript.Name = "btnSuperscript";
            btnSuperscript.Size = new Size(33, 33);
            btnSuperscript.TabIndex = 50;
            btnSuperscript.TabStop = false;
            toolTip1.SetToolTip(btnSuperscript, "      Select the text you want superscripted, then click this button");
            btnSuperscript.UseVisualStyleBackColor = true;
            btnSuperscript.Click += BtnSuperscript_Click;
            // 
            // btnFrenchLowerCase_oe
            // 
            btnFrenchLowerCase_oe.Image = Resource.froe;
            btnFrenchLowerCase_oe.Location = new Point(299, 33);
            btnFrenchLowerCase_oe.Name = "btnFrenchLowerCase_oe";
            btnFrenchLowerCase_oe.Size = new Size(33, 33);
            btnFrenchLowerCase_oe.TabIndex = 29;
            btnFrenchLowerCase_oe.TabStop = false;
            btnFrenchLowerCase_oe.UseVisualStyleBackColor = true;
            btnFrenchLowerCase_oe.Click += BtnFrenchLowerCase_oe_Click;
            // 
            // btnFrall
            // 
            btnFrall.Image = Resource.frall;
            btnFrall.Location = new Point(628, 0);
            btnFrall.Name = "btnFrall";
            btnFrall.Size = new Size(33, 33);
            btnFrall.TabIndex = 58;
            btnFrall.TabStop = false;
            btnFrall.UseVisualStyleBackColor = true;
            btnFrall.Click += BtnForall_Click;
            // 
            // btnSubscript
            // 
            btnSubscript.Image = Resource.sub;
            btnSubscript.Location = new Point(365, 33);
            btnSubscript.Name = "btnSubscript";
            btnSubscript.Size = new Size(33, 33);
            btnSubscript.TabIndex = 49;
            btnSubscript.TabStop = false;
            toolTip1.SetToolTip(btnSubscript, "      Select the text you want subscripted, then click this button");
            btnSubscript.UseVisualStyleBackColor = true;
            btnSubscript.Click += BtnSubscript_Click;
            // 
            // btnReals
            // 
            btnReals.Image = Resource.Reals;
            btnReals.Location = new Point(628, 33);
            btnReals.Name = "btnReals";
            btnReals.Size = new Size(33, 33);
            btnReals.TabIndex = 57;
            btnReals.TabStop = false;
            btnReals.UseVisualStyleBackColor = true;
            btnReals.Click += BtnReals_Click;
            // 
            // btnApprox
            // 
            btnApprox.Image = Resource.approx;
            btnApprox.Location = new Point(398, 0);
            btnApprox.Name = "btnApprox";
            btnApprox.Size = new Size(33, 33);
            btnApprox.TabIndex = 48;
            btnApprox.TabStop = false;
            btnApprox.UseVisualStyleBackColor = true;
            btnApprox.Click += BtnApprox_Click;
            // 
            // btnUnion
            // 
            btnUnion.Image = Resource.Union;
            btnUnion.Location = new Point(529, 0);
            btnUnion.Name = "btnUnion";
            btnUnion.Size = new Size(33, 33);
            btnUnion.TabIndex = 56;
            btnUnion.TabStop = false;
            btnUnion.UseVisualStyleBackColor = true;
            btnUnion.Click += BtnUnion_Click;
            // 
            // btnPlusMinus
            // 
            btnPlusMinus.Image = Resource.pm;
            btnPlusMinus.Location = new Point(398, 33);
            btnPlusMinus.Name = "btnPlusMinus";
            btnPlusMinus.Size = new Size(33, 33);
            btnPlusMinus.TabIndex = 47;
            btnPlusMinus.TabStop = false;
            btnPlusMinus.UseVisualStyleBackColor = true;
            btnPlusMinus.Click += BtnPlusMinus_Click;
            // 
            // btnIntersect
            // 
            btnIntersect.Image = Resource.Intersect;
            btnIntersect.Location = new Point(529, 33);
            btnIntersect.Name = "btnIntersect";
            btnIntersect.Size = new Size(33, 33);
            btnIntersect.TabIndex = 55;
            btnIntersect.TabStop = false;
            btnIntersect.UseVisualStyleBackColor = true;
            btnIntersect.Click += BtnIntersect_Click;
            // 
            // btnDegree
            // 
            btnDegree.Image = Resource.deg;
            btnDegree.Location = new Point(266, 0);
            btnDegree.Name = "btnDegree";
            btnDegree.Size = new Size(33, 33);
            btnDegree.TabIndex = 46;
            btnDegree.TabStop = false;
            btnDegree.UseVisualStyleBackColor = true;
            btnDegree.Click += BtnDegree_Click;
            // 
            // btnDotR
            // 
            btnDotR.Image = Resource.dotLR;
            btnDotR.Location = new Point(562, 0);
            btnDotR.Name = "btnDotR";
            btnDotR.Size = new Size(33, 33);
            btnDotR.TabIndex = 54;
            btnDotR.TabStop = false;
            btnDotR.UseVisualStyleBackColor = true;
            btnDotR.Click += BtnDotR_Click;
            // 
            // btnSpExclaim
            // 
            btnSpExclaim.Image = Resource.Spex5;
            btnSpExclaim.Location = new Point(266, 33);
            btnSpExclaim.Name = "btnSpExclaim";
            btnSpExclaim.Size = new Size(33, 33);
            btnSpExclaim.TabIndex = 45;
            btnSpExclaim.TabStop = false;
            btnSpExclaim.UseVisualStyleBackColor = true;
            btnSpExclaim.Click += BtnSpExclaim_Click;
            // 
            // btnAngle
            // 
            btnAngle.Image = Resource.Angle;
            btnAngle.Location = new Point(562, 33);
            btnAngle.Name = "btnAngle";
            btnAngle.Size = new Size(33, 33);
            btnAngle.TabIndex = 53;
            btnAngle.TabStop = false;
            btnAngle.UseVisualStyleBackColor = true;
            btnAngle.Click += btnAngle_Click;
            // 
            // btnLongLine
            // 
            btnLongLine.Location = new Point(332, 0);
            btnLongLine.Name = "btnLongLine";
            btnLongLine.Size = new Size(33, 33);
            btnLongLine.TabIndex = 44;
            btnLongLine.TabStop = false;
            btnLongLine.Text = "—";
            btnLongLine.UseVisualStyleBackColor = true;
            btnLongLine.Click += BtnLongLine_Click;
            // 
            // btnInfinity
            // 
            btnInfinity.Image = Resource.Infinity;
            btnInfinity.Location = new Point(463, 0);
            btnInfinity.Name = "btnInfinity";
            btnInfinity.Size = new Size(33, 33);
            btnInfinity.TabIndex = 52;
            btnInfinity.TabStop = false;
            btnInfinity.UseVisualStyleBackColor = true;
            btnInfinity.Click += BtnInfinity_Click;
            // 
            // btnArrowR
            // 
            btnArrowR.Location = new Point(332, 33);
            btnArrowR.Name = "btnArrowR";
            btnArrowR.Size = new Size(33, 33);
            btnArrowR.TabIndex = 43;
            btnArrowR.TabStop = false;
            btnArrowR.Text = "→";
            btnArrowR.UseVisualStyleBackColor = true;
            btnArrowR.Click += BtnArrowR_Click;
            // 
            // btnIdenticalto
            // 
            btnIdenticalto.Image = Resource.Identicalto;
            btnIdenticalto.Location = new Point(463, 33);
            btnIdenticalto.Name = "btnIdenticalto";
            btnIdenticalto.Size = new Size(33, 33);
            btnIdenticalto.TabIndex = 51;
            btnIdenticalto.TabStop = false;
            btnIdenticalto.UseVisualStyleBackColor = true;
            btnIdenticalto.Click += BtnIdenticalto_Click;
            // 
            // btnSpNye
            // 
            btnSpNye.Location = new Point(200, 1);
            btnSpNye.Name = "btnSpNye";
            btnSpNye.Size = new Size(33, 33);
            btnSpNye.TabIndex = 42;
            btnSpNye.TabStop = false;
            btnSpNye.Text = "ñ";
            btnSpNye.UseVisualStyleBackColor = true;
            btnSpNye.Click += BtnSpanishNye_Click;
            // 
            // btnGte
            // 
            btnGte.Image = Resource.gte;
            btnGte.Location = new Point(496, 0);
            btnGte.Name = "btnGte";
            btnGte.Size = new Size(33, 33);
            btnGte.TabIndex = 50;
            btnGte.TabStop = false;
            btnGte.UseVisualStyleBackColor = true;
            btnGte.Click += BtnGte_Click;
            // 
            // btnCedilla
            // 
            btnCedilla.Image = Resource.Frced2;
            btnCedilla.Location = new Point(200, 33);
            btnCedilla.Name = "btnCedilla";
            btnCedilla.Size = new Size(33, 33);
            btnCedilla.TabIndex = 41;
            btnCedilla.TabStop = false;
            btnCedilla.UseVisualStyleBackColor = true;
            btnCedilla.Click += BtnCedilla_Click;
            // 
            // btnLte
            // 
            btnLte.Image = Resource.lte;
            btnLte.Location = new Point(496, 33);
            btnLte.Name = "btnLte";
            btnLte.Size = new Size(33, 33);
            btnLte.TabIndex = 49;
            btnLte.TabStop = false;
            btnLte.UseVisualStyleBackColor = true;
            btnLte.Click += BtnLte_Click;
            // 
            // btnNaturals
            // 
            btnNaturals.Image = Resource.Naturals;
            btnNaturals.Location = new Point(758, 33);
            btnNaturals.Name = "btnNaturals";
            btnNaturals.Size = new Size(33, 33);
            btnNaturals.TabIndex = 12;
            btnNaturals.TabStop = false;
            btnNaturals.UseVisualStyleBackColor = true;
            btnNaturals.Click += BtnNaturals_Click;
            // 
            // btnTsus
            // 
            btnTsus.Location = new Point(233, 0);
            btnTsus.Name = "btnTsus";
            btnTsus.Size = new Size(33, 33);
            btnTsus.TabIndex = 40;
            btnTsus.TabStop = false;
            btnTsus.Text = "ß";
            btnTsus.UseVisualStyleBackColor = true;
            btnTsus.Click += BtnTsus_Click;
            // 
            // btnSpQuestionMk
            // 
            btnSpQuestionMk.Image = Resource.Spqu;
            btnSpQuestionMk.Location = new Point(233, 33);
            btnSpQuestionMk.Name = "btnSpQuestionMk";
            btnSpQuestionMk.Size = new Size(33, 33);
            btnSpQuestionMk.TabIndex = 39;
            btnSpQuestionMk.TabStop = false;
            btnSpQuestionMk.UseVisualStyleBackColor = true;
            btnSpQuestionMk.Click += BtnSpQuestionMk_Click;
            // 
            // BtnBrackets
            // 
            BtnBrackets.Image = Resource.Brakets2;
            BtnBrackets.Location = new Point(167, 0);
            BtnBrackets.Name = "BtnBrackets";
            BtnBrackets.Size = new Size(33, 33);
            BtnBrackets.TabIndex = 38;
            BtnBrackets.TabStop = false;
            BtnBrackets.UseVisualStyleBackColor = true;
            BtnBrackets.Click += BtnBrackets_Click;
            // 
            // BtnInsertParenthesis
            // 
            BtnInsertParenthesis.Image = Resource.Parenthesis1;
            BtnInsertParenthesis.Location = new Point(167, 33);
            BtnInsertParenthesis.Name = "BtnInsertParenthesis";
            BtnInsertParenthesis.Size = new Size(33, 33);
            BtnInsertParenthesis.TabIndex = 37;
            BtnInsertParenthesis.TabStop = false;
            BtnInsertParenthesis.UseVisualStyleBackColor = true;
            BtnInsertParenthesis.Click += BtnInsertParenthesis_Click;
            // 
            // btnIntegers
            // 
            btnIntegers.Image = Resource.Integers;
            btnIntegers.Location = new Point(758, 0);
            btnIntegers.Name = "btnIntegers";
            btnIntegers.Size = new Size(33, 33);
            btnIntegers.TabIndex = 8;
            btnIntegers.TabStop = false;
            btnIntegers.UseVisualStyleBackColor = true;
            btnIntegers.Click += BtnIntegers_Click;
            // 
            // btnDotProd
            // 
            btnDotProd.Image = Resource.DotProd;
            btnDotProd.Location = new Point(102, 0);
            btnDotProd.Name = "btnDotProd";
            btnDotProd.Size = new Size(33, 33);
            btnDotProd.TabIndex = 36;
            btnDotProd.TabStop = false;
            btnDotProd.UseVisualStyleBackColor = true;
            btnDotProd.Click += BtnDotProd_Click;
            // 
            // btnSingleQuotes
            // 
            btnSingleQuotes.Image = Resource.ApostAltr;
            btnSingleQuotes.Location = new Point(102, 33);
            btnSingleQuotes.Name = "btnSingleQuotes";
            btnSingleQuotes.Size = new Size(33, 33);
            btnSingleQuotes.TabIndex = 35;
            btnSingleQuotes.TabStop = false;
            btnSingleQuotes.UseVisualStyleBackColor = true;
            btnSingleQuotes.Click += BtnSingleQuotes_Click;
            // 
            // BtnInsertFrenchQuotes
            // 
            BtnInsertFrenchQuotes.Image = Resource.QuoteFrench;
            BtnInsertFrenchQuotes.Location = new Point(135, 0);
            BtnInsertFrenchQuotes.Name = "BtnInsertFrenchQuotes";
            BtnInsertFrenchQuotes.Size = new Size(33, 33);
            BtnInsertFrenchQuotes.TabIndex = 34;
            BtnInsertFrenchQuotes.TabStop = false;
            BtnInsertFrenchQuotes.UseVisualStyleBackColor = true;
            BtnInsertFrenchQuotes.Click += BtnInsertFrenchQuotes_Click;
            // 
            // BtnInsertFancyQuotes
            // 
            BtnInsertFancyQuotes.Image = Resource.QuoteAlt;
            BtnInsertFancyQuotes.Location = new Point(135, 33);
            BtnInsertFancyQuotes.Name = "BtnInsertFancyQuotes";
            BtnInsertFancyQuotes.Size = new Size(33, 33);
            BtnInsertFancyQuotes.TabIndex = 33;
            BtnInsertFancyQuotes.TabStop = false;
            BtnInsertFancyQuotes.UseVisualStyleBackColor = true;
            BtnInsertFancyQuotes.Click += BtnInsertFancyQuotes_Click;
            // 
            // btnCircumflex
            // 
            btnCircumflex.Image = Resource.ciralt;
            btnCircumflex.Location = new Point(69, 0);
            btnCircumflex.Name = "btnCircumflex";
            btnCircumflex.Size = new Size(33, 33);
            btnCircumflex.TabIndex = 32;
            btnCircumflex.TabStop = false;
            btnCircumflex.Text = "              Type a vowel then click this button for a circumflex";
            toolTip1.SetToolTip(btnCircumflex, "              Type a vowel then click this button for a circumflex (ALT-END)");
            btnCircumflex.UseVisualStyleBackColor = true;
            btnCircumflex.Click += btnCircumflex_Click;
            // 
            // btnShort
            // 
            btnShort.Image = Resource.sm;
            btnShort.Location = new Point(69, 33);
            btnShort.Name = "btnShort";
            btnShort.Size = new Size(33, 33);
            btnShort.TabIndex = 31;
            btnShort.TabStop = false;
            btnShort.Text = "              Type a vowel then click this button for a short vowel\r\n              Keyboard shortcut is Alt +";
            toolTip1.SetToolTip(btnShort, "              Type a vowel then click this button for a short symbol");
            btnShort.UseVisualStyleBackColor = true;
            btnShort.Click += BtnShort_Click;
            // 
            // btnUmlaut
            // 
            btnUmlaut.Image = Resource.um;
            btnUmlaut.Location = new Point(3, 0);
            btnUmlaut.Name = "btnUmlaut";
            btnUmlaut.Size = new Size(33, 33);
            btnUmlaut.TabIndex = 28;
            btnUmlaut.TabStop = false;
            toolTip1.SetToolTip(btnUmlaut, "              Type a vowel then click this button for an umlaut");
            btnUmlaut.UseVisualStyleBackColor = true;
            btnUmlaut.Click += BtnUmlaut_Click;
            // 
            // btnAcute
            // 
            btnAcute.Image = Resource.acute;
            btnAcute.Location = new Point(3, 33);
            btnAcute.Name = "btnAcute";
            btnAcute.Size = new Size(33, 33);
            btnAcute.TabIndex = 27;
            btnAcute.TabStop = false;
            toolTip1.SetToolTip(btnAcute, "              Type a vowel then click this button for an acute accent (ALT right arow)");
            btnAcute.UseVisualStyleBackColor = true;
            btnAcute.Click += BtnAcute_Click;
            // 
            // btnGrave
            // 
            btnGrave.Image = Resource.grv;
            btnGrave.Location = new Point(36, 0);
            btnGrave.Name = "btnGrave";
            btnGrave.Size = new Size(33, 33);
            btnGrave.TabIndex = 26;
            btnGrave.TabStop = false;
            toolTip1.SetToolTip(btnGrave, "              Type a vowel then click this button for an grave accent");
            btnGrave.UseVisualStyleBackColor = true;
            btnGrave.Click += BtnGrave_Click;
            // 
            // btnMacron
            // 
            btnMacron.Image = Resource.mac;
            btnMacron.Location = new Point(36, 33);
            btnMacron.Name = "btnMacron";
            btnMacron.Size = new Size(33, 33);
            btnMacron.TabIndex = 25;
            btnMacron.TabStop = false;
            toolTip1.SetToolTip(btnMacron, "              Type a vowel then click this button for a macron");
            btnMacron.UseVisualStyleBackColor = true;
            btnMacron.Click += BtnMacron_Click;
            // 
            // RTBQuestion
            // 
            RTBQuestion.BackColor = SystemColors.ScrollBar;
            RTBQuestion.Font = new Font("Times New Roman", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBQuestion.Location = new Point(6, 73);
            RTBQuestion.Name = "RTBQuestion";
            RTBQuestion.Size = new Size(1482, 190);
            RTBQuestion.TabIndex = 0;
            RTBQuestion.Text = "";
            RTBQuestion.Click += RTBQuestion_Click;
            RTBQuestion.TextChanged += RTBQuestion_TextChanged;
            RTBQuestion.DoubleClick += RTBQuestion_DoubleClick;
            RTBQuestion.KeyDown += RTBQuestion_KeyDown;
            RTBQuestion.KeyUp += RTBQuestion_KeyUp;
            RTBQuestion.MouseDoubleClick += RTBQuestion_MouseDoubleClick;
            RTBQuestion.MouseDown += RTBQuestion_MouseDown;
            RTBQuestion.MouseMove += RTBQuestion_MouseMove;
            RTBQuestion.MouseUp += RTBQuestion_MouseUp;
            // 
            // RTBAnswer
            // 
            RTBAnswer.BackColor = SystemColors.ScrollBar;
            RTBAnswer.Font = new Font("Times New Roman", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBAnswer.Location = new Point(5, 352);
            RTBAnswer.Name = "RTBAnswer";
            RTBAnswer.Size = new Size(1482, 458);
            RTBAnswer.TabIndex = 1;
            RTBAnswer.Text = "";
            RTBAnswer.Click += RTBAnswer_Click;
            RTBAnswer.TextChanged += RTBAnswer_TextChanged;
            RTBAnswer.DoubleClick += RTBAnswer_DoubleClick;
            RTBAnswer.KeyDown += RTBAnswer_KeyDown;
            RTBAnswer.KeyUp += RTBAnswer_KeyUp;
            RTBAnswer.MouseDoubleClick += RTBAnswer_MouseDoubleClick;
            RTBAnswer.MouseDown += RTBAnswer_MouseDown;
            RTBAnswer.MouseMove += RTBAnswer_MouseMove;
            RTBAnswer.MouseUp += RTBAnswer_MouseUp;
            // 
            // btnInvert
            // 
            btnInvert.BackColor = Color.LightCyan;
            btnInvert.Font = new Font("Times New Roman", 13F);
            btnInvert.Location = new Point(95, 3);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(121, 32);
            btnInvert.TabIndex = 56;
            btnInvert.TabStop = false;
            btnInvert.Text = "Invert";
            btnInvert.UseVisualStyleBackColor = false;
            btnInvert.Click += BtnInvert_Click;
            // 
            // ControlPanel
            // 
            ControlPanel.BackColor = SystemColors.ControlDark;
            ControlPanel.Controls.Add(BtnQuizOpenFolder);
            ControlPanel.Controls.Add(chkAddClear);
            ControlPanel.Controls.Add(chkShuffle);
            ControlPanel.Controls.Add(ChkEnforceFontSize);
            ControlPanel.Controls.Add(btnRevert);
            ControlPanel.Controls.Add(BtnClearBottom);
            ControlPanel.Controls.Add(BtnClearTop);
            ControlPanel.Controls.Add(btnDeleteQA);
            ControlPanel.Controls.Add(btnClearTitle);
            ControlPanel.Controls.Add(RTBQuizName);
            ControlPanel.Controls.Add(txtTTLQRemaining);
            ControlPanel.Controls.Add(txtTTLQuestions);
            ControlPanel.Controls.Add(lblRemainders);
            ControlPanel.Controls.Add(lblTotalQuestions);
            ControlPanel.Controls.Add(btnNewQuiz);
            ControlPanel.Controls.Add(btnGetQuiz);
            ControlPanel.Controls.Add(btnSaveEdit);
            ControlPanel.Controls.Add(btnNext);
            ControlPanel.Controls.Add(btnAddQuestion);
            ControlPanel.Controls.Add(btnClearAll);
            ControlPanel.Controls.Add(btnAppend);
            ControlPanel.Controls.Add(btnInvert);
            ControlPanel.Location = new Point(7, 267);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Size = new Size(1478, 72);
            ControlPanel.TabIndex = 57;
            // 
            // BtnQuizOpenFolder
            // 
            BtnQuizOpenFolder.BackColor = Color.BlueViolet;
            BtnQuizOpenFolder.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnQuizOpenFolder.ForeColor = Color.White;
            BtnQuizOpenFolder.Location = new Point(1105, 35);
            BtnQuizOpenFolder.Name = "BtnQuizOpenFolder";
            BtnQuizOpenFolder.Size = new Size(111, 32);
            BtnQuizOpenFolder.TabIndex = 77;
            BtnQuizOpenFolder.TabStop = false;
            BtnQuizOpenFolder.Text = "Quiz Folder";
            BtnQuizOpenFolder.UseVisualStyleBackColor = false;
            BtnQuizOpenFolder.Click += BtnQuizOpenFolder_Click;
            // 
            // chkAddClear
            // 
            chkAddClear.AutoSize = true;
            chkAddClear.Checked = true;
            chkAddClear.CheckState = CheckState.Checked;
            chkAddClear.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAddClear.Location = new Point(1223, 40);
            chkAddClear.Name = "chkAddClear";
            chkAddClear.Size = new Size(188, 24);
            chkAddClear.TabIndex = 59;
            chkAddClear.TabStop = false;
            chkAddClear.Text = "Add Question + Clear";
            chkAddClear.UseVisualStyleBackColor = true;
            // 
            // chkShuffle
            // 
            chkShuffle.AutoSize = true;
            chkShuffle.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkShuffle.Location = new Point(1222, 8);
            chkShuffle.Name = "chkShuffle";
            chkShuffle.Size = new Size(83, 24);
            chkShuffle.TabIndex = 58;
            chkShuffle.TabStop = false;
            chkShuffle.Text = "Shuffle";
            chkShuffle.UseVisualStyleBackColor = true;
            chkShuffle.CheckedChanged += ChkShuffle_CheckedChanged;
            // 
            // ChkEnforceFontSize
            // 
            ChkEnforceFontSize.AutoSize = true;
            ChkEnforceFontSize.Checked = true;
            ChkEnforceFontSize.CheckState = CheckState.Checked;
            ChkEnforceFontSize.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ChkEnforceFontSize.Location = new Point(1314, 7);
            ChkEnforceFontSize.Name = "ChkEnforceFontSize";
            ChkEnforceFontSize.Size = new Size(157, 24);
            ChkEnforceFontSize.TabIndex = 77;
            ChkEnforceFontSize.TabStop = false;
            ChkEnforceFontSize.Text = "Impose Font Size";
            ChkEnforceFontSize.UseVisualStyleBackColor = true;
            // 
            // btnRevert
            // 
            btnRevert.BackColor = Color.Gold;
            btnRevert.Font = new Font("Times New Roman", 13F);
            btnRevert.Location = new Point(4, 3);
            btnRevert.Name = "btnRevert";
            btnRevert.Size = new Size(91, 32);
            btnRevert.TabIndex = 75;
            btnRevert.TabStop = false;
            btnRevert.Text = "Revert";
            btnRevert.UseVisualStyleBackColor = false;
            btnRevert.Click += BtnRevert_Click;
            // 
            // BtnClearBottom
            // 
            BtnClearBottom.BackColor = Color.GreenYellow;
            BtnClearBottom.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnClearBottom.Location = new Point(968, 35);
            BtnClearBottom.Name = "BtnClearBottom";
            BtnClearBottom.Size = new Size(132, 32);
            BtnClearBottom.TabIndex = 74;
            BtnClearBottom.TabStop = false;
            BtnClearBottom.Text = "Clear Bottom";
            BtnClearBottom.UseVisualStyleBackColor = false;
            BtnClearBottom.Click += BtnClearBottom_Click;
            // 
            // BtnClearTop
            // 
            BtnClearTop.BackColor = Color.GreenYellow;
            BtnClearTop.Font = new Font("Times New Roman", 13F);
            BtnClearTop.Location = new Point(968, 2);
            BtnClearTop.Name = "BtnClearTop";
            BtnClearTop.Size = new Size(132, 32);
            BtnClearTop.TabIndex = 73;
            BtnClearTop.TabStop = false;
            BtnClearTop.Text = "Clear Top";
            BtnClearTop.UseVisualStyleBackColor = false;
            BtnClearTop.Click += BtnClearTop_Click;
            // 
            // btnDeleteQA
            // 
            btnDeleteQA.BackColor = SystemColors.ActiveCaption;
            btnDeleteQA.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeleteQA.ForeColor = Color.Red;
            btnDeleteQA.Location = new Point(856, 36);
            btnDeleteQA.Name = "btnDeleteQA";
            btnDeleteQA.Size = new Size(108, 32);
            btnDeleteQA.TabIndex = 70;
            btnDeleteQA.TabStop = false;
            btnDeleteQA.Text = "Delete";
            btnDeleteQA.UseVisualStyleBackColor = false;
            btnDeleteQA.Click += BtnDeleteQuestion_Click;
            // 
            // btnClearTitle
            // 
            btnClearTitle.BackColor = Color.GreenYellow;
            btnClearTitle.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearTitle.Location = new Point(856, 2);
            btnClearTitle.Name = "btnClearTitle";
            btnClearTitle.Size = new Size(108, 32);
            btnClearTitle.TabIndex = 69;
            btnClearTitle.TabStop = false;
            btnClearTitle.Text = "Clear Title";
            btnClearTitle.UseVisualStyleBackColor = false;
            btnClearTitle.Click += BtnClearTitle_Click;
            // 
            // RTBQuizName
            // 
            RTBQuizName.BackColor = Color.Silver;
            RTBQuizName.Location = new Point(467, 2);
            RTBQuizName.Name = "RTBQuizName";
            RTBQuizName.Size = new Size(381, 32);
            RTBQuizName.TabIndex = 68;
            RTBQuizName.TabStop = false;
            RTBQuizName.Text = "";
            RTBQuizName.Click += RTBQuizName_Click;
            RTBQuizName.TextChanged += RTBQuizName_TextChanged;
            RTBQuizName.MouseMove += RTBQuizName_MouseMove;
            // 
            // txtTTLQRemaining
            // 
            txtTTLQRemaining.BackColor = Color.Black;
            txtTTLQRemaining.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            txtTTLQRemaining.ForeColor = Color.Lime;
            txtTTLQRemaining.Location = new Point(783, 37);
            txtTTLQRemaining.Name = "txtTTLQRemaining";
            txtTTLQRemaining.Size = new Size(66, 32);
            txtTTLQRemaining.TabIndex = 67;
            txtTTLQRemaining.TabStop = false;
            txtTTLQRemaining.Text = "0";
            txtTTLQRemaining.TextAlign = HorizontalAlignment.Center;
            // 
            // txtTTLQuestions
            // 
            txtTTLQuestions.BackColor = Color.Black;
            txtTTLQuestions.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            txtTTLQuestions.ForeColor = Color.Lime;
            txtTTLQuestions.Location = new Point(602, 38);
            txtTTLQuestions.Name = "txtTTLQuestions";
            txtTTLQuestions.Size = new Size(66, 32);
            txtTTLQuestions.TabIndex = 66;
            txtTTLQuestions.TabStop = false;
            txtTTLQuestions.Text = "0";
            txtTTLQuestions.TextAlign = HorizontalAlignment.Center;
            // 
            // lblRemainders
            // 
            lblRemainders.AutoSize = true;
            lblRemainders.Font = new Font("Times New Roman", 12F);
            lblRemainders.Location = new Point(683, 43);
            lblRemainders.Name = "lblRemainders";
            lblRemainders.Size = new Size(94, 22);
            lblRemainders.TabIndex = 65;
            lblRemainders.Text = "Remaining";
            // 
            // lblTotalQuestions
            // 
            lblTotalQuestions.AutoSize = true;
            lblTotalQuestions.Font = new Font("Times New Roman", 12F);
            lblTotalQuestions.Location = new Point(465, 43);
            lblTotalQuestions.Name = "lblTotalQuestions";
            lblTotalQuestions.Size = new Size(127, 22);
            lblTotalQuestions.TabIndex = 64;
            lblTotalQuestions.Text = "TTL Questions";
            // 
            // btnNewQuiz
            // 
            btnNewQuiz.BackColor = Color.Yellow;
            btnNewQuiz.Font = new Font("Times New Roman", 13F);
            btnNewQuiz.Location = new Point(341, 37);
            btnNewQuiz.Name = "btnNewQuiz";
            btnNewQuiz.Size = new Size(121, 32);
            btnNewQuiz.TabIndex = 63;
            btnNewQuiz.TabStop = false;
            btnNewQuiz.Text = "New Quiz";
            btnNewQuiz.UseVisualStyleBackColor = false;
            btnNewQuiz.Click += BtnNewQuiz_Click;
            // 
            // btnGetQuiz
            // 
            btnGetQuiz.BackColor = Color.Yellow;
            btnGetQuiz.Font = new Font("Times New Roman", 13F);
            btnGetQuiz.Location = new Point(341, 3);
            btnGetQuiz.Name = "btnGetQuiz";
            btnGetQuiz.Size = new Size(121, 32);
            btnGetQuiz.TabIndex = 62;
            btnGetQuiz.TabStop = false;
            btnGetQuiz.Text = "Get Quiz";
            btnGetQuiz.UseVisualStyleBackColor = false;
            btnGetQuiz.Click += BtnGetQuiz_Click;
            // 
            // btnSaveEdit
            // 
            btnSaveEdit.BackColor = Color.DarkGoldenrod;
            btnSaveEdit.Font = new Font("Times New Roman", 13F);
            btnSaveEdit.Location = new Point(217, 37);
            btnSaveEdit.Name = "btnSaveEdit";
            btnSaveEdit.Size = new Size(121, 32);
            btnSaveEdit.TabIndex = 61;
            btnSaveEdit.TabStop = false;
            btnSaveEdit.Text = "Save Edit";
            btnSaveEdit.UseVisualStyleBackColor = false;
            btnSaveEdit.Click += BtnSaveEdit_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.Cyan;
            btnNext.Font = new Font("Times New Roman", 13F);
            btnNext.Location = new Point(217, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(121, 32);
            btnNext.TabIndex = 60;
            btnNext.TabStop = false;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += BtnNext_Click;
            // 
            // btnAddQuestion
            // 
            btnAddQuestion.BackColor = Color.DarkGoldenrod;
            btnAddQuestion.Font = new Font("Times New Roman", 13F);
            btnAddQuestion.Location = new Point(95, 37);
            btnAddQuestion.Name = "btnAddQuestion";
            btnAddQuestion.Size = new Size(121, 32);
            btnAddQuestion.TabIndex = 59;
            btnAddQuestion.TabStop = false;
            btnAddQuestion.Text = "Add Question";
            btnAddQuestion.UseVisualStyleBackColor = false;
            btnAddQuestion.Click += BtnAddQuestion_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.BackColor = Color.GreenYellow;
            btnClearAll.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearAll.Location = new Point(1105, 2);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(111, 32);
            btnClearAll.TabIndex = 58;
            btnClearAll.TabStop = false;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = false;
            btnClearAll.Click += BtnClearAll_Click;
            // 
            // btnAppend
            // 
            btnAppend.BackColor = Color.Gold;
            btnAppend.Font = new Font("Times New Roman", 13F);
            btnAppend.Location = new Point(3, 37);
            btnAppend.Name = "btnAppend";
            btnAppend.Size = new Size(92, 32);
            btnAppend.TabIndex = 57;
            btnAppend.TabStop = false;
            btnAppend.Text = "Append";
            toolTip1.SetToolTip(btnAppend, "      Open the first quiz, then click the append button and select the second quiz you want to append to the one you have open.");
            btnAppend.UseVisualStyleBackColor = false;
            btnAppend.Click += BtnAppend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 9F);
            label1.Location = new Point(1272, 3);
            label1.Name = "label1";
            label1.Size = new Size(202, 68);
            label1.TabIndex = 78;
            label1.Text = "File extensions in Quiz Folder:\r\nSearches = .fdta   Quizes = .qdta\r\nBe careful not to delete file your \r\nhistory (recentFiles.txt)!\r\n";
            // 
            // lblDebuggingTest
            // 
            lblDebuggingTest.AutoSize = true;
            lblDebuggingTest.Font = new Font("Times New Roman", 11F);
            lblDebuggingTest.Location = new Point(1338, 82);
            lblDebuggingTest.Name = "lblDebuggingTest";
            lblDebuggingTest.Size = new Size(127, 21);
            lblDebuggingTest.TabIndex = 76;
            lblDebuggingTest.Text = "Debugging Test";
            lblDebuggingTest.Visible = false;
            // 
            // FrmQuiz
            // 
            AutoScaleDimensions = new SizeF(14F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1494, 819);
            Controls.Add(label1);
            Controls.Add(lblDebuggingTest);
            Controls.Add(ControlPanel);
            Controls.Add(RTBAnswer);
            Controls.Add(RTBQuestion);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(FontSizeComboBox);
            Controls.Add(FontComboBox);
            Font = new Font("Times New Roman", 15F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MinimumSize = new Size(1512, 800);
            Name = "FrmQuiz";
            Text = "  Tachufind";
            Activated += FrmQuiz_Activated;
            Deactivate += FrmQuiz_Deactivate;
            FormClosing += FrmQuiz_FormClosing;
            Load += FrmQuiz_Load;
            LocationChanged += FrmQuiz_LocationChanged;
            SizeChanged += FrmQuiz_SizeChanged;
            KeyUp += FrmQuiz_KeyUp;
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ControlPanel.ResumeLayout(false);
            ControlPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox FontSizeComboBox;
        private ComboBox FontComboBox;
        private Panel panel2;
        private Button btnBold;
        private Button btnUnderline;
        private Button btnItalic;
        private Button btnCapDeCap;
        private Button btnImageInsert;
        private Panel panel3;
        private Button btnRemHighlight;
        private Button colorBtn4;
        private Button colorBtn9;
        private Button colorBtn5;
        private Button colorBtn10;
        private Button colorBtn2;
        private Button colorBtn7;
        private Button colorBtn3;
        private Button colorBtn8;
        private Button FrmMainBtnWhite;
        private Button colorBtn1;
        private Button colorBtn6;
        private Panel panel4;
        private Button btnEuler;
        private Button btnElement;
        private Button btnDifferential;
        private Button btnPartialDifferential;
        private Button btnComplex;
        private Button btnCopyright;
        private Button btnSqrt;
        private Button btnOr;
        private Button btnIntegral;
        private Button btnAnd;
        private Button btnSuperscript;
        private Button btnFrall;
        private Button btnSubscript;
        private Button btnReals;
        private Button btnApprox;
        private Button btnUnion;
        private Button btnPlusMinus;
        private Button btnIntersect;
        private Button btnDegree;
        private Button btnDotR;
        private Button btnSpExclaim;
        private Button btnAngle;
        private Button btnLongLine;
        private Button btnInfinity;
        private Button btnArrowR;
        private Button btnIdenticalto;
        private Button btnSpNye;
        private Button btnGte;
        private Button btnCedilla;
        private Button btnLte;
        private Button btnNaturals;
        private Button btnTsus;
        private Button btnSpQuestionMk;
        private Button BtnBrackets;
        private Button BtnInsertParenthesis;
        private Button btnIntegers;
        private Button btnDotProd;
        private Button btnSingleQuotes;
        private Button BtnInsertFrenchQuotes;
        private Button BtnInsertFancyQuotes;
        private Button btnCircumflex;
        private Button btnShort;
        private Button btnFrenchLowerCase_ae;
        private Button btnFrenchLowerCase_oe;
        private Button btnUmlaut;
        private Button btnAcute;
        private Button btnGrave;
        private Button btnMacron;
        public RichTextBox RTBQuestion;
        public RichTextBox RTBAnswer;
        private Button btnInvert;
        private Panel ControlPanel;
        private Button btnAddQuestion;
        private Button btnClearAll;
        private Button btnAppend;
        private FontDialog fontDialog1;
        private Button btnNewQuiz;
        private Button btnGetQuiz;
        private Button btnSaveEdit;
        private Button btnNext;
        private Label lblRemainders;
        private Label lblTotalQuestions;
        private RichTextBox RTBQuizName;
        private TextBox txtTTLQRemaining;
        private TextBox txtTTLQuestions;
        private Button BtnClearBottom;
        private Button BtnClearTop;
        private Button btnDeleteQA;
        private Button btnClearTitle;
        private Button btnRevert;
        private CheckBox chkShuffle;
        private CheckBox chkAddClear;
        private Label lblDebuggingTest;
        private ToolTip toolTip1;
        private Button FrmQuizBtnBlack;
        private Button BtnSection;
        private Button BtnQuizOpenFolder;
        private Label label1;
        private CheckBox ChkEnforceFontSize;
    }
}