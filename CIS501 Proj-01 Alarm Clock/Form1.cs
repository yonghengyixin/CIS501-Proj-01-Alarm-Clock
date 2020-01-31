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
        public string[] timeData = new string[10];
        System.Timers.Timer timer1 = new System.Timers.Timer(1000);

        public Form1()
        {
            InitializeComponent();

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = new StreamWriter(fileName)) ;
            }

            StreamReader sr = new StreamReader(fileName);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                listBox.Items.Add(line);
                timeData[timeNum] = line;
                timeNum++;
            }
            sr.Close();
        }

        /// <summary>
        /// Add Alarm to listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {

            StreamWriter sw = new StreamWriter(fileName);
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if(f2.Time != null)
            {
                listBox.Items.Add(f2.Time);//add one to listbox

                timeData[timeNum] = f2.Time;//add one to timeData array
                timeNum++;

                sw.Flush();
                timeNum = 0;
                while (timeData[timeNum] != null)
                {
                    sw.WriteLine(timeData[timeNum]);//add one to file
                    timeNum++;
                }
            }
            sw.Close();
        }

        /// <summary>
        /// edit the choosed item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            int line = listBox.SelectedIndex;//get the choosed line number
            Time = listBox.SelectedItem.ToString();
            Form2 f2 = new Form2(Time);
            f2.ShowDialog();
            if (f2.Time != null)
            {
                listBox.Items.RemoveAt(line);
                listBox.Items.Insert(line, f2.Time);//change the listbox

                timeData[line] = f2.Time;//change the timeData array

                StreamWriter sw = new StreamWriter(fileName);//change the file data
                sw.Flush();
                timeNum = 0;
                while(timeData[timeNum] != null)
                {
                    sw.WriteLine(timeData[timeNum]);
                    timeNum++;
                }
                sw.Close();

            }
            this.Invoke(new Action(() => { EditButton.Enabled = false; }));
            Time = null;
        }

        /// <summary>
        /// if user click the listBox, EditButton should enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.Invoke(new Action(() => { EditButton.Enabled = true; }));
        }

        /// <summary>
        /// snooze Alarm will noise 30 second later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => { SnoozeButton.Enabled = false; }));
            this.Invoke(new Action(() => { StopButton.Enabled = false; }));
            this.Invoke(new Action(() => { Label.Text = "Snooze 5 sec"; }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => { SnoozeButton.Enabled = false; }));
            this.Invoke(new Action(() => { StopButton.Enabled = false; }));
            this.Invoke(new Action(() => { Label.Text = "Stopped 5 sec"; }));//stop 
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
            timer1.Start();
        }

        /// <summary>
        /// compare current time and system time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeCompare(object sender, System.Timers.ElapsedEventArgs e)
        {
            int i = 0;
            bool alarmOn = false;

            foreach(string a in timeData)
            {
                if (a != null)
                {
                    string[] pieces = a.Split(' ');
                    if (string.Equals(pieces[2], "On"))
                    {
                        alarmOn = true;
                    }
                }
                
            }

            if ((string.Equals(Label.Text, "Snooze 5 sec")) && alarmOn == true)
            {
                count++;
                if (count > 5)
                {
                    this.Invoke(new Action(() => { Label.Text = "Running"; }));
                    count = 0;
                    OnTime();
                }
            }
            else if ((string.Equals(Label.Text, "Stopped 5 sec")) && alarmOn == true)//stop button and On alarm
            {
                count++;
                if (count > 5)
                {
                    this.Invoke(new Action(() => { Label.Text = "Running"; }));
                    count = 0;
                }
            }
            else if ((!string.Equals(Label.Text, "Stopped")) && alarmOn == true && (!string.Equals(Label.Text, "Alarm went off")) &&
                (!string.Equals(Label.Text, "Stopped 5 sec")) && (!string.Equals(Label.Text, "Snooze 5 sec")))//no stopped, no ontime and On alarm
            {
                this.Invoke(new Action(() => { Label.Text = "Running"; }));
            }
            else if (alarmOn == false)//all alarm off
            {
                this.Invoke(new Action(() => { Label.Text = "Stopped"; }));
            }
            else if (string.Equals(Label.Text, "Alarm went off")) ;//if On time
            else
            {
                this.Invoke(new Action(() => { Label.Text = ""; }));
            }

            while (timeData[i] != null)//if compared into
            {
                string[] dataPieces = timeData[i].Split(' ');// 11:01:36 PM OFF
                string[] realPieces = System.DateTime.Now.ToString().Split(' ');// 1/30/2020 11:01:36 PM

                StringBuilder sbData = new StringBuilder();// 11:01:36 PM
                sbData.Append(dataPieces[0]);
                sbData.Append(dataPieces[1]);

                StringBuilder sbReal = new StringBuilder();// 11:01:36 PM
                sbReal.Append(realPieces[1]);
                sbReal.Append(realPieces[2]);

                if (string.Equals(dataPieces[2], "On"))
                {
                    if (string.Equals(sbData.ToString(), sbReal.ToString()))
                    {
                        this.Invoke(new Action(() => { SnoozeButton.Enabled = true; }));
                        this.Invoke(new Action(() => { StopButton.Enabled = true; }));
                        this.Invoke(new Action(() => { Label.Text = "Alarm went off"; }));
                    }
                }
                i++;
            }
        }

        private void OnTime()
        {
            this.Invoke(new Action(() => { SnoozeButton.Enabled = true; }));
            this.Invoke(new Action(() => { StopButton.Enabled = true; }));
            this.Invoke(new Action(() => { Label.Text = "Alarm went off"; }));
        }
    }
}
