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
    public partial class frmClub : Form
    {


        #region Constant
        #endregion

        #region Variable
        BIZ.Club Club;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public String ClubAbbreviation { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }
        public String Version { get; set; }
        public String DateTimeSource { get; set; }
        public Boolean IsBackUp { get; set; }
        public Boolean IsMAVCStickerUsed { get; set; }
        public DateTime LastSubcription { get; set; }
        public DateTime SubcriptionDate { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        public String Server { get; set; }
        #endregion

        #region Events
        public frmClub()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmClub_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Global.IsMainDatabase = true;
                PopulateCombobox();
                ClearControl();
                ClubSelectAll();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClubDelete();
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
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
                txtClubID.Text = "0";
                txtClubName.Text = "";
                txtClubAcronym.Text = "";
                txtDistanceLatDegree.Text = "0";
                txtDistanceLatMinutes.Text = "0";
                txtDistanceLatSeconds.Text = "0";
                cmbLatSign.Text = "";
                txtDistanceLongDegree.Text = "0";
                txtDistanceLongMinutes.Text = "0";
                txtDistanceLongSeconds.Text = "0";
                cmbLongSign.Text = "";
                IsEdit = true;
                cmbVersion.SelectedItem = "3";
                cmbDateTimeSource.SelectedItem = "Network";
                comboBox1.SelectedItem = "Server 1";
                chkIsBackUp.Checked = false;
                chkMAVCSticker.Checked = true;
                dtpLastSubcriptionDate.Value = DateTime.Now;
                dtpSubcriptionDate.Value = DateTime.Now;
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
                ClubID = Convert.ToInt64(txtClubID.Text);
                ClubName = txtClubName.Text;
                ClubAbbreviation = txtClubAcronym.Text;
                DistanceLatDegree = Convert.ToInt64(txtDistanceLatDegree.Text);
                DistanceLatMinutes = Convert.ToInt64(txtDistanceLatMinutes.Text);
                DistanceLatSecond = Convert.ToDouble(txtDistanceLatSeconds.Text);
                DistanceLatSign = Convert.ToString(cmbLatSign.Text);
                DistanceLongDegree = Convert.ToInt64(txtDistanceLongDegree.Text);
                DistanceLongMinutes = Convert.ToInt64(txtDistanceLongMinutes.Text);
                DistanceLongSecond = Convert.ToDouble(txtDistanceLongSeconds.Text);
                DistanceLongSign = Convert.ToString(cmbLongSign.Text);
                LastSubcription = Convert.ToDateTime(dtpLastSubcriptionDate.Value);
                SubcriptionDate = Convert.ToDateTime(dtpSubcriptionDate.Value);
                Version = cmbVersion.Text;
                DateTimeSource = cmbDateTimeSource.Text;
                IsBackUp = chkIsBackUp.Checked;
                IsMAVCStickerUsed = chkMAVCSticker.Checked;
                Server = comboBox1.SelectedItem.ToString();
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
                    Club = new BIZ.Club();
                    index = datagrid.CurrentRow.Index;
                    ClubID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);

                    if (ClubID >= 0)
                    {
                        DataTable dtresult = new DataTable();
                        PopulateBussinessLayer();
                        dtresult = Club.ClubSearchByKey();

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
        private void ClubDelete()
        {
            try
            {
                Club = new BIZ.Club();
                GetControlValue();
                if (ClubID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        Club.ClubDelete();
                        ClearControl();
                        ClubSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ClubSelectAll()
        {
            try
            {
                Club = new BIZ.Club();
                PopulateBussinessLayer();
                Club.ClubSelectAll(this.dataGridView1);
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
                if (RecordSearched.Rows.Count > 0)
                {
                    IsEdit = true;
                    txtClubID.Text = RecordSearched.Rows[0]["ClubID"].ToString();
                    txtClubName.Text = RecordSearched.Rows[0]["ClubName"].ToString();
                    txtClubAcronym.Text = RecordSearched.Rows[0]["ClubAbbreviation"].ToString();
                    txtDistanceLatDegree.Text = RecordSearched.Rows[0]["DistanceLatDegree"].ToString();
                    txtDistanceLatMinutes.Text = RecordSearched.Rows[0]["DistanceLatMinutes"].ToString();
                    txtDistanceLatSeconds.Text = RecordSearched.Rows[0]["DistanceLatSecond"].ToString();
                    cmbLatSign.Text = RecordSearched.Rows[0]["DistanceLatSign"].ToString();
                    txtDistanceLongDegree.Text = RecordSearched.Rows[0]["DistanceLongDegree"].ToString();
                    txtDistanceLongMinutes.Text = RecordSearched.Rows[0]["DistanceLongMinutes"].ToString();
                    txtDistanceLongSeconds.Text = RecordSearched.Rows[0]["DistanceLongSecond"].ToString();
                    cmbLongSign.Text = RecordSearched.Rows[0]["DistanceLongSign"].ToString();
                    cmbDateTimeSource.Text = RecordSearched.Rows[0]["DateTimeSource"].ToString();
                    cmbVersion.Text = RecordSearched.Rows[0]["Version"].ToString();
                    chkIsBackUp.Checked = (bool)RecordSearched.Rows[0]["IsBackup"];
                    chkMAVCSticker.Checked = (bool)RecordSearched.Rows[0]["IsMAVCStickerUsed"];
                    dtpLastSubcriptionDate.Value =(DateTime)RecordSearched.Rows[0]["LastSubscriptionDate"];
                    dtpSubcriptionDate.Value = (DateTime)RecordSearched.Rows[0]["SubscriptionDate"];
                    comboBox1.SelectedItem = RecordSearched.Rows[0]["Server"];
                }
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
                Club.UserID = UserID;
                Club.ClubID = ClubID;
                Club.ClubName = ClubName;
                Club.ClubAbbreviation = ClubAbbreviation;
                Club.DistanceLatDegree = DistanceLatDegree;
                Club.DistanceLatMinutes = DistanceLatMinutes;
                Club.DistanceLatSecond = DistanceLatSecond;
                Club.DistanceLatSign = DistanceLatSign;
                Club.DistanceLongDegree = DistanceLongDegree;
                Club.DistanceLongMinutes = DistanceLongMinutes;
                Club.DistanceLongSecond = DistanceLongSecond;
                Club.DistanceLongSign = DistanceLongSign;
                Club.Version = Version;
                Club.DateTimeSource = DateTimeSource;
                Club.IsBackUp = IsBackUp;
                Club.IsMAVCStickerUsed = IsMAVCStickerUsed;
                Club.LastSubcription = LastSubcription;
                Club.SubcriptionDate = SubcriptionDate;
                Club.Server = Server;
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
                Club = new BIZ.Club();
                GetControlValue();
                PopulateBussinessLayer();
                if (Club.Save())
                {
                    ClearControl();
                    this.txtClubName.Focus();
                    ClubSelectAll();
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

                //Version
                this.cmbVersion.Items.Add("1");
                this.cmbVersion.Items.Add("3");

                //DateTimeSource
                this.cmbDateTimeSource.Items.Add("Network");
                this.cmbDateTimeSource.Items.Add("System");

                //Server
                this.comboBox1.Items.Add("Server 1");
                this.comboBox1.Items.Add("Server 2");
                this.comboBox1.Items.Add("Server 3");
                this.comboBox1.Items.Add("Server 4");
                this.comboBox1.Items.Add("Server 5");
                this.comboBox1.Items.Add("Server 6");
                this.comboBox1.Items.Add("Server 7");
                this.comboBox1.Items.Add("Server 8");
                this.comboBox1.Items.Add("Server 9");
                this.comboBox1.Items.Add("Server 10");

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

    }
}
