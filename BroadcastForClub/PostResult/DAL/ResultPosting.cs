using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PostResult.DAL
{
    class ResultPosting
    {
        #region Constant
        private const string SP_RACERESULT = "ResultPosting";
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public DataSet RaceResult(string Club)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RACERESULT);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@Club", Club);

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
        public DataSet GetClubList()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ClubSelectAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();

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

        #region Private Methods
        private string GetClubName()
        {
            try
            {
                string club="";
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\TextFile\\ClubName.txt";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        club = tr.ReadLine();
                    }
                }
                return club;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
