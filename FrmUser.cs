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
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            if (!Globals.UserHasAgreed) {
                Globals.User_Settings.StrSetUserAgreed = "";
            }
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Globals.UserHasAgreed = false;
            Application.Exit();
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            Globals.User_Settings.StrSetUserAgreed = "согласен";
            Globals.UserHasAgreed = true;
            this.Visible = false;
        }
    }
}
