using System;
using AhoyBrowser.Crypto;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class MainForm : Form
    {
        #region VARIABLES
        // Key derived from the user's password (PBKDF2); the password itself is never kept
        private PoolKey CurrentKey { get; set; }
        internal const int MaxPasswordLength = 64;
        internal static bool InterfaceLock { get; set; }
        private static bool ShowPinAndMemoAnimation { get; set; }
        private static bool MemoWasLastVisible { get; set; }
        private static bool MemoIsVisible { get; set; }
        private static bool PinnedIsVisible { get; set; }
        private static bool ShowPinnedAnimationOnEmptyPinnedList { get; set; }
        private static bool AccessGranted { get; set; }
        private static bool LoggedInOnce { get; set; }
        private static Entry CurrentEntry { get; set; }
        internal static List<Entry> Entries { get; set; }
        internal static List<Entry> EntriesMirror { get; set; } // Used for the Undo All command (load content since opening the pool)
        internal static bool PoolHasChanges { get; set; }
        internal static bool SettingsHasChanges { get; set; }
        private static bool ReturnToEditorAfterSleep { get; set; }
        private Brush ListBoxSearchResultSelectedForeColor { get; set; }
        private Brush ListBoxSearchResultForeColor { get; set; }
        private Brush ListBoxPinnedSelectedForeColor { get; set; }
        private Brush ListBoxPinnedForeColor { get; set; }
        private Color ListBoxPinnedHighlightColor { get; set; }
        private static bool LockedForWritingToDisk { get; set; }
        private static bool HoldEntryA { get; set; }
        private static string HoldEntryAID { get; set; }
        private static bool HoldEntryB { get; set; }
        private static string HoldEntryBID { get; set; }
        internal static int FieldsFontSize { get; set; }
        private static int ListsFontSize { get; set; }
        private static bool IsFullScreen { get; set; }
        private static bool IsMaximized { get; set; }
        private static Size WindowDefaultSize { get; set; }
        private static string CustomDisplayText { get; set; }
        private static bool ForceDefaultDisplayText { get; set; }
        private const int DefaultSleepInterval = 3600;  // 3600 = 60 minutes
        private const int NeverSleepValue = 99999;  // Value of 99999 indicates that Ahoy should stay awake
        private int SleepIntervalSeconds { get; set; }
        private int SleepIntervalIndex { get; set; }
        internal static int BackupIdentifier { get; set; }
        internal static int AlternateBackupIdentifier { get; set; }
        internal static bool ForceBackup { get; set; }
        internal static bool AttachedEditorOpen { get; set; }
        private int MemoCaretPosition { get; set; }
        private int LayoutMode { get; set; }
        private static bool AutoHidePinnedAndMemo { get; set; }
        private static bool PinnedAndMemoAreHidden { get; set; }
        private bool ListsActUponMouseMovement { get; set; }
        private bool AnimationsEnabled { get; set; }
        internal static int SpellCheckerLanguage { get; set; }
        private static string StoredClipboardString { get; set; }
        internal static int SelectedInternetBrowser { get; set; }
        private static bool DetectURLs { get; set; }
        internal static int SnoozeInterval { get; set; }
        internal static bool AutoSaveEntryChanges { get; set; }
        private static bool EntrySelectedBySearchResult { get; set; }
        private static bool AutoSearchInProgress { get; set; }
        private static bool AutoSearch { get; set; }
        private static bool IsSecondaryMatch { get; set; }
        public static string DividerAString { get; set; }
        public static string DividerBString { get; set; }
        public static string DividerCString { get; set; }
        public static string DividerDString { get; set; }

        

        // Utility/PLOP Editor
        internal static bool StyleEditorHasChanges { get; set; }
        internal static int CurrentLocationX { get; set; }
        internal static int CurrentLocationY { get; set; }
        internal static bool NewEntryFromUE { get; set; }

        internal static bool SearchFromUE { get; set; }
        internal static bool TogglePinFromUE { get; set; }
        internal static bool OpenPoolFromUE { get; set; }
        internal static bool OpenQuickGuideFromUE { get; set; }

        internal static bool ActivateMainForm { get; set; }

        // Notifications
        private static bool EntrySelectionInProgress { get; set; }
        private bool EntrySavedByDialog { get; set; }
        internal static int SelectedPasswordStyle { get; set; }
        internal static string OpenFilePath { get; set; }
        internal static string SaveFilePath { get; set; }
        internal static bool PoolLoadFromFileDialogSuccess { get; set; }
        internal static bool PoolSaveWithFileDialogSuccess { get; set; }
        private static bool LogMonitorEnabled { get; set; }
        private bool Dump { get; set; }
        internal static bool MasterCloseStickies { get; set; }
        internal static int MasterCloseStickiesCode { get; set; }
        private const string Srv = "debug";
        private static bool SpawnCommandPrompt { get; set; }

        // Default and minimum window size
        internal const int WindowsDefaultWidth = 1050;
        internal const int WindowsDefaultHeight = 750;
        internal const int WindowsMinimumWidth = 1050;
        internal const int WindowsMinimumHeight = 700;
        internal const int DefaultSplitterDistance = 330;
        internal static int CurrentWindowWidth { get; set; }
        internal static int CurrentWindowHeight { get; set; }
        internal static string FormTitle = MessageText.AhoyCaption();
        internal static double FormOpacity { get; set; }
        internal static int SelectedTexture { get; set; }
        internal static int SelectedTexturePreview { get; set; }
        private static int RuntimeAutoBackupSeconds { get; set; }
        private int SearchBoxResultCycleIndex { get; set; }

        // Backup parameters
        internal static string AlternateBackupPath { get; set; }
        internal static bool AlternateBackupPathSet { get; set; }
        private int PoolPreviousCharCount { get; set; }
        private int PoolCurrentCharCount { get; set; }
        private int MainPoolAutoSaveCount { get; set; }
        private bool DisplayPoolParameters { get; set; }
        private static string Changes { get; set; }

        // Cypher parameters
        private string CypherKey { get; set; }

        // WPM Counter parameters
        internal static bool ShowWPMCounter { get; set; }

        // Pool Read-Only parameters
        internal static bool PoolIsReadOnly { get; set; }
        private static bool OverrideToSetWriteProtection { get; set; }

        // PD Editor
        private bool PDEditorOpen { get; set; }

        // TypeWriter

        // Typermax

        // MicroCalculator

        // DisplayText cycle
        public static int DisplayTextCycle { get; set; }

        // Tab Title to Content
        public static bool TitleTabToContent { get; set; }

        // Insert New Lines
        public static bool InsertLines { get; set; }

        // Autosave auto-blinker
        private static Timer AutosaveBlinkerTimer { get; set; }

        // Screen blocker

        // Global settings blocker
        private bool AllowSavingSettings { get; set; }

        // Prism Effect

        // Random numbers

        // Clock
        private bool ShowClock { get; set; }

        // Pinned sorting
        private bool CanUndoSort { get; set; }

        // Search
        private bool LiveSearch { get; set; }
        private bool TitlePrioritySearch { get; set; }

        // Pinned by click
        private bool NewPinnedByClick { get; set; }
        private bool HidePinnedByClick { get; set; }

        // Keyboard Control
        public static bool MoveByKeyboard { get; set; }
        public static bool ResizeByKeyboard { get; set; }

        // TAB memory
        private string TabMemoryA { get; set; }
        private string TabMemoryB { get; set; }

        // PAUSE
        internal static bool IsPaused { get; set; }

        // Timestamp
        public static int TimeStampFormat { get; set; }

        // --------------------------------------------------------
        // Release date and time (timestamp) / timestamp (seconds omitted, like this "202208081401")
        // --------------------------------------------------------

        #endregion
        public MainForm()
        {
            #region APPLICATION INIT
            // Initialize application
            InitializeComponent();
            InitializedRelatedContextMenuItems();

            // Pause
            IsPaused = false;

            // Timestamp
            TimeStampFormat = 0;

            // Tab memory
            InitializeTabMemory();

            // Form border
            EnableFormBorder = Settings.DefaultEnableFormBorder;

            // Pinned by click
            NewPinnedByClick = Settings.DefaultNewPinnedByClick;

            // Allow saving settings
            AllowSavingSettings = true;

            // Initialize custom filepath
            IO.InitializeInitProperties();
            IO.ClearCustomFilePath(false);

            // Create homescreen label
            InitializeHomeScreenLabel();

            // Autosave blinker timer
            AutosaveBlinkerTimer = new Timer();
            AutosaveBlinkerTimer.Interval = 3000;
            AutosaveBlinkerTimer.Enabled = false;
            AutosaveBlinkerTimer.Tick += AutosaveBlinkerTimer_Tick;

            // Sort pinned
            CanUndoSort = false;

            // Calculator

            // Initialize commands
            CommandInput.InitializeCommands();

            // Prism effect

            // Random numbers

            // Clock
            ShowClock = Settings.DefaultShowClock;



            // Ready log
            Log.InitializeLog();
            Log.LogProgramStartup();
            // Load Init file
            IO.InitializeInitFile(); // Must be called after initializing the log
            Dump = false;
            MasterCloseStickies = false;
            MasterCloseStickiesCode = 0;
            SpawnCommandPrompt = false;
            // Set initial readonly mode to false = Read/Write permission
            OverrideToSetWriteProtection = false;
            TogglePoolReadOnly(true, false, false);
            SetIndicatorLabel("READ-ONLY", "", false);
            lbReadOnlyIndicator.Click += LbReadOnlyIndicator_Click;
            // TypeWriter
            DisplayTextCycle = Settings.DefaultDisplayTextCycle;
            HideLoadingPanel();
            // Typermax
            // Use primary, secondary and demo Pool
            IO.UseDemoPool = false;
            IO.UseSecondaryPool = false; // Use primary
            SleepIntervalSeconds = DefaultSleepInterval;
            AlternateBackupPath = "";
            RuntimeAutoBackupSeconds = 0;
            PoolPreviousCharCount = 0;
            PoolCurrentCharCount = 0;
            MainPoolAutoSaveCount = 0;
            AlternateBackupPathSet = false;
            DisplayPoolParameters = false;
            CypherKey = "";
            ShowWPMCounter = false;
            Changes = "";
            EntrySelectionInProgress = false;
            EntrySavedByDialog = true; // Must be True
            SelectedPasswordStyle = Settings.DefaultPasswordStyle;
            AutoSaveEntryChanges = Settings.DefaultAutoSaveEntryChanges;
            OpenFilePath = "";
            SaveFilePath = "";
            PoolLoadFromFileDialogSuccess = false;
            PoolSaveWithFileDialogSuccess = false;
            EntrySelectedBySearchResult = false;
            AutoSearchInProgress = false;
            AutoSearch = Settings.DefaultAutoSearch;
            autoSearchToolStripMenuItem.Checked = AutoSearch;
            IsSecondaryMatch = false;
            DividerAString = "";
            DividerBString = "";
            DividerCString = "";
            DividerDString = "";
            PDEditorOpen = false;
            ResetParagraphDividers();
            LogMonitorEnabled = false;
            // Export
            btnExport.Visible = false;
            btnClearExportMarks.Visible = false;

            // Set default color theme
            AhoyColors.ColorTheme = 0;
            ButtonBitmaps.ButtonBitmapVersion = 0;
            timerReselectPinned.Interval = 100;
            LayoutMode = Settings.DefaultLayoutMode;
            BackupIdentifier = 0;
            AlternateBackupIdentifier = 0;
            MemoCaretPosition = 0;
            ForceBackup = false;
            AttachedEditorOpen = false;
            Navigator.InitializeNavigator();
            ShellCommands.InitializeShellCommandsList();
            WPMCounter.InitializeSpeedTimer();
            lbStatus2.Text = "";
            InitializeSearchHistoryList();
            this.Text = GetFormTitle();
            CustomDisplayText = "";
            ForceDefaultDisplayText = false;
            AnimationsEnabled = true;
            StoredClipboardString = "";
            SelectedTexture = 0;
            StyleEditorHasChanges = false;
            NewEntryFromUE = false;
            SearchFromUE = false;
            TogglePinFromUE = false;
            OpenPoolFromUE = false;
            OpenQuickGuideFromUE = false;
            ActivateMainForm = false;

            // Initialize dynamic controls
            InitializeAnimationLabel();
            InitializeMemoField();
            InitializeBasicContextMenus();
            InitializeSearchResultLabel();

            // Set flags
            SelectedInternetBrowser = Settings.DefaultBrowser;
            ResolveDefaultBrowserCheckedMenuItems();
            DetectURLs = Settings.DefaultDetectURLs;
            SnoozeInterval = Settings.DefaultSnoozeInterval;
            ResolveDetectURLsCheckedMenuItem();

            // Set Fonts (not fully implemented yet, must be done for each consolas/courier new field on each form)
            lbInterfaceLocked.Font = Design.SleepFont();
            tbContent.Font = Design.ContentFieldFont();
            FieldsFontSize = 1;
            ListsFontSize = 1;
            tbContent.KeyUp += tbContent_KeyUp;
            Design.EnforceScrollbars = Settings.DefaultEnforceScrollbars;

            // Setup listbox 1
            Design.CurrentListFontWeight = Settings.DefaultListFontWeight;
            listBoxSearchResult.SelectionMode = SelectionMode.One;
            listBoxSearchResult.MultiColumn = false;
            listBoxSearchResult.DisplayMember = "Title";
            listBoxSearchResult.ValueMember = "ID";
            listBoxSearchResult.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxSearchResult.MouseDown += ListBoxSearchResult_MouseDown;
            listBoxSearchResult.Font = Design.ListBoxFont(ListsFontSize);
            listBoxSearchResult.ItemHeight = 18;
            // Setup listbox 2
            listBoxPinned.SelectionMode = SelectionMode.One;
            listBoxPinned.MultiColumn = false;
            listBoxPinned.DisplayMember = "Title";
            listBoxPinned.ValueMember = "ID";
            listBoxPinned.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxPinned.KeyPress += listBoxPinned_KeyPress;
            listBoxPinned.MouseDown += listBoxPinned_MouseDown;
            listBoxPinned.Font = Design.ListBoxFont(ListsFontSize);
            listBoxPinned.ItemHeight = 18;

            // Setup Notification Preview Selector
            SearchBoxResultCycleIndex = 0;

            // Set text color on current zoom selections
            ResolveFieldsZoomIndicator(FieldsFontSize);
            ResolveListsZoomIndicator(ListsFontSize);

            InterfaceLock = false;
            interfaceLockToolStripMenuItem.Text = MessageText.UnLockedMenuItemText();
            LockedForWritingToDisk = false;
            lbInterfaceLocked.Visible = false;
            AccessGranted = false;
            LoggedInOnce = false;
            Entries = new List<Entry>();
            EntriesMirror = new List<Entry>();
            PoolHasChanges = false;
            SettingsHasChanges = false;
            InitializeStickies();
            lbStatus.Text = MessageText.DefaultStatus(GetAdditionalStatusText());
            tbContent.WordWrap = true;
            wordWrapToolStripMenuItem.Checked = tbContent.WordWrap;
            tbContent.DetectUrls = true;
            ReturnToEditorAfterSleep = false;
            ListsActUponMouseMovement = true;
            lbMaintenance.Visible = false;

            // Set default menuitem text
            openToolStripMenuItem.Text = "&Open Pool Or Backup";
            readOnlyToolStripMenuItem.Text = "Read-Only";

            // Set button background images
            btnPinnedUp.Text = "";
            btnPinnedUp.BackgroundImage = Properties.Resources.up;
            btnPinnedUp.BackgroundImageLayout = ImageLayout.Center;
            btnPinnedDown.Text = "";
            btnPinnedDown.BackgroundImage = Properties.Resources.down;
            btnPinnedDown.BackgroundImageLayout = ImageLayout.Center;
            btnPinnedTop.Text = "";
            btnPinnedTop.BackgroundImage = Properties.Resources.top;
            btnPinnedTop.BackgroundImageLayout = ImageLayout.Center;
            btnPinnedBottom.Text = "";
            btnPinnedBottom.BackgroundImage = Properties.Resources.bottom;
            btnPinnedBottom.BackgroundImageLayout = ImageLayout.Center;
            btnBack.Text = "";
            btnBack.BackgroundImage = Properties.Resources.left;
            btnBack.BackgroundImageLayout = ImageLayout.Center;
            btnForward.Text = "";
            btnForward.BackgroundImage = Properties.Resources.right;
            btnForward.BackgroundImageLayout = ImageLayout.Center;
            btnClear.Text = "";
            btnClear.BackgroundImage = Properties.Resources.clear;
            btnClear.BackgroundImageLayout = ImageLayout.Center;
            HoldEntryA = false;
            HoldEntryAID = "";
            HoldEntryB = false;
            HoldEntryBID = "";
            btnHoldA.Text = "Hold";
            btnHoldB.Text = "Hold";
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;

            btnTogglePinned.BackgroundImage = Properties.Resources.pinlist;
            btnTogglePinned.BackgroundImageLayout = ImageLayout.Center;
            btnTogglePinned.Text = "";
            btnToggleMemo.BackgroundImage = Properties.Resources.memo;
            btnToggleMemo.BackgroundImageLayout = ImageLayout.Center;
            btnToggleMemo.Text = "";
            btnTogglePinAndMemo.BackgroundImage = Properties.Resources.sleep;
            btnTogglePinAndMemo.BackgroundImageLayout = ImageLayout.Center;
            btnTogglePinAndMemo.Text = "";
            btnSleep.BackgroundImage = Properties.Resources.sleep;
            btnSleep.BackgroundImageLayout = ImageLayout.Center;
            btnSleep.Text = "";

            // Setup custom checkbox buttons
            cbPinned.CheckedText = ""; // No text
            cbPinned.UnCheckedText = ""; // No text
            cbPinned.CheckedBackgroundImage = Properties.Resources.pinned;
            cbPinned.UnCheckedBackgroundImage = Properties.Resources.unpinned;
            cbPinned.BackgroundImageLayout = ImageLayout.Center;
            cbPinned.Checked = false;
            cbProtect.CheckedText = ""; // No text
            cbProtect.UnCheckedText = ""; // No text
            cbProtect.CheckedBackgroundImage = Properties.Resources.locked;
            cbProtect.UnCheckedBackgroundImage = Properties.Resources.unlocked;
            cbProtect.BackgroundImageLayout = ImageLayout.Center;
            cbProtect.Checked = false;
            cbBookmark.CheckedText = ""; // No text
            cbBookmark.UnCheckedText = ""; // No text
            cbBookmark.CheckedBackgroundImage = Properties.Resources.bookmarkhighlight;
            cbBookmark.UnCheckedBackgroundImage = Properties.Resources.bookmark;
            cbBookmark.BackgroundImageLayout = ImageLayout.Center;
            cbBookmark.Checked = false;


            // Typewriter "button" (it's a panel)

            // Sticky "button" (it's a panel)
            pnlSpawnSticky.BackgroundImage = Properties.Resources.sticky1;
            pnlSpawnSticky.BackgroundImageLayout = ImageLayout.Center;
            pnlSpawnSticky.BringToFront(); // (in front of top menu)
            pnlShowAllStickies.BackgroundImage = Properties.Resources.sticky1;
            pnlShowAllStickies.BackgroundImageLayout = ImageLayout.Center;
            pnlShowAllStickies.BringToFront(); // (in front of top menu)
            pnlShowAllStickies.Visible = false; // Hide from the start
            pnlShowAllStickies.MouseEnter += PnlShowAllStickies_MouseEnter;

            ShowPinAndMemoAnimation = false;
            ShowPinnedAnimationOnEmptyPinnedList = true;
            ResetContentSearchVariables();
            RefreshDummyText(); // Prepare dummy text
            RefreshQuickGuide(); // Prepare quickguide string
            CreateDummyEntry(1); // Must be set here
            IsFullScreen = false;
            IsMaximized = false;
            // Set window default size values
            WindowDefaultSize = new Size(WindowsDefaultWidth, WindowsDefaultHeight);
            this.MinimumSize = new Size(WindowsMinimumWidth, WindowsMinimumHeight);
            CurrentWindowWidth = this.Size.Width;
            CurrentWindowHeight = this.Size.Height;
            AutoHidePinnedAndMemo = false;
            PinnedAndMemoAreHidden = false;
            MemoIsVisible = false;
            PinnedIsVisible = false;
            SetSleepInterval(3, false); // Default
            SetColorTheme(false);
            SetLayoutMode(Settings.DefaultLayoutMode); // Default

            // Extra activity Events (apart from those set through the designer)
            // to prevent from auto saving while working
            // and to enable mousewheel scrolling on the list boxes
            tbContent.VScroll += TbContent_VScroll;
            tbContent.MouseWheel += TbContent_MouseWheel;
            tbContent.MouseClick += TbContent_MouseClick;
            listBoxPinned.MouseMove += ListBoxPinned_MouseMove; // Set focus to the listbox on mouse move, to enable mouse wheel scrolling
            listBoxPinned.MouseClick += ListBoxPinned_MouseClick;
            listBoxPinned.KeyDown += ListBoxPinned_KeyDown; // Handle copy events if focus is on pinned list but text in content is selected for copy
            listBoxSearchResult.MouseMove += ListBoxSearchResult_MouseMove; // Set focus to the listbox on mouse move, to enable mouse wheel scrolling
            lbHomeScreen.MouseDown += LbHomeScreen_MouseDown;

            // Handle navigation by mouse buttons
            tbContent.MouseDown += TbContent_MouseDown;
            tbTitle.MouseDown += TbContent_MouseDown;
            tbSearchInContent.MouseDown += TbContent_MouseDown;
            tbSearch.MouseDown += TbContent_MouseDown;
            listBoxPinned.MouseDown += TbContent_MouseDown;
            listBoxSearchResult.MouseDown += TbContent_MouseDown;

            // tbTitle TAB
            TitleTabToContent = Settings.DefaultTitleTabToContent;
            tbTitle.PreviewKeyDown += TbTitle_PreviewKeyDown;
            tbTitle.GotFocus += TbTitle_GotFocus;
            tbContent.PreviewKeyDown += TbContent_PreviewKeyDown;
            ResolveTitleTabToContent();

            // Insert new lines
            InsertLines = Settings.DefaultInsertNewLines;
            ResolveInsertNewLines();

            // Detect URLs
            tbContent.LinkClicked += TbContent_LinkClicked;
            // Accept Custom Tabs
            tbContent.AcceptsTab = true;
            tbMemoField.AcceptsTab = true;

            // Screen Blocker
            // Click to keep awake
            tbSearch.MouseClick += KeepAwake_Click;
            tbSearchInContent.MouseClick += KeepAwake_Click;
            tbTitle.MouseClick += KeepAwake_Click;


            // New pinned by click
            NewPinnedByClick = Settings.DefaultNewPinnedByClick;
            ResolveNewPinnedByClickMenuItem();
            // Hide pinned by click
            HidePinnedByClick = Settings.DefaultHidePinnedByClick;
            ResolveHidePinnedByClickMenuItem();
            // Title priority search
            TitlePrioritySearch = Settings.DefaultTitlePrioritySearch;
            ResolveTitlePrioritySearchMenuItem();
            // Live search
            LiveSearch = Settings.DefaultLiveSearch;
            ResolveLiveSearchMenuItem();
            // Live search
            InitializeLiveSearch();

            // Key preview (used for keyboard shortcuts)
            this.KeyPreview = true;
            #endregion
        }

        // Remove default title on click
        private void TbTitle_GotFocus(object sender, EventArgs e)
        {
            // If title contains "New Entry", delete it on click
            if (tbTitle.Text == NewEntryCaption)
            {
                try
                {
                    tbTitle.Text = "";
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        // Used to postpone screen blocker and sleep
        private void KeepAwake_Click(object sender, EventArgs e)
        {
            KeepAwake();
        }


        // Navigate back and forward using the mouse (side back/forward buttons)
        private void LbHomeScreen_MouseDown(object sender, MouseEventArgs e)
        {
            // Mousebutton Back
            if (e.Button == MouseButtons.XButton1)
            {
                NavigateBack();
            }// Mousebutton Forward
            else if (e.Button == MouseButtons.XButton2)
            {
                NavigateForward();
            }
        }

        private string GetFormTitle()
        {
            if (IO.UsingCustomFilePath())
            {
                return FormTitle + TextTransform.GetBlankSpace(3) + IO.GetCustomFileName();
            }
            else
            {
                return FormTitle;
            }
        }

        // Select content field on TAB
        private void TbTitle_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (TitleTabToContent && e.KeyData == Keys.Tab)
            {
                selectContentToolStripMenuItem_Click(this, null);
                e.IsInputKey = true;
            }

        }
        // Select title field when pressing Shift+TAB
        private void TbContent_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (TitleTabToContent && e.KeyData == (Keys.Shift | Keys.Tab)) // Multiple
            {
                tbContent.IgnoreNextTab();
                selectTitleToolStripMenuItem_Click(this, null);
                e.IsInputKey = true;
            }
        }

        // Resolve menu item
        private void ResolveTitleTabToContent()
        {
            titleTabToContentToolStripMenuItem.Checked = TitleTabToContent;
        }

        // Change Title tab to content
        private void ToggleTitleTabToContent()
        {
            TitleTabToContent = !TitleTabToContent;
            SettingsHasChanges = true;
            ResolveTitleTabToContent();
        }

        // Insert new lines
        private void ResolveInsertNewLines()
        {
            autoAddBlankLinesToolStripMenuItem.Checked = InsertLines;
        }

        private void ToggleInsertNewLines()
        {
            InsertLines = !InsertLines;
            SettingsHasChanges = true;
            ResolveInsertNewLines();
        }


        // Set tooltip
        private void PnlShowAllStickies_MouseEnter(object sender, EventArgs e)
        {
            ResolveBtnShowToolTip();
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

        // Set default divider values
        private void ResetParagraphDividers()
        {
            ParagraphDividers.InitializePresetList();
            DividerAString = ParagraphDividers.GetPresetValue(0);
            DividerBString = ParagraphDividers.GetPresetValue(1);
            DividerCString = ParagraphDividers.GetPresetValue(2);
            DividerDString = ParagraphDividers.GetPresetValue(3);
            ParagraphDividers.DividerAString = DividerAString;
            ParagraphDividers.DividerBString = DividerBString;
            ParagraphDividers.DividerCString = DividerCString;
            ParagraphDividers.DividerDString = DividerDString;
        }

        // Browse when a URL is clicked
        private void TbContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ShellCommands.BrowseInternet(e.LinkText, SelectedInternetBrowser);
        }

        // Navigate back and forward using the mouse (side back/forward buttons)
        private void TbContent_MouseDown(object sender, MouseEventArgs e)
        {
            // Mousebutton Back
            if (e.Button == MouseButtons.XButton1)
            {
                NavigateBack();
            }// Mousebutton Forward
            else if (e.Button == MouseButtons.XButton2)
            {
                NavigateForward();
            }
        }

        private void SetupHelpPictureBox()
        {
        }

        #region COLOR THEME
        private void SetColorTheme(bool invokeStickyCycle)
        {
            // Index 0 Forms background
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            // Index 0 Notification Selector background
            SetupHelpPictureBox();

            // Setup spawn typewriter "button" (it's a panel)

            // Setup spawn Sticky "button" (it's a panel)
            pnlSpawnSticky.BackColor = AhoyColors.CurrentColorTheme()[7];
            pnlShowAllStickies.BackColor = AhoyColors.CurrentColorTheme()[7];

            // Loading panel
            pnlLoading.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbLoading.BackColor = pnlLoading.BackColor;
            lbLoading.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Read-only label (highlight and menu backcolor)
            lbReadOnlyIndicator.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbReadOnlyIndicator.BackColor = AhoyColors.CurrentColorTheme()[7];

            // Index 1 TextBox background
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbSearch.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbSearchInContent.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbTitle.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContent.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbMemoField.BackColor = AhoyColors.CurrentColorTheme()[29];
            panel1.BackColor = AhoyColors.CurrentColorTheme()[29];
            panel2.BackColor = AhoyColors.CurrentColorTheme()[29];
            panel3.BackColor = AhoyColors.CurrentColorTheme()[29];

            listBoxSearchResult.BackColor = AhoyColors.CurrentColorTheme()[1];
            listBoxPinned.BackColor = AhoyColors.CurrentColorTheme()[1];


            // Index 2 Label text color
            lbSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPinned.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbContent.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbInterfaceLocked.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbMaintenance.BackColor = AhoyColors.CurrentColorTheme()[6];
            lbMaintenance.ForeColor = AhoyColors.CurrentColorTheme()[1];

            // Index 3 TextBox text color
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbTitle.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbContent.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbMemoField.ForeColor = AhoyColors.CurrentColorTheme()[30];

            // Index 4 Listbox Pinned text color
            listBoxPinned.ForeColor = AhoyColors.CurrentColorTheme()[4];
            ListBoxPinnedForeColor = new SolidBrush(AhoyColors.CurrentColorTheme()[4]);

            // Index 5 Listboxes selected text color
            ListBoxSearchResultSelectedForeColor = new SolidBrush(AhoyColors.CurrentColorTheme()[5]);
            ListBoxPinnedSelectedForeColor = new SolidBrush(AhoyColors.CurrentColorTheme()[5]);

            // Index 6 Highlight color (Listbox selections, error message and save button highlight etc.)
            tbSearch.ForeColor = AhoyColors.CurrentColorTheme()[6];
            tbSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[6];
            listBoxSearchResult.ForeColor = AhoyColors.CurrentColorTheme()[6];
            ListBoxSearchResultForeColor = new SolidBrush(AhoyColors.CurrentColorTheme()[6]);
            ListBoxPinnedHighlightColor = AhoyColors.CurrentColorTheme()[6];

            // Status 2 label
            lbStatus2.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Index 7 Button background
            btnBack.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnForward.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPinnedUp.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPinnedDown.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPinnedTop.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPinnedBottom.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSearch.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClear.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHistory.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSearchInContent.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnNew.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDelete.BackColor = AhoyColors.CurrentColorTheme()[7];
            cbProtect.BackColor = AhoyColors.CurrentColorTheme()[7];
            cbPinned.BackColor = AhoyColors.CurrentColorTheme()[7];
            cbBookmark.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHoldA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnHoldB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSave.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSleep.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTogglePinAndMemo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleMemo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnTogglePinned.BackColor = AhoyColors.CurrentColorTheme()[7];
            menuStrip1.BackColor = AhoyColors.CurrentColorTheme()[7]; // Menu top background
            statusStrip1.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnExport.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClearExportMarks.BackColor = AhoyColors.CurrentColorTheme()[7];

            // Index 8 Button text color
            btnBack.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnForward.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPinnedUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPinnedDown.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPinnedTop.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPinnedBottom.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSearch.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClear.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHistory.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnNew.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDelete.ForeColor = AhoyColors.CurrentColorTheme()[8];
            cbProtect.ForeColor = AhoyColors.CurrentColorTheme()[8];
            cbPinned.ForeColor = AhoyColors.CurrentColorTheme()[8];
            cbBookmark.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSave.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSleep.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTogglePinAndMemo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnToggleMemo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnTogglePinned.ForeColor = AhoyColors.CurrentColorTheme()[8];
            lbStatus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnExport.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClearExportMarks.ForeColor = AhoyColors.CurrentColorTheme()[8];


            // Index 9 Button border
            btnBack.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnForward.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPinnedUp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPinnedDown.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPinnedTop.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPinnedBottom.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSearch.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClear.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHistory.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSearchInContent.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnNew.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDelete.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            cbProtect.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            cbPinned.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            cbBookmark.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHoldA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnHoldB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSave.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSleep.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTogglePinAndMemo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleMemo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnTogglePinned.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnExport.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClearExportMarks.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            // Index 10 Button mouse over
            btnBack.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnForward.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPinnedUp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPinnedDown.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPinnedTop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPinnedBottom.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSearch.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClear.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHistory.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSearchInContent.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnNew.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDelete.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            cbProtect.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            cbPinned.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            cbBookmark.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHoldA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnHoldB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSave.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSleep.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTogglePinAndMemo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleMemo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnTogglePinned.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnExport.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClearExportMarks.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            // Index 11 Button mouse down
            btnBack.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnForward.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnPinnedUp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnPinnedDown.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnPinnedTop.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnPinnedBottom.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSearch.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClear.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHistory.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSearchInContent.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnNew.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDelete.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            cbProtect.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            cbPinned.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            cbBookmark.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHoldA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnHoldB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSave.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSleep.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTogglePinAndMemo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnToggleMemo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnTogglePinned.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnExport.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClearExportMarks.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Preset color tags (colors are resolved in the AhoyMenuStrip class)
            color1ToolStripMenuItem1.Tag = 0;
            color2ToolStripMenuItem1.Tag = 1;
            color3ToolStripMenuItem1.Tag = 2;
            color4ToolStripMenuItem1.Tag = 3;
            color5ToolStripMenuItem1.Tag = 4;
            color6ToolStripMenuItem1.Tag = 5;
            color7ToolStripMenuItem1.Tag = 6;
            color8ToolStripMenuItem1.Tag = 7;
            // Custom color tags (colors are resolved in the AhoyMenuStrip class)
            example1ToolStripMenuItem.Tag = 10;
            example2ToolStripMenuItem.Tag = 11;
            example3ToolStripMenuItem.Tag = 12;
            example4ToolStripMenuItem.Tag = 13;
            example5ToolStripMenuItem.Tag = 14;
            example6ToolStripMenuItem.Tag = 15;
            example7ToolStripMenuItem.Tag = 16;
            example8ToolStripMenuItem.Tag = 17;
            example9ToolStripMenuItem.Tag = 18;
            example10ToolStripMenuItem.Tag = 19;
            example11ToolStripMenuItem.Tag = 20;
            example12ToolStripMenuItem.Tag = 21;
            sample13ToolStripMenuItem.Tag = 22;
            sample14ToolStripMenuItem.Tag = 23;
            sample15ToolStripMenuItem.Tag = 24;
            sample16ToolStripMenuItem.Tag = 25;
            sample17ToolStripMenuItem.Tag = 26;
            sample18ToolStripMenuItem.Tag = 27;
            sample19ToolStripMenuItem.Tag = 28;
            sample20ToolStripMenuItem.Tag = 29;


            ResolveFieldsZoomIndicator(FieldsFontSize);
            ResolveListsZoomIndicator(FieldsFontSize);
            // Update memo and pinned visuals with changes
            UpdatePinAndMemoVisuals();
            listBoxPinned.Refresh(); // Make shure the list is refreshed to show any changes from the style picker at once.

            // Update menu
            menuStrip1.Refresh();

            // Update utility editor
            SetButtonBitmaps();

            // Update Log Monitor
            ResolveLogMonitorThemeAndFont();

            // Set Backdrop
            if (IsTextureEnabled())
            {
                ResolveTexture();
            }

            // Update stickies
            if (invokeStickyCycle)
            {
                ColorThemeChanged();
            }

            // Update Micro Calculator

            // Resolve hold buttons
            ResolveHoldButtons();

            // Resolve Title field and panel
            RefreshTitleColor();

            // Dummy text label
            RefreshHomeScreenLabel();

            // Plus/Minus button
            SetExpanderColorTheme();
        }

        private void SetButtonBitmaps()
        {
            btnPinnedUp.BackgroundImage = ButtonBitmaps.Up();
            btnPinnedDown.BackgroundImage = ButtonBitmaps.Down();
            btnPinnedTop.BackgroundImage = ButtonBitmaps.Top();
            btnPinnedBottom.BackgroundImage = ButtonBitmaps.Bottom();
            btnBack.BackgroundImage = ButtonBitmaps.Left();
            btnForward.BackgroundImage = ButtonBitmaps.Right();
            btnClear.BackgroundImage = ButtonBitmaps.Clear(false);
            btnTogglePinned.BackgroundImage = ButtonBitmaps.Pinlist();
            btnToggleMemo.BackgroundImage = ButtonBitmaps.Memo();
            btnTogglePinAndMemo.BackgroundImage = ButtonBitmaps.Sleep();
            btnSleep.BackgroundImage = ButtonBitmaps.Sleep();
            cbPinned.CheckedBackgroundImage = ButtonBitmaps.Pinned();
            cbPinned.UnCheckedBackgroundImage = ButtonBitmaps.Unpinned();
            cbProtect.CheckedBackgroundImage = ButtonBitmaps.Locked();
            cbProtect.UnCheckedBackgroundImage = ButtonBitmaps.Unlocked();
            cbBookmark.CheckedBackgroundImage = ButtonBitmaps.BookmarkSet();
            cbBookmark.UnCheckedBackgroundImage = ButtonBitmaps.NoBookmark();
            pnlSpawnSticky.BackgroundImage = ButtonBitmaps.Sticky(false);
            pnlShowAllStickies.BackgroundImage = ButtonBitmaps.ShiningEye(false);
            btnClearExportMarks.BackgroundImage = ButtonBitmaps.Clear(false);

            // Update buttons
            ResolveAnimationButton();
            ResolveBookmarkButton();
            ResolvePinnedButton();
            ResolveMemoButton();
            ResolveNavigationButtons();
            ResolvePinnedNavigationButtons();
            ResolvePinButton();
            ResolveWriteProtectedButton();
        }





        private bool NoEntrySelected()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        private string GetWeekdayFormatted(DateTime notificationDateTime)
        {
            if (notificationDateTime != null)
            {
                // If date is today
                if (notificationDateTime.Date == DateTime.Now.Date)
                {
                    return "*** TODAY ***";
                }
                else if (notificationDateTime.Date == DateTime.Now.Date.AddDays(1))
                {
                    return "Tomorrow";
                }
                else if ((notificationDateTime - DateTime.Now).Days <= 7)
                {
                    return notificationDateTime.ToString("dddd", new CultureInfo("en-GB"));
                }
                else
                {
                    return "in " + (notificationDateTime - DateTime.Now).Days.ToString() + " days";
                }
            }
            else
            {
                return "N/A";
            }
        }




        private string GetCurrentEntryTitle()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                return CurrentEntry.Title.Trim();
            }
            else
            {
                return "";
            }
        }








        private void ResolvePinButton()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (CurrentEntry.Pin)
                {
                    cbPinned.CheckedBackgroundImage = ButtonBitmaps.Pinned();
                }
                else
                {
                    cbPinned.UnCheckedBackgroundImage = ButtonBitmaps.Unpinned();
                }
            }
            // Refresh visuals
            cbPinned.ResolveDisplay();
        }

        private void ResolveWriteProtectedButton()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (CurrentEntry.Protected)
                {
                    cbProtect.CheckedBackgroundImage = ButtonBitmaps.Locked();
                }
                else
                {
                    cbProtect.UnCheckedBackgroundImage = ButtonBitmaps.Unlocked();
                }
            }
            // Refresh visuals
            cbProtect.ResolveDisplay();
        }



        private void SetNoTexture(bool saveChanges)
        {
            SelectedTexture = Settings.DefaultTexture;
            ResolveTexture();

            if (saveChanges)
            {
                SettingsHasChanges = true;
            }
        }

        private void ResolveTexture()
        {
            // Make split container and panlels transparent
            splitContainer1.BackColor = Color.Transparent;
            splitContainer1.Panel1.BackColor = Color.Transparent;
            splitContainer1.Panel2.BackColor = Color.Transparent;

            // Set form background image
            this.BackgroundImageLayout = ImageLayout.Tile;
            this.BackgroundImage = GetTexture(SelectedTexture);

            // Set Notification Selector panel

            // Set labels to transparent
            lbInterfaceLocked.BackColor = Color.Transparent;
            lbSearch.BackColor = Color.Transparent;
            lbContent.BackColor = Color.Transparent;
            lbPinned.BackColor = Color.Transparent;
            lbTitle.BackColor = Color.Transparent;
            lbContentSearch.BackColor = Color.Transparent;
        }

        internal static bool IsTextureEnabled()
        {
            if (SelectedTexture > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static Bitmap GetTexture(int number)
        {
            switch (number)
            {
                case 1:
                    return Properties.Resources.backdrop1;
                case 2:
                    return Properties.Resources.backdrop2;
                case 3:
                    return Properties.Resources.backdrop4;
                case 4:
                    return Properties.Resources.backdrop3;
                case 5:
                    return Properties.Resources.backdrop22;
                case 6:
                    return Properties.Resources.backdrop29;
                case 7:
                    return Properties.Resources.backdrop24;
                case 8:
                    return Properties.Resources.backdrop25;
                case 9:
                    return Properties.Resources.backdrop26;
                case 10:
                    return Properties.Resources.backdrop27;
                case 11:
                    return Properties.Resources.backdrop23;
                case 12:
                    return Properties.Resources.backdrop28;
                case 13:
                    return Properties.Resources.backdrop30;
                case 14:
                    return Properties.Resources.backdrop5;
                case 15:
                    return Properties.Resources.backdrop6;
                case 16:
                    return Properties.Resources.backdrop7;
                case 17:
                    return Properties.Resources.backdrop8;
                case 18:
                    return Properties.Resources.backdrop9;
                case 19:
                    return Properties.Resources.backdrop10;
                case 20:
                    return Properties.Resources.backdrop11;
                case 21:
                    return Properties.Resources.backdrop12;
                case 22:
                    return Properties.Resources.backdrop13;
                case 23:
                    return Properties.Resources.backdrop14;
                case 24:
                    return Properties.Resources.backdrop15;
                case 25:
                    return Properties.Resources.backdrop16;
                case 26:
                    return Properties.Resources.backdrop17;
                case 27:
                    return Properties.Resources.backdrop18;
                case 28:
                    return Properties.Resources.backdrop19;
                case 29:
                    return Properties.Resources.backdrop20;
                case 30:
                    return Properties.Resources.backdrop21;
                default:
                    return null;
            }
        }
        #endregion

        #region FORM EVENTS
        private void Form1_Load(object sender, EventArgs e)
        {
            MaskInterface(false);

            // Set default location for the Utility Editor

            // Load Settings here to fetch open/save file paths
            LoadSettings();
            // Initialize expand/collapse searchresult function
            InitializePlusMinusButton();

            // Load pool from dialog
            if (Splash.StartupCommandActivated && Splash.LoadPoolFile)
            {
                Splash.StartupCommandActivated = false;
                LoadPoolFromFileDialog();

                // Check if loading was successful or load normally
                if (!EntriesExist() && !LoggedInOnce)
                {
                    LoadPool(false, "");
                }
            }
            else if (Splash.StartupCommandActivated && Splash.StartDemo) // Start Demo
            {
                Splash.StartupCommandActivated = false;
                IO.CheckDataPath(); // Ensure folders are created
                ToggleDemo(false);
            }
            else
            {
                // If using custom file path by init
                if (IO.UsingCustomFilePath())
                {
                    LoadPool(true, "");
                }
                else // else load normally
                {
                    LoadPool(false, "");
                }
            }

            // Detect readonly after loading
            if (Splash.StartupCommandActivated) // Set Read-Only
            {
                Splash.StartupCommandActivated = false;
                TogglePoolReadOnly(true, Splash.ReadOnlyMode, false);
            }

            if (LoggedInOnce) // Only continue if login was successfull
            {
                // Check notifications

                // Dont save twice
                SettingsHasChanges = false;

                // Start the global timer after loading and login response
                StartGlobalTimers();

                // Hide pinned list if it contains items
                if (PinnedList != null && PinnedList.Count > 0)
                {
                    ShowPinAndMemoAnimation = true;
                    UpdatePinAndMemoVisuals();
                }
            }

            // Set QuickGuide and dummy text
            RefreshDummyText();
            RefreshQuickGuide();
            // Jolt textbox to fix scrollbars
            tbContent.JoltToFixScrollbars();

            // Focus search
            tbSearch.Focus();
            MaskInterface(true);
        }

        private void TestSessionID()
        {
            // Check and log session changes
            if (!IO.SessionIDMatch())
            {
                Log.LogSessionIDDeviation();
                //AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Session ID discrepancy:\nThe Pool does not match the current Session ID registered on this instance of Ahoy. The Pool might have been changed by another instance of Ahoy.", "Session", false);
                //msg.ShowDialog();
            }
            else
            {
                Log.LogSessionIDMatch();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoggedInOnce) // Prevent saving empty settings/pool on exit
            {
                if (!IsPaused) // Only save on exit if not paused
                {
                    SaveSettings();
                    SaveChangesWithBackup();
                    // Invoke the global tick to force save/backup if changes
                    timerGlobal_Tick(this, null);
                }
            }
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!InterfaceLock)
            {
                // Screen Blocker
                if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Enter)) // Multiple shortcut keypress
                {
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.P)) // Multiple shortcut keypress
                {
                    InsertChecklistTemplate();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X)) // Multiple shortcut keypress
                {
                    generatePasswordToolStripMenuItem_Click(this, null);
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.N)) // Multiple shortcut keypress
                {
                    CreatePinnedEntry();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.B)) // Multiple shortcut keypress
                {
                    InsertBookmarkTemplate();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Y)) // Multiple shortcut keypress
                {
                    PicExpandResult_Click(this, null);
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Up) && MoveByKeyboard) // Move form UP
                {
                    MoveForm("u", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Down) && MoveByKeyboard) // Move form DOWN
                {
                    MoveForm("d", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Left) && MoveByKeyboard) // Move form LEFT
                {
                    MoveForm("l", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Alt | Keys.Shift | Keys.Right) && MoveByKeyboard) // Move form RIGHT
                {
                    MoveForm("r", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Up) && ResizeByKeyboard) // Risize height -
                {
                    ResizeForm("h-", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Down) && ResizeByKeyboard) // Risize height +
                {
                    ResizeForm("h+", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Left) && ResizeByKeyboard) // Risize width -
                {
                    ResizeForm("w-", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.Shift | Keys.Right) && ResizeByKeyboard) // Risize width +
                {
                    ResizeForm("w+", Control.IsKeyLocked(Keys.CapsLock));
                    e.Handled = true;
                }
            }

            // Pressing ESC or Enter while sleeping will wakeup AHOY!
            if (InterfaceLock)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                {
                    ToggleInterfaceLock();
                    e.Handled = true;
                }
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
            SettingsHasChanges = true;
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
            SettingsHasChanges = true;
        }
        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                IsMaximized = true;
            }
            else
            {
                IsMaximized = false;
            }

            // Register current size
            CurrentWindowWidth = this.Size.Width;
            CurrentWindowHeight = this.Size.Height;

            // Update Utility Editor location
            SetDetachedEditorParentLocation();

            // Register changes
            SettingsHasChanges = true;
        }

        private void UpdateLocationInformation()
        {
            // Save current location
            CurrentLocationX = this.Location.X;
            CurrentLocationY = this.Location.Y;
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            // Update Utility Editor location
            SetDetachedEditorParentLocation();

            // Get current location
            UpdateLocationInformation();

            // Register changes
            SettingsHasChanges = true;
        }

        private void SetDetachedEditorParentLocation()
        {
            try
            {
            }
            catch
            {
                Log.LogWindowStateChangeError();
            }
        }
        #endregion

        #region ENTRIES
        private void ToggleHoldEntryA()
        {
            // Hold an entry in memory
            if (!HoldEntryA && (CurrentEntryNotNullOrDummy()))
            {
                Log.LogEntryStateChange(CurrentEntry.Title, "Held A registered");
                HoldEntryAID = CurrentEntry.ID;
                HoldEntryA = true;
                btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[4];
                UpdateHoldButtons();
                tbContent.Focus();
                PopulatePinListAndReselect();
                // Save changes
                SetPoolHasChanges();
            }
            else if (HoldEntryA && CurrentEntry.ID != HoldEntryAID) // If already in memory but not selected, select it
            {
                // If it does not exist inform the user and deselect it
                // Else select it
                if (EntryExist(HoldEntryAID))
                {
                    SelectWithCancel(HoldEntryAID, true, true);
                    if (EntrySavedByDialog)
                    {
                        CheckListsAndHighlight(HoldEntryAID);
                    }
                }
                else
                {
                    // Remove it from memory and inform the user
                    Log.LogEntryStateChange("", "Held A unregistered");
                    HoldEntryA = false;
                    HoldEntryAID = "";
                    btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    UpdateHoldButtons();
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.AttentionCaption(), false);
                    msg.ShowDialog();
                    PopulatePinListAndReselect();
                    // Save changes
                    SetPoolHasChanges();
                }
            }
            else if (HoldEntryA && CurrentEntry.ID == HoldEntryAID) // If already selected deselect it
            {
                ClearHoldButton(false);
            }
            KeepAwake();
        }

        private void TryToSelectHoldEntryA()
        {
            if (HoldEntryA && CurrentEntry.ID != HoldEntryAID) // If already in memory but not selected, select it
            {
                // If it does not exist inform the user and deselect it
                // Else select it
                if (EntryExist(HoldEntryAID))
                {
                    SelectWithCancel(HoldEntryAID, true, true);
                    if (EntrySavedByDialog)
                    {
                        CheckListsAndHighlight(HoldEntryAID);
                    }
                }
                else
                {
                    // Remove it from memory and inform the user
                    Log.LogEntryStateChange("", "Held A unregistered");
                    HoldEntryA = false;
                    HoldEntryAID = "";
                    btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    UpdateHoldButtons();
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.AttentionCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                // do nothing
            }
        }


        private void TryToSelectHoldEntryB()
        {
            if (HoldEntryB && CurrentEntry.ID != HoldEntryBID) // If already in memory but not selected, select it
            {
                // If it does not exist inform the user and deselect it
                // Else select it
                if (EntryExist(HoldEntryBID))
                {
                    SelectWithCancel(HoldEntryBID, true, true);
                    if (EntrySavedByDialog)
                    {
                        CheckListsAndHighlight(HoldEntryBID);
                    }
                }
                else
                {
                    // Remove it from memory and inform the user
                    Log.LogEntryStateChange("", "Held B unregistered");
                    HoldEntryB = false;
                    HoldEntryBID = "";
                    btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    UpdateHoldButtons();
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.AttentionCaption(), false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                }
            }
            else
            {
                // do nothing
            }
        }

        private void ToggleHoldEntryB()
        {
            // Hold an entry in memory
            if (!HoldEntryB && (CurrentEntryNotNullOrDummy()))
            {
                Log.LogEntryStateChange(CurrentEntry.Title, "Held B registered");
                HoldEntryBID = CurrentEntry.ID;
                HoldEntryB = true;
                btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[4];
                UpdateHoldButtons();
                tbContent.Focus();
                PopulatePinListAndReselect();
                // Save changes
                SetPoolHasChanges();
            }
            else if (HoldEntryB && CurrentEntry.ID != HoldEntryBID) // If already in memory but not selected, select it
            {
                // If it does not exist inform the user and deselect it
                // Else select it
                if (EntryExist(HoldEntryBID))
                {
                    SelectWithCancel(HoldEntryBID, true, true);
                    if (EntrySavedByDialog)
                    {
                        CheckListsAndHighlight(HoldEntryBID);
                    }
                }
                else
                {
                    // Remove it from memory and inform the user
                    Log.LogEntryStateChange("", "Held B unregistered");
                    HoldEntryB = false;
                    HoldEntryBID = "";
                    btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    UpdateHoldButtons();
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.AttentionCaption(), false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                    PopulatePinListAndReselect();
                    // Save changes
                    SetPoolHasChanges();
                }
            }
            else if (HoldEntryB && CurrentEntry.ID == HoldEntryBID) // If already selected deselect it
            {
                ClearHoldButton(true);
            }
            KeepAwake();
        }

        private void ResolveHoldButtons()
        {
            if (HoldEntryAID.Length > 0)
            {
                if (EntryExist(HoldEntryAID))
                {
                    HoldEntryA = true;
                }
            }
            if (HoldEntryBID.Length > 0)
            {
                if (EntryExist(HoldEntryBID))
                {
                    HoldEntryB = true;
                }
            }
            UpdateHoldButtons();
        }

        private void ClearHoldButtons()
        {
            ClearHoldButton(true);
            ClearHoldButton(false);
        }

        private void ClearHoldButton(bool clearButtonB)
        {
            if (clearButtonB)
            {
                Log.LogEntryStateChange("", "Held B unregistered");
                HoldEntryB = false;
                HoldEntryBID = "";
                btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[4];
                UpdateHoldButtons();
                tbContent.Focus();
            }
            else
            {
                Log.LogEntryStateChange("", "Held A unregistered");
                HoldEntryA = false;
                HoldEntryAID = "";
                btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[4];
                UpdateHoldButtons();
                tbContent.Focus();
            }
            PopulatePinListAndReselect();
            // Save changes
            SetPoolHasChanges();
        }


        public static bool EntryExist(string ID)
        {
            if (Entries != null && Entries.Count > 0)
            {
                foreach (Entry e in Entries)
                {
                    if (e.ID == ID)
                    {
                        return true;
                    }
                }
                // Return false if it falls through
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool MirrorEntryExist(string ID)
        {
            if (EntriesMirror.Count > 0)
            {
                foreach (Entry e in EntriesMirror)
                {
                    if (e.ID == ID)
                    {
                        return true;
                    }
                }
                // Return false if it falls through
                return false;
            }
            else
            {
                return false;
            }
        }

        private void FocusToPreviousField()
        {
            // Switch focus back to previous control
            if (CurrentEntryNotNullOrDummy())
            {
                tbContent.Focus();
            }
            else
            {
                tbSearch.Focus();
            }
        }

        private void MovePinnedUp()
        {
            SuspendRelatedToggle();
            if (listBoxPinned.SelectedIndex != -1)
            {
                if (IsCurrentEntryPinnedAndNotRelated())
                {
                    RankPinned(CurrentEntry, true, true);
                }
            }
            else
            {
                FocusToPreviousField();
            }
        }



        private void MovePinnedDown()
        {
            SuspendRelatedToggle();
            if (listBoxPinned.SelectedIndex != -1)
            {
                if (IsCurrentEntryPinnedAndNotRelated())
                {
                    RankPinned(CurrentEntry, false, true);
                }
            }
            else
            {
                FocusToPreviousField();
            }
        }

        private void MovePinnedToTop()
        {
            SuspendRelatedToggle();
            if (listBoxPinned.SelectedIndex != -1)
            {
                if (IsCurrentEntryPinnedAndNotRelated() && RankIndex != 0)
                {
                    do
                    {
                        RankPinned(CurrentEntry, true, false);
                    }
                    while (RankIndex > 0);

                    // Display result
                    SubmitPinnedRanking();
                }
            }
            else
            {
                FocusToPreviousField();
            }
        }

        private void MovePinnedToBottom()
        {
            SuspendRelatedToggle();
            if (listBoxPinned.SelectedIndex != -1)
            {
                // Hvis man nu hiver alle related ud af pinned listen og kun ranker pinned items

                if (IsCurrentEntryPinnedAndNotRelated() && RankIndex != PinnedList.Count - 1)
                {
                    do
                    {
                        RankPinned(CurrentEntry, false, false);
                    }
                    while (RankIndex < PinnedList.Count - 1);

                    // Display result
                    SubmitPinnedRanking();
                }
            }
            else
            {
                FocusToPreviousField();
            }
        }



        // Ensure old formatting is gone from the content field
        private void ResetContentFieldFormatting()
        {
            tbContent.Font = Design.FieldFont(FieldsFontSize);
            tbContent.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbContent.ForeColor = AhoyColors.CurrentColorTheme()[30];
            // .Clear() will reset the clipboard aswell, making copy/paste operations omitted
            tbContent.Clear();
        }

        private void SelectWithCancel(object sender, bool clearSearchSelection, bool clearPinnedSelection)
        {
            // Indicate selection in progress
            EntrySelectionInProgress = true;

            if (AutoSaveEntryChanges) // Make a hidden save, if autosave entry is true
            {
                SaveCurrentEntry(false);
                EntrySavedByDialog = true;
            }
            else
            {
                EntrySavedByDialog = SaveCurrentEntry(true);
            }

            if (EntrySavedByDialog)
            {
                SelectEntry(sender, clearSearchSelection, clearPinnedSelection);
            }
            else
            {
                // Return focus to the  content field if SaveCurrentEntry returns false
                // because the user selected Cancel
                if (CurrentEntry != null)
                {
                    CheckListsAndHighlight(CurrentEntry.ID);
                }
                tbContent.Focus();
            }

            // Indicate selection in progress ended
            EntrySelectionInProgress = false;

        }

        private void SelectWithCancel(string ID, bool clearSearchSelection, bool clearPinnedSelection)
        {
            // Indicate selection in progress
            EntrySelectionInProgress = true;

            if (AutoSaveEntryChanges) // Make a hidden save, if autosave entry is true
            {
                SaveCurrentEntry(false);
                EntrySavedByDialog = true;
            }
            else
            {
                EntrySavedByDialog = SaveCurrentEntry(true);
            }

            if (EntrySavedByDialog)
            {
                SelectEntryByID(ID, clearSearchSelection, clearPinnedSelection);
            }
            else
            {
                // Return focus to the  content field if SaveCurrentEntry returns false
                // because the user selected Cancel
                if (CurrentEntry != null)
                {
                    CheckListsAndHighlight(CurrentEntry.ID);
                }

                // Focus content
                tbContent.Focus();
            }

            // Indicate selection in progress ended
            EntrySelectionInProgress = false;
        }

        // Master-suspend layout
        private void SuspendVisuals()
        {
            this.SuspendLayout();
            tbContent.SuspendLayout();
            listBoxPinned.SuspendLayout();
            listBoxSearchResult.SuspendLayout();
        }

        // Master-resume layout
        private void ResumeVisuals()
        {
            this.ResumeLayout();
            tbContent.ResumeLayout();
            listBoxPinned.ResumeLayout();
            listBoxSearchResult.ResumeLayout();
            SetDefaultStatusText();
        }


        private void SelectEntry(object sender, bool clearSearchSelection, bool clearPinnedSelection)
        {

            // Persist notification preview

            // Return if selected item is null
            if (((ListBox)sender).SelectedItem == null)
            {
                return;

            } // Return if entry ID does not exist
            else if (!EntryExist(((EntryMinimal)((ListBox)sender).SelectedItem).ID))
            {
                return;
            }

            // Reset WPM Counter
            ResetWPMValues();

            // Suspend layout
            SuspendVisuals();

            // Select or deselect search list
            if (clearSearchSelection)
            {
                listBoxSearchResult.ClearSelected();
            }
            // Select or deselect pinned list
            if (clearPinnedSelection)
            {
                listBoxPinned.ClearSelected();
                // Update with changes
                PopulatePinList();
            }

            // Reset content search variables
            ResetContentSearchVariables();
            KeepAwake();

            // Get the selected object and find the corrosponding entry by ID to avoid
            CurrentEntry = GetEntryByID(((EntryMinimal)((ListBox)sender).SelectedItem).ID);

            if (CurrentEntry != null)
            {
                // Search
                // Clear any search results
                if (tbSearch.Text.Length > 0)
                {
                    tbSearch.Text = "";
                }

                Log.LogSelect(CurrentEntry.Title);
                CurrentEntry.IgnoreTextChanged = true;
                tbTitle.Text = CurrentEntry.Title;
                tbContent.ResetHistory();
                tbContent.SuspendLayout();
                tbContent.SetText(CurrentEntry.Content, false);
                tbContent.ResumeLayout();
                // Hide homescreen label
                ResolveHomeScreenLabel("");
                cbPinned.Checked = CurrentEntry.Pin;
                cbProtect.Checked = CurrentEntry.Protected;
                // Check for protected state
                if (CurrentEntry.Protected)
                {
                    BlinkButton(cbProtect, "Entry: Protected");
                }
                // Resolve history state
                if (CurrentEntry.TempHistoryState != null && CurrentEntry.TempHistoryState.Count > 0)
                {
                    tbContent.SetEntireHistoryState(CurrentEntry.TempHistoryState);
                }

                // Resolve bookmark button
                if (CurrentEntry.Bookmark > 0)
                {
                    cbBookmark.Checked = true;
                }
                else
                {
                    cbBookmark.Checked = false;
                }
                UpdateMenuItems();

                // Resolve title field colors
                RefreshTitleColor();

                // Resolve Bookmark template and scroll to index
                if (EntryTemplate.HasBookmarkTemplate(CurrentEntry.Content))
                {
                    int index = EntryTemplate.GetBookmarkCaretIndex(CurrentEntry.Content);
                    if (index >= 0)
                    {
                        // Move cursor to last selected position
                        tbContent.Focus();
                        tbContent.Select(index, 0);
                        tbContent.ScrollToCaret();
                    }
                }
                else
                {
                    // Set caret position if available
                    if (CurrentEntry.CaretPosition > 0 && CurrentEntry.CaretPosition <= tbContent.Text.Length)
                    {
                        // Move cursor to last selected position
                        tbContent.Focus();
                        tbContent.Select(CurrentEntry.CaretPosition, 0);
                        tbContent.ScrollToCaret();
                    }
                    else if (CurrentEntry.CaretPosition > tbContent.Text.Length)
                    {
                        // Scroll to end
                        tbContent.Focus();
                        tbContent.Select(tbContent.Text.Length, 0);
                        tbContent.ScrollToCaret();
                    }
                }
                CurrentEntry.IgnoreTextChanged = false;
                AddToNavigator(CurrentEntry);

                // Search
                AutoPerformSearchInContent();
                // Resolve Marked for Export menu
                ResolveExportMenuChecked();
            }

            // Resume visuals
            ResumeVisuals();
            // Update Tab memory
            UpdateTabMemory();

            // TEST
            // CheckRelated();
        }



        // Auto-Search
        private void AutoPerformSearchInContent()
        {
            // Search
            // Perform content search when selected by search result
            if (AutoSearch && EntrySelectedBySearchResult)
            {
                AutoSearchInProgress = true;
                if (!string.IsNullOrEmpty(CurrentEntry.SearchResult))
                {
                    tbSearchInContent.Text = CurrentEntry.SearchResult;
                    tbSearch.Text = CurrentEntry.SearchResult;
                    FindContent(CurrentEntry.SearchResult);
                    tbSearchInContent.Focus();
                    AutoSearchInProgress = false;
                }
                EntrySelectedBySearchResult = false;
            }
            else
            {
                EntrySelectedBySearchResult = false;
                AutoSearchInProgress = false;
            }
        }


        private void SelectEntryByID(string ID, bool clearSearchSelection, bool clearPinnedSelection)
        {
            if (EntryExist(ID))
            {
                // Persist notification preview

                // Reset WPM Counter
                ResetWPMValues();

                // Suspend layout
                SuspendVisuals();

                // Select or deselect search list
                if (clearSearchSelection)
                {
                    listBoxSearchResult.ClearSelected();
                }
                // Select or deselect pinned list
                if (clearPinnedSelection)
                {
                    listBoxPinned.ClearSelected();
                    // Update with changes
                    PopulatePinList();
                }

                // Reset content search variables
                ResetContentSearchVariables();
                KeepAwake();

                // Get the selected object and find the corrosponding entry by ID to avoid
                CurrentEntry = GetEntryByID(ID);

                if (CurrentEntry != null)
                {
                    // Clear any search results
                    if (tbSearch.Text.Length > 0)
                    {
                        tbSearch.Text = "";
                    }

                    Log.LogSelect(CurrentEntry.Title);
                    CurrentEntry.IgnoreTextChanged = true;
                    tbTitle.Text = CurrentEntry.Title;
                    tbContent.ResetHistory();
                    tbContent.SuspendLayout();
                    tbContent.SetText(CurrentEntry.Content, false);
                    tbContent.ResumeLayout();
                    // Hide Home screen
                    ResolveHomeScreenLabel("");
                    cbPinned.Checked = CurrentEntry.Pin;
                    cbProtect.Checked = CurrentEntry.Protected;
                    // Check for protected state
                    if (CurrentEntry.Protected)
                    {
                        BlinkButton(cbProtect, "Entry: Protected");
                    }
                    // Resolve history state
                    if (CurrentEntry.TempHistoryState != null && CurrentEntry.TempHistoryState.Count > 0)
                    {
                        tbContent.SetEntireHistoryState(CurrentEntry.TempHistoryState);
                    }

                    // Resolve bookmark button
                    if (CurrentEntry.Bookmark > 0)
                    {
                        cbBookmark.Checked = true;
                    }
                    else
                    {
                        cbBookmark.Checked = false;
                    }
                    UpdateMenuItems();

                    // Resolve Bookmark template and scroll to index
                    if (EntryTemplate.HasBookmarkTemplate(CurrentEntry.Content))
                    {
                        int index = EntryTemplate.GetBookmarkCaretIndex(CurrentEntry.Content);
                        if (index >= 0)
                        {
                            // Move cursor to last selected position
                            tbContent.Focus();
                            tbContent.Select(index, 0);
                            tbContent.ScrollToCaret();
                        }
                    }
                    else
                    {
                        // Set caret position if available
                        if (CurrentEntry.CaretPosition > 0 && CurrentEntry.CaretPosition <= tbContent.Text.Length)
                        {
                            // Move cursor to last selected position
                            tbContent.Focus();
                            tbContent.Select(CurrentEntry.CaretPosition, 0);
                            tbContent.ScrollToCaret();
                        }
                        else if (CurrentEntry.CaretPosition > tbContent.Text.Length)
                        {
                            // Scroll to end
                            tbContent.Focus();
                            tbContent.Select(tbContent.Text.Length, 0);
                            tbContent.ScrollToCaret();
                        }
                    }
                    CurrentEntry.IgnoreTextChanged = false;
                    AddToNavigator(CurrentEntry);
                    // Resolve Marked for Export menu
                    ResolveExportMenuChecked();
                }

                // Refresh title field colors
                RefreshTitleColor();
                // Resume visuals
                ResumeVisuals();
                // Update Tab memory
                UpdateTabMemory();
            }
        }

        private void InitializeHomeScreenLabel()
        {
            if (lbHomeScreen == null)
            {
                lbHomeScreen = new Label();
                lbHomeScreen.SetBounds(tbContent.Location.X, tbContent.Location.Y, tbContent.Width, tbContent.Height);
                lbHomeScreen.Name = "lbDummyOverlay";
                lbHomeScreen.Font = Design.ContentFieldFont();
                lbHomeScreen.TextAlign = ContentAlignment.TopLeft;
                lbHomeScreen.AutoSize = false;
                lbHomeScreen.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
                this.splitContainer1.Panel2.Controls.Add(lbHomeScreen);
                lbHomeScreen.Cursor = Cursors.Default;
                lbHomeScreen.Click += LbHomeScreen_Click;
                lbHomeScreen.SendToBack();
                this.toolTip1.SetToolTip(lbHomeScreen, "");
                lbHomeScreen.Visible = false;
            }
        }

        private Label lbHomeScreen;
        private void RefreshHomeScreenLabel()
        {
            if (lbHomeScreen != null)
            {
                lbHomeScreen.BackColor = AhoyColors.CurrentColorTheme()[29];
                lbHomeScreen.ForeColor = AhoyColors.CurrentColorTheme()[30];
                lbHomeScreen.Font = Design.ContentFieldFont();
            }
        }

        private void LbHomeScreen_Click(object sender, EventArgs e)
        {
            if (HidePinnedByClick)
            {
                ShowAnimation();
            }
        }

        private void HideHomeScreenLabel()
        {
            RefreshHomeScreenLabel();
            lbHomeScreen.Text = "";
            lbHomeScreen.Visible = false;
            lbHomeScreen.SendToBack();
            //Log.LogHomeScreenStatus("Hide"); // Can be omitted
            tbContent.TabStop = true;
        }

        private void ResolveHomeScreenLabel(string displayText)
        {
            if (CurrentEntryIsDummy())
            {
                RefreshHomeScreenLabel();
                if (!string.IsNullOrEmpty(displayText))
                {
                    lbHomeScreen.Text = displayText;
                    ResetClock();
                }
                else
                {
                    lbHomeScreen.Text = "";
                }

                if (!lbHomeScreen.Visible)
                {
                    lbHomeScreen.Visible = true;
                    lbHomeScreen.BringToFront();
                    //Log.LogHomeScreenStatus("Show"); // Can be omitted
                    ResetClock();
                    tbContent.TabStop = false;
                }
            }
            else
            {
                HideHomeScreenLabel();
            }
        }


        private void CreateDummyEntry(int displayText)
        {
            CurrentEntry = new Entry();
            CurrentEntry.HasChanges = false;
            CurrentEntry.IsNewEntry = false;
            CurrentEntry.WasDeleted = false;
            CurrentEntry.MarkExport = false;
            CurrentEntry.IsDummyEntry = true;

            // Update title field colors
            ResetTitleColorOnCurrentEntry();

            switch (displayText)
            {
                case 1:
                    SetDefaultText();
                    break;
                case 2:
                    SetNoText(); // Was mantra text
                    HideHomeScreenLabel();
                    break;
                case 3:
                    SetLogText();
                    HideHomeScreenLabel();
                    break;
                case 4:
                    SetNoText();
                    HideHomeScreenLabel();
                    break;
                case 5:
                    SetSheellToolsText();
                    HideHomeScreenLabel();
                    break;
                case 6:
                    OutputAllEntryTitles();
                    HideHomeScreenLabel();
                    break;
                default:
                    SetDefaultText();
                    break;
            }
        }

        private void RenewSessionID()
        {
            InputPrompt serviceMsg = new InputPrompt(MessageText.ServiceCaption(), "", "", "Enter service code", 128, false, "OK", false, null, null, true);
            serviceMsg.ShowDialog();
            if (serviceMsg.UserInput.Trim() == Srv)
            {
                AhoyInfoMessageBox msg = new AhoyInfoMessageBox(IO.RenewSessionID(), MessageText.SessionCaption(), false, false, this.Opacity);
                msg.ShowDialog();
            }
        }

        private void ModifyEntry()
        {
            InputPrompt serviceMsg = new InputPrompt(MessageText.ServiceCaption(), "", "", "Enter service code", 128, false, "OK", false, null, null, true);
            serviceMsg.ShowDialog();
            if (serviceMsg.UserInput.Trim() == Srv)
            {
                try
                {
                    // Save backup in memory
                    SaveBackupForUndo();
                    Halt();
                    int tempCount = 0;
                    if (Entries != null && Entries.Count > 0)
                    {
                        for (int i = 0; i < Entries.Count; i++)
                        {
                            if (Entries[i].Created.Contains(".") ||
                                Entries[i].Changed.Contains("."))
                            {
                                tempCount += 1;
                                Entries[i].Created = Entries[i].Created.Replace(".", ":");
                                Entries[i].Changed = Entries[i].Changed.Replace(".", ":");
                            }
                        }
                    }
                    if (tempCount > 0)
                    {
                        SetPoolHasChanges();
                    }
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Entries updated: " + tempCount, MessageText.ModifyCaption(), false);
                    msg.ShowDialog();
                }
                catch
                {
                    // Do nothing
                }
                finally
                {
                    Resume();
                }
            }
        }

        private void DeleteCurrentEntry()
        {
            if (AccessGranted && Entries != null)
            {
                // Save backup in memory
                SaveBackupForUndo();

                // Clear WPM counter
                ResetWPMValues();

                if (CurrentEntry != null && !string.IsNullOrEmpty(CurrentEntry.ID) && !CurrentEntry.WasDeleted)
                {
                    if (CurrentEntry.Protected)
                    {
                        AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EntryWriteProtectedMessage(), MessageText.AttentionCaption(), false);
                        messageBox.TopMost = true;
                        messageBox.ShowDialog();
                        return;
                    }

                    AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.DeleteEntryQuestion(CurrentEntry.Title.Trim()), MessageText.DeleteCaption(), false, true);
                    ahoyAnswerMessageBox.TopMost = true;
                    ahoyAnswerMessageBox.ShowDialog();

                    if (ahoyAnswerMessageBox.Accepted)
                    {
                        AhoyAnswerMessageBox ahoyAnswerMessageBoxConfirm = new AhoyAnswerMessageBox(MessageText.DeleteEntryConfirmation(CurrentEntry.Title.Trim()), MessageText.DeleteCaption(), false, true);
                        ahoyAnswerMessageBoxConfirm.TopMost = true;
                        ahoyAnswerMessageBoxConfirm.ShowDialog();

                        if (ahoyAnswerMessageBoxConfirm.Accepted)
                        {
                            // If current entry is new (and not saved to disk)
                            if (CurrentEntry.IsNewEntry)
                            {
                                CancelEntryCreation();
                            }
                            else
                            {   // Else try to find it in the list of entries
                                for (int i = 0; i < Entries.Count; i++)
                                {
                                    if (Entries[i].ID == CurrentEntry.ID)
                                    {
                                        Entries.RemoveAt(i);
                                        CurrentEntry.WasDeleted = true;
                                    }
                                }
                            }


                            if (CurrentEntry.WasDeleted)
                            {
                                // Log deletion
                                Log.LogDelete(CurrentEntry.Title);

                                // Remove entry from categories
                                Related.UpdateMasterWhenDeletingRelated(CurrentEntry, Entries);

                                // Save Pool with changes
                                SetPoolHasChanges();

                                // Update Navigator
                                RemoveFromNavigator(CurrentEntry);

                                // Reset hold buttons if deleted entry was on hold
                                if (HoldEntryAID == CurrentEntry.ID)
                                {
                                    HoldEntryA = false;
                                    HoldEntryAID = "";
                                    ResetHoldButtonA();
                                }

                                if (HoldEntryBID == CurrentEntry.ID)
                                {
                                    HoldEntryB = false;
                                    HoldEntryBID = "";
                                    ResetHoldButtonB();
                                }

                                ResolveExportButton();
                                ResolveExportMenuChecked();
                                CurrentEntry.IsDummyEntry = true;
                                // Update title field colors
                                ResetTitleColorOnCurrentEntry();
                                SetDefaultText();
                                cbPinned.Checked = false;
                                cbProtect.Checked = false;
                                cbBookmark.Checked = false;
                                listBoxSearchResult.Items.Clear();
                                PopulatePinList();
                                tbSearch.Focus();

                                // Update live search
                                if (LiveSearch && !string.IsNullOrEmpty(tbSearch.Text))
                                {
                                    PerformLiveSearch(true);
                                }
                            }
                            else
                            {
                                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.ErrorMessage(), MessageText.DeleteCaption(), false);
                                messageBox.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.SelectEntryForDeletionMessage(), MessageText.DeleteCaption(), false);
                    messageBox.ShowDialog();
                }

                // Resolve status
            }
        }

        private void CancelEntryCreation()
        {
            if (CurrentEntry != null && CurrentEntry.IsNewEntry)
            {
                CurrentEntry.IsNewEntry = false;
                CurrentEntry.WasDeleted = true;
            }
        }


        private string tempNewEntryTitle;
        private bool tempNewEntryPinned;
        private static bool NewEntryCreationCancelled;
        private const string NewEntryCaption = "New Entry";
        private static string NewEntryOptionalText = "";
        private static bool EnablePinning;
        private void CreateNewEntry(string optionalContentText, bool useOptionalText, string optionalTitleText, bool useOptionalTitle, bool isNotification, bool pin)
        {
            // Enable pinning by default
            EnablePinning = true;

            // Optional title
            if (useOptionalTitle)
            {
                NewEntryOptionalText = optionalTitleText;
            }
            else
            {
                NewEntryOptionalText = "";
            }


            // Enter title text in dialog
            InputPrompt inputForm = new InputPrompt(NewEntryCaption, NewEntryOptionalText, "", "Enter Title", 64, EnablePinning, "OK", false, cbPinned.CheckedBackgroundImage, cbPinned.UnCheckedBackgroundImage, false);
            // Prevent saving while dialog is open
            AttachedEditorOpen = true;
            if (pin)
            {
                inputForm.SetToPinned();
            }
            inputForm.ShowDialog();

            if (!inputForm.ActionCancelled)
            {
                NewEntryCreationCancelled = false;

                // Get the new title text from the prompt
                tempNewEntryTitle = TextTransform.RemoveNewLines(inputForm.UserInput);

                // If pinned from new title dialog
                if (inputForm.PinEntry)
                {
                    tempNewEntryPinned = true;
                }
                else
                {
                    tempNewEntryPinned = false;
                }

                // Fill in title text if none was provided
                if (string.IsNullOrEmpty(tempNewEntryTitle))
                {
                    tempNewEntryTitle = NewEntryCaption;
                }

                // Check if current entry has changes and ask to save
                // before continuing.
                if (CurrentEntry != null && CurrentEntry.HasChanges)
                {
                    if (SaveCurrentEntry(!AutoSaveEntryChanges))
                    {
                        // Reset content search field
                        ResetContentSearchVariables();
                        // Deselect search and pinlist
                        listBoxSearchResult.ClearSelected();
                        listBoxPinned.ClearSelected();
                    }
                    else
                    {
                        // Exit if canceled
                        return;
                    }
                }

                if (AccessGranted)
                {
                    // Create new entry
                    CurrentEntry = new Entry();
                    // Set variables
                    CurrentEntry.IsNewEntry = true;
                    CurrentEntryHasChanges();
                    CurrentEntry.WasDeleted = false;
                    CurrentEntry.IgnoreTextChanged = false;
                    CurrentEntry.IsDummyEntry = false;
                    CurrentEntry.TempHistoryState = new List<HistoryState>();

                    // Create default content
                    CurrentEntry.ID = Guid.NewGuid().ToString();
                    CurrentEntry.Created = IO.GetIsoDateTimeNow();
                    CurrentEntry.Changed = IO.GetIsoDateTimeNow();
                    CurrentEntry.Title = "";
                    CurrentEntry.Content = "";
                    CurrentEntry.Pin = false; // Will be set through the cbPinned box further down
                    CurrentEntry.Bookmark = 0;
                    CurrentEntry.RelatedEntries = string.Empty;
                    CurrentEntry.RelatedView = false;
                    CurrentEntry.RelatedStyle = 0;
                    CurrentEntry.Rank = IO.DefaultRank; // Default rank will put newly pinned entries at the bottom of the list
                    // Update title field colors
                    ResetTitleColorOnCurrentEntry();
                    CurrentEntry.Protected = false;

                    // Clear content afterwards to prevent from saving over the current selected entry
                    CurrentEntry.IgnoreTextChanged = true;
                    tbTitle.Text = tempNewEntryTitle; // Add new title text to the title field
                    CurrentEntry.Title = tbTitle.Text;
                    ResolveHomeScreenLabel("");
                    tbContent.ResetHistory();

                    // Add optional content
                    if (useOptionalText && (optionalContentText != null && optionalContentText.Length > 0))
                    {
                        tbContent.SetText(optionalContentText, true);
                    }
                    else
                    {
                        // Must be set for the eventual initial Paste function to work
                        tbContent.SetText(NewEntryCaption, false);
                        InsertNewLines(tbContent);
                    }

                    CurrentEntry.Content = tbContent.Text;
                    cbPinned.Checked = tempNewEntryPinned;
                    cbProtect.Checked = false;
                    cbBookmark.Checked = false;
                    CurrentEntry.IgnoreTextChanged = false;
                    AddToNavigator(CurrentEntry);
                    Log.LogNewEntry();

                    // Secret save if pinned from New Title dialog
                    if (tempNewEntryPinned)
                    {
                        SaveCurrentEntry(false);
                        PopulatePinList();
                        if (PinnedIsVisible)
                        {
                            ReselectPinned();
                        }
                    }

                    // Focus entry and select text with options
                    tbContent.Focus();
                    if (!InsertLines && !useOptionalText)
                    {
                        tbContent.SelectAll();
                    }
                    else if (InsertLines && !useOptionalText)
                    {
                        if (tbContent.Text.Contains(NewEntryCaption))
                        {
                            tbContent.Select(0, NewEntryCaption.Length);
                        }
                    }

                    // Add new entry to tab memory
                    UpdateTabMemory();
                }

                // Update hold buttons to reflect changes immediately
                UpdateHoldButtons();
                // Resume save activity when dialog closes
                AttachedEditorOpen = false;
            }
            else
            {
                AttachedEditorOpen = false;
                NewEntryCreationCancelled = true;
                tbSearch.Focus();
            }
        }


        private void UnpinCurrentEntry()
        {
            if (AccessGranted && Entries != null)
            {
                // Save backup in memory
                SaveBackupForUndo();
                if (IsCurrentEntryPinnedAndNotRelated())
                {
                    SuspendVisuals();
                    // Unpin
                    CurrentEntry.Pin = false;
                    // Update title field colors
                    //ResetTitleColorOnCurrentEntry(); // Related -color is kept on unpin
                    cbPinned.Checked = false;
                    SubmitChangesToEntriesList();
                    // Log event
                    Log.LogUnpinEntry();
                    KeepAwake();
                    // Update with changes
                    PopulatePinList();
                    ResumeVisuals();
                    // Focus entry and select all
                    tbContent.Focus();
                }
            }
        }

        private void DuplicateCurrentEntry(bool displayDialog)
        {
            if (AccessGranted && Entries != null)
            {
                try
                {
                    // Save backup in memory
                    SaveBackupForUndo();
                    if (CurrentEntryNotNullOrDummy())
                    {
                        AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Duplicate selected Entry?", MessageText.DuplicateEntryCaption(), false, false);
                        if (displayDialog)
                        {
                            msg.ShowDialog();
                        }
                        if (!displayDialog || msg.Accepted)
                        {
                            SuspendVisuals();
                            // Save changes before cloning
                            SaveCurrentEntry(!AutoSaveEntryChanges);
                            // Duplicate Entry
                            Entry newEntry = IO.CloneEntry(CurrentEntry, true);
                            // Supply a new ID
                            newEntry.ID = Guid.NewGuid().ToString();
                            newEntry.Title = newEntry.Title + " (Copy)";
                            // Set timestamps
                            newEntry.Created = IO.GetIsoDateTimeNow();
                            newEntry.Changed = IO.GetIsoDateTimeNow();
                            // Submit to Entries list
                            Entries.Add(newEntry);

                            // Related: If current entry was related, then the duplicated should be added to master
                            if (Related.IsRelated(CurrentEntry, Entries))
                            {
                                // Submit duplicate to same master
                                if (!Related.AddToMaster(CurrentEntry, newEntry, Entries))
                                {
                                    Log.GeneralEntryStatus("Relation failed (duplication)");
                                    AhoySimpleMessageBox msgDuplicate = new AhoySimpleMessageBox("Relation failed", MessageText.DuplicateEntryCaption(), false);
                                    msgDuplicate.ShowDialog();
                                }
                            }

                            // Save pool
                            SetPoolHasChanges(); // Initialise saving the pool
                                                 // Log event
                            Log.LogDuplicateEntry();
                            KeepAwake();


                            // If entry was pinned, select it and move it above the one it was pinned from
                            if (newEntry.Pin)
                            {
                                // Make the new entry the currently selected entry
                                SelectEntryByID(newEntry.ID, true, true);
                                PopulatePinList();
                                // Reselect pinned
                                if (CurrentEntryNotNullOrDummy() && CurrentEntry.Pin) // Related // TEST
                                {
                                    // Show Pinned list if hidden/masked
                                    ShowPinned();
                                    CheckListsAndHighlight(CurrentEntry.ID);
                                }
                                // Move it up above its parent
                                //MovePinnedUp(); // <-- disabled becaused it moved it too far up :)

                                ResumeVisuals();
                            }
                            else
                            {
                                // Else select the new entry and focus title field
                                // Make the new entry the currently selected entry
                                SelectEntryByID(newEntry.ID, true, true);
                                // Update with changes
                                PopulatePinList();
                                ReselectPinned();
                                ResumeVisuals();
                            }

                            if (displayDialog && !newEntry.Pin) // don't show dialog if pinned
                            {
                                AhoySimpleMessageBox msg1 = new AhoySimpleMessageBox("Entry duplicated", MessageText.DuplicateEntryCaption(), false);
                                msg1.ShowDialog();
                            }
                            // Focus entry title
                            tbTitle.Focus();
                        }
                    }
                    else
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToDuplicateMessage(), MessageText.DuplicateEntryCaption(), false);
                        msg.ShowDialog();
                    }
                }
                catch
                {
                    Log.LogDuplicateEntryError();
                }
            }
        }

        // Clear all colorized entries
        private bool tempColoredEntriesExist;
        private void ClearAllPinnedColors()
        {
            if (Entries != null && Entries.Count > 0)
            {
                tempColoredEntriesExist = false;

                // Check if any colored entries exist
                foreach (Entry e in Entries)
                {
                    if (e.PresetColor > 0 || AhoyColors.HasDualHexValue(e.CustomColor))
                    {
                        tempColoredEntriesExist = true;
                    }
                }

                if (tempColoredEntriesExist)
                {
                    AhoyAnswerMessageBox answerMsg = new AhoyAnswerMessageBox(MessageText.ClearColorizedEntriesQuestion(), MessageText.AttentionCaption(), false, false);
                    answerMsg.ShowDialog();

                    if (answerMsg.Accepted)
                    {
                        // Reset colors
                        ResetColors();
                        // Save entry and pool
                        SaveCurrentEntry(!AutoSaveEntryChanges);
                        SavePool(false, false, false, "");
                    }
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptyColoredEntriesSearchMessage(), MessageText.AttentionCaption(), false);
                    msg.ShowDialog();
                }
            }
        }

        internal void ResetColors()
        {
            if (Entries != null && Entries.Count > 0)
            {
                SaveBackupForUndo();
                Log.GeneralEntryStatus("Clear pinned colors:");
                foreach (Entry entry in Entries)
                {
                    if (entry.PresetColor > 0 || AhoyColors.HasDualHexValue(entry.CustomColor))
                    {
                        if (entry.PresetColor > 0)
                        {
                            Log.GeneralEntryStatus("Color (preset): " + entry.PresetColor.ToString());
                        }
                        else
                        {
                            Log.GeneralEntryStatus("Color (custom): " + entry.CustomColor);
                        }
                        entry.PresetColor = 0;
                        entry.CustomColor = "";
                        Log.GeneralEntryStatus("Clear (" + entry.PresetColor.ToString() + ")");
                    }
                }
                PopulatePinList();
                // Resolve title field colors
                RefreshTitleColor();
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Colors reset", MessageText.AttentionCaption(), false);
                msg.ShowDialog();
            }
        }
        #endregion

        #region TEXTBOX AND LISTBOX EVENTS
        private void listBoxSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CancelEntrySelectionForHighlight)
            {
                EntrySelectedBySearchResult = true;
                // Track selected index for notification preview dialog
                if (listBoxSearchResult.SelectedIndex >= 0)
                {
                    SetSearchResultSelectionIndex(listBoxSearchResult.SelectedIndex);
                }

                // Deselect already pinned entries
                SelectWithCancel(sender, false, true);
                CheckRelatedAndPostInSearch();

                // Select in pinned list if pinned
                ReselectPinned();
                // Expand related if any // Related
                //if (Related.HasRelated(CurrentEntry))
                //{ 
                // Perhaps here
                //}

                // Ensure staying in content field
                ListsActUponMouseMovement = false;
            }
        }

        // When clicking an entry in the pinned list
        private void listBoxPinned_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set selected index for the ranking mechanism
            RankIndex = listBoxPinned.SelectedIndex;
            // Update navigation buttons
            ResolvePinnedNavigationButtons();

            if (!CancelEntrySelectionForHighlight)
            {
                // Deselect search list
                SelectWithCancel(sender, true, false);
                // Toggle related view
                if (RankIndex != -1) // Prevent toggle if no index was selected (important)
                {
                    ToggleRelatedView();
                }
                // Ensure staying in content field
                ListsActUponMouseMovement = false;
            }
        }


        // If clipboard is empty add the current string in memory
        private static string tempClipboardString;
        public static void RetrieveClipboardText()
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    tempClipboardString = Clipboard.GetText(TextDataFormat.UnicodeText);
                }
                else
                {
                    tempClipboardString = "";
                }

                if (tempClipboardString.Length == 0 && StoredClipboardString.Length > 0)
                {
                    SetClipboardText(StoredClipboardString);
                }
            }
            catch
            {
                // Do nothing
            }
        }


        // Save text in clipboard to variable
        public static void PersistClipboard()
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    StoredClipboardString = Clipboard.GetText(TextDataFormat.UnicodeText);
                }
            }
            catch
            {
                // Do nothing
            }
        }


        public static void ClearClipboard()
        {
            try
            {
                Clipboard.Clear();
                StoredClipboardString = "";
            }
            catch
            {
                // Do nothing
            }
        }

        private void tbContent_KeyDown(object sender, KeyEventArgs e)
        {
            // Keep from saving if keys are pressed
            KeepAwake();

            // Save any navigation
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
            {
                SaveCaretPosition();
                // Do not suppress here to let the message pass through
            }

            // This multi shortcut statement must be first
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C)) // Multiple shortcut keypress (Copy formatted)
            {
                tbContent.Copy();
                PersistClipboard();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.X)) // Multiple shortcut keypress (Cut formatted)
            {
                tbContent.Cut();
                PersistClipboard();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.V)) // Multiple shortcut keypress (Paste formatted)
            {
                tbContent.Paste();
                PersistClipboard();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.D)) // Multiple shortcut keypress (Duplicate selection)
            {
                DuplicateSelection(tbContent);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
            {
                tbContent.CopyUnformatted();
                PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
            {
                tbContent.CutUnformatted();
                PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                RetrieveClipboardText();
                tbContent.PasteUnformatted(); // Paste unformatted
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (!tbContent.Focused)
                {   // Re-focus the content field and "replay" the key pressed
                    tbContent.Focus();
                    SendKeys.Send("{BACKSPACE}");
                }
                // Do not suppress here to let the message pass through
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (!tbContent.Focused)
                {   // Re-focus the content field and "replay" the key pressed
                    tbContent.Focus();
                    SendKeys.Send("{DELETE}");
                }
                // Do not suppress here to let the message pass through
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                undoToolStripMenuItem1_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                toolStripMenuItem12_Click(this, null);
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.Q)
            {
                ClearFormatting();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                InsertDate();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                try
                {
                    // Prevent bug if casting from an unknown control
                    contextMenuContent.Show(((HistoryTextBox)sender), ((HistoryTextBox)sender).Location);
                }
                catch
                {
                    // Do nothing
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                SelectionToUpper();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                LineDelete();
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                InsertParagraphDividerByShortcut(0, tbContent);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                InsertParagraphDividerByShortcut(1, tbContent);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemPeriod)
            {
                InsertParagraphDividerByShortcut(2, tbContent);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                InsertParagraphDividerByShortcut(3, tbContent);
                e.Handled = true;
            }
            else
            {
                // Submit WPM counter input
                if (ShowWPMCounter)
                {
                    WPMCounter.SubmitInputCount(e);
                }
                KeepAwake();
                ClearDisplays(e);
                SelectSearchResultByShortcut(e);
            }
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

        private void CycleHoldButtonsForward()
        {
            if (HoldButtonSelected == 1)
            {
                TryToSelectHoldEntryB();
            }
            else if (HoldButtonSelected == 2)
            {
                TryToSelectHoldEntryA();
            }
            else
            {
                TryToSelectHoldEntryA();
            }
        }

        private void CycleHoldButtonsBackwards()
        {
            if (HoldButtonSelected == 2)
            {
                TryToSelectHoldEntryA();
            }
            else if (HoldButtonSelected == 1)
            {
                TryToSelectHoldEntryB();
            }
            else
            {
                TryToSelectHoldEntryA();
            }
        }

        // Select all if tabbing into the content (to prepare for deleting the default text)
        private void tbContent_Enter(object sender, EventArgs e)
        {
            if (CurrentEntry.IsNewEntry && !string.IsNullOrEmpty(tbContent.Text))
            {
                tbContent.SelectAll();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            ReturnToEditorAfterSleep = false;
            if (e.KeyCode == Keys.Enter)
            {
                BlockLiveSearch = true;
                btnSearch_Click(this, null);
                // Prevent ding
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X))
            {
                generatePasswordToolStripMenuItem_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                tbSearch.SelectAll();
                // Prevent ding
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                tbSearch.Copy();
                // Prevent ding
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                tbSearch.Cut();
                // Prevent ding
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Tab)
            {
                CycleHoldButtonsForward();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Tab)
            {
                CycleHoldButtonsBackwards();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Go backwards in previous searches
                RecallSearchBack();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Go forwards in previous searches
                RecallSearchForward();
                e.Handled = true;
            }
            else
            {
                ClearDisplays(e);
                SelectSearchResultByShortcut(e);
            }
        }

        private string tempUnformattedText;
        private void tbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            // Control + A: Select All
            if (e.Control && e.KeyCode == Keys.A)
            {
                tbTitle.SelectAll();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                tbTitle.Copy();
                // Convert to unicode text
                tempUnformattedText = Clipboard.GetText(TextDataFormat.UnicodeText);
                SetClipboardText(tempUnformattedText);
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X))
            {
                generatePasswordToolStripMenuItem_Click(this, null);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                tbTitle.Cut();
                // Convert to unicode text
                tempUnformattedText = Clipboard.GetText(TextDataFormat.UnicodeText);
                SetClipboardText(tempUnformattedText);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.T)
            {
                tbTitle.Paste(TimeStamp.GetISOTimestamp());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                ClearDisplays(e);
                SelectSearchResultByShortcut(e);
            }
        }

        private void tbSearchInContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                tbSearchInContent.SelectAll();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X))
            {
                generatePasswordToolStripMenuItem_Click(this, null);
                e.Handled = true;
            }
            else
            {
                SelectSearchResultByShortcut(e);
            }
        }

        private void tbContent_KeyUp(object sender, EventArgs e)
        {
            // React if user changes text, and not when set by listbox selection or PLOP Editor
            if ((CurrentEntry != null && !CurrentEntry.IgnoreTextChanged) && (tbContent.Text != CurrentEntry.Content))
            {
                if (!CurrentEntry.IsDummyEntry)
                {
                    if (!CurrentEntry.Protected)
                    {
                        // Save undo history
                        if (CurrentEntry.TempHistoryState != null && tbContent.GetEntireHistoryState() != null)
                        {
                            CurrentEntry.TempHistoryState.Clear();
                            CurrentEntry.TempHistoryState.AddRange(tbContent.GetEntireHistoryState());
                        }
                    }
                }
            }
        }

        private static bool CurrentEntryNotNullOrDummy()
        {
            if (CurrentEntry != null && !CurrentEntry.IsDummyEntry)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CurrentEntryIsDummy()
        {
            if (CurrentEntry != null && CurrentEntry.IsDummyEntry)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void tbContent_TextChanged(object sender, EventArgs e)
        {
            PerformAutoHidePinned();
            KeepAwake();

            // React if user changes text, and not when set by listbox selection or PLOP Editor
            if ((CurrentEntry != null && !CurrentEntry.IgnoreTextChanged) && (tbContent.Text != CurrentEntry.Content))
            {
                if (!CurrentEntry.IsDummyEntry)
                {
                    if (!CurrentEntry.Protected)
                    {
                        // Submit to PLOP Editor if open and has same ID linked as currently selected

                        // Set has changes to true
                        CurrentEntryHasChanges();
                        ReturnToEditorAfterSleep = true;
                    }
                    else
                    {
                        // Blink lock button
                        BlinkButton(cbProtect, "Entry: Protected warning");

                        CurrentEntry.IgnoreTextChanged = true;
                        tbContent.Text = CurrentEntry.Content;
                        CurrentEntry.IgnoreTextChanged = false;
                        try
                        {
                            // Move cursor to last selected position
                            tbContent.Focus();
                            tbContent.Select(CurrentEntry.CaretPosition, 0);
                            tbContent.ScrollToCaret();
                        }
                        catch
                        {
                            // Do nothing
                        }
                        AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EntryWriteProtectedMessage(), MessageText.WriteProtectionCaption(), false);
                        messageBox.ShowDialog();
                    }
                }
            }

            UpdateWPMValues(); // Show WPM counter
        }

        // General button blinker
        // ATTENTION: Only one button at the time can blink because
        // the timer won't take more than one control when running
        private Timer ButtonBlinkerTimer;
        private Color OriginalColor;
        private Color OriginalTextColor;
        private Button ButtonToBlink;
        private void BlinkButton(Button control, string logText)
        {
            try
            {
                // Initialize timer
                if (ButtonBlinkerTimer == null)
                {
                    ButtonBlinkerTimer = new Timer();
                    ButtonBlinkerTimer.Tick += ButtonBlinkerTimer_Tick;
                    ButtonBlinkerTimer.Interval = 500;
                }

                // Return if already running
                if (ButtonBlinkerTimer.Enabled)
                {
                    return;
                }
                else
                {
                    Log.GeneralLogMessage(logText);
                    // Save current background color
                    ButtonToBlink = control;
                    OriginalColor = ButtonToBlink.BackColor;
                    OriginalTextColor = ButtonToBlink.ForeColor;
                    // Set control highlight color
                    control.ForeColor = AhoyColors.CurrentColorTheme()[5];
                    control.BackColor = AhoyColors.CurrentColorTheme()[6];
                    // Start timer
                    ButtonBlinkerTimer.Enabled = true;
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void ButtonBlinkerTimer_Tick(object sender, EventArgs e)
        {
            ButtonBlinkerTimer.Enabled = false;
            try
            {
                if (OriginalColor != null && ButtonToBlink != null)
                {
                    // Set lock button background color to normal
                    ButtonToBlink.BackColor = OriginalColor;
                    ButtonToBlink.ForeColor = OriginalTextColor;
                }
            }
            catch
            {
                // Do nothing
            }
        }


        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            KeepAwake();

            // React if user changes text, and not when set by listbox selection
            if ((CurrentEntry != null && !CurrentEntry.IgnoreTextChanged) && (tbTitle.Text != CurrentEntry.Title))
            {
                if (!CurrentEntry.IsDummyEntry)
                {
                    if (!CurrentEntry.Protected)
                    {
                        // Submit to PLOP Editor if open and has same ID linked as currently selected

                        // Set has changes to true
                        CurrentEntryHasChanges();
                    }
                    else
                    {
                        CurrentEntry.IgnoreTextChanged = true;
                        tbTitle.Text = CurrentEntry.Title;
                        CurrentEntry.IgnoreTextChanged = false;
                        AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EntryWriteProtectedMessage(), MessageText.WriteProtectionCaption(), false);
                        messageBox.ShowDialog();
                    }
                }
            }
        }

        private void tbSearchInContent_TextChanged(object sender, EventArgs e)
        {
            RestartSearchVariables();
            KeepAwake();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            KeepAwake();
        }

        // Able to clear screen when focus are on pinned list
        void listBoxPinned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!InterfaceLock)
            {
                if ((Keys)e.KeyChar == Keys.Escape)
                {
                    ClearDisplays(true);
                    e.Handled = true;
                }
            }
        }

        // Insure selection from both left and right mouse buttons
        // on the pinned list
        private void listBoxPinned_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (listBoxPinned.Items.Count > 0)
                {
                    listBoxPinned.SelectedIndex = listBoxPinned.IndexFromPoint(e.X, e.Y);
                    // if -1 ask to create new entry (when clicking the pinned liste with no entry selected)
                    if (listBoxPinned.SelectedIndex == -1 && NewPinnedByClick)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            BlinkButton(btnNew, "Entry: New Pinned");
                            CreatePinnedEntry();
                        }
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Left) // Creat new entry pinned if cliking an empty pinned list
                    {
                        if (NewPinnedByClick)
                        {
                            BlinkButton(btnNew, "Entry: New Pinned");
                            CreatePinnedEntry();
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }


        private void newPinnedEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePinnedEntry();
            AddNewEntryToEditingCategory();
        }

        private void newPinnedEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreatePinnedEntry();
            AddNewEntryToEditingCategory();
        }

        private void CreatePinnedEntry()
        {
            CreateNewEntry(string.Empty, false, string.Empty, false, false, true);
        }

        // Insure selection from both left and right mouse buttons
        // on the search list
        private void ListBoxSearchResult_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (listBoxSearchResult.Items.Count > 0)
                {
                    listBoxSearchResult.ContextMenuStrip.Enabled = true; // Enable context menu if list has items
                    listBoxSearchResult.SelectedIndex = listBoxSearchResult.IndexFromPoint(e.X, e.Y);
                    // Add to search history when selecting from the search results
                    if (!string.IsNullOrEmpty(tbSearch.Text) && tbSearch.Text.Length > 1)
                    {
                        AddToSearchHistoryList(tbSearch.Text.Trim());
                    }
                }
                else
                {
                    listBoxSearchResult.ContextMenuStrip.Enabled = false; // Disable context menu if list does not have items
                    btnHistory_Click(this, null);
                    BlinkButton(btnHistory, "Recall: History");
                }
            }
            catch
            {
                // Do nothing
            }
        }
        #endregion

        #region UTILITY
        private void LineDelete()
        {
            if (CurrentEntry != null)
            {
                // Perform line delete
                tbContent.DeleteCurrentLine();
            }
        }

        private void SelectionToSlickFrame(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.TextToSlickFrame(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToEmbossedFrame(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.TextToEmbossedFrame(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToSlickTab(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.TextToSlickTab(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToEmbossedTab(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.TextToEnbossedTab(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToBulletAndLine(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.SelectionToBulletAndLine(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToBullet(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.SelectionToBullet(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToUnderline(bool caps)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = TextTransform.SelectionToUnderline(tbContent.SelectedText, caps);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToUpper()
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = tbContent.SelectedText.ToUpper();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void SelectionToLower()
        {
            if (tbContent.SelectedText.Length > 0)
            {
                tbContent.SelectedText = tbContent.SelectedText.ToLower();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        // Insert Date
        private void InsertDate()
        {
            tbContent.InsertISOTimestamp();
        }

        // Insert Paragraph divider
        private void InsertParagraphDividerByShortcut(int selection, HistoryTextBox tb)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                InsertParagraphDivider(selection, tb);
            }
        }

        private void InsertParagraphDivider(int selection, HistoryTextBox tb)
        {
            switch (selection)
            {
                case 0:
                    tb.InsertParagraphDivider(DividerAString);
                    break;
                case 1:
                    tb.InsertParagraphDivider(DividerBString);
                    break;
                case 2:
                    tb.InsertParagraphDivider(DividerCString);
                    break;
                case 3:
                    tb.InsertParagraphDivider(DividerDString);
                    break;
                default:
                    tb.InsertParagraphDivider(DividerAString);
                    break;
            }
        }



        // Save caret position without submitting content text changes
        private void SaveCaretPosition()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // Save caret position
                if (CurrentEntry.CaretPosition != tbContent.SelectionStart)
                {
                    CurrentEntry.CaretPosition = tbContent.SelectionStart;
                    // Submit just the caret position changes
                    KeepAwake();
                    SubmitCaretPositionChangesToEntriesList();
                }
            }
        }

        string TempUnformattedText { get; set; }
        private void ClearFormatting()
        {
            tbContent.SuspendLayout();
            // Save caret position
            SaveCaretPosition();
            // Save composition
            TempUnformattedText = tbContent.Text;
            // Reset textbox
            tbContent.Clear();
            // Add unformatted content back
            tbContent.SetText(TempUnformattedText, false);
            tbContent.ResumeLayout();

            if (CurrentEntry.CaretPosition > 0)
            {
                // Move cursor to last selected position
                tbContent.Focus();
                tbContent.Select(CurrentEntry.CaretPosition, 0);
                tbContent.ScrollToCaret();
            }
        }

        private static string entryInformation { get; set; }
        private void ShowEntryInformation()
        {
            if (CurrentEntry != null)
            {
                if (!CurrentEntry.IsDummyEntry)
                {
                    Log.GeneralEntryStatus("Information dialog");
                    entryInformation = "Selected Entry: \"" + CurrentEntry.Title + "\"" + Environment.NewLine + Environment.NewLine;
                    entryInformation += "Created date: " + CurrentEntry.Created + Environment.NewLine;
                    entryInformation += "Changed date: " + CurrentEntry.Changed + Environment.NewLine;
                    if (!string.IsNullOrEmpty(CurrentEntry.Content) && !string.IsNullOrEmpty(tbContent.Text))
                    {
                        // Count characters
                        entryInformation += "Character count: " + tbContent.Text.Length + Environment.NewLine;

                        // Count words ("^\s.?,"  and "\w")
                        string groupOfWords = tbContent.Text;
                        var matchesByAlphaNumeric = Regex.Matches(groupOfWords,
                                @"[^\s.?,]+", RegexOptions.CultureInvariant | RegexOptions.Multiline
                                | RegexOptions.IgnoreCase);

                        entryInformation += "Word count: " + matchesByAlphaNumeric.Count + Environment.NewLine;
                    }
                    entryInformation += "Pinned: " + CurrentEntry.Pin.ToString() + Environment.NewLine;
                    entryInformation += "Write protected: " + CurrentEntry.Protected.ToString() + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    entryInformation = MessageText.SelectEntryToContinueMessage();
                }
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(entryInformation, MessageText.EntryInformationCaption(), false);
                messageBox.TopMost = true;
                messageBox.ShowDialog();
            }
        }

        #endregion

        #region LOAD AND SAVE
        bool foundPinnedMatch { get; set; }
        bool messageBoxAnswer { get; set; }
        bool pinChanged { get; set; }
        bool titleUpdated { get; set; }
        private bool SaveCurrentEntry(bool askBeforeSaving)
        {
            // Reset activity for global timer
            KeepAwake();

            if (CurrentEntry != null && (CurrentEntry.HasChanges && !CurrentEntry.IsDummyEntry))
            {
                if (askBeforeSaving)
                {
                    AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.SaveChangesQuestion(CurrentEntry.Title), MessageText.SaveCaption(), true, false);
                    ahoyAnswerMessageBox.TopMost = true;
                    ahoyAnswerMessageBox.ShowDialog();

                    // If canceled return false
                    if (ahoyAnswerMessageBox.Canceled)
                    {
                        return false;
                    }
                    else
                    {
                        messageBoxAnswer = ahoyAnswerMessageBox.Accepted;
                    }
                }
                else
                {
                    messageBoxAnswer = true;
                }

                if (messageBoxAnswer)
                {
                    if (CurrentEntry.Title != tbTitle.Text)
                    {
                        titleUpdated = true;
                    }
                    else
                    {
                        titleUpdated = false;
                    }

                    CurrentEntry.IgnoreTextChanged = false;
                    // Check for template tags and update tilte
                    CurrentEntry.Title = EntryTemplate.GetResult(tbContent.Text, tbTitle.Text);
                    CurrentEntry.Content = tbContent.Text;
                    // Update Pinned list and ressubmit changed display title
                    if (EntryTemplate.Triggered)
                    {
                        tbTitle.Text = CurrentEntry.Title;
                        PopulatePinList();
                    }
                    CurrentEntry.Changed = IO.GetIsoDateTimeNow();
                    pinChanged = CurrentEntry.Pin;
                    //CurrentEntry.Pin = cbPinned.Checked;
                    SetPinnedState(CurrentEntry, cbPinned.Checked); // Related
                    //if (!CurrentEntry.Pin)
                    //{
                    //    ResetTitleColorOnCurrentEntry(); // Remove pinned color // Pinned color // pin color
                    //}
                    //else
                    //{
                    //    RefreshTitleColor(); // Update title color
                    //}
                    RefreshTitleColor(); // Keep colors: Update title color (was normally the one in the else statement above)
                    CurrentEntry.Protected = cbProtect.Checked;
                    CurrentEntry.CaretPosition = tbContent.SelectionStart;
                    UpdateMenuItems();

                    // Submit changes to entries list
                    SubmitChangesToEntriesList();
                }
                else
                {
                    // If no ignore changes to old and new entries
                    CurrentEntry.HasChanges = false;
                    CurrentEntry.IsNewEntry = false;
                }

                // Update pinlist item with changed title text if current saved item is pinned
                if ((CurrentEntry.Pin || IsCurrenRelatedAndVisibleInPinnedList()) && !EntrySelectionInProgress && titleUpdated)
                {
                    PopulatePinList();
                    if (PinnedIsVisible)
                    {
                        ReselectPinned();
                    }
                }
                else if (listBoxPinned.Items.Count > 0 && (CurrentEntry.Pin || IsCurrenRelatedAndVisibleInPinnedList()))
                {
                    // Check if entry is in pin list already
                    foundPinnedMatch = false;
                    for (int i = 0; i < listBoxPinned.Items.Count; i++)
                    {
                        if (((EntryMinimal)listBoxPinned.Items[i]).ID == CurrentEntry.ID)
                        {
                            foundPinnedMatch = true;
                        }
                    }

                    // If entry was just pinned and does not exist in pinlist populate pinlist
                    if (!foundPinnedMatch)
                    {
                        PopulatePinList();
                    }

                    // Update title with current changes
                    for (int i = 0; i < listBoxPinned.Items.Count; i++)
                    {
                        if (((EntryMinimal)listBoxPinned.Items[i]).ID == CurrentEntry.ID)
                        {
                            ((EntryMinimal)listBoxPinned.Items[i]).Title = CurrentEntry.Title;
                        }
                    }
                }
                else if (listBoxPinned.Items.Count > 0 && !CurrentEntry.Pin)
                {
                    // Else remove from pin list if pin is false and ID exists in pinlist
                    for (int i = 0; i < listBoxPinned.Items.Count; i++)
                    {
                        if (((EntryMinimal)listBoxPinned.Items[i]).ID == CurrentEntry.ID)
                        {
                            PopulatePinList();
                            break;
                        }
                    }
                }

                // Highlight if just pinned
                if (messageBoxAnswer && (pinChanged != CurrentEntry.Pin || IsCurrenRelatedAndVisibleInPinnedList()))
                {
                    if (CurrentEntry.Pin || IsCurrenRelatedAndVisibleInPinnedList())
                    {
                        PopulatePinList();
                        // Show pinned
                        ShowPinned();
                        CheckListsAndHighlight(CurrentEntry.ID);
                        // Move the newly added pin to the top of the list
                        MovePinnedToTop();
                    }
                }

            }
            return true;
        }

        /// <summary>
        /// Will only set pinned to true if not related
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="state"></param>
        private void SetPinnedState(Entry entry, bool state)
        {
            if (state && !Related.IsRelated(entry, Entries))
            {
                entry.Pin = state;
            }
            else
            {
                entry.Pin = false;
            }
        }

        private void SubmitChangesToEntriesList()
        {
            if (!LockedForWritingToDisk)
            {
                // If new entry, add the object to the list
                if (CurrentEntry.IsNewEntry)
                {
                    // Set default caret position equal to content length (default text "New content")
                    if (CurrentEntry.CaretPosition == 0)
                    {
                        CurrentEntry.CaretPosition = CurrentEntry.Content.Length;
                    }
                    Log.LogSaveEntry(CurrentEntry.Title);
                    Entries.Add(IO.CloneEntry(CurrentEntry, false));
                    CurrentEntry.HasChanges = false;
                    CurrentEntry.IsNewEntry = false;
                    SetPoolHasChanges();
                }
                else if (CurrentEntry.HasChanges)
                {
                    // Find the matching entry and update all but the ID and Created date
                    foreach (Entry entry in Entries)
                    {
                        if (entry.ID == CurrentEntry.ID)
                        {
                            Log.LogSaveEntry(CurrentEntry.Title);
                            entry.Title = CurrentEntry.Title;
                            entry.Content = CurrentEntry.Content;
                            entry.Changed = CurrentEntry.Changed;
                            entry.Pin = CurrentEntry.Pin;
                            // Not set ranking here (entry.Rank = CurrentEntry.Rank;), this is handled in the SubmitPinnedRanking() method // 19-03-2020
                            entry.PresetColor = CurrentEntry.PresetColor;
                            entry.CustomColor = CurrentEntry.CustomColor;
                            entry.Protected = CurrentEntry.Protected;
                            entry.CaretPosition = CurrentEntry.CaretPosition;
                            entry.Bookmark = CurrentEntry.Bookmark;
                            entry.MarkExport = CurrentEntry.MarkExport;
                            entry.RelatedEntries = CurrentEntry.RelatedEntries;
                            entry.RelatedView = CurrentEntry.RelatedView;
                            entry.RelatedStyle = CurrentEntry.RelatedStyle;
                            CurrentEntry.HasChanges = false;
                            SetPoolHasChanges();
                        }
                    }

                    // Resolve Export button when submitting to entries list
                    ResolveExportButton();
                }
            }
        }

        // This function will save the caret position regardless of content
        // -hence you can discard text changes but the caret will be persisted
        // behind the scenes.
        private void SubmitCaretPositionChangesToEntriesList()
        {
            if (!LockedForWritingToDisk)
            {
                // Find the matching entry and update the caret position
                foreach (Entry entry in Entries)
                {
                    if (entry.ID == CurrentEntry.ID)
                    {
                        entry.CaretPosition = CurrentEntry.CaretPosition;
                        SetPoolHasChanges();
                    }
                }
            }
        }

        // This function will save new content regardless of the currently selected
        // entry, behind the scenes. (Used by the PLOP Editor)
        private static bool SubmitContentChangesSuccess;
        private void SubmitContentChangesToEntriesList(string entryID, string newContent, bool caretTop)
        {
            SubmitContentChangesSuccess = false;
            if (!LockedForWritingToDisk)
            {
                // Find the matching entry and update the caret position
                foreach (Entry entry in Entries)
                {
                    if (entry.ID == entryID)
                    {
                        entry.Content = newContent;
                        if (caretTop)
                        {
                            entry.CaretPosition = 0;
                        }
                        else
                        {
                            entry.CaretPosition = entry.Content.Length - 1;
                        }
                        entry.Changed = IO.GetIsoDateTimeNow();
                        SetPoolHasChanges();
                        SubmitContentChangesSuccess = true;
                    }
                }
            }
        }

        private bool tempWasDemoBeforeReloading;
        private bool tempWasSecondaryPoolBeforeReloading;
        private void ReloadDefaultPool(bool showDialog, bool showMaintenanceIndicator, bool saveAll)
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            if (saveAll)
            {
                // Save everything if changes are present
                SaveAll(); // <------------- SAVE ALL
            }

            tempWasDemoBeforeReloading = IO.UseDemoPool;
            tempWasSecondaryPoolBeforeReloading = IO.UseSecondaryPool;

            // Stop global timer
            if (showMaintenanceIndicator)
            {
                StopGlobalTimerWithMaintenanceMessage();
            }
            else
            {
                StopGlobalTimers();
            }

            AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.ReloadDefaultPoolQuestion(), MessageText.AttentionCaption(), false, true);
            if (showDialog)
            {
                ahoyAnswerMessageBox.ShowDialog();
            }

            if (!showDialog || ahoyAnswerMessageBox.Accepted)
            {
                try
                {
                    // Reset everything before loading
                    ClearDisplays(false);
                    ResetInternalVariablesForLoad();
                    Navigator.InitializeNavigator();
                    ResolveNavigationButtons();
                    // Reset everything before loading
                    ClearAllEntriesFromMemory(true);
                    // Get pool state
                    IO.UseDemoPool = tempWasDemoBeforeReloading;
                    IO.UseSecondaryPool = tempWasSecondaryPoolBeforeReloading;
                    // Load Pool
                    IO.ClearCustomFilePath(true); // Clear custom filepath and save init
                    LoadPool(false, "");
                }
                finally
                {
                    // Start the global timer
                    StartGlobalTimerWithMaintenanceMessage();
                    ShowPinned();
                }
            }
            else
            {
                // Start the global timer
                StartGlobalTimerWithMaintenanceMessage();
            }
        }

        private void ReloadCurrentPool()
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            // Stop global timers
            StopGlobalTimerWithMaintenanceMessage();

            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.ReloadPoolQuestion(PoolHasChanges), MessageText.PoolCaption(), false, true);
            msg.TopMost = true;
            msg.ShowDialog();

            if (msg.Accepted)
            {
                try
                {
                    // Close Utility Editor

                    // Reset everything before loading
                    ClearDisplays(false);
                    ResetInternalVariablesForLoad();
                    Navigator.InitializeNavigator();
                    ResolveNavigationButtons();
                    // Reset everything before loading
                    ClearAllEntriesFromMemory(false);
                    // Load settings
                    LoadSettings();
                    // Reload Load current Pool
                    // If using custom file path by init
                    if (IO.UsingCustomFilePath())
                    {
                        LoadPool(true, "");
                    }
                    else // else load normally
                    {
                        LoadPool(false, "");
                    }
                }
                finally
                {
                    // Start the global timer
                    StartGlobalTimerWithMaintenanceMessage();
                    ShowPinned();
                }
            }
            else
            {
                // Start the global timer
                StartGlobalTimerWithMaintenanceMessage();
            }

        }

        // Reset variables before loading
        private void ResetInternalVariablesForLoad()
        {
            AccessGranted = false;
            IO.UseDemoPool = false;
        }


        private bool tempWasDemoPool;
        private void SwitchPool()
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            if (IO.UsingCustomFilePath())
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to switch pool while using a custom directory path...", MessageText.SwitchPoolCaption(), false);
                msg.ShowDialog();
                return;
            }

            // Remember demo
            tempWasDemoPool = IO.UseDemoPool;

            ShowAnimation();
            AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.SwitchPoolQuestion(), MessageText.SwitchPoolCaption(), false, false);
            ahoyAnswerMessageBox.ShowDialog();

            if (ahoyAnswerMessageBox.Accepted)
            {
                try
                {
                    // Stop the global timer
                    StopGlobalTimers();

                    // Save everything if changes are present
                    SaveAll();

                    // Reset everything before loading
                    ClearAllEntriesFromMemory(true);
                    // Reset all settings
                    ResetAllSettings();
                    SetDefaultWindowSizeAndLocation();

                    // Recall demo setting
                    if (tempWasDemoPool)
                    {
                        IO.UseDemoPool = tempWasDemoPool;
                    }

                    // Close Utility Editor

                    // Exit fullscreen
                    if (IsFullScreen)
                    {
                        ToggleFullScreen();
                    }

                    // SWITCH POOL
                    // (important that it is set after the ClearAllEntriesFromMemory method above)
                    IO.UseSecondaryPool = !IO.UseSecondaryPool;

                    // Load Pool
                    LoadPool(false, "");

                    // Load Settings
                    SetNoTexture(false);
                    LoadSettings();
                    SettingsHasChanges = false;

                    // Update status strip
                    SetStatusText(MessageText.PoolSwitchStatus(IO.GetISOTimeNow()));

                    // Update indicator label
                    ResolveIndicatorLabel();
                }
                finally
                {
                    // Start the global timer
                    StartGlobalTimers();
                    ShowPinned();
                }

                // Resolve status
            }
        }

        // Used when switching pools (clipboard is persisted)
        private void ClearAllEntriesFromMemory(bool clearCustomFilePath)
        {
            // Close typermax
            // Close all Stickies
            CloseAllStickiesAndClearMemory();
            // This will prohibit saving the pool because access will be denied
            ResetInternalVariablesForLoad();
            // Clear Export marks
            ClearExportMarks(false);
            // Clear Pool
            Entries.Clear();
            EntriesMirror.Clear();
            // Clear PLOP
            // Clear custom file path
            if (clearCustomFilePath)
            {
                IO.ClearCustomFilePath(false);
            }
            // Clear Utility Editor
            // Reset Paragraph Dividers
            ResetParagraphDividers();
            // Clear Log
            Log.ClearLog();
            // Clear typermax highscore
            // Clear Hold buttons
            ClearHoldButtons();
            // Clear all internal lists
            CustomDisplayText = "";
            if (SearchCollection != null)
                SearchCollection.Clear();
            if (HistorySearchCollection != null)
                HistorySearchCollection.Clear();
            if (UtilitySearchCollection != null)
                UtilitySearchCollection.Clear();
            if (PinnedList != null)
                PinnedList.Clear();
            // Clear pinned listbox (important)
            listBoxPinned.Items.Clear();
            Navigator.InitializeNavigator();
            ResolveNavigationButtons();
            // Clear memo field
            ClearMemoField();
            // Clar mark
            Related.ClearMark();
            // Preserve clipboard between pool switches
            PersistClipboard();
            ClearDisplays(false);
            // Retrieve if any text from the clipboard were persisted
            RetrieveClipboardText();
        }


        private void SaveChangesWithBackup()
        {
            // Save pending changes which in term will make the SavePool method take action
            SaveCurrentEntry(false);
            // Save pool if closing form
            ResolveMainAndAlternateBackup();
            // Save settings to save the selected backup identifier
            SaveSettings();
        }


        // Ready for saving pool
        internal static void SetPoolHasChanges()
        {
            PoolHasChanges = true;
        }

        private void SavePool(bool showDialog, bool saveWithBackup, bool saveFilePath, string filePath)
        {
            if ((PoolHasChanges && !PoolIsReadOnly) || OverrideToSetWriteProtection)
            {
                OverrideToSetWriteProtection = false;
                PoolSaveWithFileDialogSuccess = false;
                PoolHasChanges = false;

                Log.LogSavePool(saveWithBackup);
                LockedForWritingToDisk = true;
                try
                {
                    if (IO.SaveEntries(Entries, StickySettingsList, Log.GetLogAsSaveData(), HoldEntryAID, HoldEntryBID, tbMemoField.Text, MemoCaretPosition, CustomDisplayText, CurrentKey, saveWithBackup, CypherKey, saveFilePath, filePath, PoolIsReadOnly, DividerAString, DividerBString, DividerCString, DividerDString, Dump))
                    {
                        if (showDialog)
                        {
                            AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.PoolSavedMessage(), MessageText.SaveCaption(), false);
                            messageBox.TopMost = true;
                            messageBox.ShowDialog();
                        }

                        SetStatusText(MessageText.PoolSavedStatus(IO.GetISOTimeNow()));
                        Log.LogSavePoolSuccess();
                        // Indicate success
                        PoolSaveWithFileDialogSuccess = true;
                    }
                    else
                    {
                        Log.LogSavePoolError();
                        SetStatusText(MessageText.PoolErrorStatus());
                    }
                }
                catch
                {
                    // Do nothing
                }
                finally
                {
                    LockedForWritingToDisk = false;
                }
            }
            else if (PoolIsReadOnly)
            {
                SetStatusText("*** UNABLE TO SAVE CHANGES: POOL WRITE-PROTECTED *** ");
            }
        }
        private Load PerformPoolLoad(bool openFilePath, string filePath)
        {
            return IO.LoadEntries(Entries, UnlockPool, false, false, openFilePath, filePath);
        }
        // Will try to load both backup A and B
        private Load PerformPoolBackupLoad()
        {
            // Try to load Backup "A"
            Load loadedObject = IO.LoadEntries(Entries, UnlockPool, true, true, false, "");
            // If failed (but not because the user cancelled the password prompt) try to load Backup "B"
            if (!loadedObject.LoadSuccess && !loadedObject.UnlockCancelled && !loadedObject.OpenOtherPoolRequested)
            {
                loadedObject = IO.LoadEntries(Entries, UnlockPool, true, false, false, "");
            }
            return loadedObject;
        }
        // Count all text data in the pool
        private int tempPoolTotalCharacterCount;
        private int GetPoolTotalCharacterCount()
        {
            tempPoolTotalCharacterCount = 0;
            try
            {
                if (Entries != null && Entries.Count > 0)
                {
                    foreach (Entry e in Entries)
                    {
                        if (e.Content != null && e.Content.Length > 0)
                        {
                            tempPoolTotalCharacterCount += e.Content.Length;
                        }

                        if (e.Title != null && e.Title.Length > 0)
                        {
                            tempPoolTotalCharacterCount += e.Title.Length;
                        }

                    }
                }

                if (tbMemoField != null && tbMemoField.Text.Length > 0)
                {
                    tempPoolTotalCharacterCount += tbMemoField.Text.Length;
                }

                if (DividerAString != null && DividerAString.Length > 0)
                {
                    tempPoolTotalCharacterCount += DividerAString.Length;
                }

                if (DividerBString != null && DividerBString.Length > 0)
                {
                    tempPoolTotalCharacterCount += DividerBString.Length;
                }

                if (DividerCString != null && DividerCString.Length > 0)
                {
                    tempPoolTotalCharacterCount += DividerCString.Length;
                }

                if (DividerDString != null && DividerDString.Length > 0)
                {
                    tempPoolTotalCharacterCount += DividerDString.Length;
                }

                if (Changes != null && Changes.Length > 0)
                {
                    tempPoolTotalCharacterCount += Changes.Length;
                }

                if (StickySettingsList != null && StickySettingsList.Count > 0)
                {
                    foreach (StickySettings s in StickySettingsList)
                    {
                        tempPoolTotalCharacterCount += s.Text.Length;
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return tempPoolTotalCharacterCount;
        }


        private void CopyPoolToMirror()
        {
            // Set Backup list
            if (Entries.Count > 0)
            {
                EntriesMirror.Clear();
                foreach (Entry e in Entries)
                {
                    EntriesMirror.Add(IO.CloneEntry(e, false));
                }
            }
        }


        private static void EnableMasterCloseStickies()
        {
            // "IgnoreStickyChanges = true;" Not used here, because master close is sometimes used to close and save stickies too
            // -when needed called by the method below "IgnoreStickySaveChanges()"
            Log.StickyStatus("Terminate (All)");
            MasterCloseStickies = true;
            MasterCloseStickiesCode = 2;
        }

        private static void IgnoreStickySaveChanges()
        {
            IgnoreStickyChanges = true;
        }

        private static void DisableMasterCloseStickies()
        {
            IgnoreStickyChanges = false; // Revoke ignore changes
            Log.StickyStatus("Terminate (None)");
            MasterCloseStickies = false;
            MasterCloseStickiesCode = 0;
        }
        // Set when the last LoadPool did not unlock/upgrade the pool (wrong password, cancelled)
        private bool LastPoolUnlockCancelled { get; set; }

        private void LoadPool(bool openFilePath, string filePath)
        {
            Related.ClearMark();
            ShowAnimation();
            PoolLoadFromFileDialogSuccess = false;
            LastPoolUnlockCancelled = false;
            Log.LogPreloadStatus("Loading Pool");
            Load loadedObject = PerformPoolLoad(openFilePath, filePath);

            // F5/CTRL+O in the password dialog: open another pool instead
            if (loadedObject.OpenOtherPoolRequested)
            {
                LoadPoolFromFileDialog();
                if (!AccessGranted && !LoggedInOnce)
                {
                    Application.Exit();
                }
                return;
            }

            // Wrong password or cancelled. Never continue to "create a new pool" from here:
            // that would overwrite the file that could not be unlocked.
            if (loadedObject.UnlockCancelled)
            {
                LastPoolUnlockCancelled = true;
                if (!LoggedInOnce)
                {
                    Application.Exit();
                }
                return;
            }

            TogglePoolReadOnly(true, loadedObject.WriteProtected, false);
            this.Text = GetFormTitle();

            // If pool does not exist try to create a new one
            if (!loadedObject.LoadSuccess)
            {
                Log.LogPreloadStatus("No Pool found");
                Log.LogPreloadStatus("Posting selection dialog");
            Retry: // Goto statement
                AhoyAnswerMessageBox msg0 = new AhoyAnswerMessageBox(MessageText.WelcomeNewProfileOrLoadMessage(), MessageText.WelcomeCaption(), true, false);
                msg0.TopMost = true;
                msg0.ShowInTaskbar = true; // The main window isn't shown yet: give the first-run dialog a taskbar button
                // Set custom text
                msg0.SetCustomButtonText("Exit", "New", "Load", "Welcome", "Profile", "Pool (Select File)", "Application (ESC)", 2);
                msg0.ShowDialog();

                // Exit if cancel was selected
                if (msg0.Canceled)
                {
                    Application.Exit();
                    return;
                }

                // If new profile is selected
                if (msg0.Accepted)
                {
                    // Never replace a Pool file that exists but could not be opened
                    if (IO.DoesPoolExist())
                    {
                        AhoySimpleMessageBox exists = new AhoySimpleMessageBox(MessageText.ErrorMessage(loadedObject.LoadMessage) + "\n\nA Pool file already exists here and will not be overwritten. Move or rename it to create a new Pool.", MessageText.AttentionCaption(), false);
                        exists.TopMost = true;
                        exists.ShowDialog();
                        goto Retry;
                    }

                    // The password is the encryption key, so it must exist before the first save
                    if (!AskForNewPoolPassword(MessageText.CreatePasswordTitle(), MessageText.WelcomeCaption()))
                    {
                        goto Retry;
                    }

                    // Grant temporary access to allow saving
                    AccessGranted = true;
                    // Create the new (empty) pool so there is something for the next steps to validate
                    SetPoolHasChanges();
                    SavePool(false, false, false, "");
                    // Revoke access to return to normal
                    AccessGranted = false;
                }
                else // Else load from file dialog
                {
                    LoadPoolFromFileDialog();
                }

                // Open theme selector
                OpenThemeSelector(false);

                // Remote backup path
                SetAlternateBackupFolder(true);

                // Check if new pool exists, if not in ReadOnly mode (check not required)
                if (!PoolIsReadOnly)
                {
                    if (!IO.DoesPoolExist())
                    {
                        AhoySimpleMessageBox msg2 = new AhoySimpleMessageBox(MessageText.ErrorMessage(loadedObject.LoadMessage), MessageText.AttentionCaption(), false);
                        msg2.TopMost = true;
                        msg2.ShowDialog();
                        goto Retry; // Goto statement to re-show the welcome dialog
                    }
                }
            }

            if (loadedObject.LoadSuccess)
            {
                ApplyLoadedPool(loadedObject);

                // Ahoy 1.x pool: upgrade to the password-based format before anything else
                if (loadedObject.IsLegacyFormat && !UpgradeLegacyPool(loadedObject))
                {
                    // Not upgraded: forget what was read; nothing has been written
                    ClearAllEntriesFromMemory(true);
                    SetCurrentKey(null);
                    AccessGranted = false;
                    LastPoolUnlockCancelled = true;
                    if (!LoggedInOnce)
                    {
                        Application.Exit();
                    }
                    return;
                }

                // Indicate success (the password was verified while decrypting)
                PoolLoadFromFileDialogSuccess = true;
                AccessGranted = true;
                LoggedInOnce = true; // Will enable alerts to show during sleep but not before loggin in
                PopulatePinList();
                SetDefaultText();
                SpawnSticky(false, false, 0, 0, false);
                tbSearch.Focus();
            }
            else if (loadedObject.Entries.Count == 0)
            {
                // Access granted for new pool creation
                AccessGranted = true;
                LoggedInOnce = true; // Will enable alerts to show during sleep but not before loggin in
                // Load log
                Log.SetLogFromSaveData("");
            }
            else
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(loadedObject.LoadMessage, MessageText.LoadCaption(), false);
                messageBox.ShowDialog();
            }

            // Check Session ID
            TestSessionID();
        }

        // Move the data of a loaded pool into the form
        private void ApplyLoadedPool(Load loadedObject)
        {
            // Load entries
            Entries.Clear();
            foreach (Entry entry in loadedObject.Entries)
            {
                Entries.Add(entry);
            }

            // Load stickies
            StickySettingsList.Clear();
            foreach (StickySettings s in loadedObject.Stickies)
            {
                StickySettingsList.Add(s);
            }

            // Set Backup list
            CopyPoolToMirror();

            // Load log
            Log.SetLogFromSaveData(loadedObject.LogText);
            // Load Hold ID's
            HoldEntryAID = loadedObject.HoldEntryAID;
            HoldEntryBID = loadedObject.HoldEntryBID;
            ResolveHoldButtons(); // Resolve buttons

            // Set Memo Text (without saving pool)
            MemoTextChangeRegisterChanges = false;
            tbMemoField.Text = loadedObject.MemoText;
            MemoCaretPosition = loadedObject.MemoCaretPosition;
            FocusMemoField();

            // Paragraph Dividers
            SetParagraphDividersFromLoad(loadedObject);

            // Set Cypher key
            CypherKey = loadedObject.CypherKey;

            // Custom Home Screen Display Text
            CustomDisplayText = loadedObject.DisplayText;

            // Remote Backup Path (now placed in settings instead)
            // (Transfers old backup path in pool to settings)
            // Will be deleted on next pool save
            if (loadedObject.AlternateBackupPath.Length > 0)
            {
                Log.LogLegacyBackupPathUpdate();
                AlternateBackupPath = loadedObject.AlternateBackupPath;
                AlternateBackupPathStringIsSet(); // Update menu item with check/uncheck
                // Validate remote backup path
                AlternateBackupPathValid(true);
                // Fix settings saving path
                SettingsHasChanges = true;
                SaveSettings();
            }

            // Get total char count for backup rutine
            ProcessPoolSizeForBackupRutine();
        }
        private void SetParagraphDividersFromLoad(Load loadedObject)
        {
            if (!string.IsNullOrEmpty(loadedObject.DividerAString))
            {
                DividerAString = loadedObject.DividerAString;
                ParagraphDividers.DividerAString = DividerAString;
            }

            if (!string.IsNullOrEmpty(loadedObject.DividerBString))
            {
                DividerBString = loadedObject.DividerBString;
                ParagraphDividers.DividerBString = DividerBString;
            }

            if (!string.IsNullOrEmpty(loadedObject.DividerCString))
            {
                DividerCString = loadedObject.DividerCString;
                ParagraphDividers.DividerCString = DividerCString;
            }

            if (!string.IsNullOrEmpty(loadedObject.DividerDString))
            {
                DividerDString = loadedObject.DividerDString;
                ParagraphDividers.DividerDString = DividerDString;
            }
        }


        private void LoadBackupPool()
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            if (IO.UsingCustomFilePath())
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to run automated backup recovery while using a custom directory path. Load Pool backups manually instead", MessageText.BackupRecoveryCaption(), false);
                msg.ShowDialog();
                return;
            }


            StopGlobalTimerWithMaintenanceMessage();
            AhoyAnswerMessageBox backupMessageBox = new AhoyAnswerMessageBox(MessageText.BackupRecoveryQuestion(), MessageText.BackupRecoveryCaption(), false, true);
            backupMessageBox.ShowDialog();

            if (!backupMessageBox.Accepted || backupMessageBox.Canceled)
            {
                StartGlobalTimerWithMaintenanceMessage();
                return;
            }

            Log.LogLoadPool();
            Load loadedObject = PerformPoolBackupLoad();

            // If loading backup failed
            if (!loadedObject.LoadSuccess)
            {
                AhoySimpleMessageBox msg1 = new AhoySimpleMessageBox(MessageText.ErrorMessage(loadedObject.LoadMessage), MessageText.AttentionCaption(), false);
                msg1.ShowDialog();
                StartGlobalTimerWithMaintenanceMessage();
                return;
            }

            // A backup from Ahoy 1.x is simply re-saved with the current password
            ApplyLoadedPool(loadedObject);
            if (loadedObject.IsLegacyFormat)
            {
                SetPoolHasChanges();
                SavePool(false, false, false, "");
            }

            if (loadedObject.LoadSuccess)
            {
                LoggedInOnce = true; // Will enable alerts to show during sleep but not before loggin in
                PopulatePinList();
                SetDefaultText();
                SpawnSticky(false, false, 0, 0, false);
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.BackupRecoveryCompletedMessage(), MessageText.BackupRecoveryCaption(), false);
                msg.ShowDialog();
                tbSearch.Focus();
            }
            else if (loadedObject.Entries.Count == 0)
            {
                // Ask for new login credentials
                AccessGranted = true;
            }
            else
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(loadedObject.LoadMessage, MessageText.LoadCaption(), false);
                messageBox.ShowDialog();
            }

            // Check Session ID
            TestSessionID();
            // Start global timer again
            StartGlobalTimerWithMaintenanceMessage();
        }

        // Get char count for the Main and Remote backup rutine (in the global timer _Tick event)
        private void ProcessPoolSizeForBackupRutine()
        {
            PoolPreviousCharCount = GetPoolTotalCharacterCount();
            PoolCurrentCharCount = PoolPreviousCharCount;
        }

        private void LoadSettings()
        {
            Log.LogPreloadLoadSettings();
            Settings settingsObject = new Settings();
            // Utility Editor
            // Ensure default values are set to the static fields
            // inside the utility editor class
            SetDetachedEditorParentLocation();
            // Then load settings
            settingsObject = IO.LoadSettings();

            if (settingsObject.LoadSuccess)
            {
                Log.LogPreloadStatus("Applying settings");
                // Custom colors first
                AhoyColors.LoadCustomColorsIntoList(settingsObject.CustomColors);
                AhoyColors.CustomColorThemeTitle = settingsObject.ThemeTitle;
                // Set color theme
                AhoyColors.ColorTheme = settingsObject.ColorTheme;
                ButtonBitmaps.UserSelectedBitmapVersion = settingsObject.ButtonBitmapOverride;
                // Texture
                SelectedTexture = settingsObject.Texture;
                // Produce color theme
                SetColorTheme(false);

                // Set layout mode (Force showing the Hold buttons if Hold ID's are present)
                if (settingsObject.LayoutMode == 0 && (HoldEntryAID.Length > 0 || HoldEntryBID.Length > 0))
                {
                    settingsObject.LayoutMode = Settings.DefaultLayoutMode;
                    SetLayoutMode(settingsObject.LayoutMode);
                }
                else
                {
                    SetLayoutMode(settingsObject.LayoutMode);
                }

                // Set Opacity
                SetFormOpacity(settingsObject.Opacity, false);

                // Set Backup identifier
                BackupIdentifier = settingsObject.BackupIdentifier;
                AlternateBackupIdentifier = settingsObject.BackupIdentifier;

                // Remote backup path
                // Remote backup path
                AlternateBackupPath = settingsObject.BackupLocation;
                AlternateBackupPathStringIsSet(); // Update menu item with check/uncheck
                                                  // Validate remote backup path
                AlternateBackupPathValid(true);

                // Set sleep interval
                SetSleepInterval(settingsObject.SleepIntervalIndex, false);

                // Set word wrap
                tbContent.WordWrap = settingsObject.WordWrap;
                ResolveWordWrapMenuCheck();

                // Title Tab to Content
                TitleTabToContent = settingsObject.TitleTabToContent;
                ResolveTitleTabToContent();

                // Insert new lines
                InsertLines = settingsObject.InsertNewLines;
                ResolveInsertNewLines();

                // Set Browser
                SelectedInternetBrowser = settingsObject.InternetBrowser;
                DetectURLs = settingsObject.DetectURLs;
                ResolveDefaultBrowserCheckedMenuItems();
                ResolveDetectURLsCheckedMenuItem();

                // Notification settings

                // Set lists autohide
                AutoHidePinnedAndMemo = settingsObject.ListsAutoHide;
                ResolveAutoHideListsMenuCheck();

                // Set spell checker language
                SpellCheckerLanguage = settingsObject.SpellCheckerLanguage;

                // Set Animations
                AnimationsEnabled = settingsObject.Animations;

                // DisplayText cycle
                DisplayTextCycle = settingsObject.DisplayTextCycle;
                RefreshDummyText();

                // Set text zooms
                Design.CurrentListFontWeight = settingsObject.CurrentListFontWeight; // Must be set first
                SetFieldsFontSize(settingsObject.FieldsFontSize, false, false);
                SetListsFontSize(settingsObject.ListsFontSize);
                Design.CurrentFont = settingsObject.CurrentFont;
                ResolveFontSelector();
                Design.EnforceScrollbars = settingsObject.EnforceScrollbars;
                ResolveEnforceScrollbarsMenu();

                // Set WPM counter
                ShowWPMCounter = settingsObject.ShowWPMCounter;
                ResolveWordAndWPMToolStripMenuItem();
                UpdateWPMValues();

                // Selected password style
                SelectedPasswordStyle = settingsObject.SelectedPasswordStyle;

                // Autosave Entry changes
                AutoSaveEntryChanges = settingsObject.AutoSaveEntryChanges;
                UpdateSaveButton();

                // Autosearch
                AutoSearch = settingsObject.AutoSearch;
                UpdateAutoSearchVisuals();

                // MicroCalculator

                // Typewriter

                // Screen Blocker

                // Prism effect

                // Search
                TitlePrioritySearch = settingsObject.TitlePrioritySearch;
                ResolveTitlePrioritySearchMenuItem();
                LiveSearch = settingsObject.LiveSearch;
                ResolveLiveSearchMenuItem();

                // Clock
                ShowClock = settingsObject.ShowClock;
                ResolveClockMenuItem();

                // Form border
                EnableFormBorder = settingsObject.EnableFormBorder;
                ResolveFormBorder();

                // New pinned by click
                NewPinnedByClick = settingsObject.NewPinnedByClick;
                ResolveNewPinnedByClickMenuItem();

                // Hide pinned by click
                HidePinnedByClick = settingsObject.HidePinnedByClick;
                ResolveHidePinnedByClickMenuItem();

                // Keyboard Control
                MoveByKeyboard = settingsObject.MoveByKeyboard;
                ResizeByKeyboard = settingsObject.ResizeByKeyboard;
                ResolveKeyboardControlMenuItem();

                // Timestamp format
                TimeStampFormat = settingsObject.TimestampFormat;

                // Open file dialog path
                OpenFilePath = settingsObject.OpenFileDialogPath;
                // Save file dialog path
                SaveFilePath = settingsObject.SaveFileDialogPath;

                // -- TESTED AND VERIFIED ON 3-SCREEN SETUPS --
                // Handle full screen
                if (settingsObject.Fullscreen)
                {
                    SetFullScreen(false);
                }
                else if (settingsObject.Maximized)
                {
                    MaximizeWindow(false);
                }
                else // Handle window size and position
                {
                    try
                    {
                        // Set window size
                        Size newWindowSize = new Size(settingsObject.Width, settingsObject.Height);
                        this.Size = newWindowSize;
                        // Set window location (can be + and - in both X and Y to allow for multi-screen setups)
                        Point newLocation = new Point(settingsObject.PositionX, settingsObject.PositionY);

                        this.Location = newLocation;
                        // Get current location
                        UpdateLocationInformation();
                    }
                    catch
                    {
                        // Position is not verified set defaults
                        SetDefaultWindowSizeAndLocation();
                    }
                }

                // Set Utility Editor

                // Ensure that form is inside visible screen area
                if (!IsOnScreen(this))
                {
                    Log.LogPreloadStatus("Adjusting location...");
                    SetDefaultWindowSizeAndLocation();
                }

                // Open Utility Editor
            }
            else
            {
                // Error loading settings file, perhaps because it wasn't created yet
                Log.LogPreloadLoadSettingsError(settingsObject.LoadMessage);
            }
        }

        private void SetFormOpacity(double value, bool saveChanges)
        {
            Log.Opacity(value.ToString());
            if (value >= 0.50d && value <= 1.00d)
            {
                this.Opacity = value;
            }
            else
            {
                this.Opacity = Settings.DefaultOpacity;
            }

            if (saveChanges)
            {
                SettingsHasChanges = true;
            }
            // Update static field for child forms to fetch
            FormOpacity = this.Opacity;
            // Resolve menu item check
            ResolveOpacityMenuCheck();
        }

        private void ResubmitFormOpacity()
        {
            if (FormOpacity >= 0.50d && FormOpacity <= 1.00d)
            {
                this.Opacity = FormOpacity;
            }
            else
            {
                this.Opacity = Settings.DefaultOpacity;
            }
            SettingsHasChanges = true;
            // Resolve menu item check
            ResolveOpacityMenuCheck();
        }

        // Used by child forms to retrieve opacity setting
        public static double GetFormOpacity()
        {
            if (FormOpacity >= 0.50d && FormOpacity <= 1.00d)
            {
                return FormOpacity;
            }
            else
            {
                return Settings.DefaultOpacity;
            }
        }


        private void enforceScrollbarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleScrollbars();
        }

        // Scrollbars
        public void ToggleScrollbars()
        {
            Design.EnforceScrollbars = !Design.EnforceScrollbars;
            ResolveEnforceScrollbarsMenu();
            SettingsHasChanges = true;
        }

        public void ResolveEnforceScrollbarsMenu()
        {
            if (Design.EnforceScrollbars)
            {
                enforceScrollbarsToolStripMenuItem.Checked = true;
            }
            else
            {
                enforceScrollbarsToolStripMenuItem.Checked = false;
            }

            ResolveWordWrapMenuCheck();
            UpdateListScrollbars();
        }


        private void SaveSettings()
        {
            if (!AllowSavingSettings)
            {
                return;
            }

            if (SettingsHasChanges)
            {
                SettingsHasChanges = false;
                ForceSavingSettings = false;
                Settings settingsObject = new Settings();
                settingsObject.Fullscreen = IsFullScreen;
                settingsObject.Maximized = IsMaximized;

                // Handle windows size:
                /* The WindowLocation setting is self-explanatory, but the WindowSize property has a wrinkle.
                 * If a window is normal size, we can read its size property in the usual manner.
                 * But if a window is minimized or maximized, the size property will return an inaccurate value.
                 * So, .NET provides a RestoreBounds property that will return the size of the window in its normal state.
                 * But—and here’s the wrinkle—the RestoreBounds property returns a valid value only when the window is minimized or maximized.
                 * As a result, we have to test the WindowState property, and call either the Size or RestoreBounds property based on the results.*/
                if (this.WindowState == FormWindowState.Normal)
                {
                    settingsObject.Width = this.Size.Width;
                    settingsObject.Height = this.Size.Height;
                    settingsObject.PositionX = this.Location.X;
                    settingsObject.PositionY = this.Location.Y;
                }
                else if (this.WindowState == FormWindowState.Maximized)
                {
                    settingsObject.Maximized = true;
                }
                else
                {
                    settingsObject.Width = this.RestoreBounds.Size.Width;
                    settingsObject.Height = this.RestoreBounds.Size.Height;
                }

                // Utility/PLOP editor

                // Save color theme
                settingsObject.ColorTheme = AhoyColors.ColorTheme;
                // Save custom colors
                settingsObject.CustomColors = AhoyColors.RetrieveCustomColorsAsCSV();
                settingsObject.ThemeTitle = AhoyColors.CustomColorThemeTitle;
                settingsObject.ButtonBitmapOverride = ButtonBitmaps.UserSelectedBitmapVersion;
                settingsObject.LayoutMode = LayoutMode;
                settingsObject.Opacity = this.Opacity;
                settingsObject.Texture = SelectedTexture;
                settingsObject.Animations = AnimationsEnabled;
                settingsObject.BackupIdentifier = BackupIdentifier;
                settingsObject.SleepIntervalIndex = SleepIntervalIndex;
                settingsObject.WordWrap = tbContent.WordWrap;
                settingsObject.TitleTabToContent = TitleTabToContent;
                settingsObject.InsertNewLines = InsertLines;
                settingsObject.CurrentFont = Design.CurrentFont;
                settingsObject.FieldsFontSize = FieldsFontSize;
                settingsObject.ListsFontSize = ListsFontSize;
                settingsObject.CurrentListFontWeight = Design.CurrentListFontWeight;
                settingsObject.EnforceScrollbars = Design.EnforceScrollbars;
                settingsObject.ListsAutoHide = AutoHidePinnedAndMemo;
                settingsObject.SpellCheckerLanguage = SpellCheckerLanguage;
                settingsObject.InternetBrowser = SelectedInternetBrowser;
                settingsObject.DetectURLs = DetectURLs;
                settingsObject.ShowWPMCounter = ShowWPMCounter;
                settingsObject.SelectedPasswordStyle = SelectedPasswordStyle;
                settingsObject.AutoSaveEntryChanges = AutoSaveEntryChanges;
                settingsObject.OpenFileDialogPath = OpenFilePath;
                settingsObject.SaveFileDialogPath = SaveFilePath;
                settingsObject.AutoSearch = AutoSearch;
                settingsObject.DisplayTextCycle = DisplayTextCycle;
                settingsObject.ShowClock = ShowClock;
                settingsObject.TitlePrioritySearch = TitlePrioritySearch;
                settingsObject.LiveSearch = LiveSearch;
                settingsObject.EnableFormBorder = EnableFormBorder;
                settingsObject.NewPinnedByClick = NewPinnedByClick;
                settingsObject.HidePinnedByClick = HidePinnedByClick;

                // Remote backup path
                settingsObject.BackupLocation = AlternateBackupPath;

                // MicroCalculator

                // Screen Blocker

                // Prism effect

                // Keyboard Control
                settingsObject.MoveByKeyboard = MoveByKeyboard;
                settingsObject.ResizeByKeyboard = ResizeByKeyboard;

                // Timestamp
                settingsObject.TimestampFormat = TimeStampFormat;

                if (!IO.SaveSettings(settingsObject))
                {
                    // Error saving settings file
                    Log.LogSaveSettingsError("Unable to save settings...");
                }
                else
                {
                    Log.LogSaveSettings();
                }

                SetStatusText(MessageText.SettingsSavedStatus(IO.GetISOTimeNow()));

            }
        }

        #endregion

        #region General helpers


        private static bool ForceSavingSettings = false;

        public static void ForceSaveSettings()
        {
            SettingsHasChanges = true;
            ForceSavingSettings = true;
        }


        internal static void PrepareTexturePreview()
        {
            SelectedTexturePreview = SelectedTexture;
        }


        private static bool RefreshFormRequested { get; set; }
        internal static void RefreshForm()
        {
            RefreshFormRequested = true;
        }


        private static bool RemoteStyleEditor;
        internal static void OpenStyleEditorPerRemote()
        {
            RemoteStyleEditor = true;
        }


        private void CheckForStyleEditorActions()
        {
            if (StyleEditorHasChanges)
            {
                // Update color and texture
                SetColorTheme(true);
                ResolveTexture();
                ResubmitFormOpacity();
                RefreshDummyText();
                ResolveHomeScreenSelectionMenu();
                // Update variables
                StyleEditorHasChanges = false;
                SettingsHasChanges = true;
                SaveSettings();
                Log.StyleUpdated();
            }
        }


        internal static bool EntriesExist()
        {
            if (Entries != null && Entries.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void ResetSearchResultSelectionIndex()
        {
            KeepAwake();
            SearchBoxResultCycleIndex = 0;
        }


        private void SetSearchResultSelectionIndex(int index)
        {
            KeepAwake();
            SearchBoxResultCycleIndex = index;
        }


        private void HideLoadingPanel()
        {
            if (pnlLoading.Visible)
            {
                pnlLoading.Visible = false;
                pnlLoading.SendToBack();
            }
        }
        #endregion

        #region PASSWORD
        // Check a password against the key of the open pool
        private bool VerifyPassword(string password)
        {
            return CurrentKey != null && CurrentKey.Verify(password);
        }

        private void SetCurrentKey(PoolKey key)
        {
            if (!ReferenceEquals(CurrentKey, key))
            {
                CurrentKey?.Dispose();
            }
            CurrentKey = key;
        }

        // Ask for the password to unlock the interface (e.g. after sleep)
        private void PromptForPassword(string displayCaption, string optionalWelcomeMessage, string optionalTitleString)
        {
            Password passwordDialog = new Password(MessageText.EnterPasswordCaption(), displayCaption, optionalWelcomeMessage, VerifyPassword, true, this.Opacity, true);
            passwordDialog.TopMost = true;
            passwordDialog.Text = GetPasswordDialogTitle(displayCaption, optionalTitleString);
            passwordDialog.ShowDialog();
            AccessGranted = passwordDialog.AccessGranted;

            // If F5/CTRL+O was pressed
            if (passwordDialog.LoadCommandActivated)
            {
                LoadPoolFromFileDialog();
            }
        }

        private static string GetPasswordDialogTitle(string displayCaption, string optionalTitleString)
        {
            if (string.IsNullOrEmpty(optionalTitleString))
            {
                return displayCaption;
            }
            if (optionalTitleString.Length > 25)
            {
                optionalTitleString = optionalTitleString.Substring(0, 24) + "...";
            }
            return displayCaption + TextTransform.GetBlankSpace(3) + optionalTitleString;
        }

        // Ask user for password to verify before continuing
        private bool SimplePromptForPassword(string displayCaption, string optionalWelcomeMessage, bool enableKeyCommands)
        {
            Password passwordDialog = new Password(MessageText.EnterPasswordCaption(), displayCaption, optionalWelcomeMessage, VerifyPassword, true, this.Opacity, enableKeyCommands);
            passwordDialog.TopMost = true;
            passwordDialog.ShowDialog();

            // If F5/CTRL+O was pressed
            if (passwordDialog.LoadCommandActivated)
            {
                LoadPoolFromFileDialog();
                return false;
            }
            return passwordDialog.AccessGranted;
        }

        /// <summary>
        /// Called by IO.LoadEntries for an encrypted pool: derive the key from the user's password
        /// (or reuse the key of the open pool) and return the decrypted XML, or null.
        /// </summary>
        private string UnlockPool(byte[] encryptedPool, Load loadObject)
        {
            if (!PoolFile.TryReadHeader(encryptedPool, out byte[] salt, out int iterations))
            {
                loadObject.LoadMessage = "Unsupported or damaged Pool file.";
                return null;
            }

            // Same pool (or one of its backups) as the one already unlocked: no need to ask again
            if (CurrentKey != null && CurrentKey.Matches(salt, iterations) &&
                PoolFile.TryDecrypt(encryptedPool, CurrentKey, out string sameKeyXml))
            {
                return sameKeyXml;
            }

            string decryptedXml = null;
            PoolKey unlockedKey = null;
            bool TryPassword(string password)
            {
                PoolKey candidate = PoolKey.Derive(password, salt, iterations);
                if (PoolFile.TryDecrypt(encryptedPool, candidate, out string xml))
                {
                    decryptedXml = xml;
                    unlockedKey = candidate;
                    return true;
                }
                candidate.Dispose();
                return false;
            }

            Password passwordDialog = new Password(MessageText.EnterPasswordCaption(), MessageText.WelcomeCaption(), MessageText.WelcomeMessage(), TryPassword, true, this.Opacity, true);
            passwordDialog.TopMost = true;
            passwordDialog.Text = GetPasswordDialogTitle(MessageText.WelcomeCaption(), IO.UsingCustomFilePath() ? IO.GetCustomFileName() : "");
            passwordDialog.ShowDialog();

            if (passwordDialog.LoadCommandActivated)
            {
                loadObject.OpenOtherPoolRequested = true;
                loadObject.LoadMessage = "Open another Pool";
                unlockedKey?.Dispose();
                return null;
            }

            if (!passwordDialog.AccessGranted || decryptedXml == null)
            {
                loadObject.UnlockCancelled = true;
                loadObject.LoadMessage = "The Pool was not unlocked (wrong password or damaged file).";
                unlockedKey?.Dispose();
                return null;
            }

            SetCurrentKey(unlockedKey);
            AccessGranted = true;
            return decryptedXml;
        }

        /// <summary>
        /// Ask for a new password and make it the pool key. Returns false if the user cancelled.
        /// The pool is NOT saved here.
        /// </summary>
        private bool AskForNewPoolPassword(string welcomeMessage, string formCaption)
        {
            CreatePassword createPassword = new CreatePassword(welcomeMessage, formCaption);
            createPassword.TopMost = true;
            createPassword.ShowDialog();

            if (string.IsNullOrEmpty(createPassword.AuthPassword))
            {
                return false;
            }

            Cursor previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SetCurrentKey(PoolKey.CreateNew(createPassword.AuthPassword));
            }
            finally
            {
                Cursor.Current = previousCursor;
            }
            Log.LogCreatePassword();
            return true;
        }

        // Change the password of the open pool (re-encrypts the pool with a new salt and key)
        private void ChangePoolPassword(string completedMessage, string welcomeMessage, string formCaption)
        {
            PoolKey previousKey = CurrentKey;
            CreatePassword createPassword = new CreatePassword(welcomeMessage, formCaption);
            createPassword.TopMost = true;
            createPassword.ShowDialog();

            if (string.IsNullOrEmpty(createPassword.AuthPassword))
            {
                AhoySimpleMessageBox cancelled = new AhoySimpleMessageBox("Password not changed:\nUsing previous password...", MessageText.PasswordCaption(), false);
                cancelled.TopMost = true;
                cancelled.ShowDialog();
                return;
            }

            CurrentKey = PoolKey.CreateNew(createPassword.AuthPassword);
            SetPoolHasChanges();
            SavePool(false, true, false, "");

            if (IO.VerifySavedPool(CurrentKey))
            {
                previousKey?.Dispose();
                Log.LogCreatePassword();
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(completedMessage, MessageText.PasswordCaption(), false);
                messageBox.TopMost = true;
                messageBox.ShowDialog();
            }
            else
            {
                // Saving failed: keep using the old key so the pool on disk stays readable
                CurrentKey.Dispose();
                CurrentKey = previousKey;
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox("Unable to save the Pool with the new password.\nThe previous password is still in use.", MessageText.ErrorCaption(), false);
                messageBox.TopMost = true;
                messageBox.ShowDialog();
            }
        }

        /// <summary>
        /// First start after updating from Ahoy 1.x: the pool was read with the old built-in key.
        /// Confirm the old password, have the user create a new one (with a clear warning that it
        /// cannot be recovered), save in the new format, verify it, and offer to delete old copies.
        /// Returns false if the user cancelled; the old file is then left untouched.
        /// </summary>
        private bool UpgradeLegacyPool(Load loadedObject)
        {
            Log.LogPreloadStatus("Legacy Pool format detected");

            // 1. Confirm it is the owner (Ahoy 1.x stored the password inside the pool)
            if (!string.IsNullOrEmpty(loadedObject.LegacyPassword))
            {
                Password oldPasswordDialog = new Password(MessageText.EnterPasswordCaption(), MessageText.UpgradeCaption(), MessageText.UpgradeEnterOldPasswordMessage(),
                    password => string.Equals(password, loadedObject.LegacyPassword, StringComparison.Ordinal), true, this.Opacity, false);
                oldPasswordDialog.TopMost = true;
                oldPasswordDialog.ShowDialog();
                if (!oldPasswordDialog.AccessGranted)
                {
                    return false;
                }
            }

            // 2. New password, with the encryption notice and no-recovery warning
            if (!AskForNewPoolPassword(MessageText.UpgradeCreatePasswordMessage(), MessageText.UpgradeCaption()))
            {
                AhoySimpleMessageBox notUpgraded = new AhoySimpleMessageBox(MessageText.UpgradeCancelledMessage(), MessageText.UpgradeCaption(), false);
                notUpgraded.TopMost = true;
                notUpgraded.ShowDialog();
                return false;
            }

            // 3. Save in the new format (pool + backups) and read it back to be sure
            AccessGranted = true;
            SetPoolHasChanges();
            SavePool(false, true, false, "");
            if (!IO.VerifySavedPool(CurrentKey))
            {
                AhoySimpleMessageBox failed = new AhoySimpleMessageBox(MessageText.UpgradeFailedMessage(), MessageText.ErrorCaption(), false);
                failed.TopMost = true;
                failed.ShowDialog();
                return false;
            }
            Log.LogPreloadStatus("Pool upgraded to " + PoolFile.AlgorithmDescription);

            // 4. Old-format copies (backups) are readable by anyone with Ahoy 1.x: offer to delete them
            OfferToDeleteLegacyFiles();

            AhoySimpleMessageBox done = new AhoySimpleMessageBox(MessageText.UpgradeCompletedMessage(), MessageText.UpgradeCaption(), false);
            done.TopMost = true;
            done.ShowDialog();
            return true;
        }

        private void OfferToDeleteLegacyFiles()
        {
            List<string> legacyFiles = IO.FindLegacyPoolFiles();
            if (legacyFiles.Count == 0)
            {
                return;
            }

            AhoyAnswerMessageBox question = new AhoyAnswerMessageBox(MessageText.UpgradeDeleteLegacyFilesQuestion(legacyFiles), MessageText.UpgradeCaption(), false, false);
            question.TopMost = true;
            question.ShowDialog();
            if (!question.Accepted)
            {
                return;
            }

            List<string> notDeleted = new List<string>();
            foreach (string file in legacyFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    notDeleted.Add(file);
                }
            }

            if (notDeleted.Count > 0)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("These files could not be deleted:\n\n" + string.Join("\n", notDeleted), MessageText.UpgradeCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region INTERFACE
        // Display a selection of text symbols to play with
        private void SelectDummyTextVersion(int index)
        {
            // Cycle between guides
            if (index >= 0 && index <= 2)
            {
                Log.LogHomeScreenSelection();
                DisplayTextCycle = index;
                RefreshDummyText();
                RefreshDummyEntry();
                SettingsHasChanges = true;
                ResolveHomeScreenSelectionMenu();
            }
        }

        private void FlipDummyTextVersion()
        {
            // Cycle between guides
            if (DisplayTextCycle < 2)
            {
                DisplayTextCycle += 1;
            }
            else
            {
                DisplayTextCycle = 0;
            }

            RefreshDummyText();
            RefreshDummyEntry();
            SettingsHasChanges = true;
        }

        private void RefreshDummyEntry()
        {
            // Update current entry, if dummy
            if (CurrentEntry != null && CurrentEntry.IsDummyEntry)
            {
                SetDefaultText();
            }
        }

        // Dummy text

        private void RefreshDummyText()
        {
            try
            {
                LoadDummyText();
                RefreshDummyEntry();
            }
            catch
            {
                // Do nothing
            }
        }

        private string DummyText { get; set; }
        private void LoadDummyText()
        {
            DummyText = "";
            DummyText = IO.ReadDummyText(true);
        }

        // Used when changing autosave, pool read/write and remote backup
        private void UpdateDummyTextAndSetHomeScreen()
        {
            // Always load dummy text so that it is prepared
            // to when the home screen is shown
            LoadDummyText();

            // If dummy text is displayed update it
            if (CurrentEntryIsDummy())
            {
                // Set text
                SetDefaultText();
            }
        }

        // Quickguide
        private string QuickGuideText { get; set; }
        private void RefreshQuickGuide()
        {
            try
            {
                QuickGuideText = "";
                QuickGuideText = IO.ReadQuickGuideText();
                RefreshDummyEntry();
            }
            catch
            {
                // Do nothing
            }
        }

        // Quickguide
        private void DisplayQuickGuideInMessageBox()
        {
            if (!string.IsNullOrEmpty(QuickGuideText) && QuickGuideText.Length > 0)
            {
                AhoySearchMessageBox messageBox = new AhoySearchMessageBox(QuickGuideText, MessageText.QuickGuideCaption(), false, false, this.Opacity);
                messageBox.SetSize(800, 700);
                messageBox.TopMost = true;
                messageBox.Show();
            }
        }

        // Style Picker
        private void DisplayStyleEditor()
        {
            KeepAwake();
            Log.StyleEditor();
            StylePicker picker = new StylePicker();
            picker.TopMost = true;
            picker.ShowDialog();
        }

        private void OpenThemeSelector(bool advanced)
        {
            KeepAwake();
            Log.ThemeSelector();
            ThemeSelector selector = new ThemeSelector(advanced);
            selector.TopMost = true;
            selector.ShowDialog();
        }

        // Insert a selection of text symbols to play with
        private void ListShellTools()
        {
            // Reset content search
            ResetContentSearchVariables();
            // Set the navigation buttons to start over
            RewindNavigator();

            // Save current before creating dummy
            if (SaveCurrentEntry(!AutoSaveEntryChanges))
            {
                // Create dummy entry
                CreateDummyEntry(5);
                tbSearch.Clear();
                listBoxSearchResult.Items.Clear();
                listBoxPinned.ClearSelected();
                cbPinned.Checked = false;
                cbProtect.Checked = false;
                pinToolStripMenuItem.Checked = false;
                protectToolStripMenuItem.Checked = false;
                // Update hold buttons
                UpdateHoldButtons();
                tbContent.Focus();
            }
        }

        private string DefaultSymbols2 = "";
        private void SetSheellToolsText()
        {
            DefaultSymbols2 = IO.CommandReferenceReader();
            tbTitle.Text = "";
            tbContent.SuspendLayout();
            ResetContentFieldFormatting();
            tbContent.ResetHistory();
            tbContent.SetText(DefaultSymbols2, false);
            tbContent.ResumeLayout();
        }

        // Call this to display all entry titles
        private void ListEntries()
        {
            if (EntriesExist())
            {
                // Reset content search
                ResetContentSearchVariables();
                // Set the navigation buttons to start over
                RewindNavigator();

                // Save current before creating dummy
                if (SaveCurrentEntry(!AutoSaveEntryChanges))
                {
                    // Create dummy entry
                    CreateDummyEntry(6);
                    tbSearch.Clear();
                    listBoxSearchResult.Items.Clear();
                    listBoxPinned.ClearSelected();
                    cbPinned.Checked = false;
                    cbProtect.Checked = false;
                    pinToolStripMenuItem.Checked = false;
                    protectToolStripMenuItem.Checked = false;
                    // Update hold buttons
                    UpdateHoldButtons();
                    tbContent.Focus();
                }
            }
        }

        private string tempAllTitles;
        private void OutputAllEntryTitles()
        {
            if (EntriesExist())
            {
                tempAllTitles = "Created/Title (" + Entries.Count.ToString() + " entries)" + Environment.NewLine + Environment.NewLine;
                for (int i = 0; i < Entries.Count; i++)
                {
                    tempAllTitles += Entries[i].Created + " " + Entries[i].Title + Environment.NewLine;
                }
                tbTitle.Text = "";
                tbContent.SuspendLayout();
                ResetContentFieldFormatting();
                tbContent.ResetHistory();
                tbContent.SetText(tempAllTitles, false);
                tbContent.ResumeLayout();
            }
        }

        // Insert text log
        private void DisplayLog()
        {
            // Reset content search
            ResetContentSearchVariables();
            // Set the navigation buttons to start over
            RewindNavigator();

            // Save current before creating dummy
            if (SaveCurrentEntry(!AutoSaveEntryChanges))
            {
                // Create dummy entry
                CreateDummyEntry(3);
                tbSearch.Clear();
                listBoxSearchResult.Items.Clear();
                listBoxPinned.ClearSelected();
                cbPinned.Checked = false;
                cbProtect.Checked = false;
                pinToolStripMenuItem.Checked = false;
                protectToolStripMenuItem.Checked = false;
                // Update hold buttons
                UpdateHoldButtons();

                // Move cursor end of text
                tbContent.Focus();
                tbContent.Select(tbContent.Text.Length, 0);
                tbContent.ScrollToCaret();
            }
        }

        private void SetCustomDisplayText()
        {
            if (!CurrentEntryIsDummy() && tbContent.Text.Length > 0)
            {
                if (SimplePromptForPassword(MessageText.DisplayTextCaption(), MessageText.ConfirmDisplayTextTitle(), false))
                {
                    Log.LogHomeScreenTextChanged();
                    CustomDisplayText = tbContent.Text;
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.DisplayTextSetMessage(), MessageText.DisplayTextCaption(), false);
                    msg.ShowDialog();
                    SetPoolHasChanges();
                }

                ResolveHomeScreenSelectionMenu();
            }
            else
            {
                if (tbContent.Text.Length == 0)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoTextAvailableMessage(), MessageText.DisplayTextCaption(), false);
                    msg.ShowDialog();
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.DisplayTextCaption(), false);
                    msg.ShowDialog();
                }

            }
        }

        private void RemoveCustomDisplayText()
        {
            if (CustomDisplayText.Length > 0)
            {
                AhoyAnswerMessageBox answerMsg = new AhoyAnswerMessageBox(MessageText.ClearDisplayTextQuestion(), MessageText.DisplayTextCaption(), false, false);
                answerMsg.ShowDialog();
                if (answerMsg.Accepted)
                {
                    Log.LogHomeScreenTextChanged();
                    CustomDisplayText = "";
                    ClearDisplays(false);
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.DisplayTextResetMessage(), MessageText.DisplayTextCaption(), false);
                    msg.ShowDialog();
                }

                ResolveHomeScreenSelectionMenu();
            }
            else
            {
                layoutAANSIToolStripMenuItem_Click(this, null);
            }
        }

        // Timestamp
        // (\w)(?:[01]?[0-9]|2[0-3]):[0-5][0-9]
        // Timestamp + weeknumber
        // (\w)(?:[01]?[0-9]|2[0-3]):[0-5][0-9] (\w)([0-5][0-9])
        // Timestamp, weekday and number
        // (\w)(?:[01]?[0-9]|2[0-3]):[0-5][0-9] (\w{3}) (\w)([0-5][0-9])
        // Timestamp etc. without preceeding "t"
        // (?:[01]?[0-9]|2[0-3]):[0-5][0-9] (\w{3}) (\w)([0-5][0-9])
        // Timestamp matching: 00:00 ABC 00 ABC 0000 W00 (19:23 FEB 23 SUN 2020 W08)
        // (?:[01]?[0-9]|2[0-3]):[0-5][0-9] (\w{3}) ([0-3][0-9]) (\w{3}) \d{4} (\w)([0-5][0-9])
        private const string ClockTag = @"(?:[01]?[0-9]|2[0-3]):[0-5][0-9] (\w{3}) ([0-3][0-9]) (\w{3}) \d{4} (\w)([0-5][0-9])";
        private static Match clockMatch;
        private static string PreviousClock = "";
        private void UpdateClockDisplay()
        {
            if (!InterfaceLock && CurrentEntryIsDummy())
            {
                if (lbHomeScreen != null && !string.IsNullOrEmpty(lbHomeScreen.Text))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(PreviousClock) || GetClockFormat() != PreviousClock)
                        {
                            clockMatch = Regex.Match(lbHomeScreen.Text, ClockTag, RegexOptions.IgnoreCase);
                            if (clockMatch.Success)
                            {
                                if (ShowClock)
                                {
                                    PreviousClock = GetClockFormat();
                                    lbHomeScreen.Text = lbHomeScreen.Text.Replace(clockMatch.Groups[0].Value, PreviousClock);
                                }
                                else
                                {
                                    lbHomeScreen.Text = lbHomeScreen.Text.Replace(clockMatch.Groups[0].Value, "");
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Do nothing
                    }
                }
            }
        }

        private void ResetClock()
        {
            PreviousClock = "";
        }

        private string GetClockFormat()
        {
            // 19:23 FEB 23 SUN 2020 W08
            return IO.GetISOTimeNow() + " " +  // 19:23
                GetMonthName() + " " +  // FEB
                DateTime.Now.Day.ToString() + " " + // 23
                GetWeekday() + " " + // SUN
                DateTime.Now.Year + " " + // 2020
                GetIsoWeekNumber(); // W08
        }

        private string GetIsoWeekNumber()
        {
            return "W" + ISOWeek.GetWeekOfYear(DateTime.Now).ToString("00");
        }

        private string GetMonthName()
        {
            return DateTime.Now.ToString("MMMM", new CultureInfo("en-GB")).Substring(0, 3).ToUpper(); // SUN/MON osv.
        }

        private string GetWeekday()
        {
            return DateTime.Now.ToString("dddd", new CultureInfo("en-GB")).Substring(0, 3).ToUpper(); // SUN/MON osv.
        }

        private string DefaultText1 = "";
        private string DefaultText2 = "";
        private void SetDefaultText()
        {
            if (!string.IsNullOrEmpty(CustomDisplayText) && !ForceDefaultDisplayText)
            {
                DefaultText2 = CustomDisplayText;
            }
            else
            {
                ForceDefaultDisplayText = false;
                DefaultText2 = DummyText;
            }

            // Update hold buttons
            UpdateHoldButtons();
            tbTitle.Text = DefaultText1;
            ResetContentFieldFormatting();
            tbContent.ResetHistory();
            ResolveHomeScreenLabel(DefaultText2); // Set homescreen label
            tbContent.Clear();
            ResolveHomeScreenSelectionMenu();
            UpdateClockDisplay();
        }

        private string LogText1 = "";
        private string LogText2 = "";
        private void SetLogText()
        {
            Log.LogShowLog();
            // Read log here
            LogText2 = Log.GetLogForDisplay();
            tbTitle.Text = LogText1;
            tbContent.SuspendLayout();
            ResetContentFieldFormatting();
            tbContent.ResetHistory();
            tbContent.SetText(LogText2, false);
            tbContent.ResumeLayout();
        }

        private void SetNoText()
        {
            tbTitle.Text = "";
            tbContent.SuspendLayout();
            ResetContentFieldFormatting();
            tbContent.ResetHistory();
            tbContent.SetText("", false);
            tbContent.ResumeLayout();
        }

        private void ClearDisplays(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClearDisplays(false);

                // Prevent Ding
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ClearDisplays(bool contentFocusOnCancel)
        {
            // Set status label
            SetStatusText(MessageText.DefaultStatus(GetAdditionalStatusText()));

            // Reset WPM counter
            ResetWPMValues();

            // Toggle pinned/animation by pressing escaping continuously
            if (!CurrentEntry.IsDummyEntry)
            {
                // Do nothing if the screen was just cleared from viewing an entry
                // (before it becomes a dummy entry later in this method)
            }
            else if (CurrentEntry.IsDummyEntry && !ShowPinAndMemoAnimation)
            {
                ShowAnimation();

                // Clear clipboard
                ClearClipboard();
                // Clear search history
                ClearSearchHistoryList();
            }
            else if (CurrentEntry.IsDummyEntry && ShowPinAndMemoAnimation)
            {
                ShowPinned();
            }

            // Set the navigation buttons to start over
            RewindNavigator();

            // Reset content search
            ResetContentSearchVariables();

            // Update Hold Buttons
            UpdateHoldButtons();

            // Hide Notification Selector

            // Activate lists functionality
            ListsActUponMouseMovement = true;

            // Save current before creating dummy
            if (SaveCurrentEntry(!AutoSaveEntryChanges))
            {
                // Create dummy entry
                CreateDummyEntry(1);
                tbSearch.Clear();
                listBoxSearchResult.Items.Clear();

                // Clear pinned selection without deactivating mouse scrolling
                CancelEntrySelectionForHighlight = true;
                listBoxPinned.ClearSelected();
                CancelEntrySelectionForHighlight = false;

                if (listBoxPinned.Items.Count > 0)
                    listBoxPinned.TopIndex = 0; // Scroll listbox to top
                cbPinned.Checked = false;
                cbProtect.Checked = false;
                cbBookmark.Checked = false;
                pinToolStripMenuItem.Checked = false;
                protectToolStripMenuItem.Checked = false;
                bookmarkEntryToolStripMenuItem.Checked = false;
                tbSearch.Focus();
            }
            else
            {
                // Decide if a cancellation should return cursor to content field
                if (contentFocusOnCancel)
                {
                    tbContent.Focus();
                }
                else
                {
                    tbSearch.Focus();
                }
            }

            // Resolve title field colors
            RefreshTitleColor();

            // Update Notification Button

            // Collapse search results
            //CollapseList();
        }

        private void ClearSearchResults()
        {
            tbSearch.Clear();
            listBoxSearchResult.Items.Clear();
            ClearEntrySearchResults();
            tbSearch.Focus();
        }

        private void ClearContentSearchField()
        {
            if (tbSearchInContent.Text.Length > 0)
            {
                tbSearchInContent.Text = "";
            }
        }

        // Position the navigator at the beginning
        private void RewindNavigator()
        {
            Navigator.RewindNavigator(btnBack, btnForward);
        }

        private void LockInterface()
        {
            // Log sleep
            Log.LogSleep();
            // Save entry without asking
            SaveCurrentEntry(false);
            // Save pool
            SavePool(false, true, false, "");

            // Hide Pinned if not already hidden
            if (!ShowPinAndMemoAnimation)
            {
                ShowPinAndMemoAnimation = true;
                UpdatePinAndMemoVisuals();
            }

            // Hide Utility editor

            // Hide notification selector

            // Hide Stickies
            EnableMasterCloseStickies();

            // Lock Ahoy
            interfaceLockToolStripMenuItem.Text = MessageText.LockedMenuItemText(); // Sl&eep/Lock
            InterfaceLock = true;
            SetInterfaceVisibility(false);
            AccessGranted = false;
            KeepAwake();
        }

        private void SetInterfaceVisibility(bool visible)
        {
            // This will be shown while the others are hidden
            lbInterfaceLocked.Visible = !visible;
            // Hidden controls
            splitContainer1.Visible = visible;
            tbContent.Enabled = visible;
            tbTitle.Enabled = visible;
            tbSearch.Enabled = visible;
            listBoxSearchResult.Enabled = visible;
            listBoxPinned.Enabled = visible;
            btnSearch.Enabled = visible;
            btnClear.Enabled = visible;
            btnNew.Enabled = visible;
            btnHistory.Enabled = visible;
            btnSave.Enabled = visible;
            btnSleep.Enabled = visible;
            btnTogglePinAndMemo.Enabled = visible;
            btnToggleMemo.Enabled = visible;
            btnTogglePinned.Enabled = visible;
            btnClose.Enabled = visible;
            cbPinned.Enabled = visible;
            cbBookmark.Enabled = visible;
            newToolStripMenuItem.Enabled = visible;
            deleteToolStripMenuItem.Enabled = visible;
            locatePoolToolStripMenuItem.Enabled = visible;
            findToolStripMenuItem.Enabled = visible;
            selectTitleToolStripMenuItem.Enabled = visible;
            selectContentToolStripMenuItem.Enabled = visible;
            displayInfoToolStripMenuItem.Enabled = visible;
            viewInfoToolStripMenuItem1.Enabled = visible;
            saveEntryToolStripMenuItem.Enabled = visible;
            findAllToolStripMenuItem.Enabled = visible;
            findInContentToolStripMenuItem.Enabled = visible;
            changePasswordToolStripMenuItem.Enabled = visible;
            showHistoryToolStripMenuItem.Enabled = visible;
            clearAllSettingsToolStripMenuItem.Enabled = visible;
            sleepIntervalToolStripMenuItem.Enabled = visible;
            selectPinnedByNumberToolStripMenuItem.Enabled = visible;
            wordWrapToolStripMenuItem.Enabled = visible;
            clearDisplaysToolStripMenuItem.Enabled = visible;
            savePoolToolStripMenuItem.Enabled = visible;
            gettingStartedToolStripMenuItem.Enabled = visible;
            reloadPoolToolStripMenuItem.Enabled = visible;
            poolInformationToolStripMenuItem.Enabled = visible;
            movePinnedUpToolStripMenuItem.Enabled = visible;
            movePinnedDownToolStripMenuItem.Enabled = visible;
            movePinnedToTopToolStripMenuItem.Enabled = visible;
            movePinnedToBottomToolStripMenuItem.Enabled = visible;
            changePinnedColorToolStripMenuItem.Enabled = visible;
            holdEntryToolStripMenuItem.Enabled = visible;
            holdEntry2ToolStripMenuItem.Enabled = visible;
            clearHoldButtonsToolStripMenuItem.Enabled = visible;
            btnBack.Enabled = visible;
            btnForward.Enabled = visible;
            goBackToolStripMenuItem.Enabled = visible;
            goForwardToolStripMenuItem.Enabled = visible;
            commandInputToolStripMenuItem.Enabled = visible;
            closeEntryToolStripMenuItem.Enabled = visible;
            showPinnedToolStripMenuItem.Enabled = visible;
            textToolStripMenuItem.Enabled = visible;
            restoreDefaultToolStripMenuItem.Enabled = visible;
            showLogToolStripMenuItem.Enabled = visible;
            setDisplayTextToolStripMenuItem.Enabled = visible;
            clearDisplayTextToolStripMenuItem.Enabled = visible;
            spellCheckToolStripMenuItem1.Enabled = visible;
            printEntryToolStripMenuItem.Enabled = visible;
            switchPoolToolStripMenuItem.Enabled = visible;
            showMemoToolStripMenuItem.Enabled = visible;
            openEmptySpellCheckerToolStripMenuItem.Enabled = visible;
            autoHideListsToolStripMenuItem.Enabled = visible;
            loadBackupToolStripMenuItem.Enabled = visible;
            colorThemesToolStripMenuItem.Enabled = visible;
            detectURLsToolStripMenuItem.Enabled = visible;
            duplicateEntryToolStripMenuItem1.Enabled = visible;
            autoSaveEntryToolStripMenuItem.Enabled = visible;
            openToolStripMenuItem.Enabled = visible;
            saveAsToolStripMenuItem.Enabled = visible;
            readOnlyToolStripMenuItem.Enabled = visible;
            autoSearchToolStripMenuItem.Enabled = visible;
            toggleDemoModeToolStripMenuItem.Enabled = visible;
            paragraphDividersToolStripMenuItem.Enabled = visible;
            fontToolStripMenuItem.Enabled = visible;
            markForExportToolStripMenuItem.Enabled = visible;
            importExportToolStripMenuItem.Enabled = visible;
            markAllForExportToolStripMenuItem.Enabled = visible;
            fontWeightToolStripMenuItem.Enabled = visible;
            enforceScrollbarsToolStripMenuItem.Enabled = visible;
            titleTabToContentToolStripMenuItem.Enabled = visible;
            autoAddBlankLinesToolStripMenuItem.Enabled = visible;
            findAndReplaceToolStripMenuItem.Enabled = visible;
            advancedToolStripMenuItem.Enabled = visible;
            opacityToolStripMenuItem.Enabled = visible;
            //checkForUpdatesToolStripMenuItem.Enabled = visible;
            showClockToolStripMenuItem.Enabled = visible;
            globalUndoToolStripMenuItem.Enabled = visible;
            newPinnedByClickToolStripMenuItem.Enabled = visible;
            hidePinnedByClickToolStripMenuItem.Enabled = visible;

            // Hide/show certain menu items
            editToolStripMenuItem.Enabled = visible;
            viewToolStripMenuItem.Enabled = visible;
            toolsToolStripMenuItem.Enabled = visible;
            editToolStripMenuItem.Visible = visible;
            viewToolStripMenuItem.Visible = visible;
            toolsToolStripMenuItem.Visible = visible;
            copyPoolToClipboardToolStripMenuItem.Visible = visible;
            setAlternateBackupToolStripMenuItem.Enabled = visible;
            openAlternateRepositoryToolStripMenuItem.Enabled = visible;
            clearAlternateBackupToolStripMenuItem.Enabled = visible;
            pinToolStripMenuItem.Enabled = visible;
            protectToolStripMenuItem.Enabled = visible;
            saveAsTextDocumentToolStripMenuItem.Enabled = visible;
            wordAndWPMCounterToolStripMenuItem.Enabled = visible;
            newPinnedEntryToolStripMenuItem.Enabled = visible;
            titlePrioritySearchToolStripMenuItem.Enabled = visible;
            liveSearchToolStripMenuItem.Enabled = visible;
            keyboardControlToolStripMenuItem.Enabled = visible;

            // General above ^^

            // Hide/show Typewriter icon
            // Hide/show Sticky icons (New sticky and bring to front icon)
            pnlSpawnSticky.Enabled = visible;
            pnlSpawnSticky.Visible = visible;
            pnlShowAllStickies.Enabled = visible;
            pnlShowAllStickies.Visible = visible;
            stickyToolStripMenuItem.Enabled = visible;
            stickyToolStripMenuItem.Visible = visible;
            stickyOptionsToolStripMenuItem.Enabled = visible;
            stickyOptionsToolStripMenuItem.Visible = visible;


            // Enable/disable Screen Blocker menu item

            // Enable the exit full screen option while locked to enable quick
            // exit of full screen mode
            if (InterfaceLock && IsFullScreen)
            {
                fullScreenToolStripMenuItem.Enabled = true;
            }
            else
            {
                fullScreenToolStripMenuItem.Enabled = visible;
            }
        }

        private void ToggleInterfaceLock()
        {
            if (!InterfaceLock)
            {
                LockInterface();
            }
            else
            {
                // Take password to unlock interface
                if (IO.UsingCustomFilePath())
                {
                    PromptForPassword(MessageText.WelcomeCaption(), "", IO.GetCustomFileName());
                }
                else
                {
                    PromptForPassword(MessageText.WelcomeCaption(), "", "");
                }

                if (AccessGranted)
                {
                    Log.LogWakeup();

                    // Show Utility editor

                    // Show Stickies
                    ShowStickies();

                    interfaceLockToolStripMenuItem.Text = MessageText.UnLockedMenuItemText(); // &Wakeup/Unlock
                    KeepAwake();
                    InterfaceLock = false;
                    SetInterfaceVisibility(true);
                    this.BringToFront();
                    this.Focus();
                }
            }

            if (!InterfaceLock)
            {
                // Set focus after waking up if not a dummy entry
                if (ReturnToEditorAfterSleep || !CurrentEntry.IsDummyEntry)
                {
                    tbContent.Focus();
                }
                else
                {
                    tbSearch.Focus();
                }

                // Reset activity
                KeepAwake();
            }
        }
        #endregion

        #region POOL SEARCH
        internal static Entry GetEntryByID(string ID)
        {
            foreach (Entry entry in Entries)
            {
                if (entry.ID == ID)
                {
                    return entry;
                }
            }

            // If non was found return null
            return null;
        }

        private Entry GetMirrorEntryByID(string ID)
        {
            foreach (Entry entry in EntriesMirror)
            {
                if (entry.ID == ID)
                {
                    return entry;
                }
            }

            // If non was found return null
            return null;
        }


        // Select search result by index
        private void SelectSearchResult(int index)
        {
            if (listBoxSearchResult.Items.Count > 0 && index <= listBoxSearchResult.Items.Count - 1)
            {
                listBoxSearchResult.SetSelected(index, true);
                // Add to search history when selecting from the search results
                if (!string.IsNullOrEmpty(tbSearch.Text) && tbSearch.Text.Length > 1)
                {
                    AddToSearchHistoryList(tbSearch.Text.Trim());
                }
            }
        }

        // Search (clear entry search results)
        private void ClearEntrySearchResults()
        {
            if (Entries != null && Entries.Count > 0)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    Entries[i].SearchResult = "";
                }
            }
        }

        private bool DoesSearchResultExist(List<Entry> SearchResult, string ID)
        {
            if (SearchResult != null && SearchResult.Count > 0)
            {
                foreach (Entry e in SearchResult)
                {
                    if (e.ID == ID)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private string GetTitlePriorityString()
        {
            return TextTransform.GetOnOff(TitlePrioritySearch);
        }

        // Search
        private List<Entry> SearchCollection { get; set; }
        private List<Entry> SearchResultA { get; set; }
        private List<Entry> SearchResultB { get; set; }
        private List<Entry> SearchResultC { get; set; }
        private bool SearchSuccess { get; set; }
        private bool SearchEntriesFound { get; set; }
        private int SearchedHitCount { get; set; }
        private int SearchedHitTarget { get; set; }
        private string tempCombinedText;
        private string tempSearchResult;
        private void PerformSearch(string searchQuery, bool isLiveSearch)
        {
            ClearEntrySearchResults();
            ResetSearchResultSelectionIndex();
            tempCombinedText = "";
            tempSearchResult = "";
            SearchEntriesFound = false;
            // Continue if Search string contains any characters
            if ((Entries != null && Entries.Count > 0) && (tbSearch.Text.Length > 0))
            {
                if (SearchCollection == null)
                {
                    SearchCollection = new List<Entry>();
                    SearchResultA = new List<Entry>();
                    SearchResultB = new List<Entry>();
                    SearchResultC = new List<Entry>();
                }

                SearchCollection.Clear();
                SearchResultA.Clear();
                SearchResultB.Clear();
                SearchResultC.Clear();

                // Initialize search algorithm
                StringSearch.InitializeSearchPatterns(searchQuery);
                // Create array with query strings
                string[] SearchedHitTargetArr = searchQuery.Split(' ');
                SearchedHitTarget = SearchedHitTargetArr.Length;

                // If title has priority search titles before searching content
                if (TitlePrioritySearch)
                {
                    // FIRST: Search titles only and add them to SearchResultA
                    foreach (Entry entry in Entries)
                    {
                        SearchSuccess = false;
                        SearchedHitCount = 0;
                        // Prepare title string
                        tempCombinedText = entry.Title.Trim().ToLower();

                        // If hit count reaches number of search strings it is a match
                        foreach (string s in SearchedHitTargetArr)
                        {
                            // Search inputting strings trimmed with lowercasing
                            if (StringSearch.IsMatchA(tempCombinedText, s.Trim().ToLower()))
                            {
                                tempSearchResult = StringSearch.MatchingSearchPattern;
                                SearchedHitCount += 1;
                            }

                            if (SearchedHitCount >= SearchedHitTarget)
                            {
                                SearchSuccess = true;
                                break;
                            }
                        }

                        if (SearchSuccess)
                        {
                            entry.SearchResult = tempSearchResult;
                            SearchResultA.Add(entry);
                        }
                    }

                    // SECONDLY: Search content and notifications, and add them to SearchResultA if empty, or SearchResultB if SearchResultA has results
                    foreach (Entry entry in Entries)
                    {
                        if (!DoesSearchResultExist(SearchResultA, entry.ID))
                        {
                            SearchSuccess = false;
                            SearchedHitCount = 0;
                            // Construct searchable content, trimmed with lowercasing
                            tempCombinedText = entry.Content.Trim().ToLower();

                            // If hit count reaches number of search strings it is a match
                            foreach (string s in SearchedHitTargetArr)
                            {
                                // Search inputting strings trimmed with lowercasing
                                if (StringSearch.IsMatchA(tempCombinedText, s.Trim().ToLower()))
                                {
                                    tempSearchResult = StringSearch.MatchingSearchPattern;
                                    SearchedHitCount += 1;
                                }

                                if (SearchedHitCount >= SearchedHitTarget)
                                {
                                    SearchSuccess = true;
                                    break;
                                }
                            }

                            if (SearchSuccess)
                            {
                                entry.SearchResult = tempSearchResult;
                                SearchResultB.Add(entry);
                            }
                        }
                    }
                }
                else
                {
                    // Search title, content and notification text
                    foreach (Entry entry in Entries)
                    {
                        SearchSuccess = false;
                        SearchedHitCount = 0;
                        // Construct searchable content, trimmed with lowercasing
                        tempCombinedText = entry.Title.Trim().ToLower() + " " + entry.Content.Trim().ToLower();

                        // If hit count reaches number of search strings it is a match
                        foreach (string s in SearchedHitTargetArr)
                        {
                            // Search inputting strings trimmed with lowercasing
                            if (StringSearch.IsMatchA(tempCombinedText, s.Trim().ToLower()))
                            {
                                tempSearchResult = StringSearch.MatchingSearchPattern;
                                SearchedHitCount += 1;
                            }

                            if (SearchedHitCount >= SearchedHitTarget)
                            {
                                SearchSuccess = true;
                                break;
                            }
                        }

                        if (SearchSuccess)
                        {
                            entry.SearchResult = tempSearchResult;
                            SearchResultA.Add(entry);
                        }
                    }
                }

                // If no entries was found in the primary search use the secondary search
                if (SearchResultA.Count == 0 && SearchResultB.Count == 0 && !isLiveSearch)
                {
                    foreach (Entry entry in Entries)
                    {
                        SearchSuccess = false;
                        SearchedHitCount = 0;
                        // Construct searchable content, trimmed with lowercasing
                        tempCombinedText = entry.Title.Trim().ToLower() + " " + entry.Content.Trim().ToLower();

                        // If hit count reaches number of search strings it is a match
                        foreach (string s in SearchedHitTargetArr)
                        {
                            // Search inputting strings trimmed with lowercasing
                            if (StringSearch.IsMatchB(tempCombinedText, s.Trim().ToLower()))
                            {
                                SearchedHitCount += 1;
                            }

                            if (SearchedHitCount >= SearchedHitTarget)
                            {
                                SearchSuccess = true;
                                break;
                            }
                        }

                        if (SearchSuccess)
                        {
                            entry.SearchResult = StringSearch.MatchingSearchPattern;
                            SearchResultC.Add(entry);
                        }
                    }
                }

                // Add collections A, B and C to the search collection
                // ------------- RESULT "A" ---------------
                if (SearchResultA.Count > 0)
                {
                    // Sort listst according to edited date before adding to the collection
                    try
                    {
                        SearchResultA.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                    }
                    catch
                    {
                        // Log sorting error due to DateTime issues
                        Log.LogGeneralError("Sorting list failed");
                    }
                    for (int i = 0; i < SearchResultA.Count; i++)
                    {
                        SearchCollection.Add(SearchResultA[i]);
                    }

                    IsSecondaryMatch = false;
                }
                // ------------- RESULT "B" ---------------
                if (SearchResultB.Count > 0)
                {
                    // Sort listst according to edited date before adding to the collection
                    try
                    {
                        SearchResultB.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                    }
                    catch
                    {
                        // Log sorting error due to DateTime issues
                        Log.LogGeneralError("Sorting list failed");
                    }
                    for (int i = 0; i < SearchResultB.Count; i++)
                    {
                        SearchCollection.Add(SearchResultB[i]);
                    }

                    IsSecondaryMatch = false;
                }

                if (isLiveSearch)
                {
                    SetStatusText("Found: " + SearchCollection.Count.ToString());
                }
                else if (SearchResultA.Count > 0 || SearchResultB.Count > 0)
                {
                    SetStatusText("Primary matches found: " + SearchCollection.Count.ToString());
                }

                // ------------- RESULT "C" ---------------
                if (SearchResultC.Count > 0)
                {
                    // Sort listst according to edited date before adding to the collection
                    try
                    {
                        SearchResultC.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                    }
                    catch
                    {
                        // Log sorting error due to DateTime issues
                        Log.LogGeneralError("Sorting list failed");
                    }
                    for (int i = 0; i < SearchResultC.Count; i++)
                    {
                        SearchCollection.Add(SearchResultC[i]);
                    }

                    IsSecondaryMatch = true;
                    SetStatusText("Secondary matches found: " + SearchCollection.Count.ToString());
                }

                // If the search resulted in more than zero entries, populate the listbox
                if (SearchCollection.Count > 0)
                {
                    RemoveNoLiveSearchResultsLabel(); // Remove label immediately if displayed
                    SearchEntriesFound = true;
                    listBoxSearchResult.Items.Clear();
                    PopulateSearchListBox(listBoxSearchResult, SearchCollection);
                    Log.LogSearch("\"" + tbSearch.Text + "\"" + " (Found: " + SearchCollection.Count.ToString() + ")", false, isLiveSearch);
                }
                else if (SearchCollection.Count == 0 && listBoxSearchResult.Items.Count > 0)
                {
                    // Clear old search results if any, if result is empty
                    listBoxSearchResult.Items.Clear();
                    if (!isLiveSearch)
                    {
                        SetStatusText(MessageText.EmptySearchMessage());
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                    }
                    else
                    {
                        NoLiveSearchResult();
                    }
                }
                else if (SearchCollection.Count == 0 && listBoxSearchResult.Items.Count == 0)
                {
                    if (!isLiveSearch)
                    {
                        SetStatusText(MessageText.EmptySearchMessage());
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                    }
                    else
                    {
                        NoLiveSearchResult();
                    }
                }
            }
            else if (LiveSearch)
            {
                NoLiveSearchResult();
            }

            // Only select content if not performing live search
            if (!SearchEntriesFound && !LiveSearch)
            {
                tbSearch.SelectAll();
            }

            tbSearch.Focus();
        }

        private void FindAllEntries()
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            // Continue if entries exist
            if (Entries != null && Entries.Count > 0)
            {
                Log.LogSearch("(Find All)", false, false);
                // Ask to continue number of entries exceeds 10000
                if (Entries.Count >= 10000)
                {
                    AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.LargeEntrySearchQuestion(Entries.Count.ToString()), MessageText.SearchCaption(), false, false);
                    ahoyAnswerMessageBox.ShowDialog();

                    if (!ahoyAnswerMessageBox.Accepted)
                    {
                        tbSearch.Focus();
                        return;
                    }
                }

                listBoxSearchResult.Items.Clear();
                PopulateSearchListBox(listBoxSearchResult, Entries);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }

            tbSearch.Clear();
            tbSearch.Focus();
        }




        // Find entry-range
        private List<Entry> tempFindSizedEntries;
        private void FindEntryRangeCount(int from, int to)
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            // Continue if entries exist
            if (Entries != null && Entries.Count > 0)
            {
                Log.LogSearch("(Find entries from " + from.ToString() + "to" + to.ToString() + ")", false, false);
                if (tempFindSizedEntries == null)
                {
                    tempFindSizedEntries = new List<Entry>();
                }
                tempFindSizedEntries.Clear();
                foreach (Entry e in Entries)
                {
                    if (e.Content.Length >= from && e.Content.Length <= to)
                    {
                        tempFindSizedEntries.Add(e);
                    }
                }
                if (tempFindSizedEntries.Count > 0)
                {
                    listBoxSearchResult.Items.Clear();
                    PopulateSearchListBox(listBoxSearchResult, tempFindSizedEntries);
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }
            tbSearch.Clear();
            tbSearch.Focus();
        }

        private void FindEntryRangeCount(int from)
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            // Continue if entries exist
            if (Entries != null && Entries.Count > 0)
            {
                Log.LogSearch("(Find entries from " + from.ToString() + ")", false, false);
                if (tempFindSizedEntries == null)
                {
                    tempFindSizedEntries = new List<Entry>();
                }
                tempFindSizedEntries.Clear();
                foreach (Entry e in Entries)
                {
                    if (e.Content.Length >= from)
                    {
                        tempFindSizedEntries.Add(e);
                    }
                }
                if (tempFindSizedEntries.Count > 0)
                {
                    listBoxSearchResult.Items.Clear();
                    PopulateSearchListBox(listBoxSearchResult, tempFindSizedEntries);
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }
            tbSearch.Clear();
            tbSearch.Focus();
        }


        private List<Entry> UtilitySearchCollection { get; set; }
        private void FindProtectedEntries()
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            UtilitySearchCollection = new List<Entry>();
            foreach (Entry entry in Entries)
            {
                if (entry.Protected)
                {
                    UtilitySearchCollection.Add(entry);
                }
            }

            // Continue if entries exist
            if (UtilitySearchCollection != null && UtilitySearchCollection.Count > 0)
            {
                Log.LogSearch("(Find Locked)", false, false);
                listBoxSearchResult.Items.Clear();
                // Sort list to display the latest edited first
                try
                {
                    UtilitySearchCollection.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                }
                catch
                {
                    // Log sorting error due to DateTime issues
                    Log.LogGeneralError("Sorting list failed");
                }
                PopulateSearchListBox(listBoxSearchResult, UtilitySearchCollection);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }

            tbSearch.Clear();
            tbSearch.Focus();
        }

        private List<Entry> ExportSearchCollection { get; set; }
        private void FindEntriesMarkedForExport()
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            ExportSearchCollection = new List<Entry>();
            foreach (Entry entry in Entries)
            {
                if (entry.MarkExport)
                {
                    ExportSearchCollection.Add(entry);
                }
            }

            // Continue if entries exist
            if (ExportSearchCollection != null && ExportSearchCollection.Count > 0)
            {
                Log.LogSearch("(Find Export)", false, false);
                listBoxSearchResult.Items.Clear();
                // Sort list to display the latest edited first
                try
                {
                    ExportSearchCollection.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                }
                catch
                {
                    // Log sorting error due to DateTime issues
                    Log.LogGeneralError("Sorting list failed");
                }
                PopulateSearchListBox(listBoxSearchResult, ExportSearchCollection);
                ExportCollection();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }

            tbSearch.Clear();
            tbSearch.Focus();
        }

        private List<Entry> HistorySearchCollection { get; set; }
        private void FindHistoryEntries()
        {
            SetDefaultStatusText();
            ResetSearchResultSelectionIndex();
            HistorySearchCollection = new List<Entry>();
            HistorySearchCollection.AddRange(Entries);

            // Continue if entries exist
            if (HistorySearchCollection != null && HistorySearchCollection.Count > 0)
            {
                Log.LogSearch("(History)", false, false);
                listBoxSearchResult.Items.Clear();
                // Sort list to display the latest edited first
                try
                {
                    HistorySearchCollection.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                }
                catch
                {
                    // Log sorting error due to DateTime issues
                    Log.LogGeneralError("Sorting list failed");
                }
                // Truncate the HistorySearchCollection to contain the 100 latest changes
                if (HistorySearchCollection.Count > 100)
                {
                    HistorySearchCollection.RemoveRange(99, HistorySearchCollection.Count - 100);
                }

                PopulateSearchListBox(listBoxSearchResult, HistorySearchCollection);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                msg.ShowDialog();
            }

            tbSearch.Clear();
            tbSearch.Focus();
        }



        // Select search result by F-keys
        private void SelectSearchResultByShortcut(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    SelectSearchResult(0);
                    break;
                case Keys.F2:
                    SelectSearchResult(1);
                    break;
                case Keys.F3:
                    SelectSearchResult(2);
                    break;
                case Keys.F4:
                    SelectSearchResult(3);
                    break;
                case Keys.F5:
                    SelectSearchResult(4);
                    break;
                case Keys.F6:
                    SelectSearchResult(5);
                    break;
                case Keys.F7:
                    SelectSearchResult(6);
                    break;
                case Keys.F8:
                    SelectSearchResult(7);
                    break;
                case Keys.F9:
                    SelectSearchResult(8);
                    break;
                case Keys.F10:
                    SelectSearchResult(9);
                    break;
            }
        }
        #endregion

        #region ENTRY TEXT SEARCH
        int start;
        int indexOfSearchText;
        string contentSearchString = "";

        private void tbSearchInContent_Click(object sender, EventArgs e)
        {
            tbSearchInContent.SelectAll();
        }

        private void tbContentSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (!timerRefocusContentSearch.Enabled)
                {
                    FindContent(tbSearchInContent.Text);
                }

                // Prevent Ding
                e.Handled = true;
            }
            else if ((Keys)e.KeyChar == Keys.Escape)
            {
                tbContent.Focus();

                // Prevent Ding
                e.Handled = true;
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
            tempSource = tbContent.Text.ToLower();

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
                lbContentSearch.ForeColor = AhoyColors.ErrorTextColor();
            }

            // Display the "Next" text if there is more than one result
            if (searchCount > 1)
            {
                btnSearchInContent.Text = "Next";
                btnSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[6];
                this.toolTip1.SetToolTip(btnSearchInContent, "Find Next Match");
            }

            // Display results count for primary and secondary matches
            if (IsSecondaryMatch)
            {
                lbContentSearch.Text = "Secondary matches: " + tempCurrentIndex.ToString() + " of " + searchCount.ToString();
            }
            else
            {
                lbContentSearch.Text = "Found: " + tempCurrentIndex.ToString() + " of " + searchCount.ToString();
            }
        }

        private void ResetContentSearchVariables()
        {
            RestartSearchVariables(); // Reset variables
            tbSearchInContent.Text = ""; // Clear textbox
        }

        private void RestartSearchVariables()
        {
            // On new searches the "start" variable must be reset
            start = 0;
            indexOfSearchText = 0;
            tempCurrentIndex = 0;
            contentSearchString = "";
            lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbContentSearch.Text = "Content Search";
            btnSearchInContent.Text = "Search";
            btnSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[8];
            this.toolTip1.SetToolTip(btnSearchInContent, "Find Content (CTRL+ALT+F)");
        }

        private void timerRefocusContentSearch_Tick(object sender, EventArgs e)
        {
            tbSearchInContent.Text = contentSearchString;
            tbSearchInContent.SelectAll();
            // Must be disabled at the end of the method
            timerRefocusContentSearch.Enabled = false;
        }

        // Search
        private void FindContent(string searchQuery)
        {
            if (searchQuery != null && searchQuery.Length > 0)
            {
                contentSearchString = searchQuery;

                if (!string.IsNullOrEmpty(contentSearchString))
                    Log.LogContentSearch(contentSearchString);

                ResolveSearchCount();

                int startindex = 0;
                if (tbSearchInContent.Text.Length > 0)
                {
                    startindex = FindMyText(tbSearchInContent.Text.Trim(), start, tbContent.Text.Length + 1);
                }

                // If string was found in the RichTextBox, highlight it
                if (startindex >= 0)
                {
                    // Set the highlight color as red (Remember to set the rich textbox to show highlights in its properties)
                    //tbContent.SelectionColor = Color.Red;
                    // Find the end index. End Index = number of characters in textbox
                    int endindex = tbSearchInContent.Text.Length;
                    // Highlight the search string
                    tbContent.Select(startindex, endindex);
                    // mark the start position after the position of
                    // last search string
                    start = startindex + endindex;
                    // Count up current index
                    tempCurrentIndex += 1;
                    UpdateSearchVisuals();
                }

                // Search
                if (indexOfSearchText == -1) // end of search
                {
                    if (!AutoSearchInProgress) // Display messagebox if not called from auto-search (don't change this)
                    {
                        AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EndOfSearchMessage(), MessageText.SearchCaption(), false);
                        messageBox.TopMost = true;
                        messageBox.ShowDialog();
                    }

                    start = 0;
                    indexOfSearchText = 0;
                    tempCurrentIndex = 0;
                }

                // Refocus search field
                timerRefocusContentSearch.Enabled = true;
            }
            else
            {
                tbSearchInContent.Focus();
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
                    indexOfSearchText = tbContent.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
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

        #region MENU EVENTS
        private void findInContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Move Cursor away from lists
            CenterCursor();
            tbSearchInContent.Focus();
        }

        // Create new entry
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewEntry(string.Empty, false, string.Empty, false, false, false);
            AddNewEntryToEditingCategory();
        }

        // Unpin entry // unpinmenu // menuitemunpin
        private void unpinEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnpinCurrentEntry();
        }

        // Duplicate entry from main menu
        private void duplicateEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DuplicateCurrentEntry(true);
        }

        // Duplicate entry
        private void duplicateEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DuplicateCurrentEntry(false);
        }

        // Delete entry
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentEntry();
        }

        // Delete entry
        private void deleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentEntry();
        }

        private void savePoolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveCurrentEntry(false);
        }

        private void savePoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForceSaveEverything();
        }


        // Open current Pool location in explorer
        private void locatePoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPoolRepository(false);
        }

        // Open default directory
        private void openDefaultDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDefaultDirectory();
        }

        private void OpenPoolRepository(bool askFirst)
        {
            if (askFirst)
            {
                AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.LocatePoolQuestion(), MessageText.LocatePoolCaption(), false, false);
                ahoyAnswerMessageBox.ShowDialog();

                if (ahoyAnswerMessageBox.Accepted)
                {
                    OpenPoolFolder();
                }
            }
            else
            {
                OpenPoolFolder();
            }
        }

        private void OpenPoolFolder()
        {
            try
            {
                if (Directory.Exists(IO.GetDataPath()))
                {
                    Log.ProcessStart(IO.GetDataPath());
                    Process.Start(IO.GetDataPath());
                }
                else
                {
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.InvalidFilePathMessage(), MessageText.LocatePoolCaption(), false);
                    messageBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.FilePathErrorMessage(ex.Message), MessageText.LocatePoolCaption(), false);
                messageBox.ShowDialog();
            }
        }

        private void OpenDefaultDirectory()
        {
            try
            {
                if (Directory.Exists(IO.GetDefaultDataPath()))
                {
                    Log.ProcessStart(IO.GetDefaultDataPath());
                    Process.Start(IO.GetDefaultDataPath());
                }
                else
                {
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.InvalidFilePathMessage(), MessageText.LocatePoolCaption(), false);
                    messageBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.FilePathErrorMessage(ex.Message), MessageText.LocatePoolCaption(), false);
                messageBox.ShowDialog();
            }
        }

        // Reload pool
        private void reloadPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadDefaultPool(false, false, true);
        }

        // Exit application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.ExitApplicationQuestion(), MessageText.ExitCaption(), false, false);
            ahoyAnswerMessageBox.ShowDialog();

            if (ahoyAnswerMessageBox.Accepted)
            {
                ExitAhoy();
            }
        }

        private void ExitAhoy()
        {
            SavePool(false, true, false, "");
            Application.Exit();
        }

        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleEntryPinned();
        }

        // pin // pinmenu // Togglepinned // toggle pinned
        private void ToggleEntryPinned()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                cbPinned.Checked = !cbPinned.Checked;
                // If is related pin the master instead by selecting it (only if not already pinned)
                if (Related.IsRelated(CurrentEntry, Entries) && Related.IsMasterPinned(CurrentEntry.ID, Entries))
                {
                    cbPinned.Checked = false;
                    return;
                }
                else
                {
                    ResolveMasterPin(CurrentEntry, cbPinned.Checked);
                }
                // Resolve pinned menu item
                ResolvePinnedMenuItem();
                CurrentEntryHasChanges();
                tbContent.Focus();
                InvokeAutoSaveEntry();
            }
        }

        private void ResolvePinnedMenuItem()
        {
            pinToolStripMenuItem.Checked = cbPinned.Checked;
        }

        private void protectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                cbProtect.Checked = !cbProtect.Checked;
                protectToolStripMenuItem.Checked = cbProtect.Checked;
                CurrentEntryHasChanges();
                SaveCurrentEntry(false);
            }
        }

        private void clearClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearClipboard();
            AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.ClearClipboardMessage(), MessageText.ClipboardCaption(), false);
            messageBox.ShowDialog();
        }

        // Select search field
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Move Cursor away from lists
            CenterCursor();
            tbSearch.Focus();

            if (tbSearch.Text.Length > 0)
            {
                tbSearch.SelectAll();
            }
        }


        // Entry search on size:
        // XS Text: 1-1500
        // S Text: 1500-5000
        // M Text: 5000-25000
        // L Text: 25000-100000
        // XL Text: 100000-500000
        // XXL Text: 500000-1000000
        // XXXL Text: 1000000+


        private void findSXCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(1, 1500);
        }

        private void findSCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(1500, 5000);
        }

        private void findMCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(5000, 25000);
        }

        private void findLCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(25000, 100000);
        }

        private void findXLCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(100000, 500000);
        }

        private void findXXLCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(500000, 1000000);
        }

        private void findXXXLCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindEntryRangeCount(1000000);
        }


        // Find All
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindAllEntries();
        }

        // Find New
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FindHistoryEntries();
        }

        // Find Locked
        private void lockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindProtectedEntries();
        }

        private void showHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Find entries that was changed the last 7 days
            PauseLiveSearch(); // Pause to prevent clearing
            FindHistoryEntries();
        }

        private int SelectPinnedUserInput { get; set; }
        private void selectPinnedByNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectPinnedByNumber(false);
        }

        private void SelectPinnedByNumber(bool transferToUtilityEditor)
        {
            // Ask for pin number
            InputPrompt inputForm = new InputPrompt(MessageText.EnterPinnedNumberCaption(), "", "", MessageText.SelectPinnedTitle(), 20, false, "OK", false, cbPinned.CheckedBackgroundImage, cbPinned.UnCheckedBackgroundImage, false);
            inputForm.ShowDialog();

            // Determine if input string is numeric value
            if (!string.IsNullOrEmpty(inputForm.UserInput))
            {
                if (Regex.IsMatch(inputForm.UserInput.Trim(), @"^\d", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                {
                    try
                    {
                        SelectPinnedUserInput = Convert.ToInt32(inputForm.UserInput.Trim());
                    }
                    catch
                    {
                        return;
                    }
                    SelectPinned(SelectPinnedUserInput - 1);

                    // Utility Editor section
                }
            }
        }

        // Select title field
        private void selectTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CenterCursor();
            tbTitle.Focus();
        }

        // Select content field
        private void selectContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                CenterCursor();
                tbContent.Focus();
            }
        }

        private void displayInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEntryInformation();
        }

        private void viewInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowEntryInformation();
        }

        private void poolInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display pool information also when zero entries
            if (Entries != null)
            {
                Log.LogPoolStatus("Information dialog");
                AboutInformation = "*** POOL INFORMATION ***" + Environment.NewLine + Environment.NewLine;
                try
                {
                    string tempInfoFileName = "Pool name";
                    string tempInfoEntryCount = "Entry count";
                    string tempInfoStickyCount = "Sticky count";
                    string tempInfoAnotifications = "Active notifications";
                    string tempInfoInotifications = "Inactive notifications";
                    string tempInfoTotalChars = "Total character count";
                    string tempInfoLogEntries = "Log entries";
                    string tempInfoPoolChanged = "Pool changed";
                    string tempInfoPoolSize = "Pool size";

                    AboutInformation += tempInfoFileName + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoFileName, 25, ".") + " " + IO.GetCurrentPoolFileNameForDisplay(false) + Environment.NewLine;
                    AboutInformation += tempInfoEntryCount + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoEntryCount, 25, ".") + " " + Entries.Count.ToString() + Environment.NewLine;
                    AboutInformation += tempInfoStickyCount + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoStickyCount, 25, ".") + " " + StickySettingsList.Count.ToString() + Environment.NewLine;
                    AboutInformation += tempInfoTotalChars + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoTotalChars, 25, ".") + " " + GetPoolTotalCharacterCount().ToString() + Environment.NewLine;
                    AboutInformation += "(Includes all editors, Stickies and Paragraph Dividers)" + Environment.NewLine;
                    AboutInformation += tempInfoLogEntries + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoLogEntries, 25, ".") + " " + Log.GetLogEntriesCountAsString() + Environment.NewLine;
                    AboutInformation += tempInfoPoolChanged + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoPoolChanged, 25, ".") + " " + IO.PoolSessionChanged + Environment.NewLine;
                    AboutInformation += tempInfoPoolSize + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempInfoPoolSize, 25, ".") + " " + IO.GetFileSize() + Environment.NewLine;
                    AboutInformation += Environment.NewLine;
                    AboutInformation += "Session ID: " + IO.CurrentSessionId + Environment.NewLine;
                    AboutInformation += "Init: " + IO.GetInitDataPathAndFileName() + Environment.NewLine;
                    AboutInformation += "Pool: " + IO.GetCurrentPoolFileName() + Environment.NewLine;
                    AboutInformation += "Directory: " + IO.GetDataPath() + "\\" + Environment.NewLine;
                    AboutInformation += "Local backup: " + IO.GetBackupDirectory() + "\\" + Environment.NewLine;
                    AboutInformation += "Remote backup: " + GetAlternateBackupPathForDisplay();
                }
                catch
                {
                    AboutInformation += Environment.NewLine + "N/A";
                }
                AhoyInfoMessageBox messageBox = new AhoyInfoMessageBox(AboutInformation, MessageText.PoolInformationCaption(), false, true, this.Opacity);
                messageBox.TopMost = true;
                messageBox.ShowDialog();
            }
        }

        private string GetAlternateBackupPathForDisplay()
        {
            if (!string.IsNullOrEmpty(AlternateBackupPath))
            {
                return AlternateBackupPath + "\\";
            }
            else
            {
                return "N/A";
            }
        }


        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbContent.WordWrap = !tbContent.WordWrap;
            ResolveWordWrapMenuCheck();
            // Register changes
            SettingsHasChanges = true;
        }

        // Scrollbars
        private void ShowHorizontalScrollBar()
        {
            if (!Design.EnforceScrollbars)
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.Both;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.Both;
            }
            else
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            }
        }

        private void HideHorizontalScrollBar()
        {
            if (!Design.EnforceScrollbars)
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.Vertical;
            }
            else
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            }
        }

        private void ResolveWordWrapMenuCheck()
        {
            wordWrapToolStripMenuItem.Checked = tbContent.WordWrap;

            if (tbContent.WordWrap)
            {
                HideHorizontalScrollBar();
            }
            else
            {
                ShowHorizontalScrollBar();
            }
        }

        private void clearDisplaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDisplays(false);
        }

        // Toggle interface lock
        private void interfaceLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleInterfaceLock();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.CreatePasswordQuestion(), MessageText.PasswordCaption(), false, false);
            ahoyAnswerMessageBox.ShowDialog();
            if (!ahoyAnswerMessageBox.Accepted)
            {
                return;
            }

            // Confirm the current password first
            Password passwordDialog = new Password(MessageText.EnterOldPasswordTitle(), MessageText.PasswordCaption(), "", VerifyPassword, true, this.Opacity, false);
            passwordDialog.TopMost = true;
            passwordDialog.ShowDialog();

            if (passwordDialog.AccessGranted)
            {
                ChangePoolPassword(MessageText.PasswordCreatedMessage(), MessageText.CreatePasswordTitle(), MessageText.NewPasswordCaption());
            }
            else
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.NewPasswordFailedMessage(), MessageText.PasswordCaption(), false);
                messageBox.ShowDialog();
            }
        }

        // Load the welcometext in the content window
        private void gettingStartedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayQuickGuideInMessageBox();
        }

        // Display about information
        private string AboutInformation { get; set; }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutInformation = MessageText.AboutVersion();
            AhoyInfoMessageBox messageBox = new AhoyInfoMessageBox(AboutInformation, MessageText.AboutCaption(), false, true, this.Opacity);
            messageBox.DetectUrls(true);
            messageBox.TopMost = true;
            messageBox.ShowDialog();
        }

        private void movePinnedUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovePinnedUp();
        }

        private void movePinnedDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovePinnedDown();
        }

        private void movePinnedToTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovePinnedToTop();
        }

        private void movePinnedToBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovePinnedToBottom();
        }

        private void holdEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleHoldEntryA();
        }

        private void holdEntry2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleHoldEntryB();
        }

        private void clearHoldButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearHoldButtons();
        }

        private void goBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        private void goForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }

        private void commandInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCommandPrompt();
        }


        #region Command specific methods added with version 1.3.2.1
        private void ClearScreen()
        {
            ClearClipboard();
            // Close entry if open
            if (!CurrentEntry.IsDummyEntry)
            {
                ClearDisplays(false);
            }
            // Clear lists
            ClearDisplays(false);
            ShowAnimation();
        }

        private void ClearPool()
        {
            if (Entries != null && Entries.Count > 0)
            {
                AhoyAnswerMessageBox msg1 = new AhoyAnswerMessageBox("Delete All Entries?", MessageText.PoolCaption(), false, true);
                msg1.ShowDialog();
                if (!msg1.Accepted)
                {
                    return;
                }
                else
                {
                    if (SimplePromptForPassword(MessageText.PoolInfoCaption(), MessageText.EnterPasswordMessage(), false))
                    {
                        SaveBackupForUndo();
                        ClearScreen();
                        Entries.Clear();
                        PopulatePinList();
                        ClearSearchResults();
                        Log.LogPoolStatus("Reset complete (All Entries)");
                        //Related.MaintainIntegrity(Entries, false);
                        AhoySimpleMessageBox msg2 = new AhoySimpleMessageBox("Reset complete", MessageText.PoolCaption(), false);
                        msg2.ShowDialog();
                    }
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.PoolInfoCaption(), false);
                msg.ShowDialog();
            }
        }


        private void globalUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoLastCommand(true);
        }

        // Retrieve all cloned entries
        private void UndoLastCommand(bool showdialog)
        {
            if (BackupEntries != null && BackupEntries.Count > 0)
            {
                if (showdialog)
                {
                    AhoyAnswerMessageBox msg1 = new AhoyAnswerMessageBox("Undo last Pool operation?\n(Entry deletion, duplication, import, pinned/color reset/sorting and Backup Recovery)", MessageText.PoolCaption(), false, true);
                    msg1.ShowDialog();
                    if (!msg1.Accepted)
                    {
                        return;
                    }
                }
                // Deselect to prevent current entry from saving empty
                ClearDisplays(true); // <-- Must be done
                Entries.Clear();
                for (int i = 0; i < BackupEntries.Count; i++)
                {
                    Entries.Add(IO.CloneEntry(BackupEntries[i], false));
                }
                Log.LogPoolStatus("Undo complete");
                // Update title field colors and stuff
                ResetTitleColorOnCurrentEntry();
                SetDefaultText();
                cbPinned.Checked = false;
                cbProtect.Checked = false;
                cbBookmark.Checked = false;
                listBoxSearchResult.Items.Clear();
                PopulatePinList();
                tbSearch.Focus();
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Undo complete", MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No undo cache available", MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
        }

        // Clone all entries for backup
        private List<Entry> BackupEntries;
        private void SaveBackupForUndo()
        {
            CanUndoSort = false;
            if (BackupEntries == null)
            {
                BackupEntries = new List<Entry>();
            }

            if (Entries != null && Entries.Count > 0)
            {
                BackupEntries.Clear();
                for (int i = 0; i < Entries.Count; i++)
                {
                    BackupEntries.Add(IO.CloneEntry(Entries[i], false));
                }
                Log.LogPoolStatus("Cloning for Undo");
            }
        }

        // Save with backup
        private void SaveWithBackup()
        {
            ForceBackup = true;
            Log.LogPoolStatus("Backup initialized");
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Backup executed", MessageText.PoolCaption(), false);
            msg.ShowDialog();
        }

        // Stop global timers for maintenance
        private void Halt()
        {
            if (timerGlobal.Enabled)
            {
                StopGlobalTimerWithMaintenanceMessage();
            }
        }

        // Start global timers
        private void Resume()
        {
            if (!timerGlobal.Enabled)
            {
                StartGlobalTimerWithMaintenanceMessage();
            }
        }

        private void ToggleAnimations()
        {
            // Toggle Animations
            AnimationsEnabled = !AnimationsEnabled;
            // Register changes
            SettingsHasChanges = true;
        }

        private void ResetPinRank()
        {
            if (Entries != null && Entries.Count > 0)
            {
                SaveBackupForUndo();
                foreach (Entry entry in Entries)
                {
                    if (entry.Rank > 0)
                    {
                        entry.Rank = 0;
                    }
                }
                PopulatePinList();
                Log.LogPoolStatus("Pin Ranking (sort) reset");
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Pin ranking (sort) reset", MessageText.PinCaption(), false);
                msg.ShowDialog();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
        }

        private void ResetPinned()
        {
            if (Entries != null && Entries.Count > 0)
            {
                AhoyAnswerMessageBox msg1 = new AhoyAnswerMessageBox("Remove all Entries from Pinned List?", MessageText.PinCaption(), false, true);
                msg1.ShowDialog();
                if (!msg1.Accepted)
                {
                    return;
                }

                SaveBackupForUndo();
                foreach (Entry entry in Entries)
                {
                    if (entry.Pin)
                    {
                        entry.Pin = false;
                    }
                }
                PopulatePinList();
                Log.LogPoolStatus("Pinned reset (All)");
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Pinned reset", MessageText.PinCaption(), false);
                msg.ShowDialog();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
        }

        private void ResetProtected()
        {
            if (Entries != null && Entries.Count > 0)
            {
                AhoyAnswerMessageBox msg1 = new AhoyAnswerMessageBox("Unlock All Entries?", MessageText.PoolCaption(), false, true);
                msg1.ShowDialog();
                if (!msg1.Accepted)
                {
                    return;
                }

                SaveBackupForUndo();
                foreach (Entry entry in Entries)
                {
                    if (entry.Protected)
                    {
                        entry.Protected = false;
                    }
                }
                Log.LogPoolStatus("Locked Entries unlocked (All)");
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Entries unlocked", MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.PoolCaption(), false);
                msg.ShowDialog();
            }
        }

        private void ResetLog()
        {
            if (SimplePromptForPassword(MessageText.ShowLogCaption(), MessageText.EnterPasswordMessage(), false))
            {
                Log.ClearLog();
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Log cleared", MessageText.ShowLogCaption(), false);
                msg.ShowDialog();
            }
        }

        private void ToggleReduceCache()
        {
            if (!tbContent.ReduceCache)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Clear and reduce session undo cache?", MessageText.UndoCacheCaption(), false, true);
                msg.TopMost = true;
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    tbContent.ReduceUndoCache(true);
                    foreach (Entry e in Entries)
                    {
                        if (e.TempHistoryState != null)
                        {
                            e.TempHistoryState.Clear();
                        }
                    }
                    Log.GeneralStatus("Cache reduced");
                    AhoySimpleMessageBox msgAccepted = new AhoySimpleMessageBox("Cache reduced", MessageText.UndoCacheCaption(), false);
                    msgAccepted.TopMost = true;
                    msgAccepted.ShowDialog();
                }
            }
            else
            {
                tbContent.ReduceUndoCache(false);
                Log.GeneralStatus("Cache enabled");
                AhoySimpleMessageBox msgAccepted = new AhoySimpleMessageBox("Cache enabled", MessageText.UndoCacheCaption(), false);
                msgAccepted.TopMost = true;
                msgAccepted.ShowDialog();
            }
        }

        private static string tempSizeAndLocationInfo;
        private void GetWindowSizeAndLocation()
        {
            tempSizeAndLocationInfo = "Current Window Size:\nWidth: " + MainForm.CurrentWindowWidth + "px  Height: " + MainForm.CurrentWindowHeight + "px";
            tempSizeAndLocationInfo += "\n(Defaults are " + MainForm.WindowsDefaultWidth + "x" + MainForm.WindowsDefaultHeight + "px)";
            tempSizeAndLocationInfo += "\n\nCurrent Window Location:\nX:" + MainForm.CurrentLocationX + "px" + "  Y:" + MainForm.CurrentLocationY + "px";
            tempSizeAndLocationInfo += "\n(Calculated from the bounds of the monitor selected as main, in multi-screen setups)";
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(tempSizeAndLocationInfo, MessageText.SizeLocationCaption(), false);
            msg.ShowDialog();
        }
        #endregion

        // EXECUTE COMMAND VIA COMMAND PROMPT
        // INPUT COMMAND
        // Open command input - open prompt - show prompt
        // Open the command prompt for command input
        private void OpenCommandPrompt()
        {
            if (!InterfaceLock)
            {
                KeepAwake();
                // Initialize prompt and show dialog
                InputPrompt commandPrompt = new InputPrompt(MessageText.CommandsCaption(), "", MessageText.CommandAttentionMessage(), MessageText.CommandMessage(), 256, false, "Execute", true, cbPinned.CheckedBackgroundImage, cbPinned.UnCheckedBackgroundImage, false);
                commandPrompt.TopMost = true;
                commandPrompt.ShowDialog(); // Modal dialog will stop execution here and return the thread (and continue down) when the form closes.
                // Return if cancelled
                if (commandPrompt.ActionCancelled)
                {
                    CommandInput.CommandValidated = false;
                    return;
                }
                else
                {
                    // Submit command and continue
                    CommandInput.SubmitCommandInput(commandPrompt.UserInput);
                }
                // SHELL COMMANDS
                if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.ShellCommand)
                {
                    #region Shell commands
                    ShellCommands.StartProcess(ShellCommands.GetStringSection(commandPrompt.UserInput, 1), ShellCommands.GetStringSection(commandPrompt.UserInput, 2));
                    #endregion

                } // GENERAL COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.GeneralCommand)
                {
                    #region General commands
                    switch (CommandInput.CommandString)
                    {
                        case "cls":
                            ClearScreen();
                            break;
                        case "help": // Show Quickguide
                            DisplayQuickGuideInMessageBox();
                            break;
                        case "list": // List all commands
                            ListShellTools();
                            break;
                        case "new":
                            CreateNewEntry(string.Empty, false, string.Empty, false, false, false);
                            AddNewEntryToEditingCategory();
                            break;
                        // Related commands
                        case "rel":
                            RelatedStatus();
                            break;
                        case "relm":
                            RelatedMark(true);
                            break;
                        case "rela":
                            RelatedAdd(true);
                            break;
                        case "relc":
                            RelatedClearAll();
                            break;
                        case "relr":
                            RelatedRemove();
                            break;
                        case "relu":
                            RelatedRemoveAll();
                            break;
                        case "rell":
                            RelatedList();
                            break;
                        case "rels":
                            RelatedSelectMaster();
                            break;
                        case "relf":
                            FlipRelatedDisplayStyle();
                            break;
                        case "relq":
                            CreateDummyCategory();
                            break;
                        case "relg":
                            SetRelatedGradient(false);
                            break;
                        case "sticky":
                            SpawnSticky(true, false, 0, 0, false);
                            break;
                        case "x":
                            generatePasswordToolStripMenuItem_Click(this, null);
                            break;
                        case "kb":
                            ToggleKeyboardControl();
                            break;
                        case "op":
                            OpenPoolRepository(false);
                            break;
                        case "div":
                            OpenParagraphDividersEditor();
                            break;
                        case "theme":
                            OpenThemeSelector(true);
                            break;
                        case "style":
                            DisplayStyleEditor();
                            break;
                        case "save":
                            SaveAll();
                            break;
                        case "reset":
                            RestoreDefaultWindowSizeAndLocation();
                            break;
                        case "clear":
                            clearAllSettingsToolStripMenuItem_Click(this, null);
                            break;
                        case "demo":
                            ToggleDemo(false);
                            break;
                        case "info":
                            poolInfoToolStripMenuItem_Click(this, null);
                            break;
                        case "mon":
                            ToggleLogMonitor();
                            break;
                        case "debug":
                            ToggleLogMonitor();
                            break;
                        // case "update":
                        //     CheckForUpdates(0, false); // Standard check
                        //     break;
                        case "test":
                            QuickBrownFox();
                            break;
                        case "default":
                            ReloadDefaultPool(false, false, true);
                            break;
                        case "reload":
                            ReloadCurrentPool();
                            break;
                        case "log":
                            DisplayLogWithPassword();
                            break;
                        case "az":
                            SortSelection(false);
                            break;
                        case "za":
                            SortSelection(true);
                            break;
                        case "sleep":
                            LockInterface();
                            break;
                        case "lock":
                            LockInterface();
                            break;
                        case "halt":
                            Halt();
                            break;
                        case "stop":
                            Halt();
                            break;
                        case "resume":
                            Resume();
                            break;
                        case "start":
                            Resume();
                            break;
                        case "border":
                            ToggleFormBorder();
                            break;
                        case "clock":
                            ToggleClock();
                            break;
                        case "time":
                            ShowDateTime();
                            break;
                        case "t":
                            ShowDateTime();
                            break;
                        case "check":
                            // Insert progress template text
                            InsertChecklistTemplate();
                            break;
                        case "task":
                            // Insert progress template text
                            InsertChecklistTemplate();
                            break;
                        case "prog":
                            // Insert progress template text
                            InsertChecklistTemplate();
                            break;
                        case "book":
                            // Insert progress template text
                            InsertBookmarkTemplate();
                            break;
                        case "flip":
                            FlipDummyTextVersion();
                            break;
                        case "ent":
                            ListEntries();
                            break;
                        case "table":
                            GetThemeColors();
                            break;
                        case "csv":
                            GetThemeColorsCsv();
                            break;
                        case "ts":
                            DisplayTimestampFormat("Timestamp format", TimeStampFormat);
                            break;
                        case "ts1":
                            SetTimeStampFormat(0);
                            break;
                        case "ts2":
                            SetTimeStampFormat(1);
                            break;
                        case "ts3":
                            SetTimeStampFormat(2);
                            break;
                        case "ts4":
                            SetTimeStampFormat(3);
                            break;
                        case "ts5":
                            SetTimeStampFormat(4);
                            break;
                        case "ts6":
                            SetTimeStampFormat(5);
                            break;
                        case "undo":
                            UndoLastCommand(true);
                            break;
                        case "exit":
                            ExitAhoy();
                            break;
                    }
                    #endregion

                }// ENTRY COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.EntryCommand)
                {
                    #region Entry commands
                    switch (CommandInput.CommandString)
                    {
                        case "new":
                            CreateNewEntry(string.Empty, false, string.Empty, false, false, false);
                            AddNewEntryToEditingCategory();
                            break;
                        case "delete":
                            DeleteCurrentEntry();
                            break;
                        case "close":
                            ClearDisplays(true);
                            break;
                        case "saveas":
                            SaveEntryAsTXT();
                            break;
                        case "duplicate":
                            DuplicateCurrentEntry(true);
                            break;
                        case "mark":
                            MarkForExport();
                            break;
                        case "pin":
                            ToggleEntryPinned();
                            break;
                    }
                    #endregion
                }// POOL COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.PoolCommand)
                {
                    #region Pool commands
                    switch (CommandInput.CommandString)
                    {
                        case "open":
                            LoadPoolFromFileDialog();
                            break;
                        case "new":
                            NewPool();
                            break;
                        case "saveas":
                            SavePoolFromFileDialog();
                            break;
                        case "switch":
                            SwitchPool();
                            break;
                        case "import":
                            ImportCollection();
                            break;
                        case "export":
                            ExportCollection();
                            break;
                        case "readonly":
                            SavePoolWithReadOnlyParameters();
                            break;
                        case "setbak":
                            SetAlternateBackupFolder(false);
                            break;
                        case "openbak":
                            OpenAlternateFolder();
                            break;
                        case "clearbak":
                            ClearAlternateBackupPath(true);
                            break;
                        case "backup":
                            SaveWithBackup();
                            break;
                        case "recover":
                            LoadBackupPool();
                            break;
                        case "info":
                            poolInfoToolStripMenuItem_Click(this, null);
                            break;
                        case "show":
                            OpenPoolRepository(false);
                            break;
                        case "default":
                            ReloadDefaultPool(false, false, true);
                            break;
                        case "reload":
                            ReloadCurrentPool();
                            break;
                        case "copy":
                            CopyPoolToClipboard();
                            break;
                        case "delete":
                            ClearPool();
                            break;
                        case "undo":
                            UndoLastCommand(true);
                            break;
                        case "dump":
                            PoolDump();
                            break;
                        case "modify":
                            ModifyEntry();
                            break;
                        case "renew":
                            RenewSessionID();
                            break;
                        case "cleanup":
                            PoolCleanup();
                            break;
                    }
                    #endregion
                }// GLOBAL COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.GlobalCommand)
                {
                    #region Global commands
                    switch (CommandInput.CommandString)
                    {
                        case "save":
                            SaveAll();
                            break;
                        case "halt":
                            Halt();
                            break;
                        case "resume":
                            Resume();
                            break;
                    }
                    #endregion
                }// OPTIONS COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.OptionCommand)
                {
                    #region Options commands
                    switch (CommandInput.CommandString)
                    {
                        case "wpm":
                            ToggleWordAndWPMCounter();
                            break;
                        case "anim":
                            ToggleAnimations();
                            break;
                        case "reset":
                            RestoreDefaultWindowSizeAndLocation();
                            break;
                        case "clear":
                            clearAllSettingsToolStripMenuItem_Click(this, null);
                            break;
                        case "az":
                            SortPinnedList(true);
                            break;
                        case "za":
                            SortPinnedList(false);
                            break;
                        case "pinrank":
                            ResetPinRank();
                            break;
                        case "pincolor":
                            ClearAllPinnedColors();
                            break;
                        case "pinreset":
                            ResetPinned();
                            break;
                        case "unlock":
                            ResetProtected();
                            break;
                        case "logreset":
                            ResetLog();
                            break;
                        case "navreset":
                            Navigator.ResetNavigator(btnBack, btnForward);
                            break;
                        case "cache":
                            ToggleReduceCache();
                            break;
                        case "wsize":
                            GetWindowSizeAndLocation();
                            break;
                        case "clickpin":
                            ToggleNewPinnedByClick();
                            break;
                        case "hidepin":
                            ToggleHideByClick();
                            break;
                        case "timestamp":
                            DisplayTimestampFormat("Timestamp format", TimeStampFormat);
                            break;
                        case "timestamp1":
                            SetTimeStampFormat(0);
                            break;
                        case "timestamp2":
                            SetTimeStampFormat(1);
                            break;
                        case "timestamp3":
                            SetTimeStampFormat(2);
                            break;
                        case "timestamp4":
                            SetTimeStampFormat(3);
                            break;
                        case "timestamp5":
                            SetTimeStampFormat(4);
                            break;
                        case "timestamp6":
                            SetTimeStampFormat(5);
                            break;
                        case "flip":
                            FlipDummyTextVersion();
                            break;
                    }
                    #endregion
                }// TOOLS COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.ToolCommand)
                {
                    switch (CommandInput.CommandString)
                    {
                        case "div":
                            OpenParagraphDividersEditor();
                            break;
                        case "snippet":
                            OpenSelectionInNewWindow();
                            break;
                        case "mon":
                            ToggleLogMonitor();
                            break;
                        case "pass":
                            string tempPassword = PasswordGenerator.GeneratePassword(20);
                            AhoySimpleMessageBox msgPass = new AhoySimpleMessageBox("Clipboard set:\n" + tempPassword, MessageText.PasswordCaption(), false);
                            SetClipboardText(tempPassword);
                            msgPass.ShowDialog();
                            break;
                        case "chars":
                            AhoySearchMessageBox msgChars = new AhoySearchMessageBox(TextTransform.GetCharSelection(), MessageText.CharsCaption(), false, false, this.Opacity);
                            msgChars.Show();
                            msgChars.BringToFront();
                            break;
                        case "morechars":
                            AhoySearchMessageBox msgMoreChars = new AhoySearchMessageBox(TextTransform.GetPrintableChars(), MessageText.CharsCaption(), false, false, this.Opacity);
                            msgMoreChars.Show();
                            msgMoreChars.BringToFront();
                            break;
                        // case "dcheck":
                        //     CheckForUpdates(1, true); // Low version test
                        //     break;
                        // case "ucheck":
                        //     CheckForUpdates(2, true); // High version test
                        //     break;
                        // case "vcheck":
                        //     VisitDownloadSite(true); // Visit download site
                        //     break;
                    }
                }// THEME COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.ThemeCommand)
                {
                    switch (CommandInput.CommandString)
                    {
                        case "edit":
                            DisplayStyleEditor();
                            break;
                        case "select":
                            OpenThemeSelector(true);
                            break;
                        case "dev":
                            RestoreDefaultView();
                            SetLayoutMode(Settings.DefaultLayoutMode);
                            SetSleepInterval(5, true);
                            SelectDummyTextVersion(0);
                            ActivateWordAndWPMCounter();
                            AutoSaveEntryChanges = Settings.DefaultAutoSaveEntryChanges;
                            EnableKeyboardControl();
                            ToggleLogMonitor();
                            break;
                        case "table":
                            GetThemeColors();
                            break;
                        case "list":
                            ListThemeColors();
                            break;
                    }
                }// STICKY COMMANDS
                else if (CommandInput.CommandValidated && CommandInput.SelectedCommandType == CommandInput.CommandType.StickyCommand)
                {
                    switch (CommandInput.CommandString)
                    {
                        case "new":
                            SpawnSticky(true, false, 0, 0, false);
                            break;
                        case "hide":
                            HideStickies();
                            break;
                        case "delete":
                            DeleteAllStickies();
                            break;
                        case "align":
                            CenterStickies();
                            break;
                        case "terminate":
                            InputPrompt serviceMsg = new InputPrompt(MessageText.ServiceCaption(), "", "", "Enter service code", 128, false, "OK", false, null, null, true);
                            serviceMsg.ShowDialog();
                            if (serviceMsg.UserInput.Trim() == Srv)
                            {
                                EnableMasterCloseStickies();
                            }
                            break;
                        case "get":
                            RetrieveStickyContent();
                            break;
                    }

                } // Else notify the user that it had no effect
                else if (!CommandInput.CommandValidated)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.CommandAbortedMessage(), MessageText.CommandsCaption(), false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                    // And re-display the prompt
                    OpenCommandPrompt();
                }

                // End of command execution
                CommandInput.CommandValidated = false;
                // ------------------------ end -----------------------------
            }
        }

        private void ShowDateTime()
        {
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(IO.GetIsoDateTimeNow() +
                "\n\n(ISO Weeknumber: " + ISOWeek.GetWeekOfYear(DateTime.Now).ToString("00") + ")", MessageText.DateTimeCaption(), false);
            msg.TopMost = true;
            msg.SetMicroFont();
            msg.ShowDialog();
        }

        // This refers to the CTRL+T command (insert iso datetime)
        public static void SetTimeStampFormat(int value)
        {
            // set value
            TimeStampFormat = value;
            // Display info
            DisplayTimestampFormat("Timestamp (CTRL+T) format set to", value);
            // Save settings
            SettingsHasChanges = true;
        }

        private static void DisplayTimestampFormat(string displayChangeText, int value)
        {
            string tempTimestamp = "";
            for (int i = 0; i < 6; i++)
            {
                if (i == value)
                {
                    tempTimestamp += "● ";
                    tempTimestamp += (i + 1) + ": " + TimeStamp.GetTimeStampFormat(i) + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(TimeStamp.GetTimeStampFormat(i), TimeStamp.GetTimeStampFormat(0).Length, " ") + Environment.NewLine;
                }
                else
                {
                    tempTimestamp += TextTransform.GetBlankSpace(2) + (i + 1) + ": " + TimeStamp.GetTimeStampFormat(i) + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(TimeStamp.GetTimeStampFormat(i), TimeStamp.GetTimeStampFormat(0).Length, " ") + Environment.NewLine;
                }
            }
            if (!string.IsNullOrEmpty(displayChangeText))
            {
                displayChangeText += " " + (value + 1) + "\n\n";
            }
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(displayChangeText + tempTimestamp, "Timestamp", false);
            msg.ShowDialog();
        }

        // Change pinned colors
        private void closeEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDisplays(true);
        }

        private void hidePinnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnTogglePinnedList_Click();
        }

        private void color1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(1);
        }

        private void color2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(2);
        }

        private void color3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(3);
        }

        private void color4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(4);
        }

        private void color5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(5);
        }

        private void color6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(6);
        }

        private void color7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(7);
        }

        private void color8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(8);
        }

        private void customColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(9);
        }

        private void noColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(0);
        }

        // Pinned list context menu
        private void color1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(1);
        }

        private void color2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(2);
        }

        private void color3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(3);
        }

        private void color4ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(4);
        }

        private void color5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(5);
        }

        private void color6ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(6);
        }

        private void color7ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(7);
        }

        private void color8ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(8);
        }

        private void customColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(9);
        }

        private void noColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePinnedColorByNumber(0);
        }




        // Lists font sizes
        private void zoom1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetListsFontSize(0);
        }

        private void zoom2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetListsFontSize(1);
        }

        private void zoom3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetListsFontSize(2);
        }


        private static int tempItemHeight;
        private void SetListsFontSize(int size)
        {
            Log.SettingsChanged("Font size " + size.ToString() + " (List)");
            // Change item height according to font size
            switch (size)
            {
                case 0:
                    tempItemHeight = 14;
                    break;
                case 1:
                    tempItemHeight = 18;
                    break;
                case 2:
                    tempItemHeight = 20;
                    break;
                default:
                    tempItemHeight = 18;
                    break;
            }

            ListsFontSize = size;
            listBoxSearchResult.Font = Design.ListBoxFont(ListsFontSize);
            listBoxSearchResult.ItemHeight = tempItemHeight;
            listBoxPinned.Font = Design.ListBoxFont(ListsFontSize);
            listBoxPinned.ItemHeight = tempItemHeight;
            ResolveListsZoomIndicator(ListsFontSize);
            ResolveListFontWeightMenuItem();
            // Register changes
            SettingsHasChanges = true;
        }

        private void boldListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.SettingsChanged("Font weight (List)");
            Design.CurrentListFontWeight = !Design.CurrentListFontWeight;
            ResolveFonts();
        }

        private void ResolveListFontWeightMenuItem()
        {
            if (Design.CurrentListFontWeight)
            {
                boldListsToolStripMenuItem.Checked = true;
            }
            else
            {
                boldListsToolStripMenuItem.Checked = false;
            }
        }

        private void ResolveListsZoomIndicator(int zoom)
        {
            switch (zoom)
            {
                case 0:// Outcommented while color is used in the context menus to display selection
                    zoom1ToolStripMenuItem.Checked = true;
                    //zoom1ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    zoom2ToolStripMenuItem.Checked = false;
                    //zoom2ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom3ToolStripMenuItem.Checked = false;
                    //zoom3ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 1:
                    zoom1ToolStripMenuItem.Checked = false;
                    //zoom1ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom2ToolStripMenuItem.Checked = true;
                    //zoom2ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    zoom3ToolStripMenuItem.Checked = false;
                    //zoom3ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 2:
                    zoom1ToolStripMenuItem.Checked = false;
                    //zoom1ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom2ToolStripMenuItem.Checked = false;
                    //zoom2ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom3ToolStripMenuItem.Checked = true;
                    //zoom3ToolStripMenuItem.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
            }

            ResolveZoomMasterMenuCheckedState();
        }

        // Fields font sizes
        private void zoom1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetFieldsFontSize(0, true, true);
        }

        private void zoom2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetFieldsFontSize(1, true, true);
        }

        private void zoom3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetFieldsFontSize(2, true, true);
        }

        private void SetFieldsFontSize(int size, bool registerChanges, bool invokeStickyCycle)
        {
            Log.SettingsChanged("Font size " + size.ToString() + " (Fields)");
            // Reset Zoomfactor if any
            tbContent.ResetZoomFactor();
            FieldsFontSize = size;
            tbSearch.Font = Design.FieldFont(FieldsFontSize);
            tbSearchInContent.Font = Design.FieldFont(FieldsFontSize);
            tbTitle.Font = Design.FieldFont(FieldsFontSize);
            tbContent.Font = Design.FieldFont(FieldsFontSize);
            lbPinnedAndMemoAnimation.Font = Design.ContentFieldFont();
            if (tbMemoField != null)
            {
                tbMemoField.Font = Design.FieldFont(FieldsFontSize);
            }

            ResolveFieldsZoomIndicator(FieldsFontSize);

            // Update stickies
            if (invokeStickyCycle)
            {
                ColorThemeChanged();
            }

            if (registerChanges)
            {
                // Register changes
                SettingsHasChanges = true;
            }

            // Update home screen label
            RefreshHomeScreenLabel();
        }


        private void ResolveFieldsZoomIndicator(int zoom)
        {
            switch (zoom)
            {
                case 0: // Outcommented while color is used in the context menus to display selection
                    zoom1ToolStripMenuItem1.Checked = true;
                    //zoom1ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    zoom2ToolStripMenuItem1.Checked = false;
                    //zoom2ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom3ToolStripMenuItem1.Checked = false;
                    //zoom3ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 1:
                    zoom1ToolStripMenuItem1.Checked = false;
                    //zoom1ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom2ToolStripMenuItem1.Checked = true;
                    //zoom2ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    zoom3ToolStripMenuItem1.Checked = false;
                    //zoom3ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 2:
                    zoom1ToolStripMenuItem1.Checked = false;
                    //zoom1ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom2ToolStripMenuItem1.Checked = false;
                    //zoom2ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    zoom3ToolStripMenuItem1.Checked = true;
                    //zoom3ToolStripMenuItem1.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
            }

            ResolveZoomMasterMenuCheckedState();
        }

        private void ResolveZoomMasterMenuCheckedState()
        {
            // If both fields and lists have the same
            // Zoom level check the master zoom menu
            if (FieldsFontSize == ListsFontSize)
            {
                switch (FieldsFontSize)
                {
                    case 0:
                        zoom50ToolStripMenuItem.Checked = true;
                        zoom100ToolStripMenuItem.Checked = false;
                        zoom150ToolStripMenuItem.Checked = false;
                        break;
                    case 1:
                        zoom50ToolStripMenuItem.Checked = false;
                        zoom100ToolStripMenuItem.Checked = true;
                        zoom150ToolStripMenuItem.Checked = false;
                        break;
                    case 2:
                        zoom50ToolStripMenuItem.Checked = false;
                        zoom100ToolStripMenuItem.Checked = false;
                        zoom150ToolStripMenuItem.Checked = true;
                        break;
                }
            }
            else
            {   // If not uncheck all master zoom indications
                zoom50ToolStripMenuItem.Checked = false;
                zoom100ToolStripMenuItem.Checked = false;
                zoom150ToolStripMenuItem.Checked = false;
            }
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void ToggleFullScreen()
        {
            if (!IsFullScreen)
            {
                SetFullScreen(true);
            }
            else
            {
                SetNorrmalScreen(true);
            }
        }


        Timer tempFullscreenTimer;
        private void SetFullScreen(bool registerChanges)
        {
            try
            {
                // Wait until the form is visible
                if (!this.Visible)
                {
                    tempFullscreenTimer = new Timer();
                    tempFullscreenTimer.Interval = 500;
                    tempFullscreenTimer.Tick += TempFullscreenTimer_Tick;
                    tempFullscreenTimer.Enabled = true;
                    return;
                }

                SuspendLayout();
                // Indicate full screen
                IsFullScreen = true;
                fullScreenToolStripMenuItem.Text = "Exit &Full Screen";
                IsMaximized = false;

                // Toggle full screen
                this.WindowState = FormWindowState.Normal; // Start by resetting a maximized state
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                if (registerChanges)
                {
                    // Register changes
                    SettingsHasChanges = true;
                }
                ResumeLayout();
            }
            catch
            {
                Log.LogGeneralError("Full screen");
            }
        }

        private void TempFullscreenTimer_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                tempFullscreenTimer.Enabled = false;
                SetFullScreen(false);
            }
        }

        private void MaximizeWindow(bool registerChanges)
        {
            try
            {
                SuspendLayout();
                // Indicate maximized window
                IsFullScreen = false;
                fullScreenToolStripMenuItem.Text = "&Full Screen";
                IsMaximized = true;

                // Restore previous window style and maximize
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;

                if (registerChanges)
                {
                    // Register changes
                    SettingsHasChanges = true;
                }
                ResumeLayout();
            }
            catch
            {
                Log.LogGeneralError("Maximize");
            }
        }

        private void SetNorrmalScreen(bool registerChanges)
        {
            try
            {
                SuspendLayout();
                // Indicate normal screen
                IsFullScreen = false;
                fullScreenToolStripMenuItem.Text = "&Full Screen";
                IsMaximized = false;

                // Restore previous window size and refresh
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;

                // Set window size
                Size newWindowSize = new Size(CurrentWindowWidth, CurrentWindowHeight);
                this.Size = newWindowSize;
                // Set window location (can be + and - in both X and Y to allow for multi-screen setups)
                Point newLocation = new Point(CurrentLocationX, CurrentLocationY);
                this.Location = newLocation;


                if (registerChanges)
                {
                    // Register changes
                    SettingsHasChanges = true;
                }
                ResumeLayout();
            }
            catch
            {
                Log.LogWindowStateChangeError();
                SetDefaultWindowSizeAndLocation();
            }
        }



        // Restore default window and text sizes
        private void restoreDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreDefaultWindowSizeAndLocation();
        }

        private void RestoreDefaultWindowSizeAndLocation()
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.ResetViewQuestion(), MessageText.ViewCaption(), true, false);
            msg.ShowDialog();

            if (msg.Accepted)
            {
                RestoreDefaultView();
            }
        }

        private void SetDefaultWindowSizeAndLocation()
        {
            try
            {
                // Restore default window size and boarder style
                IsFullScreen = false;
                IsMaximized = false;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = WindowDefaultSize;
                this.splitContainer1.SplitterDistance = DefaultSplitterDistance;
                this.CenterToScreen();
                this.Refresh();
                // Get current location
                UpdateLocationInformation();
            }
            catch
            {
                Log.LogGeneralError("Default windows size and location");
            }
        }


        // THIS METHOD ONLY RESTORE THE VIEW SETTINGS NOT OPTIONS
        private void RestoreDefaultView()
        {
            // Log the event
            Log.LogRestoreDefaultView();

            SetNorrmalScreen(false);
            SetDefaultWindowSizeAndLocation();

            // Restore field and list text size
            zoom2ToolStripMenuItem1_Click(this, null);
            zoom2ToolStripMenuItem_Click(this, null);

            // Restore font
            Design.CurrentFont = 0;
            Design.CurrentListFontWeight = Settings.DefaultListFontWeight;
            ResolveFontSelector();
            ResolveFonts();
            Design.EnforceScrollbars = Settings.DefaultEnforceScrollbars;
            ResolveEnforceScrollbarsMenu();

            // Reset custom colors
            AhoyColors.ResetAllCustomPreviewColors();
            AhoyColors.ResetAllCustomColors();

            // MicroCalculator

            // DisplayText cycle
            DisplayTextCycle = Settings.DefaultDisplayTextCycle;
            RefreshDummyText();

            // Restore color theme
            AhoyColors.ColorTheme = 0;
            SetNoTexture(true);
            SetColorTheme(true);
            SetFormOpacity(Settings.DefaultOpacity, false);

            // Reset screen blocker interval

            // Prism effect

            // Clock
            ShowClock = Settings.DefaultShowClock;
            RefreshDummyText();
            ResolveClockMenuItem();

            // Form border
            EnableFormBorder = Settings.DefaultEnableFormBorder;
            ResolveFormBorder();

            // Keyboard control
            MoveByKeyboard = Settings.DefaultMoveByKeyboard;
            ResizeByKeyboard = Settings.DefaultResizeByKeyboard;
            ResolveKeyboardControlMenuItem();

            // Restore splitter panels
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
            }
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }

            // Restore layout
            SetLayoutMode(Settings.DefaultLayoutMode);

            // Register changes
            SettingsHasChanges = true;
        }

        private void clearAllSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.ResetSettingsQuestion(), MessageText.SettingsCaption(), false, false);
            msg.ShowDialog();

            if (msg.Accepted)
            {
                ResetAllSettings();
            }
        }

        private void ResetAllSettings()
        {
            RestoreDefaultView();
            // Reset word wrap
            tbContent.WordWrap = true;
            ResolveWordWrapMenuCheck();
            // Blank lines and Title tab to content
            TitleTabToContent = Settings.DefaultTitleTabToContent;
            ResolveTitleTabToContent();
            InsertLines = Settings.DefaultInsertNewLines;
            ResolveInsertNewLines();
            // Auto-search
            AutoSearch = Settings.DefaultAutoSearch;
            UpdateAutoSearchVisuals();
            // Auto hide
            AutoHidePinnedAndMemo = false;
            ResolveAutoHideListsMenuCheck();
            // Reset detect URL
            DetectURLs = Settings.DefaultDetectURLs;
            ResolveDetectURLsCheckedMenuItem();
            // Default Browser
            SelectedInternetBrowser = Settings.DefaultBrowser;
            ResolveDefaultBrowserCheckedMenuItems();
            // Snooze Interval
            SnoozeInterval = Settings.DefaultSnoozeInterval;
            // Reset spell checker language
            SpellCheckerLanguage = 1;
            // Reset password generator selection
            SelectedPasswordStyle = Settings.DefaultPasswordStyle;
            // Reset auto save Entry setting
            AutoSaveEntryChanges = Settings.DefaultAutoSaveEntryChanges;
            UpdateSaveButton();
            // Reset sleep duration
            SetSleepInterval(3, true);
            // Clear Hold Buttons
            ClearHoldButtons();
            // Clear PLOP ID
            // Clear Cypher key
            CypherKey = "";
            // Clear saved dialog paths
            OpenFilePath = "";
            SaveFilePath = "";
            // Reset WPM state
            ResetWordAndWPMCounter();
            // Reset Typewriter
            // Reset Screen Blocker
            // Prism effect
            // Title priority search
            TitlePrioritySearch = Settings.DefaultTitlePrioritySearch;
            ResolveTitlePrioritySearchMenuItem();
            LiveSearch = Settings.DefaultLiveSearch;
            ResolveLiveSearchMenuItem();
            // Clock
            ShowClock = Settings.DefaultShowClock;
            RefreshDummyText();
            ResolveClockMenuItem();
            // Form border
            EnableFormBorder = Settings.DefaultEnableFormBorder;
            ResolveFormBorder();
            // Pinned by click
            NewPinnedByClick = Settings.DefaultNewPinnedByClick;
            ResolveNewPinnedByClickMenuItem();
            // Keyboard control
            MoveByKeyboard = Settings.DefaultMoveByKeyboard;
            ResizeByKeyboard = Settings.DefaultResizeByKeyboard;
            ResolveKeyboardControlMenuItem();
            // Timestamp format
            TimeStampFormat = Settings.DefaultTimestampFormat;
            // Hide pinned by click
            HidePinnedByClick = Settings.DefaultHidePinnedByClick;
            ResolveHidePinnedByClickMenuItem();
            // Remote backup
            ForceClearAlternateBackupPath();
            // Log the event
            Log.LogResetSettingsChanged();
        }


        private void ClearMemoField()
        {
            MemoCaretPosition = 0;
            tbMemoField.Clear();
        }

        private void zoom50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom1ToolStripMenuItem1_Click(this, null);
            zoom1ToolStripMenuItem_Click(this, null);
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom2ToolStripMenuItem1_Click(this, null);
            zoom2ToolStripMenuItem_Click(this, null);
        }

        private void zoom150ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom3ToolStripMenuItem1_Click(this, null);
            zoom3ToolStripMenuItem_Click(this, null);
        }

        private void clearAllColorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearAllPinnedColors();
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayLogWithPassword();
        }

        private void DisplayLogWithPassword()
        {
            if (SimplePromptForPassword(MessageText.ShowLogCaption(), MessageText.LogAccessTitle(), false))
            {
                DisplayLog();
            }
        }

        private void setDisplayTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCustomDisplayText();
        }

        private void clearDisplayTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCustomDisplayText();
        }


        // 5 min (300 sec)
        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(0);
        }

        // 15 min (900 sec)
        private void minToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(1);
        }

        // 30 min (1800 sec)
        private void minToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(2);
        }

        // 60 min (3600 sec)  
        private void minToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(3);
        }

        // 90 min (5400 sec)  
        private void minToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(4);
        }

        // 120 min (7200 sec)  
        private void minToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(5);
        }

        // Never
        private void neverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(6);
        }

        // 180 min (10800 sec) 3 Hours
        private void minToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(7);
        }

        // 240 min (14400 sec) 4 Hours
        private void minToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(8);
        }

        // 300 min (18000 sec) 5 Hours
        private void minToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            UserSetSleepInterval(9);
        }

        // Set sleep interval with password (for user selection, as only authorized users should be able to change it)
        private void UserSetSleepInterval(int index)
        {
            // Require password if interval is set to never
            if (index == 6)
            {
                if (SimplePromptForPassword(MessageText.SleepIntervalCaption(), MessageText.ChangeSleepIntervalTitle(), false))
                {
                    SetSleepInterval(index, true);
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SleepIntervalChangedMessage(), MessageText.SleepIntervalCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                SetSleepInterval(index, true);
            }
        }

        // Set sleep interval
        private void SetSleepInterval(int index, bool registerChanges)
        {
            Log.SettingsChanged("Sleep interval set (" + index.ToString() + ")");
            // Save selected index
            SleepIntervalIndex = index;
            switch (index)
            {
                case 0: // 5 min (300 sec)
                    KeepAwake();
                    SleepIntervalSeconds = 300;
                    break;
                case 1:// 15 min (900 sec)
                    KeepAwake();
                    SleepIntervalSeconds = 900;
                    break;
                case 2: // 30 min (1800 sec)
                    KeepAwake();
                    SleepIntervalSeconds = 1800;
                    break;
                case 3:// 60 min (3600 sec) 1 Hour
                    KeepAwake();
                    SleepIntervalSeconds = DefaultSleepInterval; // Default
                    break;
                case 4:// 90 min (5400 sec) 
                    KeepAwake();
                    SleepIntervalSeconds = 5400;
                    break;
                case 5:// 120 min (7200 sec) 2 Hours
                    KeepAwake();
                    SleepIntervalSeconds = 7200;
                    break;
                case 6:// Never
                    KeepAwake();
                    SleepIntervalSeconds = NeverSleepValue; // (99999)
                    break;
                case 7:// 180 min (10800 sec) 3 Hours  (ADDED LATER HENCE USING HIGHER INDEX)
                    KeepAwake();
                    SleepIntervalSeconds = 10800;
                    break;
                case 8:// 240 min (14400 sec) 4 Hours
                    KeepAwake();
                    SleepIntervalSeconds = 14400;
                    break;
                case 9:// 300 min (18000 sec) 5 Hours
                    KeepAwake();
                    SleepIntervalSeconds = 18000;
                    break;
                default:// 60 min (3600 sec)
                    KeepAwake();
                    SleepIntervalSeconds = DefaultSleepInterval; // Default
                    break;
            }

            ResolveSleepIntervalMenu(index);

            if (registerChanges)
            {
                // Register changes
                SettingsHasChanges = true;
            }
        }

        private void ResolveSleepIntervalMenu(int index)
        {
            switch (index)
            {
                case 0: // 5 min (300 sec)
                    minToolStripMenuItem.Checked = true;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 1:// 15 min (900 sec)
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = true;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 2: // 30 min (1800 sec)
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = true;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 3:// 60 min (3600 sec) 
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = true; // Default
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 4:// 90 min (5400 sec) 
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = true;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 5:// 120 min (7200 sec) 
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = true;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 6:// Never
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = true;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 7:// 180 min
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = true;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 8:// 240 min
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = true;
                    minToolStripMenuItem8.Checked = false;
                    break;
                case 9:// 300 min
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = false;
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    minToolStripMenuItem6.Checked = false;
                    minToolStripMenuItem7.Checked = false;
                    minToolStripMenuItem8.Checked = true;
                    break;
                default:// 60 min (3600 sec)
                    minToolStripMenuItem.Checked = false;
                    minToolStripMenuItem1.Checked = false;
                    minToolStripMenuItem2.Checked = false;
                    minToolStripMenuItem3.Checked = true; // Default
                    minToolStripMenuItem4.Checked = false;
                    minToolStripMenuItem5.Checked = false;
                    neverToolStripMenuItem.Checked = false;
                    break;
            }
        }

        #endregion

        #region BUTTON EVENTS
        // Perform search
        private static List<string> searchHistoryList;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Search
            // Indicate selection by search result
            EntrySelectedBySearchResult = true;
            PerformSearch(tbSearch.Text.Trim(), false);
        }

        private static bool tempSearchHistoryMatch;
        private void AddToSearchHistoryList(string query)
        {
            tempSearchHistoryMatch = false;
            // Check that search string is not in list
            if (searchHistoryList.Count > 0)
            {
                foreach (string s in searchHistoryList)
                {
                    if (s == query)
                    {
                        tempSearchHistoryMatch = true;
                        break;
                    }
                }
            }

            // Add to list if not found
            if (!tempSearchHistoryMatch)
            {
                searchHistoryList.Add(query);
            }
        }

        private void InitializeSearchHistoryList()
        {
            recallSearchIndex = 0;
            if (searchHistoryList == null)
            {
                searchHistoryList = new List<string>();
            }
        }

        private void ClearSearchHistoryList()
        {
            recallSearchIndex = 0;
            searchHistoryList.Clear();
        }

        private static int recallSearchIndex;
        private void RecallSearchBack()
        {
            if (searchHistoryList.Count > 0)
            {
                if (recallSearchIndex >= 1)
                {
                    recallSearchIndex -= 1;
                }
                else
                {
                    recallSearchIndex = searchHistoryList.Count - 1;
                }

                InsertRecalledSearchString();
            }
        }

        private void RecallSearchForward()
        {
            if (searchHistoryList.Count > 0)
            {
                if (recallSearchIndex < searchHistoryList.Count - 1)
                {
                    recallSearchIndex += 1;
                }
                else
                {
                    recallSearchIndex = 0;
                }

                InsertRecalledSearchString();
            }
        }

        private void InsertRecalledSearchString()
        {
            if (searchHistoryList.Count > 1)
            {
                if (tbSearch.Text != searchHistoryList[recallSearchIndex])
                {
                    tbSearch.Text = searchHistoryList[recallSearchIndex];
                }
                else
                {
                    RecallSearchForward();
                    tbSearch.Text = searchHistoryList[recallSearchIndex];
                }
            }
            else if (searchHistoryList.Count == 1)
            {
                tbSearch.Text = searchHistoryList[0];
            }

            // Move cursor to end
            tbSearch.Focus();
            if (tbSearch.Text.Length > 0)
            {
                tbSearch.Select(tbContent.Text.Length, 0);
                tbSearch.ScrollToCaret();
            }
        }


        private void btnHistory_Click(object sender, EventArgs e)
        {
            FindHistoryEntries();
        }

        private void btnSearchInContent_Click(object sender, EventArgs e)
        {
            if (!timerRefocusContentSearch.Enabled)
            {
                FindContent(tbSearchInContent.Text);
            }
        }

        // Create new entry
        private void btnNew_Click(object sender, EventArgs e)
        {
            CreateNewEntry(string.Empty, false, string.Empty, false, false, false);
            AddNewEntryToEditingCategory();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(this, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save by click and return focus to content
            SaveCurrentEntry(false);
            tbContent.Focus();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            ToggleInterfaceLock();
        }

        // Sleep label click
        private void lbInterfaceLocked_Click(object sender, EventArgs e)
        {
            ToggleInterfaceLock();
        }

        // Protect entry
        private void cbProtect_Click(object sender, EventArgs e)
        {
            // React if user changes text, and not when set by listbox selection
            if (CurrentEntry != null && !CurrentEntry.IgnoreTextChanged && !CurrentEntry.IsDummyEntry)
            {
                Log.LogEntryStateChange(CurrentEntry.Title, "Locked state changed");
                // Inform about changes
                CurrentEntryHasChanges();
                SaveCurrentEntry(false);
                tbContent.Focus();
            }
            else if (CurrentEntryIsDummy())
            {
                cbProtect.Checked = false;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToLockMessage(), MessageText.WriteProtectionCaption(), false);
                msg.BringToFront();
                msg.ShowDialog();
            }
        }

        // Pin entry
        private void cbPinned_Click(object sender, EventArgs e)
        {
            // React if user changes text, and not when set by listbox selection
            if (CurrentEntry != null && !CurrentEntry.IgnoreTextChanged && !CurrentEntry.IsDummyEntry)
            {
                // If is related pin the master instead by selecting it (only if not already pinned)
                if (Related.IsRelated(CurrentEntry, Entries) && Related.IsMasterPinned(CurrentEntry.ID, Entries))
                {
                    cbPinned.Checked = false;
                    return;
                }
                else
                {
                    // If is related and not paster is  pinned, pin the master (category)
                    ResolveMasterPin(CurrentEntry, cbPinned.Checked);
                }

                Log.LogEntryStateChange(CurrentEntry.Title, "Pinned state changed");
                CurrentEntryHasChanges();
                tbContent.Focus();
                InvokeAutoSaveEntry();
            }
            else if (CurrentEntryIsDummy()) // Force pinned back to false
            {
                cbPinned.Checked = false;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToPinMessage(), MessageText.PinCaption(), false);
                msg.BringToFront();
                msg.ShowDialog();
            }
        }

        // If pinning a related entry pin the master instead // Related
        private void ResolveMasterPin(Entry slave, bool checkedState)
        {
            if (Related.IsRelated(slave, Entries) && !Related.IsMasterPinned(slave.ID, Entries)) // Related
            {
                SelectEntryByID(Related.GetMasterFromSlave(slave, Entries).ID, false, false);
                // Selecting the master entry will set the cbPinned, therefore update it
                if (cbPinned.Checked != checkedState)
                {
                    cbPinned.Checked = checkedState;
                }
                ResolvePinnedMenuItem(); // Update menu
            }
        }

        internal void CurrentEntryHasChanges()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                CurrentEntry.HasChanges = true;
            }
        }

        // Move pinned up
        private void btnPinnedUp_Click(object sender, EventArgs e)
        {
            MovePinnedUp();
        }

        // Move pinned down
        private void btnPinnedDown_Click(object sender, EventArgs e)
        {
            MovePinnedDown();
        }

        // Move pinned to top
        private void btnPinnedTop_Click(object sender, EventArgs e)
        {
            MovePinnedToTop();
        }

        // Move pinned to bottom
        private void btnPinnedBottom_Click(object sender, EventArgs e)
        {
            MovePinnedToBottom();
        }

        // React on pinned key press
        private void btnPinnedButtonsKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                // Clear displays
                ClearDisplays(false);
            }
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            ToggleHoldEntryA();
        }

        private void btnHoldA_MouseDown(object sender, MouseEventArgs e)
        {
            // If right clicked ask to clear button
            if (e.Button == MouseButtons.Right && HoldEntryA)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.ClearHoldButtonQuestion(1), MessageText.HoldButtonCaption(), false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    ClearHoldButton(false);
                }
            }
        }

        private void btnHoldB_Click(object sender, EventArgs e)
        {
            ToggleHoldEntryB();
        }

        private void btnHoldB_MouseDown(object sender, MouseEventArgs e)
        {
            // If right clicked ask to clear button
            if (e.Button == MouseButtons.Right && HoldEntryB)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.ClearHoldButtonQuestion(2), MessageText.HoldButtonCaption(), false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    ClearHoldButton(true);
                }
            }
        }

        // Navigate back
        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        // Navigate forward
        private void btnForward_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }

        // Close entry if selected
        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearDisplays(true);
        }

        private void btn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClearDisplays(true);
                e.Handled = true;
            }
        }

        // Show pinned list
        private void btnTogglePinnedList_Click()
        {
            ShowPinned();

            if (CurrentEntryNotNullOrDummy())
            {
                tbContent.Focus(); // Refocus the content field after hiding the pinned list
            }
            else
            {
                tbSearch.Focus();
            }
        }

        // Used by the PinnedListMaskLabel to reactivate the pinned list when clicking it
        void PinnedListMaskLabel_Click(object sender, EventArgs e)
        {
            if (MemoWasLastVisible)
            {
                ShowMemo();
            }
            else
            {
                ShowPinned();
                if (CurrentEntryNotNullOrDummy())
                {
                    tbContent.Focus(); // Refocus the content field after hiding the pinned list
                }
                else
                {
                    tbSearch.Focus();
                }
            }
        }


        // Change pinned color by shortcut (related to method above all though it is called by menu events)
        private void ChangePinnedColorByNumber(int color)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // Save entry before continuing
                SaveCurrentEntry(!AutoSaveEntryChanges);
                Log.GeneralEntryStatus("Pinned color (" + color.ToString() + ")");

                switch (color)
                {
                    case 0:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 0;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 1:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 1;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 2:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 2;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 3:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 3;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 4:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 4;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 5:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 5;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 6:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 6;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 7:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 7;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 8:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 8;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                    case 9:
                        SetCustomPinnedColor(); // Open color picker
                        break;
                    default:
                        CurrentEntry.CustomColor = "";
                        CurrentEntry.PresetColor = 0;
                        // Set colors on related as well
                        DistributeColorsToRelated();
                        break;
                }

                // Submit changes
                SubmitStatusChanges();

                // Populate and reselect pinned item
                PopulatePinListAndReselect();
            }
            else
            {
                DisplayPinnedSelectionMessage();
            }
        }

        private void DistributeColorsToRelated()
        {
            if (Related.HasRelated(CurrentEntry))
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Set all entries in category?", MessageText.RelationCaption(), false, false);
                msg.ShowDialog();
                if (msg.Accepted)
                {
                    Related.CopyColorToAllRelated(CurrentEntry, Entries);
                }
            }
        }

        private void SetCustomPinnedColor()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                try
                {
                    Log.GeneralEntryStatus("Set custom color (\"" + CurrentEntry.Title + "\")");
                    AhoyColorPicker picker;

                    // Set colors of the color picker according to currently pinned color, if any
                    if (CurrentEntry.PresetColor > 0)
                    {
                        picker = new AhoyColorPicker(true, Design.ReturnStatusBackgroundColor(CurrentEntry.PresetColor), Design.ReturnStatusForegroundColor(CurrentEntry.PresetColor), "0", "1", "2");
                    }
                    else if (AhoyColors.HasDualHexValue(CurrentEntry.CustomColor))
                    {
                        picker = new AhoyColorPicker(true, AhoyColors.ConvertFromHex(CurrentEntry.CustomColor.Split(';')[0], false), AhoyColors.ConvertFromHex(CurrentEntry.CustomColor.Split(';')[1], false), "0", "1", "2");
                    }
                    else
                    {
                        picker = new AhoyColorPicker(true, AhoyColors.CurrentColorTheme()[1], AhoyColors.CurrentColorTheme()[4], "0", "1", "2");
                    }
                    picker.BringToFront();
                    picker.ShowDialog();
                    if (picker.Accepted)
                    {
                        CurrentEntry.PresetColor = 0; // Reset color preset
                        CurrentEntry.CustomColor = AhoyColorPicker.PrimaryColorHex + ";" + AhoyColorPicker.SecondaryColorHex;
                        if (AhoyColors.HasDualHexValue(CurrentEntry.CustomColor))
                        {
                            Log.GeneralEntryStatus("Hex values set (" + CurrentEntry.CustomColor + ")");
                            // Set colors on related as well
                            DistributeColorsToRelated();
                        }
                        else
                        {
                            CurrentEntry.CustomColor = ""; // Clear invalid color
                            Log.GeneralEntryStatus("Invalid custom color");
                        }
                        // Updating visuals and saving must be handled from the calling method
                    }
                    else
                    {
                        Log.GeneralEntryStatus("Custom color aborted");
                    }
                }
                catch
                {
                    Log.GeneralEntryStatus("Error setting custom color");
                }
            }
        }

        private void copyCustomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyCustomToolStripMenuItem_Click(this, null);
        }

        private void pasteCustomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteCustomToolStripMenuItem_Click(this, null);
        }

        // Copy custom pinned color
        private string tempCustomColorClipboard;
        private int tempCustomColorClipboard2;
        private void copyCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (AhoyColors.HasDualHexValue(CurrentEntry.CustomColor))
                {
                    tempCustomColorClipboard = CurrentEntry.CustomColor;
                    Log.GeneralEntryStatus("Copy colors (" + tempCustomColorClipboard + ")");
                    SetStatusText("Copy colors");
                    tempCustomColorClipboard2 = 0; // Reset opposite clipboard
                }
                else if (CurrentEntry.PresetColor > 0)
                {
                    tempCustomColorClipboard2 = CurrentEntry.PresetColor;
                    Log.GeneralEntryStatus("Copy colors (Preset " + tempCustomColorClipboard2 + ")");
                    SetStatusText("Copy colors");
                    tempCustomColorClipboard = ""; // Reset opposite clipboard
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoCustomColorPinMessage(), MessageText.PinCaption(), false);
                    msg.BringToFront();
                    msg.ShowDialog();
                }
            }
        }

        // Paste custom pinned color
        private void pasteCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy() && (AhoyColors.HasDualHexValue(tempCustomColorClipboard) || tempCustomColorClipboard2 > 0))
            {
                if (AhoyColors.HasDualHexValue(tempCustomColorClipboard))
                {
                    CurrentEntry.CustomColor = tempCustomColorClipboard;
                    CurrentEntry.PresetColor = 0;// reset value
                    Log.GeneralEntryStatus("Paste colors (" + tempCustomColorClipboard + ")");
                    // Set colors on related as well
                    DistributeColorsToRelated();
                    SetStatusText("Paste colors");
                    // Submit changes
                    SubmitStatusChanges();
                    // Populate and reselect pinned item
                    PopulatePinListAndReselect();
                }
                else if (tempCustomColorClipboard2 > 0)
                {
                    CurrentEntry.PresetColor = tempCustomColorClipboard2;
                    CurrentEntry.CustomColor = "";// reset value
                    Log.GeneralEntryStatus("Paste colors (Preset: " + tempCustomColorClipboard2 + ")");
                    // Set colors on related as well
                    DistributeColorsToRelated();
                    SetStatusText("Paste colors");
                    // Submit changes
                    SubmitStatusChanges();
                    // Populate and reselect pinned item
                    PopulatePinListAndReselect();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoCustomColorPinMessage(), MessageText.PinCaption(), false);
                msg.BringToFront();
                msg.ShowDialog();
            }
        }

        // Clear search field and search results
        private void btnClear_Click(object sender, EventArgs e)
        {
            Log.GeneralStatus("Clear fields");
            ClearContentSearchField();
            ClearSearchHistoryList();
            ClearSearchResults();
            RemoveNoLiveSearchResultsLabel();
        }
        #endregion

        #region NAVIGATION
        private static bool IgnoreNavigatorSelection { get; set; }
        private void AddToNavigator(Entry entry)
        {
            if (!IgnoreNavigatorSelection)
            {
                Navigator.AddEntry(entry);
                ResolveNavigationButtons();
            }
        }

        private void RemoveFromNavigator(Entry entry)
        {
            Navigator.RemoveEntry(entry);
            ResolveNavigationButtons();
        }

        private void NavigateBack()
        {
            if (Navigator.CanNavigateBack())
            {
                if (SaveCurrentEntry(!AutoSaveEntryChanges))
                {
                    IgnoreNavigatorSelection = true;
                    SelectEntryByID(Navigator.NavigateBack(), true, true);
                    IgnoreNavigatorSelection = false;
                    ResolveNavigationButtons();
                    // CheckRelated(); // TEST
                    // Check if the entry is in the pinned list and highlight it
                    if (CurrentEntry != null)
                    {
                        CheckListsAndHighlight(CurrentEntry.ID);
                    }
                }
                else
                {
                    tbContent.Focus();
                }
            }
            else
            {   // Return to content field if unable to navigate further
                tbContent.Focus();
            }
        }

        private void NavigateForward()
        {
            if (Navigator.CanNavigateForward())
            {
                if (SaveCurrentEntry(!AutoSaveEntryChanges))
                {
                    IgnoreNavigatorSelection = true;
                    SelectEntryByID(Navigator.NavigateForward(), true, true);
                    IgnoreNavigatorSelection = false;
                    ResolveNavigationButtons();
                    // CheckRelated(); // TEST
                    // Check if the entry is in the pinned list and highlight it
                    if (CurrentEntry != null)
                    {
                        CheckListsAndHighlight(CurrentEntry.ID);
                    }
                }
                else
                {
                    tbContent.Focus();
                }
            }
            else
            {   // Return to content field if unable to navigate further
                tbContent.Focus();
            }
        }

        // Set Navigation button colors
        private void ResolveNavigationButtons()
        {
            Navigator.ResolveNavigationButtons(btnBack, btnForward);
        }

        // If selected entry is pinned, highlight it in the pinned list
        private static bool CancelEntrySelectionForHighlight { get; set; }
        private static bool TempWasHighlightedInPinnedList { get; set; }
        private void CheckListsAndHighlight(string id) // Works alot like the method SelectPinned(string id) = nice to search by entry ID instead of toddling with indexes
        {
            TempWasHighlightedInPinnedList = false;
            if (listBoxPinned.Items.Count > 0)
            {
                listBoxPinned.BeginUpdate();
                for (int i = 0; i < listBoxPinned.Items.Count; i++)
                {
                    if (((EntryMinimal)listBoxPinned.Items[i]).ID == id)
                    {
                        TempWasHighlightedInPinnedList = true;
                        CancelEntrySelectionForHighlight = true;
                        listBoxPinned.SetSelected(i, true);
                        CancelEntrySelectionForHighlight = false;
                    }
                }
                listBoxPinned.EndUpdate();
            }

            // If not found in pinned list check search results
            if (!TempWasHighlightedInPinnedList)
            {
                if (listBoxSearchResult.Items.Count > 0)
                {
                    for (int i = 0; i < listBoxSearchResult.Items.Count; i++)
                    {
                        if (((EntryMinimal)listBoxSearchResult.Items[i]).ID == id)
                        {
                            CancelEntrySelectionForHighlight = true;
                            listBoxSearchResult.SetSelected(i, true);
                            CancelEntrySelectionForHighlight = false;
                        }
                    }
                }
            }


            // Search
            if (TempWasHighlightedInPinnedList && EntrySelectedBySearchResult)
            {
                EntrySelectedBySearchResult = false; // Inserted to try to avoid continuous reselects, 06-08-2022
                tbSearchInContent.Focus();
            }
        }
        #endregion

        #region CONTEXT MENU EVENTS
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tbContent.CanUndo())
                tbContent.Undo();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (tbContent.CanRedo())
                tbContent.Redo();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbContent.CutUnformatted();
            PersistClipboard();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbContent.CopyUnformatted();
            PersistClipboard();
        }


        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbContent.PasteUnformatted();
        }

        private void clearFormattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearFormatting();
        }

        private void insertDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertDate();
        }

        private void insertParagraphDividerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                InsertParagraphDivider(0, tbContent);
            }
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbContent.SelectAll();
        }

        private void viewInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEntryInformation();
        }

        private void toCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUpper();
        }

        private void selectionToLowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToLower();
        }

        private void slickFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a simple frame around selection
            SelectionToSlickFrame(false);
        }

        private void slickFrameAndCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToSlickFrame(true);
        }

        private void embossedFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToEmbossedFrame(false);
        }

        private void embossedFrameCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToEmbossedFrame(true);
        }

        private void slickTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToSlickTab(false);
        }

        private void slickTabCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToSlickTab(true);
        }

        private void embossedTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToEmbossedTab(false);
        }

        private void embossedTabCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToEmbossedTab(true);
        }

        private void bulletLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBulletAndLine(false);
        }

        private void bulletLineCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBulletAndLine(true);
        }

        private void bulletIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBullet(false);
        }

        private void bulletIndentCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToBullet(true);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUnderline(false);
        }

        private void underlineCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToUnderline(true);
        }

        private void deleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineDelete();
        }

        private void clearAllColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAllPinnedColors();
        }

        private void undoEverythingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (MirrorEntryExist(CurrentEntry.ID))
                {
                    AhoyAnswerMessageBox message = new AhoyAnswerMessageBox(MessageText.RevertChangesQuestion(), MessageText.AttentionCaption(), false, false);
                    message.TopMost = true;
                    message.ShowDialog();
                    if (message.Accepted)
                    {
                        tbContent.Text = GetMirrorEntryByID(CurrentEntry.ID).Content;
                    }
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.UnableToUndoAllChangesMessage(), MessageText.AttentionCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.UnableToUndoAllChangesMessage(), MessageText.AttentionCaption(), false);
                msg.ShowDialog();
            }
        }

        #endregion

        #region UI
        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        // Temp list to hold transfered items
        List<Entry> tempTransferPinnedItems;
        /// <summary>
        /// Ensure that only pinned are included when ranking and not related
        /// </summary>
        private void TransferPinnedForRanking()
        {
            if (PinnedList != null && PinnedList.Count > 0)
            {
                if (tempTransferPinnedItems == null)
                {
                    tempTransferPinnedItems = new List<Entry>();
                }
                // Ready list
                tempTransferPinnedItems.Clear();
                for (int i = 0; i < PinnedList.Count; i++)
                {
                    tempTransferPinnedItems.Add(IO.CloneEntry(PinnedList[i], true));
                }
                // Ready list
                PinnedList.Clear();
                for (int j = 0; j < tempTransferPinnedItems.Count; j++)
                {
                    if (tempTransferPinnedItems[j].Pin)
                    {
                        PinnedList.Add(tempTransferPinnedItems[j]);
                    }
                }
                // Cleanup
                tempTransferPinnedItems.Clear();
            }
        }

        // Move the entry in the collection to change sorting
        private int RankIndex { get; set; }
        private void RankPinned(Entry edited, bool moveUp, bool autoSubmitRanking)
        {
            if (!LockedForWritingToDisk)
            {
                if (PinnedList != null && PinnedList.Count > 0)
                {
                    TransferPinnedForRanking();

                    for (int i = 0; i < PinnedList.Count; i++)
                    {
                        // Find the index by ID to move
                        if (PinnedList[i].ID == edited.ID)
                        {
                            if (moveUp && i > 0) // If moveUp and i is larger than first index
                            {
                                PinnedList.RemoveAt(i); // Remove the item
                                PinnedList.Insert(i - 1, edited); // And insert it again at a lower index
                                RankIndex = (i - 1);
                            }
                            else if (moveUp && i == 0) // Else move entry to the bottom of the list
                            {
                                PinnedList.RemoveAt(i); // Remove the item
                                PinnedList.Insert(PinnedList.Count, edited); // Add it to bottom of list
                                RankIndex = (PinnedList.Count - 1);
                            }
                            else if (!moveUp && i < PinnedList.Count - 1) // If move down and i is smaller than last index
                            {
                                PinnedList.RemoveAt(i); // Remove the item
                                PinnedList.Insert(i + 1, edited); // And insert it again at a higher index
                                RankIndex = (i + 1);
                            }
                            else if (!moveUp && i == PinnedList.Count - 1) // Else move entry to the top of the list
                            {
                                PinnedList.RemoveAt(i); // Remove the item
                                PinnedList.Insert(0, edited); // Add it to top of list
                                RankIndex = 0;
                            }

                            // Break out of loop here
                            break;
                        }
                    }

                    // Used to update the ranking after one or multiple ranking presses
                    if (autoSubmitRanking)
                    {
                        SubmitPinnedRanking();
                    }

                    // Register changes
                    RegisterChangesForSaveAndBackup();
                }
            }
        }

        internal static void RegisterChangesForSaveAndBackup()
        {
            // Register changes (by adding chars the total char count will change)
            Changes += "x";
        }

        private void SubmitPinnedRanking()
        {
            // Submit pin order to Entries collection
            CreatePinnedIndexOrder();
            // Repopulate pinned list
            PopulatePinList();
            // Find and reselect after populating
            if (IsCurrentEntryPinned())
            {
                SelectPinned(CurrentEntry.ID);
            }
        }


        // Write new pinned ranking into main entries list
        // Save the current order as index numbers to sort by in
        // the main Entries collection
        private int pinnedIndexCounter { get; set; }
        private void CreatePinnedIndexOrder()
        {
            pinnedIndexCounter = 0;
            for (int i = 0; i < PinnedList.Count; i++) // Specific ordered entries
            {
                for (int j = 0; j < Entries.Count; j++) // Find matches and set index
                {
                    // Find mathing entries in the main entries collection
                    // and save index numer in zero based order
                    if (Entries[j].ID == PinnedList[i].ID)
                    {
                        Entries[j].Rank = pinnedIndexCounter;
                        pinnedIndexCounter += 1;
                    }
                }
            }

            // Save changes to disk (will occur when user is inactive or when exiting)
            SetPoolHasChanges();
        }

        // Select search result by index
        private void SelectPinned(int index)
        {
            if (index >= 0)
            {
                if (listBoxPinned.Items.Count > 0 && index <= listBoxPinned.Items.Count - 1)
                {
                    listBoxPinned.SetSelected(index, true);
                }
            }
        }

        // Select search result by ID (works alot like the method CheckListsAndHighlight)
        private void SelectPinned(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (listBoxPinned.Items.Count > 0)
                {
                    for (int i = 0; i < listBoxPinned.Items.Count; i++)
                    {
                        if (((EntryMinimal)listBoxPinned.Items[i]).ID == id)
                        {
                            CancelEntrySelectionForHighlight = true; // Dont reselect entry to minimize text flicker
                            listBoxPinned.SetSelected(i, true); // Select in the list
                            CancelEntrySelectionForHighlight = false;
                        }
                    }
                }
            }
        }

        // Check items in search list and update their icons
        private void RefreshSearchList()
        {
            try
            {
                if (listBoxSearchResult != null && listBoxSearchResult.Items.Count > 0)
                {
                    tempSearchItemPrefix = "";
                    for (int i = 0; i < listBoxSearchResult.Items.Count; i++)
                    {
                        if (EntryExist(((EntryMinimal)listBoxSearchResult.Items[i]).ID))
                        {

                            // Create prefix
                            if (GetEntryByID(((EntryMinimal)listBoxSearchResult.Items[i]).ID).MarkExport)
                            {
                                tempSearchItemPrefix = "EX>";
                            }
                            else
                            {
                                if (i <= 10)
                                {
                                    tempSearchItemPrefix = "F" + (i + 1).ToString() + ">";
                                }
                                else
                                {
                                    tempSearchItemPrefix = ">";
                                }
                            }

                            // Add item with prefix
                            listBoxSearchResult.BeginUpdate();
                            listBoxSearchResult.Items[i] = IO.ConvertToMinimalEntry(GetEntryByID(((EntryMinimal)listBoxSearchResult.Items[i]).ID), true, tempSearchItemPrefix);
                            listBoxSearchResult.EndUpdate();
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }


        //private string GetListPrefix(int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            return ">"; // Default search result pointer
        //        case 1:
        //            return "F"; // Function key indicator
        //        case 2:
        //            return "EX"; // Export
        //        case 3:
        //            return "N";  // Active notification
        //        case 4:
        //            return "I";  // Inactive notification
        //        case 5:
        //            return "╤"; // Pinned
        //        case 6:
        //            return "►"; // Notification indicator
        //        case 7:
        //            return "+"; // Related
        //        case 7:
        //            return "H1"; // Hold button 1
        //        case 7:
        //            return "H2"; // Hold button 2
        //        case 7:
        //            return "H►"; // Hold+Notification
        //    }
        //    return "N/A";
        //}


        private int FButtonShortcutCounter { get; set; }
        private string tempSearchItemPrefix;
        private void PopulateSearchListBox(ListBox lbControl, List<Entry> entries)
        {
            if (entries != null && entries.Count > 0)
            {
                // Shutdown the painting of the ListBox as items are added.
                lbControl.BeginUpdate();
                FButtonShortcutCounter = 0;
                tempSearchItemPrefix = "";
                foreach (Entry entry in entries)
                {
                    FButtonShortcutCounter += 1;
                    if (entry.MarkExport)
                    {
                        tempSearchItemPrefix = "EX>"; // "EX" <- just to make finding it easier by search
                    }
                    else if (Related.HasRelated(entry)) // Add related symbol
                    {
                        tempSearchItemPrefix = Related.GetMasterDisplayChar(FButtonShortcutCounter.ToString("00"), entry, 0, true);
                    }
                    else if (entry.MarkRelated)
                    {
                        tempSearchItemPrefix = Related.GetRelatedDisplayChar(entry, 0);
                    }
                    else if (Related.IsRelated(entry, Entries)) // Add related symbol
                    {
                        tempSearchItemPrefix = "+>";
                    }
                    else
                    {
                        if (entry.Pin)
                        {
                            tempSearchItemPrefix = "╤";
                        }
                        else
                        {
                            // Display F button shortcut for the first 10 entries (F1-F10) -F11 is used for Full Screen
                            if (FButtonShortcutCounter <= 10)
                            {
                                tempSearchItemPrefix = "F" + FButtonShortcutCounter.ToString() + ">";
                            }
                            else
                            {
                                tempSearchItemPrefix = ">";
                            }
                        }
                    }

                    // Finally add item to listbox
                    lbControl.Items.Add(IO.ConvertToMinimalEntry(entry, true, tempSearchItemPrefix));
                }
                // Allow the ListBox to repaint and display the new items.
                lbControl.EndUpdate();
            }
        }

        private void UpdateMenuItems()
        {
            // Set menu items checked state
            pinToolStripMenuItem.Checked = CurrentEntry.Pin;
            protectToolStripMenuItem.Checked = CurrentEntry.Protected;
            bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;
            UpdateHoldButtons();
        }


        private int HoldButtonSelected;
        private void UpdateHoldButtons()
        {
            HoldButtonSelected = 0;
            if (CurrentEntry != null)
            {
                // Resolve hold A's button text
                if (HoldEntryA)
                {
                    if (HoldEntryAID == CurrentEntry.ID)
                    {
                        btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[6];
                        btnHoldA.Text = "Selected";
                        HoldButtonSelected = 1;
                    }
                    else
                    {
                        btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[4];
                        btnHoldA.Text = "Recall";
                    }
                }
                else if (!HoldEntryA)
                {
                    ResetHoldButtonA();
                }

                // Resolve hold B's button text
                if (HoldEntryB)
                {
                    if (HoldEntryBID == CurrentEntry.ID)
                    {
                        btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[6];
                        btnHoldB.Text = "Selected";
                        HoldButtonSelected = 2;
                    }
                    else
                    {
                        btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[4];
                        btnHoldB.Text = "Recall";
                    }
                }
                else if (!HoldEntryB)
                {
                    ResetHoldButtonB();
                }
            }
        }

        private void ResetHoldButtonA()
        {
            btnHoldA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHoldA.Text = "Hold";
        }

        private void ResetHoldButtonB()
        {
            btnHoldB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnHoldB.Text = "Hold";
        }

        // Pinned list
        private void PopulatePinListAndReselect()
        {
            // Update pinned list
            PopulatePinList();
            // Select in pinned list if pinned
            ReselectPinned();
            // Resolve title field colors
            RefreshTitleColor();
        }

        // Pinned list
        private int PinListShortcutCounter { get; set; }
        private static List<Entry> PinnedList { get; set; } // Made static 14-07-2016
        private static int pinnedRelatedCounter;
        private void PopulatePinList()
        {
            // Reset activity for global timer
            KeepAwake();

            PinListShortcutCounter = 0;
            pinnedRelatedCounter = 0;
            if (Entries != null && Entries.Count >= 0)
            {
                if (PinnedList == null)
                {
                    PinnedList = new List<Entry>();
                }
                else
                {
                    PinnedList.Clear();
                }

                // Add entries and sort them in the new list if Pool count is more than zero
                if (Entries.Count > 0)
                {
                    foreach (Entry e in Entries)
                    {
                        if (e.Pin)
                        {
                            PinnedList.Add(e);
                        }
                    }

                    // Sort list to display the smallest rank numbers first
                    PinnedList.Sort(delegate (Entry x, Entry y) { return x.Rank.CompareTo(y.Rank); });

                    // Related
                    // If there is any related entries in the pinned list insert them into the list
                    // from the Pool after each entry
                    if (PinnedList.Count > 0 && Related.HasRelated(PinnedList))
                    {
                        // Run through the pinned list to check for related
                        for (int i = 0; i < PinnedList.Count; i++)
                        {
                            if (Related.HasRelated(PinnedList[i]) && PinnedList[i].RelatedView) // Check if item has related ids and is expanded
                            {
                                // Get the number of entries to add for the current entry
                                pinnedRelatedCounter = Related.CountRelated(PinnedList[i]);
                                // Insert range and bump up the i-variable to continue after the inserted items
                                if ((i + 1) < PinnedList.Count)
                                {
                                    // If inside the list insert the items
                                    PinnedList.InsertRange(i + 1, Related.RelatedConvert(Related.GetRelatedAsClone(PinnedList[i], Entries), PinnedList[i].RelatedStyle));
                                    i += pinnedRelatedCounter;
                                }
                                else
                                {
                                    // At the end of the list append the items marked as related (this means that all related entries in
                                    // the pinned list can be identified throug the "MarkRelated" property
                                    PinnedList.AddRange(Related.RelatedConvert(Related.GetRelatedAsClone(PinnedList[i], Entries), PinnedList[i].RelatedStyle));
                                    i += pinnedRelatedCounter;
                                }
                            }
                        }
                    }

                    // Submit entries to pinned list
                    SubmitToPinnedList(PinnedList);
                }
                else
                {
                    // Clear list
                    listBoxPinned.Items.Clear();
                    // Allow the ListBox to repaint and display the new items.
                    listBoxPinned.EndUpdate();
                    if (PinnedIsVisible)
                    {
                        ShowAnimation();
                    }
                }
            }
        }

        // Pinned list
        private void SortPinnedList(bool direction)
        {
            // Return if no pinned are available
            if (!HasPinned(Entries))
            {
                return;
            }

            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Sort pinned entries " + GetSortDirection(direction) + "?", "Sort Pinned", false, true);
            msg.ShowDialog();

            if (!msg.Accepted)
            {
                return;
            }
            // Deselect
            ClearDisplays(true);
            Log.GeneralStatus("Sort pinned " + GetSortDirection(direction));
            // Save pool for undo
            SaveBackupForUndo();
            CanUndoSort = true;
            // Reset activity for global timer
            KeepAwake();

            PinListShortcutCounter = 0;
            if (Entries != null && Entries.Count >= 0)
            {
                if (PinnedList == null)
                {
                    PinnedList = new List<Entry>();
                }
                else
                {
                    PinnedList.Clear();
                }

                // Add entries and sort them in the new list if Pool count is more than zero
                if (Entries.Count > 0)
                {
                    foreach (Entry e in Entries)
                    {
                        if (e.Pin)
                        {
                            PinnedList.Add(e);
                        }
                    }

                    // Sort list alphabetically
                    if (direction)
                    {
                        PinnedList.Sort(delegate (Entry x, Entry y) { return x.Title.CompareTo(y.Title); });
                    }
                    else
                    {
                        PinnedList.Sort(delegate (Entry x, Entry y) { return y.Title.CompareTo(x.Title); });
                    }
                    // Submit entries to pinned list
                    SubmitToPinnedList(PinnedList);
                    // Submit new ranking
                    SubmitPinnedRanking();
                }
                else
                {
                    // Clear list
                    listBoxPinned.Items.Clear();
                    // Allow the ListBox to repaint and display the new items.
                    listBoxPinned.EndUpdate();
                    if (PinnedIsVisible)
                    {
                        ShowAnimation();
                    }
                }
            }
        }

        private void sortAZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortPinnedList(true);
        }
        private void sortZAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortPinnedList(false);
        }
        private void undoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UndoSort();
        }

        private void UndoSort()
        {
            if (CanUndoSort)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Undo sort?", "Undo", false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    Log.GeneralStatus("Undo sort");
                    UndoLastCommand(false);
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to undo sort", "Undo", false);
                msg.ShowDialog();
            }
        }

        private string GetSortDirection(bool direction)
        {
            if (direction)
            {
                return "A-Z";
            }
            else
            {
                return "Z-A";
            }
        }

        // Pinned list
        // Submit entries to the pinned list and add identifier characters
        private void SubmitToPinnedList(List<Entry> PinnedList)
        {
            if (PinnedList != null && PinnedList.Count > 0)
            {
                // Clear list before populating
                listBoxPinned.Items.Clear();
                // Shutdown the painting of the ListBox as items are added.
                listBoxPinned.BeginUpdate();
                tempSearchItemPrefix = "";
                for (int i = 0; i < PinnedList.Count; i++)
                {
                    PinListShortcutCounter += 1; // Add number to display and select pins from list
                    PinnedList[i].Rank = i; // Rewrite rank to make new ranked items always appear in the bottom of the pinned list
                    if (PinnedList[i].MarkExport) // Export prefix
                    {
                        tempSearchItemPrefix = "EX";
                    }
                    else if (Related.HasRelated(PinnedList[i]))// && !PinnedList[i].RelatedView) // Master, collapsed view
                    {
                        tempSearchItemPrefix = Related.GetMasterDisplayChar(PinListShortcutCounter.ToString("00"), PinnedList[i], 1, false);
                    }
                    else if (PinnedList[i].MarkRelated) // Draw Expanded category symbols
                    {
                        tempSearchItemPrefix = Related.GetRelatedDisplayChar(PinnedList[i], 1);
                    }
                    else // Else draw normal prefixes
                    {
                        // If entry is held by Hold button A
                        if (PinnedList[i].ID == HoldEntryAID)
                        {
                            tempSearchItemPrefix = "H1";
                        }
                        // If entry is held by Hold button B
                        else if (PinnedList[i].ID == HoldEntryBID)
                        {
                            tempSearchItemPrefix = "H2";
                        }
                        else
                        {
                            tempSearchItemPrefix = PinListShortcutCounter.ToString("00");
                        }
                    }

                    // And finally add item with prefix
                    listBoxPinned.Items.Add(IO.ConvertToMinimalEntry(PinnedList[i], true, tempSearchItemPrefix));
                }
                // Allow the ListBox to repaint and display the new items.
                listBoxPinned.EndUpdate();
            }
            else
            {
                // Clear old items
                listBoxPinned.Items.Clear();
            }
        }



        // Custom listbox color
        private void lbEntries_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            // If the item state is selected then change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.BackColor,
                                          AhoyColors.CurrentColorTheme()[6]); //Choose the color

                // Draw the background of the ListBox control for each item.
                e.DrawBackground();
                // Draw the current item text
                e.Graphics.DrawString(((EntryMinimal)listBoxSearchResult.Items[e.Index]).Title, e.Font, ListBoxSearchResultSelectedForeColor, e.Bounds, StringFormat.GenericDefault);
            }
            else // Else draw the current selected or default color
            {
                if (((EntryMinimal)listBoxSearchResult.Items[e.Index]).PresetColor > 0 || AhoyColors.HasDualHexValue(((EntryMinimal)listBoxSearchResult.Items[e.Index]).CustomColor))
                {
                    // Change pinned status fore and background colors by passing the number saved in PinStatus
                    if (((EntryMinimal)listBoxSearchResult.Items[e.Index]).PresetColor > 0)
                    {
                        tempStatusBackgroundColor = Design.ReturnStatusBackgroundColor(((EntryMinimal)listBoxSearchResult.Items[e.Index]).PresetColor);
                        tempStatusForegroundColor = new SolidBrush(Design.ReturnStatusForegroundColor(((EntryMinimal)listBoxSearchResult.Items[e.Index]).PresetColor));
                    }
                    else // Or use the custom colors
                    {
                        tempStatusBackgroundColor = AhoyColors.ConvertFromHex(((EntryMinimal)listBoxSearchResult.Items[e.Index]).CustomColor.Split(';')[0], false);
                        tempStatusForegroundColor = new SolidBrush(AhoyColors.ConvertFromHex(((EntryMinimal)listBoxSearchResult.Items[e.Index]).CustomColor.Split(';')[1], false));
                    }

                    e = new DrawItemEventArgs(e.Graphics,
                          e.Font,
                          e.Bounds,
                          e.Index,
                          e.State ^ DrawItemState.Default,
                          e.BackColor,
                          tempStatusBackgroundColor);//Choose the color

                    // Draw the background of the ListBox control for each item.
                    e.DrawBackground();
                    // Draw the current item text
                    e.Graphics.DrawString(((EntryMinimal)listBoxSearchResult.Items[e.Index]).Title, e.Font, tempStatusForegroundColor, e.Bounds, StringFormat.GenericDefault);
                    // Calling Dispose allows the resources used by this Brush object to be reallocated for other purposes
                    tempStatusForegroundColor.Dispose();
                }
                else // Else draw the default item color
                {
                    // Draw the background of the ListBox control for each item.
                    e.DrawBackground();
                    // Draw the current item text
                    e.Graphics.DrawString(((EntryMinimal)listBoxSearchResult.Items[e.Index]).Title, e.Font, ListBoxSearchResultForeColor, e.Bounds, StringFormat.GenericDefault);
                }
            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }


        // Pinned list
        // Custom listbox color
        private static Color tempStatusBackgroundColor;
        private static Brush tempStatusForegroundColor;
        private void lbPinned_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            //if the item state is selected then change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.BackColor,
                                          AhoyColors.CurrentColorTheme()[6]);//Choose the color

                // Draw the background of the ListBox control for each item.
                e.DrawBackground();
                // Draw the current item text
                e.Graphics.DrawString(((EntryMinimal)listBoxPinned.Items[e.Index]).Title, e.Font, ListBoxPinnedSelectedForeColor, e.Bounds, StringFormat.GenericDefault);
            }
            else // Else draw the current selected or default color
            {
                if (((EntryMinimal)listBoxPinned.Items[e.Index]).PresetColor > 0 || AhoyColors.HasDualHexValue(((EntryMinimal)listBoxPinned.Items[e.Index]).CustomColor))
                {
                    // Change pinned status fore and background colors by passing the number saved in PinStatus
                    if (((EntryMinimal)listBoxPinned.Items[e.Index]).PresetColor > 0)
                    {
                        tempStatusBackgroundColor = Design.ReturnStatusBackgroundColor(((EntryMinimal)listBoxPinned.Items[e.Index]).PresetColor);
                        tempStatusForegroundColor = new SolidBrush(Design.ReturnStatusForegroundColor(((EntryMinimal)listBoxPinned.Items[e.Index]).PresetColor));
                    }
                    else // Or use the custom colors
                    {
                        tempStatusBackgroundColor = AhoyColors.ConvertFromHex(((EntryMinimal)listBoxPinned.Items[e.Index]).CustomColor.Split(';')[0], false);
                        tempStatusForegroundColor = new SolidBrush(AhoyColors.ConvertFromHex(((EntryMinimal)listBoxPinned.Items[e.Index]).CustomColor.Split(';')[1], false));
                    }

                    e = new DrawItemEventArgs(e.Graphics,
                          e.Font,
                          e.Bounds,
                          e.Index,
                          e.State ^ DrawItemState.Default,
                          e.BackColor,
                          tempStatusBackgroundColor);//Choose the color

                    // Draw the background of the ListBox control for each item.
                    e.DrawBackground();
                    // Draw the current item text
                    e.Graphics.DrawString(((EntryMinimal)listBoxPinned.Items[e.Index]).Title, e.Font, tempStatusForegroundColor, e.Bounds, StringFormat.GenericDefault);
                    // Calling Dispose allows the resources used by this Brush object to be reallocated for other purposes
                    tempStatusForegroundColor.Dispose();
                }
                else // Else draw the default item color
                {
                    // Draw the background of the ListBox control for each item.
                    e.DrawBackground();
                    // Draw the current item text
                    e.Graphics.DrawString(((EntryMinimal)listBoxPinned.Items[e.Index]).Title, e.Font, ListBoxPinnedForeColor, e.Bounds, StringFormat.GenericDefault);
                }
            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        // Pinned masking visuals
        private void UpdatePinAndMemoVisuals()
        {
            TogglePinAndMemoAnimation();
        }


        private void ShowPinned()
        {
            if (!PinnedIsVisible) // Enable double function for the pinned list button if AutoHide is enabled
            {
                HideAnimation();
                HideMemo();
                TogglePinnedListVisibility(true);
            }
            else if (AutoHidePinnedAndMemo)
            {
                ShowAnimation();
            }
        }

        private void ShowMemo()
        {
            // If show memo is clicked while showing memo, show animation instead/hide memo
            if (!MemoIsVisible && !ListExpanded) // Enable double function for the memo button if AutoHide is enabled
            {
                HideAnimation();
                HidePinned();
                ToggleMemoVisibility(true);
            }
            else if (AutoHidePinnedAndMemo)
            {
                ShowAnimation();
            }
        }

        private void ShowAnimation()
        {
            HidePinned();
            HideMemo();
            PinnedAndMemoAreHidden = true;
            ToggleAnimationVisibility(true);
        }

        private void HidePinned()
        {
            TogglePinnedListVisibility(false);
        }

        private void HideMemo()
        {
            ToggleMemoVisibility(false);
        }

        private void HideAnimation()
        {
            PinnedAndMemoAreHidden = false;
            ToggleAnimationVisibility(false);
        }



        private Label lbPinnedAndMemoAnimation;
        private void InitializeAnimationLabel()
        {
            // Create label for text animation if not already initialized
            // with pinned list as reference
            if (lbPinnedAndMemoAnimation == null)
            {
                // Use the pinned list color theme styling for the animation
                lbPinnedAndMemoAnimation = new Label();
                lbPinnedAndMemoAnimation.SetBounds(listBoxPinned.Location.X, listBoxPinned.Location.Y, listBoxPinned.Width, listBoxPinned.Height);
                lbPinnedAndMemoAnimation.Font = Design.ContentFieldFont();
                lbPinnedAndMemoAnimation.TextAlign = ContentAlignment.MiddleCenter;
                lbPinnedAndMemoAnimation.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
                this.splitContainer1.Panel1.Controls.Add(lbPinnedAndMemoAnimation);
                lbPinnedAndMemoAnimation.Cursor = Cursors.Default;
                lbPinnedAndMemoAnimation.SendToBack();
                this.toolTip1.SetToolTip(lbPinnedAndMemoAnimation, "Click to show Pinned or Memo");
                lbPinnedAndMemoAnimation.Visible = false;
                lbPinnedAndMemoAnimation.Click += PinnedListMaskLabel_Click;
            }
        }

        private void TogglePinAndMemoAnimation()
        {
            //ShowPinAndMemoAnimation = !ShowPinAndMemoAnimation;

            // Hide pin and memo field
            if (ShowPinAndMemoAnimation)
            {
                ShowAnimation();
            }
            else
            {
                // Show the one that was last shown
                if (MemoWasLastVisible)
                {
                    ShowMemo();
                }
                else
                {
                    ShowPinned();
                }
            }
        }


        private void ToggleAnimationVisibility(bool showAnimation)
        {
            ShowPinAndMemoAnimation = showAnimation;

            if (showAnimation)
            {
                // Clear the small label above the panel
                lbPinned.Text = "";
                // Hide pinned and memo list
                HidePinned();
                HideMemo();
                // Show animation label
                // Get the latest color theme form the list box
                lbPinnedAndMemoAnimation.ForeColor = listBoxPinned.ForeColor;
                lbPinnedAndMemoAnimation.BackColor = listBoxPinned.BackColor;
                lbPinnedAndMemoAnimation.Font = Design.ContentFieldFont();
                lbPinnedAndMemoAnimation.BringToFront();
                lbPinnedAndMemoAnimation.Visible = true;
                lbPinnedAndMemoAnimation.Text = "";
                MaskAnimation.ResetAnimation();
                BringExpanderToFront();
            }
            else
            {
                // Hide animation label
                lbPinnedAndMemoAnimation.SendToBack();
                lbPinnedAndMemoAnimation.Visible = false;

                // Search
                // Switch focus back to previous control
                if (!tbSearchInContent.Focused && CurrentEntryNotNullOrDummy())
                {
                    tbContent.Focus();
                }
                else if (!tbSearchInContent.Focused)
                {
                    tbSearch.Focus();
                }
            }

            ResolveAnimationButton();
        }

        private void ResolveAnimationButton()
        {
            if (lbPinnedAndMemoAnimation.Visible)
            {
                btnTogglePinAndMemo.BackgroundImage = ButtonBitmaps.SleepHighlighted();
            }
            else
            {
                btnTogglePinAndMemo.BackgroundImage = ButtonBitmaps.Sleep();
            }
        }


        // Show or hide pinned list
        private void TogglePinnedListVisibility(bool showPinned)
        {
            if (showPinned)
            {
                PinnedIsVisible = true;
                lbPinned.Text = "Pinned";
                MemoWasLastVisible = false;
                listBoxPinned.Visible = true;
                btnPinnedUp.Enabled = true;
                btnPinnedDown.Enabled = true;
                btnPinnedTop.Enabled = true;
                btnPinnedBottom.Enabled = true;
            }
            else
            {
                PinnedIsVisible = false;
                listBoxPinned.Visible = false;
                btnPinnedUp.Enabled = false;
                btnPinnedDown.Enabled = false;
                btnPinnedTop.Enabled = false;
                btnPinnedBottom.Enabled = false;

                // Search
                // Switch focus back to previous control
                if (!tbSearchInContent.Focused && CurrentEntryNotNullOrDummy())
                {
                    tbContent.Focus();
                }
                else if (!tbSearchInContent.Focused)
                {
                    tbSearch.Focus();
                }
            }

            ResolvePinnedButton();
        }

        // Update pinned button visuals
        private void ResolvePinnedButton()
        {
            if (listBoxPinned.Visible)
            {
                btnTogglePinned.BackgroundImage = ButtonBitmaps.PinlistHighlight();
                showPinnedToolStripMenuItem.Checked = true;
            }
            else if (!listBoxPinned.Visible && listBoxPinned.Items.Count > 0)
            {
                btnTogglePinned.BackgroundImage = ButtonBitmaps.PinlistActive();
                showPinnedToolStripMenuItem.Checked = false;
            }
            else
            {
                btnTogglePinned.BackgroundImage = ButtonBitmaps.Pinlist();
                showPinnedToolStripMenuItem.Checked = false;
            }

            ResolvePinnedNavigationButtons();
        }

        private void ResolvePinnedNavigationButtons()
        {
            if (listBoxPinned.Visible && listBoxPinned.SelectedIndex != -1 && !IsEntryInPinnedListRelated(listBoxPinned.SelectedItem)) // Related update
            {
                btnPinnedUp.BackgroundImage = ButtonBitmaps.UpHighlight();
                btnPinnedDown.BackgroundImage = ButtonBitmaps.DownHighlight();
                btnPinnedTop.BackgroundImage = ButtonBitmaps.TopHighlight();
                btnPinnedBottom.BackgroundImage = ButtonBitmaps.BottomHighlight();
            }
            else
            {
                btnPinnedUp.BackgroundImage = ButtonBitmaps.Up();
                btnPinnedDown.BackgroundImage = ButtonBitmaps.Down();
                btnPinnedTop.BackgroundImage = ButtonBitmaps.Top();
                btnPinnedBottom.BackgroundImage = ButtonBitmaps.Bottom();
            }

        }

        // Add the basic context menu to all textboxes
        // that don't have a special context menu assigned
        private void InitializeBasicContextMenus()
        {
            tbSearch.ContextMenuStrip = ahoyContextMenuSearchFields;
            tbSearchInContent.ContextMenuStrip = ahoyContextMenuSearchFields;
            tbTitle.ContextMenuStrip = contextMenuBasicTitle;
        }

        private HistoryTextBox tbMemoField;
        private void InitializeMemoField()
        {
            // Create Memo textbox
            if (tbMemoField == null)
            {
                // Use pinned list as size and locationreference
                tbMemoField = new HistoryTextBox();
                tbMemoField.Multiline = true;
                tbMemoField.DetectUrls = true;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbMemoField.BorderStyle = BorderStyle.None;
                tbMemoField.SetBounds(listBoxPinned.Location.X, listBoxPinned.Location.Y, listBoxPinned.Width, listBoxPinned.Height);
                tbMemoField.Font = Design.ContentFieldFont();
                tbMemoField.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
                this.splitContainer1.Panel1.Controls.Add(tbMemoField);
                tbMemoField.SendToBack();
                tbMemoField.Visible = false;
                tbMemoField.TextChanged += MemoField_TextChanged;
                tbMemoField.KeyPress += tbMemoField_KeyPress;
                tbMemoField.KeyDown += TbMemoField_KeyDown;
                tbMemoField.Click += TbMemoField_Click;
                tbMemoField.ContextMenuStrip = contextMenuMemo;
            }
        }

        private void TbMemoField_KeyDown(object sender, KeyEventArgs e)
        {
            KeepAwake();
            if (!InterfaceLock)
            {
                // This multi shortcut statement must be first
                if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C)) // Multiple shortcut keypress (Copy formatted)
                {
                    tbMemoField.Copy();
                    PersistClipboard();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.X)) // Multiple shortcut keypress (Cut formatted)
                {
                    tbMemoField.Cut();
                    PersistClipboard();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X)) // Multiple shortcut keypress
                {
                    GeneratePasswordsInMemoField();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.D)) // Multiple shortcut keypress
                {
                    DuplicateSelection(tbMemoField);
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
                {
                    tbMemoField.CopyUnformatted();
                    PersistClipboard();
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
                {
                    tbMemoField.CutUnformatted();
                    PersistClipboard();
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.V) // Paste unformatted
                {
                    tbMemoField.PasteUnformatted();
                    PersistClipboard();
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.T) // Insert timestamp
                {
                    tbMemoField.InsertISOTimestamp();
                    e.Handled = true;
                }
                if (e.Control && e.KeyCode == Keys.Oemplus) // Multiple shortcut keypress (Insert Paragraph Divider)
                {
                    InsertParagraphDivider(0, tbMemoField);
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.Oemcomma)
                {
                    InsertParagraphDivider(1, tbMemoField);
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.OemPeriod)
                {
                    InsertParagraphDivider(2, tbMemoField);
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.OemMinus)
                {
                    InsertParagraphDivider(3, tbMemoField);
                    e.Handled = true;
                }
            }
        }


        // Ensure memo field responds to pressing Escape
        void tbMemoField_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeepAwake();
            if (!InterfaceLock)
            {
                if ((Keys)e.KeyChar == Keys.Escape)
                {
                    ClearDisplays(true);
                    e.Handled = true;
                }
            }
        }

        private void TbMemoField_Click(object sender, EventArgs e)
        {
            MemoField_TextChanged(this, null);
        }

        // Show or hide memo
        private void ToggleMemoVisibility(bool showMemo)
        {
            if (showMemo)
            {
                MemoIsVisible = true;
                MemoWasLastVisible = true;
                lbPinned.Text = "Memo";
                tbMemoField.BackColor = AhoyColors.CurrentColorTheme()[29];
                tbMemoField.ForeColor = AhoyColors.CurrentColorTheme()[30];
                tbMemoField.Font = Design.FieldFont(ListsFontSize);
                tbMemoField.BringToFront();
                tbMemoField.Visible = true;
                FocusMemoField();
            }
            else
            {
                MemoIsVisible = false;
                tbMemoField.SendToBack();
                tbMemoField.Visible = false;

                // Search
                // Switch focus back to previous control
                if (!tbSearchInContent.Focused && CurrentEntryNotNullOrDummy())
                {
                    tbContent.Focus();
                }
                else if (!tbSearchInContent.Focused)
                {
                    tbSearch.Focus();
                }
            }

            // Update visuals for button
            ResolveMemoButton();
        }

        // The three memo buttons image states: deselected, deselected(with content), selected
        private void ResolveMemoButton()
        {
            if (tbMemoField.Visible)
            {
                btnToggleMemo.BackgroundImage = ButtonBitmaps.MemoHighlight();
                showMemoToolStripMenuItem.Checked = true;
            }
            else if (!tbMemoField.Visible && tbMemoField.Text.Length > 0)
            {
                btnToggleMemo.BackgroundImage = ButtonBitmaps.MemoActive();
                showMemoToolStripMenuItem.Checked = false;
            }
            else
            {
                btnToggleMemo.BackgroundImage = ButtonBitmaps.Memo();
                showMemoToolStripMenuItem.Checked = false;
            }
        }


        // Save memo to Memo string
        private static bool MemoTextChangeRegisterChanges; // Used when loading, so the pool wont save immediately after
        void MemoField_TextChanged(object sender, EventArgs e)
        {
            KeepAwake();
            if (MemoTextChangeRegisterChanges)
            {
                // Save caret position
                MemoCaretPosition = tbMemoField.SelectionStart;

                // Tell Ahoy to save when possible
                SetPoolHasChanges();
            }

            // Always back to true after loading
            MemoTextChangeRegisterChanges = true;
        }


        // Used by pinned color changes
        private void SubmitStatusChanges()
        {
            // Update entries list to reflect changes immediately
            CurrentEntryHasChanges();
            SubmitChangesToEntriesList();

            // Tell Ahoy to save when possible
            SetPoolHasChanges();
        }

        private void DisplayPinnedSelectionMessage()
        {
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectPinnedEntryToContinueMessage(), MessageText.SelectEntryCaption(), false);
            msg.ShowDialog();
        }

        private void ReselectPinned()
        {
            if (!timerReselectPinned.Enabled)
            {
                timerReselectPinned.Interval = 300;
                timerReselectPinned.Enabled = true;
            }
        }

        #endregion

        #region TIMER
        private void StartGlobalTimers()
        {
            // Hide maintenance message if visible
            HideMaintenanceMessage();

            // Start main timer
            if (!timerGlobal.Enabled)
            {
                timerGlobal.Interval = 1000; // One second
                timerGlobal.Enabled = true;
            }

            // Start fast timer
            StartFastTimer();

            // Enable save settings
            AllowSavingSettings = true;
        }

        private void StopGlobalTimers()
        {
            // Stop main timer
            if (timerGlobal.Enabled)
            {
                timerGlobal.Enabled = false;
            }

            // Stop fast timer
            StopFastTimer();

            // Disable save settings
            AllowSavingSettings = false;
        }


        private void StartFastTimer()
        {
            if (!timerFastGlobal.Enabled)
            {
                timerFastGlobal.Interval = 100;
                timerFastGlobal.Enabled = true;
            }
        }

        private void StopFastTimer()
        {
            if (timerFastGlobal.Enabled)
            {
                timerFastGlobal.Enabled = false;
            }
        }

        private void StopGlobalTimerWithMaintenanceMessage()
        {
            Related.ClearMark();
            this.Text = GetFormTitle() + TextTransform.GetBlankSpace(3) + "(PAUSED)";
            // Update status strip
            Log.LogMaintenance("Started (timers suspended)");
            SetStatusText("PAUSED");
            lbMaintenance.Font = Design.MessageboxTinyFont();
            lbMaintenance.BackColor = AhoyColors.CurrentColorTheme()[6];
            lbMaintenance.ForeColor = AhoyColors.CurrentColorTheme()[1];
            lbMaintenance.Visible = true;
            lbMaintenance.Text = MessageText.GlobalTimerMaintenance();
            StopGlobalTimers();
            SetPaused(true);
        }

        private void SetPaused(bool state)
        {
            if (state)
            {
                IsPaused = true;

            }
            else
            {
                IsPaused = false;
            }
            UpdateSaveButton();
            RefreshDummyText();
        }

        private void StartGlobalTimerWithMaintenanceMessage()
        {
            this.Text = GetFormTitle();
            Log.LogMaintenance("Ended (timers enabled)");
            SetStatusText("Ready");
            HideMaintenanceMessage();
            StartGlobalTimers();
            // The IsPaused variable to false is handle by the global timer alone
        }

        // If Maintenance message was visible
        private void HideMaintenanceMessage() // Called by StartGlobalTimers and StartGlobalTimerWithMaintenanceMessage
        {
            if (lbMaintenance.Visible)
            {
                lbMaintenance.Visible = false;
                // The IsPaused variable to false is handle by the global timer alone
            }
        }



        private static bool tempPoolUpdated;


        // Fast timer (where fast reaction times are needed)
        private void timerFastGlobal_Tick(object sender, EventArgs e)
        {
            // Check for notification editor updates
        }

        // LOCAL + REMOTE BACKUP RUTINE LOGIC
        private void ResolveMainAndAlternateBackup()
        {
            // Check total char count (all text content is checked)
            PoolCurrentCharCount = GetPoolTotalCharacterCount();
            if (PoolPreviousCharCount != PoolCurrentCharCount)
            {
                PoolPreviousCharCount = PoolCurrentCharCount;
                // Validate alternate path and disable if invalid
                if (AlternateBackupPathValid(true))
                {
                    Log.LogSavingAlternateBackup();
                }
                // Force save
                ForceBackup = true;
            }
        }


        // If not autosaving, create a backup after every 7 save.
        // If autosaving (does this more often than manual) backup every 16 save.
        private int GetAutoBackupInvokeCount()
        {
            if (!AutoSaveEntryChanges)
            {
                return 7;
            }
            else
            {
                return 16;
            }
        }

        internal static void OpenCommandPromptPerRemote()
        {
            SpawnCommandPrompt = true;
        }





        // The global timer handles the auto save and auto lock feature
        internal static int ActivitySecondsCounter { get; set; }
        internal static int ActivityAutoBackupCounter { get; set; }
        private int AutoSaveSecondsCounter { get; set; }
        private void timerGlobal_Tick(object sender, EventArgs e)
        {
            // Remove paused flag
            if (IsPaused)
            {
                SetPaused(false);
            }

            // Clock
            UpdateClockDisplay();

            // Check for updates
            // if (InvokeCheckForUpdates)
            // {
            //     InvokeCheckForUpdates = false;
            //     DoCheckForUpdates();
            // }

            // Blink Indicator Label
            if (PoolIsReadOnly)
            {
                lbReadOnlyIndicator.Visible = !lbReadOnlyIndicator.Visible;
            }
            else
            {
                if (!lbReadOnlyIndicator.Visible)
                {
                    lbReadOnlyIndicator.Visible = true;
                    ResolveIndicatorLabel();
                }
            }

            // Remove indicator if screen blocker text visible

            // Screen Blocker per remote

            // Screen Blocker auto invoke (only run the counter if screenblocker is enabled,
            // and only if the interface isnt locked and not already running an active blocker)

            // Bring form to front
            if (RemoteStyleEditor)
            {
                RemoteStyleEditor = false;
                DisplayStyleEditor();
            }

            if (SpawnCommandPrompt)
            {
                SpawnCommandPrompt = false;
                OpenCommandPrompt();
            }

            // Typewriter control

            // Check for typewriter changes


            // Check for open notification selector preview panel

            // LOCAL + REMOTE BACKUP RUTINE - when Pool has different
            // char count than last time it was loaded or checked.
            // (check every 30 minutes, also when locked)
            RuntimeAutoBackupSeconds += 1;
            ActivityAutoBackupCounter += 1;
            if ((RuntimeAutoBackupSeconds >= 1800 || MainPoolAutoSaveCount >= GetAutoBackupInvokeCount()) && ActivityAutoBackupCounter >= 10) // 1800 = every 30 minutes or after X autosaves when 10 seconds of inactivity has elapsed
            {
                ActivityAutoBackupCounter = 0;
                MainPoolAutoSaveCount = 0;
                RuntimeAutoBackupSeconds = 0;
                if (!LockedForWritingToDisk)
                {
                    ResolveMainAndAlternateBackup();
                }
            }

            // Push counters while awake
            if (!InterfaceLock)
            {
                // Add to counters every second
                ActivitySecondsCounter += 1;
                AutoSaveSecondsCounter += 1;
            }

            // Check Utility, Style and Notification Editor Actions
            if (!InterfaceLock)
            {
                CheckForStyleEditorActions();
            }

            // A dialog asked for a background refresh (see RefreshForm)
            if (RefreshFormRequested)
            {
                RefreshFormRequested = false;
                this.Refresh();
            }

            // Check for Notifications every 15 seconds also when interface is locked
            // Will be disabled if pool has zero entries, and enabled the next time
            // a notification is set, to optimize performance.


            // Change color on save button if changes are found
            if (CurrentEntry != null && !InterfaceLock)
            {
                if (CurrentEntry.HasChanges && !CurrentEntry.IsDummyEntry && !PoolIsReadOnly)
                {
                    // Prevent from calling colortheme more than once
                    if (btnSave.StatusText != "cha")
                    {
                        btnSave.StatusText = "cha";
                        btnSave.ForeColor = AhoyColors.CurrentColorTheme()[28];
                    }
                }
                else
                {
                    // *** AUTO SAVE ENTRY BUTTON COLORIZATION ***
                    if (AutoSaveEntryChanges && !PoolIsReadOnly)
                    {
                        // Prevent from calling colortheme more than once
                        if (btnSave.StatusText != "aut")
                        {
                            btnSave.StatusText = "aut";
                            btnSave.ForeColor = AhoyColors.CurrentColorTheme()[4];
                        }
                    }
                    else if (!AutoSaveEntryChanges)
                    {
                        // Prevent from calling colortheme more than once
                        if (btnSave.StatusText != "man")
                        {
                            btnSave.StatusText = "man";
                            btnSave.ForeColor = AhoyColors.CurrentColorTheme()[8];
                        }
                    }
                }

                // *** AUTO SAVE ENTRY CHANGES ***
                if (ActivitySecondsCounter >= 2)
                {
                    if (AutoSaveEntryChanges && (CurrentEntry.HasChanges && !CurrentEntry.IsDummyEntry && !PoolIsReadOnly))
                    {
                        KeepAwake();
                        AutoSaveEntryWithButtonAnimation();
                    }
                }
            }

            // Show Pinned animation on starup with no entries pinned
            if (ShowPinnedAnimationOnEmptyPinnedList && listBoxPinned.Items.Count == 0 && !ShowPinAndMemoAnimation)
            {
                // Disable showing the animation again
                ShowPinnedAnimationOnEmptyPinnedList = false;
                ShowPinAndMemoAnimation = true;
                UpdatePinAndMemoVisuals();
            }

            // Display pinned mask animation
            if (AnimationsEnabled && !InterfaceLock && ShowPinAndMemoAnimation)
            {
                if (lbPinnedAndMemoAnimation != null)
                {
                    lbPinnedAndMemoAnimation.Text = MaskAnimation.GetMaskString();
                }
            }
            else if (!AnimationsEnabled)
            {
                if (lbPinnedAndMemoAnimation != null && !string.IsNullOrEmpty(lbPinnedAndMemoAnimation.Text))
                {
                    lbPinnedAndMemoAnimation.Text = "";
                }
            }

            // Go to sleep if no user activity was registered within interval set by SleepInterval
            // plus save pool if it has changes
            if (!InterfaceLock && !AttachedEditorOpen)
            {
                if (ActivitySecondsCounter >= SleepIntervalSeconds && SleepIntervalSeconds < NeverSleepValue) // SleepInterval holds the value for when Ahoy should sleep
                {
                    // Prepare sleep animation
                    lbInterfaceLocked.Text = "";
                    // Put ahoy to sleep
                    LockInterface();
                }
            }

            // Sleep animation
            if (AnimationsEnabled && InterfaceLock)
            {
                lbInterfaceLocked.SuspendLayout();
                lbInterfaceLocked.Text = SleepAnimation.GetSleepString();
                lbInterfaceLocked.ResumeLayout();
            }

            // In short: After 1 minute Ahoy will start pushing to save the pool
            // if the SleepSecondsCounter reach 10 or more (and the pool has changes).
            // In verbose: If Ahoy is not sleeping while logged in, save every 1 minutes.
            // If pool has changes and there was no user activity for 10 seconds
            // after which point it will try to save every 10 seconds
            if (AccessGranted && !InterfaceLock && !AttachedEditorOpen)
            {
                if (AutoSaveSecondsCounter >= 60 && ActivitySecondsCounter >= 10) // 60 seconds = 1 minute. 10 secods = Save-Retry...
                {
                    SaveSettings();
                    SavePool(false, false, false, "");
                    AutoSaveSecondsCounter = 0;
                    MainPoolAutoSaveCount += 1;
                }
                else if (AutoSaveSecondsCounter >= 3600) // Ensure a save is done after 60 minutes. 3600 = 60 minutes
                {
                    SaveSettings();
                    SavePool(false, false, false, "");
                    AutoSaveSecondsCounter = 0;
                    MainPoolAutoSaveCount += 1;
                }
            }

            // Forced backup called by autobackup and by command input (command: "backup")
            if (ForceBackup && !LockedForWritingToDisk)
            {
                ForceBackup = false;
                SetPoolHasChanges();
                SavePool(false, true, false, "");
            }

            // Force save settings invoked by PLOP editor when changing topmost status
            if (ForceSavingSettings)
            {
                ForceSavingSettings = false;
                SaveSettings();
            }

            // Open Utility/PLOP editor (set in LoadSettings method)

            // Resolve New Entry button highlight
            if (!InterfaceLock)
            {
                ResolveNewEntryButton();
            }
        }

        // Highlight New button if no entry is selected
        private void ResolveNewEntryButton()
        {
            if (CurrentEntry != null && CurrentEntry.IsDummyEntry && !NewEntryButtonIsHighlighted())
            {
                btnNew.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else if (CurrentEntryNotNullOrDummy() && NewEntryButtonIsHighlighted())
            {
                btnNew.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private bool NewEntryButtonIsHighlighted()
        {
            if (btnNew.ForeColor == AhoyColors.CurrentColorTheme()[6])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // All activity is resetting the sleep counter
        // Previously "ResetActivity"
        internal static void KeepAwake()
        {
            ActivitySecondsCounter = 0;
            ActivityAutoBackupCounter = 0;
            // TEST
            //Log.GeneralStatus("TEST");
        }

        // Invoke autosave
        internal void InvokeAutoSaveEntry()
        {
            if (AutoSaveEntryChanges && (CurrentEntry.HasChanges && !CurrentEntry.IsDummyEntry && !PoolIsReadOnly))
            {
                KeepAwake();
                AutoSaveEntryWithButtonAnimation();
            }
        }

        // Search
        // Timer used for reselecting pinned entries after coloration
        private void timerReselectPinned_Tick(object sender, EventArgs e)
        {
            timerReselectPinned.Enabled = false;
            // Reselect pinned
            if (IsCurrentEntryPinned() || IsCurrenRelatedAndVisibleInPinnedList())
            {
                // Show Pinned list if hidden/masked
                ShowPinned();
                CheckListsAndHighlight(CurrentEntry.ID);
            }
        }
        #endregion


        #region BOOKMARKS (addition 23-09-2015)
        private void bookmarkEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBookmark();
        }

        private void cbBookmark_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                SetBookmark();
            }
            else if (CurrentEntryIsDummy()) // Force bookmark back to false
            {
                cbBookmark.Checked = false;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToBookmarkMessage(), MessageText.BookmarkCaption(), false);
                msg.BringToFront();
                msg.ShowDialog();
            }
        }

        private void bookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBookmark();
        }

        private void replaceBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceBookmark();
        }

        private void replaceBookmarkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReplaceBookmark();
        }

        private void ReplaceBookmark()
        {
            BookmarkChanged = false;
            if (CurrentEntryNotNullOrDummy())
            {
                AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.ReplaceBookmarkQuestion(), MessageText.BookmarkCaption(), false, false);
                ahoyAnswerMessageBox.ShowDialog();

                if (ahoyAnswerMessageBox.Accepted)
                {
                    Log.SetBookmark();
                    BookmarkChanged = true;
                    CurrentEntry.Bookmark = tbContent.SelectionStart;
                    tbContent.Focus();
                }
            }
        }

        private bool BookmarkChanged;
        private void SetBookmark()
        {
            BookmarkChanged = false;
            if (CurrentEntryNotNullOrDummy())
            {
                // If bookmark is empty, set new
                if (CurrentEntry.Bookmark == 0 && tbContent.SelectionStart > 0)
                {
                    Log.SetBookmark();
                    SetStatusText(MessageText.BookmarkAdded());
                    BookmarkChanged = true;
                    CurrentEntry.Bookmark = tbContent.SelectionStart;
                    tbContent.Focus();
                }
                else if (CurrentEntry.Bookmark == 0 && tbContent.SelectionStart == 0)
                {
                    // Persist bookmarks button
                    cbBookmark.Checked = false;
                    bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;
                    tbContent.Focus();
                }
                else
                {
                    // Move cursor to bookmark
                    tbContent.Focus();
                    tbContent.Select(CurrentEntry.Bookmark, 0);
                    tbContent.ScrollToCaret();
                    // Persist bookmarks button
                    cbBookmark.Checked = true;
                    bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;
                }

                ResolveBookmarkButton();
            }
        }



        private void ResolveBookmarkButton()
        {

            if (CurrentEntryNotNullOrDummy())
            {
                if (BookmarkChanged)
                {
                    // Resolve button
                    if (CurrentEntry.Bookmark > 0)
                    {
                        cbBookmark.Checked = true;
                        bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;
                        // Force save
                        CurrentEntryHasChanges();
                        SaveCurrentEntry(false);
                    }
                    else
                    {
                        cbBookmark.Checked = false;
                        bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;
                        // Force save
                        CurrentEntryHasChanges();
                        SaveCurrentEntry(false);
                    }
                }
            }

            // Refresh visuals
            cbBookmark.ResolveDisplay();
        }

        private void cbBookmark_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // If right clicked ask to clear bookmark
                if (e.Button == MouseButtons.Right && CurrentEntry.Bookmark > 0)
                {
                    ResetBookmark();
                }

            }
        }

        private void clearBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetBookmark();
        }

        private void clearBookmarkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetBookmark();
        }

        private void ResetBookmark()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // Clear bookmark
                if (CurrentEntry.Bookmark > 0)
                {
                    AhoyAnswerMessageBox ahoyAnswerMessageBox = new AhoyAnswerMessageBox(MessageText.ClearBookmarkQuestion(), MessageText.BookmarkCaption(), false, false);
                    ahoyAnswerMessageBox.ShowDialog();

                    if (ahoyAnswerMessageBox.Accepted)
                    {
                        // Reset bookmark
                        Log.RemoveBookmark();
                        SetStatusText(MessageText.BookmarkCleared());
                        CurrentEntry.Bookmark = 0;
                        cbBookmark.Checked = false;
                        bookmarkEntryToolStripMenuItem.Checked = cbBookmark.Checked;

                        // Force save
                        CurrentEntryHasChanges();
                        SaveCurrentEntry(false);
                    }
                }
            }
        }
        #endregion


        #region Generic dialogs
        /// <summary>
        /// "No Entry selected"
        /// </summary>
        /// <param name="caption"></param>
        private void SelectEntryDialog(string caption)
        {
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), caption, false);
            msg.ShowDialog();
        }
        #endregion

        #region SPELL CHECK (addition 23-09-2015)
        private void spellCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbMemoField.Focused)
            {
                OpenSpellChecker(tbMemoField, false);
            }
            else
            {
                if (CurrentEntryNotNullOrDummy())
                {
                    OpenSpellChecker(tbContent, false);
                }
                else
                {
                    SelectEntryDialog("Spell Check");
                }
            }
        }

        private void openEmptySpellCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                OpenSpellChecker(tbContent, true);
            }
            else
            {
                SelectEntryDialog("Spell Check");
            }
        }

        private void spellCheckToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenSpellChecker(tbMemoField, false);
        }

        internal static void SetSpellCheckerLanguage(int language)
        {
            if (SpellCheckerLanguage != language)
            {
                SettingsHasChanges = true;
            }
            SpellCheckerLanguage = language;
        }

        internal static string SpellCheckedText;
        internal static bool SpellCheckSubmitted;
        private static int SpellCheckerCaretPosition;
        private WPFSpellChecker spellCheck;
        private void OpenSpellChecker(HistoryTextBox textboxToCheck, bool emptySpellChecker)
        {
            try
            {
                AttachedEditorOpen = true;
                KeepAwake();
                Log.SpellCheck();
                SpellCheckedText = "";
                SpellCheckSubmitted = false;
                SpellCheckerCaretPosition = 0;

                if (!emptySpellChecker)
                {
                    SpellCheckerCaretPosition = textboxToCheck.SelectionStart;
                }

                spellCheck = new WPFSpellChecker(textboxToCheck.Text, tbContent.WordWrap, emptySpellChecker, SpellCheckerLanguage, SpellCheckerCaretPosition, this.Opacity);
                spellCheck.ShowDialog();

                // Get language change
                // (is set from the WPFSpellChecker.cs which is the host for the WPF control that has the spellchecking capabilities)

                // If spell check was accepted replace the content with the
                // corrected text
                if (SpellCheckSubmitted && !string.IsNullOrEmpty(SpellCheckedText))
                {
                    textboxToCheck.Text = SpellCheckedText;
                }
            }
            finally
            {
                // Clear variables
                SpellCheckedText = "";
                AttachedEditorOpen = false;
                spellCheck.Dispose();
            }
        }


        #endregion


        #region PRINTING (addition 24-09-2015)
        private void printEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Print content
            if (CurrentEntryNotNullOrDummy())
            {
                Printing.PrintText(CurrentEntry.Title, CurrentEntry.Content.TrimEnd(new char[1] { '\n' }), Design.CurrentFont);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryPrintMessage(), MessageText.PrintCaption(), false);
                msg.ShowDialog();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printEntryToolStripMenuItem_Click(this, null);
        }
        #endregion



        #region SWITCH POOL (addition 27-09-2015)
        private void switchPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchPool();
        }
        #endregion


        #region MEMO FIELD (addition 01-10-2015)

        private void clearFormattingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearMemoFormatting();
        }

        string TempUnformattedMemoText { get; set; }
        private void ClearMemoFormatting()
        {
            tbMemoField.SuspendLayout();
            // Save caret position
            MemoCaretPosition = tbMemoField.SelectionStart;
            // Save composition
            TempUnformattedMemoText = tbMemoField.Text;
            // Reset textbox
            tbMemoField.Clear();
            // Add unformatted content back
            tbMemoField.SetText(TempUnformattedMemoText, false);
            tbMemoField.ResumeLayout();

            if (MemoCaretPosition > 0)
            {
                // Move cursor to last selected position
                tbMemoField.Focus();
                tbMemoField.Select(MemoCaretPosition, 0);
                tbMemoField.ScrollToCaret();
            }
        }

        private void FocusMemoField()
        {
            if (MemoCaretPosition > 0)
            {
                // Move cursor to last selected position
                tbMemoField.Focus();
                tbMemoField.Select(MemoCaretPosition, 0);
                tbMemoField.ScrollToCaret();
            }
            else
            {
                tbMemoField.Focus();
            }
        }


        // May only be called by the pinned button
        private void btnTogglePinned_Click(object sender, EventArgs e)
        {
            if (!PinnedIsVisible)
            {
                ShowPinned();
            }
            else
            {
                ShowAnimation();
            }
        }

        // May only be called by the memo button
        private void btnMemo_Click(object sender, EventArgs e)
        {
            if (!MemoIsVisible)
            {
                ShowMemo();
            }
            else
            {
                ShowAnimation();
            }
        }

        private void btnTogglePinAndMemo_Click(object sender, EventArgs e)
        {
            ClearSearchResults();
            ShowAnimation();
        }

        private void showMemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMemo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbMemoField.Cut();
            PersistClipboard();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbMemoField.CopyUnformatted();
            PersistClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbMemoField.PasteUnformatted();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbMemoField.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbMemoField.Redo();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox clearMemoMessage = new AhoyAnswerMessageBox(MessageText.ClearMemoQuestion(), MessageText.MemoCaption(), false, false);
            clearMemoMessage.ShowDialog();

            if (clearMemoMessage.Accepted)
            {
                tbMemoField.Text = "";
            }
        }
        #endregion


        #region LAYOUT MODE (addition 05-06-2016)

        // Modes:
        // 0 = Basic
        // 1 = More
        // 2 = Plenty (Default)

        private bool LayoutVisibility { get; set; }
        List<Control> controlsForVisibilityChange;
        private void SetLayoutMode(int displayMode)
        {
            // Log
            Log.LayoutMode(displayMode.ToString());
            // Force old displaymode 3 to default
            if (displayMode > 2)
            {
                displayMode = 2;
            }

            if (displayMode >= 0 && displayMode <= 2)
            {

                // Save mode
                LayoutMode = displayMode;

                if (controlsForVisibilityChange == null)
                {
                    controlsForVisibilityChange = new List<Control>();
                }

                // Reset list and controls
                ResetVisibilityMode();
                controlsForVisibilityChange.Clear();

                // Default (standard controls)
                if (displayMode == 0)
                {
                    LayoutVisibility = false;
                    // Buttons
                    controlsForVisibilityChange.Add(btnHoldA);
                    controlsForVisibilityChange.Add(btnHoldB);
                    controlsForVisibilityChange.Add(cbBookmark);
                    controlsForVisibilityChange.Add(cbProtect);
                    controlsForVisibilityChange.Add(btnTogglePinAndMemo);
                    controlsForVisibilityChange.Add(btnToggleMemo);
                    controlsForVisibilityChange.Add(btnTogglePinned);
                    controlsForVisibilityChange.Add(pnlSpawnSticky);
                }
                else if (displayMode == 1)
                {
                    LayoutVisibility = false;
                    // Buttons
                    controlsForVisibilityChange.Add(cbProtect);
                    controlsForVisibilityChange.Add(btnTogglePinAndMemo);

                } // Rich (all controls)
                else if (displayMode == 2)
                {
                    // Do nothing (is handled in top of method)

                }

                // Update controls
                DisplayModes.ChangeDisplayMode(LayoutVisibility, controlsForVisibilityChange);

                // Set menu checked
                ResolveLayoutMenuItems(displayMode);

                // Register changes
                SettingsHasChanges = true;
            }
        }

        private void ResetVisibilityMode()
        {
            LayoutVisibility = true;
            // Buttons
            controlsForVisibilityChange.Add(btnHoldA);
            controlsForVisibilityChange.Add(btnHoldB);
            controlsForVisibilityChange.Add(cbBookmark);
            controlsForVisibilityChange.Add(cbProtect);
            controlsForVisibilityChange.Add(btnTogglePinAndMemo);
            controlsForVisibilityChange.Add(btnToggleMemo);
            controlsForVisibilityChange.Add(btnTogglePinned);
            controlsForVisibilityChange.Add(btnTogglePinned);

            // Additional buttons
            controlsForVisibilityChange.Add(btnSearch);
            controlsForVisibilityChange.Add(btnClear);
            controlsForVisibilityChange.Add(btnHistory);
            controlsForVisibilityChange.Add(btnPinnedUp);
            controlsForVisibilityChange.Add(btnPinnedDown);
            controlsForVisibilityChange.Add(btnPinnedTop);
            controlsForVisibilityChange.Add(btnPinnedBottom);
            controlsForVisibilityChange.Add(btnNew);
            controlsForVisibilityChange.Add(btnSave);
            controlsForVisibilityChange.Add(btnDelete);
            controlsForVisibilityChange.Add(btnSleep);
            controlsForVisibilityChange.Add(cbPinned);
            controlsForVisibilityChange.Add(btnSearchInContent);
            controlsForVisibilityChange.Add(btnClose);
            controlsForVisibilityChange.Add(btnBack);
            controlsForVisibilityChange.Add(btnForward);

            // Scrollbars
            // Special
            UpdateListScrollbars();
            if (Design.EnforceScrollbars)
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            }
            else
            {
                tbContent.ScrollBars = RichTextBoxScrollBars.Vertical;
                tbMemoField.ScrollBars = RichTextBoxScrollBars.Vertical;
            }

            // Labels
            controlsForVisibilityChange.Add(lbSearch);
            controlsForVisibilityChange.Add(lbContentSearch);
            controlsForVisibilityChange.Add(lbPinned);
            controlsForVisibilityChange.Add(lbTitle);
            controlsForVisibilityChange.Add(lbContent);

            // Set visibility
            DisplayModes.ChangeDisplayMode(LayoutVisibility, controlsForVisibilityChange);
        }

        private void UpdateListScrollbars()
        {
            listBoxPinned.ScrollAlwaysVisible = Design.EnforceScrollbars;
            listBoxSearchResult.ScrollAlwaysVisible = Design.EnforceScrollbars;
        }


        private void toggleLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void basicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetLayoutMode(0);
        }

        private void moreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLayoutMode(1);
        }

        private void plentyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLayoutMode(Settings.DefaultLayoutMode);
        }

        private void ResolveLayoutMenuItems(int displayMode)
        {
            switch (displayMode)
            {
                case 0:
                    defaultToolStripMenuItem1.Checked = true; // <--
                    richToolStripMenuItem.Checked = false;
                    minimalToolStripMenuItem.Checked = false;
                    break;
                case 1:
                    defaultToolStripMenuItem1.Checked = false;
                    richToolStripMenuItem.Checked = true;   // <--
                    minimalToolStripMenuItem.Checked = false;
                    break;
                case 2:
                    defaultToolStripMenuItem1.Checked = false;
                    richToolStripMenuItem.Checked = false;
                    minimalToolStripMenuItem.Checked = true;  // <--
                    break;
            }
        }
        #endregion


        #region PASSWORD GENERATOR (addition 25-06-2016 (changed 16-04-2017))
        // Will create a strong password with 8 to 64 unique chars
        // From a selection of 94 printable ASCII characters
        private void generatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryIsDummy())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.PasswordGeneratorSelectEntryToContinueMessage(), MessageText.PasswordGeneratorCaption(), false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    CreateNewEntry("", false, "Keys " + IO.GetIsoDateTimeNow(), true, false, false);
                }
                else
                {
                    return;
                }
            }

            if (!InterfaceLock)
            {
                // Clipboard sometimes fails, hence the try-catch
                try
                {
                    PasswordTypeSelector pwts = new PasswordTypeSelector(SelectedPasswordStyle);
                    pwts.TopMost = true;
                    pwts.ShowDialog();

                    if (pwts.GenerationSuccess)
                    {
                        // Record user selection
                        if (SelectedPasswordStyle != pwts.CurrentSelection)
                        {
                            SelectedPasswordStyle = pwts.CurrentSelection;
                            SettingsHasChanges = true;
                        }

                        // Paste password text to textbox
                        tbContent.Focus();
                        SetClipboardText(pwts.GetGeneratedKeys());
                        tbContent.Paste();

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
        }
        #endregion


        #region ADDED LISTBOX EVENTS (addition 26-06-2016)

        // If the mouse is placed over the listbox, keep copy and cutting shortcuts
        // affecting the content field
        private void ListBoxPinned_KeyDown(object sender, KeyEventArgs e)
        {
            // Use the content keydown, as the listBoxSearchResult
            tbContent_KeyDown(this, e);
        }

        private void ListBoxPinned_MouseClick(object sender, MouseEventArgs e)
        {
            ResetActivityUponMouseMovement();
        }

        // Search
        private void ListBoxSearchResult_MouseMove(object sender, MouseEventArgs e)
        {
            if (!tbSearchInContent.Focused && listBoxSearchResult.Items.Count > 0 && ListsActUponMouseMovement)
            {
                listBoxSearchResult.Focus(); // Set focus to enable scrolling
                ResetActivityUponMouseMovement();
            }
        }




        // Search
        private void ListBoxPinned_MouseMove(object sender, MouseEventArgs e)
        {
            if (!tbSearchInContent.Focused && listBoxPinned.Items.Count > 0 && ListsActUponMouseMovement)
            {
                listBoxPinned.Focus(); // Set focus to enable scrolling (Do not keep awake here, to avoid never sleeping if the mouse is over the pinned list)
            }
        }

        private void TbContent_VScroll(object sender, EventArgs e)
        {
            ListsActUponMouseMovement = true;
            PerformAutoHidePinned();
            ResetActivityUponMouseMovement();
        }

        private void TbContent_MouseWheel(object sender, MouseEventArgs e)
        {
            ListsActUponMouseMovement = true;
            PerformAutoHidePinned();
            ResetActivityUponMouseMovement();
        }


        private void TbContent_MouseClick(object sender, MouseEventArgs e)
        {
            ListsActUponMouseMovement = true;
            PerformAutoHidePinned();
            ResetActivityUponMouseMovement();
            SaveCaretPosition();
        }


        private void ResetActivityUponMouseMovement()
        {
            if (AccessGranted && !LockedForWritingToDisk && !InterfaceLock)
            {
                KeepAwake();
            }
        }


        #endregion


        #region AUTO HIDE LISTS (addition 27-06-2016)
        private void autoHideListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAutoHide();
        }

        private void autoHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAutoHide();
        }

        private void ToggleAutoHide()
        {
            AutoHidePinnedAndMemo = !AutoHidePinnedAndMemo;
            ResolveAutoHideListsMenuCheck();
            SettingsHasChanges = true;
        }

        private void ResolveAutoHideListsMenuCheck()
        {
            // Main menu
            autoHideListsToolStripMenuItem.Checked = AutoHidePinnedAndMemo;

            // Context menu
            if (AutoHidePinnedAndMemo)
            {
                autoHideToolStripMenuItem.Checked = true;
            }
            else
            {
                autoHideToolStripMenuItem.Checked = false;
            }
        }

        // Autohide pinned
        private void PerformAutoHidePinned()
        {
            // The PinnedIsHidden variable is set in the ShowAnimation and HideAnimation methods
            if (AutoHidePinnedAndMemo && !PinnedAndMemoAreHidden)
            {
                ShowAnimation();
            }
        }


        #endregion


        #region BACKUP RECOVERY (addition 29-06-2015)
        private void loadBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBackupPool();
        }
        #endregion


        #region JUMP HELD ENTRIES (addition 12-07-2016)
        private void jumpBetweenHeldEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HoldEntryA && HoldEntryB)
            {
                // Select hold B if A is selected
                if (HoldEntryAID == CurrentEntry.ID)
                {
                    ToggleHoldEntryB();
                }
                else
                {
                    ToggleHoldEntryA();
                }
            }
            else
            {
                SelectByTabMemory();
            }
        }
        #endregion


        #region BACKGROUND TEXTURE (addition 25-07-2016)
        // Code moved to Style Editor 14-08-2016
        #endregion

        #region TEXT OPERATIONS (addition 04-08-2016)
        private void eOLToSpaceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbContent.RemoveNewLines(false, true);
        }

        private void eOLToSpacePreserveParagraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbContent.RemoveNewLines(true, true);
        }

        private void tabToSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbContent.TabToSpace();
        }

        private void trimSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbContent.TrimSpace();
        }

        #endregion

        #region STYLES EDITOR (addition 14-08-2016)
        private void colorThemesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenThemeSelector(true);
        }
        #endregion

        #region Universal Basic Context Menu (compatible with both TextBox and HistoryTextBox)(addition 18-08-2016)

        private void cutToolStripMenuBasicItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuBasicItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuBasicItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }



        #endregion

        #region Browser options added (addition 04-09-2016)
        private void openFirefoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellCommands.StartProcess("firefox", "");
        }

        private void openChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellCommands.StartProcess("chrome", "");
        }

        private void openChromeIncognitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellCommands.StartProcess("chrome", "-incognito");
        }

        private void openiexploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellCommands.StartProcess("msedge", "");
        }

        // Default browser selection from the menu
        private void windowsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedInternetBrowser = 0;
            ResolveDefaultBrowserCheckedMenuItems();
            SettingsHasChanges = true;
        }

        private void firefoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedInternetBrowser = 1;
            ResolveDefaultBrowserCheckedMenuItems();
            SettingsHasChanges = true;
        }

        private void chromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedInternetBrowser = 2;
            ResolveDefaultBrowserCheckedMenuItems();
            SettingsHasChanges = true;
        }

        private void iExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedInternetBrowser = 3;
            ResolveDefaultBrowserCheckedMenuItems();
            SettingsHasChanges = true;
        }

        private void chromeIncognitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedInternetBrowser = 4;
            ResolveDefaultBrowserCheckedMenuItems();
            SettingsHasChanges = true;
        }

        private void ResolveDefaultBrowserCheckedMenuItems()
        {
            windowsDefaultToolStripMenuItem.Checked = false;
            firefoxToolStripMenuItem.Checked = false;
            chromeToolStripMenuItem.Checked = false;
            iExplorerToolStripMenuItem.Checked = false;
            chromeIncognitoToolStripMenuItem.Checked = false;

            switch (SelectedInternetBrowser)
            {
                case 0:
                    windowsDefaultToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    firefoxToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    chromeToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    iExplorerToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    chromeIncognitoToolStripMenuItem.Checked = true;
                    break;
            }

        }

        private void ResolveDetectURLsCheckedMenuItem()
        {
            detectURLsToolStripMenuItem.Checked = DetectURLs;
            tbContent.DetectUrls = DetectURLs;
            tbMemoField.DetectUrls = DetectURLs;
        }

        private void detectURLsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectURLs = !DetectURLs;
            ResolveDetectURLsCheckedMenuItem();
            SettingsHasChanges = true;
        }


        #endregion


        #region Generate Memorizable Passwords (addition 12-11-2016)
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
                    tbContent.Paste();

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


        private void generatePasswordsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GeneratePasswordsInMemoField();
        }

        private void GeneratePasswordsInMemoField()
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
                    if (SelectedPasswordStyle != pwts.CurrentSelection)
                    {
                        SelectedPasswordStyle = pwts.CurrentSelection;
                        SettingsHasChanges = true;
                    }

                    // Save clipboard content to temp variable
                    tempClipboard = "";
                    if (Clipboard.ContainsText())
                    {
                        tempClipboard = Clipboard.GetText(TextDataFormat.Text);
                    }

                    SetClipboardText(pwts.GetGeneratedKeys());

                    // Paste password text to textbox
                    tbMemoField.Paste();

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

        #endregion

        #region COPY POOL TO CLIPBOARD
        private string tempPoolPath;
        private void CopyPoolToClipboard()
        {
            try
            {
                // Get file path
                tempPoolPath = IO.GetPoolPath();
                if (tempPoolPath.Length > 0)
                {
                    // Collect file paths to copy
                    System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
                    paths.Add(tempPoolPath);
                    // Set clipboard
                    Clipboard.SetFileDropList(paths);
                    // Set status
                    Log.LogPoolStatus("Copy to Clipboard");
                    SetStatusText("Current Pool was copied to clipboard.");
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Clipboard set", MessageText.CopyCaption(), false);
                    msg.ShowDialog();
                }
                else
                {
                    SetStatusText("Unable to copy current Pool to clipboard...");
                }
            }
            catch
            {
                SetStatusText("Error copying current Pool to clipboard...");
            }
        }

        // Copy Pool to clipboard
        private void copyPoolToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyPoolToClipboard();
        }
        #endregion

        #region Refocus content
        private void FocusContent()
        {
            if (!timerRefocusContent.Enabled)
            {
                timerRefocusContent.Enabled = true;
            }
        }

        private void timerRefocusContent_Tick(object sender, EventArgs e)
        {
            timerRefocusContent.Enabled = false;
            tbContent.Focus();
        }



        #endregion

        #region REMOTE BACKUP (addtion 21-01-2017)
        private void setAlternateBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlternateBackupFolder(false);
        }

        // Set/Skip
        private string tempAlternateBackupText = MessageText.RemoteBackupMessage();
        private void SetAlternateBackupFolder(bool showDialog)
        {
            // New the dialog to make it accesible for the if statement further down
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("", "", false, false);
            msg.Accepted = false;
            if (!string.IsNullOrEmpty(AlternateBackupPath))
            {
                msg = new AhoyAnswerMessageBox("Override current backup path: " + "\"" + AlternateBackupPath + "\"?", MessageText.AlternateBackupCaption(), false, false);
                msg.ShowDialog();
                if (!msg.Accepted)
                {
                    return;
                }
            }
            else if (showDialog)
            {
                msg = new AhoyAnswerMessageBox(tempAlternateBackupText, MessageText.AlternateBackupCaption(), false, false);
                msg.SetCustomButtonText("", "Set", "Skip", MessageText.AlternateBackupCaption(), "", "", "", 3);
                msg.ShowDialog();
                if (!msg.Accepted)
                {
                    return;
                }
            }

            if (msg.Accepted || !showDialog)
            {
                FolderSelectDialog selectFolderDialog = new FolderSelectDialog();
                selectFolderDialog.Title = "Select Backup Directory";

                if (selectFolderDialog.ShowDialog() && !string.IsNullOrEmpty(selectFolderDialog.FileName))
                {
                    if (!IsPathSameAsDefaultPoolRepository(selectFolderDialog.FileName))
                    {
                        AlternateBackupPath = selectFolderDialog.FileName;
                        // Save settings
                        SettingsHasChanges = true;
                        AlternateBackupPathStringIsSet();
                        Log.LogSetAlternateBackup(AlternateBackupPath);
                        UpdateDummyTextAndSetHomeScreen();
                    }
                    else
                    {
                        AhoySimpleMessageBox wrongPathMsg = new AhoySimpleMessageBox("Unable to use the selected path (default Pool repository): Please select a different path", MessageText.AlternateBackupCaption(), false);
                        wrongPathMsg.ShowDialog();
                    }
                }
            }
            else
            {
                // Do nothing
            }

        }

        private bool IsPathSameAsDefaultPoolRepository(string path)
        {
            if (path == IO.GetDataPath())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void AlternateBackupPathStringIsSet()
        {
            if (!string.IsNullOrEmpty(AlternateBackupPath))
            {
                AlternateBackupPathSet = true;
                setAlternateBackupToolStripMenuItem.Checked = true;
            }
            else
            {
                setAlternateBackupToolStripMenuItem.Checked = false;
                AlternateBackupPathSet = false;
            }
        }


        private bool AlternateBackupPathValid(bool showInvalidPathDialog)
        {
            if (AlternateBackupPathSet)
            {
                if (!string.IsNullOrEmpty(AlternateBackupPath) && Directory.Exists(AlternateBackupPath))
                {
                    return true;
                }
                else
                {
                    // Clear remote backup path on error to disable continuous dialogs
                    AlternateBackupPath = "";
                    AlternateBackupPathStringIsSet();
                    Log.LogErrorAlternateBackup();
                    if (showInvalidPathDialog)
                    {
                        AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("*** ATTENTION ***\nRemote backup path is inaccessible. Backup to remote location was disabled.\nSet a new backup location?", MessageText.AlternateBackupCaption(), false, true);
                        msg.SetCustomButtonText("", "Yes", "No", MessageText.AlternateBackupCaption(), "", "", "", 2);
                        msg.ShowDialog();

                        if (msg.Accepted)
                        {
                            Log.LogGeneralAlternateBackup("Selecting new location...");
                            SetAlternateBackupFolder(false);
                        }
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void clearAlternateBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAlternateBackupPath(true);
        }

        private void ClearAlternateBackupPath(bool showDialog)
        {
            if (!string.IsNullOrEmpty(AlternateBackupPath))
            {
                AhoyAnswerMessageBox msg;
                msg = new AhoyAnswerMessageBox("Disable remote backup?", MessageText.AlternateBackupCaption(), false, false);
                if (showDialog)
                {
                    msg.ShowDialog();
                }

                if (msg.Accepted || !showDialog)
                {
                    AlternateBackupPath = "";
                    SettingsHasChanges = true;
                    AlternateBackupPathStringIsSet();
                    Log.LogClearAlternateBackup();
                    UpdateDummyTextAndSetHomeScreen();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No remote backup path set", MessageText.AlternateBackupCaption(), false);
                msg.ShowDialog();
            }
        }

        // Clear remote backup path without saving
        private void ForceClearAlternateBackupPath()
        {
            AlternateBackupPath = "";
            AlternateBackupPathStringIsSet();
            UpdateDummyTextAndSetHomeScreen();
        }

        private void openAlternateRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAlternateFolder();
        }

        private void OpenAlternateFolder()
        {
            try
            {
                if (!string.IsNullOrEmpty(AlternateBackupPath) && Directory.Exists(AlternateBackupPath))
                {
                    Log.ProcessStart(AlternateBackupPath);
                    Process.Start(AlternateBackupPath);
                }
                else
                {
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.InvalidFilePathMessage(), MessageText.LocatePoolCaption(), false);
                    messageBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.FilePathErrorMessage(ex.Message), MessageText.LocatePoolCaption(), false);
                messageBox.ShowDialog();
            }
        }
        #endregion

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

        #region  CYPHER ENCRYPT/DECRYPT (addition 12-02-2017)
        private void encryptDecryptSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CypherEncryptDecrypt();
        }

        // Error message
        private void DisplayCypherErrorMessage()
        {
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Error processing input", MessageText.CypherCaption(), false);
            msg.ShowDialog();
        }

        private void CypherEncryptDecrypt()
        {
            if (tbContent.SelectedText.Length == 0)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please make a text selection to continue", MessageText.CypherCaption(), false);
                msg.ShowDialog();
                return;
            }

            // Display cypher prompt
            InputPrompt prompt = new InputPrompt(MessageText.CypherCaption(), CypherKey,
                "Message Encryption\n" + PoolFile.AlgorithmDescription + " (messages from older versions can still be decrypted)",
                "Enter Encryption Key", MaxPasswordLength, false, "OK", false, null, null, true);
            prompt.ShowDialog();
            if (prompt.ActionCancelled)
            {
                return;
            }

            KeepAwake();
            CypherKey = prompt.UserInput.Trim();
            SetPoolHasChanges();

            // Exit if empty key
            if (CypherKey.Length == 0)
            {
                return;
            }

            string input = tbContent.SelectedText.Trim();
            string output;
            Cursor previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (MessageCrypto.LooksEncrypted(input))
                {
                    // Decrypt
                    if (!MessageCrypto.TryDecrypt(input, CypherKey, out output))
                    {
                        Cursor.Current = previousCursor;
                        AhoySimpleMessageBox failed = new AhoySimpleMessageBox("Unable to decrypt the selection.\nWrong key, or the text was changed.", MessageText.CypherCaption(), false);
                        failed.ShowDialog();
                        return;
                    }
                }
                else
                {
                    // Encrypt
                    output = MessageCrypto.Encrypt(input, CypherKey);
                    SetClipboardText(output);
                    Cursor.Current = previousCursor;
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Clipboard set", MessageText.CypherCaption(), false);
                    msg.ShowDialog();
                }
            }
            catch
            {
                Cursor.Current = previousCursor;
                DisplayCypherErrorMessage();
                return;
            }
            finally
            {
                Cursor.Current = previousCursor;
            }

            // Set result
            tbContent.SelectedText = Environment.NewLine + "──── Input ────" + Environment.NewLine + input + Environment.NewLine + Environment.NewLine +
                                     "──── Output ────" + Environment.NewLine + output + Environment.NewLine;
        }
        #endregion

        #region Open selection in new window
        private void viewSelectionInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectionInNewWindow();
        }

        private void openSnippetEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectionInNewWindow();
        }

        private void OpenSelectionInNewWindow()
        {
            try
            {
                if (tbContent.SelectedText.Length > 0)
                {
                    OpenSnippetWindow(tbContent.SelectedText);
                }
                else
                {
                    OpenSnippetWindow("");
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void OpenSnippetWindow(string content)
        {
            try
            {
                AhoySearchMessageBox messageBox = new AhoySearchMessageBox(content, MessageText.SnippetCaption() + TextTransform.GetBlankSpace(2) + MessageText.NotPersistedOnCloseExitCaption(), false, false, this.Opacity);
                messageBox.TopMost = true;
                messageBox.StartPosition = FormStartPosition.CenterScreen;
                messageBox.Show();
            }
            catch
            {
                // Do nothing
            }
        }
        #endregion

        #region WPM Counter
        private void lbStatus2_Click(object sender, EventArgs e)
        {
            if (ShowWPMCounter)
            {
                ResetWPMValues();
            }
        }

        private void ResetWPMValues()
        {
            if (ShowWPMCounter)
            {
                WPMCounter.ResetWPMcounter();
                UpdateWPMValues();
            }
        }

        private void UpdateWPMValues()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (ShowWPMCounter)
                {
                    lbStatus2.Text = WPMCounter.GetProgressBarWithWordCount(tbContent.Text);
                }
                else if (lbStatus2.Text.Length > 0)
                {
                    lbStatus2.Text = "";
                }
            }
        }

        private void wordAndWPMCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWordAndWPMCounter();
        }

        private void ResetWordAndWPMCounter()
        {
            ShowWPMCounter = false;
            ResolveWordAndWPMToolStripMenuItem();
            SettingsHasChanges = true;
            UpdateWPMValues();
        }

        private void ActivateWordAndWPMCounter()
        {
            ShowWPMCounter = true;
            ResolveWordAndWPMToolStripMenuItem();
            SettingsHasChanges = true;
            UpdateWPMValues();
        }

        private void ToggleWordAndWPMCounter()
        {
            ShowWPMCounter = !ShowWPMCounter;
            ResolveWordAndWPMToolStripMenuItem();
            SettingsHasChanges = true;
            UpdateWPMValues();
        }

        private void ResolveWordAndWPMToolStripMenuItem()
        {
            wordAndWPMCounterToolStripMenuItem.Checked = ShowWPMCounter;
        }
        #endregion

        #region Center Cursor (addition 15-04-2017)
        private void CenterCursor()
        {
            try
            {
                // Center with offset
                Cursor.Position = new Point(GetFormCenterPoint(100, 0).X, GetFormCenterPoint(0, 0).Y);
            }
            catch
            {
                Log.LogLocationChangeError();
            }
        }

        // Calculate form center
        private Point GetFormCenterPoint(int offsetX, int offsetY)
        {
            Point p = new Point();
            p.X = this.Location.X + (this.Width / 2) + offsetX;
            p.Y = this.Location.Y + (this.Height / 2) + offsetY;
            return p;
        }

        // Calculate center of control / Get center coordinates as Point
        //private static Point GetControlCenterPoint(Control parent)
        //{
        //    Point p = new Point();
        //    p.X = parent.Location.X + (parent.Width / 2);
        //    p.Y = parent.Location.Y + (parent.Height / 2);
        //    return p;
        //}

        private Point GetControlCenterPointComp(Control parent, Control child)
        {
            Point p = new Point();
            p.X = parent.Location.X + ((parent.Width / 2) - (child.Width / 2));
            p.Y = parent.Location.Y + ((parent.Height / 2) - (child.Height / 2));
            return p;
        }
        #endregion

        #region Autosave Entry Animation (addition 27-04-2017)
        // *** ENABLE/DISABLE AUTOSAVE ENTRY CHANGES ***
        private void ToggleAutoSaveEntryChanges()
        {
            AutoSaveEntryChanges = !AutoSaveEntryChanges;
            UpdateSaveButton();
            SettingsHasChanges = true;
            UpdateDummyTextAndSetHomeScreen();
        }

        // THIS HAS BEEN REMOVE BECAUSE OF ACCIDENTAL SWITCHING OFF AUTOSAVE...
        //private void btnSave_MouseDown(object sender, MouseEventArgs e)
        //{
        //    // If right clicked toggle autosave
        //    //if (e.Button == MouseButtons.Right)
        //    //{
        //    //    // DO NOTHING (HAS BEEN DISABLED IN VERSION .25)
        //    //    //ToggleAutoSaveEntryChanges();
        //    //}
        //}


        private void autoSaveEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAutoSaveEntryChanges();
        }

        private void UpdateSaveButton()
        {
            if (PoolIsReadOnly || IsPaused)
            {
                AutosaveBlinkerTimer.Enabled = false; // Disable autosave blinker
                btnSave.Text = "OFF";
                this.toolTip1.SetToolTip(this.btnSave, "Save changes disabled (Pool is write protected or paused)");
                saveEntryToolStripMenuItem.Text = "&Save Entry";
            }
            else if (AutoSaveEntryChanges)
            {
                btnSave.Text = "Save";
                this.toolTip1.SetToolTip(this.btnSave, "Autosave Enabled");
                autoSaveEntryToolStripMenuItem.Checked = true;
                saveEntryToolStripMenuItem.Text = "&Save Entry (Auto)";
                AutosaveBlinkerTimer.Enabled = true; // Enable autosave blinker
            }
            else
            {
                AutosaveBlinkerTimer.Enabled = false; // Disable autosave blinker
                btnSave.Text = "Save";
                this.toolTip1.SetToolTip(this.btnSave, "Save Entry (CTRL+S)");
                autoSaveEntryToolStripMenuItem.Checked = false;
                saveEntryToolStripMenuItem.Text = "&Save Entry";
            }
        }


        private void AutosaveBlinkerTimer_Tick(object sender, EventArgs e)
        {
            if (AutoSaveEntryChanges && !timerFastAnimation.Enabled)
            {
                if (btnSave.Text == "Save")
                {
                    btnSave.Text = "AUTO";
                }
                else
                {
                    btnSave.Text = "Save";
                }
            }
            else
            {
                AutosaveBlinkerTimer.Enabled = false;
            }
        }

        private static int autoSaveEntryAnimationCounter;
        private void AutoSaveEntryWithButtonAnimation()
        {
            if (AutoSaveEntryChanges)
            {
                AutosaveBlinkerTimer.Enabled = false;
                autoSaveEntryAnimationCounter = 0;
                btnSave.Text = "";
                autoSaveEntryAnimationIndex = 0;
                autoSaveEntryAnimationString = "";
                timerFastAnimation.Interval = 50;
                timerFastAnimation.Enabled = true;
            }
        }

        private int autoSaveEntryAnimationIndex;
        private string autoSaveEntryAnimationString;
        private string GetAutoSaveEntryAnimationANSI()
        {
            switch (autoSaveEntryAnimationIndex)
            {
                case 0:
                    autoSaveEntryAnimationString = @"--";
                    break;
                case 1:
                    autoSaveEntryAnimationString = @"\";
                    break;
                case 2:
                    autoSaveEntryAnimationString = @"|";
                    break;
                case 3:
                    autoSaveEntryAnimationString = @"/";
                    break;
                default:
                    autoSaveEntryAnimationString = "";
                    break;
            }
            autoSaveEntryAnimationIndex += 1;
            if (autoSaveEntryAnimationIndex >= 4)
            {
                autoSaveEntryAnimationIndex = 0;
            }
            return autoSaveEntryAnimationString;
        }

        private void timerFastAnimation_Tick(object sender, EventArgs e)
        {
            autoSaveEntryAnimationCounter += 1;
            btnSave.Text = GetAutoSaveEntryAnimationANSI();
            if (autoSaveEntryAnimationCounter >= 12)
            {
                timerFastAnimation.Enabled = false;
                SaveCurrentEntry(false);
                SetStatusText(MessageText.AutoSaved());
                UpdateSaveButton();
            }
        }
        #endregion

        #region Open file dialog and toggle ReadOnly (addition 01-05-2017 - 03-05-2017)
        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePoolWithReadOnlyParameters();
        }

        private void SavePoolWithReadOnlyParameters()
        {
            if (TogglePoolReadOnly(false, false, true))
            {
                OverrideToSetWriteProtection = true;
                SavePool(false, false, false, "");
            }

            // Update dummy text
            UpdateDummyTextAndSetHomeScreen();
        }

        private bool tempPoolReadOnlyPolicy;
        private bool TogglePoolReadOnly(bool setManually, bool value, bool showMessage)
        {
            tempPoolReadOnlyPolicy = false;
            if (showMessage)
            {
                AhoyAnswerMessageBox msg;
                if (!PoolIsReadOnly)
                {
                    msg = new AhoyAnswerMessageBox("Enable write protection?", MessageText.WriteProtectionCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                    if (!msg.Accepted)
                    {
                        return false;
                    }
                    else
                    {
                        tempPoolReadOnlyPolicy = true;
                    }
                }
                else
                {
                    msg = new AhoyAnswerMessageBox("Disable write protection?", MessageText.WriteProtectionCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                    if (!msg.Accepted)
                    {
                        return false;
                    }
                    else
                    {
                        if (SimplePromptForPassword(MessageText.EnterPasswordMessage(), "", false))
                        {
                            tempPoolReadOnlyPolicy = true;
                        }
                        else
                        {
                            return false; // Exit method if no password was supplied
                        }
                    }
                }
            }

            if (setManually)
            {
                PoolIsReadOnly = value;
            }
            else
            {
                PoolIsReadOnly = !PoolIsReadOnly;
            }

            // Log
            if (PoolIsReadOnly)
            {
                Log.LogPoolStatus("Read-Only");
            }
            else
            {
                Log.LogPoolStatus("Read/Write");
            }

            // Update menu items here
            readOnlyToolStripMenuItem.Checked = PoolIsReadOnly;
            UpdateSaveButton();

            // Set form title
            if (PoolIsReadOnly)
            {
                this.Text = MessageText.AhoyCaption() + TextTransform.GetBlankSpace(3) + "READ-ONLY";
                readOnlyToolStripMenuItem.Text = "Read-Only (Enabled)";
                lbStatus.Text = "";
                ResolveIndicatorLabel();
            }
            else
            {
                this.Text = MessageText.AhoyCaption();
                readOnlyToolStripMenuItem.Text = "Read-Only";
                lbStatus.Text = MessageText.DefaultStatus(GetAdditionalStatusText());
                ResolveIndicatorLabel();
            }

            // Message user
            if (PoolIsReadOnly && showMessage)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.WriteProtectedMessage(true), MessageText.WriteProtectionCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
            else if (!PoolIsReadOnly && showMessage)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.WriteProtectedMessage(false), MessageText.WriteProtectionCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }

            // Update dummy text to reflect changes
            LoadDummyText();
            return tempPoolReadOnlyPolicy;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPoolFromFileDialog();
        }

        private bool AbortIfDemo()
        {
            if (IO.UseDemoPool)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.ExitDemoToContinueMessage(), MessageText.DemoCaption(), false);
                msg.ShowDialog();
                return true;
            }
            else
            {
                return false;
            }
        }

        //private bool tempPoolReadOnlySelection;
        private static string tempOpenFilePath;
        private static bool tempLoadPoolAccepted;
        private void LoadPoolFromFileDialog()
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            tempLoadPoolAccepted = true;
            if (tempLoadPoolAccepted)
            {
                try
                {
                    tempOpenFilePath = "";
                    // Open file dialog
                    tempOpenFilePath = IO.OpenFileDialog(0);

                    if (tempOpenFilePath.Length > 0)
                    {
                        // Stop the global timer
                        StopGlobalTimers();

                        // Autosave before switching
                        if (EntriesExist() && LoggedInOnce)
                        {
                            ForceSaveEverything();
                        }

                        // Remember the open pool, to return to it if the new one is not unlocked
                        bool previousWasCustomPath = IO.UsingCustomFilePath();
                        string previousPoolPath = IO.GetCustomFilePathAndName();
                        bool previousWasSecondaryPool = IO.UseSecondaryPool;
                        bool hadOpenPool = LoggedInOnce;

                        // Reset everything before loading
                        ClearAllEntriesFromMemory(true);
                        // Reset secondary pool flag
                        IO.UseSecondaryPool = false;

                        // Close Utility Editor

                        // Exit fullscreen
                        if (IsFullScreen)
                        {
                            ToggleFullScreen();
                        }

                        Log.LogLoadPoolFromFilePath();

                        // Load pool
                        LoadPool(true, tempOpenFilePath);

                        // Not unlocked: go back to the pool that was open before
                        if (LastPoolUnlockCancelled && hadOpenPool)
                        {
                            AhoySimpleMessageBox notOpened = new AhoySimpleMessageBox("The selected Pool was not opened." + Environment.NewLine + "Returning to the previous Pool.",MessageText.LoadCaption(), false);
                            notOpened.ShowDialog();
                            ClearAllEntriesFromMemory(true);
                            IO.UseSecondaryPool = previousWasSecondaryPool;
                            if (previousWasCustomPath)
                            {
                                LoadPool(true, previousPoolPath);
                            }
                            else
                            {
                                LoadPool(false, "");
                            }
                            PoolLoadFromFileDialogSuccess = false;
                        }

                        // Pool loaded from open file dialog success
                        if (PoolLoadFromFileDialogSuccess)
                        {
                            PoolLoadFromFileDialogSuccess = false;
                            SetPoolHasChanges();
                            SavePool(false, false, false, "");
                        }
                    }
                }
                catch
                {
                    // Do nothing
                }
                finally
                {
                    // Start the global timer
                    StartGlobalTimers();
                    ShowPinned();
                }

                // Resolve status
            }
            else
            {
                if (LoggedInOnce)
                {
                    // return if canceled and logged in once (a pool is already there to fall back on)
                    return;
                }
                else
                {
                    // Else Exit the application
                    Application.Exit();
                }
            }
        }
        #endregion

        #region Save file dialog (addition 02-05-2017)
        private static string tempSaveFilePath;
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePoolFromFileDialog();
        }

        private void SavePoolFromFileDialog()
        {
            Log.LogSavePoolFromFilePath();
            tempSaveFilePath = "";
            // Save file dialog
            tempSaveFilePath = IO.SaveFileDialog(0, "", false);

            // Check result
            if (tempSaveFilePath.Length > 0)
            {
                // Save pool (with write protection override)
                OverrideToSetWriteProtection = true;
                SavePool(false, false, true, tempSaveFilePath);

                // Save from file dialog success message
                if (PoolSaveWithFileDialogSuccess)
                {
                    PoolSaveWithFileDialogSuccess = false;
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.PoolSavedMessage(), MessageText.SaveCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                // Do nothing
            }
        }

        private void SaveEntryAsTXT()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                Log.LogSaveEntryAsTXT();
                tempSaveFilePath = "";
                // Save file dialog
                tempSaveFilePath = IO.SaveFileDialog(1, CurrentEntry.Title, false);

                // Check result
                if (tempSaveFilePath.Length > 0)
                {
                    if (IO.SaveTextDocument(CurrentEntry.Content.TrimEnd(new char[1] { '\n' }), tempSaveFilePath))
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Entry saved to disk", MessageText.SaveAsTXTCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                    }
                    else
                    {
                        Log.LogSaveEntryAsTXTError();
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Error saving entry as text document...", MessageText.SaveAsTXTCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                    }
                }
                else
                {
                    // Do nothing
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.SaveAsTXTCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region Auto-Search (addition 14-05-2017)
        private void autoSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAutoSearch();
        }

        private void ToggleAutoSearch()
        {
            AutoSearch = !AutoSearch;
            UpdateAutoSearchVisuals();
            SettingsHasChanges = true;
        }

        private void UpdateAutoSearchVisuals()
        {
            autoSearchToolStripMenuItem.Checked = AutoSearch;
        }
        #endregion

        #region Demo Mode (addition 18-05-2017)
        private void ResolveIndicatorLabel()
        {
            if (IO.UseDemoPool)
            {
                if (IO.UseSecondaryPool)
                {
                    if (PoolIsReadOnly)
                    {
                        SetIndicatorLabel("[EXIT DEMO] (POOL 2) READ-ONLY", "Click to Exit", true);
                    }
                    else
                    {
                        SetIndicatorLabel("[EXIT DEMO] (POOL 2)", "Click to Exit", true);
                    }
                }
                else
                {
                    if (PoolIsReadOnly)
                    {
                        SetIndicatorLabel("[EXIT DEMO] READ-ONLY", "Click to Exit", true);
                    }
                    else
                    {
                        SetIndicatorLabel("[EXIT DEMO]", "Click to Exit", true);
                    }
                }
            }
            else
            {
                if (IO.UseSecondaryPool)
                {
                    if (PoolIsReadOnly)
                    {
                        SetIndicatorLabel("(POOL 2) READ-ONLY", "", true);
                    }
                    else
                    {
                        SetIndicatorLabel("(POOL 2)", "", true);
                    }
                }
                else
                {
                    if (PoolIsReadOnly)
                    {
                        SetIndicatorLabel("READ-ONLY", "", true);
                    }
                    else
                    {
                        SetIndicatorLabel("", "", false);
                    }
                }
            }
        }

        private void SetIndicatorLabel(string text, string toolTip, bool visible)
        {
            this.toolTip1.SetToolTip(lbReadOnlyIndicator, toolTip);
            lbReadOnlyIndicator.Text = text;
            lbReadOnlyIndicator.Visible = visible;
        }

        private void LbReadOnlyIndicator_Click(object sender, EventArgs e)
        {
            if (IO.UseDemoPool)
            {
                ToggleDemo(true);
            }
        }

        private void SaveAll()
        {
            if (!IsPaused)
            {
                // Save everything if changes are present
                SaveCurrentEntry(false);
                SaveSettings();
                SavePool(false, true, false, "");
            }
            else
            {
                InformPaused();
            }
        }

        private void ForceSaveEverything()
        {
            if (!IsPaused)
            {
                // Force save everything with backup
                SettingsHasChanges = true;
                SetPoolHasChanges();
                SavePool(false, true, false, "");
            }
            else
            {
                InformPaused();
            }
        }

        private void InformPaused()
        {
            SetStatusText("*** UNABLE TO SAVE: PAUSED ***");
        }

        private void toggleDemoModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleDemo(true);
        }

        private void ToggleDemo(bool showDialog)
        {
            if (!IO.UseDemoPool)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Enable Demo?", MessageText.DemoCaption(), false, false);
                msg.TopMost = true;
                if (showDialog)
                {
                    msg.ShowDialog();
                }

                if (!showDialog || msg.Accepted)
                {
                    // Save everything if changes are present
                    SaveAll(); // <------------- SAVE ALL
                    try
                    {
                        // Stop the global timer
                        StopGlobalTimers();
                        // Reset all settings
                        ResetAllSettings();
                        SetDefaultWindowSizeAndLocation();
                        // Set demo colortheme
                        AhoyColors.ColorTheme = 15; // Blue theme
                        SetColorTheme(false);
                        // Reset everything before loading
                        ClearAllEntriesFromMemory(true);
                        // Clear custom filepath
                        IO.ClearCustomFilePath(false);
                        // Set demo
                        IO.UseDemoPool = true;
                        // Close Utility Editor
                        // Reset WPM Counter
                        WPMCounter.ClearWPMCounter();
                        // Exit fullscreen
                        if (IsFullScreen)
                        {
                            ToggleFullScreen();
                        }
                        // Set default demo credentials (password "demo"), access granted and no backup path
                        SetCurrentKey(PoolKey.CreateNew("demo"));
                        AccessGranted = true;
                        AlternateBackupPath = "";
                        AlternateBackupPathStringIsSet();
                        // Remove readonly
                        TogglePoolReadOnly(true, false, false);
                        // Create new demo entry
                        CreateNewDemoEntry();
                        SetPoolHasChanges();
                        SavePool(false, true, false, "");
                        // Update status strip and title
                        this.Text = GetFormTitle() + TextTransform.GetBlankSpace(3) + "(DEMO)";
                        ResolveIndicatorLabel();
                        lbReadOnlyIndicator.Cursor = Cursors.Default;
                        SetStatusText("Demo Ready");
                        AhoySimpleMessageBox message = new AhoySimpleMessageBox("*** Demo enabled ***\nPassword: \"demo\"\nClick [EXIT DEMO] in the top Menu or select\nToggle demo from the Tools menu to exit mode", "Demo", false);
                        message.TopMost = true;
                        message.ShowDialog();
                    }
                    finally
                    {
                        // Start the global timer
                        StartGlobalTimers();
                        ShowPinned();
                    }
                    // Resolve status
                }
            }
            else
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Exit Demo?", MessageText.DemoCaption(), false, false);
                msg.TopMost = true;
                if (showDialog)
                {
                    msg.ShowDialog();
                }

                if (!showDialog || msg.Accepted)
                {
                    try
                    {
                        // Stop the global timer
                        StopGlobalTimers();
                        // Reset all settings
                        ResetAllSettings();
                        SetDefaultWindowSizeAndLocation();
                        // Reset everything before loading
                        ClearAllEntriesFromMemory(true);
                        ResolveIndicatorLabel();
                        lbReadOnlyIndicator.Cursor = Cursors.Default;
                        IO.UseDemoPool = false;
                        this.Text = GetFormTitle();
                        LoadSettings();
                        ReloadDefaultPool(false, false, false);
                    }
                    finally
                    {
                        // Start the global timer
                        StartGlobalTimers();
                        ShowPinned();
                    }
                    // Resolve status
                }
            }
        }

        private void CreateNewDemoEntry()
        {
            // Create new entry
            CurrentEntry = new Entry();
            // Set variables
            CurrentEntry.IsNewEntry = true;
            CurrentEntryHasChanges();
            CurrentEntry.WasDeleted = false;
            CurrentEntry.IgnoreTextChanged = false;
            CurrentEntry.IsDummyEntry = false;
            // Create default content
            CurrentEntry.ID = Guid.NewGuid().ToString();
            CurrentEntry.Created = IO.GetIsoDateTimeNow();
            CurrentEntry.Changed = IO.GetIsoDateTimeNow();
            CurrentEntry.Title = "";
            CurrentEntry.Content = "";
            CurrentEntry.Pin = false; // Will be set through the cbPinned box further down
            CurrentEntry.Bookmark = 0;
            CurrentEntry.Rank = IO.DefaultRank; // Default rank will put newly pinned entries at the bottom of the list
            CurrentEntry.RelatedEntries = "";
            CurrentEntry.RelatedStyle = 0;
            CurrentEntry.RelatedView = false;
            ResetTitleColorOnCurrentEntry();
            CurrentEntry.Protected = false;
            // Clear content afterwards to prevent from saving over the current selected entry
            CurrentEntry.IgnoreTextChanged = true;
            tbTitle.Text = "Demo Title"; // Add new title text to the title field
            CurrentEntry.Title = tbTitle.Text;
            tbContent.ResetHistory();
            // Must be set for the eventual initial Paste function to work
            tbContent.SetText("Demo Entry", false);
            CurrentEntry.Content = tbContent.Text;
            cbPinned.Checked = false;
            cbProtect.Checked = false;
            cbBookmark.Checked = false;
            CurrentEntry.IgnoreTextChanged = false;
            AddToNavigator(CurrentEntry);
            Log.LogNewEntry();
            // Clear displays
            ClearDisplays(false);
        }
        #endregion

        #region PARAGRAPH DIVIDERS (addition 20-05-2017)
        private void paragraphDividersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenParagraphDividersEditor();
        }

        private void OpenParagraphDividersEditor()
        {
            if (!PDEditorOpen)
            {
                PDEditorOpen = true;
                KeepAwake();
                Log.ParagraphDividerEdit();
                ParagraphDividers editor = new ParagraphDividers();
                editor.FormClosed += Editor_FormClosed;
                editor.TopMost = true;
                editor.Show();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.AlreadyOpenMessage(), MessageText.ParagraphDividersCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            PDEditorOpen = false;
        }

        #endregion

        #region SAVE AS .TXT (addition 13-06-2017)
        private void saveAsTextDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEntryAsTXT();
        }
        #endregion

        #region STICKY (addition 04-07-2017)
        private const int MaxSticky = 32;
        private static List<StickySettings> StickySettingsList;
        internal static int StickyStatusCode { get; set; }
        internal static bool StatusCodeInvoked { get; set; }
        internal static Timer StickyStatusTimer { get; set; }
        private static bool SpawnPerRemote { get; set; }
        private static int SpawnPerRemoteLeft { get; set; }
        private static int SpawnPerRemoteTop { get; set; }
        public const int StickyDefaultWidth = 300;
        public const int StickyDefaultHeight = 300;
        private static bool IgnoreStickyChanges { get; set; }
        private void InitializeStickies()
        {
            IgnoreStickyChanges = false;
            StickyStatusCode = 0;
            tempStickyOffset = 0;
            SpawnPerRemote = false;
            SpawnPerRemoteLeft = 0;
            SpawnPerRemoteTop = 0;
            tempStatusCycleCounter = 0;
            tempShowBringToFrontButtonCounter = 0;
            StatusCodeInvoked = false;
            StickyStatusTimer = new Timer();
            StickyStatusTimer.Tick += StickyStatusTimer_Tick;
            StickyStatusTimer.Interval = 500;
            StickyStatusTimer.Enabled = true;
            if (StickySettingsList == null)
            {
                StickySettingsList = new List<StickySettings>();
            }
        }

        private int tempStatucCompleteCount;
        private bool tempStatusCaptured;
        private static int tempStatusCycleCounter;
        private int tempShowBringToFrontButtonCounter;
        private void StickyStatusTimer_Tick(object sender, EventArgs e)
        {
            // Spawn sticky per remote
            if (SpawnPerRemote)
            {
                SpawnPerRemote = false;
                SpawnSticky(true, true, SpawnPerRemoteLeft, SpawnPerRemoteTop, true);
            }

            // Check if all Stickies have checked in before resetting
            if (StickyExists())
            {
                tempStatucCompleteCount = 0;
                tempStatusCaptured = false;
                if (StatusCodeInvoked)
                {
                    tempStatusCycleCounter += 1;
                    foreach (StickySettings s in StickySettingsList)
                    {
                        if (s.StatusCode > 0 && !s.StatusComplete)
                        {
                            s.StatusComplete = true;
                            s.StatusCode = 0;
                            tempStatucCompleteCount += 1;
                            tempStatusCaptured = true;
                        }
                    }

                    // Completed
                    if (tempStatusCaptured && tempStatucCompleteCount == StickySettingsList.Count)
                    {
                        // Reset status code when all stickies are updated
                        Log.StickyStatus("Cycle #" + StickyStatusCode.ToString("00") + " completed");
                        StickyStatusCode = 0;
                        StatusCodeInvoked = false;
                    }

                    // Partially complete
                    if (tempStatusCaptured && tempStatucCompleteCount != StickySettingsList.Count)
                    {
                        // Reset status code when all stickies are updated
                        Log.StickyStatus("Cycle #" + StickyStatusCode.ToString("00") + " partially complete");
                    }

                    // Pending
                    // Check if a loop ran without affecting any Stickies
                    if (!tempStatusCaptured)
                    {
                        Log.StickyStatus("Cycle #" + StickyStatusCode.ToString("00") + " pending");
                        CancelCycle();
                    }
                }

                // Show/Hide the eyebutton on the main form (show if hidden stickies are found)
                tempShowBringToFrontButtonCounter += 1;
                if (tempShowBringToFrontButtonCounter >= 5)
                {
                    tempShowBringToFrontButtonCounter = 0;
                    if (OpenStickyExists() && TopMostFalseStickiesExist())
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
                }
            }
            else
            {
                if (StickyStatusCode != 2)
                {
                    StickyStatusCode = 2;  // Tell all stickies to close if there is nothing registered
                }

                if (pnlShowAllStickies.Visible)
                {
                    pnlShowAllStickies.Visible = false;
                }
            }
        }

        private void CancelCycle()
        {
            if (tempStatusCycleCounter >= 10) // Exit after waiting for 10 cycles
            {
                Log.StickyStatus("Cycle #" + StickyStatusCode.ToString("00") + " ended");
                tempStatusCycleCounter = 0;
                StickyStatusCode = 0;
                StatusCodeInvoked = false;
            }
        }


        // Called by both the main form and
        // by the stickies
        // 1 = close sticky
        // 2 = center align sticky
        internal static void SetStickyStatus(int statusCode)
        {
            if (!StatusCodeInvoked)
            {
                tempStatusCycleCounter = 0;
                StatusCodeInvoked = true;
                tempStickyOffset = 250;
                StickyStatusCode = statusCode;
                StickyStatusTimer.Enabled = true;
            }
        }

        public static bool StatusCodeCycleEnded()
        {
            if (StatusCodeInvoked)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private static void CloseAllStickiesAndClearMemory()
        {
            Log.StickyInitiatingTerminateAll();
            StickySettingsList.Clear();
            IgnoreStickySaveChanges();
            EnableMasterCloseStickies();
        }

        internal static void ColorThemeChanged()
        {
            SetStickyStatus(6);
        }

        internal static void BringStickiesToFront()
        {
            SetStickyStatus(5);
        }

        internal void BringStickiesToFrontLocal()
        {
            if (AllStickiesClosed())
            {
                ShowStickies();
                SetStickyStatus(5);
            }
            else
            {
                SetStickyStatus(5);
            }
        }

        private void ShowStickies()
        {
            SpawnSticky(false, false, 0, 0, false);
        }

        // Non-static method
        public void HideStickies()
        {
            SetStickyStatus(4);
            tbContent.Focus();
        }

        // Static method
        public static void HideStickiesRemote()
        {
            SetStickyStatus(4);
        }

        private static void CenterStickies()
        {
            SetStickyStatus(3);
        }

        private static int tempStickyOffset;
        internal static int GetStickyOffset()
        {
            tempStickyOffset += 5;
            return tempStickyOffset;
        }

        public static int GetStickyCount()
        {
            if (StickySettingsList != null && StickySettingsList.Count > 0)
            {
                return StickySettingsList.Count;
            }
            else
            {
                return 0;
            }
        }

        // Clear all closed stickies
        private static bool tempRemoveStickiesSuccess;
        private static void RemoveStickies()
        {
            try
            {
                tempRemoveStickiesSuccess = false;
                if (RemovedStickyExists())
                {
                    while (RemovedStickyExists())
                    {
                        for (int i = 0; i < StickySettingsList.Count; i++)
                        {
                            if (StickySettingsList[i].Remove)
                            {
                                Log.StickyDeleted();
                                StickySettingsList.RemoveAt(i);
                                tempRemoveStickiesSuccess = true;
                                break;
                            }
                        }
                    }

                    // Save changes
                    if (tempRemoveStickiesSuccess)
                    {
                        SetPoolHasChanges();
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }


        private void pnlShowAllStickies_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HideStickies();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (StickyExists())
                {
                    BringStickiesToFront();
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                    msg.ShowDialog();
                }
            }
        }

        private void pnlSpawnSticky_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HideStickies();
            }
            else if (e.Button == MouseButtons.Left)
            {
                SpawnSticky(true, false, 0, 0, false);
            }
        }

        private void stickyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnSticky(true, false, 0, 0, false);
        }

        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StickyExists())
            {
                HideStickies();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StickyExists())
            {
                if (AllStickiesOpen())
                {
                    BringStickiesToFront();
                }
                else
                {
                    ShowStickies();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void centerAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StickyExists())
            {
                CenterStickies();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void DeleteAllStickies()
        {
            if (StickyExists())
            {
                if (SimplePromptForPassword(MessageText.StickyCaption(), "Delete all Stickies?", false))
                {
                    CloseAllStickiesAndClearMemory();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StickyExists())
            {
                DeleteAllStickies();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void pnlShowAllStickies_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StickyExists())
            {
                if (MainForm.AllStickiesClosed())
                {
                    ShowStickies();
                }
                else
                {
                    BringStickiesToFrontLocal();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }

        private void pnlSpawnSticky_Click(object sender, EventArgs e)
        {
            // Do nothing                
        }

        public static void SpawnStickyPerRemote(int left, int top)
        {
            SpawnPerRemote = true;
            SpawnPerRemoteLeft = left;
            SpawnPerRemoteTop = top;
        }

        private void SpawnSticky(bool createNew, bool customLocation, int left, int top, bool perRemote)
        {
            // Reset status code
            DisableMasterCloseStickies();
            StickyStatusCode = 0;
            // Start by opening closed stickies
            if (ClosedStickyExists())
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (!s.Open)
                    {
                        s.Open = s.Open = true;
                        s.Remove = s.Remove = false;
                        Log.StickyReopen();
                        ConfigureSticky(s, false);
                    }
                }
            }
            else if (createNew)
            {
                // If number of available stickies are below max create a stikcy
                if (StickySettingsList.Count < MaxSticky)
                {
                    StickySettings stS = new StickySettings();
                    stS.ID = Guid.NewGuid().ToString();
                    stS.Width = StickyDefaultWidth;
                    stS.Height = StickyDefaultHeight;
                    if (customLocation)
                    {
                        stS.Left = left;
                        stS.Top = top;
                    }
                    else
                    {
                        stS.Left = this.Location.X + (this.Width / 4);
                        stS.Top = this.Location.Y + (this.Height / 4);
                    }
                    stS.Text = "";
                    stS.Theme = 3; // Yellow theme by default (new stickky)
                    stS.TopMost = true;
                    stS.Open = true;
                    stS.Remove = false;
                    Log.StickyCreated(perRemote);
                    ConfigureSticky(stS, true);
                    StickySettingsList.Add(stS);
                    SetPoolHasChanges();
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Maximum number of stickies: " + MaxSticky, MessageText.StickyCaption(), false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                }
            }
        }

        private void ConfigureSticky(StickySettings stS, bool focusOnSpawn)
        {
            Sticky note = new Sticky(stS.ID, stS.Text, stS.Width, stS.Height, stS.Left, stS.Top, focusOnSpawn, stS.Theme, stS.TopMost);
            note.Show();
        }

        private static bool RemovedStickyExists()
        {
            // Set values here for save...
            if (StickyExists())
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (s.Remove)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool ClosedStickyExists()
        {
            if (StickyExists())
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (!s.Open)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool OpenStickyExists()
        {
            if (StickyExists())
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (s.Open)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool StickyExists()
        {
            if (StickySettingsList != null && StickySettingsList.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static int tempAllStickyClosedCounter;
        public static bool AllStickiesClosed()
        {
            if (StickyExists())
            {
                tempAllStickyClosedCounter = 0;
                foreach (StickySettings s in StickySettingsList)
                {
                    if (!s.Open)
                    {
                        tempAllStickyClosedCounter += 1;
                    }
                }

                if (tempAllStickyClosedCounter == StickySettingsList.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private static int tempAllStickyOpenCounter;
        public static bool AllStickiesOpen()
        {
            if (StickyExists())
            {
                tempAllStickyOpenCounter = 0;
                foreach (StickySettings s in StickySettingsList)
                {
                    if (s.Open)
                    {
                        tempAllStickyOpenCounter += 1;
                    }
                }

                if (tempAllStickyOpenCounter == StickySettingsList.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        internal static bool TopMostFalseStickiesExist()
        {
            // Set values here for save...
            if (StickyExists())
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (!s.TopMost)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool MoreThanOneTopMostFalseStickiesExist()
        {
            // Set values here for save...
            if (StickyExists() && StickySettingsList.Count > 1)
            {
                foreach (StickySettings s in StickySettingsList)
                {
                    if (!s.TopMost)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private string tempRetrieveStickyContent;
        private int tempRetrieveStickyContentCounter;
        private void RetrieveStickyContent()
        {
            if (StickyExists())
            {
                tempRetrieveStickyContent = "";
                tempRetrieveStickyContentCounter = 0;
                foreach (StickySettings s in StickySettingsList)
                {
                    tempRetrieveStickyContentCounter += 1;
                    tempRetrieveStickyContent += "Sticky #" + tempRetrieveStickyContentCounter.ToString("00") + ":" + Environment.NewLine +
                       TextTransform.GetRepeatedCharacter(11, "-") + Environment.NewLine + s.Text + TextTransform.GetNewLines(4);
                }

                // Add retrieved content to new entry
                CreateNewEntry(tempRetrieveStickyContent, true, "Sticky " + DateTime.Now.ToString("yyyy-MM-dd"), true, false, false);
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.NoStickiesMessage(), MessageText.StickyCaption(), false);
                msg.ShowDialog();
            }
        }


        private void retrieveAllContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RetrieveStickyContent();
        }

        // Called by the sticky notes to find themselves in the list
        // to update values...
        private static bool tempSubmitStickyContentSuccess;
        internal static void SubmitStickyContent(string stickyID, string stickyText, int width, int height, int left, int top, int colorTheme, bool topMost, bool open, bool remove, int statusCode, bool statusComplete)
        {
            // Ignore everything below
            if (IgnoreStickyChanges)
            {
                return;
            }

            try
            {
                tempSubmitStickyContentSuccess = false;
                // Set values here for save...
                if (StickySettingsList != null)
                {
                    foreach (StickySettings s in StickySettingsList)
                    {
                        if (stickyID == s.ID)
                        {   // If match, submit values
                            s.Text = stickyText;
                            s.Width = width;
                            s.Height = height;
                            s.Left = left;
                            s.Theme = colorTheme;
                            s.Top = top;
                            s.TopMost = topMost;
                            s.Open = open;
                            s.Remove = remove;
                            s.StatusCode = statusCode;
                            s.StatusComplete = statusComplete;
                            tempSubmitStickyContentSuccess = true;
                        }
                    }
                }

                if (tempSubmitStickyContentSuccess)
                {
                    KeepAwake();
                    // Save changes (size and location is also saved to pool)
                    SetPoolHasChanges();
                }

                // Cleanup stickies marked as "Remove"
                RemoveStickies();
            }
            catch
            {
                // Do nothing
            }
        }

        // TEST denne her fejler, med at kører i ring
        // VIGTIG THEK STICKY FOR "_POPUP", DA DET OGSÅ BENYTTES DER
        private void ResolveBtnShowToolTip()
        {
            if (pnlShowAllStickies.Visible)
            {
                this.toolTip1.SetToolTip(this.pnlShowAllStickies, GetStickyCountForToolTip());
            }
        }

        private string tempStickyCountText;
        private string GetStickyCountForToolTip()
        {
            if (StickyExists() && TopMostFalseStickiesExist())
            {
                tempStickyCountText = "";
                if (GetStickyCount() == 1)
                {
                    tempStickyCountText = GetStickyCount() + " Sticky";
                }
                else if (GetStickyCount() > 1)
                {
                    tempStickyCountText = GetStickyCount() + " Stickies";
                }
                else
                {
                    tempStickyCountText = "Stickies";
                }

                return "Bring " + tempStickyCountText + " To Front - Right Click To Hide All (CTRL+SHIFT+H)";
            }
            else
            {
                return "Bring Stickies To Front - Right Click To Hide All (CTRL+SHIFT+H)";
            }
        }
        #endregion

        #region QUICK BROWN FOX (addition 23-01-2019)
        private Timer tempQuickBrownFoxTimer;
        private const string tempQuickBrownFoxOutput = "The quick brown fox jumps over the lazy dog!";
        private int foxCounter;
        private bool foxNewLines;
        private int foxNewLineCounter;
        private static Random foxRandom;
        private void QuickBrownFox()
        {
            if (CurrentEntryIsDummy())
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.SelectEntryCaption(), false);
                msg.BringToFront();
                msg.ShowDialog();
                return;
            }

            InputPrompt serviceMsg = new InputPrompt(MessageText.ServiceCaption(), "", "", "Enter service code", 128, false, "OK", false, null, null, true);
            serviceMsg.ShowDialog();
            if (serviceMsg.UserInput.Trim() == Srv && !CurrentEntry.IsDummyEntry)
            {
                foxRandom = new Random();
                tempQuickBrownFoxTimer = new Timer();
                tempQuickBrownFoxTimer.Interval = 60;
                tempQuickBrownFoxTimer.Enabled = false;
                tempQuickBrownFoxTimer.Tick += TempQuickBrownFoxTimer_Tick;
                foxCounter = 0;
                foxNewLineCounter = 0;
                foxNewLines = true;
                tempQuickBrownFoxTimer.Enabled = true;
            }
        }

        private void TempQuickBrownFoxTimer_Tick(object sender, EventArgs e)
        {
            // Focus and scroll to caret
            if (foxCounter == 0)
            {
                tbContent.Focus();
                tbContent.Select(CurrentEntry.CaretPosition, 0);
                tbContent.ScrollToCaret();
            }

            // Make a few newlines
            if (foxNewLines)
            {
                foxNewLineCounter += 1;
                tbContent.Text += Environment.NewLine;
                tbContent.SelectionStart = tbContent.TextLength;
                tbContent.SelectionLength = 0;
                if (foxNewLineCounter == 10)
                {
                    foxNewLines = false;
                }
            }

            if (foxNewLines)
            {
                return;
            }

            // Type text
            tempQuickBrownFoxTimer.Interval = foxRandom.Next(20, 120);
            if (foxCounter < tempQuickBrownFoxOutput.Length)
            {
                tbContent.Text += tempQuickBrownFoxOutput[foxCounter];
                foxCounter += 1;
                tbContent.SelectionStart = tbContent.TextLength;
                tbContent.SelectionLength = 0;
            }
            else
            {
                tempQuickBrownFoxTimer.Enabled = false;
                tbContent.SelectionStart = tbContent.TextLength;
                tbContent.SelectionLength = 0;
            }
        }
        #endregion

        #region LOG MONITOR (addition 12-07-2017)
        private static Timer logMonTimer;
        private AhoySearchMessageBox logMonitorWindow;
        private const int tempLogMonitorLines = 256; // Number of log entries to get
        private void ToggleLogMonitor()
        {
            if (!LogMonitorEnabled)
            {
                Log.LogShowLog();
                LogMonitorEnabled = true;
                // Open separate window "Log Monitor" and update it from a special (separate) timer
                // to keep logging even when the global timer is off
                // Disable monitoring when the window closes or when the "logmon" command is executed
                if (logMonTimer == null)
                {
                    logMonTimer = new Timer();
                    logMonTimer.Interval = 1500;
                    logMonTimer.Tick += LogMonTimer_Tick;
                }

                logMonitorWindow = new AhoySearchMessageBox("", MessageText.LogMonitorCaption(), false, true, 0.85d);
                logMonitorWindow.FormClosing += LogMonitorWindow_FormClosing;
                // Customize for log monitor view
                ResolveLogMonitorThemeAndFont(); // Unlock for resizing
                logMonitorWindow.AddWidth(-150);
                logMonitorWindow.AddHeight(150);
                // Set text
                logMonitorWindow.SetTextContent(Log.GetLogLines(tempLogMonitorLines));
                logMonitorWindow.Show(); // Not as dialog
                logMonitorWindow.BringToFront();

                // Enable timer
                if (!logMonTimer.Enabled)
                {
                    logMonTimer.Enabled = true;
                }
            }
            else
            {
                LogMonitorEnabled = false;
                if (logMonTimer.Enabled)
                {
                    logMonTimer.Enabled = false;
                }

                if (logMonitorWindow != null && logMonitorWindow.Created)
                {
                    try
                    {
                        logMonitorWindow.Close();
                    }
                    catch
                    {
                        // Do nothing
                    }
                }
            }
        }

        private void LogMonitorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logMonTimer.Enabled)
            {
                LogMonitorEnabled = false;
                logMonTimer.Enabled = false;
            }
        }

        private void ResolveLogMonitorThemeAndFont()
        {
            try
            {
                if (LogMonitorEnabled && logMonitorWindow != null)
                {
                    logMonitorWindow.EnableAutoScrollToEnd();
                    logMonitorWindow.HideSearchControls();
                    logMonitorWindow.ShowDateTime();
                    logMonitorWindow.HighlightDateTimeLabel();
                    logMonitorWindow.ForceMinSizeAndFont();
                    logMonitorWindow.RemoveScrollBars();
                    logMonitorWindow.SetColorTheme();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private string tempLogMonTimerTextA;
        private string tempLogMonTimerTextB;
        private void LogMonTimer_Tick(object sender, EventArgs e)
        {
            if (LogMonitorEnabled && logMonitorWindow != null)
            {
                try
                {
                    tempLogMonTimerTextA = Log.GetLogLines(tempLogMonitorLines);
                    if (tempLogMonTimerTextA != tempLogMonTimerTextB)
                    {
                        // Read log here
                        tempLogMonTimerTextB = tempLogMonTimerTextA;
                        logMonitorWindow.SuspendLayout();
                        logMonitorWindow.SetTextContent(tempLogMonTimerTextB);
                        //logMonitorWindow.ColorizeTimestamps();
                        logMonitorWindow.ScrollToEndWithoutFocus();
                        logMonitorWindow.ResumeLayout();
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
            else
            {
                logMonTimer.Enabled = false;
                LogMonitorEnabled = false;
            }
        }
        #endregion

        #region POOL DUMP (addition 13-07-2017)
        private string tempDump;
        private void PoolDump()
        {
            if (SimplePromptForPassword(MessageText.ServiceCaption(), MessageText.ServiceAccessTitle(), false))
            {
                InputPrompt serviceMsg = new InputPrompt(MessageText.ServiceCaption(), "", "", "Enter service code", 128, false, "OK", false, null, null, true);
                serviceMsg.ShowDialog();
                if (serviceMsg.UserInput.Trim() == Srv)
                {
                    Log.LogPoolStatus("Service dump");
                    Dump = true;
                    OverrideToSetWriteProtection = true;
                    // Force everything to generate content
                    SaveCurrentEntry(false);
                    SavePool(false, true, false, "");
                    tempDump = IO.PoolDumpString;
                    if (tempDump != null)
                    {
                        // Remove the first 8 lines to hide the credentials section
                        tempDump = tempDump.Replace("\r\n", "\n");
                        List<string> tempDumpList = new List<string>();
                        tempDumpList.AddRange(tempDump.Split('\n'));
                        tempDumpList.RemoveRange(0, 8);
                        tempDump = "Service Dump:" + Environment.NewLine;
                        tempDump += TextTransform.GetRepeatedCharacter(tempDump.Length, "-") + Environment.NewLine + Environment.NewLine;
                        foreach (string s in tempDumpList)
                        {
                            tempDump += s + Environment.NewLine;
                        }

                        if (tempDump.Length < HistoryTextBox.GetMaxLength())
                        {
                            AhoySearchMessageBox msg = new AhoySearchMessageBox(tempDump, MessageText.ServiceCaption() + " (" + tempDump.Length.ToString() + " chars)", false, false, this.Opacity);
                            msg.Show();
                        }
                        else
                        {
                            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Value too large...", MessageText.ErrorCaption(), false);
                            msg.ShowDialog();
                        }
                    }
                    else
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No data", MessageText.ServiceCaption(), false);
                        msg.ShowDialog();
                    }
                    OverrideToSetWriteProtection = false;
                    Dump = false;
                }
            }
        }

        #endregion

        #region STATUS TEXT ANIMATION (addition 27-07-2017)

        private string GetAdditionalStatusText()
        {
            if (tbContent.ReduceCache)
            {
                return "(Reduced Cache)";
            }
            else
            {
                return "";
            }
        }

        private void SetDefaultStatusText()
        {
            SetStatusText(MessageText.DefaultStatus(GetAdditionalStatusText()));
        }

        // Make shure the status list is not longer than 12 awaiting messages
        private void TruncateAnimationStausList()
        {
            try
            {
                if (tempStatusInputList != null && tempStatusInputList.Count > 12)
                {
                    while (tempStatusInputList.Count > 12)
                    {
                        tempStatusInputList.RemoveRange(0, 1);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private Timer timerStatusAnimation { get; set; }
        private int tempStatusAnimationIndex { get; set; }
        private int tempStatusAnimationCounter { get; set; }
        private string tempStatusAnimationString { get; set; }
        private List<string> tempStatusInputList { get; set; }
        private void SetStatusText(string input)
        {
            // Initialize timer
            if (timerStatusAnimation == null)
            {
                timerStatusAnimation = new Timer();
                timerStatusAnimation.Tick += TimerStatusAnimation_Tick;
                timerStatusAnimation.Interval = 25;
                timerStatusAnimation.Enabled = true;
                tempStatusInputList = new List<string>();
            }

            // Add string to display
            if (input != null && input.Length > 0)
            {
                tempStatusInputList.Add(TextTransform.GetBlankSpace(input.Length + 1));
                tempStatusInputList.Add(input);
            }

            // Truncate list
            TruncateAnimationStausList();

            // Start timer
            StartStatusAnimation();
        }

        private void StartStatusAnimation()
        {
            lbStatus.ForeColor = AhoyColors.CurrentColorTheme()[6];
            // Start timer
            if (!timerStatusAnimation.Enabled)
            {
                lbStatus.ForeColor = AhoyColors.CurrentColorTheme()[6];
                lbStatus.Text = "";
                tempStatusAnimationString = "";
                tempStatusAnimationIndex = 0;
                tempStatusAnimationCounter = 0;
                timerStatusAnimation.Enabled = true;
            }
        }

        private void StopStatusAnimation()
        {
            lbStatus.ForeColor = AhoyColors.CurrentColorTheme()[8];
            tempStatusAnimationIndex = 0;
            tempStatusAnimationCounter = 0;
            timerStatusAnimation.Enabled = false;
            tempStatusInputList.Clear();
        }

        private int tempPauseAnimationTimerInterval = 0;
        private bool PauseStatusAnimationTimer = false;
        private void PauseStatusAnimation()
        {
            PauseStatusAnimationTimer = true;
            tempPauseAnimationTimerInterval = 8; // Pause of x-number of ticks
        }

        private void TimerStatusAnimation_Tick(object sender, EventArgs e)
        {
            // Disable timer if no strings are available
            if (tempStatusInputList.Count == 0)
            {
                StopStatusAnimation();
            }
            else
            {
                // Linger if paused:
                if (PauseStatusAnimationTimer && tempPauseAnimationTimerInterval > 0)
                {
                    tempPauseAnimationTimerInterval -= 1;
                    if (tempPauseAnimationTimerInterval <= 0)
                    {
                        PauseStatusAnimationTimer = false;
                    }
                    else
                    {
                        // Is paused
                        return;
                    }
                }

                // If index is below or equal to list count proceed
                if (tempStatusAnimationIndex <= tempStatusInputList.Count - 1)
                {
                    if (tempStatusAnimationCounter <= tempStatusInputList[tempStatusAnimationIndex].Length - 1)
                    {
                        tempStatusAnimationString += tempStatusInputList[tempStatusAnimationIndex][tempStatusAnimationCounter];
                        if (tempStatusAnimationCounter == tempStatusInputList[tempStatusAnimationIndex].Length - 1)
                        {
                            lbStatus.Text = tempStatusAnimationString;
                        }
                        else
                        {
                            lbStatus.Text = tempStatusAnimationString + "_";
                        }
                    }
                }

                if (tempStatusAnimationCounter < tempStatusInputList[tempStatusAnimationIndex].Length - 1)
                {
                    tempStatusAnimationCounter += 1;
                }
                else
                {
                    if (tempStatusAnimationCounter == tempStatusInputList[tempStatusAnimationIndex].Length - 1 && tempStatusAnimationIndex == tempStatusInputList.Count - 1)
                    {
                        StopStatusAnimation();
                    }
                    else if (tempStatusAnimationIndex < tempStatusInputList.Count - 1)
                    {
                        //lbStatus.Text = ""; <-- Removed after the introduction of the PauseStatusAnimation method below
                        tempStatusAnimationString = "";
                        tempStatusAnimationIndex += 1;
                        tempStatusAnimationCounter = 0;
                        PauseStatusAnimation(); // Pause if "paging" more messages...
                    }
                }

            }
        }

        #endregion



        #region FONT SELECTION (addition 29-12-2017)
        private void modernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Design.CurrentFont = 0;
            ResolveFontSelector();
            SetStickyStatus(7);
            ResolveFonts();
            SettingsHasChanges = true;
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Design.CurrentFont = 1;
            ResolveFontSelector();
            SetStickyStatus(7);
            ResolveFonts();
            SettingsHasChanges = true;
        }

        private void ResolveFontSelector()
        {
            switch (Design.CurrentFont)
            {
                case 0:
                    modernToolStripMenuItem.Checked = true;
                    classicToolStripMenuItem.Checked = false;
                    break;
                case 1:
                    modernToolStripMenuItem.Checked = false;
                    classicToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void ResolveFonts()
        {
            SetFieldsFontSize(FieldsFontSize, false, false);
            SetListsFontSize(ListsFontSize);
        }
        #endregion


        #region DEFINE COLORS FROM PINNED CONTEXT MENU (addition 23-01-2018)
        private void defineColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayStyleEditor();
        }
        #endregion



        #region IMPORT/EXPORT (addition 11-02-2018)
        private void btnExport_Click(object sender, EventArgs e)
        {
            FindExport();
        }

        private void FindExport()
        {
            FindEntriesMarkedForExport();
        }

        private void markForExportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MarkForExport();
        }

        private void markForExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkForExport();
        }


        private void markForExportToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MarkForExport();
        }

        private void MarkForExport()
        {
            KeepAwake();
            // Mark selected entry for export
            if (CurrentEntryNotNullOrDummy())
            {
                if (!CurrentEntry.MarkExport)
                {
                    CurrentEntry.MarkExport = true;
                }
                else
                {
                    CurrentEntry.MarkExport = false;
                }
                Log.LogMarkExport(CurrentEntry.Title, CurrentEntry.MarkExport);
                // If current entry has related entries mark them as well (true or false)
                if (Related.HasRelated(CurrentEntry))
                {
                    List<Entry> tempMarkRelatedForExport = new List<Entry>();
                    tempMarkRelatedForExport = Related.GetRelatedAsClone(CurrentEntry, Entries);
                    if (tempMarkRelatedForExport != null && tempMarkRelatedForExport.Count > 0)
                    {
                        // Set export marks on related entries
                        for (int i = 0; i < tempMarkRelatedForExport.Count; i++)
                        {
                            tempMarkRelatedForExport[i].MarkExport = CurrentEntry.MarkExport;
                            Log.LogMarkExport(tempMarkRelatedForExport[i].Title, tempMarkRelatedForExport[i].MarkExport);
                        }
                        // Update pool with marks
                        for (int j = 0; j < Entries.Count; j++)
                        {
                            for (int k = 0; k < tempMarkRelatedForExport.Count; k++)
                            {
                                if (Entries[j].ID == tempMarkRelatedForExport[k].ID)
                                {
                                    Entries[j].MarkExport = tempMarkRelatedForExport[k].MarkExport;
                                }
                            }
                        }
                        // Cleanup
                        tempMarkRelatedForExport.Clear();
                    }
                }

                CurrentEntryHasChanges();
                SubmitChangesToEntriesList();
                ResolveExportMenuChecked();
                PopulatePinListAndReselect();
                RefreshSearchList();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.ExportCaption(), false);
                msg.ShowDialog();
                RefreshForm();
            }
        }


        private void markAllForExportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MarkAllForExport();
        }

        private void MarkAllForExport()
        {
            KeepAwake();
            if (Entries != null && Entries.Count > 0)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    Entries[i].MarkExport = true;
                }
                ResolveExportButton();
                ResolveExportMenuChecked();
                PopulatePinListAndReselect();
                RefreshSearchList();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.ExportCaption(), false);
                msg.ShowDialog();
                RefreshForm();
            }
        }

        // Run through all entries and show export button when the 
        // first entry with export is found, and return to reduce CPU load
        private void ResolveExportButton()
        {
            if (HasExportEntries())
            {
                btnExport.Text = "Export (" + GetExportCount().ToString() + ")";
            }

            if (HasExportEntries() && !btnExport.Visible)
            {
                btnExport.Visible = true;
                btnClearExportMarks.Visible = true;
            }
            else if (!HasExportEntries() && btnExport.Visible)
            {
                btnExport.Visible = false;
                btnClearExportMarks.Visible = false;
            }
        }

        private bool HasExportEntries()
        {
            if (Entries != null && Entries.Count > 0)
            {
                foreach (Entry entry in Entries)
                {
                    if (entry.MarkExport)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int tempExportCount;
        private int GetExportCount()
        {
            tempExportCount = 0;
            if (Entries != null && Entries.Count > 0)
            {
                foreach (Entry entry in Entries)
                {
                    if (entry.MarkExport)
                    {
                        tempExportCount += 1;
                    }
                }
            }

            return tempExportCount;
        }

        private bool tempExportMarksCleared;
        private void ClearExportMarks(bool showDialog)
        {
            tempExportMarksCleared = false;

            if (CurrentEntryNotNullOrDummy())
            {
                if (CurrentEntry.MarkExport)
                {
                    CurrentEntry.MarkExport = false;
                    tempExportMarksCleared = true;
                }
            }

            if (HasExportEntries())
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (Entries[i].MarkExport)
                    {
                        Entries[i].MarkExport = false;
                        tempExportMarksCleared = true;
                    }
                }
            }
            Log.LogMarkExport("All", false);
            ResolveExportButton();
            ResolveExportMenuChecked();
            PopulatePinListAndReselect();
            RefreshSearchList();

            if (tempExportMarksCleared && showDialog)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Export collection cleared", MessageText.ExportCaption(), false);
                msg.ShowDialog();
            }
            else if (!tempExportMarksCleared && showDialog)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No entries marked for export", MessageText.ExportCaption(), false);
                msg.ShowDialog();
            }
        }

        private void ResolveExportMenuChecked()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (CurrentEntry.MarkExport)
                {
                    markForExportToolStripMenuItem.Checked = true;
                }
                else
                {
                    markForExportToolStripMenuItem.Checked = false;
                }
            }
        }

        private void btnClearExportMarks_Click(object sender, EventArgs e)
        {
            ClearExportMarks(false);
        }

        private void clearExportCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearExportMarks(true);
        }

        private void importPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportCollection();
        }

        private void exportCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportCollection();
        }

        private string tempExportFilePath = "";
        private string tempExportTimeStamp = "";
        private List<Entry> tempExportCollection;
        private void ExportCollection()
        {
            if (HasExportEntries())
            {
                AhoyAnswerMessageBox msgContinue = new AhoyAnswerMessageBox("*** IMPORTANT ***\nExported entries are not encrypted and may be read by anyone who opens the exported file.\n\nContinue?", MessageText.ExportCaption(), false, false);
                msgContinue.ShowDialog();
                if (!msgContinue.Accepted)
                {
                    return;
                }

                // Initialize temp collection
                if (tempExportCollection == null)
                {
                    tempExportCollection = new List<Entry>();
                }
                tempExportCollection.Clear();

                // Fetch marked entries
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (Entries[i].MarkExport)
                    {
                        tempExportCollection.Add(IO.CloneEntry(Entries[i], false));
                    }
                }

                // Set file name with timestamp ("AhoyExport-2018-02-11-184834.xml")
                tempExportTimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");

                // Log process
                Log.LogExportCollection();
                tempExportFilePath = "";
                // Save file dialog
                tempExportFilePath = IO.SaveFileDialog(2, "AhoyExport-" + tempExportTimeStamp, false);

                if (tempExportFilePath.Length > 0)
                {
                    // Perform export
                    Log.LogImportExportStatus("Export", "Start ->");
                    if (IO.ExportCollection(tempExportCollection, tempExportFilePath))
                    {
                        Log.LogImportExportCollectionStatus(1, "Export success");
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Collection exported", MessageText.ExportCaption(), false);
                        msg.ShowDialog();
                    }
                    else
                    {
                        Log.LogImportExportCollectionStatus(0, "Export failed");
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Export failed!", MessageText.ExportCaption(), false);
                        msg.ShowDialog();
                    }
                    Log.LogImportExportStatus("Export", "End <-");
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No Entries marked for export", MessageText.ExportCaption(), false);
                msg.ShowDialog();
            }
        }

        private string tempImportFilePath = "";
        private string tempImportInfo = "";
        private List<Entry> tempImportCollection;
        private string tempImportCountMessage = "";
        private void ImportCollection()
        {
            if (Entries != null)
            {
                // Initialize temp collection
                if (tempImportCollection == null)
                {
                    tempImportCollection = new List<Entry>();
                }
                tempImportCollection.Clear();

                // Log process
                Log.LogImportCollection();
                tempImportFilePath = "";
                // Open file dialog
                tempImportFilePath = IO.OpenFileDialog(1);

                if (tempImportFilePath.Length > 0)
                {
                    Log.LogImportExportStatus("Import", "Start ->");
                    tempImportCollection = IO.ImportCollection(tempImportFilePath);
                    Log.LogImportExportStatus("Import", "End <-");
                    if (tempImportCollection != null && tempImportCollection.Count > 0)
                    {
                        // Construct import dialog message
                        if (tempImportCollection.Count == 1)
                        {
                            tempImportCountMessage = "Import " + tempImportCollection.Count.ToString() + " Entry?";
                        }
                        else
                        {
                            tempImportCountMessage = "Import " + tempImportCollection.Count.ToString() + " Entries?";
                        }

                        Log.LogImportExportStatus("Import", "Pending commit");
                        AhoyAnswerMessageBox msgImport = new AhoyAnswerMessageBox(tempImportCountMessage, MessageText.ImportCaption(), false, false);
                        msgImport.ShowDialog();
                        if (msgImport.Accepted)
                        {
                            // Save backup in memory
                            SaveBackupForUndo();
                            foreach (Entry entry in tempImportCollection)
                            {
                                Entries.Add(IO.CloneEntry(entry, false));
                            }

                            // Check if any imported entries are pinned
                            if (HasPinned(tempImportCollection))
                            {
                                // Update pinned list to show imported
                                PopulatePinList();
                                // Submit pinned rankings to all entries and re-populate pinned list (is done withing the SubmitPinnedRanking method).
                                SubmitPinnedRanking();
                            }
                            Log.LogImportExportCollectionStatus(1, "Import success (Count: " + tempImportCollection.Count.ToString() + ")");

                            if (tempImportCollection.Count == 1)
                            {
                                tempImportInfo = tempImportCollection.Count.ToString() + " Entry Imported";
                            }
                            else
                            {
                                tempImportInfo = tempImportCollection.Count.ToString() + " Entries Imported";
                            }

                            AhoySimpleMessageBox msg = new AhoySimpleMessageBox(tempImportInfo, MessageText.ImportCaption(), false);
                            msg.ShowDialog();
                            tbSearch.Focus();
                            SetPoolHasChanges(); // Added // TEST
                            SaveAll();
                        }
                        else
                        {
                            Log.LogImportExportStatus("Import", "Aborted");
                        }
                    }
                    else
                    {
                        Log.LogImportExportCollectionStatus(0, "Import failed");
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Import failed. Please revise import file and formatting...", MessageText.ImportCaption(), false);
                        msg.ShowDialog();
                    }
                }
            }
        }

        private bool HasPinned(List<Entry> listOfEntries)
        {
            if (listOfEntries != null && listOfEntries.Count > 0)
            {
                foreach (Entry e in listOfEntries)
                {
                    if (e.Pin)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }


        #endregion


        #region Check form position (addition 22-03-2018)
        bool isInsideBounds;
        public bool IsOnScreen(Form form)
        {
            try
            {
                Log.LogPreloadStatus("Checking screen bounds");
                isInsideBounds = false;

                // Create rectangle
                Rectangle formRectangle = new Rectangle(form.Left, form.Top, form.Width, form.Height);
                isInsideBounds = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(formRectangle));

                if (!isInsideBounds)
                {
                    Log.LogPreloadStatus("Outside bounds");
                }

                return isInsideBounds;
            }
            catch
            {
                Log.LogPreloadStatus("Error: Screen bounds check");
                return true;
            }
        }
        #endregion

        #region Title Tab to Content (addition 29-03-2018)
        private void titleTabToContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleTitleTabToContent();
        }
        #endregion

        #region Insert new lines (addition 29-03-2018)
        private void InsertNewLines(HistoryTextBox textbox)
        {
            if (InsertLines)
            {
                textbox.SetText(textbox.Text + TextTransform.GetNewLines(15), false);
            }
        }

        private void autoAddBlankLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleInsertNewLines();
        }

        #endregion


        #region Title Selection to case...
        private void selectionToUPPERCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionToCase(tbTitle, true);
        }

        private void selectionToLowercaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectionToCase(tbTitle, false);
        }

        private void SelectionToCase(TextBox tb, bool uppercase)
        {
            if (tb.SelectedText.Length > 0)
            {
                if (uppercase)
                {
                    tb.SelectedText = tb.SelectedText.ToUpper();
                }
                else
                {
                    tb.SelectedText = tb.SelectedText.ToLower();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToDecorate(), MessageText.DecorateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region Search fields context menu (addition 29-03-2018)
        private void toolStripMenuItemSearchFieldsCut_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void toolStripMenuItemSearchFieldsCopy_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void toolStripMenuItemSearchFieldsPaste_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }
        #endregion

        #region Search and replace (addition 19-04-2018)
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEntryNotNullOrDummy())
            {
                if (!CurrentEntry.Protected)
                {
                    MessageBoxes.SearchAndReplace replaceDialog = new MessageBoxes.SearchAndReplace(tbContent);
                    replaceDialog.TopMost = true;
                    replaceDialog.StartPosition = FormStartPosition.CenterParent;
                    replaceDialog.ShowDialog();
                    if (replaceDialog.Success)
                    {
                        //tbContent.SetText(replaceDialog.ResultText, false);
                        PostMessageDialogAfterReplacing(replaceDialog.GetMatchCount());
                    }
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EntryWriteProtectedMessage(), MessageText.FindAndReplaceCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectEntryToContinueMessage(), MessageText.FindAndReplaceCaption(), false);
                msg.ShowDialog();
            }
        }

        private string tempFindReplaceResult;
        private void PostMessageDialogAfterReplacing(int countReplaced)
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

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceToolStripMenuItem_Click(this, null);
        }

        #endregion


        #region Advanced Pool operations (addition 21-05-2018)
        // New Pool (Name and create new profile)
        private void newPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPool();
        }

        // Create new Pool and ask for credentials + path
        private void NewPool()
        {
            // Abort operation if demo
            if (AbortIfDemo())
            {
                return;
            }

            // Prepare status
            bool NewPoolSuccess = false;
            // Save current custom pool values
            bool WasCustomFilePath = IO.UsingCustomFilePath();
            string CustomFilePath = IO.GetCustomFilePathAndName();
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.CreateNewPool(), MessageText.NewPoolCaption(), false, false);
            msg.TopMost = true;
            msg.ShowDialog();

            if (msg.Accepted)
            {
                // Save everything if changes are present
                SaveAll(); // <------------- SAVE ALL
                try
                {
                    // Stop the global timer
                    StopGlobalTimers();
                    // Reset everything before loading
                    ClearAllEntriesFromMemory(true);
                    // Set demo
                    IO.UseDemoPool = false;
                    // Reset WPM Counter
                    WPMCounter.ClearWPMCounter();
                RetryNewPool: // Goto statement
                    // The new pool gets its own password (and therefore its own encryption key)
                    if (AskForNewPoolPassword("Create a password for the new Pool", MessageText.NewPoolCaption()))
                    {

                        AlternateBackupPath = "";
                        AlternateBackupPathStringIsSet();
                        // Remove readonly
                        TogglePoolReadOnly(true, false, false);

                        // Save changes
                        SetPoolHasChanges();

                        tempSaveFilePath = "";
                        // Save file dialog
                        tempSaveFilePath = IO.SaveFileDialog(0, "", false);

                        // Check result
                        if (tempSaveFilePath.Length > 0 && IO.ValidateCustomDirectoryPath(tempSaveFilePath))
                        {
                            // Save pool (with write protection override)
                            OverrideToSetWriteProtection = true;
                            SavePool(false, false, true, tempSaveFilePath);
                            IO.SetAndValidateCustomFilePath(tempSaveFilePath);
                            NewPoolSuccess = true;
                        }
                        else
                        {
                            NewPoolSuccess = false;
                            AhoySimpleMessageBox msgFailed = new AhoySimpleMessageBox(MessageText.PoolCreationErrorRetry(), MessageText.NewPoolCaption(), false);
                            msgFailed.ShowDialog();
                            goto RetryNewPool; // Retry

                        }

                        // If success
                        if (NewPoolSuccess)
                        {
                            // Update status strip and title
                            this.Text = GetFormTitle();
                            ResolveIndicatorLabel();
                            SetStatusText(MessageText.PoolSavedMessage());
                            AhoySimpleMessageBox message = new AhoySimpleMessageBox(MessageText.PoolSavedMessage(), MessageText.NewPoolCaption(), false);
                            message.TopMost = true;
                            message.ShowDialog();
                            // Resolve status
                            // Load the newly created pool from custom filepath
                            LoadPool(true, "");
                        }
                    }
                    else
                    {
                        AhoySimpleMessageBox msgFailed = new AhoySimpleMessageBox(MessageText.PoolCreationAbortedLoadDefault(), MessageText.NewPoolCaption(), false);
                        msgFailed.ShowDialog();

                        // If failed fall back to previously loaded pool or default pool
                        if (WasCustomFilePath && CustomFilePath.Length > 0)
                        {
                            LoadPool(true, CustomFilePath);
                        }
                        else
                        {
                            ReloadDefaultPool(false, false, false);
                        }
                    }
                }
                catch
                {
                    AhoySimpleMessageBox msgFailed = new AhoySimpleMessageBox(MessageText.PoolCreationErrorExit(), MessageText.NewPoolCaption(), false);
                    msgFailed.ShowDialog();
                    Application.Exit();
                }
                finally
                {
                    // Start the global timer
                    StartGlobalTimers();
                    ShowPinned();
                }
            }
        }


        #endregion


        #region Set opacity from View menu (addition 27-06-2018)

        // 100%
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFormOpacity(Settings.DefaultOpacity, true);
        }

        // 95%
        private void toolStripMenuItem70_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.95d, true);
        }

        // 90%
        private void toolStripMenuItem71_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.90d, true);
        }

        // 85%
        private void toolStripMenuItem72_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.85d, true);
        }

        // 75%
        private void toolStripMenuItem73_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.75d, true);
        }

        // 65%
        private void toolStripMenuItem74_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.65d, true);
        }

        // 50%
        private void toolStripMenuItem69_Click(object sender, EventArgs e)
        {
            SetFormOpacity(0.50d, true);
        }

        private void ResolveOpacityMenuCheck()
        {
            if (FormOpacity == Settings.DefaultOpacity)
            {
                defaultToolStripMenuItem.Checked = true;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.95d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = true;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.90d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = true;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.85d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = true;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.75d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = true;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.65d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = true;
                toolStripMenuItem69.Checked = false;
            }
            else if (FormOpacity == 0.50d)
            {
                defaultToolStripMenuItem.Checked = false;
                toolStripMenuItem70.Checked = false;
                toolStripMenuItem71.Checked = false;
                toolStripMenuItem72.Checked = false;
                toolStripMenuItem73.Checked = false;
                toolStripMenuItem74.Checked = false;
                toolStripMenuItem69.Checked = true;
            }
        }
        #endregion


        #region Handle Home Screen types (addition 28-01-2019)
        // Make logo selections 
        private void layoutAANSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!layoutAToolStripMenuItem.Checked)
            {
                SetAnsiLogo();
            }
        }

        private void layoutBASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!layoutBToolStripMenuItem.Checked)
            {
                SetAsciiLogo();
            }
        }

        private void labyrinthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!labyrinthToolStripMenuItem.Checked)
            {
                SetLabyrinthLogo();
            }
        }

        private void layoutCCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!layoutCToolStripMenuItem.Checked)
            {
                SetCustomDisplayText();
            }
        }

        private void SetAnsiLogo()
        {
            SelectDummyTextVersion(0);
        }

        private void SetAsciiLogo()
        {
            SelectDummyTextVersion(1);
        }

        private void SetLabyrinthLogo()
        {
            SelectDummyTextVersion(2);
        }

        // Resolve Home Screen menu buttons
        private void ResolveHomeScreenSelectionMenu()
        {
            layoutAToolStripMenuItem.Checked = false;
            layoutBToolStripMenuItem.Checked = false;
            layoutCToolStripMenuItem.Checked = false;
            labyrinthToolStripMenuItem.Checked = false;

            // If custom display text
            if (!string.IsNullOrEmpty(CustomDisplayText))
            {
                layoutCToolStripMenuItem.Checked = true;
            }
            else if (DisplayTextCycle == 0) // If displaytext 0
            {
                layoutAToolStripMenuItem.Checked = true;
            }
            else if (DisplayTextCycle == 1) // If displaytext 1
            {
                layoutBToolStripMenuItem.Checked = true;
            }
            else if (DisplayTextCycle == 2) // If displaytext 2
            {
                labyrinthToolStripMenuItem.Checked = true;
            }
        }
        #endregion


        #region Colorize title field according to pinned color (addition 12-02-2019)
        private void ResetTitleColorOnCurrentEntry()
        {
            if (CurrentEntry != null) // check for null
            {
                CurrentEntry.PresetColor = 0;
                CurrentEntry.CustomColor = "";
                RefreshTitleColor();
            }
        }

        private void RefreshTitleColor()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // Standard/Theme color
                if (CurrentEntry.PresetColor == 0 && string.IsNullOrEmpty(CurrentEntry.CustomColor))
                {
                    SetDefaultTitleColors();
                }
                else // Custom color
                {
                    SetCustomTitleColors(CurrentEntry.PresetColor, CurrentEntry.CustomColor);
                }

            }
            else
            {
                SetDefaultTitleColors();
            }
        }

        // Set default title colors
        private void SetDefaultTitleColors()
        {
            // Background
            panel2.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbTitle.BackColor = AhoyColors.CurrentColorTheme()[29];
            // Foreground
            tbTitle.ForeColor = AhoyColors.CurrentColorTheme()[30];
        }

        // Set title colors according to entry color status
        private void SetCustomTitleColors(int presetColor, string customColor)
        {
            if (presetColor > 0)
            {
                // Background
                panel2.BackColor = Design.ReturnStatusBackgroundColor(presetColor);
                tbTitle.BackColor = Design.ReturnStatusBackgroundColor(presetColor);
                // Foreground
                tbTitle.ForeColor = Design.ReturnStatusForegroundColor(presetColor);
            }
            else if (AhoyColors.HasDualHexValue(customColor))
            {
                try
                {
                    // Background
                    panel2.BackColor = AhoyColors.ConvertFromHex(customColor.Split(';')[0], false);
                    tbTitle.BackColor = AhoyColors.ConvertFromHex(customColor.Split(';')[0], false);
                    // Foreground
                    tbTitle.ForeColor = AhoyColors.ConvertFromHex(customColor.Split(';')[1], false);
                }
                catch
                {
                    Log.GeneralEntryStatus("Error converting color codes");
                }
            }
        }
        #endregion


        #region Checklist/Progress Bar (addition 28-02-2019)
        private void checklistTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertChecklistTemplate();
        }

        private void InsertChecklistTemplate()
        {
            if (CurrentEntryNotNullOrDummy() && tbContent.Focused)
            {
                try
                {
                    AhoyAnswerMessageBox msgAccept = new AhoyAnswerMessageBox(MessageText.ChecklistInsertionMessage(), MessageText.EntryTemplateCaption(), false, false);
                    msgAccept.TopMost = true;
                    msgAccept.ShowDialog();
                    if (!msgAccept.Accepted)
                    {
                        return;
                    }

                    if (EntryTemplate.HasTemplate(tbContent.Text))
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EntryTemplateAlreadyActiveMessage(MessageText.ProgressBarCaption()), MessageText.EntryTemplateCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                        return;
                    }

                    Log.LogEntryTemplateStatus("Checklist");
                    // Save text, if any in the clipboard
                    string tempText = "";
                    if (Clipboard.ContainsText())
                    {
                        tempText = Clipboard.GetText();
                    }

                    // Ask for standard or matrix
                    AhoyAnswerMessageBox msgSelectType = new AhoyAnswerMessageBox("*** Please select template style ***\nStandard = Tag based\nMatrix = Grid based", "Template Style", true, false);
                    msgSelectType.SetCustomButtonText("Cancel", "Standard", "Matrix", "Template Style", "(tags)", "(grid)", "", 0);
                    msgSelectType.ShowDialog();

                    if (msgSelectType.Canceled)
                    {
                        Log.LogEntryTemplateStatus("Insertion canceled");
                        return;
                    }
                    else if (msgSelectType.Accepted) // Standard
                    {
                        // Get template text
                        SetClipboardText(IO.ChecklistTemplateTextReader(0) + TextTransform.GetNewLines(2));
                    }
                    else if (!msgSelectType.Accepted) // Matrix
                    {
                        // Get template text
                        SetClipboardText(IO.ChecklistTemplateTextReader(1) + TextTransform.GetNewLines(2));
                    }

                    // Go to index 0
                    if (tbContent.Text.Length > 0)
                    {
                        tbContent.Select(0, 0);
                    }
                    //Paste into content
                    tbContent.Paste();
                    // Add text back to clipboard
                    if (tempText.Length > 0)
                    {
                        SetClipboardText(tempText);
                    }
                }
                catch
                {
                    Log.LogEntryTemplateStatus("Error");
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.InsertionFailedMessage("Checklist"), MessageText.EntryTemplateCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region Bookmark template (addition 17-04-2019)
        private void bookmarkTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertBookmarkTemplate();
        }
        private void InsertBookmarkTemplate()
        {
            if (CurrentEntryNotNullOrDummy() && tbContent.Focused)
            {
                try
                {
                    if (EntryTemplate.IdentifyBookmarkTemplate(tbContent.Text))
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EntryTemplateAlreadyActiveMessage(MessageText.BookmarkCaption()), MessageText.EntryTemplateCaption(), false);
                        msg.TopMost = true;
                        msg.ShowDialog();
                        return;
                    }

                    Log.LogEntryTemplateStatus("Bookmark");
                    // Save text, if any in the clipboard
                    string tempText = "";
                    if (Clipboard.ContainsText())
                    {
                        tempText = Clipboard.GetText();
                    }
                    // Get template text
                    SetClipboardText(IO.BookmarkTemplateTextReader() + TextTransform.GetNewLines(2));
                    // Go to index 0
                    if (tbContent.Text.Length > 0)
                    {
                        tbContent.Select(0, 0);
                    }
                    //Paste into content
                    tbContent.Paste();
                    // Add text back to clipboard
                    if (tempText.Length > 0)
                    {
                        SetClipboardText(tempText);
                    }
                }
                catch
                {
                    Log.LogEntryTemplateStatus("Error");
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.InsertionFailedMessage("Bookmark"), MessageText.BookmarkCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion


        #region PINNED COLOR TABLES
        private void ListThemeColors()
        {
            AhoyInfoMessageBox msg = new AhoyInfoMessageBox(AhoyColors.GetListOfThemeColorsAsString(), "Current Theme", false, false, this.Opacity);
            msg.ShowDialog();
        }

        private void GetThemeColors()
        {
            int dots = 16;
            string tempIndex = "Index";
            string tempTitle = "Title";
            string tempBackground = "Background";
            string tempLabels = "Labels";
            string tempHighlight = "Highlight";
            string tempFields = "Fields";
            string tempLists = "Lists";
            string tempBorder = "Border";
            string tempText = "Text";
            string tempAlt1 = "Alt 1";
            string tempAlt2 = "Alt 2";
            //string tempDefault = "Default";
            string tempPreset = "Preset";
            string tempCustom = "Custom";

            // Headline
            string tempOutput = "THEME COLOR TABLE" + Environment.NewLine;
            tempOutput += "-----------------" + Environment.NewLine + Environment.NewLine;
            // Index
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempIndex, dots, ".", (AhoyColors.ColorTheme + 1).ToString()) + Environment.NewLine;
            // Title
            if (AhoyColors.HasCustomColors())
            {
                // Custom tile
                tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempTitle, dots, ".", "\"" + AhoyColors.CustomColorThemeTitle + "\"");
            }
            else
            {
                // Preset title
                tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempTitle, dots, ".", "\"" + AhoyColors.GetThemeTitleByIndex(AhoyColors.ColorTheme) + "\"");
            }

            // Add 2 x newlines
            tempOutput += TextTransform.GetNewLines(2);
            // Interface headline
            tempOutput += "Main colors" + Environment.NewLine;
            tempOutput += "-----------" + Environment.NewLine;

            // Theme colors HEX/RGB
            // Background
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempBackground, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[0])) + Environment.NewLine;
            // Labels
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempLabels, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[2])) + Environment.NewLine;
            // Highlignts
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempHighlight, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[6], AhoyColors.CurrentColorTheme()[5])) + Environment.NewLine;
            // Fields
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempFields, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[29], AhoyColors.CurrentColorTheme()[30])) + Environment.NewLine;
            // Lists
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempLists, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[1], AhoyColors.CurrentColorTheme()[4]));

            // Add 2 x newlines
            tempOutput += TextTransform.GetNewLines(2);
            // Interface headline
            tempOutput += "Interface (buttons and menus)" + Environment.NewLine;
            tempOutput += "-----------------------------" + Environment.NewLine;
            // Button and menu backgrounds
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempBackground, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[7])) + Environment.NewLine;
            // Button border
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempBorder, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[9])) + Environment.NewLine;
            // Text
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempText, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[8])) + Environment.NewLine;
            // Alt 1
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempAlt1, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[10])) + Environment.NewLine;
            // Alt 2
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempAlt2, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[11]));

            // Add 2 x newlines
            tempOutput += TextTransform.GetNewLines(2);
            // Pin presets
            tempOutput += "Pin Presets" + Environment.NewLine;
            tempOutput += "-----------" + Environment.NewLine;
            // Preset 1
            tempPreset = "Preset 1";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[12], AhoyColors.CurrentColorTheme()[20])) + Environment.NewLine;
            tempPreset = "Preset 2";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[13], AhoyColors.CurrentColorTheme()[21])) + Environment.NewLine;
            tempPreset = "Preset 3";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[14], AhoyColors.CurrentColorTheme()[22])) + Environment.NewLine;
            tempPreset = "Preset 4";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[15], AhoyColors.CurrentColorTheme()[23])) + Environment.NewLine;
            tempPreset = "Preset 5";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[16], AhoyColors.CurrentColorTheme()[24])) + Environment.NewLine;
            tempPreset = "Preset 6";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[17], AhoyColors.CurrentColorTheme()[25])) + Environment.NewLine;
            tempPreset = "Preset 7";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[18], AhoyColors.CurrentColorTheme()[26])) + Environment.NewLine;
            tempPreset = "Preset 8";
            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[19], AhoyColors.CurrentColorTheme()[27]));
            // Add 2 x newlines
            tempOutput += TextTransform.GetNewLines(2);
            // Pin presets
            tempOutput += "Custom Colors" + Environment.NewLine;
            tempOutput += "-------------" + Environment.NewLine;

            List<Entry> tempCollectedEntries = new List<Entry>();

            if (EntriesExist() && HasCustomColors())
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if ((Entries[i].Pin || Related.IsRelated(Entries[i], Entries)) && !string.IsNullOrEmpty(Entries[i].CustomColor))
                    {
                        tempCollectedEntries.Add(Entries[i]);

                    }
                }

                if (tempCollectedEntries.Count > 0)
                {
                    // Sort entries according tor rank
                    tempCollectedEntries.Sort(delegate (Entry x, Entry y) { return x.Rank.CompareTo(y.Rank); });
                    // Add collection to text output
                    for (int i = 0; i < tempCollectedEntries.Count; i++)
                    {
                        tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCustom, dots, ".", AhoyColors.GetColorTableFromHex(tempCollectedEntries[i].CustomColor.Split(';')[0], tempCollectedEntries[i].CustomColor.Split(';')[1])) + Environment.NewLine;
                        tempOutput += " (Rank: " + tempCollectedEntries[i].Rank.ToString() + " " + tempTitle + ": \"" + tempCollectedEntries[i].Title + "\")" + Environment.NewLine + Environment.NewLine;
                    }
                }
                else
                {
                    tempOutput += "N/A";
                }

            }
            else
            {
                tempOutput += "N/A";
            }
            // Add 2 x newlines
            tempOutput += TextTransform.GetNewLines(2);
            // Get Pinned colors
            tempOutput = GetPinnedColorValues(tempOutput, false);
            tempOutput += Environment.NewLine;
            AhoySearchMessageBox msg = new AhoySearchMessageBox(tempOutput, "Color Table", false, false, this.Opacity);
            msg.SetSize(725, 725);
            msg.Show();
            msg.BringToFront();
        }

        // Display color theme as CSV (like the embedded theme files and theme export)
        private void GetThemeColorsCsv()
        {
            // Headline
            string tempOutput = AhoyColors.GetThemeTitleByIndex(AhoyColors.ColorTheme) + "\n";
            for (int i = 0; i < 31; i++)
            {
                tempOutput += AhoyColors.ConvertToHex(AhoyColors.CurrentColorTheme()[i]) + ";";
            }
            AhoySearchMessageBox msg = new AhoySearchMessageBox(tempOutput, "CSV", false, false, this.Opacity);
            msg.SetSize(725, 725);
            msg.Show();
            msg.BringToFront();
        }





        private string GetPinnedColorValues(string precedingString, bool withTitle)
        {
            List<Entry> tempCollectedEntries = new List<Entry>();
            tempCollectedEntries.Clear();
            int tempTotalCount = 0;
            int tempCustomCount = 0;
            int tempPresetCount = 0;
            int tempCounter = 0;
            int dots = 21;
            string tempPreset = "Preset";
            string tempCustom = "Custom";
            string tempOutput = "";
            // Add previous values if any
            if (!string.IsNullOrEmpty(precedingString))
            {
                tempOutput = precedingString;
            }
            // Pin presets
            if (withTitle)
            {
                tempOutput += "COLOR TABLE" + Environment.NewLine + Environment.NewLine;
            }
            tempOutput += "Pinned colors (in order)" + Environment.NewLine;
            tempOutput += "Index Rank Type Color (Background/Text (HEX/RGB))" + Environment.NewLine;
            tempOutput += "-------------------------------------------------" + Environment.NewLine;

            if (EntriesExist() && HasPinned(Entries))
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    // If entry has custom or preset color
                    if ((Entries[i].Pin || Related.IsRelated(Entries[i], Entries)) && (AhoyColors.HasDualHexValue(Entries[i].CustomColor) || Entries[i].PresetColor > 0))
                    {
                        tempTotalCount += 1;
                        tempCollectedEntries.Add(Entries[i]);
                        // tempOutput += Entries[i].CustomColor + " " + Entries[i].PresetColor + Environment.NewLine; //  Debug
                    }
                }

                // If any entries was collected
                if (tempCollectedEntries.Count > 0)
                {
                    // Sort entries according tor rank
                    tempCollectedEntries.Sort(delegate (Entry x, Entry y) { return x.Rank.CompareTo(y.Rank); });
                    // Add collection to text output
                    tempCustom = "Custom";
                    tempPreset = "Preset";
                    for (int i = 0; i < tempCollectedEntries.Count; i++)
                    {
                        tempCounter += 1;
                        // If entry has custom color
                        if (AhoyColors.HasDualHexValue(tempCollectedEntries[i].CustomColor))
                        {
                            tempCustomCount += 1;
                            tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempCollectedEntries[i].Rank.ToString("0000") + " " + tempCustom, dots, ".", AhoyColors.GetColorTableFromHex(tempCollectedEntries[i].CustomColor.Split(';')[0], tempCollectedEntries[i].CustomColor.Split(';')[1])) + Environment.NewLine;
                        }
                        else if (tempCollectedEntries[i].PresetColor > 0) // If entry has preset color
                        {
                            tempPresetCount += 1;
                            switch (tempCollectedEntries[i].PresetColor)
                            {
                                case 1:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 1";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[12], AhoyColors.CurrentColorTheme()[20])) + Environment.NewLine;
                                    break;
                                case 2:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 2";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[13], AhoyColors.CurrentColorTheme()[21])) + Environment.NewLine;
                                    break;
                                case 3:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 3";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[14], AhoyColors.CurrentColorTheme()[22])) + Environment.NewLine;
                                    break;
                                case 4:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 4";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[15], AhoyColors.CurrentColorTheme()[23])) + Environment.NewLine;
                                    break;
                                case 5:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 5";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[16], AhoyColors.CurrentColorTheme()[24])) + Environment.NewLine;
                                    break;
                                case 6:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 6";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[17], AhoyColors.CurrentColorTheme()[25])) + Environment.NewLine;
                                    break;
                                case 7:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 7";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[18], AhoyColors.CurrentColorTheme()[26])) + Environment.NewLine;
                                    break;
                                case 8:
                                    tempPreset = tempCollectedEntries[i].Rank.ToString("0000") + " Preset 8";
                                    tempOutput += TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempCounter.ToString("0000") + " " + tempPreset, dots, ".", AhoyColors.GetColorTableFromRGB(AhoyColors.CurrentColorTheme()[19], AhoyColors.CurrentColorTheme()[27])) + Environment.NewLine;
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    tempOutput += "N/A";
                }
            }
            else
            {
                tempOutput += "N/A";
            }

            string tempTotalWithColors = "Total with colors ";
            string tempTotalPreset = "Total preset ";
            string tempTotalCustom = "Total custom ";

            // Ad an extra new line
            tempOutput += Environment.NewLine + tempTotalWithColors + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempTotalWithColors + tempTotalCount.ToString("0000"), 26, ".") + " " + tempTotalCount.ToString("0000") + Environment.NewLine;
            tempOutput += tempTotalPreset + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempTotalPreset + tempPresetCount.ToString("0000"), 26, ".") + " " + tempPresetCount.ToString("0000") + Environment.NewLine;
            tempOutput += tempTotalCustom + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(tempTotalCustom + tempCustomCount.ToString("0000"), 26, ".") + " " + tempCustomCount.ToString("0000") + Environment.NewLine;

            //tempOutput += Environment.NewLine + "Total with colors: " + tempTotalCount + Environment.NewLine +
            //    "Total preset: " + tempPresetCount + Environment.NewLine +
            //    "Total custom: " + tempCustomCount + Environment.NewLine;
            tempOutput += Environment.NewLine;
            return tempOutput;
        }

        private void DisplayColorTable()
        {
            string tempOutput = GetPinnedColorValues(string.Empty, true);
            AhoySearchMessageBox msg = new AhoySearchMessageBox(tempOutput, "Color Table", false, false, this.Opacity);
            msg.SetSize(725, 725);
            msg.Show();
            msg.BringToFront();
        }

        private void ColorTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayColorTable();
        }

        private bool HasCustomColors()
        {
            if (EntriesExist())
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (Entries[i].Pin && AhoyColors.HasDualHexValue(Entries[i].CustomColor))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Select sample color
        private void Example1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check the tag of the current selection and act accordingly
            if (CurrentEntryNotNullOrDummy())
            {
                try
                {
                    if (((ToolStripMenuItem)sender).Tag != null)
                    {
                        int colorSample = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
                        string colorString = AhoyColors.ConvertToHex(Design.ReturnCustomBackgroundColor(colorSample)) + ";" + AhoyColors.ConvertToHex(Design.ReturnCustomForegroundColor(colorSample));

                        if (AhoyColors.HasDualHexValue(colorString))
                        {
                            // Save entry before continuing
                            SaveCurrentEntry(!AutoSaveEntryChanges);
                            Log.GeneralEntryStatus("Pinned color (" + colorString + ")");
                            CurrentEntry.CustomColor = colorString;
                            CurrentEntry.PresetColor = 0;

                            // Set related entries as well
                            DistributeColorsToRelated();

                            // Submit changes
                            SubmitStatusChanges();

                            // Populate and reselect pinned item
                            PopulatePinListAndReselect();
                        }
                        else
                        {
                            // Do nothing
                        }
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
            else
            {
                DisplayPinnedSelectionMessage();
            }

        }
        #endregion



        #region Clock (addition 02-05-2019)
        private void ToggleClock()
        {
            ShowClock = !ShowClock;
            RefreshDummyText();
            ResolveClockMenuItem();
            SettingsHasChanges = true;
        }

        private void showClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleClock();
        }

        private void ResolveClockMenuItem()
        {
            showClockToolStripMenuItem.Checked = ShowClock;
        }

        private void titlePrioritySearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitlePrioritySearch = !TitlePrioritySearch;
            Log.GeneralStatus("Title priority search " + GetTitlePriorityString());
            ResolveTitlePrioritySearchMenuItem();
            SettingsHasChanges = true;
        }

        private void ResolveTitlePrioritySearchMenuItem()
        {
            if (TitlePrioritySearch)
            {
                titlePrioritySearchToolStripMenuItem.Checked = true;
            }
            else
            {
                titlePrioritySearchToolStripMenuItem.Checked = false;
            }
        }
        #endregion

        #region Expand/Collapse search list (addition 09-06-2019)
        private int ListOriginalHeight { get; set; }
        private bool ListExpanded { get; set; }
        private void InitializePlusMinusButton()
        {
            // Initialize list height and plus/minus button
            ListOriginalHeight = listBoxSearchResult.Height;
            picExpandResult.Size = new Size(17, 16);
            picExpandResult.Click += PicExpandResult_Click;
            toolTip1.SetToolTip(this.picExpandResult, "Expand View (CTRL+ALT+Y)");
            SetExpanderColorTheme();
            CollapseList();
            PositionExpander();
        }

        private void SetExpanderColorTheme()
        {
            // Set image
            if (!ListExpanded)
            {
                picExpandResult.Image = ButtonBitmaps.Expand();
            }
            else
            {
                picExpandResult.Image = ButtonBitmaps.Collapse();
            }
            // Set background color
            picExpandResult.BackColor = AhoyColors.CurrentColorTheme()[7];
        }

        private void PicExpandResult_Click(object sender, EventArgs e)
        {
            PauseLiveSearch(); // Pause to prevent clearing
            if (!ListExpanded)
            {
                ExpandList();
            }
            else
            {
                CollapseList();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            CollapseList();
            PositionExpander();
        }

        private void listBoxSearchResult_SizeChanged(object sender, EventArgs e)
        {
            PositionExpander();
        }

        private void PositionExpander()
        {
            // Position the plus/minus box (note: the scrollbar makes it better to place the button in center of the list)
            picExpandResult.Location = new Point((listBoxSearchResult.Location.X + (listBoxSearchResult.Width / 2) - 8), (listBoxSearchResult.Location.Y + listBoxSearchResult.Height - 16));
        }

        // Toggle list expansion
        private void picPlusMinus_Click(object sender, EventArgs e)
        {
            if (!ListExpanded)
            {
                ExpandList();
            }
            else
            {
                CollapseList();
            }
        }

        private void BringExpanderToFront()
        {
            // Bring list to front
            listBoxSearchResult.BringToFront();
            // And bring image to front
            picExpandResult.BringToFront();
        }

        private void ExpandList()
        {
            KeepAwake();
            toolTip1.SetToolTip(this.picExpandResult, "Collapse View (CTRL+ALT+Y)");
            ListExpanded = true;
            // Use button as reference for bottom expansion
            listBoxSearchResult.Height = listBoxSearchResult.Location.Y + btnPinnedUp.Location.Y - 159;
            // Set image
            picExpandResult.Image = ButtonBitmaps.Collapse();
            BringExpanderToFront();
            RemoveNoLiveSearchResultsLabel();
        }

        private void CollapseList()
        {
            KeepAwake();
            toolTip1.SetToolTip(this.picExpandResult, "Expand View (CTRL+ALT+Y)");
            ListExpanded = false;
            // Return to original height
            listBoxSearchResult.Height = ListOriginalHeight;
            // Set image
            picExpandResult.Image = ButtonBitmaps.Expand();
            RemoveNoLiveSearchResultsLabel();
        }
        #endregion

        #region Live search (addition 10-06-2019)

        private Timer timedLiveSearchBlock;
        private bool BlockLiveSearchTimed = false;
        private void PauseLiveSearch()
        {
            if (timedLiveSearchBlock == null)
            {
                timedLiveSearchBlock = new Timer();
                timedLiveSearchBlock.Interval = 2000;
                timedLiveSearchBlock.Tick += TimedLiveSearchBlock_Tick;
                timedLiveSearchBlock.Enabled = false;
            }

            BlockLiveSearchTimed = true;
            timedLiveSearchBlock.Enabled = true;
        }

        private void TimedLiveSearchBlock_Tick(object sender, EventArgs e)
        {
            timedLiveSearchBlock.Enabled = false;
            BlockLiveSearchTimed = false;

        }

        private bool BlockLiveSearch { get; set; }
        private Timer timerLiveSearch;
        private void InitializeLiveSearch()
        {
            if (timerLiveSearch == null)
            {
                timerLiveSearch = new Timer();
                timerLiveSearch.Interval = 400;
                timerLiveSearch.Tick += TimerLiveSearch_Tick;
                timerLiveSearch.Enabled = false;
            }

            BlockLiveSearch = false;
            tbSearch.KeyUp += TbSearch_KeyUp;
            tbSearch.TextChanged += TbSearch_ImeTextCommitted;
        }

        // Set while an IME (Japanese, Chinese, ...) is composing in the search box
        private bool ImeInputPending { get; set; }

        private void TbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // Keys typed into an IME: wait until the text is committed (see TbSearch_ImeTextCommitted),
            // searching now would interrupt the composition
            if (ImeHelper.IsImeKey(e))
            {
                ImeInputPending = true;
                return;
            }

            if (!BlockLiveSearchTimed)
            {
                if (BlockLiveSearch)
                {
                    BlockLiveSearch = false;
                }
                else
                {
                    PerformLiveSearch(false);
                }
            }
        }

        // The IME committed its text: run the live search on the final text
        private void TbSearch_ImeTextCommitted(object sender, EventArgs e)
        {
            if (ImeInputPending && !ImeHelper.IsComposing(tbSearch))
            {
                ImeInputPending = false;
                if (!BlockLiveSearchTimed && !BlockLiveSearch)
                {
                    PerformLiveSearch(false);
                }
            }
        }

        private void TimerLiveSearch_Tick(object sender, EventArgs e)
        {
            // Still composing in an IME: try again on the next tick
            if (ImeHelper.IsComposing(tbSearch))
            {
                return;
            }
            timerLiveSearch.Enabled = false;
            if (tbSearch.Text.Length > 1)
            {
                LiveSearchCurrentQuery = tbSearch.Text.Trim();
                if (LiveSearchCurrentQuery != LiveSearchPreviousQuery)
                {
                    LiveSearchPreviousQuery = LiveSearchCurrentQuery;
                    // Indicate selection by search result
                    EntrySelectedBySearchResult = true;
                    PerformSearch(LiveSearchCurrentQuery, true);
                }
            }
        }

        private string LiveSearchPreviousQuery = "";
        private string LiveSearchCurrentQuery = "";
        private void PerformLiveSearch(bool forceSearch)
        {
            if (LiveSearch && !BlockLiveSearch)
            {
                if (tbSearch.Text.Length > 1)
                {
                    if (forceSearch)
                    {
                        LiveSearchPreviousQuery = "";
                    }

                    SetStatusText("");
                    if (!timerLiveSearch.Enabled)
                    {
                        timerLiveSearch.Enabled = true;
                    }
                }
                else
                {
                    ClearSearchResultsList();
                }
            }
        }

        private void ClearSearchResultsList()
        {
            LiveSearchPreviousQuery = "";
            listBoxSearchResult.Items.Clear();
            ClearEntrySearchResults();
            tbSearch.Focus();
        }

        private Timer tempNoLiveSearchResult;
        private void NoLiveSearchResult()
        {
            if (LiveSearch)
            {
                if (tempNoLiveSearchResult == null)
                {
                    tempNoLiveSearchResult = new Timer();
                    tempNoLiveSearchResult.Interval = 3000;
                    tempNoLiveSearchResult.Tick += TempNoLiveSearchResult_Tick;
                    tempNoLiveSearchResult.Enabled = false;
                }

                // Set colors and font every time to get latest theme/settings
                lbNoSearchResult.ForeColor = listBoxSearchResult.ForeColor; // Take color from list
                lbNoSearchResult.BackColor = listBoxSearchResult.BackColor; // Take color from list
                lbNoSearchResult.SetBounds(listBoxSearchResult.Location.X, listBoxSearchResult.Location.Y, listBoxSearchResult.Width, listBoxSearchResult.Height);
                lbNoSearchResult.Font = Design.ListBoxFont(ListsFontSize); // Same font as list
                lbNoSearchResult.BringToFront();
                picExpandResult.BringToFront(); // Bring expander to front (on top of search result status)
                lbNoSearchResult.Visible = true;
                if (!tempNoLiveSearchResult.Enabled) // Only set new message if timer not already running
                {
                    lbNoSearchResult.Text = GetNoLiveSearchResultMessage(); // Set text
                }
                tempNoLiveSearchResult.Enabled = true; // Enable timer to hide the label again
            }
        }

        private void RemoveNoLiveSearchResultsLabel()
        {
            if (lbNoSearchResult != null && lbNoSearchResult.Visible)
            {
                lbNoSearchResult.Visible = false;
            }
        }

        private void TempNoLiveSearchResult_Tick(object sender, EventArgs e)
        {
            tempNoLiveSearchResult.Enabled = false;
            lbNoSearchResult.Visible = false;
        }

        private Label lbNoSearchResult;
        private void InitializeSearchResultLabel()
        {
            // Create label for text animation if not already initialized
            // with pinned list as reference
            if (lbNoSearchResult == null)
            {
                // Use the pinned list color theme styling for the animation
                lbNoSearchResult = new Label();
                lbNoSearchResult.SetBounds(listBoxSearchResult.Location.X, listBoxSearchResult.Location.Y, listBoxSearchResult.Width, listBoxSearchResult.Height);
                lbNoSearchResult.Font = Design.ListBoxFont(ListsFontSize);
                lbNoSearchResult.ForeColor = AhoyColors.CurrentColorTheme()[6];
                lbNoSearchResult.BackColor = AhoyColors.CurrentColorTheme()[29];
                lbNoSearchResult.TextAlign = ContentAlignment.TopCenter;
                lbNoSearchResult.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
                this.splitContainer1.Panel1.Controls.Add(lbNoSearchResult);
                lbNoSearchResult.Cursor = Cursors.Default;
                lbNoSearchResult.BringToFront();
                lbNoSearchResult.Text = GetNoLiveSearchResultMessage(); // Set text
                lbNoSearchResult.Visible = false;
            }
        }

        private static Random tempRandSearchResult;
        private string tempPreviousNoResultMessage = "";
        private string tempCurrentNoResultMessage = "";
        private string GetNoLiveSearchResultMessage()
        {
            if (tempRandSearchResult == null)
            {
                tempRandSearchResult = new Random();
            }

            // If pool is empty return static message
            if (Entries != null && Entries.Count == 0)
            {
                tempCurrentNoResultMessage = "Pool is empty";
            }
            else // else get random message
            {
                // Get random message
                tempCurrentNoResultMessage = GetNoSearchResultPhrases(tempRandSearchResult.Next(0, 4));

                // Get new random message if already shown
                while (tempCurrentNoResultMessage == tempPreviousNoResultMessage)
                {
                    tempCurrentNoResultMessage = GetNoSearchResultPhrases(tempRandSearchResult.Next(0, 4));
                }

                // Save new message
                tempPreviousNoResultMessage = tempCurrentNoResultMessage;
            }

            // Return message
            return TextTransform.TextToSlickFrame(tempCurrentNoResultMessage, false);
        }

        // Get random message
        private string GetNoSearchResultPhrases(int random)
        {
            switch (random)
            {
                case 0:
                    return "No match...";
                case 1:
                    return "No results...";
                case 2:
                    return "Nothing found...";
                case 3:
                    return "No match...";
                default:
                    return "No match...";
            }
        }

        private void ResolveLiveSearchMenuItem()
        {
            if (LiveSearch)
            {
                liveSearchToolStripMenuItem.Checked = true;
            }
            else
            {
                liveSearchToolStripMenuItem.Checked = false;
            }
        }

        private void liveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LiveSearch)
            {
                LiveSearch = false;
                Log.GeneralStatus("Live Search disabled");
            }
            else
            {
                LiveSearch = true;
                Log.GeneralStatus("Live Search enabled");
            }
            SettingsHasChanges = true;
            ResolveLiveSearchMenuItem();
        }
        #endregion

        #region Toggle form border (addition 12-06-2019)
        private void windowBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFormBorder();
        }

        private void ResolveWindowBorderMenu()
        {
            if (EnableFormBorder)
            {
                windowBorderToolStripMenuItem.Checked = true;
            }
            else
            {
                windowBorderToolStripMenuItem.Checked = false;
            }
        }

        internal static bool EnableFormBorder { get; set; }
        private void ResolveFormBorder()
        {
            if (EnableFormBorder)
            {
                ShowFormBorder();
            }
            else
            {
                HideFormBorder();
            }
            ResolveWindowBorderMenu();
        }

        private void ToggleFormBorder()
        {
            if (EnableFormBorder)
            {
                EnableFormBorder = false;
                HideFormBorder();
            }
            else
            {
                EnableFormBorder = true;
                ShowFormBorder();
            }

            // Log event
            if (EnableFormBorder)
            {
                Log.GeneralStatus("Window border enabled");
            }
            else
            {
                Log.GeneralStatus("Window border disabled");
            }

            SettingsHasChanges = true;
            ResolveWindowBorderMenu();
        }

        private void HideFormBorder()
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                statusStrip1.SizingGrip = false;
                // Move form
                menuStrip1.MouseDown += MenuStrip_MouseDown;
                menuStrip1.MouseUp += MenuStrip_MouseUp;
                menuStrip1.MouseMove += MenuStrip_MouseMove;
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
                menuStrip1.MouseDown -= MenuStrip_MouseDown;
                menuStrip1.MouseUp -= MenuStrip_MouseUp;
                menuStrip1.MouseMove -= MenuStrip_MouseMove;
            }
            catch
            {
                // Do nothing
            }
        }

        private bool mouseDown;
        private Point lastLocation;
        private void MenuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MenuStrip_MouseMove(object sender, MouseEventArgs e)
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

        private void MenuStrip_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            tbSearch.Focus();
        }
        #endregion

        #region Toggle pinned operations by click (addition 17-06-2019)
        // Toggle New Pinned by Click
        private void newPinnedByClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleNewPinnedByClick();
        }

        private void ToggleNewPinnedByClick()
        {
            if (NewPinnedByClick)
            {
                NewPinnedByClick = false;
                Log.GeneralStatus("Pinned by Click disabled");
                SetStatusText("Pinned by Click " + TextTransform.GetOnOff(NewPinnedByClick));
            }
            else
            {
                NewPinnedByClick = true;
                Log.GeneralStatus("Pinned by Click enabled");
                SetStatusText("Pinned by Click " + TextTransform.GetOnOff(NewPinnedByClick));
            }
            ResolveNewPinnedByClickMenuItem();
            SettingsHasChanges = true;
        }

        private void ResolveNewPinnedByClickMenuItem()
        {
            if (NewPinnedByClick)
            {
                newPinnedByClickToolStripMenuItem.Checked = true;
            }
            else
            {
                newPinnedByClickToolStripMenuItem.Checked = false;
            }
        }

        // Toggle Hide Pinned by Click
        private void hidePinnedByClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleHideByClick();
        }

        private void ToggleHideByClick()
        {
            if (HidePinnedByClick)
            {
                HidePinnedByClick = false;
                Log.GeneralStatus("Hide Pinned by Click disabled");
                SetStatusText("Hide Pinned by Click " + TextTransform.GetOnOff(HidePinnedByClick));
            }
            else
            {
                HidePinnedByClick = true;
                Log.GeneralStatus("Hide Pinned by Click enabled");
                SetStatusText("Hide Pinned by Click " + TextTransform.GetOnOff(HidePinnedByClick));
            }
            ResolveHidePinnedByClickMenuItem();
            SettingsHasChanges = true;
        }

        private void ResolveHidePinnedByClickMenuItem()
        {
            if (HidePinnedByClick)
            {
                hidePinnedByClickToolStripMenuItem.Checked = true;
            }
            else
            {
                hidePinnedByClickToolStripMenuItem.Checked = false;
            }
        }
        #endregion


        #region Mask interface on startup (addition 17-08-2019)
        private void MaskInterface(bool visible)
        {
            this.Visible = visible;
        }
        #endregion


        #region Sort text selection (addition 25-10-2019)

        private void sortAZToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortSelection(false);
        }
        private void sortZAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortSelection(true);
        }
        private void SortSelection(bool direction)
        {
            if (tbContent.SelectedText.Length > 0)
            {
                if (!direction)
                {
                    tbContent.SelectedText = TextTransform.SortText(tbContent.SelectedText, false);
                }
                else if (direction)
                {
                    tbContent.SelectedText = TextTransform.SortText(tbContent.SelectedText, true);
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToPerformTextOperation(), MessageText.TextOperationCaption(), false);
                msg.ShowDialog();
            }
        }
        #endregion

        #region Keyboard Control (25-10-2019)
        private void ToggleKeyboardControl()
        {
            // If one of them is set, set the other
            if (MoveByKeyboard && !ResizeByKeyboard)
            {
                ResizeByKeyboard = MoveByKeyboard; // Set by MoveByKeyboard to streamline
            }
            else if (ResizeByKeyboard && !MoveByKeyboard)
            {
                MoveByKeyboard = ResizeByKeyboard;
            }
            else if (MoveByKeyboard && ResizeByKeyboard) // if both is set disable
            {
                MoveByKeyboard = false;
                ResizeByKeyboard = false;
            }
            else if (!MoveByKeyboard && !ResizeByKeyboard) // if both disabled enable
            {
                MoveByKeyboard = true;
                ResizeByKeyboard = true;
            }

            ResolveKeyboardControlMenuItem();
            SettingsHasChanges = true;
            ResolveKeyboardControlStatusDisplay(3);
        }

        private void ResolveKeyboardControlStatusDisplay(int display)
        {
            if (MoveByKeyboard && (display == 1 || display == 3))
            {
                SetStatusText("Move by Keyboard enabled");
            }
            else if (!MoveByKeyboard && (display == 1 || display == 3))
            {
                SetStatusText("Move by Keyboard disabled");
            }

            if (ResizeByKeyboard && (display == 2 || display == 3))
            {
                SetStatusText("Resize by Keyboard enabled");
            }
            else if (!ResizeByKeyboard && (display == 2 || display == 3))
            {
                SetStatusText("Resize by Keyboard disabled");
            }

        }

        private void EnableKeyboardControl()
        {
            MoveByKeyboard = true;
            ResizeByKeyboard = true;
            ResolveKeyboardControlMenuItem();
            SettingsHasChanges = true;
            ResolveKeyboardControlStatusDisplay(3);
        }

        private void ResolveKeyboardControlMenuItem()
        {
            moveByKeyboardToolStripMenuItem.Checked = MoveByKeyboard;
            resizeByKeyboardToolStripMenuItem.Checked = ResizeByKeyboard;
        }

        private void moveByKeyboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveByKeyboard = !MoveByKeyboard;
            SettingsHasChanges = true;
            ResolveKeyboardControlMenuItem();
            if (MoveByKeyboard)
            {
                // Show dialog with keyboard shortcuts
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Move by Keyboard:\n\nALT+SHIFT+Arrow keys\n(Enable CAPSLOCK to move with greater intervals)", "Keyboard Control", false);
                msg.ShowDialog();
            }
            ResolveKeyboardControlStatusDisplay(1);
        }

        private void resizeByKeyboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResizeByKeyboard = !ResizeByKeyboard;
            SettingsHasChanges = true;
            ResolveKeyboardControlMenuItem();
            if (ResizeByKeyboard)
            {
                // Show dialog with keyboard shortcuts
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Resize by Keyboard:\n\nCTRL+ALT+SHIFT+Arrow keys\n(Enable CAPSLOCK to resize with greater intervals)", "Keyboard Control", false);
                msg.ShowDialog();
            }
            ResolveKeyboardControlStatusDisplay(2);
        }
        #endregion


        #region Tab memory (addition 26-10-2019)
        private void InitializeTabMemory() // Called from the constructor
        {
            TabMemoryA = "";
            TabMemoryB = "";
        }

        private void UpdateTabMemory() // Called from the SelectEntry and SelectEntryByID methods
        {
            if (CurrentEntryNotNullOrDummy())
            {
                // Check that entry is not registered in any bank
                if (CurrentEntry.ID != TabMemoryA && CurrentEntry.ID != TabMemoryB)
                {
                    // Move Bank A to Bank B, and register the new entry in bank A
                    TabMemoryB = TabMemoryA;
                    TabMemoryA = CurrentEntry.ID;
                }
            }
        }
        private void SelectByTabMemory() // Called from jumpBetweenHeldEntriesToolStripMenuItem_Click
        {
            // Check that no entries are held by both hold buttons A and B
            if (HoldEntryA && HoldEntryB)
            {
                return;
            }
            else
            {
                // Check that entry exists in both tab memories and select one that does not match the current entry
                if (!string.IsNullOrEmpty(TabMemoryA) && !string.IsNullOrEmpty(TabMemoryB))
                {
                    if (EntryExist(TabMemoryA) && CurrentEntry.ID != TabMemoryA)
                    {
                        SelectWithCancel(TabMemoryA, true, true);
                        // Only select entry if entry selection was not cancelled within the SelectWithCancel method
                        if (CurrentEntryNotNullOrDummy() && CurrentEntry.ID == TabMemoryA)
                        {
                            CheckListsAndHighlight(TabMemoryA);
                        }
                    }
                    else if (EntryExist(TabMemoryB) && CurrentEntry.ID != TabMemoryB)
                    {
                        SelectWithCancel(TabMemoryB, true, true);
                        // Only select entry if entry selection was not cancelled within the SelectWithCancel method
                        if (CurrentEntryNotNullOrDummy() && CurrentEntry.ID == TabMemoryB)
                        {
                            CheckListsAndHighlight(TabMemoryB);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(TabMemoryA)) // Else select if entry A exists and not already selected
                {
                    if (EntryExist(TabMemoryA) && CurrentEntry.ID != TabMemoryA)
                    {
                        SelectWithCancel(TabMemoryA, true, true);
                        // Only select entry if entry selection was not cancelled within the SelectWithCancel method
                        if (CurrentEntryNotNullOrDummy() && CurrentEntry.ID == TabMemoryA)
                        {
                            CheckListsAndHighlight(TabMemoryA);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(TabMemoryB)) // Else select if entry A exists and not already selected
                {
                    if (EntryExist(TabMemoryB) && CurrentEntry.ID != TabMemoryB)
                    {
                        SelectWithCancel(TabMemoryB, true, true);
                        // Only select entry if entry selection was not cancelled within the SelectWithCancel method
                        if (CurrentEntryNotNullOrDummy() && CurrentEntry.ID == TabMemoryB)
                        {
                            CheckListsAndHighlight(TabMemoryB);
                        }
                    }
                }
            }
        }

        #endregion

        #region Pool cleanup (addition 29-02-2020)
        private void PoolCleanup()
        {
            if (EntriesExist())
            {
                SaveBackupForUndo();
                SetDefaultStatusText();
                ResetSearchResultSelectionIndex();
                ClearDisplays(false);
                ShowPinned();
                Halt();
                UtilitySearchCollection = new List<Entry>();
                // Collect unused entries
                foreach (Entry entry in Entries)
                {
                    // If title and content is empty or default content
                    if (((entry.Title.Trim().ToLower() == NewEntryCaption.ToLower()) || entry.Title.Trim().Length == 0) &&
                        (entry.Content.Trim().ToLower() == NewEntryCaption.ToLower() || entry.Content.Trim().Length == 0))
                    {
                        UtilitySearchCollection.Add(entry);
                    }
                }

                // Continue if entries exist
                if (UtilitySearchCollection != null && UtilitySearchCollection.Count > 0)
                {
                    Log.LogSearch("(Find unused entries)", false, false);
                    listBoxSearchResult.Items.Clear();
                    // Sort list to display the latest edited first
                    try
                    {
                        UtilitySearchCollection.Sort(delegate (Entry x, Entry y) { return DateTime.Compare(Convert.ToDateTime(y.Changed), Convert.ToDateTime(x.Changed)); });
                    }
                    catch
                    {
                        // Log sorting error due to DateTime issues
                        Log.LogGeneralError("Sorting list failed");
                    }
                    PopulateSearchListBox(listBoxSearchResult, UtilitySearchCollection);

                    string tempUtil = "";
                    string tempInfo = "\nEntries without title, content and notification text (PLOP/UE is not affected).";
                    int tempCounter = 0;
                    if (UtilitySearchCollection.Count == 1)
                    {
                        tempUtil = "entry?";
                    }
                    else
                    {
                        tempUtil = "entries?";
                    }

                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Delete " + UtilitySearchCollection.Count + " unused " + tempUtil + tempInfo, MessageText.DeleteCaption(), false, true);
                    msg.ShowDialog();

                    if (msg.Accepted)
                    {
                        if (UtilitySearchCollection.Count == 1)
                        {
                            tempUtil = "entry";
                        }
                        else
                        {
                            tempUtil = "entries";
                        }

                        Log.LogPoolStatus("Deleting " + UtilitySearchCollection.Count + " " + tempUtil);
                        try
                        {
                            foreach (Entry e in UtilitySearchCollection)
                            {
                                for (int i = 0; i < Entries.Count; i++)
                                {
                                    if (Entries[i].ID == e.ID)
                                    {
                                        Entries.RemoveAt(i);
                                        tempCounter += 1;
                                    }
                                }
                            }

                            if (tempCounter == 1)
                            {
                                tempUtil = "entry";
                            }
                            else
                            {
                                tempUtil = "entries";
                            }

                            AhoySimpleMessageBox msgDeleted = new AhoySimpleMessageBox(tempCounter.ToString() + " " + tempUtil + " deleted", MessageText.DeleteCaption(), false);
                            msgDeleted.ShowDialog();
                        }
                        catch
                        {
                            // Do nothing
                        }

                        ClearSearchResults();
                        SetPoolHasChanges();
                        PopulatePinList();
                        RefreshTitleColor();
                    }
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.EmptySearchMessage(), MessageText.SearchCaption(), false);
                    msg.ShowDialog();
                }
                Resume();
            }
        }
        #endregion



        #region Related entries (addition 12-03-2020)
        ToolStripMenuItem newRelatedMenuItemPinnedTitle;
        ToolStripMenuItem newRelatedMenuItemSearchTitle;
        ToolStripMenuItem newRelatedMenuItemPinned;
        ToolStripMenuItem newRelatedMenuItemSearch;
        ToolStripMenuItem newRelatedMenuItemClearRelatedPinned;
        ToolStripMenuItem newRelatedMenuItemClearRelatedSearch;
        ToolStripMenuItem newRelatedMenuItemShowPinned;
        ToolStripMenuItem newRelatedMenuItemShowSearch;
        ToolStripMenuItem newRelatedMenuItemPinSearch;
        ToolStripMenuItem newRelatedMenuItemOptionsPinned;

        ToolStripMenuItem newRelatedMenuItemOptionGradient;
        ToolStripMenuItem newRelatedMenuItemOption1;
        ToolStripMenuItem newRelatedMenuItemOption2;
        ToolStripMenuItem newRelatedMenuItemOption3;
        ToolStripMenuItem newRelatedMenuItemOption4;
        ToolStripMenuItem newRelatedMenuItemOption5;
        ToolStripMenuItem newRelatedMenuItemOption6;
        ToolStripMenuItem newRelatedMenuItemOption7;
        ToolStripMenuItem newRelatedMenuItemOption8;

        ToolStripMenuItem newRelatedMenuItemShowAllPinned;
        ToolStripMenuItem newRelatedMenuItemShowAllSearch;
        ToolStripMenuItem newRelatedMenuItemShowRelatedPinned;
        ToolStripMenuItem newRelatedMenuItemShowRelatedSearch;

        ToolStripSeparator newRelatedSeparatorPinned;
        ToolStripSeparator newRelatedSeparatorOptionsPinned;
        ToolStripSeparator newRelatedSeparatorSearch;
        /// <summary>
        /// Initialize related menu items
        /// </summary>
        private void InitializedRelatedContextMenuItems()
        {
            // Titles
            newRelatedMenuItemPinnedTitle = new ToolStripMenuItem();
            newRelatedMenuItemPinnedTitle.Name = rel6;
            newRelatedMenuItemPinnedTitle.Text = "Category Title";
            newRelatedMenuItemPinnedTitle.Tag = "";
            newRelatedMenuItemPinnedTitle.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemPinnedTitle.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemSearchTitle = new ToolStripMenuItem();
            newRelatedMenuItemSearchTitle.Name = rel6;
            newRelatedMenuItemSearchTitle.Text = "Category Title";
            newRelatedMenuItemSearchTitle.Tag = "";
            newRelatedMenuItemSearchTitle.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemSearchTitle.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            // Menuitems
            newRelatedMenuItemPinned = new ToolStripMenuItem();
            newRelatedMenuItemPinned.Name = rel0;
            newRelatedMenuItemPinned.Text = "Category Pinned";
            newRelatedMenuItemPinned.Tag = "";
            newRelatedMenuItemPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemSearch = new ToolStripMenuItem();
            newRelatedMenuItemSearch.Name = rel0;
            newRelatedMenuItemSearch.Text = "Category Search";
            newRelatedMenuItemSearch.Tag = "";
            newRelatedMenuItemSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowPinned = new ToolStripMenuItem();
            newRelatedMenuItemShowPinned.Name = rel0;
            newRelatedMenuItemShowPinned.Text = "Show";
            newRelatedMenuItemShowPinned.Tag = "";
            newRelatedMenuItemShowPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowSearch = new ToolStripMenuItem();
            newRelatedMenuItemShowSearch.Name = rel0;
            newRelatedMenuItemShowSearch.Text = "Show";
            newRelatedMenuItemShowSearch.Tag = "";
            newRelatedMenuItemShowSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemPinSearch = new ToolStripMenuItem();
            newRelatedMenuItemPinSearch.Name = rel10;
            newRelatedMenuItemPinSearch.Text = "Pin Category";
            newRelatedMenuItemPinSearch.Tag = "";
            newRelatedMenuItemPinSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemPinSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOptionsPinned = new ToolStripMenuItem();
            newRelatedMenuItemOptionsPinned.Name = rel0;
            newRelatedMenuItemOptionsPinned.Text = "Options";
            newRelatedMenuItemOptionsPinned.Tag = "high";
            newRelatedMenuItemOptionsPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOptionsPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);


            // Nested menuitems
            newRelatedMenuItemClearRelatedPinned = new ToolStripMenuItem();
            newRelatedMenuItemClearRelatedPinned.Name = rel7;
            newRelatedMenuItemClearRelatedPinned.Text = "Clear Category";
            newRelatedMenuItemClearRelatedPinned.Tag = "";
            newRelatedMenuItemClearRelatedPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemClearRelatedPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemClearRelatedSearch = new ToolStripMenuItem();
            newRelatedMenuItemClearRelatedSearch.Name = rel7;
            newRelatedMenuItemClearRelatedSearch.Text = "Clear Category";
            newRelatedMenuItemClearRelatedSearch.Tag = "";
            newRelatedMenuItemClearRelatedSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemClearRelatedSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowAllPinned = new ToolStripMenuItem();
            newRelatedMenuItemShowAllPinned.Name = rel8;
            newRelatedMenuItemShowAllPinned.Text = "All categories";
            newRelatedMenuItemShowAllPinned.Tag = "";
            newRelatedMenuItemShowAllPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowAllPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowAllSearch = new ToolStripMenuItem();
            newRelatedMenuItemShowAllSearch.Name = rel8;
            newRelatedMenuItemShowAllSearch.Text = "All categories";
            newRelatedMenuItemShowAllSearch.Tag = "";
            newRelatedMenuItemShowAllSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowAllSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowRelatedPinned = new ToolStripMenuItem();
            newRelatedMenuItemShowRelatedPinned.Name = rel9;
            newRelatedMenuItemShowRelatedPinned.Text = "Related categories";
            newRelatedMenuItemShowRelatedPinned.Tag = "";
            newRelatedMenuItemShowRelatedPinned.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowRelatedPinned.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemShowRelatedSearch = new ToolStripMenuItem();
            newRelatedMenuItemShowRelatedSearch.Name = rel9;
            newRelatedMenuItemShowRelatedSearch.Text = "Related categories";
            newRelatedMenuItemShowRelatedSearch.Tag = "";
            newRelatedMenuItemShowRelatedSearch.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemShowRelatedSearch.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            // Category styles
            newRelatedMenuItemOptionGradient = new ToolStripMenuItem();
            newRelatedMenuItemOptionGradient.Name = rel19;
            newRelatedMenuItemOptionGradient.Text = "Color Gradient";
            newRelatedMenuItemOptionGradient.Tag = "";
            newRelatedMenuItemOptionGradient.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOptionGradient.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption1 = new ToolStripMenuItem();
            newRelatedMenuItemOption1.Name = rel11;
            newRelatedMenuItemOption1.Text = "Style 1 (Default)";
            newRelatedMenuItemOption1.Tag = "";
            newRelatedMenuItemOption1.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption1.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption2 = new ToolStripMenuItem();
            newRelatedMenuItemOption2.Name = rel12;
            newRelatedMenuItemOption2.Text = "Style 2";
            newRelatedMenuItemOption2.Tag = "";
            newRelatedMenuItemOption2.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption2.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption3 = new ToolStripMenuItem();
            newRelatedMenuItemOption3.Name = rel13;
            newRelatedMenuItemOption3.Text = "Style 3";
            newRelatedMenuItemOption3.Tag = "";
            newRelatedMenuItemOption3.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption3.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption4 = new ToolStripMenuItem();
            newRelatedMenuItemOption4.Name = rel14;
            newRelatedMenuItemOption4.Text = "Style 4";
            newRelatedMenuItemOption4.Tag = "";
            newRelatedMenuItemOption4.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption4.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption5 = new ToolStripMenuItem();
            newRelatedMenuItemOption5.Name = rel15;
            newRelatedMenuItemOption5.Text = "Style 5";
            newRelatedMenuItemOption5.Tag = "";
            newRelatedMenuItemOption5.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption5.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption6 = new ToolStripMenuItem();
            newRelatedMenuItemOption6.Name = rel16;
            newRelatedMenuItemOption6.Text = "Style 6";
            newRelatedMenuItemOption6.Tag = "";
            newRelatedMenuItemOption6.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption6.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption7 = new ToolStripMenuItem();
            newRelatedMenuItemOption7.Name = rel17;
            newRelatedMenuItemOption7.Text = "Style 7";
            newRelatedMenuItemOption7.Tag = "";
            newRelatedMenuItemOption7.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption7.Click += new System.EventHandler(relatedMenuItem_ItemClicked);

            newRelatedMenuItemOption8 = new ToolStripMenuItem();
            newRelatedMenuItemOption8.Name = rel18;
            newRelatedMenuItemOption8.Text = "Style 8";
            newRelatedMenuItemOption8.Tag = "";
            newRelatedMenuItemOption8.Size = new System.Drawing.Size(251, 22);
            newRelatedMenuItemOption8.Click += new System.EventHandler(relatedMenuItem_ItemClicked);


            // Separators
            newRelatedSeparatorPinned = new ToolStripSeparator();
            newRelatedSeparatorPinned.Name = "relatedMenuItemDividerPinned";
            newRelatedSeparatorPinned.Size = new System.Drawing.Size(248, 6);
            newRelatedSeparatorOptionsPinned = new ToolStripSeparator();
            newRelatedSeparatorOptionsPinned.Name = "relatedMenuItemDividerOptionsPinned";
            newRelatedSeparatorOptionsPinned.Size = new System.Drawing.Size(248, 6);
            newRelatedSeparatorSearch = new ToolStripSeparator();
            newRelatedSeparatorSearch.Name = "relatedMenuItemDividerSearch";
            newRelatedSeparatorSearch.Size = new System.Drawing.Size(248, 6);

            // Add items to pinned context
            contextMenuPinned.Items.Insert(0, newRelatedMenuItemPinnedTitle);
            contextMenuPinned.Items.Insert(1, newRelatedMenuItemPinned);
            contextMenuPinned.Items.Insert(2, newRelatedMenuItemShowPinned);
            contextMenuPinned.Items.Insert(3, newRelatedMenuItemOptionsPinned);
            // Insert separators to Pinned context
            contextMenuPinned.Items.Insert(4, newRelatedSeparatorPinned);
            contextMenuPinned.Opening += ContextMenuPinned_Opening;

            // Add items to search context
            contextMenuSearchResult.Items.Insert(0, newRelatedMenuItemSearchTitle);
            contextMenuSearchResult.Items.Insert(1, newRelatedMenuItemSearch);
            contextMenuSearchResult.Items.Insert(2, newRelatedMenuItemPinSearch);
            contextMenuSearchResult.Items.Insert(3, newRelatedMenuItemShowSearch);
            // Insert separators to Search context
            contextMenuSearchResult.Items.Insert(4, newRelatedSeparatorSearch);
            contextMenuSearchResult.Opening += ContextMenuSearch_Opening;

            // Add nested menu items
            newRelatedMenuItemPinnedTitle.DropDownItems.Add(newRelatedMenuItemClearRelatedPinned);
            newRelatedMenuItemSearchTitle.DropDownItems.Add(newRelatedMenuItemClearRelatedSearch);
            newRelatedMenuItemShowPinned.DropDownItems.Add(newRelatedMenuItemShowAllPinned);
            newRelatedMenuItemShowSearch.DropDownItems.Add(newRelatedMenuItemShowAllSearch);
            newRelatedMenuItemShowPinned.DropDownItems.Add(newRelatedMenuItemShowRelatedPinned);
            newRelatedMenuItemShowSearch.DropDownItems.Add(newRelatedMenuItemShowRelatedSearch);

            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOptionGradient);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedSeparatorOptionsPinned);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption1);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption2);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption3);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption4);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption5);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption6);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption7);
            newRelatedMenuItemOptionsPinned.DropDownItems.Add(newRelatedMenuItemOption8);
        }

        // Refresh the itemems when opening the context menus

        private int tempMenuSelected;
        private void ContextMenuPinned_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tempMenuSelected = 0;
            ResolveRelatedMenuitems();
        }

        private void ContextMenuSearch_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tempMenuSelected = 1;
            ResolveRelatedMenuitems();
        }

        // Action names
        private const string rel0 = "rel0", rel1 = "rel1", rel2 = "rel2", rel3 = "rel3",
            rel4 = "rel4", rel5 = "rel5", rel6 = "rel6", rel7 = "rel7", rel8 = "rel8", rel9 = "rel9", rel10 = "rel10",
            rel11 = "rel11", rel12 = "rel12", rel13 = "rel13", rel14 = "rel14", rel15 = "rel15", rel16 = "rel16",
            rel17 = "rel17", rel18 = "rel18", rel19 = "rel19";

        // Perform actions based on context menu selections in both pinned and search list
        private void relatedMenuItem_ItemClicked(object sender, EventArgs e)
        {
            // rel0 is neutral with no actions

            // Take action according to action name
            switch (((ToolStripMenuItem)sender).Name)
            {
                case rel1:
                    RelatedMark(false); // Mark selected for editing (as master)
                    try
                    {
                        if (tempMenuSelected == 0) // Open menu again when setting new mark
                        {
                            contextMenuPinned.Show();
                        }
                        else if (tempMenuSelected == 1)
                        {
                            contextMenuSearchResult.Show();
                        }
                    }
                    catch
                    {
                        // Do nothing
                    }
                    break;
                case rel2:
                    RelatedAdd(false); // Add selected to editing master
                    RefreshSearchResultAfterRelatedAdd(); //  Update search if master is unpinned
                    break;
                case rel3:
                    RelatedRemove(); // Remove selected from master
                    if (tempMenuSelected == 1) //  Refresh current result if called by the search menu
                    {
                        RefreshSearchResultAfterRelatedRemove();
                    }
                    break;
                case rel4:
                    Related.ClearMark(); // Stop editing
                    break;
                case rel5:
                    Related.ClearAllRelated(CurrentEntry, Entries); // Clear all relations on all masters
                    break;
                case rel6:
                    RelatedSelectMasterFromSlaveUnlessEditing(); // Select master related to selected unless editing another master
                    CheckRelatedAndPostInSearch();
                    break;
                case rel7:
                    ClearRelatedFromMaster();
                    if (tempMenuSelected == 1) //  Refresh current result if called by the search menu
                    {
                        RefreshSearchResultAfterMasterReset();
                    }
                    break;
                case rel8:
                    Log.LogPoolStatus("Get all relations");
                    RelatedFindAll();
                    break;
                case rel9:
                    Log.GeneralEntryStatus("Get relations");
                    RelatedFindAllMastersRelatedToSlave();
                    break;
                case rel10:
                    // Pin category or categories if not pinned
                    if (Related.IsRelated(CurrentEntry, Entries))
                    {
                        Related.PinAllMastersRelatedToSlave(CurrentEntry, Entries);
                    }
                    else if (Related.HasRelated(CurrentEntry) && !CurrentEntry.Pin)
                    {
                        cbPinned.Checked = true;
                        ResolvePinnedMenuItem(); // Update menu
                        RefreshSearchResultAfterMasterReset();
                        CurrentEntryHasChanges(); // Save entry
                        InvokeAutoSaveEntry();
                    }
                    PopulatePinListAndReselect();
                    break;
                case rel11:
                    SetCategoryStyle(0);
                    break;
                case rel12:
                    SetCategoryStyle(1);
                    break;
                case rel13:
                    SetCategoryStyle(2);
                    break;
                case rel14:
                    SetCategoryStyle(3);
                    break;
                case rel15:
                    SetCategoryStyle(4);
                    break;
                case rel16:
                    SetCategoryStyle(5);
                    break;
                case rel17:
                    SetCategoryStyle(6);
                    break;
                case rel18:
                    SetCategoryStyle(7);
                    break;
                case rel19:
                    SetRelatedGradient(true);
                    break;
            }
        }

        private void RefreshSearchResultAfterRelatedRemove()
        {
            if (!string.IsNullOrEmpty(Related.MasterFromSlaveID))
            {
                if (EntryExist(Related.MasterFromSlaveID))
                {
                    SelectEntryByID(Related.MasterFromSlaveID, false, false);

                    if (Related.HasRelated(CurrentEntry))
                    {
                        CheckRelatedAndPostInSearch();
                    }
                    else
                    {
                        PostEntryInSearchResult(CurrentEntry); // if no related left, just post the entry 
                    }
                    Related.MasterFromSlaveID = string.Empty;
                }
            }
        }

        private void RefreshSearchResultAfterMasterReset()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                PostEntryInSearchResult(CurrentEntry); // if no related left, just post the entry 
            }
        }



        private void RefreshSearchResultAfterRelatedAdd()
        {
            if (Related.CanMark())
            {
                if (EntryExist(Related.GetMark()))
                {
                    SelectEntryByID(Related.GetMark(), false, false);
                    if (IsCurrentMasterAndNotPinned())
                    {
                        CheckRelatedAndPostInSearch();
                        PopulatePinListAndReselect();
                    }
                    else
                    {
                        PopulatePinListAndReselect();
                    }
                }
            }
        }

        private void ResolveRelatedOptionsMenuItems(Entry entry)
        {
            newRelatedMenuItemOptionsPinned.Visible = false;
            newRelatedMenuItemOption1.Checked = false;
            newRelatedMenuItemOption2.Checked = false;
            newRelatedMenuItemOption3.Checked = false;
            newRelatedMenuItemOption4.Checked = false;
            newRelatedMenuItemOption5.Checked = false;
            newRelatedMenuItemOption6.Checked = false;
            newRelatedMenuItemOption7.Checked = false;
            newRelatedMenuItemOption8.Checked = false;

            if (entry != null && Related.HasRelated(entry))
            {
                newRelatedMenuItemOptionsPinned.Visible = true;
                // Update options
                switch (CurrentEntry.RelatedStyle)
                {
                    case 0:
                        newRelatedMenuItemOption1.Checked = true;
                        break;
                    case 1:
                        newRelatedMenuItemOption2.Checked = true;
                        break;
                    case 2:
                        newRelatedMenuItemOption3.Checked = true;
                        break;
                    case 3:
                        newRelatedMenuItemOption4.Checked = true;
                        break;
                    case 4:
                        newRelatedMenuItemOption5.Checked = true;
                        break;
                    case 5:
                        newRelatedMenuItemOption6.Checked = true;
                        break;
                    case 6:
                        newRelatedMenuItemOption7.Checked = true;
                        break;
                    case 7:
                        newRelatedMenuItemOption8.Checked = true;
                        break;
                }
            }
        }

        // Default empty text
        private const string DefaultNoRelatedAction = "Category Unavailable";

        // Update related menu items
        private void ResolveRelatedMenuitems()
        {
            // Set category TITLE fields
            newRelatedMenuItemPinnedTitle.Visible = false; // Must be set separately for each iten
            newRelatedMenuItemPinnedTitle.Text = "";
            newRelatedMenuItemPinnedTitle.Tag = "29";
            newRelatedMenuItemSearchTitle.Visible = false; // Must be set separately for each iten
            newRelatedMenuItemSearchTitle.Tag = "29";
            newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;

            // Find all items
            newRelatedMenuItemShowPinned.Visible = false;
            newRelatedMenuItemShowSearch.Visible = false;

            // Pin masters
            newRelatedMenuItemPinSearch.Visible = false;

            // Set NESTED
            newRelatedMenuItemClearRelatedPinned.Visible = false;
            newRelatedMenuItemClearRelatedSearch.Visible = false;

            // Set default values for PINNED click item
            newRelatedMenuItemPinned.Enabled = false;
            newRelatedMenuItemPinned.Text = DefaultNoRelatedAction;
            newRelatedMenuItemPinned.Tag = "";
            newRelatedMenuItemPinned.Name = rel0;
            // Set default values for SEARCH click item based on PINNED
            newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
            newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
            newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
            newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;
            // Options
            newRelatedMenuItemOptionsPinned.Visible = false;


            // Show find all categories if pool has any relations
            if (Related.HasRelated(Entries))
            {
                newRelatedMenuItemShowPinned.Visible = true;
                newRelatedMenuItemShowSearch.Visible = true;

                // Check if current selected entry has more than one master and highlight menu item
                if (Related.IsRelated(CurrentEntry, Entries) && Related.GetMasterRelationsCount(CurrentEntry, Entries) > 1)
                {
                    newRelatedMenuItemShowRelatedPinned.Visible = true;
                    newRelatedMenuItemShowRelatedSearch.Visible = true;
                    newRelatedMenuItemShowRelatedPinned.Tag = "high";
                    newRelatedMenuItemShowRelatedSearch.Tag = "high";
                }
                else
                {
                    newRelatedMenuItemShowRelatedPinned.Visible = false;
                    newRelatedMenuItemShowRelatedSearch.Visible = false;
                    newRelatedMenuItemShowRelatedPinned.Tag = "";
                    newRelatedMenuItemShowRelatedSearch.Tag = "";
                }
            }

            // Show Pin Category in search list if master is unpinned or selected slave has an unpinned master
            if (IsCurrentSlaveWithUnpinnedMastersOrMasterUnpinned())
            {
                newRelatedMenuItemPinSearch.Visible = true;
                if (Related.GetMasterRelationsCount(CurrentEntry, Entries) > 1 && !Related.IsAllMastersPinned(CurrentEntry, Entries))
                {
                    newRelatedMenuItemPinSearch.Text = "Pin related Categories";
                }
                else
                {
                    newRelatedMenuItemPinSearch.Text = "Pin Category";
                }
            }

            // Resolve options menu
            ResolveRelatedOptionsMenuItems(CurrentEntry);


            // Resolve menu items
            if (CurrentEntryNotNullOrDummy())
            {
                // Current entry is marked as editing master
                if (Related.CanMark() && CurrentEntry.ID == Related.GetMark())
                {
                    // Set master tilte
                    newRelatedMenuItemPinnedTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemPinnedTitle.Text = "Adding to " + TextTransform.FormatTitleForCategoryPreview(CurrentEntry.Title);
                    newRelatedMenuItemPinnedTitle.Tag = "17";
                    newRelatedMenuItemSearchTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;
                    newRelatedMenuItemSearchTitle.Tag = "17";
                    // Set menu options
                    newRelatedMenuItemPinned.Enabled = true;
                    newRelatedMenuItemPinned.Text = "Stop Editing";
                    newRelatedMenuItemPinned.Tag = "high"; // Highligt color
                    newRelatedMenuItemPinned.Name = rel4;
                    // Mirror options in search menu
                    newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
                    newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
                    newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
                    newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;
                    // Set nested
                    newRelatedMenuItemClearRelatedPinned.Visible = true;
                    newRelatedMenuItemClearRelatedSearch.Visible = true;
                }

                // Current entry is not master, not related and there is no marsters marked
                else if (!Related.CanMark() && !Related.HasRelated(CurrentEntry) && !Related.IsRelated(CurrentEntry, Entries))
                {
                    // Set master tilte
                    newRelatedMenuItemPinnedTitle.Visible = false; // Must be set separately for each iten
                    newRelatedMenuItemPinnedTitle.Text = "";
                    newRelatedMenuItemSearchTitle.Visible = false; // Must be set separately for each iten
                    newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;
                    // Set menu options
                    newRelatedMenuItemPinned.Enabled = true;
                    newRelatedMenuItemPinned.Text = "New Category";
                    newRelatedMenuItemPinned.Tag = "bold";
                    newRelatedMenuItemPinned.Name = rel1;
                    // Mirror options in search menu
                    newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
                    newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
                    newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
                    newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;

                } // Current entry is master but not currently editing
                else if (CurrentEntry.ID != Related.GetMark() && Related.HasRelated(CurrentEntry) && !Related.IsRelated(CurrentEntry, Entries))
                {
                    // Set master tilte
                    newRelatedMenuItemPinnedTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemPinnedTitle.Text = "Category: " + TextTransform.FormatTitleForCategoryPreview(CurrentEntry.Title);
                    newRelatedMenuItemSearchTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;
                    // Set menu options
                    newRelatedMenuItemPinned.Enabled = true;
                    newRelatedMenuItemPinned.Text = "Edit Category";
                    newRelatedMenuItemPinned.Tag = "bold"; // Bold font
                    newRelatedMenuItemPinned.Name = rel1;
                    // Mirror options in search menu
                    newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
                    newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
                    newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
                    newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;
                }

                // SLAVES
                // Mark is open and entry is not master and not already added to the current master (Can be added to several masters)
                else if (Related.CanMark() && !Related.HasRelated(CurrentEntry) && !Related.HasRelatedMastersMatchingCurrentMark(CurrentEntry, Entries))
                {
                    // Set master tilte
                    newRelatedMenuItemPinnedTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemPinnedTitle.Text = GetOtherMasterTitle();
                    newRelatedMenuItemSearchTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;
                    // Set menu options
                    newRelatedMenuItemPinned.Enabled = true;
                    newRelatedMenuItemPinned.Text = "Add to Category";
                    newRelatedMenuItemPinned.Tag = "high";
                    newRelatedMenuItemPinned.Name = rel2;
                    // Mirror options in search menu
                    newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
                    newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
                    newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
                    newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;

                } // If current entry is related
                else if (Related.IsRelated(CurrentEntry, Entries)) // Related.ContainsMatch(Related.GetMasterFromSlave(CurrentEntry, Entries), CurrentEntry)
                {
                    // Set master tilte
                    newRelatedMenuItemPinnedTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemPinnedTitle.Text = GetMasterTitleFromSlave();
                    newRelatedMenuItemSearchTitle.Visible = true; // Must be set separately for each iten
                    newRelatedMenuItemSearchTitle.Text = newRelatedMenuItemPinnedTitle.Text;
                    // Set menu options
                    newRelatedMenuItemPinned.Enabled = true;
                    newRelatedMenuItemPinned.Text = "Remove from Category";
                    newRelatedMenuItemPinned.Tag = "high";
                    newRelatedMenuItemPinned.Name = rel3;
                    // Mirror options in search menu
                    newRelatedMenuItemSearch.Enabled = newRelatedMenuItemPinned.Enabled;
                    newRelatedMenuItemSearch.Text = newRelatedMenuItemPinned.Text;
                    newRelatedMenuItemSearch.Tag = newRelatedMenuItemPinned.Tag;
                    newRelatedMenuItemSearch.Name = newRelatedMenuItemPinned.Name;
                }
            }
        }



        // Get master titles:
        private string GetMasterTitleFromSlave()
        {
            if (CurrentEntryNotNullOrDummy() && Related.IsRelated(CurrentEntry, Entries))
            {
                // If a master is marked and master to the current slave -set this master as the one it belongs to
                int tempCountRelated = Related.GetMasterRelationsCount(CurrentEntry, Entries);
                string tempRelatedText = "";
                if (tempCountRelated > 1)
                {
                    if (Related.HasRelatedMastersMatchingCurrentMark(CurrentEntry, Entries))
                    {
                        tempRelatedText = TextTransform.FormatTitleForCategoryPreview(GetEntryByID(Related.GetMark()).Title);
                    }
                    else
                    {
                        tempRelatedText = TextTransform.FormatTitleForCategoryPreview(Related.GetMasterFromSlave(CurrentEntry, Entries).Title);
                    }

                    // Add count
                    tempRelatedText += " +" + (tempCountRelated - 1).ToString();
                }
                else
                {
                    tempRelatedText = TextTransform.FormatTitleForCategoryPreview(Related.GetMasterFromSlave(CurrentEntry, Entries).Title);
                }
                return "Belongs to " + tempRelatedText;
            }
            else
            {
                return DefaultNoRelatedAction;
            }
        }

        private string GetOtherMasterTitle()
        {
            if (Related.CanMark() && EntryExist(Related.GetMark()))
            {
                return "Editing: " + TextTransform.FormatTitleForCategoryPreview(GetEntryByID(Related.GetMark()).Title);
            }
            else
            {
                return DefaultNoRelatedAction;
            }
        }

        private void RelatedFindAll()
        {
            if (Related.HasRelated(Entries))
            {
                // List current entry related entries in search result
                ResetSearchResultSelectionIndex();
                if (tempRelatedSearchCollection == null)
                {
                    tempRelatedSearchCollection = new List<Entry>();
                }
                tempRelatedSearchCollection.Clear();
                tempRelatedSearchCollection.AddRange(Related.GetMasterCollectionAsClone(Entries));
                // Sort A-Z by title
                try
                {
                    tempRelatedSearchCollection.Sort(delegate (Entry x, Entry y) { return String.Compare(x.Title, y.Title); });
                }
                catch
                {
                    // Log sorting error
                    Log.LogGeneralError("Sorting list failed (Related)");
                }

                // Clear control and add collected items
                listBoxSearchResult.Items.Clear();
                PopulateSearchListBox(listBoxSearchResult, tempRelatedSearchCollection);
                tbSearch.Clear();
                // Cleanup
                tempRelatedSearchCollection.Clear();
            }
        }

        private void RelatedFindAllMastersRelatedToSlave()
        {
            if (CurrentEntryNotNullOrDummy() && Related.IsRelated(CurrentEntry, Entries))
            {
                // List current entry related entries in search result
                ResetSearchResultSelectionIndex();
                if (tempRelatedSearchCollection == null)
                {
                    tempRelatedSearchCollection = new List<Entry>();
                }
                tempRelatedSearchCollection.Clear();
                tempRelatedSearchCollection.AddRange(Related.GetAllMastersRelatedToSlave(CurrentEntry, Entries));
                // Sort A-Z by title
                try
                {
                    tempRelatedSearchCollection.Sort(delegate (Entry x, Entry y) { return String.Compare(x.Title, y.Title); });
                }
                catch
                {
                    // Log sorting error
                    Log.LogGeneralError("Sorting list failed (Related)");
                }

                // Clear control and add collected items
                listBoxSearchResult.Items.Clear();
                PopulateSearchListBox(listBoxSearchResult, tempRelatedSearchCollection);
                tbSearch.Clear();
                // Cleanup
                tempRelatedSearchCollection.Clear();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No related categories", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
        }

        private void RelatedStatus()
        {
            if (CurrentEntryNotNullOrDummy())
            {
                string tempOutput = "";
                int tempCountByEntries = 0;
                int tempCountBySplit = 0;

                if (Related.HasRelated(CurrentEntry))
                {
                    tempCountByEntries = Related.GetRelatedAsClone(CurrentEntry, Entries).Count;
                    tempCountBySplit = Related.CountRelated(CurrentEntry);

                    // Compare count
                    if (tempCountByEntries != tempCountBySplit)
                    {
                        Log.LogGeneralError("Discrepancy detected (Categories + " + tempCountByEntries.ToString() + "/" + tempCountBySplit.ToString() + ")");
                        AhoySimpleMessageBox msgDiscrepancy = new AhoySimpleMessageBox("Discrepancy detected " + tempCountByEntries.ToString() + "/" + tempCountBySplit.ToString(), MessageText.RelationCaption(), false);
                        msgDiscrepancy.ShowDialog();

                        AhoyAnswerMessageBox msgFixDiscrepancy = new AhoyAnswerMessageBox("Correct any discrepancies?", MessageText.RelationCaption(), false, false);
                        msgFixDiscrepancy.ShowDialog();
                        if (msgFixDiscrepancy.Accepted)
                        {
                            if (Related.RemoveDuplicateIds(CurrentEntry, Entries))
                            {
                                RegisterChangesForSaveAndBackup();
                                SetPoolHasChanges();
                                tempOutput = "Process complete";
                            }
                            PopulatePinListAndReselect();
                        }
                        else
                        {
                            tempOutput = "Action aborted";
                        }
                    }
                    else
                    {
                        if (tempCountByEntries > 1 || tempCountByEntries == 0)
                        {
                            tempOutput = "Category contains " + tempCountByEntries + " entries";
                        }
                        else
                        {
                            tempOutput = "Category contains " + tempCountByEntries + " entry";
                        }
                    }
                }
                else
                {
                    tempOutput = "No related entries";
                }
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(tempOutput, MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
        }

        /// <summary>
        /// Set mark for relations if not currently related (avoid nested relations)
        /// </summary>
        private void RelatedMark(bool showDialog)
        {
            if (CurrentEntryNotNullOrDummy() && !Related.IsRelated(CurrentEntry, Entries))
            {
                // Save ID for later (remember to clear this if entry is deleted etc.)
                if (Related.SetMark(CurrentEntry.ID) && showDialog)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Entry marked: " + CurrentEntry.Title, MessageText.RelationCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to mark", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
        }

        private void RelatedSelectMaster()
        {
            if (!string.IsNullOrEmpty(Related.GetMark()))
            {
                try
                {
                    if (EntryExist(Related.GetMark()))
                    {
                        SelectEntryByID(Related.GetMark(), false, false);
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        // Select master related to selected unless editing another master
        private void RelatedSelectMasterFromSlaveUnlessEditing()
        {
            if (CurrentEntryNotNullOrDummy() && Related.IsRelated(CurrentEntry, Entries))
            {
                try
                {
                    if (Related.CanMark() && Related.GetMasterFromSlave(CurrentEntry, Entries).ID != Related.GetMark())
                    {
                        SelectEntryByID(Related.GetMark(), false, false);

                        if (!Related.HasRelated(CurrentEntry)) // Add just the one
                        {
                            PostEntryInSearchResult(CurrentEntry);
                        }
                        else
                        {
                            CheckListsAndHighlight(CurrentEntry.ID);
                        }
                    }
                    else
                    {
                        SelectEntryByID(Related.GetMasterFromSlave(CurrentEntry, Entries).ID, false, false);
                        CheckListsAndHighlight(CurrentEntry.ID);
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
            else if (CurrentEntryNotNullOrDummy() && !Related.HasRelated(CurrentEntry) && Related.CanMark())
            {
                SelectEntryByID(Related.GetMark(), false, false);
                CheckListsAndHighlight(CurrentEntry.ID);
            }
        }

        private void RelatedAdd(bool showDialog)
        {
            if (Related.CanMark())
            {
                // If current entry != null and not master
                if (CurrentEntryNotNullOrDummy() && !Related.HasRelated(CurrentEntry))
                {
                    if (Related.AddRelated(GetEntryByID(Related.GetMark()), CurrentEntry))
                    {
                        SetStatusText("Category updated");
                        if (CurrentEntry.Pin || cbPinned.Checked)
                        {
                            cbPinned.Checked = false;
                            CurrentEntry.Pin = false; // Remove pin
                        }
                        CurrentEntryHasChanges(); // Save entry
                        SaveCurrentEntry(false);
                        if (showDialog)
                        {
                            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Relation added", MessageText.RelationCaption(), false);
                            msg.ShowDialog();
                        }
                    }
                    else
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to relate", MessageText.RelationCaption(), false);
                        msg.ShowDialog();
                    }
                }
            }
        }

        private List<Entry> tempRelatedSearchCollection;
        private void CheckRelatedAndPostInSearch()
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry))
            {
                if (Related.HasRelated(Entries))
                {
                    try
                    {
                        // List current entry related entries in search result
                        ResetSearchResultSelectionIndex();
                        if (tempRelatedSearchCollection == null)
                        {
                            tempRelatedSearchCollection = new List<Entry>();
                        }
                        tempRelatedSearchCollection.Clear();
                        tempRelatedSearchCollection.AddRange(Related.GetRelatedAsClone(CurrentEntry, Entries));
                        // Sort A-Z by title
                        try
                        {
                            tempRelatedSearchCollection.Sort(delegate (Entry x, Entry y) { return String.Compare(x.Title, y.Title); });
                        }
                        catch
                        {
                            // Log sorting error
                            Log.LogGeneralError("Sorting list failed (Related)");
                        }
                        // Set design code and related mark
                        tempRelatedSearchCollection = Related.RelatedConvert(tempRelatedSearchCollection, CurrentEntry.RelatedStyle);

                        // Insert master at the top
                        tempRelatedSearchCollection.Insert(0, CurrentEntry);
                        // Clear control and add collected items
                        listBoxSearchResult.Items.Clear();
                        PopulateSearchListBox(listBoxSearchResult, tempRelatedSearchCollection);
                        tbSearch.Clear();
                        // Cleanup
                        tempRelatedSearchCollection.Clear();
                        // Reselect master
                        CancelEntrySelectionForHighlight = true;
                        SelectSearchResult(0);
                        CancelEntrySelectionForHighlight = false;
                    }
                    catch
                    {
                        // Log error
                        Log.LogGeneralError("Unable to initialize result");
                    }
                }
            }
        }

        private void PostEntryInSearchResult(Entry entry)
        {
            if (CurrentEntryNotNullOrDummy())
            {

                try
                {
                    ResetSearchResultSelectionIndex();
                    if (tempRelatedSearchCollection == null)
                    {
                        tempRelatedSearchCollection = new List<Entry>();
                    }
                    tempRelatedSearchCollection.Clear();

                    // Insert master at the top
                    tempRelatedSearchCollection.Insert(0, CurrentEntry);
                    // Clear control and add collected items
                    listBoxSearchResult.Items.Clear();
                    PopulateSearchListBox(listBoxSearchResult, tempRelatedSearchCollection);
                    tbSearch.Clear();
                    // Cleanup
                    tempRelatedSearchCollection.Clear();
                    // Reselect master
                    CancelEntrySelectionForHighlight = true;
                    SelectSearchResult(0);
                    CancelEntrySelectionForHighlight = false;
                }
                catch
                {
                    // Log error
                    Log.LogGeneralError("Unable to initialize result");
                }
            }
        }

        private void RelatedList()
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry))
            {
                // List current entry related entries in search result
                SetDefaultStatusText();
                ResetSearchResultSelectionIndex();
                // Continue if entries exist
                if (Related.HasRelated(Entries))
                {
                    listBoxSearchResult.Items.Clear();
                    PopulateSearchListBox(listBoxSearchResult, Related.GetRelatedAsClone(CurrentEntry, Entries));
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No related entries", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
            FocusContent();
        }



        private void RelatedRemove()
        {
            if (CurrentEntryNotNullOrDummy() && Related.IsRelated(CurrentEntry, Entries))
            {
                if (Related.HasRelatedMastersMatchingCurrentMark(CurrentEntry, Entries))
                {
                    Related.RemoveRelated(GetEntryByID(Related.GetMark()), CurrentEntry);
                    SetStatusText("Category updated");
                }
                else
                {
                    Related.RemoveRelated(Related.GetMasterFromSlave(CurrentEntry, Entries), CurrentEntry);
                }
                CurrentEntryHasChanges(); // Save entry
                PopulatePinList();
            }
        }


        private void ClearRelatedFromMaster()
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry) && Related.CanMark() && CurrentEntry.ID == Related.GetMark())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Clear category: \n" + TextTransform.FormatTitleForCategoryPreview(CurrentEntry.Title) + "?", MessageText.RelationCaption(), false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    Related.ClearRelated(CurrentEntry);
                    Related.ClearMark(); // Remove mark to exit editing category
                    CurrentEntryHasChanges(); // Save entry
                    PopulatePinList();
                }
            }
            else if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry))
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Enable editing to clear category", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
            else if (CurrentEntryNotNullOrDummy() && CurrentEntry.ID == Related.GetMark() && !Related.HasRelated(CurrentEntry))
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Category has no related entries", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Select category to perform action", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
        }

        /// <summary>
        /// Remove all relations in the current master
        /// </summary>
        private void RelatedRemoveAll()
        {
            if (Related.CanMark() && CurrentEntryNotNullOrDummy())
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Clear category?", MessageText.RelationCaption(), false, false);
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    SaveBackupForUndo();
                    Related.ClearRelated(CurrentEntry);
                    CurrentEntryHasChanges(); // Save entry
                    PopulatePinList();
                }
            }
        }

        private void RelatedClearAll()
        {
            AhoyAnswerMessageBox msgClearAll = new AhoyAnswerMessageBox("Clear all categories?", MessageText.RelationCaption(), false, false);
            msgClearAll.ShowDialog();

            if (msgClearAll.Accepted)
            {
                if (SimplePromptForPassword(MessageText.RelationCaption(), MessageText.EnterPasswordMessage(), false))
                {
                    SaveBackupForUndo();
                    if (Related.ClearAllRelated(CurrentEntry, Entries))
                    {
                        Related.ClearMark(); // Remove mark to exit editing category
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("All relations cleared", MessageText.RelationCaption(), false);
                        msg.ShowDialog();
                        CurrentEntryHasChanges(); // Save entry
                        PopulatePinList();
                        SetPoolHasChanges();
                    }
                    else
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No relations found", MessageText.RelationCaption(), false);
                        msg.ShowDialog();
                    }
                }
            }
        }

        // Related
        private bool IsCurrentEntryPinnedAndNotRelated()
        {
            if (IsCurrentEntryPinned() && !Related.IsRelated(CurrentEntry, Entries))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Related
        private bool IsCurrentEntryPinnedAndHasRelated()
        {
            if (IsCurrentEntryPinned() && Related.HasRelated(CurrentEntry))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCurrenRelatedAndVisibleInPinnedList()
        {
            if (Related.IsRelated(CurrentEntry, Entries) && Related.IsMasterPinned(CurrentEntry.ID, Entries)) // Find master and check if it is pinned
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Related
        private bool IsCurrentEntryPinned()
        {
            if (CurrentEntryNotNullOrDummy() && CurrentEntry.Pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Related
        private bool IsEntryInPinnedListRelated(object item)
        {
            if (item != null)
            {
                try
                {
                    if (((EntryMinimal)item).MarkRelated)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsCurrentMasterAndNotPinned()
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry) && !CurrentEntry.Pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCurrentSlaveWithUnpinnedMastersOrMasterUnpinned()
        {
            if (CurrentEntryNotNullOrDummy() && (Related.IsRelated(CurrentEntry, Entries) && !Related.IsAllMastersPinned(CurrentEntry, Entries)) ||
                (Related.HasRelated(CurrentEntry) && !CurrentEntry.Pin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Prevent ToggleRelatedView from running once (Used by the PinndUp/Down etc. buttons)
        /// </summary>
        private void SuspendRelatedToggle()
        {
            SuspendToggleRelatedView = true;
        }

        private bool SuspendToggleRelatedView;
        private string tempToggleRelatedViewLastID = "";
        private void ToggleRelatedView()
        {
            // Exit routine if SuspendToggleRelatedView is true
            if (SuspendToggleRelatedView)
            {
                SuspendToggleRelatedView = false;
                return;
            }

            // Is current entry pinned and is master
            if (IsCurrentEntryPinnedAndHasRelated())
            {
                // Ensure the master entry can be selected first and collapsed on second click
                if (CurrentEntry.RelatedView && tempToggleRelatedViewLastID != CurrentEntry.ID)
                {
                    tempToggleRelatedViewLastID = CurrentEntry.ID;
                }
                else
                {
                    // Selecting again should not clear the ID to make expand/collapse more intuitive:
                    // Flip the values, save and repopulate
                    CurrentEntry.RelatedView = !CurrentEntry.RelatedView;
                    if (CurrentEntry.RelatedView)
                    {
                        tempToggleRelatedViewLastID = CurrentEntry.ID;
                        Log.GeneralEntryStatus("Related expanded");
                    }
                    else
                    {
                        tempToggleRelatedViewLastID = "";
                        Log.GeneralEntryStatus("Related collapsed");
                    }
                    CurrentEntryHasChanges();
                    SaveCurrentEntry(false);
                    PopulatePinListAndReselect();
                }
            }
            else
            {
                tempToggleRelatedViewLastID = ""; // Important to clear here to make functionality seem intuitive
            }
        }

        private void FlipRelatedDisplayStyle()
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry))
            {
                if (CurrentEntry.RelatedStyle == 0)
                {
                    SetCategoryStyle(1);
                }
                else if (CurrentEntry.RelatedStyle == 1)
                {
                    SetCategoryStyle(2);
                }
                else if (CurrentEntry.RelatedStyle == 2)
                {
                    SetCategoryStyle(3);
                }
                else if (CurrentEntry.RelatedStyle == 3)
                {
                    SetCategoryStyle(4);
                }
                else if (CurrentEntry.RelatedStyle == 4)
                {
                    SetCategoryStyle(5);
                }
                else if (CurrentEntry.RelatedStyle == 5)
                {
                    SetCategoryStyle(6);
                }
                else if (CurrentEntry.RelatedStyle == 6)
                {
                    SetCategoryStyle(7);
                }
                else
                {
                    SetCategoryStyle(0);
                }
            }
        }

        private void SetCategoryStyle(int index)
        {
            if (CurrentEntryNotNullOrDummy() && Related.HasRelated(CurrentEntry))
            {
                CurrentEntry.RelatedStyle = index;
                Log.GeneralEntryStatus("Category style " + index.ToString());
                CurrentEntryHasChanges();
                SubmitChangesToEntriesList();
                PopulatePinListAndReselect();
                ResolveRelatedOptionsMenuItems(CurrentEntry);
            }
        }


        private void AddNewEntryToEditingCategory()
        {
            // If a category is currently editing while creating a new entry ask to add it
            if (Related.CanMark() && EntryExist(Related.GetMark()) && !Related.HasRelated(CurrentEntry))
            {
                // Force save entry to make it available
                SaveCurrentEntry(false);
                if (CurrentEntryNotNullOrDummy())
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Add entry to category\n" + TextTransform.FormatTitleForCategoryPreview(GetEntryByID(Related.GetMark()).Title) + "?", MessageText.RelationCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();
                    if (msg.Accepted)
                    {
                        RelatedAdd(false); // Add selected to editing master
                        RefreshSearchResultAfterRelatedAdd(); //  Update search if master is unpinned
                    }
                }
            }
        }

        private void CreateDummyCategory()
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Create dummy category?", MessageText.RelationCaption(), false, false);
            msg.TopMost = true;
            msg.ShowDialog();

            if (msg.Accepted)
            {
                try
                {
                    SaveBackupForUndo();
                    List<Entry> temptDummyCategoryList = new List<Entry>();
                    temptDummyCategoryList.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        // Create new entries
                        temptDummyCategoryList.Add(CreateSilentEntry(Related.GetDummyMasterChars(i)));
                    }
                    // Create pinned and expanded category
                    temptDummyCategoryList[0].Pin = true;
                    temptDummyCategoryList[0].RelatedView = true;
                    temptDummyCategoryList[0].RelatedStyle = 7;
                    temptDummyCategoryList[0].RelatedEntries = "";
                    temptDummyCategoryList[0].Rank = -99999; // Place topmost
                    temptDummyCategoryList[0].CustomColor = AhoyColors.ConvertFromHex("#8600FF", false) + ";" + AhoyColors.ConvertFromHex("#E6CAFF", false);

                    // Add related entries
                    for (int k = 1; k < temptDummyCategoryList.Count; k++)
                    {
                        temptDummyCategoryList[0].RelatedEntries += temptDummyCategoryList[k].ID + ";";
                    }

                    // Add gradient
                    Related.GradientColorToAllRelated(temptDummyCategoryList[0], temptDummyCategoryList);

                    // When done add the list to the pool by clone and refresh
                    if (temptDummyCategoryList.Count > 0)
                    {
                        foreach (Entry e in temptDummyCategoryList)
                        {
                            Entries.Add(IO.CloneEntry(e, false));
                        }
                        // Show and update pinned list
                        ShowPinned();
                        PopulatePinListAndReselect();
                        RegisterChangesForSaveAndBackup();
                        SetPoolHasChanges();
                    }

                    // Cleanup temp list
                    temptDummyCategoryList.Clear();
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        private Entry CreateSilentEntry(string title)
        {
            // Initialize new entry
            Entry newEntry = new Entry();
            // Entry variables
            newEntry.ID = Guid.NewGuid().ToString();
            newEntry.Title = title;
            newEntry.Content = "";
            newEntry.Created = IO.GetIsoDateTimeNow();
            newEntry.Changed = IO.GetIsoDateTimeNow();
            newEntry.Bookmark = 0;
            newEntry.Pin = false;
            newEntry.Rank = 0;
            newEntry.PresetColor = 0;
            newEntry.CustomColor = "";
            newEntry.Protected = false;
            newEntry.CaretPosition = 0;
            newEntry.RelatedEntries = "";
            newEntry.RelatedView = false;
            newEntry.RelatedStyle = 0;
            return newEntry;
        }

        private void SetRelatedGradient(bool showDialog)
        {
            if (Related.HasRelated(CurrentEntry) && Related.CountRelated(CurrentEntry) > 1)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Set gradient on category?", MessageText.RelationCaption(), false, false);
                msg.TopMost = true;
                if (showDialog)
                {
                    msg.ShowDialog();
                }

                if (msg.Accepted || !showDialog)
                {
                    SaveBackupForUndo();
                    Related.GradientColorToAllRelated(CurrentEntry, Entries);
                    ShowPinned();
                    PopulatePinListAndReselect();
                    RegisterChangesForSaveAndBackup();
                    SetPoolHasChanges();
                }
            }
            else if (Related.CountRelated(CurrentEntry) <= 1)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Category must contain more than 1 entry", MessageText.RelationCaption(), false);
                msg.ShowDialog();
            }
        }

        #endregion
    }
}

