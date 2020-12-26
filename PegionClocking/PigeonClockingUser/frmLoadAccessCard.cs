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
    public partial class frmLoadAccessCard : Form
    {
        public String UserID { get; set; }
        public frmLoadAccessCard()
        {
            InitializeComponent();
        }

        private void frmLoadAccessCard_Load(object sender, EventArgs e)
        {
            try
            {
                GetLoadBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void GetLoadBalance()
        {
            try
            {
                MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                var result = login.GetLoadBalance(UserID);
                this.label2.Text = "CURRENT BALANCE: " + String.Format("{0:#,##0.00}", result.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text != "")
                {
                    MavcPigeonClockingPortal.Models.MyProfileData login = new MavcPigeonClockingPortal.Models.MyProfileData();
                    var result = login.LoadMavcCard("tcpc", UserID, this.textBox1.Text);
                    this.textBox1.Text = "";
                    MessageBox.Show(result.Rows[0]["result"].ToString().Replace("<br>",Environment.NewLine), "Error");
                    GetLoadBalance();
                    this.textBox1.Focus();
                }
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
