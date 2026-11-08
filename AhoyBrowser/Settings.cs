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
        internal const bool DefaultPlopDirection = true;
        internal const bool DefaultListFontWeight = false;
        internal const bool DefaultEnforceScrollbars = false;
        internal const bool DefaultMicroCalculator = false;
        internal const int DefaultDisplayTextCycle = 0;
        internal const bool DefaultTitleTabToContent = true;
        internal const bool DefaultInsertNewLines = false;
        internal const bool DefaultUEBlankLines = false;
        internal const bool DefaultUETrimWhitespace = true;
        internal const bool DefaultUEPlopESCtoClose = false;
        internal const bool DefaultNotificationScrollToCaret = false;
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
        public int SnoozeInterval { get; set; }
        public bool PersistNotificationPreview { get; set; }
        public bool ShowWPMCounter { get; set; }
        public int SelectedPasswordStyle { get; set; }
        public bool AutoSaveEntryChanges { get; set; }
        public string OpenFileDialogPath { get; set; }
        public string SaveFileDialogPath { get; set; }
        public bool AutoSearch { get; set; }
        public bool CurrentListFontWeight { get; set; }
        public bool EnforceScrollbars { get; set; }
        public bool MicroCalculatorMain { get; set; }
        public bool MicroCalculatorUE { get; set; }
        public int DisplayTextCycle { get; set; }
        public bool TitleTabToContent { get; set; }
        public bool InsertNewLines { get; set; }
        public bool PlopESCtoClose { get; set; }
        public string BackupLocation { get; set; }
        public bool NotificationScrollToCaret { get; set; }
        public bool ShowClock { get; set; }

        // Typewriter
        public int TypeWriterFontSize { get; set; }
        public int TypeWriterLineCount { get; set; }
        public int TypeWriterCPLIndex { get; set; }
        public int TypeWriterBellMargin { get; set; }
        public int TypeWriterLeftMargin { get; set; }
        public bool TypeWriterDelLock { get; set; }
        public string TypeWriterLogo { get; set; }
        public int TypeWriterBasketX { get; set; }
        public int TypeWriterBasketY { get; set; }
        public bool TypeWriterAutoBasket { get; set; }
        public bool TypeWriterAutoMenu { get; set; }
        public bool TypeWriterTabRuler { get; set; }
        public int TypeWriterColorTheme { get; set; }
        public bool TypeWriterPageAutoScroll { get; set; }


        // Utility/PLOP Editor
        public bool UEOpen { get; set; }
        public bool UEMaximized { get; set; }
        public int UEWidth { get; set; }
        public int UEHeight { get; set; }
        public int UEPositionX { get; set; }
        public int UEPositionY { get; set; }
        public double UEOpacity { get; set; }
        public int UEVersionSelection { get; set; }
        public bool UETopMost { get; set; }
        public bool UEPlopDirection { get; set; }
        public bool UEBlankLines { get; set; }
        public bool UETrimWhitespace { get; set; }

        // Custom colors
        public string CustomColors { get; set; }
        public string ThemeTitle { get; set; }

        // Screen blocker
        public bool ScreenBlocker { get; set; }
        public const bool DefaultScreenBlockerState = false;
        public int ScreenBlockerIndex { get; set; }
        public const int DefaultScreenBlockerIndex = 0; // = OFF

        // Prism effect
        public bool PrismEffect { get; set; }
        public const bool DefaultPrismEffect = false;

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
            SnoozeInterval = 1;
            PersistNotificationPreview = false;
            ShowWPMCounter = false;
            SelectedPasswordStyle = DefaultPasswordStyle;
            AutoSaveEntryChanges = DefaultAutoSaveEntryChanges;
            OpenFileDialogPath = "";
            SaveFileDialogPath = "";
            AutoSearch = DefaultAutoSearch;
            CurrentListFontWeight = DefaultListFontWeight;
            EnforceScrollbars = DefaultEnforceScrollbars;
            MicroCalculatorMain = DefaultMicroCalculator;
            MicroCalculatorUE = DefaultMicroCalculator;
            DisplayTextCycle = DefaultDisplayTextCycle;
            TitleTabToContent = DefaultTitleTabToContent;
            InsertNewLines = DefaultInsertNewLines;
            PlopESCtoClose = DefaultUEPlopESCtoClose;
            BackupLocation = "";
            NotificationScrollToCaret = DefaultNotificationScrollToCaret;
            ShowClock = DefaultShowClock;
            EnableFormBorder = DefaultEnableFormBorder;

            // Utility/PLOP Editor Default Settings
            UEOpen = false;
            UEMaximized = false;
            UEWidth = DetachedEditor.UEDefaultWidth;
            UEHeight = DetachedEditor.UEDefaultHeight;
            UEPositionX = DetachedEditor.GetParentLocationXWithOffset();
            UEPositionY = DetachedEditor.GetParentLocationYWithOffset();
            UEOpacity = DetachedEditor.UEDefaultOpacity;
            UEVersionSelection = DetachedEditor.UEDefaultBank;
            UETopMost = DetachedEditor.UEDefaultTopMost;
            UEPlopDirection = DefaultPlopDirection;
            UEBlankLines = DefaultUEBlankLines;
            UETrimWhitespace = DefaultUETrimWhitespace;

            // TypeWriter default settings
            TypeWriterFontSize = TypeWriter.DefaultFontSize;
            TypeWriterLineCount = TypeWriter.DefaultLineCount;
            TypeWriterCPLIndex = TypeWriter.DefaultCPLIndex;
            TypeWriterBellMargin = TypeWriter.DefaultBellMargin;
            TypeWriterLeftMargin = TypeWriter.DefaultLeftMargin;
            TypeWriterDelLock = TypeWriter.DefaultDelLock;
            TypeWriterLogo = TypeWriter.DefaultLogo;
            TypeWriterBasketX = TypeWriter.DefaultTypeBasketX;
            TypeWriterBasketY = TypeWriter.DefaultTypeBasketY;
            TypeWriterAutoBasket = TypeWriter.DefaultAutoBasket;
            TypeWriterAutoMenu = TypeWriter.DefaultAutoMenu;
            TypeWriterTabRuler = TypeWriter.DefaultTabRuler;
            TypeWriterColorTheme = TypeWriter.DefaultColorTheme;
            TypeWriterPageAutoScroll = TypeWriter.DefaultPageAutoScroll;

            // Custom colors
            CustomColors = DefaultCustomColors;
            ThemeTitle = DefaultThemeTitle;

            // Screen blocker
            ScreenBlocker = DefaultScreenBlockerState;
            ScreenBlockerIndex = DefaultScreenBlockerIndex;

            // Prism effect
            PrismEffect = DefaultPrismEffect;

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
