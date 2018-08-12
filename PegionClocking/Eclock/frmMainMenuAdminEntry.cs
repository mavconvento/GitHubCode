using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eclock
{
    public partial class frmMainMenuAdminEntry : Form
    {
        #region Variable
        BIZ.RegisterRFID BizRegisterRFID;
        BIZ.Entry BizEntry;
        #endregion

        #region Properties
        public frmMainMenuAdmin Parent { get; set; }
        public Int64 MemberRFIDRegisterID { get; set; }
        public Int64 BandID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public Int64 EclockEntryID { get; set; }
        public bool Isverified { get; set; }
        #endregion

        #region Events
        public frmMainMenuAdminEntry()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmMainMenuAdminEntry_Load(object sender, EventArgs e)
        {
            try
            {
                //PopulateDataGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void frmMainMenuAdminEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.CloseSubForm(this);
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsResult = new DataSet();
                BizEntry = new BIZ.Entry();
                BizEntry = PopulateEntryBizLayer(BizEntry);
                dsResult = BizEntry.GetReleasePointDetails();

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsResult.Tables[0].Rows[0];
                        lblLiberationPoint.Text = dr["LocationName"].ToString();
                        lblReleaseDate.Text = dr["DateRelease"].ToString();
                        lblCoordinates.Text = dr["Coordinates"].ToString();
                        lblEclockEntry.Text = dr["TotalEntry"].ToString();
                        RaceReleasePointID = (Int64)dr["RaceReleasePointID"];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BizRegisterRFID = new BIZ.RegisterRFID();
                DataSet dsMemberDetails = new DataSet();
                BizRegisterRFID.ClubID = Parent.ClubID;
                BizRegisterRFID.MemberIDNo = txtMemberIDNo.Text;

                dsMemberDetails = BizRegisterRFID.GetMemberDetails();

                if (dsMemberDetails.Tables.Count > 0)
                {
                    if (dsMemberDetails.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsMemberDetails.Tables[0].Rows[0];
                        txtName.Text = dr["MemberName"].ToString();
                        txtLoftName.Text = dr["LoftName"].ToString();
                        txtCoordinates.Text = dr["Coordinates"].ToString();
                        txtMemberID.Text = dr["MemberID"].ToString();
                        PopulateDataGrid();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnVerifyRFID_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsResult = new DataSet();
                BizEntry = new BIZ.Entry();
                BizEntry = PopulateEntryBizLayer(BizEntry);
                dsResult = BizEntry.VerifyRFID();

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsResult.Tables[0].Rows[0];
                        txtBandNumber.Text = dr["BandNumber"].ToString();
                        BandID = (Int64)dr["BandID"];
                        MemberRFIDRegisterID = (Int64)dr["ID"];
                        Isverified = true;
                    }
                }
                else
                {
                    MessageBox.Show("RFID is not register.");
                    Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isverified)
                {
                    BizEntry = new BIZ.Entry();
                    BizEntry = PopulateEntryBizLayer(BizEntry);
                    if (BizEntry.Save())
                    {
                        MessageBox.Show("Entry saving success.");
                        PopulateDataGrid();
                        Clear();
                    }
                    else
                        MessageBox.Show("Entry saving failed.", "Error");
                }
                else
                {
                    MessageBox.Show("RFID not verified.", "Error");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnNewMember_Click(object sender, EventArgs e)
        {
            try
            {
                Clear(true);
                PopulateDataGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnImporttoSDCard_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDir = "";
                string fullpath = "";
                string clubinfo = "";
                string raceInfo = "";

                DataTable dtEntry = (DataTable)this.dataGridView1.DataSource;

                if (dtEntry != null)
                {
                    if (dtEntry.Rows.Count > 0)
                    {
                        DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                        if (driveInfo != null)
                        {
                            sysDir = driveInfo.RootDirectory.ToString();
                            fullpath = sysDir + "ECLOCK\\" + Parent.ClubAbbreviation + "\\" + "RaceEntry.inf";
                            clubinfo = sysDir + "ECLOCK\\" + Parent.ClubAbbreviation + "\\" + "ClubInfo.inf";
                            raceInfo = sysDir + "ECLOCK\\" + Parent.ClubAbbreviation + "\\" + "RaceInfo.inf";

                            //create Eclock folder if not exists
                            if (!Directory.Exists(sysDir + "ECLOCK")) Directory.CreateDirectory((sysDir + "ECLOCK"));

                            //create club folder not exists
                            if (!Directory.Exists(sysDir + "ECLOCK\\" + Parent.ClubAbbreviation)) Directory.CreateDirectory((sysDir + "ECLOCK\\" + Parent.ClubAbbreviation));

                            //create clubinfo if not exists
                            if (!File.Exists(clubinfo)) File.Create(clubinfo).Close();

                            //clear club info File
                            File.WriteAllText(clubinfo, String.Empty);

                            //write club info in clubinfo file
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(clubinfo, true))
                            {
                                file.WriteLine(Parent.ClubID + "|" +
                                               Parent.ClubName + "|" +
                                               Parent.ClubAbbreviation);
                            };

                            //create raceinfo if not exists
                            if (!File.Exists(raceInfo)) File.Create(raceInfo).Close();

                            //clear club info File
                            File.WriteAllText(raceInfo, String.Empty);

                            //write club info in clubinfo file
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(raceInfo, true))
                            {
                                file.WriteLine(Parent.ClubID + "|" +
                                               Parent.ClubName + "|" +
                                               Parent.ClubAbbreviation + "|" +
                                               this.txtMemberID.Text + "|" +
                                               lblLiberationPoint.Text + "|" +
                                               lblReleaseDate.Text + "|" +
                                               lblCoordinates.Text);
                            };

                            //Create RaceEntry File if not exists in sd card
                            if (!File.Exists(fullpath)) File.Create(fullpath).Close();

                            //clear Race Entry File
                            File.WriteAllText(fullpath, String.Empty);

                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                            {
                                string columnHeader = "";
                                foreach (DataColumn item in dtEntry.Columns)
                                {
                                    if (item.ColumnName.Trim() != "")
                                    {
                                        if (columnHeader != "") columnHeader += "|";
                                        columnHeader = columnHeader + item.ColumnName;
                                    }
                                }

                                //column header
                                columnHeader = "ClubID|RaceReleasePoint|MemberID|MemberIDNo|" + columnHeader;
                                file.WriteLine(columnHeader);

                                foreach (DataRow item in dtEntry.Rows)
                                {
                                    file.WriteLine(
                                        BIZ.Common.Encrypt(
                                        Parent.ClubID.ToString() + "|" +
                                        this.RaceReleasePointID.ToString() + "|" +
                                        this.txtMemberID.Text + "|" +
                                        this.txtMemberIDNo.Text + "|" +
                                        item["ID"] + "|" +
                                        item["BandID"] + "|" +
                                        item["MemberRegisterRFID"] + "|" +
                                        item["BandNumber"] + "|" +
                                        item["SerialRFIDNo"].ToString()
                                        )
                                    );
                                }
                            };

                            MessageBox.Show("Member Entry Importing successful.");
                        }
                        else
                        {
                            MessageBox.Show("Please insert player SD Card.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region Private Methods
        private BIZ.Entry PopulateEntryBizLayer(BIZ.Entry bizData)
        {
            try
            {
                bizData.ClubID = Parent.ClubID;
                bizData.ReleaseDate = dtpReleasePoint.Value;
                bizData.RFIDSerialNo = txtRFID.Text;
                bizData.ReleasepointID = RaceReleasePointID;
                bizData.BandID = BandID;
                bizData.BandNumber = txtBandNumber.Text;
                bizData.MemberRFIDRegisterID = MemberRFIDRegisterID;
                bizData.EclockEntryID = EclockEntryID;
                bizData.MemberID = 0;
                if (txtMemberID.Text != "") bizData.MemberID = Convert.ToInt64(txtMemberID.Text);
                return bizData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private BIZ.RegisterRFID PopulateRegisterRFIDBizLayer(BIZ.RegisterRFID bizData)
        {
            try
            {
                bizData.MemberID = 0;
                if (txtMemberID.Text != "") bizData.MemberID = Convert.ToInt64(txtMemberID.Text);
                bizData.MemberIDNo = txtMemberIDNo.Text;
                bizData.ClubID = Parent.ClubID;

                return bizData;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void PopulateDataGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                BizEntry = new BIZ.Entry();
                BizEntry = PopulateEntryBizLayer(BizEntry);
                dt = BizEntry.GetMemberEclockEntry().Tables[0];
                this.dataGridView1.DataSource = dt;
                this.lblTotalEntry.Text = "Total : " + dt.Rows.Count.ToString();
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[2].Visible = false;

            }
            catch (Exception ex)
            {

                throw ex;
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
                    BizRegisterRFID = new BIZ.RegisterRFID();
                    index = datagrid.CurrentRow.Index;
                    EclockEntryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (EclockEntryID > 0)
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString().ToUpper() == "EDIT")
                        {
                            BandID = (Int64)datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value;
                            MemberRFIDRegisterID = (Int64)datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value;
                            txtBandNumber.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value.ToString();
                            txtRFID.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value.ToString();
                            Isverified = true;
                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString().ToUpper() == "DELETE")
                        {
                            Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void Delete()
        {
            try
            {
                if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    BizEntry = new BIZ.Entry();
                    BizEntry = PopulateEntryBizLayer(BizEntry);
                    if (BizEntry.Delete()) MessageBox.Show("Eclock entry deleted.");
                    PopulateDataGrid();
                    Clear();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Clear(Boolean IsclearAll = false)
        {
            try
            {
                EclockEntryID = 0;
                BandID = 0;
                txtBandNumber.Text = "";
                MemberRFIDRegisterID = 0;
                txtRFID.Text = "";
                Isverified = false;
                txtRFID.Focus();

                if (IsclearAll)
                {
                    txtMemberIDNo.Text = "";
                    txtMemberID.Text = "";
                    txtCoordinates.Text = "";
                    txtName.Text = "";
                    txtLoftName.Text = "";
                    txtMemberIDNo.Focus();
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
