import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PrintService } from '../../print.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-report-print',
  templateUrl: './report-print.component.html',
  styleUrls: ['./report-print.component.css']
})
export class ReportPrintComponent implements OnInit {
  totalAmount: string;
  fightNo: string
  eventId: string
  status: string;
  declare: string;
  commission: string;
  odds: string

  winodds: string;
  title: string;

  constructor(
    private route: ActivatedRoute,
    private printService: PrintService,
    private router: Router
  ) { }


  ngOnInit() {
    console.log(this.route.snapshot.params['totalAmount']);
    this.totalAmount = this.route.snapshot.params['totalAmount'];
    this.fightNo = this.route.snapshot.params['fightNo'];
    this.eventId = this.route.snapshot.params['eventId'];
    this.status = this.route.snapshot.params['status'];
    this.declare = this.route.snapshot.params['declare'];
    this.commission = this.route.snapshot.params['commission'];
    this.odds = this.route.snapshot.params['odds'];
  }

  ngAfterViewInit() {
    const invoiceIds = [];
    this.printService.printDocument('invoice', invoiceIds);
  }

  onPrintInvoice() {
    this.router.navigate(['/fight'])
  }
}
