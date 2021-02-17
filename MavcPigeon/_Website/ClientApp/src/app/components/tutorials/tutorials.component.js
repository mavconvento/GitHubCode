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
exports.TutorialsComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var TutorialsComponent = /** @class */ (function () {
    function TutorialsComponent() {
        this.width = 500;
        this.getScreenSize();
    }
    TutorialsComponent.prototype.ngOnInit = function () {
    };
    TutorialsComponent.prototype.getScreenSize = function (event) {
        this.scrHeight = window.innerHeight;
        this.scrWidth = window.innerWidth;
        //console.log(this.scrWidth);
        if (this.scrWidth < 565)
            this.width = 330;
        else if (this.scrWidth < 1008) {
            this.width = 500;
        }
        else {
            this.width = 500;
        }
    };
    __decorate([
        core_2.HostListener('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], TutorialsComponent.prototype, "getScreenSize", null);
    TutorialsComponent = __decorate([
        core_1.Component({
            selector: 'app-tutorials',
            templateUrl: './tutorials.component.html',
            styleUrls: ['./tutorials.component.css']
        }),
        __metadata("design:paramtypes", [])
    ], TutorialsComponent);
    return TutorialsComponent;
}());
exports.TutorialsComponent = TutorialsComponent;
//# sourceMappingURL=tutorials.component.js.map