import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';

@Component({
  selector: 'app-main-ocbs',
  templateUrl: './main-ocbs.component.html',
  styleUrls: ['./main-ocbs.component.css']
})
export class MainOcbsComponent implements OnInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  isAdmin: boolean = false;
  isSuperAdmin: boolean = false;
  isSupervisor: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private event: EventsService
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      teller: ['', Validators.required]
    });

    this.myform.controls["teller"].setValue(localStorage.getItem("firstName"))
    if (localStorage.getItem("roleDescription") == "Admin") this.isAdmin = true;
    if (localStorage.getItem("IsSuperAdmin") == 'true') this.isSuperAdmin = true;
    if (localStorage.getItem("roleDescription") == 'Supervisor') this.isSupervisor = true;
    this.GetCurrentEvent();
  }

  // convenience getter for easy access to form fields
  get myFormControl() { return this.myform.controls; }

  onUser() {
    this.router.navigate(['/users'])
  }

  GetCurrentEvent() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      localStorage.setItem("teller", this.myform.controls["teller"].value);
      localStorage.setItem("eventId", result.EventId);
      localStorage.setItem("eventname", result.Description)

      this.event.GetFights(result.EventId).subscribe(x => {
        var x = JSON.parse(x.content);

        if (x.requestStatus == 'success') {
          localStorage.setItem("fightNo", x.fightNo);
          localStorage.setItem("fightStatus", x.status);
          localStorage.setItem("fightId", x.fightId);
        }
      })
    }, error => { this.showerror(error.error.message) });
  }

  onBetting() {
    this.router.navigate(['/'])
  }

  logout() {
    this.router.navigate(['/login'])
  }

  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }

  onUnclaimed() {
    this.router.navigate(['/unclaimed'])
  }

  onBetMonitoring() {
    this.router.navigate(['/betmonitoring'])
  }

  onMoneyCounter() {
    this.router.navigate(['/moneycounter'])
  }

  onFight() {
    this.router.navigate(['/fight'])
  }

  onBettingHistory() {
    this.router.navigate(['/bettinghistory'])
  }

  onEvent() {
    this.router.navigate(['/events'])
  }

  onReport() {
    this.router.navigate(['/reportsummary'])
  }

  onPoints() {
    this.router.navigate(['/points'])
  }

  showOdds() {
    this.router.navigate(['/showodds'])
  }
}
