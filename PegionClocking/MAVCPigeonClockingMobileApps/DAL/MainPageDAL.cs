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
    public class MainPageDAL : BaseDAL
    {
        #region Variables
        private Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
        #endregion Variables

        public DataSet GetRaceDetails(string clubID,DateTime releaseDate)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceReleasePointGetbyKey");
                database.AddInParameter(DbCommand, "@RaceReleasePointID", DbType.Int32, 0);
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, clubID);
                database.AddInParameter(DbCommand, "@RaceReleaseDate", DbType.DateTime, releaseDate);

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SendStickerNumber(string StickerNumber,string ClubName,string MobileNumber)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("WebClockingSave");
                database.AddInParameter(DbCommand, "@ClubName", DbType.String, ClubName);
                database.AddInParameter(DbCommand, "@SMSMobileNumber", DbType.String, MobileNumber);
                database.AddInParameter(DbCommand, "@StickerCode", DbType.String, StickerNumber);
                database.AddInParameter(DbCommand, "@RequestAction", DbType.String, "Mobile");
                database.AddInParameter(DbCommand, "@Action", DbType.String, "Sticker");

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SendForecast(string StickerNumber, string ClubName, string MobileNumber)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("WebClockingSave");
                database.AddInParameter(DbCommand, "@ClubName", DbType.String, ClubName);
                database.AddInParameter(DbCommand, "@SMSMobileNumber", DbType.String, MobileNumber);
                database.AddInParameter(DbCommand, "@StickerCode", DbType.String, StickerNumber); //Sticker value is always equal to Forecast
                database.AddInParameter(DbCommand, "@RequestAction", DbType.String, "Mobile");
                database.AddInParameter(DbCommand, "@Action", DbType.String, "Forecast");

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet testresult()
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("gettestresult");
                //database.AddInParameter(DbCommand, "@ClubName", DbType.String, ClubName);
                //database.AddInParameter(DbCommand, "@SMSMobileNumber", DbType.String, MobileNumber);
                //database.AddInParameter(DbCommand, "@StickerCode", DbType.String, StickerNumber); //Sticker value is always equal to Forecast
                //database.AddInParameter(DbCommand, "@RequestAction", DbType.String, "Mobile");
                //database.AddInParameter(DbCommand, "@Action", DbType.String, "Forecast");

                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}