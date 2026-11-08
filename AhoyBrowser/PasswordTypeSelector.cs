using System;
using System.Windows.Forms;
using System.Drawing;


namespace AhoyBrowser
{
    public partial class PasswordTypeSelector : Form
    {
        public bool GenerationSuccess { get; set; }
        private string GeneratedString { get; set; }
        public int CurrentSelection { get; set; }
        private bool ShowPasswordChars { get; set; }
        public PasswordTypeSelector(int previousSelection)
        {
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = startPosition;
            this.StartPosition = FormStartPosition.CenterParent;
            GenerationSuccess = false;
            GeneratedString = "";
            CurrentSelection = previousSelection;
            this.TopMost = true;
            ShowPasswordChars = true;

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];
            
            // Context menu
            tbPassword.ContextMenuStrip = contextMenuStrip1;

            // Set style
            SetColorTheme();
            SelectButton(previousSelection);
            Log.PasswordGenerator();
        }


        private void SetColorTheme()
        {
            // Form
            this.BackColor = AhoyColors.CurrentColorTheme()[0];

            // Form labels
            label2.ForeColor = AhoyColors.CurrentColorTheme()[2];
            label2.BackColor = Color.Transparent;
            label3.ForeColor = AhoyColors.CurrentColorTheme()[2];
            label3.BackColor = Color.Transparent;


            // Password tester
            tbPassword.Font = Design.PasswordFieldFont();
            lbPasswordTesterTitle.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPasswordTesterTitle.BackColor = Color.Transparent;
            tbPassword.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbPassword.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbStrength.BackColor = this.BackColor;
            lbStrength.ForeColor = AhoyColors.CurrentColorTheme()[6];

            btnShowHide.Text = "";
            btnShowHide.BackColor = Color.Transparent;
            btnShowHide.FlatStyle = FlatStyle.Flat;
            btnShowHide.FlatAppearance.BorderSize = 0;
            btnShowHide.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnShowHide.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnShowHide.FlatAppearance.BorderColor = this.BackColor;
            btnShowHide.FlatAppearance.BorderSize = 0;
            btnShowHide.BackgroundImageLayout = ImageLayout.Tile;
            ResolveShowButton(); // Resolve eye button bitmap

            // Divider line
            pnlDivider1.BackColor = AhoyColors.CurrentColorTheme()[7];
            pnlDivider2.BackColor = AhoyColors.CurrentColorTheme()[7];

            // Buttons
            btnStyleA.FlatStyle = FlatStyle.Flat;
            btnStyleB.FlatStyle = FlatStyle.Flat;
            btnStyleC.FlatStyle = FlatStyle.Flat;
            btnStyleD.FlatStyle = FlatStyle.Flat;
            btnStyleE.FlatStyle = FlatStyle.Flat;
            btnStyleF.FlatStyle = FlatStyle.Flat;
            btnStyleG.FlatStyle = FlatStyle.Flat;
            btnStyleH.FlatStyle = FlatStyle.Flat;
            btnStyleI.FlatStyle = FlatStyle.Flat;
            btnStyleJ.FlatStyle = FlatStyle.Flat;
            btnStyleK.FlatStyle = FlatStyle.Flat;
            btnStyleL.FlatStyle = FlatStyle.Flat;
            btnStyleM.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;

            btnStyleA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleD.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleE.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleF.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleG.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleH.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleI.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleJ.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleL.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnStyleM.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnStyleA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleD.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleE.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleF.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleG.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleH.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleI.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleJ.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleL.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnStyleM.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnStyleA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleD.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleE.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleF.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleG.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleH.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleI.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleJ.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleL.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnStyleM.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnStyleA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleD.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleE.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleF.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleG.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleH.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleI.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleJ.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleK.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleL.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnStyleM.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];

            btnStyleA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleD.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleE.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleF.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleG.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleH.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleI.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleJ.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleK.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleL.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleM.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            SetupHelpPictureBox();
        }

        private void SetupHelpPictureBox()
        {
            picHelp.BackColor = Color.Transparent;
            picHelp.BackgroundImage = ButtonBitmaps.HelpButtonHighlight();
            picHelp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void ResolveShowButton()
        {
            try
            {
                if (ShowPasswordChars)
                {
                    btnShowHide.BackgroundImage = ButtonBitmaps.OpenEye(false);
                }
                else
                {
                    btnShowHide.BackgroundImage = ButtonBitmaps.HalfOpenEye(false);
                }
                tbPassword.Focus();
            }
            catch
            {
                // Do nothing
            }
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void SelectButton(int previousSelection)
        {
            switch (previousSelection)
            {
                case 0:
                    btnStyleA.Select();
                    break;
                case 1:
                    btnStyleB.Select();
                    break;
                case 2:
                    btnStyleC.Select();
                    break;
                case 3:
                    btnStyleD.Select();
                    break;
                case 4:
                    btnStyleE.Select();
                    break;
                case 5:
                    btnStyleF.Select();
                    break;
                case 6:
                    btnStyleG.Select();
                    break;
                case 7:
                    btnStyleH.Select();
                    break;
                case 8:
                    btnStyleI.Select();
                    break;
                case 9:
                    btnStyleJ.Select();
                    break;
                case 10:
                    btnStyleK.Select();
                    break;
                case 11:
                    btnStyleL.Select();
                    break;
                case 12:
                    btnStyleM.Select();
                    break;
            }
        }

        public string GetGeneratedKeys()
        {
            return GeneratedString;
        }

        private void ABC123(int length)
        {
            GeneratedString = Environment.NewLine + "──── Keys ────" + Environment.NewLine;
            for (int i = 0; i < 5; i++)
            {
                GeneratedString += PasswordGeneratorC.GeneratePassword(length) + Environment.NewLine;
            }
            GeneratedString += Environment.NewLine;
        }

        private void WordPlay(bool useSpecialChars)
        {
            GeneratedString = Environment.NewLine + "──── Keys ────" + Environment.NewLine + PasswordGeneratorB.GetPasswords(useSpecialChars) + Environment.NewLine;
        }

        private void GenerateGuid()
        {
            GeneratedString = Environment.NewLine + "──── Keys ────" + Environment.NewLine;
            for (int i = 0; i < 5; i++)
            {
                GeneratedString += Guid.NewGuid().ToString() + Environment.NewLine;
            }
            GeneratedString += Environment.NewLine;
        }

        private void GenerateStrong(int length)
        {
            GeneratedString = Environment.NewLine + "──── Keys ────" + Environment.NewLine;
            for (int i = 0; i < 5; i++)
            {
                GeneratedString += PasswordGenerator.GeneratePassword(length) + Environment.NewLine;
            }
            GeneratedString += Environment.NewLine;
        }


        // ABC123
        private void btnStyleA_Click(object sender, EventArgs e)
        {
            CurrentSelection = 0;
            ABC123(8);
            GenerationSuccess = true;
            CloseForm();
        }

        // Wordplay
        private void btnStyleB_Click(object sender, EventArgs e)
        {
            CurrentSelection = 1;
            WordPlay(true);
            GenerationSuccess = true;
            CloseForm();
        }

        // Strong 8 chars
        private void btnStyleC_Click(object sender, EventArgs e)
        {
            CurrentSelection = 2;
            GenerateStrong(8);
            GenerationSuccess = true;
            CloseForm();
        }


        // Strong 12 chars
        private void btnStyleD_Click(object sender, EventArgs e)
        {
            CurrentSelection = 3;
            GenerateStrong(12);
            GenerationSuccess = true;
            CloseForm();
        }

        // Strong  chars 16
        private void btnStyleE_Click(object sender, EventArgs e)
        {
            CurrentSelection = 4;
            GenerateStrong(16);
            GenerationSuccess = true;
            CloseForm();
        }


        // Strong 20 chars
        private void btnStyleF_Click(object sender, EventArgs e)
        {
            CurrentSelection = 5;
            GenerateStrong(20);
            GenerationSuccess = true;
            CloseForm();
        }

        // Strong 32 chars
        private void btnStyleG_Click(object sender, EventArgs e)
        {
            CurrentSelection = 6;
            GenerateStrong(32);
            GenerationSuccess = true;
            CloseForm();
        }




        // Strong 64 chars
        private void btnStyleH_Click(object sender, EventArgs e)
        {
            CurrentSelection = 7;
            GenerateStrong(64);
            GenerationSuccess = true;
            CloseForm();
        }

        // Guid
        private void btnStyleI_Click(object sender, EventArgs e)
        {
            CurrentSelection = 8;
            GenerateGuid();
            GenerationSuccess = true;
            CloseForm();
        }

        private void btnStyleJ_Click(object sender, EventArgs e)
        {
            CurrentSelection = 9;
            ABC123(12);
            GenerationSuccess = true;
            CloseForm();
        }

        private void btnStyleK_Click(object sender, EventArgs e)
        {
            CurrentSelection = 10;
            ABC123(16);
            GenerationSuccess = true;
            CloseForm();
        }

        private void btnStyleL_Click(object sender, EventArgs e)
        {
            CurrentSelection = 11;
            ABC123(20);
            GenerationSuccess = true;
            CloseForm();
        }

        private void btnStyleM_Click(object sender, EventArgs e)
        {
            CurrentSelection = 12;
            WordPlay(false);
            GenerationSuccess = true;
            CloseForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GenerationSuccess = false;
            CloseForm();
        }

        private void PasswordTypeSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                GenerationSuccess = false;
                CloseForm();
            }
        }

        private void CloseForm()
        {
            this.Close();
        }

        private void btnStyleA_Enter(object sender, EventArgs e)
        {
            // Reset colors
            btnStyleA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleD.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleE.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleF.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleG.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleH.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleI.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleJ.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleK.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleL.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnStyleM.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Highlight
            ((Button)sender).ForeColor = AhoyColors.CurrentColorTheme()[6];
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            TestStrength(); // Test password strength
        }

        private void TestStrength()
        {
            lbStrength.Text = PasswordTester.GetPasswordStrength(tbPassword.Text, true);
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(this, null);
            }
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            if (ShowPasswordChars)
            {
                ShowPasswordChars = false;
                this.tbPassword.PasswordChar = '\0';
            }
            else
            {
                ShowPasswordChars = true;
                this.tbPassword.PasswordChar = '*';
            }
            ResolveShowButton(); // Resolve eye button bitmap
        }//this.tbPassword.PasswordChar = ShowPasswordChars? '\0' : '*';

        private void picHelp_Click(object sender, EventArgs e)
        {
            AhoySearchMessageBox msg = new AhoySearchMessageBox(IO.PasswordInfoTextReader(), MessageText.InformationCaption(), false, true, this.Opacity);
            msg.ShowDialog();
        }


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
    }
}
