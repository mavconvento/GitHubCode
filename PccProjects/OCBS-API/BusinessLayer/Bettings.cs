using BusinessLayer.Contracts;
using DomainObject;
using DomainObject.DatabaseObject;
using DomainObject.PlatformObject;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Bettings : IBettings
    {
        private readonly IBettingRepository _betting;
        private readonly IPlatform _platform;
        private readonly IEvents _events;
        public Bettings(IBettingRepository betting, IPlatform platform, IEvents events)
        {
            _betting = betting ?? throw new ArgumentNullException(nameof(betting));
            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _events = events ?? throw new ArgumentNullException(nameof(events));
        }
        public async Task<Betting> BettingSave(Bet bet, string token)
        {
            try
            {
                var betting = new Betting();
                var playerBet = new PlatfromPlayerBet() { amount = Decimal.Parse(bet.betAmount), bet = bet.betType, Event = int.Parse(bet.eventId), fightId = int.Parse(bet.fightId), fightNumber = int.Parse(bet.fightNo) };

                PlatformBetResponse result = new PlatformBetResponse();
                if (!bool.Parse(bet.betOffline))
                {
                    var relativeurl = "player/bet";
                    result = await _platform.PostAsync<PlatformBetResponse, PlatfromPlayerBet>(relativeurl, token, bet.platformUserId, playerBet);
                }
                else
                    result = new PlatformBetResponse() { success = 1 };
                       
                if (result.success == 1)
                {
                    bet.platformRefId = result.inserId.ToString();
                    betting = await _betting.BettingSave(bet);
                    betting.Message = result.message;
                    betting.CurrentPoints = result.current_points;
                    betting.EventId = bet.eventId;
                    betting.Status = "success";
                }
                else
                {
                    betting.Message = result.message;
                    betting.CurrentPoints = result.current_points;
                    betting.EventId = bet.eventId;
                    betting.Status = "failed";
                }
                    
                return betting;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ClaimPayout(Payout payout)
        {
            try
            {
                return await _betting.ClaimPayout(payout);
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
                return await _betting.CancelBetting(payout);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Betting>> GetBettingByFightNo(string fightno, string eventid, long userid)
        {
            try
            {
                return await _betting.GetBettingByFightNo(fightno, eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Claim> GetBettingByRefId(string eventid, string refId, string token, Int64 userid, string platformUserId, bool isOffline)
        {
            try
            {
                var claim = new Claim();
                var betting = await _betting.GetBettingByRefId(refId, eventid, userid);

                if (betting.Status == "success")
                {
                    var winner = await _events.GetDeclareWinner(eventid, betting.FightNo, token, userid, platformUserId, isOffline);
                    claim.BettingId = betting.BettingId;
                    claim.BetAmount = String.Format("{0:#,##0.00}", betting.Amount);
                    claim.WinningSide = winner.winner;
                    claim.MeronOdds = winner.meronodd;
                    claim.WalaOdds = winner.walaodd;
                    claim.DrawOdds = winner.drawodd;
                    claim.Status = "success";

                    if (winner.winner == betting.BetType)
                    {
                        if (winner.winner == "MERON")
                        {
                            var win = Decimal.Parse(betting.Amount) * (Decimal.Parse(winner.meronodd) / 100);
                            claim.FightNo = betting.FightNo;
                            claim.Odds = String.Format("{0:#,##0.00}", winner.meronodd);
                            claim.Win = String.Format("{0:#,##0.00}", win);

                        }
                        else if (winner.winner == "WALA")
                        {
                            var win = Decimal.Parse(betting.Amount) * (Decimal.Parse(winner.walaodd) / 100);
                            claim.FightNo = betting.FightNo;
                            claim.Odds = String.Format("{0:#,##0.00}", winner.walaodd);
                            claim.Win = String.Format("{0:#,##0.00}", win);

                        }
                        else if (winner.winner == "DRAW")
                        {
                            var win = Decimal.Parse(betting.Amount) * (Decimal.Parse(winner.drawodd) / 100);
                            claim.FightNo = betting.FightNo;
                            claim.Odds = String.Format("{0:#,##0.00}", winner.drawodd);
                            claim.Win = String.Format("{0:#,##0.00}", win);
                        }
                    }
                    else
                    {
                        claim.Status = "This ticket is not a winner";
                    }
                }
                else
                {
                    claim.Status = betting.Status;
                }
                
                return claim;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 

        }
        public async Task<Betting> GetCancelBettingByRefId(string eventid, string refId, string token, Int64 userid, string platformUserId, bool isOffline)
        {
            try
            {
                var betting = await _betting.GetBettingByRefId(refId, eventid, userid);
                return betting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<PlatformCurrentPoints> GetCurrentPoints(string token, string platformUserId, Int64 userid, bool isOffline, Int64 eventid)
        {
            try
            {
                PlatformCurrentPoints result = new PlatformCurrentPoints();

                if (!isOffline)
                {
                    var relativeurl = "player/points";
                    result = await _platform.GetAsync<PlatformCurrentPoints>(relativeurl, token, platformUserId);
                }
                else
                    result = await _betting.GetPoints(userid,eventid);

                return result;
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
                return await _betting.GetPlotWinner(eventid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<List<PointsHistory>> GetPointHistory(string eventid, long userid)
        {
            try
            {
                return _betting.GetPointHistory(eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<List<UnClaimed>> GetUnClaimedTicket(string eventid, long userid)
        {
            try
            {
                return _betting.GetUnClaimedTicket(eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<List<UnClaimed>> GetClaimedTicket(string eventid, long userid)
        {
            try
            {
                return _betting.GetClaimedTicket(eventid, userid);
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
                return await _betting.TellerPointSave(points);
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
                return await _betting.GetBettingHistoryByEvent(eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
