using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class DatabaseConnection
    {
        #region Constants
        //production
        private const string cryptoKey = "cryptoKey";
        private const string SERVERNAME = "198.38.94.72";
        private const string SERVERNAME2 = "198.38.86.120";
        private const string DATABASENAME = "pigeon_mavcpigeonclocking";
        private const string USERNAME = "sa";
        private const string PASSWORD = "06242009";

        //local
        //private const string cryptoKey = "cryptoKey";
        //private const string SERVERNAME = @"PHPPC03X4NW\SQLEXPRESS";
        //private const string DATABASENAME = "pigeon_mavcpigeonclocking";
        //private const string USERNAME = "sa";
        //private const string PASSWORD = "06081986cv5M";
        #endregion

        #region Variables
        public SqlConnection sqlConn;
        public SqlCommand sqlComm;
        string servername = "";
        string databasename = "";
        string username = "";
        string password = "";
        #endregion

        #region Private Methods
        private static readonly byte[] EncryptDecrypt = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };
        private static string Encrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = EncryptDecrypt;
                result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }
            return result;
        }
        private static string Decrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Convert.FromBase64String(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = EncryptDecrypt;
                result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }
            return result;
        }
        private void ReadConnecntionStringFile(string type = "",bool islocal = false)
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + @"\SyncApplication\mavcserver" + type + ".txt";
                string rootconnectionString = sysDir + @"\mavcserver" + type + ".txt";
                string localRootConString = sysDir + @"\localserver" + type + ".txt";
                string server = "";


                if (islocal)
                {
                    if (File.Exists(localRootConString))
                    {
                        TextReader tr = new StreamReader(localRootConString);
                        using (tr)
                        {
                            servername = tr.ReadLine();
                        }
                    }
                }
                else
                {
                    //check if file is in root directory
                    if (File.Exists(rootconnectionString))
                    {
                        TextReader tr = new StreamReader(rootconnectionString);
                        using (tr)
                        {
                            servername = tr.ReadLine();
                        }
                    }
                    else if (File.Exists(connectionString))
                    {
                        TextReader tr = new StreamReader(connectionString);
                        using (tr)
                        {
                            server = tr.ReadLine();
                        }
                    }

                    if (server == "server 2")
                    {
                        servername = SERVERNAME2; //Decrypt(tr.ReadLine());
                    }
                    else
                        servername = SERVERNAME; //Decrypt(tr.ReadLine());
                }
                
                databasename = DATABASENAME; //Decrypt(tr.ReadLine());
                username = USERNAME; //Decrypt(tr.ReadLine());
                password = PASSWORD; //Decrypt(tr.ReadLine());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Public Methods
        public void DatabaseConn(string procName, string dbname = "")
        {
            try
            {
                sqlConn = new SqlConnection();
                sqlComm = new SqlCommand();
                this.ReadConnecntionStringFile(dbname);
                databasename = "pigeon_mavcpigeonclocking";
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

        public void DatabaseConnTopPigeon(string procName, string dbname = "")
        {
            try
            {
                sqlConn = new SqlConnection();
                sqlComm = new SqlCommand();
                this.ReadConnecntionStringFile(dbname, true);
                databasename = "TopPigeonEclock";
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
