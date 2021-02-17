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
exports.LinkNumberDialogComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var forms_1 = require("@angular/forms");
var material_1 = require("@angular/material");
var alert_service_1 = require("../../../services/alert.service");
var authentication_service_1 = require("../../../services/authentication.service");
var user_service_1 = require("../../../services/user.service");
var LinkNumberDialogComponent = /** @class */ (function () {
    function LinkNumberDialogComponent(fb, userService, alertService, authenticationService, dialogRef, data) {
        this.fb = fb;
        this.userService = userService;
        this.alertService = alertService;
        this.authenticationService = authenticationService;
        this.dialogRef = dialogRef;
        this.IsResend = false;
        this.title = data.title;
    }
    LinkNumberDialogComponent.prototype.ngOnInit = function () {
        this.isOtp = false;
        this.form = this.fb.group({
            mobileNumber: ['', []],
            otpCode: ['', []]
        });
    };
    LinkNumberDialogComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    Object.defineProperty(LinkNumberDialogComponent.prototype, "f", {
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    LinkNumberDialogComponent.prototype.Resend = function () {
        this.IsResend = true;
        this.linkMobileNumber("GetOTP");
    };
    LinkNumberDialogComponent.prototype.linkMobileNumber = function (action) {
        var _this = this;
        var currentUser = this.authenticationService.currentUserValue;
        this.form.addControl('userid', new forms_1.FormControl(currentUser.userID, forms_1.Validators.required));
        this.form.addControl('action', new forms_1.FormControl('', forms_1.Validators.required));
        this.form.controls["action"].setValue(action);
        this.userService.linkMobileNumber(this.form.value).subscribe(function (result) {
            if (result.content == "Success") {
                _this.form.controls["otpCode"].setValue("12345");
                _this.isOtp = true;
                _this.IsResend = false;
            }
            else if (result.content == "LinkMobileNumber") {
                _this.alertService.successNotification("Mobile Number successfully link.");
                _this.close();
            }
            else {
                _this.alertService.errorNotification(result.content);
            }
            ;
        }, function (error) {
            _this.alertService.errorNotification(error);
            _this.IsResend = false;
        });
    };
    LinkNumberDialogComponent = __decorate([
        core_2.Component({
            selector: 'app-link-number',
            templateUrl: './link-number.component.html',
            styleUrls: ['./link-number.component.css']
        }),
        __param(5, core_1.Inject(material_1.MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            user_service_1.UserService,
            alert_service_1.AlertService,
            authentication_service_1.AuthenticationService,
            material_1.MatDialogRef, Object])
    ], LinkNumberDialogComponent);
    return LinkNumberDialogComponent;
}());
exports.LinkNumberDialogComponent = LinkNumberDialogComponent;
//# sourceMappingURL=link-number.component.js.map