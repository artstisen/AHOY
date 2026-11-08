using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class AhoyInfoMessageBox : Form
    {
        public bool IsReadOnly { get; set; }
        public bool OptionA { get; set; }
        public AhoyInfoMessageBox(string textToDisplay, string formCaption, bool useStrongColors, bool readOnly, double opacity)
        {
            InitializeComponent();

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;
            tbInformation.DetectUrls = false;
            tbInformation.LinkClicked += TbInformation_LinkClicked;
            IsReadOnly = readOnly;

            // Register activity
            tbInformation.VScroll += TbInformation_VScroll;
            tbInformation.MouseWheel += TbInformation_MouseWheel;
            tbInformation.Click += TbInformation_Click;

            // OK button
            btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));

            // Hide option button
            btnToggleOptionA.Visible = false;
            btnToggleOptionA.Enabled = false;
            OptionA = false;

            // Set opacity etc.
            this.Opacity = opacity;
            // Topmost
            this.TopMost = true;
            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;
            // Hide the control box
            this.ControlBox = false;

            // Set button appearance
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnOK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnOK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnToggleOptionA.FlatStyle = FlatStyle.Flat;
            btnToggleOptionA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleOptionA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleOptionA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            btnOK.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnToggleOptionA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleOptionA.ForeColor = AhoyColors.CurrentColorTheme()[8];

            if (useStrongColors)
            {
                this.tbInformation.BackColor = AhoyColors.CurrentColorTheme()[6];
                this.tbInformation.ForeColor = AhoyColors.CurrentColorTheme()[1];
            }
            else
            {
                this.tbInformation.BackColor = AhoyColors.CurrentColorTheme()[1];
                this.tbInformation.ForeColor = AhoyColors.CurrentColorTheme()[3];
            }

            // Change font size depending on content length, if ReadOnly (If not ReadOnly the dialog is used as smal TextEditor)
            if (readOnly)
            {
                this.ControlBox = false;
                if (textToDisplay.Length > 24)
                {
                    this.tbInformation.Font = Design.MessageboxSmallFont();
                }
                else
                {
                    this.tbInformation.Font = Design.MessageboxLargeFont();
                }
            }
            else
            {
                // if not readonly set editor properties
                this.ControlBox = true;
                this.tbInformation.Font = Design.MessageboxLargeFont();
            }

            // Set Form caption
            this.Text = formCaption;
            // Set content text
            tbInformation.ReadOnly = false;
            tbInformation.SetText(textToDisplay, false, true, 0);
            tbInformation.Focus();
            tbInformation.ReadOnly = IsReadOnly;

            // Setup editor properties
            if (IsReadOnly)
            {
                this.AcceptButton = btnOK;
                tbInformation.ContextMenuStrip = contextMenuCopy;
            }
            else
            {
                this.AcceptButton = null;
                tbInformation.ContextMenuStrip = contextMenuBasic;
            }
        }

        #region Keep awake
        private void TbInformation_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
        }

        private void TbInformation_MouseWheel(object sender, MouseEventArgs e)
        {
            MainForm.KeepAwake();
        }

        private void TbInformation_VScroll(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
        }
        #endregion

        private void TbInformation_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                // Open URL
                // Open default browser and go to download page
                Log.ProcessStart(e.LinkText);
                ShellCommands.BrowseInternet(e.LinkText, MainForm.SelectedInternetBrowser);
            }
            catch
            {
                // Do nothing
            }
        }

        public void DetectUrls(bool detectUrls)
        {
            tbInformation.DetectUrls = detectUrls;
        }

        private string DisabledTitle = "";
        private string EnabledTitle = "";
        public void SetOptionA(string disabledTitle, string enabledTitle, bool state)
        {
            DisabledTitle = disabledTitle;
            EnabledTitle = enabledTitle;
            OptionA = state;

            if (!btnToggleOptionA.Visible)
            {
                btnToggleOptionA.Visible = true;
                btnToggleOptionA.Enabled = true;
                if (OptionA)
                {
                    btnToggleOptionA.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    btnToggleOptionA.Text = EnabledTitle;
                }
                else
                {
                    btnToggleOptionA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnToggleOptionA.Text = DisabledTitle;
                }
            }
        }

        private void btnToggleOptionA_Click(object sender, EventArgs e)
        {
            ToggleOptionA();
        }

        private void ToggleOptionA()
        {
            OptionA = !OptionA;
            if (OptionA)
            {
                btnToggleOptionA.ForeColor = AhoyColors.CurrentColorTheme()[6];
                btnToggleOptionA.Text = EnabledTitle;
            }
            else
            {
                btnToggleOptionA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnToggleOptionA.Text = DisabledTitle;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Only works if KeyPreview is set to true
        private void InfoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnOK_Click(this, null);
                e.Handled = true;
            }
            else if (IsReadOnly && (Keys)e.KeyChar == Keys.Enter)
            {
                btnOK_Click(this, null);
                e.Handled = true;
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }

        private void tbInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V) // Paste unformatted
            {
                tbInformation.PasteUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
            {
                tbInformation.CutUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
            {
                tbInformation.CopyUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
        }

    }
}
