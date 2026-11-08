namespace AhoyBrowser.MessageBoxes
{
    partial class SearchAndReplace
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.lbTarget = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.lbMatches = new System.Windows.Forms.Label();
            this.btnToggleCaseSense = new System.Windows.Forms.Button();
            this.lbCase = new System.Windows.Forms.Label();
            this.lbSelection = new System.Windows.Forms.Label();
            this.btnToggleSelection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCancel.Location = new System.Drawing.Point(219, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReplace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnReplace.Location = new System.Drawing.Point(300, 141);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(137, 23);
            this.btnReplace.TabIndex = 6;
            this.btnReplace.Text = "&Replace All";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // lbTarget
            // 
            this.lbTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbTarget.Location = new System.Drawing.Point(12, 32);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(183, 13);
            this.lbTarget.TabIndex = 9;
            this.lbTarget.Text = "Replace:";
            // 
            // tbTarget
            // 
            this.tbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbTarget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTarget.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0)))));
            this.tbTarget.Location = new System.Drawing.Point(15, 48);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(422, 19);
            this.tbTarget.TabIndex = 8;
            this.tbTarget.TextChanged += new System.EventHandler(this.tbTarget_TextChanged);
            // 
            // tbInput
            // 
            this.tbInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.tbInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0)))));
            this.tbInput.Location = new System.Drawing.Point(15, 89);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(422, 19);
            this.tbInput.TabIndex = 10;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // lbInput
            // 
            this.lbInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbInput.Location = new System.Drawing.Point(12, 73);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(183, 13);
            this.lbInput.TabIndex = 11;
            this.lbInput.Text = "With:";
            // 
            // lbMatches
            // 
            this.lbMatches.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbMatches.Location = new System.Drawing.Point(254, 32);
            this.lbMatches.Name = "lbMatches";
            this.lbMatches.Size = new System.Drawing.Size(183, 13);
            this.lbMatches.TabIndex = 12;
            this.lbMatches.Text = "Matching: 0";
            this.lbMatches.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnToggleCaseSense
            // 
            this.btnToggleCaseSense.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleCaseSense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToggleCaseSense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnToggleCaseSense.Location = new System.Drawing.Point(15, 141);
            this.btnToggleCaseSense.Name = "btnToggleCaseSense";
            this.btnToggleCaseSense.Size = new System.Drawing.Size(75, 23);
            this.btnToggleCaseSense.TabIndex = 13;
            this.btnToggleCaseSense.Text = "YES";
            this.btnToggleCaseSense.UseVisualStyleBackColor = true;
            this.btnToggleCaseSense.Click += new System.EventHandler(this.btnToggleCaseSense_Click);
            // 
            // lbCase
            // 
            this.lbCase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbCase.Location = new System.Drawing.Point(12, 125);
            this.lbCase.Name = "lbCase";
            this.lbCase.Size = new System.Drawing.Size(78, 13);
            this.lbCase.TabIndex = 14;
            this.lbCase.Text = "Ignore case";
            this.lbCase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSelection
            // 
            this.lbSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbSelection.Location = new System.Drawing.Point(94, 125);
            this.lbSelection.Name = "lbSelection";
            this.lbSelection.Size = new System.Drawing.Size(75, 13);
            this.lbSelection.TabIndex = 15;
            this.lbSelection.Text = "Selection";
            this.lbSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnToggleSelection
            // 
            this.btnToggleSelection.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToggleSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnToggleSelection.Location = new System.Drawing.Point(96, 141);
            this.btnToggleSelection.Name = "btnToggleSelection";
            this.btnToggleSelection.Size = new System.Drawing.Size(75, 23);
            this.btnToggleSelection.TabIndex = 16;
            this.btnToggleSelection.Text = "YES";
            this.btnToggleSelection.UseVisualStyleBackColor = true;
            this.btnToggleSelection.Click += new System.EventHandler(this.btnToggleSelection_Click);
            // 
            // SearchAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 176);
            this.ControlBox = false;
            this.Controls.Add(this.btnToggleSelection);
            this.Controls.Add(this.lbSelection);
            this.Controls.Add(this.lbCase);
            this.Controls.Add(this.btnToggleCaseSense);
            this.Controls.Add(this.lbMatches);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplace);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximumSize = new System.Drawing.Size(465, 215);
            this.MinimumSize = new System.Drawing.Size(465, 215);
            this.Name = "SearchAndReplace";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find/Replace";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchAndReplace_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Label lbMatches;
        private System.Windows.Forms.Button btnToggleCaseSense;
        private System.Windows.Forms.Label lbCase;
        private System.Windows.Forms.Label lbSelection;
        private System.Windows.Forms.Button btnToggleSelection;
    }
}