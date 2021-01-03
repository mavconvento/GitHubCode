import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { Inject } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  private _baseUrl: String;
  public currentUser: Observable<User>;
  private isUserLoginSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
    this._baseUrl = baseUrl;
  }

  public get currentUserValue(): any {
    return this.currentUserSubject.value;
  }

  public get Islogin(): Observable<boolean> {
    return this.isUserLoginSubject.asObservable();
  };

  login(username, password) {
    return this.http.post<any>(this._baseUrl + 'api/user/authenticate', { username, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        this.isUserLoginSubject.next(true);
        return user;
      }));
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('currentUser');
    this.isUserLoginSubject.next(false);
    this.currentUserSubject.next(null);
  }
}
