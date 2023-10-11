using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IReportRepository
    {
        Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventid, Int64 fightno);
        Task<List<DomainObject.DatabaseObject.BettingReport>> BettingReportSummary(long eventid, long userid);
    }
}
