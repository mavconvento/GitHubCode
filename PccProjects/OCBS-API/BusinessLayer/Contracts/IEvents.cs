using DomainObject;
using DomainObject.DatabaseObject;
using DomainObject.PlatformObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IEvents
    {
        Task<DomainObject.DatabaseObject.Event> GetCurrentEvent(string token, Int64 userid, string platformuserid, bool isOffline, string eventname, int eventid);
        Task<Fight> GetFight(string eventId, string token, Int64 userid, string platformuserid, bool isOffline);
        Task<Odd> GetFightOdds(string eventId, string token, Int64 userid, string platformuserid, bool isOffline);
        Task<PlatformWinner> GetDeclareWinner(string eventId, string fightNumber, string token, Int64 userid, string platformuserid, bool isOffline);
        Task<DomainObject.PlatformObject.PlatformFight> GetFightOffline(Int64 eventid, Int64 userid, string fightno = "");
        Task<DomainObject.PlatformObject.PlatformFightWithDeclare> FightOfflineSave(FightOffline fightOffline);
        Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventId, Int64 fightno);
        Task<List<DomainObject.DatabaseObject.Event>> GetEventById(Int64 companyId, Int64 eventid);
    }
}
