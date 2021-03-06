"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
exports.TokenService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var operators_1 = require("rxjs/operators");
var config_1 = require("../config/config");
var base_service_1 = require("./base.service");
//import { Token } from '../models/token';
var helpers_1 = require("../helpers/helpers");
var TokenService = /** @class */ (function (_super) {
    __extends(TokenService, _super);
    function TokenService(http, config, helper) {
        var _this = _super.call(this, helper) || this;
        _this.http = http;
        _this.config = config;
        _this.pathAPI = _this.config.setting['PathAPI'];
        return _this;
    }
    TokenService.prototype.auth = function (data) {
        var body = JSON.stringify(data);
        return this.getToken(body);
    };
    TokenService.prototype.getToken = function (body) {
        return this.http.post(this.pathAPI + 'token', body, _super.prototype.header.call(this)).pipe(operators_1.catchError(_super.prototype.handleError));
    };
    TokenService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient, config_1.AppConfig, helpers_1.Helpers])
    ], TokenService);
    return TokenService;
}(base_service_1.BaseService));
exports.TokenService = TokenService;
//# sourceMappingURL=token.service.js.map