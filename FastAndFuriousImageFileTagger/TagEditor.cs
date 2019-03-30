using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastAndFuriousImageFileTagger
{
    public partial class TagEditor : Form
    {
        private AutoCompleteStringCollection tagCollection;

        public AutoCompleteStringCollection TagCollection { get => tagCollection; set => tagCollection = value; }

        public TagEditor()
        {
            InitializeComponent();

            
        }

        public void InitializeTagCheckBoxListFromAutocompletionList()
        {
            List<string> tags = new List<string>();

            //string[] tagArray = tags.ToArray();

            string[] tagArray = new string[TagCollection.Count];

            // TODO : Sorting of the elements, Array.Sort(tagArray) is resulting in strange entries ??

            TagCollection.CopyTo(tagArray, 0);

            checkedListBox_Tags.Items.AddRange(tagArray);

            for (int i = 0; i < checkedListBox_Tags.Items.Count; i++)
                checkedListBox_Tags.SetItemCheckState(i, CheckState.Checked);
        }

        private void Button_tagDialogDone_click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
