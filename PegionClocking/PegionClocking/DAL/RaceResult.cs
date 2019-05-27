using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class RaceResult
    {
        #region Constant
        private const string SP_RACERESULTGETBYKEYS = "RaceResultGetbyKeys";
        private const string SP_RACERESULTGETBYSCHEDULECATEGORY = "RaceResultGetByScheduleCategory";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public DateTime DateRelease { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string RaceCategoryName { get; set; }
        public string RaceCategoryGroupName { get; set; }
        public String Sender { get; set; }
        public String StickerCode { get; set; }
        public String Arrival { get; set; }
        public String PigeonID { get; set; }
        #endregion

        #region Public Methods
        public DataSet RaceResultGetByKeys()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERESULTGETBYKEYS);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", DateRelease.Date);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategoryGroupName);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultAddFromBackup(string source)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@Content", "");
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerNumber", StickerCode);
                dbconn.sqlComm.Parameters.AddWithValue("@Arrival", Arrival);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleaseDate", DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", source);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ViewClubRace()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ViewClubRace");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@Date", DateRelease);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", StickerCode);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonID);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultGetByScheduleCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERESULTGETBYSCHEDULECATEGORY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategoryGroupName);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
