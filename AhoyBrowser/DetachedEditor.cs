using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace AhoyBrowser
{
    public partial class DetachedEditor : Form
    {
        // Default editor size
        public const int UEMinimumHeight = 325;
        public const int UEMinimumWidth = 650;
        public const int UEDefaultHeight = 500;
        public const int UEDefaultWidth = 675;
        public const int UEDefaultLocationXOffset = -75;
        public const int UEDefaultLocationYOffset = 70;
        public const double UEDefaultOpacity = 1.0d;
        public const int UEDefaultBank = 4;
        public const bool UEDefaultTopMost = false;
        public static int ParentLocationX { get; set; }
        public static int ParentLocationY { get; set; }
        public static int ParentWidth { get; set; }
        public static int ParentHeight { get; set; }
        public bool TransferToCurrentEntry { get; set; }
        public bool TransferToNewEntry { get; set; }
        public bool ReplaceWithCurrentEntry { get; set; }
        public bool LoadPinnedEntry { get; set; }
        public double FormOpacity { get; set; }
        public int VersionSelection { get; set; }
        public string VersionTextA { get; set; }
        public string VersionTextB { get; set; }
        public string VersionTextC { get; set; }
        public string VersionTextD { get; set; }
        public int CaretPositionA { get; set; }
        public int CaretPositionB { get; set; }
        public int CaretPositionC { get; set; }
        public int CaretPositionD { get; set; }
        public bool IsPlopEditor { get; set; }
        public string PlopLinkedID { get; set; }
        public string PlopLinkedTitle { get; set; }
        public string PlopLinkedOriginalContent { get; set; }
        private int PlopLinkedPresetColor { get; set; }
        private string PlopLinkedCustomColor { get; set; }
        public bool PlopIsLinked { get; set; }
        public bool PlopWriteProtected { get; set; }
        public bool IsTopMost { get; set; }
        private bool SplashDisplayed { get; set; }
        private bool PlopUtilityEditorExpanded { get; set; }
        private static bool NotificationEditorOpen { get; set; }
        private static bool LockedForPositioning { get; set; }
        private static string UETitle = MessageText.UtilityEditorCaption();
        private static string PlopTitle = MessageText.PlopEditorCaption();
        private bool RefocusContent { get; set; }
        private bool RefreshScrollbars { get; set; }
        private Timer timerDelayText;
        private bool InvokeEntryCheck { get; set; } // Used by the main form to update the PLOP editor when changing colors, deleting or locking/unlocking entry
        private Color LinkBlinkerForeground;
        private Color LinkBlinkerBackground;
        private Color FormBlinkerA;
        private Color FormBlinkerB;
        private Color FormBlinkerC;
        private Color ExpansionColorA;
        private Color ExpansionColorB;

        private bool InvokeAutoSave;
        private int AutoSaveCounter;

        public DetachedEditor(int xpos, int ypos, int width, int height, FormWindowState windowState, double formOpacity, int versionSelection, bool detectURLs, bool isPlopEditor, Entry linkedEntry, bool windowBorder)
        {
            LockedForPositioning = true;
            InitializeComponent();
            InvokeAutoSave = false;
            AutoSaveCounter = 0;
            // Window border
            if (!windowBorder)
            {
                WindowBorder = false;
                HideFormBorder();
            }
            else
            {
                WindowBorder = true;
                ShowFormBorder();
            }
            // Load event
            this.Load += DetachedEditor_Load;
            timerBlinker.Interval = 1000;
            timerBlinker.Enabled = false;
            InvokeEntryCheck = false;
            RefocusContent = false;
            this.KeyPreview = true;
            this.KeyDown += DetachedEditor_KeyDown;
            this.Move += DetachedEditor_Move;
            VersionSelection = 4; // Select Temp by default
            if (versionSelection == 0)
            {
                versionSelection = VersionSelection;
            }
            InitializeTextBoxes(detectURLs);
            // Set PLOP variables
            SplashDisplayed = true;
            IsPlopEditor = isPlopEditor;
            PlopIsLinked = false;
            PlopLinkedID = "";
            IsTopMost = false;
            PlopUtilityEditorExpanded = false;
            NotificationEditorOpen = false;
            timerGlobalTimer.Interval = 1000;
            timerGlobalTimer.Enabled = false;

            // Calculator
            microCalculator1.SetVersion(1);
            microCalculator1.HideControlWithoutSavingState();

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Initialize properties
            VersionTextA = "";
            VersionTextB = "";
            VersionTextC = "";
            VersionTextD = "";
            CaretPositionA = 0;
            CaretPositionB = 0;
            CaretPositionC = 0;
            CaretPositionD = 0;
            LockedForPositioning = true;
            tbSearchInContent.ContextMenuStrip = contextMenuBasic;
            tbSearchInContent.GotFocus += TbSearchInContent_GotFocus;
            tbPlopUtilityText.AcceptsTab = true;
            lbContentSearch.BackColor = Color.Transparent;
            lbOptions.BackColor = Color.Transparent;
            lbOptions.Text = "PLOP Settings";

            // Sticky "button" (it's a panel)
            pnlSpawnSticky.BackgroundImage = Properties.Resources.sticky1;
            pnlSpawnSticky.BackgroundImageLayout = ImageLayout.Center;
            pnlSpawnSticky.BringToFront(); // (in front of top menu)
            pnlShowAllStickies.BackgroundImage = Properties.Resources.sticky1;
            pnlShowAllStickies.BackgroundImageLayout = ImageLayout.Center;
            pnlShowAllStickies.BringToFront(); // (in front of top menu)
            pnlShowAllStickies.Visible = false; // Hide from the start
            pnlShowAllStickies.MouseEnter += PnlShowAllStickies_MouseEnter;

            // Search field
            tbSearchInContent.Font = Design.SearchFieldFont();

            // Plop Utility field
            tbPlopUtilityText.Font = Design.ContentFieldFont();
            tbPlopUtilityText.DetectUrls = detectURLs;
            tbPlopUtilityText.Click += TbPlopUtilityText_Click;
            tbPlopUtilityText.LinkClicked += TbContentA_LinkClicked;
            //SetColorTheme();
            toolStripStatusLabel1.Text = "Ready";
            this.CenterToParent();
            this.FormClosing += DetachedEditor_FormClosing;
            this.MinimumSize = new Size(UEMinimumWidth, UEMinimumHeight); // Set minimum size
            SetWindowPositionAndSize(xpos, ypos, width, height, windowState, formOpacity);
            ResetSpecialCommandVariables();

            if (!IsOnScreen(this))
            {
                Log.UEEditor("UE/PLOP: Adjusting location...");
                RestoreUtilityEditorView();
            }

            // Retrieve version text and make selection -TO AVOID MEMORY ERROR (UE only, PLOP is set through the splash timer)
            // -to avoid memory exception when setting text in the constructor
            timerDelayText = new Timer();
            timerDelayText.Tick += TimerDelayText_Tick;
            timerDelayText.Interval = 300;
            timerDelayText.Enabled = false;

            // Set text version
            VersionSelection = versionSelection;


            // Ready tb's for search
            tbContentA.HideSelection = false;
            tbContentB.HideSelection = false;
            tbContentC.HideSelection = false;
            tbContentD.HideSelection = false;
            ResetContentSearchVariables();

            marqueePlop.Visible = false;
            btnPlop.Visible = false;
            btnClose.Visible = false;
            tbPlopUtilityText.Visible = false;
            btnLink.Visible = false;
            btnCallUp.Visible = false;
            btnTopMost.Visible = false;
            btnClear.Visible = false;
            btnCopy.Visible = false;
            btnCopyAtoB.Visible = false;
            btnCopyBtoC.Visible = false;
            btnCopyTempToA.Visible = false;
            btnLoadPinnedEntry.Visible = false;
            btnNewEntry.Visible = false;
            btnRetrieve.Visible = false;
            btnTransfer.Visible = false;
            btnVersionA.Visible = false;
            btnVersionB.Visible = false;
            btnVersionC.Visible = false;
            btnVersionTemp.Visible = false;
            btnReplaceCurrentEntry.Visible = false;
            toolStripMenuItem5.Visible = false;
            clearPLOPContentToolStripMenuItem.Visible = false;
            clearPLOPContentAndUtilityToolStripMenuItem.Visible = false;
            // Hide settings panel
            pnlSettings.Visible = false;
            ResolveTogglePlopDirectionButton();
            ResolveAdditionalSettingsButtons();
            lbPlopDirection.BackColor = Color.Transparent;
            lbBlankLines.BackColor = Color.Transparent;
            lbTrim.BackColor = Color.Transparent;
            lbEscToClose.BackColor = Color.Transparent;

            // Set PLOP controls
            if (IsPlopEditor)
            {
                // Splash label
                lbSplash.Dock = DockStyle.Fill;

                marqueePlop.Visible = true;
                marqueePlop.SetTextAlignment(ContentAlignment.MiddleCenter);
                marqueePlop.SetTestMode(false);
                marqueePlop.SetPauseDuration(175);

                this.Text = PlopTitle;
                btnLink.Text = "No link";
                lbPlopDirection.Text = "PLOP to ";

                btnPlop.Visible = true;
                btnClose.Visible = true;
                tbPlopUtilityText.Visible = true;
                btnLink.Visible = true;
                btnCallUp.Visible = true;
                btnTopMost.Visible = true;
                clearToolStripMenuItem.Visible = false;
                clearAllMemoBanksToolStripMenuItem.Visible = false;
                toolStripMenuItem2.Visible = false;

                toolStripMenuItem5.Visible = true;
                clearPLOPContentToolStripMenuItem.Visible = true;
                clearPLOPContentAndUtilityToolStripMenuItem.Visible = true;
                picGear.Visible = true;
            }
            else
            {
                // Hide splash label
                lbSplash.Visible = false;
                lbPlopDirection.Text = "";

                this.Text = UETitle;
                btnClose.Visible = true;
                btnClear.Visible = true;
                btnCopy.Visible = true;
                btnCopyAtoB.Visible = true;
                btnCopyBtoC.Visible = true;
                btnCopyTempToA.Visible = true;
                btnLoadPinnedEntry.Visible = true;
                btnNewEntry.Visible = true;
                btnRetrieve.Visible = true;
                btnTransfer.Visible = true;
                btnVersionA.Visible = true;
                btnVersionB.Visible = true;
                btnVersionC.Visible = true;
                btnVersionTemp.Visible = true;
                btnReplaceCurrentEntry.Visible = true;
                picGear.Visible = false;
            }

            // Check plop link
            if (IsPlopEditor && linkedEntry != null)
            {
                UpdatePlopLinkVariables(linkedEntry);
                PlopLinkedPresetColor = linkedEntry.PresetColor; // Must be set here, and not inside UpdatePlopLinkVariables above
                PlopLinkedCustomColor = linkedEntry.CustomColor;
                CheckPlopLink();
                ResolveLinkButtonBlinker();
            }

            // Color theme
            SetColorTheme();

            // Update fonts
            UpdateFontSize();

            // Update context menu with sticky count
            SetStickyCount();
            // Scrollbars
            RefreshScrollbars = true;

            // Plop splash
            if (IsPlopEditor)
            {
                DisplayPlopSplashLogo();
            }

            LockedForPositioning = false;
            // Enable the global timer
            timerGlobalTimer.Enabled = true;
        }

        // Save changes when moving the form
        private void DetachedEditor_Move(object sender, EventArgs e)
        {
            SubmitDataToMainForm();
        }

        // Upate pr. remote
        internal void UpdatePLOPLinkData()
        {
            InvokeEntryCheck = true;
        }

        // Check up on linked entry
        private Entry LinkedEntry;
        private void RetrievePlopEntryState()
        {
            if (IsPlopEditor && PlopLinkedID.Length > 0)
            {
                LinkedEntry = MainForm.GetEntryByID(PlopLinkedID);

                // Check current entry
                if (LinkedEntry != null)
                {
                    // Check entry still exists by current ID from main form
                    // If not de-link plop
                    if (LinkedEntry.ID != PlopLinkedID)
                    {
                        PlopLinkedPresetColor = 0;
                        PlopLinkedCustomColor = "";
                        SetPlopLink(false);
                    }
                    else
                    {
                        UpdatePlopLinkVariables(LinkedEntry);
                    }

                    // Check color status
                    // and update color theme if changed
                    if (LinkedEntry.PresetColor > 0 && LinkedEntry.PresetColor != PlopLinkedPresetColor)
                    {
                        PlopLinkedPresetColor = LinkedEntry.PresetColor;
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor));
                    }
                    else if (LinkedEntry.PresetColor != PlopLinkedPresetColor)
                    {
                        PlopLinkedPresetColor = LinkedEntry.PresetColor;
                        SetColorTheme();
                    }

                    if (AhoyColors.HasDualHexValue(LinkedEntry.CustomColor) && LinkedEntry.CustomColor != PlopLinkedCustomColor)
                    {
                        PlopLinkedCustomColor = LinkedEntry.CustomColor;
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false));
                    }
                    else if (LinkedEntry.CustomColor != PlopLinkedCustomColor)
                    {
                        PlopLinkedCustomColor = LinkedEntry.CustomColor;
                        SetColorTheme();
                    }
                }
                else
                {
                    SetPlopLink(false);
                    PlopLinkedPresetColor = 0;
                    PlopLinkedCustomColor = "";
                    SetColorTheme();
                    SetPlopMarqueeText();
                }

                ResolveTopMostButton();
                ResolveLinkButtonBlinker();
            }
        }

        private void UpdatePlopLinkVariables(Entry linkedEntry)
        {
            PlopLinkedID = linkedEntry.ID;
            PlopLinkedTitle = linkedEntry.Title;
            PlopLinkedOriginalContent = linkedEntry.Content;
            //PlopLinkedStatusColor = linkedEntry.Status; // Must not be set here
            PlopWriteProtected = linkedEntry.Protected;
            PlopIsLinked = true;
            CheckPlopLink();
        }


        private void TimerDelayText_Tick(object sender, EventArgs e)
        {
            timerDelayText.Enabled = false;
            SelectTextVersion(VersionSelection);
            TransferTextAndCaretPositionsToEditor(); // Caret position only for UE, not PLOP
        }

        private void DetachedEditor_Load(object sender, EventArgs e)
        {
            // Retrieve version text and make selection for UE (not for splash, this is handled through the Splash timed dialog)
            // -to avoid memory exception when setting text in the constructor
            if (!IsPlopEditor)
            {
                timerDelayText.Enabled = true;
            }
        }

        private void ResolveScrollbars()
        {
            SuspendLayout();
            // Must be set first to make changes happen
            tbContentA.ScrollBars = RichTextBoxScrollBars.None;
            tbContentB.ScrollBars = RichTextBoxScrollBars.None;
            tbContentC.ScrollBars = RichTextBoxScrollBars.None;
            tbContentD.ScrollBars = RichTextBoxScrollBars.None;
            tbPlopUtilityText.ScrollBars = RichTextBoxScrollBars.None;

            // Show/Hide scrollbars
            if (Design.EnforceScrollbars)
            {
                tbContentA.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbContentB.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbContentC.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbContentD.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbPlopUtilityText.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            }
            else
            {
                tbContentA.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbContentB.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbContentC.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbContentD.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbPlopUtilityText.ScrollBars = RichTextBoxScrollBars.Vertical;
            }
            ResumeLayout();
        }

        // Duplicate selected text (called from the tbContent keydown)
        private void DuplicateSelection(HistoryTextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                try
                {
                    if (textBox.SelectedText.Length > 0)
                    {
                        textBox.Copy(); // Copy selection
                        textBox.DeselectAll(); // Deselect
                    }
                    textBox.Paste(); // Paste
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        private void DetachedEditor_KeyDown(object sender, KeyEventArgs e)
        {
            Reaction();
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.W)) // Multiple shortcut keypress
            {
                ToggleMicroCalculator();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Enter)) // Multiple shortcut keypress
            {
                MainForm.StartMagicBlockerPerRemote(); // Start magic blocker
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.D)) // Multiple shortcut keypress
            {
                DuplicateSelection(GetCurrentTextBox());
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.R)) // Multiple shortcut keypress
            {
                findAndReplaceToolStripMenuItem_Click(this, null);
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.W)) // Multiple shortcut keypress
            {
                picGear_Click(this, null);
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.N)) // Multiple shortcut keypress
            {
                CreateNewEntry();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.F)) // Search main form
            {
                SearchMainForm();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.O)) // Open Pool from dialog
            {
                OpenPoolMainForm();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.I)) // Pin/Unpin selected entry (main form)
            {
                PinUnpinMainForm();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Q)) // Open quickguide
            {
                OpenQuickGuide();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.E)) // Multiple shortcut keypress
            {
                ActivateMainForm();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Escape))
            {
                if (IsPlopEditor)
                {
                    CloseSettingsPanel();
                }
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
            SubmitDataToMainForm();
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
            SubmitDataToMainForm();
        }
        #endregion

        private void ActivateMainForm()
        {
            Reaction();
            MainForm.ActivateMainForm = true;
        }

        private void CreateNewEntry()
        {
            Reaction();
            MainForm.NewEntryFromUE = true;
        }

        private void SearchMainForm()
        {
            Reaction();
            MainForm.SearchFromUE = true;
        }

        private void OpenPoolMainForm()
        {
            Reaction();
            MainForm.OpenPoolFromUE = true;
        }

        private void PinUnpinMainForm()
        {
            Reaction();
            MainForm.TogglePinFromUE = true;
        }

        private void OpenQuickGuide()
        {
            Reaction();
            MainForm.OpenQuickGuideFromUE = true;
        }

        private void Reaction()
        {
            MainForm.KeepAwake();
        }

        #region RE-DRAW While resizing issue fix
        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }
        #endregion

        private void InitializeTextBoxes(bool detectURLs)
        {
            tbContentA.Font = Design.ContentFieldFont();
            tbContentB.Font = Design.ContentFieldFont();
            tbContentC.Font = Design.ContentFieldFont();
            tbContentD.Font = Design.ContentFieldFont();
            tbContentA.Click += TbContentA_Click;
            tbContentB.Click += TbContentA_Click;
            tbContentC.Click += TbContentA_Click;
            tbContentD.Click += TbContentA_Click;
            tbContentA.DetectUrls = detectURLs;
            tbContentB.DetectUrls = detectURLs;
            tbContentC.DetectUrls = detectURLs;
            tbContentD.DetectUrls = detectURLs;
            tbContentA.LinkClicked += TbContentA_LinkClicked;
            tbContentB.LinkClicked += TbContentA_LinkClicked;
            tbContentC.LinkClicked += TbContentA_LinkClicked;
            tbContentD.LinkClicked += TbContentA_LinkClicked;
            // Text changed is set in the designer
            tbContentA.AcceptsTab = true;
            tbContentB.AcceptsTab = true;
            tbContentC.AcceptsTab = true;
            tbContentD.AcceptsTab = true;

            // Show/Hide scrollbars
            ResolveScrollbars();
        }

        private void TbSearchInContent_GotFocus(object sender, EventArgs e)
        {
            if (tbSearchInContent.Text.Length > 0)
            {
                tbSearchInContent.SelectAll();
            }
        }

        private void TbPlopUtilityText_Click(object sender, EventArgs e)
        {
            PerformWordCountUtilityWindow(true);
        }

        private void SetMarquee(string message)
        {
            if (message.Length > 0)
            {
                if (!marqueePlop.IsRunning())
                {
                    marqueePlop.StartLightShow(message, 35, false);
                }
                else
                {
                    marqueePlop.SetDisplayText(message, 35);
                }
            }
        }


        public void SubmitNewOriginalLinkedPlupContent(string newContent)
        {
            PlopLinkedOriginalContent = newContent;
        }

        public void SubmitNewOriginalLinkedPlupTitle(string newContent)
        {
            PlopLinkedTitle = newContent;
            MarqueeSetForLinked = false; // Allow setting marquee again
            UpdateUI(2000);
        }


        private void TbContentA_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ShellCommands.BrowseInternet(e.LinkText, MainForm.SelectedInternetBrowser);
        }

        private void TbContentA_Click(object sender, EventArgs e)
        {
            PerformWordCountMainWindow(true);
            if (!IsPlopEditor)
            {
                SaveCaretPositions();
            }
            else if (IsPlopEditor)
            {
                // Only call method if expanded
                if (PlopUtilityEditorExpanded)
                {
                    TogglePlopUtilityExpansion();
                }
            }
        }

        public void ResolveTexture()
        {
            if (MainForm.IsTextureEnabled())
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);

                // Set labels to transparent
                lbContentSearch.BackColor = Color.Transparent;
            }
            else
            {
                this.BackgroundImage = null;
            }
        }


        public bool HasFocus()
        {
            if (GetCurrentTextBox().Focused)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SelectTextVersion(int number)
        {
            if (number > 0)
            {
                VersionSelection = number;
            }
            else
            {
                VersionSelection = UEDefaultBank;
            }
            ResolveSelectedVersion();
        }


        private void CycleTextVersionBackwards()
        {
            if (VersionSelection > 1)
            {
                VersionSelection -= 1;
            }
            else
            {
                VersionSelection = 4;
            }
            ResolveSelectedVersion();
        }

        private void NextVersion()
        {
            if (VersionSelection < 4)
            {
                VersionSelection += 1;
            }
            else
            {
                VersionSelection = 1;
            }

            ResolveSelectedVersion();
        }

        private void PreviousVersion()
        {
            if (VersionSelection > 1)
            {
                VersionSelection -= 1;
            }
            else
            {
                VersionSelection = 4;
            }

            ResolveSelectedVersion();
        }

        public static int GetParentLocationXWithOffset()
        {
            return ParentLocationX + UEDefaultLocationXOffset;
        }

        public static int GetParentLocationYWithOffset()
        {
            return ParentLocationY + UEDefaultLocationYOffset;
        }

        internal void SetParentWindowLocationDefaults(int xpos, int ypos, int with, int height)
        {
            ParentLocationX = xpos;
            ParentLocationY = ypos;
            ParentWidth = with;
            ParentHeight = height;
        }

        private void ResetSpecialCommandVariables()
        {
            TransferToNewEntry = false;
            ReplaceWithCurrentEntry = false;
            LockedForPositioning = false;
            LoadPinnedEntry = false;
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void DetachedEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SubmitDataToMainForm();
        }

        private int CheckForMinimumWidth(int width)
        {
            if (width < UEMinimumWidth)
            {
                return UEMinimumWidth;
            }
            else
            {
                return width;
            }
        }

        private int CheckForMinimumHeight(int height)
        {
            if (height < UEMinimumHeight)
            {
                return UEMinimumHeight;
            }
            else
            {
                return height;
            }
        }

        public void SetWindowPositionAndSize(int xpos, int ypos, int width, int height, FormWindowState windowState, double opacity)
        {
            try
            {
                // Lock while setting size
                LockedForPositioning = true;
                if (windowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                    SetOpacity(opacity);
                }
                else
                {
                    SnapOutOfMaximization();

                    // Set window size
                    Size newWindowSize = new Size(CheckForMinimumWidth(width), CheckForMinimumHeight(height));
                    this.Size = newWindowSize;
                    // Set window location
                    Point newLocation = new Point(xpos, ypos);
                    this.Location = newLocation;
                    SetOpacity(opacity);
                }
                // Unlock
                LockedForPositioning = false;
            }
            catch
            {
                Log.LogWindowStateChangeErrorUE();
            }
            finally
            {
                LockedForPositioning = false;
            }
        }

        private void RestoreUtilityEditorView()
        {
            SnapOutOfMaximization();
            SetWindowPositionAndSize(GetParentLocationXWithOffset(), GetParentLocationYWithOffset(), UEDefaultWidth, UEDefaultHeight, FormWindowState.Normal, UEDefaultOpacity);
            ToggleTopMost();
            MainForm.InvokeSaveSettings();
        }

        private void SubmitDataToMainForm()
        {
            if (!LockedForPositioning)
            {
                try
                {
                    MainForm.UEWindowState = this.WindowState;
                    MainForm.UEPositionX = this.Location.X;
                    MainForm.UEPositionY = this.Location.Y;
                    MainForm.UEHeight = this.Size.Height;
                    MainForm.UEWidth = this.Size.Width;
                    MainForm.UEOpacity = FormOpacity;
                    MainForm.UEVersionSelection = VersionSelection;
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        MainForm.UERestoreBoundsWidth = this.RestoreBounds.Width;
                        MainForm.UERestoreBoundsHeight = this.RestoreBounds.Height;
                    }
                    else
                    {
                        MainForm.UERestoreBoundsWidth = UEDefaultWidth;
                        MainForm.UERestoreBoundsHeight = UEDefaultHeight;
                    }
                    MainForm.InvokeSaveSettings();
                }
                catch
                {
                    Log.LogWindowStateChangeErrorUE();
                }
            }
        }

        const string tempPlopStart = "// PLOP!";
        const string tempPlopEnd = "";
        static string tempPlopContent = "";
        public void SubmitPlopToMainForm()
        {
            // Refresh activity counters
            Reaction();
            if (IsPlopEditor && tbContentD.Text.Length > 0)
            {
                Log.UEEditor("PLOP: " + PlopTitle);
                tempPlopContent = Environment.NewLine;
                tempPlopContent += tempPlopStart + " [" + IO.GetIsoDateTimeNow() + "]" + TextTransform.GetNewLines(2);
                if (MainForm.UETrimWhitespace)
                {
                    tempPlopContent += tbContentD.Text.Trim() + Environment.NewLine;
                }
                else
                {
                    tempPlopContent += tbContentD.Text + Environment.NewLine;
                    tempPlopContent += tempPlopEnd + TextTransform.GetNewLines(4);
                }

                // Submit the new Plop with the original content attached
                if (MainForm.PlopDirection)
                {
                    MainForm.PlopContent = tempPlopContent + PlopLinkedOriginalContent;
                }
                else
                {
                    MainForm.PlopContent = PlopLinkedOriginalContent + tempPlopContent;
                }

                // Update the Plop original content with the new content
                PlopLinkedOriginalContent = MainForm.PlopContent;
                MainForm.SubmitPlopContent = true;
                tbContentD.Clear();
                MainForm.PlopPersistedContent = ""; // Clear persistet content after plopping
                InsertNewLines(tbContentD);
            }
        }

        // Insert empty lines / blank lines / blanklines
        private void InsertNewLines(HistoryTextBox textbox)
        {
            if (MainForm.UEBlankLines)
            {
                textbox.SetText(textbox.Text + TextTransform.GetNewLines(15), false);
            }
        }

        // Have the main form select the linked entry
        public void SubmitCallUpRequest()
        {
            Log.PlopCallUpEntry();
            MainForm.PlopCallUpEntry = true;
        }

        public void SubmitTextVersionsToMainForm()
        {
            // Count up autosave counter
            AutoSaveCounter += 1;

            if (IsPlopEditor)
            {
                if (tbContentD.Text.Length > 0)
                {
                    MainForm.PlopPersistedContent = tbContentD.Text;
                }
                MainForm.PlopUtilityContent = tbPlopUtilityText.Text;
            }
            else
            {
                MainForm.UEContentA = VersionTextA;
                MainForm.UECaretPositionA = CaretPositionA;

                MainForm.UEContentB = VersionTextB;
                MainForm.UECaretPositionB = CaretPositionB;

                MainForm.UEContentC = VersionTextC;
                MainForm.UECaretPositionC = CaretPositionC;

                MainForm.UEContentD = VersionTextD;
                MainForm.UECaretPositionD = CaretPositionD;

            }

            // Invoke autosave if changes are registered
            if (AutoSaveCounter >= 12)
            {
                AutoSaveCounter = 0;
                InvokeAutoSave = true;
            }
        }

        public void TransferTextAndCaretPositionsToEditor()
        {
            if (IsPlopEditor)
            {
                RegisterTextChanged = false;
                tbContentD.SetText(MainForm.PlopPersistedContent, false); // Rare memory error occurs here (trying to avoid by adding directly to tbContentD 19-02-2019)
                tbPlopUtilityText.Text = MainForm.PlopUtilityContent;
                RegisterTextChanged = true;
            }
            else
            {
                RegisterTextChanged = false;
                VersionTextA = MainForm.UEContentA;
                CaretPositionA = MainForm.UECaretPositionA;
                tbContentA.Text = VersionTextA;

                VersionTextB = MainForm.UEContentB;
                CaretPositionB = MainForm.UECaretPositionB;
                tbContentB.Text = VersionTextB;

                VersionTextC = MainForm.UEContentC;
                CaretPositionC = MainForm.UECaretPositionC;
                tbContentC.Text = VersionTextC;

                VersionTextD = MainForm.UEContentD;
                CaretPositionD = MainForm.UECaretPositionD;
                tbContentD.Text = VersionTextD;
                RegisterTextChanged = true;
            }
        }

        private void SaveCaretPositions()
        {
            switch (VersionSelection)
            {
                case 1:
                    CaretPositionA = tbContentA.SelectionStart;
                    break;
                case 2:
                    CaretPositionB = tbContentB.SelectionStart;
                    break;
                case 3:
                    CaretPositionC = tbContentC.SelectionStart;
                    break;
                case 4:
                    CaretPositionD = tbContentD.SelectionStart;
                    break;
            }

            SubmitTextVersionsToMainForm();
        }

        private void UpdateFontSize()
        {
            GetCurrentTextBox().ResetZoomFactor();
            GetCurrentTextBox().Font = Design.FieldFont(MainForm.FieldsFontSize);
            tbPlopUtilityText.Font = Design.FieldFont(MainForm.FieldsFontSize);
            tbSearchInContent.Font = Design.SearchFieldFont();
        }

        public void SetColorTheme()
        {
            UpdateFontSize();

            // Set colors
            marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.CurrentColorTheme()[8], AhoyColors.CurrentColorTheme()[7], AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1]);

            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlSettings.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbSplash.BackColor = AhoyColors.CurrentColorTheme()[1];
            lbSplash.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPlopDirection.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbBlankLines.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTrim.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbEscToClose.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbOptions.ForeColor = AhoyColors.CurrentColorTheme()[2];
            tbContentA.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContentA.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbContentB.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContentB.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbContentC.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContentC.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbContentD.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContentD.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbPlopUtilityText.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbPlopUtilityText.ForeColor = AhoyColors.CurrentColorTheme()[3];
            statusStrip1.BackColor = AhoyColors.CurrentColorTheme()[7];
            toolStripStatusLabel1.ForeColor = AhoyColors.CurrentColorTheme()[8];
            tbSearchInContent.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Set button appearance
            btnTopMost.FlatStyle = FlatStyle.Flat;
            btnCallUp.FlatStyle = FlatStyle.Flat;
            btnLink.FlatStyle = FlatStyle.Flat;
            btnPlop.FlatStyle = FlatStyle.Flat;
            btnCopy.FlatStyle = FlatStyle.Flat;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnRetrieve.FlatStyle = FlatStyle.Flat;
            btnTransfer.FlatStyle = FlatStyle.Flat;
            btnNewEntry.FlatStyle = FlatStyle.Flat;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnReplaceCurrentEntry.FlatStyle = FlatStyle.Flat;
            btnSpellCheck.FlatStyle = FlatStyle.Flat;
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnRedo.FlatStyle = FlatStyle.Flat;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnLoadPinnedEntry.FlatStyle = FlatStyle.Flat;
            btnVersionA.FlatStyle = FlatStyle.Flat;
            btnVersionB.FlatStyle = FlatStyle.Flat;
            btnVersionC.FlatStyle = FlatStyle.Flat;
            btnVersionTemp.FlatStyle = FlatStyle.Flat;
            btnCopyAtoB.FlatStyle = FlatStyle.Flat;
            btnCopyBtoC.FlatStyle = FlatStyle.Flat;
            btnCopyTempToA.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnTogglePlopDirection.FlatStyle = FlatStyle.Flat;
            btnCloseSettingsPanel.FlatStyle = FlatStyle.Flat;
            btnBlankLines.FlatStyle = FlatStyle.Flat;
            btnTrim.FlatStyle = FlatStyle.Flat;
            btnEscToClose.FlatStyle = FlatStyle.Flat;


            btnTopMost.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCallUp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnLink.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPlop.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCopy.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRetrieve.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTransfer.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnNewEntry.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClear.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnReplaceCurrentEntry.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSpellCheck.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUndo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRedo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMenu.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnLoadPinnedEntry.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnVersionA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnVersionB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnVersionC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnVersionTemp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCopyAtoB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCopyBtoC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCopyTempToA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSearch.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTogglePlopDirection.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCloseSettingsPanel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnBlankLines.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTrim.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnEscToClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnTopMost.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCallUp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnLink.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPlop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCopy.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRetrieve.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTransfer.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnNewEntry.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClear.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnReplaceCurrentEntry.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSpellCheck.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUndo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRedo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMenu.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnLoadPinnedEntry.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnVersionA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnVersionB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnVersionC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnVersionTemp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCopyAtoB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCopyBtoC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCopyTempToA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSearch.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTogglePlopDirection.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCloseSettingsPanel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnBlankLines.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTrim.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnEscToClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnTopMost.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCallUp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnLink.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnPlop.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCopy.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRetrieve.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTransfer.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnNewEntry.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClear.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnReplaceCurrentEntry.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSpellCheck.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnUndo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnRedo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnMenu.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnLoadPinnedEntry.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnVersionA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnVersionB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnVersionC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnVersionTemp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCopyAtoB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCopyBtoC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCopyTempToA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSearch.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTogglePlopDirection.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCloseSettingsPanel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnBlankLines.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTrim.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnEscToClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnTopMost.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTopMost.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCallUp.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCallUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnLink.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnLink.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPlop.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPlop.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCopy.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCopy.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRetrieve.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRetrieve.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTransfer.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTransfer.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnNewEntry.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnNewEntry.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClear.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClear.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnReplaceCurrentEntry.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnReplaceCurrentEntry.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSpellCheck.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSpellCheck.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnUndo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUndo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRedo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRedo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMenu.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMenu.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnLoadPinnedEntry.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnLoadPinnedEntry.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnVersionA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnVersionB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnVersionC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionTemp.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnVersionTemp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCopyAtoB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCopyAtoB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCopyBtoC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCopyBtoC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCopyTempToA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCopyTempToA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSearch.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSearch.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTogglePlopDirection.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTogglePlopDirection.ForeColor = AhoyColors.CurrentColorTheme()[6];
            btnCloseSettingsPanel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCloseSettingsPanel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnBlankLines.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnBlankLines.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTrim.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTrim.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnEscToClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnEscToClose.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Set topmost button
            btnTopMost.Text = "";
            btnTopMost.BackgroundImage = ButtonBitmaps.Unpinned();
            btnTopMost.BackgroundImageLayout = ImageLayout.Center;

            // Set panel bitmaps
            pnlSpawnSticky.BackgroundImage = ButtonBitmaps.Sticky(false);
            pnlShowAllStickies.BackgroundImage = ButtonBitmaps.ShiningEye(false);

            // Set calculator image
            picCalculator.BackgroundImage = ButtonBitmaps.Plus();
            picGear.BackgroundImage = ButtonBitmaps.Gear();

            // Set close settings button
            btnCloseSettingsPanel.Text = "";
            btnCloseSettingsPanel.BackgroundImage = ButtonBitmaps.Clear(false);
            btnCloseSettingsPanel.BackgroundImageLayout = ImageLayout.Center;

            // Set undo button
            btnUndo.Text = "";
            btnUndo.BackgroundImage = ButtonBitmaps.Left();
            btnUndo.BackgroundImageLayout = ImageLayout.Center;

            // Set redo button
            btnRedo.Text = "";
            btnRedo.BackgroundImage = ButtonBitmaps.Right();
            btnRedo.BackgroundImageLayout = ImageLayout.Center;

            // Link Blinker
            LinkBlinkerForeground = AhoyColors.CurrentColorTheme()[4];
            LinkBlinkerBackground = AhoyColors.CurrentColorTheme()[7];
            // Form Blinker
            FormBlinkerA = AhoyColors.CurrentColorTheme()[0];
            FormBlinkerB = AhoyColors.CurrentColorTheme()[6]; // Highlight I
            FormBlinkerC = AhoyColors.CurrentColorTheme()[4]; // Highlight II
            // Expansion colors 
            ExpansionColorA = AhoyColors.CurrentColorTheme()[1];
            ExpansionColorB = AhoyColors.CurrentColorTheme()[3];


            // Reselect memory bank to update button colors if not PLOP
            if (IsPlopEditor)
            {
                ResolveButtonHighlight();
                CheckPlopLink();

                if ((PlopLinkedPresetColor > 0 || AhoyColors.HasDualHexValue(PlopLinkedCustomColor)) && PlopIsLinked)
                {
                    if (PlopLinkedPresetColor > 0)
                    {
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor));
                    }
                    else
                    {
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false));
                    }
                }
                else if (PlopIsLinked)
                {
                    SetMarqueeColors(1);
                }
                else
                {
                    SetMarqueeColors(0);
                }

                // Resolve the "Pin" button which is set to false, further back in this method (as init)
                ResolveTopMostButton();
            }
            else
            {
                SelectTextVersion(VersionSelection);
            }
            // Resolve Texture
            ResolveTexture();

            // Update Micro Calculator
            microCalculator1.SetColorTheme();
        }


        private void ResolveTextBoxVisibility()
        {
            switch (VersionSelection)
            {
                case 1:
                    tbContentA.Visible = true;
                    tbContentA.BringToFront();
                    tbContentB.Visible = false;
                    tbContentC.Visible = false;
                    tbContentD.Visible = false;
                    break;
                case 2:
                    tbContentB.Visible = true;
                    tbContentB.BringToFront();
                    tbContentA.Visible = false;
                    tbContentC.Visible = false;
                    tbContentD.Visible = false;
                    break;
                case 3:
                    tbContentC.Visible = true;
                    tbContentC.BringToFront();
                    tbContentA.Visible = false;
                    tbContentB.Visible = false;
                    tbContentD.Visible = false;
                    break;
                case 4:
                    tbContentD.Visible = true;
                    tbContentD.BringToFront();
                    tbContentA.Visible = false;
                    tbContentB.Visible = false;
                    tbContentC.Visible = false;
                    break;
                default:
                    tbContentD.Visible = true;
                    tbContentD.BringToFront();
                    tbContentA.Visible = false;
                    tbContentB.Visible = false;
                    tbContentC.Visible = false;
                    break;
            }
        }

        private HistoryTextBox GetCurrentTextBox()
        {
            switch (VersionSelection)
            {
                case 1:
                    return tbContentA;
                case 2:
                    return tbContentB;
                case 3:
                    return tbContentC;
                case 4:
                    return tbContentD;
                default:
                    return tbContentD;
            }
        }


        private void ResolveTopMostButton()
        {
            if (IsTopMost)
            {
                btnTopMost.Text = "";
                btnTopMost.BackgroundImage = ButtonBitmaps.Pinned();
            }
            else
            {
                btnTopMost.Text = "";
                btnTopMost.BackgroundImage = ButtonBitmaps.Unpinned();
            }

            ResolveButtonHighlight();
        }

        private void ResolveButtonHighlight()
        {
            // Resolve other buttons
            if (!string.IsNullOrEmpty(GetCurrentTextBox().Text) && GetCurrentTextBox().Text.Length > 0)
            {
                btnTransfer.ForeColor = AhoyColors.CurrentColorTheme()[6];
                btnReplaceCurrentEntry.ForeColor = AhoyColors.CurrentColorTheme()[6];
                btnNewEntry.ForeColor = AhoyColors.CurrentColorTheme()[6];
                if (IsPlopEditor)
                {
                    if (PlopIsLinked)
                    {
                        btnPlop.ForeColor = AhoyColors.CurrentColorTheme()[7];
                        btnPlop.BackColor = AhoyColors.CurrentColorTheme()[6];
                        btnPlop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[4];
                        SetPlopMarqueeColors();
                    }
                    else
                    {
                        btnPlop.ForeColor = AhoyColors.CurrentColorTheme()[8];
                        btnPlop.BackColor = AhoyColors.CurrentColorTheme()[7];
                        btnPlop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
                        SetPlopMarqueeColors();
                    }
                }
                else
                {
                    btnClose.ForeColor = AhoyColors.CurrentColorTheme()[6];
                }
                if (!MainForm.PrismEffect)
                {
                    toolStripStatusLabel1.ForeColor = AhoyColors.CurrentColorTheme()[6];
                }
            }
            else
            {
                btnTransfer.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnReplaceCurrentEntry.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnNewEntry.ForeColor = AhoyColors.CurrentColorTheme()[8];
                if (IsPlopEditor)
                {
                    btnPlop.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnPlop.BackColor = AhoyColors.CurrentColorTheme()[7];
                    btnPlop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
                }
                {
                    btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
                }
                if (!MainForm.PrismEffect)
                {
                    toolStripStatusLabel1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            // Refresh activity counters
            Reaction();
            WPMCounter.ResetWPMcounter();
            SetStatusLabel(WPMCounter.GetProgressBarWithWordCount(""));
        }

        public void FocusContent()
        {
            GetCurrentTextBox().Focus();
        }

        public void SetText(string text, int caretPosition)
        {
            GetCurrentTextBox().SetText(text, false, true, caretPosition);
        }

        public void AppendText(string text)
        {
            GetCurrentTextBox().AppendText(text, 2, true);
        }

        private void ScrollToCaret(int index)
        {
            if (index > 0)
            {
                // Move cursor to last selected position
                GetCurrentTextBox().Focus();
                GetCurrentTextBox().Select(index, 0);
                GetCurrentTextBox().ScrollToCaret();
            }
        }

        // Scroll to end without activating the form/field
        private void ScrollToEnd()
        {
            if (!string.IsNullOrEmpty(GetCurrentTextBox().Text))
            {
                // Move cursor to last selected position
                GetCurrentTextBox().Select(GetCurrentTextBox().Text.Length, 0);
                GetCurrentTextBox().ScrollToCaret();
            }
        }


        private bool RegisterTextChanged;
        private void tbContent_TextChanged(object sender, EventArgs e)
        {
            // Prevent scrollbars from not showing up!
            if (RefreshScrollbars)
            {
                RefreshScrollbars = false;
                ResolveScrollbars();
            }

            Reaction();
            if (RegisterTextChanged)
            {
                if (VersionSelection == 1)
                {
                    VersionTextA = tbContentA.Text;
                    CaretPositionA = tbContentA.SelectionStart;
                    SetSelectionTitleText();
                    // Invoke saving pool (must be inside this if-statement)
                    MainForm.InvokeSavePool();
                }
                else if (VersionSelection == 2)
                {
                    VersionTextB = tbContentB.Text;
                    CaretPositionB = tbContentB.SelectionStart;
                    SetSelectionTitleText();
                    // Invoke saving pool (must be inside this if-statement)
                    MainForm.InvokeSavePool();
                }
                else if (VersionSelection == 3)
                {
                    VersionTextC = tbContentC.Text;
                    CaretPositionC = tbContentC.SelectionStart;
                    SetSelectionTitleText();
                    // Invoke saving pool (must be inside this if-statement)
                    MainForm.InvokeSavePool();
                }
                else if (VersionSelection == 4)
                {
                    VersionTextD = tbContentD.Text;
                    CaretPositionD = tbContentD.SelectionStart;
                    SetSelectionTitleText();
                    // InvokeSavePool omitted on purpose, because the Temp
                    // bank is not saved to disk
                }

                SubmitTextVersionsToMainForm();
            }
            ResolveButtonHighlight();
            PerformWordCountMainWindow(false);
        }

        private void PerformWordCountMainWindow(bool resetCounter)
        {
            if (resetCounter)
            {
                WPMCounter.ResetWPMcounter();
                SetStatusLabel(WPMCounter.GetWordCount(GetCurrentTextBox().Text));
            }
            else
            {
                SetStatusLabel(WPMCounter.GetProgressBarWithWordCount(GetCurrentTextBox().Text));
            }
        }

        private void PerformWordCountUtilityWindow(bool resetCounter)
        {
            if (resetCounter)
            {
                WPMCounter.ResetWPMcounter();
                SetStatusLabel(WPMCounter.GetWordCount(tbPlopUtilityText.Text));
            }
            else
            {
                SetStatusLabel(WPMCounter.GetProgressBarWithWordCount(tbPlopUtilityText.Text));
            }
        }

        private void tbPlopUtilityText_TextChanged(object sender, EventArgs e)
        {
            if (RegisterTextChanged)
            {
                Reaction();
                SubmitTextVersionsToMainForm();
                // Invoke saving pool (must be inside this if-statement)
                MainForm.InvokeSavePool();
                PerformWordCountUtilityWindow(false);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            SubmitDataAndCloseForm(true);
        }


        private void btnCopy_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().SelectAll();
            GetCurrentTextBox().CopyUnformatted();
            MainForm.PersistClipboard();
            GetCurrentTextBox().Focus();
        }

        public void SubmitDataAndCloseForm(bool forceSubmitAndClose)
        {
            if (forceSubmitAndClose)
            {
                SubmitDataToMainForm();
                this.Close();
            }
            else if (IsPlopEditor && MainForm.UEESCtoClose)
            {
                Log.UEEditor("PLOP: Close");
                SubmitDataToMainForm();
                this.Close();
            }
            else if (IsPlopEditor && !MainForm.UEESCtoClose)
            {
                SubmitDataToMainForm();
            }
            else
            {
                SubmitDataToMainForm();
                this.Close();
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MainForm.CurrentContentForUtilityEditor))
            {
                GetCurrentTextBox().SetText(MainForm.CurrentContentForUtilityEditor, false, true, MainForm.CurrentCaretPositionForUtilityEditor);
                if (MainForm.CurrentCaretPositionForUtilityEditor > 0)
                {
                    ScrollToCaret(MainForm.CurrentCaretPositionForUtilityEditor);
                }
                else
                {
                    GetCurrentTextBox().Focus();
                    ScrollToEnd();
                }
            }

        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Clear();
            GetCurrentTextBox().Focus();
        }

        private void DetachedEditor_LocationChanged(object sender, EventArgs e)
        {
            Reaction();
            SubmitDataToMainForm();
        }

        private void DetachedEditor_Resize(object sender, EventArgs e)
        {
            Reaction();
            SubmitDataToMainForm();
        }

        private void DetachedEditor_ResizeBegin(object sender, EventArgs e)
        {
            // Collapse utility text box before resizing to prevent
            // false location placement
            if (PlopUtilityEditorExpanded)
            {
                TogglePlopUtilityExpansion();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            Reaction();

            if (MainForm.IsEntrySelected())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Append text to current Entry?", MessageText.AppendCaption(), false, false);
                msg.CenterParent();
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    ResetSpecialCommandVariables();
                    TransferToCurrentEntry = true;
                }

                MainForm.RefreshForm();
                FocusContent();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.AppendCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
                MainForm.RefreshForm();
            }
        }

        private void btnReplaceCurrentEntry_Click(object sender, EventArgs e)
        {
            Reaction();
            if (MainForm.IsEntrySelected())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Overwrite text in current Entry?", MessageText.OverwriteCaption(), false, false);
                msg.CenterParent();
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    ResetSpecialCommandVariables();
                    ReplaceWithCurrentEntry = true;
                }

                MainForm.RefreshForm();
                FocusContent();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.OverwriteCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
                MainForm.RefreshForm();
            }
        }

        private void btnNewEntry_Click(object sender, EventArgs e)
        {
            Reaction();
            ResetSpecialCommandVariables();
            TransferToNewEntry = true;
            FocusContent();
        }

        private void btnSpellCheck_Click(object sender, EventArgs e)
        {
            Reaction();
            OpenSpellChecker(GetCurrentTextBox(), false);
        }

        private static int UESpellCheckerCaretPosition;
        private static WPFSpellChecker spellCheck;
        private static bool UEWasTopMost;
        private void OpenSpellChecker(HistoryTextBox textboxToCheck, bool emptySpellChecker)
        {
            try
            {
                UEWasTopMost = this.TopMost;
                Log.SpellCheck();
                MainForm.SpellCheckedText = "";
                MainForm.SpellCheckSubmitted = false;
                UESpellCheckerCaretPosition = 0;
                Reaction();

                if (!emptySpellChecker)
                {
                    UESpellCheckerCaretPosition = textboxToCheck.SelectionStart;
                }

                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = false;
                }

                // Open spellchecker (Opacity is not transferred to avoid rendering problems with multiple opaque forms)
                spellCheck = new WPFSpellChecker(textboxToCheck.Text, textboxToCheck.WordWrap, emptySpellChecker, MainForm.SpellCheckerLanguage, UESpellCheckerCaretPosition, 1.0d);
                spellCheck.ShowDialog();

                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = true;
                }

                // If spell check was accepted replace the content with the
                // corrected text
                if (MainForm.SpellCheckSubmitted && !string.IsNullOrEmpty(MainForm.SpellCheckedText))
                {
                    textboxToCheck.Text = MainForm.SpellCheckedText;
                    textboxToCheck.Focus();
                }
                else
                {
                    textboxToCheck.Focus();
                }
            }
            finally
            {
                // Clear variables
                MainForm.SpellCheckedText = "";
                spellCheck.Dispose();
            }
        }

        private void tbContent_KeyDown(object sender, KeyEventArgs e)
        {
            // Refresh activity counters
            Reaction();

            // This multi shortcut statement must be first
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C)) // Multiple shortcut keypress (Copy formatted)
            {
                GetCurrentTextBox().Copy();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.X)) // Multiple shortcut keypress (Cut formatted)
            {
                GetCurrentTextBox().Cut();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.I)) // Multiple shortcut keypress
            {
                SpawnSticky();
                e.SuppressKeyPress = true; // Prevent inserting Tab by CTRL+SHIFT+I
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.H)) // Multiple shortcut keypress
            {
                HideSticky();
                e.SuppressKeyPress = true; // Prevent inserting Tab by CTRL+SHIFT+H
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.A)) // Multiple shortcut keypress (Open command prompt)
            {
                MainForm.OpenCommandPromptPerRemote();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                InsertDate();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // Add current (TEMP, A, B, C tb content at the top of current entry
                // and clear content
                if (IsPlopEditor)
                {
                    Plop();
                }
                else
                {
                    SubmitDataAndCloseForm(false);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F8)
            {
                CallUp();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                DisconnectLink();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F7)
            {
                ToggleTopMost();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                tbSearchInContent.Focus();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Tab)
            {
                if (!IsPlopEditor)
                {
                    CycleTextVersionBackwards();
                }
                else
                {
                    TogglePlopUtilityExpansion();
                }
                e.Handled = true;
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.G)
            {
                btnSpellCheck_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                if (!IsPlopEditor)
                {
                    btnLoadPinnedEntry_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                ShowContextMenu();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
            {
                GetCurrentTextBox().CopyUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
            {
                GetCurrentTextBox().CutUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V) // Paste unformatted
            {
                GetCurrentTextBox().PasteUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.Left)
            {
                if (!IsPlopEditor)
                {
                    PreviousVersion();
                }
                else
                {
                    TogglePlopUtilityExpansion();
                }
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.Right)
            {
                if (!IsPlopEditor)
                {
                    NextVersion();
                }
                else
                {
                    TogglePlopUtilityExpansion();
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                GetCurrentTextBox().DeleteCurrentLine();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (!IsPlopEditor)
                {
                    btnNewEntry_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                if (!IsPlopEditor)
                {
                    btnReplaceCurrentEntry_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                if (!IsPlopEditor)
                {
                    btnNewEntry_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.R)
            {
                if (!IsPlopEditor)
                {
                    btnRetrieve_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.C)
            {
                btnCopy_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Delete)
            {
                btnClear_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                if (!IsPlopEditor)
                {
                    btnTransfer_Click(this, null);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D1)
            {
                if (!IsPlopEditor)
                {
                    SelectTextVersion(4);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                if (!IsPlopEditor)
                {
                    SelectTextVersion(1);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D3)
            {
                if (!IsPlopEditor)
                {
                    SelectTextVersion(2);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D4)
            {
                if (!IsPlopEditor)
                {
                    SelectTextVersion(3);
                }
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                InsertParagraphDivider(0, GetCurrentTextBox());
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                InsertParagraphDivider(1, GetCurrentTextBox());
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemPeriod)
            {
                InsertParagraphDivider(2, GetCurrentTextBox());
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                InsertParagraphDivider(3, GetCurrentTextBox());
                e.Handled = true;
            }
            else
            {
                // Get WPM speed counter
                WPMCounter.SubmitInputCount(e);
            }

        }

        // Insert Paragraph divider
        private void InsertParagraphDivider(int selection, HistoryTextBox tb)
        {
            switch (selection)
            {
                case 0:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
                case 1:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerBString);
                    break;
                case 2:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerCString);
                    break;
                case 3:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerDString);
                    break;
                default:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Undo();
            GetCurrentTextBox().Focus();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Redo();
            GetCurrentTextBox().Focus();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Cut();
            MainForm.PersistClipboard();
            GetCurrentTextBox().Focus();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().CopyUnformatted();
            MainForm.PersistClipboard();
            GetCurrentTextBox().Focus();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().PasteUnformatted();
            GetCurrentTextBox().Focus();
        }

        private void spellCheckToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Reaction();
            btnSpellCheck_Click(this, null);
        }

        private void clearFormattingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reaction();
            ClearUtilityContentFormatting();
        }

        private string TempUnformattedMemoText { get; set; }
        private int MemoCaretPosition { get; set; }
        private void ClearUtilityContentFormatting()
        {
            GetCurrentTextBox().SuspendLayout();
            // Save caret position
            MemoCaretPosition = GetCurrentTextBox().SelectionStart;
            // Save composition
            TempUnformattedMemoText = GetCurrentTextBox().Text;
            // Reset textbox
            GetCurrentTextBox().Clear();
            // Add unformatted content back
            GetCurrentTextBox().SetText(TempUnformattedMemoText, false);
            GetCurrentTextBox().ResumeLayout();

            if (MemoCaretPosition > 0)
            {
                // Move cursor to last selected position
                GetCurrentTextBox().Focus();
                GetCurrentTextBox().Select(MemoCaretPosition, 0);
                GetCurrentTextBox().ScrollToCaret();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().SelectAll();
            GetCurrentTextBox().Focus();
        }

        private void lineDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().DeleteCurrentLine();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Undo();
            GetCurrentTextBox().Focus();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().Redo();
            GetCurrentTextBox().Focus();
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            GetCurrentTextBox().SelectAll();
            GetCurrentTextBox().CopyUnformatted();
            MainForm.PersistClipboard();
            GetCurrentTextBox().Focus();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            ShowContextMenu();
        }

        private void ShowContextMenu()
        {
            Reaction();
            contextMenuUtilityEditor.Show(btnMenu, btnMenu.Location);
        }

        private void resetWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reaction();
            RestoreUtilityEditorView();
            GetCurrentTextBox().Focus();
        }

        private void tbContent_MouseClick(object sender, MouseEventArgs e)
        {
            Reaction();
        }

        public void SetOpacity(double Opacity)
        {
            if (Opacity >= 0.50d)
            {
                this.Opacity = Opacity;
                FormOpacity = this.Opacity;
            }
            else
            {
                SetOpacity(1.00d);
            }
            ResolveOpacityMenuChecked();
        }


        private void ResolveOpacityMenuChecked()
        {
            opacity100ToolStripMenuItem.Checked = false;
            opacity95toolStripMenuItem.Checked = false;
            opacity90toolStripMenuItem.Checked = false;
            opacity85toolStripMenuItem.Checked = false;
            opacity75toolStripMenuItem.Checked = false;
            opacity65ToolStripMenuItem.Checked = false;
            opacity50ToolStripMenuItem.Checked = false;

            if (FormOpacity == 1.00d)
            {
                opacity100ToolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.95d)
            {
                opacity95toolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.90d)
            {
                opacity90toolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.85d)
            {
                opacity85toolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.75d)
            {
                opacity75toolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.65d)
            {
                opacity65ToolStripMenuItem.Checked = true;
            }
            else if (FormOpacity == 0.50d)
            {
                opacity50ToolStripMenuItem.Checked = true;
            }
            else
            {
                opacity100ToolStripMenuItem.Checked = true;
            }
        }


        private void opacity100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetOpacity(1.00d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity95toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SetOpacity(0.95d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity90toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SetOpacity(0.90d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity85toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SetOpacity(0.85d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity75toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            SetOpacity(0.75d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity65ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetOpacity(0.65d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void opacity50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetOpacity(0.50d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void matchMainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWindowPositionAndSize(ParentLocationX, ParentLocationY, ParentWidth, ParentHeight, FormWindowState.Normal, 1.00d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void matchTransparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWindowPositionAndSize(ParentLocationX, ParentLocationY, ParentWidth, ParentHeight, FormWindowState.Normal, 0.85d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
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
                Log.LogWindowStateChangeErrorUE();
            }
        }

        private void centerSmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnapOutOfMaximization();
            SetWindowPositionAndSize(ParentLocationX + (ParentWidth / 4), ParentLocationY + (ParentHeight / 4), UEDefaultWidth, UEDefaultHeight, FormWindowState.Normal, 1.00d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void centerTransparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnapOutOfMaximization();
            SetWindowPositionAndSize(ParentLocationX + (ParentWidth / 4), ParentLocationY + (ParentHeight / 4), UEDefaultWidth, UEDefaultHeight, FormWindowState.Normal, 0.85d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void leftTallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnapOutOfMaximization();
            SetWindowPositionAndSize(ParentLocationX, ParentLocationY, UEDefaultWidth, ParentHeight, FormWindowState.Normal, 1.00d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void rightTallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnapOutOfMaximization();
            SetWindowPositionAndSize(ParentLocationX + (ParentWidth - UEDefaultWidth), ParentLocationY, UEDefaultWidth, ParentHeight, FormWindowState.Normal, 1.00d);
            SubmitDataToMainForm();
            Reaction();
            GetCurrentTextBox().Focus();
        }

        private void btnLoadPinnedEntry_Click(object sender, EventArgs e)
        {
            Reaction();

            if (MainForm.PinnedEntriesExist())
            {
                ResetSpecialCommandVariables();
                LoadPinnedEntry = true;
                FocusContent();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SelectEntryCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
                MainForm.RefreshForm();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsPlopEditor)
            {
                UEWasTopMost = this.TopMost;
                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = false;
                }
                AhoyInfoMessageBox msg = new AhoyInfoMessageBox(IO.PlopEditorTextReader(), MessageText.PlopEditorCaption(), false, true, this.Opacity);
                msg.StartPosition = FormStartPosition.CenterParent;
                msg.TopMost = true;
                msg.ShowDialog();
                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = true;
                }
                FocusContent();
                MainForm.RefreshForm();
            }
            else
            {
                AhoyInfoMessageBox msg = new AhoyInfoMessageBox(IO.UtilityEditorTextReader(), MessageText.UtilityEditorCaption(), false, true, this.Opacity);
                msg.StartPosition = FormStartPosition.CenterParent;
                msg.ShowDialog();
                FocusContent();
                MainForm.RefreshForm();
            }
        }

        private void btnVersionTemp_Click(object sender, EventArgs e)
        {
            SelectTextVersion(4);
            SubmitDataToMainForm();
            Reaction();
        }

        private void btnVersionA_Click(object sender, EventArgs e)
        {
            SelectTextVersion(1);
            SubmitDataToMainForm();
            Reaction();
        }

        private void btnVersionB_Click(object sender, EventArgs e)
        {
            SelectTextVersion(2);
            SubmitDataToMainForm();
            Reaction();
        }

        private void btnVersionC_Click(object sender, EventArgs e)
        {
            SelectTextVersion(3);
            SubmitDataToMainForm();
            Reaction();
        }

        private void ResolveSelectedVersion()
        {
            ForceHideCalculator(); // To keep calculater from being hidden behind the boxes
            ResolveTextBoxVisibility();
            ResolveBankButtonColors();

            RegisterTextChanged = false;
            switch (VersionSelection)
            {
                case 1:
                    btnVersionA.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    tbContentA.SetText(VersionTextA, false, true, CaretPositionA);
                    SetSelectionTitleText();
                    break;
                case 2:
                    btnVersionB.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    tbContentB.SetText(VersionTextB, false, true, CaretPositionB);
                    SetSelectionTitleText();
                    break;
                case 3:
                    btnVersionC.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    tbContentC.SetText(VersionTextC, false, true, CaretPositionC);
                    SetSelectionTitleText();
                    break;
                case 4:
                    btnVersionTemp.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    tbContentD.SetText(VersionTextD, false, true, CaretPositionD);
                    SetSelectionTitleText();
                    break;
                default:
                    btnVersionTemp.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    tbContentD.SetText(VersionTextD, false, true, CaretPositionD);
                    SetSelectionTitleText();
                    break;
            }

            RegisterTextChanged = true;
            PerformWordCountMainWindow(false);
            ResetContentSearchVariables();
            ResolveScrollbars();
        }

        private void SetSelectionTitleText()
        {
            if (!IsPlopEditor)
            {
                if (VersionSelection >= 4)
                {
                    this.Text = UETitle + TextTransform.GetBlankSpace(2) + "(Temp: Not persisted on application Exit)";
                }
                else if (VersionSelection == 1)
                {
                    this.Text = UETitle + TextTransform.GetBlankSpace(2) + "(A: Persisted)";
                }
                else if (VersionSelection == 2)
                {
                    this.Text = UETitle + TextTransform.GetBlankSpace(2) + "(B: Persisted)";
                }
                else if (VersionSelection == 3)
                {
                    this.Text = UETitle + TextTransform.GetBlankSpace(2) + "(C: Persisted)";
                }
                else
                {
                    this.Text = UETitle;
                }
            }
        }

        private void ResolveBankButtonColors()
        {
            btnVersionA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnVersionTemp.ForeColor = AhoyColors.CurrentColorTheme()[8];

            if (VersionTextA.Length > 0)
            {
                btnVersionA.ForeColor = AhoyColors.CurrentColorTheme()[4];
            }

            if (VersionTextB.Length > 0)
            {
                btnVersionB.ForeColor = AhoyColors.CurrentColorTheme()[4];
            }

            if (VersionTextC.Length > 0)
            {
                btnVersionC.ForeColor = AhoyColors.CurrentColorTheme()[4];
            }

            if (VersionTextD.Length > 0)
            {
                btnVersionTemp.ForeColor = AhoyColors.CurrentColorTheme()[4];
            }
        }


        private void btnCopyTempToA_Click(object sender, EventArgs e)
        {
            // Only start process if start-field has text
            if (VersionTextD.Length > 0)
            {
                // Ask if destination is occupied by text
                if (VersionTextA.Length > 0)
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Replace content in Memory Bank A?", MessageText.CopyCaption(), false, false);
                    msg.CenterParent();
                    msg.ShowDialog();

                    if (!msg.Accepted)
                    {
                        return;
                    }
                }

                VersionTextA = VersionTextD;
                CaretPositionA = CaretPositionD;
                SelectTextVersion(VersionSelection); // Update destination button color to show that it contains content and reselect current button
                MainForm.RefreshForm();
            }
        }

        private void btnCopyAtoB_Click(object sender, EventArgs e)
        {
            // Only start process if start-field has text
            if (VersionTextA.Length > 0)
            {
                // Ask if destination is occupied by text
                if (VersionTextB.Length > 0)
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Replace content in Memory Bank B?", MessageText.CopyCaption(), false, false);
                    msg.CenterParent();
                    msg.ShowDialog();

                    if (!msg.Accepted)
                    {
                        return;
                    }
                }

                VersionTextB = VersionTextA;
                CaretPositionB = CaretPositionA;
                SelectTextVersion(VersionSelection); // Update destination button color to show that it contains content and reselect current button
                MainForm.RefreshForm();
            }
        }

        private void btnCopyBtoC_Click(object sender, EventArgs e)
        {
            // Only start process if start-field has text
            if (VersionTextB.Length > 0)
            {
                // Ask if destination is occupied by text
                if (VersionTextC.Length > 0)
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Replace content in Memory Bank C?", MessageText.CopyCaption(), false, false);
                    msg.CenterParent();
                    msg.ShowDialog();

                    if (!msg.Accepted)
                    {
                        return;
                    }
                }

                VersionTextC = VersionTextB;
                CaretPositionC = CaretPositionB;
                SelectTextVersion(VersionSelection); // Update destination button color to show that it contains content and reselect current button
                MainForm.RefreshForm();
            }
        }

        private void clearAllMemoBanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Clear All Memo Banks?", MessageText.ClearCaption(), false, false);
            msg.CenterParent();
            msg.ShowDialog();

            if (msg.Accepted)
            {
                Reaction();
                SelectTextVersion(1);
                tbContentA.Clear();
                SelectTextVersion(2);
                tbContentB.Clear();
                SelectTextVersion(3);
                tbContentC.Clear();
                SelectTextVersion(4); // Ending up with selecting the Temp bank
                tbContentD.Clear();
                MainForm.RefreshForm();
            }

        }

        private void tbContentD_VScroll(object sender, EventArgs e)
        {
            MainForm.RefreshForm();
        }

        private void removeNewLinesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentTextBox().RemoveNewLines(false, true);
        }

        private void clearNewLinesPreserveParagraphsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentTextBox().RemoveNewLines(true, true);
        }

        private void tABToSpaceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentTextBox().TabToSpace();
        }

        private void trimSpaceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentTextBox().TrimSpace();
        }

        #region ENTRY TEXT SEARCH
        int start;
        int indexOfSearchText;
        string contentSearchString = "";
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!timerRefocusContentSearch.Enabled)
            {
                FindContent();
            }
            GetCurrentTextBox().Focus();
        }

        private void tbSearchInContent_Click(object sender, EventArgs e)
        {
            tbSearchInContent.SelectAll();
        }


        private void tbSearchInContent_KeyDown(object sender, KeyEventArgs e)
        {
            // Refresh activity counters
            Reaction();

            if (e.KeyCode == Keys.Enter)
            {
                if (!timerRefocusContentSearch.Enabled)
                {
                    FindContent();
                }
                // Prevent Ding
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                GetCurrentTextBox().Focus();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X))
            {
                generatePasswordsToolStripMenuItem_Click(this, null);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                return; // Prevent from running to the bottom of this method
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                return; // Prevent from running to the bottom of this method
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                return; // Prevent from running to the bottom of this method
            }
            else if (e.KeyCode == Keys.F8)
            {
                CallUp();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F7)
            {
                ToggleTopMost();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                DisconnectLink();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
            {
                // Send the rest of the keypresses to the tbContent_KeyDown method
                tbContent_KeyDown(sender, e);
            }
        }

        int searchCount;
        string tempPattern;
        string tempSource;
        int tempCurrentIndex;
        private void ResolveSearchCount()
        {
            // Fetch strings (to lower to make it case insensitive and get all counts)
            tempPattern = tbSearchInContent.Text.ToLower();
            tempSource = GetCurrentTextBox().Text.ToLower();

            // Loop through all instances of the tempPattern
            searchCount = 0;
            int i = 0;
            while ((i = tempSource.IndexOf(tempPattern, i)) != -1)
            {
                i += tempPattern.Length;
                searchCount++;
            }

            UpdateSearchVisuals();
        }

        private void UpdateSearchVisuals()
        {
            if (searchCount > 0)
            {
                if (AhoyColors.ColorTheme == 1 || AhoyColors.ColorTheme == 2)
                {
                    lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[4];
                }
                else
                {
                    lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[6];
                }
            }

            // Display the "Next" text if there is more than one result
            if (searchCount > 1)
            {
                btnSearch.Text = "Next";
                btnSearch.ForeColor = AhoyColors.CurrentColorTheme()[6];
                this.toolTip1.SetToolTip(btnSearch, "Find Next Match");
            }

            if (searchCount <= 1)
            {
                lbContentSearch.Text = "Found: " + searchCount.ToString();
            }
            else
            {
                lbContentSearch.Text = tempCurrentIndex.ToString() + " of " + searchCount.ToString();
            }
        }

        private void ResetContentSearchVariables()
        {
            // On new searches the "start" variable must be reset
            RestartSearchVariables();
            tbSearchInContent.Text = "";
        }

        private void RestartSearchVariables()
        {
            // On new searches the "start" variable must be reset
            start = 0;
            indexOfSearchText = 0;
            tempCurrentIndex = 0;
            contentSearchString = "";
            lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbContentSearch.Text = "Search";
            btnSearch.Text = "Search";
            btnSearch.ForeColor = AhoyColors.CurrentColorTheme()[8];
            this.toolTip1.SetToolTip(btnSearch, "Find Content (CTRL+ALT+F)");
        }

        private void timerRefocusContentSearch_Tick(object sender, EventArgs e)
        {
            tbSearchInContent.Text = contentSearchString;
            tbSearchInContent.SelectAll();
            // Must be disabled at the end of the method
            timerRefocusContentSearch.Enabled = false;
        }

        private void tbSearchInContent_TextChanged(object sender, EventArgs e)
        {
            Reaction();
            RestartSearchVariables();
        }


        private HistoryTextBox tempTextBox;
        private void FindContent()
        {
            if (tbSearchInContent.Text.Length > 0)
            {
                tempTextBox = GetCurrentTextBox();
                contentSearchString = tbSearchInContent.Text;

                if (!string.IsNullOrEmpty(contentSearchString))
                    Log.LogUEContentSearch(contentSearchString);

                ResolveSearchCount();

                int startindex = 0;
                if (tbSearchInContent.Text.Length > 0)
                {
                    startindex = FindMyText(tbSearchInContent.Text.Trim(), start, tempTextBox.Text.Length + 1);
                }

                // If string was found in the RichTextBox, highlight it
                if (startindex >= 0)
                {
                    // Set the highlight color as red (Remember to set the rich textbox to show highlights in its properties)
                    //GetCurrentTextBox().SelectionColor = Color.Red;
                    // Find the end index. End Index = number of characters in textbox
                    int endindex = tbSearchInContent.Text.Length;
                    // Highlight the search string
                    tempTextBox.Select(startindex, endindex);

                    // mark the start position after the position of
                    // last search string
                    start = startindex + endindex;
                    // Count up current index
                    tempCurrentIndex += 1;
                    UpdateSearchVisuals();
                }

                if (indexOfSearchText == -1)
                {
                    UEWasTopMost = this.TopMost;
                    // Handle topmost appearence
                    if (UEWasTopMost)
                    {
                        this.TopMost = false;
                    }
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EndOfSearchMessage(), MessageText.SearchCaption(), false);
                    messageBox.CenterParent();
                    messageBox.TopMost = true;
                    messageBox.ShowDialog();
                    // Handle topmost appearence
                    if (UEWasTopMost)
                    {
                        this.TopMost = true;
                    }
                    start = 0;
                    indexOfSearchText = 0;
                    tempCurrentIndex = 0;
                    MainForm.RefreshForm();
                }

                // Refocus search field
                timerRefocusContentSearch.Enabled = true;
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = GetCurrentTextBox().Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }
        #endregion

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



        private void btnPlop_Click(object sender, EventArgs e)
        {
            Plop();
        }

        // Perform Plop
        private string tempContent;
        private void Plop()
        {
            // Hide settings and plop
            CloseSettingsPanel();

            // Only plop if linked
            if (PlopIsLinked && !PlopWriteProtected)
            {
                // Submit content if not Splash text
                if (!SplashDisplayed)
                {
                    tempContent = GetCurrentTextBox().Text;
                }

                // Close editor if no text is available
                if (SplashDisplayed || tempContent.Length == 0)
                {
                    SubmitDataAndCloseForm(false);
                }
                else
                {
                    // Else submit text to main form
                    SubmitPlopToMainForm();
                    FocusContent();
                    // Blink form
                    BlinkForm();
                }
            }
            else
            {
                if (PlopWriteProtected)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EntryWriteProtectedMessage(), MessageText.PlopEditorCaption(), false);
                    msg.CenterParent();
                    msg.ShowDialog();
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No Link established", MessageText.PlopEditorCaption(), false);
                    msg.CenterParent();
                    msg.ShowDialog();
                }
            }
        }

        // React on keydowns in the Plop Utility textbox
        private void tbPlopUtilityText_KeyDown(object sender, KeyEventArgs e)
        {
            // Refresh activity counters
            Reaction();

            if (e.KeyCode == Keys.Escape)
            {
                FocusContent();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
            {
                tbPlopUtilityText.CopyUnformatted();
                MainForm.PersistClipboard();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
            {
                tbPlopUtilityText.CutUnformatted();
                MainForm.PersistClipboard();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V) // Paste unformatted
            {
                tbPlopUtilityText.PasteUnformatted();
                MainForm.PersistClipboard();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            if (e.Alt && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                TogglePlopUtilityExpansion();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                CallUp();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                DisconnectLink();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F8)
            {
                ToggleTopMost();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
            {
                // Get WPM speed counter
                WPMCounter.SubmitInputCount(e);
            }
        }

        private void UpdateUI(int interval)
        {
            if (!timerUIUpdater.Enabled)
            {
                timerUIUpdater.Interval = interval;
                timerUIUpdater.Enabled = true;
            }
        }

        private void timerUIUpdater_Tick(object sender, EventArgs e)
        {
            timerUIUpdater.Enabled = false;
            // Do updates here
            SetPlopMarqueeText();
        }


        private bool MarqueeSetForLinked;
        private bool ForceMarqueeTextPLOP;
        private void SetPlopMarqueeText()
        {
            if (PlopIsLinked && !MarqueeSetForLinked) // Only call once if linked and vice versa
            {
                MarqueeSetForLinked = true;
                SetPlopLinkedMarqueeText();
            }
            else if (!PlopIsLinked && MarqueeSetForLinked)
            {
                SetPlopMarqueeColors();
                MarqueeSetForLinked = false;
                SetMarquee("NO LINK" + TextTransform.GetBlankSpace(5) + "NO LINK" + TextTransform.GetBlankSpace(5) + "NO LINK");
            }

            if (ForceMarqueeTextPLOP)
            {
                ForceMarqueeTextPLOP = false;
                SetPlopLinkedMarqueeText();
            }
        }

        private void SetPlopLinkedMarqueeText()
        {
            SetPlopMarqueeColors();
            marqueePlop.StopSequence();
            SetMarquee("PLOP is linked to " + PlopLinkedTitle + "...");
            marqueePlop.ResumeSequence();
        }

        private void SetPlopMarqueeTexPLOPPED()
        {
            SetMarqueeColors(2);
            marqueePlop.StopSequence();
            SetMarquee("PLOP!");
            marqueePlop.ResumeSequence();
        }

        private bool tempMarqueeColorsSetForLink;
        private void SetPlopMarqueeColors()
        {
            if (PlopLinkedPresetColor > 0 || AhoyColors.HasDualHexValue(PlopLinkedCustomColor))
            {
                SetMarqueeColors(3);
            }
            else if (PlopIsLinked && !tempMarqueeColorsSetForLink)
            {
                tempMarqueeColorsSetForLink = true;
                SetMarqueeColors(1);
            }
            else if (!PlopIsLinked && tempMarqueeColorsSetForLink)
            {
                tempMarqueeColorsSetForLink = false;
                SetMarqueeColors(0);
            }

        }

        // 0 = not linked, 1 = linked, 2 = *** PLOP *** color
        private void SetMarqueeColors(int colorCombination)
        {
            switch (colorCombination)
            {
                case 0:
                    marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.CurrentColorTheme()[8], AhoyColors.CurrentColorTheme()[7], AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1]);
                    break;
                case 1:
                    marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.CurrentColorTheme()[4], AhoyColors.CurrentColorTheme()[7], AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1]);
                    break;
                case 2:
                    marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.CurrentColorTheme()[6], AhoyColors.CurrentColorTheme()[7], AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1]);
                    tempMarqueeColorsSetForLink = false;
                    break;
                case 3:
                    if (PlopLinkedPresetColor > 0)
                    {
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor), Design.ReturnStatusForegroundColor(PlopLinkedPresetColor), Design.ReturnStatusBackgroundColor(PlopLinkedPresetColor));
                    }
                    else
                    {
                        marqueePlop.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(PlopLinkedCustomColor.Split(';')[1], false));
                    }
                    break;
            }
        }

        private const string LinkedText = "Linked";
        private const string UnlinkedText = "NO LINK";
        private void CheckPlopLink()
        {
            // If linked
            if (PlopIsLinked && PlopLinkedID.Length > 0 && btnLink.Text != LinkedText)
            {
                btnLink.ForeColor = LinkBlinkerForeground;
                btnLink.BackColor = LinkBlinkerBackground;
                btnLink.Text = LinkedText;
                btnCallUp.Text = "Call-Up";
                btnCallUp.ForeColor = LinkBlinkerForeground;
                UpdateUI(2000);
            }
            else if (!PlopIsLinked && btnLink.Text != UnlinkedText)
            {
                PlopIsLinked = false;
                btnLink.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnLink.BackColor = AhoyColors.CurrentColorTheme()[7];
                btnLink.Text = UnlinkedText;
                btnCallUp.Text = "N/A";
                btnCallUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
                UpdateUI(2000);
            }
        }

        private void ResolveLinkButtonBlinker()
        {
            if (PlopIsLinked && !timerBlinker.Enabled)
            {
                timerBlinker.Enabled = true;
            }
            else if (!PlopIsLinked && timerBlinker.Enabled)
            {
                timerBlinker.Enabled = false;
            }
        }

        public void SetPlopLink(bool status)
        {
            PlopIsLinked = status;
            // Remove PLOP Link
            if (!PlopIsLinked)
            {
                // Clear all PLOP content
                PlopLinkedID = "";
                PlopLinkedTitle = "";
                PlopLinkedOriginalContent = "";
                PlopLinkedPresetColor = 0;
                PlopLinkedCustomColor = "";
                MainForm.PlopContent = "";
                MainForm.PlopLinkedID = "";
                MainForm.ForceSavePool();
                ResolveLinkButtonBlinker();
            }
            CheckPlopLink();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            Reaction();
            DisconnectLink();
        }

        private void DisconnectLink()
        {
            // If linked show title to current link and as with Yes/No to remove link
            // Button displays [Linked] or [No link]
            // Marquee displays "No link... No link... No link..." if not linked.
            if (PlopIsLinked && PlopLinkedID.Length > 0)
            {
                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = false;
                }

                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.RemovePlopLinkQuestion(PlopLinkedTitle), MessageText.PlopEditorCaption(), false, true);
                msg.CenterParent();
                msg.TopMost = true;
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    SetPlopLink(false);
                    SetColorTheme();
                }
                // Handle topmost appearence
                if (UEWasTopMost)
                {
                    this.TopMost = true;
                }


                ResolveLinkButtonBlinker();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Select an Entry to use with the PLOP Editor. Then open the PLOP Editor to link the two together", MessageText.PlopEditorCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }

            MainForm.RefreshForm();
        }

        private bool tempBlinkStatus;
        private void timerBlinker_Tick(object sender, EventArgs e)
        {
            if (IsPlopEditor && PlopIsLinked)
            {
                tempBlinkStatus = !tempBlinkStatus;
                btnBlankLines.SuspendLayout();
                if (tempBlinkStatus)
                {

                    btnLink.ForeColor = LinkBlinkerBackground;
                    btnLink.BackColor = LinkBlinkerForeground;
                }
                else
                {
                    btnLink.ForeColor = LinkBlinkerForeground;
                    btnLink.BackColor = LinkBlinkerBackground;
                }
                btnBlankLines.ResumeLayout();
            }
        }

        // Call up the linked entry in AHOY!
        private void btnCallUp_Click(object sender, EventArgs e)
        {
            // Refresh activity counters
            Reaction();
            CallUp();
        }

        private void CallUp()
        {
            if (PlopIsLinked)
            {
                SubmitCallUpRequest();
            }
        }


        // Set TopMost from MainForm
        public void SetTopMost(bool state)
        {
            this.TopMost = state;
            IsTopMost = this.TopMost;
            ResolveTopMostButton();
        }

        // Toggle TopMost from PLOP and submit to MainForm
        public void ToggleTopMost()
        {
            Reaction();

            if (TopMost)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Disable Top Most position on desktop?", MessageText.PlopEditorCaption(), false, false);
                msg.CenterParent();
                msg.BringToFront();
                msg.ShowDialog();
                if (!msg.Accepted)
                {
                    return;
                }
            }

            this.TopMost = !this.TopMost;
            IsTopMost = this.TopMost;
            MainForm.PlopTopMost = this.TopMost;
            MainForm.ForceSaveSettings();
            ResolveTopMostButton();
        }

        public void ResetTopMost()
        {
            IsTopMost = UEDefaultTopMost;
            this.TopMost = IsTopMost;
            MainForm.PlopTopMost = IsTopMost;
            MainForm.ForceSaveSettings();
            ResolveTopMostButton();
        }


        private void btnTopMost_Click(object sender, EventArgs e)
        {
            ToggleTopMost();
        }

        private void DisplayPlopSplashLogo()
        {
            try
            {
                lbSplash.Visible = true;
                lbSplash.BringToFront();
                lbSplash.Font = Design.ContentFieldFont();
                lbSplash.Text = IO.PlopEditorLogoTextReader();
                timerLogoSplash.Interval = 1200;
                timerLogoSplash.Enabled = true;
            }
            catch
            {
                timerLogoSplash.Enabled = false;
            }
        }

        private void timerLogoSplash_Tick(object sender, EventArgs e)
        {
            lbSplash.Visible = false;
            timerLogoSplash.Enabled = false;
            SplashDisplayed = false;
            // MicroCalculator
            microCalculator1.CheckShowControl();
            SelectTextVersion(0);
            TransferTextAndCaretPositionsToEditor();
            ResolveAdditionalSettingsButtons();
        }


        private string tempClipboard;
        private void generatePasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clipboard sometimes fails, hence the try-catch
            try
            {
                PasswordTypeSelector pwts = new PasswordTypeSelector(MainForm.SelectedPasswordStyle);
                pwts.TopMost = true;
                pwts.ShowDialog();

                if (pwts.GenerationSuccess)
                {
                    // Record user selection
                    if (MainForm.SelectedPasswordStyle != pwts.CurrentSelection)
                    {
                        MainForm.SelectedPasswordStyle = pwts.CurrentSelection;
                        MainForm.SettingsHasChanges = true;
                    }

                    // Save clipboard content to temp variable
                    tempClipboard = "";
                    if (Clipboard.ContainsText())
                    {
                        tempClipboard = Clipboard.GetText(TextDataFormat.Text);
                    }

                    SetClipboardText(pwts.GetGeneratedKeys());

                    // Paste password text to textbox
                    GetCurrentTextBox().Paste();

                    // Add previous content (if any) back to clipboard
                    if (tempClipboard.Length > 0)
                    {
                        SetClipboardText(tempClipboard);
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                // Do nothing
            }
        }

        // Copy normal (with special chars etc.)
        private void copySpecialCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTextBox().Copy();
            MainForm.PersistClipboard();
        }

        private void cutSpecialCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTextBox().Cut();
            MainForm.PersistClipboard();
        }



        private void BlinkForm()
        {
            if (!timerFormBlinker.Enabled)
            {
                SetPlopMarqueeTexPLOPPED();
                timerFormBlinker.Interval = 50;
                timerFormBlinker.Enabled = true;
            }
        }

        private int tempCurrentColor;
        private void timerFormBlinker_Tick(object sender, EventArgs e)
        {

            if (tempCurrentColor > 2)
            {
                timerFormBlinker.Enabled = false;
                this.BackColor = FormBlinkerA;
                tempCurrentColor = 0;
                ForceMarqueeTextPLOP = true;
                UpdateUI(3000);
                return;
            }

            if (tempCurrentColor >= 0)
            {
                switch (tempCurrentColor)
                {
                    case 0:
                        this.BackColor = FormBlinkerB; // Highlight I
                        break;
                    case 1:
                        this.BackColor = FormBlinkerC; // Highlight II
                        break;
                    case 2:
                        this.BackColor = FormBlinkerA; // Normal
                        break;

                }
            }

            // Change color
            tempCurrentColor += 1;
        }

        private void clearPLOPContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTextBox().SetText("", false);
            MainForm.PlopPersistedContent = "";
            FocusContent();
        }

        private void clearPLOPContentAndUtilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTextBox().SetText("", false);
            MainForm.PlopPersistedContent = "";

            tbPlopUtilityText.SetText("", false);
            MainForm.PlopUtilityContent = "";
            FocusContent();
        }

        private void insertDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertDate();
        }

        // Insert Date
        private void InsertDate()
        {
            GetCurrentTextBox().InsertISOTimestamp();
        }

        // Insert Paragraph divider
        private void InsertParagraphDivider(int selection)
        {
            switch (selection)
            {
                case 0:
                    GetCurrentTextBox().InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
                case 1:
                    GetCurrentTextBox().InsertParagraphDivider(ParagraphDividers.DividerBString);
                    break;
                case 2:
                    GetCurrentTextBox().InsertParagraphDivider(ParagraphDividers.DividerCString);
                    break;
                case 3:
                    GetCurrentTextBox().InsertParagraphDivider(ParagraphDividers.DividerDString);
                    break;
                default:
                    GetCurrentTextBox().InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
            }
        }

        private void insertParagraphDividerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertParagraphDivider(0);
        }

        private void viewSelectionInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectionInNewWindow();
        }


        private void OpenSelectionInNewWindow()
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                OpenSnippetWindow(GetCurrentTextBox().SelectedText);
            }
            else
            {
                OpenSnippetWindow("");
            }
        }

        private void OpenSnippetWindow(string content)
        {
            try
            {
                AhoySearchMessageBox messageBox = new AhoySearchMessageBox(content, MessageText.SnippetCaption() + TextTransform.GetBlankSpace(2) + MessageText.NotPersistedOnCloseExitCaption(), false, false, this.Opacity);
                messageBox.FormClosed += MessageBox_FormClosed;
                messageBox.TopMost = true;
                messageBox.Show();
            }
            catch
            {
                // Do nothing
            }
        }

        private void MessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetCurrentTextBox().Focus();
        }


        #region CLIPBOARD SET OPERATION (addition 22-01-2017)
        private static void SetClipboardText(string input)
        {
            try
            {
                // Set clipboard
                Clipboard.SetDataObject(
                        input, // Text
                        false, // Do not persist on application exit
                        7,     // Retry times
                        200    // Delay (ms) between retrys
                        );

                // Check that the clipboard was set and try again if not
                if (!Clipboard.ContainsText() || Clipboard.GetText() != input)
                {
                    Log.Log1stClipboardError();
                    Clipboard.SetDataObject(
                     input, // Text
                     false, // Do not persist on application exit
                     7,     // Retry times
                     250    // Delay (ms) between retrys
                     );
                }

                // Log further error
                if (!Clipboard.ContainsText() || Clipboard.GetText() != input)
                {
                    Log.Log2ndClipboardError();
                }
            }
            catch
            {
                // Do nothing
            }

        }
        #endregion

        #region Utility text field behaviour
        int tempOriginalUtilityLocationX;
        int tempOriginalUtilityLocationY;
        int tempOriginalUtilityHeight;
        int tempUtilityGrowth = 150;
        private void tbPlopUtilityText_MouseClick(object sender, MouseEventArgs e)
        {
            // Only call method if not expanded
            if (!PlopUtilityEditorExpanded)
            {
                TogglePlopUtilityExpansion();
            }
        }

        private void TogglePlopUtilityExpansion()
        {
            if (!PlopUtilityEditorExpanded)
            {
                PlopUtilityEditorExpanded = true;
                RegisterTextChanged = false;
                // WPM counter
                UpdateWPMValues(true);
                // Record text box dimension and location
                tempOriginalUtilityLocationX = tbPlopUtilityText.Location.X;
                tempOriginalUtilityLocationY = tbPlopUtilityText.Location.Y;
                tempOriginalUtilityHeight = tbPlopUtilityText.Height;

                // Make text box higher (Y = vertical position)
                tbPlopUtilityText.SuspendLayout();
                tbPlopUtilityText.BackColor = ExpansionColorB;
                tbPlopUtilityText.ForeColor = ExpansionColorA;
                tbPlopUtilityText.Height += tempUtilityGrowth;
                tbPlopUtilityText.Location = new Point(tempOriginalUtilityLocationX, tempOriginalUtilityLocationY - tempUtilityGrowth);
                tbPlopUtilityText.BringToFront();
                tbPlopUtilityText.ResumeLayout();
                tbPlopUtilityText.Refresh();
                tbPlopUtilityText.Focus();
                RegisterTextChanged = true;
            }
            else if (PlopUtilityEditorExpanded)
            {
                PlopUtilityEditorExpanded = false;
                RegisterTextChanged = false;

                // WPM counter
                UpdateWPMValues(false);
                // Make text box higher (Y = vertical position)
                tbPlopUtilityText.SuspendLayout();
                tbPlopUtilityText.BackColor = ExpansionColorA;
                tbPlopUtilityText.ForeColor = ExpansionColorB;
                tbPlopUtilityText.Height = tempOriginalUtilityHeight;
                tbPlopUtilityText.Location = new Point(tempOriginalUtilityLocationX, tempOriginalUtilityLocationY);
                tbPlopUtilityText.BringToFront();
                tbPlopUtilityText.ResumeLayout();
                tbPlopUtilityText.Refresh();
                GetCurrentTextBox().Focus();
                RegisterTextChanged = true;
            }
        }



        #endregion


        #region WMP counter
        private void UpdateWPMValues(bool countUtilityText)
        {
            if (countUtilityText)
            {
                WPMCounter.ResetWPMcounter();
                SetStatusLabel(WPMCounter.GetWordCount(tbPlopUtilityText.Text));
            }
            else
            {
                WPMCounter.ResetWPMcounter();
                SetStatusLabel(WPMCounter.GetWordCount(GetCurrentTextBox().Text));
            }
        }

        #endregion




        #region Notification indicator
        private void timerGlobalTimer_Tick(object sender, EventArgs e)
        {
            // Auto save
            if (InvokeAutoSave)
            {
                InvokeAutoSave = false;
                MainForm.SetPoolHasChanges();
            }

            // Prism effect
            StartColorShiftTimer();

            // Form border
            SetWindowBorder();

            // Check current entry state
            if (IsPlopEditor && InvokeEntryCheck)
            {
                RetrievePlopEntryState();
            }

            // Toggle eye button
            if (MainForm.AllStickiesOpen() && MainForm.TopMostFalseStickiesExist())
            {
                if (!pnlShowAllStickies.Visible)
                {
                    pnlShowAllStickies.Visible = true;
                }
            }
            else
            {
                if (pnlShowAllStickies.Visible)
                {
                    pnlShowAllStickies.Visible = false;
                }
            }

            // Blink title tekst if notification editor is open (raised or not raised)
            if (MainForm.NotificationEditorOpen && MainForm.NotificationRaised && !NotificationEditorOpen)
            {
                NotificationEditorOpen = true;

                if (IsPlopEditor)
                {
                    this.Text = "PLOP! *** NOTIFICATION ***";
                    this.BackColor = AhoyColors.CurrentColorTheme()[6];
                    SetStatusLabel("*** NOTIFICATION LOCK ***");
                }
                else
                {
                    this.Text = "Utility Editor *** NOTIFICATION ***";
                    this.BackColor = AhoyColors.CurrentColorTheme()[6];
                    SetStatusLabel("*** NOTIFICATION LOCK ***");
                }
            }
            else
            {
                if (NotificationEditorOpen)
                {
                    NotificationEditorOpen = false;
                    if (IsPlopEditor)
                    {
                        this.Text = MessageText.PlopEditorCaption();
                        this.BackColor = AhoyColors.CurrentColorTheme()[0];
                        SetStatusLabel("");
                    }
                    else
                    {
                        this.Text = MessageText.UtilityEditorCaption();
                        this.BackColor = AhoyColors.CurrentColorTheme()[0];
                        SetStatusLabel("");
                    }
                }
            }

            if (RefocusContent)
            {
                RefocusContent = false;
                this.BringToFront();
                GetCurrentTextBox().Focus();
            }
        }
        #endregion


        #region Text Decoration
        private void SelectionToSlickFrame(bool caps)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = TextTransform.TextToSlickFrame(GetCurrentTextBox().SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }

        private void SelectionToEmbossedFrame(bool caps)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = TextTransform.TextToEmbossedFrame(GetCurrentTextBox().SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }

        private void SelectionToBullet(bool caps)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = TextTransform.SelectionToBullet(GetCurrentTextBox().SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }

        private void SelectionToUnderline(bool caps)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = TextTransform.SelectionToUnderline(GetCurrentTextBox().SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUnderline(false);
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBullet(false);
        }

        private void slickFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToSlickFrame(false);
        }

        private void embossedFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToEmbossedFrame(false);
        }

        private void selectionToUppercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUpper();
        }

        private void selectionToLowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToLower();
        }

        private void SelectionToUpper()
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = GetCurrentTextBox().SelectedText.ToUpper();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }

        private void SelectionToLower()
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                GetCurrentTextBox().SelectedText = GetCurrentTextBox().SelectedText.ToLower();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }
        #endregion


        #region STICKY


        private void pnlShowAllStickies_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HideSticky();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (MainForm.AllStickiesClosed())
                {
                    SpawnSticky();
                }
                else
                {
                    BringStickiesToFront();
                }
            }
        }

        private void pnlSpawnSticky_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HideSticky();
            }
            else if (e.Button == MouseButtons.Left)
            {
                SpawnSticky();
            }
        }


        private void newStickyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnSticky();
        }

        // Set tooltip
        private void PnlShowAllStickies_MouseEnter(object sender, EventArgs e)
        {
            ResolveBtnShowToolTip();
        }

        private void ResolveBtnShowToolTip()
        {
            if (pnlShowAllStickies.Visible)
            {
                this.toolTip1.SetToolTip(this.pnlShowAllStickies, GetStickyCountForToolTip());
            }
        }

        private void bringStickiesToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.AllStickiesClosed())
            {
                SpawnSticky();
            }
            else
            {
                BringStickiesToFront();
            }
        }

        private void hideStickiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideSticky();
        }

        private void BringStickiesToFront()
        {
            MainForm.BringStickiesToFront();
            RefocusContent = true;
        }

        private void HideSticky()
        {
            MainForm.HideStickiesRemote();
            RefocusContent = true;
        }

        private void SpawnSticky()
        {
            // Tell main form to spawn new stickie and send position data
            MainForm.SpawnStickyPerRemote(this.Left + 20, this.Top + 20);
        }


        private string tempStickyCountText;
        private void SetStickyCount()
        {
            if (MainForm.TopMostFalseStickiesExist())
            {
                this.bringStickiesToFrontToolStripMenuItem.Visible = true;
                tempStickyCountText = "";
                if (MainForm.GetStickyCount() == 0)
                {
                    tempStickyCountText = "Stickies (none)";

                }
                else if (MainForm.GetStickyCount() == 1)
                {
                    tempStickyCountText = MainForm.GetStickyCount() + " Sticky";
                }
                else if (MainForm.GetStickyCount() > 1)
                {
                    tempStickyCountText = MainForm.GetStickyCount() + " Stickies";
                }

                this.bringStickiesToFrontToolStripMenuItem.Text = "Bring " + tempStickyCountText + " To &Front";
            }
            else
            {
                this.bringStickiesToFrontToolStripMenuItem.Visible = false;
            }
        }

        private void contextMenuUtilityEditor_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetStickyCount();
        }

        private string tempStickyCountForToolTip;
        private string GetStickyCountForToolTip()
        {
            if (MainForm.TopMostFalseStickiesExist())
            {
                tempStickyCountForToolTip = "";
                if (MainForm.GetStickyCount() == 1)
                {
                    tempStickyCountForToolTip = MainForm.GetStickyCount() + " Sticky";
                }
                else if (MainForm.GetStickyCount() > 1)
                {
                    tempStickyCountForToolTip = MainForm.GetStickyCount() + " Stickies";
                }
                else
                {
                    tempStickyCountForToolTip = "Stickies";
                }

                return "Bring " + tempStickyCountForToolTip + " To Front - Right Click To Hide All (CTRL+SHIFT+H)";
            }
            else
            {
                return "Bring Stickies To Front - Right Click To Hide All (CTRL+SHIFT+H)";
            }
        }


        #endregion


        #region Calculator
        private void picCalculator_Click(object sender, EventArgs e)
        {
            ToggleMicroCalculator();
        }

        // Used when switching inputboxes to avoid the control from getting hidden
        private void ForceHideCalculator()
        {
            if (microCalculator1.Visible)
            {
                microCalculator1.HideControlWithoutSavingState();
            }
        }

        // Show/Hide Micro Calculator
        private void ToggleMicroCalculator()
        {
            Reaction();
            if (!microCalculator1.Visible)
            {
                microCalculator1.ReturnFocusTo(GetCurrentTextBox());
                microCalculator1.ShowControl();
            }
            else
            {
                microCalculator1.HideControl(false);
            }
        }

        #endregion

        #region Settings
        private void SetStatusLabel(string text)
        {
            if (IsPlopEditor)
            {
                toolStripStatusLabel1.Text = GetPlopDirectionStatusText() + TextTransform.GetBlankSpace(3) + text;
            }
            else
            {
                toolStripStatusLabel1.Text = text;
            }
        }


        private void picGear_Click(object sender, EventArgs e)
        {
            Reaction();
            if (!pnlSettings.Visible)
            {
                pnlSettings.Visible = true;
                pnlSettings.BringToFront();
                btnTogglePlopDirection.Focus();
            }
            else
            {
                pnlSettings.Visible = false;
                FocusContent();
            }
        }

        private void TogglePlopDirection()
        {
            Reaction();
            MainForm.PlopDirection = !MainForm.PlopDirection;
            MainForm.SettingsHasChanges = true;
            ResolveTogglePlopDirectionButton();
        }

        private void ResolveTogglePlopDirectionButton()
        {

            btnTogglePlopDirection.Text = GetPlopDirectionText();
            SetStatusLabel("");
        }

        private string GetPlopDirectionStatusText()
        {
            if (MainForm.PlopDirection)
            {
                return "PLOP to TOP";
            }
            else
            {
                return "PLOP to END";
            }
        }

        private string GetPlopDirectionText()
        {
            if (MainForm.PlopDirection)
            {
                return "TOP";
            }
            else
            {
                return "END";
            }
        }

        private void btnTogglePlopDirection_Click(object sender, EventArgs e)
        {
            TogglePlopDirection();
        }

        private void CloseSettingsPanel()
        {
            Reaction();
            if (pnlSettings.Visible)
            {
                pnlSettings.Visible = false;
                FocusContent();
            }
        }

        private void btnCloseSettingsPanel_Click(object sender, EventArgs e)
        {
            CloseSettingsPanel();
        }

        #endregion


        #region Check form position (addition 22-03-2018)
        bool isInsideBounds;
        public bool IsOnScreen(Form form)
        {
            try
            {
                Log.UEEditor("UE/PLOP: Checking screen bounds");
                isInsideBounds = false;

                // Create rectangle
                Rectangle formRectangle = new Rectangle(form.Left, form.Top, form.Width, form.Height);
                isInsideBounds = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(formRectangle));

                if (!isInsideBounds)
                {
                    Log.UEEditor("UE/PLOP: Outside bounds");
                }

                return isInsideBounds;
            }
            catch
            {
                Log.UEEditor("Error: UE/PLOP Screen bounds check");
                return true;
            }
        }
        #endregion

        #region Search and replace (addition 19-04-2018)
        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxes.SearchAndReplace replaceDialog = new MessageBoxes.SearchAndReplace(GetCurrentTextBox());
            replaceDialog.TopMost = true;
            replaceDialog.StartPosition = FormStartPosition.CenterParent;
            replaceDialog.ShowDialog();
            if (replaceDialog.Success)
            {
                //GetCurrentTextBox().SetText(replaceDialog.ResultText, false);
                PostDialogAfterReplacing(replaceDialog.GetMatchCount());
            }
        }

        private string tempFindReplaceResult;
        private void PostDialogAfterReplacing(int countReplaced)
        {
            if (countReplaced > 0)
            {
                if (countReplaced == 1)
                {
                    tempFindReplaceResult = "1 occurrence replaced";
                }
                else
                {
                    tempFindReplaceResult = countReplaced.ToString() + " occurrences replaced";
                }
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(tempFindReplaceResult, MessageText.FindAndReplaceCaption(), false);
                msg.CenterParent();
                msg.ShowDialog();
            }
        }
        #endregion


        #region Additional PLOP settings
        private void ResolveAdditionalSettingsButtons()
        {
            // Blank lines button
            btnBlankLines.Text = GetOnOffButtonText(MainForm.UEBlankLines);
            if (MainForm.UEBlankLines)
            {
                btnBlankLines.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnBlankLines.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }

            // Trim button
            btnTrim.Text = GetOnOffButtonText(MainForm.UETrimWhitespace);
            if (MainForm.UETrimWhitespace)
            {
                btnTrim.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnTrim.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }

            // ESC to close button
            btnEscToClose.Text = GetOnOffButtonText(MainForm.UEESCtoClose);
            if (MainForm.UEESCtoClose)
            {
                btnEscToClose.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnEscToClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private void btnBlankLines_Click(object sender, EventArgs e)
        {
            ToggleBlankLines();
        }

        private void ToggleBlankLines()
        {
            Reaction();
            MainForm.UEBlankLines = !MainForm.UEBlankLines;
            MainForm.SettingsHasChanges = true;
            btnBlankLines.Text = GetOnOffButtonText(MainForm.UEBlankLines);
            ResolveAdditionalSettingsButtons();
        }


        private void btnTrim_Click(object sender, EventArgs e)
        {
            ToggleTrim();
        }

        private void ToggleTrim()
        {
            Reaction();
            MainForm.UETrimWhitespace = !MainForm.UETrimWhitespace;
            MainForm.SettingsHasChanges = true;
            btnTrim.Text = GetOnOffButtonText(MainForm.UETrimWhitespace);
            ResolveAdditionalSettingsButtons();
        }

        private string GetOnOffButtonText(bool state)
        {
            if (state)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        private void btnEscToClose_Click(object sender, EventArgs e)
        {
            ToggleEscToClose();
        }

        private void ToggleEscToClose()
        {
            Reaction();
            MainForm.UEESCtoClose = !MainForm.UEESCtoClose;
            MainForm.SettingsHasChanges = true;
            btnEscToClose.Text = GetOnOffButtonText(MainForm.UEESCtoClose);
            ResolveAdditionalSettingsButtons();
        }
        #endregion


        #region Prism effect
        private void StartColorShiftTimer()
        {
            if (!IsPlopEditor) // Only use prism effect with plop
            {
                return;
            }

            if (MainForm.PrismEffect == false) // If not enabled but running, stop it and return.
            {
                if (tempColorShiftTimer != null && tempColorShiftTimer.Enabled)
                {
                    StopColorShiftTimer();
                }
                return;
            }

            // Create timer instance
            if (tempColorShiftTimer == null)
            {
                tempColorShiftTimer = new Timer();
                tempColorShiftTimer.Interval = 5;
                tempColorShiftTimer.Tick += TempColorShiftTimer_Tick;
            }

            if (tempColorShiftTimer != null && !tempColorShiftTimer.Enabled)
            {
                ShiftTimerCounter = ShiftTimerCounterDefault;
                tempColorShiftTimer.Enabled = true;
                Log.GeneralStatus("Prism eff. start (PLOP/UE)");
            }
        }

        private void StopColorShiftTimer()
        {
            if (tempColorShiftTimer != null && tempColorShiftTimer.Enabled)
            {
                tempColorShiftTimer.Enabled = false;
                Log.GeneralStatus("Prism eff. stop (PLOP/UE)");
                // Retrieve colors
                toolStripStatusLabel1.ForeColor = AhoyColors.CurrentColorTheme()[6];
                btnCallUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        // Color shift timer
        private float ShiftTimerCounter = 0f;
        private const float ShiftTimerCounterDefault = 0f;
        Timer tempColorShiftTimer;
        private void TempColorShiftTimer_Tick(object sender, EventArgs e)
        {
            ShiftTimerCounter += 0.001f;
            if (ShiftTimerCounter > 1)
            {
                ShiftTimerCounter = ShiftTimerCounterDefault;
            }

            // Colorize status and history button color
            toolStripStatusLabel1.ForeColor = Rainbow(ShiftTimerCounter);
            btnCallUp.ForeColor = Rainbow(ShiftTimerCounter);

        }

        // Generate color from input
        private static int MaxValue = 255;
        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * MaxValue);
            int descending = MaxValue - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(MaxValue, MaxValue, ascending, 0);
                case 1:
                    return Color.FromArgb(MaxValue, descending, MaxValue, 0);
                case 2:
                    return Color.FromArgb(MaxValue, 0, MaxValue, ascending);
                case 3:
                    return Color.FromArgb(MaxValue, 0, descending, MaxValue);
                case 4:
                    return Color.FromArgb(MaxValue, ascending, 0, MaxValue);
                default: // case 5:
                    return Color.FromArgb(MaxValue, MaxValue, 0, descending);
            }
        }
        #endregion

        #region Window border (addition 12-06-2019)
        private bool WindowBorder { get; set; }
        private void SetWindowBorder()
        {
            LockedForPositioning = true;
            if (MainForm.EnableFormBorder && !WindowBorder)
            {
                WindowBorder = true;
                ShowFormBorder();
            }
            else if (!MainForm.EnableFormBorder && WindowBorder)
            {
                WindowBorder = false;
                HideFormBorder();
            }
            LockedForPositioning = false;
        }

        private void HideFormBorder()
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                statusStrip1.SizingGrip = false;
                // Move form
                this.MouseDown += Form_MouseDown;
                this.MouseUp += Form_MouseUp;
                this.MouseMove += Form_MouseMove;
            }
            catch
            {
                // Do nothing
            }
        }

        private void ShowFormBorder()
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                statusStrip1.SizingGrip = true;
                this.MouseDown -= Form_MouseDown;
                this.MouseUp -= Form_MouseUp;
                this.MouseMove -= Form_MouseMove;
                FocusContent();
            }
            catch
            {
                // Do nothing
            }
        }

        private bool mouseDown;
        private Point lastLocation;
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
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

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
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
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                if (!direction)
                {
                    GetCurrentTextBox().SelectedText = TextTransform.SortText(GetCurrentTextBox().SelectedText, false);
                }
                else if (direction)
                {
                    GetCurrentTextBox().SelectedText = TextTransform.SortText(GetCurrentTextBox().SelectedText, true);
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
