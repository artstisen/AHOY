namespace AhoyBrowser
{
    partial class AhoyInfoMessageBox
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
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbInformation = new AhoyBrowser.HistoryTextBox();
            this.btnToggleOptionA = new System.Windows.Forms.Button();
            this.contextMenuCopy = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuBasic.SuspendLayout();
            this.contextMenuCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnOK.Location = new System.Drawing.Point(467, 347);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // contextMenuBasic
            // 
            this.contextMenuBasic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1});
            this.contextMenuBasic.Name = "contextMenuBasic";
            this.contextMenuBasic.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.cutToolStripMenuItem1.Text = "Cu&t";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem1.Text = "&Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
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
            this.tbInformation.Location = new System.Drawing.Point(12, 23);
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.ReduceCache = false;
            this.tbInformation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbInformation.Size = new System.Drawing.Size(530, 318);
            this.tbInformation.TabIndex = 0;
            this.tbInformation.Text = "- Information -";
            this.tbInformation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInformation_KeyDown);
            // 
            // btnToggleOptionA
            // 
            this.btnToggleOptionA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleOptionA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToggleOptionA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnToggleOptionA.Location = new System.Drawing.Point(12, 347);
            this.btnToggleOptionA.Name = "btnToggleOptionA";
            this.btnToggleOptionA.Size = new System.Drawing.Size(241, 23);
            this.btnToggleOptionA.TabIndex = 2;
            this.btnToggleOptionA.Text = "Toggle";
            this.btnToggleOptionA.UseVisualStyleBackColor = true;
            this.btnToggleOptionA.Click += new System.EventHandler(this.btnToggleOptionA_Click);
            // 
            // contextMenuCopy
            // 
            this.contextMenuCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuCopy.Name = "contextMenuCopy";
            this.contextMenuCopy.Size = new System.Drawing.Size(181, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // AhoyInfoMessageBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(554, 381);
            this.Controls.Add(this.btnToggleOptionA);
            this.Controls.Add(this.tbInformation);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(570, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(570, 420);
            this.Name = "AhoyInfoMessageBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InfoForm_KeyPress);
            this.contextMenuBasic.ResumeLayout(false);
            this.contextMenuCopy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private AhoyBrowser.HistoryTextBox tbInformation;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
        private System.Windows.Forms.Button btnToggleOptionA;
        private AhoyContextMenuStrip contextMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}