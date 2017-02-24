namespace EbPsiAnimationEditor
{
    partial class FrameEditor
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.frameBox = new System.Windows.Forms.PictureBox();
            this.tileBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.Controls.Add(this.frameBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.Controls.Add(this.tileBox);
            this.splitContainer.Size = new System.Drawing.Size(748, 537);
            this.splitContainer.SplitterDistance = 385;
            this.splitContainer.TabIndex = 2;
            // 
            // frameBox
            // 
            this.frameBox.Location = new System.Drawing.Point(3, 3);
            this.frameBox.Name = "frameBox";
            this.frameBox.Size = new System.Drawing.Size(100, 50);
            this.frameBox.TabIndex = 0;
            this.frameBox.TabStop = false;
            this.frameBox.Paint += new System.Windows.Forms.PaintEventHandler(this.frameBox_Paint);
            this.frameBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frameBox_MouseMove);
            this.frameBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frameBox_MouseUp);
            // 
            // tileBox
            // 
            this.tileBox.Location = new System.Drawing.Point(3, 3);
            this.tileBox.Name = "tileBox";
            this.tileBox.Size = new System.Drawing.Size(100, 50);
            this.tileBox.TabIndex = 1;
            this.tileBox.TabStop = false;
            this.tileBox.Paint += new System.Windows.Forms.PaintEventHandler(this.tileBox_Paint);
            this.tileBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tileBox_MouseClick);
            // 
            // FrameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer);
            this.Name = "FrameEditor";
            this.Size = new System.Drawing.Size(754, 543);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.frameBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox frameBox;
        private System.Windows.Forms.PictureBox tileBox;
    }
}
