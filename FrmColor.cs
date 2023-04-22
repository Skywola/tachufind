using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

// ProcessCmdKey

namespace Tachufind
{
	public partial class FrmColor : Form
	{
		FrmMain frmMain;
		public FrmColor(FrmMain frmMain)
		{
			InitializeComponent();
			this.frmMain = frmMain;
			FrmGetSearches frxMain = new FrmGetSearches(this);

		}
		FrmSaved frmSaved = new FrmSaved();
		// InputLanguage cultureInfo = InputLanguage.CurrentInputLanguage;


		#region "Initialization"
		public static string[] pFrmColorFindArry = {
			SearchSettings.frmColorFind01_Txt, SearchSettings.frmColorFind02_Txt, SearchSettings.frmColorFind03_Txt,
			SearchSettings.frmColorFind04_Txt, SearchSettings.frmColorFind05_Txt, SearchSettings.frmColorFind06_Txt,
			SearchSettings.frmColorFind07_Txt, SearchSettings.frmColorFind08_Txt, SearchSettings.frmColorFind09_Txt,
			SearchSettings.frmColorFind10_Txt};

		// Used for frmColor, taken from the "signifies" textboxes,
		//  and used only for color search-edit mode
		public static string[] searchSettingsHintsArray = {
			SearchSettings.frmColorHints01_Txt, SearchSettings.frmColorHints02_Txt, SearchSettings.frmColorHints03_Txt,
			SearchSettings.frmColorHints04_Txt, SearchSettings.frmColorHints05_Txt, SearchSettings.frmColorHints06_Txt,
			SearchSettings.frmColorHints07_Txt, SearchSettings.frmColorHints08_Txt, SearchSettings.frmColorHints09_Txt,
			SearchSettings.frmColorHints10_Txt, };

		// Used for frmColor, taken from the "replace" textboxes,
		//  and used only for search-replace mode, (no color)
		public static string[] searchSettingsReplaceArray = {
			SearchSettings.frmColorReplace01_Txt, SearchSettings.frmColorReplace02_Txt, SearchSettings.frmColorReplace03_Txt, 
			SearchSettings.frmColorReplace04_Txt, SearchSettings.frmColorReplace05_Txt, SearchSettings.frmColorReplace06_Txt, 
			SearchSettings.frmColorReplace07_Txt, SearchSettings.frmColorReplace08_Txt, SearchSettings.frmColorReplace09_Txt,
			SearchSettings.frmColorReplace10_Txt, };



		// DFK =  Dat File Keys, they are all strings that are stored in files for persisting application settings
		FileIO FIO = new FileIO();
		//int EOF;

		// This is used only for find-replace (no colors)
		public static string[] strSearchArray = { "", "", "", "", "", "", "", "", "", "", "" };
		public static string[] strReplaceArray = { "", "", "", "", "", "", "", "", "", "", "" };
		// This is used only for find-replace (no colors)
		public static string[] strSearchColorArray = { "", "", "", "", "", "", "", "", "", "", "" };
		public static string[] srtToolTipArray = { "", "", "", "", "", "", "", "", "", "", "" };

		public bool inColorEditMode = true;
		//public Queue<searchInfo> queueSearchReplace = new Queue<searchInfo>();
		private const bool save = true;
		private const bool retreive = false;
		private bool formLoading = true;   // This IS used, despite the warning

        KeyboardShortcuts keyboardShortcuts = new KeyboardShortcuts();


        #endregion  // "Initialization"


        private void FrmColor_Load(object sender, EventArgs e)
		{
			this.KeyPreview = true;
            Color backColor = Color.FromArgb(200, 190, 200);
			this.btnFind.BackColor = backColor;
			this.btnReplace.BackColor = backColor;
			this.btnReplace_All.BackColor = backColor;
			this.btnSuffixReplace.BackColor = backColor;
			this.btnPrefixReplace.BackColor = backColor;
			this.btnUndo.BackColor = backColor;
			this.btnGetSearch.BackColor = backColor;
			this.btnSaveSearch.BackColor = backColor;
			this.btnClearAll.BackColor = backColor;
			this.btnClose.BackColor = backColor;
			this.btnPasteInto.BackColor = backColor;
			this.btnClearFinds.BackColor = backColor;
			this.btnClearReplace.BackColor = backColor;
			this.btnClear.BackColor = backColor;
			this.BtnImportSearch.BackColor = backColor;
			this.BtnExportSearch.BackColor = backColor;

            this.chkMatchCase.TabStop = false;
			this.chkWordOnly.TabStop = false;
			this.chkReverse.TabStop = false;
			this.txtSearchTime.TabStop = false;
			this.rbEditColor.TabStop = false;

			lblReplace01.Text = "Signifies ";
			lblReplace02.Text = "Signifies ";
			lblReplace03.Text = "Signifies ";
			lblReplace04.Text = "Signifies ";
			lblReplace05.Text = "Signifies ";
			lblReplace06.Text = "Signifies ";
			lblReplace07.Text = "Signifies ";
			lblReplace08.Text = "Signifies ";
			lblReplace09.Text = "Signifies ";
			lblReplace10.Text = "Signifies ";

            chkMatchCase.Checked = false;
			chkWordOnly.Checked = false;
			chkReverse.Checked = false;
			Globals.Current_RTB_withFocus = rtfFind01;
			SearchFns search = new SearchFns();

			Cursor.Current = Cursors.WaitCursor;
			GetFrmColorValues();
            this.Left = Globals.User_Settings.FrmColorLocation.X;
            this.Top = Globals.User_Settings.FrmColorLocation.Y;

            if (string.IsNullOrEmpty(Globals.User_Settings.FilePath))
			{
				//rbEditColor.Checked = true;
				this.rtfSearchName.Text = "";
				return;
			}
			formLoading = true;
			this.BringToFront();
			if (Globals.P_RTFMain_RTF.Length > 0)
			{  //  Allows one undo after a search
				Globals.P_FrmMainTextUndo = Globals.P_RTFMain_RTF;
			}
			Cursor.Current = Cursors.Default;
            rbEditColor.Checked = false;
            rbMultipleReplace.Checked = true;
            this.txtSearchTime.Text = Globals.User_Settings.searchTimeLimit.ToString();
            rtfFind01.Select();
		}

		public void GetFrmColorValues()
		{
			this.rtfFind01.Text = SearchSettings.frmColorFind01_Txt;
			this.rtfFind02.Text = SearchSettings.frmColorFind02_Txt;
			this.rtfFind03.Text = SearchSettings.frmColorFind03_Txt;
			this.rtfFind04.Text = SearchSettings.frmColorFind04_Txt;
			this.rtfFind05.Text = SearchSettings.frmColorFind05_Txt;
			this.rtfFind06.Text = SearchSettings.frmColorFind06_Txt;
			this.rtfFind07.Text = SearchSettings.frmColorFind07_Txt;
			this.rtfFind08.Text = SearchSettings.frmColorFind08_Txt;
			this.rtfFind09.Text = SearchSettings.frmColorFind09_Txt;
			this.rtfFind10.Text = SearchSettings.frmColorFind10_Txt;

			this.rtfReplace01.Text = SearchSettings.frmColorReplace01_Txt;
			this.rtfReplace02.Text = SearchSettings.frmColorReplace02_Txt;
			this.rtfReplace03.Text = SearchSettings.frmColorReplace03_Txt;
			this.rtfReplace04.Text = SearchSettings.frmColorReplace04_Txt;
			this.rtfReplace05.Text = SearchSettings.frmColorReplace05_Txt;
			this.rtfReplace06.Text = SearchSettings.frmColorReplace06_Txt;
			this.rtfReplace07.Text = SearchSettings.frmColorReplace07_Txt;
			this.rtfReplace08.Text = SearchSettings.frmColorReplace08_Txt;
			this.rtfReplace09.Text = SearchSettings.frmColorReplace09_Txt;
			this.rtfReplace10.Text = SearchSettings.frmColorReplace10_Txt;

			// LOCATION Set color values for FrmColor all in one shot
			// FrmMain.colorIndex[1] sets access to colors, based on their location in the array
			// This access is meant to be static on this side, color SHIFTS will be done in FrmMain
			// simply by changing the numbers in colorIndex
			this.FrmColorBtnC01.BackColor = this.rtfFind01.ForeColor = Globals.User_Settings.Color01; 
            this.FrmColorBtnC02.BackColor = this.rtfFind02.ForeColor = Globals.User_Settings.Color02;
			this.FrmColorBtnC03.BackColor = this.rtfFind03.ForeColor = Globals.User_Settings.Color03;
			this.FrmColorBtnC04.BackColor = this.rtfFind04.ForeColor = Globals.User_Settings.Color04;
			this.FrmColorBtnC05.BackColor = this.rtfFind05.ForeColor = Globals.User_Settings.Color05;
																												 
            this.FrmColorBtnC06.BackColor = this.rtfFind06.ForeColor = Globals.User_Settings.Color06;
			this.FrmColorBtnC07.BackColor = this.rtfFind07.ForeColor = Globals.User_Settings.Color07;
			this.FrmColorBtnC08.BackColor = this.rtfFind08.ForeColor = Globals.User_Settings.Color08;
			this.FrmColorBtnC09.BackColor = this.rtfFind09.ForeColor = Globals.User_Settings.Color09;
			this.FrmColorBtnC10.BackColor = this.rtfFind10.ForeColor = Globals.User_Settings.Color10;

            this.rbEditColor.Checked = SearchSettings.rbEditColor; //Reference
			this.rbMultipleReplace.Checked = !SearchSettings.rbEditColor; //Negated
			this.chkAutoFindNext.Checked = SearchSettings.chkAutoFindNext;
			this.chkMatchCase.Checked = SearchSettings.chkMatchCase;
			this.chkWordOnly.Checked = SearchSettings.chkWordOnly;
			this.chkReverse.Checked = SearchSettings.chkReverse;
		}


		public void GetRtfFindForeColor() {
			// LOCATION Set color values for FrmColor all in one shot
			// This access is meant to be static on this side, color SHIFTS will be done in FrmMain
			// simply by changing the numbers in colorIndex
			this.FrmColorBtnC01.BackColor = this.rtfFind01.ForeColor = Globals.User_Settings.Color01;
			this.FrmColorBtnC02.BackColor = this.rtfFind02.ForeColor = Globals.User_Settings.Color02;
			this.FrmColorBtnC03.BackColor = this.rtfFind03.ForeColor = Globals.User_Settings.Color03;
			this.FrmColorBtnC04.BackColor = this.rtfFind04.ForeColor = Globals.User_Settings.Color04;
			this.FrmColorBtnC05.BackColor = this.rtfFind05.ForeColor = Globals.User_Settings.Color05;
			this.FrmColorBtnC06.BackColor = this.rtfFind06.ForeColor = Globals.User_Settings.Color06;
			this.FrmColorBtnC07.BackColor = this.rtfFind07.ForeColor = Globals.User_Settings.Color07;
			this.FrmColorBtnC08.BackColor = this.rtfFind08.ForeColor = Globals.User_Settings.Color08;
			this.FrmColorBtnC09.BackColor = this.rtfFind09.ForeColor = Globals.User_Settings.Color09;
			this.FrmColorBtnC10.BackColor = this.rtfFind10.ForeColor = Globals.User_Settings.Color10;
            this.rtfFind01.SelectAll();
			this.rtfFind01.SelectionColor = Globals.User_Settings.Color01;
            this.rtfFind02.SelectAll();
			this.rtfFind02.SelectionColor = Globals.User_Settings.Color02;
            this.rtfFind03.SelectAll();
			this.rtfFind03.SelectionColor = Globals.User_Settings.Color03;
            this.rtfFind04.SelectAll();
			this.rtfFind04.SelectionColor = Globals.User_Settings.Color04;
            this.rtfFind05.SelectAll();
			this.rtfFind05.SelectionColor = Globals.User_Settings.Color05;
            this.rtfFind06.SelectAll();
			this.rtfFind06.SelectionColor = Globals.User_Settings.Color06;
            this.rtfFind07.SelectAll();
			this.rtfFind07.SelectionColor = Globals.User_Settings.Color07;
            this.rtfFind08.SelectAll();
			this.rtfFind08.SelectionColor = Globals.User_Settings.Color08;
            this.rtfFind09.SelectAll();
			this.rtfFind09.SelectionColor = Globals.User_Settings.Color09;
            this.rtfFind10.SelectAll();
			this.rtfFind10.SelectionColor = Globals.User_Settings.Color10;
        }

		public void SetRtfFindsBackColorToNormal()
		{
			this.rtfFind01.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind02.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind03.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind04.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind05.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind06.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind07.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind08.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind09.BackColor = Color.FromArgb(230, 230, 230);
			this.rtfFind10.BackColor = Color.FromArgb(230, 230, 230);
		}

		private void BtnFind_Click(object sender, EventArgs e)
		{
			this.Refresh();
			// NOTE - this function is in FrmMain, not here
			frmMain.FrmColorBtnFindClick();
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			this.Refresh();
			frmMain.FrmColorBtnReplaceClick();
			if (chkAutoFindNext.Checked) {
				this.btnFind.PerformClick();
			}
		}

		private void BtnGetSearch_Click(object sender, EventArgs e)
		{
			try
			{
                FrmGetSearches frmGetSearches = new FrmGetSearches(this);
                List<string> FindReplaceTitlesList = new List<string>();

                frmGetSearches.listOfSearches.Items.Clear();
                FindReplaceTitlesList = GeneralFns.GetListOfFilesInDirectory(Globals.Data_Folder, Globals.FindReplaceFileExtension);
                if (FindReplaceTitlesList.Count < 1)
                {
                    MessageBox.Show("No searches currently exist.", "No Searches", MessageBoxButtons.OK);
                    return;
                }
                foreach (string item in FindReplaceTitlesList)
                {
                    if (!frmGetSearches.listOfSearches.Items.Contains(item))
                    {
                        frmGetSearches.listOfSearches.Items.Add(item);
                    }
                }

                frmGetSearches.Refresh();
                frmGetSearches.Show();
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

        }

		private void BtnSaveSearch_Click(object sender, EventArgs e)
		{

			frmSaved.Show();
			SearchSettings.SearchName = rtfSearchName.Text;
			SearchSettings.rbEditColor = rbEditColor.Checked;
			SearchSettings.chkAutoFindNext = chkAutoFindNext.Checked;
			SearchSettings.chkMatchCase = chkMatchCase.Checked;
			SearchSettings.chkWordOnly = chkWordOnly.Checked;
			SearchSettings.chkReverse = chkReverse.Checked;

			SearchSettings.SearchName = rtfSearchName.Text;
			SearchSettings.frmColorFind01_Txt = this.rtfFind01.Text;
			SearchSettings.frmColorFind02_Txt = this.rtfFind02.Text;
			SearchSettings.frmColorFind03_Txt = this.rtfFind03.Text;
			SearchSettings.frmColorFind04_Txt = this.rtfFind04.Text;
			SearchSettings.frmColorFind05_Txt = this.rtfFind05.Text;
			SearchSettings.frmColorFind06_Txt = this.rtfFind06.Text;
			SearchSettings.frmColorFind07_Txt = this.rtfFind07.Text;
			SearchSettings.frmColorFind08_Txt = this.rtfFind08.Text;
			SearchSettings.frmColorFind09_Txt = this.rtfFind09.Text;
			SearchSettings.frmColorFind10_Txt = this.rtfFind10.Text;
			SearchSettings.frmColorReplace01_Txt = this.rtfReplace01.Text;
			SearchSettings.frmColorReplace02_Txt = this.rtfReplace02.Text;
			SearchSettings.frmColorReplace03_Txt = this.rtfReplace03.Text;
			SearchSettings.frmColorReplace04_Txt = this.rtfReplace04.Text;
			SearchSettings.frmColorReplace05_Txt = this.rtfReplace05.Text;
			SearchSettings.frmColorReplace06_Txt = this.rtfReplace06.Text;
			SearchSettings.frmColorReplace07_Txt = this.rtfReplace07.Text;
			SearchSettings.frmColorReplace08_Txt = this.rtfReplace08.Text;
			SearchSettings.frmColorReplace09_Txt = this.rtfReplace09.Text;
			SearchSettings.frmColorReplace10_Txt = this.rtfReplace10.Text;
			SearchSettings.rbEditColor = this.rbEditColor.Checked;
			SearchSettings.chkAutoFindNext = this.chkAutoFindNext.Checked;
			SearchSettings.chkMatchCase = this.chkMatchCase.Checked;
			SearchSettings.chkWordOnly = this.chkWordOnly.Checked;
			SearchSettings.chkReverse = this.chkReverse.Checked;

			SaveSearch();
			frmSaved.Hide();
		}

		void SaveSearch()
		{
			try
			{
				if (rtfSearchName.Text.Length < 1)
				{
					MessageBox.Show("Search name must be entered in order to save it.", "Error Detected", MessageBoxButtons.OK);
					return;
				}
				SearchSettings.SearchName = rtfSearchName.Text;
				// Get Title in .fdta file
				if (File.Exists(Globals.Data_Folder + rtfSearchName.Text + Globals.FindReplaceFileExtension))
				{
					int answer = (int)MessageBox.Show("This search exists, overwrite?", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (answer == (int)System.Windows.Forms.DialogResult.No)
					{
						return;
					}
				}
				BuildFindReplace();


			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private String ColorToString(Color inputColor)    
		{
			string Alpha = inputColor.A.ToString();
			string Red = inputColor.R.ToString();
			string Green = inputColor.G.ToString();
			string Blue = inputColor.B.ToString();
			return Alpha + "," + Red + "," + Green + "," + Blue;
		}

		public void BuildFindReplace()
		{
			string fileContents = "";
			Queue<string> searchItemsQueue = new Queue<string>(); // Holds a single search settings

			try
			{
				// Colors
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color00));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color01));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color02));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color03));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color04));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color05));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color06));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color07));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color08));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color09));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color10));
				searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color11));
				// End Colors

				searchItemsQueue.Enqueue(SearchSettings.rbEditColor.ToString());
				searchItemsQueue.Enqueue(SearchSettings.chkAutoFindNext.ToString());
				searchItemsQueue.Enqueue(SearchSettings.chkMatchCase.ToString());
				searchItemsQueue.Enqueue(SearchSettings.chkWordOnly.ToString());
				searchItemsQueue.Enqueue(SearchSettings.chkReverse.ToString()); // 5

				searchItemsQueue.Enqueue(SearchSettings.frmColorFind01_Txt); // 6
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace01_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind02_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace02_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind03_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace03_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind04_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace04_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind05_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace05_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind06_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace06_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind07_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace07_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind08_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace08_Txt); // 27
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind09_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace09_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorFind10_Txt);
				searchItemsQueue.Enqueue(SearchSettings.frmColorReplace10_Txt);

				// unload all settings into one resulting record.
				while (searchItemsQueue.Count > 0)
				{
					fileContents = fileContents + searchItemsQueue.Dequeue() + Environment.NewLine;
				}

				string filePath = Globals.Data_Folder + rtfSearchName.Text + Globals.FindReplaceFileExtension;
				FIO.CreateDirectoryIfItDoesNotExist(filePath);
				FIO.WriteFile(filePath, fileContents);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}



		private void BtnC01_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				Color selectedColor = colorDialog1.Color;
				Globals.User_Settings.Color01 = selectedColor;
				FrmColorBtnC01.BackColor = frmMain.FrmMainColorBtn01.BackColor = selectedColor;
				rtfFind01.SelectAll();
				rtfFind01.ForeColor = selectedColor;
				rtfFind01.SelectionLength = 0;
				Globals.BoolFrmColorSearchSaved = false;
			}
        }

		private void BtnC02_Click(object sender, EventArgs e)
		{
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color02 = selectedColor;
                FrmColorBtnC02.BackColor = frmMain.FrmMainColorBtn02.BackColor = selectedColor;
                rtfFind02.SelectAll();
                rtfFind02.ForeColor = selectedColor;
                rtfFind02.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC03_Click(object sender, EventArgs e)
		{
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color03 = selectedColor;
                FrmColorBtnC03.BackColor = frmMain.FrmMainColorBtn03.BackColor = selectedColor;
                rtfFind03.SelectAll();
                rtfFind03.ForeColor = selectedColor;
                rtfFind03.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC04_Click(object sender, EventArgs e)
		{
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color04 = selectedColor;
                FrmColorBtnC04.BackColor = frmMain.FrmMainColorBtn04.BackColor = selectedColor;
                rtfFind04.SelectAll();
                rtfFind04.ForeColor = selectedColor;
                rtfFind04.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC05_Click(object sender, EventArgs e)
		{
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color05 = selectedColor;
                FrmColorBtnC05.BackColor = frmMain.FrmMainColorBtn05.BackColor = selectedColor;
                rtfFind05.SelectAll();
                rtfFind05.ForeColor = selectedColor;
                rtfFind05.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC06_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color06 = selectedColor;
                FrmColorBtnC06.BackColor = frmMain.FrmMainColorBtn06.BackColor = selectedColor;
                rtfFind06.SelectAll();
                rtfFind06.ForeColor = selectedColor;
                rtfFind06.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC07_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color07 = selectedColor;
                FrmColorBtnC07.BackColor = frmMain.FrmMainColorBtn07.BackColor = selectedColor;
                rtfFind07.SelectAll();
                rtfFind07.ForeColor = selectedColor;
                rtfFind07.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC08_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color08 = selectedColor;
                FrmColorBtnC08.BackColor = frmMain.FrmMainColorBtn08.BackColor = selectedColor;
                rtfFind08.SelectAll();
                rtfFind08.ForeColor = selectedColor;
                rtfFind08.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC09_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color09 = selectedColor;
                FrmColorBtnC09.BackColor = frmMain.FrmMainColorBtn09.BackColor = selectedColor;
                rtfFind09.SelectAll();
                rtfFind09.ForeColor = selectedColor;
                rtfFind09.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }

		private void BtnC10_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
                Color selectedColor = colorDialog1.Color;
                Globals.User_Settings.Color10 = selectedColor;
                FrmColorBtnC10.BackColor = frmMain.FrmMainColorBtn10.BackColor = selectedColor;
                rtfFind10.SelectAll();
                rtfFind10.ForeColor = selectedColor;
                rtfFind10.SelectionLength = 0;
                Globals.BoolFrmColorSearchSaved = false;
            }
        }


		private void RtfReplace01_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace01;
		}

		private void RtfReplace02_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace02;
		}

		private void RtfReplace03_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace03;
		}

		private void RtfReplace04_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace04;
		}

		private void RtfReplace05_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace05;
		}

		private void RtfReplace06_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace06;
		}

		private void RtfReplace07_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace07;
		}

		private void RtfReplace08_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace08;
		}

		private void RtfReplace09_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace09;
		}

		private void RtfReplace10_Click(object sender, EventArgs e)
		{
			//setRtfReplacesBackColorToNormal();
			Globals.Current_RTB_withFocus = rtfReplace10;
		}

		private void RtfFind01_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind01;
		}

		private void RtfFind02_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind02;
		}

		private void RtfFind03_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind03;
		}

		private void RtfFind04_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind04;
		}

		private void RtfFind05_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind05;
		}

		private void RtfFind06_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind06;
		}

		private void RtfFind07_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind07;
		}

		private void RtfFind08_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind08;
		}

		private void RtfFind09_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind09;
		}

		private void RtfFind10_Click(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind10;
		}

		private void RtfFind01_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind01;
			rtfFind01.SelectAll();
		}

		private void RtfFind02_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind02;
			rtfFind02.SelectAll();
		}

		private void RtfFind03_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind03;
			rtfFind03.SelectAll();
		}

		private void RtfFind04_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind04;
			rtfFind04.SelectAll();
		}

		private void RtfFind05_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind05;
			rtfFind05.SelectAll();
		}

		private void RtfFind06_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind06;
			rtfFind06.SelectAll();
		}

		private void RtfFind07_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind07;
			rtfFind07.SelectAll();
		}

		private void RtfFind08_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind08;
			rtfFind08.SelectAll();
		}

		private void RtfFind09_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind09;
			rtfFind09.SelectAll();
		}

		private void RtfFind10_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfFind10;
			rtfFind10.SelectAll();
		}

		private void RtfReplace01_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace01;
			rtfReplace01.SelectAll();
		}

		private void RtfReplace02_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace02;
			rtfReplace02.SelectAll();
		}

		private void RtfReplace03_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace03;
			rtfReplace03.SelectAll();
		}

		private void RtfReplace04_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace04;
			rtfReplace04.SelectAll();
		}

		private void RtfReplace05_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace05;
			rtfReplace05.SelectAll();
		}

		private void RtfReplace06_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace06;
			rtfReplace06.SelectAll();
		}

		private void RtfReplace07_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace07;
			rtfReplace07.SelectAll();
		}

		private void RtfReplace08_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace08;
			rtfReplace08.SelectAll();
		}

		private void RtfReplace09_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace09;
			rtfReplace09.SelectAll();
		}

		private void RtfReplace10_DoubleClick(object sender, EventArgs e)
		{
			Globals.Current_RTB_withFocus = rtfReplace10;
			rtfReplace10.SelectAll();
		}

		
		private void RbMultipleReplace_CheckedChanged(object sender, EventArgs e)
		{
			// This little section may or may not be necessary for this proceedure,
			// two identically named proceedures were listed, this part is one of them
			SetRtfFindsBackColorToNormal();
			Globals.SearchChanged = true;

			if (rbEditColor.Checked)
			{ // COLOR REPLACE
				rbMultipleReplace.Checked = false;
				Color backColor = Color.FromArgb(200, 190, 200);  //  200, 190, 200
				this.btnFind.BackColor = backColor;
				this.btnReplace.BackColor = backColor;
				this.btnReplace_All.BackColor = backColor;
				this.btnClearFinds.BackColor = backColor;
				this.btnSaveSearch.BackColor = backColor;
				this.btnGetSearch.BackColor = backColor;
				this.btnClose.BackColor = backColor;
				this.btnClearReplace.BackColor = backColor;
				this.btnUndo.BackColor = backColor;
				this.btnClearAll.BackColor = backColor;
				this.btnClose.BackColor = backColor;
				this.btnPasteInto.BackColor = backColor;
				this.btnClear.BackColor = backColor;
				this.btnSuffixReplace.BackColor = backColor;
				this.btnPrefixReplace.BackColor = backColor;
				rtfFind01.ForeColor = Globals.User_Settings.Color01;
				rtfFind02.ForeColor = Globals.User_Settings.Color02;
				rtfFind03.ForeColor = Globals.User_Settings.Color03;
				rtfFind04.ForeColor = Globals.User_Settings.Color04;
				rtfFind05.ForeColor = Globals.User_Settings.Color05;
				rtfFind06.ForeColor = Globals.User_Settings.Color06;
				rtfFind07.ForeColor = Globals.User_Settings.Color07;
				rtfFind08.ForeColor = Globals.User_Settings.Color08;
				rtfFind09.ForeColor = Globals.User_Settings.Color09;
				rtfFind10.ForeColor = Globals.User_Settings.Color10;
                lblReplace01.Text = "Note:";
				lblReplace02.Text = "Note:";
				lblReplace03.Text = "Note:";
				lblReplace04.Text = "Note:";
				lblReplace05.Text = "Note:";
				lblReplace06.Text = "Note:";
				lblReplace07.Text = "Note:";
				lblReplace08.Text = "Note:";
				lblReplace09.Text = "Note:";
				lblReplace10.Text = "Note:";
			}
			else
			{   // TEXT REPLACE
				rbEditColor.Checked = false;
				Color backColor = Color.FromArgb(180, 180, 180);
				this.btnFind.BackColor = backColor;
				this.btnReplace.BackColor = backColor;
				this.btnReplace_All.BackColor = backColor;
				this.btnClearFinds.BackColor = backColor;
				this.btnSaveSearch.BackColor = backColor;
				this.btnGetSearch.BackColor = backColor;
				this.btnClose.BackColor = backColor;
				this.btnClearReplace.BackColor = backColor;
				this.btnUndo.BackColor = backColor;
				this.btnClearAll.BackColor = backColor;
				this.btnClose.BackColor = backColor;
				this.btnPasteInto.BackColor = backColor;
				this.btnClear.BackColor = backColor;
				this.btnSuffixReplace.BackColor = backColor;
				this.btnPrefixReplace.BackColor = backColor;
				rtfFind01.ForeColor = Color.Black;
				rtfReplace01.ForeColor = Color.Black;
				rtfFind02.ForeColor = Color.Black;
				rtfReplace02.ForeColor = Color.Black;
				rtfFind03.ForeColor = Color.Black;
				rtfReplace03.ForeColor = Color.Black;
				rtfFind04.ForeColor = Color.Black;
				rtfReplace04.ForeColor = Color.Black;
				rtfFind05.ForeColor = Color.Black;
				rtfReplace05.ForeColor = Color.Black;
				rtfFind06.ForeColor = Color.Black;
				rtfReplace06.ForeColor = Color.Black;
				rtfFind07.ForeColor = Color.Black;
				rtfReplace07.ForeColor = Color.Black;
				rtfFind08.ForeColor = Color.Black;
				rtfReplace08.ForeColor = Color.Black;
				rtfFind09.ForeColor = Color.Black;
				rtfReplace09.ForeColor = Color.Black;
				rtfFind10.ForeColor = Color.Black;
				rtfReplace10.ForeColor = Color.Black;
				lblReplace01.Text = "Replace";
				lblReplace02.Text = "Replace";
				lblReplace03.Text = "Replace";
				lblReplace04.Text = "Replace";
				lblReplace05.Text = "Replace";
				lblReplace06.Text = "Replace";
				lblReplace07.Text = "Replace";
				lblReplace08.Text = "Replace";
				lblReplace09.Text = "Replace";
				lblReplace10.Text = "Replace";
			}
			this.BackColor = Color.FromArgb(200, 200, 200);

		}


		private void RtfFind01_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind01.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind01;
		}

		private void RtfFind02_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind02.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind02;
		}

		private void RtfFind03_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind03.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind03;
		}

		private void RtfFind04_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind04.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind04;
		}

		private void RtfFind05_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind05.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind05;
		}

		private void RtfFind06_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind06.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind06;
		}

		private void RtfFind07_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind07.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind07;
		}

		private void RtfFind08_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind08.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind08;
		}

		private void RtfFind09_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind09.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind09;
		}

		private void RtfFind10_MouseDown(object sender, MouseEventArgs e)
		{
			rtfFind10.Focus();
			Globals.Current_RTB_withFocus = this.rtfFind10;
		}

		private void RtfReplace01_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace01.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace01;
		}

		private void RtfReplace02_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace02.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace02;
		}

		private void RtfReplace03_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace03.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace03;
		}

		private void RtfReplace04_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace04.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace04;
		}

		private void RtfReplace05_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace05.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace05;
		}

		private void RtfReplace06_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace06.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace06;
		}

		private void RtfReplace07_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace07.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace07;
		}

		private void RtfReplace08_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace08.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace08;
		}

		private void RtfReplace09_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace09.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace09;
		}

		private void RtfReplace10_MouseDown(object sender, MouseEventArgs e)
		{
			rtfReplace10.Focus();
			Globals.Current_RTB_withFocus = this.rtfReplace10;
		}

		private void RbEditColor_CheckedChanged(object sender, EventArgs e)
		{
			BackColor = Color.FromArgb(200, 190, 200);
		}


		private void BtnClear_Click(object sender, EventArgs e)
		{
			rtfSearchName.Text = "";
		}


		private void RtfFind01_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace01;
			}
		}

		private void RtfFind02_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace02;
			}
		}

		private void RtfFind03_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace03;
			}
		}

		private void RtfFind04_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace04;
			}
		}

		private void RtfFind05_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace05;
			}
		}

		private void RtfFind06_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace06;
			}
		}

		private void RtfFind07_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace07;
			}
		}

		private void RtfFind08_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace08;
			}
		}

		private void RtfFind09_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace09;
			}
		}

		private void RtfFind10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfReplace10;
			}
		}


		private void RtfReplace01_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind02;
			}

		}

		private void RtfReplace02_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind03;
			}
		}

		private void RtfReplace03_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind04;
			}
		}

		private void RtfReplace04_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind05;
			}
		}

		private void RtfReplace05_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind06;
			}
		}

		private void RtfReplace06_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind07;
			}
		}

		private void RtfReplace07_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind08;
			}
		}

		private void RtfReplace08_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind09;
			}
		}

		private void RtfReplace09_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind10;
			}
		}

		private void RtfReplace10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Tab)
			{
				Globals.Current_RTB_withFocus = rtfFind01;
			}
		}

		private void ChkMatchCase_Click(object sender, EventArgs e)
		{
			SetRtfFindsBackColorToNormal();
			Globals.SearchChanged = true;
			SearchSettings.chkMatchCase = chkMatchCase.Checked;
		}

		private void LblPipe01_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind01.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind01.Text = this.rtfFind01.Text.Insert(cursorloct, "|");
				this.rtfFind01.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe02_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind02.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind02.Text = this.rtfFind02.Text.Insert(cursorloct, "|");
				this.rtfFind02.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe03_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind03.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind03.Text = this.rtfFind03.Text.Insert(cursorloct, "|");
				this.rtfFind03.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe04_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind04.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind04.Text = this.rtfFind04.Text.Insert(cursorloct, "|");
				this.rtfFind04.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe05_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind05.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind05.Text = this.rtfFind05.Text.Insert(cursorloct, "|");
				this.rtfFind05.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe06_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind06.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind06.Text = this.rtfFind06.Text.Insert(cursorloct, "|");
				this.rtfFind06.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe07_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind07.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind07.Text = this.rtfFind07.Text.Insert(cursorloct, "|");
				this.rtfFind07.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LlblPipe08_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind08.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind08.Text = this.rtfFind08.Text.Insert(cursorloct, "|");
				this.rtfFind08.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe09_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind09.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind09.Text = this.rtfFind09.Text.Insert(cursorloct, "|");
				this.rtfFind09.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void LblPipe10_Click(object sender, EventArgs e)
		{
			int cursorloct = this.rtfFind10.Text.Length;
			if (cursorloct > 0)
			{
				this.rtfFind10.Text = this.rtfFind10.Text.Insert(cursorloct, "|");
				this.rtfFind10.SelectionStart = cursorloct + 1;
			}
			else
			{
				MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
				return;
			}
		}

		private void BtnClearAll_Click(object sender, EventArgs e)
		{
			// return all textboxes to grey
			this.rtfFind01.BackColor = Color.LightGray;
			this.rtfFind02.BackColor = Color.LightGray;
			this.rtfFind03.BackColor = Color.LightGray;
			this.rtfFind04.BackColor = Color.LightGray;
			this.rtfFind05.BackColor = Color.LightGray;
			this.rtfFind06.BackColor = Color.LightGray;
			this.rtfFind07.BackColor = Color.LightGray;
			this.rtfFind08.BackColor = Color.LightGray;
			this.rtfFind09.BackColor = Color.LightGray;
			this.rtfFind10.BackColor = Color.LightGray;

			this.rtfReplace01.BackColor = Color.LightGray;
			this.rtfReplace02.BackColor = Color.LightGray;
			this.rtfReplace03.BackColor = Color.LightGray;
			this.rtfReplace04.BackColor = Color.LightGray;
			this.rtfReplace05.BackColor = Color.LightGray;
			this.rtfReplace06.BackColor = Color.LightGray;
			this.rtfReplace07.BackColor = Color.LightGray;
			this.rtfReplace08.BackColor = Color.LightGray;
			this.rtfReplace09.BackColor = Color.LightGray;
			this.rtfReplace10.BackColor = Color.LightGray;


			this.rtfFind01.Text = "";
			this.rtfFind02.Text = "";
			this.rtfFind03.Text = "";
			this.rtfFind04.Text = "";
			this.rtfFind05.Text = "";
			this.rtfFind06.Text = "";
			this.rtfFind07.Text = "";
			this.rtfFind08.Text = "";
			this.rtfFind09.Text = "";
			this.rtfFind10.Text = "";

			this.rtfReplace01.Text = "";
			this.rtfReplace02.Text = "";
			this.rtfReplace03.Text = "";
			this.rtfReplace04.Text = "";
			this.rtfReplace05.Text = "";
			this.rtfReplace06.Text = "";
			this.rtfReplace07.Text = "";
			this.rtfReplace08.Text = "";
			this.rtfReplace09.Text = "";
			this.rtfReplace10.Text = "";
		}

		private void BtnClearFinds_Click(object sender, EventArgs e)
		{
			// return all textboxes to grey
			this.rtfFind01.BackColor = Color.LightGray;
			this.rtfFind02.BackColor = Color.LightGray;
			this.rtfFind03.BackColor = Color.LightGray;
			this.rtfFind04.BackColor = Color.LightGray;
			this.rtfFind05.BackColor = Color.LightGray;
			this.rtfFind06.BackColor = Color.LightGray;
			this.rtfFind07.BackColor = Color.LightGray;
			this.rtfFind08.BackColor = Color.LightGray;
			this.rtfFind09.BackColor = Color.LightGray;
			this.rtfFind10.BackColor = Color.LightGray;

			this.rtfFind01.Text = "";
			this.rtfFind02.Text = "";
			this.rtfFind03.Text = "";
			this.rtfFind04.Text = "";
			this.rtfFind05.Text = "";
			this.rtfFind06.Text = "";
			this.rtfFind07.Text = "";
			this.rtfFind08.Text = "";
			this.rtfFind09.Text = "";
			this.rtfFind10.Text = "";
		}

		private void BtnClearReplace_Click(object sender, EventArgs e)
		{
			this.rtfReplace01.BackColor = Color.LightGray;
			this.rtfReplace02.BackColor = Color.LightGray;
			this.rtfReplace03.BackColor = Color.LightGray;
			this.rtfReplace04.BackColor = Color.LightGray;
			this.rtfReplace05.BackColor = Color.LightGray;
			this.rtfReplace06.BackColor = Color.LightGray;
			this.rtfReplace07.BackColor = Color.LightGray;
			this.rtfReplace08.BackColor = Color.LightGray;
			this.rtfReplace09.BackColor = Color.LightGray;
			this.rtfReplace10.BackColor = Color.LightGray;

			this.rtfReplace01.Text = "";
			this.rtfReplace02.Text = "";
			this.rtfReplace03.Text = "";
			this.rtfReplace04.Text = "";
			this.rtfReplace05.Text = "";
			this.rtfReplace06.Text = "";
			this.rtfReplace07.Text = "";
			this.rtfReplace08.Text = "";
			this.rtfReplace09.Text = "";
			this.rtfReplace10.Text = "";

		}

		private void ChkAutoFindNext_Click(object sender, EventArgs e)
		{
			SearchSettings.chkAutoFindNext = chkAutoFindNext.Checked;
		}

		private void FrmColor_MouseDown(object sender, MouseEventArgs e)
		{
			formLoading = false;
		}

		/// <summary>
		/// Append formatted text to a Rich Text Box control 
		/// </summary>
		/// <param name="rtb">Rich Text Box to which horizontal bar is to be added</param>
		/// <param name="text">Text to be appended to Rich Text Box</param>
		private void AppendFormattedText(RichTextBox rtb, string text)
		{
			int start = rtb.TextLength;
			rtb.AppendText(text);
			int end = rtb.TextLength; // now longer by length of appended text

			// Select text that was appended
			rtb.Select(start, end - start);

			#region Apply Formatting

			rtb.SelectionColor = Color.Black;

			//rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Regular);
			rtb.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
			#endregion

			// Unselect text
			rtb.SelectionLength = 0;
		}

		private string DrawAnArrowOnlyIfTextBoxHasContent(int n, int rlen, int max) {
            string arrow = "";
			if (n > 0 & rlen > 0)
            { // draw arrow only if both textboxes have content
                string point = "--» ";
                if (n == max) { arrow = point; } // arrow = point if findtxt is already a max length
                while (n != max)
                {            // max is the longest string of every textbox 0 - 7
                    arrow = "-" + point;    // make all separation distances similar by adjusting arrow length
                    n = n + 1;
                }
                arrow = " " + arrow; // add a space in front of arrow beginning
				return arrow;
            }
			return "";
        }


		private void BtnPasteInto_Click(object sender, EventArgs e)
		{
			int i = 0;
			int max = 0;
			RichTextBox RTB = new RichTextBox();
			var rtf = new RichTextBox();

            try
            {
                // get the length of the longest string in rtfFind
                int[] maxLengthF = new int[] { rtfFind01.Text.Length, rtfFind02.Text.Length, rtfFind03.Text.Length,
                                        rtfFind04.Text.Length, rtfFind05.Text.Length, rtfFind06.Text.Length,
                                        rtfFind07.Text.Length, rtfFind08.Text.Length, rtfFind09.Text.Length,
                                        rtfFind10.Text.Length };
                rtf.Text = " ";
                for (i = 0; i <= 9; i++) 
                {
                    if (maxLengthF[i] > max)
                    {
                        max = maxLengthF[i];
                    }
                }
                // Make all the Find entries the same length
                int n = rtfFind01.Text.Length;
                int rlen = rtfReplace01.Text.Length;
                string arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);

                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind01.Rtf;

                    AppendFormattedText(RTB, arrow); // color is black, that is what I want.  Changed 2-23-2022
                    AppendFormattedText(RTB, rtfReplace01.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind02.Text.Length;
                rlen = rtfReplace02.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);

                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind02.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace02.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind03.Text.Length;
                rlen = rtfReplace03.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind03.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace03.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind04.Text.Length;
                rlen = rtfReplace04.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind04.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace04.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind05.Text.Length;
                rlen = rtfReplace05.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind05.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace05.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind06.Text.Length;
                rlen = rtfReplace06.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind06.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace06.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind07.Text.Length;
                rlen = rtfReplace07.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind07.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace07.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind08.Text.Length;
                rlen = rtfReplace08.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind08.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace08.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind09.Text.Length;
                rlen = rtfReplace09.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind09.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace09.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                n = rtfFind10.Text.Length;
                rlen = rtfReplace10.Text.Length;
                arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);
                if (n > 0 | rlen > 0)
                { // if one of the textboxes has content, process it
                    RTB.Rtf = rtfFind10.Rtf;
                    AppendFormattedText(RTB, arrow); // color is black, that is what I want
                    AppendFormattedText(RTB, rtfReplace10.Text);
                    RTB.SelectAll();
                    RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                    frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                }

                // Reset backcolor, so whole text has the same backcolor
                ColorDialog d = new ColorDialog();
                d.AnyColor = true;
                d.SolidColorOnly = false;
                d.Color = Globals.User_Settings.RTBMainBackColor;   // was Color.FromArgb(200, 190, 200);
                var _with17 = frmMain.RTBMain;
                _with17.SelectAll();
                _with17.BackColor = d.Color;
                _with17.SelectionBackColor = d.Color;
                _with17.SelectionLength = 0;
                _with17.SelectionStart = Convert.ToInt32(frmMain.RTBMain.SelectionStart);
                d.Dispose();
                frmMain.Activate();
                frmMain.BtnReturn.PerformClick();
                frmMain.RTBMain.Focus();
                frmMain.RTBMain.SelectionLength = 0;
                frmMain.RTBMain.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


		// For Changing font size
		// Font currentFont = rtfFind01.SelectionFont;
		// rtfFind01.SelectionFont = new Font(currentFont.FontFamily, 26, FontStyle.Regular);

		private void RbEditColor_Click(object sender, EventArgs e)
		{
			int z = 0;
			try
			{
				SearchSettings.rbEditColor = rbEditColor.Checked = true;
				rbMultipleReplace.Checked = !rbEditColor.Checked;


				Label[] lblReplace = {this.lblReplace01, this.lblReplace02,
					this.lblReplace03, this.lblReplace04, this.lblReplace05,
					this.lblReplace06, this.lblReplace07, this.lblReplace08,
					this.lblReplace09, this.lblReplace10
				};

				inColorEditMode = true;
				if (formLoading == false)
				{
					SearchSettings.chkAutoFindNext = this.chkAutoFindNext.Checked;
					SearchSettings.chkMatchCase = this.chkMatchCase.Checked;
					SearchSettings.chkWordOnly = this.chkWordOnly.Checked;
				}

				this.chkAutoFindNext.Checked = SearchSettings.chkAutoFindNext;
				this.chkMatchCase.Checked = SearchSettings.chkMatchCase;
				this.chkWordOnly.Checked = SearchSettings.chkWordOnly;
				SearchSettings.SearchName = this.rtfSearchName.Text;
				this.rtfSearchName.Text = SearchSettings.SearchName;


				this.btnGetSearch.Visible = true;
				this.btnSaveSearch.Visible = true;
				btnPasteInto.Visible = true;

				for (z = 0; z <= 7; z++)
				{
					lblReplace[z].Text = "Signifies ";
				}
				Button[] btnColor = {this.FrmColorBtnC01, this.FrmColorBtnC02, this.FrmColorBtnC03, this.FrmColorBtnC04,
							this.FrmColorBtnC05, this.FrmColorBtnC06, this.FrmColorBtnC07, this.FrmColorBtnC08,
							this.FrmColorBtnC09, this.FrmColorBtnC10};
				int x;
				for (x = 0; x <= 9; x++)
				{
					btnColor[x].Visible = true;
					// save find - replace textboxes,
					strSearchArray[x] = pFrmColorFindArry[x];
					strReplaceArray[x] = searchSettingsHintsArray[x];
					// retrieve color textbox contents
					searchSettingsHintsArray[x] = srtToolTipArray[x] + "";
				}

				// set frmColor to open on edit Color OPTION 0
				chkAutoFindNext.Visible = true;
				chkAutoFindNext.Checked = true;
				btnPasteInto.Visible = true;
				lblPipe01.Visible = true;
				lblPipe02.Visible = true;
				lblPipe03.Visible = true;
				lblPipe04.Visible = true;
				lblPipe05.Visible = true;
				lblPipe06.Visible = true;
				lblPipe07.Visible = true;
				lblPipe08.Visible = true;
				lblPipe09.Visible = true;
				lblPipe10.Visible = true;
				Globals.SearchChanged = false;

				// set button backcolors to show in color edit mode
				Color backColor = Color.FromArgb(228, 192, 255);
				this.btnFind.BackColor = backColor;
				this.btnReplace.BackColor = backColor;
				this.btnReplace_All.BackColor = backColor;
				this.btnClearFinds.BackColor = backColor;
				this.btnSaveSearch.BackColor = backColor;
				this.btnGetSearch.BackColor = backColor;
				this.btnClose.BackColor = backColor;
				this.btnClearReplace.BackColor = backColor;
				this.btnUndo.BackColor = backColor;
				this.btnClearAll.BackColor = backColor;
				this.btnSuffixReplace.BackColor = backColor;
				this.btnPrefixReplace.BackColor = backColor;
				this.btnPasteInto.BackColor = backColor;
				this.btnClear.BackColor = backColor;
				frmMain.Refresh();

				this.BackColor = Color.FromArgb(200, 190, 200);
				this.Refresh();
				Cursor.Current = Cursors.Default;
				rtfFind01.Select();
				GetRtfFindForeColor();
				this.Refresh();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void RbMultipleReplace_Click(object sender, EventArgs e)
		{
			SearchSettings.rbEditColor = rbEditColor.Checked = false;
			rbMultipleReplace.Checked = !rbEditColor.Checked;
			//rbReplaceAll.Checked = true;

			Button[] btnColor = {this.FrmColorBtnC01, this.FrmColorBtnC02, this.FrmColorBtnC03, this.FrmColorBtnC04,
							this.FrmColorBtnC05, this.FrmColorBtnC06, this.FrmColorBtnC07, this.FrmColorBtnC08,
							this.FrmColorBtnC09, this.FrmColorBtnC10};
			int buttonX;
			for (buttonX = 0; buttonX <= 9; buttonX++)
			{
				btnColor[buttonX].Visible = false;
			}
			btnPasteInto.Visible = false;
			rtfFind01.Select();
		}

		private void FrmColor_MouseEnter(object sender, EventArgs e)
		{
			if (rbEditColor.Checked) {
				this.FrmColorBtnC01.BackColor = Globals.User_Settings.Color01;
				this.FrmColorBtnC02.BackColor =Globals.User_Settings.Color02;
				this.FrmColorBtnC03.BackColor =Globals.User_Settings.Color03;
				this.FrmColorBtnC04.BackColor =Globals.User_Settings.Color04;
				this.FrmColorBtnC05.BackColor = Globals.User_Settings.Color05;
                this.FrmColorBtnC06.BackColor = Globals.User_Settings.Color06;
				this.FrmColorBtnC07.BackColor = Globals.User_Settings.Color07;
				this.FrmColorBtnC08.BackColor = Globals.User_Settings.Color08;
				this.FrmColorBtnC09.BackColor = Globals.User_Settings.Color09;
				this.FrmColorBtnC10.BackColor = Globals.User_Settings.Color10;

                this.rtfFind01.ForeColor = Globals.User_Settings.Color01;
				this.rtfFind02.ForeColor = Globals.User_Settings.Color02;
				this.rtfFind03.ForeColor = Globals.User_Settings.Color03;
				this.rtfFind04.ForeColor = Globals.User_Settings.Color04;
				this.rtfFind05.ForeColor = Globals.User_Settings.Color05;
				this.rtfFind06.ForeColor = Globals.User_Settings.Color06;
				this.rtfFind07.ForeColor = Globals.User_Settings.Color07;
				this.rtfFind08.ForeColor = Globals.User_Settings.Color08;
				this.rtfFind09.ForeColor = Globals.User_Settings.Color09;
				this.rtfFind10.ForeColor = Globals.User_Settings.Color10;
            }
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Visible = false;
			SearchSettings.rbEditColor = this.rbEditColor.Checked;
			SearchSettings.chkAutoFindNext = this.chkAutoFindNext.Checked;
			//SearchSettings.rbReplaceAll = this.rbReplaceAll.Checked;
			//SearchSettings.rbSuffix = this.rbSuffix.Checked;
			//SearchSettings.rbPrefix = this.rbPrefix.Checked;
			SearchSettings.chkMatchCase = this.chkMatchCase.Checked;
			SearchSettings.chkWordOnly = this.chkWordOnly.Checked;
			SearchSettings.chkReverse = this.chkReverse.Checked;

            SearchSettings.frmColorFind01_Rtf = this.rtfFind01.Rtf;
            SearchSettings.frmColorFind02_Rtf = this.rtfFind02.Rtf;
            SearchSettings.frmColorFind03_Rtf = this.rtfFind03.Rtf;
            SearchSettings.frmColorFind04_Rtf = this.rtfFind04.Rtf;
            SearchSettings.frmColorFind05_Rtf = this.rtfFind05.Rtf;
            SearchSettings.frmColorFind06_Rtf = this.rtfFind06.Rtf;
            SearchSettings.frmColorFind07_Rtf = this.rtfFind07.Rtf;
            SearchSettings.frmColorFind08_Rtf = this.rtfFind08.Rtf;
            SearchSettings.frmColorFind09_Rtf = this.rtfFind09.Rtf;
            SearchSettings.frmColorFind10_Rtf = this.rtfFind10.Rtf;

            SearchSettings.frmColorReplace01_Rtf = this.rtfReplace01.Rtf;
            SearchSettings.frmColorReplace02_Rtf = this.rtfReplace02.Rtf;
            SearchSettings.frmColorReplace03_Rtf = this.rtfReplace03.Rtf;
            SearchSettings.frmColorReplace04_Rtf = this.rtfReplace04.Rtf;
            SearchSettings.frmColorReplace05_Rtf = this.rtfReplace05.Rtf;
            SearchSettings.frmColorReplace06_Rtf = this.rtfReplace06.Rtf;
            SearchSettings.frmColorReplace07_Rtf = this.rtfReplace07.Rtf;
            SearchSettings.frmColorReplace08_Rtf = this.rtfReplace08.Rtf;
            SearchSettings.frmColorReplace09_Rtf = this.rtfReplace09.Rtf;
            SearchSettings.frmColorReplace10_Rtf = this.rtfReplace10.Rtf;


            SearchSettings.frmColorFind01_Txt = this.rtfFind01.Text;
			SearchSettings.frmColorFind02_Txt = this.rtfFind02.Text;
			SearchSettings.frmColorFind03_Txt = this.rtfFind03.Text;
			SearchSettings.frmColorFind04_Txt = this.rtfFind04.Text;
			SearchSettings.frmColorFind05_Txt = this.rtfFind05.Text;
			SearchSettings.frmColorFind06_Txt = this.rtfFind06.Text;
			SearchSettings.frmColorFind07_Txt = this.rtfFind07.Text;
			SearchSettings.frmColorFind08_Txt = this.rtfFind08.Text;
			SearchSettings.frmColorFind09_Txt = this.rtfFind09.Text;
			SearchSettings.frmColorFind10_Txt = this.rtfFind10.Text;

            SearchSettings.frmColorReplace01_Txt = this.rtfReplace01.Text;
			SearchSettings.frmColorReplace02_Txt = this.rtfReplace02.Text;
			SearchSettings.frmColorReplace03_Txt = this.rtfReplace03.Text;
			SearchSettings.frmColorReplace04_Txt = this.rtfReplace04.Text;
			SearchSettings.frmColorReplace05_Txt = this.rtfReplace05.Text;
			SearchSettings.frmColorReplace06_Txt = this.rtfReplace06.Text;
			SearchSettings.frmColorReplace07_Txt = this.rtfReplace07.Text;
			SearchSettings.frmColorReplace08_Txt = this.rtfReplace08.Text;
			SearchSettings.frmColorReplace09_Txt = this.rtfReplace09.Text;
			SearchSettings.frmColorReplace10_Txt = this.rtfReplace10.Text;

			Point pt = new Point(this.Left, this.Top);	
			Globals.User_Settings.FrmColorLocation = pt;

			for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
			{
				if (Application.OpenForms[i].Name == "FrmGetSearches")
					Application.OpenForms[i].Visible = false;
			}

		}


		private void ChkWordOnly_Click(object sender, EventArgs e)
		{
			SetRtfFindsBackColorToNormal();
			Globals.SearchChanged = true;
			SearchSettings.chkWordOnly = chkWordOnly.Checked;
		}

		private void ChkReverse_Click(object sender, EventArgs e)
		{
			Globals.FindStart = 1;
			SearchSettings.chkReverse = chkReverse.Checked;
		}

		// Prevent any entries other than numbers
		private void TxtSearchTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void BtnUndo_Click(object sender, EventArgs e)
		{
			frmMain.RTBMain.Rtf = Globals.Main_Backup.Rtf;
			frmMain.Refresh();
		}

		public void SetDisplayTime(){
			this.txtSearchTime.Text = Globals.DisplayTime.ToString();
		}

		void UpdateSearchSettings()
		{
			Globals.Main_Backup.Rtf = frmMain.RTBMain.Rtf;
			Globals.SearchInProgress = true;
			Globals.StartTime = DateTime.Now; // For limiting search time
			Globals.DisplayTime = Convert.ToInt32(this.txtSearchTime.Text);

			SearchSettings.SearchName = rtfSearchName.Text;
			SearchSettings.frmColorFind01_Txt = this.rtfFind01.Text;
			SearchSettings.frmColorFind02_Txt = this.rtfFind02.Text;
			SearchSettings.frmColorFind03_Txt = this.rtfFind03.Text;
			SearchSettings.frmColorFind04_Txt = this.rtfFind04.Text;
			SearchSettings.frmColorFind05_Txt = this.rtfFind05.Text;
			SearchSettings.frmColorFind06_Txt = this.rtfFind06.Text;
			SearchSettings.frmColorFind07_Txt = this.rtfFind07.Text;
			SearchSettings.frmColorFind08_Txt = this.rtfFind08.Text;
			SearchSettings.frmColorFind09_Txt = this.rtfFind09.Text;
			SearchSettings.frmColorFind10_Txt = this.rtfFind10.Text;
			SearchSettings.frmColorReplace01_Txt = this.rtfReplace01.Text;
			SearchSettings.frmColorReplace02_Txt = this.rtfReplace02.Text;
			SearchSettings.frmColorReplace03_Txt = this.rtfReplace03.Text;
			SearchSettings.frmColorReplace04_Txt = this.rtfReplace04.Text;
			SearchSettings.frmColorReplace05_Txt = this.rtfReplace05.Text;
			SearchSettings.frmColorReplace06_Txt = this.rtfReplace06.Text;
			SearchSettings.frmColorReplace07_Txt = this.rtfReplace07.Text;
			SearchSettings.frmColorReplace08_Txt = this.rtfReplace08.Text;
			SearchSettings.frmColorReplace09_Txt = this.rtfReplace09.Text;
			SearchSettings.frmColorReplace10_Txt = this.rtfReplace10.Text;
			SearchSettings.rbEditColor = this.rbEditColor.Checked;
			SearchSettings.chkAutoFindNext = this.chkAutoFindNext.Checked;
			//SearchSettings.rbSuffix = this.rbSuffix.Checked;
			//SearchSettings.rbPrefix = this.rbPrefix.Checked;
			SearchSettings.chkMatchCase = this.chkMatchCase.Checked;
			SearchSettings.chkWordOnly = this.chkWordOnly.Checked;
			SearchSettings.chkReverse = this.chkReverse.Checked;
		}


		// Each of the following 3 functions are tied to buttons Replace All, Prefix Replace, Suffix Replace
		// Each call startReplaceProceedure(object sender, EventArgs e)
		// Which is listed below them, and goes out to frmMain to complete the search
		private void BtnReplace_All_Click(object sender, EventArgs e)
		{
			Globals.ReplaceAll = true;
			Globals.Suffix = false;
			Globals.Prefix = false;
			UpdateSearchSettings();
			StartReplaceProceedure(sender, e);
			Globals.ReplaceAll = false;
			Globals.Suffix = false;
			Globals.Prefix = false;
		}

		private void BtnPrefixReplace_Click(object sender, EventArgs e)
		{
			Globals.Prefix = true;
			Globals.Suffix = false;
			Globals.ReplaceAll = false;
			UpdateSearchSettings();
			StartReplaceProceedure(sender, e);
			Globals.ReplaceAll = false;
			Globals.Suffix = false;
			Globals.Prefix = false;
		}

		private void BtnSuffixReplace_Click(object sender, EventArgs e)
		{
			Globals.Suffix = true;
			Globals.Prefix = false;
			Globals.ReplaceAll = false;
			UpdateSearchSettings();
			StartReplaceProceedure(sender, e);
			Globals.ReplaceAll = false;
			Globals.Suffix = false;
			Globals.Prefix = false;
		}

 		// This jumps to frmMain to do the search-replace for text or color
		private void StartReplaceProceedure(object sender, EventArgs e) {

			frmMain.FrmColorbtnReplace_All_In_Main(this);
			Globals.SearchChanged = true;
			frmMain.RTBMain.SelectionLength = 0;
			if (this.rbEditColor.Checked)
			{
				Globals.ToolTip01 = this.rtfReplace01.Text;
				Globals.ToolTip02 = this.rtfReplace02.Text;
				Globals.ToolTip03 = this.rtfReplace03.Text;
				Globals.ToolTip04 = this.rtfReplace04.Text;
				Globals.ToolTip05 = this.rtfReplace05.Text;
				Globals.ToolTip06 = this.rtfReplace06.Text;
				Globals.ToolTip07 = this.rtfReplace07.Text;
				Globals.ToolTip08 = this.rtfReplace08.Text;
				Globals.ToolTip09 = this.rtfReplace09.Text;
				Globals.ToolTip10 = this.rtfReplace10.Text;
			}
			Globals.SearchInProgress = false;
			SetRtfFindsBackColorToNormal();
			this.txtSearchTime.Text = Globals.DisplayTime.ToString();
		}


        private void FrmColor_VisibleChanged(object sender, EventArgs e)
        {
			if (this.Visible) {
				if (SearchSettings.rbEditColor)
				{
					rbEditColor.Checked = true;
					rbMultipleReplace.Checked = false;
				}
				else
				{
					rbMultipleReplace.Checked = true;
					rbEditColor.Checked = false;
				}
			}

		}

        private void FrmColor_MouseLeave(object sender, EventArgs e)
        {
			Globals.ToolTip01 = this.rtfReplace01.Text;
			Globals.ToolTip02 = this.rtfReplace02.Text;
			Globals.ToolTip03 = this.rtfReplace03.Text;
			Globals.ToolTip04 = this.rtfReplace04.Text;
			Globals.ToolTip05 = this.rtfReplace05.Text;
			Globals.ToolTip06 = this.rtfReplace06.Text;
			Globals.ToolTip07 = this.rtfReplace07.Text;
			Globals.ToolTip08 = this.rtfReplace08.Text;
			Globals.ToolTip09 = this.rtfReplace09.Text;
			Globals.ToolTip10 = this.rtfReplace10.Text;
		}

        private void RtfSearchName_MouseDown(object sender, MouseEventArgs e)
        {
			Globals.Current_RTB_withFocus = rtfSearchName;
            Globals.Current_RTB_withFocus.Focus();
            // Capture the starting position of the mouse cursor
            Globals.SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);
        }

		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		// START KEYUP - KEYDOWN
		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

		private void TxtSearchName_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.SearchName = this.rtfSearchName.Text;
		}

		private void RtfFind01_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind01_Txt = rtfFind01.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			//preventFontChange(rtfFind01, this.btnC01.ForeColor); // Removed and added to KeyPress event, as copy-paste cannot work with this here.
		}

		private void RtfFind02_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind02_Txt = rtfFind02.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			//preventFontChange(rtfFind01, this.btnC02.ForeColor); // Removed and added to KeyPress event, as copy-paste cannot work with this here.
		}

		private void RtfFind03_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind03_Txt = rtfFind03.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind03, this.btnC03.ForeColor);  // See above
		}

		private void RtfFind04_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind04_Txt = rtfFind04.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind04, this.btnC04.ForeColor);
		}

		private void RtfFind05_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind05_Txt = rtfFind05.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind05, this.btnC05.ForeColor);
		}

		private void RtfFind06_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind06_Txt = rtfFind06.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind06, this.btnC06.ForeColor);
		}

		private void RtfFind07_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind07_Txt = rtfFind07.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind07, this.btnC07.ForeColor);
		}

		private void RtfFind08_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind08_Txt = rtfFind08.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind08, this.btnC08.ForeColor);
		}

		private void RtfFind09_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind09_Txt = rtfFind09.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind09, this.btnC09.ForeColor);
		}

		private void RtfFind10_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorFind10_Txt = rtfFind10.Text;
			Globals.BtnFindStart = 0;
			Globals.SearchRoundCount = 0;
			Globals.BoolFrmColorSearchSaved = false;
			Globals.SearchChanged = true;
			// preventFontChange(rtfFind10, this.btnC10.ForeColor);
		}


		private void RtfReplace01_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace01_Txt = rtfReplace01.Text;
			//preventFontChange(rtfReplace01, Color.Black);  // Removed and added to KeyPress event, as copy-paste cannot work with this here.
		}

		private void RtfReplace02_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace02_Txt = rtfReplace02.Text;
			//preventFontChange(rtfReplace02, Color.Black); // Removed and added to KeyPress event, as copy-paste cannot work with this here. 
		}

		private void RtfReplace03_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace03_Txt = rtfReplace03.Text;
			//preventFontChange(rtfReplace03, Color.Black);  // See above
		}

		private void RtfReplace04_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace04_Txt = rtfReplace04.Text;
			//preventFontChange(rtfReplace04, Color.Black);
		}

		private void RtfReplace05_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace05_Txt = rtfReplace05.Text;
			// preventFontChange(rtfReplace05, Color.Black);
		}

		private void RtfReplace06_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace06_Txt = rtfReplace06.Text;
			// preventFontChange(rtfReplace06, Color.Black);
		}

		private void RtfReplace07_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace07_Txt = rtfReplace07.Text;
			// preventFontChange(rtfReplace07, Color.Black);
		}

		private void RtfReplace08_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace08_Txt = rtfReplace08.Text;
			// preventFontChange(rtfReplace08, Color.Black);
		}

		private void RtfReplace09_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace09_Txt = rtfReplace09.Text;
			// preventFontChange(rtfReplace09, Color.Black);
		}

		private void RtfReplace10_KeyUp(object sender, KeyEventArgs e)
		{
			SearchSettings.frmColorReplace10_Txt = rtfReplace10.Text;
			// preventFontChange(rtfReplace10, Color.Black);
		}



        private void FrmColor_KeyDown(object sender, KeyEventArgs e)
        {
            keyboardShortcuts.HandleFKeys(sender, e);
            keyboardShortcuts.HandleAltKeys(sender, e);
            keyboardShortcuts.HandleControlKeys(sender, e);
            keyboardShortcuts.HandleShiftKeys(sender, e);
        }

        private void FrmColor_KeyUp(object sender, KeyEventArgs e)
        {
            keyboardShortcuts.ShowKeyboardShortcutsPopWindowIfNeeded();
        }

		public static string GetCurrentLanguage()
		{
			return InputLanguage.CurrentInputLanguage.Culture.EnglishName;
		}

		// foreColor is the richtextbox forecolor desired
		private void PreventFontChange(RichTextBox RTB,  Color foreColor) {
			//int length = RTB.SelectionStart + Globals.current_RTB_withFocus.SelectedText.Length;
			int length = RTB.SelectionStart + RTB.SelectedText.Length; // for end cursor position
			RTB.SelectAll();
			RTB.ForeColor = foreColor;
			RTB.SelectionBackColor = Color.FromArgb(230, 230, 230);
			RTB.SelectionFont = new Font("Times New Roman", 16, FontStyle.Bold);
			RTB.SelectionStart = length;  // end cursor position
			RTB.SelectionLength = 0;
		}


		private void RtfSearchName_KeyPress(object sender, KeyPressEventArgs e)
		{
			PreventFontChange(rtfSearchName, Color.Black);
		}

		private void RtfFind01_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind01, this.FrmColorBtnC01.ForeColor);
		}

        private void RtfFind02_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind02, this.FrmColorBtnC02.ForeColor);
		}

        private void RtfFind03_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind03, this.FrmColorBtnC03.ForeColor);
		}

        private void RtfFind04_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind04, this.FrmColorBtnC04.ForeColor);
		}

        private void RtfFind05_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind05, this.FrmColorBtnC05.ForeColor);
		}

        private void RtfFind06_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind06, this.FrmColorBtnC06.ForeColor);
		}

        private void RtfFind07_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind07, this.FrmColorBtnC07.ForeColor);
		}

        private void RtfFind08_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind08, this.FrmColorBtnC08.ForeColor);
		}

        private void RtfFind09_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind09, this.FrmColorBtnC09.ForeColor);
		}

        private void RtfFind10_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfFind10, this.FrmColorBtnC10.ForeColor);
		}

        private void RtfReplace01_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace01, Color.Black);
		}

        private void RtfReplace02_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace02, Color.Black);
		}

        private void RtfReplace03_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace03, Color.Black);
		}

        private void RtfReplace04_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace04, Color.Black);
		}

        private void RtfReplace05_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace05, Color.Black);
		}

        private void RtfReplace06_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace06, Color.Black);
		}

        private void RtfReplace07_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace07, Color.Black);
		}

        private void RtfReplace08_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace08, Color.Black);
		}

        private void RtfReplace09_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace09, Color.Black);
		}

        private void RtfReplace10_KeyPress(object sender, KeyPressEventArgs e)
        {
			PreventFontChange(rtfReplace10, Color.Black);
		}

        private void RtfSearchName_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = rtfSearchName;
            Globals.Current_RTB_withFocus.Focus();

            // Calculate the ending position of the selection
            int selEnd = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);

            // If the starting position and the ending position are the same, return without selecting anything
            if (selEnd == Globals.SelStart) { return; }

            // Determine the selection start and length based on the relative positions of the starting and ending points
            int selectionStart = Math.Min(Globals.SelStart, selEnd);
            int selectionLength = Math.Abs(Globals.SelStart - selEnd);

            // Set the selection start and length
            Globals.Current_RTB_withFocus.SelectionStart = selectionStart;
            Globals.Current_RTB_withFocus.SelectionLength = selectionLength;

            // Set the focus back to the control to keep the selection highlighted
            Globals.Current_RTB_withFocus.Focus();
        }

        private void FrmColor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain.FrmMainColorBtn01.BackColor = FrmColorBtnC01.BackColor;
            frmMain.FrmMainColorBtn02.BackColor = FrmColorBtnC02.BackColor;
            frmMain.FrmMainColorBtn03.BackColor = FrmColorBtnC03.BackColor;
            frmMain.FrmMainColorBtn04.BackColor = FrmColorBtnC04.BackColor;
            frmMain.FrmMainColorBtn05.BackColor = FrmColorBtnC05.BackColor;
            frmMain.FrmMainColorBtn06.BackColor = FrmColorBtnC06.BackColor;
            frmMain.FrmMainColorBtn07.BackColor = FrmColorBtnC07.BackColor;
            frmMain.FrmMainColorBtn08.BackColor = FrmColorBtnC08.BackColor;
            frmMain.FrmMainColorBtn09.BackColor = FrmColorBtnC09.BackColor;
            frmMain.FrmMainColorBtn10.BackColor = FrmColorBtnC10.BackColor;

            Globals.SearchTimeLimit = Convert.ToInt32(this.txtSearchTime.Text);
            Globals.User_Settings.searchTimeLimit = Globals.SearchTimeLimit;
            this.Visible = false;
            e.Cancel = true;
            Globals.Current_RTB_withFocus = frmMain.RTBMain;
        }

        private void BtnImportSearch_Click(object sender, EventArgs e)
        {
            GeneralFns.SelectAndImportFiles(FileExtensionType.Fdta);
        }

        private void BtnExportSearch_Click(object sender, EventArgs e)
        {
            GeneralFns.SelectAndExportFiles(FileExtensionType.Fdta);
        }

		private void RetNormalFontOnChange(RichTextBox richTextBox, Color color) {
            // Save the current selection and clear the formatting
            int selectionStart = richTextBox.SelectionStart;
            int selectionLength = richTextBox.SelectionLength;
           richTextBox.SelectionFont = richTextBox.Font;
           richTextBox.SelectionColor = Color.Black;
            richTextBox.SelectionBackColor = Color.White;

            // Apply the desired formatting to the entire text
            richTextBox.SelectAll();
            richTextBox.SelectionFont = new Font("Times New Roman", 16, FontStyle.Bold);
            richTextBox.SelectionColor = color;
            richTextBox.SelectionBackColor = Color.FromArgb(230, 230, 230);

            // Restore the previous selection
            richTextBox.SelectionStart = selectionStart;
            richTextBox.SelectionLength = selectionLength;
        }

        private void RtfFind01_TextChanged(object sender, EventArgs e)
        {
			RetNormalFontOnChange(rtfFind01, FrmColorBtnC01.BackColor);
        }

        private void RtfReplace01_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace01, Color.Black);
        }

        private void RtfFind02_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind02, FrmColorBtnC02.BackColor);
        }

        private void RtfReplace02_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace02, Color.Black);
        }

        private void RtfFind03_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind03, FrmColorBtnC03.BackColor);
        }

        private void RtfReplace03_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace03, Color.Black);
        }

        private void RtfFind04_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind04, FrmColorBtnC04.BackColor);
        }

        private void RtfReplace04_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace04, Color.Black);
        }

        private void RtfFind05_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind05, FrmColorBtnC05.BackColor);
        }

        private void RtfReplace05_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace05, Color.Black);
        }

        private void RtfFind06_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind06, FrmColorBtnC06.BackColor);
        }

        private void RtfReplace06_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace06, Color.Black);
        }

        private void RtfFind07_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind07, FrmColorBtnC07.BackColor);
        }

        private void RtfReplace07_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace07, Color.Black);
        }

        private void RtfFind08_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind08, FrmColorBtnC08.BackColor);
        }

        private void RtfReplace08_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace08, Color.Black);
        }

        private void RtfFind09_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind09, FrmColorBtnC09.BackColor);
        }

        private void RtfReplace09_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace09, Color.Black);
        }

        private void RtfFind10_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfFind10, FrmColorBtnC10.BackColor);
        }

        private void RtfReplace10_TextChanged(object sender, EventArgs e)
        {
            RetNormalFontOnChange(rtfReplace10, Color.Black);
        }
    }
}


