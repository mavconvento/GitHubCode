"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.RaceResultComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var paginator_1 = require("@angular/material/paginator");
var sort_1 = require("@angular/material/sort");
var table_1 = require("@angular/material/table");
var router_1 = require("@angular/router");
var clubname_1 = require("../../models/clubname");
var alert_service_1 = require("../../services/alert.service");
var authentication_service_1 = require("../../services/authentication.service");
var race_service_1 = require("../../services/race.service");
var core_2 = require("@angular/core");
var helpers_1 = require("../../helpers/helpers");
var user_service_1 = require("../../services/user.service");
//import { conditionallyCreateMapObjectLiteral } from '@angular/compiler/src/render3/view/util';
var RaceResultComponent = /** @class */ (function () {
    function RaceResultComponent(fb, route, router, authenticationService, alertService, raceService, helper, userService) {
        this.fb = fb;
        this.route = route;
        this.router = router;
        this.authenticationService = authenticationService;
        this.alertService = alertService;
        this.raceService = raceService;
        this.helper = helper;
        this.userService = userService;
        this.previousClub = "";
        this.loading = false;
        this.isMobile = false;
        this.isMobileDetails = false;
        this.isMobileSummary = true;
        this.TotalBirdEntry = "0";
        this.ReleaseLat = "";
        this.ReleaseLong = "";
        this.displayedColumns = ["Rank", "MemberName", "Location", "TotalEntry", "RingNumber", "Distance", "Arrival", "Flight", "Speed", "Remarks"];
        this.displayedColumnsMobile = ["Rank", "Details"];
        this.displayedColumnsMobileSummary = ["RankMobile", "MemberDetails", "LocationMobile", "RingNumberMobile", "SpeedMobile", "SourceMobile"];
        this.length = 0;
        this.pageIndex = 0;
        this.pageSize = 50;
        this.pageSizeOptions = [25, 50, 100, 200];
        this.displayedColumns_entry = ["RingNumber"];
        this.length_entry = 0;
        this.pageIndex_entry = 0;
        this.pageSize_entry = 10;
        this.pageSizeOptions_entry = [10, 20];
        this.dataSource = new table_1.MatTableDataSource(this.raceResultCollection);
        this.dataSource_entry = new table_1.MatTableDataSource(this.raceEntryCollection);
        this.getScreenSize();
    }
    Object.defineProperty(RaceResultComponent.prototype, "sort", {
        set: function (sort) {
            this.dataSource.sort = sort;
        },
        enumerable: false,
        configurable: true
    });
    ;
    RaceResultComponent.prototype.getScreenSize = function (event) {
        this.scrHeight = window.innerHeight;
        this.scrWidth = window.innerWidth;
        if (this.scrWidth < 1008) {
            this.isMobile = true;
        }
        else {
            this.isMobile = false;
        }
    };
    RaceResultComponent.prototype.ngAfterViewInit = function () {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource_entry.paginator = this.paginator_entry;
    };
    RaceResultComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            ClubID: ['', forms_1.Validators.required],
            LiberationDate: ['', forms_1.Validators.required],
            DateRelease: ['', forms_1.Validators.required],
            FilterName: [''],
            ClubFullName: ['', forms_1.Validators.required],
            ClubName: ['', forms_1.Validators.required],
            Category: [''],
            Group: [''],
            MobileNumber: ['', forms_1.Validators.required],
            DbName: ['', forms_1.Validators.required],
            UserID: ['', forms_1.Validators.required],
            mobileView: ["2", forms_1.Validators.required],
        });
        if (!localStorage.getItem("clubs")) {
            this.GetMobileList();
        }
        else
            this.SetClubCollection();
        this.form.controls["LiberationDate"].setValue(new Date());
        if (localStorage.getItem('selectedClub')) {
            this.form.controls["ClubFullName"].setValue(localStorage.getItem('selectedClub'));
        }
    };
    RaceResultComponent.prototype.changeView = function (option) {
        if (option.value == "1") {
            this.isMobileDetails = true;
            this.isMobileSummary = false;
        }
        else if (option.value == "2") {
            this.isMobileDetails = false;
            this.isMobileSummary = true;
        }
        console.log(option.value);
    };
    RaceResultComponent.prototype.GetMobileList = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.userService.getLinkMobileList(currentUser.userID).subscribe(function (result) {
            var data = JSON.parse(result.content);
            localStorage.setItem("clubs", JSON.stringify(data.Table));
            localStorage.setItem("mobile", JSON.stringify(data.Table1));
            _this.clubList = JSON.parse(localStorage.getItem("clubs"));
            var mobileList = JSON.parse(localStorage.getItem("mobile"));
            var primary = mobileList.filter(function (x) { return x.IsMain == true; });
            if (primary.length > 0) {
                localStorage.setItem("primary", primary[0].MobileNumber);
            }
            _this.SetClubCollection();
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    RaceResultComponent.prototype.getNext = function (event) {
        this.length = event.length;
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;
    };
    RaceResultComponent.prototype.getNext_entry = function (event) {
        this.length_entry = event.length;
        this.pageIndex_entry = event.pageIndex;
        this.pageSize_entry = event.pageSize;
    };
    RaceResultComponent.prototype.ViewResult = function () {
        var _this = this;
        var clubIndex = this.clubList.filter(function (x) { return x.name == _this.form.controls["ClubFullName"].value; });
        this.form.controls["DbName"].setValue(clubIndex[0].dbName);
        this.form.controls["ClubName"].setValue(clubIndex[0].clubabbreviation);
        var release = new Date();
        release = this.form.controls["LiberationDate"].value;
        var formatRelease = release.getFullYear().toString() + "-" + (release.getMonth() + 1).toString() + "-" + release.getDate().toString();
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
    };
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
    RaceResultComponent.prototype.ViewMaps = function (item) {
        console.log(item);
        window.open('https://www.google.com/maps/place/' + item.Latitude + ' ' + item.Longtitude, '_blank');
    };
    RaceResultComponent.prototype.ViewReleaseMaps = function () {
        if (this.ReleaseLat != "" && this.ReleaseLong != "") {
            window.open('https://www.google.com/maps/place/' + this.ReleaseLat + ' ' + this.ReleaseLong, '_blank');
        }
    };
    RaceResultComponent.prototype.GetCategory = function (dbName, clubName) {
        var _this = this;
        this.raceService.getRaceCategory(dbName, clubName).subscribe(function (data) {
            _this.categoryCollection = JSON.parse(data.content);
        });
    };
    RaceResultComponent.prototype.GetGRoup = function (dbName, clubName) {
        var _this = this;
        this.raceService.getRaceGroup(dbName, clubName).subscribe(function (data) {
            _this.groupCollection = JSON.parse(data.content);
        });
    };
    RaceResultComponent.prototype.GetResult = function () {
        var _this = this;
        this.raceService.getRaceResult(this.form.value).subscribe(function (data) {
            _this.raceResultCollection = JSON.parse(data.content);
            //console.log(this.raceResultCollection);
            _this.length = _this.raceResultCollection.length;
            _this.dataSource.data = _this.raceResultCollection;
            _this.loading = false;
        }, function (error) {
            _this.alertService.errorNotification(error);
            _this.loading = false;
        });
    };
    RaceResultComponent.prototype.GetRaceDetails = function () {
        var _this = this;
        var user = this.authenticationService.currentUserValue;
        this.form.controls["UserID"].setValue(user.userID.toString());
        this.raceService.getRaceDetails(this.form.value).subscribe(function (data) {
            var details = JSON.parse(data.content);
            //race details
            _this.raceDetails = details.Table;
            //race entry
            _this.raceEntryCollection = details.Table1;
            //console.log(details.Table1);
            _this.length_entry = _this.raceEntryCollection.length;
            _this.dataSource_entry.data = _this.raceEntryCollection;
            if (_this.raceDetails[0]) {
                _this.LocationName = _this.raceDetails[0].LocationName;
                _this.ReleaseTime = _this.raceDetails[0].ReleaseTime;
                _this.MinSpeed = _this.raceDetails[0].MinSpeed;
                _this.TotalBird = _this.raceDetails[0].TotalBird;
                _this.SMSCount = _this.raceDetails[0].SMSCount;
                _this.TotalBirdEntry = _this.raceDetails[0].BirdEntryCount;
                _this.ReleaseLat = _this.raceDetails[0].Latitude;
                _this.ReleaseLong = _this.raceDetails[0].Longtitude;
            }
            else {
                _this.LocationName = "";
                _this.ReleaseTime = "";
                _this.MinSpeed = "";
                _this.TotalBird = "";
                _this.SMSCount = "";
                _this.TotalBirdEntry = "0";
                _this.ReleaseLat = "";
                _this.ReleaseLong = "";
            }
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    RaceResultComponent.prototype.seachClub = function (event) {
        var clubCollection = JSON.parse(localStorage.getItem("clubs"));
        var clubSearch = clubCollection.filter(function (x) { return x.ClubName.toUpperCase().indexOf(event.toUpperCase()) > -1; });
        var cl = new Array();
        clubSearch.forEach(function (item) {
            var c = new clubname_1.Club;
            c.clubId = item.ClubID;
            c.clubabbreviation = item.clubabbreviation;
            c.dbName = item.dbName;
            c.name = item.ClubName;
            cl.push(c);
        });
        //console.log(event.toUpper);
        //console.log(clubSearch);
        this.clubList = cl;
    };
    RaceResultComponent.prototype.SetClubCollection = function () {
        var clubCollection = JSON.parse(localStorage.getItem("clubs"));
        var cl = new Array();
        clubCollection.forEach(function (item) {
            var c = new clubname_1.Club;
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
    };
    Object.defineProperty(RaceResultComponent.prototype, "f", {
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    RaceResultComponent.prototype.displayValue = function (val) {
        return val ? val.name : null;
    };
    __decorate([
        core_1.ViewChild('paginator', { static: false }),
        __metadata("design:type", paginator_1.MatPaginator)
    ], RaceResultComponent.prototype, "paginator", void 0);
    __decorate([
        core_1.ViewChild(sort_1.MatSort, { static: false }),
        __metadata("design:type", sort_1.MatSort),
        __metadata("design:paramtypes", [sort_1.MatSort])
    ], RaceResultComponent.prototype, "sort", null);
    __decorate([
        core_1.ViewChild('paginator_entry', { static: false }),
        __metadata("design:type", paginator_1.MatPaginator)
    ], RaceResultComponent.prototype, "paginator_entry", void 0);
    __decorate([
        core_2.HostListener('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], RaceResultComponent.prototype, "getScreenSize", null);
    RaceResultComponent = __decorate([
        core_1.Component({
            selector: 'app-race-result',
            templateUrl: './race-result.component.html',
            styleUrls: ['./race-result.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            router_1.ActivatedRoute,
            router_1.Router,
            authentication_service_1.AuthenticationService,
            alert_service_1.AlertService,
            race_service_1.RaceService,
            helpers_1.Helpers,
            user_service_1.UserService])
    ], RaceResultComponent);
    return RaceResultComponent;
}());
exports.RaceResultComponent = RaceResultComponent;
//# sourceMappingURL=race-result.component.js.map