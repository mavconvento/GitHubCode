import { Component, OnInit } from '@angular/core';
import { HostListener } from "@angular/core";

@Component({
  selector: 'app-tutorials',
  templateUrl: './tutorials.component.html',
  styleUrls: ['./tutorials.component.css']
})
export class TutorialsComponent implements OnInit {
  scrHeight: any;
  scrWidth: any;
  width: number = 500;

  constructor() {
    this.getScreenSize();
  }

  ngOnInit() {
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.scrHeight = window.innerHeight;
    this.scrWidth = window.innerWidth;

    //console.log(this.scrWidth);
    if (this.scrWidth < 565)
      this.width = 330;
    else if (this.scrWidth < 1008) {
      this.width = 500;
    }
    else {
      this.width = 500;
    }

  }

}
