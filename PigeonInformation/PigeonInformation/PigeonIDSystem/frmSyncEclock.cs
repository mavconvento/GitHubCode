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

        public DateTime DateRelease { get; set; }
        public String MemberID { get; set; }

        public frmSyncEclock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ActionType == "BANDED")
            {
                SyncBanded();
            }
            else if (ActionType == "ENTRY")
            {
                SyncEntry();
            }
            else if (ActionType == "ENTRYDB")
            {
                SyncEntryInDatabase();
            }
            else if (ActionType == "RESULTDB")
            {
                SyncResultInDatabase();
            }
            else if (ActionType == "RESULT")
            {
                SyncResult();
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
            }
        }

        private void SyncResult()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = path + "\\resultinfo.txt";
                string Actionfilepath = path + "\\action.txt";

                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string[] args = { MemberID, dateString, ReadText.ReadFilePath("datapath") };
                string[] actionargs = {"RESULT","LOCAL"};

                System.IO.File.WriteAllLines(filepath, args); //pigeondetails
                System.IO.File.WriteAllLines(Actionfilepath, actionargs); //pigeondetails

                var process = Process.Start(path + "\\SyncEclock.exe");
                process.WaitForExit();
                var exitCode = process.ExitCode;
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
                string[] actionargs = { "RESULTDB"};

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

                string[] args = { ClubName, dateString, ReadText.ReadFilePath("datapath"),this.DateRelease.ToShortDateString() };
                string[] actionargs = { "ENTRYDB"};

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
                    String bandedData = "$BaNd$" +
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

                eclock.SendData("$Done$|#", commPort);
                MessageBox.Show("Data sync", "Eclock Sync");
                this.Close();
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
                progressBar1.Maximum = dt.Rows.Count;
                System.Threading.Thread.Sleep(2500);

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
        }
    }
}
