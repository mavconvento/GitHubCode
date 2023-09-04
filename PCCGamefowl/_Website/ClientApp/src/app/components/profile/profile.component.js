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
exports.ProfileComponent = void 0;
var core_1 = require("@angular/core");
var material_1 = require("@angular/material");
var profile_details_component_1 = require("../dialog/profile-details/profile-details.component");
var link_number_component_1 = require("../dialog/link-number/link-number.component");
var online_clocking_component_1 = require("../dialog/online-clocking/online-clocking.component");
var confirmdialog_component_1 = require("../dialog/confirmdialog/confirmdialog.component");
var image_service_1 = require("../../services/image.service");
var user_service_1 = require("../../services/user.service");
var account_service_1 = require("../../services/account.service");
var authentication_service_1 = require("../../services/authentication.service");
var alert_service_1 = require("../../services/alert.service");
var helpers_1 = require("../../helpers/helpers");
var router_1 = require("@angular/router");
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(dialog, authenticationService, imageService, userService, accountService, alertService, helper, router) {
        this.dialog = dialog;
        this.authenticationService = authenticationService;
        this.imageService = imageService;
        this.userService = userService;
        this.accountService = accountService;
        this.alertService = alertService;
        this.helper = helper;
        this.router = router;
        this.totalLoad = "0";
        this.picture = "7d033eb5-80ac-4ad1-88be-e39c33fe1f47";
        this.isPha = false;
    }
    ProfileComponent.prototype.ngOnInit = function () {
        var currentUser = this.authenticationService.currentUserValue;
        this.userID = currentUser.userID;
        if (localStorage.getItem("userlogin") == "pha.mavcpigeon@gmail.com") {
            this.isPha = true;
            console.log(this.isPha);
        }
        //set profileImageID
        if (currentUser.fileUploadID) {
            localStorage.setItem("profileImageID", currentUser.fileUploadID);
        }
        this.getMemberInfo();
        //this.GetMobileList();
    };
    ;
    ProfileComponent.prototype.onFileSelected = function (events) {
    };
    ;
    ProfileComponent.prototype.getProfilePicture = function (fileUploadID) {
        var _this = this;
        this.imageService.getImage(fileUploadID).subscribe(function (result) {
            console.log(result);
            _this.profileImage = 'data:' + result.fileType + ';base64,' + result.data;
        });
    };
    ;
    ProfileComponent.prototype.getMemberInfo = function () {
        var currentUser = this.authenticationService.currentUserValue;
        this.name = currentUser.firstName + ' ' + currentUser.lastName;
        this.loftName = currentUser.loftName;
        this.globelId = currentUser.globalId;
        this.emailAddress = currentUser.userName;
        if (localStorage.getItem("profileImageID")) {
            this.getProfilePicture(localStorage.getItem("profileImageID"));
        }
    };
    ProfileComponent.prototype.openProfileDialog = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        dialogConfig.data = {
            title: 'Profile Update',
            fileUploadID: currentUser.fileUploadID
        };
        var dialogRef = this.dialog.open(profile_details_component_1.ProfileDetailsDialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            _this.getMemberInfo();
        });
    };
    ProfileComponent.prototype.openOnlineClockingDialog = function (dialogType) {
        var _this = this;
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        var title = "Load Mavc Card";
        var isLoadCard = false;
        var isPasaload = false;
        var pasaloadBalance = 0;
        var mobilenumber = this.primaryMobileNumber;
        var clubName = this.clubName;
        var dbName = this.dbName;
        if (dialogType == "PASALOAD") {
            var allowPasaload = this.mobileList.filter(function (x) { return x.AllowPasaload == "1" && Number(x.LoadBalance) > 0 && x.MobileNumber == mobilenumber; });
            isPasaload = true;
            title = "Pasaload";
            if (allowPasaload.length > 0) {
                pasaloadBalance = allowPasaload[0].LoadBalance;
                mobilenumber = allowPasaload[0].MobileNumber;
            }
        }
        else if (dialogType == "LOADCARD") {
            isLoadCard = true;
            title = "Load Mavc Card";
        }
        dialogConfig.data = {
            title: title,
            isPasaload: isPasaload,
            isLoadCard: isLoadCard,
            pasaloadBalance: pasaloadBalance,
            mobilenumber: mobilenumber,
            clubName: clubName,
            dbName: dbName
        };
        var dialogRef = this.dialog.open(online_clocking_component_1.OnlineClockingDialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            _this.GetMobileList();
        });
    };
    ;
    ProfileComponent.prototype.openLinkMobileDialog = function () {
        var _this = this;
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        dialogConfig.data = {
            title: 'Link Mobile Number'
        };
        var dialogRef = this.dialog.open(link_number_component_1.LinkNumberDialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            _this.GetMobileList();
        });
    };
    ProfileComponent.prototype.SetAsPrimary = function (mobilenumber) {
        var _this = this;
        this.accountService.setAsPrimary(mobilenumber, this.userID).subscribe(function (x) {
            _this.alertService.simpleNotification("Mobile number : " + mobilenumber + " is now set as primary number.");
            _this.GetMobileList();
        }, function (error) { _this.alertService.errorNotification(error); });
    };
    ProfileComponent.prototype.Unreg = function (mobilenumber, club, db) {
        var _this = this;
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        dialogConfig.data = {
            title: 'Confirm Unregistration.',
            message: 'Are you sure you want to unreg this mobile number?'
        };
        var dialogRef = this.dialog.open(confirmdialog_component_1.ConfirmdialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            if (data) {
                _this.accountService.Unreg(mobilenumber, _this.userID, club, db).subscribe(function (result) {
                    var data = JSON.parse(result.content);
                    _this.alertService.simpleNotification(data.Table[0].Result);
                    _this.GetMobileList();
                }, function (error) { _this.alertService.errorNotification(error); });
            }
        });
    };
    ProfileComponent.prototype.getMemberCoordinates = function (item) {
        console.log(item);
        this.router.navigate(["/distance", item.clubabbreviation, item.MemberClubID, item.dbName]);
        //this.userService.getMemberCoordinates(item.MemberClubID, item.clubabbreviation, item.dbName).subscribe(data => {
        //  var result = JSON.parse(data.content);
        //  if (result.Table.length > 0) {
        //    window.open('https://www.google.com/maps/place/' + result.Table[0].Lat + ' ' + result.Table[0].Long, '_blank');
        //  }
        //  console.log((JSON.parse(data.content)).Table)
        //});
    };
    ProfileComponent.prototype.GetMobileList = function () {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.userService.getLinkMobileList(currentUser.userID).subscribe(function (result) {
            var data = JSON.parse(result.content);
            localStorage.setItem("clubs", JSON.stringify(data.Table));
            localStorage.setItem("mobile", JSON.stringify(data.Table1));
            var club = JSON.parse(localStorage.getItem("clubs"));
            _this.mobileList = JSON.parse(localStorage.getItem("mobile"));
            _this.clubList = club.filter(function (x) { return x.MemberClubID != ''; });
            var primary = _this.mobileList.filter(function (x) { return x.IsMain == true; });
            var eclock = _this.mobileList.filter(function (x) { return x.IsClock == "YES"; });
            var allowPasaload = _this.mobileList.filter(function (x) { return x.AllowPasaload == "1"; });
            if (primary.length > 0) {
                localStorage.setItem("primary", primary[0].MobileNumber);
                _this.primaryMobileNumber = primary[0].MobileNumber;
                _this.clubName = primary[0].ClubAbbreviation;
                _this.dbName = primary[0].dbName;
                _this.totalLoad = primary[0].LoadBalance;
            }
            if (eclock.length > 0) {
                _this.eclockMobileNumber = eclock[0].MobileNumber;
            }
            if (allowPasaload.length > 0) {
                _this.isAllowPasaload = true;
            }
        }, function (error) {
            _this.alertService.errorNotification(error);
        });
    };
    ProfileComponent = __decorate([
        core_1.Component({
            selector: 'app-profile',
            templateUrl: './profile.component.html',
            styleUrls: ['./profile.component.css']
        }),
        __metadata("design:paramtypes", [material_1.MatDialog,
            authentication_service_1.AuthenticationService,
            image_service_1.ImageService,
            user_service_1.UserService,
            account_service_1.AccountService,
            alert_service_1.AlertService,
            helpers_1.Helpers,
            router_1.Router])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map