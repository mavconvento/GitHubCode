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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPhotoCapture banding = new frmPhotoCapture();
            this.Hide();
            banding.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEntry entry = new frmEntry();
            this.Hide();
            entry.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmResult result = new frmResult();
            this.Hide();
            result.ShowDialog();
            this.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Common.GetClub();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmSetClub clubform = new frmSetClub();
            clubform.ShowDialog();
        }
    }
}
