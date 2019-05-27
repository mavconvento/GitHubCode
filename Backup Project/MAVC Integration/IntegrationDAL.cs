using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MAVC_Integration
{
    public class IntegrationDAL
    {

        #region Constant
        #endregion

        #region Variables
        MAVC_Integration.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public MAVC_Integration.IntegrationBLL bll { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetRecord()
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection("local");
                dbconn.DatabaseConn("FileNotesGetAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();

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
        }
        public DataSet TransferRecord()
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection("local");
                dbconn.DatabaseConn("FileNotesIntegrateData");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@AccountID", bll.AccountID);
                dbconn.sqlComm.Parameters.AddWithValue("@AccountName", bll.AccountName);
                dbconn.sqlComm.Parameters.AddWithValue("@Action", bll.Action);
                dbconn.sqlComm.Parameters.AddWithValue("@ExternalClubID", bll.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@FileNotesID", bll.FileNotesID);

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
        }
        #endregion

        #region Private Methods
        
        #endregion
    }
}
