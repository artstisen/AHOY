using System.Windows.Forms;

namespace AhoyBrowser
{
    public partial class MagicBlockerOverlay : Form
    {
        public MagicBlockerOverlay()
        {
            InitializeComponent();
            // Set image to be stretched
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            // Set font
            lbIdentity.Font = Design.MessageboxTinyFont();

            // Make label transparent
            var pos = this.PointToScreen(lbIdentity.Location);
            pos = picImage.PointToClient(pos);
            lbIdentity.Parent = picImage;
            lbIdentity.Location = pos;
            lbIdentity.Dock = DockStyle.Bottom;
            lbIdentity.BackColor = System.Drawing.Color.Transparent;
            lbIdentity.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            // Set text
            lbIdentity.Text = MagicBlocker.IndicatorLabelText;
        }

        // Close by ESC (does not actually work as the main overlay will jump on-top by timer
        // to "hide" the overlay)
        private void MagicBlockOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
