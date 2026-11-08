using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class Sticky : Form
    {
        /* Sticky July 2017 by LBS */

        public string ID { get; set; }
        public string StickyText { get; set; }
        private int SplashTimeoutCounter { get; set; }
        private const string StickyVersion = "v1.0.3";
        private bool IgnoreChanges { get; set; }
        public bool FocusOnSpawn { get; set; }
        private bool StickySleeping { get; set; }
        public int StatusCode { get; set; }
        private int ColorTheme { get; set; }
        public bool StatusComplete { get; set; }
        private int PreviousStatusCode { get; set; }

        #region CONSTRUCTOR
        public Sticky(string id, string stickyText, int width, int height, int left, int top, bool focusOnSpawn, int theme, bool topMost)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Sticky_KeyDown;

            SuspendLayout();
            IgnoreChanges = true; // Ignore start -------------------------->
            this.TopMost = topMost;
            ID = id;
            this.Text = MessageText.StickyCaption();
            HideBorder();
            IgnoreFormClosing = false;
            StickySleeping = false;
            StatusCode = 0;
            PreviousStatusCode = 0;
            ColorTheme = theme;
            StatusComplete = false;
            FocusOnSpawn = focusOnSpawn;
            this.ControlBox = false;
            tbNote.ContextMenuStrip = contextMenuBasic;
            TextBoxBackgroundColor = AhoyColors.CurrentColorTheme()[29];
            TextBoxForegroundColor = AhoyColors.CurrentColorTheme()[30];
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            btnShow.MouseEnter += BtnShow_MouseEnter;
            SetColorTheme();

            tbNote.AcceptsTab = true;
            tbNote.TabStop = false;
            tbNote.DetectUrls = true;
            SplashTimeoutCounter = 0;
            timerStickyTimer.Interval = 500; // 500ms
            timerStickyTimer.Enabled = true;
            btnClose.Visible = true;
            btnClose.Enabled = true;
            btnNew.Visible = true;
            btnNew.Enabled = true;
            btnHide.Visible = true;
            btnHide.Enabled = true;
            btnMove.Visible = true;
            btnMove.Enabled = true;
            btnResize.Visible = true;
            btnResize.Enabled = true;
            btnTheme.Visible = true;
            btnTheme.Enabled = true;

            btnClose.TabStop = false;
            btnNew.TabStop = false;
            btnHide.TabStop = false;
            btnMove.TabStop = false;
            btnResize.TabStop = false;
            btnTheme.TabStop = false;

            FocusContentDelayed = false;
            FocusContentDelayCounter = 0;
            tbNote.Text = stickyText;
            this.CenterToParent(); // Ensure the location can be set
            SetWindowPositionAndSize(left, top, width, height);
            if (!IsOnScreen(this))
            {
                Log.StickyStatus("Adjusting location...");
                SetDefaultWindowSizeAndLocation();
            }
            IgnoreChanges = false;// Ignore end <----------------------------
            ShowSplash(false, "", false, 0);
            ResumeLayout();
            this.BringToFront();
            FocusContent();
        }

        private void Sticky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.D))
            {
                CloseAndDelete();
            }
            else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Up) && MainForm.MoveByKeyboard) // Move form UP
            {
                MoveForm("u", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Down) && MainForm.MoveByKeyboard) // Move form DOWN
            {
                MoveForm("d", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Left) && MainForm.MoveByKeyboard) // Move form LEFT
            {
                MoveForm("l", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Right) && MainForm.MoveByKeyboard) // Move form RIGHT
            {
                MoveForm("r", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Up) && MainForm.ResizeByKeyboard) // Risize height -
            {
                ResizeForm("h-", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Down) && MainForm.ResizeByKeyboard) // Risize height +
            {
                ResizeForm("h+", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Left) && MainForm.ResizeByKeyboard) // Risize width -
            {
                ResizeForm("w-", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Right) && MainForm.ResizeByKeyboard) // Risize width +
            {
                ResizeForm("w+", Control.IsKeyLocked(Keys.CapsLock));
                e.Handled = true;
            }
        }

        #region PIXEL NUDGE form location and size (addition 24-10-2019)
        private int moveFormNudgeValue;
        private void MoveForm(string direction, bool capsLock)
        {
            if (capsLock)
            {
                moveFormNudgeValue = 5;
            }
            else
            {
                moveFormNudgeValue = 1;
            }

            switch (direction)
            {
                case "u":
                    this.Location = new Point(this.Location.X, this.Location.Y - moveFormNudgeValue);
                    break;
                case "d":
                    this.Location = new Point(this.Location.X, this.Location.Y + moveFormNudgeValue);
                    break;
                case "l":
                    this.Location = new Point(this.Location.X - moveFormNudgeValue, this.Location.Y);
                    break;
                case "r":
                    this.Location = new Point(this.Location.X + moveFormNudgeValue, this.Location.Y);
                    break;
            }

            // Save changes
            AutoSubmitChangesToMainForm();
        }
        private void ResizeForm(string direction, bool capsLock)
        {
            if (capsLock)
            {
                moveFormNudgeValue = 5;
            }
            else
            {
                moveFormNudgeValue = 1;
            }

            switch (direction)
            {
                case "w+":
                    this.Size = new Size(this.Size.Width + moveFormNudgeValue, this.Size.Height);
                    break;
                case "w-":
                    this.Size = new Size(this.Size.Width - moveFormNudgeValue, this.Size.Height);
                    break;
                case "h+":
                    this.Size = new Size(this.Size.Width, this.Size.Height + moveFormNudgeValue);
                    break;
                case "h-":
                    this.Size = new Size(this.Size.Width, this.Size.Height - moveFormNudgeValue);
                    break;
            }

            // Save changes
            AutoSubmitChangesToMainForm();
        }
        #endregion

        private void BtnShow_MouseEnter(object sender, EventArgs e)
        {
            ResolveBtnShowToolTip();
        }

        private void FocusContent()
        {
            tbNote.Focus();
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        #region SPLASH
        private void ShowSplash(bool customText, string text, bool matchOutputField, int matchDuration)
        {
            SplashTimeoutCounter = 0;
            if (matchOutputField)
            {
                SplashTimeoutCounterReference = matchDuration; // The duration to show the splash (1 or 2)
            }
            else
            {
                SplashTimeoutCounterReference = 1; // The duration to show the splash (must be 1 here, or else its too long)
            }

            if (matchOutputField)
            {
                lbSplash.Size = new Size(tbNote.Width, tbNote.Height);
                lbSplash.Location = new Point(tbNote.Left, tbNote.Top);
                lbSplash.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            }
            else
            {
                lbSplash.Dock = DockStyle.Fill;
            }

            lbSplash.TextAlign = ContentAlignment.MiddleCenter;
            lbSplash.Font = Design.FieldFont(MainForm.FieldsFontSize);

            if (TextBoxBackgroundColor == null)
            {
                lbSplash.BackColor = AhoyColors.CurrentColorTheme()[29];
                lbSplash.ForeColor = AhoyColors.CurrentColorTheme()[30];
            }
            else
            {
                lbSplash.BackColor = TextBoxBackgroundColor;
                lbSplash.ForeColor = TextBoxForegroundColor;
            }

            if (customText && text.Length > 0)
            {
                lbSplash.Text = text;
            }
            else
            {
                lbSplash.Text = "┌──────────┐" + Environment.NewLine;
                lbSplash.Text += "│  STICKY  │" + Environment.NewLine;
                lbSplash.Text += "└────" + StickyVersion + "┘";
            }
            lbSplash.Visible = true;
            lbSplash.BringToFront();
            if (matchOutputField)
            {
                btnResize.BringToFront();
            }
        }
        #endregion

        private void HideSplash()
        {
            lbSplash.Visible = false;
            lbSplash.SendToBack();
            if (FocusOnSpawn)
            {
                // Move cursor to last selected position
                tbNote.Focus();
                tbNote.Select(tbNote.Text.Length, 0);
                tbNote.ScrollToCaret();
            }
        }

        public void SetWindowPositionAndSize(int xpos, int ypos, int width, int height)
        {
            try
            {
                SnapOutOfMaximization();
                // Set window size
                Size newWindowSize = new Size(CheckForMinimumWidth(width), CheckForMinimumHeight(height));
                this.Size = newWindowSize;
                // Set window location
                Point newLocation = new Point(xpos, ypos);
                this.Location = newLocation;
            }
            catch
            {
                // Position is not verified set defaults
                SetDefaultWindowSizeAndLocation();
                Log.StickyError(10);
            }
        }

        private void SetDefaultWindowSizeAndLocation()
        {
            try
            {
                // Restore default window size and boarder style
                this.WindowState = FormWindowState.Normal;
                HideBorder();
                this.Size = new Size(MainForm.StickyDefaultWidth, MainForm.StickyDefaultHeight);
                this.CenterToScreen();
                this.Refresh();
                SubmitContent(true, false);
            }
            catch
            {
                Log.LogWindowStateChangeErrorSticky();
            }
        }

        private int CheckForMinimumWidth(int width)
        {
            if (width < this.MinimumSize.Width)
            {
                return this.MinimumSize.Width;
            }
            else
            {
                return width;
            }
        }

        private int CheckForMinimumHeight(int height)
        {
            if (height < this.MinimumSize.Height)
            {
                return this.MinimumSize.Height;
            }
            else
            {
                return height;
            }
        }

        private void SnapOutOfMaximization()
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
            catch
            {
                Log.LogWindowStateChangeErrorSticky();
            }
        }
        #endregion

        #region SHOW NOTE
        private void tbNote_MouseEnter(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void tbNote_Click(object sender, EventArgs e)
        {
            tbNote.Focus();
        }

        private void Sticky_Activated(object sender, EventArgs e)
        {
            // Do nothing
        }

        #endregion

        #region HIDE NOTE
        private void HideBorder()
        {
            SuspendLayout();
            this.FormBorderStyle = FormBorderStyle.None;
            ResumeLayout();
            Refresh();
        }
        #endregion

        #region TIMER
        private int SplashTimeoutCounterReference = 1;
        private void timerStickyTimer_Tick(object sender, EventArgs e)
        {
            // Splash
            if (lbSplash.Visible && !StickySleeping)
            {
                SplashTimeoutCounter += 1;
                if (SplashTimeoutCounter >= SplashTimeoutCounterReference)
                {
                    SplashTimeoutCounter = 0;
                    HideSplash();
                }
            }

            // Show close Sticky when ahoy is sleeping/awake
            if (StickySleeping)
            {
                CloseStickyWithoutRemoving();
            }

            if (FocusContentDelayed)
            {
                FocusContentDelayCounter += 1;
                if (FocusContentDelayCounter == 2)
                {
                    FocusContentDelayed = false;
                    FocusContentDelayCounter = 0;
                    FocusContent();
                }
            }

            if (AutoSubmitChanges)
            {
                AutoSubmitChangesCounter += 1; // (counts up by 2 pr second as the timer is set to 500ms)
                if (AutoSubmitChangesCounter >= 6) // 3 seconds
                {
                    AutoSubmitChanges = false;
                    SubmitContent(true, false);
                }
            }

            ResolveShowButton();
            // Check Status
            CheckStatus();
        }

        // Will make a timed submit
        private int AutoSubmitChangesCounter;
        private bool AutoSubmitChanges;
        private void AutoSubmitChangesToMainForm()
        {
            // Allow for calling this multiple times while running
            AutoSubmitChangesCounter = 0;
            AutoSubmitChanges = true;
        }

        private Color FormBackgroundColor;
        private Color TextBoxBackgroundColor;
        private Color TextBoxForegroundColor;
        private Color ToolTipBackgroundColor;
        private Color ToolTipForegroundColor;
        private void SetColorTheme()
        {
            if (ColorTheme == 0) // Uses form colors
            {
                SetInheritedTheme();
            }
            else if (ColorTheme == 1) // Uses inverted for colors
            {
                SetInheritedInvertedTheme();
            }
            else if (ColorTheme == 2) // Orange/Gray theme
            {
                SetOrangeTheme();
            }
            else if (ColorTheme == 3) // Yello theme (Default)
            {
                SetYelloTheme();
            }
            else if (ColorTheme == 4) // Blue theme
            {
                SetBlueTheme();
            }
            else if (ColorTheme == 5) // Green theme
            {
                SetGreenTheme();
            }
            else if (ColorTheme == 6) // Red theme
            {
                SetRedTheme();
            }
            else if (ColorTheme == 7) // Purple theme
            {
                SetPurpleTheme();
            }
            else
            {
                SetYelloTheme();
            }

            // Update controls
            UpdateColorTheme();
        }


        public bool GetHighlightByTheme()
        {
            switch (ColorTheme)
            {
                case 0:
                    return false;
                case 1:
                    return false;
                case 2:
                    return false;
                case 3:
                    return false;
                case 4:
                    return true;
                case 5:
                    return true;
                case 6:
                    return true;
                case 7:
                    return true;
                default:
                    return false;
            }
        }


        private void SetInheritedTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = AhoyColors.CurrentColorTheme()[0];
            TextBoxBackgroundColor = AhoyColors.CurrentColorTheme()[29];
            TextBoxForegroundColor = AhoyColors.CurrentColorTheme()[30];
            ToolTipBackgroundColor = AhoyColors.CurrentColorTheme()[1];
            ToolTipForegroundColor = AhoyColors.CurrentColorTheme()[3];
        }

        private void SetInheritedInvertedTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = AhoyColors.CurrentColorTheme()[0];
            TextBoxBackgroundColor = AhoyColors.CurrentColorTheme()[30];
            TextBoxForegroundColor = AhoyColors.CurrentColorTheme()[29];
            ToolTipBackgroundColor = AhoyColors.CurrentColorTheme()[1];
            ToolTipForegroundColor = AhoyColors.CurrentColorTheme()[3];
        }

        private void SetYelloTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(255, 242, 128);
            TextBoxBackgroundColor = FormBackgroundColor;
            TextBoxForegroundColor = Color.FromArgb(0, 0, 0);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void SetBlueTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(1, 146, 209);
            TextBoxBackgroundColor = FormBackgroundColor;
            TextBoxForegroundColor = Color.FromArgb(255, 255, 255);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void SetGreenTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(63, 137, 98);
            TextBoxBackgroundColor = FormBackgroundColor;
            TextBoxForegroundColor = Color.FromArgb(255, 255, 255);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void SetRedTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(168, 65, 65);
            TextBoxBackgroundColor = FormBackgroundColor;
            TextBoxForegroundColor = Color.FromArgb(255, 255, 255);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void SetPurpleTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(154, 82, 154);
            TextBoxBackgroundColor = FormBackgroundColor;
            TextBoxForegroundColor = Color.FromArgb(255, 255, 255);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void SetOrangeTheme()
        {
            // Ahoy selected theme
            FormBackgroundColor = Color.FromArgb(40, 40, 40);
            TextBoxBackgroundColor = Color.FromArgb(30, 30, 30);
            TextBoxForegroundColor = Color.FromArgb(255, 175, 0);
            ToolTipBackgroundColor = TextBoxBackgroundColor;
            ToolTipForegroundColor = TextBoxForegroundColor;
        }

        private void UpdateColorTheme()
        {
            // Opacity
            if (MainForm.FormOpacity > 0.0d)
            {
                this.Opacity = MainForm.FormOpacity;
            }

            // Get colors
            this.BackColor = FormBackgroundColor;
            tbNote.BackColor = TextBoxBackgroundColor;
            tbNote.ForeColor = TextBoxForegroundColor;
            tbNote.Font = Design.FieldFont(MainForm.FieldsFontSize);
            toolTip1.BackColor = ToolTipBackgroundColor;
            toolTip1.ForeColor = ToolTipForegroundColor;
            // Set form background image - OBS TEXTURES HAVE BEEN REMOVED AGAIN BECAUSE IT MAKES AESTHETICS TROUBLE
            //this.BackgroundImageLayout = ImageLayout.Tile;
            //this.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);

            // Replicate clors
            btnClose.Text = "";
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnClose.FlatAppearance.BorderColor = FormBackgroundColor;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.BackgroundImageLayout = ImageLayout.Tile;
            btnClose.BackgroundImage = ClearBitmap();

            btnMove.Text = "";
            btnMove.BackColor = Color.Transparent;
            btnMove.FlatStyle = FlatStyle.Flat;
            btnMove.FlatAppearance.BorderSize = 0;
            btnMove.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMove.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMove.FlatAppearance.BorderColor = FormBackgroundColor;
            btnMove.FlatAppearance.BorderSize = 0;
            btnMove.BackgroundImageLayout = ImageLayout.Tile;
            btnMove.BackgroundImage = MoveBitmap();

            btnHide.Text = "";
            btnHide.BackColor = Color.Transparent;
            btnHide.FlatStyle = FlatStyle.Flat;
            btnHide.FlatAppearance.BorderSize = 0;
            btnHide.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnHide.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnHide.FlatAppearance.BorderColor = FormBackgroundColor;
            btnHide.FlatAppearance.BorderSize = 0;
            btnHide.BackgroundImageLayout = ImageLayout.Tile;
            ResolveEyeButton(); // Resolve eye button bitmap

            btnShow.Text = "";
            btnShow.BackColor = Color.Transparent;
            btnShow.FlatStyle = FlatStyle.Flat;
            btnShow.FlatAppearance.BorderSize = 0;
            btnShow.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnShow.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnShow.FlatAppearance.BorderColor = FormBackgroundColor;
            btnShow.FlatAppearance.BorderSize = 0;
            btnShow.BackgroundImageLayout = ImageLayout.Tile;
            ResolveShowButton(); // Resolve eye button bitmap

            btnNew.Text = "";
            btnNew.BackColor = Color.Transparent;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNew.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNew.FlatAppearance.BorderColor = FormBackgroundColor;
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.BackgroundImageLayout = ImageLayout.Tile;
            btnNew.BackgroundImage = StickyBitmap();

            btnResize.Text = "";
            btnResize.BackColor = Color.Transparent;
            btnResize.FlatStyle = FlatStyle.Flat;
            btnResize.FlatAppearance.BorderSize = 0;
            btnResize.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnResize.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnResize.FlatAppearance.BorderColor = FormBackgroundColor;
            btnResize.FlatAppearance.BorderSize = 0;
            btnResize.BackgroundImageLayout = ImageLayout.Tile;
            btnResize.BackgroundImage = ResizeBitmap();

            btnTheme.Text = "";
            btnTheme.BackColor = Color.Transparent;
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnTheme.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnTheme.FlatAppearance.BorderColor = FormBackgroundColor;
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.BackgroundImageLayout = ImageLayout.Tile;
            btnTheme.BackgroundImage = ThemeBitmap();

            SubmitContent(true, false);
        }

        private Bitmap ClearBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.Clear();
            }
            else
            {
                return ButtonBitmaps.Clear(GetHighlightByTheme());
            }
        }

        private Bitmap MoveBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to theme 1, 2 and 3
            {
                return ButtonBitmaps.Move();
            }
            else
            {
                return ButtonBitmaps.Move(GetHighlightByTheme());
            }
        }

        private void ResolveEyeButton()
        {
            if (this.TopMost)
            {
                btnHide.BackgroundImage = OpenEyeBitmap();
            }
            else
            {
                btnHide.BackgroundImage = HalfOpenEyeBitmap();
            }
        }

        private void ResolveShowButton()
        {
            if (MainForm.MoreThanOneTopMostFalseStickiesExist())
            {
                btnShow.Visible = true;
                btnShow.BackgroundImage = ShiningEyeBitmap();
            }
            else
            {
                if (btnShow.Visible)
                {
                    btnShow.Visible = false;
                }
            }
        }

        private Bitmap ShiningEyeBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.ShiningEye();
            }
            else
            {
                return ButtonBitmaps.ShiningEye(GetHighlightByTheme());
            }
        }

        private Bitmap OpenEyeBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.OpenEye();
            }
            else
            {
                return ButtonBitmaps.OpenEye(GetHighlightByTheme());
            }
        }

        private Bitmap HalfOpenEyeBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.HalfOpenEye();
            }
            else
            {
                return ButtonBitmaps.HalfOpenEye(GetHighlightByTheme());
            }
        }


        private Bitmap StickyBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.Sticky();
            }
            else
            {
                return ButtonBitmaps.Sticky(GetHighlightByTheme());
            }
        }

        private Bitmap ResizeBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.Resize();
            }
            else
            {
                return ButtonBitmaps.Resize(GetHighlightByTheme());
            }
        }

        private Bitmap ThemeBitmap()
        {
            if (ColorTheme == 2 || ColorTheme == 3) // Specific to them 2 and 3
            {
                return ButtonBitmaps.Theme();
            }
            else
            {
                return ButtonBitmaps.Theme(GetHighlightByTheme());
            }
        }
        #endregion

        #region STATUS CODES
        private void CheckStatus()
        {
            // React if sticky status is not null
            if (MainForm.MasterCloseStickies)
            {
                // Close and save (Terminate)
                Log.StickyTerminated(true);
                SetStatus(MainForm.MasterCloseStickiesCode, false);
                IgnoreFormClosing = false;
                this.Close();
            }
            else if (MainForm.StickyStatusCode != 0 && !MainForm.StatusCodeCycleEnded() && StatuCodeAllowedToRunTwice())
            {
                switch (MainForm.StickyStatusCode)
                {
                    case 1: // Close Sticky while saving
                        Log.StickyClosed(true);
                        SetStatus(MainForm.StickyStatusCode, false);
                        IgnoreFormClosing = false;
                        this.Close(); // Values submitted on close
                        break;
                    case 2: // Close Sticky without saving
                        Log.StickyClosed(false);
                        SetStatus(MainForm.StickyStatusCode, false);
                        IgnoreFormClosing = true;
                        this.Close();  // Values not submitted on close
                        break;
                    case 3: // Reset location
                        try
                        {
                            Log.StickyCentered();
                            SetStatus(MainForm.StickyStatusCode, false);
                            this.TopMost = true; // Bring all to front by topmost
                            ResolveEyeButton();
                            ResolveShowButton();
                            SubmitContent(true, false); // Submit changes (this status is not submitted by other functions)
                            SetWindowPositionAndSize(MainForm.CurrentLocationX + MainForm.GetStickyOffset(), MainForm.CurrentLocationY + MainForm.GetStickyOffset(), MainForm.StickyDefaultWidth, MainForm.StickyDefaultHeight);
                        }
                        catch
                        {
                            SetStatus(0, false);
                            Log.StickyError(20);
                        }
                        break;
                    case 4: // Sleep stickies
                        Log.StickyHide();
                        SetStatus(MainForm.StickyStatusCode, false);
                        StickySleeping = true;
                        ShowSplash(false, "", false, 0); // Submitted values by timer
                        break;
                    case 5: // Bring to front
                        Log.StickyBringToFront();
                        this.BringToFront();
                        SetStatus(MainForm.StickyStatusCode, false);
                        SubmitContent(true, false);
                        if (!this.TopMost) // The ones that are not topmost should say hello
                        {
                            ShowSplash(true, "Hello!", true, 1);
                        }
                        break;
                    case 6: // Change colortheme
                        Log.StickyTheme();
                        SetStatus(MainForm.StickyStatusCode, false);
                        SetColorTheme(); // Values submitted by theme change
                        break;
                    case 7: // Change font
                        Log.StickyFont();
                        SetStatus(MainForm.StickyStatusCode, false);
                        UpdateFont();
                        break;
                }

                PreviousStatusCode = MainForm.StickyStatusCode;
            }
        }

        private void UpdateFont()
        {
            lbSplash.Font = Design.FieldFont(MainForm.FieldsFontSize);
            tbNote.Font = Design.FieldFont(MainForm.FieldsFontSize);
        }

        private bool StatuCodeAllowedToRunTwice()
        {
            // Status codes only allowed to run once: 1, 2 and 4
            if (PreviousStatusCode == MainForm.StickyStatusCode && (MainForm.StickyStatusCode == 1 || MainForm.StickyStatusCode == 2 || MainForm.StickyStatusCode == 4))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        private void SetStatus(int code, bool completed)
        {
            StatusCode = code;
            StatusComplete = completed;
        }

        private void tbNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.A)) // Multiple shortcut keypress (Open command prompt)
            {
                MainForm.OpenCommandPromptPerRemote();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CloseAndDelete();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                NewSticky();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                HideStickies();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                ShowStickies();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                tbNote.PasteUnformatted();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                tbNote.CopyUnformatted();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                tbNote.CutUnformatted();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        // Call the SubmitContent
        private void tbNote_TextChanged(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            StickyText = tbNote.Text;
            AutoSubmitChangesToMainForm();
        }

        // Submit content
        private void SubmitContent(bool open, bool remove)
        {
            if (!IgnoreChanges)
            {
                MainForm.SubmitStickyContent(this.ID, tbNote.Text, this.Width, this.Height, this.Left, this.Top, ColorTheme, this.TopMost, open, remove, StatusCode, StatusComplete);
            }
        }

        private void Sticky_ResizeEnd(object sender, EventArgs e)
        {
            SubmitContent(true, false);
        }

        private void Sticky_Move(object sender, EventArgs e)
        {
            // Do nothing            
        }

        private void tbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ShellCommands.BrowseInternet(e.LinkText, MainForm.SelectedInternetBrowser);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseAndDelete();
        }

        private void CloseAndDelete()
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Delete this Sticky?", MessageText.StickyCaption(), false, false);
            msg.TopMost = true;
            msg.ShowDialog();

            if (msg.Accepted)
            {
                IgnoreFormClosing = true;
                SubmitContent(false, true); // Remove sticky
                this.Close();
            }
        }

        private void CloseStickyWithoutRemoving()
        {
            IgnoreFormClosing = true;
            SubmitContent(false, false);
            this.Close();
        }

        private bool IgnoreFormClosing { get; set; }
        private void Sticky_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IgnoreFormClosing)
            {
                SubmitContent(false, false); // Tell main form that the Sticky is closed
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            // Do nothing
        }




        private bool mouseDown;
        private Point lastLocation;
        //private bool canMove;
        private void tbNote_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void tbNote_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void tbNote_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            SubmitContent(true, false); // Save position
            FocusContent();
        }


        bool allowResize;
        private void btnResize_MouseDown(object sender, MouseEventArgs e)
        {
            allowResize = true;
        }

        private void btnResize_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (allowResize)
                {
                    this.Height = btnResize.Top + e.Y;
                    this.Width = btnResize.Left + e.X;
                    this.Update();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void btnResize_MouseUp(object sender, MouseEventArgs e)
        {
            allowResize = false;
            SubmitContent(true, false); // Save position
            FocusContent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NewSticky();
            }
            else if (e.Button == MouseButtons.Right)
            {
                HideStickies();
            }
        }

        private void NewSticky()
        {
            // Tell main form to spawn new stickie and send position data
            MainForm.SpawnStickyPerRemote(this.Left + 20, this.Top + 20);
        }

        private void ShowStickies()
        {
            MainForm.BringStickiesToFront();
        }

        private void HideStickies()
        {
            // Tell mainform to hide all stickies
            MainForm.HideStickiesRemote();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void btnHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // If top-most set topmost to false
                if (this.TopMost)
                {
                    this.TopMost = false;
                    ShowSplash(true, "Top-Most: OFF\n(Right Click to Hide)", true, 2);
                }
                else
                {
                    this.TopMost = true;
                    ShowSplash(true, "Top-Most: ON\n(Right Click to Hide)", true, 2);
                }

                ResolveEyeButton();
                SubmitContent(true, false);
                ResolveShowButton();
                FocusContent();
            }
            else if (e.Button == MouseButtons.Right)
            {
                HideStickies();
            }
        }


        private void btnShow_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private bool FocusContentDelayed { get; set; }
        private int FocusContentDelayCounter { get; set; }
        private void FocusContentWithDelayed()
        {
            FocusContentDelayed = true;
            FocusContentDelayCounter = 0;
        }

        private void btnShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MainForm.MoreThanOneTopMostFalseStickiesExist())
                {
                    MainForm.BringStickiesToFront();
                }

                FocusContentWithDelayed();
            }
            else if (e.Button == MouseButtons.Right)
            {
                HideStickies();
            }
        }

        private void ResolveBtnShowToolTip()
        {
            this.toolTip1.SetToolTip(this.btnShow, GetStickyCountForToolTip());

        }

        private string tempStickyCountText;
        private string GetStickyCountForToolTip()
        {
            if (MainForm.TopMostFalseStickiesExist())
            {
                tempStickyCountText = "";
                if (MainForm.GetStickyCount() == 1)
                {
                    tempStickyCountText = MainForm.GetStickyCount() + " Sticky";
                }
                else if (MainForm.GetStickyCount() > 1)
                {
                    tempStickyCountText = MainForm.GetStickyCount() + " Stickies";
                }
                else
                {
                    tempStickyCountText = "Stickies";
                }

                return "Bring " + tempStickyCountText + " To Front - Right Click To Hide All";
            }
            else
            {
                return "Bring Stickies To Front - Right Click To Hide All";
            }
        }


        private void btnTheme_Click(object sender, EventArgs e)
        {
            // Do nothing
        }


        private void btnTheme_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NextColorTheme();
            }
            else if (e.Button == MouseButtons.Right)
            {
                PreviousColorTheme();
            }
        }

        private void NextColorTheme()
        {
            if (ColorTheme < 7)
            {
                ColorTheme += 1;
            }
            else
            {
                ColorTheme = 0;
            }
            SetColorTheme();
            FocusContent();
        }

        private void PreviousColorTheme()
        {
            if (ColorTheme > 0)
            {
                ColorTheme -= 1;
            }
            else
            {
                ColorTheme = 7;
            }
            SetColorTheme();
            FocusContent();
        }

        #region TEXT DECORATION
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

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 3);
        }

        private void toUPPERCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUpper();
        }

        private void toLowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToLower();
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBullet(false);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUnderline(false);
        }


        private void SelectionToBullet(bool caps)
        {
            if (tbNote.SelectedText.Length > 0)
            {
                tbNote.SelectedText = TextTransform.SelectionToBullet(tbNote.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.StickyCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToUnderline(bool caps)
        {
            if (tbNote.SelectedText.Length > 0)
            {
                tbNote.SelectedText = TextTransform.SelectionToUnderline(tbNote.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.StickyCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToUpper()
        {
            if (tbNote.SelectedText.Length > 0)
            {
                tbNote.SelectedText = tbNote.SelectedText.ToUpper();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.StickyCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToLower()
        {
            if (tbNote.SelectedText.Length > 0)
            {
                tbNote.SelectedText = tbNote.SelectedText.ToLower();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.StickyCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region Check form position (addition 22-03-2018)
        bool isInsideBounds;
        public bool IsOnScreen(Form form)
        {
            try
            {
                Log.StickyStatus("Checking screen bounds");
                isInsideBounds = false;

                // Create rectangle
                Rectangle formRectangle = new Rectangle(form.Left, form.Top, form.Width, form.Height);
                // Test
                isInsideBounds = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(formRectangle));

                if (!isInsideBounds)
                {
                    Log.StickyStatus("Outside bounds");
                }

                return isInsideBounds;
            }
            catch
            {
                Log.StickyError("Sticky screen bounds check");
                return true;
            }
        }
        #endregion

        #region Sort selection
        private void sortSelectionAZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortSelection(false);
        }

        private void sortSelectionZAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortSelection(true);
        }

        private void SortSelection(bool direction)
        {
            if (tbNote.SelectedText.Length > 0)
            {
                if (!direction)
                {
                    tbNote.SelectedText = TextTransform.SortText(tbNote.SelectedText, false);
                }
                else if (direction)
                {
                    tbNote.SelectedText = TextTransform.SortText(tbNote.SelectedText, true);
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToPerformTextOperation(), MessageText.TextOperationCaption(), false);
                msg.ShowDialog();
            }
        }
        #endregion
    }
}
