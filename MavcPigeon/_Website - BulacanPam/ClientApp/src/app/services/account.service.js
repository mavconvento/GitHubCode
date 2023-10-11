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
exports.AccountService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var AccountService = /** @class */ (function () {
    function AccountService(http, baseUrl) {
        this.http = http;
        this._baseUrl = baseUrl;
    }
    AccountService.prototype.getAll = function () {
        return this.http.get(this._baseUrl + 'user');
    };
    AccountService.prototype.setAsPrimary = function (mobileNumber, userID) {
        var getURL = this._baseUrl + 'api/account/SetAsPrimary';
        return this.http.post(getURL, { MobileNumber: mobileNumber, userID: userID });
    };
    AccountService.prototype.loadMavcCard = function (formData) {
        var getURL = this._baseUrl + 'api/account/LoadMavcCard';
        return this.http.post(getURL, formData);
    };
    AccountService.prototype.pasaload = function (formData) {
        var getURL = this._baseUrl + 'api/account/Pasaload';
        return this.http.post(getURL, formData);
    };
    AccountService.prototype.Unreg = function (mobileNumber, userID, clubName, dbName) {
        var getURL = this._baseUrl + 'api/account/Unreg';
        return this.http.post(getURL, { mobileNumber: mobileNumber, userID: userID, clubName: clubName, dbName: dbName });
    };
    AccountService.prototype.getLinkMobileByEmail = function (email) {
        var getURL = this._baseUrl + 'api/account/LoadMavcCard';
    };
    AccountService.prototype.forgotPassword = function (mobile) {
        var getURL = this._baseUrl + 'api/account/LoadMavcCard';
    };
    AccountService = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], AccountService);
    return AccountService;
}());
exports.AccountService = AccountService;
//# sourceMappingURL=account.service.js.map