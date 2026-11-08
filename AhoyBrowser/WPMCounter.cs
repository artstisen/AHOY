using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class WPMCounter
    {
        private static Timer SpeedTimer;
        private static decimal InputCount { get; set; }
        public static void InitializeSpeedTimer()
        {
            InputCount = 0;
            SpeedTimer = new Timer();
            SpeedTimer.Interval = 1000; // One second ticks
            SpeedTimer.Tick += timerWpmCounter_Tick;
        }

        // Test key event to check if it is a control button
        private static bool IsControlButton(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
    e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
            {
                return true;
            }
            else if (e.Control || e.KeyCode == Control.ModifierKeys || e.KeyCode == Keys.Space || e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // "Hunt and peck" typist types around 30 WPM
        // Average professional types 50-80 words per minutes (WPM) (5 chars is regarded as one word)
        // Professionals: 70-95 WPM
        // Worlds fastest typist: 216 WPM in 1 minute on IBM electric in 1946

        /// <summary>
        /// Submit keypress
        /// </summary>
        public static void SubmitInputCount(KeyEventArgs e)
        {
            if (!IsControlButton(e))
            {
                if (!SpeedTimer.Enabled)
                {
                    SpeedTimer.Enabled = true;
                }

                tempKeepAliveCounter += 0.5m;
                InputChanged();
            }
        }

        /// <summary>
        /// Retrieve progress bar
        /// </summary>
        /// <returns></returns>
        public static string GetProgressBar()
        {
            return tempWPMStringOutput;
        }

        /// <summary>
        /// Retrieve progress bar with wordcount
        /// </summary>
        /// <returns></returns>
        private static string tempWordCount;
        private static string tempGroupOfWords;
        public static string GetProgressBarWithWordCount(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                // Clear variable
                tempWordCount = "";
                tempGroupOfWords = "";
                // Count characters
                tempWordCount += "Character count: " + input.Length + " / ";
                // Count words ("^\s.?,"  and "\w")
                tempGroupOfWords = input;
                var matchesByAlphaNumeric = Regex.Matches(tempGroupOfWords,
                        @"[^\s.?,]+", RegexOptions.CultureInvariant | RegexOptions.Multiline
                        | RegexOptions.IgnoreCase);

                tempWordCount += "Word count: " + matchesByAlphaNumeric.Count.ToString();
                return tempWordCount + WPMCounter.GetProgressBar();
            }
            else
            {
                return tempWordCount;
            }
        }

        private const int ProgressBarLength = 50;
        private const string ProgressBarSymbol = "|"; // the symbol to show
        private const decimal MaximumValue = 150m;
        private static string TempProgressBarOutput;
        private static string RetrieveScore(decimal currentValue)
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
            }

            // Output value
            if (TempProgressBarOutput.Length > 0)
            {
                return GetBlankSpace(5) + TempProgressBarOutput;
            }
            else
            {
                return TempProgressBarOutput;
            }
        }

        private static string tempStringBLK;
        private static string GetBlankSpace(int count)
        {
            tempStringBLK = "";
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    tempStringBLK += " ";
                }
            }
            return tempStringBLK;
        }

        private static decimal tempKeepAliveCounter;
        static string tempWPMStringOutput = "";
        static decimal _strWPM; // WPM = Words per minute, CPM = Characters per minute
        static decimal _iWPMCount;
        static decimal _iWPMTotalTime;
        public static void ResetWPMcounter()
        {
            _iWPMTotalTime = 0;
            _strWPM = 0;
            _iWPMCount = 0;
            tempWPMStringOutput = "";
            InputCount = 0;
        }

        public static void ClearWPMCounter()
        {
            _iWPMTotalTime = 0;
            _strWPM = 0;
            _iWPMCount = 0;
            InputCount = 0;
            tempWPMStringOutput = "";
            TempProgressBarOutput = "";
        }

        private static void InputChanged()
        {
            try
            {
                _strWPM += 1;

                if (_iWPMTotalTime > 0 && _strWPM >= 5)
                {
                    // Note: divides by 4 keystrokes per word (the WPM standard above uses 5 characters);
                    // kept as-is so scores match earlier versions
                    _iWPMCount = ((60 / _iWPMTotalTime) * (_strWPM / 4));
                    if (_iWPMCount >= 0)
                        tempWPMStringOutput = RetrieveScore(_iWPMCount) + " (" + _iWPMCount.ToString("0") + "WPM)"; // + ((60 / _iWPMTotalTime) * (_strWPM)).ToString("0") + "CPM)"; // WPM/CPM
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private static void timerWpmCounter_Tick(object sender, EventArgs e)
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

    }
}
