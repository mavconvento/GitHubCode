using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PegionClocking
{
    public partial class frmBirdEntryMasterlist : Form
    {
        #region Variable
        BIZ.RaceCategory raceCategory;
        BIZ.RaceReleasePoint raceReleasePoint;
        BIZ.Entry entry;
        #endregion

        #region Properties
        public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 MemberID { get; set; }
        public DataTable ReleasePointSummary { get; set; }
        public DataTable RaceReleasePointData { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        #endregion

        #region Events
        public frmBirdEntryMasterlist()
        {
            InitializeComponent();
        }

        private void frmBirdEntryMasterlist_Load(object sender, EventArgs e)
        {
           Initialize();
           PopulateCombobox();
           if (ReleasePointSummary.Rows.Count > 0)
           {
               this.dtEntryList.DataSource = ReleasePointSummary;
               this.lblRecordEntry.Text = ReleasePointSummary.Rows.Count.ToString();
           }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmReportGeneration reportGeneration = new frmReportGeneration();
            DataTable dt = new DataTable();
            dt = (DataTable)this.dtEntryList.DataSource;
            reportGeneration.Type = "Entry";
            reportGeneration.dtRecord = dt;
            reportGeneration.ShowDialog();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                EntryListGetByRaceReleasePoint();
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        #endregion

        #region Private Methods
        private void GetControlValue()
        {
            try
            {
                RaceCategoryName = cmbRaceCategory.Text;
                RaceCategoryGroupName = cmbGroupCategory.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EntryListGetByRaceReleasePoint()
        {
            entry = new BIZ.Entry();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
            entry.EntryGetByRaceReleasePoint(this.dtEntryList, this.lblRecordEntry);
        }
        private void PopulateBusinessLayer(Common.Common.RaceEntryClassType Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceEntryClassType.RaceCategory:
                        raceCategory.UserID = UserID;
                        raceCategory.ClubID = ClubID;
                        raceCategory.RaceCategoryName = RaceCategoryName;
                        break;
                    case Common.Common.RaceEntryClassType.RaceReleasePoint:
                        raceReleasePoint.ClubID = ClubID;
                        raceReleasePoint.UserID = UserID;
                        raceReleasePoint.RaceReleasePointID = RaceReleasePointID;
                        break;
                    case Common.Common.RaceEntryClassType.Entry:
                        entry.ClubID = ClubID;
                        entry.UserID = UserID;
                        entry.MemberID = MemberID;
                        entry.RaceCategoryName = RaceCategoryName;
                        entry.RaceCategoryGroupName = RaceCategoryGroupName;
                        entry.RaceReleasePointID = RaceReleasePointID;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateCombobox()
        {
            try
            {
                raceCategory = new BIZ.RaceCategory();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceCategory);

                //Race Schedule
                DataTable dtRaceCategory;
                DataTable dtRaceCategoryGroup;

                dtRaceCategory = raceCategory.RaceCategoryGetByKey().Tables[0];
                dtRaceCategoryGroup = raceCategory.RaceCategoryGetByKey().Tables[1];

                if (dtRaceCategory.Rows.Count > 0)
                {
                    cmbRaceCategory.Items.Add("All");
                    foreach (DataRow dtrow in dtRaceCategory.Rows)
                    {
                        cmbRaceCategory.Items.Add(dtrow["Description"].ToString());
                    }
                }
                if (dtRaceCategoryGroup.Rows.Count > 0)
                {
                    cmbGroupCategory.Items.Add("All");
                    foreach (DataRow dtrow in dtRaceCategoryGroup.Rows)
                    {
                        cmbGroupCategory.Items.Add(dtrow["RaceCategoryGroupName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void Initialize()
        {
            lblRaceSchedule.Text = RaceReleasePointData.Rows[0]["RaceScheduleName"].ToString();
            lblRaceScheduleCategory.Text = RaceReleasePointData.Rows[0]["RaceScheduleCategoryName"].ToString();
            lblLap.Text = RaceReleasePointData.Rows[0]["Lap"].ToString();
            lblLocationName.Text = RaceReleasePointData.Rows[0]["LocationName"].ToString();
            lblCoordinates.Text = RaceReleasePointData.Rows[0]["Coordinates"].ToString();
            lblDistance.Text = RaceReleasePointData.Rows[0]["Distance"].ToString() + " KM";
            lblReleaseDate.Text = Convert.ToString(RaceReleasePointData.Rows[0]["ReleasedDate"]).Split(' ').GetValue(0).ToString();
            lblReleaseTime.Text = RaceReleasePointData.Rows[0]["ReleaseTime"].ToString();
            lblLapNo.Text = RaceReleasePointData.Rows[0]["LapNo"].ToString();
        }
        #endregion

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void lblRaceScheduleCategory_Click(object sender, EventArgs e)
        {

        }

        private void lblReleaseTime_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void lblReleaseDate_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void lblCoordinates_Click(object sender, EventArgs e)
        {

        }

        private void lblLocationName_Click(object sender, EventArgs e)
        {

        }

        private void lblLapNo_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void shapeContainer1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
