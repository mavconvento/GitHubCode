import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { Jsonp } from '@angular/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-show-odds',
  templateUrl: './show-odds.component.html',
  styleUrls: ['./show-odds.component.css']
})
export class ShowOddsComponent implements OnInit, OnDestroy, AfterViewInit {
  meronodd: string;
  merontotal: string;
  walaodd: string;
  walatotal: string;
  drawtotal: string;
  mySub: Subscription;
  mystatus: Subscription;
  buttonclosedlabel: string;
  fightno: string;
  items: any
  currentFight: string
  winnerlabel: string
  isLastCall: boolean;


  constructor(
    private betting: BettingService,
    private router: Router,
    private event: EventsService,) {
    this.mySub = interval(3000).subscribe((func => {
      this.GetOdd()
    }));

    this.mystatus = interval(1000).subscribe((func => {
      if (localStorage.getItem("fightStatus") != "CLOSE") this.buttonclosedlabel = "";

      setTimeout(() => {
        this.SetFightStatus();
      }, 500);
    }))
  }

  ngAfterViewInit(): void {
    this.onGetEventDetails();
  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      localStorage.setItem("eventId", result.EventId);

      this.event.GetFights(result.EventId).subscribe(x => {
        var x = JSON.parse(x.content);
        if (x.requestStatus == 'success') {
          localStorage.setItem("fightNo", x.fightNo);
          localStorage.setItem("fightStatus", x.status);
          localStorage.setItem("fightId", x.fightId);

          console.log(x);
          if (x.isLastCall) {
            this.isLastCall = true;
            this.winnerlabel = 'LAST CALL BETTING';
          }
          else {
            this.isLastCall = false;
            this.winnerlabel = '';
          }
        }
      })
    }, error => { console.log(error.error.message) });
  }

  SetFightStatus() {
    var fightStatus = localStorage.getItem("fightStatus");
    this.buttonclosedlabel = "BETTING IS " + fightStatus;
  }

  ngOnDestroy(): void {
    this.mySub.unsubscribe();
    this.mystatus.unsubscribe();
  }

  GetWinners() {
    this.betting.GetPlotWinners(localStorage.getItem("eventId")).subscribe(x => {
      var result = JSON.parse(x.content);
      this.items = result;
    });
  }

  ngOnInit() {
    this.GetOdd();
    this.GetWinners();
  }

  back() {
    this.router.navigate(['/main']);
  }

  GetOdd() {
    let eventid = localStorage.getItem("eventId");
    this.winnerlabel = '';
    this.event.GetCurrentFightOdds(eventid).subscribe(x => {
      var result = JSON.parse(x.content);
      console.log(result);
      this.merontotal = result.MeronTotal;
      this.meronodd = result.MeronOdds;

      this.walaodd = result.WalaOdds;
      this.walatotal = result.WalaTotal;

      this.drawtotal = result.DrawTotal;
      this.fightno = result.fightNo;

      if (this.currentFight != result.fightNo) {
        //this.GetWinners()
        this.currentFight = result.fightNo
      }

      if (result.declare != '')
        this.winnerlabel = result.declare + ' ' + 'WINS'
      else {
        if (result.status == 'CLOSE') this.winnerlabel = ''
        else if (result.isLastCall) this.winnerlabel = 'LAST CALL BETTING'
      }

      localStorage.setItem("fightStatus", result.status);
    })
  }

}
