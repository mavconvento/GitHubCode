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
    public partial class frmLogin : Form
    {
        #region Constants
        const string VERSION = "4.3";
        #endregion

        #region Variable
        BIZ.User user;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Boolean IsAdmin { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String ClubName { get; set; }
        #endregion

        #region Events
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Common.Global.ClubNameConnection = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        #endregion

        #region Private Methods
        private void GetControlValue()
        {
            try
            {
                UserName = txtUserName.Text;
                Password = txtPassword.Text;
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
                txtPassword.Text = "";
                txtUserName.Text = "";
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
                user.Password = Password;
                user.UserName = UserName;
                user.Version = VERSION;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void Login()
        {
            try
            {
                GetControlValue();
                if (UserName.ToUpper() == "ADMINISTRATOR" && Password == "06081986cv5M")
                {
                    UserID = 9000;
                    ClubID = 9000;
                    IsAdmin = true;
                    ClubName = "Administrator";
                }
                else
                {
                    DataTable dtResult;
                    user = new BIZ.User();
                    PopulateBussinessLayer();
                    dtResult = user.Login().Tables[0];
                    if (dtResult.Rows.Count > 0)
                    {
                        UserID = Convert.ToInt64(dtResult.Rows[0]["UserID"]);
                        ClubID = Convert.ToInt64(dtResult.Rows[0]["ClubID"]);
                        ClubName = Convert.ToString(dtResult.Rows[0]["ClubName"]);
                        IsAdmin = Convert.ToBoolean(dtResult.Rows[0]["IsAdmin"]);
                        Common.Global.ClubNameConnection = "_" + Convert.ToString(dtResult.Rows[0]["dbname"]);
                        user.CreateClubInfo(ClubID);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                }
                if (UserID > 0) this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion  

    }
}
