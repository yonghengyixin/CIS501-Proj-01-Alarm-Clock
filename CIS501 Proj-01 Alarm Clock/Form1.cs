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
        System.Timers.Timer timer1 = new System.Timers.Timer(1000);

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add Alarm to listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if(f2.Time != null)
            {
                listBox.Items.Add(f2.Time);//also need add to file
            }
        }

        /// <summary>
        /// edit the choosed item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            int line = listBox.SelectedIndex;
            Time = listBox.SelectedItem.ToString();//should change to the time which from file
            Form2 f2 = new Form2(Time);
            f2.ShowDialog();
            if (f2.Time != null)
            {
                listBox.Items.RemoveAt(line);
                listBox.Items.Insert(line, f2.Time);//also need change file's data
            }
            EditButton.Enabled = false;
            Time = null;
        }

        /// <summary>
        /// if user click the listBox, EditButton should enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditButton.Enabled = true;
        }

        /// <summary>
        /// run it, if this app is open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.AutoReset = true;
            timer1.Elapsed += TimeCompare;
        }

        /// <summary>
        /// compare current time and system time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeCompare(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        /// <summary>
        /// snooze Alarm will noise 30 second later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnoozeButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {

        }
    }
}
