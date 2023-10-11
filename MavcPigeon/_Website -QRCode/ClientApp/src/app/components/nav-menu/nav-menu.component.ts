import { Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/user';
import { AuthenticationService} from '../../services/authentication.service'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit  {
  isExpanded = false;
  isLoggedIn$: Observable<boolean>;
  user$: Observable<User>;
 

  constructor(private authentication: AuthenticationService) {
    this.isLoggedIn$ = this.authentication.Islogin;
    this.user$ = this.authentication.currentUser;

    
  };

  ngOnInit() {

  };
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
