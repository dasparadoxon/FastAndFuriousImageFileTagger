using System;
//using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Data.SQLite;

/** FAST AND FURIOUS IMAGE FILE TAGGER 
 * 
 * Quickly add tags to your image filename
 * 
 * 2019 Tom Trottel, dasparadoxon
 * 
 * Public Domain or whatever license is good for you, but keeps this programm free as in freedom.
 * 
 * Version 0.1.2 (18.04.2019) [VERIFIED GITHUB TEST 3]
 * 
 */

enum TagListStoringChoice { TextFile, SQLiteDatabase };

namespace FastAndFuriousImageFileTagger
{

    public partial class FastAndFuriousImageTagger : Form
    {
        #region Fields

        private string databaseFileName = "FastAndFuriousImageFileTagger.db";

        private class ImageFileNameIndex
        {
            static public void Increase()
            {

                Properties.Settings.Default.imageIndex++;

                Properties.Settings.Default.Save();

            }

            static public long Get()
            {
                Console.WriteLine("Setting:" + Properties.Settings.Default["imageIndex"]);

                return (long)Properties.Settings.Default["imageIndex"];
            }

        }

        List<Control> mostUsedTagsButtons = new List<Control>();

        List<String> mostUsedTags = new List<string>();

        // IMAGES

        CurrentSelectedImageFile currentSelectedImage;

        private IEnumerable<string> imageFilesInCurrentDirectory;

        Size originalSizeOfPictureBox;

        int pictureBoxLeftDistanceToContainingContainer;
        int pictureBoxTopDistanceToContainingContainer;

        // TAGGING SETUP

        private TagListStoringChoice tagListStoringChoice = TagListStoringChoice.SQLiteDatabase;

        private string tagSeperator = "_";

        private string tagStringFromBaseFileNameSeperator = "__";

        static string tagFileName = "tagFile.txt";

        string tagFileLocationAndFileName = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + tagFileName;

        // APP STATUS 

        bool showOnlyNotTaggedFiles = true;

        string userDataDirectory;

        private int imageIndex = 0;

        Image startImage;

        private FolderBrowserDialog folderBrowserDialog1;

        public AutoCompleteStringCollection tagAutoCompleteStringsCollection;

        public string workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public StringSplitOptions RemoveEmptyEntries { get; private set; }

        #endregion

        #region Constructors

        public FastAndFuriousImageTagger()
        {
            InitializeComponent();

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            currentSelectedImage = new CurrentSelectedImageFile("", "");

            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            startImage = pictureBox1.Image;

            pictureBox1.MouseWheel += PictureBox1_WheelEvent;

            SavingPictureBoxSizeAndPosition();

            UserDataDirectoryHandling();

            InitializeAutoCompletionForNewTagTextBox();

            SetUpCurrentImage();

            InitMostUsedTagButtonList();

            SetNameOfMostUsedTagButtons(GetMostUsedTags(11));
        }

        #endregion

        #region EventHandlers

        private void MostUsedQuickTagButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the
            // user file and close it.
            Console.WriteLine("Closing Application.");

            WriteTagCollectionToSQLiteFile();
        }

        /// <summary>
        /// Saves the new size of the PictureBox control so it nows where to reset when picture changes or MouseWheelButtonDown
        /// </summary>
        private void FastAndFuriousImageTagger_SizeChanged(object sender, EventArgs e)
        {
            SavingPictureBoxSizeAndPosition();
        }

        private void Button_index_rename_click(object sender, EventArgs e)
        {
            RenameBaseFilenameToIndexedBaseFilename();
        }

        private void PreviousImageButtonClick(object sender, EventArgs e)
        {

            imageIndex--;

            SetUpCurrentImage(true);
        }

        private void NextImageButton_click(object sender, EventArgs e)
        {
            imageIndex++;

            SetUpCurrentImage(true);

        }

        private void CopyToDesktopButtonClick(object sender, EventArgs e)
        {

            CopyCurrentImageFileToDesktop();
        }

        /// <summary>
        /// Tampers something so that Arrow Keys are send to the KeyDown Handler (yikes) 
        /// </summary>
        private void NewTag_textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        /// <summary>
        /// Handles Space and Enter KeyDowns in the NewTagBox
        /// </summary>
        private void NewTag_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                imageIndex--;

                SetUpCurrentImage(true);
            }

            if (e.KeyCode == Keys.Right)
            {
                imageIndex++;

                SetUpCurrentImage(true);
            }


            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("TextBox New Tag Enter : " + newTag_textBox.Text);

                AddTagToImageTagCheckBox(newTag_textBox.Text);

                AddTagToImageFile(newTag_textBox.Text, currentSelectedImage.Name);

                e.Handled = true;
            }

            if (e.KeyCode == Keys.Delete)
            {
                if (newTag_textBox.Text.Length < 2)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;

                    DeleteImage();

                    newTag_textBox.Focus();
                }
            }

            if (e.KeyCode == Keys.Space)
            {

                //newTag_textBox.Text == " " ||
                if (newTag_textBox.Text.Length < 2)
                {
                    newTag_textBox.Clear();

                    e.SuppressKeyPress = true;
                    e.Handled = true;

                    //newTag_textBox.Text = Regex.Replace(newTag_textBox.Text, "(?<Text>.*)(?:[\r\n]?(?:\r\n)?)", "${Text}\r\n");

                    //newTag_textBox.Text = Regex.Replace(newTag_textBox.Text, "\\s+\r\n", "\r\n");

                    imageIndex++;

                    SetUpCurrentImage(true);

                    // TODO : If there are no images left to process, set the initial image.
                }
            }
        }

        /// <summary>
        ///   Handles MouseWheel events on the PictureBox
        /// </summary>
        private void PictureBox1_WheelEvent(object sender, MouseEventArgs e)
        {
            bool zoom = true;

            if (e.Delta < 0)
                zoom = false;

            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            //Zoom ratio by which the images will be zoomed by default
            int zoomRatio = 10;
            //Set the zoomed width and height
            int widthZoom = pictureBox1.Width * zoomRatio / 100;
            int heightZoom = pictureBox1.Height * zoomRatio / 100;
            //zoom = true --> zoom in
            //zoom = false --> zoom out
            if (!zoom)
            {
                widthZoom *= -1;
                heightZoom *= -1;
            }
            //Add the width and height to the picture box dimensions
            pictureBox1.Width += widthZoom;
            pictureBox1.Height += heightZoom;

            // Console.WriteLine("E Delta : " + e.Delta + " / MouseWheelScrollLines : " + SystemInformation.MouseWheelScrollLines);
        }

        private Point MouseDownLocation;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Left = e.X + pictureBox1.Left - MouseDownLocation.X;
                pictureBox1.Top = e.Y + pictureBox1.Top - MouseDownLocation.Y;
            }
        }

        /// <summary>
        /// Tampers something so that Arrow Keys are send to the KeyDown Handler (yikes) 
        /// </summary>
        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        /// <summary>
        /// Handles MouseHovering over Picture Box also enables MouseWheelEvents with Focusing the PictureBox
        /// </summary>
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        /// <summary>
        /// Eventhandler, Handles HotKeys, up to now hardcoded
        /// </summary>
        private void MainForm_KeyDown_Event(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                renameBase_textBox.Select();
            }

            if (e.KeyCode == Keys.F2)
            {
                RenameBaseFilenameToIndexedBaseFilename();
            }

            if (e.KeyCode == Keys.F3)
            {
                newTag_textBox.Select();
            }

            if (e.KeyCode == Keys.F4)
                CopyCurrentImageFileToDesktop();

            if (e.KeyCode == Keys.F5)
                ChangeDirectory();

            // Only Process Those Hotkeys if neither TagBox or RenameBox is active !!
            if (!newTag_textBox.Focused && !renameBase_textBox.Focused)
            {

                if (e.KeyCode == Keys.Delete)
                {
                    DeleteImage();
                }

                if (e.KeyCode == Keys.Left)
                {
                    imageIndex--;

                    SetUpCurrentImage(true);
                }

                if (e.KeyCode == Keys.Right)
                {
                    imageIndex++;

                    SetUpCurrentImage(true);
                }

            }
        }

        private void AddTagButton_AddTag_clicked(object sender, EventArgs e)
        {
            AddTagToImageTagCheckBox(newTag_textBox.Text);

            AddTagToImageFile(newTag_textBox.Text, currentSelectedImage.Name);
        }

        private void DirectoryChangeButton_clicked(object sender, EventArgs e)
        {
            ChangeDirectory();
        }

        private void RenameBaseNameButton_Click(object sender, EventArgs e)
        {
            // are there tags in the current active image ?

            string newFileName;

            if (HasThisFileTags(currentSelectedImage.Name))
            {
                Console.WriteLine("RenameBaseNameButton_Click : Current file has Tags");

                string tags = GetTagStringOfFile(currentSelectedImage.Name);

                string newBaseFileName = renameBase_textBox.Text + Path.GetExtension(currentSelectedImage.Name);

                newFileName = tags + tagStringFromBaseFileNameSeperator + newBaseFileName;



            }
            else
                newFileName = renameBase_textBox.Text + Path.GetExtension(currentSelectedImage.Name);

            RenameFile(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name,
                currentSelectedImage.Path + Path.DirectorySeparatorChar + newFileName);

            currentSelectedImage.Name = newFileName;

            Console.WriteLine("RenameBaseNameButton_Click : New Current Image File Name : " + currentSelectedImage.Name);

            // remove them but store them temp.
            // take the new base name and put them in front of it
            // rename file
            // set current file name to the form scope !! (dont forget that)



            //renameBase_textBox.Text = GetBaseFileName(baseFileName);
        }

        private void AssignedTagWasDeactivated_TagsForThisFileListCheckBox(object sender, EventArgs e)
        {
            int selected = tagsForThisFile_checkedListBox.SelectedIndex;

            if (selected != -1)
            {
                string checkBoxText = tagsForThisFile_checkedListBox.Items[selected].ToString();

                tagsForThisFile_checkedListBox.Items.Remove(checkBoxText);

                Console.WriteLine("Removed Tag entry '" + checkBoxText + "' from TagListBox.");
            }

            // rebuild the tag string from the listbox

            string newTagString = "";

            for (int i = 0; i < tagsForThisFile_checkedListBox.Items.Count; i++)
            {
                newTagString += tagsForThisFile_checkedListBox.Items[i].ToString() + tagSeperator;
            }

            Console.WriteLine("New TagString after removing an element for the TagCheckBoxList : " + newTagString);

            // reassemble new filename

            string newWholeFileName = GetBaseFileName(currentSelectedImage.Name);

            newWholeFileName = newTagString + tagStringFromBaseFileNameSeperator + newWholeFileName;

            // test for unnecessary tagFromBaseFile Delimiter and if, remove it
            if (newWholeFileName.Substring(0, 2) == tagStringFromBaseFileNameSeperator)
                newWholeFileName = newWholeFileName.Replace(tagStringFromBaseFileNameSeperator, "");

            // correct length of tagFromBaseFile Delimiter if necessary
            if (newWholeFileName.Contains("___"))
                newWholeFileName = newWholeFileName.Replace("___", tagStringFromBaseFileNameSeperator);

            Console.WriteLine("Fully new Filename from TagCheckBoxList : " + newWholeFileName);

            RenameFile(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name,
                currentSelectedImage.Path + Path.DirectorySeparatorChar + newWholeFileName);

            currentSelectedImage.Name = newWholeFileName;

        }

        private void DeleteButton_clicked(object sender, EventArgs e)
        {
            DeleteImage();
        }

        private void onlyShowNonTaggedImagesCheckbox_clicked(object sender, EventArgs e)
        {

            showOnlyNotTaggedFiles = !showOnlyNotTaggedFiles;

            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            if (imageFilesInCurrentDirectory.Count() != 0)
            {

                string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
                string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

                UpdateBaseNameTextBox(filename);

                ParseTagsAndPopulateTagListForImage(filename);

                UpdateNumberOfImagesInDirectoryTextbox();

                currentSelectedImage.Path = directory;

                currentSelectedImage.Name = filename;

                string pathAndFileName = directory + Path.DirectorySeparatorChar + filename;

                SetCurrentImageToPictureBox(pathAndFileName);

                InitializeAutoCompletionForNewTagTextBox();

            }

        }

        private void Event_RenameBaseFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RenameBaseNameButton_Click(sender, e);

                //e.Handled = true;
                e.SuppressKeyPress = true;

                newTag_textBox.Select();
            }

        }

        private void EditTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagEditor tagEditorInstance = new TagEditor();

            tagEditorInstance.mainForm = this;

            //tagEditorInstance.TagCollection = tagAutoCompleteStringsCollection;

            //tagEditorInstance.InitializeTagCheckBoxListFromAutocompletionList();

            tagEditorInstance.Show();
        }

        #endregion

        #region SQLITE Functions

        /// <summary>
        /// Retrievs the top 10 most used tags from the database 
        /// </summary>
        /// <returns>List<string></returns>
        private List<string> GetMostUsedTags(int numberOfTags)
        {
            List<string> mostUsedTags = new List<string>();

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = "SELECT tag FROM tags ORDER BY used DESC LIMIT "+ numberOfTags.ToString();

                    sqlite_cmd.ExecuteNonQuery();

                    using (SQLiteDataReader rdr = sqlite_cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(Convert.ToString(rdr.GetString(0)));
                            mostUsedTags.Add(Convert.ToString(rdr.GetString(0)));
                        }
                    }
                }

                sqlite_conn.Close();
            }

            return mostUsedTags;
        }

        /// <summary>
        /// Adds new tag to the database
        /// </summary>
        /// <param name="tagToAdd">new tag string</param>
        private void AddTagToSQLiteDatabase(string tagToAdd)
        {

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {

                        sqlite_cmd.CommandText = "INSERT INTO tags (tag,used) VALUES ('" + tagToAdd + "',0)";

                        sqlite_cmd.ExecuteNonQuery();
                }

                sqlite_conn.Close();
            }
        }

        private void CreateSQLITEDatabase()
        {

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = "CREATE TABLE 'tags' ('tag' TEXT,'used' INTEGER);";

                    sqlite_cmd.ExecuteNonQuery();
                }

                sqlite_conn.Close();
            }

        }

        private void ImportTagsIntoDB()
        {
            SQLiteConnection sqlite_conn;

            SQLiteCommand sqlite_cmd;

            sqlite_conn = new SQLiteConnection("Data Source=" + databaseFileName + ";New=True");

            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            //sqlite_cmd.CommandText = "CREATE TABLE tags (text varchar(200),INTEGER used);";

            //sqlite_cmd.ExecuteNonQuery();

            List<String> tagList = LoadTagsFromTagFile();

            var noDupes = tagList.Distinct().ToList();

            foreach (string tag in noDupes)
            {
                sqlite_cmd.CommandText = "INSERT INTO tags (tag,used) VALUES ('" + tag + "',0)";

                sqlite_cmd.ExecuteNonQuery();
            }

            sqlite_conn.Close();
        }

        private List<String> LoadTagsFromSQLiteDatabaseFile()
        {

            List<String> tagList = new List<string>();

            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;

            Console.WriteLine("Loading Tags from SQLite Database at : " + tagFileLocationAndFileName);

            if (!File.Exists(tagFileLocationAndFileName))
            {
                CreateSQLITEDatabase();
            }



            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {

                    string sqlString = "SELECT * from tags";

                    sqlite_cmd.CommandText = sqlString;

                    sqlite_cmd.ExecuteNonQuery();

                    using (SQLiteDataReader rdr = sqlite_cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            tagList.Add(Convert.ToString(rdr.GetString(0)));
                        }
                    }

                }

                sqlite_conn.Close();
            }

            return tagList;

        }

        private void WriteTagCollectionToSQLiteFile()
        {
            /* NO
            DeleteDatabaseFile();

            CreateSQLITEDatabase();

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {

                    string[] tags = new string[tagAutoCompleteStringsCollection.Count];

                    tagAutoCompleteStringsCollection.CopyTo(tags, 0);

                    

                    foreach (string tag in tags)
                    {
                        sqlite_cmd.CommandText = "INSERT INTO tags (tag,used) VALUES ('" + tag + "',0)";

                        sqlite_cmd.ExecuteNonQuery();
                    }

                }

                sqlite_conn.Close();
            }*/

        }

        private void DeleteDatabaseFile()
        {
            string tagDatabaseFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;

            File.Delete(tagDatabaseFileLocationAndFileName);

        }

        private void UpdateUsedCounterForTag(string tag)
        {
            string sqlUpdateCommand = "UPDATE tags SET used = used + 1 WHERE tag='"+tag+"';";

            Console.Write("Updating used counter for tag " + tag + " with sql-statment : '" + sqlUpdateCommand+"'");

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName + ";New=True"))
            {
                sqlite_conn.Open();

                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {

                    sqlite_cmd.CommandText = sqlUpdateCommand;

                    sqlite_cmd.ExecuteNonQuery();
                }

                sqlite_conn.Close();
            }
        }

        #endregion

        #region HandlingFunctions

        /// <summary>
        /// Initializes the List Containing the Quick Most Used Tag Controls
        /// </summary>
        private void InitMostUsedTagButtonList()
        {
            IEnumerable<Control> QuickTagControls = GetAllControls(MostUsedTagsQuickBoxPanel);

            foreach (Control control in QuickTagControls)
            {
                Console.WriteLine(control.Name);

                if (control.Text == "Quick Tag")
                    mostUsedTagsButtons.Add(control);
            }
        }

        private void SetNameOfMostUsedTagButtons(List<string> mostUsedTagsStringList)
        {
            int i = mostUsedTagsStringList.Count;

            foreach (Control control in mostUsedTagsButtons)
            {
                i--;

                Console.WriteLine(i);

                string tag = mostUsedTagsStringList[i];

                control.Text = tag;
               
            }
        }

        /// <summary>
        /// Saves the size and the position relative to the containing container
        /// </summary>
        private void SavingPictureBoxSizeAndPosition()
        {
            originalSizeOfPictureBox = pictureBox1.Size;
            pictureBoxLeftDistanceToContainingContainer = pictureBox1.Left;
            pictureBoxTopDistanceToContainingContainer = pictureBox1.Top;
        }

        /// <summary>
        /// Restores Size and Position of the PictureBox Controll
        /// </summary>
        private void RestoringPictureBoxSizeAndPosition()
        {
            pictureBox1.Size = originalSizeOfPictureBox;
            pictureBox1.Left = pictureBoxLeftDistanceToContainingContainer;
            pictureBox1.Top = pictureBoxTopDistanceToContainingContainer;
        }

        private void SetUpCurrentImage(bool scanDirectory = false)
        {

            RestoringPictureBoxSizeAndPosition();

            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            if (scanDirectory)
                ScanDirectoryAndUpdateImageIndexTextBox();

            if (imageFilesInCurrentDirectory.Count() != 0)
            {

                string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
                string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

                UpdateBaseNameTextBox(filename);

                ParseTagsAndPopulateTagListForImage(filename);

                UpdateNumberOfImagesInDirectoryTextbox();

                currentSelectedImage = new CurrentSelectedImageFile(filename, directory);

                string pathAndFileName = directory + Path.DirectorySeparatorChar + filename;

                SetCurrentImageToPictureBox(pathAndFileName);

            }
        }

        private void ScanDirectoryAndUpdateImageIndexTextBox()
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            if (imageIndex > imageFilesInCurrentDirectory.Count() - 1)
                imageIndex = 0;

            if (imageIndex < 0)
                imageIndex = imageFilesInCurrentDirectory.Count() - 1;

            UpdateNumberOfImagesInDirectoryTextbox();

        }

        public void InitializeAutoCompletionForNewTagTextBox()
        {
            tagAutoCompleteStringsCollection = new AutoCompleteStringCollection();

            List<String> tagList = null;

            Console.WriteLine("Tag Storage Mode is : " + tagListStoringChoice.ToString());

            if (tagListStoringChoice == TagListStoringChoice.TextFile)
                tagList = LoadTagsFromTagFile();

            if (tagListStoringChoice == TagListStoringChoice.SQLiteDatabase)
                tagList = LoadTagsFromSQLiteDatabaseFile();

            foreach (string tag in tagList)
            {
                tagAutoCompleteStringsCollection.Add(tag);
            }

            Console.WriteLine("Added " + tagAutoCompleteStringsCollection.Count.ToString() + " to AutoCompletionTagList.");

            newTag_textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            newTag_textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            newTag_textBox.AutoCompleteCustomSource = tagAutoCompleteStringsCollection;
        }

        private void UserDataDirectoryHandling()
        {
            userDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string fastAndFuriousImageFileTaggerDirectory = "FastAndFuriousImageFileTagger";

            string fullPathToUserDirectory = userDataDirectory + Path.DirectorySeparatorChar + fastAndFuriousImageFileTaggerDirectory;

            Console.WriteLine("Writing User Data Directory to : " + fullPathToUserDirectory);

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(fullPathToUserDirectory))
                {
                    Console.WriteLine("That path exists already.");

                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(fullPathToUserDirectory);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(fullPathToUserDirectory));

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }

            userDataDirectory = fullPathToUserDirectory;

        }


        /// <summary>
        /// Adds Tag to TagAutoCompleteCollection if not allready present in it.
        /// Also Inserts Tag if new into Database or Tagfile
        /// Also checks if Tag is bigger than only one char, cause then its probably the space shortcut to go 
        /// to next image.
        /// </summary>
        /// <param name="tagToAdd">string of Tag to add</param>
        private void AddTagToAutoCompleteListIfNotPresent(string tagToAdd)
        {

            if (!tagAutoCompleteStringsCollection.Contains(tagToAdd) && tagToAdd.Length > 1)
            {

                Console.WriteLine("Tag " + tagToAdd + " is new, adding it.");

                tagAutoCompleteStringsCollection.Add(tagToAdd);

                AddTagToSQLiteDatabase(tagToAdd);

                UpdateUsedCounterForTag(tagToAdd);
            }
            else
            { 
                Console.WriteLine("Tag "+tagToAdd+" is allready in TagAutoCompleteCollection.");

                UpdateUsedCounterForTag(tagToAdd);
            }
        }

        private void SetCurrentImageToPictureBox(string pathAndFileName)
        {

            Bitmap MyImage = LoadBitmapUnlocked(pathAndFileName);


            //Bitmap cloneBitmap = (Bitmap)MyImage.Clone();

            //MyImage.Dispose();

            //BitmapImage bmi = new BitmapImage();

            /*
            BitmapImage bmi = new BitmapImage();

            bmi.BeginInit();
            bmi.UriSource = new Uri(myFilePath);
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.EndInit();
            Image1.Source = bmi;*/

            pictureBox1.Image = (Image)MyImage;
        }

        private Bitmap LoadBitmapUnlocked(string file_name)
        {

            Bitmap bitmapToUse = null;

            try
            {
                using (Bitmap bm = new Bitmap(file_name))
                {
                    bitmapToUse =  new Bitmap(bm);
                }
            }
            catch (Exception)
            {


                //throw;
            }

            return bitmapToUse;

        }

        private void UpdateNumberOfImagesInDirectoryTextbox()
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            NumberOfImagesInDirectoryTextbox.Text =
                "Nr." + (imageIndex + 1).ToString() + " of " + imageFilesInCurrentDirectory.Count().ToString() + " Images";

        }

        private void ParseTagsAndPopulateTagListForImage(string fileName)
        {
            // First Split TagList in Front FromRest of FileName

            tagsForThisFile_checkedListBox.Items.Clear();

            string[] delimiterTags = new string[1];

            delimiterTags[0] = tagStringFromBaseFileNameSeperator;

            string[] subStrings = fileName.Split(delimiterTags, 5, RemoveEmptyEntries);

            if (subStrings.Count() == 1)
                Console.WriteLine("No Tags found for this Image");
            else
            {
                string[] tags = subStrings[0].Split(tagSeperator[0]);

                foreach (string str in tags)
                {
                    Console.WriteLine("TAG: " + str);
                }

                tagsForThisFile_checkedListBox.Items.AddRange(tags);

                for (int i = 0; i < tagsForThisFile_checkedListBox.Items.Count; i++)
                    tagsForThisFile_checkedListBox.SetItemCheckState(i, CheckState.Checked);
            }



        }

        /// <summary>
        /// Adds Tag to Image Tag Check Box if not allready assigned , also gives it to the AutoCompletion
        /// </summary>
        /// <param name="newTag"></param>
        private void AddTagToImageTagCheckBox(string newTag)
        {
            newTag = newTag.ToLower();

            if (newTag.Length > 1)
            {

                if (!tagsForThisFile_checkedListBox.Items.Contains(newTag)) { 

                    string[] newTagWTF = new string[1];

                    newTagWTF[0] = newTag;

                    tagsForThisFile_checkedListBox.Items.AddRange(newTagWTF);

                    for (int i = 0; i < tagsForThisFile_checkedListBox.Items.Count; i++)
                        tagsForThisFile_checkedListBox.SetItemCheckState(i, CheckState.Checked);

                    AddTagToAutoCompleteListIfNotPresent(newTag);

                }

            }
        }

        private void AddTagToImageFile(string newTag, string fileName)
        {
            Console.WriteLine("Trying to add new tag to file '" + fileName + "' in Directory '" + currentSelectedImage.Path + "'");

            if (fileName != null && newTag.Length > 1)

                if (!ImageHasTags(fileName))
                {
                    string finalFileName;

                    finalFileName = newTag.ToUpper() + tagStringFromBaseFileNameSeperator + fileName;

                    string finalPathAndFileName = CleanPath(currentSelectedImage.Path + Path.DirectorySeparatorChar + finalFileName);

                    System.IO.File.Move(currentSelectedImage.Path + Path.DirectorySeparatorChar + fileName, finalPathAndFileName);

                    currentSelectedImage.Name = finalFileName;

                }
                else
                {
                    string finalFileName;

                    finalFileName = newTag.ToUpper() + tagSeperator + fileName;

                    string finalPathAndFileName = CleanPath(currentSelectedImage.Path + Path.DirectorySeparatorChar + finalFileName);

                    System.IO.File.Move(currentSelectedImage.Path + Path.DirectorySeparatorChar + fileName, finalPathAndFileName);

                    currentSelectedImage.Name = finalFileName;
                }

            newTag_textBox.Clear();

            ScanDirectoryAndUpdateImageIndexTextBox();


        }

        private void RenameFile(string oldFileName, string newFileName)
        {
            Console.WriteLine("Renaming File from '" + oldFileName + "' to '" + newFileName);

            System.IO.File.Move(oldFileName, newFileName);
        }

        private bool ImageHasTags(string fileName)
        {
            if (fileName != "" && fileName != null)
            {

                string[] delimiterTags = new string[1];

                delimiterTags[0] = tagStringFromBaseFileNameSeperator;

                string[] subStrings = fileName.Split(delimiterTags, 5, RemoveEmptyEntries);

                if (subStrings.Count() == 1)
                    return false;

            }
            else
                return false;

            return true;
        }

        private string CleanPath(string toCleanPath, string replaceWith = "")
        {
            //get just the filename - can't use Path.GetFileName since the path might be bad!  
            string[] pathParts = toCleanPath.Split(new char[] { Path.DirectorySeparatorChar });
            string newFileName = pathParts[pathParts.Length - 1];
            //get just the path  
            string newPath = toCleanPath.Substring(0, toCleanPath.Length - newFileName.Length);
            //clean bad path chars  
            foreach (char badChar in Path.GetInvalidPathChars())
            {
                newPath = newPath.Replace(badChar.ToString(), replaceWith);
            }
            //clean bad filename chars  
            foreach (char badChar in Path.GetInvalidFileNameChars())
            {
                newFileName = newFileName.Replace(badChar.ToString(), replaceWith);
            }
            //remove duplicate "replaceWith" characters. ie: change "test-----file.txt" to "test-file.txt"  
            if (string.IsNullOrWhiteSpace(replaceWith) == false)
            {
                newPath = newPath.Replace(replaceWith.ToString() + replaceWith.ToString(), replaceWith.ToString());
                newFileName = newFileName.Replace(replaceWith.ToString() + replaceWith.ToString(), replaceWith.ToString());
            }
            //return new, clean path:  
            return newPath + newFileName;
        }

        private void UpdateBaseNameTextBox(string fullFileName)
        {
            renameBase_textBox.Text = GetBaseFileName(Path.GetFileNameWithoutExtension(fullFileName));
        }

        private string GetBaseFileName(string fullName)
        {

            string[] delimiterTags = new string[1];

            delimiterTags[0] = tagStringFromBaseFileNameSeperator;

            string[] subStrings = fullName.Split(delimiterTags, 5, RemoveEmptyEntries);

            return subStrings[subStrings.Count() - 1];
        }

        private string GetTagStringOfFile(string fullName)
        {

            string[] delimiterTags = new string[1];

            delimiterTags[0] = tagStringFromBaseFileNameSeperator;

            string[] subStrings = fullName.Split(delimiterTags, 5, RemoveEmptyEntries);

            return subStrings[0];
        }

        private bool HasThisFileTags(string fullFileName)
        {
            string[] delimiterTags = new string[1];

            delimiterTags[0] = tagStringFromBaseFileNameSeperator;

            string[] subStrings = fullFileName.Split(delimiterTags, 5, RemoveEmptyEntries);

            if (subStrings.Count() == 1)
                return false;

            return true;
        }

        private IEnumerable<string> GetFileListFromCurrentDirectory()
        {

            Console.WriteLine("Getting File List from +'" + workingDirectory + "'");

            DirectoryInfo dinfo = new DirectoryInfo(workingDirectory);

            IEnumerable<string> filesWithEnum;

            if (showOnlyNotTaggedFiles)
                filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => (
                s.EndsWith(".jpg") && !s.Contains(tagStringFromBaseFileNameSeperator)) 
                || 
                (s.EndsWith(".png") && !s.Contains(tagStringFromBaseFileNameSeperator))
                ||
                (s.EndsWith(".jpeg") && !s.Contains(tagStringFromBaseFileNameSeperator))
                ||
                (s.EndsWith(".avi") && !s.Contains(tagStringFromBaseFileNameSeperator))
                ||
                (s.EndsWith(".mpg") && !s.Contains(tagStringFromBaseFileNameSeperator))
                ||
                (s.EndsWith(".mp4") && !s.Contains(tagStringFromBaseFileNameSeperator))

                );
            else
                filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
               .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png"));

            return filesWithEnum;

        }

        public IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control control in root.Controls)
            {
                foreach (Control child in GetAllControls(control))
                {
                    yield return child;
                }
            }
            yield return root;
        }

        private void RenameBaseFilenameToIndexedBaseFilename()
        {
            long currentImageIndex = ImageFileNameIndex.Get();

            string extension = Path.GetExtension(currentSelectedImage.Name);

            string newIndexedBasefileName = "I" + currentImageIndex.ToString() + extension;

            string newFileName;

            if (HasThisFileTags(currentSelectedImage.Name))
            {
                Console.WriteLine("RenameBaseNameButton_Click : Current file has Tags");

                string tags = GetTagStringOfFile(currentSelectedImage.Name);

                string newBaseFileName = newIndexedBasefileName;

                newFileName = tags + tagStringFromBaseFileNameSeperator + newBaseFileName;

            }
            else
                newFileName = newIndexedBasefileName;

            RenameFile(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name,
                currentSelectedImage.Path + Path.DirectorySeparatorChar + newFileName);

            currentSelectedImage.Name = newFileName;

            UpdateBaseNameTextBox(currentSelectedImage.Name);

            ImageFileNameIndex.Increase();

        }

        private void DeleteImage()
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this Image ?",
                                     "Confirm Removal",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                File.Delete(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name);

                SetUpCurrentImage();

            }
            else
            {

            }

            TagBoxLabel.Focus();
        }

        private void CopyCurrentImageFileToDesktop()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string fileWithPath = desktopPath + Path.DirectorySeparatorChar + currentSelectedImage.Name;

            if (!File.Exists(fileWithPath))
                File.Copy(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name,
                    fileWithPath);
            else
                MessageBox.Show("File allready exits in that location", "Notice",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
        }

        private void ChangeDirectory()
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine("Setting working directory to : " + folderBrowserDialog1.SelectedPath);
                workingDirectory = folderBrowserDialog1.SelectedPath;

                SetUpCurrentImage(true);
            }
        }

        #endregion

        #region TagTextFileFunctions

        private void AddTagToTagFile(string newTag)
        {

            Console.WriteLine("Writing Tag to Tag File in : " + tagFileLocationAndFileName);

            using (var writer = new StreamWriter(tagFileLocationAndFileName, true))
            {
                writer.WriteLine(newTag);
            }
        }

        private List<String> LoadTagsFromTagFile()
        {
            List<String> tagList = new List<string>();

            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + tagFileName;

            Console.WriteLine("Loading Tags from : " + tagFileLocationAndFileName);

            if (!File.Exists(tagFileLocationAndFileName))
            {
                using (StreamWriter w = File.AppendText(tagFileLocationAndFileName)) { }

            }

            StreamReader inFile = new StreamReader(tagFileLocationAndFileName);

            string tag;

            while ((tag = inFile.ReadLine()) != null)
            {

                tagList.Add(Convert.ToString(tag));

            }

            inFile.Close();

            return tagList;
        }

        #endregion

        #region UnusedFunctions

        private void NewTagLabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FastAndFuriousImageTagger_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TagBoxLabel_Click(object sender, EventArgs e)
        {

        }

        private void onlyShowNonTaggedImages_CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void newTag_textBox_KeyDown(object sender, KeyEventArgs e)
        {

        }



        #endregion


    }
}
