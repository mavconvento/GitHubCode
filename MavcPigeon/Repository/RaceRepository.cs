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
    public class RaceRepository : IRaceRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;

        #region Variables
        DatabaseConnection dbconn;
        #endregion Variables

        public RaceRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public async Task<DataSet> GetRaceDetails(RaceFilter raceFilter)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceDetailsGetbyKeys", raceFilter.ClubName, raceFilter.DbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", raceFilter.ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", raceFilter.DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", raceFilter.Category);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", raceFilter.Group);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", raceFilter.FilterName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", raceFilter.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", raceFilter.UserID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataSet>.FromResult(dataResult);
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

        public async Task<DataSet> TopigeonTrainingSave(TopigeonTraining value)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TopigeonTrainingListSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", value.EclockId);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", value.ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@Liberation", value.Liberation);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", value.DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseTime", value.ReleaseTime);
                dbconn.sqlComm.Parameters.AddWithValue("@LatDegree", value.LatDeg);
                dbconn.sqlComm.Parameters.AddWithValue("@LatMin", value.LatMin);
                dbconn.sqlComm.Parameters.AddWithValue("@LatSec", value.LatSec);
                dbconn.sqlComm.Parameters.AddWithValue("@LatSign", value.LatSign);
                dbconn.sqlComm.Parameters.AddWithValue("@LongDegree", value.LongDeg);
                dbconn.sqlComm.Parameters.AddWithValue("@LongMin", value.LongMin);
                dbconn.sqlComm.Parameters.AddWithValue("@LongSec", value.LongSec);
                dbconn.sqlComm.Parameters.AddWithValue("@LongSign", value.LongSign);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", value.UserID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataSet>.FromResult(dataResult);
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

        public async Task<DataSet> GetTopigeonTraining(TopigeonTraining value)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetTopigeonTraining");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", value.EclockId);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", value.DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", value.UserID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataSet>.FromResult(dataResult);
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

        public async Task<DataTable> GetTrainingResult(TopigeonTraining value)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetTrainingResult");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", value.EclockId);
                dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", value.DateRelease);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataTable> GetRaceResult(RaceFilter raceFilter)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultGetbyKey", raceFilter.ClubName, raceFilter.DbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", raceFilter.ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", raceFilter.DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", raceFilter.Category == null ? "All" : raceFilter.Category);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", raceFilter.Group == null ? "All" : raceFilter.Group);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", raceFilter.FilterName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteReaderAsync();
                
                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataTable> GetLocation(RaceFilter raceFilter)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("LocationSelectAll", raceFilter.ClubName, raceFilter.DbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", raceFilter.ClubID);
                //dbconn.sqlComm.Parameters.AddWithValue("@ClubName", raceFilter.ClubName);
                //dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", raceFilter.DateRelease);
                //dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", raceFilter.Category == null ? "All" : raceFilter.Category);
                //dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", raceFilter.Group == null ? "All" : raceFilter.Group);
                //dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                //dbconn.sqlComm.Parameters.AddWithValue("@Name", raceFilter.FilterName);
                //dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                //dbconn.sqlComm.ExecuteReaderAsync();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataTable> GetBirdCategory(string dbName = "", string clubName = "")
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceCategorySelectAll", "", dbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", clubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReturnAll", 0);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataTable> GetGroupCategory(string dbName = "", string clubName = "")
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceCategoryGroupSelectAll", clubName, dbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", clubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReturnAll", 0);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataSet> QRCodeClocking(string qrcode, string action)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("QRCodeClockingSave","","",action);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@qrcode", qrcode);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataSet>.FromResult(dataResult);
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
        public async Task<DataTable> GetRaceEntry(RaceFilter raceFilter)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceDetailsGetbyKeys", raceFilter.ClubName, raceFilter.DbName);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", raceFilter.ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", raceFilter.DateRelease);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", raceFilter.Category);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", raceFilter.Group);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", raceFilter.FilterName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", raceFilter.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", raceFilter.Source);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", raceFilter.UserID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[1]);
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
        public async Task<DataTable> GetBalance(string mobileNumber)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("MobileCheckLoadBalance");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobilenumber", mobileNumber);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();

                if (dataResult.Tables.Count > 0)
                {
                    return await Task<DataTable>.FromResult(dataResult.Tables[0]);
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

        public async Task<DataSet> OnlineClocking(OnlineClocking online)
        {
            try
            {
                CommonRepository common = new CommonRepository();
                return await common.WebClockingSave(online.ClubName, online.MobileNumber, online.Keyword, "Online", online.DBName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
