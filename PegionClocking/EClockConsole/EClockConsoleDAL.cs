using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EClockConsole
{
    class EClockConsoleDAL
    {
        #region Constant
        #endregion

        #region Variables
        EClockConsole.DatabaseConnection dbconn;
        #endregion

        #region Properties
        //public MAVC_IntegrationV2.IntegrationBLL integratedBLL { get; set; }
        #endregion

        public DataSet GetBirdArrivedNotProcess(string dbSource)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("Eclock_GetBirdArrivedForProcess");

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

        public DataSet EclockSaveReply(string mobilenumber,string rfid, DateTime arrival, string dbSource)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("SaveEclockForReply");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("MobileNumber", mobilenumber);
                dbconn.sqlComm.Parameters.AddWithValue("RFID", rfid);
                dbconn.sqlComm.Parameters.AddWithValue("Arrival", arrival);

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

        public DataSet UpdateBirdProcess(string ID, string dbSource)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("Eclock_UpdateBirdArrivedProcess");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);

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
    }
}
