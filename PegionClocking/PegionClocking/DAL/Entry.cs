using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class Entry
    {
        #region Constant
        private const string SP_ENTRYSAVE = "EntrySave";
        private const string SP_ENTRYGETBYRACERELEASEPOINT = "EntryGetByRaceReleasePoint";
        private const string SP_ENTRYDELETE = "EntryDelete";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 EntryID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string RaceCategoryName { get; set; }
        public string RaceCategoryGroupName { get; set; }
        public string StickerCode { get; set; }
        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public string BarcodeBandID { get; set; }
        public string Remarks { get; set; }
        public string MemberIDNo { get; set; }
        public Boolean Isuploaded { get; set; }
        public DateTime ReleaseDate { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetEntryDetailsByEntryBarcodeID()
        {
            try
            {
                 DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetEntryIdentity");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BarcodeID", BarcodeBandID);
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
                
                throw ex;
            }
        }
        public DataSet Save()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ENTRYSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleName", RaceScheduleName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategoryGroupName);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", StickerCode);
                dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", RingNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@BarCodeEntryID", BarcodeBandID);
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", BandID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@EntryRemarks", Remarks);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@IsUpload", Isuploaded);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;

                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveDuplicateEntry()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("SaveDuplicateEntry");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleName", RaceScheduleName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", StickerCode);
                dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", RingNumber);

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
        public DataSet GetLastEntry()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetLastEntry");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasepointID", RaceReleasePointID);
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
        public DataSet EntryGetByRaceReleasePoint()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ENTRYGETBYRACERELEASEPOINT);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
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
        public DataSet EntryGetByMemberIDNo()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ENTRYGETBYRACERELEASEPOINT);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategoryGroupName);
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
        public void MemberDetailsDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ENTRYDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@EntryID", EntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet EClockRegisterRFID()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EClockRegisterRFID");

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
        public DataSet EClockRegisterRFIDMember()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EclockRegisterRFIDMember");

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
