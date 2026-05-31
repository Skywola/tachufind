using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class BoundaryMarkerSearch
    {
        internal int indexBM { get; set; } = 0;
        internal string searchRangeText { get; set; } = String.Empty;
        internal int subStringPointerBM { get; set; } = 0;
        internal int startBMIndex { get; set; } = 0;
        internal int endBMIndex { get; set; } = 0;
        internal string findStringBM { get; set; } = String.Empty;
        internal string replaceStringBM { get; set; } = String.Empty;
        internal bool getNextBMSet { get; set; } = true;

        internal Color findColorBM { get; set; } = Color.Black;
        internal Font FontBM { get; set; } = new Font("Times New Roman", 22, FontStyle.Regular);

        internal int textBoxNumBM { get; set; } = 0;
        internal int subStringLengthBM { get; set; } = 0;
        internal int boundryMarkerCount { get; set; } = 0;
        internal bool searchBMComplete { get; set; } = false;
        public Affix AffixType { get; set; } = Affix.All;
        public Mode SearchMode { get; set; } = Mode.Text;

        // Method to clear the search and reset properties to default values
        public void Clear()
        {
            Color color = Color.Black;
            startBMIndex = 0;
            findStringBM = string.Empty;
            findColorBM = Color.Black;  // Reset to default color
            replaceStringBM = string.Empty;
            textBoxNumBM = 0;  // Assuming default is 0
            AffixType = Affix.All;  // Reset to default search type
            SearchMode = Mode.Text;
            FontBM = new Font("Times New Roman", 22, FontStyle.Regular);
            searchBMComplete = false;
        }


    }
}
