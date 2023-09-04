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
exports.MemberLogsComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var forms_1 = require("@angular/forms");
var material_1 = require("@angular/material");
var clubname_1 = require("../../models/clubname");
var alert_service_1 = require("../../services/alert.service");
var authentication_service_1 = require("../../services/authentication.service");
var user_service_1 = require("../../services/user.service");
var MemberLogsComponent = /** @class */ (function () {
    function MemberLogsComponent(fb, userService, authenticationService, alertService) {
        this.fb = fb;
        this.userService = userService;
        this.authenticationService = authenticationService;
        this.alertService = alertService;
        this.loading = false;
        this.length = 0;
        this.pageIndex = 0;
        this.pageSize = 50;
        this.pageSizeOptions = [25, 50, 100, 200];
        this.displayedColumns = ["Reciever", "Details", "ReplyMessage"];
        this.dataSource = new material_1.MatTableDataSource(this.logsCollection);
    }
    Object.defineProperty(MemberLogsComponent.prototype, "sort", {
        set: function (sort) {
            this.dataSource.sort = sort;
        },
        enumerable: false,
        configurable: true
    });
    ;
    MemberLogsComponent.prototype.ngAfterViewInit = function () {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    };
    MemberLogsComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            ClubID: ['', forms_1.Validators.required],
            From: ['', forms_1.Validators.required],
            DateFrom: ['', forms_1.Validators.required],
            To: ['', forms_1.Validators.required],
            DateTo: ['', forms_1.Validators.required],
            Keyword: [''],
            ClubFullName: ['', forms_1.Validators.required],
            ClubName: ['', forms_1.Validators.required],
            MobileNumber: [''],
            DbName: ['', forms_1.Validators.required],
        });
        this.GetMobileList();
        this.form.controls["From"].setValue(new Date());
        this.form.controls["To"].setValue(new Date());
        //if (localStorage.getItem('selectedClub')) {
        //  this.form.controls["ClubFullName"].setValue(localStorage.getItem('selectedClub'))
        //  var mobile = this.mobileListOrig.filter(x => x.ClubAbbreviation == this.form.controls["ClubFullName"].value);
        //  this.form.controls["MobileNumber"].setValue("");
        //}
    };
    MemberLogsComponent.prototype.OptionChange = function (event) {
        this.form.controls["MobileNumber"].setValue("");
        if (event) {
            console.log(this.mobileList);
            this.mobileList = this.mobileListOrig.filter(function (x) { return x.ClubAbbreviation == event.clubabbreviation; });
            console.log(this.mobileList);
            if (this.mobileList.length == 1) {
                this.form.controls["MobileNumber"].setValue(this.mobileList[0].MobileNumber);
            }
        }
        else {
            this.mobileList = this.mobileListOrig.filter(function (x) { return x.ClubAbbreviation == ""; });
        }
    };
    MemberLogsComponent.prototype.seachClub = function (event) {
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
        this.clubList = cl;
        console.log(cl);
    };
    MemberLogsComponent.prototype.GetLogs = function () {
        var _this = this;
        var user = this.authenticationService.currentUserValue;
        //console.log(user.userName);
        var IsValid = this.mobileList.filter(function (x) { return x.MobileNumber == _this.form.controls["MobileNumber"].value; });
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
        var fromLogs = from.getFullYear().toString() + "-" + (from.getMonth() + 1).toString() + "-" + from.getDate().toString();
        var toLogs = to.getFullYear().toString() + "-" + (to.getMonth() + 1).toString() + "-" + to.getDate().toString();
        this.form.controls["DateFrom"].setValue(fromLogs);
        this.form.controls["DateTo"].setValue(toLogs);
        var club = this.clubList.filter(function (x) { return x.name == _this.form.controls["ClubFullName"].value; });
        this.form.controls["ClubID"].setValue(club[0].clubId.toString());
        this.form.controls["DbName"].setValue(club[0].dbName);
        if (user.userName == "mavconvento@gmail.com")
            this.form.controls["ClubID"].setValue("9000");
        this.userService.getLogs(this.form.value).subscribe(function (data) {
            var result = JSON.parse(data.content);
            //console.log(result);
            _this.logsCollection = result.Table;
            _this.dataSource.data = _this.logsCollection;
            _this.length = _this.logsCollection.length;
            _this.loading = false;
        }, function (error) { _this.alertService.errorNotification(error); _this.loading = false; });
    };
    MemberLogsComponent.prototype.GetMobileList = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.userService.getLinkMobileList(currentUser.userID).subscribe(function (result) {
            var data = JSON.parse(result.content);
            localStorage.setItem("clubs", JSON.stringify(data.Table));
            localStorage.setItem("mobile", JSON.stringify(data.Table1));
            _this.clubList = JSON.parse(localStorage.getItem("clubs"));
            var mobileCol = JSON.parse(localStorage.getItem("mobile"));
            _this.mobileList = mobileCol;
            _this.mobileListOrig = mobileCol;
            //console.log(this.mobileList);
            var primary = mobileCol.filter(function (x) { return x.IsMain == true; });
            if (primary.length > 0) {
                localStorage.setItem("primary", primary[0].MobileNumber);
                //this.form.controls["MobileNumber"].setValue(primary[0].MobileNumber);
            }
            _this.SetClubCollection();
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    MemberLogsComponent.prototype.getNext = function (event) {
        this.length = event.length;
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;
    };
    MemberLogsComponent.prototype.SetClubCollection = function () {
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
        //this.clubListOriginal = cl;
        if (this.clubList.length == 1) {
            this.form.controls["ClubFullName"].setValue(this.clubList[0].name);
        }
    };
    __decorate([
        core_1.ViewChild('paginator', { static: false }),
        __metadata("design:type", material_1.MatPaginator)
    ], MemberLogsComponent.prototype, "paginator", void 0);
    __decorate([
        core_1.ViewChild(material_1.MatSort, { static: false }),
        __metadata("design:type", material_1.MatSort),
        __metadata("design:paramtypes", [material_1.MatSort])
    ], MemberLogsComponent.prototype, "sort", null);
    MemberLogsComponent = __decorate([
        core_2.Component({
            selector: 'app-member-logs',
            templateUrl: './member-logs.component.html',
            styleUrls: ['./member-logs.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            user_service_1.UserService,
            authentication_service_1.AuthenticationService,
            alert_service_1.AlertService])
    ], MemberLogsComponent);
    return MemberLogsComponent;
}());
exports.MemberLogsComponent = MemberLogsComponent;
//# sourceMappingURL=member-logs.component.js.map