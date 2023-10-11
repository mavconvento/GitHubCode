using System;
using Repository.Database;
using System.Threading.Tasks;
using System.Linq;
using Repository.Contracts;
using DomainObject;
using DomainObject.AppServices;
using Repository.Helper;
using System.Data.SqlClient;
using System.Data;
//using Newtonsoft.Json;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;

        #region Variables
        DatabaseConnection dbconn;
        #endregion Variables

        public AccountRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Authenticate");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", userName);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", password);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    User user = new User()
                    {
                        DisplayID = Convert.ToInt64(dataResult.Tables[0].Rows[0]["DisplayID"]),
                        //FileUploadID = dataResult.Tables[0].Rows[0]["FileUploadID"] != null ? (Guid?)dataResult.Tables[0].Rows[0]["FileUploadID"] : null,
                        FirstName = dataResult.Tables[0].Rows[0]["FirstName"].ToString(),
                        LastName = dataResult.Tables[0].Rows[0]["LastName"].ToString(),
                        LoftName = dataResult.Tables[0].Rows[0]["LoftName"].ToString(),
                        EclockID = dataResult.Tables[0].Rows[0]["EclockID"].ToString(),
                        UserID = (Guid?)dataResult.Tables[0].Rows[0]["UserID"],
                        UserName = dataResult.Tables[0].Rows[0]["UserName"].ToString()
                    };

                    if (!string.IsNullOrEmpty(dataResult.Tables[0].Rows[0]["FileUploadID"].ToString()))
                    {
                        user.FileUploadID = (Guid?)dataResult.Tables[0].Rows[0]["FileUploadID"];
                    }

                    return await Task.FromResult(user);
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

        public async Task<DataTable> Update(Profile profile)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("UpdateProfile");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@UserId", profile.UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@LoftName", profile.LoftName);
                dbconn.sqlComm.Parameters.AddWithValue("@FirstName", profile.FirstName);
                dbconn.sqlComm.Parameters.AddWithValue("@LastName", profile.LastName);
                dbconn.sqlComm.Parameters.AddWithValue("@EclockId", profile.EClockID);
                dbconn.sqlComm.Parameters.AddWithValue("@FileUploadId", profile.FileUploadId);

                if (profile.Image != null)
                {

                    dbconn.sqlComm.Parameters.AddWithValue("@FileName", profile.Image.FileName);
                    dbconn.sqlComm.Parameters.AddWithValue("@FileType", profile.Image.ContentType);
                    dbconn.sqlComm.Parameters.AddWithValue("@Image", Helper.ImageUpload.GetImageAsBytes(profile.Image));
                }
                
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task.FromResult(dataResult.Tables[0]);
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

        public async Task<DataSet> GetLinkMobileNumber(string userid)
        {
            try
            {
                try
                {

                    DataSet dataResult = new DataSet();
                    dbconn = new DatabaseConnection();
                    dbconn.DatabaseConn("GetLinkMobileList");

                    if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                    dbconn.sqlConn.Open();
                    dbconn.sqlComm.Parameters.Clear();
                    dbconn.sqlComm.CommandTimeout = 0;
                    dbconn.sqlComm.Parameters.AddWithValue("@UserID", userid);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = dbconn.sqlComm;
                    da.Fill(dataResult);
                    dbconn.sqlConn.Close();

                    if (dataResult.Tables.Count > 0)
                    {
                        return await Task.FromResult(dataResult);
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> Insert(User user)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Insert_MavcPigeonUser");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@FirstName", user.FirstName);
                dbconn.sqlComm.Parameters.AddWithValue("@LastName", user.LastName);
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", user.UserName);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", user.Password);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task.FromResult(dataResult.Tables[0].Rows[0]["UserID"].ToString());
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



        public async Task<DataSet> Pasaload(string mobileNumberFrom, String mobileNumberTo, string Amount)
        {
            CommonRepository common = new CommonRepository();
            return await common.WebClockingSave("", mobileNumberFrom, Amount, "Pasaload", "", mobileNumberTo);
        }

        public async Task<DataSet> LoadMavcCard(string ClubName, String mobileNumber, string keyword, string dbName)
        {
            try
            {

                CommonRepository common = new CommonRepository();
                return await common.WebClockingSave(ClubName, mobileNumber, keyword, "Load", dbName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataSet> Unreg(string ClubName, string mobileNumber, string keyword, string DBName, string UserID)
        {
            CommonRepository common = new CommonRepository();
            return  await common.WebClockingSave(ClubName, mobileNumber, keyword, "Unreg", UserID);
        }

        public async Task<string> SetAsPrimary(String mobileNumber, string UserID)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("SetAsPrimaryNumber");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobileNumber", mobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult("Success");
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

        public async Task<DataSet> GetMobileListByEmail(string email)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetMobileListByEmail");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@email", email);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult(dataResult);
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
        public async Task<DataSet> ValidateMobileNumber(string email, string mobileNumber)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ValidateMobileNumber");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobileNumber", "+" + mobileNumber.Trim());
                dbconn.sqlComm.Parameters.AddWithValue("@email", email);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult(dataResult);

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

        public async Task<DataSet> GetMemberCoordinates(string memberidno, string clubname,string dbname)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetMemberLocationByID", clubname, dbname);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIdNo", memberidno);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", clubname);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult(dataResult);

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

        public async Task<DataSet> GetVideo(string type)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetVideo");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@type", type);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                return await Task.FromResult(dataResult);

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
