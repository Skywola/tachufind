using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    public partial class FrmGetQuizes : Form
    {
        public FrmGetQuizes()
        {
            InitializeComponent();
        }

        private void FrmGetQuizes_Load(object sender, EventArgs e)
        {
            ScreenSetUp(this);
        }
        private void ScreenSetUp(Form form)
        {
            Point savedLocation = Globals.User_Settings.FrmGetQuizesLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        private void FrmGetQuizes_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmGetQuizesLocation = location);
        }

        private void FrmGetQuizes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.btnGetQuizesClose.PerformClick(); // Simulate button click only if user closed the form using the "X" button
            }
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmGetQuizesLocation = location);
        }


        
        private void btnGetQuizesClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (Globals.QuizNameClicked == false)
                {
                    this.Visible = false;
                    return;
                }
                Globals.QuizNameClicked = false;
                Globals.InitializeCounts = true;
                this.Visible = false;
                if (Globals.QuizRetrieved)
                {
                    // 0 = currently on a Question,  1 =  = currently on an Answer
                    Globals.DualQAList.Clear();  // Clear to refill on btnNext_PerformClick
                    Globals.KeyQueueLoaded = false;
                }
                Globals.User_Settings.quizListBoxSelection = this.CboQuizFilter.Text;

                // NOTE: AFTER THIS CLOSES, CODE PATH GOES TO FrmQuiz_Activated
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  LOCATION → → → → → ListBoxQuizes_Click
        private void ListBoxQuizes_Click(object sender, EventArgs e)
        {
            FileIO FIO = new FileIO();
            string fileContents = string.Empty;
            string filePath = string.Empty;
            Globals.QuizNameClicked = true;
            Globals.RunningRTFDictionary.Clear();
            Globals.OriginalRTFDictionary.Clear();

            filePath = AppSettings.Data_Folder + Globals.QuizName + AppSettings.QuizesFileExtension;

            try
            {
                fileContents = GetFileFromListboxSelectedItem();  // Get .qdta file contents. 
                if (AdviseUser_WhenQuizEmpty(fileContents)) { return; }

                ProcessQDTAFile(fileContents);  // Fills both Globals.OriginalRTFDictionary, and Globals.RunningRTFDictionary
                Globals.RunningRTFDictionary = new Dictionary<int, string>(Globals.OriginalRTFDictionary);   // Make a DEEP copy                
                // Alerts FrmQuiz that a quiz has been opened, so it automatically clicks the Next Button.
                Globals.QuizRetrieved = true;  // This is the only place Globals.quizRetrieved is set to true.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get fileContents
        private string GetFileFromListboxSelectedItem()
        {
            string fileContents = "";

            try
            {
                if (this.ListBoxQuizes.SelectedItem != null && this.ListBoxQuizes.SelectedIndex > -1)
                {
                    Globals.QuizName = this.ListBoxQuizes.SelectedItem?.ToString() ?? string.Empty;
                }
                else
                {
                    return "";
                }


                fileContents = GetFileContents();
                if (Globals.QuizName == null || Globals.QuizName.Length < 1) { return ""; } // Was 6 changed to 1, this is not counting the dot or extension
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContents;
        }

        private string GetFileContents()
        {
            string fileContents = string.Empty;
            string filePath = string.Empty;

            try
            {
                filePath = AppSettings.Data_Folder + Globals.QuizName + AppSettings.QuizesFileExtension;
                if (!System.IO.File.Exists(filePath)) { return ""; } // Return if file NOT exists.
                fileContents = FileIO.ReadFile(filePath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContents;
        }

        private void btnGetQuizesDelete_Click(object sender, EventArgs e)
        {
            int x = 0;
            List<string> savedItems;

            try
            {
                if (this.ListBoxQuizes.SelectedItem == null)
                {
                    return;
                }
                // delete quiz in list and the file
                string itemToDelete = this.ListBoxQuizes.SelectedItem?.ToString() ?? string.Empty;
                bool deleted = DeleteQuiz(itemToDelete);
                if (!deleted)
                {
                    return;
                }
                savedItems = new List<string>();
                RemoveSelectedItem(this.ListBoxQuizes);

                for (x = 0; x <= ListBoxQuizes.Items.Count - 1; x++)
                {
                    object listItem = ListBoxQuizes.Items[x];

                    if (listItem != null)
                    {
                        string itemString = Convert.ToString(listItem) ?? string.Empty;

                        if (!string.IsNullOrEmpty(itemString))
                        {
                            savedItems.Add(itemString);
                        }
                    }


                }
                Globals.RunningRTFDictionary.Clear();
                Globals.QuizDeleted = true;
                this.Show();
                this.ListBoxQuizes.Focus();

            }
            catch (Exception ex)
            {
                string m = "Error:" + ex.StackTrace + ". . . ." + Environment.NewLine + " Exception:  " + ex.ToString();
                MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // deleteEntry() checks for a matching entry. If a matching entry is found,
        // it removes it.  After this is complete, it deletes file.
        public bool DeleteQuiz(string itemToDelete)
        {
            string filePath = AppSettings.Data_Folder + itemToDelete + AppSettings.QuizesFileExtension;

            try
            {   // These three refs were changed from File.Exists(filePath) to System.IO.File.Exists(filePath) 1-24-2023
                if (!System.IO.File.Exists(filePath))
                {
                    return false;
                }
                int answer = (int)MessageBox.Show("Are you sure you want to delete this Quiz?", "Delete Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == (int)System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public void RemoveSelectedItem(ListBox Box)
        {
            try
            {
                int Index = Box.SelectedIndex;
                //Index of selected item
                if (Index > -1)
                {
                    Box.Items.RemoveAt(Index);
                    //Remove it
                    Box.Refresh();
                }
                this.ListBoxQuizes.Focus();
                int itemCount = ListBoxQuizes.Items.Count - 1;
                if (itemCount > Index)
                {
                    this.ListBoxQuizes.SelectedIndex = Index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AdviseUser_WhenQuizEmpty(string fileContents)
        {
            if (fileContents.Length < 6)
            {  // 1.qdta or 1.json
                string message = "* No questions in this review, add some questions. *";
                string heading = "No entries";
                MessageBox.Show(this, heading, message);
                return true;
            }
            return false;
        }

        // This takes file questions, and separates them into Globals.QA_Pairs
        private void SeparateQuestionAnswer_Pairs(string fileContents)
        {
            // Separate questions using marker
            if (fileContents.Length > 2)
            {
                Globals.QuestionAnswer_Pair = fileContents.Split(new string[] { AppConstants.QUESTION_TAG }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                string message = "* No questions in this review, add some questions. *";
                string heading = "No entries";
                MessageBox.Show(this, heading, message);
                return;
            }
        }

        private void ProcessQDTAFile(string fileContents)
        {
            Globals.OriginalRTFDictionary.Clear();

            try
            {
                // Remove initial QUESTION_TAG
                fileContents = fileContents.Remove(0, AppConstants.QUESTION_TAG.Length);

                // Split into question/answer pairs
                SeparateQuestionAnswer_Pairs(fileContents);

                int totalQuestions = Globals.QuestionAnswer_Pair.Count;

                for (int i = 0; i < totalQuestions; i++)
                {
                    // i is the stable, ascending key
                    Globals.OriginalRTFDictionary.Add(i, Globals.QuestionAnswer_Pair[i]);
                }

                // Optional: clear the temp list if you don't need it anymore
                // Globals.QuestionAnswer_Pair.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Filter_ListBox()
        {
            Globals.User_Settings.quizListBoxSelection = CboQuizFilter.Text;  // listBox.Text;

            // Initialize QuizTitles with an empty list to avoid potential null reference
            List<string> QuizTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.QuizesFileExtension) ?? new List<string>();

            // Check if the list is empty
            if (!QuizTitles.Any())
            {
                return;
            }

            string quizFilterItem = CboQuizFilter.Text; // listBox.Text;

            this.ListBoxQuizes.Items.Clear();
            foreach (string item in QuizTitles)
            {
                if (quizFilterItem == "All")
                {
                    this.ListBoxQuizes.Items.Add(item);
                }
                else
                {
                    if (item.StartsWith(quizFilterItem))
                    {
                        this.ListBoxQuizes.Items.Add(item);
                    }
                }
            }
        }


        private void ListBoxQuizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.quizListBoxSelection = CboQuizFilter.Text;  // listBox.Text;

            // Initialize QuizTitles with an empty list to avoid potential null reference
            List<string> QuizTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.QuizesFileExtension) ?? new List<string>();

            // Check if the list is empty
            if (!QuizTitles.Any())
            {
                return;
            }

            string quizFilterItem = CboQuizFilter.Text; // listBox.Text;
        }

        private void CboQuizFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter_ListBox();
            Globals.User_Settings.quizListBoxSelection = CboQuizFilter.Text;
        }
    }
}
