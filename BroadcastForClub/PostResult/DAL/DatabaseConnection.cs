using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PostResult.DAL
{
    class DatabaseConnection
    {
        #region Variables
        public SqlConnection sqlConn;
        public SqlCommand sqlComm;
        string servername = "";
        string databasename = "";
        string username = "";
        string password = "";
        #endregion

        #region Private Methods
        private void ReadConnecntionStringFile()
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\TextFile\\ConnectionString.inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        servername = tr.ReadLine();
                        databasename = tr.ReadLine();
                        username = tr.ReadLine();
                        password = tr.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Public Methods
        public void DatabaseConn(string procName)
        {
            try
            {
                sqlConn = new SqlConnection();
                sqlComm = new SqlCommand();
                this.ReadConnecntionStringFile();
                sqlConn.ConnectionString = "Address=" + servername + ";database=" + databasename + ";user id=" + username + ";pwd=" + password;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = procName;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
