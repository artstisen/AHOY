using System;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    // Marquee control version 1.2 by LBS
    // (last update 12-09-2016, adding Halt/Release sequence + CalculateValues etc.)
    // (last update 19-09-2016, dynamic forward/backwards transport + Overseer-timer etc.)
    // (last update 20-02-2017, "update" method to display label for smooth transitions)
    public partial class Marquee : UserControl
    {
        private string DisplayText;
        private string DisplayString;
        private int DisplayStringLength;
        private int DisplayWindow;
        private int DisplayEnd;
        private int StartIndex;
        private int EndIndex;
        private int PauseDuration;
        private int PauseCounter;
        private bool Paused;
        private bool Halted;
        private bool RunOnce;
        private Timer SequenceTimer;
        private Timer OverseerTimer;
        private bool TestMode;
        private ToolTip ControlToolTip;
        private bool Direction;
        private int TransportTimeout;
        private bool ShowDirectionalChar { get; set; }

        public Marquee()
        {
            InitializeComponent();
            ShowDirectionalChar = true;
            InitializeLightShow();
            lbOutput.MouseClick += LbOutput_Click;
            lbOutput.Cursor = Cursors.Default;
            ControlToolTip = new ToolTip();
            ControlToolTip.OwnerDraw = true;
            ControlToolTip.Draw += toolTip1_Draw;
            this.ControlToolTip.SetToolTip(lbOutput, "Use Left/Right Click to control Transport");
            // Set default colors
            SetFontAndColors(new Font(AhoyBrowser.Design.GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Color.White, Color.Black, Color.White, Color.Black);
            TestMode = true;
            Direction = false;
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
        /// <summary>
        /// Test Mode will display a default string and stop (RunOnce)
        /// when clicking the control
        /// </summary>
        /// <param name="status"></param>
        public void SetTestMode(bool status)
        {
            TestMode = status;
        }

        public void SetDirectionalChar(bool onOff)
        {
            ShowDirectionalChar = onOff;
        }

        private bool IgnorePauses;
        private bool WasPaused;
        public event MouseEventHandler LightShowClick;
        private void LbOutput_Click(object sender, MouseEventArgs e)
        {
            // Reset variables
            WasPaused = IsPaused();
            IgnorePauses = true;
            ResetTimeout();
            ResetPauseCounter();

            // Open menu with right button
            if (e.Button == MouseButtons.Right)
            {
                if (IsRunning() && !Direction)
                {
                    // Run left to right
                    Direction = true;
                }
                else
                {
                    // Run left to right
                    Direction = true;
                    if (!IsRunning())
                    {
                        if (TestMode)
                        {
                            StartTestShow();
                        }
                        else
                        {
                            ReleaseSequence();
                        }
                    }
                    else
                    {
                        if (!WasPaused)
                        {
                            HaltSequence();
                        }
                    }
                }
            }

            // Start/Stop using left button
            if (e.Button == MouseButtons.Left)
            {
                if (IsRunning() && Direction)
                {
                    // Run right to left
                    Direction = false;
                }
                else
                {
                    // Run right to left
                    Direction = false;

                    if (!IsRunning())
                    {
                        if (TestMode)
                        {
                            StartTestShow();
                        }
                        else
                        {
                            ReleaseSequence();
                        }
                    }
                    else
                    {
                        if (!WasPaused)
                        {
                            HaltSequence();
                        }
                    }
                }
            }

            // Bubble the event up to the parent
            // Possible to subscribe on the OnClick
            // Event in host form
            if (this.LightShowClick != null)
            {
                this.LightShowClick(this, e);
            }
        }

        private void StartTestShow()
        {
            TestMode = false;
            PauseDuration = 175;
            StartLightShow("Marquee test mode enabled...", 20, false);
        }



        private string GetDirectionChar()
        {
            if (ShowDirectionalChar)
            {
                if (!IsRunning() && DisplayStringLength > 0)
                {
                    return " ■";
                }
                else if (!IsRunning() && DisplayStringLength == 0)
                {
                    return "";
                }
                else if (IsRunning() && !Direction)
                {
                    return " ◄";
                }
                else if (IsRunning() && Direction)
                {
                    return " ►";
                }
                else
                { return " ?"; }
            }
            else
            {
                return "";
            }
        }

        private void InitializeLightShow()
        {
            DisplayText = "";
            DisplayString = "";
            DisplayStringLength = 0;
            DisplayWindow = 20;
            DisplayEnd = 0;
            StartIndex = 0;
            EndIndex = 0;
            PauseDuration = 0;
            PauseCounter = 0;
            TransportTimeout = 0;
            Halted = false;
            SequenceTimer = new Timer();
            SequenceTimer.Interval = 50;
            SequenceTimer.Tick += LightShowTimer_Tick;
            OverseerTimer = new Timer();
            OverseerTimer.Interval = 1000;
            OverseerTimer.Tick += CustodianTimer_Tick;
            OverseerTimer.Enabled = true;
            PauseDuration = 175;
            Paused = false;
            RunOnce = false;
            lbOutput.Text = GetDirectionChar();
            SetTextAlignment(ContentAlignment.MiddleRight);
        }

        public void SetTextAlignment(ContentAlignment alignment)
        {
            lbOutput.TextAlign = alignment;
        }

        /// <summary>
        /// Set display font and colors
        /// </summary>
        /// <param name="displayFont"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="toolTipForeColor"></param>
        /// <param name="toolTipBackColor"></param>
        public void SetFontAndColors(Font displayFont, Color foreColor, Color backColor, Color toolTipForeColor, Color toolTipBackColor)
        {
            lbOutput.ForeColor = foreColor;
            lbOutput.BackColor = Color.Transparent;
            this.BackColor = backColor;
            lbOutput.Font = displayFont;
            ControlToolTip.BackColor = toolTipBackColor;
            ControlToolTip.ForeColor = toolTipForeColor;
        }



        // ------------------------------- START WITH PREDEFINE ARGS ---------------------------------------
        // Set for predefined start
        private bool tempWasRunning;
        /// <summary>
        /// Set display text dynamically (during a running or stopped sequence)
        /// </summary>
        /// <param name="displayText"></param>
        /// <param name="displayWindow"></param>
        public void SetDisplayText(string displayText, int displayWindow)
        {
            // If setting displaytext while running
            // stop, add the text and resume.
            // Else just ad the text
            tempWasRunning = IsRunning();
            if (tempWasRunning)
            {
                StopSequence();
            }

            // Add display text
            CalculateValues(displayText, displayWindow);

            // Resume if sequence was running
            if (tempWasRunning)
            {
                ResumeSequence();
                tempWasRunning = false;
            }
        }

        
        /// <summary>
        /// Set pause duration in milliseconds between each loop
        /// </summary>
        /// <param name="milliseconds"></param>
        public void SetPauseDuration(int milliseconds)
        {
            PauseDuration = milliseconds;
        }


        /// <summary>
        /// Stop sequence (halt timer) and clear message
        /// </summary>
        public void StopSequence()
        {
            if (SequenceTimer.Enabled)
            {
                SequenceTimer.Enabled = false;
                lbOutput.Text = GetDirectionChar();
            }
        }
        /// <summary>
        /// Stop sequence (halt timer) without clearing message
        /// </summary>
        public void HaltSequence()
        {
            if (SequenceTimer.Enabled)
            {
                SequenceTimer.Enabled = false;
                lbOutput.Text = RevolveSequence() + GetDirectionChar();
                Halted = true;
            }
        }
        /// <summary>
        /// Resume sequence (halt timer) without clearing message
        /// </summary>
        public void ReleaseSequence()
        {
            if (!SequenceTimer.Enabled)
            {
                lbOutput.Text = RevolveSequence() + GetDirectionChar();
                SequenceTimer.Enabled = true;
            }
        }
        /// <summary>
        /// Resume sequence (enable timer) and clear message
        /// </summary>
        public void ResumeSequence()
        {
            SequenceTimer.Enabled = true;
            lbOutput.Text = GetDirectionChar();
        }
        // ------------------------------- START WITH PREDEFINE ARGS END -----------------------------------

        private void CalculateValues(string displayText, int displayWindow)
        {
            PauseCounter = 0;
            DisplayWindow = displayWindow;
            DisplayText = CleanString(displayText);
            DisplayString = GetBlankSpace(DisplayWindow) + DisplayText + GetBlankSpace(DisplayWindow);
            DisplayStringLength = DisplayString.Length;
            DisplayEnd = DisplayString.Length - DisplayWindow;
        }

        // Remove linebreaks
        private string tempStringCleaning;
        private string CleanString(string displayText)
        {
            tempStringCleaning = displayText;
            tempStringCleaning = tempStringCleaning.Replace('\n', ' ');
            tempStringCleaning = tempStringCleaning.Replace('\r', ' ');
            return tempStringCleaning;
        }


        // --------------------------------------- START ARGS -----------------------------------------------
        /// <summary>
        /// Start sequence with arguments
        /// </summary>
        /// <param name="displayText"></param>
        /// <param name="displayWindow"></param>
        /// <param name="runOnce"></param>
        public void StartLightShow(string displayText, int displayWindow, bool runOnce)
        {
            if (!SequenceTimer.Enabled && displayText.Length > 0)
            {
                Paused = false;
                CalculateValues(displayText, displayWindow);
                ResetLightShowIndex();
                SequenceTimer.Enabled = true;
                RunOnce = runOnce;
            }
        }
        // --------------------------------------- START ARGS END -------------------------------------------

        /// <summary>
        /// Check if timer is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return SequenceTimer.Enabled;
        }

        /// <summary>
        /// Check if a pause is in progress
        /// </summary>
        /// <returns></returns>
        public bool IsPaused()
        {
            return Paused;
        }


        // Timer sequence
        private void LightShowTimer_Tick(object sender, EventArgs e)
        {
            if (!Paused)
            {
                lbOutput.SuspendLayout();
                lbOutput.Text = RevolveSequence() + GetDirectionChar();
                lbOutput.ResumeLayout();
                lbOutput.Update();
            }
            else
            {
                if (RunOnce)
                {
                    StopSequence();
                    RunOnce = false;
                    return;
                }

                PauseCounter += 1;
                if (PauseCounter >= PauseDuration)
                {
                    ResetPauseCounter();
                }
            }
            IgnorePauses = false;
        }

        private void ResetPauseCounter()
        {
            Paused = false;
            PauseCounter = 0;
        }

        private void CustodianTimer_Tick(object sender, EventArgs e)
        {
            // Restart sequence after 15 seconds, if halted
            if (Halted)
            {
                TransportTimeout += 1;
                if (TransportTimeout >= 15)
                {
                    ResetTimeout();
                    Direction = false;
                    ReleaseSequence();
                }
            }
        }

        private void ResetTimeout()
        {
            Halted = false;
            TransportTimeout = 0;
        }



        private static string tempString;
        internal string GetBlankSpace(int count)
        {
            tempString = "";
            for (int i = 0; i < count; i++)
            {
                tempString += " ";
            }
            return tempString;
        }

        private void ResetLightShowIndex()
        {
            if (!Direction)
            {
                StartIndex = 0;
                EndIndex = DisplayWindow;
            }
            else
            {
                StartIndex = DisplayStringLength - DisplayWindow;
                EndIndex = DisplayWindow;
            }
        }

        private string RevolveSequence()
        {
            if (!Direction)
            {
                return AdvanceSequenceByOne();
            }
            else
            {
                return RearwardSequenceByOne();
            }
        }

        private string AdvanceSequenceByOne()
        {
            if (StartIndex < DisplayStringLength && StartIndex < DisplayEnd)
            {
                StartIndex += 1;
            }
            else
            {
                // Pointer hits here when a full text has passed the window
                if (!IgnorePauses)
                {
                    Paused = true;
                }
                ResetLightShowIndex();
            }
            return GetDisplayString();
        }

        private string RearwardSequenceByOne()
        {
            if (StartIndex > 0 && EndIndex > 0)
            {
                StartIndex -= 1;
            }
            else
            {
                // Pointer hits here when a full text has passed the window
                if (!IgnorePauses)
                {
                    Paused = true;
                }
                ResetLightShowIndex();
            }
            return GetDisplayString();
        }

        static string tempDisplayString;
        private string GetDisplayString()
        {
            if (DisplayStringLength > 0)
            {
                tempDisplayString = DisplayString.Substring(StartIndex, EndIndex);
            }
            return tempDisplayString;
        }

    }
}
