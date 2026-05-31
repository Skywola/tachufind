using Tachufind;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;



namespace Tachufind       //  French undo is CONTROL W,   German undo is CONTROL Y.
{
    internal class KeyboardShortcuts
    {
        readonly string messageEnterAVowelFirst = "Enter a vowel first.";
        readonly string messageForGreek = "Enter a vowel first, in Greek Language mode.";

        private static void HandleControlKeyCombinations(RichTextBox currentRTB, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.V) // Paste (Control + V)
                {
                    Font font = new Font("Times New Roman", 12, FontStyle.Regular);
                    if (currentRTB.SelectionFont != null) 
                    {
                        font = currentRTB.SelectionFont;
                    }                    
                    HandlePasteOperation(currentRTB);
                    //if (Clipboard.ContainsText())
                    //{
                    //    currentRTB.Paste();
                    //}

                    e.Handled = true; // Prevent further processing
                    e.SuppressKeyPress = true; // Prevent default behavior
                }

                // Bold (Control + B)
                if (e.KeyCode == Keys.B)
                {
                    if (currentRTB.SelectionFont != null)
                    {
                        currentRTB.SelectionFont = new Font(currentRTB.SelectionFont, currentRTB.SelectionFont.Style ^ FontStyle.Bold);
                    }
                    e.Handled = true;
                }

                // Italics (Control + I)
                if (e.KeyCode == Keys.I)
                {
                    if (currentRTB.SelectionFont != null)
                    {
                        currentRTB.SelectionFont = new Font(currentRTB.SelectionFont, currentRTB.SelectionFont.Style ^ FontStyle.Italic);
                    }
                    e.Handled = true;
                }

                // Underline (Control + U)
                if (e.KeyCode == Keys.U)
                {
                    if (currentRTB.SelectionFont != null)
                    {
                        currentRTB.SelectionFont = new Font(currentRTB.SelectionFont, currentRTB.SelectionFont.Style ^ FontStyle.Underline);
                    }
                    e.Handled = true;
                }
            }
        }

        public static bool HandleControlKeys(RichTextBox currentRTB, KeyEventArgs e)
        {
            Font font = new("Times New Roman", 22);
            if (currentRTB.SelectionFont != null) { font = currentRTB.SelectionFont; }

            // Handle ALT key combinations
            HandleAltKeyCombinations(currentRTB, e);

            // Handle Function key combinations
            HandleFunctionKeyCombinations(currentRTB, e);

            if (e.Control)
            {
                // Handle other Control key combinations
                HandleControlKeyCombinations(currentRTB, e);

                // Suppress key press after handling any control key events
                if (e.Handled)
                {
                    e.SuppressKeyPress = true; // Prevent default behavior
                }

                return e.Handled;
            }
            return false;
        }

        private static void HandlePasteOperation(RichTextBox currentRTB)
        {
            Font font = new("Times New Roman", 20);
            if (currentRTB.SelectionFont != null)
            {
                font = currentRTB.SelectionFont;
            }
            // TEXT handling
            IDataObject clipboardData = Clipboard.GetDataObject() ?? new DataObject();
            if (clipboardData.GetDataPresent(DataFormats.Text) || clipboardData.GetDataPresent(DataFormats.Rtf))
            {
                SetTextSizeOfPastedInText(currentRTB);
                return;
            }

            // IMAGE handling 
            // Check if the clipboard contains image data
            if (Clipboard.ContainsImage())
            {
                // Retrieve the image from the clipboard
                Image? clipboardImage = Clipboard.GetImage();

                if (clipboardImage != null)
                {
                    // Paste BMP into currentRTB - This does NOT set the image to the same size as that of the clipboard
                    // PasteImageAsBmp(currentRTB, clipboardImage);

                    // Set the image to the clipboard, including the size being matched with that of the clipboard
                    InsertImage(currentRTB, clipboardImage);
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain a valid image.");
                }
            }
        }

        private static void InsertImage(RichTextBox richTextBox, Image image)
        {
            Clipboard.SetImage(image); // Set the image to the clipboard
            richTextBox.Paste();       // Paste the image from the clipboard to the RichTextBox
        }


        private static void SetTextSizeOfPastedInText(RichTextBox currentRTB)
        {
            Font font = new("Times New Roman", 22);
            if (currentRTB.SelectionFont != null)
            {
                font = currentRTB.SelectionFont;
            }

            // Create a temporary RichTextBox to handle the paste operation
            using (RichTextBox tempRTB = new())
            {
                // Paste the clipboard content into the temporary RichTextBox
                tempRTB.Paste();

                // Select all text in the temporary RichTextBox
                tempRTB.SelectAll();

                // Apply the passed-in font to the pasted content
                if (tempRTB.Text.Length > 0)
                {
                    tempRTB.SelectionFont = font;  // Apply the font passed in as a parameter
                }

                // Replace the currently selected text in the original RichTextBox with the newly formatted content
                currentRTB.SelectedRtf = tempRTB.Rtf;
            }

            // Return focus to the current RichTextBox
            currentRTB.Focus();
        }

        private static void PasteImageAsBmp(RichTextBox currentRTB, Image clipboardImage)
        {
            // Convert the image to BMP format (RichTextBox typically handles BMP images)
            using Bitmap bmp = new(clipboardImage);
            // Copy the image as BMP to a MemoryStream
            using System.IO.MemoryStream ms = new();
            // Save the image in BMP format to the memory stream
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            // Create a new DataObject with BMP image data
            DataObject data = new();
            data.SetData(DataFormats.Bitmap, true, bmp);

            if (currentRTB.Name == "RTBMain" || currentRTB.Name == "RTBQuestion" || currentRTB.Name == "RTBAnswer" ||
                currentRTB.Name == "RtbTextBottom" || currentRTB.Name == "RtbTextTop")
            {
                // Paste the image as BMP into the RichTextBox
                DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
                if (currentRTB.CanPaste(format))
                {
                    currentRTB.Paste(format); // Ensure image is pasted only once
                }
                else
                {
                    MessageBox.Show("Cannot paste image in this format.");
                }
            }
            else
            {
                MessageBox.Show("This operation is only allowed in Main Textbox, or in the Question and Answer Quiz textboxes.");
            }
        }

        private static void HandleFunctionKeyCombinations(RichTextBox currentRTB, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1: // F1 Opens a help screen when this key is pressed
                        string url = "https://www.archeuslore.com/tachufind/search.html";
                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                        e.Handled = true;
                        break;

                    case Keys.F2: // F2 Opens On-Screen Keyboard
                        string powerShellCommand = "Start-Process osk.exe";

                        ProcessStartInfo startInfo = new ProcessStartInfo()
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-NoProfile -Command \"{powerShellCommand}\"",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        FrmMain frmMain = new();
                        try
                        {
                            if (startInfo != null)
                            {
                                Process? process = Process.Start(startInfo); // Use nullable Process
                                if (process != null)
                                {
                                    process.WaitForExit();

                                    if (process.ExitCode != 0)
                                    {
                                        MessageBox.Show("Failed to start On-Screen Keyboard.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Failed to initiate the process.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to start On-Screen Keyboard: " + ex.Message);
                        }

                        e.Handled = true;
                        break;

                    case Keys.F3:
                        if (Globals.Current_RTB_withFocus != null && Globals.Current_RTB_withFocus.SelectionLength > 0)
                        {
                            //Globals.Current_RTB_withFocus.Copy(); // replaced 01-06-2025
                            Clipboard.SetText(Globals.Current_RTB_withFocus.SelectedText);
                        }
                        break;

                    case Keys.F4:
                        if (Globals.Current_RTB_withFocus != null)
                        {
                            Globals.Current_RTB_withFocus.Paste();
                        }
                        break;

                    case Keys.F5:
                        if (Globals.Current_RTB_withFocus != null)
                        {
                            Globals.Current_RTB_withFocus.Cut();
                        }
                        break;

                    case Keys.F6: // F5 Opens Keyboard Shortcuts window
                        if (!GeneralFns.IsFormTypeOpen(typeof(FrmKBShortcuts)))
                        {
                            //FrmMain frmMain = new();
                            FrmMain.ShowShortcutsForm(AppConstants.EnglishShortcuts);
                        }
                        e.Handled = true;
                        break;

                    case Keys.F7:
                        // Select all text from the cursor position back to the beginning of the line, make it bold
                        	// Get the current cursor position
	                    int cursorPosition = Globals.Current_RTB_withFocus.SelectionStart;

	                    // Get the text of the current line
	                    int lineIndex = Globals.Current_RTB_withFocus.GetLineFromCharIndex(cursorPosition);
	                    int lineStart = Globals.Current_RTB_withFocus.GetFirstCharIndexFromLine(lineIndex);

	                    // Calculate the length of the text to select
	                    int selectionLength = cursorPosition - lineStart;

	                    // Set the selection start and length
	                    Globals.Current_RTB_withFocus.SelectionStart = lineStart;
	                    Globals.Current_RTB_withFocus.SelectionLength = selectionLength;

	                    if (Globals.Current_RTB_withFocus.SelectionLength > 0) // Ensure that there is selected text
	                    {
		                    // Get the current selection font. If null, use a default font.
		                    System.Drawing.Font currentFont = Globals.Current_RTB_withFocus.SelectionFont ?? new System.Drawing.Font("Times New Roman", 20);

		                    // Toggle the bold style.
		                    FontStyle newFontStyle;
		                    newFontStyle = currentFont.Style | FontStyle.Bold; // Add bold

		                    // Apply the new font style to the selected text.
		                    Globals.Current_RTB_withFocus.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
		                    Globals.Current_RTB_withFocus.Focus(); // Optional, to ensure the RichTextBox has focus after the operation
	                    }
                        // Deselect the text by setting SelectionLength to 0
                        Globals.Current_RTB_withFocus.SelectionLength = 0;

                        // Return the cursor to its original position
                        Globals.Current_RTB_withFocus.SelectionStart = cursorPosition;
                        break;
                    case Keys.F8:
                        // F8 Opens Quiz window FrmQuiz frmQuiz = new(); frmQuiz.Show(currentRTB);  e.Handled = true; // break;
                        break;
                    case Keys.F9: // Text to Speech - Toggle Play / Stop

                        // Access the button via FrmMain instance
                        Button? playButton = FrmMain.Instance?.Controls.Find("btnTTSPlay", true).FirstOrDefault() as Button;

                        if (playButton != null)
                        {
                            playButton.PerformClick();
                        }
                        else
                        {
                            Console.WriteLine("Play button not found!");
                        }
                        break;

                    case Keys.F10:
                        // F10 Opens Color window  FrmColor frmColor = new(new FrmMain());   frmColor.Show(currentRTB); e.Handled = true; //break;
                        break;
                    case Keys.F11: // F11 Insert boundary marker
                        break;
                    case Keys.F12: // F12 Delete boundary markers
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }


        internal static void HandleAltKeyCombinations(RichTextBox currentRTB, KeyEventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();

            if (e.Alt)
            {
                // ALT `  for circumflex 
                if (e.KeyCode == Keys.End)  //  This was Keys.Oemtilde, but Windows seems to have a bug, it adds a circumflex after  the modified character.
                {
                    keyboardShortcuts.SetCircumflex(currentRTB);
                    currentRTB.HideSelection = false;
                    e.Handled = true;
                }

                //// ALT + ----> ARROW   for Accute Accent  á
                if (e.KeyCode == Keys.Right)
                {
                    keyboardShortcuts.SetAcuteAccent(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }
                // ALT + LEFT ARROW for Grave accent à
                if (e.KeyCode == Keys.Left)
                {
                    keyboardShortcuts.SetGraveAccent(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }

                // ALT -  for Macron ᾱ    
                if (e.KeyCode == Keys.OemMinus)
                {
                    keyboardShortcuts.SetMacron(currentRTB);
                    e.Handled = true;
                    currentRTB.Focus();
                    e.Handled = true;
                }
                // ALT + "="  for Short ᾰ
                if (e.KeyCode == Keys.Oemplus)
                {
                    keyboardShortcuts.SetShort(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }

                //// ALT ;    Umlaut - This also set Greek Diaresis if in Greek mode
                if (e.KeyCode == Keys.OemSemicolon)
                {
                    InputLanguage language = InputLanguage.CurrentInputLanguage;
                    string lang = GetKeyboardShortcuts(language);
                    if (lang == "ell")
                    {
                        keyboardShortcuts.SetGreekDiaresis(currentRTB);
                    }
                    else
                    {
                        keyboardShortcuts.SetUmlaut(currentRTB);
                    }
                    currentRTB.Focus();
                    e.Handled = true;
                }

                // GREEKS  γγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγ
                //// ALT ,  Greek Iota
                if (e.KeyCode == Keys.OemQuotes)
                {
                    keyboardShortcuts.SetGreekIota(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }

                //// ALT (  Greek Rough
                if (e.KeyCode == Keys.D9)
                {
                    keyboardShortcuts.SetGreekRough(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }

                //// ALT + )    Greek Smooth
                if (e.KeyCode == Keys.D0)
                {
                    keyboardShortcuts.SetGreekSmooth(currentRTB);
                    currentRTB.Focus();
                    e.Handled = true;
                }

                //// ALT + .  Greek Period
                if (e.KeyCode == Keys.OemPeriod)
                {
                    currentRTB.SelectedText = "·";
                    currentRTB.Focus();
                    e.Handled = true;
                }
                // END GREEKS  γγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγ
            }
        }

        private static bool CheckLetter(string letter, string message)
            {
                if (letter.Length < 1)
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (char.IsLetter(Convert.ToChar(letter)))
                {
                    return true;
                }
                return false;
            }

            private static string GetAndSelectLetterBehindCursor(RichTextBox currentRTB)
            {
                int selStart = currentRTB.SelectionStart;
                string selectedText = "";
                if (selStart > 0)
                {
                    currentRTB.Select(selStart - 1, 1);
                    selectedText = currentRTB.SelectedText;
                }
                string send = RegexHelpers.RemoveNewLineCharsRegex().Replace(selectedText, "");
                return send;
            }

            public void SetAcuteAccent(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToAcute(letter);
                }
                else { return; }
                currentRTB.Focus();
            }

            public void SetGraveAccent(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToGrave(letter);
                }
                else { return; }
                currentRTB.Focus();
            }

            public void SetUmlaut(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText =
                    GetSpecialCharacters.ChangeVowelToUmlaut(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }
            public void SetMacron(RichTextBox currentRTB)
            { // For long vowel ā    From btnSpChar4.PerformClick();
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToLong(letter);
                }
                else { return; }
                currentRTB.Focus();
            }

            public void SetShort(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToShort(letter);
                }
                else { return; }
                currentRTB.Focus();
            }

            // Replace the letter behind the cursor with a similar letter with a trema (circumflex) character over it
            public void SetCircumflex(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageEnterAVowelFirst))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToCircumflex(letter);
                }
                else { return; }
                currentRTB.Focus();
            }


            #region GREEK 
            public void SetGreekIota(RichTextBox currentRTB) // Alt + ,
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageForGreek))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToIotaSubscript(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }

            //  for rough mark  ἁ
            public void SetGreekRough(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageForGreek))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToRough(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }

            // for smooth mark  ἀ 
            public void SetGreekSmooth(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageForGreek))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToSmooth(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }

            public void SetGreekCircumflex(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageForGreek))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToTrema(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }

            public void SetGreekDiaresis(RichTextBox currentRTB)
            {
                string letter = GetAndSelectLetterBehindCursor(currentRTB);
                if (CheckLetter(letter, messageForGreek))
                {
                    currentRTB.SelectedText = GetSpecialCharacters.ChangeVowelToUmlaut(letter) + "";
                }
                else { return; }
                currentRTB.Focus();
            }
        #endregion // GREEK




        private static (int, int) SetSizeBasedOnLanguage(string language)
        {
            // default size is English, set here:
            int width = 900;
            int height = 640;

            switch (language)
            {
                case "ell":
                    width = 1000;
                    height = 1000;
                    break;
                case "fra":
                    width = 900;
                    height = 640;
                    break;
                // 920, 550
                case "deu":
                    width = 920;
                    height = 550;
                    break;
                case "ita":
                    width = 920;
                    height = 500;
                    break;
                case "spa":
                    width = 900;
                    height = 850;
                    break;
                case "ara":
                    width = 960;
                    height = 840;
                    break;
                case "ru":
                    width = 960;
                    height = 840;
                    break;
                default:
                    break;
            }
            return (width, height);
        }

        public static void ShowKeyboardShortcutsPopWindowIfNeeded()
        {
            if (AppSettings.ShowFrmKeyboardShorcutsPopWindow)
            {
                FrmMain frmMain = new FrmMain();

                // Get the form width and height based on the language, so size looks right
                InputLanguage language = InputLanguage.CurrentInputLanguage;
                string languageID = new CultureInfo(language.Culture.LCID).ThreeLetterISOLanguageName;
                (int width, int height) = SetSizeBasedOnLanguage(languageID);

                KeyboardShortcuts keyboardShortcuts = new KeyboardShortcuts();
                FrmMain.ShowShortcutsForm(GetKeyboardShortcuts(InputLanguage.CurrentInputLanguage));
            }
        }


        // Functions required by HandleFKeys @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public static string GetKeyboardShortcuts(InputLanguage language)
        {
            string languageCode = new CultureInfo(language.Culture.LCID).ThreeLetterISOLanguageName;

            var shortcutsDictionary = new Dictionary<string, string>
            {
                { "eng", AppConstants.EnglishShortcuts },
                { "fra", AppConstants.FrenchShortcuts },
                { "deu", AppConstants.GermanShortcuts },
                { "ell", AppConstants.AncientGreekShortcuts },
                { "ita", AppConstants.ItalianShortcuts },
                { "rus", AppConstants.RussianShortcuts },
                { "spa", AppConstants.SpanishShortcuts },
                { "lat", AppConstants.EnglishShortcuts },
                { "ara", AppConstants.ArabicShortcuts }
            };

            // Try to get the shortcuts from the dictionary
            if (shortcutsDictionary.TryGetValue(languageCode, out string? shortcuts) && shortcuts != null)
            {
                return shortcuts;
            }

            // Fallback to EnglishShortcuts if the language code is not found or if the value is null
            return AppConstants.EnglishShortcuts;
        }







        //
    }
}
