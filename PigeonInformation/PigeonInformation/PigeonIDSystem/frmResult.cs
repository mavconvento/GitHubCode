using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmResult : Form
    {
        public String ClubName { get; set; }
        public frmResult()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.button3.Text == "SET")
            {
                ControlState(true);
                this.button3.Text = "CHANGE";
                this.txtMemberID.Focus();
            }
            else if (this.button3.Text == "CHANGE")
            {
                ControlState(false);
                this.button3.Text = "SET";
            }
        }

        private void ControlState(bool value)
        {
            try
            {
                //this.txtrfid.Enabled = value;
                this.txtMemberID.Enabled = value;
                this.btnSyncTraining.Enabled = value;
                this.btnSync.Enabled = value;
                this.dateTimePicker1.Enabled = !value;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMemberID.Text != "")
                {
                    ControlState(true);
                    GetPigeonList(this.txtMemberID.Text);
                    GetMemberName(this.txtMemberID.Text);
                    //this.txtrfid.Focus();
                }
                else
                {
                    MessageBox.Show("Please entry MemberID", "Error");
                    this.txtMemberID.Focus();
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message, "Error");
            }
        }

        private void GetMemberName(string memberID)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                string filepath = path + "\\members\\" + memberID + ".txt";

                if (File.Exists(filepath))
                {
                    string[] memberDetails = ReadText.ReadTextFile(filepath);
                    this.txtName.Text = memberDetails[1].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid MemberID", "Error");
                    ClearControl();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetPigeonList(string memberid)
        {
            try
            {
                DataTable pigeonList = new DataTable();

                //DataColumn dc1 = new DataColumn();
                //dc1.ColumnName = "EDIT";

                //DataColumn dc2 = new DataColumn();
                //dc2.ColumnName = "DELETE";

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "SeqID";

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "BandNumber";

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "TagID";

                DataColumn dc6 = new DataColumn();
                dc6.ColumnName = "Category";

                DataColumn dc7 = new DataColumn();
                dc7.ColumnName = "Sex";

                DataColumn dc8 = new DataColumn();
                dc8.ColumnName = "Color";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "Arrival";

                DataColumn dc9 = new DataColumn();
                dc9.ColumnName = "Flight";

                DataColumn dc10 = new DataColumn();
                dc10.ColumnName = "Speed";

                //pigeonList.Columns.Add(dc1);
                pigeonList.Columns.Add(dc2);
                
                pigeonList.Columns.Add(dc4);
                pigeonList.Columns.Add(dc5);
                pigeonList.Columns.Add(dc6);
                pigeonList.Columns.Add(dc7);
                pigeonList.Columns.Add(dc8);
                pigeonList.Columns.Add(dc3);
                pigeonList.Columns.Add(dc9);
                pigeonList.Columns.Add(dc10);

                string path = ReadText.ReadFilePath("datapath");
                string dateString = this.dateTimePicker1.Value.Year.ToString() + this.dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + this.dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');

                //string entryDirectory = path + "entry\\" + dateString;
                //string filepath = entryDirectory + "\\" + memberid + ".txt";

                string resultDirectory = path + "\\result\\" + dateString;
                string filepath = resultDirectory + "\\" + memberid + ".txt";

                //string filepathList = entryDirectory + "\\" + MemberID + ".txt";

                if (File.Exists(filepath))
                {
                    string[] entryCollection = ReadText.ReadTextFile(filepath);
                    int counter = 1;
                    foreach (var rfid in entryCollection)
                    {
                        string birdDetailsPath = path + "\\PigeonDetails\\" + memberid + "\\" + rfid + ".txt";
                        string[] pigeonDetailsCollection = ReadText.ReadTextFile(birdDetailsPath);

                        DataRow dr = pigeonList.NewRow();
                        //dr["EDIT"] = "EDIT";
                        //dr["DELETE"] = "DELETE";
                        dr["SeqID"] = counter;
                        dr["BandNumber"] = pigeonDetailsCollection[0].ToString();
                        dr["TagID"] = pigeonDetailsCollection[1].ToString();
                        dr["Category"] = pigeonDetailsCollection[2].ToString();
                        dr["Color"] = pigeonDetailsCollection[4].ToString();
                        dr["Sex"] = pigeonDetailsCollection[3].ToString();

                        

                        String resultDetailsPath = resultDirectory + "\\" + memberid + "\\" + rfid + ".txt";

                        if (File.Exists(resultDetailsPath))
                        {
                            string[] resultDetails = ReadText.ReadTextFile(resultDetailsPath);
                            dr["Arrival"] = resultDetails[3] + " " + resultDetails[4];

                            if (resultDetails.Count() > 5)
                            {
                                dr["Flight"] = resultDetails[5];
                                dr["Speed"] = resultDetails[6];
                            }
                        }

                        pigeonList.Rows.Add(dr);
                        counter++;
                    }
                }

                dtList.DataSource = pigeonList;
                lblcount.Text = "Total Birds: " + pigeonList.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }

        private void Result_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Focus();
            ClubName = Common.GetClub();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            frmSyncEclock sync = new frmSyncEclock();
            sync.ClubName = ClubName;
            sync.MemberID = txtMemberID.Text;
            sync.DateRelease = this.dateTimePicker1.Value;
            sync.ActionType = "RESULTRACE";
            sync.ShowDialog();

            GetPigeonList(this.txtMemberID.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ClearControl()
        {
            //clear control
            this.txtMemberID.Text = "";
            this.txtName.Text = "";
            this.dtList.DataSource = null;
            this.txtMemberID.Focus();
            this.lblcount.Text = "Total Birds:";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmSyncAll syncAll = new frmSyncAll();
            this.Hide();
            syncAll.ClubName = ClubName;
            syncAll.DateRelease = this.dateTimePicker1.Value;
            syncAll.ActionType = "RESULTDB";
            syncAll.ActionTypeDescription = "Result";
            syncAll.ShowDialog();
            GetPigeonList(this.txtMemberID.Text);
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)this.dtList.DataSource;

            if (dt.Rows.Count > 0)
            {
                frmPrint print = new frmPrint();
                print.DataForPrint = dt;
                print.PlayerName = txtName.Text;
                print.ListType = "Pigeon Result";
                print.ShowDialog();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSyncEclock sync = new frmSyncEclock();
            sync.ClubName = ClubName;
            sync.MemberID = txtMemberID.Text;
            sync.DateRelease = this.dateTimePicker1.Value;
            sync.ActionType = "RESULTTRAINING";
            sync.ShowDialog();

            GetPigeonList(this.txtMemberID.Text);
        }
    }
}
