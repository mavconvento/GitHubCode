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
    public partial class frmSchedule : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceSchedule schedule;
        BIZ.Region region;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ScheduleID { get; set; }
        public String ScheduleName { get; set; }
        public String RegionName { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmSchedule()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmSchedule_Load(object sender, EventArgs e)
        {
            ClearControl();
            PopulateCombobox();
            ScheduleSelectAll();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ScheduleDelete();
        }
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void ClearControl()
        {
            try
            {
                txtScheduleID.Text = "0";
                cmbSeason.Text = "";
                IsEdit = false;
                cmbSeason.Focus();
                cmbRegion.SelectedIndex = -1;
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
                ScheduleID = Convert.ToInt64(txtScheduleID.Text);
                ScheduleName = cmbSeason.Text + " " + cmbYear.Text;
                RegionName = cmbRegion.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                Int64 colIndex;
                if (datagrid.RowCount > 0)
                {
                    schedule = new BIZ.RaceSchedule();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    if (colIndex == 5)
                    {
                        ScheduleID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                        if (ScheduleID > 0)
                        {
                            DataTable dtresult = new DataTable();
                            PopulateBussinessLayer(Common.Common.Schedule.Schedule);
                            dtresult = schedule.ScheduleSearchByKey();

                            if (dtresult.Rows.Count > 0)
                            {
                                RecordSearched = dtresult;
                                PopulateControl();
                            }
                            else
                            {
                                MessageBox.Show("No record is found", "Search");
                            }

                        }
                    }
                    else
                    {
                        if (colIndex == 4)
                        {
                            frmScheduleDetails scheduleDetails = new frmScheduleDetails();
                            scheduleDetails.ScheduleID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                            scheduleDetails.ScheduleName = datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value.ToString();
                            scheduleDetails.UserID = UserID;
                            scheduleDetails.ClubID = ClubID;
                            scheduleDetails.ShowDialog();
                            ScheduleSelectAll();
                        }
                        else
                        {
                            if (colIndex == 5)
                            {
                                frmScheduleCategory scheduleCategory = new frmScheduleCategory();
                                scheduleCategory.ScheduleID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                                scheduleCategory.ScheduleName = datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value.ToString();
                                scheduleCategory.UserID = UserID;
                                scheduleCategory.ClubID = ClubID;
                                scheduleCategory.ShowDialog();
                                ScheduleSelectAll();
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
        private void ScheduleDelete()
        {
            try
            {
                schedule = new BIZ.RaceSchedule();
                GetControlValue();
                if (ScheduleID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer(Common.Common.Schedule.Schedule);
                        schedule.ScheduleDelete();
                        ClearControl();
                        ScheduleSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ScheduleSelectAll()
        {
            try
            {
                schedule = new BIZ.RaceSchedule();
                PopulateBussinessLayer(Common.Common.Schedule.Schedule);
                schedule.ScheduleSelectAll(this.dataGridView1);
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(Font, FontStyle.Bold);
                dataGridView1.Columns[3].DefaultCellStyle = style;
                dataGridView1.Columns[4].DefaultCellStyle = style;
                //dataGridView1.Columns[5].DefaultCellStyle = style;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateControl()
        {
            try
            {
                if (RecordSearched.Rows.Count > 0)
                {
                    IsEdit = true;
                    string[] scheduleNameCollection;
                    string scheduleName= "";
                    txtScheduleID.Text = RecordSearched.Rows[0]["ScheduleID"].ToString();
                    scheduleNameCollection = RecordSearched.Rows[0]["ScheduleName"].ToString().Split(' ');
                    for (int i = 0; i < scheduleNameCollection.Length - 1; i++)
                    {
                        scheduleName = scheduleName.Trim() + " " + scheduleNameCollection[i];
                    }
                    cmbSeason.Text = scheduleName;
                    cmbYear.Text = scheduleNameCollection[scheduleNameCollection.Length - 1];
                    cmbRegion.SelectedItem = RecordSearched.Rows[0]["RegionName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBussinessLayer(Common.Common.Schedule type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.Schedule.Schedule:
                        schedule.ClubID = ClubID;
                        schedule.UserID = UserID;
                        schedule.ScheduleID = ScheduleID;
                        schedule.ScheduleName = ScheduleName;
                        schedule.RegionName = RegionName;
                        break;
                    case Common.Common.Schedule.Region:
                        region.ClubID = ClubID;
                        region.UserID = UserID;
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void Save()
        {
            try
            {
                schedule = new BIZ.RaceSchedule();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.Schedule.Schedule);
                if (ScheduleName.Split(' ').Length > 1)
                {
                    if (schedule.Save())
                    {
                        ClearControl();
                        ScheduleSelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Schedule Name Value, Please indicate Race Season and Year", "Error");
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

                //Region
                region = new BIZ.Region();
                PopulateBussinessLayer(Common.Common.Schedule.Region);

                //Race Region
                DataTable dtregion;
                dtregion = region.RegionGetByKey().Tables[0];
                if (dtregion.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtregion.Rows)
                    {
                        cmbRegion.Items.Add(dtrow["RegionName"].ToString());
                    }
                }

                //Race Season
                DataTable dtSeason;
                dtSeason = region.GetRaceSeason().Tables[0];
                if (dtSeason.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtSeason.Rows)
                    {
                        cmbSeason.Items.Add(dtrow["Description"].ToString());
                    }
                }

                this.cmbYear.Items.Clear();
                for (int i = DateTime.Now.Year ; i < DateTime.Now.Year + 5; i++)
                {
                    this.cmbYear.Items.Add(i);
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
