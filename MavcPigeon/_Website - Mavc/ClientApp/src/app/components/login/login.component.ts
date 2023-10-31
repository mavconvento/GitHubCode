import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { TokenService } from '../../services/token.service';
import { Helpers } from '../../helpers/helpers';
import { AuthenticationService } from '../../services/authentication.service'
import { AlertService } from '../../services/alert.service'
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ForgotPasswordComponent } from '../dialog/forgot-password/forgot-password.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
//export class LoginComponent implements OnInit {
//  constructor(private helpers: Helpers, private router: Router, private tokenService: TokenService) {
//  }
//  ngOnInit() {
//  }


//  //login(): void {
//  //  let authValues = { "Username": "pablo", "Password": "secret" };
//  //  this.tokenService.auth(authValues).subscribe(token => {
//  //    this.helpers.setToken(token);
//  //    this.router.navigate(['/dashboard']);
//  //  });
//  //}
//}

export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/profile']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });


    if (localStorage.getItem("userlogin")) {
      this.loginForm.controls["username"].setValue(localStorage.getItem("userlogin"));
      this.loginForm.controls["password"].setValue(localStorage.getItem("userpassword"));
    }

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/profile';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          console.log(data.eclockID);
          localStorage.setItem("eclockID", data.eclockID);
          localStorage.setItem("userlogin", this.f.username.value);
          localStorage.setItem("userpassword", this.f.password.value);
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.alertService.errorNotification(error);
          this.loading = false;
        });
  }

  forgotPassword() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      title: 'Forgot Password'
    };

    const dialogRef = this.dialog.open(ForgotPasswordComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        console.log(data);
      });

    console.log("forgot password");
  }
}
