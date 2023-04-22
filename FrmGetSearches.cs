using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tachufind
{
	
	public partial class FrmGetSearches : Form
    {
		FrmColor frmColor;
		public FrmGetSearches(FrmColor frmColor)
        {
            InitializeComponent();
			this.frmColor = frmColor;
		}

        private void FrmGetSearches_Load(object sender, EventArgs e)
        {
            this.listOfSearches.Focus();

            this.Left = Globals.User_Settings.FrmColorLocation.X - 40;
            this.Top = Globals.User_Settings.FrmColorLocation.Y + 180;
        }

        private void ListOfSearches_Click(object sender, EventArgs e)
        {
			SearchFns srch = new SearchFns();
			List<string> searchValues = null;

			try
			{
				if ((this.listOfSearches.SelectedItem == null))
				{
					return;
				}
				SearchSettings.SearchName = this.listOfSearches.SelectedItem.ToString();
				frmColor.rtfSearchName.Text = SearchSettings.SearchName;
				if (this.listOfSearches.SelectedIndex < 0)
					return;
				if (SearchSettings.SearchName.Length > 0)
				{
					// retreive a search table from Searches.dat
					searchValues = srch.GetEntryTitleByName(SearchSettings.SearchName);
					if (searchValues.Count < 1) { return; }

                    SearchSettings.rbEditColor = Convert.ToBoolean(searchValues[12]);
					SearchSettings.chkAutoFindNext = Convert.ToBoolean(searchValues[13]);
					SearchSettings.chkMatchCase = Convert.ToBoolean(searchValues[14]);
					SearchSettings.chkWordOnly = Convert.ToBoolean(searchValues[15]);
					SearchSettings.chkReverse = Convert.ToBoolean(searchValues[16]);

					SearchSettings.frmColorFind01_Txt = frmColor.rtfFind01.Text = searchValues[17];
                    SearchSettings.frmColorReplace01_Txt = frmColor.rtfReplace01.Text = searchValues[18];
					SearchSettings.frmColorFind02_Txt = frmColor.rtfFind02.Text = searchValues[19];
					SearchSettings.frmColorReplace02_Txt = frmColor.rtfReplace02.Text = searchValues[20];
					SearchSettings.frmColorFind03_Txt = frmColor.rtfFind03.Text = searchValues[21];
					SearchSettings.frmColorReplace03_Txt = frmColor.rtfReplace03.Text = searchValues[22];
					SearchSettings.frmColorFind04_Txt = frmColor.rtfFind04.Text = searchValues[23];
					SearchSettings.frmColorReplace04_Txt = frmColor.rtfReplace04.Text = searchValues[24];
					SearchSettings.frmColorFind05_Txt = frmColor.rtfFind05.Text = searchValues[25];
					SearchSettings.frmColorReplace05_Txt = frmColor.rtfReplace05.Text = searchValues[26];
					SearchSettings.frmColorFind06_Txt = frmColor.rtfFind06.Text = searchValues[27];
					SearchSettings.frmColorReplace06_Txt = frmColor.rtfReplace06.Text = searchValues[28];
					SearchSettings.frmColorFind07_Txt = frmColor.rtfFind07.Text = searchValues[29];
					SearchSettings.frmColorReplace07_Txt = frmColor.rtfReplace07.Text = searchValues[30];
					SearchSettings.frmColorFind08_Txt = frmColor.rtfFind08.Text = searchValues[31];
					SearchSettings.frmColorReplace08_Txt = frmColor.rtfReplace08.Text = searchValues[32];
					SearchSettings.frmColorFind09_Txt = frmColor.rtfFind09.Text = searchValues[33];
					SearchSettings.frmColorReplace09_Txt = frmColor.rtfReplace09.Text = searchValues[34];
					SearchSettings.frmColorFind10_Txt = frmColor.rtfFind10.Text = searchValues[35];
					SearchSettings.frmColorReplace10_Txt = frmColor.rtfReplace10.Text = searchValues[36];
				}
				frmColor.Refresh();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Convert String value to Color value
		private Color StrToColor(string colorString)
		{
			string[] values = colorString.Split(',');
			int alpha = Convert.ToInt32(values[0]);
			int red = Convert.ToInt32(values[1]);
			int green = Convert.ToInt32(values[2]);
			int blue = Convert.ToInt32(values[3]);
			return Color.FromArgb(alpha, red, green, blue);
		}


		private void BtnDelete_Click(object sender, EventArgs e)
		{
			int x = 0;
			List<string> savedItems = null;

			try
			{
				// delete search file and remove from list
				if ((this.listOfSearches.SelectedItem == null) || this.listOfSearches.SelectedItem.ToString().Length < 1)
				{
					return;
				}
				string path = Globals.Data_Folder + this.listOfSearches.SelectedItem.ToString() + Globals.FindReplaceFileExtension;
				bool deleted = DeleteEntry(this.listOfSearches.SelectedItem.ToString(), path, true);
				if (!deleted)
				{
					return;
				}
				savedItems = new List<string>();
				RemoveSelectedItem(this.listOfSearches);

				for (x = 0; x <= listOfSearches.Items.Count - 1; x++)
				{
					if (!string.IsNullOrEmpty(Convert.ToString(listOfSearches.Items[x])))
					{
						savedItems.Add(Convert.ToString(listOfSearches.Items[x]));
					}
				}
				this.Show();
				this.listOfSearches.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		// If a matching entry is found, it deletes the file.
		public bool DeleteEntry(string itemTitleToBeDeleted, string pathToFile, bool confirmBeforedelete = false)
		{
			try
			{
				if (!File.Exists(pathToFile))
				{
					return false;
				}
				string title = string.Empty;
				title = Path.GetFileNameWithoutExtension(pathToFile);
				if (confirmBeforedelete == true)
				{
					int answer = (int)MessageBox.Show("Are you sure you want to delete " +
						  title + "?", "Delete Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (answer == (int)System.Windows.Forms.DialogResult.No)
					{
						return false;
					}
				}
				File.Delete(pathToFile);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return true;
		}

		public void RemoveSelectedItem(ListBox Box)
		{
			int Index = Box.SelectedIndex;
            try
            {
                //Index of selected item
                if (Index > -1)
                {
                    Box.Items.RemoveAt(Index);
                    //Remove it
                    Box.Refresh();
                }
                this.listOfSearches.Focus();
                int itemCount = listOfSearches.Items.Count - 1;
                if (itemCount > Index)
                {
                    this.listOfSearches.SelectedIndex = Index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


		private void FrmGetSearches_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
			this.Visible = false;
			frmColor.Activate();
			frmColor.Show();
		}

        private void FrmGetSearches_KeyDown(object sender, KeyEventArgs e)
        {
			this.listOfSearches.Focus();
			if (Control.ModifierKeys == Keys.Escape)
			{
				this.Visible = false;
			}
		}

        private void ListOfSearches_KeyUp(object sender, KeyEventArgs e)
        {
			try
			{
				int index = 0;

				if (e.KeyCode == Keys.Delete)
				{
					btnDelete.PerformClick();
				}

				index = this.listOfSearches.SelectedIndex;
				if (e.KeyCode == Keys.Up & index == 0)
				{
					this.listOfSearches.SelectedIndex = this.listOfSearches.Items.Count - 1;
				}
				if (this.listOfSearches.SelectedIndex == this.listOfSearches.Items.Count - 1 & e.KeyCode == Keys.Down)
				{
					this.listOfSearches.SelectedIndex = 0;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void FrmGetSearches_MouseEnter(object sender, EventArgs e)
        {
			Focus();
		}

        private void ListOfSearches_MouseEnter(object sender, EventArgs e)
        {
			Focus();
		}


    }
}
