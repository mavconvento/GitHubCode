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

namespace MAVCPigeonClockingMobileApps.DAL
{
    public class OriginatorDAL : BaseDAL
    {

        #region Variables
        private Database database = LWTDatabase.GetInstance().GetDatabase( CommonConstants.DATABASE_NAME);
        #endregion Variables


		public DataSet GetAccountFacilityOriginatorDetails(Int64 customerID, Int64 AccountID)
        {
            try
            {
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalGetAccountFacilityOriginatorDetails");
				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@CustomerID", DbType.Int64, customerID);
				database.AddInParameter(DbCommand, "@AccountID", DbType.Int64, AccountID);
				
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

    }
}