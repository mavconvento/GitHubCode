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
    public partial class frmForgotPassword : Form
    {
        public String UserID { get; set; }
        public frmForgotPassword()
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
                if (this.textBox1.Text != "")
                {
                    MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                    var result = login.ForgotPassword(new MavcPigeonClockingPortal.Models.ForgotPasswordData() { ActionType = "ForgotPassword", MobileNumber = this.textBox1.Text, Password = "", ReTypePassword = "", SecurityCode = "" });
                    var message = result.Tables[0].Rows[0]["ErrMsg"].ToString();

                    if (message.Contains("Your password will send to your mobile."))
                    {
                        string[] value = message.Split('|');

                        var sms = "Your Password: " + value[1];

                        //send password via sms
                        var ret = itexmo(this.textBox1.Text, sms, "PR-MARKA754822_4H5EX");
                        if (ret.ToString() == "0")
                        {
                            MessageBox.Show(value[0], "Forgot Password");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error detected while password is sending on your mobile number.", "Forgot Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private object itexmo(string Number, string Message, string API_CODE, Boolean isImportant = false)
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                parameter.Add("1", Number);
                parameter.Add("2", Message);
                parameter.Add("3", API_CODE);

                if (isImportant)
                {
                    parameter.Add("5", "HIGH");
                }
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }
            return functionReturnValue;
        }
    }
}
