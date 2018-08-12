using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProcessInbox
{
    class DAL
    {
        #region Constant
        #endregion

        #region Variables
        DatabaseConnection dbconn;
        #endregion

        #region Properties
        //public MAVC_IntegrationV2.IntegrationBLL integratedBLL { get; set; }
        #endregion

        public DataSet ProcessInbox(string dbSource)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("ProcessInboxContent");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateInboxImport(string dbSource, string id)
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
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SaveInbox(string dbSource, string SMSID, string SMSContent, string Sender, string SMSDate, string SMSTime, string ActivationCode, string ModemID, string Isprocess, string Source)
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
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
