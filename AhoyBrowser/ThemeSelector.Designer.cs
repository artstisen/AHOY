namespace AhoyBrowser
{
    partial class ThemeSelector
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
            this.picA = new System.Windows.Forms.PictureBox();
            this.picB = new System.Windows.Forms.PictureBox();
            this.btnSelectA = new System.Windows.Forms.Button();
            this.btnSelectB = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMore = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelectC = new System.Windows.Forms.Button();
            this.picC = new System.Windows.Forms.PictureBox();
            this.lbEscapeText = new System.Windows.Forms.Label();
            this.lbAdditionalInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picC)).BeginInit();
            this.SuspendLayout();
            // 
            // picA
            // 
            this.picA.Cursor = System.Windows.Forms.Cursors.Default;
            this.picA.Location = new System.Drawing.Point(13, 72);
            this.picA.Name = "picA";
            this.picA.Size = new System.Drawing.Size(200, 200);
            this.picA.TabIndex = 0;
            this.picA.TabStop = false;
            this.toolTip1.SetToolTip(this.picA, "Select Dark Theme");
            this.picA.Click += new System.EventHandler(this.picA_Click);
            this.picA.MouseEnter += new System.EventHandler(this.picA_MouseEnter);
            // 
            // picB
            // 
            this.picB.Cursor = System.Windows.Forms.Cursors.Default;
            this.picB.Location = new System.Drawing.Point(425, 72);
            this.picB.Name = "picB";
            this.picB.Size = new System.Drawing.Size(200, 200);
            this.picB.TabIndex = 1;
            this.picB.TabStop = false;
            this.toolTip1.SetToolTip(this.picB, "Select Light Theme");
            this.picB.Click += new System.EventHandler(this.picB_Click);
            this.picB.MouseEnter += new System.EventHandler(this.picB_MouseEnter);
            // 
            // btnSelectA
            // 
            this.btnSelectA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelectA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectA.Location = new System.Drawing.Point(13, 278);
            this.btnSelectA.Name = "btnSelectA";
            this.btnSelectA.Size = new System.Drawing.Size(200, 23);
            this.btnSelectA.TabIndex = 0;
            this.btnSelectA.Text = "Dark";
            this.toolTip1.SetToolTip(this.btnSelectA, "Select Dark Theme");
            this.btnSelectA.UseVisualStyleBackColor = true;
            this.btnSelectA.Click += new System.EventHandler(this.btnSelectA_Click);
            // 
            // btnSelectB
            // 
            this.btnSelectB.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelectB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectB.Location = new System.Drawing.Point(425, 278);
            this.btnSelectB.Name = "btnSelectB";
            this.btnSelectB.Size = new System.Drawing.Size(200, 23);
            this.btnSelectB.TabIndex = 1;
            this.btnSelectB.Text = "Light";
            this.toolTip1.SetToolTip(this.btnSelectB, "Select Light Theme");
            this.btnSelectB.UseVisualStyleBackColor = true;
            this.btnSelectB.Click += new System.EventHandler(this.btnSelectB_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbTitle.Location = new System.Drawing.Point(14, 32);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(196, 16);
            this.lbTitle.TabIndex = 13;
            this.lbTitle.Text = "Please select Color Theme";
            // 
            // btnMore
            // 
            this.btnMore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnMore.Location = new System.Drawing.Point(506, 326);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(119, 23);
            this.btnMore.TabIndex = 15;
            this.btnMore.Text = "More...";
            this.toolTip1.SetToolTip(this.btnMore, "Open Style Editor");
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCancel.Location = new System.Drawing.Point(425, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel (ESC)");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelectC
            // 
            this.btnSelectC.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelectC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectC.Location = new System.Drawing.Point(219, 278);
            this.btnSelectC.Name = "btnSelectC";
            this.btnSelectC.Size = new System.Drawing.Size(200, 23);
            this.btnSelectC.TabIndex = 17;
            this.btnSelectC.Text = "Blue";
            this.toolTip1.SetToolTip(this.btnSelectC, "Select Blue Theme");
            this.btnSelectC.UseVisualStyleBackColor = true;
            this.btnSelectC.Click += new System.EventHandler(this.btnSelectC_Click);
            // 
            // picC
            // 
            this.picC.Cursor = System.Windows.Forms.Cursors.Default;
            this.picC.Location = new System.Drawing.Point(219, 72);
            this.picC.Name = "picC";
            this.picC.Size = new System.Drawing.Size(200, 200);
            this.picC.TabIndex = 18;
            this.picC.TabStop = false;
            this.toolTip1.SetToolTip(this.picC, "Select Blue Theme");
            this.picC.Click += new System.EventHandler(this.picC_Click);
            this.picC.MouseEnter += new System.EventHandler(this.picC_MouseEnter);
            // 
            // lbEscapeText
            // 
            this.lbEscapeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEscapeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbEscapeText.Location = new System.Drawing.Point(475, 9);
            this.lbEscapeText.Name = "lbEscapeText";
            this.lbEscapeText.Size = new System.Drawing.Size(153, 15);
            this.lbEscapeText.TabIndex = 14;
            this.lbEscapeText.Text = "Press ESC to cancel";
            this.lbEscapeText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbAdditionalInfo
            // 
            this.lbAdditionalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAdditionalInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.lbAdditionalInfo.Location = new System.Drawing.Point(14, 331);
            this.lbAdditionalInfo.Name = "lbAdditionalInfo";
            this.lbAdditionalInfo.Size = new System.Drawing.Size(405, 15);
            this.lbAdditionalInfo.TabIndex = 19;
            this.lbAdditionalInfo.Text = "--";
            // 
            // ThemeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 361);
            this.ControlBox = false;
            this.Controls.Add(this.lbAdditionalInfo);
            this.Controls.Add(this.btnSelectC);
            this.Controls.Add(this.picC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.lbEscapeText);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnSelectB);
            this.Controls.Add(this.btnSelectA);
            this.Controls.Add(this.picB);
            this.Controls.Add(this.picA);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(656, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(656, 400);
            this.Name = "ThemeSelector";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Theme";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ThemeSelector_KeyPress);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ThemeSelector_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picA;
        private System.Windows.Forms.PictureBox picB;
        private System.Windows.Forms.Button btnSelectA;
        private System.Windows.Forms.Button btnSelectB;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbEscapeText;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelectC;
        private System.Windows.Forms.PictureBox picC;
        private System.Windows.Forms.Label lbAdditionalInfo;
    }
}