namespace FastAndFuriousImageFileTagger
{
    partial class TagEditor
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
            this.checkedListBox_Tags = new System.Windows.Forms.CheckedListBox();
            this.button_tagDialogDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox_Tags
            // 
            this.checkedListBox_Tags.FormattingEnabled = true;
            this.checkedListBox_Tags.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox_Tags.Name = "checkedListBox_Tags";
            this.checkedListBox_Tags.Size = new System.Drawing.Size(776, 424);
            this.checkedListBox_Tags.TabIndex = 0;
            // 
            // button_tagDialogDone
            // 
            this.button_tagDialogDone.Location = new System.Drawing.Point(621, 445);
            this.button_tagDialogDone.Name = "button_tagDialogDone";
            this.button_tagDialogDone.Size = new System.Drawing.Size(167, 23);
            this.button_tagDialogDone.TabIndex = 1;
            this.button_tagDialogDone.Text = "Done";
            this.button_tagDialogDone.UseVisualStyleBackColor = true;
            this.button_tagDialogDone.Click += new System.EventHandler(this.Button_tagDialogDone_click);
            // 
            // TagEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.button_tagDialogDone);
            this.Controls.Add(this.checkedListBox_Tags);
            this.Name = "TagEditor";
            this.Text = "TagEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_Tags;
        private System.Windows.Forms.Button button_tagDialogDone;
    }
}