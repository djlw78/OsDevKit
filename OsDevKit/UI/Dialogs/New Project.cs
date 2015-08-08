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
using System.IO.Compression;
using Newtonsoft.Json;

namespace OsDevKit.UI.Dialogs
{
    public partial class New_Project : Form
    {
        public New_Project()
        {
            InitializeComponent();
        }


        private List<string> Templates = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dlg.SelectedPath;
            }

        }

        public void CreateProject(string name, string path, string Template)
        {
            var d = new DirectoryInfo(path + "\\" + name);
            Directory.CreateDirectory(d.FullName );
            ZipFile.ExtractToDirectory("./Templates/" + Template + ".zip", d.FullName);
            var z = new ProjectFile();
            z.Name = name;

            foreach (var i in File.ReadAllLines(Path.Combine(d.FullName, "index.dat")))
            {
                if (i != "")
                {
                    z.Files.Add(i);

                }
            }

            File.WriteAllText(Path.Combine(d.FullName, name + ".proj"), JsonConvert.SerializeObject(z));
            Global.CurrentProjectFile = z;
            Global.CurrentProjectFilePath = d.FullName;
            this.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (!Directory.Exists(textBox1.Text))
                {
                    if (listBox1.SelectedIndex != -1)
                    {
                        if (Templates[listBox1.SelectedIndex].ToLower() != "null")
                        {
                            CreateProject(textBox1.Text, textBox2.Text, Templates[listBox1.SelectedIndex]);
                        }
                        else
                        {
                            MessageBox.Show("Please select a project template.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please select a project template.");
                    }
                }
                else
                {
                    MessageBox.Show("Project Name Alredy taken.");
                }

            }
            else
            {
                MessageBox.Show("Please make sure you provided a name and folder.");

            }

        }

        private void New_Project_Load(object sender, EventArgs e)
        {
            foreach (var i in File.ReadAllText("Template.map").Replace("\r\n", "\n").Split('\n'))
            {
                if(!string.IsNullOrEmpty(i))
                {
                    string[] z = i.Split('~');
                    listBox1.Items.Add(z[0]);
                    Templates.Add(z[1]);
                }

            }

            listBox1.SelectedIndex = 0;
        }
    }
}
