using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMSWindowService.Config;


namespace SMSWindowService.DAL
{
    public class SMSIntegrateDal
    {
        #region Variable
        DatabaseConnection dbconn;
        #endregion

        public DataSet GetInbox()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetInbox","Local");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlComm.Connection.Close();
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateInboxImport(string id,string replyMessage)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("UpdateInboxImport", "Local");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", id);
                dbconn.sqlComm.Parameters.AddWithValue("@ReplyMessage", replyMessage);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet SaveInbox(string SMSID, string SMSContent, string Sender, string SMSDate, string SMSTime, string ActivationCode, string ModemID, string Isprocess, string Source, string IsStickerNo, string Value)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("IntegrateInboxSave", "Web");

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
                dbconn.sqlComm.Parameters.AddWithValue("@IsStickerNo", IsStickerNo);
                dbconn.sqlComm.Parameters.AddWithValue("@Value", Value);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dtResult);
                dbconn.sqlComm.Connection.Close();
                dbconn.sqlConn.Close();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
