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
    public class MobileRegistrationDAL : BaseDAL
    {
        #region Variables
        private Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
        #endregion Variables

        public DataSet MobileRegistrationStep1(String ClubName,String MemberID,String MobileNumber,String Step)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("MobileRegistrationSave");
                database.AddInParameter(DbCommand, "@MemberId", DbType.String, MemberID);
                database.AddInParameter(DbCommand, "@MobileNumber", DbType.String, MobileNumber);
                database.AddInParameter(DbCommand, "@Step", DbType.String, Step);
                database.AddInParameter(DbCommand, "@ClubName", DbType.String, ClubName);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet MobileRegistrationStep2(String Password,String MobileNumber,String Step)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("MobileRegistrationSave");
                database.AddInParameter(DbCommand, "@Password", DbType.String, Password);
                database.AddInParameter(DbCommand, "@Step", DbType.String, Step);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet MobileRegistrationStep3(String ActivationCode, String MobileNumber, String Step)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("MobileRegistrationSave");
                database.AddInParameter(DbCommand, "@ActivationCode", DbType.String, ActivationCode);
                database.AddInParameter(DbCommand, "@Step", DbType.String, Step);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}