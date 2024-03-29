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
exports.MainMenuComponent = void 0;
var core_1 = require("@angular/core");
var authentication_service_1 = require("../../services/authentication.service");
var MainMenuComponent = /** @class */ (function () {
    function MainMenuComponent(authentication) {
        this.authentication = authentication;
        this.isExpanded = false;
        this.isPha = false;
        var local = localStorage.getItem("currentUser");
        if (local == null) {
            this.authentication.logout();
        }
    }
    ;
    MainMenuComponent.prototype.ngOnInit = function () {
        this.isLoggedIn$ = this.authentication.Islogin;
        this.user$ = this.authentication.currentUser;
    };
    ;
    MainMenuComponent.prototype.collapse = function () {
        this.isExpanded = false;
    };
    MainMenuComponent.prototype.toggle = function () {
        this.isExpanded = !this.isExpanded;
    };
    MainMenuComponent = __decorate([
        core_1.Component({
            selector: 'app-main-menu',
            templateUrl: './main-menu.component.html',
            styleUrls: ['./main-menu.component.css']
        }),
        __metadata("design:paramtypes", [authentication_service_1.AuthenticationService])
    ], MainMenuComponent);
    return MainMenuComponent;
}());
exports.MainMenuComponent = MainMenuComponent;
//# sourceMappingURL=main-menu.component.js.map