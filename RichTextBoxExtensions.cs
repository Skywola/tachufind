using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tachufind
{
    public static class RichTextBoxExtensions
    {

        private const int WM_SETREDRAW = 0x0B;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, IntPtr lParam);

        public static void Freeze(this RichTextBox box)
        {
            SendMessage(box.Handle, WM_SETREDRAW, false, IntPtr.Zero);
        }

        public static void Unfreeze(this RichTextBox box)
        {
            SendMessage(box.Handle, WM_SETREDRAW, true, IntPtr.Zero);
            box.Invalidate();
        }


        public static int GetVisualEndCharIndexFromLineIndex(this RichTextBox rtb, int lineIndex)
        {
            // Get the first character index of the current line
            int lineStartIndex = rtb.GetFirstCharIndexFromLine(lineIndex);
            if (lineStartIndex == -1) { return -1; }  // Line index out of range

            // Check if there is a next line
            int nextLineStartIndex = rtb.GetFirstCharIndexFromLine(lineIndex + 1);
            if (nextLineStartIndex == -1)
            { return rtb.TextLength - 1; } // No next line, so return the last character index of the entire text

            // Return the index of the character just before the next line starts
            return nextLineStartIndex - 1;
        }


        public static void BoldGreekCharacters(this RichTextBox richTextBox)
        {
            // Regular expression pattern to match Greek characters
            //string pattern = @"\p{IsGreek}|[\u0300-\u03FF\u1F00-\u1FFF]";
            string pattern = @"\p{IsGreek}|[\u0300-\u03FF\u1F00-\u1FFF|\u0390-\u03FF]"; // Include characters from U+0390 to U+03FF

            MatchCollection matches = Regex.Matches(richTextBox.Text, pattern);

            foreach (Match match in matches)
            {
                int startIndex = match.Index;
                int length = match.Length;
                richTextBox.Select(startIndex, length);

                // Check if the selected text is not already bold, adding null check 
                if (richTextBox.SelectionFont != null && (richTextBox.SelectionFont.Style & FontStyle.Bold) != FontStyle.Bold)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Bold);
                }
                richTextBox.SelectionLength = 0;
            }
        }

       

        public static void BoldRussianCharacters(this RichTextBox richTextBox)
        {
            string text = richTextBox.Text;

            // Split into words but keep delimiters
            string pattern = @"(\b[\w-]+\b|[^\w-]+)";
            MatchCollection words = Regex.Matches(text, pattern);

            foreach (Match word in words)
            {
                if (IsRussianWord(word.Value))
                {
                    // Select and bold the entire word
                    richTextBox.Select(word.Index, word.Length);

                    if (richTextBox.SelectionFont != null &&
                        (richTextBox.SelectionFont.Style & FontStyle.Bold) != FontStyle.Bold)
                    {
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, FontStyle.Bold);
                    }
                    richTextBox.SelectionLength = 0;
                }
            }
        }

        private static bool IsRussianWord(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            int russianCharCount = 0;
            int totalLetterCount = 0;

            foreach (char c in word)
            {
                // Skip hyphens for counting
                if (c == '-') continue;

                // Check if it's a letter
                if (char.IsLetter(c))
                {
                    totalLetterCount++;
                    if (IsRussianCharacter(c))
                    {
                        russianCharCount++;
                    }
                }
            }

            // If word has letters and at least 70% are Russian, consider it a Russian word
            return totalLetterCount > 0 &&
                   (russianCharCount / (double)totalLetterCount) >= 0.7;
        }

        private static bool IsRussianCharacter(char c)
        {
            // Core Russian alphabet: А-Я, а-я, plus Ё and ё
            return (c >= '\u0410' && c <= '\u042F') || // A-Я uppercase
                   (c >= '\u0430' && c <= '\u044F') || // a-я lowercase
                   c == '\u0401' || c == '\u0451';      // Ё and ё
        }


        //
    }
}
