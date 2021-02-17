import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/user';
import { AuthenticationService } from '../../services/authentication.service'

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css']
})
export class MainMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn$: Observable<boolean>;
  user$: Observable<User>;

  constructor(private authentication: AuthenticationService) {
    var local = localStorage.getItem("currentUser");

    if (local == null) {
      this.authentication.logout();
    }
  };

  ngOnInit() {
    this.isLoggedIn$ = this.authentication.Islogin;
    this.user$ = this.authentication.currentUser;
  };

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
