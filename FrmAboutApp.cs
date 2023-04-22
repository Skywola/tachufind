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
    public partial class FrmAboutApp : Form
    {
        public FrmAboutApp()
        {
            InitializeComponent();
        }

        private void FrmAboutApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void FrmAboutApp_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
