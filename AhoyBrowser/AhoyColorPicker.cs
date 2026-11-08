using System;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class AhoyColorPicker : Form
    {
        // Ahoy Color Picker v1.00 by LBS 23-01-2018
        // Ahoy Color Picker v1.01 by LBS 24-01-2018
        // Ahoy Color Picker v1.02 by LBS 25-01-2018
        // Ahoy Color Picker v1.03 by LBS 29-01-2018
        // Ahoy Color Picker v1.04 by LBS 06-02-2018
        // Ahoy Color Picker v1.05 by LBS 08-02-2018
        // Ahoy Color Picker v1.06 by LBS 23-02-2018
        // Ahoy Color Picker v1.07 by LBS 24-02-2018
        // Ahoy Color Picker v1.08 by LBS 26-02-2019
        // Ahoy Color Picker v1.09 by LBS 31-03-2019
        // Ahoy Color Picker v1.10 by LBS 23-02-2020

        // Public properties
        public bool Accepted { get; set; }
        public static string PrimaryColorHex { get; set; }
        public static Color PrimaryColorRGB { get; set; }
        public static string SecondaryColorHex { get; set; }
        public static Color SecondaryColorRGB { get; set; }
        // Private properties
        private static Color PrimaryGeneratedRGB { get; set; }
        private static Color SecondaryGeneratedRGB { get; set; }
        private static Color OriginalPrimary { get; set; }
        private static Color OriginalSecondary { get; set; }
        private bool DualColorMode { get; set; }
        private int ColorsSelectionCounter { get; set; }
        private string StandardInfoText { get; set; }
        private string PrimaryInfoText { get; set; }
        private string SecondaryInfoText { get; set; }
        private string[] DefaultInfoText = { "Color", "Background Color", "Text Color" };
        private bool LockHoverSelectionA { get; set; }
        private bool LockHoverSelectionB { get; set; }
        private bool OriginalColorWasChangedA { get; set; }
        private bool OriginalColorWasChangedB { get; set; }

        public AhoyColorPicker(bool dualColorMode, Color primaryColor, Color secondaryColor, string standardInfoTextOrCode, string primaryInfoTextOrCode, string secondaryInfoTextOrCode)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.Ahoy;
            this.ShowIcon = true;
            DualColorMode = dualColorMode;
            ColorsSelectionCounter = 0;
            this.KeyPreview = true;
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            StandardInfoText = standardInfoTextOrCode;
            PrimaryInfoText = primaryInfoTextOrCode;
            SecondaryInfoText = secondaryInfoTextOrCode;

            PrimaryColorRGB = new Color();
            SecondaryColorRGB = new Color();
            PrimaryGeneratedRGB = new Color();
            SecondaryGeneratedRGB = new Color();
            OriginalPrimary = new Color();
            OriginalSecondary = new Color();
            LockHoverSelectionA = false;
            LockHoverSelectionB = false;
            pnlSelectionAActive.SendToBack();
            pnlSelectionBActive.SendToBack();
            OriginalColorWasChangedA = false;
            OriginalColorWasChangedB = false;
            pnlAdjust.Visible = false;
            pnlAdjust.SendToBack();
            pnlHoverColor.BringToFront();

            OriginalPrimary = primaryColor;
            OriginalSecondary = secondaryColor;
            PrimaryColorRGB = OriginalPrimary;
            SecondaryColorRGB = OriginalSecondary;
            PrimaryGeneratedRGB = OriginalPrimary;
            SecondaryGeneratedRGB = OriginalSecondary;
            PrimaryColorHex = ConvertToHex(OriginalPrimary);
            SecondaryColorHex = ConvertToHex(OriginalSecondary);
            SetInitialColorValues();

            if (DualColorMode)
            {
                this.Text = "Color Picker (Dual Color)";
                tbHexValueB.Visible = true;
                btnFlip.Visible = true;
                lbInformation.Text = GetDefaultInfoText(PrimaryInfoText);
                toolTip1.SetToolTip(pnlPrimaryColorView, "Click to Select");
                toolTip1.SetToolTip(pnlSecondaryColorView, "Click to Select");
                pnlPrimaryColorView.Cursor = Cursors.Default;
                pnlSecondaryColorView.Cursor = Cursors.Default;
            }
            else
            {
                this.Text = "Color Picker (Single Color)";
                tbHexValueB.Visible = false;
                btnFlip.Visible = false;
                lbInformation.Text = GetDefaultInfoText(StandardInfoText);
                toolTip1.SetToolTip(pnlPrimaryColorView, "New Color");
                toolTip1.SetToolTip(pnlSecondaryColorView, "Original Color");
                pnlPrimaryColorView.Cursor = Cursors.Default;
                pnlSecondaryColorView.Cursor = Cursors.Default;
            }

            OmitTextChanged = false;
            Accepted = false;
            SetColorTheme();
            Log.LogColorPickerStatus("Open");
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private string GetDefaultInfoText(string input)
        {
            switch (input)
            {
                case "0":
                    return DefaultInfoText[0];
                case "1":
                    return DefaultInfoText[1];
                case "2":
                    return DefaultInfoText[2];
                case "3":
                    return DefaultInfoText[3];
                case "4":
                    return DefaultInfoText[4];
                default:
                    return input;
            }
        }

        // Set initial values
        private void SetInitialColorValues()
        {
            OmitTextChanged = true;
            if (DualColorMode)
            {
                pnlPrimaryColorView.BackColor = PrimaryGeneratedRGB;
                tbHexValueA.Text = ConvertToHex(PrimaryGeneratedRGB);
                pnlHoverColor.BackColor = pnlPrimaryColorView.BackColor;
                pnlSecondaryColorView.BackColor = SecondaryGeneratedRGB;
                tbHexValueB.Text = ConvertToHex(SecondaryGeneratedRGB);
            }
            else
            {
                pnlPrimaryColorView.BackColor = PrimaryGeneratedRGB;
                tbHexValueA.Text = ConvertToHex(PrimaryGeneratedRGB);
                pnlHoverColor.BackColor = pnlPrimaryColorView.BackColor;
                pnlSecondaryColorView.BackColor = PrimaryGeneratedRGB;
                tbHexValueB.Text = ConvertToHex(PrimaryGeneratedRGB);
            }
            UpdateSelectionIndicators();
            OmitTextChanged = false;
        }

        // The highlight bars beneath the hex code displayes/inputs
        private void UpdateSelectionIndicators()
        {
            if (ColorsSelectionCounter == 0)
            {
                pnlSelectionAActive.BackColor = AhoyColors.CurrentColorTheme()[6];
                pnlSelectionBActive.BackColor = this.BackColor;
            }
            else if (ColorsSelectionCounter == 1)
            {
                pnlSelectionAActive.BackColor = this.BackColor;
                pnlSelectionBActive.BackColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                pnlSelectionAActive.BackColor = this.BackColor;
                pnlSelectionBActive.BackColor = this.BackColor;
            }
        }

        #region Color Theme
        private void SetColorTheme()
        {
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbHexValueA.Font = Design.ContentFieldFont();
            tbHexValueA.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbHexValueA.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbHexValueB.Font = Design.ContentFieldFont();
            tbHexValueB.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbHexValueB.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbInformation.Font = Design.MessageboxTinyFont();
            lbInformation.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbInformation.ForeColor = AhoyColors.CurrentColorTheme()[30];
            pnlAdjust.BackColor = AhoyColors.CurrentColorTheme()[29];


            // Random button
            btnRandomColor.FlatStyle = FlatStyle.Flat;
            btnRandomColor.BackColor = trackBarRed.BackColor;
            btnRandomColor.ForeColor = lbRed.ForeColor;
            btnRandomColor.FlatAppearance.BorderColor = lbRed.ForeColor;
            btnRandomColor.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnRandomColor.FlatAppearance.MouseDownBackColor = Color.Red;

            // Ballance buttons
            btnBallanceMinusMore.FlatStyle = FlatStyle.Flat;
            btnBallanceMinus.FlatStyle = FlatStyle.Flat;
            btnBallancePlus.FlatStyle = FlatStyle.Flat;
            btnBallancePlusMore.FlatStyle = FlatStyle.Flat;

            // Back color
            btnBallanceMinusMore.BackColor = Color.FromArgb(255, 64, 64, 64);
            btnBallanceMinus.BackColor = Color.FromArgb(255, 128, 128, 128);
            btnBallancePlus.BackColor = Color.FromArgb(255, 192, 192, 192);
            btnBallancePlusMore.BackColor = Color.FromArgb(255, 224, 224, 224);

            // Border color
            btnBallanceMinusMore.FlatAppearance.BorderColor = Color.FromArgb(255, 64, 64, 64);
            btnBallanceMinus.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128, 128);
            btnBallancePlus.FlatAppearance.BorderColor = Color.FromArgb(255, 192, 192, 192);
            btnBallancePlusMore.FlatAppearance.BorderColor = Color.FromArgb(255, 224, 224, 224);

            // Fore color
            btnBallanceMinusMore.ForeColor = Color.FromArgb(255, 224, 224, 224);
            btnBallanceMinus.ForeColor = Color.FromArgb(255, 224, 224, 224);
            btnBallancePlus.ForeColor = Color.FromArgb(255, 64, 64, 64);
            btnBallancePlusMore.ForeColor = Color.FromArgb(255, 64, 64, 64);

            // Mouse over
            btnBallanceMinusMore.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 24, 24, 24);
            btnBallanceMinus.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 24, 24, 24);
            btnBallancePlus.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 24, 24, 24);
            btnBallancePlusMore.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 24, 24, 24);

            // Mouse down
            btnBallanceMinus.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 96, 96, 96);
            btnBallancePlus.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 96, 96, 96);
            btnBallanceMinusMore.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 96, 96, 96);
            btnBallancePlusMore.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 96, 96, 96);





            // Trackbar buttons
            btnRedMinus.FlatStyle = FlatStyle.Flat;
            btnAlignRed.FlatStyle = FlatStyle.Flat;
            btnCenterRed.FlatStyle = FlatStyle.Flat;
            btnRedPlus.FlatStyle = FlatStyle.Flat;
            btnGreenMinus.FlatStyle = FlatStyle.Flat;
            btnAlignGreen.FlatStyle = FlatStyle.Flat;
            btnCenterGreen.FlatStyle = FlatStyle.Flat;
            btnGreenPlus.FlatStyle = FlatStyle.Flat;
            btnBlueMinus.FlatStyle = FlatStyle.Flat;
            btnAlignBlue.FlatStyle = FlatStyle.Flat;
            btnCenterBlue.FlatStyle = FlatStyle.Flat;
            btnBluePlus.FlatStyle = FlatStyle.Flat;

            // Back color
            btnRedMinus.BackColor = trackBarRed.BackColor;
            btnAlignRed.BackColor = trackBarRed.BackColor;
            btnCenterRed.BackColor = trackBarRed.BackColor;
            btnRedPlus.BackColor = trackBarRed.BackColor;
            btnGreenMinus.BackColor = trackBarGreen.BackColor;
            btnAlignGreen.BackColor = trackBarGreen.BackColor;
            btnCenterGreen.BackColor = trackBarGreen.BackColor;
            btnGreenPlus.BackColor = trackBarGreen.BackColor;
            btnBlueMinus.BackColor = trackBarBlue.BackColor;
            btnAlignBlue.BackColor = trackBarBlue.BackColor;
            btnCenterBlue.BackColor = trackBarBlue.BackColor;
            btnBluePlus.BackColor = trackBarBlue.BackColor;

            // Forecolor
            btnRedMinus.ForeColor = lbRed.ForeColor;
            btnAlignRed.ForeColor = lbRed.ForeColor;
            btnCenterRed.ForeColor = lbRed.ForeColor;
            btnRedPlus.ForeColor = lbRed.ForeColor;
            btnGreenMinus.ForeColor = lbRed.ForeColor;
            btnAlignGreen.ForeColor = lbRed.ForeColor;
            btnCenterGreen.ForeColor = lbRed.ForeColor;
            btnGreenPlus.ForeColor = lbRed.ForeColor;
            btnBlueMinus.ForeColor = lbRed.ForeColor;
            btnAlignBlue.ForeColor = lbRed.ForeColor;
            btnCenterBlue.ForeColor = lbRed.ForeColor;
            btnBluePlus.ForeColor = lbRed.ForeColor;

            // Border color
            btnRedMinus.FlatAppearance.BorderColor = trackBarRed.BackColor;
            btnAlignRed.FlatAppearance.BorderColor = trackBarRed.BackColor;
            btnCenterRed.FlatAppearance.BorderColor = trackBarRed.BackColor;
            btnRedPlus.FlatAppearance.BorderColor = trackBarRed.BackColor;
            btnGreenMinus.FlatAppearance.BorderColor = trackBarGreen.BackColor;
            btnAlignGreen.FlatAppearance.BorderColor = trackBarGreen.BackColor;
            btnCenterGreen.FlatAppearance.BorderColor = trackBarGreen.BackColor;
            btnGreenPlus.FlatAppearance.BorderColor = trackBarGreen.BackColor;
            btnBlueMinus.FlatAppearance.BorderColor = trackBarBlue.BackColor;
            btnAlignBlue.FlatAppearance.BorderColor = trackBarBlue.BackColor;
            btnCenterBlue.FlatAppearance.BorderColor = trackBarBlue.BackColor;
            btnBluePlus.FlatAppearance.BorderColor = trackBarBlue.BackColor;

            // Mouse over
            btnRedMinus.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnAlignRed.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnCenterRed.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnRedPlus.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnGreenMinus.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            btnAlignGreen.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            btnCenterGreen.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            btnGreenPlus.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            btnBlueMinus.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            btnAlignBlue.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            btnCenterBlue.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            btnBluePlus.FlatAppearance.MouseOverBackColor = Color.DarkBlue;

            // Mouse down
            btnRedMinus.FlatAppearance.MouseDownBackColor = Color.Red;
            btnAlignRed.FlatAppearance.MouseDownBackColor = Color.Red;
            btnCenterRed.FlatAppearance.MouseDownBackColor = Color.Red;
            btnRedPlus.FlatAppearance.MouseDownBackColor = Color.Red;
            btnGreenMinus.FlatAppearance.MouseDownBackColor = Color.Green;
            btnAlignGreen.FlatAppearance.MouseDownBackColor = Color.Green;
            btnCenterGreen.FlatAppearance.MouseDownBackColor = Color.Green;
            btnGreenPlus.FlatAppearance.MouseDownBackColor = Color.Green;
            btnBlueMinus.FlatAppearance.MouseDownBackColor = Color.Blue;
            btnAlignBlue.FlatAppearance.MouseDownBackColor = Color.Blue;
            btnCenterBlue.FlatAppearance.MouseDownBackColor = Color.Blue;
            btnBluePlus.FlatAppearance.MouseDownBackColor = Color.Blue;



            // Preset
            btnPreset1.FlatStyle = FlatStyle.Flat;
            btnPreset2.FlatStyle = FlatStyle.Flat;
            btnPreset3.FlatStyle = FlatStyle.Flat;
            btnPreset4.FlatStyle = FlatStyle.Flat;
            btnPreset5.FlatStyle = FlatStyle.Flat;
            btnPreset6.FlatStyle = FlatStyle.Flat;
            // Border color
            btnPreset1.FlatAppearance.BorderColor = btnPreset1.BackColor;
            btnPreset2.FlatAppearance.BorderColor = btnPreset2.BackColor;
            btnPreset3.FlatAppearance.BorderColor = btnPreset3.BackColor;
            btnPreset4.FlatAppearance.BorderColor = btnPreset4.BackColor;
            btnPreset5.FlatAppearance.BorderColor = btnPreset5.BackColor;
            btnPreset6.FlatAppearance.BorderColor = btnPreset6.BackColor;
            // Mouse over
            btnPreset1.FlatAppearance.MouseOverBackColor = btnPreset1.BackColor;
            btnPreset2.FlatAppearance.MouseOverBackColor = btnPreset2.BackColor;
            btnPreset3.FlatAppearance.MouseOverBackColor = btnPreset3.BackColor;
            btnPreset4.FlatAppearance.MouseOverBackColor = btnPreset4.BackColor;
            btnPreset5.FlatAppearance.MouseOverBackColor = btnPreset5.BackColor;
            btnPreset6.FlatAppearance.MouseOverBackColor = btnPreset6.BackColor;
            // Mouse down
            btnPreset1.FlatAppearance.MouseDownBackColor = btnPreset1.BackColor;
            btnPreset2.FlatAppearance.MouseDownBackColor = btnPreset2.BackColor;
            btnPreset3.FlatAppearance.MouseDownBackColor = btnPreset3.BackColor;
            btnPreset4.FlatAppearance.MouseDownBackColor = btnPreset4.BackColor;
            btnPreset5.FlatAppearance.MouseDownBackColor = btnPreset5.BackColor;
            btnPreset6.FlatAppearance.MouseDownBackColor = btnPreset6.BackColor;




            // Continuing with the other buttons
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnFlip.FlatStyle = FlatStyle.Flat;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnAdjust.FlatStyle = FlatStyle.Flat;
            btnCloseAdjustPanel.FlatStyle = FlatStyle.Flat;


            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnOK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnFlip.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnReset.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnAdjust.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCloseAdjustPanel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];






            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnOK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnFlip.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnReset.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnAdjust.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCloseAdjustPanel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];





            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnOK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnFlip.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnReset.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnAdjust.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCloseAdjustPanel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];





            // Back color
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnOK.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnFlip.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnReset.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnAdjust.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCloseAdjustPanel.BackColor = AhoyColors.CurrentColorTheme()[7];

            // Fore color
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnFlip.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnReset.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnAdjust.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCloseAdjustPanel.ForeColor = AhoyColors.CurrentColorTheme()[8];



            btnFlip.Text = "";
            btnFlip.BackgroundImage = ButtonBitmaps.Flip();
            btnFlip.BackgroundImageLayout = ImageLayout.Center;

            btnReset.Text = "";
            btnReset.BackgroundImage = ButtonBitmaps.Refresh();
            btnReset.BackgroundImageLayout = ImageLayout.Center;

            btnAdjust.Text = "";
            btnAdjust.BackgroundImage = ButtonBitmaps.Adjust();
            btnAdjust.BackgroundImageLayout = ImageLayout.Center;

            btnCloseAdjustPanel.Text = "";
            btnCloseAdjustPanel.BackgroundImage = ButtonBitmaps.Clear();
            btnCloseAdjustPanel.BackgroundImageLayout = ImageLayout.Center;

            UpdateSelectionIndicators();
        }
        #endregion


        private void RemoveHoverLock()
        {
            LockHoverSelectionA = false; // Unlock hover color
            LockHoverSelectionB = false; // Unlock hover color
        }

        // All panel-clicks go through this
        private bool OmitTextChanged;
        private void panel1_Click(object sender, EventArgs e)
        {
            Reaction();
            RemoveHoverLock();
            if (DualColorMode)
            {
                if (ColorsSelectionCounter == 0)
                {
                    OriginalColorWasChangedA = true;
                    tbHexValueA.Text = ConvertToHex(((Panel)sender).BackColor);
                }
                else if (ColorsSelectionCounter == 1)
                {
                    OriginalColorWasChangedB = true;
                    tbHexValueB.Text = ConvertToHex(((Panel)sender).BackColor);
                    btnOK.ForeColor = AhoyColors.CurrentColorTheme()[28];
                }

                ColorsSelectionCounter += 1;
                if (ColorsSelectionCounter >= 3)
                {
                    //LockHoverSelectionA = true;
                    ColorsSelectionCounter = 0;
                    //OriginalColorWasChangedA = true;
                    tbHexValueA.Text = ConvertToHex(((Panel)sender).BackColor);
                    btnOK.ForeColor = ForeColor = AhoyColors.CurrentColorTheme()[8];
                }
            }
            else
            {
                LockHoverSelectionA = true;
                OriginalColorWasChangedA = true;
                tbHexValueA.Text = ConvertToHex(((Panel)sender).BackColor);
                btnOK.ForeColor = AhoyColors.CurrentColorTheme()[28];
            }

            // Always call
            UpdateInfoLabel();
            UpdateSelectionIndicators();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Reaction();
                UpdateInfoLabel();
                pnlHoverColor.BackColor = ((Panel)sender).BackColor;
            }
            catch
            {
                // Do nothing
            }
        }


        private void UpdateInfoLabel()
        {
            if (DualColorMode)
            {
                if (ColorsSelectionCounter == 0)
                {
                    lbInformation.Text = GetDefaultInfoText(PrimaryInfoText);
                }
                else if (ColorsSelectionCounter == 1)
                {
                    lbInformation.Text = GetDefaultInfoText(SecondaryInfoText);
                }
                else if (ColorsSelectionCounter == 2)
                {
                    lbInformation.Text = "Ready";
                }
            }
            else
            {
                lbInformation.Text = GetDefaultInfoText(StandardInfoText);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Reaction();
            Accepted = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reaction();
            Accepted = false;
            this.Close();
        }

        private void tbHexValue_Click(object sender, EventArgs e)
        {
            ManualFlipColorSelection(0);
            RestoreOriginalColors(1);
            tbHexValueA.Focus();
            tbHexValueA.SelectAll();
        }

        private void tbHexValueB_Click(object sender, EventArgs e)
        {
            ManualFlipColorSelection(1);
            RestoreOriginalColors(0);
            tbHexValueB.Focus();
            tbHexValueB.SelectAll();
        }

        private void tbHexValue_TextChanged(object sender, EventArgs e)
        {
            tbHexValueA.ClearFormatting();
            if (!OmitTextChanged) // Run only when inserting Hex value
            {
                OmitTextChanged = true;
                if (tbHexValueA.Text.Length == 7)
                {
                    PrimaryGeneratedRGB = ConvertFromHex(tbHexValueA.Text);
                    if (ConversionSuccess)
                    {
                        PrimaryColorHex = ConvertToHex(PrimaryGeneratedRGB);
                        pnlPrimaryColorView.BackColor = PrimaryGeneratedRGB;
                    }
                }
                OmitTextChanged = false;
            }
        }

        private void tbHexValueB_TextChanged(object sender, EventArgs e)
        {
            tbHexValueB.ClearFormatting();
            if (!OmitTextChanged)// Run only when inserting Hex value
            {
                OmitTextChanged = true;
                if (tbHexValueB.Text.Length == 7)
                {
                    SecondaryGeneratedRGB = ConvertFromHex(tbHexValueB.Text);
                    if (ConversionSuccess)
                    {
                        SecondaryColorHex = ConvertToHex(SecondaryGeneratedRGB);
                        pnlSecondaryColorView.BackColor = SecondaryGeneratedRGB;
                    }
                }
                OmitTextChanged = false;
            }
        }

        private void tbHexValueA_KeyDown(object sender, KeyEventArgs e)
        {
            // If pasting into control
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                if (IsHexValue(tbHexValueA.Text))
                {
                    OriginalColorWasChangedA = true;
                    LockHoverSelectionA = true;
                    ManualFlipColorSelection(0);
                }
            }
            else if (tbHexValueA.Text.Length >= 7)
            {
                if (IsHexValue(tbHexValueA.Text))
                {
                    OriginalColorWasChangedA = true;
                    LockHoverSelectionA = true;
                    ManualFlipColorSelection(0);
                }
            }
        }

        private void tbHexValueB_KeyDown(object sender, KeyEventArgs e)
        {
            // If pasting into control
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                if (IsHexValue(tbHexValueB.Text))
                {
                    OriginalColorWasChangedB = true;
                    LockHoverSelectionB = true;
                    ManualFlipColorSelection(1);
                }
            }
            else if (tbHexValueB.Text.Length >= 7)
            {
                if (IsHexValue(tbHexValueB.Text))
                {
                    OriginalColorWasChangedB = true;
                    LockHoverSelectionB = true;
                    ManualFlipColorSelection(1);
                }
            }
        }




        #region COLOR CONVERTERS (MODIFIED FOR PICKER)
        private bool IsHexValue(string input)
        {
            input = input.ToLower().Trim();
            if (System.Text.RegularExpressions.Regex.IsMatch(input, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ConversionSuccess;
        private Color ConvertFromHex(string hexString)
        {
            ConversionSuccess = false;
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(hexString, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
                {
                    ConversionSuccess = true;
                    int red = int.Parse(hexString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int green = int.Parse(hexString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    int blue = int.Parse(hexString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                    return Color.FromArgb(red, green, blue);
                }
                else
                {
                    ConversionSuccess = false;
                    Log.LogColorPickerStatus("HEX to RGB failed. Value: " + hexString);
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to convert HEX value to RGB.\nPlease revise your input", MessageText.ErrorCaption(), false);
                    msg.ShowDialog();
                    msg.TopMost = true;
                }
            }
            catch
            {
                // Do nothing
            }
            return Color.White;
        }

        private string tempColorHex;
        private Color tempColorRGB;
        private string ConvertToHex(Color color)
        {
            tempColorHex = "";
            ConversionSuccess = false;
            try
            {
                ConversionSuccess = true;
                //tempColorHex = ColorTranslator.ToHtml(color); <- Does not work properly makes #ffffff = "White"
                tempColorRGB = color;
                tempColorHex = "#" + tempColorRGB.R.ToString("X2") + tempColorRGB.G.ToString("X2") + tempColorRGB.B.ToString("X2");
                return tempColorHex;
            }
            catch
            {
                ConversionSuccess = false;
                AhoyInfoMessageBox msg = new AhoyInfoMessageBox("Unable to convert RGB value to HEX!", MessageText.ErrorCaption(), false, true, this.Opacity);
                msg.TopMost = true;
                msg.ShowDialog();
                return "";
            }
        }
        #endregion

        private void AhoyColorPicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do nothing
        }

        private void AhoyColorPicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (IsAdjustPanelVisible())
                {
                    HideAdjustPanel();
                }
                else
                {
                    btnCancel_Click(this, null);
                }
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.X)
            {
                btnFlip_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                BtnReset_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                btnAdjust_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                btnRandomColor_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void Reaction()
        {
            MainForm.KeepAwake();
        }

        private void lbInformation_Click(object sender, EventArgs e)
        {
            FlipColorSelection();
        }

        private void pnlSecondaryColorView_Click(object sender, EventArgs e)
        {
            // If already selected, open adjustment panel
            if (ColorsSelectionCounter == 1)
            {
                ToggleAdjustPanel();
            }
            else
            {
                ManualFlipColorSelection(1);
            }
        }

        private void pnlPrimaryColorView_Click(object sender, EventArgs e)
        {
            // If already selected, open adjustment panel
            if (ColorsSelectionCounter == 0)
            {
                ToggleAdjustPanel();
            }
            else
            {
                ManualFlipColorSelection(0);
            }
        }

        private void RestoreOriginalColors(int index)
        {
            //Ensure original colors are shown if they wasn't changed
            OmitTextChanged = true;
            if (index == 0)
            {
                if (!OriginalColorWasChangedA)
                {
                    tbHexValueA.Text = ConvertToHex(OriginalPrimary);
                    pnlPrimaryColorView.BackColor = OriginalPrimary;
                }
            }
            else if (index == 1)
            {
                if (!OriginalColorWasChangedB)
                {
                    tbHexValueB.Text = ConvertToHex(OriginalSecondary);
                    pnlSecondaryColorView.BackColor = OriginalSecondary;
                }
            }
            OmitTextChanged = false;
        }

        private void ManualFlipColorSelection(int index)
        {
            if (DualColorMode && index != ColorsSelectionCounter)
            {
                ColorsSelectionCounter = index;
            }
            UpdateInfoLabel();
            UpdateSelectionIndicators();
            UpdateAdjustTrackers();
        }

        private void FlipColorSelection()
        {
            RemoveHoverLock();
            if (DualColorMode)
            {
                if (ColorsSelectionCounter == 0)
                {
                    ColorsSelectionCounter = 1;
                }
                else if (ColorsSelectionCounter == 1)
                {
                    ColorsSelectionCounter = 0;
                }
                else if (ColorsSelectionCounter == 2)
                {
                    ColorsSelectionCounter = 0;
                }

                UpdateInfoLabel();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Only available in dual color mode", MessageText.SelectionCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
            UpdateSelectionIndicators();
            UpdateAdjustTrackers();
        }

        // Flip color
        private string tempFlip;
        private void btnFlip_Click(object sender, EventArgs e)
        {
            if (DualColorMode && (!string.IsNullOrEmpty(tbHexValueA.Text) && !string.IsNullOrEmpty(tbHexValueB.Text)))
            {
                tempFlip = tbHexValueA.Text;
                tbHexValueA.Text = tbHexValueB.Text;
                tbHexValueB.Text = tempFlip;
                OriginalColorWasChangedA = true;
                OriginalColorWasChangedB = true;
            }
            UpdateAdjustTrackers();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (DualColorMode && (OriginalPrimary != null && OriginalSecondary != null))
            {
                tbHexValueA.Text = ConvertToHex(OriginalPrimary);
                tbHexValueB.Text = ConvertToHex(OriginalSecondary);
            }
            else if (!DualColorMode && OriginalPrimary != null)
            {
                tbHexValueA.Text = ConvertToHex(OriginalPrimary);
            }
            UpdateAdjustTrackers();
        }



        #region Color adjust (addition 23-02-2020)

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            ToggleAdjustPanel();
        }


        private void ToggleAdjustPanel()
        {
            if (!IsAdjustPanelVisible())
            {
                ShowAdjustPanel();
            }
            else
            {
                HideAdjustPanel();
            }
        }


        private void ShowAdjustPanel()
        {
            if (!IsAdjustPanelVisible())
            {
                if (ColorsSelectionCounter > 1)
                {
                    ManualFlipColorSelection(0);
                }

                pnlAdjust.Visible = true;
                pnlAdjust.BringToFront();
                UpdateAdjustTrackers();
            }
        }
        private void HideAdjustPanel()
        {
            if (IsAdjustPanelVisible())
            {
                pnlAdjust.Visible = false;
                pnlAdjust.SendToBack();
            }
        }
        private bool IsAdjustPanelVisible()
        {
            if (pnlAdjust.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void UpdateRGBDisplayValues()
        {
            // Set labels to the RGB values
            lbRed.Text = GetColorFromTrackerValues().R.ToString("000");
            lbGreen.Text = GetColorFromTrackerValues().G.ToString("000");
            lbBlue.Text = GetColorFromTrackerValues().B.ToString("000");
        }

        private Color PrimaryAdjustColor;
        private Color SecondaryAdjustColor;

        private void UpdateAdjustTrackers()
        {
            if (IsAdjustPanelVisible())
            {
                PrimaryAdjustColor = pnlPrimaryColorView.BackColor;
                SecondaryAdjustColor = pnlSecondaryColorView.BackColor;

                if (DualColorMode)
                {
                    if (ColorsSelectionCounter == 0)  // Primary
                    {
                        trackBarRed.Value = PrimaryAdjustColor.R;
                        trackBarGreen.Value = PrimaryAdjustColor.G;
                        trackBarBlue.Value = PrimaryAdjustColor.B;
                        pnlAdjust.BackColor = PrimaryAdjustColor;

                    }
                    else if (ColorsSelectionCounter == 1) // Secondary
                    {
                        trackBarRed.Value = SecondaryAdjustColor.R;
                        trackBarGreen.Value = SecondaryAdjustColor.G;
                        trackBarBlue.Value = SecondaryAdjustColor.B;
                        pnlAdjust.BackColor = SecondaryAdjustColor;
                    }
                }
                else
                {
                    trackBarRed.Value = PrimaryAdjustColor.R;
                    trackBarGreen.Value = PrimaryAdjustColor.G;
                    trackBarBlue.Value = PrimaryAdjustColor.B;
                    pnlAdjust.BackColor = PrimaryAdjustColor;
                }
            }

            UpdateRGBDisplayValues();
        }

        private void btnCloseAdjustPanel_Click(object sender, EventArgs e)
        {
            HideAdjustPanel();
        }

        private void trackBarRed_Scroll(object sender, EventArgs e)
        {
            UpdateAdjustment();
        }

        private void trackBarGreen_Scroll(object sender, EventArgs e)
        {
            UpdateAdjustment();
        }

        private void trackBarBlue_Scroll(object sender, EventArgs e)
        {
            UpdateAdjustment();
        }

        private Color GetColorFromTrackerValues()
        {
            return Color.FromArgb(255, trackBarRed.Value, trackBarGreen.Value, trackBarBlue.Value);
        }

        private void UpdateAdjustment()
        {
            if (DualColorMode)
            {
                if (ColorsSelectionCounter == 0)
                {
                    OriginalColorWasChangedA = true;
                    tbHexValueA.Text = ConvertToHex(GetColorFromTrackerValues());
                    ;
                }
                else if (ColorsSelectionCounter == 1)
                {
                    OriginalColorWasChangedB = true;
                    tbHexValueB.Text = ConvertToHex(GetColorFromTrackerValues());
                }
            }
            else
            {
                LockHoverSelectionA = true;
                OriginalColorWasChangedA = true;
                tbHexValueA.Text = ConvertToHex(GetColorFromTrackerValues());
            }

            // Set adjustment panel to color selection
            pnlAdjust.BackColor = GetColorFromTrackerValues();
            // Set labels to the RGB values
            lbRed.Text = GetColorFromTrackerValues().R.ToString("000");
            lbGreen.Text = GetColorFromTrackerValues().G.ToString("000");
            lbBlue.Text = GetColorFromTrackerValues().B.ToString("000");

            // Always call
            UpdateInfoLabel();
            UpdateSelectionIndicators();
        }


        private void SetTrackbarValue(int index, int value)
        {
            switch (index)
            {
                case 0:
                    trackBarRed.Value = value;
                    UpdateAdjustment();
                    break;
                case 1:
                    trackBarGreen.Value = value;
                    UpdateAdjustment();
                    break;
                case 2:
                    trackBarBlue.Value = value;
                    UpdateAdjustment();
                    break;
            }
        }


        // Trackbar buttons
        private void btnRedMinus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, 0);
        }
        private void btnRedPlus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, 255);
        }
        private void btnAlignRed_Click(object sender, EventArgs e)
        {
            trackBarGreen.Value = trackBarRed.Value;
            trackBarBlue.Value = trackBarRed.Value;
            UpdateAdjustment();
        }
        private void btnCenterRed_Click(object sender, EventArgs e)
        {
            trackBarRed.Value = 127;
            UpdateAdjustment();
        }

        private void btnGreenMinus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(1, 0);
        }
        private void btnGreenPlus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(1, 255);
        }
        private void btnAlignGreen_Click(object sender, EventArgs e)
        {
            trackBarRed.Value = trackBarGreen.Value;
            trackBarBlue.Value = trackBarGreen.Value;
            UpdateAdjustment();
        }
        private void btnCenterGreen_Click(object sender, EventArgs e)
        {
            trackBarGreen.Value = 127;
            UpdateAdjustment();
        }

        private void btnBlueMinus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(2, 0);
        }
        private void btnBluePlus_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(2, 255);
        }
        private void btnAlignBlue_Click(object sender, EventArgs e)
        {
            trackBarRed.Value = trackBarBlue.Value;
            trackBarGreen.Value = trackBarBlue.Value;
            UpdateAdjustment();
        }
        private void btnCenterBlue_Click(object sender, EventArgs e)
        {
            trackBarBlue.Value = 127;
            UpdateAdjustment();
        }

        private void btnBallanceMinus_Click(object sender, EventArgs e)
        {
            ChangeBallance(-1);
        }

        private void btnBallancePlus_Click(object sender, EventArgs e)
        {
            ChangeBallance(1);
        }

        private void btnBallanceMinusMore_Click(object sender, EventArgs e)
        {
            ChangeBallance(-10);
        }

        private void btnBallancePlusMore_Click(object sender, EventArgs e)
        {
            ChangeBallance(10);
        }
        private void ChangeBallance(int value)
        {
            trackBarRed.Value = GetValueWithinTrackBarLimit(0, value);
            trackBarGreen.Value = GetValueWithinTrackBarLimit(1, value);
            trackBarBlue.Value = GetValueWithinTrackBarLimit(2, value);
            UpdateAdjustment();
        }

        private int GetValueWithinTrackBarLimit(int index, int value)
        {
            if (index == 0)
            {
                return FlattenValue(trackBarRed.Value + value);
            }
            else if (index == 1)
            {
                return FlattenValue(trackBarGreen.Value + value);
            }
            else if (index == 2)
            {
                return FlattenValue(trackBarBlue.Value + value);
            }
            else
            {
                return 0;
            }
        }
        private int FlattenValue(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            else if (value > 255)
            {
                return 255;
            }
            else
            {
                return value;
            }
        }

        private void btnRandomColor_Click(object sender, EventArgs e)
        {
            SetRandomColor();
        }

        private static Random rand;

        private void SetRandomColor()
        {
            if (rand == null)
            {
                rand = new Random();
            }

            SetTrackbarValue(0, rand.Next(0, 255));
            SetTrackbarValue(1, rand.Next(0, 255));
            SetTrackbarValue(2, rand.Next(0, 255));
        }


        //private int MemoryR;
        //private int MemoryG;
        //private int MemoryB;
        //private void SetMemory()
        //{
        //    MemoryR = trackBarRed.Value;
        //    MemoryG = trackBarRed.Value;
        //    MemoryB = trackBarRed.Value;
        //}


        // Preset select
        private void btnPreset1_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset1.BackColor.R);
            SetTrackbarValue(1, btnPreset1.BackColor.G);
            SetTrackbarValue(2, btnPreset1.BackColor.B);
        }
        private void btnPreset2_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset2.BackColor.R);
            SetTrackbarValue(1, btnPreset2.BackColor.G);
            SetTrackbarValue(2, btnPreset2.BackColor.B);
        }

        private void btnPreset3_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset3.BackColor.R);
            SetTrackbarValue(1, btnPreset3.BackColor.G);
            SetTrackbarValue(2, btnPreset3.BackColor.B);
        }

        private void btnPreset4_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset4.BackColor.R);
            SetTrackbarValue(1, btnPreset4.BackColor.G);
            SetTrackbarValue(2, btnPreset4.BackColor.B);
        }

        private void btnPreset5_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset5.BackColor.R);
            SetTrackbarValue(1, btnPreset5.BackColor.G);
            SetTrackbarValue(2, btnPreset5.BackColor.B);
        }
        private void btnPreset6_Click(object sender, EventArgs e)
        {
            SetTrackbarValue(0, btnPreset6.BackColor.R);
            SetTrackbarValue(1, btnPreset6.BackColor.G);
            SetTrackbarValue(2, btnPreset6.BackColor.B);
        }

        #endregion

        private void pnlAdjust_Click(object sender, EventArgs e)
        {
            HideAdjustPanel();
        }
    }
}
