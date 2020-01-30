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

        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// handle editButton part
        /// </summary>
        /// <param name="time"></param>
        public Form2(string time)
        {
            InitializeComponent();
            if (time != null)
            {
                StringBuilder sb = new StringBuilder();
                string[] pieces = time.Split(' ', ':');
                sb.Append(pieces[0] + ":");
                sb.Append(pieces[1] + ":");
                sb.Append(pieces[2] + " ");
                sb.Append(pieces[3]);
                if(pieces[0].Length > 1)
                {
                    TimePicker.Value = DateTime.ParseExact(sb.ToString(), "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    TimePicker.Value = DateTime.ParseExact(sb.ToString(), "h:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                }

                
                if (string.Equals(pieces, "On"))
                {
                    On = true;                }
                else
                {
                    On = false;
                }
            }
        }

        /// <summary>
        /// Cancel all, and don't return anything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// return the time as DateTime to listBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetButton_Click(object sender, EventArgs e)
        {
            if (On)
            {
                Time = TimePicker.Text + " On";
            }
            else
            {
                Time = TimePicker.Text + " OFF";
            }

            this.Close();
        }

        /// <summary>
        /// checkBox has been check or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
