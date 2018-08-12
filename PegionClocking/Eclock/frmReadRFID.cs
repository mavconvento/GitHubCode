using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eclock.BIZ;
using System.IO;
using System.Media;

namespace Eclock
{
    public partial class frmReadRFID : Form
    {
        #region Variables
        BIZ.Race bizRace;
        #endregion

        #region Properties
        public DataTable EntryList { get; set; }
        public String Mode { get; set; }
        public String MobileNumber { get; set; }
        public String SMSActivated { get; set; }
        #endregion

        static System.Timers.Timer timer;

        public frmReadRFID()
        {
            InitializeComponent();
        }

        private void frmReadRFID_Load(object sender, EventArgs e)
        {
            GetTotalBirdArrived();

            timer = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为1000毫秒 就是1秒；
            timer.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            if (backgroundWorker1.IsBusy != true) backgroundWorker1.RunWorkerAsync();

        }

        private void txtRFID_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 0)
            {
                timer.Start();
            }
        }

        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new TextOption(f));//invok 委托实现跨线程的调用

        }
        delegate void TextOption();//定义一个委托

        void f()
        {
            string value = "";
            value = txtRFID.Text;
            txtRFID.Text = "";
            timer.Enabled = false;
            timer.Close();

            if (value.Length >= 15) //default lenght is 18
            {
                string ApplicationDirectory = Common.GetApplicationDirectory();
                string fullpath = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\" + Mode + "\\WithoutTime\\" + value + ".inf";
                SoundPlayer startSoundPlayer = new SoundPlayer(ApplicationDirectory + "beep.wav");
                startSoundPlayer.Play();
                if (!File.Exists(fullpath))
                {
                    File.Create(fullpath).Close();
                }

                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Int64 filecount = 0;

        StartPosition:
            try
            {
                filecount = 0; //reset filecount
                string ApplicationDirectory = Common.GetApplicationDirectory();
                string[] BirdArriveFile = Directory.GetFiles(ApplicationDirectory + "\\DataCollection\\Member\\RaceResult\\" + Mode + "\\WithoutTime");
                foreach (string rfidfile in BirdArriveFile)
                    ProcessDoWork(rfidfile);

            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
            finally
            {
                string ApplicationDirectory = Common.GetApplicationDirectory();
                string[] BirdArriveFile = Directory.GetFiles(ApplicationDirectory + "\\DataCollection\\Member\\RaceResult\\" + Mode + "\\WithoutTime");
                filecount = BirdArriveFile.Length;
            }

            if (filecount > 0)
            {
                goto StartPosition;
            }
        }

        private void ProcessDoWork(string rfidfile)
        {
            try
            {
                bizRace = new BIZ.Race();
                string filename = Path.GetFileNameWithoutExtension(rfidfile);
                string arrivaltime = "";

                //get time into database
                if (Mode == "RaceMode")
                {
                    bizRace.SerialRFIDNo = filename;
                    bizRace.MobileNumber = MobileNumber;
                    bizRace.SMSActivated = SMSActivated;
                    arrivaltime = bizRace.GetArrivalTime().Tables[0].Rows[0]["ArrivalTime"].ToString();
                }
                else
                {
                    arrivaltime = DateTime.Now.ToString();
                }

                string rfidclockdetails = filename + "|" + arrivaltime;
                //string rfidclockdetails = filename + "|" + GetBandNumber(filename) + "|" + arrivaltime; //band number

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rfidfile, true))
                {
                    file.WriteLine(Common.Encrypt(rfidclockdetails));
                    file.Close();
                };

                //application directory
                string ApplicationDirectory = Common.GetApplicationDirectory();

                //application race result summary file
                string resultSummary = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\" + Mode + "\\RaceResultSummary\\" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".inf";

                //create file if not exists
                if (!File.Exists(resultSummary)) File.Create(resultSummary).Close();

                //write into result result summary
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultSummary, true))
                {
                    file.WriteLine(Common.Encrypt(rfidclockdetails));
                    file.Close();
                };

                //if race mode write into sd card for club copy
                if (Mode == "RaceMode")
                {
                    WriteResultInSDCard(rfidclockdetails);
                }

                //application destination file
                string TodayFolder = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                string WithTimeRootDirectory = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\" + Mode + "\\WithTime\\" + TodayFolder;
                if (!Directory.Exists(WithTimeRootDirectory)) Directory.CreateDirectory(WithTimeRootDirectory);

                string destinationFile = WithTimeRootDirectory + "\\" + filename + ".inf";
                if (!File.Exists(destinationFile))
                { File.Move(rfidfile, destinationFile); }
                else
                { File.Delete(rfidfile); }

                backgroundWorker1.ReportProgress(1);
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        private void WriteResultInSDCard(string result)
        {
            try
            {
                DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                if (driveInfo != null)
                {
                    DirectoryInfo di = new DirectoryInfo(driveInfo.RootDirectory.ToString());
                    DirectoryInfo[] directoryList = di.GetDirectories();
                    foreach (DirectoryInfo item in directoryList)
                    {
                        if (item.Name == "ECLOCK")
                        {
                            //application race result summary file
                            string resultSummary = item.Root + item.Name + "\\RaceResultSummary.inf";
                            if (!File.Exists(resultSummary)) File.Create(resultSummary).Close();

                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultSummary, true))
                            {
                                file.WriteLine(Common.Encrypt(result));
                                file.Close();
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError(ex.Message);
            }
        }

        private string GetBandNumber(string serialRFIDNo)
        {
            try
            {
                string bandNumber = "";
                DataRow[] dr = EntryList.Select("SerialRFIDNo = '" + serialRFIDNo + "'");

                foreach (DataRow item in dr)
                {
                    if (item["SerialRFIDNo"].ToString() == serialRFIDNo)
                    {
                        bandNumber = item["BandNumber"].ToString();
                        return bandNumber;
                    }
                }

                return bandNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void logError(String Message)
        {
            string ApplicationDirectory = Common.GetApplicationDirectory();
            string fullpath = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\" + Mode + "\\Logs\\" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".inf";

            //create file log if not exists
            if (!File.Exists(fullpath))
            {
                File.Create(fullpath).Close();
            }

            if (File.Exists(fullpath))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                {
                    file.WriteLine(Message + DateTime.Today.ToString());
                };
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GetTotalBirdArrived();
        }

        private void GetTotalBirdArrived()
        {
            string ApplicationDirectory = Common.GetApplicationDirectory();
            string TodayFolder = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            string WithTimeRootDirectory = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\" + Mode + "\\WithTime\\" + TodayFolder;
            if (!Directory.Exists(WithTimeRootDirectory)) Directory.CreateDirectory(WithTimeRootDirectory);

            //get the number of bird arrived on specific date.
            string[] fileEntries = Directory.GetFiles(WithTimeRootDirectory);
            lblTotalArrived.Text = fileEntries.Length.ToString();
        }

    }
}
