import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { PrintService } from '../print.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DecimalPipe } from '@angular/common';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ConfirmDialogComponent } from '../dialog/confirm-dialog/confirm-dialog.component';
import { interval, Subscription } from 'rxjs';
import { HelperService } from '../services/helper.services';
import { UserService } from '../services/user.services';

@Component({
  selector: 'app-pcc-ocbs',
  templateUrl: './pcc-ocbs.component.html',
  styleUrls: ['./pcc-ocbs.component.css']
})
export class PccOcbsComponent implements OnInit, AfterViewInit, OnDestroy {
  myform: FormGroup;
  teller: string;
  title = 'OCBS PLATFORM';
  location: string;
  isMeron: boolean = true;
  isWala: boolean = true;
  isDraw: boolean = true;
  fightNo: string = null;
  betAmount: string = null;
  buttonclosedlabel: string = null;
  eventId: string = null;
  successmessage: string = null;
  invalidmessage: string = null;
  payout: string = null;
  isPayout: boolean = false;
  payoutErrorMessage: string = null;
  totalMeronBet: string;
  totalWalaBet: string;
  totalDrawBet: string;
  mySub: Subscription;
  mystatus: Subscription;
  current_points: string;
  isOffline: boolean;
  isAdmin: boolean = false;
  isMaster: boolean = false;
  isVip: boolean = false;
  cashOnHand: string;
  commission: string;
  bettinglist: Array<any>;
  isHidden: boolean = false;

  //table colums
  displayedColumns: string[] = ['referenceid', 'bettype', 'amount', 'reprint'];

  constructor(
    public printService: PrintService,
    private router: Router,
    private formBuilder: FormBuilder,
    private betting: BettingService,
    private event: EventsService,
    private dialog: MatDialog,
    private helper: HelperService,
    private user: UserService
  ) {
    this.mySub = interval(5000).subscribe((func => {
      this.closed()
    }))

    this.mystatus = interval(1000).subscribe((func => {
      if (localStorage.getItem("fightStatus") != "CLOSE") this.buttonclosedlabel = "";
      setTimeout(() => {
        this.SetFightStatus();
      }, 500);
    }))
  }
  ngOnDestroy(): void {
    this.mySub.unsubscribe();
    this.mystatus.unsubscribe();
  }

  ngOnInit() {
    this.location = "";

    this.teller = localStorage.getItem("teller");

    this.myform = this.formBuilder.group({
      fightNo: ['', Validators.required],
      fightId: ['', Validators.required],
      betAmount: ['', Validators.required],
      betType: ['', Validators.required],
      betfightNo: ['', Validators.required],
      meronOdds: ['', Validators.required],
      walaOdds: ['', Validators.required],
      winner: ['', Validators.required],
      totalWin: ['0.00', Validators.required],
      userId: ['', Validators.required],
      eventId: ['', Validators.required],
      platformUserId: ['', Validators.required],
      betWin: ['', Validators.required],
      refId: ['', Validators.required],
      betOffline: ['', Validators.required],
      payout: ['', Validators.required],
      payoutstatus: ['', Validators.required]
    });

    this.refresh();
  }

  ngAfterViewInit() {
    this.helper.refreshTranDate();
  }

  refresh() {
    this.eventId = localStorage.getItem("eventId");
    var fightNo = localStorage.getItem("fightNo");
    var fightId = localStorage.getItem("fightId");
    var meronodd = localStorage.getItem("meronodd");
    var walaodd = localStorage.getItem("walaodd");
    var winner = localStorage.getItem("winner");
    var betfightNo = localStorage.getItem("betfightNo");

    this.totalWalaBet = localStorage.getItem("WalaTotalBet");
    this.totalMeronBet = localStorage.getItem("MeronTotalBet");
    this.totalDrawBet = localStorage.getItem("DrawTotalBet");

    if (fightNo != 'null') this.myform.controls["fightNo"].setValue(fightNo);
    if (meronodd != 'null') this.myform.controls["meronOdds"].setValue(meronodd);
    if (walaodd != 'null') this.myform.controls["walaOdds"].setValue(walaodd);
    if (winner != 'null') this.myform.controls["winner"].setValue(winner);
    if (betfightNo != 'null') this.myform.controls['betfightNo'].setValue(betfightNo);
    if (fightId != 'null') this.myform.controls['fightId'].setValue(fightId);

    if (localStorage.getItem("IsOffline") == "true") this.isOffline = true;
    if (localStorage.getItem("roleDescription") == "Admin") this.isAdmin = true;
    if (localStorage.getItem("roleDescription") == "MasterAgent") this.isMaster = true;
    if (localStorage.getItem("roleDescription") == "VIP") this.isVip = true;

    this.closed();
    this.SetFightStatus();
    this.GetCurrentPoints();
    this.GetCashOnHand();
    this.getBettingByFightNo();
    this.onStatusClick('PAYOUT');
  }

  GetCurrentPoints() {
    let eventid: string = localStorage.getItem("eventId");
    this.betting.GetCurrentPoints(eventid).subscribe(x => {
      var result = JSON.parse(x.content);
      this.current_points = result.playing_points;
    });
  }

  GetCashOnHand() {
    this.user.GetTellerList(localStorage.getItem('companyId'), localStorage.getItem('eventId'), Number(localStorage.getItem('userId'))).subscribe(x => {
      var result = JSON.parse(x.content);

      this.commission = Number(result[0].Commision).toFixed(2)

      //for vip viewing revert sign of cash on hand
      if (this.isVip == true) {
        this.cashOnHand = (Number(result[0].CashOnhand) * -1).toFixed(2);
      }
      else
        this.cashOnHand = Number(result[0].CashOnhand).toFixed(2);

    })
  }

  confirmBet(betType: string) {
    if (Number(this.myform.controls["betAmount"].value) < 100) {
      this.showError("Amount less than minimum bet.");
      return;
    }
    else if (Number(this.myform.controls["betAmount"].value) > Number(this.cashOnHand) && this.isVip) {
      this.showError("Vip Account is out of cash points");
      return;
    }

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    let amount = this.myform.controls["betAmount"].value;
    dialogConfig.data = {
      title: 'Betting Confirmation.',
      message: amount + ' Bet to ' + betType
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.bettingSave(betType);
        }
      });
  }

  onBetAdd(amount: string) {
    if (amount == '') amount = '0';
    var value = this.myform.controls["betAmount"].value;
    var nvalue: number = Number(value) + Number(amount);
    this.myform.controls["betAmount"].setValue(String(nvalue));
  }

  onWinnerClick(value: string): void {
    this.myform.controls["winner"].setValue(value);
    //this.onChange("null");
  }

  onOddChange(Value: string): void {
    this.onChange("null");
  }

  onChange(Value: string): void {
    this.payout = "0.00";
    this.isPayout = false;

    if (Value.length == 6) {
      if (this.myform.controls["payoutstatus"].value == 'CANCEL') this.cancelBet();
      else this.claims();
    }
  }

  bettingSave(betType: string) {
    this.myform.controls["betType"].setValue(betType);
    this.myform.controls["eventId"].setValue(localStorage.getItem("eventId"));
    this.myform.controls["userId"].setValue(localStorage.getItem("userId"));
    this.myform.controls["platformUserId"].setValue(localStorage.getItem("platformUserId"));
    this.myform.controls["fightNo"].setValue(localStorage.getItem("fightNo"));
    this.myform.controls["fightId"].setValue(localStorage.getItem("fightId"));

    if (localStorage.getItem("IsOffline") == "true") {
      this.myform.controls["platformUserId"].setValue("0");
      this.myform.controls["betOffline"].setValue(localStorage.getItem("IsOffline"));
    }

    this.betting.BettingSave(this.myform.value).subscribe(x => {

      var result = JSON.parse(x.content);

      //call the invoice printing
      if (result.Status == "success") {
        this.successmessage = result.Message;
        this.getBettingByFightNo();

        localStorage.setItem("MeronTotalBet", result.MeronTotalBet);
        localStorage.setItem("WalaTotalBet", result.WalaTotalBet)
        localStorage.setItem("DrawTotalBet", result.DrawTotalBet)

        setTimeout(() => {
          this.successmessage = "";
          if (this.isVip) {
            this.refresh();
            this.myform.controls["betAmount"].setValue('');
          }
          else this.onPrintInvoice(betType, result.ReferenceId);
        }, 500);
      }
      else {
        this.showError(result.Message);
      }
    }, error => { console.log(error.error); this.showError(error.error.message) });
  }

  showError(message: string) {
    this.invalidmessage = message;
    setTimeout(() => {
      this.invalidmessage = "";
    }, 2000);
  }

  onRePrint(betType: string, barcodeValue: string, amount: string) {
    this.fightNo = this.myform.controls["fightNo"].value;
    this.betAmount = amount;
    localStorage.setItem("fightNo", this.fightNo);
    this.router.navigate(['/betprint', { fightNo: this.fightNo, betAmount: this.betAmount, betType: betType, teller: this.teller, barcodeValue: barcodeValue }])

  }

  onPrintInvoice(betType: string, barcodeValue: string) {
    this.fightNo = this.myform.controls["fightNo"].value;
    this.betAmount = this.myform.controls["betAmount"].value;
    localStorage.setItem("fightNo", this.fightNo);
    this.router.navigate(['/betprint', { fightNo: this.fightNo, betAmount: this.betAmount, betType: betType, teller: this.teller, barcodeValue: barcodeValue }])
  }

  getBettingByFightNo() {
    var fightno = localStorage.getItem("fightNo");
    var evid = localStorage.getItem("eventId");

    this.betting.GetBettingByFightNo(evid, fightno).subscribe(x => {
      var result = JSON.parse(x.content);
      this.bettinglist = result;
    });
  }

  onStatusClick(value: string): void {
    this.myform.controls["payout"].setValue(value);
    if (value == 'CANCEL')
      this.myform.controls["payoutstatus"].setValue('CANCEL');
    else {
      this.myform.controls["payoutstatus"].setValue('PAYOUT');
    }
  }

  closed() {
    this.event.GetCurrentFightOdds(this.eventId).subscribe(x => {
      var x = JSON.parse(x.content);
      if (x.requestStatus == 'success') {
        localStorage.setItem("fightNo", x.fightNo);
        localStorage.setItem("fightId", x.fightId);
        localStorage.setItem("fightStatus", x.status);

        //console.log(x)
        //check user role
        if (x.userRole == 'VIP') this.isVip = true;
        else (this.isVip = false)
        //console.log(this.isVip);
        this.myform.controls["fightNo"].setValue(x.fightNo);
        this.isMeron = true;
        this.isWala = true;
        this.isDraw = true;

        if (x.status == 'DONE' || x.status == 'CANCEL' || x.status == 'PENDING') {
          this.totalWalaBet = "0";
          this.totalMeronBet = "0";
          this.totalDrawBet = "0";

          localStorage.setItem("MeronTotalBet", "0");
          localStorage.setItem("WalaTotalBet", "0")
          localStorage.setItem("DrawTotalBet", "0")

          this.GetCurrentPoints();
          this.GetCashOnHand();
          this.getBettingByFightNo();
        }

        if (!this.isMaster && !this.isVip) {
          if (x.status == 'OPEN (WALA ONLY)') this.isMeron = false;
          if (x.status == 'OPEN (MERON ONLY)') this.isWala = false;
        }

        if (x.status == 'CLOSE' || x.status == 'PENDING' || x.status == 'DONE') {
          this.isDraw = false;
          this.isWala = false;
          this.isMeron = false;
          this.isHidden = false;
        }
      }
    })
  }

  SetFightStatus() {
    var fightStatus = localStorage.getItem("fightStatus");
    this.buttonclosedlabel = "BETTING IS " + fightStatus;
  }

  printodds() {
    let meronOdds: string;
    let walaodds: string;
    let betfightNo: string;

    meronOdds = this.myform.controls["meronOdds"].value;
    walaodds = this.myform.controls["walaOdds"].value;
    betfightNo = this.myform.controls["betfightNo"].value;

    localStorage.setItem("meronodd", this.myform.controls["meronOdds"].value)
    localStorage.setItem("walaodd", this.myform.controls["walaOdds"].value)
    localStorage.setItem("winner", this.myform.controls["winner"].value)
    localStorage.setItem("betfightNo", this.myform.controls["betfightNo"].value)
    this.router.navigate(['/oddsprint', { meronodds: meronOdds, walaodds: walaodds, fightNo: betfightNo, teller: this.teller }])
  }

  claims() {

    let refid: string;
    let bettingId: string;
    refid = this.myform.controls["refId"].value;

    this.betting.GetClaims(localStorage.getItem("eventId"), refid).subscribe(x => {
      var x = JSON.parse(x.content);

      if (x.Status == "success") {
        this.payout = x.Win;
        this.isPayout = true;

        localStorage.setItem("odds", x.Odds);
        localStorage.setItem("betfightNo", x.FightNo);
        localStorage.setItem("winner", x.WinningSide)

        this.myform.controls["totalWin"].setValue(x.Win);
        this.myform.controls["betWin"].setValue(x.BetAmount);
        this.myform.controls["meronOdds"].setValue(x.MeronOdds);
        this.myform.controls["walaOdds"].setValue(x.WalaOdds);
        this.myform.controls["winner"].setValue(x.WinningSide);
        bettingId = x.BettingId;
        this.claimpayout(bettingId);
      }
      else {
        this.payoutErrorMessage = x.Status;
        this.isPayout = false;
        setTimeout(() => {
          this.payoutErrorMessage = "";
          this.myform.controls["refId"].setValue('');
        }, 2000);
      }
    }, error => { this.showError(error.error) })
  }

  clearbet() {
    this.myform.controls["betAmount"].setValue('');
  }

  back() {
    this.router.navigate(['/main']);
  }

  claimpayout(bettingId: string) {
    let winner: string = this.myform.controls["winner"].value;
    let betwin: string = this.myform.controls["betWin"].value;
    let betfightNo: string = localStorage.getItem("betfightNo");
    let odds: string = localStorage.getItem("odds");
    let payoutAmount = this.myform.controls["totalWin"].value;

    var request: any = {};
    request.eventId = localStorage.getItem("eventId");
    request.referenceId = this.myform.controls["refId"].value;
    request.payoutAmount = payoutAmount;
    request.winningSide = localStorage.getItem("winner");
    request.odds = localStorage.getItem("odds");
    request.userId = localStorage.getItem("userId");
    request.bettingId = bettingId;

    this.betting.PayoutClaims(request).subscribe(x => {
      var x = JSON.parse(x.content);
      this.myform.controls["refId"].setValue('');
      this.router.navigate(['/claims', { winner: winner, betAmount: betwin, totalwin: payoutAmount, teller: this.teller, fightNo: betfightNo, winodds: odds, title: 'Claims' }])
    }, error => { this.showError(error.error) })
  }

  cancelBet() {
    let refid: string;
    refid = this.myform.controls["refId"].value;

    this.betting.GetCancelBet(localStorage.getItem("eventId"), refid).subscribe(x => {
      var x = JSON.parse(x.content);
      if (x.Status == "success") {
        this.myform.controls["betWin"].setValue(x.Amount);
        this.cancelBetting();
      }
      else {
        this.payoutErrorMessage = x.Status;
        this.isPayout = false;
        setTimeout(() => {
          this.payoutErrorMessage = "";
          this.myform.controls["refId"].setValue('');
        }, 2000);
      }

      this.onStatusClick('PAYOUT');
    }, error => { this.showError(error.error) })
  }

  cancelBetting() {
    let betwin: string = this.myform.controls["betWin"].value;

    var request: any = {};
    request.eventId = localStorage.getItem("eventId");
    request.referenceId = this.myform.controls["refId"].value;
    request.userId = localStorage.getItem("userId");

    this.betting.CancelBetting(request).subscribe(x => {
      var x = JSON.parse(x.content);
      if (x.Status == "success") {

        localStorage.setItem("MeronTotalBet", x.MeronTotalBet);
        localStorage.setItem("WalaTotalBet", x.WalaTotalBet)
        localStorage.setItem("DrawTotalBet", x.DrawTotalBet)

        this.myform.controls["refId"].setValue('');
        this.router.navigate(['/claims', { winner: '', betAmount: betwin, totalwin: '0', teller: this.teller, fightNo: x.FightNo, winodds: '0', title: 'Cancel' }])
      }
      else {
        this.payoutErrorMessage = x.Status;
        this.isPayout = false;
        setTimeout(() => {
          this.payoutErrorMessage = "";
          this.myform.controls["refId"].setValue('');
        }, 2000);
      }
    }, error => { this.showError(error.error) })
  }
}

