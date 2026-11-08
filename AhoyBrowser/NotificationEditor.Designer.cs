namespace AhoyBrowser
{
    partial class NotificationEditor
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
            this.btnToggleState = new System.Windows.Forms.Button();
            this.btnMinutePlus5 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbYear = new System.Windows.Forms.Label();
            this.btnMinutePlus10 = new System.Windows.Forms.Button();
            this.btnHourPlus2 = new System.Windows.Forms.Button();
            this.btnYearMinus = new System.Windows.Forms.Button();
            this.btnYearPlus = new System.Windows.Forms.Button();
            this.btnMinutePlus15 = new System.Windows.Forms.Button();
            this.lbState = new System.Windows.Forms.Label();
            this.lbMonth = new System.Windows.Forms.Label();
            this.lbDay = new System.Windows.Forms.Label();
            this.btnMonthMinus = new System.Windows.Forms.Button();
            this.btnMonthPlus = new System.Windows.Forms.Button();
            this.btnDayMinus = new System.Windows.Forms.Button();
            this.btnDayPlus = new System.Windows.Forms.Button();
            this.lbMonthName = new System.Windows.Forms.Label();
            this.lbHour = new System.Windows.Forms.Label();
            this.lbMinute = new System.Windows.Forms.Label();
            this.btnHourMinus = new System.Windows.Forms.Button();
            this.btnHourPlus = new System.Windows.Forms.Button();
            this.btnMinutesMinus = new System.Windows.Forms.Button();
            this.btnMinutesPlus = new System.Windows.Forms.Button();
            this.lbTimeDivider = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbSystemTimeDisplay = new System.Windows.Forms.Label();
            this.lbSystemTimeTitle = new System.Windows.Forms.Label();
            this.btnDayPlus7 = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.lbUndoRedo = new System.Windows.Forms.Label();
            this.pnlDateTimeContainer = new System.Windows.Forms.Panel();
            this.timerSystemTime = new System.Windows.Forms.Timer(this.components);
            this.lbDayName = new System.Windows.Forms.Label();
            this.btnHourMinus2 = new System.Windows.Forms.Button();
            this.btnMinuteZero = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetContent = new System.Windows.Forms.Button();
            this.btnGetTitle = new System.Windows.Forms.Button();
            this.btnHourMinus4 = new System.Windows.Forms.Button();
            this.btnMinute30 = new System.Windows.Forms.Button();
            this.btnHourPlus4 = new System.Windows.Forms.Button();
            this.btnDayPlus14 = new System.Windows.Forms.Button();
            this.btnTimeZero = new System.Windows.Forms.Button();
            this.btnDayMinus7 = new System.Windows.Forms.Button();
            this.btnMonthMinus2 = new System.Windows.Forms.Button();
            this.btnMonthPlus2 = new System.Windows.Forms.Button();
            this.btnCallUp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.picHelp = new System.Windows.Forms.PictureBox();
            this.btnSnooze = new System.Windows.Forms.Button();
            this.btnToggleCaret = new System.Windows.Forms.Button();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnEnableRepeat = new System.Windows.Forms.Button();
            this.btnRepeatDaily = new System.Windows.Forms.Button();
            this.btnRepeatWeekly = new System.Windows.Forms.Button();
            this.btnDisableRepeat = new System.Windows.Forms.Button();
            this.btnCloseRepeat = new System.Windows.Forms.Button();
            this.btnRepeatCode = new System.Windows.Forms.Button();
            this.btnResetRepeatCode = new System.Windows.Forms.Button();
            this.lbIsoWeekNumber = new System.Windows.Forms.Label();
            this.timerBlinker = new System.Windows.Forms.Timer(this.components);
            this.timerReFocus = new System.Windows.Forms.Timer(this.components);
            this.lbDateTimeAnsi = new System.Windows.Forms.Label();
            this.pnlRepeat = new System.Windows.Forms.Panel();
            this.pnlRepeatTextField = new System.Windows.Forms.Panel();
            this.lbRepeat = new System.Windows.Forms.Label();
            this.tbMessage = new AhoyBrowser.HistoryTextBox();
            this.contextMenuBasic = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDateTimeContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
            this.pnlRepeat.SuspendLayout();
            this.pnlRepeatTextField.SuspendLayout();
            this.contextMenuBasic.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnToggleState
            // 
            this.btnToggleState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleState.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleState.Location = new System.Drawing.Point(267, 502);
            this.btnToggleState.Name = "btnToggleState";
            this.btnToggleState.Size = new System.Drawing.Size(170, 23);
            this.btnToggleState.TabIndex = 32;
            this.btnToggleState.Text = "Disabled";
            this.toolTip1.SetToolTip(this.btnToggleState, "Toggle State (CTRL+I)");
            this.btnToggleState.UseVisualStyleBackColor = true;
            this.btnToggleState.Click += new System.EventHandler(this.btnToggleState_Click);
            // 
            // btnMinutePlus5
            // 
            this.btnMinutePlus5.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinutePlus5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinutePlus5.Location = new System.Drawing.Point(483, 403);
            this.btnMinutePlus5.Name = "btnMinutePlus5";
            this.btnMinutePlus5.Size = new System.Drawing.Size(35, 23);
            this.btnMinutePlus5.TabIndex = 22;
            this.btnMinutePlus5.Text = "+5";
            this.toolTip1.SetToolTip(this.btnMinutePlus5, "Add 5 Minutes");
            this.btnMinutePlus5.UseVisualStyleBackColor = true;
            this.btnMinutePlus5.Click += new System.EventHandler(this.btnMinutePlus5_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(443, 502);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.toolTip1.SetToolTip(this.btnClose, "Close/Submit (F5)");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbYear
            // 
            this.lbYear.BackColor = System.Drawing.Color.Transparent;
            this.lbYear.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbYear.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbYear.Location = new System.Drawing.Point(-2, 2);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(130, 69);
            this.lbYear.TabIndex = 15;
            this.lbYear.Text = "2016";
            this.lbYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbYear, "Left/Right Click To Change");
            this.lbYear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbYear_MouseClick);
            // 
            // btnMinutePlus10
            // 
            this.btnMinutePlus10.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinutePlus10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinutePlus10.Location = new System.Drawing.Point(483, 432);
            this.btnMinutePlus10.Name = "btnMinutePlus10";
            this.btnMinutePlus10.Size = new System.Drawing.Size(35, 23);
            this.btnMinutePlus10.TabIndex = 27;
            this.btnMinutePlus10.Text = "+10";
            this.toolTip1.SetToolTip(this.btnMinutePlus10, "Add 10 Minutes");
            this.btnMinutePlus10.UseVisualStyleBackColor = true;
            this.btnMinutePlus10.Click += new System.EventHandler(this.btnMinutePlus10_Click);
            // 
            // btnHourPlus2
            // 
            this.btnHourPlus2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourPlus2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourPlus2.Location = new System.Drawing.Point(401, 403);
            this.btnHourPlus2.Name = "btnHourPlus2";
            this.btnHourPlus2.Size = new System.Drawing.Size(35, 23);
            this.btnHourPlus2.TabIndex = 20;
            this.btnHourPlus2.Text = "+2";
            this.toolTip1.SetToolTip(this.btnHourPlus2, "Add 2 Hours");
            this.btnHourPlus2.UseVisualStyleBackColor = true;
            this.btnHourPlus2.Click += new System.EventHandler(this.btnHourPlus2_Click);
            // 
            // btnYearMinus
            // 
            this.btnYearMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnYearMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYearMinus.Location = new System.Drawing.Point(33, 374);
            this.btnYearMinus.Name = "btnYearMinus";
            this.btnYearMinus.Size = new System.Drawing.Size(35, 23);
            this.btnYearMinus.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnYearMinus, "Previous Year");
            this.btnYearMinus.UseVisualStyleBackColor = true;
            this.btnYearMinus.Click += new System.EventHandler(this.btnYearMinus_Click);
            // 
            // btnYearPlus
            // 
            this.btnYearPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnYearPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYearPlus.Location = new System.Drawing.Point(74, 374);
            this.btnYearPlus.Name = "btnYearPlus";
            this.btnYearPlus.Size = new System.Drawing.Size(35, 23);
            this.btnYearPlus.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnYearPlus, "Next Year");
            this.btnYearPlus.UseVisualStyleBackColor = true;
            this.btnYearPlus.Click += new System.EventHandler(this.btnYearPlus_Click);
            // 
            // btnMinutePlus15
            // 
            this.btnMinutePlus15.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinutePlus15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinutePlus15.Location = new System.Drawing.Point(483, 461);
            this.btnMinutePlus15.Name = "btnMinutePlus15";
            this.btnMinutePlus15.Size = new System.Drawing.Size(35, 23);
            this.btnMinutePlus15.TabIndex = 29;
            this.btnMinutePlus15.Text = "+15";
            this.toolTip1.SetToolTip(this.btnMinutePlus15, "Add 15 Minutes");
            this.btnMinutePlus15.UseVisualStyleBackColor = true;
            this.btnMinutePlus15.Click += new System.EventHandler(this.btnMinutePlus15_Click);
            // 
            // lbState
            // 
            this.lbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbState.Location = new System.Drawing.Point(264, 486);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(173, 13);
            this.lbState.TabIndex = 44;
            this.lbState.Text = "Notification State";
            this.lbState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMonth
            // 
            this.lbMonth.BackColor = System.Drawing.Color.Transparent;
            this.lbMonth.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbMonth.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMonth.Location = new System.Drawing.Point(134, 2);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(75, 69);
            this.lbMonth.TabIndex = 45;
            this.lbMonth.Text = "02";
            this.lbMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbMonth, "Left/Right Click To Change");
            this.lbMonth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMonth_MouseClick);
            // 
            // lbDay
            // 
            this.lbDay.BackColor = System.Drawing.Color.Transparent;
            this.lbDay.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbDay.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.Location = new System.Drawing.Point(215, 2);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(75, 69);
            this.lbDay.TabIndex = 46;
            this.lbDay.Text = "21";
            this.lbDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbDay, "Left/Right Click To Change");
            this.lbDay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbDay_MouseClick);
            // 
            // btnMonthMinus
            // 
            this.btnMonthMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMonthMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthMinus.Location = new System.Drawing.Point(143, 374);
            this.btnMonthMinus.Name = "btnMonthMinus";
            this.btnMonthMinus.Size = new System.Drawing.Size(35, 23);
            this.btnMonthMinus.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnMonthMinus, "Previous Month");
            this.btnMonthMinus.UseVisualStyleBackColor = true;
            this.btnMonthMinus.Click += new System.EventHandler(this.btnMonthMinus_Click);
            // 
            // btnMonthPlus
            // 
            this.btnMonthPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMonthPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthPlus.Location = new System.Drawing.Point(184, 374);
            this.btnMonthPlus.Name = "btnMonthPlus";
            this.btnMonthPlus.Size = new System.Drawing.Size(35, 23);
            this.btnMonthPlus.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnMonthPlus, "Next Month");
            this.btnMonthPlus.UseVisualStyleBackColor = true;
            this.btnMonthPlus.Click += new System.EventHandler(this.btnMonthPlus_Click);
            // 
            // btnDayMinus
            // 
            this.btnDayMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDayMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayMinus.Location = new System.Drawing.Point(225, 374);
            this.btnDayMinus.Name = "btnDayMinus";
            this.btnDayMinus.Size = new System.Drawing.Size(35, 23);
            this.btnDayMinus.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnDayMinus, "Previous Day");
            this.btnDayMinus.UseVisualStyleBackColor = true;
            this.btnDayMinus.Click += new System.EventHandler(this.btnDayMinus_Click);
            // 
            // btnDayPlus
            // 
            this.btnDayPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDayPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayPlus.Location = new System.Drawing.Point(266, 374);
            this.btnDayPlus.Name = "btnDayPlus";
            this.btnDayPlus.Size = new System.Drawing.Size(35, 23);
            this.btnDayPlus.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnDayPlus, "Next Day");
            this.btnDayPlus.UseVisualStyleBackColor = true;
            this.btnDayPlus.Click += new System.EventHandler(this.btnDayPlus_Click);
            // 
            // lbMonthName
            // 
            this.lbMonthName.BackColor = System.Drawing.Color.Transparent;
            this.lbMonthName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMonthName.Location = new System.Drawing.Point(136, 264);
            this.lbMonthName.Name = "lbMonthName";
            this.lbMonthName.Size = new System.Drawing.Size(92, 26);
            this.lbMonthName.TabIndex = 51;
            this.lbMonthName.Text = "Feburary";
            this.lbMonthName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHour
            // 
            this.lbHour.BackColor = System.Drawing.Color.Transparent;
            this.lbHour.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbHour.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHour.Location = new System.Drawing.Point(347, 2);
            this.lbHour.Name = "lbHour";
            this.lbHour.Size = new System.Drawing.Size(75, 69);
            this.lbHour.TabIndex = 52;
            this.lbHour.Text = "24";
            this.lbHour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbHour, "Left/Right Click To Change");
            this.lbHour.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbHour_MouseClick);
            // 
            // lbMinute
            // 
            this.lbMinute.BackColor = System.Drawing.Color.Transparent;
            this.lbMinute.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbMinute.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinute.Location = new System.Drawing.Point(436, 2);
            this.lbMinute.Name = "lbMinute";
            this.lbMinute.Size = new System.Drawing.Size(75, 69);
            this.lbMinute.TabIndex = 53;
            this.lbMinute.Text = "59";
            this.lbMinute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbMinute, "Left/Right Click To Change");
            this.lbMinute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMinute_MouseClick);
            // 
            // btnHourMinus
            // 
            this.btnHourMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourMinus.Location = new System.Drawing.Point(361, 374);
            this.btnHourMinus.Name = "btnHourMinus";
            this.btnHourMinus.Size = new System.Drawing.Size(35, 23);
            this.btnHourMinus.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnHourMinus, "Previous Hour");
            this.btnHourMinus.UseVisualStyleBackColor = true;
            this.btnHourMinus.Click += new System.EventHandler(this.btnHourMinus_Click);
            // 
            // btnHourPlus
            // 
            this.btnHourPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourPlus.Location = new System.Drawing.Point(402, 374);
            this.btnHourPlus.Name = "btnHourPlus";
            this.btnHourPlus.Size = new System.Drawing.Size(35, 23);
            this.btnHourPlus.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btnHourPlus, "Next Hour");
            this.btnHourPlus.UseVisualStyleBackColor = true;
            this.btnHourPlus.Click += new System.EventHandler(this.btnHourPlus_Click);
            // 
            // btnMinutesMinus
            // 
            this.btnMinutesMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinutesMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinutesMinus.Location = new System.Drawing.Point(442, 374);
            this.btnMinutesMinus.Name = "btnMinutesMinus";
            this.btnMinutesMinus.Size = new System.Drawing.Size(35, 23);
            this.btnMinutesMinus.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnMinutesMinus, "Previous Minute");
            this.btnMinutesMinus.UseVisualStyleBackColor = true;
            this.btnMinutesMinus.Click += new System.EventHandler(this.btnMinutesMinus_Click);
            // 
            // btnMinutesPlus
            // 
            this.btnMinutesPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinutesPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinutesPlus.Location = new System.Drawing.Point(483, 374);
            this.btnMinutesPlus.Name = "btnMinutesPlus";
            this.btnMinutesPlus.Size = new System.Drawing.Size(35, 23);
            this.btnMinutesPlus.TabIndex = 13;
            this.toolTip1.SetToolTip(this.btnMinutesPlus, "Next Minute");
            this.btnMinutesPlus.UseVisualStyleBackColor = true;
            this.btnMinutesPlus.Click += new System.EventHandler(this.btnMinutesPlus_Click);
            // 
            // lbTimeDivider
            // 
            this.lbTimeDivider.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeDivider.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimeDivider.Location = new System.Drawing.Point(414, 2);
            this.lbTimeDivider.Name = "lbTimeDivider";
            this.lbTimeDivider.Size = new System.Drawing.Size(29, 69);
            this.lbTimeDivider.TabIndex = 58;
            this.lbTimeDivider.Text = ":";
            this.lbTimeDivider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(33, 403);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(76, 23);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Now";
            this.toolTip1.SetToolTip(this.btnReset, "Set Date Time Now");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbTitle.Location = new System.Drawing.Point(39, 17);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(326, 13);
            this.lbTitle.TabIndex = 66;
            this.lbTitle.Text = "Message";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSystemTimeDisplay
            // 
            this.lbSystemTimeDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lbSystemTimeDisplay.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSystemTimeDisplay.Location = new System.Drawing.Point(30, 458);
            this.lbSystemTimeDisplay.Name = "lbSystemTimeDisplay";
            this.lbSystemTimeDisplay.Size = new System.Drawing.Size(230, 23);
            this.lbSystemTimeDisplay.TabIndex = 68;
            this.lbSystemTimeDisplay.Text = "0000-00-00   00:00:00";
            this.lbSystemTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSystemTimeTitle
            // 
            this.lbSystemTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbSystemTimeTitle.Location = new System.Drawing.Point(30, 441);
            this.lbSystemTimeTitle.Name = "lbSystemTimeTitle";
            this.lbSystemTimeTitle.Size = new System.Drawing.Size(114, 13);
            this.lbSystemTimeTitle.TabIndex = 69;
            this.lbSystemTimeTitle.Text = "System Time";
            this.lbSystemTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDayPlus7
            // 
            this.btnDayPlus7.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDayPlus7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayPlus7.Location = new System.Drawing.Point(266, 403);
            this.btnDayPlus7.Name = "btnDayPlus7";
            this.btnDayPlus7.Size = new System.Drawing.Size(35, 23);
            this.btnDayPlus7.TabIndex = 18;
            this.btnDayPlus7.Text = "+7";
            this.toolTip1.SetToolTip(this.btnDayPlus7, "Add 7 Days");
            this.btnDayPlus7.UseVisualStyleBackColor = true;
            this.btnDayPlus7.Click += new System.EventHandler(this.btnDayPlus7_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Location = new System.Drawing.Point(447, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(35, 23);
            this.btnUndo.TabIndex = 34;
            this.btnUndo.Text = "<";
            this.toolTip1.SetToolTip(this.btnUndo, "Undo (CTRL+Z)");
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedo.Location = new System.Drawing.Point(487, 12);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(35, 23);
            this.btnRedo.TabIndex = 35;
            this.btnRedo.Text = ">";
            this.toolTip1.SetToolTip(this.btnRedo, "Redo (CTRL+Y)");
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // lbUndoRedo
            // 
            this.lbUndoRedo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbUndoRedo.Location = new System.Drawing.Point(371, 17);
            this.lbUndoRedo.Name = "lbUndoRedo";
            this.lbUndoRedo.Size = new System.Drawing.Size(70, 13);
            this.lbUndoRedo.TabIndex = 73;
            this.lbUndoRedo.Text = "Undo/Redo";
            this.lbUndoRedo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDateTimeContainer
            // 
            this.pnlDateTimeContainer.Controls.Add(this.lbTimeDivider);
            this.pnlDateTimeContainer.Controls.Add(this.lbYear);
            this.pnlDateTimeContainer.Controls.Add(this.lbMonth);
            this.pnlDateTimeContainer.Controls.Add(this.lbDay);
            this.pnlDateTimeContainer.Controls.Add(this.lbHour);
            this.pnlDateTimeContainer.Controls.Add(this.lbMinute);
            this.pnlDateTimeContainer.Location = new System.Drawing.Point(12, 293);
            this.pnlDateTimeContainer.Name = "pnlDateTimeContainer";
            this.pnlDateTimeContainer.Size = new System.Drawing.Size(510, 75);
            this.pnlDateTimeContainer.TabIndex = 77;
            // 
            // timerSystemTime
            // 
            this.timerSystemTime.Interval = 1000;
            this.timerSystemTime.Tick += new System.EventHandler(this.timerSystemTime_Tick);
            // 
            // lbDayName
            // 
            this.lbDayName.BackColor = System.Drawing.Color.Transparent;
            this.lbDayName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDayName.Location = new System.Drawing.Point(227, 264);
            this.lbDayName.Name = "lbDayName";
            this.lbDayName.Size = new System.Drawing.Size(91, 26);
            this.lbDayName.TabIndex = 78;
            this.lbDayName.Text = "Wednesday";
            this.lbDayName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHourMinus2
            // 
            this.btnHourMinus2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourMinus2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourMinus2.Location = new System.Drawing.Point(361, 403);
            this.btnHourMinus2.Name = "btnHourMinus2";
            this.btnHourMinus2.Size = new System.Drawing.Size(35, 23);
            this.btnHourMinus2.TabIndex = 19;
            this.btnHourMinus2.Text = "-2";
            this.toolTip1.SetToolTip(this.btnHourMinus2, "Subtract 2 Hours");
            this.btnHourMinus2.UseVisualStyleBackColor = true;
            this.btnHourMinus2.Click += new System.EventHandler(this.btnHourMinus2_Click);
            // 
            // btnMinuteZero
            // 
            this.btnMinuteZero.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinuteZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinuteZero.Location = new System.Drawing.Point(442, 403);
            this.btnMinuteZero.Name = "btnMinuteZero";
            this.btnMinuteZero.Size = new System.Drawing.Size(35, 23);
            this.btnMinuteZero.TabIndex = 21;
            this.btnMinuteZero.Text = "00";
            this.toolTip1.SetToolTip(this.btnMinuteZero, "Set Zero Minutes");
            this.btnMinuteZero.UseVisualStyleBackColor = true;
            this.btnMinuteZero.Click += new System.EventHandler(this.btnMinuteZero_Click);
            // 
            // btnGetContent
            // 
            this.btnGetContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGetContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetContent.Location = new System.Drawing.Point(54, 264);
            this.btnGetContent.Name = "btnGetContent";
            this.btnGetContent.Size = new System.Drawing.Size(35, 23);
            this.btnGetContent.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnGetContent, "Insert Content");
            this.btnGetContent.UseVisualStyleBackColor = true;
            this.btnGetContent.Click += new System.EventHandler(this.btnGetContent_Click);
            // 
            // btnGetTitle
            // 
            this.btnGetTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGetTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetTitle.Location = new System.Drawing.Point(13, 264);
            this.btnGetTitle.Name = "btnGetTitle";
            this.btnGetTitle.Size = new System.Drawing.Size(35, 23);
            this.btnGetTitle.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnGetTitle, "Insert Title");
            this.btnGetTitle.UseVisualStyleBackColor = true;
            this.btnGetTitle.Click += new System.EventHandler(this.btnGetTitle_Click);
            // 
            // btnHourMinus4
            // 
            this.btnHourMinus4.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourMinus4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourMinus4.Location = new System.Drawing.Point(361, 432);
            this.btnHourMinus4.Name = "btnHourMinus4";
            this.btnHourMinus4.Size = new System.Drawing.Size(35, 23);
            this.btnHourMinus4.TabIndex = 24;
            this.btnHourMinus4.Text = "-4";
            this.toolTip1.SetToolTip(this.btnHourMinus4, "Subtract 4 Hours");
            this.btnHourMinus4.UseVisualStyleBackColor = true;
            this.btnHourMinus4.Click += new System.EventHandler(this.btnHourMinus4_Click);
            // 
            // btnMinute30
            // 
            this.btnMinute30.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMinute30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinute30.Location = new System.Drawing.Point(442, 432);
            this.btnMinute30.Name = "btnMinute30";
            this.btnMinute30.Size = new System.Drawing.Size(35, 23);
            this.btnMinute30.TabIndex = 26;
            this.btnMinute30.Text = "30";
            this.toolTip1.SetToolTip(this.btnMinute30, "Set 30 Minutes");
            this.btnMinute30.UseVisualStyleBackColor = true;
            this.btnMinute30.Click += new System.EventHandler(this.btnMinute30_Click);
            // 
            // btnHourPlus4
            // 
            this.btnHourPlus4.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHourPlus4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHourPlus4.Location = new System.Drawing.Point(402, 432);
            this.btnHourPlus4.Name = "btnHourPlus4";
            this.btnHourPlus4.Size = new System.Drawing.Size(35, 23);
            this.btnHourPlus4.TabIndex = 25;
            this.btnHourPlus4.Text = "+4";
            this.toolTip1.SetToolTip(this.btnHourPlus4, "Add 4 Hours");
            this.btnHourPlus4.UseVisualStyleBackColor = true;
            this.btnHourPlus4.Click += new System.EventHandler(this.btnHourPlus4_Click);
            // 
            // btnDayPlus14
            // 
            this.btnDayPlus14.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDayPlus14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayPlus14.Location = new System.Drawing.Point(266, 432);
            this.btnDayPlus14.Name = "btnDayPlus14";
            this.btnDayPlus14.Size = new System.Drawing.Size(35, 23);
            this.btnDayPlus14.TabIndex = 23;
            this.btnDayPlus14.Text = "+14";
            this.toolTip1.SetToolTip(this.btnDayPlus14, "Add 14 Days");
            this.btnDayPlus14.UseVisualStyleBackColor = true;
            this.btnDayPlus14.Click += new System.EventHandler(this.btnDayPlus14_Click);
            // 
            // btnTimeZero
            // 
            this.btnTimeZero.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTimeZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimeZero.Location = new System.Drawing.Point(320, 374);
            this.btnTimeZero.Name = "btnTimeZero";
            this.btnTimeZero.Size = new System.Drawing.Size(35, 23);
            this.btnTimeZero.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnTimeZero, "Set Zero Time (00:00)");
            this.btnTimeZero.UseVisualStyleBackColor = true;
            this.btnTimeZero.Click += new System.EventHandler(this.btnTimeZero_Click);
            // 
            // btnDayMinus7
            // 
            this.btnDayMinus7.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDayMinus7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayMinus7.Location = new System.Drawing.Point(225, 403);
            this.btnDayMinus7.Name = "btnDayMinus7";
            this.btnDayMinus7.Size = new System.Drawing.Size(35, 23);
            this.btnDayMinus7.TabIndex = 17;
            this.btnDayMinus7.Text = "-7";
            this.toolTip1.SetToolTip(this.btnDayMinus7, "Subtract 7 Days");
            this.btnDayMinus7.UseVisualStyleBackColor = true;
            this.btnDayMinus7.Click += new System.EventHandler(this.btnDayMinus7_Click);
            // 
            // btnMonthMinus2
            // 
            this.btnMonthMinus2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMonthMinus2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthMinus2.Location = new System.Drawing.Point(143, 403);
            this.btnMonthMinus2.Name = "btnMonthMinus2";
            this.btnMonthMinus2.Size = new System.Drawing.Size(35, 23);
            this.btnMonthMinus2.TabIndex = 15;
            this.btnMonthMinus2.Text = "-2";
            this.toolTip1.SetToolTip(this.btnMonthMinus2, "Subtract 2 Months");
            this.btnMonthMinus2.UseVisualStyleBackColor = true;
            this.btnMonthMinus2.Click += new System.EventHandler(this.btnMonthMinus2_Click);
            // 
            // btnMonthPlus2
            // 
            this.btnMonthPlus2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMonthPlus2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthPlus2.Location = new System.Drawing.Point(184, 403);
            this.btnMonthPlus2.Name = "btnMonthPlus2";
            this.btnMonthPlus2.Size = new System.Drawing.Size(35, 23);
            this.btnMonthPlus2.TabIndex = 16;
            this.btnMonthPlus2.Text = "+2";
            this.toolTip1.SetToolTip(this.btnMonthPlus2, "Add 2 Months");
            this.btnMonthPlus2.UseVisualStyleBackColor = true;
            this.btnMonthPlus2.Click += new System.EventHandler(this.btnMonthPlus2_Click);
            // 
            // btnCallUp
            // 
            this.btnCallUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallUp.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCallUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCallUp.Location = new System.Drawing.Point(186, 502);
            this.btnCallUp.Name = "btnCallUp";
            this.btnCallUp.Size = new System.Drawing.Size(75, 23);
            this.btnCallUp.TabIndex = 31;
            this.btnCallUp.Text = "Call-Up";
            this.toolTip1.SetToolTip(this.btnCallUp, "Show Linked Entry (F8)");
            this.btnCallUp.UseVisualStyleBackColor = true;
            this.btnCallUp.Click += new System.EventHandler(this.btnCallUp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(13, 502);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel (ESC)");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picHelp
            // 
            this.picHelp.BackColor = System.Drawing.Color.Transparent;
            this.picHelp.BackgroundImage = global::AhoyBrowser.Properties.Resources.help;
            this.picHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picHelp.Cursor = System.Windows.Forms.Cursors.Default;
            this.picHelp.Location = new System.Drawing.Point(13, 13);
            this.picHelp.Name = "picHelp";
            this.picHelp.Size = new System.Drawing.Size(20, 20);
            this.picHelp.TabIndex = 90;
            this.picHelp.TabStop = false;
            this.toolTip1.SetToolTip(this.picHelp, "Help");
            this.picHelp.Click += new System.EventHandler(this.picHelp_Click);
            // 
            // btnSnooze
            // 
            this.btnSnooze.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSnooze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnooze.Location = new System.Drawing.Point(361, 461);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.Size = new System.Drawing.Size(116, 23);
            this.btnSnooze.TabIndex = 91;
            this.btnSnooze.Text = "Snooze 25";
            this.toolTip1.SetToolTip(this.btnSnooze, "Close and Snooze (F4) - Right Click To Change");
            this.btnSnooze.UseVisualStyleBackColor = true;
            this.btnSnooze.Click += new System.EventHandler(this.btnSnooze_Click);
            this.btnSnooze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSnooze_MouseDown);
            // 
            // btnToggleCaret
            // 
            this.btnToggleCaret.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleCaret.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleCaret.Location = new System.Drawing.Point(95, 264);
            this.btnToggleCaret.Name = "btnToggleCaret";
            this.btnToggleCaret.Size = new System.Drawing.Size(35, 23);
            this.btnToggleCaret.TabIndex = 92;
            this.toolTip1.SetToolTip(this.btnToggleCaret, "Toggle AutoScroll");
            this.btnToggleCaret.UseVisualStyleBackColor = true;
            this.btnToggleCaret.Click += new System.EventHandler(this.btnToggleCaret_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeat.Location = new System.Drawing.Point(320, 403);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(35, 23);
            this.btnRepeat.TabIndex = 95;
            this.btnRepeat.Text = "R";
            this.toolTip1.SetToolTip(this.btnRepeat, "Repeat");
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnEnableRepeat
            // 
            this.btnEnableRepeat.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEnableRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnableRepeat.Location = new System.Drawing.Point(316, 131);
            this.btnEnableRepeat.Name = "btnEnableRepeat";
            this.btnEnableRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnEnableRepeat.TabIndex = 97;
            this.btnEnableRepeat.Text = "Enable";
            this.toolTip1.SetToolTip(this.btnEnableRepeat, "Enable");
            this.btnEnableRepeat.UseVisualStyleBackColor = true;
            this.btnEnableRepeat.Click += new System.EventHandler(this.btnEnableRepeat_Click);
            // 
            // btnRepeatDaily
            // 
            this.btnRepeatDaily.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRepeatDaily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeatDaily.Location = new System.Drawing.Point(3, 102);
            this.btnRepeatDaily.Name = "btnRepeatDaily";
            this.btnRepeatDaily.Size = new System.Drawing.Size(75, 23);
            this.btnRepeatDaily.TabIndex = 98;
            this.btnRepeatDaily.Text = "Daily";
            this.toolTip1.SetToolTip(this.btnRepeatDaily, "Daily");
            this.btnRepeatDaily.UseVisualStyleBackColor = true;
            this.btnRepeatDaily.Click += new System.EventHandler(this.lbRepeatDaily_Click);
            // 
            // btnRepeatWeekly
            // 
            this.btnRepeatWeekly.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRepeatWeekly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeatWeekly.Location = new System.Drawing.Point(84, 102);
            this.btnRepeatWeekly.Name = "btnRepeatWeekly";
            this.btnRepeatWeekly.Size = new System.Drawing.Size(75, 23);
            this.btnRepeatWeekly.TabIndex = 99;
            this.btnRepeatWeekly.Text = "Weekly";
            this.toolTip1.SetToolTip(this.btnRepeatWeekly, "Weekly");
            this.btnRepeatWeekly.UseVisualStyleBackColor = true;
            this.btnRepeatWeekly.Click += new System.EventHandler(this.btnRepeatWeekly_Click);
            // 
            // btnDisableRepeat
            // 
            this.btnDisableRepeat.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDisableRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableRepeat.Location = new System.Drawing.Point(235, 131);
            this.btnDisableRepeat.Name = "btnDisableRepeat";
            this.btnDisableRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnDisableRepeat.TabIndex = 101;
            this.btnDisableRepeat.Text = "Disable";
            this.toolTip1.SetToolTip(this.btnDisableRepeat, "Disable");
            this.btnDisableRepeat.UseVisualStyleBackColor = true;
            this.btnDisableRepeat.Click += new System.EventHandler(this.btnDisableRepeat_Click);
            // 
            // btnCloseRepeat
            // 
            this.btnCloseRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseRepeat.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCloseRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseRepeat.Location = new System.Drawing.Point(353, 0);
            this.btnCloseRepeat.Name = "btnCloseRepeat";
            this.btnCloseRepeat.Size = new System.Drawing.Size(35, 23);
            this.btnCloseRepeat.TabIndex = 98;
            this.toolTip1.SetToolTip(this.btnCloseRepeat, "Close");
            this.btnCloseRepeat.UseVisualStyleBackColor = true;
            this.btnCloseRepeat.Click += new System.EventHandler(this.btnCloseRepeat_Click);
            // 
            // btnRepeatCode
            // 
            this.btnRepeatCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRepeatCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeatCode.Location = new System.Drawing.Point(3, 131);
            this.btnRepeatCode.Name = "btnRepeatCode";
            this.btnRepeatCode.Size = new System.Drawing.Size(115, 23);
            this.btnRepeatCode.TabIndex = 102;
            this.btnRepeatCode.Text = "All";
            this.toolTip1.SetToolTip(this.btnRepeatCode, "All");
            this.btnRepeatCode.UseVisualStyleBackColor = true;
            this.btnRepeatCode.Click += new System.EventHandler(this.btnRepeatCode_Click);
            // 
            // btnResetRepeatCode
            // 
            this.btnResetRepeatCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnResetRepeatCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRepeatCode.Location = new System.Drawing.Point(124, 131);
            this.btnResetRepeatCode.Name = "btnResetRepeatCode";
            this.btnResetRepeatCode.Size = new System.Drawing.Size(35, 23);
            this.btnResetRepeatCode.TabIndex = 99;
            this.toolTip1.SetToolTip(this.btnResetRepeatCode, "Reset");
            this.btnResetRepeatCode.UseVisualStyleBackColor = true;
            this.btnResetRepeatCode.Click += new System.EventHandler(this.btnResetRepeatCode_Click);
            // 
            // lbIsoWeekNumber
            // 
            this.lbIsoWeekNumber.BackColor = System.Drawing.Color.Transparent;
            this.lbIsoWeekNumber.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIsoWeekNumber.Location = new System.Drawing.Point(324, 264);
            this.lbIsoWeekNumber.Name = "lbIsoWeekNumber";
            this.lbIsoWeekNumber.Size = new System.Drawing.Size(194, 26);
            this.lbIsoWeekNumber.TabIndex = 89;
            this.lbIsoWeekNumber.Text = "(ISO Weeknumber: 53)";
            this.lbIsoWeekNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerBlinker
            // 
            this.timerBlinker.Interval = 175;
            this.timerBlinker.Tick += new System.EventHandler(this.timerBlinker_Tick);
            // 
            // timerReFocus
            // 
            this.timerReFocus.Tick += new System.EventHandler(this.timerReFocus_Tick);
            // 
            // lbDateTimeAnsi
            // 
            this.lbDateTimeAnsi.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDateTimeAnsi.Location = new System.Drawing.Point(12, 38);
            this.lbDateTimeAnsi.Name = "lbDateTimeAnsi";
            this.lbDateTimeAnsi.Size = new System.Drawing.Size(511, 40);
            this.lbDateTimeAnsi.TabIndex = 93;
            this.lbDateTimeAnsi.Text = "label1";
            this.lbDateTimeAnsi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlRepeat
            // 
            this.pnlRepeat.Controls.Add(this.btnResetRepeatCode);
            this.pnlRepeat.Controls.Add(this.btnRepeatCode);
            this.pnlRepeat.Controls.Add(this.btnDisableRepeat);
            this.pnlRepeat.Controls.Add(this.pnlRepeatTextField);
            this.pnlRepeat.Controls.Add(this.btnRepeatWeekly);
            this.pnlRepeat.Controls.Add(this.btnRepeatDaily);
            this.pnlRepeat.Controls.Add(this.btnEnableRepeat);
            this.pnlRepeat.Location = new System.Drawing.Point(74, 91);
            this.pnlRepeat.Name = "pnlRepeat";
            this.pnlRepeat.Size = new System.Drawing.Size(394, 157);
            this.pnlRepeat.TabIndex = 96;
            // 
            // pnlRepeatTextField
            // 
            this.pnlRepeatTextField.Controls.Add(this.btnCloseRepeat);
            this.pnlRepeatTextField.Controls.Add(this.lbRepeat);
            this.pnlRepeatTextField.Location = new System.Drawing.Point(3, 3);
            this.pnlRepeatTextField.Name = "pnlRepeatTextField";
            this.pnlRepeatTextField.Size = new System.Drawing.Size(388, 93);
            this.pnlRepeatTextField.TabIndex = 100;
            // 
            // lbRepeat
            // 
            this.lbRepeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbRepeat.Location = new System.Drawing.Point(0, 0);
            this.lbRepeat.Name = "lbRepeat";
            this.lbRepeat.Size = new System.Drawing.Size(388, 93);
            this.lbRepeat.TabIndex = 97;
            this.lbRepeat.Text = "Repeat";
            this.lbRepeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMessage
            // 
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMessage.ContextMenuStrip = this.contextMenuBasic;
            this.tbMessage.Location = new System.Drawing.Point(13, 81);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReduceCache = false;
            this.tbMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.tbMessage.Size = new System.Drawing.Size(509, 177);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.Text = "";
            this.tbMessage.Click += new System.EventHandler(this.tbMessage_Click);
            this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged);
            this.tbMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMessage_KeyDown);
            this.tbMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMessage_KeyPress);
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
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem1.Text = "Cu&t";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem1.Text = "&Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // NotificationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 536);
            this.Controls.Add(this.pnlRepeat);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.lbDateTimeAnsi);
            this.Controls.Add(this.btnToggleCaret);
            this.Controls.Add(this.btnSnooze);
            this.Controls.Add(this.picHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCallUp);
            this.Controls.Add(this.btnMonthMinus2);
            this.Controls.Add(this.btnMonthPlus2);
            this.Controls.Add(this.btnDayMinus7);
            this.Controls.Add(this.lbIsoWeekNumber);
            this.Controls.Add(this.btnTimeZero);
            this.Controls.Add(this.btnDayPlus14);
            this.Controls.Add(this.btnHourPlus4);
            this.Controls.Add(this.btnMinute30);
            this.Controls.Add(this.btnHourMinus4);
            this.Controls.Add(this.btnGetTitle);
            this.Controls.Add(this.btnGetContent);
            this.Controls.Add(this.btnMinuteZero);
            this.Controls.Add(this.btnHourMinus2);
            this.Controls.Add(this.lbMonthName);
            this.Controls.Add(this.lbDayName);
            this.Controls.Add(this.pnlDateTimeContainer);
            this.Controls.Add(this.lbUndoRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnDayPlus7);
            this.Controls.Add(this.lbSystemTimeTitle);
            this.Controls.Add(this.lbSystemTimeDisplay);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnMinutesMinus);
            this.Controls.Add(this.btnMinutesPlus);
            this.Controls.Add(this.btnHourMinus);
            this.Controls.Add(this.btnHourPlus);
            this.Controls.Add(this.btnDayMinus);
            this.Controls.Add(this.btnDayPlus);
            this.Controls.Add(this.btnMonthMinus);
            this.Controls.Add(this.btnMonthPlus);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.btnMinutePlus15);
            this.Controls.Add(this.btnYearMinus);
            this.Controls.Add(this.btnYearPlus);
            this.Controls.Add(this.btnHourPlus2);
            this.Controls.Add(this.btnMinutePlus10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnToggleState);
            this.Controls.Add(this.btnMinutePlus5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 575);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 575);
            this.Name = "NotificationEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AHOY! Notification";
            this.pnlDateTimeContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
            this.pnlRepeat.ResumeLayout(false);
            this.pnlRepeatTextField.ResumeLayout(false);
            this.contextMenuBasic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToggleState;
        private System.Windows.Forms.Button btnMinutePlus5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.Button btnMinutePlus10;
        private System.Windows.Forms.Button btnHourPlus2;
        private System.Windows.Forms.Button btnYearMinus;
        private System.Windows.Forms.Button btnYearPlus;
        private System.Windows.Forms.Button btnMinutePlus15;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label lbMonth;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Button btnMonthMinus;
        private System.Windows.Forms.Button btnMonthPlus;
        private System.Windows.Forms.Button btnDayMinus;
        private System.Windows.Forms.Button btnDayPlus;
        private System.Windows.Forms.Label lbMonthName;
        private System.Windows.Forms.Label lbHour;
        private System.Windows.Forms.Label lbMinute;
        private System.Windows.Forms.Button btnHourMinus;
        private System.Windows.Forms.Button btnHourPlus;
        private System.Windows.Forms.Button btnMinutesMinus;
        private System.Windows.Forms.Button btnMinutesPlus;
        private System.Windows.Forms.Label lbTimeDivider;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbTitle;
        private HistoryTextBox tbMessage;
        private System.Windows.Forms.Label lbSystemTimeDisplay;
        private System.Windows.Forms.Label lbSystemTimeTitle;
        private System.Windows.Forms.Button btnDayPlus7;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lbUndoRedo;
        private System.Windows.Forms.Panel pnlDateTimeContainer;
        private System.Windows.Forms.Timer timerSystemTime;
        private System.Windows.Forms.Label lbDayName;
        private System.Windows.Forms.Button btnHourMinus2;
        private System.Windows.Forms.Button btnMinuteZero;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnGetContent;
        private System.Windows.Forms.Button btnGetTitle;
        private System.Windows.Forms.Button btnHourMinus4;
        private System.Windows.Forms.Button btnMinute30;
        private System.Windows.Forms.Button btnHourPlus4;
        private System.Windows.Forms.Button btnDayPlus14;
        private System.Windows.Forms.Button btnTimeZero;
        private System.Windows.Forms.Label lbIsoWeekNumber;
        private System.Windows.Forms.Button btnDayMinus7;
        private System.Windows.Forms.Button btnMonthMinus2;
        private System.Windows.Forms.Button btnMonthPlus2;
        private System.Windows.Forms.Timer timerBlinker;
        private System.Windows.Forms.Button btnCallUp;
        private System.Windows.Forms.Timer timerReFocus;
        private System.Windows.Forms.Button btnCancel;
        private AhoyContextMenuStrip contextMenuBasic;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
        private System.Windows.Forms.PictureBox picHelp;
        private System.Windows.Forms.Button btnSnooze;
        private System.Windows.Forms.Button btnToggleCaret;
        private System.Windows.Forms.Label lbDateTimeAnsi;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Panel pnlRepeat;
        private System.Windows.Forms.Button btnEnableRepeat;
        private System.Windows.Forms.Label lbRepeat;
        private System.Windows.Forms.Button btnRepeatWeekly;
        private System.Windows.Forms.Button btnRepeatDaily;
        private System.Windows.Forms.Panel pnlRepeatTextField;
        private System.Windows.Forms.Button btnDisableRepeat;
        private System.Windows.Forms.Button btnCloseRepeat;
        private System.Windows.Forms.Button btnRepeatCode;
        private System.Windows.Forms.Button btnResetRepeatCode;
    }
}