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
    public partial class frmMemberRingManagement : Form
    {
        #region Constant
        #endregion

        #region Variables
        BIZ.Member member;
        BIZ.RaceSchedule raceSchedule;
        BIZ.RaceScheduleCategory raceScheduleCategory;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string MemberIDNo { get; set; }
        public string RFIDSerialNo { get; set; }
        public String RFIDSerialNoEncrypted { get; set; }
        public DataTable MemberDetailsData { get; set; }
        public Boolean IsFromEntry { get; set; }
        #endregion

        #region Events
        public frmMemberRingManagement()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmMemberRingManagement_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            this.rbIndividual.Checked = true;
            if (IsFromEntry)
            {
                EnrollRingFromEntryView(RaceScheduleCategoryName, MemberIDNo);
                this.rbBatch.Enabled = false;
            }
        }
        private void btnGO_Click(object sender, EventArgs e)
        {
            GetMemberDetails();
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Delete();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetMemberRingEnrolled();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearControl();
            this.txtMemberIDNo.Focus();
        }
        private void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            GetMemberRingEnrolled();
        }
        private void cmbRaceSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRaceScheduleCategoryItems();
            ClearControl();
            if (this.cmbRaceScheduleCategory.Text != "")
            {
                this.groupBox1.Enabled = true;
            }
            else
            {
                this.groupBox1.Enabled = false;
            }


        }
        private void cmbRaceScheduleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbRaceScheduleCategory.Text != "")
                {
                    this.groupBox1.Enabled = true;
                }
                else
                {
                    this.groupBox1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.txtRange1.Text = "0";
                this.txtRange2.Text = "0";
                this.txtBandFormat.Text = "";
                this.txtRingNumber.Focus();
                SetStatus(false);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.txtRingNumber.Text = "";
                this.txtBandFormat.Focus();
                SetStatus(true);
            }
        }
        #endregion

        #region Private Methods
        private void SetStatus(Boolean type)
        {
            this.txtBandFormat.Visible = type;
            this.txtRange1.Visible = type;
            this.txtRange2.Visible = type;
            this.label7.Visible = type;
            this.label4.Visible = type;
            this.label6.Visible = type;

            this.txtRingNumber.Visible = !type;
            this.label14.Visible = !type;
            this.txtRFIDSerialNo.Visible = !type;
            this.label8.Visible = !type;
        }
        private void EnrollRingFromEntryView(string raceScheduleCategoryName, string memberIDNo)
        {
            this.cmbRaceSchedule.SelectedItem = RaceScheduleName;
            this.cmbRaceScheduleCategory.SelectedItem = raceScheduleCategoryName;
            this.cmbRaceScheduleCategory.Enabled = false;
            this.cmbRaceSchedule.Enabled = false;
            this.txtMemberIDNo.Text = memberIDNo;
            GetMemberDetails();
            this.btnNew.Enabled = false;
            //this.btnDelete.Enabled = false;
        }

        private void SetRaceScheduleCategoryItems()
        {
            try
            {
                raceScheduleCategory = new BIZ.RaceScheduleCategory();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RingManagement.RaceScheduleCategory);

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
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;
                    BandID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    RingNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    PopulateControl();
                    this.rbIndividual.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ClearControl()
        {
            try
            {
                txtMemberID.Text = "0";
                txtMemberIDNo.Text = "";
                txtMemberName.Text = "";
                txtMemberCoordinates.Text = "";
                txtRingNumber.Text = "";
                txtBandFormat.Text = "";
                rbIndividual.Checked = true;
                txtRange1.Text = "0";
                txtRange2.Text = "0";
                txtBandID.Text = "0";
                GetControlValue();  //reset properties value
                ReadOnlyControl(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                if (MemberID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBusinessLayer(Common.Common.RingManagement.Member);
                        if (member.MemberRingDelete())
                        {
                            txtRingNumber.Text = "";
                            this.txtBandID.Text = "0";
                            txtRingNumber.Focus();
                            GetMemberRingEnrolled();
                            GetControlValue();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetMemberRingEnrolled()
        {
            member = new BIZ.Member();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RingManagement.Member);
            this.dataGridView1.DataSource = member.MemberRingSearchByKey();
            this.dataGridView1.Columns[0].Visible = false;
        }
        private void PopulateCombobox()
        {
            try
            {
                raceSchedule = new BIZ.RaceSchedule();
                PopulateBusinessLayer(Common.Common.RingManagement.Schedule);

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
        private void GetMemberDetails()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RingManagement.Member);
                MemberDetailsData = member.MemberDetailsSearchByKey().Tables[0];
                if (MemberDetailsData.Rows.Count > 0)
                {
                    PopulateControlValue();
                    txtMemberIDNo.ReadOnly = true;
                    btnGO.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No record found, Invalid Member ID", "No Record");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateControlValue()
        {
            try
            {
                if (MemberDetailsData.Rows.Count > 0)
                {
                    txtMemberID.Text = MemberDetailsData.Rows[0]["MemberID"].ToString();
                    txtMemberName.Text = MemberDetailsData.Rows[0]["MemberName"].ToString();
                    txtMemberCoordinates.Text = MemberDetailsData.Rows[0]["Coordinates"].ToString();
                    ReadOnlyControl(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateControl()
        {
            try
            {
                txtRingNumber.Text = RingNumber;
                txtBandID.Text = BandID.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReadOnlyControl(Boolean value)
        {
            txtRingNumber.Enabled = value;
            txtRFIDSerialNo.Enabled = value;
            txtBandFormat.Enabled = value;
            txtRange1.Enabled = value;
            txtRange2.Enabled = value;
            btnSave.Enabled = value;
            btnNew.Enabled = value;
            if (MemberID > 0)
            {
                txtMemberIDNo.ReadOnly = true;
                btnGO.Enabled = false;
                btnDelete.Enabled = value;
            }
            else
            {
                btnGO.Enabled = true;
                txtMemberIDNo.ReadOnly = false;
                btnDelete.Enabled = value;
            }
        }
        private void GetControlValue()
        {
            try
            {
                MemberID = Convert.ToInt64(txtMemberID.Text);
                MemberIDNo = txtMemberIDNo.Text;
                RingNumber = txtRingNumber.Text;
                RaceScheduleName = cmbRaceSchedule.Text;
                RaceScheduleCategoryName = cmbRaceScheduleCategory.Text;
                BandID = Convert.ToInt64(txtBandID.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBusinessLayer(Common.Common.RingManagement type)
        {
            try
            {
                switch (type)
                {
                    case Common.Common.RingManagement.Member:
                        member.MemberID = MemberID;
                        member.MemberIDNo = MemberIDNo;
                        member.RaceScheduleName = RaceScheduleName;
                        member.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        member.BandID = BandID;
                        member.ClubID = ClubID;
                        member.UserID = UserID;
                        member.RingNumber = RingNumber;
                        break;
                    case Common.Common.RingManagement.RaceScheduleCategory:
                        raceScheduleCategory.RaceScheduleCategoryID = 0;
                        raceScheduleCategory.ClubID = ClubID;
                        raceScheduleCategory.UserID = UserID;
                        raceScheduleCategory.RaceScheduleName = RaceScheduleName;
                        break;
                    case Common.Common.RingManagement.Schedule:
                        raceSchedule.UserID = UserID;
                        raceSchedule.ClubID = ClubID;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Save()
        {
            try
            {
                member = new BIZ.Member();

                if (this.rbIndividual.Checked)
                {
                    GetControlValue();
                    PopulateBusinessLayer(Common.Common.RingManagement.Member);
                    if (member.MemberRingSave())
                    {
                        clearBandInfo();
                        this.txtRingNumber.Focus();
                        GetControlValue();
                        MessageBox.Show("Member Ring Record Save!", "Record Save");

                    }
                }
                else
                {
                    if (Convert.ToInt32(txtRange1.Text) >= Convert.ToInt32(txtRange2.Text))
                    {
                        MessageBox.Show("Invalid Range.", "Error");
                    }
                    else if (this.txtBandFormat.Text == "")
                    {
                        MessageBox.Show("Band format is empty.", "Error");
                    }
                    else
                    {
                        for (int a = Convert.ToInt32(this.txtRange1.Text); a <= Convert.ToInt32(this.txtRange2.Text); a++)
                        {
                            this.txtRingNumber.Text = txtBandFormat.Text + a.ToString();
                            GetControlValue();
                            PopulateBusinessLayer(Common.Common.RingManagement.Member);
                            member.MemberRingSave();
                        }
                        clearBandInfo();
                        this.txtBandFormat.Focus();
                        GetControlValue();
                        MessageBox.Show("Member Ring Record Save!", "Record Save");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
                clearBandInfo();
            }
        }
        private void clearBandInfo()
        {
            this.txtRingNumber.Text = "";
            this.txtBandID.Text = "0";
            this.txtBandFormat.Text = "";
            this.txtRange1.Text = "0";
            this.txtRange2.Text = "0";
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}