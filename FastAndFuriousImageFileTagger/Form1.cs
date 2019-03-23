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
        private int imageIndex = 0;

        public StringSplitOptions RemoveEmptyEntries { get; private set; }

        private string currentImageFilePath;

        private string currentImageFileName;

        private FolderBrowserDialog folderBrowserDialog1;

        public AutoCompleteStringCollection tagAutoCompleteStringsCollection;

        //public string workingDirectory = @"C:\Users\dasparadoxon\Desktop";

        public string workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private string tagSeperator = "_";

        private string tagStringFromBaseFileNameSeperator = "__";

        private IEnumerable<string> imageFilesInCurrentDirectory;

        string tagFileLocationAndFileName = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "tagFile.txt";

        bool showOnlyNotTaggedFiles = true;

        string userDataDirectory;

        string tagFileName = "tagFile.txt";
        


        // --- METHODS -------

        public FastAndFuriousImageTagger()
        {
            InitializeComponent();

            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            UserDataDirectoryHandling();

            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            InitializeAutoCompletionForNewTagTextBox();

            if (imageFilesInCurrentDirectory.Count() != 0)
            {

                string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
                string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

                //UpdateBaseNameTextBox(filename);

                ParseTagsAndPopulateTagListForImage(filename);

                UpdateNumberOfImagesInDirectoryTextbox();

                currentImageFilePath = directory;

                currentImageFileName = filename;

                string pathAndFileName = directory + "\\" + filename;

                SetCurrentImageToPictureBox(pathAndFileName);

                

            }

        }

        // EVENT HANDLER -------

        private void PreviousImageButtonClick(object sender, EventArgs e)
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            imageIndex--;

            ScanDirectoryAndUpdateImageIndexTextBox();

            string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
            string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

            string pathAndFileName = directory
                + "\\" + filename;

            SetCurrentImageToPictureBox(pathAndFileName);

            UpdateBaseNameTextBox(filename);

            ParseTagsAndPopulateTagListForImage(filename);

            currentImageFilePath = directory;

            currentImageFileName = filename;
        }

        private void NextImageButton_click(object sender, EventArgs e)
        {
            imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

            imageIndex++;

            ScanDirectoryAndUpdateImageIndexTextBox();

            string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
            string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

            string pathAndFileName = directory
                + "\\" + filename;

            SetCurrentImageToPictureBox(pathAndFileName);

            UpdateBaseNameTextBox(filename);

            ParseTagsAndPopulateTagListForImage(filename);

            currentImageFilePath = directory;

            currentImageFileName = filename;


        }

        private void newTag_textBox_textChanged(object sender, EventArgs e)
        {
            Console.WriteLine("New TagTextbox has changed : " + newTag_textBox.Text);

            AddTagToImageTagCheckBox(newTag_textBox.Text);

            AddTagToImageFile(newTag_textBox.Text, currentImageFileName);
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("TextBox New Tag Enter : " + newTag_textBox.Text);

                AddTagToImageTagCheckBox(newTag_textBox.Text);

                AddTagToImageFile(newTag_textBox.Text, currentImageFileName);

                e.Handled = true;
            }

            if (e.KeyCode == Keys.Space)
            {
                if (newTag_textBox.Text == " ")
                {

                    imageFilesInCurrentDirectory = GetFileListFromCurrentDirectory();

                    imageIndex++;

                    ScanDirectoryAndUpdateImageIndexTextBox();

                    string directory = Path.GetDirectoryName(imageFilesInCurrentDirectory.ElementAt(imageIndex));
                    string filename = Path.GetFileName(imageFilesInCurrentDirectory.ElementAt(imageIndex));

                    string pathAndFileName = directory
                        + "\\" + filename;

                    SetCurrentImageToPictureBox(pathAndFileName);

                    UpdateBaseNameTextBox(filename);

                    ParseTagsAndPopulateTagListForImage(filename);

                    currentImageFilePath = directory;

                    currentImageFileName = filename;

                    newTag_textBox.Clear();

                    e.Handled = true;

                }
            }
        }

        private void addTagButton_AddTag_clicked(object sender, EventArgs e)
        {
            AddTagToImageTagCheckBox(newTag_textBox.Text);
        }

        private void FastAndFuriousImageTagger_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void RenameBaseNameButton_Click(object sender, EventArgs e)
        {
            // are there tags in the current active image ?

            string newFileName;

            if (HasThisFileTags(currentImageFileName))
            {
                Console.WriteLine("RenameBaseNameButton_Click : Current file has Tags");

                string tags = GetTagStringOfFile(currentImageFileName);

                string newBaseFileName = renameBase_textBox.Text;

                newFileName = tags + "__" + newBaseFileName;



            }
            else
                newFileName = renameBase_textBox.Text;

            RenameFile(currentImageFilePath + "//" + currentImageFileName, currentImageFilePath + "//" + newFileName);

            currentImageFileName = newFileName;

            Console.WriteLine("RenameBaseNameButton_Click : New Current Image File Name : " + currentImageFileName);

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

            string newWholeFileName = GetBaseFileName(currentImageFileName);

            newWholeFileName = newTagString + tagStringFromBaseFileNameSeperator + newWholeFileName;

            // test for unnecessary tagFromBaseFile Delimiter and if, remove it
            if (newWholeFileName.Substring(0, 2) == "__")
                newWholeFileName = newWholeFileName.Replace("__", "");

            // correct length of tagFromBaseFile Delimiter if necessary
            if (newWholeFileName.Contains("___"))
                newWholeFileName = newWholeFileName.Replace("___", "__");

            Console.WriteLine("Fully new Filename from TagCheckBoxList : " + newWholeFileName);

            RenameFile(currentImageFilePath + "//" + currentImageFileName, currentImageFilePath + "//" + newWholeFileName);

            currentImageFileName = newWholeFileName;

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

                //UpdateBaseNameTextBox(filename);

                ParseTagsAndPopulateTagListForImage(filename);

                UpdateNumberOfImagesInDirectoryTextbox();

                currentImageFilePath = directory;

                currentImageFileName = filename;

                string pathAndFileName = directory + "\\" + filename;

                SetCurrentImageToPictureBox(pathAndFileName);

                InitializeAutoCompletionForNewTagTextBox();

            }

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
            if (!tagAutoCompleteStringsCollection.Contains(tagToAdd))
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

            delimiterTags[0] = "__";

            string[] subStrings = fileName.Split(delimiterTags, 5, RemoveEmptyEntries);

            if (subStrings.Count() == 1)
                Console.WriteLine("No Tags found for this Image");
            else
            {
                string[] tags = subStrings[0].Split('_');

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

            string[] newTagWTF = new string[1];

            newTagWTF[0] = newTag;

            tagsForThisFile_checkedListBox.Items.AddRange(newTagWTF);

            for (int i = 0; i < tagsForThisFile_checkedListBox.Items.Count; i++)
                tagsForThisFile_checkedListBox.SetItemCheckState(i, CheckState.Checked);

            AddTagToAutoCompleteListIfNotPresent(newTag);

            AddTagToTagFile(newTag);
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
            Console.WriteLine("Trying to add new tag to file '" + fileName + "' in Directory '" + currentImageFilePath + "'");

            if (!ImageHasTags(fileName))
            {
                string finalFileName;

                finalFileName = newTag.ToUpper() + "__" + fileName;

                string finalPathAndFileName = CleanPath(currentImageFilePath + "\\" + finalFileName);

                System.IO.File.Move(currentImageFilePath + "\\" + fileName, finalPathAndFileName);

                currentImageFileName = finalFileName;

            }
            else
            {
                string finalFileName;

                finalFileName = newTag.ToUpper() + "_" + fileName;

                string finalPathAndFileName = CleanPath(currentImageFilePath + "\\" + finalFileName);

                System.IO.File.Move(currentImageFilePath + "\\" + fileName, finalPathAndFileName);

                currentImageFileName = finalFileName;
            }

            newTag_textBox.Clear();

            ScanDirectoryAndUpdateImageIndexTextBox();


        }

        private void RenameFile(string oldFileName, string newFileName)
        {
            System.IO.File.Move(oldFileName, newFileName);
        }

        private bool ImageHasTags(string fileName)
        {

            string[] delimiterTags = new string[1];

            delimiterTags[0] = "__";

            string[] subStrings = fileName.Split(delimiterTags, 5, RemoveEmptyEntries);

            if (subStrings.Count() == 1)
                return false;

            return true;
        }

        private string CleanPath(string toCleanPath, string replaceWith = "")
        {
            //get just the filename - can't use Path.GetFileName since the path might be bad!  
            string[] pathParts = toCleanPath.Split(new char[] { '\\' });
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
            renameBase_textBox.Text = GetBaseFileName(fullFileName);
        }

        private string GetBaseFileName(string fullName)
        {

            string[] delimiterTags = new string[1];

            delimiterTags[0] = "__";

            string[] subStrings = fullName.Split(delimiterTags, 5, RemoveEmptyEntries);

            return subStrings[subStrings.Count() - 1];
        }

        private string GetTagStringOfFile(string fullName)
        {

            string[] delimiterTags = new string[1];

            delimiterTags[0] = "__";

            string[] subStrings = fullName.Split(delimiterTags, 5, RemoveEmptyEntries);

            return subStrings[0];
        }

        private bool HasThisFileTags(string fullFileName)
        {
            string[] delimiterTags = new string[1];

            delimiterTags[0] = "__";

            string[] subStrings = fullFileName.Split(delimiterTags, 5, RemoveEmptyEntries);

            if (subStrings.Count() == 1)
                return false;

            return true;
        }

        private FileInfo[] GetFileListFromCurrentDirectory_old()
        {

            Console.WriteLine("Getting File List from +'" + workingDirectory + "'");


            DirectoryInfo dinfo = new DirectoryInfo(workingDirectory);



            var filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => (s.EndsWith(".jpg") && !s.Contains("__")) || (s.EndsWith(".png") && !s.Contains("__")));

            return dinfo.GetFiles("*.jpg");

        }

        private IEnumerable<string> GetFileListFromCurrentDirectory()
        {

            Console.WriteLine("Getting File List from +'" + workingDirectory + "'");

            DirectoryInfo dinfo = new DirectoryInfo(workingDirectory);

            IEnumerable<string> filesWithEnum;

            if (showOnlyNotTaggedFiles)
                filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => (s.EndsWith(".jpg") && !s.Contains("__")) || (s.EndsWith(".png") && !s.Contains("__")));
            else
                filesWithEnum = Directory.EnumerateFiles(workingDirectory, "*.*", SearchOption.TopDirectoryOnly)
               .Where(s => s.EndsWith(".jpg")|| s.EndsWith(".png"));

            return filesWithEnum;

        }

        private void DeleteImage()
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                File.Delete(currentImageFilePath + "//" + currentImageFileName);
            }
            else
            {
                // If 'No', do something here.
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

        private void DirectoryChangeButton_clicked(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine("Setting working directory to : " + folderBrowserDialog1.SelectedPath);
                workingDirectory = folderBrowserDialog1.SelectedPath;
                ScanDirectoryAndUpdateImageIndexTextBox();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TagBoxLabel_Click(object sender, EventArgs e)
        {

        }


    }
}
