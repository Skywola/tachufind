using System;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;


namespace Tachufind
{
    public partial class FrmWordFrequency : Form
    {
        Dictionary<string, string> entries = new Dictionary<string, string>();

        public FrmWordFrequency()
        {
            InitializeComponent();
            RTBDictionaryWord.Enter += RTBDictionaryWord_Enter;
            RTBWordDefinition.Enter += RTBDictionaryWord_Enter;
        }
        string voice1 = string.Empty;
        string voice2 = string.Empty;

        private void FrmWordFrequency_Load(object sender, EventArgs e)
        {
            using (var synth = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    string realName = voice.VoiceInfo.Name;          // required for SelectVoice()
                    string displayName = voice.VoiceInfo.Description // friendly text
                        .Replace("Microsoft", "")
                        .Replace("Desktop", "")
                        .Replace("IVONA 2", "")
                        .Replace("Harpo", "")
                        .Replace("[22kHz]", "")
                        .Replace("22kHz", "")
                        .Trim();

                    // Add display text but store real name in Tag
                    CmbWFVoice1.Items.Add(new ComboBoxItem(displayName, realName));
                    CmbWFVoice2.Items.Add(new ComboBoxItem(displayName, realName));
                    Globals.Current_RTB_withFocus = RTBDictionaryWord;
                }
            }

            if (!string.IsNullOrEmpty(Globals.User_Settings.TTS_WFVoice1))
            {
                foreach (ComboBoxItem item in CmbWFVoice1.Items)
                {
                    if (item.Value == Globals.User_Settings.TTS_WFVoice1)
                    {
                        CmbWFVoice1.SelectedItem = item;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(Globals.User_Settings.TTS_WFVoice2))
            {
                foreach (ComboBoxItem item in CmbWFVoice2.Items)
                {
                    if (item.Value == Globals.User_Settings.TTS_WFVoice2)
                    {
                        CmbWFVoice2.SelectedItem = item;
                        break;
                    }
                }
            }

            NumVolume1.Value = Globals.User_Settings.TTS_WFVoice1_Volume;
            NumVolume2.Value = Globals.User_Settings.TTS_WFVoice2_Volume;
            NumRate1.Value = Globals.User_Settings.TTS_WFVoice1_Rate1;
            NumRate2.Value = Globals.User_Settings.TTS_WFVoice1_Rate2;

            AppSettings.loadfirst = false;
        }

        private void BtnCreateAudio_Click(object sender, EventArgs e)
        {
            if (entries.Count == 0)
            {
                MessageBox.Show("No entries to export.");
                return;
            }

            using (var synth = new SpeechSynthesizer())
            {
                synth.Rate = -2;
                synth.Volume = 100;

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "WAV Audio (*.wav)|*.wav";
                    dlg.Title = "Save Flashcard Audio";

                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    // ⭐ NEW: Create text file path next to WAV
                    string textFilePath = Path.ChangeExtension(dlg.FileName, ".txt");
                    ExportDictionaryToText(textFilePath);

                    // IMPORTANT: Use the correct API for multi‑Speak WAV output
                    synth.SetOutputToWaveFile(dlg.FileName);

                    // ⭐ EXTRACTED: Call the new method
                    ProcessAllEntries(synth, entries);

                    // Release the file cleanly
                    synth.SetOutputToNull();
                }
            }

            CleanupAfterAudioExport();
        }

        // ⭐ NEW EXTRACTED METHOD
        private void ProcessAllEntries(SpeechSynthesizer synth, Dictionary<string, string> entries)
        {
            int processed = 0;
            int total = entries.Count;

            foreach (var pair in entries)
            {
                ProcessSingleEntry(synth, pair.Key, pair.Value);

                // Progress bar update
                processed++;
                UpdateProgressBar(processed, total);
            }
        }

        // ⭐ NEW: Process a single flashcard entry
        private void ProcessSingleEntry(SpeechSynthesizer synth, string word, string definition)
        {
            // Get voice settings (moved outside loop for efficiency)
            var voice1 = GetSelectedVoice1();
            var voice2 = GetSelectedVoice2();

            // Voice 1 - Speak word
            ConfigureVoice(synth, voice1, (int)NumVolume1.Value, (int)NumRate1.Value);
            synth.Speak(word);

            // Pause 1
            SpeakPause(synth, 0.6);

            // Voice 2 - Speak definition
            ConfigureVoice(synth, voice2, (int)NumVolume2.Value, (int)NumRate2.Value);
            synth.Speak(definition);

            // Pause 1 again
            SpeakPause(synth, 0.6);

            // Voice 1 - Repeat word
            ConfigureVoice(synth, voice1, (int)NumVolume1.Value, (int)NumRate1.Value);
            synth.Speak(word);

            // Pause 2 (longer)
            SpeakPause(synth, 1.2);
        }

        // ⭐ NEW: Helper methods
        private string GetSelectedVoice1()
        {
            var item = CmbWFVoice1.SelectedItem as ComboBoxItem;
            return item?.Value ?? "Microsoft Irina Desktop";
        }

        private string GetSelectedVoice2()
        {
            var item = CmbWFVoice2.SelectedItem as ComboBoxItem;
            return item?.Value ?? "Microsoft David Desktop";
        }

        private void ConfigureVoice(SpeechSynthesizer synth, string voice, int volume, int rate)
        {
            synth.SelectVoice(voice);
            synth.Volume = volume;
            synth.Rate = rate;
        }

        private void SpeakPause(SpeechSynthesizer synth, double seconds)
        {
            var pause = new PromptBuilder();
            pause.AppendBreak(TimeSpan.FromSeconds(seconds));
            synth.Speak(pause);
        }

        private void UpdateProgressBar(int processed, int total)
        {
            int percent = (int)((processed / (double)total) * 100);
            progressBarAudio.Value = percent;
            progressBarAudio.Refresh();
        }

        private void CleanupAfterAudioExport()
        {
            progressBarAudio.Value = 0;
            entries.Clear();
            LblDictCount.Text = entries.Count.ToString();

            MessageBox.Show("Audio file created successfully!", "Done",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportDictionaryToText(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Header
                writer.WriteLine("Word\t\t\t\t\tDefinition");
                writer.WriteLine("---------------------------------------------------");

                foreach (var pair in entries)
                {
                    writer.WriteLine(FormatDictionaryLine(pair.Key, pair.Value));
                }
            }
        }

        private string FormatDictionaryLine(string word, string definition)
        {
            int columnWidth = 30; // adjust as needed
            return word.PadRight(columnWidth) + "\t" + definition;
        }


        private int GetMaxWordLength()
        {
            return entries.Keys.Max(w => w.Length) + 5; // add padding
        }



        private void RTBWordFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Capture what you need *now* while the selection is still valid
                string selectedTextNow = RTBWordFreq.SelectedText;
                int originalStart = RTBWordFreq.SelectionStart;

                string word = RegexHelpers.GetWordRegex().Match(selectedTextNow).Value;
                int wordLength = word.Length;

                // Now defer only the *application* of the selection
                BeginInvoke(new Action(() =>
                {
                    RTBWordFreq.Select(originalStart, wordLength);
                    RTBDictionaryWord.Text = word; // + word + Environment.NewLine;
                }));
                Clipboard.SetText(word); // Copy the word to clipboard
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string key = RTBDictionaryWord.Text.Trim();
            string value = RTBWordDefinition.Text.Trim();

            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
            {
                entries[key] = value; // adds or updates
                //Clipboard.SetText(key); // Copy the word to clipboard
                LblDictCount.Text = entries.Count.ToString();
                RTBDictionaryWord.Clear();
                RTBWordDefinition.Clear();
            }
            else
            {
                MessageBox.Show("Please enter text before saving.", "Missing Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            RTBDictionaryWord.Text = "";
            RTBWordDefinition.Text = "";
        }


        private void RTBWordFreq_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // Get the index in the text where the mouse was clicked
                int index = RTBWordFreq.GetCharIndexFromPosition(e.Location);

                // If index is invalid, exit
                if (index < 0 || index >= RTBWordFreq.Text.Length)
                    return;

                string text = RTBWordFreq.Text;

                // Expand left to find the start of the word
                int start = index;
                while (start > 0 && char.IsLetterOrDigit(text[start - 1]))
                    start--;

                // Expand right to find the end of the word
                int end = index;
                while (end < text.Length && char.IsLetterOrDigit(text[end]))
                    end++;

                int length = end - start;

                if (length <= 0)
                    return;

                string word = text.Substring(start, length);

                // Apply selection AFTER the click event finishes
                BeginInvoke(new Action(() =>
                {
                    RTBWordFreq.Select(start, length);
                    RTBDictionaryWord.Text = word;
                }));

                Clipboard.SetText(word);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmWordFrequency_FormClosing(object sender, FormClosingEventArgs e)
        {
            var item1 = CmbWFVoice1.SelectedItem as ComboBoxItem;
            var item2 = CmbWFVoice2.SelectedItem as ComboBoxItem;

            Globals.User_Settings.TTS_WFVoice1 = item1?.Value ?? "";
            Globals.User_Settings.TTS_WFVoice2 = item2?.Value ?? "";

            Globals.User_Settings.TTS_WFVoice1_Volume = (int)NumVolume1.Value;
            Globals.User_Settings.TTS_WFVoice2_Volume = (int)NumVolume2.Value;
            Globals.User_Settings.TTS_WFVoice1_Rate1 = (int)NumRate1.Value;
            Globals.User_Settings.TTS_WFVoice1_Rate2 = (int)NumRate2.Value;
        }

        private void CmbWFVoice1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.TTS_WFVoice1 = CmbWFVoice1.Text;
        }

        private void CmbWFVoice2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.User_Settings.TTS_WFVoice2 = CmbWFVoice2.Text;
        }

        private void RTBDictionaryWord_Enter(object? sender, EventArgs e)
        {
            if (sender is RichTextBox rtb)
            {
                Globals.Current_RTB_withFocus = (RichTextBox)sender;
            }
                
        }

        private void RTBWordDefinition_Enter(object sender, EventArgs e)
        {
            if (sender is RichTextBox rtb) 
            {
                Globals.Current_RTB_withFocus = (RichTextBox)sender;
            }                
        }
    }
}
