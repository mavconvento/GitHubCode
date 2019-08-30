using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class RaceCategoryGroup
    {
        #region Constant
        private const string SP_RaceCategoryGroupGETBYKEY = "RaceCategoryGroupGetbyKey";
        private const string SP_RaceCategoryGroupSAVE = "RaceCategoryGroupSave";
        private const string SP_RaceCategoryGroupDELETE = "RaceCategoryGroupDelete";
        private const string SP_RaceCategoryGroupLIST = "RaceCategoryGroupSelectAll";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String MemberID { get; set; }
        public String EntryID { get; set; }
        public Int64 RaceCategoryGroupID { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public String EntryList { get; set; }
        #endregion

        #region Public Methods
        public DataSet AddEntryCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("AddEntryCategory");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@CategoryName", RaceCategoryGroupName);
                dbconn.sqlComm.Parameters.AddWithValue("EntryList", EntryList);
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

        public DataSet RemoveEntryCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RemoveEntryCategory");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@CategoryName", RaceCategoryGroupName);
                dbconn.sqlComm.Parameters.AddWithValue("EntryList", EntryList);
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

        public DataSet GetEntryCategory()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetEntryCategory");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);

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
        public DataSet RaceCategoryGroupGetByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RaceCategoryGroupGETBYKEY);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
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
                dbconn.DatabaseConn(SP_RaceCategoryGroupSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroupID", RaceCategoryGroupID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroupName", RaceCategoryGroupName);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceCategoryGroupDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RaceCategoryGroupDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroupID", RaceCategoryGroupID);
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
        public DataSet RaceCategoryGroupSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RaceCategoryGroupLIST);

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
