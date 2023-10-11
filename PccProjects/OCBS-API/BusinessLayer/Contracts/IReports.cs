using DomainObject;
using DomainObject.DatabaseObject;
using DomainObject.PlatformObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IReports
    {
        Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventId, Int64 fightno);
        Task<List<DomainObject.DatabaseObject.BettingReport>> BettingReportSummary(Int64 eventid, Int64 userid);
    }
}
