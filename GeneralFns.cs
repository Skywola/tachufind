using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Tachufind
{
    internal class GeneralFns
    {

        // USAGE CASE EXAMPLE FOR CustomMessageBox procedure below:
        // string heading = "Form1";
        // string message = "This is a message from Form1.";
        // string[] buttons = { "Yes", "No", "Cancel" };

        // DialogResult result = CustomMessageBox(heading, message, buttons);

        // switch (result)
        // {
        //     case DialogResult.Yes:
        //     // Handle Yes action in Form1
        //     MessageBox.Show("Form1: You clicked Yes!");
        //     break;
        // case DialogResult.No:
        //     // Handle No action in Form1
        //     MessageBox.Show("Form1: You clicked No!");
        //     break;
        // case DialogResult.Cancel:
        //     // Handle Cancel action in Form1
        //     MessageBox.Show("Form1: You clicked Cancel!");
        //     break;
        //}
        //    string[] buttons = { "Yes", "No", "Cancel" };
        //    DialogResult result = GeneralFns.CustomMessageBox(heading, message, buttons);

        // SEE above usage case example
        public static DialogResult CustomMessageBox(string heading, string messageTxt, string[]? buttons = null, 
            string font = "Cambria", int fontSize = 18,Color foreColor = default, int buttonYLocation = -1)
        {
            DialogResult result = DialogResult.None;

            try
            {
                if (foreColor == Color.Empty) foreColor = Color.Black;
                if (buttons == null || buttons.Length == 0)
                {
                    buttons = new[] { "OK" }; // Default to "OK" if no buttons are provided
                }

                using Form messageBox = new()
                {
                    Font = new Font(font, fontSize),
                    Text = heading,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MinimizeBox = false,
                    MaximizeBox = false,
                    TopMost = true
                };
                using Label label = new()
                {
                    Text = "  " + messageTxt,
                    AutoSize = true,
                    ForeColor = foreColor
                };

                // Add controls to form
                messageBox.Controls.Add(label);

                // Measure the text size
                using (Graphics graphics = messageBox.CreateGraphics())
                {
                    SizeF size = graphics.MeasureString(messageTxt, messageBox.Font);
                    int labelHeight = (int)size.Height + 40; // Adjust for padding
                    int formWidth = (int)size.Width + 40; // Adjust for padding
                    int formHeight = labelHeight + 60; // Initial height without button

                    // Calculate the total width for the buttons
                    int buttonWidth = 100;
                    int buttonHeight = 36;
                    int spacing = 10;
                    int totalButtonWidth = (buttonWidth + spacing) * buttons.Length - spacing;

                    // Ensure the form is wide enough to fit the buttons
                    formWidth = Math.Max(formWidth, totalButtonWidth + 40);

                    // Position the label and buttons
                    label.Size = new Size(formWidth - 40, labelHeight);
                    label.Location = new Point(20, 20);

                    int buttonY = buttonYLocation != -1 ? buttonYLocation : label.Bottom + 20;
                    int xPosition = (formWidth - totalButtonWidth) / 2;

                    for (int i = 0; i < buttons.Length; i++)
                    {
                        string buttonText = buttons[i];
                        Button button = new Button()
                        {
                            Text = buttonText,
                            Size = new Size(buttonWidth, buttonHeight),
                            Location = new Point(xPosition + (buttonWidth + spacing) * i, buttonY),
                            DialogResult = (DialogResult)Enum.Parse(typeof(DialogResult), buttonText, true)
                        };
                        button.Click += (sender, e) =>
                        {
                            result = button.DialogResult;
                            messageBox.Close();
                        };
                        messageBox.Controls.Add(button);
                    }

                    // Adjust form height to fit the buttons
                    formHeight = buttonY + buttonHeight + 40;
                    messageBox.ClientSize = new Size(formWidth, formHeight);
                }

                messageBox.TopMost = true;
                messageBox.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        internal static string GetProjectRoot()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (directory != null && !directory.GetDirectories().Any(d => d.Name == "tessdata"))
            {
                directory = directory.Parent;
            }
            return directory?.FullName ?? string.Empty;
        }


        #region Search-RelatedFunctions 

        public static bool GetSearchesQueue()
        {
            try
            { 
                Globals.searchQueue.Clear();
                string[] tempArry;

                // the contents for the search and replace function.
                for (int index = 1; index < 11; index++)  //   WAS:  // 0-9       // (int index = 9; index >= 0; index--)
                {
                    char separator = '|';
                    tempArry = SearchSettings.GetText(index, true).Split(separator);
                    foreach (string unit in tempArry) // use pipe symbol as a way of stacking searches in a textbox
                    {
                        if (System.String.IsNullOrWhiteSpace(unit)) { continue; };

                        // Create the search
                        Globals.search = CreateSearch(index, unit);
                        Globals.searchQueue.Enqueue(Globals.search);

                    }
                }
                if (Globals.searchQueue.Count > 0) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static Search CreateSearch(int index, string unit)
        {
            Color find_Color = Color.Black; // Default for Simple Replace
            bool colorON = false;

            if (SearchSettings.FrmColorReplaceMode)
            {
                find_Color = ColorManager.GetColor("C" + index.ToString()); // colorUserSettings[index];
                colorON = true;
            }

            Globals.search = new()
            {
                FindColor = find_Color,
                FindString = unit,
                ReplaceString = SearchSettings.GetText(index, false) + "",  // "" prevents empties
                TextBoxNum = index,
            };

            AppSettings.SearchMode = colorON ? Mode.Color : Mode.Text;

            return Globals.search;
        }
        #endregion // Search-RelatedFunctions

        #region FileImport_And_Export

        public static void SelectAndImportFiles(FileExtensionType fileType)   // This is used for importing various file types.
        {
            try
            {
                string fileExtensionFilter = string.Empty;
                if (fileType == FileExtensionType.Qdta)
                {
                    fileExtensionFilter = "Quiz File Format(*.qdta)|*.qdta";
                }
                if (fileType == FileExtensionType.Fdta)
                {
                    fileExtensionFilter = "Quiz File Format(*.fdta)|*.fdta";
                }

                var openFileDialog = new OpenFileDialog
                {
                    Multiselect = true, // allow multiple file selection
                    InitialDirectory = @"C:\", // default folder to start browsing
                    Title = "Import - Get File(s)",
                    Filter = fileExtensionFilter // file filter
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bool displayMessage = false;

                    // import all selected files
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        string destPath = Path.Combine(AppSettings.Data_Folder, Path.GetFileName(fileName));

                        // check if file already exists
                        if (File.Exists(destPath))
                        {
                            var result = MessageBox.Show($"The file {Path.GetFileName(fileName)} already exists. Do you want to overwrite it?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                continue; // skip importing this file
                            }
                            destPath = AppSettings.Data_Folder + fileName; // copy the file to the destination folder
                            File.Copy(fileName, destPath, true);
                            displayMessage = true;
                        }
                    }
                    if (displayMessage)
                        MessageBox.Show("Import completed successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// </summary>
        /// <param name="fileType"></param>
        public static void SelectAndExportFiles(FileExtensionType fileType)  // This is used for exporting various file types.
        {
            bool displayMessage = false;

            try
            {
                OpenFileDialog openFileDialog1 = new()
                {
                    Multiselect = true,
                    DefaultExt = "qdta"
                };
                if (fileType == FileExtensionType.Qdta)
                {
                    openFileDialog1.Filter = "Quiz File Format(*.qdta)|*.qdta";
                }
                if (fileType == FileExtensionType.Fdta)
                {
                    openFileDialog1.Filter = "Quiz File Format(*.fdta)|*.fdta";
                }
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.InitialDirectory = AppSettings.Data_Folder;
                openFileDialog1.Title = "Select Files";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FolderBrowserDialog folderBrowserDialog1 = new()
                    {
                        SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Description = "Select Folder to Save Files"
                    };
                    string sourcePath = AppSettings.Data_Folder;
                    string destinationPath = folderBrowserDialog1.SelectedPath;


                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in openFileDialog1.FileNames)
                        {
                            string fileName = Path.GetFileName(file);

                            // check if file already exists
                            if (File.Exists(folderBrowserDialog1.SelectedPath + "\\" + fileName))
                            {
                                var result = MessageBox.Show($"The file {Path.GetFileName(destinationPath)} already exists. Do you want to overwrite it?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.No)
                                {
                                    continue; // skip importing this file
                                }
                                File.Copy(sourcePath + "\\" + fileName, folderBrowserDialog1.SelectedPath + "\\" + fileName, true);
                                displayMessage = true;
                            }
                        }
                        if (displayMessage)
                            MessageBox.Show("File(s) exported successfully to " + folderBrowserDialog1.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // FileImport_And_Export

        #region StringManipulationFunctions

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Start interlinear creation procedures
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Add the desired number of spaces to a line, used to make one sentence length = another sentence length
        public static string AddSpaces(string input, int desiredLength)
        {
            try
            { 
                if (input.Length >= desiredLength)
                {
                    return input; // No padding needed
                }

                int spacesToAdd = desiredLength - input.Length;

                // Count existing spaces
                int existingSpaces = input.Count(char.IsWhiteSpace);
                spacesToAdd += existingSpaces;

                // Split the string into words, preserving spaces
                //string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] words = input.Split(AppConstants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

                // Sometimes a single word in the text, or trailing space, can
                // lead to an error.  Prevent these errors from
                // occurring by simply returning a single space.
                int length = words.Length;
                if (length < 2)
                {
                    return " ";
                }
                // Calculate spaces between words
                int spacesBetweenWords = spacesToAdd / (length - 1);
                int extraSpaces = spacesToAdd % (length - 1);

                // Build the padded string
                StringBuilder paddedText = new();
                for (int i = 0; i < length; i++)
                {
                    paddedText.Append(words[i]);
                    if (i < length - 1)
                    {
                        paddedText.Append(' ', spacesBetweenWords);
                        if (extraSpaces > 0)
                        {
                            paddedText.Append(' ');
                            extraSpaces--;
                        }
                    }
                }
                return paddedText.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return " ";
            }
        }

        // This splits the sentence into chunks that are maxLength long.  Both strings should be of equal length, and the remainder of each should be added to the queue
        public static Queue<string> SplitSentenceIntoChunks(string sentence, int maxLength)
        {
            Queue<string> SentenceWordsQueue = new();

            // Check if the input sentence is null or empty
            if (string.IsNullOrEmpty(sentence))
            {
                MessageBox.Show("The input sentence is null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SentenceWordsQueue;  // Return empty queue
            }

            // Check if maxLength is valid
            if (maxLength <= 0)
            {
                MessageBox.Show("The maxLength must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SentenceWordsQueue;  // Return empty queue
            }

            int start = 0;

            while (start < sentence.Length)
            {
                // Calculate the length of the current chunk
                int length = Math.Min(maxLength, sentence.Length - start);

                // Extract the chunk and add it to the queue
                string chunk = sentence.Substring(start, length);
                SentenceWordsQueue.Enqueue(chunk);

                // Move the start index forward by the length of the chunk
                start += length;
            }

            return SentenceWordsQueue;
        }


        public static Queue<string> GetSentenceLines(Queue<string> sentenceChunksQueue)
        {
            Queue<string> localSentenceLinesQueue = new();

            // Check if the input queue is null or empty
            if (sentenceChunksQueue == null || sentenceChunksQueue.Count == 0)
            {
                MessageBox.Show("The input queue is null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return localSentenceLinesQueue;  // Return an empty queue
            }

            string remainder = string.Empty;
            while (sentenceChunksQueue.Count > 0)
            {
                (string left, string right) = SplitAtLastSpace(sentenceChunksQueue.Dequeue());

                if (remainder.Length > 0)
                {
                    remainder += left;
                    localSentenceLinesQueue.Enqueue(remainder);
                    remainder = string.Empty;  // Reset remainder
                }
                else
                {
                    localSentenceLinesQueue.Enqueue(left);
                }

                remainder = right;
            }

            // If there's any remaining text after processing all chunks, enqueue it
            if (!string.IsNullOrEmpty(remainder))
            {
                localSentenceLinesQueue.Enqueue(remainder);
            }

            return localSentenceLinesQueue;
        }


        public static (string leftPart, string rightPart) SplitAtLastSpace(string line)
        {
            // Check if the input string is null or empty
            if (string.IsNullOrEmpty(line))
            {
                MessageBox.Show("The input string is null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (string.Empty, string.Empty);  // Return empty strings
            }

            // Find the index of the last space in the string
            int lastSpaceIndex = line.LastIndexOf(' ');

            // If no space is found, return the entire string as the left part and an empty string as the right part
            if (lastSpaceIndex == -1)
            {
                return (line, string.Empty);
            }

            // Get the string to the left of the last space
            string leftPart = line[..lastSpaceIndex]; // (using range operator)

            // Get the string to the right of the last space
            string rightPart = line[(lastSpaceIndex + 1)..]; // (using range operator)

            return (leftPart, rightPart);
        }

        public static void BreakSentencesIntoChunksAtSpaces(string sentenceTop, string sentenceBottom, int maxLength)
        {
            // Check for invalid maxLength
            if (maxLength <= 0) { maxLength = 40; }

            // Check if the input sentences are null or empty
            if (string.IsNullOrEmpty(sentenceTop) || string.IsNullOrEmpty(sentenceBottom))
            {
                MessageBox.Show("Input sentences cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Adding a space at the end allows finding the end of the sentence
            var localTopSentenceChunksQueue = SplitSentenceIntoChunks(sentenceTop + " ", maxLength);
            var localBottomSentenceChunksQueue = SplitSentenceIntoChunks(sentenceBottom + " ", maxLength);

            Globals.linesQueueTop = GetSentenceLines(localTopSentenceChunksQueue);
            Globals.linesQueueBottom = GetSentenceLines(localBottomSentenceChunksQueue);
        }


        // This is used to pad sentences so that interlinear text sentences turn out to be the same length.
        // It takes the shorter of the two sentences, and adds spaces between the words until it is the
        // same length as the longer sentence
        // Example would be:
        //  	Technology    has    rapidly   transformed   the   way   we   live    and   work.	
        //      La technologie a rapidement transformé notre façon de vivre et de travailler.
        public static void MakeSentenceLengthsEqual()
        {
            // Check if the input queues are null or empty
            if (Globals.TopSentencesQueue == null || Globals.BottomSentencesQueue == null)
            {
                MessageBox.Show("Input queues cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Process all items in the queues
            while (Globals.TopSentencesQueue.Count > 0 && Globals.BottomSentencesQueue.Count > 0)
            {
                // Pop the first item from each queue
                string topSentence = Globals.TopSentencesQueue.Dequeue();
                string bottomSentence = Globals.BottomSentencesQueue.Dequeue();

                // Compare lengths
                int topLength = topSentence.Length;
                int bottomLength = bottomSentence.Length;

                // Determine the target length (longer of the two)
                int targetLength = Math.Max(topLength, bottomLength);

                // Pad the shorter string and add results to their respective queues
                if (topLength < bottomLength)
                {
                    Globals.PaddedTopQueue.Enqueue(GeneralFns.AddSpaces(topSentence, targetLength));
                    Globals.PaddedBottomQueue.Enqueue(bottomSentence);
                }
                else if (bottomLength < topLength)
                {
                    Globals.PaddedBottomQueue.Enqueue(GeneralFns.AddSpaces(bottomSentence, targetLength));
                    Globals.PaddedTopQueue.Enqueue(topSentence);
                }
                else
                {
                    // If lengths are equal, no padding is needed
                    Globals.PaddedTopQueue.Enqueue(topSentence);
                    Globals.PaddedBottomQueue.Enqueue(bottomSentence);
                }
            }
        }

        public static string DoRight(string param, int length)
        {
            // Handle null or empty input
            if (string.IsNullOrEmpty(param))
            {
                return string.Empty;
            }

            // Ensure the length is valid
            if (length <= 0)
            {
                return string.Empty;
            }

            // Adjust length if it exceeds the string length
            if (length > param.Length)
            {
                length = param.Length;
            }

            return param.Substring(param.Length - length, length);
        }

        public static string DoLeft(string param, int length)
        {
            // Check for null or empty input and invalid length values
            if (string.IsNullOrEmpty(param) || length <= 0)
            {
                return string.Empty;
            }

            // Adjust length if it exceeds the string length
            if (length > param.Length)
            {
                length = param.Length;
            }

            // Return the left part of the string using the range operator
            return param[..length];
        }

        public static string DoMid(string param, int startIndex, int length)
        {
            // Check for null or empty input, and ensure valid startIndex and length values
            if (string.IsNullOrEmpty(param) || length <= 0 || startIndex < 0 || startIndex >= param.Length)
            {
                return string.Empty;
            }

            // Adjust length if it exceeds the string length
            if (startIndex + length > param.Length)
            {
                return param[startIndex..]; // Return from startIndex to end if length exceeds
            }

            // Return the substring using the range operator
            return param[startIndex..(startIndex + length)];
        }


        // Optional Parameter: The start parameter has a default value of 0, making it optional.
        // This allows the function to be called without specifying the start index if it’s not needed.
        // Combines Logic: Handles both cases, with and without a start index, in a single function.
        public static int DoInStr(string sText, string findString, int start = 0)
        {
            // Check for null or empty input strings
            if (string.IsNullOrEmpty(sText) || string.IsNullOrEmpty(findString))
            {
                return -1;
            }

            // Check for valid start index
            if (start < 0 || start >= sText.Length)
            {
                return -1;
            }

            // Perform the index search
            return sText.IndexOf(findString, start, StringComparison.CurrentCulture);
        }

        #endregion // StringManipulationFunctions

        #region DirectoryFunctions
        public static List<string> GetListOfFilesInDirectory(string directory, string fileExtension)
        {
            List<string> fileList = new List<string>();

            // Check if the directory exists, if not create it
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Enumerate through the files in the directory with the specified file extension
            foreach (string fileName in Directory.EnumerateFiles(directory, "*" + fileExtension.ToLower()))
            {
                if (File.Exists(fileName) &&
                    Path.GetExtension(fileName).Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    fileList.Add(Path.GetFileNameWithoutExtension(fileName));
                }
            }

            return fileList;
        }

        #endregion // DirectoryFunctions

        #region FormFunctions
        // Method to check if a form of a specific type exists
        public static bool FormExists(string formName)
        {
            // Check for null or empty form name
            if (string.IsNullOrEmpty(formName))
            {
                MessageBox.Show("Form name cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form.Name.Equals(formName, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Form exists
                }
            }
            return false; // Form does not exist
        }

        public static bool IsFormTypeOpen(Type formType)
        {

            foreach (Form frm in System.Windows.Forms.Application.OpenForms)
            {
                if (frm.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion // FormFunctions

        #region FontOrRTF-RelatedFunctions
        public static void CreateSuperorSubScript(RichTextBox rtb, int amount, int size)
        {
            if (rtb == null || rtb.SelectionLength < 1)
            {
                return; // Exit if RichTextBox is null or no text is selected
            }

            float fontSize;
            string fontname = rtb.Font.Name;
            int selStart = rtb.SelectionStart;

            if (rtb.SelectionFont != null)
            {
                fontSize = rtb.SelectionFont.Size;

                rtb.SelectionCharOffset = amount;
                rtb.SelectionFont = new Font(rtb.SelectionFont.Name, size, FontStyle.Bold);

                rtb.SelectionStart += rtb.SelectionLength;
                rtb.SelectionLength = 0;
                rtb.SelectionFont = new Font(fontname, fontSize, FontStyle.Regular);
                rtb.SelectionStart = selStart;
            }
        }


        // LOCATION   → →  → →   Set_RtfFontFamily 
        public static string Set_RtfFontFamily(string fontFamily, RichTextBox rtb)
        {
            if (rtb == null || string.IsNullOrEmpty(rtb.Rtf))
            {
                MessageBox.Show("RichTextBox is null or contains no RTF content.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

            var installedFonts = FontFamily.Families.Select(f => f.Name).ToList();

            string fontHeader = "";
            string remainingRtf = "";

            // Separate RTF header and body if Rtf content exists
            (fontHeader, remainingRtf) = SeparateRtfHeaderAndBody(rtb.Rtf);

            // Replace existing font names with the new font family
            foreach (string fontName in installedFonts)
            {
                fontHeader = fontHeader.Replace(fontName, fontFamily);
            }

            return fontHeader + remainingRtf;
        }


        public static (string rtfHeader, string rtfBody) SeparateRtfHeaderAndBody(string rtf)
        {
            // Look for the ending of the header by finding the }\viewkind
            int viewKindIndex = rtf.IndexOf(@"}\viewkind");
            if (viewKindIndex == -1)
            {
                MessageBox.Show("Invalid RTF format: Header not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (string.Empty, rtf); // Returning an empty header and full body if viewkind is not found
            }

            // Move to the end of the }\viewkind section
            int headerEnd = viewKindIndex + @"}\viewkind".Length;

            // Now find the next space after }\viewkind to determine the end of the header
            int spaceIndex = rtf.IndexOf(' ', headerEnd);
            if (spaceIndex == -1)
            {
                // No space found after }\viewkind; assume the rest is body
                spaceIndex = rtf.Length; // Capture everything till the end
            }

            // Extract header (includes the space) and body (starts after the space)
            string rtfHeader = rtf[..spaceIndex]; // Header includes the space (using range operator)
            string rtfBody = rtf[spaceIndex..].TrimStart(); // Body starts after the space (using range operator)

            return (rtfHeader, rtfBody);
        }

        #endregion // FontOrRTF-RelatedFunctions

























        //
    }
}
