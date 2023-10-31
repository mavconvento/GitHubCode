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
import { Location } from '../../models/location';
import { Entry } from '../../models/entry';
import { Result } from '../../models/result';
import { AlertService } from '../../services/alert.service';
import { AuthenticationService } from '../../services/authentication.service';
import { RaceService } from '../../services/race.service';
import { HostListener } from "@angular/core";
import { Helpers } from '../../helpers/helpers';
import { UserService } from '../../services/user.service';
import { TrainingResult } from 'src/app/models/trainingresult';
//import { conditionallyCreateMapObjectLiteral } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-race-topigeon',
  templateUrl: './race-topigeon.component.html',
  styleUrls: ['./race-topigeon.component.css']
})
export class RaceTopigeonComponent implements OnInit, AfterViewInit {
  form: FormGroup;
  clubList: Array<Club>;
  clubListOriginal: Array<Club>;
  raceDetails: any;
  raceResultCollection: TrainingResult[];
  //raceTrainingCollection: TrainingResult[];
  raceLocationCollection: Location[];
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

  //checkbox label
  isSubscribedToEmailsMessage: string = "Custom Release Point"

  //race details
  LocationName: string;
  ReleaseTime: string;
  MinSpeed: string;
  TotalBird: string;
  SMSCount: string;
  TotalBirdEntry: string = "0";
  ReleaseLat: string = "";
  ReleaseLong: string = "";
  StopTimeFrom: string = "";
  StopTimeTo: string = "";
  IsTimeStop: Boolean = false;

  //race result data source
  dataSource: MatTableDataSource<Result>;
  displayedColumns: string[] = ["Rank", "RingNumber", "Distance", "Backtime", "Flight", "Speed"];
  displayedColumnsMobile: string[] = ["Rank", "Details"];
  displayedColumnsMobileSummary: string[] = ["RankMobile", "RingNumberMobile", "BacktimeMobile", "SpeedMobile"];


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
    //private route: ActivatedRoute,
    //private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private raceService: RaceService,
    //private helper: Helpers,
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
      EclockId: ['', Validators.required],
      LiberationDate: ['', Validators.required],
      Liberation: ['', Validators.required],
      DateRelease: ['', Validators.required],
      FilterName: [''],
      ClubFullName: [''],
      ClubName: [''],
      Category: [''],
      Group: [''],
      MobileNumber: ['', Validators.required],
      UserID: ['', Validators.required],
      mobileView: ["2", Validators.required],
      ReleaseTime: ['', Validators.required],
      CustomTraining: [false, Validators.required],
      LatDeg: [''],
      LatMin: [''],
      LatSec: [''],
      LatSign: [''],
      LongDeg: [''],
      LongMin: [''],
      LongSec: [''],
      LongSign: ['']
    });

    this.GetMobileList();

    this.form.controls["LiberationDate"].setValue(new Date())
    this.form.get("Liberation").valueChanges.subscribe(x => this.getLocation(x));
    this.form.get("LiberationDate").valueChanges.subscribe(x => this.GetTopigeonTraining());

    if (localStorage.getItem('selectedClub')) {
      this.form.controls["ClubFullName"].setValue(localStorage.getItem('selectedClub'))
    }

    this.GetTopigeonTraining();
  }

  getLocation(event) {

    this.form.controls["LatDeg"].setValue("");
    this.form.controls["LatMin"].setValue("");
    this.form.controls["LatSec"].setValue("");
    this.form.controls["LatSign"].setValue("");
    this.form.controls["LongDeg"].setValue("");
    this.form.controls["LongMin"].setValue("");
    this.form.controls["LongSec"].setValue("");
    this.form.controls["LongSign"].setValue("");

    if (event != "") {
      if (this.raceLocationCollection != null) {
        var loc = this.raceLocationCollection.filter(x => x.LocationName == event);

        if (loc.length > 0) {
          var coor = loc[0].Coordinates.split(" ", 20)
          this.form.controls["LatDeg"].setValue(coor[0]);
          this.form.controls["LatMin"].setValue(coor[1]);
          this.form.controls["LatSec"].setValue(coor[2]);
          this.form.controls["LatSign"].setValue(coor[3]);
          this.form.controls["LongDeg"].setValue(coor[5]);
          this.form.controls["LongMin"].setValue(coor[6]);
          this.form.controls["LongSec"].setValue(coor[7]);
          this.form.controls["LongSign"].setValue(coor[8]);
        }
      }
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


    //console.log(option.value);
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
    this.GetTopigeonTraining();
  }

  SaveTraining() {

    this.loading = true;

    if (this.form.controls["ClubFullName"].value != "") {
      var clubIndex = this.clubList.filter(x => x.name == this.form.controls["ClubFullName"].value)
      this.form.controls["ClubName"].setValue(clubIndex[0].clubabbreviation);

      if (this.previousClub != clubIndex[0].clubabbreviation) {
        this.previousClub = clubIndex[0].clubabbreviation;
      }
    }
    var release = new Date();
    release = this.form.controls["LiberationDate"].value;
    let formatRelease = release.getFullYear().toString() + "-" + (release.getMonth() + 1).toString() + "-" + release.getDate().toString()
    this.form.controls["DateRelease"].setValue(formatRelease);

    //this.GetRaceDetails();
    this.TopigeonTrainingSave();

    //set last select club
    localStorage.setItem('selectedClub', this.form.controls["ClubFullName"].value);
  }

  ViewMaps(item) {
    //console.log(item);
    //window.open('https://www.google.com/maps/place/' + item.Latitude + ' ' + item.Longtitude, '_blank');
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

  TopigeonTrainingSave() {

    this.form.controls["EclockId"].setValue(localStorage.getItem("eclockID"));
    var userlog = JSON.parse(localStorage.getItem("currentUser"));
    //console.log(userlog.userID);
    this.form.controls["UserID"].setValue(userlog.userID);
    this.raceService.topigeonTrainingSave(this.form.value).subscribe(data => {
      var result = JSON.parse(data.content);
      //console.log(result)

      this.GetTopigeonTraining();
      this.loading = false;

    }, error => {
      this.alertService.errorNotification(error);
      this.loading = false;
    });
  }

  GetTopigeonTraining() {
    var release = new Date();
    release = this.form.controls["LiberationDate"].value;
    let formatRelease = release.getFullYear().toString() + "-" + (release.getMonth() + 1).toString() + "-" + release.getDate().toString()
    this.form.controls["DateRelease"].setValue(formatRelease);

    var userlog = JSON.parse(localStorage.getItem("currentUser"));
    this.form.controls["UserID"].setValue(userlog.userID);
    this.form.controls["EclockId"].setValue(localStorage.getItem("eclockID"));

    this.raceService.getTopigeonTraining(this.form.value).subscribe(data => {
      var result = JSON.parse(data.content);
      //console.log(result)
      if (result.Table.length > 0) {
        this.form.controls["Liberation"].setValue(result.Table[0].Liberation)
        this.form.controls["ReleaseTime"].setValue(result.Table[0].ReleaseTime)
        this.form.controls["LatDeg"].setValue(result.Table[0].LatDegree)
        this.form.controls["LatMin"].setValue(result.Table[0].LatMin)
        this.form.controls["LatSec"].setValue(result.Table[0].LatSec)
        this.form.controls["LatSign"].setValue(result.Table[0].LatSign)
        this.form.controls["LongDeg"].setValue(result.Table[0].LongDegree)
        this.form.controls["LongMin"].setValue(result.Table[0].LongMin)
        this.form.controls["LongSec"].setValue(result.Table[0].LongSec)
        this.form.controls["LongSign"].setValue(result.Table[0].LongSign)
      }
      else {
        this.form.controls["ClubFullName"].setValue("");
        this.form.controls["Liberation"].setValue("");
        this.form.controls["ReleaseTime"].setValue("");
        this.form.controls["LatDeg"].setValue("");
        this.form.controls["LatMin"].setValue("");
        this.form.controls["LatSec"].setValue("");
        this.form.controls["LatSign"].setValue("");
        this.form.controls["LongDeg"].setValue("");
        this.form.controls["LongMin"].setValue("");
        this.form.controls["LongSec"].setValue("");
        this.form.controls["LongSign"].setValue("");
      }
    }, error => {
      this.alertService.errorNotification(error);
      this.loading = false;
    });

    this.GetResult();
  }

  GetResult() {
    this.raceService.getTrainingResult(this.form.value).subscribe(data => {
      this.raceResultCollection = JSON.parse(data.content);

      //console.table(this.raceResultCollection);
      this.length = this.raceResultCollection.length;
      this.dataSource.data = this.raceResultCollection;
      this.loading = false;

    }, error => {
      this.alertService.errorNotification(error);
      this.loading = false;
    });
  }

  GetLocation() {
    var clubIndex = this.clubList.filter(x => x.name == this.form.controls["ClubFullName"].value)
    //this.form.controls["DbName"].setValue(clubIndex[0].dbName);
    this.form.controls["ClubName"].setValue(clubIndex[0].clubabbreviation);
    this.form.controls["ClubID"].setValue(clubIndex[0].clubId.toString());

    this.raceService.getLocation(this.form.value).subscribe(data => {
      this.raceLocationCollection = JSON.parse(data.content);
      //console.log(this.raceLocationCollection);
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

      //console.log(details.Table);

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

        if (this.raceDetails[0].StopTime != "") {
          this.StopTimeFrom = this.raceDetails[0].StopFrom;
          this.StopTimeTo = this.raceDetails[0].StopTo;
          this.IsTimeStop = true;
        }

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
        this.StopTimeTo = "";
        this.StopTimeFrom = "";
        this.IsTimeStop = false;
      }

    },
      error => {
        this.alertService.errorNotification(error);
      });
  }

  seachClub(event) {
    var clubCollection = JSON.parse(localStorage.getItem("clubs"));

    var clubSearch = clubCollection.filter(x => x.ClubName.indexOf(event.value) > -1);

    var cl = new Array<Club>();

    clubSearch.forEach(item => {
      var c = new Club;

      c.clubId = item.ClubID;
      c.clubabbreviation = item.clubabbreviation;
      c.dbName = item.dbName;
      c.name = item.ClubName;
      cl.push(c);
    });

    this.GetLocation();
    this.form.controls["Liberation"].setValue(" ");
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
