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
using BusinessLayer;
using Helper;

namespace PigeonIDSystem
{
    public partial class frmRaceCode : Form
    {
        public frmRaceCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                EclockEntryBLL eclockEntryBLL = new EclockEntryBLL();

                DataSet dataSet = new DataSet();

                dataSet = eclockEntryBLL.GetRaceCode(txtclubid.Text, dateTimePicker1.Value.Date);

                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        txtracecode.Text = dataSet.Tables[0].Rows[0]["RaceCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void frmRaceCode_Load(object sender, EventArgs e)
        {
            string sysDir = AppDomain.CurrentDomain.BaseDirectory;
            string clubidpath = sysDir + "clubid.inf";

            this.txtracecode.Focus();

            if (File.Exists(clubidpath))
            {
                string[] pathlist = ReadText.ReadTextFile(clubidpath);
                this.txtclubid.Text = pathlist[0].ToString();
            }
        }
    }
}
