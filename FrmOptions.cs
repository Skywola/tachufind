using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    public partial class FrmOptions : Form
    {
        public FrmOptions()
        {
            InitializeComponent();
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            if (Globals.User_Settings.FrmOptionsOpenFromLastLocation)
            {
                this.chkOpenFromLastLocation.Checked = true;
            }
            else { this.chkOpenFromLastLocation.Checked = false; }

            if (Globals.User_Settings.FrmOptionsOptOutOfFutureChangeFontWarnings)
            {
                this.chkOptOutOfFontChangeWarnings.Checked = true;
            }
            else { this.chkOptOutOfFontChangeWarnings.Checked = false; }

        }

        private void FrmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void ChkOpenFromLastLocation_CheckStateChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.FrmOptionsOpenFromLastLocation = chkOpenFromLastLocation.Checked;
        }

        private void ChkOptOutOfFontChangeWarnings_CheckStateChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.FrmOptionsOptOutOfFutureChangeFontWarnings = chkOptOutOfFontChangeWarnings.Checked;
        }

    }
}
