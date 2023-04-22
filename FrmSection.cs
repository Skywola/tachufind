using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        private void FrmSection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Size sz = new Size(this.Width, this.Height);
            Globals.User_Settings.FrmSectionSize = sz;
            this.Visible = false;
            e.Cancel = true;
        }

        private void FrmSection_LocationChanged(object sender, EventArgs e)
        {
            if (Globals.FrmSectionInit == true) { return; }
            Point pt = new Point(this.Left, this.Top);
            Globals.User_Settings.FrmSectionLocation = pt;
        }

        private void FrmSection_Load(object sender, EventArgs e)
        {
            //this.Width = Globals.user_settings.FrmSectionSize.Width;
            //this.Height = Globals.user_settings.FrmSectionSize.Height;
            // Set the size of the RichTextBox based on the preferred size
            this.Left = Globals.User_Settings.FrmSectionLocation.X;
            this.Top = Globals.User_Settings.FrmSectionLocation.Y;
            Globals.FrmSectionInit = false;
        }

    }
}
