namespace EbPsiAnimationEditor
{
    partial class ParameterBox
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.frameInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.paletteInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.enemyColorDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.enemyColorInterval = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.unknownA = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.unknownB = new System.Windows.Forms.NumericUpDown();
            this.enemyColor = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.target = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.paletteMarker = new System.Windows.Forms.Label();
            this.tilesetList = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.frameInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyColorDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyColorInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknownB)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(180, 317);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(99, 317);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // frameInterval
            // 
            this.frameInterval.Location = new System.Drawing.Point(129, 64);
            this.frameInterval.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.frameInterval.Name = "frameInterval";
            this.frameInterval.Size = new System.Drawing.Size(72, 20);
            this.frameInterval.TabIndex = 2;
            this.frameInterval.ValueChanged += new System.EventHandler(this.frameInterval_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frame interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Palette interval";
            // 
            // paletteInterval
            // 
            this.paletteInterval.Location = new System.Drawing.Point(129, 90);
            this.paletteInterval.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.paletteInterval.Name = "paletteInterval";
            this.paletteInterval.Size = new System.Drawing.Size(72, 20);
            this.paletteInterval.TabIndex = 4;
            this.paletteInterval.ValueChanged += new System.EventHandler(this.paletteInterval_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Enemy color delay";
            // 
            // enemyColorDelay
            // 
            this.enemyColorDelay.Location = new System.Drawing.Point(129, 12);
            this.enemyColorDelay.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.enemyColorDelay.Name = "enemyColorDelay";
            this.enemyColorDelay.Size = new System.Drawing.Size(72, 20);
            this.enemyColorDelay.TabIndex = 6;
            this.enemyColorDelay.ValueChanged += new System.EventHandler(this.enemyColorDelay_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Enemy color interval";
            // 
            // enemyColorInterval
            // 
            this.enemyColorInterval.Location = new System.Drawing.Point(129, 38);
            this.enemyColorInterval.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.enemyColorInterval.Name = "enemyColorInterval";
            this.enemyColorInterval.Size = new System.Drawing.Size(72, 20);
            this.enemyColorInterval.TabIndex = 8;
            this.enemyColorInterval.ValueChanged += new System.EventHandler(this.enemyColorInterval_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Unknown A";
            // 
            // unknownA
            // 
            this.unknownA.Location = new System.Drawing.Point(129, 116);
            this.unknownA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.unknownA.Name = "unknownA";
            this.unknownA.Size = new System.Drawing.Size(72, 20);
            this.unknownA.TabIndex = 10;
            this.unknownA.ValueChanged += new System.EventHandler(this.unknownA_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Unknown B";
            // 
            // unknownB
            // 
            this.unknownB.Location = new System.Drawing.Point(129, 142);
            this.unknownB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.unknownB.Name = "unknownB";
            this.unknownB.Size = new System.Drawing.Size(72, 20);
            this.unknownB.TabIndex = 12;
            this.unknownB.ValueChanged += new System.EventHandler(this.unknownB_ValueChanged);
            // 
            // enemyColor
            // 
            this.enemyColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enemyColor.Location = new System.Drawing.Point(176, 258);
            this.enemyColor.Name = "enemyColor";
            this.enemyColor.Size = new System.Drawing.Size(34, 34);
            this.enemyColor.TabIndex = 14;
            this.enemyColor.Click += new System.EventHandler(this.enemyColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Target";
            // 
            // target
            // 
            this.target.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.target.FormattingEnabled = true;
            this.target.Location = new System.Drawing.Point(129, 168);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(124, 21);
            this.target.TabIndex = 17;
            this.target.SelectedIndexChanged += new System.EventHandler(this.target_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(173, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Enemy color";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Palette";
            // 
            // paletteMarker
            // 
            this.paletteMarker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteMarker.Location = new System.Drawing.Point(12, 258);
            this.paletteMarker.Name = "paletteMarker";
            this.paletteMarker.Size = new System.Drawing.Size(130, 34);
            this.paletteMarker.TabIndex = 20;
            // 
            // tilesetList
            // 
            this.tilesetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetList.FormattingEnabled = true;
            this.tilesetList.Location = new System.Drawing.Point(129, 195);
            this.tilesetList.Name = "tilesetList";
            this.tilesetList.Size = new System.Drawing.Size(124, 21);
            this.tilesetList.TabIndex = 22;
            this.tilesetList.SelectedIndexChanged += new System.EventHandler(this.tilesetList_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Tileset";
            // 
            // ParameterBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(267, 352);
            this.Controls.Add(this.tilesetList);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.paletteMarker);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.target);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.enemyColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.unknownB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.unknownA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.enemyColorInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.enemyColorDelay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.paletteInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frameInterval);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ParameterBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit parameters";
            ((System.ComponentModel.ISupportInitialize)(this.frameInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyColorDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyColorInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknownB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.NumericUpDown frameInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown paletteInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown enemyColorDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown enemyColorInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown unknownA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown unknownB;
        private System.Windows.Forms.Label enemyColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox target;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label paletteMarker;
        private System.Windows.Forms.ComboBox tilesetList;
        private System.Windows.Forms.Label label10;
    }
}