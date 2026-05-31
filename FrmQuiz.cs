
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Tachufind;

namespace Tachufind
{
    public partial class FrmQuiz : Form
    {
        public FrmQuiz()
        {
            InitializeComponent();
        }

        #region FrmQuizInitializeAndLoad

        public static bool ReorderDictionaryNeeded = false;
        public static int CurrentQueueKey = 0;
        public static bool Invert = false;
        bool quizInProgress = true;
        public static int SelStart = 0;

        private void FrmQuiz_Load(object sender, EventArgs e)
        {
            // LOCATION   → →  → →   QUIZ FONT AT LOAD

            try
            {
                ScreenSetUp(this);
                if (!int.TryParse(Globals.QuizFont.Size.ToString(), out int fontSize) || fontSize <= 0)
                {
                    Globals.QuizFont = new Font("Times New Roman", 22);
                }
                Globals.QuizFont = new Font(Globals.User_Settings.FrmQuizFontName, Globals.User_Settings.FrmQuizFontSize);
                FontSizeComboBox.Text = Globals.QuizFont.Size.ToString();
                RTBQuestion.Text = string.Empty;
                RTBAnswer.Text = string.Empty;
                RTBQuestion.SelectionFont = Globals.QuizFont;
                RTBAnswer.SelectionFont = Globals.QuizFont;
                this.KeyPreview = true;

                Globals.Current_RTB_withFocus = RTBQuestion;

                this.Left = Globals.User_Settings.FrmQuizLocation.X;
                this.Top = Globals.User_Settings.FrmQuizLocation.Y;
                Size sz = new Size(1473, 190);
                this.Size = sz;

                RTBQuizName.BackColor = Color.FromArgb(180, 180, 180);
                RTBQuizName.ForeColor = Color.Black;

                RTBQuestion.SelectionIndent = 2;
                RTBQuestion.SelectionRightIndent = 2;
                RTBQuestion.SelectionColor = Color.Black;

                RTBAnswer.SelectionIndent = 2;
                RTBAnswer.SelectionRightIndent = 2;
                RTBAnswer.SelectionColor = Color.Black;

                if (Globals.User_Settings.UseDefaultColors)
                {
                    colorBtn1.BackColor = ColorManager.GetDefaultColor("C1");
                    colorBtn2.BackColor = ColorManager.GetDefaultColor("C2");
                    colorBtn3.BackColor = ColorManager.GetDefaultColor("C3");
                    colorBtn4.BackColor = ColorManager.GetDefaultColor("C4");
                    colorBtn5.BackColor = ColorManager.GetDefaultColor("C5");
                    colorBtn6.BackColor = ColorManager.GetDefaultColor("C6");
                    colorBtn7.BackColor = ColorManager.GetDefaultColor("C7");
                    colorBtn8.BackColor = ColorManager.GetDefaultColor("C8");
                    colorBtn9.BackColor = ColorManager.GetDefaultColor("C9");
                    colorBtn10.BackColor = ColorManager.GetDefaultColor("C10");
                }
                else
                {
                    colorBtn1.BackColor = ColorManager.GetColorFromUserSettings(1);
                    colorBtn2.BackColor = ColorManager.GetColorFromUserSettings(2);
                    colorBtn3.BackColor = ColorManager.GetColorFromUserSettings(3);
                    colorBtn4.BackColor = ColorManager.GetColorFromUserSettings(4);
                    colorBtn5.BackColor = ColorManager.GetColorFromUserSettings(5);
                    colorBtn6.BackColor = ColorManager.GetColorFromUserSettings(6);
                    colorBtn7.BackColor = ColorManager.GetColorFromUserSettings(7);
                    colorBtn8.BackColor = ColorManager.GetColorFromUserSettings(8);
                    colorBtn9.BackColor = ColorManager.GetColorFromUserSettings(9);
                    colorBtn10.BackColor = ColorManager.GetColorFromUserSettings(10);
                }
                RTBQuestion.BackColor = Globals.User_Settings.RTBMainBackColor;
                RTBAnswer.BackColor = Globals.User_Settings.RTBMainBackColor; // Globals.BackColorStd;
                RTBQuestion.Refresh();
                RTBAnswer.Refresh();

                // Populate the FontComboBox with available font families
                PopulateFontComboBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFontComboBox()
        {
            // Get the installed fonts on the system
            System.Drawing.Text.InstalledFontCollection installedFonts = new System.Drawing.Text.InstalledFontCollection();

            // Add each font family name to the ComboBox
            foreach (System.Drawing.FontFamily fontFamily in installedFonts.Families)
            {
                FontComboBox.Items.Add(fontFamily.Name);
            }

            // Optionally, set a default selected item
            FontComboBox.SelectedIndex = 0;
            FontComboBox.Text = "Times New Roman";
        }

        private void ScreenSetUp(Form form)
        {
            Size savedSize = Globals.User_Settings.FrmQuizSize;
            Point savedLocation = Globals.User_Settings.FrmQuizLocation;
            ScreenUtility.InitializeForm(form, savedLocation, savedSize);
        }
        #endregion // FrmQuizInitializeAndLoad


        #region GeneralUtilityFunctions

        private Font GetSelectedTextFont(RichTextBox richTextBox)
        {
            if (richTextBox != null && richTextBox.SelectionFont != null)
            {
                return richTextBox.SelectionFont;
            }
            else
            {
                // Handle the case where the selection is null or there is no selected text
                MessageBox.Show("No font selected. Using default font.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new Font("Times New Roman", 20f); // Return a default font
            }
        }

        private int GetSelectedFontSize()
        {
            if (FontSizeComboBox.SelectedItem != null)
            {
                if (int.TryParse(FontSizeComboBox.SelectedItem.ToString(), out int fontSize))
                {
                    return fontSize;
                }
                else
                {
                    MessageBox.Show("Selected font size is not a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No font size selected.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return 0; // Return zero if no valid font size is found
        }

        private void DoEdit(Dictionary<int, string> dict)
        {
            if (dict.Count > 0 && dict.ContainsKey(CurrentQueueKey))  // DO THE EDIT
            {
                dict[CurrentQueueKey] = RTBQuestion.Rtf + AppConstants.ANSWER_TAG + RTBAnswer.Rtf;
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

            // Record as saved
            Globals.BoolRTBQuestionModified = false;
            Globals.BoolRTBAnswerModified = false;
        }

        private float GetFontSizeFromSelectedText()
        {
            float fontSize = 20f; // Default font size

            try
            {
                if (Globals.Current_RTB_withFocus != null && Globals.Current_RTB_withFocus.SelectionFont != null)
                {
                    return Globals.Current_RTB_withFocus.SelectionFont.Size;
                }
                else
                {
                    // Handle the case where SelectionFont is null
                    MessageBox.Show("No font selected. Using default font size.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fontSize;
        }



        private void SubScript(object sender, EventArgs e)
        {
            float fontSize = GetFontSizeFromSelectedText();

            string fontName = Globals.Current_RTB_withFocus.Font.Name;

            int selLen = Globals.Current_RTB_withFocus.SelectionLength;
            GeneralFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, -4, 12);

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

        public string GetAndSelectLetterBehindCursor()
        {
            string selectedText = "";

            try
            {
                if (Globals.Current_RTB_withFocus != null)
                {
                    int selStart = Globals.Current_RTB_withFocus.SelectionStart;
                    if (selStart > 0)
                    {
                        Globals.Current_RTB_withFocus.Select(selStart - 1, 1);
                        selectedText = Globals.Current_RTB_withFocus.SelectedText;
                    }
                }
                else
                {
                    MessageBox.Show("No text box is currently in focus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return selectedText;
        }

        private void SuperScript(object sender, EventArgs e)
        {
            try
            {
                float fontSize = 20;
                if (Globals.Current_RTB_withFocus.SelectionFont != null)
                {
                    fontSize = Globals.Current_RTB_withFocus.SelectionFont.Size;
                }
                string fontName = Globals.Current_RTB_withFocus.Font.Name;

                int selLen = Globals.Current_RTB_withFocus.SelectionLength;
                GeneralFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, 8, 12);

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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        static int GetNextAvailableKey(Dictionary<int, string> dictionary)
        {
            // Start checking from 0
            int nextKey = 0;

            // Keep incrementing the key until it's not found in the dictionary
            while (dictionary.ContainsKey(nextKey))
            {
                nextKey++;
            }

            return nextKey;
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
            // prevent clearing when quiz name has not been enterred or user does not want it cleared
            if (chkAddClear.Checked)
            {
                if (RTBQuizName.Text.Length > 1)
                {
                    RTBQuestion.Text = "";
                    RTBAnswer.Text = "";
                }
            }
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

        // Dual list is a list that holds one question and one answer
        private bool LoadDualQAList()
        {
            try
            {
                string questionAnswerString = GetNextQAValue();
                if (questionAnswerString == "") { return true; }
                Globals.DualQAList = questionAnswerString.Split(new string[] { AppConstants.ANSWER_TAG }, StringSplitOptions.RemoveEmptyEntries).ToList();
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

        // This is STRICTLY for getting the question, and the question, answer pair can be
        // INVERTED using the Invert button, so which item is chosen as the question
        // depends on is the button is in the invert state or not. (Globals.invert)
        private void FillQuestionTracker_QuestionAndAnswerFrom_Global_DualList()
        {
            try
            {
                if (Invert == true)
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
                    CurrentQueueKey = Globals.QueueKeyContainer.Dequeue();
                    if (Globals.RunningRTFDictionary.ContainsKey(CurrentQueueKey))
                    {
                        questionAnswerString = Globals.RunningRTFDictionary[CurrentQueueKey] + "";
                    }
                    return questionAnswerString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
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

        private List<int> ShuffleKeys(List<int> keys)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<int>(); // Return an empty list in case of an error
            }
        }

        private void EndQuiz()
        {
            // Prevents RTBQuestion_TextChanged and RTBAnswer_TextChanged from firing when questions and answers are placed in textboxes
            quizInProgress = false;
            RTBAnswer.Text = "";
            RTBQuestion.Text = "End of review";
            if (Globals.QuizDeleted)
            {
                RTBQuestion.Text = "Quiz deleted.";
                Globals.QuizDeleted = false;
            }
        }

        private void SetStandardBackColor()
        {
            // GeneralFns generalFns = new GeneralFns();
            // Globals.BackColorStd = generalFns.GetLoadedColor();

            RTBQuestion.BackColor = Globals.User_Settings.RTBMainBackColor;
            RTBAnswer.BackColor = Globals.User_Settings.RTBMainBackColor;
        }

        private void CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding()
        {
            if (ReorderDictionaryNeeded == true)
            {
                Dictionary<int, string> tempDictionary = new Dictionary<int, string>();
                tempDictionary = ReorderDictionaryKeysDescending(Globals.RunningRTFDictionary);
                Globals.RunningRTFDictionary = tempDictionary;
                ReorderDictionaryNeeded = false;
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

        private bool CheckForNoQuizTitles(List<String> QuizTitles)
        {
            if (QuizTitles.Count() < 1)
            {
                MessageBox.Show("No quizes currently exist.", "No Quizes", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        private void InitalizeAndClearQuizValues(bool revert = false)
        {
            try
            {
                CheckForUnsavedQuestion();
                Globals.Shuffle = chkShuffle.Checked;

                RTBQuestion.Text = RTBAnswer.Text = String.Empty;

                if (Globals.QuestionTracker.questionOrAnswerPosition == 1) { Globals.QuestionTracker.ToggleQuestionOrAnswerPosition(); }
                Globals.DualQAList.Clear();
                this.Refresh();

                if (revert) { return; } // Return, Do not clear when revertQuiz

                Globals.KeyQueueLoaded = false;
                if (!Globals.GetAppendFile)
                {
                    Globals.RunningRTFDictionary.Clear();
                    Globals.OriginalRTFDictionary.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CheckForUnsavedQuestion()
        {
            try
            {
                if (IfTextboxesFilledModifiedButNotSaved())
                {
                    if (PromptUserToSaveQuestion())
                    {
                        SaveQuiz(Globals.RunningRTFDictionary);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool PromptUserToSaveQuestion()
        {
            // Prompt user if they want to save
            int answer = (int)MessageBox.Show("Question / Answer not saved, do you want to save it?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == (int)System.Windows.Forms.DialogResult.Yes)
            {
                // Perform the save action by simulating a button click
                btnAddQuestion.PerformClick();
                // Record as saved
                Globals.BoolRTBQuestionModified = false;
                Globals.BoolRTBAnswerModified = false;
                return true;
            }

            return false;
        }

        private bool IfTextboxesFilledModifiedButNotSaved()
        {
            try
            {
                bool prompt = false;
                bool textInBothTextboxes = (RTBQuestion.Text.Length > 0 & RTBAnswer.Text.Length > 0);
                Globals.questionOrAnswerModified = Globals.BoolRTBQuestionModified || Globals.BoolRTBAnswerModified;

                if (Globals.questionOrAnswerModified && textInBothTextboxes)
                {
                    prompt = true;
                }

                return prompt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        // LOCATION   → →  → →   saveQuiz()
        private void SaveQuiz(Dictionary<int, string> dict)
        {
            string filePath = AppSettings.Data_Folder + RTBQuizName.Text + AppSettings.QuizesFileExtension;
            //Dictionary<int, string> tempDictionary = new Dictionary<int, string>();

            try
            {
                // Check for anomalies, errors
                if (CheckForHitches(dict)) { return; }

                // Reorder dictionary if the save/edit button was not clicked
                if (!Globals.SaveEditBtnClicked)
                {
                    Globals.RunningRTFDictionary = ReorderDictionaryKeysDescending(dict);
                    Globals.SaveEditBtnClicked = false;
                }

                // Write the dictionary to a file
                WriteDictionaryToFile(filePath, Globals.RunningRTFDictionary);

                // Add the new entry to the top of the list
                AddNewEntryToTopOfList(RTBQuizName.Text);

                // Update global state and UI
                Globals.QuizSaved = true;
                // Record as saved
                Globals.BoolRTBQuestionModified = false;
                Globals.BoolRTBAnswerModified = false;
                txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reorder the remaining keys in descending order.
        private static Dictionary<int, string> ReorderDictionaryKeysDescending(Dictionary<int, string> dict)
        {
            try
            {
                int newKey = dict.Count;
                var newDictionary = dict.Keys.OrderBy(k => k)
                                             .Where(key => dict.ContainsKey(key))
                                             .ToDictionary(key => newKey--, key => dict[key]);

                return newDictionary;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<int, string>();  // This is already optimal for returning an empty dictionary
            }
        }

        private void WriteDictionaryToFile(string filePath, Dictionary<int, string> dict)
        {
            var sb = new StringBuilder();  // Using StringBuilder for efficient string concatenation

            try
            {
                // Append each dictionary item to the StringBuilder
                foreach (KeyValuePair<int, string> item in dict)
                {
                    sb.Append(AppConstants.QUESTION_TAG + item.Value);  // Removed ANSWER_TAG on 2-12-23
                }

                // Check memory usage before writing to file
                MemoryChecker(sb.Length);

                // Write the content to the specified file path
                File.WriteAllText(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add item to top of a List(Of String), remove any duplicate items
        private static void AddNewEntryToTopOfList(string itemToAdd)
        {
            try
            {
                // Ensure the list is initialized
                Globals.ListOfQuizTitles ??= new List<string>();

                // Remove the item if it already exists in the list
                Globals.ListOfQuizTitles.Remove(itemToAdd);

                // Add the new entry to the top of the list
                Globals.ListOfQuizTitles.Insert(0, itemToAdd);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckForHitches(Dictionary<int, string> dict)
        {
            try
            {
                // Prompt to save existing question if necessary
                if (!Globals.QuestionSaved && IfTextboxesFilledModifiedButNotSaved())
                {
                    if (Globals.SaveEditBtnClicked == false)
                        PromptUserToSaveQuestion();
                }
                Globals.SaveEditBtnClicked = false; // reset, this changes to true only when SaveEdit button is clicked.
                // Check for quiz name in the name text box
                bool hitches = CheckForQuizNameInNameTextBox();

                // Check if shuffle is enabled and the dictionary is empty
                if (Globals.Shuffle && dict.Count < 1)
                {
                    MessageBox.Show("No questions saved.", "Empty", MessageBoxButtons.OK);
                }

                return hitches;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private static void MemoryChecker(float fileSize)
        {
            if (fileSize * 2 > 2000000000)  // Limit file size to roughly 2GB
            {
                string message = "This file size is getting too large. Consider creating a new file to prevent bad performance or errors.";
                GeneralFns.CustomMessageBox("File Size", message);
            }
        }

        private bool CheckForQuizNameInNameTextBox()
        {
            try
            {
                // Set the global quiz name to the current text in RTBQuizName
                Globals.QuizName = RTBQuizName.Text;

                // Check if the quiz name is empty
                if (Globals.QuizName.Length < 1)
                {
                    // Show an error message and highlight the text box in red
                    MessageBox.Show("A name must be entered in order to save content.", "Error Detected", MessageBoxButtons.OK);
                    RTBQuizName.BackColor = Color.Red;
                    return true;  // Yep, there is a hitch
                }
                else
                {
                    // Reset the text box background color to default
                    RTBQuizName.BackColor = Color.FromArgb(180, 180, 180);
                }
                return false;  // No hitches
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;  // Indicate an error occurred
            }
        }


        private void HandleSimpleQuizRetrieved()
        {
            try
            {
                // Initialize to 0 if currently on an Answer (1)
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1)
                {
                    Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                }

                // Clear the dual question-answer list
                Globals.DualQAList.Clear();

                // Update the quiz name and counts
                UpdateQuizNameAndCounts();

                // Simulate a click on the Next button
                btnNext.PerformClick();
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateQuizNameAndCounts()
        {
            try
            {
                // If a quiz has been retrieved
                if (Globals.QuizRetrieved)
                {
                    // Update quiz name and counts
                    this.RTBQuizName.Text = Globals.QuizName;
                    this.txtTTLQRemaining.Text = this.txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();
                    RTBAnswer.Text = "";
                    Globals.QuizRetrieved = false;
                    return;  // Prevent seeing a decremented count on txtTTLQRemaining as quiz loads
                }

                // Update total questions count
                this.txtTTLQuestions.Text = Globals.RunningRTFDictionary.Count.ToString();

                // Handle display based on current position
                if (Globals.QuestionTracker.questionOrAnswerPosition == 0)  // Currently on a question
                {
                    if (chkAddClear.Checked)
                        RTBAnswer.Text = "";
                }
                else  // Currently on an answer
                {
                    SetRemainingQuestionsDisplayBasedOnShuffleStatus();
                }

                // Refresh the form
                this.Refresh();
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetRemainingQuestionsDisplayBasedOnShuffleStatus()
        {
            // Check if shuffle is enabled
            if (Globals.Shuffle)
            {
                // Set the remaining questions count to the count of QueueKeyContainer
                this.txtTTLQRemaining.Text = Globals.QueueKeyContainer.Count.ToString();
            }
            else
            {
                // If currently on an answer (1), set the remaining questions count to the count of QueueKeyContainer
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1)
                {
                    this.txtTTLQRemaining.Text = Globals.QueueKeyContainer.Count.ToString();
                }
            }
        }
        #endregion //  GeneralUtilityFunctions


        #region Buttons__Click

        private void BtnClearTitle_Click(object sender, EventArgs e)
        {
            RTBQuizName.Text = "";
        }

        private void BtnRemHighlight_Click(object sender, EventArgs e)
        {
            RemoveHighlighting(RTBQuestion);
            RemoveHighlighting(RTBAnswer);
        }

        public static void RemoveHighlighting(RichTextBox richTextBox)
        {
            if (richTextBox == null)
            {
                return; // Simply return if richTextBox is null
            }

            // Backup the current selection
            int originalSelectionStart = richTextBox.SelectionStart;
            int originalSelectionLength = richTextBox.SelectionLength;

            // Select all text
            richTextBox.SelectAll();

            // Set the background color to the RichTextBox's default back color (or any specific color)
            richTextBox.SelectionBackColor = Globals.User_Settings.RTBMainBackColor; // Or use richTextBox.BackColor

            // Restore the original selection
            richTextBox.Select(originalSelectionStart, originalSelectionLength);
        }


        private void BtnClearTop_Click(object sender, EventArgs e)
        {
            RTBQuestion.Text = string.Empty;
        }

        private void BtnClearBottom_Click(object sender, EventArgs e)
        {
            RTBAnswer.Text = string.Empty;
        }

        private void RTBQuestion_MouseDown(object sender, MouseEventArgs e)
        {
            SelStart = 0;
            RTBQuestion.Focus();
            // Capture the starting position of the mouse cursor
            SelStart = RTBQuestion.GetCharIndexFromPosition(e.Location);
        }

        private void RTBQuestion_MouseUp(object sender, MouseEventArgs e)
        {
            //Globals.Current_RTB_withFocus = this.RTBQuestion;
            RTBQuestion.Focus();

            // Calculate the ending position of the selection
            int selEnd = RTBQuestion.GetCharIndexFromPosition(e.Location);

            // If the starting position and the ending position are the same, return without selecting anything
            if (RTBQuestion.SelectionLength < 1) { return; }

            // Determine the selection start and length based on the relative positions of the starting and ending points
            int selectionStart = Math.Min(SelStart, selEnd);
            int selectionLength = Math.Abs(SelStart - selEnd);

            // Set the selection start and length
            RTBQuestion.SelectionStart = selectionStart;
            RTBQuestion.SelectionLength = selectionLength;

            // Set the focus back to the control to keep the selection highlighted
            RTBQuestion.Focus();
        }

        private void RTBAnswer_MouseDown(object sender, MouseEventArgs e)
        {
            SelStart = 0;
            RTBAnswer.Focus();
            // Capture the starting position of the mouse cursor
            SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);
        }

        private void RTBAnswer_MouseUp(object sender, MouseEventArgs e)
        {
            RTBAnswer.Focus();

            int selEnd = RTBAnswer.GetCharIndexFromPosition(e.Location);

            // If the starting position and the ending position are the same, return without selecting anything
            if (RTBAnswer.SelectionLength < 1) { return; }

            // Determine the selection start and length based on the relative positions of the starting and ending points
            int selectionStart = Math.Min(SelStart, selEnd);
            int selectionLength = Math.Abs(SelStart - selEnd);

            // Set the selection start and length
            //Globals.Current_RTB_withFocus = this.RTBAnswer;
            RTBAnswer.SelectionStart = selectionStart;
            RTBAnswer.SelectionLength = selectionLength;

            // Set the focus back to the control to keep the selection highlighted
            RTBAnswer.Focus();
        }

        private void BtnSpQuestionMk_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¿";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnSpExclaim_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¡";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void FrmMainBtnWhite_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus.SelectionLength < 1)
            {
                MessageBox.Show("Text must be selected to change a color.", "Error", MessageBoxButtons.OK);
            }

            if (Control.ModifierKeys == Keys.Shift)  // Text is selected
            {
                if (Globals.Current_RTB_withFocus.SelectionBackColor == Color.White)
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionBackColor = Color.White;
                }
            }
            else
            {
                Globals.Current_RTB_withFocus.SelectionColor = Color.White;
            }
            int cursor = Globals.Current_RTB_withFocus.SelectionStart;
            Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
            Globals.Current_RTB_withFocus.Focus();
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

        private void BtnSpanishNye_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ñ";
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

        private void BtnUmlaut_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGrave_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAcute_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnShort_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMacron_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void BtnCedilla_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ç";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnLongLine_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "—";
            Globals.Current_RTB_withFocus.Focus();
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }

        private void BtnInsertFrenchQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "«»";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnInsertFancyQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "“”";
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnSingleQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ʽʼ";
            Globals.Current_RTB_withFocus.SelectionStart -= 1; // Move cursor between brackets
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

        private void BtnSqrt_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≈";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnIntegral_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "∫";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnIdenticalto_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≡"; // Identical to symbol
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnDotProd_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "˙";
            int cursorLocation = Globals.Current_RTB_withFocus.SelectionStart;
            Globals.Current_RTB_withFocus.SelectionStart = cursorLocation - 1;
            Globals.Current_RTB_withFocus.SelectionLength = 1;
            if (Globals.Current_RTB_withFocus.SelectionFont != null)
            {
                Globals.Current_RTB_withFocus.SelectionFont = new Font(Globals.Current_RTB_withFocus.SelectionFont, FontStyle.Bold);
            }
            Globals.Current_RTB_withFocus.SelectionStart = cursorLocation;
            Globals.Current_RTB_withFocus.SelectionLength = 0;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnGte_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "≥";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnLte_Click(object sender, EventArgs e)
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
            Globals.Current_RTB_withFocus.SelectedText = "⟨⟩";
            Globals.Current_RTB_withFocus.Font = currentfont;
            Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart - 1;
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

        private void BtnForall_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "Ɐ";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
            Globals.Current_RTB_withFocus.Focus();
        }

        private void ColorBtn1_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn1.BackColor = ColorManager.GetColor("C1");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C1"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C1");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C1");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn2_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn2.BackColor = ColorManager.GetColor("C2");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C2"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C2");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C2");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn3_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn3.BackColor = ColorManager.GetColor("C3");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C3"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C3");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C3");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn4_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn4.BackColor = ColorManager.GetColor("C4");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C4"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C4");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C4");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn5_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn5.BackColor = ColorManager.GetColor("C5");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C5"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C5");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C5");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn6_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn6.BackColor = ColorManager.GetColor("C6");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C6"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C6");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C6");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn7_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn7.BackColor = ColorManager.GetColor("C7");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C7"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C7");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C7");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn8_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn8.BackColor = ColorManager.GetColor("C8");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C8"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C8");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C8");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn9_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn9.BackColor = ColorManager.GetColor("C9");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C9"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C9");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C9");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorBtn10_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorBtn10.BackColor = ColorManager.GetColor("C10");  // Globals.User_Settings.Color01;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == ColorManager.GetColor("C10"))  // Globals.User_Settings.Color01)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = ColorManager.GetColor("C10");
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = ColorManager.GetColor("C10");
                    // Restore backcolor requires Control Z
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmQuizBtnBlack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Globals.Current_RTB_withFocus.SelectionLength < 1)
                {
                    MessageBox.Show("Text must be selected to change a color.", "Error", MessageBoxButtons.OK);
                }

                if (Control.ModifierKeys == Keys.Shift)  // Text is selected
                {
                    if (Globals.Current_RTB_withFocus.SelectionBackColor == Color.Black)
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
                    }
                    else
                    {
                        Globals.Current_RTB_withFocus.SelectionBackColor = Color.Black;
                    }
                }
                else
                {
                    Globals.Current_RTB_withFocus.SelectionColor = Color.Black;
                }
                int cursor = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(cursor + Globals.Current_RTB_withFocus.SelectionLength, 0);
                Globals.Current_RTB_withFocus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTsus_Click(object sender, EventArgs e)
        {
            // ß
            Globals.Current_RTB_withFocus.SelectedText = "ß";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void RTBAnswer_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBQuestion_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
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
            Invert = !Invert;
            if (Invert)
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

        private void BtnSuperscript_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus != null && Globals.Current_RTB_withFocus.SelectedText.Length > 0 && Globals.Current_RTB_withFocus.SelectionFont != null)
            {
                float fontSize = Globals.Current_RTB_withFocus.SelectionFont.Size;
                string fontName = Globals.Current_RTB_withFocus.SelectionFont.Name;

                int selLen = Globals.Current_RTB_withFocus.SelectionLength;

                // Apply superscript formatting
                GeneralFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, 8, 12);

                // Move the cursor to the end of the selected text
                Globals.Current_RTB_withFocus.SelectionStart += selLen;
                Globals.Current_RTB_withFocus.SelectedText = " ";  // Add a space

                // Position the cursor at the space
                int selStart = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(selStart - 1, 1);

                // Reset the font and char offset to normal
                Globals.Current_RTB_withFocus.SelectionCharOffset = 0;
                Globals.Current_RTB_withFocus.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);

                // Ensure the selection is collapsed and the cursor is placed correctly
                Globals.Current_RTB_withFocus.SelectionStart = selStart;
                Globals.Current_RTB_withFocus.SelectionLength = 0;
                Globals.Current_RTB_withFocus.Focus();
            }
        }

        private void BtnSection_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "§";
            RTBAnswer.DeselectAll();
            RTBAnswer.Refresh();
            RTBQuestion.DeselectAll();
            RTBQuestion.Refresh();
        }


        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            RTBQuestion.Text = "";
            RTBAnswer.Text = "";
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
                // Globals.RunningRTFDictionary.Count = 0;
                Globals.RunningRTFDictionary.Clear();
                Globals.RunningRTFDictionary = new Dictionary<int, string>();
                txtTTLQRemaining.Text = "0";
                QuizTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.QuizesFileExtension);
                foreach (string item in QuizTitles)
                {
                    ItemsList.Add(item);
                }
                if (ItemsList.Contains(RTBQuizName.Text))
                {
                    string heading = " Create New Quiz";
                    string message = "Quiz " + RTBQuizName.Text +
                        " already exists, if you continue, you will overwrite it!  Change the title before adding a new question.";
                    GeneralFns.CustomMessageBox(heading, message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSubscript_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus != null && Globals.Current_RTB_withFocus.SelectedText.Length > 0 && Globals.Current_RTB_withFocus.SelectionFont != null)
            {
                float fontSize = Globals.Current_RTB_withFocus.SelectionFont.Size;
                string fontName = Globals.Current_RTB_withFocus.Font.Name;

                int selLen = Globals.Current_RTB_withFocus.SelectionLength;

                // Apply subscript formatting
                GeneralFns.CreateSuperorSubScript(Globals.Current_RTB_withFocus, -4, 12);

                // Move the cursor to the end of the selected text
                Globals.Current_RTB_withFocus.SelectionStart += selLen;
                Globals.Current_RTB_withFocus.SelectedText = " ";  // Add a space

                // Position the cursor at the space
                int selStart = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.Select(selStart - 1, 1);

                // Reset the font and char offset to normal
                Globals.Current_RTB_withFocus.SelectionCharOffset = 0;
                Globals.Current_RTB_withFocus.SelectionFont = new Font(fontName, fontSize, FontStyle.Regular);

                // Ensure the selection is collapsed and the cursor is placed correctly
                Globals.Current_RTB_withFocus.SelectionStart = selStart;
                Globals.Current_RTB_withFocus.SelectionLength = 0;
                Globals.Current_RTB_withFocus.Focus();
            }
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

                Globals.BoolRTBQuestionModified = false;
                Globals.BoolRTBAnswerModified = false;

                string newEntry = RTBQuestion.Rtf + AppConstants.ANSWER_TAG + RTBAnswer.Rtf;
                newDictionary = ReorderDictionaryKeysDescending(Globals.RunningRTFDictionary);
                Globals.RunningRTFDictionary = newDictionary;
                key = Globals.RunningRTFDictionary.Count + 1;
                if (!Globals.RunningRTFDictionary.ContainsKey(key))
                {
                    Globals.RunningRTFDictionary.Add(key, newEntry);
                }
                else
                {
                    Globals.RunningRTFDictionary.Add(GetNextAvailableKey(Globals.RunningRTFDictionary), newEntry);
                }
                UpdateQuizNameAndCounts();
                SaveQuiz(Globals.RunningRTFDictionary);
                Globals.QuestionSaved = true;
                ClearTextBoxesIfRequested();
                RTBQuestion.Focus();
                // Initialize to 0.    Toggle questionOrAnswerPosition to 0 within the class
                Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                Globals.DualQAList.Clear();
                //}
                txtTTLQRemaining.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRevert_Click(object sender, EventArgs e)
        {
            Globals.RunningRTFDictionary = new Dictionary<int, string>(Globals.OriginalRTFDictionary);  // Deep copy
            this.txtTTLQuestions.Text = this.txtTTLQRemaining.Text = Globals.RunningRTFDictionary.Count.ToString();
            InitalizeAndClearQuizValues(true);
            Globals.KeyQueueLoaded = false;
            Globals.QuestionTracker.questionOrAnswerPosition = 0;
            RTBAnswer.Text = "";
            btnNext.PerformClick();
        }

        private void EnsureFontSizeIsConstant()
        {
            // Ensure font size is constant
            int fontSize = Globals.User_Settings.FrmQuizFontSize;
            // Get the current RTF code of the selected text
            RTBQuestion.SelectAll();
            string rtfCode = RTBQuestion.SelectedRtf;
            // Replace the font size value in the RTF code
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");
            // Set the modified RTF code to the selected text
            RTBQuestion.SelectedRtf = rtfCode;

            RTBAnswer.SelectAll();
            rtfCode = RTBAnswer.SelectedRtf;
            // Replace the font size value in the RTF code
            rtfCode = Regex.Replace(rtfCode, @"\\fs\d+", $"\\fs{fontSize * 2}");
            // Set the modified RTF code to the selected text
            RTBAnswer.SelectedRtf = rtfCode;
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
                    RTBQuestion.Refresh();
                }

                // 0 = currently on a Question,  1 =  = currently on an Answer
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1)
                {
                    RTBAnswer.Rtf = Globals.QuestionTracker.answer;
                }
                UpdateQuizNameAndCounts();
                //Change_RTF_Font_Using_Tags(Globals.StandardFont);
                SetStandardBackColor();
                //this.Refresh();
                EnsureFontSizeIsConstant();

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

        private void BtnDeleteQuestion_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> tempDictionary = new Dictionary<int, string>();

            if (Globals.RunningRTFDictionary.Count < 1) { return; }
            if (CheckForNoQuestionsOrAnswers()) { return; }  // If no questions in textboxes, can't delete
            string filePath = AppSettings.Data_Folder + RTBQuizName.Text + AppSettings.QuizesFileExtension;

            try
            {
                if (Globals.RunningRTFDictionary.ContainsKey(CurrentQueueKey))
                {
                    Globals.RunningRTFDictionary.Remove(CurrentQueueKey);
                    ReorderDictionaryNeeded = true;
                    WriteDictionaryToFile(filePath, Globals.RunningRTFDictionary);
                }
                if (Globals.QuestionTracker.questionOrAnswerPosition == 1)
                {  // If on a question, toggle to get next question rather than getting an answer.
                    Globals.QuestionTracker.ToggleQuestionOrAnswerPosition();
                }
                RTBQuestion.Text = "";
                RTBAnswer.Text = "";   // Below line changed 6-24-2025
                txtTTLQRemaining.Text = Globals.RunningRTFDictionary.Count.ToString();  // Globals.QueueKeyContainer.Count.ToString(); // Globals.RunningRTFDictionary.Count().ToString();
                UpdateQuizNameAndCounts();
                Globals.DualQAList.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAppend_Click(object sender, EventArgs e)
        {
            // Initiating the first step
            // Calls btnGetQuiz, which calls frmGetQuizes listBoxQuizes_Click procedure.
            string heading = " Append Quizes";
            string message = " Have you opened the quiz you want to add the questions to?" +
                "\n If not, click No and open it, then select the Append button.)";
            string[] buttons = { "Yes", "No", "Cancel" };
            DialogResult result = GeneralFns.CustomMessageBox(heading, message, buttons);
            switch (result)
            {
                case DialogResult.Yes:
                    Globals.GetAppendFile = true;
                    Globals.AppendRTFDictionary.Clear();
                    // Perform a deep copy of the original dictionary
                    Globals.AppendRTFDictionary = new Dictionary<int, string>(Globals.OriginalRTFDictionary);
                    btnGetQuiz.PerformClick();  // Get append dictionary
                    break;
                case DialogResult.No:

                    break;
                case DialogResult.Cancel:
                    break;
            }
        }


        private void BtnGetQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIfDictionaryReorderAndSaveNeededBeforeDiscarding();
                InitalizeAndClearQuizValues();

                // For frmGetQuizes %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                List<string> QuizTitles = new List<string>();
                QuizTitles = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.QuizesFileExtension);
                if (CheckForNoQuizTitles(QuizTitles)) { return; }

                // This reads the setting so QuizTitles can be filtered based on the selection in the selection Listbox in FrmGetQuizes
                string quizFilterItem = Globals.User_Settings.quizListBoxSelection;

                FrmGetQuizes frmGetQuizes = new FrmGetQuizes();
                frmGetQuizes.Show();
                if (frmGetQuizes.Created)
                {
                    frmGetQuizes.Visible = true;
                    frmGetQuizes.ListBoxQuizes.Items.Clear();
                    frmGetQuizes.CboQuizFilter.Text = Globals.User_Settings.quizListBoxSelection;
                    foreach (string item in QuizTitles)
                    {
                        if (!frmGetQuizes.ListBoxQuizes.Items.Contains(item))
                        {
                            if (quizFilterItem == "All")
                            {
                                frmGetQuizes.ListBoxQuizes.Items.Add(item);
                            }
                            else
                            {
                                if (item.StartsWith(quizFilterItem))
                                {
                                    frmGetQuizes.ListBoxQuizes.Items.Add(item);
                                }
                            }
                        }
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

        private void BtnBold_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnItalic_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUnderline_Click(object sender, EventArgs e)
        {
            try
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

        private void BtnImageInsert_Click(object sender, EventArgs e)
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
                System.Drawing.Image img;
                img = System.Drawing.Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                System.Windows.Forms.DataFormats.Format df;
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
        #endregion //  Buttons__Click


        #region Events

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedFontFamily = string.Empty;
                if (FontSizeComboBox?.SelectedItem?.ToString() != null)
                {
                    selectedFontFamily = FontSizeComboBox.SelectedItem?.ToString() ?? string.Empty;
                }

                // Apply the selected font family to the selected text in a RichTextBox
                RTBQuestion.SelectAll();

                Font currentFont = GetSelectedTextFont(RTBQuestion);
                if (currentFont != null)
                {
                    GeneralFns generalFns = new GeneralFns();
                    RTBQuestion.Rtf = GeneralFns.Set_RtfFontFamily(selectedFontFamily, RTBQuestion);
                    RTBAnswer.Rtf = GeneralFns.Set_RtfFontFamily(selectedFontFamily, RTBAnswer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected font size from the ComboBox control
                int fontSize = GetSelectedFontSize();
                if (fontSize < 1) { return; }
                Globals.User_Settings.FrmQuizFontSize = Convert.ToInt32(FontSizeComboBox.Text);

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
                RTBQuestion.BackColor = Globals.User_Settings.RTBMainBackColor;
                RTBAnswer.BackColor = Globals.User_Settings.RTBMainBackColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkShuffle_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.chkShuffle = chkShuffle.Checked;
        }

        private void FrmQuiz_Deactivate(object sender, EventArgs e)
        {
            Globals.QuizName = RTBQuizName.Text;
        }

        private void FrmQuiz_LocationChanged(object sender, EventArgs e)
        {
            if (Globals.FrmQuizInit)
            {
                Globals.FrmQuizInit = false;
                return;
            }
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmQuizLocation = location);
        }


        private void RTBQuizName_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuizName;
        }


        private void FrmQuiz_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardShortcuts.ShowKeyboardShortcutsPopWindowIfNeeded();

            // BtnBlackWhite.BackColor = Color.Black;
        }

        private void RTBAnswer_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }


        private void EnforceFontSize()
        {
            int fontSize = Convert.ToInt32(FontSizeComboBox.Text);
            RTBQuestion.SelectAll();
            RTBAnswer.SelectAll();
            RTBQuestion.Font = new Font(RTBQuestion.Font.FontFamily, fontSize);
            RTBAnswer.Font = new Font(RTBAnswer.Font.FontFamily, fontSize);
            RTBQuestion.DeselectAll();
            RTBAnswer.DeselectAll();
        }



        private void RTBQuestion_TextChanged(object sender, EventArgs e)
        {
            if (quizInProgress != true)
            {
                Globals.BoolRTBQuestionModified = true;
            }
            if (ChkEnforceFontSize.Checked)
            {
                //EnforceFontSize();
                ApplyFontSettings(RTBQuestion);
            }
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

            // Handle keyboard shortcuts using RTBQuestion directly
            KeyboardShortcuts.HandleControlKeys(RTBQuestion, e);
        }


        private void RTBQuestion_KeyUp(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBQuestion;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;
        }

        private void RTBAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            Globals.Current_RTB_withFocus = RTBAnswer;
            Globals.QuestionSaved = false;
            Globals.QuizSaved = false;

            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            KeyboardShortcuts.HandleControlKeys(RTBAnswer, e);
        }

        private void RTBAnswer_TextChanged(object sender, EventArgs e)
        {
            if (quizInProgress != true)
            {
                Globals.BoolRTBAnswerModified = true;
            }
        }

        private void ApplyFontSettings(RichTextBox RTB)
        {
            // Get the current font size
            float fontSize = Convert.ToInt32(FontSizeComboBox.Text);

            // Apply the new font size to the entire text
            string rtf = string.Empty;

            if (RTB.Rtf != null && RTB.Rtf != string.Empty)
            {
                // Store the text to be modified in rtf
                rtf = RTB.Rtf;
            }
            else
            {
                return;
            }

            // Convert font size from points to half-points (RTF uses half-points)
            int halfPoints = (int)(fontSize * 2);

            // Create a pattern to find the font size tags in the RTF
            string pattern = @"\\fs\d+";

            // Use regular expression to replace the font size tags
            string updatedRtf = System.Text.RegularExpressions.Regex.Replace(rtf, pattern, $"\\fs{halfPoints}");

            // Set the updated RTF back to the RichTextBox
            RTB.Rtf = updatedRtf;
        }

        private void RTBAnswer_KeyUp(object sender, KeyEventArgs e)
        {
            // Store to prevent cursor position from being modified
            if (RTBAnswer.Text.Length > 0)
            {
                AppSettings.testvariable = RTBAnswer.SelectionStart;
            }

            if (ChkEnforceFontSize.Checked)
            {
                //EnforceFontSize();
                ApplyFontSettings(RTBAnswer);
            }
            // Retrieve original cursor position
            if (RTBAnswer.Text.Length > 0)
            {
                RTBAnswer.SelectionStart = AppSettings.testvariable;
            }
        }

        private void FrmQuiz_SizeChanged(object sender, EventArgs e)
        {
            if (Globals.FrmQuizInit)
            {
                Globals.FrmQuizInit = false;
                return;
            }

            try
            {
                // ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmQuizSize = size);

                int topControlsSpace = 75;
                int spaceBetweenControls = 2;
                const int controlPanelHeight = 128;
                int spaceAvailable = (this.Height - topControlsSpace);  // Exclude the 44 which is RTBQuestion.Top
                int verticalLocationRTBQuestion = topControlsSpace + spaceBetweenControls;
                RTBQuestion.Location = new Point(RTBAnswer.Location.X, verticalLocationRTBQuestion);
                RTBQuestion.Height = (int)((spaceAvailable - controlPanelHeight) * 0.3);

                this.ControlPanel.Location = new Point(this.ControlPanel.Location.X, RTBQuestion.Height + topControlsSpace + spaceBetweenControls);
                // int maxControlWidth = (int)(this.Width - 46); // Set the maximum control width
                int maxControlWidth = (int)Math.Floor((double)(this.Width - 40));

                int rTBAnswerHeight = this.ControlPanel.Location.Y + topControlsSpace - spaceBetweenControls;
                RTBAnswer.Location = new Point(RTBAnswer.Location.X, rTBAnswerHeight);
                RTBAnswer.Height = (int)((spaceAvailable - controlPanelHeight) * 0.7);
                RTBAnswer.Width = maxControlWidth;
                RTBQuestion.Width = maxControlWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during form closing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmQuiz_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Prompt to save existing question
                if (!Globals.QuestionSaved && IfTextboxesFilledModifiedButNotSaved())
                {
                    if (PromptUserToSaveQuestion())
                    {
                        btnAddQuestion.PerformClick();
                        SaveQuiz(Globals.RunningRTFDictionary);
                    }
                }

                // Ensure the position is within screen bounds
                ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmQuizLocation = location);
                ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmQuizSize = size);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during form closing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCircumflex_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetCircumflex(Globals.Current_RTB_withFocus);
        }

        private void btnAngle_Click(object sender, EventArgs e)
        {
            System.Drawing.Font currentfont = Globals.Current_RTB_withFocus.Font;
            Globals.Current_RTB_withFocus.SelectionFont = new System.Drawing.Font("Arial Unicode MS", 18);  // FontStyle.Bold
            Globals.Current_RTB_withFocus.SelectedText = "∠";
            Globals.Current_RTB_withFocus.Font = currentfont;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnQuizOpenFolder_Click(object sender, EventArgs e)
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

        private void FrmQuiz_Activated(object sender, EventArgs e)
        {
            try
            {
                // Do nothing, unless a quiz has been retrieved
                if (Globals.QuizRetrieved) // Returning from getting a new quiz, initialize settings
                {
                    if (Globals.GetAppendFile == false)
                    {
                        HandleSimpleQuizRetrieved();
                    }
                    // Appending a quiz
                    if (Globals.GetAppendFile)
                    {
                        Globals.QuestionTracker = new QuestionTracker(0);
                        InitalizeAndClearQuizValues();
                        DoAppendAndSaveQuizToFile();
                        Globals.GetAppendFile = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // Events


        public void DoAppendAndSaveQuizToFile()
        {
            try
            {
                // Initialize count for new keys
                int count = Globals.AppendRTFDictionary.Count + 1;

                // Append values from the second dictionary
                foreach (var value in Globals.RunningRTFDictionary.Select(x => x.Value))
                {
                    Globals.AppendRTFDictionary.Add(count, value);
                    count++;
                }

                // Save the combined dictionary
                SaveQuiz(Globals.AppendRTFDictionary);

                // Display confirmation message
                string heading = "Append";
                string message = "Quiz Appended.";
                GeneralFns.CustomMessageBox(heading, message);
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RTBQuizName_TextChanged(object sender, EventArgs e)
        {
            if (RTBQuizName.Text.Length > 0)
            {
                RTBQuizName.BackColor = Color.Silver;
            }
        }

        private void RTBQuestion_MouseMove(object sender, MouseEventArgs e)
        {
            RTBQuestion.Cursor = Cursors.Default;
        }

        private void RTBQuizName_MouseMove(object sender, MouseEventArgs e)
        {
            RTBQuizName.Cursor = Cursors.Default;
        }

        private void RTBAnswer_MouseMove(object sender, MouseEventArgs e)
        {
            RTBAnswer.Cursor = Cursors.Default;
        }

        private void RTBQuestion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Capture what you need *now* while the selection is still valid
                string selectedTextNow = RTBQuestion.SelectedText;
                int originalStart = RTBQuestion.SelectionStart;

                string word = RegexHelpers.GetWordRegex().Match(selectedTextNow).Value;
                int wordLength = word.Length;

                // Now defer only the *application* of the selection
                BeginInvoke(new Action(() =>
                {
                    RTBQuestion.Select(originalStart, wordLength);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RTBAnswer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Capture what you need *now* while the selection is still valid
                string selectedTextNow = RTBAnswer.SelectedText;
                int originalStart = RTBAnswer.SelectionStart;

                string word = RegexHelpers.GetWordRegex().Match(selectedTextNow).Value;
                int wordLength = word.Length;

                // Now defer only the *application* of the selection
                BeginInvoke(new Action(() =>
                {
                    RTBAnswer.Select(originalStart, wordLength);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        //
    }
}