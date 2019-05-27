using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PigeonProgram
{
    public partial class Pedigree : Form
    {
        public Int64 PigeonID { get; set; }
        public Int64 UserID { get; set; }
        public string PigeonName { get; set; }
        public Boolean Istrial { get; set; }

        public Pedigree()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Pedigree_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPedigree();
                if (Istrial)
                {
                    this.btnPrintPedigree.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LoadPedigree()
        {
            try
            {
                DataSet dtresult = new DataSet();

                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;
                dtresult = pigeonDetails.PigeonPedigree();

                if (dtresult.Tables.Count > 0)
                {
                    if (dtresult.Tables[0].Rows.Count > 0)
                    {
                        LoadPicture(pictureBox1, (byte[])dtresult.Tables[0].Rows[0]["Picture"]);
                        txtRoot.Text = dtresult.Tables[0].Rows[0]["Root"].ToString();
                        txtFirstLevelCock.Text = dtresult.Tables[0].Rows[0]["FirstLevelCock"].ToString();
                        txtFirstLevelHen.Text = dtresult.Tables[0].Rows[0]["FirstLevelHen"].ToString();
                        txtSecondLevelCock1.Text = dtresult.Tables[0].Rows[0]["SecondLevelCock1"].ToString();
                        txtSecondLevelHen1.Text = dtresult.Tables[0].Rows[0]["SecondLevelHen1"].ToString();
                        txtSecondLevelCock2.Text = dtresult.Tables[0].Rows[0]["SecondLevelCock2"].ToString();
                        txtSecondLevelHen2.Text = dtresult.Tables[0].Rows[0]["SecondLevelHen2"].ToString();
                        txtThirdLevelCock1.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock1"].ToString();
                        txtThirdLevelCock2.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock2"].ToString();
                        txtThirdLevelCock3.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock3"].ToString();
                        txtThirdLevelCock4.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock4"].ToString();
                        txtThirdLevelHen1.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen1"].ToString();
                        txtThirdLevelHen2.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen2"].ToString();
                        txtThirdLevelHen3.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen3"].ToString();
                        txtThirdLevelHen4.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen4"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadPicture(PictureBox pbPigeon, byte[] images)
        {
            try
            {
                pbPigeon.Image = null;
                MemoryStream ms = new MemoryStream(images);
                pbPigeon.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPigeon.Image = Image.FromStream(ms);
            }
            catch (Exception)
            { }
            finally
            { }
        }

        private void ViewDetails()
        {
            try
            {
                PigeonProgram.PigeonDetails frmPigeonDetails = new PigeonDetails();
                frmPigeonDetails.BandNumber = PigeonName;
                frmPigeonDetails.UserID = this.UserID;
                frmPigeonDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSecondLevelHen2_Click(object sender, EventArgs e)
        {
            if (txtSecondLevelHen2.Text != "")
            {
                PigeonName = txtSecondLevelHen2.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnFirstLevelHen_Click(object sender, EventArgs e)
        {
            if (txtFirstLevelHen.Text != "")
            {
                PigeonName = txtFirstLevelHen.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnFirstLevelCock_Click(object sender, EventArgs e)
        {
            if (txtFirstLevelCock.Text != "")
            {
                PigeonName = txtFirstLevelCock.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnSecondLevelCock1_Click(object sender, EventArgs e)
        {
            if (txtSecondLevelCock1.Text != "")
            {
                PigeonName = txtSecondLevelCock1.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnSecondLevelHen1_Click(object sender, EventArgs e)
        {
            if (txtSecondLevelHen1.Text != "")
            {
                PigeonName = txtSecondLevelHen1.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnSecondLevelCock2_Click(object sender, EventArgs e)
        {
            if (txtSecondLevelCock2.Text != "")
            {
                PigeonName = txtSecondLevelCock2.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelCock1_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelCock1.Text != "")
            {
                PigeonName = txtThirdLevelCock1.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelCock2_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelCock2.Text != "")
            {
                PigeonName = txtThirdLevelCock2.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelCock3_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelCock3.Text != "")
            {
                PigeonName = txtThirdLevelCock3.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelCock4_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelCock4.Text != "")
            {
                PigeonName = txtThirdLevelCock4.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelHen1_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelHen1.Text != "")
            {
                PigeonName = txtThirdLevelHen1.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelHen2_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelHen2.Text != "")
            {
                PigeonName = txtThirdLevelHen2.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelHen3_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelHen3.Text != "")
            {
                PigeonName = txtThirdLevelHen3.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void btnThirdLevelHen4_Click(object sender, EventArgs e)
        {
            if (txtThirdLevelHen4.Text != "")
            {
                PigeonName = txtThirdLevelHen4.Text.Replace("\r\n", "|");
                ViewDetails();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (PigeonID == 0) return;
                PedigreePrint pedigree = new PedigreePrint();
                pedigree.PigeonID = PigeonID;
                pedigree.UserID = UserID;
                //this.Hide();
                pedigree.ShowDialog();
                //this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
