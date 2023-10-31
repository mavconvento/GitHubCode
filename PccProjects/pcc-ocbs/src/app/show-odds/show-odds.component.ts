import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('content') content: ElementRef;
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
  fightCollection: Array<any>;
  fightCollectionPlot: any;
  currentFight: string
  winnerlabel: string
  isLastCall: boolean;
  meronEntryName: string;
  walaEntryName: string
  prevStatus: string;

  displayedColumns: string[] = ['fightno', 'declare'];

  constructor(
    private betting: BettingService,
    private router: Router,
    private event: EventsService,) {
    this.mySub = interval(1000).subscribe((func => {
      this.GetOdd()
    }));

    this.mystatus = interval(3500).subscribe((func => {
      if (localStorage.getItem("fightStatus") != "CLOSE") {
        this.buttonclosedlabel = "";
        if (this.isLastCall && localStorage.getItem("fightStatus") != "DONE") {
          setTimeout(() => {
            this.winnerlabel = 'LAST CALL BETTING';
          }, 500);
          this.winnerlabel = ''
        }
      }

      setTimeout(() => {
        this.SetFightStatus();
      }, 500);
    }))
  }

  ngAfterViewInit(): void {
    this.onGetEventDetails();
  }

  scrollToBottom = () => {
    try {
      this.content.nativeElement.scrollTop = this.content.nativeElement.scrollHeight;
    } catch (err) { }
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

          this.GetFightHistory();
          this.GetFightHistoryForPlotting();

          if (x.isLastCall) {
            this.isLastCall = true;
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
    if (fightStatus != 'DONE') this.buttonclosedlabel = fightStatus;
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

  GetFightHistory() {
    let eventid = localStorage.getItem("eventId");
    this.event.GetFightHistory(eventid).subscribe(x => {
      var result = JSON.parse(x.content)
      this.fightCollection = result;
      console.log(result);
      this.scrollToBottom();
    })
  }

  GetFightHistoryForPlotting() {
    let eventid = localStorage.getItem("eventId");
    this.event.GetFightHistoryPlotting(eventid).subscribe(x => {
      var result = JSON.parse(x.content);
      this.fightCollectionPlot = result;
      //console.log(result);
    })
  }

  GetOdd() {
    let eventid = localStorage.getItem("eventId");
    this.winnerlabel = '';
    this.event.GetCurrentFightOdds(eventid).subscribe(x => {
      var result = JSON.parse(x.content);
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

      if (result.declare == 'CANCEL')
        this.winnerlabel = 'CANCELLED FIGHT'
      else if (result.declare != '')
        this.winnerlabel = result.declare + ' ' + 'WINS'
      else {
        this.isLastCall = false;
        if (result.status == 'CLOSE') this.winnerlabel = ''
        else if (result.isLastCall) {
          this.isLastCall = true;
        }
      }

      if (result.status == 'DONE') {
        this.GetFightHistory();
        this.GetFightHistoryForPlotting();
      }

      localStorage.setItem("fightStatus", result.status);
    })
  }

}
