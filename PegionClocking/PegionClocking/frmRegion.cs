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
    public partial class frmRegion : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.Region region;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 RegionID { get; set; }
        public String RegionName { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmRegion()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmRegion_Load(object sender, EventArgs e)
        {
            ClearControl();
            //PopulateCombobox();
            RegionSelectAll();
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
            RegionDelete();
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
                txtRegionID.Text = "0";
                txtRegionName.Text = "";
                IsEdit = false;
                txtRegionName.Focus();
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
                RegionID = Convert.ToInt64(txtRegionID.Text);
                RegionName = txtRegionName.Text;
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

                    RegionID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    RegionName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    PopulateControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RegionDelete()
        {
            try
            {
                region = new BIZ.Region();
                GetControlValue();
                if (RegionID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        region.RegionDelete();
                        ClearControl();
                        RegionSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RegionSelectAll()
        {
            try
            {
                region = new BIZ.Region();
                PopulateBussinessLayer();
                region.RegionSelectAll(this.dataGridView1);
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
                txtRegionID.Text = RegionID.ToString();
                txtRegionName.Text = RegionName.ToString();

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
                region.ClubID = ClubID;
                region.UserID = UserID;
                region.RegionID = RegionID;
                region.RegionName = RegionName;
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
                region = new BIZ.Region();
                GetControlValue();
                PopulateBussinessLayer();
                if (region.Save())
                {
                    ClearControl();
                    //this.txtLocationName.Focus(); 
                    RegionSelectAll();
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
