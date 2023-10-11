import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { EventsService } from 'src/app/services/event.services';

@Component({
  selector: 'app-confirm-fightno',
  templateUrl: './confirm-fightno.component.html',
  styleUrls: ['./confirm-fightno.component.css']
})
export class ConfirmFightnoComponent implements OnInit {

  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  isAdmin: boolean = false;
  isSuperAdmin: boolean = false;
  isSupervisor: boolean = false;
  data: any;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private event: EventsService,
    private dialogRef: MatDialogRef<ConfirmFightnoComponent>,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.data = data;
  }

  close(confirm: boolean) {
    if (confirm)
      this.data.password = this.myform.controls["svPassword"].value
    else
      this.data.password = '';

    this.dialogRef.close(this.data);
  }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      svPassword: ['', Validators.required]
    });

    if (localStorage.getItem("roleDescription") == "Admin") this.isAdmin = true;
    if (localStorage.getItem("IsSuperAdmin") == 'true') this.isSuperAdmin = true;
    if (localStorage.getItem("roleDescription") == 'Supervisor') this.isSupervisor = true;
    //this.GetCurrentEvent();
  }

}
