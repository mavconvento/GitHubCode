import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, ObservableLike } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User, Authenticate } from '../../app/models';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  //public authenticate: Observable<Authenticate>;
  public baseURL: String;
  constructor(
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    this.user = this.userSubject.asObservable();
    this.baseURL = baseUrl;
  }

  public get userValue(): User {
    return this.userSubject.value;
  }


  login(authenticate: Authenticate) {
    const url = this.baseURL + "account/authenticate";
    return this.http.post<User>(url, authenticate)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('token', user.token);
        localStorage.setItem('userid', user.userid);
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  register(user: User) {
    const getUrl = this.baseURL + "account/register";
    return this.http.post<String>(getUrl, user);
  }

  getAll() {
    return; //this.http.get<User[]>(`${this.baseURL}/users`);
  }

  getById(id: string) {
    return; //this.http.get<User>(`${this.baseURL}/users/${id}`);
  }

  update(id, params) {
    return;
    //return this.http.put(`${this.baseURL}/users/${id}`, params)
    //  .pipe(map(x => {
    //    // update stored user if the logged in user updated their own record
    //    if (id == this.userValue.id) {
    //      // update local storage
    //      const user = { ...this.userValue, ...params };
    //      localStorage.setItem('user', JSON.stringify(user));

    //      // publish updated user to subscribers
    //      this.userSubject.next(user);
    //    }
    //    return x;
    //  }));
  }

  delete(id: string) {
    return;
    //return this.http.delete(`${this.baseURL}/users/${id}`)
    //  .pipe(map(x => {
    //    // auto logout if the logged in user deleted their own record
    //    if (id == this.userValue.id) {
    //      this.logout();
    //    }
    //    return x;
    //  }));
  }
}
