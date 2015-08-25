using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsDevKit.UI
{
    public partial class Importer : Form
    {
        public Importer()
        {
            InitializeComponent();
        }

        public List<string> Files = new List<string>();
        public string origionalPath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                Files = new List<string>();
                origionalPath = dlg.SelectedPath;
                IterateDirectory(dlg.SelectedPath);

                foreach(var i in Files)
                {
                    checkedListBox1.Items.Add(i);
                }
            }
        }

        public void IterateDirectory(string path)
        {
            foreach (var i in Directory.GetFiles(path))
            {
                Files.Add(i.Replace(origionalPath, ""));
            }
            foreach (var i in Directory.GetDirectories(path))
            {
                IterateDirectory(i);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            foreach (var i in checkedListBox1.CheckedItems)
            {
                string a = origionalPath + i.ToString();
                string b = Global.CurrentProjectFilePath + "\\files" + i.ToString();
                if (!Directory.Exists(new FileInfo(b).DirectoryName))
                {
                    Directory.CreateDirectory(new FileInfo(b).DirectoryName);
                }
                
                File.Copy(a, b);

                Global.CurrentProjectFile.Files.Add(i.ToString().TrimStart('\\'));
            }
            Global.Save();
        }
    }
}
