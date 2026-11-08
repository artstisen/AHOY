using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace AhoyBrowser
{
    public partial class Splash : Form
    {
        internal static Assembly asm { get; set; } // Used throughout the application
        internal static bool ReadOnlyMode { get; set; }
        internal static bool LoadPoolFile { get; set; }
        internal static bool StartDemo { get; set; }
        internal static bool StartupCommandActivated { get; set; }
        internal static bool KeyWasPressed { get; set; }
        internal static int TimerCounter { get; set; }
        internal static string AhoyVersionNumber { get; set; }
        public Splash()
        {
            InitializeComponent();
            // Will enable the form to take keypress events like Close on ESC
            KeyPreview = true;

            // Set command variables
            ReadOnlyMode = false;
            LoadPoolFile = false;
            StartDemo = false;
            StartupCommandActivated = false;
            KeyWasPressed = false;
            TimerCounter = 0;

            // Set start position
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Activate();
            asm = Assembly.GetExecutingAssembly(); // Get assembly
            lbVersion.Visible = false;
            lbVersion.Font = Design.SplashFont();
            lbVersion.TextAlign = ContentAlignment.MiddleLeft;
            lbVersion.ForeColor = AhoyColors.CurrentColorTheme()[2];
            AhoyVersionNumber = "v" + asm.GetName().Version.ToString(3); // major.minor.patch
            lbVersion.Text = AhoyVersionNumber + TextTransform.GetBlankSpace(5) + "F5: Load" + TextTransform.GetBlankSpace(3) + "F6: Read-Only" + TextTransform.GetBlankSpace(3) + "F7: Demo";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Do nothing here
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            // The image (1280x640) is drawn scaled down to the window: use high quality scaling
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gfx.DrawImage(ResourceCache.Get(nameof(Properties.Resources.splash)), new Rectangle(0, 0, this.Width, this.Height));
            if (!timerSplash.Enabled && !KeyWasPressed)
            {
                lbVersion.Visible = true;
                timerSplash.Enabled = true;
            }
        }

        private void timerSplashTimer_Tick(object sender, EventArgs e)
        {
            TimerCounter += 1;
            if (TimerCounter >= 3)
            {
                timerSplash.Enabled = false;
                this.Close();
            }
        }

        // Register startup commands
        private void Splash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (!StartupCommandActivated)
                {
                    KeyWasPressed = true;
                    TimerCounter = 1;
                    timerSplash.Enabled = false;
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("F6 was pressed during startup:\nLoad Pool in Read-Only Mode?", MessageText.StartupCommandsCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();

                    if (msg.Accepted)
                    {
                        StartupCommandActivated = true;
                        ReadOnlyMode = true;
                        lbVersion.Text = AhoyVersionNumber + TextTransform.GetBlankSpace(2) + "(Read-Only)";
                        timerSplash.Enabled = true;
                    }
                    else
                    {
                        timerSplash.Enabled = true;
                    }
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5 || e.KeyData == (Keys.Control | Keys.O))
            {
                if (!StartupCommandActivated)
                {
                    KeyWasPressed = true;
                    TimerCounter = 1;
                    timerSplash.Enabled = false;
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("F5/CTRL+O pressed during startup:\nLoad Pool or Backup?", MessageText.StartupCommandsCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();

                    if (msg.Accepted)
                    {
                        StartupCommandActivated = true;
                        LoadPoolFile = true;
                        lbVersion.Text = AhoyVersionNumber + TextTransform.GetBlankSpace(2) + "(Select file)";
                        timerSplash.Enabled = true;
                    }
                    else
                    {
                        timerSplash.Enabled = true;
                    }
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (!StartupCommandActivated)
                {
                    KeyWasPressed = true;
                    TimerCounter = 1;
                    timerSplash.Enabled = false;
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("F7 was pressed during startup:\nEnable Demo?", MessageText.StartupCommandsCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();

                    if (msg.Accepted)
                    {
                        StartupCommandActivated = true;
                        StartDemo = true;
                        lbVersion.Text = AhoyVersionNumber + TextTransform.GetBlankSpace(2) + "(Enable Demo)";
                        timerSplash.Enabled = true;
                    }
                    else
                    {
                        timerSplash.Enabled = true;
                    }
                }
                e.Handled = true;
            }
        }
    }
}
