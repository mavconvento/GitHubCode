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
    public partial class frmTransactionSummary : Form
    {
        BIZ.Transaction transaction;
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }

        public frmTransactionSummary()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void frmTransactionSummary_Load(object sender, EventArgs e)
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
                dtResult = transaction.TransactionHistory();

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
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                Int64 colIndex;
                String BillingNo;
                if (datagrid.RowCount > 0)
                {
                    transaction = new BIZ.Transaction();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    if (colIndex == 4)
                    {
                        BillingNo = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (BillingNo != "")
                        {
                            DataSet dtresult = new DataSet();
                            transaction.BillingNumber = BillingNo;
                            transaction.IsTranDetails = true;
                            dtresult = transaction.TransactionGetByBillingNo();

                            if (dtresult.Tables.Count > 0)
                            {
                                frmTransactionDetails transactionDetails = new frmTransactionDetails();
                                transactionDetails.DTTransactionDetails = dtresult.Tables[1];
                                transactionDetails.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No record is found", "Search");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            frmReportGeneration reportGeneration = new frmReportGeneration();
            DataTable dt = new DataTable();
            dt = (DataTable)this.dataGridView1.DataSource;
            reportGeneration.Type = "TransactionHistory";
            reportGeneration.dtRecord = dt;
            reportGeneration.ShowDialog();
        }
    }
}
