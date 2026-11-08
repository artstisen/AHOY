using System;
using System.IO;
using System.Windows.Forms;

namespace AhoyBrowser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Optional: --data-dir "path" uses that folder instead of %APPDATA% (e.g. to test with a copy of a Pool).
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string dataDirectory = GetDataDirectoryArgument(args);
            if (dataDirectory != null)
            {
                Directory.CreateDirectory(dataDirectory);
                IO.DataRootOverride = Path.GetFullPath(dataDirectory);
            }

            // Report unexpected errors instead of closing silently
            CrashReporter.Install();

            try
            {
                Run(dataDirectory);
            }
            catch (Exception ex)
            {
                // Errors before/after the message loop (e.g. while creating the main window)
                CrashReporter.Report(ex, canContinue: false);
            }
        }

        private static void Run(string dataDirectory)
        {
            // Initialize the application (before any window is created)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // The forms were designed with .NET Framework's default font (Microsoft Sans Serif 8.25pt,
            // AutoScaleDimensions 6x13). .NET Core+ defaults to Segoe UI 9pt, which makes every form
            // auto-scale ~15% and pushes manually positioned controls out of place.
            Application.SetDefaultFont(new System.Drawing.Font("Microsoft Sans Serif", 8.25F));

            // Prevent Ahoy from running more than one instance (per data folder)
            string mutexName = "ahoymutexid";
            if (dataDirectory != null && !IsDefaultDataRoot(IO.DataRootOverride))
            {
                byte[] pathHash = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(IO.DataRootOverride.ToUpperInvariant()));
                mutexName += "-" + Convert.ToHexString(pathHash, 0, 8);
            }
            bool result;
            var mutex = new System.Threading.Mutex(true, mutexName, out result);

            if (!result)
            {
                // Already running: bring it (or the dialog it is waiting on) to the front
                if (!SingleInstance.ActivateRunningInstance())
                {
                    MessageBox.Show("AHOY! is already running.\n\nIf you can't see it, check the taskbar or close Ahoy.exe in Task Manager and start it again.",
                        MessageText.AhoyCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            // Run Splash
            Application.Run(new Splash());

            // Run Ahoy
            Application.Run(new MainForm());

            GC.KeepAlive(mutex); // mutex shouldn't be released - important line
        }

        private static string GetDataDirectoryArgument(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (string.Equals(args[i], "--data-dir", StringComparison.OrdinalIgnoreCase))
                {
                    return args[i + 1];
                }
            }
            return null;
        }

        // --data-dir pointing at %APPDATA% is the normal data folder: it shares the normal instance lock
        private static bool IsDefaultDataRoot(string path)
        {
            string defaultRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return string.Equals(Path.TrimEndingDirectorySeparator(Path.GetFullPath(path)),
                Path.TrimEndingDirectorySeparator(Path.GetFullPath(defaultRoot)), StringComparison.OrdinalIgnoreCase);
        }
    }
}
