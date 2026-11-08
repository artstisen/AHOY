namespace AhoyBrowser
{
    partial class TypeWriter
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
            this.lbOutput = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.tbInput = new AhoyBrowser.HistoryTextBox();
            this.timerGlobalTimer = new System.Windows.Forms.Timer(this.components);
            this.timerDisplayWriter = new System.Windows.Forms.Timer(this.components);
            this.lbSplash = new System.Windows.Forms.Label();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.lbInstructions = new System.Windows.Forms.Label();
            this.lbCPLRuler = new System.Windows.Forms.Label();
            this.lbLogo = new System.Windows.Forms.Label();
            this.lbMenu = new System.Windows.Forms.Label();
            this.lbMessage = new System.Windows.Forms.Label();
            this.pnlExit = new System.Windows.Forms.Panel();
            this.pnlBasketPosition = new System.Windows.Forms.Panel();
            this.pnlPresets = new System.Windows.Forms.Panel();
            this.pnlInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOutput.Location = new System.Drawing.Point(0, 0);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(568, 404);
            this.lbOutput.TabIndex = 0;
            this.lbOutput.Text = "Ready";
            // 
            // pnlInput
            // 
            this.pnlInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlInput.Controls.Add(this.lbCPLRuler);
            this.pnlInput.Controls.Add(this.tbInput);
            this.pnlInput.Location = new System.Drawing.Point(85, 268);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(400, 60);
            this.pnlInput.TabIndex = 1;
            // 
            // lbCPLRuler
            // 
            this.lbCPLRuler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCPLRuler.Font = new System.Drawing.Font("Courier New", 12F);
            this.lbCPLRuler.Location = new System.Drawing.Point(5, 35);
            this.lbCPLRuler.Name = "lbCPLRuler";
            this.lbCPLRuler.Size = new System.Drawing.Size(390, 18);
            this.lbCPLRuler.TabIndex = 3;
            // 
            // tbInput
            // 
            this.tbInput.AcceptsTab = true;
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(5, 6);
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbInput.Size = new System.Drawing.Size(390, 26);
            this.tbInput.TabIndex = 2;
            this.tbInput.Text = "";
            this.tbInput.WordWrap = false;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // timerDisplayWriter
            // 
            this.timerDisplayWriter.Tick += new System.EventHandler(this.timerDisplayWriter_Tick);
            // 
            // lbSplash
            // 
            this.lbSplash.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSplash.Location = new System.Drawing.Point(98, 62);
            this.lbSplash.Name = "lbSplash";
            this.lbSplash.Size = new System.Drawing.Size(100, 23);
            this.lbSplash.TabIndex = 2;
            this.lbSplash.Text = "Splash...";
            // 
            // timerSplash
            // 
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // lbInstructions
            // 
            this.lbInstructions.BackColor = System.Drawing.Color.Silver;
            this.lbInstructions.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbInstructions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbInstructions.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInstructions.Location = new System.Drawing.Point(0, 381);
            this.lbInstructions.Name = "lbInstructions";
            this.lbInstructions.Size = new System.Drawing.Size(568, 27);
            this.lbInstructions.TabIndex = 3;
            this.lbInstructions.Text = "Ready";
            this.lbInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbInstructions.Click += new System.EventHandler(this.lbInstructions_Click);
            // 
            // lbLogo
            // 
            this.lbLogo.BackColor = System.Drawing.Color.Silver;
            this.lbLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbLogo.Dock = System.Windows.Forms.DockStyle.None;
            this.lbLogo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogo.Location = new System.Drawing.Point(0, 0);
            this.lbLogo.Name = "lbLogo";
            this.lbLogo.Size = new System.Drawing.Size(275, 60);
            this.lbLogo.TabIndex = 3;
            this.lbLogo.Text = "TypeWriter";
            this.lbLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMessage
            // 
            this.lbMessage.BackColor = System.Drawing.Color.Silver;
            this.lbMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbMessage.Dock = System.Windows.Forms.DockStyle.None;
            this.lbMessage.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.Location = new System.Drawing.Point(0, 0);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(275, 60);
            this.lbMessage.TabIndex = 3;
            this.lbMessage.Text = "Message";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMenu
            // 
            this.lbMenu.BackColor = System.Drawing.Color.Silver;
            this.lbMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.lbMenu.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMenu.Location = new System.Drawing.Point(10, 10);
            this.lbMenu.Name = "lbMenu";
            this.lbMenu.Size = new System.Drawing.Size(425, 625);
            this.lbMenu.TabIndex = 3;
            this.lbMenu.Text = "Ready";
            this.lbMenu.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // pnlExit
            // 
            this.pnlExit.BackColor = System.Drawing.Color.Silver;
            this.pnlExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlExit.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlExit.Location = new System.Drawing.Point(10, 10);
            this.pnlExit.Name = "pnlExit";
            this.pnlExit.Size = new System.Drawing.Size(37, 23);
            this.pnlExit.TabIndex = 1;
            // 
            // pnlBasketPosition
            // 
            this.pnlBasketPosition.BackColor = System.Drawing.Color.Silver;
            this.pnlBasketPosition.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlBasketPosition.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlBasketPosition.Location = new System.Drawing.Point(10, 10);
            this.pnlBasketPosition.Name = "pnlBasketPosition";
            this.pnlBasketPosition.Size = new System.Drawing.Size(37, 23);
            this.pnlBasketPosition.TabIndex = 1;
            // 
            // pnlPresets
            // 
            this.pnlPresets.BackColor = System.Drawing.Color.Silver;
            this.pnlPresets.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlPresets.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlPresets.Location = new System.Drawing.Point(10, 10);
            this.pnlPresets.Name = "pnlPresets";
            this.pnlPresets.Size = new System.Drawing.Size(37, 23);
            this.pnlPresets.TabIndex = 1;
            // 
            // TypeWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbInstructions);
            this.Controls.Add(this.lbSplash);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.lbLogo);
            this.Controls.Add(this.lbMenu);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.pnlExit);
            this.Controls.Add(this.pnlBasketPosition);
            this.Controls.Add(this.pnlPresets);
            this.DoubleBuffered = true;
            this.Name = "TypeWriter";
            this.Size = new System.Drawing.Size(568, 404);
            this.pnlInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbOutput;
        private System.Windows.Forms.Panel pnlInput;
        private AhoyBrowser.HistoryTextBox tbInput;
        private System.Windows.Forms.Timer timerGlobalTimer;
        private System.Windows.Forms.Timer timerDisplayWriter;
        private System.Windows.Forms.Label lbSplash;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.Label lbInstructions;
        private System.Windows.Forms.Label lbCPLRuler;
        private System.Windows.Forms.Label lbLogo;
        private System.Windows.Forms.Label lbMenu;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Panel pnlExit;
        private System.Windows.Forms.Panel pnlBasketPosition;
        private System.Windows.Forms.Panel pnlPresets;
    }
}
