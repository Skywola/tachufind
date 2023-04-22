using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
// New INI file module, combine with FileHistorySettings
// AppData is saved in an .ini file
public class AppINI
{
	//GeneralFns genFns = new GeneralFns();
	//FileIO FIO = new FileIO();
	//Dictionary<int, string> AppDataDictionary = new Dictionary<int, string>();
	//public List<string> savedFileProperties = new List<string>();

	// Get all file paths, then get initial settings from first file stored in the INI file.
	public void getSettingsFromINIfile()
	{
		//Queue<string> sectionQueue = new Queue<string>(); // Multiple sections containing settings for each file
		//Queue<string> settingsQueue = new Queue<string>(); // Holds a single file's settings
		//List<string> outList = new List<string>();
		//string fileContents = string.Empty;

		try
		{
			//if (!File.Exists(Globals.INI_Path))
			//{
			//return;
			//}
			//fileContents = FIO.readFile(Globals.INI_Path);
			//outList = fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
			//foreach (string setting in outList)
			//{ // Use for output
			//settingsQueue.Enqueue(setting);
			//}
			//Globals.mus.FilePath = settingsQueue.Dequeue();  // Line 1
			//Globals.FrmMainXLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmMainYLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmMainWidth = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmMainHeight = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmColorXLocation = Convert.ToInt32(settingsQueue.Dequeue());  // Line 5
			//Globals.FrmColorYLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmQuizXLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmQuizYLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmSectionXLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmSectionYLocation = Convert.ToInt32(settingsQueue.Dequeue());


			// FrmSectionSize
			// FrmGetSearchLocation   // Not used
			//Globals.FrmSectionWidth = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmSectionHeight = Convert.ToInt32(settingsQueue.Dequeue()); // L10
			//Globals.FrmGetSearchXLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmGetSearchYLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmSaveSearchesXLocation = Convert.ToInt32(settingsQueue.Dequeue());
			//Globals.FrmSaveSearchesYLocation = Convert.ToInt32(settingsQueue.Dequeue()); 
			//Globals.CursorPosition = Convert.ToInt32(settingsQueue.Dequeue());    // L15   // FrmMain  // Below: items = 2 + 12

			// Start Colors
			//Globals.FrmMainTextForecolor = strToColor(settingsQueue.Dequeue());   // Take stored string representations of colors and convert
			//Globals.backColorStd = strToColor(settingsQueue.Dequeue());                  // them to Colors
			//Globals.color01 = strToColor(settingsQueue.Dequeue());
			//Globals.color02 = strToColor(settingsQueue.Dequeue());
			//Globals.color03 = strToColor(settingsQueue.Dequeue());
			//Globals.color04 = strToColor(settingsQueue.Dequeue());
			//Globals.color05 = strToColor(settingsQueue.Dequeue());
			//Globals.color06 = strToColor(settingsQueue.Dequeue());
			//Globals.color07 = strToColor(settingsQueue.Dequeue());
			//Globals.color08 = strToColor(settingsQueue.Dequeue());
			//Globals.color09 = strToColor(settingsQueue.Dequeue());
			//Globals.color10 = strToColor(settingsQueue.Dequeue());
			//Globals.color11 = strToColor(settingsQueue.Dequeue());
			// End of colors

			// Globals.StrSetUserAgreed = settingsQueue.Dequeue();  // L33    // Options from FrmOptions
			//Globals.FrmOptionsOpenFromLastLocation = bool.Parse(settingsQueue.Dequeue());
			//Globals.FrmOptionsOptOutOfFutureChangeFontWarnings = bool.Parse(settingsQueue.Dequeue());
			//Globals.StrBuildDate = settingsQueue.Dequeue();
			//Globals.BuildVersion = settingsQueue.Dequeue();  // Line 38
			//Globals.mus.FilePath01 = settingsQueue.Dequeue();  // Line 39
			//Globals.mus.FilePath02 = settingsQueue.Dequeue();
			//Globals.mus.FilePath03 = settingsQueue.Dequeue();
			//Globals.mus.FilePath04 = settingsQueue.Dequeue();
			//Globals.mus.FilePath05 = settingsQueue.Dequeue();
			//Globals.mus.FilePath06 = settingsQueue.Dequeue();
			//Globals.mus.FilePath07 = settingsQueue.Dequeue();  // Line 45
			//Globals.mus.FilePath08 = settingsQueue.Dequeue();
			//Globals.mus.FilePath09 = settingsQueue.Dequeue();
			//Globals.mus.FilePath10 = settingsQueue.Dequeue();
			//Globals.mus.FilePath11 = settingsQueue.Dequeue();
			//Globals.mus.FilePath12 = settingsQueue.Dequeue();
			//Globals.mus.FilePath13 = settingsQueue.Dequeue();  // Line 51
			//Globals.mus.FilePath14 = settingsQueue.Dequeue();
			//Globals.mus.FilePath15 = settingsQueue.Dequeue();
			//Globals.mus.FilePath16 = settingsQueue.Dequeue();
			//Globals.mus.FilePath17 = settingsQueue.Dequeue();
			//Globals.mus.FilePath18 = settingsQueue.Dequeue();
			//Globals.mus.FilePath19 = settingsQueue.Dequeue();
			//Globals.mus.FilePath20 = settingsQueue.Dequeue();
			//Globals.mus.FilePath21 = settingsQueue.Dequeue();   // L59


		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}


	//  App settings will be saved in .ini file
	// Saves all app settings, if parameters are passed, change the passed setting.
	public void saveSettingsToINIfile()
	{
		try
		{
			//string newFileProperties = "";
			//Queue<string> queueResult = new Queue<string>();
			//FIO.createDirectoryIfItDoesNotExist(Globals.INI_Path);

			//queueResult.Enqueue(Globals.mus.FilePath);  // Line 1
			//queueResult.Enqueue(Globals.FrmMainXLocation.ToString());
			//queueResult.Enqueue(Globals.FrmMainYLocation.ToString());
			//queueResult.Enqueue(Globals.FrmMainWidth.ToString());
			//queueResult.Enqueue(Globals.FrmMainHeight.ToString());  // Line 5
			//queueResult.Enqueue(Globals.FrmColorXLocation.ToString());
			//queueResult.Enqueue(Globals.FrmColorYLocation.ToString());
			//queueResult.Enqueue(Globals.FrmQuizXLocation.ToString());  // Line 8
			//queueResult.Enqueue(Globals.FrmQuizYLocation.ToString());
			//queueResult.Enqueue(Globals.FrmSectionXLocation.ToString());  // Line 10
			//queueResult.Enqueue(Globals.FrmSectionYLocation.ToString());
			//queueResult.Enqueue(Globals.FrmSectionWidth.ToString());  
			//queueResult.Enqueue(Globals.FrmSectionHeight.ToString());  // Line 13
			//queueResult.Enqueue(Globals.FrmGetSearchXLocation.ToString());
			//queueResult.Enqueue(Globals.FrmGetSearchYLocation.ToString());  // Line 15
			//queueResult.Enqueue(Globals.FrmSaveSearchesXLocation.ToString());
			//queueResult.Enqueue(Globals.FrmSaveSearchesYLocation.ToString());
			//queueResult.Enqueue(Globals.CursorPosition.ToString());  // Line 18

			// enqueue Colors as strings
			//queueResult.Enqueue(colorToString(Globals.FrmMainTextForecolor));
			//queueResult.Enqueue(colorToString(Globals.mus.RTBMainBackColor));
			//queueResult.Enqueue(colorToString(Globals.color00));
			//queueResult.Enqueue(colorToString(Globals.color01));
			//queueResult.Enqueue(colorToString(Globals.color02));
			//queueResult.Enqueue(colorToString(Globals.color03));
			//queueResult.Enqueue(colorToString(Globals.color04));
			//queueResult.Enqueue(colorToString(Globals.color05));
			//queueResult.Enqueue(colorToString(Globals.color06));
			//queueResult.Enqueue(colorToString(Globals.color07));
			//queueResult.Enqueue(colorToString(Globals.color08));
			//queueResult.Enqueue(colorToString(Globals.color09));
			//queueResult.Enqueue(colorToString(Globals.color10));
			//queueResult.Enqueue(colorToString(Globals.color11));
			// End Colors


			//queueResult.Enqueue(Globals.StrSetUserAgreed);
			//queueResult.Enqueue(Globals.FrmOptionsOpenFromLastLocation.ToString());
			//queueResult.Enqueue(Globals.FrmOptionsOptOutOfFutureChangeFontWarnings.ToString());

			// Not used
			// queueResult.Enqueue(Globals.StrBuildDate);
			// queueResult.Enqueue(Globals.BuildVersion);  // Line 38

			// queueResult.Enqueue(Globals.mus.FilePath01);  // Line 39
			// queueResult.Enqueue(Globals.mus.FilePath02);
			// queueResult.Enqueue(Globals.mus.FilePath03);
			// queueResult.Enqueue(Globals.mus.FilePath04);
			// queueResult.Enqueue(Globals.mus.FilePath05);
			// queueResult.Enqueue(Globals.mus.FilePath06);  // Line 44
			// queueResult.Enqueue(Globals.mus.FilePath07);
			// queueResult.Enqueue(Globals.mus.FilePath08);
			// queueResult.Enqueue(Globals.mus.FilePath09);
			// queueResult.Enqueue(Globals.mus.FilePath10);  // Line 48
			// queueResult.Enqueue(Globals.mus.FilePath11);
			// queueResult.Enqueue(Globals.mus.FilePath12);
			// queueResult.Enqueue(Globals.mus.FilePath13);  // Line 51
			// queueResult.Enqueue(Globals.mus.FilePath14);
			// queueResult.Enqueue(Globals.mus.FilePath15);
			// queueResult.Enqueue(Globals.mus.FilePath16);
			// queueResult.Enqueue(Globals.mus.FilePath17);
			// queueResult.Enqueue(Globals.mus.FilePath18);  // L56
			// queueResult.Enqueue(Globals.mus.FilePath19);
			// queueResult.Enqueue(Globals.mus.FilePath20);
			// queueResult.Enqueue(Globals.mus.FilePath21);  // L59

			//while (queueResult.Count > 0)
			//{
				//newFileProperties = newFileProperties + queueResult.Dequeue() + Environment.NewLine;
			//}

			//FIO.writeFile(Globals.INI_Path, newFileProperties);

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}








}
