using System;
//using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/** FAST AND FURIOUS IMAGE FILE TAGGER 
 * 
 * Quickly add tags to your image filename
 * 
 * Done in 2019 in a few hours by dasparadoxon, programming by Tom Trottel
 * 
 */

namespace FastAndFuriousImageFileTagger
{

    public partial class FastAndFuriousImageTagger : Form
    {
        private class ImageFileNameIndex
        {
            static public void Increase()
            {

                Properties.Settings.Default.imageIndex++;

                Properties.Settings.Default.Save();

            }

            static public long  Get()
            {
                Console.WriteLine("Setting:" + Properties.Settings.Default["imageIndex"]);

                return (long)Properties.Settings.Default["imageIndex"];
            }
        
        }

        // IMAGES

        CurrentSelectedImageFile currentSelectedImage;

        private IEnumerable<string> imageFilesInCurrentDirectory;

        // TAGGING SETUP

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

        // --- METHODS -------

        public FastAndFuriousImageTagger()
        {
            InitializeComponent();

            currentSelectedImage = new CurrentSelectedImageFile("", "");

            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            startImage = pictureBox1.Image;

            UserDataDirectoryHandling();

            //imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            InitializeAutoCompletionForNewTagTextBox();

            SetUpCurrentImage();

        }


        private void SetUpCurrentImage(bool scanDirectory = false)
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            if(scanDirectory)
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


        // EVENT HANDLER -------

        // CHANGES THE BASEFILENAME TO A FILENAME USING THE APP INDEX COUNTER
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

        private void CopyCurrentImageFileToDesktop()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            File.Copy(currentSelectedImage.Path + Path.DirectorySeparatorChar + currentSelectedImage.Name,
                desktopPath + Path.DirectorySeparatorChar + currentSelectedImage.Name);
        }

        // Tampers something so that Arrow Keys are send to the KeyDown Handler (yikes)
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

        // CHECKS IF IN THE NEW TAG BOX A SPACE IS PRESSED, WHICH THEN GOES TO THE NEXT IMAGE
        // CHECKS IF ENTER IN THE NEW TAG BOX IS PRESSED, AND ADS THE TAG TO THE CURRENT PICTURE
        private void NewTag_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
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

            if (e.KeyCode == Keys.Space)
            {
                if (newTag_textBox.Text == " ")
                {
                    imageIndex++;

                    SetUpCurrentImage(true);

                    // TODO : If there are no images left to process, set the initial image.

                    newTag_textBox.Clear();

                    e.Handled = true;

                }
            }
        }

        private void MainForm_KeyDown_Event(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
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

            if(e.KeyCode == Keys.Delete)
            {
                DeleteImage();
            }

            if (e.KeyCode == Keys.F4)
                CopyCurrentImageFileToDesktop();
        }

        private void AddTagButton_AddTag_clicked(object sender, EventArgs e)
        {
            AddTagToImageTagCheckBox(newTag_textBox.Text);
        }

        private void DirectoryChangeButton_clicked(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine("Setting working directory to : " + folderBrowserDialog1.SelectedPath);
                workingDirectory = folderBrowserDialog1.SelectedPath;

                SetUpCurrentImage(true);
            }
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

                e.Handled = true;
                e.SuppressKeyPress = true;

                newTag_textBox.Select();            }

        }

        private void EditTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagEditor tagEditorInstance = new TagEditor();

            tagEditorInstance.TagCollection = tagAutoCompleteStringsCollection;

            tagEditorInstance.InitializeTagCheckBoxListFromAutocompletionList();

            tagEditorInstance.Show();
        }

        // DATA AND TAG HANDLING

        private void ScanDirectoryAndUpdateImageIndexTextBox()
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            if (imageIndex > imageFilesInCurrentDirectory.Count() - 1)
                imageIndex = 0;

            if (imageIndex < 0)
                imageIndex = imageFilesInCurrentDirectory.Count() - 1;

            UpdateNumberOfImagesInDirectoryTextbox();

        }

        private void InitializeAutoCompletionForNewTagTextBox()
        {
            tagAutoCompleteStringsCollection = new AutoCompleteStringCollection();

            List<String> tagList = LoadTagsFromTagFile();

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

        private void AddTagToAutoCompleteListIfNotPresent(string tagToAdd)
        {
            if (!tagAutoCompleteStringsCollection.Contains(tagToAdd) && tagToAdd.Length > 1)
            {
                tagAutoCompleteStringsCollection.Add(tagToAdd);
            }
        }

        private void SaveTagToTagFileIfNotPresent()
        {

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
            using (Bitmap bm = new Bitmap(file_name))
            {
                return new Bitmap(bm);
            }
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

        private void AddTagToImageTagCheckBox(string newTag)
        {
            if(newTag.Length > 1) { 

                string[] newTagWTF = new string[1];

                newTagWTF[0] = newTag;

                tagsForThisFile_checkedListBox.Items.AddRange(newTagWTF);

                for (int i = 0; i < tagsForThisFile_checkedListBox.Items.Count; i++)
                    tagsForThisFile_checkedListBox.SetItemCheckState(i, CheckState.Checked);

                AddTagToAutoCompleteListIfNotPresent(newTag);

                AddTagToTagFile(newTag);

            }
        }

        private void AddTagToTagFile(string newTag)
        {


            Console.WriteLine("Writing Tag to Tag File in : " + tagFileLocationAndFileName);

            using (var writer = new StreamWriter(tagFileLocationAndFileName, true))
            {
                writer.WriteLine(newTag);
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
                .Where(s => (s.EndsWith(".jpg") && !s.Contains(tagStringFromBaseFileNameSeperator)) || (s.EndsWith(".png") && !s.Contains("__")));
            else
                filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
               .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png"));

            return filesWithEnum;

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
        }
        
        // UNUSED

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


    }
}
