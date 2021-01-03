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
exports.AppConfig = void 0;
var core_1 = require("@angular/core");
var AppConfig = /** @class */ (function () {
    function AppConfig() {
        this._config = {
            PathAPI: 'http://localhost:44391/api/'
        };
    }
    Object.defineProperty(AppConfig.prototype, "setting", {
        get: function () {
            return this._config;
        },
        enumerable: false,
        configurable: true
    });
    AppConfig.prototype.get = function (key) {
        return this._config[key];
    };
    AppConfig = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], AppConfig);
    return AppConfig;
}());
exports.AppConfig = AppConfig;
;
//# sourceMappingURL=config.js.map