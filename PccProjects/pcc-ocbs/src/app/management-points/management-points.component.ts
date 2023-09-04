import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { UserService } from '../services/user.services';
import { HelperService } from '../services/helper.services';

@Component({
  selector: 'app-management-points',
  templateUrl: './management-points.component.html',
  styleUrls: ['./management-points.component.css']
})
export class ManagementPointsComponent implements OnInit, AfterViewInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  isHidden: boolean = false;
  tellerlist: Array<any>;
  pointslist: Array<any>;

  //table colums
  displayedColumns: string[] = ['pointsamount', 'type', 'approvedby', 'daterequested'];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private betting: BettingService,
    private event: EventsService,
    private user: UserService,
  ) { }

  ngAfterViewInit(): void {
    this.onGetEventDetails();
    //this.helper.refreshTranDate();
  }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      Teller: ['', Validators.required],
      Amount: [''],
      UserId: ['', Validators.required],
      TellerId: ['', Validators.required],
      Type: ['', Validators.required],
      Eventid: ['', Validators.required],
      cash_advance: ['', Validators.required],
      total_balance: ['', Validators.required],
      total_payout: ['', Validators.required],
      total_betting: ['', Validators.required],
      commission: ['', Validators.required],
    });
  }

  searchteller(event) {
    var tellerSearch = this.tellerlist.filter(x => x.UserName.toUpperCase().indexOf(event.toUpperCase()) > -1);
    this.myform.controls["cash_advance"].setValue(Number(tellerSearch[0].CashAdvance));
    this.myform.controls["total_betting"].setValue(Number(tellerSearch[0].TotalBetRunning).toFixed(2));
    this.myform.controls["total_payout"].setValue(Number(tellerSearch[0].Payout).toFixed(2));
    this.myform.controls["commission"].setValue(Number(tellerSearch[0].Commision).toFixed(2));
    this.myform.controls["total_balance"].setValue(Number(tellerSearch[0].CashOnhand).toFixed(2));

    this.getpointhistory(tellerSearch[0].Userid);
  }

  onStatusClick(value: string): void {
    this.myform.controls["Type"].setValue(value);
  }

  getpointhistory(userid: string): void {
    this.betting.GetPointsHistory(localStorage.getItem("eventId"), userid).subscribe(x => {
      var result = JSON.parse(x.content);
      this.pointslist = result;
    });
  }

  async getTellers(id: string, eventid: string): Promise<any> {
    var data = await this.user.GetTellerList(id, eventid, 0).toPromise();
    return JSON.parse(data.content);
  }

  back() {
    this.router.navigate(['/main']);
  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      localStorage.setItem("eventId", result.EventId);
      this.getTellers(localStorage.getItem('companyId'), result.EventId).then(x => {
        this.tellerlist = x;
        if (this.myform.controls["Teller"].value != '' && this.myform.controls["Teller"].value != null) this.searchteller(this.myform.controls["Teller"].value);
      });
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

  tellerSave() {
    var tel = this.tellerlist.filter(x => x.UserName == this.myform.controls["Teller"].value)
    this.myform.controls["TellerId"].setValue(tel[0].Userid.toString());
    this.myform.controls["UserId"].setValue(localStorage.getItem("userId"));
    this.myform.controls["Eventid"].setValue(localStorage.getItem("eventId"));
    this.betting.TellerPointSave(this.myform.value).subscribe(x => {
      this.onGetEventDetails();
      this.myform.controls["Type"].setValue('');
      this.myform.controls["Amount"].setValue('');
    });
  }
}
