using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    public class RichTextBoxHandler
    {
        private RichTextBox[] rtfFindArray = new RichTextBox[11];
        private RichTextBox[] rtfReplaceArray = new RichTextBox[11];

        public RichTextBoxHandler()
        {
            // Initialize the rich text boxes
            for (int i = 1; i <= 10; i++)
            {
                rtfFindArray[i] = new RichTextBox();
                rtfReplaceArray[i] = new RichTextBox();
            }
        }

        public string[] GetAllRtf()
        {
            List<string> allRichText = new List<string>();

            // Iterate through all the rich text boxes and add their Rtf to the list
            for (int i = 1; i <= 10; i++)
            {
                allRichText.Add(rtfFindArray[i].Rtf);
                allRichText.Add(rtfReplaceArray[i].Rtf);
            }

            return allRichText.ToArray();
        }

        public string GetRTF_Find(int findIndex, int replaceIndex)
        {
            return $"{rtfFindArray[findIndex].Rtf}";
        }

        public string GetRTF_Replace(int replaceIndex)
        {
            return $"{rtfReplaceArray[replaceIndex].Rtf}";
        }

        public void SetRTF_Find(int findIndex, string findText)
        {
            rtfFindArray[findIndex].Rtf = findText;
        }

        public void SetRTF_Replace(int replaceIndex, string replaceText)
        {
            rtfReplaceArray[replaceIndex].Rtf = replaceText;
        }
    }
}
