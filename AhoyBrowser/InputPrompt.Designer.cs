namespace AhoyBrowser
{
    partial class InputPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputPrompt));
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbAttentionMessage = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbCommandPromptLabel = new System.Windows.Forms.Label();
            this.cbPinned = new AhoyBrowser.CustomCheckBoxButton();
            this.contextMenuBasic.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0)))));
            this.tbInput.Location = new System.Drawing.Point(38, 75);
            this.tbInput.MaxLength = 20;
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(214, 22);
            this.tbInput.TabIndex = 4;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnOK.Location = new System.Drawing.Point(258, 75);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbMessage.Location = new System.Drawing.Point(35, 59);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(379, 13);
            this.lbMessage.TabIndex = 6;
            this.lbMessage.Text = "Enter Text";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCancel.Location = new System.Drawing.Point(339, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbAttentionMessage
            // 
            this.lbAttentionMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbAttentionMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbAttentionMessage.Location = new System.Drawing.Point(35, 9);
            this.lbAttentionMessage.Name = "lbAttentionMessage";
            this.lbAttentionMessage.Size = new System.Drawing.Size(402, 47);
            this.lbAttentionMessage.TabIndex = 8;
            this.lbAttentionMessage.Text = "--";
            this.lbAttentionMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // lbCommandPromptLabel
            // 
            this.lbCommandPromptLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbCommandPromptLabel.Location = new System.Drawing.Point(12, 69);
            this.lbCommandPromptLabel.Name = "lbCommandPromptLabel";
            this.lbCommandPromptLabel.Size = new System.Drawing.Size(20, 34);
            this.lbCommandPromptLabel.TabIndex = 10;
            this.lbCommandPromptLabel.Text = ">";
            this.lbCommandPromptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbPinned
            // 
            this.cbPinned.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbPinned.CheckedBackgroundImage = null;
            this.cbPinned.CheckedText = null;
            this.cbPinned.CheckedTextColor = System.Drawing.Color.Empty;
            this.cbPinned.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbPinned.Location = new System.Drawing.Point(38, 103);
            this.cbPinned.Name = "cbPinned";
            this.cbPinned.Size = new System.Drawing.Size(37, 23);
            this.cbPinned.TabIndex = 9;
            this.cbPinned.UnCheckedBackgroundImage = null;
            this.cbPinned.UnCheckedText = null;
            this.cbPinned.UnCheckedTextColor = System.Drawing.Color.Empty;
            this.cbPinned.UseVisualStyleBackColor = true;
            this.cbPinned.Click += new System.EventHandler(this.cbPinned_Click);
            // 
            // InputPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(449, 177);
            this.ControlBox = false;
            this.Controls.Add(this.lbCommandPromptLabel);
            this.Controls.Add(this.cbPinned);
            this.Controls.Add(this.lbAttentionMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbInput);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 215);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(465, 215);
            this.Name = "InputPrompt";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input";
            this.TopMost = true;
            this.contextMenuBasic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbAttentionMessage;
        private CustomCheckBoxButton cbPinned;
        private System.Windows.Forms.ToolTip toolTip1;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label lbCommandPromptLabel;
    }
}