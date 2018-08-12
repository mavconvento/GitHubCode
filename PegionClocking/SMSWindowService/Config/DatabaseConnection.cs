using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using SMSWindowService.Entity;
using System.Xml;

namespace SMSWindowService.Config
{
    public class DatabaseConnection
    {
        #region Constants
        private const string cryptoKey = "cryptoKey";
        #endregion

        #region Variables
        XMLConfig oSetting;
        XmlNode oNode;
        XmlNode oNodeWithEncryption;
        public SqlConnection sqlConn;
        public SqlCommand sqlComm;
        string connectionString = "";

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
        #endregion

        #region Public Methods
        public void DatabaseConn(string procName, string dbType)
        {
            try
            {

                oSetting = Entity.Config.GetConfig();
                oNode = oSetting.SystemSettingsXML.SelectSingleNode("databaseConnection" + dbType);
                oNodeWithEncryption = oSetting.SystemSettingsXML.SelectSingleNode("withencryption");
                connectionString = oNode.InnerXml;

                if (oNodeWithEncryption.InnerXml == "yes")
                {
                    connectionString = Decrypt(connectionString);
                }

                sqlConn = new SqlConnection();
                sqlComm = new SqlCommand();
                sqlConn.ConnectionString = connectionString;
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
