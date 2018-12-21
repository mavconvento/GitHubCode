using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.GatewaySMSIntegration
{
    public class GatewaySMS
    {
        #region Constant
        #endregion

        #region Variables
        DataAccess.DatabaseConnection dbconn;
        #endregion

        public DataSet SMSGatewayOutboxSave(string dbSource,string SMSContent, string Keyword, string Sender, string Status)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("SMSGatewayOutboxSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@Keyword", Keyword);
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@Status", Status);
                dbconn.sqlComm.ExecuteNonQuery();
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

        public DataSet GetRecordForReply(string dbSource, string activationcode, string inboxID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetRecordForReply");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@activationcode", activationcode);
                dbconn.sqlComm.Parameters.AddWithValue("@inboxID", inboxID);

                SqlDataAdapter da = new SqlDataAdapter();
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

        public DataSet UpdateInboxReply(string dbSource, string ID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("UpdateInboxReply");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.ExecuteNonQuery();
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
    }
}
