using OsDevKit.UI.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsDevKit.UI
{
    public partial class BuildFileEditor : Form
    {
        public BuildFileEditor()
        {
            InitializeComponent();
        }

        private void BuildFileEditor_Load(object sender, EventArgs e)
        {
            if (Global.CurrentBuildFile == null)
            {
                NewBuildFileDialog dlg = new NewBuildFileDialog();
                //dlg.MdiParent = this.MdiParent;
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                }
                else
                {

                    this.BeginInvoke(new MethodInvoker(Close));
                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Global.CurrentBuildFile != null)
            {
                label1.Text = "Name : " + Global.CurrentBuildFile.Name;
            }
        }
    }
}
