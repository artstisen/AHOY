namespace AhoyBrowser
{
    partial class MagicBlockerOverlay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicBlockerOverlay));
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lbIdentity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Image = global::AhoyBrowser.Properties.Resources.magicblock;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(335, 335);
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // lbIdentity
            // 
            this.lbIdentity.BackColor = System.Drawing.Color.Transparent;
            this.lbIdentity.Location = new System.Drawing.Point(12, 303);
            this.lbIdentity.Name = "lbIdentity";
            this.lbIdentity.Size = new System.Drawing.Size(311, 23);
            this.lbIdentity.TabIndex = 1;
            this.lbIdentity.Text = "Ready";
            this.lbIdentity.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MagicBlockerOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 335);
            this.Controls.Add(this.lbIdentity);
            this.Controls.Add(this.picImage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(335, 335);
            this.MinimumSize = new System.Drawing.Size(335, 335);
            this.Name = "MagicBlockerOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MagicBlockOverlay";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MagicBlockOverlay_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lbIdentity;
    }
}