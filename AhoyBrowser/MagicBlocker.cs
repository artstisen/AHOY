using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AhoyBrowser
{
    public partial class MagicBlocker : Form
    {
        // Define properties
        internal static bool ExitMagickBlock;
        List<MagicBlockerOverlay> listOverlays;
        MagicBlockerOverlay overlay;
        double tempOpacity;
        private string auth;
        internal const string IndicatorLabelText = "CTRL+ALT+ENTER";

        // Constructor
        public MagicBlocker(string authorization)
        {
            InitializeComponent();
            Auth = authorization;
            listOverlays = new List<MagicBlockerOverlay>();
            timerBlocker.Enabled = false;
            ExitMagickBlock = false;
            MaximizeForm();

        }

        private void StartFader()
        {
            tempOpacity = 0.6d;
            this.Opacity = tempOpacity;
            Log.ScreenBlockerStaus("Start fade (" + tempOpacity + ")");
            timerFader.Interval = 25;
            timerFader.Enabled = true;
        }

        private void timerFader_Tick(object sender, EventArgs e)
        {
            if (tempOpacity > 0.2d)
            {
                tempOpacity -= 0.1d;
                this.Opacity = tempOpacity;
                Log.ScreenBlockerStaus("Fade (" + tempOpacity + ")");
            }
            else
            {
                Log.ScreenBlockerStaus("Stop fade (" + tempOpacity + ")");
                timerFader.Enabled = false;
                tempOpacity = 0.01d;
                this.Opacity = tempOpacity;
            }
        }

        // Maximize and start timer
        private void MaximizeForm()
        {
            try
            {
                // Important if full screen (multiple monitors) shall work
                this.StartPosition = FormStartPosition.Manual;

                // Set opacity to 0.01d (0 will make it disappear)
                this.Opacity = 0.01d;

                // Then place and resize
                int screenLeft = SystemInformation.VirtualScreen.Left;
                int screenTop = SystemInformation.VirtualScreen.Top;
                int screenWidth = SystemInformation.VirtualScreen.Width;
                int screenHeight = SystemInformation.VirtualScreen.Height;
                this.Location = new System.Drawing.Point(screenLeft, screenTop);
                this.Size = new System.Drawing.Size(screenWidth, screenHeight);

                // Topmost and bring to front
                this.TopMost = true;
                this.BringToFront();
                timerBlocker.Enabled = true; // Enable blocker timer
                StartFader();
            }
            catch
            {
                // Log event and exit
                Log.ScreenBlockerStaus("Error (Maximization)");
                ExitMagickBlock = true; // Exit application
                this.Close();
            }
        }


        // When clicking the form with the Mouse
        private void MagicBlock_Click(object sender, EventArgs e)
        {
            PostMessage();
        }

        private void MagicBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.Enter) // Prompt for password by pressing CTRL + ALT + ENTER
            {
                // Log event
                Log.ScreenBlockerStaus("Exit request");

                // Stop timer
                timerBlocker.Enabled = false;

                // Prompt for password
                if (SimplePromptForPassword(MessageText.PasswordCaption(), "Exit Magic Screen Blocker", Auth))
                {
                    MainForm.KeepAwake();
                    ExitMagickBlock = true; // Exit application
                    this.Close();
                }
                else
                {
                    // Start timer
                    timerBlocker.Enabled = true;
                }
            }
            else
            {
                if (e.Control || e.Alt) // Catch CTRL and ALT keys if pressed separately
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                else
                {
                    PostMessage(); // Post dialog
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }

        // Ask user for password to verify before continuing
        private static bool tempUserGranted;
        protected string Auth { get => auth; set => auth = value; }
        private bool SimplePromptForPassword(string displayCaption, string optionalWelcomeMessage, string authPassword)
        {
            tempUserGranted = false;
            // Ask for password
            Password passwordDialog = new Password(MessageText.EnterPasswordCaption(), displayCaption, optionalWelcomeMessage, authPassword, "", "", true, false, 1.0d, false);
            passwordDialog.TopMost = true;
            passwordDialog.ShowDialog();
            tempUserGranted = passwordDialog.AccessGranted;
            return tempUserGranted;
        }

        // Post message
        private void PostMessage()
        {
            // Log event
            Log.ScreenBlockerStaus("Triggered");

            try
            {
                // Create a maximum of 24 overlays
                if (listOverlays.Count < 24)
                {
                    CreateOverlay();
                }
                else if (listOverlays.Count > 1) // Else remove an overlay, and create a new one
                {
                    listOverlays[0].Close();
                    listOverlays.RemoveAt(0);
                    CreateOverlay();
                }
            }
            catch
            {
                ExitMagickBlock = true;
                this.Close();
            }
        }

        private void CreateOverlay()
        {
            // Create new overlay
            overlay = new MagicBlockerOverlay();
            // add it to the overlay list for reference
            listOverlays.Add(overlay);
            overlay.Show();
            overlay.Top = MousePosition.Y - 170;
            overlay.Left = MousePosition.X - 125;
            overlay.BringToFront();
        }

        // Close collection of overlays
        private void CloseOverlays()
        {
            if (listOverlays != null && listOverlays.Count > 0)
            {
                Log.ScreenBlockerStaus("Performing cleanup");
                for (int i = 0; i < listOverlays.Count; i++)
                {
                    listOverlays[i].Close();
                }
            }
        }

        // Force the application window to be topmost
        private void timerBlocker_Tick(object sender, EventArgs e)
        {
            // Topmost and bring to front
            this.TopMost = true;
            this.BringToFront();
            this.Activate(); // Activate to remove focus from other applications
        }

        // Prevent application from closing
        // Application will only exit if ExitApplication = true
        private void MagicBlock_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if exit is okay
            if (!ExitMagickBlock)
            {
                // Log event
                Log.ScreenBlockerStaus("Exit attempt");
                e.Cancel = true;
            }
            else
            {
                // Close overlays
                CloseOverlays();
                // Log event
                Log.ScreenBlockerStaus("Exit");
                MainForm.ScreenBlockerIsActive = false; // Indicate inactive
            }
        }


    }
}
