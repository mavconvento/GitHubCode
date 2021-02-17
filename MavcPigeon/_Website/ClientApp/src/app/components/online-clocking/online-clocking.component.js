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
exports.OnlineClockingComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var helpers_1 = require("../../helpers/helpers");
var alert_service_1 = require("../../services/alert.service");
var authentication_service_1 = require("../../services/authentication.service");
var race_service_1 = require("../../services/race.service");
var user_service_1 = require("../../services/user.service");
var OnlineClockingComponent = /** @class */ (function () {
    function OnlineClockingComponent(fb, route, router, authenticationService, alertService, raceService, userService, helper) {
        this.fb = fb;
        this.route = route;
        this.router = router;
        this.authenticationService = authenticationService;
        this.alertService = alertService;
        this.raceService = raceService;
        this.userService = userService;
        this.helper = helper;
        this.noLinkMobileMessage = false;
        this.keywordList = ["FORECAST", "CUT-OFF", "STATUS"];
    }
    OnlineClockingComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            ClubName: ['', forms_1.Validators.required],
            Message: [''],
            Keyword: [''],
            ReplyMessage: ['', forms_1.Validators.required],
            MobileNumber: ['', forms_1.Validators.required],
            TotalLoad: ['', forms_1.Validators.required],
            DbName: ['', forms_1.Validators.required],
            UserID: ['', forms_1.Validators.required]
        });
        if (!localStorage.getItem("primary")) {
            this.noLinkMobileMessage = true;
        }
        if (!localStorage.getItem("clubs")) {
            this.GetMobileList();
        }
        else {
            //console.log(localStorage.getItem("clubs"));
            var club = JSON.parse(localStorage.getItem("clubs"));
            this.clubList = club.filter(function (x) { return x.MemberClubID != ''; });
            if (localStorage.getItem('onlineClockingClub')) {
                this.form.controls["ClubName"].setValue(localStorage.getItem('onlineClockingClub'));
            }
            else if (this.clubList.length == 1) {
                this.form.controls["ClubName"].setValue(this.clubList[0].clubabbreviation);
            }
            this.getMobileNumber();
            this.getLoadBalance();
        }
    };
    OnlineClockingComponent.prototype.getMobileNumber = function () {
        var mobile = localStorage.getItem("primary");
        this.form.controls["MobileNumber"].setValue(mobile);
    };
    OnlineClockingComponent.prototype.getLoadBalance = function () {
        var _this = this;
        this.raceService.getBalance(this.form.controls["MobileNumber"].value).subscribe(function (data) {
            var result = JSON.parse(data.content);
            if (result[0]) {
                var balance = Number(result[0].LoadBalance).toLocaleString();
                _this.form.controls["TotalLoad"].setValue(balance);
            }
            else
                _this.form.controls["TotalLoad"].setValue("0.00");
        });
    };
    Object.defineProperty(OnlineClockingComponent.prototype, "f", {
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    OnlineClockingComponent.prototype.onlineClocking = function () {
        var keyword = this.form.controls["Message"].value;
        var clubname = this.form.controls["ClubName"].value;
        this.form.controls["DbName"].setValue("");
        //this.form.controls["ClubName"].setValue("");
        this.form.controls["Keyword"].setValue("");
        //set last selected club
        localStorage.setItem('onlineClockingClub', clubname);
        if (clubname != "" && keyword != "") {
            var club = this.clubList.filter(function (fil) { return fil.clubabbreviation.toUpperCase() == clubname.toUpperCase(); });
            this.form.controls["Keyword"].setValue(keyword);
            if (club.length > 0)
                this.form.controls["DbName"].setValue(club[0].dbName);
            this.submitOnlineClocking(keyword);
            this.form.controls["Message"].setValue("");
        }
        else if (clubname == "") {
            this.alertService.errorNotification("Club name is not indicated.");
        }
        else
            this.alertService.errorNotification("Invalid Format.");
    };
    OnlineClockingComponent.prototype.clearKeys = function (event) {
        //console.log(event);
        if (event != "") {
            this.keywordList = [];
        }
        else
            this.keywordList = ["FORECAST", "CUT-OFF", "STATUS"];
    };
    OnlineClockingComponent.prototype.submitOnlineClocking = function (keyword) {
        var _this = this;
        this.loading = true;
        var key = keyword.split(" ");
        this.raceService.onlineClocking(this.form.value).subscribe(function (data) {
            var result = JSON.parse(data.content);
            //console.log(result.Table[0]);
            if (result.Table[0].IsValid) {
                _this.alertService.simpleNotification(result.Table[0].Result);
                if (key[0].toUpperCase() == "REG") {
                    _this.GetMobileList();
                    _this.clubList = JSON.parse(localStorage.getItem("clubs"));
                }
            }
            else {
                _this.alertService.errorNotification(result.Table[0].Result);
            }
            _this.loading = false;
            _this.getLoadBalance();
        }, function (error) {
            _this.alertService.errorNotification(error);
            _this.loading = false;
        });
        this.keywordList = ["FORECAST", "CUT-OFF", "STATUS"];
    };
    OnlineClockingComponent.prototype.GetMobileList = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.userService.getLinkMobileList(currentUser.userID).subscribe(function (result) {
            var data = JSON.parse(result.content);
            localStorage.setItem("clubs", JSON.stringify(data.Table));
            localStorage.setItem("mobile", JSON.stringify(data.Table1));
            var club = JSON.parse(localStorage.getItem("clubs"));
            _this.clubList = club.filter(function (x) { return x.MemberClubID != ''; });
            _this.mobileList = JSON.parse(localStorage.getItem("mobile"));
            var primary = _this.mobileList.filter(function (x) { return x.IsMain == true; });
            if (primary.length > 0) {
                localStorage.setItem("primary", primary[0].MobileNumber);
            }
            _this.getMobileNumber();
            _this.getLoadBalance();
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    OnlineClockingComponent = __decorate([
        core_1.Component({
            selector: 'app-online-clocking',
            templateUrl: './online-clocking.component.html',
            styleUrls: ['./online-clocking.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            router_1.ActivatedRoute,
            router_1.Router,
            authentication_service_1.AuthenticationService,
            alert_service_1.AlertService,
            race_service_1.RaceService,
            user_service_1.UserService,
            helpers_1.Helpers])
    ], OnlineClockingComponent);
    return OnlineClockingComponent;
}());
exports.OnlineClockingComponent = OnlineClockingComponent;
//# sourceMappingURL=online-clocking.component.js.map