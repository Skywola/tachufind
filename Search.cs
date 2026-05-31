using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Windows.Storage.AccessCache;


namespace Tachufind
{
    public enum Affix
    {
        All,
        Suffix,
        Prefix
    }

    public enum Mode
    { 
        Color,
        Text
    }

    public class Search
    {
        public int index { get; set; } = 1;  // Property with default value of 1
        public string FindString { get; set; } = string.Empty;  // The string to search for
        public Color FindColor { get; set; } = Color.Black; // Default color set to Black
        public string ReplaceString { get; set; } = string.Empty;  // String to replace the found text
        public int TextBoxNum { get; set; }  // Refers to a specific RichTextBox (if multiple text boxes are used)
        public Font Font { get; set; } = new Font("Times New Roman", 22, FontStyle.Regular);

        internal bool firstPass { get; set; } = true;
        internal int forwardSearchIndex { get; set; } = 0;  // This is for Forward searches only, that is from top to bottom
        internal int lengthOfTextToSearch { get; set; } = 0;  // Used by Search Buttons on FrmMain, Not to be confused with searchLength. 
        internal bool SearchInProgress { get; set; } = false;

        // For BtnSearchFromBottom_Click This button searches for occurences starting from the end of the file
        // These are distinguished from the above variables by the _ preceeding the name
        internal Stack<int> _reverseResultsSearchStack = new();
        internal bool _firstPass { get; set; } = true;
        internal int _reverseSearchIndex { get; set; } = 0;
        // For BtnFindFwdFrmCursorClick and BtnFindRevFrmCursorClick 
        internal int start { get; set; } = 0;
        internal int searchLength { get; set; } = 0;  // Used by FrmColor Searches, not to be confused with lengthOfTextToSearch 



        // Method to clear the search and reset properties to default values
        public void Clear()
        {
            Color color = Color.White;
            index = 1;
            FindString = string.Empty;
            FindColor = Color.Black;  // Reset to default color
            ReplaceString = string.Empty;
            TextBoxNum = 0;  // Assuming default is 0
             Font = new Font("Times New Roman", 22, FontStyle.Regular);
        }
    }
}
