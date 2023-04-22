using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{

    //  French undo is CONTROL W,   German undo is CONTROL Y.
    internal class KeyboardShortcuts
    {
        string messageEnterAVowelFirst = "Enter a vowel first.";
        string messageForGreek = "Enter a vowel first, in Greek Language mode.";


        public void HandleShiftKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                // FrmMain frmMain = new FrmMain();
            }
        }

        // START CONTROL BASED KEYBOARD SHORTCUTS $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        public void HandleControlKeys(object sender, KeyEventArgs e)
        {
            Control activeControl = Form.ActiveForm.ActiveControl;
            if (activeControl.GetType() == typeof(RichTextBox))
            {
                if (e.Control)
                {
                    if (e.KeyCode == Keys.B) // Bold
                    {
                        int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
                        int selStart = Globals.Current_RTB_withFocus.SelectionStart;

                        if ((Globals.Current_RTB_withFocus.SelectionFont != null))
                        {
                            Globals.Current_RTB_withFocus.SelectionFont =
                                new Font(Globals.Current_RTB_withFocus.SelectionFont, (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Bold));
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    if (e.KeyCode == Keys.I)  // Italics
                    {
                        int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
                        int selStart = Globals.Current_RTB_withFocus.SelectionStart;

                        if ((Globals.Current_RTB_withFocus.SelectionFont != null))
                        {
                            Globals.Current_RTB_withFocus.SelectionFont =
                                new Font(Globals.Current_RTB_withFocus.SelectionFont, (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Italic));
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    if (e.KeyCode == Keys.U)  // Underline
                    {
                        int selectedLen = Globals.Current_RTB_withFocus.SelectedText.Length;
                        int selStart = Globals.Current_RTB_withFocus.SelectionStart;

                        if ((Globals.Current_RTB_withFocus.SelectionFont != null))
                        {
                            Globals.Current_RTB_withFocus.SelectionFont =
                                new Font(Globals.Current_RTB_withFocus.SelectionFont, (Globals.Current_RTB_withFocus.SelectionFont.Style ^ FontStyle.Underline));
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    if (e.KeyCode == Keys.C)  // CONTROL C   Copy
                    {
                        if (Globals.Current_RTB_withFocus.SelectionLength > 0)
                            Globals.Current_RTB_withFocus.Copy();

                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    if (e.KeyCode == Keys.X)  // CONTROL X   Cut
                    {
                        if (Globals.Current_RTB_withFocus.SelectionLength > 0)
                            Globals.Current_RTB_withFocus.Cut();

                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    if (e.KeyCode == Keys.V)  // CONTROL V   Paste
                    {
                        if ((Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true) || (Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf) == true))
                        {
                            if (Globals.Current_RTB_withFocus.SelectionLength > 0)
                            {
                                Globals.Current_RTB_withFocus.SelectedText = "";
                                Globals.Current_RTB_withFocus.SelectionStart = Globals.Current_RTB_withFocus.SelectionStart + Globals.Current_RTB_withFocus.SelectionLength;
                            }
                            Globals.Current_RTB_withFocus.Paste();
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }
                }
            }
        }  // END CONTROL BASED KEYBOARD SHORTCUTS $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$





        // Functions required by HandleALTKeys ################################################
        private bool CheckLetter(string letter, string message)
        {
            if (letter.Length < 1)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (char.IsLetter(Convert.ToChar(letter)))
            {
                return true;
            }
            return false;
        }

        private string GetAndSelectLetterBehindCursor()
        {
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            string selectedText = "";
            if (selStart > 0)
            {
                Globals.Current_RTB_withFocus.Select(selStart - 1, 1);
                selectedText = Globals.Current_RTB_withFocus.SelectedText;
            }
            string send = Regex.Replace(selectedText, @"\r\n?|\n", "");
            return send;
        }

        public void SetAcuteAccent()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToAcute(letter);
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetGraveAccent()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToGrave(letter);
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetUmlaut()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText =
                GetSpecialCharacters.ChangeVowelToUmlaut(letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetCircumflex()
        {
            string letter = GetAndSelectLetterBehindCursor();
            int selStart = Globals.Current_RTB_withFocus.SelectionStart;
            string selectedText = "";
            Globals.Current_RTB_withFocus.Select(selStart, 1);
            selectedText = Globals.Current_RTB_withFocus.SelectedText;
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText =
                GetSpecialCharacters.ChangeVowelToCircumflex(letter);
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetMacron()
        { // For long vowel ā    From btnSpChar4.PerformClick();
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToLong(letter);
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetShort()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageEnterAVowelFirst))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToShort(letter);
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }
        // END Functions required by HandleALTKeys ################################################

        // Non-Language specific keyboard shortcuts 
        // START ALT BASED KEYBOARD SHORTCUTS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void HandleAltKeys(object sender, KeyEventArgs e)
        {
            Control activeControl = Form.ActiveForm.ActiveControl;
            if (activeControl.GetType() == typeof(RichTextBox))
            {
                if (e.Alt)
                {

                    //// ALT + ----> ARROW   for Accute Accent  á
                    if (e.KeyCode == Keys.Right)
                    {
                        SetAcuteAccent();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }
                    // ALT + LEFT ARROW for Grave accent à
                    if (e.KeyCode == Keys.Left)
                    {
                        SetGraveAccent();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    // ALT -  for Macron ᾱ    
                    if (e.KeyCode == Keys.OemMinus)
                    {
                        SetMacron();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }
                    // ALT = for Short ᾰ
                    if (e.KeyCode == Keys.Oemplus)
                    {
                        SetShort();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    // ALT ~   for circumflex 
                    if (e.KeyCode == Keys.Oemtilde)
                    {
                        SetCircumflex();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    //// ALT ;    Umlaut - This also set Greek Diaresis if in Greek mode
                    if (e.KeyCode == Keys.OemSemicolon)
                    {
                        InputLanguage language = InputLanguage.CurrentInputLanguage;
                        string lang = GetKeyboardShortcuts(language);
                        if (lang == "ell")
                        {
                            SetGreekDiaresis();
                        }
                        else {
                            SetUmlaut();
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    // GREEKS  γγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγ
                    //// ALT ,  Greek Iota
                    if (e.KeyCode == Keys.OemQuotes)
                    {
                        SetGreekIota();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    //// ALT (  Greek Rough
                    if (e.KeyCode == Keys.D9)
                    {
                        SetGreekRough();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    //// ALT + )    Greek Smooth
                    if (e.KeyCode == Keys.D0)
                    {
                        SetGreekSmooth();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }

                    //// ALT + .  Greek Period
                    if (e.KeyCode == Keys.OemPeriod)
                    {
                        Globals.Current_RTB_withFocus.SelectedText = "·";
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Globals.Current_RTB_withFocus.Focus();
                        return;
                    }
                    // END GREEKS  γγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγγ


                }
            }
        }  // END ALT BASED KEYBOARD SHORTCUTS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // GREEK ΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓ
        public void SetGreekIota() // Alt + ,
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageForGreek))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToIotaSubscript(letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        //  for rough mark  ἁ
        public void SetGreekRough()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageForGreek))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToRough(letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        // for smooth mark  ἀ 
        public void SetGreekSmooth()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageForGreek))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToSmooth(letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetGreekCircumflex()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageForGreek))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToTrema(letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        public void SetGreekDiaresis()
        {
            string letter = GetAndSelectLetterBehindCursor();
            if (CheckLetter(letter, messageForGreek))
            {
                Globals.Current_RTB_withFocus.SelectedText = GetSpecialCharacters.ChangeVowelToUmlaut (letter) + "";
            }
            else { return; }
            Globals.Current_RTB_withFocus.Focus();
        }

        // END  GREEK ΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓΓ






        public void HandleFKeys(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)  // F1 Opens a help screen when this key is pressed.
            {
                try
                {
                    WebBrowser wb = new WebBrowser();
                    string url = "https://www.archeuslore.com/tachufind/search.html";
                    // Get Value based on Key
                    wb.Navigate(url);
                    Process.Start(url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (e.KeyCode == Keys.F2) // F2 Process.Start("osk.exe");  // This works!
            {
                Process.Start("osk.exe");  // This works!
            }

            if (e.KeyCode == Keys.F3)// F3 Opens Color window.
            {
                FrmMain frmMain = new FrmMain();
                FrmColor frmColor = new FrmColor(frmMain);
                frmColor.Show();
            }

            if (e.KeyCode == Keys.F4) // F4 Open Quiz window.
            {
                FrmQuiz frmQuiz = new FrmQuiz();
                frmQuiz.Show();
            }

            if (e.KeyCode == Keys.F5) // F5 Open Keyboard Shortcuts window.
            {
                FrmShortcuts frmShortcuts = new FrmShortcuts();
                if (!GeneralFns.IsFormTypeOpen(typeof(FrmShortcuts)))
                {
                    InputLanguage language = InputLanguage.CurrentInputLanguage;
                    frmShortcuts.RTBShortcuts.Rtf = GetKeyboardShortcuts(language);
                    frmShortcuts.Visible = true;
                }
            }

            if (e.KeyCode == Keys.F6) // F6 Return to previous cursor position.
            {

            }

            if (e.KeyCode == Keys.F7)
            {
            }

            if (e.KeyCode == Keys.F8)
            {

            }

            if (e.KeyCode == Keys.F9) // F9  Find reverse from cursor.
            {

            }

            if (e.KeyCode == Keys.F10)
            {

            }

            if (e.KeyCode == Keys.F11) // F11 Insert boundary marker. 
            {

            }

            if (e.KeyCode == Keys.F12)   // F12 Delete boundary markers. 
            {

            }
            // End LOCATION Function keys F1 - F12 %%%%%%%%%%%%%%%%%%%%%%%%
            //string letter = string.Empty;
        }

        private static (int, int) SetSizeBasedOnLanguage(string language) {
            // default size is English, set here:
            int width = 900;
            int height = 640;

            switch (language) 
            {
                case "ell":
                    width = 1000;
                    height = 1000;
                    break;
                case "fra":
                    width = 900;
                    height = 640;
                    break;
                // 920, 550
                case "deu":
                    width = 920;
                    height = 550;
                    break;
                case "ita":
                    width = 920;
                    height = 500;
                    break;
                case "spa":
                    width = 900;
                    height = 850;
                    break;
                case "ara":
                    width = 960;
                    height = 840;
                    break;
                case "ru":
                    width = 960;
                    height = 840;
                    break;
                default:
                    break;
            }
            return (width, height);
        }

        public void ShowKeyboardShortcutsPopWindowIfNeeded()
        {
            if (Globals.ShowFrmKeyboardShorcutsPopWindow == true)
            {
                int width = 1000;
                int height = 1000;

                FrmShortcuts frmShortcuts = new FrmShortcuts();
                // Get the form width height based on the language, so size looks right
                InputLanguage language = InputLanguage.CurrentInputLanguage;
                string languageID = new CultureInfo(language.Culture.LCID).ThreeLetterISOLanguageName;
                (width, height) = SetSizeBasedOnLanguage(languageID);
                frmShortcuts.Size = new Size(width, height);
                if (!GeneralFns.IsFormTypeOpen(typeof(FrmShortcuts))) {
                    frmShortcuts.Visible = true;
                    frmShortcuts.Show();
                    frmShortcuts.BringToFront();
                    KeyboardShortcuts keyboardShortcuts = new KeyboardShortcuts();
                    frmShortcuts.RTBShortcuts.Rtf = keyboardShortcuts.GetKeyboardShortcuts(language);
                    Globals.ShowFrmKeyboardShorcutsPopWindow = false;
                }
            }
        }

        // Functions required by HandleFKeys @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public string GetKeyboardShortcuts(InputLanguage language)
        {
            string languageCode = new CultureInfo(language.Culture.LCID).ThreeLetterISOLanguageName;
            switch (languageCode)
            {
                case "eng":
                    return Globals.EnglishShortcuts;
                case "fra":
                    return Globals.FrenchShortcuts;
                case "deu":
                    return Globals.GermanShortcuts;
                case "ell":
                    return Globals.AncientGreekShortcuts;
                case "ita":
                    return Globals.ItalianShortcuts;
                case "rus":
                    return Globals.RussianShortcuts;
                case "spa":
                    return Globals.SpanishShortcuts;
                case "lat":
                    return Globals.EnglishShortcuts;
                case "ara":
                    return Globals.ArabicShortcuts;
                default:
                    return Globals.EnglishShortcuts;
            }
        }
        // END  Functions required by HandleFKeys @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

 

    }
}
