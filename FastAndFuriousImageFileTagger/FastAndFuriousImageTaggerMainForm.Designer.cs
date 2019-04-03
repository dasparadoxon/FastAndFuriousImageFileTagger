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
            this.NumberOfImagesInDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.tagOfCurrentFilePanel = new System.Windows.Forms.Panel();
            this.tagsForThisFile_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.TagBoxLabel = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.NewTagPanel = new System.Windows.Forms.Panel();
            this.AddTagButton = new System.Windows.Forms.Button();
            this.NewTagLabel = new System.Windows.Forms.Label();
            this.newTag_textBox = new System.Windows.Forms.TextBox();
            this.DirectoryChangeButton = new System.Windows.Forms.Button();
            this.onlyShowNonTaggedImages_CheckBox = new System.Windows.Forms.CheckBox();
            this.renameBasePanel = new System.Windows.Forms.Panel();
            this.button_index_rename = new System.Windows.Forms.Button();
            this.RenameBaseNameButton = new System.Windows.Forms.Button();
            this.RenameTextBoxLabel = new System.Windows.Forms.Label();
            this.renameBase_textBox = new System.Windows.Forms.TextBox();
            this.directoryInformationPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MainMenuStripe = new System.Windows.Forms.MenuStrip();
            this.tagMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTagsetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToDesktopButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TagSetsLabel = new System.Windows.Forms.Label();
            this.TagSetButton1 = new System.Windows.Forms.Button();
            this.TagSetButton2 = new System.Windows.Forms.Button();
            this.TagSetButton3 = new System.Windows.Forms.Button();
            this.TagSetButton4 = new System.Windows.Forms.Button();
            this.TagSetButton5 = new System.Windows.Forms.Button();
            this.TagSetButton6 = new System.Windows.Forms.Button();
            this.tagOfCurrentFilePanel.SuspendLayout();
            this.NewTagPanel.SuspendLayout();
            this.renameBasePanel.SuspendLayout();
            this.directoryInformationPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MainMenuStripe.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviousImageButton
            // 
            this.PreviousImageButton.Location = new System.Drawing.Point(3, 7);
            this.PreviousImageButton.Name = "PreviousImageButton";
            this.PreviousImageButton.Size = new System.Drawing.Size(75, 23);
            this.PreviousImageButton.TabIndex = 0;
            this.PreviousImageButton.Text = "<<";
            this.PreviousImageButton.UseVisualStyleBackColor = true;
            this.PreviousImageButton.Click += new System.EventHandler(this.PreviousImageButtonClick);
            // 
            // NextImageButton
            // 
            this.NextImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextImageButton.Location = new System.Drawing.Point(893, 7);
            this.NextImageButton.Name = "NextImageButton";
            this.NextImageButton.Size = new System.Drawing.Size(75, 23);
            this.NextImageButton.TabIndex = 1;
            this.NextImageButton.Text = ">>";
            this.NextImageButton.UseVisualStyleBackColor = true;
            this.NextImageButton.Click += new System.EventHandler(this.NextImageButton_click);
            // 
            // NumberOfImagesInDirectoryTextbox
            // 
            this.NumberOfImagesInDirectoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberOfImagesInDirectoryTextbox.Location = new System.Drawing.Point(84, 7);
            this.NumberOfImagesInDirectoryTextbox.Name = "NumberOfImagesInDirectoryTextbox";
            this.NumberOfImagesInDirectoryTextbox.ReadOnly = true;
            this.NumberOfImagesInDirectoryTextbox.Size = new System.Drawing.Size(803, 20);
            this.NumberOfImagesInDirectoryTextbox.TabIndex = 3;
            this.NumberOfImagesInDirectoryTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tagOfCurrentFilePanel
            // 
            this.tagOfCurrentFilePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagOfCurrentFilePanel.BackColor = System.Drawing.SystemColors.Control;
            this.tagOfCurrentFilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagOfCurrentFilePanel.Controls.Add(this.tagsForThisFile_checkedListBox);
            this.tagOfCurrentFilePanel.Controls.Add(this.TagBoxLabel);
            this.tagOfCurrentFilePanel.Location = new System.Drawing.Point(743, 11);
            this.tagOfCurrentFilePanel.Name = "tagOfCurrentFilePanel";
            this.tagOfCurrentFilePanel.Size = new System.Drawing.Size(225, 490);
            this.tagOfCurrentFilePanel.TabIndex = 4;
            // 
            // tagsForThisFile_checkedListBox
            // 
            this.tagsForThisFile_checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsForThisFile_checkedListBox.FormattingEnabled = true;
            this.tagsForThisFile_checkedListBox.Location = new System.Drawing.Point(13, 29);
            this.tagsForThisFile_checkedListBox.Name = "tagsForThisFile_checkedListBox";
            this.tagsForThisFile_checkedListBox.Size = new System.Drawing.Size(200, 439);
            this.tagsForThisFile_checkedListBox.TabIndex = 1;
            this.tagsForThisFile_checkedListBox.SelectedIndexChanged += new System.EventHandler(this.AssignedTagWasDeactivated_TagsForThisFileListCheckBox);
            // 
            // TagBoxLabel
            // 
            this.TagBoxLabel.AutoSize = true;
            this.TagBoxLabel.Location = new System.Drawing.Point(10, 12);
            this.TagBoxLabel.Name = "TagBoxLabel";
            this.TagBoxLabel.Size = new System.Drawing.Size(100, 13);
            this.TagBoxLabel.TabIndex = 0;
            this.TagBoxLabel.Text = "Current Image Tags";
            this.TagBoxLabel.Click += new System.EventHandler(this.TagBoxLabel_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.ForeColor = System.Drawing.Color.Red;
            this.DeleteButton.Location = new System.Drawing.Point(796, 507);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 23);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "DELETE IMAGE";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_clicked);
            // 
            // NewTagPanel
            // 
            this.NewTagPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewTagPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewTagPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewTagPanel.Controls.Add(this.AddTagButton);
            this.NewTagPanel.Controls.Add(this.NewTagLabel);
            this.NewTagPanel.Controls.Add(this.newTag_textBox);
            this.NewTagPanel.Location = new System.Drawing.Point(13, 662);
            this.NewTagPanel.Name = "NewTagPanel";
            this.NewTagPanel.Size = new System.Drawing.Size(974, 64);
            this.NewTagPanel.TabIndex = 5;
            // 
            // AddTagButton
            // 
            this.AddTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTagButton.Location = new System.Drawing.Point(862, 19);
            this.AddTagButton.Name = "AddTagButton";
            this.AddTagButton.Size = new System.Drawing.Size(95, 23);
            this.AddTagButton.TabIndex = 2;
            this.AddTagButton.Text = "Add Tag";
            this.AddTagButton.UseVisualStyleBackColor = true;
            this.AddTagButton.Click += new System.EventHandler(this.AddTagButton_AddTag_clicked);
            // 
            // NewTagLabel
            // 
            this.NewTagLabel.AutoSize = true;
            this.NewTagLabel.Location = new System.Drawing.Point(13, 25);
            this.NewTagLabel.Name = "NewTagLabel";
            this.NewTagLabel.Size = new System.Drawing.Size(72, 13);
            this.NewTagLabel.TabIndex = 1;
            this.NewTagLabel.Text = "New Tag (F3)";
            this.NewTagLabel.Click += new System.EventHandler(this.NewTagLabel_Click);
            // 
            // newTag_textBox
            // 
            this.newTag_textBox.AcceptsReturn = true;
            this.newTag_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.newTag_textBox.Location = new System.Drawing.Point(98, 22);
            this.newTag_textBox.Name = "newTag_textBox";
            this.newTag_textBox.Size = new System.Drawing.Size(758, 20);
            this.newTag_textBox.TabIndex = 0;
            this.newTag_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewTag_textBox_KeyDown);
            this.newTag_textBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.NewTag_textBox_PreviewKeyDown);
            // 
            // DirectoryChangeButton
            // 
            this.DirectoryChangeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryChangeButton.Location = new System.Drawing.Point(3, 36);
            this.DirectoryChangeButton.Name = "DirectoryChangeButton";
            this.DirectoryChangeButton.Size = new System.Drawing.Size(621, 23);
            this.DirectoryChangeButton.TabIndex = 6;
            this.DirectoryChangeButton.Text = "Change Directory";
            this.DirectoryChangeButton.UseVisualStyleBackColor = true;
            this.DirectoryChangeButton.Click += new System.EventHandler(this.DirectoryChangeButton_clicked);
            // 
            // onlyShowNonTaggedImages_CheckBox
            // 
            this.onlyShowNonTaggedImages_CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.onlyShowNonTaggedImages_CheckBox.AutoSize = true;
            this.onlyShowNonTaggedImages_CheckBox.Checked = true;
            this.onlyShowNonTaggedImages_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyShowNonTaggedImages_CheckBox.Location = new System.Drawing.Point(630, 40);
            this.onlyShowNonTaggedImages_CheckBox.Name = "onlyShowNonTaggedImages_CheckBox";
            this.onlyShowNonTaggedImages_CheckBox.Size = new System.Drawing.Size(168, 17);
            this.onlyShowNonTaggedImages_CheckBox.TabIndex = 7;
            this.onlyShowNonTaggedImages_CheckBox.Text = "Only show non-tagged images";
            this.onlyShowNonTaggedImages_CheckBox.UseVisualStyleBackColor = true;
            this.onlyShowNonTaggedImages_CheckBox.CheckedChanged += new System.EventHandler(this.onlyShowNonTaggedImages_CheckBox_CheckedChanged);
            this.onlyShowNonTaggedImages_CheckBox.Click += new System.EventHandler(this.onlyShowNonTaggedImagesCheckbox_clicked);
            // 
            // renameBasePanel
            // 
            this.renameBasePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renameBasePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.renameBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renameBasePanel.Controls.Add(this.button_index_rename);
            this.renameBasePanel.Controls.Add(this.RenameBaseNameButton);
            this.renameBasePanel.Controls.Add(this.RenameTextBoxLabel);
            this.renameBasePanel.Controls.Add(this.renameBase_textBox);
            this.renameBasePanel.Location = new System.Drawing.Point(13, 781);
            this.renameBasePanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.renameBasePanel.Name = "renameBasePanel";
            this.renameBasePanel.Size = new System.Drawing.Size(973, 61);
            this.renameBasePanel.TabIndex = 6;
            // 
            // button_index_rename
            // 
            this.button_index_rename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_index_rename.Location = new System.Drawing.Point(847, 15);
            this.button_index_rename.Name = "button_index_rename";
            this.button_index_rename.Size = new System.Drawing.Size(110, 23);
            this.button_index_rename.TabIndex = 3;
            this.button_index_rename.Text = "Index Rename (F2)";
            this.button_index_rename.UseVisualStyleBackColor = true;
            this.button_index_rename.Click += new System.EventHandler(this.Button_index_rename_click);
            // 
            // RenameBaseNameButton
            // 
            this.RenameBaseNameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RenameBaseNameButton.Location = new System.Drawing.Point(761, 16);
            this.RenameBaseNameButton.Name = "RenameBaseNameButton";
            this.RenameBaseNameButton.Size = new System.Drawing.Size(80, 23);
            this.RenameBaseNameButton.TabIndex = 2;
            this.RenameBaseNameButton.Text = "Rename";
            this.RenameBaseNameButton.UseVisualStyleBackColor = true;
            this.RenameBaseNameButton.Click += new System.EventHandler(this.RenameBaseNameButton_Click);
            // 
            // RenameTextBoxLabel
            // 
            this.RenameTextBoxLabel.AutoSize = true;
            this.RenameTextBoxLabel.Location = new System.Drawing.Point(17, 21);
            this.RenameTextBoxLabel.Name = "RenameTextBoxLabel";
            this.RenameTextBoxLabel.Size = new System.Drawing.Size(68, 13);
            this.RenameTextBoxLabel.TabIndex = 1;
            this.RenameTextBoxLabel.Text = "Rename (F1)";
            this.RenameTextBoxLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // renameBase_textBox
            // 
            this.renameBase_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.renameBase_textBox.Location = new System.Drawing.Point(97, 18);
            this.renameBase_textBox.Name = "renameBase_textBox";
            this.renameBase_textBox.Size = new System.Drawing.Size(658, 20);
            this.renameBase_textBox.TabIndex = 0;
            this.renameBase_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Event_RenameBaseFile_KeyDown);
            // 
            // directoryInformationPanel
            // 
            this.directoryInformationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryInformationPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.directoryInformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryInformationPanel.Controls.Add(this.CopyToDesktopButton);
            this.directoryInformationPanel.Controls.Add(this.onlyShowNonTaggedImages_CheckBox);
            this.directoryInformationPanel.Controls.Add(this.DirectoryChangeButton);
            this.directoryInformationPanel.Controls.Add(this.PreviousImageButton);
            this.directoryInformationPanel.Controls.Add(this.NumberOfImagesInDirectoryTextbox);
            this.directoryInformationPanel.Controls.Add(this.NextImageButton);
            this.directoryInformationPanel.Location = new System.Drawing.Point(13, 39);
            this.directoryInformationPanel.Name = "directoryInformationPanel";
            this.directoryInformationPanel.Size = new System.Drawing.Size(973, 67);
            this.directoryInformationPanel.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.DeleteButton);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.tagOfCurrentFilePanel);
            this.panel2.Location = new System.Drawing.Point(13, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(973, 545);
            this.panel2.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(721, 518);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MainMenuStripe
            // 
            this.MainMenuStripe.BackColor = System.Drawing.SystemColors.ControlLight;
            this.MainMenuStripe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagMenu,
            this.aboutToolStripMenuItem});
            this.MainMenuStripe.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStripe.Name = "MainMenuStripe";
            this.MainMenuStripe.Padding = new System.Windows.Forms.Padding(0);
            this.MainMenuStripe.Size = new System.Drawing.Size(999, 24);
            this.MainMenuStripe.TabIndex = 11;
            this.MainMenuStripe.Text = "menuStrip1";
            // 
            // tagMenu
            // 
            this.tagMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTagsToolStripMenuItem,
            this.manageTagsetsToolStripMenuItem});
            this.tagMenu.Name = "tagMenu";
            this.tagMenu.Size = new System.Drawing.Size(44, 24);
            this.tagMenu.Text = "Tags";
            // 
            // editTagsToolStripMenuItem
            // 
            this.editTagsToolStripMenuItem.Name = "editTagsToolStripMenuItem";
            this.editTagsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editTagsToolStripMenuItem.Text = "Edit tags...";
            this.editTagsToolStripMenuItem.Click += new System.EventHandler(this.EditTagsToolStripMenuItem_Click);
            // 
            // manageTagsetsToolStripMenuItem
            // 
            this.manageTagsetsToolStripMenuItem.Name = "manageTagsetsToolStripMenuItem";
            this.manageTagsetsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.manageTagsetsToolStripMenuItem.Text = "Manage Tagsets...";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statisticsToolStripMenuItem,
            this.AboutMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.aboutToolStripMenuItem.Text = "Options";
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.statisticsToolStripMenuItem.Text = "Statistics...";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(129, 22);
            this.AboutMenuItem.Text = "About...";
            // 
            // CopyToDesktopButton
            // 
            this.CopyToDesktopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyToDesktopButton.Location = new System.Drawing.Point(796, 36);
            this.CopyToDesktopButton.Name = "CopyToDesktopButton";
            this.CopyToDesktopButton.Size = new System.Drawing.Size(172, 23);
            this.CopyToDesktopButton.TabIndex = 4;
            this.CopyToDesktopButton.Text = "Copy to Desktop (F4)";
            this.CopyToDesktopButton.UseVisualStyleBackColor = true;
            this.CopyToDesktopButton.Click += new System.EventHandler(this.CopyToDesktopButtonClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TagSetButton6);
            this.panel1.Controls.Add(this.TagSetButton5);
            this.panel1.Controls.Add(this.TagSetButton4);
            this.panel1.Controls.Add(this.TagSetButton3);
            this.panel1.Controls.Add(this.TagSetButton2);
            this.panel1.Controls.Add(this.TagSetButton1);
            this.panel1.Controls.Add(this.TagSetsLabel);
            this.panel1.Location = new System.Drawing.Point(12, 732);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 43);
            this.panel1.TabIndex = 6;
            // 
            // TagSetsLabel
            // 
            this.TagSetsLabel.AutoSize = true;
            this.TagSetsLabel.Location = new System.Drawing.Point(18, 15);
            this.TagSetsLabel.Name = "TagSetsLabel";
            this.TagSetsLabel.Size = new System.Drawing.Size(50, 13);
            this.TagSetsLabel.TabIndex = 1;
            this.TagSetsLabel.Text = "Tag Sets";
            // 
            // TagSetButton1
            // 
            this.TagSetButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton1.Location = new System.Drawing.Point(98, 10);
            this.TagSetButton1.Name = "TagSetButton1";
            this.TagSetButton1.Size = new System.Drawing.Size(105, 23);
            this.TagSetButton1.TabIndex = 3;
            this.TagSetButton1.Text = "TagSet (1)";
            this.TagSetButton1.UseVisualStyleBackColor = true;
            // 
            // TagSetButton2
            // 
            this.TagSetButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton2.Location = new System.Drawing.Point(209, 10);
            this.TagSetButton2.Name = "TagSetButton2";
            this.TagSetButton2.Size = new System.Drawing.Size(123, 23);
            this.TagSetButton2.TabIndex = 4;
            this.TagSetButton2.Text = "TagSet (2)";
            this.TagSetButton2.UseVisualStyleBackColor = true;
            // 
            // TagSetButton3
            // 
            this.TagSetButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton3.Location = new System.Drawing.Point(338, 9);
            this.TagSetButton3.Name = "TagSetButton3";
            this.TagSetButton3.Size = new System.Drawing.Size(119, 23);
            this.TagSetButton3.TabIndex = 5;
            this.TagSetButton3.Text = "TagSet (3)";
            this.TagSetButton3.UseVisualStyleBackColor = true;
            // 
            // TagSetButton4
            // 
            this.TagSetButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton4.Location = new System.Drawing.Point(463, 9);
            this.TagSetButton4.Name = "TagSetButton4";
            this.TagSetButton4.Size = new System.Drawing.Size(151, 23);
            this.TagSetButton4.TabIndex = 6;
            this.TagSetButton4.Text = "TagSet (4)";
            this.TagSetButton4.UseVisualStyleBackColor = true;
            // 
            // TagSetButton5
            // 
            this.TagSetButton5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton5.Location = new System.Drawing.Point(620, 9);
            this.TagSetButton5.Name = "TagSetButton5";
            this.TagSetButton5.Size = new System.Drawing.Size(167, 23);
            this.TagSetButton5.TabIndex = 7;
            this.TagSetButton5.Text = "TagSet (5)";
            this.TagSetButton5.UseVisualStyleBackColor = true;
            // 
            // TagSetButton6
            // 
            this.TagSetButton6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagSetButton6.Location = new System.Drawing.Point(793, 9);
            this.TagSetButton6.Name = "TagSetButton6";
            this.TagSetButton6.Size = new System.Drawing.Size(165, 23);
            this.TagSetButton6.TabIndex = 8;
            this.TagSetButton6.Text = "TagSet (6)";
            this.TagSetButton6.UseVisualStyleBackColor = true;
            // 
            // FastAndFuriousImageTagger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(999, 848);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.NewTagPanel);
            this.Controls.Add(this.renameBasePanel);
            this.Controls.Add(this.directoryInformationPanel);
            this.Controls.Add(this.MainMenuStripe);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenuStripe;
            this.MaximumSize = new System.Drawing.Size(1200, 1200);
            this.MinimumSize = new System.Drawing.Size(542, 582);
            this.Name = "FastAndFuriousImageTagger";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Fast and Furious Image Tagger";
            this.Load += new System.EventHandler(this.FastAndFuriousImageTagger_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown_Event);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            this.tagOfCurrentFilePanel.ResumeLayout(false);
            this.tagOfCurrentFilePanel.PerformLayout();
            this.NewTagPanel.ResumeLayout(false);
            this.NewTagPanel.PerformLayout();
            this.renameBasePanel.ResumeLayout(false);
            this.renameBasePanel.PerformLayout();
            this.directoryInformationPanel.ResumeLayout(false);
            this.directoryInformationPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MainMenuStripe.ResumeLayout(false);
            this.MainMenuStripe.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_index_rename;
        private System.Windows.Forms.MenuStrip MainMenuStripe;
        private System.Windows.Forms.ToolStripMenuItem tagMenu;
        private System.Windows.Forms.ToolStripMenuItem editTagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTagsetsToolStripMenuItem;
        private System.Windows.Forms.Button CopyToDesktopButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TagSetsLabel;
        private System.Windows.Forms.Button TagSetButton6;
        private System.Windows.Forms.Button TagSetButton5;
        private System.Windows.Forms.Button TagSetButton4;
        private System.Windows.Forms.Button TagSetButton3;
        private System.Windows.Forms.Button TagSetButton2;
        private System.Windows.Forms.Button TagSetButton1;
    }
}

