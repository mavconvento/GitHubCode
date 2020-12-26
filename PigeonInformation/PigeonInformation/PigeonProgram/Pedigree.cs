using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PigeonProgram.Common;

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

                        string color = dtresult.Tables[0].Rows[0]["BackColor"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtRoot);

                        txtFirstLevelCock.Text = dtresult.Tables[0].Rows[0]["FirstLevelCock"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFirstLevelCock"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFirstLevelCock);

                        txtFirstLevelHen.Text = dtresult.Tables[0].Rows[0]["FirstLevelHen"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFirstLevelHen"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFirstLevelHen);

                        txtSecondLevelCock1.Text = dtresult.Tables[0].Rows[0]["SecondLevelCock1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorSecondLevelCock1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtSecondLevelCock1);

                        txtSecondLevelHen1.Text = dtresult.Tables[0].Rows[0]["SecondLevelHen1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorSecondLevelHen1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtSecondLevelHen1);
                        
                        txtSecondLevelCock2.Text = dtresult.Tables[0].Rows[0]["SecondLevelCock2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorSecondLevelCock2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtSecondLevelCock2);

                        txtSecondLevelHen2.Text = dtresult.Tables[0].Rows[0]["SecondLevelHen2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorSecondLevelHen2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtSecondLevelHen2);

                        txtThirdLevelCock1.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelCock1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelCock1);

                        txtThirdLevelCock2.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelCock2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelCock2);

                        txtThirdLevelCock3.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock3"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelCock3"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelCock3);

                        txtThirdLevelCock4.Text = dtresult.Tables[0].Rows[0]["ThirdLevelCock4"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelCock4"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelCock4);

                        txtThirdLevelHen1.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelHen1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelHen1);

                        txtThirdLevelHen2.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelHen2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelHen2);

                        txtThirdLevelHen3.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen3"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelHen3"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelHen3);

                        txtThirdLevelHen4.Text = dtresult.Tables[0].Rows[0]["ThirdLevelHen4"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorThirdLevelHen4"].ToString(); 
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtThirdLevelHen4);

                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void SetBackColor(string color, TextBox txtbox)
        //{
        //    string[] rgb = color.Split(' ');
        //    txtbox.BackColor = System.Drawing.Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]), int.Parse(rgb[3]));
        //}

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

                PrinterSetup printer = new PrinterSetup();
                printer.UserID = UserID;
                printer.ShowDialog();

                this.Hide();
                if (printer.IsFourthGen)
                {
                    Pedigree5thGenPrint pedigree = new Pedigree5thGenPrint();
                    pedigree.PedigreeSetup = printer.PedigreeSetup;
                    pedigree.BackgroundImages = printer.BackgroundImages;
                    pedigree.PigeonID = PigeonID;
                    pedigree.UserID = UserID;
                    pedigree.IsShowOwnerDetails = printer.IsShowOwnerDetails;
                    pedigree.ShowDialog();
                }
                else if (printer.IsHorizontal)
                {
                    PedigreePrint pedigree = new PedigreePrint();
                    pedigree.PedigreeSetup = printer.PedigreeSetup;
                    pedigree.BackgroundImages = printer.BackgroundImages;
                    pedigree.PigeonID = PigeonID;
                    pedigree.UserID = UserID;
                    pedigree.IsShowOwnerDetails = printer.IsShowOwnerDetails;
                    pedigree.ShowDialog();
                }
                else
                {
                    PedigreeVerticalPrint pedigree = new PedigreeVerticalPrint();
                    pedigree.PedigreeSetup = printer.PedigreeSetup;
                    pedigree.BackgroundImages = printer.BackgroundImages;
                    pedigree.PigeonID = PigeonID;
                    pedigree.UserID = UserID;
                    pedigree.IsShowOwnerDetails = printer.IsShowOwnerDetails;
                    pedigree.ShowDialog();
                }
                
                this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
