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
exports.ForgotPasswordComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var material_1 = require("@angular/material");
var alert_service_1 = require("../../../services/alert.service");
var authentication_service_1 = require("../../../services/authentication.service");
var user_service_1 = require("../../../services/user.service");
var ForgotPasswordComponent = /** @class */ (function () {
    function ForgotPasswordComponent(fb, userService, alertService, authenticationService, dialogRef, data) {
        this.fb = fb;
        this.userService = userService;
        this.alertService = alertService;
        this.authenticationService = authenticationService;
        this.dialogRef = dialogRef;
        this.isMobileList = false;
        this.isSubmit = false;
        this.title = data.title;
    }
    ForgotPasswordComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            EmailAddress: ['', []],
            MobileNumber: ['', []]
        });
        this.isMobileList = true;
    };
    ForgotPasswordComponent.prototype.close = function () {
        this.dialogRef.close(confirm);
    };
    ForgotPasswordComponent.prototype.forgotPassword = function (action) {
        var _this = this;
        console.log(action);
        if (action == "getmobilelist") {
            this.userService.getLinkMobileByEmail(this.form.controls["EmailAddress"].value).subscribe(function (data) {
                var result = JSON.parse(data.content);
                _this.mobilelist = result.Table;
                console.log(result);
                _this.isMobileList = false;
                _this.isSubmit = true;
            });
        }
        else if (action == "submit") {
            this.userService.forgotPassword(this.form.controls["EmailAddress"].value, this.form.controls["MobileNumber"].value).subscribe(function (data) {
                console.log(data.content);
                if (data.content == "Success") {
                    _this.alertService.successNotification("Password has been send to your mobile number.");
                }
                else
                    _this.alertService.errorNotification(data.content);
                _this.close();
            }, function (error) { _this.alertService.errorNotification(error); });
        }
    };
    ForgotPasswordComponent = __decorate([
        core_1.Component({
            selector: 'app-forgot-password',
            templateUrl: './forgot-password.component.html',
            styleUrls: ['./forgot-password.component.css']
        }),
        __param(5, core_1.Inject(material_1.MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            user_service_1.UserService,
            alert_service_1.AlertService,
            authentication_service_1.AuthenticationService,
            material_1.MatDialogRef, Object])
    ], ForgotPasswordComponent);
    return ForgotPasswordComponent;
}());
exports.ForgotPasswordComponent = ForgotPasswordComponent;
//# sourceMappingURL=forgot-password.component.js.map