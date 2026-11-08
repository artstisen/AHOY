using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AhoyBrowser
{
    public partial class MicroCalculator : UserControl
    {

        /* AHOY! MicroCalc by LBS v1.00 31-01-2018 */
        /* AHOY! MicroCalc by LBS v1.01 02-02-2018 */
        /* AHOY! MicroCalc by LBS v1.02 15-02-2018 */
        /* AHOY! MicroCalc by LBS v1.03 16-02-2018 */

        private MathParser Calculator { get; set; }
        private List<string> HistoryList { get; set; }
        private int HistoryIndex { get; set; }
        private int ResetCount { get; set; }
        private const string CalculatorTitle = "Calculator";
        public string LatestResult { get; set; }
        private int Version { get; set; }

        public MicroCalculator()
        {
            InitializeComponent();
            // Initialize Calculator
            Calculator = new MathParser();
            HistoryList = new List<string>();
            HistoryIndex = 0;
            ResetCount = 0;
            tbInput.ContextMenuStrip = contextMenuBasic;
            // Transparent label backgrounds
            lbOutput.BackColor = System.Drawing.Color.Transparent;
            lbClose.BackColor = System.Drawing.Color.Transparent;
            lbIndicator.BackColor = System.Drawing.Color.Transparent;
            lbClear.BackColor = System.Drawing.Color.Transparent;
            lbRecallNext.BackColor = System.Drawing.Color.Transparent;
            lbRecallPrevious.BackColor = System.Drawing.Color.Transparent;
            lbCopy.BackColor = System.Drawing.Color.Transparent;
            lbTitle.BackColor = System.Drawing.Color.Transparent;
            lbTitle.Text = CalculatorTitle;

            Version = 0;
            LatestResult = "";
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            tbInput.TextChanged += TbInput_TextChanged;
            SetColorTheme();
            FocusInput(false);
        }

        public void SetVersion(int version)
        {
            Version = version;
        }

        // Change font size on input textbox while typing
        private void TbInput_TextChanged(object sender, EventArgs e)
        {
            if (tbInput.Text.Length > 29 && tbInput.Text.Length < 37)
            {
                tbInput.Font = Design.MessageboxSmallFont();
            }
            else if (tbInput.Text.Length >= 37 && tbInput.Text.Length < 45)
            {
                tbInput.Font = Design.MessageboxXSmallFont();
            }
            else if (tbInput.Text.Length >= 45)
            {
                tbInput.Font = Design.MessageboxTinyFont();
            }
            else
            {
                tbInput.Font = Design.ContentFieldFont();
            }
        }

        /*
         
            Public method calls:
            --------------------

            // In the constructor:
            // Calculator
            microCalculator1.HideControl();

            // In the set colortheme:
            // Update Micro Calculator
            microCalculator1.SetColorTheme();
             
            // In the toggle calculator method:
            // Show/Hide Micro Calculator
            private void ToggleMicroCalculator()
            {
                if (!microCalculator1.Visible)
                {
                    microCalculator1.ShowControl();
                }
                else
                {
                    microCalculator1.HideControl();
                }
            }
        */

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        public void SetColorTheme()
        {
            // Buttons
            btnGo.FlatStyle = FlatStyle.Flat;
            btnGo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnGo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnGo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnGo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnGo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnGo.Text = "GO";

            // Tooltip
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];

            SetupHelpPictureBox();

            // Fonts
            lbOutput.Font = Design.ContentFieldFont();
            tbInput.Font = Design.ContentFieldFont();
            lbIndicator.Font = Design.ContentFieldFont();

            // Colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            pnlInputBox.BackColor = AhoyColors.CurrentColorTheme()[29];

            lbClose.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbClear.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbRecallNext.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbRecallPrevious.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbCopy.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbOutput.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbOutput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            lbIndicator.ForeColor = AhoyColors.CurrentColorTheme()[30];
        }

        private void SetupHelpPictureBox()
        {
            picHelp.BackColor = System.Drawing.Color.Transparent;
            picHelp.BackgroundImage = ButtonBitmaps.HelpButtonHighlight();
            picHelp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DoCalculation();
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            Reaction();
            if (e.KeyCode == Keys.Enter)
            {
                DoCalculation();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ResetCalculator();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                tbInput.Text = GetPreviousHistory();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                tbInput.Text = GetNextHistory();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void lbRecallPrevious_Click(object sender, EventArgs e)
        {
            tbInput.Text = GetPreviousHistory();
        }

        private void lbRecallNext_Click(object sender, EventArgs e)
        {
            tbInput.Text = GetNextHistory();
        }


        #region History
        private string GetPreviousHistory()
        {
            ResetCloseCounter();
            if (HistoryList != null && HistoryList.Count > 0)
            {
                if (HistoryIndex >= 1)
                {
                    HistoryIndex -= 1;
                }
                else
                {
                    HistoryIndex = HistoryList.Count - 1;
                }

                return HistoryList[HistoryIndex];
            }
            else
            {
                return "";
            }
        }

        private string GetNextHistory()
        {
            ResetCloseCounter();
            if (HistoryList != null && HistoryList.Count > 0)
            {
                if (HistoryIndex < HistoryList.Count - 1)
                {
                    HistoryIndex += 1;
                }
                else
                {
                    HistoryIndex = 0;
                }

                return HistoryList[HistoryIndex];
            }
            else
            {
                return "";
            }
        }

        private void SubmitToHistory(string input)
        {
            if (HistoryList != null)
            {
                HistoryList.Add(input);
            }
            TruncateHistoryList();
        }

        private void TruncateHistoryList()
        {
            if (HistoryList != null && HistoryList.Count > 30)
            {
                do
                {
                    HistoryList.RemoveAt(0);
                } while (HistoryList.Count > 30);
            }
        }
        #endregion

        private string tempResult;
        private string tempInput;
        private string tempOutput;
        private void DoCalculation()
        {
            Reaction();
            ResetCloseCounter();
            tempResult = "";
            tempInput = "";
            tempOutput = "";
            tempInput = tbInput.Text.Trim().ToLower(); // Trim + ToLower

            if (tempInput == "cls" || tempInput == "clear" || tempInput == "reset")
            {
                SimpleReset();
                return;
            }
            else if (tempInput == "help" || tempInput == "about" || tempInput == "?")
            {
                DisplayHelp();
                FocusInput(true);
                return;
            }
            else if (tempInput == "copy")
            {
                CopyResult();
                FocusInput(true);
                return;
            }
            else if (tempInput == "exit" || tempInput == "close" || tempInput == "hide")
            {
                HideControl(true);
                FocusInput(true);
                return;
            }
            else if (tempInput == "hello" || tempInput == "hej" || tempInput == "davs" || tempInput == "dav" || tempInput == "goddag" || tempInput == "jaja" || tempInput == "asdf" || tempInput == "bla")
            {
                SetOutputText("Hello :)");
                FocusInput(true);
                return;
            }

            // Fetch previous calculator result
            if (LatestResult != null && LatestResult.Length > 0)
            {
                if (tempInput.Length > 0 && IsMathSymbol(tempInput[0]))
                {
                    tempInput = LatestResult + tempInput;
                }
            }

            // Perform calculation  
            tempResult = Calculator.DoCalculation(tempInput);
            if (tempResult == "nocalc")
            {
                lbOutput.Font = Design.ContentFieldFont();
                SetOutputText("Please revise your input...");
                if (tbInput.Text.Length > 0)
                {
                    tbInput.SelectAll();
                }
                FocusInput(false);
            }
            else
            {
                SubmitToHistory(tempInput);
                tbInput.Text = "";
                tempOutput = Calculator.GetProcessedInputFormula() + " = " + tempResult;

                if (tempOutput.Length > 29 && tempOutput.Length < 37)
                {
                    lbOutput.Font = Design.MessageboxSmallFont();
                }
                else if (tempOutput.Length >= 37 && tempOutput.Length < 45)
                {
                    lbOutput.Font = Design.MessageboxXSmallFont();
                }
                else if (tempOutput.Length >= 45)
                {
                    lbOutput.Font = Design.MessageboxTinyFont();
                }
                else
                {
                    lbOutput.Font = Design.ContentFieldFont();
                }

                //toolTip1.SetToolTip(lbOutput, tempOutput);
                SetOutputText(tempOutput);
                LatestResult = tempResult;
                FocusInput(false);
            }
        }

        private void FocusInput(bool clearContent)
        {
            if (clearContent)
            {
                tbInput.Text = "";
            }
            tbInput.Focus();
        }


        private void ResetCloseCounter()
        {
            ResetCount = 0;
        }

        private void ResetCalculator()
        {
            ResetCount += 1;
            if (ResetCount >= 2 || LatestResult == "")
            {
                HideControl(true);
            }
            else
            {
                // Reset
                HistoryIndex = 0;
                LatestResult = "";
                SetOutputText("Ready");
                lbOutput.Font = Design.ContentFieldFont();
                FocusInput(true);
            }
        }

        private void SimpleReset()
        {
            // Reset
            HistoryIndex = 0;
            LatestResult = "";
            SetOutputText("Ready");
            lbOutput.Font = Design.ContentFieldFont();
            FocusInput(true);
        }

        private void lbClear_Click(object sender, EventArgs e)
        {
            SimpleReset();
        }

        private bool IsMathSymbol(char input)
        {
            if (input == '/' || input == '*' || input == '-' || input == '+')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Copy result to clipboard
        private void lbOutput_Click(object sender, EventArgs e)
        {
            CopyResult();
        }

        private void lbCopy_Click(object sender, EventArgs e)
        {
            CopyResult();
        }

        private void CopyResult()
        {
            if (LatestResult != null && LatestResult.Length > 0)
            {
                Clipboard.SetText(LatestResult);
                SetOutputText("[copy] " + LatestResult);
                FocusInput(false);
            }
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            HideControl(true);
        }

        private void Reaction()
        {
            MainForm.KeepAwake();
        }


        #region Cut/Copy/Paste
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }
        #endregion



        #region Help
        private void picHelp_Click(object sender, EventArgs e)
        {
            DisplayHelp();
        }

        private void DisplayHelp()
        {
            // Show help message, just the basic calc info...
            AhoyInfoMessageBox msg = new AhoyInfoMessageBox(IO.CalculatorTextReader(), MessageText.AhoyCaption() + " " + CalculatorTitle, false, true, Settings.DefaultOpacity);
            msg.TopMost = true;
            msg.ShowDialog();
        }
        #endregion

        public void ShowControl()
        {
            this.Visible = true;
            this.BringToFront();
            if (Version == 0)
            {
                MainForm.ShowMicroCalcMain = true;
                MainForm.SettingsHasChanges = true;
            }
            else if (Version == 1)
            {
                MainForm.ShowMicroCalcUE = true;
                MainForm.SettingsHasChanges = true;
            }
            FocusInput(false);
        }

        public void CheckShowControl()
        {
            if (Version == 0 && MainForm.ShowMicroCalcMain)
            {
                this.Visible = true;
                this.BringToFront();
                FocusInput(false);
            }
            else if (Version == 1 && MainForm.ShowMicroCalcUE)
            {
                this.Visible = true;
                this.BringToFront();
                FocusInput(false);
            }
        }

        public void HideControlWithoutSavingState()
        {
            this.Visible = false;
            ReturnFocus();
        }

        public void HideControl(bool forceHide)
        {
            if (forceHide)
            {
                this.Visible = false;
                if (Version == 0)
                {
                    MainForm.ShowMicroCalcMain = false;
                    MainForm.SettingsHasChanges = false;
                }
                else if (Version == 1)
                {
                    MainForm.ShowMicroCalcUE = false;
                    MainForm.SettingsHasChanges = false;
                }
                ReturnFocus();
            }
            else
            {
                if (!tbInput.Focused)
                {
                    FocusInput(false);
                }
                else
                {
                    this.Visible = false;
                    if (Version == 0)
                    {
                        MainForm.ShowMicroCalcMain = false;
                        MainForm.SettingsHasChanges = false;
                    }
                    else if (Version == 1)
                    {
                        MainForm.ShowMicroCalcUE = false;
                        MainForm.SettingsHasChanges = false;
                    }
                    ReturnFocus();
                }
            }
        }

        #region Return focus to control
        private void ReturnFocus()
        {
            try
            {
                if (tempControl != null)
                {
                    tempControl.Focus();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private Control tempControl;
        public void ReturnFocusTo(Control ctrl)
        {
            tempControl = ctrl;
        }
        #endregion

        #region Set output text and blink color
        private Timer outputTimer;
        private void SetOutputText(string text)
        {
            if (outputTimer == null)
            {
                outputTimer = new Timer();
                outputTimer.Tick += OutputTimer_Tick;
                outputTimer.Interval = 100;
            }
            lbOutput.Text = text;
            lbOutput.ForeColor = AhoyColors.CurrentColorTheme()[6];
            outputTimer.Enabled = true;
        }

        private void OutputTimer_Tick(object sender, EventArgs e)
        {
            outputTimer.Enabled = false;
            lbOutput.ForeColor = AhoyColors.CurrentColorTheme()[30];
        }
        #endregion

    }
}
