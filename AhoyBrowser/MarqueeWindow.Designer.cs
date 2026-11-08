namespace AhoyBrowser
{
    partial class MarqueeWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarqueeWindow));
            this.marqueeBanner = new Marquee();
            this.SuspendLayout();
            // 
            // marqueeBanner
            // 
            this.marqueeBanner.BackColor = System.Drawing.Color.Black;
            this.marqueeBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marqueeBanner.Location = new System.Drawing.Point(0, 0);
            this.marqueeBanner.MinimumSize = new System.Drawing.Size(100, 20);
            this.marqueeBanner.Name = "marqueeBanner";
            this.marqueeBanner.Size = new System.Drawing.Size(384, 41);
            this.marqueeBanner.TabIndex = 0;
            // 
            // MarqueeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 41);
            this.ControlBox = false;
            this.Controls.Add(this.marqueeBanner);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximumSize = new System.Drawing.Size(400, 75);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 75);
            this.Name = "MarqueeWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reminder";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Marquee marqueeBanner;
    }
}