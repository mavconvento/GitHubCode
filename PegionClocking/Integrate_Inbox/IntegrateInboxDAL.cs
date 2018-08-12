using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Inbox
{
    class IntegrateInboxDAL
    {
        #region Constant
        #endregion

        #region Variables
        Integrate_Inbox.DatabaseConnection dbconn;
        #endregion

        #region Properties
        //public MAVC_IntegrationV2.IntegrationBLL integratedBLL { get; set; }
        #endregion

        public DataSet GetInbox(string dbSource,string modemID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetInbox");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                
                SqlDataAdapter da = new SqlDataAdapter();
                dbconn.sqlComm.Parameters.AddWithValue("@ModemID", modemID);
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dtResult);
                dbconn.sqlConn.Close();

                return dtResult;
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

        public void UpdateInboxImport(string dbSource,string id,string replyMessage,string keyword)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("UpdateInboxImport");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", id);
                dbconn.sqlComm.Parameters.AddWithValue("@ReplyMessage", replyMessage);
                dbconn.sqlComm.Parameters.AddWithValue("@Keyword", keyword);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
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

        public void SaveInbox(string dbSource, string SMSID, string SMSContent, string Sender, string SMSDate, string SMSTime, string ActivationCode, string ModemID, string Isprocess, string Source,out string ReplyMessage, out string Keyword)
        {
            try
            {
                DataSet dtResult = new DataSet();
                string clubname = ValidateClub("local", SMSContent);
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("InboxSave", clubname);
                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSID", SMSID);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSDate", SMSDate);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSTime", SMSTime);
                dbconn.sqlComm.Parameters.AddWithValue("@ActivationCode", ActivationCode);
                dbconn.sqlComm.Parameters.AddWithValue("@ModemID", ModemID);
                dbconn.sqlComm.Parameters.AddWithValue("@Isprocess", Isprocess);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", Source);
                dbconn.sqlComm.Parameters.AddWithValue("@IsImport", true);
                if (clubname != "") dbconn.sqlComm.Parameters.AddWithValue("@IsFromPilipinasKalapati", true);
                dbconn.sqlComm.Parameters.Add("@ReplyMessage", SqlDbType.VarChar,5000).Direction = ParameterDirection.Output;
                dbconn.sqlComm.Parameters.Add("@Keyword", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                dbconn.sqlComm.ExecuteNonQuery();
                
                //Return ReplyMessage to save in the local table
                ReplyMessage = Convert.ToString(dbconn.sqlComm.Parameters["@ReplyMessage"].Value);
                Keyword = Convert.ToString(dbconn.sqlComm.Parameters["@Keyword"].Value);

                dbconn.sqlConn.Close();

                //return ReplyMessage;
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

         public void SaveInboxLocal(string dbSource, string SMSID, string SMSContent, string Sender, string SMSDate, string SMSTime, string ActivationCode, string ModemID, string Isprocess, string Source,string ReplyMessage, string Keyword)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("InboxSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSID", SMSID);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSDate", SMSDate);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSTime", SMSTime);
                dbconn.sqlComm.Parameters.AddWithValue("@ActivationCode", ActivationCode);
                dbconn.sqlComm.Parameters.AddWithValue("@ModemID", ModemID);
                dbconn.sqlComm.Parameters.AddWithValue("@Isprocess", Isprocess);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", Source);
                dbconn.sqlComm.Parameters.AddWithValue("@IsImport", true);
                dbconn.sqlComm.Parameters.AddWithValue("@ReplyMessage", ReplyMessage);
                dbconn.sqlComm.Parameters.AddWithValue("@Keyword", @Keyword);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
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

        public DataSet GetSMSModemID(string dbSource)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("SMSViewer_GetModemIDImport");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                //dbconn.sqlComm.Parameters.AddWithValue("@Club", Club);
                

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

        private string ValidateClub(string dbSource,string smscontent)
        {
            try
            {
                string clubName = "";
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetClubPilipinasKalapati");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", smscontent);
                dbconn.sqlComm.Parameters.Add("@ClubName", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                dbconn.sqlComm.ExecuteNonQuery();

                clubName = Convert.ToString(dbconn.sqlComm.Parameters["@ClubName"].Value);

                dbconn.sqlConn.Close();

                return clubName; 
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
