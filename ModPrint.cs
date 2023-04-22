using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Drawing.Printing;
using System.Runtime.InteropServices;



public static class ModPrint
{
	public class RichTextBoxPrintCtrl : RichTextBox
	{
		//Convert the unit used by the .NET framework (1/100 inch) 
		//and the unit used by Win32 API calls (twips 1/1440 inch)

		private const double anInch = 14.4;
		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct CHARRANGE
		{
			public int cpMin;
			//First character of range (0 for start of doc)
			public int cpMax;
			//Last character of range (-1 for end of doc)
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct FORMATRANGE
		{
			public IntPtr hdc;
			//Actual DC to draw on
			public IntPtr hdcTarget;
			//Target DC for determining text formatting
			public RECT rc;
			//Region of the DC to draw to (in twips)
			public RECT rcPage;
			//Region of the whole DC (page size) (in twips)
			public CHARRANGE chrg;
			//Range of text to draw (see earlier declaration)
		}

		private const int WM_USER = 0x400;

		private const int EM_FORMATRANGE = WM_USER + 57;
		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		// Render the contents of the RichTextBox for printing
		//	Return the last character printed + 1 (printing start from this point for next page)
		public int Print(int charFrom, int charTo, PrintPageEventArgs e)
		{
			//Calculate the area to render and print
			RECT rectToPrint = default(RECT);
			rectToPrint.Top = Convert.ToInt32(Math.Truncate(e.MarginBounds.Top * anInch));
			rectToPrint.Bottom = Convert.ToInt32(Math.Truncate(e.MarginBounds.Bottom * anInch));
			rectToPrint.Left = Convert.ToInt32(Math.Truncate(e.MarginBounds.Left * anInch));
			rectToPrint.Right = Convert.ToInt32(Math.Truncate(e.MarginBounds.Right * anInch));

			//Calculate the size of the page
			RECT rectPage = default(RECT);
			rectPage.Top = Convert.ToInt32(Math.Truncate(e.PageBounds.Top * anInch));
			rectPage.Bottom = Convert.ToInt32(Math.Truncate(e.PageBounds.Bottom * anInch));
			rectPage.Left = Convert.ToInt32(Math.Truncate(e.PageBounds.Left * anInch));
			rectPage.Right = Convert.ToInt32(Math.Truncate(e.PageBounds.Right * anInch));

			IntPtr hdc = e.Graphics.GetHdc();

			FORMATRANGE fmtRange = default(FORMATRANGE);
			fmtRange.chrg.cpMax = charTo;
			//Indicate character from to character to 
			fmtRange.chrg.cpMin = charFrom;
			fmtRange.hdc = hdc;
			//Use the same DC for measuring and rendering
			fmtRange.hdcTarget = hdc;
			//Point at printer hDC
			fmtRange.rc = rectToPrint;
			//Indicate the area on page to print
			fmtRange.rcPage = rectPage;
			//Indicate size of page
			IntPtr res = IntPtr.Zero;

			IntPtr wparam = IntPtr.Zero;
			wparam = new IntPtr(1);

			//Get the pointer to the FORMATRANGE structure in memory
			IntPtr lparam = IntPtr.Zero;
			lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
			Marshal.StructureToPtr(fmtRange, lparam, false);

			//Send the rendered data for printing 
			res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

			//Free the block of memory allocated
			Marshal.FreeCoTaskMem(lparam);

			//Release the device context handle obtained by a previous call
			e.Graphics.ReleaseHdc(hdc);

			//Return last + 1 character printer
			return res.ToInt32();
		}
	}

}



