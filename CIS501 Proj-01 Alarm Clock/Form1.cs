using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIS501_Proj_01_Alarm_Clock
{
    public partial class Form1 : Form
    {
        public string Time = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if(f2.Time != null)
            {
                listBox.Items.Add(f2.Time);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            int line = listBox.SelectedIndex;
            Time = listBox.SelectedItem.ToString();
            Form2 f2 = new Form2(Time);
            f2.ShowDialog();
            if (f2.Time != null)
            {
                listBox.Items.RemoveAt(line);
                listBox.Items.Insert(line, f2.Time);
            }
            EditButton.Enabled = false;
            Time = null;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditButton.Enabled = true;
        }
    }
}
