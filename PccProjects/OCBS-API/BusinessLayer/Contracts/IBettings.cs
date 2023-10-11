using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObject;
using DomainObject.DatabaseObject;
using DomainObject.PlatformObject;

namespace BusinessLayer.Contracts
{
    public interface IBettings
    {
        Task<Betting> BettingSave(Bet bet, string token);
        Task<Betting> GetBettingSaved(Bet bet, string referenceid, string fightno, string eventid, Int64 userid, string token);
        Task<Claim> GetBettingByRefId(string eventid, string refId, string token, Int64 userid, string platformUserId, bool isOffline);
        Task<Betting> GetCancelBettingByRefId(string eventid, string refId, string token, Int64 userid, string platformUserId, bool isOffline);
        Task<string> ClaimPayout(Payout payout);
        Task<Betting> CancelBetting(Payout payout);
        Task<PlatformCurrentPoints> GetCurrentPoints(string token, string platformUserId, Int64 userid, bool isOffline, Int64 eventid);
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
