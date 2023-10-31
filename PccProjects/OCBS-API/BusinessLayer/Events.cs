using BusinessLayer.ApiHelper;
using BusinessLayer.Contracts;
using DomainObject;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DomainObject.PlatformObject;
using Newtonsoft.Json;
using System.Data;

namespace BusinessLayer
{
    public class Events : IEvents
    {
        private readonly IConfiguration _configuration;
        private readonly IPlatform _platform;
        private readonly IEventRepository _eventRepository;
        public Events(IConfiguration config, IPlatform platform, IEventRepository eventRepository)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<DomainObject.DatabaseObject.Event> GetCurrentEvent(string token, Int64 userid, string platformuserid, bool isOffline, string eventname, int eventid)
        {
            try
            {

                Event result = new Event();
                if (!isOffline)
                {
                    var _result = await _platform.GetAsync<PlatformEvent>("event", token, platformuserid);
                    result.eventId = _result.eventId;
                    result.event_name = _result.event_name;
                }
                else
                {
                    result.event_name = eventname;
                    result.eventId = eventid;
                }
                   

                result.userId = userid;
                result.isoffline = isOffline;
                var eventresult = await _eventRepository.EventSave(result);

                return eventresult;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //public async Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventId, Int64 fightno)
        //{
        //    try
        //    {
        //        return await _eventRepository.BettingReportByFightNo(eventId, fightno);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }    
        //}

        public async Task<Fight> GetFight(string eventId, string token, Int64 userid, string platformuserid, bool isOffline)
        {
            try
            {
                PlatformFight result = new PlatformFight();
                if (!isOffline)
                {
                    var relativeurl = "fight?event=" + eventId;
                    result = await _platform.GetAsync<PlatformFight>(relativeurl, token, platformuserid);
                }
                else
                    result = await _eventRepository.GetFight(Int64.Parse(eventId), userid);

                var fight = new Fight() { eventId = eventId, fightId = result.details.fightId.ToString(), isLastCall = result.details.isLastCall, fightNo = result.details.fightNumber.ToString(), status = result.details.status, declare = result.details.declare, requestStatus = "success" };

                return fight;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Odd> GetFightOdds(string eventId, string token, Int64 userid, string platformuserid, bool isOffline)
        {
            try
            {
                PlatformFight result = new PlatformFight();
                if (!isOffline)
                {
                    var relativeurl = "fight?event=" + eventId;
                    result = await _platform.GetAsync<PlatformFight>(relativeurl, token, platformuserid);
                }
                else
                    result = await _eventRepository.GetFight(Int64.Parse(eventId), userid);

                var meron = result.bet.Where(x => x.bet_type == "MERON").FirstOrDefault().odds.ToString();
                var wala = result.bet.Where(x => x.bet_type == "WALA").FirstOrDefault().odds.ToString();
                var merontotal = result.bet.Where(x => x.bet_type == "MERON").FirstOrDefault().totalBet.ToString();
                var walatotal = result.bet.Where(x => x.bet_type == "WALA").FirstOrDefault().totalBet.ToString();
                var drawtotal = result.bet.Where(x => x.bet_type == "DRAW").FirstOrDefault().totalBet.ToString();

                var odd = new Odd()
                {
                    fightId = result.details.fightId.ToString(),
                    fightNo = result.details.fightNumber.ToString(),
                    eventId = eventId,
                    status = result.details.status,
                    isLastCall = result.details.isLastCall,
                    MeronOdds = meron,
                    WalaOdds = wala,
                    WalaTotal = walatotal,
                    DrawTotal = drawtotal,
                    MeronTotal = merontotal,
                    WalaBet = result.details.userwalatotalbet.ToString(),
                    MeronBet = result.details.usermerontotalbet.ToString(),
                    DrawBet = result.details.userdrawtotalbet.ToString(),
                    declare = result.details.declare,
                    requestStatus = "success",
                    userRole = result.details.userRole
                };

                return odd;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PlatformWinner> GetDeclareWinner(string eventId, string fightNumber, string token, Int64 userid, string platformuserid, bool isOffline)
        {
            try
            {
                PlatformFightWithDeclare result = new PlatformFightWithDeclare();
                if (!isOffline)
                {
                    var relativeurl = "fight?event=" + eventId + "&fight_number=" + fightNumber;
                    result = await _platform.GetAsync<PlatformFightWithDeclare>(relativeurl, token, platformuserid);
                }
                else
                    result = await _eventRepository.GetFightWithDeclare(Int64.Parse(eventId), userid, fightNumber);

                var meron = result.bet.Where(x => x.bet_type == "MERON").FirstOrDefault().odds.ToString();
                var wala = result.bet.Where(x => x.bet_type == "WALA").FirstOrDefault().odds.ToString();
                var draw = result.bet.Where(x => x.bet_type == "DRAW").FirstOrDefault().odds.ToString();
                var winner = new PlatformWinner() { winner = result.declare, meronodd = meron, walaodd = wala, drawodd = draw };
                return winner;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<PlatformFight> GetFightOffline(Int64 eventid, long userid, string fightno = "")
        {
            try
            {
                return _eventRepository.GetFight(eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PlatformFightWithDeclare> FightOfflineSave(FightOffline fightOffline)
        {
            try
            {
                return await _eventRepository.FightOfflineSave(fightOffline);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DomainObject.DatabaseObject.Event>> GetEventById(long companyId, long eventid)
        {
            try
            {
                return await _eventRepository.GetEventById(companyId,eventid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DomainObject.DatabaseObject.FightHistory>> GetFightHistory(long eventid, long userid)
        {
            try
            {
                return await _eventRepository.GetFightHistory(eventid, userid,false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<System.Data.DataTable> GetFightHistoryForPlotting(long eventid, long userid)
        {
            try
            {
                DataTable result = new DataTable("PlottingResult");
                int diff = 10;

                for (int i = 0; i < 90; i++)
                {
                    result.Columns.Add("Col" + i, typeof(String));
                }

                for (int x = 0; x < 6; x++)
                {
                    DataRow dataRow;
                    dataRow = result.NewRow();
                    for (int i = 0; i < 90; i++)
                    {
                        dataRow[i] = "";
                    }
                    result.Rows.Add(dataRow);
                }
                
                //result.Columns.Add("Col" + 1);
                //DataRow
                var fightHistories = await _eventRepository.GetFightHistory(eventid, userid, true);
                string prevValue = "";
                int lastrow = -1;
                int lastcol = -1;
                int lastcoldiffvalue = 0;
                int lastbeforeL = 0;
                int tempmaxrow = 5;
                int maxrowwithvalue = -1;
                bool firstCol = true;
                foreach (var item in fightHistories)
                {
                    if(item.Declare != "")
                    {
                        if (prevValue != item.Declare && item.Declare != "DRAW" && item.Declare != "CANCEL" && prevValue != "")
                        {

                            lastrow = 0;
                            tempmaxrow = 5;

                            if (lastbeforeL > 0)
                            {
                                if (firstCol)
                                   lastcoldiffvalue = 0;
                                else
                                   lastcoldiffvalue = lastbeforeL;

                                lastbeforeL = 0;
                            }
                            else if (firstCol) 
                                lastcoldiffvalue = 0;

                            
                            lastcol = lastcoldiffvalue;
                            lastcol++;
                            lastcoldiffvalue = lastcol;
                            result.Rows[lastrow][lastcoldiffvalue] = item.Declare;
                            firstCol = false;

                            if (lastcoldiffvalue > maxrowwithvalue) maxrowwithvalue = lastcoldiffvalue;
                            lastcoldiffvalue = lastcol;
                            //lastrow++;
                        }
                        else
                        {

                            if (lastrow < tempmaxrow)
                            {
                                lastrow++;
                                if (result.Rows[lastrow][lastcoldiffvalue].ToString() != "")
                                {
                                    lastrow--;
                                    tempmaxrow = lastrow;
                                    if (lastbeforeL == 0) lastbeforeL = lastcoldiffvalue;
                                    lastcoldiffvalue++;

                                    if (lastrow <= 0)
                                    {
                                        lastbeforeL = lastcoldiffvalue;
                                    }
                                }
                            } 
                            else
                            {
                                if (lastbeforeL == 0) lastbeforeL = lastcoldiffvalue;
                                lastcoldiffvalue++;

                                if (lastrow <= 0)
                                {
                                    lastbeforeL = lastcoldiffvalue;
                                }
                            }
                                
                            result.Rows[lastrow][lastcoldiffvalue] = item.Declare;
                            if (lastcoldiffvalue > maxrowwithvalue) maxrowwithvalue = lastcoldiffvalue;
                        }

                        if (item.Declare == "WALA" || item.Declare == "MERON")
                        {
                            prevValue = item.Declare;
                        }
                    }
                }

                if (maxrowwithvalue >= 40- diff) return Plotting(result, maxrowwithvalue, diff);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable Plotting(DataTable dtrecord, int maxcol, int diff)
        {
            try
            {
                
                DataTable result = new DataTable("Plotting");

                for (int i = 0; i < (40- diff); i++)
                {
                    result.Columns.Add("Col" + i, typeof(String));
                }

                for (int x = 0; x < 6; x++)
                {
                    DataRow dataRow;
                    dataRow = result.NewRow();
                    for (int i = 0; i < (40- diff); i++)
                    {
                        dataRow[i] = "";
                    }
                    result.Rows.Add(dataRow);
                }

                
                for (int x = 0; x < 6; x++)
                {
                    int counter = -1;
                    for (int i = maxcol - (39- diff); i <= maxcol; i++)
                    {
                        counter++;
                        if (counter < (40- diff))
                        {
                            result.Rows[x][counter] = dtrecord.Rows[x]["Col" + i];
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

}
