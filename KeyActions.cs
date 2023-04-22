using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;


namespace Tachufind
{
    class KeyActions
    {
		/*  EXAMPLES:          
		 *  if (Control.ModifierKeys == Keys.Shift)
            if (Control.ModifierKeys == Keys.ControlKey)
            if(e.KeyCode == Keys.Back)
            if (e.KeyCode == KeyActions.Shift)
            if (Keys.Shift == KeyActions.Shift)
		 */

		//  French undo is control w   German undo is control y
		//  old note: case Keys.W:   Undo does not work for French, in French, Ctrl Z = Ctrl W
		//  << and >> are inserted when pressing control, shift, ( and control, shift, ) respectively.
		//  Note that you must release the control and shift buttons in between the first and the second.

		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		// START KEYUP - KEYDOWN
		//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%




		public static string getAndSelectLetterBehindCursor(RichTextBox current_RTB_withFocus)
		{
			//int selStart = this.RTBMain.SelectionStart;
			int selStart = Globals.current_RTB_withFocus.SelectionStart;
			string selectedText = "";
			if (selStart > 0)
			{
				Globals.current_RTB_withFocus.Select(selStart - 1, 1);
				selectedText = Globals.current_RTB_withFocus.SelectedText;
			}
			return selectedText;
		}

		public static void frmsKeyAction(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			InputLanguage cultureInfo = InputLanguage.CurrentInputLanguage;
			int selectedLen = Globals.current_RTB_withFocus.SelectedText.Length;
			int selStart = Globals.current_RTB_withFocus.SelectionStart;
			RichTextBox RTextBox = Globals.current_RTB_withFocus;

			try
			{
				switch (cultureInfo.Culture.EnglishName)
				{
					case "Greek (Greece)":
						handleGreekKeyboardShortcuts(sender, e);
						break;

					default: // English
						executeKeyboardShortcuts(sender, e);
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		// LOCATION KEYBOARD SHORTCUTS
		public static void executeKeyboardShortcuts(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			int selectedLen = Globals.current_RTB_withFocus.SelectedText.Length;
			int selStart = Globals.current_RTB_withFocus.SelectionStart;
			RichTextBox RTextBox = Globals.current_RTB_withFocus;
			string letter = string.Empty;


			// ctrl \ for Grave accent  à    From btnSpChar0.PerformClick();
			if ((e.KeyCode == Keys.Oem5) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToGrave(letter);
					}
				}
			}



			// ctrl / for Accute Accent   á    From btnSpChar1.PerformClick();
			if ((e.KeyCode == Keys.Oem2) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToAcute(letter);
					}
				}

			}


			// ctrl ;  for Umlaut  ä    From btnSpChar2.PerformClick();
			if ((e.KeyCode == Keys.Oem1) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToUmlaut(letter) + "";
					}
				}
			}

			if ((e.KeyCode == Keys.B) & (e.Control)) {
				if ((RTextBox.SelectionFont != null))
				{
					RTextBox.SelectionFont =
						new Font(RTextBox.SelectionFont, (RTextBox.SelectionFont.Style ^ FontStyle.Bold));
				}
				RTextBox.Select(selStart, selectedLen);
			}

			// italics - Serious bug, find via keywork "kludge"  <--- OLD BUT MAY STILL BE RELEVANT
			// Essentially substituted Keys.I with Keys.O

			if ((e.KeyCode == Keys.I) & (e.Control)) {
				if ((RTextBox.SelectionFont != null))
				{
					RTextBox.SelectionFont =
						new Font(RTextBox.SelectionFont, (RTextBox.SelectionFont.Style | FontStyle.Italic));
				}
				RTextBox.Select(selStart, selectedLen);
			}



			if ((e.KeyCode == Keys.U) & (e.Control)) {
				if ((RTextBox.SelectionFont != null))
				{
					RTextBox.SelectionFont =
						new Font(RTextBox.SelectionFont, (RTextBox.SelectionFont.Style ^ FontStyle.Underline));
				}
				RTextBox.Select(selStart, selectedLen);
			}

			//  ctrl ' for  Circumflex   â    From btnSpChar3.PerformClick();
			if ((e.KeyCode == Keys.K) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToCircumflex(letter) + "";
					}
					// System.Windows.Forms.SendKeys.Send(" ")  ' needed to override existing shortcuts
				}
			}


			// ctrl -  for long vowel ā  From btnSpChar4.PerformClick();
			if ((e.KeyCode == Keys.K) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToLong(letter);
					}
				}
			}


			// ctrl -  for long vowel ā    From btnSpChar4.PerformClick();
			if ((e.KeyCode == Keys.OemMinus) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToLong(letter);
					}
				}
			}


			// ctrl for Short Vowel  ă   From btnSpChar5.PerformClick();
			if ((e.KeyCode == Keys.Oemplus) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToShort(letter);
					}
					//.SelectionStart, 0)
				}
			}


			// ctrl ,  for Cedilla Ç   From btnSpChar6.PerformClick();
			if ((e.KeyCode == Keys.Oemcomma) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						if (letter == "c")
						{
							Globals.current_RTB_withFocus.SelectedText = "ç";
						}
						if (letter == "C")
						{
							Globals.current_RTB_withFocus.SelectedText = "Ç";
						}
					}
				}
			}

			// Keys.OemPeriod:
			// ctrl .
			// UNASSIGNED

			// ctrl shift  (  for «
			if ((e.KeyCode == Keys.D9) & (e.Control)) {
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.SelectedText = "«" + "";
				}
			}


			// ctrl shift )   for »
			if ((e.KeyCode == Keys.D0) & (e.Control)) {
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.SelectedText = "»" + "";
				}
			}



		}


		public static string getAndSelectLetterBehindCursor()
		{
			int selStart = Globals.current_RTB_withFocus.SelectionStart;
			string selectedText = "";
			if (selStart > 0)
			{
				Globals.current_RTB_withFocus.Select(selStart - 1, 1);
				selectedText = Globals.current_RTB_withFocus.SelectedText;
			}
			return selectedText;
		}


		public static void handleGreekKeyboardShortcuts(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			int selStart = Globals.current_RTB_withFocus.SelectionStart;
			string letter = string.Empty;

			// ctrl \  for Grave accent  à    From btnSpChar0.PerformClick();
			if ((e.KeyCode == Keys.Oem5) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToGrave(letter);
					}
				}
			}

			if ((e.KeyCode == Keys.Oem2) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				// ctrl / for Accute Accent  á    From btnSpChar1.PerformClick();
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToAcute(letter);
					}
				}
			}


			// ctrl  ;  for high dot ˙
			if ((e.KeyCode == Keys.Oem2) & (e.Control)) {
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.SelectedText = "˙" + "";
				}
			}


			// Long and short vowels
			if ((e.KeyCode == Keys.OemMinus) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				// ctrl -  for  Macron   ᾱ    From btnSpChar4.PerformClick();
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToLong(letter);
					}
				}
			}


			if ((e.KeyCode == Keys.Oemplus) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				// ctrl =  for  Short  ᾰ    From btnSpChar5.PerformClick();
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToShort(letter);
					}
				}
			}


			//  ctrl κ for  Circumflex ᾶ    From btnSpChar3.PerformClick();
			if ((e.KeyCode == Keys.K) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToTrema(letter) + "";
					}
				}
			}


			// ctrl ' for iota subscript  ᾳ    From btnSpChar16.PerformClick();
			if ((e.KeyCode == Keys.Q) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToIotaSubscript(letter) + "";
					}
				}

				if ((e.KeyCode == Keys.K) & (e.Control))
				{

				}
			}

			if ((e.KeyCode == Keys.Oem6) & (e.Control)) {
				if (Control.ModifierKeys == Keys.Shift) { }
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.Text = Globals.current_RTB_withFocus.Text.Insert(selStart, "«");
					Globals.current_RTB_withFocus.SelectionStart = Globals.selStart + 1;
				}
			}


			// ctrl .  for rough mark  ἁ  From btnSpChar18.PerformClick();
			if ((e.KeyCode == Keys.OemPeriod) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);

				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToRough(letter) + "";
					}
				}
			}


			// ctrl  ,  for smooth mark  ἀ  From btnSpChar19.PerformClick();
			if ((e.KeyCode == Keys.Oemcomma) & (e.Control)) {
				letter = getAndSelectLetterBehindCursor(Globals.current_RTB_withFocus);
				if (letter.Length < 1)
				{
					MessageBox.Show("Enter a vowel first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (char.IsLetter(Convert.ToChar(letter)))
				{
					if (Control.ModifierKeys == Keys.ControlKey)
					{
						Globals.current_RTB_withFocus.SelectedText = GetSpecialCharacters.changeVowelToSmooth(letter) + "";
					}
				}
			}



			if ((e.KeyCode == Keys.D9) & (e.Control)) {
				// ctrl shift  (  for «
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.SelectedText = "«" + "";
				}
			}


			if ((e.KeyCode == Keys.D0) & (e.Control)) {
				// ctrl shift )  for »
				if (Control.ModifierKeys == Keys.ControlKey)
				{
					Globals.current_RTB_withFocus.SelectedText = "»" + "";
				}
			}


		}





	}
}
//		string name = RTextBox.Name;
//		int cursor = RTextBox.SelectionStart;