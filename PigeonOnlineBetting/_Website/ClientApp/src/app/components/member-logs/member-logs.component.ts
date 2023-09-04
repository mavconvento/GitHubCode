import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator, MatSort, MatTableDataSource, PageEvent } from '@angular/material';
import { Club } from '../../models/clubname';
import { Logs } from '../../models/logs';
import { AlertService } from '../../services/alert.service';
import { AuthenticationService } from '../../services/authentication.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-member-logs',
  templateUrl: './member-logs.component.html',
  styleUrls: ['./member-logs.component.css']
})
export class MemberLogsComponent implements OnInit {
  form: FormGroup;
  clubList: Array<Club>;
  loading: boolean = false;
  mobileList: any;
  mobileListOrig: any;
  logsCollection: Logs[];

  length: number = 0;
  pageIndex: number = 0;
  pageSize: number = 50;
  pageSizeOptions: number[] = [25, 50, 100, 200];

  //race result data source
  dataSource: MatTableDataSource<Logs>;
  displayedColumns: string[] = ["Reciever", "Details", "ReplyMessage"];

  //@ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild('paginator', { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) set sort(sort: MatSort) {
    this.dataSource.sort = sort;
  };

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) {

    this.dataSource = new MatTableDataSource(this.logsCollection);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit() {
    this.form = this.fb.group({
      ClubID: ['', Validators.required],
      From: ['', Validators.required],
      DateFrom: ['', Validators.required],
      To: ['', Validators.required],
      DateTo: ['', Validators.required],
      Keyword: [''],
      ClubFullName: ['', Validators.required],
      ClubName: ['', Validators.required],
      MobileNumber: [''],
      DbName: ['', Validators.required],
    });


    this.GetMobileList();

    this.form.controls["From"].setValue(new Date())
    this.form.controls["To"].setValue(new Date())

    //if (localStorage.getItem('selectedClub')) {
    //  this.form.controls["ClubFullName"].setValue(localStorage.getItem('selectedClub'))
    //  var mobile = this.mobileListOrig.filter(x => x.ClubAbbreviation == this.form.controls["ClubFullName"].value);
    //  this.form.controls["MobileNumber"].setValue("");
    //}
  }

  OptionChange(event) {
    this.form.controls["MobileNumber"].setValue("");
    if (event) {
      console.log(this.mobileList);
      this.mobileList = this.mobileListOrig.filter(x => x.ClubAbbreviation == event.clubabbreviation);
      console.log(this.mobileList);

      if (this.mobileList.length == 1) {
        this.form.controls["MobileNumber"].setValue(this.mobileList[0].MobileNumber);
      }
    }
    else {
      this.mobileList = this.mobileListOrig.filter(x => x.ClubAbbreviation == "");
    }

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

    this.clubList = cl;
    console.log(cl);
  }

  GetLogs() {

    var user = this.authenticationService.currentUserValue;
    //console.log(user.userName);

    var IsValid = this.mobileList.filter(x => x.MobileNumber == this.form.controls["MobileNumber"].value)

    if (user.userName != "mavconvento@gmail.com") {
      if (this.form.controls["ClubFullName"].value == "") {
        this.alertService.errorNotification("Select Club Name.");
        return;
      }
      else if (this.form.controls["MobileNumber"].value == "") {
        this.alertService.errorNotification("Select Mobile Number.");
        return;
      }
      else if (IsValid.length == 0) {
        this.alertService.errorNotification("Invalid Mobile Number.");
        return;
      }
    }

    this.loading = true;
    var from = new Date();
    var to = new Date();
    from = this.form.controls["From"].value;
    to = this.form.controls["To"].value;
    let fromLogs = from.getFullYear().toString() + "-" + (from.getMonth() + 1).toString() + "-" + from.getDate().toString()
    let toLogs = to.getFullYear().toString() + "-" + (to.getMonth() + 1).toString() + "-" + to.getDate().toString()
    this.form.controls["DateFrom"].setValue(fromLogs);
    this.form.controls["DateTo"].setValue(toLogs);

    var club = this.clubList.filter(x => x.name == this.form.controls["ClubFullName"].value)

    this.form.controls["ClubID"].setValue(club[0].clubId.toString());
    this.form.controls["DbName"].setValue(club[0].dbName);

    if (user.userName == "mavconvento@gmail.com") this.form.controls["ClubID"].setValue("9000");

    this.userService.getLogs(this.form.value).subscribe(data => {
      var result = JSON.parse(data.content);
      //console.log(result);
      this.logsCollection = result.Table;
      this.dataSource.data = this.logsCollection;
      this.length = this.logsCollection.length;
      this.loading = false;
    }, error => { this.alertService.errorNotification(error); this.loading = false; })
  }

  GetMobileList() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userService.getLinkMobileList(currentUser.userID).subscribe(result => {
      var data = JSON.parse(result.content);
      localStorage.setItem("clubs", JSON.stringify(data.Table));
      localStorage.setItem("mobile", JSON.stringify(data.Table1));

      this.clubList = JSON.parse(localStorage.getItem("clubs"));
      let mobileCol = JSON.parse(localStorage.getItem("mobile"));
      this.mobileList = mobileCol;
      this.mobileListOrig = mobileCol;
      //console.log(this.mobileList);
      var primary = mobileCol.filter(x => x.IsMain == true);

      if (primary.length > 0) {
        localStorage.setItem("primary", primary[0].MobileNumber);
        //this.form.controls["MobileNumber"].setValue(primary[0].MobileNumber);
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
    //this.clubListOriginal = cl;

    if (this.clubList.length == 1) {
      this.form.controls["ClubFullName"].setValue(this.clubList[0].name);
    }
  }


}
