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
exports.AuthenticationService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var core_2 = require("@angular/core");
var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(http, baseUrl) {
        this.http = http;
        this.isUserLoginSubject = new rxjs_1.BehaviorSubject(false);
        this.currentUserSubject = new rxjs_1.BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));
        if (this.currentUserSubject) {
            this.isUserLoginSubject.next(true);
        }
        this.currentUser = this.currentUserSubject.asObservable();
        this._baseUrl = baseUrl;
    }
    Object.defineProperty(AuthenticationService.prototype, "currentUserValue", {
        get: function () {
            return this.currentUserSubject.value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AuthenticationService.prototype, "Islogin", {
        get: function () {
            return this.isUserLoginSubject.asObservable();
        },
        enumerable: false,
        configurable: true
    });
    ;
    AuthenticationService.prototype.login = function (username, password) {
        var _this = this;
        return this.http.post(this._baseUrl + 'api/user/authenticate', { username: username, password: password })
            .pipe(operators_1.map(function (user) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            _this.currentUserSubject.next(user);
            _this.isUserLoginSubject.next(true);
            return user;
        }));
    };
    AuthenticationService.prototype.logout = function () {
        // remove user from local storage and set current user to null
        localStorage.removeItem('currentUser');
        localStorage.removeItem('profileImageID');
        localStorage.removeItem('primary');
        localStorage.removeItem('clubs');
        localStorage.removeItem('mobile');
        localStorage.removeItem('onlineClockingClub');
        localStorage.removeItem('selectedClub');
        this.isUserLoginSubject.next(false);
        this.currentUserSubject.next(null);
    };
    AuthenticationService = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __param(1, core_2.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], AuthenticationService);
    return AuthenticationService;
}());
exports.AuthenticationService = AuthenticationService;
//# sourceMappingURL=authentication.service.js.map