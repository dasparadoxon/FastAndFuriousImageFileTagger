namespace FastAndFuriousImageFileTagger
{
    partial class FastAndFuriousImageTagger
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastAndFuriousImageTagger));
            this.PreviousImageButton = new System.Windows.Forms.Button();
            this.NextImageButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NumberOfImagesInDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.tagOfCurrentFilePanel = new System.Windows.Forms.Panel();
            this.tagsForThisFile_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.TagBoxLabel = new System.Windows.Forms.Label();
            this.NewTagPanel = new System.Windows.Forms.Panel();
            this.AddTagButton = new System.Windows.Forms.Button();
            this.NewTagLabel = new System.Windows.Forms.Label();
            this.newTag_textBox = new System.Windows.Forms.TextBox();
            this.DirectoryChangeButton = new System.Windows.Forms.Button();
            this.onlyShowNonTaggedImages_CheckBox = new System.Windows.Forms.CheckBox();
            this.renameBasePanel = new System.Windows.Forms.Panel();
            this.RenameBaseNameButton = new System.Windows.Forms.Button();
            this.RenameTextBoxLabel = new System.Windows.Forms.Label();
            this.renameBase_textBox = new System.Windows.Forms.TextBox();
            this.directoryInformationPanel = new System.Windows.Forms.Panel();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.additionalOptions1Panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tagOfCurrentFilePanel.SuspendLayout();
            this.NewTagPanel.SuspendLayout();
            this.renameBasePanel.SuspendLayout();
            this.directoryInformationPanel.SuspendLayout();
            this.additionalOptions1Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviousImageButton
            // 
            this.PreviousImageButton.Location = new System.Drawing.Point(3, 3);
            this.PreviousImageButton.Name = "PreviousImageButton";
            this.PreviousImageButton.Size = new System.Drawing.Size(75, 23);
            this.PreviousImageButton.TabIndex = 0;
            this.PreviousImageButton.Text = "<<";
            this.PreviousImageButton.UseVisualStyleBackColor = true;
            this.PreviousImageButton.Click += new System.EventHandler(this.PreviousImageButtonClick);
            // 
            // NextImageButton
            // 
            this.NextImageButton.Location = new System.Drawing.Point(229, 3);
            this.NextImageButton.Name = "NextImageButton";
            this.NextImageButton.Size = new System.Drawing.Size(75, 23);
            this.NextImageButton.TabIndex = 1;
            this.NextImageButton.Text = ">>";
            this.NextImageButton.UseVisualStyleBackColor = true;
            this.NextImageButton.Click += new System.EventHandler(this.NextImageButton_click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(309, 278);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // NumberOfImagesInDirectoryTextbox
            // 
            this.NumberOfImagesInDirectoryTextbox.Location = new System.Drawing.Point(84, 6);
            this.NumberOfImagesInDirectoryTextbox.Name = "NumberOfImagesInDirectoryTextbox";
            this.NumberOfImagesInDirectoryTextbox.ReadOnly = true;
            this.NumberOfImagesInDirectoryTextbox.Size = new System.Drawing.Size(142, 20);
            this.NumberOfImagesInDirectoryTextbox.TabIndex = 3;
            this.NumberOfImagesInDirectoryTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tagOfCurrentFilePanel
            // 
            this.tagOfCurrentFilePanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tagOfCurrentFilePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tagOfCurrentFilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagOfCurrentFilePanel.Controls.Add(this.tagsForThisFile_checkedListBox);
            this.tagOfCurrentFilePanel.Controls.Add(this.TagBoxLabel);
            this.tagOfCurrentFilePanel.Location = new System.Drawing.Point(327, 95);
            this.tagOfCurrentFilePanel.Name = "tagOfCurrentFilePanel";
            this.tagOfCurrentFilePanel.Size = new System.Drawing.Size(184, 278);
            this.tagOfCurrentFilePanel.TabIndex = 4;
            // 
            // tagsForThisFile_checkedListBox
            // 
            this.tagsForThisFile_checkedListBox.FormattingEnabled = true;
            this.tagsForThisFile_checkedListBox.Location = new System.Drawing.Point(13, 44);
            this.tagsForThisFile_checkedListBox.Name = "tagsForThisFile_checkedListBox";
            this.tagsForThisFile_checkedListBox.Size = new System.Drawing.Size(159, 214);
            this.tagsForThisFile_checkedListBox.TabIndex = 1;
            this.tagsForThisFile_checkedListBox.SelectedIndexChanged += new System.EventHandler(this.AssignedTagWasDeactivated_TagsForThisFileListCheckBox);
            // 
            // TagBoxLabel
            // 
            this.TagBoxLabel.AutoSize = true;
            this.TagBoxLabel.Location = new System.Drawing.Point(10, 17);
            this.TagBoxLabel.Name = "TagBoxLabel";
            this.TagBoxLabel.Size = new System.Drawing.Size(118, 13);
            this.TagBoxLabel.TabIndex = 0;
            this.TagBoxLabel.Text = "TAGS OF THIS IMAGE";
            this.TagBoxLabel.Click += new System.EventHandler(this.TagBoxLabel_Click);
            // 
            // NewTagPanel
            // 
            this.NewTagPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NewTagPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewTagPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewTagPanel.Controls.Add(this.AddTagButton);
            this.NewTagPanel.Controls.Add(this.NewTagLabel);
            this.NewTagPanel.Controls.Add(this.newTag_textBox);
            this.NewTagPanel.Location = new System.Drawing.Point(12, 396);
            this.NewTagPanel.Name = "NewTagPanel";
            this.NewTagPanel.Size = new System.Drawing.Size(499, 64);
            this.NewTagPanel.TabIndex = 5;
            // 
            // AddTagButton
            // 
            this.AddTagButton.Location = new System.Drawing.Point(412, 22);
            this.AddTagButton.Name = "AddTagButton";
            this.AddTagButton.Size = new System.Drawing.Size(75, 23);
            this.AddTagButton.TabIndex = 2;
            this.AddTagButton.Text = "Add Tag";
            this.AddTagButton.UseVisualStyleBackColor = true;
            this.AddTagButton.Click += new System.EventHandler(this.AddTagButton_AddTag_clicked);
            // 
            // NewTagLabel
            // 
            this.NewTagLabel.AutoSize = true;
            this.NewTagLabel.Location = new System.Drawing.Point(17, 25);
            this.NewTagLabel.Name = "NewTagLabel";
            this.NewTagLabel.Size = new System.Drawing.Size(58, 13);
            this.NewTagLabel.TabIndex = 1;
            this.NewTagLabel.Text = "NEW TAG";
            this.NewTagLabel.Click += new System.EventHandler(this.NewTagLabel_Click);
            // 
            // newTag_textBox
            // 
            this.newTag_textBox.AcceptsReturn = true;
            this.newTag_textBox.AutoCompleteCustomSource.AddRange(new string[] {
            "animal",
            "cute",
            "human",
            "actor",
            "architecture",
            "inspiration",
            "cult",
            "religion",
            "funny",
            "art",
            "artistic",
            "totexture",
            "house",
            "building",
            "scene",
            "sky",
            "horizon"});
            this.newTag_textBox.Location = new System.Drawing.Point(81, 22);
            this.newTag_textBox.Name = "newTag_textBox";
            this.newTag_textBox.Size = new System.Drawing.Size(320, 20);
            this.newTag_textBox.TabIndex = 0;
            this.newTag_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyUp);
            // 
            // DirectoryChangeButton
            // 
            this.DirectoryChangeButton.Location = new System.Drawing.Point(3, 3);
            this.DirectoryChangeButton.Name = "DirectoryChangeButton";
            this.DirectoryChangeButton.Size = new System.Drawing.Size(176, 23);
            this.DirectoryChangeButton.TabIndex = 6;
            this.DirectoryChangeButton.Text = "Change Directory";
            this.DirectoryChangeButton.UseVisualStyleBackColor = true;
            this.DirectoryChangeButton.Click += new System.EventHandler(this.DirectoryChangeButton_clicked);
            // 
            // onlyShowNonTaggedImages_CheckBox
            // 
            this.onlyShowNonTaggedImages_CheckBox.AutoSize = true;
            this.onlyShowNonTaggedImages_CheckBox.Checked = true;
            this.onlyShowNonTaggedImages_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyShowNonTaggedImages_CheckBox.Location = new System.Drawing.Point(4, 32);
            this.onlyShowNonTaggedImages_CheckBox.Name = "onlyShowNonTaggedImages_CheckBox";
            this.onlyShowNonTaggedImages_CheckBox.Size = new System.Drawing.Size(168, 17);
            this.onlyShowNonTaggedImages_CheckBox.TabIndex = 7;
            this.onlyShowNonTaggedImages_CheckBox.Text = "Only show non-tagged images";
            this.onlyShowNonTaggedImages_CheckBox.UseVisualStyleBackColor = true;
            this.onlyShowNonTaggedImages_CheckBox.Click += new System.EventHandler(this.onlyShowNonTaggedImagesCheckbox_clicked);
            // 
            // renameBasePanel
            // 
            this.renameBasePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.renameBasePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.renameBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renameBasePanel.Controls.Add(this.RenameBaseNameButton);
            this.renameBasePanel.Controls.Add(this.RenameTextBoxLabel);
            this.renameBasePanel.Controls.Add(this.renameBase_textBox);
            this.renameBasePanel.Location = new System.Drawing.Point(12, 475);
            this.renameBasePanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.renameBasePanel.Name = "renameBasePanel";
            this.renameBasePanel.Size = new System.Drawing.Size(499, 60);
            this.renameBasePanel.TabIndex = 6;
            // 
            // RenameBaseNameButton
            // 
            this.RenameBaseNameButton.Location = new System.Drawing.Point(412, 16);
            this.RenameBaseNameButton.Name = "RenameBaseNameButton";
            this.RenameBaseNameButton.Size = new System.Drawing.Size(75, 23);
            this.RenameBaseNameButton.TabIndex = 2;
            this.RenameBaseNameButton.Text = "Rename";
            this.RenameBaseNameButton.UseVisualStyleBackColor = true;
            this.RenameBaseNameButton.Click += new System.EventHandler(this.RenameBaseNameButton_Click);
            // 
            // RenameTextBoxLabel
            // 
            this.RenameTextBoxLabel.AutoSize = true;
            this.RenameTextBoxLabel.Location = new System.Drawing.Point(14, 21);
            this.RenameTextBoxLabel.Name = "RenameTextBoxLabel";
            this.RenameTextBoxLabel.Size = new System.Drawing.Size(84, 13);
            this.RenameTextBoxLabel.TabIndex = 1;
            this.RenameTextBoxLabel.Text = "RENAME BASE";
            this.RenameTextBoxLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // renameBase_textBox
            // 
            this.renameBase_textBox.AcceptsReturn = true;
            this.renameBase_textBox.AutoCompleteCustomSource.AddRange(new string[] {
            "animal",
            "cute",
            "human",
            "actor",
            "architecture",
            "inspiration",
            "cult",
            "religion",
            "funny",
            "art",
            "artistic",
            "totexture",
            "house",
            "building",
            "scene",
            "sky",
            "horizon"});
            this.renameBase_textBox.Location = new System.Drawing.Point(104, 18);
            this.renameBase_textBox.Name = "renameBase_textBox";
            this.renameBase_textBox.Size = new System.Drawing.Size(297, 20);
            this.renameBase_textBox.TabIndex = 0;
            // 
            // directoryInformationPanel
            // 
            this.directoryInformationPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.directoryInformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryInformationPanel.Controls.Add(this.DeleteButton);
            this.directoryInformationPanel.Controls.Add(this.PreviousImageButton);
            this.directoryInformationPanel.Controls.Add(this.NumberOfImagesInDirectoryTextbox);
            this.directoryInformationPanel.Controls.Add(this.NextImageButton);
            this.directoryInformationPanel.Location = new System.Drawing.Point(12, 12);
            this.directoryInformationPanel.Name = "directoryInformationPanel";
            this.directoryInformationPanel.Size = new System.Drawing.Size(309, 67);
            this.directoryInformationPanel.TabIndex = 8;
            // 
            // DeleteButton
            // 
            this.DeleteButton.ForeColor = System.Drawing.Color.Red;
            this.DeleteButton.Location = new System.Drawing.Point(229, 39);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "REMOVE";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_clicked);
            // 
            // additionalOptions1Panel
            // 
            this.additionalOptions1Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.additionalOptions1Panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.additionalOptions1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.additionalOptions1Panel.Controls.Add(this.DirectoryChangeButton);
            this.additionalOptions1Panel.Controls.Add(this.onlyShowNonTaggedImages_CheckBox);
            this.additionalOptions1Panel.Location = new System.Drawing.Point(327, 12);
            this.additionalOptions1Panel.Name = "additionalOptions1Panel";
            this.additionalOptions1Panel.Size = new System.Drawing.Size(184, 67);
            this.additionalOptions1Panel.TabIndex = 9;
            // 
            // FastAndFuriousImageTagger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(526, 548);
            this.Controls.Add(this.NewTagPanel);
            this.Controls.Add(this.additionalOptions1Panel);
            this.Controls.Add(this.renameBasePanel);
            this.Controls.Add(this.tagOfCurrentFilePanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.directoryInformationPanel);
            this.MaximumSize = new System.Drawing.Size(1200, 1200);
            this.MinimumSize = new System.Drawing.Size(542, 582);
            this.Name = "FastAndFuriousImageTagger";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Fast and Furious Image Tagger";
            this.Load += new System.EventHandler(this.FastAndFuriousImageTagger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tagOfCurrentFilePanel.ResumeLayout(false);
            this.tagOfCurrentFilePanel.PerformLayout();
            this.NewTagPanel.ResumeLayout(false);
            this.NewTagPanel.PerformLayout();
            this.renameBasePanel.ResumeLayout(false);
            this.renameBasePanel.PerformLayout();
            this.directoryInformationPanel.ResumeLayout(false);
            this.directoryInformationPanel.PerformLayout();
            this.additionalOptions1Panel.ResumeLayout(false);
            this.additionalOptions1Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PreviousImageButton;
        private System.Windows.Forms.Button NextImageButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox NumberOfImagesInDirectoryTextbox;
        private System.Windows.Forms.Panel tagOfCurrentFilePanel;
        private System.Windows.Forms.Label TagBoxLabel;
        private System.Windows.Forms.CheckedListBox tagsForThisFile_checkedListBox;
        private System.Windows.Forms.Panel NewTagPanel;
        private System.Windows.Forms.TextBox newTag_textBox;
        private System.Windows.Forms.Label NewTagLabel;
        private System.Windows.Forms.Button AddTagButton;
        private System.Windows.Forms.Button DirectoryChangeButton;
        private System.Windows.Forms.CheckBox onlyShowNonTaggedImages_CheckBox;
        private System.Windows.Forms.Panel renameBasePanel;
        private System.Windows.Forms.Button RenameBaseNameButton;
        private System.Windows.Forms.Label RenameTextBoxLabel;
        private System.Windows.Forms.TextBox renameBase_textBox;
        private System.Windows.Forms.Panel directoryInformationPanel;
        private System.Windows.Forms.Panel additionalOptions1Panel;
        private System.Windows.Forms.Button DeleteButton;
    }
}

