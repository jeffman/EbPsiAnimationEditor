namespace EbPsiAnimationEditor
{
    partial class TilesetBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.tilesetList = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.importMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importNewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importOverwriteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesetPreview = new System.Windows.Forms.PictureBox();
            this.importDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.importMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tilesetPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tileset";
            // 
            // tilesetList
            // 
            this.tilesetList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tilesetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetList.FormattingEnabled = true;
            this.tilesetList.Location = new System.Drawing.Point(53, 12);
            this.tilesetList.Name = "tilesetList";
            this.tilesetList.Size = new System.Drawing.Size(215, 21);
            this.tilesetList.TabIndex = 2;
            this.tilesetList.SelectedIndexChanged += new System.EventHandler(this.tilesetList_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(193, 229);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(12, 167);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(256, 23);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importButton.Location = new System.Drawing.Point(12, 109);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(256, 23);
            this.importButton.TabIndex = 14;
            this.importButton.Text = "Import...";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(12, 138);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(256, 23);
            this.exportButton.TabIndex = 16;
            this.exportButton.Text = "Export...";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // importMenu
            // 
            this.importMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importNewMenu,
            this.importOverwriteMenu});
            this.importMenu.Name = "importMenu";
            this.importMenu.Size = new System.Drawing.Size(206, 48);
            // 
            // importNewMenu
            // 
            this.importNewMenu.Name = "importNewMenu";
            this.importNewMenu.Size = new System.Drawing.Size(205, 22);
            this.importNewMenu.Text = "As new tileset";
            this.importNewMenu.Click += new System.EventHandler(this.importNewMenu_Click);
            // 
            // importOverwriteMenu
            // 
            this.importOverwriteMenu.Name = "importOverwriteMenu";
            this.importOverwriteMenu.Size = new System.Drawing.Size(205, 22);
            this.importOverwriteMenu.Text = "Overwrite selected tileset";
            this.importOverwriteMenu.Click += new System.EventHandler(this.importOverwriteMenu_Click);
            // 
            // tilesetPreview
            // 
            this.tilesetPreview.Location = new System.Drawing.Point(12, 39);
            this.tilesetPreview.Name = "tilesetPreview";
            this.tilesetPreview.Size = new System.Drawing.Size(256, 64);
            this.tilesetPreview.TabIndex = 19;
            this.tilesetPreview.TabStop = false;
            this.tilesetPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.tilesetPreview_Paint);
            // 
            // importDialog
            // 
            this.importDialog.Filter = "Tileset dumps|*.smc;*.bin";
            // 
            // exportDialog
            // 
            this.exportDialog.Filter = "Tileset dump|*.smc|Tileset dump|*.bin|All files|*.*";
            // 
            // TilesetBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 264);
            this.Controls.Add(this.tilesetPreview);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tilesetList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TilesetBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit tilesets";
            this.Load += new System.EventHandler(this.TilesetBox_Load);
            this.importMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tilesetPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tilesetList;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ContextMenuStrip importMenu;
        private System.Windows.Forms.ToolStripMenuItem importNewMenu;
        private System.Windows.Forms.ToolStripMenuItem importOverwriteMenu;
        private System.Windows.Forms.PictureBox tilesetPreview;
        private System.Windows.Forms.OpenFileDialog importDialog;
        private System.Windows.Forms.SaveFileDialog exportDialog;
    }
}