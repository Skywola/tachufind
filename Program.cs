namespace Tachufind
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Keep buttons from getting messed up by screen resolution changes.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            // Install your global hotkey handler BEFORE the message loop starts
            Application.AddMessageFilter(new HotkeyMessageFilter());

            Application.Run(new FrmMain());
        }
    }
}