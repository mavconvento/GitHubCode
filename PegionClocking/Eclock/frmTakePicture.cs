using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eclock
{
    public partial class frmTakePicture : Form
    {
        public byte[] Picture { get; set; }
        public string fileExtension { get; set; }
        public string PictureFileName { get; set; }

        public frmTakePicture()
        {
            InitializeComponent();
        }

        private void btnTakePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.InitialDirectory = "C:/Picture/";
            f.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif|PNG|*.png";
            f.FilterIndex = 2;

            if (f.ShowDialog() == DialogResult.OK)
            {
                pbPigeonPicture.Image = Image.FromFile(f.FileName);
                pbPigeonPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPigeonPicture.BorderStyle = BorderStyle.Fixed3D;

                Picture = GetImage();
                PictureFileName = f.FileName;
            }
        }

        private byte[] GetImage()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (pbPigeonPicture.Image != null)
                {
                    pbPigeonPicture.Image.Save(ms, pbPigeonPicture.Image.RawFormat);
                }

                byte[] image = ms.GetBuffer();
                return image;
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
