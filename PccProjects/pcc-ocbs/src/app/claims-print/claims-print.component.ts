import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
//import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PrintService } from '../print.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-claims-print',
  templateUrl: './claims-print.component.html',
  styleUrls: ['./claims-print.component.css']
})
export class ClaimsPrintComponent implements OnInit {
  winner: string;
  betAmount: string;
  totalwin: string;
  teller: string;
  fightNo: string
  winodds: string;
  title: string;
  isPrint: boolean;
  barcode: string;
  isCancel: boolean = false;
  betType: string;

  constructor(
    private route: ActivatedRoute,
    private printService: PrintService,
    private router: Router
  ) { }


  ngOnInit() {
    this.winner = this.route.snapshot.params['winner'];
    this.betAmount = this.route.snapshot.params['betAmount'];
    this.totalwin = this.route.snapshot.params['totalwin'];
    this.teller = this.route.snapshot.params['teller'];
    this.fightNo = this.route.snapshot.params['fightNo'];
    this.winodds = this.route.snapshot.params['winodds'];
    this.title = this.route.snapshot.params['title'];
    this.isPrint = this.route.snapshot.params['isprint'];
    this.barcode = this.route.snapshot.params['barcode'];

    if (this.title == 'Cancel') this.isCancel = true;
    //this.betType = this.route.snapshot.params['bettype'];
  }

  ngAfterViewInit() {
    //this.onRePrintInvoice();
  }

  onRePrintInvoice() {
    const invoiceIds = [];
    if (this.isPrint) this.printService.printDocument('invoice', invoiceIds);
    else if (this.winner == 'WALA' || this.winner == 'MERON') this.printService.printDocument('invoice', invoiceIds);
  }

  onPrintInvoice() {
    this.router.navigate(['/'])
  }
}
