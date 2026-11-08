using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AhoyBrowser
{
    public partial class ParagraphDividers : Form
    {
        // Shortcuts
        private const string ShortcutA = "CTRL+";
        private const string ShortcutB = "CTRL,";
        private const string ShortcutC = "CTRL.";
        private const string ShortcutD = "CTRL-";
        // Labels
        private const string Intro = "A text-vignette to mark the beginning/end of a text block by pressing the corresponding keyboard shortcuts listed below. Use empty lines to add space above and below the Divider. Create custom Tab indents, text snippets, your favourite quotes...";
        private const string Tag1 = "Click to insert ISO Date/Time Tag";
        private const string Tag2 = "Click to insert Selection Tag";
        // Preset strings
        private static List<string> PresetList { get; set; }
        // 1-4 Selected strings
        internal static string DividerAString { get; set; }
        internal static string DividerBString { get; set; }
        internal static string DividerCString { get; set; }
        internal static string DividerDString { get; set; }
        // Initial Values
        internal static string InitialDividerAString { get; set; }
        internal static string InitialDividerBString { get; set; }
        internal static string InitialDividerCString { get; set; }
        internal static string InitialDividerDString { get; set; }
        // Utility fields
        private bool SuppressTextChanged { get; set; }
        private int SelectedPreset { get; set; }
        // Timestamp and selection values
        internal const string TimeStampReplaceString = "[pd=time]";
        internal const string SelectionReplaceString = "[pd=select]";

        public ParagraphDividers()
        {
            InitializeComponent();
            // Create the Presets
            InitializePresetList();
            // Default preset (used when cycling presets
            SelectedPreset = 0;
            InitialDividerAString = "";
            InitialDividerBString = "";
            InitialDividerCString = "";
            InitialDividerDString = "";
            // Form takes keypress
            this.KeyPreview = true;
            this.KeyDown += ParagraphDividersEditor_KeyDown;
            // Tooltip
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            // Text changed
            SuppressTextChanged = false;
            // Context Menu
            tbDeviderA.ContextMenuStrip = contextMenuBasic;
            tbDeviderB.ContextMenuStrip = contextMenuBasic;
            tbDeviderC.ContextMenuStrip = contextMenuBasic;
            tbDeviderD.ContextMenuStrip = contextMenuBasic;
            // Set theme and content
            SetColorTheme();
            LoadDividerStringsFromMainForm();
            // Set instructions text
            lbInstructions.Text = Intro;
            lbTimestampTag.Text = Tag1;
            lbSelectionTag.Text = Tag2;
        }

        #region Initialize presets
        // Load presets into Preset List
        internal static void InitializePresetList()
        {
            if (PresetList == null)
            {
                PresetList = new List<string>();
            }

            // Build presets if the list is empty
            if (PresetList.Count == 0)
            {
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "*** DONE ***" + TextTransform.GetNewLines(2));
                // Preset
                PresetList.Add(TextTransform.GetBlankSpace(2) + "*** BLOCK START *** (" + TimeStampReplaceString + ")" + Environment.NewLine +
                    "Title: " + SelectionReplaceString + TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "*** BLOCK END ***");
                // Preset
                PresetList.Add(TextTransform.GetNewLines(1) + "Name: " + SelectionReplaceString + Environment.NewLine +
                    "Email: " + Environment.NewLine +
                    "URL: ");
                // Preset
                PresetList.Add(TextTransform.GetNewLines(1) + TextTransform.GetBlankSpace(2) + "-== Start (" + TimeStampReplaceString + ") ==-" + TextTransform.GetNewLines(2) +
                        SelectionReplaceString + TextTransform.GetNewLines(1));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(1) + "/////////////// " + TimeStampReplaceString + " ///////////////" + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(1) + TextTransform.GetBlankSpace(2) + TimeStampReplaceString + "\n>------- PARAGRAPH START" + TextTransform.GetNewLines(2));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "/*** DONE ***/ (" + TimeStampReplaceString + ")" + TextTransform.GetNewLines(4));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "+--------------------+" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "| #> TODO            |" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "+--------------------+" + TextTransform.GetNewLines(4));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "-- // --" + TextTransform.GetNewLines(4));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "-== // ==-" + TextTransform.GetNewLines(4));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + "//////// " + SelectionReplaceString + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + "#------------>" + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + "<------------o" + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + "-------------------------" + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "/*** BLOCK START ***/" + TextTransform.GetNewLines(4));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "+-----====-----+" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "|--   DONE   --|" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "+-----====-----+" + TextTransform.GetNewLines(2));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + SelectionReplaceString + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "########################" + TextTransform.GetNewLines(3));
                // Preset
                PresetList.Add(TextTransform.GetNewLines(2) + TextTransform.GetBlankSpace(2) + "+----------------------+" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "| PENDING              |" + TextTransform.GetNewLines(1) +
                        TextTransform.GetBlankSpace(2) + "+======================+" + TextTransform.GetNewLines(2));
            }
        }

        public static string GetPresetValue(int index)
        {
            InitializePresetList();
            return PresetList[index];
        }
        #endregion

        private void SetInitialStrings()
        {
            // Set initial strings
            InitialDividerAString = DividerAString;
            InitialDividerBString = DividerBString;
            InitialDividerCString = DividerCString;
            InitialDividerDString = DividerDString;
        }

        public void LoadDividerStringsFromMainForm()
        {
            // Set strings from main form
            DividerAString = MainForm.DividerAString;
            DividerBString = MainForm.DividerBString;
            DividerCString = MainForm.DividerCString;
            DividerDString = MainForm.DividerDString;

            // If emtpy select a preset
            if (string.IsNullOrEmpty(DividerAString))
            {
                DividerAString = PresetList[0].Replace("\r\n", "\n");
            }

            if (string.IsNullOrEmpty(DividerBString))
            {
                DividerBString = PresetList[1].Replace("\r\n", "\n");
            }

            if (string.IsNullOrEmpty(DividerCString))
            {
                DividerCString = PresetList[2].Replace("\r\n", "\n");
            }

            if (string.IsNullOrEmpty(DividerDString))
            {
                DividerDString = PresetList[3].Replace("\r\n", "\n");
            }
            SetInitialStrings();
            UpdateTextBoxes();
            ResolveSaveButton();
        }

        private void UpdateTextBoxes()
        {
            SuppressTextChanged = true;
            tbDeviderA.Text = DividerAString;
            tbDeviderB.Text = DividerBString;
            tbDeviderC.Text = DividerCString;
            tbDeviderD.Text = DividerDString;
            SuppressTextChanged = false;
            UpdateLabels();
        }


        #region Form and Tooltip
        private void ParagraphDividersEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                SubmitChangesAndClose();
                e.Handled = true;
            }
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
        #endregion

        #region Set color theme
        private void SetColorTheme()
        {
            lbInstructions.Font = Design.MessageboxXSmallFont();
            lbClearA.Font = Design.MessageboxXSmallFont();
            lbClearB.Font = Design.MessageboxXSmallFont();
            lbClearC.Font = Design.MessageboxXSmallFont();
            lbClearD.Font = Design.MessageboxXSmallFont();
            lbPasteA.Font = Design.MessageboxXSmallFont();
            lbPasteB.Font = Design.MessageboxXSmallFont();
            lbPasteC.Font = Design.MessageboxXSmallFont();
            lbPasteD.Font = Design.MessageboxXSmallFont();
            lbCopyA.Font = Design.MessageboxXSmallFont();
            lbCopyB.Font = Design.MessageboxXSmallFont();
            lbCopyC.Font = Design.MessageboxXSmallFont();
            lbCopyD.Font = Design.MessageboxXSmallFont();
            lbTimestampTag.Font = Design.MessageboxXSmallFont();
            lbSelectionTag.Font = Design.MessageboxXSmallFont();
            lbResetA.Font = Design.MessageboxXSmallFont();
            lbResetB.Font = Design.MessageboxXSmallFont();
            lbResetC.Font = Design.MessageboxXSmallFont();
            lbResetD.Font = Design.MessageboxXSmallFont();


            btnUpA.BackgroundImage = ButtonBitmaps.Up();
            btnUpA.BackgroundImageLayout = ImageLayout.Center;
            btnUpA.Text = "";
            btnDownA.BackgroundImage = ButtonBitmaps.Down();
            btnDownA.BackgroundImageLayout = ImageLayout.Center;
            btnDownA.Text = "";
            btnUpA.FlatStyle = FlatStyle.Flat;
            btnUpA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUpA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUpA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnUpA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUpA.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDownA.FlatStyle = FlatStyle.Flat;
            btnDownA.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDownA.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDownA.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDownA.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDownA.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnUpB.BackgroundImage = ButtonBitmaps.Up();
            btnUpB.BackgroundImageLayout = ImageLayout.Center;
            btnUpB.Text = "";
            btnDownB.BackgroundImage = ButtonBitmaps.Down();
            btnDownB.BackgroundImageLayout = ImageLayout.Center;
            btnDownB.Text = "";
            btnUpB.FlatStyle = FlatStyle.Flat;
            btnUpB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUpB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUpB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnUpB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUpB.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDownB.FlatStyle = FlatStyle.Flat;
            btnDownB.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDownB.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDownB.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDownB.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDownB.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnUpC.BackgroundImage = ButtonBitmaps.Up();
            btnUpC.BackgroundImageLayout = ImageLayout.Center;
            btnUpC.Text = "";
            btnDownC.BackgroundImage = ButtonBitmaps.Down();
            btnDownC.BackgroundImageLayout = ImageLayout.Center;
            btnDownC.Text = "";
            btnUpC.FlatStyle = FlatStyle.Flat;
            btnUpC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUpC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUpC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnUpC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUpC.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDownC.FlatStyle = FlatStyle.Flat;
            btnDownC.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDownC.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDownC.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDownC.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDownC.ForeColor = AhoyColors.CurrentColorTheme()[8];

            btnUpD.BackgroundImage = ButtonBitmaps.Up();
            btnUpD.BackgroundImageLayout = ImageLayout.Center;
            btnUpD.Text = "";
            btnDownD.BackgroundImage = ButtonBitmaps.Down();
            btnDownD.BackgroundImageLayout = ImageLayout.Center;
            btnDownD.Text = "";
            btnUpD.FlatStyle = FlatStyle.Flat;
            btnUpD.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnUpD.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnUpD.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnUpD.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnUpD.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnDownD.FlatStyle = FlatStyle.Flat;
            btnDownD.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnDownD.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnDownD.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnDownD.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnDownD.ForeColor = AhoyColors.CurrentColorTheme()[8];


            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];
            lbDeviderShortcutA.BackColor = Color.Transparent;
            lbDeviderShortcutB.BackColor = Color.Transparent;
            lbDeviderShortcutC.BackColor = Color.Transparent;
            lbDeviderShortcutD.BackColor = Color.Transparent;
            lbInstructions.BackColor = Color.Transparent;
            lbTimestampTag.BackColor = Color.Transparent;
            lbSelectionTag.BackColor = Color.Transparent;
            lbResetA.BackColor = Color.Transparent;
            lbResetB.BackColor = Color.Transparent;
            lbResetC.BackColor = Color.Transparent;
            lbResetD.BackColor = Color.Transparent;

            lbDeviderShortcutA.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbDeviderShortcutB.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbDeviderShortcutC.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbDeviderShortcutD.ForeColor = AhoyColors.CurrentColorTheme()[6];
            lbInstructions.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbTimestampTag.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbSelectionTag.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbResetA.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbResetB.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbResetC.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbResetD.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbDeviderShortcutA.Font = Design.ContentFieldFont();
            lbDeviderShortcutB.Font = Design.ContentFieldFont();
            lbDeviderShortcutC.Font = Design.ContentFieldFont();
            lbDeviderShortcutD.Font = Design.ContentFieldFont();

            lbCopyA.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPasteA.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbClearA.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbCopyB.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPasteB.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbClearB.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbCopyC.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPasteC.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbClearC.ForeColor = AhoyColors.CurrentColorTheme()[2];

            lbCopyD.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbPasteD.ForeColor = AhoyColors.CurrentColorTheme()[2];
            lbClearD.ForeColor = AhoyColors.CurrentColorTheme()[2];

            tbDeviderA.Font = Design.ContentFieldFont();
            tbDeviderB.Font = Design.ContentFieldFont();
            tbDeviderC.Font = Design.ContentFieldFont();
            tbDeviderD.Font = Design.ContentFieldFont();

            tbDeviderA.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbDeviderA.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbDeviderB.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbDeviderB.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbDeviderC.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbDeviderC.ForeColor = AhoyColors.CurrentColorTheme()[30];
            tbDeviderD.BackColor = AhoyColors.CurrentColorTheme()[29];
            tbDeviderD.ForeColor = AhoyColors.CurrentColorTheme()[30];

            btnSaveAndClose.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnClearAll.FlatStyle = FlatStyle.Flat;
            btnResetAll.FlatStyle = FlatStyle.Flat;


            btnSaveAndClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnClearAll.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnResetAll.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];

            btnSaveAndClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnResetAll.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnClearAll.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];

            btnSaveAndClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnResetAll.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnClearAll.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];


            btnSaveAndClose.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnClearAll.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnResetAll.BackColor = AhoyColors.CurrentColorTheme()[7];


            btnSaveAndClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnResetAll.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnClearAll.ForeColor = AhoyColors.CurrentColorTheme()[8];

            SetupHelpPictureBox();
        }

        private void SetupHelpPictureBox()
        {
            picHelp.BackColor = Color.Transparent;
            picHelp.BackgroundImage = ButtonBitmaps.HelpButtonHighlight();
            picHelp.BackgroundImageLayout = ImageLayout.Center;
        }
        #endregion

        // Update divider strings
        private void tbDeviderA_TextChanged(object sender, EventArgs e)
        {
            if (!SuppressTextChanged)
            {
                DividerAString = tbDeviderA.Text;
                UpdateLabels();
            }
        }

        private void tbDeviderB_TextChanged(object sender, EventArgs e)
        {
            if (!SuppressTextChanged)
            {
                DividerBString = tbDeviderB.Text;
                UpdateLabels();
            }
        }

        private void tbDeviderC_TextChanged(object sender, EventArgs e)
        {
            if (!SuppressTextChanged)
            {
                DividerCString = tbDeviderC.Text;
                UpdateLabels();
            }
        }

        private void tbDeviderD_TextChanged(object sender, EventArgs e)
        {
            if (!SuppressTextChanged)
            {
                DividerDString = tbDeviderD.Text;
                UpdateLabels();
            }
        }

        private bool HasContentChanged()
        {
            if (InitialDividerAString != tbDeviderA.Text || InitialDividerBString != tbDeviderB.Text ||
                InitialDividerCString != tbDeviderC.Text || InitialDividerDString != tbDeviderD.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResolveSaveButton()
        {
            if (HasContentChanged())
            {
                btnSaveAndClose.ForeColor = AhoyColors.CurrentColorTheme()[6];
            }
            else
            {
                btnSaveAndClose.ForeColor = AhoyColors.CurrentColorTheme()[8];
            }
        }

        private void UpdateLabels()
        {
            lbDeviderShortcutA.Text = ShortcutA + TextTransform.CalculateSpacesFromStringToGivenLength(ShortcutA, 12) + "(" + TextTransform.GetNewLineCount(tbDeviderA) + " lines/" + tbDeviderA.Text.Length + " chars)";
            lbDeviderShortcutB.Text = ShortcutB + TextTransform.CalculateSpacesFromStringToGivenLength(ShortcutB, 12) + "(" + TextTransform.GetNewLineCount(tbDeviderB) + " lines/" + tbDeviderB.Text.Length + " chars)";
            lbDeviderShortcutC.Text = ShortcutC + TextTransform.CalculateSpacesFromStringToGivenLength(ShortcutC, 12) + "(" + TextTransform.GetNewLineCount(tbDeviderC) + " lines/" + tbDeviderC.Text.Length + " chars)";
            lbDeviderShortcutD.Text = ShortcutD + TextTransform.CalculateSpacesFromStringToGivenLength(ShortcutD, 12) + "(" + TextTransform.GetNewLineCount(tbDeviderD) + " lines/" + tbDeviderD.Text.Length + " chars)";
            ResolveSaveButton();
            MainForm.KeepAwake();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            CloseForm();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            SubmitChangesAndClose();
        }

        // Submit values to main form and close form
        private void SubmitChangesAndClose()
        {
            MainForm.DividerAString = DividerAString;
            MainForm.DividerBString = DividerBString;
            MainForm.DividerCString = DividerCString;
            MainForm.DividerDString = DividerDString;
            MainForm.KeepAwake();
            MainForm.SetPoolHasChanges();
            this.Close();
        }

        // Close form without saving
        private void CloseForm()
        {
            this.Close();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            AhoyAnswerMessageBox msg = new AhoyAnswerMessageBox("Reset Paragraph Dividers?", MessageText.ResetCaption(), false, false);
            msg.ShowDialog();
            if (msg.Accepted)
            {
                SelectedPreset = 0;
                tbDeviderA.Text = "";
                tbDeviderB.Text = "";
                tbDeviderC.Text = "";
                tbDeviderD.Text = "";
                MainForm.DividerAString = "";
                MainForm.DividerBString = "";
                MainForm.DividerCString = "";
                MainForm.DividerDString = "";
                LoadDividerStringsFromMainForm();
                UpdateTextBoxes();
                tbDeviderA.Focus();
            }
            else
            {
                tbDeviderA.Focus();
            }
        }


        private void btnClearAll_Click(object sender, EventArgs e)
        {
            SuppressTextChanged = true;
            tbDeviderA.Text = "";
            tbDeviderB.Text = "";
            tbDeviderC.Text = "";
            tbDeviderD.Text = "";
            SuppressTextChanged = false;
        }

        #region Context Menu Events
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

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicContextMenuOperations.ExecuteContextMenuAction(sender, 3);
        }
        #endregion

        private void tbDeviderA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                tbDeviderA.Undo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                tbDeviderA.Redo();
                e.Handled = true;
            }
        }

        private void tbDeviderB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                tbDeviderB.Undo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                tbDeviderB.Redo();
                e.Handled = true;
            }
        }

        private void tbDeviderC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                tbDeviderC.Undo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                tbDeviderC.Redo();
                e.Handled = true;
            }
        }

        private void tbDeviderD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                tbDeviderD.Undo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                tbDeviderD.Redo();
                e.Handled = true;
            }
        }


        private void CountPresetIndexUp()
        {
            MainForm.KeepAwake();
            if (SelectedPreset < PresetList.Count - 1)
            {
                SelectedPreset += 1;
            }
            else
            {
                SelectedPreset = 0;
            }
        }

        private void CountPresetIndexDown()
        {
            MainForm.KeepAwake();
            if (SelectedPreset > 0)
            {
                SelectedPreset -= 1;
            }
            else
            {
                SelectedPreset = PresetList.Count - 1;
            }
        }

        private string GetCurrentPreset()
        {
            MainForm.KeepAwake();
            return PresetList[SelectedPreset].Replace("\r\n", "\n");
        }


        private void btnUpA_Click(object sender, EventArgs e)
        {
            CountPresetIndexUp();
            tbDeviderA.Text = GetCurrentPreset();
        }

        private void btnDownA_Click(object sender, EventArgs e)
        {
            CountPresetIndexDown();
            tbDeviderA.Text = GetCurrentPreset();
        }

        private void btnUpB_Click(object sender, EventArgs e)
        {
            CountPresetIndexUp();
            tbDeviderB.Text = GetCurrentPreset();
        }

        private void btnDownB_Click(object sender, EventArgs e)
        {
            CountPresetIndexDown();
            tbDeviderB.Text = GetCurrentPreset();
        }

        private void btnUpC_Click(object sender, EventArgs e)
        {
            CountPresetIndexUp();
            tbDeviderC.Text = GetCurrentPreset();
        }

        private void btnDownC_Click(object sender, EventArgs e)
        {
            CountPresetIndexDown();
            tbDeviderC.Text = GetCurrentPreset();
        }

        private void btnUpD_Click(object sender, EventArgs e)
        {
            CountPresetIndexUp();
            tbDeviderD.Text = GetCurrentPreset();
        }

        private void btnDownD_Click(object sender, EventArgs e)
        {
            CountPresetIndexDown();
            tbDeviderD.Text = GetCurrentPreset();
        }

        #region COPY/PASTE BUTTONS
        private void lbCopyA_Click(object sender, EventArgs e)
        {
            tbDeviderA.SelectAll();
            tbDeviderA.CopyUnformatted();
        }

        private void lbPasteA_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbDeviderA.SetText(Clipboard.GetText(), false);
            }
        }

        private void lbClearA_Click(object sender, EventArgs e)
        {
            tbDeviderA.Text = "";
            tbDeviderA.Focus();
        }


        private void lbCopyB_Click(object sender, EventArgs e)
        {
            tbDeviderB.SelectAll();
            tbDeviderB.CopyUnformatted();
        }
        private void lbPasteB_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbDeviderB.SetText(Clipboard.GetText(), false);
            }
        }
        private void lbClearB_Click(object sender, EventArgs e)
        {
            tbDeviderB.Text = "";
            tbDeviderB.Focus();
        }

        private void lbCopyC_Click(object sender, EventArgs e)
        {
            tbDeviderC.SelectAll();
            tbDeviderC.CopyUnformatted();
        }
        private void lbClearC_Click(object sender, EventArgs e)
        {
            tbDeviderC.Text = "";
            tbDeviderC.Focus();
        }
        private void lbPasteC_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbDeviderC.SetText(Clipboard.GetText(), false);
            }
        }

        private void lbCopyD_Click(object sender, EventArgs e)
        {
            tbDeviderD.SelectAll();
            tbDeviderD.CopyUnformatted();
        }
        private void lbPasteD_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbDeviderD.SetText(Clipboard.GetText(), false);
            }
        }
        private void lbClearD_Click(object sender, EventArgs e)
        {
            tbDeviderD.Text = "";
            tbDeviderD.Focus();
        }


        #endregion

        #region Insert replacement tags
        private void lbCopyTimeTag_Click()
        {
            if (tbDeviderA.Focused)
            {
                InsertTimestampTag(tbDeviderA);
            }
            else if (tbDeviderB.Focused)
            {
                InsertTimestampTag(tbDeviderB);
            }
            else if (tbDeviderC.Focused)
            {
                InsertTimestampTag(tbDeviderC);
            }
            else if (tbDeviderD.Focused)
            {
                InsertTimestampTag(tbDeviderD);
            }
        }

        private void lbCopySelectionTag_Click()
        {
            if (tbDeviderA.Focused)
            {
                InsertSelectionTag(tbDeviderA);
            }
            else if (tbDeviderB.Focused)
            {
                InsertSelectionTag(tbDeviderB);
            }
            else if (tbDeviderC.Focused)
            {
                InsertSelectionTag(tbDeviderC);
            }
            else if (tbDeviderD.Focused)
            {
                InsertSelectionTag(tbDeviderD);
            }
        }

        private void lbTimestampTag_Click(object sender, EventArgs e)
        {
            lbCopyTimeTag_Click();
        }

        private void lbSelectionTag_Click(object sender, EventArgs e)
        {
            lbCopySelectionTag_Click();
        }
        private void InsertTimestampTag(HistoryTextBox tb)
        {
            MainForm.KeepAwake();
            tb.InsertText(TimeStampReplaceString);
        }
        private void InsertSelectionTag(HistoryTextBox tb)
        {
            MainForm.KeepAwake();
            tb.InsertText(SelectionReplaceString);
        }
        #endregion


        private void lbResetA_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (PresetList != null && PresetList.Count > 0)
            {
                DividerAString = PresetList[0].Replace("\r\n", "\n");
                SuppressTextChanged = true;
                tbDeviderA.Text = DividerAString;
                SuppressTextChanged = false;
                UpdateLabels();
            }
        }

        private void lbResetB_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (PresetList != null && PresetList.Count > 0)
            {
                DividerBString = PresetList[1].Replace("\r\n", "\n");
                SuppressTextChanged = true;
                tbDeviderB.Text = DividerBString;
                SuppressTextChanged = false;
                UpdateLabels();
            }
        }

        private void lbResetC_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (PresetList != null && PresetList.Count > 0)
            {
                DividerCString = PresetList[2].Replace("\r\n", "\n");
                SuppressTextChanged = true;
                tbDeviderC.Text = DividerCString;
                SuppressTextChanged = false;
                UpdateLabels();
            }
        }

        private void lbResetD_Click(object sender, EventArgs e)
        {
            MainForm.KeepAwake();
            if (PresetList != null && PresetList.Count > 0)
            {
                DividerDString = PresetList[3].Replace("\r\n", "\n");
                SuppressTextChanged = true;
                tbDeviderD.Text = DividerDString;
                SuppressTextChanged = false;
                UpdateLabels();
            }
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            AhoySearchMessageBox msg = new AhoySearchMessageBox(IO.ParagraphDividersHelpTextReader(), MessageText.InformationCaption(), false, true, this.Opacity);
            msg.ShowDialog();
        }
    }

    class HoverLabel : Label
    {
        private Timer hoverTimer;
        private int timerCounter;
        public HoverLabel()
        {
            // Timer
            hoverTimer = new Timer();
            hoverTimer.Interval = 1000;
            hoverTimer.Tick += HoverTimer_Tick;
            hoverTimer.Enabled = false;

            // Initial values
            this.ForeColor = AhoyColors.CurrentColorTheme()[2];
            this.MouseEnter += HoverLabel_MouseEnter;
            this.MouseLeave += HoverLabel_MouseLeave;
        }

        private void HoverLabel_MouseLeave(object sender, EventArgs e)
        {
            if (hoverTimer.Enabled)
            {
                hoverTimer.Enabled = false;
            }
            this.ForeColor = AhoyColors.CurrentColorTheme()[2];
        }

        private void HoverLabel_MouseEnter(object sender, EventArgs e)
        {
            this.ForeColor = AhoyColors.CurrentColorTheme()[6];
            if (!hoverTimer.Enabled)
            {
                timerCounter = 0;
                hoverTimer.Enabled = true;
            }
        }

        // Ensure the label will turn off highlight after 5 sec
        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            timerCounter += 1;
            if (timerCounter >= 5)
            {
                hoverTimer.Enabled = false;
                if (this.ForeColor == AhoyColors.CurrentColorTheme()[6])
                {
                    this.ForeColor = AhoyColors.CurrentColorTheme()[2];
                }
            }
        }
    }
}
