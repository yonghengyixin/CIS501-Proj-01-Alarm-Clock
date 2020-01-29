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
    public partial class Form2 : Form
    {
        public String Time;
        private bool On = false;

        Form1 f1 = new Form1();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string time)
        {
            InitializeComponent();
            if (time != null)
            {
                string[] pieces = time.Split(' ', ':');
                //TimePicker.Value = 
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            Time = TimePicker.Text + " " + On.ToString();
            this.Close();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (On == true)
            {
                OnButton.Checked = false;
                On = false;
            }
            else if (On == false)
            {
                OnButton.Checked = true;
                On = true;
            }
        }
    }
}
