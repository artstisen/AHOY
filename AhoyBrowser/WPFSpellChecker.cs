using System;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class WPFSpellChecker : Form
    {
        internal string TextToBeChecked;
        internal static bool InvokeCloseForm { get; set; }
        internal static bool InvokeSubmitChanges { get; set; }
        public WPFSpellChecker(string textToBeChecked, bool wordWrap, bool emptySpellChecker, int language, int caretPosition, double formOpacity)
        {
            InitializeComponent();

            // Will enable the form to take keypress events like Close on ESC
            this.KeyPreview = true;
            this.ControlBox = false;
            InvokeCloseForm = false;
            InvokeSubmitChanges = false;

            // Set button appearance
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnSubmit.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnSubmit.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];
            btnCancel.FlatAppearance.BorderColor = AhoyColors.CurrentColorTheme()[9];
            btnCancel.FlatAppearance.MouseOverBackColor = AhoyColors.CurrentColorTheme()[10];
            btnCancel.FlatAppearance.MouseDownBackColor = AhoyColors.CurrentColorTheme()[11];

            // Set colors
            this.BackColor = AhoyColors.CurrentColorTheme()[0];
            elementHost1.BackColor = AhoyColors.CurrentColorTheme()[0];
            btnSubmit.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnSubmit.ForeColor = AhoyColors.CurrentColorTheme()[8];
            btnCancel.BackColor = AhoyColors.CurrentColorTheme()[7];
            btnCancel.ForeColor = AhoyColors.CurrentColorTheme()[8];

            // Setup tooltip colors
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += toolTip1_Draw;
            toolTip1.BackColor = AhoyColors.CurrentColorTheme()[1];
            toolTip1.ForeColor = AhoyColors.CurrentColorTheme()[3];

            // Set values
            if (!emptySpellChecker)
            {
                TextToBeChecked = textToBeChecked;
            }
            else
            {
                btnSubmit.Visible = false;
                btnCancel.Text = "Close";
                this.toolTip1.SetToolTip(this.btnCancel, "Close (ESC)");
                TextToBeChecked = "";
            }
            spellCheckUserControl1.WordWrap(wordWrap);
            spellCheckUserControl1.SetLanguage(language);
            spellCheckUserControl1.SetCaretPosition(caretPosition);
            spellCheckUserControl1.TextBoxFontFamily(Design.GetFontName());

            // Set more colors
            spellCheckUserControl1.ButtonsForeColor(System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[8].R, AhoyColors.CurrentColorTheme()[8].G, AhoyColors.CurrentColorTheme()[8].B));
            spellCheckUserControl1.ButtonsBackColor(System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[7].R, AhoyColors.CurrentColorTheme()[7].G, AhoyColors.CurrentColorTheme()[7].B));
            spellCheckUserControl1.TextBoxForeColor(System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[3].R, AhoyColors.CurrentColorTheme()[3].G, AhoyColors.CurrentColorTheme()[3].B));
            spellCheckUserControl1.TextBoxBackColor(System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[1].R, AhoyColors.CurrentColorTheme()[1].G, AhoyColors.CurrentColorTheme()[1].B));

            // Form Opacity
            if (formOpacity > 0)
            {
                this.Opacity = formOpacity;
            }
            else
            {
                this.Opacity = 1.00d;
            }

            // Resolve Texture
            ResolveTexture();
        }

        private void ResolveTexture()
        {
            if (MainForm.IsTextureEnabled())
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = MainForm.GetTexture(MainForm.SelectedTexture);
            }
        }

        private void WPFSpellChecker_Load(object sender, EventArgs e)
        {
            timerFormEvents.Interval = 100;
            timerFormEvents.Enabled = true;

            // Set text to be checked
            spellCheckUserControl1.SetText(TextToBeChecked);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm.SetSpellCheckerLanguage(spellCheckUserControl1.SpellCheckerSelectedLanguage);
            MainForm.RefreshForm();
            this.Close();
        }

        // Only works if KeyPreview is set to true
        private void WPFSpellChecker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                MainForm.SetSpellCheckerLanguage(spellCheckUserControl1.SpellCheckerSelectedLanguage);
                MainForm.RefreshForm();
                this.Close();
            }
        }

        // F5 submits (F5 has no KeyChar, so it can't be handled in KeyPress)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                btnSubmit_Click(this, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MainForm.SpellCheckSubmitted = true;
            MainForm.SpellCheckedText = spellCheckUserControl1.GetText();
            MainForm.SetSpellCheckerLanguage(spellCheckUserControl1.SpellCheckerSelectedLanguage);
            MainForm.RefreshForm();
            this.Close();
        }

        // Handle events sent from the hosted WPF user control
        private void timerFormEvents_Tick(object sender, EventArgs e)
        {
            if (InvokeCloseForm)
            {
                timerFormEvents.Enabled = false;
                MainForm.SetSpellCheckerLanguage(spellCheckUserControl1.SpellCheckerSelectedLanguage);
                MainForm.RefreshForm();
                this.Close();
            }
            else if (InvokeSubmitChanges)
            {
                btnSubmit_Click(this, null);
            }
        }


        // Handle tooltip draw to set custom back and foreground colors
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }


    }
}
