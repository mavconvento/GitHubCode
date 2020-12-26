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
    public partial class frmMenu : Form
    {
        public String UserID { get; set; }
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            frmOnlineClocking form = new frmOnlineClocking();
            this.Hide();
            form.UserID = UserID;
            form.ShowDialog();
            GetLoadBalance();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmLoadAccessCard form = new frmLoadAccessCard();
            this.Hide();
            form.UserID = UserID;
            form.ShowDialog();
            GetLoadBalance();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmChangePassword form = new frmChangePassword();
            this.Hide();
            form.UserID = UserID;
            form.ShowDialog();
            GetLoadBalance();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetLoadBalance()
        {
            try
            {
                MavcPigeonClockingPortal.Models.LoginData login = new MavcPigeonClockingPortal.Models.LoginData();
                var result = login.GetLoadBalance(UserID);
                this.label5.Text = "LOAD BALANCE: " + String.Format("{0:#,##0.00}", result.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
