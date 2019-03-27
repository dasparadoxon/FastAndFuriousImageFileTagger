using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastAndFuriousImageFileTagger
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FastAndFuriousImageTagger());
        }
    }

    public class CurrentSelectedImageFile
    {
        private string path;
        private string name;

        public CurrentSelectedImageFile(string name, string path)
        {
            this.path = path;
            this.name = name;
        }

        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
    }
}
