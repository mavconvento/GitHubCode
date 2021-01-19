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
exports.ImageService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var helpers_1 = require("../helpers/helpers");
var ImageService = /** @class */ (function () {
    function ImageService(http, helper, baseUrl) {
        this.http = http;
        this._baseUrl = baseUrl;
    }
    ImageService.prototype.getImage = function (id) {
        return this.http.get(this._baseUrl + 'api/uploadFile/getImage/' + id);
    };
    ImageService = __decorate([
        core_1.Injectable({ providedIn: 'root' }),
        __param(2, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, helpers_1.Helpers, String])
    ], ImageService);
    return ImageService;
}());
exports.ImageService = ImageService;
//# sourceMappingURL=image.service.js.map