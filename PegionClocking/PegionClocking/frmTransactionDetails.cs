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
    public partial class frmTransactionDetails : Form
    {
        public DataTable DTTransactionDetails { get; set; }
        public frmTransactionDetails()
        {
            InitializeComponent();
        }

        private void frmTransactionDetails_Load(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = true;
            dataGridView1.DataSource = DTTransactionDetails;
            //dataGridView1.Columns[5].Visible = false;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            frmReportGeneration reportGeneration = new frmReportGeneration();
            DataTable dt = new DataTable();
            dt = (DataTable)this.dataGridView1.DataSource;
            reportGeneration.Type = "TransactionDetails";
            reportGeneration.dtRecord = dt;
            reportGeneration.ShowDialog();
        }
    }
}
