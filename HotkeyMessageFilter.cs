using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public class HotkeyMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x100) // WM_KEYDOWN
            {
                Keys key = (Keys)m.WParam;

                if (key == Keys.F3)
                {
                    if (Globals.Current_RTB_withFocus != null &&
                        Globals.Current_RTB_withFocus.SelectionLength > 0)
                    {
                        Clipboard.SetText(Globals.Current_RTB_withFocus.SelectedText);
                    }
                    return true;
                }

                if (key == Keys.F4)
                {
                    if (Globals.Current_RTB_withFocus != null)
                    {
                        Globals.Current_RTB_withFocus.Paste();
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
