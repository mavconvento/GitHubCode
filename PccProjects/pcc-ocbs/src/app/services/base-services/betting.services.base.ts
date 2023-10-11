import { Observable } from "rxjs";

export abstract class BettingServiceBase {
  abstract BettingSave(params: any): Observable<any>
  abstract GetClaims(eventid: string, refid: string): Observable<any>
  abstract GetCancelBet(eventid: string, refid: string): Observable<any>
  abstract CancelBetting(params: any): Observable<any>
  abstract PayoutClaims(params: any): Observable<any>
  abstract GetCurrentPoints(eventid: string): Observable<any>
  abstract TellerPointSave(params: any): Observable<any>
  abstract GetPlotWinners(eventid: string): Observable<any>
  abstract GetBettingByFightNo(eventid: string, fightNo: string): Observable<any>
  abstract GetHighBettingByFightNo(eventid: string, fightNo: string): Observable<any>
  abstract GetUnClaimsTicket(eventid: string, userid: string): Observable<any>
  abstract GetClaimsTicket(eventid: string, userid: string): Observable<any>
  abstract GetBettingHistoryByEvent(eventid: string, userid: string): Observable<any>
  abstract GetLastClaims(eventid: string, userid: string): Observable<any>
  abstract GetPointsHistory(eventid: string, userid: string): Observable<any>
}
