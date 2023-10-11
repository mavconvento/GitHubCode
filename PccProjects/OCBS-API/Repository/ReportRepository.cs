using DomainObject.DatabaseObject;
using DomainObject.PlatformObject;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDatabaseConnection _dbconn;

        public ReportRepository(IDatabaseConnection databaseConnection)
        {
            _dbconn = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<BettingReport> BettingReportByFightNo(Int64 eventid, Int64 fightno)
        {
            try
            {

                BettingReport results = new BettingReport();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("bettingreport", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@fightno", fightno);

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results = new BettingReport()
                                {
                                    Commission = reader["Commission"].ToString(),
                                    FightNo = reader["FightNo"].ToString(),
                                    Wala = reader["Wala"].ToString(),
                                    Meron = reader["Meron"].ToString(),
                                    TotalAmount = reader["TotalBetting"].ToString(),
                                    Status = reader["status"].ToString(),
                                    Declare = reader["declare"].ToString(),
                                    EventId = reader["eventid"].ToString(),
                                    PayoutOdd = reader["Payout"].ToString(),
                                };
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BettingReport>> BettingReportSummary(long eventid, long userid)
        {
            try
            {

                List<BettingReport> results = new List<BettingReport>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("bettingsummaryreportbyType", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@userid", userid);

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new BettingReport()
                                {
                                    //EventId = reader["eventid"].ToString(),
                                    FightNo = reader["FightNo"].ToString(),
                                    Wala = reader["Wala"].ToString(),
                                    Meron = reader["Meron"].ToString(),
                                    TotalAmount = reader["TotalBetting"].ToString(),
                                    Commission = reader["Commission"].ToString(),
                                    //Status = reader["status"].ToString(),
                                    Declare = reader["declare"].ToString(), 
                                    //PayoutOdd = reader["Payout"].ToString(),
                                });
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
