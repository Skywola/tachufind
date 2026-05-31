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
    public partial class FrmSection : Form
    {
        public FrmSection()
        {
            InitializeComponent();
        }

        private void FrmSection_Load(object sender, EventArgs e)
        {
            ScreenSetUp(this);

            Globals.FrmSectionInit = false;
            RTBSection.BackColor = Globals.User_Settings.RTBMainBackColor;
        }

        private void ScreenSetUp(Form form)
        {
            Size savedSize = Globals.User_Settings.FrmSectionSize;
            Point savedLocation = Globals.User_Settings.FrmSectionLocation;
            ScreenUtility.InitializeForm(form, savedLocation, savedSize);
        }

        private void FrmSection_LocationChanged(object sender, EventArgs e)
        {
            if (Globals.FrmSectionInit == true) { return; }

            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmSectionLocation = location);
        }

        private void FrmSection_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmSectionLocation = location);
            ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmSectionSize = size);
        }

        private void FrmSection_Shown(object sender, EventArgs e)
        {
            this.Width = AppSettings.FrmSectionWidth;
            this.Height = AppSettings.FrmSectionHeight;
        }

        private void FrmSection_SizeChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmSectionSize = size);
        }
    }
}
