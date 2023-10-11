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
    public class EventRepository : IEventRepository
    {
        private readonly IDatabaseConnection _dbconn;

        public EventRepository(IDatabaseConnection databaseConnection)
        {
            _dbconn = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }


        public async Task<DomainObject.DatabaseObject.Event> EventSave(DomainObject.Event _event)
        {
            try
            {

                Event results = new Event();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("EventSave", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PlatformEventId", _event.eventId);
                        cmd.Parameters.AddWithValue("@EventName", _event.event_name);
                        cmd.Parameters.AddWithValue("@UserId", _event.userId);
                        cmd.Parameters.AddWithValue("@IsOffline", _event.isoffline);

                        await sql.OpenAsync();


                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.EventId = reader["EventId"].ToString();
                                results.Description = reader["Description"].ToString();
                                results.PlatformEventId = reader["PlatformEventId"].ToString();
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

        public async Task<PlatformFightWithDeclare> FightOfflineSave(DomainObject.FightOffline fightOffline)
        {
            try
            {

                PlatformFightWithDeclare results = new PlatformFightWithDeclare();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("FightSave", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", fightOffline.eventid);
                        cmd.Parameters.AddWithValue("@fightno", fightOffline.fightno);
                        cmd.Parameters.AddWithValue("@userid", fightOffline.userid);
                        cmd.Parameters.AddWithValue("@status", fightOffline.status);
                        cmd.Parameters.AddWithValue("@declare", fightOffline.declare);
                        cmd.Parameters.AddWithValue("@isLastCall", fightOffline.lastCall);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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

        public async Task<PlatformFight> GetFight(Int64 eventid, Int64 userid)
        {
            try
            {

                PlatformFight results = new PlatformFight();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFight", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@userid", userid);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results = new PlatformFight()
                                {
                                    details = new PlatfromFightDetails()
                                    {
                                        fightId = (Int64)reader["fightId"],
                                        fightNumber = Int64.Parse(reader["fightNumber"].ToString()),
                                        status = reader["status"].ToString(),
                                        declare = reader["declare"].ToString(),
                                        isLastCall = (bool)reader["islastcall"],
                                        userRole = reader["userrole"].ToString(),
                                        userdrawtotalbet = (decimal)reader["userdrawtotalbet"],
                                        usermerontotalbet = (decimal)reader["usermerontotalbet"],
                                        userwalatotalbet = (decimal)reader["userwalatotalbet"],

                                    },
                                    bet = new List<PlatformBet>
                                    {
                                       new PlatformBet() { bet_type = "MERON", odds = (decimal?)reader["meronodd"], totalBet = (decimal?)reader["merontotal"]},
                                       new PlatformBet() { bet_type = "WALA", odds = (decimal?)reader["walaodd"], totalBet = (decimal?)reader["walatotal"]},
                                       new PlatformBet() { bet_type = "DRAW", odds = (decimal?)reader["drawodd"], totalBet = (decimal?)reader["drawtotal"]}
                                    }
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

        public async Task<PlatformFightWithDeclare> GetFightWithDeclare(Int64 eventid, Int64 userid, string fightno)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                PlatformFightWithDeclare results = new PlatformFightWithDeclare();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFight", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@fightNo", fightno);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results = new PlatformFightWithDeclare()
                                {
                                    details = new PlatfromFightDetails()
                                    {
                                        fightId = (Int64)reader["fightId"],
                                        fightNumber = Int64.Parse(reader["fightNumber"].ToString()),
                                        status = reader["status"].ToString(),

                                    },
                                    bet = new List<PlatformBet>
                                    {
                                       new PlatformBet() { bet_type = "MERON", odds = (decimal?)reader["meronodd"], totalBet = (decimal?)reader["merontotal"]},
                                       new PlatformBet() { bet_type = "WALA", odds = (decimal?)reader["walaodd"], totalBet = (decimal?)reader["walatotal"]},
                                       new PlatformBet() { bet_type = "DRAW", odds = (decimal?)reader["drawodd"], totalBet = (decimal?)reader["drawtotal"]}
                                    },
                                    declare = reader["declare"].ToString()
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

        public async Task<List<Event>> GetEventById(long companyId, long eventid)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                List<Event> events = new List<Event>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetEventByID", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@companyid", companyId);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Event comp = new Event()
                                {
                                    Description = reader["Description"].ToString(),
                                    EventId = reader["Id"].ToString()
                                };

                                events.Add(comp);
                            }
                        }
                    }
                }

                return events;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<List<FightHistory>> GetFightHistory(long eventid, long userid, bool isplot)
        {
            try
            {
                //var result = await _dbconn.ExecuteReadAsync("GetTestTable", null, null);
                List<FightHistory> fightHistories = new List<FightHistory>();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFightHistory", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@eventid", eventid);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@isplot", isplot);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                FightHistory comp = new FightHistory()
                                {
                                    FightNo = reader["FightNo"].ToString(),
                                    Declare = reader["Declare"].ToString(),
                                    Remarks = reader["Remarks"].ToString()
                                };

                                fightHistories.Add(comp);
                            }
                        }
                    }
                }

                return fightHistories;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

      
    }
}
