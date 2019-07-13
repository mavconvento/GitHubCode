using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmCapturePhoto : Form
    {
        WebCam webcam;
        public frmCapturePhoto()
        {
            InitializeComponent();
        }

        public Image PigeonPhoto { get; set; }

        private void CapturePhoto_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            webcam.Start();
            //webcam.Continue();
            //ComboBoxItem();
            //CreateStorageFolder();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            PigeonPhoto = imgVideo.Image;
            webcam.Stop();
            this.Close();
            //if (imgCapture.Image != null)
            //{
            //    pictureBox1.Image = imgVideo.Image;
            //}
            //else
            //{
            //    imgCapture.Image = imgVideo.Image;
            //}
        }
    }
}
