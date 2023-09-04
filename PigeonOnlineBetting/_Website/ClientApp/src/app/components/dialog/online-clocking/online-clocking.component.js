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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.OnlineClockingDialogComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var forms_1 = require("@angular/forms");
var material_1 = require("@angular/material");
var alert_service_1 = require("../../../services/alert.service");
var authentication_service_1 = require("../../../services/authentication.service");
var account_service_1 = require("../../../services/account.service");
var OnlineClockingDialogComponent = /** @class */ (function () {
    function OnlineClockingDialogComponent(fb, accountService, alertService, authenticationService, dialogRef, data) {
        this.fb = fb;
        this.accountService = accountService;
        this.alertService = alertService;
        this.authenticationService = authenticationService;
        this.dialogRef = dialogRef;
        this.title = data.title;
        this.isLoadCard = data.isLoadCard;
        this.isPasaload = data.isPasaload;
        this.pasaloadBalance = data.pasaloadBalance;
        this.mobilenumber = data.mobilenumber;
        this.clubName = data.clubName;
        this.dbName = data.dbName;
    }
    OnlineClockingDialogComponent.prototype.ngOnInit = function () {
        var currentUser = this.authenticationService.currentUserValue;
        this.form = this.fb.group({
            mobileNumberLoadReceiver: ['', []],
            pinNumber: ['', []],
            amount: ['', []],
            userid: ['', []],
            Keyword: ['', []],
            mobilenumber: ['', []],
            clubName: ['', []],
            dbName: ['', []]
        });
    };
    Object.defineProperty(OnlineClockingDialogComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    OnlineClockingDialogComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    OnlineClockingDialogComponent.prototype.submit = function () {
        var _this = this;
        this.form.controls["userid"].setValue(this.userID);
        this.form.controls["mobilenumber"].setValue(this.mobilenumber);
        this.form.controls["clubName"].setValue(this.clubName);
        this.form.controls["dbName"].setValue(this.dbName);
        if (this.isLoadCard) {
            if (this.f["pinNumber"].value != "") {
                this.form.controls["Keyword"].setValue("Load " + this.f["pinNumber"].value);
                this.accountService.loadMavcCard(this.form.value).subscribe(function (result) {
                    var data = JSON.parse(result.content);
                    _this.alertService.simpleNotification(data.Table[0].Result);
                    _this.close();
                }, function (error) { _this.alertService.errorNotification(error); });
            }
        }
        else if (this.isPasaload) {
            if (this.f["amount"].value != "" && this.f["mobileNumberLoadReceiver"].value) {
                if (Number(this.f["amount"].value) > 0) {
                    this.accountService.pasaload(this.form.value).subscribe(function (result) {
                        var data = JSON.parse(result.content);
                        _this.alertService.simpleNotification(data.Table[0].Result);
                        _this.close();
                    }, function (error) { _this.alertService.errorNotification(error); });
                }
            }
            else
                this.alertService.errorNotification("Enter mobile number and pasaload amount.");
        }
    };
    OnlineClockingDialogComponent = __decorate([
        core_2.Component({
            selector: 'app-online-clocking',
            templateUrl: './online-clocking.component.html',
            styleUrls: ['./online-clocking.component.css']
        }),
        __param(5, core_1.Inject(material_1.MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            account_service_1.AccountService,
            alert_service_1.AlertService,
            authentication_service_1.AuthenticationService,
            material_1.MatDialogRef, Object])
    ], OnlineClockingDialogComponent);
    return OnlineClockingDialogComponent;
}());
exports.OnlineClockingDialogComponent = OnlineClockingDialogComponent;
//# sourceMappingURL=online-clocking.component.js.map