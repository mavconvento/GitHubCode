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
    public class MemberDAL : BaseDAL
    {
        #region Variables
        private Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
        #endregion Variables

        public DataSet ValidateLogin(Member oMem)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceResultLogins");
                //database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
                database.AddInParameter(DbCommand, "@RequestOrigin", DbType.String, oMem.Page);
                database.AddInParameter(DbCommand, "@UserName", DbType.String, oMem.UserName);
                database.AddInParameter(DbCommand, "@Password", DbType.String, oMem.Password);

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



}