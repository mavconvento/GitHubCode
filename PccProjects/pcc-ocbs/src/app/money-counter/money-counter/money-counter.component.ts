import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EventsService } from 'src/app/services/event.services';

@Component({
  selector: 'app-money-counter',
  templateUrl: './money-counter.component.html',
  styleUrls: ['./money-counter.component.css']
})
export class MoneyCounterComponent implements OnInit {
  myform: FormGroup;
  errorMessage: string;
  total: number;
  ten = 0;
  twenty = 0;
  fifty = 0;
  onehundred = 0;
  twohundred = 0;
  fivehundred = 0;
  onethousand = 0;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private event: EventsService
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      ten: ['', Validators.required],
      twenty: ['', Validators.required],
      fifty: ['', Validators.required],
      onehundred: ['', Validators.required],
      twohundred: ['', Validators.required],
      fivehundred: ['', Validators.required],
      onethousand: ['', Validators.required],
      total: ['', Validators.required]
    });
  }

  back() {
    this.router.navigate(['/main']);
  }

  onChange() {

    this.ten = 0;
    this.twenty = 0;
    this.fifty = 0;
    this.onehundred = 0;
    this.twohundred = 0;
    this.fivehundred = 0;
    this.onethousand = 0;

    if (this.myform.controls['ten'].value != '') this.ten = 10 * this.myform.controls['ten'].value
    if (this.myform.controls['twenty'].value != '') this.twenty = 20 * this.myform.controls['twenty'].value
    if (this.myform.controls['fifty'].value != '') this.fifty = 50 * this.myform.controls['fifty'].value
    if (this.myform.controls['onehundred'].value != '') this.onehundred = 100 * this.myform.controls['onehundred'].value
    if (this.myform.controls['twohundred'].value != '') this.twohundred = 200 * this.myform.controls['twohundred'].value
    if (this.myform.controls['fivehundred'].value != '') this.fivehundred = 500 * this.myform.controls['fivehundred'].value
    if (this.myform.controls['onethousand'].value != '') this.onethousand = 1000 * this.myform.controls['onethousand'].value

    this.total = this.ten + this.twenty + this.fifty + this.onehundred + this.twohundred + this.fivehundred + this.onethousand;
  }
}
