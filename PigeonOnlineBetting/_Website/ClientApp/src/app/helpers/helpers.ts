import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs';
import { AlertService } from '../services/alert.service';
import { AuthenticationService } from '../services/authentication.service';
import { RaceService } from '../services/race.service';
import { UserService } from '../services/user.service';

@Injectable({ providedIn: 'root' })
export class Helpers {
  private authenticationChanged = new Subject<boolean>();
  constructor(
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private raceService: RaceService,
    private userService: UserService
  ) { }

  public isAuthenticated(): boolean {
    return (!(window.localStorage['token'] === undefined ||
      window.localStorage['token'] === null ||
      window.localStorage['token'] === 'null' ||
      window.localStorage['token'] === 'undefined' ||
      window.localStorage['token'] === ''));
  }

  public isAuthenticationChanged(): any {
    return this.authenticationChanged.asObservable();
  }

  public getToken(): any {
    if (window.localStorage['token'] === undefined ||
      window.localStorage['token'] === null ||
      window.localStorage['token'] === 'null' ||
      window.localStorage['token'] === 'undefined' ||
      window.localStorage['token'] === '') {
      return '';
    }
    let obj = JSON.parse(window.localStorage['token']);
    return obj.token;
  }
  public setToken(data: any): void {
    this.setStorageToken(JSON.stringify(data));
  }
  public failToken(): void {
    this.setStorageToken(undefined);
  }
  public logout(): void {
    this.setStorageToken(undefined);
  }

  private setStorageToken(value: any): void {
    window.localStorage['token'] = value;
    this.authenticationChanged.next(this.isAuthenticated());
  }
}
