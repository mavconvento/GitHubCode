using System;
using System.Collections.Generic;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using MavcPigeonClockingPortal.Constants;
using MavcPigeonClockingPortal.Models;

//--common dll
using LWT.Common.DAL;
using LWT.Common;

namespace MavcPigeonClockingPortal.DAL
{
    public class Common : BaseDAL
    {
        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion Variables

        public DataSet ChargeText(string mobilenumber)
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
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID",0);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }
        public DataSet WebClockingSave(String ClubID, String SMSMobileNumber, String Keyword, String Action,string UserID = "", String SMSMobileNumberTo = "")
        {

            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("WebClockingSave", ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
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
                return dataResult;
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

        public String GetDBName(String ClubID)
        {

           
            try
            {
                string dbName = "";
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetDBName");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    if (dataResult.Tables[0].Rows.Count > 0)
                    {
                        dbName = dataResult.Tables[0].Rows[0]["dbName"].ToString();
                    }
                }

                return dbName;
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
    }
}