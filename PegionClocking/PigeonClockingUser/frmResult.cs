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
    public partial class frmResult : Form
    {
        public String UserID { get; set; }
        public String ClubName { get; set; }

        public List<MavcPigeonClockingPortal.Models.ClubData> ClubList { get; set; }

        public frmResult()
        {
            InitializeComponent();
        }

        private void frmResult_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in ClubList)
                {
                    this.comboBox3.Items.Add(item.clubName);
                }

                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
                ClubName = ClubList.FirstOrDefault(x => x.clubName == this.comboBox3.Text).clubAbbreviation;
                var birdCategory = race.GetBirdCategory(ClubName);
                var raceCategory = race.GetGroupCategory(ClubName);

                this.comboBox2.Items.Clear();
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.Add("All");
                this.comboBox2.Items.Add("All");

                foreach (DataRow item in birdCategory.Rows)
                {
                    this.comboBox2.Items.Add(item["Description"]);
                }

                foreach (DataRow item in raceCategory.Rows)
                {
                    this.comboBox1.Items.Add(item["Race Group"]);
                }

                this.comboBox1.SelectedIndex = comboBox1.FindStringExact("All");
                this.comboBox2.SelectedIndex = comboBox1.FindStringExact("All");
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
                if (this.comboBox3.Text != "")
                {
                    MavcPigeonClockingPortal.Models.RaceResultData race = new MavcPigeonClockingPortal.Models.RaceResultData();
                    var result = race.GetRaceResult(ClubName, this.comboBox2.Text, this.comboBox1.Text, this.dateTimePicker1.Value.Date, this.textBox1.Text);
                    var raceDetails = race.GetRaceDetails(ClubName, this.comboBox2.Text, this.comboBox1.Text, this.dateTimePicker1.Value.Date, this.textBox1.Text, UserID);


                    lbllocation.Text = "Location Name: " + raceDetails.locationName;
                    lbltime.Text = "Release Time: " + raceDetails.releaseTime;
                    lblTotalEntry.Text = "Total Bird Entry: " + raceDetails.totalBird;
                    lblTotalArrived.Text = "Total Arrived: " + raceDetails.sMSCount;
                    lblTotalClock.Text = "Min Speed: " + raceDetails.minSpeed;
                    this.dataGridView1.DataSource = result;

                    foreach (DataGridViewColumn item in this.dataGridView1.Columns)
                    {
                        if (!item.Name.Contains("Rank") && !item.Name.Contains("MemberName") && !item.Name.Contains("RingNumber") && !item.Name.Contains("Speed") && !item.Name.Contains("Remarks"))
                        {
                            this.dataGridView1.Columns[item.Name].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
