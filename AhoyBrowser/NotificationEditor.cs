using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace AhoyBrowser
{
    public partial class NotificationEditor : Form
    {
        public bool NotificationEnabled { get; set; }
        public DateTime NotificationDateTime { get; set; }
        private int CaretPosition { get; set; }
        private int NotificationCaretPosition { get; set; }
        private bool IgnoreTextChanged { get; set; }
        private string TitleText { get; set; }
        private string ContentText { get; set; }
        private string NotificationText { get; set; }
        private string NotificationID { get; set; }
        private bool AlarmRaised { get; set; }
        private bool AlarmWasRaised { get; set; }
        private bool TextInputLock { get; set; }
        private int SnoozeInterval { get; set; }
        private int SnoozeMinutes { get; set; }
        private bool AutoScrollToCaret { get; set; }
        private bool RepeatNotification { get; set; }
        private bool RepeatWeekly { get; set; }
        private string RepeatData { get; set; }
        private bool ConfirmRepetition { get; set; }

        // Constructor
        public NotificationEditor(Entry entry, bool detectURLs, int snoozeInterval, bool insertTitleAsMessage)
        {
            InitializeComponent();
            btnDisableRepeat.Text = "OFF";
            btnEnableRepeat.Text = "ON";

            tbMessage.ScrollBars = RichTextBoxScrollBars.Vertical;
            AutoScrollToCaret = MainForm.NotificationScrollToCaret;
            RepeatNotification = false;
            RepeatWeekly = false;
            pnlRepeat.Visible = false;
            pnlRepeat.SendToBack();
            RepeatData = "";
            Notification.ResetRepeatCode();
            ConfirmRepetition = false;

            // Align Date Time Ansi
            lbDateTimeAnsi.Width = tbMessage.Width;
            lbDateTimeAnsi.Left = tbMessage.Left;

            // Align text
            //tbMessage.SelectAll();
            //tbMessage.SelectionAlignment = HorizontalAlignment.Center;
            //tbMessage.DeselectAll();

            SetDateTimeNow();
            NotificationID = entry.ID;
            TitleText = entry.Title;
            ContentText = entry.Content;
            CaretPosition = entry.CaretPosition;
            NotificationCaretPosition = entry.NotificationCaretPosition;

            // Set Snooze interval
            SnoozeInterval = snoozeInterval;
            SnoozeMinutes = 10;
            // Resolve Snooze button
            ResolveSnoozeInterval();
            if (insertTitleAsMessage)
            {
                NotificationText = entry.Title;
            }
            else
            {
                NotificationText = entry.NotificationText;
            }
            AlarmRaised = false;
            MainForm.NotificationRaised = AlarmRaised;
            AlarmWasRaised = false;
            // Opacity 100%
            this.Opacity = 1.0d;

            // Show above all else
            this.TopMost = true;

            // Set DateTime display fonts
            lbYear.Font = Design.NotificationEditorDateTimeDisplay();
            lbMonth.Font = Design.NotificationEditorDateTimeDisplay();
            lbDay.Font = Design.NotificationEditorDateTimeDisplay();
            lbHour.Font = Design.NotificationEditorDateTimeDisplay();
            lbMinute.Font = Design.NotificationEditorDateTimeDisplay();
            lbTimeDivider.Font = Design.NotificationEditorDateTimeDisplay();

            lbMonthName.Font = Design.MessageboxSmallFont();
            lbDayName.Font = Design.MessageboxSmallFont();
            lbIsoWeekNumber.Font = Design.MessageboxSmallFont();

            // Enable if Notification is present
            if (!string.IsNullOrEmpty(entry.NotificationTimeStamp))
            {
                NotificationEnabled = true;
                NotificationDateTime = Convert.ToDateTime(entry.NotificationTimeStamp);
            }
            else
            {
                NotificationEnabled = false;
                NotificationDateTime = DateTime.Now;
            }

            // Setup tbMessage
            tbMessage.Font = Design.ContentFieldFont();
            IgnoreTextChanged = true;
            tbMessage.SetText(NotificationText, false);
            IgnoreTextChanged = false;
            tbMessage.DetectUrls = detectURLs;
            tbMessage.LinkClicked += TbMessage_LinkClicked;

            // Update color theme and controls
            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];
            DisplayCurrentDateTime();
            timerSystemTime_Tick(this, null); // Invoke timer immediately
            timerSystemTime.Enabled = true;
            SetColorTheme();

            // Enable Repeat if string is present
            if (Notification.ValidPrepeatData(entry.NotificationRepeat))
            {
                RepeatData = entry.NotificationRepeat;
                RepeatWeekly = Notification.IsWeekly(RepeatData);
                Notification.SetRepeatCode(RepeatData);
                RepeatNotification = true;
                //NotificationDateTime = Notification.GetRepeatDataAsDateTime(RepeatData);
                ResolveRepeatVsStandard();
                ResolveRepeatButton();
                ResolveRepeatCodeButtonColors();
                SetRepeatButtonTexts();
            }

            UpdateControls(true);
            // Accept tabs
            tbMessage.AcceptsTab = true;
            SetRepeatButtonTexts();
        }

        private void TbMessage_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ShellCommands.BrowseInternet(e.LinkText, MainForm.SelectedInternetBrowser);
        }

        // Must only be used for display (log etc.) and not for date/time calculations
        static Match match;
        internal static string GetTruncatedTimestamp(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input))
                {

                    // Match ISO datetime
                    match = Regex.Match(input, @"[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        return input.Substring(0, input.Length - 3);
                    }
                    else
                    {
                        return "N/A";
                    }
                }
                else
                {
                    return "N/A";

                }
            }
            catch
            {
                return "N/A";
            }
        }

        private void SetTextInputLock(bool readOnly)
        {
            // Set input as read-only
            tbMessage.ReadOnly = readOnly;
            TextInputLock = readOnly;
        }

        // Set datetime now, with Zero seconds
        private static TimeSpan time; // This variable is used further down the code
        private void SetDateTimeNow()
        {
            time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            NotificationDateTime = NotificationDateTime.Date + time;
        }

        // Set Date Time ANSI
        private void SetDateTimeAnsi()
        {
            lbDateTimeAnsi.Text = AnsiCalendar.GetCalendar(NotificationDateTime, true);
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        // Update controls
        private void UpdateControls(bool focusInput)
        {
            MainForm.KeepAwake();
            // Date Time ANSI
            SetDateTimeAnsi();

            // Set repeat data (Do not update if locked for repeat)
            if (!RepeatNotification && pnlRepeat.Visible)
            {
                UpdateRepeatPanelText();
            }

            // Display notification date and time
            lbYear.Text = NotificationDateTime.Year.ToString("0000");
            lbMonth.Text = NotificationDateTime.Month.ToString("00");

            // If date is today
            if (NotificationDateTime.Date == DateTime.Now.Date)
            {
                lbMonthName.Text = "";
                lbDayName.Text = "[TODAY]";
            }
            else if (NotificationDateTime.Date == DateTime.Now.Date.AddDays(1))
            {
                lbMonthName.Text = "";
                lbDayName.Text = "[TOMORROW]";
            }
            else
            {
                lbMonthName.Text = NotificationDateTime.ToString("MMMM", new CultureInfo("en-GB"));
                lbDayName.Text = NotificationDateTime.ToString("dddd", new CultureInfo("en-GB"));
            }

            // Overdue dates make Date Time display colored
            if (NotificationDateTime.Date <= DateTime.Now.Date.AddDays(-1))
            {
                SetDateTimeColorPanel(6);
            }
            else
            {
                ResetDateTimeColorPanel();
            }

            lbDay.Text = NotificationDateTime.Day.ToString("00");
            lbHour.Text = NotificationDateTime.Hour.ToString("00");
            lbMinute.Text = NotificationDateTime.Minute.ToString("00");
            lbIsoWeekNumber.Text = "(ISO Weeknumber: " + IsoWeekNumber.GetISO8601WeekNumber(NotificationDateTime).ToString("00") + ")";

            ResolveStateButton();

            // Focus input
            if (focusInput)
            {
                FocusInput();
            }
        }




        private void ResolveStateButton()
        {
            if (AlarmRaised && RepeatNotification)
            {
                // Set toggle button
                ResolveTitleLabel();
                btnToggleState.Text = "CONFIRM";
                btnClose.Text = "Close";
                ConfirmRepetition = true;
            }
            else if (!AlarmRaised && RepeatNotification)
            {
                // Set toggle button
                ResolveTitleLabel();
                btnToggleState.Text = "REPEATING";
                btnClose.Text = "Close";
            }
            else if ((NotificationEnabled && RepeatNotification) || (NotificationEnabled && !RepeatNotification))
            {
                // Set toggle button
                ResolveTitleLabel();
                btnToggleState.Text = "ACTIVE (ON)";
                btnClose.Text = "Submit";
            }
            else
            {
                // Set toggle button
                ResolveTitleLabel();
                btnToggleState.Text = "INACTIVE (OFF)";
                btnClose.Text = "Close";
            }
            ResolveButtonColors();
        }

        private void ResolveTitleLabel()
        {
            if ((NotificationEnabled || RepeatNotification) && !IsDue())
            {
                lbTitle.Text = "Message (Active Notification)";
            }
            else if (!IsDue())
            {
                lbTitle.Text = "Message";
            }
        }

        private void FocusInput()
        {
            timerReFocus.Enabled = true;
        }
        private void timerReFocus_Tick(object sender, EventArgs e)
        {
            timerReFocus.Enabled = false;
            if (AutoScrollToCaret && (NotificationCaretPosition > 0 && NotificationCaretPosition <= tbMessage.Text.Length))
            {
                if (NotificationCaretPosition > 0)
                {
                    tbMessage.Select(NotificationCaretPosition, 0);
                }
                else
                {
                    tbMessage.Select(tbMessage.Text.Length, 0);
                }
                tbMessage.ScrollToCaret();
                tbMessage.Focus();
            }
            else if (tbMessage.Text.Length > 0)
            {
                tbMessage.Select(0, 0);
                tbMessage.ScrollToCaret();
                tbMessage.Focus();
            }
        }

        #region Color Theme
        public void SetColorTheme()
        {
            // Font
            UpdateFontSize();

            // Help button
            SetupHelpPictureBox();

            // Texture
            ResolveTexture();

            // Button images
            btnRepeat.BackgroundImage = ButtonBitmaps.Repeat();
            btnRepeat.BackgroundImageLayout = ImageLayout.Center;
            btnRepeat.Text = "";

            btnGetTitle.BackgroundImage = ButtonBitmaps.GetTitle();
            btnGetTitle.BackgroundImageLayout = ImageLayout.Center;
            btnGetTitle.Text = "";

            btnGetContent.BackgroundImage = ButtonBitmaps.GetContent();
            btnGetContent.BackgroundImageLayout = ImageLayout.Center;
            btnGetContent.Text = "";

            ResolveAutoScrollButton();

            btnYearMinus.BackgroundImage = ButtonBitmaps.Left();
            btnYearMinus.BackgroundImageLayout = ImageLayout.Center;
            btnYearMinus.Text = "";

            btnYearPlus.BackgroundImage = ButtonBitmaps.Right();
            btnYearPlus.BackgroundImageLayout = ImageLayout.Center;
            btnYearPlus.Text = "";

            btnMonthMinus.BackgroundImage = ButtonBitmaps.Left();
            btnMonthMinus.BackgroundImageLayout = ImageLayout.Center;
            btnMonthMinus.Text = "";

            btnMonthPlus.BackgroundImage = ButtonBitmaps.Right();
            btnMonthPlus.BackgroundImageLayout = ImageLayout.Center;
            btnMonthPlus.Text = "";

            btnDayMinus.BackgroundImage = ButtonBitmaps.Left();
            btnDayMinus.BackgroundImageLayout = ImageLayout.Center;
            btnDayMinus.Text = "";

            btnDayPlus.BackgroundImage = ButtonBitmaps.Right();
            btnDayPlus.BackgroundImageLayout = ImageLayout.Center;
            btnDayPlus.Text = "";

            btnHourMinus.BackgroundImage = ButtonBitmaps.Left();
            btnHourMinus.BackgroundImageLayout = ImageLayout.Center;
            btnHourMinus.Text = "";

            btnHourPlus.BackgroundImage = ButtonBitmaps.Right();
            btnHourPlus.BackgroundImageLayout = ImageLayout.Center;
            btnHourPlus.Text = "";

            btnMinutesMinus.BackgroundImage = ButtonBitmaps.Left();
            btnMinutesMinus.BackgroundImageLayout = ImageLayout.Center;
            btnMinutesMinus.Text = "";

            btnMinutesPlus.BackgroundImage = ButtonBitmaps.Right();
            btnMinutesPlus.BackgroundImageLayout = ImageLayout.Center;
            btnMinutesPlus.Text = "";

            btnTimeZero.BackgroundImage = ButtonBitmaps.Clear(false);
            btnTimeZero.BackgroundImageLayout = ImageLayout.Center;
            btnTimeZero.Text = "";

            btnCloseRepeat.BackgroundImage = ButtonBitmaps.Clear(false);
            btnCloseRepeat.BackgroundImageLayout = ImageLayout.Center;
            btnCloseRepeat.Text = "";

            btnResetRepeatCode.BackgroundImage = ButtonBitmaps.Clear(false);
            btnResetRepeatCode.BackgroundImageLayout = ImageLayout.Center;
            btnResetRepeatCode.Text = "";

            // Form
            this.BackColor = AhoyColors.CurrentColorTheme()[0];

            // Repeat panel
            pnlRepeat.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlRepeatTextField.BackColor = AhoyColors.CurrentColorTheme()[1];
            lbRepeat.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbRepeat.BackColor = System.Drawing.Color.Transparent;

            // Content
            tbMessage.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbMessage.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Form labels
            lbTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTitle.BackColor = System.Drawing.Color.Transparent;
            lbUndoRedo.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbUndoRedo.BackColor = System.Drawing.Color.Transparent;
            lbSystemTimeTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbSystemTimeTitle.BackColor = System.Drawing.Color.Transparent;
            lbState.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbState.BackColor = System.Drawing.Color.Transparent;
            lbSystemTimeDisplay.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbSystemTimeDisplay.BackColor = System.Drawing.Color.Transparent;
            lbIsoWeekNumber.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbIsoWeekNumber.BackColor = System.Drawing.Color.Transparent;

            // Date Time Display
            lbYear.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbYear.BackColor = System.Drawing.Color.Transparent;
            lbMonth.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMonth.BackColor = System.Drawing.Color.Transparent;
            lbMonthName.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMonthName.BackColor = System.Drawing.Color.Transparent;
            lbDayName.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbDayName.BackColor = System.Drawing.Color.Transparent;
            lbDay.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbDay.BackColor = System.Drawing.Color.Transparent;
            lbHour.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbHour.BackColor = System.Drawing.Color.Transparent;
            lbMinute.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMinute.BackColor = System.Drawing.Color.Transparent;
            lbTimeDivider.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbTimeDivider.BackColor = System.Drawing.Color.Transparent;
            pnlDateTimeContainer.BackColor = AhoyColors.CurrentColorTheme()[1];

            // Date Time ANSI
            lbDateTimeAnsi.BackColor = AhoyColors.CurrentColorTheme()[1];
            lbDateTimeAnsi.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Buttons
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnRedo.FlatStyle = FlatStyle.Flat;
            btnToggleState.FlatStyle = FlatStyle.Flat;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnYearMinus.FlatStyle = FlatStyle.Flat;
            btnYearPlus.FlatStyle = FlatStyle.Flat;
            btnMonthMinus.FlatStyle = FlatStyle.Flat;
            btnMonthPlus.FlatStyle = FlatStyle.Flat;
            btnMonthPlus2.FlatStyle = FlatStyle.Flat;
            btnMonthMinus2.FlatStyle = FlatStyle.Flat;
            btnDayMinus.FlatStyle = FlatStyle.Flat;
            btnDayPlus.FlatStyle = FlatStyle.Flat;
            btnHourMinus.FlatStyle = FlatStyle.Flat;
            btnHourPlus.FlatStyle = FlatStyle.Flat;
            btnMinutesMinus.FlatStyle = FlatStyle.Flat;
            btnMinutesPlus.FlatStyle = FlatStyle.Flat;
            btnDayPlus7.FlatStyle = FlatStyle.Flat;
            btnDayMinus7.FlatStyle = FlatStyle.Flat;
            btnDayPlus14.FlatStyle = FlatStyle.Flat;
            btnHourPlus2.FlatStyle = FlatStyle.Flat;
            btnHourPlus4.FlatStyle = FlatStyle.Flat;
            btnMinutePlus5.FlatStyle = FlatStyle.Flat;
            btnMinutePlus10.FlatStyle = FlatStyle.Flat;
            btnMinutePlus15.FlatStyle = FlatStyle.Flat;
            btnHourMinus2.FlatStyle = FlatStyle.Flat;
            btnHourMinus4.FlatStyle = FlatStyle.Flat;
            btnMinuteZero.FlatStyle = FlatStyle.Flat;
            btnMinute30.FlatStyle = FlatStyle.Flat;
            btnGetTitle.FlatStyle = FlatStyle.Flat;
            btnGetContent.FlatStyle = FlatStyle.Flat;
            btnToggleCaret.FlatStyle = FlatStyle.Flat;
            btnTimeZero.FlatStyle = FlatStyle.Flat;
            btnCallUp.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnSnooze.FlatStyle = FlatStyle.Flat;
            btnRepeat.FlatStyle = FlatStyle.Flat;
            btnEnableRepeat.FlatStyle = FlatStyle.Flat;
            btnRepeatDaily.FlatStyle = FlatStyle.Flat;
            btnRepeatWeekly.FlatStyle = FlatStyle.Flat;
            btnDisableRepeat.FlatStyle = FlatStyle.Flat;
            btnCloseRepeat.FlatStyle = FlatStyle.Flat;
            btnRepeatCode.FlatStyle = FlatStyle.Flat;
            btnResetRepeatCode.FlatStyle = FlatStyle.Flat;

            btnUndo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRedo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleState.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnReset.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnYearMinus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnYearPlus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMonthMinus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMonthPlus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMonthPlus2.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMonthMinus2.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDayMinus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDayPlus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourMinus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourPlus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinutesMinus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinutesPlus.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDayPlus7.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDayMinus7.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDayPlus14.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourPlus2.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourPlus4.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinutePlus5.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinutePlus10.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinutePlus15.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourMinus2.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHourMinus4.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinuteZero.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMinute30.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnGetTitle.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnGetContent.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleCaret.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTimeZero.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCallUp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSnooze.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRepeat.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnEnableRepeat.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRepeatDaily.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRepeatWeekly.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDisableRepeat.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCloseRepeat.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRepeatCode.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnResetRepeatCode.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];


            btnUndo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRedo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleState.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnReset.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnYearMinus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnYearPlus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMonthMinus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMonthPlus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMonthPlus2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMonthMinus2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDayMinus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDayPlus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHourMinus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHourPlus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinutesMinus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinutesPlus.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDayPlus7.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDayMinus7.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDayPlus14.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHourPlus2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHourPlus4.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinutePlus5.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinutePlus10.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinutePlus15.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10]; ;
            btnHourMinus2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHourMinus4.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinuteZero.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMinute30.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnGetTitle.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnGetContent.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleCaret.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTimeZero.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCallUp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSnooze.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnEnableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRepeatDaily.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRepeatWeekly.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDisableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCloseRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRepeatCode.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnResetRepeatCode.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnUndo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRedo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnToggleState.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnReset.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnYearMinus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnYearPlus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMonthMinus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMonthPlus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMonthPlus2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMonthMinus2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDayMinus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDayPlus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHourMinus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHourPlus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinutesMinus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinutesPlus.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDayPlus7.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDayMinus7.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDayPlus14.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHourPlus2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHourPlus4.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinutePlus5.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinutePlus10.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinutePlus15.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11]; ;
            btnHourMinus2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHourMinus4.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinuteZero.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMinute30.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnGetTitle.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnGetContent.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnToggleCaret.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTimeZero.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCallUp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSnooze.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRepeat.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnEnableRepeat.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRepeatDaily.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRepeatWeekly.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDisableRepeat.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCloseRepeat.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRepeatCode.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnResetRepeatCode.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnUndo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRedo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleState.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnReset.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnYearMinus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnYearPlus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMonthMinus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMonthPlus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMonthPlus2.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMonthMinus2.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDayMinus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDayPlus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourMinus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourPlus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinutesMinus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinutesPlus.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDayPlus7.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDayMinus7.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDayPlus14.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourPlus2.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourPlus4.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinutePlus5.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinutePlus10.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinutePlus15.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourMinus2.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHourMinus4.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinuteZero.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMinute30.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnGetTitle.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnGetContent.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleCaret.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTimeZero.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCallUp.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSnooze.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnEnableRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatDaily.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatWeekly.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDisableRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCloseRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatCode.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnResetRepeatCode.BackColor = AhoyColors.CurrentColorTheme()[7];

            btnUndo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRedo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnToggleState.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnReset.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnYearMinus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnYearPlus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMonthMinus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMonthPlus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMonthPlus2.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMonthMinus2.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDayMinus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDayPlus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourMinus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourPlus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinutesMinus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinutesPlus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDayPlus7.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDayMinus7.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDayPlus14.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourPlus2.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourPlus4.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinutePlus5.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinutePlus10.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinutePlus15.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourMinus2.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHourMinus4.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinuteZero.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMinute30.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnGetTitle.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnGetContent.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnToggleCaret.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTimeZero.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCallUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSnooze.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnEnableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatDaily.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatWeekly.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDisableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCloseRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatCode.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnResetRepeatCode.ForeColor = AhoyColors.CurrentColorTheme()[8];
        }

        public void ResolveTexture()
        {
            if (MainForm.IsTextureEnabled())
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);
            }
            else
            {
                this.BackgroundImage = null;
            }
        }



        private void ResetDateTimeColorPanel()
        {
            lbYear.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbYear.BackColor = System.Drawing.Color.Transparent;
            lbMonth.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMonth.BackColor = System.Drawing.Color.Transparent;
            lbMonthName.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMonthName.BackColor = System.Drawing.Color.Transparent;
            lbDayName.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbDayName.BackColor = System.Drawing.Color.Transparent;
            lbDay.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbDay.BackColor = System.Drawing.Color.Transparent;
            lbHour.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbHour.BackColor = System.Drawing.Color.Transparent;
            lbMinute.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbMinute.BackColor = System.Drawing.Color.Transparent;
            lbTimeDivider.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbTimeDivider.BackColor = System.Drawing.Color.Transparent;
            pnlDateTimeContainer.BackColor = AhoyColors.CurrentColorTheme()[1];
        }

        private void SetDateTimeColorPanel(int colorIndex)
        {
            lbYear.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbYear.BackColor = System.Drawing.Color.Transparent;
            lbMonth.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbMonth.BackColor = System.Drawing.Color.Transparent;
            lbMonthName.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbMonthName.BackColor = System.Drawing.Color.Transparent;
            lbDayName.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbDayName.BackColor = System.Drawing.Color.Transparent;
            lbDay.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbDay.BackColor = System.Drawing.Color.Transparent;
            lbHour.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbHour.BackColor = System.Drawing.Color.Transparent;
            lbMinute.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbMinute.BackColor = System.Drawing.Color.Transparent;
            lbTimeDivider.ForeColor = AhoyColors.CurrentColorTheme()[colorIndex];
            lbTimeDivider.BackColor = System.Drawing.Color.Transparent;
        }

        //private void SetDateTimeColorPanel(Color background, Color foreground)
        //{
        //    pnlDateTimeContainer.BackColor = background;
        //    lbYear.ForeColor = foreground;
        //    lbYear.BackColor = System.Drawing.Color.Transparent;
        //    lbMonth.ForeColor = foreground;
        //    lbMonth.BackColor = System.Drawing.Color.Transparent;
        //    lbMonthName.ForeColor = foreground;
        //    lbMonthName.BackColor = System.Drawing.Color.Transparent;
        //    lbDayName.ForeColor = foreground;
        //    lbDayName.BackColor = System.Drawing.Color.Transparent;
        //    lbDay.ForeColor = foreground;
        //    lbDay.BackColor = System.Drawing.Color.Transparent;
        //    lbHour.ForeColor = foreground;
        //    lbHour.BackColor = System.Drawing.Color.Transparent;
        //    lbMinute.ForeColor = foreground;
        //    lbMinute.BackColor = System.Drawing.Color.Transparent;
        //    lbTimeDivider.ForeColor = foreground;
        //    lbTimeDivider.BackColor = System.Drawing.Color.Transparent;
        //}

        #endregion

        // Font
        private void UpdateFontSize()
        {
            tbMessage.Font = Design.FieldFont(MainForm.FieldsFontSize);
            lbSystemTimeDisplay.Font = Design.ListBoxFont(MainForm.FieldsFontSize);
            lbDateTimeAnsi.Font = Design.StyleEditorXXSSmallFont();
            lbRepeat.Font = Design.TitleFieldFont();
        }

        private void DisplayCurrentDateTime()
        {
            lbSystemTimeDisplay.Text = IO.GetIsoDateTimeNow();
        }

        // Show system dateTime
        private void timerSystemTime_Tick(object sender, EventArgs e)
        {
            // Update DateTime label
            DisplayCurrentDateTime();

            if ((NotificationEnabled || RepeatNotification) && IsDue())
            {
                // Bring to front when raised
                this.BringToFront();
                if (!timerBlinker.Enabled)
                {
                    // Lock text input when raised
                    SetTextInputLock(true);
                    if (NotificationDateTime.Date <= DateTime.Now.Date.AddDays(-1))
                    {
                        AlarmRaised = true;
                        MainForm.NotificationRaised = AlarmRaised;
                        AlarmWasRaised = true;
                        //ToggleColorShiftTimer(true);
                        timerBlinker.Enabled = true;
                        lbMonthName.Text = "";
                        lbDayName.Text = "[OVERDUE]";
                        lbTitle.Text = "--> NOTIFICATION <--";
                        ToggleButtonHighlight(true);
                        ResolveStateButton();
                    }
                    else
                    {
                        AlarmRaised = true;
                        MainForm.NotificationRaised = AlarmRaised;
                        AlarmWasRaised = true;
                        //ToggleColorShiftTimer(true);
                        timerBlinker.Enabled = true;
                        lbMonthName.Text = "";
                        lbDayName.Text = "[NOW]";
                        lbTitle.Text = "--> NOTIFICATION <--";
                        ToggleButtonHighlight(true);
                        ResolveStateButton();
                    }
                }
            }
            else
            {
                if (timerBlinker.Enabled)
                {
                    // Unlock text input
                    SetTextInputLock(false);
                    AlarmRaised = false;
                    MainForm.NotificationRaised = AlarmRaised;
                    ResolveTitleLabel();
                    //ToggleColorShiftTimer(false);
                    timerBlinker.Enabled = false;
                    ToggleButtonHighlight(false);
                    ResetDateTimeColorPanel();
                    ResolveStateButton();
                }
            }
        }

        private void ToggleButtonHighlight(bool highlight)
        {
            if (highlight)
            {
                btnMinutePlus5.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus5.BackColor = AhoyColors.CurrentColorTheme()[4];
                btnMinutePlus5.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[6];

                btnMinutePlus10.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus10.BackColor = AhoyColors.CurrentColorTheme()[4];
                btnMinutePlus10.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[6];

                btnMinutePlus15.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus15.BackColor = AhoyColors.CurrentColorTheme()[4];
                btnMinutePlus15.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[6];

                btnSnooze.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnSnooze.BackColor = AhoyColors.CurrentColorTheme()[4];
                btnSnooze.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnMinutePlus5.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnMinutePlus5.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus5.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

                btnMinutePlus10.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnMinutePlus10.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus10.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

                btnMinutePlus15.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnMinutePlus15.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnMinutePlus15.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

                btnSnooze.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnSnooze.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnSnooze.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            }
        }


        private void ResolveRepeatVsStandard()
        {
            // Show the next repeating alarm if nothing was set already
            if (!AlarmRaised && (RepeatNotification && !NotificationEnabled))
            {
                PublishRepeatData();
            }
        }

        private void PublishRepeatData()
        {
            if (!string.IsNullOrEmpty(RepeatData))
            {
                // Set the NotificationDateTime to the repeated date/time
                NotificationDateTime = Notification.GetRepeatDataAsDateTime(RepeatData);
            }
        }

        private bool IsDue()
        {
            // Repeated notification (must be run first to eliminate double alarms)
            if (RepeatNotification && (!string.IsNullOrEmpty(RepeatData) && Notification.GetRepeatDataAsDateTime(RepeatData) <= DateTime.Now))
            {
                // Force the repeat data over to the current entry notification
                NotificationEnabled = true;
                PublishRepeatData();
                // Update the repeat data to the next day or week after invoking standard notification
                PushRepeat();
                return true;
            }
            // Standard notification
            else if (NotificationEnabled && NotificationDateTime <= DateTime.Now)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void PushRepeat()
        {
            if (Notification.IsWeekly(RepeatData))
            {
                RepeatData = Notification.PushWeeks(RepeatData, Notification.GetRepeatCode());
            }
            else
            {
                RepeatData = Notification.PushDays(RepeatData, Notification.GetRepeatCode());
            }
        }

        // Toggle state
        private void btnToggleState_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            ToggleNotificationState(true);
        }


        private void ToggleNotificationState(bool refocusInput)
        {
            // Toggling
            if (NotificationEnabled)
            {
                NotificationEnabled = false;
            }
            else
            {
                NotificationEnabled = true;
            }

            // Logging
            if (NotificationEnabled)
            {
                Log.NotificationSet("Enabled");
            }
            else
            {
                if (RepeatNotification)
                {
                    Log.NotificationSet("Disabled (Repeating)");
                }
                else
                {
                    Log.NotificationSet("Disabled");
                }
            }

            // Confirm will update the display to next alarm
            if (ConfirmRepetition)
            {
                ConfirmRepetition = false;
                PublishRepeatData();
            }

            UpdateControls(refocusInput);
        }

        private void ResolveButtonColors()
        {
            if (NotificationEnabled)
            {
                btnToggleState.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnToggleState.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnClose.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnClose.BackColor = AhoyColors.CurrentColorTheme()[4];
                btnToggleState.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
                btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnToggleState.ForeColor = AhoyColors.CurrentColorTheme()[4];
                btnToggleState.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnToggleState.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
                btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            }
        }

        // Reset notification date time
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            MainForm.KeepAwake();
            time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            NotificationDateTime = DateTime.Now;
            NotificationDateTime = NotificationDateTime.Date + time;
            UpdateControls(true);
        }


        // Undo
        private void btnUndo_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            tbMessage.Undo();
            tbMessage.Focus();
        }

        // Redo
        private void btnRedo_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            tbMessage.Redo();
            tbMessage.Focus();
        }

        #region Manipulate TimeStamp
        private void btnYearMinus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddYears(-1);
            UpdateControls(true);
        }

        private void btnYearPlus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddYears(1);
            UpdateControls(true);
        }

        private void btnMonthMinus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddMonths(-1);
            UpdateControls(true);
        }

        private void btnMonthPlus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddMonths(1);
            UpdateControls(true);
        }

        private void btnMonthPlus2_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddMonths(2);
            UpdateControls(true);
        }

        private void btnMonthMinus2_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddMonths(-2);
            UpdateControls(true);
        }

        private void btnDayMinus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddDays(-1);
            UpdateControls(true);
        }

        private void btnDayPlus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddDays(1);
            UpdateControls(true);
        }

        private void btnHourMinus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddHours(-1);
            UpdateControls(true);
        }

        private void btnHourPlus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddHours(1);
            UpdateControls(true);
        }

        private void btnMinutesMinus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddMinutes(-1);
            UpdateControls(true);
        }

        private void btnMinutesPlus_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddMinutes(1);
            UpdateControls(true);
        }


        // Do not allow editing date/time when repeat is enabled
        private bool CanEdit()
        {
            if (RepeatNotification && !AlarmRaised)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        // If notification is active and current time set is 1 minute old
        // update it to the current date and time (used when adding hours and minutes)
        private bool TimeStampOld { get; set; }
        private void RefreshTimeStamp()
        {
            TimeStampOld = false;
            if (NotificationDateTime < DateTime.Now.AddMinutes(-1))
            {
                TimeStampOld = true;
            }

            if (NotificationEnabled && TimeStampOld)
            {
                TimeStampOld = false;
                time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                NotificationDateTime = DateTime.Now;
            }
        }

        private void btnDayPlus7_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddDays(7);
            UpdateControls(true);
        }

        private void btnDayMinus7_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            NotificationDateTime = NotificationDateTime.AddDays(-7);
            UpdateControls(true);
        }

        private void btnDayPlus14_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddDays(14);
            UpdateControls(true);
        }

        private void btnHourPlus2_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddHours(2);
            UpdateControls(true);
        }

        private void btnHourPlus4_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddHours(4);
            UpdateControls(true);
        }

        private void btnHourMinus2_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddHours(-2);
            UpdateControls(true);
        }


        private void btnHourMinus4_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            NotificationDateTime = NotificationDateTime.AddHours(-4);
            UpdateControls(true);
        }

        private void btnMinutePlus5_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            if (AlarmRaised) // Set DateTime now and add snooze
            {
                time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                NotificationDateTime = DateTime.Now.Date + time;
                NotificationDateTime = NotificationDateTime.AddMinutes(5);
                Log.NotificationSet("Snooze (" + IO.FormatAsISODateMinutes(NotificationDateTime) + ")");
                UpdateControls(true);
            }
            else
            {
                RefreshTimeStamp(); // Update timestamp if active
                NotificationDateTime = NotificationDateTime.AddMinutes(5);
                UpdateControls(true);
            }
        }

        private void btnMinutePlus10_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            if (AlarmRaised) // Set DateTime now and add snooze
            {
                time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                NotificationDateTime = DateTime.Now.Date + time;
                NotificationDateTime = NotificationDateTime.AddMinutes(10);
                Log.NotificationSet("Snooze (" + IO.FormatAsISODateMinutes(NotificationDateTime) + ")");
                UpdateControls(true);
            }
            else
            {
                RefreshTimeStamp(); // Update timestamp if active
                NotificationDateTime = NotificationDateTime.AddMinutes(10);
                UpdateControls(true);
            }
        }

        private void btnMinutePlus15_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            if (AlarmRaised) // Set DateTime now and add snooze
            {
                time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                NotificationDateTime = DateTime.Now.Date + time;
                NotificationDateTime = NotificationDateTime.AddMinutes(15);
                Log.NotificationSet("Snooze (" + IO.FormatAsISODateMinutes(NotificationDateTime) + ")");
                UpdateControls(true);
            }
            else
            {
                RefreshTimeStamp(); // Update timestamp if active
                NotificationDateTime = NotificationDateTime.AddMinutes(15);
                UpdateControls(true);
            }
        }

        private void btnMinute30_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            time = new TimeSpan(NotificationDateTime.Hour, 30, 0);
            NotificationDateTime = NotificationDateTime.Date + time;
            UpdateControls(true);
        }

        private void btnMinuteZero_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            time = new TimeSpan(NotificationDateTime.Hour, 0, 0);
            NotificationDateTime = NotificationDateTime.Date + time;
            UpdateControls(true);
        }

        private void btnTimeZero_Click(object sender, EventArgs e)
        {
            // Return if locked for editing
            if (!CanEdit()) return;

            RefreshTimeStamp(); // Update timestamp if active
            time = new TimeSpan(0, 0, 0);
            NotificationDateTime = NotificationDateTime.Date + time;
            UpdateControls(true);
        }
        #endregion

        private void tbMessage_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreTextChanged)
            {
                SetCaretPosition();
            }
        }

        private void tbMessage_Click(object sender, EventArgs e)
        {
            SetCaretPosition();
        }

        private void btnGetContent_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (!string.IsNullOrEmpty(ContentText) && !MainForm.InterfaceLock && !TextInputLock)
            {
                tbMessage.SetText(ContentText, false);
                // Move cursor to last selected position
                tbMessage.Select(CaretPosition, 0);
                tbMessage.ScrollToCaret();
                tbMessage.Focus();
            }
        }

        private void btnGetTitle_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (!string.IsNullOrEmpty(TitleText) && !MainForm.InterfaceLock && !TextInputLock)
            {
                tbMessage.SetText(TitleText, false);
                // Move cursor to last selected position
                tbMessage.Select(tbMessage.Text.Length, 0);
                tbMessage.ScrollToCaret();
                tbMessage.Focus();
            }
        }

        private int tempCurrentColor;
        //private bool tempShiftColor;
        private void timerBlinker_Tick(object sender, EventArgs e)
        {
            if (tempCurrentColor > 2)
            {
                tempCurrentColor = 0;
            }

            pnlDateTimeContainer.SuspendLayout();
            //tempShiftColor = !tempShiftColor;
            //if (tempShiftColor)
            //{
            //    SetDateTimeColorPanel(ShiftTimerBackgroundColor, ShiftTimerForegroundColor);
            //}
            //else
            //{
            //    SetDateTimeColorPanel(ShiftTimerForegroundColor, ShiftTimerBackgroundColor);
            //}

            if (tempCurrentColor >= 0)
            {
                switch (tempCurrentColor)
                {
                    case 0:
                        SetDateTimeColorPanel(6); // Highlight I
                        break;
                    case 1:
                        SetDateTimeColorPanel(4); // Highlight II
                        break;
                    case 2:
                        SetDateTimeColorPanel(3); // Normal
                        break;

                }
            }
            pnlDateTimeContainer.ResumeLayout();
            tempCurrentColor += 1;
        }

        private void btnCallUp_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            // Ask the main form to call-up the associated entry
            SubmitCallUpRequest();
            FocusInput();
        }

        // Have the main form select the linked entry
        public void SubmitCallUpRequest()
        {
            // Call-up entry in main editor if not locked
            if (!MainForm.InterfaceLock)
            {
                Log.NotificationSet("Call-Up");
                MainForm.NotificationCallupID = NotificationID;
                MainForm.NotificationCallUpEntry = true;
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to call-up entry when AHOY! is locked/sleeping", MessageText.NotificationCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }


        // Close form
        private void btnClose_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            SubmitAndClose();
        }

        private string GetFormattedNotificationString(DateTime input)
        {
            if (NotificationEnabled)
            {
                return IO.FormatAsISODateTimeZeroSeconds(NotificationDateTime);
            }
            else
            {
                return "";
            }
        }

        private void SubmitAndClose()
        {
            HideRepeatPanel();
            if (NotificationEnabled || RepeatNotification)
            {
                // Clear any generated repeat data if disabled before submitting
                if (!RepeatNotification)
                {
                    ClearRepeatData();
                }

                // Submit with time stamp if enabled
                MainForm.SubmitNotificationData(NotificationID, tbMessage.Text, GetFormattedNotificationString(NotificationDateTime), NotificationCaretPosition, AutoScrollToCaret, RepeatData);
                MainForm.RegisterChangesForSaveAndBackup();
                CloseForm();
            }
            else
            {
                if (!AlarmWasRaised)
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Close (and save) without making Active?", MessageText.NotificationCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();

                    if (msg.Accepted)
                    {
                        SubmitDataAndClose();
                    }
                }
                else
                {
                    SubmitDataAndClose();
                }
            }
        }

        private void SubmitDataAndClose()
        {
            // Submit WITHOUT time stamp if disabled
            MainForm.SubmitNotificationData(NotificationID, tbMessage.Text, "", NotificationCaretPosition, AutoScrollToCaret, "");
            MainForm.RegisterChangesForSaveAndBackup();
            CloseForm();
        }

        private void CloseForm()
        {
            this.Close();
        }

        // Close without submitting
        private void CancelEdits()
        {
            this.Close();
        }

        private void tbMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancel changes
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                CancelEdits();
                e.Handled = true;
            }
            else if (TextInputLock)
            {
                e.Handled = true;
            }
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            MainForm.KeepAwake();
            if (e.Control && e.KeyCode == Keys.Oemplus) // Multiple shortcut keypress (Insert Paragraph Divider)
            {
                InsertParagraphDivider(0);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                InsertParagraphDivider(1);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemPeriod)
            {
                InsertParagraphDivider(2);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                InsertParagraphDivider(3);
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.I)) // Multiple shortcut keypress (Toggle state)
            {
                ToggleNotificationState(true);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.X)) // Multiple shortcut keypress (Cut)
            {
                tbMessage.CutUnformatted();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.C)) // Multiple shortcut keypress (Copy)
            {
                tbMessage.CopyUnformatted();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.V)) // Multiple shortcut keypress (Paste)
            {
                tbMessage.PasteUnformatted();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Z))
            {
                tbMessage.Undo();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Y))
            {
                tbMessage.Redo();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.T)) // Timestamp
            {
                InsertDate();
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F5)
            {
                SubmitAndClose();
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F4)
            {
                Snooze();
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F8)
            {
                SubmitCallUpRequest();
                e.Handled = true;
            }
        }

        private void InsertDate()
        {
            tbMessage.InsertISOTimestamp();
        }

        private void SetCaretPosition()
        {
            if (AutoScrollToCaret)
            {
                NotificationCaretPosition = tbMessage.SelectionStart;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            CancelEdits();
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



        private void lbHour_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NotificationDateTime = NotificationDateTime.AddHours(1);
                UpdateControls(true);
            }
            else
            {
                NotificationDateTime = NotificationDateTime.AddHours(-1);
                UpdateControls(true);
            }
        }

        private void lbMinute_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NotificationDateTime = NotificationDateTime.AddMinutes(1);
                UpdateControls(true);
            }
            else
            {
                NotificationDateTime = NotificationDateTime.AddMinutes(-1);
                UpdateControls(true);
            }
        }

        private void lbDay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NotificationDateTime = NotificationDateTime.AddDays(1);
                UpdateControls(true);
            }
            else
            {
                NotificationDateTime = NotificationDateTime.AddDays(-1);
                UpdateControls(true);
            }
        }

        private void lbMonth_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NotificationDateTime = NotificationDateTime.AddMonths(1);
                UpdateControls(true);
            }
            else
            {
                NotificationDateTime = NotificationDateTime.AddMonths(-1);
                UpdateControls(true);
            }
        }

        private void lbYear_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NotificationDateTime = NotificationDateTime.AddYears(1);
                UpdateControls(true);
            }
            else
            {
                NotificationDateTime = NotificationDateTime.AddYears(-1);
                UpdateControls(true);
            }
        }

        private void SetupHelpPictureBox()
        {
            picHelp.BackColor = System.Drawing.Color.Transparent;
            picHelp.BackgroundImage = ButtonBitmaps.HelpButtonHighlight();
            picHelp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            // Show help
            AhoyInfoMessageBox mgs = new AhoyInfoMessageBox(IO.NotificationEditorHelpMessageTextReader(), MessageText.HelpCaption(), false, true, this.Opacity);
            mgs.TopMost = true;
            mgs.ShowDialog();
        }

        // Insert Paragraph divider
        private void InsertParagraphDivider(int selection)
        {
            switch (selection)
            {
                case 0:
                    tbMessage.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
                case 1:
                    tbMessage.InsertParagraphDivider(ParagraphDividers.DividerBString);
                    break;
                case 2:
                    tbMessage.InsertParagraphDivider(ParagraphDividers.DividerCString);
                    break;
                case 3:
                    tbMessage.InsertParagraphDivider(ParagraphDividers.DividerDString);
                    break;
                default:
                    tbMessage.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
            }
        }

        private void btnSnooze_Click(object sender, EventArgs e)
        {
            Snooze();
        }

        private void Snooze()
        {
            if (AlarmRaised) // Set DateTime now and add snooze
            {
                time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                NotificationDateTime = DateTime.Now.Date + time;
                NotificationDateTime = NotificationDateTime.AddMinutes(SnoozeMinutes);
                Log.NotificationSet("Snooze (" + IO.FormatAsISODateMinutes(NotificationDateTime) + ")");
                SubmitAndClose();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Snooze for " + SnoozeMinutes.ToString() + " minutes when raised.\nRight click the Snooze button to change duration", MessageText.SnoozeCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
                FocusInput();
            }
        }

        private void btnSnooze_MouseDown(object sender, MouseEventArgs e)
        {
            // Cycle Snooze interval
            if (e.Button == MouseButtons.Right)
            {
                CycleSnoozeInterval();
            }
        }

        private void CycleSnoozeInterval()
        {
            if (SnoozeInterval == 12)
            {
                SnoozeInterval = 0;
            }
            else
            {
                SnoozeInterval += 1;
            }

            // Submit value to main form
            MainForm.SnoozeInterval = SnoozeInterval;
            MainForm.SettingsHasChanges = true;
            ResolveSnoozeInterval();
        }


        private const string snoozeText = "Snooze";
        private void ResolveSnoozeInterval()
        {
            switch (SnoozeInterval)
            {
                case 0:
                    SnoozeMinutes = 5;
                    break;
                case 1:
                    SnoozeMinutes = 10;
                    break;
                case 2:
                    SnoozeMinutes = 15;
                    break;
                case 3:
                    SnoozeMinutes = 20;
                    break;
                case 4:
                    SnoozeMinutes = 25;
                    break;
                case 5:
                    SnoozeMinutes = 30;
                    break;
                case 6:
                    SnoozeMinutes = 45;
                    break;
                case 7:
                    SnoozeMinutes = 60;
                    break;
                case 8:
                    SnoozeMinutes = 90;
                    break;
                case 9:
                    SnoozeMinutes = 120;
                    break;
                case 10:
                    SnoozeMinutes = 150;
                    break;
                case 11:
                    SnoozeMinutes = 180;
                    break;
                case 12:
                    SnoozeMinutes = 240;
                    break;
            }

            // Update Snooze button text
            btnSnooze.Text = snoozeText + " " + GetHoursFromMinutes(SnoozeMinutes);
        }

        // Convert minutes into formatted string
        private string GetHoursFromMinutes(decimal minutes)
        {
            if (minutes == 60)
            {
                return "1 hour";
            }
            else if (minutes > 60)
            {
                return (minutes / 60).ToString("0.0") + " hours";
            }
            else
            {
                return minutes + " min";
            }
        }

        private void btnToggleCaret_Click(object sender, EventArgs e)
        {
            AutoScrollToCaret = !AutoScrollToCaret;
            Log.NotificationSet("AutoScroll (" + AutoScrollToCaret.ToString() + ")");
            ResolveAutoScrollButton();
        }

        private void ResolveAutoScrollButton()
        {
            if (AutoScrollToCaret)
            {
                btnToggleCaret.BackgroundImage = ButtonBitmaps.GetScrollToCaretOn();
                btnToggleCaret.BackgroundImageLayout = ImageLayout.Center;
                btnToggleCaret.Text = "";
                this.toolTip1.SetToolTip(this.btnToggleCaret, "Scroll to Caret: ON");
                if (tbMessage.Text.Length > 0)
                {
                    if (NotificationCaretPosition > 0)
                    {
                        tbMessage.Select(NotificationCaretPosition, 0);
                    }
                    else
                    {
                        tbMessage.Select(tbMessage.Text.Length, 0);
                    }
                    tbMessage.ScrollToCaret();
                    tbMessage.Focus();
                }
                else
                {
                    tbMessage.Focus();
                }
            }
            else
            {
                try
                {
                    btnToggleCaret.BackgroundImage = ButtonBitmaps.GetScrollToCaretOff();
                    btnToggleCaret.BackgroundImageLayout = ImageLayout.Center;
                    btnToggleCaret.Text = "";
                    this.toolTip1.SetToolTip(this.btnToggleCaret, "Scroll to Caret: OFF");
                    tbMessage.Select(0, 0);
                    tbMessage.ScrollToCaret();
                    tbMessage.Focus();
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        // Principle:
        // A string containing the following determines the repetition:
        // "D;0;2019-12-17 23:59 23:59" (Daily at 23:59) <- Extract time
        // "W;0;2019-12-17 23:59" (Weekly: Tuesday at 23:59) <- Extract day of week + time
        // The MainForm just checks if there is a string in the variable, in "HasNotifications"
        // As soon as a value is changed in the RepeatPanel the string is submitted by special method into
        // the associated entry, even if it is not selected
        // NotificationDateTime
        private void GenerateRepeatData()
        {
            // Do not generate new repeat data if repeat is set
            if (RepeatNotification)
            {
                return;
            }

            if (RepeatWeekly)
            {
                RepeatData = Notification.GetRepeatData(NotificationDateTime, true, Notification.GetRepeatCode()); // W;0;2019-12-17 15:45:00
            }
            else
            {
                RepeatData = Notification.GetRepeatData(NotificationDateTime, false, Notification.GetRepeatCode()); // D;0;2019-12-17 15:45:00
            }
        }

        private void ClearRepeatData()
        {
            RepeatData = "";
        }

        // Repeat notification
        private void btnRepeat_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (!pnlRepeat.Visible)
            {
                UpdateRepeatPanelText();
                ShowRepeatPanel();
            }
            else
            {
                HideRepeatPanel();
            }
        }

        private void EnableRepeat()
        {
            MainForm.KeepAwake();
            // Only enable repeat if not locked (already enabled)
            if (!RepeatNotification)
            {
                GenerateRepeatData();
                NotificationEnabled = true;
                RepeatNotification = true;
                Log.NotificationSet("Repeat enabled");
                ResolveStateButton();
                ResolveRepeatButton();
                ResolveRepeatPanelButtons();
                SetRepeatButtonTexts();
            }
        }

        private void DisableRepeat()
        {
            MainForm.KeepAwake();
            if (RepeatNotification)
            {
                RepeatNotification = false;
                Log.NotificationSet("Repeat disabled");
                ResolveStateButton();
                ResolveRepeatButton();
                ResolveRepeatPanelButtons();
                ResetRepeatCode();
                UpdateRepeatPanelText();
                SetRepeatButtonTexts();
            }
        }




        private void ResolveRepeatButton()
        {
            if (RepeatNotification)
            {
                ToggleColorShiftTimer(true);
            }
            else
            {
                ToggleColorShiftTimer(false);
                btnRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            }
        }

        private void ShowRepeatPanel()
        {
            ResolveRepeatPanelButtons();
            pnlRepeat.BringToFront();
            pnlRepeat.Visible = true;
        }

        private void HideRepeatPanel()
        {
            if (pnlRepeat.Visible)
            {
                pnlRepeat.Visible = false;
                pnlRepeat.SendToBack();
            }
        }

        private void UpdateRepeatPanelText()
        {
            GenerateRepeatData();

            if (RepeatNotification)
            {
                lbRepeat.Text = "*** LOCKED ***\n";
            }
            else
            {
                lbRepeat.Text = "";
            }

            if (RepeatWeekly)
            {
                // Weekly
                lbRepeat.Text += "Repeat " + Notification.GetRepeatData(RepeatData, 5);
            }
            else
            {
                // Daily
                lbRepeat.Text += "Repeat " + Notification.GetRepeatName().ToLower() + " at " + Notification.GetRepeatData(RepeatData, 4);
            }

            // Resolve next alarm/starting from
            if (Notification.SnapDateTimeToRepeatCode(Notification.GetRepeatDataAsDateTime(RepeatData).Date) == DateTime.Now.AddDays(1).Date)
            {
                lbRepeat.Text += "\nNext/starting from: Tomorrow";
            }
            else if (Notification.SnapDateTimeToRepeatCode(Notification.GetRepeatDataAsDateTime(RepeatData).Date) == DateTime.Now.Date)
            {
                lbRepeat.Text += "\nNext/starting from: Today";
            }
            else
            {
                lbRepeat.Text += "\nNext/starting: " + Notification.SnapDateTimeToRepeatCode(Notification.GetRepeatDataAsDateTime(RepeatData)).DayOfWeek.ToString().Substring(0, 3) + " " + IO.FormatAsISODate(Notification.SnapDateTimeToRepeatCode(Notification.GetRepeatDataAsDateTime(RepeatData)));
            }

            if (RepeatWeekly)
            {
                lbRepeat.Text += Environment.NewLine + Notification.GetWeekGraphics((int)Notification.GetRepeatDataAsDateTime(RepeatData).DayOfWeek);
            }
            else
            {
                lbRepeat.Text += Environment.NewLine + Notification.GetRepeatGraphics();
            }
        }

        private void btnEnableRepeat_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            EnableRepeat();
        }


        // btnDisableRepetition
        private void btnDisableRepeat_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            DisableRepeat();
        }

        private void btnCloseRepeat_Click(object sender, EventArgs e)
        {
            HideRepeatPanel();
        }

        private void lbRepeatDaily_Click(object sender, EventArgs e)
        {
            if (!RepeatNotification)
            {
                MainForm.KeepAwake();
                RepeatWeekly = false;
                ResolveRepeatPanelButtons();
            }
        }

        private void btnRepeatWeekly_Click(object sender, EventArgs e)
        {
            if (!RepeatNotification)
            {
                MainForm.KeepAwake();
                RepeatWeekly = true;
                ResetRepeatCode();
                ResolveRepeatPanelButtons();
            }
        }

        private void ResolveRepeatPanelButtons()
        {
            // Reset colors
            btnRepeatDaily.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatDaily.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatDaily.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRepeatWeekly.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatWeekly.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatWeekly.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnDisableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDisableRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDisableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnEnableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnEnableRepeat.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnEnableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            // ON/OFF buttons
            if (RepeatNotification)
            {
                btnEnableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnEnableRepeat.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnEnableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
            }
            else
            {
                btnDisableRepeat.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnDisableRepeat.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnDisableRepeat.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
            }

            // Repetition buttons
            if (RepeatWeekly)
            {
                // Set weekly
                btnRepeatWeekly.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnRepeatWeekly.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnRepeatWeekly.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
                DimColors(btnRepeatWeekly);
            }
            else
            {
                // Set daily
                btnRepeatDaily.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnRepeatDaily.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnRepeatDaily.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
                DimColors(btnRepeatDaily);
            }
            ResolveRepeatCodeButtonColors();
            UpdateRepeatPanelText();
        }

        private void ResolveRepeatCodeButtonColors()
        {
            btnRepeatCode.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRepeatCode.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRepeatCode.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            if (!RepeatWeekly && Notification.GetRepeatCode() > 0)
            {
                btnRepeatCode.ForeColor = AhoyColors.CurrentColorTheme()[7];
                btnRepeatCode.BackColor = AhoyColors.CurrentColorTheme()[6];
                btnRepeatCode.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
                DimColors(btnRepeatCode);
            }
        }


        private void DimColors(Button btn)
        {
            if (RepeatNotification)
            {
                DimColor.DimColors(btn);
            }
        }
       

        private void SetRepeatButtonTexts()
        {
            if (RepeatNotification)
            {
                if (RepeatWeekly)
                {
                    btnRepeatDaily.Text = "N/A";
                    btnRepeatCode.Text = "N/A";
                    btnRepeatWeekly.Text = "Weekly";
                }
                else
                {
                    btnRepeatDaily.Text = "Daily";
                    btnRepeatCode.Text = Notification.GetRepeatName();
                    btnRepeatWeekly.Text = "N/A";
                }
            }
            else
            {
                btnRepeatDaily.Text = "Daily";
                btnRepeatCode.Text = Notification.GetRepeatName();
                btnRepeatWeekly.Text = "Weekly";
            }

            // Set tooltips
            this.toolTip1.SetToolTip(this.btnRepeatDaily, btnRepeatDaily.Text);
            this.toolTip1.SetToolTip(this.btnRepeatCode, btnRepeatCode.Text);
            this.toolTip1.SetToolTip(this.btnRepeatWeekly, btnRepeatWeekly.Text);
            ResolveRepeatCodeButtonColors();
        }


        private void btnRepeatCode_Click(object sender, EventArgs e)
        {
            if (!RepeatNotification && !RepeatWeekly)
            {
                Notification.NextRepeatCode();
                SetRepeatButtonTexts();
                UpdateRepeatPanelText();
            }
        }
        private void btnResetRepeatCode_Click(object sender, EventArgs e)
        {
            if (!RepeatNotification && !RepeatWeekly)
            {
                ResetRepeatCode();
                UpdateRepeatPanelText();
            }
        }

        private void ResetRepeatCode()
        {
            Notification.ResetRepeatCode();
            SetRepeatButtonTexts();
        }


        #region COLOR SHIFT (addition 07-04-2019)
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

        private Color GetColorShiftBackground()
        {
            if (ShiftTimerBackgroundColor != null)
            {
                return ShiftTimerBackgroundColor;
            }
            else
            {
                return Color.Black;
            }
        }

        private Color GetColorShiftForeground()
        {
            if (ShiftTimerForegroundColor != null)
            {
                return ShiftTimerBackgroundColor;
            }
            else
            {
                return Color.Black;
            }
        }

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

            btnRepeat.BackColor = ShiftTimerBackgroundColor;
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
