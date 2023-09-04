using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IEventRepository
    {
        Task<DomainObject.DatabaseObject.Event> EventSave(Event _event);
        Task<DomainObject.PlatformObject.PlatformFight> GetFight(Int64 eventid, Int64 userid);
        Task<DomainObject.PlatformObject.PlatformFightWithDeclare> GetFightWithDeclare(Int64 eventid, Int64 userid, string fightno);
        Task<DomainObject.PlatformObject.PlatformFightWithDeclare> FightOfflineSave(FightOffline fightOffline);
        Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventid, Int64 fightno);
        Task<List<DomainObject.DatabaseObject.Event>> GetEventById(Int64 companyId, Int64 eventid);

    }
}
