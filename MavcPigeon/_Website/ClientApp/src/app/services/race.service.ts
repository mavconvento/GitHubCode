import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base.service';
import { Result } from '../models/result';
import { Helpers } from '../helpers/helpers';


export class RaceService extends BaseService {
  private _baseUrl: string;
  private getUrl: string;

  constructor(private http: HttpClient, helper: Helpers, @Inject('BASE_URL') baseUrl: string) {
    super(helper);
    this._baseUrl = baseUrl;
  }

  getResult(): Observable<Result[]> {
    let getURL = this._baseUrl + 'api/weatherforecast/get';
    this.getUrl = getURL;

    return this.http.get<Result[]>(this.getUrl, super.header()).pipe(
      catchError(super.handleError));
  }
}
