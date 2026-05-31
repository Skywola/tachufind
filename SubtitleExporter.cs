using System;
using System.IO;
using System.Text;

namespace Tachufind
{
    public class SubtitleExporter
    {
        // Make the method public or internal so it can be used
        public string FormatTimestamp(TimeSpan time)
        {
            return $"{(int)time.TotalHours:00}:{time.Minutes:00}:{time.Seconds:00},{time.Milliseconds:000}";
        }

        public void ExportBilingualToSRT(string srtFilePath, List<BilingualDictionaryEntry> entries)
        {
            using (StreamWriter writer = new StreamWriter(srtFilePath, false, Encoding.UTF8))
            {
                int displayDurationSeconds = 4;

                for (int i = 0; i < entries.Count; i++)
                {
                    writer.WriteLine((i + 1).ToString());

                    TimeSpan startTime = TimeSpan.FromSeconds(i * displayDurationSeconds);
                    TimeSpan endTime = TimeSpan.FromSeconds((i + 1) * displayDurationSeconds);

                    // Now FormatTimestamp is accessible
                    writer.WriteLine($"{FormatTimestamp(startTime)} --> {FormatTimestamp(endTime)}");

                    writer.WriteLine(entries[i].Language1);
                    writer.WriteLine(entries[i].Language2);

                    writer.WriteLine();
                }
            }
        }
    }
}
