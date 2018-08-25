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
    public partial class frmRaceEntry : Form
    {
        #region Constants
        
        #endregion

        #region Variable
        BIZ.RaceSchedule raceSchedule;
        BIZ.RaceScheduleCategory raceScheduleCategory;
        BIZ.RaceReleasePoint raceReleasePoint;
        #endregion

        #region Properties
        public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String RaceScheduleName { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        #endregion

        #region Events
        public frmRaceEntry()
        {
            InitializeComponent();
            dtReleasePoint.DoubleClick += new EventHandler(dtReleasePoint_DoubleClick);
        }
        private void frmRaceEntry_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            GetReleasePoint();
        }
        private void cmbRaceSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetRaceScheduleCategoryItems();
            GetReleasePoint();
        }
        private void cmbRaceScheduleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReleasePoint();
        }
        #endregion

        #region Private Methods
        private void dtReleasePoint_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtReleasePoint;
                Int64 index;

                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "SELECT")
                    {
                        ID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                        raceReleasePoint = new BIZ.RaceReleasePoint();

                        if (ID > 0)
                        {
                            RaceReleasePointID = ID;
                            DataTable dtresult = new DataTable();
                            PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
                            dtresult = raceReleasePoint.RaceReleasePointGetbyKey();

                            if (dtresult.Rows.Count > 0)
                            {
                                frmEntryBird entryBird = new frmEntryBird();
                                entryBird.RaceReleasePointData = dtresult;
                                entryBird.ClubID = ClubID;
                                entryBird.UserID = UserID;
                                entryBird.PopulateControlValue("RaceReleasePoint");
                                entryBird.ShowDialog();
                                //MemberDetailsSelectAll(); //Refresh value of data grid
                            }
                            else
                            {
                                MessageBox.Show("No record is found", "Search");
                            }
                        }
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
            RaceScheduleName = cmbRaceSchedule.Text;
            RaceScheduleCategoryName = cmbRaceScheduleCategory.Text;
        }
        private void PopulateBusinessLayer(Common.Common.RaceEntryClassType Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceEntryClassType.RaceSchedule :
                        raceSchedule.ScheduleID = ID;
                        raceSchedule.ClubID = ClubID;
                        raceSchedule.UserID = UserID;
                        break;
                    case Common.Common.RaceEntryClassType.RaceScheduleCategory :
                        raceScheduleCategory.RaceScheduleCategoryID = ID;
                        raceScheduleCategory.ClubID = ClubID;
                        raceScheduleCategory.UserID = UserID;
                        raceScheduleCategory.RaceScheduleName = RaceScheduleName;
                        break;
                    case Common.Common.RaceEntryClassType.RaceReleasePoint:
                        raceReleasePoint.RaceReleasePointID = ID;
                        raceReleasePoint.ClubID = ClubID;
                        raceReleasePoint.UserID = UserID;
                        raceReleasePoint.RaceScheduleCategoryName = RaceScheduleName; // RaceScheduleCategoryName;
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
                raceSchedule = new BIZ.RaceSchedule();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceSchedule);

                //Race Schedule
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
        private void SetRaceScheduleCategoryItems()
        {
            try
            {
                raceScheduleCategory = new BIZ.RaceScheduleCategory();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceScheduleCategory);

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
        private void GetReleasePoint()
        {
            try
            {
                raceReleasePoint = new BIZ.RaceReleasePoint();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);

                //Race Schedule
                DataTable dtRaceScheduleCategory;
                dtRaceScheduleCategory = raceReleasePoint.RaceReleasePointGetbyRaceScheduleCategory();
                dtReleasePoint.DataSource = dtRaceScheduleCategory;

                if (dtRaceScheduleCategory.Rows.Count > 0)
                {
                    dtReleasePoint.Columns[0].Visible = false;
                    dtReleasePoint.Columns[1].Visible = false;
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
