using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eclock.DAL
{
    class Common
    {
        #region Constants
        private const string SP_SETUPSAVE = "EclockSetupSave";
        private const string SP_READKEYID = "ReadKeyID";
        #endregion

        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Public Methods
        public DataSet GetSetup()
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ReadSetup","");

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

        public DataSet ElockSetupSave(BIZ.Common common)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_SETUPSAVE,"");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BaudRate", common.BaudRate);
                dbconn.sqlComm.Parameters.AddWithValue("@PortNumber", common.PortNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Drive", common.Drive);
                dbconn.sqlComm.Parameters.AddWithValue("@RFIDKey", common.RFIDKey);
                dbconn.sqlComm.Parameters.AddWithValue("@RFIDKeyBackup", common.RFIDKeybackup);
                dbconn.sqlComm.Parameters.AddWithValue("@Mode", common.Mode);

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
        }
        public DataSet ReadRFIDKey(string KeyIDTypeAction)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_READKEYID,"");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@KeyIDAction", KeyIDTypeAction);

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
        }
        #endregion
    }
}
