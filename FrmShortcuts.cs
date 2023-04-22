using System;
using System.Text;
using System.Windows.Forms;

namespace Tachufind
{
    public partial class FrmShortcuts : Form
    {
        public FrmShortcuts()
        {
            InitializeComponent();
        }

        private void FrmShortcuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void RTBShortcuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;
            }
        }


    }
}
