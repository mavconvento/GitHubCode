import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from "../../environments/environment";
import { BettingServiceBase } from './base-services/betting.services.base';
import { HelperService } from './helper.services';

const apiUrl = environment.apiurl + '/api/betting';

@Injectable({
  providedIn: 'root'
})
export class BettingService extends BettingServiceBase {

  GetBettingHistoryByEvent(eventid: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetBettingHistoryByEvent/" + eventid + "/" + userid, this.requestOptions);
  }

  GetLastClaims(eventid: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetLastClaims/" + eventid + "/" + userid, this.requestOptions);
  }

  GetPointsHistory(eventid: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetPointsHistory/" + eventid + "/" + userid, this.requestOptions);
  }

  GetUnClaimsTicket(eventid: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetUnclaimedTicket/" + eventid + "/" + userid, this.requestOptions);
  }

  GetClaimsTicket(eventid: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetclaimedTicket/" + eventid + "/" + userid, this.requestOptions);
  }

  GetBettingByFightNo(eventid: string, fightNo: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetBettingByFightNo/" + eventid + "/" + localStorage.getItem("userId") + "/" + fightNo, this.requestOptions);
  }

  GetHighBettingByFightNo(eventid: string, fightNo: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetHighBettingByFightNo/" + eventid + "/" + localStorage.getItem("userId") + "/" + fightNo, this.requestOptions);
  }

  GetPlotWinners(eventid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetPlotWinner/" + eventid, this.requestOptions);
  }

  headerDict = {
    'tokenBearer': localStorage.getItem("tokenBearer")
  }

  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };

  TellerPointSave(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/TellerPointSave", params, this.requestOptions)
  }

  GetCurrentPoints(eventid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetCurrentPoints/" + localStorage.getItem("platformUserId") + "/" + localStorage.getItem("userId") + "/" + localStorage.getItem("IsOffline") + "/" + eventid, this.requestOptions);
  }

  constructor(private httpClient: HttpClient, private helper: HelperService) {
    super();
  }

  GetClaims(eventid: string, refid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetClaims/" + eventid + "/" + localStorage.getItem("userId") + "/" + localStorage.getItem("platformUserId") + "/" + refid + "/" + localStorage.getItem("IsOffline"), this.requestOptions);
  }

  PayoutClaims(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/ClaimPayout", params, this.requestOptions);
  }

  GetCancelBet(eventid: string, refid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetCancelBet/" + eventid + "/" + localStorage.getItem("userId") + "/" + localStorage.getItem("platformUserId") + "/" + refid + "/" + localStorage.getItem("IsOffline"), this.requestOptions);
  }

  CancelBetting(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/CancelBetting", params, this.requestOptions);
  }

  BettingSave(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/BettingSave", params, this.requestOptions);
  }
}