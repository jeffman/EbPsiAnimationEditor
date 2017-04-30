namespace EbPsiAnimationEditor
{
    partial class RomConfigBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RomConfigBox));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.parametersGroup = new System.Windows.Forms.GroupBox();
            this.defaultParameters = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.psiAnimationCount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.psiPalettes = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.psiArrangements = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.psiAnimationInfo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.namesGroup = new System.Windows.Forms.GroupBox();
            this.animationName = new System.Windows.Forms.TextBox();
            this.animationNames = new System.Windows.Forms.ListBox();
            this.freeRangesGroup = new System.Windows.Forms.GroupBox();
            this.defaultFreeRanges = new System.Windows.Forms.Button();
            this.deleteRange = new System.Windows.Forms.Button();
            this.insertRange = new System.Windows.Forms.Button();
            this.rangeEnd = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rangeStart = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.freeRangesList = new System.Windows.Forms.ListView();
            this.startColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lengthColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rangeApplyButton = new System.Windows.Forms.Button();
            this.parametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psiAnimationCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiPalettes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiArrangements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiAnimationInfo)).BeginInit();
            this.namesGroup.SuspendLayout();
            this.freeRangesGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeStart)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(464, 426);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(380, 426);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "PSI animation info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Treat all addresses as relative to the start of an unheadered ROM.";
            // 
            // parametersGroup
            // 
            this.parametersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parametersGroup.Controls.Add(this.defaultParameters);
            this.parametersGroup.Controls.Add(this.label9);
            this.parametersGroup.Controls.Add(this.psiAnimationCount);
            this.parametersGroup.Controls.Add(this.label8);
            this.parametersGroup.Controls.Add(this.psiPalettes);
            this.parametersGroup.Controls.Add(this.label6);
            this.parametersGroup.Controls.Add(this.label7);
            this.parametersGroup.Controls.Add(this.psiArrangements);
            this.parametersGroup.Controls.Add(this.label4);
            this.parametersGroup.Controls.Add(this.label5);
            this.parametersGroup.Controls.Add(this.psiAnimationInfo);
            this.parametersGroup.Controls.Add(this.label3);
            this.parametersGroup.Controls.Add(this.label1);
            this.parametersGroup.Location = new System.Drawing.Point(284, 38);
            this.parametersGroup.Name = "parametersGroup";
            this.parametersGroup.Size = new System.Drawing.Size(255, 191);
            this.parametersGroup.TabIndex = 12;
            this.parametersGroup.TabStop = false;
            this.parametersGroup.Text = "Animation parameters (advanced!)";
            // 
            // defaultParameters
            // 
            this.defaultParameters.Location = new System.Drawing.Point(164, 123);
            this.defaultParameters.Name = "defaultParameters";
            this.defaultParameters.Size = new System.Drawing.Size(75, 23);
            this.defaultParameters.TabIndex = 23;
            this.defaultParameters.Text = "Defaults";
            this.defaultParameters.UseVisualStyleBackColor = true;
            this.defaultParameters.Click += new System.EventHandler(this.defaultParameters_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(6, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 32);
            this.label9.TabIndex = 22;
            this.label9.Text = "Parameter changes will not take effect until you re-open the ROM.";
            // 
            // psiAnimationCount
            // 
            this.psiAnimationCount.Location = new System.Drawing.Point(164, 19);
            this.psiAnimationCount.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.psiAnimationCount.Name = "psiAnimationCount";
            this.psiAnimationCount.Size = new System.Drawing.Size(75, 20);
            this.psiAnimationCount.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "PSI animation count";
            // 
            // psiPalettes
            // 
            this.psiPalettes.Hexadecimal = true;
            this.psiPalettes.Location = new System.Drawing.Point(164, 97);
            this.psiPalettes.Name = "psiPalettes";
            this.psiPalettes.Size = new System.Drawing.Size(75, 20);
            this.psiPalettes.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "0x";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Palettes";
            // 
            // psiArrangements
            // 
            this.psiArrangements.Hexadecimal = true;
            this.psiArrangements.Location = new System.Drawing.Point(164, 71);
            this.psiArrangements.Name = "psiArrangements";
            this.psiArrangements.Size = new System.Drawing.Size(75, 20);
            this.psiArrangements.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "0x";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Arrangements pointer table";
            // 
            // psiAnimationInfo
            // 
            this.psiAnimationInfo.Hexadecimal = true;
            this.psiAnimationInfo.Location = new System.Drawing.Point(164, 45);
            this.psiAnimationInfo.Name = "psiAnimationInfo";
            this.psiAnimationInfo.Size = new System.Drawing.Size(75, 20);
            this.psiAnimationInfo.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "0x";
            // 
            // namesGroup
            // 
            this.namesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namesGroup.Controls.Add(this.animationName);
            this.namesGroup.Controls.Add(this.animationNames);
            this.namesGroup.Location = new System.Drawing.Point(12, 38);
            this.namesGroup.Name = "namesGroup";
            this.namesGroup.Size = new System.Drawing.Size(266, 191);
            this.namesGroup.TabIndex = 13;
            this.namesGroup.TabStop = false;
            this.namesGroup.Text = "Animation names";
            // 
            // animationName
            // 
            this.animationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animationName.Location = new System.Drawing.Point(6, 19);
            this.animationName.MaxLength = 128;
            this.animationName.Name = "animationName";
            this.animationName.Size = new System.Drawing.Size(254, 20);
            this.animationName.TabIndex = 1;
            this.animationName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.animationName_KeyDown);
            this.animationName.Leave += new System.EventHandler(this.animationName_Leave);
            // 
            // animationNames
            // 
            this.animationNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animationNames.FormattingEnabled = true;
            this.animationNames.IntegralHeight = false;
            this.animationNames.Location = new System.Drawing.Point(6, 45);
            this.animationNames.Name = "animationNames";
            this.animationNames.ScrollAlwaysVisible = true;
            this.animationNames.Size = new System.Drawing.Size(254, 140);
            this.animationNames.TabIndex = 0;
            this.animationNames.SelectedIndexChanged += new System.EventHandler(this.animationNames_SelectedIndexChanged);
            // 
            // freeRangesGroup
            // 
            this.freeRangesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.freeRangesGroup.Controls.Add(this.rangeApplyButton);
            this.freeRangesGroup.Controls.Add(this.defaultFreeRanges);
            this.freeRangesGroup.Controls.Add(this.deleteRange);
            this.freeRangesGroup.Controls.Add(this.insertRange);
            this.freeRangesGroup.Controls.Add(this.rangeEnd);
            this.freeRangesGroup.Controls.Add(this.label12);
            this.freeRangesGroup.Controls.Add(this.label13);
            this.freeRangesGroup.Controls.Add(this.rangeStart);
            this.freeRangesGroup.Controls.Add(this.label11);
            this.freeRangesGroup.Controls.Add(this.label10);
            this.freeRangesGroup.Controls.Add(this.freeRangesList);
            this.freeRangesGroup.Location = new System.Drawing.Point(12, 235);
            this.freeRangesGroup.Name = "freeRangesGroup";
            this.freeRangesGroup.Size = new System.Drawing.Size(527, 185);
            this.freeRangesGroup.TabIndex = 14;
            this.freeRangesGroup.TabStop = false;
            this.freeRangesGroup.Text = "Free ranges";
            // 
            // defaultFreeRanges
            // 
            this.defaultFreeRanges.Location = new System.Drawing.Point(296, 156);
            this.defaultFreeRanges.Name = "defaultFreeRanges";
            this.defaultFreeRanges.Size = new System.Drawing.Size(75, 23);
            this.defaultFreeRanges.TabIndex = 24;
            this.defaultFreeRanges.Text = "Defaults";
            this.defaultFreeRanges.UseVisualStyleBackColor = true;
            this.defaultFreeRanges.Click += new System.EventHandler(this.defaultFreeRanges_Click);
            // 
            // deleteRange
            // 
            this.deleteRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteRange.Image = ((System.Drawing.Image)(resources.GetObject("deleteRange.Image")));
            this.deleteRange.Location = new System.Drawing.Point(266, 155);
            this.deleteRange.Name = "deleteRange";
            this.deleteRange.Size = new System.Drawing.Size(24, 24);
            this.deleteRange.TabIndex = 22;
            this.deleteRange.UseVisualStyleBackColor = true;
            this.deleteRange.Click += new System.EventHandler(this.deleteRange_Click);
            // 
            // insertRange
            // 
            this.insertRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertRange.Image = ((System.Drawing.Image)(resources.GetObject("insertRange.Image")));
            this.insertRange.Location = new System.Drawing.Point(266, 125);
            this.insertRange.Name = "insertRange";
            this.insertRange.Size = new System.Drawing.Size(24, 24);
            this.insertRange.TabIndex = 21;
            this.insertRange.UseVisualStyleBackColor = true;
            this.insertRange.Click += new System.EventHandler(this.insertRange_Click);
            // 
            // rangeEnd
            // 
            this.rangeEnd.Hexadecimal = true;
            this.rangeEnd.Location = new System.Drawing.Point(337, 45);
            this.rangeEnd.Name = "rangeEnd";
            this.rangeEnd.Size = new System.Drawing.Size(75, 20);
            this.rangeEnd.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(322, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "0x";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(264, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "End";
            // 
            // rangeStart
            // 
            this.rangeStart.Hexadecimal = true;
            this.rangeStart.Location = new System.Drawing.Point(337, 19);
            this.rangeStart.Name = "rangeStart";
            this.rangeStart.Size = new System.Drawing.Size(75, 20);
            this.rangeStart.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(322, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "0x";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Start";
            // 
            // freeRangesList
            // 
            this.freeRangesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.freeRangesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.startColumn,
            this.endColumn,
            this.lengthColumn});
            this.freeRangesList.FullRowSelect = true;
            this.freeRangesList.HideSelection = false;
            this.freeRangesList.Location = new System.Drawing.Point(6, 19);
            this.freeRangesList.MultiSelect = false;
            this.freeRangesList.Name = "freeRangesList";
            this.freeRangesList.Size = new System.Drawing.Size(254, 160);
            this.freeRangesList.TabIndex = 0;
            this.freeRangesList.UseCompatibleStateImageBehavior = false;
            this.freeRangesList.View = System.Windows.Forms.View.Details;
            this.freeRangesList.SelectedIndexChanged += new System.EventHandler(this.freeRangesList_SelectedIndexChanged);
            // 
            // startColumn
            // 
            this.startColumn.Text = "Start";
            this.startColumn.Width = 80;
            // 
            // endColumn
            // 
            this.endColumn.Text = "End";
            this.endColumn.Width = 80;
            // 
            // lengthColumn
            // 
            this.lengthColumn.Text = "Length";
            this.lengthColumn.Width = 80;
            // 
            // rangeApplyButton
            // 
            this.rangeApplyButton.Location = new System.Drawing.Point(337, 71);
            this.rangeApplyButton.Name = "rangeApplyButton";
            this.rangeApplyButton.Size = new System.Drawing.Size(75, 23);
            this.rangeApplyButton.TabIndex = 25;
            this.rangeApplyButton.Text = "Apply";
            this.rangeApplyButton.UseVisualStyleBackColor = true;
            this.rangeApplyButton.Click += new System.EventHandler(this.rangeApplyButton_Click);
            // 
            // RomConfigBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(551, 461);
            this.Controls.Add(this.freeRangesGroup);
            this.Controls.Add(this.namesGroup);
            this.Controls.Add(this.parametersGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RomConfigBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ROM configuration";
            this.Load += new System.EventHandler(this.RomConfigBox_Load);
            this.parametersGroup.ResumeLayout(false);
            this.parametersGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psiAnimationCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiPalettes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiArrangements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psiAnimationInfo)).EndInit();
            this.namesGroup.ResumeLayout(false);
            this.namesGroup.PerformLayout();
            this.freeRangesGroup.ResumeLayout(false);
            this.freeRangesGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox parametersGroup;
        private System.Windows.Forms.NumericUpDown psiAnimationInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown psiAnimationCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown psiPalettes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown psiArrangements;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button defaultParameters;
        private System.Windows.Forms.GroupBox namesGroup;
        private System.Windows.Forms.TextBox animationName;
        private System.Windows.Forms.ListBox animationNames;
        private System.Windows.Forms.GroupBox freeRangesGroup;
        private System.Windows.Forms.ListView freeRangesList;
        private System.Windows.Forms.ColumnHeader startColumn;
        private System.Windows.Forms.ColumnHeader endColumn;
        private System.Windows.Forms.ColumnHeader lengthColumn;
        private System.Windows.Forms.NumericUpDown rangeEnd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown rangeStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button deleteRange;
        private System.Windows.Forms.Button insertRange;
        private System.Windows.Forms.Button defaultFreeRanges;
        private System.Windows.Forms.Button rangeApplyButton;
    }
}