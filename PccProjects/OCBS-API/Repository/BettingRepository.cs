using DomainObject;
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
    public class BettingRepository : IBettingRepository
    {
        private readonly IDatabaseConnection _dbconn;

        public BettingRepository(IDatabaseConnection databaseConnection)
        {
            _dbconn = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<string> ClaimPayout(Payout payout)
        {
            try
            {
                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClaimPayout", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Eventid", payout.eventId);
                        cmd.Parameters.AddWithValue("@RefId", payout.referenceId);
                        cmd.Parameters.AddWithValue("@BettingId", payout.bettingId);
                        cmd.Parameters.AddWithValue("@Payout", payout.payoutAmount.Replace(",", ""));
                        cmd.Parameters.AddWithValue("@Odds", payout.odds);
                        cmd.Parameters.AddWithValue("@WinningSide", payout.winningSide);
                        cmd.Parameters.AddWithValue("@UserId", payout.userId);
                        

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        return "Success";
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Betting> CancelBetting(Payout payout)
        {
            try
            {
                string connString = await _dbconn.DBConnection();
                Betting results = new Betting();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("CancelBetting", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Eventid", payout.eventId);
                        cmd.Parameters.AddWithValue("@RefId", payout.referenceId);
                        cmd.Parameters.AddWithValue("@UserId", payout.userId);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Status = reader["Status"].ToString();
                                if (results.Status == "success")
                                {
                                    results.FightNo = reader["FightNo"].ToString();
                                    results.EventId = reader["EventId"].ToString();
                                    results.MeronTotalBet = String.Format("{0:#,##0.00}", reader["MeronBetTotal"].ToString());
                                    results.WalaTotalBet = String.Format("{0:#,##0.00}", reader["WalaBetTotal"].ToString());
                                    results.DrawTotalBet = String.Format("{0:#,##0.00}", reader["DrawBetTotal"].ToString());
                                }
                                
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
        public async Task<Betting> BettingSave(Bet bet)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                Betting results = new Betting();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("BettingSave", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Eventid", bet.eventId);
                        cmd.Parameters.AddWithValue("@FightNo", bet.fightNo);
                        cmd.Parameters.AddWithValue("@Amount", bet.betAmount);
                        cmd.Parameters.AddWithValue("@BetType", bet.betType);
                        cmd.Parameters.AddWithValue("@UserID", bet.userId);
                        cmd.Parameters.AddWithValue("@PlatformRefId", bet.platformRefId);
                        cmd.Parameters.AddWithValue("@Status", bet.betstatus);


                        await sql.OpenAsync();


                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Status = reader["Status"].ToString();
                                results.FightNo = reader["FightNo"].ToString();
                                results.Amount = reader["Amount"].ToString();
                                results.BetType = reader["BetType"].ToString();
                                results.ReferenceId = reader["ReferenceId"].ToString();
                                results.EventId = reader["EventId"].ToString();
                                results.PlatformRefId = reader["PlatformRefId"].ToString();
                                results.MeronTotalBet = String.Format("{0:#,##0.00}", reader["MeronBetTotal"].ToString());
                                results.WalaTotalBet = String.Format("{0:#,##0.00}", reader["WalaBetTotal"].ToString());
                                results.DrawTotalBet = String.Format("{0:#,##0.00}", reader["DrawBetTotal"].ToString());
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

        public async Task<Betting> GetBettingByRefId(string refId, string eventid, Int64 userid)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                Betting results = new Betting();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetBettingByRefId", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReferenceId", refId);
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@UserId", userid);


                        await sql.OpenAsync();


                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Status = reader["Status"].ToString();

                                if (results.Status == "success")
                                {
                                    results.BettingId =(Int64)reader["BettingId"];
                                    results.FightNo = reader["FightNo"].ToString();
                                    results.Amount = reader["Amount"].ToString();
                                    results.BetType = reader["BetType"].ToString();
                                    results.EventId = reader["EventId"].ToString();
                                }
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

        public async Task<List<Betting>> GetBettingByFightNo(string fightno, string eventid, Int64 userid)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                List<Betting> results = new List<Betting>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetBettingByFightNo", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fightno", fightno);
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@UserId", userid);


                        await sql.OpenAsync();


                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new Betting()
                                {
                                    Amount = reader["Amount"].ToString(),
                                    BetType = reader["BetType"].ToString(),
                                    ReferenceId = reader["Barcode"].ToString()
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

        public async Task<PlatformCurrentPoints> GetPoints(Int64 userid, Int64 eventid)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                PlatformCurrentPoints results = new PlatformCurrentPoints();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPoints", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.Parameters.AddWithValue("@eventid", eventid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.playing_points = (decimal)reader["Points"];
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

        public async Task<PlatformCurrentPoints> TellerPointSave(Points points)
        {
            try
            {
                PlatformCurrentPoints results = new PlatformCurrentPoints();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("TellerPointsSave", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", points.UserId);
                        cmd.Parameters.AddWithValue("@EventId", points.Eventid);
                        cmd.Parameters.AddWithValue("@TellerId", points.TellerId);
                        cmd.Parameters.AddWithValue("@Amount", points.Amount);
                        cmd.Parameters.AddWithValue("@Type", points.Type);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.playing_points = (decimal)reader["Points"];
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

        public async Task<List<PlotWinner>> GetPlotWinner(string eventid)
        {
            try
            {
                List<PlotWinner> results = new List<PlotWinner>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPlotWinner", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventId", eventid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new PlotWinner()
                                {
                                    Row1 = reader["Row1"].ToString(),
                                    Row2 = reader["Row2"].ToString(),
                                    Row3 = reader["Row3"].ToString(),
                                    Row4 = reader["Row4"].ToString(),
                                    Row5 = reader["Row5"].ToString(),
                                    Row6 = reader["Row6"].ToString()
                                }); ;
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

        public async Task<List<UnClaimed>> GetUnClaimedTicket(string eventid, long userid)
        {
            try
            {
                List<UnClaimed> results = new List<UnClaimed>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUnClaimed", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@Userid", userid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new UnClaimed()
                                {
                                    ReferenceId = reader["ReferenceId"].ToString(),
                                    FightNo = reader["FightNo"].ToString(),
                                    BetAmount = reader["Amount"].ToString()
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

        public async Task<List<PointsHistory>> GetPointHistory(string eventid, long userid)
        {
            try
            {
                List<PointsHistory> results = new List<PointsHistory>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPointsHistory", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@Userid", userid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new PointsHistory()
                                {
                                    PointsAmount = (Decimal)reader["Points"],
                                    Type = reader["Type"].ToString(),
                                    ApprovedBy = reader["ApprovedBy"].ToString(),
                                    DateRequested = (DateTime)reader["DateRequested"]
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

        public async Task<List<UnClaimed>> GetClaimedTicket(string eventid, long userid)
        {
            try
            {
                List<UnClaimed> results = new List<UnClaimed>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetClaimed", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@Userid", userid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new UnClaimed()
                                {
                                    ReferenceId = reader["ReferenceId"].ToString(),
                                    FightNo = reader["FightNo"].ToString(),
                                    BetAmount = reader["Amount"].ToString(),
                                    Payout = reader["Payout"].ToString(),
                                    Odds = reader["Odds"].ToString()
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
        public async Task<List<UnClaimed>> GetBettingHistoryByEvent(string eventid, long userid)
        {
            try
            {
                List<UnClaimed> results = new List<UnClaimed>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetBettingHistoryByEvent", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventId", eventid);
                        cmd.Parameters.AddWithValue("@Userid", userid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new UnClaimed()
                                {
                                    ReferenceId = reader["ReferenceId"].ToString(),
                                    FightNo = reader["FightNo"].ToString(),
                                    BetAmount = reader["Amount"].ToString(),
                                    Payout = reader["Payout"].ToString(),
                                    Odds = reader["Odds"].ToString(),
                                    Teller = reader["Teller"].ToString()
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
