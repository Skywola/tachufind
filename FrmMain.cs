/*
 * Tachufind
 * Originally programmed by Shawn Irwin as an ongoing project since 2010
 * This is not only a great self-study assistant, but also specialized for learning languages.
 * 
 *
 */

using System.Diagnostics;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Tachufind
{

    public partial class FrmMain : Form
    {
        #region API Declarations
        private const int WM_USER = 0x0400;
        private const int EM_SETCHARFORMAT = WM_USER + 68;
        private const int SCF_SELECTION = 0x0001;
        private const int SCF_ALL = 0x0004;

        private const uint CFM_FACE = 0x20000000;
        private const uint CFM_SIZE = 0x80000000;
        private const uint CFE_AUTOBACKCOLOR = 0x04000000;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct CHARFORMAT2
        {
            public uint cbSize;
            public uint dwMask;
            public uint dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szFaceName;

            public ushort wWeight;
            public short sSpacing;
            public int crBackColor;
            public int lcid;
            public int dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref CHARFORMAT2 lParam);

        #endregion

        #region Initializations
        private SpeechSynthesizer? synth; // Class-level variable

        public static FrmMain Instance { get; private set; } = null!;

        private FileHistoryManager fileHistoryManager;
        private Stack<int> returnButtonStack;
        private static Search search = new();
        private static BoundaryMarkerSearch searchBM = new();
        internal static Queue<BoundaryMarkerSearch> searchBMQueue = new();  // FrmColor/FrmMain SearchBM - Replace operations with Boundary Markers present


        private static Search panel_BtnBFSearch = new();
        private static Search panel_BtnEOFSearch = new();
        //private static Search panelBtnFindRevFrmCursor = new();
        private static int panel_BtnFindFwdFrmCursorPointer = 0;
        private static VisualLines visualLines = new();


        public FrmMain()
        {
            InitializeComponent();
            this.KeyPreview = true;


            InitializeSpeechSynthesizer();
            Instance = this; // Set the static reference in the constructor

            fileHistoryManager = new FileHistoryManager();
            returnButtonStack = new Stack<int>();
            // Ensuring the RichTextBox is added to the form and has a minimum size
            RTBMain.MinimumSize = new Size(1000, 666); // Set a reasonable minimum size
        }

        private void InitializeSpeechSynthesizer()
        {
            synth = new SpeechSynthesizer();
            synth.SpeakCompleted += Synth_SpeakCompleted;
            synth.SpeakStarted += Synth_SpeakStarted;
            synth.SetOutputToDefaultAudioDevice();
        }

        #endregion // Initializations


        // LOCATION → → → → →  RunThisSearchInFrmMain() 
        // Search coming over from FrmColor, with two possibilities
        // Search with No Boundary Markers, or Search With Boudary Markers
        public void RunInFrmMain()
        {
            if (CheckForOddNumberOfBoundryMarkers()) { return; } // MessageBox to user IF odd number of BMs
            bool BM = (searchBM.boundryMarkerCount > 0);

            if (BM) // Boundary Markers
            {
                this.BMOperationInitialization();
            }
            // No Boundary Markers
            else if (!BM)
            {
                this.NoBMs_Initialization();
            }
        }


        #region FrmMainLoadingAndInitialization

        // LOCATION  → → → → →  FrmMain Load
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                ScreenSetUp(this);
                fileHistoryManager.LoadFilesFromTextFile(AppSettings.FileMenuFileHistoryTextFile, true);
                fileHistoryManager.LoadDropDownMenu(frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                // needs to set colors in globals, and set the color buttons in Main
                SetColorButtons();
                // FOR TESTING USER AGREEMENT INITIALIZATION AND REGESTERING TACHUFIND AS BEING ASSOCIATED WITH RTF FILES
                // Globals.User_Settings.StrSetUserAgreed = "";
                // AppSettings.UserHasAgreed = false;
                HandleUserAgreement();
                SetFrmMainLocation();
                if (Globals.User_Settings.TTS_Voice.Length > 1)
                {
                    cmboVoice.Text = Globals.User_Settings.TTS_Voice;
                }
                SetFrmMainSize();
                InitializeUI();
                InitializeVoiceOptions();
                ChkLoop.Checked = Globals.User_Settings.LoopTTSPlayback ? true : false;
                if (!GetFileToOpenPath(fileHistoryManager)) { return; }
                this.Refresh();
                this.Focus();
                this.Focus();
                this.BringToFront();
                this.Show();

                // Assign Context Menu to RichTextBox
                RTBMain.ContextMenuStrip = contextMenuStrip1;

                this.RTBMain.Focus();
                Globals.Current_RTB_withFocus = RTBMain;

                //AppSettings.FrmMainInit = false;  // End INIT phase, allow updates of forms when they are repositioned
                AppSettings.RtbModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTBMain.SelectedText.Length > 0)
                RTBMain.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                RTBMain.Paste();
        }



        // ScreenSetup is necessary for possible multi-screen systems.
        private void ScreenSetUp(Form form)
        {
            Size savedSize = Globals.User_Settings.FrmMainSize;
            Point savedLocation = Globals.User_Settings.FrmMainLocation;
            ScreenUtility.InitializeForm(form, savedLocation, savedSize);
        }


        private void SetColorButtons()
        {
            this.FrmMainColorBtn1.BackColor = ColorManager.GetColor("C1");
            this.FrmMainColorBtn2.BackColor = ColorManager.GetColor("C2");
            this.FrmMainColorBtn3.BackColor = ColorManager.GetColor("C3");
            this.FrmMainColorBtn4.BackColor = ColorManager.GetColor("C4");
            this.FrmMainColorBtn5.BackColor = ColorManager.GetColor("C5");
            this.FrmMainColorBtn6.BackColor = ColorManager.GetColor("C6");
            this.FrmMainColorBtn7.BackColor = ColorManager.GetColor("C7");
            this.FrmMainColorBtn8.BackColor = ColorManager.GetColor("C8");
            this.FrmMainColorBtn9.BackColor = ColorManager.GetColor("C9");
            this.FrmMainColorBtn10.BackColor = ColorManager.GetColor("C10");
        }

        private static string GetCommandLinePath()
        {
            string[] args = Environment.GetCommandLineArgs();

            // Check if there's more than one argument and the second argument has a length greater than 1
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                string commandLinePath = args[1];

                // Check if the file exists
                if (File.Exists(commandLinePath))
                {
                    Globals.User_Settings.FilePath01 = commandLinePath;
                    MessageBox.Show("Argument: " + commandLinePath);
                    return commandLinePath;
                }
            }
            return string.Empty; // Return string.Empty instead of null
        }


        private bool GetFileToOpenPath(FileHistoryManager fileHistoryManager)
        {
            string possiblePath = GetCommandLinePath();
            bool notNullOrWhiteSpace = !(string.IsNullOrWhiteSpace(possiblePath) || possiblePath == "");

            if (File.Exists(possiblePath) && notNullOrWhiteSpace)
            {
                LoadAndUpdateFile(possiblePath);
                return true;
            }
            else
            {
                try
                {
                    // If no command-line argument, get file path from file history
                    string historyFilePath = fileHistoryManager.GetFirstFilePath();
                    notNullOrWhiteSpace = !(string.IsNullOrWhiteSpace(historyFilePath) || historyFilePath == "");
                    if (File.Exists(historyFilePath) && notNullOrWhiteSpace)
                    {
                        LoadAndUpdateFile(historyFilePath);
                        return true;
                    }
                    else
                    {
                        newToolStripMenuItem.PerformClick();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("File path was invalid.");
                    Globals.User_Settings.FilePath01 = "";  // Change contents to empty, so next file user opens is placed here.
                    newToolStripMenuItem.PerformClick();
                }
            }
            return false;
        }


        private bool LoadAndUpdateFile(string filePath)
        {
            Globals.User_Settings.FilePath = filePath;
            AppSettings.filePath = filePath;
            this.Text = "Tachufind     Editing: " + filePath;

            string fileExtension = Path.GetExtension(AppSettings.filePath).ToUpper();
            if (fileExtension == ".TXT") // Change extension to txt
            {
                RTBMain.Text = FileIO.LoadFileContents(filePath);
            }
            else
            {
                RTBMain.Rtf = FileIO.LoadFileContents(filePath);
            }
            // Update the file history
            var menu = new ToolStripMenuItem();
            fileHistoryManager.LoadFilesFromTextFile(AppSettings.FileMenuFileHistoryTextFile, true);
            fileHistoryManager.LoadDropDownMenu(menu, AppConstants.STATIC_MENU_ITEMS);
            fileHistoryManager.AddFilePath(filePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
            fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);

            // Display the file path and set any specific settings for the newly opened file
            DisplayFilePath(filePath);
            SetNewlyOpenedFileSettings();

            this.RTBMain.SelectionStart = 0;
            Globals.FindStart = 0;
            this.RTBMain.SelectionStart = Globals.FindStart;
            AppSettings.RtbModified = false;
            return true;
        }



        private void SetFrmMainLocation()
        {
            var primaryScreen = System.Windows.Forms.Screen.PrimaryScreen;

            // Check if primaryScreen is not null
            if (primaryScreen != null)
            {
                int screenWidth = primaryScreen.Bounds.Width;
                int screenHeight = primaryScreen.Bounds.Height;

                Point? storedLocation = Globals.User_Settings.FrmMainLocation;

                // Check if stored location is valid and within screen bounds
                if (storedLocation != null)
                {
                    Point pt = (Point)storedLocation;

                    // Check if this point is on any available screen
                    bool pointOnAnyScreen = false;
                    foreach (Screen screen in Screen.AllScreens)
                    {
                        if (screen.Bounds.Contains(pt))
                        {
                            pointOnAnyScreen = true;
                            break;
                        }
                    }

                    // Also check if it's within reasonable bounds (not -32000)
                    bool notMinimized = pt.X > -32000 && pt.Y > -32000;

                    // Set form position based on stored coordinates if valid
                    this.Left = (pointOnAnyScreen && notMinimized) ? pt.X : Convert.ToInt32(ClientSize.Width / 2.3);
                    this.Top = (pointOnAnyScreen && notMinimized) ? pt.Y : Convert.ToInt32(ClientSize.Height / 8);
                }
                else
                {
                    // Use default values if no stored location is found
                    this.Left = Convert.ToInt32(ClientSize.Width / 2.3);
                    this.Top = Convert.ToInt32(ClientSize.Height / 8);
                }
            }
            else
            {
                // Handle case where primaryScreen is null
                this.Left = Convert.ToInt32(ClientSize.Width / 2.3);
                this.Top = Convert.ToInt32(ClientSize.Height / 8);
            }
        }

        private void InitializeUI()
        {

            if (this.RTBMain != null)
            {
                // Set RTBMain properties
                this.RTBMain.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
                this.RTBMain.EnableAutoDragDrop = true;
                this.RTBMain.AutoWordSelection = true;
                this.RTBMain.SelectionStart = Globals.User_Settings.CursorPosition;
            }

            PopulateFontComboBox();

            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
        }

        private void PopulateFontComboBox()
        {
            // Get the installed fonts on the system
            System.Drawing.Text.InstalledFontCollection installedFonts = new System.Drawing.Text.InstalledFontCollection();

            // Add each font family name to the ComboBox
            foreach (System.Drawing.FontFamily fontFamily in installedFonts.Families)
            {
                CmboFontSelect.Items.Add(fontFamily.Name);
            }

            // Optionally, set a default selected item
            CmboFontSelect.SelectedIndex = 0;
            CmboFontSelect.Text = "Times New Roman";
        }


        public void SetColorDefaults()
        {
            ColorManager.SetColor("C1", Color.Cyan);
            Globals.User_Settings.Color1 = Color.Cyan;
            this.FrmMainColorBtn1.BackColor = Color.Cyan;

            ColorManager.SetColor("C2", Color.Green);
            Globals.User_Settings.Color2 = Color.Green;
            this.FrmMainColorBtn2.BackColor = Color.Green;

            ColorManager.SetColor("C3", Color.Red);
            Globals.User_Settings.Color3 = Color.Red;
            this.FrmMainColorBtn3.BackColor = Color.Red;

            ColorManager.SetColor("C4", Color.Gold);
            Globals.User_Settings.Color4 = Color.Gold;
            this.FrmMainColorBtn4.BackColor = Color.Gold;

            ColorManager.SetColor("C5", Color.Coral);
            Globals.User_Settings.Color5 = Color.Coral;
            this.FrmMainColorBtn5.BackColor = Color.Coral;

            ColorManager.SetColor("C6", Color.SaddleBrown);
            Globals.User_Settings.Color6 = Color.SaddleBrown;
            this.FrmMainColorBtn6.BackColor = Color.SaddleBrown;

            ColorManager.SetColor("C7", Color.Orchid);
            Globals.User_Settings.Color7 = Color.Orchid;
            this.FrmMainColorBtn7.BackColor = Color.Orchid;

            ColorManager.SetColor("C8", Color.DodgerBlue);
            Globals.User_Settings.Color8 = Color.DodgerBlue;
            this.FrmMainColorBtn8.BackColor = Color.DodgerBlue;

            ColorManager.SetColor("C9", Color.Purple);
            Globals.User_Settings.Color9 = Color.Purple;
            this.FrmMainColorBtn9.BackColor = Color.Purple;

            ColorManager.SetColor("C10", Color.Olive);
            Globals.User_Settings.Color10 = Color.Olive;
            this.FrmMainColorBtn10.BackColor = Color.Olive;
        }

        public static void SetUserControlledColorSelections()
        {
            // Use stored colors if they are good, otherwise use the default colors
            // LOCATION   → →  → →   DEFAULT COLORS FOR SEARCH REPLACE
            if (ColorManager.IsValidColor(Globals.User_Settings.Color1)) { ColorManager.SetColor("C1", Globals.User_Settings.Color1.ToString()); }
            else { ColorManager.SetColor("C1", Color.FromArgb(255, 0, 0, 255)); } // Default Blue

            if (ColorManager.IsValidColor(Globals.User_Settings.Color2)) { ColorManager.SetColor("C2", Globals.User_Settings.Color2.ToString()); }
            else { ColorManager.SetColor("C2", Color.FromArgb(255, 0, 255, 0)); }  // Green

            if (ColorManager.IsValidColor(Globals.User_Settings.Color3)) { ColorManager.SetColor("C3", Globals.User_Settings.Color3.ToString()); }
            else { ColorManager.SetColor("C3", Color.FromArgb(255, 255, 0, 0)); }  // Red

            if (ColorManager.IsValidColor(Globals.User_Settings.Color4)) { ColorManager.SetColor("C4", Globals.User_Settings.Color4.ToString()); }
            else { ColorManager.SetColor("C4", Color.FromArgb(255, 0, 255, 255)); } // Yellow

            if (ColorManager.IsValidColor(Globals.User_Settings.Color5)) { ColorManager.SetColor("C5", Globals.User_Settings.Color5.ToString()); }
            else { ColorManager.SetColor("C5", Color.FromArgb(255, 148, 0, 211)); } // Dark Violet    5

            if (ColorManager.IsValidColor(Globals.User_Settings.Color6)) { ColorManager.SetColor("C6", Globals.User_Settings.Color6.ToString()); }
            else { ColorManager.SetColor("C6", Color.FromArgb(255, 139, 69, 19)); }  // Saddle Brown

            if (ColorManager.IsValidColor(Globals.User_Settings.Color7)) { ColorManager.SetColor("C7", Globals.User_Settings.Color7.ToString()); }
            else { ColorManager.SetColor("C7", Color.FromArgb(255, 112, 128, 144)); } // Slate Gray

            if (ColorManager.IsValidColor(Globals.User_Settings.Color8)) { ColorManager.SetColor("C8", Globals.User_Settings.Color8.ToString()); }
            else { ColorManager.SetColor("C8", Color.FromArgb(255, 30, 144, 255)); } // Dodger Blue

            if (ColorManager.IsValidColor(Globals.User_Settings.Color9)) { ColorManager.SetColor("C9", Globals.User_Settings.Color9.ToString()); }
            else { ColorManager.SetColor("C9", Color.FromArgb(255, 220, 20, 60)); } // Crimson

            if (ColorManager.IsValidColor(Globals.User_Settings.Color10)) { ColorManager.SetColor("C10", Globals.User_Settings.Color10.ToString()); }
            else { ColorManager.SetColor("C10", Color.FromArgb(255, 128, 128, 0)); } // Olive
        }

        private static void HandleUserAgreement()
        {
            if (Globals.User_Settings.StrSetUserAgreed != "согласен")
            {
                var frmUser = new FrmUser
                {
                    Visible = false
                };

                frmUser.ShowDialog(); // Show User Agreement
            }

            if (!Globals.User_Settings.StrSetUserAgreed.Contains("согласен"))
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void SetFrmMainSize()
        {
            var primaryScreen = System.Windows.Forms.Screen.PrimaryScreen;

            // Use null-conditional operator to safely access Bounds
            int screenWidth = primaryScreen?.Bounds.Width ?? 1920; // Default width if primaryScreen is null
            int screenHeight = primaryScreen?.Bounds.Height ?? 1080; // Default height if primaryScreen is null

            int storedWidth = Globals.User_Settings.FrmMainSize.Width;
            int storedHeight = Globals.User_Settings.FrmMainSize.Height;

            // Set form width based on storedWidth or default value
            this.Width = (storedWidth > 1000 && storedWidth < screenWidth) ? storedWidth : Convert.ToInt32(screenWidth / 1.2);

            // Set form height based on storedHeight or default value
            this.Height = (storedHeight > 800 && storedHeight < screenHeight) ? storedHeight : Convert.ToInt32(screenHeight / 1.2);
        }

        private void PreserveHighlightingAndSetBackColor(Color newBackColor)
        {
            RTBMain.Visible = false;  // Hide the control to avoid flickering during updates
            RTBMain.SuspendLayout();  // Suspend layout to make operations faster

            // Backup the current selection
            int originalSelectionStart = RTBMain.SelectionStart;
            int originalSelectionLength = RTBMain.SelectionLength;

            // Select all text
            RTBMain.SelectAll();

            // Create a list to store the ranges of highlighted text
            List<(int Start, int Length, Color HighlightColor)> highlightedSections = [];

            // Loop through the text and find highlighted sections
            int lastCheckedIndex = 0;
            while (lastCheckedIndex < RTBMain.TextLength)
            {
                RTBMain.Select(lastCheckedIndex, 1);

                // Check if the current character has highlighting (different from the default BackColor)
                if (RTBMain.SelectionBackColor != RTBMain.BackColor)
                {
                    Color currentHighlight = RTBMain.SelectionBackColor;
                    int start = lastCheckedIndex;
                    int length = 0;

                    // Find the continuous range of highlighted text
                    while (lastCheckedIndex < RTBMain.TextLength && RTBMain.SelectionBackColor == currentHighlight)
                    {
                        length++;
                        lastCheckedIndex++;
                        if (lastCheckedIndex < RTBMain.TextLength)
                        {
                            RTBMain.Select(lastCheckedIndex, 1);
                        }
                    }

                    // Store the start, length, and highlight color
                    highlightedSections.Add((start, length, currentHighlight));
                }
                else
                {
                    lastCheckedIndex++;
                }
            }

            // Reset selection and apply the new back color to all text
            RTBMain.SelectAll();
            RTBMain.SelectionBackColor = newBackColor;

            // Reapply the original highlight colors to the previously highlighted sections
            foreach (var (Start, Length, HighlightColor) in highlightedSections)
            {
                RTBMain.Select(Start, Length);
                RTBMain.SelectionBackColor = HighlightColor;
            }


            // Restore the original selection
            RTBMain.Select(originalSelectionStart, originalSelectionLength);

            RTBMain.ResumeLayout();  // Resume layout after operation
            RTBMain.Visible = true;  // Show the control again
        }
        #endregion  // FrmMainLoadingAndInitialization




        #region SearchTimer
        // This will control the maximum time spent on a search done using FrmColor.  
        // The duration is set on FrmColor, and is stored in Global.searchTimeLimit.
        // btnReplace_All_Click assigns startTime to DateTime.Now() when clicked
        // Each While loop in searches updates endTime on each loop
        // Globals.searchInProgress = true; shows that a search is in progress
        // LOCATION  → → → → → SEARCH TIME CONTROL 
        private void SearchTimeControl()  // <<------pass frmColor in
        {
            FrmColor? frmColor = FrmColor.Instance;

            Globals.DisplayTime = Globals.User_Settings.SearchTimeLimit; // Added 3-3-2025 prompt was firing before it even ran once

            var diffInSeconds = (Globals.StartTime - DateTime.Now).TotalSeconds;
            int elapsed = Convert.ToInt32(diffInSeconds);
            float timeLeft = Globals.DisplayTime + elapsed;  // (elapsed is negative)

            if (timeLeft <= 0)
            {
                this.Invoke((MethodInvoker)(() => frmColor.TxtTimeIndicator.Text = "0"));
                string message = "Your search has exceeed the time you alloted for it," + Environment.NewLine
                    + "under Search Time in Seconds, do you want to continue?";
                DialogResult dr = MessageBox.Show(message, "Time limit exceed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                switch (dr)
                {
                    case DialogResult.Yes:
                        Globals.DisplayTime = Globals.User_Settings.SearchTimeLimit;
                        this.Invoke((MethodInvoker)(() => frmColor.TxtTimeIndicator.Text = Globals.DisplayTime.ToString()));
                        this.Invoke((MethodInvoker)(() => frmColor.Refresh()));
                        Globals.DisplayTime = Globals.User_Settings.SearchTimeLimit;
                        Globals.StartTime = DateTime.Now;
                        break;
                    case DialogResult.No:
                        Globals.DisplayTime = Globals.User_Settings.SearchTimeLimit;
                        this.Invoke((MethodInvoker)(() => frmColor.TxtTimeIndicator.Text = Globals.DisplayTime.ToString()));
                        // KEY TO EXITING THE SEARCH:
                        AppSettings.SearchInProgress = false;
                        searchBM.searchBMComplete = true;
                        searchBMQueue.Clear(); // this effectively stops the search loop
                        break;
                }
            }
            else
            { // update frmColor time.  (elapsed is negative)
                this.Invoke((MethodInvoker)(() => frmColor.TxtTimeIndicator.Text = timeLeft.ToString()));
                this.Invoke((MethodInvoker)(() => frmColor.Refresh()));
            }
        }
        #endregion // SearchTimer



        #region Replace_NoBoundaryMarkers

        public void NoBMs_Initialization() // textMode (false) or colorMode (true)
        {
            Globals.search.Clear();
            AppSettings.currentCursorPosition = RTBMain.SelectionStart;
            Cursor.Current = Cursors.WaitCursor;
            RTBMain.SuspendLayout();  // Detach event handlers if any
            if (RTBMain.Rtf != null) { AppSettings.undoStack.Push(RTBMain.Rtf); } //  UNDO for RTBMain

            // Get all Searches.    If nothing in the search Queue, exit      
            // Loads into the Global:  Queue<SearchGroup> searches = new Queue<SearchGroup>();
            if (!GeneralFns.GetSearchesQueue()) { return; }

            try
            {
                SelectSearchType();
                AppSettings.UndoQueue.Enqueue(AppSettings.UndoCount);
                RTBMain.ResumeLayout(true);
                RTBMain.SelectionStart = AppSettings.currentCursorPosition;
                RTBMain.DeselectAll();
                RTBMain.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        public void SelectSearchType() // textMode or colorMode
        {
            Globals.search.index = 1;
            search.start = 0;
            // This is ONLY for the timer, it is not necessary for breaking out of the while statment.
            AppSettings.SearchInProgress = true;
            Globals.StartTime = DateTime.Now;

            try
            {
                while (Globals.searchQueue.Count > 0)
                {
                    Globals.search = Globals.searchQueue.Dequeue();
                    // TODO This should be used for TIMER 
                    if (!AppSettings.SearchInProgress) { return; }

                    // Apply replacement and formatting logic
                    if (AppSettings.SearchMode == Mode.Text)
                    {
                        RTBMain.SuspendLayout();
                        FilterAndReplaceText();
                        RTBMain.ResumeLayout();
                    }
                    else
                    {
                        RTBMain.SuspendLayout();
                        FilterAndReplaceColor();
                        RTBMain.ResumeLayout();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterAndReplaceText()
        {
            int startIndex = 0;

            while (startIndex < RTBMain.Text.Length && AppSettings.SearchInProgress == true)
            {
                startIndex = RTBMain.Find(Globals.search.FindString, startIndex, RichTextBoxFinds.None);
                if (startIndex < 0)
                {
                    break;
                }

                bool match = (MatchCaseWordAndAffix(RTBMain, startIndex, searchBM.findStringBM.Length));

                if (match)
                {
                    RTBMain.Select(startIndex, Globals.search.FindString.Length);
                    RTBMain.SelectedText = Globals.search.ReplaceString;
                    AppSettings.UndoCount++;
                }
                startIndex += Globals.search.ReplaceString.Length;

                // Check the search time control periodically SearchTimeControl();
                SearchTimeControl();
            }
        }

        private void FilterAndReplaceColor()
        {
            int startIndex = 0;

            try
            {
                while (startIndex < RTBMain.Text.Length && AppSettings.SearchInProgress == true)
                {
                    startIndex = RTBMain.Text.IndexOf(Globals.search.FindString, startIndex);
                    if (startIndex < 0)
                    {
                        break;
                    }

                    bool match = MatchCaseWordAndAffix(RTBMain, startIndex, Globals.search.FindString.Length);

                    if (match)
                    {
                        RTBMain.Select(startIndex, Globals.search.FindString.Length);
                        RTBMain.SelectionColor = Globals.search.FindColor;
                        AppSettings.UndoCount++;
                    }
                    startIndex += Globals.search.FindString.Length;

                    // Check the search time control periodically SearchTimeControl();
                    SearchTimeControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion // Replace_NoBoundaryMarkers


        #region Events

        private void FrmMain_LocationChanged(object sender, EventArgs e)
        {
            //ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmMainLocation = location);
            //if (AppSettings.FrmMainInit == true) { return; }
            Point pt = new(this.Left, this.Top);
            Globals.User_Settings.FrmMainLocation = pt;
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmMainSize = size);
        }

        // TODO I don't think this is needed, and likely should be removed.
        //private void FrmMain_TextChanged(object sender, EventArgs e)
        //{
        //    AppSettings.RtbModified = true;
        //}

        private void RTBMain_TextChanged(object sender, EventArgs e)
        {
            AppSettings.RtbModified = true;
            RTBMain.Focus();
        }
        #endregion // Events


        #region RTBMainActions

        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            //if (AppSettings.FrmMainInit == true) { return; }

            Size sz = new(this.Width, this.Height);
            Globals.User_Settings.FrmMainSize = sz;
        }

        private void RTBMain_MouseClick(object sender, MouseEventArgs e)
        {
            // Set the cursor (caret) in a RichTextBox at the exact mouse click position
            if (e.Button == MouseButtons.Left && e.Clicks == 1) // Only for left-click
            {
                // Get character index under mouse position
                int charIndex = RTBMain.GetCharIndexFromPosition(e.Location);
            }

            Globals.Current_RTB_withFocus = RTBMain;

            // Update cursor location display ( lblCursorLocation.Text )
            lblCursorLocation.Text = this.RTBMain.SelectionStart.ToString();
        }

        private void RTBMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int clickPosition = RTBMain.GetCharIndexFromPosition(e.Location);
                Globals.User_Settings.CursorPosition = clickPosition;

                // Check stack for entries 
                if (returnButtonStack != null && returnButtonStack.Count > 0)
                {
                    long cursorArrayZeroPosition = returnButtonStack.Peek();
                    if (clickPosition >= cursorArrayZeroPosition + 20 ||
                        clickPosition <= cursorArrayZeroPosition - 20)
                    {
                        returnButtonStack.Push(clickPosition);
                    }
                }
                if (returnButtonStack != null && returnButtonStack.Count == 0)
                {
                    returnButtonStack.Push(clickPosition);
                }
            }
        }

        private void elseif(bool v)
        {
            throw new NotImplementedException();
        }

        private void RTBMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RTBMain.Freeze();  // Prevent scrolling issue, from class RichTextBoxExtensions

            //string word = RegexHelpers.GetWordRegex().Match(this.RTBMain.SelectedText).Value;  // Removed 01-04-26
            string word = RTBMain.SelectedText;
            RTBSearchBox.Text = word;
            RTBMain.SelectionLength = word.Length;

            RTBMain.Unfreeze();  // Prevent scrolling issue, from class RichTextBoxExtensions
        }

        private void RTBMain_MouseEnter(object sender, EventArgs e)
        {
            RTBMain.Cursor = Cursors.Default;
            //RTBMain.Focus();
        }

        private void RTBMain_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is down, the user is likely dragging for selection
            if (AppSettings.isMouseDown)
            {
                // Let the selection happen normally during drag, so no need to reset anything here
                return;
            }
            RTBMain.Cursor = Cursors.Default;
        }

        private void RTBMain_MouseUp(object sender, MouseEventArgs e)
        {
            RTBMain.Focus();
            AppSettings.isMouseDown = false;

            // Check if the Shift key is pressed and it's a left mouse button click (For selecting text)
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Shift && AppSettings.initialMouseClickPosition >= 0)
            {
                // Get the position of the second click
                int finalClickPosition = RTBMain.SelectionStart;

                // Determine the range and select the text between initial and final positions
                if (finalClickPosition > AppSettings.initialMouseClickPosition)
                {
                    RTBMain.SelectionStart = AppSettings.initialMouseClickPosition;
                    RTBMain.SelectionLength = finalClickPosition - AppSettings.initialMouseClickPosition;
                }
                else
                {
                    RTBMain.SelectionStart = finalClickPosition;
                    RTBMain.SelectionLength = AppSettings.initialMouseClickPosition - finalClickPosition;
                }
            }
            // RTBMain.ResumeLayout();
            // Set the focus back to the control to keep the selection highlighted
            RTBMain.Focus();
        }

        #endregion //  RTBMainActions


        #region VOICE (Text To Speech)

        private static void SetVoiceAndVolume(SpeechSynthesizer synth)
        {
            synth.Rate = Globals.User_Settings.TTS_Speed;  // -10 through 10 will be speed,  Default -4
            //synth.Volume = 100; // 0 to 100.  Leaving this static at 100  Who adjusts voice volume here?
            //synth.Volume = (int)VolumeChanger.Value;
            synth.SelectVoice(AppSettings.Voice); // Selects a voice by name
        }

        private void EnablePlayButton()
        {
            btnTTSPlay.Enabled = true;
            //btnTTSStop.Enabled = false;
            // btnPauseResume.Enabled = false;
        }

        private void BtnTTSStop_Click(object sender, EventArgs e)
        {
            try
            {
                // Set a flag to indicate user requested stop
                AppSettings.userRequestedStop = true;

                if (btnTTSStop.Enabled && synth != null)
                {
                    if (synth.State == SynthesizerState.Speaking)
                    {
                        synth.SpeakAsyncCancelAll();
                        synth = new SpeechSynthesizer();
                        visualLines.ClearLines(); // forces a stop if necessary
                        visualLines = new();
                        RTBMain.DeselectAll();
                        AppSettings.isSpeaking = false;
                        EnablePlayButton();
                    }
                }
                btnTTSPlay.Enabled = true;
                //btnTTSStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnTTSPlay_Click(object sender, EventArgs e)
        {
            // Reset the stop flag when starting fresh playback
            AppSettings.userRequestedStop = false;

            RTBMain.Focus();
            if (RTBMain.Text.Length < 1) // || AppSettings.SpeechSynthClosing)
            {
                return;
            }

            try
            {
                DisablePlayButton();
                AppSettings.IsSynthActive = true;
                if (synth != null)
                {
                    AppSettings.isSpeaking = true;
                    if (Globals.User_Settings.TTS_Voice.Length > 0)
                    {
                        AppSettings.Voice = cmboVoice.Text = Globals.User_Settings.TTS_Voice;
                    }
                    SetVoiceAndVolume(synth);
                }

                if (RTBMain.SelectionLength > 0 && synth != null)
                {
                    string textToRead = CleanText(RTBMain.SelectedText);
                    ReadSelectedText(textToRead);
                    return;
                }
                else
                {
                    HandlePlaybackLoop();
                }

                //btnTTSStop.PerformClick();  // Not in Copilot code ##########
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnablePlayButton();
            }
        }

        private void ReadSelectedText(string textToRead)
        {
            if (synth != null)
            {
                // Check if the synthesizer is in a state that can be canceled
                if (synth.State == SynthesizerState.Speaking || synth.State == SynthesizerState.Paused)
                {
                    synth.SpeakAsyncCancelAll(); // Cancel all ongoing speech
                }
                visualLines.ClearLines();
                synth.Volume = (int)VolumeChanger.Value;
                // LOCATION  → → → → →  SPEAKPOINT2  
                synth.SpeakAsync(textToRead);
            }
        }

        private void HandlePlaybackLoop()
        {
            bool scrolled = false;

            try
            {
                RTBMain.Focus();

                // CRITICAL FIX: Always regenerate visual lines from current position
                // This ensures we have the correct lines for the current playback session
                visualLines = GetVisualLines(RTBMain);

                if (visualLines.Count < 1) { return; }

                while (visualLines.Count > 0)
                {
                    KeyValuePair<int, string> dequeuedLine = visualLines.GetNextLine();
                    AppSettings.startCharacter = dequeuedLine.Key;
                    AppSettings.lengthTTS = dequeuedLine.Value.Length;
                    if (AppSettings.lengthTTS < 1) { continue; }

                    RTBMain.Select(AppSettings.startCharacter, AppSettings.lengthTTS);
                    RTBMain.Refresh();
                    string sentence = RTBMain.SelectedText;
                    sentence = CleanText(sentence); // Clean the text

                    ScrollRichTextBox();

                    if (!scrolled)
                    {
                        RTBMain.ScrollToCaret();
                        scrolled = true;
                        System.Windows.Forms.Application.DoEvents();
                    }

                    // LOCATION  → → → → →  SPEAKPOINT1  
                    if (synth != null)
                    {
                        synth.Volume = (int)VolumeChanger.Value;
                        synth.SpeakAsync(sentence);
                    }

                    RTBMain.Select(AppSettings.startCharacter, AppSettings.lengthTTS);

                    while (synth?.State == SynthesizerState.Speaking)
                    {
                        DisablePlayButton();
                        System.Windows.Forms.Application.DoEvents();
                    }

                    if (synth != null)
                    {
                        synth.Rate = Globals.User_Settings.TTS_Speed;
                        synth.Volume = (int)VolumeChanger.Value;
                    }
                }

                // End of text reached
                RTBMain.SelectionLength = 0;
                visualLines.ClearLines();
                EnablePlayButton();

                // Handle looping
                // End of text reached
                RTBMain.SelectionLength = 0;
                visualLines.ClearLines();
                EnablePlayButton();

                // Handle looping - but ONLY if user didn't request stop
                if (ChkLoop.Checked && !AppSettings.userRequestedStop)
                {
                    // Reset to beginning
                    RTBMain.SelectionStart = 0;
                    RTBMain.SelectionLength = 0;
                    AppSettings.isSpeaking = true;

                    // Small delay to ensure clean restart
                    System.Windows.Forms.Application.DoEvents();

                    // Start over
                    btnTTSPlay.PerformClick();
                }
                else
                {
                    // Stop if not looping OR user requested stop
                    AppSettings.isSpeaking = false;
                    AppSettings.userRequestedStop = false; // Reset the flag
                    btnTTSStop.PerformClick();
                }
            }
            catch (ObjectDisposedException ode)
            {
                MessageBox.Show("The SpeechSynthesizer object was disposed unexpectedly: " + ode.Message, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                AppSettings.IsSynthActive = false;
            }
        }

        private string CleanText(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, @"\[.*?\]", "")
                       .Replace("\r", "")
                       .Replace("\n", " ")
                       .Replace("  ", " ")
                       .Trim();
        }

        private void InitializeVoiceOptions()
        {
            if (synth != null)
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    if (AppSettings.loadfirst)
                    {
                        //AppSettings.Voice = voice.VoiceInfo.Name;  
                        AppSettings.Voice = Globals.User_Settings.TTS_Voice;   // 03-05-2026
                        cmboVoice.Text = voice.VoiceInfo.Description.Replace("Microsoft", "").Replace("Desktop ", "").Replace("IVONA 2", "").Replace("Harpo", "").Replace("[22kHz]", "").Replace("22kHz", "").Trim();
                        AppSettings.loadfirst = false;
                    }
                    //cmboVoice.Items.Add(voice.VoiceInfo.Description.Replace("Microsoft", "").Replace("Desktop ", "").Replace("IVONA 2", "").Replace("Harpo", "").Replace("[22kHz]", "").Replace("22kHz", "").Trim());
                    cmboVoice.Items.Add(voice.VoiceInfo.Name);
                }

            // Recover setting as it existed when app was closed
            if (Globals.User_Settings.TTS_Voice.Length > 1)
            {
                cmboVoice.SelectedItem = cmboVoice.Items.Cast<string>().FirstOrDefault(x => x == Globals.User_Settings.TTS_Voice);
            }

            //btnTTSStop.Enabled = false;


            this.SpeedChanger.Value = Globals.User_Settings.TTS_Speed;
            this.VolumeChanger.Value = (Int32)Globals.User_Settings.TTS_Volume;
        }

        private static bool IsLineBracketed(string line)
        {
            bool bracketed = false;

            line = line.TrimStart(); line = line.TrimEnd();
            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                bracketed = true;
            }
            return bracketed;
        }


        private VisualLines GetVisualLines(RichTextBox rtb)
        {
            int totalChars = rtb.Text.Length;
            int startPoint = rtb.SelectionStart;
            VisualLines visualLines = new();  // This is a Queue class < int, string > 
            visualLines.AddLine(0, "");

            while (startPoint < totalChars)
            {
                int lineNum = RTBMain.GetLineFromCharIndex(startPoint);
                int visualLineEnd = RTBMain.GetVisualEndCharIndexFromLineIndex(lineNum);

                // Check for images and other controls
                int lineStart = startPoint;
                int lineEnd = visualLineEnd;
                for (int i = 0; i < rtb.Controls.Count; i++)
                {
                    var ctrl = rtb.Controls[i];
                    if (ctrl is PictureBox pictureBox)
                    {
                        int imgStart = rtb.GetCharIndexFromPosition(pictureBox.Location);
                        int imgEnd = imgStart + 1; // Assuming single-character width for simplicity

                        if (imgStart >= lineStart && imgEnd <= lineEnd)
                        {
                            // Adjust lineEnd to account for image
                            lineEnd = Math.Max(lineEnd, imgEnd);
                        }
                    }
                }

                if (lineEnd >= startPoint)
                {
                    //string textLine = rtb.Text.Substring(startPoint, lineEnd - startPoint);
                    string textLine = rtb.Text[startPoint..lineEnd];  // use of range operator [..]
                    if (!IsLineBracketed(textLine) && textLine.Length > 0)
                    {
                        visualLines.AddLine(startPoint, textLine);
                    }
                    startPoint = lineEnd + 1;
                }
            }
            rtb.SelectionStart = startPoint;
            return visualLines;
        }

        private void DisablePlayButton()
        {
            btnTTSPlay.Enabled = false;
            btnTTSStop.Enabled = true;
        }

        private void Synth_SpeakStarted(object? sender, SpeakStartedEventArgs e)
        {
            AppSettings.isSpeaking = true;
        }

        private void Synth_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
        {
            btnTTSPlay.Enabled = true;
            AppSettings.isSpeaking = false;
        }

        private void ScrollRichTextBox()
        {
            // Get the last visible character index
            int lastVisibleCharIndex = this.RTBMain.GetCharIndexFromPosition(new Point(this.RTBMain.ClientSize.Width - 1, this.RTBMain.ClientSize.Height - 1));

            // Get the last visible line index
            int lastVisibleLineIndex = this.RTBMain.GetLineFromCharIndex(lastVisibleCharIndex);

            // Get the current line of the caret
            int currentLineIndex = this.RTBMain.GetLineFromCharIndex(this.RTBMain.SelectionStart);

            // Determine the number of lines to scroll (page down) and subtract 2 lines
            int linesToScroll = (this.RTBMain.Height / this.RTBMain.Font.Height);

            // Check if the current line is at or near the third from the bottom of the visible area
            if (currentLineIndex >= lastVisibleLineIndex - 1)
            {
                // Calculate the new position after scrolling down
                int newLineIndex = currentLineIndex + linesToScroll;

                // Ensure the new line index is within bounds using the helper method
                newLineIndex = EnsureLineIndexInBounds(newLineIndex, this.RTBMain.Lines.Length);

                // Set the caret to the new line position
                int newCharIndex = this.RTBMain.GetFirstCharIndexFromLine(newLineIndex);
                this.RTBMain.SelectionStart = newCharIndex;
                this.RTBMain.ScrollToCaret(); // Scroll to the new caret position
                this.RTBMain.SelectionStart = newCharIndex; // Critical for actually setting cursor.

                // Additional safeguard to check if we've reached the end
                if (newCharIndex == this.RTBMain.TextLength - 1)
                {
                    // We have reached the end of the text, no further scrolling is needed
                    return;
                }
                else
                {
                    //Globals.newCharIndex = newCharIndex;   // 12-29-2025 This may be unnecesary
                }
            }
        }

        private static int EnsureLineIndexInBounds(int lineIndex, int totalLines)
        {
            if (lineIndex >= totalLines)
            {
                return totalLines - 1;
            }
            else if (lineIndex < 0)
            {
                return 0;
            }
            return lineIndex;
        }

        private void CmboVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.Voice = cmboVoice.Text;
            Globals.User_Settings.TTS_Voice = cmboVoice.Text;
        }

        private void SpeedChanger_ValueChanged(object sender, EventArgs e)
        {
            int speed = Convert.ToInt32(this.SpeedChanger.Value);
            Globals.User_Settings.TTS_Speed = Convert.ToInt32(speed);
            RTBMain.Focus();
        }
        #endregion //  VOICE (Text To Speech)


        #region FileOps

        private void HandleMissingFile(string filePath)
        {
            if (filePath.Length > 0)
            {
                fileHistoryManager.RemoveFilePath(filePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                fileHistoryManager.LoadDropDownMenu(frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);

                string caption = "No File Exists";
                MessageBox.Show("This file no longer exists at this location! Removing file name from the list.", caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DisplayFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                this.Text = "Tachufind     Editing: New file";
            }
            else
            {
                this.Text = $"Tachufind     Editing: {filePath}";
            }
        }

        // All AppProperties need to be set before 
        public void SetNewlyOpenedFileSettings()
        {
            try
            {
                SetFrmMainSize();
                //SetFrmMainLocation();  // Removed, this is set in FrmMain_Load  03-14-2026
                Globals.FindStart = 0;
                this.RTBMain.SelectionStart = Globals.FindStart;
                if (Globals.User_Settings.ChkPreserveHighlighting == true)
                {
                    // Persist all Highlighting - Slows down file acessing!  More processing required.
                    PreserveHighlightingAndSetBackColor(Globals.User_Settings.RTBMainBackColor);
                }
                else
                {
                    // Much less processor intense!
                    SetConsistentBackColor(RTBMain, Globals.User_Settings.RTBMainBackColor);
                }
                AppSettings.RtbModified = false;
                RTBMain.ScrollToCaret();
                //dlg.Dispose();
                // this SearchItems.colorBtn0  info needs to be converted after being read from the info file
            }
            // END of public void setNewlyOpenedFileSettings()

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // End of public void setNewlyOpenedFileSettings()

        #endregion // FileOps


        #region ToolStripMenuItems

        // LOCATION   → →  → →   FileToolStripMenuItem_DropDownItemClicked  
        private void FrmMainToolStripMenu_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string tag = e.ClickedItem?.Tag?.ToString() ?? "";

            // Return early if something not related to file paths is clicked or it is a STATIC menu item
            if (e.ClickedItem == null || string.IsNullOrWhiteSpace(e.ClickedItem.Text) ||
                (e.ClickedItem.Tag != null && e.ClickedItem.Tag.Equals("STATIC")))
            {
                return;  // This will be handled by another Click Procedure
            }

            if (tag != "STATIC" && File.Exists(e.ClickedItem.Text))
            {
                AppSettings.filePath = e.ClickedItem.Text;
                // Immediately hide the dropdown after an item is clicked
                e.ClickedItem?.Owner?.Hide();

                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (AppSettings.RtbModified)  // Prompt to save changes if the document is modified
                    {
                        if (!PromptToSaveChanges()) return;
                    }

                    AppSettings.UndoQueue.Clear();
                    RTBMain.Clear();
                    string fileExtension = Path.GetExtension(AppSettings.filePath).ToUpper();
                    if (fileExtension == ".TXT") // Change extension to txt
                    {
                        RTBMain.Text = LoadAndDisplayFile(); // file path is in AppSettings.filePath
                    }
                    else
                    {
                        RTBMain.Rtf = LoadAndDisplayFile(); // file path is in AppSettings.filePath                    
                    }
                    Globals.User_Settings.FilePath = AppSettings.filePath;
                    RTBMain.Refresh();
                    fileHistoryManager.AddFilePath(AppSettings.filePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                    DisplayFilePath(AppSettings.filePath); // Update form title with the file name
                    // Save the initial directory to the last used directory
                    AppSettings.lastUsedDirectory = Path.GetDirectoryName(AppSettings.filePath) ?? string.Empty;
                    AppSettings.FileNotFound = false;

                    // Set any necessary settings after the file is opened
                    SetNewlyOpenedFileSettings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                HandleMissingFile(e.ClickedItem.Text);
                return;
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTBMain != null && RTBMain.SelectionLength > 0)
            {
                RTBMain.Cut();
            }
        }

        private void OnScreenKeyboard_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = @"C:\Windows\System32\osk.exe",
                    UseShellExecute = true // Ensures the process is started with the system shell
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                string caption = "Error Detected";
                MessageBox.Show("Unable to start the on-screen keyboard." + Environment.NewLine + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTBMain != null && RTBMain.SelectionLength > 0)
            {
                RTBMain.Copy();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTBMain?.Paste();
        }

        private void ClearAllHighlightingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete all highlighting?",
                "Confirm Delete",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            // Check if the user clicked OK
            if (result == DialogResult.OK)
                if (result == DialogResult.OK)
                {
                    ClearAllHighlighting();
                }
        }

        private void OpenQnAFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderPath = @AppSettings.Data_Folder;
            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                string header = "Directory not available.";
                string message = "This directory has not yet been created and does not exist.";
                GeneralFns.CustomMessageBox(header, message);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string contents = string.Empty;
            AppSettings.FileNotFound = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (IfDocNotsavedPromptToSave(sender, e)) { return; }

                AppSettings.filePath = FileIO.OpenFileWithDialog();
                contents = FileIO.LoadRtfFile(AppSettings.filePath);

                // Assume file open has been canceled if contents is empty.
                if (contents == null || contents == "")
                {
                    return;
                }

                string fileExtension = Path.GetExtension(AppSettings.filePath).ToUpper();
                if (fileExtension == ".TXT") // Check format
                {
                    RTBMain.Text = contents;
                }
                else
                {
                    RTBMain.Rtf = contents;
                }

                Globals.User_Settings.FilePath = AppSettings.filePath;
                if (AppSettings.FileNotFound == true) // File missing message occurs in FIO
                {
                    HandleMissingFile(AppSettings.filePath);
                    return;
                }
                SetNewlyOpenedFileSettings();
                AppSettings.UndoQueue.Clear(); // Clear any undo's from previous file

                fileHistoryManager.AddFilePath(AppSettings.filePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);

                if (Globals.User_Settings.ChkPreserveHighlighting == true)
                {
                    // Persist all Highlighting - Slows down file acessing!  More processing required.
                    Color backcolor = Globals.User_Settings.RTBMainBackColor;
                    PreserveHighlightingAndSetBackColor(backcolor);   //Globals.User_Settings.RTBMainBackColor);
                }
                else
                {
                    // Much less processor intense!
                    Color backcolor = Globals.User_Settings.RTBMainBackColor;
                    SetConsistentBackColor(RTBMain, backcolor);  // Globals.User_Settings.RTBMainBackColor);
                }

                DisplayFilePath(AppSettings.filePath);

                AppSettings.RtbModified = false;
                this.RTBMain.SelectionStart = 0;
                this.RTBMain.Focus();
                Globals.Current_RTB_withFocus = RTBMain;
                //this.Refresh();

            }
            catch (Exception ex)
            {
                // RemoveItemFromDropDownList(Globals.User_Settings.AppSettings.filePath);
                if (AppSettings.filePath.Length > 0)
                {
                    string caption = "Error Detected";
                    string m = "TsMenuOpenClick - This file no longer exists at this location! Removing file from list." + Environment.NewLine + ex.Message;
                    MessageBox.Show(m, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void GoToWebsiteHelpPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.archeuslore.com/tachufind/search.html";
            GoToWebSite(url);
        }

        private void GoToWebsiteHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string URL = "https://www.archeuslore.com/tachufind";
            GoToWebSite(URL);
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int answer = 0;
                if (AppSettings.RtbModified == true)
                {
                    ToggleFormVisibility();
                    answer = (int)MessageBox.Show("Save changes to this document?", "Unsaved Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (answer == (int)System.Windows.Forms.DialogResult.Yes)
                    {
                        // Save the presently open file
                        SaveFileBasedOnExtensionType(Globals.User_Settings.FilePath);
                    }
                    if (answer == (int)System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                this.RTBMain.Clear();
                //this.RTBMain.SelectAll();
                //this.RTBMain.SelectionFont = new Font("Times New Roman", 22, FontStyle.Regular);
                this.RTBMain.ForeColor = Color.Black;
                this.RTBMain.BackColor = Globals.User_Settings.RTBMainBackColor;
                Globals.User_Settings.FilePath = "";
                this.Text = "Tachufind    Editing: New file";
                AppSettings.NewFileNameAlreadyExistsButHasBeenSaved = false;
                AppSettings.RtbModified = false;
                //this.RTBMain.Focus();
                // AddItemsToDropDownList();  // inserted 8-4-2022
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        void SaveFileAs()
        {
            SaveFileDialog dlg = new();
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dlg.Title = "RTE - Save File";
                dlg.DefaultExt = "rtf";
                dlg.Filter = "Rich Text Format(*.rtf)|*.rtf|Text File(*.txt)|*.txt|" +
                             "Text Document - MS-DOS Format(*.txt)|*.txt|Unicode Text Document(*.utf)|*.utf|" +
                             "Subtitle Files(*.srt)|*.srt|All Files|*.*";
                dlg.FilterIndex = 1;

                // *** THIS IS THE IMPORTANT FIX ***
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    this.Text = "Tachufind     Editing: New file";
                    return;
                }

                // user clicked OK
                Globals.User_Settings.FilePath = dlg.FileName;
                string ext = Path.GetExtension(dlg.FileName).ToUpper();

                if (ext == ".RTF")
                    RTBMain.SaveFile(dlg.FileName, RichTextBoxStreamType.RichText);
                else
                    FileIO.WriteFile(dlg.FileName, RTBMain.Text);

                fileHistoryManager.AddFilePath(dlg.FileName, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);

                AppSettings.RtbModified = false;
                this.Text = "Tachufind     Editing: " + dlg.FileName;

                ShowFrmSavedBriefly(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BackcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog dlg = new();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var _with3 = this.RTBMain;
                    _with3.SelectAll();
                    _with3.BackColor = dlg.Color;
                    _with3.SelectionBackColor = dlg.Color;
                    _with3.SelectionLength = 0;
                    Globals.User_Settings.RTBMainBackColor = dlg.Color;
                    AppSettings.RtbModified = true;

                    Globals.User_Settings.RTBMainBackColor = dlg.Color;

                    // Control whether Highlighting is preserved, this IS processing intensive, and time consuming on large files.
                    if (Globals.User_Settings.ChkPreserveHighlighting == true)
                    {
                        // Persist all Highlighting - Slows down file acessing!  More processing required.
                        PreserveHighlightingAndSetBackColor(Globals.User_Settings.RTBMainBackColor);
                    }
                    else
                    {
                        // Much less processor intense!
                        SetConsistentBackColor(RTBMain, Globals.User_Settings.RTBMainBackColor);
                    }
                    System.Windows.Forms.Application.DoEvents();
                    Globals.User_Settings.RTBMainBackColor = dlg.Color;
                    //Globals.BackColorStd = dlg.Color;
                    Globals.User_Settings.RTBMainBackColor = dlg.Color;
                }
                dlg.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOptions frmOptions = new()
            {
                Visible = true
            };
            frmOptions.Show();
            frmOptions.BringToFront();
        }

        private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RTBMain.WordWrap = !Globals.User_Settings.FrmMainWordWrap;
            Globals.User_Settings.FrmMainWordWrap = this.RTBMain.WordWrap;
            tsMenuWordWrap.Checked = this.RTBMain.WordWrap;
        }

        private void ImageInsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Added 11-23-2011 Was OpenFileDialog02 in previous  build
            OpenFileDialog OpenFileDialog1 = new();

            try
            {
                OpenFileDialog1.Title = "RTE - Insert Image File";
                OpenFileDialog1.DefaultExt = "rtf";
                OpenFileDialog1.Filter = "JPEG Files|*.jpg|GIF Files|*.gif|Bitmap Files|*.bmp";
                OpenFileDialog1.FilterIndex = 1;
                OpenFileDialog1.ShowDialog();

                if (string.IsNullOrEmpty(OpenFileDialog1.FileName))
                    return;

                string strImagePath = OpenFileDialog1.FileName;
                System.Drawing.Image img;
                img = System.Drawing.Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                System.Windows.Forms.DataFormats.Format df;
                df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this.RTBMain.CanPaste(df))
                {
                    this.RTBMain.Paste(df);
                }
                Clipboard.Clear();  // ADDED 2-26-2023
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTBMain.CanRedo | RTBMain.CanRedo)
            {
                RTBMain.Redo();
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTBMain.CanUndo | RTBMain.CanRedo)
            {
                RTBMain.Undo();
                RTBMain.DeselectAll();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnClose.PerformClick();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAboutApp frmAboutApp = new FrmAboutApp();
            frmAboutApp.StartPosition = FormStartPosition.CenterParent; // Ensure it centers relative to its parent
            frmAboutApp.ShowDialog();
        }
        #endregion // ToolStripMenuItems


        #region GeneralCategoryButtons

        private void BtnCharButtons_Click(object sender, EventArgs e)
        {
            ToggleButtonsVisibility();
            this.Validate();
            this.Refresh();
        }

        private void BtnTextToSpeech_Click(object sender, EventArgs e)
        {
            ToggleButtonsVisibility();
            this.Validate();
            this.Refresh();
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            if (Globals.User_Settings.ToggleTTS_SpecChar)
            {
                BtnCharButtons.Visible = true;
                BtnTTSToggleOn.Visible = false;
                PanelTTS.Visible = true;
                PanelTTS.BringToFront();
            }
            else
            {
                BtnCharButtons.Visible = false;
                BtnTTSToggleOn.Visible = true;
                PanelTTS.Visible = false;
                PanelSpChars.Visible = true;
                PanelSpChars.BringToFront();
            }
            this.Validate();
            this.Refresh();
        }

        private void ToggleButtonsVisibility()
        {

            if (BtnCharButtons.Visible)
            {
                BtnCharButtons.Visible = false;
                BtnTTSToggleOn.Visible = true;
                PanelTTS.Visible = false;
                Globals.User_Settings.ToggleTTS_SpecChar = false;
                PanelSpChars.Visible = true;
                PanelSpChars.BringToFront();
            }
            else
            {
                BtnCharButtons.Visible = true;
                BtnTTSToggleOn.Visible = false;
                PanelTTS.Visible = true;
                Globals.User_Settings.ToggleTTS_SpecChar = true;
                PanelTTS.BringToFront();
            }
        }


        private void BtnDefaultColors_Click(object sender, EventArgs e)
        {
            MyUserSettings myUserSettings = new();
            if (myUserSettings.UseDefaultColors)
            {
                FrmMain.SetUserControlledColorSelections();
                myUserSettings.UseDefaultColors = false;
                BtnDefaultColors.Name = "Default Colors";
            }
            else
            {
                this.SetColorDefaults();
                myUserSettings.UseDefaultColors = true;
                BtnDefaultColors.Name = "Use My Colors";
            }
        }

        private void BtnBrackets_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "[]";
            RTBMain.SelectionStart -= 1;
            RTBMain.Focus();
        }

        private void BtnSpExclaim_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "¡";
            RTBMain.Focus();
        }

        private void FrmMainBtnBlack_Click(object sender, EventArgs e)
        {
            if (RTBMain.SelectionLength < 1)
            {
                MessageBox.Show("Text must be selected to change a color.", "Error", MessageBoxButtons.OK);
            }

            // Text is selected
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (RTBMain.SelectionBackColor == Color.Black)
                {
                    RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    RTBMain.SelectionBackColor = Color.Black;
                }
            }
            else
            {
                RTBMain.SelectionColor = Color.Black;
            }
            RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainBtnWhite_Click(object sender, EventArgs e)
        {
            if (RTBMain.SelectionLength < 1)
            {
                MessageBox.Show("Text must be selected to change a color.", "Error", MessageBoxButtons.OK);
            }

            // Text is selected
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == Color.White)
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = Color.White;
                }
            }
            else
            {
                this.RTBMain.SelectionColor = Color.White;
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void BtnInsertSecMarker_Click(object sender, EventArgs e)
        {
            this.RTBMain.Focus();
            int currentPosition = this.RTBMain.SelectionStart;
            this.RTBMain.SelectedText = "§";
            this.RTBMain.SelectionStart = currentPosition + 1;
            this.RTBMain.Focus();
        }


        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            RTBSearchBox.Text = String.Empty;
            RTBSearchBox.Focus();
        }

        private void BoldGreekCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTBMain.BoldGreekCharacters();
        }

        private void BoldRussianCharacters_Click(object sender, EventArgs e)
        {
            RTBMain.BoldRussianCharacters();
        }

        private void BtnSRT_Click(object sender, EventArgs e)
        {
            bool exists = false;
            var form = new Form
            {
                Owner = this
            };
            foreach (Form formCheck in System.Windows.Forms.Application.OpenForms)
            {
                if (formCheck.Name == "FrmInterlinear")
                {
                    exists = true;
                    form = formCheck;
                }
            }

            if (exists)
            {
                form.Visible = true;
                form.Left = Globals.User_Settings.FrmInterlinearLocation.X;
                form.Top = Globals.User_Settings.FrmInterlinearLocation.Y;
                form.BringToFront();
                form.Refresh();
                form.Show();
                form.BringToFront();
            }
            else
            {
                var frmInterlinear = new FrmInterlinear
                {
                    // System.Windows.Forms.Application.OpenForms.Add(frmOCR.Name);
                    Visible = true,
                    Left = Globals.User_Settings.FrmInterlinearLocation.X,
                    Top = Globals.User_Settings.FrmInterlinearLocation.Y
                };
                frmInterlinear.BringToFront();
                frmInterlinear.Refresh();
            }
        }

        private void FrmMainColorBtn1_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn1.BackColor = ColorManager.GetColor("C1");  // Globals.User_Settings.Color01;
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (RTBMain.SelectionBackColor == ColorManager.GetColor("C1"))  // Globals.User_Settings.Color01)
                {
                    RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    RTBMain.SelectionBackColor = ColorManager.GetColor("C1");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C1");
                // Restore backcolor requires Control Z
            }
            int cursor = this.RTBMain.SelectionStart;
            this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn2_Click(object sender, EventArgs e)
        {
            FrmMainColorBtn2.BackColor = ColorManager.GetColor("C2");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (RTBMain.SelectionBackColor == ColorManager.GetColor("C2"))
                {
                    RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    RTBMain.SelectionBackColor = ColorManager.GetColor("C2");
                }
            }
            else
            {
                RTBMain.SelectionColor = ColorManager.GetColor("C2");
                // Restore backcolor requires Control Z
            }
            RTBMain.Select(RTBMain.SelectionStart + RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn3_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn3.BackColor = ColorManager.GetColor("C3");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C3"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C3");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C3");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn4_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn4.BackColor = ColorManager.GetColor("C4");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C4"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C4");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C4");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn5_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn5.BackColor = ColorManager.GetColor("C5");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C5"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C5");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C5");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn6_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn6.BackColor = ColorManager.GetColor("C6");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C6"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C6");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C6");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn7_Click(object sender, EventArgs e)
        {
            this.FrmMainColorBtn7.BackColor = ColorManager.GetColor("C7");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C7"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C7");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C7");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn8_Click(object sender, EventArgs e)
        {
            FrmMainColorBtn8.BackColor = ColorManager.GetColor("C8");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C8"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C8");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C8");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn9_Click(object sender, EventArgs e)
        {
            FrmMainColorBtn9.BackColor = ColorManager.GetColor("C9");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C9"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C9");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C9");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void FrmMainColorBtn10_Click(object sender, EventArgs e)
        {
            FrmMainColorBtn10.BackColor = ColorManager.GetColor("C10");
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (this.RTBMain.SelectionBackColor == ColorManager.GetColor("C10"))
                {
                    this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    this.RTBMain.SelectionBackColor = ColorManager.GetColor("C10");
                }
            }
            else
            {
                this.RTBMain.SelectionColor = ColorManager.GetColor("C10");
                // Restore backcolor requires Control Z
            }
            this.RTBMain.Select(this.RTBMain.SelectionStart + this.RTBMain.SelectionLength, 0);
            RTBMain.Focus();
        }

        private void BtnShort_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetShort(RTBMain);
        }

        private void BtnMacron_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetMacron(RTBMain);
        }

        private void BtnGrave_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetGraveAccent(RTBMain);
        }

        private void BtnAcute_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetAcuteAccent(RTBMain);
        }

        private void BtnUmlaut_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetUmlaut(RTBMain);
        }

        private void BtnCircumflex_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetCircumflex(RTBMain);
        }

        private void FrmMainColorBtn1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn1.BackColor = this.FrmMainColorBtn1.BackColor = newColor;
                    FrmMainColorBtn1.ForeColor = newColor;
                    Globals.User_Settings.Color1 = newColor;
                    ColorManager.SetColor("C1", newColor);
                }
            }
        }

        private void FrmMainColorBtn2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn2.BackColor = this.FrmMainColorBtn2.BackColor = newColor;
                    FrmMainColorBtn2.ForeColor = newColor;
                    Globals.User_Settings.Color2 = newColor;
                    ColorManager.SetColor("C2", newColor);
                }
            }
        }

        private void FrmMainColorBtn3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn3.BackColor = this.FrmMainColorBtn3.BackColor = newColor;
                    FrmMainColorBtn3.ForeColor = newColor;
                    Globals.User_Settings.Color3 = newColor;
                    ColorManager.SetColor("C3", newColor);
                }
            }
        }

        private void FrmMainColorBtn4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn4.BackColor = this.FrmMainColorBtn4.BackColor = newColor;
                    FrmMainColorBtn4.ForeColor = newColor;
                    Globals.User_Settings.Color4 = newColor;
                    ColorManager.SetColor("C4", newColor);
                }
            }
        }

        private void FrmMainColorBtn5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn5.BackColor = this.FrmMainColorBtn5.BackColor = newColor;
                    FrmMainColorBtn5.ForeColor = newColor;
                    Globals.User_Settings.Color5 = newColor;
                    ColorManager.SetColor("C5", newColor);
                }
            }
        }

        private void FrmMainColorBtn6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn6.BackColor = this.FrmMainColorBtn6.BackColor = newColor;
                    FrmMainColorBtn6.ForeColor = newColor;
                    Globals.User_Settings.Color6 = newColor;
                    ColorManager.SetColor("C6", newColor);
                }
            }
        }

        private void FrmMainColorBtn7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn7.BackColor = this.FrmMainColorBtn7.BackColor = newColor;
                    FrmMainColorBtn7.ForeColor = newColor;
                    Globals.User_Settings.Color7 = newColor;
                    ColorManager.SetColor("C7", newColor);
                }
            }
        }

        private void FrmMainColorBtn8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn8.BackColor = this.FrmMainColorBtn8.BackColor = newColor;
                    FrmMainColorBtn8.ForeColor = newColor;
                    Globals.User_Settings.Color8 = newColor;
                    ColorManager.SetColor("C8", newColor);
                }
            }
        }

        private void FrmMainColorBtn9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn9.BackColor = this.FrmMainColorBtn9.BackColor = newColor;
                    FrmMainColorBtn9.ForeColor = newColor;
                    Globals.User_Settings.Color9 = newColor;
                    ColorManager.SetColor("C9", newColor);
                }
            }
        }

        private void FrmMainColorBtn10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color newColor = colorDialog1.Color;
                    FrmMainColorBtn10.BackColor = this.FrmMainColorBtn10.BackColor = newColor;
                    FrmMainColorBtn10.ForeColor = newColor;
                    Globals.User_Settings.Color10 = newColor;
                    ColorManager.SetColor("C10", newColor);
                }
            }
        }

        private void AncientGreekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.AncientGreekShortcuts);
        }


        private void ArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.ArabicShortcuts);
        }

        // EnglishShortcuts
        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.EnglishShortcuts);
        }

        // FrenchShortcuts
        private void FrenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.FrenchShortcuts);
        }

        // GermanShortcuts
        private void GermanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.GermanShortcuts);

        }

        // ItalianShortcuts
        private void ItalianToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.ItalianShortcuts);
        }

        //  LatinShortcuts
        private void LatinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.LatinShortcuts);
        }

        // SpanishShortcuts
        private void SpanishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.SpanishShortcuts);
        }

        // RussianShortcuts
        private void RussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowShortcutsForm(AppConstants.RussianShortcuts);
        }

        private void BtnCedilla_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ç";
            RTBMain.Focus();
        }

        private void BtnTsus_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ß";
            RTBMain.Focus();
        }

        private void BtnSpNye_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ñ";
            RTBMain.Focus();
        }

        private void BtnSpQuestionMk_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "¿";
            RTBMain.Focus();
        }

        private void BtnImageInsert_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            try
            {
                openFileDialog.Title = "RTE - Insert Image File";
                openFileDialog.DefaultExt = "rtf";
                openFileDialog.Filter = "JPEG Files|*.jpg|GIF Files|*.gif|Bitmap Files|*.bmp|Files|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.ShowDialog();

                if (string.IsNullOrEmpty(openFileDialog.FileName))
                    return;

                string imagePath = openFileDialog.FileName;
                System.Drawing.Image img;
                if (System.Drawing.Image.FromFile(imagePath) != null)
                {
                    img = System.Drawing.Image.FromFile(imagePath);
                    Clipboard.SetDataObject(img);
                }
                else
                {
                    MessageBox.Show("Image is corrupt or not loadable.");
                    return;
                }

                System.Windows.Forms.DataFormats.Format df;
                if (DataFormats.GetFormat(DataFormats.Bitmap) != null)
                {
                    df = DataFormats.GetFormat(DataFormats.Bitmap);
                    if (RTBMain.CanPaste(df))
                    {
                        RTBMain.Paste(df);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Clipboard.Clear();  // ADDED 2-26-2023
        }


        private void BtnBoundryMarker_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "¦¦¦";
            RTBMain.Focus();
        }

        private void BtnDeleteBoundaryMarkers_Click(object sender, EventArgs e)
        {
            AppSettings.currentCursorPosition = RTBMain.SelectionStart;
            if (RTBMain.Rtf != null)
            {
                RTBMain.Rtf = RTBMain.Rtf.Replace("\\'a6\\'a6\\'a6", string.Empty);
                AppSettings.EOF = RTBMain.Text.Length;
                RTBMain.SelectionStart = AppSettings.currentCursorPosition;
            }
        }


        private void BtnSingleQuotes_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ʽʼ";
            RTBMain.SelectionStart -= 1; // Move cursor between brackets
            RTBMain.Focus();
        }

        private void BtnLongLine_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "—";
            RTBMain.Focus();
        }

        private void BtnInsertFancyQuotes_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "“”";
            RTBMain.SelectionStart -= 1;
            RTBMain.Focus();
        }

        private void BtnInsertParenthesis_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "()";
            RTBMain.SelectionStart -= 1;
            RTBMain.Focus();
        }

        private void BtnRemHighlight_Click(object sender, EventArgs e)
        {
            ClearAllHighlighting();
        }

        private void BtnDegree_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "°";
            RTBMain.Focus();
        }

        private void BtnInsertFrenchQuotes_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "«»"; // Insert brackets
            RTBMain.SelectionStart -= 1; // Move cursor between brackets
            RTBMain.Focus();
        }

        private void BtnFrenchLowerCase_ae_Click(object sender, EventArgs e)
        {
            // æ
            RTBMain.SelectedText = "æ";
            RTBMain.Focus();
        }

        private void BtnFrenchLowerCase_oe_Click(object sender, EventArgs e)
        {
            // œ
            RTBMain.SelectedText = "œ";
            RTBMain.Focus();
        }

        private void BtnSuperscript_Click(object sender, EventArgs e)
        {
            if (RTBMain != null && RTBMain.SelectedText.Length > 0 && RTBMain.SelectionFont != null)
            {
                float fontSize = RTBMain.SelectionFont.Size;
                string fontName = RTBMain.SelectionFont.Name;

                int selLen = RTBMain.SelectionLength;

                // Apply superscript formatting
                GeneralFns.CreateSuperorSubScript(RTBMain, 8, 12);

                // Move the cursor to the end of the selected text
                RTBMain.SelectionStart += selLen;
                RTBMain.SelectedText = " ";  // Add a space

                // Position the cursor at the space
                int selStart = RTBMain.SelectionStart;
                RTBMain.Select(selStart - 1, 1);

                // Reset the font and char offset to normal
                RTBMain.SelectionCharOffset = 0;
                RTBMain.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);

                // Ensure the selection is collapsed and the cursor is placed correctly
                RTBMain.SelectionStart = selStart;
                RTBMain.SelectionLength = 0;
                RTBMain.Focus();
            }
        }

        private void BtnSubscript_Click(object sender, EventArgs e)
        {
            if (RTBMain != null && RTBMain.SelectedText.Length > 0 && RTBMain.SelectionFont != null)
            {
                float fontSize = RTBMain.SelectionFont.Size;
                string fontName = RTBMain.Font.Name;

                int selLen = RTBMain.SelectionLength;
                GeneralFns.CreateSuperorSubScript(RTBMain, -4, 12);

                RTBMain.SelectionStart += selLen;
                RTBMain.SelectedText = " ";
                int selStart = RTBMain.SelectionStart;
                RTBMain.Select(selStart - 1, 1);
                RTBMain.SelectionCharOffset = 0;
                RTBMain.SelectionFont = new System.Drawing.Font(fontName, fontSize, FontStyle.Regular);

                // Fix for advancing one space
                RTBMain.SelectionStart = selStart;

                RTBMain.SelectionLength = 0;
                RTBMain.Focus();
            }
        }

        private void BtnCopyright_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "©";
            RTBMain.Focus();
        }

        private void BtnPlusMinus_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "±";
            RTBMain.Focus();
        }

        private void BtnArrowR_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "→";
            RTBMain.Focus();
        }

        private void BtnIntegers_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ℤ";
            RTBMain.Focus();
        }

        private void BtnNaturals_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ℕ";
            RTBMain.Focus();
        }

        private void BtnElement_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "\u03F5";
            RTBMain.Focus();
        }

        private void BtnReals_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ℝ";
            RTBMain.Focus();
        }

        private void BtnAnd_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "∧";
            RTBMain.Focus();
        }

        private void BtnOr_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "∨";
            RTBMain.Focus();
        }

        private void BtnIdenticalto_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "≡";
            RTBMain.Focus();
        }

        private void BtnPartialDifferential_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "\u2202";
            RTBMain.Focus();
        }

        private void BtnDifferential_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ⅅ";
            RTBMain.Focus();
        }

        private void BtnIntegral_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "∫";
            RTBMain.Focus();
        }

        private void BtnEuler_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ℯ";
            RTBMain.Focus();
        }

        private void BtnDotProd_Click(object sender, EventArgs e)
        {
            if (RTBMain != null && RTBMain.SelectionFont != null)
            {
                RTBMain.SelectedText = "˙";
                int cursorLocation = RTBMain.SelectionStart;
                RTBMain.SelectionStart = cursorLocation - 1;
                RTBMain.SelectionLength = 1;
                RTBMain.SelectionFont = new System.Drawing.Font(RTBMain.SelectionFont, FontStyle.Bold);
                RTBMain.SelectionStart = cursorLocation;
                RTBMain.SelectionLength = 0;
                RTBMain.Focus();
            }
        }

        private void BtnInfinity_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "∞";
            RTBMain.Focus();
        }

        private void BtnSqrt_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "\u221A";
            RTBMain.Focus();
        }

        private void BtnIntersect_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "\u2229";
            RTBMain.Focus();
        }

        private void BtnUnion_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "\u222A";
            RTBMain.Focus();
        }

        private void BtnDotR_Click(object sender, EventArgs e)
        {
            System.Drawing.Font currentfont = RTBMain.Font;
            RTBMain.SelectionFont = new System.Drawing.Font("Cambria", 18);  // FontStyle.Bold
            RTBMain.SelectedText = "⟨⟩";
            RTBMain.SelectionFont = currentfont;
            RTBMain.Focus();
            RTBMain.SelectionStart -= 1;
        }

        private void BtnAngle_Click(object sender, EventArgs e)
        {
            System.Drawing.Font currentfont = RTBMain.Font;
            RTBMain.SelectionFont = new System.Drawing.Font("Arial Unicode MS", 18);  // FontStyle.Bold
            RTBMain.SelectedText = "∠";
            RTBMain.Font = currentfont;
            RTBMain.Focus();
        }

        private void BtnGte_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "≥";
            RTBMain.Focus();
        }

        private void BtnLte_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "≤";
            RTBMain.Focus();
        }

        private void BtnApprox_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "≈";
            RTBMain.Focus();
        }

        private void BtnFrall_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "Ɐ";
            RTBMain.Focus();
        }

        private void BtnComplex_Click(object sender, EventArgs e)
        {
            RTBMain.SelectedText = "ℂ";
            RTBMain.Focus();
        }

        private void BtnSection_Click(object sender, EventArgs e)
        {
            RTBSearchBox.Focus();
            RTBSearchBox.Text = RTBSearchBox.Text.ToUpper();
            RTBSearchBox.Refresh();

            // Handle preconditions
            if (HandlePreconditionsAndChecks())
            {
                return;
            }

            // Preserve the position of the cursor before the search
            // int localPropertyCursorPreSearchPosition = RTBMain.SelectionStart;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Get the section number from the search text
                string sectionNumber = RTBSearchBox.Text.Replace("§", "");
                AppSettings.SectionNumber = int.Parse(sectionNumber);

                // Find the start and end indexes of the section
                Tuple<int, int> index = FindSectionBreadth(RTBMain, AppSettings.SectionNumber);

                // Ensure valid section indexes are found
                if ((index.Item2 - index.Item1) < 1)
                {
                    return;
                }

                // Get the formatted text between the start and end indexes
                string formattedText = GetFormattedTextBetweenIndexes(RTBMain, index.Item1, index.Item2);

                // Manage the scroll position
                RichTextBoxScrollManager richTextBoxScrollManager = new();
                int savedScrollIndex = RichTextBoxScrollManager.GetTopVisibleCharIndex(RTBMain);

                // Display the formatted section
                GetFrmSection(formattedText, savedScrollIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void BtnEofSearch_Click(object sender, EventArgs e)
        {
            // Process search rejection cases
            if (Control.ModifierKeys == Keys.Control) { return; } // Should not work when control key is down
            if (string.IsNullOrEmpty(RTBSearchBox.Text) || string.IsNullOrEmpty(RTBMain.Text))
            {
                MessageBox.Show("Search text box or main text box is empty.");
                return;
            }
            // End  Process search rejection cases

            try
            {
                RTBSearchBox.Focus();
                if (panel_BtnEOFSearch._firstPass)
                {
                    PopulateGlobals_reverseResultsSearchStack(RTBSearchBox.Text);  // this is used because it searches the text forward, then retrieves the indexes as a reverse search.
                    panel_BtnEOFSearch._firstPass = false;
                }

                // Obtain the index of the search string in RichTextBox.
                panel_BtnEOFSearch._reverseSearchIndex = GetSearchIndex();

                // Determine whether the text was found in RichTextBox.
                if (panel_BtnEOFSearch._reverseSearchIndex > 0)
                {
                    // Return the index to the specified search text.
                    this.RTBMain.Select(panel_BtnEOFSearch._reverseSearchIndex, RTBSearchBox.Text.Length);
                }
                else
                {
                    string caption = "Not Found";
                    MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
                    panel_BtnEOFSearch._firstPass = true;
                }
                RTBMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFindRevFrmCursor_Click(object sender, EventArgs e)
        {
            int index = 0;

            try
            {
                int end = this.RTBMain.SelectionStart - RTBSearchBox.Text.Length;

                // Ensure that a search string is specified and that the starting point is valid.
                if (end >= 0)
                {
                    index = this.RTBMain.Find(RTBSearchBox.Text, 0, end, RichTextBoxFinds.Reverse);
                }

                if (index < 0)
                {
                    string caption = "Not Found";
                    MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
                    BtnReturn.PerformClick();
                    this.RTBMain.SelectionLength = 0;
                    this.RTBMain.Focus();
                    return;
                }
                RTBMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFindFwdFrmCursor_Click(object sender, EventArgs e)
        {
            int index = 0;

            try
            {
                panel_BtnFindFwdFrmCursorPointer = this.RTBMain.SelectionStart + RTBSearchBox.Text.Length;
                AppSettings.EOF = this.RTBMain.Text.Length;
                // Ensure that a search string and a valid starting point are specified.
                if (AppSettings.EOF > panel_BtnFindFwdFrmCursorPointer)
                {
                    index = this.RTBMain.Find(RTBSearchBox.Text, panel_BtnFindFwdFrmCursorPointer, AppSettings.EOF, RichTextBoxFinds.None);
                }

                if ((index < 0))
                {
                    string caption = "Not Found";
                    MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
                    BtnReturn.PerformClick();
                    this.RTBMain.SelectionLength = 0;
                    this.RTBMain.Focus();
                    return;
                }
                RTBMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            int previousLocationOfCursor = 1;

            // this just pulls the old cursor locations out of the array from top down
            try
            {
                this.RTBMain.Focus();
                if (returnButtonStack.Count > 0)
                {
                    previousLocationOfCursor = returnButtonStack.Pop();
                }
                if (previousLocationOfCursor < 1)
                {
                    previousLocationOfCursor = 1;
                }
                if (this.RTBMain.TextLength >= previousLocationOfCursor)
                {
                    this.RTBMain.SelectionStart = previousLocationOfCursor;
                    this.RTBMain.SelectionLength = 0;
                    if ((AppSettings.clickNum < AppSettings.ReturnButtonMemoryPositions))
                    {
                        AppSettings.clickNum++;
                    }
                    else
                    {
                        AppSettings.clickNum = 0;
                    }
                }
                else
                {
                    previousLocationOfCursor = this.RTBMain.TextLength - 12;
                    if ((AppSettings.clickNum < AppSettings.ReturnButtonMemoryPositions))
                    {
                        AppSettings.clickNum++;
                    }
                    else
                    {
                        AppSettings.clickNum = 0;
                    }
                }
                this.RTBMain.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Check if the file path is empty and prompt to save as a new file
                if (File.Exists(Globals.User_Settings.FilePath) & AppSettings.NewFileNameAlreadyExistsButHasBeenSaved == false)
                {
                    int answer = (int)MessageBox.Show("File " + Globals.User_Settings.FilePath + " already exits.  Do you want to overwrite?", "Saving Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == (int)System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

                AppSettings.NewFileNameAlreadyExistsButHasBeenSaved = true;
                if (string.IsNullOrEmpty(Globals.User_Settings.FilePath))
                {
                    SaveAsToolStripMenuItem_Click(this, e);
                    return;
                }

                // Determine the file extension and save accordingly
                string strExt = System.IO.Path.GetExtension(Globals.User_Settings.FilePath);

                switch (strExt.ToUpper())
                {
                    case ".RTF":
                        RTBMain.SaveFile(Globals.User_Settings.FilePath);
                        fileHistoryManager.AddFilePath(Globals.User_Settings.FilePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                        fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);
                        ShowFrmSavedBriefly(this);
                        break;
                    default:
                        FileIO.WriteFile(Globals.User_Settings.FilePath, RTBMain.Text);
                        fileHistoryManager.AddFilePath(Globals.User_Settings.FilePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
                        fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);
                        ShowFrmSavedBriefly(this);
                        break;
                }
                AppSettings.RtbModified = false;
                RTBMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void BtnFirstFindSearch_Click(object sender, EventArgs e)
        {
            // Handle search rejection cases
            if (Control.ModifierKeys == Keys.Control) { return; } // Should not work when control key is down
            if (string.IsNullOrEmpty(RTBSearchBox.Text) || string.IsNullOrEmpty(RTBMain.Text))
            {
                MessageBox.Show("Search text box or main text box is empty.");
                return;
            }  // End  Handle search rejection cases

            // TODO Consider making a specific search class for panel BF and EOF searches.
            panel_BtnBFSearch.FindString = RTBSearchBox.Text;

            try
            {
                RTBSearchBox.Focus();
                if (panel_BtnBFSearch.firstPass)
                {
                    panel_BtnBFSearch.firstPass = false;
                    panel_BtnBFSearch.forwardSearchIndex = 0; // This is needed to sum the searchIndex + searchStrLength thus preventing search overrun
                }
                panel_BtnBFSearch.lengthOfTextToSearch = this.RTBMain.Text.Length;
                int outerLimitCheck = panel_BtnBFSearch.forwardSearchIndex + RTBSearchBox.Text.Length;

                // Ensure that a search string and a valid starting point are specified.
                if (panel_BtnBFSearch.lengthOfTextToSearch > RTBSearchBox.Text.Length)
                {
                    // Check to avoid over-run
                    if (panel_BtnBFSearch.lengthOfTextToSearch > panel_BtnBFSearch.forwardSearchIndex)
                    {
                        // Obtain the location of the search string in RichTextBox.
                        panel_BtnBFSearch.forwardSearchIndex = GetNextForwardSearchIndex(panel_BtnBFSearch.FindString);

                        // Determine whether the text was found in RichTextBox.
                        if (panel_BtnBFSearch.forwardSearchIndex >= 0)
                        {
                            if (RTBMain.Text.Length > outerLimitCheck)
                            {
                                // Return the index to the specified search text.
                                RTBMain.Select(panel_BtnBFSearch.forwardSearchIndex, RTBSearchBox.Text.Length);
                                RTBMain.Focus();

                                // Update the next starting position for the search.
                                panel_BtnBFSearch.forwardSearchIndex += RTBSearchBox.Text.Length;
                            }
                        }
                        else
                        {
                            string caption = "Not Found";
                            MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
                            panel_BtnBFSearch.firstPass = true;
                            panel_BtnBFSearch.forwardSearchIndex = 0;
                        }
                    }
                }
                RTBMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Dispose of synthesized resources
            //Synth?.Dispose();

            Point pt = new(this.Left, this.Top);
            Globals.User_Settings.FrmMainLocation = pt;

            // Loop through all open forms and close them except FrmMain
            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
                var form = System.Windows.Forms.Application.OpenForms[i];
                if (form != null && form.Name != "FrmMain")
                {
                    form.Close();
                }
            }

            // Close the current form
            this.Close();
        }


        private void BtnColor_Click(object sender, EventArgs e)
        {
            bool exists = false;
            FrmColor? frmColor = null;

            foreach (Form formCheck in Application.OpenForms)
            {
                if (formCheck.Name == "FrmColor")
                {
                    exists = true;
                    frmColor = (FrmColor)formCheck;
                }
            }

            if (exists && frmColor != null)
            {
                frmColor.Visible = true;
                frmColor.Show();
                frmColor.BringToFront();
                frmColor.Left = Globals.User_Settings.FrmColorLocation.X;
                frmColor.Top = Globals.User_Settings.FrmColorLocation.Y;
            }
            else
            {
                frmColor = FrmColor.Instance; // Use Singleton instance
                frmColor.Left = Globals.User_Settings.FrmColorLocation.X;
                frmColor.Top = Globals.User_Settings.FrmColorLocation.Y;
                frmColor.BackColor = Color.FromArgb(200, 190, 200);

                frmColor.Show();
                frmColor.BringToFront();
            }
        }


        private void BtnOCR_Click(object sender, EventArgs e)
        {
            bool exists = false;
            Form form = new();
            foreach (Form formCheck in System.Windows.Forms.Application.OpenForms)
            {
                if (formCheck.Name == "FrmOCR")
                {
                    exists = true;
                    form = formCheck;
                }
            }

            if (exists)
            {
                form.Visible = true;
                form.Show();
                form.BringToFront();
                form.Left = Globals.User_Settings.FrmOCRLocation.X;
                form.Top = Globals.User_Settings.FrmOCRLocation.Y;
            }
            else
            {
                FrmOCR frmOCR = new()
                {
                    Visible = true,
                    Left = Globals.User_Settings.FrmOCRLocation.X,
                    Top = Globals.User_Settings.FrmOCRLocation.Y
                };

                frmOCR.Visible = true;
                frmOCR.Refresh();
                frmOCR.BringToFront();
                frmOCR.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void BtnQuiz_Click(object sender, EventArgs e)
        {
            bool exists = false;
            Form form = new();
            foreach (Form formCheck in System.Windows.Forms.Application.OpenForms)
            {
                if (formCheck.Name == "FrmQuiz")
                {
                    exists = true;
                    form = formCheck;
                }
            }

            if (exists)
            {
                form.Visible = true;
                form.Show();
                form.BringToFront();
                form.Left = Globals.User_Settings.FrmQuizLocation.X;
                form.Top = Globals.User_Settings.FrmQuizLocation.Y;
            }
            else
            {
                FrmQuiz frmQuiz = new()
                {
                    Visible = true,
                    Left = Globals.User_Settings.FrmQuizLocation.X,
                    Top = Globals.User_Settings.FrmQuizLocation.Y
                };

                frmQuiz.BringToFront();
                frmQuiz.RTBQuestion.BackColor = Globals.User_Settings.RTBMainBackColor;
                frmQuiz.RTBAnswer.BackColor = Globals.User_Settings.RTBMainBackColor;
                frmQuiz.RTBQuestion.Refresh();
                frmQuiz.RTBAnswer.Refresh();
            }
        }

        private void BtnBold_Click(object sender, EventArgs e)
        {
            try
            {
                if (RTBMain.SelectionLength > 0) // Ensure that there is selected text
                {
                    // Get the current selection font. If null, use a default font.
                    System.Drawing.Font currentFont = RTBMain.SelectionFont ?? new System.Drawing.Font("Times New Roman", 20);

                    // Toggle the bold style.
                    FontStyle newFontStyle;
                    if (currentFont.Style.HasFlag(FontStyle.Bold))
                    {
                        newFontStyle = currentFont.Style & ~FontStyle.Bold; // Remove bold
                    }
                    else
                    {
                        newFontStyle = currentFont.Style | FontStyle.Bold; // Add bold
                    }

                    // Apply the new font style to the selected text.
                    RTBMain.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    RTBMain.Focus(); // Optional, to ensure the RichTextBox has focus after the operation
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnItalic_Click(object sender, EventArgs e)
        {

            try
            {
                if (RTBMain.SelectionLength > 0) // Ensure that there is selected text
                {
                    // Get the current selection font. If null, use a default font.
                    System.Drawing.Font currentFont = RTBMain.SelectionFont ?? new System.Drawing.Font("Times New Roman", 20);

                    // Toggle the italic style.
                    FontStyle newFontStyle;
                    if (currentFont.Style.HasFlag(FontStyle.Italic))
                    {
                        newFontStyle = currentFont.Style & ~FontStyle.Italic; // Remove italic
                    }
                    else
                    {
                        newFontStyle = currentFont.Style | FontStyle.Italic; // Add italic
                    }

                    // Apply the new font style to the selected text.
                    RTBMain.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    RTBMain.Focus(); // Optional, to ensure the RichTextBox has focus after the operation
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnUnderline_Click(object sender, EventArgs e)
        {
            try
            {
                if (RTBMain.SelectionLength > 0) // Ensure that there is selected text
                {
                    // Get the current selection font. If null, use a default font.
                    System.Drawing.Font currentFont = RTBMain.SelectionFont ?? new System.Drawing.Font("Times New Roman", 20);

                    // Toggle the underline style.
                    FontStyle newFontStyle;
                    if (currentFont.Style.HasFlag(FontStyle.Underline))
                    {
                        newFontStyle = currentFont.Style & ~FontStyle.Underline; // Remove underline
                    }
                    else
                    {
                        newFontStyle = currentFont.Style | FontStyle.Underline; // Add underline
                    }

                    // Apply the new font style to the selected text.
                    RTBMain.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    RTBMain.Focus(); // Optional, to ensure the RichTextBox has focus after the operation
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnCapDeCap_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedLen = RTBMain.SelectedText.Length;
                int selStart = RTBMain.SelectionStart;

                if (selectedLen > 0) // Ensure there is selected text
                {
                    // Check the case of the first character in the selected text
                    char check = RTBMain.SelectedText[0];

                    if (char.IsUpper(check))
                    {
                        // Convert the selected text to lowercase
                        RTBMain.SelectedText = RTBMain.SelectedText.ToLower();
                    }
                    else
                    {
                        // Convert the selected text to uppercase
                        RTBMain.SelectedText = RTBMain.SelectedText.ToUpper();
                    }

                    // Restore the selection
                    RTBMain.Select(selStart, selectedLen);
                    RTBMain.Focus(); // Optional, to ensure the RichTextBox has focus after the operation
                }
                else
                {
                    // Optionally handle the case where no text is selected
                    MessageBox.Show("To change the case, select some text first.", "No Text Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // GeneralCategoryButtons


        #region GenearalUtilityFunctions

        // THis is for showing ALL keyboard shortcuts windows, 
        // with the shortcuts for the language needed passed in
        internal static void ShowShortcutsForm(string rtfContent)
        {
            FrmKBShortcuts frmShortcuts = new FrmKBShortcuts
            {
                Size = new Size(1000, 1000),
                StartPosition = FormStartPosition.CenterParent,
            };
            frmShortcuts.RTBKBShortcuts.Rtf = rtfContent;
            frmShortcuts.RTBKBShortcuts.SelectAll();
            frmShortcuts.RTBKBShortcuts.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            frmShortcuts.RTBKBShortcuts.SelectionLength = 0;
            frmShortcuts.Show();
        }

        // Used ONLY for loading an item clicked in the drop-down menu
        private string LoadAndDisplayFile() // IF this is called fileExists has already been checked
        {
            // Determine file extension and load file content
            string strExt = Path.GetExtension(AppSettings.filePath)?.ToUpper() ?? "";
            if (strExt == ".SRT") // Change extension to txt
            {
                AppSettings.filePath = FileIO.SetExtensionToRtf(AppSettings.filePath);
            }

            // Update File History (IF this is called fileExists has already been checked)
            fileHistoryManager.AddFilePath(AppSettings.filePath, frmMainToolStripMenu, AppConstants.STATIC_MENU_ITEMS);
            fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);
            Globals.User_Settings.FilePath = AppSettings.filePath;
            Globals.User_Settings.Save();

            AppSettings.RtbModified = false;

            if (strExt == ".RTF")
            {
                return FileIO.LoadFileContents(AppSettings.filePath);
            }
            if (strExt == ".TXT") // Change extension to txt
            {
                return FileIO.LoadRtfFile(AppSettings.filePath);
            }
            else
            {
                return FileIO.LoadFileContents(AppSettings.filePath);
            }
        }

        private bool PromptToSaveChanges()  // For DropDownItemClick   Just YES and NO options
        {
            ToggleFormVisibility();
            var answer = MessageBox.Show("Save changes to this document?", "Unsaved Document",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                SaveFileBasedOnExtensionType(Globals.User_Settings.FilePath);
            }
            return true;
        }

        private void GoToWebSite(string url)
        {
            try
            {
                // Open the URL in the default web browser
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Ensures the URL is opened in the default browser
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                string caption = "Error Detected";
                MessageBox.Show("Unable to proceed to this web address. You may not have a default web browser designated." + Environment.NewLine + ex.Message, caption, MessageBoxButtons.OK);
            }
        }

        private void PanelColor_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.FrmMainColorBtn1, AppSettings.ToolTip1);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn2, AppSettings.ToolTip2);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn3, AppSettings.ToolTip3);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn4, AppSettings.ToolTip4);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn5, AppSettings.ToolTip5);
            // Start at beginning of BOTTOM buttons
            this.toolTip1.SetToolTip(this.FrmMainColorBtn6, AppSettings.ToolTip6);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn7, AppSettings.ToolTip7);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn8, AppSettings.ToolTip8);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn9, AppSettings.ToolTip9);
            this.toolTip1.SetToolTip(this.FrmMainColorBtn10, AppSettings.ToolTip10);
        }

        private void ClearAllHighlighting()
        {
            // Backup the current selection
            int originalSelectionStart = RTBMain.SelectionStart;
            int originalSelectionLength = RTBMain.SelectionLength;

            // Select all text
            RTBMain.SelectAll();

            // Set the background color to the RichTextBox's default back color (or any specific color)
            RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;

            // Restore the original selection
            RTBMain.Select(originalSelectionStart, originalSelectionLength);
        }

        public static string GetFormattedTextBetweenIndexes(RichTextBox richTextBox, int startIndex, int endIndex)
        {
            // Ensure valid indices
            if (startIndex < 0 || endIndex < 0 || startIndex > endIndex || endIndex > richTextBox.Text.Length)
            {
                return string.Empty;
            }

            // Select the text in the specified range
            richTextBox.Select(startIndex, endIndex - startIndex);

            // Get the selected RTF formatted text
            string selectedRtf = richTextBox.SelectedRtf;

            // Deselect the text
            richTextBox.DeselectAll();

            return selectedRtf;
        }


        private void GetFrmSection(string formattedText, int savedScrollIndex)
        {
            // Open the new window and fill it
            var frmSection = new FrmSection();
            frmSection.Show();
            frmSection.RTBSection.Rtf = formattedText;
            frmSection.BackColor = Globals.User_Settings.RTBMainBackColor;

            if (frmSection.WindowState == FormWindowState.Minimized)
            {
                frmSection.WindowState = FormWindowState.Normal;
            }

            this.RTBMain.Focus();
            this.RTBMain.SelectionStart = savedScrollIndex;
            this.RTBMain.ScrollToCaret();  // Ensures the caret and scroll are aligned
            RichTextBoxScrollManager.ScrollToCharIndex(RTBMain, savedScrollIndex);
            this.RTBMain.SelectionLength = 0;

            if (this.RTBSearchBox.TextLength > 0)
            {
                frmSection.RTBSection.BackColor = Globals.User_Settings.RTBMainBackColor;
                frmSection.Visible = true;
                frmSection.Show();
                frmSection.BringToFront();
                frmSection.Focus();
                this.AddOwnedForm(frmSection);

            }
            else
            {
                MessageBox.Show("You must input a section number!", "Error . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HandlePreconditionsAndChecks()
        {
            // Focus on the search text box
            RTBSearchBox.Focus();

            // If the search text box is empty, insert a section character
            if (RTBSearchBox.Text.Length == 0)
            {
                RTBSearchBox.Text = "§";
                RTBSearchBox.SelectionStart = 1;
                RTBSearchBox.SelectionLength = 0;
                RTBSearchBox.Focus();
                return true;
            }

            // Ensure that a file is open to search
            bool fileCheck = CheckEntryForSearch();
            if (!fileCheck)
            {
                return true;
            }

            string sFind = RTBSearchBox.Text;

            // Check if the search text is only the section character
            if (sFind == "§")
            {
                MessageBox.Show("Error: No number after the section symbol §");
                return true;
            }

            // Validate the search text against the pattern § followed by a number
            //Regex pattern = new("^§\\d+$");
            string result = RTBSearchBox.Text;
            RegexHelpers.GetSectionRegex().Replace(result, "\r\t");
            if (!RegexHelpers.GetSectionRegex().IsMatch(sFind))
            {
                MessageBox.Show("Error: A number is required after the section symbol §. Entry should look similar to § followed by a number, for example §3");
                return true;  // Invalid pattern, ignore button click
            }

            return false;
        }

        public static Tuple<int, int> FindSectionBreadth(RichTextBox richTextBox, int sectionNumber)
        {
            // Get the text from the RichTextBox control
            string input = richTextBox.Text;

            // Section indicators
            string firstIndicator = $"§{sectionNumber}";
            string secondIndicator = $"§{sectionNumber + 1}";

            // Find the start position and end position
            int startIndex = input.IndexOf(firstIndicator);
            int endIndex = input.IndexOf(secondIndicator);

            if (startIndex == -1)
            {
                return new Tuple<int, int>(0, 0);  // First indicator not found
            }

            // If the second indicator is not found, extend to the end of the text
            if (endIndex == -1)
            {
                endIndex = input.Length;
            }

            return new Tuple<int, int>(startIndex, endIndex);
        }

        private static void ShowFrmSavedBriefly(Form? frmMain = null)
        {
            if (frmMain == null)
            {
                throw new ArgumentNullException(nameof(frmMain), "Parent form must be provided.");
            }

            FrmSaving frmSaving = new FrmSaving
            {
                StartPosition = FormStartPosition.Manual
            };

            // Center frmSaving relative to frmMain
            frmSaving.Location = new Point(
                frmMain.Left + (frmMain.Width - frmSaving.Width) / 2,
                frmMain.Top + (frmMain.Height - frmSaving.Height) / 2);

            // Show the form non-modally
            frmSaving.Show(frmMain);

            // Initialize a timer to close the form after a delay
            System.Windows.Forms.Timer FrmSavedShow_Timer = new System.Windows.Forms.Timer
            {
                Interval = 800 // 800 milliseconds = 0.8 seconds
            };

            FrmSavedShow_Timer.Tick += (sender, e) =>
            {
                // Stop the timer
                FrmSavedShow_Timer.Stop();

                // Close the form
                frmSaving.Close();

                // Dispose of the timer
                FrmSavedShow_Timer.Dispose();
            };

            // Start the timer
            FrmSavedShow_Timer.Start();
        }



        private void PopulateGlobals_reverseResultsSearchStack(string searchString)
        {
            Globals.search._reverseResultsSearchStack.Clear(); // Clear previous results
            Globals.search._reverseSearchIndex = -1;

            // Normalize the search string and the text
            string normalizedSearchString = searchString.Normalize(NormalizationForm.FormC);
            string normalizedText = RTBMain.Text.Normalize(NormalizationForm.FormC);

            // Use the normalized strings in your regex
            Regex regex = new(Regex.Escape(normalizedSearchString), RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(normalizedText);

            foreach (Match match in matches)
            {
                Globals.search._reverseResultsSearchStack.Push(match.Index);
            }
        }

        private static int GetSearchIndex()
        {
            if (Globals.search._reverseResultsSearchStack.Count > 0)
            {
                return Globals.search._reverseResultsSearchStack.Pop();
            }
            return -1; // No more results
        }

        public bool CheckEntryForSearch()
        {
            bool check = true;
            int EndOfFile = this.RTBMain.TextLength;

            if (EndOfFile < 2)
            {
                MessageBox.Show("Error: There is no file to search!");
                check = false;
            }
            return check;
        }

        // if a form is currently visible, make it invisible, then on next function run, toggle it back to visible
        private static void ToggleFormVisibility()
        {
            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
                var form = System.Windows.Forms.Application.OpenForms[i];
                if (form != null && form.Name != "FrmMain")
                {
                    form.Visible = false;
                }
            }
        }


        private void SaveFileBasedOnExtensionType(string filePath)
        {
            try
            {
                string strExt = System.IO.Path.GetExtension(filePath);
                strExt = strExt.ToUpper();
                switch (strExt)
                {
                    case ".RTF":
                        RTBMain.SaveFile(filePath);
                        break;
                    default:
                        // to save as plain text
                        Cursor.Current = Cursors.WaitCursor;
                        FileIO.WriteFile(filePath, RTBMain.Text);
                        this.RTBMain.SelectionStart = 0;
                        this.RTBMain.SelectionLength = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private int GetNextForwardSearchIndex(string searchString)
        {
            string normalizedSearchString = searchString.Normalize(NormalizationForm.FormC);
            string normalizedText = RTBMain.Text.Normalize(NormalizationForm.FormC);

            Regex regex = new(Regex.Escape(normalizedSearchString), RegexOptions.IgnoreCase);
            Match match = regex.Match(normalizedText, panel_BtnBFSearch.forwardSearchIndex);

            if (match.Success)
            {
                // Update the index to the end of the current match
                panel_BtnBFSearch.forwardSearchIndex = match.Index + match.Length;
                return match.Index;
            }
            else
            {
                // No more results found
                // Globals.nextForwardSearchIndex = -1; // Set to -1 to indicate no further results
                return -1;
            }
        }

        // Method to set the background color for all text in a RichTextBox
        public static void SetConsistentBackColor(RichTextBox richTextBox, Color color)
        {
            // Check if the RichTextBox is not empty
            if (richTextBox.TextLength > 0)
            {
                // Preserve the current cursor position
                int originalPosition = richTextBox.SelectionStart;

                // Set the background color of the RichTextBox control
                richTextBox.BackColor = color;

                // Select all text in the RichTextBox
                richTextBox.SelectAll();

                // Set the background color of the selected text
                richTextBox.SelectionBackColor = color;

                // Restore the cursor position
                richTextBox.SelectionStart = originalPosition;

                // Optionally, deselect the text
                richTextBox.DeselectAll();
            }
        }

        // LOCATION  → → → → →  FrmMain Closing
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If already exiting, don't proceed
            if (AppSettings.isExiting) { return; }

            try
            {
                Globals.User_Settings.TTS_Volume = (int)VolumeChanger.Value;

                fileHistoryManager.SaveFilePathsToTextFile(AppSettings.FileMenuFileHistoryTextFile);

                bool isFormOpen = GeneralFns.FormExists("FrmMain");
                if (!isFormOpen) { return; }

                Point pt = new(this.Left, this.Top);
                Globals.User_Settings.FrmMainLocation = pt;

                ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmMainLocation = location);
                ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmMainSize = size);
                Globals.User_Settings.ToggleTTS_SpecChar = BtnCharButtons.Visible;
                AppSettings.ShowFrmKeyboardShorcutsPopWindow = false;

                Globals.User_Settings.Save();   // Save form settings like location, size, etc.

                // Prompt to save un-saved documents
                bool cancelClose = IfDocNotsavedPromptToSave(sender, e);
                if (cancelClose)
                {
                    e.Cancel = true; // Cancel the close if necessary
                }
                else
                {
                    AppSettings.isExiting = true;
                    // Dispose of the synthesizer
                    if (synth != null)
                    {
                        synth.Dispose();
                    }
                    Application.Exit();  // Optionally ensure base class behavior (resource cleanup, etc.)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // LOCATION   → →  → →   ifNotsavedDocPromptAndSave
        private bool IfDocNotsavedPromptToSave(object sender, EventArgs e)
        {
            //if (AppSettings.SpeechSynthClosing) { AppSettings.SpeechSynthClosing = false; return false; }

            if (AppSettings.RtbModified == true)
            {
                //ToggleFormVisibility();
                int answer = (int)MessageBox.Show("Save changes to this document?", "Unsaved Document", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (answer == (int)System.Windows.Forms.DialogResult.Yes)
                {
                    // Save the presently open file
                    SaveFileBasedOnExtensionType(Globals.User_Settings.FilePath);
                    AppSettings.RtbModified = false;
                    return false;
                }
                if (answer == (int)System.Windows.Forms.DialogResult.Cancel)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion // GenearalUtilityFunctions


        #region MatchCaseWordOnlyPrefixAndSuffixFilters

        // If MatchCase, Word, Affix, determine if there is a match on these
        private static bool MatchCaseWordAndAffix(RichTextBox rtb, int index, int findStringLength)
        {
            // False means not a case or word match if the match is required
            bool matchCaseOrWordSearch = CheckForCaseAndWordMatch(rtb, index);

            // False means it is not mode 0 (search all), or it is not a prefix or suffix.
            bool prefixOrSuffixSearch = CheckForPrefixOrSuffix(rtb, index, findStringLength);

            return matchCaseOrWordSearch && prefixOrSuffixSearch;
        }

        private static bool CheckForCaseAndWordMatch(RichTextBox rtb, int index)
        {
            bool caseMatch = true;
            bool wordMatch = true;

            if (SearchSettings.ChkWordOnly)
            {
                if (index > 1 && index + 1 < rtb.Text.Length)  // Avoid out of bounds errors
                {
                    string outerString = rtb.Text.Substring(index - 1, searchBM.findStringBM.Length + 2);  // Get the outer character around the found text
                                                                                                           // Check if there are word boundaries around the substring
                    wordMatch = CheckForWord(outerString);
                }
            }
            if (SearchSettings.ChkMatchCase)
            {
                string subString = rtb.Text.Substring(index, searchBM.findStringBM.Length);  // Get the outer character around the found text
                caseMatch = subString.Equals(searchBM.findStringBM);
            }
            // If false, no proceed with change
            if (wordMatch && caseMatch) { return true; }
            else return false;
        }

        // Is it a word?
        private static bool CheckForWord(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length == 1) { return false; } // No valid word

            // Return the search type    0 = search ALL   1 = Prefix search      2 = Suffix search
            char precedingChar = text[0];
            //char followingChar = text[text.Length - 1]; // old way
            char followingChar = text[^1]; // New way of accessing the last character of a string

            // Check if the preceding character is a space, tab, carriage return, or line feed (prefix condition)
            bool prefix = (precedingChar == ' ' || precedingChar == '\t' || precedingChar == '\r'
                || precedingChar == '\n');
            if (!prefix) { return false; } // Not a word

            // Is the following character prove it to be a word rather than a prefix?
            bool word = (followingChar == ' ' || followingChar == '\t' || followingChar == '\r' || followingChar == '\n'
                                       || char.IsPunctuation(followingChar));

            return word;
        }

        // Return the search type    0 = search ALL   1 = Prefix search      2 = Suffix search
        // Search type is determined and then checked
        private static bool CheckForPrefixOrSuffix(RichTextBox rtb, int index, int findStringLength)
        {
            if (AppSettings.AffixType == Affix.All) { return true; } // search ALL, proceed (i.e., do nothing)

            // Ensure searchBM.indexBM and searchBM.findStringBM.Length are within bounds
            if (index <= 1 || index + searchBM.findStringBM.Length >= rtb.Text.Length)
            {
                return false;
            }

            char precedingChar = rtb.Text[index - 1];
            char followingChar = rtb.Text[index + findStringLength];

            if (AppSettings.AffixType == Affix.Prefix) // Check for Prefix
            {
                bool prefix = CheckForPrefix(precedingChar, followingChar, rtb.Text, index);
                if (!prefix) { return false; }
            }
            if (AppSettings.AffixType == Affix.Suffix) // Check for Suffix
            {
                bool suffix = CheckForSuffix(precedingChar, followingChar, rtb.Text, index);
                if (!suffix) { return false; }
            }
            // Affix affirmed, proceed (i.e., do nothing), return true to proceed
            return true;
        }

        private static bool CheckForPrefix(char precedingChar, char followingChar, string text, int index)
        {
            if (index < 0 || index > text.Length) { return false; }

            // Check if the preceding character is a space, tab, carriage return, or line feed (prefix condition)
            bool passPre = (precedingChar == ' ' || precedingChar == '\t' || precedingChar == '\r'
                || precedingChar == '\n');
            if (!passPre) { return false; } // Not a prefix

            // Is the following character prove it to be a word rather than a prefix?
            bool word = (followingChar == ' ' || followingChar == '\t' || followingChar == '\r' || followingChar == '\n'
                                       || char.IsPunctuation(followingChar));

            // End of the the string must not be a space or any of the above characters, because that would make it a word
            if (!word)
            {
                return true;
            }
            return false;
        }

        private static bool CheckForSuffix(char precedingChar, char followingChar, string text, int index)
        {
            if (index < 0 || index > text.Length) { return false; }

            // Check if the following character is a space, tab, carriage return, or line feed (suffix condition)
            bool suffixEnding = (followingChar == ' ' || followingChar == '\t' || followingChar == '\r'
                || followingChar == '\n' || char.IsPunctuation(followingChar));
            if (!suffixEnding) { return false; } // Not a suffix

            // Check if the preceding character is not a space, tab, carriage return, or line feed
            // (it should be part of a word, proving it's a suffix)
            bool isWordImbedded = (precedingChar != ' ' && precedingChar != '\t' && precedingChar != '\r' && precedingChar != '\n'
                         && !char.IsPunctuation(precedingChar));

            // True means it is a suffix
            return isWordImbedded;
        }
        #endregion // MatchCaseWordOnlyPrefixAndSuffixFilters


        #region FrmColorSearchUtilityFunctions

        // This procedure is called from FrmColor
        public void UndoRTBMainAction()
        {
            if (RTBMain.CanUndo)
            {
                RTBMain.Undo();
            }
            else
            {
                if (AppSettings.undoStack.Count > 0)
                {
                    RTBMain.Rtf = AppSettings.undoStack.Pop();
                }
            }
        }

        public void SimpleUndo()
        {
            if (RTBMain.CanUndo)
            {
                RTBMain.Undo();
            }
        }

        private static (int start, int end) FindBoundaryMarkers(string text, int searchStart)
        {
            if (searchStart < 0 || text == null || text.Length < 1 || searchStart > text.Length - AppConstants.BM.Length)
            {
                return (-1, -1);
            }

            int start = text.IndexOf(AppConstants.BM, searchStart);  // Find start marker
            if (start == -1) return (-1, -1);  // No boundary marker found

            int end = text.IndexOf(AppConstants.BM, start + AppConstants.BM.Length);  // Find end marker
            if (end == -1) return (-1, -1);  // No closing marker found

            start += AppConstants.BM.Length;  // Move start to position after the marker
            return (start, end);
        }

        // Return true if odd number of BoundryMarkers 
        public bool CheckForOddNumberOfBoundryMarkers()
        {
            bool result = false;
            searchBM.boundryMarkerCount = RegexHelpers.GetBoundaryMarkerRegex().Matches(RTBMain.Text).Count;
            Globals.BMCount = searchBM.boundryMarkerCount;
            if (searchBM.boundryMarkerCount % 2 == 1)
            {
                result = true;  // Odd number of boundryMarkers
                string message = "You have " + searchBM.boundryMarkerCount + " boundary markers!" + " There should be an even number " + Environment.NewLine + "of boundary markers. ";
                MessageBox.Show(message + "You must correct this before proceeding!", "Uneven number of boundary markers present!", MessageBoxButtons.OK);
            }
            return result;
        }
        #endregion // FrmColorSearchUtilityFunctions

        #region FontRelatedFunctions
        private void CmboFontSelect_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CmboFontSelect.SelectedItem is string selectedFontFamily)
            {
                AppSettings.SelectedFontFamily = selectedFontFamily;
                ApplyFontSettings();
            }
        }

        private void ComboBoxFontSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxFontSize.SelectedItem is string selectedSize && float.TryParse(selectedSize, out float newSize))
            {
                AppSettings.SelectedFontSize = newSize; ApplyFontSettings();
            }
        }

        private void ApplyFontSettings()
        {
            string family = AppSettings.SelectedFontFamily;
            float size = AppSettings.SelectedFontSize;

            if (!string.IsNullOrEmpty(family))
                ApplyFontFamily(family);

            if (size > 0)
                ApplyFontSize(size);

            RTBMain.Focus();
        }


        private void ApplyFontFamily(string fontFamily)
        {
            if (string.IsNullOrEmpty(fontFamily))
                return;

            CHARFORMAT2 cf = new CHARFORMAT2();
            cf.cbSize = (uint)Marshal.SizeOf(cf);
            cf.dwMask = CFM_FACE;
            cf.szFaceName = fontFamily;

            int flags = (RTBMain.SelectionLength > 0) ? SCF_SELECTION : SCF_ALL;

            SendMessage(RTBMain.Handle, EM_SETCHARFORMAT, flags, ref cf);
        }

        private void ApplyFontSize(float size)
        {
            if (size <= 0)
                return;

            CHARFORMAT2 cf = new CHARFORMAT2();
            cf.cbSize = (uint)Marshal.SizeOf(cf);
            cf.dwMask = CFM_SIZE;
            cf.yHeight = (int)(size * 20); // RichEdit uses twips (1/20 pt)

            int flags = (RTBMain.SelectionLength > 0) ? SCF_SELECTION : SCF_ALL;

            SendMessage(RTBMain.Handle, EM_SETCHARFORMAT, flags, ref cf);
        }



        #endregion // FontRelatedFunctions


        #region SearchReplaceWithinBoundaryMarkers


        public static bool GetSearchBMQueue()
        {
            searchBMQueue.Clear();
            string[] tempArry;

            // the contents for the search and replace function.
            for (int index = 1; index < 11; index++)  //   WAS:  // 0-9       // (int index = 9; index >= 0; index--)
            {
                char separator = '|';
                tempArry = SearchSettings.GetText(index, true).Split(separator);
                foreach (string unit in tempArry) // use pipe symbol as a way of stacking searches in a textbox
                {
                    if (System.String.IsNullOrWhiteSpace(unit)) { continue; }
                    ;

                    // Create the search
                    searchBM = CreateBMSearch(index, unit);
                    searchBMQueue.Enqueue(searchBM);

                }
            }
            if (searchBMQueue.Count > 0) { return true; }
            else { return false; }
        }

        private static BoundaryMarkerSearch CreateBMSearch(int index, string unit)
        {
            Color find_Color = Color.Black; // Default for Simple Replace
            bool colorON = false;

            if (SearchSettings.FrmColorReplaceMode)
            {
                find_Color = ColorManager.GetColor("C" + index.ToString()); // colorUserSettings[index];
                colorON = true;
            }

            searchBM = new BoundaryMarkerSearch()
            {
                findColorBM = find_Color,
                findStringBM = unit,
                replaceStringBM = SearchSettings.GetText(index, false) + "",  // "" prevents empties
                textBoxNumBM = index,
            };

            searchBM.SearchMode = colorON ? Mode.Color : Mode.Text;

            return searchBM;
        }

        // Initialize and load SearchQueue
        public void BMOperationInitialization()
        {
            AppSettings.currentCursorPosition = RTBMain.SelectionStart;  // Preserve current cursor position
            Cursor.Current = Cursors.WaitCursor;
            RTBMain.SuspendLayout();  // Detach event handlers if any
            if (RTBMain.Rtf != null) { AppSettings.undoStack.Push(RTBMain.Rtf); } //  UNDO for RTBMain

            // Get all Searches. If nothing in the search Queue, exit      
            if (!GetSearchBMQueue()) { return; }

            searchBM.searchBMComplete = false;
            searchBM.indexBM = 0;
            searchBM.endBMIndex = 0;
            searchBM.startBMIndex = 0;
            searchBM.subStringPointerBM = 0;
            Globals.StartTime = DateTime.Now;

            try
            {
                // LOCATION  → → → → →  searchBMQueue.Dequeue()
                while (searchBMQueue.Count > 0)
                {
                    searchBM = searchBMQueue.Dequeue();
                    GetBoundaryMarkerRange();  // Do modifications within this BM set
                }

                AppSettings.UndoQueue.Enqueue(AppSettings.UndoCount);

                RTBMain.ResumeLayout(true);
                RTBMain.SelectionStart = AppSettings.currentCursorPosition;
                RTBMain.DeselectAll();
                RTBMain.Refresh();
                searchBM.searchBMComplete = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void GetBoundaryMarkerRange()
        {
            try
            {
                while (true)
                {
                    if (searchBM.getNextBMSet)
                    {
                        GetBoundaryMarkerInformation();
                        searchBM.getNextBMSet = false;
                    }
                    if (searchBM.searchBMComplete) { break; }

                    // Define the search range
                    int searchStart = searchBM.startBMIndex;
                    int searchEnd = searchBM.endBMIndex;
                    if (searchEnd < searchStart) { return; } // Ensure searchEnd is not less than searchStart

                    SearchAndReplaceWithinRange(searchStart, searchEnd);

                    if (searchBM.subStringPointerBM < 0)
                    {
                        searchBM.subStringLengthBM = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get the substring between the boundary markers. 
        private void SearchAndReplaceWithinRange(int searchStart, int searchEnd)
        {

            string searchRangeText = RTBMain.Text.Substring(searchStart, searchEnd - searchStart);

            while (!searchBM.searchBMComplete)
            {
                if (searchBM.subStringPointerBM < 0 || searchBM.subStringPointerBM >= searchRangeText.Length)
                {
                    searchBM.indexBM = searchEnd; // Update the global index to avoid infinite loop
                    break; // Exit if the search index is out of range
                }

                searchBM.subStringPointerBM = searchRangeText.IndexOf(searchBM.findStringBM, searchBM.subStringPointerBM, StringComparison.OrdinalIgnoreCase);
                if (searchBM.subStringPointerBM < 0 || searchBM.subStringPointerBM >= searchRangeText.Length)
                {
                    searchBM.indexBM = searchEnd; // Update the global index to avoid infinite loop
                    searchBM.subStringPointerBM = 0;
                    searchBM.endBMIndex = searchEnd + searchBM.findStringBM.Length;
                    searchBM.getNextBMSet = true;
                    break; // Exit if no more matches are found within the range
                }

                int globalIndex = searchStart + searchBM.subStringPointerBM;

                bool match = (MatchCaseWordAndAffix(RTBMain, globalIndex, searchBM.findStringBM.Length));

                if (AppSettings.SearchMode == Mode.Text)
                {
                    if (match)
                    {
                        // Perform replacement in the dummy guide string (searchRangeText)
                        searchRangeText = searchRangeText.Remove(searchBM.subStringPointerBM, searchBM.findStringBM.Length);
                        searchRangeText = searchRangeText.Insert(searchBM.subStringPointerBM, searchBM.replaceStringBM);
                        // Now do the actual replacement
                        PerformTextReplacement(globalIndex);
                    }
                    // Adjust the global index to reflect the correct position
                    searchBM.indexBM = globalIndex + searchBM.replaceStringBM.Length;
                    searchBM.subStringPointerBM = AdjustIndexUsingSubstring();
                    searchBM.subStringPointerBM += searchBM.replaceStringBM.Length;
                }
                else
                {
                    if (match)
                    {
                        PerformColorReplacement(globalIndex);
                    }
                    searchBM.subStringPointerBM += searchBM.findStringBM.Length;
                    searchBM.indexBM = globalIndex + searchBM.findStringBM.Length;
                    RTBMain.Refresh();  // for Debugging
                }
                SearchTimeControl();
            }
        }

        private void PerformTextReplacement(int globalIndex)
        {
            RTBMain.Select(globalIndex, searchBM.findStringBM.Length);
            RTBMain.SelectedText = searchBM.replaceStringBM;
            RTBMain.Refresh();
            AppSettings.UndoCount++;
            //RTBMain.Select(globalIndex, searchBM.replaceStringBM.Length);
            //RTBMain.SelectionFont = Globals.search.Font;
            //AppSettings.UndoCount++;
        }

        private int AdjustIndexUsingSubstring()
        {
            // Calculate the difference in length
            int lengthDifference = searchBM.replaceStringBM.Length - searchBM.findStringBM.Length;

            // Adjust the index accordingly
            return searchBM.subStringPointerBM + lengthDifference;
        }


        private void PerformColorReplacement(int globalIndex)
        {
            RTBMain.Select(globalIndex, searchBM.findStringBM.Length);
            RTBMain.SelectionColor = searchBM.findColorBM;
            RTBMain.Refresh();
            AppSettings.UndoCount++;
        }

        public void GetBoundaryMarkerInformation()
        {
            Color currentColor = Color.Black;

            try
            {
                searchBM.boundryMarkerCount = RegexHelpers.GetBoundaryMarkerRegex().Matches(this.RTBMain.Text).Count;
                Globals.BMCount = searchBM.boundryMarkerCount;

                // 
                if (searchBM.searchBMComplete) { return; }

                searchBM.endBMIndex++;
                (searchBM.startBMIndex, searchBM.endBMIndex) = FindBoundaryMarkers(this.RTBMain.Text, searchBM.endBMIndex);   // + Globals.BM.Length);
                if (searchBM.startBMIndex == -1 || searchBM.endBMIndex == -1)
                {
                    searchBM.searchBMComplete = true;
                    return;
                }

                searchBM.subStringLengthBM = searchBM.endBMIndex - searchBM.startBMIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion // SearchReplaceWithinBoundaryMarkers

        #region Word Frequency Count
        private void getWordFrequenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var entries = new Dictionary<string, string> { };
            string text1 = RTBMain.Text;
            string text = text1;

            // Remove text between brackets
            text = RemoveTextBetweenBrackets(text);

            // Create an instance of FrmWordFrequency
            FrmWordFrequency frmWordFrequency = new FrmWordFrequency();
            DisplayWordFrequencies(text, frmWordFrequency);
            // entries.Add(text, "");

            // Show the form non-modally
            frmWordFrequency.Show();
        }


        private string RemoveTextBetweenBrackets(string text)
        {
            var regex = new Regex(@"\[.*?\]");
            return regex.Replace(text, string.Empty);
        }

        private Dictionary<string, int> CalculateWordFrequencies(string text)
        {
            var frequencies = new Dictionary<string, int>();
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '\"', '\'' },
                                        StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string lowerCaseWord = word.ToLower();
                if (frequencies.ContainsKey(lowerCaseWord))
                {
                    frequencies[lowerCaseWord]++;
                }
                else
                {
                    frequencies.Add(lowerCaseWord, 1);
                }
            }

            return frequencies;
        }

        private string FormatFrequencies(Dictionary<string, int> frequencies)
        {
            //var sortedFrequencies = frequencies.OrderByDescending(pair => pair.Value);
            var sortedFrequencies = frequencies.OrderBy(pair => pair.Value);
            var result = new StringBuilder();

            foreach (var pair in sortedFrequencies)
            {
                result.AppendLine($"{pair.Value} {pair.Key}");
            }

            return result.ToString();
        }

        private void DisplayWordFrequencies(string text, FrmWordFrequency frmWordFrequency)
        {
            var frequencies = CalculateWordFrequencies(text);
            var formattedFrequencies = FormatFrequencies(frequencies);

            // Calculate total word count
            int totalWords = frequencies.Values.Sum();
            string totalWordCountText = $"Total Words = {totalWords:N0}"; // N0 formats number with commas

            // Combine total word count with formatted frequencies
            string resultText = totalWordCountText + Environment.NewLine + formattedFrequencies;

            frmWordFrequency.RTBWordFreq.Text = resultText;
        }

        #endregion  // Word Frequency Count




        #region Recent Changes

        private void RTBSearchBox_Enter(object sender, EventArgs e)
        {
            //RTBSearchBox.Focus();
            //Globals.Current_RTB_withFocus = RTBSearchBox;
        }

        private void RTBMain_Enter(object sender, EventArgs e)
        {
            RTBMain.Visible = true;
            //Globals.Current_RTB_withFocus = RTBMain;
        }

        private void RTBMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender != null && sender.GetType() == typeof(RichTextBox))
            {
                KeyboardShortcuts.HandleControlKeys(RTBMain, e);
            }
        }

        private void RTBSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBSearchBox;
            // Ensure RTBSearchBox has focus
            RTBSearchBox.Focus();

            if (e.Control)
            {
                KeyboardShortcuts.HandleControlKeys(RTBSearchBox, e);
                e.Handled = true; // Prevent further processing
                e.SuppressKeyPress = true; // Prevent default behavior
            }

            if (e.Alt)
            {
                KeyboardShortcuts.HandleAltKeyCombinations(RTBSearchBox, e);
                e.Handled = true; // Prevent further processing
                e.SuppressKeyPress = true; // Prevent default behavior
            }


        }

        private void RTBSearchBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBSearchBox;
            // Ensure RTBSearchBox has focus
            RTBSearchBox.Focus();

            if (e.KeyCode == Keys.V && e.Control)
            {
                e.IsInputKey = true; // Prevent default behavior
            }
        }

        private void RTBSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (RTBSearchBox.Text.Length > 0 && RTBSearchBox.Text != null)
                panel_BtnBFSearch.FindString = RTBSearchBox.Text;

            int selectionStart = RTBSearchBox.SelectionStart;
            int selectionLength = RTBSearchBox.SelectionLength;

            RTBSearchBox.SelectAll();
            RTBSearchBox.SelectionFont = new Font("Times New Roman", 16, FontStyle.Regular);
            RTBSearchBox.Select(selectionStart, selectionLength);
        }

        private void buildTTSFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] lines = RTBMain.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                // Skip lines that are just numbers (subtitle indices)
                if (lines[i].Trim().All(char.IsDigit))
                    continue;

                // Skip lines that look like timestamps (contain --> or have time format)
                if (lines[i].Contains("-->") ||
                    Regex.IsMatch(lines[i], @"\d{2}:\d{2}:\d{2}[.,]\d{3}"))
                    continue;

                // This should be either Russian or English text
                string currentLine = lines[i].Trim();

                // Check if the next line exists and is not a timestamp/index
                if (i + 1 < lines.Length)
                {
                    string nextLine = lines[i + 1].Trim();

                    // If next line is not empty and not a timestamp/index
                    if (!string.IsNullOrWhiteSpace(nextLine) &&
                        !nextLine.Contains("-->") &&
                        !nextLine.All(char.IsDigit) &&
                        !Regex.IsMatch(nextLine, @"\d{2}:\d{2}:\d{2}[.,]\d{3}"))
                    {
                        // Assume current is Russian, next is English
                        result.AppendLine(currentLine);
                        result.AppendLine($"[{nextLine}]");
                        result.AppendLine();
                        i++; // Skip the next line since we processed it
                        continue;
                    }
                }

                // If we couldn't pair it, just add it as is
                result.AppendLine(currentLine);
            }

            // Clean up
            string formattedText = result.ToString();
            formattedText = Regex.Replace(formattedText, "\n{3,}", "\n\n");
            formattedText = Regex.Replace(formattedText, "- ", "");

            RTBMain.Text = formattedText.Trim();
            RTBMain.SelectAll();
            RTBMain.SelectionFont = new Font("Times New Roman", 26, FontStyle.Regular);
            RTBMain.SelectionLength = 0;
        }

        private void RTBSearchBox_MouseEnter(object sender, EventArgs e)
        {
            RTBSearchBox.Cursor = Cursors.Default;
            //RTBSearchBox.Focus();
        }

        private void RTBSearchBox_MouseMove(object sender, MouseEventArgs e)
        {
            RTBSearchBox.Cursor = Cursors.Default;
        }

        private void cmboVoice_MouseMove(object sender, MouseEventArgs e)
        {
            cmboVoice.Cursor = Cursors.Default;
        }

        private void VolumeChanger_ValueChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.TTS_Volume = (int)VolumeChanger.Value;
        }

        private void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Globals.Current_RTB_withFocus
            // Get the text box that has focus
            RichTextBox activeTextBox = Globals.Current_RTB_withFocus;

            if (activeTextBox != null && !string.IsNullOrWhiteSpace(activeTextBox.SelectedText))
            {
                string selectedText = activeTextBox.SelectedText;
                TranslateText(selectedText);
            }
            else
            {
                MessageBox.Show("Please select text to translate.", "No Text Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TranslateText(string text)
        {
            string encodedText = Uri.EscapeDataString(text);
            string url = $"https://www.bing.com/translator?text={encodedText}";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void ChkLoop_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.LoopTTSPlayback = ChkLoop.Checked;
        }

        #endregion //  Recent Changes



        //
    }
}










