using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class Club
    {
        #region Constant
        private const string SP_ClubSAVE = "ClubSave";
        private const string SP_ClubDELETE = "ClubDelete";
        private const string SP_ClubSEARCHBYKEYS = "ClubSearchByKey";
        private const string SP_ClubLIST = "ClubSelectAll";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public String ClubAbbreviation { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }
        public String Version { get; set; }
        public String DateTimeSource { get; set; }
        public Boolean IsBackUp { get; set; }
        public Boolean IsMAVCStickerUsed { get; set; }
        public DateTime LastSubcription { get; set; }
        public DateTime SubcriptionDate { get; set; }
        public String Server { get; set; }
        #endregion

        #region Public Methods
        public void Save()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ClubSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubAbbreviation", ClubAbbreviation);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", DistanceLatDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", DistanceLatMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecond", DistanceLatSecond);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", DistanceLatSign);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", DistanceLongDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", DistanceLongMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecond", DistanceLongSecond);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", DistanceLongSign);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", Version);
                dbconn.sqlComm.Parameters.AddWithValue("@IsBackup", IsBackUp);
                dbconn.sqlComm.Parameters.AddWithValue("@IsMAVCStickerUsed", IsMAVCStickerUsed);
                dbconn.sqlComm.Parameters.AddWithValue("@DateTimeSource", DateTimeSource);
                dbconn.sqlComm.Parameters.AddWithValue("@LastSubcriptionDate", LastSubcription);
                dbconn.sqlComm.Parameters.AddWithValue("@SubcriptionDate", SubcriptionDate);
                dbconn.sqlComm.Parameters.AddWithValue("@Server", Server);
                dbconn.sqlComm.Parameters.AddWithValue("@IsPilipinasKalapati", true);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ClubDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ClubDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
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
        public DataSet ClubSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ClubLIST);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@IsAll", true);

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
        public DataSet ClubSearchByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ClubSEARCHBYKEYS);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
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
