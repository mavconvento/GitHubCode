using DomainObject;
using DomainObject.DatabaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IBettingRepository
    {
        Task<Betting> BettingSave(Bet bet);
        Task<Betting> GetBettingSaved(string referenceid, string fightno, string eventid, Int64 userid, string token);
        Task<Betting> GetBettingByRefId(string refId, string eventid, Int64 userid);
        Task<string> ClaimPayout(Payout payout);
        Task<Betting> CancelBetting(Payout payout);
        Task<DomainObject.PlatformObject.PlatformCurrentPoints> GetPoints(Int64 userid, Int64 eventid);
        Task<DomainObject.PlatformObject.PlatformCurrentPoints> TellerPointSave(Points points);
        Task<List<DomainObject.DatabaseObject.PlotWinner>> GetPlotWinner(string eventid);
        Task<List<Betting>> GetBettingByFightNo(string fightno, string eventid, Int64 userid);
        Task<List<Betting>> GetHighBettingByFightNo(string fightno, string eventid, Int64 userid);
        Task<List<UnClaimed>> GetUnClaimedTicket(string eventid, Int64 userid);
        Task<List<PointsHistory>> GetPointHistory(string eventid, Int64 userid);
        Task<List<UnClaimed>> GetClaimedTicket(string eventid, Int64 userid);
        Task<List<UnClaimed>> GetBettingHistoryByEvent(string eventid, Int64 userid);
        Task<List<UnClaimed>> GetLastClaims(string eventid, long userid);
    }
}
