using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PigeonProgram.DAL
{
    class User
    {
        #region Constant
        private const string SP_LOGIN = "UserLogin";
        private const string SP_UserSAVE = "UserSave";
        private const string SP_UserDELETE = "UserDelete";
        private const string SP_UserLIST = "UserSelectAll";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public Int64 UserID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Version { get; set; }

        public BIZ.User BLLUser { get; set; }
        #endregion

        #region Public Methods
        public DataSet Login()
        {
            try 
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_LOGIN);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", UserName);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", Password);
                //dbconn.sqlComm.Parameters.AddWithValue("@Version", Version);
                //dbconn.sqlComm.Parameters.AddWithValue("@Domain", "pilipinaskalapati");

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
        public DataSet PedigreeSetup()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PedigreeSetup");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", BLLUser.Name);
                dbconn.sqlComm.Parameters.AddWithValue("@LoftName", BLLUser.LoftName);
                dbconn.sqlComm.Parameters.AddWithValue("@Address", BLLUser.Address);
                dbconn.sqlComm.Parameters.AddWithValue("@ContactNumber", BLLUser.ContactNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Logo", BLLUser.Logo);
                dbconn.sqlComm.Parameters.AddWithValue("@Resolution", BLLUser.Resolution);
                dbconn.sqlComm.Parameters.AddWithValue("@ResolutionY", BLLUser.ResolutionY);
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
                dbconn.DatabaseConn(SP_UserSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", UserName);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", Password);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ChangePassword()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_UserSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", Password);
                dbconn.sqlComm.Parameters.AddWithValue("@ChangePassword", true);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UserDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_UserDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet UserSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_UserLIST);

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
