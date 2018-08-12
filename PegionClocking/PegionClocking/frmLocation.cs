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
    public partial class frmLocation : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.Location location;
        BIZ.Region region;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public String RegionName { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmLocation()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmLocation_Load(object sender, EventArgs e)
        {
            ClearControl();
            PopulateCombobox();
            LocationSelectAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LocationDelete();
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
                txtLocationID.Text = "0";
                txtLocationName.Text = "";
                txtDistanceLatDegree.Text = "0";
                txtDistanceLatMinutes.Text = "0";
                txtDistanceLatSeconds.Text = "0";
                cmbLatSign.Text = "";
                txtDistanceLongDegree.Text = "0";
                txtDistanceLongMinutes.Text = "0";
                txtDistanceLongSeconds.Text = "0";
                cmbLongSign.Text = "";
                cmbRegion.SelectedIndex = -1;
                IsEdit = false;
                txtLocationName.Focus();
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
                LocationName = txtLocationName.Text;
                RegionName = cmbRegion.Text;
                DistanceLatDegree = Convert.ToInt64(txtDistanceLatDegree.Text);
                DistanceLatMinutes = Convert.ToInt64(txtDistanceLatMinutes.Text);
                DistanceLatSecond = Convert.ToDouble(txtDistanceLatSeconds.Text);
                DistanceLatSign = Convert.ToString(cmbLatSign.Text);
                DistanceLongDegree = Convert.ToInt64(txtDistanceLongDegree.Text);
                DistanceLongMinutes = Convert.ToInt64(txtDistanceLongMinutes.Text);
                DistanceLongSecond = Convert.ToDouble(txtDistanceLongSeconds.Text);
                DistanceLongSign = Convert.ToString(cmbLongSign.Text);

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
                    location = new BIZ.Location();
                    index = datagrid.CurrentRow.Index;
                    LocationID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (LocationID > 0)
                    {
                        DataTable dtresult = new DataTable();
                        PopulateBussinessLayer(Common.Common.Location.Location);
                        dtresult = location.LocationSearchByKey();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void LocationDelete()
        {
            try
            {
                location = new BIZ.Location();
                GetControlValue();
                if (LocationID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer(Common.Common.Location.Location);
                        location.LocationDelete();
                        ClearControl();
                        LocationSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void LocationSelectAll()
        {
            try
            {
                location = new BIZ.Location();
                PopulateBussinessLayer(Common.Common.Location.Location);
                location.LocationSelectAll(this.dataGridView1);
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
                    txtLocationID.Text = RecordSearched.Rows[0]["LocationID"].ToString();
                    txtLocationName.Text = RecordSearched.Rows[0]["LocationName"].ToString();
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                    cmbRegion.SelectedItem = RecordSearched.Rows[0]["RegionName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBussinessLayer(Common.Common.Location type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.Location.Location:
                        location.ClubID = ClubID;
                        location.UserID = UserID;
                        location.LocationID = LocationID;
                        location.LocationName = LocationName;
                        location.RegionName = RegionName;
                        location.DistanceLatDegree = DistanceLatDegree;
                        location.DistanceLatMinutes = DistanceLatMinutes;
                        location.DistanceLatSecond = DistanceLatSecond;
                        location.DistanceLatSign = DistanceLatSign;
                        location.DistanceLongDegree = DistanceLongDegree;
                        location.DistanceLongMinutes = DistanceLongMinutes;
                        location.DistanceLongSecond = DistanceLongSecond;
                        location.DistanceLongSign = DistanceLongSign;
                        break;
                    case Common.Common.Location.Region:
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
                location = new BIZ.Location();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.Location.Location);
                if (location.Save())
                {
                    ClearControl();
                    this.txtLocationName.Focus();
                    LocationSelectAll();
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
                this.cmbLatSign.Items.Add("N");
                this.cmbLatSign.Items.Add("S");
                this.cmbLongSign.Items.Add("E");
                this.cmbLongSign.Items.Add("W");

                //Region
                region = new BIZ.Region();
                PopulateBussinessLayer(Common.Common.Location.Region);

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

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

    }
}
