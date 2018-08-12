using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MAVC_IntegrationV2
{
    public class IntegrationDAL
    {

        #region Constant
        #endregion

        #region Variables
        MAVC_IntegrationV2.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public MAVC_IntegrationV2.IntegrationBLL  integratedBLL { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetFileNote()
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
        #endregion

        #region Private Methods
        
        #endregion
    }
}
