using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Prevent Ahoy from running more than one instance
            bool result;
            var mutex = new System.Threading.Mutex(true, "ahoymutexid", out result);

            if (!result)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Another instance of AHOY! is already running", MessageText.AhoyCaption(), false);
                msg.ShowDialog();
                return;
            }

            // Initialize the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Run Splash
            Application.Run(new Splash());

            // Run Ahoy
            Application.Run(new MainForm());


            GC.KeepAlive(mutex); // mutex shouldn't be released - important line
        }
    }
}
