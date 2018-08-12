using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class Location
    {
        #region Constant
        private const string SP_LocationSAVE = "LocationSave";
        private const string SP_LocationDELETE = "LocationDelete";
        private const string SP_LOCATIONSEARCHBYKEYS = "LocationSearchByKey";
        private const string SP_LOCATIONLIST = "LocationSelectAll";
        private const string SP_LOCATIONBYREGION = "LocationSelectByRegion";
        private const string SP_LOCATIONSEARCHBYSCHEDULECATEGORY = "LocationSearchbyScheduleCategory";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ScheduleCategoryID { get; set; }
        public Int64 ScheduleID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public String RegionName { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }
        #endregion

        #region Public Methods
        public void Save()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LocationSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationID", LocationID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationName", LocationName);
                dbconn.sqlComm.Parameters.AddWithValue("@RegionName", RegionName);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", DistanceLatDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", DistanceLatMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecond", DistanceLatSecond);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", DistanceLatSign);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", DistanceLongDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", DistanceLongMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecond", DistanceLongSecond);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", DistanceLongSign);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LocationDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LocationDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@LocationID", LocationID);
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
        public DataSet LocationSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LOCATIONLIST);

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
        public DataSet LocationSelectByRegion()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LOCATIONBYREGION);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleID", ScheduleID);

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
        public DataSet LocationSearchbyScheduleCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LOCATIONSEARCHBYSCHEDULECATEGORY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleCategoryID", ScheduleCategoryID);

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
                dbconn.sqlComm.Parameters.AddWithValue("@LocationID", LocationID);
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
