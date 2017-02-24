namespace EbPsiAnimationEditor
{
    partial class ClosingBox
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
            this.closingGif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.closingGif)).BeginInit();
            this.SuspendLayout();
            // 
            // closingGif
            // 
            this.closingGif.Image = global::EbPsiAnimationEditor.Properties.Resources.Closing;
            this.closingGif.Location = new System.Drawing.Point(0, 0);
            this.closingGif.Name = "closingGif";
            this.closingGif.Size = new System.Drawing.Size(160, 120);
            this.closingGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closingGif.TabIndex = 0;
            this.closingGif.TabStop = false;
            // 
            // ClosingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(17, 20);
            this.ControlBox = false;
            this.Controls.Add(this.closingGif);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClosingBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Closing...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingBox_FormClosing);
            this.Load += new System.EventHandler(this.ClosingBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.closingGif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox closingGif;
    }
}