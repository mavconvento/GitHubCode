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
    public partial class frmChangePassword : Form
    {
        #region Variable
        BIZ.User user;
        //BIZ.Club club;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }

        #endregion

        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = true;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtChangePassword.Text == "")
                {
                    MessageBox.Show("Please enter new password.");
                }
                else
                {
                    user = new BIZ.User();
                    user.UserID = UserID;
                    user.ClubID = ClubID;
                    user.Password = this.txtChangePassword.Text;
                    user.ChangePassword();
               
                    this.txtChangePassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }


    }
}
