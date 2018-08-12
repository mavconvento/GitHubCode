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
    public partial class frmCalculator : Form
    {
        BIZ.Calculator calculator;

        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public DataTable RaceReleasePoint { get; set; }
        public String ScheduleDetailsID { get; set; }
        public string ReleasePointID { get; set; }
        public String MemberID { get; set; }

        #region Events
        public frmCalculator()
        {
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            try
            {
                InializeControl();
                GetRaceSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void cmbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRaceReleasePointBySchedule();
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow item in RaceReleasePoint.Rows)
            {
                if (item["ReleasePoint"].ToString() == cmbLocation.Text)
                {
                    ScheduleDetailsID = item["RaceScheduleDetailsID"].ToString();
                    ReleasePointID = item["RaceReleasePointID"].ToString();
                    txtDistanceLatDegree.Text = item["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = item["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = item["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = item["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = item["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = item["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = item["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = item["DistanceLongSign"].ToString();
                    dtReleaseDate.Value = Convert.ToDateTime(item["DateRelease"]);
                    dtArrivalDate.Value = Convert.ToDateTime(item["DateRelease"]);
                    txtReleaseTime.Text = item["ReleaseTime"].ToString();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string Error = "";
                if (ReleasePointID == "")
                {
                    Error = "Please Select Release Point.";
                }
                else if (MemberID == "")
                {
                    Error = "Please Select Member";
                }
                else if (txtArrivalTime.Text == "")
                {
                    Error = "Please indicate arrival time.";
                }
                else
                {
                    DataSet dtResult = new DataSet();
                    calculator = new BIZ.Calculator();
                    PopulateBusinessLayer();
                    dtResult = calculator.Calculate();

                    if (dtResult.Tables.Count > 0)
                    {
                        if (dtResult.Tables[0].Rows.Count > 0)
                        {
                            txtSpeed.Text = dtResult.Tables[0].Rows[0]["speed"].ToString();
                            txtFlight.Text = dtResult.Tables[0].Rows[0]["flight"].ToString();
                            txtDistance.Text = dtResult.Tables[0].Rows[0]["distance"].ToString();
                        }
                    }
                }

                if (Error != "") MessageBox.Show(Error, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        #endregion

        private void GetRaceSchedule()
        {
            try
            {
                DataTable dt = new DataTable();
                BIZ.RaceSchedule raceSchedule = new BIZ.RaceSchedule();
                raceSchedule.ClubID = ClubID;
                dt = raceSchedule.ScheduleSelectAll();

                foreach (DataRow item in dt.Rows)
                {
                    cmbSchedule.Items.Add(item["Schedule Name"]);
                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        private void InializeControl()
        {
            try
            {
                MemberID = "";
                ScheduleDetailsID = "";
                ReleasePointID = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetRaceReleasePointBySchedule()
        {
            BIZ.RaceReleasePoint raceReleasePoint = new BIZ.RaceReleasePoint();
            raceReleasePoint.ClubID = ClubID;
            raceReleasePoint.RaceScheduleName = cmbSchedule.Text;
            RaceReleasePoint = raceReleasePoint.GetRaceReleasePointBySchedule();

            if (RaceReleasePoint.Rows.Count > 0)
            {
                foreach (DataRow dr in RaceReleasePoint.Rows)
                {
                    this.cmbLocation.Items.Add(dr["ReleasePoint"]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cmbLocation.SelectedIndex = -1;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            GetMemberDetails();
        }

        private void GetMemberDetails()
        {
            try
            {
               DataSet dt = new DataSet();
               BIZ.Member member = new BIZ.Member();
               member.ClubID = ClubID;
               member.MemberIDNo = txtMemberIDNo.Text;
               dt = member.GetMemberDistance();
               PopulateControlValue(dt.Tables[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        public void PopulateControlValue(DataTable RecordSearched)
        {
            try
            {
                if (RecordSearched.Rows.Count > 0)
                {
                    MemberID = RecordSearched.Rows[0]["MemberID"].ToString();
                    txtName.Text = RecordSearched.Rows[0]["Name"].ToString();
                    txtLoftName.Text = RecordSearched.Rows[0]["LoftName"].ToString();
                    txtMemberLatDegree.Text = RecordSearched.Rows[0]["LatDegree"].ToString();
                    txtMemberLatMinute.Text = RecordSearched.Rows[0]["LatMinute"].ToString();
                    txtMemberLatSecond.Text = RecordSearched.Rows[0]["LatSecond"].ToString();
                    cmbMemberLatSign.Text = RecordSearched.Rows[0]["LatSection"].ToString();
                    txtMemberLongDegree.Text = RecordSearched.Rows[0]["LongDegree"].ToString();
                    txtMemberLongMinute.Text = RecordSearched.Rows[0]["LongMinute"].ToString();
                    txtMemberLongSecond.Text = RecordSearched.Rows[0]["LongSecond"].ToString();
                    cmbMemberLongSign.Text = RecordSearched.Rows[0]["LongSection"].ToString();
                }
                else
                {
                    MessageBox.Show("No Record Found", "Search");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulateBusinessLayer()
        {
            calculator.ClubID =Convert.ToString(ClubID);
            calculator.RaceScheduleDetailsID = ScheduleDetailsID;
            calculator.ReleasePointID = ReleasePointID;
            calculator.MemberID = MemberID;
            calculator.ArrivalDate = dtArrivalDate.Value.Date;
            calculator.ArrivalTime = txtArrivalTime.Text;
        }

    }
}
