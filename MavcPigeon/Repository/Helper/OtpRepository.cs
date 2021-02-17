using DomainObject;
using DomainObject.AppServices;
using Repository.Contracts;
using Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helper
{
    public class OtpRepository : IOTPCodeRepository
    {
        DatabaseConnection dbconn;

        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        private readonly MavcPigeonDBContext _dbcontext;

        public OtpRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }
        private string GenerateRandomOTP(int iOTPLength)

        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;
        }

        public async Task<string> ValidateOTPCode(LinkMobile linkMobile)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ValidateOTPCode");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobilenumber", linkMobile.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@otpcode", linkMobile.OtpCode);
                dbconn.sqlComm.Parameters.AddWithValue("@userid", linkMobile.UserId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult("LinkMobileNumber");
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
        public async Task<string> GetOtpCode(LinkMobile linkMobile)
        {
            try
            {

                //generate otp
                linkMobile.OtpCode = GenerateRandomOTP(5);
                linkMobile.ReferenceID = GenerateRandomOTP(5);

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("OtpCodeSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobilenumber", linkMobile.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@otpcode", linkMobile.OtpCode);
                dbconn.sqlComm.Parameters.AddWithValue("@referenceNo", linkMobile.ReferenceID);
                dbconn.sqlComm.Parameters.AddWithValue("@userid", linkMobile.UserId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task.FromResult(dataResult.Tables[0].Rows[0]["ReferenceID"].ToString());
                }

                return null;
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
    }
}
