using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BitMiracle.LibTiff.Classic;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;


namespace Tachufind
{
    public partial class FrmColor : Form
    {

        #region Initialization

        internal static FrmColor _instance = null!;
        internal static readonly object _lock = new object();

        private static Search search = new();

        public FrmColor()
        {
            InitializeComponent();
        }

        public static FrmColor Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    lock (_lock)
                    {
                        if (_instance == null || _instance.IsDisposed)
                        {
                            _instance = new FrmColor();
                        }
                    }
                }
                return _instance;
            }
        }


        private bool formLoading = true;   // This IS used, despite the warning

        #endregion  // "Initialization"


        #region FrmColorActivate_And_load
        private void FrmColor_Activated(object sender, EventArgs e)
        {
            try
            {
                if (AppSettings.SearchJustRetrieved == true)
                {
                    RtfSearchName.Text = SearchSettings.SearchName;
                    this.RbEditColor.Checked = SearchSettings.FrmColorReplaceMode;
                    this.RbTextReplace.Checked = !SearchSettings.FrmColorReplaceMode;
                    this.ChkMatchCase.Checked = SearchSettings.ChkMatchCase;
                    this.ChkWordOnly.Checked = SearchSettings.ChkWordOnly;
                    AppSettings.SearchJustRetrieved = false;
                }

                for (int i = 1; i <= 10; i++)
                {
                    if (this.Controls[$"RtfFind{i}"] is RichTextBox RtfFind)
                    {
                        RtfFind.Text = SearchSettings.GetText(i, true); // true means RtfFind
                    }
                    if (this.Controls[$"RtfReplace{i}"] is RichTextBox RtfReplace)
                    {
                        RtfReplace.Text = SearchSettings.GetText(i, false); // false means rtfReplace
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FrmColor_Load(object sender, EventArgs e)
        {
            try
            {

                ScreenSetUp(this);
                this.KeyPreview = true;

                // Button colors are set based on functionality, to hightlight their difference in function
                this.btnClear.BackColor = Color.AntiqueWhite;

                this.btnGetSearch.BackColor = Color.LightGreen;
                this.btnSaveSearch.BackColor = Color.LightGreen;

                this.btnPasteInto.BackColor = Color.LightSkyBlue;

                this.BtnUndo.BackColor = Color.Gold;
                this.btnClearAll.BackColor = Color.Gold;
                this.btnClearFinds.BackColor = Color.Gold;
                this.btnClearReplace.BackColor = Color.Gold;
                this.BtnSearchFolder.BackColor = Color.BlueViolet;

                Color colorGlobalBackcolor = Globals.User_Settings.RTBMainBackColor;
                RtfSearchName.BackColor = Globals.User_Settings.RTBMainBackColor;
                for (int i = 1; i < 11; i++)
                {
                    // Set backcolors for FrmFind and FrmReplace, the bool switches between the two
                    SearchSettings.SetBackColor(this, i, true, colorGlobalBackcolor);
                    SearchSettings.SetBackColor(this, i, false, colorGlobalBackcolor);
                }

                this.ChkMatchCase.TabStop = false;
                this.ChkWordOnly.TabStop = false;
                this.RbEditColor.TabStop = false;

                for (int i = 1; i <= 10; i++)
                {
                    //  lblReplace01 - 10.Text = "Notes " 
                    var control = this.Controls[$"lblReplace{i:00}"];

                    if (control != null && control.Text != null && control.Text.Length > 0)
                    {
                        control.Text = "Notes ";
                    }
                }

                ChkMatchCase.Checked = false;
                ChkWordOnly.Checked = false;
                Globals.Current_RTB_withFocus = RtfFind1;

                Cursor.Current = Cursors.WaitCursor;
                GetFrmTextAndColorValues();
                this.Left = Globals.User_Settings.FrmColorLocation.X;
                this.Top = Globals.User_Settings.FrmColorLocation.Y;

                //Globals.User_Settings.SearchTimeLimit = Convert.ToInt32(this.TxtTimeIndicator.Text);
                this.TxtTimeIndicator.Text = Globals.User_Settings.SearchTimeLimit.ToString();

                // TODO THIS SHOULD NOT BE ACCESSING Globals.User_Settings.FilePath   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                //if (string.IsNullOrEmpty(Globals.User_Settings.FilePath))
                //{
                //    this.RtfSearchName.Text = "";
                //    return;
                //}
                formLoading = true;
                this.BringToFront();

                Cursor.Current = Cursors.Default;
                MyUserSettings myUserSettings = new();
                // Set FrmColor Mode, Boolean  Color Replace or Text Replace
                SearchSettings.FrmColorReplaceMode = myUserSettings.FrmColor_ReplaceMode;  // Initialization
                RbEditColor.Checked = SearchSettings.FrmColorReplaceMode;
                RbTextReplace.Checked = !SearchSettings.FrmColorReplaceMode;

                ActivateReplaceMode();
                RtfFind1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActivateReplaceMode()
        {
            if (SearchSettings.FrmColorReplaceMode)
            {
                RbEditColor.PerformClick();
            }
            else {
                RbTextReplace.PerformClick();
            }
        }

        private void ScreenSetUp(Form form)
        {
            Point pt = new Point(0, 0);
            Point savedLocation = Globals.User_Settings.FrmQuizLocation;
            ScreenUtility.InitializeForm(form, savedLocation);
        }

        public void GetFrmTextAndColorValues()
        {
            // LOCATION   → →  → →   Get FrmColor Settings 
            GetAndSetRtfFindForeColors();
            MyUserSettings myUserSettings = new();
            this.RbEditColor.Checked = SearchSettings.FrmColorReplaceMode = myUserSettings.FrmColor_ReplaceMode; // Color mode
            this.RbTextReplace.Checked = !SearchSettings.FrmColorReplaceMode; //inverted (radio button)
            this.ChkMatchCase.Checked = SearchSettings.ChkMatchCase;
            this.ChkWordOnly.Checked = SearchSettings.ChkWordOnly;
        }

        public void GetAndSetRtfFindForeColors()
        {
            try
            {

                if (RbEditColor.Checked)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        var color = ColorManager.GetColor($"C{i}");

                        // Get the button control and check if it exists
                        var buttonControl = this.Controls[$"FrmColorBtnC{i}"];
                        if (buttonControl != null)
                        {
                            buttonControl.BackColor = color;
                        }
                        else
                        {
                            MessageBox.Show($"FrmColorBtnC{i} not found");
                        }

                        // Get the rich text box control and check if it exists
                        var richTextBoxControl = this.Controls[$"RtfFind{i}"];
                        if (richTextBoxControl != null)
                        {
                            richTextBoxControl.ForeColor = color;
                        }
                        else
                        {
                            MessageBox.Show($"RtfFind{i} not found");
                        }
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                        {
                            rtfFind.SelectAll();
                            rtfFind.SelectionColor = ColorManager.GetColor($"C{i}");
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        // Get the button control and check if it exists
                        var buttonControl = this.Controls[$"FrmColorBtnC{i}"];
                        if (buttonControl != null)
                        {
                            buttonControl.BackColor = ColorManager.GetColor($"C{i}");
                        }
                        else
                        {
                            MessageBox.Show($"FrmColorBtnC{i} not found");
                        }
                    }

                    // Use only Black forecolor
                    for (int i = 1; i <= 10; i++)
                    {
                        SearchSettings.SetForeColor(i, true, Color.Black);   // Findx.ForeColor = Color.Black;

                        if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                        {
                            rtfFind.SelectAll();
                            rtfFind.SelectionColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion // FrmColorActivate_And_load


        #region BtnC01-10__Click
        private void BtnC01_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C1", selectedColor);
                    Globals.User_Settings.Color1 = selectedColor;

                    // These two should always be identically matched
                    if (frmMain != null)
                    {
                        FrmColorBtnC1.BackColor = frmMain.FrmMainColorBtn1.BackColor = selectedColor;
                    }
                    RtfFind1.SelectAll();
                    RtfFind1.ForeColor = selectedColor;
                    RtfFind1.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC02_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C2", selectedColor);  // Globals.User_Settings.Color02 = selectedColor;
                    Globals.User_Settings.Color2 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC2.BackColor = frmMain.FrmMainColorBtn2.BackColor = selectedColor;
                    }
                    RtfFind2.SelectAll();
                    RtfFind2.ForeColor = selectedColor;
                    RtfFind2.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC03_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C3", selectedColor);
                    Globals.User_Settings.Color3 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC3.BackColor = frmMain.FrmMainColorBtn3.BackColor = selectedColor;
                    }
                    RtfFind3.SelectAll();
                    RtfFind3.ForeColor = selectedColor;
                    RtfFind3.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC04_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C4", selectedColor);
                    Globals.User_Settings.Color4 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC4.BackColor = frmMain.FrmMainColorBtn4.BackColor = selectedColor;
                    }
                    RtfFind4.SelectAll();
                    RtfFind4.ForeColor = selectedColor;
                    RtfFind4.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC05_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C5", selectedColor);
                    Globals.User_Settings.Color5 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC5.BackColor = frmMain.FrmMainColorBtn5.BackColor = selectedColor;
                    }
                    RtfFind5.SelectAll();
                    RtfFind5.ForeColor = selectedColor;
                    RtfFind5.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC06_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C6", selectedColor);
                    Globals.User_Settings.Color6 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC6.BackColor = frmMain.FrmMainColorBtn6.BackColor = selectedColor;
                    }
                    RtfFind6.SelectAll();
                    RtfFind6.ForeColor = selectedColor;
                    RtfFind6.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC07_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C7", selectedColor);
                    Globals.User_Settings.Color7 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC7.BackColor = frmMain.FrmMainColorBtn7.BackColor = selectedColor;
                    }
                    RtfFind7.SelectAll();
                    RtfFind7.ForeColor = selectedColor;
                    RtfFind7.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC08_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C8", selectedColor);
                    Globals.User_Settings.Color8 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC8.BackColor = frmMain.FrmMainColorBtn8.BackColor = selectedColor;
                    }
                    RtfFind8.SelectAll();
                    RtfFind8.ForeColor = selectedColor;
                    RtfFind8.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC09_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C9", selectedColor);
                    Globals.User_Settings.Color9 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC9.BackColor = frmMain.FrmMainColorBtn9.BackColor = selectedColor;
                    }
                    RtfFind9.SelectAll();
                    RtfFind9.ForeColor = selectedColor;
                    RtfFind9.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnC10_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                Globals.User_Settings.UseDefaultColors = false; // allow User to change colors
                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    ColorManager.SetColor("C10", selectedColor);
                    Globals.User_Settings.Color10 = selectedColor;
                    if (frmMain != null)
                    {
                        FrmColorBtnC10.BackColor = frmMain.FrmMainColorBtn10.BackColor = selectedColor;
                    }
                    RtfFind10.SelectAll();
                    RtfFind10.ForeColor = selectedColor;
                    RtfFind10.SelectionLength = 0;
                    Globals.BoolFrmColorSearchSaved = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // BtnC01-10__Click

        #region RtfFind01-10__Click
        private void RtfFind01_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind1;
        }

        private void RtfFind02_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind2;
        }

        private void RtfFind03_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind3;
        }

        private void RtfFind04_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind4;
        }

        private void RtfFind05_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind5;
        }

        private void RtfFind06_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind6;
        }

        private void RtfFind07_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind7;
        }

        private void RtfFind08_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind8;
        }

        private void RtfFind09_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind9;
        }

        private void RtfFind10_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind10;
        }
        #endregion // RtfFind01-10__Click

        #region RtfReplace01-10__Click
        private void RtfReplace01_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace1;
        }

        private void RtfReplace02_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace2;
        }

        private void RtfReplace03_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace3;
        }

        private void RtfReplace04_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace4;
        }

        private void RtfReplace05_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace5;
        }

        private void RtfReplace06_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace6;
        }

        private void RtfReplace07_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace7;
        }

        private void RtfReplace08_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace8;
        }

        private void RtfReplace09_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace9;
        }

        private void RtfReplace10_Click(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace10;
        }
        #endregion // RtfReplace01-10__Click

        #region RtfFind01-10__DoubleClick
        private void RtfFind01_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind1;
            RtfFind1.SelectAll();
        }

        private void RtfFind02_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind2;
            RtfFind2.SelectAll();
        }

        private void RtfFind03_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind3;
            RtfFind3.SelectAll();
        }

        private void RtfFind04_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind4;
            RtfFind4.SelectAll();
        }

        private void RtfFind05_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind5;
            RtfFind5.SelectAll();
        }

        private void RtfFind06_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind6;
            RtfFind6.SelectAll();
        }

        private void RtfFind07_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind7;
            RtfFind7.SelectAll();
        }

        private void RtfFind08_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind8;
            RtfFind8.SelectAll();
        }

        private void RtfFind09_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind9;
            RtfFind9.SelectAll();
        }

        private void RtfFind10_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfFind10;
            RtfFind10.SelectAll();
        }
        #endregion // RtfFind01-10__DoubleClick

        #region RtfReplace01-10__DoubleClick
        private void RtfReplace01_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace1;
            RtfReplace1.SelectAll();
        }

        private void RtfReplace02_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace2;
            RtfReplace2.SelectAll();
        }

        private void RtfReplace03_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace3;
            RtfReplace3.SelectAll();
        }

        private void RtfReplace04_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace4;
            RtfReplace4.SelectAll();
        }

        private void RtfReplace05_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace5;
            RtfReplace5.SelectAll();
        }

        private void RtfReplace06_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace6;
            RtfReplace6.SelectAll();
        }

        private void RtfReplace07_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace7;
            RtfReplace7.SelectAll();
        }

        private void RtfReplace08_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace8;
            RtfReplace8.SelectAll();
        }

        private void RtfReplace09_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace9;
            RtfReplace9.SelectAll();
        }

        private void RtfReplace10_DoubleClick(object sender, EventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfReplace10;
            RtfReplace10.SelectAll();
        }
        #endregion // RtfReplace01-10__DoubleClick

        #region RtfFind01-10__MouseDown
        private void RtfFind01_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind1.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind1;
        }

        private void RtfFind02_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind2.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind2;
        }

        private void RtfFind03_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind3.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind3;
        }

        private void RtfFind04_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind4.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind4;
        }

        private void RtfFind05_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind5.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind5;
        }

        private void RtfFind06_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind6.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind6;
        }

        private void RtfFind07_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind7.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind7;
        }

        private void RtfFind08_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind8.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind8;
        }

        private void RtfFind09_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind9.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind9;
        }

        private void RtfFind10_MouseDown(object sender, MouseEventArgs e)
        {
            RtfFind10.Focus();
            Globals.Current_RTB_withFocus = this.RtfFind10;
        }
        #endregion // RtfFind01-10__MouseDown

        #region RtfReplace01-10__MouseDown
        private void RtfReplace01_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace1.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace1;
        }

        private void RtfReplace02_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace2.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace2;
        }

        private void RtfReplace03_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace3.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace3;
        }

        private void RtfReplace04_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace4.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace4;
        }

        private void RtfReplace05_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace5.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace5;
        }

        private void RtfReplace06_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace6.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace6;
        }

        private void RtfReplace07_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace7.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace7;
        }

        private void RtfReplace08_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace8.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace8;
        }

        private void RtfReplace09_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace9.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace9;
        }

        private void RtfReplace10_MouseDown(object sender, MouseEventArgs e)
        {
            RtfReplace10.Focus();
            Globals.Current_RTB_withFocus = this.RtfReplace10;
        }
        #endregion // RtfReplace01-10__MouseDown

        #region RtfFind01-10_PreviewKeyDown
        private void RtfFind01_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace1;
            }
        }

        private void RtfFind02_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace2;
            }
        }

        private void RtfFind03_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace3;
            }
        }

        private void RtfFind04_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace4;
            }
        }

        private void RtfFind05_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace5;
            }
        }

        private void RtfFind06_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace6;
            }
        }

        private void RtfFind07_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace7;
            }
        }

        private void RtfFind08_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace8;
            }
        }

        private void RtfFind09_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace9;
            }
        }

        private void RtfFind10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfReplace10;
            }
        }
        #endregion // RtfFind01-10_PreviewKeyDown

        #region RtfReplace01-10__PreviewKeyDown
        private void RtfReplace01_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind2;
            }

        }

        private void RtfReplace02_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind3;
            }
        }

        private void RtfReplace03_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind4;
            }
        }

        private void RtfReplace04_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind5;
            }
        }

        private void RtfReplace05_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind6;
            }
        }

        private void RtfReplace06_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind7;
            }
        }

        private void RtfReplace07_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind8;
            }
        }

        private void RtfReplace08_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind9;
            }
        }

        private void RtfReplace09_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind10;
            }
        }

        private void RtfReplace10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                Globals.Current_RTB_withFocus = RtfFind1;
            }
        }
        #endregion // RtfReplace01-10__PreviewKeyDown

        #region LblPipe01-10__Click
        private void LblPipe01_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind1.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind1.Text = this.RtfFind1.Text.Insert(cursorloct, "|");
                this.RtfFind1.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe02_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind2.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind2.Text = this.RtfFind2.Text.Insert(cursorloct, "|");
                this.RtfFind2.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe03_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind3.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind3.Text = this.RtfFind3.Text.Insert(cursorloct, "|");
                this.RtfFind3.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe04_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind4.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind4.Text = this.RtfFind4.Text.Insert(cursorloct, "|");
                this.RtfFind4.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe05_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind5.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind5.Text = this.RtfFind5.Text.Insert(cursorloct, "|");
                this.RtfFind5.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe06_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind6.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind6.Text = this.RtfFind6.Text.Insert(cursorloct, "|");
                this.RtfFind6.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe07_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind7.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind7.Text = this.RtfFind7.Text.Insert(cursorloct, "|");
                this.RtfFind7.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LlblPipe08_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind8.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind8.Text = this.RtfFind8.Text.Insert(cursorloct, "|");
                this.RtfFind8.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe09_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind9.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind9.Text = this.RtfFind9.Text.Insert(cursorloct, "|");
                this.RtfFind9.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }

        private void LblPipe10_Click(object sender, EventArgs e)
        {
            int cursorloct = this.RtfFind10.Text.Length;
            if (cursorloct > 0)
            {
                this.RtfFind10.Text = this.RtfFind10.Text.Insert(cursorloct, "|");
                this.RtfFind10.SelectionStart = cursorloct + 1;
            }
            else
            {
                MessageBox.Show("Enter your first search value before the pipe symbol!", "Error Detected", MessageBoxButtons.OK);
                return;
            }
        }
        #endregion // LblPipe01-10__Click

        #region RtfFind01-10__KeyPress

        private void RtfFind01_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind1, this.FrmColorBtnC1.ForeColor);
        }

        private void RtfFind02_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind2, this.FrmColorBtnC2.ForeColor);
        }

        private void RtfFind03_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind3, this.FrmColorBtnC3.ForeColor);
        }

        private void RtfFind04_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind4, this.FrmColorBtnC4.ForeColor);
        }

        private void RtfFind05_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind5, this.FrmColorBtnC5.ForeColor);
        }

        private void RtfFind06_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind6, this.FrmColorBtnC6.ForeColor);
        }

        private void RtfFind07_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind7, this.FrmColorBtnC7.ForeColor);
        }

        private void RtfFind08_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind8, this.FrmColorBtnC8.ForeColor);
        }

        private void RtfFind09_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind9, this.FrmColorBtnC9.ForeColor);
        }

        private void RtfFind10_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfFind10, this.FrmColorBtnC10.ForeColor);
        }
        #endregion // RtfFind01-10__KeyPress

        #region RtfReplace01-10__KeyPress

        private void RtfReplace01_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace1, Color.Black);
        }

        private void RtfReplace02_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace2, Color.Black);
        }

        private void RtfReplace03_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace3, Color.Black);
        }

        private void RtfReplace04_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace4, Color.Black);
        }

        private void RtfReplace05_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace5, Color.Black);
        }

        private void RtfReplace06_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace6, Color.Black);
        }

        private void RtfReplace07_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace7, Color.Black);
        }

        private void RtfReplace08_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace8, Color.Black);
        }

        private void RtfReplace09_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace9, Color.Black);
        }

        private void RtfReplace10_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfReplace10, Color.Black);
        }
        #endregion // RtfReplace01-10__KeyPress

        #region RtfFind01-10__MouseClick
        private void RtfFind01_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind02_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind03_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind04_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind05_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind06_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind07_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind08_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind09_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfFind10_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }
        #endregion // RtfFind01-10__MouseClick

        #region RtfReplace01-10__MouseClick

        private void RtfReplace01_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace02_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace03_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace04_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace05_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace06_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace07_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace08_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace09_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfReplace10_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        #endregion // RtfReplace01-10__MouseClick

        #region RtfFind1-10__Leave
        private void RtfFind1_Leave(object sender, EventArgs e)
        {

            var originalFont = RtfFind1.Font;
            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind1.SelectAll();
            RtfFind1.SelectionBackColor = Globals.User_Settings.RTBMainBackColor; 
            RtfFind1.SelectionFont = boldFont;

            RtfFind1.SelectionLength = 0;
            // Moving these to the textchanged event for all RtfFinds
            SearchSettings.SetText(1, true, RtfFind1.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            // END

            this.KeyPreview = true;
        }

        private void RrtfFind2_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind2.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind2.SelectAll();
            RtfFind2.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind2.SelectionFont = boldFont;

            RtfFind2.SelectionLength = 0;
            SearchSettings.SetText(2, true, RtfFind2.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind3_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind3.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind3.SelectAll();
            RtfFind3.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind3.SelectionFont = boldFont;

            RtfFind3.SelectionLength = 0;
            SearchSettings.SetText(3, true, RtfFind3.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind4_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind4.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind4.SelectAll();
            RtfFind4.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind4.SelectionFont = boldFont;

            RtfFind4.SelectionLength = 0;
            SearchSettings.SetText(4, true, RtfFind4.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind5_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind5.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind5.SelectAll();
            RtfFind5.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind5.SelectionFont = boldFont;

            RtfFind5.SelectionLength = 0;
            SearchSettings.SetText(5, true, RtfFind5.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind6_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind6.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind6.SelectAll();
            RtfFind6.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind6.SelectionFont = boldFont;

            RtfFind6.SelectionLength = 0;
            SearchSettings.SetText(6, true, RtfFind6.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind7_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind7.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind7.SelectAll();
            RtfFind7.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind7.SelectionFont = boldFont;

            RtfFind7.SelectionLength = 0;
            SearchSettings.SetText(7, true, RtfFind7.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind8_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind8.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind8.SelectAll();
            RtfFind8.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind8.SelectionFont = boldFont;

            RtfFind8.SelectionLength = 0;
            SearchSettings.SetText(8, true, RtfFind8.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind9_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind9.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind9.SelectAll();
            RtfFind9.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind9.SelectionFont = boldFont;

            RtfFind9.SelectionLength = 0;
            SearchSettings.SetText(9, true, RtfFind9.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }

        private void RtfFind10_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfFind10.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfFind10.SelectAll();
            RtfFind10.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfFind10.SelectionFont = boldFont;

            RtfFind10.SelectionLength = 0;
            SearchSettings.SetText(10, true, RtfFind10.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
            this.KeyPreview = true;
        }
        #endregion // RtfFind1-10__Leave

        #region RtfReplace1-10__Leave

        private void RtfReplace1_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace1.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace1.SelectAll();
            RtfReplace1.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace1.SelectionFont = boldFont;
            RtfReplace1.SelectionLength = 0;
            SearchSettings.SetText(1, false, RtfReplace1.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace2_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace2.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace2.SelectAll();
            RtfReplace2.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace2.SelectionFont = boldFont;
            RtfReplace2.SelectionLength = 0;
            SearchSettings.SetText(2, false, RtfReplace2.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace3_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace3.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace3.SelectAll();
            RtfReplace3.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace3.SelectionFont = boldFont;
            RtfReplace3.SelectionLength = 0;
            SearchSettings.SetText(3, false, RtfReplace3.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace4_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace4.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace4.SelectAll();
            RtfReplace4.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace4.SelectionFont = boldFont;
            RtfReplace4.SelectionLength = 0;
            SearchSettings.SetText(4, false, RtfReplace4.Text);
            this.KeyPreview = true;
        }
        private void RtfReplace5_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace5.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace5.SelectAll();
            RtfReplace5.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace5.SelectionFont = boldFont;
            RtfReplace5.SelectionLength = 0;
            SearchSettings.SetText(5, false, RtfReplace5.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace6_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace6.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace6.SelectAll();
            RtfReplace6.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace6.SelectionFont = boldFont;
            RtfReplace6.SelectionLength = 0;
            SearchSettings.SetText(6, false, RtfReplace6.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace7_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace7.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace7.SelectAll();
            RtfReplace7.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace7.SelectionFont = boldFont;
            RtfReplace7.SelectionLength = 0;
            SearchSettings.SetText(7, false, RtfReplace7.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace8_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace8.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace8.SelectAll();
            RtfReplace8.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace8.SelectionFont = boldFont;
            RtfReplace8.SelectionLength = 0;
            SearchSettings.SetText(8, false, RtfReplace8.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace9_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace9.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace9.SelectAll();
            RtfReplace9.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace9.SelectionFont = boldFont;
            RtfReplace9.SelectionLength = 0;
            SearchSettings.SetText(9, false, RtfReplace9.Text);
            this.KeyPreview = true;
        }

        private void RtfReplace10_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfReplace10.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfReplace10.SelectAll();
            RtfReplace10.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfReplace10.SelectionFont = boldFont;
            RtfReplace10.SelectionLength = 0;
            SearchSettings.SetText(10, false, RtfReplace10.Text);
            this.KeyPreview = true;
        }

        #endregion // RtfReplace1-10__Leave

        #region RtfFind01-10__KeyDown
        private void RtfFind01_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind02_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind03_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind04_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind05_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind06_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind07_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind08_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind09_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfFind10_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }
        #endregion // RtfFind01-10__KeyDown

        #region RtfReplace01-10__KeyDown
        private void RtfReplace01_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace02_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace03_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace04_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace05_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace06_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace07_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace08_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace09_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfReplace10_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }
        #endregion // RtfReplace01-10__KeyDown



        #region GeneralFunctions

        public static FrmGetSearches GetOrCreateFrmGetSearches()
        {
            // Check if FrmGetSearches already exists
            var frmGetSearches = Application.OpenForms.OfType<FrmGetSearches>().FirstOrDefault();
            if (frmGetSearches == null)
            {
                // Create a new instance if it doesn't exist
                frmGetSearches = new FrmGetSearches();
            }
            else
            {
                frmGetSearches.Visible = true;
            }
            return frmGetSearches;
        }

        void SaveSearch()
        {
            try
            {
                if (RtfSearchName.Text.Length < 1)
                {
                    MessageBox.Show("Search name must be entered in order to save it.", "Error Detected", MessageBoxButtons.OK);
                    return;
                }
                SearchSettings.SearchName = RtfSearchName.Text;
                // Get Title in .fdta file
                if (File.Exists(AppSettings.Data_Folder + RtfSearchName.Text + AppSettings.FindReplaceFileExtension))
                {
                    int answer = (int)MessageBox.Show("This search exists, overwrite?", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == (int)System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                BuildFindReplace();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BuildFindReplace()
        {
            string fileContents = "";
            var searchItemsQueue = new Queue<string>(); // Holds a single search settings

            try
            {
                // Colors
                searchItemsQueue.Enqueue("Reserved");  // searchItemsQueue.Enqueue(ColorToString(Globals.User_Settings.Color00));
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");
                searchItemsQueue.Enqueue("Reserved");   // End Colors

                searchItemsQueue.Enqueue(SearchSettings.FrmColorReplaceMode.ToString());
                searchItemsQueue.Enqueue(SearchSettings.ChkMatchCase.ToString());
                searchItemsQueue.Enqueue(SearchSettings.ChkWordOnly.ToString());
                searchItemsQueue.Enqueue("Reserved");

                for (int i = 1; i < 11; i++)
                {
                    searchItemsQueue.Enqueue(SearchSettings.GetText(i, true));  // True for Find text
                    searchItemsQueue.Enqueue(SearchSettings.GetText(i, false)); // False for Replace text
                }

                // unload all settings into one resulting record.
                while (searchItemsQueue.Count > 0)
                {
                    fileContents = fileContents + searchItemsQueue.Dequeue() + Environment.NewLine;
                }

                string filePath = AppSettings.Data_Folder + RtfSearchName.Text + AppSettings.FindReplaceFileExtension;
                FileIO.CreateDirectoryIfItDoesNotExist(filePath);
                FileIO.WriteFile(filePath, fileContents);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AppendFormattedText(RichTextBox rtb, string text)
        {
            int start = rtb.TextLength;
            rtb.AppendText(text);
            int end = rtb.TextLength; // now longer by length of appended text

            // Select text that was appended
            rtb.Select(start, end - start);

            #region Apply Formatting

            rtb.SelectionColor = Color.Black;

            //rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Regular);
            rtb.SelectionFont = new System.Drawing.Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
            #endregion

            // Unselect text
            rtb.SelectionLength = 0;
        }

        private void GetTimeLimit()
        {
            Globals.User_Settings.SearchTimeLimit = Convert.ToInt32(this.TxtTimeIndicator.Text);
        }

        private void StartSearch(FrmMain frmMain)
        {
            SearchSettings.FrmColorReplaceMode = this.RbEditColor.Checked;
            // Bring FrmMain to the foreground
            frmMain.BringToFront();
            frmMain.Activate();

            // Run the search on FrmMain
            frmMain.RunInFrmMain();
        }

        private void UpdateSearch()
        {
            SearchSettings.SetText(1, true, RtfFind1.Text);
            SearchSettings.SetText(1, false, RtfReplace1.Text);
            SearchSettings.SetText(2, true, RtfFind2.Text);
            SearchSettings.SetText(2, false, RtfReplace2.Text);
            SearchSettings.SetText(3, true, RtfFind3.Text);
            SearchSettings.SetText(3, false, RtfReplace3.Text);
            SearchSettings.SetText(4, true, RtfFind4.Text);
            SearchSettings.SetText(4, false, RtfReplace4.Text);
            SearchSettings.SetText(5, true, RtfFind5.Text);
            SearchSettings.SetText(5, false, RtfReplace5.Text);
            SearchSettings.SetText(6, true, RtfFind6.Text);
            SearchSettings.SetText(6, false, RtfReplace6.Text);
            SearchSettings.SetText(7, true, RtfFind7.Text);
            SearchSettings.SetText(7, false, RtfReplace7.Text);
            SearchSettings.SetText(8, true, RtfFind8.Text);
            SearchSettings.SetText(8, false, RtfReplace8.Text);
            SearchSettings.SetText(9, true, RtfFind9.Text);
            SearchSettings.SetText(9, false, RtfReplace9.Text);
            SearchSettings.SetText(10, true, RtfFind10.Text);
            SearchSettings.SetText(10, false, RtfReplace10.Text);

        }

        private void DisplayBoundaryMarkerCount(int count)
        {
            btnReplace_All.BackColor = Color.Wheat;
            btnSuffixReplace.BackColor = Color.Wheat;
            btnPrefixReplace.BackColor = Color.Wheat;
            lblBoundaryMarkerCount.ForeColor = Color.Gold;
            lblBoundaryMarkerCount.Text = "Boundary Markers = " + count.ToString();
            if (count > 0)
            {
                lblBoundaryMarkerCount.ForeColor = Color.Crimson;
                btnReplace_All.BackColor = Color.Crimson;
                btnSuffixReplace.BackColor = Color.Crimson;
                btnPrefixReplace.BackColor = Color.Crimson;
            }
        }

        private void RbTextReplace_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // This little section may or may not be necessary for this proceedure,
                // two identically named proceedures were listed, this part is one of them
                //SetRtfFindsBackColorToNormal();
                if (RbEditColor.Checked)
                { // COLOR REPLACE
                    RbTextReplace.Checked = false;
                    //Color backColor = Color.FromArgb(200, 190, 200);  //  200, 190, 200

                    RtfFind1.ForeColor = ColorManager.GetColor("C1");  // Globals.User_Settings.Color01;
                    RtfFind2.ForeColor = ColorManager.GetColor("C2");
                    RtfFind3.ForeColor = ColorManager.GetColor("C3");
                    RtfFind4.ForeColor = ColorManager.GetColor("C4");
                    RtfFind5.ForeColor = ColorManager.GetColor("C5");
                    RtfFind6.ForeColor = ColorManager.GetColor("C6");
                    RtfFind7.ForeColor = ColorManager.GetColor("C7");
                    RtfFind8.ForeColor = ColorManager.GetColor("C8");
                    RtfFind9.ForeColor = ColorManager.GetColor("C9");
                    RtfFind10.ForeColor = ColorManager.GetColor("C10");

                    for (int i = 1; i <= 10; i++)
                    {
                        var labelControl = this.Controls[$"lblReplace{i:00}"];

                        // Check if the control exists
                        if (labelControl != null)
                        {
                            labelControl.Text = "Note:"; // Use the labelControl variable directly
                        }
                        else
                        {
                            MessageBox.Show($"Control lblReplace{i:00} not found.");
                        }
                    }
                    RbEditColor.PerformClick();
                }
                else
                {   // TEXT REPLACE
                    RbEditColor.Checked = false;

                    for (int i = 1; i <= 10; i++)
                    {
                        if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind && this.Controls[$"RtfReplace{i:00}"] is RichTextBox rtfReplace)
                        {
                            rtfFind.ForeColor = Color.Black;
                            rtfReplace.ForeColor = Color.Black;
                        }
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        if (this.Controls[$"lblReplace{i:00}"] is Label lblReplace)
                        {
                            lblReplace.Text = "Replace";
                        }
                    }
                    RbTextReplace.PerformClick();
                }
                this.BackColor = Color.FromArgb(200, 200, 200);
                Globals.SearchChanged = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string DrawAnArrowOnlyIfTextBoxHasContent(int n, int rlen, int max)
        {
            try
            {
                string arrow = "";
                if (n > 0 & rlen > 0)
                { // draw arrow only if both textboxes have content
                    string point = "--» ";
                    if (n == max) { arrow = point; } // arrow = point if findtxt is already a max length
                    while (n != max)
                    {            // max is the longest string of every textbox 0 - 7
                        arrow = "-" + point;    // make all separation distances similar by adjusting arrow length
                        n++;
                    }
                    arrow = " " + arrow; // add a space in front of arrow beginning
                    return arrow;
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        #endregion // GeneralFunctions


        #region Events

        private void FrmColor_MouseDown(object sender, MouseEventArgs e)
        {
            formLoading = false;
        }

        private void RtfSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard shortcuts using this Class, which makes them all behave the same.
            if (sender is RichTextBox currentRTB)
            {
                KeyboardShortcuts.HandleControlKeys(currentRTB, e);
            }
        }

        private void RtfSearchName_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = false;
        }

        private void RtfSearchName_Leave(object sender, EventArgs e)
        {
            var originalFont = RtfSearchName.Font;

            // Create a new font with the same settings but with bold style
            var boldFont = new Font(originalFont, FontStyle.Bold);

            // Set BackColor and KeyPreview back to normal
            RtfSearchName.SelectAll();
            RtfSearchName.SelectionBackColor = Globals.User_Settings.RTBMainBackColor;
            RtfSearchName.SelectionFont = boldFont;
            RtfSearchName.SelectionLength = 0;
            this.KeyPreview = true;
        }

        private void TxtTimeIndicator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmColor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RbEditColor.Checked = SearchSettings.FrmColorReplaceMode;
                RbTextReplace.Checked = !SearchSettings.FrmColorReplaceMode;
            }
        }

        private void FrmColor_MouseLeave(object sender, EventArgs e)
        {
            AppSettings.ToolTip1 = "            " + this.RtfReplace1.Text;
            AppSettings.ToolTip2 = "            " + this.RtfReplace2.Text;
            AppSettings.ToolTip3 = "            " + this.RtfReplace3.Text;
            AppSettings.ToolTip4 = "            " + this.RtfReplace4.Text;
            AppSettings.ToolTip5 = "            " + this.RtfReplace5.Text;
            AppSettings.ToolTip6 = "            " + this.RtfReplace6.Text;
            AppSettings.ToolTip7 = "            " + this.RtfReplace7.Text;
            AppSettings.ToolTip8 = "            " + this.RtfReplace8.Text;
            AppSettings.ToolTip9 = "            " + this.RtfReplace9.Text;
            AppSettings.ToolTip10 = "            " + this.RtfReplace10.Text;
        }

        private void RtfSearchName_MouseDown(object sender, MouseEventArgs e)
        {
            // Capture the starting position of the mouse cursor
            if (sender is RichTextBox currentRTB)
            {
                currentRTB.Focus();
                AppSettings.SelStart = currentRTB.GetCharIndexFromPosition(e.Location);
            }
            //Globals.SelStart = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);
        }

        private void FrmColor_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardShortcuts.ShowKeyboardShortcutsPopWindowIfNeeded();
        }

        // foreColor is the richtextbox forecolor desired
        private void PreventFontChange(RichTextBox RTB, Color foreColor)
        {
            //int length = RTB.SelectionStart + Globals.current_RTB_withFocus.SelectedText.Length;
            int length = RTB.SelectionStart + RTB.SelectedText.Length; // for end cursor position
            RTB.SelectAll();
            if (!RbTextReplace.Checked)
            {
                RTB.ForeColor = foreColor;
            }
            else
            {
                RTB.ForeColor = Color.Black;
            }
            RTB.SelectionBackColor = Color.FromArgb(230, 230, 230);
            RTB.SelectionFont = new Font("Times New Roman", 16, FontStyle.Bold);
            RTB.SelectionStart = length;  // end cursor position
            RTB.SelectionLength = 0;
        }

        private void RtfSearchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            PreventFontChange(RtfSearchName, Color.Black);
        }

        private void RtfSearchName_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.Current_RTB_withFocus = RtfSearchName;
            Globals.Current_RTB_withFocus.Focus();

            // Calculate the ending position of the selection
            int selEnd = Globals.Current_RTB_withFocus.GetCharIndexFromPosition(e.Location);

            // If the starting position and the ending position are the same, return without selecting anything
            if (selEnd == AppSettings.SelStart) { return; }

            // Determine the selection start and length based on the relative positions of the starting and ending points
            int selectionStart = Math.Min(AppSettings.SelStart, selEnd);
            int selectionLength = Math.Abs(AppSettings.SelStart - selEnd);

            // Set the selection start and length
            Globals.Current_RTB_withFocus.SelectionStart = selectionStart;
            Globals.Current_RTB_withFocus.SelectionLength = selectionLength;

            // Set the focus back to the control to keep the selection highlighted
            Globals.Current_RTB_withFocus.Focus();
        }

        // LOCATION    → →  → →   FRMCOLOR CLOSING
        private void FrmColor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            MyUserSettings myUserSettings = new();
            myUserSettings.FrmColor_ReplaceMode = SearchSettings.FrmColorReplaceMode;
            SearchSettings.ChkMatchCase = this.ChkMatchCase.Checked;
            SearchSettings.ChkWordOnly = this.ChkWordOnly.Checked;
            SearchSettings.Reserved = false;

            for (int i = 1; i < 11; i++)
            {
                SearchSettings.GetText(i, true);  // True for Find text
                SearchSettings.GetText(i, false); // False for Replace text
            }

            // Ensure the position and size are valid and saved
            ScreenUtility.HandleLocationChanged(this, location => Globals.User_Settings.FrmColorLocation = location);

            Globals.User_Settings.SearchTimeLimit = Convert.ToInt32(this.TxtTimeIndicator.Text);
        }

        private void RbEditColor_CheckedChanged(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(200, 190, 200);
            SearchSettings.FrmColorReplaceMode = RbEditColor.Checked;
        }

        private void FrmColor_MouseEnter(object sender, EventArgs e)
        {
            FrmMain frmMain = FrmMain.Instance;

            if (RbEditColor.Checked)
            {

                for (int i = 1; i <= 10; i++)
                {
                    if (this.Controls[$"FrmColorBtnC{i}"] is Button colorButton)
                    {
                        colorButton.BackColor = ColorManager.GetColor($"C{i}");
                    }
                }

                for (int i = 1; i <= 10; i++)
                {
                    if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                    {
                        rtfFind.ForeColor = ColorManager.GetColor($"C{i}");
                    }
                }

            }

            frmMain?.CheckForOddNumberOfBoundryMarkers();
            frmMain?.GetBoundaryMarkerInformation();
            DisplayBoundaryMarkerCount(Globals.BMCount);   // Globals.BMCount = searchBM.boundryMarkerCount;  searchBM is a class in FrmMain
            Application.DoEvents();
        }

        #endregion //  Events


        #region Button__Clicks

        private void BtnGetSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the utility method to get existing or create FrmGetSearches
                FrmGetSearches frmGetSearches = GetOrCreateFrmGetSearches();

                var FindReplaceTitlesList = new List<string>();

                frmGetSearches.ListOfSearchTitles.Items.Clear();
                FindReplaceTitlesList = GeneralFns.GetListOfFilesInDirectory(AppSettings.Data_Folder, AppSettings.FindReplaceFileExtension);
                if (FindReplaceTitlesList.Count < 1)
                {
                    MessageBox.Show("No searches currently exist.", "No Searches", MessageBoxButtons.OK);
                    return;
                }
                foreach (string item in FindReplaceTitlesList)
                {
                    if (!frmGetSearches.ListOfSearchTitles.Items.Contains(item))
                    {
                        frmGetSearches.ListOfSearchTitles.Items.Add(item);
                    }
                }
                frmGetSearches.cboSearchFilter.Text = Globals.User_Settings.CboSearchFilterSelection;
                frmGetSearches.Filter_QuizListBox();
                frmGetSearches.BringToFront();
                frmGetSearches.Refresh();
                if (!frmGetSearches.Visible)
                {
                    frmGetSearches.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchSettings.SearchName = RtfSearchName.Text;
                SearchSettings.FrmColorReplaceMode = RbEditColor.Checked;
                SearchSettings.Reserved = false;

                for (int i = 1; i < 11; i++)
                {
                    SearchSettings.GetText(i, true);
                    SearchSettings.GetText(i, false);
                }

                SaveSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReplace_All_Click(object sender, EventArgs e)
        {
            //Globals.search.Clear();
            AppSettings.AffixType = Affix.All;
            SetSearchType();  // Text replace or color replace
            UpdateSearch();
            GoToMainToCompleteFrmColorSearch();
            TxtTimeIndicator.Text = Globals.User_Settings.SearchTimeLimit.ToString();
        }

        private void BtnReplacePrefix_Click(object sender, EventArgs e)
        {
            //Globals.search.Clear();
            AppSettings.AffixType = Affix.Prefix;
            SetSearchType();  // Text replace or color replace
            UpdateSearch();
            GoToMainToCompleteFrmColorSearch();
            TxtTimeIndicator.Text = Globals.User_Settings.SearchTimeLimit.ToString();
        }

        private void BtnReplaceSuffix_Click(object sender, EventArgs e)
        {
            //Globals.search.Clear();
            AppSettings.AffixType = Affix.Suffix;
            SetSearchType();  // Text replace or color replace
            UpdateSearch();
            GoToMainToCompleteFrmColorSearch();
            TxtTimeIndicator.Text = Globals.User_Settings.SearchTimeLimit.ToString();
        }

        // Sets whether this is a text replace or a color replace
        private void SetSearchType()
        {
            AppSettings.SearchMode = Mode.Text;
            if (RbEditColor.Checked)
            {
                AppSettings.SearchMode = Mode.Color;
                SearchSettings.FrmColorReplaceMode = RbEditColor.Checked;
            }
        }

        private void BtnDefaultColors_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset to default colors?", "Confirm Reset",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FrmColorBtnC1.BackColor = ColorManager.GetDefaultColor("C1");
                FrmColorBtnC2.BackColor = ColorManager.GetDefaultColor("C2");
                FrmColorBtnC3.BackColor = ColorManager.GetDefaultColor("C3");
                FrmColorBtnC4.BackColor = ColorManager.GetDefaultColor("C4");
                FrmColorBtnC5.BackColor = ColorManager.GetDefaultColor("C5");
                FrmColorBtnC6.BackColor = ColorManager.GetDefaultColor("C6");
                FrmColorBtnC7.BackColor = ColorManager.GetDefaultColor("C7");
                FrmColorBtnC8.BackColor = ColorManager.GetDefaultColor("C8");
                FrmColorBtnC9.BackColor = ColorManager.GetDefaultColor("C9");
                FrmColorBtnC10.BackColor = ColorManager.GetDefaultColor("C10");
                this.Refresh();

                GoToMainToSetDefaultColors();
            }
            else
            {
                return;
            }
        }

        private void BtnImportSearch_Click(object sender, EventArgs e)
        {
            GeneralFns.SelectAndImportFiles(FileExtensionType.Fdta);
        }

        private void BtnSearchFolder_Click(object sender, EventArgs e)
        {
            string folderPath = @AppSettings.Data_Folder;
            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                string header = "Directory not available.";
                string message = "This directory has not yet been created and does not exist.";
                GeneralFns.CustomMessageBox(header, message);
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = FrmMain.Instance;
                AppSettings.currentCursorPosition = frmMain.RTBMain.SelectionStart;

                // This is a kludge, necessary, because Windows stores the undos 
                // of the rich textbox even when undo is deactivated.
                if (AppSettings.UndoQueue.Count > 0)
                {  // Allows for multi-step undo
                    AppSettings.UndoCount = AppSettings.UndoQueue.Dequeue();
                }
                while (AppSettings.UndoCount > 0)
                {
                    frmMain?.UndoRTBMainAction();

                    AppSettings.UndoCount--;
                }
                // This catches the case where, if for any reason the count 
                // has failed, undo can still be done
                if (AppSettings.UndoCount < 1)
                {
                    frmMain?.SimpleUndo();

                }

                if (frmMain != null)
                {
                    frmMain.RTBMain.DeselectAll();
                    frmMain.RTBMain.Focus();
                    frmMain.RTBMain.SelectionStart = AppSettings.currentCursorPosition;
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false;
                SearchSettings.FrmColorReplaceMode = this.RbEditColor.Checked;
                SearchSettings.ChkMatchCase = this.ChkMatchCase.Checked;
                SearchSettings.ChkWordOnly = this.ChkWordOnly.Checked;
                SearchSettings.Reserved = false;

                for (int i = 1; i < 11; i++)
                {
                    SearchSettings.GetText(i, true);  // True for Find text
                    SearchSettings.GetText(i, false); // False for Replace text
                }

                var pt = new Point(this.Left, this.Top);
                Globals.User_Settings.FrmColorLocation = pt;

                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            RtfSearchName.Text = "";
        }

        private void ChkMatchCase_Click(object sender, EventArgs e)
        {
            Globals.SearchChanged = true;
            SearchSettings.ChkMatchCase = ChkMatchCase.Checked;
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            // return all textboxes to grey
            Color globalBackColor = Globals.User_Settings.RTBMainBackColor;

            RtfSearchName.Text = String.Empty;
            for (int i = 1; i <= 10; i++)
            {
                if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                {
                    rtfFind.BackColor = globalBackColor;
                    rtfFind.Text = "";
                }
                if (this.Controls[$"RtfReplace{i}"] is RichTextBox rtfReplace)
                {
                    rtfReplace.BackColor = globalBackColor;
                    rtfReplace.Text = "";
                }
            }
            SearchSettings.ClearFindTexts();
            SearchSettings.ClearReplaceTexts();
            SearchSettings.ClearToolTipsTexts();
        }



        private void BtnPasteInto_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = FrmMain.Instance;
            int max = 0;
            var RTB = new RichTextBox();
            var rtf = new RichTextBox();

            try
            {
                // Get the length of the longest string in rtfFind
                var maxLengthF = Enumerable.Range(1, 10).Select(i => (this.Controls[$"RtfFind{i}"] as RichTextBox)?.Text.Length ?? 0).ToArray();
                rtf.Text = " ";
                max = maxLengthF.Max();

                // Iterate through each rtfFind and rtfReplace pair
                for (int i = 1; i <= 10; i++)
                {
                    var findControl = this.Controls[$"RtfFind{i}"] as RichTextBox;
                    var replaceControl = this.Controls[$"RtfReplace{i}"] as RichTextBox;

                    if (findControl != null && replaceControl != null)
                    {
                        int n = findControl.Text.Length;
                        int rlen = replaceControl.Text.Length;
                        string arrow = DrawAnArrowOnlyIfTextBoxHasContent(n, rlen, max);

                        if (n > 0 || rlen > 0)
                        { // If one of the textboxes has content, process it
                            RTB.Rtf = findControl.Rtf;
                            AppendFormattedText(RTB, arrow); // Color is black, that is what I want
                            AppendFormattedText(RTB, replaceControl.Text);
                            RTB.SelectAll();
                            RTB.SelectionFont = new Font("Times New Roman", Globals.BtnPasteIntoFontSize, FontStyle.Regular);
                            if (frmMain != null)
                            {
                                frmMain.RTBMain.SelectedRtf = RTB.Rtf + Environment.NewLine;
                            }
                        }
                    }
                }

                // Reset backcolor, so whole text has the same backcolor
                var d = new ColorDialog
                {
                    AnyColor = true,
                    SolidColorOnly = false,
                    Color = Globals.User_Settings.RTBMainBackColor
                };

                if (frmMain != null)
                {
                    var _with17 = frmMain.RTBMain;
                    _with17.SelectAll();
                    _with17.BackColor = d.Color;
                    _with17.SelectionBackColor = d.Color;
                    _with17.SelectionLength = 0;
                    _with17.SelectionStart = Convert.ToInt32(frmMain.RTBMain.SelectionStart);
                    d.Dispose();
                    frmMain.Activate();
                    frmMain.BtnReturn.PerformClick();
                    frmMain.RTBMain.Focus();
                    frmMain.RTBMain.SelectionLength = 0;
                    frmMain.RTBMain.Select();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion  //  Button__Clicks


        #region RadioButton_Or_CheckBox__Clicks

        private void ChkWordOnly_Click(object sender, EventArgs e)
        {
            //SetRtfFindsBackColorToNormal();
            Globals.SearchChanged = true;
            SearchSettings.ChkWordOnly = ChkWordOnly.Checked;
        }

        // For Changing font size
        // Font currentFont = rtfFind01.SelectionFont;
        // rtfFind01.SelectionFont = new Font(currentFont.FontFamily, 26, FontStyle.Regular);


        private void RbEditColor_Click(object sender, EventArgs e)
        {
            SetColorReplaceMode();
        }

        private void RbTextReplace_Click(object sender, EventArgs e)
        {
            MyUserSettings myUserSettings = new();
            myUserSettings.FrmColor_ReplaceMode = SearchSettings.FrmColorReplaceMode = RbEditColor.Checked;
            SetTextReplaceMode();
        }

        private void SetColorReplaceMode()
        {
            try
            {
                SearchSettings.FrmColorReplaceMode = RbEditColor.Checked = true;
                RbTextReplace.Checked = false;

                if (formLoading == false)
                {
                    SearchSettings.ChkMatchCase = this.ChkMatchCase.Checked;
                    SearchSettings.ChkWordOnly = this.ChkWordOnly.Checked;
                }

                this.ChkMatchCase.Checked = SearchSettings.ChkMatchCase;
                this.ChkWordOnly.Checked = SearchSettings.ChkWordOnly;

                this.btnGetSearch.Visible = true;
                this.btnSaveSearch.Visible = true;
                btnPasteInto.Visible = true;

                Label[] lblReplace = Enumerable.Range(1, 10).Select(i => this.Controls[$"lblReplace{i}"] as Label).Where(label => label != null).ToArray()!;
                for (int y = 0; y < lblReplace.Length; y++)
                {
                    lblReplace[y].Text = "Note: ";
                }

                for (int y = 0; y < lblReplace.Length; y++)
                {
                    if (lblReplace[y] != null)
                    {
                        lblReplace[y].Text = "Note: ";
                    }
                }
                Button[] btnColor = Enumerable.Range(1, 10).Select(i => this.Controls[$"FrmColorBtnC{i}"] as Button).Where(button => button != null).Select(button => button!).ToArray();

                int x;
                for (x = 0; x < btnColor.Length; x++)
                {
                    btnColor[x].Visible = true;

                    // Clear all textboxes
                    for (int i = 1; i <= 10; i++)
                    {
                        SearchSettings.GetText(i, true); // true means it is find
                        SearchSettings.GetText(i, false); // false means it is Replace
                        SearchSettings.GetToolTips(i);
                    }
                }

                Application.DoEvents();

                // set frmColor to open on edit Color OPTION 0
                btnPasteInto.Visible = true;
                for (int i = 1; i <= 10; i++)
                {
                    if (this.Controls[$"lblPipe{i:00}"] is Label lblPipe)
                    {
                        lblPipe.Visible = true;
                    }
                }

                Globals.SearchChanged = false;

                // set button backcolors to show in color edit mode
                Color backColor = Color.FromArgb(228, 192, 255);

                this.BackColor = Color.FromArgb(200, 190, 200);
                this.Refresh();
                Cursor.Current = Cursors.Default;
                RtfFind1.Select();
                GetAndSetRtfFindForeColors();
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetTextReplaceMode()
        {
            try
            {
                SearchSettings.FrmColorReplaceMode = RbEditColor.Checked = false;
                RbTextReplace.Checked = true;

                Button[] btnColor = Enumerable.Range(1, 10).Select(i => this.Controls[$"FrmColorBtnC{i}"] as Button).Where(button => button != null).Select(button => button!).ToArray();

                foreach (System.Windows.Forms.Button buttonX in btnColor)
                {
                    buttonX.Visible = false;
                }

                for (int i = 1; i < 11; i++)
                {
                    SearchSettings.SetForeColor(i, true, Color.Black);
                }
                RtfFind1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion //  RadioButton_Or_CheckBox__Clicks


        #region Hand Control Over To FrmMain
        private void GoToMainToCompleteFrmColorSearch()
        {
            GetTimeLimit();
            FrmMain frmMain = FrmMain.Instance;

            if (frmMain == null)
            {
                MessageBox.Show("FrmMain instance is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Start the search in FrmMain
            if (frmMain.InvokeRequired)
            {
                frmMain.Invoke(new Action(() => StartSearch(frmMain)));
            }
            else
            {
                StartSearch(frmMain);
            }
        }


        private void GoToMainToSetDefaultColors()
        {
            GetTimeLimit();
            FrmMain frmMain = FrmMain.Instance;

            // Start the search in FrmMain
            if (frmMain.InvokeRequired)
            {
                frmMain.Invoke(new Action(() => SetDefaultColors(frmMain)));
            }
            else
            {
                SetDefaultColors(frmMain);
            }
        }

        private void SetDefaultColors(FrmMain frmMain)
        {
            // Bring FrmMain to the foreground
            frmMain.BringToFront();
            frmMain.Activate();

            // Run this in frmMain
            frmMain.FrmMainColorBtn1.BackColor = ColorManager.GetColor("C1");
            frmMain.FrmMainColorBtn2.BackColor = ColorManager.GetColor("C2");
            frmMain.FrmMainColorBtn3.BackColor = ColorManager.GetColor("C3");
            frmMain.FrmMainColorBtn4.BackColor = ColorManager.GetColor("C4");
            frmMain.FrmMainColorBtn5.BackColor = ColorManager.GetColor("C5");
            frmMain.FrmMainColorBtn6.BackColor = ColorManager.GetColor("C6");
            frmMain.FrmMainColorBtn7.BackColor = ColorManager.GetColor("C7");
            frmMain.FrmMainColorBtn8.BackColor = ColorManager.GetColor("C8");
            frmMain.FrmMainColorBtn9.BackColor = ColorManager.GetColor("C9");
            frmMain.FrmMainColorBtn10.BackColor = ColorManager.GetColor("C10");
            frmMain.Refresh();
        }

        private void ChkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            SearchSettings.ChkMatchCase = ChkMatchCase.Checked;
        }

        private void ChkWordOnly_CheckedChanged(object sender, EventArgs e)
        {
            SearchSettings.ChkWordOnly = ChkWordOnly.Checked;
        }


        private void TxtTimeIndicator_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (TxtTimeIndicator.Text != null && TxtTimeIndicator.Text.Length > 0)
                {
                    if (int.TryParse(TxtTimeIndicator.Text, out int result))
                    {
                        Globals.User_Settings.SearchTimeLimit = Convert.ToInt32(TxtTimeIndicator.Text);
                    }
                }
            }
            catch (Exception)
            {
                return; // Return without any effect if an error occurs
            }
        }
        #endregion  // HandControlOverToFrmMain



        #region Recent Modifications

        private void BtnClearFinds_Click(object sender, EventArgs e)
        {
            Color globalBackColor = Globals.User_Settings.RTBMainBackColor;

            for (int i = 1; i <= 10; i++)
            {
                if (this.Controls[$"RtfFind{i}"] is RichTextBox rtfFind)
                {
                    rtfFind.BackColor = globalBackColor;
                    rtfFind.Text = "";
                }
            }
            SearchSettings.ClearFindTexts();
        }

        private void BtnClearReplace_Click(object sender, EventArgs e)
        {
            Color globalBackColor = Globals.User_Settings.RTBMainBackColor;

            for (int i = 1; i <= 10; i++)
            {
                if (this.Controls[$"RtfReplace{i}"] is RichTextBox rtfReplace)
                {
                    rtfReplace.BackColor = globalBackColor;
                    rtfReplace.Text = "";
                }
            }
            SearchSettings.ClearReplaceTexts();
        }

        private void RtfFind1_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(1, true, RtfFind1.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind2_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(2, true, RtfFind2.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind3_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(3, true, RtfFind3.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind4_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(4, true, RtfFind4.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind5_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(5, true, RtfFind5.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind6_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(6, true, RtfFind6.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind7_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(7, true, RtfFind7.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind8_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(8, true, RtfFind8.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind9_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(9, true, RtfFind9.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfFind10_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(10, true, RtfFind10.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace1_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(1, false, RtfReplace1.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace2_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(2, false, RtfReplace2.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace3_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(3, false, RtfReplace3.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace4_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(4, false, RtfReplace4.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace5_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(5, false, RtfReplace5.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace6_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(6, false, RtfReplace6.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace7_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(7, false, RtfReplace7.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace8_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(8, false, RtfReplace8.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace9_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(9, false, RtfReplace9.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }

        private void RtfReplace10_TextChanged(object sender, EventArgs e)
        {
            SearchSettings.SetText(10, false, RtfReplace10.Text);
            Globals.BoolFrmColorSearchSaved = false;
            Globals.SearchChanged = true;
        }



        private void RtfFind1_MouseEnter(object sender, EventArgs e)
        {
            RtfFind1.Cursor = Cursors.Default;
        }

        private void RtfSearchName_MouseMove(object sender, MouseEventArgs e)
        {
            RtfSearchName.Cursor = Cursors.Default;
        }

        private void RtfFind1_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind1.Cursor = Cursors.Default;
        }

        private void RtfReplace1_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace1.Cursor = Cursors.Default;
        }

        private void RtfFind2_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind2.Cursor = Cursors.Default;
        }

        private void RtfReplace2_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace2.Cursor = Cursors.Default;
        }

        private void RtfFind3_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind3.Cursor = Cursors.Default;
        }

        private void RtfReplace3_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace3.Cursor = Cursors.Default;
        }

        private void RtfFind4_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind4.Cursor = Cursors.Default;
        }

        private void RtfReplace4_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace4.Cursor = Cursors.Default;
        }

        private void RtfFind5_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind5.Cursor = Cursors.Default;
        }

        private void RtfReplace5_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace5.Cursor = Cursors.Default;
        }

        private void RtfFind6_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind6.Cursor = Cursors.Default;
        }

        private void RtfReplace6_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace6.Cursor = Cursors.Default;
        }

        private void RtfFind7_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind7.Cursor = Cursors.Default;
        }

        private void RtfReplace7_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace7.Cursor = Cursors.Default;
        }

        private void RtfFind8_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind8.Cursor = Cursors.Default;
        }

        private void RtfReplace8_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace8.Cursor = Cursors.Default;
        }

        private void RtfFind9_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind9.Cursor = Cursors.Default;
        }

        private void RtfReplace9_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace9.Cursor = Cursors.Default;
        }

        private void RtfFind10_MouseMove(object sender, MouseEventArgs e)
        {
            RtfFind10.Cursor = Cursors.Default;
        }

        private void RtfReplace10_MouseMove(object sender, MouseEventArgs e)
        {
            RtfReplace10.Cursor = Cursors.Default;
        }

        #endregion // Recent Modifications











        //
    }
}


