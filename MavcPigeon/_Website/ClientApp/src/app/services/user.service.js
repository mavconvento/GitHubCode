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
exports.UserService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var UserService = /** @class */ (function () {
    function UserService(http, baseUrl) {
        this.http = http;
        this._baseUrl = baseUrl;
    }
    UserService.prototype.getAll = function () {
        return this.http.get(this._baseUrl + 'user');
    };
    UserService.prototype.getLinkMobileList = function (id) {
        return this.http.get(this._baseUrl + 'api/user/GetMobileLinkList/' + id);
    };
    UserService.prototype.register = function (user) {
        return this.http.post(this._baseUrl + 'api/user/register', user);
    };
    UserService.prototype.updateProfile = function (formData) {
        var getURL = this._baseUrl + 'api/user/UpdateProfile';
        return this.http.post(getURL, formData);
    };
    UserService.prototype.linkMobileNumber = function (formData) {
        var getURL = this._baseUrl + 'api/user/LinkMobileNumber';
        return this.http.post(getURL, formData);
    };
    UserService.prototype.delete = function (id) {
        return this.http.delete(this._baseUrl + 'user');
    };
    UserService.prototype.getLinkMobileByEmail = function (email) {
        var getURL = this._baseUrl + 'api/user/GetMobileByEmail?email=' + email;
        return this.http.get(getURL);
    };
    UserService.prototype.forgotPassword = function (email, mobile) {
        var getURL = this._baseUrl + 'api/user/SendPassword?email=' + email + '&mobile=' + mobile;
        return this.http.get(getURL);
    };
    UserService.prototype.getVideo = function (type) {
        var getURL = this._baseUrl + 'api/user/getVideo?type=' + type;
        return this.http.get(getURL);
    };
    UserService.prototype.getMemberCoordinates = function (memberidno, clubname, dbname) {
        var getURL = this._baseUrl + 'api/user/getMemberCoordinates?memberidno=' + memberidno + '&clubname=' + clubname + '&dbname=' + dbname;
        return this.http.get(getURL);
    };
    UserService = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], UserService);
    return UserService;
}());
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map