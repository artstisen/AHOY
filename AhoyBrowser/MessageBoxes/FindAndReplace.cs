using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AhoyBrowser.MessageBoxes
{
    public partial class SearchAndReplace : Form
    {
        public string ResultText { get; set; }
        public bool Success { get; set; }
        private string SourceText { get; set; }
        private string TargetText { get; set; }
        private string InputText { get; set; }
        private int MatchCount { get; set; }
        private bool CaseSens { get; set; }
        private bool Selection { get; set; }
        private int startIndex { get; set; }
        private int endIndex { get; set; }
        private int targetLength { get; set; }
        private HistoryTextBox tbSourceRef;
        private Timer focusTargetTimer;

        public SearchAndReplace(HistoryTextBox tbSource)
        {
            InitializeComponent();
            MainForm.KeepAwake();
            this.Text = "Find and Replace";
            SetColorThem();
            ResultText = "";
            Success = false;
            tbSourceRef = tbSource;
            SourceText = tbSourceRef.Text;
            TargetText = "";
            InputText = "";
            CaseSens = false;
            Selection = false;
            ResetIndexes();
            ResetMatchCount();
            ResolveMatchCount();
            ResolveCaseSensButton();
            if (tbSource.SelectedText.Length > 0)
            {
                SetSelection(true);
            }
            else
            {
                ResolveSelectionButton();
            }
            focusTargetTimer = new Timer();
            focusTargetTimer.Tick += FocusTargetTimer_Tick;
            focusTargetTimer.Interval = 50;
            focusTargetTimer.Enabled = true;
            this.KeyPreview = true;
            lbCase.Text = "Case-sens";
            lbSelection.Text = "Selection";
        }

        private void FocusTargetTimer_Tick(object sender, EventArgs e)
        {
            focusTargetTimer.Enabled = false;
            FocusTarget();
        }

        private void FocusTarget()
        {
            tbTarget.Focus();
        }

        #region Colortheme
        private void SetColorThem()
        {
            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];

            // Set button appearance
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnReplace.FlatStyle = FlatStyle.Flat;
            btnReplace.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnReplace.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnReplace.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnReplace.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnReplace.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnToggleCaseSense.FlatStyle = FlatStyle.Flat;
            btnToggleCaseSense.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleCaseSense.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleCaseSense.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnToggleCaseSense.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleCaseSense.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnToggleSelection.FlatStyle = FlatStyle.Flat;
            btnToggleSelection.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnToggleSelection.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnToggleSelection.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnToggleSelection.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnToggleSelection.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // TextBoxes
            tbTarget.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbTarget.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbTarget.Font = Design.TitleFieldFont();
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbInput.Font = Design.TitleFieldFont();

            // Labels
            lbTarget.BackColor = Color.Transparent;
            lbMatches.BackColor = Color.Transparent;
            lbInput.BackColor = Color.Transparent;
            lbCase.BackColor = Color.Transparent;
            lbSelection.BackColor = Color.Transparent;

            lbTarget.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbMatches.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbInput.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbCase.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbSelection.ForeColor = AhoyColors.CurrentColorTheme()[2];
        }
        #endregion


        private void ResetMatchCount()
        {
            MatchCount = 0;
        }

        public int GetMatchCount()
        {
            return MatchCount;
        }

        private void SetSelection(bool state)
        {
            Selection = state;
            ResolveSelectedText();
        }

        private void ResetIndexes()
        {
            startIndex = 0;
            endIndex = 0;
            targetLength = 0;
        }

        private string HandleCase(string input)
        {
            if (CaseSens)
            {
                return input;
            }
            else
            {
                return input.ToLower();
            }
        }

        private string tempSubstring;
        private void RunThroughIndexes()
        {
            if (TargetText.Length > 0 && SourceText.Length > 0)
            {
                try
                {
                    tempSubstring = "";
                    targetLength = TargetText.Length;
                    for (startIndex = 0; (startIndex + targetLength - 1) < SourceText.Length; startIndex++)
                    {
                        tempSubstring = HandleCase(SourceText.Substring(startIndex, targetLength));
                        if (tempSubstring == HandleCase(TargetText))
                        {
                            MatchCount += 1;
                            startIndex += targetLength;
                        }
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
        }


        private void ResolveMatchCount()
        {
            ResetMatchCount();
            RunThroughIndexes();

            if (MatchCount > 0)
            {
                lbMatches.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                lbMatches.ForeColor = AhoyColors.CurrentColorTheme()[2];
            }

            lbMatches.Text = "Matches: " + MatchCount.ToString();
        }


        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            InputText = tbInput.Text;
        }

        private void tbTarget_TextChanged(object sender, EventArgs e)
        {
            ResetMatchCount();
            if (tbTarget.Text.Length > 0)
            {
                TargetText = tbTarget.Text;
                ResolveMatchCount();
            }
            else
            {
                TargetText = "";
                ResolveMatchCount();
            }
        }

        private void DoReplace()
        {
            if (TargetText.Length > 0 && SourceText.Length > 0)
            {
                try
                {
                    // Selection håndteres direkte på SourceText når man klikker på selection knappen
                    if (CaseSens)
                    {
                        TargetText = Regex.Escape(TargetText);
                        ResultText = Regex.Replace(SourceText, TargetText, InputText, RegexOptions.None);
                    }
                    else
                    {
                        TargetText = Regex.Escape(TargetText);
                        ResultText = Regex.Replace(SourceText, TargetText, InputText, RegexOptions.IgnoreCase);
                    }

                    // Return selection only
                    if (Selection)
                    {
                        tbSourceRef.SelectedText = ResultText;
                    }
                    else // Return all
                    {
                        tbSourceRef.Text = ResultText;
                    }
                }
                catch (Exception e)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(e.Message, MessageText.FindAndReplaceCaption(), false);
                    msg.ShowDialog();
                }
            }
            else
            {
                ResultText = "";
                Success = false;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please revise your input", MessageText.FindAndReplaceCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
                FocusTarget();
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MatchCount > 0)
            {
                DoReplace();
                Success = true;
                this.Close();
            }
            else
            {
                ResultText = "";
                Success = false;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please revise your input", MessageText.FindAndReplaceCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
                FocusTarget();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResultText = "";
            Success = false;
            this.Close();
        }

        private void ResolveCaseSensButton()
        {
            if (CaseSens)
            {
                btnToggleCaseSense.Text = "ON";
                btnToggleCaseSense.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnToggleCaseSense.Text = "OFF";
                btnToggleCaseSense.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private void ResolveSelectionButton()
        {
            if (Selection)
            {
                btnToggleSelection.Text = "ON";
                btnToggleSelection.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnToggleSelection.Text = "OFF";
                btnToggleSelection.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private void btnToggleCaseSense_Click(object sender, EventArgs e)
        {
            CaseSens = !CaseSens;
            ResolveCaseSensButton();
            ResolveMatchCount();
        }

        private void btnToggleSelection_Click(object sender, EventArgs e)
        {
            Selection = !Selection;
            ResolveSelectedText();
        }

        private void ResolveSelectedText()
        {
            if (Selection)
            {
                if (tbSourceRef.SelectedText.Length > 0)
                {
                    SourceText = tbSourceRef.SelectedText;
                }
                else
                {
                    Selection = false;
                }
            }
            else
            {
                SourceText = tbSourceRef.Text;
            }

            ResolveSelectionButton();
            ResolveMatchCount();
        }

        private void SearchAndReplace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnReplace_Click(this, null);
            }
        }


    }
}
