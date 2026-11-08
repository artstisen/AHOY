using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class AhoyAnswerMessageBox : Form
    {
        public bool Accepted { get; set; }
        public bool Canceled { get; set; }
        public bool ShowScrollBars { get; set; }
        public AhoyAnswerMessageBox(string textToDisplay, string formCaption, bool enableCancelButton, bool useStrongColors)
        {
            InitializeComponent();
            MainForm.KeepAwake();

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Get Main form opacity
            this.Opacity = MainForm.GetFormOpacity();

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;

            // Topmost
            this.TopMost = true;

            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;
            // Hide the control box
            this.ControlBox = false;
            // Show scrollbar
            this.ShowScrollBars = false;

            // Set button appearance
            btnYes.FlatStyle = FlatStyle.Flat;
            btnNo.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;

            btnYes.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnNo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnYes.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnNo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnYes.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnNo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            btnYes.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnNo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnYes.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnNo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            if (useStrongColors)
            {
                this.lbInformation.BackColor = AhoyColors.CurrentColorTheme()[6];
                this.lbInformation.ForeColor = AhoyColors.CurrentColorTheme()[1];
            }
            else
            {
                this.lbInformation.BackColor = AhoyColors.CurrentColorTheme()[1];
                this.lbInformation.ForeColor = AhoyColors.CurrentColorTheme()[3];
            }

            // Change font size depending on content length
            if (textToDisplay.Length > 24)
            {
                if (useStrongColors)
                {
                    this.lbInformation.Font = Design.MessageboxSmallFontBold();
                }
                else
                {
                    this.lbInformation.Font = Design.MessageboxSmallFont();
                }
            }
            else
            {
                if (useStrongColors)
                {
                    this.lbInformation.Font = Design.MessageboxLargeFontBold();
                }
                else
                {
                    this.lbInformation.Font = Design.MessageboxLargeFont();
                }
            }

            // Set text
            this.Text = formCaption;
            lbInformation.Text = textToDisplay;
            Accepted = false;
            Canceled = false;
            btnCancel.Visible = enableCancelButton;


        }

        public void CenterParent()
        {
            // Set start position
            this.StartPosition = FormStartPosition.CenterParent;
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        public void SetCustomButtonText(string cancelButtonText, string yesButtonText, string noButtonText, string formTitleText, string yesToolTipAddition, string noToolTipAddition, string cancelToolTipAddition, int buttonHighlightIndex)
        {
            btnYes.Text = yesButtonText;
            btnNo.Text = noButtonText;
            btnCancel.Text = cancelButtonText;
            this.Text = formTitleText;

            // if zero or above 3 set no colors
            // else highlight the corrosponding text
            switch (buttonHighlightIndex)
            {
                case 1:
                    btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
                case 2:
                    btnYes.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
                case 3:
                    btnNo.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
            }

            toolTip1.SetToolTip(this.btnYes, yesButtonText + " " + yesToolTipAddition);
            toolTip1.SetToolTip(this.btnNo, noButtonText + " " + noToolTipAddition);
            toolTip1.SetToolTip(this.btnCancel, cancelButtonText + " " + cancelToolTipAddition);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            // Accept
            Accepted = true;
            Canceled = false;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            // Decline
            Accepted = false;
            Canceled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel
            Accepted = false;
            Canceled = true;
            this.Close();
        }

        // Only works if KeyPreview is set to true
        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnCancel_Click(this, null);
            }
            else if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnYes_Click(this, null);
            }
            else if ((Keys)e.KeyChar == Keys.Y)
            {
                btnYes_Click(this, null);
                e.Handled = true;
            }
            else if ((Keys)e.KeyChar == Keys.N)
            {
                btnNo_Click(this, null);
                e.Handled = true;
            }
        }


    }
}
