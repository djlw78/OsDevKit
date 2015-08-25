﻿using FastColoredTextBoxNS;
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
    public partial class Editor : Form
    {
        public string FileName { get; set; }

        public Editor()
        {
            InitializeComponent();
        }

        Style Green = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        Style Maroon = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        Style Blue = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Green);
            e.ChangedRange.ClearStyle(Blue);
            e.ChangedRange.ClearStyle(Maroon);


            e.ChangedRange.SetStyle(Maroon, "\"(.*)\"");
            e.ChangedRange.SetStyle(Maroon, "#(.*)");
            e.ChangedRange.SetStyle(Green, "//(.*)");
            e.ChangedRange.SetStyle(Green, "/\\*(.*)\\*/", System.Text.RegularExpressions.RegexOptions.Multiline);

            e.ChangedRange.SetStyle(Blue, "auto|break|case|char(\\s)+|const|continue|default|do|double(\\s)+|enum(\\s)+|extern|floa(\\s)+t|for|goto|" +
                "if|int(\\s)+|long|register|return|short|signed|sizeof|static|struct|switch|typedef|union|unsigned|void(\\s)+|volatile|while");

            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (String.IsNullOrEmpty(FileName))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, fastColoredTextBox1.Text);
                }
            }
            else
            {
                File.WriteAllText(Path.Combine(Global.CurrentProjectFilePath + "\\files\\" + FileName), fastColoredTextBox1.Text);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fastColoredTextBox1.Text = File.ReadAllText(dlg.FileName);
                this.Text = "Editor - " + new FileInfo(dlg.FileName).Name;
                FileName = new FileInfo(dlg.FileName).Name;
                //  MdiParent.LayoutMdi(MdiLayout.Cascade);
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FileName))
            {
                try {
                    fastColoredTextBox1.Text = File.ReadAllText(Path.Combine(Global.CurrentProjectFilePath, "files", FileName));
                    this.Text = "Editor - " + FileName;
                }
                catch(Exception ee)
                {

                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }
    }
}
