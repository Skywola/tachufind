using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class SetLineLengthToDesiredLength
    {
        private static readonly char[] splitCharacters = new[] { ' ', '\n', '\r', '\t' };

        // Your method

        public static string ModifyTextToDesiredLength(int desiredLineLength, string originalText)
        {
            string[] words = originalText.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder formattedText = new StringBuilder();
            StringBuilder currentLine = new StringBuilder();

            foreach (string word in words)
            {
                if ((currentLine.Length + word.Length + 1) <= desiredLineLength) // Adding 1 for the space
                {
                    if (currentLine.Length > 0)
                    {
                        currentLine.Append(' ');
                        currentLine = currentLine.Replace(",", ", ");  // Added to see if it will put spaces after commas where they have been missing
                        currentLine = currentLine.Replace("  ", " ");  // in case too much was added
                    }
                    currentLine.Append(word);
                }
                else
                {
                    formattedText.AppendLine(currentLine.ToString());
                    //Globals.lineLengthQueue.Enqueue(currentLine.Length);  // Record each string length
                    currentLine.Clear().Append(word);
                }
            }

            formattedText.Append(currentLine);

            return formattedText.ToString();
        }

        // Old Method   New Method above altered by MS Copilot
        //public static string ModifyTextToDesiredLength(int desiredLineLength, string originalText)
        //{
        //    string[] words = originalText.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
        //    StringBuilder formattedText = new StringBuilder();
        //    StringBuilder currentLine = new StringBuilder();

        //    foreach (string word in words)
        //    {
        //        if ((currentLine.Length + word.Length + 1) <= desiredLineLength) // Adding 1 for the space
        //        {
        //            if (currentLine.Length > 0)
        //            {
        //                currentLine.Append(' ');
        //                currentLine = currentLine.Replace(",", ", ");  // Added to see if it will put spaces after commas where they have been missing
        //                currentLine = currentLine.Replace("  ", " ");  // in case too much was added
        //            }
        //            currentLine.Append(word);
        //        }
        //        else
        //        {
        //            formattedText.AppendLine(currentLine.ToString());
        //            //Globals.lineLengthQueue.Enqueue(currentLine.Length);  // Record each string length
        //            currentLine.Clear().Append(word);
        //        }
        //    }

        //    formattedText.Append(currentLine);

        //    return formattedText.ToString();
        //}

    }
}
