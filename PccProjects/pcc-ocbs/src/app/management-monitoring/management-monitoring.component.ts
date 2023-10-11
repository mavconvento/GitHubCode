import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { UserService } from '../services/user.services';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-management-monitoring',
  templateUrl: './management-monitoring.component.html',
  styleUrls: ['./management-monitoring.component.css']
})
export class ManagementMonitoringComponent implements OnInit, OnDestroy {
  mySub: Subscription;
  myform: FormGroup;
  bettinglist: Array<any>;
  errorMessage: string;
  isShow: boolean = false;
  tellerlist: Array<any>;

  //table colums
  displayedColumns: string[] = ['referenceid', 'fightno', 'amount', 'bettype', 'teller'];

  constructor(
    private betting: BettingService,
    private formBuilder: FormBuilder,
    private user: UserService,
    private event: EventsService,
    private router: Router
  ) {
    this.mySub = interval(8000).subscribe((func => {
      this.closed()
    }))
  }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      Teller: ['', Validators.required],
    });

    this.getHighBettingByFightNo();
  }

  closed() {
    let eventId = localStorage.getItem("eventId");
    this.event.GetCurrentFightOdds(eventId).subscribe(x => {
      var x = JSON.parse(x.content);
      if (x.requestStatus == 'success') {
        localStorage.setItem("fightNo", x.fightNo);
        localStorage.setItem("fightId", x.fightId);
        localStorage.setItem("fightStatus", x.status);
        localStorage.setItem("declare", x.declare);
        this.getHighBettingByFightNo();
      }
    })
  }

  ngOnDestroy(): void {
    this.mySub.unsubscribe();
  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      var role = localStorage.getItem("roleDescription");
      var userid = localStorage.getItem("userId");

      localStorage.setItem("eventId", result.EventId);
      this.getTellers(localStorage.getItem('companyId'), result.EventId).then(x => {
        if (role == 'Admin' || role == 'Supervisor')
          this.tellerlist = x;
        else {
          this.tellerlist = x.filter(a => a.Userid == userid);
        }
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

  getHighBettingByFightNo() {
    var fightno = localStorage.getItem("fightNo");
    var evid = localStorage.getItem("eventId");

    this.betting.GetHighBettingByFightNo(evid, fightno).subscribe(x => {
      var result = JSON.parse(x.content);
      this.bettinglist = result;
    });
  }

  GetUnClaimedTicket(userid: string): void {
    this.betting.GetBettingHistoryByEvent(localStorage.getItem("eventId"), userid).subscribe(x => {
      var result = JSON.parse(x.content);
      this.bettinglist = result;
    });
  }
}
