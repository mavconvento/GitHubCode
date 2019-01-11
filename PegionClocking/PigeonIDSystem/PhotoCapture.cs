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

namespace PigeonIDSystem
{
    public partial class PhotoCapture : Form
    {
        WebCam webcam;
        public Int64 PigeonID { get; set; }
        public String ActionType { get; set; }
        public PhotoCapture()
        {
            InitializeComponent();
            dtList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            webcam.Start();
            ComboBoxItem();
            CreateStorageFolder();
        }

        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            if (imgCapture.Image != null)
            {
                pictureBox1.Image = imgVideo.Image;
            }
            else
            {
                imgCapture.Image = imgVideo.Image;
            }
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            Helper.SaveImageCapture(imgCapture.Image);
        }

        private void bntVideoFormat_Click(object sender, EventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void bntVideoSource_Click(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //clear control
                this.txtMemberID.Text = "";
                this.txtName.Text = "";
                this.txtRingNumber.Text = "";
                this.cbmSex.SelectedIndex = -1;
                this.txtColor.Text = "";
                this.imgCapture.Image = null;
                this.pictureBox1.Image = null;
                this.dtList.DataSource = null;
                PigeonID = 0;
                ControlEnabled(false);
                this.txtMemberID.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateStorageFolder()
        {
            try
            {
                string images = @"C:\PigeonIDSystem\Images";
                if (!Directory.Exists(images))
                {
                    Directory.CreateDirectory(images);
                }

                string members = @"C:\PigeonIDSystem\Members";
                if (!Directory.Exists(members))
                {
                    Directory.CreateDirectory(members);
                }

                string pigeonDetails = @"C:\PigeonIDSystem\PigeonDetails";
                if (!Directory.Exists(pigeonDetails))
                {
                    Directory.CreateDirectory(pigeonDetails);
                }

                string pigeonList = @"C:\PigeonIDSystem\PigeonList";
                if (!Directory.Exists(pigeonList))
                {
                    Directory.CreateDirectory(pigeonList);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtList;
                int index;
                Int64 PigeonID;
                String BandNumber = "";
                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;
                    PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    BandNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                    string Sex = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);
                    string Color = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);

                    //if (PigeonID > 0)
                    //{
                    //    DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();

                    //    DataSet dsResult = new DataSet();
                    //    member.GetPigeonDetails("local", PigeonID);
                    //    index = 0;
                    //    if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    //    {
                    //        if (dsResult.Tables.Count > 0)
                    //        {
                    //            if (dsResult.Tables[0].Rows.Count > 0)
                    //            {
                    //                ActionType = "EDIT";
                    //                txtRingNumber.Text = BandNumber;
                    //                txtColor.Text = Color;
                    //                int indexitem = -1;
                    //                if (Sex != "") indexitem = cbmSex.FindString(Sex);
                    //                cbmSex.SelectedIndex = indexitem;
                    //                LoadImage(BandNumber);
                    //                txtRingNumber.Enabled = false;
                    //            }
                    //        }
                    //    }
                    //member = new BIZ.Member();
                    //index = datagrid.CurrentRow.Index;
                    //PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    if (BandNumber != "")
                    {
                        DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();

                        DataSet dsResult = new DataSet();
                        member.GetPigeonDetails("local", PigeonID);
                        if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                        {
                            dsResult = member.GetPigeonDetails("local", PigeonID);
                            if (dsResult.Tables.Count > 0)
                            {
                                if (dsResult.Tables[0].Rows.Count > 0)
                                {
                                    ActionType = "EDIT";
                                    txtRingNumber.Text = dsResult.Tables[0].Rows[0]["BandNumber"].ToString();
                                    txtColor.Text = dsResult.Tables[0].Rows[0]["Color"].ToString();
                                    int indexitem = -1;
                                    if (dsResult.Tables[0].Rows[0]["Sex"].ToString() != "") indexitem = cbmSex.FindString(dsResult.Tables[0].Rows[0]["Sex"].ToString());
                                    cbmSex.SelectedIndex = indexitem;
                                    if (dsResult.Tables[0].Rows[0]["Photo"].ToString() != null)
                                    {
                                        this.imgCapture.Image = LoadImage((byte[])dsResult.Tables[0].Rows[0]["Photo"]);
                                        //LoadImage(dsResult.Tables[0].Rows[0]["BandNumber"].ToString());
                                    }
                                }
                            }


                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                        {
                            if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                            {
                                ActionType = "DELETE";

                                string path = ReadText.ReadFilePath("datapath");
                                string[] pigeonList = SetPigeonList(ActionType, PigeonID.ToString(), BandNumber, "", "").ToArray();
                                System.IO.File.WriteAllLines(path + "pigeonlist\\" + txtMemberID.Text + ".txt", pigeonList);

                                GetPigeonList(txtMemberID.Text);
                                ActionType = "";
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

                cbmSex.SelectedIndex = -1;
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
                if (txtMemberID.Text == "" || txtName.Text == "" || txtRingNumber.Text == "" || imgCapture.Image == null)
                {
                    MessageBox.Show("Please filled-up required field.", "Error");
                }
                else
                {
                    Boolean status = SavetoFile(txtMemberID.Text, txtName.Text, PigeonID, txtRingNumber.Text, cbmSex.Text, txtColor.Text, imgCapture, pictureBox1);
                    MessageBox.Show("Record Save.", "Save");
                    GetPigeonList(this.txtMemberID.Text);

                    //clear control
                    this.txtRingNumber.Text = "";
                    this.cbmSex.SelectedIndex = -1;
                    this.txtColor.Text = "";
                    this.PigeonID = 0;
                    this.imgCapture.Image = null;
                    this.pictureBox1.Image = null;
                    this.ActionType = "";
                    txtRingNumber.Enabled = true;
                    this.txtRingNumber.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private Boolean SavetoFile(string MemberIDNo, string MemberName, Int64 PigeonID, string BandNumber, string Sex, string Color, PictureBox Photo1, PictureBox Photo2)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
               
                string[] memberDetails = { MemberIDNo, MemberName, };
                string[] pigeonDetails = { BandNumber, Sex, Color };

                string[] pigeonList = SetPigeonList(ActionType, PigeonID.ToString(), BandNumber, Sex, Color).ToArray();
                System.IO.File.WriteAllLines(path + "members\\" + MemberIDNo + ".txt", memberDetails); //memberdetails
                System.IO.File.WriteAllLines(path + "pigeonlist\\" + MemberIDNo + ".txt", pigeonList); //memberpigeonlist

                string pigeondetailsDirectory = path + "pigeondetails\\" + MemberIDNo;
                if (!Directory.Exists(pigeondetailsDirectory))
                {
                    Directory.CreateDirectory(pigeondetailsDirectory);
                }
                System.IO.File.WriteAllLines(path + "pigeondetails\\" + MemberIDNo + "\\" + BandNumber + ".txt", pigeonDetails); //pigeondetails


                string pigeonimageDirectory = path + "images\\" + MemberIDNo;
                if (!Directory.Exists(pigeonimageDirectory))
                {
                    Directory.CreateDirectory(pigeonimageDirectory);
                }

                if (Photo1.Image != null)
                {
                    string filename = pigeonimageDirectory + "\\" + BandNumber + ".txt";
                    File.WriteAllBytes(filename, GetPhoto(imgCapture));
                }

                if (Photo2.Image != null)
                {
                    string filename = pigeonimageDirectory + "\\" + BandNumber + "_P2" + ".txt";
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
                DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();
                DataSet dsResult = new DataSet();
                dsResult = member.GetAllPigeonDetails("local", this.txtMemberID.Text);
                if (dsResult.Tables.Count > 0)
                {
                    dtList.DataSource = dsResult.Tables[0];
                    lblcount.Text = "Total Birds: " + dtList.Rows.Count.ToString();
                }

                //DataTable pigeonList = new DataTable();

                //DataColumn dc1 = new DataColumn();
                //dc1.ColumnName = "EDIT";

                //DataColumn dc2 = new DataColumn();
                //dc2.ColumnName = "DELETE";

                //DataColumn dc3 = new DataColumn();
                //dc3.ColumnName = "PigeonID";

                //DataColumn dc4 = new DataColumn();
                //dc4.ColumnName = "BandNumber";

                //DataColumn dc5 = new DataColumn();
                //dc5.ColumnName = "Sex";

                //DataColumn dc6 = new DataColumn();
                //dc6.ColumnName = "Color";

                //pigeonList.Columns.Add(dc1);
                //pigeonList.Columns.Add(dc2);
                //pigeonList.Columns.Add(dc3);
                //pigeonList.Columns.Add(dc4);
                //pigeonList.Columns.Add(dc5);
                //pigeonList.Columns.Add(dc6);

                //string path = ReadText.ReadFilePath("datapath");
                //string filepath = path + "pigeonlist\\" + memberid + ".txt";
                //if (File.Exists(filepath))
                //{
                //    string[] pigeonCollection = ReadText.ReadTextFile(filepath);

                //    foreach (string item in pigeonCollection)
                //    {
                //        string[] value = item.Split('|');
                //        DataRow dr = pigeonList.NewRow();
                //        dr["EDIT"] = "EDIT";
                //        dr["DELETE"] = "DELETE";
                //        dr["PigeonID"] = value[0].ToString();
                //        dr["BandNumber"] = value[1].ToString();
                //        dr["Sex"] = value[2].ToString();
                //        dr["Color"] = value[3].ToString();

                //        pigeonList.Rows.Add(dr);
                //    }

                //}

                //if (pigeonList.Rows.Count > 0)
                //{
                //    dtList.DataSource = pigeonList;
                //    lblcount.Text = "Total Birds: " + pigeonList.Rows.Count.ToString();
                //}

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<string> SetPigeonList(string action, string PigeonID, string BandNumber, string Sex, string Color)
        {
            try
            {
                DataGridView dv = new DataGridView();
                int counter = 0;
                dv = this.dtList;

                List<string> pigeonListCollection = new List<string>();
                foreach (DataGridViewRow item in dv.Rows)
                {
                    if (action == "EDIT")
                    {
                        //if (item.Index.ToString() == PigeonID)
                        if (item.Cells["BandNumber"].Value.ToString() == BandNumber)
                        {
                            pigeonListCollection.Add(item.Index.ToString() + "|" + BandNumber + "|" + Color + "|" + Sex);
                        }
                        else
                        {
                            pigeonListCollection.Add(item.Index.ToString() + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                        }
                    }
                    else if (action == "DELETE")
                    {
                        if (item.Cells["BandNumber"].Value.ToString() != BandNumber)
                        {
                            pigeonListCollection.Add(counter + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                            counter++;
                        }
                    }
                    else
                    {
                        pigeonListCollection.Add(item.Index.ToString() + "|" + item.Cells["BandNumber"].Value + "|" + item.Cells["Color"].Value + "|" + item.Cells["Sex"].Value);
                    }
                }

                if (action == "")
                {
                    pigeonListCollection.Add(dv.Rows.Count + "|" + BandNumber + "|" + Color + "|" + Sex);
                }
                return pigeonListCollection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LoadImage(string bandnumber)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                if (File.Exists(path + bandnumber + ".txt"))
                {
                    string filename = path + bandnumber + ".txt";
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(filename));
                    imgCapture.Image = Image.FromStream(ms);
                   
                    //imgCapture.Image = photo;
                }

                if (File.Exists(path + bandnumber + "_P2" + ".jpeg"))
                {
                    Image photo = Image.FromFile(path + bandnumber + "_P2" + ".jpeg");
                    pictureBox1.Image = photo;
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
                //photo_aray = (byte[])drow[4];
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
                DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();
                DataSet dsResult = new DataSet();
                dsResult = member.GetAllPigeonDetails("local", this.txtMemberID.Text);
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
        private void GetMemberName(string memberID)
        {
            try
            {
                DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();
                DataSet dsResult = new DataSet();
                dsResult = member.GetMemberDetails("local", this.txtMemberID.Text);
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.txtName.Text = dsResult.Tables[0].Rows[0]["MemberName"].ToString();
                    }
                }

                //string path = ReadText.ReadFilePath("datapath");
                //string filepath = path + "members\\" + memberID + ".txt";

                //if (File.Exists(filepath))
                //{
                //    string[] memberDetails = ReadText.ReadTextFile(filepath);
                //    this.txtName.Text = memberDetails[1].ToString();
                //}

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
                    ControlEnabled(true);
                    GetPigeonList(this.txtMemberID.Text);
                    GetMemberName(this.txtMemberID.Text);
                    this.txtName.Focus();
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
                this.txtMemberID.Enabled = !value;
                this.txtName.Enabled = value;
                this.txtRingNumber.Enabled = value;
                this.cbmSex.Enabled = value;
                this.txtColor.Enabled = value;
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
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtList.DataSource;
                if (dt.Rows.Count > 0)
                {
                    reportGeneration.MemberID = txtMemberID.Text.ToUpper();
                    reportGeneration.MemberName = txtName.Text.ToUpper();
                    reportGeneration.Type = "PigeonList";
                    reportGeneration.dtRecord = dt;
                    reportGeneration.ShowDialog();
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
