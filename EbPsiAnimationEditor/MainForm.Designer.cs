namespace EbPsiAnimationEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openRom = new System.Windows.Forms.OpenFileDialog();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dividerMenu1 = new System.Windows.Forms.ToolStripSeparator();
            this.importMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dividerMenu3 = new System.Windows.Forms.ToolStripSeparator();
            this.romConfigMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.quitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.discussionThreadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.githubMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.animationList = new System.Windows.Forms.ComboBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dividerMenu2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyImageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportGifMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dividerMenu4 = new System.Windows.Forms.ToolStripSeparator();
            this.editTilesetsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFrame = new System.Windows.Forms.Button();
            this.insertFrame = new System.Windows.Forms.Button();
            this.moveFrameDown = new System.Windows.Forms.Button();
            this.moveFrameUp = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.editParametersButton = new System.Windows.Forms.Button();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.importDialog = new System.Windows.Forms.OpenFileDialog();
            this.frameEditor = new EbPsiAnimationEditor.FrameEditor();
            this.frameList = new EbPsiAnimationEditor.CustomListView();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openRom
            // 
            this.openRom.Filter = "SNES ROM files|*.smc;*.sfc|All files|*.*";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.saveMenu,
            this.closeMenu,
            this.dividerMenu1,
            this.importMenu,
            this.exportMenu,
            this.dividerMenu3,
            this.romConfigMenu,
            this.settingsMenu,
            this.quitMenu});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // openMenu
            // 
            this.openMenu.Name = "openMenu";
            this.openMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenu.Size = new System.Drawing.Size(185, 22);
            this.openMenu.Text = "&Open ROM...";
            this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenu.Size = new System.Drawing.Size(185, 22);
            this.saveMenu.Text = "&Save ROM";
            this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
            // 
            // closeMenu
            // 
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.Size = new System.Drawing.Size(185, 22);
            this.closeMenu.Text = "&Close ROM";
            this.closeMenu.Click += new System.EventHandler(this.closeMenu_Click);
            // 
            // dividerMenu1
            // 
            this.dividerMenu1.Name = "dividerMenu1";
            this.dividerMenu1.Size = new System.Drawing.Size(182, 6);
            // 
            // importMenu
            // 
            this.importMenu.Name = "importMenu";
            this.importMenu.Size = new System.Drawing.Size(185, 22);
            this.importMenu.Text = "&Import...";
            this.importMenu.Click += new System.EventHandler(this.importMenu_Click);
            // 
            // exportMenu
            // 
            this.exportMenu.Name = "exportMenu";
            this.exportMenu.Size = new System.Drawing.Size(185, 22);
            this.exportMenu.Text = "&Export...";
            this.exportMenu.Click += new System.EventHandler(this.exportMenu_Click);
            // 
            // dividerMenu3
            // 
            this.dividerMenu3.Name = "dividerMenu3";
            this.dividerMenu3.Size = new System.Drawing.Size(182, 6);
            // 
            // romConfigMenu
            // 
            this.romConfigMenu.Name = "romConfigMenu";
            this.romConfigMenu.Size = new System.Drawing.Size(185, 22);
            this.romConfigMenu.Text = "&ROM configuration...";
            this.romConfigMenu.Click += new System.EventHandler(this.romConfigMenu_Click);
            // 
            // settingsMenu
            // 
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(185, 22);
            this.settingsMenu.Text = "Se&ttings...";
            this.settingsMenu.Click += new System.EventHandler(this.settingsMenu_Click);
            // 
            // quitMenu
            // 
            this.quitMenu.Name = "quitMenu";
            this.quitMenu.Size = new System.Drawing.Size(185, 22);
            this.quitMenu.Text = "&Quit";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discussionThreadMenu,
            this.githubMenu,
            this.aboutMenu});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // discussionThreadMenu
            // 
            this.discussionThreadMenu.Name = "discussionThreadMenu";
            this.discussionThreadMenu.Size = new System.Drawing.Size(176, 22);
            this.discussionThreadMenu.Text = "&Discussion thread...";
            // 
            // githubMenu
            // 
            this.githubMenu.Name = "githubMenu";
            this.githubMenu.Size = new System.Drawing.Size(176, 22);
            this.githubMenu.Text = "&Github...";
            this.githubMenu.Click += new System.EventHandler(this.githubMenu_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new System.Drawing.Size(176, 22);
            this.aboutMenu.Text = "&About...";
            this.aboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 777);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(752, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // animationList
            // 
            this.animationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.animationList.FormattingEnabled = true;
            this.animationList.Location = new System.Drawing.Point(12, 27);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(318, 21);
            this.animationList.TabIndex = 2;
            this.animationList.SelectedIndexChanged += new System.EventHandler(this.animationList_SelectedIndexChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewToolStripMenuItem,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(752, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenu,
            this.copyMenu,
            this.pasteMenu,
            this.dividerMenu2,
            this.copyImageMenu,
            this.exportGifMenu,
            this.dividerMenu4,
            this.editTilesetsMenu});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 20);
            this.editMenu.Text = "&Edit";
            // 
            // undoMenu
            // 
            this.undoMenu.Name = "undoMenu";
            this.undoMenu.ShortcutKeyDisplayString = "";
            this.undoMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoMenu.Size = new System.Drawing.Size(189, 22);
            this.undoMenu.Text = "&Undo";
            this.undoMenu.Click += new System.EventHandler(this.undoMenu_Click);
            // 
            // copyMenu
            // 
            this.copyMenu.Name = "copyMenu";
            this.copyMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenu.Size = new System.Drawing.Size(189, 22);
            this.copyMenu.Text = "&Copy";
            this.copyMenu.Click += new System.EventHandler(this.copyMenu_Click);
            // 
            // pasteMenu
            // 
            this.pasteMenu.Name = "pasteMenu";
            this.pasteMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteMenu.Size = new System.Drawing.Size(189, 22);
            this.pasteMenu.Text = "&Paste";
            this.pasteMenu.Click += new System.EventHandler(this.pasteMenu_Click);
            // 
            // dividerMenu2
            // 
            this.dividerMenu2.Name = "dividerMenu2";
            this.dividerMenu2.Size = new System.Drawing.Size(186, 6);
            // 
            // copyImageMenu
            // 
            this.copyImageMenu.Name = "copyImageMenu";
            this.copyImageMenu.Size = new System.Drawing.Size(189, 22);
            this.copyImageMenu.Text = "Copy as &image";
            this.copyImageMenu.Click += new System.EventHandler(this.copyImageMenu_Click);
            // 
            // exportGifMenu
            // 
            this.exportGifMenu.Name = "exportGifMenu";
            this.exportGifMenu.Size = new System.Drawing.Size(189, 22);
            this.exportGifMenu.Text = "&Export animated GIF...";
            this.exportGifMenu.Click += new System.EventHandler(this.exportGifMenu_Click);
            // 
            // dividerMenu4
            // 
            this.dividerMenu4.Name = "dividerMenu4";
            this.dividerMenu4.Size = new System.Drawing.Size(186, 6);
            // 
            // editTilesetsMenu
            // 
            this.editTilesetsMenu.Name = "editTilesetsMenu";
            this.editTilesetsMenu.Size = new System.Drawing.Size(189, 22);
            this.editTilesetsMenu.Text = "Edit &tilesets...";
            this.editTilesetsMenu.Click += new System.EventHandler(this.editTilesetsMenu_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridMenu,
            this.zoomIn,
            this.zoomOut});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // gridMenu
            // 
            this.gridMenu.Checked = true;
            this.gridMenu.CheckOnClick = true;
            this.gridMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridMenu.Name = "gridMenu";
            this.gridMenu.Size = new System.Drawing.Size(225, 22);
            this.gridMenu.Text = "&Gridlines";
            this.gridMenu.CheckedChanged += new System.EventHandler(this.gridMenu_CheckedChanged);
            // 
            // zoomIn
            // 
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.ShortcutKeyDisplayString = "Ctrl+WheelUp";
            this.zoomIn.Size = new System.Drawing.Size(225, 22);
            this.zoomIn.Text = "Zoom &in";
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.ShortcutKeyDisplayString = "Ctrl+WheelDown";
            this.zoomOut.Size = new System.Drawing.Size(225, 22);
            this.zoomOut.Text = "Zoom &out";
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // deleteFrame
            // 
            this.deleteFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteFrame.Image = ((System.Drawing.Image)(resources.GetObject("deleteFrame.Image")));
            this.deleteFrame.Location = new System.Drawing.Point(102, 750);
            this.deleteFrame.Name = "deleteFrame";
            this.deleteFrame.Size = new System.Drawing.Size(24, 24);
            this.deleteFrame.TabIndex = 8;
            this.deleteFrame.UseVisualStyleBackColor = true;
            this.deleteFrame.Click += new System.EventHandler(this.deleteFrame_Click);
            // 
            // insertFrame
            // 
            this.insertFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertFrame.Image = ((System.Drawing.Image)(resources.GetObject("insertFrame.Image")));
            this.insertFrame.Location = new System.Drawing.Point(72, 750);
            this.insertFrame.Name = "insertFrame";
            this.insertFrame.Size = new System.Drawing.Size(24, 24);
            this.insertFrame.TabIndex = 7;
            this.insertFrame.UseVisualStyleBackColor = true;
            this.insertFrame.Click += new System.EventHandler(this.insertFrame_Click);
            // 
            // moveFrameDown
            // 
            this.moveFrameDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moveFrameDown.Image = ((System.Drawing.Image)(resources.GetObject("moveFrameDown.Image")));
            this.moveFrameDown.Location = new System.Drawing.Point(42, 750);
            this.moveFrameDown.Name = "moveFrameDown";
            this.moveFrameDown.Size = new System.Drawing.Size(24, 24);
            this.moveFrameDown.TabIndex = 6;
            this.moveFrameDown.UseVisualStyleBackColor = true;
            this.moveFrameDown.Click += new System.EventHandler(this.moveFrameDown_Click);
            // 
            // moveFrameUp
            // 
            this.moveFrameUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moveFrameUp.Image = ((System.Drawing.Image)(resources.GetObject("moveFrameUp.Image")));
            this.moveFrameUp.Location = new System.Drawing.Point(12, 750);
            this.moveFrameUp.Name = "moveFrameUp";
            this.moveFrameUp.Size = new System.Drawing.Size(24, 24);
            this.moveFrameUp.TabIndex = 5;
            this.moveFrameUp.UseVisualStyleBackColor = true;
            this.moveFrameUp.Click += new System.EventHandler(this.moveFrameUp_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // editParametersButton
            // 
            this.editParametersButton.Location = new System.Drawing.Point(336, 26);
            this.editParametersButton.Name = "editParametersButton";
            this.editParametersButton.Size = new System.Drawing.Size(144, 23);
            this.editParametersButton.TabIndex = 10;
            this.editParametersButton.Text = "Edit parameters...";
            this.editParametersButton.UseVisualStyleBackColor = true;
            this.editParametersButton.Click += new System.EventHandler(this.editParametersButton_Click);
            // 
            // exportDialog
            // 
            this.exportDialog.Filter = "Animation files|*.anim";
            // 
            // importDialog
            // 
            this.importDialog.Filter = "Animation files|*.anim";
            // 
            // frameEditor
            // 
            this.frameEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameEditor.BackColor = System.Drawing.SystemColors.Control;
            this.frameEditor.CurrentTile = -1;
            this.frameEditor.Grid = true;
            this.frameEditor.GridColor = System.Drawing.Color.Gray;
            this.frameEditor.Location = new System.Drawing.Point(149, 54);
            this.frameEditor.Name = "frameEditor";
            this.frameEditor.Size = new System.Drawing.Size(591, 720);
            this.frameEditor.TabIndex = 9;
            this.frameEditor.TileColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(150)))), ((int)(((byte)(130)))), ((int)(((byte)(40)))));
            this.frameEditor.Zoom = 2;
            this.frameEditor.FrameChanged += new EbPsiAnimationEditor.FrameEditor.FrameChangedEventHandler(this.frameEditor_FrameChanged);
            // 
            // frameList
            // 
            this.frameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.frameList.HideSelection = false;
            this.frameList.Location = new System.Drawing.Point(12, 54);
            this.frameList.MultiSelect = false;
            this.frameList.Name = "frameList";
            this.frameList.Size = new System.Drawing.Size(131, 690);
            this.frameList.TabIndex = 3;
            this.frameList.UseCompatibleStateImageBehavior = false;
            this.frameList.SelectedIndexChanged += new System.EventHandler(this.frameList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 799);
            this.Controls.Add(this.editParametersButton);
            this.Controls.Add(this.frameEditor);
            this.Controls.Add(this.deleteFrame);
            this.Controls.Add(this.insertFrame);
            this.Controls.Add(this.moveFrameDown);
            this.Controls.Add(this.moveFrameUp);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.frameList);
            this.Controls.Add(this.animationList);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(520, 520);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PSI Animation Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openRom;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem discussionThreadMenu;
        private System.Windows.Forms.ToolStripMenuItem githubMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
        private System.Windows.Forms.ToolStripMenuItem openMenu;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.ToolStripMenuItem closeMenu;
        private System.Windows.Forms.ToolStripSeparator dividerMenu1;
        private System.Windows.Forms.ToolStripMenuItem quitMenu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ComboBox animationList;
        private EbPsiAnimationEditor.CustomListView frameList;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Button moveFrameUp;
        private System.Windows.Forms.Button moveFrameDown;
        private System.Windows.Forms.Button insertFrame;
        private System.Windows.Forms.Button deleteFrame;
        private FrameEditor frameEditor;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridMenu;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem zoomIn;
        private System.Windows.Forms.ToolStripMenuItem zoomOut;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem undoMenu;
        private System.Windows.Forms.ToolStripMenuItem copyMenu;
        private System.Windows.Forms.ToolStripMenuItem pasteMenu;
        private System.Windows.Forms.ToolStripSeparator dividerMenu2;
        private System.Windows.Forms.ToolStripMenuItem copyImageMenu;
        private System.Windows.Forms.Button editParametersButton;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripMenuItem romConfigMenu;
        private System.Windows.Forms.ToolStripMenuItem importMenu;
        private System.Windows.Forms.ToolStripMenuItem exportMenu;
        private System.Windows.Forms.ToolStripSeparator dividerMenu3;
        private System.Windows.Forms.ToolStripMenuItem exportGifMenu;
        private System.Windows.Forms.ToolStripSeparator dividerMenu4;
        private System.Windows.Forms.ToolStripMenuItem editTilesetsMenu;
        private System.Windows.Forms.SaveFileDialog exportDialog;
        private System.Windows.Forms.OpenFileDialog importDialog;
    }
}

