using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmSyncEclock : Form
    {
        public String ClubName { get; set; }
        public DataTable DataList { get; set; }
        public String ActionType { get; set; }
        public int DataStartIndex { get; set; }
        public int DataEndtIndex { get; set; }

        public DateTime DateRelease { get; set; }
        public String MemberID { get; set; }

        public frmSyncEclock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActionType == "BANDED")
                {
                    DataStartIndex = Convert.ToInt32(this.txtCount.Text);
                    DataEndtIndex = Convert.ToInt32(this.txtto.Text);
                    SyncBanded();
                }
                else if (ActionType == "ENTRY")
                {
                    DataStartIndex = Convert.ToInt32(this.txtCount.Text);
                    DataEndtIndex = Convert.ToInt32(this.txtto.Text);
                    SyncEntry();
                }
                else if (ActionType == "UNLOCK")
                {
                    UnLockEclock();
                }
                else if (ActionType == "ENTRYDB")
                {
                    SyncEntryInDatabase();
                }
                else if (ActionType == "RESULTDB")
                {
                    SyncResultInDatabase();
                }
                else if (ActionType == "RESULTRACE")
                {
                    SyncResult(ActionType);
                }
                else if (ActionType == "RESULTTRAINING")
                {
                    SyncResult(ActionType);
                }
                else if (ActionType == "UPLOADPROGRAM")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "ProgramLoader";
                    var process = Process.Start(path + "\\ConsoleApp1.exe");
                    process.WaitForExit();
                    var exitCode = process.ExitCode;
                    this.Close();
                }
                else if (ActionType == "RESET")
                {
                    Eclock eclock = new Eclock();
                    string serialPort = eclock.GetPort();
                    string[] ports = SerialPort.GetPortNames();
                    string commPort = "";
                    foreach (var item in ports)
                    {
                        if (serialPort.Contains(item)) commPort = item;
                    }

                    if (commPort != "")
                    {
                        eclock.SyncTime(commPort);
                    }
                    this.Close();
                }
                else if (ActionType == "CLOCK")
                {
                    Eclock eclock = new Eclock();
                    string serialPort = eclock.GetPort();
                    string[] ports = SerialPort.GetPortNames();
                    string commPort = "";
                    foreach (var item in ports)
                    {
                        if (serialPort.Contains(item)) commPort = item;
                    }

                    if (commPort != "")
                    {
                        eclock.SyncTime(commPort);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Common.Logs(ex.Message);
            }
        }

        private void SyncResult(string action)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = path + "\\resultinfo.txt";
                string Actionfilepath = path + "\\action.txt";

                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string[] args = { MemberID, dateString, ReadText.ReadFilePath("datapath") };
                string[] actionargs = { action, "LOCAL" };

                System.IO.File.WriteAllLines(filepath, args); //pigeondetails
                System.IO.File.WriteAllLines(Actionfilepath, actionargs); //pigeondetails

                var process = Process.Start(path + "\\SyncEclock.exe");
                process.WaitForExit();
                var exitCode = process.ExitCode;
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SyncResultInDatabase()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = path + "\\resultinfo.txt";
                string Actionfilepath = path + "\\action.txt";

                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string[] args = { ClubName, dateString, ReadText.ReadFilePath("datapath"), this.DateRelease.ToShortDateString() };
                string[] actionargs = { "RESULTDB" };

                System.IO.File.WriteAllLines(filepath, args); //pigeondetails
                System.IO.File.WriteAllLines(Actionfilepath, actionargs); //pigeondetails

                var process = Process.Start(path + "\\SyncEclock.exe");
                process.WaitForExit();
                var exitCode = process.ExitCode;
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void SyncEntryInDatabase()
        {
            try
            {

                string path = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = path + "\\resultinfo.txt";
                string Actionfilepath = path + "\\action.txt";


                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string[] args = { ClubName, dateString, ReadText.ReadFilePath("datapath"), this.DateRelease.ToShortDateString() };
                string[] actionargs = { "ENTRYDB" };

                System.IO.File.WriteAllLines(filepath, args); //pigeondetails
                System.IO.File.WriteAllLines(Actionfilepath, actionargs); //pigeondetails

                var process = Process.Start(path + "\\SyncEclock.exe");
                process.WaitForExit();
                var exitCode = process.ExitCode;
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SyncBanded()
        {
            Eclock eclock = new Eclock();
            string serialPort = eclock.GetPort();
            string[] ports = SerialPort.GetPortNames();
            string commPort = "";
            foreach (var item in ports)
            {
                if (serialPort.Contains(item)) commPort = item;
            }

            if (!String.IsNullOrEmpty(commPort))
            {

                //eclock.SyncTime(commPort);
                DataTable dt = new DataTable();
                dt = DataList;

                eclock.SendData("$Stat$", commPort);
                progressBar1.Maximum = dt.Rows.Count;
                System.Threading.Thread.Sleep(2500);

                int counter = 1;
                foreach (DataRow item in dt.Rows)
                {
                    if (counter >= DataStartIndex && counter <= DataEndtIndex)
                    {
                        string path = ReadText.ReadFilePath("datapath");
                        string pigeonMobileListPath = path + "\\PigeonMobileList\\" + item["TagID"].ToString() + ".txt";
                        string mobileNumber = "";
                        if (File.Exists(pigeonMobileListPath))
                        {
                            string[] pigeonMobileCollection = ReadText.ReadTextFile(pigeonMobileListPath);
                            string[] values = pigeonMobileCollection[0].ToString().Split('|');
                            mobileNumber = values[1].ToString().Trim();
                        }

                        String bandedData = "$BaNd$" +
                                        ClubName + "|" +
                                        item["BandNumber"].ToString() + "|" +
                                        item["TagID"].ToString() + "|" +
                                        item["Category"].ToString() + "|" +
                                        item["Color"].ToString() + "|" +
                                        item["Sex"].ToString().Substring(0, 1) + (mobileNumber != "" ? "|" + mobileNumber + "|#" : "|#");

                        eclock.SendData(bandedData, commPort);
                        this.txtCount.Text = counter.ToString();
                        System.Threading.Thread.Sleep(2500);
                    }

                    progressBar1.Value = counter;
                    counter++;
                }

                eclock.SendData("$Done$|#", commPort);
                MessageBox.Show("Data sync", "Eclock Sync");
                this.Close();
            }
        }

        private void UnLockEclock()
        {
            try
            {
                Eclock eclock = new Eclock();
                string serialPort = eclock.GetPort();
                string[] ports = SerialPort.GetPortNames();
                string commPort = "";

                foreach (var item in ports)
                {
                    if (serialPort.Contains(item)) commPort = item;
                }

                eclock.SendData("$UnLo$", commPort);
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SyncEntry()
        {
            try
            {
                Eclock eclock = new Eclock();
                string serialPort = eclock.GetPort();
                string[] ports = SerialPort.GetPortNames();
                string commPort = "";
                foreach (var item in ports)
                {
                    if (serialPort.Contains(item)) commPort = item;
                }

                //eclock.SyncTime(commPort);
                DataTable dt = new DataTable();
                dt = DataList;


                eclock.SendData("$Stat$", commPort);
                System.Threading.Thread.Sleep(1000);

                progressBar1.Maximum = dt.Rows.Count;
                System.Threading.Thread.Sleep(1000);

                int counter = 1;
                if (!String.IsNullOrEmpty(commPort))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        String bandedData = "$Enty$" +
                                            ClubName + "|" +
                                            item["BandNumber"].ToString() + "|" +
                                            item["TagID"].ToString() + "|" +
                                            item["Category"].ToString() + "|" +
                                            item["Color"].ToString() + "|" +
                                            item["Sex"].ToString().Substring(0, 1) + "|#";

                        eclock.SendData(bandedData, commPort);
                        progressBar1.Value = counter;
                        System.Threading.Thread.Sleep(1000);
                        counter++;
                    }
                }

                eclock.SendData("$Done$|#", commPort);
                MessageBox.Show("Data sync", "Eclock Sync");
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SyncEclock_Load(object sender, EventArgs e)
        {

            Eclock eclock = new Eclock();
            string serialPort = eclock.GetPort();
            string[] ports = SerialPort.GetPortNames();
            string commPort = "";
            foreach (var item in ports)
            {
                if (serialPort.Contains(item)) commPort = item;
            }

            if (!String.IsNullOrEmpty(commPort)) eclock.InitializeEclock(commPort);
            this.label1.Text = "E-CLOCK SYNC " + ActionType;

            if (DataStartIndex > 0)
            {
                this.txtCount.Text = DataStartIndex.ToString();
                this.txtto.Text = DataEndtIndex.ToString();
                this.lblCount.Visible = true;
                this.label2.Visible = true;
                this.txtCount.Visible = true;
                this.txtto.Visible = true;
            }
        }

        private void txtCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }
    }
}
