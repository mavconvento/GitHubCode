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
exports.Helpers = void 0;
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var alert_service_1 = require("../services/alert.service");
var authentication_service_1 = require("../services/authentication.service");
var race_service_1 = require("../services/race.service");
var user_service_1 = require("../services/user.service");
var Helpers = /** @class */ (function () {
    function Helpers(authenticationService, alertService, raceService, userService) {
        this.authenticationService = authenticationService;
        this.alertService = alertService;
        this.raceService = raceService;
        this.userService = userService;
        this.authenticationChanged = new rxjs_1.Subject();
    }
    Helpers.prototype.isAuthenticated = function () {
        return (!(window.localStorage['token'] === undefined ||
            window.localStorage['token'] === null ||
            window.localStorage['token'] === 'null' ||
            window.localStorage['token'] === 'undefined' ||
            window.localStorage['token'] === ''));
    };
    Helpers.prototype.isAuthenticationChanged = function () {
        return this.authenticationChanged.asObservable();
    };
    Helpers.prototype.getToken = function () {
        if (window.localStorage['token'] === undefined ||
            window.localStorage['token'] === null ||
            window.localStorage['token'] === 'null' ||
            window.localStorage['token'] === 'undefined' ||
            window.localStorage['token'] === '') {
            return '';
        }
        var obj = JSON.parse(window.localStorage['token']);
        return obj.token;
    };
    Helpers.prototype.setToken = function (data) {
        this.setStorageToken(JSON.stringify(data));
    };
    Helpers.prototype.failToken = function () {
        this.setStorageToken(undefined);
    };
    Helpers.prototype.logout = function () {
        this.setStorageToken(undefined);
    };
    Helpers.prototype.setStorageToken = function (value) {
        window.localStorage['token'] = value;
        this.authenticationChanged.next(this.isAuthenticated());
    };
    Helpers = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __metadata("design:paramtypes", [authentication_service_1.AuthenticationService,
            alert_service_1.AlertService,
            race_service_1.RaceService,
            user_service_1.UserService])
    ], Helpers);
    return Helpers;
}());
exports.Helpers = Helpers;
//# sourceMappingURL=helpers.js.map