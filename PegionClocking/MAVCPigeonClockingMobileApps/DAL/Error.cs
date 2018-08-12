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
    public class Error : BaseDAL
    {
        #region Constants
        private Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
        private const string SP_GETERRORLISTSEARCH = "shrErrorListSearch";
        #endregion


        #region Properties
        public Int64 IErrCode { get; set; }
        public string CErrCode { get; set; }
        #endregion

        #region Contructors
        #endregion Contructors

        #region Public Methods
        public DataTable GetErrorListSearch()
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand(SP_GETERRORLISTSEARCH);
                database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32,System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@iErrCode", DbType.Int64, IErrCode);
                database.AddInParameter(DbCommand, "@cErrCode", DbType.String, CErrCode);

                return InternalExecuteDataSet(database, DbCommand, null).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}