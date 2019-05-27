using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace Eclock
{
    public partial class frmMainMenuMemberTrainingMode : Form
    {
        DataTable ClubCollection;
        BIZ.Entry BizEntry;

        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String MobileNumber { get; set; }
        public String SMSActivated { get; set; }

        public frmMainMenuMemberTrainingMode()
        {
            InitializeComponent();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            if (txtLiberationPoint.Text == "")
            {
                MessageBox.Show("Enter liberation point.");
                return;
            }
            if (txtReleaseTime.Text == "")
            {
                MessageBox.Show("Bird not yet release.");
                return;
            }

            if (cmbClubList.Text == "")
            {
                MessageBox.Show("No selected club name.");
                return;
            }

            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("No Register RFID.");
                return;
            }

            frmBirdTimeArrival raceresult = new frmBirdTimeArrival();
            raceresult.MemberID = this.MemberID;
            raceresult.EntryList = (DataTable)dataGridView1.DataSource;
            raceresult.ReleaseDate = Convert.ToDateTime(this.dateTimePicker1.Value.ToShortDateString() + " " + this.txtReleaseTime.Text);
            raceresult.ClubID = this.ClubID;
            raceresult.ClubName = this.ClubName;
            raceresult.Distance = Convert.ToDecimal(this.txtDistance.Text);
            raceresult.CutOff = Convert.ToDateTime(this.dateTimePicker1.Value.ToShortDateString() + " " + this.txtReleaseTime.Text);
            raceresult.Mode = "TrainingMode";
            raceresult.ShowDialog();
        }

        private void frmMainMenuMemberTrainingMode_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                GetClubInfoList();
                GetRaceInfo(this.dateTimePicker1.Value);
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void GetRaceInfo(DateTime trainingDate)
        {
            try
            {
                //application directory
                string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();

                //application race result summary file
                string raceinfo = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\TrainingMode\\RaceInfo\\" + this.dateTimePicker1.Value.Year.ToString() + "_" + this.dateTimePicker1.Value.Month.ToString() + "_" + this.dateTimePicker1.Value.Day.ToString() + ".inf";

                //create file if not exists
                if (!File.Exists(raceinfo)) File.Create(raceinfo).Close();

                //write into result result summary
                TextReader tr = new StreamReader(raceinfo);
                using (tr)
                {
                    string content = tr.ReadLine();
                    if (content != null)
                    {
                        string[] infocontent = content.Split('|');
                        txtLiberationPoint.Text = infocontent[0].ToString();
                        txtDistance.Text = infocontent[1].ToString();
                        txtReleaseTime.Text = infocontent[2].ToString();

                    }

                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmMainMenuMemberTrainingMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                BIZ.Common Common = new BIZ.Common();
                Common.CloseSubForm(this);
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }

        }

        private void Initialize()
        {
            try
            {
                //add column in clubcollection table
                ClubCollection = new DataTable();
                ClubCollection.Columns.Add("ClubID");
                ClubCollection.Columns.Add("ClubName");
                ClubCollection.Columns.Add("ClubAbbreviation");

                MobileNumber = BIZ.Common.GetSystemConfigValue("mobileNumber");
                SMSActivated = BIZ.Common.GetSystemConfigValue("smsActivated");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetClubInfoList()
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
                            DirectoryInfo diEclock = new DirectoryInfo(item.Root + item.Name);
                            DirectoryInfo[] directorylistEclock = diEclock.GetDirectories();
                            foreach (DirectoryInfo eclockfolderitem in directorylistEclock)
                            {
                                string eclockInfoFile = eclockfolderitem.Root + item.Name + "\\" + eclockfolderitem.Name + "\\" + "ClubInfo.inf";
                                if (File.Exists(eclockInfoFile))
                                {
                                    TextReader tr = new StreamReader(eclockInfoFile);
                                    using (tr)
                                    {
                                        string[] clubinfo = tr.ReadLine().Split('|');
                                        this.cmbClubList.Items.Add(clubinfo[1].ToString());

                                        DataRow dr = ClubCollection.NewRow();
                                        dr["ClubID"] = clubinfo[0].ToString();
                                        dr["ClubName"] = clubinfo[1].ToString();
                                        dr["ClubAbbreviation"] = clubinfo[2].ToString();

                                        ClubCollection.Rows.Add(dr);
                                    }
                                }

                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void cmbClubList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow item in ClubCollection.Rows)
                {
                    if (item["ClubName"].ToString() == this.cmbClubList.Text)
                    {
                        ClubName = item["ClubAbbreviation"].ToString();
                        ClubID = Convert.ToInt64(item["ClubID"].ToString());
                        DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                        if (driveInfo != null)
                        {
                            DirectoryInfo di = new DirectoryInfo(driveInfo.RootDirectory.ToString());
                            DirectoryInfo[] directoryList = di.GetDirectories();
                            foreach (DirectoryInfo directoryitem in directoryList)
                            {
                                if (directoryitem.Name == "ECLOCK")
                                {
                                    string registerrfid = directoryitem.Root + directoryitem.Name + "\\" + ClubName + "\\" + "RegisterRFID.inf";
                                    if (File.Exists(registerrfid))
                                    {
                                        DataTable dtRegisterRFID = new DataTable();
                                        dtRegisterRFID = BIZ.Common.ConvertTextDataToTable(registerrfid);
                                        this.dataGridView1.DataSource = dtRegisterRFID;

                                        foreach (DataColumn columns in dtRegisterRFID.Columns)
                                        {
                                            this.dataGridView1.Columns[columns.ColumnName].Visible = false;
                                            if (columns.ColumnName == "BandNumber") this.dataGridView1.Columns[columns.ColumnName].Visible = true;
                                            if (columns.ColumnName == "BirdCategory") this.dataGridView1.Columns[columns.ColumnName].Visible = true;
                                            if (columns.ColumnName == "RingType") this.dataGridView1.Columns[columns.ColumnName].Visible = true;
                                        }

                                    }
                                    return;
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void btnStartEclock_Click(object sender, EventArgs e)
        {
            try
            {
                frmReadRFID readRFID = new frmReadRFID();
                readRFID.EntryList = (DataTable)dataGridView1.DataSource;
                readRFID.Mode = "TrainingMode";
                readRFID.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void btnSetLocation_Click(object sender, EventArgs e)
        {
            try
            {
                //liberation point check
                if (txtLiberationPoint.Text == "")
                {
                    MessageBox.Show("Enter Liberation Point", "Error");
                    return;
                }

                //application directory
                string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();

                //application race result summary file
                string raceinfo = ApplicationDirectory + "\\DataCollection\\Member\\Raceresult\\TrainingMode\\RaceInfo\\" + this.dateTimePicker1.Value.Year.ToString() + "_" + this.dateTimePicker1.Value.Month.ToString() + "_" + this.dateTimePicker1.Value.Day.ToString() + ".inf";

                //create file if not exists
                if (!File.Exists(raceinfo)) File.Create(raceinfo).Close();
                File.WriteAllText(raceinfo, String.Empty);

                //write into result result summary
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(raceinfo, true))
                {
                    string infocontent = txtLiberationPoint.Text + "|" + txtDistance.Text + "|" + txtReleaseTime.Text;
                    file.WriteLine(infocontent);
                    file.Close();
                };

                MessageBox.Show("Training Location set successful.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetRaceInfo(this.dateTimePicker1.Value);
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
            
        }
    }
}
