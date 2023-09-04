import { Observable } from "rxjs";

export abstract class UserServiceBase {
  abstract Login(params: any): Observable<any>
  abstract GetTellerList(companyId: string, eventid: string, userid: number): Observable<any>
  abstract UserSave(params: any): Observable<any>
  abstract GetUserById(companyId: string, id: string, userid: string): Observable<any>
  abstract GetRole(): Observable<any>
  abstract GetCompany(): Observable<any>
}
