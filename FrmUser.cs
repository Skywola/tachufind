using Microsoft.Win32;
using System.Diagnostics;

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
            if (!AppSettings.UserHasAgreed)
            {
                Globals.User_Settings.StrSetUserAgreed = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AppSettings.UserHasAgreed = false;
            Application.Exit();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            RegisterFileAssociations();
            Globals.User_Settings.StrSetUserAgreed = "согласен";
            AppSettings.UserHasAgreed = true;
            this.Close();
        }

        private static void RegisterFileAssociations()
        {
            try
            {
                string? exePath = Process.GetCurrentProcess().MainModule?.FileName;

                if (string.IsNullOrWhiteSpace(exePath))
                {
                    throw new InvalidOperationException("Unable to determine executable path.");
                }

                // Register application for .rtf
                RegisterFileType("rtf", exePath);

                // Register application for .txt
                RegisterFileType("txt", exePath);

                // Register application for .srt
                RegisterFileType("srt", exePath);

                Console.WriteLine("File associations registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering file associations: " + ex.Message);
            }
        }

        private static void RegisterFileType(string extension, string exePath)
        {
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey($@"Tachufind.{extension}\shell\open\command"))
            {
                key?.SetValue("", "\"" + exePath + "\" \"%1\"");
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey($@".{extension}"))
            {
                key?.SetValue("", $"Tachufind.{extension}");
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey($@"Tachufind.{extension}\DefaultIcon"))
            {
                key?.SetValue("", "\"" + exePath + "\",0");
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey($@".{extension}\OpenWithProgids"))
            {
                key?.SetValue($"Tachufind.{extension}", $"Tachufind.{extension}");
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey($@".{extension}\OpenWithList\Tachufind.exe"))
            {
                key?.SetValue("", "");
            }
        }


    }
}
