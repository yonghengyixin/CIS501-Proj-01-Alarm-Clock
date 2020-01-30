using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CIS501_Proj_01_Alarm_Clock
{
    public partial class Form1 : Form
    {
        public string Time = null;
        private int count = 0;
        private int timeNum = 0;
        private string fileName = "Time.txt";
        private string[] timeData = new string[10];
        System.Timers.Timer timer1 = new System.Timers.Timer(1000);
        System.Timers.Timer timer5 = new System.Timers.Timer(5000);

        public Form1()
        {
            InitializeComponent();
            StreamReader File = new StreamReader(fileName);
            string line;
            while((line = File.ReadLine()) != null)
            {
                listBox.Items.Add(line);
                timeData[timeNum] = line;
                timeNum++;
            }
            File.Close();
        }

        /// <summary>
        /// Add Alarm to listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {

            StreamWriter File = new StreamWriter(fileName);
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if(f2.Time != null)
            {
                listBox.Items.Add(f2.Time);
                File.WriteLine(f2.Time);
                timeNum++;
                timeData[timeNum] = f2.Time;
            }
            File.Close();
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
                timeData[line] = f2.Time;
                StreamWriter File = new StreamWriter(fileName);
                File.Flush();
                timeNum = 0;
                while(timeData[timeNum] != null)
                {
                    File.WriteLine(timeData[timeNum]);
                    timeNum++;
                }
                File.Close();
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
            bool alarmOn = false;

            StreamReader File = new StreamReader(fileName);
            string line;
            while ((line = File.ReadLine()) != null)
            {
                string[] pieces = line.Split(' ');
                if (string.Equals(pieces[2], "On"))
                {
                    alarmOn = true;
                }
            }
            File.Close();

            if ((string.Equals(Label.Text, "Stopped")) && alarmOn == true)
            {
                count++;
                if(count > 5)
                {
                    Label.Text = "Running";
                    count = 0;
                }
            }
            else if((!string.Equals(Label.Text, "Stopped")) && alarmOn == true)
            {
                Label.Text = "Running";
            }
            else
            {
                Label.Text = "Stopped";
            }

            timer1.AutoReset = true;
            timer1.Elapsed += TimeCompare;
            timer1.Start();
        }

        /// <summary>
        /// snooze Alarm will noise 30 second later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            SnoozeButton.Enabled = false;
            StopButton.Enabled = false;
            Label.Text = "Running";
            timer5.Elapsed += OnTime;
            timer5.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            SnoozeButton.Enabled = false;
            StopButton.Enabled = false;
            Label.Text = "Stopped";
        }

        /// <summary>
        /// compare current time and system time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeCompare(object sender, System.Timers.ElapsedEventArgs e)
        {
            int i = 0;
            while (timeData[i] != null)
            {
                string[] pieces = timeData[i].Split(' ');
                StringBuilder sb = new StringBuilder();
                sb.Append(pieces[0]);
                sb.Append(pieces[1]);
                if (string.Equals(pieces[2], "On"))
                {
                    if (string.Equals(sb.ToString(), System.DateTime.Now.ToString()))
                    {
                        SnoozeButton.Enabled = true;
                        StopButton.Enabled = true;
                        Label.Text = "Alarm went off";
                    }
                }
                i++;
            }
        }

        private void OnTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            SnoozeButton.Enabled = true;
            StopButton.Enabled = true;
            Label.Text = "Alarm went off";
        }
    }
}
