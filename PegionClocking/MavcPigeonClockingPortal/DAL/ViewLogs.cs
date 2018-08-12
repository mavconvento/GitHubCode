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
    public class ViewLogs : BaseDAL
    {

        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion Variables

        public DataSet InboxView(string clubid,String sender, String keyword, DateTime dateCoveredFrom, DateTime dateCoveredTO)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("InboxView",clubid);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@sender", sender);
                dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredFrom", dateCoveredFrom);
                dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredTO", dateCoveredTO);
                dbconn.sqlComm.Parameters.AddWithValue("@keyword", keyword);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 9000);
                dbconn.sqlComm.Parameters.AddWithValue("@IsPilipinasKalapati", true);

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
    }
}