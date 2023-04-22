using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Net.WebRequestMethods;
using System.Linq.Expressions;

namespace Tachufind
{
    public partial class FrmGetQuizes : Form
    {
		//public FrmGetQuizes(RichTextBox RTBQuestion, RichTextBox RTBAnswer)
		//KeyList<string> myKeyList = new KeyList<string>(Globals.RunningRTFDictionary);

		public FrmGetQuizes()
		{
            InitializeComponent();
        }


		private void BtnGetQuizesClose_Click(object sender, EventArgs e)
		{
             try
            {
                if (Globals.QuizNameClicked == false)
                {
                    this.Visible = false;
                    Globals.AppendStep1 = false; Globals.AppendStep2 = false;
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

                if (Globals.AppendStep1)
                {
                    List<string> QuizTitles = new List<string>();
                    // Copy OriginalRTFDictionary to AppendRTFDictionary2, this is the first step, first step starts in the btnAppendClick procedure
                    Globals.AppendRTFDictionary2 = new Dictionary<int, string>(Globals.OriginalRTFDictionary);
                    Globals.AppendStep1 = false;
                    Globals.AppendQuizName = Globals.QuizName;
                    Globals.AppendStep2 = true;
                    // Now initiating the second step . . . 
                    string heading = "  Get Review";
                    string message = "  Select the title you will add them to.";
                    GeneralFns.CustomMessageBox(heading, message, "OK");
                    this.Show();
                    this.listBoxQuizes.Items.Clear();
                    QuizTitles = GeneralFns.GetListOfFilesInDirectory(Globals.Data_Folder, Globals.QuizesFileExtension);
                    foreach (string item in QuizTitles)
                    {
                        this.listBoxQuizes.Items.Add(item);
                    }
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListBoxQuizes_Click(object sender, EventArgs e)
        {
            FileIO FIO = new FileIO();
            string fileContents = string.Empty;
            Globals.Question.Text = "";
            Globals.Answer.Text = "";
            string filePath = string.Empty;
            Globals.QuizNameClicked = true;
            Globals.RunningRTFDictionary.Clear();
            Globals.OriginalRTFDictionary.Clear();

            filePath = Globals.Data_Folder + Globals.QuizName + Globals.QuizesFileExtension;

            try
            {
                fileContents = GetFileFromListboxSelectedItem();  // Get .qdta file contents. 
                if (AdviseUser_WhenQuizEmpty(fileContents)) { return; }

                ProcessQDTAFile(fileContents);  // Fills both Globals.OriginalRTFDictionary, and Globals.RunningRTFDictionary
                Globals.RunningRTFDictionary = new Dictionary<int, string>(Globals.OriginalRTFDictionary);   // Make a DEEP copy                
                Globals.AddQuizTitle = true;
                // Alerts FrmQuiz that a quiz has been opened, so it automatically clicks the Next Button.
                Globals.QuizRetrieved = true;  // This is the only place Globals.quizRetrieved is set to true.

                if (Globals.AppendStep2)
                {
                    FrmQuiz frmQuiz = new FrmQuiz();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetQuizesDelete_Click(object sender, EventArgs e)
        {
			int x = 0;
			List<string> savedItems = null;

			try
			{
				if (this.listBoxQuizes.SelectedItem == null)
				{
					return;
				}
				// delete quiz in list and the file
				string itemToDelete = this.listBoxQuizes.SelectedItem.ToString();
				bool deleted = DeleteQuiz(itemToDelete);
				if (!deleted)
				{
					return;
				}
				savedItems = new List<string>();
				RemoveSelectedItem(this.listBoxQuizes);

				for (x = 0; x <= listBoxQuizes.Items.Count - 1; x++)
				{
					if (!string.IsNullOrEmpty(Convert.ToString(listBoxQuizes.Items[x])))
					{
						savedItems.Add(Convert.ToString(listBoxQuizes.Items[x]));
					}
				}
                Globals.RunningRTFDictionary.Clear();
                Globals.QuizDeleted = true;
                this.Show();
				this.listBoxQuizes.Focus();

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
			string filePath = Globals.Data_Folder + itemToDelete + Globals.QuizesFileExtension;

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
                this.listBoxQuizes.Focus();
                int itemCount = listBoxQuizes.Items.Count - 1;
                if (itemCount > Index)
                {
                    this.listBoxQuizes.SelectedIndex = Index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		// Get fileContents
		private string GetFileContents() {

            FileIO FIO = new FileIO();
            string fileContents = string.Empty;
            string filePath = string.Empty;

            try
            {
                filePath = Globals.Data_Folder + Globals.QuizName + Globals.QuizesFileExtension;
                if (!System.IO.File.Exists(filePath)) { return ""; } // Return if file NOT exists.
                fileContents = FIO.ReadFile(filePath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContents;
        }

		private bool AdviseUser_WhenQuizEmpty(string fileContents) {
			if (fileContents.Length < 6) {  // 1.qdta or 1.json
				string message = "* No questions in this review, add some questions. *";
				string heading = "No entries";
                GeneralFns.CustomMessageBox(heading, message, "OK");
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
                Globals.QuestionAnswer_Pair = fileContents.Split(new string[] { Globals.QUESTION_TAG }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                string message = "* No questions in this review, add some questions. *";
                string heading = "No entries";
                GeneralFns.CustomMessageBox(heading, message, "OK");
                return;
            }
        }

		// Get fileContents
		private string GetFileFromListboxSelectedItem()
		{
			string fileContents = "";

            try
            {
                if (this.listBoxQuizes.SelectedItem == null | this.listBoxQuizes.SelectedIndex < 0)
                {
                    return "";
                }
                Globals.QuizName = this.listBoxQuizes.SelectedItem.ToString();

                fileContents = GetFileContents();
                if (Globals.QuizName.Length < 1) { return ""; } // Was 6 changed to 1, this is not counting the dot or extension
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContents;
		}

        private void ProcessQDTAFile(string fileContents)
        {
            int keyCount = 0;
            int element = 0;
            Globals.OriginalRTFDictionary.Clear();
            try
            {
                fileContents = fileContents.Remove(0, Globals.QUESTION_TAG.Length); // Remove initial \n<|Q]>
                SeparateQuestionAnswer_Pairs(fileContents);
                while (Globals.QuestionAnswer_Pair.Count > 0)
                {
                    keyCount = Globals.QuestionAnswer_Pair.Count;  // total question numbers
                    element = Globals.QuestionAnswer_Pair.Count - 1;
                    Globals.OriginalRTFDictionary.Add(keyCount, Globals.QuestionAnswer_Pair.ElementAt(element));  //
                    Globals.QuestionAnswer_Pair.RemoveAt(element);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
