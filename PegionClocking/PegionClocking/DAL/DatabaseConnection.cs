using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml;

namespace PegionClocking.DAL
{
    class DatabaseConnection
    {
        #region Constants
        //production
        private const string cryptoKey = "cryptoKey";
        private const string SERVERNAME = "204.93.160.206";
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
        private void ReadConnecntionStringFile(string type = "")
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\ConnectionString" + type + ".inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        servername = SERVERNAME; //Decrypt(tr.ReadLine());
                        databasename = DATABASENAME; //Decrypt(tr.ReadLine());
                        username = USERNAME; //Decrypt(tr.ReadLine());
                        password = PASSWORD; //Decrypt(tr.ReadLine());
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
        public void DatabaseConn(string procName,string type = "")
        {
            try
            {
                sqlConn=new SqlConnection();
                sqlComm=new SqlCommand();
                this.ReadConnecntionStringFile(type);
                if (Common.Global.ClubNameConnection != "" && !Common.Global.IsMainDatabase) databasename = "pigeonclocking" + Common.Global.ClubNameConnection;
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
