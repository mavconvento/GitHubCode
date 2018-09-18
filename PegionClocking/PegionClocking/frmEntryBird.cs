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
    public partial class frmEntryBird : Form
    {
        #region Constant
        #endregion

        #region Variable
        BIZ.RaceCategory raceCategory;
        BIZ.Member member;
        BIZ.Entry entry;
        BIZ.RaceReleasePoint raceReleasePoint;
        #endregion

        #region Properties
        //public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 EntryID { get; set; }
        public Int64 MemberID { get; set; }
        public String MemberIDNo { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public String RaceScheduleName { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public String Lap { get; set; }
        public String LocationName { get; set; }
        public String Coordinates { get; set; }
        public String Distance { get; set; }
        public DateTime ReleasedDate { get; set; }
        public String ReleaseTime { get; set; }
        public String StickerCode { get; set; }
        public Int64 BandID { get; set; }
        public String RingNumber { get; set; }
        public String BarcodeBandID { get; set; }
        public DataTable RaceReleasePointData { get; set; }
        public DataTable MemberDetailsData { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmEntryBird()
        {
            InitializeComponent();
            dtEntryList.DoubleClick += new EventHandler(DTEntryList_DoubleClick);
            dtEntryMemberList.DoubleClick += new EventHandler(DTMemberEntryList_DoubleClick);
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void button4_Click(object sender, EventArgs e) //done
        {
            this.Close();
        }
        private void frmEntryBird_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            dtEntryMemberList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private void btnGO_Click(object sender, EventArgs e)
        {
            GetMemberDetails();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void cmbRaceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntryListGetByRaceReleasePoint();
        }
        private void cmbGroupCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntryListGetByRaceReleasePoint();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        private void btnSummary_Click(object sender, EventArgs e)
        {
            RaceReleasePointSummary();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddNewRing();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GetEntryDetails();
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            frmMemberDataEntry memberDataEntry = new frmMemberDataEntry();
            memberDataEntry.ClubID = ClubID;
            memberDataEntry.UserID = UserID;
            memberDataEntry.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CopyLastEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                frmRaceCategoryGroup raceCategoryGroup = new frmRaceCategoryGroup();
                raceCategoryGroup.ClubID = ClubID;
                raceCategoryGroup.UserID = UserID;
                raceCategoryGroup.ShowDialog();
                PopulateCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void btnSearchIdentity_Click(object sender, EventArgs e)
        {
            try
            {
                GetEntryIdentity();
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName;
                int stopper = 0;
                int row = 7;

                string remarks = "";
                string memberIDNumber = "";
                string barcodeBandID = "";
                string ringNumber = "";
                string outter = "";
                string category = "";
                string group = "";
                string entryremarks = "";

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
                    ws.Unprotect("06242009mavc");

                    while (stopper != 5)
                    {

                        memberIDNumber = Convert.ToString(ws.Cells[row, 1].Value);
                        barcodeBandID = Convert.ToString(ws.Cells[row, 3].Value);
                        ringNumber = Convert.ToString(ws.Cells[row, 4].Value);
                        outter = Convert.ToString(ws.Cells[row, 5].Value);
                        category = Convert.ToString(ws.Cells[row, 6].Value);
                        group = Convert.ToString(ws.Cells[row, 7].Value);
                        entryremarks = Convert.ToString(ws.Cells[row, 8].Value);

                        if (memberIDNumber == "" || memberIDNumber == null)
                        {
                            stopper += 1;
                        }
                        else
                        {
                            DAL.Entry entry = new DAL.Entry();
                            DataSet dtresult = new DataSet();
                            entry.ClubID = ClubID;
                            entry.EntryID = 0;
                            entry.MemberIDNo = Convert.ToString(memberIDNumber);
                            entry.RaceScheduleName = lblRaceSchedule.Text;
                            entry.RaceScheduleCategoryName = lblRaceScheduleCategory.Text;
                            entry.RaceCategoryName = category;
                            entry.RaceCategoryGroupName = group;
                            entry.RaceReleasePointID = Convert.ToInt64(txtReleasePointID.Text);
                            entry.StickerCode = outter;
                            entry.BandID = BandID;
                            entry.RingNumber = ringNumber;
                            entry.BarcodeBandID = barcodeBandID;
                            entry.Remarks = entryremarks;

                            entry.Isuploaded = true;
                            dtresult = entry.Save();
                            if (dtresult.Tables.Count > 0)
                            {
                                if (dtresult.Tables.Count == 2)
                                {
                                    ws.Cells[row, 3] = dtresult.Tables[1].Rows[0]["bandid"].ToString();
                                    remarks = dtresult.Tables[0].Rows[0]["remarks"].ToString();
                                }
                                else
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
                            }
                            else
                            {
                                remarks = "";
                            }
                            stopper = 0;
                        }
                        ws.Cells[row, 9] = remarks;
                        remarks = "";
                        row += 1;
                    }

                    ws.Protect("06242009mavc");
                    excelApp.DisplayAlerts = false;
                    wb.Save();
                    excelApp.DisplayAlerts = true;
                    excelApp.Visible = true;
                    MessageBox.Show("Uploading Race Entry Finished", "Uploading");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                txtMemberIDNo.Text = "";
                frmMemberMasterlist memberMasterlist = new frmMemberMasterlist();
                memberMasterlist.ClubID = ClubID;
                memberMasterlist.UserID = UserID;
                memberMasterlist.Width = 761;
                memberMasterlist.Height = 293;
                memberMasterlist.FormBorderStyle = FormBorderStyle.FixedSingle;
                memberMasterlist.ActionFrom = "Entry";
                memberMasterlist.ShowDialog();

                this.txtMemberIDNo.Text = memberMasterlist.MemberIDNo;
                if (txtMemberIDNo.Text != "")
                {
                    GetMemberDetails();
                } 
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void txtBandID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetEntryDetails();
            }
        }
        private void txtRingNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtStickerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtMemberIDNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetMemberDetails();
            }
        }
        private void txtEntryIdentity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            frmRegisterMobileNumber RegisterMobileNumber = new frmRegisterMobileNumber();
            RegisterMobileNumber.ClubID = ClubID;
            RegisterMobileNumber.UserID = UserID;
            RegisterMobileNumber.ShowDialog();
        }
        #endregion

        #region Public Methods
        public void PopulateControlValue(string Type)
        {
            try
            {
                switch (Type)
                {
                    case "RaceReleasePoint":
                        if (RaceReleasePointData.Rows.Count > 0)
                        {
                            txtReleasePointID.Text = RaceReleasePointData.Rows[0]["RaceReleasePointID"].ToString();
                            lblRaceSchedule.Text = RaceReleasePointData.Rows[0]["RaceScheduleName"].ToString();
                            lblRaceScheduleCategory.Text = RaceReleasePointData.Rows[0]["RaceScheduleCategoryName"].ToString();
                            lblLap.Text = RaceReleasePointData.Rows[0]["Lap"].ToString();
                            lblLocationName.Text = RaceReleasePointData.Rows[0]["LocationName"].ToString();
                            lblCoordinates.Text = RaceReleasePointData.Rows[0]["Coordinates"].ToString();
                            lblDistance.Text = RaceReleasePointData.Rows[0]["Distance"].ToString() + " KM";
                            lblReleaseDate.Text = Convert.ToString(RaceReleasePointData.Rows[0]["ReleasedDate"]).Split(' ').GetValue(0).ToString();
                            lblReleaseTime.Text = RaceReleasePointData.Rows[0]["ReleaseTime"].ToString();
                            lblLapNo.Text = RaceReleasePointData.Rows[0]["LapNo"].ToString();
                        }
                        break;
                    case "Member":
                        if (MemberDetailsData.Rows.Count > 0)
                        {
                            txtMemberID.Text = MemberDetailsData.Rows[0]["MemberID"].ToString();
                            txtMemberName.Text = MemberDetailsData.Rows[0]["MemberName"].ToString();
                            txtMemberCoordinates.Text = MemberDetailsData.Rows[0]["Coordinates"].ToString();
                            dtpExpirationDate.Value = (DateTime)MemberDetailsData.Rows[0]["DateofExpiration"];
                            if (MemberDetailsData.Rows[0]["IsExpired"].ToString() == "true") chkMembershipExpired.Checked = true;
                            txtRingNumber.Enabled = true;
                            txtStickerCode.Enabled = true;
                            txtEntryBarcodeID.Enabled = true;
                            ReadOnlyControl(true);
                        }
                        break;
                    case "MemberDataFromGrid":
                        txtEntryID.Text = EntryID.ToString();
                        txtMemberIDNo.Text = MemberIDNo;
                        txtStickerCode.Text = StickerCode;
                        txtRingNumber.Text = RingNumber;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Private
        private void GetEntryIdentity()
        {
            try
            {
                GetEntryDetails();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        private void CopyLastEntry()
        {
            try
            {
                FrmCopyEntry copyEntry = new FrmCopyEntry();
                GetControlValue();
                copyEntry.ClubID = ClubID;
                copyEntry.RaceScheduleName = RaceScheduleName;
                copyEntry.RaceScheduleCategoryName = RaceScheduleCategoryName;
                copyEntry.RaceReleasePointID = RaceReleasePointID;
                copyEntry.MemberID = MemberID;
                copyEntry.ShowDialog();

                //on dialogbox close
                GetMemberRingEnrolled();
                EntryListGetByMemberIDNo();
                EntryListGetByRaceReleasePoint();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetEntryDetails()
        {
            try
            {
                GetControlValue();
                if (BarcodeBandID != "")
                {
                    entry = new BIZ.Entry();
                    PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
                    DataSet dtResult = new DataSet();
                    dtResult = entry.GetEntryDetailsByEntryBarcodeID();

                    if (dtResult.Tables.Count > 0)
                    {
                        if (dtResult.Tables[0].Rows.Count > 0)
                        {
                            this.BandID = (Int64)dtResult.Tables[0].Rows[0]["BandID"];
                            this.txtMemberID.Text = (string)dtResult.Tables[0].Rows[0]["MemberID"].ToString();
                            this.txtRingNumber.Text = (string)dtResult.Tables[0].Rows[0]["BandNumber"].ToString();
                            this.txtMemberIDNo.Text = (string)dtResult.Tables[0].Rows[0]["MemberIDNo"].ToString();
                            GetMemberDetails();
                            this.txtStickerCode.Focus();
                        }
                        else
                        {
                            //MessageBox.Show("No Record Found or Invalid Entry Barcode ID", "Error");
                            //ClearControl();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void AddNewRing()
        {
            frmMemberRingManagement memberring = new frmMemberRingManagement();
            memberring.IsFromEntry = true;
            memberring.RaceScheduleName = lblRaceSchedule.Text;
            memberring.RaceScheduleCategoryName = lblRaceScheduleCategory.Text;
            memberring.MemberIDNo = txtMemberIDNo.Text;
            memberring.UserID = UserID;
            memberring.ClubID = ClubID;
            memberring.ShowDialog();
            GetMemberRingEnrolled();
        }
        private void GetMemberRingEnrolled()
        {
            member = new BIZ.Member();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RaceEntryClassType.Member);
            this.dataGridView1.DataSource = member.MemberRingSearchByKey();
            this.dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[0].Visible = false;
        }
        private void ClearControl()
        {
            try
            {
                txtMemberID.Text = "0";
                txtEntryID.Text = "0";
                txtMemberIDNo.Text = "";
                txtMemberName.Text = "";
                txtMemberCoordinates.Text = "";
                txtStickerCode.Text = "";
                txtRingNumber.Text = "";
                txtEntryIdentity.Text = "";
                this.BandID = 0;
                dtpExpirationDate.Value = DateTime.Now;
                chkMembershipExpired.Checked = false;
                GetControlValue();  //reset properties value
                ReadOnlyControl(false);
                txtEntryIdentity.Focus();
                button5.Enabled = false;
                this.dtEntryMemberList.DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void EntryListGetByRaceReleasePoint()
        {
            entry = new BIZ.Entry();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
            entry.EntryGetByRaceReleasePoint(this.dtEntryList, this.lblBirdCount);
        }
        private void EntryListGetByMemberIDNo()
        {
            entry = new BIZ.Entry();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
            entry.EntryGetByMemberIDNo(this.dtEntryMemberList, this.lblMemberEntryList);


        }
        private void GetControlValue()
        {
            try
            {
                MemberID = Convert.ToInt64(txtMemberID.Text);
                EntryID = Convert.ToInt64(txtEntryID.Text);
                MemberIDNo = txtMemberIDNo.Text;
                RaceCategoryName = cmbRaceCategory.Text;
                RaceCategoryGroupName = cmbGroupCategory.Text;
                RaceScheduleName = lblRaceSchedule.Text;
                RaceScheduleCategoryName = lblRaceScheduleCategory.Text;
                StickerCode = txtStickerCode.Text;
                RingNumber = txtRingNumber.Text;
                BarcodeBandID = txtEntryIdentity.Text;  //txtEntryBarcodeID.Text;
                RaceReleasePointID = Convert.ToInt64(txtReleasePointID.Text);
                this.lblMemberEntryLabel.Text = "Entry List of " + MemberIDNo;
                this.lblEntryRaceCategory.Text = "List of Entry for : " + RaceCategoryName + "/" + RaceCategoryGroupName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetMemberDetails()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.Member);
                MemberDetailsData = member.MemberDetailsSearchByKey().Tables[0];
                if (MemberDetailsData.Rows.Count > 0)
                {
                    PopulateControlValue("Member");
                    txtMemberIDNo.ReadOnly = true;
                    btnGO.Enabled = false;
                    button1.Enabled = true;
                    button5.Enabled = true;
                    GetMemberRingEnrolled();
                    EntryListGetByMemberIDNo();
                    this.txtRingNumber.Focus();
                }
                else
                {
                    button1.Enabled = false;
                    button5.Enabled = false;
                    MessageBox.Show("No record found, Invalid Member ID", "No Record");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void EntryDelete()
        {
            try
            {
                member = new BIZ.Member();
                GetControlValue();
                if (EntryID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
                        if (entry.EntryDelete())
                        {
                            this.txtEntryID.Text = "0";
                            this.txtRingNumber.Text = "";
                            this.txtStickerCode.Text = "";
                            this.txtEntryBarcodeID.Text = "";
                            this.BandID = 0;
                            this.txtRingNumber.Focus();
                            EntryListGetByRaceReleasePoint();
                            EntryListGetByMemberIDNo();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void DTMemberEntryList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtEntryMemberList;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    EntryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (EntryID > 0)
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                        {
                            MemberIDNo = datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value.ToString();
                            BandID = (Int64)datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value;
                            RingNumber = datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value.ToString();
                            StickerCode = datagrid.Rows[Convert.ToInt32(index)].Cells[7].Value.ToString();
                            txtEntryIdentity.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[8].Value);
                            PopulateControlValue("MemberDataFromGrid");
                            GetMemberDetails();
                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString() == " + ")
                        {
                            frmEntryMultipleCategory frmMultiplyCategory = new frmEntryMultipleCategory();
                            frmMultiplyCategory.BandNumber = datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value.ToString();
                            frmMultiplyCategory.StickerCode = datagrid.Rows[Convert.ToInt32(index)].Cells[7].Value.ToString();
                            frmMultiplyCategory.EntryID = datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value.ToString();
                            frmMultiplyCategory.OrigCategory = datagrid.Rows[Convert.ToInt32(index)].Cells[10].Value.ToString();
                            frmMultiplyCategory.ClubID = ClubID;
                            frmMultiplyCategory.MemberID = txtMemberID.Text;
                            frmMultiplyCategory.ShowDialog();
                            EntryListGetByMemberIDNo();
                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                        {
                            txtEntryID.Text = EntryID.ToString();
                            EntryDelete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void DTEntryList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtEntryList;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    EntryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (EntryID > 0)
                    {
                        MemberIDNo = datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value.ToString();
                        BandID = (Int64)datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value;
                        RingNumber = datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value.ToString();
                        StickerCode = datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value.ToString();
                        txtEntryBarcodeID.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value);
                        PopulateControlValue("MemberDataFromGrid");
                        GetMemberDetails();
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
                    BandID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    txtRingNumber.Text = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateBusinessLayer(Common.Common.RaceEntryClassType Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceEntryClassType.RaceCategory:
                        raceCategory.UserID = UserID;
                        raceCategory.ClubID = ClubID;
                        raceCategory.RaceCategoryName = RaceCategoryName;
                        raceCategory.IsFromEntry = true;
                        break;
                    case Common.Common.RaceEntryClassType.Member:
                        member.ID = "0";
                        member.MemberID = MemberID;
                        member.UserID = UserID;
                        member.RaceScheduleName = RaceScheduleName;
                        member.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        member.ClubID = ClubID;
                        member.MemberIDNo = MemberIDNo;
                        break;
                    case Common.Common.RaceEntryClassType.Entry:
                        entry.ClubID = ClubID;
                        entry.UserID = UserID;
                        entry.EntryID = EntryID;
                        entry.MemberID = MemberID;
                        entry.RaceScheduleName = RaceScheduleName;
                        entry.RaceScheduleCategoryName = RaceScheduleCategoryName;
                        entry.RaceCategoryName = RaceCategoryName;
                        entry.RaceCategoryGroupName = RaceCategoryGroupName;
                        entry.RaceReleasePointID = RaceReleasePointID;
                        entry.StickerCode = StickerCode;
                        entry.BandID = BandID;
                        entry.RingNumber = RingNumber;
                        entry.BarcodeBandID = BarcodeBandID;
                        break;
                    case Common.Common.RaceEntryClassType.RaceReleasePoint:
                        raceReleasePoint.ClubID = ClubID;
                        raceReleasePoint.UserID = UserID;
                        raceReleasePoint.RaceReleasePointID = RaceReleasePointID;
                        break;

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
                raceCategory = new BIZ.RaceCategory();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceCategory);

                //Race Schedule
                DataTable dtRaceCategory;
                DataTable dtRaceCategoryGroup;

                dtRaceCategory = raceCategory.RaceCategoryGetByKey().Tables[0];
                dtRaceCategoryGroup = raceCategory.RaceCategoryGetByKey().Tables[1];

                if (dtRaceCategory.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceCategory.Rows)
                    {
                        cmbRaceCategory.Items.Add(dtrow["Description"].ToString());
                    }
                }
                if (dtRaceCategoryGroup.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceCategoryGroup.Rows)
                    {
                        cmbGroupCategory.Items.Add(dtrow["RaceCategoryGroupName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ReadOnlyControl(Boolean value)
        {
            txtRingNumber.Enabled = value;
            txtStickerCode.Enabled = value;
            //txtEntryBarcodeID.Enabled = value;
            btnSave.Enabled = value;
            btnNew.Enabled = value;
            if (EntryID > 0)
            {
                txtMemberIDNo.ReadOnly = true;
                btnGO.Enabled = false;
                btnClear.Enabled = !value;
                btnDelete.Enabled = value;
            }
            else
            {
                btnGO.Enabled = true;
                txtMemberIDNo.ReadOnly = false;
                btnClear.Enabled = !value;
                btnDelete.Enabled = value;
            }
        }
        private void Save()
        {
            try
            {

                GetControlValue();
                if (RaceCategoryGroupName != "" && RaceCategoryName != "")
                {
                    entry = new BIZ.Entry();
                    PopulateBusinessLayer(Common.Common.RaceEntryClassType.Entry);
                    if (entry.Save() && !IsEdit)
                    {
                        //ClearControl(); 
                        //this.txtMemberIDNo.Focus();
                        this.txtEntryID.Text = "0";
                        this.txtRingNumber.Text = "";
                        this.txtStickerCode.Text = "";
                        this.txtEntryIdentity.Text = "";
                        this.BandID = 0;
                        this.txtRingNumber.Focus();
                        GetMemberRingEnrolled();
                    }
                    EntryListGetByMemberIDNo();
                    EntryListGetByRaceReleasePoint();
                }
                else
                {
                    MessageBox.Show("Please select Race Category and Race Group Category, Invalid Entry", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
                this.txtStickerCode.Focus();
            }
        }
        private void RaceReleasePointSummary()
        {
            DataTable dtResult;
            raceReleasePoint = new BIZ.RaceReleasePoint();
            GetControlValue();
            PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceReleasePoint);
            dtResult = raceReleasePoint.RaceReleasePointSummary();

            if (dtResult.Rows.Count > 0)
            {
                frmBirdEntryMasterlist releasePointSummary = new frmBirdEntryMasterlist();
                releasePointSummary.RaceReleasePointData = RaceReleasePointData;
                releasePointSummary.ReleasePointSummary = dtResult;
                releasePointSummary.ClubID = ClubID;
                releasePointSummary.UserID = UserID;
                releasePointSummary.RaceReleasePointID = RaceReleasePointID;
                releasePointSummary.ShowDialog();
            }
        }



        #endregion

        private void shapeContainer1_Load(object sender, EventArgs e)
        {

        }
    }
}
