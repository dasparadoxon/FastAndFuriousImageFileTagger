using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

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



        #endregion

        public TagEditor()
        {
            InitializeComponent();

            userDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            dataGridView1.DataSource = CreateTagList();

            //int numberOfRows = dataGridView1.ColumnCount;

            //for(int i = 0; i < numberOfRows; i++)
            //{

            var col4 = new DataGridViewButtonColumn();

            col4.HeaderText = "Delete Tag";
            col4.Name = "Delete";
            col4.Text = "Delete Tag";
            col4.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col4 });

            //}

            
        }

        public void InitializeTagCheckBoxListFromAutocompletionList()
        {
            /*
            List<string> tags = new List<string>();

            //string[] tagArray = tags.ToArray();

            string[] tagArray = new string[TagCollection.Count];

            // TODO : Sorting of the elements, Array.Sort(tagArray) is resulting in strange entries ??

            TagCollection.CopyTo(tagArray, 0);

            checkedListBox_Tags.Items.AddRange(tagArray);

            for (int i = 0; i < checkedListBox_Tags.Items.Count; i++)
                checkedListBox_Tags.SetItemCheckState(i, CheckState.Checked);
            */
        }

        private void Button_tagDialogDone_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<TagEntry> CreateTagList()
        {
            List < TagEntry > tagListToReturn = new List<TagEntry>();

            string fastAndFuriousImageFileTaggerDirectory = "FastAndFuriousImageFileTagger";

            string fullPathToUserDirectory = userDataDirectory + Path.DirectorySeparatorChar + fastAndFuriousImageFileTaggerDirectory;

            userDataDirectory = fullPathToUserDirectory;

            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;

            Console.WriteLine("Reading SQL Database at : " + tagFileLocationAndFileName + " to fill TagEditor");

            using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + tagFileLocationAndFileName+";" ))
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
                            TagEntry tagToAdd = new TagEntry();

                            tagToAdd.Tag = Convert.ToString(rdr.GetString(0));
                            tagToAdd.Used = Convert.ToInt32(rdr.GetInt32(1));

                            tagListToReturn.Add(tagToAdd);

                        }
                    }

                }

                sqlite_conn.Close();

            }

            return tagListToReturn;


        }

        private DataTable GetData(string sqlCommand)
        {
            tagFileLocationAndFileName = userDataDirectory + Path.DirectorySeparatorChar + databaseFileName;

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
                            //tagList.Add(Convert.ToString(rdr.GetString(0)));
                        }
                    }

                }

                sqlite_conn.Close();

            }

                /*

                    string connectionString = "Integrated Security=SSPI;" +
                    "Persist Security Info=False;" +
                    "Initial Catalog=Northwind;Data Source=localhost";

                SqlConnection northwindConnection = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand(sqlCommand, northwindConnection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);*/

                return null;
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