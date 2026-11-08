using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    /// <summary>
    /// Password prompt. The dialog never sees the stored secret: it hands what the user typed to a
    /// verifier (normally "does this password decrypt the pool / match the in-memory key?").
    /// There is deliberately no password reset: without the password the pool cannot be decrypted.
    /// </summary>
    public partial class Password : Form
    {
        internal bool AccessGranted { get; set; }
        private bool EnableKeyCommands { get; set; }
        internal bool LoadCommandActivated { get; set; }
        private Timer BringToFrontTimer { get; set; }
        private readonly Func<string, bool> verifyPassword;

        public Password(string userMessage, string displayCaption, string welcomeMessage, Func<string, bool> verifyPassword, bool showCancelButton, double formOpacity, bool enableKeyCommands)
        {
            InitializeComponent();
            this.verifyPassword = verifyPassword ?? throw new ArgumentNullException(nameof(verifyPassword));

            BringToFrontTimer = new Timer();
            BringToFrontTimer.Tick += BringToFrontTimer_Tick;
            BringToFrontTimer.Interval = 250;
            BringToFrontTimer.Enabled = false;

            // Text length
            tbInput.MaxLength = MainForm.MaxPasswordLength;

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;

            // Form Opacity
            this.Opacity = formOpacity > 0 ? formOpacity : 1.00d;

            // Hide the control box
            this.ControlBox = false;
            // Top Most
            this.TopMost = true;
            // Set key preview (to take key commands)
            this.KeyPreview = true;
            lbEscapeText.Text = "Press F5 to load";
            EnableKeyCommands = enableKeyCommands;
            lbEscapeText.Visible = EnableKeyCommands;
            LoadCommandActivated = false;
            this.KeyDown += Password_KeyDown;

            // Set fonts
            tbInput.Font = Design.PasswordFieldFont();

            // Set button appearance
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnLogin.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnLogin.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Setup context menu
            tbInput.ContextMenuStrip = contextMenuBasic;

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbWelcomeMessage.BackColor = this.BackColor;
            lbWelcomeMessage.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbMessage.BackColor = this.BackColor;
            lbMessage.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbErrorMessage.BackColor = this.BackColor;
            lbErrorMessage.ForeColor = AhoyColors.ErrorTextColor();
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            btnLogin.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnLogin.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            pnlPasswordInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            lbEscapeText.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbEscapeText.BackColor = this.BackColor;

            this.Text = displayCaption.Trim();
            lbWelcomeMessage.Text = welcomeMessage;
            lbMessage.Text = userMessage;
            AccessGranted = false;

            if (!showCancelButton)
            {
                btnCancel.Visible = false;
            }
        }

        // Bring to front and focus
        private void BringToFrontTimer_Tick(object sender, EventArgs e)
        {
            BringToFrontTimer.Enabled = false;
            try
            {
                this.BringToFront();
                this.Activate();
                tbInput.Focus();
            }
            catch
            {
                // Do nothing
            }
        }

        // Form commands ("Press F5 to Load")
        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (EnableKeyCommands)
            {
                // F5 or CTRL+O
                if (e.KeyCode == Keys.F5 || e.KeyData == (Keys.Control | Keys.O))
                {
                    // Load pool from password dialog
                    LoadCommandActivated = true;
                    AccessGranted = false;
                    this.Close();
                }
            }
        }

        private void Password_Load(object sender, EventArgs e)
        {
            tbInput.TextChanged += TbInput_TextChanged;
            // Bring to front
            BringToFrontTimer.Enabled = true;
        }

        // Change button text color on input
        private void TbInput_TextChanged(object sender, EventArgs e)
        {
            btnLogin.ForeColor = !string.IsNullOrEmpty(tbInput.Text)
                ? AhoyColors.CurrentColorTheme()[28]
                : AhoyColors.CurrentColorTheme()[8];
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Key derivation takes a moment (by design), so show that we are working
            bool granted;
            Cursor previousCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            btnLogin.Enabled = false;
            try
            {
                granted = verifyPassword(tbInput.Text.Trim());
            }
            catch
            {
                granted = false;
            }
            finally
            {
                btnLogin.Enabled = true;
                this.Cursor = previousCursor;
            }

            if (granted)
            {
                Log.LogLogin();
                AccessGranted = true;
                this.Close();
            }
            else
            {
                Log.LogLoginAttempt();
                lbErrorMessage.Text = "Wrong password. Please try again.";
                tbInput.Text = "";
                tbInput.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, null);

                // Prevent Ding
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && btnCancel.Visible)
            {
                btnCancel_Click(this, null);
            }
            else if (Control.IsKeyLocked(Keys.CapsLock))
            {
                lbErrorMessage.Text = "CapsLock is enabled!";
            }
            else if (!Control.IsKeyLocked(Keys.CapsLock))
            {
                lbErrorMessage.Text = "";
            }
        }

        // Used for canceling out of the dialog and quitting ahoy on starup
        private void btnCancel_Click(object sender, EventArgs e)
        {
            AccessGranted = false;
            this.Close();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }
    }
}
