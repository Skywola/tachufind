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
    public partial class FrmKBShortcuts : Form
    {
        public FrmKBShortcuts()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void FrmKBShortcuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FrmKBShortcuts_Load(object sender, EventArgs e)
        {
            ScreenSetUp(this);
        }

        private void ScreenSetUp(Form form)
        {
            Size savedSize = Globals.User_Settings.FrmKBShortcutsSize;
            Point savedLocation = Globals.User_Settings.FrmKBShortcutsLocation;
            ScreenUtility.InitializeForm(form, savedLocation, savedSize);
        }

        private void FrmKBShortcuts_LocationChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmKBShortcutsLocation = location);
        }

        private void FrmKBShortcuts_SizeChanged(object sender, EventArgs e)
        {
            ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmKBShortcutsSize = size);
        }

        private void FrmKBShortcuts_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure the position and size are valid and saved
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmKBShortcutsLocation = location);
            ScreenUtility.HandleSizeChanged(this, size => Globals.User_Settings.FrmKBShortcutsSize = size);
        }
    }
}

