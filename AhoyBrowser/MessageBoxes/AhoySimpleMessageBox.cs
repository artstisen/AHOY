using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class AhoySimpleMessageBox : Form
    {
        public AhoySimpleMessageBox(string textToDisplay, string formCaption, bool useStrongColors)
        {
            InitializeComponent();
            MainForm.KeepAwake();

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;

            // Topmost
            this.TopMost = true;

            // Get Main form opacity
            this.Opacity = MainForm.GetFormOpacity();

            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;
            // Hide the control box
            this.ControlBox = false;

            try
            {
                // Set button appearance
                btnOK.FlatStyle = FlatStyle.Flat;
                btnOK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
                btnOK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
                btnOK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

                // Set colors
                this.BackColor = AhoyColors.CurrentColorTheme()[0];
                btnOK.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];

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
                    this.lbInformation.Font = Design.MessageboxSmallFont();
                }
                else
                {
                    this.lbInformation.Font = Design.MessageboxLargeFont();
                }
            }
            catch
            { 
              // Do nothing (might be caught if already running ahoy and unable to load embedded resources)
            }

            // Set text
            this.Text = formCaption;
            lbInformation.Text = textToDisplay;
        }

        public void SetMicroFont()
        {
            this.lbInformation.Font = Design.StyleEditorXSSmallFont();
        }

        public void CenterParent()
        {
            // Set start position
            this.StartPosition = FormStartPosition.CenterParent;
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
                this.Close();
            }
        }
    }
}
