using Repository.Contracts;
using Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helper
{
    public class CommonRepository : ICommonRepository
    {
        #region Variables
        DatabaseConnection dbconn;

        public async Task<DataSet> ChargeText(string mobilenumber)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ChargeText");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", mobilenumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Multiplier", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();


                return await Task<DataSet>.FromResult(dataResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }

        public async Task<DataSet> WebClockingSave(string ClubName, string SMSMobileNumber, string Keyword, string Action, string UserID = "", string SMSMobileNumberTo = "", string dbName = "")
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("WebClockingSave", ClubName, dbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSMobileNumber", SMSMobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSMobileNumberTo", SMSMobileNumberTo);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", Keyword);
                dbconn.sqlComm.Parameters.AddWithValue("@Action", Action);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RequestAction", "Mobile");
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromPilipinasKalapati", true);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task<DataSet>.FromResult(dataResult);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }
        #endregion Variables
    }
}
