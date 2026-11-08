namespace AhoyBrowser
{
    partial class MagicBlocker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicBlocker));
            this.timerBlocker = new System.Windows.Forms.Timer(this.components);
            this.timerFader = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerBlocker
            // 
            this.timerBlocker.Tick += new System.EventHandler(this.timerBlocker_Tick);
            // 
            // timerFader
            // 
            this.timerFader.Interval = 50;
            this.timerFader.Tick += new System.EventHandler(this.timerFader_Tick);
            // 
            // MagicBlocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MagicBlocker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MagicBlock_FormClosing);
            this.Click += new System.EventHandler(this.MagicBlock_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MagicBlock_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerBlocker;
        private System.Windows.Forms.Timer timerFader;
    }
}