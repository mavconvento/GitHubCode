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
    public partial class frmRaceResults : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceCategory raceCategory;
        BIZ.RaceScheduleCategory raceScheduleCategory;
        BIZ.RaceResult raceResults;
        BIZ.RaceSchedule raceSchedule;
        //BIZ.ReportGeneration reportGeneration;
        #endregion

        #region Properties
        public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public DateTime DateReleased { get; set; }
        public String RaceScheduleName { get; set; }
        #endregion

        #region "Events"
        public frmRaceResults()
        {
            InitializeComponent();
        }
        private void frmRaceResults_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            ClearControl();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dataGridView1.DataSource;
                reportGeneration.Type = "ResultSummary";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCategory.Text != "" && cmbCategoryGroup.Text != "")
            {
            ViewResult();
            }
            else
            {
                MessageBox.Show("Please set your Category and Group", "Error");
            }
        }
        private void cmbRaceSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRaceScheduleCategoryItems();
        }
        #endregion

        #region "Private Methods"
        private void PopulateBusinessLayer(Common.Common.RaceResult Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceResult.RaceCategory:
                        //raceCategory.ID = ID;
                        raceCategory.UserID = UserID;
                        raceCategory.ClubID = ClubID;
                        break;
                    case Common.Common.RaceResult.RaceResult:
                        raceResults.UserID = UserID;
                        raceResults.ClubID = ClubID;
                        raceResults.ReleasedDate = DateReleased;
                        raceResults.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        raceResults.RaceCategoryGroupName = RaceCategoryGroupName;
                        raceResults.RaceCategoryName = RaceCategoryName;
                        break;
                    case Common.Common.RaceResult.RaceScheduleCategory:
                        raceScheduleCategory.UserID = UserID;
                        raceScheduleCategory.ClubID = ClubID;
                        raceScheduleCategory.RaceScheduleName = RaceScheduleName;
                        break;
                    case Common.Common.RaceResult.RaceSchedule:
                        raceSchedule.UserID = UserID;
                        raceSchedule.ClubID = ClubID;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void SetRaceScheduleCategoryItems()
        {
            try
            {
                raceScheduleCategory = new BIZ.RaceScheduleCategory();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceScheduleCategory);

                //Race Schedule
                DataTable dtRaceScheduleCategory;
                dtRaceScheduleCategory = raceScheduleCategory.RaceScheduleCategoryGetByRaceSchedule();
                if (dtRaceScheduleCategory.Rows.Count > 0)
                {
                    cmbRaceScheduleCategory.Items.Clear();      //CLEAR ITEMS
                    foreach (DataRow dtrow in dtRaceScheduleCategory.Rows)
                    {
                        cmbRaceScheduleCategory.Items.Add(dtrow["Category Name"].ToString());
                    }
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
                PopulateBusinessLayer(Common.Common.RaceResult.RaceCategory);

                //Race Schedule
                DataTable dtRaceCategory;
                DataTable dtRaceCategoryGroup;

                dtRaceCategory = raceCategory.RaceCategoryGetByKey().Tables[0];
                dtRaceCategoryGroup = raceCategory.RaceCategoryGetByKey().Tables[1];

                if (dtRaceCategory.Rows.Count > 0)
                {
                    cmbCategory.Items.Add("All");
                    cmbCategoryGroup.Items.Add("All");
                    foreach (DataRow dtrow in dtRaceCategory.Rows)
                    {
                        cmbCategory.Items.Add(dtrow["Description"].ToString());
                    }
                }
                if (dtRaceCategoryGroup.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceCategoryGroup.Rows)
                    {
                        cmbCategoryGroup.Items.Add(dtrow["RaceCategoryGroupName"].ToString());
                    }
                }

                raceSchedule = new BIZ.RaceSchedule();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceSchedule);

                DataTable dtRaceSchedule;
                dtRaceSchedule = raceSchedule.ScheduleSelectAll();
                if (dtRaceSchedule.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceSchedule.Rows)
                    {
                        cmbRaceSchedule.Items.Add(dtrow["Schedule Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetControlValue()
        {
            RaceScheduleCategoryName = cmbRaceScheduleCategory.Text;
            RaceCategoryGroupName = cmbCategoryGroup.Text;
            RaceCategoryName = cmbCategory.Text;
            RaceScheduleName = cmbRaceSchedule.Text;
        }
        private void ClearControl()
        {
        }
        private void ViewResult()
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceResults = new BIZ.RaceResult();
                ClearControl();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceResult);
                dtResult=raceResults.RaceResultGetByScheduleCategory();
                if (dtResult.Tables.Count > 0)
                {
                    this.dataGridView1.DataSource = dtResult.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
            MessageBox.Show(ex.Message,"Error");
            }
        }
        #endregion

       

    }
}
