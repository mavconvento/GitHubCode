using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using PigeonIDSystem;
using System.IO;
using Helper;
using BusinessLayer;
using System.IO.Ports;
using System.Management;

namespace PigeonIDSystem
{
    public partial class frmPhotoCapture : Form
    {
        public Int64 PigeonID { get; set; }
        public String ActionType { get; set; }
        public String ClubName { get; set; }
        public String ActionOrigin { get; set; }
        public frmPhotoCapture()
        {
            InitializeComponent();
            dtList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBoxItem();
            ClubName = Common.GetClub();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            frmCapturePhoto capture = new frmCapturePhoto();
            capture.ShowDialog();

            Image photo = capture.PigeonPhoto;


            if (imgCapture.Image != null)
            {
                pictureBox1.Image = photo;
            }
            else
            {
                imgCapture.Image = photo;
            }
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            Common.SaveImageCapture(imgCapture.Image);
        }

        private void bntVideoFormat_Click(object sender, EventArgs e)
        {
            //webcam.ResolutionSetting();
        }

        private void bntVideoSource_Click(object sender, EventArgs e)
        {
            //webcam.AdvanceSetting();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
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
            this.txtRingNumber.Text = "";
            this.txtTestRing.Text = "";
            //this.cbmSex.SelectedIndex = -1;
            this.cmbCategory.SelectedIndex = -1;
            this.txtrfid.Text = "";
            //this.txtColor.Text = "";
            this.imgCapture.Image = null;
            this.pictureBox1.Image = null;
            this.dtList.DataSource = null;
            this.checkBox1.Checked = false;
            PigeonID = 0;
            ControlEnabled(false);
            this.txtMemberID.Focus();
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
                    int addcellindex = 1;
                    //PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    BandNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1 + addcellindex].Value);
                    RFID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[2 + addcellindex].Value);
                    string Category = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[3 + addcellindex].Value);
                    string Sex = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4 + addcellindex].Value);
                    string Color = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[5 + addcellindex].Value);
                    string IsRegistered = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[6 + addcellindex].Value);

                    if (BandNumber != "")
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                        {
                            ActionType = "EDIT";
                            txtRingNumber.Text = BandNumber;
                            txtColor.Text = Color;
                            int indexitem = -1;
                            if (Sex != "") indexitem = cbmSex.FindString(Sex);
                            cbmSex.SelectedIndex = indexitem;

                            if (IsRegistered == "YES") checkBox1.Checked = true;
                            else checkBox1.Checked = false;

                            //if (Category != "") indexitem = cmbCategory.FindString(Category);
                            //cmbCategory.SelectedIndex = indexitem;

                            txtrfid.Text = RFID;
                            //LoadImage(RFID, txtMemberID.Text);
                            //txtRingNumber.Enabled = false;
                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                        {
                            if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                            {

                                ActionType = "DELETE";
                                string path = ReadText.ReadFilePath("datapath");
                                string[] pigeonList = SetPigeonList(ActionType, "0", BandNumber, "", "", RFID, "").ToArray();
                                System.IO.File.WriteAllLines(path + "\\pigeonlist\\" + txtMemberID.Text + ".txt", pigeonList);
                                GetPigeonList(txtMemberID.Text);

                                string deletePath = path + "\\pigeondetails\\" + txtMemberID.Text + "\\" + RFID + ".txt";
                                if (File.Exists(deletePath)) File.Delete(deletePath);

                                ActionType = "";
                                //ClearControl();
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

        private void ComboBoxItem()
        {
            try
            {
                ComboboxItem hen = new ComboboxItem();
                hen.Text = "HEN";
                hen.Value = "HEN";
                cbmSex.Items.Add(hen);

                ComboboxItem cock = new ComboboxItem();
                cock.Text = "COCK";
                cock.Value = "COCK";
                cbmSex.Items.Add(cock);

                ComboboxItem na = new ComboboxItem();
                na.Text = "NA";
                na.Value = "NA";
                cbmSex.Items.Add(na);

                //cbmSex.SelectedIndex = -1;

                ComboboxItem yb = new ComboboxItem();
                yb.Text = "YB";
                yb.Value = "YB";
                cmbCategory.Items.Add(yb);

                ComboboxItem ob = new ComboboxItem();
                ob.Text = "OB";
                ob.Value = "OB";
                cmbCategory.Items.Add(ob);

                ComboboxItem both = new ComboboxItem();
                both.Text = "BOTH";
                both.Value = "BOTH";
                cmbCategory.Items.Add(both);

                cmbCategory.SelectedIndex = -1;
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
                Boolean status = PigeonSave(this.txtRingNumber.Text, this.txtMemberID.Text, this.checkBox1.Checked, this.txtrfid.Text);

                if (status)
                {
                    MessageBox.Show("Record Save.", "Save");
                    GetPigeonList(this.txtMemberID.Text);

                    //clear control
                    this.txtRingNumber.Text = "";
                    this.txtrfid.Text = "";
                    this.cbmSex.SelectedIndex = -1;
                    this.cmbCategory.SelectedIndex = -1;
                    this.txtColor.Text = "";
                    this.PigeonID = 0;
                    this.imgCapture.Image = null;
                    this.pictureBox1.Image = null;
                    this.checkBox1.Checked = false;
                    this.ActionType = "";
                    txtRingNumber.Enabled = true;
                    this.txtRingNumber.Focus();
                }
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private bool PigeonSave(string pigeonId, string memberId, bool isRegistered, string ering)
        {
            try
            {
                MemberBLL memberBLL = new MemberBLL();
                memberBLL.MemberSave(memberId, this.checkBox2.Checked);

                if (pigeonId != "") memberBLL.PigeonSave(pigeonId, memberId, isRegistered, ering);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Boolean SavetoFile(string MemberIDNo, string MemberName, Int64 PigeonID, string BandNumber, string Sex, string Color, PictureBox Photo1, PictureBox Photo2, string RFID, String category, string TestRing)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                string detailspath = path + "\\pigeondetails\\" + MemberIDNo + "\\" + RFID + ".txt";
                string memberpath = path + "\\members\\" + MemberIDNo + ".txt";
                string pigeonlistpath = path + "\\pigeonlist\\" + MemberIDNo + ".txt";

                if (ActionType != "EDIT" && File.Exists(detailspath))
                {
                    MessageBox.Show("Tag already assign to other bird.", "Error");
                    return false;
                }


                string[] memberDetails = { MemberIDNo, MemberName, TestRing };
                string[] pigeonDetails = { BandNumber, RFID, category, Sex, Color };

                string[] pigeonList = SetPigeonList(ActionType, PigeonID.ToString(), BandNumber, Sex, Color, RFID, category).ToArray();

                System.IO.File.WriteAllLines(memberpath, memberDetails); //memberdetails
                System.IO.File.WriteAllLines(pigeonlistpath, pigeonList); //memberpigeonlist

                string pigeondetailsDirectory = path + "\\pigeondetails\\" + MemberIDNo;
                if (!Directory.Exists(pigeondetailsDirectory))
                {
                    Directory.CreateDirectory(pigeondetailsDirectory);
                }
                System.IO.File.WriteAllLines(detailspath, pigeonDetails); //pigeondetails


                string pigeonimageDirectory = path + "\\images\\" + MemberIDNo;
                if (!Directory.Exists(pigeonimageDirectory))
                {
                    Directory.CreateDirectory(pigeonimageDirectory);
                }

                if (Photo1.Image != null)
                {
                    string filename = pigeonimageDirectory + "\\" + RFID + ".txt";
                    File.WriteAllBytes(filename, GetPhoto(imgCapture));
                }

                if (Photo2.Image != null)
                {
                    string filename = pigeonimageDirectory + "\\" + RFID + "_P2" + ".txt";
                    File.WriteAllBytes(filename, GetPhoto(pictureBox1));
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetPigeonList(string memberid)
        {
            try
            {
                DataTable pigeonList = new DataTable();

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "EDIT";

                //DataColumn dc2 = new DataColumn();
                //dc2.ColumnName = "DELETE";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "SeqNo";

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

                DataColumn dc9 = new DataColumn();
                dc9.ColumnName = "IsRegistered";

                pigeonList.Columns.Add(dc1);
                //pigeonList.Columns.Add(dc2);
                pigeonList.Columns.Add(dc3);
                pigeonList.Columns.Add(dc4);
                pigeonList.Columns.Add(dc5);
                pigeonList.Columns.Add(dc6);
                pigeonList.Columns.Add(dc7);
                pigeonList.Columns.Add(dc8);
                pigeonList.Columns.Add(dc9);

                //add test ring on the top list
                //DataRow drtest = pigeonList.NewRow();
                //drtest["EDIT"] = "";
                //drtest["DELETE"] = "";
                //drtest["SeqNo"] = 1.ToString();
                //drtest["BandNumber"] = "TEST RING";
                //drtest["TagID"] = this.txtTestRing.Text;
                //drtest["Category"] = "BOTH";
                //drtest["Color"] = "NA";
                //drtest["Sex"] = "NA";
                //pigeonList.Rows.Add(drtest);


                //get record from toppigeon database
                MemberBLL memberBLL = new MemberBLL();
                DataTable pigeons = new DataTable();
                pigeons = memberBLL.GetPigeonList(memberid);

                //string path = ReadText.ReadFilePath("datapath");
                //string filepath = path + "\\pigeonlist\\" + memberid + ".txt";
                //if (File.Exists(filepath))
                //{
                //string[] pigeonCollection = ReadText.ReadTextFile(filepath);
                int seqNumber = 1;
                foreach (DataRow value in pigeons.Rows)
                {
                    //string[] value = item.Split('|');
                    DataRow dr = pigeonList.NewRow();
                    dr["EDIT"] = "EDIT";
                    //dr["DELETE"] = "DELETE";
                    dr["SeqNo"] = seqNumber.ToString();
                    dr["BandNumber"] = value["PRingNo"].ToString();
                    dr["TagID"] = value["E_ring"].ToString();
                    dr["Category"] = "NA";
                    dr["Color"] = value["ColorType"].ToString();
                    dr["Sex"] = value["Sex"].ToString();

                    if (value["IsRegistered"].ToString() == "1")
                        dr["IsRegistered"] = "YES";
                    else
                        dr["IsRegistered"] = "";

                    pigeonList.Rows.Add(dr);
                    seqNumber++;
                }

                //}

                if (ActionOrigin == "Search" && pigeons.Rows.Count > 0)
                {
                    DataRow[] drcol = pigeonList.Select().OrderBy(u => u["BandNumber"]).ToArray();
                    pigeonList = drcol.CopyToDataTable();
                    ActionOrigin = "";
                }

                dtList.DataSource = pigeonList;
                lblcount.Text = "Total Birds: " + pigeonList.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<string> SetPigeonList(string action, string PigeonID, string BandNumber, string Sex, string Color, string RFID, String Category)
        {
            try
            {
                DataGridView dv = new DataGridView();
                int counter = 0;
                dv = this.dtList;

                List<string> pigeonListCollection = new List<string>();
                foreach (DataGridViewRow item in dv.Rows)
                {
                    if (item.Cells["BandNumber"].Value.ToString() != "TEST RING")
                    {
                        if (action == "EDIT")
                        {
                            //if (item.Index.ToString() == PigeonID)
                            if (item.Cells["BandNumber"].Value.ToString() == BandNumber)
                            {
                                pigeonListCollection.Add(item.Index.ToString() + "|" + BandNumber + "|" + RFID + "|" + Category + "|" + Color + "|" + Sex);
                            }
                            else
                            {
                                pigeonListCollection.Add(item.Index.ToString() + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["TagID"].Value + "|" + item.Cells["Category"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                            }
                        }
                        else if (action == "DELETE")
                        {
                            if (item.Cells["BandNumber"].Value.ToString() != BandNumber)
                            {
                                pigeonListCollection.Add(counter + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["TagID"].Value + "|" + item.Cells["Category"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                                counter++;
                            }
                        }
                        else
                        {
                            pigeonListCollection.Add(item.Index.ToString() + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["TagID"].Value + "|" + item.Cells["Category"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                        }
                    }
                }

                if (string.IsNullOrEmpty(action))
                {
                    pigeonListCollection.Add(dv.Rows.Count + "|" + BandNumber + "|" + RFID + "|" + Category + "|" + Color + "|" + Sex);
                }

                return pigeonListCollection;
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
                string path = ReadText.ReadFilePath("datapath") + "\\images\\" + memberid + "\\";
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

        private Image LoadImage(byte[] image)
        {
            try
            {
                MemoryStream ms = new MemoryStream(image);
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetPigeonList_SQL(string memberID)
        {
            try
            {
                DataSet dsResult = new DataSet();
                if (dsResult.Tables.Count > 0)
                {
                    dtList.DataSource = dsResult.Tables[0];
                    lblcount.Text = "Total Birds: " + dsResult.Tables[0].Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool GetMemberName(string memberID)
        {
            try
            {

                //get record from toppigeon db
                MemberBLL memberBLL = new MemberBLL();
                DataTable dt = new DataTable();
                dt = memberBLL.GetMemberName(memberID);

                if (dt.Rows.Count > 0)
                {
                    this.txtName.Text = dt.Rows[0]["LoftName"].ToString();
                    this.txtTestRing.Text = dt.Rows[0]["ClockId"].ToString();

                    if (dt.Rows[0]["IsMultiple"].ToString() == "1") this.checkBox2.Checked = true;
                    else this.checkBox2.Checked = false;

                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private byte[] GetPhoto(PictureBox pb)
        {
            try
            {
                Image img = pb.Image;
                byte[] arr;

                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

                return arr;
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
                if (this.txtMemberID.Text != "")
                {
                    
                    ActionOrigin = "Search";
                    if (GetMemberName(this.txtMemberID.Text))
                    {
                        GetPigeonList(this.txtMemberID.Text);
                        ControlEnabled(true);
                        this.txtName.Focus();
                    }
                    else
                        MessageBox.Show("MemberID Not Found in TOP-Pigeon.", "Error");
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
        private void ControlEnabled(Boolean value)
        {
            try
            {
                //this.txtMobileNumber.Enabled = value;
                this.txtMemberID.Enabled = !value;
                this.txtName.Enabled = value;
                this.txtTestRing.Enabled = value;
                this.btnTestReadTags.Enabled = value;
                this.txtRingNumber.Enabled = value;
                this.cbmSex.Enabled = value;
                this.cmbCategory.Enabled = value;
                this.txtColor.Enabled = value;
                this.txtrfid.Enabled = value;
                this.btnRead.Enabled = value;
                this.btnSync.Enabled = value;
                this.checkBox1.Enabled = value;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;
                frmPrint print = new frmPrint();

                if (dt.Rows.Count > 0)
                {
                    print.DataForPrint = dt;
                    print.PlayerName = txtName.Text;
                    print.ListType = "Banded Pigeon";
                    print.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.imgCapture.Image = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                frmSyncAll sync = new frmSyncAll();
                sync.ClubName = ClubName;
                sync.ActionType = "TOPPIGEONPIGDATA";
                sync.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;

                frmSyncEclock sync = new frmSyncEclock();
                sync.DataList = dt;
                sync.ClubName = ClubName;
                sync.DataStartIndex = 1;
                sync.DataEndtIndex = dt.Rows.Count;
                sync.ActionType = "BANDED";
                sync.ShowDialog();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void cbmSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void txtMemberID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.txtrfid.Focus();
            frmReadRFID readRFID = new frmReadRFID();
            readRFID.ShowDialog();
            this.txtrfid.Text = readRFID.RFIDTags;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)this.dtList.DataSource;

            frmSyncEclock sync = new frmSyncEclock();
            sync.DataList = dt;
            sync.ClubName = ClubName;
            sync.ActionType = "CLOCK";
            sync.ShowDialog();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            frmAssignMobileNumber assignMobileNumber = new frmAssignMobileNumber();
            assignMobileNumber.MemberID = txtMemberID.Text;
            assignMobileNumber.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            frmRegisterRFID registerRFID = new frmRegisterRFID();
            registerRFID.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)this.dtList.DataSource;

            frmSyncEclock sync = new frmSyncEclock();
            sync.DataList = dt;
            sync.ClubName = ClubName;
            sync.ActionType = "RESET";
            sync.ShowDialog();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.txtTestRing.Focus();
            frmReadRFID readRFID = new frmReadRFID();
            readRFID.ShowDialog();
            this.txtTestRing.Text = readRFID.RFIDTags;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;

                frmSyncEclock sync = new frmSyncEclock();
                sync.DataList = dt;
                sync.ClubName = ClubName;
                sync.MemberID = this.txtMemberID.Text;
                sync.MemberName = this.txtName.Text;
                sync.TestRing = this.txtTestRing.Text;

                sync.ActionType = "READBANDED";
                sync.ShowDialog();
                GetPigeonList(this.txtMemberID.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
