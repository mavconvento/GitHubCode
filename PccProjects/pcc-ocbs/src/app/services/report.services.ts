import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from "../../environments/environment";
import { HelperService } from './helper.services';
import { ReportsServiceBase } from './base-services/report.services.base';

const apiUrl = environment.apiurl + '/api/report';

@Injectable({
  providedIn: 'root'
})
export class ReportsService extends ReportsServiceBase {

  headerDict = {
    'tokenBearer': localStorage.getItem("tokenBearer")
  }

  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };

  GetBettingReportSummary(eventid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetBettingReportSummary/" + eventid + "/" + localStorage.getItem("userId"), this.requestOptions);
  }

  GetBettingReportByFightNo(eventId: number, fightno: number): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetBettingReportByFightNo/" + eventId + "/" + localStorage.getItem("userId") + "/" + fightno, this.requestOptions);
  }

  constructor(private httpClient: HttpClient, private helper: HelperService) {
    super();
  }
}