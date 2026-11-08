using System;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class ThemeSelector : Form
    {
        public int SelectedColorTheme { get; set; }
        public ThemeSelector(bool advanced)
        {
            InitializeComponent();
            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;

            // Title
            this.Text = MessageText.ColorThemeCaption();

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;

            // Set theme
            SelectedColorTheme = 0;

            // TopMost
            this.TopMost = true;

            // Set pictureboxes
            picA.Image = Properties.Resources.themedark;
            picB.Image = Properties.Resources.themelight;
            picC.Image = Properties.Resources.themeblue;

            // Style form
            SetColorTheme();

            if (advanced)
            {
                lbAdditionalInfo.Visible = false;
                lbAdditionalInfo.Text = "";
                lbEscapeText.Visible = false;
                this.ShowIcon = true;
                this.ControlBox = true;
                btnCancel.Visible = true;
                btnMore.Visible = true;
            }
            else
            {
                lbAdditionalInfo.Visible = true;
                lbAdditionalInfo.Text = "Color themes can always be changed later in the View menu";
                lbEscapeText.Visible = true;
                this.ShowIcon = false;
                this.ControlBox = false;
                btnCancel.Visible = false;
                btnMore.Visible = false;
            }

            // Set text
            lbTitle.Text = "Please select Color Theme";
            btnSelectA.Focus();

            // Start color shift timer
            ToggleColorShiftTimer(true);
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void SetColorTheme()
        {
            // Tooltip
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Background
            this.BackColor = AhoyColors.CurrentColorTheme()[0];

            // Set button appearance
            btnSelectA.FlatStyle = FlatStyle.Flat;
            btnSelectA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSelectA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSelectA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSelectA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectA.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnSelectB.FlatStyle = FlatStyle.Flat;
            btnSelectB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSelectB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSelectB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSelectB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectB.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnSelectC.FlatStyle = FlatStyle.Flat;
            btnSelectC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSelectC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSelectC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSelectC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectC.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnMore.FlatStyle = FlatStyle.Flat;
            btnMore.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMore.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMore.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMore.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMore.ForeColor = AhoyColors.CurrentColorTheme()[6]; // Highlight the "More..." button

            // Set labels
            lbTitle.BackColor = Color.Transparent;
            lbTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbEscapeText.BackColor = Color.Transparent;
            lbEscapeText.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbAdditionalInfo.BackColor = Color.Transparent;
            lbAdditionalInfo.ForeColor = AhoyColors.CurrentColorTheme()[2];
        }

        private void btnSelectA_Click(object sender, EventArgs e)
        {
            SubmitSelection(0);
        }

        private void btnSelectB_Click(object sender, EventArgs e)
        {
            SubmitSelection(1);
        }

        private void btnSelectC_Click(object sender, EventArgs e)
        {
            SubmitSelection(15);
        }

        private void picA_Click(object sender, EventArgs e)
        {
            btnSelectA_Click(this, null);
        }

        private void picB_Click(object sender, EventArgs e)
        {
            btnSelectB_Click(this, null);
        }

        private void picC_Click(object sender, EventArgs e)
        {
            btnSelectC_Click(this, null);
        }

        private void SubmitSelection(int themeIndex)
        {
            if (AhoyColors.HasCustomColors())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Do you want to reset any custom colors before setting a new theme?", MessageText.ColorThemeCaption(), true, false);
                msg.TopMost = true;
                msg.ShowDialog();
                if (msg.Accepted)
                {
                    AhoyColors.ResetAllCustomColors();
                    AhoyColors.ResetAllCustomPreviewColors();
                }
                else if(msg.Canceled)
                {
                    return;
                }
            }

            SelectedColorTheme = themeIndex;
            Log.StyleChanged();
            AhoyColors.ColorTheme = SelectedColorTheme;
            MainForm.SelectedTexture = Settings.DefaultTexture;
            MainForm.PrismEffect = Settings.DefaultPrismEffect;
            MainForm.FormOpacity = Settings.DefaultOpacity;
            ButtonBitmaps.UserSelectedBitmapVersion = 0; // Always set the two theme selector themes to use standard home screen (ANSI)
            MainForm.DisplayTextCycle = 0;
            MainForm.StyleEditorHasChanges = true;
            this.Close();
        }

        private void ThemeSelector_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            // Hide form
            this.Visible = false;
            // And remote prompt Style Editor
            MainForm.OpenStyleEditorPerRemote();
            // Close form
            this.Close();
        }

        private void picA_MouseEnter(object sender, EventArgs e)
        {
            btnSelectA.BackColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectA.ForeColor = AhoyColors.CurrentColorTheme()[7];

            btnSelectB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectC.ForeColor = AhoyColors.CurrentColorTheme()[8];
        }

        private void picB_MouseEnter(object sender, EventArgs e)
        {
            btnSelectB.BackColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectB.ForeColor = AhoyColors.CurrentColorTheme()[7];

            btnSelectA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectC.ForeColor = AhoyColors.CurrentColorTheme()[8];
        }

        private void picC_MouseEnter(object sender, EventArgs e)
        {
            btnSelectC.BackColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectC.ForeColor = AhoyColors.CurrentColorTheme()[7];

            btnSelectA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSelectB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectB.ForeColor = AhoyColors.CurrentColorTheme()[8];
        }

        private void ThemeSelector_MouseMove(object sender, MouseEventArgs e)
        {
            ResetButtonColors();
        }

        private void ResetButtonColors()
        {
            btnSelectA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectA.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnSelectB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectB.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnSelectC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectC.ForeColor = AhoyColors.CurrentColorTheme()[8];
        }




        #region COLOR SHIFT (addition 04-01-2020)
        private void ToggleColorShiftTimer(bool enabled)
        {
            if (tempColorShiftTimer == null)
            {
                tempColorShiftTimer = new Timer();
                tempColorShiftTimer.Interval = 5;
                tempColorShiftTimer.Tick += TempColorShiftTimer_Tick;
            }

            if (enabled)
            {
                StartShiftTimer();
            }
            else
            {
                StopColorShiftTimer();
            }
        }

        //private Color GetColorShiftBackground()
        //{
        //    if (ShiftTimerBackgroundColor != null)
        //    {
        //        return ShiftTimerBackgroundColor;
        //    }
        //    else
        //    {
        //        return Color.Black;
        //    }
        //}

        //private Color GetColorShiftForeground()
        //{
        //    if (ShiftTimerForegroundColor != null)
        //    {
        //        return ShiftTimerBackgroundColor;
        //    }
        //    else
        //    {
        //        return Color.Black;
        //    }
        //}

        private void StartShiftTimer()
        {
            ShiftTimerCounter1 = ShiftTimerCounter1Default;
            ShiftTimerCounter2 = ShiftTimerCounter2Default;
            tempColorShiftTimer.Enabled = true;
        }

        private void StopColorShiftTimer()
        {
            tempColorShiftTimer.Enabled = false;
        }

        // Color shift timer
        private float ShiftTimerCounter1 = 0f;
        private float ShiftTimerCounter2 = 0f;
        private const float ShiftTimerCounter1Default = 0f;
        private const float ShiftTimerCounter2Default = 0f;
        private Color ShiftTimerBackgroundColor;
        private Color ShiftTimerForegroundColor;
        Timer tempColorShiftTimer;
        private void TempColorShiftTimer_Tick(object sender, EventArgs e)
        {
            ShiftTimerCounter1 += 0.001f;
            if (ShiftTimerCounter1 > 1)
            {
                ShiftTimerCounter1 = ShiftTimerCounter1Default;
            }

            ShiftTimerCounter2 += 0.002f;
            if (ShiftTimerCounter2 > 11)
            {
                ShiftTimerCounter2 = ShiftTimerCounter2Default;
            }

            ShiftTimerBackgroundColor = Rainbow(ShiftTimerCounter1);
            ShiftTimerForegroundColor = Rainbow(ShiftTimerCounter2);

            // Set colors
            btnMore.BackColor = ShiftTimerBackgroundColor;
            btnMore.ForeColor = ShiftTimerForegroundColor;
        }

        // Generate color from input
        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }
        #endregion
    }
}
