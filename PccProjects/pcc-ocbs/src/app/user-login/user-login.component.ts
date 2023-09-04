import { Component, OnInit } from '@angular/core';
import { PrintService } from '../print.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../environments/environment';
import { DecimalPipe, formatNumber } from '@angular/common';
import { BettingService } from '../services/betting.services';
import { UserService } from '../services/user.services';
import { HelperService } from '../services/helper.services';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  constructor(
    private user: UserService,
    private router: Router,
    private formBuilder: FormBuilder,
    private helper: HelperService) { }

  ngOnInit() {
    this.helper.ClearLoginSession();
    this.myform = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });

  }

  onRegister() {
    this.router.navigate(['/registration']);
  }

  onLogin() {
    this.user.Login(this.myform.value).subscribe(data => {
      var result = JSON.parse(data.content)
      if (result.Status == 'success') {
        localStorage.setItem("tokenBearer", result.platformBearerToken);
        localStorage.setItem("firstName", result.firstName);
        localStorage.setItem("userId", result.userId);
        localStorage.setItem("platformUserId", result.platformUserId);
        localStorage.setItem("IsOffline", result.IsOffline);
        localStorage.setItem("companyId", result.companyId);
        localStorage.setItem("roleDescription", result.roleDescription);
        localStorage.setItem("lasttransaction", Date());
        this.router.navigate(["/main"]);
      }
      else
        this.showerror('Invalid Username or Password.')
    }, error => { this.showerror(error.error) })
  }

  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }
}
