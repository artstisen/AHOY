using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class TypeWriter : UserControl
    {
        /* TypeWriter control v1.0.4 by LBS, June 2017 */
        /* TypeWriter control v1.0.6 by LBS, July 2017 */
        /* TypeWriter control v1.0.7 by LBS, Dec 2017 */
        /* TypeWriter control v1.0.8 by LBS, Feb 2019 */

        #region PROPERTIES
        private List<TWString> InputList { get; set; }
        private List<string> OutputList { get; set; }
        private List<string> ImportList { get; set; }
        private int PreviousListCount { get; set; }
        private string OutputSummary { get; set; }
        private List<string> SplashLogoList { get; set; }
        private static int CurrentLineCount { get; set; } // The number of lines to display
        private int CPLIndex;
        private int CPLCount;
        private int BellMargin;
        private int LeftMargin;
        private int CurrentFontSize { get; set; }
        private string PreviousContent { get; set; }
        public string LinkedID { get; set; }
        private bool SettingsChanged { get; set; }
        private bool IgnoreSettingsChanged { get; set; }
        private bool IgnoreChanges { get; set; }
        private bool DelLock { get; set; }
        private string LogoComposition { get; set; }
        private string LogoText { get; set; }
        private int BasketX { get; set; }
        private int BasketY { get; set; }
        private bool BasketAdjust { get; set; }
        private bool AutoMenu { get; set; }
        private bool ReviewMode { get; set; }
        private Timer timerMessageDisplay { get; set; }
        private bool DingMessageDisplayed { get; set; }
        private bool RulerCaretInserted { get; set; }
        private bool AnimationRunning { get; set; }
        private string RulerCaret { get; set; }
        private int InstructionCardPage { get; set; }
        private bool ShowTabRuler { get; set; }
        private int ColorTheme { get; set; }
        private bool AutoBasket { get; set; }
        private int SavedScrollPosition { get; set; }
        private bool PageAutoScroll { get; set; }
        private string DocumentTitle { get; set; }
        #endregion

        #region Default values
        // Default values
        internal const int DefaultLineCount = 28; // Default line count
        private const int MinimumLineCount = 10; // Default minimum line count (MUST NOT BE LESS THAN 2)
        private const int MaximumLineCount = 500; // Maximum lines in feed/scoll
        internal const int FeedNudge = 2; // Normal feed cycle
        internal const int FeedJumpNudge = 10; // Fast feed cycle
        internal const int DefaultFontSize = 0; // Small, Medium, Large
        internal const int MaximumFontSize = 2; // Large
        internal const int DefaultCPLIndex = 1; // 1 = 54 CPL
        internal const int DefaultBellMargin = 6; // The "DING!" indicator
        internal const int MaximumBellMargin = 12; // The Max Bell indicator
        internal const int MinimumBellMargin = 3; // The Min Bell indicator (MUST NOT BE LESS THAN 3)
        internal const int DefaultLeftMargin = 2; // The left margin indicator (0, 1, 2)
        internal const int MaximumLeftMargin = 5; // The Max left margin
        internal const bool DefaultDelLock = false;
        private const bool DefaultAdjustBasket = false; // Must be false
        private const bool DefaultReviewMode = false; // Must be false
        internal const string VersionText = "v1.0.8"; // Version text
        internal const string DefaultLogo = "TYPEWRITER"; // Default logo text
        internal const int MaximumLogoLength = 24;
        internal const int MaximumMessageLength = 36;
        internal const int TypeBasketNudge = 5; // Defines how many pixels the type basket moves
        private const int TypeBasketWidthA = 955; // Default size with S font
        private const int TypeBasketWidthB = 1085; // Default size with M font
        private const int TypeBasketWidthC = 1250; // Default size with L font
        internal static int DefaultTypeBasketX = 40; // Default location X
        internal static int DefaultTypeBasketY; // Default location Y (initializes to 0 by default when not specifying value)
        internal const bool DefaultAutoMenu = true;
        internal const bool DefaultTabRuler = true;
        private const int DefaultMessageInterval = 1000;
        private const int DingInterval = 1500;
        internal const int DefaultColorTheme = 0;
        internal const bool DefaultAutoBasket = true;
        internal const bool DefaultPageAutoScroll = true;

        #endregion

        #region Symbol diffinitions (constants)
        // Define Symbols
        private const string CaretSymbol = "^";
        private const string BellSymbol = "•";
        private const string BellReachedSymbol = BellSymbol;
        private const string TopLineSymbol = "─";
        private const string MarginSymbol = "│";
        private const string LMRulerDividerSymbol = "|";
        private const string LMRulerNudgeSymbol = "'";
        private const string Caret = "█"; // The caret shown in the editor
        #endregion

        #region CONSTRUCTOR
        // Constructor
        public TypeWriter()
        {
            InitializeComponent();

            this.PreviewKeyDown += TypeWriter_PreviewKeyDown;
            this.Resize += TypeWriter_Resize;

            // Initialize variables
            PageAutoScroll = DefaultPageAutoScroll;
            CPLIndex = DefaultCPLIndex; // Set default CPL index first
            tbInput.MaxLength = GetCPLCount();
            tbInput.RightToLeft = RightToLeft.No;
            pnlInput.Anchor = AnchorStyles.Bottom;
            pnlInput.Width = TypeBasketWidthA;
            lbMenu.Click += LbMenu_Click;
            lbMenu.Cursor = Cursors.Default;
            lbLogo.Click += LbLogo_Click;
            lbLogo.Cursor = Cursors.Default;
            pnlBasketPosition.Cursor = Cursors.Default;
            pnlBasketPosition.Click += PnlBasketPosition_Click;
            pnlPresets.Cursor = Cursors.Default;
            pnlPresets.Click += pnlPresets_Click;
            lbOutput.Click += LbOutput_Click;
            tbInput.KeyUp += TbInput_KeyUp;
            HideMessageLabel();
            DingMessageDisplayed = false;
            RulerCaretInserted = false;
            AnimationRunning = false;
            InstructionCardPage = 0;
            ColorTheme = DefaultColorTheme;
            AutoBasket = DefaultAutoBasket;
            SavedScrollPosition = 0;
            DocumentTitle = "";

            lbOutput.RightToLeft = RightToLeft.No;
            CPLCount = GetCPLCount();
            BellMargin = DefaultBellMargin;
            InputList = new List<TWString>();
            OutputList = new List<string>();
            ImportList = new List<string>();
            SplashLogoList = new List<string>();

            // Set Splash logo
            SplashSetLogo();
            // Det default values
            PreviousListCount = 0;
            CurrentFontSize = DefaultFontSize;
            CurrentLineCount = DefaultLineCount;
            LeftMargin = DefaultLeftMargin;
            GenerateRulerCaret();
            OutputSummary = "";
            LinkedID = "";
            SettingsChanged = false;
            IgnoreSettingsChanged = false;
            IgnoreChanges = false;
            tbInput.DetectUrls = false;
            DelLock = DefaultDelLock;
            LogoComposition = DefaultLogo;
            LogoText = "";
            BasketAdjust = DefaultAdjustBasket;
            AutoMenu = DefaultAutoMenu;
            ShowTabRuler = DefaultTabRuler;
            ReviewMode = DefaultReviewMode;
            pnlExit.Click += PnlExit_Click;

            // Fonts
            this.lbOutput.Font = new Font(Design.GetFontNameByIndex(1), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbCPLRuler.Font = new Font(Design.GetFontNameByIndex(1), 12F);
            this.tbInput.Font = new Font(Design.GetFontNameByIndex(1), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbSplash.Font = new Font(Design.GetFontNameByIndex(1), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbInstructions.Font = new Font(Design.GetFontNameByIndex(1), 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbLogo.Font = new Font(Design.GetFontNameByIndex(1), 9.75F,FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbMessage.Font = new Font(Design.GetFontNameByIndex(1), 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lbMenu.Font = new Font(Design.GetFontNameByIndex(1), 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);


            // Accept custom tabs
            tbInput.AcceptsTab = true;
            tbInput.TabStop = true;
            // Global timer
            timerGlobalTimer.Interval = 150; // Must be 150ms
            timerGlobalTimer.Tick += timerGlobalTimer_Tick;
            timerGlobalTimer.Enabled = false; // Must be True upon startup
            // Display timer
            timerDisplayWriter.Interval = 10;
            timerDisplayWriter.Tick += timerDisplayWriter_Tick;
            timerDisplayWriter.Enabled = false; // Must be False upon startup
            // Message timer
            timerMessageDisplay = new Timer();
            timerMessageDisplay.Interval = DefaultMessageInterval;
            timerMessageDisplay.Tick += TimerMessageDisplay_Tick;
            timerMessageDisplay.Enabled = false;

            tbInput.TextChanged += TbInput_TextChanged;
            SetRuler();

            // Set control to invisible
            HideControls(true);
        }

        private void TypeWriter_Resize(object sender, EventArgs e)
        {
            if (PageAutoScroll)
            {
                RecalculateOutput();
            }
        }
        #endregion

        #region DISPLAY MESSAGE
        private void DisplayMessage(string message)
        {
            if (!timerMessageDisplay.Enabled)
            {
                ShowMessageLabel(message);
                RefocusInput();
                timerMessageDisplay.Enabled = true;
            }
        }

        private void TimerMessageDisplay_Tick(object sender, EventArgs e)
        {
            HideMessageLabel();
            timerMessageDisplay.Enabled = false;
            timerMessageDisplay.Interval = DefaultMessageInterval; // Reset interval if set by DING
            RefocusInput();
        }

        /// <summary>
        /// Used by the DisplayMessage method
        /// </summary>
        /// <param name="message"></param>
        /// Create message box
        private bool tempLeadingMessageSpace;
        private string tempMessageComposition;
        private void ShowMessageLabel(string message)
        {
            if (message.Length > 0 && message.Length <= MaximumMessageLength)
            {
                if (message.Length < MaximumMessageLength)
                {
                    tempLeadingMessageSpace = false;
                    while (message.Length < MaximumMessageLength)
                    {
                        if (tempLeadingMessageSpace)
                        {
                            message = " " + message;
                        }
                        else
                        {
                            message += " ";
                        }
                        tempLeadingMessageSpace = !tempLeadingMessageSpace;
                    }
                }
                tempMessageComposition = "";
                tempMessageComposition += "┌" + TextTransform.GetRepeatedCharacter(MaximumMessageLength, "─") + "┐" + Environment.NewLine;
                tempMessageComposition += "│" + TextTransform.GetBlankSpace(MaximumMessageLength) + "│" + Environment.NewLine;
                tempMessageComposition += "│" + message + "│" + Environment.NewLine;
                tempMessageComposition += "│" + TextTransform.GetBlankSpace(MaximumMessageLength) + "│" + Environment.NewLine;
                tempMessageComposition += "└" + TextTransform.GetRepeatedCharacter(MaximumMessageLength, "─") + "┘";
            }
            lbMessage.TextAlign = ContentAlignment.MiddleCenter;
            lbMessage.AutoSize = true;
            lbMessage.Text = tempMessageComposition;
            lbMessage.Location = GetControlCenterPointComp(lbOutput, lbMessage, 0, 0);
            lbMessage.Visible = true;
            lbMessage.BringToFront();
        }

        private void HideMessageLabel()
        {
            lbMessage.Text = "";
            lbMessage.Visible = false;
        }
        #endregion

        #region MOUSE CLICKS
        private void LbOutput_Click(object sender, EventArgs e)
        {
            if (lbMenu.Visible)
            {
                ToggleMenu(false, 0);
            }
        }

        private void lbInstructions_Click(object sender, EventArgs e)
        {
            ToggleMenu(true, 2);
        }

        private void LbLogo_Click(object sender, EventArgs e)
        {
            SetLogoFromDialog();
        }

        private void pnlPresets_Click(object sender, EventArgs e)
        {
            ToggleMenu(true, 3);
        }

        private void PnlBasketPosition_Click(object sender, EventArgs e)
        {
            ToggleMenu(true, 2);
        }

        private void LbMenu_Click(object sender, EventArgs e)
        {
            if (lbMenu.Visible)
            {
                HideMenu();
            }
        }
        #endregion


        private void TbInput_TextChanged(object sender, EventArgs e)
        {
            SetRuler();
            UpdateInformationBar();
        }

        private void HideControls(bool hideInstructions)
        {
            lbOutput.Visible = false;
            tbInput.Visible = false;
            pnlInput.Visible = false;
            lbLogo.Visible = false;
            pnlExit.Visible = false;
            pnlBasketPosition.Visible = false;
            pnlPresets.Visible = false;
            HideMenu();
            if (hideInstructions)
            {
                lbInstructions.Visible = false;
            }
        }

        private void ShowControls()
        {
            lbOutput.Visible = true;
            tbInput.Visible = true;
            ShowBasket();
            lbInstructions.Visible = true;
            lbLogo.Visible = true;
            pnlBasketPosition.Visible = true;
            pnlPresets.Visible = true;
            pnlExit.Visible = true;
        }

        #region Start Typewriter
        public void StartTypeWriter(string linkedID, string contentText, string title, int fontSize, int lineCount, int cplIndex, int bellMargin, int leftMargin, bool delLock, string logoText, int basketX, int basketY, bool autoMenu, bool tabRuler, int colorTheme, bool autoBasket, bool autoScroll)
        {
            try
            {
                // Hide controls till after splash
                HideControls(true);
                this.SuspendLayout(); // Suspend layout <----------------------------------
                // Clear internals
                tbInput.Text = "";
                lbOutput.Text = "";
                InputList.Clear();
                ImportList.Clear();
                OutputList.Clear();
                LinkedID = linkedID;
                DingMessageDisplayed = false;
                RulerCaretInserted = false;
                AnimationRunning = false;
                PageAutoScroll = autoScroll;
                InstructionCardPage = 0;
                BasketAdjust = DefaultAdjustBasket; // Always default when opening
                ReviewMode = DefaultReviewMode; // Always default when opening
                ColorTheme = colorTheme;
                AutoBasket = autoBasket;
                // Set basket location
                BasketX = basketX;
                BasketY = basketY;
                ShowTabRuler = tabRuler;
                AutoMenu = autoMenu; // Must be set before splash, here
                DocumentTitle = title;

                if (contentText.ToLower().Trim() == "new entry")
                {
                    contentText = "";
                }

                PreviousContent = contentText;
                PreviousListCount = 0;

                // Set color theme
                SetColorTheme(ColorTheme);

                // Set CPL ruler location
                this.lbCPLRuler.Location = new Point(5, 35); // (X, Y) -nudge Y to move it up and down

                // Set default font size
                IgnoreSettingsChanged = true; // Ignore ON <-------------------------------
                CurrentFontSize = fontSize;
                SetFontSize(CurrentFontSize);
                // This one after the font size is set
                CurrentLineCount = lineCount;
                SaveLineFeed(false);

                // Populate with existing content
                if (PreviousContent.Length > 0)
                {
                    ImportList.AddRange(PreviousContent.Split('\n'));
                }

                if (ImportList.Count > 0)
                {
                    IgnoreChanges = true;
                    for (int i = 0; i < ImportList.Count; i++)
                    {
                        SubmitContentToList(ImportList[i], false);
                    }
                    // Calculate linefeed
                    AddLineFeed(InputList);
                    IgnoreChanges = false;
                    GenerateContentFromInput();
                    //RecalculateOutput();
                }
                this.ResumeLayout(); // Resume layout <----------------------------------

                // Global timer
                timerGlobalTimer.Enabled = true; // Must be True upon startup

                // Splash
                lbSplash.Visible = true;
                lbSplash.Text = "";
                lbSplash.BringToFront();
                lbSplash.Dock = DockStyle.Fill;
                lbSplash.TextAlign = ContentAlignment.MiddleCenter;
                for (int i = 0; i < SplashLogoList.Count; i++)
                {
                    lbSplash.Text += SplashLogoList[i] + Environment.NewLine;
                }
                timerSplash.Interval = 2000; // 2 sec
                timerSplash.Enabled = true;

                CPLIndex = cplIndex;
                BellMargin = bellMargin;
                LeftMargin = leftMargin;
                DelLock = delLock;
                GenerateRulerCaret();
                // Set logo
                LogoText = logoText;
                if (LogoText.Length > 0)
                {
                    SetLogoText(LogoText);
                }
                UpdateCPLCount();
                InsertLeftMargin(); // this first, then SetCaretAndCPLRuler()
                SetRuler();
                IgnoreSettingsChanged = false; // Ignore OFF <-------------------------------
                UpdateInformationBar();
            }
            catch
            {
                Log.LogTypeWriterError();
                StopTypeWriter(true);
            }
        }


        #region COLOR THEME
        private void SetColorTheme(int currentTheme)
        {
            switch (currentTheme)
            {
                case 0:
                    SetAsParentColorTheme();
                    break;
                case 1: // Red theme
                    SetRedTheme(false);
                    break;
                case 2: // Green theme
                    SetGreenTheme(false);
                    break;
                case 3: // Black theme
                    SetBlackTheme(false);
                    break;
                case 4: // Gray theme
                    SetGrayTheme(false);
                    break;
                case 5: // Red theme (with dark paper)
                    SetRedTheme(true);
                    break;
                case 6: // Green theme (with dark paper)
                    SetGreenTheme(true);
                    break;
                case 7: // Black theme (with dark paper)
                    SetBlackTheme(true);
                    break;
                case 8: // Gray theme (with dark paper)
                    SetGrayTheme(true);
                    break;
                default:
                    SetAsParentColorTheme();
                    break;
            }

            // Textures
            ResolveTexture();
            ResolveExitButtonBitmap();
            ResolveBasketPositionAndPresetBitmap();
        }

        private void SetAsParentColorTheme()
        {
            // Set color theme
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbSplash.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbSplash.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbLogo.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbLogo.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbOutput.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbOutput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbCPLRuler.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbCPLRuler.ForeColor = AhoyColors.CurrentColorTheme()[2];
            pnlInput.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlBasketPosition.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlPresets.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlExit.BackColor = AhoyColors.CurrentColorTheme()[0];

            lbInstructions.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbInstructions.ForeColor = AhoyColors.CurrentColorTheme()[2];
            // Set menu
            lbMenu.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbMenu.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbMenu.BorderStyle = BorderStyle.None;
            // Set message
            lbMessage.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbMessage.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbMessage.BorderStyle = BorderStyle.None;
        }

        private void SetRedTheme(bool dark)
        {
            // Form
            this.BackColor = GetColor(5);
            // Splash
            lbSplash.BackColor = GetColor(5);
            lbSplash.ForeColor = GetColor(9);
            // Logo
            lbLogo.BackColor = GetColor(5);
            lbLogo.ForeColor = GetColor(9);
            // Basket background and exit button
            pnlInput.BackColor = GetColor(5);
            pnlBasketPosition.BackColor = GetColor(5);
            pnlPresets.BackColor = GetColor(5);
            pnlExit.BackColor = GetColor(5);
            // Ruler
            lbCPLRuler.BackColor = GetColor(5);
            lbCPLRuler.ForeColor = GetColor(13);
            // Bottom Bar
            lbInstructions.BackColor = GetColor(5);
            lbInstructions.ForeColor = GetColor(9);
            // Paper and Input basket
            if (dark)
            {
                lbOutput.BackColor = GetColor(2);
                lbOutput.ForeColor = GetColor(3);
                tbInput.BackColor = GetColor(2);
                tbInput.ForeColor = GetColor(3);
            }
            else
            {
                lbOutput.BackColor = GetColor(1);
                lbOutput.ForeColor = GetColor(4);
                tbInput.BackColor = GetColor(1);
                tbInput.ForeColor = GetColor(4);
            }
            // Instruction card
            lbMenu.BackColor = GetColor(5);
            lbMenu.ForeColor = GetColor(9);
            lbMenu.BorderStyle = BorderStyle.None;
            // Message dialog
            lbMessage.BackColor = GetColor(5);
            lbMessage.ForeColor = GetColor(9);
            lbMessage.BorderStyle = BorderStyle.None;
        }


        private void SetGreenTheme(bool dark)
        {
            // Form
            this.BackColor = GetColor(6);
            // Splash
            lbSplash.BackColor = GetColor(6);
            lbSplash.ForeColor = GetColor(10);
            // Logo
            lbLogo.BackColor = GetColor(6);
            lbLogo.ForeColor = GetColor(10);
            // Basket background and exit button
            pnlInput.BackColor = GetColor(6);
            pnlBasketPosition.BackColor = GetColor(6);
            pnlPresets.BackColor = GetColor(6);
            pnlExit.BackColor = GetColor(6);
            // Ruler
            lbCPLRuler.BackColor = GetColor(6);
            lbCPLRuler.ForeColor = GetColor(14);
            // Bottom Bar
            lbInstructions.BackColor = GetColor(6);
            lbInstructions.ForeColor = GetColor(10);
            // Paper and Input basket
            if (dark)
            {
                lbOutput.BackColor = GetColor(2);
                lbOutput.ForeColor = GetColor(3);
                tbInput.BackColor = GetColor(2);
                tbInput.ForeColor = GetColor(3);
            }
            else
            {
                lbOutput.BackColor = GetColor(1);
                lbOutput.ForeColor = GetColor(4);
                tbInput.BackColor = GetColor(1);
                tbInput.ForeColor = GetColor(4);
            }
            // Instruction card
            lbMenu.BackColor = GetColor(6);
            lbMenu.ForeColor = GetColor(10);
            lbMenu.BorderStyle = BorderStyle.None;
            // Message dialog
            lbMessage.BackColor = GetColor(6);
            lbMessage.ForeColor = GetColor(10);
            lbMessage.BorderStyle = BorderStyle.None;
        }

        private void SetBlackTheme(bool dark)
        {
            // Form
            this.BackColor = GetColor(7);
            // Splash
            lbSplash.BackColor = GetColor(7);
            lbSplash.ForeColor = GetColor(11);
            // Logo
            lbLogo.BackColor = GetColor(7);
            lbLogo.ForeColor = GetColor(11);
            // Basket background and exit button
            pnlInput.BackColor = GetColor(7);
            pnlBasketPosition.BackColor = GetColor(7);
            pnlPresets.BackColor = GetColor(7);
            pnlExit.BackColor = GetColor(7);
            // Ruler
            lbCPLRuler.BackColor = GetColor(7);
            lbCPLRuler.ForeColor = GetColor(15);
            // Bottom Bar
            lbInstructions.BackColor = GetColor(7);
            lbInstructions.ForeColor = GetColor(11);
            // Paper and Input basket
            if (dark)
            {
                lbOutput.BackColor = GetColor(2);
                lbOutput.ForeColor = GetColor(3);
                tbInput.BackColor = GetColor(2);
                tbInput.ForeColor = GetColor(3);
            }
            else
            {
                lbOutput.BackColor = GetColor(1);
                lbOutput.ForeColor = GetColor(4);
                tbInput.BackColor = GetColor(1);
                tbInput.ForeColor = GetColor(4);
            }
            // Instruction card
            lbMenu.BackColor = GetColor(7);
            lbMenu.ForeColor = GetColor(11);
            lbMenu.BorderStyle = BorderStyle.None;
            // Message dialog
            lbMessage.BackColor = GetColor(7);
            lbMessage.ForeColor = GetColor(11);
            lbMessage.BorderStyle = BorderStyle.None;
        }

        private void SetGrayTheme(bool dark)
        {
            // Form
            this.BackColor = GetColor(8);
            // Splash
            lbSplash.BackColor = GetColor(8);
            lbSplash.ForeColor = GetColor(12);
            // Logo
            lbLogo.BackColor = GetColor(8);
            lbLogo.ForeColor = GetColor(12);
            // Basket background and exit button
            pnlInput.BackColor = GetColor(8);
            pnlBasketPosition.BackColor = GetColor(8);
            pnlPresets.BackColor = GetColor(8);
            pnlExit.BackColor = GetColor(8);
            // Ruler
            lbCPLRuler.BackColor = GetColor(8);
            lbCPLRuler.ForeColor = GetColor(16);
            // Bottom Bar
            lbInstructions.BackColor = GetColor(8);
            lbInstructions.ForeColor = GetColor(12);
            // Paper and Input basket
            if (dark)
            {
                lbOutput.BackColor = GetColor(2);
                lbOutput.ForeColor = GetColor(3);
                tbInput.BackColor = GetColor(2);
                tbInput.ForeColor = GetColor(3);
            }
            else
            {
                lbOutput.BackColor = GetColor(1);
                lbOutput.ForeColor = GetColor(4);
                tbInput.BackColor = GetColor(1);
                tbInput.ForeColor = GetColor(4);
            }
            // Instruction card
            lbMenu.BackColor = GetColor(8);
            lbMenu.ForeColor = GetColor(12);
            lbMenu.BorderStyle = BorderStyle.None;
            // Message dialog
            lbMessage.BackColor = GetColor(8);
            lbMessage.ForeColor = GetColor(12);
            lbMessage.BorderStyle = BorderStyle.None;
        }




        private Color GetRulerColor(bool highlight)
        {
            if (highlight)
            {
                switch (ColorTheme)
                {
                    case 0:
                        return AhoyColors.CurrentColorTheme()[6];
                    case 1:
                        return GetColor(17);
                    case 2:
                        return GetColor(17);
                    case 3:
                        return GetColor(19);
                    case 4:
                        return GetColor(18);
                    case 5:
                        return GetColor(17);
                    case 6:
                        return GetColor(17);
                    case 7:
                        return GetColor(19);
                    case 8:
                        return GetColor(18);
                }
            }
            else
            {
                switch (ColorTheme)
                {
                    case 0:
                        return AhoyColors.CurrentColorTheme()[2];
                    case 1:
                        return GetColor(13);
                    case 2:
                        return GetColor(14);
                    case 3:
                        return GetColor(15);
                    case 4:
                        return GetColor(16);
                    case 5:
                        return GetColor(13);
                    case 6:
                        return GetColor(14);
                    case 7:
                        return GetColor(15);
                    case 8:
                        return GetColor(16);
                }
            }

            return AhoyColors.CurrentColorTheme()[2];
        }


        // Colors are defined here: The "Paintjob"
        public static Color GetColor(int index)
        {
            if (index > 0)
            {
                switch (index)
                {
                    case 1: // PAPER COLORS
                        return Color.FromArgb(255, 255, 255); // White paper
                    case 2:
                        return Color.FromArgb(18, 19, 20); // Black paper
                    case 3:
                        return Color.FromArgb(255, 255, 255); // White font
                    case 4:
                        return Color.FromArgb(0, 0, 0); // Black font
                    case 5: // *** CHASSIS PAINT ***
                        return Color.FromArgb(136, 74, 53); // Red
                    case 6:
                        return Color.FromArgb(95, 104, 78); // Green
                    case 7:
                        return Color.FromArgb(45, 45, 39); // Black
                    case 8:
                        return Color.FromArgb(212, 212, 212); // Gray
                    case 9: // *** CHASSIS TEXT ***
                        return Color.FromArgb(255, 255, 255); // Red fg text
                    case 10:
                        return Color.FromArgb(255, 255, 255); // Green fg text
                    case 11:
                        return Color.FromArgb(187, 165, 100); // Black fg text
                    case 12:
                        return Color.FromArgb(0, 0, 0); // Gray fg text
                    case 13: // *** RULER COLORS ***
                        return Color.FromArgb(255, 255, 255); // Red Ruler fg text
                    case 14:
                        return Color.FromArgb(255, 255, 255); // Green Ruler fg text
                    case 15:
                        return Color.FromArgb(187, 165, 100); // Black Ruler fg text
                    case 16:
                        return Color.FromArgb(0, 0, 0); // Gray Ruler fg text
                    case 17:  // *** RULER HIGHLIGHT ***
                        return Color.FromArgb(255, 255, 255); // Ruler highlight text 1
                    case 18:
                        return Color.FromArgb(0, 0, 0); // Ruler highlight text 2
                    case 19:
                        return Color.FromArgb(255, 255, 255); // Ruler highlight text 3
                    default:
                        return Color.FromArgb(0, 0, 0);
                }
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            }
        }
        #endregion

        private void ResolveExitButton()
        {
            ResolveExitButtonBitmap();
            pnlExit.Anchor = AnchorStyles.None;
            pnlExit.Size = new Size(37, 23);
            pnlExit.Top = 10;
            pnlExit.Left = (lbOutput.Width - (pnlExit.Width + 10));
            pnlExit.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pnlExit.BringToFront();
        }

        private void ResolveExitButtonBitmap()
        {
            pnlExit.BackgroundImageLayout = ImageLayout.Center;
            if (ColorTheme == 0)
            {
                pnlExit.BackgroundImage = ButtonBitmaps.Clear(false);
            }
            else if (ColorTheme == 3 || ColorTheme == 7)
            {
                pnlExit.BackgroundImage = ButtonBitmaps.ClearGold();
            }
            else
            {
                pnlExit.BackgroundImage = ButtonBitmaps.ClearHighlight();
            }
        }

        // Get values if zero position
        private void InitializeBasketLocation()
        {
            if (AutoBasket)
            {
                AlignBasketCenter();
            }
            else if (BasketY == 0)
            {
                // Set default basket position
                DefaultTypeBasketX = 40;
                DefaultTypeBasketY = lbOutput.Height - ((pnlInput.Height + lbLogo.Height) + 40);
                BasketX = DefaultTypeBasketX;
                BasketY = DefaultTypeBasketY;
            }
        }




        // The Arrow and preset button
        private void ResolveBasketPositionAndPresetButtons()
        {
            pnlPresets.BackgroundImageLayout = ImageLayout.Center;
            pnlPresets.Anchor = AnchorStyles.Bottom;
            pnlPresets.Size = new Size(37, 23);
            pnlPresets.Top = lbLogo.Top + 5;
            pnlPresets.Left = lbLogo.Left - pnlPresets.Width;
            pnlPresets.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pnlPresets.BringToFront();

            pnlBasketPosition.BackgroundImageLayout = ImageLayout.Center;
            ResolveBasketPositionAndPresetBitmap();
            pnlBasketPosition.Anchor = AnchorStyles.Bottom;
            pnlBasketPosition.Size = new Size(37, 23);
            pnlBasketPosition.Top = lbLogo.Top + 5;
            pnlBasketPosition.Left = lbLogo.Left + lbLogo.Width;
            pnlBasketPosition.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pnlBasketPosition.BringToFront();
        }

        private void ResolveBasketPositionAndPresetBitmap()
        {
            if (ColorTheme == 0)
            {
                pnlPresets.BackgroundImage = ButtonBitmaps.TypeWriterPreset();
                if (BasketAdjust)
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketPosition();
                }
                else
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketLocked();
                }
            }
            else if (ColorTheme == 3 || ColorTheme == 7)
            {
                pnlPresets.BackgroundImage = ButtonBitmaps.TypeWriterPresetGold();
                if (BasketAdjust)
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketPositionGold();
                }
                else
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketLockedGold();
                }
            }
            else
            {
                pnlPresets.BackgroundImage = ButtonBitmaps.TypeWriterPresetHighLight();
                if (BasketAdjust)
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketPositionHighLight();
                }
                else
                {
                    pnlBasketPosition.BackgroundImage = ButtonBitmaps.TypeWriterBasketLockedHighLight();
                }
            }


        }


        private void PnlExit_Click(object sender, EventArgs e)
        {
            StopTypeWriter(false);
        }

        private void ResolveTexture()
        {
            // Set form background image
            pnlInput.BackgroundImageLayout = ImageLayout.Tile;
            pnlInput.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);
            lbLogo.BackgroundImageLayout = ImageLayout.Tile;
            lbLogo.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);
            lbInstructions.BackgroundImageLayout = ImageLayout.Tile;
            lbInstructions.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);
            lbCPLRuler.BackColor = Color.Transparent;
        }
        #endregion


        #region LOGO
        private void SetLogoText(string text)
        {
            // Must be between 1 and MaximumLogoLength
            // Truncate if too long
            if (text.Length > MaximumLogoLength)
            {
                text = text.Substring(0, MaximumLogoLength);
            }

            LogoText = text;
            lbLogo.BringToFront();
            CreateLogo(text);
        }

        private void ResetLogo()
        {
            CreateLogo(DefaultLogo);
        }

        // Create Logo
        private bool tempLeadingSpace;
        private void CreateLogo(string text)
        {
            if (text.Length > 0 && text.Length <= MaximumLogoLength)
            {
                if (text.Length < MaximumLogoLength)
                {
                    tempLeadingSpace = false;
                    while (text.Length < MaximumLogoLength)
                    {
                        if (tempLeadingSpace)
                        {
                            text = " " + text;
                        }
                        else
                        {
                            text += " ";
                        }
                        tempLeadingSpace = !tempLeadingSpace;
                    }
                }

                LogoComposition = "";
                LogoComposition += "  ╓" + TextTransform.GetRepeatedCharacter(MaximumLogoLength, "─") + "╖  " + Environment.NewLine;
                LogoComposition += "◊═╣" + text + "╠═◊" + Environment.NewLine;
                LogoComposition += "  ╚" + TextTransform.GetRepeatedCharacter(MaximumLogoLength, "═") + "╝  ";
            }

            LogoText = text;
            lbLogo.Text = LogoComposition;
            this.lbLogo.Size = new Size(275, 60); // Set label size
            CenterLogo();
        }

        private void CenterLogo()
        {
            lbLogo.Location = GetControlCenterPointComp(pnlInput, lbLogo, 0, pnlInput.Height - 5);
            lbLogo.Anchor = AnchorStyles.Bottom;

            pnlPresets.Top = lbLogo.Top + 5;
            pnlPresets.Left = lbLogo.Left - pnlPresets.Width;
            pnlPresets.Anchor = AnchorStyles.Bottom;
            pnlPresets.BringToFront();

            pnlBasketPosition.Top = lbLogo.Top + 5;
            pnlBasketPosition.Left = lbLogo.Left + lbLogo.Width;
            pnlBasketPosition.Anchor = AnchorStyles.Bottom;
            pnlBasketPosition.BringToFront();
            lbLogo.BringToFront();
            pnlInput.BringToFront();

            // Bring menu to front if visible.
            if (lbMenu.Visible)
            {
                lbMenu.BringToFront();
            }

            SettingsChanged = true;
            SubmitContent();
        }

        private Point GetControlCenterPointComp(Control parent, Control child, int offsetX, int offsetY)
        {
            Point p = new Point();
            p.X = parent.Location.X + ((parent.Width / 2) - (child.Width / 2)) + offsetX;
            p.Y = parent.Location.Y + ((parent.Height / 2) - (child.Height / 2)) + offsetY;
            return p;
        }
        #endregion


        private int tempLineCount;
        private void AddLineFeed(List<TWString> list)
        {
            if (PageAutoScroll)
            {
                CurrentLineCount = GetLinesNeededForAutoScroll();
            }

            if (list != null)
            {
                // Calculate linecount
                tempLineCount = list.Count;
                // If less lines than line count
                if (tempLineCount < CurrentLineCount)
                {
                    // Insert top line at index 0 before looping on tempLineCount
                    // to make one extra string available
                    list.Insert(0, new TWString("", true, true, true));
                    for (int i = tempLineCount; i < CurrentLineCount; i++)
                    {
                        // Insert line feed
                        list.Insert(0, new TWString("", true, true, false));
                    }
                }
            }
        }

        // Strip list of LineFeeds
        private void RemoveLineFeeds(List<TWString> list)
        {
            try
            {
                if (list != null && list.Count > 0)
                {
                    while (ListContainsLineFeed(list))
                    {
                        RemoveListItem(list, 0);
                    }
                }
            }
            catch
            {
                // do nothing
            }
        }

        private void RemoveListItem(List<TWString> list, int index)
        {
            if (index < list.Count)
            {
                list.RemoveAt(index);
            }
        }

        private bool tempContainsLineFeed;
        private bool ListContainsLineFeed(List<TWString> list)
        {
            tempContainsLineFeed = false;

            if (list != null && list.Count > 0)
            {
                foreach (TWString s in list)
                {
                    if (s.LineFeed)
                    {
                        tempContainsLineFeed = true;
                        break;
                    }
                }
            }

            return tempContainsLineFeed;
        }

        #region AUTOSCROLL
        private void ToggleAutoScroll()
        {
            if (!ReviewMode)
            {
                PageAutoScroll = !PageAutoScroll;
                // Recalculate output if entering Auto-Scroll mode
                if (PageAutoScroll)
                {
                    RecalculateOutput();
                    DisplayMessage("Auto-Scroll ON");
                }
                else
                {
                    DisplayMessage("Auto-Scroll OFF");
                }
                UpdateInformationBar();
                SettingsChanged = true;
                SubmitContent();
            }
        }

        // Calculate lines from font height in pixels
        private float tempFontSizeInPoints;
        private float tempFontSizeInPixels;
        private float tempFontSizeInPixelsWithLineSpacing;
        private float tempOutputHeightMinusControls;
        private int tempLinesAvailable;
        private int tempMenuAndBasketPixelHeight;
        private int GetLinesNeededForAutoScroll()
        {
            try
            {
                tempFontSizeInPoints = 0;
                tempFontSizeInPixels = 0;
                tempFontSizeInPixelsWithLineSpacing = 0;
                tempLinesAvailable = 0;
                using (Graphics g = this.CreateGraphics())
                {
                    // Calculate font height
                    tempFontSizeInPoints = lbOutput.Font.SizeInPoints;
                    tempFontSizeInPixels = tempFontSizeInPoints * g.DpiX / 72;
                    tempFontSizeInPixelsWithLineSpacing = tempFontSizeInPixels + 2;

                    // Calculate how many lines can be created with the current font height:
                    // Get available pixels minus the typebasket with logo, bottom menu and additional spacing
                    tempMenuAndBasketPixelHeight = (pnlInput.Height + lbLogo.Height + lbInstructions.Height + GetAutoScrollCompensation());
                    // From the remaining pixels calculate number of lines
                    tempOutputHeightMinusControls = lbOutput.Height - tempMenuAndBasketPixelHeight;
                    // Round to the highest value and convert to int (also does rounding)
                    tempLinesAvailable = Convert.ToInt32(Math.Round(tempOutputHeightMinusControls / tempFontSizeInPixelsWithLineSpacing, MidpointRounding.AwayFromZero));

                    if (tempLinesAvailable > 0)
                    {
                        return tempLinesAvailable;
                    }
                    else
                    {
                        DisplayMessage("Error: Auto-Scroll");
                        return DefaultLineCount;
                    }
                }
            }
            catch
            {
                DisplayMessage("Error: Auto-Scroll");
                return DefaultLineCount;
            }
        }

        // Some font sizes brings the lines too close to the basket because of
        // the nature of lines and font sizes that differ in decimal float sizes
        private int GetAutoScrollCompensation()
        {
            if (CurrentFontSize == 0)
            {
                return 75;
            }
            else if (CurrentFontSize == 1)
            {
                return 95;
            }
            else
            {
                return 105;
            }
        }

        private void UpdateLineCount()
        {
            // Autoscroll page
            if (PageAutoScroll && !ReviewMode)
            {
                CurrentLineCount = GetLinesNeededForAutoScroll();
            }
        }

        #endregion


        // Recalculate everything
        private void RecalculateOutput()
        {
            UpdateLineCount();
            IgnoreChanges = true;
            RemoveLineFeeds(InputList);
            AddLineFeed(InputList);
            ResetOutputAndTruncate();
            IgnoreChanges = false;
        }

        private void ResetOutputAndTruncate()
        {
            if (InputList != null && InputList.Count > 0)
            {
                lbOutput.Text = "";
                OutputList.Clear();
                // Add the list after deletion
                for (int i = 0; i < InputList.Count; i++)
                {
                    InputList[i].NewContent = false;
                    if (InputList[i].TopLine)
                    {
                        OutputList.Add(TopLineSymbol + Environment.NewLine);
                    }
                    else
                    {
                        OutputList.Add(InputList[i].Content + Environment.NewLine);
                    }
                }

                // Truncate output list to a maximum of MaxLines
                TruncateOutputList();

                for (int i = 0; i < OutputList.Count; i++)
                {
                    lbOutput.Text += OutputList[i];
                }

                // Insert caret
                InsertCaret();
                // Update previous list count
                PreviousListCount = InputList.Count;
                GenerateContentFromInput();
            }
        }

        private void TruncateOutputList()
        {
            // Truncate output list to a maximum of MaxLines
            if (OutputList.Count > CurrentLineCount)
            {
                while (OutputList.Count > CurrentLineCount)
                {
                    OutputList.RemoveAt(0);
                }
            }
        }


        private bool IsLineFeed(TWString line)
        {
            if (line.LineFeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetRuler()
        {
            if (tbInput.Text.Length > 0)
            {
                // Print caret location
                if (tbInput.Text.Length < tbInput.MaxLength)
                {
                    lbCPLRuler.Text = GetBlankSpaceWithBellSymbol(tbInput.Text.Length, BellReachedSymbol, CPLCount, BellMargin, MarginSymbol, LeftMargin, LMRulerNudgeSymbol, LMRulerDividerSymbol) + CaretSymbol;
                }
                else
                {
                    lbCPLRuler.Text = GetBlankSpaceWithBellSymbol(tbInput.Text.Length - 1, BellReachedSymbol, CPLCount, BellMargin, MarginSymbol, LeftMargin, LMRulerNudgeSymbol, LMRulerDividerSymbol) + CaretSymbol;
                }
                // Print CPL Stop
                lbCPLRuler.Text += CalculateSpacesFromStringWithBell(lbCPLRuler.Text, tbInput.MaxLength) + MarginSymbol + GetCPLMarginBell();
            }
            else
            {
                // Print CPL Stop
                lbCPLRuler.Text = CaretSymbol;
                lbCPLRuler.Text += CalculateSpacesFromStringWithBell(lbCPLRuler.Text, tbInput.MaxLength) + MarginSymbol + GetCPLMarginBell();
            }
        }

        private static string TempSpaces { get; set; }
        internal string GetBlankSpaceWithBellSymbol(int spaces, string bellSymbol, int cplCount, int bellMargin, string marginSymbol, int leftMargin, string lmNudgeSymbol, string lmDividerSymbol)
        {
            TempSpaces = "";
            for (int i = 0; i < spaces; i++)
            {
                if (i == (cplCount - bellMargin)) // Insert bell symbol or whitespace
                {
                    TempSpaces += bellSymbol;
                }
                else
                {
                    if ((i < GetTabs(leftMargin).Length - 1) && IsBetweenTabs(i)) // Insert LM ruler divider (|) between two tabs
                    {
                        TempSpaces += lmDividerSymbol;
                    }
                    else if ((i < GetTabs(leftMargin).Length - 1) && leftMargin > 0) // Insert LM ruler chars (') before left margin bar
                    {
                        TempSpaces += lmNudgeSymbol;
                    }
                    else if ((i == GetTabs(leftMargin).Length - 1) && leftMargin > 0) //Insert left margin bar 1 before caret
                    {
                        TempSpaces += marginSymbol;
                    }
                    else
                    {
                        TempSpaces += " ";
                    }
                }
            }
            return TempSpaces;
        }


        private static int TempDistance { get; set; }
        internal string CalculateSpacesFromStringWithBell(string shortcutText, int totalLength)
        {
            // Calculate the remaining spaces to reach a maximum of X char-length
            TempDistance = totalLength - shortcutText.Length;
            return GetBlankSpaceWithBellSymbol(TempDistance, BellSymbol, TempDistance, BellMargin, "", 0, LMRulerNudgeSymbol, LMRulerDividerSymbol);
        }

        private string TempTabs { get; set; }
        internal string GetTabs(int count)
        {
            TempTabs = "";
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    TempTabs += TextTransform.GetBlankSpace(4);
                }
            }
            return TempTabs;
        }

        private bool IsBetweenTabs(int index)
        {
            switch (index + 1)
            {
                case 4:
                    return true;
                case 8:
                    return true;
                case 12:
                    return true;
                case 16:
                    return true;
                case 20:
                    return true;
                default:
                    return false;
            }
        }

        internal string RemoveTabs(string input)
        {
            return input.TrimStart(new char[1] { ' ' });
        }

        private string GetCPLMarginBell()
        {
            // Display "DING!" when MarginBellCount characters left to right margin, but not at the last 2 characters
            if (tbInput.Text.Length >= tbInput.MaxLength - BellMargin && tbInput.Text.Length <= tbInput.MaxLength - 2)
            {
                lbCPLRuler.ForeColor = GetRulerColor(true);

                if (!DingMessageDisplayed)
                {
                    DingMessageDisplayed = true;
                    timerMessageDisplay.Interval = DingInterval;
                    DisplayMessage("DING!");
                }
                return "Bell";
            }
            else if (tbInput.Text.Length >= tbInput.MaxLength - 1 && tbInput.Text.Length <= tbInput.MaxLength - 1)
            {
                lbCPLRuler.ForeColor = GetRulerColor(true);
                return "";
            }
            else if (tbInput.Text.Length == tbInput.MaxLength)
            {
                lbCPLRuler.ForeColor = GetRulerColor(true);
                return "EOL";
            }
            else
            {
                DingMessageDisplayed = false;
                lbCPLRuler.ForeColor = GetRulerColor(false);
                return "";
            }
        }

        #region Menu
        private void UpdateInformationBar()
        {
            if (ReviewMode) // Display different Info-Bar in Review Mode
            {
                lbInstructions.Text =
                    "Review " + GetReviewModeByName() + " │ " +
                    "Scroll " + GetAutoScrollByName() + " │ " +
                    "Use Arrow Keys or PageUp/Down to scroll Page. Hold CTRL to scroll x10 │ " +
                    "Press F11 or ESC to Exit";
            }
            else // Display normal Info-Bar in Type Mode
            {
                lbInstructions.Text =
                    "Review " + GetReviewModeByName() + " │ " +
                    "LM " + GetTabSizeByName() + " │ " +
                    "Scroll " + GetAutoScrollByName() + " │ " +
                    "CPL " + (CPLCount - GetTabsCount()).ToString("00") + " │ " +
                    "Bell " + BellMargin.ToString("00") + " │ " +
                    "Del Lock " + GetDelLockByName() + " │ " +
                    "Basket " + GetBasketLockByName() + " │ " +
                    "Font " + GetFontSizeByName() + " │ " +
                    "Style " + GetColorThemeByName() + " │ " +
                    "F7 = Menu" + GetAutoShowMenuByName() + " │ " +
                    //"F9 = Reset" + " │ " +
                    "ESC = Exit";
            }
            RefocusInput();
        }
        #endregion

        private void StopTypeWriter(bool showDialogOnEmptyField)
        {
            // Check if content was left in the input field
            if (tbInput.Text.Trim().Length > 0)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox(MessageText.SubmitContentTypeWriteMessage(), MessageText.TypeWriterCaption(), true, false);
                msg.TopMost = true;
                msg.ShowDialog();

                if (msg.Canceled)
                {
                    return;
                }
                else if (msg.Accepted)
                {
                    SubmitLineToPaper();
                }
            }
            else
            {
                if (showDialogOnEmptyField)
                {
                    AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Exit Typewriter?", MessageText.TypeWriterCaption(), false, false);
                    msg.TopMost = true;
                    msg.ShowDialog();

                    if (msg.Canceled || !msg.Accepted)
                    {
                        return;
                    }
                }
            }

            lbInstructions.Text = "Exit...";
            timerGlobalTimer.Enabled = false;
            HideControls(false);
            MainForm.ExitTypeWriter = true;
        }

        // Fontsize (0-2)
        public void SetFontSize(int size)
        {
            switch (size)
            {
                case 0:
                    CurrentLineCount = DefaultLineCount; // Default line count
                    this.lbOutput.Font = new Font(Design.GetFontNameByIndex(1), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    SetTypeBasketWidth(TypeBasketWidthA);
                    break;
                case 1:
                    CurrentLineCount = 24; // Adjust line count to 24
                    this.lbOutput.Font = new Font(Design.GetFontNameByIndex(1), 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    SetTypeBasketWidth(TypeBasketWidthB);
                    break;
                case 2:
                    CurrentLineCount = 20; // Adjust line count to 20
                    this.lbOutput.Font = new Font(Design.GetFontNameByIndex(1), 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    SetTypeBasketWidth(TypeBasketWidthC);
                    break;
                default:
                    CurrentLineCount = DefaultLineCount; // Default line count
                    this.lbOutput.Font = new Font(Design.GetFontNameByIndex(1), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    SetTypeBasketWidth(TypeBasketWidthA);
                    break;
            }

            // Update input and ruler font
            tbInput.Font = lbOutput.Font;
            lbCPLRuler.Font = tbInput.Font;
        }

        private void SetTypeBasketWidth(int width)
        {
            pnlInput.Width = width;
            CenterLogo();
        }

        #region SPLASH
        private void SplashSetLogo()
        {
            SplashLogoList.Clear();
            SplashLogoList.Add("            ▄▄▄▄▄▄▄▄▄             ");
            SplashLogoList.Add("            █ ▄ ▄▄▄ █             ");
            SplashLogoList.Add("            █ ▄▄▄▄  █             ");
            SplashLogoList.Add("          ▄▄█ ▄▄▄ ▄ █▄▄           ");
            SplashLogoList.Add("          █ █▄     ▄█ █           ");
            SplashLogoList.Add("        █▄█▄████ ████▄█▄█         ");
            SplashLogoList.Add("        █████ ▄ ▄ ▄ █████         ");
            SplashLogoList.Add("         ██▄ █ ▀ ▀ █ ▄██          ");
            SplashLogoList.Add("         ██▄▀ █▀▀▀█ ▀▄██          ");
            SplashLogoList.Add("         ███████████████          ");
            SplashLogoList.Add("          ▀▀▀▀▀▀▀▀▀▀▀▀▀           ");
            SplashLogoList.Add("                                  ");
            SplashLogoList.Add(" *** Distraction-free writing *** ");
            SplashLogoList.Add("         AHOY! Typewriter         ");
            SplashLogoList.Add("             " + VersionText + "              ");
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            timerSplash.Enabled = false;
            lbSplash.Visible = false;
            this.SuspendLayout();
            // Focus input
            if (OutputList.Count > 0)
            {
                WriteOutput();
            }
            else
            {
                RecalculateOutput();
            }
            ShowControls();
            this.ResumeLayout();
            InitializeControlsAfterLoad();
        }
        #endregion


        private void InitializeControlsAfterLoad()
        {
            // Initialize Basket location
            InitializeBasketLocation();
            // Resolve Basket indicator
            ResolveBasketPositionAndPresetButtons();
            // Place Basket
            SetBasket(BasketX, BasketY);
            // Place exit button
            ResolveExitButton();
            // Auto show instruction card
            if (AutoMenu)
            {
                ToggleMenu(false, 0);
            }

            // Recalculate output
            RecalculateOutput();
            // Focus input
            RefocusInput();
        }




        private void SubmitLineToPaper()
        {
            // Check if input is Carriage Return (new line)
            // and set it directly
            if (tbInput.Text.Trim().Length == 0)
            {
                tbInput.SuspendLayout();
                DingMessageDisplayed = false;
                InsertCarriageReturn();
                tbInput.Clear(); // Clear input box
                InsertLeftMargin();
                tbInput.ResumeLayout();
            }
            else
            {
                DingMessageDisplayed = false;
                // Submit string to list
                SubmitContentToList(tbInput.Text, false);
                tbInput.Clear(); // Clear input box
                InsertLeftMargin();
            }
        }


        // Duplicate content on purpose:
        // This event will catch any keypresses done if the tbInput doesn't have focus
        // which occurs if the user activates another application window
        // while in Preview Mode. Once the tbInput is hidden (visible = false) it is not
        // possible to re-focus it from another method.
        private void TypeWriter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!tbInput.Focused && ReviewMode)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F11)
                {
                    ToggleReviewMode();
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.F3 || e.Control && e.KeyCode == Keys.PageDown || (ReviewMode && e.Control && e.KeyCode == Keys.Down)) // Move feed down by JumpNudge
                {
                    // Cycle line count backwards
                    CycleLineFeedJump(true);
                    return;
                }
                else if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.PageDown || (ReviewMode && e.KeyCode == Keys.Down)) // Move feed down
                {
                    // Cycle line count backwards
                    CycleLineFeed(true);
                    return;
                }
                else if (e.Control && e.KeyCode == Keys.F4 || e.Control && e.KeyCode == Keys.PageUp || (ReviewMode && e.Control && e.KeyCode == Keys.Up)) // Move feed up by JumpNudge
                {
                    // Cycle line count
                    CycleLineFeedJump(false);
                    return;
                }
                else if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.PageUp || (ReviewMode && e.KeyCode == Keys.Up)) // Move feed up
                {
                    // Cycle line count
                    CycleLineFeed(false);
                    return;
                }
                else if (e.KeyCode == Keys.F7)
                {
                    // Show instructions
                    ToggleMenu(false, 0);
                }
            }
        }


        // Remove rulercaret on keyUp
        private void TbInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (RulerCaretInserted)
            {
                RemoveRulerCaret();
            }
        }

        // Register Return Key
        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Refresh activity
            MainForm.KeepAwake();

            // Insert Tab-Ruler on control key
            if (e.Control && ShowTabRuler && !RulerCaretInserted)
            {
                InsertRulerCaret();
            }

            if (lbMenu.Visible && (e.KeyCode == Keys.Up | e.KeyCode == Keys.Right))
            {
                FlipInstructionCard(false);
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            else if (lbMenu.Visible && (e.KeyCode == Keys.Down | e.KeyCode == Keys.Left))
            {
                FlipInstructionCard(true);
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (!ReviewMode)
                {
                    // Double Carriage Return
                    SubmitLineToPaper();
                    SubmitLineToPaper();
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!ReviewMode)
                {
                    SubmitLineToPaper();
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D1)
            {
                // Default theme (reset)
                SetPreset(0);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                SetPreset(1);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D3)
            {
                SetPreset(2);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D4)
            {
                SetPreset(3);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D5)
            {
                SetPreset(4);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D6)
            {
                SetPreset(5);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D7)
            {
                SetPreset(6);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                // Reset Scroll
                ResetScroll();

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.K)
            {
                // Toggle Auto menu
                ToggleAutoMenu();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                // Toggle Tab-Ruler
                ToggleTabRuler();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                // Toggle Del lock
                ToggleLockDelete();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                if (!ReviewMode)
                {
                    // Cycle Bell margin
                    CycleBellMargin(false);
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                // Basket Lock
                ToggleBasketLock();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                // Basket Auto-Align
                ToggleBasketAutoAlignment();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                // Colortheme
                CycleColorTheme(true);
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.J)
            {
                // Colortheme
                CycleColorTheme(false);
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                // Toggle Auto-Scroll
                ToggleAutoScroll();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                // Paste unformatted
                tbInput.PasteUnformatted();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                // Print
                Print();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (BasketAdjust && e.KeyCode == Keys.Up)
            {
                if (!ReviewMode)
                {
                    if (e.Control)
                    {
                        MoveTypeBasketUp(5);
                    }
                    else
                    {
                        MoveTypeBasketUp(1);
                    }
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (BasketAdjust && e.KeyCode == Keys.Down)
            {
                if (!ReviewMode)
                {
                    if (e.Control)
                    {
                        MoveTypeBasketDown(5);
                    }
                    else
                    {
                        MoveTypeBasketDown(1);
                    }
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (BasketAdjust && e.KeyCode == Keys.Left)
            {
                if (!ReviewMode)
                {
                    if (e.Control)
                    {
                        MoveTypeBasketLeft(5);
                    }
                    else
                    {
                        MoveTypeBasketLeft(1);
                    }
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (BasketAdjust && e.KeyCode == Keys.Right)
            {
                if (!ReviewMode)
                {
                    if (e.Control)
                    {
                        MoveTypeBasketRight(5);
                    }
                    else
                    {
                        MoveTypeBasketRight(1);
                    }
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                if (!ReviewMode)
                {
                    // Cycle Left Margin
                    CycleLeftMargin(true);
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (!ReviewMode)
                {
                    // Cycle Left Margin
                    CycleLeftMargin(false);
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F3 || e.Control && e.KeyCode == Keys.PageDown || (ReviewMode && e.Control && e.KeyCode == Keys.Down)) // Move feed down by JumpNudge
            {
                // Cycle line count backwards
                CycleLineFeedJump(true);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.PageDown || (ReviewMode && e.KeyCode == Keys.Down)) // Move feed down
            {
                // Cycle line count backwards
                CycleLineFeed(true);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F4 || e.Control && e.KeyCode == Keys.PageUp || (ReviewMode && e.Control && e.KeyCode == Keys.Up)) // Move feed up by JumpNudge
            {
                // Cycle line count
                CycleLineFeedJump(false);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.PageUp || (ReviewMode && e.KeyCode == Keys.Up)) // Move feed up
            {
                // Cycle line count
                CycleLineFeed(false);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (!ReviewMode)
                {
                    // Cycle CPL count
                    CycleCPLCount(true);
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (!ReviewMode)
                {
                    // Cycle CPL count
                    CycleCPLCount(false);
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F7)
            {
                // Show instructions
                ToggleMenu(false, 0);
            }
            else if (e.KeyCode == Keys.F8)
            {
                // Cycle font Size
                CycleFontSize(false);

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F9)
            {
                // Reset settings
                ResetSettings();

                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (!ReviewMode)
                {
                    SetLogoFromDialog();
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F11)
            {
                // Toggle review mode
                ToggleReviewMode();
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Back)
            {
                // Delete line if not Del lock, review or menu is showing
                if (!DelLock && !ReviewMode && !lbMenu.Visible)
                {
                    RemoveLastLineFromList();
                }
                else
                {
                    DisplayMessage(MessageText.LockedTypeWriterMessage());
                }
                // Stop here
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back && tbInput.Text.Length == 0 || e.KeyCode == Keys.Back && tbInput.Text.Length == GetTabsCount())
            {   // Stop delete (backspace) if reaching LM
                // On purpose here without nothing more inside
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (ReviewMode)
                {
                    // Exit review and return
                    ToggleReviewMode();
                    return;
                }

                if (lbMenu.Visible)
                {
                    HideMenu();
                }
                else
                {
                    StopTypeWriter(true);
                }
            }
            else if (ReviewMode)
            {
                // Refocus to take input (important)
                RefocusInput();
                // Prevent typing if in review mode
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
            {
                // If at end of line and not control or backspace was pressed
                if (!e.Control && e.KeyCode != Keys.Back && tbInput.Text.Length >= tbInput.MaxLength)
                {
                    DisplayMessage("End Of Line");
                }
            }
        }

        private void SetLogoFromDialog()
        {
            // Set logo
            InputPrompt logoDialog = new InputPrompt("Logo", "", "", "Set Logo (1-" + MaximumLogoLength + " chars)", MaximumLogoLength, false, "Save", false, null, null, false);
            logoDialog.TopMost = true;
            logoDialog.ShowDialog();

            if (!logoDialog.ActionCancelled)
            {
                SetLogoText(logoDialog.UserInput);
                DisplayMessage("Logo set");
            }
        }

        private void RefocusInput()
        {
            tbInput.Focus();
        }


        private bool tempPageWasAutoScrolling;
        private void ToggleReviewMode()
        {
            ReviewMode = !ReviewMode;
            if (ReviewMode)
            {
                tempPageWasAutoScrolling = PageAutoScroll;
                SaveLineFeed(true);
                if (tempPageWasAutoScrolling)
                {
                    PageAutoScroll = false;
                }
                HideBasket();
                DisplayMessage("Review ON");
            }
            else
            {
                RecallLineFeed(true);
                if (tempPageWasAutoScrolling)
                {
                    PageAutoScroll = true;
                }
                ShowBasket();
                DisplayMessage("Review OFF");
            }
            UpdateInformationBar();
            RecalculateOutput();
            RefocusInput();
        }

        private void ShowBasket()
        {
            ReviewMode = false;
            pnlInput.Visible = true;
            lbLogo.Visible = true;
            pnlBasketPosition.Visible = true;
            pnlPresets.Visible = true;
            RefocusInput();
        }

        private void HideBasket()
        {
            ReviewMode = true;
            pnlInput.Visible = false;
            lbLogo.Visible = false;
            pnlBasketPosition.Visible = false;
            pnlPresets.Visible = false;
            RefocusInput();
        }


        private void ToggleMenu(bool jumpToPage, int pageNumber)
        {
            if (!lbMenu.Visible)
            {
                // Load instruction card
                LoadInstructionCard();
                // Set label size according to content (before: //lbMenu.Size = new Size(425, 710);)
                // Now using AutoSize
                lbMenu.AutoSize = true;
                // Set text align
                lbMenu.TextAlign = ContentAlignment.MiddleCenter;
                // Load text
                if (jumpToPage)
                {
                    SetInstructionCardPage(pageNumber);
                }
                else
                {
                    SetInstructionCard();
                }
                // Place menu in center and show
                lbMenu.Anchor = AnchorStyles.None;
                lbMenu.Location = new Point(((lbMenu.Parent.ClientSize.Width / 2) - (lbMenu.Width / 2)),
                                            ((lbMenu.Parent.ClientSize.Height / 2) - (lbMenu.Height / 2)));
                lbMenu.Refresh();
                lbMenu.Visible = true;
                lbMenu.BringToFront();
                RefocusInput();
            }
            else
            {
                HideMenu();
                RefocusInput();
            }
        }

        private string tempPlaceholder;
        private string tempInstructionCardPageA;
        private string tempInstructionCardPageB;
        private string tempInstructionCardPageC;
        private string tempInstructionCardPageD;
        private void LoadInstructionCard()
        {
            // Load pages
            tempInstructionCardPageA = IO.TypewriterInstructionsTextReader(0);
            tempInstructionCardPageB = IO.TypewriterInstructionsTextReader(1);
            tempInstructionCardPageC = IO.TypewriterInstructionsTextReader(2);
            tempInstructionCardPageD = IO.TypewriterInstructionsTextReader(3);
            // Replace placeholder text
            // (Blank space first is important)
            tempPlaceholder = " " + GetAutoShowMenuByLongName();
            tempInstructionCardPageA = tempInstructionCardPageA.Replace("[replacethis]", tempPlaceholder + TextTransform.CalculateSpacesFromStringToGivenLength(tempPlaceholder, 48));
        }


        private void SetInstructionCardPage(int pageNumber)
        {
            if (pageNumber >= 0 && pageNumber <= 3)
            {
                InstructionCardPage = pageNumber;
                SetInstructionCard();
            }
        }


        private void SetInstructionCard()
        {

            if (InstructionCardPage == 0)
            {
                lbMenu.Text = tempInstructionCardPageA;
            }
            else if (InstructionCardPage == 1)
            {
                lbMenu.Text = tempInstructionCardPageB;
            }
            else if (InstructionCardPage == 2)
            {
                lbMenu.Text = tempInstructionCardPageC;
            }
            else if (InstructionCardPage == 3)
            {
                lbMenu.Text = tempInstructionCardPageD;
            }
            RefocusInput();
        }

        private void FlipInstructionCard(bool reverse)
        {
            // Load text
            if (reverse)
            {
                if (InstructionCardPage > 0)
                {
                    InstructionCardPage -= 1;
                }
                else
                {
                    InstructionCardPage = 3;
                }
            }
            else
            {
                if (InstructionCardPage < 3)
                {
                    InstructionCardPage += 1;
                }
                else
                {
                    InstructionCardPage = 0;
                }
            }

            SetInstructionCard();
        }

        private void HideMenu()
        {
            lbMenu.Visible = false;
        }


        private void ResetScroll()
        {
            if (!ReviewMode && PageAutoScroll)
            {
                DisplayMessage(MessageText.LockedByAutoScrollTypeWriterMessage());
                return;
            }

            // Reset to user specified scroll if in review mode
            if (ReviewMode)
            {
                RecallLineFeed(true);
                RecalculateOutput();
                RefocusInput();
            }
            else
            {
                SetFontSize(CurrentFontSize); // This will trigger standard scroll position
                UpdateInformationBar();
                SettingsChanged = true;
                RecalculateOutput();
                SubmitContent();
            }
        }


        /*
            1) LM = 0-5 (4-20 spaces)
            2) CPL Index = 0-5
            3) Bell = 3-12
            4) Font S/M/L = 0-2
            5) ColorTheme = 0-8 (0 = Parent theme, 1 = Red, 2 = Green, 3 = Black, 4 = Gray, 
                              5 = Red/Dark, 6 = Green/Dark, 7 =  Black/Dark, 8 = Gray/Dark)
        */
        private void SetPreset(int preset)
        {
            switch (preset)
            {
                case 0: // 46-CPL Draft (Default)
                    ChangeLayout(DefaultLeftMargin, DefaultCPLIndex, DefaultBellMargin, DefaultFontSize, DefaultColorTheme);
                    break;
                case 1: // 24-CPL Poetry
                    ChangeLayout(4, 0, 4, 2, 2);
                    break;
                case 2: // 38-CPL Fable
                    ChangeLayout(4, 1, 5, 1, 2);
                    break;
                case 3: // 55-CPL Script
                    ChangeLayout(3, 2, 6, 0, 1);
                    break;
                case 4: // 64-CPL Short-story
                    ChangeLayout(2, 3, 6, 0, 3);
                    break;
                case 5: // 72-CPL Fiction
                    ChangeLayout(2, 4, 6, 0, 8);
                    break;
                case 6: // 83-CPL Report
                    ChangeLayout(1, 5, 6, 0, 6);
                    break;
            }
            // Display message
            DisplayMessage(GetPresetTitle(preset));
        }

        private string GetPresetTitle(int preset)
        {
            switch (preset)
            {
                case 0:
                    return "Preset: 46-CPL Draft"; // CTRL + 1
                case 1:
                    return "Preset: 24-CPL Poetry"; // CTRL + 2
                case 2:
                    return "Preset: 38-CPL Fable"; // CTRL + 3
                case 3:
                    return "Preset: 55-CPL Script"; // CTRL + 4
                case 4:
                    return "Preset: 64-CPL Short-story"; // CTRL + 5
                case 5:
                    return "Preset: 72-CPL Fiction"; // CTRL + 6
                case 6:
                    return "Preset: 83-CPL Report"; // CTRL + 7
                default:
                    return "N/A";
            }
        }

        // Change preset
        private void ChangeLayout(int leftMargin, int cplIndex, int bellMargin, int fontSize, int colorTheme)
        {
            // Submit values
            LeftMargin = leftMargin;
            BellMargin = bellMargin;

            // CPL Index/Count
            CPLIndex = cplIndex;
            UpdateCPLCount();

            // Font size
            CurrentFontSize = fontSize;
            SetFontSize(CurrentFontSize);

            // Color theme
            ColorTheme = colorTheme;
            SetColorTheme(ColorTheme);

            // Basket
            ResetBasket();

            // Update values and save settings
            RemoveLeftMargin();
            SetRuler(); // Includes bell margin
            InsertLeftMargin();
            GenerateRulerCaret();
            // Recalculate all
            RecalculateOutput();
            AlignBasketIfAuto();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void ResetSettings()
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Reset settings?", MessageText.TypeWriterCaption(), false, false);
            msg.TopMost = true;
            msg.ShowDialog();

            if (msg.Accepted)
            {
                CurrentFontSize = DefaultFontSize;
                CurrentLineCount = DefaultLineCount;
                CPLIndex = DefaultCPLIndex;
                BellMargin = DefaultBellMargin;
                LeftMargin = DefaultLeftMargin;
                DelLock = DefaultDelLock;
                BasketAdjust = DefaultAdjustBasket;
                AutoMenu = DefaultAutoMenu;
                ShowTabRuler = DefaultTabRuler;
                AutoBasket = DefaultAutoBasket;
                PageAutoScroll = DefaultPageAutoScroll;
                ResetColorTheme();

                // Update info line
                RemoveLeftMargin();
                InsertLeftMargin();
                GenerateRulerCaret();
                SetFontSize(CurrentFontSize);
                UpdateCPLCount();
                UpdateInformationBar();
                SettingsChanged = true;
                RecalculateOutput();
                SubmitContent();
                ResolveBasketPositionAndPresetBitmap();
                ResetBasket();
                ResetLogo();
                ShowBasket();
                InstructionCardPage = 0; // Reset instruction card page
                LoadInstructionCard(); // Load instruction card text
                SetInstructionCard(); // Set instruction card text
                DisplayMessage("Settings reset");
            }
        }

        private void CycleFontSize(bool reverse)
        {
            if (reverse)
            {
                if (CurrentFontSize > 0)
                {
                    CurrentFontSize -= 1;
                }
                else
                {
                    CurrentFontSize = MaximumFontSize;
                }
            }
            else
            {
                if (CurrentFontSize < MaximumFontSize)
                {
                    CurrentFontSize += 1;
                }
                else
                {
                    CurrentFontSize = 0;
                }
            }

            SetFontSize(CurrentFontSize);
            // Recalculate all
            RecalculateOutput();
            AlignBasketIfAuto();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private string GetAutoShowMenuByName()
        {
            if (AutoMenu)
            {
                return "(A)";
            }
            else
            {
                return "";
            }
        }

        private string GetAutoShowMenuByLongName()
        {
            if (AutoMenu)
            {
                return "(AUTO)";
            }
            else
            {
                return "";
            }
        }

        private string GetColorThemeByName()
        {
            switch (ColorTheme)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "G";
                case 7:
                    return "H";
                case 8:
                    return "I";
                default:
                    return "";
            }
        }

        private string GetFontSizeByName()
        {
            switch (CurrentFontSize)
            {
                case 0:
                    return "S";
                case 1:
                    return "M";
                case 2:
                    return "L";
                default:
                    return "";
            }
        }

        private string GetAutoScrollByName()
        {
            if (PageAutoScroll)
            {
                return "AUTO";
            }
            else
            {
                return CurrentLineCount.ToString("000");
            }
        }

        private string GetDelLockByName()
        {
            if (DelLock)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        private string GetBasketLockByName()
        {
            if (AutoBasket)
            {
                return "AUTO";
            }
            else if (BasketAdjust)
            {
                return "Lock OFF";
            }
            else
            {
                return "Lock ON";
            }
        }

        private string GetReviewModeByName()
        {
            if (ReviewMode)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        private string GetTabSizeByName()
        {
            return GetTabsCount().ToString("00");
        }

        private int GetTabsCount()
        {
            return GetTabs(LeftMargin).Length;
        }

        private void SaveLineFeed(bool overrideReviewLock)
        {
            if (!ReviewMode || overrideReviewLock)
            {
                SavedScrollPosition = CurrentLineCount;
            }
        }

        private void RecallLineFeed(bool overrideReviewLock)
        {
            if (!ReviewMode || overrideReviewLock)
            {
                CurrentLineCount = SavedScrollPosition;
            }
        }

        private void CycleLineFeed(bool reverse)
        {
            if (PageAutoScroll)
            {
                DisplayMessage(MessageText.LockedByAutoScrollTypeWriterMessage());
                return;
            }

            if (reverse)
            {
                if (CurrentLineCount > MinimumLineCount + FeedNudge)
                {
                    CurrentLineCount -= FeedNudge;
                }
                else
                {
                    DisplayMessage(MessageText.LineStopTypeWriterMessage());
                }
            }
            else
            {
                if (CurrentLineCount < MaximumLineCount - FeedNudge)
                {
                    CurrentLineCount += FeedNudge;
                }
                else
                {
                    DisplayMessage(MessageText.LineStopTypeWriterMessage());
                }
            }

            // Recalculate all
            SaveLineFeed(false);
            RecalculateOutput();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
            RefocusInput();
        }

        private void CycleLineFeedJump(bool reverse)
        {
            if (PageAutoScroll)
            {
                DisplayMessage(MessageText.LockedByAutoScrollTypeWriterMessage());
                return;
            }

            if (reverse)
            {
                if (CurrentLineCount > MinimumLineCount + FeedJumpNudge)
                {
                    CurrentLineCount -= FeedJumpNudge;
                }
                else
                {
                    DisplayMessage(MessageText.LineStopTypeWriterMessage());
                }
            }
            else
            {
                if (CurrentLineCount < MaximumLineCount - FeedJumpNudge)
                {
                    CurrentLineCount += FeedJumpNudge;
                }
                else
                {
                    DisplayMessage(MessageText.LineStopTypeWriterMessage());
                }
            }

            // Recalculate all
            SaveLineFeed(false);
            RecalculateOutput();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
            RefocusInput();
        }

        // Skal saves i settings
        private void CycleLeftMargin(bool reverse)
        {
            if (reverse)
            {
                if (LeftMargin > 0) // Steps 0 to 5
                {
                    LeftMargin -= 1;
                }
            }
            else
            {

                if (LeftMargin < MaximumLeftMargin) // Steps 0 to 5
                {
                    LeftMargin += 1;
                }
            }
            RemoveLeftMargin();
            SetRuler();
            UpdateInformationBar();
            InsertLeftMargin();
            GenerateRulerCaret();
            SettingsChanged = true;
            SubmitContent();
        }

        private void InsertLeftMargin()
        {
            // Remove any tabs
            tbInput.Text = RemoveTabs(tbInput.Text);
            if (LeftMargin > 0)
            {
                // Set tabs
                tbInput.Text = GetTabs(LeftMargin) + tbInput.Text;
            }
            // Move caret to end
            tbInput.Select(tbInput.Text.Length, 0);
        }

        private void RemoveLeftMargin()
        {
            // Remove any tabs
            tbInput.Text = RemoveTabs(tbInput.Text);
        }

        private void ToggleAutoMenu()
        {
            AutoMenu = !AutoMenu;

            if (!AutoMenu)
            {
                DisplayMessage("Auto-Menu OFF");
            }
            else
            {
                DisplayMessage("Auto-Menu ON");
            }

            LoadInstructionCard();
            SetInstructionCard();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void ToggleTabRuler()
        {
            ShowTabRuler = !ShowTabRuler;

            if (!ShowTabRuler)
            {
                DisplayMessage("Tab Ruler OFF");
            }
            else
            {
                DisplayMessage("Tab Ruler ON");
            }

            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void ToggleLockDelete()
        {
            DelLock = !DelLock;

            if (!DelLock)
            {
                DisplayMessage("Del Lock OFF");
            }
            else
            {
                DisplayMessage("Del Lock ON");
            }

            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void ToggleBasketLock()
        {
            if (AutoBasket)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Disable Basket Auto-Align?", MessageText.TypeWriterCaption(), false, false);
                msg.TopMost = true;
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    ToggleBasketAutoAlignment();
                }
                else
                {
                    return;
                }
            }

            BasketAdjust = !BasketAdjust;

            if (BasketAdjust) // Must be like this for OFF
            {
                DisplayMessage("Basket Lock OFF");
            }
            else
            {
                DisplayMessage("Basket Lock ON");
            }

            ResolveBasketPositionAndPresetBitmap();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }


        private void ToggleBasketAutoAlignment()
        {
            AutoBasket = !AutoBasket;

            if (AutoBasket)
            {
                DisplayMessage("Basket AUTO");
                AlignBasketCenter();
            }
            else
            {
                DisplayMessage("Basket MANUAL");
            }

            ResolveBasketPositionAndPresetBitmap();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void CycleBellMargin(bool reverse)
        {
            if (reverse)
            {
                if (BellMargin > MinimumBellMargin)
                {
                    BellMargin -= 1;
                }
                else
                {
                    BellMargin = MaximumBellMargin;
                }
            }
            else
            {
                if (BellMargin < MaximumBellMargin)
                {
                    BellMargin += 1;
                }
                else
                {
                    BellMargin = MinimumBellMargin; // Can not be les than 3 because the bell will cling-out 2 before EOL
                }
            }
            // Update info line
            SetRuler();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }


        // Skal saves i settings
        private void CycleCPLCount(bool reverse)
        {
            if (reverse)
            {
                if (CPLIndex > 0)
                {
                    CPLIndex -= 1;
                }
            }
            else
            {
                if (CPLIndex < 5)
                {
                    CPLIndex += 1;
                }
            }
            UpdateCPLCount();
        }


        private void UpdateCPLCount()
        {
            // Set CPL Count
            CPLCount = GetCPLCount();
            // Set max char length
            tbInput.MaxLength = CPLCount;
            SetRuler();
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        // Define CPL - Characters Per Line (40, 54, 67, 72, 80, 87)
        private int GetCPLCount()
        {
            switch (CPLIndex)
            {
                case 0:
                    return 40;
                case 1:
                    return 54;
                case 2:
                    return 67;
                case 3:
                    return 72;
                case 4:
                    return 80;
                case 5:
                    return 87;
                default:
                    return 67;
            }
        }

        private void CycleColorTheme(bool reverse)
        {
            if (reverse)
            {
                if (ColorTheme > 0)
                {
                    ColorTheme -= 1;
                }
                else
                {
                    ColorTheme = 8;
                }
            }
            else
            {
                if (ColorTheme < 8)
                {
                    ColorTheme += 1;
                }
                else
                {
                    ColorTheme = 0;
                }
            }
            SetColorTheme(ColorTheme);
            UpdateInformationBar();
            SettingsChanged = true;
            SubmitContent();
        }

        private void ResetColorTheme()
        {
            ColorTheme = DefaultColorTheme;
            SetColorTheme(ColorTheme);
        }


        // Submit input to content list
        private void SubmitContentToList(string input, bool isLineFeed)
        {
            InputList.Add(new TWString(input, true, isLineFeed, false));
            if (!IgnoreChanges)
            {
                GenerateContentFromInput();
            }
        }

        private void RemoveLastLineFromList()
        {
            if (InputList.Count > 0)
            {
                if (InputList[InputList.Count - 1].TopLine)
                {
                    DisplayMessage(MessageText.LineStopTypeWriterMessage());
                }
                else
                {
                    InputList.RemoveAt(InputList.Count - 1);
                    RecalculateOutput();
                }
            }
        }

        private void InsertCarriageReturn()
        {
            if (InputList != null)
            {
                InputList.Add(new TWString("", false, false, false));
                RecalculateOutput();
            }
        }

        // Run through input list and collect strings
        // into one variable
        private void GenerateContentFromInput()
        {
            if (InputList.Count > 0)
            {
                OutputSummary = "";
                for (int i = 0; i < InputList.Count; i++)
                {
                    if (i == InputList.Count - 1)// If last line, don't add a NewLine
                    {
                        if (!IsLineFeed(InputList[i]))
                        {
                            OutputSummary += InputList[i].Content;
                        }
                    }
                    else
                    {
                        if (!IsLineFeed(InputList[i]))
                        {
                            OutputSummary += InputList[i].Content + Environment.NewLine;
                        }
                    }
                }
                // Submit content to main form
                SubmitContent();
            }
        }

        private void SubmitContent()
        {
            if (!IgnoreSettingsChanged)
            {
                // Invoke main form
                MainForm.KeepAwake();
                MainForm.TypeWriterFontSize = CurrentFontSize;
                MainForm.TypeWriterLineCount = CurrentLineCount;
                MainForm.TypeWriterCPLIndex = CPLIndex;
                MainForm.TypeWriterBellMargin = BellMargin;
                MainForm.TypeWriterLeftMargin = LeftMargin;
                MainForm.TypeWriterDelLock = DelLock;
                MainForm.TypeWriterLogo = LogoText;
                MainForm.TypeWriterBasketX = GetBasketX();
                MainForm.TypeWriterBasketY = GetBasketY();
                MainForm.TypeWriterAutoMenu = AutoMenu;
                MainForm.TypeWriterTabRuler = ShowTabRuler;
                MainForm.TypeWriterColorTheme = ColorTheme;
                MainForm.TypeWriterAutoBasket = AutoBasket;
                MainForm.TypeWriterPageAutoScroll = PageAutoScroll;
                if (SettingsChanged)
                {
                    SettingsChanged = false;
                    MainForm.SettingsHasChanges = true;
                }
                MainForm.SubmitTypeWriterContent = true;
            }
        }

        public string GetOutputSummary()
        {
            return OutputSummary;
        }

        #region OUTPUT MECHANICS
        // The global timer, responsible for setting the output
        // visuals in action...
        private void timerGlobalTimer_Tick(object sender, EventArgs e)
        {
            if (!IgnoreChanges)
            {
                // Check if there is any changes to the input list
                if (InputList.Count != PreviousListCount)
                {
                    // Update list count and write out values
                    PreviousListCount = InputList.Count;
                    WriteOutput();
                }
            }
        }

        private bool FoundNewStrings;
        private void WriteOutput()
        {
            FoundNewStrings = false;
            // Add new items to the output list
            for (int i = 0; i < InputList.Count; i++)
            {
                if (InputList[i].NewContent)
                {
                    FoundNewStrings = true;
                    InputList[i].NewContent = false;
                    if (InputList[i].TopLine)
                    {
                        OutputList.Add(TopLineSymbol + Environment.NewLine);
                    }
                    else
                    {
                        OutputList.Add(InputList[i].Content + Environment.NewLine);
                    }
                }
            }

            // Truncate output list to a maximum of MaxLines
            TruncateOutputList();

            // React on result
            if (FoundNewStrings)
            {
                // Clear and write out all but the last line
                if (OutputList.Count > 1)
                {
                    lbOutput.Text = "";
                    for (int i = 0; i < OutputList.Count - 1; i++)
                    {
                        lbOutput.Text += OutputList[i];
                    }
                }
                WriteLastStringAnimated();
            }
            else
            {
                timerGlobalTimer.Enabled = true;
            }
        }

        private void WriteLastStringAnimated()
        {
            AnimationRunning = true;
            CharCounter = 0;
            ListIndexCounter = 0;
            EndOfLine = false;
            ListIndexCounter = OutputList.Count - 1;
            CalculateAnimationSpeed(OutputList[OutputList.Count - 1]);
            timerDisplayWriter.Enabled = true;
        }

        // Control char output speed according to text length
        private void CalculateAnimationSpeed(string text)
        {
            if (text.Length < 16)
            {
                timerDisplayWriter.Interval = 50;
            }
            else if (text.Length >= 16 && text.Length < 30)
            {
                timerDisplayWriter.Interval = 30;
            }
            else if (text.Length >= 30)
            {
                timerDisplayWriter.Interval = 10;
            }
        }


        #region CARET
        private void InsertCaret()
        {
            lbOutput.Text = lbOutput.Text.Replace(Caret, "");
            lbOutput.Text += Caret;
        }

        private void InsertRulerCaret()
        {
            if (!AnimationRunning && !ReviewMode && !ContainsRulerCaret())
            {
                RulerCaretInserted = true;
                // Remove old rulercaret
                lbOutput.Text = lbOutput.Text.Replace(RulerCaret, "");
                // Remove old caret
                lbOutput.Text = lbOutput.Text.Replace(Caret, "");
                // Insert rulercaret
                lbOutput.Text += RulerCaret;
            }
        }

        private void RemoveRulerCaret()
        {
            if (!AnimationRunning && ContainsRulerCaret())
            {
                RulerCaretInserted = false;
                // Remove rulercaret
                lbOutput.Text = lbOutput.Text.Replace(RulerCaret, "");
                // Remove caret
                lbOutput.Text = lbOutput.Text.Replace(Caret, "");
                // Insert caret
                lbOutput.Text += Caret;
            }
        }

        private string tempRulerCaret;
        private void GenerateRulerCaret()
        {

            tempRulerCaret = "";
            if (LeftMargin > 0)
            {
                for (int i = 0; i < LeftMargin; i++)
                {
                    if (i == LeftMargin - 1)
                    {
                        tempRulerCaret += "''''";
                    }
                    else
                    {
                        tempRulerCaret += "'''|";
                    }
                }
            }
            else
            {
                tempRulerCaret = "''''";
            }

            if (tempRulerCaret.Length > 0)
            {
                RulerCaret = tempRulerCaret;
            }
        }

        private bool ContainsRulerCaret()
        {
            if (lbOutput.Text.Contains(RulerCaret))
            {
                RulerCaretInserted = true;
                return true;
            }
            else
            {
                RulerCaretInserted = false;
                return false;
            }
        }
        #endregion


        private int CharCounter;
        private int ListIndexCounter;
        private bool EndOfLine;
        private void timerDisplayWriter_Tick(object sender, EventArgs e)
        {
            if (OutputList.Count > 0)
            {
                if (CharCounter <= OutputList[ListIndexCounter].Length - 1)
                {
                    lbOutput.Text += OutputList[ListIndexCounter][CharCounter];
                    CharCounter += 1;
                }
                else
                {
                    EndOfLine = true;
                }

                // Insert caret
                InsertCaret();

                // Disable timer when end of list
                if (EndOfLine)
                {
                    // Disable this timer and start the global timer
                    timerDisplayWriter.Enabled = false;
                    AnimationRunning = false;
                    timerGlobalTimer.Enabled = true;
                }
            }
        }
        #endregion



        #region MOVE TYPEBASKET
        private void MoveTypeBasketUp(int speedFactor)
        {
            if (speedFactor >= 1)
            {
                SetBasket(pnlInput.Location.X, pnlInput.Location.Y - (TypeBasketNudge * speedFactor));
            }
        }
        private void MoveTypeBasketDown(int speedFactor)
        {
            if (speedFactor >= 1)
            {
                SetBasket(pnlInput.Location.X, pnlInput.Location.Y + (TypeBasketNudge * speedFactor));
            }
        }
        private void MoveTypeBasketLeft(int speedFactor)
        {
            if (speedFactor >= 1)
            {
                SetBasket(pnlInput.Location.X - (TypeBasketNudge * speedFactor), pnlInput.Location.Y);
            }
        }
        private void MoveTypeBasketRight(int speedFactor)
        {
            if (speedFactor >= 1)
            {
                SetBasket(pnlInput.Location.X + (TypeBasketNudge * speedFactor), pnlInput.Location.Y);
            }
        }

        private void SetBasket(int x, int y)
        {
            if (!BasketAdjust && AutoBasket)
            {
                AlignBasketCenter();
            }
            else if ((x >= -500 && x <= 50000) && (y >= -500 && y <= 50000))
            {
                pnlInput.Anchor = AnchorStyles.None;
                pnlInput.Left = x;
                pnlInput.Top = y;
                pnlInput.Anchor = AnchorStyles.Bottom;
                CenterLogo();
                SettingsChanged = true;
                SubmitContent();
                UpdateInformationBar();
            }
        }

        private void AlignBasketIfAuto()
        {
            if (!BasketAdjust && AutoBasket)
            {
                AlignBasketCenter();
            }
        }

        private void ResetBasket()
        {
            AlignBasketCenter();
        }

        private int YOffset;
        private void AlignBasketCenter()
        {
            // Get distance from top of form with offset
            YOffset = ((pnlInput.Parent.Height / 2) - 135);
            // Place panel
            pnlInput.Anchor = AnchorStyles.None;
            pnlInput.Location = new Point(((pnlInput.Parent.ClientSize.Width / 2) - (pnlInput.Width / 2)),
                                        ((pnlInput.Parent.ClientSize.Height / 2) - (pnlInput.Height / 2)) + YOffset);
            pnlInput.Anchor = AnchorStyles.Bottom;
            pnlInput.Refresh();

            // Do the rest
            DefaultTypeBasketY = pnlInput.Top;
            CenterLogo();
            ResolveBasketPositionAndPresetBitmap();
            UpdateInformationBar();
        }

        private int GetBasketX()
        {
            return pnlInput.Left;
        }

        private int GetBasketY()
        {
            return pnlInput.Top;
        }
        #endregion


        #region Printing (addition 26-02-2019)
        private void Print()
        {
            // Print content
            if (lbOutput.Text.Length > 0)
            {
                Printing.PrintText(DocumentTitle, GetOutputSummary().TrimEnd(new char[1] { '\n' }), 1); // Courier New
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("No characters printed", MessageText.PrintCaption(), false);
                msg.ShowDialog();
            }
        }
        #endregion

    }

    // Declare the TypeWriterString/TWString
    class TWString
    {
        public string Content { get; set; }
        public bool NewContent { get; set; }
        public bool LineFeed { get; set; }
        public bool TopLine { get; set; }

        public TWString(string content, bool newContent, bool lineFeed, bool topLine)
        {
            Content = content;
            NewContent = newContent;
            LineFeed = lineFeed;
            TopLine = topLine;
        }
    }
}
