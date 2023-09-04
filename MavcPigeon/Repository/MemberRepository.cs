using Repository.Contracts;
using Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;

        #region Variables
        DatabaseConnection dbconn;
        #endregion Variables

        public MemberRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public async Task<DataSet> GetMemberDistance(string clubname, string memberidno, string dbname)
        {
            try
            {
                try
                {

                    DataSet dataResult = new DataSet();
                    dbconn = new DatabaseConnection();
                    dbconn.DatabaseConn("MemberDistance", clubname, dbname);

                    if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                    dbconn.sqlConn.Open();
                    dbconn.sqlComm.Parameters.Clear();
                    dbconn.sqlComm.CommandTimeout = 0;
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID", clubname);
                    dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", memberidno);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsFromPilipinasKalapati", true);

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

        public async Task<DataSet> GetLogs(String ClubID, String MobileNumber, String Keyword, String DateFrom, String DateTo, String DBName)
        {
            try
            {
                try
                {

                    DataSet dataResult = new DataSet();
                    dbconn = new DatabaseConnection();
                    dbconn.DatabaseConn("InboxView", "", DBName);

                    if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                    dbconn.sqlConn.Open();
                    dbconn.sqlComm.Parameters.Clear();
                    dbconn.sqlComm.Parameters.AddWithValue("@sender", MobileNumber);
                    dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredFrom", Convert.ToDateTime(DateFrom).Date);
                    dbconn.sqlComm.Parameters.AddWithValue("@dateCoveredTO", Convert.ToDateTime(DateTo).Date);
                    dbconn.sqlComm.Parameters.AddWithValue("@keyword", Keyword);
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsPilipinasKalapati", true);

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
    }
}
