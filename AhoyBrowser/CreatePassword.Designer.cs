namespace AhoyBrowser
{
    partial class CreatePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePassword));
            this.btnSave = new System.Windows.Forms.Button();
            this.tbPassword1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPassword2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbErrorMessage = new System.Windows.Forms.Label();
            this.lbWarning = new System.Windows.Forms.Label();
            this.cbUnderstand = new System.Windows.Forms.CheckBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbEscapeText = new System.Windows.Forms.Label();
            this.lbStrength = new System.Windows.Forms.Label();
            this.picHelp = new System.Windows.Forms.PictureBox();
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
            this.contextMenuBasic.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnSave.Location = new System.Drawing.Point(257, 282);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.toolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbPassword1
            // 
            this.tbPassword1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbPassword1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbPassword1.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbPassword1.Location = new System.Drawing.Point(37, 94);
            this.tbPassword1.MaxLength = 20;
            this.tbPassword1.Name = "tbPassword1";
            this.tbPassword1.PasswordChar = '*';
            this.tbPassword1.Size = new System.Drawing.Size(214, 19);
            this.tbPassword1.TabIndex = 0;
            this.tbPassword1.Click += new System.EventHandler(this.tbPassword1_Click);
            this.tbPassword1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPassword1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.label1.Location = new System.Drawing.Point(34, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "--";
            // 
            // tbPassword2
            // 
            this.tbPassword2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbPassword2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbPassword2.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbPassword2.Location = new System.Drawing.Point(37, 135);
            this.tbPassword2.MaxLength = 20;
            this.tbPassword2.Name = "tbPassword2";
            this.tbPassword2.PasswordChar = '*';
            this.tbPassword2.Size = new System.Drawing.Size(214, 19);
            this.tbPassword2.TabIndex = 1;
            this.tbPassword2.Click += new System.EventHandler(this.tbPassword2_Click);
            this.tbPassword2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPassword1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.label2.Location = new System.Drawing.Point(34, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "--";
            // 
            // lbErrorMessage
            // 
            this.lbErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0)))));
            this.lbErrorMessage.Location = new System.Drawing.Point(34, 282);
            this.lbErrorMessage.Name = "lbErrorMessage";
            this.lbErrorMessage.Size = new System.Drawing.Size(217, 23);
            this.lbErrorMessage.TabIndex = 7;
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = false;
            this.lbWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbWarning.Location = new System.Drawing.Point(34, 165);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(298, 82);
            this.lbWarning.TabIndex = 9;
            this.lbWarning.Text = "--";
            // 
            // cbUnderstand
            // 
            this.cbUnderstand.Location = new System.Drawing.Point(37, 252);
            this.cbUnderstand.Name = "cbUnderstand";
            this.cbUnderstand.Size = new System.Drawing.Size(295, 22);
            this.cbUnderstand.TabIndex = 3;
            this.cbUnderstand.Text = "I understand: a forgotten password cannot be recovered";
            this.cbUnderstand.UseVisualStyleBackColor = true;
            // 
            // lbMessage
            // 
            this.lbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbMessage.Location = new System.Drawing.Point(34, 36);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(298, 26);
            this.lbMessage.TabIndex = 12;
            this.lbMessage.Text = "Enter password";
            // 
            // lbEscapeText
            // 
            this.lbEscapeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEscapeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbEscapeText.Location = new System.Drawing.Point(187, 9);
            this.lbEscapeText.Name = "lbEscapeText";
            this.lbEscapeText.Size = new System.Drawing.Size(175, 15);
            this.lbEscapeText.TabIndex = 13;
            this.lbEscapeText.Text = "Press ESC to cancel";
            this.lbEscapeText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbStrength
            // 
            this.lbStrength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbStrength.Location = new System.Drawing.Point(257, 94);
            this.lbStrength.Name = "lbStrength";
            this.lbStrength.Size = new System.Drawing.Size(105, 19);
            this.lbStrength.TabIndex = 14;
            this.lbStrength.Text = "N/A";
            this.lbStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picHelp
            // 
            this.picHelp.BackColor = System.Drawing.Color.Transparent;
            this.picHelp.BackgroundImage = global::AhoyBrowser.Properties.Resources.help;
            this.picHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picHelp.Cursor = System.Windows.Forms.Cursors.Default;
            this.picHelp.Location = new System.Drawing.Point(260, 116);
            this.picHelp.Name = "picHelp";
            this.picHelp.Size = new System.Drawing.Size(20, 20);
            this.picHelp.TabIndex = 93;
            this.picHelp.TabStop = false;
            this.toolTip1.SetToolTip(this.picHelp, "Help");
            this.picHelp.Click += new System.EventHandler(this.picHelp_Click);
            // 
            // contextMenuBasic
            // 
            this.contextMenuBasic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuBasic.Name = "contextMenuBasic";
            this.contextMenuBasic.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // CreatePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(374, 325);
            this.ControlBox = false;
            this.Controls.Add(this.picHelp);
            this.Controls.Add(this.lbStrength);
            this.Controls.Add(this.lbEscapeText);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.cbUnderstand);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.lbErrorMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbPassword1);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximumSize = new System.Drawing.Size(390, 350);
            this.MinimumSize = new System.Drawing.Size(390, 350);
            this.Name = "CreatePassword";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Password";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CreatePassword_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
            this.contextMenuBasic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbPassword1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPassword2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbErrorMessage;
        private System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.CheckBox cbUnderstand;
        private System.Windows.Forms.Label lbMessage;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label lbEscapeText;
        private System.Windows.Forms.Label lbStrength;
        private System.Windows.Forms.PictureBox picHelp;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}