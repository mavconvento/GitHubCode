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
    public partial class frmPaymentTransactionSummary : Form
    {
        BIZ.Transaction transaction;
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }

        public frmPaymentTransactionSummary()
        {
            InitializeComponent();
        }

        private void frmPaymentTransactionSummary_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Global.IsMainDatabase = true;
                TransactionGetByClub();
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void TransactionGetByClub()
        {
            try
            {
                transaction = new BIZ.Transaction();
                DataSet dtResult = new DataSet();

                transaction.ClubName = ClubName;
                transaction.ClubID = ClubID;
                dtResult = transaction.PaymentHistory();

                if (dtResult.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dtResult.Tables[0];
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            frmReportGeneration reportGeneration = new frmReportGeneration();
            DataTable dt = new DataTable();
            dt = (DataTable)this.dataGridView1.DataSource;
            reportGeneration.Type = "PaymentHistory";
            reportGeneration.dtRecord = dt;
            reportGeneration.ShowDialog();
        }
    }
}
