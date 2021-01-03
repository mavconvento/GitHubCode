//import { Component, OnInit } from '@angular/core';

//@Component({
//  selector: 'app-logout',
//  templateUrl: './logout.component.html',
//  styleUrls: ['./logout.component.css']
//})
//export class LogoutComponent implements OnInit {

//  constructor() { }

//  ngOnInit() {
//  }

//}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Helpers } from '../../helpers/helpers';
import { AuthenticationService } from '../../services/authentication.service'
@Component({
  selector: 'app-logout',
  template: '<ng-content></ng-content>'
})
export class LogoutComponent implements OnInit {
  constructor(private router: Router, private helpers: Helpers, private authenticate : AuthenticationService) { }
  ngOnInit() {
    this.authenticate.logout();
    this.router.navigate(['/login']);
  }
}
