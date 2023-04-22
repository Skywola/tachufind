using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Tachufind;

// Searches are saved in Searches.dat file.  
public class SearchFns
{
	GeneralFns genFns = new GeneralFns();
	ColorFns clrFns = new ColorFns();
	Dictionary<int, string> SearchValues = new Dictionary<int, string>();
	List<string> listSearchValues = new List<string>();
	public List<string> ListOfSearches = new List<string>();
	FileIO FIO = new FileIO();
	public bool searchExists = true;

	public List<string> GetEntryTitleByName(string entryTitle)
	{
		List<string> settings = new List<string>();
		List<string> sectionsList = new List<string>();
		List<string> ItemsList = new List<string>();
		string fileContents = "";

		try
		{
			// ListFilePath is the selected find - replace item from list
			string ListFilePath = Globals.Data_Folder + entryTitle + Globals.FindReplaceFileExtension;
			if (!File.Exists(ListFilePath)) // If not exists, just drop out
			{
				return settings;
			}
			fileContents = FIO.ReadFile(ListFilePath);
			if (fileContents.Length < 1)
			{
				return settings;
			}
			ItemsList = Regex.Split(fileContents, "\r\n").ToList();
			return ItemsList;

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return ItemsList;
	}


	// Returns a queue with the values all stored in it.
	public List<string> TokenizeString(string strVal, string strDelimiter)
	{
		List<string> tempList = new List<string>();

		try
		{
			string[] tokenize = null;
			int intIndex2 = 1;
			int intDelimitLen = 0;
			int i = 0;

			intDelimitLen = strDelimiter.Length;
			while (intIndex2 > 0)
			{
				Array.Resize(ref tokenize, i + 2);
				intIndex2 = GeneralFns.DoInStr(1, strVal, strDelimiter);
				if (intIndex2 > 0)
				{
					tempList.Add(GeneralFns.DoMid(strVal, 1, (intIndex2 - 1)));
					strVal = GeneralFns.DoMid(strVal, (intIndex2 + intDelimitLen), strVal.Length);
				}
				else
				{
					tempList.Add(strVal);
				}
				i = i + 1;
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return tempList;
	}

	/*
	public void getFrmBtnAndTxtboxColors()
	{
		string storedColor = "";
		string[] arrayOfStrsARGB = new string[6];

		// this also sets default starting colors.
		try
		{
			// NOTE: storedColor is common to all If - Then statements
			Color colorValue = default(Color);
			string colorNumber = null;
			int z;
			for (z = 0; z <= 7; z++)  //CHANGE 7 TO 9   9-20-2020
			{
				colorNumber = "Color" + z;
				// conversion critical line
				//storedColor =  a.getApplicationSettingsFromDatFile("color" & z)
				storedColor = "";

				// set a default color for button if none exists
				if (!string.IsNullOrEmpty(storedColor) & storedColor != "Color []")
				{
					colorValue = clrFns.interpretColorString(storedColor);
				}
				else
				{
					colorValue = FrmMain.colors[FrmMain.colorIndex[z + 1]];  //colorArray[z];
					// if no values in XML, use default colors
				}
				setFrmColor_ColorArray(z, colorValue);
				// Resets the colors in colorArray
				//OLD-> rtfFind[z].ForeColor = colorValue;
				colorArray[z] = colorValue;
				//  This was greying shit on at initialization when opening a NEW file.
				//OLD-> colorPanel[z].BackColor = colorValue;
				colorArray[z] = colorValue;

			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}*/

	/*
	// This may have needed to return a color
	public void setFrmColor_ColorArray(int position, Color colr)
	{

		if (SearchSettings.rbBlackoutList == false)
		{
			colorArray[position] = colr;
		}
		else
		{
			colorArray[position] = Color.Black;
		}
	}
	*/

}

