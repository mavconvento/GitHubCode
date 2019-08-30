using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace PigeonIDSystem
{
    public partial class frmEntry : Form
    {
        public String ClubName { get; set; }
        public Int64 PigeonID { get; set; }
        public String ActionType { get; set; }
        public String EntryList { get; set; }
        public frmEntry()
        {
            InitializeComponent();
            dtList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Entry_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Focus();
            ClubName = Common.GetClub();
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

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtList;
                int index;
                //Int64 PigeonID;
                String BandNumber = "";
                String RFID = "";
                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;
                    //PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    BandNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    RFID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    string Category = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                    string Sex = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);
                    string Color = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);

                    if (BandNumber != "")
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                        {
                            if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                            {
                                ActionType = "DELETE";
                                string path = ReadText.ReadFilePath("datapath");
                                string[] pigeonList = SetPigeonList(ActionType, BandNumber, RFID).ToArray();

                                string dateString = this.dateTimePicker1.Value.Year.ToString() + this.dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + this.dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');
                                string entryDirectory = path + "\\entry\\" + dateString;
                                string filepath = entryDirectory + "\\" + txtMemberID.Text + ".txt";

                                if (File.Exists(filepath))
                                {
                                    File.Delete(filepath);
                                }

                                System.IO.File.WriteAllLines(filepath, pigeonList);
                                GetPigeonList(txtMemberID.Text);
                                ActionType = "";
                                this.btnRead.Focus();
                                MessageBox.Show("Record Deleted.", "Delete Record");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private List<string> SetPigeonList(string action, string BandNumber, string RFID)
        {
            try
            {
                DataGridView dv = new DataGridView();
                int counter = 0;
                dv = this.dtList;

                List<string> pigeonListCollection = new List<string>();
                foreach (DataGridViewRow item in dv.Rows)
                {
                    if (action == "DELETE")
                    {
                        if (item.Cells["BandNumber"].Value.ToString() != BandNumber)
                        {
                            pigeonListCollection.Add(item.Cells["TagID"].Value.ToString());
                            counter++;
                        }
                    }
                    else
                    {
                        pigeonListCollection.Add(item.Cells["TagID"].Value.ToString());
                    }
                }

                if (string.IsNullOrEmpty(action))
                {
                    pigeonListCollection.Add(RFID);
                }

                return pigeonListCollection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ControlState(bool value)
        {
            try
            {
                this.txtrfid.Enabled = value;
                this.txtMemberID.Enabled = value;
                this.btnRead.Enabled = value;
                this.btnSync.Enabled = value;
                this.dateTimePicker1.Enabled = !value;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LoadImage(string rfid, string memberid)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath") + "images\\" + memberid + "\\";
                if (File.Exists(path + rfid + ".txt"))
                {
                    string filename = path + rfid + ".txt";
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(filename));
                    imgCapture.Image = Image.FromStream(ms);
                }

                if (File.Exists(path + rfid + "_P2" + ".txt"))
                {
                    string filename = path + rfid + "_P2" + ".txt";
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(filename));
                    pictureBox1.Image = Image.FromStream(ms);

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

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "DELETE";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "SeqID";

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

                //pigeonList.Columns.Add(dc1);
                pigeonList.Columns.Add(dc2);
                pigeonList.Columns.Add(dc3);
                pigeonList.Columns.Add(dc4);
                pigeonList.Columns.Add(dc5);
                pigeonList.Columns.Add(dc6);
                pigeonList.Columns.Add(dc7);
                pigeonList.Columns.Add(dc8);

                string path = ReadText.ReadFilePath("datapath");
                string dateString = this.dateTimePicker1.Value.Year.ToString() + this.dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + this.dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');

                string entryDirectory = path + "\\entry\\" + dateString;
                string filepath = entryDirectory + "\\" + memberid + ".txt";

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
                        dr["DELETE"] = "DELETE";
                        dr["SeqID"] = counter;
                        dr["BandNumber"] = pigeonDetailsCollection[0].ToString();
                        dr["TagID"] = pigeonDetailsCollection[1].ToString();
                        dr["Category"] = pigeonDetailsCollection[2].ToString();
                        dr["Color"] = pigeonDetailsCollection[4].ToString();
                        dr["Sex"] = pigeonDetailsCollection[3].ToString();

                        EntryList = EntryList == "" ? pigeonDetailsCollection[1].ToString() : EntryList + "|" + pigeonDetailsCollection[1].ToString();
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

        private void txtrfid_TextChanged(object sender, EventArgs e)
        {
            if (this.txtrfid.Text.Length == 8)
            {
                GetDetails();
            }
        }

        private void GetDetails()
        {
            string path = ReadText.ReadFilePath("datapath");
            // string filepath = path + "\\ClubInfo.txt";

            string birdDetailsPath = path + "\\PigeonDetails\\" + txtMemberID.Text + "\\" + txtrfid.Text + ".txt";

            if (EntryList != "" && EntryList.Contains(txtrfid.Text))
            {
                MessageBox.Show("Bird Already Entry.", "Information");
                this.txtrfid.Text = "";
            }
            else
            {
                if (File.Exists(birdDetailsPath))
                {
                    string[] pigeonDetailsCollection = ReadText.ReadTextFile(birdDetailsPath);
                    txtRingNumber.Text = pigeonDetailsCollection[0];
                    txtSex.Text = pigeonDetailsCollection[3];
                    txtColor.Text = pigeonDetailsCollection[4];
                    txtCategory.Text = pigeonDetailsCollection[2];
                    LoadImage(txtrfid.Text, txtMemberID.Text);
                    Save();
                }
                else
                {
                    MessageBox.Show("Bird not found!", "Error");
                    this.txtrfid.Text = "";
                    this.txtrfid.Focus();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMemberID.Text != "")
                {
                    EntryList = "";
                    ControlState(true);
                    GetPigeonList(this.txtMemberID.Text);
                    GetMemberName(this.txtMemberID.Text);
                    this.txtrfid.Focus();
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

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Save()
        {
            try
            {
                if (txtMemberID.Text == "" || txtrfid.Text == "")
                {
                    MessageBox.Show("Bird not banded.", "Error");
                }
                else
                {
                    Boolean status = SavetoFile(txtMemberID.Text, txtrfid.Text);

                    if (status)
                    {
                        MessageBox.Show("Record Save.", "Save");
                        GetPigeonList(this.txtMemberID.Text);
                        //clear control
                        this.txtRingNumber.Text = "";
                        this.txtrfid.Text = "";
                        this.txtColor.Text = "";
                        this.txtSex.Text = "";
                        this.txtCategory.Text = "";
                        this.imgCapture.Image = null;
                        this.pictureBox1.Image = null;
                        txtRingNumber.Enabled = true;
                        this.txtrfid.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private Boolean SavetoFile(string MemberIDNo, string RFID)
        {
            try
            {
                // string[] pigeonEntry = {RFID};

                string path = ReadText.ReadFilePath("datapath");
                string dateString = this.dateTimePicker1.Value.Year.ToString() + this.dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + this.dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');

                string entryDirectory = path + "\\entry\\" + dateString;
                string filepath = entryDirectory + "\\" + MemberIDNo + ".txt";

                string[] pigeonEntry = SetPigeonList(ActionType, this.txtRingNumber.Text, RFID).ToArray();

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                if (!Directory.Exists(entryDirectory))
                {
                    Directory.CreateDirectory(entryDirectory);
                }

                if (File.Exists(filepath))
                {
                    string[] entryList = ReadText.ReadTextFile(filepath);
                    if (entryList.Contains(RFID))
                    {
                        MessageBox.Show("Bird Already Entry.");
                        return false;
                    }
                    else
                    {
                        System.IO.File.WriteAllLines(filepath, pigeonEntry); //entrylist
                    }
                }
                else
                {
                    System.IO.File.WriteAllLines(filepath, pigeonEntry); //entrylist
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
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
            this.txtRingNumber.Text = "";
            this.txtrfid.Text = "";
            this.txtColor.Text = "";
            this.txtSex.Text = "";
            this.txtCategory.Text = "";
            this.txtMemberID.Text = "";
            this.imgCapture.Image = null;
            this.txtName.Text = "";
            this.pictureBox1.Image = null;
            this.dtList.DataSource = null;
            txtRingNumber.Enabled = true;
            //ControlState(false);
            this.txtMemberID.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;

                frmSyncEclock sync = new frmSyncEclock();
                sync.DataList = dt;
                sync.ClubName = ClubName;
                sync.ActionType = "ENTRY";
                sync.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtrfid.Focus();
                frmReadRFID readRFID = new frmReadRFID();
                readRFID.ShowDialog();
                this.txtrfid.Text = readRFID.RFIDTags;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmSyncAll syncAll = new frmSyncAll();
            this.Hide();
            syncAll.ClubName = ClubName;
            syncAll.DateRelease = this.dateTimePicker1.Value;
            syncAll.ActionType = "ENTRYDB";
            syncAll.ActionTypeDescription = "Entry";
            syncAll.ShowDialog();
            this.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;

                frmSyncEclock sync = new frmSyncEclock();
                sync.DataList = dt;
                sync.ClubName = ClubName;
                sync.ActionType = "UNLOCK";
                sync.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
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
                print.ListType = "Pigeon Entry";
                print.ShowDialog();
            }

        }
    }
}
