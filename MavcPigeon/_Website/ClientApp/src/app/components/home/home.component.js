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
exports.HomeComponent = void 0;
var core_1 = require("@angular/core");
var image_service_1 = require("../../services/image.service");
var user_service_1 = require("../../services/user.service");
var account_service_1 = require("../../services/account.service");
var authentication_service_1 = require("../../services/authentication.service");
var alert_service_1 = require("../../services/alert.service");
var core_2 = require("@angular/core");
var helpers_1 = require("../../helpers/helpers");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(authenticationService, imageService, userService, accountService, alertService, helper) {
        this.authenticationService = authenticationService;
        this.imageService = imageService;
        this.userService = userService;
        this.accountService = accountService;
        this.alertService = alertService;
        this.helper = helper;
        this.width = 500;
        this.getScreenSize();
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.GetMobileList();
    };
    HomeComponent.prototype.getScreenSize = function (event) {
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
    };
    HomeComponent.prototype.GetMobileList = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.userService.getLinkMobileList(currentUser.userID).subscribe(function (result) {
            var data = JSON.parse(result.content);
            localStorage.setItem("clubs", JSON.stringify(data.Table));
            localStorage.setItem("mobile", JSON.stringify(data.Table1));
            var club = JSON.parse(localStorage.getItem("clubs"));
            var mobile = JSON.parse(localStorage.getItem("mobile"));
            var primary = mobile.filter(function (x) { return x.IsMain == true; });
            if (primary.length > 0) {
                localStorage.setItem("primary", primary[0].MobileNumber);
            }
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    __decorate([
        core_2.HostListener('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], HomeComponent.prototype, "getScreenSize", null);
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'app-home',
            templateUrl: './home.component.html',
        }),
        __metadata("design:paramtypes", [authentication_service_1.AuthenticationService,
            image_service_1.ImageService,
            user_service_1.UserService,
            account_service_1.AccountService,
            alert_service_1.AlertService,
            helpers_1.Helpers])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map