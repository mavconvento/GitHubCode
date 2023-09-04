import { AfterViewInit, Component, Inject, Input, OnInit } from '@angular/core';   
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ConfirmDialogService } from 'src/app/services/ConfirmDialogService';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit, AfterViewInit {
  title: string;
  message: string;
  data:any;
  constructor(
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.data = data;
   
  }

  ngOnInit() {
    this.title = this.data.title;
    this.message = this.data.message;
  }

  ngAfterViewInit(): void {
   
  }

  close(confirm:boolean) {
    this.dialogRef.close(confirm);
  }

}
