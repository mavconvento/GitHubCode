import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Club } from '../../../models/clubname';
import { AlertService } from '../../../services/alert.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  form: FormGroup;
  title: string;
  mobilelist: any;
  isMobileList: boolean = false;
  isSubmit: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<ForgotPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.title = data.title;
  }

  ngOnInit() {
    this.form = this.fb.group({
      EmailAddress: ['', []],
      MobileNumber: ['', []]
    });

    this.isMobileList = true;
  }

  close() {
    this.dialogRef.close(confirm);
  }

  forgotPassword(action: string) {
    console.log(action);
    if (action == "getmobilelist") {
      this.userService.getLinkMobileByEmail(this.form.controls["EmailAddress"].value).subscribe(data => {
        var result = JSON.parse(data.content);

        this.mobilelist = result.Table;
        console.log(result);
        this.isMobileList = false;
        this.isSubmit = true;
      });
    }
    else if (action == "submit") {
      this.userService.forgotPassword(this.form.controls["EmailAddress"].value,this.form.controls["MobileNumber"].value).subscribe(data => {
        console.log(data.content);
        if (data.content == "Success") {
          this.alertService.successNotification("Password has been send to your mobile number.");
        }
        else
          this.alertService.errorNotification(data.content);

        this.close();
      }, error => { this.alertService.errorNotification(error); });
    }
  }

}
