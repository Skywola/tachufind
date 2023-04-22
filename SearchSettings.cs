using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tachufind
{
	public static class SearchSettings
	{
		public static string SearchName { get; set; }
		public static bool rbEditColor { get; set; }
		// NOTE = THERE IS NO rbMultipleReplace data stored, because
		// it is simply the opposite of rbEditColor.
		public static bool chkAutoFindNext { get; set; }
		//public static bool rbBlackoutList { get; set; }
		public static bool chkMatchCase { get; set; }
		public static bool chkWordOnly { get; set; }
		public static bool chkIgnoreBoundryMarkers { get; set; }
		public static bool chkReverse { get; set; }

		// Place to store text from FIND textboxes located on frmColor
		public static string frmColorFind01_Txt { get; set; }
		public static string frmColorFind02_Txt { get; set; }
		public static string frmColorFind03_Txt { get; set; }
		public static string frmColorFind04_Txt { get; set; }
		public static string frmColorFind05_Txt { get; set; }
		public static string frmColorFind06_Txt { get; set; }
		public static string frmColorFind07_Txt { get; set; }
		public static string frmColorFind08_Txt { get; set; }
		public static string frmColorFind09_Txt { get; set; }
		public static string frmColorFind10_Txt { get; set; }


		// Used for frmColor, taken from the "signifies" textboxes, used only for color search-edit mode
		public static string frmColorHints01_Txt { get; set; }
		public static string frmColorHints02_Txt { get; set; }
		public static string frmColorHints03_Txt { get; set; }
		public static string frmColorHints04_Txt { get; set; }
		public static string frmColorHints05_Txt { get; set; }
		public static string frmColorHints06_Txt { get; set; }
		public static string frmColorHints07_Txt { get; set; }
		public static string frmColorHints08_Txt { get; set; }
		public static string frmColorHints09_Txt { get; set; }
		public static string frmColorHints10_Txt { get; set; }


		// Used for frmColor, taken from the "replace" textboxes,
		//  and used only for search-replace mode, (no color)
		public static string frmColorReplace01_Txt { get; set; }
		public static string frmColorReplace02_Txt { get; set; }
		public static string frmColorReplace03_Txt { get; set; }
		public static string frmColorReplace04_Txt { get; set; }
		public static string frmColorReplace05_Txt { get; set; }
		public static string frmColorReplace06_Txt { get; set; }
		public static string frmColorReplace07_Txt { get; set; }
		public static string frmColorReplace08_Txt { get; set; }
		public static string frmColorReplace09_Txt { get; set; }
		public static string frmColorReplace10_Txt { get; set; }


        public static string frmColorFind01_Rtf { get; set; }
        public static string frmColorFind02_Rtf { get; set; }
        public static string frmColorFind03_Rtf { get; set; }
        public static string frmColorFind04_Rtf { get; set; }
        public static string frmColorFind05_Rtf { get; set; }
        public static string frmColorFind06_Rtf { get; set; }
        public static string frmColorFind07_Rtf { get; set; }
        public static string frmColorFind08_Rtf { get; set; }
        public static string frmColorFind09_Rtf { get; set; }
        public static string frmColorFind10_Rtf { get; set; }

        public static string frmColorReplace01_Rtf { get; set; }
        public static string frmColorReplace02_Rtf { get; set; }
        public static string frmColorReplace03_Rtf { get; set; }
        public static string frmColorReplace04_Rtf { get; set; }
        public static string frmColorReplace05_Rtf { get; set; }
        public static string frmColorReplace06_Rtf { get; set; }
        public static string frmColorReplace07_Rtf { get; set; }
        public static string frmColorReplace08_Rtf { get; set; }
        public static string frmColorReplace09_Rtf { get; set; }
        public static string frmColorReplace10_Rtf { get; set; }




    }

	//  public static string SECTION_MARKER = "Eº§";

}
