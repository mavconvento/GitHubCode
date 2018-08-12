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
    public partial class frmTransaction : Form
    {
        #region Constants

        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public Int64 UserID { get; set; }
        public Int64 TransactionID { get; set; }
        public String TransactionName { get; set; }
        public String TransactionType { get; set; }
        public String BillingNumber { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Quantity { get; set; }
        public String Particular { get; set; }
        public Decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DataTable RecordSearched { get; set; }
        public DataTable BillingStatementDetails { get; set; }
        public String OrderStatus { get; set; }
        #endregion

        #region Variable
        BIZ.Transaction transaction;
        #endregion

        #region Events
        public frmTransaction()
        {
            InitializeComponent();
            dtTransactionDetailsList.DoubleClick += new EventHandler(grid_DoubleClick);
            dtBillingSummary.DoubleClick += new EventHandler(dtBillingSummary_DoubleClick);
        }
        private void frmTransaction_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Global.IsMainDatabase = true;
                ClearControl();
                PopulateComboBox();
                TransactionGetByBillingNo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void txtUnitPrize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetParticular();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                GetBillingNo();
                TransactionGetByBillingNo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                TransactionGetByBillingNo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateBillingStatement();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        #endregion

        #region Private Methods
        private void GenerateBillingStatement()
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dtTransactionDetailsList.DataSource;
                reportGeneration.Type = "BillingStatement";
                reportGeneration.dtRecord = dt;
                reportGeneration.dtBillingStatement = BillingStatementDetails;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void dtBillingSummary_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtBillingSummary;
                Int64 index;
                Int64 colIndex;
                if (datagrid.RowCount > 0)
                {
                    transaction = new BIZ.Transaction();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    if (colIndex == 6)
                    {
                        ClubName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (ClubName != "")
                        {
                            DataSet dtresult = new DataSet();
                            //PopulateBLL();
                            transaction.ClubName = ClubName;
                            dtresult = transaction.TransactionHistory();

                            if (dtresult.Tables.Count > 0)
                            {
                                frmTransactionSummary transactionhistory = new frmTransactionSummary();
                                transactionhistory.ClubName = ClubName;
                                transactionhistory.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No record is found", "Search");
                            }

                        }
                    }
                    if (colIndex == 7)
                    {
                        ClubName = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (ClubName != "")
                        {
                            DataSet dtresult = new DataSet();
                            //PopulateBLL();
                            transaction.ClubName = ClubName;
                            dtresult = transaction.PaymentHistory();

                            if (dtresult.Tables.Count > 0)
                            {

                                frmPaymentTransactionSummary paymentSummary = new frmPaymentTransactionSummary();
                                paymentSummary.ClubName = ClubName;
                                paymentSummary.ShowDialog();
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
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtTransactionDetailsList;
                Int64 index;
                Int64 colIndex;
                if (datagrid.RowCount > 0)
                {
                    transaction = new BIZ.Transaction();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    if (colIndex == 5)
                    {
                        TransactionID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (TransactionID > 0)
                        {
                            DataSet dtresult = new DataSet();
                            PopulateBLL();
                            dtresult = transaction.TransactionGetByKey();

                            if (dtresult.Tables.Count > 0)
                            {
                                if (dtresult.Tables[0].Rows.Count > 0)
                                {
                                    RecordSearched = dtresult.Tables[0];
                                    PopulateControl();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No record is found", "Search");
                            }

                        }
                    }
                    else if (colIndex == 6)
                    {

                        TransactionID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (TransactionID > 0)
                        {
                            if (MessageBox.Show("Are you sure you would like to delete this transaction?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                transaction.TransactionID = TransactionID;
                                transaction.TransactionDelete();
                                ClearControl();
                                TransactionGetByBillingNo();
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
        private void PopulateControl()
        {
            try
            {
                cmbClub.SelectedItem = RecordSearched.Rows[0]["ClubName"];
                cmbParticular.SelectedItem = RecordSearched.Rows[0]["DESCRIPTION"];
                txtQuantity.Text = RecordSearched.Rows[0]["Quantity"].ToString();
                txtUnitPrize.Text = RecordSearched.Rows[0]["Unit"].ToString();
                txtPaymentAmount.Text = RecordSearched.Rows[0]["PaymentAmount"].ToString();
                dpPaymentDate.Value = Convert.ToDateTime(RecordSearched.Rows[0]["PaymentDate"]);
                cmbOrderStatus.SelectedItem = RecordSearched.Rows[0]["OrderStatus"].ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ClearControl()
        {
            try
            {
                txtUnitPrize.Text = "0";
                txtQuantity.Text = "0";
                txtPaymentAmount.Text = "0.00";
                txtPayment.Text = "0";
                txtPaymentAmount.Text = "0";
                dpPaymentDate.Value = DateTime.Now;
                txtPreviousBalance.Text = "0";
                txtBalance.Text = "0";
                txtTotalAmount.Text = "0";
                cmbClub.SelectedIndex = -1;
                cmbParticular.SelectedIndex = -1;
                cmbTransactionType.SelectedIndex = -1;
                cmbOrderStatus.SelectedIndex = -1;
                TransactionID = 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ControlValue()
        {
            try
            {
                BillingNumber = txtBillingNo.Text;
                UnitPrice = Convert.ToDecimal(txtUnitPrize.Text);
                Quantity = Convert.ToDecimal(txtQuantity.Text);
                Particular = cmbParticular.Text;
                ClubName = cmbClub.Text;
                TransactionType = cmbTransactionType.Text;
                OrderStatus = cmbOrderStatus.Text;
                PaymentAmount = Convert.ToDecimal(txtPaymentAmount.Text);
                PaymentDate = dpPaymentDate.Value;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CalculateTotalAmount()
        {
            try
            {
                Decimal TotalAmount = 0;
                Decimal UnitPrice = 0;
                Decimal Quantity = 0;

                if (txtQuantity.Text != "")
                {
                    Quantity = Convert.ToDecimal(txtQuantity.Text);
                }

                if (txtUnitPrize.Text != "")
                {
                    UnitPrice = Convert.ToDecimal(txtUnitPrize.Text);
                }
                TotalAmount = UnitPrice * Quantity;
                txtTotalCost.Text = String.Format("{0:#,##0.00}", TotalAmount);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void PopulateComboBox()
        {
            try
            {
                //TransactionType
                cmbTransactionType.Items.Add("Billing");
                cmbTransactionType.Items.Add("Payment");

                //Order Status
                cmbOrderStatus.Items.Add("Pending");
                cmbOrderStatus.Items.Add("Delivered");

                //Get Club List
                BIZ.Club club = new BIZ.Club();
                DataTable dtResult = new DataTable();
                dtResult = club.ClubSelectAll();
                foreach (DataRow dr in dtResult.Rows)
                {
                    cmbClub.Items.Add(dr["Club Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetParticular()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new BIZ.Transaction();
                PopulateBLL();

                dtResult = transaction.GetParticular();
                cmbParticular.Items.Clear();
                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            cmbParticular.Items.Add(item["DESCRIPTION"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetBillingNo()
        {
            try
            {
                transaction = new BIZ.Transaction();
                DataSet dtResult = new DataSet();

                dtResult = transaction.GetBillingNumber();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        txtBillingNo.Text = dtResult.Tables[0].Rows[0]["BillingNo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulateBLL()
        {
            try
            {
                ControlValue();
                transaction.TransactionID = TransactionID;
                transaction.TransactionType = TransactionType;
                transaction.ClubName = ClubName;
                transaction.BillingNumber = BillingNumber;
                transaction.UnitPrice = UnitPrice;
                transaction.Quantity = Quantity;
                transaction.Particular = Particular;
                transaction.UserID = UserID;
                transaction.PaymentAmount = PaymentAmount;
                transaction.PaymentDate = PaymentDate;
                transaction.OrderStatus = OrderStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Save()
        {
            try
            {
                transaction = new BIZ.Transaction();
                PopulateBLL();

                if (transaction.Save())
                {
                    MessageBox.Show("Transaction Record Save.", "Error");
                    ClearControl();
                    TransactionGetByBillingNo();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void TransactionGetByBillingNo()
        {
            try
            {
                transaction = new BIZ.Transaction();
                PopulateBLL();

                DataSet dtResult = new DataSet();

                dtResult = transaction.TransactionGetByBillingNo();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        txtTotalAmount.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(dtResult.Tables[0].Rows[0]["TotalAmount"]));
                        txtPayment.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(dtResult.Tables[0].Rows[0]["TotalPayment"]));
                        txtBalance.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(dtResult.Tables[0].Rows[0]["Balance"]));
                        txtPreviousBalance.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(dtResult.Tables[0].Rows[0]["PreviousBalance"]));
                        cmbClub.SelectedItem = dtResult.Tables[0].Rows[0]["ClubName"].ToString();
                        cmbTransactionType.SelectedItem = dtResult.Tables[0].Rows[0]["TransactionType"].ToString();
                        BillingStatementDetails = dtResult.Tables[0];
                    }
                    dtTransactionDetailsList.DataSource = dtResult.Tables[1];
                    dtBillingSummary.DataSource = dtResult.Tables[2];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

    }
}
