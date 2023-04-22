using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Tachufind;
using System.Configuration;


// Note that this uses the InStr method, which requires the use
// if the indexof method in C#
//TestPos = InStr(1, SearchString, SearchChar, CompareMethod.Binary)

public class GeneralFns
{

    public static string DoTrim(string param)
	{
		param = param.Replace(" ", "");
		return param;
	}

	// If test = "1234567" & length = 4 then "1234"
	public static string DoLeft(string param, int length)
	{
		if (param == null) { return ""; }
		if (param.Length < length | length < 0) { return ""; }
		string result = param.Substring(0, length);
		return result;
	}

	// If test = "1234567" & length = 4 then "4567"
	public static string DoRight(string param, int length)
	{
		if (param == null) { return ""; }
		if (param.Length < length | length < 0) { return ""; }
		string result = param.Substring(param.Length - length, length);
		return result;
	}

	// If test = "1234567" & startIndex = 5 length = 2 then "67"
	public static string DoMid(string param, int startIndex, int length)
	{
		if (param == null) { return ""; }
		if (param.Length < length) { return ""; }
		if (startIndex < 0 | startIndex > param.Length) { return ""; }
		if (param.Length < startIndex + length) { return ""; }
		string result = param.Substring(startIndex, length);
		return result;
	}


	// 0 is start index and can be modified.
	// If test = "strafe" & sValue = "af" then 3
	public static int DoInStr(string sText, string findString)
	{
		if (sText == null) { return -1; }
		if (findString == null) { return -1; }
		int iVal = (int)sText.IndexOf(findString, StringComparison.CurrentCulture);
		return iVal;
	}

	// Looks for the findString from the position given
	// If test = "strafe" & pos = 4,  ch = 'af' then -1
	// If test = "strafe" & pos = 3,  ch = 'af' then 3
	public static int DoInStr(int start, string sText, string findString)
	{
		if (sText == null) { return -1; }
		if (findString == null) { return -1; }
		if (sText.Length < start) { return -1; }
		if (sText.Length < findString.Length) { return -1; }
		int iVal = (int)sText.IndexOf(findString, start, StringComparison.CurrentCulture); // 0 is start index and can be modified.
		return iVal;
	}

	// if iText = "strafe" & oldStr = "tr", newStr = "", then "safe"
	public static string RReplace(string sText, string oldStr, string newStr)
	{
		if (sText == null) { return ""; }
		if (oldStr == null) { return ""; }
		if (newStr == null) { return ""; }
		if (sText.Length < oldStr.Length) { return ""; }
		string strVal = sText.Replace(oldStr, newStr); // 0 is start index and can be modified.
		return strVal;
	}

	public static Boolean IsNumericOnly(string strToCheck)
	{
		// new Regex(@"[a-zA-Z]") // for letters only
		Regex rg = new Regex(@"^\d+$");
		return rg.IsMatch(strToCheck);
	}

	public static Boolean IsLettersOnly(string strToCheck)
	{
		Regex rg = new Regex(@"[a-zA-Z]");
		return rg.IsMatch(strToCheck);
	}


    /// </summary>
    /// <param name="rtb"></param>
    /// <param name="amount"></param>
    /// <param name="size"></param>
	public void CreateSuperorSubScript(RichTextBox rtb, int amount, int size)
	{
		if (rtb.SelectionLength < 1) { return; }
		//Font fontname = rtb.Font;
		string fontname = rtb.Font.Name;
		int selStart = rtb.SelectionStart;
		int selLength = rtb.SelectionLength;
		float fontSize = rtb.SelectionFont.Size;
		rtb.SelectionCharOffset = amount;
		rtb.SelectionFont = new Font(rtb.Font.Name, size, FontStyle.Bold);
		rtb.SelectionStart = rtb.SelectionStart + 3;
		//rtb.SelectionStart = rtb.SelectionLength + 1;
		rtb.SelectionFont = new Font(fontname, fontSize, FontStyle.Regular);
		rtb.SelectionLength = 0;
		rtb.SelectionStart = selStart;

	}

	public static List<string> GetListOfFilesInDirectory(string directory, string fileExtension)
	{
		List<string> fileList = new List<string>();

		string extUC = fileExtension.ToUpper();
		string extLC = fileExtension.ToLower();

		if (!Directory.Exists(@directory))
		{
			Directory.CreateDirectory(@directory);
		}
		string[] fileEntries = Directory.GetFiles(@directory, "*" + extLC);
		foreach (string fileName in fileEntries)
		{
			if (File.Exists(fileName))
			{
				if (fileName.Contains(extLC) | fileName.Contains(extUC))
				{
					fileList.Add(Path.GetFileNameWithoutExtension(fileName));
				}
			}
		}
		return fileList; 
	}

		public void CreateDirAndFileIfNotExists(string filePath, string programDataDirectory)
	{
		if (!System.IO.Directory.Exists(programDataDirectory))
		{
			Directory.CreateDirectory(programDataDirectory);
		}

		if (!File.Exists(filePath))
		{
			FileIO io = new FileIO();
			io.WriteFile(filePath, "");
		}
	}


	// Set condition that User has agreed to User Agreement
	public void SetAppUserAgreed()
	{
		try
		{
			Globals.User_Settings.StrSetUserAgreed = "согласен";
			//saveSettingsToINIfile();

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}


    /// </summary>
    /// <param name="heading"></param>
    /// <param name="messageTxt"></param>
    /// <param name="btnOkCnclEtc"></param>
    /// <param name="font"></param>
    /// <param name="fontSize"></param>
    /// <param name="foreColor"></param>
    public static void CustomMessageBox(string heading, string messageTxt, string btnOkCnclEtc="OK", 
			string font = "Cambria", int fontSize = 18, Color foreColor = default)
    {
		Form messageBox = new Form();
        Label label = new Label();
		Button button = new Button();

        if (foreColor == default(Color)){foreColor = Color.FromArgb(0, 0, 0);}

        messageBox.Font = new Font(font, fontSize); // Set the font size here
        using (var graphics = messageBox.CreateGraphics())
		{
            SizeF size = graphics.MeasureString(messageTxt, messageBox.Font);
            Rectangle bounds = new Rectangle(0, 0, messageBox.ClientSize.Width, (int)size.Height);
            TextRenderer.DrawText(graphics, messageTxt, messageBox.Font, bounds, messageBox.ForeColor, TextFormatFlags.HorizontalCenter);

            // Set button characteristics
            int buttonWidth = 100;
            int buttonHeight = 36;
            {
				button.Text = btnOkCnclEtc;
				button.Size = new Size(buttonWidth, buttonHeight);
				button.Location = new Point((int)((size.Width - buttonWidth) / 2), (int)size.Height + 20);
            };
            messageBox.Controls.Add(button);
            messageBox.ClientSize = new Size((int)size.Width+20, (int)size.Height + buttonHeight + 40);
        }

        messageBox.Text = heading;
		label.Text = "  " + messageTxt;
        label.AutoSize = true;
        messageBox.Controls.AddRange(new Control[] { label, button });
		messageBox.FormBorderStyle = FormBorderStyle.FixedDialog;
		messageBox.StartPosition = FormStartPosition.CenterScreen;
		messageBox.MinimizeBox = false;
		messageBox.MaximizeBox = false;
		messageBox.AcceptButton = button;
		messageBox.TopMost = true;

		button.Click += (sender, e) => { messageBox.Close(); };
		messageBox.ShowDialog();
	}
		
    // Add item to top of a List(Of String), remove any duplicate items
    private static void AddNewEntryToTopOfList(string itemToAdd)
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

	public static bool IsFormTypeOpen(Type formType) {

        foreach (Form frm in Application.OpenForms)
        {
            if (frm.GetType() == formType)
            {
                return true;
            }
        }
        return false;
    }


    /// </summary>
    /// <param name="fileType"></param>
    public static void SelectAndExportFiles(FileExtensionType fileType)  // This is used for exporting various file types.
    {
		bool displayMessage = false;

        try
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.DefaultExt = "qdta";
            if (fileType == FileExtensionType.Qdta)
            {
                openFileDialog1.Filter = "Quiz File Format(*.qdta)|*.qdta";
            }
            if (fileType == FileExtensionType.Fdta)
            {
                openFileDialog1.Filter = "Quiz File Format(*.fdta)|*.fdta";
            }
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Globals.Data_Folder;
            openFileDialog1.Title = "Select Files";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                folderBrowserDialog1.Description = "Select Folder to Save Files";
                string sourcePath = Globals.Data_Folder;
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


    /// </summary>
    /// <param name="fileType"></param>
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
                    string destPath = Path.Combine(Globals.Data_Folder, Path.GetFileName(fileName));

                    // check if file already exists
                    if (File.Exists(destPath))
                    {
                        var result = MessageBox.Show($"The file {Path.GetFileName(fileName)} already exists. Do you want to overwrite it?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            continue; // skip importing this file
                        }
                        destPath = Globals.Data_Folder + fileName; // copy the file to the destination folder
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

	/// <summary>
	///  Used to size a rich text box based on the amount of text it contains and the font used
	/// </summary>
	/// <param name="RTB"></param>
	/// <returns></returns>
    public static Size UpdateFormSize(RichTextBox RTB)
    {
		System.Drawing.Size frmSize = new Size();

        // Get the message text from the RTBSection control
        string messageTxt = RTB.Text;

        // Create a new Graphics object based on the RTBSection control
        using (Graphics graphics = RTB.CreateGraphics())
        {
            // Measure the size of the message text based on the RTBSection font
            SizeF size = graphics.MeasureString(messageTxt, RTB.Font);

            // Create a new rectangle that is the size of the RTBSection control
            Rectangle bounds = new Rectangle(0, 0, RTB.ClientSize.Width, (int)size.Height);

            // Draw the text onto the Graphics object using TextRenderer
            TextRenderer.DrawText(graphics, messageTxt, RTB.Font, bounds, RTB.ForeColor, TextFormatFlags.HorizontalCenter);

			// Set the size of the form to the size of the Graphics object plus a margin
			int marginWidth = 200;
			int marginHeight = 200;
            frmSize = new Size((int)size.Width + marginWidth, (int)size.Height + marginHeight);
        }
		return frmSize;
    }


}

