using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class RaceScheduleCategory
    {
        #region Constant
        private const string SP_RACESHEDULESEARCHBYRACESCHEDULE = "RaceScheduleCategorySearchByRaceSchedule";
        private const string SP_RACESHEDULECATEGORYSAVE = "RaceScheduleCategorySave";
        private const string SP_RACESHEDULECATEGORYDELETE = "RaceScheduleCategoryDelete";
        private const string SP_RACESHEDULECATEGORYSELECTALL = "RaceScheduleCategorySelectAll";
        private const string SP_RACESHEDULECATEGORYSEARCHBYKEY = "RaceScheduleCategorySearchByKey";
        #endregion
            
        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        //public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceScheduleID { get; set; }
        public String RaceScheduleName { get; set; }
        public Int64 RaceScheduleCategoryID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public Int64 Lap { get; set; }
        #endregion

        #region Public Methods
        public DataSet RaceScheduleCategoryGetByRaceSchedule()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACESHEDULESEARCHBYRACESCHEDULE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceSchedule", RaceScheduleName);

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
        public DataSet RaceScheduleCategoryGetByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACESHEDULECATEGORYSEARCHBYKEY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID", RaceScheduleCategoryID);
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
        public void Save()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACESHEDULECATEGORYSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleID", RaceScheduleID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID", RaceScheduleCategoryID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@Lap", Lap);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceScheduleCategoryDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACESHEDULECATEGORYDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID", RaceScheduleCategoryID);
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
        public DataSet RaceScheduleCategorySelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACESHEDULECATEGORYSELECTALL);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleID", RaceScheduleID);

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
