using System;
using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class CustomCheckBoxButton : Button
    {
        /* Custom checkbox button v1.0 by LBS, Okt. 2014 */
        public string CheckedText { get; set; }
        public string UnCheckedText { get; set; }
        public Color CheckedTextColor { get; set; }
        public Color UnCheckedTextColor { get; set; }
        public Bitmap CheckedBackgroundImage { get; set; }
        public Bitmap UnCheckedBackgroundImage { get; set; }

        public CustomCheckBoxButton()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Zoom;
            // Default to unchecked
            buttonState = false;
            // Update visible state
            ResolveDisplay();
        }

        private bool buttonState;
        internal bool Checked
        {
            get
            {
                return buttonState;

            }
            set
            {
                buttonState = value;
                ResolveDisplay();
            }
        }

        public void ResolveDisplay()
        {
            if (buttonState)
            {
                this.BackgroundImage = CheckedBackgroundImage;
                this.ForeColor = CheckedTextColor;
                this.Text = CheckedText;
            }
            else
            {
                this.BackgroundImage = UnCheckedBackgroundImage;
                this.ForeColor = UnCheckedTextColor;
                this.Text = UnCheckedText;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            buttonState = !buttonState;
            ResolveDisplay();
            base.OnClick(e);
        }
    }
}
