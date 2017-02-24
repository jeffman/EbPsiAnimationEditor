namespace EbPsiAnimationEditor
{
    partial class SettingsBox
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
            this.fastCompress = new System.Windows.Forms.CheckBox();
            this.multiThreaded = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridColor = new System.Windows.Forms.Label();
            this.tileColor = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // fastCompress
            // 
            this.fastCompress.AutoSize = true;
            this.fastCompress.Location = new System.Drawing.Point(12, 12);
            this.fastCompress.Name = "fastCompress";
            this.fastCompress.Size = new System.Drawing.Size(94, 17);
            this.fastCompress.TabIndex = 0;
            this.fastCompress.Text = "Fast compress";
            this.fastCompress.UseVisualStyleBackColor = true;
            // 
            // multiThreaded
            // 
            this.multiThreaded.AutoSize = true;
            this.multiThreaded.Location = new System.Drawing.Point(12, 35);
            this.multiThreaded.Name = "multiThreaded";
            this.multiThreaded.Size = new System.Drawing.Size(90, 17);
            this.multiThreaded.TabIndex = 1;
            this.multiThreaded.Text = "Multithreaded";
            this.multiThreaded.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Grid color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tile selection color";
            // 
            // gridColor
            // 
            this.gridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridColor.Location = new System.Drawing.Point(113, 74);
            this.gridColor.Name = "gridColor";
            this.gridColor.Size = new System.Drawing.Size(48, 48);
            this.gridColor.TabIndex = 4;
            this.gridColor.Click += new System.EventHandler(this.gridColor_Click);
            // 
            // tileColor
            // 
            this.tileColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tileColor.Location = new System.Drawing.Point(113, 140);
            this.tileColor.Name = "tileColor";
            this.tileColor.Size = new System.Drawing.Size(48, 48);
            this.tileColor.TabIndex = 5;
            this.tileColor.Click += new System.EventHandler(this.tileColor_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(70, 239);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(154, 239);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // SettingsBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(241, 274);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tileColor);
            this.Controls.Add(this.gridColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.multiThreaded);
            this.Controls.Add(this.fastCompress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox fastCompress;
        private System.Windows.Forms.CheckBox multiThreaded;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label gridColor;
        private System.Windows.Forms.Label tileColor;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}