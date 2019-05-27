using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eclock
{
    public partial class frmMainMenuMemberRaceMode : Form
    {
        DataTable ClubCollection;
        BIZ.Entry BizEntry;

        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public Boolean GetRaceInfo { get; set; }
        public String MobileNumber { get; set; }
        public String SMSActivated { get; set; }

        public frmMainMenuMemberRaceMode()
        {
            InitializeComponent();
        }

        private void frmRaceMode_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                GetClubInfoList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void frmRaceMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.CloseSubForm(this);
        }

        private void btnStartEclock_Click(object sender, EventArgs e)
        {
            try
            {
                frmReadRFID readRFID = new frmReadRFID();
                readRFID.EntryList = (DataTable)dataGridView1.DataSource;
                readRFID.Mode = "RaceMode";
                readRFID.MobileNumber = BIZ.Common.GetSystemConfigValue("mobileNumber");
                readRFID.SMSActivated = BIZ.Common.GetSystemConfigValue("smsactivated");
                readRFID.ShowDialog();
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
                                    string raceinfopath = directoryitem.Root + directoryitem.Name + "\\" + ClubName + "\\" + "RaceInfo.inf";
                                    string entrypath = directoryitem.Root + directoryitem.Name + "\\" + ClubName + "\\" + "RaceEntry.inf";
                                    if (File.Exists(raceinfopath))
                                    {
                                        TextReader tr = new StreamReader(raceinfopath);
                                        using (tr)
                                        {
                                            string[] raceinfo = tr.ReadLine().Split('|');
                                            this.MemberID = Convert.ToInt64(raceinfo[3].ToString());
                                            this.txtLiberationPoint.Text = raceinfo[4].ToString();
                                            this.txtReleaseDate.Text = raceinfo[5].ToString();
                                            this.txtCoordinates.Text = raceinfo[6].ToString();
                                            Clear();
                                        }
                                    }

                                    if (File.Exists(entrypath))
                                    {
                                        DataTable dtEntry = new DataTable();
                                        dtEntry = BIZ.Common.ConvertTextDataToTable(entrypath);
                                        this.dataGridView1.DataSource = dtEntry;

                                        foreach (DataColumn columns in dtEntry.Columns)
                                        {
                                            this.dataGridView1.Columns[columns.ColumnName].Visible = false;
                                            if (columns.ColumnName == "BandNumber") this.dataGridView1.Columns[columns.ColumnName].Visible = true;
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

        private BIZ.Entry PopulateEntryBizLayer(BIZ.Entry bizData)
        {
            try
            {
                bizData.ClubID = this.ClubID;
                bizData.ReleaseDate = Convert.ToDateTime(txtReleaseDate.Text);
                bizData.MemberID = this.MemberID;
                return bizData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnGetRaceInfo_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsResult = new DataSet();
                BizEntry = new BIZ.Entry();
                BizEntry = PopulateEntryBizLayer(BizEntry);
                dsResult = BizEntry.GetReleasePointDetails();

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsResult.Tables[0].Rows[0];
                        txtMinimumSpeed.Text = dr["MinSpeed"].ToString();
                        txtReleaseTime.Text = "NOT RELEASE";
                        if (dr["ReleaseTime"].ToString() != "") txtReleaseTime.Text = dr["ReleaseTime"].ToString();
                        txtStopTimeFrom.Text = dr["StopTimeFrom"].ToString();
                        txtStopTimeTo.Text = dr["StopTimeTo"].ToString();
                        txtCutoff.Text = dr["CutOff"].ToString();
                        txtDistance.Text = dr["Distance"].ToString();
                        txtNoOfEntry.Text = dr["TotalEntry"].ToString();
                        txtEclockRaceResult.Text = dr["TotalEclockResult"].ToString();
                        txtTotalSMSClock.Text = dr["TotalSMSResult"].ToString();
                        RaceReleasePointID = (Int64)dr["RaceReleasePointID"];
                        GetRaceInfo = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void Clear()
        {
            try
            {
                txtMinimumSpeed.Text = "";
                txtReleaseTime.Text = "";
                txtStopTimeFrom.Text = "";
                txtStopTimeTo.Text = "";
                txtCutoff.Text = "";
                txtNoOfEntry.Text = "";
                txtEclockRaceResult.Text = "";
                txtTotalSMSClock.Text = "";
                RaceReleasePointID = 0;
                GetRaceInfo = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void btnViewResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GetRaceInfo)
                {
                    MessageBox.Show("Please get race information to view result." + Environment.NewLine +
                                    "Click GET RACE INFO button");
                    return;
                }
                if (txtReleaseTime.Text == "")
                {
                    MessageBox.Show("Bird not yet release.");
                    return;
                }

                frmBirdTimeArrival raceresult = new frmBirdTimeArrival();
                raceresult.RaceReleasePointID = RaceReleasePointID;
                raceresult.MemberID = this.MemberID;
                raceresult.EntryList = (DataTable)dataGridView1.DataSource;
                raceresult.ReleaseDate = Convert.ToDateTime(this.txtReleaseDate.Text + " " + this.txtReleaseTime.Text);
                raceresult.ClubID = this.ClubID;
                raceresult.ClubName = this.ClubName;
                raceresult.CutOff = Convert.ToDateTime(this.txtReleaseDate.Text);
                raceresult.Distance = Convert.ToDecimal(this.txtDistance.Text);
                raceresult.Mode = "RaceMode";
                raceresult.IsStop = false;

                if (this.txtStopTimeFrom.Text != "")
                {
                    raceresult.IsStop = true;
                    raceresult.StopTimeFrom = Convert.ToDateTime(txtStopTimeFrom.Text);
                    raceresult.StopTimeTo = Convert.ToDateTime(txtStopTimeTo.Text);
                }

                if (this.txtCutoff.Text != "") raceresult.CutOff = Convert.ToDateTime(this.txtCutoff.Text);
                raceresult.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
