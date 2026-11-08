using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AhoyBrowser
{
    public partial class AnsiEditForm : Form
    {
        // Canvas Size
        private int SelectedCanvasWidth { get; set; }
        private int SelectedCanvasHeight { get; set; }
        private const int CanvasWidthA = 96;
        private const int CanvasHeightA = 28;
        private const int CanvasWidthB = 116;
        private const int CanvasHeightB = 38;
        private const int CanvasWidthC = 170;
        private const int CanvasHeightC = 50;

        private bool SelectionInProgress { get; set; }
        private const string ClipboardSetMessage = "Clipboard set";
        private const string NoCharactersFoundMessage = "Nothing to select :)";
        private const string SelectionErrorMessage = "Selection error...";
        private const string DeselectionMessage = "Deselect to continue...";
        private const string FillCharacter = "░";
        private int CanvasSizePreset { get; set; }

        public AnsiEditForm(double opacity)
        {
            InitializeComponent();
            SelectionInProgress = false;
            CanvasSizePreset = 0;

            // Set start position
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set form opacity
            this.Opacity = opacity;

            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            lbInfo.Text = "Press ARROW KEYS to move caret / KEYS 1-0 to insert / SPACE to delete";
        }

        private void SetFonts()
        {
            tbCanvas.Font = Design.ContentFieldFont();
            tbInfo.Font = Design.MessageboxSmallFont();
        }

        private void SetColorTheme()
        {
            // Index 0 Forms background
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            MainMenuStrip.BackColor = AhoyColors.CurrentColorTheme()[7]; // Menu top background

            // Labels
            lbInfo.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbInfo.BackColor = AhoyColors.CurrentColorTheme()[7];
            lbUndoRedo.BackColor = this.BackColor;
            lbPosition.BackColor = this.BackColor;
            lbTimesTen.BackColor = this.BackColor;
            lbCanvasSize.BackColor = this.BackColor;
            lbSelectAndCopy.BackColor = this.BackColor;
            lbCanvas.BackColor = this.BackColor;
            lbUndoRedo.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPosition.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTimesTen.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbCanvasSize.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbSelectAndCopy.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbCanvas.ForeColor = AhoyColors.CurrentColorTheme()[2];


            // Canvas
            tbCanvas.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbCanvas.ForeColor = AhoyColors.CurrentColorTheme()[30];
            pnlContainer.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbSplash.BackColor = AhoyColors.CurrentColorTheme()[29];
            lbSplash.ForeColor = AhoyColors.CurrentColorTheme()[30];
            // Info pane
            tbInfo.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbInfo.ForeColor = AhoyColors.CurrentColorTheme()[30];

            // Status strip
            statusStrip1.BackColor = AhoyColors.CurrentColorTheme()[7];
            lbStatus.BackColor = statusStrip1.BackColor;
            lbStatus.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Tool tip
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[30];

            // Buttons
            btnClear.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClear.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClear.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClear.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClear.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnCopy.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCopy.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCopy.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCopy.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCopy.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnInsert.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnInsert.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnInsert.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnInsert.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnInsert.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnSelect.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelect.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnSelect.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSelect.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSelect.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnDeselect.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDeselect.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDeselect.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDeselect.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDeselect.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnCanvasSizeA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCanvasSizeA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCanvasSizeA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCanvasSizeA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCanvasSizeA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnCanvasSizeB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCanvasSizeB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCanvasSizeB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCanvasSizeB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCanvasSizeB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnCanvasSizeC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCanvasSizeC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCanvasSizeC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCanvasSizeC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCanvasSizeC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnSelectAndCopy.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSelectAndCopy.ForeColor = AhoyColors.CurrentColorTheme()[6];
            btnSelectAndCopy.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSelectAndCopy.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSelectAndCopy.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnMoveDown.Text = "";
            btnMoveDown.BackgroundImage = ButtonBitmaps.Down();
            btnMoveDown.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMoveDown.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMoveDown.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMoveDown.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMoveDown.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnMoveLeft.Text = "";
            btnMoveLeft.BackgroundImage = ButtonBitmaps.Left();
            btnMoveLeft.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMoveLeft.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMoveLeft.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMoveLeft.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMoveLeft.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnMoveRight.Text = "";
            btnMoveRight.BackgroundImage = ButtonBitmaps.Right();
            btnMoveRight.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMoveRight.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMoveRight.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMoveRight.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMoveRight.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnMoveUp.Text = "";
            btnMoveUp.BackgroundImage = ButtonBitmaps.Up();
            btnMoveUp.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnMoveUp.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnMoveUp.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnMoveUp.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnMoveUp.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnNext.Text = "";
            btnNext.BackgroundImage = ButtonBitmaps.Up();
            btnNext.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnNext.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnNext.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnNext.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnNext.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnPrevious.Text = "";
            btnPrevious.BackgroundImage = ButtonBitmaps.Down();
            btnPrevious.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnPrevious.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnPrevious.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnPrevious.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnPrevious.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnRedo.Text = "";
            btnRedo.BackgroundImage = ButtonBitmaps.Right();
            btnRedo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRedo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRedo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRedo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRedo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnUndo.Text = "";
            btnUndo.BackgroundImage = ButtonBitmaps.Left();
            btnUndo.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUndo.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnUndo.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUndo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUndo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnRedoMore.Text = "";
            btnRedoMore.BackgroundImage = ButtonBitmaps.Right();
            btnRedoMore.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnRedoMore.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnRedoMore.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnRedoMore.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnRedoMore.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            btnUndoMore.Text = "";
            btnUndoMore.BackgroundImage = ButtonBitmaps.Left();
            btnUndoMore.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUndoMore.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnUndoMore.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUndoMore.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUndoMore.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Fonts
            SetFonts();
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCanvas();
            SetColorTheme();
            this.Text = MessageText.AnsiEditCaption();
            DisplaySplash();
        }

        private void DisplaySplash()
        {
            lbSplash.Font = Design.ContentFieldFont();
            lbSplash.Visible = true;
            lbSplash.Text = LogoReader.GetLogoText();
            lbSplash.Dock = DockStyle.Fill;
            lbSplash.BringToFront();
            timerLogo.Interval = 1000;
            timerLogo.Enabled = true;
        }

        private void InitializeCanvas()
        {
            ResolveCanvasSize(0, false);
        }

        // Save caret position
        private void RegisterCaretPosition()
        {
            // Save caret position
            caretPosition = tbCanvas.SelectionStart;
            DisplayCaretPosition();
        }

        private void DisplayCaretPosition()
        {
            if (LockedForSelection(false))
            {
                return;
            }
            lbStatus.Text = "Caret position: " + caretPosition.ToString("0000");
        }

        private bool ClearCanvas(string text, bool showMessage)
        {
            if (showMessage && CanvasHasChars())
            {
                AhoyAnswerMessageBox clearCanvasDialog = new AhoyAnswerMessageBox(text, MessageText.AttentionCaption(), false, false);
                clearCanvasDialog.ShowDialog();
                if (!clearCanvasDialog.Accepted)
                {
                    tbCanvas.Focus();
                    return false;
                }
                else
                {
                    tempCropCacheForDeselect = "";
                    SelectionInProgress = false;
                    ResolveSelectionButton();
                    ResolveCanvasSize(CanvasSizePreset, false);
                    tbCanvas.Focus();
                    return true;
                }
            }
            else
            {
                tempCropCacheForDeselect = "";
                SelectionInProgress = false;
                ResolveSelectionButton();
                ResolveCanvasSize(CanvasSizePreset, false);
                tbCanvas.Focus();
                return true;

            }
        }

        // Take input
        private static int caretPosition;
        private static int previousCaretPosition;
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            MainForm.KeepAwake();
            RegisterCaretPosition();
            if (e.KeyCode == Keys.Escape)
            {
                ClearCanvas("Clear Canvas?", true);
                return;
            }

            // Restrict caret to canvas area
            if (!IsInsideCanvas(caretPosition))
            {
                caretPosition = previousCaretPosition;
                SetCaret();
                // Suppress original keypress except arrows
                e.Handled = e.SuppressKeyPress = true;
                return;
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.C)
            {
                CopySelectionOrAllContent();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Copy();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                Paste();
            }
            else if (e.Control && e.KeyCode == Keys.W)
            {
                CloseForm();
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                // Suppress original keypress except arrows
                e.Handled = e.SuppressKeyPress = true;
                return;
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                SelectPreviousCharSet();
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                SelectNextCharSet();
            }
            else if (e.Alt && e.KeyCode == Keys.Up)
            {
                MoveCanvas(0);
            }
            else if (e.Alt && e.KeyCode == Keys.Down)
            {
                MoveCanvas(1);
            }
            else if (e.Alt && e.KeyCode == Keys.Left)
            {
                MoveCanvas(2);
            }
            else if (e.Alt && e.KeyCode == Keys.Right)
            {
                MoveCanvas(3);
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.C || e.KeyCode == Keys.D || e.KeyCode == Keys.E ||
                e.KeyCode == Keys.F || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.I || e.KeyCode == Keys.J ||
                e.KeyCode == Keys.K || e.KeyCode == Keys.L || e.KeyCode == Keys.M || e.KeyCode == Keys.N || e.KeyCode == Keys.O ||
                e.KeyCode == Keys.P || e.KeyCode == Keys.Q || e.KeyCode == Keys.R || e.KeyCode == Keys.S || e.KeyCode == Keys.T ||
                e.KeyCode == Keys.U || e.KeyCode == Keys.V || e.KeyCode == Keys.W || e.KeyCode == Keys.X || e.KeyCode == Keys.Y ||
                e.KeyCode == Keys.Z)
            {
                // Check for Caps lock and shift keys
                if (Control.IsKeyLocked(Keys.CapsLock) || e.Shift)
                {
                    InsertChar(e.KeyCode.ToString());
                }
                else
                {
                    InsertChar(e.KeyCode.ToString().ToLower());
                }

                // Suppress original keypress except arrows
                e.Handled = e.SuppressKeyPress = true;
                return;
            }

            // Handle char drawing
            switch (e.KeyData)
            {
                case Keys.D1:
                    InsertChar(AnsiEditCanvas.CS[0]);
                    break;
                case Keys.D2:
                    InsertChar(AnsiEditCanvas.CS[1]);
                    break;
                case Keys.D3:
                    InsertChar(AnsiEditCanvas.CS[2]);
                    break;
                case Keys.D4:
                    InsertChar(AnsiEditCanvas.CS[3]);
                    break;
                case Keys.D5:
                    InsertChar(AnsiEditCanvas.CS[4]);
                    break;
                case Keys.D6:
                    InsertChar(AnsiEditCanvas.CS[5]);
                    break;
                case Keys.D7:
                    InsertChar(AnsiEditCanvas.CS[6]);
                    break;
                case Keys.D8:
                    InsertChar(AnsiEditCanvas.CS[7]);
                    break;
                case Keys.D9:
                    InsertChar(AnsiEditCanvas.CS[8]);
                    break;
                case Keys.D0:
                    InsertChar(AnsiEditCanvas.CS[9]);
                    break;
                case Keys.Space:
                    InsertChar(AnsiEditCanvas.CS[10]);
                    break;
                case Keys.Delete:
                    InsertChar(AnsiEditCanvas.CS[10]);
                    break;
                case Keys.Back:
                    InsertChar(AnsiEditCanvas.CS[10]);
                    break;
            }

            // If any thing else than the keys already accepted plus the arrowkeys
            // suppress original keys
            if (e.KeyData != Keys.Up && e.KeyData != Keys.Down &&
                e.KeyData != Keys.Left && e.KeyData != Keys.Right)
            {
                // Suppress original keypress except arrows
                e.Handled = e.SuppressKeyPress = true;
            }
            else
            {
                previousCaretPosition = caretPosition;
            }

        }

        private bool IsInsideCanvas(int caretPosition)
        {
            if (caretPosition >= AnsiEditCanvas.GetCanvasUpperIndex() && caretPosition <= AnsiEditCanvas.GetCanvasLowerIndex())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void SetCaret()
        {
            if (caretPosition < tbCanvas.Text.Length)
            {
                // Focus canvas
                tbCanvas.Focus();
                // Select position
                tbCanvas.Select(caretPosition, 0);
            }
        }

        private string GetCurrentChar()
        {
            if (caretPosition < tbCanvas.Text.Length)
            {
                return tbCanvas.Text[caretPosition].ToString();
            }
            else
            {
                return "";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectNextCharSet();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SelectPreviousCharSet();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopySelectionOrAllContent();
            tbCanvas.Focus();
        }


        private void SelectNextCharSet()
        {
            AnsiEditCanvas.NextCharacterSet();
            tbInfo.Text = AnsiEditCanvas.GetInfoText();
            tbCanvas.Focus();
        }

        private void SelectPreviousCharSet()
        {
            AnsiEditCanvas.PreviousCharacterSet();
            tbInfo.Text = AnsiEditCanvas.GetInfoText();
            tbCanvas.Focus();
        }

        #region SELECT
        private string tempCropCacheForDeselect;
        private void tbCrop_Click(object sender, EventArgs e)
        {
            if (!SelectionInProgress)
            {
                tempCropCacheForDeselect = tbCanvas.Text;
                CropSelection();
            }
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            Deselect(false);
        }

        private void Deselect(bool clearCache)
        {
            if (clearCache)
                tempCropCacheForDeselect = "";

            if (SelectionInProgress)
            {
                croppedOutput = "";
                lbStatus.Text = "Ready";
                SelectionInProgress = false;
                ResolveSelectionButton();
                if (!string.IsNullOrEmpty(tempCropCacheForDeselect) && tempCropCacheForDeselect.Length > 0)
                {
                    tbCanvas.SetText(tempCropCacheForDeselect, false);
                }
            }
            else
            {
                tbCanvas.Focus();
            }
        }


        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbCrop_Click(this, null);
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeselect_Click(this, null);
        }


        // Find and draw chars around graphics on entire canvas with SelectionChar
        // Copy all lines that do not contain SelectionChar into List<string>...
        // Gets the first row index containing a char
        // Gets the last row index containing a char
        // Gets the first string index containing a char
        // Gets the last string index containing a char
        private List<string> tempListInput;
        private List<string> tempListSelection;
        private List<string> tempListEmptyCanvas;
        private int FirstRowWithChar;
        private int LastRowWithChar;
        private int XMinimum;
        private int XMaximum;
        private bool CharFound;
        private string croppedOutput = "";
        private string selectionOutput = "";
        private void CropSelection()
        {
            // Saml alle lines i en liste og behandl det som sådan
            // Løb listerne igennem og registrer i en ny liste hvilken der først og sidste har en char
            // Løb denne liste igenne og find det mindste og største index med char
            // Når dette er registreret saml disse rows i en tredige liste som bliver det croppede canvas
            FirstRowWithChar = 0;
            LastRowWithChar = 0;
            XMinimum = 0;
            XMaximum = 0;
            croppedOutput = "";
            selectionOutput = "";
            CharFound = false;

            #region MakeSelection
            try
            {
                if (tempListInput == null)
                {
                    tempListInput = new List<string>();
                    tempListSelection = new List<string>();
                    tempListEmptyCanvas = new List<string>();
                }
                tempListInput.Clear();
                tempListSelection.Clear();
                tempListEmptyCanvas.Clear();

                // Step 1: Get Canvas
                tempListInput.AddRange(tbCanvas.Text.Split('\n'));

                // Exit selection if no chars are found
                if (!ListContainsChar(tempListInput))
                {
                    lbStatus.Text = NoCharactersFoundMessage;
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(NoCharactersFoundMessage, MessageText.SelectionCaption(), false);
                    msg.ShowDialog();
                    SelectionInProgress = false;
                    ResolveSelectionButton(); // Must be called after setting SelectionInProgress
                    tbCanvas.Focus();
                    return;
                }
                else
                {
                    lbStatus.Text = "Selection active";
                    SelectionInProgress = true;
                    ResolveSelectionButton(); // Must be called after setting SelectionInProgress
                }

                // Fill canvas list with fillchars
                for (int i = 0; i < tempListInput.Count; i++)
                {
                    tempListEmptyCanvas.Add(TextTransform.GetRepeatedCharacter(tempListInput[i].Length, FillCharacter));
                }

                // Step 2: Check if Canvas has chars, else return
                CharFound = false;
                for (int i = 0; i < tempListInput.Count; i++)
                {
                    if (StringContainsChar(tempListInput[i]))
                    {
                        CharFound = true;
                        break;
                    }
                }
                if (!CharFound)
                {
                    return;
                }

                // Step 3: Gather info
                // Find top row index with char
                CharFound = false;
                for (int i = 0; i < tempListInput.Count - 1; i++)
                {
                    if (CharFound)
                        break;

                    if (StringContainsChar(tempListInput[i]))
                    {
                        FirstRowWithChar = i;
                        CharFound = true;
                    }
                }

                // Find bottom row index with char
                CharFound = false;
                for (int i = tempListInput.Count; i > 0; i--)
                {
                    if (CharFound)
                        break;

                    if (StringContainsChar(tempListInput[i - 1]))
                    {
                        LastRowWithChar = i;
                        CharFound = true;
                    }
                }


                // IF chars are only present on one line
                XMinimum = tempListInput[FirstRowWithChar].Length - 1; // Get length of line
                if (FirstRowWithChar == LastRowWithChar)
                {
                    // Get smallest row index
                    for (int i = 0; i < tempListInput[FirstRowWithChar].Length; i++)
                    {
                        if (CharContainsChar(tempListInput[FirstRowWithChar][i]))
                        {
                            if (i < XMinimum)
                            {
                                XMinimum = i;
                            }
                        }
                    }
                }
                else
                {
                    // Find smallest and largest row index with char    
                    for (int i = FirstRowWithChar; i < LastRowWithChar; i++)
                    {
                        //if (CharFound)
                        //    break;

                        // Get smallest row index
                        for (int j = 0; j < tempListInput[i].Length; j++)
                        {
                            if (CharContainsChar(tempListInput[i][j]))
                            {
                                if (j < XMinimum)
                                {
                                    XMinimum = j;
                                }
                            }
                        }
                    }
                }

                // Get largest row index starting backwards
                XMaximum = 0;
                //LastRowWithChar = LastRowWithChar + 1;
                if (FirstRowWithChar == LastRowWithChar)
                {
                    // Get smallest row index
                    for (int i = 0; i < tempListInput[FirstRowWithChar].Length; i++)
                    {
                        if (CharContainsChar(tempListInput[FirstRowWithChar][i]))
                        {
                            if (i > XMaximum)
                            {
                                XMaximum = i;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = LastRowWithChar; i > FirstRowWithChar; i--)
                    {
                        // Find char running backwards
                        for (int j = tempListInput[i - 1].Length - 1; j > 0; j--)
                        {
                            if (CharContainsChar(tempListInput[i - 1][j]))
                            {
                                if (j > XMaximum)
                                {
                                    XMaximum = j;
                                }
                            }
                        }
                    }
                }

                // Step 4: Collect content within the bounds of the indexes
                if (FirstRowWithChar == LastRowWithChar && XMinimum == XMaximum) // If same row and one char
                {
                    tempListSelection.Add(tempListInput[FirstRowWithChar].Substring(XMinimum, 1));
                }
                else if (FirstRowWithChar == LastRowWithChar) // Else if same row but more chars
                {
                    tempListSelection.Add(tempListInput[FirstRowWithChar].Substring(XMinimum, (XMaximum - XMinimum + 1)));
                }
                else// Else if more rows, proceed as normal
                {
                    for (int i = FirstRowWithChar; i < LastRowWithChar; i++)
                    {
                        tempListSelection.Add(tempListInput[i].Substring(XMinimum, (XMaximum - XMinimum + 1)));
                    }
                }
                // Add selection to clipboard
                for (int i = 0; i < tempListSelection.Count; i++)
                {
                    croppedOutput += tempListSelection[i] + Environment.NewLine;
                }
            }
            catch
            {
                lbStatus.Text = SelectionErrorMessage;
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(SelectionErrorMessage, MessageText.SelectionCaption(), false);
                msg.ShowDialog();
                SelectionInProgress = false;
                ResolveSelectionButton();
            }
            #endregion

            // -----------------------------------   DISPLAY CROP ------------------------------------------

            // Display selection on canvas
            // Fill the selectionOutput with fillChars
            selectionOutput = "";
            // Insert selection into empty selection canvas
            for (int i = 0; i < tempListEmptyCanvas.Count; i++)
            {
                if (i >= FirstRowWithChar && i <= LastRowWithChar)
                {
                    for (int j = 0; j < tempListSelection.Count; j++)
                    {
                        tempListEmptyCanvas[i] = SurroundStringWidthFillChar(XMinimum, tempListSelection[j], tempListEmptyCanvas[i].Length, FillCharacter);
                        i += 1;
                    }
                }

            }

            // Add result to output
            for (int i = 0; i < tempListEmptyCanvas.Count; i++)
            {
                if (i == tempListEmptyCanvas.Count - 1)
                {
                    selectionOutput += tempListEmptyCanvas[i];
                }
                else
                {
                    selectionOutput += tempListEmptyCanvas[i] + Environment.NewLine;
                }

            }

            // Set canvas
            tbCanvas.SetText(selectionOutput, false);
            SetCaret();
        }

        private string tempSurroundResult;
        internal string SurroundStringWidthFillChar(int inputStartIndex, string input, int totalLength, string repeatedChar)
        {
            tempSurroundResult = "";
            for (int i = 0; i < totalLength; i++)
            {
                if (i == inputStartIndex && input.Length > 1)
                {
                    tempSurroundResult += input;
                    i += input.Length - 1;
                }
                else if (i == inputStartIndex && input.Length == 1)
                {
                    tempSurroundResult += input;
                }
                else
                {
                    tempSurroundResult += repeatedChar;
                }
            }

            return tempSurroundResult;
        }


        #region CLIPBOARD SET OPERATION
        private void SetClipboardText(string input)
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
        #endregion
        #endregion


        private void selectAndCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAndCopy();
        }

        private void btnSelectAndCopy_Click(object sender, EventArgs e)
        {
            SelectAndCopy();
        }

        private void SelectAndCopy()
        {
            if (!LockedForSelection(true))
            {
                if (CanvasHasChars())
                {
                    tempCropCacheForDeselect = tbCanvas.Text;
                    CropSelection();
                    timerSelectAndCopy.Interval = 500;
                    timerSelectAndCopy.Enabled = true;
                }
                else
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(NoCharactersFoundMessage, MessageText.SelectionCaption(), false);
                    msg.ShowDialog();
                }
            }
        }

        private void timerSelectAndCopy_Tick(object sender, EventArgs e)
        {
            timerSelectAndCopy.Enabled = false;
            CopySelectionOrAllContent();
            tbCanvas.Focus();
        }

        private void CopySelectionOrAllContent()
        {
            if (!string.IsNullOrEmpty(croppedOutput) && croppedOutput.Length > 0)
            {
                SetClipboardText(croppedOutput);
                MainForm.PersistClipboard();
                SetCaret();
            }
            else
            {
                tbCanvas.Select(AnsiEditCanvas.HeaderCharCount, AnsiEditCanvas.CanvasEndIndex);
                tbCanvas.Copy();
                MainForm.PersistClipboard();
                tbCanvas.DeselectAll();
                // Select position
                SetCaret();
            }
            btnDeselect_Click(this, null);
            lbStatus.Text = ClipboardSetMessage;
        }

        private void tbCanvas_SelectionChanged(object sender, EventArgs e)
        {
            // Nothing
        }

        private void tbCanvas_CursorChanged(object sender, EventArgs e)
        {
            // Nothing
        }

        private void tbCanvas_Click(object sender, EventArgs e)
        {
            // Nothing
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearCanvas("Clear Canvas?", true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            AhoyAnswerMessageBox clearCanvasDialog = new AhoyAnswerMessageBox("Exit ANSI Edit?", MessageText.AttentionCaption(), false, false);
            clearCanvasDialog.ShowDialog();
            if (clearCanvasDialog.Accepted)
            {
                this.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AhoyInfoMessageBox clearCanvasDialog = new AhoyInfoMessageBox(AnsiEditCanvas.GetHelpText(), MessageText.AboutCaption(), false, true, this.Opacity);
            clearCanvasDialog.ShowDialog();
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertContentFromClipboard();
        }

        private void timerLogo_Tick(object sender, EventArgs e)
        {
            lbSplash.Visible = false;
            timerLogo.Enabled = false;
            tbCanvas.ClearTextAndResetHistory();
            InitializeCanvas();
        }

        private void tbCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            RegisterCaretPosition();
        }

        private void tbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            RegisterCaretPosition();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbCanvas.Undo();
            // Select position
            SetCaret();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbCanvas.Redo();
            // Select position
            SetCaret();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCanvas("Clear Canvas?", true);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertContentFromClipboard();
        }

        private void copyContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectionOrAllContent();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void btnUndoMore_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Undo();
            }
        }

        private void btnRedoMore_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Redo();
            }
        }

        private void undoX10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUndoMore_Click(this, null);
        }

        private void redoX10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRedoMore_Click(this, null);
        }


        #region EDIT

        static string TempUnformattedText { get; set; }
        private string ClearFormatting(string text)
        {
            TempUnformattedText = text;
            return TempUnformattedText;
        }

        private void ClearFormatting()
        {
            tbCanvas.SuspendLayout();
            // Save caret position
            RegisterCaretPosition();
            // Save composition
            TempUnformattedText = tbCanvas.Text;
            // Reset textbox
            tbCanvas.Clear();
            // Add unformatted content back
            tbCanvas.SetText(TempUnformattedText, false);
            tbCanvas.ResumeLayout();
            // Set caret
            SetCaret();
        }


        // Insert and clean text
        static string tempString;
        static List<string> tempList;
        private void InsertContentFromClipboard()
        {
            if (LockedForSelection(true))
            {
                return;
            }

            // If a string is more than CanvasWidth chars wide, use only the CanvasWidth
            // If a string is les than CanvasWidth chars wide, fill in blank spaces up to CanvasWidth
            // Only use CanvasHeight rows
            if (!ClearCanvas("Insert Text?", true))
            {
                tbCanvas.Focus();
                return;
            }

            if (tempList == null)
            {
                tempList = new List<string>();
            }
            else
            {
                tempList.Clear();
            }

            tempString = "";
            if (Clipboard.ContainsText())
            {
                tempString = Clipboard.GetText(TextDataFormat.UnicodeText);
                tempString = ClearFormatting(tempString);
                tempList.AddRange(tempString.Split('\n'));
                tbCanvas.Text = "";

                // If there is less than CanvasHeight rows in the list add up to CanvasHeight with CanvasWidth blank spaces in each
                if (tempList.Count < SelectedCanvasHeight)
                {
                    for (int k = tempList.Count; k < SelectedCanvasHeight; k++)
                    {
                        tempList.Add(AnsiEditCanvas.GetBlankSpace(SelectedCanvasWidth));
                    }
                }

                // Clean strings
                for (int l = 0; l < tempList.Count; l++)
                {
                    tempList[l] = tempList[l].Replace("\r", "");
                    tempList[l] = tempList[l].Replace("\n", "");
                }

                // Run through CanvasHeight rows and print their content, adding whitespace if missing to
                // fill the total of CanvasWidth columns pr. row
                for (int i = 0; i < SelectedCanvasHeight; i++)
                {
                    if (tempList[i].Length >= SelectedCanvasWidth)
                    {
                        if (i < SelectedCanvasHeight - 1)
                        {
                            tbCanvas.Text += tempList[i].Substring(0, SelectedCanvasWidth) + Environment.NewLine;
                        }
                        else
                        {
                            tbCanvas.Text += tempList[i].Substring(0, SelectedCanvasWidth);
                        }
                    }
                    else if (tempList[i].Length < SelectedCanvasWidth)
                    {
                        tempList[i] += AnsiEditCanvas.GetBlankSpace(SelectedCanvasWidth - tempList[i].Length);

                        // Only add new line if not the last row
                        if (i < SelectedCanvasHeight - 1)
                        {
                            tbCanvas.Text += tempList[i] + Environment.NewLine;
                        }
                        else
                        {
                            tbCanvas.Text += tempList[i];
                        }
                    }
                }

                // Center the caret
                CenterCaret();
            }
        }

        private void CenterCaret()
        {
            // Center the caret
            tbCanvas.Select(AnsiEditCanvas.GetCanvasCenterIndex(), 0);
        }

        // Replace and insert character
        private void InsertChar(string character)
        {
            if (!SelectionInProgress)
            {
                // Remove old character if not end of line
                if (AnsiEditCanvas.IsCharacterValid(GetCurrentChar(), caretPosition))
                {
                    tbCanvas.SuspendHistory();
                    tbCanvas.Text = tbCanvas.Text.Remove(caretPosition, 1);
                    tbCanvas.ResumeHistory();
                    // Insert new character
                    tbCanvas.Text = tbCanvas.Text.Insert(caretPosition, character);
                    // Select position
                    SetCaret();
                }
                else
                {
                    lbStatus.Text = "*** CANVAS BORDER ***";
                }
            }
            else
            {
                LockedForSelection(false);
            }
        }

        private void ResolveSelectionButton()
        {
            if (SelectionInProgress)
            {
                btnSelect.ForeColor = AhoyColors.CurrentColorTheme()[6];
                btnDeselect.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnSelect.ForeColor = AhoyColors.CurrentColorTheme()[8];
                btnDeselect.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private bool LockedForSelection(bool displayMessage)
        {
            if (SelectionInProgress)
            {
                ResolveSelectionButton();
                lbStatus.Text = DeselectionMessage;
                if (displayMessage)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(DeselectionMessage, MessageText.SelectionCaption(), false);
                    msg.ShowDialog();
                }
                return true;
            }
            else
            {
                ResolveSelectionButton();
                return false;
            }
        }

        private void Undo()
        {
            if (!LockedForSelection(true))
            {
                tbCanvas.Undo();
                // Select position
                SetCaret();
            }
        }

        private void Redo()
        {
            if (!LockedForSelection(true))
            {
                tbCanvas.Redo();
                // Select position
                SetCaret();
            }
        }

        private void Copy()
        {
            Clipboard.SetText(GetCurrentChar());
            MainForm.PersistClipboard();
        }

        private void Paste()
        {
            if (AnsiEditCanvas.IsCharacterValid(Clipboard.GetText(), caretPosition))
            {
                InsertChar(Clipboard.GetText());
                ClearFormatting();
            }
        }

        #region MOVE CANVAS

        // Insert and clean text
        static string tempCanvas;
        static List<string> tempCanvasList;
        private void MoveCanvas(int direction) // 0 = up, 1 = down, 2 = left, 3 = right
        {
            if (LockedForSelection(true))
            {
                return;
            }

            if (tbCanvas.Text.Length > 0 && !timerLogo.Enabled)
            {
                tbCanvas.SuspendHistory();
                if (tempCanvasList == null)
                {
                    tempCanvasList = new List<string>();
                }
                else
                {
                    tempCanvasList.Clear();
                }

                // Split content into canvas list
                tempCanvas = tbCanvas.Text;
                tempCanvasList.AddRange(tempCanvas.Split('\n'));

                switch (direction)
                {
                    case 0:  // UP
                        if (!StringContainsChar(tempCanvasList[0]))// Remove first row and ad new at end
                        {
                            tempCanvasList.RemoveAt(0);
                            tempCanvasList.Add(AnsiEditCanvas.GetBlankSpace(SelectedCanvasWidth));
                        }
                        break;
                    case 1: // DOWN
                        if (!StringContainsChar(tempCanvasList[tempCanvasList.Count - 1]))// Remove last row and ad new at top
                        {
                            tempCanvasList.RemoveAt(tempCanvasList.Count - 1);
                            tempCanvasList.Insert(0, AnsiEditCanvas.GetBlankSpace(SelectedCanvasWidth));
                        }
                        break;
                    case 2: // LEFT
                        // Remove first char in each row and ad new blank space at the end of each row
                        for (int i = 0; i < tempCanvasList.Count - 1; i++)
                        {
                            if (i >= 0 && i < tempCanvasList.Count - 1)
                            {
                                if (!CharContainsChar(tempCanvasList[i][0]))
                                {
                                    tempCanvasList[i] = tempCanvasList[i].Insert(tempCanvasList[i].Length, AnsiEditCanvas.GetBlankSpace(1));
                                    tempCanvasList[i] = tempCanvasList[i].Remove(0, 1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 3: // RIGHT
                        // Remove last char in each row and ad new blank space at the start of each row
                        for (int i = 0; i < tempCanvasList.Count; i++)
                        {
                            if (i >= 0 && i < tempCanvasList.Count)
                            {
                                if (!CharContainsChar(tempCanvasList[i][tempCanvasList[i].Length - 1]))
                                {
                                    tempCanvasList[i] = tempCanvasList[i].Insert(0, AnsiEditCanvas.GetBlankSpace(1));
                                    tempCanvasList[i] = tempCanvasList[i].Remove(tempCanvasList[i].Length - 1, 1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                }

                // Fill content back into the canvas
                // Run through CanvasHeight rows and print their content, adding whitespace if missing to
                // fill the total of CanvasWidth columns pr. row
                tempCanvas = "";
                for (int i = 0; i < tempCanvasList.Count; i++)
                {
                    // Only add new line if not the last row
                    if (i < SelectedCanvasHeight - 1)
                    {
                        tempCanvas += tempCanvasList[i] + Environment.NewLine;
                    }
                    else
                    {
                        tempCanvas += tempCanvasList[i];
                    }
                }

                tbCanvas.ResumeHistory();
                tbCanvas.SetText(tempCanvas, false);
                SetCaret();
            }
        }

        #endregion

        private bool ListContainsChar(List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (string s in list)
                {
                    if (StringContainsChar(s))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool StringContainsChar(string s)
        {
            if (!string.IsNullOrEmpty(s) && s.Length > 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != ' ')
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool CharContainsChar(char c)
        {
            if (c != ' ')
            {
                return true;
            }
            return false;
        }
        #endregion


        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveCanvas(0);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MoveCanvas(1);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            MoveCanvas(2);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            MoveCanvas(3);
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCanvas(0);
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCanvas(1);
        }

        private void moveLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCanvas(2);
        }

        private void moveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCanvas(3);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }



        private void btnCanvasSizeA_Click(object sender, EventArgs e)
        {
            ResolveCanvasSize(0, true);
        }

        private void btnCanvasSizeB_Click(object sender, EventArgs e)
        {
            ResolveCanvasSize(1, true);
        }

        private void btnCanvasSizeC_Click(object sender, EventArgs e)
        {
            ResolveCanvasSize(2, true);
        }

        private void ResolveCanvasSizeButtons()
        {
            switch (CanvasSizePreset)
            {
                case 0:
                    btnCanvasSizeA.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    btnCanvasSizeB.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnCanvasSizeC.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 1:
                    btnCanvasSizeA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnCanvasSizeB.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    btnCanvasSizeC.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    break;
                case 2:
                    btnCanvasSizeA.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnCanvasSizeB.ForeColor = AhoyColors.CurrentColorTheme()[8];
                    btnCanvasSizeC.ForeColor = AhoyColors.CurrentColorTheme()[6];
                    break;
            }
        }

        private bool CanvasHasChars()
        {
            if (StringContainsChar(tbCanvas.Text.Replace('\n', ' ')))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResolveCanvasSize(int canvasSize, bool showMessage)
        {
            if (CanvasHasChars() && showMessage)
            {
                AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Clear Canvas?", MessageText.CanvasCaption(), false, false);
                msg.ShowDialog();
                if (!msg.Accepted)
                {
                    return;
                }
            }
            // Exit selection
            Deselect(true);
            // Select character set
            AnsiEditCanvas.SelectCharacterSet(0);
            // Set info and help fields
            tbInfo.Text = AnsiEditCanvas.GetInfoText();
            //tbHelp.Text = Canvas.GetHelpText();
            tbCanvas.Focus();
            CanvasSizePreset = canvasSize;
            // Create and set Canvas
            switch (canvasSize)
            {
                case 0:
                    btnCanvasSizeA.Text = "[Small]";
                    btnCanvasSizeB.Text = "Medium";
                    btnCanvasSizeC.Text = "Large";
                    SelectedCanvasWidth = CanvasWidthA;
                    SelectedCanvasHeight = CanvasHeightA;
                    tbCanvas.Font = Design.ContentFieldFont();
                    tbCanvas.SetText(AnsiEditCanvas.GetNewCanvas(SelectedCanvasWidth, SelectedCanvasHeight), false);
                    break;
                case 1:
                    btnCanvasSizeA.Text = "Small";
                    btnCanvasSizeB.Text = "[Medium]";
                    btnCanvasSizeC.Text = "Large";
                    SelectedCanvasWidth = CanvasWidthB;
                    SelectedCanvasHeight = CanvasHeightB;
                    tbCanvas.Font = Design.MessageboxXSmallFont();
                    tbCanvas.SetText(AnsiEditCanvas.GetNewCanvas(SelectedCanvasWidth, SelectedCanvasHeight), false);
                    break;
                case 2:
                    btnCanvasSizeA.Text = "Small";
                    btnCanvasSizeB.Text = "Medium";
                    btnCanvasSizeC.Text = "[Large]";
                    SelectedCanvasWidth = CanvasWidthC;
                    SelectedCanvasHeight = CanvasHeightC;
                    tbCanvas.Font = Design.StyleEditorXXSSmallFont();
                    tbCanvas.SetText(AnsiEditCanvas.GetNewCanvas(SelectedCanvasWidth, SelectedCanvasHeight), false);
                    break;
            }

            // Center the caret
            tbCanvas.Select(AnsiEditCanvas.GetCanvasCenterIndex(), 0);
            // Clear status
            lbStatus.Text = "Ready";
            ResolveCanvasSizeButtons();
        }

        private void smallCanvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCanvasSizeA_Click(this, null);
        }

        private void mediumCanvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCanvasSizeB_Click(this, null);
        }

        private void largeCanvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCanvasSizeC_Click(this, null);
        }

  
    }
}
