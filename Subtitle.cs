using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class Subtitle
    {
        public int LineNumber { get; set; }
        public string TimeRange { get; set; }
        public string TopLanguageText { get; set; }
        public string BottomLanguageText { get; set; }

        public string PreviousTimeRange { get; set; }
        public string CurrentTimeRange { get; set; }

        public string LastTime { get; set; }
        public string CurrentTime { get; set; }

        TimeSpan MinGap;

        public Subtitle(int lineNumber, string timeRange, string topLanguageText, string bottomLanguageText,
            string previousTimeRange, string currentTimeRange, string lastTime, string currentTime, TimeSpan minGap)
        {
            LineNumber = lineNumber;
            TimeRange = timeRange;
            TopLanguageText = topLanguageText;
            BottomLanguageText = bottomLanguageText;
            PreviousTimeRange = previousTimeRange;
            CurrentTimeRange = currentTimeRange;
            LastTime = lastTime;
            CurrentTime = currentTime;
            MinGap = minGap;
        }
    }
}
