import { error } from '@angular/compiler/src/util';
import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AlertService } from '../../../services/alert.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-online-clocking',
  templateUrl: './online-clocking.component.html',
  styleUrls: ['./online-clocking.component.css']
})
export class OnlineClockingDialogComponent implements OnInit {
  form: FormGroup;
  title: string;
  isLoadCard: boolean;
  isPasaload: boolean;
  pasaloadBalance: string;
  mobilenumber: string;
  userID: string;
  clubName: string;
  dbName: string;
  noLinkMobileMessage: string;


  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<OnlineClockingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

  
    this.title = data.title;
    this.isLoadCard = data.isLoadCard;
    this.isPasaload = data.isPasaload;
    this.pasaloadBalance = data.pasaloadBalance;
    this.mobilenumber = data.mobilenumber;
    this.clubName = data.clubName;
    this.dbName = data.dbName;
  }

  ngOnInit() {
    let currentUser = this.authenticationService.currentUserValue;

    this.form = this.fb.group({
      mobileNumberLoadReceiver: ['', []],
      pinNumber: ['', []],
      amount: ['', []],
      userid: ['', []],
      Keyword: ['', []],
      mobilenumber: ['', []],
      clubName: ['', []],
      dbName: ['', []]
    });

  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  close() {
    this.dialogRef.close();
  }

  submit() {

    this.form.controls["userid"].setValue(this.userID);
    this.form.controls["mobilenumber"].setValue(this.mobilenumber);
    this.form.controls["clubName"].setValue(this.clubName);
    this.form.controls["dbName"].setValue(this.dbName);

    if (this.isLoadCard) {
      if (this.f["pinNumber"].value != "") {
        this.form.controls["Keyword"].setValue("Load " + this.f["pinNumber"].value);
        this.accountService.loadMavcCard(this.form.value).subscribe(result => {
          var data = JSON.parse(result.content);
    
          this.alertService.simpleNotification(data.Table[0].Result);
          this.close();
        },
          error => { this.alertService.errorNotification(error) });
      }
    }
    else if (this.isPasaload) {
      if (this.f["amount"].value != "" && this.f["mobileNumberLoadReceiver"].value) {
        if (Number(this.f["amount"].value) > 0) {
         
          this.accountService.pasaload(this.form.value).subscribe(result => {
            var data = JSON.parse(result.content);
           
            this.alertService.simpleNotification(data.Table[0].Result);
            this.close();
          },
            error => { this.alertService.errorNotification(error) });
        }
      }
      else this.alertService.errorNotification("Enter mobile number and pasaload amount.")
    }


  }

}
