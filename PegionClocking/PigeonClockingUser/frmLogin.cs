using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonClockingUser
{
    public partial class frmLogin : Form
    {
        public String UserID { get; set; }
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                login.UserName = this.txtUserID.Text;
                login.Password = this.txtPassword.Text;
                var dt = login.ValidateLogin(login).Tables[0];

                if (LWT.Common.LWTSafeTypes.SafeInt64(dt.Rows[0]["loginID"]) > 0)
                {
                    UserID = LWT.Common.LWTSafeTypes.SafeString(dt.Rows[0]["UserID"]);
                    frmMenu menu = new frmMenu();
                    menu.UserID = UserID;
                    this.Hide();
                    menu.ShowDialog();
                    this.txtPassword.Text = "";
                    this.txtUserID.Text = "";
                    this.txtUserID.Focus();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Invalid UserID or Password", "Invalid Login");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmForgotPassword forgotpassword = new frmForgotPassword();
                this.Hide();
                forgotpassword.ShowDialog();
                this.txtPassword.Text = "";
                this.txtUserID.Text = "";
                this.txtUserID.Focus();
                this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetPassword setPassword = new frmSetPassword();
                this.Hide();
                setPassword.ShowDialog();
                this.txtPassword.Text = "";
                this.txtUserID.Text = "";
                this.txtUserID.Focus();
                this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
