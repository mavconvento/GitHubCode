import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PrintService } from '../print.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bet-print',
  templateUrl: './bet-print.component.html',
  styleUrls: ['./bet-print.component.css']
})
export class BetPrintComponent implements OnInit, AfterViewInit {
  fightNo: string;
  betAmount: string;
  betType: string;
  teller: string;
  barcodeValue: string;
  eventname: string;

  constructor(
    private route: ActivatedRoute,
    private printService: PrintService,
    private router: Router
  ) { }

  ngOnInit() {
    this.fightNo = this.route.snapshot.params['fightNo'];
    this.betAmount = this.route.snapshot.params['betAmount'];
    this.betType = this.route.snapshot.params['betType'];
    this.teller = this.route.snapshot.params['teller'];
    this.barcodeValue = this.route.snapshot.params['barcodeValue'];

    this.eventname = localStorage.getItem("eventname");
  }

  ngAfterViewInit() {
    const invoiceIds = [];
    this.printService.printDocument('invoice', invoiceIds);
    //setTimeout(() => {this.router.navigate(['/'])},1000);
  }

  onPrintInvoice() {
    this.router.navigate(['/'])
  }
}
