using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace Tachufind
{
    public partial class FrmGetSearches : Form
    {

        public FrmGetSearches()
        {
            InitializeComponent();
        }

        private void FrmGetSearches_Load(object sender, EventArgs e)
        {
            this.cboSearchFilter.Focus();

            ScreenSetUp(this);
        }

        private void ScreenSetUp(Form form)
        {
            Point savedLocation = Globals.User_Settings.FrmGetSearchesLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        #region Button__Clicks

        private void BtnGetSearchesDelete_Click(object sender, EventArgs e)
        {
            int x = 0;
            List<string> savedItems;

            try
            {
                // delete search file and remove from list
                var selectedItem = this.ListOfSearchTitles.SelectedItem as string;

                if (string.IsNullOrEmpty(selectedItem) || selectedItem.Length < 1)
                {
                    return;
                }
                string path = AppSettings.Data_Folder + selectedItem.ToString() + AppSettings.FindReplaceFileExtension;
                bool deleted = DeleteEntry(path, true);
                if (!deleted)
                {
                    return;
                }
                savedItems = [];
                RemoveSelectedItem(this.ListOfSearchTitles);


                var item = ListOfSearchTitles.Items[x] as string;
                for (x = 0; x <= ListOfSearchTitles.Items.Count - 1; x++)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        savedItems.Add(item);
                    }
                }
                this.Show();
                this.ListOfSearchTitles.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetSearchesClose_Click(object sender, EventArgs e)
        {
            Globals.User_Settings.CboSearchFilterSelection = cboSearchFilter.Text;
            this.Visible = false;
        }

        #endregion Button__Clicks


        #region Events

        private void FrmGetSearches_KeyDown(object sender, KeyEventArgs e)
        {
            this.ListOfSearchTitles.Focus();
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
                    this.btnGetSearchesDelete.PerformClick();
                }

                index = this.ListOfSearchTitles.SelectedIndex;
                if (e.KeyCode == Keys.Up & index == 0)
                {
                    this.ListOfSearchTitles.SelectedIndex = this.ListOfSearchTitles.Items.Count - 1;
                }
                if (this.ListOfSearchTitles.SelectedIndex == this.ListOfSearchTitles.Items.Count - 1 & e.KeyCode == Keys.Down)
                {
                    this.ListOfSearchTitles.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmGetSearches_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ListOfSearches_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void FrmGetSearches_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.User_Settings.SearchListBoxSelection = cboSearchFilter.Text;

            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmGetSearchesLocation = location);

            this.Visible = false;
            e.Cancel = true;
        }


        // LOCATION → → → → →  ListOfSearches_MouseClick
        private void ListOfSearches_MouseClick(object sender, MouseEventArgs e)
        {
            ListOfSearchTitles.Enabled = false;

            var srch = new GetColorWindowSavedSearches();
            List<string> searchValues;

            try
            {
                if ((this.ListOfSearchTitles.SelectedItem == null))
                {
                    return;
                }
                SearchSettings.SearchName = this.ListOfSearchTitles.SelectedItem.ToString() ?? string.Empty;
                if (this.ListOfSearchTitles.SelectedIndex < 0)
                    return;
                if (SearchSettings.SearchName.Length > 0)
                {
                    // retreive a search table from Searches.dat
                    try
                    {
                        searchValues = srch.GetEntryTitleByName(SearchSettings.SearchName);
                    }
                    catch
                    {
                        MessageBox.Show("This search has been corrupted and fails to load.");
                    }

                    searchValues = srch.GetEntryTitleByName(SearchSettings.SearchName);
                    if (searchValues.Count < 1) { return; }

                    // This uses checking to make sure values translate through for SearchSettings:
                    // .RbEditColor, .ChkAutoFindNext, .ChkMatchCase, .ChkWordOnly.
                    // These are values accessed as 12-15
                    UseDefaultValuesForAnyFailures(searchValues);
                    //SearchSettings.RbEditColor = Convert.ToBoolean(searchValues[12]);
                    //SearchSettings.ChkAutoFindNext = Convert.ToBoolean(searchValues[13]);
                    //SearchSettings.ChkMatchCase = Convert.ToBoolean(searchValues[14]);
                    //SearchSettings.ChkWordOnly = Convert.ToBoolean(searchValues[15]);
                    SearchSettings.Reserved = true;  // Reserved [16]

                    for (int i = 17; i < 37; i += 2) // Use checking and send a string.Empty if failure occurs.
                    {
                        int place = i;      // first entry in the pair
                        int next = i + 1;   // second entry in the pair

                        string find = string.Empty;
                        string replace = string.Empty;

                        try
                        {
                            find = searchValues[place];
                        }
                        catch
                        {
                            find = string.Empty;
                        }

                        try
                        {
                            replace = searchValues[next];  // Replace or Note when in Color mode
                        }
                        catch
                        {
                            replace = string.Empty;
                        }

                        // Assuming you want to update SearchSettings from index 1 to 10
                        int index = (i - 17) / 2 + 1;
                        SearchSettings.SetText(index, true, find);  // true means rtfFind
                        SearchSettings.SetText(index, false, replace);  // false means rtfReplace
                    }
                }
                AppSettings.SearchJustRetrieved = true;
                this.Hide();
                this.UpdateFrmColor();
                this.Show();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ListOfSearchTitles.Enabled = true;
            }
        }

        public void UpdateFrmColor()
        {
            try
            {
                FrmColor? frmColor = FrmColor.Instance;

                if (frmColor == null)
                {
                    MessageBox.Show("FrmColor instance could not be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (frmColor.InvokeRequired)
                {
                    frmColor.BeginInvoke(new Action(UpdateFormFields));
                }
                else
                {
                    UpdateFormFields();
                }

                void UpdateFormFields()
                {
                    FrmColor? frmColor = FrmColor.Instance;

                    Globals.DisplayTime = Convert.ToInt32(frmColor.TxtTimeIndicator.Text);
                    frmColor.RtfSearchName.Text = SearchSettings.SearchName;    // Changed from AppSettings 11-30-2024
                    frmColor.RbEditColor.Checked = SearchSettings.FrmColorReplaceMode;
                    frmColor.RbTextReplace.Checked = !SearchSettings.FrmColorReplaceMode;
                    frmColor.ChkMatchCase.Checked = SearchSettings.ChkMatchCase;
                    frmColor.ChkWordOnly.Checked = SearchSettings.ChkWordOnly;

                    for (int i = 1; i <= 10; i++)
                    {
                        if (frmColor.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                        {
                            rtfFind.Text = SearchSettings.GetText(i, true); // true means rtfFind
                        }
                        if (frmColor.Controls[$"RtfReplace{i}"] is RichTextBox rtfReplace)
                        {
                            rtfReplace.Text = SearchSettings.GetText(i, false); // false means rtfReplace
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UseDefaultValuesForAnyFailures(List<string> searchValues)
        {
            try
            {
                SearchSettings.FrmColorReplaceMode = Convert.ToBoolean(searchValues[12]);
            }
            catch
            {
                SearchSettings.FrmColorReplaceMode = true;
            }

            try
            {
                SearchSettings.ChkMatchCase = Convert.ToBoolean(searchValues[14]);
            }
            catch
            {
                SearchSettings.ChkMatchCase = false;
            }

            try
            {
                SearchSettings.ChkWordOnly = Convert.ToBoolean(searchValues[15]);
            }
            catch
            {
                SearchSettings.ChkWordOnly = false;
            }
        }
        #endregion // Events


        #region GeneralUtilityFunctions

        public void Filter_QuizListBox()
        {
            // CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding();
            // InitalizeAndClearQuizValues();

            Globals.User_Settings.quizListBoxSelection = cboSearchFilter.Text;  // listBox.Text;
            List<string> QuizTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.FindReplaceFileExtension);

            if (QuizTitles == null || QuizTitles.Count < 1)
            {
                return;
            }

            string quizFilterItem = cboSearchFilter.Text; // listBox.Text;
            this.ListOfSearchTitles.Items.Clear();

            foreach (string item in QuizTitles)
            {
                if (quizFilterItem == "All" || item.StartsWith(quizFilterItem))
                {
                    this.ListOfSearchTitles.Items.Add(item);
                }
            }
        }


        // If a matching entry is found, it deletes the file.
        public static bool DeleteEntry(string pathToFile, bool confirmBeforedelete = false)
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
                this.ListOfSearchTitles.Focus();
                int itemCount = ListOfSearchTitles.Items.Count - 1;
                if (itemCount > Index)
                {
                    this.ListOfSearchTitles.SelectedIndex = Index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion // GeneralUtilityFunctions

        private void FrmGetSearches_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmGetSearchesLocation = location);
        }

        private void cboSearchFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter_SearchListBox();
            Globals.User_Settings.SearchListBoxSelection = cboSearchFilter.Text.Trim();
        }

        private void Filter_SearchListBox()
        {
            Globals.User_Settings.SearchListBoxSelection = cboSearchFilter.Text;  // listBox.Text;

            // Initialize SearchTitles with an empty list to avoid potential null reference
            List<string> SearchTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.FindReplaceFileExtension) ?? new List<string>();

            // Check if the list is empty
            if (!SearchTitles.Any())
            {
                return;
            }

            string SearchFilterItem = cboSearchFilter.Text; // listBox.Text;

            this.ListOfSearchTitles.Items.Clear();
            foreach (string item in SearchTitles)
            {
                if (SearchFilterItem == "All")
                {
                    this.ListOfSearchTitles.Items.Add(item);
                }
                else
                {
                    if (item.StartsWith(SearchFilterItem))
                    {
                        this.ListOfSearchTitles.Items.Add(item);
                    }
                }
            }
        }


    }
}
