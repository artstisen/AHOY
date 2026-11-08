using System;
using System.Windows.Forms;
using System.Drawing;


namespace AhoyBrowser
{
    public partial class StylePicker : Form
    {
        private int OpacityIndex { get; set; }
        private int OriginalColorTheme { get; set; }
        private int OriginalTextureSelection { get; set; }
        private int BitmapPreviewIndex { get; set; }
        private double OriginalFormOpacity { get; set; }
        private bool ChangesSubmitted { get; set; }
        public static bool PrismEffect { get; set; }
        private bool OverrideThemePrism { get; set; }


        public StylePicker()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += StylePicker_KeyDown;
            ChangesSubmitted = false;
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            BitmapPreviewIndex = 0;
            // Prism effect
            PrismEffect = MainForm.PrismEffect;
            OverrideThemePrism = true;

            // Prepare previews and save original values
            AhoyColors.PreparePreview();
            MainForm.PrepareTexturePreview();
            OriginalColorTheme = AhoyColors.PreviewColorTheme;
            OriginalTextureSelection = MainForm.SelectedTexturePreview;
            OriginalFormOpacity = MainForm.GetFormOpacity();
            BitmapPreviewIndex = ButtonBitmaps.UserSelectedBitmapVersion;

            // Setup context menu
            tbThemeTitle.ContextMenuStrip = contextMenuBasic;

            // Make name field readonly
            tbThemeTitle.ReadOnly = true;

            // Set labels to transparent
            lbCurrentTheme.BackColor = Color.Transparent;
            lbCurrentTexture.BackColor = Color.Transparent;
            lbOpacity.BackColor = Color.Transparent;
            lbButtonBitmaps.BackColor = Color.Transparent;
            lbButtonIcons.BackColor = Color.Transparent;
            lbPinnedColors.BackColor = Color.Transparent;
            lbInterfaceOptions.BackColor = Color.Transparent;
            lbCopyPreset.BackColor = Color.Transparent;
            lbPastePreset.BackColor = Color.Transparent;

            lbTestContent.Font = Design.TypermaxXSSmallFont();
            lbTestList.Font = Design.TypermaxXSSmallFont();
            lbTestContent.TextAlign = ContentAlignment.TopLeft;
            lbTestList.TextAlign = ContentAlignment.TopLeft;
            tbThemeTitle.Font = Design.ContentFieldFont();
            lbTestContent.Text = IO.StyleEditorTextReader();
            lbTestList.Text = IO.StyleEditorListFieldTextReader();
            pnlInterfaceOptions.Visible = false;
            ResolveOpacityIndex();
            UpdateColorTheme();
            UpdateTexture();
            ResolveFormOpacity();
        }

        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void StylePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (pnlInterfaceOptions.Visible)
                {
                    pnlInterfaceOptions.Visible = false;
                }
                else
                {
                    btnClose_Click(this, null);
                    e.Handled = true;
                }
            }
        }

        private void UpdateColorTheme()
        {
            Reaction();
            SuspendLayout();

            // Interface panel
            pnlInterfaceOptions.BackColor = AhoyColors.CurrentColorThemePreview()[0];
            lbInterfaceOptions.ForeColor = ForeColor = AhoyColors.CurrentColorThemePreview()[2];

            // Tooltip:
            toolTip1.BackColor = AhoyColors.CurrentColorThemePreview()[29];
            toolTip1.ForeColor = AhoyColors.CurrentColorThemePreview()[30];


            this.BackColor = AhoyColors.CurrentColorThemePreview()[0];
            lbCurrentTheme.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbCurrentTheme.Font = Design.ContentFieldFont();
            lbCurrentTexture.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbOpacity.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbButtonBitmaps.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbButtonIcons.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbPinnedColors.ForeColor = AhoyColors.CurrentColorThemePreview()[2];
            lbCopyPreset.ForeColor = AhoyColors.CurrentColorThemePreview()[6];
            lbPastePreset.ForeColor = AhoyColors.CurrentColorThemePreview()[6];

            tbThemeTitle.BackColor = AhoyColors.CurrentColorThemePreview()[29];
            lbTestContent.BackColor = AhoyColors.CurrentColorThemePreview()[29];
            lbPrismEffect.BackColor = AhoyColors.CurrentColorThemePreview()[29];

            tbThemeTitle.ForeColor = AhoyColors.CurrentColorThemePreview()[30];
            lbTestContent.ForeColor = AhoyColors.CurrentColorThemePreview()[30];
            lbPrismEffect.ForeColor = AhoyColors.CurrentColorThemePreview()[30];

            // Divider line
            pnlDivider.BackColor = AhoyColors.CurrentColorThemePreview()[7];

            // Pinned field
            lbTestList.BackColor = AhoyColors.CurrentColorThemePreview()[1];
            lbTestList.ForeColor = AhoyColors.CurrentColorThemePreview()[4];

            btnClose.FlatStyle = FlatStyle.Flat;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnPreviousTheme.FlatStyle = FlatStyle.Flat;
            btnNextTheme.FlatStyle = FlatStyle.Flat;
            btnPreviousTexture.FlatStyle = FlatStyle.Flat;
            btnNextTexture.FlatStyle = FlatStyle.Flat;
            btnUseSelection.FlatStyle = FlatStyle.Flat;
            btnPreviousOpacity.FlatStyle = FlatStyle.Flat;
            btnNextOpacity.FlatStyle = FlatStyle.Flat;
            btnNextBitmap.FlatStyle = FlatStyle.Flat;
            btnPreviousBitmap.FlatStyle = FlatStyle.Flat;

            btnLabelColor.FlatStyle = FlatStyle.Flat;
            btnResetLabelColor.FlatStyle = FlatStyle.Flat;
            btnFieldColors.FlatStyle = FlatStyle.Flat;
            btnResetFieldColors.FlatStyle = FlatStyle.Flat;
            btnListColors.FlatStyle = FlatStyle.Flat;
            btnResetListColors.FlatStyle = FlatStyle.Flat;
            btnClearPinnedPresetColors.FlatStyle = FlatStyle.Flat;
            btnWindowColor.FlatStyle = FlatStyle.Flat;
            btnResetWindowColor.FlatStyle = FlatStyle.Flat;
            btnImportTheme.FlatStyle = FlatStyle.Flat;
            btnExportTheme.FlatStyle = FlatStyle.Flat;
            btnHighLightColor.FlatStyle = FlatStyle.Flat;
            btnResetHighLightColor.FlatStyle = FlatStyle.Flat;
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnResetOpacity.FlatStyle = FlatStyle.Flat;
            btnResetTexture.FlatStyle = FlatStyle.Flat;
            btnResetIconStyle.FlatStyle = FlatStyle.Flat;
            btnSetName.FlatStyle = FlatStyle.Flat;
            btnResetName.FlatStyle = FlatStyle.Flat;

            btnInterfaceOptions.FlatStyle = FlatStyle.Flat;
            btnResetInterfaceOptions.FlatStyle = FlatStyle.Flat;
            btnInterfaceBackground.FlatStyle = FlatStyle.Flat;
            btnInterfaceText.FlatStyle = FlatStyle.Flat;
            btnInterfaceAlt1.FlatStyle = FlatStyle.Flat;
            btnInterfaceAlt2.FlatStyle = FlatStyle.Flat;
            btnInterfaceBorder.FlatStyle = FlatStyle.Flat;


            btnPinnedBack1.FlatStyle = FlatStyle.Flat;
            btnPinnedBack2.FlatStyle = FlatStyle.Flat;
            btnPinnedBack3.FlatStyle = FlatStyle.Flat;
            btnPinnedBack4.FlatStyle = FlatStyle.Flat;
            btnPinnedBack5.FlatStyle = FlatStyle.Flat;
            btnPinnedBack6.FlatStyle = FlatStyle.Flat;
            btnPinnedBack7.FlatStyle = FlatStyle.Flat;
            btnPinnedBack8.FlatStyle = FlatStyle.Flat;

            btnClose.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnReset.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnPreviousTheme.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnNextTheme.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnPreviousTexture.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnNextTexture.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnUseSelection.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnPreviousOpacity.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnNextOpacity.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnNextBitmap.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnPreviousBitmap.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];

            btnLabelColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetLabelColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnFieldColors.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetFieldColors.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnListColors.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetListColors.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnClearPinnedPresetColors.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnWindowColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetWindowColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnImportTheme.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnExportTheme.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnHighLightColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[6];
            btnResetHighLightColor.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnUndo.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetOpacity.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetTexture.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetIconStyle.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnSetName.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetName.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];

            btnInterfaceOptions.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnResetInterfaceOptions.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnInterfaceBackground.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnInterfaceText.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnInterfaceAlt1.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnInterfaceAlt2.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];
            btnInterfaceBorder.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[9];


            btnPinnedBack1.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[12];
            btnPinnedBack2.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[13];
            btnPinnedBack3.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[14];
            btnPinnedBack4.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[15];
            btnPinnedBack5.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[16];
            btnPinnedBack6.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[17];
            btnPinnedBack7.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[18];
            btnPinnedBack8.FlatAppearance.BorderColor = AhoyColors.CurrentColorThemePreview()[19];


            btnClose.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnReset.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnPreviousTheme.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnNextTheme.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnPreviousTexture.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnNextTexture.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnUseSelection.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnPreviousOpacity.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnNextOpacity.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnNextBitmap.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnPreviousBitmap.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];

            btnLabelColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetLabelColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnFieldColors.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetFieldColors.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnListColors.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetListColors.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnClearPinnedPresetColors.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnWindowColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetWindowColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnImportTheme.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnExportTheme.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnHighLightColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[6]; // Highlight
            btnResetHighLightColor.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnUndo.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetOpacity.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetTexture.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetIconStyle.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnSetName.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetName.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];

            btnInterfaceOptions.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnResetInterfaceOptions.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnInterfaceBackground.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnInterfaceText.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];
            btnInterfaceAlt1.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[7]; // Display the background color here
            btnInterfaceAlt2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[7]; // Display the background color here
            btnInterfaceBorder.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[10];

            btnPinnedBack1.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[12];
            btnPinnedBack2.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[13];
            btnPinnedBack3.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[14];
            btnPinnedBack4.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[15];
            btnPinnedBack5.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[16];
            btnPinnedBack6.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[17];
            btnPinnedBack7.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[18];
            btnPinnedBack8.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorThemePreview()[19];



            btnClose.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnReset.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnPreviousTheme.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnNextTheme.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnPreviousTexture.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnNextTexture.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnUseSelection.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnPreviousOpacity.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnNextOpacity.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnNextBitmap.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnPreviousBitmap.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];

            btnLabelColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetLabelColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnFieldColors.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetFieldColors.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnListColors.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetListColors.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnClearPinnedPresetColors.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnWindowColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetWindowColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnImportTheme.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnExportTheme.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnHighLightColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[6]; // Highlight
            btnResetHighLightColor.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnUndo.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetOpacity.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetTexture.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetIconStyle.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnSetName.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetName.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];

            btnInterfaceOptions.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnResetInterfaceOptions.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnInterfaceBackground.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnInterfaceText.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnInterfaceAlt1.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnInterfaceAlt2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];
            btnInterfaceBorder.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[11];

            btnPinnedBack1.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[12];
            btnPinnedBack2.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[13];
            btnPinnedBack3.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[14];
            btnPinnedBack4.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[15];
            btnPinnedBack5.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[16];
            btnPinnedBack6.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[17];
            btnPinnedBack7.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[18];
            btnPinnedBack8.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorThemePreview()[19];




            btnClose.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnReset.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnPreviousTheme.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnNextTheme.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnPreviousTexture.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnNextTexture.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnUseSelection.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnPreviousOpacity.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnNextOpacity.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnNextBitmap.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnPreviousBitmap.BackColor = AhoyColors.CurrentColorThemePreview()[7];

            btnLabelColor.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetLabelColor.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnFieldColors.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetFieldColors.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnListColors.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetListColors.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnClearPinnedPresetColors.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnWindowColor.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetWindowColor.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnImportTheme.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnExportTheme.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnHighLightColor.BackColor = AhoyColors.CurrentColorThemePreview()[6]; // Highlight
            btnResetHighLightColor.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnUndo.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetOpacity.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetTexture.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetIconStyle.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnSetName.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetName.BackColor = AhoyColors.CurrentColorThemePreview()[7];

            btnInterfaceOptions.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnResetInterfaceOptions.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnInterfaceBackground.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnInterfaceText.BackColor = AhoyColors.CurrentColorThemePreview()[7];
            btnInterfaceAlt1.BackColor = AhoyColors.CurrentColorThemePreview()[10]; // Display the mouse over color
            btnInterfaceAlt2.BackColor = AhoyColors.CurrentColorThemePreview()[11]; // Display the mouse down color
            btnInterfaceBorder.BackColor = AhoyColors.CurrentColorThemePreview()[7];

            btnPinnedBack1.BackColor = AhoyColors.CurrentColorThemePreview()[12];
            btnPinnedBack2.BackColor = AhoyColors.CurrentColorThemePreview()[13];
            btnPinnedBack3.BackColor = AhoyColors.CurrentColorThemePreview()[14];
            btnPinnedBack4.BackColor = AhoyColors.CurrentColorThemePreview()[15];
            btnPinnedBack5.BackColor = AhoyColors.CurrentColorThemePreview()[16];
            btnPinnedBack6.BackColor = AhoyColors.CurrentColorThemePreview()[17];
            btnPinnedBack7.BackColor = AhoyColors.CurrentColorThemePreview()[18];
            btnPinnedBack8.BackColor = AhoyColors.CurrentColorThemePreview()[19];

            btnClose.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnReset.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnPreviousTheme.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnNextTheme.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnPreviousTexture.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnNextTexture.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnUseSelection.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnPreviousOpacity.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnNextOpacity.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnNextBitmap.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnPreviousBitmap.ForeColor = AhoyColors.CurrentColorThemePreview()[8];

            btnLabelColor.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetLabelColor.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnFieldColors.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetFieldColors.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnListColors.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetListColors.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnClearPinnedPresetColors.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnWindowColor.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetWindowColor.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnImportTheme.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnExportTheme.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnHighLightColor.ForeColor = AhoyColors.CurrentColorThemePreview()[5]; // Highlight text
            btnResetHighLightColor.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnUndo.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetOpacity.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetTexture.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetIconStyle.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnSetName.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetName.ForeColor = AhoyColors.CurrentColorThemePreview()[8];

            btnInterfaceOptions.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnResetInterfaceOptions.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnInterfaceBackground.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnInterfaceText.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnInterfaceAlt1.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnInterfaceAlt2.ForeColor = AhoyColors.CurrentColorThemePreview()[8];
            btnInterfaceBorder.ForeColor = AhoyColors.CurrentColorThemePreview()[8];

            btnPinnedBack1.ForeColor = AhoyColors.CurrentColorThemePreview()[20];
            btnPinnedBack2.ForeColor = AhoyColors.CurrentColorThemePreview()[21];
            btnPinnedBack3.ForeColor = AhoyColors.CurrentColorThemePreview()[22];
            btnPinnedBack4.ForeColor = AhoyColors.CurrentColorThemePreview()[23];
            btnPinnedBack5.ForeColor = AhoyColors.CurrentColorThemePreview()[24];
            btnPinnedBack6.ForeColor = AhoyColors.CurrentColorThemePreview()[25];
            btnPinnedBack7.ForeColor = AhoyColors.CurrentColorThemePreview()[26];
            btnPinnedBack8.ForeColor = AhoyColors.CurrentColorThemePreview()[27];

            if (AhoyColors.CustomColorThemeTitle != null && AhoyColors.CustomColorThemeTitle.Length > 0
                && (AhoyColors.HasCustomColorsPreview()))
            {
                SetCustomColorThemeTitle(AhoyColors.CustomColorThemeTitle);
            }
            else
            {
                SetColorThemeTitle(AhoyColors.GetThemeTitleByIndex(AhoyColors.PreviewColorTheme));
            }

            // Set button background images
            // Theme
            btnNextTheme.Text = "";
            btnNextTheme.BackgroundImage = ButtonBitmaps.Right(BitmapPreviewIndex);
            btnNextTheme.BackgroundImageLayout = ImageLayout.Center;

            btnPreviousTheme.Text = "";
            btnPreviousTheme.BackgroundImage = ButtonBitmaps.Left(BitmapPreviewIndex);
            btnPreviousTheme.BackgroundImageLayout = ImageLayout.Center;

            // Texture
            btnNextTexture.Text = "";
            btnNextTexture.BackgroundImage = ButtonBitmaps.Right(BitmapPreviewIndex);
            btnNextTexture.BackgroundImageLayout = ImageLayout.Center;

            btnPreviousTexture.Text = "";
            btnPreviousTexture.BackgroundImage = ButtonBitmaps.Left(BitmapPreviewIndex);
            btnPreviousTexture.BackgroundImageLayout = ImageLayout.Center;

            // Opacity
            btnNextOpacity.Text = "";
            btnNextOpacity.BackgroundImage = ButtonBitmaps.Right(BitmapPreviewIndex);
            btnNextOpacity.BackgroundImageLayout = ImageLayout.Center;

            btnPreviousOpacity.Text = "";
            btnPreviousOpacity.BackgroundImage = ButtonBitmaps.Left(BitmapPreviewIndex);
            btnPreviousOpacity.BackgroundImageLayout = ImageLayout.Center;

            // Bitmaps / Icon collection
            btnNextBitmap.Text = "";
            btnNextBitmap.BackgroundImage = ButtonBitmaps.Right(BitmapPreviewIndex);
            btnNextBitmap.BackgroundImageLayout = ImageLayout.Center;

            btnPreviousBitmap.Text = "";
            btnPreviousBitmap.BackgroundImage = ButtonBitmaps.Left(BitmapPreviewIndex);
            btnPreviousBitmap.BackgroundImageLayout = ImageLayout.Center;

            // Reset label color
            btnResetLabelColor.Text = "";
            btnResetLabelColor.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetLabelColor.BackgroundImageLayout = ImageLayout.Center;

            // Reset field color
            btnResetFieldColors.Text = "";
            btnResetFieldColors.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetFieldColors.BackgroundImageLayout = ImageLayout.Center;

            // Reset list color
            btnResetListColors.Text = "";
            btnResetListColors.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetListColors.BackgroundImageLayout = ImageLayout.Center;

            // Reset pinned preset colors
            btnClearPinnedPresetColors.Text = "";
            btnClearPinnedPresetColors.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnClearPinnedPresetColors.BackgroundImageLayout = ImageLayout.Center;

            // Reset window color
            btnResetWindowColor.Text = "";
            btnResetWindowColor.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetWindowColor.BackgroundImageLayout = ImageLayout.Center;

            // Reset highlight color
            btnResetHighLightColor.Text = "";
            btnResetHighLightColor.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetHighLightColor.BackgroundImageLayout = ImageLayout.Center;


            // Reset interface options
            btnResetInterfaceOptions.Text = "";
            btnResetInterfaceOptions.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetInterfaceOptions.BackgroundImageLayout = ImageLayout.Center;


            // Reset opacity
            btnResetOpacity.Text = "";
            btnResetOpacity.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetOpacity.BackgroundImageLayout = ImageLayout.Center;

            // Reset texture
            btnResetTexture.Text = "";
            btnResetTexture.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetTexture.BackgroundImageLayout = ImageLayout.Center;

            // Reset Icon style
            btnResetIconStyle.Text = "";
            btnResetIconStyle.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetIconStyle.BackgroundImageLayout = ImageLayout.Center;

            // Reset Name
            btnResetName.Text = "";
            btnResetName.BackgroundImage = ButtonBitmaps.Clear(BitmapPreviewIndex);
            btnResetName.BackgroundImageLayout = ImageLayout.Center;


            ResumeLayout();
            ResolveBitmapLabel();
            UpdateCurrentThemeLabel();
            UpdateTexture();
            SetDisplayTextPreview();
            if (!OverrideThemePrism) // Only get preview prism if not an import
            {
                PrismEffect = AhoyColors.GetCurrentThemePrismEffect(AhoyColors.PreviewColorTheme); // Get prism state from theme
            }
            else
            {
                OverrideThemePrism = false;
            }
            RespondToCurrentPrismSetting();
        }



        // The small preview text content window
        private void SetDisplayTextPreview()
        {
            if (GetQuickGuideCycle() == 1)
            {
                lbTestContent.Font = Design.StyleEditorXXSSmallFont();
                lbTestContent.Text = IO.AhoyASCIILogo1TextReader(4, 5, 0);
            }
            else if (GetQuickGuideCycle() == 2)
            {
                lbTestContent.Font = Design.StyleEditorXSSmallFont();
                lbTestContent.Text = IO.AhoyASCIILogo1TextReader(2, 6, 1);
            }
            else
            {
                lbTestContent.Font = Design.StyleEditorXSSmallFont();
                lbTestContent.Text = IO.StyleEditorTextReader();
            }
        }

        // Determine Home Screen by theme
        private int GetQuickGuideCycle()
        {
            if (AhoyColors.PreviewColorTheme == 3 || AhoyColors.PreviewColorTheme == 5 || AhoyColors.PreviewColorTheme == 8 || AhoyColors.PreviewColorTheme == 14)
            {
                return 1;
            }
            else if (AhoyColors.PreviewColorTheme == 13)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }


        private const string tempThemeLabelText = "ColorTheme";
        private void UpdateCurrentThemeLabel()
        {
            if (AhoyColors.HasCustomColorsPreview())
            {
                lbCurrentTheme.Text = tempThemeLabelText + " (" + (AhoyColors.PreviewColorTheme + 1).ToString() + "/" + (AhoyColors.ThemeCount + 1).ToString() + ")*";
            }
            else
            {
                lbCurrentTheme.Text = tempThemeLabelText + " (" + (AhoyColors.PreviewColorTheme + 1).ToString() + "/" + (AhoyColors.ThemeCount + 1).ToString() + ")";
            }
        }

        private void SetColorThemeTitle(string text)
        {
            if (text.Length > 0)
            {
                if (AhoyColors.HasCustomColorsPreview())
                {
                    tbThemeTitle.Text = "Modified: " + text;
                }
                else
                {
                    tbThemeTitle.Text = "Selected: " + text;
                }
            }
        }

        private void SetCustomColorThemeTitle(string text)
        {
            if (text.Length > 0)
            {
                if (AhoyColors.HasCustomColorsPreview())
                {
                    tbThemeTitle.Text = text;
                }
            }
        }

        private void SetTextureLabel()
        {
            lbCurrentTexture.Text = "Texture: " + MainForm.SelectedTexturePreview.ToString("0") + "/30";
        }

        private void SetOpacityLabel()
        {
            lbOpacity.Text = "Opacity: " + GetOpacityAsString();
        }

        private string GetOpacityAsString()
        {
            switch (OpacityIndex)
            {
                case 0:
                    return "100%";
                case 1:
                    return "95%";
                case 2:
                    return "90%";
                case 3:
                    return "85%";
                case 4:
                    return "75%";
                case 5:
                    return "65%";
                case 6:
                    return "50%";
                default:
                    return "100%";
            }
        }

        private void UpdateTexture()
        {
            Reaction();
            if (MainForm.SelectedTexturePreview > 0)
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexturePreview);
            }
            else
            {
                this.BackgroundImage = null;
            }

            SetTextureLabel();
        }

        public static void NextColorTheme()
        {
            if (AhoyColors.PreviewColorTheme < AhoyColors.ThemeCount)
            {
                AhoyColors.PreviewColorTheme += 1;
            }
            else
            {
                AhoyColors.PreviewColorTheme = 0; // Jump to start
            }
        }

        public static void PreviousColorTheme()
        {
            if (AhoyColors.PreviewColorTheme > 0)
            {
                AhoyColors.PreviewColorTheme -= 1;
            }
            else
            {
                AhoyColors.PreviewColorTheme = AhoyColors.ThemeCount; // Jump to end
            }
        }

        public static void NextTexture()
        {
            if (MainForm.SelectedTexturePreview < 30)
            {
                MainForm.SelectedTexturePreview += 1;
            }
        }

        public static void PreviousTexture()
        {
            if (MainForm.SelectedTexturePreview > 0)
            {
                MainForm.SelectedTexturePreview -= 1;
            }
        }



        private void btnPreviousTheme_Click(object sender, EventArgs e)
        {
            BitmapPreviewIndex = 0;
            PreviousColorTheme();
            UpdateColorTheme();
            ResetSubmissionVariable();
        }

        private void btnNextTheme_Click(object sender, EventArgs e)
        {
            BitmapPreviewIndex = 0;
            NextColorTheme();
            UpdateColorTheme();
            ResetSubmissionVariable();
        }

        private void btnPreviousTexture_Click(object sender, EventArgs e)
        {
            PreviousTexture();
            UpdateTexture();
            ResetSubmissionVariable();
        }

        private void btnNextTexture_Click(object sender, EventArgs e)
        {
            NextTexture();
            UpdateTexture();
            ResetSubmissionVariable();
        }



        private void btnPreviousOpacity_Click(object sender, EventArgs e)
        {
            if (OpacityIndex == 0)
            {
                OpacityIndex = 6;
            }
            else if (OpacityIndex > 0)
            {
                OpacityIndex -= 1;
            }
            ResolveFormOpacity();
            ResetSubmissionVariable();
        }

        private void btnNextOpacity_Click(object sender, EventArgs e)
        {
            if (OpacityIndex == 6)
            {
                OpacityIndex = 0;
            }
            else if (OpacityIndex < 6)
            {
                OpacityIndex += 1;
            }
            ResolveFormOpacity();
            ResetSubmissionVariable();
        }


        private void ResolveFormOpacity()
        {
            Reaction();
            switch (OpacityIndex)
            {
                case 0:
                    this.Opacity = Settings.DefaultOpacity;
                    break;
                case 1:
                    this.Opacity = 0.95d;
                    break;
                case 2:
                    this.Opacity = 0.90d;
                    break;
                case 3:
                    this.Opacity = 0.85d;
                    break;
                case 4:
                    this.Opacity = 0.75d;
                    break;
                case 5:
                    this.Opacity = 0.65d;
                    break;
                case 6:
                    this.Opacity = 0.50d;
                    break;
            }

            SetOpacityLabel();
        }

        private void ResolveOpacityIndex()
        {
            if (MainForm.FormOpacity == Settings.DefaultOpacity)
            {
                OpacityIndex = 0;
            }
            else if (MainForm.FormOpacity == 0.95d)
            {
                OpacityIndex = 1;
            }
            else if (MainForm.FormOpacity == 0.90d)
            {
                OpacityIndex = 2;
            }
            else if (MainForm.FormOpacity == 0.85d)
            {
                OpacityIndex = 3;
            }
            else if (MainForm.FormOpacity == 0.75d)
            {
                OpacityIndex = 4;
            }
            else if (MainForm.FormOpacity == 0.65d)
            {
                OpacityIndex = 5;
            }
            else if (MainForm.FormOpacity == 0.50d)
            {
                OpacityIndex = 6;
            }
        }

        private int GetOpacityIndex(double opacity)
        {
            Reaction();
            switch (opacity)
            {
                case Settings.DefaultOpacity:
                    return 0;
                case 0.95d:
                    return 1;
                case 0.90d:
                    return 2;
                case 0.85d:
                    return 3;
                case 0.75d:
                    return 4;
                case 0.65d:
                    return 5;
                case 0.50d:
                    return 6;
                default:
                    return 0;
            }
        }


        // Reset to default theme
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllValues();
        }

        private void ResetAllValues()
        {
            Reaction();
            BitmapPreviewIndex = 0;
            AhoyColors.PreviewColorTheme = 0;
            ButtonBitmaps.UserSelectedBitmapVersion = Settings.DefaultButtonBitmapOverride;
            MainForm.SelectedTexturePreview = 0;
            this.Opacity = Settings.DefaultOpacity;
            OpacityIndex = 0;
            AhoyColors.ResetAllCustomPreviewColors();
            UpdateColorTheme();
            UpdateTexture();
            ResolveFormOpacity();
            ResetSubmissionVariable();
        }

        // Revert to original values
        private void RevertValues()
        {
            Reaction();
            // Save original values
            AhoyColors.ResetAllCustomPreviewColors();
            AhoyColors.PreviewColorTheme = OriginalColorTheme;
            MainForm.SelectedTexturePreview = OriginalTextureSelection;
            MainForm.FormOpacity = OriginalFormOpacity;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!ChangesSubmitted)
            {
                RevertValues();
            }

            this.Close();
        }

        // Make main form pickup the changes
        private void btnUseSelection_Click(object sender, EventArgs e)
        {
            Reaction();
            Log.StyleChanged();
            ChangesSubmitted = true;
            ButtonBitmaps.UserSelectedBitmapVersion = BitmapPreviewIndex;
            AhoyColors.SubmitCustomColorPreview();
            AhoyColors.ColorTheme = AhoyColors.PreviewColorTheme;
            MainForm.DisplayTextCycle = GetQuickGuideCycle();
            MainForm.SelectedTexture = MainForm.SelectedTexturePreview;
            MainForm.FormOpacity = this.Opacity;
            MainForm.UEOpacity = this.Opacity; // Enforce opacity to PLOP/UE
            MainForm.PrismEffect = PrismEffect;
            MainForm.StyleEditorHasChanges = true;
        }

        private void ResetSubmissionVariable()
        {
            Reaction();
            ChangesSubmitted = false;
        }

        private void Reaction()
        {
            MainForm.KeepAwake();
        }


        #region Context Menu
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

        #region Button bitmaps
        private void btnPreviousBitmap_Click(object sender, EventArgs e)
        {
            if (BitmapPreviewIndex > 0)
            {
                BitmapPreviewIndex -= 1;
            }
            else
            {
                BitmapPreviewIndex = 3;
            }

            ResolveBitmapLabel();
            UpdateColorTheme();
        }

        private void btnNextBitmap_Click(object sender, EventArgs e)
        {
            if (BitmapPreviewIndex < 3)
            {
                BitmapPreviewIndex += 1;
            }
            else
            {
                BitmapPreviewIndex = 0;
            }

            ResolveBitmapLabel();
            UpdateColorTheme();
        }

        private void ResolveBitmapLabel()
        {
            switch (BitmapPreviewIndex)
            {
                case 0:
                    lbButtonBitmaps.Text = "Default";
                    break;
                case 1:
                    lbButtonBitmaps.Text = "1/3";
                    break;
                case 2:
                    lbButtonBitmaps.Text = "2/3";
                    break;
                case 3:
                    lbButtonBitmaps.Text = "3/3";
                    break;
            }
        }
        #endregion

        // 12-19 (background)
        // 20-27 (foreground)
        private void btnPinnedBack1_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack1.BackColor, btnPinnedBack1.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(12, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(20, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack2_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack2.BackColor, btnPinnedBack2.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(13, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(21, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack3_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack3.BackColor, btnPinnedBack3.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(14, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(22, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack4_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack4.BackColor, btnPinnedBack4.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(15, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(23, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack5_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack5.BackColor, btnPinnedBack5.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(16, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(24, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack6_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack6.BackColor, btnPinnedBack6.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(17, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(25, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme(); ;
            }
        }

        private void btnPinnedBack7_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack7.BackColor, btnPinnedBack7.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(18, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(26, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnPinnedBack8_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnPinnedBack8.BackColor, btnPinnedBack8.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(19, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(27, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void lbCopyPreset_Click(object sender, EventArgs e)
        {
            CopyPinnedPresets();
        }

        private void lbPastePreset_Click(object sender, EventArgs e)
        {
            PastePinnedPresets();
        }

        // Memory banks for copy/paste
        private Color[] backgroundPresetColor = new Color[8];
        private Color[] foregroundPresetColor = new Color[8];
        private void CopyPinnedPresets()
        {
            try
            {
                // Background colors
                backgroundPresetColor[0] = AhoyColors.CurrentColorThemePreview()[12];
                backgroundPresetColor[1] = AhoyColors.CurrentColorThemePreview()[13];
                backgroundPresetColor[2] = AhoyColors.CurrentColorThemePreview()[14];
                backgroundPresetColor[3] = AhoyColors.CurrentColorThemePreview()[15];
                backgroundPresetColor[4] = AhoyColors.CurrentColorThemePreview()[16];
                backgroundPresetColor[5] = AhoyColors.CurrentColorThemePreview()[17];
                backgroundPresetColor[6] = AhoyColors.CurrentColorThemePreview()[18];
                backgroundPresetColor[7] = AhoyColors.CurrentColorThemePreview()[19];
                // Foreground colors
                foregroundPresetColor[0] = AhoyColors.CurrentColorThemePreview()[20];
                foregroundPresetColor[1] = AhoyColors.CurrentColorThemePreview()[21];
                foregroundPresetColor[2] = AhoyColors.CurrentColorThemePreview()[22];
                foregroundPresetColor[3] = AhoyColors.CurrentColorThemePreview()[23];
                foregroundPresetColor[4] = AhoyColors.CurrentColorThemePreview()[24];
                foregroundPresetColor[5] = AhoyColors.CurrentColorThemePreview()[25];
                foregroundPresetColor[6] = AhoyColors.CurrentColorThemePreview()[26];
                foregroundPresetColor[7] = AhoyColors.CurrentColorThemePreview()[27];
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Presets copied", "Copy", false);
                msg.ShowDialog();
            }
            catch
            {
                // Do nothing
            }
        }

        private void PastePinnedPresets()
        {
            try
            {
                if (backgroundPresetColor[0] != null)
                {
                    // Button 1
                    AhoyColors.SetCustomColorPreviewByIndex(12, AhoyColors.ConvertToHex(backgroundPresetColor[0]));
                    AhoyColors.SetCustomColorPreviewByIndex(20, AhoyColors.ConvertToHex(foregroundPresetColor[0]));
                    // Button 2
                    AhoyColors.SetCustomColorPreviewByIndex(13, AhoyColors.ConvertToHex(backgroundPresetColor[1]));
                    AhoyColors.SetCustomColorPreviewByIndex(21, AhoyColors.ConvertToHex(foregroundPresetColor[1]));
                    // Button 3
                    AhoyColors.SetCustomColorPreviewByIndex(14, AhoyColors.ConvertToHex(backgroundPresetColor[2]));
                    AhoyColors.SetCustomColorPreviewByIndex(22, AhoyColors.ConvertToHex(foregroundPresetColor[2]));
                    // Button 4
                    AhoyColors.SetCustomColorPreviewByIndex(15, AhoyColors.ConvertToHex(backgroundPresetColor[3]));
                    AhoyColors.SetCustomColorPreviewByIndex(23, AhoyColors.ConvertToHex(foregroundPresetColor[3]));
                    // Button 5
                    AhoyColors.SetCustomColorPreviewByIndex(16, AhoyColors.ConvertToHex(backgroundPresetColor[4]));
                    AhoyColors.SetCustomColorPreviewByIndex(24, AhoyColors.ConvertToHex(foregroundPresetColor[4]));
                    // Button 6
                    AhoyColors.SetCustomColorPreviewByIndex(17, AhoyColors.ConvertToHex(backgroundPresetColor[5]));
                    AhoyColors.SetCustomColorPreviewByIndex(25, AhoyColors.ConvertToHex(foregroundPresetColor[5]));
                    // Button 7
                    AhoyColors.SetCustomColorPreviewByIndex(18, AhoyColors.ConvertToHex(backgroundPresetColor[6]));
                    AhoyColors.SetCustomColorPreviewByIndex(26, AhoyColors.ConvertToHex(foregroundPresetColor[6]));
                    // Button 8
                    AhoyColors.SetCustomColorPreviewByIndex(19, AhoyColors.ConvertToHex(backgroundPresetColor[7]));
                    AhoyColors.SetCustomColorPreviewByIndex(27, AhoyColors.ConvertToHex(foregroundPresetColor[7]));

                    UpdateColorTheme();
                }
            }
            catch
            {
                // Do nothing
            }
        }


        #region Pinned preset colors
        private void btnClearPinnedPresetColors_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(12);
            AhoyColors.ResetCustomColorPreviewByIndex(13);
            AhoyColors.ResetCustomColorPreviewByIndex(14);
            AhoyColors.ResetCustomColorPreviewByIndex(15);
            AhoyColors.ResetCustomColorPreviewByIndex(16);
            AhoyColors.ResetCustomColorPreviewByIndex(17);
            AhoyColors.ResetCustomColorPreviewByIndex(18);
            AhoyColors.ResetCustomColorPreviewByIndex(19);
            AhoyColors.ResetCustomColorPreviewByIndex(20);
            AhoyColors.ResetCustomColorPreviewByIndex(21);
            AhoyColors.ResetCustomColorPreviewByIndex(22);
            AhoyColors.ResetCustomColorPreviewByIndex(23);
            AhoyColors.ResetCustomColorPreviewByIndex(24);
            AhoyColors.ResetCustomColorPreviewByIndex(25);
            AhoyColors.ResetCustomColorPreviewByIndex(26);
            AhoyColors.ResetCustomColorPreviewByIndex(27);
            UpdateColorTheme();
        }
        #endregion

        #region Field colors
        private void lbTestContent_Click(object sender, EventArgs e)
        {
            btnFieldForeground_Click(this, null);
        }
        private void btnFieldForeground_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, lbTestContent.BackColor, lbTestContent.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(29, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(30, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnResetFieldColors_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(29);
            AhoyColors.ResetCustomColorPreviewByIndex(30);
            UpdateColorTheme();
        }
        #endregion

        #region Label Color
        private void btnLabelColor_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, lbCurrentTheme.ForeColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(2, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnResetLabelColor_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(2);
            UpdateColorTheme();
        }

        #endregion

        #region List colors
        private void lbTestList_Click(object sender, EventArgs e)
        {
            btnListColors_Click(this, null);
        }

        private void btnListColors_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, lbTestList.BackColor, lbTestList.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(1, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(3, AhoyColorPicker.SecondaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(4, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnResetListColors_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(1);
            AhoyColors.ResetCustomColorPreviewByIndex(3);
            AhoyColors.ResetCustomColorPreviewByIndex(4);
            UpdateColorTheme();
        }

        #endregion

        #region Background color
        private void btnWindowColor_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, this.BackColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(0, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnResetWindowColor_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(0);
            UpdateColorTheme();
        }
        #endregion

        #region Highlight color
        private void btnHighLightColor_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(true, btnHighLightColor.BackColor, btnHighLightColor.ForeColor, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(6, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(28, AhoyColorPicker.PrimaryColorHex);
                AhoyColors.SetCustomColorPreviewByIndex(5, AhoyColorPicker.SecondaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnResetHighLightColor_Click(object sender, EventArgs e)
        {
            AhoyColors.ResetCustomColorPreviewByIndex(6);
            AhoyColors.ResetCustomColorPreviewByIndex(28);
            AhoyColors.ResetCustomColorPreviewByIndex(5);
            UpdateColorTheme();
        }
        #endregion

        #region Interface Options
        private void btnButtonColorsA_Click(object sender, EventArgs e)
        {
            pnlInterfaceOptions.Visible = !pnlInterfaceOptions.Visible;
        }

        // Button/Menu Background
        private void btnInterfaceBackground_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, btnInterfaceBackground.BackColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(7, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        // Button Text color
        private void btnInterfaceText_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, btnInterfaceText.ForeColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(8, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        // Alt 1 (button mouse over)
        private void btnInterfaceAlt1_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, btnInterfaceAlt1.BackColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(10, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        // Alt 2 (button mouse down)
        private void btnInterfaceAlt2_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, btnInterfaceAlt2.BackColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(11, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        // Border
        private void btnInterfaceBorder_Click(object sender, EventArgs e)
        {
            AhoyColorPicker picker = new AhoyColorPicker(false, btnInterfaceBorder.FlatAppearance.BorderColor, Color.Black, "0", "1", "2");
            picker.BringToFront();
            picker.ShowDialog();
            if (picker.Accepted)
            {
                AhoyColors.SetCustomColorPreviewByIndex(9, AhoyColorPicker.PrimaryColorHex);
                UpdateColorTheme();
            }
        }

        private void btnClearButtonColors_Click(object sender, EventArgs e)
        {
            // Clear button background
            AhoyColors.ResetCustomColorPreviewByIndex(7);
            // Clear button text color
            AhoyColors.ResetCustomColorPreviewByIndex(8);
            // Clear button border color
            AhoyColors.ResetCustomColorPreviewByIndex(9);
            // Clear button mouse over
            AhoyColors.ResetCustomColorPreviewByIndex(10);
            // Clear button mouse down
            AhoyColors.ResetCustomColorPreviewByIndex(11);
            UpdateColorTheme();

            pnlInterfaceOptions.Visible = false;
        }
        #endregion

        #region Undo edits
        private void btnUndo_Click(object sender, EventArgs e)
        {
            // If no custom colors where used by the active styling
            // force the editor to reset preview. If custom colors where
            // available they would be set to preview instead by the PreparePreview method
            if (!AhoyColors.HasCustomColors())
            {
                AhoyColors.ResetAllCustomPreviewColors();
            }
            // Prepare previews and save original values
            AhoyColors.PreparePreview();
            MainForm.PrepareTexturePreview();
            BitmapPreviewIndex = ButtonBitmaps.UserSelectedBitmapVersion;
            OriginalColorTheme = AhoyColors.PreviewColorTheme;
            OriginalTextureSelection = MainForm.SelectedTexturePreview;
            OriginalFormOpacity = MainForm.GetFormOpacity();
            OpacityIndex = GetOpacityIndex(OriginalFormOpacity);
            SetOpacityLabel();
            ResolveFormOpacity();
            SetCustomColorThemeTitle(AhoyColors.CustomColorThemeTitle);
            UpdateColorTheme();
            // Handle current prism effect
            PrismEffect = MainForm.PrismEffect;
            RespondToCurrentPrismSetting();
        }
        #endregion



        #region Reset Opacity, Texture and Icon style
        private void btnResetOpacity_Click(object sender, EventArgs e)
        {
            OpacityIndex = 0;
            ResolveFormOpacity();
            ResetSubmissionVariable();
        }

        private void btnResetTexture_Click(object sender, EventArgs e)
        {
            MainForm.SelectedTexturePreview = Settings.DefaultTexture;
            UpdateTexture();
            ResetSubmissionVariable();
        }

        private void btnResetIconStyle_Click(object sender, EventArgs e)
        {
            BitmapPreviewIndex = Settings.DefaultButtonBitmapOverride;
            ButtonBitmaps.UserSelectedBitmapVersion = Settings.DefaultButtonBitmapOverride;
            ResolveBitmapLabel();
            UpdateColorTheme();
        }
        #endregion

        #region Hide Interface options panel by clicking form
        private void StylePicker_Click(object sender, EventArgs e)
        {
            if (pnlInterfaceOptions.Visible)
            {
                pnlInterfaceOptions.Visible = false;
            }
        }
        #endregion

        private void tbThemeTitle_KeyDown(object sender, KeyEventArgs e)
        {
            AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Click the [Name] button to edit theme title", MessageText.NameCaption(), false);
            msg.ShowDialog();
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        #region Set theme name
        private void tbThemeTitle_Click(object sender, EventArgs e)
        {
            btnSetName_Click(this, null);
        }

        private void btnSetName_Click(object sender, EventArgs e)
        {
            if (AhoyColors.HasCustomColorsPreview())
            {
                InputPrompt input = new InputPrompt("Name", AhoyColors.CustomColorThemeTitle, "", "Set Name", 256, false, "Submit", false, null, null, false);
                input.TopMost = true;
                input.ShowDialog();

                if (input.UserInput != null && input.UserInput.Length > 0)
                {
                    AhoyColors.CustomColorThemeTitle = input.UserInput.Trim();
                    SetCustomColorThemeTitle(AhoyColors.CustomColorThemeTitle);
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please create a custom color theme to\nset the Name field", MessageText.NameCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }

        private void btnResetName_Click(object sender, EventArgs e)
        {
            if (AhoyColors.CustomColorThemeTitle != null && AhoyColors.CustomColorThemeTitle.Length > 0)
            {
                AhoyColors.CustomColorThemeTitle = Settings.DefaultThemeTitle;
                UpdateColorTheme();
            }
        }
        #endregion



        #region Import/Eksport
        private void btnImportTheme_Click(object sender, EventArgs e)
        {
            Reaction();
            Log.LogImportTheme();
            string tempOpenFilePath = "";
            // Open file dialog
            tempOpenFilePath = IO.OpenFileDialog(1);
            Settings settingsObject = IO.ImportTheme(tempOpenFilePath);

            if (settingsObject.LoadSuccess && settingsObject.CustomColors.Contains("#"))
            {
                OverrideThemePrism = true;
                ResetSubmissionVariable();

                // 1 Set title
                if (settingsObject.ThemeTitle.Length == 0)
                {
                    AhoyColors.CustomColorThemeTitle = "Custom Theme";
                }
                else
                {
                    AhoyColors.CustomColorThemeTitle = settingsObject.ThemeTitle;
                }

                // 2 Set opacity
                OpacityIndex = GetOpacityIndex(settingsObject.Opacity);
                SetOpacityLabel();
                ResolveFormOpacity();

                // 3 Set texture
                MainForm.SelectedTexturePreview = settingsObject.Texture;
                UpdateTexture();

                // 4 Buttons
                BitmapPreviewIndex = settingsObject.ButtonBitmapOverride;
                ResolveBitmapLabel();

                // Prism effect
                PrismEffect = settingsObject.PrismEffect; // The respons to this setting is done through the UpdateColorTheme method

                // Home screen
                MainForm.DisplayTextCycle = settingsObject.DisplayTextCycle;

                // Last update colortheme
                AhoyColors.PreviewColorTheme = settingsObject.ColorTheme;
                AhoyColors.LoadCustomPreviewColorsIntoList(settingsObject.CustomColors);
                UpdateColorTheme();

            }
            else
            {
                Log.LogImportThemeError();
            }
        }

        private void btnExportTheme_Click(object sender, EventArgs e)
        {
            Reaction();
            if (AhoyColors.HasCustomColorsPreview())
            {
                Log.LogExportTheme();
                // New settings object
                Settings settingsObject = new Settings();
                // Save values
                if (AhoyColors.CustomColorThemeTitle.Length == 0)
                {
                    AhoyColors.CustomColorThemeTitle = tbThemeTitle.Text.Replace(":", "-");
                }
                settingsObject.ThemeTitle = AhoyColors.CustomColorThemeTitle;
                settingsObject.ColorTheme = AhoyColors.PreviewColorTheme;
                settingsObject.Opacity = this.Opacity;
                settingsObject.Texture = MainForm.SelectedTexturePreview;
                settingsObject.ButtonBitmapOverride = BitmapPreviewIndex;
                settingsObject.PrismEffect = PrismEffect;
                settingsObject.DisplayTextCycle = MainForm.DisplayTextCycle;
                settingsObject.CustomColors = AhoyColors.RetrieveCustomColorPreviewAsCSV();

                if (IO.ExportTheme(settingsObject))
                {
                    Log.LogExportTheme();
                }
                else
                {
                    Log.LogExportThemeError();
                }
            }
            else
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Please create a custom color theme to export", MessageText.ExportCaption(), false);
                msg.TopMost = true;
                msg.ShowDialog();
            }
        }
        #endregion

        #region Prism effect
        private void lbPrismEffect_Click(object sender, EventArgs e)
        {
            ResolvePrismEffect();
        }

        private void RespondToCurrentPrismSetting()
        {
            if (PrismEffect)
            {
                StartColorShiftTimer();
            }
            else
            {
                StopColorShiftTimer();
            }
        }

        private void ResolvePrismEffect()
        {
            if (PrismEffect)
            {
                StopColorShiftTimer();
            }
            else
            {
                StartColorShiftTimer();
            }
        }

        private void StartColorShiftTimer()
        {
            // Create timer instance
            if (tempColorShiftTimer == null)
            {
                tempColorShiftTimer = new Timer();
                tempColorShiftTimer.Interval = 5;
                tempColorShiftTimer.Tick += TempColorShiftTimer_Tick;
            }

            if (tempColorShiftTimer != null && !tempColorShiftTimer.Enabled)
            {
                lbPrismEffect.Text = "Prism: ON";
                PrismEffect = true;
                ShiftTimerCounter = ShiftTimerCounterDefault;
                tempColorShiftTimer.Enabled = true;
            }
        }

        private void StopColorShiftTimer()
        {
            if (tempColorShiftTimer != null && tempColorShiftTimer.Enabled)
            {
                PrismEffect = false;
                tempColorShiftTimer.Enabled = false;
                lbPrismEffect.Text = "Prism: OFF";
                lbPrismEffect.BackColor = AhoyColors.CurrentColorThemePreview()[29];
                lbPrismEffect.ForeColor = AhoyColors.CurrentColorThemePreview()[30];
            }
        }

        // Color shift timer
        private float ShiftTimerCounter = 0f;
        private const float ShiftTimerCounterDefault = 0f;
        Timer tempColorShiftTimer;
        private void TempColorShiftTimer_Tick(object sender, EventArgs e)
        {
            ShiftTimerCounter += 0.001f;
            if (ShiftTimerCounter > 1)
            {
                ShiftTimerCounter = ShiftTimerCounterDefault;
            }
            lbPrismEffect.ForeColor = Rainbow(ShiftTimerCounter);
        }

        // Generate color from input
        private static int MaxValue = 255;
        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * MaxValue);
            int descending = MaxValue - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(MaxValue, MaxValue, ascending, 0);
                case 1:
                    return Color.FromArgb(MaxValue, descending, MaxValue, 0);
                case 2:
                    return Color.FromArgb(MaxValue, 0, MaxValue, ascending);
                case 3:
                    return Color.FromArgb(MaxValue, 0, descending, MaxValue);
                case 4:
                    return Color.FromArgb(MaxValue, ascending, 0, MaxValue);
                default: // case 5:
                    return Color.FromArgb(MaxValue, MaxValue, 0, descending);
            }
        }
        #endregion

    }
}
