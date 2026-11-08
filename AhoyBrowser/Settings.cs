namespace AhoyBrowser
{
    class Settings
    {
        internal const double DefaultOpacity = 1.00d; // 100%
        internal const int DefaultTexture = 0;
        internal const int DefaultColorTheme = 0;
        internal const int DefaultSnoozeInterval = 6; // 60 minutes
        internal const int DefaultBrowser = 0;
        internal const int DefaultLayoutMode = 2;
        internal const bool DefaultDetectURLs = true;
        internal const bool DefaultAutoSaveEntryChanges = true;
        internal const bool DefaultAutoSearch = true;
        internal const int DefaultPasswordStyle = 1; // Wordplay
        internal const int DefaultButtonBitmapOverride = 0;
        internal const string DefaultCustomColors = "%NOCOLOR";
        internal const string DefaultThemeTitle = "";
        internal const bool DefaultListFontWeight = false;
        internal const bool DefaultEnforceScrollbars = false;
        internal const int DefaultDisplayTextCycle = 0;
        internal const bool DefaultTitleTabToContent = true;
        internal const bool DefaultInsertNewLines = false;
        internal const bool DefaultShowClock = false;
        internal bool LoadSuccess { get; set; }
        internal string LoadMessage { get; set; }
        public int BackupIdentifier { get; set; }
        public int SleepIntervalIndex { get; set; }
        public int ColorTheme { get; set; }
        public int ButtonBitmapOverride { get; set; }
        public int LayoutMode { get; set; }
        public double Opacity { get; set; }
        public int Texture { get; set; }
        public bool Animations { get; set; }
        public bool WordWrap { get; set; }
        public int SpellCheckerLanguage { get; set; }
        public int CurrentFont { get; set; }
        public int FieldsFontSize { get; set; }
        public int ListsFontSize { get; set; }
        public bool ListsAutoHide { get; set; }
        public bool Fullscreen { get; set; }
        public bool Maximized { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int InternetBrowser { get; set; }
        public bool DetectURLs { get; set; }
        public string HoldButtonAID { get; set; }
        public string HoldButtonBID { get; set; }
        public bool ShowWPMCounter { get; set; }
        public int SelectedPasswordStyle { get; set; }
        public bool AutoSaveEntryChanges { get; set; }
        public string OpenFileDialogPath { get; set; }
        public string SaveFileDialogPath { get; set; }
        public bool AutoSearch { get; set; }
        public bool CurrentListFontWeight { get; set; }
        public bool EnforceScrollbars { get; set; }
        public int DisplayTextCycle { get; set; }
        public bool TitleTabToContent { get; set; }
        public bool InsertNewLines { get; set; }
        public string BackupLocation { get; set; }
        public bool ShowClock { get; set; }

        // Custom colors
        public string CustomColors { get; set; }
        public string ThemeTitle { get; set; }

        // Search
        public bool TitlePrioritySearch { get; set; }
        public const bool DefaultTitlePrioritySearch = false;
        public bool LiveSearch { get; set; }
        public const bool DefaultLiveSearch = true;

        // Form border
        public bool EnableFormBorder { get; set; }
        public const bool DefaultEnableFormBorder = true;

        // New Pinned by Click
        public bool NewPinnedByClick { get; set; }
        public const bool DefaultNewPinnedByClick = false;

        // Hide pinned by homescreen click
        public bool HidePinnedByClick { get; set; }
        public const bool DefaultHidePinnedByClick = false;

        // Keyboard Control
        public bool MoveByKeyboard { get; set; }
        public const bool DefaultMoveByKeyboard = false;
        public bool ResizeByKeyboard { get; set; }
        public const bool DefaultResizeByKeyboard = false;

        // Timestamp
        public int TimestampFormat { get; set; }
        public const int DefaultTimestampFormat = 0;


        public Settings()
        {
            // Ahoy Default Settings
            BackupIdentifier = 0;
            LoadSuccess = false;
            LoadMessage = "No data";
            SleepIntervalIndex = 0;
            ColorTheme = DefaultColorTheme;
            ButtonBitmapOverride = DefaultButtonBitmapOverride;
            LayoutMode = DefaultLayoutMode;
            Opacity = DefaultOpacity;
            Texture = DefaultTexture;
            Animations = true;
            WordWrap = false;
            SpellCheckerLanguage = 1;
            CurrentFont = 0;
            FieldsFontSize = 0;
            ListsFontSize = 0;
            ListsAutoHide = false;
            Fullscreen = false;
            Maximized = false;
            Width = 0;
            Height = 0;
            PositionX = 0;
            PositionY = 0;
            InternetBrowser = DefaultBrowser;
            DetectURLs = DefaultDetectURLs;
            HoldButtonAID = "";
            HoldButtonBID = "";
            ShowWPMCounter = false;
            SelectedPasswordStyle = DefaultPasswordStyle;
            AutoSaveEntryChanges = DefaultAutoSaveEntryChanges;
            OpenFileDialogPath = "";
            SaveFileDialogPath = "";
            AutoSearch = DefaultAutoSearch;
            CurrentListFontWeight = DefaultListFontWeight;
            EnforceScrollbars = DefaultEnforceScrollbars;
            DisplayTextCycle = DefaultDisplayTextCycle;
            TitleTabToContent = DefaultTitleTabToContent;
            InsertNewLines = DefaultInsertNewLines;
            BackupLocation = "";
            ShowClock = DefaultShowClock;
            EnableFormBorder = DefaultEnableFormBorder;

            // Custom colors
            CustomColors = DefaultCustomColors;
            ThemeTitle = DefaultThemeTitle;

            // Search
            TitlePrioritySearch = DefaultTitlePrioritySearch;
            LiveSearch = DefaultLiveSearch;

            // New Pinned by Click
            NewPinnedByClick = DefaultNewPinnedByClick;

            // Hide Pinned by Click
            HidePinnedByClick = DefaultHidePinnedByClick;

            // Keyboard control
            MoveByKeyboard = DefaultMoveByKeyboard;
            ResizeByKeyboard = DefaultResizeByKeyboard;

            // Timestamp
            TimestampFormat = DefaultTimestampFormat;
        }
    }
}
