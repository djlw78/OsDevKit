﻿using System;
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
    public partial class Output : Form
    {
        public Output()
        {
            InitializeComponent();
        }

        string buff = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Global.OutPut != buff)
            {
                richTextBox1.Text = Global.OutPut;
                
                buff = Global.OutPut;
            }
        }
    }
}
