using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser.Typermax
{
    public partial class Typermax : Form
    {
        // Typermax v1.03 by LBS, August 2017
        // Typermax v1.04 by LBS, December 2017 (Minor changes)
        // Typermax v1.05 by LBS, March 2018 (Added super phrase)
        // Typermax v1.06 by LBS, April 2018 (Added version display, details, logo and adjusted gameplay + Training mode)
        // Typermax v1.07 by LBS, June 2018 (Added more/different number/char combinations)
        // Typermax v1.08 by LBS, November 2019 (pause game when notifications are snown, added history display and penalty highlight-blink)

        #region Variable declaration
        private int Credits { get; set; }
        private const int DefaultCredits = 3;
        private const string TypermaxVersion = "v1.08";
        private int Level { get; set; }
        private int HighScore { get; set; }
        private int Score { get; set; } // Score skal tælle på antal completed ord * level (10*1 osv.)
        private int WordsCompleted { get; set; }
        private int WordsPostedInOnePhrase { get; set; }
        private string TargetPhrase { get; set; }
        private int PhraseCount { get; set; }
        private int TopWordValue { get; set; }
        private int CurrentWPMCount { get; set; }
        private const string BarCharacter = "█";
        private const string AlternateBarCharacter = "░";
        private const string BarEndCharacter = "|";
        private const int MaxBarLength = 30;
        private const int WordScoreValue = 10; // Base word value
        private const int LevelBonus = 1000;
        private int GameMode { get; set; } // 0 = stop, 1 = running, 2 = paused...
        private bool GameWasReset { get; set; }
        private List<string> DictionaryList { get; set; }
        private List<string> FourLetterWords { get; set; }
        private List<string> FiveLetterWords { get; set; }
        private List<string> SixLetterWords { get; set; }
        private List<string> SevenLetterWords { get; set; }
        private List<string> EightLetterWords { get; set; }
        private List<string> NineLetterWords { get; set; }
        private List<string> TenLetterWords { get; set; }
        private List<string> ElevenLetterWords { get; set; }
        private List<string> TwelveLetterWords { get; set; }
        private List<string> ThirteenLetterWords { get; set; }
        private List<string> FourteenLetterWords { get; set; }
        private List<string> FifteenLetterWords { get; set; }
        private List<string> SixteenLetterWords { get; set; }
        private List<string> SeventeenLetterWords { get; set; }
        private List<string> EighteenLetterWords { get; set; }

        private List<int> PreviousValue4 { get; set; }
        private List<int> PreviousValue5 { get; set; }
        private List<int> PreviousValue6 { get; set; }
        private List<int> PreviousValue7 { get; set; }
        private List<int> PreviousValue8 { get; set; }
        private List<int> PreviousValue9 { get; set; }
        private List<int> PreviousValue10 { get; set; }
        private List<int> PreviousValue11 { get; set; }
        private List<int> PreviousValue12 { get; set; }
        private List<int> PreviousValue13 { get; set; }
        private List<int> PreviousValue14 { get; set; }
        private List<int> PreviousValue15 { get; set; }
        private List<int> PreviousValue16 { get; set; }
        private List<int> PreviousValue17 { get; set; }
        private List<int> PreviousValue18 { get; set; }

        private const string CreditsString = "CREDITS: ";
        private const string ScoreString = "SCORE: ";
        private const string HighScoreString = "HIGH: ";
        private const string WordValueString = "CURRENT WORD VALUE: ";
        private const string LevelString = "LEVEL: ";
        private const string WPMString = " WPM";
        private string SuperPhrase1 { get; set; }
        private string SuperPhrase2 { get; set; }
        private string SuperPhrase3 { get; set; }
        private string SuperPhrase4 { get; set; }
        private string SuperPhrase5 { get; set; }
        private string SuperPhrase6 { get; set; }
        private bool SuperPhrase { get; set; }
        private string InitialPhrase { get; set; }
        private bool RunInitialPhrase { get; set; }
        private int SuperPhrase1TimeBonus { get; set; }
        private int SuperPhrase2TimeBonus { get; set; }
        private int SuperPhrase3TimeBonus { get; set; }
        private int SuperPhrase4TimeBonus { get; set; }
        private int SuperPhrase5TimeBonus { get; set; }
        private int SuperPhrase6TimeBonus { get; set; }
        private int SuperPhrase1WordCount { get; set; }
        private int SuperPhrase2WordCount { get; set; }
        private int SuperPhrase3WordCount { get; set; }
        private int SuperPhrase4WordCount { get; set; }
        private int SuperPhrase5WordCount { get; set; }
        private int SuperPhrase6WordCount { get; set; }
        private int InitialPhraseWordCount { get; set; }
        private int InitialPhraseTimeBonus { get; set; }
        private int SuperPhraseCounter { get; set; }
        private int CurrentSuperPhrase { get; set; }
        private const string DefaultErrorLabelTest = "Ready";
        private const string DefaultTestModeErrorLabelTest = "Training";
        private bool TestMode { get; set; }

        // Summary
        private decimal SuccessfullKeystrokes { get; set; }
        private decimal FailedKeystrokes { get; set; }
        private decimal TotalTimePenalty { get; set; }
        private const int PrimaryPenalty = 10; // Subtract 10%
        private const int SubsequentPenalty = 2; // Subtract 2%
        // Standard text
        private const string TrainingModeText = "TRAINING MODE";
        private const int TrainingModeTimeout = 500;
        #endregion

        #region Constructor
        public Typermax(int highscore)
        {
            InitializeComponent();
            // Set test mode
            TestMode = false;
            // Form
            InitializeSpeedTimer();
            DictionaryList = new List<string>();
            FourLetterWords = new List<string>();
            FiveLetterWords = new List<string>();
            SixLetterWords = new List<string>();
            SevenLetterWords = new List<string>();
            EightLetterWords = new List<string>();
            NineLetterWords = new List<string>();
            TenLetterWords = new List<string>();
            ElevenLetterWords = new List<string>();
            TwelveLetterWords = new List<string>();
            ThirteenLetterWords = new List<string>();
            FourteenLetterWords = new List<string>();
            FifteenLetterWords = new List<string>();
            SixteenLetterWords = new List<string>();
            SeventeenLetterWords = new List<string>();
            EighteenLetterWords = new List<string>();

            PreviousValue4 = new List<int>();
            PreviousValue5 = new List<int>();
            PreviousValue6 = new List<int>();
            PreviousValue7 = new List<int>();
            PreviousValue8 = new List<int>();
            PreviousValue9 = new List<int>();
            PreviousValue10 = new List<int>();
            PreviousValue11 = new List<int>();
            PreviousValue12 = new List<int>();
            PreviousValue13 = new List<int>();
            PreviousValue14 = new List<int>();
            PreviousValue15 = new List<int>();
            PreviousValue16 = new List<int>();
            PreviousValue17 = new List<int>();
            PreviousValue18 = new List<int>();

            mqLogo.SetTextAlignment(ContentAlignment.MiddleCenter);
            mqLogo.SetFontAndColors(Design.ContentFieldFont(), AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1], AhoyColors.CurrentColorTheme()[3], AhoyColors.CurrentColorTheme()[1]);

            // Place ruler behind textboxes
            lbRuler.Text = "";
            lbRuler.SendToBack();
            lbTarget.BringToFront();
            lbInput.BringToFront();
            ResetValues();
            HighScore = highscore;
            mqLogo.SetTestMode(false);
            mqLogo.SetDirectionalChar(false);
            mqLogo.SetPauseDuration(100);
            mqLogo.ForeColor = Color.Black;

            GameWasReset = false;
            SetErrorLabel(DefaultErrorLabelTest);

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;

            // Super Phrases
            SuperPhrase1 = "";
            SuperPhrase2 = "";
            SuperPhrase3 = "";
            SuperPhrase4 = "";
            SuperPhraseCounter = 0;
            CurrentSuperPhrase = 0;
            SuperPhrase = false;
            // Must be set after clearing the super phrase properties
            SetGameMode();
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
        #endregion

        #region Color theme
        private void SetColorTheme()
        {
            // Form
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            // Mask panel
            pnlRulerBottomMask.BackColor = AhoyColors.CurrentColorTheme()[0];
            ResolveTexture();

            mqLogo.SetFontAndColors(Design.FieldFont(1), AhoyColors.CurrentColorTheme()[30], AhoyColors.CurrentColorTheme()[29], AhoyColors.CurrentColorTheme()[30], AhoyColors.CurrentColorTheme()[29]);

            // Tooltip
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];

            // Buttons
            btnStartStop.FlatStyle = FlatStyle.Flat;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnHelp.FlatStyle = FlatStyle.Flat;

            btnStartStop.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[30];
            btnExit.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[30];
            btnHelp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[30];

            btnStartStop.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[29];
            btnExit.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[29];
            btnHelp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[29];

            btnStartStop.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[6];
            btnExit.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[6];
            btnHelp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[6];

            btnStartStop.BackColor = AhoyColors.CurrentColorTheme()[29];
            btnStartStop.ForeColor = AhoyColors.CurrentColorTheme()[30];

            btnExit.BackColor = AhoyColors.CurrentColorTheme()[29];
            btnExit.ForeColor = AhoyColors.CurrentColorTheme()[30];

            btnHelp.BackColor = AhoyColors.CurrentColorTheme()[29];
            btnHelp.ForeColor = AhoyColors.CurrentColorTheme()[30];

            // Textboxes
            tbTarget.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbTarget.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbHistory.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbHistory.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbRuler.BackColor = this.BackColor;
            lbRuler.ForeColor = AhoyColors.CurrentColorTheme()[30];


            // Labels
            lbSplash.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbSplash.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbVersionNumber.BackColor = Color.Transparent;
            lbVersionNumber.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbErrorDisplay.BackColor = Color.Transparent;
            lbErrorDisplay.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbCountDownSpeed.BackColor = Color.Transparent;
            lbCountDownSpeed.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbScore.BackColor = Color.Transparent;
            lbScore.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbHighScore.BackColor = Color.Transparent;
            lbHighScore.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbLevelLabel.BackColor = Color.Transparent;
            lbLevelLabel.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbLevelDisplay.BackColor = Color.Transparent;
            lbLevelDisplay.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbLevelMeter.BackColor = Color.Transparent;
            lbLevelMeter.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbWPMLabel.BackColor = Color.Transparent;
            lbWPMLabel.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbWPMDisplay.BackColor = Color.Transparent;
            lbWPMDisplay.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbWPMMeter.BackColor = Color.Transparent;
            lbWPMMeter.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbTimeMeter.BackColor = Color.Transparent;
            lbTimeMeter.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbPercentageDisplay.BackColor = Color.Transparent;
            lbPercentageDisplay.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbCredits.BackColor = Color.Transparent;
            lbCredits.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbTarget.BackColor = Color.Transparent;
            lbTarget.ForeColor = AhoyColors.CurrentColorTheme()[30];

            lbInput.BackColor = Color.Transparent;
            lbInput.ForeColor = AhoyColors.CurrentColorTheme()[30];


            // Fonts
            tbTarget.Font = Design.TypermaxFieldFont();
            tbInput.Font = Design.TypermaxFieldFont();
            tbHistory.Font = Design.TypermaxFieldFont();
            lbRuler.Font = Design.TypermaxFieldFont();

            lbSplash.Font = Design.TitleFieldFont();
            lbVersionNumber.Font = Design.TypermaxXSSmallFont();
            lbVersionNumber.Text = TypermaxVersion;
            lbCountDownSpeed.Font = Design.MessageboxXSmallFont();
            lbErrorDisplay.Font = Design.MessageboxSmallFont();

            lbScore.Font = Design.TitleFieldFont();
            lbHighScore.Font = Design.TitleFieldFont();
            lbLevelLabel.Font = Design.TitleFieldFont();
            lbLevelDisplay.Font = Design.TitleFieldFont();
            lbLevelMeter.Font = Design.TitleFieldFont();
            lbWPMLabel.Font = Design.TitleFieldFont();

            lbWPMDisplay.Font = Design.TitleFieldFont();
            lbWPMMeter.Font = Design.TitleFieldFont();
            lbTimeMeter.Font = Design.TitleFieldFont();
            lbPercentageDisplay.Font = Design.TitleFieldFont();

            lbCredits.Font = Design.TitleFieldFont();
            lbTarget.Font = Design.TypermaxFieldFont();
            lbInput.Font = Design.TypermaxFieldFont();

            btnExit.Font = Design.TitleFieldFont();
            btnHelp.Font = Design.TitleFieldFont();
            btnStartStop.Font = Design.TitleFieldFont();
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
        #endregion


        #region Prepare dictionary
        private void InitializeInitialPhrase()
        {
            try
            {
                InitialPhrase = IO.TypermaxPhraseTextReader(0);
                InitialPhraseWordCount = InitialPhrase.Split(' ').Length;
            }
            catch
            {
                InitialPhrase = "Error";
            }
        }

        private void InitializeSuperPhrases()
        {
            try
            {
                // Load-index definitions:
                //-----------------------------
                // 1 = Typewriter info
                // 2 = Touch typing info
                // 3 = Typwriter info (Hansen writing ball)
                // 4 = Typewriting facts
                // 5 = Roman numerals in use
                // 6 = Roman numeral basics

                // Get prases (LOAD PHRASES INTO THE PHRASE PROPERTIES)
                SuperPhrase1 = IO.TypermaxPhraseTextReader(1); // 1 = Typewriter info (45 chars)
                SuperPhrase2 = IO.TypermaxPhraseTextReader(2); // 2 = Touch typing info (39 chars)
                SuperPhrase3 = IO.TypermaxPhraseTextReader(6); // 6 = Roman numeral basics (79 chars)
                SuperPhrase4 = IO.TypermaxPhraseTextReader(5); // 5 = Roman numerals in use (132 chars)
                SuperPhrase5 = IO.TypermaxPhraseTextReader(3); // 3 = Typwriter info (Hansen writing ball) (41 chars)
                SuperPhrase6 = IO.TypermaxPhraseTextReader(4); // 4 = Typewriting facts (44 chars)
                // Get word count
                SuperPhrase1WordCount = SuperPhrase1.Split(' ').Length;
                SuperPhrase2WordCount = SuperPhrase2.Split(' ').Length;
                SuperPhrase3WordCount = SuperPhrase3.Split(' ').Length;
                SuperPhrase4WordCount = SuperPhrase4.Split(' ').Length;
                SuperPhrase5WordCount = SuperPhrase5.Split(' ').Length;
                SuperPhrase6WordCount = SuperPhrase6.Split(' ').Length;

                // Upon completing the challenges add time bonuses (ADJUSTMENTS TO TIME BONUS)
                if (TestMode)
                {
                    InitialPhraseTimeBonus = TrainingModeTimeout * 5; // +2500%
                }
                else
                {
                    InitialPhraseTimeBonus = 50; // 100%
                }
                SuperPhrase1TimeBonus = SuperPhrase1WordCount * 6; // +270% // 1 = Typewriter info (45 chars)
                SuperPhrase2TimeBonus = SuperPhrase2WordCount * 9; // +351% // 2 = Touch typing info (39 chars)
                SuperPhrase3TimeBonus = SuperPhrase3WordCount * 6; // +474% // 6 = Roman numeral basics (79 chars)
                SuperPhrase4TimeBonus = SuperPhrase4WordCount * 4; // +528% // 5 = Roman numerals in use (132 chars)
                SuperPhrase5TimeBonus = SuperPhrase5WordCount * 8; // +321% // 3 = Typwriter info (Hansen writing ball) (41 chars)
                SuperPhrase6TimeBonus = SuperPhrase6WordCount * 5; // +220% // 4 = Typewriting facts (44 chars)
            }
            catch
            {
                SuperPhrase1 = "Error";
                SuperPhrase2 = "Error";
                SuperPhrase3 = "Error";
                SuperPhrase4 = "Error";
                SuperPhrase5 = "Error";
                SuperPhrase6 = "Error";
            }
        }

        private void InitializeDictionary()
        {
            InitializeInitialPhrase();
            InitializeSuperPhrases();
            try
            {
                DictionaryList.Clear();
                DictionaryList.AddRange(IO.DictionaryTextReader());
                FourLetterWords.Clear();
                FiveLetterWords.Clear();
                SixLetterWords.Clear();
                SevenLetterWords.Clear();
                EightLetterWords.Clear();
                NineLetterWords.Clear();
                TenLetterWords.Clear();
                ElevenLetterWords.Clear();
                TwelveLetterWords.Clear();
                ThirteenLetterWords.Clear();
                FourteenLetterWords.Clear();
                FifteenLetterWords.Clear();
                SixteenLetterWords.Clear();
                SeventeenLetterWords.Clear();
                EighteenLetterWords.Clear();

                PreviousValue4.Clear();
                PreviousValue5.Clear();
                PreviousValue6.Clear();
                PreviousValue7.Clear();
                PreviousValue8.Clear();
                PreviousValue9.Clear();
                PreviousValue10.Clear();
                PreviousValue11.Clear();
                PreviousValue12.Clear();
                PreviousValue13.Clear();
                PreviousValue14.Clear();
                PreviousValue15.Clear();
                PreviousValue16.Clear();
                PreviousValue17.Clear();
                PreviousValue18.Clear();

                foreach (string s in DictionaryList)
                {
                    if (s.Length == 4)
                    {
                        FourLetterWords.Add(s);
                    }
                    else if (s.Length == 5)
                    {
                        FiveLetterWords.Add(s);
                    }
                    else if (s.Length == 6)
                    {
                        SixLetterWords.Add(s);
                    }
                    else if (s.Length == 7)
                    {
                        SevenLetterWords.Add(s);
                    }
                    else if (s.Length == 8)
                    {
                        EightLetterWords.Add(s);
                    }
                    else if (s.Length == 9)
                    {
                        NineLetterWords.Add(s);
                    }
                    else if (s.Length == 10)
                    {
                        TenLetterWords.Add(s);
                    }
                    else if (s.Length == 11)
                    {
                        ElevenLetterWords.Add(s);
                    }
                    else if (s.Length == 12)
                    {
                        TwelveLetterWords.Add(s);
                    }
                    else if (s.Length == 13)
                    {
                        ThirteenLetterWords.Add(s);
                    }
                    else if (s.Length == 14)
                    {
                        FourteenLetterWords.Add(s);
                    }
                    else if (s.Length == 15)
                    {
                        FifteenLetterWords.Add(s);
                    }
                    else if (s.Length == 16)
                    {
                        SixteenLetterWords.Add(s);
                    }
                    else if (s.Length == 17)
                    {
                        SeventeenLetterWords.Add(s);
                    }
                    else if (s.Length == 18)
                    {
                        EighteenLetterWords.Add(s);
                    }
                }
            }
            catch
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Error loading dictionary...", MessageText.TypermaxCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        // Used to clear previous values if they reach the count of the
        // source list, to prevent all words in a list from bein used up
        private void CheckAndClear(List<string> sourceList, List<int> targetList)
        {
            if (targetList.Count >= sourceList.Count - 20)
            {
                targetList.Clear();
            }
        }

        private int tempCurrentRandomNumber;
        private string GetRandomWord(int length)
        {
            if (length >= 4 && length <= 18)
            {
                if (length == 4)
                {
                    // Get random number that does  not match previous number
                    tempCurrentRandomNumber = GetRandomNumber(FourLetterWords.Count, PreviousValue4);
                    // Update previous number
                    PreviousValue4.Add(tempCurrentRandomNumber);
                    CheckAndClear(FourLetterWords, PreviousValue4); // Truncate if too close to source list
                    // Retrieve value
                    return FourLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 5)
                {
                    // Get random number that does  not match previous number
                    tempCurrentRandomNumber = GetRandomNumber(FiveLetterWords.Count, PreviousValue5);
                    // Update previous number
                    PreviousValue5.Add(tempCurrentRandomNumber);
                    CheckAndClear(FiveLetterWords, PreviousValue5); // Truncate if too close to source list
                    // Retrieve value
                    return FiveLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 6)
                {
                    tempCurrentRandomNumber = GetRandomNumber(SixLetterWords.Count, PreviousValue6);
                    // Update previous number
                    PreviousValue6.Add(tempCurrentRandomNumber);
                    CheckAndClear(SixLetterWords, PreviousValue6); // Truncate if too close to source list
                    // Retrieve value
                    return SixLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 7)
                {
                    tempCurrentRandomNumber = GetRandomNumber(SevenLetterWords.Count, PreviousValue7);
                    // Update previous number
                    PreviousValue7.Add(tempCurrentRandomNumber);
                    CheckAndClear(SevenLetterWords, PreviousValue7); // Truncate if too close to source list
                    // Retrieve value
                    return SevenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 8)
                {
                    tempCurrentRandomNumber = GetRandomNumber(EightLetterWords.Count, PreviousValue8);
                    // Update previous number
                    PreviousValue8.Add(tempCurrentRandomNumber);
                    CheckAndClear(EightLetterWords, PreviousValue8); // Truncate if too close to source list
                    // Retrieve value
                    return EightLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 9)
                {
                    tempCurrentRandomNumber = GetRandomNumber(NineLetterWords.Count, PreviousValue9);
                    // Update previous number
                    PreviousValue9.Add(tempCurrentRandomNumber);
                    CheckAndClear(NineLetterWords, PreviousValue9); // Truncate if too close to source list
                    // Retrieve value
                    return NineLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 10)
                {
                    tempCurrentRandomNumber = GetRandomNumber(TenLetterWords.Count, PreviousValue10);
                    // Update previous number
                    PreviousValue10.Add(tempCurrentRandomNumber);
                    CheckAndClear(TenLetterWords, PreviousValue10); // Truncate if too close to source list
                    // Retrieve value
                    return TenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 11)
                {
                    tempCurrentRandomNumber = GetRandomNumber(ElevenLetterWords.Count, PreviousValue11);
                    // Update previous number
                    PreviousValue11.Add(tempCurrentRandomNumber);
                    CheckAndClear(ElevenLetterWords, PreviousValue11); // Truncate if too close to source list
                    // Retrieve value
                    return ElevenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 12)
                {
                    tempCurrentRandomNumber = GetRandomNumber(TwelveLetterWords.Count, PreviousValue12);
                    // Update previous number
                    PreviousValue12.Add(tempCurrentRandomNumber);
                    CheckAndClear(TwelveLetterWords, PreviousValue12); // Truncate if too close to source list
                    // Retrieve value
                    return TwelveLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 13)
                {
                    tempCurrentRandomNumber = GetRandomNumber(ThirteenLetterWords.Count, PreviousValue13);
                    // Update previous number
                    PreviousValue13.Add(tempCurrentRandomNumber);
                    CheckAndClear(ThirteenLetterWords, PreviousValue13); // Truncate if too close to source list
                    // Retrieve value
                    return ThirteenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 14)
                {
                    tempCurrentRandomNumber = GetRandomNumber(FourteenLetterWords.Count, PreviousValue14);
                    // Update previous number
                    PreviousValue14.Add(tempCurrentRandomNumber);
                    CheckAndClear(FourteenLetterWords, PreviousValue14); // Truncate if too close to source list
                    // Retrieve value
                    return FourteenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 15)
                {
                    tempCurrentRandomNumber = GetRandomNumber(FifteenLetterWords.Count, PreviousValue15);
                    // Update previous number
                    PreviousValue15.Add(tempCurrentRandomNumber);
                    CheckAndClear(FifteenLetterWords, PreviousValue15); // Truncate if too close to source list
                    // Retrieve value
                    return FifteenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 16)
                {
                    tempCurrentRandomNumber = GetRandomNumber(SixteenLetterWords.Count, PreviousValue16);
                    // Update previous number
                    PreviousValue16.Add(tempCurrentRandomNumber);
                    CheckAndClear(SixteenLetterWords, PreviousValue16); // Truncate if too close to source list
                    // Retrieve value
                    return SixteenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 17)
                {
                    tempCurrentRandomNumber = GetRandomNumber(SeventeenLetterWords.Count, PreviousValue17);
                    // Update previous number
                    PreviousValue17.Add(tempCurrentRandomNumber);
                    CheckAndClear(SeventeenLetterWords, PreviousValue17); // Truncate if too close to source list
                    // Retrieve value
                    return SeventeenLetterWords[tempCurrentRandomNumber];
                }
                else if (length == 18)
                {
                    tempCurrentRandomNumber = GetRandomNumber(EighteenLetterWords.Count, PreviousValue18);
                    // Update previous number
                    PreviousValue18.Add(tempCurrentRandomNumber);
                    CheckAndClear(EighteenLetterWords, PreviousValue18); // Truncate if too close to source list
                    // Retrieve value
                    return EighteenLetterWords[tempCurrentRandomNumber];
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

        private static Random rand;
        private static int tempRandomNumberResult;
        private int GetRandomNumber(int maxValue, List<int> randomList)
        {
            if (rand == null)
            {
                rand = new Random();
            }
            // Generate initial random
            tempRandomNumberResult = rand.Next(1, maxValue - 1);
            // If random is equal to "notEqualTo" generate a new one until different
            if (DoesRandomExist(randomList, tempRandomNumberResult))
            {
                while (DoesRandomExist(randomList, tempRandomNumberResult))
                {
                    tempRandomNumberResult = rand.Next(1, maxValue - 1);
                }
            }

            return tempRandomNumberResult;
        }

        private bool DoesRandomExist(List<int> list, int compare)
        {
            if (list != null && list.Count > 0)
            {
                foreach (int i in list)
                {
                    if (i == compare)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Fill Meter
        private int tempFillMeterCounter;
        private void StartFillMeterCounter()
        {
            if (TestMode)
            {
                tempFillMeterCounter = 0;
                lbTimeMeter.Text = "";
                timerFillMeter.Interval = 5;
                timerFillMeter.Enabled = true;
            }
            else
            {
                tempFillMeterCounter = 0;
                lbTimeMeter.Text = "";
                timerFillMeter.Interval = 10;
                timerFillMeter.Enabled = true;
            }
        }

        private void timerFillMeter_Tick(object sender, EventArgs e)
        {
            if (TestMode && tempFillMeterCounter < TrainingModeTimeout)
            {
                tempFillMeterCounter += 5;
                SetLevelMeter(lbTimeMeter, tempFillMeterCounter);
            }
            else if (tempFillMeterCounter < GetLevelStartBarLength())
            {
                tempFillMeterCounter += 1;
                SetLevelMeter(lbTimeMeter, tempFillMeterCounter);
            }
            else
            {
                timerFillMeter.Enabled = false;
            }
        }
        #endregion

        #region Blink label
        private int tempBlinkCount;
        private const int blinkMaxCount = 20;
        private Label tempBlinkLabel;
        private bool tempBlinkOnOff;
        private void BlinkControl(Label label)
        {
            StopBlinker();
            tempBlinkOnOff = false;
            tempBlinkCount = 0;
            tempBlinkLabel = label;
            timerBlinker.Enabled = true;
        }

        private void StopBlinker()
        {
            if (timerBlinker.Enabled)
            {
                timerBlinker.Enabled = false;
                tempBlinkLabel.Visible = true;
            }
            UpdateScore();
        }

        private void timerBlinker_Tick(object sender, EventArgs e)
        {
            tempBlinkCount += 1;
            if (tempBlinkOnOff)
            {
                tempBlinkOnOff = false;
                tempBlinkLabel.Visible = false;
            }
            else
            {
                tempBlinkOnOff = true;
                tempBlinkLabel.Visible = true;
            }

            if (tempBlinkCount >= blinkMaxCount)
            {
                timerBlinker.Enabled = false;
                tempBlinkLabel.Visible = true;
            }
        }

        #endregion

        #region Count down for start
        private int tempCountDown;
        private bool tempResume;
        private void StartCountDown(bool resumeGame)
        {
            tempResume = resumeGame;
            if (!timerFillMeter.Enabled)
            {
                lbSplash.Text = "Ready!";
                lbSplash.Font = Design.MessageboxXXLFont();
                lbSplash.Visible = true;
                lbSplash.BringToFront();
                tempCountDown = 3;
                timerCountDown.Interval = 500;
                timerCountDown.Enabled = true;
            }
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            lbSplash.Text = tempCountDown.ToString();
            tempCountDown -= 1;
            if (tempCountDown == -1)
            {
                timerCountDown.Enabled = false;
                lbSplash.Visible = false;

                if (tempResume)
                {
                    ResumeEngine();
                }
                else
                {
                    StartGame();
                }
            }
        }
        #endregion

        #region Global timer

        // Global timer updates Marquee control with current highscore etc.
        private int tempQuoteCounter;
        private void timerGlobal_Tick(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (timerGameEngine.Enabled)
            {
                // Pause if notification is triggered
                if (MainForm.NotificationEditorOpen)
                {
                    PauseGame();
                }

                lbWPMMeter.Text = GetProgressBar();
                lbWPMDisplay.Text = GetWPMCount();
                CompareInput();
                UpdateScore();
                // Write random quotes and occasional score (every 5th second)
                tempQuoteCounter += 1;
                if (tempQuoteCounter == 5)
                {
                    UpdateTopWordValue();
                    tempQuoteCounter = 0;
                    mqLogo.StopSequence();
                    mqLogo.StartLightShow(GetRandomQuote(), 40, false);
                }

                // Refocus input field if focus was lost (tested and works)
                if (!tbInput.Focused)
                {
                    tbInput.Focus();
                }
            }
        }
        #endregion

        #region Random Quotes in Marquee
        private int GetRandom2Number()
        {
            if (rand2 == null)
            {
                rand2 = new Random();
            }
            return rand2.Next(1, 13);
        }

        private static Random rand2;
        private int tempCurrentRandom2;
        private int tempPreviousRandom2;
        private string GetRandomQuote()
        {
            if (TestMode)
            {
                return TrainingModeText;
            }

            tempCurrentRandom2 = GetRandom2Number();
            if (tempCurrentRandom2 == tempPreviousRandom2)
            {
                while (tempCurrentRandom2 == tempPreviousRandom2)
                {
                    tempCurrentRandom2 = GetRandom2Number();
                }
            }
            tempPreviousRandom2 = tempCurrentRandom2;
            switch (tempCurrentRandom2)
            {
                case 1:
                    return "CLICK, CLICK, CLICK...";
                case 2:
                    return "*** " + WordValueString + TopWordValue.ToString() + " ***"; // Return current word value
                case 3:
                    return "THE SOUND OF TYPING...";
                case 4:
                    return "*** " + LevelString + Level.ToString() + " ***";
                case 5:
                    switch (Level)
                    {
                        case 1:
                            return "OKAY ROOKIE, LETS SEE WHAT YOU'VE GOT!";
                        case 2:
                            return "YEAH, THERE YOU GO GRUNT!";
                        case 3:
                            return "ALL RIGHT SO YOU ARE ADVANCING, NOT BAD!";
                        case 4:
                            return "ALL HAIL THE BATTERED KEYBOARD BUTCHER, THIS IS GOING WELL!";
                        case 5:
                            return "PROFESSIONAL SKILLS, NOTHING IS HOLDING YOU BACK!";
                        default:
                            return "YOU HAVE REACHED THE LEVEL OF MASTER, NOTHING IS HOLDING YOU BACK!";
                    }
                case 6:
                    return "EXERCISE MAKES THE MASTER...";
                case 7:
                    return "*** " + ScoreString + Score.ToString() + " ***"; // Return score
                case 8:
                    return "*** " + LevelString + Level.ToString() + " ***";
                case 9:
                    return "MAKE THE MOST OF IT!";
                case 10:
                    return "*** " + WordValueString + TopWordValue.ToString() + " ***"; // Return current word value
                case 11:
                    return "*** " + WordValueString + TopWordValue.ToString() + " ***"; // Return current word value
                case 12:
                    return "THAT'S A CHAMPION! REACH FOR THE STARS...";
                default:
                    return "*** TYPERMAX ***";
            }
        }
        #endregion

        #region Button events

        private void tbTarget_TextChanged(object sender, EventArgs e)
        {
            if (tbTarget.Text.Length > 0)
            {
                lbRuler.Text = TextTransform.GetRepeatedCharacter(tbTarget.Text.Length, "|");
            }
            else
            {
                lbRuler.Text = "";
            }
        }

        private void tbTarget_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
        }


        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (timerGameEngine.Enabled)
            {
                // Do nothing
            }
            else
            {
                tbInput.Text = "";
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            // Do nothing
        }

        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do nothing
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (timerGameEngine.Enabled)
            {
                CompareInput();
            }
        }

        private void tbTarget_Enter(object sender, EventArgs e)
        {
            tbInput.Focus();
        }

        private void Typermax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnStartStop_Click(this, null);
                tbInput.Text = "";
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


        // 0 = stop, 1 = running, 2 = paused...
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!timerFillMeter.Enabled)
            {
                // If game stopped
                if (GameMode == 0)
                {
                    GameMode = 1;
                    StartCountDown(false);
                }
                else if (GameMode == 1) // If game running
                {
                    GameMode = 2;
                    PauseGame();
                }
                else if (GameMode == 2) // If game paused
                {
                    GameMode = 1;
                    ResumeGame();
                }
            }
            FocusInput();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (GameWasReset || GameMode == 0 || GameMode == 1) // Exit
            {
                this.Close();
            }
            else // Reset
            {
                GameMode = 0;
                GameWasReset = true;
                InitializeGameEngine();
                ResetWPMcounter();
                StopGameRunningAnimation();
                btnStartStop.Text = "Start (F5)";
                btnExit.Text = "Exit";
            }
            FocusInput();
        }

        private void FocusInput()
        {
            tbInput.Focus();
        }
        #endregion

        #region Transport
        private void StartGame()
        {
            if (!timerGameEngine.Enabled)
            {
                InitializeGameEngine();
                ResetWPMcounter();
                SetPhrase(GetPhraseLength());
                StartEngine();
            }
        }

        private void ResetValues()
        {
            PreviousValue4.Clear();
            PreviousValue5.Clear();
            PreviousValue6.Clear();
            PreviousValue7.Clear();
            PreviousValue8.Clear();
            PreviousValue9.Clear();
            PreviousValue10.Clear();
            PreviousValue11.Clear();
            PreviousValue12.Clear();
            PreviousValue13.Clear();
            PreviousValue14.Clear();
            PreviousValue15.Clear();
            PreviousValue16.Clear();
            PreviousValue17.Clear();
            PreviousValue18.Clear();
            Credits = DefaultCredits;
            Level = 1;
            Score = 0;
            WordsCompleted = 0;
            TopWordValue = 0;
            CurrentWPMCount = 0;
            tbTarget.Text = "";
            tempInput = "";
            TargetPhrase = "";
            PhraseCount = 0;
            WordsPostedInOnePhrase = 0;
            InitializeFields();
        }

        private void InitializeFields()
        {
            // Set Meters
            lbLevelMeter.Text = BarCharacter + TextTransform.GetBlankSpace(MaxBarLength - 2) + BarEndCharacter;
            lbWPMMeter.Text = BarCharacter + TextTransform.GetBlankSpace(MaxBarLength - 2) + BarEndCharacter;
            lbTimeMeter.Text = BarCharacter + TextTransform.GetBlankSpace(MaxBarLength - 2) + BarEndCharacter;
            // Set displays
            lbCredits.Text = CreditsString + Credits.ToString();
            lbLevelDisplay.Text = LevelString + Level.ToString();
            lbWPMDisplay.Text = CurrentWPMCount.ToString() + WPMString;
            if (TestMode)
            {
                lbHighScore.Text = HighScoreString + "N/A";
            }
            else
            {
                lbHighScore.Text = HighScoreString + HighScore.ToString();
            }
            lbScore.Text = ScoreString + Score.ToString();
            ResetPenaltyProperties();
        }



        private void PauseGame()
        {
            PauseEngine();
        }

        private void ResumeGame()
        {
            StartCountDown(true);
        }

        // Define the start timeout value (The factor which the % uses when counting up)
        // DONT CHANGE THIS
        private int GetLevelStartBarLength()
        {
            return ((MaxBarLength * 2) + 16); // = 100 %
        }


        private int GetPhraseLength()
        {
            if (RunInitialPhrase)
            {
                // Test adding extra percentage to test countdown bar...
                //TimeOutCount += 200;

                // Set initial phrase
                WordsPostedInOnePhrase = InitialPhraseWordCount;
                return InitialPhraseWordCount;
            }

            // Phrase examples: "Great", "E934 There", "Start by typing this..." etc.

            // Super phrases
            SuperPhraseCounter += 1;
            if (SuperPhraseCounter == 30)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 0;
                // NO: TimeOutCount += 100; // Add a bonus at the start
                WordsPostedInOnePhrase = SuperPhrase1WordCount;
                return SuperPhrase1WordCount;
            }
            else if (SuperPhraseCounter == 60)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 1;
                // NO: TimeOutCount += 50; // Add a bonus at the start
                WordsPostedInOnePhrase = SuperPhrase2WordCount;
                return SuperPhrase2WordCount;
            }
            else if (SuperPhraseCounter == 90)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 2;
                // NO: TimeOutCount += 25; // Add a bonus at the start
                WordsPostedInOnePhrase = SuperPhrase3WordCount;
                return SuperPhrase3WordCount;
            }
            else if (SuperPhraseCounter == 120)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 3;
                // NO: TimeOutCount += 25; // Add a bonus at the start ADD NOTHING HERE
                WordsPostedInOnePhrase = SuperPhrase4WordCount;
                return SuperPhrase4WordCount;
            }
            else if (SuperPhraseCounter == 150)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 4;
                // NO: TimeOutCount += 25; // Add a bonus at the start ADD NOTHING HERE
                WordsPostedInOnePhrase = SuperPhrase5WordCount;
                return SuperPhrase5WordCount;
            }
            else if (SuperPhraseCounter == 180)
            {
                SuperPhrase = true;
                CurrentSuperPhrase = 5;
                // NO: TimeOutCount += 25; // Add a bonus at the start ADD NOTHING HERE
                WordsPostedInOnePhrase = SuperPhrase6WordCount;
                return SuperPhrase6WordCount;
            }
            else if (SuperPhraseCounter >= 181)
            {
                SuperPhraseCounter = 0;
            }

            // Standard phrases
            PhraseCount += 1;
            if (PhraseCount == 3)
            {
                WordsPostedInOnePhrase = 2;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount == 6)
            {
                WordsPostedInOnePhrase = 3;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount == 9)
            {
                WordsPostedInOnePhrase = 4;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount == 12)
            {
                WordsPostedInOnePhrase = 2;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount == 16)
            {
                WordsPostedInOnePhrase = 3;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount == 17)
            {
                WordsPostedInOnePhrase = 2;
                return WordsPostedInOnePhrase;
            }
            if (PhraseCount >= 20)
            {
                PhraseCount = 0;
                WordsPostedInOnePhrase = 5;
                return WordsPostedInOnePhrase;
            }
            else
            {
                WordsPostedInOnePhrase = 1;
                return WordsPostedInOnePhrase;
            }
        }


        // Here the bonus time is set (when completing words) // points // score
        private const int OneWordBonus = 7;
        private const int TwoWordBonus = 15;
        private const int ThreeWordBonus = 25;
        private const int FourWordBonus = 35;
        private const int FiveWordBonus = 45;
        private int GetTimeBonus()
        {
            if (RunInitialPhrase)
            {
                return InitialPhraseTimeBonus; // Initial phrase bonus
            }

            if (SuperPhrase)
            {
                switch (CurrentSuperPhrase)
                {
                    case 0:
                        return SuperPhrase1TimeBonus;
                    case 1:
                        return SuperPhrase2TimeBonus;
                    case 2:
                        return SuperPhrase3TimeBonus;
                    case 3:
                        return SuperPhrase4TimeBonus;
                    case 4:
                        return SuperPhrase5TimeBonus;
                    case 5:
                        return SuperPhrase6TimeBonus;
                    default:
                        return SuperPhrase1TimeBonus;
                }
            }

            if (WordsPostedInOnePhrase == 2)
            {
                return TwoWordBonus;
            }
            else if (WordsPostedInOnePhrase == 3)
            {
                return ThreeWordBonus;
            }
            else if (WordsPostedInOnePhrase == 4)
            {
                return FourWordBonus;
            }
            else if (WordsPostedInOnePhrase == 5)
            {
                return FiveWordBonus;
            }
            else
            {
                return OneWordBonus;
            }
        }

        private void ResumeEngine()
        {
            GameMode = 1;
            timerGameRunning.Enabled = true;
            timerGameEngine.Enabled = true;
            ResolveGameButtons();
        }

        private void StartEngine()
        {
            if (Credits > 0 && !timerGameEngine.Enabled)
            {
                GameWasReset = false;
                mqLogo.StopSequence();
                mqLogo.StartLightShow("*** START ***", 40, false);
                StartGameRunningAnimation();
                SetPhrase(GetPhraseLength());
                GameMode = 1;
                timerGameEngine.Interval = 250; // Game engine speed
                timerGameEngine.Enabled = true;
                ResolveGameButtons();
            }
            else
            {
                GameMode = 0;
            }
        }

        private void GameOver()
        {
            StopEngine();
            mqLogo.StopSequence();
            mqLogo.StartLightShow("*** GAME OVER ***" +
                TextTransform.GetBlankSpace(30) + "*** SCORE: " + Score.ToString() + " ***" +
                TextTransform.GetBlankSpace(30) + "*** WORDS AND NUMBERS TYPED: " + WordsCompleted.ToString() + " ***" +
                TextTransform.GetBlankSpace(30) + "*** TOP PHRASE VALUE: " + TopWordValue.ToString() + " ***", 40, false);
            UpdateScore();
            ResolveGameButtons();
        }

        private void PauseEngine()
        {
            if (timerGameEngine.Enabled)
            {
                StopWPMTimer();
                GameMode = 2;
                timerGameRunning.Enabled = false;
                timerGameEngine.Enabled = false;
                mqLogo.StopSequence();
                mqLogo.StartLightShow("*** GAME PAUSED ***", 40, false);
                ResolveGameButtons();
            }
        }

        private void StopEngine()
        {
            if (timerGameEngine.Enabled)
            {
                StopWPMTimer();
                GameMode = 0;
                StopGameRunningAnimation();
                mqLogo.StopSequence();
                mqLogo.StartLightShow("*** READY AND WAITING ***", 40, false);
                ClearPhrase();
                timerGameEngine.Enabled = false;
                ResolveGameButtons();
            }
        }


        private void ResolveGameButtons()
        {
            // If game stopped
            if (GameMode == 0)
            {
                btnStartStop.Text = "Start (F5)";
                btnExit.Text = "Exit";
            }
            else if (GameMode == 1) // If game running
            {
                btnStartStop.Text = "Pause (F5)";
                btnExit.Text = "Exit";
            }
            else if (GameMode == 2) // If game paused
            {
                btnStartStop.Text = "Resume (F5)";
                btnExit.Text = "Reset";
            }
        }
        #endregion

        #region Game engine
        private int TimeOutCount;
        private int TimeOutSubtractionCounter;
        private int LevelBar;
        private int LevelBarInterval;
        private void InitializeGameEngine()
        {
            ResetValues();
            LevelBar = 0;
            LevelBarInterval = 0;
            SuperPhraseCounter = 0;
            CurrentSuperPhrase = 0;
            RunInitialPhrase = true;
            TimeOutSubtractionCounter = 0;
            ResetPenaltyProperties();
            SetRetractionLabel(1);
            if (TestMode)
            {
                SetErrorLabel(DefaultTestModeErrorLabelTest);
                TimeOutCount = TrainingModeTimeout;
                SetPercentage(TimeOutCount);
            }
            else
            {
                SetErrorLabel(DefaultErrorLabelTest);
                TimeOutCount = GetLevelStartBarLength();
                SetPercentage(TimeOutCount);
            }
            lbLevelMeter.Text = BarCharacter + TextTransform.GetBlankSpace(MaxBarLength - 2) + BarEndCharacter;
            SetLevelMeter(lbTimeMeter, TimeOutCount);
            tbHistory.Text = ""; // Clear history
        }

        private void SetRetractionLabel(int speed)
        {
            lbCountDownSpeed.Text = "Timeout: " + speed + "/4" +
                                    TextTransform.GetBlankSpace(2) + "Hits: +" + SuccessfullKeystrokes + "/-" + FailedKeystrokes +
                                    TextTransform.GetBlankSpace(2) + "Accuracy: " + GetPrecision() +
                                    TextTransform.GetBlankSpace(2) + "Penalty: " + GetTimePenalty();
        }

        private string GetTimePenalty()
        {
            if (TotalTimePenalty > 0)
            {
                return "-" + TotalTimePenalty + "%";
            }
            else
            {
                return TotalTimePenalty + "%";
            }
        }

        private string GetPrecision()
        {
            if (SuccessfullKeystrokes > 0 && FailedKeystrokes > 0 && (FailedKeystrokes < SuccessfullKeystrokes))
            {
                return (100 - ((FailedKeystrokes / SuccessfullKeystrokes) * 100)).ToString("0.0") + "%";
            }
            else if (FailedKeystrokes >= SuccessfullKeystrokes)
            {
                return "0%";
            }
            else
            {
                return "100.0%";
            }
        }

        private void AddWordCompletedScore()
        {
            // Count op words completed
            WordsCompleted += WordsPostedInOnePhrase;
            Score += GetCurrentWordValue();
            UpdateScore();
            UpdateTopWordValue();
        }

        private int tempWordValue;
        private int GetCurrentWordValue()
        {
            // Word value * level + level completet percentage
            tempWordValue = ((WordScoreValue + WordsCompleted) * Level) + GetLevelCompletionPercentage();

            UpdateTopWordValue();
            return tempWordValue;
        }

        private void UpdateTopWordValue()
        {
            if (tempWordValue > TopWordValue)
            {
                TopWordValue = tempWordValue;
            }
        }

        private int GetLevelCompletionPercentage()
        {
            if (LevelBar >= 0)
            {

                return ((LevelBar * 100) / MaxBarLength);
            }
            else
            {
                return 0;
            }
        }

        private void UpdateScore()
        {
            MainForm.KeepAwake();
            // Update highscore
            if (!TestMode && Score > HighScore)
            {
                HighScore = Score;
                lbHighScore.Text = HighScoreString + HighScore.ToString();
                BlinkControl(lbHighScore);
                MainForm.SetTypermaxHighScore(HighScore);
            }
            // Update normal score
            lbScore.Text = ScoreString + Score.ToString();
        }

        private void UpdateCredits()
        {
            lbCredits.Text = CreditsString + Credits.ToString();
            BlinkControl(lbCredits);
        }

        private void UpdateLevel()
        {
            lbLevelDisplay.Text = LevelString + Level.ToString();
            BlinkControl(lbLevelDisplay);
        }

        private void SetPercentage(int value)
        {
            // Set progress percentage
            lbPercentageDisplay.Text = ((value * 100) / GetLevelStartBarLength()).ToString() + "%";
        }

        private void SetLevelMeter(Label label, int value)
        {
            SetPercentage(value);
            if (value == 0)
            {
                label.Text = BarCharacter + TextTransform.GetBlankSpace(MaxBarLength - 2) + BarEndCharacter;
            }
            else if (value > 0 && value < MaxBarLength)
            {
                label.Text = TextTransform.GetRepeatedCharacter(value, BarCharacter) + TextTransform.GetBlankSpace((MaxBarLength - value) - 1) + BarEndCharacter;
            }
            else if (value > 0 && (value >= MaxBarLength && value < (MaxBarLength * 2)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - MaxBarLength, AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - MaxBarLength), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 2) && value < (MaxBarLength * 3)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 2), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 2)), AlternateBarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 3) && value < (MaxBarLength * 4)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 3), AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 3)), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 4) && value < (MaxBarLength * 5)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 4), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 4)), AlternateBarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 5) && value < (MaxBarLength * 6)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 5), AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 5)), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 6) && value < (MaxBarLength * 7)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 6), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 6)), AlternateBarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 7) && value < (MaxBarLength * 8)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 7), AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 7)), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 8) && value < (MaxBarLength * 9)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 8), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 8)), AlternateBarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 9) && value < (MaxBarLength * 10)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 9), AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 9)), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 10) && value < (MaxBarLength * 11)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 10), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 10)), AlternateBarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 11) && value < (MaxBarLength * 12)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 11), AlternateBarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 11)), BarCharacter);
            }
            else if (value > 0 && (value >= (MaxBarLength * 12) && value < (MaxBarLength * 13)))
            {
                label.Text = TextTransform.GetRepeatedCharacter(value - (MaxBarLength * 12), BarCharacter) + TextTransform.GetRepeatedCharacter(MaxBarLength - (value - (MaxBarLength * 12)), AlternateBarCharacter);
            }
            else if (value > 0 && value >= (MaxBarLength * 13))
            {
                label.Text = TextTransform.GetRepeatedCharacter(MaxBarLength, BarCharacter);
            }
            else
            {
                label.Text = TextTransform.GetRepeatedCharacter(MaxBarLength, BarCharacter);
            }
        }

        private void timerGameEngine_Tick(object sender, EventArgs e)
        {
            // Level bar (only encrease level if not running superphrase)
            if (!SuperPhrase && !RunInitialPhrase)
            {
                if (LevelBar < MaxBarLength)
                {
                    LevelBarInterval += 1;
                    if (LevelBarInterval >= 10)
                    {
                        LevelBarInterval = 0;
                        LevelBar += 1;
                        SetLevelMeter(lbLevelMeter, LevelBar);
                    }
                }
                else if (LevelBar == MaxBarLength)
                {
                    SetLevelMeter(lbLevelMeter, LevelBar);
                    LevelBar = 0;
                    // Levelup
                    Level += 1;
                    Score += LevelBonus; // Add level bonus
                    UpdateLevel();
                }
            }

            // Time out bar
            if (TimeOutCount > 0)
            {
                if (CanTimeOutSubtract())
                {
                    TimeOutCount -= 1;
                }
                SetLevelMeter(lbTimeMeter, TimeOutCount);
            }
            else if (TimeOutCount == 0)
            {
                SetLevelMeter(lbTimeMeter, TimeOutCount);
                if (Credits > 0)
                {
                    Credits -= 1;
                    UpdateCredits();
                }

                if (Credits == 0)
                {
                    // Gameover
                    GameOver();
                }
                else
                {
                    TimeOutCount = GetLevelStartBarLength();
                }
            }
        }


        private bool CanTimeOutSubtract()
        {
            TimeOutSubtractionCounter += 1;
            switch (Level) // Check the level and run faster with each level
            {
                case 1: // Pause by a count of 3
                    if (TimeOutSubtractionCounter >= 3)
                    {
                        SetRetractionLabel(1);
                        TimeOutSubtractionCounter = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2: // Pause by a count of 2
                    if (TimeOutSubtractionCounter >= 2)
                    {
                        SetRetractionLabel(2);
                        TimeOutSubtractionCounter = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3: // Pause by a count of 1
                    if (TimeOutSubtractionCounter >= 1)
                    {
                        SetRetractionLabel(3);
                        TimeOutSubtractionCounter = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:  // Do not pause
                    SetRetractionLabel(4);
                    TimeOutSubtractionCounter = 0;
                    return true;
            }
        }
        #endregion

        #region Process action
        private string GenerateTarget(int level, int levelProgress)
        {
            if (level == 1 && levelProgress <= 49)
            {
                return GetRandomWord(4);
            }
            else if (level == 1 && levelProgress >= 50)
            {
                return GetRandomWord(5);
            }
            else if (level == 2 && levelProgress <= 49)
            {
                return GetRandomWord(6);
            }
            else if (level == 2 && levelProgress >= 50)
            {
                return GetRandomWord(7);
            }
            else if (level == 3 && levelProgress <= 49)
            {
                return GetRandomWord(8);
            }
            else if (level == 3 && levelProgress >= 50)
            {
                return GetRandomWord(9);
            }
            else if (level == 4 && levelProgress <= 49)
            {
                return GetRandomWord(10);
            }
            else if (level == 4 && levelProgress >= 50)
            {
                return GetRandomWord(11);
            }
            else if (level == 5 && levelProgress <= 49)
            {
                return GetRandomWord(12);
            }
            else if (level == 5 && levelProgress >= 50)
            {
                return GetRandomWord(13);
            }
            else if (level == 6 && levelProgress <= 49)
            {
                return GetRandomWord(14);
            }
            else if (level == 6 && levelProgress >= 50)
            {
                return GetRandomWord(15);
            }
            else if (level == 7 && levelProgress <= 49)
            {
                return GetRandomWord(16);
            }
            else if (level == 7 && levelProgress >= 50)
            {
                return GetRandomWord(17);
            }
            else if (level >= 8 && levelProgress <= 49)
            {
                return GetRandomWord(17);
            }
            else if (level >= 8 && levelProgress >= 50)
            {
                return GetRandomWord(18);
            }
            else { return "N/A"; }
        }


        private void ClearPhrase()
        {
            // Clear target
            TargetPhrase = "";
            tbTarget.Text = "";
            tbInput.Text = "";
        }

        private void SetPhrase(int numberOfWords)
        {
            ClearPhrase();
            if (RunInitialPhrase)
            {
                TargetPhrase = InitialPhrase;
            }
            else if (SuperPhrase)
            {
                TargetPhrase = GetSuperPhrase(CurrentSuperPhrase);
            }
            else
            {
                if (numberOfWords == 1)
                {
                    TargetPhrase = GenerateTarget(Level, GetLevelCompletionPercentage());
                }
                else
                {
                    for (int i = 0; i < numberOfWords; i++)
                    {
                        if (i == numberOfWords - 1) // Last word without whitespace
                        {
                            TargetPhrase += GenerateTarget(Level, GetLevelCompletionPercentage());
                        }
                        else
                        {
                            TargetPhrase += GenerateTarget(Level, GetLevelCompletionPercentage()) + " ";
                        }
                    }
                }
            }
            tbTarget.Text = TargetPhrase.TrimEnd(' '); // Trim, just to be safe :)
        }

        private string GetSuperPhrase(int phrase)
        {
            switch (phrase)
            {
                case 0:
                    return SuperPhrase1;
                case 1:
                    return SuperPhrase2;
                case 2:
                    return SuperPhrase3;
                case 3:
                    return SuperPhrase4;
                case 4:
                    return SuperPhrase5;
                case 5:
                    return SuperPhrase6;
                default:
                    return SuperPhrase1;
            }
        }


        private void RevolveGameRunningAnimation()
        {
            lbInput.Text = EvolveAnimationANSI(false);
        }

        private string ReplaceBlankSpace(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Contains(" "))
                {
                    return "_";
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return input;
            }
        }

        private string tempInput = "";
        private void CompareInput()
        {
            // make sure both textboxes have content before testing
            if (tbInput.Text.Length > 0 && tbTarget.Text.Length > 0)
            {
                // if the first letter in both textboxes matches
                if (tbInput.Text.Substring(0, 1) == tbTarget.Text.Substring(0, 1))
                {
                    SubmitInputCount(); // Invoke WPM counter
                    lbWPMMeter.Text = GetProgressBar(); // Update WPM counter

                    // add the input to the input variable for later comparison
                    tempInput += tbInput.Text;
                    // only remove chars from the Typerphrase textbox there is any
                    if (tbTarget.Text.Length > 0)
                    {
                        tbTarget.Text = tbTarget.Text.Substring(1, tbTarget.Text.Length - 1);
                    }

                    // Register successful keystroke
                    DoSuccsessfulKeystroke();
                    // Type animation next step
                    RevolveGameRunningAnimation();
                    // clear the input textbox
                    tbInput.Text = "";
                }
                else
                {
                    // Add failed key to history display
                    tbHistory.Text = ReplaceBlankSpace(tbInput.Text) + tbHistory.Text;
                    if (tbHistory.Text.Length > 35)
                    {
                        tbHistory.Text = tbHistory.Text.Substring(0, 34);
                    }

                    // Register failed keystroke
                    DoFailedKeystroke();
                    // clear the input textbox
                    tbInput.Text = "";
                    // Give penalty for false key
                    PenaltyError();
                }
            }


            // Check match
            if (TargetPhrase.Length > 0 && tempInput.Trim() == TargetPhrase.Trim())
            {
                // Clear input
                tempInput = "";

                // Add points
                AddWordCompletedScore();

                // Add extra time
                TimeOutCount += GetTimeBonus();

                // Generate new phrase
                RunInitialPhrase = false;
                SuperPhrase = false; // Reset superphrase
                SetPhrase(GetPhraseLength());
            }
        }
        #endregion

        #region WPM Counter
        private Timer SpeedTimer;
        private decimal InputCount { get; set; }
        public void InitializeSpeedTimer()
        {
            InputCount = 0;
            SpeedTimer = new Timer();
            SpeedTimer.Interval = 1000; // One second ticks
            SpeedTimer.Tick += timerWpmCounter_Tick;
        }

        // "Hunt and peck" typist types around 30 WPM
        // Average professional types 50-80 words per minutes (WPM) (5 chars is regarded as one word)
        // Professionals: 70-95 WPM
        // Worlds fastest typist: 216 WPM in 1 minute on IBM electric in 1946

        /// <summary>
        /// Submit keypress
        /// </summary>
        public void SubmitInputCount()
        {
            if (!SpeedTimer.Enabled)
            {
                SpeedTimer.Enabled = true;
            }

            tempKeepAliveCounter += 0.5m;
            InputChanged();
        }

        private void StopWPMTimer()
        {
            if (SpeedTimer.Enabled)
            {
                SpeedTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Retrieve progress bar
        /// </summary>
        /// <returns></returns>
        public string GetProgressBar()
        {
            return tempWPMStringOutput;
        }

        private const int ProgressBarLength = 30;
        private const string ProgressBarSymbol = BarCharacter; // the symbol to show
        private const decimal MaximumValue = 150m;
        private string TempProgressBarOutput;
        private string RetrieveScore(decimal currentValue)
        {
            TempProgressBarOutput = "";
            if (currentValue > 0)
            {
                // Calculate progress bar
                for (int i = 0; i < (currentValue / (MaximumValue / ProgressBarLength)); i++)
                {
                    if (TempProgressBarOutput.Length < ProgressBarLength)
                    {
                        TempProgressBarOutput += ProgressBarSymbol;
                    }
                }

                // Style for typermax
                if (TempProgressBarOutput.Length < ProgressBarLength)
                {
                    TempProgressBarOutput = TempProgressBarOutput + TextTransform.GetBlankSpace((ProgressBarLength - TempProgressBarOutput.Length) - 1) + BarEndCharacter;
                }
            }
            else
            {
                TempProgressBarOutput = BarCharacter + TextTransform.GetBlankSpace((ProgressBarLength) - 2) + BarEndCharacter;
            }

            // Output value
            return TempProgressBarOutput;
        }

        private decimal tempKeepAliveCounter;
        string tempWPMStringOutput = "";
        private decimal _strWPM; // WPM = Words per minute, CPM = Characters per minute
        private decimal _iWPMCount;
        private decimal _iWPMTotalTime;
        public void ResetWPMcounter()
        {
            _iWPMTotalTime = 0;
            _strWPM = 0;
            _iWPMCount = 0;
            tempWPMStringOutput = RetrieveScore(0);
            InputCount = 0;
        }

        private void InputChanged()
        {
            try
            {
                _strWPM += 1;

                if (_iWPMTotalTime > 0 && _strWPM >= 5)
                {
                    _iWPMCount = ((60 / _iWPMTotalTime) * (_strWPM / 3)); // The factor to define speed 4 = normal, 3 = faster to compensate for the programs anatomy
                    if (_iWPMCount >= 0)
                    {
                        tempWPMStringOutput = RetrieveScore(_iWPMCount);
                    }
                }
                else
                {
                    tempWPMStringOutput = RetrieveScore(0);
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private string GetWPMCount()
        {
            if (_iWPMCount >= 0)
            {
                return _iWPMCount.ToString("0") + WPMString;
            }
            else
            {
                return "0" + WPMString;
            }
        }


        private void timerWpmCounter_Tick(object sender, EventArgs e)
        {
            if (tempKeepAliveCounter > 0)
            {
                tempKeepAliveCounter -= (0.5m + (tempKeepAliveCounter / 2)); // Reduce with a factor of 0.5 + the total running time devided by 2
            }
            else
            {
                ResetWPMcounter();
                if (SpeedTimer.Enabled)
                {
                    SpeedTimer.Enabled = false;
                }
            }
            _iWPMTotalTime = _iWPMTotalTime + 1;
        }
        #endregion

        #region Splash
        private void ShowSplash()
        {
            // Set logo
            lbSplash.BringToFront();
            lbSplash.Dock = DockStyle.Fill;
            lbSplash.Visible = true;
            lbSplash.Font = Design.MessageboxSmallFont();
            if (TestMode)
            {
                lbSplash.Text = IO.TypermaxLogoTextReader(1);
            }
            else
            {
                lbSplash.Text = IO.TypermaxLogoTextReader(0);
            }
            lbSplash.TextAlign = ContentAlignment.MiddleCenter;
            timerSplash.Interval = 2000;
            timerSplash.Enabled = true;
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            timerSplash.Enabled = false;
            lbSplash.Visible = false;
            lbSplash.SendToBack();
            BlinkControl(lbCredits);
            StartFillMeterCounter();
            tbInput.Focus();
            timerGlobal.Enabled = true;
        }
        #endregion

        #region Game running animation
        private void StartGameRunningAnimation()
        {
            if (!timerGameRunning.Enabled)
            {
                lbInput.Text = "";
                lbTarget.Text = "";
                tempGameRunningAnimationIndex = 0;
                tempGameRunningAnimationString = "";
                tempEvolveAnimationIndex = 0;
                tempEvolveAnimationString = "";
                timerGameRunning.Interval = 50;
                timerGameRunning.Enabled = true;
            }
        }

        private void StopGameRunningAnimation()
        {
            timerGameRunning.Enabled = false;
            lbInput.Text = "<";
            lbTarget.Text = ">";
        }

        // ─\│/
        // │\─/
        private int tempGameRunningAnimationIndex;
        private string tempGameRunningAnimationString;
        private string GetGameRunningAnimationANSI(bool direction)
        {
            switch (tempGameRunningAnimationIndex)
            {
                case 0:
                    if (direction)
                    {
                        tempGameRunningAnimationString = @"─";
                    }
                    else
                    {
                        tempGameRunningAnimationString = @"│";
                    }
                    break;
                case 1:
                    tempGameRunningAnimationString = @"\";
                    break;
                case 2:
                    if (direction)
                        tempGameRunningAnimationString = @"│";
                    else
                    {
                        tempGameRunningAnimationString = @"─";
                    }
                    break;
                case 3:
                    tempGameRunningAnimationString = @"/";
                    break;
                default:
                    tempGameRunningAnimationString = "";
                    break;
            }

            tempGameRunningAnimationIndex += 1;
            if (tempGameRunningAnimationIndex >= 4)
            {
                tempGameRunningAnimationIndex = 0;
            }
            return tempGameRunningAnimationString;
        }

        private void timerGameRunning_Tick(object sender, EventArgs e)
        {
            lbTarget.Text = GetGameRunningAnimationANSI(true);
        }

        private int tempEvolveAnimationIndex;
        private string tempEvolveAnimationString;
        private string EvolveAnimationANSI(bool direction)
        {
            switch (tempEvolveAnimationIndex)
            {
                case 0:
                    if (direction)
                    {
                        tempEvolveAnimationString = @"─";
                    }
                    else
                    {
                        tempEvolveAnimationString = @"│";
                    }
                    break;
                case 1:
                    tempEvolveAnimationString = @"\";
                    break;
                case 2:
                    if (direction)
                        tempEvolveAnimationString = @"│";
                    else
                    {
                        tempEvolveAnimationString = @"─";
                    }
                    break;
                case 3:
                    tempEvolveAnimationString = @"/";
                    break;
                default:
                    tempEvolveAnimationString = "";
                    break;
            }

            tempEvolveAnimationIndex += 1;
            if (tempEvolveAnimationIndex >= 4)
            {
                tempEvolveAnimationIndex = 0;
            }
            return tempEvolveAnimationString;
        }
        #endregion

        #region Display help
        private string tempHelp;
        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                if (GameMode == 1) // If game running, pause it....
                {
                    GameMode = 2;
                    PauseGame();
                }

                if (TestMode)
                {
                    tempHelp = IO.TypermaxLogoTextReader(1) + TextTransform.GetNewLines(2);
                }
                else
                {
                    tempHelp = IO.TypermaxLogoTextReader(0) + TextTransform.GetNewLines(2);
                }

                tempHelp += IO.TypermaxHelpMessageTextReader();
                AhoyInfoMessageBox msg = new AhoyInfoMessageBox(tempHelp, MessageText.TypermaxCaption(), false, true, this.Opacity);
                msg.SetOptionA("Training Mode = OFF", "Training Mode = ON", TestMode);
                msg.TopMost = true;
                msg.FormClosing += Msg_FormClosing;
                msg.ShowDialog();

                if (!TestMode && msg.OptionA) // Enable test mode
                {
                    TestMode = msg.OptionA; // retrieve choice
                    SetTestMode();
                }
                else if (TestMode && !msg.OptionA) // Disable test mode
                {
                    TestMode = msg.OptionA; // retrieve choice
                    SetGameMode();
                }
                else
                {
                    // Do nothing but stay in the current mode
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void Msg_FormClosing(object sender, FormClosingEventArgs e)
        {
            FocusInput();
        }
        #endregion

        #region Set game mode
        // Changing mode will stop the game and reset the game
        private void SetGameMode()
        {
            Log.TypermaxTest(false); // log
            // Initialize the game from start with game values
            // Must be set after clearing the super phrase properties
            StopEngine();
            GameMode = 0;
            Score = 0;
            tbTarget.Text = "";
            ResolveGameButtons();
            InitializeDictionary();
            SetRetractionLabel(1);
            SetErrorLabel(DefaultErrorLabelTest);
            mqLogo.StopSequence();
            mqLogo.StartLightShow(TextTransform.GetBlankSpace(30) + "*** TYPERMAX ***" + TextTransform.GetBlankSpace(10) + "...IS READY AND WAITING!" + TextTransform.GetBlankSpace(30) + "*** DICTIONARY: 60.000 WORDS ***", 40, false);
            InitializeFields();
            SetColorTheme();
            ShowSplash();
        }
        #endregion

        #region Set test mode
        // Changing mode will stop the game and reset the game
        private void SetTestMode()
        {
            Log.TypermaxTest(true); // log
            // Initialize the game from start with test values
            // Must be set after clearing the super phrase properties
            StopEngine();
            GameMode = 0;
            Score = 0;
            tbTarget.Text = "";
            ResolveGameButtons();
            InitializeDictionary();
            SetRetractionLabel(1);
            SetErrorLabel(DefaultTestModeErrorLabelTest);
            mqLogo.StopSequence();
            mqLogo.StartLightShow(TextTransform.GetBlankSpace(10) + TrainingModeText + TextTransform.GetBlankSpace(10) + TrainingModeText + TextTransform.GetBlankSpace(10) + TrainingModeText, 40, false);
            InitializeFields();
            SetColorTheme();
            ShowSplash();
        }
        #endregion





        #region Time Penalty bliker

        private void ResetPenaltyProperties()
        {
            SuccessfullKeystrokes = 0;
            FailedKeystrokes = 0;
            TotalTimePenalty = 0;
        }

        private void DoSuccsessfulKeystroke()
        {
            SuccessfullKeystrokes += 1;
        }

        private void DoFailedKeystroke()
        {
            FailedKeystrokes += 1;
        }

        private void DoPrimaryPenalty()
        {
            TimeOutCount -= PrimaryPenalty;
            TotalTimePenalty += PrimaryPenalty;
            BlinkPentalty(true);
        }

        private Timer tempBlinkPenaltyTimer;
        private int tempBlinkPenaltyCounter;
        private Color tempForeColor;
        private Color tempBackColor;
        private bool tempBlinkPenaltySwitch;
        private bool tempBlinkPenaltyPrimary;
        private void BlinkPentalty(bool primary)
        {
            if (tempBlinkPenaltyTimer == null)
            {
                tempBlinkPenaltyTimer = new Timer();
                tempBlinkPenaltyTimer.Interval = 100;
                tempBlinkPenaltyTimer.Tick += TempBlinkPenaltyTimer_Tick;
            }

            if (!tempBlinkPenaltyTimer.Enabled)
            {
                tempBlinkPenaltyPrimary = primary;
                // Get current colors
                tempForeColor = tbHistory.ForeColor;
                tempBackColor = tbHistory.BackColor;
                tempBlinkPenaltySwitch = false;
                tempBlinkPenaltyCounter = 0;
                tempBlinkPenaltyTimer.Enabled = true;
            }
        }

        private void TempBlinkPenaltyTimer_Tick(object sender, EventArgs e)
        {
            if (tempBlinkPenaltySwitch)
            {
                if (tempBlinkPenaltyPrimary)
                {
                    tbTarget.BackColor = tempBackColor;
                    tbTarget.ForeColor = tempForeColor;
                }
                else
                {
                    tbHistory.BackColor = tempBackColor;
                    tbHistory.ForeColor = tempForeColor;
                }

            }
            else if (!tempBlinkPenaltySwitch)
            {
                if (tempBlinkPenaltyPrimary)
                {

                    tbTarget.BackColor = tempForeColor;
                    tbTarget.ForeColor = tempBackColor;
                }
                else
                {
                    tbHistory.BackColor = tempForeColor;
                    tbHistory.ForeColor = tempBackColor;
                }
            }

            tempBlinkPenaltySwitch = !tempBlinkPenaltySwitch;
            tempBlinkPenaltyCounter += 1;


            // Set to default colors again
            if (tempBlinkPenaltyCounter >= 2)
            {
                tempBlinkPenaltyTimer.Enabled = false;
                tbTarget.BackColor = tempBackColor;
                tbTarget.ForeColor = tempForeColor;
                tbHistory.BackColor = tempBackColor;
                tbHistory.ForeColor = tempForeColor;
            }
        }

        private void DoSecondaryPenalty()
        {
            TimeOutCount -= SubsequentPenalty;
            TotalTimePenalty += SubsequentPenalty;
            BlinkPentalty(false);
        }

        private void SetErrorLabel(string text)
        {
            lbErrorDisplay.Text = text;
        }


        // Give a penalty of -10% and another 5% for each subsequent error following close
        private void PenaltyError()
        {
            if (!timerPenaltyBlinker.Enabled)
            {
                if (TimeOutCount > PrimaryPenalty)
                {
                    DoPrimaryPenalty();
                }
                SetErrorLabel("*** TYPING FAULT = TIME PENALTY ***");
                tempPenaltyBlinkCounter = 0;
                timerPenaltyBlinker.Interval = 250;
                timerPenaltyBlinker.Enabled = true;
            }
            else if (timerPenaltyBlinker.Enabled)
            {
                tempPenaltyBlinkCounter = 0;
                if (TimeOutCount > SubsequentPenalty)
                {
                    DoSecondaryPenalty();
                }
            }
        }

        private int tempPenaltyBlinkCounter;
        private void timerPenaltyBlinker_Tick(object sender, EventArgs e)
        {
            lbErrorDisplay.Visible = !lbErrorDisplay.Visible;
            tempPenaltyBlinkCounter += 1;
            if (tempPenaltyBlinkCounter >= 8)
            {
                timerPenaltyBlinker.Enabled = false;
                lbErrorDisplay.Visible = true;
                if (TestMode)
                {
                    lbErrorDisplay.Text = DefaultTestModeErrorLabelTest;
                }
                else
                {
                    lbErrorDisplay.Text = DefaultErrorLabelTest;
                }
            }
        }
        #endregion
    }
}
