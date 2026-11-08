using System.Windows.Forms;
using AhoyBrowser.Crypto;

namespace AhoyBrowser
{
    /// <summary>
    /// Create (or change) the pool password. The dialog states which encryption is used and that a
    /// forgotten password cannot be recovered; the user has to acknowledge this before saving.
    /// </summary>
    public partial class CreatePassword : Form
    {
        internal string AuthPassword { get; set; }
        private string ErrorMessage { get; set; }

        internal static string EncryptionNotice()
        {
            return "Encryption: " + PoolFile.AlgorithmDescription + "\n" +
                   "Key derivation: " + PoolFile.KdfDescription + "\n\n" +
                   "Your password is the only key to your notes. Ahoy does not store it and " +
                   "cannot reset it. If you forget it, your data cannot be recovered.";
        }

        public CreatePassword(string welcomeMessage, string formCaption)
        {
            InitializeComponent();
            AuthPassword = "";

            // Hide the control box
            this.ControlBox = false;

            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;

            // Set button appearance
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSave.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSave.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set help button
            SetupHelpPictureBox();

            // Setup context menu
            tbPassword1.ContextMenuStrip = contextMenuBasic;
            tbPassword2.ContextMenuStrip = contextMenuBasic;

            // Set fonts
            tbPassword1.Font = Design.PasswordFieldFont();
            tbPassword2.Font = Design.PasswordFieldFont();
            lbStrength.Font = Design.MessageboxXSmallFont();

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbMessage.BackColor = this.BackColor;
            lbMessage.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbErrorMessage.BackColor = this.BackColor;
            lbErrorMessage.ForeColor = AhoyColors.ErrorTextColor();
            label1.ForeColor = AhoyColors.CurrentColorTheme()[2];
            label2.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbWarning.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbWarning.BackColor = this.BackColor;
            cbUnderstand.ForeColor = AhoyColors.CurrentColorTheme()[2];
            cbUnderstand.BackColor = this.BackColor;
            lbEscapeText.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbEscapeText.BackColor = this.BackColor;
            tbPassword1.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbPassword1.ForeColor = AhoyColors.CurrentColorTheme()[3];
            tbPassword2.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbPassword2.ForeColor = AhoyColors.CurrentColorTheme()[3];
            btnSave.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSave.ForeColor = AhoyColors.CurrentColorTheme()[8];
            lbStrength.BackColor = this.BackColor;
            lbStrength.ForeColor = AhoyColors.CurrentColorTheme()[2];

            this.Text = formCaption;
            lbMessage.Text = welcomeMessage;
            label1.Text = "Enter password";
            label2.Text = "Please repeat password";
            lbWarning.Text = EncryptionNotice();

            // Hook events for highlighting save button
            tbPassword1.TextChanged += TbPassword1_TextChanged;
            tbPassword2.TextChanged += General_TextChanged;
            cbUnderstand.CheckedChanged += General_TextChanged;
            TestStrength();
            // Set max password length
            tbPassword1.MaxLength = MainForm.MaxPasswordLength;
            tbPassword2.MaxLength = MainForm.MaxPasswordLength;
        }

        private void SetupHelpPictureBox()
        {
            picHelp.BackColor = System.Drawing.Color.Transparent;
            picHelp.BackgroundImage = ButtonBitmaps.HelpButtonHighlight();
            picHelp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void TestStrength()
        {
            lbStrength.Text = PasswordTester.GetPasswordStrength(tbPassword1.Text, true);
        }

        private void TbPassword1_TextChanged(object sender, System.EventArgs e)
        {
            if (tbPassword1.Text.Length == tbPassword1.MaxLength)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Maximum length is " + tbPassword1.MaxLength.ToString() + " chars", MessageText.PasswordCaption(), false);
                msg.ShowDialog();
            }

            TestStrength(); // Test password strength
            General_TextChanged(sender, e);
        }

        private void General_TextChanged(object sender, System.EventArgs e)
        {
            btnSave.ForeColor = ReadyToSave()
                ? AhoyColors.CurrentColorTheme()[6]
                : AhoyColors.CurrentColorTheme()[8];
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // Validate input before submitting
            lbErrorMessage.Text = "";
            ErrorMessage = "";

            if (FieldsMissingText())
            {
                ErrorMessage = "Please enter the password twice";
            }
            else if (tbPassword1.Text.Trim() != tbPassword2.Text.Trim())
            {
                ErrorMessage = "Passwords do not match!";
                tbPassword1.Text = "";
                tbPassword2.Text = "";
                tbPassword1.Focus();
            }
            else if (!PasswordTester.IsScoreValid())
            {
                ErrorMessage = "Password is too weak";
                tbPassword1.Text = "";
                tbPassword2.Text = "";
                tbPassword1.Focus();
            }
            else if (!cbUnderstand.Checked)
            {
                ErrorMessage = "Please tick the box to confirm";
                cbUnderstand.Focus();
            }
            else
            {
                AuthPassword = tbPassword1.Text.Trim();
                this.Close();
            }

            lbErrorMessage.Text = ErrorMessage;
        }

        private void tbPassword1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnSave_Click(this, null);
                e.Handled = true;
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

        private bool FieldsMissingText()
        {
            return string.IsNullOrEmpty(tbPassword1.Text.Trim()) || string.IsNullOrEmpty(tbPassword2.Text.Trim());
        }

        private bool ReadyToSave()
        {
            return !FieldsMissingText() && cbUnderstand.Checked;
        }

        // Context menu actions
        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }

        // Select all when clicking password fields
        private void tbPassword1_Click(object sender, System.EventArgs e)
        {
            if (tbPassword1.Text.Length > 0)
            {
                tbPassword1.SelectAll();
            }
        }

        private void tbPassword2_Click(object sender, System.EventArgs e)
        {
            if (tbPassword2.Text.Length > 0)
            {
                tbPassword2.SelectAll();
            }
        }

        private void CreatePassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                CancelForm();
                e.Handled = true;
            }
        }

        private void CancelForm()
        {
            AuthPassword = "";
            this.Close();
        }

        private void picHelp_Click(object sender, System.EventArgs e)
        {
            AhoySearchMessageBox msg = new AhoySearchMessageBox(IO.PasswordInfoTextReader(), MessageText.InformationCaption(), false, true, this.Opacity);
            msg.ShowDialog();
        }
    }
}
