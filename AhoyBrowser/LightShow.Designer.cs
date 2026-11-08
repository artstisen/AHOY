namespace AhoyBrowser
{
    partial class Marquee
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOutput.ForeColor = System.Drawing.Color.White;
            this.lbOutput.Location = new System.Drawing.Point(0, 0);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(250, 50);
            this.lbOutput.TabIndex = 0;
            this.lbOutput.Text = "Ready";
            this.lbOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LightShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbOutput);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(100, 20);
            this.Name = "LightShow";
            this.Size = new System.Drawing.Size(250, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbOutput;
    }
}
