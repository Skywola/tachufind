using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    internal class RichTextBoxScrollManager
    {
        // Method to get the top visible character index based on the scroll position
        public static int GetTopVisibleCharIndex(RichTextBox rtb)
        {
            // Get the character index at the top-left corner (visible area)
            return rtb.GetCharIndexFromPosition(new System.Drawing.Point(1, 1));  // Avoid (0, 0) in case it's outside the bounds
        }

        // Method to scroll to a specific character index (restoring the scroll position)
        public static void ScrollToCharIndex(RichTextBox rtb, int charIndex)
        {
            // Temporarily hide the caret by moving it to the end of the document
            int originalSelectionStart = rtb.SelectionStart;
            int originalSelectionLength = rtb.SelectionLength;

            rtb.SelectionStart = charIndex;  // Set caret to the desired charIndex
            rtb.ScrollToCaret();             // Scroll to make the caret visible

            // Restore the caret to its original position
            rtb.SelectionStart = originalSelectionStart;
            rtb.SelectionLength = originalSelectionLength;
        }
    }
}
