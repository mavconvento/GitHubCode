using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eclock.BIZ;
using System.Data.SqlClient;

namespace Eclock.DAL
{
    public class RegisterRFID
    {
        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Public Methods
        public DataSet GetMemberDetails(BIZ.RegisterRFID bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_MemberDetailsSearchByKey", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);

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
        public DataSet GetSeason(BIZ.RegisterRFID bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_GetSeason", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);

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
        public DataSet GetMemberRFIDRegisterGetByID(BIZ.RegisterRFID bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_MemberRFIDRegisrationGeyByID", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleName", bizData.Season);
                dbconn.sqlComm.Parameters.AddWithValue("@Clubid", bizData.ClubID);

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
        public Boolean Save(BIZ.RegisterRFID bizData)
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_MemberRFIDRegisrationSave", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberRegistrationID", bizData.MemberRFIDRegistrationID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", bizData.MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleName", bizData.Season);
                dbconn.sqlComm.Parameters.AddWithValue("@RingType", bizData.Type);
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", bizData.BandNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", bizData.BandID);
                dbconn.sqlComm.Parameters.AddWithValue("@RFIDSerialNo", bizData.RFID);
                dbconn.sqlComm.Parameters.AddWithValue("@Picture", "");
                dbconn.sqlComm.Parameters.AddWithValue("@BirdCategory", bizData.BirdCategory);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        
        }
        public Boolean Delete(BIZ.RegisterRFID bizData)
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_MemberRFIDRegisrationDelete", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberRegistrationID", bizData.MemberRFIDRegistrationID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
    }
}
