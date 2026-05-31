using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Tachufind
{
    internal partial class FileIO
    {
        public static bool CheckPath(string path) 
        { 
        if (string.IsNullOrEmpty(path)) 
        { 
            MessageBox.Show("The specified path is null or empty.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            return false; 
        } 
        if (!Directory.Exists(path)) 
        { 
            string message = $"The specified path is invalid:\n\nInvalid path: {path}"; 
            MessageBox.Show(message, "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; 
        } 
            return true; 
        }



        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filePath">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static string GetEncoding(string filePath)
        {
            try
            { 
                // Read the BOM
                var bom = new byte[4];
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    file.Read(bom, 0, 4);
                }

                // Analyze the BOM
                if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return "UTF7";  // Encoding.UTF7;
                if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return "UTF8";
                if (bom[0] == 0xff && bom[1] == 0xfe) return "Unicode"; //UTF-16LE
                if (bom[0] == 0xfe && bom[1] == 0xff) return "BigEndianUnicode"; //UTF-16BE
                if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return "UTF32";
                return "ASCII";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }



        public static string ReadFile(string path, bool createIfNotPresent = true)
        {
            string fileContent = "";
            try
            {
                if (File.Exists(path))
                {
                    // Use simplified using declaration
                    using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                    // fdta and qdata should never be stored in this setting                    
                    string fileExtension = Path.GetExtension(path).ToUpper();
                    if (fileExtension != ".FDTA" && fileExtension != ".QDTA")
                    {
                        Globals.User_Settings.FilePath = path;
                    }                    
                    // Use UTF-8 encoding explicitly (or another encoding as needed)
                    using var sr = new StreamReader(fs, Encoding.UTF8);
                    {
                        fileContent = sr.ReadToEnd();
                    }
                }
                else
                {
                    if (!createIfNotPresent)
                    {
                        AppSettings.FileNotFound = true;
                        string message = $"The file you are attempting to open located at: {path} no longer exists.";
                        MessageBox.Show(message, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.StackTrace}{Environment.NewLine}Exception: {ex}";
                MessageBox.Show(errorMessage, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContent;
        }




        // OVERLOADED Text files (non-RichTextBox), UNICODE
        public static void WriteFile(string? path, string txtStrng, bool acknowledge = false)
        {
            if (path == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(path)); // Use throw helper
            }

            try
            {
                // Additional validation to ensure path is valid
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentException("Path cannot be empty or whitespace.", nameof(path));
                }

                var routePath = Path.GetDirectoryName(path) ?? string.Empty;
                if (!Directory.Exists(routePath))
                {
                    Directory.CreateDirectory(routePath);
                }

                // Use simplified using declaration with var and ensure UTF-8 without BOM
                using var sw = new StreamWriter(path, false, new UTF8Encoding(false));
                sw.Write(txtStrng);

                if (acknowledge)
                {
                    MessageBox.Show("File saved.");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.StackTrace}{Environment.NewLine}Exception: {ex}";
                MessageBox.Show(errorMessage, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public static class ThrowHelper
        {
            public static void ThrowArgumentNullException(string paramName)
            {
                throw new ArgumentNullException(paramName);
            }
        }





        // Helper method to handle different file types
        public static string LoadFileContents(string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToUpper();
                if (extension == ".HTM") { extension = ".HTML"; } // Normalize HTML extension

                switch (extension) // Normalize to upper case for consistency
                {
                    case ".RTF":
                        var rtfBox = new RichTextBox();
                        return LoadRtfFile(filePath);

                    case ".HTML":
                        string htmlContent = ReadFile(filePath);
                        return ConvertHtmlToText(htmlContent);

                    case ".TXT":
                    default:
                        return ReadFile(filePath);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"IO Error: {ioEx.Message}", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string or handle it as needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string or handle it as needed
            }
        }

        public static string OpenFileWithDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                //openFileDialog.Filter = "RTF files (*.rtf)|*.rtf|All files (*.*)|*.*";
                openFileDialog.Filter = "Rich Text Format(*.rtf)|*.rtf|Text Files(*.txt)|*.txt|Subtitle Files(*.srt)|*.srt|All Files(*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                // Set the initial directory to the last used directory
                openFileDialog.InitialDirectory = !string.IsNullOrEmpty(AppSettings.lastUsedDirectory) ? AppSettings.lastUsedDirectory : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    AppSettings.lastUsedDirectory = Path.GetDirectoryName(openFileDialog.FileName) ?? string.Empty;
                    // Get the path of the selected file
                    return openFileDialog.FileName;
                }
                return "";
            }

        }

        public static string LoadRtfFile(string filePath)
        {
            try
            {
                AppSettings.filePath = filePath;
                string content = "";

                // Ensure the file exists
                if (File.Exists(AppSettings.filePath))
                {
                    string fileExtension = Path.GetExtension(AppSettings.filePath).ToUpper();

                    if (fileExtension == ".SRT") // Change extension to txt
                    {
                        AppSettings.filePath = SetExtensionToRtf(AppSettings.filePath); 
                    }
                    if (fileExtension == ".TXT") // Distinguish between TXT and TRF
                    {
                        content =  FileIO.LoadFileContents(filePath);
                    }
                    else 
                    {
                        content = File.ReadAllText(AppSettings.filePath);
                    }

                    // fdta and qdata should never be stored in this setting
                    if (fileExtension != ".FDTA" && fileExtension != ".QDTA")
                    {
                        Globals.User_Settings.FilePath = AppSettings.filePath;
                    }

                    return content;
                }
                else
                {
                    AppSettings.FileNotFound = true;
                    // Handle the case where the file does not exist or empty string
                    if (filePath.Length > 0) 
                    {
                        MessageBox.Show("Invalid file or file not found.");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return string.Empty;
        }

        public static string SetExtensionToRtf(string f1Path)
        {
            try
            {
                // Ensure the file is in UTF-8 format
                var encoding = FileIO.GetEncoding(f1Path);
                if (encoding != "UTF8")
                {
                    string f1 = FileIO.ReadFile(f1Path);  // Open file
                    FileIO.WriteFile(f1Path, f1); // Write the file back in UTF8 format without BOM
                }

                // Change the file extension to .rtf
                string newFilePath = Path.ChangeExtension(f1Path, ".rtf");

                // Convert the text to RTF format
                string plainText = File.ReadAllText(f1Path);
                string rtfText = ConvertToRtf(plainText);

                // Write the RTF text to the new file
                File.WriteAllText(newFilePath, rtfText);

                return newFilePath; // Return the new file path if it is modified
            }
            catch (Exception ex)
            {
                // Display the error in a message box
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return f1Path; // Return the original file path if an exception occurs
        }

        private static string ConvertToRtf(string plainText)
        {
            // Convert the plain text to RTF format
            using (var rtfConverter = new System.Windows.Forms.RichTextBox())
            {
                rtfConverter.Text = plainText;
                return rtfConverter.Rtf!;
            }
        }




        public static string ConvertHtmlToText(string source)
        {
            string result = source.Replace("\r", " ")
                                  .Replace("\n", " ")
                                  .Replace("\t", string.Empty);

            // Remove repeating spaces because browsers ignore them
            result = RegexHelpers.MultipleSpacesRegex().Replace(result, " ");

            // Remove HTML Development formatting
            result = RegexHelpers.HeadTagOpenRegex().Replace(result, "<head>");
            result = RegexHelpers.HeadTagCloseRegex().Replace(result, "</head>");
            result = RegexHelpers.HeadContentRegex().Replace(result, string.Empty);
            result = RegexHelpers.ScriptTagOpenRegex().Replace(result, "<script>");
            result = RegexHelpers.ScriptTagCloseRegex().Replace(result, "</script>");
            result = RegexHelpers.ScriptContentRegex().Replace(result, string.Empty);
            result = RegexHelpers.StyleTagOpenRegex().Replace(result, "<style>");
            result = RegexHelpers.StyleTagCloseRegex().Replace(result, "</style>");
            result = RegexHelpers.StyleContentRegex().Replace(result, string.Empty);
            result = RegexHelpers.TdTagOpenRegex().Replace(result, "\t");
            result = RegexHelpers.BrTagRegex().Replace(result, "\r");
            result = RegexHelpers.LiTagRegex().Replace(result, "\r");
            result = RegexHelpers.DivTagOpenRegex().Replace(result, "\r\r");
            result = RegexHelpers.TrTagOpenRegex().Replace(result, "\r\r");
            result = RegexHelpers.PTagOpenRegex().Replace(result, "\r\r");
            result = RegexHelpers.AllTagsRegex().Replace(result, string.Empty);
            result = RegexHelpers.NbspRegex().Replace(result, " ");
            result = RegexHelpers.BullRegex().Replace(result, " * ");
            result = RegexHelpers.LsaquoRegex().Replace(result, "<");
            result = RegexHelpers.RsaquoRegex().Replace(result, ">");
            result = RegexHelpers.TradeRegex().Replace(result, "(tm)");
            result = RegexHelpers.FraslRegex().Replace(result, "/");
            result = RegexHelpers.CopyRegex().Replace(result, "(c)");
            result = RegexHelpers.RegRegex().Replace(result, "(r)");
            result = RegexHelpers.SpecialCharsRegex().Replace(result, string.Empty);
            result = RegexHelpers.RedundantLineBreaksRegex().Replace(result, "\r\r");
            result = RegexHelpers.RedundantTabsRegex().Replace(result, "\t\t");
            result = RegexHelpers.TabsAndLineBreaksRegex().Replace(result, "\t\r");
            result = RegexHelpers.LineBreaksAndTabsRegex().Replace(result, "\r\t");
            result = RegexHelpers.MultipleTabsBetweenLineBreaksRegex().Replace(result, "\r\r");
            result = RegexHelpers.TabsAfterLineBreakRegex().Replace(result, "\r\t");

            // Make line breaking consistent
            result = result.Replace("\n", "\r");

            // Initial replacement target string for linebreaks
            string breaks = "\r\r\r";
            // Initial replacement target string for tabs
            string tabs = "\t\t\t\t\t";
            for (int index = 0; index < result.Length; index++)
            {
                result = result.Replace(breaks, "\r\r");
                result = result.Replace(tabs, "\t\t\t\t");
                breaks += "\r";
                tabs += "\t";
            }

            return result; // Ensure the method returns the result
        }



        public static void CreateDirectoryIfItDoesNotExist(string FilePath)
        {
            try
            {
                // if there is no directory, create one in the Environment.SpecialFolder.CommonApplicationData folder
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(FilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }










        // 
    }

}
