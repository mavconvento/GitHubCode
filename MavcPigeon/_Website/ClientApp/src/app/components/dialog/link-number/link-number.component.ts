import { error } from '@angular/compiler/src/util';
import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AlertService } from '../../../services/alert.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-link-number',
  templateUrl: './link-number.component.html',
  styleUrls: ['./link-number.component.css']
})
export class LinkNumberDialogComponent implements OnInit {
  form: FormGroup;
  isOtp: boolean;
  title: string;
  IsResend: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<LinkNumberDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {


    this.title = data.title;
  }

  ngOnInit() {
    this.isOtp = false;

    this.form = this.fb.group({
      mobileNumber: ['', []],
      otpCode: ['', []]
    });
  }

  close() {
    this.dialogRef.close();
  }

  get f() { return this.form.controls; }

  Resend() {
    this.IsResend = true;
    this.linkMobileNumber("GetOTP");
  }

  linkMobileNumber(action: string) {
    let currentUser = this.authenticationService.currentUserValue;

    this.form.addControl('userid', new FormControl(currentUser.userID, Validators.required));
    this.form.addControl('action', new FormControl('', Validators.required));

    this.form.controls["action"].setValue(action);

    this.userService.linkMobileNumber(this.form.value).subscribe(result => {
      
      if (result.content == "Success") {
        this.form.controls["otpCode"].setValue("12345");
        this.isOtp = true;
        this.IsResend = false;
      }
      else if (result.content == "LinkMobileNumber") {
        this.alertService.successNotification("Mobile Number successfully link.");
        this.close();
      }
      else {
        this.alertService.errorNotification(result.content);
      };
    }, error => {
        this.alertService.errorNotification(error);
        this.IsResend = false;
    });
  }
}
