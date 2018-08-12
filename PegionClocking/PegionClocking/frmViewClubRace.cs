using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PegionClocking
{
    public partial class frmViewClubRace : Form
    {
        public frmViewClubRace()
        {
            InitializeComponent();
        }

        private void frmViewClubRace_Load(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dtResult = new DataSet();
            BIZ.RaceResult raceResult = new BIZ.RaceResult();
            raceResult.ReleasedDate = dateTimePicker1.Value.Date;
            dtResult = raceResult.ViewClubRace();
            if (dtResult.Tables.Count > 0)
            {
                dataGridView1.DataSource = dtResult.Tables[0];
            }
        }
    }
}
