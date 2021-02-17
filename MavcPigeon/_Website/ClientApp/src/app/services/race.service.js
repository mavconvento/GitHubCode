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
exports.RaceService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var RaceService = /** @class */ (function () {
    function RaceService(http, baseUrl) {
        this.http = http;
        this._baseUrl = baseUrl;
    }
    RaceService.prototype.getRaceResult = function (formData) {
        var getURL = this._baseUrl + 'api/race/getRaceResult';
        return this.http.post(getURL, formData);
    };
    RaceService.prototype.getRaceDetails = function (formData) {
        var getURL = this._baseUrl + 'api/race/getRaceDetails';
        return this.http.post(getURL, formData);
    };
    RaceService.prototype.getRaceEntry = function (formData) {
        var getURL = this._baseUrl + 'api/race/getRaceEntry';
        return this.http.post(getURL, formData);
    };
    RaceService.prototype.getRaceCategory = function (dbName, clubName) {
        return this.http.get(this._baseUrl + 'api/race/getRaceCategory?dbName=' + dbName + "&clubName=" + clubName);
    };
    RaceService.prototype.getRaceGroup = function (dbName, clubName) {
        return this.http.get(this._baseUrl + 'api/race/getRaceGroup?dbName=' + dbName + "&clubName=" + clubName);
    };
    RaceService.prototype.getBalance = function (mobilenumber) {
        return this.http.get(this._baseUrl + 'api/race/getBalance?mobileNumber=' + mobilenumber);
    };
    RaceService.prototype.onlineClocking = function (formData) {
        var getURL = this._baseUrl + 'api/race/onlineClocking';
        return this.http.post(getURL, formData);
    };
    RaceService = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], RaceService);
    return RaceService;
}());
exports.RaceService = RaceService;
//# sourceMappingURL=race.service.js.map