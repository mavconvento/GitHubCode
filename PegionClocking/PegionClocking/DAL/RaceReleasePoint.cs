using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class RaceReleasePoint
    {
        #region Constant
        private const string SP_RACEENTRYRELEASEPOINTSUMMARY = "BirdEntryReleasePointSummary";
        private const string SP_RACERELEASEPOINTGEYBYSCHEDULECATEGORY = "RaceReleasePointGetbyRaceScheduleCategory";
        private const string SP_RACERELEASEPOINTGETBYKEY = "RaceReleasePointGetbyKey";
        private const string SP_RACERELEASEPOINTSAVE = "RaceReleasePointSave";
        private const string SP_RACERELEASEPOINTDELETE = "RaceReleasePointDelete";
        private const string SP_RACERELEASEPOINTLIST = "RaceReleasePointSelectAll";
        private const string SP_LOCATIONSEARCHBYKEYS = "LocationSearchbyScheduleCategory";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        //public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String LocationName { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public Int64 RaceScheduleCategoryID { get; set; }
        public Int64 RaceScheduleDetailsID { get; set; }
        public string ReleaseTime { get; set; }
        public string ReleaseDate { get; set; }
        public double Multiplier { get; set; }
        public Int64 LapNo { get; set; }
        public string MinSpeed { get; set; }
        public bool IsStop { get; set; }
        public DateTime StopFromDate { get; set; }
        public string StopFromTime { get; set; }
        public DateTime StopToDate { get; set; }
        public string StopToTime { get; set; }
        public String RaceScheduleName { get; set; }
        public String Description { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        #endregion

        #region Public Methods
        public DataSet RaceReleasePointGetbyRaceScheduleCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERELEASEPOINTGEYBYSCHEDULECATEGORY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);

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
        public DataSet RaceReleasePointSummary()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACEENTRYRELEASEPOINTSUMMARY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryName", RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroupName", RaceCategoryGroupName);

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
        public DataSet GetRaceReleasePointBySchedule()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetRaceReleasePointBySchedule");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleName", RaceScheduleName);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                
                throw  ex;
            }
        }
        public void Save()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERELEASEPOINTSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID", RaceScheduleCategoryID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleDetailsID", RaceScheduleDetailsID);
                dbconn.sqlComm.Parameters.AddWithValue("@TimeReleased", ReleaseTime);
                dbconn.sqlComm.Parameters.AddWithValue("@LapNo", LapNo);
                dbconn.sqlComm.Parameters.AddWithValue("@Multiplier", Multiplier);
                dbconn.sqlComm.Parameters.AddWithValue("@MinSpeed", MinSpeed);
                dbconn.sqlComm.Parameters.AddWithValue("@IsStop", IsStop);
                dbconn.sqlComm.Parameters.AddWithValue("@StopFromDate", StopFromDate);
                dbconn.sqlComm.Parameters.AddWithValue("@StopFromTime", StopFromTime);
                dbconn.sqlComm.Parameters.AddWithValue("@StopToDate", StopToDate);
                dbconn.sqlComm.Parameters.AddWithValue("@StopToTime", StopToTime);
                dbconn.sqlComm.Parameters.AddWithValue("@Description", Description);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceReleasePointDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERELEASEPOINTDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceReleasePointSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERELEASEPOINTLIST);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID", RaceScheduleCategoryID);

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
        public DataSet RaceReleasePointGetbyKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERELEASEPOINTGETBYKEY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);

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
        public DataSet LocationSearchByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LOCATIONSEARCHBYKEYS);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleCategoryID", RaceScheduleCategoryID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationName", LocationName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

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
        public DataSet EClockRaceReleasePoint()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EclockRaceReleasePoint");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
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
        #endregion

        #region Private Methods
        #endregion
    }
}
