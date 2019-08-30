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
                string sysDir = AppDomain.CurrentDomain.BaseDirectory;
                string path = sysDir;

                if (this.textBox1.Text != "" && this.txtDataPath.Text != "")
                { 
                    System.IO.File.WriteAllText(path + "club.txt", this.textBox1.Text + @"\");
                    System.IO.File.WriteAllText(path + "datapath.inf", this.txtDataPath.Text);
                    Common.CreateStorageFolder();
                }
                
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
         
        }

        private void SetClub_Load(object sender, EventArgs e)
        {
            string sysDir = AppDomain.CurrentDomain.BaseDirectory;
            string filepath = sysDir + "club.txt";
            string datapath = sysDir + "datapath.inf";
            this.textBox1.Focus();
            if (File.Exists(filepath))
            {
                string[] clublist = ReadText.ReadTextFile(filepath);

                this.textBox1.Text = clublist[0].ToString().Replace(@"\","");
                this.button1.Text = "Update";
            }

            if (File.Exists(datapath))
            {
                string[] pathlist = ReadText.ReadTextFile(datapath);
                this.txtDataPath.Text = pathlist[0].ToString();
                //this.button1.Text = "Update";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
