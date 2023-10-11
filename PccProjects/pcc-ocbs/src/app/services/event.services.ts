import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from "../../environments/environment";
import { EventsServiceBase } from './base-services/event.services.base';
import { HelperService } from './helper.services';

const apiUrl = environment.apiurl + '/api/event';

@Injectable({
  providedIn: 'root'
})
export class EventsService extends EventsServiceBase {
  headerDict = {
    'tokenBearer': localStorage.getItem("tokenBearer")
  }

  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };

  GetFightHistory(eventId: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetFightHistory/" + eventId + "/" + localStorage.getItem("userId"), this.requestOptions);
  }

  GetFightHistoryPlotting(eventId: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetFightHistoryForPlotting/" + eventId + "/" + localStorage.getItem("userId"), this.requestOptions);
  }

  GetEventById(eventId: number): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetEventById/" + localStorage.getItem("companyId") + "/" + eventId, this.requestOptions);
  }

  EventOfflineSave(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/EventOfflineSave", params, this.requestOptions);
  }

  GetCurrentFightOdds(eventId: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetCurrentFightOdds/" + eventId + "/" + localStorage.getItem("userId") + "/" + localStorage.getItem("platformUserId") + "/" + localStorage.getItem("IsOffline"), this.requestOptions);
  }

  GetFights(eventId: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetCurrentFight/" + eventId + "/" + localStorage.getItem("userId") + "/" + localStorage.getItem("platformUserId") + "/" + localStorage.getItem("IsOffline"), this.requestOptions);
  }

  GetEvents(): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetEvent/" + localStorage.getItem("userId") + "/" + localStorage.getItem("platformUserId") + "/" + localStorage.getItem("IsOffline"), this.requestOptions);
  }

  // GetBettingReportByFightNo(eventId: number, fightno: number): Observable<any> {
  //   return this.httpClient.get(apiUrl + "/GetBettingReportByFightNo/" + eventId + "/" + localStorage.getItem("userId") + "/" + fightno, this.requestOptions);
  // }

  FightOfflineSave(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/OfflineFightSave", params, this.requestOptions);
  }

  constructor(private httpClient: HttpClient, private helper: HelperService) {
    super();
  }
}