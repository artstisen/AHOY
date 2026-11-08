namespace AhoyBrowser
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.lbVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerSplash
            // 
            this.timerSplash.Interval = 500;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplashTimer_Tick);
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lbVersion.Location = new System.Drawing.Point(24, 21);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(514, 23);
            this.lbVersion.TabIndex = 0;
            this.lbVersion.Text = "--";
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 550);
            this.Controls.Add(this.lbVersion);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximumSize = new System.Drawing.Size(550, 550);
            this.MinimumSize = new System.Drawing.Size(550, 550);
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Splash_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.Label lbVersion;
    }
}