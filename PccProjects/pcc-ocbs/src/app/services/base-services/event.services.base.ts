import { Observable } from "rxjs";

export abstract class EventsServiceBase {
  abstract GetEvents(params: any): Observable<any>
  abstract GetFights(eventId: string): Observable<any>
  abstract GetCurrentFightOdds(eventId: string): Observable<any>
  abstract GetCurrentFightOdds(eventId: string): Observable<any>
  abstract EventOfflineSave(params: any): Observable<any>
  abstract FightOfflineSave(params: any): Observable<any>
  // abstract GetBettingReportByFightNo(eventId: number, fightno: number): Observable<any>
  abstract GetEventById(eventId: number): Observable<any>
  abstract GetFightHistory(eventId: string): Observable<any>
  abstract GetFightHistoryPlotting(eventId: string): Observable<any>
}
