using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;

namespace PegionClocking
{
    public partial class frmMemberDataEntry : Form
    {
        #region Variables
        BIZ.Member member;
        #endregion

        #region Properties
        public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public string MemberIDNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtensionName { get; set; }
        public string LoftName { get; set; }
        public string Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSeconds { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSeconds { get; set; }
        public String DistanceLongSign { get; set; }
        public DateTime DateofMembership { get; set; }
        public DateTime LastRenewalDate { get; set; }
        public DateTime DateofExpiration { get; set; }
        public Boolean DeActivatedMember { get; set; }

        //record from member from BackEnd
        public DataTable RecordSearched { get; set; }
        public DataTable RecordSearchedSMS { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        #endregion

        #region Events
        public frmMemberDataEntry()
        {
            InitializeComponent();
        }
        private void frmMemberDataEntry_Load(object sender, EventArgs e)
        {
            InitializeControl();
            PopulateCombobox();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            MemberDetailDelete();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName;
                int stopper = 0;
                int row = 8;

                string remarks = "";
                string memberIDNumber = "";
                string lastName = "";
                string firstName = "";
                string middleName = "";
                string loftName = "";

                int latdegree = 0;
                int latmin = 0;
                double latsec = 0;
                string latsection = "";

                int longdegree = 0;
                int longmin = 0;
                double longsec = 0;
                string longsection = "";

                openFileDialog1.Filter = "Excel .xls|*.xls";
                openFileDialog1.FileName = "";
                openFileDialog1.ShowDialog();
                fileName = openFileDialog1.FileName;


                if (fileName != "")
                {
                    excel.Application excelApp = new excel.Application();
                    excel.Workbook wb;
                    excel.Worksheet ws;

                    //GetTemplate();
                    wb = excelApp.Workbooks.Open(fileName);
                    ws = wb.Sheets[1];

                    while (stopper != 5)
                    {

                        memberIDNumber = Convert.ToString(ws.Cells[row, 1].Value);
                        lastName = ws.Cells[row, 2].Value;
                        firstName = ws.Cells[row, 3].Value;
                        middleName = ws.Cells[row, 4].Value;
                        loftName = ws.Cells[row, 5].Value;

                        //latitude
                        if (Convert.ToString(ws.Cells[row, 6].Value) == "")
                        {
                            latdegree = 0;
                        }
                        else
                        {
                            latdegree = Convert.ToInt32(ws.Cells[row, 6].Value);
                        }

                        if (Convert.ToString(ws.Cells[row, 7].ToString()) == "")
                        {
                            latmin = 0;
                        }
                        else
                        {
                            latmin = Convert.ToInt32(ws.Cells[row, 7].Value);
                        }

                        if (Convert.ToString(ws.Cells[row, 8].Value) == "")
                        {
                            latsec = 0;
                        }
                        else
                        {
                            latsec = Convert.ToDouble(ws.Cells[row, 8].Value);
                        }


                        latsection = Convert.ToString(ws.Cells[row, 9].Value);


                        //longtitude
                        if (Convert.ToString(ws.Cells[row, 10].Value) == "")
                        {
                            longdegree = 0;
                        }
                        else
                        {
                            longdegree = Convert.ToInt32(ws.Cells[row, 10].Value);
                        }

                        if (Convert.ToString(ws.Cells[row, 11].Value) == "")
                        {
                            longmin = 0;
                        }
                        else
                        {
                            longmin = Convert.ToInt32(ws.Cells[row, 11].Value);
                        }

                        if (Convert.ToString(ws.Cells[row, 12].Value) == "")
                        {
                            longsec = 0;
                        }
                        else
                        {
                            longsec = Convert.ToDouble(ws.Cells[row, 12].Value);
                        }

                        longsection = Convert.ToString(ws.Cells[row, 13].Value);

                        if (memberIDNumber == "" || memberIDNumber == null)
                        {
                            stopper += 1;
                        }
                        else
                        {
                            DAL.Member member = new DAL.Member();
                            DataSet dtresult = new DataSet();
                            member.MemberIDNo = memberIDNumber;
                            member.FirstName = firstName;
                            member.LastName = lastName;
                            member.MiddleName = middleName;
                            member.LoftName = loftName;
                            member.DistanceLatDegree = latdegree;
                            member.DistanceLatMinutes = latmin;
                            member.DistanceLatSeconds = latsec;
                            member.DistanceLatSign = latsection;
                            member.DistanceLongDegree = longdegree;
                            member.DistanceLongMinutes = longmin;
                            member.DistanceLongSeconds = longsec;
                            member.DistanceLongSign = longsection;
                            member.ExtensionName = "";
                            member.Address = "";
                            member.DateofBirth = DateTime.Now;
                            member.DateofMembership = DateTime.Now;
                            member.DateofExpiration = DateTime.Now;
                            member.LastRenewalDate = DateTime.Now;
                            member.ID = "0";
                            member.ClubID = ClubID;
                            member.UserID = UserID;
                            member.IsUploaded = true;
                            dtresult = member.Save();
                            if (dtresult.Tables.Count > 0)
                            {
                                if (dtresult.Tables[0].Rows.Count > 0)
                                {
                                    remarks = dtresult.Tables[0].Rows[0]["Remarks"].ToString();
                                }
                                else
                                {
                                    remarks = "";
                                }
                            }
                            else
                            {
                                remarks = "";
                            }
                            stopper = 0;
                        }
                        ws.Cells[row, 14] = remarks;
                        remarks = "";
                        row += 1;
                    }

                    excelApp.DisplayAlerts = false;
                    wb.Save();
                    excelApp.DisplayAlerts = true;
                    excelApp.Visible = true;
                    MessageBox.Show("Member List Uploading Finished", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmRegisterMobileNumber RegisterMobileNumber = new frmRegisterMobileNumber();
            RegisterMobileNumber.ClubID = ClubID;
            RegisterMobileNumber.UserID = UserID;
            RegisterMobileNumber.ShowDialog();
        }
        #endregion

        #region Private Methods
        private void InitializeControl()
        {
            if (IsEdit)
            {
                btnClear.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }
        private void Save()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                PopulateBussinessLayer();
                if (member.Save() && !IsEdit) { ClearControl(); this.txtMemberIDNo.Focus(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void MemberDetailDelete()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                if (ID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        if (member.MemberDetailsDelete()) { this.Close(); }
                    }
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
                //Extension Name
                this.cmbExtName.Items.Add("SR.");
                this.cmbExtName.Items.Add("JR.");
                this.cmbExtName.Items.Add("I");
                this.cmbExtName.Items.Add("II");
                this.cmbExtName.Items.Add("III");
                this.cmbExtName.Items.Add("IV");
                this.cmbExtName.Items.Add("V");

                //Direction Sign
                this.cmbLatSign.Items.Add("N");
                this.cmbLatSign.Items.Add("S");
                this.cmbLongSign.Items.Add("E");
                this.cmbLongSign.Items.Add("W");

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
                MemberIDNo = txtMemberIDNo.Text;
                LastName = txtLastName.Text;
                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                ExtensionName = cmbExtName.Text;
                LoftName = txtLoftName.Text;
                Address = txtAddress.Text;
                DateofBirth = dtpDateofBirth.Value;
                DistanceLatDegree = Convert.ToInt64(txtDistanceLatDegree.Text);
                DistanceLatMinutes = Convert.ToInt64(txtDistanceLatMinutes.Text);
                DistanceLatSeconds = Convert.ToDouble(txtDistanceLatSeconds.Text);
                DistanceLatSign = Convert.ToString(cmbLatSign.Text);
                DistanceLongDegree = Convert.ToInt64(txtDistanceLongDegree.Text);
                DistanceLongMinutes = Convert.ToInt64(txtDistanceLongMinutes.Text);
                DistanceLongSeconds = Convert.ToDouble(txtDistanceLongSeconds.Text);
                DistanceLongSign = Convert.ToString(cmbLongSign.Text);
                DateofMembership = dtpDateofMembership.Value;
                LastRenewalDate = dtpLastRenewalDate.Value;
                DateofExpiration = dtpDateofExpiration.Value;
                DeActivatedMember = chkDeactivateMember.Checked;

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
                txtID.Text = "0";
                txtMemberIDNo.Text = "";
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                cmbExtName.Text = "";
                txtLoftName.Text = "";
                txtAddress.Text = "";
                txtDistanceLatDegree.Text = "0";
                txtDistanceLatMinutes.Text = "0";
                txtDistanceLatSeconds.Text = "0";
                cmbLatSign.Text = "";
                txtDistanceLongDegree.Text = "0";
                txtDistanceLongMinutes.Text = "0";
                txtDistanceLongSeconds.Text = "0";
                cmbLongSign.Text = "";
                chkDeactivateMember.Checked = false;
                dtpDateofBirth.Value = DateTime.Now;
                dtpDateofExpiration.Value = DateTime.Now;
                dtpDateofMembership.Value = DateTime.Now;
                dtpLastRenewalDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateBussinessLayer()
        {
            try
            {
                member.ID = Convert.ToString(ID);
                member.ClubID = ClubID;
                member.UserID = UserID;
                member.MemberIDNo = MemberIDNo;
                member.LastName = LastName;
                member.FirstName = FirstName;
                member.MiddleName = MiddleName;
                member.ExtensionName = ExtensionName;
                member.LoftName = LoftName;
                member.Address = Address;
                member.DateofBirth = DateofBirth;
                member.DistanceLatDegree = DistanceLatDegree;
                member.DistanceLatMinutes = DistanceLatMinutes;
                member.DistanceLatSeconds = DistanceLatSeconds;
                member.DistanceLatSign = DistanceLatSign;
                member.DistanceLongDegree = DistanceLongDegree;
                member.DistanceLongMinutes = DistanceLongMinutes;
                member.DistanceLongSeconds = DistanceLongSeconds;
                member.DistanceLongSign = DistanceLongSign;
                member.DateofMembership = DateofMembership;
                member.LastRenewalDate = LastRenewalDate;
                member.DateofExpiration = DateofExpiration;
                member.DeactivateMember = DeActivatedMember;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ApplyBusinessRule(Common.Common.ApplyBusinessRules type)
        {
            switch (type)
            {
                case Common.Common.ApplyBusinessRules.Save:

                    break;
            }
        }
        #endregion

        #region Public Methods
        public void PopulateControl()
        {
            try
            {
                if (RecordSearched.Rows.Count > 0)
                {
                    txtID.Text = RecordSearched.Rows[0]["ID"].ToString();
                    txtMemberIDNo.Text = RecordSearched.Rows[0]["MemberIDNo"].ToString();
                    txtLastName.Text = RecordSearched.Rows[0]["LastName"].ToString();
                    txtFirstName.Text = RecordSearched.Rows[0]["FirstName"].ToString();
                    txtMiddleName.Text = RecordSearched.Rows[0]["MiddleName"].ToString();
                    cmbExtName.Text = RecordSearched.Rows[0]["ExtensionName"].ToString();
                    txtLoftName.Text = RecordSearched.Rows[0]["LoftName"].ToString();
                    txtAddress.Text = RecordSearched.Rows[0]["Address"].ToString();
                    dtpDateofBirth.Value = Common.Common.ConvertDate(RecordSearched.Rows[0]["DateofBirth"].ToString());
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSeconds"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSeconds"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                    dtpDateofMembership.Value = Common.Common.ConvertDate(RecordSearched.Rows[0]["DateofMembership"].ToString());
                    dtpLastRenewalDate.Value = Common.Common.ConvertDate(RecordSearched.Rows[0]["LastRenewalDate"].ToString());
                    dtpDateofExpiration.Value = Common.Common.ConvertDate(RecordSearched.Rows[0]["DateofExpiration"].ToString());
                    chkDeactivateMember.Checked = Convert.ToBoolean(RecordSearched.Rows[0]["DeactivateMember"]);
                    this.dataGridView1.DataSource = RecordSearchedSMS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
