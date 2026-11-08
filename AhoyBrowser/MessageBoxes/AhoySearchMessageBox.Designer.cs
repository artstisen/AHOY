namespace AhoyBrowser
{
    partial class AhoySearchMessageBox
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lbContentSearch = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbSearchInContent = new System.Windows.Forms.TextBox();
            this.timerRefocusContentSearch = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbTopText = new System.Windows.Forms.Label();
            this.tbInformation = new AhoyBrowser.HistoryTextBox();
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ahoyContextMenuCopy = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuBasic.SuspendLayout();
            this.ahoyContextMenuCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnOK.Location = new System.Drawing.Point(597, 477);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Close";
            this.toolTip1.SetToolTip(this.btnOK, "Close (ESC)");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbContentSearch
            // 
            this.lbContentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbContentSearch.Location = new System.Drawing.Point(414, 17);
            this.lbContentSearch.Name = "lbContentSearch";
            this.lbContentSearch.Size = new System.Drawing.Size(71, 13);
            this.lbContentSearch.TabIndex = 26;
            this.lbContentSearch.Text = "Search";
            this.lbContentSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(597, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "Search";
            this.toolTip1.SetToolTip(this.btnSearch, "Search (CTRL+F)");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbSearchInContent
            // 
            this.tbSearchInContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchInContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSearchInContent.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearchInContent.Location = new System.Drawing.Point(491, 14);
            this.tbSearchInContent.Name = "tbSearchInContent";
            this.tbSearchInContent.Size = new System.Drawing.Size(100, 19);
            this.tbSearchInContent.TabIndex = 24;
            this.tbSearchInContent.Click += new System.EventHandler(this.tbSearchInContent_Click);
            this.tbSearchInContent.TextChanged += new System.EventHandler(this.tbSearchInContent_TextChanged);
            this.tbSearchInContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchInContent_KeyDown);
            // 
            // timerRefocusContentSearch
            // 
            this.timerRefocusContentSearch.Tick += new System.EventHandler(this.timerRefocusContentSearch_Tick);
            // 
            // lbTopText
            // 
            this.lbTopText.Location = new System.Drawing.Point(12, 7);
            this.lbTopText.Name = "lbTopText";
            this.lbTopText.Size = new System.Drawing.Size(396, 29);
            this.lbTopText.TabIndex = 27;
            this.lbTopText.Text = "--";
            this.lbTopText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbInformation
            // 
            this.tbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbInformation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInformation.ContextMenuStrip = this.contextMenuBasic;
            this.tbInformation.DetectUrls = false;
            this.tbInformation.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInformation.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbInformation.HideSelection = false;
            this.tbInformation.Location = new System.Drawing.Point(12, 41);
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.ReduceCache = false;
            this.tbInformation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbInformation.Size = new System.Drawing.Size(660, 430);
            this.tbInformation.TabIndex = 0;
            this.tbInformation.Text = "- Information -";
            this.tbInformation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInformation_KeyDown);
            // 
            // contextMenuBasic
            // 
            this.contextMenuBasic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.selectAllToolStripMenuItem});
            this.contextMenuBasic.Name = "contextMenuBasic";
            this.contextMenuBasic.Size = new System.Drawing.Size(123, 98);
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.cutToolStripMenuItem1.Text = "Cu&t";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem1.Text = "&Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // ahoyContextMenuCopy
            // 
            this.ahoyContextMenuCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.ahoyContextMenuCopy.Name = "ahoyContextMenuCopy";
            this.ahoyContextMenuCopy.Size = new System.Drawing.Size(181, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // AhoySearchMessageBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.lbTopText);
            this.Controls.Add(this.lbContentSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearchInContent);
            this.Controls.Add(this.tbInformation);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "AhoySearchMessageBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InfoForm_KeyPress);
            this.contextMenuBasic.ResumeLayout(false);
            this.ahoyContextMenuCopy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private AhoyBrowser.HistoryTextBox tbInformation;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
        private System.Windows.Forms.Label lbContentSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbSearchInContent;
        private System.Windows.Forms.Timer timerRefocusContentSearch;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.Label lbTopText;
        private AhoyContextMenuStrip ahoyContextMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}