using System;
using System.Drawing;

namespace Tachufind
{
    public static class SearchSettings
    {
        public static string SearchName { get; set; } = string.Empty;
        public static bool FrmColorReplaceMode { get; set; } = true;  // true means color mode, false means replace text mode
        public static bool ChkAutoFindNext { get; set; } = true; // AVAILABLE - NO LONGER USED
        public static bool ChkMatchCase { get; set; } = false;
        public static bool ChkWordOnly { get; set; } = false;
        public static bool chkIgnoreBoundaryMarkers { get; set; } = false;
        public static bool Reserved { get; set; } = false;

        private const int MaxTextBoxes = 10;

        // Arrays to store text from FIND, HINTS, and REPLACE textboxes
        public static string[] frmColorFindTexts { get; set; } = new string[MaxTextBoxes];
        public static string[] frmColorReplaceTexts { get; set; } = new string[MaxTextBoxes];
        public static string[] frmColorToolTipsTexts { get; set; } = new string[MaxTextBoxes];

        // Arrays to store the foreground and background colors for FIND and REPLACE textboxes
        public static Color[] frmColorFindForeColors { get; set; } = new Color[MaxTextBoxes];
        public static Color[] frmColorFindBackColors { get; set; } = new Color[MaxTextBoxes];
        public static Color[] frmColorReplaceForeColors { get; set; } = new Color[MaxTextBoxes];
        public static Color[] frmColorReplaceBackColors { get; set; } = new Color[MaxTextBoxes];

        public static void ClearFindTexts()
        {
            for (int i = 0; i < MaxTextBoxes; i++)
            {
                frmColorFindTexts[i] = string.Empty;
            }
        }

        public static void ClearReplaceTexts()
        {
            for (int i = 0; i < MaxTextBoxes; i++)
            {
                frmColorReplaceTexts[i] = string.Empty;
            }
        }

        public static void ClearToolTipsTexts()
        {
            for (int i = 0; i < MaxTextBoxes; i++)
            {
                frmColorToolTipsTexts[i] = string.Empty;
            }
        }


        public static void SetText(int index, bool isFind, string text)
        {
            if (index < 1 || index > MaxTextBoxes)  // index - 1 so that the text box number aligns with the index number in the loop
                throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");

            if (isFind)
                frmColorFindTexts[index - 1] = text;  // Set the text in the find array
            else
                frmColorReplaceTexts[index - 1] = text;  // Set the text in the replace array
        }

        public static string GetText(int index, bool isFind)
        {
            if (index < 1 || index > MaxTextBoxes)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");

            // index - 1 so that the text box number aligns with the index number in the loop
            return isFind ? frmColorFindTexts[index - 1] : frmColorReplaceTexts[index - 1];  // Get the text from the appropriate array
        }

        public static void SetForeColor(int index, bool isFind, Color foreColor)
        {
            if (index < 1 || index > MaxTextBoxes)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");

            if (isFind)
            {
                // Apply foreground color if it is not null
                if (foreColor.IsKnownColor)
                    frmColorFindForeColors[index - 1] = foreColor;
            }
            else
            {
                // Apply foreground color if it is not null
                if (foreColor.IsKnownColor)
                    frmColorReplaceForeColors[index - 1] = foreColor;
            }
        }

        public static void SetBackColor(Control parentControl, int index, bool isFind, Color backColor)
        {
            if (index < 1 || index > MaxTextBoxes)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");

            if (isFind)
            {
                // Apply background color if it is not null
                if (backColor.IsKnownColor)
                    frmColorFindBackColors[index - 1] = backColor;

                // Direct set Find rich textboxes
                for (int i = 1; i <= MaxTextBoxes; i++)
                {
                    if (parentControl.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                    {
                        rtfFind.BackColor = backColor;
                    }
                }
            }
            else
            {
                // Apply background color if it is not null
                if (backColor.IsKnownColor)
                    frmColorReplaceBackColors[index - 1] = backColor;

                // Direct set Find rich textboxes
                for (int i = 1; i <= MaxTextBoxes; i++)
                {
                    if (parentControl.Controls[$"RtfReplace{i}"] is RichTextBox rtfReplace)
                    {
                        rtfReplace.BackColor = backColor;
                    }
                }
            }
        }


        public static void SetToolTips(int index, string text)
        {
            if (index < 1 || index > MaxTextBoxes) throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");
            frmColorToolTipsTexts[index - 1] = text;
        }

        public static string GetToolTips(int index)
        {
            if (index < 1 || index > MaxTextBoxes) throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 1 and {MaxTextBoxes}.");
            return frmColorToolTipsTexts[index - 1]; // Get the hint text from the hints array
        }


    }
}

