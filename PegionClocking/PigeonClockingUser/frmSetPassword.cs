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
    public partial class frmSetPassword : Form
    {
        public frmSetPassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                {
                    if (textBox2.Text == textBox3.Text)
                    {
                        MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                        var result = login.ForgotPassword(new MavcPigeonClockingPortal.Models.ForgotPasswordData() { MobileNumber = this.textBox1.Text, Password = this.textBox2.Text, ActionType = "SetPassword", ReTypePassword = this.textBox3.Text, SecurityCode = "" });
                        MessageBox.Show(result.Tables[0].Rows[0]["errmsg"].ToString(), "Set Password");
                        this.textBox1.Text = "";
                        this.textBox2.Text = "";
                        this.textBox3.Text = "";
                        this.textBox1.Focus();
                    }
                    else
                        MessageBox.Show("Password did not match.", "Error");
                }
                else
                    MessageBox.Show("Missing Required Field.", "Error");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
