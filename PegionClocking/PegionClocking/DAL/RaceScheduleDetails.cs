using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class RaceScheduleDetails
    {
        #region Constant
        private const string SP_SCHEDULEDETAILSSAVE = "RaceScheduleDetailsSave";
        private const string SP_SCHEDULEDETAILSDELETE = "RaceScheduleDetailsDelete";
        private const string SP_LOCATIONSEARCHBYKEYS = "LocationSearchByKey";
        private const string SP_SCHEDULEDETAILSLIST = "RaceSceduleDetailsSelectAll";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ID { get; set; }
        public Int64 RaceScheduleID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public DateTime DateRelease { get; set; }
        public DateTime Loading { get; set; }
        public string LoadingTimeFrom { get; set; }
        public string LoadingTimeTo { get; set; }

        //release point
        public Int64 RaceReleasePointID { get; set; }
        public string ReleaseTime { get; set; }
        public Int64 LapNo { get; set; }
        public double Multiplier { get; set; }
        public string MinSpeed { get; set; }
        public bool IsStop { get; set; }
        public DateTime StopFromDate { get; set; }
        public string StopFromTime { get; set; }
        public DateTime StopToDate { get; set; }
        public string StopToTime { get; set; }
        public String Description { get; set; }
        #endregion

        #region Public Methods
        public void AddLocation()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_SCHEDULEDETAILSSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleID", RaceScheduleID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationID", LocationID);
                dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@Loading", Loading);
                dbconn.sqlComm.Parameters.AddWithValue("@LoadingTimeFrom", LoadingTimeFrom);
                dbconn.sqlComm.Parameters.AddWithValue("@LoadingTimeTo", LoadingTimeTo);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@TimeReleased", ReleaseTime);
                dbconn.sqlComm.Parameters.AddWithValue("@Multiplier", Multiplier);
                dbconn.sqlComm.Parameters.AddWithValue("@LapNo", LapNo);
                dbconn.sqlComm.Parameters.AddWithValue("@MinSpeed", MinSpeed);
                dbconn.sqlComm.Parameters.AddWithValue("@IsStop", IsStop);
                dbconn.sqlComm.Parameters.AddWithValue("@StopFromDate", StopFromDate);
                dbconn.sqlComm.Parameters.AddWithValue("@StopFromTime", StopFromTime);
                dbconn.sqlComm.Parameters.AddWithValue("@StopToDate", @StopToDate);
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
        public void RemoveLocation()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_SCHEDULEDETAILSDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
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
        public DataSet ScheduleDetailsSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_SCHEDULEDETAILSLIST);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleID", RaceScheduleID);

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
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationName", LocationName);

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
