using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


namespace Tachufind
{
    public class SearchInstance
	{
		public int position;
		public string findString { get; set; }
		public Color findColor { get; set; }
		public string replaceString { get; set; }
		public int textBoxNum { get; set; }
		public RichTextBoxFinds RichTextBoxFinds_GFindMode { get; set; }
	}
}
