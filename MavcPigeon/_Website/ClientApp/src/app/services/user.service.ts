import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base.service';
import { User } from '../models/user';


@Injectable({ providedIn: 'root' })
export class UserService {
  private _baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getAll() {
    return this.http.get<User[]>(this._baseUrl + 'user');
  }

  getLinkMobileList(id: string): Observable<any> {
    return this.http.get(this._baseUrl + 'api/user/GetMobileLinkList/' + id);
  }

  public register(user: User) {
    return this.http.post(this._baseUrl + 'api/user/register', user);
  }

  updateProfile(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/user/UpdateProfile';

    return this.http.post<boolean>(getURL, formData);
  }

  linkMobileNumber(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/user/LinkMobileNumber';
    return this.http.post<string>(getURL, formData);
  }

  delete(id: number) {
    return this.http.delete(this._baseUrl + 'user');
  }

  getLinkMobileByEmail(email: string) {
    let getURL = this._baseUrl + 'api/user/GetMobileByEmail?email=' + email;
    return this.http.get<any>(getURL);
  }

  forgotPassword(email: string, mobile: string) {
    let getURL = this._baseUrl + 'api/user/SendPassword?email=' + email + '&mobile=' + mobile;
    return this.http.get<any>(getURL);
  }

  getVideo(type: string) {
    let getURL = this._baseUrl + 'api/user/getVideo?type=' + type;
    return this.http.get<any>(getURL);
  }

  getMemberCoordinates(memberidno: string, clubname: string, dbname: string) {
    let getURL = this._baseUrl + 'api/user/getMemberCoordinates?memberidno=' + memberidno + '&clubname=' + clubname + '&dbname=' + dbname;
    return this.http.get<any>(getURL);
  }
}
