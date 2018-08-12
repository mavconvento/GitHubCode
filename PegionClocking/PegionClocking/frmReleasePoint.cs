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
    public partial class frmReleasePoint : Form
    {
        
        #region Constant
        #endregion

        #region Variable
        BIZ.RaceReleasePoint raceReleasePoint;
        BIZ.Location location;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public Int64 RaceScheduleCategoryID { get; set; }
        public Int64 RaceScheduleDetailsID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public string ReleaseTime { get; set; }
        public string ReleaseDate { get; set; }
        public Int64 LapNo { get; set; }
        public double Multiplier { get; set; }
        public string LocationName { get; set; }
        public string MinSpeed { get; set; }
        public bool IsStop { get; set; }
        public DateTime StopFromDate { get; set; }
        public string StopFromTime { get; set; }
        public DateTime StopToDate { get; set; }
        public string StopToTime { get; set; }
        public String Description { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmReleasePoint()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmReleasePoint_Load(object sender, EventArgs e)
        {
            ClearControl();
            PopulateCombobox();
            InitialiseControl();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ReleasePointDelete();
        }
        private void cmbLocationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocationCategory();
        }
        private void chkIsStop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsStop.Checked)
            {
                this.groupBox2.Enabled = true;
            }
            else
            {
                this.groupBox2.Enabled = false;
            }
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
               
                cmbLocationName.SelectedItem = -1;
                cmbLocationName.Text = "";
                txtDistanceLatDegree.Text = "";
                txtDistanceLatMinutes.Text = "";
                txtDistanceLatSeconds.Text = "";
                cmbLatSign.Text = "";
                txtDistanceLongDegree.Text = "";
                txtDistanceLongMinutes.Text = "";
                txtDistanceLongSeconds.Text = "";
                cmbLongSign.Text = "";
                dtpDateRelease.Value = DateTime.Now;
                cmbHour.Text = "";
                cmbMinute.Text = "";// .Text = "";
                txtMultiplier.Text = "1.00";
                txtLapNo.Text = "1";
                cmbMinSpeed.Text = MinSpeed;
                chkIsStop.Checked = false;
                dtpStopFromDate.Value = DateTime.Now;
                txtStopFromTime.Text = "00:00";
                dtpStopToDate.Value = DateTime.Now;
                txtStopToTime.Text = "00:00";
                IsEdit = false;
                ReleaseDate = "";
                txtReleasePointID.Text = "0";
                txtScheduleDetailsID.Text = "0";
                txtRemarks.Text = "";
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
                string[] loc = cmbLocationName.Text.Split('|');
                
                LocationName = "";
                ReleaseDate = "";

                if (loc.Length > 0 && loc.Length == 2)
                {
                LocationName = loc.GetValue(1).ToString().Trim();
                ReleaseDate = loc.GetValue(0).ToString().Trim();
                }
                
                RaceReleasePointID = Convert.ToInt64(txtReleasePointID.Text);
                RaceScheduleDetailsID = Convert.ToInt64(txtScheduleDetailsID.Text);
                RaceScheduleCategoryName = txtScheduleCategoryName.Text;
                RaceScheduleCategoryID = Convert.ToInt64(txtScheduleCategoryID.Text);
                ReleaseTime = cmbHour.Text + ':' + cmbMinute.Text; //txtTimeRelease.Text;
                LapNo =Convert.ToInt64(txtLapNo.Text);
                Multiplier =Convert.ToDouble(txtMultiplier.Text);
                MinSpeed = cmbMinSpeed.Text;
                IsStop = chkIsStop.Checked;
                StopFromDate = dtpStopFromDate.Value;
                StopFromTime = txtStopFromTime.Text;
                StopToDate = dtpStopToDate.Value;
                StopToTime = txtStopToTime.Text;
                Description = txtRemarks.Text;
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
                String time = "";
                String[] hm;

                if (datagrid.RowCount > 0)
                {
                    raceReleasePoint = new BIZ.RaceReleasePoint();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    {
                        LocationName = Convert.ToString(((DateTime)datagrid.Rows[Convert.ToInt32(index)].Cells[9].Value).ToShortDateString()) + " | " + Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[7].Value);
                        if (LocationName != "")
                        {
                            this.cmbLocationName.SelectedItem = LocationName;
                            this.txtReleasePointID.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                            this.cmbMinSpeed.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                            this.chkIsStop.Checked = Convert.ToBoolean(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                            if (chkIsStop.Checked)
                            {
                                this.dtpStopFromDate.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                                this.txtStopFromTime.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value.ToString();
                                this.dtpStopToDate.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);
                                this.txtStopToTime.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value.ToString();
                            }
                            else
                            {
                                this.dtpStopFromDate.Value = DateTime.Now;
                                this.txtStopFromTime.Text = "00:00";
                                this.dtpStopToDate.Value = DateTime.Now;
                                this.txtStopToTime.Text = "00:00";
                            }
                            this.dtpDateRelease.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[9].Value);
                            time = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[10].Value);
                            hm = time.Split(':');

                            if (hm.Length == 2)
                            {
                                if (Convert.ToString(hm[0]).Length == 1)
                                {
                                    this.cmbHour.Text = "0" + Convert.ToString(hm[0]);
                                }
                                else
                                {
                                    this.cmbHour.Text = Convert.ToString(hm[0]);
                                }
                                this.cmbMinute.Text = Convert.ToString(hm[1]);
                            }
                            else
                            {
                                cmbHour.Text = "";
                                cmbMinute.Text = "";// .Text = "";
                            }
                            this.txtMultiplier.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[11].Value);
                            this.txtLapNo.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[12].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetLocationCategory()
        {
            try
            {

                if (cmbLocationName.Text != "")
                {
                    DataTable dtResult;
                    raceReleasePoint = new BIZ.RaceReleasePoint();
                    GetControlValue();
                    PopulateBussinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
                    dtResult = raceReleasePoint.RaceReleasePointGetLocation();
                    if (dtResult.Rows.Count > 0)
                    {
                        RecordSearched = dtResult;
                        PopulateControl();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void InitialiseControl()
        {
            try
            {
                this.txtScheduleCategoryID.Text = RaceScheduleCategoryID.ToString();
                this.txtScheduleCategoryName.Text = RaceScheduleCategoryName.ToString();
                ReleasePointSelectAll();
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[2].Visible = false;
                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].Visible = false;
                this.dataGridView1.Columns[5].Visible = false;
                this.dataGridView1.Columns[6].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReleasePointDelete()
        {
            try
            {
                raceReleasePoint = new BIZ.RaceReleasePoint();
                GetControlValue();
                if (RaceScheduleDetailsID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
                        raceReleasePoint.RaceReleasePointDelete();
                        ClearControl(); 
                        ReleasePointSelectAll(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ReleasePointSelectAll()
        {
            try
            {
                raceReleasePoint = new BIZ.RaceReleasePoint();
                PopulateBussinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
                raceReleasePoint.RaceReleasePointSelectAll(this.dataGridView1);
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
                    txtScheduleDetailsID.Text = RecordSearched.Rows[0]["RaceScheduleDetailsID"].ToString();
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                    dtpDateRelease.Value = Convert.ToDateTime(RecordSearched.Rows[0]["DateRelease"]);
                    txtRemarks.Text = RecordSearched.Rows[0]["Description"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBussinessLayer(Common.Common.RaceEntryClassType type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.RaceEntryClassType.Location:
                        location.ClubID = ClubID;
                        location.ScheduleCategoryID = RaceScheduleCategoryID;
                        break;
                    case Common.Common.RaceEntryClassType.RaceReleasePoint:
                        raceReleasePoint.ClubID = ClubID;
                        raceReleasePoint.UserID = UserID;
                        raceReleasePoint.LocationName = LocationName;
                        raceReleasePoint.RaceReleasePointID = RaceReleasePointID;
                        raceReleasePoint.RaceScheduleDetailsID = RaceScheduleDetailsID;
                        raceReleasePoint.RaceScheduleCategoryID = RaceScheduleCategoryID;
                        raceReleasePoint.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        raceReleasePoint.ReleaseTime = ReleaseTime;
                        raceReleasePoint.ReleaseDate = ReleaseDate;
                        raceReleasePoint.Multiplier = Multiplier;
                        raceReleasePoint.LapNo = LapNo;
                        raceReleasePoint.MinSpeed = MinSpeed;
                        raceReleasePoint.IsStop = IsStop;
                        raceReleasePoint.StopFromDate = StopFromDate;
                        raceReleasePoint.StopFromTime = StopFromTime;
                        raceReleasePoint.StopToDate = StopToDate;
                        raceReleasePoint.StopToTime = StopToTime;
                        raceReleasePoint.Description = Description;
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
                raceReleasePoint = new BIZ.RaceReleasePoint();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
                if (raceReleasePoint.Save())
                {
                    this.txtScheduleCategoryName.Focus();
                    ReleasePointSelectAll();
                    ClearControl();
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
                DataTable dtLocation;
                location = new BIZ.Location();
                PopulateBussinessLayer(Common.Common.RaceEntryClassType.Location);
                dtLocation = location.LocationSearchByScheduleCategory();
                this.cmbLocationName.Items.Add("");
                foreach (DataRow dr in dtLocation.Rows)
                {
                    this.cmbLocationName.Items.Add(dr["LocationName"].ToString());
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
