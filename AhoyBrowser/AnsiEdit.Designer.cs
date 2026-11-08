namespace AhoyBrowser
{
    partial class AnsiEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnsiEditForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbInfo = new System.Windows.Forms.Label();
            this.AnsiEditMenuStrip = new AhoyBrowser.AhoyMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAndCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoX10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoX10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallCanvasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumCanvasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeCanvasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnInsert = new System.Windows.Forms.Button();
            this.timerLogo = new System.Windows.Forms.Timer(this.components);
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.lbUndoRedo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDeselect = new System.Windows.Forms.Button();
            this.btnRedoMore = new System.Windows.Forms.Button();
            this.btnUndoMore = new System.Windows.Forms.Button();
            this.btnCanvasSizeA = new System.Windows.Forms.Button();
            this.btnCanvasSizeB = new System.Windows.Forms.Button();
            this.btnCanvasSizeC = new System.Windows.Forms.Button();
            this.btnSelectAndCopy = new System.Windows.Forms.Button();
            this.lbPosition = new System.Windows.Forms.Label();
            this.tbCanvas = new AhoyBrowser.HistoryTextBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lbSplash = new System.Windows.Forms.Label();
            this.lbTimesTen = new System.Windows.Forms.Label();
            this.lbCanvasSize = new System.Windows.Forms.Label();
            this.timerSelectAndCopy = new System.Windows.Forms.Timer(this.components);
            this.lbSelectAndCopy = new System.Windows.Forms.Label();
            this.lbCanvas = new System.Windows.Forms.Label();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1.SuspendLayout();
            this.AnsiEditMenuStrip.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 659);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1034, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(39, 17);
            this.lbStatus.Text = "Ready";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnNext.Location = new System.Drawing.Point(3, 568);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "˄";
            this.toolTip1.SetToolTip(this.btnNext, "Next Preset (Control+Up)");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnPrevious.Location = new System.Drawing.Point(3, 597);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(35, 23);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Text = "˅";
            this.toolTip1.SetToolTip(this.btnPrevious, "Previous Preset (Ctrl+Down)");
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCopy.Location = new System.Drawing.Point(947, 572);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.TabStop = false;
            this.btnCopy.Text = "Copy";
            this.toolTip1.SetToolTip(this.btnCopy, "Copy (Ctrl+Shift+C)");
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnClear.Location = new System.Drawing.Point(947, 174);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Clear (ESC)");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbInfo
            // 
            this.tbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbInfo.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(175)))));
            this.tbInfo.Location = new System.Drawing.Point(42, 569);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(887, 50);
            this.tbInfo.TabIndex = 15;
            this.tbInfo.Text = "Ready";
            this.tbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AnsiEditMenuStrip
            // 
            this.AnsiEditMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.AnsiEditMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.AnsiEditMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AnsiEditMenuStrip.Name = "AnsiEditMenuStrip";
            this.AnsiEditMenuStrip.Size = new System.Drawing.Size(1034, 24);
            this.AnsiEditMenuStrip.TabIndex = 17;
            this.AnsiEditMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+W";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.copyContentToolStripMenuItem,
            this.toolStripMenuItem2,
            this.selectToolStripMenuItem,
            this.selectAndCopyToolStripMenuItem,
            this.deselectToolStripMenuItem,
            this.toolStripMenuItem4,
            this.undoToolStripMenuItem,
            this.undoX10ToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.redoX10ToolStripMenuItem,
            this.toolStripMenuItem5,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.moveLeftToolStripMenuItem,
            this.moveRightToolStripMenuItem,
            this.toolStripMenuItem3,
            this.clearToolStripMenuItem});
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.insertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.insertToolStripMenuItem.Text = "&Insert Content";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
            // 
            // copyContentToolStripMenuItem
            // 
            this.copyContentToolStripMenuItem.Name = "copyContentToolStripMenuItem";
            this.copyContentToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.copyContentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyContentToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.copyContentToolStripMenuItem.Text = "&Copy Content";
            this.copyContentToolStripMenuItem.Click += new System.EventHandler(this.copyContentToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(242, 6);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.selectToolStripMenuItem.Text = "&Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // selectAndCopyToolStripMenuItem
            // 
            this.selectAndCopyToolStripMenuItem.Name = "selectAndCopyToolStripMenuItem";
            this.selectAndCopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.selectAndCopyToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.selectAndCopyToolStripMenuItem.Text = "Select &And Copy";
            this.selectAndCopyToolStripMenuItem.Click += new System.EventHandler(this.selectAndCopyToolStripMenuItem_Click);
            // 
            // deselectToolStripMenuItem
            // 
            this.deselectToolStripMenuItem.Name = "deselectToolStripMenuItem";
            this.deselectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deselectToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.deselectToolStripMenuItem.Text = "&Deselect";
            this.deselectToolStripMenuItem.Click += new System.EventHandler(this.deselectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(242, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // undoX10ToolStripMenuItem
            // 
            this.undoX10ToolStripMenuItem.Name = "undoX10ToolStripMenuItem";
            this.undoX10ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.undoX10ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.undoX10ToolStripMenuItem.Text = "&Undo x10";
            this.undoX10ToolStripMenuItem.Click += new System.EventHandler(this.undoX10ToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // redoX10ToolStripMenuItem
            // 
            this.redoX10ToolStripMenuItem.Name = "redoX10ToolStripMenuItem";
            this.redoX10ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Y)));
            this.redoX10ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.redoX10ToolStripMenuItem.Text = "&Redo x10";
            this.redoX10ToolStripMenuItem.Click += new System.EventHandler(this.redoX10ToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.copyToolStripMenuItem.Text = "&Copy (Character)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.pasteToolStripMenuItem.Text = "&Paste (Character)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(242, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.moveUpToolStripMenuItem.Text = "Move Content Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.moveDownToolStripMenuItem.Text = "Move Content Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // moveLeftToolStripMenuItem
            // 
            this.moveLeftToolStripMenuItem.Name = "moveLeftToolStripMenuItem";
            this.moveLeftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.moveLeftToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.moveLeftToolStripMenuItem.Text = "Move Content Left";
            this.moveLeftToolStripMenuItem.Click += new System.EventHandler(this.moveLeftToolStripMenuItem_Click);
            // 
            // moveRightToolStripMenuItem
            // 
            this.moveRightToolStripMenuItem.Name = "moveRightToolStripMenuItem";
            this.moveRightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.moveRightToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.moveRightToolStripMenuItem.Text = "Move Content Right";
            this.moveRightToolStripMenuItem.Click += new System.EventHandler(this.moveRightToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(242, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeyDisplayString = "ESC";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.clearToolStripMenuItem.Text = "C&lear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallCanvasToolStripMenuItem,
            this.mediumCanvasToolStripMenuItem,
            this.largeCanvasToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // smallCanvasToolStripMenuItem
            // 
            this.smallCanvasToolStripMenuItem.Name = "smallCanvasToolStripMenuItem";
            this.smallCanvasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.smallCanvasToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.smallCanvasToolStripMenuItem.Text = "&Small Canvas";
            this.smallCanvasToolStripMenuItem.Click += new System.EventHandler(this.smallCanvasToolStripMenuItem_Click);
            // 
            // mediumCanvasToolStripMenuItem
            // 
            this.mediumCanvasToolStripMenuItem.Name = "mediumCanvasToolStripMenuItem";
            this.mediumCanvasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.mediumCanvasToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.mediumCanvasToolStripMenuItem.Text = "&Medium Canvas";
            this.mediumCanvasToolStripMenuItem.Click += new System.EventHandler(this.mediumCanvasToolStripMenuItem_Click);
            // 
            // largeCanvasToolStripMenuItem
            // 
            this.largeCanvasToolStripMenuItem.Name = "largeCanvasToolStripMenuItem";
            this.largeCanvasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.largeCanvasToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.largeCanvasToolStripMenuItem.Text = "&Large Canvas";
            this.largeCanvasToolStripMenuItem.Click += new System.EventHandler(this.largeCanvasToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lbInfo
            // 
            this.lbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lbInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbInfo.Location = new System.Drawing.Point(228, 6);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(488, 13);
            this.lbInfo.TabIndex = 14;
            this.lbInfo.Text = " - Info -";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnInsert.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInsert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnInsert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnInsert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnInsert.Location = new System.Drawing.Point(947, 145);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 16;
            this.btnInsert.TabStop = false;
            this.btnInsert.Text = "Insert";
            this.toolTip1.SetToolTip(this.btnInsert, "Insert (Ctrl+I)");
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // timerLogo
            // 
            this.timerLogo.Enabled = true;
            this.timerLogo.Interval = 2000;
            this.timerLogo.Tick += new System.EventHandler(this.timerLogo_Tick);
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUndo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnUndo.Location = new System.Drawing.Point(947, 239);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(35, 23);
            this.btnUndo.TabIndex = 18;
            this.btnUndo.TabStop = false;
            this.btnUndo.Text = "<";
            this.toolTip1.SetToolTip(this.btnUndo, "Undo (Ctrl+Z)");
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnRedo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnRedo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnRedo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRedo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnRedo.Location = new System.Drawing.Point(988, 239);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(35, 23);
            this.btnRedo.TabIndex = 19;
            this.btnRedo.TabStop = false;
            this.btnRedo.Text = ">";
            this.toolTip1.SetToolTip(this.btnRedo, "Redo (Ctrl+Y)");
            this.btnRedo.UseVisualStyleBackColor = false;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // lbUndoRedo
            // 
            this.lbUndoRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUndoRedo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbUndoRedo.Location = new System.Drawing.Point(947, 223);
            this.lbUndoRedo.Name = "lbUndoRedo";
            this.lbUndoRedo.Size = new System.Drawing.Size(75, 13);
            this.lbUndoRedo.TabIndex = 20;
            this.lbUndoRedo.Text = "Undo/Redo";
            this.lbUndoRedo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.toolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnMoveRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMoveRight.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnMoveRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMoveRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnMoveRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnMoveRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnMoveRight.Location = new System.Drawing.Point(988, 373);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(35, 23);
            this.btnMoveRight.TabIndex = 25;
            this.btnMoveRight.TabStop = false;
            this.btnMoveRight.Text = ">";
            this.toolTip1.SetToolTip(this.btnMoveRight, "Right (Alt+Right)");
            this.btnMoveRight.UseVisualStyleBackColor = false;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnMoveLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMoveLeft.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnMoveLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMoveLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnMoveLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnMoveLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnMoveLeft.Location = new System.Drawing.Point(947, 373);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(35, 23);
            this.btnMoveLeft.TabIndex = 24;
            this.btnMoveLeft.TabStop = false;
            this.btnMoveLeft.Text = "<";
            this.toolTip1.SetToolTip(this.btnMoveLeft, "Left (Alt+Left)");
            this.btnMoveLeft.UseVisualStyleBackColor = false;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnMoveUp.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMoveUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnMoveUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMoveUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnMoveUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnMoveUp.Location = new System.Drawing.Point(967, 344);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(35, 23);
            this.btnMoveUp.TabIndex = 26;
            this.btnMoveUp.TabStop = false;
            this.btnMoveUp.Text = "˄";
            this.toolTip1.SetToolTip(this.btnMoveUp, "Up (Alt+Up)");
            this.btnMoveUp.UseVisualStyleBackColor = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnMoveDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMoveDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnMoveDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMoveDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnMoveDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnMoveDown.Location = new System.Drawing.Point(967, 402);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(35, 23);
            this.btnMoveDown.TabIndex = 27;
            this.btnMoveDown.TabStop = false;
            this.btnMoveDown.Text = "˅";
            this.toolTip1.SetToolTip(this.btnMoveDown, "Down (Alt+Down)");
            this.btnMoveDown.UseVisualStyleBackColor = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnClose.Location = new System.Drawing.Point(947, 630);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 29;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Exit";
            this.toolTip1.SetToolTip(this.btnClose, "Exit (Ctrl+W)");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnSelect.Location = new System.Drawing.Point(947, 468);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 31;
            this.btnSelect.TabStop = false;
            this.btnSelect.Text = "Select";
            this.toolTip1.SetToolTip(this.btnSelect, "Select (CTRL+A)");
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.tbCrop_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnDeselect.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDeselect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnDeselect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDeselect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnDeselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnDeselect.Location = new System.Drawing.Point(947, 497);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(75, 23);
            this.btnDeselect.TabIndex = 32;
            this.btnDeselect.TabStop = false;
            this.btnDeselect.Text = "Deselect";
            this.toolTip1.SetToolTip(this.btnDeselect, "Deselect (CTRL+D)");
            this.btnDeselect.UseVisualStyleBackColor = false;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnRedoMore
            // 
            this.btnRedoMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedoMore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnRedoMore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedoMore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnRedoMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnRedoMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnRedoMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedoMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRedoMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnRedoMore.Location = new System.Drawing.Point(988, 281);
            this.btnRedoMore.Name = "btnRedoMore";
            this.btnRedoMore.Size = new System.Drawing.Size(35, 23);
            this.btnRedoMore.TabIndex = 34;
            this.btnRedoMore.TabStop = false;
            this.btnRedoMore.Text = ">";
            this.toolTip1.SetToolTip(this.btnRedoMore, "Redo x10 (CTRL+SHIFT+Y)");
            this.btnRedoMore.UseVisualStyleBackColor = false;
            this.btnRedoMore.Click += new System.EventHandler(this.btnRedoMore_Click);
            // 
            // btnUndoMore
            // 
            this.btnUndoMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndoMore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnUndoMore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUndoMore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnUndoMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnUndoMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnUndoMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndoMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnUndoMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnUndoMore.Location = new System.Drawing.Point(947, 281);
            this.btnUndoMore.Name = "btnUndoMore";
            this.btnUndoMore.Size = new System.Drawing.Size(35, 23);
            this.btnUndoMore.TabIndex = 33;
            this.btnUndoMore.TabStop = false;
            this.btnUndoMore.Text = "<";
            this.toolTip1.SetToolTip(this.btnUndoMore, "Undo x10 (CTRL+SHIFT+Z)");
            this.btnUndoMore.UseVisualStyleBackColor = false;
            this.btnUndoMore.Click += new System.EventHandler(this.btnUndoMore_Click);
            // 
            // btnCanvasSizeA
            // 
            this.btnCanvasSizeA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanvasSizeA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnCanvasSizeA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCanvasSizeA.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnCanvasSizeA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCanvasSizeA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCanvasSizeA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanvasSizeA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCanvasSizeA.Location = new System.Drawing.Point(947, 45);
            this.btnCanvasSizeA.Name = "btnCanvasSizeA";
            this.btnCanvasSizeA.Size = new System.Drawing.Size(75, 23);
            this.btnCanvasSizeA.TabIndex = 36;
            this.btnCanvasSizeA.TabStop = false;
            this.btnCanvasSizeA.Text = "[Small]";
            this.toolTip1.SetToolTip(this.btnCanvasSizeA, "Small Canvas (CTRL+1)");
            this.btnCanvasSizeA.UseVisualStyleBackColor = false;
            this.btnCanvasSizeA.Click += new System.EventHandler(this.btnCanvasSizeA_Click);
            // 
            // btnCanvasSizeB
            // 
            this.btnCanvasSizeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanvasSizeB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnCanvasSizeB.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCanvasSizeB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnCanvasSizeB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCanvasSizeB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCanvasSizeB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanvasSizeB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCanvasSizeB.Location = new System.Drawing.Point(948, 74);
            this.btnCanvasSizeB.Name = "btnCanvasSizeB";
            this.btnCanvasSizeB.Size = new System.Drawing.Size(75, 23);
            this.btnCanvasSizeB.TabIndex = 38;
            this.btnCanvasSizeB.TabStop = false;
            this.btnCanvasSizeB.Text = "Medium";
            this.toolTip1.SetToolTip(this.btnCanvasSizeB, "Medium Canvas (CTRL+2)");
            this.btnCanvasSizeB.UseVisualStyleBackColor = false;
            this.btnCanvasSizeB.Click += new System.EventHandler(this.btnCanvasSizeB_Click);
            // 
            // btnCanvasSizeC
            // 
            this.btnCanvasSizeC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanvasSizeC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnCanvasSizeC.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCanvasSizeC.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnCanvasSizeC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCanvasSizeC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCanvasSizeC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanvasSizeC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCanvasSizeC.Location = new System.Drawing.Point(947, 103);
            this.btnCanvasSizeC.Name = "btnCanvasSizeC";
            this.btnCanvasSizeC.Size = new System.Drawing.Size(75, 23);
            this.btnCanvasSizeC.TabIndex = 39;
            this.btnCanvasSizeC.TabStop = false;
            this.btnCanvasSizeC.Text = "Large";
            this.toolTip1.SetToolTip(this.btnCanvasSizeC, "Large Canvas (CTRL+3)");
            this.btnCanvasSizeC.UseVisualStyleBackColor = false;
            this.btnCanvasSizeC.Click += new System.EventHandler(this.btnCanvasSizeC_Click);
            // 
            // btnSelectAndCopy
            // 
            this.btnSelectAndCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAndCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnSelectAndCopy.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelectAndCopy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnSelectAndCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSelectAndCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelectAndCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAndCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnSelectAndCopy.Location = new System.Drawing.Point(947, 543);
            this.btnSelectAndCopy.Name = "btnSelectAndCopy";
            this.btnSelectAndCopy.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAndCopy.TabIndex = 40;
            this.btnSelectAndCopy.TabStop = false;
            this.btnSelectAndCopy.Text = "Sel+Cpy";
            this.toolTip1.SetToolTip(this.btnSelectAndCopy, "Select And Copy (CTRL+ALT+C)");
            this.btnSelectAndCopy.UseVisualStyleBackColor = false;
            this.btnSelectAndCopy.Click += new System.EventHandler(this.btnSelectAndCopy_Click);
            // 
            // lbPosition
            // 
            this.lbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbPosition.Location = new System.Drawing.Point(948, 328);
            this.lbPosition.Name = "lbPosition";
            this.lbPosition.Size = new System.Drawing.Size(75, 13);
            this.lbPosition.TabIndex = 28;
            this.lbPosition.Text = "Position";
            this.lbPosition.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbCanvas
            // 
            this.tbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCanvas.DetectUrls = false;
            this.tbCanvas.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCanvas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbCanvas.Location = new System.Drawing.Point(3, 3);
            this.tbCanvas.Name = "tbCanvas";
            this.tbCanvas.ReduceCache = false;
            this.tbCanvas.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbCanvas.Size = new System.Drawing.Size(926, 562);
            this.tbCanvas.TabIndex = 1;
            this.tbCanvas.Text = "";
            this.tbCanvas.WordWrap = false;
            this.tbCanvas.SelectionChanged += new System.EventHandler(this.tbCanvas_SelectionChanged);
            this.tbCanvas.Click += new System.EventHandler(this.tbCanvas_Click);
            this.tbCanvas.CursorChanged += new System.EventHandler(this.tbCanvas_CursorChanged);
            this.tbCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.tbCanvas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCanvas_KeyUp);
            this.tbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbCanvas_MouseUp);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.lbSplash);
            this.pnlContainer.Controls.Add(this.btnPrevious);
            this.pnlContainer.Controls.Add(this.btnNext);
            this.pnlContainer.Controls.Add(this.tbInfo);
            this.pnlContainer.Controls.Add(this.tbCanvas);
            this.pnlContainer.Location = new System.Drawing.Point(12, 30);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(929, 623);
            this.pnlContainer.TabIndex = 30;
            // 
            // lbSplash
            // 
            this.lbSplash.Location = new System.Drawing.Point(341, 193);
            this.lbSplash.Name = "lbSplash";
            this.lbSplash.Size = new System.Drawing.Size(260, 158);
            this.lbSplash.TabIndex = 16;
            this.lbSplash.Text = "Splash...";
            this.lbSplash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTimesTen
            // 
            this.lbTimesTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTimesTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbTimesTen.Location = new System.Drawing.Point(948, 265);
            this.lbTimesTen.Name = "lbTimesTen";
            this.lbTimesTen.Size = new System.Drawing.Size(74, 13);
            this.lbTimesTen.TabIndex = 35;
            this.lbTimesTen.Text = "x10";
            this.lbTimesTen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCanvasSize
            // 
            this.lbCanvasSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCanvasSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbCanvasSize.Location = new System.Drawing.Point(947, 29);
            this.lbCanvasSize.Name = "lbCanvasSize";
            this.lbCanvasSize.Size = new System.Drawing.Size(75, 13);
            this.lbCanvasSize.TabIndex = 37;
            this.lbCanvasSize.Text = "Canvas Size";
            this.lbCanvasSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerSelectAndCopy
            // 
            this.timerSelectAndCopy.Tick += new System.EventHandler(this.timerSelectAndCopy_Tick);
            // 
            // lbSelectAndCopy
            // 
            this.lbSelectAndCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelectAndCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbSelectAndCopy.Location = new System.Drawing.Point(947, 452);
            this.lbSelectAndCopy.Name = "lbSelectAndCopy";
            this.lbSelectAndCopy.Size = new System.Drawing.Size(75, 13);
            this.lbSelectAndCopy.TabIndex = 41;
            this.lbSelectAndCopy.Text = "Actions";
            this.lbSelectAndCopy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCanvas
            // 
            this.lbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCanvas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbCanvas.Location = new System.Drawing.Point(947, 129);
            this.lbCanvas.Name = "lbCanvas";
            this.lbCanvas.Size = new System.Drawing.Size(75, 13);
            this.lbCanvas.TabIndex = 42;
            this.lbCanvas.Text = "Canvas";
            this.lbCanvas.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(242, 6);
            // 
            // AnsiEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1034, 681);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.lbCanvas);
            this.Controls.Add(this.lbTimesTen);
            this.Controls.Add(this.lbSelectAndCopy);
            this.Controls.Add(this.lbUndoRedo);
            this.Controls.Add(this.btnSelectAndCopy);
            this.Controls.Add(this.btnRedoMore);
            this.Controls.Add(this.btnCanvasSizeC);
            this.Controls.Add(this.btnUndoMore);
            this.Controls.Add(this.btnCanvasSizeB);
            this.Controls.Add(this.lbCanvasSize);
            this.Controls.Add(this.btnCanvasSizeA);
            this.Controls.Add(this.btnDeselect);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbPosition);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AnsiEditMenuStrip);
            this.Controls.Add(this.pnlContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MainMenuStrip = this.AnsiEditMenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1050, 720);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1050, 720);
            this.Name = "AnsiEditForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANSI Edit v1.06";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.AnsiEditMenuStrip.ResumeLayout(false);
            this.AnsiEditMenuStrip.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AhoyBrowser.HistoryTextBox tbCanvas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label tbInfo;
        private AhoyMenuStrip AnsiEditMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Timer timerLogo;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem copyContentToolStripMenuItem;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lbUndoRedo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Label lbPosition;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDeselect;
        private System.Windows.Forms.Button btnRedoMore;
        private System.Windows.Forms.Button btnUndoMore;
        private System.Windows.Forms.Label lbTimesTen;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.Label lbSplash;
        private System.Windows.Forms.Button btnCanvasSizeA;
        private System.Windows.Forms.Label lbCanvasSize;
        private System.Windows.Forms.Button btnCanvasSizeB;
        private System.Windows.Forms.Button btnCanvasSizeC;
        private System.Windows.Forms.Button btnSelectAndCopy;
        private System.Windows.Forms.Timer timerSelectAndCopy;
        private System.Windows.Forms.Label lbSelectAndCopy;
        private System.Windows.Forms.Label lbCanvas;
        private System.Windows.Forms.ToolStripMenuItem selectAndCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallCanvasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumCanvasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeCanvasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoX10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoX10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    }
}

