using System;
using System.Windows.Forms;
using System.Drawing;


namespace AhoyBrowser
{
    public partial class AhoySearchMessageBox : Form
    {
        public bool IsReadOnly { get; set; }
        private bool HighlightTopLabel { get; set; }
        private bool UseStrongColors { get; set; }
        private bool AutoScrollToEnd { get; set; }
        public AhoySearchMessageBox(string textToDisplay, string formCaption, bool useStrongColors, bool readOnly, double opacity)
        {
            InitializeComponent();
            HighlightTopLabel = false;
            AutoScroll = false;

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;
            tbInformation.DetectUrls = false;
            IsReadOnly = readOnly;

            // Topmost
            this.TopMost = true;

            // Set opacity etc.
            this.Opacity = opacity;

            // Register activity
            tbInformation.MouseWheel += TbInformation_MouseWheel;
            tbInformation.Click += TbInformation_Click;

            // Will enable the form to take keypress events like Close on ESC
            this.KeyDown += AhoySearchMessageBox_KeyDown;
            this.KeyPreview = true;
            // Hide the control box
            this.ControlBox = false;
            UseStrongColors = useStrongColors;

            this.ResizeEnd += AhoySearchMessageBox_ResizeEnd;

            // Set Form caption
            this.Text = formCaption;
            // Set content text
            this.ControlBox = true;
            this.tbInformation.Font = Design.MessageboxLargeFont();
            this.tbSearchInContent.Font = Design.SearchFieldFont();
            tbInformation.ReadOnly = false;
            tbInformation.SetText(textToDisplay, false, true, 0);
            tbInformation.Focus();
            tbInformation.ReadOnly = IsReadOnly;

            // Setup editor properties
            if (IsReadOnly)
            {
                this.AcceptButton = btnOK;
                tbInformation.ContextMenuStrip = ahoyContextMenuCopy;
            }
            else
            {
                this.AcceptButton = null;
                tbInformation.ContextMenuStrip = contextMenuBasic;
            }

            // Search components
            tbInformation.HideSelection = false; // Force the control to show selections when not in focus
            tbSearchInContent.ContextMenuStrip = contextMenuBasic;
            tbSearchInContent.GotFocus += TbSearchInContent_GotFocus;
            timerRefocusContentSearch.Interval = 100;

            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;

            // Tabs
            tbInformation.AcceptsTab = true;

            // TopText
            lbTopText.Text = "";
            lbTopText.Visible = false;
            lbTopText.Font = Design.MessageboxMediumSmallFont();
            // Set color theme
            SetColorTheme();
        }

        #region Keep awake
        private void TbInformation_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
        }

        private void TbInformation_MouseWheel(object sender, MouseEventArgs e)
        {
            MainForm.KeepAwake();
        }
        #endregion

        public void SetSize(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                this.Size = new Size(width, height);
            }
        }

        private void AhoySearchMessageBox_ResizeEnd(object sender, EventArgs e)
        {
            if (AutoScrollToEnd)
            {
                ScrollToEndOnResize();
            }
        }

        public void SetColorTheme()
        {
            // Set button appearance
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnOK.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnOK.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            btnOK.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnOK.ForeColor = AhoyColors.CurrentColorTheme()[8];

            if (UseStrongColors)
            {
                this.tbInformation.BackColor = AhoyColors.CurrentColorTheme()[6];
                this.tbInformation.ForeColor = AhoyColors.CurrentColorTheme()[1];
            }
            else
            {
                this.tbInformation.BackColor = AhoyColors.CurrentColorTheme()[29];
                this.tbInformation.ForeColor = AhoyColors.CurrentColorTheme()[30];
            }

            lbContentSearch.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbContentSearch.BackColor = Color.Transparent;
            tbSearchInContent.BackColor = AhoyColors.CurrentColorTheme()[1];
            tbSearchInContent.ForeColor = AhoyColors.CurrentColorTheme()[3];
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSearch.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSearch.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnSearch.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSearch.ForeColor = AhoyColors.CurrentColorTheme()[8];

            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];

            if (HighlightTopLabel)
            {
                lbTopText.ForeColor = AhoyColors.CurrentColorTheme()[6];
                lbTopText.BackColor = Color.Transparent;
            }
            else
            {
                lbTopText.ForeColor = AhoyColors.CurrentColorTheme()[2];
                lbTopText.BackColor = Color.Transparent;
            }

        }

        private void AhoySearchMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.D)) // Multiple shortcut keypress
            {
                DuplicateSelection(tbInformation);
                e.Handled = true;
                return;
            }
        }

        private void DuplicateSelection(HistoryTextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                try
                {
                    if (textBox.SelectedText.Length > 0)
                    {
                        textBox.Copy(); // Copy selection
                        textBox.DeselectAll(); // Deselect
                    }
                    textBox.Paste(); // Paste
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        #region RE-DRAW While resizing issue fix
        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }
        #endregion

        private void TbSearchInContent_GotFocus(object sender, EventArgs e)
        {
            if (tbSearchInContent.Text.Length > 0)
            {
                tbSearchInContent.SelectAll();
            }
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }


        #region Public modification properties

        public void EnableAutoScrollToEnd()
        {
            AutoScrollToEnd = true;
        }

        public void SetTextContent(string text)
        {
            if (text.Length > 0)
            {
                SuspendLayout();
                tbInformation.Text = text;
                ResumeLayout();
            }
        }

        public void HideSearchControls()
        {
            tbSearchInContent.Visible = false;
            btnSearch.Visible = false;
            lbContentSearch.Visible = false;
        }

        private Timer timerDateTime;
        public void ShowDateTime()
        {
            if (timerDateTime == null)
            {
                timerDateTime = new Timer();
                timerDateTime.Interval = 1000;
                timerDateTime.Tick += TimerDateTime_Tick;
            }
            lbTopText.Text = IO.GetIsoDateTimeNow();
            lbTopText.Visible = true;
            timerDateTime.Enabled = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerDateTime?.Stop();
            timerDateTime?.Dispose();
            timerDateTime = null;
            base.OnFormClosed(e);
        }

        public void HighlightDateTimeLabel()
        {
            HighlightTopLabel = true;
            SetColorTheme();
        }

        public void ForceMinSizeAndFont()
        {
            this.MinimumSize = new Size(250, 250);
            tbInformation.Font = Design.LogMonitorFont();
        }

        public void RemoveScrollBars()
        {
            tbInformation.ScrollBars = RichTextBoxScrollBars.None;
        }


        public void AddHeight(int height)
        {
            this.Height = this.Height + height;
        }

        public void AddWidth(int width)
        {
            this.Width = this.Width + width;
        }

        // Ensure that text is scrolled to bottom
        private void ScrollToEndOnResize()
        {
            if (tbInformation.Text.Length > 0)
            {
                // First scroll to top, then to end :)
                tbInformation.Select(0, 0);
                tbInformation.ScrollToCaret();
                tbInformation.Select(tbInformation.Text.Length, 0);
                tbInformation.ScrollToCaret();
            }
        }

        public void ScrollToEndWithoutFocus()
        {
            if (tbInformation.Text.Length > 0)
            {
                SuspendLayout();
                // Move cursor end of text without focusing the control
                tbInformation.Select(tbInformation.Text.Length, 0);
                tbInformation.ScrollToCaret();
                ResumeLayout();
            }
        }
        #endregion


        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            lbTopText.Text = IO.GetIsoDateTimeNow();
        }

        #region Search logic
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!timerRefocusContentSearch.Enabled)
            {
                FindContent();
            }
            tbInformation.Focus();
        }

        private void tbSearchInContent_Click(object sender, EventArgs e)
        {
            tbSearchInContent.SelectAll();
        }

        private void tbSearchInContent_TextChanged(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            RestartSearchVariables();
        }

        private void tbSearchInContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!timerRefocusContentSearch.Enabled)
                {
                    FindContent();
                }
                // Prevent Ding
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                tbInformation.Focus();
                // Prevent Ding
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                return; // Prevent from running to the bottom of this method
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                return; // Prevent from running to the bottom of this method
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                return; // Prevent from running to the bottom of this method
            }
            else
            {
                // Send the rest of the keypresses to the tbContent_KeyDown method
                tbInformation_KeyDown(sender, e);
            }
        }

        int searchCount;
        string tempPattern;
        string tempSource;
        private void ResolveSearchCount()
        {
            // Fetch strings (to lower to make it case insensitive and get all counts)
            tempPattern = tbSearchInContent.Text.ToLower();
            tempSource = tbInformation.Text.ToLower();

            // Loop through all instances of the tempPattern
            searchCount = 0;
            int i = 0;
            while ((i = tempSource.IndexOf(tempPattern, i)) != -1)
            {
                i += tempPattern.Length;
                searchCount++;
            }

            lbContentSearch.Text = "Found: " + searchCount.ToString();
        }

        private void RestartSearchVariables()
        {
            // On new searches the "start" variable must be reset
            start = 0;
            indexOfSearchText = 0;
            contentSearchString = "";
            lbContentSearch.Text = "Search";
        }

        int start;
        int indexOfSearchText;
        string contentSearchString = "";

        private HistoryTextBox tempTextBox;
        private void FindContent()
        {
            if (tbSearchInContent.Text.Length > 0)
            {
                tempTextBox = tbInformation;
                contentSearchString = tbSearchInContent.Text;

                if (!string.IsNullOrEmpty(contentSearchString))
                    Log.LogGeneralContentSearch(contentSearchString);

                ResolveSearchCount();

                int startindex = 0;
                if (tbSearchInContent.Text.Length > 0)
                {
                    startindex = FindMyText(tbSearchInContent.Text.Trim(), start, tempTextBox.Text.Length + 1);
                }

                // If string was found in the RichTextBox, highlight it
                if (startindex >= 0)
                {
                    // Set the highlight color as red (Remember to set the rich textbox to show highlights in its properties)
                    //GetCurrentTextBox().SelectionColor = Color.Red;
                    // Find the end index. End Index = number of characters in textbox
                    int endindex = tbSearchInContent.Text.Length;
                    // Highlight the search string
                    tempTextBox.Select(startindex, endindex);

                    // mark the start position after the position of
                    // last search string
                    start = startindex + endindex;
                }

                if (indexOfSearchText == -1)
                {
                    AhoySimpleMessageBox messageBox = new AhoySimpleMessageBox(MessageText.EndOfSearchMessage(), MessageText.SearchCaption(), false);
                    messageBox.StartPosition = FormStartPosition.CenterParent;
                    messageBox.TopMost = true;
                    messageBox.ShowDialog();
                    start = 0;
                    indexOfSearchText = 0;
                    MainForm.RefreshForm();
                }

                // Refocus search field
                timerRefocusContentSearch.Enabled = true;
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = tbInformation.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }

        private void timerRefocusContentSearch_Tick(object sender, EventArgs e)
        {
            tbSearchInContent.Text = contentSearchString;
            tbSearchInContent.SelectAll();
            // Must be disabled at the end of the method (here)
            timerRefocusContentSearch.Enabled = false;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Only works if KeyPreview is set to true
        private void InfoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Close dialog?", MessageText.CloseCaption(), false, false);
                if (this.TopMost)
                {
                    msg.TopMost = true;
                }
                msg.ShowDialog();

                if (msg.Accepted)
                {
                    btnOK_Click(this, null);
                    e.Handled = true;
                }
            }
            else if (IsReadOnly && (Keys)e.KeyChar == Keys.Enter)
            {
                btnOK_Click(this, null);
                e.Handled = true;
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 0);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 2);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 3);
        }

        // Simple copy menu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 1);
        }

        private void tbInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.X))
            {
                GeneratePasswords();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V) // Paste unformatted
            {
                tbInformation.PasteUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Cut unformatted
            {
                tbInformation.CutUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Copy unformatted
            {
                tbInformation.CopyUnformatted();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F) // Search
            {
                tbSearchInContent.Focus();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                InsertParagraphDivider(0, tbInformation);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                InsertParagraphDivider(1, tbInformation);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemPeriod)
            {
                InsertParagraphDivider(2, tbInformation);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                InsertParagraphDivider(3, tbInformation);
                e.Handled = true;
            }
        }

        private void GeneratePasswords()
        {
            // Clipboard sometimes fails, hence the try-catch
            try
            {
                PasswordTypeSelector pwts = new PasswordTypeSelector(MainForm.SelectedPasswordStyle);
                pwts.TopMost = true;
                pwts.ShowDialog();

                if (pwts.GenerationSuccess)
                {
                    // Record user selection
                    if (MainForm.SelectedPasswordStyle != pwts.CurrentSelection)
                    {
                        MainForm.SelectedPasswordStyle = pwts.CurrentSelection;
                        MainForm.SettingsHasChanges = true;
                    }

                    // Insert the generated keys directly (not via the clipboard)
                    tbInformation.InsertText(pwts.GetGeneratedKeys());
                }
                else
                {
                    return;
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private static void SetClipboardText(string input)
        {
            try
            {
                // Set clipboard
                Clipboard.SetDataObject(
                        input, // Text
                        false, // Do not persist on application exit
                        7,     // Retry times
                        200    // Delay (ms) between retrys
                        );

                // Check that the clipboard was set and try again if not
                if (!Clipboard.ContainsText() || Clipboard.GetText() != input)
                {
                    Log.Log1stClipboardError();
                    Clipboard.SetDataObject(
                     input, // Text
                     false, // Do not persist on application exit
                     7,     // Retry times
                     250    // Delay (ms) between retrys
                     );
                }

                // Log further error
                if (!Clipboard.ContainsText() || Clipboard.GetText() != input)
                {
                    Log.Log2ndClipboardError();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        // Insert Paragraph divider
        private void InsertParagraphDivider(int selection, HistoryTextBox tb)
        {
            switch (selection)
            {
                case 0:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
                case 1:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerBString);
                    break;
                case 2:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerCString);
                    break;
                case 3:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerDString);
                    break;
                default:
                    tb.InsertParagraphDivider(ParagraphDividers.DividerAString);
                    break;
            }
        }


    }
}
