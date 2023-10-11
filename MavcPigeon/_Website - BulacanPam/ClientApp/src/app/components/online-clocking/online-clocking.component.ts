import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Helpers } from '../../helpers/helpers';
import { Club } from '../../models/clubname';
import { AlertService } from '../../services/alert.service';
import { AuthenticationService } from '../../services/authentication.service';
import { RaceService } from '../../services/race.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-online-clocking',
  templateUrl: './online-clocking.component.html',
  styleUrls: ['./online-clocking.component.css']
})
export class OnlineClockingComponent implements OnInit {
  form: FormGroup;
  clubList: Array<Club>;
  loading: boolean; false;
  mobileList: any;
  noLinkMobileMessage: boolean = false;
  keywordList: string[] = ["FORECAST", "CUT-OFF", "STATUS"]
  isPha: Boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private raceService: RaceService,
    private userService: UserService,
    private helper: Helpers

  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      ClubName: ['', Validators.required],
      Message: [''],
      Keyword: [''],
      ReplyMessage: ['', Validators.required],
      MobileNumber: ['', Validators.required],
      TotalLoad: ['', Validators.required],
      DbName: ['', Validators.required],
      UserID: ['', Validators.required]
    });

    if (!localStorage.getItem("primary")) {
      this.noLinkMobileMessage = true;
    }

    if (localStorage.getItem("userlogin") == "pha.mavcpigeon@gmail.com") {
      this.isPha = true;
    }

    this.GetMobileList();

    //if (!localStorage.getItem("clubs")) {
    //  this.GetMobileList();
    //}
    //else {
    //  //console.log(localStorage.getItem("clubs"));
    //  var club = JSON.parse(localStorage.getItem("clubs"));
    //  this.clubList = club.filter(x => x.MemberClubID != '')

    //  if (localStorage.getItem('onlineClockingClub')) {
    //    this.form.controls["ClubName"].setValue(localStorage.getItem('onlineClockingClub'));
    //  }
    //  else if (this.clubList.length == 1) {
    //    this.form.controls["ClubName"].setValue(this.clubList[0].clubabbreviation);
    //  }

    //  this.getMobileNumber();
    //  this.getLoadBalance();
    //}
  }

  getMobileNumber() {
    let mobile = localStorage.getItem("primary");
    this.form.controls["MobileNumber"].setValue(mobile);
  }

  getLoadBalance() {
    this.raceService.getBalance(this.form.controls["MobileNumber"].value).subscribe(data => {
      let result = JSON.parse(data.content);

      if (result[0]) {
        let balance = Number(result[0].LoadBalance).toLocaleString();
        this.form.controls["TotalLoad"].setValue(balance);
      }
      else
        this.form.controls["TotalLoad"].setValue("0.00");
    });
  }

  get f() { return this.form.controls; }

  seachClub(event) {
      var clubCollection = JSON.parse(localStorage.getItem("clubs"));
      var clubSearch = clubCollection.filter(x => x.ClubName.toUpperCase().indexOf(event.toUpperCase()) > -1 && x.MemberClubID != '');

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

      //if (this.clubList.length == 1) {
      //  this.form.controls["ClubName"].setValue(this.clubList[0].clubabbreviation);
      //}
  }


  onlineClocking(action:string) {
    let keyword = this.form.controls["Message"].value;
    let clubname: string = this.form.controls["ClubName"].value.toUpperCase();

    this.form.controls["DbName"].setValue("");
    //this.form.controls["ClubName"].setValue("");
    this.form.controls["Keyword"].setValue("");


    if (action == "forecast") keyword = "FORECAST";
    else if (action == "cutoff") keyword = "CUT-OFF";

    //set last selected club
    localStorage.setItem('onlineClockingClub', clubname);

    if (clubname != "" && keyword != "") {
      var club = this.clubList.filter(fil => fil.clubabbreviation.toUpperCase() == clubname.toUpperCase())
      this.form.controls["Keyword"].setValue(keyword);
      if (club.length > 0) this.form.controls["DbName"].setValue(club[0].dbName);
      this.submitOnlineClocking(keyword);
      this.form.controls["Message"].setValue("");
    }
    else
      if (clubname == "") {
        this.alertService.errorNotification("Club name is not indicated.")
      }
      else
        this.alertService.errorNotification("Invalid Format.")

  }


  clearKeys(event) {
    //console.log(event);
    if (event != "") {
      this.keywordList = [];
    }
    //else
    //  this.keywordList = ["FORECAST", "CUT-OFF", "STATUS"];
  }

  submitOnlineClocking(keyword: string) {
    this.loading = true;
    let key: string[] = keyword.split(" ");
    this.raceService.onlineClocking(this.form.value).subscribe(data => {
      var result = JSON.parse(data.content)
      //console.log(result.Table[0]);
      if (result.Table[0].IsValid) {
        this.alertService.simpleNotification(result.Table[0].Result)

        if (key[0].toUpperCase() == "REG") {
          this.GetMobileList();
          this.clubList = JSON.parse(localStorage.getItem("clubs"));
        }
      }
      else {
        this.alertService.errorNotification(result.Table[0].Result)
      }
      this.loading = false;

      this.getLoadBalance();
    }, error => {
      this.alertService.errorNotification(error);
      this.loading = false;
    });

    //this.keywordList = ["FORECAST", "CUT-OFF", "STATUS"];
  }

  GetMobileList() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userService.getLinkMobileList(currentUser.userID).subscribe(result => {
      var data = JSON.parse(result.content);
      localStorage.setItem("clubs", JSON.stringify(data.Table));
      localStorage.setItem("mobile", JSON.stringify(data.Table1));


      var club = JSON.parse(localStorage.getItem("clubs"));
      this.clubList = club.filter(x => x.MemberClubID != '')

      this.mobileList = JSON.parse(localStorage.getItem("mobile"));
      var primary = this.mobileList.filter(x => x.IsMain == true);

      if (primary.length > 0) {
        localStorage.setItem("primary", primary[0].MobileNumber);
      }

      this.getMobileNumber();
      this.getLoadBalance();
    },
      error => {
        this.alertService.errorNotification(error);
      });
  }

}
