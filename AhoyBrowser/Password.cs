using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class Password : Form
    {
        private int RetryCount { get; set; }
        internal bool AccessGranted { get; set; }
        internal string AuthPassword { get; set; }
        internal string AuthQuestion { get; set; }
        internal string AuthAnswer { get; set; }
        private bool AllowRestorePassword { get; set; }
        private bool RestorePassword { get; set; }
        internal bool PasswordRestored { get; set; }
        private bool EnableKeyCommands { get; set; }
        internal bool LoadCommandActivated { get; set; }
        private Timer BringToFrontTimer { get; set; }

        public Password(string userMessage, string displayCaption, string welcomeMessage, string authPassword, string authQuestion, string authAnswer, bool showCancelButton, bool allowRestorePassword, double formOpacity, bool enableKeyCommands)
        {
            InitializeComponent();

            BringToFrontTimer = new Timer();
            BringToFrontTimer.Tick += BringToFrontTimer_Tick;
            BringToFrontTimer.Interval = 250;
            BringToFrontTimer.Enabled = false;

            // Text length
            tbInput.MaxLength = MainForm.MaxPasswordLength;

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;

            // Form Opacity
            if (formOpacity > 0)
            {
                this.Opacity = formOpacity;
            }
            else
            {
                this.Opacity = 1.00d;
            }

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
            btnRestorePassword.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;

            btnLogin.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRestorePassword.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnLogin.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRestorePassword.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnLogin.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRestorePassword.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
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

            // Modify error message color, depending on theme ("Text Editor" and "Gadget")
            if (AhoyColors.ColorTheme == 1 || AhoyColors.ColorTheme == 2)
            {
                lbErrorMessage.ForeColor = AhoyColors.CurrentColorTheme()[4];
            }
            else
            {
                lbErrorMessage.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }

            tbInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            btnLogin.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRestorePassword.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnLogin.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRestorePassword.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            pnlPasswordInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            lbEscapeText.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbEscapeText.BackColor = this.BackColor;

            this.Text = displayCaption.Trim();
            lbWelcomeMessage.Text = welcomeMessage;
            lbMessage.Text = userMessage;
            AccessGranted = false;
            RetryCount = 3;
            AuthPassword = authPassword;
            AuthQuestion = authQuestion;
            AuthAnswer = authAnswer;

            // Determine if reset password is allowed
            AllowRestorePassword = allowRestorePassword;
            if (allowRestorePassword)
            {
                btnRestorePassword.Enabled = false;
                btnRestorePassword.Visible = false;
            }
            else
            {
                btnRestorePassword.Enabled = false;
                btnRestorePassword.Visible = false;
            }
            RestorePassword = false;
            PasswordRestored = false;

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

        // Form commands
        // ("Press F5 to Load")
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
            if (!string.IsNullOrEmpty(tbInput.Text))
            {
                btnLogin.ForeColor = AhoyColors.CurrentColorTheme()[28];
            }
            else
            {
                btnLogin.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (RestorePassword)
            {
                if (tbInput.Text.Trim() == AuthAnswer)
                {
                    CreatePassword createPassword = new CreatePassword("Please enter a new password", "New Password");
                    createPassword.ShowDialog();
                    Log.LogCreatePassword();
                    AuthPassword = createPassword.AuthPassword;
                    AuthQuestion = createPassword.AuthQuestion;
                    AuthAnswer = createPassword.AuthAnswer;
                    AccessGranted = true;
                    PasswordRestored = true;
                    this.Close();
                }
                else
                {
                    Log.LogLoginAttempt();
                    RetryCount -= 1;
                    lbErrorMessage.Text = "Please try again. Retries left: " + RetryCount.ToString();
                    tbInput.Text = "";
                    tbInput.Focus();

                    if (RetryCount == 0)
                    {
                        AccessGranted = false;
                        this.Close();
                    }
                }
            }
            else
            {
                if (tbInput.Text.Trim() == AuthPassword)
                {
                    Log.LogLogin();
                    AccessGranted = true;
                    this.Close();
                }
                else
                {
                    Log.LogLoginAttempt();
                    RetryCount -= 1;
                    lbErrorMessage.Text = "Please try again. Retries left: " + RetryCount.ToString();
                    tbInput.Text = "";
                    tbInput.Focus();

                    if (RetryCount == 1 && AllowRestorePassword)
                    {
                        btnRestorePassword.Enabled = true;
                        btnRestorePassword.Visible = true;
                    }
                    else if (RetryCount == 0)
                    {
                        AccessGranted = false;
                        this.Close();
                    }
                }
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

        private void btnRestorePassword_Click(object sender, EventArgs e)
        {
            btnRestorePassword.Enabled = false;
            btnRestorePassword.Visible = false;
            lbErrorMessage.Text = "";
            lbMessage.Text = "Answer this question: " + AuthQuestion;
            RestorePassword = true;
            RetryCount = 3;
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
