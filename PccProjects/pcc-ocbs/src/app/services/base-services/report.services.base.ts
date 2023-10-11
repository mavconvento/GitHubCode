import { Observable } from "rxjs";

export abstract class ReportsServiceBase {
  abstract GetBettingReportByFightNo(eventId: number, fightno: number): Observable<any>
  abstract GetBettingReportSummary(eventid: string): Observable<any>
}
