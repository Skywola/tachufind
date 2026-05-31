using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Text;
using System.Text.RegularExpressions;
using Tesseract;
using Windows.UI.Popups;


namespace Tachufind
{

    public partial class FrmOCR : Form
    {
        public FrmOCR()
        {
            InitializeComponent();
        }
        internal static string tessdata_Path = Path.Combine(GeneralFns.GetProjectRoot(), "tessdata");

        private static string? revertTop;
        private static string? revertBottom;
        //private float Zoom = 0.8f;  // ZoomFactor for RtbTextTop and RtbTextBottom


        #region ActivateAndLoad
        private void FrmOCR_Activated(object sender, EventArgs e)
        {
            RtbTextTop.Select(0, 0);
            RtbTextTop.Focus();
        }

        private void FrmOCR_Load(object sender, EventArgs e)
        {
            ScreenSetUp(this);

            RestoreImageAndPictureBoxImages();
            // Top to bottom
            CmbLanguage1.Text = Globals.User_Settings.OCR_Language1;

            CmbLanguage3.Text = Globals.User_Settings.OCR_Language3;

            RtbTextTop.BackColor = Globals.User_Settings.RTBMainBackColor;
            RtbTextBottom.BackColor = Globals.User_Settings.RTBMainBackColor;

            ChkTTSBrackets.Checked = Convert.ToBoolean(Globals.User_Settings.StateOfOCRTTSBrackets);
            SpinnerLineLength.Value = Globals.User_Settings.LineLengthForInterlinears;
        }

        private void ScreenSetUp(Form form)
        {
            System.Drawing.Point savedLocation = Globals.User_Settings.FrmOCRLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        #endregion // ActivateAndLoad


        #region GeneralUtilityFunctions

        private void ResetLabelsAndButton()
        {
            lblTop.Text = "Top = 0";
            lblBottom.Text = "Bot = 0";
            lblTop.ForeColor = Color.Black;
            lblBottom.ForeColor = Color.Black;
            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";
        }

        private static void DisposeOfImageAndPictureBoxImage(PictureBox pictureBox, ref Image cachedImage)
        {
            cachedImage?.Dispose();
            pictureBox?.Dispose();
        }

        private void DisposePictureBoxImages()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
        }


        private static string RemoveInteriorDashesFromWords(string textString)
        {
            // Regular expression to find words split by a hyphen and any whitespace (space or newline)
            string pattern = @"(\w+)-\s+(\w+)";

            // Replace the matched pattern with the full word (concatenated parts)
            string result = Regex.Replace(textString, pattern, "$1$2");

            return result;
        }



        private void ScrollToTop()
        {
            RtbTextTop.SelectionStart = 0;
            RtbTextTop.SelectionLength = 0;
            RtbTextBottom.SelectionStart = 0;
            RtbTextBottom.SelectionLength = 0;
            RtbTextBottom.Refresh();
            RtbTextTop.Refresh();
            RtbTextTop.Focus(); // Focus the RichTextBox
        }

        private static string GetLanguageIndicator(int languageIndex) => languageIndex switch
        {
            -1 => "eng",    // English
            0 => "ara",     // Arabic
            1 => "chi_sim", // Chinese Simplified
            2 => "chi_tra", // Chinese Traditional
            3 => "eng",     // English
            4 => "fas",     // Farsi
            5 => "fra",     // French
            6 => "deu",     // German
            7 => "ell",     // Greek
            8 => "ita",     // Italian
            9 => "lat",     // Latin
            10 => "rus",    // Russian
            11 => "spa",    // Spanish
            _ => "eng",     // Default case (English)
        };

        private static string GetSecondaryLanguageIndicator(int languageIndex) => languageIndex switch
        {
            -1 => "none",    // English
            0 => "none",     // Arabic
            1 => "ara",     // Arabic
            2 => "chi_sim", // Chinese Simplified
            3 => "chi_tra", // Chinese Traditional
            4 => "eng",     // English
            5 => "fas",     // Farsi
            6 => "fra",     // French
            7 => "deu",     // German
            8 => "ell",     // Greek
            9 => "ita",     // Italian
            10 => "lat",     // Latin
            11 => "rus",    // Russian
            12 => "spa",    // Spanish
            _ => "none",     // Default case (none)
        };


        // Sets the markers for the language case.
        private static string GetMarkoff(string language, string text)  // Quotes are only split at the end if there is a termination chara
        {
            string outText = string.Empty;
            if (language == "ara") // : Use DEFAULT VALUES for Arabic     Only split if closing quote is preceeded by closing punctuation ( . ! ? )
            {
                outText = FormatAndMarkoff(text.TrimEnd(),
                    "«.", "«؟", "«!",  // Arabic-style double quotes with sentence-ending punctuation
                    "».", "»؟", "»!",  // Closing Arabic double quotes
                    "‘.", "’؟", "’!",  // Single quotes, may be less common in Arabic
                    "“.", "”؟", "”!",  // English-style Arabic mirrored quotes
                    "\".", "\"؟", "\"!", // Standard double quotes with punctuation
                    ".", "؟", "!"  // Sentence endings without spaces
                );
            }
            if (language == "chi_sim" || language == "chi_tra")  // Mandarin and Cantonese Chinese 
            {
                text = text.Replace("......", "¤");  // Handle ...... by replacing it and later substituting it back in.  This prevents Carriage returns on ......
                outText = FormatAndMarkoff(text.TrimEnd(),
                    "。", // Chinese period (full stop)
                    "？", // Chinese question mark
                    "！", // Chinese exclamation mark
                    "“。", "”！", "”？", // Double quotes with sentence-ending punctuation
                    "‘。", "’！", "’？", // Single quotes with sentence-ending punctuation
                    "\"。", "\"？", "\"！", // Standard double quotes with punctuation
                    ".", "?", "!"  // English punctuation (if mixing languages)
                );
                text = text.Replace("¤", "......");  // Handle ...... by replacing it and later substituting it back in.  This prevents Carriage returns on ......
            }
            if (language == "deu")  // : Use DEFAULT VALUES for German.  German is a special case.   Only split if closing quote is followed by closing punctuation ( . ! ? )
            {
                outText = FormatAndMarkoff(text.TrimEnd(), "?“", ".“", "!“", "».", "»?", "»!", ".’", "?’", "!’", "\".", "'.", ". ", "? ", "! ");   // ": "
            }
            if (language == "ell")
            {  // Greek  Quote Close characters:  "»", "’", "”",   Last three parameters are delimiters for Greek text!       // excluded so far - ", '
                outText = FormatAndMarkoff(text.TrimEnd(), "»", "’", "”", ".", ";", "˙");
            }
            if (language == "eng" || language == "spa")  // English, Spanish    Only split if closing quote is preceeded by closing punctuation ( . ! ? )
            {
                text = text.Replace("...", "¤");  // Handle ... by replacing it and later substituting it back in.   This prevents Carriage returns on ... 
                outText = FormatAndMarkoff(text.TrimEnd(), ".’", ".” ", ".'", "?'", "!'", ".\"", "?\"", "!\"", ". ", "? ", "! ");   // ": ",   ", " "
                outText = outText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.
            }
            if (language == "fra") // : Use DEFAULT VALUES for French  (Can't use "'" or ": " )   Only split if closing quote is preceeded by a closing punctuation ( . ! ? )
            {
                text = text.Replace("...", "¤");  // Handle ... by replacing it and later substituting it back in.   This prevents Carriage returns on ... 
                outText = FormatAndMarkoff(text.TrimEnd(), ". »", "? »", "! »", "’ ", "” ", "\" ", ". ", "? ", "! ");
                outText = outText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.
            }
            if (language == "ita")  // Italian    Only split if closing quote is preceeded by closing punctuation ( . ! ? )
            {
                text = text.Replace("...", "¤");  // Handle ... by replacing it and later substituting it back in.   This prevents Carriage returns on ... 
                outText = FormatAndMarkoff(text.TrimEnd(), ".’ ", ".” ", ".' ", "?' ", "!' ", ".\" ", "?\" ", "!\" ", ". ", "? ", "! ");   // ": ",
                outText = outText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.
            }
            if (language == "rus") // : Use DEFAULT VALUES for Russian   Only split if closing quote is preceeded by closing punctuation ( . ! ? )
            {
                text = text.Replace("...", "¤");  // Handle ... by replacing it and later substituting it back in.   This prevents Carriage returns on ... 
                outText = FormatAndMarkoff(text.TrimEnd(), ".»", "?»", ".’", ".” ", ".\"", "?\"", "!\"", ".'", "?'", "!'", ". ", "? ", "! ");  // ": ", 
                outText = outText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.
            }
            if (language == "lat")  // Latin    Only split if closing quote is preceeded by closing punctuation ( . ! ? )
            {
                text = text.Replace("...", "¤");  // Handle ... by replacing it and later substituting it back in.   This prevents Carriage returns on ... 
                outText = FormatAndMarkoff(text.TrimEnd(), ".’", ".” ", ".'", "?'", "!'", ".\"", "?\"", "!\"", ". ", "? ", "! ");   // ": ",   ", " "
                outText = outText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.

                //outText = outText.Replace("", "");
                //outText = outText.Replace("", "");
            }
            return outText;
        }


        // Twenty parameters are allowed for delimiters.   This does the sentence splitting.
        private static string FormatAndMarkoff(string text, string delimiter1 = "", string delimiter2 = "", string delimiter3 = "",
            string delimiter4 = "", string delimiter5 = "", string delimiter6 = "", string delimiter7 = "", string delimiter8 = "",
            string delimiter9 = "", string delimiter10 = "", string delimiter11 = "", string delimiter12 = "", string delimiter13 = "",
            string delimiter14 = "", string delimiter15 = "", string delimiter16 = "", string delimiter17 = "", string delimiter18 = "",
            string delimiter19 = "", string delimiter20 = "")
        {
            text = text.Replace("   ", " ");
            text = text.Replace("  ", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", "");

            if (delimiter1?.Length > 0)
            {
                // DoSplitReplace below does this:
                text = text.Replace(delimiter1 + " ", delimiter1);  // oldValue, NewValue
                text = text.Replace(delimiter1, delimiter1 + "ↆ"); // put in marker used to split sentences at delimiters
            }
            if (delimiter2?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter2);
            }
            if (delimiter3?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter3);
            }
            if (delimiter4?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter4);
            }
            if (delimiter5?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter5);
            }
            if (delimiter6?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter6);
            }
            if (delimiter7?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter7);
            }
            if (delimiter8?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter8);
            }
            if (delimiter9?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter9);
            }
            if (delimiter10?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter10);
            }
            if (delimiter11?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter11);
            }
            if (delimiter12?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter12);
            }
            if (delimiter13?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter13);
            }
            if (delimiter14?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter14);
            }
            if (delimiter15?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter15);
            }
            if (delimiter16?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter16);
            }
            if (delimiter17?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter17);
            }
            if (delimiter18?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter18);
            }
            if (delimiter19?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter19);
            }
            if (delimiter20?.Length > 0)
            {
                text = DoSplitReplace(text, delimiter20);
            }
            return text;
        }

        private static string DoSplitReplace(string text, string splitter)
        {
            //text = text.Replace(splitter + " ", splitter);
            text = text.Replace(splitter, splitter + "ↆ");
            return text;
        }

        // Used below only to separate the first sentence and the remaining text.
        public static Tuple<string, string> SplitAtFirstDelimiter(string text, char delimiter)
        {
            string[] parts = text.Split([delimiter], 2);
            if (parts.Length == 2)
            {
                return new Tuple<string, string>(parts[0], parts[1]);
            }
            else
            {
                return new Tuple<string, string>(text, "");  // Return entire text if no delimiter found
            }
        }


        private bool WarnOnMismatch(int value1, int value2)
        {
            lblTop.Text = "Top = " + value1;
            lblBottom.Text = "Bot = " + value2;
            if (value1 != value2)
            {
                lblTop.ForeColor = Color.Red;  // Set both labels to bright red
                lblBottom.ForeColor = Color.Red;
                BtnFullMerge.ForeColor = Color.Red;
                return true;
            }
            return false;
        }

        public static Queue<string> RemoveCarriageReturnsAndSplit(string inputText)
        {
            // Remove all types of line breaks
            string cleanedText = Regex.Replace(inputText, @"\r\n|\r|\n", " ");

            // Split the text at the "ↆ" symbol
            string[] sentences = Regex.Split(cleanedText, @"ↆ");

            // Create a queue to store the results
            Queue<string> resultQueue = new();

            // Add each sentence to the queue
            foreach (string sentence in sentences)
            {
                // Avoid adding empty sentences
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    resultQueue.Enqueue(sentence.Trim());
                }
            }

            return resultQueue;
        }



        public void FullMerge(string topText, string bottomText)
        {
            try
            {
                // Save contents for step undo
                Globals.undoTopStack.Push(RtbTextTop.Text);
                Globals.undoBottomStack.Push(RtbTextBottom.Text);
                if (string.IsNullOrEmpty(RtbTextTop.Text) || string.IsNullOrEmpty(RtbTextBottom.Text)) { return; }

                Globals.TopSentencesQueue.Clear(); Globals.BottomSentencesQueue.Clear();

                // Start
                string pattern = @"ↆ\s+";
                string replacement = "ↆ";
                topText = Regex.Replace(topText, pattern, replacement);
                bottomText = Regex.Replace(bottomText, pattern, replacement);
                Globals.TopSentencesQueue = RemoveCarriageReturnsAndSplit(topText);
                Globals.BottomSentencesQueue = RemoveCarriageReturnsAndSplit(bottomText);

                // Warns and exits if the two above values are not the same
                if (WarnOnMismatch(Globals.TopSentencesQueue.Count, Globals.BottomSentencesQueue.Count))
                {
                    MessageBox.Show("Top sentence count does not equal bottom sentence count.");
                    return;
                }

                // make the shorter sentence as long as the longer of the two sentences by adding spaces between words
                // This uses as inputs:
                //  Globals.TopSentencesQueue
                //  Globals.BottomSentencesQueue
                //
                // This outputs results to:
                // Globals.PaddedTopQueue
                // Globals.PaddedBottomQueue
                GeneralFns.MakeSentenceLengthsEqual();


                // DEBUG CHECK CONFIRMED THAT GeneralFns.MakeSentenceLengthsEqual(); IS FUNCTIONING PROPERLY

                // Globals.PaddedTopQueue and Globals.PaddedBottomQueue contain the two equal-length sentences
                // These are broken into chunks whose length is as close as possible to the maxLength
                // The number of chunks from the top line must match the number of chunks in the bottom line.

                int maxLength = (int)SpinnerLineLength.Value;
                if (maxLength < 3) { maxLength = 40; }

                Queue<string> resultQueueTop = new();
                Queue<string> resultQueueBottom = new();
                while (Globals.PaddedTopQueue.Count > 0 && Globals.PaddedBottomQueue.Count > 0)
                {
                    string sentenceTop = Globals.PaddedTopQueue.Dequeue();
                    string sentenceBottom = Globals.PaddedBottomQueue.Dequeue();

                    GeneralFns.BreakSentencesIntoChunksAtSpaces(sentenceTop, sentenceBottom, maxLength);


                    while (Globals.linesQueueTop.Count > 0)
                    {
                        resultQueueTop.Enqueue(Globals.linesQueueTop.Dequeue());
                    }
                    while (Globals.linesQueueBottom.Count > 0)
                    {
                        resultQueueBottom.Enqueue(Globals.linesQueueBottom.Dequeue());
                    }
                }


                // These should always have the same count, if not, something is wrong
                if (resultQueueTop.Count != resultQueueBottom.Count)
                {
                    MessageBox.Show("Mismatched sentence count. See FrmOCR line 807");
                    return;
                }

                StringBuilder outputToText = new();
                while (resultQueueTop.Count > 0 && resultQueueBottom.Count > 0)
                {
                    outputToText.AppendLine(resultQueueTop.Dequeue());
                    string line = resultQueueBottom.Dequeue();
                    line = ChkTTSBrackets.Checked ? $"[{line}]" : line;  // Adds brackets to bottom line if requested
                    outputToText.AppendLine(line + Environment.NewLine);
                }
                RtbTextTop.Text = outputToText.ToString();
                ScrollToTop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatText(string text)
        {
            // Perform any additional formatting here, if needed
            // For example, specifying the encoding as UTF-8
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }


        private void RestoreImageAndPictureBoxImages()
        {
            if (pictureBox1.Image == null && Globals.cachedImageTop != null)
            {
                pictureBox1.Image = Globals.cachedImageTop;
            }
            if (pictureBox2.Image == null && Globals.cachedImageBottom != null)
            {
                pictureBox2.Image = Globals.cachedImageTop;
            }
        }

        // TODO This might make a good class in the future
        private static string AlterTextBasedOnLanguage(string text, string currentLanguageCode)
        {
            if (currentLanguageCode == "ell")
            {
                // NOTE → →  → → Special case situations for Greek
                text = text.Replace("δ)", "δ᾽");
                text = text.Replace("δ'", "δ᾽");
                text = text.Replace("ὑπ)", "ὑπ᾽");
                text = text.Replace("ἀλλ)", "ἀλλ᾽");
                text = text.Replace("μεθ)", "μεθ᾽");
                text = text.Replace("᾿Α", "Ἀ");
                text = text.Replace("᾽Α", "Ἀ");
                text = text.Replace(" δ ", " δ᾽");
                text = text.Replace("'", "˙");
                text = text.Replace(":", "˙");
                text = text.Replace('"', '˙');
                text = text.Replace(".!", ".");
                text = text.Replace(". !", ".");
                text = text.Replace("π˙", "π");
                text = text.Replace("““", "“");
                text = text.Replace("ὃ᾽", "δ᾽");
                text = text.Replace("€", "ἐ");
                text = text.Replace(" Ἠ", " Π");
            }
            if (currentLanguageCode == "lat")
            {
                text = text.Replace("á", "ā");
                text = text.Replace("à", "ā");
                text = text.Replace("ä", "ā");
                text = text.Replace("é", "ē");
                text = text.Replace("è", "ē");
                text = text.Replace("ë", "ē");
                text = text.Replace("ó", "ō");
                text = text.Replace("ò", "ō");
                text = text.Replace("ö", "ō");
                text = text.Replace("ú", "ū");
                text = text.Replace("ù", "ū");
                text = text.Replace("ü", "ū");
                text = text.Replace("£", "s");
                text = text.Replace("/", "");
                text = text.Replace('*', '\"');
                text = text.Replace("\"\"", "\"");
                // New dictionary for replacements where Macron is missing
                text = ReplaceCommonOCRErrorsInLatin(text);
            }
            text = text.TrimEnd('\r', '\n');
            return text;
        }


        // NOTE → →  → →  Macron is missing replacement STLatin
        private static string ReplaceCommonOCRErrorsInLatin(string text)
        {
            text = text.Replace("i ", "ī ");
            text = text.Replace("i.", "ī.");
            text = text.Replace("amo", "amō"); // first person singular verb
            text = text.Replace("templo", "templō");
            text = text.Replace("puella", "puellā");
            text = text.Replace("Romani", "Rōmānī");
            text = text.Replace("Rōmāni ", "Rōmānī ");
            text = text.Replace("Rōmānos", "Rōmānōs");
            text = text.Replace("Rōma", "Rōmā");
            text = text.Replace("vici", "vīcī");
            text = text.Replace("milites", "mīlitēs");
            text = text.Replace("erunt", "ērunt");
            //text = text.Replace("ūci", "ūcī");
            //text = text.Replace("api", "apī");
            //text = text.Replace("ici", "icī");  // ineffective
            //text = text.Replace("āni", "ānī");
            text = text.Replace("ivi", "īvī");
            // text = text.Replace("gni", "gnī");     // ineffective
            text = text.Replace("vōbis", "vōbīs");
            text = text.Replace("amici", "amīcī");
            text = text.Replace("civēs", "cīvēs");
            text = text.Replace("quos", "quōs");
            //text = text.Replace("nostri", "nostrī");
            //text = text.Replace("Nostri", "Nostrī");
            text = text.Replace("pri", "prī");
            text = text.Replace("o.", "ō.");
            text = text.Replace("j", "i");
            //text = text.Replace("w", "m");  // Dangerous when text is mixed languages - look for alt
            text = text.Replace("o ", "ō ");
            text = text.Replace("o.", "ō."); // oō
            text = text.Replace("oō", "ō");
            text = text.Replace(" tō ", " to ");  // recognize as English 
            text = text.Replace("(tō ", "(to ");  // recognize as English 
            text = text.Replace("āā", "ā");
            text = text.Replace("aā", "ā");
            text = text.Replace("ü ", "ū ");
            text = text.Replace(" rō.", " 10.");
            text = text.Replace("\nrō.", "\n10.");
            //text = text.Replace(" vi", " vī");    // ineffective
            //text = text.Replace("-āvi", "-āvī"); 
            //text = text.Replace("ivi", "īvī");   // ineffective
            //text = text.Replace(" oi ", " oī ");
            //text = text.Replace(" ei ", " eī ");
            text = text.Replace(" se ", " sē ");
            //text = text.Replace(" mei ", " meī ");
            text = text.Replace("&", "a");
            text = text.Replace("é", "ē");  // Misses, Different characters?
            text = text.Replace('é', 'ē'); // Different characters?  Maybe the problem is coming from the ENGLISH side!
            text = text.Replace("Á", "Ā");
            text = text.Replace("É", "Ē");
            text = text.Replace("Í", "Ī");
            text = text.Replace("Ó", "Ō");
            text = text.Replace("^", "");  // Single replacements, for vocabulary section
            text = text.Replace("'", "");  // Single replacements, for vocabulary section
            text = text.Replace("’", "");  // Single replacements, for vocabulary section
            text = text.Replace("milit", "mīlit");  // Single replacements, for vocabulary section  

            // OCR is creating d's instead of long o's. Latin words that end in d's and 
            // are preceded by a consonant need to be changed to long o's.
            text = Regex.Replace(text, @"(?<=[^aeiou\s])d(?=[^aeiou\s])(?=\b|[.,;:!?])", "ō");

            // Regex to detect likely long ī candidates
            string pattern2 = @"\b\w+[lr]ī\b|\b\w+[mn]ī\b|\b\w+[aeiou]ī\b|\b\w+[lr][aeiou]ī\b";

            // Replacement function to replace 'i' with 'ī'
            text = Regex.Replace(text, pattern2, match =>
            {
                // Replace all short 'i's in the match with 'ī'
                return match.Value.Replace("i", "ī");
            });

            return text;
        }


        #endregion //  GeneralUtilityFunctions


        #region Buttons__Click

        private void BtnFullMerge_Click(object sender, EventArgs e)
        {
            // Save contents for step undo
            //Globals.stepUndoTop = RtbTextTop.Text;
            //Globals.stepUndoBottom = RtbTextBottom.Text;
            Globals.undoTopStack.Push(RtbTextTop.Text);
            Globals.undoBottomStack.Push(RtbTextBottom.Text);

            if (RtbTextTop.Text.Length > 0 && RtbTextBottom.Text.Length > 0)
            {
                FullMerge(RtbTextTop.Text, RtbTextBottom.Text);
                //RtbTextBottom.Text = string.Empty;
            }
        }

        private bool countMarkersOnly()
        {
            int countTop = 0;
            int countBottom = 0;

            if (btnSplit.Text == "Recount")
            {
                countTop = RtbTextTop.Text.Count(c => c == 'ↆ');
                countBottom = RtbTextBottom.Text.Count(c => c == 'ↆ');
                lblTop.Text = "Top = " + countTop.ToString();
                lblBottom.Text = "Bot = " + countBottom.ToString();

                if (countTop != countBottom)
                {  // Mis-match in splitter numbers between top and bottom textbox
                    lblTop.ForeColor = Color.Red;  // Set both labels to bright red
                    lblBottom.ForeColor = Color.Red;
                    btnSplit.ForeColor = Color.Red;
                    btnSplit.Text = "Recount";
                }
                else
                {
                    lblTop.ForeColor = Color.Black;  // Set both labels to bright red
                    lblBottom.ForeColor = Color.Black;
                    btnSplit.ForeColor = Color.Black;
                    btnSplit.Text = "Split";
                }
            }
            if (countTop < 1 || countBottom < 1)
            {
                return false; // Proceed to further processing (not CountMarkersOnly)
            }
            else
            {
                return true;  // This is just a count, return  (CountMarkersOnly)
            }
        }

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            try
            {
                // Do count and return if not the same number of markers 'ↆ'
                if (countMarkersOnly()) { return; }

                // Save contents for step undo
                Globals.undoTopStack.Push(RtbTextTop.Text);
                Globals.undoBottomStack.Push(RtbTextBottom.Text);
                //Globals.stepUndoBottom = RtbTextBottom.Text;

                string topText = string.Empty;
                string bottomText = string.Empty;

                // This is needed for delimiting text.
                string currentLanguageCode = GetLanguageIndicator(CmbLanguage1.SelectedIndex);

                // Insert marker ↆ at the end of each sentence, based on the language chosen in the CmbLanguage box
                topText = GetMarkoff(GetLanguageIndicator(CmbLanguage1.SelectedIndex), RtbTextTop.Text.TrimEnd());
                bottomText = GetMarkoff(GetLanguageIndicator(CmbLanguage3.SelectedIndex), RtbTextBottom.Text.TrimEnd());

                // NOTE: → →  → →  This is the correction section for OCR
                // Check for any quotation marks following the markoff, if so, 

                //string CR1 = "\r\n";
                // string CR2 = "\r\n\r\n";

                topText = topText.Replace(".!", ".");
                topText = topText.Replace(". !", ".");
                topText = topText.Replace(". ↆ»ↆ", ". »ↆ");  // . ↆ»ↆ
                topText = topText.Replace("? ↆ»ↆ", "? »ↆ");
                topText = topText.Replace("! ↆ»ↆ", "! »ↆ");
                topText = topText.Replace(". ↆ»", ". »ↆ");
                topText = topText.Replace("? ↆ»", "? »ↆ");
                topText = topText.Replace("! ↆ»", "! »ↆ");
                // topText = topText.Replace("¤", "...");  // Handle ... by replacing it and later substituting it back in.
                //}

                // Split the paragraphs into sentences using the marker "ↆ"
                List<string> splitTopList = topText.Split(AppConstants.ParagraphDelimiters, StringSplitOptions.None)
                                                   .TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();
                List<string> splitBottomList = bottomText.Split(AppConstants.ParagraphDelimiters, StringSplitOptions.None)
                                                         .TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();


                lblTop.Text = "Top = " + splitTopList.Count;
                lblBottom.Text = "Bot = " + splitBottomList.Count;

                if (splitTopList.Count != splitBottomList.Count)
                {  // Mis-match in splitter numbers between top and bottom textbox
                    lblTop.ForeColor = Color.Red;  // Set both labels to bright red
                    lblBottom.ForeColor = Color.Red;
                    btnSplit.ForeColor = Color.Red;
                    btnSplit.Text = "Recount";
                }

                // Add "\n" in places to limit line length in items in splitTopList
                string line = string.Empty; string outText = string.Empty;
                foreach (string bigSentence in splitTopList)
                {
                    line = SetLineLengthToDesiredLength.ModifyTextToDesiredLength(Convert.ToInt32(SpinnerLineLength.Value), bigSentence + "ↆ");
                    outText += line + "\r\n\r\n";
                }
                // Fill Top Textbox
                RtbTextTop.Text = outText;

                line = string.Empty; outText = string.Empty;
                foreach (string bigSentence in splitBottomList)
                {
                    int bottomLineLength = (int)SpinnerLineLength.Value;
                    line = SetLineLengthToDesiredLength.ModifyTextToDesiredLength(Convert.ToInt32(bottomLineLength), bigSentence + "ↆ");
                    outText += line + "\r\n\r\n";
                    outText = outText.Replace(",", ", ");
                    outText = outText.Replace("  ", " ");
                }
                // Fill Bottom Textbox
                RtbTextBottom.Text = outText;
                RtbTextBottom.SelectionStart = 0;
                RtbTextBottom.Focus();
                ScrollToTop();

                RtbTextTop.SelectionStart = 0;
                RtbTextTop.Focus();
                ScrollToTop();

                // Text is now available for critical editing.
                // User can click the STEP button to step through each section, creating the interlinear step by step
                // OR user clicks the Full Merge button and merges all in one step.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearTop_Click_1(object sender, EventArgs e)
        {
            RtbTextTop.Text = string.Empty;
            RtbTextTop.Focus();
        }

        private void BtnClearBottom_Click(object sender, EventArgs e)
        {
            RtbTextBottom.Text = string.Empty;
            RtbTextBottom.Focus();
        }

        // This allows for possible user modification between the Split button and the Step button
        // User could modify line lengths or number of lines.
        private void BtnStepMerge_Click(object sender, EventArgs e)
        {
            try
            {
                // Save contents for step undo
                Globals.undoTopStack.Push(RtbTextTop.Text);
                Globals.undoBottomStack.Push(RtbTextBottom.Text);
                if (RtbTextTop.Text.Length < 0 | RtbTextBottom.Text.Length < 0) { return; }


                // Split the paragraphs into first sentence and remainder using the marker "ↆ"
                char delimiter = 'ↆ'; // string delimiter = "ↆ\r?\n*";   Matches "ↆ" followed by any number of "\r" or "\n"
                Tuple<string, string> topSplit = SplitAtFirstDelimiter(RtbTextTop.Text, delimiter);
                Tuple<string, string> bottomSplit = SplitAtFirstDelimiter(RtbTextBottom.Text, delimiter);

                // Get the first sentence for processing  
                string sectionTop = topSplit.Item1;
                string sectionBottom = bottomSplit.Item1;

                // Remove all types of line breaks and replace with a space
                sectionTop = Regex.Replace(sectionTop, @"\r\n|\r|\n", " ");
                sectionBottom = Regex.Replace(sectionBottom, @"\r\n|\r|\n", " ");

                int maxLength = (int)SpinnerLineLength.Value;
                if (maxLength < 3) { maxLength = 40; }

                Globals.TopSentencesQueue.Clear();
                Globals.BottomSentencesQueue.Clear();
                Globals.PaddedTopQueue.Clear();
                Globals.PaddedBottomQueue.Clear();

                Globals.TopSentencesQueue.Enqueue(sectionTop);
                Globals.BottomSentencesQueue.Enqueue(sectionBottom);

                // Make both sentences equal length by adding spaces to the shorter of the two
                // place results in PaddedTopQueue and PaddedBottomQueue
                GeneralFns.MakeSentenceLengthsEqual();

                string sentenceTop = Globals.PaddedTopQueue.Dequeue();
                string sentenceBottom = Globals.PaddedBottomQueue.Dequeue();

                // Get lines by splitting sentences when needed to limit their length
                //Queue<string> linesTopQueue = new Queue<string>();
                //Queue<string> linesBottomQueue = new Queue<string>();

                var linesTopQueue = GeneralFns.SplitSentenceIntoChunks(sentenceTop + " ", maxLength);   // Adding a space at the end allows finding the end of the sentence
                var linesBottomQueue = GeneralFns.SplitSentenceIntoChunks(sentenceBottom + " ", maxLength);

                if (linesTopQueue.Count < 1 || linesBottomQueue.Count < 1)
                {
                    MessageBox.Show("No lines retrieved.");
                    return;
                }

                Queue<string> TopLineQueue = GeneralFns.GetSentenceLines(linesTopQueue);
                Queue<string> BottomLineQueue = GeneralFns.GetSentenceLines(linesBottomQueue);

                StringBuilder outlines = new();

                while (TopLineQueue.Count > 0 && BottomLineQueue.Count > 0)
                {
                    outlines.AppendLine(TopLineQueue.Dequeue());

                    string bottomLine = BottomLineQueue.Dequeue();
                    if (ChkTTSBrackets.Checked)
                    {
                        bottomLine = "[" + bottomLine + "]";
                    }

                    outlines.AppendLine(bottomLine);
                    outlines.AppendLine();

                    if (TopLineQueue.Count == 0)
                    {
                        outlines.AppendLine("§");
                        outlines.AppendLine();
                    }
                }

                // Get the text back
                RtbTextTop.Text = outlines + topSplit.Item2.Trim() + " ";   // End space is a end sentence marker
                RtbTextBottom.Text = bottomSplit.Item2.Trim() + " ";

                RtbTextTop.Focus();
                ScrollToTop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            this.RtbTextTop.Clear();
            this.RtbTextBottom.Clear();
            RtbTextTop.Focus();
        }

        private void RtbTextTop_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtbTextBottom_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Globals.User_Settings.LineLengthForInterlinears = Convert.ToInt32(SpinnerLineLength.Value);
            this.Visible = false;
            //this.Close();
        }

        private void BtnCache_Click(object sender, EventArgs e)
        {
            // Save contents for step undo
            Globals.undoTopStack.Push(RtbTextTop.Text);
            Globals.undoBottomStack.Push(RtbTextBottom.Text);
            if (RtbTextTop.Text.Length < 0 | RtbTextBottom.Text.Length < 0) { return; }

            try
            {
                // Extract characters before "§" and remove "§", Save resulting interlinear section
                string[] parts = RtbTextTop.Text.Split('§', (char)StringSplitOptions.RemoveEmptyEntries);
                string extractedText = parts.Length > 0 ? parts[0] : "";
                extractedText += "\r\n";
                extractedText = extractedText.Replace("§", "");
                // Format extracted text as needed (e.g., encoding)
                string formattedText = FormatText(extractedText);
                if (!string.IsNullOrEmpty(formattedText))
                {
                    Globals.storedSteps += formattedText;
                    RtbTextTop.Text = parts.Length > 1 ? parts[1].TrimStart('\r', '\n', ' ') : "";
                }
                if (RtbTextTop.Text.Length < 1 & Globals.storedSteps.Length > 0)
                {
                    // Remove any extra carriage returns
                    Globals.storedSteps = Regex.Replace(Globals.storedSteps, @"(\r\n|\r|\n){3}", "$1$1");
                    // Get text
                    RtbTextTop.Text = Globals.storedSteps + "";
                    Globals.storedSteps = string.Empty;
                }
                ScrollToTop();
                //RtbTextTop.ZoomFactor = Zoom;
                //RtbTextBottom.ZoomFactor = Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPasteIntoBottom_Click(object sender, EventArgs e)
        {
            ResetLabelsAndButton();

            try
            {
                // Create a new PictureBox and set its properties
                var newPictureBox = new PictureBox
                {
                    Name = pictureBox2.Name,
                    Size = pictureBox2.Size,
                    Location = pictureBox2.Location,
                    SizeMode = pictureBox2.SizeMode,
                    Dock = pictureBox2.Dock
                };

                // Dispose of the current PictureBox and replace it with the new one
                if (pictureBox2.Image != null)
                {
                    pictureBox2.Image.Dispose();
                }
                pictureBox2.Dispose();
                this.Controls.Remove(pictureBox2);
                this.Controls.Add(newPictureBox);

                // Set the new PictureBox as pictureBox2
                pictureBox2 = newPictureBox;

                // Paste image from clipboard
                if (Clipboard.ContainsImage())
                {
                    var clipboardImage = Clipboard.GetImage();
                    if (clipboardImage != null)
                    {
                        // Ensure proper disposal and caching
                        Globals.cachedImageBottom?.Dispose();
                        Globals.cachedImageBottom = (Image)clipboardImage.Clone();

                        // Resize the image to fit the PictureBox
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox2.Image = clipboardImage; // Display the image in PictureBox

                        // Force PictureBox to repaint and update layout
                        pictureBox2.Invalidate();
                        pictureBox2.Update();
                        pictureBox2.Refresh();

                        System.Windows.Forms.Application.DoEvents();

                        BtnGetTextBottom.PerformClick();
                        BtnGetTextBottom.Enabled = false;
                        System.Windows.Forms.Application.DoEvents();
                    }
                    else
                    {
                        MessageBox.Show("Clipboard does not contain an image.");
                    }
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain an image.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnGetTextBottom.Enabled = true;
            }
        }

        private void BtnInsertSectionMarker_Click(object sender, EventArgs e)
        {
            RtbTextTop.Focus();
            int insertionPoint = RtbTextTop.SelectionStart;
            RtbTextTop.Text = RtbTextTop.Text.Insert(insertionPoint, "§" + Environment.NewLine + Environment.NewLine);
            RtbTextTop.Select(insertionPoint + 1, 0);
        }

        private void BtnInsertDivMarker_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.Focus();
            int insertionPoint = Globals.Current_RTB_withFocus.SelectionStart;
            Globals.Current_RTB_withFocus.Text = Globals.Current_RTB_withFocus.Text.Insert(insertionPoint, "ↆ" + Environment.NewLine + Environment.NewLine);
            Globals.Current_RTB_withFocus.Select(insertionPoint + 1, 0);
        }

        private void RtbTextBottom_MouseClick(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = RtbTextBottom;
        }

        private void RtbTextTop_MouseClick(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = RtbTextTop;
        }

        private void RtbTextTop_Enter(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RtbTextTop;
        }

        private void RtbTextBottom_Enter(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = this.RtbTextBottom;
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                RtbTextTop.Text = string.Empty;
                RtbTextBottom.Text = string.Empty;
                if (Globals.undoTopStack.Count > 0)
                {
                    RtbTextTop.Text = Globals.undoTopStack.Pop();
                }
                if (Globals.undoBottomStack.Count > 0)
                {
                    RtbTextBottom.Text = Globals.undoBottomStack.Pop();
                }

                lblTop.ForeColor = Color.Black;  // Set both labels back to black
                lblBottom.ForeColor = Color.Black;
                BtnFullMerge.ForeColor = Color.Black;
                ScrollToTop();
                //RtbTextTop.ZoomFactor = Zoom;
                //RtbTextBottom.ZoomFactor = Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BtnGetImageTop_Click(object sender, EventArgs e)
        {
            lblTop.Text = "Top = 0";
            lblBottom.Text = "Bot = 0";
            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";

            try
            {
                string pathLastFolderAccessed = Globals.User_Settings.LastFolderPathToOCRImageTop;
                OpenFileDialog openFileDialog = new()
                {
                    // image filters
                    Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
                };

                // Set initial directory to the last accessed folder path, if available
                if (!string.IsNullOrEmpty(pathLastFolderAccessed))
                {
                    openFileDialog.InitialDirectory = pathLastFolderAccessed;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Dispose of previously loaded image (if any)
                    pictureBox1.Image?.Dispose();

                    var image = new Bitmap(openFileDialog.FileName);
                    this.pictureBox1.Image = image;
                    Globals.cachedImageTop?.Dispose();

                    Globals.cachedImageTop = new Bitmap(image); // Assuming a copy is needed

                    // Adjust image to fit the PictureBox
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    string filePath = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        var directoryName = Path.GetDirectoryName(filePath);
                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            Globals.User_Settings.LastFolderPathToOCRImageTop = directoryName;
                            this.Text = "Tachufind OCR   " + filePath;
                        }
                        else
                        {
                            MessageBox.Show("Directory path is invalid.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File does not exist at the specified path.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (pictureBox1.Image != null)
                {
                    BtnGetTextTop.PerformClick();
                    BtnGetTextTop.Enabled = false;
                    System.Windows.Forms.Application.DoEvents();
                }
                BtnGetTextTop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetImageBottom_Click(object sender, EventArgs e)
        {
            lblTop.Text = "Top = 0";
            lblBottom.Text = "Bot = 0";
            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";

            try
            {
                string pathLastFolderAccessed = Globals.User_Settings.LastFolderPathToOCRImageTop;
                OpenFileDialog openFileDialog = new()
                {
                    // image filters
                    Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
                };

                // Set initial directory to the last accessed folder path, if available
                if (!string.IsNullOrEmpty(pathLastFolderAccessed))
                {
                    openFileDialog.InitialDirectory = pathLastFolderAccessed;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Dispose of previously loaded image (if any)
                    pictureBox2.Image?.Dispose();


                    var image = new Bitmap(openFileDialog.FileName);
                    this.pictureBox2.Image = image;

                    Globals.cachedImageBottom?.Dispose();

                    Globals.cachedImageBottom = new Bitmap(image); // Assuming a copy is needed

                    // Adjust image to fit the PictureBox
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                    string filePath = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        var directoryName = Path.GetDirectoryName(filePath);
                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            Globals.User_Settings.LastFolderPathToOCRImageBottom = directoryName;
                            this.Text = "Tachufind OCR   " + filePath;
                        }
                        else
                        {
                            MessageBox.Show("Directory path is invalid.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File does not exist at the specified path.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (pictureBox1.Image != null)
                {
                    BtnGetTextBottom.PerformClick();
                    BtnGetTextBottom.Enabled = false;
                    System.Windows.Forms.Application.DoEvents();
                }
                BtnGetTextBottom.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetLengthTop_Click(object sender, EventArgs e)
        {
            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";

            Globals.undoTopStack.Push(RtbTextTop.Text);
            Globals.undoBottomStack.Push(RtbTextBottom.Text);

            // Call the ModifyTextToDesiredLength method using the instance
            RtbTextTop.Text = SetLineLengthToDesiredLength.ModifyTextToDesiredLength((int)SpinnerLineLength.Value, RtbTextTop.Text);
            int bottomLineLength = (int)SpinnerLineLength.Value;
            RtbTextBottom.Text = SetLineLengthToDesiredLength.ModifyTextToDesiredLength(bottomLineLength, RtbTextBottom.Text);

            revertTop = RtbTextTop.Text;
            revertBottom = RtbTextBottom.Text;
            ScrollToTop();
        }

        // INSTALL VIA NEWGET PACKAGE MANAGER
        // EXPERIMENTAL - THIS WILL NEED TO BE CHECKED, AS CURRENT OCR MAY ALREADY DO THIS,
        // SO THIS NEEDS TO BE CHECKED TO SEE IF IT INDEED IMPROVES RESULTS.
        //using OpenCvSharp;
        //public Bitmap PreprocessImage(string imagePath)
        //    {
        //        using (var src = new Mat(imagePath, ImreadModes.Grayscale))
        //        {
        //            // Binarize the image
        //            Cv2.Threshold(src, src, 150, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

        //            // Denoise the image
        //            Cv2.FastNlMeansDenoising(src, src, 30, 7, 21);

        //            // Convert to Bitmap
        //            Bitmap bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(src);
        //            return bmp;
        //        }
        //    }

        private void PasteIntoTop_Click(object sender, EventArgs e)
        {
            ResetLabelsAndButton();

            try
            {
                // Create a new PictureBox and set its properties
                var newPictureBox = new PictureBox
                {
                    Name = pictureBox1.Name,
                    Size = pictureBox1.Size,
                    Location = pictureBox1.Location,
                    SizeMode = pictureBox1.SizeMode,
                    Dock = pictureBox1.Dock
                };

                // Dispose of the current PictureBox and replace it with the new one
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Dispose();
                this.Controls.Remove(pictureBox1);
                this.Controls.Add(newPictureBox);

                // Set the new PictureBox as pictureBox1
                pictureBox1 = newPictureBox;

                // Paste image from clipboard
                if (Clipboard.ContainsImage())
                {
                    var clipboardImage = Clipboard.GetImage();
                    if (clipboardImage != null)
                    {
                        // Ensure proper disposal and caching
                        Globals.cachedImageTop?.Dispose();
                        Globals.cachedImageTop = (Image)clipboardImage.Clone();

                        // Resize the image to fit the PictureBox
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = clipboardImage; // Display the image in PictureBox

                        // Force PictureBox to repaint and update layout
                        pictureBox1.Invalidate();
                        pictureBox1.Update();
                        pictureBox1.Refresh();

                        System.Windows.Forms.Application.DoEvents();

                        BtnGetTextTop.PerformClick();
                        BtnGetTextTop.Enabled = false;
                        System.Windows.Forms.Application.DoEvents();
                    }
                    else
                    {
                        MessageBox.Show("Clipboard does not contain an image.");
                    }
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain an image.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnGetTextTop.Enabled = true;
            }
        }

        private Bitmap PreprocessImage(Bitmap bitmap)
        {
            // Convert Bitmap to Mat
            using Mat src = BitmapConverter.ToMat(bitmap);
            // Convert to grayscale
            using Mat gray = new();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            // Binarize the image
            using Mat binary = new();
            Cv2.Threshold(gray, binary, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            // Denoise the image
            using Mat denoised = new();
            Cv2.FastNlMeansDenoising(binary, denoised);
            // Convert Mat back to Bitmap
            Bitmap result = BitmapConverter.ToBitmap(denoised);
            return result;
        }



        private void BtnGetTextTop_Click(object sender, EventArgs e)
        {
            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";
            BtnGetTextTop.Enabled = false;
            var text = string.Empty;

            try
            {
                string languageCode = GetLanguageIndicator(CmbLanguage1.SelectedIndex);

                // OVERRIDE if Multi-Lingual Mode is checked
                if (CmbLanguage2.SelectedIndex != 0)
                {
                    // languageCode = "ell+eng+polyton";
                    languageCode = GetLanguageIndicator(CmbLanguage1.SelectedIndex) + "+"
                        + GetSecondaryLanguageIndicator(CmbLanguage2.SelectedIndex) + "+polyton";
                }

                if (this.pictureBox1.Image == null)
                {
                    MessageBox.Show("The image is null or could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var image = this.pictureBox1.Image;
                    Bitmap bitmap = new(image);
                    bitmap.SetResolution(300, 300);

                    // Preprocess image using OpenCvSharp
                    Bitmap preprocessedBitmap = PreprocessImage(bitmap);


                    using var stream = new MemoryStream();
                    {
                        if (ChkPreProcess.Checked)
                        {
                            preprocessedBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else
                        {
                            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        stream.Position = 0;

                        using var pix = Pix.LoadFromMemory(stream.ToArray());
                        {
                            string path = Path.Combine(GeneralFns.GetProjectRoot(), "tessdata");

                            //MessageBox.Show("Path = " + path);  // For troubleshooting
                            using var engine = new Tesseract.TesseractEngine(path, languageCode, Tesseract.EngineMode.Default);
                            {
                                using var page = engine.Process(pix);
                                {
                                    text = page.GetText();
                                }
                            }
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("The image is too large or the system is out of memory.", "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("The image format is not supported or the image is corrupted.", "Invalid Image Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Globals.cachedImageTop != null)
                {
                    pictureBox1.Image = new Bitmap(Globals.cachedImageTop);
                    pictureBox1.Refresh();
                }

                // Fix very common GREEK, Latin, etc. misinterpretation OCR does
                text = AlterTextBasedOnLanguage(text, languageCode);

                // Remove interior dashes, trim the result, and directly assign to RtbTextBottom
                text = RemoveInteriorDashesFromWords(text);
                revertBottom = text;
                RtbTextTop.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnGetTextTop.Enabled = true;
                RtbTextTop.Refresh();
                ScrollToTop();
            }
        }


        private void BtnGetTextBottom_Click(object sender, EventArgs e)
        {

            btnSplit.ForeColor = Color.Black;
            btnSplit.Text = "Split";
            BtnGetTextBottom.Enabled = false;
            var text = string.Empty;

            try
            {
                string languageCode = GetLanguageIndicator(CmbLanguage3.SelectedIndex);

                if (this.pictureBox2.Image == null)
                {
                    MessageBox.Show("Please load an image before attempting to get text.");
                    BtnGetTextBottom.Enabled = true;
                    return;
                }

                try
                {
                    var image = this.pictureBox2.Image;
                    Bitmap bitmap = new(image);
                    bitmap.SetResolution(300, 300);

                    using var stream = new MemoryStream();
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        using var pix = Pix.LoadFromMemory(stream.ToArray());
                        {
                            using var engine = new Tesseract.TesseractEngine(tessdata_Path, languageCode, Tesseract.EngineMode.Default);
                            {
                                using var page = engine.Process(pix);
                                {
                                    text = page.GetText();
                                }
                            }
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("The image is too large or the system is out of memory.", "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("The image format is not supported or the image is corrupted.", "Invalid Image Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Fix common error using Replace function
                text = AlterTextBasedOnLanguage(text, languageCode);
                // Remove interior dashes, trim the result, and directly assign to RtbTextBottom
                text = RemoveInteriorDashesFromWords(text);
                revertBottom = text;
                RtbTextBottom.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Globals.cachedImageBottom != null) { pictureBox2.Image = new Bitmap(Globals.cachedImageBottom); }
                BtnGetTextBottom.Enabled = true;
                RtbTextTop.Refresh();
                ScrollToTop();
            }
        }

        private void BtnClearCache_Click(object sender, EventArgs e)
        {
            Globals.storedSteps = string.Empty;
        }
        #endregion // Buttons__Click


        #region Events

        private void ChkTTSBrackets_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.StateOfOCRTTSBrackets = ChkTTSBrackets.Checked.ToString();
        }

        private void FrmOCR_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposePictureBoxImages();
            this.Visible = false;
            e.Cancel = true;
            FrmMain frmMain = new();
            Globals.Current_RTB_withFocus = frmMain.RTBMain;
            frmMain.BringToFront();
            frmMain.RTBMain.Focus();
            frmMain.RTBMain.SelectionStart = Globals.User_Settings.CursorPosition;

            // Dispose of any images
            Globals.cachedImageTop?.Dispose();
            Globals.cachedImageBottom?.Dispose();

            // Top to bottom
            Globals.User_Settings.OCR_Language1 = CmbLanguage1.Text;
            Globals.User_Settings.OCR_Language3 = CmbLanguage3.Text;

            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmOCRLocation = location);
        }

        private void FrmOCR_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmOCRLocation = location);
        }

        private void btnAcute_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetAcuteAccent(Globals.Current_RTB_withFocus);
        }

        private void btnGrave_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetGraveAccent(Globals.Current_RTB_withFocus);
        }

        private void btnTsus_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ß";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnDotProd_Click(object sender, EventArgs e)
        {
            if (Globals.Current_RTB_withFocus != null)
            {
                Globals.Current_RTB_withFocus.SelectedText = "˙";
                int cursorLocation = Globals.Current_RTB_withFocus.SelectionStart;
                Globals.Current_RTB_withFocus.SelectionStart = cursorLocation - 1;
                Globals.Current_RTB_withFocus.SelectionLength = 1;
                if (Globals.Current_RTB_withFocus.SelectionFont != null)
                {
                    Globals.Current_RTB_withFocus.SelectionFont = new System.Drawing.Font(Globals.Current_RTB_withFocus.SelectionFont, FontStyle.Bold);
                }
                Globals.Current_RTB_withFocus.SelectionStart = cursorLocation;
                Globals.Current_RTB_withFocus.SelectionLength = 0;
                Globals.Current_RTB_withFocus.Focus();
            }
        }

        private void btnCircumflex_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetCircumflex(Globals.Current_RTB_withFocus);
        }

        private void btnUmlaut_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetUmlaut(Globals.Current_RTB_withFocus);
        }

        private void btnMacron_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetMacron(Globals.Current_RTB_withFocus);
        }

        private void btnShort_Click(object sender, EventArgs e)
        {
            KeyboardShortcuts keyboardShortcuts = new();
            keyboardShortcuts.SetShort(Globals.Current_RTB_withFocus);
        }

        private void BtnInsertFancyQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "“”";
            Globals.Current_RTB_withFocus.SelectionStart -= 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnSingleQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ʽʼ";
            Globals.Current_RTB_withFocus.SelectionStart -= 1; // Move cursor between brackets
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnBrackets_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "[]";
            Globals.Current_RTB_withFocus.SelectionStart -= 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnInsertParenthesis_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "()";
            Globals.Current_RTB_withFocus.SelectionStart -= 1;
            Globals.Current_RTB_withFocus.Focus();
        }

        private void BtnInsertFrenchQuotes_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "«»"; // Insert brackets
            Globals.Current_RTB_withFocus.SelectionStart -= 1; // Move cursor between brackets
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnCedilla_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ç";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnSpNye_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "ñ";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnSpQuestionMk_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¿";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnSpExclaim_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "¡";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void btnLongLine_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus.SelectedText = "—";
            Globals.Current_RTB_withFocus.Focus();
        }

        private void RtbTextTop_MouseMove(object sender, MouseEventArgs e)
        {
            RtbTextTop.Cursor = Cursors.Default;
        }

        private void RtbTextBottom_MouseMove(object sender, MouseEventArgs e)
        {
            RtbTextBottom.Cursor = Cursors.Default;
        }


        #endregion //  Events








        //
    }
}

//int limit = (int)Math.Ceiling(result);
// Create a  deep copy of  the Queues
// Queue<string> deepTopTempQueue = GeneralFns.DeepCopyQueue(Globals.topSentenceQueue);

