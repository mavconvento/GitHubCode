import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base.service';
import { User } from '../models/user';


@Injectable({ providedIn: 'root' })
export class AccountService {
  private _baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getAll() {
    return this.http.get<User[]>(this._baseUrl + 'user');
  }

  setAsPrimary(mobileNumber: string, userID: string): Observable<any> {
    let getURL = this._baseUrl + 'api/account/SetAsPrimary';
    return this.http.post<string>(getURL, { MobileNumber: mobileNumber, userID: userID});
  }

  loadMavcCard(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/account/LoadMavcCard';
    return this.http.post<string>(getURL, formData);
  }

  pasaload(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/account/Pasaload';
    return this.http.post<string>(getURL, formData);
  }

  Unreg(mobileNumber: string,userID: string, clubName: string, dbName: string): Observable<any> {
    let getURL = this._baseUrl + 'api/account/Unreg';
    return this.http.post<string>(getURL, { mobileNumber: mobileNumber, userID: userID, clubName : clubName, dbName: dbName});
  }

  getLinkMobileByEmail(email: string) {
    let getURL = this._baseUrl + 'api/account/LoadMavcCard';
  }

  forgotPassword(mobile: string) {
    let getURL = this._baseUrl + 'api/account/LoadMavcCard';
  }
}

