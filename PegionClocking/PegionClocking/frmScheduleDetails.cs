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
    public partial class frmScheduleDetails : Form
    {
        #region Constant
        #endregion

        #region Variable
        BIZ.RaceScheduleDetails raceScheduleDetails;
        BIZ.Location location;
        //BIZ.ReportGeneration reportGeneration;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ID { get; set; }
        public Int64 ScheduleID { get; set; }
        public String ScheduleName { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public DateTime DateRelease { get; set; }
        public DateTime Loading { get; set; }
        public string LoadingTimeFrom { get; set; }
        public string LoadingTimeTo { get; set; }

        //release point
        public Int64 RaceReleasePointID { get; set; }
        public string ReleaseTime { get; set; }
        public Int64 LapNo { get; set; }
        public double Multiplier { get; set; }
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
        public frmScheduleDetails()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmScheduleDetails_Load_1(object sender, EventArgs e)
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
            ScheduleDetailsDelete();
        }
        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocationDetails();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dataGridView1.DataSource;
                reportGeneration.Type = "ScheduleDetails";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            ClearControl();
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
                //txtScheduleID.Text = "0";
                //txtScheduleName.Text = "";
                cmbLocation.Text = "";
                cmbLocation.SelectedIndex = -1;
                txtLocationID.Text = "0";
                txtDistanceLatDegree.Text = "";
                txtDistanceLatMinutes.Text = "";
                txtDistanceLatSeconds.Text = "";
                cmbLatSign.Text = "";
                txtDistanceLongDegree.Text = "";
                txtDistanceLongMinutes.Text = "";
                txtDistanceLongSeconds.Text = "";
                cmbLongSign.Text = "";
                dtpDateRelease.Value = DateTime.Now;
                dtpLoadingDate.Value = DateTime.Now;
                txtLoadingTimeFrom.Text = "";
                txtLoadingTimeTo.Text = "";
                txtID.Text = "0";
                IsEdit = false;

                cmbHour.Text = "";
                cmbMinute.Text = "";
                txtMultiplier.Text = "1.00";
                txtLapNo.Text = "1";
                cmbMinSpeed.Text = MinSpeed;
                chkIsStop.Checked = false;
                dtpStopFromDate.Value = DateTime.Now;
                txtStopFromTime.Text = "00:00";
                dtpStopToDate.Value = DateTime.Now;
                txtStopToTime.Text = "00:00";
                IsEdit = false;
                txtReleasePointID.Text = "0";
                txtRemarks.Text = "";

                cmbLocation.Focus();
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
                ID = Convert.ToInt64(txtID.Text);
                ScheduleID = Convert.ToInt64(txtScheduleID.Text);
                LocationID = Convert.ToInt64(txtLocationID.Text);
                LocationName = cmbLocation.Text;
                DateRelease = dtpDateRelease.Value;
                Loading = dtpLoadingDate.Value;
                LoadingTimeFrom = txtLoadingTimeFrom.Text;
                LoadingTimeTo = txtLoadingTimeTo.Text;

                RaceReleasePointID = Convert.ToInt64(txtReleasePointID.Text);
                ReleaseTime = cmbHour.Text + ':' + cmbMinute.Text;
                LapNo = Convert.ToInt64(txtLapNo.Text);
                Multiplier = Convert.ToDouble(txtMultiplier.Text);
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
                    raceScheduleDetails = new BIZ.RaceScheduleDetails();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    {
                        LocationName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                        if (LocationName != "")
                        {
                            this.cmbLocation.SelectedItem = LocationName;
                            this.txtID.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                            this.txtLocationID.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                            this.dtpLoadingDate.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);
                            string[] loadingTime = datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value.ToString().Split('-');
                            this.txtLoadingTimeFrom.Text = loadingTime.GetValue(0).ToString().Trim();
                            this.txtLoadingTimeTo.Text = loadingTime.GetValue(1).ToString().Trim();
                            this.dtpDateRelease.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[7].Value);
                            
                            //release point details
                            this.txtReleasePointID.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[9].Value);
                            this.txtLapNo.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[10].Value);
                            this.txtMultiplier.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[11].Value);
                            this.cmbMinSpeed.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[12].Value);
                            this.txtRemarks.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[13].Value);
                            this.chkIsStop.Checked = Convert.ToBoolean(datagrid.Rows[Convert.ToInt32(index)].Cells[14].Value);
                         
                            if (chkIsStop.Checked)
                            {
                                this.dtpStopFromDate.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[15].Value);
                                this.txtStopFromTime.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[16].Value.ToString();
                                this.dtpStopToDate.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[17].Value);
                                this.txtStopToTime.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[18].Value.ToString();
                            }
                            else
                            {
                                this.dtpStopFromDate.Value = DateTime.Now;
                                this.txtStopFromTime.Text = "00:00";
                                this.dtpStopToDate.Value = DateTime.Now;
                                this.txtStopToTime.Text = "00:00";
                            }
                            //this.dtpDateRelease.Value = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[9].Value);
                            time = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[8].Value);
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetLocationDetails()
        {
            try
            {
                DataTable dtResult;
                raceScheduleDetails = new BIZ.RaceScheduleDetails();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.RaceScheduleDetails);
                dtResult = raceScheduleDetails.ScheduleDetailsSearchByKey();
                if (dtResult.Rows.Count > 0)
                {
                    RecordSearched = dtResult;
                    PopulateControl();
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
                this.txtScheduleID.Text = ScheduleID.ToString();
                this.txtScheduleName.Text = ScheduleName.ToString();
                ScheduleDetailsSelectAll();
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[5].Visible = false;  //Loading
                this.dataGridView1.Columns[6].Visible = false;  //LoadingTimeFrom
                this.dataGridView1.Columns[9].Visible = false;  //RaceReleasePointID
                this.dataGridView1.Columns[13].Visible = false; //Description
                this.dataGridView1.Columns[14].Visible = false; //IsStop
                this.dataGridView1.Columns[15].Visible = false; //StopFromDate
                this.dataGridView1.Columns[16].Visible = false; //StopFromTime
                this.dataGridView1.Columns[17].Visible = false; //StopToDate
                this.dataGridView1.Columns[18].Visible = false; //StopToTime
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ScheduleDetailsDelete()
        {
            try
            {
                raceScheduleDetails = new BIZ.RaceScheduleDetails();
                GetControlValue();
                if (ID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer(Common.Common.RaceScheduleDetails.RaceScheduleDetails);
                        raceScheduleDetails.ScheduleDetailsDelete();
                        ClearControl();
                        ScheduleDetailsSelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("Record is selected for deletion.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ScheduleDetailsSelectAll()
        {
            try
            {
                raceScheduleDetails = new BIZ.RaceScheduleDetails();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.RaceScheduleDetails);
                raceScheduleDetails.ScheduleDetailsSelectAll(this.dataGridView1);
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
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBussinessLayer(Common.Common.RaceScheduleDetails type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.RaceScheduleDetails.Location:
                        location.ClubID = ClubID;
                        location.ScheduleID = ScheduleID;
                        break;
                    case Common.Common.RaceScheduleDetails.RaceScheduleDetails:
                        raceScheduleDetails.ClubID = ClubID;
                        raceScheduleDetails.UserID = UserID;
                        raceScheduleDetails.ID = ID;
                        raceScheduleDetails.ScheduleID = ScheduleID;
                        raceScheduleDetails.LocationID = LocationID;
                        raceScheduleDetails.LocationName = LocationName;
                        raceScheduleDetails.DateRelease = DateRelease;
                        raceScheduleDetails.Loading = Loading;
                        raceScheduleDetails.LoadingTimeFrom = LoadingTimeFrom;
                        raceScheduleDetails.LoadingTimeTo = LoadingTimeTo;
                        raceScheduleDetails.RaceReleasePointID = RaceReleasePointID;
                        //raceScheduleDetails.RaceScheduleDetailsID = RaceScheduleDetailsID;
                        //raceScheduleDetails.RaceScheduleCategoryID = RaceScheduleCategoryID;
                        //raceScheduleDetails.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        raceScheduleDetails.ReleaseTime = ReleaseTime;
                        //raceScheduleDetails.ReleaseDate = ReleaseDate;
                        raceScheduleDetails.Multiplier = Multiplier;
                        raceScheduleDetails.LapNo = LapNo;
                        raceScheduleDetails.MinSpeed = MinSpeed;
                        raceScheduleDetails.IsStop = IsStop;
                        raceScheduleDetails.StopFromDate = StopFromDate;
                        raceScheduleDetails.StopFromTime = StopFromTime;
                        raceScheduleDetails.StopToDate = StopToDate;
                        raceScheduleDetails.StopToTime = StopToTime;
                        raceScheduleDetails.Description = Description;
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
                raceScheduleDetails = new BIZ.RaceScheduleDetails();
                GetControlValue();
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.RaceScheduleDetails);
                if (raceScheduleDetails.Save())
                {
                    ClearControl();
                    this.txtScheduleName.Focus();
                    ScheduleDetailsSelectAll();
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
                PopulateBussinessLayer(Common.Common.RaceScheduleDetails.Location);
                dtLocation = location.LocationSelectByRegion();

                foreach (DataRow dr in dtLocation.Rows)
                {
                    this.cmbLocation.Items.Add(dr["LocationName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }


        #endregion

        private void chkIsStop_CheckedChanged_1(object sender, EventArgs e)
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
    }
}
