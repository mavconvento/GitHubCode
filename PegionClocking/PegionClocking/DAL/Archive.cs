using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    public class Archive
    {
        #region Constant
        private const string SP_ARCHIVERACERESULT_V1 = "sp_ArchiveRaceResult_v1";
        private const string SP_ARCHIVERACERESULT_V3 = "sp_ArchiveRaceResult_v3";
        private const string SP_ARCHIVERACERESULT_V4 = "sp_ArchiveRaceResult_v4";
        private const string SP_ARCHIVEINBOX_1 = "sp_Archiveinbox";
        private const string SP_ARCHIVEINBOX_2 = "sp_ArchiveInbox2";
        
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        public void ArchiveRaceResultV1()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ARCHIVERACERESULT_V1);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArchiveRaceResultV3()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ARCHIVERACERESULT_V3);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArchiveRaceResultV4()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ARCHIVERACERESULT_V4);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArchiveInbox1()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ARCHIVEINBOX_1);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArchiveInbox2()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_ARCHIVEINBOX_2);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

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
