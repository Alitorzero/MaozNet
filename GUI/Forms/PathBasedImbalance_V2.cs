using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkGUI.Forms
{
    public partial class PathBasedImbalance_V2 : Form
    {
        public bool isNull = false;
        public int orderNum = 0;
        public bool isValid = false;

        public PathBasedImbalance_V2()
        {
            InitializeComponent();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                orderNum = 1;
            }

            else if (radioButton2.Checked)
            {
                orderNum = 2;
            }

            else if(radioButton3.Checked)
            {
                orderNum = 3;
            }

            isNull = checkBox1.Checked;
            isValid = true;
            this.Close();
        }
    }
}
