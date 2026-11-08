namespace AhoyBrowser.Typermax
{
    partial class Typermax
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
            this.lbTarget = new System.Windows.Forms.Label();
            this.lbInput = new System.Windows.Forms.Label();
            this.lbLevelMeter = new System.Windows.Forms.Label();
            this.lbWPMMeter = new System.Windows.Forms.Label();
            this.lbTimeMeter = new System.Windows.Forms.Label();
            this.lbCredits = new System.Windows.Forms.Label();
            this.lbWPMDisplay = new System.Windows.Forms.Label();
            this.lbLevelDisplay = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbHighScore = new System.Windows.Forms.Label();
            this.lbScore = new System.Windows.Forms.Label();
            this.timerBlinker = new System.Windows.Forms.Timer(this.components);
            this.timerFillMeter = new System.Windows.Forms.Timer(this.components);
            this.timerGlobal = new System.Windows.Forms.Timer(this.components);
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnHelp = new System.Windows.Forms.Button();
            this.lbSplash = new System.Windows.Forms.Label();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.timerGameRunning = new System.Windows.Forms.Timer(this.components);
            this.timerGameEngine = new System.Windows.Forms.Timer(this.components);
            this.lbPercentageDisplay = new System.Windows.Forms.Label();
            this.lbWPMLabel = new System.Windows.Forms.Label();
            this.lbLevelLabel = new System.Windows.Forms.Label();
            this.lbRuler = new System.Windows.Forms.Label();
            this.lbVersionNumber = new System.Windows.Forms.Label();
            this.lbCountDownSpeed = new System.Windows.Forms.Label();
            this.lbErrorDisplay = new System.Windows.Forms.Label();
            this.timerPenaltyBlinker = new System.Windows.Forms.Timer(this.components);
            this.tbHistory = new AhoyBrowser.HistoryTextBox();
            this.mqLogo = new Marquee();
            this.tbTarget = new AhoyBrowser.HistoryTextBox();
            this.tbInput = new AhoyBrowser.HistoryTextBox();
            this.pnlRulerBottomMask = new System.Windows.Forms.Panel();
            this.pnlRulerBottomMask.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTarget
            // 
            this.lbTarget.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.ForeColor = System.Drawing.Color.Black;
            this.lbTarget.Location = new System.Drawing.Point(51, 245);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(38, 31);
            this.lbTarget.TabIndex = 2;
            this.lbTarget.Text = ">";
            this.lbTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInput
            // 
            this.lbInput.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInput.ForeColor = System.Drawing.Color.Black;
            this.lbInput.Location = new System.Drawing.Point(51, 282);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(38, 31);
            this.lbInput.TabIndex = 3;
            this.lbInput.Text = "<";
            this.lbInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLevelMeter
            // 
            this.lbLevelMeter.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevelMeter.ForeColor = System.Drawing.Color.Black;
            this.lbLevelMeter.Location = new System.Drawing.Point(92, 122);
            this.lbLevelMeter.Name = "lbLevelMeter";
            this.lbLevelMeter.Size = new System.Drawing.Size(349, 31);
            this.lbLevelMeter.TabIndex = 4;
            this.lbLevelMeter.Text = "█";
            this.lbLevelMeter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbWPMMeter
            // 
            this.lbWPMMeter.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWPMMeter.ForeColor = System.Drawing.Color.Black;
            this.lbWPMMeter.Location = new System.Drawing.Point(92, 153);
            this.lbWPMMeter.Name = "lbWPMMeter";
            this.lbWPMMeter.Size = new System.Drawing.Size(349, 31);
            this.lbWPMMeter.TabIndex = 5;
            this.lbWPMMeter.Text = "██████████";
            this.lbWPMMeter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTimeMeter
            // 
            this.lbTimeMeter.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimeMeter.ForeColor = System.Drawing.Color.Black;
            this.lbTimeMeter.Location = new System.Drawing.Point(92, 185);
            this.lbTimeMeter.Name = "lbTimeMeter";
            this.lbTimeMeter.Size = new System.Drawing.Size(349, 31);
            this.lbTimeMeter.TabIndex = 6;
            this.lbTimeMeter.Text = "██████████████████████████████";
            this.lbTimeMeter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCredits
            // 
            this.lbCredits.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCredits.ForeColor = System.Drawing.Color.Black;
            this.lbCredits.Location = new System.Drawing.Point(369, 184);
            this.lbCredits.Name = "lbCredits";
            this.lbCredits.Size = new System.Drawing.Size(223, 31);
            this.lbCredits.TabIndex = 7;
            this.lbCredits.Text = "3 CREDITS";
            this.lbCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbWPMDisplay
            // 
            this.lbWPMDisplay.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWPMDisplay.ForeColor = System.Drawing.Color.Black;
            this.lbWPMDisplay.Location = new System.Drawing.Point(447, 152);
            this.lbWPMDisplay.Name = "lbWPMDisplay";
            this.lbWPMDisplay.Size = new System.Drawing.Size(147, 31);
            this.lbWPMDisplay.TabIndex = 8;
            this.lbWPMDisplay.Text = "92 WPM";
            this.lbWPMDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLevelDisplay
            // 
            this.lbLevelDisplay.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevelDisplay.ForeColor = System.Drawing.Color.Black;
            this.lbLevelDisplay.Location = new System.Drawing.Point(447, 121);
            this.lbLevelDisplay.Name = "lbLevelDisplay";
            this.lbLevelDisplay.Size = new System.Drawing.Size(147, 31);
            this.lbLevelDisplay.TabIndex = 9;
            this.lbLevelDisplay.Text = "LEVEL 1";
            this.lbLevelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.ForeColor = System.Drawing.Color.Black;
            this.btnStartStop.Location = new System.Drawing.Point(373, 356);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(221, 43);
            this.btnStartStop.TabIndex = 11;
            this.btnStartStop.Text = "Start (F5)";
            this.toolTip1.SetToolTip(this.btnStartStop, "Start/Pause/Resume Game (Press F5)");
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(266, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 43);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.toolTip1.SetToolTip(this.btnExit, "Exit Typermax (ESC)");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbHighScore
            // 
            this.lbHighScore.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.lbHighScore.ForeColor = System.Drawing.Color.Black;
            this.lbHighScore.Location = new System.Drawing.Point(369, 65);
            this.lbHighScore.Name = "lbHighScore";
            this.lbHighScore.Size = new System.Drawing.Size(225, 56);
            this.lbHighScore.TabIndex = 14;
            this.lbHighScore.Text = "HIGH: 999999";
            this.lbHighScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbScore
            // 
            this.lbScore.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.lbScore.ForeColor = System.Drawing.Color.Black;
            this.lbScore.Location = new System.Drawing.Point(91, 65);
            this.lbScore.Name = "lbScore";
            this.lbScore.Size = new System.Drawing.Size(272, 56);
            this.lbScore.TabIndex = 15;
            this.lbScore.Text = "SCORE: 999999";
            this.lbScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerBlinker
            // 
            this.timerBlinker.Interval = 200;
            this.timerBlinker.Tick += new System.EventHandler(this.timerBlinker_Tick);
            // 
            // timerFillMeter
            // 
            this.timerFillMeter.Interval = 50;
            this.timerFillMeter.Tick += new System.EventHandler(this.timerFillMeter_Tick);
            // 
            // timerGlobal
            // 
            this.timerGlobal.Enabled = true;
            this.timerGlobal.Interval = 1000;
            this.timerGlobal.Tick += new System.EventHandler(this.timerGlobal_Tick);
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // btnHelp
            // 
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHelp.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Location = new System.Drawing.Point(206, 356);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(54, 43);
            this.btnHelp.TabIndex = 17;
            this.btnHelp.Text = "?";
            this.toolTip1.SetToolTip(this.btnHelp, "Help (F1)");
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lbSplash
            // 
            this.lbSplash.Location = new System.Drawing.Point(49, 9);
            this.lbSplash.Name = "lbSplash";
            this.lbSplash.Size = new System.Drawing.Size(100, 23);
            this.lbSplash.TabIndex = 16;
            this.lbSplash.Text = "Splash...";
            // 
            // timerSplash
            // 
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // timerGameRunning
            // 
            this.timerGameRunning.Tick += new System.EventHandler(this.timerGameRunning_Tick);
            // 
            // timerGameEngine
            // 
            this.timerGameEngine.Interval = 500;
            this.timerGameEngine.Tick += new System.EventHandler(this.timerGameEngine_Tick);
            // 
            // lbPercentageDisplay
            // 
            this.lbPercentageDisplay.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPercentageDisplay.ForeColor = System.Drawing.Color.Black;
            this.lbPercentageDisplay.Location = new System.Drawing.Point(12, 184);
            this.lbPercentageDisplay.Name = "lbPercentageDisplay";
            this.lbPercentageDisplay.Size = new System.Drawing.Size(74, 31);
            this.lbPercentageDisplay.TabIndex = 19;
            this.lbPercentageDisplay.Text = "999%";
            this.lbPercentageDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbWPMLabel
            // 
            this.lbWPMLabel.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWPMLabel.ForeColor = System.Drawing.Color.Black;
            this.lbWPMLabel.Location = new System.Drawing.Point(12, 152);
            this.lbWPMLabel.Name = "lbWPMLabel";
            this.lbWPMLabel.Size = new System.Drawing.Size(74, 31);
            this.lbWPMLabel.TabIndex = 20;
            this.lbWPMLabel.Text = "WPM";
            this.lbWPMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLevelLabel
            // 
            this.lbLevelLabel.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevelLabel.ForeColor = System.Drawing.Color.Black;
            this.lbLevelLabel.Location = new System.Drawing.Point(12, 122);
            this.lbLevelLabel.Name = "lbLevelLabel";
            this.lbLevelLabel.Size = new System.Drawing.Size(74, 31);
            this.lbLevelLabel.TabIndex = 21;
            this.lbLevelLabel.Text = "Level";
            this.lbLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRuler
            // 
            this.lbRuler.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRuler.ForeColor = System.Drawing.Color.Black;
            this.lbRuler.Location = new System.Drawing.Point(91, 264);
            this.lbRuler.Name = "lbRuler";
            this.lbRuler.Size = new System.Drawing.Size(505, 31);
            this.lbRuler.TabIndex = 22;
            this.lbRuler.Text = "||||||";
            this.lbRuler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbVersionNumber
            // 
            this.lbVersionNumber.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersionNumber.ForeColor = System.Drawing.Color.Black;
            this.lbVersionNumber.Location = new System.Drawing.Point(470, 65);
            this.lbVersionNumber.Name = "lbVersionNumber";
            this.lbVersionNumber.Size = new System.Drawing.Size(147, 18);
            this.lbVersionNumber.TabIndex = 23;
            this.lbVersionNumber.Text = "v1.07";
            this.lbVersionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCountDownSpeed
            // 
            this.lbCountDownSpeed.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountDownSpeed.ForeColor = System.Drawing.Color.Black;
            this.lbCountDownSpeed.Location = new System.Drawing.Point(95, 216);
            this.lbCountDownSpeed.Name = "lbCountDownSpeed";
            this.lbCountDownSpeed.Size = new System.Drawing.Size(499, 26);
            this.lbCountDownSpeed.TabIndex = 24;
            this.lbCountDownSpeed.Text = "Retraction speed: 1/4 (Keystrokes: +153948 -435 (-950%))";
            this.lbCountDownSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbErrorDisplay
            // 
            this.lbErrorDisplay.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErrorDisplay.ForeColor = System.Drawing.Color.Black;
            this.lbErrorDisplay.Location = new System.Drawing.Point(92, 316);
            this.lbErrorDisplay.Name = "lbErrorDisplay";
            this.lbErrorDisplay.Size = new System.Drawing.Size(502, 37);
            this.lbErrorDisplay.TabIndex = 25;
            this.lbErrorDisplay.Text = "Oops! *** TIME PENALTY ***";
            this.lbErrorDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerPenaltyBlinker
            // 
            this.timerPenaltyBlinker.Interval = 500;
            this.timerPenaltyBlinker.Tick += new System.EventHandler(this.timerPenaltyBlinker_Tick);
            // 
            // tbHistory
            // 
            this.tbHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHistory.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.tbHistory.ForeColor = System.Drawing.Color.Black;
            this.tbHistory.Location = new System.Drawing.Point(70, 0);
            this.tbHistory.Multiline = false;
            this.tbHistory.Name = "tbHistory";
            this.tbHistory.ReduceCache = false;
            this.tbHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbHistory.ShortcutsEnabled = false;
            this.tbHistory.Size = new System.Drawing.Size(429, 31);
            this.tbHistory.TabIndex = 26;
            this.tbHistory.Text = "";
            this.tbHistory.WordWrap = false;
            this.tbHistory.Enter += new System.EventHandler(this.tbTarget_Enter);
            this.tbHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTarget_KeyDown);
            // 
            // mqLogo
            // 
            this.mqLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mqLogo.ForeColor = System.Drawing.Color.Black;
            this.mqLogo.Location = new System.Drawing.Point(12, 12);
            this.mqLogo.MinimumSize = new System.Drawing.Size(100, 20);
            this.mqLogo.Name = "mqLogo";
            this.mqLogo.Size = new System.Drawing.Size(605, 50);
            this.mqLogo.TabIndex = 13;
            // 
            // tbTarget
            // 
            this.tbTarget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTarget.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.tbTarget.ForeColor = System.Drawing.Color.Black;
            this.tbTarget.Location = new System.Drawing.Point(95, 245);
            this.tbTarget.Multiline = false;
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.ReduceCache = false;
            this.tbTarget.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbTarget.ShortcutsEnabled = false;
            this.tbTarget.Size = new System.Drawing.Size(499, 31);
            this.tbTarget.TabIndex = 1;
            this.tbTarget.Text = "";
            this.tbTarget.WordWrap = false;
            this.tbTarget.TextChanged += new System.EventHandler(this.tbTarget_TextChanged);
            this.tbTarget.Enter += new System.EventHandler(this.tbTarget_Enter);
            this.tbTarget.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTarget_KeyDown);
            // 
            // tbInput
            // 
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.tbInput.ForeColor = System.Drawing.Color.Black;
            this.tbInput.Location = new System.Drawing.Point(0, 0);
            this.tbInput.Multiline = false;
            this.tbInput.Name = "tbInput";
            this.tbInput.ReduceCache = false;
            this.tbInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbInput.ShortcutsEnabled = false;
            this.tbInput.Size = new System.Drawing.Size(64, 31);
            this.tbInput.TabIndex = 0;
            this.tbInput.Text = "";
            this.tbInput.WordWrap = false;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            this.tbInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInput_KeyPress);
            this.tbInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyUp);
            // 
            // pnlRulerBottomMask
            // 
            this.pnlRulerBottomMask.Controls.Add(this.tbInput);
            this.pnlRulerBottomMask.Controls.Add(this.tbHistory);
            this.pnlRulerBottomMask.Location = new System.Drawing.Point(95, 282);
            this.pnlRulerBottomMask.Name = "pnlRulerBottomMask";
            this.pnlRulerBottomMask.Size = new System.Drawing.Size(499, 31);
            this.pnlRulerBottomMask.TabIndex = 27;
            // 
            // Typermax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 411);
            this.Controls.Add(this.pnlRulerBottomMask);
            this.Controls.Add(this.lbErrorDisplay);
            this.Controls.Add(this.lbCountDownSpeed);
            this.Controls.Add(this.lbVersionNumber);
            this.Controls.Add(this.lbLevelLabel);
            this.Controls.Add(this.lbWPMLabel);
            this.Controls.Add(this.lbPercentageDisplay);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbSplash);
            this.Controls.Add(this.lbScore);
            this.Controls.Add(this.lbHighScore);
            this.Controls.Add(this.mqLogo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.lbLevelDisplay);
            this.Controls.Add(this.lbWPMDisplay);
            this.Controls.Add(this.lbCredits);
            this.Controls.Add(this.lbTimeMeter);
            this.Controls.Add(this.lbWPMMeter);
            this.Controls.Add(this.lbLevelMeter);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.lbRuler);
            this.DoubleBuffered = true;
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(645, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(645, 450);
            this.Name = "Typermax";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Typermax";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Typermax_KeyDown);
            this.pnlRulerBottomMask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HistoryTextBox tbInput;
        private HistoryTextBox tbTarget;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Label lbLevelMeter;
        private System.Windows.Forms.Label lbWPMMeter;
        private System.Windows.Forms.Label lbTimeMeter;
        private System.Windows.Forms.Label lbCredits;
        private System.Windows.Forms.Label lbWPMDisplay;
        private System.Windows.Forms.Label lbLevelDisplay;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnExit;
        private Marquee mqLogo;
        private System.Windows.Forms.Label lbHighScore;
        private System.Windows.Forms.Label lbScore;
        private System.Windows.Forms.Timer timerBlinker;
        private System.Windows.Forms.Timer timerFillMeter;
        private System.Windows.Forms.Timer timerGlobal;
        private System.Windows.Forms.Timer timerCountDown;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbSplash;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.Timer timerGameRunning;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Timer timerGameEngine;
        private System.Windows.Forms.Label lbPercentageDisplay;
        private System.Windows.Forms.Label lbWPMLabel;
        private System.Windows.Forms.Label lbLevelLabel;
        private System.Windows.Forms.Label lbRuler;
        private System.Windows.Forms.Label lbVersionNumber;
        private System.Windows.Forms.Label lbCountDownSpeed;
        private System.Windows.Forms.Label lbErrorDisplay;
        private System.Windows.Forms.Timer timerPenaltyBlinker;
        private HistoryTextBox tbHistory;
        private System.Windows.Forms.Panel pnlRulerBottomMask;
    }
}