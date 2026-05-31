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
            ScreenSetUp(this);

            if (Globals.User_Settings.FrmOptionsOpenFromLastLocation)
            {
                this.chkOpenFromLastLocation.Checked = true;
            }
            else { this.chkOpenFromLastLocation.Checked = false; }
        }

        private void ScreenSetUp(Form form)
        {
            Point savedLocation = Globals.User_Settings.FrmOptionsLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        private void FrmOptions_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmOptionsLocation = location);
        }

        private void FrmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmOptionsLocation = location);
        }


        private void chkOpenFromLastLocation_CheckStateChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.FrmOptionsOpenFromLastLocation = chkOpenFromLastLocation.Checked;
        }

        private void chkPreserveHighlighting_CheckedChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.ChkPreserveHighlighting = !Globals.User_Settings.ChkPreserveHighlighting;
        }


        private void chkUseDefaultColors_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDefaultColors.Checked)
            {
                Globals.User_Settings.UseDefaultColors = true;
            }
            else
            {
                Globals.User_Settings.UseDefaultColors = false;
            }
        }

        private void ChkLoopTTSPlayback_CheckedChanged(object sender, EventArgs e)
        {
            // LoopTTSPlayback
            if (ChkLoopTTSPlayback.Checked)
            {
                Globals.User_Settings.UseDefaultColors = true;
            }
            else
            {
                Globals.User_Settings.UseDefaultColors = false;
            }
        }
    }
}
