using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class RandomNumbers : Form
    {
        private static Random rand;
        private Regex regex;
        private List<int> previousValues;
        private List<int> selectedValues;
        private int FromValue;
        private int ToValue;
        private int DiceCount;
        private const int MaxValue = 1000;
        private const int MaxDiceCount = 100;
        private int randValue;

        public RandomNumbers()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            tbOutput.ScrollBars = ScrollBars.Vertical;
            tbMultipleDice.ScrollBars = ScrollBars.Vertical;
            InitializeProperties();
            SetColorTheme();
        }

        private void InitializeProperties()
        {
            rand = new Random();
            regex = new Regex(@"\d+"); // A number, one or more times
            previousValues = new List<int>();
            selectedValues = new List<int>();
            tbOutput.ContextMenuStrip = ahoyContextMenuStrip1;
            tbMultipleDice.ContextMenuStrip = ahoyContextMenuStrip1;
            tbFromNumber.ContextMenuStrip = ahoyContextMenuStrip1;
            tbToNumber.ContextMenuStrip = ahoyContextMenuStrip1;
            tbDiceCount.ContextMenuStrip = ahoyContextMenuStrip1;
            tbSingleDice.ContextMenuStrip = ahoyContextMenuStrip1;

            SetInstructions();
            SetDefaultValues();
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void SetColorTheme()
        {
            btnGenerate.FlatStyle = FlatStyle.Flat;
            btnGenerate.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnGenerate.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnGenerate.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnReset.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnReset.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            this.panel1.BackColor = AhoyColors.CurrentColorTheme()[1];
            btnGenerate.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnGenerate.ForeColor = AhoyColors.CurrentColorTheme()[6];
            btnReset.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnReset.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Text boxes
            this.tbFromNumber.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbFromNumber.ForeColor = AhoyColors.CurrentColorTheme()[6];
            this.tbToNumber.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbToNumber.ForeColor = AhoyColors.CurrentColorTheme()[6];
            this.tbDiceCount.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbDiceCount.ForeColor = AhoyColors.CurrentColorTheme()[6];
            this.tbSingleDice.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbSingleDice.ForeColor = AhoyColors.CurrentColorTheme()[3];
            this.tbOutput.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbOutput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            this.tbMultipleDice.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbMultipleDice.ForeColor = AhoyColors.CurrentColorTheme()[3];

            tbFromNumber.BorderStyle = BorderStyle.None;
            tbToNumber.BorderStyle = BorderStyle.None;
            tbDiceCount.BorderStyle = BorderStyle.None;
            tbSingleDice.BorderStyle = BorderStyle.None;
            tbOutput.BorderStyle = BorderStyle.None;
            tbMultipleDice.BorderStyle = BorderStyle.None;

            tbFromNumber.Font = Design.FieldFont(2);
            tbToNumber.Font = Design.FieldFont(2);
            tbDiceCount.Font = Design.FieldFont(2);
            tbSingleDice.Font = Design.FieldFont(2);
            tbOutput.Font = Design.FieldFont(2);
            tbMultipleDice.Font = Design.FieldFont(2);
            lbInstruction.Font = Design.FieldFont(1);

            // Labels
            lbFrom.BackColor = Color.Transparent;
            lbTo.BackColor = Color.Transparent;
            lbDice.BackColor = Color.Transparent;
            lbDiceCount.BackColor = Color.Transparent;
            lbUnique.BackColor = Color.Transparent;
            lbInstruction.BackColor = Color.Transparent;

            lbFrom.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTo.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbDice.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbDiceCount.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbUnique.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbInstruction.ForeColor = AhoyColors.CurrentColorTheme()[2];

            // Tooltip
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];
        }

        private void SetDefaultValues()
        {
            try
            {
                previousValues.Clear();
                selectedValues.Clear();
                tbOutput.Text = "";
                tbSingleDice.Text = "";
                tbMultipleDice.Text = "";
                tbFromNumber.Text = "1";
                tbToNumber.Text = "6";
                tbDiceCount.Text = "3";
                FocusToValue();
            }
            catch
            {
                // Do nothing
            }
        }

        private void FocusToValue()
        {
            tbToNumber.Focus();
            if (tbToNumber.Text.Length > 0)
            {
                tbToNumber.Select(tbToNumber.Text.Length, 0);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            SetDefaultValues();
        }

        private bool ValueExists(int input)
        {
            if (previousValues != null)
            {
                foreach (int test in previousValues)
                {
                    if (input == test)
                    {
                        return true;
                    }
                }
                // Add value
                previousValues.Add(input);
                return false;
            }
            else
            {
                return false;
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        string tempOutput = "";
        private void Generate()
        {
            tbOutput.Text = "";
            tbMultipleDice.Text = "";
            tempOutput = "";
            try
            {
                #region Check numeral
                // Check the From and To values are numerals
                if (regex.IsMatch(tbFromNumber.Text.Trim()))
                {
                    FromValue = Convert.ToInt32(tbFromNumber.Text.Trim());
                }
                else
                {
                    Error();
                    return;
                }
                if (regex.IsMatch(tbToNumber.Text.Trim()))
                {
                    ToValue = Convert.ToInt32(tbToNumber.Text.Trim());
                }
                else
                {
                    Error();
                    return;
                }
                if (regex.IsMatch(tbDiceCount.Text.Trim()))
                {
                    DiceCount = Convert.ToInt32(tbDiceCount.Text.Trim());
                }
                else
                {
                    Error();
                    return;
                }
                #endregion

                // Genrate if numbers are within limits and not equal or reversed
                if ((FromValue >= 0 && FromValue < MaxValue) && (ToValue > FromValue && ToValue <= MaxValue) && (DiceCount >= 0 && DiceCount <= MaxDiceCount))
                {
                    // Clear lists
                    selectedValues.Clear();
                    previousValues.Clear();

                    // Generate single dice
                    tbSingleDice.Text = rand.Next(FromValue, ToValue + 1).ToString();

                    // Generate multiple dice
                    if (DiceCount >= 1)
                    {
                        tempOutput = "";
                        for (int i = 0; i < DiceCount; i++)
                        {
                            tempOutput += "Dice " + (i + 1).ToString() + ": " + rand.Next(FromValue, ToValue + 1).ToString() + Environment.NewLine;
                        }

                        tbMultipleDice.Text = tempOutput;
                    }

                    // Generate unique values
                    // Iterate to get the list of random numbers
                    for (int i = 0; i <= (ToValue - FromValue); i++)
                    {
                        // Keep generating randoms until an unused value is found
                        do
                        {
                            randValue = rand.Next(FromValue, ToValue + 1);
                        }
                        while (ValueExists(randValue));
                        // Add value to selected values
                        selectedValues.Add(randValue);
                    }

                    if (selectedValues.Count > 0)
                    {
                        tempOutput = "";
                        for (int i = 0; i < selectedValues.Count; i++)
                        {
                            tempOutput += "Value " + (i + 1).ToString() + ": " + selectedValues[i].ToString() + Environment.NewLine;
                        }
                        tbOutput.Text = tempOutput;
                    }
                }
                else
                {
                    Error();
                    return;
                }
            }
            catch
            {
                // inform the user with a message
                Error();
            }

            FocusToValue();
        }

        // Message to user
        private void Error()
        {
            SetDefaultValues();
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please revise your input:\n" +
                GetInstructions(), "Attention", false);
            msg.Show();
        }

        private void SetInstructions()
        {
            lbInstruction.Text = GetInstructions();
        }

        private string GetInstructions()
        {
            return "Min Value between 0 and " + (MaxValue - 1).ToString() + ". " +
                "Max Value must be higher than Min Value and max " + MaxValue.ToString() + "." +
                "\nDice Count from 0 to " + MaxDiceCount.ToString();
        }

        #region Key and Click events
        private void RandomNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(this, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Generate();
            }
            else if (e.KeyData == (Keys.Control | Keys.R))
            {
                btnReset_Click_1(this, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            Generate();
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            SetDefaultValues();
            Generate();
        }
        #endregion

        #region Context menu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }
        #endregion


    }
}
