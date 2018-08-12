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
    public partial class frmMainMenuAdminRegistration : Form
    {
        #region Variable
        BIZ.RegisterRFID BizRegisterRFID;
        #endregion

        #region Properties
        public frmMainMenuAdmin Parent { get; set; }
        public Int64 BandID { get; set; }
        public Int64 MemberRFIDRegisterID { get; set; }
        public byte[] Picture { get; set; }
        public string PictureFileName { get; set; }
        public string FileExtenstion { get; set; }
        #endregion

        #region Events
        public frmMainMenuAdminRegistration()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmMainMenuAdminRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.CloseSubForm(this);
        }
        private void frmMainMenuAdminRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                Clear(true);
                //FillFormEmtryControl();
                //PopulateDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void rdbTestRing_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (((RadioButton)sender).Checked == true)
                {
                    this.txtBandNumber.Text = "Test Ring";
                    this.txtBandNumber.ReadOnly = true;
                    this.txtRFID.Focus();
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
                if (txtMemberIDNo.Text != "")
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void rdbSeasonRing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked == true)
                {
                    this.txtBandNumber.Text = "";
                    this.txtBandNumber.ReadOnly = false;
                    this.txtBandNumber.Focus();
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
                BizRegisterRFID = new BIZ.RegisterRFID();
                BizRegisterRFID = PopulateBizLayer(BizRegisterRFID);
                if (BizRegisterRFID.Save())
                {
                    SavePicture(txtRFID.Text, this.Picture);
                    MessageBox.Show("RFID registration complete.");
                    Clear();
                    PopulateDataGrid();

                }
                else
                    MessageBox.Show("RFID registration failed.");

            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnNewPlayer_Click(object sender, EventArgs e)
        {
            try
            {
                Clear(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cmbRaceSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PopulateDataGrid();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void btnTakePicture_Click(object sender, EventArgs e)
        {
            try
            {
                frmTakePicture takePicture = new frmTakePicture();
                takePicture.ShowDialog();
                this.PictureFileName = takePicture.PictureFileName;
                this.Picture = takePicture.Picture;

                if (this.PictureFileName != null)
                {
                    this.pbPicturePigeon.Image = Image.FromFile(PictureFileName);
                    this.pbPicturePigeon.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbPicturePigeon.BorderStyle = BorderStyle.Fixed3D;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void cmbBirdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillFormEmtryControl();
                PopulateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnSaveToSDCard_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDir = "";
                string fullpath = "";
                string clubinfo = "";
                string raceInfo = "";

                DataTable dtRegisterRFID = (DataTable)this.dataGridView1.DataSource;

                if (dtRegisterRFID != null)
                {
                    if (dtRegisterRFID.Rows.Count > 0)
                    {
                        DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                        if (driveInfo != null)
                        {
                            sysDir = driveInfo.RootDirectory.ToString();
                            fullpath = sysDir + "ECLOCK\\" + Parent.ClubAbbreviation + "\\" + "RegisterRFID.inf";
                            clubinfo = sysDir + "ECLOCK\\" + Parent.ClubAbbreviation + "\\" + "ClubInfo.inf";

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


                            //Create Register RFID File if not exists in sd card
                            if (!File.Exists(fullpath)) File.Create(fullpath).Close();

                            //clear register rfid File
                            File.WriteAllText(fullpath, String.Empty);

                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                            {
                                string columnHeader = "";
                                foreach (DataColumn item in dtRegisterRFID.Columns)
                                {
                                    if (item.ColumnName.Trim() != "")
                                    {
                                        if (columnHeader != "") columnHeader += "|";
                                        columnHeader = columnHeader + item.ColumnName;
                                    }
                                }

                                //column header
                                columnHeader = "ClubID|MemberID|MemberIDNo|" + columnHeader;
                                file.WriteLine(columnHeader);

                                foreach (DataRow item in dtRegisterRFID.Rows)
                                {
                                    file.WriteLine(
                                        BIZ.Common.Encrypt(
                                        Parent.ClubID.ToString() + "|" +
                                        this.txtMemberID.Text + "|" +
                                        this.txtMemberIDNo.Text + "|" +
                                        item["ID"] + "|" +
                                        item["BandID"] + "|" +
                                        item["Picture"].ToString() + "|" +
                                        item["SerialRFIDNo"].ToString() + "|" +
                                        item["BandNumber"].ToString() + "|" +
                                        item["BirdCategory"].ToString() + "|" +
                                        item["RingType"].ToString()
                                        )
                                    );
                                }
                            };

                            MessageBox.Show("Member Register RFID Importing successful.");
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
                    MemberRFIDRegisterID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (MemberRFIDRegisterID > 0)
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString().ToUpper() == "EDIT")
                        {
                            BandID = (Int64)datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value;
                            txtBandNumber.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value.ToString();
                            txtRFID.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value.ToString();
                            //if (datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value.ToString() != "") LoadPicture(this.pbPicturePigeon,(byte[])datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                            cmbBirdCategory.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value.ToString();
                            rdbSeasonRing.Checked = true;
                            GetPicture(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value.ToString());
                            if (datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value.ToString() == "Test Ring") rdbTestRing.Checked = true;

                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString().ToUpper() == "DELETE")
                        {
                            txtRFID.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value.ToString();
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
        private void LoadPicture(PictureBox pbPigeon, string path)
        {
            try
            {
                pbPigeon.Image = null;

                System.IO.FileStream fs;
                fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                pbPigeon.Image = System.Drawing.Image.FromStream(fs);

                //pbPigeon.Image = Image.FromStream(BIZ.Common.ImageFile(path));
                this.pbPicturePigeon.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pbPicturePigeon.BorderStyle = BorderStyle.Fixed3D;
                fs.Close();
            }
            catch (Exception)
            { }
            finally
            { }
        }
        private void PopulateDataGrid()
        {
            try
            {
                if (txtMemberID.Text != "" && cmbRaceSeason.Text != "")
                {
                    DataTable dt = new DataTable();
                    BizRegisterRFID = new BIZ.RegisterRFID();
                    BizRegisterRFID = PopulateBizLayer(BizRegisterRFID);
                    dt = BizRegisterRFID.GetMemberRFIDRegisterGetByID().Tables[0];
                    this.dataGridView1.DataSource = dt;
                    this.lblTotal.Text = "Total : " + dt.Rows.Count.ToString();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].Visible = false;
                    this.dataGridView1.Columns[2].Visible = false;
                    this.dataGridView1.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void FillFormEmtryControl()
        {
            try
            {
                BizRegisterRFID = new BIZ.RegisterRFID();
                DataSet dsReason = new DataSet();
                BizRegisterRFID.ClubID = Parent.ClubID;
                dsReason = BizRegisterRFID.GetSeason();
                cmbRaceSeason.Items.Clear();

                if (dsReason.Tables.Count > 0)
                {
                    if (dsReason.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dsReason.Tables[0].Rows)
                        {
                            cmbRaceSeason.Items.Add(item["RaceScheduleName"].ToString());
                        }
                    }
                }

                cmbBirdCategory.Items.Add("Young Bird(s)");
                cmbBirdCategory.Items.Add("Old Bird(s)");
                cmbBirdCategory.Items.Add("Both");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Clear(bool IsClearAll = false)
        {
            try
            {
                MemberRFIDRegisterID = 0;
                BandID = 0;
                txtBandNumber.Focus();
                txtBandNumber.Text = "";
                txtRFID.Text = "";
                rdbSeasonRing.Checked = true;
                pbPicturePigeon.Image = null;

                if (IsClearAll)
                {
                    txtMemberIDNo.Text = "";
                    txtMemberID.Text = "";
                    txtCoordinates.Text = "";
                    txtName.Text = "";
                    cmbBirdCategory.Text = "";
                    txtMemberIDNo.Focus();
                    PopulateDataGrid();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private BIZ.RegisterRFID PopulateBizLayer(BIZ.RegisterRFID bizData)
        {
            try
            {
                bizData.MemberID = 0;
                if (txtMemberID.Text != "") bizData.MemberID = Convert.ToInt64(txtMemberID.Text);
                bizData.MemberIDNo = txtMemberIDNo.Text;
                bizData.BandNumber = txtBandNumber.Text;
                bizData.ClubID = Parent.ClubID;
                bizData.RFID = txtRFID.Text;
                bizData.Season = cmbRaceSeason.Text;
                BizRegisterRFID.BandID = BandID;
                BizRegisterRFID.MemberRFIDRegistrationID = MemberRFIDRegisterID;
                bizData.Type = "Season Ring"; //Default
                bizData.Picture = Picture;
                bizData.BirdCategory = this.cmbBirdCategory.Text;

                if (rdbTestRing.Checked == true) bizData.Type = "Test Ring";

                return bizData;
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
                if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    BizRegisterRFID = new BIZ.RegisterRFID();
                    BizRegisterRFID = PopulateBizLayer(BizRegisterRFID);
                    if (BizRegisterRFID.Delete())
                    {
                        string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                        string Extension = "";

                        string infExtesionFile = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + txtRFID.Text + ".inf";
                        if (File.Exists(infExtesionFile))
                        {
                            TextReader tr = new StreamReader(infExtesionFile);
                            using (tr)
                            {
                                Extension = tr.ReadLine();
                            }
                        }

                        this.pbPicturePigeon.Image = null;
                        string fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + txtRFID.Text + Extension;

                        if (File.Exists(fullpath)) File.Delete(fullpath);
                        if (File.Exists(infExtesionFile)) File.Delete(infExtesionFile);
                      
                        MessageBox.Show("RFID Record Deleted.");
                        PopulateDataGrid();
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetPicture(string RFID)
        {
            try
            {
                string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                string Extension = "";

                string infExtesionFile = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + RFID + ".inf";
                if (File.Exists(infExtesionFile))
                {
                    TextReader tr = new StreamReader(infExtesionFile);
                    using (tr)
                    {
                        Extension = tr.ReadLine();
                    }
                }

                this.pbPicturePigeon.Image = null;
                string fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + RFID + Extension;
                if (File.Exists(fullpath))
                {
                    LoadPicture(this.pbPicturePigeon, fullpath);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SavePicture(string RFID, byte[] image)
        {
            try
            {
                if (PictureFileName != "" && PictureFileName != null)
                {
                    string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                    string Extension = Path.GetExtension(PictureFileName);
                    string fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + RFID + Extension;
                    string infExtesionFile = ApplicationDirectory + "\\DataCollection\\Admin\\Picture\\" + RFID + ".inf";

                    //if extension file not exists
                    if (!File.Exists(infExtesionFile)) File.Create(infExtesionFile).Close();

                    File.WriteAllText(infExtesionFile, String.Empty);

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(infExtesionFile, true))
                    {
                        file.WriteLine(Extension);
                    }

                    File.Copy(PictureFileName, fullpath, true);
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
