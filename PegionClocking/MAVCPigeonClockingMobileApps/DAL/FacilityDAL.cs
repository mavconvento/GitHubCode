using System;
using System.Collections.Generic;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

using LWT.Common.DAL;
using LWT.Common;
using MAVCPigeonClockingMobileApps.Constants;
using MAVCPigeonClockingMobileApps.Models;
/*
  [1][107.20] : Edson 05/05/2014 : CR 17 : when called by LMS value = 'LMS' and CustomerPortal = 'Portal' lmsCustomerPortalAccountRedraw. added new parameter FundingStatus and RequestSource
  [2][110.2] Pewee #1870 Fixed Additional payment that can't be cancelled.
  [3][107.21] Pewee 5/6/2014 CR18
  [4][107.21] Pewee 16/6/2014 1981 Add validation in cancelstep1
 */

namespace MAVCPigeonClockingMobileApps.DAL
{
	public class FacilityDAL : BaseDAL
	{

		#region Variables
		private Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
		#endregion Variables

		public void AccountMakeNewPayment(Int64 AccountFacilityID, Int64 AccountID, double Amount, DateTime date, string freqcode,
			out Int64 PaymentID, out Int64 TransID)
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsAccountAdditionalPaymentSave");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
				database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
				database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
				database.AddInParameter(DbCommand, "@AdditionalPaymentAmount", DbType.Double, Amount);
				database.AddInParameter(DbCommand, "@PaymentFrequency ", DbType.String, freqcode);
				database.AddInParameter(DbCommand, "@StartDate", DbType.Date, date);
				
				database.AddOutParameter(DbCommand, "@RepaymentJournalID", DbType.Int64, 20);
				database.AddOutParameter(DbCommand, "@RepaymentID", DbType.Int64, 20);
				InternalExecuteNonQuery(database, DbCommand, null);

				TransID = LWTSafeTypes.SafeInt64(database.GetParameterValue(DbCommand, "@RepaymentJournalID"));
				PaymentID = LWTSafeTypes.SafeInt64(database.GetParameterValue(DbCommand, "@RepaymentID"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public Int64 AccountAdditionalPaymentCancel(Int64 AccountFacilityID, Int64 AccountID, Int64 PaymentID, Int64 CustomerID) //[2][110.2] #1870
		{
			try
			{
            
				DbCommand DbCommand = database.GetStoredProcCommand("lmsAccountAdditionalPaymentDelete");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
				database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
				database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
				database.AddInParameter(DbCommand, "@RepaymentID", DbType.Int64, PaymentID);
                database.AddInParameter(DbCommand, "@FootPrintID", DbType.Int64, CustomerID); //[2][110.2] #1870
                database.AddInParameter(DbCommand, "@FootPrintType", DbType.Int64, 1);  //[2][110.2] #1870
				database.AddOutParameter(DbCommand, "@RepaymentJournalID", DbType.Int64, 20);
				InternalExecuteNonQuery(database, DbCommand, null);
				return LWTSafeTypes.SafeInt64(database.GetParameterValue(DbCommand, "@RepaymentJournalID"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        public DataSet AccountRedrawSave(Int64 AccountFacilityID, Int64 AccountID,Int64 RedrawID,DateTime RedrawDate,Decimal RedrawAmount,String FundingStatus,String RequestSource) //[1][107.20]
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalAccountRedraw");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@RedrawID", DbType.Int64, RedrawID);
                database.AddInParameter(DbCommand, "@RedrawDate", DbType.DateTime, RedrawDate);
                database.AddInParameter(DbCommand, "@RedrawAmount", DbType.Decimal, RedrawAmount);
                //[1][107.20]
                database.AddInParameter(DbCommand, "@FundingStatus", DbType.String, FundingStatus); 
                database.AddInParameter(DbCommand, "@RequestSource", DbType.String, RequestSource);
                //[1][107.20]

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public DataSet GetAccountAdditionPaymentList(Int64 AccountFacilityID, Int64 AccountID, Int64 PaymentID)
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalGetAdditionalPayments");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
				database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
				database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
				database.AddInParameter(DbCommand, "@PaymentID", DbType.Int64, PaymentID);

				return InternalExecuteDataSet(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


        public DataSet GetAccountRedrawPaymentList(Int64 AccountFacilityID, Int64 AccountID, Int64 RedrawID)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalGetRedrawPayments");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@RedrawID", DbType.Int64, RedrawID);

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[3][107.21] start
        public DataSet GetPortalPortalCutOffTime(Int64 AccountFacilityID, Int64 AccountID)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsAccountPortalAdditionalPaymentDate");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
         }
         //[3][107.21] end

        public Int64 ProcessAccountRequestTransfer(Int64 AccountFacilityID, Int64 AccountID, Int64 AccountIDTo, Decimal TransferAmount)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalProcessRequestTransfer");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, AccountFacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@AccountIDTo", DbType.Int64, AccountIDTo);
                database.AddInParameter(DbCommand, "@CreditTransfer", DbType.Int64, TransferAmount);
                
                database.AddOutParameter(DbCommand, "@TranID", DbType.Int64, 20);
               
                InternalExecuteNonQuery(database, DbCommand, null);

                return LWTSafeTypes.SafeInt64(database.GetParameterValue(DbCommand, "@TranID"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public DataSet GetCustomerAccounts(Int64 customerID, Int64? AccountID = null)
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalGetAccounts");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@CustomerID", DbType.Int64, customerID);

				if (AccountID.HasValue) database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID.Value);

				return InternalExecuteDataSet(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet GetFacilityCustomersByAccountID(Int64 customerID, Int64 AccountID)
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalGetBorrowers");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int64, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@LoginCustID", DbType.Int64, customerID);
				database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);

				return InternalExecuteDataSet(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
        public DataSet ValidateDataRedraw(Int64 FacilityID,Int64 AccountID, DateTime nextBussinessDate,Decimal RedrawAmount)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalAccountRedrawValidate");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int64, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, FacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@RedrawAmount", DbType.Decimal, RedrawAmount);
                database.AddInParameter(DbCommand, "@RedrawDate", DbType.DateTime, nextBussinessDate);


                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[1][107.21] start
        public DataSet ValidateAddtionalPaymentDate(Int64 FacilityID, Int64 AccountID, DateTime additionalPaymentDate, Int64 additionaPaymentAmount, string mode) //[4][107.21]
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerAdditionalPaymentValidate");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int64, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, FacilityID);
                database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@AdditionalPaymentDate", DbType.DateTime, additionalPaymentDate);
                database.AddInParameter(DbCommand, "@AdditonalPaymentAmount", DbType.Int64, additionaPaymentAmount);
                database.AddInParameter(DbCommand, "@Mode", DbType.String, mode); //[4][107.21]


                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[1][107.21] end


        public DataSet ValidateDataRequestTransfer(Int64 FacilityID, Int64 AccountID, Int64 AccountIDTo, Decimal TransferAmount)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalAccountRequestTransferValidate");
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@UserID", DbType.Int64, -2);
                database.AddInParameter(DbCommand, "@AccountFacilityID", DbType.Int64, FacilityID);
                database.AddInParameter(DbCommand, "@AccountIDFrom", DbType.Int64, AccountID);
                database.AddInParameter(DbCommand, "@AccountIDTo", DbType.Int64, AccountIDTo);
                database.AddInParameter(DbCommand, "@TransferAmount", DbType.Int64, TransferAmount);

                return InternalExecuteDataSet(database, DbCommand, null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      }
	
}