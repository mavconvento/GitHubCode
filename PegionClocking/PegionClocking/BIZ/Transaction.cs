using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Transaction
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Transaction transaction;
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
        public Boolean IsTranDetails { get; set; }
        public String OrderStatus { get; set; }

        //Load Mavc Card
        public String PinNumber { get; set; }
        public String MobileNumber { get; set; }
        #endregion

        #region Public Methods
        public DataSet TransactionGetByBillingNo()
        {
            try
            {
                transaction = new DAL.Transaction();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = transaction.TransactionGetByBillingNo();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                transaction.Save();
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet TransactionHistory()
        {
            try
            {
                 DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.TransactionHistory();
                return dtResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public DataSet PaymentHistory()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.PaymentsHistory();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet TransactionDetails()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.TransactionDetails();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet TransactionGetByKey()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.TransactionGetByID();
                return dtResult;
            }
            catch (Exception ex)
            {  
                throw ex;
            }
        }
        public Boolean TransactionDelete()
        {
            try
            {
                Boolean status = false;
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                transaction.TransactionDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetBillingNumber()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.GetBillingNo();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetParticular()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.GetParticularList();
                return dtResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public DataSet LoadAccessNumber()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.LoadAccessCard();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet RegisterMobileNumber()
        {
            try
            {
                DataSet dtResult = new DataSet();
                transaction = new DAL.Transaction();
                PopulateDataLayer();
                dtResult = transaction.RegisterMobileNumber();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
                transaction.TransactionID = TransactionID;
                transaction.ClubID = ClubID;
                transaction.UserID = UserID;
                transaction.TransactionName = TransactionName;
                transaction.TransactionType = TransactionType;
                transaction.Quantity = Quantity;
                transaction.UnitPrice = UnitPrice;
                transaction.ClubName = ClubName;
                transaction.Particular = Particular;
                transaction.BillingNo = BillingNumber;
                transaction.PaymentAmount = PaymentAmount;
                transaction.PaymentDate = PaymentDate;
                transaction.IsTranDetails = IsTranDetails;
                transaction.OrderStatus = OrderStatus;
                transaction.PinNumber = PinNumber;
                transaction.MobileNumber = MobileNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
