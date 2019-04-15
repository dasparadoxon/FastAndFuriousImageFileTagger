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

        #endregion

        public TagEditor()
        {
            InitializeComponent();

            InitializeDatabasePath();

            dataSet = new DataSet();
            
            DataClass tagDataClass = new DataClass(tagFileLocationAndFileName);

            BindingSource bindingSourceForDataViewGrid = new BindingSource();

            DataTable tagDataTable = tagDataClass.selectQuery("SELECT * from tags");

            bindingSourceForDataViewGrid.DataSource = tagDataTable;

            dataGridView1.DataSource = bindingSourceForDataViewGrid;

            AddButtonColumn();


         }

        private void InitializeDatabasePath()
        {
            userDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string fastAndFuriousImageFileTaggerDirectory = "FastAndFuriousImageFileTagger";

            string fullPathToUserDirectory = userDataDirectory + Path.DirectorySeparatorChar + fastAndFuriousImageFileTaggerDirectory;

            userDataDirectory = fullPathToUserDirectory;

            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;
        }

        private void AddButtonColumn()
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
            sqlite = new SQLiteConnection("Data Source="+ pathToDB);

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
    }
}