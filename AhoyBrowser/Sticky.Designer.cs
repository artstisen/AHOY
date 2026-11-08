namespace AhoyBrowser
{
    partial class Sticky
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
            this.tbNote = new AhoyBrowser.HistoryTextBox();
            this.timerStickyTimer = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toUPPERCASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLowercaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.underlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbSplash = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMove = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnResize = new System.Windows.Forms.Button();
            this.btnTheme = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.sortSelectionAZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortSelectionZAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuBasic.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNote
            // 
            this.tbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNote.BackColor = System.Drawing.Color.White;
            this.tbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNote.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNote.Location = new System.Drawing.Point(7, 31);
            this.tbNote.Name = "tbNote";
            this.tbNote.ReduceCache = false;
            this.tbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbNote.ShowSelectionMargin = true;
            this.tbNote.Size = new System.Drawing.Size(270, 228);
            this.tbNote.TabIndex = 0;
            this.tbNote.TabStop = false;
            this.tbNote.Text = "";
            this.tbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.tbNote_LinkClicked);
            this.tbNote.Click += new System.EventHandler(this.tbNote_Click);
            this.tbNote.TextChanged += new System.EventHandler(this.tbNote_TextChanged);
            this.tbNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNote_KeyDown);
            this.tbNote.MouseEnter += new System.EventHandler(this.tbNote_MouseEnter);
            this.tbNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbNote_MouseUp);
            // 
            // timerStickyTimer
            // 
            this.timerStickyTimer.Enabled = true;
            this.timerStickyTimer.Interval = 500;
            this.timerStickyTimer.Tick += new System.EventHandler(this.timerStickyTimer_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(247, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 23);
            this.btnClose.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnClose, "Delete (ESC)");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            // 
            // contextMenuBasic
            // 
            this.contextMenuBasic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toUPPERCASEToolStripMenuItem,
            this.toLowercaseToolStripMenuItem,
            this.bulletToolStripMenuItem,
            this.underlineToolStripMenuItem,
            this.toolStripMenuItem3,
            this.sortSelectionAZToolStripMenuItem,
            this.sortSelectionZAToolStripMenuItem});
            this.contextMenuBasic.Name = "contextMenuBasic";
            this.contextMenuBasic.Size = new System.Drawing.Size(181, 264);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // toUPPERCASEToolStripMenuItem
            // 
            this.toUPPERCASEToolStripMenuItem.Name = "toUPPERCASEToolStripMenuItem";
            this.toUPPERCASEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toUPPERCASEToolStripMenuItem.Text = "&UPPERCASE";
            this.toUPPERCASEToolStripMenuItem.Click += new System.EventHandler(this.toUPPERCASEToolStripMenuItem_Click);
            // 
            // toLowercaseToolStripMenuItem
            // 
            this.toLowercaseToolStripMenuItem.Name = "toLowercaseToolStripMenuItem";
            this.toLowercaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toLowercaseToolStripMenuItem.Text = "lo&wercase";
            this.toLowercaseToolStripMenuItem.Click += new System.EventHandler(this.toLowercaseToolStripMenuItem_Click);
            // 
            // bulletToolStripMenuItem
            // 
            this.bulletToolStripMenuItem.Name = "bulletToolStripMenuItem";
            this.bulletToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bulletToolStripMenuItem.Text = "&Bullet";
            this.bulletToolStripMenuItem.Click += new System.EventHandler(this.bulletToolStripMenuItem_Click);
            // 
            // underlineToolStripMenuItem
            // 
            this.underlineToolStripMenuItem.Name = "underlineToolStripMenuItem";
            this.underlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.underlineToolStripMenuItem.Text = "&Underline";
            this.underlineToolStripMenuItem.Click += new System.EventHandler(this.underlineToolStripMenuItem_Click);
            // 
            // lbSplash
            // 
            this.lbSplash.Location = new System.Drawing.Point(141, 127);
            this.lbSplash.Name = "lbSplash";
            this.lbSplash.Size = new System.Drawing.Size(100, 23);
            this.lbSplash.TabIndex = 22;
            // 
            // btnMove
            // 
            this.btnMove.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnMove.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.btnMove.FlatAppearance.BorderSize = 0;
            this.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMove.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.Location = new System.Drawing.Point(2, 4);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(35, 23);
            this.btnMove.TabIndex = 23;
            this.toolTip1.SetToolTip(this.btnMove, "Move");
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbNote_MouseDown);
            this.btnMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbNote_MouseMove);
            this.btnMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbNote_MouseUp);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnHide.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHide.FlatAppearance.BorderSize = 0;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(206, 4);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(35, 23);
            this.btnHide.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnHide, "Toggle Top-Most - Right Click To Hide All");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            this.btnHide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnHide_MouseDown);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(43, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 23);
            this.btnNew.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btnNew, "New (CTRL+N) - Right Click To Hide All (CTRL+SHIFT+H)");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNew_MouseDown);
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.btnResize.FlatAppearance.BorderSize = 0;
            this.btnResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResize.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResize.Location = new System.Drawing.Point(247, 242);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(35, 23);
            this.btnResize.TabIndex = 26;
            this.toolTip1.SetToolTip(this.btnResize, "Resize");
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnResize_MouseDown);
            this.btnResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnResize_MouseMove);
            this.btnResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnResize_MouseUp);
            // 
            // btnTheme
            // 
            this.btnTheme.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnTheme.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTheme.FlatAppearance.BorderSize = 0;
            this.btnTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheme.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTheme.Location = new System.Drawing.Point(84, 4);
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Size = new System.Drawing.Size(35, 23);
            this.btnTheme.TabIndex = 27;
            this.toolTip1.SetToolTip(this.btnTheme, "Colors (Left/Right Click)");
            this.btnTheme.UseVisualStyleBackColor = true;
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            this.btnTheme.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTheme_MouseDown);
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.BackgroundImage = global::AhoyBrowser.Properties.Resources.clear;
            this.btnShow.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnShow.FlatAppearance.BorderSize = 0;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(165, 4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(35, 23);
            this.btnShow.TabIndex = 28;
            this.toolTip1.SetToolTip(this.btnShow, "Bring Stickies To Front - Right Click To Hide All");
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            this.btnShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShow_MouseDown);
            // 
            // sortSelectionAZToolStripMenuItem
            // 
            this.sortSelectionAZToolStripMenuItem.Name = "sortSelectionAZToolStripMenuItem";
            this.sortSelectionAZToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sortSelectionAZToolStripMenuItem.Text = "&Sort Selection A-Z";
            this.sortSelectionAZToolStripMenuItem.Click += new System.EventHandler(this.sortSelectionAZToolStripMenuItem_Click);
            // 
            // sortSelectionZAToolStripMenuItem
            // 
            this.sortSelectionZAToolStripMenuItem.Name = "sortSelectionZAToolStripMenuItem";
            this.sortSelectionZAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sortSelectionZAToolStripMenuItem.Text = "&Sort Selection Z-A";
            this.sortSelectionZAToolStripMenuItem.Click += new System.EventHandler(this.sortSelectionZAToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // Sticky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 266);
            this.ControlBox = false;
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnTheme);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.lbSplash);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbNote);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(8000, 8000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(275, 125);
            this.Name = "Sticky";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual; // Location is restored from the Pool
            this.Text = "Sticky";
            this.Activated += new System.EventHandler(this.Sticky_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sticky_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Sticky_ResizeEnd);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbNote_MouseUp);
            this.Move += new System.EventHandler(this.Sticky_Move);
            this.contextMenuBasic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HistoryTextBox tbNote;
        private System.Windows.Forms.Timer timerStickyTimer;
        private System.Windows.Forms.Button btnClose;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.Label lbSplash;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnTheme;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toUPPERCASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLowercaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem underlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sortSelectionAZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortSelectionZAToolStripMenuItem;
    }
}