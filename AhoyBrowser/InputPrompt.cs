using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace AhoyBrowser
{
    public partial class InputPrompt : Form
    {
        public string UserInput { get; set; }
        public bool ActionCancelled { get; set; }
        public bool PinEntry { get; set; }
        private bool EnablePinning { get; set; }
        private bool IsCommandline { get; set; }
        private bool IsPasswordDialog { get; set; }
        public InputPrompt(string formCaption, string optionalInputText, string optionalAttentionMessage, string inputLabel, int maxTextLength, bool enablePinning, string okButtonText, bool isCommandline, Bitmap pinnedImage, Bitmap unpinnedImage, bool displayPasswordChars)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += InputPrompt_KeyDown;
            IsPasswordDialog = false;
            UserInput = "";

            if (displayPasswordChars)
            {
                tbInput.PasswordChar = '*';
                tbInput.Font = Design.PasswordFieldFont();
                IsPasswordDialog = true;
            }
            else
            {
                // Set font
                tbInput.Font = Design.TitleFieldFont();
            }

            // Get commandline flags
            IsCommandline = isCommandline;
            InitializeCommandHistoryList();

            // Get Main form opacity
            this.Opacity = MainForm.GetFormOpacity();

            // Set start position
            this.StartPosition = FormStartPosition.CenterParent;

            // Hide the control box
            this.ControlBox = false;

            // Top Most
            this.TopMost = true;

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];

            tbInput.KeyUp += TbInput_KeyUp;
            tbInput.KeyPress += TbInput_KeyPress;

            // Set button appearance
            btnOK.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;

            btnOK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnOK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnOK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnOK.Text = okButtonText;
            toolTip1.SetToolTip(this.btnOK, okButtonText + " (Enter)");
            toolTip1.SetToolTip(this.btnCancel, "Cancel (ESC)");

            // Setup context menu
            tbInput.ContextMenuStrip = contextMenuBasic;

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            lbAttentionMessage.BackColor = this.BackColor;
            lbAttentionMessage.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbMessage.BackColor = this.BackColor;
            lbMessage.ForeColor = AhoyColors.CurrentColorTheme()[2];
            tbInput.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbInput.ForeColor = AhoyColors.CurrentColorTheme()[3];
            btnOK.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            this.tbInput.MaxLength = maxTextLength;
            this.tbInput.Text = optionalInputText;

            if (tbInput.Text.Length > 0)
            {
                // Set caret at end of text
                tbInput.Select(tbInput.Text.Length, 0);
            }

            this.Text = formCaption;
            this.lbAttentionMessage.Text = optionalAttentionMessage;
            lbMessage.Text = inputLabel;
            ActionCancelled = false;
            PinEntry = false;
            EnablePinning = enablePinning;

            // Setup custom pinned checkbox
            cbPinned.CheckedBackgroundImage = null;
            cbPinned.CheckedText = null;
            cbPinned.CheckedTextColor = Color.Empty;
            cbPinned.Cursor = Cursors.Default;
            cbPinned.FlatStyle = FlatStyle.Flat;
            toolTip1.SetToolTip(this.cbPinned, "Pin Entry (CTRL+I)");
            cbPinned.CheckedText = ""; // No text
            cbPinned.UnCheckedText = ""; // No text
            cbPinned.BackgroundImageLayout = ImageLayout.Center;
            cbPinned.CheckedBackgroundImage = pinnedImage;
            cbPinned.UnCheckedBackgroundImage = unpinnedImage;
            // Set Checked state to display images
            cbPinned.Checked = false;
            cbPinned.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            cbPinned.BackColor = AhoyColors.CurrentColorTheme()[7];
            // Show/Hide pinned box
            cbPinned.Visible = enablePinning;

            // Command prompt label
            lbCommandPromptLabel.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbCommandPromptLabel.Font = Design.SearchFieldFont();
            lbCommandPromptLabel.Visible = isCommandline;

            if (isCommandline)
            {
                tbInput.AcceptsTab = true;
            }
        }

        // Send ESC keydown event to the tbInput_KeyDown event (Do not send Enter, as it will break the calculator)
        private void InputPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                tbInput_KeyDown(sender, e);
            }
        }


        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        /// <summary>
        /// Force to pin
        /// </summary>
        public void SetToPinned()
        {
            if (EnablePinning)
            {
                cbPinned.Checked = true;
                // Pin entry
                PinEntry = cbPinned.Checked;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            // Only trim if commandline or password dialog
            if (IsCommandline || IsPasswordDialog)
            {
                UserInput = tbInput.Text.Trim();
            }
            else
            {
                UserInput = tbInput.Text;
            }

            if (IsCommandline)
            {
                AddToCommandsHistoryList(UserInput);
            }
            this.Close();
        }



        private bool tempRegisterKeyEvent;
        private void TbInput_KeyUp(object sender, KeyEventArgs e)
        {
            // If is commandline and not tab pressed
            if (IsCommandline && tempRegisterKeyEvent)
            {
                lbMessage.Text = "Enter Command";
                CommandInput.FindMatchingCommands(tbInput.Text);
            }
        }


        private void TbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do nothing, was used previously
        }


        private Command tempMacthingCommand;
        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsCommandline)
            {
                if (e.KeyData == (Keys.Shift | Keys.Tab))
                {
                    tempRegisterKeyEvent = false;
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    tempRegisterKeyEvent = false;
                }
                else
                {
                    tempRegisterKeyEvent = true;
                }
            }
            else
            {
                tempRegisterKeyEvent = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                btnOK_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.T && (!IsCommandline && !IsPasswordDialog))
            {
                tbInput.Paste(IO.GetIsoDateTimeNow());
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                tbInput.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (tbInput.Text.Length > 0)
                {
                    tbInput.Text = "";
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                else
                {
                    ActionCancelled = true;
                    UserInput = "";
                    this.Close();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (IsCommandline)
                {
                    // Go backwards in previous commands
                    RecallCommandBack();
                }
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (IsCommandline)
                {
                    // Go forwards in previous commands
                    RecallCommandForward();
                }
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Shift | Keys.Tab)) // Must be run before the Tab below
            {
                if (IsCommandline)
                {
                    // Prepare command
                    if (tempMacthingCommand == null)
                    {
                        tempMacthingCommand = new Command("", "");
                    }

                    // Get matching command (autocomplete by Tab)
                    tempMacthingCommand = CommandInput.GetPreviousMatchingCommand();
                    if (tempMacthingCommand.CommandValue != "noresult")
                    {
                        tbInput.Text = tempMacthingCommand.CommandValue;
                        lbMessage.Text = "[" + tempMacthingCommand.CommandValue + "]: " + tempMacthingCommand.CommandDescription;
                        tbInput.SelectAll();
                    }

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (IsCommandline)
                {
                    // Prepare command
                    if (tempMacthingCommand == null)
                    {
                        tempMacthingCommand = new Command("", "");
                    }

                    // Get matching command (autocomplete by Tab)
                    tempMacthingCommand = CommandInput.GetNextMatchingCommand();
                    if (tempMacthingCommand.CommandValue != "noresult")
                    {
                        tbInput.Text = tempMacthingCommand.CommandValue;
                        lbMessage.Text = "[" + tempMacthingCommand.CommandValue + "]: " + tempMacthingCommand.CommandDescription;
                        tbInput.SelectAll();
                    }

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                if (EnablePinning)
                {
                    // Pin entry
                    PinEntry = !PinEntry;
                    cbPinned.Checked = PinEntry;
                    e.SuppressKeyPress = true; // Prevent from taking further input (key combination will add tab)
                }
            }
        }

        private static bool tempCommandHistoryMatch;
        private void AddToCommandsHistoryList(string query)
        {
            tempCommandHistoryMatch = false;
            // Check that search string is not in list
            if (commandHistoryList.Count > 0)
            {
                foreach (string s in commandHistoryList)
                {
                    if (s == query)
                    {
                        tempCommandHistoryMatch = true;
                        break;
                    }
                }
            }

            // Add to list if not found
            if (!tempCommandHistoryMatch)
            {
                commandHistoryList.Add(query);
            }
        }

        private static List<string> commandHistoryList;
        private void InitializeCommandHistoryList()
        {
            recallCommandIndex = 0;
            if (commandHistoryList == null)
            {
                commandHistoryList = new List<string>();
            }
        }

        private static int recallCommandIndex;
        private void RecallCommandBack()
        {
            if (commandHistoryList.Count > 0)
            {
                if (recallCommandIndex >= 1)
                {
                    recallCommandIndex -= 1;
                }
                else
                {
                    recallCommandIndex = commandHistoryList.Count - 1;
                }

                InsertRecalledCommandString();
            }
        }

        private void RecallCommandForward()
        {
            if (commandHistoryList.Count > 0)
            {
                if (recallCommandIndex < commandHistoryList.Count - 1)
                {
                    recallCommandIndex += 1;
                }
                else
                {
                    recallCommandIndex = 0;
                }

                InsertRecalledCommandString();
            }
        }

        private void InsertRecalledCommandString()
        {
            if (commandHistoryList.Count > 1)
            {
                if (tbInput.Text != commandHistoryList[recallCommandIndex])
                {
                    tbInput.Text = commandHistoryList[recallCommandIndex];
                }
                else
                {
                    RecallCommandForward();
                    tbInput.Text = commandHistoryList[recallCommandIndex];
                }
            }
            else if (commandHistoryList.Count == 1)
            {
                tbInput.Text = commandHistoryList[0];
            }

            // Move cursor to end
            tbInput.Focus();
            if (tbInput.Text.Length > 0)
            {
                tbInput.Select(tbInput.Text.Length, 0);
                tbInput.ScrollToCaret();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ActionCancelled = true;
            UserInput = "";
            this.Close();
        }

        private void cbPinned_Click(object sender, EventArgs e)
        {
            // Pin entry
            PinEntry = cbPinned.Checked;
        }


        // Change button text color on input
        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbInput.Text))
            {
                btnOK.ForeColor = AhoyColors.CurrentColorTheme()[28];
            }
            else
            {
                btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        // Context menu actions
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
