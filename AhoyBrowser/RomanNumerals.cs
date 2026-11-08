using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class RomanNumerals : Form
    {
        private List<string> WordList { get; set; }
        private string Translation { get; set; }
        private string Test { get; set; }

        public RomanNumerals()
        {
            InitializeComponent();
            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;
            WordList = new List<string>();
            Translation = "";
            Test = "";
            tbInput.ContextMenuStrip = ahoyContextMenuStrip1;
            tbOutput.ContextMenuStrip = ahoyContextMenuStrip1;
            tbInfo.ContextMenuStrip = ahoyContextMenuStrip1;
            // Disable history on info and output
            tbInfo.SuspendHistory();
            tbOutput.SuspendHistory();

            SetColorTheme();
            tbInfo.Text = IO.RomanNumeralsReader();
            tbInput.SetText("1 2 3 4 5 6 7 8 9 10 1910 ABCDEFGHIJKLMNOPQRSTUVWXYZ", true);
            CalculateNumerals();
            tbInput.Focus();
            tbInput.Select();
        }

        private void SetColorTheme()
        {
            // Set button appearance
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];

            this.tbInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbInput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            this.tbOutput.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbOutput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            this.tbInfo.BackColor = AhoyColors.CurrentColorTheme()[1];
            this.tbInfo.ForeColor = AhoyColors.CurrentColorTheme()[3];

            tbInput.Font = Design.FieldFont(MainForm.FieldsFontSize);
            tbOutput.Font = Design.FieldFont(MainForm.FieldsFontSize);
            tbInfo.Font = Design.FieldFont(MainForm.FieldsFontSize);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RomanNumerals_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnClose_Click(this, null);
                e.Handled = true;
            }
        }

        // 12345678911111111122222222
        // |||||||||01234567890123456
        // ABCDEFGHIJKLMNOPQRSTUVWXYZ
        //  1 = I
        //  2 = II
        //  3 = III
        //  4 = IV
        //  5 = V
        //  6 = VI
        //  7 = VII
        //  8 = VIII
        //  9 = IX
        // 10 = X
        // 
        // I = 1
        // V = 5
        // X = 10
        // L = 50
        // C = 100
        // D = 500
        // M = 1000
        // 1910 is shown on Admiralty Arch, London, as MDCCCCX rather than MCMX
        // The numeral I can be placed before V and X to make 4 units (IV) and 9 units (IX) respectively
        // X can be placed before L and C to make 40 (XL) and 90 (XC) respectively
        // C can be placed before D and M to make 400 (CD) and 900 (CM) according to the same pattern.
        // An example using the above rules would be 1904: this is composed of 1 (one thousand), 9 (nine hundreds),
        // 0 (zero tens), and 4 (four units). To write the Roman numeral, each of the non-zero digits should be treated
        // separately. Thus 1,000 = M, 900 = CM, and 4 = IV. Therefore, 1904 is MCMIV.
        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            CalculateNumerals();
        }

        private void CalculateNumerals()
        {
            WordList.Clear();
            Translation = "";
            tbOutput.Text = "";
            // Split input into word list
            // Check for numeric value before translating
            if (tbInput.Text.Length > 0)
            {
                WordList.AddRange(tbInput.Text.Split(' '));

                for (int i = 0; i < WordList.Count; i++)
                {
                    // If is numeric
                    if (Regex.IsMatch(WordList[i], "\\d"))
                    {
                        Test = ConvertToRoman(WordList[i]);
                    }
                    else
                    {
                        Test = ConvertWordToRoman(WordList[i]);
                    }


                    if (Test.Length > 0 || Test != " ")
                    {
                        Translation += Test + " ";
                    }
                }

                if (Translation.Length > 0)
                {
                    tbOutput.Text = Translation;
                }
            }
        }

        /// <summary>
        /// Helper method that looks up a given index to it's roman value.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The roman literal corresponding to it's index</returns>
        private char DecimalToRoman(int index)
        {
            switch (index)
            {
                case 1: return 'I';
                case 2: return 'V';
                case 3: return 'X';
                case 4: return 'L';
                case 5: return 'C';
                case 6: return 'D';
                case 7: return 'M';
                default: return ' ';
            }
        }

        private string ConvertToRoman(string input)
        {
            int index = 0;
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                //Some magic here, this formula will calculate the correct starting
                //index of the roman literal to find in the look-up table.
                //Since units, tens and hundreds (up to thousand) can be formed of
                //three roman literals, we need three indices for looking up the
                //correct roman literal.
                index = 2 * (input.Length - (i + 1)) + 1;

                char digit1 = DecimalToRoman(index);
                char digit2 = DecimalToRoman(index + 1);
                char digit3 = DecimalToRoman(index + 2);

                int originalValue = Convert.ToInt32(input[i] - '0');

                switch (originalValue)
                {
                    case 1:
                    case 2:
                    case 3:
                        for (int j = 0; j < originalValue; j++)
                            output += digit1.ToString();
                        break;
                    case 4:
                        output += digit1.ToString() + digit2.ToString();
                        break;
                    case 5:
                        output += digit2.ToString();
                        break;
                    case 6:
                    case 7:
                    case 8:
                        output += digit2.ToString();
                        for (int j = 0; j < originalValue - 5; j++)
                            output += digit1.ToString();
                        break;
                    case 9:
                        output += digit1.ToString() + digit3.ToString();
                        break;
                }
            }
            return output;
        }


        private string tempString;
        private string ConvertWordToRoman(string input)
        {
            tempString = "";
            for (int i = 0; i < input.Length; i++)
            {
                tempString += GetAlphanumeric(input[i].ToString());
            }

            return tempString;
        }

        private string GetAlphanumeric(string input)
        {
            switch (input.ToUpper())
            {
                case "A":
                    return ConvertToRoman("1") + " ";
                case "B":
                    return ConvertToRoman("2") + " ";
                case "C":
                    return ConvertToRoman("3") + " ";
                case "D":
                    return ConvertToRoman("4") + " ";
                case "E":
                    return ConvertToRoman("5") + " ";
                case "F":
                    return ConvertToRoman("6") + " ";
                case "G":
                    return ConvertToRoman("7") + " ";
                case "H":
                    return ConvertToRoman("8") + " ";
                case "I":
                    return ConvertToRoman("9") + " ";
                case "J":
                    return ConvertToRoman("10") + " ";
                case "K":
                    return ConvertToRoman("11") + " ";
                case "L":
                    return ConvertToRoman("12") + " ";
                case "M":
                    return ConvertToRoman("13") + " ";
                case "N":
                    return ConvertToRoman("14") + " ";
                case "O":
                    return ConvertToRoman("15") + " ";
                case "P":
                    return ConvertToRoman("16") + " ";
                case "Q":
                    return ConvertToRoman("17") + " ";
                case "R":
                    return ConvertToRoman("18") + " ";
                case "S":
                    return ConvertToRoman("19") + " ";
                case "T":
                    return ConvertToRoman("20") + " ";
                case "U":
                    return ConvertToRoman("21") + " ";
                case "V":
                    return ConvertToRoman("22") + " ";
                case "W":
                    return ConvertToRoman("23") + " ";
                case "X":
                    return ConvertToRoman("24") + " ";
                case "Y":
                    return ConvertToRoman("25") + " ";
                case "Z":
                    return ConvertToRoman("26") + " ";
                case ",":
                    return ", ";
                case ".":
                    return ". ";
                case ":":
                    return ": ";
                case "-":
                    return "- ";
                case "!":
                    return "! ";
                case "?":
                    return "? ";
                case "\"":
                    return "\" ";
                case "(":
                    return "( ";
                case ")":
                    return ") ";
                case "=":
                    return "= ";
                case "&":
                    return "& ";
                case "#":
                    return "# ";
                case "+":
                    return "+ ";
                case "/":
                    return "/ ";
                case ";":
                    return "; ";
                case ">":
                    return "> ";
                case "<":
                    return "< ";
                case "~":
                    return "~ ";
                case "^":
                    return "^ ";
                case "[":
                    return "[ ";
                case "]":
                    return "] ";
                case "{":
                    return "{ ";
                case "}":
                    return "} ";
                case "*":
                    return "* ";
                case "%":
                    return "% ";
                case "|":
                    return "| ";
                case "@":
                    return "@ ";
                default:
                    return " ";
            }
        }

        #region Context menu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }
        #endregion
    }
}
