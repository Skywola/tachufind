using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    public partial class FrmInterlinear : Form
    {
        public FrmInterlinear()
        {
            InitializeComponent();
        }

        Subtitle subtitle = new Subtitle(
            1,
            "00:00:00,000 --> 00:00:05,000",
            "Top language text",
            "Bottom language text",
            "00:00:00,000 --> 00:00:03,000",
            "00:00:03,000 --> 00:00:05,000",
            "00:00:03,000",
            "00:00:05,000",
            TimeSpan.FromSeconds(1)
        );
        List<Subtitle> subtitles = new List<Subtitle>();


        string filePath = string.Empty;
        string f2Name = string.Empty;
        string buildHeader = string.Empty;
        List<string> f1List = new List<string>();
        List<string> f2List = new List<string>();
        FileIO FIO = new FileIO();
        string encoding = string.Empty;

        // SEE: AppSettings.MaxTimeDuration, AppSettings.MinTimeGap

        void BtnTop_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Empty;
            string path = Globals.User_Settings.FrmInterlinearLastLocationDir;
            if (Directory.Exists(path))
            {
                OpenFileDialog openFileDialog01 = new OpenFileDialog();
                openFileDialog01.InitialDirectory = @path; // Set desired folder path
                GetDirectoryForBtnTop(openFileDialog01);
            }
            else
            {
                OpenFileDialog openFileDialog01 = new OpenFileDialog();
                GetDirectoryForBtnTop(openFileDialog01);
            }
        }


        void GetDirectoryForBtnTop(OpenFileDialog openFileDialog)
        {
            Cursor.Current = Cursors.WaitCursor;
            openFileDialog.Title = "Open File";
            openFileDialog.DefaultExt = "srt";
            openFileDialog.Filter = "Subtitle|*.srt|Text Files|*.txt";
            // "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
            openFileDialog.FilterIndex = 1;

            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var fileName = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        // Use the null-propagation operator to get the directory path
                        string directoryPath = Path.GetDirectoryName(fileName) ?? string.Empty;

                        // Check if directoryPath is not empty
                        if (!string.IsNullOrEmpty(directoryPath))
                        {
                            Globals.User_Settings.FrmInterlinearLastLocationDir = directoryPath;
                            txtFilePath1.Text = fileName;
                            Globals.f1Path = fileName;
                        }
                        else
                        {
                            MessageBox.Show("The directory path could not be determined.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The file path could not be determined.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string m = $"Error: {ex.StackTrace} . . . .{Environment.NewLine}Exception: {ex}";
                MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        void BtnBottom_Click(object sender, EventArgs e)
        {
            string path = Globals.User_Settings.FrmInterlinearLastLocationDir;
            if (Directory.Exists(path))
            {
                OpenFileDialog openFileDialog01 = new OpenFileDialog();
                openFileDialog01.InitialDirectory = @path; // Set desired folder path
                GetDirectoryForBtnBottom(openFileDialog01);
            }
            else
            {
                OpenFileDialog openFileDialog01 = new OpenFileDialog();
                GetDirectoryForBtnBottom(openFileDialog01);
            }
        }


        void GetDirectoryForBtnBottom(OpenFileDialog openFileDialog)
        {
            Cursor.Current = Cursors.WaitCursor;
            openFileDialog.Title = "Open File";
            openFileDialog.DefaultExt = "srt";
            openFileDialog.Filter = "Subtitle|*.srt|" + "Text Files|*.txt";
            // "Rich Text Files|*.rtf|" + "Text Files|*.txt|HTML Files|" + "*.htm|All Files|*.*";
            openFileDialog.FilterIndex = 1;

            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var fileName = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        // Use the null-propagation operator to get the directory path
                        string directoryPath = Path.GetDirectoryName(fileName) ?? string.Empty;

                        // Check if directoryPath is not empty
                        if (!string.IsNullOrEmpty(directoryPath))
                        {
                            Globals.User_Settings.FrmInterlinearLastLocationDir = directoryPath;
                            txtFilePath2.Text = fileName;
                            Globals.f2Path = fileName;
                        }
                        else
                        {
                            MessageBox.Show("The directory path could not be determined.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The file path could not be determined.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string m = "Error:" + ex.StackTrace + ". . . ." + Environment.NewLine + " Exception:  " + ex.ToString();
                MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        public void SetUTF8(string f1Path)
        {
            try
            {
                var encoding = FileIO.GetEncoding(f1Path);
                if (encoding != "UTF8")
                {
                    string f1 = FileIO.ReadFile(f1Path);  // Open file
                    FileIO.WriteFile(f1Path, f1); // Write the file back in UTF8 format without BOM
                }
            }
            catch (Exception ex)
            {
                // Display the error in a message box
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static string SetFileName(int fNum1, int fNum2)
        {
            string fileName = "";

            if (fNum1 == -1 | fNum2 == -1)
            {
                fileName = "00Interlinear.srt";  // One file  only
                return fileName;
            }
            else
            {
                if (fNum1 == fNum2)
                {
                    if (fNum1 < 10)
                    {
                        fileName = "00Interlinear-Vol0" + fNum1 + ".srt";
                        return fileName;
                    }
                    fileName = "00Interlinear-Vol" + fNum1 + ".srt";
                    return fileName;
                }
                else
                {
                    // Mismatch in file numbers.
                    // message_mismatched_file_numbers(file1_name, file2_name);
                    // Display the mismatch
                    return "0";
                }
            }
        }

        void BtnCreate_Click(object sender, EventArgs e)
        {
            SubtitleProcessor processor = new SubtitleProcessor(); // Create an instance of SubtitleProcessor

            try
            {
                SetUTF8(Globals.f1Path);
                SetUTF8(Globals.f2Path);

                string f1 = FileIO.ReadFile(Globals.f1Path); // Open file 
                string f2 = FileIO.ReadFile(Globals.f2Path); // Open file

                // Parse subtitles into SubtitleLine objects
                subtitles = processor.ParseTopSubtitles(f1);

                // Add bottom subtitles to existing SubtitleLine objects
                processor.AddBottomSubtitles(subtitles, f2);

                subtitles = CleanSubtitleLines(subtitles);
                subtitles = EnforceMinTimeGapBetweenSubtitles(subtitles);
                subtitles = MaxSubtitleDisplayTimes(subtitles);
                string outputString = GenerateOutput(subtitles);
                ProcessSubtitlesPathsAndSave(outputString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.StackTrace}{Environment.NewLine}Exception: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (subtitles != null) subtitles.Clear();
                Cursor.Current = Cursors.Default;
            }
        }

        private List<Subtitle> CleanSubtitleLines(List<Subtitle> subtitleLines)
        {
            foreach (var subtitleLine in subtitleLines)
            {
                subtitleLine.TopLanguageText = CleanText(subtitleLine.TopLanguageText).Trim();
                subtitleLine.BottomLanguageText = CleanText(subtitleLine.BottomLanguageText).Trim();
            }
            return subtitleLines;
        }

        private string CleanText(string text)
        {
            // Replace unwanted characters and tags
            text = text.Replace("...", "")
                       .Replace("<i>", "")
                       .Replace("</i>", "")
                       .Replace("\r", "")
                       .Replace("\n", " ");

            // Use regular expressions to ensure correct spacing around punctuation
            text = Regex.Replace(text, @"([.,!?])", "$1 ");
            text = Regex.Replace(text, @"\s+", " "); // Replace multiple spaces with a single space

            return text.Trim(); // Remove leading/trailing spaces
        }


        private List<Subtitle> EnforceMinTimeGapBetweenSubtitles(List<Subtitle> subtitles)
        {
            int lines = 0;
            try
            {
                for (int i = 0; i < subtitles.Count - 1; i++)
                {
                    lines = i;
                    var currentSubtitle = subtitles[i];
                    var nextSubtitle = subtitles[i + 1];

                    // Parse the time ranges
                    TimeSpan currentEndTime = ParseTime(currentSubtitle.TimeRange.Split(" --> ")[1]);
                    TimeSpan nextStartTime = ParseTime(nextSubtitle.TimeRange.Split(" --> ")[0]);

                    // Ensure there is a minimum gap by adjusting the end time of the current subtitle
                    if (currentEndTime >= nextStartTime)
                    {
                        currentEndTime = nextStartTime - AppSettings.MinTimeGap;
                        currentSubtitle.TimeRange = $"{currentSubtitle.TimeRange.Split(" --> ")[0]} --> {currentEndTime.ToString(@"hh\:mm\:ss\,fff")}";
                    }
                }

                return subtitles;
            }
            catch (Exception ex)
            {
                HandleSubtitleError(lines, ex);
                return subtitles; // Return the list even if an error occurs
            }
        }

        private TimeSpan ParseTime(string time)
        {
            return TimeSpan.ParseExact(time, @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture);
        }

        private List<Subtitle> MaxSubtitleDisplayTimes(List<Subtitle> subtitles)
        {
            int lines = 0;
            try
            {
                for (int i = 0; i < subtitles.Count - 1; i++)
                {
                    lines = i;
                    var currentSubtitle = subtitles[i];
                    var nextSubtitle = subtitles[i + 1];

                    // Parse the time ranges
                    TimeSpan currentStartTime = ParseTime(currentSubtitle.TimeRange.Split(" --> ")[0]);
                    TimeSpan currentEndTime = ParseTime(currentSubtitle.TimeRange.Split(" --> ")[1]);
                    TimeSpan nextStartTime = ParseTime(nextSubtitle.TimeRange.Split(" --> ")[0]);

                    // Calculate the maximum possible end time without exceeding the maximum duration or overlapping the next subtitle
                    TimeSpan maxPossibleEndTime = currentStartTime + AppSettings.MaxTimeDuration;
                    TimeSpan adjustedEndTime = nextStartTime - AppSettings.MinTimeGap;

                    if (maxPossibleEndTime <= adjustedEndTime)
                    {
                        currentEndTime = maxPossibleEndTime;
                    }
                    else
                    {
                        currentEndTime = adjustedEndTime;
                    }

                    // Update the current subtitle's time range
                    currentSubtitle.TimeRange = $"{currentStartTime.ToString(@"hh\:mm\:ss\,fff")} --> {currentEndTime.ToString(@"hh\:mm\:ss\,fff")}";
                }
                return subtitles;
            }
            catch (Exception ex)
            {
                HandleSubtitleError(lines, ex);
                return subtitles; // Return the list even if an error occurs
            }
        }

        private string GenerateOutput(List<Subtitle> subtitles)
        {
            StringBuilder outputBuilder = new StringBuilder();
            string crlf = Environment.NewLine;

            foreach (var subtitleLine in subtitles)
            {
                // Ensure proper trimming of each line's text
                string lineNumber = subtitleLine.LineNumber.ToString().Trim();
                string timeSRT = subtitleLine.TimeRange.Trim();
                string subtitleTop = subtitleLine.TopLanguageText.Trim();
                string subtitleBottom = subtitleLine.BottomLanguageText.Trim();

                // Add each subtitle block to the output, ensuring a blank line between blocks
                outputBuilder.AppendLine($"{lineNumber}{crlf}{timeSRT}{crlf}{subtitleTop}{crlf}{subtitleBottom}{crlf}");
            }

            return outputBuilder.ToString();
        }

        private void HandleSubtitleError(int line, Exception ex)
        {
            // Populate the textboxes with the line number and error message
            TxtLineNumber.Text = "Error between lines " + line.ToString() + "and " + (line + 1).ToString();
            txtErrorList.Text = ex.Message;

            // Show a user-friendly message
            MessageBox.Show("An error occurred. Please check the subtitle file for formatting issues and try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ProcessSubtitlesPathsAndSave(string outputFile)
        {
            var path1 = Path.GetDirectoryName(Globals.f1Path);
            var path2 = Path.GetDirectoryName(Globals.f2Path);

            // Ensure paths are not null before checking them
            if (path1 == null || path2 == null)
            {
                MessageBox.Show("One or both of the paths could not be determined.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!FileIO.CheckPath(path1) || !FileIO.CheckPath(path2))
            {
                return;
            }

            filePath = Path.Combine(path1, AppConstants.SRT_FILENAME);
            try
            {
                FileIO.WriteFile(filePath, outputFile);
                txtResult.Text = "Completed!";
                Globals.currentTimeRange = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.StackTrace}{Environment.NewLine}Exception: {ex}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string EvenlyDistributeSpaces(string text, int targetLength)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int totalSpacesNeeded = targetLength - text.Length;
            int spaceSlots = words.Length - 1;

            if (spaceSlots > 0)
            {
                int spacesPerSlot = totalSpacesNeeded / spaceSlots;
                int extraSpaces = totalSpacesNeeded % spaceSlots;

                for (int i = 0; i < spaceSlots; i++)
                {
                    words[i] += new string(' ', spacesPerSlot + (i < extraSpaces ? 1 : 0));
                }
            }
            return string.Join(" ", words);
        }



        static string SplitStringTimes(string input, int part, out string[] bothTimes)
        {
            bothTimes = input.Split(new string[] { " --> " }, StringSplitOptions.None);

            if (bothTimes.Length != 2)
            {
                throw new ArgumentException($"Input '{input}' does not contain exactly two times separated by ' --> '", nameof(input));
            }

            return part == 0 ? bothTimes[0] : bothTimes[1];
        }

        static string FilterLineNumber(string num)
        {
            if (string.IsNullOrEmpty(num)) { return string.Empty; }

            string numout = string.Empty;
            num = num.Replace("eleven", "11");
            num = num.Replace("thirty", "30");
            num = num.Replace("0.", "0");
            num = num.Replace("1.", "1");
            num = num.Replace("2.", "2");
            num = num.Replace("3.", "3");
            num = num.Replace("4.", "4");
            num = num.Replace("5.", "5");
            num = num.Replace("6.", "6");
            num = num.Replace("7.", "7");
            num = num.Replace("8.", "8");
            num = num.Replace("9.", "8");
            num = num.Replace("6th", "6");
            numout = num.Replace("7th", "7");
            return numout;
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInterlinear_Load(object sender, EventArgs e)
        {
            ScreenSetUp(this);
        }

        private void ScreenSetUp(Form form)
        {
            Point savedLocation = Globals.User_Settings.FrmInterlinearLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        private void FrmInterlinear_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmInterlinearLocation = location);
        }

        private void FrmInterlinear_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmInterlinearLocation = location);
        }

        private void txtFilePath1_MouseMove(object sender, MouseEventArgs e)
        {
            txtFilePath1.Cursor = Cursors.Default;
        }

        private void txtFilePath2_MouseMove(object sender, MouseEventArgs e)
        {
            txtFilePath2.Cursor = Cursors.Default;
        }

        private void txtResult_MouseMove(object sender, MouseEventArgs e)
        {
            txtResult.Cursor = Cursors.Default;
        }

        private void TxtLineNumber_MouseMove(object sender, MouseEventArgs e)
        {
            TxtLineNumber.Cursor = Cursors.Default;
        }

        private void txtErrorList_MouseMove(object sender, MouseEventArgs e)
        {
            txtErrorList.Cursor = Cursors.Default;
        }
    }
}
