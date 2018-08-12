using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Eclock.DAL
{
    class DatabaseConnection
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
        private void ReadConnecntionStringFile(string dbType = "")
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\ConnectionString" + dbType + ".inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        servername = Decrypt(tr.ReadLine());
                        databasename = Decrypt(tr.ReadLine());
                        username = Decrypt(tr.ReadLine());
                        password = Decrypt(tr.ReadLine());
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
        public void DatabaseConn(string procName,string dbType)
        {
            try
            {
                sqlConn=new SqlConnection();
                sqlComm=new SqlCommand();
                this.ReadConnecntionStringFile(dbType);
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
