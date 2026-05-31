using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public class VisualLines
    {
        private Queue<KeyValuePair<int, string>> lines = new Queue<KeyValuePair<int, string>>();
        private Queue<KeyValuePair<int, string>> originalLines = new Queue<KeyValuePair<int, string>>(); // Store original for selection
        private bool inBracketedBlock = false;

        public void AddLine(int startPos, string text)
        {
            string trimmedText = text.Trim();
            int openingBracketIndex = trimmedText.IndexOf('[');
            int closingBracketIndex = trimmedText.IndexOf(']');

            if (inBracketedBlock)
            {
                if (closingBracketIndex >= 0)
                {
                    inBracketedBlock = false;
                }
                return;
            }

            // Case: Line starts with bracket but has text after
            if (openingBracketIndex == 0 && closingBracketIndex > openingBracketIndex)
            {
                if (closingBracketIndex < trimmedText.Length - 1)
                {
                    string textAfterBracket = trimmedText.Substring(closingBracketIndex + 1);
                    string textToSpeak = textAfterBracket.TrimStart();

                    if (textToSpeak.Length > 0)
                    {
                        // Find where the actual text starts in the ORIGINAL text
                        int textStartInOriginal = text.IndexOf(textToSpeak);
                        if (textStartInOriginal >= 0)
                        {
                            int actualStartPos = startPos + textStartInOriginal;

                            // Store the text to speak
                            lines.Enqueue(new KeyValuePair<int, string>(actualStartPos, textToSpeak));

                            // Also store the original text for selection reference (optional)
                            originalLines.Enqueue(new KeyValuePair<int, string>(actualStartPos, textToSpeak));
                        }
                    }
                }
                return;
            }

            // Case: Single line with bracket in middle
            if (openingBracketIndex >= 0 && closingBracketIndex > openingBracketIndex)
            {
                string textBeforeBracket = trimmedText.Substring(0, openingBracketIndex).Trim();
                string textAfterBracket = "";

                if (closingBracketIndex < trimmedText.Length - 1)
                {
                    textAfterBracket = trimmedText.Substring(closingBracketIndex + 1).Trim();
                }

                string cleanText = textBeforeBracket;
                if (textAfterBracket.Length > 0)
                {
                    if (cleanText.Length > 0)
                        cleanText += " " + textAfterBracket;
                    else
                        cleanText = textAfterBracket;
                }

                if (cleanText.Length > 0)
                {
                    lines.Enqueue(new KeyValuePair<int, string>(startPos, cleanText));
                    originalLines.Enqueue(new KeyValuePair<int, string>(startPos, cleanText));
                }
                return;
            }

            // Case: Starts multi-line bracket
            if (openingBracketIndex >= 0 && closingBracketIndex < 0)
            {
                string textBeforeBracket = trimmedText.Substring(0, openingBracketIndex).Trim();

                if (textBeforeBracket.Length > 0)
                {
                    lines.Enqueue(new KeyValuePair<int, string>(startPos, textBeforeBracket));
                    originalLines.Enqueue(new KeyValuePair<int, string>(startPos, textBeforeBracket));
                }

                inBracketedBlock = true;
                return;
            }

            // Normal line
            if (text.Length > 0)
            {
                lines.Enqueue(new KeyValuePair<int, string>(startPos, text));
                originalLines.Enqueue(new KeyValuePair<int, string>(startPos, text));
            }
        }

        public KeyValuePair<int, string> GetNextLine()
        {
            return lines.Dequeue();
        }

        public int Count => lines.Count;

        public void ClearLines()
        {
            lines.Clear();
            originalLines.Clear();
            inBracketedBlock = false;
        }
    }
}
