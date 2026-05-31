using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{

    // If you have bilingual entries
    public class BilingualDictionaryEntry
    {
        public string Language1 { get; set; }
        public string Language2 { get; set; }

        // Public method that can be called from anywhere in the class
        public string FormatTimestamp(TimeSpan time)
        {
            return $"{(int)time.TotalHours:00}:{time.Minutes:00}:{time.Seconds:00},{time.Milliseconds:000}";
        }


        public void ExportBilingualToSRT(string srtFilePath, List<BilingualDictionaryEntry> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                throw new ArgumentException("Entries list cannot be null or empty");
            }

            using (StreamWriter writer = new StreamWriter(srtFilePath, false, Encoding.UTF8))
            {
                int displayDurationSeconds = 4;

                for (int i = 0; i < entries.Count; i++)
                {
                    // 1. Subtitle number
                    writer.WriteLine((i + 1).ToString());

                    // 2. Calculate and format timestamps
                    TimeSpan startTime = TimeSpan.FromSeconds(i * displayDurationSeconds);
                    TimeSpan endTime = TimeSpan.FromSeconds((i + 1) * displayDurationSeconds);

                    // Call the class method
                    string timeStart = FormatTimestamp(startTime);
                    string timeEnd = FormatTimestamp(endTime);

                    writer.WriteLine($"{timeStart} --> {timeEnd}");

                    // 3. Subtitle text (bilingual)
                    writer.WriteLine(entries[i].Language1);
                    writer.WriteLine(entries[i].Language2);

                    // 4. Empty line between entries
                    writer.WriteLine();
                }
            }
        }
    }
}
