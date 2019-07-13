using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmSetClub : Form
    {
        public String Club { get; set; }
        public frmSetClub()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                if (this.textBox1.Text != "") System.IO.File.WriteAllText(path + "ClubInfo.txt", this.textBox1.Text);
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
         
        }

        private void SetClub_Load(object sender, EventArgs e)
        {
            string path = ReadText.ReadFilePath("datapath");
            string filepath = path + "\\ClubInfo.txt";
            this.textBox1.Focus();
            if (File.Exists(filepath))
            {
                string[] clublist = ReadText.ReadTextFile(filepath);

                this.textBox1.Text = clublist[0];
                this.button1.Text = "Update";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
