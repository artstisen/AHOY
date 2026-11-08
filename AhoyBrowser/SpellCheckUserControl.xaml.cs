using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace AhoyBrowser
{
    /// <summary>
    /// Interaction logic for SpellCheckUserControl.xaml
    /// </summary>
    public partial class SpellCheckUserControl : UserControl
    {

        public int SpellCheckerSelectedLanguage { get; set; }
        public int CaretPosition { get; set; }
        private SolidColorBrush TextHighlightColor { get; set; }
        private SolidColorBrush TextForeColor { get; set; }


        public SpellCheckUserControl(Color textHighlightColor, Color textForeColor)
        {
            // Must be set first
            TextHighlightColor = new SolidColorBrush(textHighlightColor);
            TextForeColor = new SolidColorBrush(textForeColor);
            InitializeComponent();
            // Initialize spell checker language variable
            SpellCheckerSelectedLanguage = 1;
            CaretPosition = 0;
        }

        public void SetCaretPosition(int caretPosition)
        {
            CaretPosition = caretPosition;
        }

        public void SetLanguage(int lang)
        {
            switch (lang)
            {
                case 1:
                    rbEnglishGB.IsChecked = true;
                    break;
                case 2:
                    rbEnglishUS.IsChecked = true;
                    break;
                case 3:
                    rbGermanDE.IsChecked = true;
                    break;
                case 4:
                    rbFrenchFR.IsChecked = true;
                    break;
                case 5:
                    rbSpanishES.IsChecked = true;
                    break;
                default:
                    rbEnglishGB.IsChecked = true;
                    break;
            }
                       
        }

        public void TextBoxFontFamily(string fontFamily)
        {
            tbSpellChecker.FontFamily = new FontFamily(fontFamily);
        }

        public void TextBoxForeColor(Color color)
        {
            tbSpellChecker.Foreground = new SolidColorBrush(color);
        }

        public void TextBoxBackColor(Color color)
        {
            tbSpellChecker.Background = new SolidColorBrush(color);
        }

        public void ButtonsForeColor(Color color)
        {
            rbEnglishGB.Foreground = new SolidColorBrush(color);
            rbEnglishUS.Foreground = new SolidColorBrush(color);
            rbGermanDE.Foreground = new SolidColorBrush(color);
            rbFrenchFR.Foreground = new SolidColorBrush(color);
            rbSpanishES.Foreground = new SolidColorBrush(color);
        }

        public void ButtonsBackColor(Color color)
        {
            rbEnglishGB.Background = new SolidColorBrush(color);
            rbEnglishUS.Background = new SolidColorBrush(color);
            rbGermanDE.Background = new SolidColorBrush(color);
            rbFrenchFR.Background = new SolidColorBrush(color);
            rbSpanishES.Background = new SolidColorBrush(color);
        }

        private void UpdateButtonHighlight()
        {
            ResetRadioButtonHighlightColors();
            if (rbEnglishGB != null && rbEnglishGB.IsChecked == true)
            {
                rbEnglishGB.Foreground = TextHighlightColor;
            }
            else if (rbEnglishUS != null && rbEnglishUS.IsChecked == true)
            {
                rbEnglishUS.Foreground = TextHighlightColor;
            }
            else if (rbGermanDE != null && rbGermanDE.IsChecked == true)
            {
                rbGermanDE.Foreground = TextHighlightColor;
            }
            else if (rbFrenchFR != null && rbFrenchFR.IsChecked == true)
            {
                rbFrenchFR.Foreground = TextHighlightColor;
            }
            else if (rbSpanishES != null && rbSpanishES.IsChecked == true)
            {
                rbSpanishES.Foreground = TextHighlightColor;
            }

        }

        private void ResetRadioButtonHighlightColors()
        {
            // Reset forecolors before highlighting
            if (rbEnglishGB != null)
            {
                rbEnglishGB.Foreground = TextForeColor;
            }

            if (rbEnglishUS != null)
            {
                rbEnglishUS.Foreground = TextForeColor;
            }

            if (rbGermanDE != null)
            {
                rbGermanDE.Foreground = TextForeColor;
            }

            if (rbFrenchFR != null)
            {
                rbFrenchFR.Foreground = TextForeColor;
            }

            if (rbSpanishES != null)
            {
                rbSpanishES.Foreground = TextForeColor;
            }
        }

        // Set and Get text
        public void SetText(string text)
        {
            tbSpellChecker.Text = text;
            // Scroll to Caret
            tbSpellChecker.SelectionStart = CaretPosition;
            // Set highlight tekst for Radio Buttons here
            UpdateButtonHighlight();
        }

        public string GetText()
        {
            return tbSpellChecker.Text;
        }

        // WordWrap
        public void WordWrap(bool wordWrap)
        {
            if (wordWrap)
            {
                tbSpellChecker.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                tbSpellChecker.TextWrapping = TextWrapping.NoWrap;
            }
        }


        // Set language
        private void rbEnglishGB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tbSpellChecker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-GB");
                SpellCheckerSelectedLanguage = 1;
                tbSpellChecker.Focus();
                UpdateButtonHighlight();
            }
            catch
            {
                SpellCheckerSelectedLanguage = 0;
                AhoySimpleMessageBox mgs = new AhoySimpleMessageBox(GetUnsupportedLanguageMessage(), MessageText.SpellCheckCaption(), false);
                mgs.ShowDialog();
            }
        }

        private void rbEnglishUS_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tbSpellChecker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-US");
                SpellCheckerSelectedLanguage = 2;
                tbSpellChecker.Focus();
                UpdateButtonHighlight();
            }
            catch
            {
                SpellCheckerSelectedLanguage = 0;
                AhoySimpleMessageBox mgs = new AhoySimpleMessageBox(GetUnsupportedLanguageMessage(), MessageText.SpellCheckCaption(), false);
                mgs.ShowDialog();
            }
        }

        private void rbGermanDE_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tbSpellChecker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("de-DE");
                SpellCheckerSelectedLanguage = 3;
                tbSpellChecker.Focus();
                UpdateButtonHighlight();
            }
            catch
            {
                SpellCheckerSelectedLanguage = 0;
                AhoySimpleMessageBox mgs = new AhoySimpleMessageBox(GetUnsupportedLanguageMessage(), MessageText.SpellCheckCaption(), false);
                mgs.ShowDialog();
            }
        }

        private void rbFrenchFR_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tbSpellChecker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("fr-FR");
                SpellCheckerSelectedLanguage = 4;
                tbSpellChecker.Focus();
                UpdateButtonHighlight();
            }
            catch
            {
                SpellCheckerSelectedLanguage = 0;
                AhoySimpleMessageBox mgs = new AhoySimpleMessageBox(GetUnsupportedLanguageMessage(), MessageText.SpellCheckCaption(), false);
                mgs.ShowDialog();
            }
        }

        private void rbSpanishES_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tbSpellChecker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("es-ES");
                SpellCheckerSelectedLanguage = 5;
                tbSpellChecker.Focus();
                UpdateButtonHighlight();
            }
            catch
            {
                SpellCheckerSelectedLanguage = 0;
                AhoySimpleMessageBox mgs = new AhoySimpleMessageBox(GetUnsupportedLanguageMessage(), MessageText.SpellCheckCaption(), false);
                mgs.ShowDialog();
            }
        }


        private string GetUnsupportedLanguageMessage()
        {
            return "Language not supported on this computer. Please consult the documentation for help";
        }


        // Handle Cut and Copy commands the WPF way (because binding keyboard shortcuts require more work with WPF)
        private string tempUnformattedText;
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy)
            {
                tbSpellChecker.Copy();
                // Convert to plan text
                tempUnformattedText = Clipboard.GetText(TextDataFormat.UnicodeText);
                Clipboard.SetText(tempUnformattedText);
                MainForm.PersistClipboard();
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Cut)
            {
                tbSpellChecker.Cut();
                MainForm.PersistClipboard();
                e.Handled = true;
            }
        }

        // Handle other keys here
        private void tbSpellChecker_KeyDown(object sender, KeyEventArgs e)
        {
            // Tell the parent form to close
            if ((System.Windows.Forms.Keys)KeyInterop.VirtualKeyFromKey(e.Key) == System.Windows.Forms.Keys.Escape)
            {
                WPFSpellChecker.InvokeCloseForm = true;
                e.Handled = true;
            }
            else if ((System.Windows.Forms.Keys)KeyInterop.VirtualKeyFromKey(e.Key) == System.Windows.Forms.Keys.F5)
            {
                WPFSpellChecker.InvokeSubmitChanges = true;
                e.Handled = true;
            }
        }

    }

}
