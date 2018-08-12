using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using LWT.Common;
using LWT.Common.DAL;

namespace WebRaceResult.DAL
{
    public class RaceResult : BaseDAL
    {
        private const string DATABASE_NAME = "Mavc";
        static Database database = LWTDatabase.GetInstance().GetDatabase(DATABASE_NAME);

        public DataSet GetClubList(String UserID)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("ClubSelectAll");
                database.AddInParameter(DbCommand, "@UserName", DbType.String, UserID);
                database.AddInParameter(DbCommand, "@NotExpired", DbType.Boolean, 1);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetBirdCategory(String ClubID)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceCategorySelectAll");
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@ReturnAll", DbType.String, 0);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetGroupCategory(String ClubID)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceCategoryGroupSelectAll");
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@ReturnAll", DbType.String, 0);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetRaceResult(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceResultGetbyKey");
                //DbCommand.CommandTimeout = 0;
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@ReleaseDate", DbType.DateTime, ReleaseDate);
                database.AddInParameter(DbCommand, "@RaceCategory", DbType.String, BirdCategory);
                database.AddInParameter(DbCommand, "@RaceCategoryGroup", DbType.String, RaceCategory);
                database.AddInParameter(DbCommand, "@Version", DbType.String, 0);
                database.AddInParameter(DbCommand, "@Name", DbType.String, SearchName);
                database.AddInParameter(DbCommand, "@IsFromWeb", DbType.Boolean, 1);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetRaceDetails(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceDetailsGetbyKeys");
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@ReleaseDate", DbType.DateTime, ReleaseDate);
                database.AddInParameter(DbCommand, "@RaceCategory", DbType.String, BirdCategory);
                database.AddInParameter(DbCommand, "@RaceCategoryGroup", DbType.String, RaceCategory);
                database.AddInParameter(DbCommand, "@Version", DbType.String, 0);
                database.AddInParameter(DbCommand, "@Name", DbType.String, SearchName);
                database.AddInParameter(DbCommand, "@IsFromWeb", DbType.Boolean, 1);
                database.AddInParameter(DbCommand, "@Sender", DbType.String, Sender);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetRaceEntry(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender)
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("RaceDetailsGetbyKeys");
                database.AddInParameter(DbCommand, "@ClubID", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@ReleaseDate", DbType.DateTime, ReleaseDate);
                database.AddInParameter(DbCommand, "@RaceCategory", DbType.String, BirdCategory);
                database.AddInParameter(DbCommand, "@RaceCategoryGroup", DbType.String, RaceCategory);
                database.AddInParameter(DbCommand, "@Version", DbType.String, 0);
                database.AddInParameter(DbCommand, "@Name", DbType.String, SearchName);
                database.AddInParameter(DbCommand, "@IsFromWeb", DbType.Boolean, 1);
                database.AddInParameter(DbCommand, "@Sender", DbType.String, Sender);
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SendSticker(string ClubID, String mobileNumber, string keyword)
        {
            try
            {
                //DAL.Common common = new DAL.Common();
                return WebClockingSave(ClubID, mobileNumber, keyword, "Sticker");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Forecast(string ClubID, String mobileNumber, string keyword)
        {
            try
            {
                //DAL.Common common = new DAL.Common();
                return WebClockingSave(ClubID, mobileNumber, keyword, "Forecast");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet WebClockingSave(String ClubID, String SMSMobileNumber, String Keyword, String Action, String SMSMobileNumberTo = "")
        {
            try
            {
                DbCommand DbCommand = database.GetStoredProcCommand("WebClockingSave");
                database.AddInParameter(DbCommand, "@ClubName", DbType.String, ClubID);
                database.AddInParameter(DbCommand, "@SMSMobileNumber", DbType.String, SMSMobileNumber);
                database.AddInParameter(DbCommand, "@SMSMobileNumberTo", DbType.String, SMSMobileNumberTo);
                database.AddInParameter(DbCommand, "@StickerCode", DbType.String, Keyword);
                database.AddInParameter(DbCommand, "@Action", DbType.String, Action);
                database.AddInParameter(DbCommand, "@RequestAction", DbType.String, "Mobile");
                return InternalExecuteDataSet(database, DbCommand, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}