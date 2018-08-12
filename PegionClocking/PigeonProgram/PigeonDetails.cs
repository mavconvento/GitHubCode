using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PigeonProgram
{
    public partial class PigeonDetails : Form
    {
        #region Properties
        public Int64 PigeonID { get; set; }
        public Int64 UserID { get; set; }
        public String TrialVersion { get; set; }
        public String BandNumber { get; set; }
        public String PigeonName { get; set; }
        public Boolean Istrial { get; set; }
        public Boolean WithErrors { get; set; }
        #endregion


        #region Events
        public PigeonDetails()
        {
            InitializeComponent();
            dtPigeonList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void PigeonDetails_Load(object sender, EventArgs e)
        {
            try
            {
                Login();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.dtDateHactchAcquired.Enabled = false;
            }
            else
            {
                this.dtDateHactchAcquired.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pbPigenPicture.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.InitialDirectory = "C:/Picture/";
            f.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
            f.FilterIndex = 2;

            if (f.ShowDialog() == DialogResult.OK)
            {
                pbPigenPicture.Image = Image.FromFile(f.FileName);
                pbPigenPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPigenPicture.BorderStyle = BorderStyle.Fixed3D;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!WithErrors)
                {
                    Save();
                }
                else
                {
                    MessageBox.Show("Error has been detected in the form", "Error");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region Private
        private void Login()
        {
            try
            {

                if (UserID == 0)
                {
                    frmLogin login = new frmLogin();
                    login.ShowDialog();
                    UserID = login.UserID;
                    TrialVersion = login.VERSION;
                    Istrial = login.Istrial;

                    if (UserID == 0)
                    {
                        this.Close();
                    }
                }


                PopulateComboBox();
                LoadPigeonList();
                this.Text = "Pigeon Details " + TrialVersion;


                if (BandNumber != "" && BandNumber != null || PigeonID > 0)
                {
                    GetPigeonDetails();
                    System.Drawing.Size size = new Size();
                    size.Width = 571;
                    size.Height = 568;
                    this.Size = size;
                    dtPigeonList.Visible = false;
                    btnClear.Visible = false;
                    btnSave.Visible = false;
                    btnBrowsePicture.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateComboBox()
        {
            try
            {
                this.cmbGender.Items.Clear();
                this.cmbGender.Items.Add("HEN");
                this.cmbGender.Items.Add("COCK");

                this.cmbFilterGender.Items.Clear();
                this.cmbFilterGender.Items.Add("HEN");
                this.cmbFilterGender.Items.Add("COCK");

                this.cmbYear.Items.Clear();
                for (int i = 1990; i < 2030; i++)
                {
                    this.cmbYear.Items.Add(i);
                    this.cmbFilterYear.Items.Add(i);
                }

                this.cmbRaceCategory.Items.Clear();
                this.cmbRaceCategory.Items.Add("");
                this.cmbRaceCategory.Items.Add("YOUNG BIRD");
                this.cmbRaceCategory.Items.Add("OLD BIRD");
                this.cmbRaceCategory.Items.Add("OPEN BIRD");
                this.cmbRaceCategory.Items.Add("BOTH");
                this.cmbRaceCategory.Items.Add("NONE");

                this.cmbSeason.Items.Clear();
                this.cmbSeason.Items.Add("");
                this.cmbSeason.Items.Add("NORTH");
                this.cmbSeason.Items.Add("SOUTH");
                this.cmbSeason.Items.Add("SUMMER");
                this.cmbSeason.Items.Add("ONE LOFT RACE");
                this.cmbSeason.Items.Add("NONE");

                this.cmbTypeofBreeding.Items.Clear();
                this.cmbTypeofBreeding.Items.Add("");
                this.cmbTypeofBreeding.Items.Add("CROSS-BREED");
                this.cmbTypeofBreeding.Items.Add("IN-BREED");
                this.cmbTypeofBreeding.Items.Add("ISLAND-BORN");
                this.cmbTypeofBreeding.Items.Add("LINE-BREED");
                this.cmbTypeofBreeding.Items.Add("OUT-BREED");
                this.cmbTypeofBreeding.Items.Add("PURE-BREED");

                this.cmbPigeonType.Items.Clear();
                this.cmbPigeonType.Items.Add("");
                this.cmbPigeonType.Items.Add("BREEDER");
                this.cmbPigeonType.Items.Add("FLYER");
                this.cmbPigeonType.Items.Add("STOCK BIRD");

                //Get All Hen
                GetAllHenPigeon();

                //Get All Cock
                GetAllCockPigeon();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte[] GetImage()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (pbPigenPicture.Image != null)
                {
                    pbPigenPicture.Image.Save(ms, pbPigenPicture.Image.RawFormat);
                }

                byte[] image = ms.GetBuffer();
                return image;
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
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;
                pigeonDetails.PigeonName = txtPigeonName.Text.ToUpper();
                pigeonDetails.BandNumber = txtBandNumber.Text.ToUpper();
                pigeonDetails.Gender = cmbGender.Text.ToUpper();
                pigeonDetails.TypeofBreeding = cmbTypeofBreeding.Text.ToUpper();
                pigeonDetails.PigeonType = cmbPigeonType.Text.ToUpper();
                pigeonDetails.EyeColor = txtEyeColor.Text.ToUpper();
                pigeonDetails.Color = txtColor.Text.ToUpper();
                pigeonDetails.Line = txtLine.Text.ToUpper();
                pigeonDetails.Owner = txtOwner.Text.ToUpper();
                pigeonDetails.Achievement = txtAchievement.Text.ToUpper();
                pigeonDetails.Picture = Common.Common.GetImage(this.pbPigenPicture); //GetImage();
                pigeonDetails.UserID = UserID;
                pigeonDetails.IsUnknown = checkBox1.Checked;
                pigeonDetails.HatchDate = dtDateHactchAcquired.Value;
                pigeonDetails.Year = cmbYear.Text.ToUpper();
                pigeonDetails.Season = cmbSeason.Text.ToUpper();
                pigeonDetails.Category = cmbRaceCategory.Text.ToUpper();
                pigeonDetails.ParentCock = cmbParentCock.Text.ToUpper();
                pigeonDetails.ParentHen = cmbParentHen.Text.ToUpper();
                pigeonDetails.PigeonDetailsSave();

                //set list of Pigeon.
                LoadPigeonList();

                //Clear Control
                ClearControl();

                //Get All Hen
                GetAllHenPigeon();

                //Get All Cock
                GetAllCockPigeon();

                MessageBox.Show("Record has been save", "Save Record");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ClearControl()
        {
            try
            {
                PigeonID = 0;
                txtPigeonName.Text = "";
                txtBandNumber.Text = "";
                cmbGender.SelectedIndex = -1;
                cmbTypeofBreeding.SelectedIndex = -1;
                cmbPigeonType.SelectedIndex = -1;
                txtEyeColor.Text = "";
                txtColor.Text = "";
                txtLine.Text = "";
                txtOwner.Text = "";
                txtAchievement.Text = "";
                pbPigenPicture.Image = null;
                checkBox1.Checked = false;
                dtDateHactchAcquired.Value = DateTime.Now;
                cmbYear.SelectedIndex = -1;
                cmbSeason.SelectedIndex = -1;
                cmbRaceCategory.SelectedIndex = -1;
                cmbParentCock.SelectedIndex = -1;
                cmbParentHen.SelectedIndex = -1;
                txtPigeonName.Focus();
                errorProvider1.Clear();
                WithErrors = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetAllHenPigeon()
        {
            try
            {
                DataSet dtResult = new DataSet();
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.Gender = "Hen";
                pigeonDetails.UserID = UserID;
                dtResult = pigeonDetails.GetPigeonByGender();

                this.cmbParentHen.Items.Clear();
                this.cmbParentHen.Items.Add("");
                foreach (DataRow item in dtResult.Tables[0].Rows)
                {
                    this.cmbParentHen.Items.Add(item["PigeonName"].ToString());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetAllCockPigeon()
        {
            try
            {
                DataSet dtResult = new DataSet();
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.Gender = "Cock";
                pigeonDetails.UserID = UserID;
                dtResult = pigeonDetails.GetPigeonByGender();

                this.cmbParentCock.Items.Clear();
                this.cmbParentCock.Items.Add("");
                foreach (DataRow item in dtResult.Tables[0].Rows)
                {
                    this.cmbParentCock.Items.Add(item["PigeonName"].ToString());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LoadPigeonList()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.UserID = UserID;
                pigeonDetails.FilterBreed = txtFilterLine.Text;
                pigeonDetails.FilterGender = cmbFilterGender.Text;
                pigeonDetails.FilterPigeonName = txtFilterPigeonName.Text;
                pigeonDetails.FilterYear = cmbFilterYear.Text;

                dtPigeonList.DataSource = ((DataSet)pigeonDetails.GetPigeonList()).Tables[0];
                dtPigeonList.Columns[0].Visible = false;

                //for (int i = 0; i < dtPigeonList.Columns.Count; i++)
                //{
                //    this.dtPigeonList.Columns[i].Frozen = false;
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LoadPicture(PictureBox pbPigeon, byte[] images)
        {
            try
            {
                pbPigeon.Image = null;
                MemoryStream ms = new MemoryStream(images);
                pbPigeon.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPigeon.Image = Image.FromStream(ms);
            }
            catch (Exception)
            { }
            finally
            { }
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                DataGridView datagrid = this.dtPigeonList;
                DataSet dtResult = new DataSet();
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    BIZ.PigeonDetails pigenDetails = new BIZ.PigeonDetails();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "SELECT")
                    {
                        PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (PigeonID > 0)
                        {
                            GetPigeonDetails();
                        }
                    }
                    else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                    {
                        PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (PigeonID > 0)
                        {
                            DialogResult dialogResult = new DialogResult();
                            dialogResult = MessageBox.Show("Are you sure you want to delete this Pigeon?", "Error", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {
                                DeletePigeonDetails(PigeonID);
                                LoadPigeonList();
                                ClearControl();
                                MessageBox.Show("Record has been deleted", "Delete Record");
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
        private void GetPigeonDetails()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                DataSet dtResult = new DataSet();
                pigeonDetails.PigeonID = PigeonID;
                pigeonDetails.BandNumber = BandNumber;
                dtResult = pigeonDetails.GetPigeonDetails();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        ClearControl();
                        PigeonID = (Int64)dtResult.Tables[0].Rows[0]["PigeonDetailsID"];
                        PigeonName = dtResult.Tables[0].Rows[0]["PigeonName"].ToString().ToUpper();
                        txtPigeonName.Text = dtResult.Tables[0].Rows[0]["PigeonName"].ToString().ToUpper();
                        txtBandNumber.Text = dtResult.Tables[0].Rows[0]["BandNumber"].ToString().ToUpper();
                        txtColor.Text = dtResult.Tables[0].Rows[0]["Color"].ToString().ToUpper();
                        txtEyeColor.Text = dtResult.Tables[0].Rows[0]["EyeColor"].ToString().ToUpper();
                        cmbGender.SelectedItem = dtResult.Tables[0].Rows[0]["Gender"].ToString().ToUpper();
                        checkBox1.Checked = Convert.ToBoolean(dtResult.Tables[0].Rows[0]["IsUnknownDate"]);
                        dtDateHactchAcquired.Value = (DateTime)dtResult.Tables[0].Rows[0]["HatchDateOrAcquiredDate"];
                        txtOwner.Text = dtResult.Tables[0].Rows[0]["Owner"].ToString().ToUpper();
                        txtLine.Text = dtResult.Tables[0].Rows[0]["Breed"].ToString().ToUpper();
                        if (dtResult.Tables[0].Rows[0]["ParentCock"].ToString() != "")
                        {
                            cmbParentCock.SelectedItem = dtResult.Tables[0].Rows[0]["ParentCock"].ToString().ToUpper();
                        }
                        else
                        {
                            cmbParentCock.SelectedItem = null;
                        }
                        if (dtResult.Tables[0].Rows[0]["ParentHen"].ToString() != "")
                        {
                            cmbParentHen.SelectedItem = dtResult.Tables[0].Rows[0]["ParentHen"].ToString().ToUpper();
                        }
                        else
                        {
                            cmbParentHen.SelectedItem = null;
                        }
                        cmbYear.Text = dtResult.Tables[0].Rows[0]["RaceYear"].ToString().ToUpper();
                        cmbSeason.SelectedItem = dtResult.Tables[0].Rows[0]["RaceSeason"].ToString().ToUpper();
                        cmbRaceCategory.SelectedItem = dtResult.Tables[0].Rows[0]["RaceCategory"].ToString().ToUpper();
                        cmbTypeofBreeding.SelectedItem = dtResult.Tables[0].Rows[0]["TypeOfBreeding"].ToString().ToUpper();
                        cmbPigeonType.SelectedItem = dtResult.Tables[0].Rows[0]["PigeonType"].ToString().ToUpper();
                        txtAchievement.Text = dtResult.Tables[0].Rows[0]["Remarks"].ToString().ToUpper();
                        LoadPicture(pbPigenPicture, (byte[])dtResult.Tables[0].Rows[0]["Photo"]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void DeletePigeonDetails(Int64 PigeonID)
        {
            try
            {
                BIZ.PigeonDetails pigenDetails = new BIZ.PigeonDetails();
                pigenDetails.PigeonID = PigeonID;
                pigenDetails.DeletePigeonDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.cmbParentHen.Text != "")
            {
                PigeonProgram.PigeonDetails frmPigeonDetails = new PigeonDetails();
                frmPigeonDetails.BandNumber = this.cmbParentHen.Text;
                frmPigeonDetails.UserID = this.UserID;
                frmPigeonDetails.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.cmbParentCock.Text != "")
            {
                PigeonProgram.PigeonDetails frmPigeonDetails = new PigeonDetails();
                frmPigeonDetails.BandNumber = this.cmbParentCock.Text;
                frmPigeonDetails.UserID = this.UserID;
                frmPigeonDetails.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PigeonID == 0) return;
            Offspring offspring = new Offspring();
            offspring.PigeonID = PigeonID;
            offspring.UserID = this.UserID;
            offspring.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (PigeonID == 0) return;
            Pedigree pedigree = new Pedigree();
            pedigree.PigeonID = PigeonID;
            pedigree.UserID = UserID;
            pedigree.Istrial = Istrial;

            //this.Hide();
            pedigree.ShowDialog();
            //this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PigeonID == 0) return;
            Treatment treatment = new Treatment();
            treatment.PigeonID = PigeonID;
            treatment.PigeonName = PigeonName;
            treatment.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (PigeonID == 0) return;
            Race_Result raceResult = new Race_Result();
            raceResult.PigeonID = PigeonID;
            raceResult.PigeonName = PigeonName;
            raceResult.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPigeonList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                txtFilterLine.Text = "";
                txtFilterPigeonName.Text = "";
                cmbFilterGender.SelectedIndex = -1;
                cmbFilterYear.SelectedIndex = -1;
                LoadPigeonList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBandNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                WithErrors = false;
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtPigeonList.DataSource;

                DataRow[] dtrow = dt.Select("BandNumber = '" + txtBandNumber.Text + "'");

                if (dtrow.Length != 0 && PigeonID == 0)
                {
                    errorProvider1.SetError(txtBandNumber, "Band number already exists");
                    WithErrors = true;
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
