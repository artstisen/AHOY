namespace AhoyBrowser
{
    partial class WPFSpellChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WPFSpellChecker));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timerFormEvents = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.spellCheckUserControl1 = new AhoyBrowser.SpellCheckUserControl(System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[6].R, AhoyColors.CurrentColorTheme()[6].G, AhoyColors.CurrentColorTheme()[6].B),
                                                                                System.Windows.Media.Color.FromRgb(AhoyColors.CurrentColorTheme()[8].R, AhoyColors.CurrentColorTheme()[8].G, AhoyColors.CurrentColorTheme()[8].B));
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSubmit.Location = new System.Drawing.Point(496, 523);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.toolTip1.SetToolTip(this.btnSubmit, "Save Changes (F5)");
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.Location = new System.Drawing.Point(577, 523);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel (ESC)");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timerFormEvents
            // 
            this.timerFormEvents.Enabled = true;
            this.timerFormEvents.Tick += new System.EventHandler(this.timerFormEvents_Tick);
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(760, 500);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.spellCheckUserControl1;
            // 
            // WPFSpellChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.elementHost1);
            this.DoubleBuffered = true;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "WPFSpellChecker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spell Check";
            this.Load += new System.EventHandler(this.WPFSpellChecker_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WPFSpellChecker_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private SpellCheckUserControl spellCheckUserControl1;
        private System.Windows.Forms.Timer timerFormEvents;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}