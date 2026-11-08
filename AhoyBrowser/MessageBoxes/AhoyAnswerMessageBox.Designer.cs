namespace AhoyBrowser
{
    partial class AhoyAnswerMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AhoyAnswerMessageBox));
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.lbInformation = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnYes.Location = new System.Drawing.Point(266, 227);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "&Yes";
            this.toolTip1.SetToolTip(this.btnYes, "Yes");
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnNo.Location = new System.Drawing.Point(347, 227);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "&No";
            this.toolTip1.SetToolTip(this.btnNo, "No");
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // lbInformation
            // 
            this.lbInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lbInformation.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInformation.ForeColor = System.Drawing.Color.Gainsboro;
            this.lbInformation.Location = new System.Drawing.Point(12, 23);
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size(410, 192);
            this.lbInformation.TabIndex = 4;
            this.lbInformation.Text = "- Information -";
            this.lbInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.btnCancel.Location = new System.Drawing.Point(15, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AhoyAnswerMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbInformation);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Icon = global::AhoyBrowser.Properties.Resources.Ahoy;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "AhoyAnswerMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label lbInformation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}