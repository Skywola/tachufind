using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;


public class FileIO
{
	 public string ReadFile(string path, bool createIfNotPresent = true)
	{
		try
		{
			if (File.Exists(path) == true)
			{
				FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
				StreamReader sr = new StreamReader(fs);
				fs.Seek(0, SeekOrigin.Begin);
				// Set cursor postion within stream
				path = sr.ReadToEnd();
				if (sr != null) { sr.Close(); }
				if (fs != null) { fs.Close(); }
			}
			else
			{
				if (createIfNotPresent == false)
				{

					string message = "The file you are attempting to open located at : " + path + " no longer exists at that location.";
					MessageBox.Show(message, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
					path = "";
				}
			}
		}
		catch (Exception ex)
		{

			string m = "Error:" + ex.StackTrace + ". . . ." + Environment.NewLine + " Exception:  " + ex.ToString();
			MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return path;  // "path" is used so I do not have to dim a new variable, it actually returns the file content!
	}

	// Open file using a dialog box 
	//  Because FIO resets current path to the new file, it must run first, then in your richtextbox code, you 
	//  must check for how to apply the text, based on whether the file being opened is a RTF file or not.
	// See the note below this procedure for an example
	public string OpenFile()
	{
		OpenFileDialog openFileDialog01 = new OpenFileDialog();
		string strExt = null;
		string newPath = null;
		string fileContents = "";
		RichTextBox rtfBox = new RichTextBox();

		Cursor.Current = Cursors.WaitCursor;

		openFileDialog01.Title = "RTE - Open File";
		openFileDialog01.DefaultExt = "rtf";
		openFileDialog01.Filter = "Rich Text Files|*.rtf|" + "Text Files|*.txt|HTML Files|" + "*.html|All Files|*.*";
		openFileDialog01.FilterIndex = 1;

		try
		{   // LOCATION Open File
			if (openFileDialog01.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				newPath = openFileDialog01.FileName;
				if ((newPath != null))
				{
					Globals.User_Settings.FilePath = newPath;
					FileInfo FIO = new FileInfo(newPath);
					strExt = Path.GetExtension(newPath);
					strExt = strExt.ToUpper();
					if (strExt == "HTM") { strExt = "HTML";}
					switch (strExt)
					{
						case ".RTF":
							rtfBox.LoadFile(openFileDialog01.FileName, RichTextBoxStreamType.RichText);
							fileContents = rtfBox.Rtf;
							break;
						case ".HTML":
							string content = ReadFile(openFileDialog01.FileName);
							fileContents = ConvertHtmlToText(content);
							break;
						case ".TXT":
							rtfBox.Text = ReadFile(openFileDialog01.FileName);
							fileContents = rtfBox.Text;
							break;
						default:
							rtfBox.Text = ReadFile(openFileDialog01.FileName);
							fileContents = rtfBox.Text;
							break;
					}
				}
			}
			Cursor.Current = Cursors.Default;

		}
		catch (Exception ex)
		{

			string m = "Error:" + ex.StackTrace + ". . . ." + Environment.NewLine + " Exception:  " + ex.ToString();
			MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return fileContents;
	}

	// EXAMPLE:  Because FIO resets current path to the new file, it must run first before
	// we check for the way to apply the text, based on whether it is a RTF file or not.
	//
	//  Dim fileContents As String = FIO.OpenFile()
	//  Dim ext As String = FIO.getFileExtension(Properties.currentFilePath)
	//  ext = ext.ToUpper
	//   If ext = ".RTF" Then
	//    Me.RTBMain.Rtf = fileContents
	//   Else
	//    Me.RTBMain.Text = fileContents
	//    End If

	//#endregion

	#region "WriteFile"

	public static string ConvertHtmlToText(string source)
	{
		string result;

		// Remove HTML Development formatting
		// Replace line breaks with space
		// because browsers inserts space
		result = source.Replace("\r", " ");
		// Replace line breaks with space
		// because browsers inserts space
		result = result.Replace("\n", " ");
		// Remove step-formatting
		result = result.Replace("\t", string.Empty);
		// Remove repeating speces becuase browsers ignore them
		result = System.Text.RegularExpressions.Regex.Replace(result,
															  @"( )+", " ");

		// Remove the header (prepare first by clearing attributes)
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*head([^>])*>", "<head>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"(<( )*(/)( )*head( )*>)", "</head>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(<head>).*(</head>)", string.Empty,
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// remove all scripts (prepare first by clearing attributes)
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*script([^>])*>", "<script>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"(<( )*(/)( )*script( )*>)", "</script>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		//result = System.Text.RegularExpressions.Regex.Replace(result, 
		//         @"(<script>)([^(<script>\.</script>)])*(</script>)",
		//         string.Empty, 
		//         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"(<script>).*(</script>)", string.Empty,
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// remove all styles (prepare first by clearing attributes)
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*style([^>])*>", "<style>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"(<( )*(/)( )*style( )*>)", "</style>",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(<style>).*(</style>)", string.Empty,
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// insert tabs in spaces of <td> tags
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*td([^>])*>", "\t",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// insert line breaks in places of <BR> and <LI> tags
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*br( )*>", "\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*li( )*>", "\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// insert line paragraphs (double line breaks) in place
		// if <P>, <DIV> and <TR> tags
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*div([^>])*>", "\r\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*tr([^>])*>", "\r\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<( )*p([^>])*>", "\r\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// Remove remaining tags like <a>, links, images,
		// comments etc - anything thats enclosed inside < >
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<[^>]*>", string.Empty,
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// replace special characters:
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&nbsp;", " ",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&bull;", " * ",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&lsaquo;", "<",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&rsaquo;", ">",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&trade;", "(tm)",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&frasl;", "/",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"<", "<",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @">", ">",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&copy;", "(c)",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&reg;", "(r)",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		// Remove all others. More can be added, see
		// http://hotwired.lycos.com/webmonkey/reference/special_characters/
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 @"&(.{2,6});", string.Empty,
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		// make line breaking consistent
		result = result.Replace("\n", "\r");

		// Remove extra line breaks and tabs:
		// replace over 2 breaks with 2 and over 4 tabs with 4. 
		// Prepare first to remove any whitespaces inbetween
		// the escaped characters and remove redundant tabs inbetween linebreaks
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\r)( )+(\r)", "\r\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\t)( )+(\t)", "\t\t",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\t)( )+(\r)", "\t\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\r)( )+(\t)", "\r\t",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		// Remove redundant tabs
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\r)(\t)+(\r)", "\r\r",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		// Remove multible tabs followind a linebreak with just one tab
		result = System.Text.RegularExpressions.Regex.Replace(result,
				 "(\r)(\t)+", "\r\t",
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		// Initial replacement target string for linebreaks
		string breaks = "\r\r\r";
		// Initial replacement target string for tabs
		string tabs = "\t\t\t\t\t";
		for (int index = 0; index < result.Length; index++)
		{
			result = result.Replace(breaks, "\r\r");
			result = result.Replace(tabs, "\t\t\t\t");
			breaks = breaks + "\r";
			tabs = tabs + "\t";
		}
		return result;
	}


	// OVERLOADED Text files (non-RichTextBox), UNICODE
	public void WriteFile(string path, string txtStrng, bool acknowledge = false)
	{
		try
		{
			string routePath = Path.GetDirectoryName(path);
			if (!Directory.Exists(routePath))
			{
				Directory.CreateDirectory(routePath);
			}
			//OLD Changed 8-21-2018 StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Unicode);
			StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
			//??StreamWriter sw = new StreamWriter(path, false, System.Text.UTF8Encoding.UTF8);  // WTF is the difference?
			sw.Write(txtStrng);
			sw.Close();
			if (acknowledge == true)
			{
				MessageBox.Show("File saved.");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	// this requires that a RichTextBox be passed in
	public void SaveFileAs(ref RichTextBox fileData, bool acknowledge = false)
	{
		SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
		string fileExtension = "";
		RichTextBox RTB = new RichTextBox();

		try
		{
			RTB.Rtf = fileData.Rtf;
			SaveFileDialog1.Title = "RTE - Save File";
			SaveFileDialog1.DefaultExt = "rtf";
			SaveFileDialog1.Filter = "Rich Text Files|*.rtf|" + "Text Files|*.txt|" + "HTML Files|*.htm" + "|All Files|*.*";
			SaveFileDialog1.FilterIndex = 1;
			SaveFileDialog1.ShowDialog();
			if (string.IsNullOrEmpty(SaveFileDialog1.FileName))
			{
				return;
			}
			fileExtension = System.IO.Path.GetExtension(SaveFileDialog1.FileName);
			fileExtension = fileExtension.ToUpper();
			switch (fileExtension)
			{
				case ".RTF":
					RTB.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
					break;
				default:
					WriteFile(SaveFileDialog1.FileName, RTB.Text);
					break;
			}

			if (acknowledge == true)
			{
				string caption = "Saved";
				MessageBox.Show("File saved.", caption, MessageBoxButtons.OK);
			}

		}
		catch (Exception ex)
		{

			string m = "Error:" + ex.StackTrace + ". . . ." + Environment.NewLine + " Exception:  " + ex.ToString();
			MessageBox.Show(m, "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	public void CreateDirectoryIfItDoesNotExist(string FilePath)
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

	public void CreateFileIfItDoesNotExist(string filepath)
	{
		try
		{
			// if there is no file, create one
			if (!File.Exists(filepath))
			{
				FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
				fs.Close();
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	#endregion

	public string GetFileExtension(string path)
	{
		path = System.IO.Path.GetExtension(path);
		return path.ToUpper();
		// uses path to output Extension
	}


	public string GetFileNameWithoutExtension(string filePath)
	{
		return Path.GetFileNameWithoutExtension(filePath);
	}






}
