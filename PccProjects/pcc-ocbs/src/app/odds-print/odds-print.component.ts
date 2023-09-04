import { Component, OnInit } from '@angular/core';
import {PrintService} from '../print.service';
import {Router,ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-odds-print',
  templateUrl: './odds-print.component.html',
  styleUrls: ['./odds-print.component.css']
})
export class OddsPrintComponent implements OnInit {

  fightNo:string;
  walaodds:string;
  meronodds: string;
  teller: string

  constructor(
    private route: ActivatedRoute,
    private printService: PrintService,
    private router: Router
  ) { }

  ngOnInit() {
   this.walaodds = this.route.snapshot.params['walaodds'];
   this.meronodds = this.route.snapshot.params['meronodds'];
   this.fightNo = this.route.snapshot.params['fightNo']; 
   this.teller = this.route.snapshot.params['teller'];
  }

  ngAfterViewInit()
  {
    const invoiceIds = [];
    this.printService.printDocument('invoice', invoiceIds);
    //setTimeout(() => {this.router.navigate(['/'])},1000);
  }

  onPrintInvoice() {
    this.router.navigate(['/'])
  }

}
