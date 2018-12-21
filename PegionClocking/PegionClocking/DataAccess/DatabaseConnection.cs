using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace DataAccess
{
    public class DatabaseConnection
    {
        #region Constants
        private const string cryptoKey = "cryptoKey";
        #endregion

        #region Variables
        public SqlConnection sqlConn;
        public SqlCommand sqlComm;
        string servername = "";
        string databasename = "";
        string username = "";
        string password = "";
        #endregion

        #region Properties
        public String ConnectionType { get; set; }
        #endregion

        public DatabaseConnection(string contype)
        {
            ConnectionType = contype;
        }

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
        private void ReadConnecntionStringFile()
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                if (ConnectionType == "local")
                {
                    connectionString = sysDir + "\\ConnectionString.inf";
                    if (File.Exists(connectionString))
                    {
                        TextReader tr = new StreamReader(connectionString);
                        using (tr)
                        {
                            servername = tr.ReadLine(); //Decrypt();
                            databasename = tr.ReadLine(); //Decrypt();
                            username = tr.ReadLine(); //Decrypt();
                            password = tr.ReadLine(); //Decrypt();
                        }
                    }
                }
                else
                {
                    servername = "204.93.160.206";
                    databasename = "pigeon_mavcpigeonclocking";
                    username = "sa";
                    password = "06242009";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Public Methods
        public void DatabaseConn(string procName, string clubname = "")
        {
            try
            {
                sqlConn = new SqlConnection();
                sqlComm = new SqlCommand();
                this.ReadConnecntionStringFile();
                if (clubname != "") databasename = "pigeonclocking_" + clubname;
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
