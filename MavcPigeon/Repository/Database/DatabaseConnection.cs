using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols;

namespace Repository.Database
{
    public class DatabaseConnection
    {
       // private readonly IConfiguration _configuration;

        #region Constants
        private const string cryptoKey = "cryptoKey";
        private const string serverip = "198.38.94.72"; //server 1
        //private const string serverip = "198.38.86.120"; //server 2
        #endregion

        #region Variables
        DatabaseConnection dbconn;
        public SqlConnection sqlConn;
        public SqlCommand sqlComm;
        string servername = "";
        string databasename = "";
        string username = "";
        string password = "";
        #endregion

        #region Private Methods
        private static readonly byte[] EncryptDecrypt = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
        private async Task<string> GetDBName(String ClubID)
        {


            try
            {
                string dbName = "";
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetDBName");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    if (dataResult.Tables[0].Rows.Count > 0)
                    {
                        dbName = dataResult.Tables[0].Rows[0]["dbName"].ToString();
                    }
                }

                return await Task.FromResult(dbName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }
        #endregion

        #region Public Methods
        public async void DatabaseConn(string procName,string type = "", string dbName = "")
        {
            try
            {
                
               
                sqlConn =new SqlConnection();
                sqlComm=new SqlCommand();

                string connstr = "Address=" + serverip + ";database=pigeon_mavcpigeonclocking;user id=sa;pwd=06242009";

                if (type != "" || dbName != "")
                {
                    if (String.IsNullOrEmpty(dbName))
                    {
                        dbName = await this.GetDBName(type);
                    }

                    connstr = "Address=" + serverip  + ";database=pigeonclocking_" + dbName + ";user id=sa;pwd=06242009";
                }
                sqlConn.ConnectionString = connstr;
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
