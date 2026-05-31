using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    internal class ComboBoxItem
    {
        public string Display { get; }
        public string Value { get; }
        public ComboBoxItem(string display, string value) { Display = display; Value = value; }
        public override string ToString() => Display;
    }
}
