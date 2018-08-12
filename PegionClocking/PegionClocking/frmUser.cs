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
    public partial class frmUser : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.User user;
        BIZ.Club club;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String ClubName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String ReTypePassword { get; set; }

        public DataTable RecordSearched { get; set; }
        public Boolean IsEdit { get; set; }
        #endregion

        #region Events
        public frmUser()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmUser_Load_1(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = true;
            ClearControl();
            InitialiseControl();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UserDelete();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void InitialiseControl()
        {
            try
            {
                PopulateCombobox();
                UserSelectAll();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
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
                txtClubID.Text = "0";
                cmbClub.SelectedIndex = -1;
                txtUserID.Text = "0";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtReTypePassword.Text = "";
                IsEdit = false;
                txtUserName.Focus();
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
                ClubName = cmbClub.SelectedItem.ToString();
                UserID = Convert.ToInt64(txtUserID.Text);
                UserName = txtUserName.Text;
                Password = txtPassword.Text;
                ReTypePassword = txtReTypePassword.Text;
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
                    //User = new BIZ.RaceUser();
                    index = datagrid.CurrentRow.Index;
                    ClubID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    ClubName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    UserID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                    UserName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);

                    PopulateControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void UserDelete()
        {
            try
            {
                user = new BIZ.User();
                GetControlValue();
                if (UserID > 0)
                {
                    if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        PopulateBussinessLayer();
                        user.UserDelete();
                        ClearControl();
                        UserSelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void UserSelectAll()
        {
            try
            {
                user = new BIZ.User();
                PopulateBussinessLayer();
                user.UserSelectAll(this.dataGridView1);
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
                txtUserID.Text = UserID.ToString();
                txtUserName.Text = UserName;
                cmbClub.SelectedItem = ClubName;
                txtClubID.Text = ClubID.ToString();
                
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
                user.ClubID = ClubID;
                user.UserID = UserID;
                user.UserName = UserName;
                user.Password = Password;
                user.ReTypePassword = ReTypePassword;
                user.ClubName = ClubName;
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
                user = new BIZ.User();
                GetControlValue();
                PopulateBussinessLayer();
                if (user.Save())
                {
                    ClearControl();
                    this.txtUserName.Focus();
                    UserSelectAll();
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
                DataTable dtResult;
                club = new BIZ.Club();
                dtResult=club.ClubSelectAll();
                foreach (DataRow dr in dtResult.Rows)
                {
                    cmbClub.Items.Add(dr["Club Name"].ToString());
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
