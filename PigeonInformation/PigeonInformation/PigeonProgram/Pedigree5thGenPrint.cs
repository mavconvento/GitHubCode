using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using PigeonProgram.Common;

namespace PigeonProgram
{
    public partial class Pedigree5thGenPrint : Form
    {
        Bitmap bmp;
        public Int64 PigeonID { get; set; }
        public Int64 UserID { get; set; }
        public string PigeonName { get; set; }
        public DataSet PedigreeSetup { get; set; }
        public Boolean Istrial { get; set; }
        public Boolean IsClosing { get; set; }
        public int Resolution { get; set; }
        public int ResolutionY { get; set; }
        public int PanelWidth { get; set; }
        public int PanelHeight { get; set; }
        public String BackgroundImages { get; set; }
        public bool IsShowOwnerDetails { get; set; }
        public Pedigree5thGenPrint()
        {
            InitializeComponent();
        }

        private void PedigreePrint_Load(object sender, EventArgs e)
        {
            try
            {
                PanelWidth = this.panel1.Width;
                PanelHeight = this.panel1.Height;
                this.groupBox1.Visible = IsShowOwnerDetails;
                SetupPrinter();

                if (IsClosing)
                {
                    this.Close();
                }
                else
                {
                    LoadPedigree();
                    //this.WindowState = FormWindowState.Maximized;
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
                        Resolution = int.Parse(PedigreeSetup.Tables[0].Rows[0]["Resolution"].ToString());
                        ResolutionY = int.Parse(PedigreeSetup.Tables[0].Rows[0]["ResolutionY"].ToString());

        
                        //if ((Boolean)PedigreeSetup.Tables[0].Rows[0]["Istrial"])
                        //{
                        //    this.button1.Enabled = false;
                        //}
                    }
                    //Resolution = 100;
                    //ResolutionY = 100;

                    if (this.BackgroundImages != "")
                    {
                        Bitmap a = new Bitmap(this.BackgroundImages);
                        this.panel1.BackgroundImage = a;
                        
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
                pigeonDetails.IsFourthGen = true;
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

                        txtFourthLevelCock1.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock1);

                        txtFourthLevelCock2.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock2);

                        txtFourthLevelCock3.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock3"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock3"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock3);

                        txtFourthLevelCock4.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock4"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock4"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock4);

                        txtFourthLevelCock5.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock5"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock5"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock5);

                        txtFourthLevelCock6.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock6"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock6"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock6);

                        txtFourthLevelCock7.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock7"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock7"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock7);

                        txtFourthLevelCock8.Text = dtresult.Tables[0].Rows[0]["FourthLevelCock8"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelCock8"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelCock8);

                        txtFourthLevelHen1.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen1"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen1"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen1);

                        txtFourthLevelHen2.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen2"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen2"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen2);

                        txtFourthLevelHen3.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen3"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen3"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen3);

                        txtFourthLevelHen4.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen4"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen4"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen4);

                        txtFourthLevelHen5.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen5"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen5"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen5);

                        txtFourthLevelHen6.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen6"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen6"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen6);

                        txtFourthLevelHen7.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen7"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen7"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen7);

                        txtFourthLevelHen8.Text = dtresult.Tables[0].Rows[0]["FourthLevelHen8"].ToString();
                        color = dtresult.Tables[0].Rows[0]["ColorFourthLevelHen8"].ToString();
                        if (!string.IsNullOrEmpty(color)) Common.Common.SetBackColor(color, txtFourthLevelHen8);

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
                Size formSize = this.ClientSize;
                this.panel1.Height = PanelHeight + ResolutionY;
                this.panel1.Width = PanelWidth + Resolution;
                bmp = new Bitmap(this.panel1.Width, this.panel1.Height, g);
                bmp.SetResolution(105, 100);
                Graphics mg = Graphics.FromImage(bmp);
                mg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                mg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                mg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                mg.CopyFromScreen(PointToScreen(this.panel1.Location).X, PointToScreen(this.panel1.Location).Y, 0, 0, this.panel1.Size);

                mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, formSize);
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void SaveAsImagePedigree()
        {
            try
            {
                ImageCodecInfo myImageCodecInfo;
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;

                Graphics g = this.CreateGraphics();
                Size formSize = this.ClientSize;
                this.panel1.Height = PanelHeight + ResolutionY;
                this.panel1.Width = PanelWidth + Resolution;
                bmp = new Bitmap(this.panel1.Width, this.panel1.Height, g);
                Graphics mg = Graphics.FromImage(bmp);
                mg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                mg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                mg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                mg.CopyFromScreen(PointToScreen(this.panel1.Location).X, PointToScreen(this.panel1.Location).Y, 0, 0, this.panel1.Size);

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Images|*.png;*.bmp;*.jpg";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = dialog.FileName;

                    if (filePath != "")
                    {
                        myImageCodecInfo = GetEncoderInfo("image/jpeg");
                        myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        myEncoderParameters = new EncoderParameters(1);
                        myEncoderParameter = new EncoderParameter(myEncoder, 75L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        bmp.Save(filePath, myImageCodecInfo, myEncoderParameters);
                    }               
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.button1.Visible = false;
                this.button2.Visible = false;
                this.button3.Visible = false;
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
                this.button3.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.button1.Visible = false;
                this.button2.Visible = false;
                this.button3.Visible = false;
                SaveAsImagePedigree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                this.button1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
            }     
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
