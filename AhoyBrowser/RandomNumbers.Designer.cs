namespace AhoyBrowser
{
    partial class RandomNumbers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomNumbers));
            this.btnReset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbDiceCount = new System.Windows.Forms.Label();
            this.tbDiceCount = new System.Windows.Forms.TextBox();
            this.tbMultipleDice = new System.Windows.Forms.TextBox();
            this.lbUnique = new System.Windows.Forms.Label();
            this.lbDice = new System.Windows.Forms.Label();
            this.tbSingleDice = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.lbTo = new System.Windows.Forms.Label();
            this.tbToNumber = new System.Windows.Forms.TextBox();
            this.lbFrom = new System.Windows.Forms.Label();
            this.tbFromNumber = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbInstruction = new System.Windows.Forms.Label();
            this.ahoyContextMenuStrip1 = new AhoyBrowser.AhoyContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ahoyContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(224, 507);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(47, 26);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "R";
            this.toolTip1.SetToolTip(this.btnReset, "Reset (Press CTRL+R)");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Location = new System.Drawing.Point(-32, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 10);
            this.panel1.TabIndex = 27;
            // 
            // lbDiceCount
            // 
            this.lbDiceCount.AutoSize = true;
            this.lbDiceCount.Location = new System.Drawing.Point(12, 212);
            this.lbDiceCount.Name = "lbDiceCount";
            this.lbDiceCount.Size = new System.Drawing.Size(60, 13);
            this.lbDiceCount.TabIndex = 26;
            this.lbDiceCount.Text = "Dice Count";
            // 
            // tbDiceCount
            // 
            this.tbDiceCount.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDiceCount.Location = new System.Drawing.Point(12, 228);
            this.tbDiceCount.MaximumSize = new System.Drawing.Size(100, 26);
            this.tbDiceCount.MinimumSize = new System.Drawing.Size(100, 26);
            this.tbDiceCount.Name = "tbDiceCount";
            this.tbDiceCount.Size = new System.Drawing.Size(100, 26);
            this.tbDiceCount.TabIndex = 2;
            this.tbDiceCount.Text = "3";
            // 
            // tbMultipleDice
            // 
            this.tbMultipleDice.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMultipleDice.Location = new System.Drawing.Point(12, 260);
            this.tbMultipleDice.Multiline = true;
            this.tbMultipleDice.Name = "tbMultipleDice";
            this.tbMultipleDice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMultipleDice.Size = new System.Drawing.Size(206, 241);
            this.tbMultipleDice.TabIndex = 6;
            this.tbMultipleDice.TabStop = false;
            // 
            // lbUnique
            // 
            this.lbUnique.AutoSize = true;
            this.lbUnique.Location = new System.Drawing.Point(221, 153);
            this.lbUnique.Name = "lbUnique";
            this.lbUnique.Size = new System.Drawing.Size(108, 13);
            this.lbUnique.TabIndex = 23;
            this.lbUnique.Text = "Non-repeating values";
            // 
            // lbDice
            // 
            this.lbDice.AutoSize = true;
            this.lbDice.Location = new System.Drawing.Point(12, 153);
            this.lbDice.Name = "lbDice";
            this.lbDice.Size = new System.Drawing.Size(61, 13);
            this.lbDice.TabIndex = 22;
            this.lbDice.Text = "Single Dice";
            // 
            // tbSingleDice
            // 
            this.tbSingleDice.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSingleDice.Location = new System.Drawing.Point(12, 171);
            this.tbSingleDice.MaximumSize = new System.Drawing.Size(100, 26);
            this.tbSingleDice.MinimumSize = new System.Drawing.Size(100, 26);
            this.tbSingleDice.Name = "tbSingleDice";
            this.tbSingleDice.Size = new System.Drawing.Size(100, 26);
            this.tbSingleDice.TabIndex = 5;
            this.tbSingleDice.TabStop = false;
            this.tbSingleDice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOutput
            // 
            this.tbOutput.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(224, 171);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(206, 330);
            this.tbOutput.TabIndex = 7;
            this.tbOutput.TabStop = false;
            // 
            // lbTo
            // 
            this.lbTo.AutoSize = true;
            this.lbTo.Location = new System.Drawing.Point(115, 86);
            this.lbTo.Name = "lbTo";
            this.lbTo.Size = new System.Drawing.Size(57, 13);
            this.lbTo.TabIndex = 19;
            this.lbTo.Text = "Max Value";
            // 
            // tbToNumber
            // 
            this.tbToNumber.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbToNumber.Location = new System.Drawing.Point(118, 102);
            this.tbToNumber.MaximumSize = new System.Drawing.Size(100, 26);
            this.tbToNumber.MinimumSize = new System.Drawing.Size(100, 26);
            this.tbToNumber.Name = "tbToNumber";
            this.tbToNumber.Size = new System.Drawing.Size(100, 26);
            this.tbToNumber.TabIndex = 0;
            this.tbToNumber.Text = "6";
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Location = new System.Drawing.Point(9, 86);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(54, 13);
            this.lbFrom.TabIndex = 17;
            this.lbFrom.Text = "Min Value";
            // 
            // tbFromNumber
            // 
            this.tbFromNumber.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFromNumber.Location = new System.Drawing.Point(12, 102);
            this.tbFromNumber.MaximumSize = new System.Drawing.Size(100, 26);
            this.tbFromNumber.MinimumSize = new System.Drawing.Size(100, 26);
            this.tbFromNumber.Name = "tbFromNumber";
            this.tbFromNumber.Size = new System.Drawing.Size(100, 26);
            this.tbFromNumber.TabIndex = 1;
            this.tbFromNumber.Text = "1";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(277, 507);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(153, 26);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "GENERATE";
            this.toolTip1.SetToolTip(this.btnGenerate, "Generate (Press Enter)");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 507);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 26);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Exit";
            this.toolTip1.SetToolTip(this.btnClose, "Exit (Press ESC)");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTip1_Draw);
            // 
            // lbInstruction
            // 
            this.lbInstruction.Location = new System.Drawing.Point(12, 9);
            this.lbInstruction.Name = "lbInstruction";
            this.lbInstruction.Size = new System.Drawing.Size(418, 77);
            this.lbInstruction.TabIndex = 30;
            this.lbInstruction.Text = "Instructions...";
            // 
            // ahoyContextMenuStrip1
            // 
            this.ahoyContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.ahoyContextMenuStrip1.Name = "ahoyContextMenuStrip1";
            this.ahoyContextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // RandomNumbers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 546);
            this.Controls.Add(this.lbInstruction);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbDiceCount);
            this.Controls.Add(this.tbDiceCount);
            this.Controls.Add(this.tbMultipleDice);
            this.Controls.Add(this.lbUnique);
            this.Controls.Add(this.lbDice);
            this.Controls.Add(this.tbSingleDice);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.lbTo);
            this.Controls.Add(this.tbToNumber);
            this.Controls.Add(this.lbFrom);
            this.Controls.Add(this.tbFromNumber);
            this.Controls.Add(this.btnGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 585);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 585);
            this.Name = "RandomNumbers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Random Numbers";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RandomNumbers_KeyDown);
            this.ahoyContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbDiceCount;
        private System.Windows.Forms.TextBox tbDiceCount;
        private System.Windows.Forms.TextBox tbMultipleDice;
        private System.Windows.Forms.Label lbUnique;
        private System.Windows.Forms.Label lbDice;
        private System.Windows.Forms.TextBox tbSingleDice;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label lbTo;
        private System.Windows.Forms.TextBox tbToNumber;
        private System.Windows.Forms.Label lbFrom;
        private System.Windows.Forms.TextBox tbFromNumber;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbInstruction;
        private AhoyContextMenuStrip ahoyContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}