import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { HelperService } from '../services/helper.services';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ConfirmDialogComponent } from '../dialog/confirm-dialog/confirm-dialog.component';
import { ReportsService } from '../services/report.services';


@Component({
  selector: 'app-management-fight',
  templateUrl: './management-fight.component.html',
  styleUrls: ['./management-fight.component.css']
})
export class ManagementFightComponent implements OnInit, AfterViewInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  isHidden: boolean = false;
  isLastCall: boolean = false;
  isChecked: boolean = true;
  IsDone: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private betting: BettingService,
    private event: EventsService,
    private report: ReportsService,
    private helper: HelperService,
    private dialog: MatDialog,
  ) { }

  ngAfterViewInit(): void {
    this.onGetEventDetails();
    //this.helper.refreshTranDate();
  }

  back() {
    this.router.navigate(['/main']);
  }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      eventid: ['', Validators.required],
      fightno: ['', Validators.required],
      userid: ['', Validators.required],
      status: ['', Validators.required],
      declare: ['', Validators.required],
      lastCall: [false, Validators.required]
    });
  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      localStorage.setItem("eventId", result.EventId);

      this.event.GetFights(result.EventId).subscribe(x => {
        var x = JSON.parse(x.content);
        //console.table(x);
        if (x.requestStatus == 'success') {
          localStorage.setItem("fightNo", x.fightNo);
          localStorage.setItem("fightStatus", x.status);
          localStorage.setItem("fightId", x.fightId);
          this.myform.controls["fightno"].setValue(x.fightNo);
          this.myform.controls["status"].setValue(x.status);
          this.onStatusClick(x.status);
          this.onWinnerClick(x.declare);

          this.isChecked = x.isLastCall;
          if (x.status == 'DONE' || x.status == 'CANCEL') this.IsDone = true;
          else this.IsDone = false;
        }
      })
    }, error => { this.showerror(error.error.message) });
  }

  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }

  onWinnerClick(value: string): void {
    this.myform.controls["declare"].setValue(value);
  }

  onLastCallClick(value: boolean) {
    this.isChecked = value;
    //this.FightSave('');
  }

  onStatusClick(value: string): void {
    this.myform.controls["status"].setValue(value);
    if (value == 'DONE') {
      this.isHidden = true
      this.isChecked = false;
    }
    else if (value == 'CANCEL')
      this.myform.controls["declare"].setValue('CANCEL');
    else {
      this.isHidden = false;
      this.myform.controls["declare"].setValue('');
    }
  }

  onNextFight() {
    this.confirmBet();
  }

  confirmBet() {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      title: 'Next Fight.',
      message: 'You would like to set new fight?'
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.FightSave('next')
        }
      });
  }

  showError(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 2000);
  }

  FightPrint() {
    let fightno = Number(this.myform.controls["fightno"].value);
    let eventid = Number(localStorage.getItem("eventId"));
    this.report.GetBettingReportByFightNo(eventid, fightno).subscribe(x => {
      var x = JSON.parse(x.content);
      //console.table(x);
      this.PrintReport(x.TotalAmount, x.EventId, x.FightNo, x.Status, x.Declare, x.Commission, x.PayoutOdd, x.Meron, x.Wala);
    });
  }

  PrintReport(totalAmount: string, eventId: string, fightNo: string, status: string, declare: string, commission: string, odds: string, meron: string, wala: string) {
    this.router.navigate(['/reportprint', { totalAmount: totalAmount, eventId: eventId, fightNo: fightNo, status: status, declare: declare, commission: commission, odds: odds, meron: meron, wala: wala }])
  }

  FightSave(action: string) {
    let fightno = Number(this.myform.controls["fightno"].value)
    if (action == 'next') {
      fightno = Number(this.myform.controls["fightno"].value) + 1;
      this.myform.controls["fightno"].setValue(fightno.toString());
      this.myform.controls["lastCall"].setValue(false);
      this.isChecked = false;
      this.onStatusClick('PENDING');
    }

    this.myform.controls["eventid"].setValue(localStorage.getItem("eventId"))
    this.myform.controls["userid"].setValue(localStorage.getItem("userId"))
    this.myform.controls["lastCall"].setValue(this.isChecked);

    if (this.myform.controls["status"].value == 'DONE' && this.myform.controls["declare"].value == '')
      this.showError('No declare winner selected.');
    else {
      this.event.FightOfflineSave(this.myform.value).subscribe(x => {
        this.isShow = true;
        this.onGetEventDetails();
        this.showError("Fight Details Save.")
      }, error => {
        this.showError(error.error);
        fightno = fightno - 1;
        this.myform.controls["fightno"].setValue(fightno.toString());
      });
    }

  }
}
