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
    public partial class frmMemberPerReleasePoint : Form
    {

        //Variables
        #region Variables
        //BIZ.RaceScheduleDetails raceScheduleDetails;
        BIZ.Location location;
        BIZ.Member member;
        #endregion


        //properties
        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public DataTable RecordSearched { get; set; }
        #endregion

        //Events
        #region Events
        public frmMemberPerReleasePoint()
        {
            InitializeComponent();
        }
        private void frmMemberPerReleasePoint_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            ClearControl();
        }
        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocationDetails();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GetMemberDistancePerReleasePoint();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtMemberList.DataSource;
                reportGeneration.Type = "Entry";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region Public Method
        private void GetLocationDetails()
        {
            try
            {
                DataTable dtResult;
                location = new BIZ.Location();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.Location);
                dtResult = location.LocationSearchByKey();
                if (dtResult.Rows.Count > 0)
                {
                    RecordSearched = dtResult;
                    PopulateControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetControlValue()
        {
            try
            {
                LocationID = Convert.ToInt64(txtLocationID.Text);
                LocationName = cmbLocation.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ClearControl()
        {
            try
            {
                txtLocationID.Text = "0";
                cmbLocation.Text = "";
                cmbLocation.SelectedItem = -1;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void GetMemberDistancePerReleasePoint()
        {
            try
            {
                DataTable dtResult;
                member = new BIZ.Member();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.Member);
                dtResult = member.GetMembetDistancePerReleasePoint();
                this.dtMemberList.DataSource = dtResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void PopulateControl()
        {
            try
            {
                if (RecordSearched.Rows.Count > 0)
                {
                    //IsEdit = true;
                    txtLocationID.Text = RecordSearched.Rows[0]["LocationID"].ToString();
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateCombobox()
        {
            try
            {
                DataTable dtLocation;
                location = new BIZ.Location();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.Location);
                dtLocation = location.LocationSelectAll();

                foreach (DataRow dr in dtLocation.Rows)
                {
                    this.cmbLocation.Items.Add(dr["LocationName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateBussinessLayer(Common.Common.RaceScheduleDetails type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.RaceScheduleDetails.Location:
                        location.ClubID = ClubID;
                        location.LocationID = LocationID;
                        location.LocationName = LocationName;
                        break;
                    case Common.Common.RaceScheduleDetails.Member:
                        member.ClubID = ClubID;
                        member.LocationID = LocationID;
                        member.UserID = UserID;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion
 
    }
}
