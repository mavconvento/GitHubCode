import { error } from '@angular/compiler/src/util';
import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AlertService } from '../../../services/alert.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-confirmdialog',
  templateUrl: './confirmdialog.component.html',
  styleUrls: ['./confirmdialog.component.css']
})
export class ConfirmdialogComponent implements OnInit {
  title: string;
  message: string;

  constructor(
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<ConfirmdialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

  
    this.title = data.title;
    this.message = data.message;
  }

  ngOnInit() {

  }

  close(confirm:boolean) {
    this.dialogRef.close(confirm);
  }

}
