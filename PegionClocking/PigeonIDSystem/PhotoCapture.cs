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

namespace PigeonIDSystem
{
    public partial class PhotoCapture : Form
    {
        WebCam webcam;
        public Int64 PigeonID { get; set; }
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
            imgCapture.Image = imgVideo.Image;
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
                PigeonID = 0;
                ControlEnabled(false);
                this.txtMemberID.Focus();
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
                Int64 index;
                Int64 PigeonID;

                if (datagrid.RowCount > 0)
                {
                    //member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    if (PigeonID > 0)
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
                                    txtRingNumber.Text = dsResult.Tables[0].Rows[0]["BandNumber"].ToString();
                                    txtColor.Text = dsResult.Tables[0].Rows[0]["Color"].ToString();
                                    int indexitem = -1;
                                    if (dsResult.Tables[0].Rows[0]["Sex"].ToString() != "") indexitem = cbmSex.FindString(dsResult.Tables[0].Rows[0]["Sex"].ToString());
                                    cbmSex.SelectedIndex = indexitem;
                                    if (dsResult.Tables[0].Rows[0]["Photo"].ToString() != null)
                                    {
                                        this.imgCapture.Image = LoadImage((byte[])dsResult.Tables[0].Rows[0]["Photo"]);
                                    }
                                }
                            }


                        }
                        else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                        {
                            if ((MessageBox.Show("Are you sure! You would like to delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
                            {
                                member.DeletePigeon("local", PigeonID);
                                GetPigeonList(txtMemberID.Text);
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
                    DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();
                    DataSet dtset = new DataSet();
                    dtset = member.MemberSave("local", txtMemberID.Text, txtName.Text, PigeonID, txtRingNumber.Text, cbmSex.Text, txtColor.Text, GetPhoto());
                    MessageBox.Show("Record Save.", "Save");
                    GetPigeonList(this.txtMemberID.Text);

                    //clear control
                    this.txtRingNumber.Text = "";
                    this.cbmSex.SelectedIndex = -1;
                    this.txtColor.Text = "";
                    this.PigeonID = 0;
                    this.imgCapture.Image = null;
                    this.txtRingNumber.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
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
        private void GetPigeonList(string memberID)
        {
            try
            {
                DataAccess.PigeonIDSystem.Member member = new DataAccess.PigeonIDSystem.Member();
                DataSet dsResult = new DataSet();
                dsResult = member.GetAllPigeonDetails("local", this.txtMemberID.Text);
                if (dsResult.Tables.Count > 0)
                {
                    dtList.DataSource = dsResult.Tables[0];
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
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private byte[] GetPhoto()
        {
            try
            {
                Image img = imgCapture.Image;
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
