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
    public partial class frmScheduleCategory : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceScheduleCategory scheduleCategory;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ScheduleID { get; set; }
        public String ScheduleName { get; set; }
        public Int64 ScheduleCategoryID { get; set; }
        public String ScheduleCategoryName { get; set; }
        public Int64 Lap { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmScheduleCategory()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmScheduleCategory_Load_1(object sender, EventArgs e)
        {
            ClearControl();
            InitialiseControl();
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
            ScheduleCategoryDelete();
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void InitialiseControl()
        {
            try
            {
                this.txtScheduleID.Text = ScheduleID.ToString();
                this.txtScheduleName.Text = ScheduleName.ToString();
                ScheduleCategorySelectAll();
                this.dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ClearControl()
        {
            try
            {
                txtScheduleCategoryID.Text = "0";
                txtScheduleCategoryName.Text = "";
                txtLap.Text = "1";
                IsEdit = false;
                txtScheduleCategoryName.Focus();
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
                ScheduleCategoryID = Convert.ToInt64(txtScheduleCategoryID.Text);
                ScheduleCategoryName = txtScheduleCategoryName.Text;
                Lap = Convert.ToInt64(txtLap.Text);
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
                    //scheduleCategory = new BIZ.RaceScheduleCategory();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    if ((String)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    {
                        ScheduleCategoryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        ScheduleCategoryName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                        Lap = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                        PopulateControl();
                    }
                    else
                    {
                        if ((String)datagrid.CurrentCell.Value.ToString() == "VIEW" || (String)datagrid.CurrentCell.Value.ToString() == "CREATE")
                        {
                            frmReleasePoint raceReleasePoint = new frmReleasePoint();
                            raceReleasePoint.RaceScheduleCategoryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                            raceReleasePoint.RaceScheduleCategoryName = datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value.ToString();
                            raceReleasePoint.UserID = UserID;
                            raceReleasePoint.ClubID = ClubID;
                            raceReleasePoint.ShowDialog();
                            ScheduleCategorySelectAll();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ScheduleCategoryDelete()
        {
            try
            {
                scheduleCategory = new BIZ.RaceScheduleCategory();
                GetControlValue();
                if (ScheduleCategoryID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        scheduleCategory.ScheduleCategoryDelete();
                        ClearControl();
                        ScheduleCategorySelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ScheduleCategorySelectAll()
        {
            try
            {
                scheduleCategory = new BIZ.RaceScheduleCategory();
                PopulateBussinessLayer();
                scheduleCategory.ScheduleCategorySelectAll(this.dataGridView1);
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(Font, FontStyle.Bold);
                dataGridView1.Columns[3].DefaultCellStyle = style;
                dataGridView1.Columns[4].DefaultCellStyle = style;
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
                txtScheduleCategoryID.Text = ScheduleCategoryID.ToString();
                txtScheduleCategoryName.Text = ScheduleCategoryName;
                txtLap.Text = Lap.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBussinessLayer()
        {
            try
            {
                scheduleCategory.ClubID = ClubID;
                scheduleCategory.UserID = UserID;
                scheduleCategory.RaceScheduleID = ScheduleID;
                scheduleCategory.RaceScheduleCategoryID = ScheduleCategoryID;
                scheduleCategory.RaceScheduleCategoryName = ScheduleCategoryName;
                scheduleCategory.Lap = Lap;
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
                scheduleCategory = new BIZ.RaceScheduleCategory();
                GetControlValue();
                PopulateBussinessLayer();
                if (scheduleCategory.Save())
                {
                    ClearControl();
                    this.txtScheduleCategoryName.Focus();
                    ScheduleCategorySelectAll();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion
    }
}
