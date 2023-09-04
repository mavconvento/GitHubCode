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
exports.ConfirmdialogComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var material_1 = require("@angular/material");
var alert_service_1 = require("../../../services/alert.service");
var authentication_service_1 = require("../../../services/authentication.service");
var user_service_1 = require("../../../services/user.service");
var ConfirmdialogComponent = /** @class */ (function () {
    function ConfirmdialogComponent(userService, alertService, authenticationService, dialogRef, data) {
        this.userService = userService;
        this.alertService = alertService;
        this.authenticationService = authenticationService;
        this.dialogRef = dialogRef;
        this.title = data.title;
        this.message = data.message;
    }
    ConfirmdialogComponent.prototype.ngOnInit = function () {
    };
    ConfirmdialogComponent.prototype.close = function (confirm) {
        this.dialogRef.close(confirm);
    };
    ConfirmdialogComponent = __decorate([
        core_2.Component({
            selector: 'app-confirmdialog',
            templateUrl: './confirmdialog.component.html',
            styleUrls: ['./confirmdialog.component.css']
        }),
        __param(4, core_1.Inject(material_1.MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [user_service_1.UserService,
            alert_service_1.AlertService,
            authentication_service_1.AuthenticationService,
            material_1.MatDialogRef, Object])
    ], ConfirmdialogComponent);
    return ConfirmdialogComponent;
}());
exports.ConfirmdialogComponent = ConfirmdialogComponent;
//# sourceMappingURL=confirmdialog.component.js.map