using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class LineInfo
    {
        //public string Line { get; set; }
        public int LineStart { get; set; }
        public int LineEnd { get; set; }
        public string IsLineBracketed { get; set; }

        public LineInfo(int lineStart, int lineEnd, string isLineBracketed)  // Old was: public LineInfo(string line, int lineStart, int lineEnd)
        {
            //Line = line;
            LineStart = lineStart;
            LineEnd = lineEnd;
            IsLineBracketed = isLineBracketed;
        }

        public void AddToStart(int value)
        {
            LineStart += value;
        }

        public void AddToEnd(int value)
        {
            LineEnd += value;
        }
    }
}
