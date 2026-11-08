using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AhoyBrowser
{
    /// <summary>
    /// Makes unexpected errors visible: without this, an error before the main window appears ends
    /// the process silently (Windows 11 shows no crash dialog by default).
    /// The log contains the error and stack trace only, never note content.
    /// </summary>
    internal static class CrashReporter
    {
        internal const string LogFileName = "Crash.log";
        private const long MaxLogSize = 256 * 1024; // keep the log small
        private static int showingDialog;

        internal static void Install()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => Report(e.Exception, canContinue: true);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => Report(e.ExceptionObject as Exception, canContinue: false);
        }

        /// <summary>Log the error and tell the user. Returns true if the user chose to keep Ahoy running.</summary>
        internal static bool Report(Exception exception, bool canContinue)
        {
            string logPath = WriteLog(exception);

            // Avoid a cascade of dialogs if errors keep coming
            if (Interlocked.Exchange(ref showingDialog, 1) == 1)
            {
                return canContinue;
            }
            try
            {
                string message = "AHOY! ran into an unexpected error" + (canContinue ? "." : " and has to close.") + Environment.NewLine + Environment.NewLine +
                                 (exception?.Message ?? "Unknown error") + Environment.NewLine + Environment.NewLine +
                                 (logPath != null ? "Details were saved to:" + Environment.NewLine + logPath + Environment.NewLine + Environment.NewLine : "") +
                                 "Please include this file when reporting the problem (it contains no note content)." +
                                 (canContinue ? Environment.NewLine + Environment.NewLine + "Continue running AHOY!?" : "");

                // Standard message box: works even if Ahoy's own forms are what failed
                DialogResult result = MessageBox.Show(message, "AHOY! - Error",
                    canContinue ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (canContinue && result == DialogResult.No)
                {
                    Environment.Exit(1);
                }
                return canContinue;
            }
            catch
            {
                return canContinue;
            }
            finally
            {
                Interlocked.Exchange(ref showingDialog, 0);
            }
        }

        /// <summary>Append the error to %APPDATA%\Ahoy\Crash.log (or the --data-dir folder). Returns the path, or null.</summary>
        internal static string WriteLog(Exception exception)
        {
            try
            {
                string directory = Path.Combine(IO.ApplicationDataRoot(), IO.ProgramFolderName);
                Directory.CreateDirectory(directory);
                string path = Path.Combine(directory, LogFileName);

                if (File.Exists(path) && new FileInfo(path).Length > MaxLogSize)
                {
                    File.Delete(path);
                }

                var entry = new StringBuilder();
                entry.AppendLine("==== " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ====");
                entry.AppendLine("Version: " + typeof(CrashReporter).Assembly.GetName().Version);
                entry.AppendLine("OS: " + Environment.OSVersion + (Environment.Is64BitProcess ? " (64-bit)" : " (32-bit)"));
                entry.AppendLine(".NET: " + Environment.Version);
                entry.AppendLine(exception?.ToString() ?? "Unknown error");
                entry.AppendLine();
                File.AppendAllText(path, entry.ToString(), Encoding.UTF8);
                return path;
            }
            catch
            {
                return null;
            }
        }
    }
}
