using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace AhoyBrowser
{
    public class HistoryTextBox : RichTextBox
    {
        // RichTextBox with its own undo/redo history (999 steps per edit, 99 across the pool),
        // unformatted cut/copy/paste, word-wrap aware line delete, tab and timestamp insertion.
        

        private const int maxHistoryCount = 999;
        private const int maxHistoryCountWithReducedCache = 99;
        private const int maxHistoryCountSessionCache = 99;
        public bool ReduceCache { get; set; }  // Feature will allow no session cache and only 99 undos on edit
        bool ignoreChange;
        bool isFirstUndo = true;
        private List<HistoryState> undoStorage { get; set; }
        private List<HistoryState> redoStorage { get; set; }
        private bool keysHandled = false;
        private int CaretPosition { get; set; }
        private bool FirstSelection { get; set; }
        private int FirstSelectionCaretPosition { get; set; }
        private bool IgnoreTab { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CaretPosition = 0;
            this.MouseUp -= HistoryTextBox_MouseClick; // OnCreateControl runs again when the handle is recreated
            this.MouseUp += HistoryTextBox_MouseClick; // Register mouse up events
            ReduceCache = false;
            IgnoreTab = false;
        }

        /// <summary>
        /// Replace the selection with text without going through the clipboard.
        /// The change is recorded by this control's own undo history (OnTextChanged).
        /// </summary>
        public void InsertText(string text)
        {
            this.SelectedText = text ?? "";
        }

        public void ReduceUndoCache(bool state)
        {
            ReduceCache = state;
        }

        public static int GetMaxLength()
        {
            return 2147483647;
        }

        // If string contains a single word and if this word is an URL
        // return it with a space and a new line to fix textbox problems with URLs
        private string tempUrlValidatedStringForPaste;
        private string CheckForUrls(string input)
        {
            tempUrlValidatedStringForPaste = input;
            if (this.DetectUrls && input.Length > 0)
            {
                if (IsValidURL(input))
                {
                    tempUrlValidatedStringForPaste = RemoveNewLines(input) + "\n\r";
                    return tempUrlValidatedStringForPaste;
                }
                else
                {
                    return tempUrlValidatedStringForPaste;
                }
            }
            else
            {
                return tempUrlValidatedStringForPaste;
            }
        }

        private string RemoveNewLines(string tmp)
        {
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace('\r', ' ');
            tmp = tmp.Trim();
            return tmp;
        }

        private bool tempValidUrl;
        private string tempValidUrlString;
        private const string tempRegexExpression = @"^((http|https|ftp|www):\/\/)?" +
                                                   @"([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)" +
                                                   @"(\.)" +
                                                   @"([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)";
        private bool IsValidURL(string tmp)
        {
            try
            {
                // Trim initial string
                tempValidUrlString = tmp.Trim();
                tempValidUrl = false;
                // If contains chars and string is only one word
                if (tempValidUrlString.Length > 0 && tempValidUrlString.Split(' ').Length == 1)
                {
                    // Trim string even more...
                    tempValidUrlString = tempValidUrlString.Replace('\n', ' ');
                    tempValidUrlString = tempValidUrlString.Replace('\r', ' ');
                    tempValidUrlString = tempValidUrlString.Replace('?', ' ');
                    tempValidUrlString = tempValidUrlString.Trim();

                    // Run through the string char by char until a URL match is registered, if registered
                    for (int i = 5; i < tempValidUrlString.Length; i++)
                    {
                        if (Regex.IsMatch(tempValidUrlString.Substring(0, i), tempRegexExpression, RegexOptions.Singleline))
                        {
                            tempValidUrl = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }

            // Return true if tmp is a valid URL.
            return tempValidUrl;
        }

        private void SetClipboardText(string input)
        {
            try
            {
                // Set clipboard
                Clipboard.SetDataObject(
                        input, // Text
                        false, // Do not persist on application exit
                        7,     // Retry times
                        200    // Delay (ms) between retries
                        );

                // Check that the clipboard was set and try again if not
                if (!Clipboard.ContainsText() || Clipboard.GetText() != input)
                {
                    Log.Log1stClipboardError();
                    Clipboard.SetDataObject(
                     input, // Text
                     false, // Do not persist on application exit
                     7,     // Retry times
                     250    // Delay (ms) between retries
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

        /// <summary>
        /// Copy text unformatted (plain Unicode text)
        /// With Windows 10 clipboard persistence
        /// </summary>
        private string tempUnformattedText;
        private string tempTextSelection;
        public void CopyUnformatted()
        {
            try
            {
                if (this.SelectedText.Length > 0)
                {
                    // Handle copying selection manually to avoid Windows 10 etc. copy issues
                    // Get text selection
                    tempTextSelection = this.SelectedText;
                    // Convert to unicode, to remove formatting symbols
                    tempUnformattedText = ConvertToUnicode(tempTextSelection);
                    // Set clipboard with retries
                    SetClipboardText(tempUnformattedText);
                }
            }
            catch
            {
                // Do nothing
            }
        }

        // Convert to Unicode
        static string tempConvertedToUnicode;
        static Encoding unicode;
        private static string ConvertToUnicode(string input)
        {
            try
            {
                if (input.Length > 0)
                {
                    unicode = Encoding.Unicode;
                    tempConvertedToUnicode = unicode.GetString(unicode.GetBytes(input));
                    tempConvertedToUnicode = tempConvertedToUnicode.Replace("\n", "\r\n"); // Fix newlines
                    return tempConvertedToUnicode;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Cut text unformatted (plain Unicode text)
        /// With Windows 10 clipboard persistance
        /// </summary>
        public void CutUnformatted()
        {
            try
            {
                if (this.SelectedText.Length > 0)
                {
                    // Handle copying selection manually to avoid Windows 10 etc. copy issues
                    // Get text selection
                    tempTextSelection = this.SelectedText;
                    this.SelectedText = ""; // Perform cut
                    // Convert to unicode, to remove formatting symbols
                    tempUnformattedText = ConvertToUnicode(tempTextSelection);
                    // Set clipboard with retries
                    SetClipboardText(tempUnformattedText);
                }
            }
            catch
            {
                // Do nothing
            }
        }

        /// <summary>
        /// Paste unformatted (plaint text)
        /// </summary>
        public void PasteUnformatted()
        {
            try
            {
                // Convert to unformatted Unicode
                if (Clipboard.ContainsText())
                {
                    tempUnformattedText = Clipboard.GetText(TextDataFormat.UnicodeText);
                    if (DetectUrls) // Check for URLS while pasting if Detection is set
                    {
                        tempUnformattedText = CheckForUrls(tempUnformattedText);
                    }
                    SetClipboardText(tempUnformattedText);
                    // Invoke paste
                    this.Paste();
                }
            }
            catch
            {
                // Do nothing
            }
        }


        public void ResetZoomFactor()
        {
            try
            {
                if (this.ZoomFactor != 1.0f)
                {
                    int tempZoomCaretPosition;
                    string tempZoomContent;
                    // Reset Zoom
                    // save caret position
                    tempZoomCaretPosition = this.CaretPosition;
                    // Save text
                    tempZoomContent = this.Text;
                    // Clear for zoom
                    this.Clear();
                    this.ZoomFactor = 1.0f;
                    this.Text = tempZoomContent;
                    // Scroll to caret
                    if (tempZoomCaretPosition > 0)
                    {
                        this.Select(tempZoomCaretPosition, 0);
                        ScrollToCaret();
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }
        public void InsertTab()
        {
            InsertText(TextTransform.GetBlankSpace(4));
        }
        /// <summary>Insert a paragraph divider (the clipboard is not touched)</summary>
        public void InsertParagraphDivider(string input)
        {
            try
            {
                InsertText(ReplaceParagraphDividerTimeTag(input));
                ReplaceAndSelectionDividerTag();
            }
            catch
            {
                // Inserting into a read-only or disposed box: nothing to do
            }
        }
        private void ReplaceAndSelectionDividerTag()
        {
            if (this.Text.Contains(ParagraphDividers.SelectionReplaceString))
            {
                try
                {
                    int tempIndex = this.Text.IndexOf(ParagraphDividers.SelectionReplaceString);
                    this.Text = this.Text.Replace(ParagraphDividers.SelectionReplaceString, "");
                    this.Select(tempIndex, 0);
                }
                catch
                {
                    // Do nothing
                }
            }
            else
            {
                // Do nothing
            }
        }

        private string ReplaceParagraphDividerTimeTag(string input)
        {
            if (input.Contains(ParagraphDividers.TimeStampReplaceString))
            {
                return input.Replace(ParagraphDividers.TimeStampReplaceString, TimeStamp.GetISOTimestamp());
            }
            else
            {
                return input;
            }
        }

        /// <summary>Insert an ISO timestamp (the clipboard is not touched)</summary>
        public void InsertISOTimestamp()
        {
            InsertText(TimeStamp.GetISOTimestamp());
        }
        private void HistoryTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            RegisterCaretPosition();
            UpdateInitialCaretPositionForRedo();
        }

        // Register the first click to make the caret return here
        // rather than the end index
        private void UpdateInitialCaretPositionForRedo()
        {
            if (FirstSelection)
            {
                FirstSelectionCaretPosition = CaretPosition;

                if (undoStorage.Count > 0)
                {
                    undoStorage[0].caretPosition = FirstSelectionCaretPosition;
                }

                FirstSelection = false;
            }
        }


        private void RegisterCaretPosition()
        {
            // Handle early object disposed situation if keypress is not set to e.Handled = true;
            try
            {
                CaretPosition = this.SelectionStart;
            }
            catch
            {
                // Do nothing
            }
        }


        public HistoryTextBox()
        {
            // Initialize Undo and Redo storage
            InitializeStorage();
        }

        protected void InitializeStorage()
        {
            if (undoStorage == null)
            {
                undoStorage = new List<HistoryState>();
            }

            if (redoStorage == null)
            {
                redoStorage = new List<HistoryState>();
            }
        }

        // This is a fix that will prevent the scrollbars from not showing when the
        // first entry is selected, which has more content than the current 
        // view will show, hence scrollbars must appear.
        internal void JoltToFixScrollbars()
        {
            string tempJolt = "";
            for (int i = 0; i < 2000; i++)
            {
                tempJolt += "Initializing..." + Environment.NewLine + Environment.NewLine; // Will create 4000 lines of text to reflect larger resolutions
            }

            SetText(tempJolt, true);
            Clear();
            tempJolt = "";
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            RegisterCaretPosition();
            base.OnKeyDown(e);
            if (keysHandled)
            {
                e.Handled = true;
            }
        }

        // Special feature to be used when making tabs with the PreviewKeyDown method from outside this class
        public void IgnoreNextTab()
        {
            IgnoreTab = true;
        }

        private void ResetTab()
        {
            IgnoreTab = false;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            RegisterCaretPosition();

            // Insert custom tab
            if (this.AcceptsTab && (Keys)e.KeyChar == Keys.Tab)
            {
                if (IgnoreTab)
                {
                    ResetTab();
                    e.Handled = true;
                    return;
                }
                else
                {
                    InsertTab();
                    e.Handled = true;
                }
            }

            base.OnKeyPress(e);
            if (keysHandled)
            {
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            RegisterCaretPosition();
            base.OnKeyUp(e);
            if (keysHandled)
            {
                e.Handled = true;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // Save change to undo storage...
            if (!ignoreChange)
            {
                RecordHistory();
            }
        }

        /// <summary>
        /// Add current Text to history
        /// </summary>
        public void RecordHistory()
        {
            isFirstUndo = true;
            undoStorage.Add(new HistoryState(this.Text, CaretPosition + 1));
            PerformCleanup();
        }


        private List<HistoryState> tempCache;
        public List<HistoryState> GetEntireHistoryState()
        {
            if (undoStorage != null && undoStorage.Count > 0)
            {
                if (tempCache == null)
                {
                    tempCache = new List<HistoryState>();
                }

                // Clear and set (return if reduce cache set)
                tempCache.Clear();
                if (ReduceCache)
                {
                    return tempCache;
                }
                tempCache.AddRange(undoStorage);

                // Limit temp cache to maxHistoryCountForCache
                if (tempCache.Count > maxHistoryCountSessionCache)
                {
                    tempCache.RemoveRange(0, tempCache.Count - maxHistoryCountSessionCache);
                }

                return tempCache;
            }
            else
            {
                return null;
            }
        }

        public void SetEntireHistoryState(List<HistoryState> state)
        {
            if (state != null)
            {
                if (undoStorage == null)
                {
                    undoStorage = new List<HistoryState>();
                }

                undoStorage.Clear();
                undoStorage.AddRange(state);
            }
        }

        /// <summary>
        /// Set Text and record History. The correct method for setting initial content.
        /// </summary>
        public void SetText(string textContent, bool scrollToEnd)
        {
            try
            {
                if (textContent != null)
                {
                    undoStorage.Add(new HistoryState(textContent, textContent.Length));
                    PerformCleanup();
                    SuspendHistory();
                    isFirstUndo = true;
                    FirstSelection = true;

                    // (SuspendLayout/ResumeLayout here breaks the scrollbars)
                    this.Text = string.Empty;
                    this.Text = textContent;

                    this.Focus();
                    if (scrollToEnd)
                    {
                        this.Select(this.Text.Length, 0);
                        this.ScrollToCaret();
                    }
                    ResumeHistory();
                }
            }
            catch
            {
                Log.LogGeneralError("Setting text (HT)");
            }
        }

        public void SetText(string text, bool scrollToEnd, bool scrollToCaret, int caretIndex)
        {
            try
            {
                if (text != null)
                {
                    undoStorage.Add(new HistoryState(text, text.Length));
                    PerformCleanup();
                    SuspendHistory();
                    isFirstUndo = true;
                    FirstSelection = true;
                    //this.SuspendLayout(); <- laver ballade med scroll bars, derfor fjernet igen...
                    this.Text = string.Empty;
                    this.Text = text;
                    this.Focus();

                    if (scrollToEnd)
                    {
                        this.Select(this.Text.Length, 0);
                        this.ScrollToCaret();
                    }
                    else if (scrollToCaret && caretIndex >= 0)
                    {
                        this.SelectionStart = caretIndex;
                        this.ScrollToCaret();
                    }


                    ResumeHistory();
                }
            }
            catch
            {
                Log.LogGeneralError("Setting text (HT)");
            }
        }

        private string tempNewLines;
        private string GetNewLines(int count)
        {
            tempNewLines = "";
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    tempNewLines += Environment.NewLine;
                }
            }
            return tempNewLines;
        }

        private string tempAppendText;
        public void AppendText(string text, int newLinesCount, bool scrollToEnd)
        {
            tempAppendText = "";
            if (text != null)
            {
                // Start by creating a new combined string
                tempAppendText = this.Text + GetNewLines(newLinesCount) + text;
                // Then continue like the SetText method
                undoStorage.Add(new HistoryState(tempAppendText, tempAppendText.Length)); // caret at the end of the appended text
                PerformCleanup();
                SuspendHistory();
                isFirstUndo = true;
                FirstSelection = true;
                this.Text = tempAppendText;
                this.Focus();
                if (scrollToEnd)
                {
                    this.Select(this.Text.Length, 0);
                    this.ScrollToCaret();
                }
                ResumeHistory();
            }
        }


        new public void Undo()
        {
            if (undoStorage.Count > 0)
            {
                this.ignoreChange = true;
                // Add redo step
                redoStorage.Add(new HistoryState(undoStorage[undoStorage.Count - 1].text, undoStorage[undoStorage.Count - 1].caretPosition));

                if (isFirstUndo)
                {
                    isFirstUndo = false;
                    // Remove current step to expose previous step for use
                    undoStorage.RemoveAt(undoStorage.Count - 1);
                    if (undoStorage.Count != 0)
                    {
                        // Recall undo step
                        this.Text = undoStorage[undoStorage.Count - 1].text;
                        // Position cursor after the last character
                        if (this.Text.Length > 0)
                        {
                            Select(undoStorage[undoStorage.Count - 1].caretPosition, 0);
                        }
                    }
                }
                else
                {
                    // Remove current step to expose previous step for use
                    undoStorage.RemoveAt(undoStorage.Count - 1);
                    if (undoStorage.Count != 0)
                    {
                        // Recall undo step
                        this.Text = undoStorage[undoStorage.Count - 1].text;
                        // Position cursor after the last character
                        if (this.Text.Length > 0)
                        {
                            Select(undoStorage[undoStorage.Count - 1].caretPosition, 0);
                        }
                    }
                }
                this.ignoreChange = false;
            }
        }


        new public void Redo()
        {
            if (redoStorage.Count > 0)
            {
                // Add redo step
                this.Text = redoStorage[redoStorage.Count - 1].text;
                // Position cursor after the last character
                if (this.Text.Length > 0)
                {
                    Select(redoStorage[redoStorage.Count - 1].caretPosition, 0);
                }
                // Remove from undo storage
                redoStorage.RemoveAt(redoStorage.Count - 1);
            }
        }

        new public bool CanUndo()
        {
            if (undoStorage.Count > 0)
                return true;
            else
                return false;
        }

        new public bool CanRedo()
        {
            if (redoStorage.Count > 0)
                return true;
            else
                return false;
        }

        private void PerformCleanup()
        {
            if (ReduceCache)
            {
                if (undoStorage.Count > maxHistoryCountWithReducedCache)
                {
                    undoStorage.RemoveRange(0, undoStorage.Count - maxHistoryCountWithReducedCache);
                }
            }
            else
            {
                if (undoStorage.Count > maxHistoryCount)
                {
                    undoStorage.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Stop adding changes to History
        /// </summary>
        public void SuspendHistory()
        {
            ignoreChange = true;
        }

        /// <summary>
        /// Start adding changes to History
        /// </summary>
        public void ResumeHistory()
        {
            ignoreChange = false;
        }


        /// <summary>
        /// Clear History
        /// </summary>
        public void ResetHistory()
        {
            InitializeStorage();
            undoStorage.Clear();
            redoStorage.Clear();
            isFirstUndo = true;
        }

        /// <summary>
        /// Delete the selected line
        /// </summary>
        private List<string> lst;
        private int firstCharIndex { get; set; }
        private int lineToBeRemoved { get; set; }
        private string tempContent { get; set; }
        private bool wordWrapped { get; set; }
        public void DeleteCurrentLine()
        {
            if (this.Text.Length > 0 && this.SelectionStart >= 0)
            {
                SuspendHistory();
                // Save current wordWrap state
                wordWrapped = this.WordWrap;
                if (lst == null)
                {
                    lst = new List<string>();
                }
                else
                {
                    lst.Clear();
                }

                // Disable wordWrap to get correct line count for selection
                this.WordWrap = false;
                lineToBeRemoved = this.GetLineFromCharIndex(this.SelectionStart);

                // If lineToBeRemoved exceeds the number of available lines
                // exit the method
                if (lineToBeRemoved >= this.Lines.Length)
                {
                    ResumeHistory();
                    return;
                }

                // Get the character index from the line after the selected for deletion
                if (lineToBeRemoved + 1 > this.Lines.Length - 1)
                {
                    firstCharIndex = this.GetFirstCharIndexFromLine(lineToBeRemoved);
                }
                else
                {
                    firstCharIndex = this.GetFirstCharIndexFromLine(lineToBeRemoved) + 1;
                }

                foreach (string s in this.Lines)
                {
                    if (s == "")
                    {
                        lst.Add("historytextboxnewlinesubstitutestring"); // Add a unique string replacer
                    }
                    else
                    {
                        lst.Add(s);
                    }
                }

                // Remove line
                lst.RemoveAt(lineToBeRemoved);

                tempContent = string.Empty;
                for (int i = 0; i <= lst.Count - 1; i++)
                {
                    if (tempContent == string.Empty)
                    {
                        tempContent = lst[i].ToString();
                    }
                    else
                    {
                        tempContent = tempContent + Environment.NewLine + lst[i].ToString();
                    }
                }

                tempContent = tempContent.Replace("historytextboxnewlinesubstitutestring", "");

                ResumeHistory();
                // Reinstate previous wordWrap state
                this.WordWrap = wordWrapped;
                // And insert new text
                this.Text = tempContent;

                // Position cursor at previous line
                if (firstCharIndex > 0 && this.Text.Length > 1)
                {
                    this.Select(firstCharIndex - 1, 0);
                }
            }
        }


        string TempUnformattedText { get; set; }
        int TempCaretPosition { get; set; }
        public void ClearFormatting()
        {
            this.SuspendLayout();
            // Save caret position
            TempCaretPosition = this.SelectionStart;
            // Set temp string
            TempUnformattedText = this.Text;
            // Reset textbox
            this.Clear();
            // Add unformatted content back and move cursor to last selected position
            SetText(TempUnformattedText, false, true, TempCaretPosition);
            this.ResumeLayout();
        }


        // Replace newlines with whitespace
        private const string tempParagraphReplacementString = "tempahoyreplacementparagraphstring";
        public void RemoveNewLines(bool preserveParagraphs, bool replaceTabsWithSpaces)
        {
            if (this.SelectedText.Length > 0)
            {
                this.SuspendLayout();
                try
                {

                    if (replaceTabsWithSpaces)
                    {
                        // Replace Tabs by four spaces
                        TempUnformattedText = Regex.Replace(this.SelectedText, @"\t", GetBlankSpace(4));
                    }

                    if (!preserveParagraphs)
                    {
                        // Set temp string
                        TempUnformattedText = Regex.Replace(this.SelectedText, @"\s+", " ");
                        TempUnformattedText = Regex.Replace(TempUnformattedText, @"\n*$", " ", RegexOptions.Multiline);
                    }
                    else
                    {
                        // Find new paragraphs and add a replacement string
                        TempUnformattedText = Regex.Replace(TempUnformattedText, @"\n\s+", tempParagraphReplacementString, RegexOptions.Multiline);
                        // Remove excessive blankspace (will preserve the above alteration as it uses the replacement string)
                        TempUnformattedText = Regex.Replace(TempUnformattedText, @"\s+", " ");
                        // Remove Carriage Returns/NewLines
                        TempUnformattedText = Regex.Replace(TempUnformattedText, @"\n*$", " ", RegexOptions.Multiline);
                        // Insert paragraphs again using the replacement string
                        TempUnformattedText = Regex.Replace(TempUnformattedText, tempParagraphReplacementString, Environment.NewLine + GetBlankSpace(4), RegexOptions.Singleline);
                    }
                    // Set textbox
                    this.SelectedText = TempUnformattedText;
                }
                catch
                {
                    Log.LogGeneralError("Operation failed (RemoveNewLines)");
                }
                this.ResumeLayout();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToPerformTextOperation(), MessageText.TextOperationCaption(), false);
                msg.ShowDialog();
            }
        }

        public void TabToSpace()
        {
            if (this.SelectedText.Length > 0)
            {
                this.SuspendLayout();
                try
                {
                    // Replace Tabs by four spaces
                    this.SelectedText = Regex.Replace(this.SelectedText, @"\t", GetBlankSpace(4));
                }
                catch
                {
                    Log.LogGeneralError("Operation failed (TabToSpace)");
                }

                this.ResumeLayout();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToPerformTextOperation(), MessageText.TextOperationCaption(), false);
                msg.ShowDialog();
            }
        }

        public void TrimSpace()
        {
            if (this.SelectedText.Length > 0)
            {
                this.SuspendLayout();
                try
                {
                    // Trim space while preserving newlines
                    this.SelectedText = Regex.Replace(this.SelectedText, @"[^\S\r\n]+", GetBlankSpace(1));
                }
                catch
                {
                    Log.LogGeneralError("Operation failed (TrimSpace)");
                }
                this.ResumeLayout();
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(MessageText.SelectToPerformTextOperation(), MessageText.TextOperationCaption(), false);
                msg.ShowDialog();
            }
        }

        private static string tempBlankSpaceString;
        internal static string GetBlankSpace(int count)
        {
            tempBlankSpaceString = "";
            for (int i = 0; i < count; i++)
            {
                tempBlankSpaceString += " ";
            }
            return tempBlankSpaceString;
        }
    }

    public class HistoryState
    {
        public string text { get; set; }
        public int caretPosition { get; set; }

        public HistoryState(string Text, int CaretPosition)
        {
            text = Text;
            caretPosition = CaretPosition;
        }
    }
}
