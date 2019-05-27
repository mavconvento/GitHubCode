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
    public partial class PedigreePrint : Form
    {
        Bitmap bmp;
        public Int64 PigeonID { get; set; }
        public Int64 UserID { get; set; }
        public string PigeonName { get; set; }
        public DataSet PedigreeSetup { get; set; }
        public Boolean Istrial { get; set; }
        public Boolean IsClosing { get; set; }
        public Int64 Resolution { get; set; }
        public Int64 ResolutionY { get; set; }

        public PedigreePrint()
        {
            InitializeComponent();
        }

        private void PedigreePrint_Load(object sender, EventArgs e)
        {
            try
            {
                SetupPrinter();

                if (IsClosing)
                {
                    this.Close();
                }
                else
                {
                    LoadPedigree();
                    this.WindowState = FormWindowState.Maximized;
                }
                
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SetupPrinter()
        {
            try
            {
                PrinterSetup printer = new PrinterSetup();
                printer.UserID = UserID;
                printer.ShowDialog();
                PedigreeSetup = printer.PedigreeSetup;

                if (PedigreeSetup == null)
                {
                    IsClosing = true;
                }
                else if (PedigreeSetup.Tables.Count > 0)
                {
                    if (PedigreeSetup.Tables[0].Rows.Count > 0)
                    {
                        LoadPicture(pictureBox2, (byte[])PedigreeSetup.Tables[0].Rows[0]["Logo"]);
                        lblLoftName.Text = PedigreeSetup.Tables[0].Rows[0]["LoftName"].ToString();
                        lblName.Text = PedigreeSetup.Tables[0].Rows[0]["Name"].ToString();
                        lblAddress.Text = PedigreeSetup.Tables[0].Rows[0]["Address"].ToString();
                        lblContactNumber.Text = PedigreeSetup.Tables[0].Rows[0]["ContactNumber"].ToString();
                        Resolution = (Int64)PedigreeSetup.Tables[0].Rows[0]["Resolution"];
                        ResolutionY = (Int64)PedigreeSetup.Tables[0].Rows[0]["ResolutionY"];

                        //if ((Boolean)PedigreeSetup.Tables[0].Rows[0]["Istrial"])
                        //{
                        //    this.button1.Enabled = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void PrintPedigree()
        {
            try
            {
                Graphics g = this.CreateGraphics();
                bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
                bmp.SetResolution(Resolution, ResolutionY);
                Graphics mg = Graphics.FromImage(bmp);
                mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
                printPreviewDialog1.ShowDialog();
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
                this.button1.Visible = false;
                this.button2.Visible = false;
                PrintPedigree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                this.button1.Visible = true;
                this.button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
