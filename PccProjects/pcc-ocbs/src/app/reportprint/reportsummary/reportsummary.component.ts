import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ReportsService } from 'src/app/services/report.services';
import { BettingService } from '../../services/betting.services';
import { EventsService } from '../../services/event.services';
import { UserService } from '../../services/user.services';

@Component({
  selector: 'app-reportsummary',
  templateUrl: './reportsummary.component.html',
  styleUrls: ['./reportsummary.component.css']
})
export class ReportsummaryComponent implements OnInit {

  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  eventList: Array<any>;
  bettingreportlist: Array<any>
  scrHeight: any;
  scrWidth: any;
  isMobile: boolean = false;

  //table colums
  displayedColumns: string[] = ['fightNo', 'meron', 'wala', 'total', 'commission', 'declare'];
  //table colums
  displayedColumnsMobile: string[] = ['fightNo', 'details'];

  constructor(
    private formBuilder: FormBuilder,
    private user: UserService,
    private event: EventsService,
    private report: ReportsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      Events: ['', Validators.required],
    });

    this.GetEventList();
    this.getScreenSize();
  }

  async GetEventList() {
    this.event.GetEventById(0).subscribe(x => {
      var result = JSON.parse(x.content);
      this.eventList = result;
    })
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.scrHeight = window.innerHeight;
    this.scrWidth = window.innerWidth;

    if (this.scrWidth < 1008) {
      this.isMobile = true;
    }
    else {
      this.isMobile = false;
    }

  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      var role = localStorage.getItem("roleDescription");
      var userid = localStorage.getItem("userId");

      localStorage.setItem("eventId", result.EventId);
      this.getTellers(localStorage.getItem('companyId'), result.EventId).then(x => {
        if (role == 'Admin')
          this.eventList = x;
        else {
          this.eventList = x.filter(a => a.Userid == userid);
        }

        if (this.myform.controls["Teller"].value != '' && this.myform.controls["Teller"].value != null) this.searchevent(this.myform.controls["Events"].value);
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

  async getTellers(id: string, eventid: string): Promise<any> {
    var data = await this.user.GetTellerList(id, eventid, 0).toPromise();
    return JSON.parse(data.content);
  }

  back() {
    this.router.navigate(['/main']);
  }

  searchevent(event) {
    var eventSearch = this.eventList.filter(x => x.EventId.indexOf(event) > -1);
    console.log(eventSearch);
    this.GetReportSummary(eventSearch[0].EventId);
  }

  GetReportSummary(eventid: string): void {
    this.report.GetBettingReportSummary(eventid).subscribe(x => {
      var result = JSON.parse(x.content);
      console.table(result);
      this.bettingreportlist = result;
    });
  }
}
