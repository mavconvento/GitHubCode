using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    public class Inbox
    {
        #region Constant
        private const string SP_INBOXVIEW = "InboxView";    
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        public DataSet GetInbox(string sender, DateTime dateFrom, DateTime dateTo,string keyword,Int64 clubid)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_INBOXVIEW);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@sender", sender);
                dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredFrom", dateFrom.Date);
                dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredTO", dateTo.Date);
                dbconn.sqlComm.Parameters.AddWithValue("@keyword", keyword);
                dbconn.sqlComm.Parameters.AddWithValue("@clubid", clubid);
                dbconn.sqlComm.Parameters.AddWithValue("@IsPilipinasKalapati", true);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlComm.Connection.Close();
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
    }
}
