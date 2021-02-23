import { JsonPipe } from '@angular/common';
import { CommentStmt } from '@angular/compiler';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Club } from '../../models/clubname';
import { Entry } from '../../models/entry';
import { Result } from '../../models/result';
import { AlertService } from '../../services/alert.service';
import { AuthenticationService } from '../../services/authentication.service';
import { RaceService } from '../../services/race.service';
import { HostListener } from "@angular/core";
import { Helpers } from '../../helpers/helpers';
import { UserService } from '../../services/user.service';
//import { conditionallyCreateMapObjectLiteral } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-race-result',
  templateUrl: './race-result.component.html',
  styleUrls: ['./race-result.component.css']
})
export class RaceResultComponent implements OnInit, AfterViewInit {
  form: FormGroup;
  clubList: Array<Club>;
  clubListOriginal: Array<Club>;
  raceDetails: any;
  raceResultCollection: Result[];
  raceEntryCollection: Entry[];
  categoryCollection: any;
  groupCollection: any;
  previousClub: string = "";
  loading: boolean = false;
  isMobile: boolean = false;
  isMobileDetails: boolean = false;
  isMobileSummary: boolean = true;
  scrHeight: any;
  scrWidth: any;


  //race details
  LocationName: string;
  ReleaseTime: string;
  MinSpeed: string;
  TotalBird: string;
  SMSCount: string;
  TotalBirdEntry: string = "0";
  ReleaseLat: string = "";
  ReleaseLong: string = "";


  //race result data source
  dataSource: MatTableDataSource<Result>;
  displayedColumns: string[] = ["Rank", "MemberName", "Location", "TotalEntry", "RingNumber" , "Distance", "Arrival", "Flight", "Speed", "Remarks"];
  displayedColumnsMobile: string[] = ["Rank", "Details"];
  displayedColumnsMobileSummary: string[] = ["RankMobile", "MemberDetails", "LocationMobile", "RingNumberMobile","SpeedMobile","SourceMobile"];


  //@ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild('paginator', { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) set sort(sort: MatSort) {
    this.dataSource.sort = sort;
  };

  length: number = 0;
  pageIndex: number = 0;
  pageSize: number = 50;
  pageSizeOptions: number[] = [25, 50, 100, 200];


  //race entry datasource
  dataSource_entry: MatTableDataSource<Entry>;
  displayedColumns_entry: string[] = ["RingNumber"];
  //@ViewChild(MatPaginator, { static: false }) paginator_entry: MatPaginator;
  @ViewChild('paginator_entry', { static: false }) paginator_entry: MatPaginator;

  length_entry: number = 0;
  pageIndex_entry: number = 0;
  pageSize_entry: number = 10;
  pageSizeOptions_entry: number[] = [10, 20];


  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private raceService: RaceService,
    private helper: Helpers,
    private userService: UserService
  ) {

    this.dataSource = new MatTableDataSource(this.raceResultCollection);
    this.dataSource_entry = new MatTableDataSource(this.raceEntryCollection);
    this.getScreenSize();
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.scrHeight = window.innerHeight;
    this.scrWidth = window.innerWidth;

    if (this.scrWidth < 1008) {
     this.isMobile = true;
    }
    else {
      this.isMobile = false;
    }

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSource_entry.paginator = this.paginator_entry;
  }


  ngOnInit() {
    this.form = this.fb.group({
      ClubID: ['', Validators.required],
      LiberationDate: ['', Validators.required],
      DateRelease: ['', Validators.required],
      FilterName: [''],
      ClubFullName: ['', Validators.required],
      ClubName: ['', Validators.required],
      Category: [''],
      Group: [''],
      MobileNumber: ['', Validators.required],
      DbName: ['', Validators.required],
      UserID: ['', Validators.required],
      mobileView: ["2", Validators.required],
    });

    if (!localStorage.getItem("clubs")) {
      this.GetMobileList();
    }
    else
      this.SetClubCollection();

    this.form.controls["LiberationDate"].setValue(new Date())

    if (localStorage.getItem('selectedClub')) {
      this.form.controls["ClubFullName"].setValue(localStorage.getItem('selectedClub'))
    }
  }

  changeView(option) {
    if (option.value == "1") {
      this.isMobileDetails = true;
      this.isMobileSummary = false;
    }

    else if (option.value == "2") {
      this.isMobileDetails = false;
      this.isMobileSummary = true;
    }
     

    console.log(option.value);
  }

  GetMobileList() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userService.getLinkMobileList(currentUser.userID).subscribe(result => {
      var data = JSON.parse(result.content);
      localStorage.setItem("clubs", JSON.stringify(data.Table));
      localStorage.setItem("mobile", JSON.stringify(data.Table1));

      this.clubList = JSON.parse(localStorage.getItem("clubs"));
      let mobileList = JSON.parse(localStorage.getItem("mobile"));
      var primary = mobileList.filter(x => x.IsMain == true);

      if (primary.length > 0) {
        localStorage.setItem("primary", primary[0].MobileNumber);
      }

      this.SetClubCollection();
    },
      error => {
        this.alertService.errorNotification(error);
      });
  }

  getNext(event: PageEvent) {
    this.length = event.length;
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  getNext_entry(event: PageEvent) {
    this.length_entry = event.length;
    this.pageIndex_entry = event.pageIndex;
    this.pageSize_entry = event.pageSize;
  }

  ViewResult() {
    var clubIndex = this.clubList.filter(x => x.name == this.form.controls["ClubFullName"].value)
    this.form.controls["DbName"].setValue(clubIndex[0].dbName);
    this.form.controls["ClubName"].setValue(clubIndex[0].clubabbreviation);

    var release = new Date();
    release = this.form.controls["LiberationDate"].value;
    let formatRelease = release.getFullYear().toString() + "-" + (release.getMonth() + 1).toString() + "-" + release.getDate().toString()
    this.form.controls["DateRelease"].setValue(formatRelease);

    this.loading = true;
    if (this.previousClub != clubIndex[0].clubabbreviation) {
      this.GetCategory(clubIndex[0].dbName, clubIndex[0].clubabbreviation);
      this.GetGRoup(clubIndex[0].dbName, clubIndex[0].clubabbreviation);
      this.previousClub = clubIndex[0].clubabbreviation;
    }

    this.GetRaceDetails();
    this.GetResult();

    //set last select club
    localStorage.setItem('selectedClub', this.form.controls["ClubFullName"].value);
  }

  //GetRaceEntry() {
  //  let user = this.authenticationService.currentUserValue;
  //  this.form.controls["UserID"].setValue(user.userID.toString());

  //  this.raceService.getRaceEntry(this.form.value).subscribe(data => {
  //    this.raceEntryCollection = JSON.parse(data.content);
  //    this.length_entry = this.raceEntryCollection.length;
  //    this.dataSource_entry.data = this.raceEntryCollection;
  //    console.log(data.content);
  //  }, error => {
  //    this.alertService.errorNotification(error);
  //  })
  //}

  ViewMaps(item) {
    console.log(item);
    window.open('https://www.google.com/maps/place/' + item.Latitude + ' ' + item.Longtitude, '_blank');
  }

  ViewReleaseMaps() {
    if (this.ReleaseLat != "" && this.ReleaseLong != "") {
      window.open('https://www.google.com/maps/place/' + this.ReleaseLat + ' ' + this.ReleaseLong, '_blank');
    }
  }

  GetCategory(dbName: string, clubName: string) {
    this.raceService.getRaceCategory(dbName, clubName).subscribe(data => {
      this.categoryCollection = JSON.parse(data.content);
    });
  }

  GetGRoup(dbName: string, clubName: string) {
    this.raceService.getRaceGroup(dbName, clubName).subscribe(data => {
      this.groupCollection = JSON.parse(data.content);
    });
  }

  GetResult() {
    this.raceService.getRaceResult(this.form.value).subscribe(data => {
      this.raceResultCollection = JSON.parse(data.content);
      //console.log(this.raceResultCollection);
      this.length = this.raceResultCollection.length;
      this.dataSource.data = this.raceResultCollection;
      this.loading = false;

    }, error => {
      this.alertService.errorNotification(error);
      this.loading = false;
    });
  }

  GetRaceDetails() {
    let user = this.authenticationService.currentUserValue;
    this.form.controls["UserID"].setValue(user.userID.toString());

    this.raceService.getRaceDetails(this.form.value).subscribe(data => {
      var details = JSON.parse(data.content);
      //race details
      this.raceDetails = details.Table;

      //race entry
      this.raceEntryCollection = details.Table1;

      //console.log(details.Table1);
      
      this.length_entry = this.raceEntryCollection.length;
      this.dataSource_entry.data = this.raceEntryCollection;

      if (this.raceDetails[0]) {
        this.LocationName = this.raceDetails[0].LocationName;
        this.ReleaseTime = this.raceDetails[0].ReleaseTime;
        this.MinSpeed = this.raceDetails[0].MinSpeed;
        this.TotalBird = this.raceDetails[0].TotalBird;
        this.SMSCount = this.raceDetails[0].SMSCount;
        this.TotalBirdEntry = this.raceDetails[0].BirdEntryCount;
        this.ReleaseLat = this.raceDetails[0].Latitude;
        this.ReleaseLong = this.raceDetails[0].Longtitude;
      }
      else {
        this.LocationName = "";
        this.ReleaseTime = "";
        this.MinSpeed = "";
        this.TotalBird = "";
        this.SMSCount = "";
        this.TotalBirdEntry = "0";
        this.ReleaseLat = "";
        this.ReleaseLong = ""
      }

    },
      error => {
        this.alertService.errorNotification(error);
      });
  }

  seachClub(event) {
    var clubCollection = JSON.parse(localStorage.getItem("clubs"));

    var clubSearch = clubCollection.filter(x => x.ClubName.toUpperCase().indexOf(event.toUpperCase()) > -1);
    
    var cl = new Array<Club>();

    clubSearch.forEach(item => {
      var c = new Club;

      c.clubId = item.ClubID;
      c.clubabbreviation = item.clubabbreviation;
      c.dbName = item.dbName;
      c.name = item.ClubName;
      cl.push(c);
    });

    //console.log(event.toUpper);
    //console.log(clubSearch);
    this.clubList = cl;
  }

  SetClubCollection() {
    var clubCollection = JSON.parse(localStorage.getItem("clubs"));
    var cl = new Array<Club>();

    clubCollection.forEach(item => {
      var c = new Club;

      c.clubId = item.ClubID;
      c.clubabbreviation = item.clubabbreviation;
      c.dbName = item.dbName;
      c.name = item.ClubName;
      cl.push(c);
    });

    this.clubList = cl;
    this.clubListOriginal = cl;

    if (this.clubList.length == 1) {
      this.form.controls["ClubFullName"].setValue(this.clubList[0].name);
    }
  }

  get f() { return this.form.controls; }

  displayValue(val: any) {
    return val ? val.name : null;
  }
}
