import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from "../../environments/environment";
import { BettingServiceBase } from './base-services/betting.services.base';
import { UserServiceBase } from './base-services/user.services.base';

const apiUrl = environment.apiurl + '/api/users';

@Injectable({
  providedIn: 'root'
})
export class UserService extends UserServiceBase {


  headerDict = {
    'tokenBearer': localStorage.getItem("tokenBearer")
  }

  requestOptions = {
    headers: new HttpHeaders(this.headerDict),
  };

  constructor(private httpClient: HttpClient) {
    super();
  }

  GetRole(): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetRole", this.requestOptions);
  }
  GetCompany(): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetCompany", this.requestOptions);
  }

  GetTellerList(companyId: string, eventid: string, userid: number): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetTellerList/" + companyId + "/" + eventid + "/" + userid, this.requestOptions)
  }

  Login(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/Login", params)
  }

  UserSave(params: any): Observable<any> {
    return this.httpClient.post(apiUrl + "/UserSave", params)
  }
  GetUserById(companyId: string, id: string, userid: string): Observable<any> {
    return this.httpClient.get(apiUrl + "/GetUserById/" + id + "/" + companyId + "/" + userid, this.requestOptions)
  }

}