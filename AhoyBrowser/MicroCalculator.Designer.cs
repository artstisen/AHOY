namespace AhoyBrowser
{
    partial class MicroCalculator
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
            this.components = new System.ComponentModel.Container();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.lbOutput = new System.Windows.Forms.Label();
            this.lbClose = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picHelp = new System.Windows.Forms.PictureBox();
            this.lbIndicator = new System.Windows.Forms.Label();
            this.pnlInputBox = new System.Windows.Forms.Panel();
            this.lbClear = new System.Windows.Forms.Label();
            this.lbRecallPrevious = new System.Windows.Forms.Label();
            this.lbRecallNext = new System.Windows.Forms.Label();
            this.lbCopy = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.contextMenuBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
            this.pnlInputBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0)))));
            this.tbInput.Location = new System.Drawing.Point(26, 32);
            this.tbInput.MaxLength = 64;
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(284, 19);
            this.tbInput.TabIndex = 5;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // btnGo
            // 
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnGo.Location = new System.Drawing.Point(316, 29);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(37, 23);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "GO";
            this.toolTip1.SetToolTip(this.btnGo, "Execute (Enter)");
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lbOutput
            // 
            this.lbOutput.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbOutput.Location = new System.Drawing.Point(3, 0);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(353, 25);
            this.lbOutput.TabIndex = 7;
            this.lbOutput.Text = "Ready";
            this.lbOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lbOutput, "Click to Copy Result");
            this.lbOutput.Click += new System.EventHandler(this.lbOutput_Click);
            // 
            // lbClose
            // 
            this.lbClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbClose.Location = new System.Drawing.Point(339, 3);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(20, 20);
            this.lbClose.TabIndex = 8;
            this.lbClose.Text = "X";
            this.lbClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbClose, "Close (ESC)");
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
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
            // picHelp
            // 
            this.picHelp.BackColor = System.Drawing.Color.Transparent;
            this.picHelp.BackgroundImage = global::AhoyBrowser.Properties.Resources.help;
            this.picHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picHelp.Cursor = System.Windows.Forms.Cursors.Default;
            this.picHelp.Location = new System.Drawing.Point(6, 4);
            this.picHelp.Name = "picHelp";
            this.picHelp.Size = new System.Drawing.Size(20, 20);
            this.picHelp.TabIndex = 92;
            this.picHelp.TabStop = false;
            this.toolTip1.SetToolTip(this.picHelp, "Help");
            this.picHelp.Click += new System.EventHandler(this.picHelp_Click);
            // 
            // lbIndicator
            // 
            this.lbIndicator.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbIndicator.Location = new System.Drawing.Point(0, 30);
            this.lbIndicator.Name = "lbIndicator";
            this.lbIndicator.Size = new System.Drawing.Size(20, 20);
            this.lbIndicator.TabIndex = 93;
            this.lbIndicator.Text = ">";
            this.lbIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlInputBox
            // 
            this.pnlInputBox.Controls.Add(this.lbOutput);
            this.pnlInputBox.Controls.Add(this.lbIndicator);
            this.pnlInputBox.Controls.Add(this.tbInput);
            this.pnlInputBox.Controls.Add(this.btnGo);
            this.pnlInputBox.Location = new System.Drawing.Point(3, 29);
            this.pnlInputBox.Name = "pnlInputBox";
            this.pnlInputBox.Size = new System.Drawing.Size(356, 54);
            this.pnlInputBox.TabIndex = 94;
            // 
            // lbClear
            // 
            this.lbClear.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbClear.Location = new System.Drawing.Point(272, 3);
            this.lbClear.Name = "lbClear";
            this.lbClear.Size = new System.Drawing.Size(20, 20);
            this.lbClear.TabIndex = 95;
            this.lbClear.Text = "C";
            this.lbClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbClear, "Clear (ESC)");
            this.lbClear.Click += new System.EventHandler(this.lbClear_Click);
            // 
            // lbRecallPrevious
            // 
            this.lbRecallPrevious.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbRecallPrevious.Location = new System.Drawing.Point(220, 3);
            this.lbRecallPrevious.Name = "lbRecallPrevious";
            this.lbRecallPrevious.Size = new System.Drawing.Size(20, 20);
            this.lbRecallPrevious.TabIndex = 96;
            this.lbRecallPrevious.Text = "<";
            this.lbRecallPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbRecallPrevious, "Recall History (Press UP)");
            this.lbRecallPrevious.Click += new System.EventHandler(this.lbRecallPrevious_Click);
            // 
            // lbRecallNext
            // 
            this.lbRecallNext.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbRecallNext.Location = new System.Drawing.Point(246, 3);
            this.lbRecallNext.Name = "lbRecallNext";
            this.lbRecallNext.Size = new System.Drawing.Size(20, 20);
            this.lbRecallNext.TabIndex = 97;
            this.lbRecallNext.Text = ">";
            this.lbRecallNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbRecallNext, "Recall History (Press Down)");
            this.lbRecallNext.Click += new System.EventHandler(this.lbRecallNext_Click);
            // 
            // lbCopy
            // 
            this.lbCopy.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbCopy.Location = new System.Drawing.Point(150, 3);
            this.lbCopy.Name = "lbCopy";
            this.lbCopy.Size = new System.Drawing.Size(64, 20);
            this.lbCopy.TabIndex = 98;
            this.lbCopy.Text = "Copy";
            this.lbCopy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbCopy, "Copy Result (Or Click Result Display)");
            this.lbCopy.Click += new System.EventHandler(this.lbCopy_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbTitle.Location = new System.Drawing.Point(26, 3);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(112, 20);
            this.lbTitle.TabIndex = 99;
            this.lbTitle.Text = "Calculator";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lbTitle, "Copy Result (Or Click Result Display)");
            // 
            // MicroCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lbCopy);
            this.Controls.Add(this.lbRecallNext);
            this.Controls.Add(this.lbRecallPrevious);
            this.Controls.Add(this.lbClear);
            this.Controls.Add(this.pnlInputBox);
            this.Controls.Add(this.picHelp);
            this.Controls.Add(this.lbClose);
            this.Name = "MicroCalculator";
            this.Size = new System.Drawing.Size(362, 86);
            this.contextMenuBasic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
            this.pnlInputBox.ResumeLayout(false);
            this.pnlInputBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lbOutput;
        private System.Windows.Forms.Label lbClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.PictureBox picHelp;
        private System.Windows.Forms.Label lbIndicator;
        private System.Windows.Forms.Panel pnlInputBox;
        private System.Windows.Forms.Label lbClear;
        private System.Windows.Forms.Label lbRecallPrevious;
        private System.Windows.Forms.Label lbRecallNext;
        private System.Windows.Forms.Label lbCopy;
        private System.Windows.Forms.Label lbTitle;
    }
}
