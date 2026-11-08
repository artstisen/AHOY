namespace AhoyBrowser
{
    partial class RomanNumerals
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
            this.tbInput = new AhoyBrowser.HistoryTextBox();
            this.tbOutput = new AhoyBrowser.HistoryTextBox();
            this.tbInfo = new AhoyBrowser.HistoryTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.ahoyContextMenuStrip1 = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ahoyContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Location = new System.Drawing.Point(12, 12);
            this.tbInput.Name = "tbInput";
            this.tbInput.ReduceCache = false;
            this.tbInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbInput.Size = new System.Drawing.Size(434, 168);
            this.tbInput.TabIndex = 0;
            this.tbInput.Text = "";
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOutput.Location = new System.Drawing.Point(12, 186);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReduceCache = false;
            this.tbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(434, 335);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "";
            // 
            // tbInfo
            // 
            this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInfo.Location = new System.Drawing.Point(452, 12);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ReadOnly = true;
            this.tbInfo.ReduceCache = false;
            this.tbInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbInfo.Size = new System.Drawing.Size(370, 509);
            this.tbInfo.TabIndex = 2;
            this.tbInfo.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnClose.Location = new System.Drawing.Point(747, 527);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ahoyContextMenuStrip1
            // 
            this.ahoyContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.ahoyContextMenuStrip1.Name = "ahoyContextMenuStrip1";
            this.ahoyContextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // RomanNumerals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.tbInput);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(850, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(850, 600);
            this.Name = "RomanNumerals";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roman Numerals";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RomanNumerals_KeyPress);
            this.ahoyContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HistoryTextBox tbInput;
        private HistoryTextBox tbOutput;
        private HistoryTextBox tbInfo;
        private System.Windows.Forms.Button btnClose;
        private AhoyContextMenuStrip ahoyContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}