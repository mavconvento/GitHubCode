using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eclock.BIZ;
using System.Data.SqlClient;

namespace Eclock.DAL
{
    public class Entry
    {
        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        public DataSet GetReleasePointDetails(BIZ.Entry bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_GetReleasePoint", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", bizData.ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);

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

        public DataSet GetMemberEclockEntry(BIZ.Entry bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_GetMemberEclockEntry", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasePointID", bizData.ReleasepointID);

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

        public DataSet VerifyRFID(BIZ.Entry bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_VerifyRFID", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@SerialRFIDNo", bizData.RFIDSerialNo);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasePointID", bizData.ReleasepointID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);

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

        public Boolean Save(BIZ.Entry bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_EclockEntrySave", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@EclockEntryID", bizData.EclockEntryID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasePointID", bizData.ReleasepointID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", bizData.BandID);
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", bizData.BandNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberRegisterRFID", bizData.MemberRFIDRegisterID);
                dbconn.sqlComm.Parameters.AddWithValue("@SerialRFIDNo", bizData.RFIDSerialNo);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Delete(BIZ.Entry bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_EclockEntryDelete", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@EclockEntryID", bizData.EclockEntryID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
