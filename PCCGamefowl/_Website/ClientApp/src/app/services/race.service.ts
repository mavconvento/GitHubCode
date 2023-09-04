import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class RaceService
{
  private _baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getRaceResult(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/race/getRaceResult';
    return this.http.post<any>(getURL, formData);
  }

  getRaceDetails(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/race/getRaceDetails';
    return this.http.post<any>(getURL, formData);
  }

  getRaceEntry(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/race/getRaceEntry';
    return this.http.post<any>(getURL, formData);
  }

  getRaceCategory(dbName: string, clubName: string): Observable<any> {
    return this.http.get(this._baseUrl + 'api/race/getRaceCategory?dbName=' + dbName + "&clubName=" + clubName);
  }

  getRaceGroup(dbName: string, clubName: string): Observable<any> {
    return this.http.get(this._baseUrl + 'api/race/getRaceGroup?dbName=' + dbName + "&clubName=" + clubName);
  }

  getBalance(mobilenumber: string): Observable<any> {
    return this.http.get(this._baseUrl + 'api/race/getBalance?mobileNumber=' + mobilenumber);
  }

  onlineClocking(formData: FormData): Observable<any> {
    let getURL = this._baseUrl + 'api/race/onlineClocking';
    return this.http.post<any>(getURL, formData);
  }
}
