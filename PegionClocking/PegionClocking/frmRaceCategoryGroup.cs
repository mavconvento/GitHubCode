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
    public partial class frmRaceCategoryGroup : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceCategoryGroup RaceCategoryGroup;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 RaceCategoryGroupID { get; set; }
        public String RaceCategoryGroupName { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmRaceCategoryGroup()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmRaceCategoryGroup_Load(object sender, EventArgs e)
        {
            ClearControl();
            //PopulateCombobox();
            RaceCategoryGroupSelectAll();
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
            RaceCategoryGroupDelete();
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
                txtRaceCategoryGroupID.Text = "0";
                txtRaceCategoryGroupName.Text = "";
                IsEdit = false;
                txtRaceCategoryGroupName.Focus();
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
                RaceCategoryGroupID = Convert.ToInt64(txtRaceCategoryGroupID.Text);
                RaceCategoryGroupName = txtRaceCategoryGroupName.Text;
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

                    RaceCategoryGroupID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    RaceCategoryGroupName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    PopulateControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RaceCategoryGroupDelete()
        {
            try
            {
                RaceCategoryGroup = new BIZ.RaceCategoryGroup();
                GetControlValue();
                if (RaceCategoryGroupID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        RaceCategoryGroup.RaceCategoryGroupDelete();
                        ClearControl();
                        RaceCategoryGroupSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void RaceCategoryGroupSelectAll()
        {
            try
            {
                RaceCategoryGroup = new BIZ.RaceCategoryGroup();
                PopulateBussinessLayer();
                RaceCategoryGroup.RaceCategoryGroupSelectAll(this.dataGridView1);
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
                txtRaceCategoryGroupID.Text = RaceCategoryGroupID.ToString();
                txtRaceCategoryGroupName.Text = RaceCategoryGroupName.ToString();

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
                RaceCategoryGroup.ClubID = ClubID;
                RaceCategoryGroup.UserID = UserID;
                RaceCategoryGroup.RaceCategoryGroupID = RaceCategoryGroupID;
                RaceCategoryGroup.RaceCategoryGroupName = RaceCategoryGroupName;
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
                RaceCategoryGroup = new BIZ.RaceCategoryGroup();
                GetControlValue();
                PopulateBussinessLayer();
                if (RaceCategoryGroup.Save())
                {
                    ClearControl();
                    //this.txtLocationName.Focus(); 
                    RaceCategoryGroupSelectAll();
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
