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
    public partial class frmOnlineClocking : Form
    {
        public String UserID { get; set; }
        public String ClubName { get; set; }
        public String DBName { get; set; }
        public List<MavcPigeonClockingPortal.Models.ClubData> ClubList { get; set; }

        public frmOnlineClocking()
        {
            InitializeComponent();
        }

        private void frmOnlineClocking_Load(object sender, EventArgs e)
        {
            GetClubList();
        }

        private void GetClubList()
        {
            MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
            ClubList =  race.GetClubList(UserID);

            foreach (var item in ClubList)
            {
                this.comboBox1.Items.Add(item.clubAbbreviation);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
                ClubName = this.comboBox1.Text;
                this.textBox1.Text = "";
                DBName = ClubList.FirstOrDefault(x => x.clubAbbreviation == ClubName).dbName;
                var result = race.GetRaceEntry(this.comboBox1.Text, "", "", DateTime.Today.Date, "", UserID, "UserApps");

                if (result.Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = result;
                    this.dataGridView1.Columns[1].Visible = false;
                }
                else
                    this.dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClubName != "" && this.textBox1.Text != "")
                {
                    MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
                    var result = race.SendSticker(ClubName, UserID, this.textBox1.Text);
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    MessageBox.Show(result.Rows[0]["result"].ToString(), "Send Sticker");
                }
                else
                {
                    if (ClubName == "")
                    {
                        MessageBox.Show("Please select clubname", "Error");
                    }
                }
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
                MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
                var result = race.Forecast(ClubName, UserID);
                MessageBox.Show(result.Rows[0]["result"].ToString(), "FORECAST");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmResult result = new frmResult();
            this.Hide();
            result.ClubList = ClubList;
            result.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
