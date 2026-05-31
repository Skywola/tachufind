namespace Tachufind
{
    partial class FrmColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmColor));
            lblSrch = new Label();
            RbEditColor = new RadioButton();
            lblFind01 = new Label();
            lblPipe01 = new Label();
            RtfFind1 = new RichTextBox();
            FrmColorBtnC1 = new Button();
            RtfReplace1 = new RichTextBox();
            lblReplace01 = new Label();
            RtfReplace2 = new RichTextBox();
            lblReplace02 = new Label();
            FrmColorBtnC2 = new Button();
            RtfFind2 = new RichTextBox();
            lblPipe02 = new Label();
            lblFind02 = new Label();
            RtfReplace3 = new RichTextBox();
            lblReplace03 = new Label();
            FrmColorBtnC3 = new Button();
            RtfFind3 = new RichTextBox();
            lblPipe03 = new Label();
            lblFind03 = new Label();
            RtfReplace4 = new RichTextBox();
            lblReplace04 = new Label();
            FrmColorBtnC4 = new Button();
            RtfFind4 = new RichTextBox();
            lblPipe04 = new Label();
            lblFind04 = new Label();
            RtfReplace5 = new RichTextBox();
            lblReplace05 = new Label();
            FrmColorBtnC5 = new Button();
            RtfFind5 = new RichTextBox();
            lblPipe05 = new Label();
            lblFind05 = new Label();
            RtfReplace6 = new RichTextBox();
            lblReplace06 = new Label();
            FrmColorBtnC6 = new Button();
            RtfFind6 = new RichTextBox();
            lblPipe06 = new Label();
            lblFind06 = new Label();
            RtfReplace7 = new RichTextBox();
            lblReplace07 = new Label();
            FrmColorBtnC7 = new Button();
            RtfFind7 = new RichTextBox();
            lblPipe07 = new Label();
            lblFind07 = new Label();
            RtfReplace8 = new RichTextBox();
            lblReplace08 = new Label();
            FrmColorBtnC8 = new Button();
            RtfFind8 = new RichTextBox();
            lblPipe08 = new Label();
            lblFind08 = new Label();
            btnReplace_All = new Button();
            btnGetSearch = new Button();
            btnSaveSearch = new Button();
            btnPasteInto = new Button();
            btnClearAll = new Button();
            ChkMatchCase = new CheckBox();
            btnClearFinds = new Button();
            btnClearReplace = new Button();
            btnClear = new Button();
            RtfReplace9 = new RichTextBox();
            lblReplace09 = new Label();
            FrmColorBtnC9 = new Button();
            RtfFind9 = new RichTextBox();
            lblPipe09 = new Label();
            lblFind09 = new Label();
            RtfReplace10 = new RichTextBox();
            lblReplace10 = new Label();
            FrmColorBtnC10 = new Button();
            RtfFind10 = new RichTextBox();
            lblPipe10 = new Label();
            lblFind10 = new Label();
            toolTip1 = new ToolTip(components);
            btnPrefixReplace = new Button();
            btnSuffixReplace = new Button();
            BtnUndo = new Button();
            BtnDefaultColors = new Button();
            colorDialog1 = new ColorDialog();
            label1 = new Label();
            ChkWordOnly = new CheckBox();
            panel1 = new Panel();
            RbTextReplace = new RadioButton();
            RtfSearchName = new RichTextBox();
            BtnSearchFolder = new Button();
            lblBoundaryMarkerCount = new Label();
            TxtTimeIndicator = new TextBox();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblSrch
            // 
            lblSrch.AutoSize = true;
            lblSrch.Font = new Font("Times New Roman", 12F);
            lblSrch.ForeColor = Color.Black;
            lblSrch.Location = new Point(2, 8);
            lblSrch.Name = "lblSrch";
            lblSrch.Size = new Size(115, 22);
            lblSrch.TabIndex = 0;
            lblSrch.Text = "Search Name";
            // 
            // RbEditColor
            // 
            RbEditColor.Font = new Font("Times New Roman", 13F);
            RbEditColor.ForeColor = Color.Black;
            RbEditColor.Location = new Point(29, 40);
            RbEditColor.Name = "RbEditColor";
            RbEditColor.Size = new Size(109, 25);
            RbEditColor.TabIndex = 0;
            RbEditColor.Text = "Color";
            RbEditColor.UseVisualStyleBackColor = true;
            RbEditColor.CheckedChanged += RbEditColor_CheckedChanged;
            RbEditColor.Click += RbEditColor_Click;
            // 
            // lblFind01
            // 
            lblFind01.AutoSize = true;
            lblFind01.Font = new Font("Times New Roman", 12F);
            lblFind01.ForeColor = Color.Black;
            lblFind01.Location = new Point(12, 77);
            lblFind01.Name = "lblFind01";
            lblFind01.Size = new Size(46, 22);
            lblFind01.TabIndex = 0;
            lblFind01.Text = "Find";
            // 
            // lblPipe01
            // 
            lblPipe01.AutoSize = true;
            lblPipe01.ForeColor = Color.Black;
            lblPipe01.Location = new Point(60, 71);
            lblPipe01.Name = "lblPipe01";
            lblPipe01.Size = new Size(18, 29);
            lblPipe01.TabIndex = 0;
            lblPipe01.Text = "|";
            lblPipe01.Click += LblPipe01_Click;
            // 
            // RtfFind1
            // 
            RtfFind1.BackColor = Color.DarkGray;
            RtfFind1.DetectUrls = false;
            RtfFind1.Font = new Font("Times New Roman", 13F);
            RtfFind1.Location = new Point(81, 74);
            RtfFind1.Multiline = false;
            RtfFind1.Name = "RtfFind1";
            RtfFind1.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind1.Size = new Size(253, 35);
            RtfFind1.TabIndex = 1;
            RtfFind1.Text = "";
            RtfFind1.Click += RtfFind01_Click;
            RtfFind1.MouseClick += RtfFind01_MouseClick;
            RtfFind1.TextChanged += RtfFind1_TextChanged;
            RtfFind1.DoubleClick += RtfFind01_DoubleClick;
            RtfFind1.KeyDown += RtfFind01_KeyDown;
            RtfFind1.KeyPress += RtfFind01_KeyPress;
            RtfFind1.Leave += RtfFind1_Leave;
            RtfFind1.MouseDown += RtfFind01_MouseDown;
            RtfFind1.MouseEnter += RtfFind1_MouseEnter;
            RtfFind1.MouseMove += RtfFind1_MouseMove;
            RtfFind1.PreviewKeyDown += RtfFind01_PreviewKeyDown;
            // 
            // FrmColorBtnC1
            // 
            FrmColorBtnC1.ForeColor = Color.Black;
            FrmColorBtnC1.Location = new Point(339, 76);
            FrmColorBtnC1.Name = "FrmColorBtnC1";
            FrmColorBtnC1.Size = new Size(30, 30);
            FrmColorBtnC1.TabIndex = 0;
            FrmColorBtnC1.TabStop = false;
            FrmColorBtnC1.UseVisualStyleBackColor = true;
            FrmColorBtnC1.Click += BtnC01_Click;
            // 
            // RtfReplace1
            // 
            RtfReplace1.BackColor = Color.DarkGray;
            RtfReplace1.DetectUrls = false;
            RtfReplace1.Font = new Font("Times New Roman", 13F);
            RtfReplace1.Location = new Point(81, 112);
            RtfReplace1.Multiline = false;
            RtfReplace1.Name = "RtfReplace1";
            RtfReplace1.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace1.Size = new Size(253, 35);
            RtfReplace1.TabIndex = 2;
            RtfReplace1.Text = "";
            RtfReplace1.Click += RtfReplace01_Click;
            RtfReplace1.MouseClick += RtfReplace01_MouseClick;
            RtfReplace1.TextChanged += RtfReplace1_TextChanged;
            RtfReplace1.DoubleClick += RtfReplace01_DoubleClick;
            RtfReplace1.KeyDown += RtfReplace01_KeyDown;
            RtfReplace1.KeyPress += RtfReplace01_KeyPress;
            RtfReplace1.Leave += RtfReplace1_Leave;
            RtfReplace1.MouseDown += RtfReplace01_MouseDown;
            RtfReplace1.MouseMove += RtfReplace1_MouseMove;
            RtfReplace1.PreviewKeyDown += RtfReplace01_PreviewKeyDown;
            // 
            // lblReplace01
            // 
            lblReplace01.AutoSize = true;
            lblReplace01.Font = new Font("Times New Roman", 12F);
            lblReplace01.ForeColor = Color.Black;
            lblReplace01.Location = new Point(0, 117);
            lblReplace01.Name = "lblReplace01";
            lblReplace01.Size = new Size(75, 22);
            lblReplace01.TabIndex = 0;
            lblReplace01.Text = "Replace";
            // 
            // RtfReplace2
            // 
            RtfReplace2.BackColor = Color.DarkGray;
            RtfReplace2.DetectUrls = false;
            RtfReplace2.Font = new Font("Times New Roman", 13F);
            RtfReplace2.Location = new Point(81, 193);
            RtfReplace2.Multiline = false;
            RtfReplace2.Name = "RtfReplace2";
            RtfReplace2.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace2.Size = new Size(253, 35);
            RtfReplace2.TabIndex = 4;
            RtfReplace2.Text = "";
            RtfReplace2.Click += RtfReplace02_Click;
            RtfReplace2.MouseClick += RtfReplace02_MouseClick;
            RtfReplace2.TextChanged += RtfReplace2_TextChanged;
            RtfReplace2.DoubleClick += RtfReplace02_DoubleClick;
            RtfReplace2.KeyDown += RtfReplace02_KeyDown;
            RtfReplace2.KeyPress += RtfReplace02_KeyPress;
            RtfReplace2.Leave += RtfReplace2_Leave;
            RtfReplace2.MouseDown += RtfReplace02_MouseDown;
            RtfReplace2.MouseMove += RtfReplace2_MouseMove;
            RtfReplace2.PreviewKeyDown += RtfReplace02_PreviewKeyDown;
            // 
            // lblReplace02
            // 
            lblReplace02.AutoSize = true;
            lblReplace02.Font = new Font("Times New Roman", 12F);
            lblReplace02.ForeColor = Color.Black;
            lblReplace02.Location = new Point(0, 194);
            lblReplace02.Name = "lblReplace02";
            lblReplace02.Size = new Size(75, 22);
            lblReplace02.TabIndex = 0;
            lblReplace02.Text = "Replace";
            // 
            // FrmColorBtnC2
            // 
            FrmColorBtnC2.ForeColor = Color.Black;
            FrmColorBtnC2.Location = new Point(339, 157);
            FrmColorBtnC2.Name = "FrmColorBtnC2";
            FrmColorBtnC2.Size = new Size(30, 30);
            FrmColorBtnC2.TabIndex = 0;
            FrmColorBtnC2.TabStop = false;
            FrmColorBtnC2.UseVisualStyleBackColor = true;
            FrmColorBtnC2.Click += BtnC02_Click;
            // 
            // RtfFind2
            // 
            RtfFind2.BackColor = Color.DarkGray;
            RtfFind2.DetectUrls = false;
            RtfFind2.Font = new Font("Times New Roman", 13F);
            RtfFind2.Location = new Point(81, 155);
            RtfFind2.Multiline = false;
            RtfFind2.Name = "RtfFind2";
            RtfFind2.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind2.Size = new Size(253, 35);
            RtfFind2.TabIndex = 3;
            RtfFind2.Text = "";
            RtfFind2.Click += RtfFind02_Click;
            RtfFind2.MouseClick += RtfFind02_MouseClick;
            RtfFind2.TextChanged += RtfFind2_TextChanged;
            RtfFind2.DoubleClick += RtfFind02_DoubleClick;
            RtfFind2.KeyDown += RtfFind02_KeyDown;
            RtfFind2.KeyPress += RtfFind02_KeyPress;
            RtfFind2.Leave += RrtfFind2_Leave;
            RtfFind2.MouseDown += RtfFind02_MouseDown;
            RtfFind2.MouseMove += RtfFind2_MouseMove;
            RtfFind2.PreviewKeyDown += RtfFind02_PreviewKeyDown;
            // 
            // lblPipe02
            // 
            lblPipe02.AutoSize = true;
            lblPipe02.ForeColor = Color.Black;
            lblPipe02.Location = new Point(60, 152);
            lblPipe02.Name = "lblPipe02";
            lblPipe02.Size = new Size(18, 29);
            lblPipe02.TabIndex = 0;
            lblPipe02.Text = "|";
            lblPipe02.Click += LblPipe02_Click;
            // 
            // lblFind02
            // 
            lblFind02.AutoSize = true;
            lblFind02.Font = new Font("Times New Roman", 12F);
            lblFind02.ForeColor = Color.Black;
            lblFind02.Location = new Point(12, 159);
            lblFind02.Name = "lblFind02";
            lblFind02.Size = new Size(46, 22);
            lblFind02.TabIndex = 0;
            lblFind02.Text = "Find";
            // 
            // RtfReplace3
            // 
            RtfReplace3.BackColor = Color.DarkGray;
            RtfReplace3.DetectUrls = false;
            RtfReplace3.Font = new Font("Times New Roman", 13F);
            RtfReplace3.Location = new Point(81, 276);
            RtfReplace3.Multiline = false;
            RtfReplace3.Name = "RtfReplace3";
            RtfReplace3.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace3.Size = new Size(253, 35);
            RtfReplace3.TabIndex = 6;
            RtfReplace3.Text = "";
            RtfReplace3.Click += RtfReplace03_Click;
            RtfReplace3.MouseClick += RtfReplace03_MouseClick;
            RtfReplace3.TextChanged += RtfReplace3_TextChanged;
            RtfReplace3.DoubleClick += RtfReplace03_DoubleClick;
            RtfReplace3.KeyDown += RtfReplace03_KeyDown;
            RtfReplace3.KeyPress += RtfReplace03_KeyPress;
            RtfReplace3.Leave += RtfReplace3_Leave;
            RtfReplace3.MouseDown += RtfReplace03_MouseDown;
            RtfReplace3.MouseMove += RtfReplace3_MouseMove;
            RtfReplace3.PreviewKeyDown += RtfReplace03_PreviewKeyDown;
            // 
            // lblReplace03
            // 
            lblReplace03.AutoSize = true;
            lblReplace03.Font = new Font("Times New Roman", 12F);
            lblReplace03.ForeColor = Color.Black;
            lblReplace03.Location = new Point(0, 278);
            lblReplace03.Name = "lblReplace03";
            lblReplace03.Size = new Size(75, 22);
            lblReplace03.TabIndex = 0;
            lblReplace03.Text = "Replace";
            // 
            // FrmColorBtnC3
            // 
            FrmColorBtnC3.ForeColor = Color.Black;
            FrmColorBtnC3.Location = new Point(339, 240);
            FrmColorBtnC3.Name = "FrmColorBtnC3";
            FrmColorBtnC3.Size = new Size(30, 30);
            FrmColorBtnC3.TabIndex = 0;
            FrmColorBtnC3.TabStop = false;
            FrmColorBtnC3.UseVisualStyleBackColor = true;
            FrmColorBtnC3.Click += BtnC03_Click;
            // 
            // RtfFind3
            // 
            RtfFind3.BackColor = Color.DarkGray;
            RtfFind3.DetectUrls = false;
            RtfFind3.Font = new Font("Times New Roman", 13F);
            RtfFind3.Location = new Point(81, 238);
            RtfFind3.Multiline = false;
            RtfFind3.Name = "RtfFind3";
            RtfFind3.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind3.Size = new Size(253, 35);
            RtfFind3.TabIndex = 5;
            RtfFind3.Text = "";
            RtfFind3.Click += RtfFind03_Click;
            RtfFind3.MouseClick += RtfFind03_MouseClick;
            RtfFind3.TextChanged += RtfFind3_TextChanged;
            RtfFind3.DoubleClick += RtfFind03_DoubleClick;
            RtfFind3.KeyDown += RtfFind03_KeyDown;
            RtfFind3.KeyPress += RtfFind03_KeyPress;
            RtfFind3.Leave += RtfFind3_Leave;
            RtfFind3.MouseDown += RtfFind03_MouseDown;
            RtfFind3.MouseMove += RtfFind3_MouseMove;
            RtfFind3.PreviewKeyDown += RtfFind03_PreviewKeyDown;
            // 
            // lblPipe03
            // 
            lblPipe03.AutoSize = true;
            lblPipe03.ForeColor = Color.Black;
            lblPipe03.Location = new Point(60, 237);
            lblPipe03.Name = "lblPipe03";
            lblPipe03.Size = new Size(18, 29);
            lblPipe03.TabIndex = 0;
            lblPipe03.Text = "|";
            lblPipe03.Click += LblPipe03_Click;
            // 
            // lblFind03
            // 
            lblFind03.AutoSize = true;
            lblFind03.Font = new Font("Times New Roman", 12F);
            lblFind03.ForeColor = Color.Black;
            lblFind03.Location = new Point(12, 244);
            lblFind03.Name = "lblFind03";
            lblFind03.Size = new Size(46, 22);
            lblFind03.TabIndex = 0;
            lblFind03.Text = "Find";
            // 
            // RtfReplace4
            // 
            RtfReplace4.BackColor = Color.DarkGray;
            RtfReplace4.DetectUrls = false;
            RtfReplace4.Font = new Font("Times New Roman", 13F);
            RtfReplace4.Location = new Point(81, 359);
            RtfReplace4.Multiline = false;
            RtfReplace4.Name = "RtfReplace4";
            RtfReplace4.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace4.Size = new Size(253, 35);
            RtfReplace4.TabIndex = 8;
            RtfReplace4.Text = "";
            RtfReplace4.Click += RtfReplace04_Click;
            RtfReplace4.MouseClick += RtfReplace04_MouseClick;
            RtfReplace4.TextChanged += RtfReplace4_TextChanged;
            RtfReplace4.DoubleClick += RtfReplace04_DoubleClick;
            RtfReplace4.KeyDown += RtfReplace04_KeyDown;
            RtfReplace4.KeyPress += RtfReplace04_KeyPress;
            RtfReplace4.Leave += RtfReplace4_Leave;
            RtfReplace4.MouseDown += RtfReplace04_MouseDown;
            RtfReplace4.MouseMove += RtfReplace4_MouseMove;
            RtfReplace4.PreviewKeyDown += RtfReplace04_PreviewKeyDown;
            // 
            // lblReplace04
            // 
            lblReplace04.AutoSize = true;
            lblReplace04.Font = new Font("Times New Roman", 12F);
            lblReplace04.ForeColor = Color.Black;
            lblReplace04.Location = new Point(0, 365);
            lblReplace04.Name = "lblReplace04";
            lblReplace04.Size = new Size(75, 22);
            lblReplace04.TabIndex = 0;
            lblReplace04.Text = "Replace";
            // 
            // FrmColorBtnC4
            // 
            FrmColorBtnC4.ForeColor = Color.Black;
            FrmColorBtnC4.Location = new Point(339, 323);
            FrmColorBtnC4.Name = "FrmColorBtnC4";
            FrmColorBtnC4.Size = new Size(30, 30);
            FrmColorBtnC4.TabIndex = 0;
            FrmColorBtnC4.TabStop = false;
            FrmColorBtnC4.UseVisualStyleBackColor = true;
            FrmColorBtnC4.Click += BtnC04_Click;
            // 
            // RtfFind4
            // 
            RtfFind4.BackColor = Color.DarkGray;
            RtfFind4.DetectUrls = false;
            RtfFind4.Font = new Font("Times New Roman", 13F);
            RtfFind4.Location = new Point(81, 321);
            RtfFind4.Multiline = false;
            RtfFind4.Name = "RtfFind4";
            RtfFind4.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind4.Size = new Size(253, 35);
            RtfFind4.TabIndex = 7;
            RtfFind4.Text = "";
            RtfFind4.Click += RtfFind04_Click;
            RtfFind4.MouseClick += RtfFind04_MouseClick;
            RtfFind4.TextChanged += RtfFind4_TextChanged;
            RtfFind4.DoubleClick += RtfFind04_DoubleClick;
            RtfFind4.KeyDown += RtfFind04_KeyDown;
            RtfFind4.KeyPress += RtfFind04_KeyPress;
            RtfFind4.Leave += RtfFind4_Leave;
            RtfFind4.MouseDown += RtfFind04_MouseDown;
            RtfFind4.MouseMove += RtfFind4_MouseMove;
            RtfFind4.PreviewKeyDown += RtfFind04_PreviewKeyDown;
            // 
            // lblPipe04
            // 
            lblPipe04.AutoSize = true;
            lblPipe04.ForeColor = Color.Black;
            lblPipe04.Location = new Point(60, 319);
            lblPipe04.Name = "lblPipe04";
            lblPipe04.Size = new Size(18, 29);
            lblPipe04.TabIndex = 0;
            lblPipe04.Text = "|";
            lblPipe04.Click += LblPipe04_Click;
            // 
            // lblFind04
            // 
            lblFind04.AutoSize = true;
            lblFind04.Font = new Font("Times New Roman", 12F);
            lblFind04.ForeColor = Color.Black;
            lblFind04.Location = new Point(12, 327);
            lblFind04.Name = "lblFind04";
            lblFind04.Size = new Size(46, 22);
            lblFind04.TabIndex = 0;
            lblFind04.Text = "Find";
            // 
            // RtfReplace5
            // 
            RtfReplace5.BackColor = Color.DarkGray;
            RtfReplace5.DetectUrls = false;
            RtfReplace5.Font = new Font("Times New Roman", 13F);
            RtfReplace5.Location = new Point(81, 443);
            RtfReplace5.Multiline = false;
            RtfReplace5.Name = "RtfReplace5";
            RtfReplace5.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace5.Size = new Size(253, 35);
            RtfReplace5.TabIndex = 10;
            RtfReplace5.Text = "";
            RtfReplace5.Click += RtfReplace05_Click;
            RtfReplace5.MouseClick += RtfReplace05_MouseClick;
            RtfReplace5.TextChanged += RtfReplace5_TextChanged;
            RtfReplace5.DoubleClick += RtfReplace05_DoubleClick;
            RtfReplace5.KeyDown += RtfReplace05_KeyDown;
            RtfReplace5.KeyPress += RtfReplace05_KeyPress;
            RtfReplace5.Leave += RtfReplace5_Leave;
            RtfReplace5.MouseDown += RtfReplace05_MouseDown;
            RtfReplace5.MouseMove += RtfReplace5_MouseMove;
            RtfReplace5.PreviewKeyDown += RtfReplace05_PreviewKeyDown;
            // 
            // lblReplace05
            // 
            lblReplace05.AutoSize = true;
            lblReplace05.Font = new Font("Times New Roman", 12F);
            lblReplace05.ForeColor = Color.Black;
            lblReplace05.Location = new Point(0, 449);
            lblReplace05.Name = "lblReplace05";
            lblReplace05.Size = new Size(75, 22);
            lblReplace05.TabIndex = 0;
            lblReplace05.Text = "Replace";
            // 
            // FrmColorBtnC5
            // 
            FrmColorBtnC5.ForeColor = Color.Black;
            FrmColorBtnC5.Location = new Point(339, 406);
            FrmColorBtnC5.Name = "FrmColorBtnC5";
            FrmColorBtnC5.Size = new Size(30, 30);
            FrmColorBtnC5.TabIndex = 0;
            FrmColorBtnC5.TabStop = false;
            FrmColorBtnC5.UseVisualStyleBackColor = true;
            FrmColorBtnC5.Click += BtnC05_Click;
            // 
            // RtfFind5
            // 
            RtfFind5.BackColor = Color.DarkGray;
            RtfFind5.DetectUrls = false;
            RtfFind5.Font = new Font("Times New Roman", 13F);
            RtfFind5.Location = new Point(81, 404);
            RtfFind5.Multiline = false;
            RtfFind5.Name = "RtfFind5";
            RtfFind5.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind5.Size = new Size(253, 35);
            RtfFind5.TabIndex = 9;
            RtfFind5.Text = "";
            RtfFind5.Click += RtfFind05_Click;
            RtfFind5.MouseClick += RtfFind05_MouseClick;
            RtfFind5.TextChanged += RtfFind5_TextChanged;
            RtfFind5.DoubleClick += RtfFind05_DoubleClick;
            RtfFind5.KeyDown += RtfFind05_KeyDown;
            RtfFind5.KeyPress += RtfFind05_KeyPress;
            RtfFind5.Leave += RtfFind5_Leave;
            RtfFind5.MouseDown += RtfFind05_MouseDown;
            RtfFind5.MouseMove += RtfFind5_MouseMove;
            RtfFind5.PreviewKeyDown += RtfFind05_PreviewKeyDown;
            // 
            // lblPipe05
            // 
            lblPipe05.AutoSize = true;
            lblPipe05.ForeColor = Color.Black;
            lblPipe05.Location = new Point(60, 401);
            lblPipe05.Name = "lblPipe05";
            lblPipe05.Size = new Size(18, 29);
            lblPipe05.TabIndex = 0;
            lblPipe05.Text = "|";
            lblPipe05.Click += LblPipe05_Click;
            // 
            // lblFind05
            // 
            lblFind05.AutoSize = true;
            lblFind05.Font = new Font("Times New Roman", 12F);
            lblFind05.ForeColor = Color.Black;
            lblFind05.Location = new Point(12, 409);
            lblFind05.Name = "lblFind05";
            lblFind05.Size = new Size(46, 22);
            lblFind05.TabIndex = 0;
            lblFind05.Text = "Find";
            // 
            // RtfReplace6
            // 
            RtfReplace6.BackColor = Color.DarkGray;
            RtfReplace6.DetectUrls = false;
            RtfReplace6.Font = new Font("Times New Roman", 13F);
            RtfReplace6.Location = new Point(81, 526);
            RtfReplace6.Multiline = false;
            RtfReplace6.Name = "RtfReplace6";
            RtfReplace6.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace6.Size = new Size(253, 35);
            RtfReplace6.TabIndex = 12;
            RtfReplace6.Text = "";
            RtfReplace6.Click += RtfReplace06_Click;
            RtfReplace6.MouseClick += RtfReplace06_MouseClick;
            RtfReplace6.TextChanged += RtfReplace6_TextChanged;
            RtfReplace6.DoubleClick += RtfReplace06_DoubleClick;
            RtfReplace6.KeyDown += RtfReplace06_KeyDown;
            RtfReplace6.KeyPress += RtfReplace06_KeyPress;
            RtfReplace6.Leave += RtfReplace6_Leave;
            RtfReplace6.MouseDown += RtfReplace06_MouseDown;
            RtfReplace6.MouseMove += RtfReplace6_MouseMove;
            RtfReplace6.PreviewKeyDown += RtfReplace06_PreviewKeyDown;
            // 
            // lblReplace06
            // 
            lblReplace06.AutoSize = true;
            lblReplace06.Font = new Font("Times New Roman", 12F);
            lblReplace06.ForeColor = Color.Black;
            lblReplace06.Location = new Point(0, 515);
            lblReplace06.Name = "lblReplace06";
            lblReplace06.Size = new Size(75, 22);
            lblReplace06.TabIndex = 0;
            lblReplace06.Text = "Replace";
            // 
            // FrmColorBtnC6
            // 
            FrmColorBtnC6.ForeColor = Color.Black;
            FrmColorBtnC6.Location = new Point(339, 490);
            FrmColorBtnC6.Name = "FrmColorBtnC6";
            FrmColorBtnC6.Size = new Size(30, 30);
            FrmColorBtnC6.TabIndex = 0;
            FrmColorBtnC6.TabStop = false;
            FrmColorBtnC6.UseVisualStyleBackColor = true;
            FrmColorBtnC6.Click += BtnC06_Click;
            // 
            // RtfFind6
            // 
            RtfFind6.BackColor = Color.DarkGray;
            RtfFind6.DetectUrls = false;
            RtfFind6.Font = new Font("Times New Roman", 13F);
            RtfFind6.Location = new Point(81, 488);
            RtfFind6.Multiline = false;
            RtfFind6.Name = "RtfFind6";
            RtfFind6.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind6.Size = new Size(253, 35);
            RtfFind6.TabIndex = 11;
            RtfFind6.Text = "";
            RtfFind6.Click += RtfFind06_Click;
            RtfFind6.MouseClick += RtfFind06_MouseClick;
            RtfFind6.TextChanged += RtfFind6_TextChanged;
            RtfFind6.DoubleClick += RtfFind06_DoubleClick;
            RtfFind6.KeyDown += RtfFind06_KeyDown;
            RtfFind6.KeyPress += RtfFind06_KeyPress;
            RtfFind6.Leave += RtfFind6_Leave;
            RtfFind6.MouseDown += RtfFind06_MouseDown;
            RtfFind6.MouseMove += RtfFind6_MouseMove;
            RtfFind6.PreviewKeyDown += RtfFind06_PreviewKeyDown;
            // 
            // lblPipe06
            // 
            lblPipe06.AutoSize = true;
            lblPipe06.ForeColor = Color.Black;
            lblPipe06.Location = new Point(60, 485);
            lblPipe06.Name = "lblPipe06";
            lblPipe06.Size = new Size(18, 29);
            lblPipe06.TabIndex = 0;
            lblPipe06.Text = "|";
            lblPipe06.Click += LblPipe06_Click;
            // 
            // lblFind06
            // 
            lblFind06.AutoSize = true;
            lblFind06.Font = new Font("Times New Roman", 12F);
            lblFind06.ForeColor = Color.Black;
            lblFind06.Location = new Point(12, 493);
            lblFind06.Name = "lblFind06";
            lblFind06.Size = new Size(46, 22);
            lblFind06.TabIndex = 0;
            lblFind06.Text = "Find";
            // 
            // RtfReplace7
            // 
            RtfReplace7.BackColor = Color.DarkGray;
            RtfReplace7.DetectUrls = false;
            RtfReplace7.Font = new Font("Times New Roman", 13F);
            RtfReplace7.Location = new Point(81, 609);
            RtfReplace7.Multiline = false;
            RtfReplace7.Name = "RtfReplace7";
            RtfReplace7.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace7.Size = new Size(253, 35);
            RtfReplace7.TabIndex = 14;
            RtfReplace7.Text = "";
            RtfReplace7.Click += RtfReplace07_Click;
            RtfReplace7.MouseClick += RtfReplace07_MouseClick;
            RtfReplace7.TextChanged += RtfReplace7_TextChanged;
            RtfReplace7.DoubleClick += RtfReplace07_DoubleClick;
            RtfReplace7.KeyDown += RtfReplace07_KeyDown;
            RtfReplace7.KeyPress += RtfReplace07_KeyPress;
            RtfReplace7.Leave += RtfReplace7_Leave;
            RtfReplace7.MouseDown += RtfReplace07_MouseDown;
            RtfReplace7.MouseMove += RtfReplace7_MouseMove;
            RtfReplace7.PreviewKeyDown += RtfReplace07_PreviewKeyDown;
            // 
            // lblReplace07
            // 
            lblReplace07.AutoSize = true;
            lblReplace07.Font = new Font("Times New Roman", 12F);
            lblReplace07.ForeColor = Color.Black;
            lblReplace07.Location = new Point(0, 605);
            lblReplace07.Name = "lblReplace07";
            lblReplace07.Size = new Size(75, 22);
            lblReplace07.TabIndex = 0;
            lblReplace07.Text = "Replace";
            // 
            // FrmColorBtnC7
            // 
            FrmColorBtnC7.ForeColor = Color.Black;
            FrmColorBtnC7.Location = new Point(339, 573);
            FrmColorBtnC7.Name = "FrmColorBtnC7";
            FrmColorBtnC7.Size = new Size(30, 30);
            FrmColorBtnC7.TabIndex = 0;
            FrmColorBtnC7.TabStop = false;
            FrmColorBtnC7.UseVisualStyleBackColor = true;
            FrmColorBtnC7.Click += BtnC07_Click;
            // 
            // RtfFind7
            // 
            RtfFind7.BackColor = Color.DarkGray;
            RtfFind7.DetectUrls = false;
            RtfFind7.Font = new Font("Times New Roman", 13F);
            RtfFind7.Location = new Point(81, 571);
            RtfFind7.Multiline = false;
            RtfFind7.Name = "RtfFind7";
            RtfFind7.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind7.Size = new Size(253, 35);
            RtfFind7.TabIndex = 13;
            RtfFind7.Text = "";
            RtfFind7.Click += RtfFind07_Click;
            RtfFind7.MouseClick += RtfFind07_MouseClick;
            RtfFind7.TextChanged += RtfFind7_TextChanged;
            RtfFind7.DoubleClick += RtfFind07_DoubleClick;
            RtfFind7.KeyDown += RtfFind07_KeyDown;
            RtfFind7.KeyPress += RtfFind07_KeyPress;
            RtfFind7.Leave += RtfFind7_Leave;
            RtfFind7.MouseDown += RtfFind07_MouseDown;
            RtfFind7.MouseMove += RtfFind7_MouseMove;
            RtfFind7.PreviewKeyDown += RtfFind07_PreviewKeyDown;
            // 
            // lblPipe07
            // 
            lblPipe07.AutoSize = true;
            lblPipe07.ForeColor = Color.Black;
            lblPipe07.Location = new Point(60, 568);
            lblPipe07.Name = "lblPipe07";
            lblPipe07.Size = new Size(18, 29);
            lblPipe07.TabIndex = 0;
            lblPipe07.Text = "|";
            lblPipe07.Click += LblPipe07_Click;
            // 
            // lblFind07
            // 
            lblFind07.AutoSize = true;
            lblFind07.Font = new Font("Times New Roman", 12F);
            lblFind07.ForeColor = Color.Black;
            lblFind07.Location = new Point(12, 576);
            lblFind07.Name = "lblFind07";
            lblFind07.Size = new Size(46, 22);
            lblFind07.TabIndex = 0;
            lblFind07.Text = "Find";
            // 
            // RtfReplace8
            // 
            RtfReplace8.BackColor = Color.DarkGray;
            RtfReplace8.DetectUrls = false;
            RtfReplace8.Font = new Font("Times New Roman", 13F);
            RtfReplace8.Location = new Point(81, 692);
            RtfReplace8.Multiline = false;
            RtfReplace8.Name = "RtfReplace8";
            RtfReplace8.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace8.Size = new Size(253, 35);
            RtfReplace8.TabIndex = 16;
            RtfReplace8.Text = "";
            RtfReplace8.Click += RtfReplace08_Click;
            RtfReplace8.MouseClick += RtfReplace08_MouseClick;
            RtfReplace8.TextChanged += RtfReplace8_TextChanged;
            RtfReplace8.DoubleClick += RtfReplace08_DoubleClick;
            RtfReplace8.KeyDown += RtfReplace08_KeyDown;
            RtfReplace8.KeyPress += RtfReplace08_KeyPress;
            RtfReplace8.Leave += RtfReplace8_Leave;
            RtfReplace8.MouseDown += RtfReplace08_MouseDown;
            RtfReplace8.MouseMove += RtfReplace8_MouseMove;
            RtfReplace8.PreviewKeyDown += RtfReplace08_PreviewKeyDown;
            // 
            // lblReplace08
            // 
            lblReplace08.AutoSize = true;
            lblReplace08.Font = new Font("Times New Roman", 12F);
            lblReplace08.ForeColor = Color.Black;
            lblReplace08.Location = new Point(0, 698);
            lblReplace08.Name = "lblReplace08";
            lblReplace08.Size = new Size(75, 22);
            lblReplace08.TabIndex = 0;
            lblReplace08.Text = "Replace";
            // 
            // FrmColorBtnC8
            // 
            FrmColorBtnC8.ForeColor = Color.Black;
            FrmColorBtnC8.Location = new Point(339, 656);
            FrmColorBtnC8.Name = "FrmColorBtnC8";
            FrmColorBtnC8.Size = new Size(30, 30);
            FrmColorBtnC8.TabIndex = 0;
            FrmColorBtnC8.TabStop = false;
            FrmColorBtnC8.UseVisualStyleBackColor = true;
            FrmColorBtnC8.Click += BtnC08_Click;
            // 
            // RtfFind8
            // 
            RtfFind8.BackColor = Color.DarkGray;
            RtfFind8.DetectUrls = false;
            RtfFind8.Font = new Font("Times New Roman", 13F);
            RtfFind8.Location = new Point(81, 654);
            RtfFind8.Multiline = false;
            RtfFind8.Name = "RtfFind8";
            RtfFind8.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind8.Size = new Size(253, 35);
            RtfFind8.TabIndex = 15;
            RtfFind8.Text = "";
            RtfFind8.Click += RtfFind08_Click;
            RtfFind8.MouseClick += RtfFind08_MouseClick;
            RtfFind8.TextChanged += RtfFind8_TextChanged;
            RtfFind8.DoubleClick += RtfFind08_DoubleClick;
            RtfFind8.KeyDown += RtfFind08_KeyDown;
            RtfFind8.KeyPress += RtfFind08_KeyPress;
            RtfFind8.Leave += RtfFind8_Leave;
            RtfFind8.MouseDown += RtfFind08_MouseDown;
            RtfFind8.MouseMove += RtfFind8_MouseMove;
            RtfFind8.PreviewKeyDown += RtfFind08_PreviewKeyDown;
            // 
            // lblPipe08
            // 
            lblPipe08.AutoSize = true;
            lblPipe08.ForeColor = Color.Black;
            lblPipe08.Location = new Point(60, 658);
            lblPipe08.Name = "lblPipe08";
            lblPipe08.Size = new Size(18, 29);
            lblPipe08.TabIndex = 0;
            lblPipe08.Text = "|";
            lblPipe08.Click += LlblPipe08_Click;
            // 
            // lblFind08
            // 
            lblFind08.AutoSize = true;
            lblFind08.Font = new Font("Times New Roman", 12F);
            lblFind08.ForeColor = Color.Black;
            lblFind08.Location = new Point(12, 666);
            lblFind08.Name = "lblFind08";
            lblFind08.Size = new Size(46, 22);
            lblFind08.TabIndex = 0;
            lblFind08.Text = "Find";
            // 
            // btnReplace_All
            // 
            btnReplace_All.BackColor = Color.Wheat;
            btnReplace_All.Font = new Font("Times New Roman", 13F);
            btnReplace_All.ForeColor = Color.Black;
            btnReplace_All.Location = new Point(388, 138);
            btnReplace_All.Name = "btnReplace_All";
            btnReplace_All.Size = new Size(124, 32);
            btnReplace_All.TabIndex = 0;
            btnReplace_All.TabStop = false;
            btnReplace_All.Text = "Replace All";
            btnReplace_All.UseVisualStyleBackColor = false;
            btnReplace_All.Click += BtnReplace_All_Click;
            // 
            // btnGetSearch
            // 
            btnGetSearch.BackColor = Color.LightGreen;
            btnGetSearch.Font = new Font("Times New Roman", 13F);
            btnGetSearch.ForeColor = Color.Black;
            btnGetSearch.Location = new Point(388, 69);
            btnGetSearch.Name = "btnGetSearch";
            btnGetSearch.Size = new Size(124, 32);
            btnGetSearch.TabIndex = 0;
            btnGetSearch.TabStop = false;
            btnGetSearch.Text = "Get Search";
            btnGetSearch.UseVisualStyleBackColor = false;
            btnGetSearch.Click += BtnGetSearch_Click;
            // 
            // btnSaveSearch
            // 
            btnSaveSearch.BackColor = Color.LightGreen;
            btnSaveSearch.Font = new Font("Times New Roman", 13F);
            btnSaveSearch.ForeColor = Color.Black;
            btnSaveSearch.Location = new Point(388, 103);
            btnSaveSearch.Name = "btnSaveSearch";
            btnSaveSearch.Size = new Size(124, 32);
            btnSaveSearch.TabIndex = 0;
            btnSaveSearch.TabStop = false;
            btnSaveSearch.Text = "Save Search";
            btnSaveSearch.UseVisualStyleBackColor = false;
            btnSaveSearch.Click += BtnSaveSearch_Click;
            // 
            // btnPasteInto
            // 
            btnPasteInto.BackColor = Color.LightSkyBlue;
            btnPasteInto.Font = new Font("Times New Roman", 13F);
            btnPasteInto.ForeColor = Color.Black;
            btnPasteInto.Location = new Point(388, 244);
            btnPasteInto.Name = "btnPasteInto";
            btnPasteInto.Size = new Size(124, 32);
            btnPasteInto.TabIndex = 0;
            btnPasteInto.TabStop = false;
            btnPasteInto.Text = "Paste Into";
            toolTip1.SetToolTip(btnPasteInto, "              Paste key into main text at cursor location");
            btnPasteInto.UseVisualStyleBackColor = false;
            btnPasteInto.Click += BtnPasteInto_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.BackColor = Color.Gold;
            btnClearAll.Font = new Font("Times New Roman", 13F);
            btnClearAll.ForeColor = Color.Black;
            btnClearAll.Location = new Point(388, 397);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(124, 30);
            btnClearAll.TabIndex = 0;
            btnClearAll.TabStop = false;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = false;
            btnClearAll.Click += BtnClearAll_Click;
            // 
            // ChkMatchCase
            // 
            ChkMatchCase.AutoSize = true;
            ChkMatchCase.Font = new Font("Times New Roman", 11F);
            ChkMatchCase.ForeColor = Color.Black;
            ChkMatchCase.Location = new Point(5, 8);
            ChkMatchCase.Name = "ChkMatchCase";
            ChkMatchCase.Size = new Size(121, 25);
            ChkMatchCase.TabIndex = 0;
            ChkMatchCase.TabStop = false;
            ChkMatchCase.Text = "Match Case";
            ChkMatchCase.UseVisualStyleBackColor = true;
            ChkMatchCase.CheckedChanged += ChkMatchCase_CheckedChanged;
            ChkMatchCase.Click += ChkMatchCase_Click;
            // 
            // btnClearFinds
            // 
            btnClearFinds.BackColor = Color.Gold;
            btnClearFinds.Font = new Font("Times New Roman", 13F);
            btnClearFinds.ForeColor = Color.Black;
            btnClearFinds.Location = new Point(388, 429);
            btnClearFinds.Name = "btnClearFinds";
            btnClearFinds.Size = new Size(124, 30);
            btnClearFinds.TabIndex = 0;
            btnClearFinds.TabStop = false;
            btnClearFinds.Text = "Clear Find";
            btnClearFinds.UseVisualStyleBackColor = false;
            btnClearFinds.Click += BtnClearFinds_Click;
            // 
            // btnClearReplace
            // 
            btnClearReplace.BackColor = Color.Gold;
            btnClearReplace.Font = new Font("Times New Roman", 13F);
            btnClearReplace.ForeColor = Color.Black;
            btnClearReplace.Location = new Point(388, 462);
            btnClearReplace.Name = "btnClearReplace";
            btnClearReplace.Size = new Size(124, 30);
            btnClearReplace.TabIndex = 0;
            btnClearReplace.TabStop = false;
            btnClearReplace.Text = "Clear Replace";
            btnClearReplace.UseVisualStyleBackColor = false;
            btnClearReplace.Click += BtnClearReplace_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Times New Roman", 13F);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(418, 2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(102, 30);
            btnClear.TabIndex = 0;
            btnClear.TabStop = false;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            // 
            // RtfReplace9
            // 
            RtfReplace9.BackColor = Color.DarkGray;
            RtfReplace9.DetectUrls = false;
            RtfReplace9.Font = new Font("Times New Roman", 13F);
            RtfReplace9.Location = new Point(81, 777);
            RtfReplace9.Multiline = false;
            RtfReplace9.Name = "RtfReplace9";
            RtfReplace9.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace9.Size = new Size(253, 35);
            RtfReplace9.TabIndex = 18;
            RtfReplace9.Text = "";
            RtfReplace9.Click += RtfReplace09_Click;
            RtfReplace9.MouseClick += RtfReplace09_MouseClick;
            RtfReplace9.TextChanged += RtfReplace9_TextChanged;
            RtfReplace9.DoubleClick += RtfReplace09_DoubleClick;
            RtfReplace9.KeyDown += RtfReplace09_KeyDown;
            RtfReplace9.KeyPress += RtfReplace09_KeyPress;
            RtfReplace9.Leave += RtfReplace9_Leave;
            RtfReplace9.MouseDown += RtfReplace09_MouseDown;
            RtfReplace9.MouseMove += RtfReplace9_MouseMove;
            RtfReplace9.PreviewKeyDown += RtfReplace09_PreviewKeyDown;
            // 
            // lblReplace09
            // 
            lblReplace09.AutoSize = true;
            lblReplace09.Font = new Font("Times New Roman", 12F);
            lblReplace09.ForeColor = Color.Black;
            lblReplace09.Location = new Point(0, 781);
            lblReplace09.Name = "lblReplace09";
            lblReplace09.Size = new Size(75, 22);
            lblReplace09.TabIndex = 0;
            lblReplace09.Text = "Replace";
            // 
            // FrmColorBtnC9
            // 
            FrmColorBtnC9.ForeColor = Color.Black;
            FrmColorBtnC9.Location = new Point(339, 739);
            FrmColorBtnC9.Name = "FrmColorBtnC9";
            FrmColorBtnC9.Size = new Size(30, 30);
            FrmColorBtnC9.TabIndex = 0;
            FrmColorBtnC9.TabStop = false;
            FrmColorBtnC9.UseVisualStyleBackColor = true;
            FrmColorBtnC9.Click += BtnC09_Click;
            // 
            // RtfFind9
            // 
            RtfFind9.BackColor = Color.DarkGray;
            RtfFind9.DetectUrls = false;
            RtfFind9.Font = new Font("Times New Roman", 13F);
            RtfFind9.Location = new Point(81, 737);
            RtfFind9.Multiline = false;
            RtfFind9.Name = "RtfFind9";
            RtfFind9.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind9.Size = new Size(253, 35);
            RtfFind9.TabIndex = 17;
            RtfFind9.Text = "";
            RtfFind9.Click += RtfFind09_Click;
            RtfFind9.MouseClick += RtfFind09_MouseClick;
            RtfFind9.TextChanged += RtfFind9_TextChanged;
            RtfFind9.DoubleClick += RtfFind09_DoubleClick;
            RtfFind9.KeyDown += RtfFind09_KeyDown;
            RtfFind9.KeyPress += RtfFind09_KeyPress;
            RtfFind9.Leave += RtfFind9_Leave;
            RtfFind9.MouseDown += RtfFind09_MouseDown;
            RtfFind9.MouseMove += RtfFind9_MouseMove;
            RtfFind9.PreviewKeyDown += RtfFind09_PreviewKeyDown;
            // 
            // lblPipe09
            // 
            lblPipe09.AutoSize = true;
            lblPipe09.ForeColor = Color.Black;
            lblPipe09.Location = new Point(60, 735);
            lblPipe09.Name = "lblPipe09";
            lblPipe09.Size = new Size(18, 29);
            lblPipe09.TabIndex = 0;
            lblPipe09.Text = "|";
            lblPipe09.Click += LblPipe09_Click;
            // 
            // lblFind09
            // 
            lblFind09.AutoSize = true;
            lblFind09.Font = new Font("Times New Roman", 12F);
            lblFind09.ForeColor = Color.Black;
            lblFind09.Location = new Point(12, 743);
            lblFind09.Name = "lblFind09";
            lblFind09.Size = new Size(46, 22);
            lblFind09.TabIndex = 0;
            lblFind09.Text = "Find";
            // 
            // RtfReplace10
            // 
            RtfReplace10.BackColor = Color.DarkGray;
            RtfReplace10.DetectUrls = false;
            RtfReplace10.Font = new Font("Times New Roman", 13F);
            RtfReplace10.Location = new Point(81, 860);
            RtfReplace10.Multiline = false;
            RtfReplace10.Name = "RtfReplace10";
            RtfReplace10.ScrollBars = RichTextBoxScrollBars.None;
            RtfReplace10.Size = new Size(253, 35);
            RtfReplace10.TabIndex = 20;
            RtfReplace10.Text = "";
            RtfReplace10.Click += RtfReplace10_Click;
            RtfReplace10.MouseClick += RtfReplace10_MouseClick;
            RtfReplace10.TextChanged += RtfReplace10_TextChanged;
            RtfReplace10.DoubleClick += RtfReplace10_DoubleClick;
            RtfReplace10.KeyDown += RtfReplace10_KeyDown;
            RtfReplace10.KeyPress += RtfReplace10_KeyPress;
            RtfReplace10.Leave += RtfReplace10_Leave;
            RtfReplace10.MouseDown += RtfReplace10_MouseDown;
            RtfReplace10.MouseMove += RtfReplace10_MouseMove;
            RtfReplace10.PreviewKeyDown += RtfReplace10_PreviewKeyDown;
            // 
            // lblReplace10
            // 
            lblReplace10.AutoSize = true;
            lblReplace10.Font = new Font("Times New Roman", 12F);
            lblReplace10.ForeColor = Color.Black;
            lblReplace10.Location = new Point(0, 865);
            lblReplace10.Name = "lblReplace10";
            lblReplace10.Size = new Size(75, 22);
            lblReplace10.TabIndex = 0;
            lblReplace10.Text = "Replace";
            // 
            // FrmColorBtnC10
            // 
            FrmColorBtnC10.ForeColor = Color.Black;
            FrmColorBtnC10.Location = new Point(339, 824);
            FrmColorBtnC10.Name = "FrmColorBtnC10";
            FrmColorBtnC10.Size = new Size(30, 30);
            FrmColorBtnC10.TabIndex = 0;
            FrmColorBtnC10.TabStop = false;
            FrmColorBtnC10.UseVisualStyleBackColor = true;
            FrmColorBtnC10.Click += BtnC10_Click;
            // 
            // RtfFind10
            // 
            RtfFind10.BackColor = Color.DarkGray;
            RtfFind10.DetectUrls = false;
            RtfFind10.Font = new Font("Times New Roman", 13F);
            RtfFind10.Location = new Point(81, 822);
            RtfFind10.Multiline = false;
            RtfFind10.Name = "RtfFind10";
            RtfFind10.ScrollBars = RichTextBoxScrollBars.None;
            RtfFind10.Size = new Size(253, 35);
            RtfFind10.TabIndex = 19;
            RtfFind10.Text = "";
            RtfFind10.Click += RtfFind10_Click;
            RtfFind10.MouseClick += RtfFind10_MouseClick;
            RtfFind10.TextChanged += RtfFind10_TextChanged;
            RtfFind10.DoubleClick += RtfFind10_DoubleClick;
            RtfFind10.KeyDown += RtfFind10_KeyDown;
            RtfFind10.KeyPress += RtfFind10_KeyPress;
            RtfFind10.Leave += RtfFind10_Leave;
            RtfFind10.MouseDown += RtfFind10_MouseDown;
            RtfFind10.MouseMove += RtfFind10_MouseMove;
            RtfFind10.PreviewKeyDown += RtfFind10_PreviewKeyDown;
            // 
            // lblPipe10
            // 
            lblPipe10.AutoSize = true;
            lblPipe10.ForeColor = Color.Black;
            lblPipe10.Location = new Point(60, 821);
            lblPipe10.Name = "lblPipe10";
            lblPipe10.Size = new Size(18, 29);
            lblPipe10.TabIndex = 0;
            lblPipe10.Text = "|";
            lblPipe10.Click += LblPipe10_Click;
            // 
            // lblFind10
            // 
            lblFind10.AutoSize = true;
            lblFind10.Font = new Font("Times New Roman", 12F);
            lblFind10.ForeColor = Color.Black;
            lblFind10.Location = new Point(12, 825);
            lblFind10.Name = "lblFind10";
            lblFind10.Size = new Size(46, 22);
            lblFind10.TabIndex = 0;
            lblFind10.Text = "Find";
            // 
            // btnPrefixReplace
            // 
            btnPrefixReplace.BackColor = Color.Wheat;
            btnPrefixReplace.Font = new Font("Times New Roman", 13F);
            btnPrefixReplace.ForeColor = Color.Black;
            btnPrefixReplace.Location = new Point(388, 173);
            btnPrefixReplace.Name = "btnPrefixReplace";
            btnPrefixReplace.Size = new Size(124, 32);
            btnPrefixReplace.TabIndex = 0;
            btnPrefixReplace.TabStop = false;
            btnPrefixReplace.Text = "Prefix Replace";
            toolTip1.SetToolTip(btnPrefixReplace, "Replace only word beginnings");
            btnPrefixReplace.UseVisualStyleBackColor = false;
            btnPrefixReplace.Click += BtnReplacePrefix_Click;
            // 
            // btnSuffixReplace
            // 
            btnSuffixReplace.BackColor = Color.Wheat;
            btnSuffixReplace.Font = new Font("Times New Roman", 13F);
            btnSuffixReplace.ForeColor = Color.Black;
            btnSuffixReplace.Location = new Point(388, 208);
            btnSuffixReplace.Name = "btnSuffixReplace";
            btnSuffixReplace.Size = new Size(124, 32);
            btnSuffixReplace.TabIndex = 0;
            btnSuffixReplace.TabStop = false;
            btnSuffixReplace.Text = "Suffix Replace";
            toolTip1.SetToolTip(btnSuffixReplace, "Replace only word endings");
            btnSuffixReplace.UseVisualStyleBackColor = false;
            btnSuffixReplace.Click += BtnReplaceSuffix_Click;
            // 
            // BtnUndo
            // 
            BtnUndo.BackColor = Color.Gold;
            BtnUndo.Font = new Font("Times New Roman", 13F);
            BtnUndo.ForeColor = Color.Black;
            BtnUndo.Location = new Point(388, 365);
            BtnUndo.Name = "BtnUndo";
            BtnUndo.Size = new Size(124, 30);
            BtnUndo.TabIndex = 22;
            BtnUndo.TabStop = false;
            BtnUndo.Text = "Undo";
            toolTip1.SetToolTip(BtnUndo, "             Undo previous action");
            BtnUndo.UseVisualStyleBackColor = false;
            BtnUndo.Click += BtnUndo_Click;
            // 
            // BtnDefaultColors
            // 
            BtnDefaultColors.BackColor = Color.LightSkyBlue;
            BtnDefaultColors.Font = new Font("Times New Roman", 13F);
            BtnDefaultColors.ForeColor = Color.Black;
            BtnDefaultColors.Location = new Point(385, 572);
            BtnDefaultColors.Name = "BtnDefaultColors";
            BtnDefaultColors.Size = new Size(163, 32);
            BtnDefaultColors.TabIndex = 25;
            BtnDefaultColors.TabStop = false;
            BtnDefaultColors.Text = "Default Colors";
            toolTip1.SetToolTip(BtnDefaultColors, "              Paste key into main text at cursor location");
            BtnDefaultColors.UseVisualStyleBackColor = false;
            BtnDefaultColors.Click += BtnDefaultColors_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 11F);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(385, 282);
            label1.Name = "label1";
            label1.Size = new Size(146, 42);
            label1.TabIndex = 0;
            label1.Text = "Search Time Limit\r\n     in Seconds:";
            // 
            // ChkWordOnly
            // 
            ChkWordOnly.AutoSize = true;
            ChkWordOnly.Font = new Font("Times New Roman", 11F);
            ChkWordOnly.ForeColor = Color.Black;
            ChkWordOnly.Location = new Point(5, 38);
            ChkWordOnly.Name = "ChkWordOnly";
            ChkWordOnly.Size = new Size(115, 25);
            ChkWordOnly.TabIndex = 0;
            ChkWordOnly.TabStop = false;
            ChkWordOnly.Text = "Word Only";
            ChkWordOnly.UseVisualStyleBackColor = true;
            ChkWordOnly.CheckedChanged += ChkWordOnly_CheckedChanged;
            ChkWordOnly.Click += ChkWordOnly_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 175, 180);
            panel1.Controls.Add(ChkMatchCase);
            panel1.Controls.Add(ChkWordOnly);
            panel1.Location = new Point(386, 498);
            panel1.Name = "panel1";
            panel1.Size = new Size(149, 68);
            panel1.TabIndex = 0;
            // 
            // RbTextReplace
            // 
            RbTextReplace.Checked = true;
            RbTextReplace.Font = new Font("Times New Roman", 13F);
            RbTextReplace.ForeColor = Color.Black;
            RbTextReplace.Location = new Point(160, 40);
            RbTextReplace.Name = "RbTextReplace";
            RbTextReplace.Size = new Size(89, 25);
            RbTextReplace.TabIndex = 0;
            RbTextReplace.TabStop = true;
            RbTextReplace.Text = "Text Replace";
            RbTextReplace.UseVisualStyleBackColor = true;
            RbTextReplace.CheckedChanged += RbTextReplace_CheckedChanged;
            RbTextReplace.Click += RbTextReplace_Click;
            // 
            // RtfSearchName
            // 
            RtfSearchName.BackColor = Color.DarkGray;
            RtfSearchName.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RtfSearchName.Location = new Point(120, 4);
            RtfSearchName.Multiline = false;
            RtfSearchName.Name = "RtfSearchName";
            RtfSearchName.Size = new Size(292, 29);
            RtfSearchName.TabIndex = 0;
            RtfSearchName.Text = "";
            RtfSearchName.MouseClick += RtfSearchName_MouseClick;
            RtfSearchName.KeyDown += RtfSearchName_KeyDown;
            RtfSearchName.KeyPress += RtfSearchName_KeyPress;
            RtfSearchName.Leave += RtfSearchName_Leave;
            RtfSearchName.MouseDown += RtfSearchName_MouseDown;
            RtfSearchName.MouseMove += RtfSearchName_MouseMove;
            RtfSearchName.MouseUp += RtfSearchName_MouseUp;
            // 
            // BtnSearchFolder
            // 
            BtnSearchFolder.BackColor = Color.BlueViolet;
            BtnSearchFolder.Font = new Font("Times New Roman", 13F);
            BtnSearchFolder.ForeColor = Color.White;
            BtnSearchFolder.Location = new Point(388, 705);
            BtnSearchFolder.Name = "BtnSearchFolder";
            BtnSearchFolder.Size = new Size(123, 28);
            BtnSearchFolder.TabIndex = 0;
            BtnSearchFolder.TabStop = false;
            BtnSearchFolder.Text = "Search Folder";
            BtnSearchFolder.UseVisualStyleBackColor = false;
            BtnSearchFolder.Click += BtnSearchFolder_Click;
            // 
            // lblBoundaryMarkerCount
            // 
            lblBoundaryMarkerCount.AutoSize = true;
            lblBoundaryMarkerCount.BackColor = Color.Gray;
            lblBoundaryMarkerCount.BorderStyle = BorderStyle.Fixed3D;
            lblBoundaryMarkerCount.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBoundaryMarkerCount.ForeColor = Color.Black;
            lblBoundaryMarkerCount.Location = new Point(265, 39);
            lblBoundaryMarkerCount.Name = "lblBoundaryMarkerCount";
            lblBoundaryMarkerCount.Size = new Size(231, 28);
            lblBoundaryMarkerCount.TabIndex = 23;
            lblBoundaryMarkerCount.Text = "Boundary Markers =    ";
            // 
            // TxtTimeIndicator
            // 
            TxtTimeIndicator.BackColor = Color.Black;
            TxtTimeIndicator.Font = new Font("Arial", 13F, FontStyle.Bold);
            TxtTimeIndicator.ForeColor = Color.Lime;
            TxtTimeIndicator.Location = new Point(390, 328);
            TxtTimeIndicator.Name = "TxtTimeIndicator";
            TxtTimeIndicator.Size = new Size(121, 32);
            TxtTimeIndicator.TabIndex = 24;
            TxtTimeIndicator.TabStop = false;
            TxtTimeIndicator.Text = "20";
            TxtTimeIndicator.TextAlign = HorizontalAlignment.Center;
            TxtTimeIndicator.KeyPress += TxtTimeIndicator_KeyPress;
            TxtTimeIndicator.KeyUp += TxtTimeIndicator_KeyUp;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Gray;
            label2.Font = new Font("Times New Roman", 11F);
            label2.Location = new Point(385, 609);
            label2.Name = "label2";
            label2.Size = new Size(163, 189);
            label2.TabIndex = 26;
            label2.Text = "Search Folder:\r\nFile extensions:\r\n Searches = .fdta\r\n Quizes = .qdta\r\n\r\n recentFiles.txt \r\n contains your file \r\n history data,  take \r\n care not to delete it!";
            // 
            // FrmColor
            // 
            AutoScaleDimensions = new SizeF(14F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 190, 200);
            ClientSize = new Size(565, 900);
            Controls.Add(RbEditColor);
            Controls.Add(RbTextReplace);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(BtnDefaultColors);
            Controls.Add(TxtTimeIndicator);
            Controls.Add(lblBoundaryMarkerCount);
            Controls.Add(BtnUndo);
            Controls.Add(BtnSearchFolder);
            Controls.Add(RtfSearchName);
            Controls.Add(btnSuffixReplace);
            Controls.Add(btnPrefixReplace);
            Controls.Add(btnReplace_All);
            Controls.Add(btnSaveSearch);
            Controls.Add(btnGetSearch);
            Controls.Add(btnPasteInto);
            Controls.Add(label1);
            Controls.Add(RtfReplace10);
            Controls.Add(lblReplace10);
            Controls.Add(FrmColorBtnC10);
            Controls.Add(RtfFind10);
            Controls.Add(lblPipe10);
            Controls.Add(lblFind10);
            Controls.Add(RtfReplace9);
            Controls.Add(lblReplace09);
            Controls.Add(FrmColorBtnC9);
            Controls.Add(RtfFind9);
            Controls.Add(lblPipe09);
            Controls.Add(lblFind09);
            Controls.Add(btnClear);
            Controls.Add(btnClearReplace);
            Controls.Add(btnClearFinds);
            Controls.Add(btnClearAll);
            Controls.Add(RtfReplace8);
            Controls.Add(lblReplace08);
            Controls.Add(FrmColorBtnC8);
            Controls.Add(RtfFind8);
            Controls.Add(lblPipe08);
            Controls.Add(lblFind08);
            Controls.Add(RtfReplace7);
            Controls.Add(lblReplace07);
            Controls.Add(FrmColorBtnC7);
            Controls.Add(RtfFind7);
            Controls.Add(lblPipe07);
            Controls.Add(lblFind07);
            Controls.Add(RtfReplace6);
            Controls.Add(lblReplace06);
            Controls.Add(FrmColorBtnC6);
            Controls.Add(RtfFind6);
            Controls.Add(lblPipe06);
            Controls.Add(lblFind06);
            Controls.Add(RtfReplace5);
            Controls.Add(lblReplace05);
            Controls.Add(FrmColorBtnC5);
            Controls.Add(RtfFind5);
            Controls.Add(lblPipe05);
            Controls.Add(lblFind05);
            Controls.Add(RtfReplace4);
            Controls.Add(lblReplace04);
            Controls.Add(FrmColorBtnC4);
            Controls.Add(RtfFind4);
            Controls.Add(lblPipe04);
            Controls.Add(lblFind04);
            Controls.Add(RtfReplace3);
            Controls.Add(lblReplace03);
            Controls.Add(FrmColorBtnC3);
            Controls.Add(RtfFind3);
            Controls.Add(lblPipe03);
            Controls.Add(lblFind03);
            Controls.Add(RtfReplace2);
            Controls.Add(lblReplace02);
            Controls.Add(FrmColorBtnC2);
            Controls.Add(RtfFind2);
            Controls.Add(lblPipe02);
            Controls.Add(lblFind02);
            Controls.Add(RtfReplace1);
            Controls.Add(lblReplace01);
            Controls.Add(FrmColorBtnC1);
            Controls.Add(RtfFind1);
            Controls.Add(lblPipe01);
            Controls.Add(lblFind01);
            Controls.Add(lblSrch);
            Font = new Font("Times New Roman", 15F);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 4, 6, 4);
            Name = "FrmColor";
            StartPosition = FormStartPosition.Manual;
            Text = " Tachufind";
            TopMost = true;
            Activated += FrmColor_Activated;
            FormClosing += FrmColor_FormClosing;
            Load += FrmColor_Load;
            VisibleChanged += FrmColor_VisibleChanged;
            KeyUp += FrmColor_KeyUp;
            MouseDown += FrmColor_MouseDown;
            MouseEnter += FrmColor_MouseEnter;
            MouseLeave += FrmColor_MouseLeave;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSrch;
        private System.Windows.Forms.Label lblFind01;
        private System.Windows.Forms.Label lblPipe01;
        private System.Windows.Forms.Label lblPipe02;
        private System.Windows.Forms.Label lblFind02;
        private System.Windows.Forms.Label lblPipe03;
        private System.Windows.Forms.Label lblFind03;
        private System.Windows.Forms.Label lblPipe04;
        private System.Windows.Forms.Label lblFind04;
        private System.Windows.Forms.Label lblPipe05;
        private System.Windows.Forms.Label lblFind05;
        private System.Windows.Forms.Label lblPipe06;
        private System.Windows.Forms.Label lblFind06;
        private System.Windows.Forms.Label lblPipe07;
        private System.Windows.Forms.Label lblFind07;
        private System.Windows.Forms.Label lblPipe08;
        private System.Windows.Forms.Label lblFind08;
        private System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnReplace_All;
        public System.Windows.Forms.Button btnClearFinds;
        public System.Windows.Forms.Button btnGetSearch;
        public System.Windows.Forms.Button btnSaveSearch;
        public System.Windows.Forms.Button btnClearReplace;
        public System.Windows.Forms.Button btnClearAll;
        public System.Windows.Forms.Button btnPasteInto;
        public System.Windows.Forms.Button FrmColorBtnC1;
        public System.Windows.Forms.Button FrmColorBtnC2;
        public System.Windows.Forms.Button FrmColorBtnC3;
        public System.Windows.Forms.Button FrmColorBtnC4;
        public System.Windows.Forms.Button FrmColorBtnC5;
        public System.Windows.Forms.Button FrmColorBtnC6;
        public System.Windows.Forms.Button FrmColorBtnC7;
        public System.Windows.Forms.Button FrmColorBtnC8;
        public System.Windows.Forms.RadioButton RbEditColor;
        public System.Windows.Forms.Label lblReplace01;
        public System.Windows.Forms.Label lblReplace02;
        public System.Windows.Forms.Label lblReplace03;
        public System.Windows.Forms.Label lblReplace04;
        public System.Windows.Forms.Label lblReplace05;
        public System.Windows.Forms.Label lblReplace06;
        public System.Windows.Forms.Label lblReplace07;
        public System.Windows.Forms.Label lblReplace08;
        public System.Windows.Forms.RichTextBox RtfFind1;
        public System.Windows.Forms.RichTextBox RtfFind2;
        public System.Windows.Forms.RichTextBox RtfFind3;
        public System.Windows.Forms.RichTextBox RtfFind4;
        public System.Windows.Forms.RichTextBox RtfFind5;
        public System.Windows.Forms.RichTextBox RtfFind6;
        public System.Windows.Forms.RichTextBox RtfFind7;
        public System.Windows.Forms.RichTextBox RtfFind8;
        public System.Windows.Forms.Label lblReplace09;
        public System.Windows.Forms.Button FrmColorBtnC9;
        public System.Windows.Forms.RichTextBox RtfFind9;
        private System.Windows.Forms.Label lblPipe09;
        private System.Windows.Forms.Label lblFind09;
        public System.Windows.Forms.Label lblReplace10;
        public System.Windows.Forms.Button FrmColorBtnC10;
        public System.Windows.Forms.RichTextBox RtfFind10;
        private System.Windows.Forms.Label lblPipe10;
        private System.Windows.Forms.Label lblFind10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton rbMultipleReplace;
        public System.Windows.Forms.RichTextBox RtfReplace1;
        public System.Windows.Forms.RichTextBox RtfReplace2;
        public System.Windows.Forms.RichTextBox RtfReplace3;
        public System.Windows.Forms.RichTextBox RtfReplace4;
        public System.Windows.Forms.RichTextBox RtfReplace5;
        public System.Windows.Forms.RichTextBox RtfReplace6;
        public System.Windows.Forms.RichTextBox RtfReplace7;
        public System.Windows.Forms.RichTextBox RtfReplace8;
        public System.Windows.Forms.RichTextBox RtfReplace9;
        public System.Windows.Forms.RichTextBox RtfReplace10;
        public System.Windows.Forms.CheckBox ChkMatchCase;
        public System.Windows.Forms.CheckBox ChkWordOnly;
        public System.Windows.Forms.Button btnPrefixReplace;
        public System.Windows.Forms.Button btnSuffixReplace;
        public System.Windows.Forms.RichTextBox RtfSearchName;
        public System.Windows.Forms.Button BtnSearchFolder;
        public System.Windows.Forms.Button BtnUndo;
        private System.Windows.Forms.Label lblBoundaryMarkerCount;
        public System.Windows.Forms.TextBox TxtTimeIndicator;
        public Button BtnDefaultColors;
        public RadioButton RbTextReplace;
        private Label label2;
    }
}