using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMSWindowService.Config;

namespace SMSWindowService.DAL
{
    public class SMSDal
    {
        #region Variable
        DatabaseConnection dbconn;
        #endregion

        public String ActivationCode { get; set; }
        public String ModemID { get; set; }

        public void InboxSave(String SMSID, String SMSContent, String Sender, String SMSDate, String SMSTime, String ModemIDValue)
        {
            try
            {
                //string message = "";

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("InboxSave", "Local");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSID", SMSID);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSDate", SMSDate);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSTime", SMSTime);
                dbconn.sqlComm.Parameters.AddWithValue("@ActivationCode", ActivationCode);
                dbconn.sqlComm.Parameters.AddWithValue("@ModemID", ModemID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                //return message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void OutboxSave(String SMSContent, String Recipient, String Status, String Remarks, Int64 InboxID, String InboxType)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("OutboxSave", "Main");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@Recipient", Recipient);
                dbconn.sqlComm.Parameters.AddWithValue("@Status", Status);
                dbconn.sqlComm.Parameters.AddWithValue("@StatusRemarks", Remarks);
                dbconn.sqlComm.Parameters.AddWithValue("@InboxID", InboxID);
                dbconn.sqlComm.Parameters.AddWithValue("@InboxType", InboxType);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetRecordForReply(String activationcode)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetRecordForReply", "Main");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@activationcode", activationcode);

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
