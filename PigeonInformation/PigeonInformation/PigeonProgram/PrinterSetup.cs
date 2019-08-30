using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;

namespace PigeonProgram
{
    public partial class PrinterSetup : Form
    {
        public Int64 UserID { get; set; }
        public DataSet PedigreeSetup { get; set; }
        public String BackgroundImages { get; set; }

        public PrinterSetup()
        {
            InitializeComponent();
        }

        private void PrinterSetup_Load(object sender, EventArgs e)
        {
            try
            {
                GetPedigreeSetup();
                GetListBoxPrinterList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
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

        private void GetPedigreeSetup()
        {
            try
            {
                DataSet PedigreeSetup = new DataSet();
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.UserID = UserID;
                PedigreeSetup = pigeonDetails.GetPedigreeSetup();

                if (PedigreeSetup.Tables.Count > 0)
                {
                    if (PedigreeSetup.Tables[0].Rows.Count > 0)
                    {
                        LoadPicture(pbLogo, (byte[])PedigreeSetup.Tables[0].Rows[0]["Logo"]);
                        txtLoftName.Text = PedigreeSetup.Tables[0].Rows[0]["LoftName"].ToString();
                        txtName.Text = PedigreeSetup.Tables[0].Rows[0]["Name"].ToString();
                        txtAddress.Text = PedigreeSetup.Tables[0].Rows[0]["Address"].ToString();
                        txtContactNumber.Text = PedigreeSetup.Tables[0].Rows[0]["ContactNumber"].ToString();
                        txtresolution.Text = PedigreeSetup.Tables[0].Rows[0]["resolution"].ToString();
                        txtResolutionY.Text = PedigreeSetup.Tables[0].Rows[0]["resolutionY"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetListBoxPrinterList()
        {
            try
            {
                foreach (var item in PrinterSettings.InstalledPrinters)
                {
                    listBox1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static class Printer
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Printer);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Pname = "";
            if (listBox1.SelectedItem != null)
            {
                Pname = listBox1.SelectedItem.ToString();
                Printer.SetDefaultPrinter(Pname);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BIZ.User blluser = new BIZ.User();
                blluser.Name = txtName.Text;
                blluser.LoftName = txtLoftName.Text;
                blluser.Address = txtAddress.Text;
                blluser.ContactNumber = txtContactNumber.Text;
                blluser.Resolution = Convert.ToInt64(txtresolution.Text);
                blluser.ResolutionY = Convert.ToInt64(txtResolutionY.Text);
                blluser.Logo = Common.Common.GetImage(this.pbLogo);
                blluser.BackgroundImages = txtbackground.Text;
                this.BackgroundImages = txtbackground.Text;
                PedigreeSetup = blluser.PedigreeSetup(UserID);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                f.InitialDirectory = "C:/Picture/";
                f.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
                f.FilterIndex = 2;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    pbLogo.Image = Image.FromFile(f.FileName);
                    pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLogo.BorderStyle = BorderStyle.Fixed3D;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            pbLogo.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                f.InitialDirectory = "C:/Picture/";
                f.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
                f.FilterIndex = 2;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    //pbLogo.Image = Image.FromFile(f.FileName);
                    //pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pbLogo.BorderStyle = BorderStyle.Fixed3D;
                    this.txtbackground.Text = f.FileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
