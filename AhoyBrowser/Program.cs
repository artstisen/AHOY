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
            // Initialize the application (before any window is created)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // The forms were designed with .NET Framework's default font (Microsoft Sans Serif 8.25pt,
            // AutoScaleDimensions 6x13). .NET Core+ defaults to Segoe UI 9pt, which makes every form
            // auto-scale ~15% and pushes manually positioned controls out of place.
            Application.SetDefaultFont(new System.Drawing.Font("Microsoft Sans Serif", 8.25F));

            string dataDirectory = GetDataDirectoryArgument(args);
            if (dataDirectory != null)
            {
                Directory.CreateDirectory(dataDirectory);
                IO.DataRootOverride = Path.GetFullPath(dataDirectory);
            }

            // Prevent Ahoy from running more than one instance (per data folder)
            string mutexName = "ahoymutexid";
            if (dataDirectory != null)
            {
                byte[] pathHash = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(IO.DataRootOverride.ToUpperInvariant()));
                mutexName += "-" + Convert.ToHexString(pathHash, 0, 8);
            }
            bool result;
            var mutex = new System.Threading.Mutex(true, mutexName, out result);

            if (!result)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Another instance of AHOY! is already running", MessageText.AhoyCaption(), false);
                msg.ShowDialog();
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
    }
}
