using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using MySqlLite;

namespace FastAndFuriousImageFileTagger
{


    public partial class TagEditor : Form
    {
        //private AutoCompleteStringCollection tagCollection;

        //public AutoCompleteStringCollection TagCollection { get => tagCollection; set => tagCollection = value; }

        #region Fields

        private string tagFileLocationAndFileName;

        private string userDataDirectory;

        private string databaseFileName = "FastAndFuriousImageFileTagger.db";

        private System.Data.DataSet dataSet;

        public FastAndFuriousImageTagger mainForm;

        #endregion

        public TagEditor()
        {
            InitializeComponent();

            InitializeDatabasePath();

            dataSet = new DataSet();

            DataClass tagDataClass = new DataClass(tagFileLocationAndFileName);

            BindingSource bindingSourceForDataViewGrid = new BindingSource();

            DataTable tagDataTable = tagDataClass.selectQuery("SELECT * from tags");

            tagDataTable.RowDeleted += new DataRowChangeEventHandler(tagDataClass.Row_Deleted);

            bindingSourceForDataViewGrid.DataSource = tagDataTable;

            dataGridView1.DataSource = bindingSourceForDataViewGrid;

   
        }

        private void InitializeDatabasePath()
        {
            userDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string fastAndFuriousImageFileTaggerDirectory = "FastAndFuriousImageFileTagger";

            string fullPathToUserDirectory = userDataDirectory + Path.DirectorySeparatorChar + fastAndFuriousImageFileTaggerDirectory;

            userDataDirectory = fullPathToUserDirectory;

            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;
        }

        private void AddDeleteButtonColumn()
        {
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();

            deleteButtonColumn.HeaderText = "Delete Tag";
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete Tag";
            deleteButtonColumn.UseColumnTextForButtonValue = true;


            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { deleteButtonColumn });
        }

        private void FillDataGridView(List<TagEntry> TagList)
        {

        }

        private void Button_tagDialogDone_click(object sender, EventArgs e)
        {
            mainForm.InitializeAutoCompletionForNewTagTextBox();

            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


class TagEntry
{
    string tag;
    int used;

    public string Tag { get => tag; set => tag = value; }
    public int Used { get => used; set => used = value; }
}

/// <summary>
/// Small Class for ADO sqlite, thanks to kux
/// </summary>
namespace MySqlLite
{
    class DataClass
    {
        private SQLiteConnection sqlite;

        public DataClass(string pathToDB)
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection("Data Source=" + pathToDB);

        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }

        public void Row_Deleted(object sender, DataRowChangeEventArgs e)
        {
            Console.WriteLine("Row_Deleted Event: name={0}; action={1}",
                e.Row["tag", DataRowVersion.Original], e.Action);

            DeleteTag(e.Row["tag", DataRowVersion.Original].ToString());

        }

        private void DeleteTag(string tag)
        {
            string deleteSQLstring = "DELETE FROM tags WHERE tag='" + tag + "';";

            selectQuery(deleteSQLstring);

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = deleteSQLstring;  //set the passed query

                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }

            sqlite.Close();



        }
    }


}