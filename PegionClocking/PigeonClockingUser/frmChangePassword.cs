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
    public partial class frmChangePassword : Form
    {
        public String UserID { get; set; }
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == textBox2.Text)
                {
                    MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                    var result = login.ChangePassword(new MavcPigeonClockingPortal.Models.ForgotPasswordData() { MobileNumber = UserID, Password = this.textBox1.Text, ActionType = "ChangePasswordApps", SecurityCode = "", ReTypePassword = this.textBox2.Text });
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    MessageBox.Show(result.Tables[0].Rows[0]["ErrMsg"].ToString(), "Change Password");
                }
                else
                    MessageBox.Show("Password not match","Error Change Password");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
