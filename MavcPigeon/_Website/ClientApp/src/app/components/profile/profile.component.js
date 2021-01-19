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
var image_service_1 = require("../../services/image.service");
var authentication_service_1 = require("../../services/authentication.service");
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(dialog, authenticationService, imageService) {
        this.dialog = dialog;
        this.authenticationService = authenticationService;
        this.imageService = imageService;
    }
    ProfileComponent.prototype.ngOnInit = function () {
        this.getMemberInfo();
    };
    ;
    ProfileComponent.prototype.onFileSelected = function (events) {
        //console.log(events)
    };
    ;
    ProfileComponent.prototype.getProfilePicture = function (fileUploadID) {
        var _this = this;
        this.imageService.getImage(fileUploadID).subscribe(function (result) {
            //console.log(result)
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
        //console.log(currentUser);
        this.getProfilePicture(currentUser.fileUploadID);
    };
    ProfileComponent.prototype.openDialog = function () {
        var _this = this;
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        dialogConfig.data = {
            id: 1,
            title: 'Profile Update'
        };
        var dialogRef = this.dialog.open(profile_details_component_1.ProfileDetailsComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            _this.getMemberInfo();
            //console.log("close");
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
            image_service_1.ImageService])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map