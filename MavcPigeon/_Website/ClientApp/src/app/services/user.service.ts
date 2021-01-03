import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base.service';
import { User } from '../models/user';
import { Helpers } from '../helpers/helpers';


@Injectable({ providedIn: 'root' })
export class UserService {
  private _baseUrl: string;
  constructor(private http: HttpClient,helper: Helpers, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getAll() {
    return this.http.get<User[]>(this._baseUrl + 'user');
  }

  public register(user: User) {
    return this.http.post(this._baseUrl + 'api/user/register', user);
  }

  delete(id: number) {
    return this.http.delete(this._baseUrl + 'user');
  }
}
