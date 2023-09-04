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

        public async Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventId, Int64 fightno)
        {
            try
            {
                return await _eventRepository.BettingReportByFightNo(eventId, fightno);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }    
        }

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
    }
}
