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
    public partial class frmMemberDistance : Form
    {
        #region Variable
        BIZ.Member member;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public String MemberIDNo { get; set; }
        public Int64 UserID { get; set; }
        public DataSet MemberDetailsData { get; set; }
        #endregion

        #region Events
        public frmMemberDistance()
        {
            InitializeComponent();
        }
        private void frmMemberDistance_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            GetMemberDetails();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmReportGeneration reportGeneration = new frmReportGeneration();
            DataSet dt = new DataSet();
            dt = MemberDetailsData;
            reportGeneration.Type = "MemberDistance";
            reportGeneration.dtMemberDistance = dt;
            reportGeneration.dtRecord = dt.Tables[0];
            reportGeneration.ShowDialog();
        }
        #endregion

        #region Public Method
        
        #endregion

        #region Private Method
        private void PopulateCombobox()
        {
            try
            {
                //Extension Name
                this.cmbExtName.Items.Add("SR.");
                this.cmbExtName.Items.Add("JR.");
                this.cmbExtName.Items.Add("I");
                this.cmbExtName.Items.Add("II");
                this.cmbExtName.Items.Add("III");
                this.cmbExtName.Items.Add("IV");
                this.cmbExtName.Items.Add("V");

                //Direction Sign
                this.cmbLatSign.Items.Add("N");
                this.cmbLatSign.Items.Add("S");
                this.cmbLongSign.Items.Add("E");
                this.cmbLongSign.Items.Add("W");

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetMemberDetails()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.Member);
                MemberDetailsData = member.GetMemberDistance();
                PopulateControlValue(MemberDetailsData.Tables[0], MemberDetailsData.Tables[1], MemberDetailsData.Tables[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        public void PopulateControlValue(DataTable RecordSearched,DataTable LeftTable,DataTable RightTable)
        {
            try
            {
                if (RecordSearched.Rows.Count > 0)
                {
                    //txtMemberIDNo.Text = RecordSearched.Rows[0]["MemberIDNo"].ToString();
                    txtLastName.Text = RecordSearched.Rows[0]["LastName"].ToString();
                    txtFirstName.Text = RecordSearched.Rows[0]["FirstName"].ToString();
                    txtMiddleName.Text = RecordSearched.Rows[0]["MiddleName"].ToString();
                    cmbExtName.Text = RecordSearched.Rows[0]["ExtensionName"].ToString();
                    txtLoftName.Text = RecordSearched.Rows[0]["LoftName"].ToString();
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["LatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["LatMinute"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["LatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["LatSection"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["LongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["LongMinute"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["LongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["LongSection"].ToString();
                    dtRightTable.DataSource = RightTable;
                    dtLeftTable.DataSource = LeftTable;
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
        private void GetControlValue()
        {
            try
            {
                MemberIDNo = txtMemberIDNo.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBusinessLayer(Common.Common.RaceEntryClassType Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceEntryClassType.Member:
                        member.ClubID = ClubID;
                        member.MemberIDNo = MemberIDNo;
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
