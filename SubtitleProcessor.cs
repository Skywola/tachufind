using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class SubtitleProcessor
    {
        static string storedTime = string.Empty;

        public List<Subtitle> ParseTopSubtitles(string subtitleContent)
        {
            List<Subtitle> subtitle = new List<Subtitle>();

            // Split the subtitle content into lines
            var lines = subtitleContent.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                // Line number
                if (int.TryParse(lines[i], out int lineNumber))
                {
                    // Ensure we don't go out of bounds
                    if (i + 1 >= lines.Length)
                    {
                        MessageBox.Show("Invalid subtitle format: Time range line is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    // Next line should be time range
                    string timeRange = lines[i + 1];

                    // Find the text lines
                    StringBuilder textBuilder = new StringBuilder();
                    int j = i + 2; // Start looking after the time range line
                    while (j < lines.Length && !string.IsNullOrWhiteSpace(lines[j]))
                    {
                        textBuilder.AppendLine(lines[j]);
                        j++;
                    }

                    // Create Subtitle object
                    Subtitle subtitleLine = new Subtitle(
                        lineNumber,
                        timeRange,
                        textBuilder.ToString().Trim(), // Remove any extra newline at the end
                        string.Empty, // Initialize BottomLineText as empty
                        string.Empty, // Initialize PreviousTimeRange as empty
                        string.Empty, // Initialize CurrentTimeRange as empty
                        string.Empty, // Initialize LastTime as empty
                        string.Empty, // Initialize CurrentTime as empty
                        TimeSpan.Zero // Initialize MinGap as TimeSpan.Zero
                    );
                    subtitle.Add(subtitleLine);

                    // Move i to the end of the text lines
                    i = j - 1;
                }
            }
            return subtitle;
        }


        public void AddBottomSubtitles(List<Subtitle> subtitles, string subtitleContent)
        {
            if (subtitles == null)
            {
                MessageBox.Show("Subtitle lines list cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(subtitleContent))
            {
                MessageBox.Show("Subtitle content cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lines = subtitleContent.Split(new[] { "\r\n" }, StringSplitOptions.None);
            int index = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                // Check if the current line is a line number
                if (int.TryParse(lines[i], out int lineNumber))
                {
                    // Ensure we don't go out of bounds
                    if (i + 1 >= lines.Length)
                    {
                        MessageBox.Show("Invalid subtitle format: Time range line is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    // Find the text lines
                    StringBuilder textBuilder = new StringBuilder();
                    int j = i + 2; // Start looking after the time range line
                    while (j < lines.Length && !string.IsNullOrWhiteSpace(lines[j]))
                    {
                        textBuilder.AppendLine(lines[j]);
                        j++;
                    }

                    // Add BottomLineText to existing SubtitleLine object
                    if (index < subtitles.Count)
                    {
                        subtitles[index].BottomLanguageText = textBuilder.ToString().Trim(); // Remove any extra newline at the end
                        index++;
                    }

                    // Move i to the end of the text lines
                    i = j - 1;
                }
            }
        }


    }
}
