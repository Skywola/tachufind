using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
// Roughly 17,483 lines of code total as of 2-26-2022.
// Important Points in this module:
// FrmMain_PreviewKeyDown
// string rtf = RTBMain.Rtf;  // For changes to keyboard shortcuts dieplays


namespace Tachufind
{
	public partial class FrmMain : Form
	{
        //public sealed class SpellCheck { };
        public FrmMain()
		{
			InitializeComponent();
        }

        SpeechSynthesizer synth = new SpeechSynthesizer();
        bool loadfirst = true;
        bool synthStopped = false;
        string[] lines;
        int lineNumber = 0; // for speech synthesizer
        List<LineInfo> lineInfo = new List<LineInfo>();
        Dictionary<int, LineInfo> lineInfoDict = new Dictionary<int, LineInfo>();

        public static Queue<SearchInstance> queSearches = new Queue<SearchInstance>();

		readonly InputLanguage cultureInfo = InputLanguage.CurrentInputLanguage;

        public static PageSetupDialog PageSetupDialog1 = new PageSetupDialog();
		public System.Drawing.Printing.PageSettings DefaultPageSettings { get; set; }

		readonly Stack<int> returnStack = new Stack<int>();
		readonly GeneralFns genFns = new GeneralFns();
		readonly FileIO FIO = new FileIO();

		readonly FrmAboutApp frmAboutApp = new FrmAboutApp();
		readonly FrmSaved frmSaved = new FrmSaved();
		readonly FrmGetQuizes frmGetQuizes = new FrmGetQuizes();
		readonly FrmOptions frmOptions = new FrmOptions();
		public static FrmQuiz frmQuiz = new FrmQuiz();
		readonly FrmSection frmSection = new FrmSection();
		readonly FrmShortcuts frmShortcuts = new FrmShortcuts();
		readonly FrmUser frmUser = new FrmUser();
		public static FrmOCR frmOCR = new FrmOCR();

		int EOF;
		int clickNum = 1;
		public Color findColor;  // color of the string    
		public string findString; // string to find
		public int boundryMarkerCount = 0;
		public int cursorPreSearchPosition = 0;
		static int timeLeft = 0;
        KeyboardShortcuts keyboardShortcuts = new KeyboardShortcuts();

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{ 
				Globals.User_Settings.Save();   // MyUserSettings - For saving form location, RTBBackcolor, etc  
				e.Cancel = false;
				bool cancelClose = IfNotsavedDocPromptAndSave(sender, e);
				if (cancelClose)
				{
					e.Cancel = true;
				}
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        // LOCATION ifNotsavedDocPromptAndSave
        bool IfNotsavedDocPromptAndSave(object sender, EventArgs e)
        {
            if (Globals.SpeechSynthClosing) { Globals.SpeechSynthClosing = false; return false; }

            int answer = 0;
            if (Globals.PBoolRTBModified == true)
            {
                ToggleFormVisibility();
                answer = (int)MessageBox.Show("Save changes to this document?", "Unsaved Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (answer == (int)System.Windows.Forms.DialogResult.Yes)
                {
                    // Save the presently open file
                    SaveFileBasedOnExtensionType(Globals.User_Settings.FilePath);
                    return false;
                }
                if (answer == (int)System.Windows.Forms.DialogResult.Cancel)
                {
                    return true;
                }
            }
            return false;
        }

		private void SetDefaultColors() {
         Globals.User_Settings = new MyUserSettings();
         Globals.User_Settings.Color00 = Color.White;
          Globals.User_Settings.Color01 = Color.FromArgb(255, 0, 0, 255);   // Blue
          Globals.User_Settings.Color02 = Color.FromArgb(255, 0, 255, 0);  // Green
          Globals.User_Settings.Color03 = Color.FromArgb(255, 255, 0, 0);  // Red
          Globals.User_Settings.Color04 = Color.FromArgb(255, 0, 255, 255);  // Yellow
          Globals.User_Settings.Color05 = Color.FromArgb(255, 148, 0, 211);  // Dark Violet    5
          Globals.User_Settings.Color06 = Color.FromArgb(255, 139, 69, 19);  // Saddle Brown
          Globals.User_Settings.Color07 = Color.FromArgb(255, 112, 128, 144);  // Slate Gray
          Globals.User_Settings.Color08 = Color.FromArgb(255, 30, 144, 255);  // Dodger Blue
          Globals.User_Settings.Color09 = Color.FromArgb(255, 220, 20, 60);  //  Crimson
          Globals.User_Settings.Color10 = Color.FromArgb(255, 128, 128, 0);  // Olive
          Globals.User_Settings.Color11 = Color.Black;
        }

        private void FrmMain_Load(object sender, EventArgs e)
		{
			SpeechSynthesizer synth = new SpeechSynthesizer();
			Globals.Synth = synth;
            synth.SetOutputToDefaultAudioDevice();

            Globals.User_Settings = new MyUserSettings();
			Globals.User_Settings.Color00 = Color.White;
			if (Globals.User_Settings.Color01 == null){ Globals.User_Settings.Color01 = Color.FromArgb(255, 0, 0, 255);}   // Blue
			if (Globals.User_Settings.Color02 == null){ Globals.User_Settings.Color02 = Color.FromArgb(255, 0, 255, 0);}  // Green
			if (Globals.User_Settings.Color03 == null){ Globals.User_Settings.Color03 = Color.FromArgb(255, 255, 0, 0); }  // Red
			if (Globals.User_Settings.Color04 == null){ Globals.User_Settings.Color04 = Color.FromArgb(255, 0, 255, 255); }  // Yellow
			if (Globals.User_Settings.Color05 == null){ Globals.User_Settings.Color05 = Color.FromArgb(255, 148, 0, 211);}  // Dark Violet    5
			if (Globals.User_Settings.Color06 == null){ Globals.User_Settings.Color06 = Color.FromArgb(255, 139, 69, 19); }  // Saddle Brown
			if (Globals.User_Settings.Color07 == null){ Globals.User_Settings.Color07 = Color.FromArgb(255, 112, 128, 144);}  // Slate Gray
			if (Globals.User_Settings.Color08 == null){ Globals.User_Settings.Color08 = Color.FromArgb(255, 30, 144, 255); }  // Dodger Blue
			if (Globals.User_Settings.Color09 == null){ Globals.User_Settings.Color09 = Color.FromArgb(255, 220, 20, 60);}  //  Crimson
			if (Globals.User_Settings.Color10 == null) { Globals.User_Settings.Color10 = Color.FromArgb(255, 128, 128, 0); }  // Olive
			Globals.User_Settings.Color11 = Color.Black;

            //this.DataBindings.Add(new Binding("BackColor", Globals.user_settings, "RTBMainBackColor"));
			this.RTBMain.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
			Globals.FindStart = 0;
			this.KeyPreview = true;
            //this.PreviewKeyDown += FrmMain_PreviewKeyDown;

            // For AutoOpen File at startup (Properties.chkOpenFromLastLocation)
            string ext; // = "";
			string fileContents; // = "";

			// NEED TO ADD BETA TIME FRAME
			// LOCATION согласен
			try
			{
				Cursor.Current = Cursors.Default;
				SetDefaultSearch(); // search initialization for FrmColor

				// User agreement
				//frmUser.ShowDialog();
				// Check to see if User has agreed to User Agreement (this is temporarily disabled)
				if (!(Globals.User_Settings.StrSetUserAgreed == "согласен"))
				{
					FrmUser frmUser = new FrmUser();
					frmUser.Visible = false;
					frmUser.ShowDialog();  //  Show User Agreement
				}
				if (!Globals.User_Settings.StrSetUserAgreed.Contains("согласен"))
				{
                    System.Windows.Forms.Application.Exit();
				}

				// Set Form location before changing Global.init to false.  (FrmMain_LocationChanged)
				// Uses if(Globals.init == true) { return; } to prevent premature registration of form location at start up.
				this.Left = Globals.User_Settings.FrmMainLocation.X;
				this.Top = Globals.User_Settings.FrmMainLocation.Y;

				this.Width = Globals.User_Settings.FrmMainSize.Width;
				this.Height = Globals.User_Settings.FrmMainSize.Height;

				Globals.FrmMainInit = false;  // End INIT phase, allow updates of forms when they are repositioned

				// Top Buttons
				this.FrmMainColorBtn01.BackColor = Globals.User_Settings.Color01;
				this.FrmMainColorBtn02.BackColor = Globals.User_Settings.Color02;
				this.FrmMainColorBtn03.BackColor = Globals.User_Settings.Color03;
				this.FrmMainColorBtn04.BackColor = Globals.User_Settings.Color04;
				this.FrmMainColorBtn05.BackColor = Globals.User_Settings.Color05;
																  // Bottom .buttons	
				this.FrmMainColorBtn06.BackColor = Globals.User_Settings.Color06;
				this.FrmMainColorBtn07.BackColor = Globals.User_Settings.Color07;
				this.FrmMainColorBtn08.BackColor = Globals.User_Settings.Color08;
				this.FrmMainColorBtn09.BackColor = Globals.User_Settings.Color09;
				this.FrmMainColorBtn10.BackColor = Globals.User_Settings.Color10;

                clickNum = 0;

				frmAboutApp.Visible = false;
				//frmColor.Visible = false;
				frmGetQuizes.Visible = false;
				frmOptions.Visible = false;
				frmQuiz.Visible = false;
				frmSection.Visible = false;
				frmShortcuts.Visible = false;
				frmSaved.Visible = false;
				frmUser.Visible = false;
				frmOCR.Visible = false;


				this.RTBMain.EnableAutoDragDrop = true;
				//this.RTBMain.SelectionIndent = 2;
				//this.RTBMain.SelectionRightIndent = 2;

				if (Globals.User_Settings.FilePath.Length > 0)
				{
					this.Text = "Tachufind     Editing: " + Globals.User_Settings.FilePath;
				}
				else
				{ // If file not exist, set default X positions, Y positions, width and height.
					this.Text = "Tachufind     Editing: New file";
				}
				Globals.BoolCursorState = false;
				this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;

				this.RTBMain.AutoWordSelection = true;
				Globals.PBoolRTBModified = false;
				this.RTBMain.WordWrap = true;

				Globals.Current_RTB_withFocus = this.RTBMain;
				this.RTBMain.SelectionStart = Globals.User_Settings.CursorPosition;

				btnQuiz.Visible = true;


				// For AutoOpen File at startup (Properties.chkOpenFromLastLocation)
				if (Globals.User_Settings.FrmOptionsOpenFromLastLocation & File.Exists(Globals.User_Settings.FilePath))
				{
					fileContents = FIO.ReadFile(Globals.User_Settings.FilePath01);
					ext = FIO.GetFileExtension(Globals.User_Settings.FilePath); // Get Extension in UpperCase format
					SetNewlyOpenedFileSettings();

					AddItemsToDropDownList();

					if (ext == ".RTF")
					{  // this solves the \highlight bug
						for (int i = 0; i <= 20; i++)
						{
							fileContents.Replace("\\highlight" + Convert.ToString(i), "");  // Get rid of \\highlight0, \\highlight1, etc.
						}
						this.RTBMain.Rtf = fileContents;
						Globals.P_RTFMain_RTF = fileContents;
						Globals.P_RTFMain_TXT = this.RTBMain.Text;
						Globals.PBoolRTBModified = false;
					}
					if (Globals.User_Settings.FilePath.Length > 0)
					{
						this.Text = "Tachufind     Editing: " + Globals.User_Settings.FilePath;
					}
					else
					{
						this.Text = "Tachufind     Editing: New file";
					}
				}
				else
				{
					this.Left = Convert.ToInt32(ClientSize.Width / 2.3);
					this.Top = Convert.ToInt32(ClientSize.Height / 8);
					if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
					{
						this.Height = Convert.ToInt32(ClientSize.Height / 1.2); //860
					}
					if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
					{
						this.Width = Convert.ToInt32(ClientSize.Height / 1.2); // 600
					}
				}
				// make sure form loads to minumum standards
				if (this.Width < 200) { 
					this.Width = 200;
				}
                if (this.Height < 200)
                {
                    this.Height = 200;
                }

                RTBSearch.ForeColor = Color.Black;
				Globals.PBoolRTBModified = false;
				Cursor.Current = Cursors.Default;

				this.RTBMain.SelectionLength = 0;

				int cursorposition = this.RTBMain.Text.Length - this.RTBMain.Text.Length + 1;
				this.RTBMain.SelectionStart = cursorposition;
				Globals.FindStart = 0;  // inserted 9-8-2020
				this.RTBMain.SelectionStart = Globals.FindStart;
				Globals.PBoolRTBModified = false;  // must be set to false here due to autofile load



                foreach (InstalledVoice voice in Globals.Synth.GetInstalledVoices())
                {
                    if (loadfirst)
                    {
                        Globals.Voice = voice.VoiceInfo.Name;
                        cmboVoice.Text = voice.VoiceInfo.Name;
                        string description = voice.VoiceInfo.Description.Replace("Microsoft", "").Trim();
                        description = description.Replace("Desktop ", "");
                        txtVInfo.Text += description;
                        txtVInfo.Text += " " + voice.VoiceInfo.Culture + "\r\n";
                        loadfirst = false;
                    }
                    cmboVoice.Items.Add(voice.VoiceInfo.Name);
                }
				// Recover setting as it existed when app was closed.
				if (Globals.User_Settings.TTS_Voice.Length > 1) {
                    cmboVoice.SelectedItem = cmboVoice.Items.Cast<string>().FirstOrDefault(x => x == Globals.User_Settings.TTS_Voice);
                }

                this.SpeedChanger.Value = Globals.User_Settings.TTS_Speed;

                this.Refresh();
				this.Focus();
				this.BringToFront();
				this.Show();
				RTBMain.Focus();  
				AddItemsToDropDownList();  // inserted 8-4-2022

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}  // END OF private void FrmMain_Load(object sender, EventArgs e) 
		   // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

		private void RTBMain_TextChanged(object sender, EventArgs e)
		{
			Globals.PBoolRTBModified = true;
        }


		private void BtnColor_Click(object sender, EventArgs e)
		{

			foreach (Form form in System.Windows.Forms.Application.OpenForms)
			{
				if (form.Name == "FrmColor")
				{
					form.Visible = true;
					form.Left = Globals.User_Settings.FrmColorLocation.X;
					form.Top = Globals.User_Settings.FrmColorLocation.Y;
					form.BringToFront();
					form.Refresh();
				}
			}
			if (Globals.FrmColorExists == false)
			{
				FrmColor frmColor = new FrmColor(this);
				frmColor.Visible = true;
				frmColor.Refresh();
				frmColor.BringToFront();
				frmColor.Left = Globals.User_Settings.FrmColorLocation.X;
				frmColor.Top = Globals.User_Settings.FrmColorLocation.Y;
				frmColor.BackColor = Color.FromArgb(200, 190, 200);
				Globals.FrmColorExists = true;
			}
		}

		// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		// FUNCTIONS used by FrmMain_Load(object sender, EventArgs e) 

		// search initialization for FrmColor
		void SetDefaultSearch()
		{
			// Set searchitems defaults
			SearchSettings.SearchName = "";
			SearchSettings.rbEditColor = true;
			SearchSettings.chkAutoFindNext = true;
			//SearchSettings.rbReplaceAll = true;
			//SearchSettings.rbSuffix = false;
			//SearchSettings.rbPrefix = false;
			SearchSettings.chkMatchCase = false;
			SearchSettings.chkWordOnly = false;
			SearchSettings.chkReverse = false;

			SearchSettings.frmColorFind01_Txt = "";
			SearchSettings.frmColorReplace01_Txt = "";
			SearchSettings.frmColorFind02_Txt = "";
			SearchSettings.frmColorReplace02_Txt = "";
			SearchSettings.frmColorFind03_Txt = "";
			SearchSettings.frmColorReplace03_Txt = "";
			SearchSettings.frmColorFind04_Txt = "";
			SearchSettings.frmColorReplace04_Txt = "";
			SearchSettings.frmColorFind05_Txt = "";
			SearchSettings.frmColorReplace05_Txt = "";
			SearchSettings.frmColorFind06_Txt = "";
			SearchSettings.frmColorReplace06_Txt = "";
			SearchSettings.frmColorFind07_Txt = "";
			SearchSettings.frmColorReplace07_Txt = "";
			SearchSettings.frmColorFind08_Txt = "";
			SearchSettings.frmColorReplace08_Txt = "";
			SearchSettings.frmColorFind09_Txt = "";
			SearchSettings.frmColorReplace09_Txt = "";
			SearchSettings.frmColorFind10_Txt = "";
			SearchSettings.frmColorReplace10_Txt = "";

			// LOCATION Button Colors  
			// Top Buttons
			this.FrmMainColorBtn01.BackColor = Globals.User_Settings.Color01;   // colorStore.GetColor("C3");
            this.FrmMainColorBtn02.BackColor = Globals.User_Settings.Color02;
            this.FrmMainColorBtn03.BackColor =Globals.User_Settings.Color03;
            this.FrmMainColorBtn04.BackColor =Globals.User_Settings.Color04;
            this.FrmMainColorBtn05.BackColor = Globals.User_Settings.Color05;
			// 
			this.FrmMainColorBtn06.BackColor = Globals.User_Settings.Color06;
			this.FrmMainColorBtn07.BackColor = Globals.User_Settings.Color07;
			this.FrmMainColorBtn08.BackColor = Globals.User_Settings.Color08;
			this.FrmMainColorBtn09.BackColor = Globals.User_Settings.Color09;
            this.FrmMainColorBtn10.BackColor = Globals.User_Settings.Color10;

        }
		// END of void setDefaultSearch()

		// All AppProperties need to be set before 
		public void SetNewlyOpenedFileSettings()
		{
			try
			{
				this.Left = Globals.User_Settings.FrmMainLocation.X;
				this.Top = Globals.User_Settings.FrmMainLocation.Y;

				//this.Left = Convert.ToInt32(Globals.FrmMainXLocation);
				//this.Top = Convert.ToInt32(Globals.FrmMainYLocation);
				this.Width = Convert.ToInt32(Globals.User_Settings.FrmMainSize.Width);
				this.Height = Convert.ToInt32(Globals.User_Settings.FrmMainSize.Height);
				Globals.FindStart = 0;
				this.RTBMain.SelectionStart = Globals.FindStart;

                //ColorDialog dlg = new ColorDialog();
                //dlg.AnyColor = true;
                //dlg.Color = Globals.user_settings.RTBMainBackColor;
                //Globals.user_settings.RTBMainBackColor  = Globals.backColorStd;

                RTBMain.SelectAll();
				RTBMain.BackColor = Globals.User_Settings.RTBMainBackColor;
				RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				RTBMain.SelectionLength = 0;
				//RTBMain.SelectionStart = 0;
				Globals.PBoolRTBModified = false;
				// CHANGE - ScrollToCaret() To scroll to top of textbox
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

		// TODO preserve file history so it can be loaded on new install

		// LOCATION Adds items back to the dropdown list  
		public void AddItemsToDropDownList()  // This fn added to VS 9-12-2020
		{
			if (Globals.User_Settings.FilePath01.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath01);
			}
			if (Globals.User_Settings.FilePath02.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath02);
			}
			if (Globals.User_Settings.FilePath03.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath03);
			}
			if (Globals.User_Settings.FilePath04.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath04);

			}
			if (Globals.User_Settings.FilePath05.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath05);

			}
			if (Globals.User_Settings.FilePath06.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath06);
			}
			if (Globals.User_Settings.FilePath07.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath07);
			}
			if (Globals.User_Settings.FilePath08.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath08);
			}
			if (Globals.User_Settings.FilePath09.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath09);
			}
			if (Globals.User_Settings.FilePath10.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath10);
			}
			if (Globals.User_Settings.FilePath11.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath11);
			}
			if (Globals.User_Settings.FilePath12.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath12);
			}
			if (Globals.User_Settings.FilePath13.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath13);
			}
			if (Globals.User_Settings.FilePath14.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath14);
			}
			if (Globals.User_Settings.FilePath15.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath15);
			}
			if (Globals.User_Settings.FilePath16.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath16);
			}
			if (Globals.User_Settings.FilePath17.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath17);
			}
			if (Globals.User_Settings.FilePath18.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath18);
			}
			if (Globals.User_Settings.FilePath19.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath19);
			}
			if (Globals.User_Settings.FilePath20.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath20);
			}
			if (Globals.User_Settings.FilePath21.Length > 0)
			{
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath21);
			}
		} // END of public void addItemsToDropDownList()

		// END FUNCTIONS used by FrmMain_Load(object sender, EventArgs e) 
		// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


		public bool CheckEntryForSearch()
		{
			bool check = true;
			int EndOfFile = this.RTBMain.TextLength;
			string find = "";
			find = GeneralFns.DoTrim(this.RTBSearch.Text);

			if (EndOfFile < 2)
			{
				MessageBox.Show("Error: There is no file to search!");
				check = false;
			}
			return check;
		}

		// if a form is currently visible, make it invisible, then on next function run, toggle it back to visible
		private void ToggleFormVisibility() {
			for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
			{
				if (System.Windows.Forms.Application.OpenForms[i].Name != "FrmMain")
                    System.Windows.Forms.Application.OpenForms[i].Visible = false;
			}
		}

		private void SaveFileBasedOnExtensionType(string filePath)
		{
			string strExt = null;
			FileIO File_IO = new FileIO();

            try
            {
                strExt = System.IO.Path.GetExtension(filePath);
                strExt = strExt.ToUpper();
                switch (strExt)
                {
                    case ".RTF":
                        RTBMain.SaveFile(filePath);
                        break;
                    default:
                        // to save as plain text
                        Cursor.Current = Cursors.WaitCursor;
                        File_IO.WriteFile(filePath, RTBMain.Text);
                        this.RTBMain.SelectionStart = 0;
                        this.RTBMain.SelectionLength = 0;
                        Cursor.Current = Cursors.Default;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		// LOCATION Fix highlighting bug
		string FixHighLightingBug(string rtfText)
		{
			string extension = FIO.GetFileExtension(Globals.User_Settings.FilePath);
			extension = extension.ToUpper();
			if (extension == ".RTF")
			{
				// this solves the \highlight bug
				int i = 0;
				for (i = 0; i <= 20; i++)
				{
					rtfText.Replace("\\highlight" + Convert.ToString(i), "");
				}

			}
			return rtfText;
		}

		void DisplayFilePath()
		{
			if (Globals.User_Settings.FilePath.Length > 0)
			{
				this.Text = "Tachufind     Editing: " + Globals.User_Settings.FilePath;
			}
			else
			{
				this.Text = "Tachufind     Editing: New file";
			}
		}


		void HandleDropDownListItems()
		{
			if (Globals.User_Settings.FilePath01 == Globals.User_Settings.FilePath)
			{
				return;
			}
			if (Globals.User_Settings.FilePath01 == "")
			{
				Globals.User_Settings.FilePath01 = Globals.User_Settings.FilePath;
				this.fileToolStripMenuItem.DropDownItems.Add(Globals.User_Settings.FilePath);
				return;
			}
			// Properties.filePath01 != Properties.filePath
			RemoveItemFromDropDownList(Globals.User_Settings.FilePath);
			IncrementFilePathPositions();
			ClearDuplicates();
			AddItemsToDropDownList();
		}

		// Removes an item from the dropdown list   
		public void RemoveItemFromDropDownList(string filePath)
		{
			try
			{
				if (filePath.Length < 1) { return; }

				int dropDownItemsCount = 7;  
				while (this.fileToolStripMenuItem.DropDownItems.Count > dropDownItemsCount)
				{  // LOCATION - Setting for number of menu items
					this.fileToolStripMenuItem.DropDownItems.RemoveAt(dropDownItemsCount);
				}
				// Clear any listings that match filePath
				if (Globals.User_Settings.FilePath01 == filePath) { Globals.User_Settings.FilePath01 = ""; }
				if (Globals.User_Settings.FilePath02 == filePath) { Globals.User_Settings.FilePath02 = ""; }
				if (Globals.User_Settings.FilePath03 == filePath) { Globals.User_Settings.FilePath03 = ""; }
				if (Globals.User_Settings.FilePath04 == filePath) { Globals.User_Settings.FilePath04 = ""; }
				if (Globals.User_Settings.FilePath05 == filePath) { Globals.User_Settings.FilePath05 = ""; }
				if (Globals.User_Settings.FilePath06 == filePath) { Globals.User_Settings.FilePath06 = ""; }
				if (Globals.User_Settings.FilePath07 == filePath) { Globals.User_Settings.FilePath07 = ""; }
				if (Globals.User_Settings.FilePath08 == filePath) { Globals.User_Settings.FilePath08 = ""; }
				if (Globals.User_Settings.FilePath09 == filePath) { Globals.User_Settings.FilePath09 = ""; }
				if (Globals.User_Settings.FilePath10 == filePath) { Globals.User_Settings.FilePath10 = ""; }
				if (Globals.User_Settings.FilePath11 == filePath) { Globals.User_Settings.FilePath11 = ""; }
				if (Globals.User_Settings.FilePath12 == filePath) { Globals.User_Settings.FilePath12 = ""; }
				if (Globals.User_Settings.FilePath13 == filePath) { Globals.User_Settings.FilePath13 = ""; }
				if (Globals.User_Settings.FilePath14 == filePath) { Globals.User_Settings.FilePath14 = ""; }
				if (Globals.User_Settings.FilePath15 == filePath) { Globals.User_Settings.FilePath15 = ""; }
				if (Globals.User_Settings.FilePath16 == filePath) { Globals.User_Settings.FilePath16 = ""; }
				if (Globals.User_Settings.FilePath17 == filePath) { Globals.User_Settings.FilePath17 = ""; }
				if (Globals.User_Settings.FilePath18 == filePath) { Globals.User_Settings.FilePath18 = ""; }
				if (Globals.User_Settings.FilePath19 == filePath) { Globals.User_Settings.FilePath19 = ""; }
				if (Globals.User_Settings.FilePath20 == filePath) { Globals.User_Settings.FilePath20 = ""; }
				if (Globals.User_Settings.FilePath21 == filePath) { Globals.User_Settings.FilePath21 = ""; }


			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void IncrementFilePathPositions()
		{
			// Move everything down starting from highest to lowest
			// overwritinging filePath13
			Globals.User_Settings.FilePath21 = Globals.User_Settings.FilePath20;
			Globals.User_Settings.FilePath20 = Globals.User_Settings.FilePath19;
			Globals.User_Settings.FilePath19 = Globals.User_Settings.FilePath18;
			Globals.User_Settings.FilePath18 = Globals.User_Settings.FilePath17;
			Globals.User_Settings.FilePath17 = Globals.User_Settings.FilePath16;
			Globals.User_Settings.FilePath16 = Globals.User_Settings.FilePath15;
			Globals.User_Settings.FilePath15 = Globals.User_Settings.FilePath14;
			Globals.User_Settings.FilePath14 = Globals.User_Settings.FilePath13;
			Globals.User_Settings.FilePath13 = Globals.User_Settings.FilePath12;
			Globals.User_Settings.FilePath12 = Globals.User_Settings.FilePath11;
			Globals.User_Settings.FilePath11 = Globals.User_Settings.FilePath10;
			Globals.User_Settings.FilePath10 = Globals.User_Settings.FilePath09;
			Globals.User_Settings.FilePath09 = Globals.User_Settings.FilePath08;
			Globals.User_Settings.FilePath08 = Globals.User_Settings.FilePath07;
			Globals.User_Settings.FilePath07 = Globals.User_Settings.FilePath06;
			Globals.User_Settings.FilePath06 = Globals.User_Settings.FilePath05;
			Globals.User_Settings.FilePath05 = Globals.User_Settings.FilePath04;
			Globals.User_Settings.FilePath04 = Globals.User_Settings.FilePath03;
			Globals.User_Settings.FilePath03 = Globals.User_Settings.FilePath02;
			Globals.User_Settings.FilePath02 = Globals.User_Settings.FilePath01;
			Globals.User_Settings.FilePath01 = Globals.User_Settings.FilePath;
		}

		void ClearDuplicates()
		{
			string f1 = Globals.User_Settings.FilePath01; string f2 = Globals.User_Settings.FilePath02;
			string f3 = Globals.User_Settings.FilePath03; string f4 = Globals.User_Settings.FilePath04;
			string f5 = Globals.User_Settings.FilePath05; string f6 = Globals.User_Settings.FilePath06;
			string f7 = Globals.User_Settings.FilePath07; string f8 = Globals.User_Settings.FilePath08;
			string f9 = Globals.User_Settings.FilePath09; string f10 = Globals.User_Settings.FilePath10;
			string f11 = Globals.User_Settings.FilePath11; string f12 = Globals.User_Settings.FilePath12;
			string f13 = Globals.User_Settings.FilePath13; string f14 = Globals.User_Settings.FilePath14;
			string f15 = Globals.User_Settings.FilePath15; string f16 = Globals.User_Settings.FilePath16;
			string f17 = Globals.User_Settings.FilePath17; string f18 = Globals.User_Settings.FilePath18;
			string f19 = Globals.User_Settings.FilePath19; string f20 = Globals.User_Settings.FilePath20;
			string f21 = Globals.User_Settings.FilePath21;
			string[] reference = { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21 };

			//string[] filepaths = reference.Distinct().ToArray(); // Clear duplicates
            string[] filepaths = reference.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();

            var result = filepaths.OrderBy(x => x == "").ThenBy(x => ""); // Move all "" to right
			Queue queuepath = new Queue();
			foreach (string item in result)
			{
                if (item != "" && (File.Exists(item)))  // Added (File.Exists(item))  3-5-2023
                {
					queuepath.Enqueue(item); // Add authentic paths to queue 
				}
			}
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath01 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath01 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath02 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath02 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath03 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath03 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath04 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath04 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath05 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath05 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath06 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath06 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath07 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath07 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath08 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath08 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath09 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath09 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath10 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath10 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath11 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath11 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath12 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath12 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath13 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath13 = ""; }

			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath14 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath14 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath15 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath15 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath16 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath16 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath17 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath17 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath18 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath18 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath19 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath19 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath20 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath20 = ""; }
			if (queuepath.Count > 0)
			{
				Globals.User_Settings.FilePath21 = (string)queuepath.Dequeue();
			}
			else { Globals.User_Settings.FilePath21 = ""; }

		}

		// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		// End Of FILE Menu
		// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%



		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		// General Functions Region
		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


		public RichTextBoxFinds GetFindMode()
		{
			SearchInstance searchItem = new SearchInstance();
			RichTextBoxFinds caseMode = 0;
			RichTextBoxFinds wordMode = 0;
			RichTextBoxFinds directionMode = 0;

			searchItem.RichTextBoxFinds_GFindMode = 0;
			// clear 
			if (SearchSettings.chkMatchCase == true)
			{
				caseMode = RichTextBoxFinds.MatchCase;
				// 4        0100 
			}
			if (SearchSettings.chkWordOnly == true)
			{
				wordMode = RichTextBoxFinds.WholeWord;
				// 2        0010
			}
			if (SearchSettings.chkReverse == true)
			{
				directionMode = RichTextBoxFinds.Reverse;
				// 16     1000
			}
			searchItem.RichTextBoxFinds_GFindMode = caseMode | wordMode | directionMode;
			return searchItem.RichTextBoxFinds_GFindMode;
		}

		public void FrmColorBtnFindClick()
		{
			try
			{
				EOF = this.RTBMain.Text.Length;
				string[] FrmColor_ArryFind_Txts = {SearchSettings.frmColorFind01_Txt, SearchSettings.frmColorFind02_Txt,
				SearchSettings.frmColorFind03_Txt, SearchSettings.frmColorFind04_Txt, SearchSettings.frmColorFind05_Txt,
				SearchSettings.frmColorFind06_Txt, SearchSettings.frmColorFind07_Txt, SearchSettings.frmColorFind08_Txt,
				SearchSettings.frmColorFind09_Txt, SearchSettings.frmColorFind10_Txt};

				Color[] colorArray = { Globals.User_Settings.Color01, Globals.User_Settings.Color02, Globals.User_Settings.Color03,
						Globals.User_Settings.Color04, Globals.User_Settings.Color05, Globals.User_Settings.Color06, Globals.User_Settings.Color07,
                        Globals.User_Settings.Color08, Globals.User_Settings.Color09, Globals.User_Settings.Color10};

				if (Globals.BtnFindSearchChanged)
				{ // if RTBMain clicked
					Globals.BtnFindStart = this.RTBMain.SelectionStart;
					Globals.BtnFindSearchChanged = false;
				}
				if (Globals.BtnFindStart < 0)
				{ // btnFindStart = -1  EO-One-search
					Globals.BtnFindSearchRoundCount += 1;  //Globals.btnFindSearchRoundCount + 1;
					Globals.BtnFindStart = 0;
					if (Globals.BtnFindSearchRoundCount == 8 | FrmColor_ArryFind_Txts[Globals.BtnFindSearchRoundCount] == "")
					{
						MessageBox.Show("End Of Search", "End Of Search", MessageBoxButtons.OK);
						//MessageBox.Show(new Form() { TopMost = true }, "End Of Search", "End Of Search", MessageBoxButtons.OK);
						Globals.BtnFindSearchRoundCount = 0;
						return;
					}
				}
				string find = FrmColor_ArryFind_Txts[Globals.BtnFindSearchRoundCount];  // get items one by one
				Globals.BtnFindStart = this.RTBMain.Find(find, Globals.BtnFindStart, GetFindMode());
				if (Globals.BtnFindStart > -1)
				{
					this.RTBMain.Select(Globals.BtnFindStart, find.Length);
					Globals.BtnFindStart = Globals.BtnFindStart + find.Length;
					string CNumber = "C" + (Globals.BtnFindSearchRoundCount + 1).ToString();
					Globals.BtnFindCurrentColor = colorArray[Globals.BtnFindSearchRoundCount + 1];
                }
				else
				{
					this.RTBMain.Select(RTBMain.SelectionStart, 0);
					this.BtnReturn.PerformClick();
				}
				this.Focus();
				this.RTBMain.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void FrmColorBtnReplaceClick()
		{
			FrmColor frmColor = new FrmColor(this);
			try
			{
				string[] FrmColor_ArryFind_Txts = {SearchSettings.frmColorFind01_Txt, SearchSettings.frmColorFind02_Txt,
				SearchSettings.frmColorFind03_Txt, SearchSettings.frmColorFind04_Txt, SearchSettings.frmColorFind05_Txt,
				SearchSettings.frmColorFind06_Txt, SearchSettings.frmColorFind07_Txt, SearchSettings.frmColorFind08_Txt,
				SearchSettings.frmColorFind09_Txt, SearchSettings.frmColorFind10_Txt};

				string[] FrmColor_ArryReplace_Txts = {SearchSettings.frmColorReplace01_Txt, SearchSettings.frmColorReplace02_Txt,
				SearchSettings.frmColorReplace03_Txt, SearchSettings.frmColorReplace04_Txt, SearchSettings.frmColorReplace05_Txt,
				SearchSettings.frmColorReplace06_Txt, SearchSettings.frmColorReplace07_Txt, SearchSettings.frmColorReplace08_Txt,
				SearchSettings.frmColorReplace09_Txt, SearchSettings.frmColorReplace10_Txt};
				if (SearchSettings.rbEditColor)
				{
					this.RTBMain.SelectionColor = Globals.BtnFindCurrentColor;  //Color
					this.RTBMain.Select(); //  (selStart, 0);  // deselect
				}
				if (SearchSettings.rbEditColor == false) // rbMultipleReplace is selected
				{
					string test = FrmColor_ArryReplace_Txts[Globals.BtnFindSearchRoundCount];
					this.RTBMain.SelectedText = FrmColor_ArryReplace_Txts[Globals.BtnFindSearchRoundCount];
					//doFindAutoReplace(Globals.btnFindStart);
				}

				if (SearchSettings.chkAutoFindNext == true)
				{
					frmColor.btnFind.PerformClick();
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		// This uses, for example "§12", find first occurrence, then uses "§13" for second occurrence
		// sections are designated sequentially, so finds the range between "§12" and "§13" for example
		public int[] GetSectionRange(object sender, EventArgs e, int sectionNumber, int EndOfFile)
		{
			string[] nums = new string[3];   // dim string array
			int[] result = new int[3];  // dim array
			string sectionStart = "§" + sectionNumber.ToString();
			string sectionEnd = "§" + (sectionNumber + 1).ToString();
			int sectionLength = 0;
			int start = 0;
			int end = 0;
			// start        length          failed  
			result[0] = -1; result[1] = -1; result[2] = -1;

			start = this.RTBMain.Find(sectionStart, 0, EndOfFile, RichTextBoxFinds.None);
			if (start < 0)
			{
				//MessageBox.Show("Section or chart not found", "Not found", MessageBoxButtons.OK);
				return result;
			}
			string endOfSection = "§" + (sectionNumber + 1).ToString(); // Increment to next sequential section - § + (number + 1)
			end = this.RTBMain.Find(endOfSection, start, EndOfFile, RichTextBoxFinds.None);
			if (end < 1)
			{ // Check for EndOfFile by serching for another "§", if none, it is the end of the file
				end = this.RTBMain.Find("§", start + 2, EndOfFile, RichTextBoxFinds.None);
				if (end < 1) { end = EndOfFile - 1; }
			}
			sectionLength = end - start;
			if (sectionLength < 1)
			{
				MessageBox.Show("Section numbers are ordered incorrectly in the text.", "Not found", MessageBoxButtons.OK);
				result[2] = -1;  // Failure
				return result;
			}
			// Store results
			result[0] = start;
			// set length
			if (start > -1 & end > 0)
			{
				result[1] = Convert.ToInt32((end) - Convert.ToInt32(start));
			} // This is the last section.  Get distance from section start to end of file
			if (start > 0 & end < 0)
			{
				result[1] = Convert.ToInt32((EndOfFile) - Convert.ToInt32(start));
			}
			result[2] = 1; // no failure
			return result;
		}

		// This uses, for example "C12", find first occurrence, then looks for second occurrence
		// because charts are designated by starting and ending them  with the same number.
		public int[] GetChartRange(object sender, EventArgs e, int chartNumber, int start, int end, int EndOfFile)
		{
			string[] nums = new string[3];   // dim string array
			int[] result = new int[3];  // dim array
			string chart = "C" + chartNumber.ToString();
			int chartLength = -1;
			// start        length          failed  
			result[0] = -1; result[1] = -1; result[2] = -1;

			start = this.RTBMain.Find(chart, start, end, RichTextBoxFinds.None);
			end = this.RTBMain.Find(chart, start + 2, EndOfFile, RichTextBoxFinds.None);
			if (start < 0)
			{
				MessageBox.Show("Chart not found", "Not found", MessageBoxButtons.OK);
				return result;
			}
			// Should find a second "C#" indicating end of chart
			chartLength = end - start + 2;
			if (chartLength < 1)
			{  
				MessageBox.Show("Chart not found in this section.", "Not found", MessageBoxButtons.OK);
				return result;
			}
			// Store results
			result[0] = start;
			result[1] = chartLength;
			result[2] = 1; // no failure
			return result;
		}

        
		// Show Languages installed - Currently not used
		public void GetInstalledLanguages()
		{
			string list = "";
			List<string> languageList = new List<string>();

			// Fill languageList with currently installed languages
			foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
			{
				languageList.Add(lang.Culture.EnglishName);
			}
			// List installed languages
			if (languageList.Count > 0)
			{
				foreach (string element in languageList)
				{
					list = list + element + Environment.NewLine;
				}
				// Retired:
				//_fLanguages.RTBLanguages.Text = list;
				//_fLanguages.ShowDialog();
			}
			else
			{
				MessageBox.Show("No languages detected.", "Languages Installed", MessageBoxButtons.OK);
			}
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			HandleDropDownListItems();
            DisposeSynth();

            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
			{
				if (System.Windows.Forms.Application.OpenForms[i].Name != "FrmMain")
                    System.Windows.Forms.Application.OpenForms[i].Close();
			}
			this.Close();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BtnClose.PerformClick();
		}

		private void BtnBold_Click(object sender, EventArgs e)
		{
			try
			{
				int selectedLen = RTBMain.SelectedText.Length;
				int selStart = RTBMain.SelectionStart;

				if ((RTBMain.SelectionFont != null))
				{
					this.RTBMain.SelectionFont = new Font(this.RTBMain.SelectionFont, (this.RTBMain.SelectionFont.Style ^ FontStyle.Bold));
				}
				//this.RTBMain.Select(selStart, selectedLen);
				this.RTBMain.Focus();

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
				int selectedLen = RTBMain.SelectedText.Length;
				int selStart = RTBMain.SelectionStart;
				if ((RTBMain.SelectionFont != null))
				{
					this.RTBMain.SelectionFont = new Font(this.RTBMain.SelectionFont, (this.RTBMain.SelectionFont.Style ^ FontStyle.Italic));
				}
				//this.RTBMain.Select(selStart, selectedLen);
				this.RTBMain.Focus();
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
				int selectedLen = RTBMain.SelectedText.Length;
				int selStart = RTBMain.SelectionStart;
				if ((RTBMain.SelectionFont != null))
				{
					this.RTBMain.SelectionFont = new Font(this.RTBMain.SelectionFont, (this.RTBMain.SelectionFont.Style ^ FontStyle.Underline));
				}
				//this.RTBMain.Select(selStart, selectedLen);
				this.RTBMain.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnCapDeCap_Click(object sender, EventArgs e)
		{
			int selectedLen;
			int selStart;
			string check;

			try
			{
				selectedLen = RTBMain.SelectedText.Length;
				selStart = RTBMain.SelectionStart;
				if (selStart < 0)
				{
					return;
				}
				this.RTBMain.Select(selStart, 1);
				check = this.RTBMain.SelectedText;
				this.RTBMain.Select(selStart, selectedLen);
				if (RTBMain.SelectedText.Length > 0)
				{
					if (char.IsUpper(Convert.ToChar(check)))
					{
						RTBMain.SelectedText = this.RTBMain.SelectedText.ToLower();
					}
					else
					{
						RTBMain.SelectedText = this.RTBMain.SelectedText.ToUpper();
					}
				}
				else
				{
					//MsgBox("To change case, select text first.", MessageBoxButtons.OK, "To change case, select text first")
				}
				this.RTBMain.Focus();
				this.RTBMain.Select(selStart, selectedLen);
				//btnCapDeCap.Focus()	
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		// This was BtnBOFSearch, changed in an attempt to avert a bug that only occured with publish version (non-debug)
		private void BtnBFSearch_Click(object sender, EventArgs e)
		{
			if (Control.ModifierKeys == Keys.ControlKey) { return; };  // Should not work when control key is down
			try
			{
				RTBSearch.Focus();
				if ((Globals.BOFPass == 0))
				{
					Globals.BOFLastCursorLocation = this.RTBMain.SelectionStart;
					// save location at start so can return to it
					Globals.BOFIndexToText = 0;
					Globals.BOFPass = Globals.BOFPass + 1;
					Globals.BOFSearchStart = 0;
				}
				Globals.BOFSearchText = RTBSearch.Text;
				Globals.BOFSearchEnd = this.RTBMain.Text.Length;
				Globals.BOFSearchStrLength = RTBSearch.Text.Length;
				// Ensure that a search string and a valid starting point are specified.
				if (Globals.BOFSearchEnd > 0 & Globals.BOFSearchStart >= 0)
				{
					// Ensure that a valid ending value is provided.
					if (Globals.BOFSearchEnd > Globals.BOFSearchStart | Globals.BOFSearchEnd == -1)
					{
						// Obtain the location of the search string in RichTextBox.
						Globals.BOFIndexToText = this.RTBMain.Find(RTBSearch.Text, Globals.BOFSearchStart, Globals.BOFSearchEnd, RichTextBoxFinds.None);
						// Determine whether the text was found in RichTextBox.
						int outerLimitCheck = Globals.BOFIndexToText + Globals.BOFSearchStrLength;
						if (Globals.BOFIndexToText >= 0)
						{
							if (outerLimitCheck < RTBMain.Text.Length)
							{
								// Return the index to the specified search text.
								this.RTBMain.Select(Globals.BOFIndexToText, Globals.BOFSearchStrLength);
								this.RTBMain.Focus();
							}
						}
					}
					if ((Globals.BOFIndexToText != -1))
					{
						int outerLimitCheck = Globals.BOFIndexToText + Globals.BOFSearchStrLength;
						if (outerLimitCheck < RTBMain.Text.Length)
						{
							Globals.BOFSearchStart = (Globals.BOFIndexToText + Globals.BOFSearchStrLength);
						}
					}
					else
					{
						string caption = "Not Found";
						MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
						Globals.BOFPass = 0;
						Globals.BOFSearchStart = 0;
						Globals.BOFIndexToText = 0;
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnEofSearch_Click(object sender, EventArgs e)
		{
			try
			{
				RTBSearch.Focus();
				if ((Globals.EOFPass == 0))
				{
					Globals.EOFLastCursorLocation = this.RTBMain.SelectionStart;
					// save location at start so can return to it
					Globals.EOFIndexToText = 0;
					Globals.EOFPass = Globals.EOFPass + 1;
					Globals.EOFSearchStart = 0;
					Globals.EOFSearchEnd = this.RTBMain.Text.Length;
				}
				Globals.EOFSearchText = RTBSearch.Text;
				Globals.EOFSearchStrLength = RTBSearch.Text.Length;
				// Ensure that a search string and a valid starting point are specified.
				if (Globals.EOFSearchEnd > 0 & Globals.EOFSearchStart >= 0)
				{
					// Ensure that a valid ending value is provided.
					if (Globals.EOFSearchEnd > Globals.EOFSearchStart | Globals.EOFSearchEnd == -1)
					{
						// Obtain the location of the search string in RichTextBoxPrintCtrl1.
						Globals.EOFIndexToText = this.RTBMain.Find(RTBSearch.Text, Globals.EOFSearchStart, Globals.EOFSearchEnd, RichTextBoxFinds.Reverse | RichTextBoxFinds.None);
						// Determine whether the text was found in RichTextBoxPrintCtrl1.
						// Avoid overrun using Properties.SearchStrLength
						if (Globals.EOFIndexToText >= 0 + Globals.EOFSearchStrLength)
						{
							// Return the index to the specified search text.
							this.RTBMain.Select(Globals.EOFIndexToText, Globals.EOFSearchStrLength);
							this.RTBMain.Focus();
						}
					}
					if ((Globals.EOFIndexToText != -1))
					{
						Globals.EOFSearchEnd = (Globals.EOFIndexToText - Globals.EOFSearchStrLength);
					}
					else
					{
						string caption = "Not Found";
						MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
						Globals.EOFPass = 0;
						Globals.EOFSearchStart = 0;
					}
				}

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
				int End = this.RTBMain.SelectionStart - RTBSearch.Text.Length;
				// Ensure that a search string and a valid starting point are specified.
				if ((this.RTBMain.SelectionStart - RTBSearch.Text.Length) > 0)
				{
					index = this.RTBMain.Find(RTBSearch.Text, 0, End, RichTextBoxFinds.Reverse);
				}

				if ((index < 1))
				{
					string caption = "Not Found";
					MessageBox.Show("Search item not found.", caption, MessageBoxButtons.OK);
					BtnReturn.PerformClick();
					this.RTBMain.SelectionLength = 0;
					this.RTBMain.Focus();
					return;
				}
				else
				{
					this.RTBMain.Focus();
				}

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
				Globals.Start = this.RTBMain.SelectionStart + RTBSearch.Text.Length;
				EOF = this.RTBMain.Text.Length;
				// Ensure that a search string and a valid starting point are specified.
				if (EOF > Globals.Start)
				{
					index = this.RTBMain.Find(RTBSearch.Text, Globals.Start, EOF, RichTextBoxFinds.None);
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
				else
				{
					this.RTBMain.Focus();
				}

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
				if (returnStack.Count > 0)
				{
					previousLocationOfCursor = returnStack.Pop();
				}
				if (previousLocationOfCursor < 1)
				{
					previousLocationOfCursor = 1;
				}
				if (this.RTBMain.TextLength >= previousLocationOfCursor)
				{
					this.RTBMain.SelectionStart = previousLocationOfCursor;
					this.RTBMain.SelectionLength = 0;
					if ((clickNum < 19))
					{
						clickNum = clickNum + 1;
					}
					else
					{
						clickNum = 0;
					}
				}
				else
				{
					previousLocationOfCursor = this.RTBMain.TextLength - 12;
					if ((clickNum < 19))
					{
						clickNum = clickNum + 1;
					}
					else
					{
						clickNum = 0;
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
			int answer = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (File.Exists(Globals.User_Settings.FilePath) & Globals.BoolNewFileNameAlreadyExistsButHasBeenSaved == false)
				{
					string caption = "Unsaved Document";
					answer = (int)MessageBox.Show("File already exits.  Do you want to overwrite?", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (answer == (int)System.Windows.Forms.DialogResult.No)
					{
						return;
					}
				}
				Globals.BoolNewFileNameAlreadyExistsButHasBeenSaved = true;
				if (string.IsNullOrEmpty(Globals.User_Settings.FilePath))
				{
					SaveAsToolStripMenuItem_Click(this, e);
					// OLD	TsMenuSaveAsClick(this, e);
					return;
				}
				string strExt = System.IO.Path.GetExtension(Globals.User_Settings.FilePath);
				switch (strExt.ToUpper())
				{
					case ".RTF":
						RTBMain.SaveFile(Globals.User_Settings.FilePath);
						break;
					default:
						FIO.WriteFile(Globals.User_Settings.FilePath, RTBMain.Text);
						break;
				}
				Globals.PBoolRTBModified = false;

				HandleDropDownListItems();

				frmSaved.Visible = true;
				Cursor.Current = Cursors.Default;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				frmSaved.Visible = false;
			}
		}

		private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileAs();
		}

		void SaveFileAs()
		{
			SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
			string strExt = "";
			List<string> Titles = new List<string>();
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				SaveFileDialog1.Title = "RTE - Save File";
				SaveFileDialog1.DefaultExt = "rtf";
				SaveFileDialog1.Filter = "Rich Text Format(*.rtf)|*.rtf|Text File(*.txt)|*.txt|" + "Text Document -MS-DOS Format(*.txt)|*.txt|Unicode Text Document(*utf)|*.utf|All Files|*.*";
				//  Word Document(*.doc)|*.doc|     This does not appear to list WORD capability, May need to look for an Add-in for Word.
				SaveFileDialog1.FilterIndex = 1;
				SaveFileDialog1.ShowDialog();
				if (SaveFileDialog1.FileName.Length > 0)
				{
					Globals.User_Settings.FilePath = SaveFileDialog1.FileName;
					strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName);
					switch (strExt.ToUpper())
					{
						case ".RTF":
							this.RTBMain.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
							break;
						default:
							FIO.WriteFile(SaveFileDialog1.FileName, RTBMain.Text);
							break;
					}
					Globals.PBoolRTBModified = false;
					this.Text = "Tachufind     Editing: " + Globals.User_Settings.FilePath;

					HandleDropDownListItems();

					frmSaved.Visible = true;
				}
				else
				{
					this.Text = "Tachufind     Editing: New file";
				}
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				frmSaved.Visible = false;
			}
		}

        public enum ElementType
        {
            Section,
            Chart,
            None
        }

        private static int GetSectionOrChartNumber(string strNum) {
			return Int32.Parse(strNum);
        }

        /// <summary>
		///  Sets Globals.sectionNumber Or Globals.chartNumber
        ///  Returns 2 for Section
        ///  Returns 1 for Chart
        ///  Returns -1 for null
        /// </summary>
        /// <param name="sFind"></param>
        /// <returns></returns>
        public static ElementType IsItASectionOrAChart(string sFind)
		{
			try
			{
				if (sFind.Length < 1) { return ElementType.None; }

				string intFind = sFind.ToUpper();
                intFind = intFind.Replace("§", ""); // Remove §
                intFind = intFind.Replace("C", ""); // Remove §
                Globals.SectionOrChartNumber = GetSectionOrChartNumber(intFind); // return section or chat number

                if (sFind.Contains("§")) // IF SECTION CHARACTER (§)
				{
                    return ElementType.Section;
                }
                sFind = sFind.ToUpper();
                if (sFind.Contains("C"))  // IF CHART CHARACTER (C)
				{
                    return ElementType.Chart;
                }
			}
			catch (Exception ex)
			{
				string message = "Section button looks up sections or charts.  To look up a section, click inside the search textbox then click the section button. " +
					" You will see the section symbol appear §.  next, add the section number you want to find, and click the section button again.   For Charts,"+
					" enter an upper or lower case C, followed by the chart number.   Sections should be in the document sequentially i.e. §1, §2, §3, §4, §5" +
					" while charts should be surrounded by their sequence numbers, i. e.  C1ChartC1, C2ChartC2, C3ChartC3, etc.  \n\n" + 
					"See  https://www.archeuslore.com/tachufind/search/charts.html for more details.";
				MessageBox.Show(message, "Entry Formatting", MessageBoxButtons.OK, MessageBoxIcon.Error);
				message = ex.ToString();
			}
            return ElementType.None;
        }

		private bool HandlePreconditionsAndChecks() {
            // Textbox is empty, put in a section character in it when it is clicked
            RTBSearch.Focus();
            if (RTBSearch.Text.Length == 0)
            {
                this.RTBSearch.Text = "§";
                RTBSearch.SelectionStart = 1;
                RTBSearch.SelectionLength = 0;
                RTBSearch.Focus();
                return true;
            }
            // There should be a file open to search
            bool fileCheck = CheckEntryForSearch();
            if (!fileCheck)
            {
                return true;
            }

            string sFind = this.RTBSearch.Text;
            if (sFind == "§")
            {
                MessageBox.Show("Error: No number after the section symbol §");
                return true;
            }

            // sFind should be either §# or C#, # being any number
            Regex pattern = new Regex("^[§C]\\d+$");
			if (!pattern.IsMatch(sFind))
			{
                return true;  // Does not match pattern ignore button click
            }
			return false;
        }



        public static Tuple<int, int> FindSectionOrChartBreadth(RichTextBox richTextBox, int sectionOrChartNumber, ElementType elementType)
        {
            // Get the text from the RichTextBox control
            string input = richTextBox.Text;
			string pattern = string.Empty;
			
			// Section type
			if(elementType == ElementType.Section)
				pattern = $@"§{sectionOrChartNumber}\s+(.*?)§{sectionOrChartNumber + 1}";
            // Chart type
            if (elementType == ElementType.Chart)
                pattern = $@"C{sectionOrChartNumber}\s+(.*?)C{sectionOrChartNumber + 1}";

            // Match the pattern against the input string
            //Match match = Regex.Match(input, pattern);
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success && match.Length > 6) 
            {
                GroupCollection groups = match.Groups; 
                int start = groups[0].Index;
                int end = match.Length;
                return new Tuple<int, int>(start, end);
            }

            // If no match was found, return null
            return new Tuple<int, int>(0, 0);
        }


        private void BtnSection_Click(object sender, EventArgs e)
		{
            this.RTBSearch.Focus();
            this.RTBSearch.Text = this.RTBSearch.Text.ToUpper();
			this.RTBSearch.Refresh();
            // handle preconditions
            if (HandlePreconditionsAndChecks()) { return; };

			Globals.BoolSectionSearchInProgress = false;
            // Preserve position of cursor before search for section or chart.
            int localPropertyCursorPreSearchPosition = this.RTBMain.SelectionStart;

			try
			{
				Cursor.Current = Cursors.WaitCursor;
				Globals.BoolSectionSearchInProgress = true;
				ElementType elementType = ElementType.None;
                elementType = IsItASectionOrAChart(this.RTBSearch.Text);
                Tuple<int, int> start_end = FindSectionOrChartBreadth(this.RTBMain, Globals.SectionOrChartNumber, elementType);
                this.RTBMain.Select(start_end.Item1, start_end.Item2);

                // At this point it does not matter if it is a section or a chart, because we have the start and the end:
                if (this.RTBMain.SelectedRtf.Length < 1)
                {
					return;
				};
                // Open the new window and fill it			
                frmSection.Show();
				frmSection.RTBSection.Rtf = this.RTBMain.SelectedRtf;
                if (frmSection.WindowState == FormWindowState.Minimized)
                {
                    frmSection.WindowState = FormWindowState.Normal;
                }
                Globals.Current_RTB_withFocus = RTBMain;
                this.RTBMain.Focus();
				this.RTBMain.SelectionStart = localPropertyCursorPreSearchPosition;
				this.RTBMain.SelectionLength = 0;


                Globals.BoolSectionSearchInProgress = false;

				if (this.RTBSearch.TextLength > 0)
				{
					frmSection.RTBSection.BackColor = Globals.User_Settings.RTBMainBackColor	;
					frmSection.Visible = true;
					frmSection.Show();
					frmSection.BringToFront();

                    frmSection.Focus();
					this.AddOwnedForm(frmSection);
					int fontSize = (int)frmSection.RTBSection.Font.SizeInPoints;
					Font font = frmSection.RTBSection.Font;
                    frmSection.Size = GeneralFns.UpdateFormSize(frmSection.RTBSection);

                    // Center the form on the screen
                    //StartPosition = FormStartPosition.CenterScreen;
                    //BtnReturn.PerformClick();
                }
                else
				{
					MessageBox.Show("You must input a section or chart number!", "Error . . .", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				// Allow input again
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnDegree_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "°";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnInsertFrenchQuotes_Click(object sender, EventArgs e)
		{
			// «
			Globals.Current_RTB_withFocus.SelectedText = "«»";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnFrenchLowerCase_ae_Click(object sender, EventArgs e)
		{
			int selStart = Globals.Current_RTB_withFocus.SelectionStart;
			// æ
			Globals.Current_RTB_withFocus.SelectedText = "æ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnFrenchLowerCase_oe_Click(object sender, EventArgs e)
		{
			// œ
			Globals.Current_RTB_withFocus.SelectedText = "œ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void SubScript(object sender, EventArgs e)
		{
			float fontSize = Globals.Current_RTB_withFocus.SelectionFont.Size;
			string fontName = Globals.Current_RTB_withFocus.Font.Name;

			int selLen = Globals.Current_RTB_withFocus.SelectionLength;
			genFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, -4, 12);

			Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart + selLen;
			Globals.Current_RTB_withFocus.SelectedText = " ";
			int selStart = Globals.Current_RTB_withFocus.SelectionStart;
			Globals.Current_RTB_withFocus.Select(selStart - 1, 1);
			Globals.Current_RTB_withFocus.SelectionCharOffset = 0;
			Globals.Current_RTB_withFocus.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);
			Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart + 1;
			Globals.Current_RTB_withFocus.SelectionLength = 0;
			Globals.Current_RTB_withFocus.Focus();
		}
		private void BtnSubscript_Click(object sender, EventArgs e)
		{
			SubScript(sender, e);
		}

		private void SuperScript(object sender, EventArgs e)
		{
			float fontSize = Globals.Current_RTB_withFocus.SelectionFont.Size;
			string fontName = Globals.Current_RTB_withFocus.Font.Name;

			int selLen = Globals.Current_RTB_withFocus.SelectionLength;
			genFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, 8, 12);

			Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart + selLen;
			Globals.Current_RTB_withFocus.SelectedText = " ";
			int selStart = Globals.Current_RTB_withFocus.SelectionStart;
			Globals.Current_RTB_withFocus.Select(selStart - 1, 1);
			Globals.Current_RTB_withFocus.SelectionCharOffset = 0;
			Globals.Current_RTB_withFocus.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);
			Globals.Current_RTB_withFocus.SelectionStart = RTBMain.SelectionStart + 1;
			Globals.Current_RTB_withFocus.SelectionLength = 0;
			Globals.Current_RTB_withFocus.Focus();
		}
		private void BtnSuperscript_Click(object sender, EventArgs e)
		{
			SuperScript(sender, e);
		}

		private void BtnCopyright_Click(object sender, EventArgs e)
		{ // ±
			Globals.Current_RTB_withFocus.SelectedText = "±";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnPlusMinus_Click(object sender, EventArgs e)
		{ // ©
			Globals.Current_RTB_withFocus.SelectedText = "©";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnArrowR_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "→";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnIntegers_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ℤ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnNaturals_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ℕ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnElement_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "\u03F5";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnReals_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ℝ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnAnd_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "∧";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnOr_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "∨";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnIdenticalto_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "≡";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnPartialDifferential_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "\u2202";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnDifferential_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ⅅ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnIntegral_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "∫";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnEuler_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ℯ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnDotProd_Click(object sender, EventArgs e)
		{
            Globals.Current_RTB_withFocus.SelectedText = "˙";
            int cursorLocation = Globals.Current_RTB_withFocus.SelectionStart;
            Globals.Current_RTB_withFocus.SelectionStart = cursorLocation - 1;
            Globals.Current_RTB_withFocus.SelectionLength = 1;
            Globals.Current_RTB_withFocus.SelectionFont = new Font(Globals.Current_RTB_withFocus.SelectionFont, FontStyle.Bold);
			Globals.Current_RTB_withFocus.SelectionStart = cursorLocation;
            Globals.Current_RTB_withFocus.SelectionLength = 0;
            Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnInfinity_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "∞";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnSqrt_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "\u221A";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnIntersect_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "\u2229";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnUnion_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "\u222A";
			Globals.Current_RTB_withFocus.Focus();
        }


        // THE TWO BELOW CAN BE DISPLACED OR A DIFFERENT FONT IS NEEDED TO DISPLAY THEM
		private void BtnDotR_Click(object sender, EventArgs e)
		{
			Font currentfont = Globals.Current_RTB_withFocus.Font;
			Globals.Current_RTB_withFocus.SelectionFont = new Font("Cambria", 18);  // FontStyle.Bold
			Globals.Current_RTB_withFocus.SelectedText = "⟩"; 
			Globals.Current_RTB_withFocus.Font = currentfont;
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnDotL_Click(object sender, EventArgs e)
		{
			Font currentfont = Globals.Current_RTB_withFocus.Font;
			Globals.Current_RTB_withFocus.SelectionFont = new Font("Cambria", 18);  // FontStyle.Bold
			Globals.Current_RTB_withFocus.SelectedText = "⟨"; 
			Globals.Current_RTB_withFocus.Font = currentfont;
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnGte_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "≥";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnLte_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "≤";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnApprox_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "≈";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnFrall_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "Ɐ";
			Globals.Current_RTB_withFocus.Focus();
		}

		private void BtnComplex_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "ℂ";
			Globals.Current_RTB_withFocus.Focus();
		}

		//↔∆
		private void RTBMain_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.ShiftKey)
			{
				Globals.ShiftKey = true;
			}
		}

		private void RTBMain_KeyUp(object sender, KeyEventArgs e)
		{
			Globals.ShiftKey = false;
		}

		private void FrmMain_LocationChanged(object sender, EventArgs e)
		{
			if(Globals.FrmMainInit == true) { return; }
				Point pt = new Point(this.Left, this.Top);
				Globals.User_Settings.FrmMainLocation = pt;
		}

		private void FrmMain_ResizeEnd(object sender, EventArgs e)
		{
			if (Globals.FrmMainInit == true) { return; }

			Size sz = new Size(this.Width, this.Height);
			Globals.User_Settings.FrmMainSize = sz;
		}

		private void RTBMain_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				Globals.Current_RTB_withFocus = RTBMain;
				string word = this.RTBMain.SelectedText;
				word = word.Trim();
				RTBSearch.Text = word;
				this.RTBMain.SelectionLength = word.Length;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void RTBMain_MouseDown(object sender, MouseEventArgs e)
		{
			Globals.Current_RTB_withFocus = this.RTBMain;
            Globals.Current_RTB_withFocus.Focus();
            // Capture the starting position of the mouse cursor
            Globals.SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);

            long cursorArrayZeroPosition = 0;
            // mouse left only, as right is used for context menu, and want to be able to copy selected text.
            if (e.Button == MouseButtons.Left)
            {
				Globals.User_Settings.CursorPosition = Globals.SelStart;
                // save location at start so can return to it

                // THIS IS CURSOR HISTORY, (Return Button ) SAVING LAST CURSOR CLICK POSITION
                if ((returnStack != null) & returnStack.Count > 0)
            	{
            		cursorArrayZeroPosition = returnStack.Peek();
            	}

            	Globals.CursorPositionChanged = true;
            // see if current cursor location differs from last cursor location by 40   
            if (Globals.User_Settings.CursorPosition >= cursorArrayZeroPosition + 20 | Globals.User_Settings.CursorPosition <= cursorArrayZeroPosition - 20)
            	{
            		returnStack.Push(Globals.SelStart);
            	}
            	Globals.BoolCursorState = true;
            }
        }

        private void RTBMain_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RTBMain;
            Globals.Current_RTB_withFocus.Focus();

            // Calculate the ending position of the selection
            int selEnd = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);

            // If the starting position and the ending position are the same, return without selecting anything
            if (selEnd == Globals.SelStart){ return; }

            // Determine the selection start and length based on the relative positions of the starting and ending points
            int selectionStart = Math.Min(Globals.SelStart, selEnd);
            int selectionLength = Math.Abs(Globals.SelStart - selEnd);

            // Set the selection start and length
            Globals.Current_RTB_withFocus.SelectionStart = selectionStart;
            Globals.Current_RTB_withFocus.SelectionLength = selectionLength;

            // Set the focus back to the control to keep the selection highlighted
            Globals.Current_RTB_withFocus.Focus();
        }


        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				int answer = 0;
				if (Globals.PBoolRTBModified == true)
				{
					ToggleFormVisibility();
					answer = (int)MessageBox.Show("Save changes to this document?", "Unsaved Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (answer == (int)System.Windows.Forms.DialogResult.Yes)
					{
						// Save the presently open file
						SaveFileBasedOnExtensionType(Globals.User_Settings.FilePath);
					}
					if (answer == (int)System.Windows.Forms.DialogResult.Cancel) {
						return;
					}
				}
				this.RTBMain.Clear();
				this.RTBMain.SelectAll();
				this.RTBMain.SelectionFont = new Font("Times New Roman", 22, FontStyle.Regular);
				this.RTBMain.ForeColor = Color.Black;
				this.RTBMain.BackColor = Globals.User_Settings.RTBMainBackColor;
				Globals.User_Settings.FilePath = "";
				this.Text = "Tachufind    Editing: New file";
				Globals.BoolNewFileNameAlreadyExistsButHasBeenSaved = false;
				Globals.PBoolRTBModified = false;
				AddItemsToDropDownList();  // inserted 8-4-2022
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Globals.FindStart = 0;
			string fileContents; // = string.Empty;

			try
			{
				Cursor.Current = Cursors.WaitCursor;
				bool save = IfNotsavedDocPromptAndSave(sender, e);

				// Because FIO resets current path to the new file, it must run first before
				// we check for the way to apply the text, based on whether it is a RTF file or not.
				fileContents = FIO.OpenFile();  // OpenFile() sets Properties.filePath
				HandleDropDownListItems();
				if (Globals.BoolFileOpenCancelled == true)
				{
					Globals.BoolFileOpenCancelled = false;
					return;
				}
				SetNewlyOpenedFileSettings();

				string extension = FIO.GetFileExtension(Globals.User_Settings.FilePath);
				extension = extension.ToUpper();
				if (extension == ".RTF") {
					fileContents = FixHighLightingBug(fileContents);
					this.RTBMain.Rtf = fileContents;
					Globals.P_RTFMain_RTF = fileContents;  // Change 10-17-2020 Moved, this fn should only do as it says
					this.RTBMain.SelectAll();  // Change 3-17-2022
					this.RTBMain.BackColor = Globals.User_Settings.RTBMainBackColor;
					this.RTBMain.SelectionLength = 0;
				}
				if (extension == ".HTML")
				{
					this.RTBMain.Text = fileContents;
					Globals.P_RTFMain_TXT = fileContents;  // This may need to be changed to .Rtf
					// Globals.pRTFMain_RTF = 
				}
				if (extension == ".TXT")
				{
					this.RTBMain.Text = fileContents;
					Globals.P_RTFMain_TXT = fileContents;
				}


				DisplayFilePath();

				Globals.PBoolRTBModified = false;
				this.RTBMain.SelectionStart = 1;
				Cursor.Current = Cursors.Default;
				this.Refresh();

			}
			catch (Exception ex)
			{
				RemoveItemFromDropDownList(Globals.User_Settings.FilePath);
				string caption = "Error Detected";
				string m = "TsMenuOpenClick - This file no longer exists at this location! Removing file from list." + Environment.NewLine + ex.Message;
				MessageBox.Show(m, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			btnSave.PerformClick();
		}

		private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Globals.Current_RTB_withFocus.CanUndo | Globals.Current_RTB_withFocus.CanRedo)
			{
				Globals.Current_RTB_withFocus.Undo();
				Globals.Current_RTB_withFocus.ClearUndo();
			}
		}

		private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Globals.Current_RTB_withFocus.CanRedo | Globals.Current_RTB_withFocus.CanRedo)
			{
				Globals.Current_RTB_withFocus.Redo();
			}
		}

		private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Added 11-23-2011 Was OpenFileDialog02 in previous  build
			OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

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
				System.Drawing.Image img = null;
				img = System.Drawing.Image.FromFile(strImagePath);
				Clipboard.SetDataObject(img);
				System.Windows.Forms.DataFormats.Format df = null;
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

		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.RTBMain.Text.Length > 0)
			{
				Globals.Current_RTB_withFocus.Copy();
				Globals.Current_RTB_withFocus.Cut();  // This works?
			}
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.RTBMain.Text.Length > 0)
			{
				Globals.Current_RTB_withFocus.Copy();
			}
		}

		private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
			{
				Globals.Current_RTB_withFocus.Paste();
			}
            // Clipboard.Clear();  // ADDED 2-26-2023
        }

		private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Globals.Current_RTB_withFocus.SelectAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void TsMenuWordWrap_Click(object sender, EventArgs e)
		{

			this.RTBMain.WordWrap = !Globals.User_Settings.FrmMainWordWrap;
			Globals.User_Settings.FrmMainWordWrap = this.RTBMain.WordWrap;
			tsMenuWordWrap.Checked = !this.RTBMain.WordWrap;

		}

		private void FontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int answer = 0;
			FontDialog FontDialog1 = new FontDialog();

			try
			{
				if (Globals.User_Settings.FrmOptionsOptOutOfFutureChangeFontWarnings == false)
				{

					answer = (int)MessageBox.Show("Warning: Changing from the default font could render some languages inoperable.  If the " + "font you change to does not have international language capability, it will fail to work properly.  " + "Do you wish to proceed?", "Change Font", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (answer == (int)System.Windows.Forms.DialogResult.No)
					{
						return;
					}
				}

				FontDialog1.ShowColor = true;
				//FontDialog1.Font = this.RTBMain.Font;
				FontDialog1.Font = new Font(Globals.User_Settings.RTBMainFontName, Convert.ToInt32(Globals.User_Settings.RTBMainFontSize));
				FontDialog1.ShowApply = true;
				if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.RTBMain.SelectionFont = FontDialog1.Font;
					Globals.User_Settings.RTBMainFontName = FontDialog1.Font.Name;
                    Globals.User_Settings.FrmQuizFontSize = Convert.ToInt32(FontDialog1.Font.Size.ToString());
                }

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmOptions.Visible = true;
			frmOptions.Show();
			frmOptions.BringToFront();
		}

		private void BackcolorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ColorDialog dlg = new ColorDialog();
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					var _with3 = this.RTBMain;
					_with3.SelectAll();
					_with3.BackColor = dlg.Color;
					_with3.SelectionBackColor = dlg.Color;
					_with3.SelectionLength = 0;
					Globals.User_Settings.RTBMainBackColor = dlg.Color;
					Globals.PBoolRTBModified = true;
					this.RTBMain.SelectAll();
					this.RTBMain.BackColor = dlg.Color;
					this.RTBMain.SelectionLength = 0;
				}
				dlg.Dispose();

				}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void GetHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					// Currently all help is online only
					// Check to see if Help can be reached via the website,
					WebBrowser wb = new WebBrowser();
					string URL = "https://www.archeuslore.com/tachufind/search.html";
					// Get Value based on Key
					wb.Navigate(URL);
					Process.Start(URL);

				}
				catch (Exception ex)
				{
					string caption = "Error Detected";
					MessageBox.Show("Unable to proceed to this web address, you may not have a default web browser designated." + Environment.NewLine + ex.Message, caption, MessageBoxButtons.OK);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		// LOCATION goToWebsiteToolStripMenuItem_Click
		private void GoToWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				WebBrowser wb = new WebBrowser();
				string URL = "https://www.archeuslore.com/tachufind";
				// Get Value based on Key
				wb.Navigate(URL);
				Process.Start(URL);

			}
			catch (Exception ex)
			{
				string caption = "Error Detected";
				MessageBox.Show("Unable to proceed to this web address, you may not have a default web browser designated." + Environment.NewLine + ex.Message, caption, MessageBoxButtons.OK);
			}
		}

		private void RTBSearch_MouseClick(object sender, MouseEventArgs e)
		{
			Globals.Current_RTB_withFocus = RTBSearch;
			RTBSearch.Focus();
		}

		private void Panel4_MouseEnter(object sender, EventArgs e)
		{
			this.toolTip1.SetToolTip(this.FrmMainColorBtn01, Globals.ToolTip01); //_fColor.rtfReplace01.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn02, Globals.ToolTip02); //_fColor.rtfReplace02.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn03, Globals.ToolTip03); //_fColor.rtfReplace03.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn04, Globals.ToolTip04); //_fColor.rtfReplace04.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn05, Globals.ToolTip05); //_fColor.rtfReplace05.Text);
																			 // Start at beginning of BOTtom buttons, at 1
			this.toolTip1.SetToolTip(this.FrmMainColorBtn06, Globals.ToolTip06); //_fColor.rtfReplace06.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn07, Globals.ToolTip07); //_fColor.rtfReplace07.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn08, Globals.ToolTip08); //_fColor.rtfReplace08.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn09, Globals.ToolTip09); //_fColor.rtfReplace09.Text);
			this.toolTip1.SetToolTip(this.FrmMainColorBtn10, Globals.ToolTip10); //_fColor.rtfReplace10.Text);

		}

		private void ColorBtn01_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn01.BackColor = Globals.User_Settings.Color01;
            if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color01)
                {
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color01;
                }
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color01;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void ColorBtn02_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn02.BackColor = Globals.User_Settings.Color02;
            if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color02)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color02;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color02;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void ColorBtn03_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn03.BackColor = Globals.User_Settings.Color03;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color03)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color03;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color03;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void ColorBtn04_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn04.BackColor = Globals.User_Settings.Color04;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color04)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color04;
				}

			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color04;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void ColorBtn05_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn05.BackColor = Globals.User_Settings.Color05;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color05) // SearchSettings.colorBtnTop5)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color05; // SearchSettings.colorBtnTop5)
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color05; // SearchSettings.colorBtnTop5
																	 // Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void ColorBtn06_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn06.BackColor = Globals.User_Settings.Color06;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color06)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color06;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color06;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}


		private void ColorBtn07_Click(object sender, EventArgs e)
		{
			this.FrmMainColorBtn07.BackColor = Globals.User_Settings.Color07;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color07)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color07;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color07;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}


		private void ColorBtn08_Click(object sender, EventArgs e)
		{
			FrmMainColorBtn08.BackColor = Globals.User_Settings.Color08;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color08)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color08;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color08;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}



		private void ColorBtn09_Click(object sender, EventArgs e)
		{
			FrmMainColorBtn09.BackColor = Globals.User_Settings.Color09;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color09)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color09;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color09;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}



		private void ColorBtn10_Click(object sender, EventArgs e)
		{
			FrmMainColorBtn10.BackColor = Globals.User_Settings.Color10;
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (this.RTBMain.SelectionBackColor == Globals.User_Settings.Color10)
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
				}
				else
				{
					this.RTBMain.SelectionBackColor = Globals.User_Settings.Color10;
				}
			}
			else
			{
				this.RTBMain.SelectionColor = Globals.User_Settings.Color10;
				// Restore backcolor requires Control Z
			}
			int cursor = this.RTBMain.SelectionStart;
			this.RTBMain.Select(cursor + this.RTBMain.SelectionLength, 0);
			Globals.Current_RTB_withFocus.Focus();
		}

		private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			Globals.Current_RTB_withFocus = this.RTBMain;
		}

		private void MenuStrip1_MouseClick(object sender, MouseEventArgs e)
		{
			Globals.Current_RTB_withFocus = this.RTBMain;
		}

		private void FileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			OpenFileDialog openFile1 = new OpenFileDialog();
			bool fileExists = false;
			string strExt = "";

			// items above file paths are numbered 1 to 10; 
			// New = 1, Open = 2 etc., including separators
			// This returns if what is clicked is something on the top like Open, New.
			if (e.ClickedItem.Tag != null) { return; } // Return - Something was clicked at either Exit or above
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				e.ClickedItem.Owner.Hide(); // hides drop down after it is clicked
				if (e.ClickedItem.Tag == null)
				{
					if (Globals.PBoolRTBModified == true)
					{
						int answer = 0;
						ToggleFormVisibility();
						answer = (int)MessageBox.Show("Save changes to this document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (answer == (int)System.Windows.Forms.DialogResult.Yes)
						{
							// save the file
							strExt = System.IO.Path.GetExtension(Globals.User_Settings.FilePath);
							strExt = strExt.ToUpper();
							switch (strExt)
							{
								case ".RTF":
									RTBMain.SaveFile(Globals.User_Settings.FilePath);
									break;
								default:
									// to save as plain text
									Cursor.Current = Cursors.WaitCursor;
									System.IO.StreamWriter txtWriter = null;
									txtWriter = new System.IO.StreamWriter(Globals.User_Settings.FilePath);
									txtWriter.Write(RTBMain.Text);
									txtWriter.Close();
									txtWriter = null;
									this.RTBMain.SelectionStart = 0;
									this.RTBMain.SelectionLength = 0;
									// Allow input again
									Cursor.Current = Cursors.Default;
									break;
							}
						}
					}
				}
				if (e.ClickedItem.Tag == null)
				{
					fileExists = File.Exists(e.ClickedItem.Text);
					Globals.User_Settings.FilePath = e.ClickedItem.Text;
					if (fileExists == true)
					{
						openFile1.FileName = e.ClickedItem.Text;
						Globals.User_Settings.FilePath = openFile1.FileName;
						strExt = System.IO.Path.GetExtension(openFile1.FileName);
						strExt = strExt.ToUpper();
						if (strExt == ".RTF")
						{
							RTBMain.LoadFile(openFile1.FileName, RichTextBoxStreamType.RichText);
							string rtf = RTBMain.Rtf;  // For changes
						}
						else
						{
							RTBMain.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
						}
						// Load the contents of the file into the RichTextBox.
						Globals.User_Settings.FilePath = openFile1.FileName;
						// save contents for later check for changes
						string strMyOriginalText = RTBMain.Text;

						HandleDropDownListItems();

						Globals.PBoolRTBModified = false;
					}
					else
					{
						RemoveItemFromDropDownList(Globals.User_Settings.FilePath);
						AddItemsToDropDownList();
						string caption = "No File Exists";
						MessageBox.Show("TsTopMenuFileDropDownItemClicked - This file no longer exists at this location! Removing file name from list.", caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Cursor.Current = Cursors.Default;
						return;
					}
				}
				if (Globals.User_Settings.FilePath.Length > 0)
				{
					this.Text = "Tachufind     Editing: " + openFile1.FileName;
				}
				else
				{
					this.Text = "Tachufind     Editing: New file";
				}
				//If Me.RTBMain.TextLength < Math.Abs(Properties.pIntFrmMainRTBSelectionStart) Then
				//    Me.RTBMain.SelectionStart = Properties.pIntFrmMainRTBSelectionStart
				//End If
				// addItemToTopOfDropDownList(Properties.filePath);
				SetNewlyOpenedFileSettings();
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void BtnQuiz_Click(object sender, EventArgs e)
		{
            frmQuiz.Visible = true;
			frmQuiz.Show();
			frmQuiz.BringToFront();
			frmQuiz.Left = Globals.User_Settings.FrmQuizLocation.X;
			frmQuiz.Top = Globals.User_Settings.FrmQuizLocation.Y;
        }


        private void FrmMain_PreviewKeyDown(object sender, KeyEventArgs e)
		{
            keyboardShortcuts.HandleFKeys(sender, e);
            keyboardShortcuts.HandleAltKeys(sender, e);
            keyboardShortcuts.HandleControlKeys(sender, e);
            keyboardShortcuts.HandleShiftKeys(sender, e);
			}

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
		{
            keyboardShortcuts.ShowKeyboardShortcutsPopWindowIfNeeded();
		}

        private void BtnShort_Click(object sender, EventArgs e)
        {
            keyboardShortcuts.SetShort();
        }

        private void BtnMacron_Click(object sender, EventArgs e)
        {
            keyboardShortcuts.SetMacron();
        }

        private void BtnGrave_Click(object sender, EventArgs e)
        {
            keyboardShortcuts.SetGraveAccent();
        }

        private void BtnAcute_Click(object sender, EventArgs e)
        {
            keyboardShortcuts.SetAcuteAccent();
        }


        private void ColorBtn01_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn01.BackColor = newColor;
                    FrmMainColorBtn01.BackColor = this.FrmMainColorBtn01.BackColor = newColor;
                    FrmMainColorBtn01.ForeColor = newColor;
                    Globals.User_Settings.Color01 = newColor;
                }
			}
		}

        private void ColorBtn02_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn02.BackColor = newColor;
                    FrmMainColorBtn02.BackColor = this.FrmMainColorBtn02.BackColor = newColor;
                    FrmMainColorBtn02.ForeColor = newColor;
                    Globals.User_Settings.Color02 = newColor;
                }
			}
		}

        private void ColorBtn03_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
					Color newColor = colorDialog1.Color;
					this.FrmMainColorBtn03.BackColor = newColor;
					FrmMainColorBtn03.BackColor = this.FrmMainColorBtn03.BackColor = newColor;
					FrmMainColorBtn03.ForeColor = newColor;
					Globals.User_Settings.Color03 = newColor;
				}
			}
		}

        private void ColorBtn04_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn04.BackColor = newColor;
                    FrmMainColorBtn04.BackColor = this.FrmMainColorBtn04.BackColor = newColor;
                    FrmMainColorBtn04.ForeColor = newColor;
                    Globals.User_Settings.Color04 = newColor;
                }
			}
		}

        private void ColorBtn05_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn05.BackColor = newColor;
                    FrmMainColorBtn05.BackColor = this.FrmMainColorBtn05.BackColor = newColor;
                    FrmMainColorBtn05.ForeColor = newColor;
                    Globals.User_Settings.Color05 = newColor;
                }
			}
		}

        private void ColorBtn06_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn06.BackColor = newColor;
                    FrmMainColorBtn06.BackColor = this.FrmMainColorBtn06.BackColor = newColor;
                    FrmMainColorBtn06.ForeColor = newColor;
                    Globals.User_Settings.Color06 = newColor;
                }
			}
		}

        private void ColorBtn07_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
					Color newColor = colorDialog1.Color;
					this.FrmMainColorBtn07.BackColor = newColor;
                    FrmMainColorBtn07.BackColor = this.FrmMainColorBtn07.BackColor = newColor;
					FrmMainColorBtn07.ForeColor = newColor;
					Globals.User_Settings.Color07 = newColor;
				}
			}
		}

        private void ColorBtn08_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn08.BackColor = newColor;
                    FrmMainColorBtn08.BackColor = this.FrmMainColorBtn08.BackColor = newColor;
                    FrmMainColorBtn08.ForeColor = newColor;
                    Globals.User_Settings.Color08 = newColor;
                }
			}
		}

        private void ColorBtn09_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn09.BackColor = newColor;
                    FrmMainColorBtn09.BackColor = this.FrmMainColorBtn09.BackColor = newColor;
                    FrmMainColorBtn09.ForeColor = newColor;
                    Globals.User_Settings.Color09 = newColor;
                }
			}
		}

        private void ColorBtn10_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				DialogResult result = colorDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
                    Color newColor = colorDialog1.Color;
                    this.FrmMainColorBtn10.BackColor = newColor;
                    FrmMainColorBtn10.BackColor = this.FrmMainColorBtn10.BackColor = newColor;
                    FrmMainColorBtn10.ForeColor = newColor;
                    Globals.User_Settings.Color10 = newColor;
                }
			}
		}

        private void BtnImageInsert_Click_1(object sender, EventArgs e)
        {
			OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
			try
			{
				OpenFileDialog1.Title = "RTE - Insert Image File";
				OpenFileDialog1.DefaultExt = "rtf";
				OpenFileDialog1.Filter = "JPEG Files|*.jpg|GIF Files|*.gif|Bitmap Files|*.bmp|Files|*.*";
				OpenFileDialog1.FilterIndex = 1;
				OpenFileDialog1.ShowDialog();

				if (string.IsNullOrEmpty(OpenFileDialog1.FileName))
					return;

				string strImagePath = OpenFileDialog1.FileName;
                System.Drawing.Image img = null;
				img = System.Drawing.Image.FromFile(strImagePath);
				Clipboard.SetDataObject(img);
				System.Windows.Forms.DataFormats.Format df = null;
				df = DataFormats.GetFormat(DataFormats.Bitmap);
				if (Globals.Current_RTB_withFocus.CanPaste(df))
				{
					Globals.Current_RTB_withFocus.Paste(df);
				}
                Clipboard.Clear();  // ADDED 2-26-2023
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


        #region SearchReplace
        // ==================================================================================
        //  START TEXT REPLACE PROCEDURES    ==================================================
        // ==================================================================================

        private bool CheckForPrefix(int positionBeforeString)
        {
            string prefix = GeneralFns.DoMid(this.RTBMain.Text, (positionBeforeString), 1);
            bool bool_Space = !char.IsLetter(Convert.ToChar(prefix));

            if (bool_Space)
            {
                return true;  // It is a prefix
            }
			return false;
        }

        private bool CheckForSuffix(string endPoint) {

            bool bool_Space = !char.IsLetter(Convert.ToChar(endPoint));
            if (bool_Space)
            {
                return true;  // It is a suffix
            }
			return false;
        }

        // Function has been optomized for speed
        // Check text found, to see if it is a match for the mode selected, 
        // Modes are Suffix, Prefix or Replace All, if a match, return TRUE
        // LOCATION checkForMatchOfType
        private bool CheckForMatchOfType(SearchInstance searchItem)
        {
            bool bool_CursorPointIsALetter;

            try
            {
                int strLength = searchItem.findString.Length;

                if (searchItem.position < 0) { return false; }
                // GeneralFns.doMid AKA test = "1234567" & If startIndex = 5, length = 2 then "67"
                string endPoint = GeneralFns.DoMid(this.RTBMain.Text, (searchItem.position + strLength), 1);
                string cursorPoint = GeneralFns.DoMid(this.RTBMain.Text, searchItem.position, 1);

                // This means replace all is active, (Not replace text, we are in color mode if we got here)
                // With rbReplaceAll active, we do not need to check for a suffix or a prefix.
                if (Globals.ReplaceAll) { return true; }  // color replace

                bool_CursorPointIsALetter = char.IsLetter(Convert.ToChar(cursorPoint));  // Expected to be true always

				if (bool_CursorPointIsALetter) {
                    if (Globals.Suffix)
                    {
                        return CheckForSuffix(endPoint);
                    }

                    int positionBeforeString = searchItem.position - 1;
                    if (Globals.Prefix & positionBeforeString > 0) // Make sure positionBeforeString is not negative
                    {
						return CheckForPrefix(positionBeforeString);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

		// This is for shading textboxes to show that their search has been completed
        public void SetTextBackColorInSearchBox(FrmColor frmColor, int txtboxnum, Color clr)
        {
            switch (txtboxnum)
            {
                case 0:
                    frmColor.rtfFind01.BackColor = clr;
                    break;
                case 1:
                    frmColor.rtfFind02.BackColor = clr;
                    break;
                case 2:
                    frmColor.rtfFind03.BackColor = clr;
                    break;
                case 3:
                    frmColor.rtfFind04.BackColor = clr;
                    break;
                case 4:
                    frmColor.rtfFind05.BackColor = clr;
                    break;
                case 5:
                    frmColor.rtfFind06.BackColor = clr;
                    break;
                case 6:
                    frmColor.rtfFind07.BackColor = clr;
                    break;
                case 7:
                    frmColor.rtfFind08.BackColor = clr;
                    break;
                case 8:
                    frmColor.rtfFind09.BackColor = clr;
                    break;
                case 9:
                    frmColor.rtfFind10.BackColor = clr;
                    break;
            }
            frmColor.Refresh();
        }

		// ==================================================================================
		//  END TEXT REPLACE PROCEDURES    ==================================================
		// ==================================================================================	
		// System.Text.RegularExpressions.RegexOptions regexOption = RegexOptions.None ;
		// Possible RegexOptions:  IgnoreCase; IgnorePatternWhitespace; RightToLeft; Singleline;
		// Search Replace for RTF Text
		private string SearchReplaceRTFText(RichTextBox richTextBox, string searchRTF, string replaceRTF, RegexOptions regexOption = RegexOptions.None)
		{
			{
				string result = richTextBox.Rtf;
				try
				{
					Regex regex = new Regex(searchRTF, regexOption);
					result = regex.Replace(result, replaceRTF); // Replace matched text
				}
				catch (Exception ex)
				{
					// Handle any exceptions thrown by the regular expression
					Console.WriteLine("Error: " + ex.Message);
				}
				return result;
			}
		}

        // ==================================================================================
        //  BEGIN TEXT AND COLOR REPLACE PROCEDURES  ========================================
        // ==================================================================================

        public void LoadQueueSearches()
		{
			string[] tempArry = null;
			string[] pFrmColorFindArry = { SearchSettings.frmColorFind01_Txt, SearchSettings.frmColorFind02_Txt,
			SearchSettings.frmColorFind03_Txt, SearchSettings.frmColorFind04_Txt, SearchSettings.frmColorFind05_Txt,
			SearchSettings.frmColorFind06_Txt, SearchSettings.frmColorFind07_Txt, SearchSettings.frmColorFind08_Txt,
			SearchSettings.frmColorFind09_Txt,SearchSettings.frmColorFind10_Txt};
			string[] pFrmColorReplaceArry = {SearchSettings.frmColorReplace01_Txt,
			SearchSettings.frmColorReplace02_Txt, SearchSettings.frmColorReplace03_Txt, SearchSettings.frmColorReplace04_Txt,
			SearchSettings.frmColorReplace05_Txt, SearchSettings.frmColorReplace06_Txt, SearchSettings.frmColorReplace07_Txt,
			SearchSettings.frmColorReplace08_Txt, SearchSettings.frmColorReplace09_Txt, SearchSettings.frmColorReplace10_Txt};
			Color[] colorUserSettings = {Globals.User_Settings.Color01, Globals.User_Settings.Color02, Globals.User_Settings.Color03,
                Globals.User_Settings.Color04, Globals.User_Settings.Color05, Globals.User_Settings.Color06, Globals.User_Settings.Color07,
                Globals.User_Settings.Color08, Globals.User_Settings.Color09, Globals.User_Settings.Color10};


			Color find_Color = Color.Black;  // Default to Simple Replace
			for (int index = 0; index < 10; index++)  // 0-9
			{
				char separator = '|';
				tempArry = pFrmColorFindArry[index].Split(separator);  // i represents a find textbox 1 - 10
				foreach (string unit in tempArry) // use pipe symbol as a way of stacking searches in a textbox
				{
					if (SearchSettings.rbEditColor)
					{ 
						find_Color = colorUserSettings[index];
					}
					if (!string.IsNullOrEmpty(unit) & unit != "")
					{
						SearchInstance search = new SearchInstance();
						// Load the queSearches, with color replace
						search.findColor = find_Color;
                        search.findString = unit;
                        search.replaceString = pFrmColorReplaceArry[index] + "";  // "" prevents empties
						search.textBoxNum = index;
						search.RichTextBoxFinds_GFindMode = GetFindMode();
						queSearches.Enqueue(search);
					}
				}
			}
		}

		// Return true if odd number of BoundryMarkers 
		public bool CheckForOddNumberOfBoundryMarkers()
		{
			bool result = false;
			boundryMarkerCount = Regex.Matches(RTBMain.Text, "¦¦¦").Count;
			if (boundryMarkerCount % 2 == 1)
			{
				result = true;  // Odd number of boundryMarkers
				string message = "You have " + boundryMarkerCount + " boundary markers!" + " There should be an even number " + Environment.NewLine + "of boundary markers. ";
				MessageBox.Show(message + "You must correct this before proceeding!", "Uneven number of boundary markers present!", MessageBoxButtons.OK);
			}
			return result;
		}

		// This will control the maximum time spent on a seach done using FrmColor.  The duration is set on FrmColor, and
		// is stored in Global.searchTimeLimit.
		// btnReplace_All_Click assigns startTime to DateTime.Now() when clicked
		// Each While loop in searches updates endTime on each loop
		// Globals.searchInProgress = true; shows that a search is in progress
		// LOCATION SEARCH TIME CONTROL 
		private void SearchTimeControl(FrmColor frmColor)  // <<------pass frmColor in
		{
			Globals.CurrentTime = DateTime.Now;
			var diffInSeconds = (Globals.StartTime - Globals.CurrentTime).TotalSeconds;
			int elapsed = Convert.ToInt32(diffInSeconds);
			timeLeft = Globals.DisplayTime + elapsed;  // (elapsed is negative)
			if (timeLeft < 0)
			{
				string message = "Your search has exceeed the time you alloted for it," + Environment.NewLine
					+ "under Search Time in Seconds, do you want to continue?";
				DialogResult dr = MessageBox.Show(message, "Time limit exceed", MessageBoxButtons.YesNo);
				switch (dr)
				{
					case DialogResult.Yes:
						this.Invoke((MethodInvoker)(() => frmColor.txtSearchTime.Text = Globals.DisplayTime.ToString()));
						this.Invoke((MethodInvoker)(() => frmColor.Refresh()));
						Globals.StartTime = DateTime.Now;
						break;
					case DialogResult.No:
						this.Invoke((MethodInvoker)(() => frmColor.txtSearchTime.Text = Globals.DisplayTime.ToString()));
						Globals.SearchInProgress = false;
						break;
				}
			}
			else
			{ // update frmColor time.  (elapsed is negative)
				this.Invoke((MethodInvoker)(() => frmColor.txtSearchTime.Text = timeLeft.ToString()));
				this.Invoke((MethodInvoker)(() => frmColor.Refresh()));
			}
		}

		// This should only return Boundary Marker 1, Boundary Marker 2 or -1
		private int[] GetBoundaryMarker_Positions(int start)
		{
			EOF = this.RTBMain.TextLength;
			int BM1Position;
			int BM2Position;
			int testForFutureBMs = 0;
			int[] bms = { 0, 0, 0 };
			int nextStartPointForBMSearch = -1;

			// Why am I getting a -1 from start?
			// Get first BM that occurs after start
			BM1Position = this.RTBMain.Find("¦¦¦", start, EOF, RichTextBoxFinds.NoHighlight); // Get position of BM1
			BM2Position = this.RTBMain.Find("¦¦¦", BM1Position + 3, EOF, RichTextBoxFinds.None); // Get position of  BM2
																								 // See if any BMs beyond this set
			if (BM2Position > 0 & BM2Position + 3 < EOF)
			{
				testForFutureBMs = this.RTBMain.Find("¦¦¦", BM2Position + 3, EOF, RichTextBoxFinds.NoHighlight);
			}
			if (BM2Position + 3 >= EOF | BM2Position < 0 | testForFutureBMs < 0)
			{
				nextStartPointForBMSearch = -1; // NO BMs left
			}
			else
			{
				nextStartPointForBMSearch = BM2Position + 3; // search past BM2Position position
			}
			bms[0] = BM1Position;
			bms[1] = BM2Position;
			bms[2] = nextStartPointForBMSearch;
			return bms;
		}


        // LOCATION frmMain start point for replace color mode and replace text mode - this is called from FrmColor
        // 1. Does checking
        // 2. Loads queSearches
        // 3. Determines if Replace is a simple replace, or if it is a replace within boundary markers
        // 4. Delegates to either GlobalReplaceSimple or GlobalReplaceWithinBoundaryMarkers
        public void FrmColorbtnReplace_All_In_Main(FrmColor frmColor)
		{
			try { 
				Cursor.Current = Cursors.WaitCursor;
				cursorPreSearchPosition = RTBMain.SelectionStart;
				Globals.PBoolRTBModified = true;
				EOF = this.RTBMain.TextLength;

				if (this.RTBMain.Rtf.Length > 0) { Globals.P_RTFMain_RTF = this.RTBMain.Rtf; } // Allows one undo after a search

				// Exit sub if boundary marker count is an odd number
				if (CheckForOddNumberOfBoundryMarkers()) {
					return;
				}

				queSearches.Clear();
                LoadQueueSearches();
                if (queSearches.Count == 0) { return; }  // If nothing in Queue, exit

				if (boundryMarkerCount == 0)
				{  // No boundary markers, simple replace
                    GlobalReplaceSimple(frmColor);
                }
                else
				{   // boundryMarkers PRESENT, more complex replace
					GlobalReplaceWithinBoundaryMarkers(frmColor);
				}

				// exiting function, reset variables, etc.
				Globals.User_Settings.CursorPosition = cursorPreSearchPosition;
				this.RTBMain.SelectionStart = cursorPreSearchPosition;
				Cursor.Current = Cursors.Default;
				boundryMarkerCount = 0;
				this.Focus();
			}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// simpleGlobalReplace No boundryMarkers Present, do normal replace
		public void GlobalReplaceSimple(FrmColor frmColor)
		{
			SearchInstance searchItem = new SearchInstance();
			searchItem.position = 1;
			int start = 0;
			bool match = false;

			while (queSearches.Count > 0)
			{
				searchItem = (SearchInstance)queSearches.Dequeue();

                // get items one by one
                while (searchItem.position > -1)
				{
					Globals.CurrentTime = DateTime.Now;
					SearchTimeControl(frmColor);
					if (Globals.SearchInProgress == false) { break; } // Break out of search, time limit exceeded

					EOF = this.RTBMain.TextLength;  // This has to be check each time, when replace is happening
					searchItem.position = this.RTBMain.Find(searchItem.findString, start, EOF, GetFindMode());
                    if (searchItem.position < 0)
					{ // not found, re-initialize
						start = 0; searchItem.position = 1;
						SetTextBackColorInSearchBox(frmColor, searchItem.textBoxNum, Color.AliceBlue); // color to show complete
						break;
					}
                    // If match on searchType return true
                    match = CheckForMatchOfType(searchItem);
                    if (match)
					{
						if (SearchSettings.rbEditColor)
						{   // Replace text Fore Color
							this.RTBMain.SelectionColor = searchItem.findColor; 
						}
						else
						{   // Text replace
							this.RTBMain.SelectedText = searchItem.replaceString;
						}
					}
					start = searchItem.position + 1;
				}
			}
            this.Refresh();
            this.BtnReturn.PerformClick();
		}

		// complexGlobalReplace BoundryMarkers Present, do complex replace
		public void GlobalReplaceWithinBoundaryMarkers(FrmColor frmColor)
		{
			EOF = this.RTBMain.TextLength;
			int BMStart = 0;
			int cursor = 0;
			int BMEnd = 0;
			int nextStartPointForBMSearch = 0;
			int[] BMLocation = { 0, 0, 0 };
			bool searchNotCompletedForThisItem = true;
			SearchInstance searchItem = new SearchInstance();

			try
			{
				BMLocation = GetBoundaryMarker_Positions(0); // Get initial BMs
				cursor = BMLocation[0]; BMEnd = BMLocation[1]; nextStartPointForBMSearch = BMLocation[2];

				while (queSearches.Count > 0)
				{
					// Next two lines just for preventing search lock
					SearchTimeControl(frmColor);
					if (Globals.SearchInProgress == false) { break; } // Break out of search, time limit exceeded
					searchNotCompletedForThisItem = true;

					searchItem = (SearchInstance)queSearches.Dequeue(); // Get next search item
					while (searchNotCompletedForThisItem)
					{
						// Search for item in rtfMain   between cursor,  BM2
						while (cursor > -1 & cursor < BMEnd)
						{
							cursor = this.RTBMain.Find(searchItem.findString, cursor, BMEnd, GetFindMode());
							searchItem.position = cursor;
							// If match on searchType return true
							if (CheckForMatchOfType(searchItem) == true)
							{  // Select found item and set text ForeColor
								this.RTBMain.Select(cursor, searchItem.findString.Length);
								if (SearchSettings.rbEditColor)
								{   // Replace text Fore Color
									this.RTBMain.SelectionColor = searchItem.findColor;
								}
								else
								{   // Text replace
									this.RTBMain.SelectedText = searchItem.replaceString;
								}
								this.Refresh();
							}
							if (cursor > -1) { cursor += searchItem.findString.Length; }
						}
						if (nextStartPointForBMSearch < 0) // No BMs left, break out
						{
							SetTextBackColorInSearchBox(frmColor, searchItem.textBoxNum, Color.AliceBlue);
							this.Refresh();
							searchNotCompletedForThisItem = false; // Done with this item, goto next item
							nextStartPointForBMSearch = 0; // Reset for next search
							break; // No BMs left to search for
						}
						else
						{ // Boundary markers left to search through
							BMLocation = GetBoundaryMarker_Positions(nextStartPointForBMSearch);
							BMStart = BMLocation[0]; BMEnd = BMLocation[1]; nextStartPointForBMSearch = BMLocation[2];
							if (BMStart < 0 | BMEnd < 0)
							{ // no more BMs
								searchNotCompletedForThisItem = false;
								BMStart = 0;
								cursor = 0;
							}
							else
							{
								cursor = BMStart + 3; // start cursor at start of range BMStart <---> BMEnd
							}
						}
					}
				}
				this.BtnReturn.PerformClick();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


		} // END Case 3. replaceColorWithinBoundryMarkers - COLOR - boundryMarkers PRESENT replace within them

        // ==================================================================================
        //  END TEXT AND COLOR REPLACE PROCEDURES  ========================================
        // ==================================================================================
        #endregion // SearchReplace

        private void RTBMain_MouseClick(object sender, MouseEventArgs e)
        {
			RTBMain.SelectionStart = RTBMain.GetCharIndexFromPosition(e.Location);
		}

        public void BtnBoundryMarker_Click(object sender, EventArgs e)
		{
			RTBMain.SelectionColor = Color.FromArgb(255, 255, 128, 255);
			RTBMain.SelectedText = "¦¦¦";
        }

		//  In Rtf, ¦¦¦ = \'a6\'a6\'a6
		public void ClearBoundaryMarkers()
		{
			RTBMain.Rtf = RTBMain.Rtf.Replace("\\'a6\\'a6\\'a6", string.Empty);
			EOF = RTBMain.Text.Length;
		}

        private void BtnDeleteBoundaryMarkers_Click(object sender, EventArgs e)
        {
			ClearBoundaryMarkers();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			frmAboutApp.Visible = true;
			frmAboutApp.Show();
			frmAboutApp.BringToFront();
		}

        private void BtnOCR_Click(object sender, EventArgs e)
        {
            FrmOCR frmOCR = new FrmOCR();
            frmOCR.Show();
            frmOCR.BringToFront();
			frmOCR.Visible = true;
		}

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
			RTBSearch.Text = String.Empty;
		}

        private void BtnUmlaut_Click(object sender, EventArgs e)
        {
			keyboardShortcuts.SetUmlaut();
		}

        private void BtnCircumflex_Click(object sender, EventArgs e)
        {
            keyboardShortcuts.SetCircumflex();
		}

        private void BtnApostrophe_Click(object sender, EventArgs e)
        {
			Globals.Current_RTB_withFocus.SelectedText = "᾽";
			Globals.Current_RTB_withFocus.Focus();
		}

        private void BtnLongDash_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus.SelectedText = "—";
			Globals.Current_RTB_withFocus.Focus();
		}

        private void BtnInsertFancyQuotes_Click(object sender, EventArgs e)
        {
			Globals.Current_RTB_withFocus.SelectedText = "“”";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
		}

        private void InsertParenthesis_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "()";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        // The following two procedures control whether Text-to-Speech shows or Special Characters show on the Main Form
        private void BtnTTS_Click(object sender, EventArgs e)
        {
            BtnTTS.Visible = false;
            TTS_Panel.Visible = false;
            BtnDisplayButtons.Visible = true;
            ButtonsPanel.Visible = true;
        }

        private void BtnDisplayButtons_Click(object sender, EventArgs e)
        {
            BtnTTS.Visible = true;
            TTS_Panel.Visible = true;
            BtnDisplayButtons.Visible = false;
            ButtonsPanel.Visible = false;
        }
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // TTS
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Need a fake bool because dictionary only takes strings!
        private string IsLineBracketed(string line)
        {
			string fake_bool = "0";

			line = line.TrimStart();  line = line.TrimEnd();
            if(line.StartsWith("[") && line.EndsWith("]")){
				fake_bool = "1";
            }
            return fake_bool;
        }
		
		private int GetLineCusorIsOn()
		{
			int lineNumber = 0;
			int cursorPosition = RTBMain.SelectionStart;
			lineNumber = RTBMain.GetLineFromCharIndex(cursorPosition);
			return lineNumber;
		}

        // TODO ScrollToMiddle
        private void ScrollToMiddle(int lineNumber)
        {
            RTBMain.Focus();
            int visibleLines = RTBMain.ClientSize.Height / RTBMain.Font.Height;
            if (visibleLines <= 0) return;  // Prevent division by 0

            int totalLines = RTBMain.Lines.Length;
            if (lineNumber < 0 || lineNumber >= totalLines) return;  // Prevent argument out of range

            int lineStart = RTBMain.GetFirstCharIndexFromLine(lineNumber - (visibleLines / 2));
            RTBMain.SelectionStart = lineStart;
            RTBMain.SelectionLength = 0;
            RTBMain.ScrollToCaret();
        }

        private void Synth_SpeakProgress(object sender, SpeakProgressEventArgs e)
		{
			try 
			{ 
				//bool exit = false;
				if (Globals.FirstPass) {
					int lineNumber = GetLineCusorIsOn();
					if (lineInfoDict.TryGetValue(lineNumber, out LineInfo lineInfo))
					{
						while (lineNumber < lineInfoDict.Count) //  && exit == false)
						{
							if (chkSkipBracketedText.Checked && lineInfo.IsLineBracketed == "1") { lineNumber++; }
							else
							{
								SelectAndColorText(this.RTBMain, Color.LightCyan, lineInfo.LineStart, lineInfo.LineEnd);
								//exit = true;
								break;
							}
                        }
                        // To do this, you have to have a standard font, and reliable ClientSize, which seems to imply less diverse text.
						// I don' think I want to go there, to much restrictions.
                        // ScrollToMiddle(lineNumber);  
                    }
                    Globals.FirstPass = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void ReinitializeTTS()
        {
            loadfirst = true;
            synthStopped = false;
            lineNumber = 0;
            lines = new string[] { };
            lineInfoDict.Clear();
            lineInfo.Clear();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            BtnTTSStop_Click((object)sender, e);
            RTBMain.Refresh();
        }

        private void BtnTTSStop_Click(object sender, EventArgs e)
        {
            try
            {
                synthStopped = true;
                Globals.Synth.SpeakAsyncCancelAll();
                Globals.FirstPass = true;
                btnTTSPause.Enabled = false;
                ResetTextBackColor(this.RTBMain);  // Resets backColor to normal
                ReinitializeTTS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTTSPause_Click(object sender, EventArgs e)
        {
            btnTTSPause.Enabled = false;
            Globals.Synth.Pause();
        }

        private void BtnTTSPlay_Click(object sender, EventArgs e)
        {
            btnTTSPlay.Enabled= false;
            try 
		   { 
				SpeechSynthesizer synth = new SpeechSynthesizer();
				Globals.Synth = synth;
                btnTTSPlay.Enabled = false;
                btnTTSPause.Enabled = true;
				btnTTSStop.Enabled = true;
				synthStopped = false;
				SetVoiceAndVolume(Globals.Synth);
				Globals.SpeechSynthClosing = true;

                if (RTBMain.SelectionLength > 0)
                {
                    btnTTSPlay.Enabled = false;
                    Globals.Synth.Speak(RTBMain.SelectedText);
                    btnTTSPlay.Enabled = true;
                    btnTTSStop.Enabled = false;
                    return;
                }

                lineNumber = GetLineCusorIsOn();  // lineNumber is declared at module level as 0, for synth
				Globals.Synth.SpeakProgress += Synth_SpeakProgress;
				Globals.Synth.SpeakCompleted += Synth_SpeakCompleted;

				if (RTBMain.Text.Length < 1) { return; }
				lines = RTBMain.Text.Split(new[] { "\n" }, StringSplitOptions.None);
				if (lines.Length < 1) { return; }
				int n = 0;
				int lineStartCharacterIndex = 0;  // Tracks the start of each line that is to be highlighted
				lineInfoDict.Clear();
				lineInfo.Clear();
				// Load information on each line in the text to lineInfoDict : 
				// line number, lineStartCharacterIndex, lineEndCharacterIndex, isLineBracketed
				foreach (string line in lines) {
					int lineEndCharacterIndex = lineStartCharacterIndex + line.Length;  // lineEnd tracks the end of each line that is to be highlighted
					string bracketed = IsLineBracketed(line);  // Dictionary needs a  string, so fake bool - 0 = false, 1 = true, 
					lineInfoDict.Add(n, new LineInfo(lineStartCharacterIndex, lineEndCharacterIndex, bracketed));
					lineStartCharacterIndex = lineStartCharacterIndex + line.Length + 1;  // Gets us to next line beginning position
                    n++;  // Go to next line
                }
				SpeakLine();
                btnTTSPlay.Enabled = true;
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void ScrollRichTextBox()
        {
			int lineStartIndex = 0;
			int lastVisibleLineNumber = 0;
            // Get the last visible line in the text box. but assume less is visible than really is visible, to limit text that is read toward the center.
            int lastVisibleCharIndex = this.RTBMain.GetCharIndexFromPosition(new Point(this.RTBMain.ClientSize.Width - 1, this.RTBMain.ClientSize.Height - 1));
            int lastVisibleLineIndex = this.RTBMain.GetLineFromCharIndex(lastVisibleCharIndex);
			if(lastVisibleLineIndex > 4)
				lastVisibleLineNumber = lastVisibleLineIndex-3;

			if (Globals.currentTTSLineNumber > 2) {
                lineStartIndex = this.RTBMain.GetFirstCharIndexFromLine(Globals.currentTTSLineNumber - 2);
            }            

            // IF selected text is below visible area
            if (Globals.currentTTSLineNumber > lastVisibleLineNumber)
            {
				RTBMain.SelectionStart = lineStartIndex;
                RTBMain.Refresh();
                RTBMain.ScrollToCaret(); // scroll to the current position of the caret
            }
        }

        private void SpeakLine()
        {
			try 
		   {
                ScrollRichTextBox();
                this.RTBMain.Refresh();

                if ((lineNumber >= lines.Length) || synthStopped)
				{
					Globals.Synth.SpeakCompleted -= Synth_SpeakCompleted;
					btnTTSStop.Enabled = false;
					btnTTSPlay.Enabled = true;
					return;
				}
				if (lineInfoDict.TryGetValue(lineNumber, out LineInfo lineInfo))
				{
					if (chkSkipBracketedText.Checked && lineInfo.IsLineBracketed == "1") { 
						lineNumber++;
                        Globals.Synth.SpeakAsync(lines[lineNumber++]);  // // initialized as 0, for synth
					}
					else
					{  // This is where the actual read of line occurrs.
						string line = lines[lineNumber++]; // get the line
                        if (Regex.IsMatch(line, @"\[[^\]]*\]"))  // check if the line has brackets
                        {
                            line = Regex.Replace(line, @"\[[^\]]*\]", "");  // remove brackets and text within brackets
                        }
                        Globals.currentTTSLineNumber = lineNumber + 3;
                        Globals.Synth.SpeakAsync(line);  // // initialized as 0, for synth
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

// This runs at the end of each line
		private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
			try 
	  	   { 
				LineInfo lineInfo;
				ResetTextBackColor(this.RTBMain);  // Resets backColor to normal
				SpeakLine();
				// Highlight next line  THIS WAS CHANGED 2-4-23 FROM lineIndex to lineNumber
				int nextLine = lineNumber - 1;
				if (lineInfoDict.TryGetValue(nextLine, out lineInfo) && nextLine < lineInfoDict.Count) {
					SelectAndColorText(this.RTBMain, Color.LightCyan, lineInfo.LineStart, lineInfo.LineEnd);
				}
                DisposeSynth();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DisposeSynth() {
            if (synth != null)
            {
                synth.Dispose();
            }
        }

        private void SelectAndColorText(RichTextBox RTBMain, Color color, int startIndex, int endIndex)
        {
            RTBMain.Select(startIndex, endIndex - startIndex);
            RTBMain.SelectionBackColor= color;
            RTBMain.SelectionLength = 0;
            RTBMain.Refresh();
        }

        private void ResetTextBackColor(RichTextBox RTBToSet)
        {
            RTBToSet.Select(0, RTBToSet.TextLength);
            RTBToSet.SelectionBackColor = Globals.BackColorStd;
            RTBToSet.SelectionLength = 0;
            RTBToSet.Refresh();
        }

        private void CmboVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.Voice = cmboVoice.Text;
			Globals.User_Settings.TTS_Voice = cmboVoice.Text;
            txtVInfo.Text = "";
            foreach (InstalledVoice voice in Globals.Synth.GetInstalledVoices())
            {
                if (voice.VoiceInfo.Name == Globals.Voice)
                {
                    string description = voice.VoiceInfo.Description.Replace("Microsoft", "").Trim();
                    description = description.Replace("Desktop ", "");
                    txtVInfo.Text += description;
                    txtVInfo.Text += " " + voice.VoiceInfo.Culture + "\r\n";
                }
            }
        }

        private void ChkOmit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSkipBracketedText.Checked)
            {
                Globals.OmitTextInSqBrackets = true;
            }
            else
            {
                Globals.OmitTextInSqBrackets = false;
            }
        }

        private void SetVoiceAndVolume(SpeechSynthesizer synth)
        {
            synth.Rate = Globals.User_Settings.TTS_Speed;  // -10 through 10 will be speed,  Default -4
            synth.Volume = 100; // 0 to 100.  Leaving this static at 100  Who adjusts voice volume here?
            synth.SelectVoice(Globals.Voice); // Selects a voice by name
        }

        private void SpeedChanger_ValueChanged(object sender, EventArgs e)
        {
            //this.SpeedChanger.ValueChanged += new System.EventHandler(this.SpeedChanger_ValueChanged);
			int speed = Convert.ToInt32(this.SpeedChanger.Value);
            Globals.User_Settings.TTS_Speed = Convert.ToInt32(speed);
        }

		private void CenterChildFormInParentForm(Form form) {
            form.Owner = this;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - frmShortcuts.Width) / 2, this.Location.Y + (this.Height - frmShortcuts.Height) / 2);
        }

        private void ToolStripAncientGreekKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(1000,1000);
            frmShortcuts.RTBShortcuts.Rtf = Globals.AncientGreekShortcuts;
            // string originalRTF = frmShortcuts.RTBShortcuts.Rtf;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void toolStripEnglishMenuItem_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(900, 640);
            frmShortcuts.RTBShortcuts.Rtf = Globals.EnglishShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ToolStripFrenchKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(900, 640);
            frmShortcuts.RTBShortcuts.Rtf = Globals.FrenchShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ToolStripGermanKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            //string originalRTF = frmShortcuts.RTBShortcuts.Rtf;
            frmShortcuts.Size = new Size(920, 550);
            frmShortcuts.RTBShortcuts.Rtf = Globals.GermanShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ToolStripItalianKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(920, 500);
            frmShortcuts.RTBShortcuts.Rtf = Globals.ItalianShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ToolStripSpanishKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            // string spanish = frmShortcuts.RTBShortcuts.Rtf;
            frmShortcuts.Size = new Size(900, 850);
            frmShortcuts.RTBShortcuts.Rtf = Globals.SpanishShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ArabicKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(960, 640);
            frmShortcuts.RTBShortcuts.Rtf = Globals.ArabicShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void ToolStripRussianKeyboardShortcuts_Click(object sender, EventArgs e)
        {
            FrmShortcuts frmShortcuts = new FrmShortcuts();
            frmShortcuts.Size = new Size(960, 640);
            frmShortcuts.RTBShortcuts.Rtf = Globals.RussianShortcuts;
            frmShortcuts.RTBShortcuts.SelectAll();
            frmShortcuts.RTBShortcuts.SelectionBackColor = Globals.BackColorStd;
            frmShortcuts.RTBShortcuts.SelectionLength = 0;
            CenterChildFormInParentForm(frmShortcuts);
            frmShortcuts.Show();
        }

        private void Cedilla_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ç";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void Eszett_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ß";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void NTrema_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ñ";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void SpanishQuestionMark_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¿";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void OnScreenKeyboard_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\System32\osk.exe");
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            string folderPath = @Globals.Data_Folder;
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

        private void BtnBrackets_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "[]";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void SpanishExclamationMark_Click(object sender, EventArgs e)
        {
			Globals.Current_RTB_withFocus.SelectedText = "¡";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus.SelectionLength > 0)
            {
				int selStart = Globals.Current_RTB_withFocus.SelectionStart;
				int selLength = Globals.Current_RTB_withFocus.SelectionLength;
                Globals.Current_RTB_withFocus.BackColor = Globals.BackColorStd;
                Globals.Current_RTB_withFocus.SelectionColor = Color.Black;
				ResetTextBackColor(Globals.Current_RTB_withFocus);
            }
            else
            {
                MessageBox.Show("Text must be selected to revert back to the color black.", "Error", MessageBoxButtons.OK);
            }
        }

        private void BtnWhite_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus.SelectionLength > 0)
            {
                Globals.Current_RTB_withFocus.SelectionColor = Color.White;
            }
            else
            {
                MessageBox.Show("Text must be selected to revert back to the color black.", "Error", MessageBoxButtons.OK);
            }
        }

        private void BtnInsertSecMarker_Click(object sender, EventArgs e)
        {
			this.RTBMain.Focus();
            int currentPosition = this.RTBMain.SelectionStart;
            this.RTBMain.SelectedText = "§";
            this.RTBMain.SelectionStart = currentPosition + 1;
            this.RTBMain.Focus();
        }


    }
}
// UNDONE a context menu would be nice
