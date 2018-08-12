using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PegionClocking.Common;
using PegionClocking.DAL;
using System.IO;

namespace PegionClocking
{
    public partial class frmExportEclockData : Form
    {
        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Boolean IsAdmin { get; set; }
        #endregion

        #region Events
        public frmExportEclockData()
        {
            InitializeComponent();
        }

        private void frmExportEclockData_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ExportEclockData();
        }
        #endregion


        #region Private
        private void ExportEclockData()
        {
            try
            {
                String Drive = "";
                string[] recordTypeCollection = { "Masterlist", "ReleasePoint", "RegisterRFID", "MemberRegisterRFID" };
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo item in allDrives)
                {
                    if (item.VolumeLabel == "ECLOCK")
                    {
                        Drive = item.Name;
                        break;
                    }
                }

                if (Drive == "")
                {
                    MessageBox.Show("Elock SD Card not found");
                    return;
                }

                foreach (string item in recordTypeCollection)
                {
                    switch (item)
                    {
                        case "Masterlist":
                            EClockMasterListExport(Drive);
                            break;
                        case "ReleasePoint":
                            EClockReleasePointExport(Drive);
                            break;
                        case "RegisterRFID":
                            EClockRegisterRFIDExport(Drive);
                            break;
                        case "MemberRegisterRFID":
                            EClockMemberRegisterRFIDExport(Drive);
                            break;

                        default:
                            break;
                    }
                }
                MessageBox.Show("E-Clock Data exported successfully.", "Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EClockMasterListExport(String Drive)
        {
            DataTable dt = new DataTable();
            BIZ.Member member = new BIZ.Member();
            member.ClubID = ClubID;
            dt = member.ExportEClockMasterlist();

            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = dt.Rows.Count;
            this.progressBar1.Step = 1;
            this.progressBar1.Value = 0;
            this.label1.Text = "Exporting Masterlist Data";
            string path = Drive + @"Masterlist.inf";
            string collection = "";
            if (File.Exists(path)) File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        collection = ClubID.ToString() + "|" + item["MemberID"].ToString() + "|" + item["MemberID No."].ToString() + "|" + item["Name"].ToString() + "|" + item["Coordinates"].ToString();
                        sw.WriteLine(Common.Common.Encrypt(collection));
                        System.Threading.Thread.Sleep(50);
                        this.progressBar1.PerformStep();
                    }
                }
            }
        }
        private void EClockReleasePointExport(String Drive)
        {
            DataTable dt = new DataTable();
            BIZ.RaceReleasePoint raceReleasePoint = new BIZ.RaceReleasePoint();
            raceReleasePoint.ClubID = ClubID;
            dt = raceReleasePoint.EClockRaceReleasePoint();

            this.progressBar2.Minimum = 0;
            this.progressBar2.Maximum = dt.Rows.Count;
            this.progressBar2.Step = 1;
            this.progressBar2.Value = 0;

            string path = Drive + @"ReleasePoint.inf";
            string collection = "";
            if (File.Exists(path)) File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        collection = ClubID.ToString() + "|" + item["RaceScheduleID"].ToString() + "|" + item["RaceScheduleName"].ToString() + "|" + item["RaceScheduleCategoryID"].ToString() + "|" + item["RaceScheduleCategoryName"].ToString() + item["RaceScheduleDetailsID"].ToString() + item["LocationID"].ToString() + item["LocationName"].ToString() + item["RaceReleasePointID"].ToString() + item["LapNo"].ToString() + item["MinSpeed"].ToString() + item["DateRelease"].ToString();
                        sw.WriteLine(Common.Common.Encrypt(collection));
                        System.Threading.Thread.Sleep(50);
                        this.progressBar2.PerformStep();
                    }
                }
            }
        }
        private void EClockRegisterRFIDExport(String Drive)
        {
            DataTable dt = new DataTable();
            BIZ.Entry entry = new BIZ.Entry();
            entry.ClubID = ClubID;
            dt = entry.EClockRegisterRFID();

            this.progressBar3.Minimum = 0;
            this.progressBar3.Maximum = dt.Rows.Count;
            this.progressBar3.Step = 1;
            this.progressBar3.Value = 0;

            string path = Drive + @"RegisterRFID.inf";
            string collection = "";
            if (File.Exists(path)) File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        collection = ClubID.ToString() + "|" + item["RFIDSerialNo"].ToString();
                        sw.WriteLine(Common.Common.Encrypt(collection));
                        System.Threading.Thread.Sleep(50);
                        this.progressBar3.PerformStep();
                    }
                }
            }
        }
        private void EClockMemberRegisterRFIDExport(String Drive)
        {
            DataTable dt = new DataTable();
            BIZ.Entry entry = new BIZ.Entry();
            entry.ClubID = ClubID;
            dt = entry.EClockRegisterRFIDMember();

            this.progressBar4.Minimum = 0;
            this.progressBar4.Maximum = dt.Rows.Count;
            this.progressBar4.Step = 1;
            this.progressBar4.Value = 0;

            string path = Drive + @"MemberRegisterRFID.inf";
            string collection = "";
            if (File.Exists(path)) File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        collection = ClubID.ToString() + "|" + item["MemberID"].ToString() + "|" + item["RFIDSerialNo"].ToString();
                        sw.WriteLine(Common.Common.Encrypt(collection));
                        System.Threading.Thread.Sleep(50);
                        this.progressBar4.PerformStep();
                    }
                }
            }
        }
        #endregion


        
    }
}
