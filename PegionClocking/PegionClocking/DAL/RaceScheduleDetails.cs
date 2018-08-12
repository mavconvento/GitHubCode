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
