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
    public partial class frmRaceCategory : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceCategory RaceCategory;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 RaceCategoryID { get; set; }
        public String RaceCategoryName { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmRaceCategory()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmRaceCategory_Load(object sender, EventArgs e)
        {
            ClearControl();
            //PopulateCombobox();
            RaceCategorySelectAll();
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
            RaceCategoryDelete();
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
                txtRaceCategoryID.Text = "0";
                txtRaceCategoryName.Text = "";
                IsEdit = false;
                txtRaceCategoryName.Focus();
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
                RaceCategoryID = Convert.ToInt64(txtRaceCategoryID.Text);
                RaceCategoryName = txtRaceCategoryName.Text;
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
                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;

                    RaceCategoryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    RaceCategoryName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    PopulateControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RaceCategoryDelete()
        {
            try
            {
                RaceCategory = new BIZ.RaceCategory();
                GetControlValue();
                if (RaceCategoryID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        RaceCategory.RaceCategoryDelete();
                        ClearControl();
                        RaceCategorySelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RaceCategorySelectAll()
        {
            try
            {
                RaceCategory = new BIZ.RaceCategory();
                PopulateBussinessLayer();
                RaceCategory.RaceCategorySelectAll(this.dataGridView1);
                if (this.dataGridView1.ColumnCount > 0)
                {
                    this.dataGridView1.Columns[0].Visible = false;
                }
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

                IsEdit = true;
                txtRaceCategoryID.Text = RaceCategoryID.ToString();
                txtRaceCategoryName.Text = RaceCategoryName.ToString();

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
                RaceCategory.ClubID = ClubID;
                RaceCategory.UserID = UserID;
                RaceCategory.RaceCategoryID = RaceCategoryID;
                RaceCategory.RaceCategoryName = RaceCategoryName;
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
                RaceCategory = new BIZ.RaceCategory();
                GetControlValue();
                PopulateBussinessLayer();
                if (RaceCategory.Save())
                {
                    ClearControl();
                    //this.txtLocationName.Focus(); 
                    RaceCategorySelectAll();
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

                //Direction Sign
                //this.cmbLatSign.Items.Add("N");
                //this.cmbLatSign.Items.Add("S");
                //this.cmbLongSign.Items.Add("E");
                //this.cmbLongSign.Items.Add("W");

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

    }
}
