using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace Tachufind
{
    public partial class FrmOCR : Form
    {
    public FrmOCR()
        {
            InitializeComponent();
        }

        public String ImagePath;

        private void FrmOCRLoad(object sender, EventArgs e)
        {
            CmbLanguage.Text = Globals.User_Settings.OCR_Language;
            Globals.FrmOCRExists = true;
        }

        private void BtnGetImageClick(object sender, EventArgs e)
        {
            try
            {
                string pathLastFolderAccessed = Globals.User_Settings.lastFolderPathToOCRImage;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                // image filters  
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (!string.IsNullOrEmpty(pathLastFolderAccessed)) // set initial directory to the last accessed folder path, if available
                {
                    openFileDialog.InitialDirectory = pathLastFolderAccessed;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    this.pictureBox.Image = new Bitmap(openFileDialog.FileName);
                    var imageSize = this.pictureBox.Image.Size;
                    var fitSize = this.pictureBox.ClientSize;

                    pictureBox.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ?
                    PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;

                    ImagePath = openFileDialog.FileName;
                    Globals.User_Settings.lastFolderPathToOCRImage = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetText_Click(object sender, EventArgs e)
        {
            try 
            {
                BtnGetText.Enabled = false;
                string languageCode = GetLanguageIndicator();
                var text = string.Empty;

                // var image = Pix.LoadFromFile(@ImagePath);
                System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath);
                Bitmap bitmap = new Bitmap(image);
                bitmap.SetResolution(300, 300);

                if (string.IsNullOrEmpty(ImagePath))
                {
                    MessageBox.Show("Please load an image before attempting to get text.");
                    BtnGetText.Enabled = true;
                    return;
                }

                using (var engine = new TesseractEngine(Globals.TESSDATA_PREFIX, languageCode, EngineMode.Default))
                using (engine)
                {
                    using (image)
                    {
                        using (var page = engine.Process(bitmap))  
                        {
                            text = page.GetText();
                        }
                    }
                    bitmap.Dispose();
                    engine.Dispose();
                }
                RtbText.Text += text + Environment.NewLine + Environment.NewLine;
                RtbText.Refresh();
                BtnGetText.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCopyAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                btnCopyAndSuspend.Enabled = false;
                if (RtbText.Text.Length > 0)
                {
                    Globals.TessText = RtbText.Text;
                    Clipboard.SetText(Globals.TessText);
                    DateTime timeout = DateTime.Now.AddSeconds(2);
                    while (!Clipboard.ContainsText())
                    {
                        if (DateTime.Now > timeout)
                        {
                            // Clipboard did not contain text within 10 seconds
                            MessageBox.Show("Text was not copied to the clipboard.");
                            btnCopyAndSuspend.Enabled = true;
                            return;
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                }

                else
                {
                    // Handle the case where the rich text string is empty
                    MessageBox.Show("The textbox has no text in it.");
                    return;
                }
                btnCopyAndSuspend.Enabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GetLanguageIndicator()
        {
            string languageCode;
            switch (CmbLanguage.SelectedIndex)
            {
                case -1:
                    languageCode = "eng";
                    break;
                case 0:
                    languageCode = "deu";
                    break;
                case 1:
                    languageCode = "eng";
                    break;
                case 2:
                    languageCode = "fra";
                    break;
                case 3:
                    languageCode = "ell";
                    break;
                case 4:
                    languageCode = "ita";
                    break;
                case 5:
                    languageCode = "lat";
                    break;
                case 6:
                    languageCode = "rus";
                    break;
                case 7:
                    languageCode = "spa";
                    break;
                default:
                    languageCode = "eng";
                    break;
            }

            return languageCode;
        }

        private void FrmOCR_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Globals.Engine != null && !Globals.Engine.IsDisposed)
                {
                    Globals.Engine.Dispose();
                }
                FrmMain frmMain = new FrmMain();
                Globals.Current_RTB_withFocus = frmMain.RTBMain;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.RtbText.Clear();
        }

        

    }
}

