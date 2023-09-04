using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadCastMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] mobileList = this.txtMobileNumber.Text.Split(';');

            foreach (var item in mobileList)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    itexmo(item, this.txtMessage.Text);
                    if (!String.IsNullOrEmpty(this.txtMessage2.Text))
                    {
                        itexmo(item, this.txtMessage2.Text);
                    }
                }
            }
        }

        public object itexmo(string Number, string Message, string API_CODE = "", string SenderID = "", string Password = "", Boolean isImportant = false)
        {
            var URL = "https://www.itexmo.com/php_api/api.php";
            var APICode = "PR-MARKA754822_4H5EX";
            var Secret = "nc]xkei6ti";

            SenderID = "MAVC-PKC";

            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = URL;
                parameter.Add("1", Number);
                parameter.Add("2", Message);
                parameter.Add("3", APICode);
                parameter.Add("6", SenderID);
                parameter.Add("passwd", Secret);

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
