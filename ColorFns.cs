using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
//using System.Drawing.Image;
//using System.Text.UnicodeEncoding;
using Tachufind;


public class ColorFns
{


	public Stack<string> getColorARGBValuesFromString(string ARGBStr)
	{
		string Astr = "";
		string Rstr = "";
		string Gstr = "";
		string Bstr = "";
		var item = "";
		List<string> colorList = new List<string>();
		Stack<string> colorStack = new Stack<string>();
		try
		{
			// check for a custom color i.e. A=255, R= 0, G=255, B=64
			// This function first checks for a custom color, if one is present it returns TRUE
			// then it fills the array with the color numbers for A, R, G, B
			//storedColor = "A=255, R=0, G=255, B=164"  ' for info and testing
			foreach (string item_loopVariable in colorList)
			{
				item = item_loopVariable;
				colorStack.Push(item);
			}
			// colorStack holds {"B=64", "G=255", "R=0", "A=255"} is it is a custom color
			// If it is a custom color, convert to get a color
			if (colorStack.Count == 4)
			{
				Bstr = colorStack.Pop().Replace("B=", "");
				Gstr = colorStack.Pop().Replace("G=", "");
				Rstr = colorStack.Pop().Replace("R=", "");
				Astr = colorStack.Pop().Replace("A=", "");
				colorStack.Clear();
				colorStack.Push(Bstr);
				colorStack.Push(Gstr);
				colorStack.Push(Rstr);
				colorStack.Push(Astr);
				return colorStack;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return colorStack;
	}



	// So far nothing below here used.


	// invert the color value
	public Color invertColor(Color clr)
	{
		Color functionReturnValue = default(Color);
		byte R = 0;
		byte G = 0;
		byte B = 0;

		try
		{
			if ((clr == null))
			{
				return Color.LightGray;
			}
			// invert the colors
			R = Convert.ToByte(Math.Abs(clr.R - 254));
			G = Convert.ToByte(Math.Abs(clr.G - 254));
			B = Convert.ToByte(Math.Abs(clr.B - 254));
			clr = Color.FromArgb(255, Convert.ToInt32(R), Convert.ToInt32(G), Convert.ToInt32(B));
			return clr;

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return functionReturnValue;
	}

	// Compare foreColor to Globals.backColorStd, make sure they are not to similar, to prevent invisible text
	public Color setForeColorDependingOnBackcolor(Color foreColor) {

		// Detect if Nightmode is ON, i.e., background color is close to black
		bool similar = compareTwoColors(Globals.User_Settings.RTBMainBackColor, foreColor);
		if (similar)
		{
			// Night mode, RtfMain - Text is White, background is whatever color RTBMain backcolor is.
			return Color.White;
		}
		return foreColor;
	}

	// Return TRUE if Colors are significantly similar
	public bool compareTwoColors(Color color1, Color color2, byte allowableRange = 40)
	{
		int R;
		int G;
		int B;

		try
		{
			//				if ((color1 == null) | (color2 == null)) {
			//					return false;
			//				}
			// compare the colors
			if (color1.R > color2.R)
			{
				R = color1.R - color2.R;
			}
			else
			{
				R = color2.R - color1.R;
			}
			if (color1.G > color2.G)
			{
				G = color1.G - color2.G;
			}
			else
			{
				G = color2.G - color1.G;
			}
			if (color1.B > color2.B)
			{
				B = color1.B - color2.B;
			}
			else
			{
				B = color2.B - color1.B;
			}
			if (R < allowableRange & G < allowableRange & B < allowableRange)
			{
				return true;
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return false;
	}


	// Passes in the new color table to sub for the old color table, then adds it to the RTF.Text
	public void replaceRTFFileColorTable(ref string RTFColorTable)
	{
		int start = 1;
		long lengthOfFile = Globals.P_RTFMain_RTF.Length;
		string colorTableHeader = "{\\colortbl ;";
		long endOfColorTable = 0;
		string oldRichTextBoxRTF = null;
		string RTF = null;

		try
		{
			oldRichTextBoxRTF = Globals.P_RTFMain_RTF;
			// potential issue: Documentation = "{\colortbl;"  Actual = "{\colortbl ;"
			start = GeneralFns.DoInStr(oldRichTextBoxRTF, colorTableHeader);
			//If start = 0 Then : start = 1 : End If
			endOfColorTable = GeneralFns.DoInStr(start, oldRichTextBoxRTF, (";}" + "\n\r"));
			if (endOfColorTable <= start)
			{

				MessageBox.Show("No Color Table found in this file.  File must contain colors and be of Rich Text Type.", "Error Detected", MessageBoxButtons.OK);
				return;
			}
			else
			{
				start -= 1;
				string first = GeneralFns.DoLeft(oldRichTextBoxRTF, start);
				first = first.Trim();
				string last = GeneralFns.DoRight(oldRichTextBoxRTF, Convert.ToInt32(lengthOfFile - endOfColorTable - 1));
				last = last.Trim();
				RTF = first + RTFColorTable + last;

				// return to FrmMain.RichTextBoxPrintCtrl0.Rtf
				Globals.P_RTFMain_RTF = RTF;
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}


	// Build the color table string from the colors of the buttons located in FrmColor
		public Stack<byte> getColorComponentsRGBValuesFromColor(Color clr)
	{
		byte R = 0;
		byte G = 0;
		byte B = 0;
		byte A = 0;
		Stack<string> colorStringStack = new Stack<string>();
		Stack<byte> colorByteStack = new Stack<byte>();

		try
		{
			colorStringStack = getColorARGBValuesFromString(clr.ToString());
			if (colorStringStack.Count > 0)
			{
				A = Convert.ToByte(colorStringStack.Pop());
				// 255 if not transparent
				R = Convert.ToByte(colorStringStack.Pop());
				G = Convert.ToByte(colorStringStack.Pop());
				B = Convert.ToByte(colorStringStack.Pop());
				// not a custom color, get color components
			}
			else
			{
				R = clr.R;
				G = clr.G;
				B = clr.B;
				A = 255;
				// defaulting to non-transparent
			}
			colorByteStack.Push(B);
			colorByteStack.Push(G);
			colorByteStack.Push(R);
			colorByteStack.Push(A);

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return colorByteStack;
	}



	// XTODo Save new background color to RTF File header = RTF header does not allow this function, aborted development on it
	//  This sets only the first color now, but could be adapted to change any color in the header.
	public string setRTFFileTableBackColor(ref string RTFColorTable, string Rstr, string Gstr, string Bstr)
	{
		// , Optional location As Integer = 0
		int start = 1;
		int endc = 2;
		string colorTableHeader = "{\\colortbl ;";
		int lengthOfColorTable = 0;
		string colorContents = "";
		Stack<string> colorStack = new Stack<string>();
		string colorVal = Color.LightGray.ToString();
		List<string> colorList = new List<string>();
		string leftStr = "";
		string rightStr = "";
		string semicolonCheck = "";
		string bracketCheck = "";
		string patch = "";

		try
		{
			// Get length of whole color table  
			start = GeneralFns.DoInStr(RTFColorTable, colorTableHeader);
			//If start = 0 Then : start = 1 : End If
			lengthOfColorTable = GeneralFns.DoInStr(start, RTFColorTable, (";}" + "\n\r"));
			// Get colors in groups of three until hit the end of color table
			// potential issue: Documentation = "{\colortbl;"  Actual = "{\colortbl ;"
			start = GeneralFns.DoInStr(RTFColorTable, colorTableHeader);
			if (start > 0)
			{
				start = GeneralFns.DoInStr(start, RTFColorTable, "red");
				endc = GeneralFns.DoInStr(start, RTFColorTable, "blue") + 7;
				colorContents = GeneralFns.DoMid(RTFColorTable, start, endc - start);
				semicolonCheck = GeneralFns.DoMid(RTFColorTable, endc - 1, endc - 2);
				bracketCheck = GeneralFns.DoMid(RTFColorTable, endc, endc - 1);
				// remove ; or }, but replace them into the final string using patch
				if (semicolonCheck == ";")
				{
					colorContents = colorContents.Replace(";", "");
					patch = ";";
				}
				if (bracketCheck == "}")
				{
					colorContents = colorContents.Replace("}", "");
					patch = "}";
				}
				// it could be a two-digit number, i.e blue22;
				if (bracketCheck == ";")
				{
					colorContents = colorContents.Replace(";", "");
					patch = ";";
				}
				var item = "";
				char separator = '\\';
				colorList.AddRange(colorContents.Split(separator));
				foreach (string item_loopVariable in colorList)
				{
					item = item_loopVariable;
					colorStack.Push(item);
				}
				if (colorStack.Count == 3)
				{
					Bstr = colorStack.Pop();
					Gstr = colorStack.Pop();
					Rstr = colorStack.Pop();
					colorVal = "\\red" + Rstr + "\\green" + Gstr + "\\blue" + Bstr + patch;
					// Color [A=255, R=174, G=174, B=174]
				}
				else
				{
					colorVal = Color.LightGray.ToString();
				}
			}
			else
			{
				colorVal = Color.LightGray.ToString();
			}
			leftStr = GeneralFns.DoMid(RTFColorTable, 1, start);
			rightStr = GeneralFns.DoMid(RTFColorTable, start, lengthOfColorTable);
			RTFColorTable = leftStr + colorVal + rightStr;

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return RTFColorTable;
	}



	public Color getRTFFileTableBackColor(ref string RTFColorTable)
	{
		int start = 1;
		int endc = 2;
		string colorTableHeader = "{\\colortbl ;";
		string colorContents = "";
		Stack<string> colorStack = new Stack<string>();
		Color colorVal = default(Color);

		try
		{
			// potential issue: Documentation = "{\colortbl;"  Actual = "{\colortbl ;"
			start = GeneralFns.DoInStr(RTFColorTable, colorTableHeader);
			if (start > 0)
			{
				start = GeneralFns.DoInStr(start, RTFColorTable, "red");
				endc = GeneralFns.DoInStr(start, RTFColorTable, "blue") + 7;
				colorContents = GeneralFns.DoMid(RTFColorTable, start, endc - start);
				colorContents = colorContents.Replace(";", "");
				colorContents = colorContents.Replace("}", "");
				colorVal = getColorRGBValuesFromString(colorContents);
			}
			else
			{
				colorVal = Color.LightGray;
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return colorVal;
	}


	public Color getColorRGBValuesFromString(string RGBStr)
	{
		string Rstr = "";
		string Gstr = "";
		string Bstr = "";
		List<string> colorList = new List<string>();
		Stack<string> colorStack = new Stack<string>();
		int Rint = 0;
		int Gint = 0;
		int Bint = 0;
		Color colorVal = default(Color);
		var item = "";

		try
		{
			// check for a custom color i.e. A=255, R= 0, G=255, B=64
			// This function first checks for a custom color, it one is present it returns TRUE
			// then it fills the array with the color numbers for A, R, G, B
			//storedColor = "A=255, R=0, G=255, B=164"  ' for info and testing
			char separator = '\\';
			colorList.AddRange(RGBStr.Split(separator));
			foreach (string item_loopVariable in colorList)
			{
				item = item_loopVariable;
				colorStack.Push(item);
			}
			// colorStack holds {"B=64", "G=255", "R=0", "A=255"} is it is a custom color
			// If it is a custom color, convert to get a color
			if (colorStack.Count == 3)
			{
				Bstr = GeneralFns.RReplace(colorStack.Pop(), "blue", "");
				Gstr = GeneralFns.RReplace(colorStack.Pop(), "green", "");
				Rstr = GeneralFns.RReplace(colorStack.Pop(), "red", "");
				Rint = Convert.ToInt32(Rstr);
				Gint = Convert.ToInt32(Gstr);
				Bint = Convert.ToInt32(Bstr);
				colorVal = Color.FromArgb(255, Rint, Gint, Bint);
			}

			// Color [A=255, R=174, G=174, B=174]

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return colorVal;
	}



	// get the color value of this string 
	public Color getColor(Stack<string> colorStack)
	{
		Color colorVal = default(Color);
		try
		{
			if (colorStack.Count == 3)
			{
				string R = null;
				string G = null;
				string B = null;
				int Aint = 0;
				int Rint = 0;
				int Gint = 0;
				int Bint = 0;
				R = colorStack.Pop();
				G = colorStack.Pop();
				B = colorStack.Pop();
				Aint = 255;
				Rint = Convert.ToInt32(R);
				Gint = Convert.ToInt32(G);
				Bint = Convert.ToInt32(B);
				colorVal = Color.FromArgb(Aint, Rint, Gint, Bint);
			}

		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		return colorVal;
	}

	// Convert Color to a string
	private String colorToString(Color inputColor)
	{
		string Alpha = inputColor.A.ToString();
		string Red = inputColor.R.ToString();
		string Green = inputColor.G.ToString();
		string Blue = inputColor.B.ToString();
		return Alpha + "," + Red + "," + Green + "," + Blue;
	}

	// Convert String value to Color value
	private Color strToColor(string colorString)
	{
		string[] values = colorString.Split(',');
		int alpha = Convert.ToInt32(values[0]);
		int red = Convert.ToInt32(values[1]);
		int green = Convert.ToInt32(values[2]);
		int blue = Convert.ToInt32(values[3]);
		return Color.FromArgb(alpha, red, green, blue);
	}


}







