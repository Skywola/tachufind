using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using static GeneralFns;
using static Tachufind.FrmQuiz;
// HandlePreviewKeyDownForGlobalCurrentRTBWithFocus

namespace Tachufind
{
    public partial class FrmQuiz : Form
    {
        public FrmQuiz()
        {
            InitializeComponent();
            this.Activated += FrmQuiz_Activated;
        }
        // FrmGetQuizes frmGetQuizes = new FrmGetQuizes();
        FileIO FIO = new FileIO();
        GeneralFns genFns = new GeneralFns();
        bool RTBQuestionChanged = false;
        bool quizInProgress = true;
        KeyboardShortcuts keyboardShortcuts = new KeyboardShortcuts();


        private void HandleSimpleQuizRetrieved()
        {
            // Initialize ---->   0 = currently on a Question,  1 = currently on an Answer
            if (Globals.QuestionTracker.questionOrAnswerPosition == 1) { Globals.QuestionTracker.ToggleQuestionOrAnswerPosition(); }
            Globals.DualQAList.Clear();
            UpdateQuizNameAndCounts();
            btnNext.PerformClick();
        }

        private void HandleQuizAppendSituation()
        {  
            Globals.AppendRTFDictionary1 = new Dictionary<int, string>(Globals.OriginalRTFDictionary);  // Deep copy
            RTBQuizName.Text = Globals.QuizName;
            DoAppendAndSaveQuizToFile(Globals.AppendRTFDictionary1);
            Globals.AppendStep2 = false;
        }

        private void FrmQuiz_Activated(object sender, EventArgs e)
        {
            // Do nothing, unless a quiz has been retrieved
            if (Globals.QuizRetrieved) // Returning from getting a new quiz, initialize settings
            {
                if (Globals.AppendStep1 == false && Globals.AppendStep2 == false)
                {
                    HandleSimpleQuizRetrieved();
                }

                if (Globals.AppendStep2)
                {
                    HandleQuizAppendSituation();
                    Globals.QuestionTracker = new QuestionTracker(0);
                    InitalizeAndClearQuizValues();
                }
            }
        }

        public void DoAppendAndSaveQuizToFile(Dictionary<int, string> dict)
        {
            try
            {
                Globals.AppendRTFDictionary1 = new Dictionary<int, string>(dict);
                int count = Globals.AppendRTFDictionary1.Count + 1;
                foreach (var value in Globals.AppendRTFDictionary2.Select(x => x.Value))
                {
                    Globals.AppendRTFDictionary1.Add(count, value);
                    count++;
                }
                // necessary for saving and update.  
                // This saves the quiz under the name of the second dictionary selected
                SaveQuiz(Globals.AppendRTFDictionary1);
                string heading = "Append";
                string message = "Quiz Appended.";  // Add quiz names with appended situation explained, and that user can now remove the old quiz if they like.
                GeneralFns.CustomMessageBox(heading, message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAppend_Click(object sender, EventArgs e)
        {
            Globals.AppendStep1 = true;
            // Initiating the first step
            // Calls btnGetQuiz, which calls frmGetQuizes listBoxQuizes_Click procedure.
            string message = "  Select the title that you will remove questions from.";
            string heading = " Get Quiz";
            GeneralFns.CustomMessageBox(heading, message);
            btnGetQuiz.PerformClick();  // Get first dictionary
        }


        public string GetAndSelectLetterBehindCursor()
        {
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            string selectedText = "";
            if (selStart > 0)
            {
                Globals.Current_RTB_withFocus.Select(selStart - 1, 1);
                selectedText = Globals.Current_RTB_withFocus.SelectedText;
            }
            return selectedText;
        }

        private void BtnBold_Click(object sender, EventArgs e)
        {
            int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;

            if ((Globals.Current_RTB_withFocus.SelectionFont != null))
            {
                Globals.Current_RTB_withFocus.SelectionFont = new
                    Font(Globals.Current_RTB_withFocus.SelectionFont,
                    (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Bold));
            }
            Globals.Current_RTB_withFocus.Select(selStart, selectedLen);
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }

        private void FrmQuiz_Load(object sender, EventArgs e)
        {
            // LOCATION QUIZ FONT AT LOAD
            //Globals.quizFont = new Font("Times New Roman", Globals.quizFontSize);
            Globals.QuizFont = new Font(Globals.User_Settings.FrmQuizFontName, Globals.User_Settings.FrmQuizFontSize);
            RTBQuestion.SelectionFont = Globals.QuizFont;
            RTBAnswer.SelectionFont = Globals.QuizFont;
            this.KeyPreview = true;

            chkShuffle.Checked = Globals.User_Settings.chkShuffle;
            chkAddClear.Checked = Globals.User_Settings.chkAddClear;

            Globals.Current_RTB_withFocus = RTBQuestion;

            this.Left = Globals.User_Settings.FrmQuizLocation.X;
            this.Top = Globals.User_Settings.FrmQuizLocation.Y;
            this.Width = Globals.User_Settings.FrmQuizSize.Width;
            this.Height = Globals.User_Settings.FrmQuizSize.Height;

            Globals.FrmQuizInit = false;

            RTBQuizName.BackColor = Color.FromArgb(180, 180, 180);
            RTBQuizName.ForeColor = Color.Black;

            RTBQuestion.SelectionIndent = 2;
            RTBQuestion.SelectionRightIndent = 2;
            RTBQuestion.SelectionColor = Color.Black;

            RTBAnswer.SelectionIndent = 2;
            RTBAnswer.SelectionRightIndent = 2;
            RTBAnswer.SelectionColor = Color.Black;

            colorBtnTop01.BackColor = Globals.User_Settings.Color01;
            colorBtnTop02.BackColor = Globals.User_Settings.Color02;
            colorBtnTop03.BackColor = Globals.User_Settings.Color03;
            colorBtnTop04.BackColor = Globals.User_Settings.Color04;
            colorBtnTop05.BackColor = Globals.User_Settings.Color05;
            colorBtnBot01.BackColor = Globals.User_Settings.Color06;
            colorBtnBot02.BackColor = Globals.User_Settings.Color07;
            colorBtnBot03.BackColor = Globals.User_Settings.Color08;
            colorBtnBot04.BackColor = Globals.User_Settings.Color09;
            colorBtnBot05.BackColor = Globals.User_Settings.Color10;

            this.btnAddQuestion.TabStop = false;
            this.btnAppend.TabStop = false;
            this.BtnBlackWhite.TabStop = false;
            btnBold.TabStop = false;
            btnCapDeCap.TabStop = false;
            btnExport.TabStop = false;
            btnClearAll.TabStop = false;
            btnInvert.TabStop = false;
            btnDeleteQA.TabStop = false;
            btnGetQuiz.TabStop = false;


            btnSuperscript.TabStop = false;
            btnImage.TabStop = false;
            btnItalic.TabStop = false;
            btnCircumflex.TabStop = false;
            btnCopyright.TabStop = false;
            btnGrave.TabStop = false;
            btnSubscript.TabStop = false;
            btnNew.TabStop = false;
            btnNext.TabStop = false;
            btnRevert.TabStop = false;
            btnUnderline.TabStop = false;
            colorBtnBot01.TabStop = false;
            colorBtnBot02.TabStop = false;
            colorBtnBot03.TabStop = false;
            colorBtnBot04.TabStop = false;
            colorBtnBot05.TabStop = false;
            colorBtnTop01.TabStop = false;
            colorBtnTop02.TabStop = false;
            colorBtnTop03.TabStop = false;
            colorBtnTop04.TabStop = false;
            colorBtnTop05.TabStop = false;
            lblQuestions.TabStop = false;
            lblRemainders.TabStop = false;
            txtTTLQRemaining.TabStop = false;
            chkAddClear.TabStop = false;
            chkShuffle.TabStop = false;
        }

        private void FrmQuiz_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding();

            // Determine if text has changed in the textbox by comparing to original text.
            if (RTBQuestionChanged)
            {
                // Display a MsgBox asking the user to save changes or abort.
                if (MessageBox.Show("No changes saved, do you realy want to quit?", " Tachufind",
                   MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
            }
            Globals.User_Settings.FrmQuizFontName = this.RTBQuestion.Font.Name;
            Globals.User_Settings.FrmQuizFontSize = Convert.ToInt32(this.RTBQuestion.Font.Size.ToString());
            Point pt = new Point(this.Left, this.Top);
            Globals.User_Settings.FrmQuizLocation = pt;
            this.Visible = false;
            e.Cancel = true;
        }

        private void BtnItalic_Click(object sender, EventArgs e)
        {
            int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            if ((Globals.Current_RTB_withFocus.SelectionFont != null))
            {
                Globals.Current_RTB_withFocus.SelectionFont = new Font(
                    Globals.Current_RTB_withFocus.SelectionFont,
                    (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Italic));
            }
            Globals.Current_RTB_withFocus.Select(selStart, selectedLen);
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }

        private void BtnUnderline_Click(object sender, EventArgs e)
        {
            int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            if ((Globals.Current_RTB_withFocus.SelectionFont != null))
            {
                Globals.Current_RTB_withFocus.SelectionFont = new Font(
                    Globals.Current_RTB_withFocus.SelectionFont,
                    (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Underline));
            }
            Globals.Current_RTB_withFocus.Select(selStart, selectedLen);
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }

        private void BtnCapDeCap_Click(object sender, EventArgs e)
        {
            int selectedLen;
            int selStart;
            string check;

            try
            {
                selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
                selStart = Globals.Current_RTB_withFocus.SelectionStart;
                if (selStart < 0)
                {
                    return;
                }
                Globals.Current_RTB_withFocus.Select(selStart, 1);
                check = Globals.Current_RTB_withFocus.SelectedText;
                Globals.Current_RTB_withFocus.Select(selStart, selectedLen);
                if (Globals.Current_RTB_withFocus.SelectedText.Length > 0)
                {
                    if (char.IsUpper(Convert.ToChar(check)))
                    {
                        Globals.Current_RTB_withFocus.SelectedText = Globals.Current_RTB_withFocus.SelectedText.ToLower();
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectedText = Globals.Current_RTB_withFocus.SelectedText.ToUpper();
                    }
                }
                else
                {
                    //MsgBox("To change case, select text first.", MessageBoxButtons.OK, "To change case, select text first")
                }
                Globals.Current_RTB_withFocus.Focus();
                Globals.Current_RTB_withFocus.Select(selStart, selectedLen);
                //btnCapDeCap.Focus()
                RTBAnswer.DeselectAll();
                RTBAnswer.Refresh();
                RTBQuestion.DeselectAll();
                RTBQuestion.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImage_Click(object sender, EventArgs e)
        {
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

        private bool CheckForNoQuestionsOrAnswers()
        {
            if (RTBQuestion.Text == "" && RTBAnswer.Text == "")
            {
                string heading = "No Entry";
                string message = "Question has been deleted.";
                GeneralFns.CustomMessageBox(heading, message);
                return true;
            }
            return false;
        }

        #region DictionaryManipulationfunctions

        private void ReorderRunningRTFDictionaryKeysAscending()
        {
            Dictionary<int, string> newDictionary = new Dictionary<int, string>();

            int newKey = 1;
            foreach (var key in Globals.RunningRTFDictionary.Keys.OrderBy(k => k))
            {
                newDictionary.Add(newKey, Globals.RunningRTFDictionary[key]);
                newKey++;
            }
            Globals.RunningRTFDictionary = newDictionary;
        }

        // When a question is deleted, need to reorder keys descending
        // Renumber the remaining keys in order descending.
        private Dictionary<int, string> ReorderDictionaryKeysDescending(Dictionary<int, string> dict)
        {
            Dictionary<int, string> newDictionary = new Dictionary<int, string>();
            int newKey = dict.Count;

            try
            {
                for (int i = 1; i <= dict.Count + 1; i++)
                {
                    if (dict.ContainsKey(i))
                    {
                        newDictionary.Add(newKey, dict[i]);
                        newKey--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return newDictionary;
        }
        #endregion // DictionaryManipulationfunctions

        private bool IfTextboxesFilledModifiedButNotSaved()
        {
            bool prompt = false;
            bool textInBothTextboxes = (RTBQuestion.Text.Length > 0 && RTBAnswer.Text.Length > 0);
            bool questionOrAnswerModified = Globals.BoolRTBQuestionModified | Globals.BoolRTBAnswerModified;
            bool hasNotBeenSaved = (!Globals.SaveEditBtnClicked && !Globals.AddBtnClicked);

            if (questionOrAnswerModified)
            {
                if (textInBothTextboxes)
                {
                    if (hasNotBeenSaved)
                    {
                        prompt = true;
                    }
                }
            }
            return prompt;
        }

        private bool PromtUserToSaveQuestion()
        {
            // promt user if they want to save
            int answer = (int)MessageBox.Show("Question / Answer not saved, do you want to save it?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == (int)System.Windows.Forms.DialogResult.Yes)
            {
                btnAddQuestion.PerformClick();
                return true;
            }
            return false;
        }

        bool CheckForHitches(Dictionary<int, string> dict)
        {
            if (!Globals.QuestionSaved & IfTextboxesFilledModifiedButNotSaved())  // Prompt to save existing question
            {
                PromtUserToSaveQuestion();
            }
            bool hitches = false;
            hitches = CheckForQuizNameInNameTextBox();
            if (Globals.Shuffle)
            {
                if (dict.Count < 1) { MessageBox.Show("No questions saved.", "Empty", MessageBoxButtons.OK); }
            }
            return hitches;
        }

        private bool CheckForQuizNameInNameTextBox()
        {
            Globals.QuizName = RTBQuizName.Text;
            if (Globals.QuizName.Length < 1)
            {
                MessageBox.Show("A name must be entered in order to save content.", "Error Detected", MessageBoxButtons.OK);
                RTBQuizName.BackColor = Color.Red;
                return true;  // yep, there is a hitch
            }
            else
            {
                RTBQuizName.BackColor = Color.FromArgb(180, 180, 180);
            }
            return false;  // no hitches
        }

        // LOCATION - BACK-UP FILE GENERATION
        private void CreateBackup(string savePath, string output)
        {
            string backupPath = Globals.Data_Folder + "Current" + GetBackupExtension();  // .bak extension   .bak3, .bak2, .bak1
            FIO.WriteFile(backupPath, output, false);
            FIO.WriteFile(savePath, output, false);
        }

        // Add item to top of a List(Of String), remove any duplicate items
        private void AddNewEntryToTopOfList(string itemToAdd)
        {
            try
            {
                List<string> QuizEntries = new List<string>();
                Globals.ListOfQuizTitles = QuizEntries;
                // Remove the item from wherever it is located somewhere in the list (not top)
                if (Globals.ListOfQuizTitles.Count > 0)
                {
                    if (Globals.ListOfQuizTitles.Contains(itemToAdd))
                    {
                        Globals.ListOfQuizTitles.Remove(itemToAdd);
                    }
                }
                // Add new entry to the top of the list
                Globals.ListOfQuizTitles.Insert(0, itemToAdd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding() {
            if (Globals.ReorderDictionaryNeeded == true)
            {
                Dictionary<int, string> tempDictionary = new Dictionary<int, string>();
                tempDictionary = ReorderDictionaryKeysDescending(Globals.RunningRTFDictionary);
                Globals.RunningRTFDictionary = tempDictionary;
                Globals.ReorderDictionaryNeeded = false;
            }
        }

        // *** Most activity carried out by this function occurs in frmGetQuizes ***
        private void BtnGetQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding();
                InitalizeAndClearQuizValues();

                // For frmGetQuizes %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                List<string> QuizTitles = new List<string>();
                QuizTitles = GeneralFns.GetListOfFilesInDirectory(Globals.Data_Folder, Globals.QuizesFileExtension);
                if (CheckForNoQuizTitles(QuizTitles)) { return; }

                FrmGetQuizes frmGetQuizes = new FrmGetQuizes();
                frmGetQuizes.Show();
                if (frmGetQuizes.Created)
                {
                    frmGetQuizes.Visible = true;
                    frmGetQuizes.Show();
                    frmGetQuizes.listBoxQuizes.Items.Clear();
                    foreach (string item in QuizTitles)
                    {
                        frmGetQuizes.listBoxQuizes.Items.Add(item);
                    }
                    frmGetQuizes.Refresh();
                    return;
                }  // For frmGetQuizes %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MemoryChecker(float fileSize)
        {
            if (fileSize * 2 > 2000000000)  // Limit file size to roughly 2GB
            {
                string message = "This file size is getting too large. Consider creating a new file to prevent bad performance or errors.";
                GeneralFns.CustomMessageBox("File Size", message);
            }
        }

        private void WriteDictionaryToFile(string filePath, Dictionary<int, string> dict)
        {
            StringBuilder sb = new StringBuilder();  // Now using StringBuilder
            // This prevents the re-ordering of quesitons in reverse
            try
            {
                foreach (KeyValuePair<int, string> item in dict)
                {
                    sb.Append(Globals.QUESTION_TAG + item.Value);  // + Globals.ANSWER_TAG  Removed 2-12-23
                }
                MemoryChecker(sb.Length);  // Check to make sure user's file is not getting to large.
                File.WriteAllText(filePath, sb.ToString());
                // FIO.writeFile(savePath, output, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // LOCATION saveQuiz()
        private void SaveQuiz(Dictionary<int, string> dict)
        {
            string filePath = Globals.Data_Folder + RTBQuizName.Text + Globals.QuizesFileExtension;
            Dictionary<int, string> tempDictionary = new Dictionary<int, string>();

            try
            {
                if (CheckForHitches(dict)) { return; }  // Check for anomalies, errors.
                if (!Globals.SaveEditBtnClicked) {
                    tempDictionary = ReorderDictionaryKeysDescending(dict);
                    Globals.RunningRTFDictionary = tempDictionary;
                    Globals.SaveEditBtnClicked = false;
                }
                WriteDictionaryToFile(filePath, Globals.RunningRTFDictionary);
                AddNewEntryToTopOfList(RTBQuizName.Text);
                Globals.QuizSaved = true;
                txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // LOCATION - BACK-UP FILE GENERATION - CURRENTLY DISABLED
        //string backupPath = Globals.Data_Folder + "Current" + getBackupExtension();  // .bak extension   .bak3, .bak2, .bak1
        //FIO.writeFile(backupPath, output, false);

        string GetBackupExtension()  //  // .bak extension   .bak3, .bak2, .bak1
        {
            int count = Globals.RunningRTFDictionary.Count;
            // count = Globals.RunningQuizList.Count;
            count = count % 3;
            switch (count)
            {
                case 1:
                    return ".bak3";
                case 2:
                    return ".bak2";
                default:
                    return ".bak1";
            }

        }

        void CheckForUnsavedQuestion()
        {
            if (IfTextboxesFilledModifiedButNotSaved())
            {
                if (PromtUserToSaveQuestion())
                {
                    SaveQuiz(Globals.RunningRTFDictionary);
                }
            }
        }


        private Dictionary<int, string> UnionMergeAndReOrderTwoDictionaries(Dictionary<int, string> dict1, Dictionary<int, string> dict2)
        {
            Dictionary<int, string> mergedDict = dict1.Union(dict2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            Dictionary<int, string> outDict = new Dictionary<int, string>();
            int newKey = mergedDict.Count;
            for (int i = 1; i <= mergedDict.Count + 1; i++)
            {
                if (mergedDict.ContainsKey(i))
                {
                    outDict.Add(newKey, mergedDict[i]);
                    newKey--;
                }
            }
            return outDict;
        }

        private Dictionary<int, string> MergeAndReOrderTwoDictionaries(Dictionary<int, string> dict1, Dictionary<int, string> dict2)
        {
            Dictionary<int, string> outDict = new Dictionary<int, string>();

            int newKey = 1;
            foreach (var item in dict1)
            {
                outDict.Add(newKey, item.Value);
                newKey++;
            }

            foreach (var item in dict2)
            {
                if (!outDict.ContainsKey(item.Key))
                {
                    outDict.Add(newKey, item.Value);
                    newKey++;
                }
                else
                {
                    outDict.Add(newKey, item.Value);
                    newKey++;
                }
            }
            return outDict;
        }

        // Delete question and dec
        private void BtnDeleteQA_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> tempDictionary = new Dictionary<int, string>();

            if (Globals.RunningRTFDictionary.Count < 1) { return; }
            if (CheckForNoQuestionsOrAnswers()) { return; }  // If no questions in textboxes, can't delete
            string filePath = Globals.Data_Folder + RTBQuizName.Text + Globals.QuizesFileExtension;

            try
            {
                if (Globals.RunningRTFDictionary.ContainsKey(Globals.CurrentQueueKey))
                {
                    Globals.RunningRTFDictionary.Remove(Globals.CurrentQueueKey);
                    Globals.ReorderDictionaryNeeded = true;
                    WriteDictionaryToFile(filePath, Globals.RunningRTFDictionary);
                }
                if(Globals.QuestionTracker.questionOrAnswerPosition == 1){  // If on a question, toggle to get next question rather than getting an answer.
                    Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                }
                RTBQuestion.Text = "";
                RTBAnswer.Text = "";
                UpdateQuizNameAndCounts();
                Globals.DualQAList.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EndQuiz()
        {
            // Prevents RTBQuestion_TextChanged and RTBAnswer_TextChanged from firing when questions and answers are placed in textboxes
            quizInProgress = false;
            RTBAnswer.Text = "";
            RTBQuestion.Text = "End of review";
            if (Globals.QuizDeleted) {
                RTBQuestion.Text = "Quiz deleted.";
                Globals.QuizDeleted = false;
            }
        }

        // This is STRICTLY for getting the question, and the question, answer pair can be
        // INVERTED using the Invert button, so which item is chosen as the question
        // depends on is the button is in the invert state or not. (Globals.invert)
        private void FillQuestionTracker_QuestionAndAnswerFrom_Global_DualList()
        {
            if (Globals.Invert == true)
            {
                Globals.QuestionTracker.question = Globals.DualQAList[1];
                Globals.QuestionTracker.answer = Globals.DualQAList[0];
            }
            else
            {
                Globals.QuestionTracker.question = Globals.DualQAList[0];
                Globals.QuestionTracker.answer = Globals.DualQAList[1];
            }
            Globals.DualQAList.Clear();
            //questionTracker.currentQANumber++;
            RTBAnswer.Rtf = "";
        }

        // questionTracker.currentQuestionNumber always starts at 1, not zero, it is a key in the dictionary, not an index

        private List<int> ShuffleKeys(List<int> keys)
        {
            // Shuffle the keys using the Fisher-Yates shuffle algorithm
            Random rand = new Random();
            for (int i = keys.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                int temp = keys[j];
                keys[j] = keys[i];
                keys[i] = temp;
            }
            return keys;
        }

        // Gets the keys from the dictionary, shuffles them if shuffle required, and places them in a Queue
        private void LoadQueueKeyContainer()
        {
            try
            {
                Globals.QueueKeyContainer.Clear();
                List<int> keys = new List<int>(Globals.RunningRTFDictionary.Keys);

                if (Globals.Shuffle) // If shuffle, shuffle the queueKeyContainer
                {
                    keys = ShuffleKeys(keys);
                    Globals.QueueKeyContainer = new Queue<int>(keys);
                }
                else
                {
                    Globals.QueueKeyContainer = new Queue<int>(Globals.RunningRTFDictionary.OrderBy(x => x.Key).Select(x => x.Key));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNextQAValue()
        {
            if (Globals.RunningRTFDictionary.Count < 1) { return ""; }  // Return when empty

            try
            {
                // Load Globals.queueKeyContainer, shuffle it if required
                if (!Globals.KeyQueueLoaded)
                {
                    LoadQueueKeyContainer();
                    UpdateQuizNameAndCounts();
                    Globals.KeyQueueLoaded = true;
                }

                // Get next QA pair
                if (Globals.KeyQueueLoaded && Globals.QueueKeyContainer.Count > 0)
                {
                    string questionAnswerString = string.Empty;
                    Globals.CurrentQueueKey = Globals.QueueKeyContainer.Dequeue();
                    questionAnswerString = Globals.RunningRTFDictionary[Globals.CurrentQueueKey];
                    return questionAnswerString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }


        // Dual list is a list that holds one question and one answer
        private bool LoadDualQAList()
        {
            try
            {
                string questionAnswerString = GetNextQAValue();
                if (questionAnswerString == "") { return true; }
                Globals.DualQAList = questionAnswerString.Split(new string[] { Globals.ANSWER_TAG }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (Globals.DualQAList.Count < 2) { return true; }
                // Send QuestionTracker class one question and one answer
                FillQuestionTracker_QuestionAndAnswerFrom_Global_DualList();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                // 0 = currently on a Question,  1 = currently on an Answer
                if (Globals.QuestionTracker.questionOrAnswerPosition == 0)
                {
                    bool loadFailed = LoadDualQAList();
                    if (loadFailed) { EndQuiz(); return; }
                    RTBQuestion.Rtf = Globals.QuestionTracker.question;
                }

                // 0 = currently on a Question,  1 =  = currently on an Answer
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1)
                {
                    RTBAnswer.Rtf = Globals.QuestionTracker.answer;
                }
                UpdateQuizNameAndCounts();
                UseStandardizedFontSize();
                SetStandardBackColor();
                this.Refresh();

                // Always ToggleQuestionOrAnswerPosition();   // 0 = On a Question,  1 = On an Answer
                Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                Globals.QuestionSaved = true;
                Globals.InitializeCounts = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UseStandardizedFontSize() {
            RTBQuestion.SelectAll();
            Set_RTF_FontSize(Globals.QuizFont.Size.ToString());
            RTBAnswer.SelectAll();
            Set_RTF_FontSize(Globals.QuizFont.Size.ToString());
        }

        private void SetStandardBackColor() {
            RTBQuestion.BackColor = Globals.BackColorStd;
            RTBAnswer.BackColor = Globals.BackColorStd;
        }


        private void UpdateQuizNameAndCounts()
        {
            try
            {
                if (Globals.QuizRetrieved)
                {
                    this.RTBQuizName.Text = Globals.QuizName;
                    this.txtTTLQRemaining.Text = this.txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();
                    RTBAnswer.Text = "";
                    Globals.QuizRetrieved = false;
                    return;   // This prevents seeing a decremented count on txtTTLQRemaining as quiz loads, decrement should only happen when answer loads.
                }

                this.txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();
                // Make the Questions Remaining show as equal to the current question number on a question, but when on answer, one less than the question number.
                // 0 = currently on a Question,  1 = currently on an Answer
                if (Globals.QuestionTracker.questionOrAnswerPosition == 0)  // Currently on a question
                {
                    RTBAnswer.Text = "";
                }
                else  // Currently on an answer
                {
                    SetRemainingQuestionsDisplayBasedOnShuffleStatus();
                }
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitalizeAndClearQuizValues()
        {
            CheckForUnsavedQuestion();
            Globals.Shuffle = chkShuffle.Checked;

            RTBQuestion.Text = RTBAnswer.Text = String.Empty;
            // Initialize to 0.    Toggle questionOrAnswerPosition to 0 within the class
            if (Globals.QuestionTracker.questionOrAnswerPosition == 1) { Globals.QuestionTracker.ToggleQuestionOrAnswerPosition(); }
            Globals.DualQAList.Clear();
            this.Refresh();

            if (Globals.RevertQuiz){ return; } // Return, Do not clear when revertQuiz

            Globals.KeyQueueLoaded = false;
            Globals.RunningRTFDictionary.Clear();
            Globals.OriginalRTFDictionary.Clear();
            Globals.AppendRTFDictionary1.Clear();
            Globals.AppendRTFDictionary2.Clear();
        }

        private void SetRemainingQuestionsDisplayBasedOnShuffleStatus() {
            if (Globals.Shuffle)
            {
                this.txtTTLQRemaining.Text = Globals.QueueKeyContainer.Count.ToString();
            }
            else {
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1) {
                    this.txtTTLQRemaining.Text = Globals.QueueKeyContainer.Count.ToString();
                }
            }
        }

        private bool CheckForEmptyTextBoxs()
        {
            RTBQuestion.Refresh(); RTBAnswer.Refresh();
            if (RTBQuestion.Text.Length < 1 | RTBAnswer.Text.Length < 1)
            {
                RTBQuizName.Text = Globals.QuizName;
                MessageBox.Show("Question or answer is blank, add a question and answer.", "Error Detected", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        private void ClearTextBoxesIfRequested()
        {
            if (chkAddClear.Checked)
            {
                if (RTBQuizName.Text.Length > 1)
                {   // prevent clearing when quiz name has not been enterred
                    RTBQuestion.Text = "";
                    RTBAnswer.Text = "";
                }
            }
        }

        private void BtnRevert_Click(object sender, EventArgs e)
        {
            Globals.RevertQuiz = true;
            Globals.RunningRTFDictionary = new Dictionary<int, string>(Globals.OriginalRTFDictionary);  // Deep copy
            this.txtTTLQuestions.Text = this.txtTTLQRemaining.Text = Globals.RunningRTFDictionary.Count.ToString();
            InitalizeAndClearQuizValues();
            RTBQuestion.Text = "Quiz Reverted and loaded.";
            RTBAnswer.Text = "";
            Globals.RevertQuiz = false;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Prompt to save existing question
            if (!Globals.QuestionSaved & IfTextboxesFilledModifiedButNotSaved())
            {
                if (PromtUserToSaveQuestion())
                {
                    btnAddQuestion.PerformClick();
                    SaveQuiz(Globals.RunningRTFDictionary);
                }
            }
            Size sz = new Size(this.Width, this.Height);
            Globals.User_Settings.FrmQuizSize = sz;

            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (System.Windows.Forms.Application.OpenForms[i].Name == "FrmGetQuizes")
                    System.Windows.Forms.Application.OpenForms[i].Visible = false;
            }
            Point pt = new Point(this.Left, this.Top);
            Globals.User_Settings.FrmQuizLocation = pt;

            this.Close();
        }

        private void BtnNewQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding();

                List<string> ItemsList = new List<string>();
                List<string> QuizTitles = new List<string>();
                Globals.QuestionTracker = new QuestionTracker(0);
                InitalizeAndClearQuizValues();
                UpdateQuizNameAndCounts();
                Globals.QuizName = String.Empty;
                RTBQuestion.Text = "";
                RTBAnswer.Text = "";
                QuizTitles = GeneralFns.GetListOfFilesInDirectory(Globals.Data_Folder, Globals.QuizesFileExtension);
                foreach (string item in QuizTitles)
                {
                    ItemsList.Add(item);
                }
                if (ItemsList.Contains(RTBQuizName.Text))
                {
                    string message = "Your list of quizes already contains an item called " + RTBQuizName.Text
                                     + " if you continue, you will overwrite it!";
                    MessageBox.Show(message, "Quiz exists", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckForNoQuizTitles(List<String> QuizTitles)
        {
            if (QuizTitles.Count() < 1)
            {
                MessageBox.Show("No quizes currently exist.", "No Quizes", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        private void BtnAddQuestion_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> newDictionary = new Dictionary<int, string>();
            int key = 0;

            try
            {
                if (CheckForEmptyTextBoxs()) { return; }

                if (RTBQuizName.Text.Length < 1)
                {
                    RTBQuizName.BackColor = Color.DarkRed;
                    MessageBox.Show("Enter a quiz name.", "New Quiz", MessageBoxButtons.OK);
                }

                Globals.AddBtnClicked = true;
                string newEntry = RTBQuestion.Rtf + Globals.ANSWER_TAG + RTBAnswer.Rtf;
                newDictionary = ReorderDictionaryKeysDescending(Globals.RunningRTFDictionary);
                Globals.RunningRTFDictionary = newDictionary;
                key = Globals.RunningRTFDictionary.Count + 1;
                Globals.RunningRTFDictionary.Add(key, newEntry);  // Was Throwing error - Item with the same key already exists has been added
                UpdateQuizNameAndCounts();
                SaveQuiz(Globals.RunningRTFDictionary);
                Globals.QuestionSaved = true;
                ClearTextBoxesIfRequested();
                RTBQuestion.Focus();
                // Initialize to 0.    Toggle questionOrAnswerPosition to 0 within the class
                Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                Globals.DualQAList.Clear();
                Globals.AddBtnClicked = false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RTBQuizName_TextChanged(object sender, EventArgs e)
        {
            Globals.QuizName = RTBQuizName.Text;
        }

        private void BtnClearTitle_Click(object sender, EventArgs e)
        {
            RTBQuizName.Text = "";
        }


        private void BtnImport_Click(object sender, EventArgs e)
        {
            GeneralFns.SelectAndImportFiles(FileExtensionType.Qdta);
        }
 
        private void BtnExport_Click(object sender, EventArgs e)
        {
            GeneralFns.SelectAndExportFiles(FileExtensionType.Qdta);
        }


        private void BtnInvert_Click(object sender, EventArgs e)
        {
            Globals.Invert = !Globals.Invert;
            if (Globals.Invert)
            {
                btnInvert.Text = "Inverted";
                btnInvert.ForeColor = Color.Green;
            }
            else
            {
                btnInvert.Text = "Invert";
                btnInvert.ForeColor = Color.RoyalBlue;
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            RTBQuestion.Text = "";
            RTBAnswer.Text = "";
        }

        private void FrmQuiz_ResizeEnd(object sender, EventArgs e)
        {
            // No longer used, if needed for any reason though, this code is the same as FrmQuiz_SizeChanged
        }

        private void FrmQuiz_SizeChanged(object sender, EventArgs e)
        {
            int topControlsSpace = 75;
            int spaceBetweenControls = 2;
            const int controlPanelHeight = 128;
            int spaceAvailable = (this.Height - topControlsSpace);  // Exclude the 44 which is RTBQuestion.Top
            int verticalLocationRTBQuestion = topControlsSpace + spaceBetweenControls;
            RTBQuestion.Location = new Point(RTBAnswer.Location.X, verticalLocationRTBQuestion);
            RTBQuestion.Height = (int)((spaceAvailable - controlPanelHeight) * 0.3);

            this.ControlPanel.Location = new Point(this.ControlPanel.Location.X, RTBQuestion.Height + topControlsSpace + spaceBetweenControls);
            // int maxControlWidth = (int)(this.Width - 46); // Set the maximum control width
            int maxControlWidth = (int)Math.Floor((double)(this.Width - 46));

            int rTBAnswerHeight = this.ControlPanel.Location.Y + topControlsSpace - spaceBetweenControls;
            RTBAnswer.Location = new Point(RTBAnswer.Location.X, rTBAnswerHeight);
            RTBAnswer.Height = (int)((spaceAvailable - controlPanelHeight) * 0.7);
            RTBAnswer.Width = maxControlWidth;
            RTBQuestion.Width = maxControlWidth;
        }

        private void RTBQuestion_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBQuestion_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
            RTBQuestion.Width = this.Width - 29;  // Added 1-1-2021
        }

        private void RTBQuestion_KeyUp(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;

        }

        private void RTBAnswer_KeyUp(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBAnswer_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBAnswer_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
            //RTBAnswer.Width = this.Width - 29;  // Added 1-1-2021
        }

        private void RTBQuestion_TextChanged(object sender, EventArgs e)
        {
            if (quizInProgress != true)
            {
                Globals.BoolRTBQuestionModified = true;
            }
        }

        private void RTBAnswer_TextChanged(object sender, EventArgs e)
        {
            if (quizInProgress != true)
            {
                Globals.BoolRTBAnswerModified = true;
            }
        }

        private void ColorBtn01_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color01)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color01;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color01;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn02_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color02)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color02;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color02;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn03_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color03)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color03;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color03;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn04_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color04)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color04;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color04;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn05_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color05)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color05;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color05;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }


        private void ColorBtn06_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color06)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color06;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color06;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }


        private void ColorBtn07_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color07)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color07;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color07;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn08_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color08)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color08;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color08;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }
        private void ColorBtn09_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color09)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color09;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color09;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn10_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Globals.User_Settings.Color10)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.Color10;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Globals.User_Settings.Color10;
                // Restore backcolor requires Control Z
            }
            Globals.Current_RTB_withFocus.Select(Globals.Current_RTB_withFocus.SelectionStart, 0);
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnHighFrenchUpperCase_ae_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "Æ";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnFrenchUpperCase_OE_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "Œ";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnEuro_Click(object sender, EventArgs e)
        { // €
            Globals.Current_RTB_withFocus.SelectedText = "€";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnSharfs_Click(object sender, EventArgs e)
        {
            // ß
            Globals.Current_RTB_withFocus.SelectedText = "ß";
            Globals.Current_RTB_withFocus.Focus();
        }

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // %%%%%%
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // END ENGLISH KEYBOARD SHORTCUTS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


        private void FrmQuiz_KeyDown(object sender, KeyEventArgs e)
        {
            keyboardShortcuts.HandleFKeys(sender, e);
            keyboardShortcuts.HandleAltKeys(sender, e);
            keyboardShortcuts.HandleControlKeys(sender, e);
            keyboardShortcuts.HandleShiftKeys(sender, e);
        }

        private void FrmQuiz_KeyUp(object sender, KeyEventArgs e)
        {
            keyboardShortcuts.ShowKeyboardShortcutsPopWindowIfNeeded();

            BtnBlackWhite.BackColor = Color.Black;
        }

        private void BtnFont_Click(object sender, EventArgs e)
        {

            FontDialog fd = new FontDialog();
            fd.ShowColor = true;
            fd.ShowApply = true;
            //fd.Font = RTBQuestion.Font;
            fd.Font = new Font(Globals.User_Settings.FrmQuizFontName, Globals.User_Settings.FrmQuizFontSize);
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RTBQuestion.SelectAll();
                RTBQuestion.SelectionFont = fd.Font;
                RTBQuestion.DeselectAll();
                RTBQuestion.Refresh();
                RTBAnswer.SelectAll();
                RTBAnswer.SelectionFont = fd.Font;
                RTBAnswer.DeselectAll();
                RTBAnswer.Refresh();
                Globals.QuizFont = fd.Font;
            }
        }

        private void RTBQuizName_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuizName;
        }

        private void BtnBlack_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus.SelectionLength > 0)
            {
                int selStart = Globals.Current_RTB_withFocus.SelectionStart;
                int selLength = Globals.Current_RTB_withFocus.SelectionLength;
                Globals.Current_RTB_withFocus.SelectionColor = Color.Black;
                Globals.Current_RTB_withFocus.Select(0, Globals.Current_RTB_withFocus.TextLength);
                Globals.Current_RTB_withFocus.SelectionBackColor = Globals.BackColorStd;
                Globals.Current_RTB_withFocus.SelectionLength = 0;
                Globals.Current_RTB_withFocus.Refresh();
            }
            else
            {
                MessageBox.Show("Text must be selected to revert back to the color black.", "Error", MessageBoxButtons.OK);
            }
        }

        private void FrmQuiz_LocationChanged(object sender, EventArgs e)
        {
            if (Globals.FrmQuizInit == true) { return; }
            Point pt = new Point(this.Left, this.Top);
            Globals.User_Settings.FrmQuizLocation = pt;
        }

        private void BtnSection(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "§";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }

        public static string GetCurrentLanguage()
        {
            return InputLanguage.CurrentInputLanguage.Culture.EnglishName;
        }

        private void FrmQuiz_Deactivate(object sender, EventArgs e)
        {
            Globals.QuizName = RTBQuizName.Text;
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
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart + 1;
            Globals.Current_RTB_withFocus.SelectionLength = 0;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnSuperscript_Click(object sender, EventArgs e)
        {
            SuperScript(sender, e);
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
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
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnUmlaut_Click(object sender, EventArgs e)
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Globals.Current_RTB_withFocus.Focus();
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToUmlaut(letter);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnGrave_Click(object sender, EventArgs e)
        {
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToGrave(letter);
                Globals.Current_RTB_withFocus.Select(selStart, 0);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnAcute_Click(object sender, EventArgs e)
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToAcute(letter);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnShort_Click(object sender, EventArgs e)
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToShort(letter);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnMacron_Click(object sender, EventArgs e)
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Globals.Current_RTB_withFocus.Focus();
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToLong(letter);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnFrenchLowerCase_ae_Click(object sender, EventArgs e)
        {
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            // æ
            Globals.Current_RTB_withFocus.SelectedText = "æ";
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnFrenchLowerCase_oe_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "œ";
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnCircumflex_Click(object sender, EventArgs e)
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (letter.Length < 1)
            {
                MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Globals.Current_RTB_withFocus.Focus();
                return;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToCircumflex(letter);
            }
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnLongDash_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "—";
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }
        private void BtnFrenchQuote_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "«»";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnFancyQuote_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "“”";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnApostrophe_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "᾽";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnDegree_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "°";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnPlusMinus_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "±";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnApprox_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≈";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnSqrt_Click_1(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "\u221A";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnIntegral_Click_1(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "∫";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnIdenticalto_Click_1(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≡"; // Identical to symbol
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnDotProd_Click_1(object sender, EventArgs e)
        {  // TODO this should be made bold if possible
            Globals.Current_RTB_withFocus.SelectedText = "˙";
            int cursorLocation = Globals.Current_RTB_withFocus.SelectionStart;
            Globals.Current_RTB_withFocus.SelectionStart = cursorLocation - 1;
            Globals.Current_RTB_withFocus.SelectionLength = 1;
            Globals.Current_RTB_withFocus.SelectionFont = new Font(Globals.Current_RTB_withFocus.SelectionFont, FontStyle.Bold);
            Globals.Current_RTB_withFocus.SelectionStart = cursorLocation;
            Globals.Current_RTB_withFocus.SelectionLength = 0;
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnGte_Click_1(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≥";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnLte_Click_1(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≤";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnUnion_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "\u222A";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnIntersect_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "\u2229";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnDotR_Click(object sender, EventArgs e)
        {
            Font currentfont = Globals.Current_RTB_withFocus.Font;
            Globals.Current_RTB_withFocus.SelectionFont = new Font("Cambria", 18);  // FontStyle.Bold
            Globals.Current_RTB_withFocus.SelectedText = "⟩";
            Globals.Current_RTB_withFocus.Font = currentfont;
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnDotL_Click(object sender, EventArgs e)
        {
            Font currentfont = Globals.Current_RTB_withFocus.Font;
            Globals.Current_RTB_withFocus.SelectionFont = new Font("Cambria", 18);  // FontStyle.Bold
            Globals.Current_RTB_withFocus.SelectedText = "⟨";
            Globals.Current_RTB_withFocus.Font = currentfont;
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnOr_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "∨";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnAnd_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "∧";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnFrall_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "Ɐ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnReals_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ℝ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnComplex_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ℂ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnCopyright_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "©";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnDifferential_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ⅅ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnPartialDifferential_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "\u2202";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnEuler_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ℯ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnElement_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "\u03F5";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnNaturals_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ℕ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnIntegers_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ℤ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnInfinity_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "∞";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }
        private void BtnArrowR_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "→";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnSaveEdit_Click(object sender, EventArgs e)
        {
            if (RTBQuestion.Text == "" || RTBAnswer.Text == "")
            {
                MessageBox.Show("Missing entry in Question or Answer.", "Empty", MessageBoxButtons.OK);
                return;
            }
            DoEdit(Globals.RunningRTFDictionary);
            Globals.SaveEditBtnClicked = true;
        }

        private void DoEdit(Dictionary<int, string> dict)
        {
            if (dict.Count > 0 && dict.ContainsKey(Globals.CurrentQueueKey))  // DO THE EDIT
            {
                dict[Globals.CurrentQueueKey] = RTBQuestion.Rtf + Globals.ANSWER_TAG + RTBAnswer.Rtf;
                SaveEdit(dict);
                Globals.QuestionSaved = true;
            }
        }

        private void SaveEdit(Dictionary<int, string> dict)
        {
            Globals.SaveEditBtnClicked = true;   // Prevent save prompt message in the checkForHitches() by detecting when Save Edit button is clicked
            SaveQuiz(dict);
            Globals.QuizSaved = true;
            // Turn off the bool that detects that this Save Edit button was clieked
            Globals.SaveEditBtnClicked = false;
        }

        private void ChkAddClear_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.chkAddClear = chkAddClear.Checked;
        }

        private void ChkShuffle_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.chkShuffle = chkShuffle.Checked;
        }

        private void BtnBrackets_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "[]";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnInsertParenthesis_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "()";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void NTrema_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ñ";
            Globals.Current_RTB_withFocus.Focus();
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

        private void SpanishQuestionMark_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¿";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void SpanishExclamationMark_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¡";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnWhite_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus.SelectionLength > 0)
            {
                Globals.Current_RTB_withFocus.SelectionColor = Color.White;
            }
            else
            {
                MessageBox.Show("Text must be selected to revert back to the color white.", "Error", MessageBoxButtons.OK);
            }
        }

        private void RTBQuestion_MouseDown(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RTBQuestion;
            Globals.Current_RTB_withFocus.Focus();
            // Capture the starting position of the mouse cursor
            Globals.SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);
        }

        private void RTBQuestion_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RTBQuestion;
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

        private void RTBAnswer_MouseDown(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RTBAnswer;
            Globals.Current_RTB_withFocus.Focus();
            // Capture the starting position of the mouse cursor
            Globals.SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);
        }

        private void RTBAnswer_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RTBAnswer;
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

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected font size from the ComboBox control
            int fontSize = int.Parse(comboBoxFontSize.SelectedItem.ToString());

            // Get the current RTF code of the selected text
            RTBQuestion.SelectAll();
            string rtfCode = RTBQuestion.SelectedRtf;

            // Replace the font size value in the RTF code
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");

            // Set the modified RTF code to the selected text
            RTBQuestion.SelectedRtf = rtfCode;
            
            RTBAnswer.SelectAll();
            rtfCode = RTBAnswer.SelectedRtf;
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");
            RTBAnswer.SelectedRtf = rtfCode;
            Globals.QuizFont = RTBQuestion.Font;
        }

        private void Set_RTF_FontSize(string rtfFontSize) {
            // Get the selected font size from the ComboBox control
            int fontSize = int.Parse(rtfFontSize); 

            RTBQuestion.SelectAll();
            string rtfCode = RTBQuestion.SelectedRtf;    // Get the current RTF code of the selected text
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");  // Replace the font size value in the RTF code
            RTBQuestion.SelectedRtf = rtfCode;   // Set the modified RTF code to the selected text

            RTBAnswer.SelectAll();
            rtfCode = RTBAnswer.SelectedRtf;
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");
            RTBAnswer.SelectedRtf = rtfCode;
            Globals.QuizFont = RTBQuestion.Font;
            Globals.QuizFontSize = fontSize;

        }


    }
}
