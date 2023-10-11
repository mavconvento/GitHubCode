"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.CompressImageService = void 0;
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
// in bytes, compress images larger than 1MB
var fileSizeMax = 1 * 512 * 512;
// in pixels, compress images have the width or height larger than 1024px
var widthHeightMax = 512;
var defaultWidthHeightRatio = 1;
var defaultQualityRatio = 0.5;
var CompressImageService = /** @class */ (function () {
    function CompressImageService() {
    }
    CompressImageService.prototype.compress = function (file) {
        var _this = this;
        var imageType = file.type || 'image/jpeg';
        var reader = new FileReader();
        reader.readAsDataURL(file);
        return rxjs_1.Observable.create(function (observer) {
            // This event is triggered each time the reading operation is successfully completed.
            reader.onload = function (ev) {
                // Create an html image element
                var img = _this.createImage(ev);
                // Choose the side (width or height) that longer than the other
                var imgWH = img.width > img.height ? img.width : img.height;
                // Determines the ratios to compress the image
                var withHeightRatio = (imgWH > widthHeightMax) ? widthHeightMax / imgWH : defaultWidthHeightRatio;
                var qualityRatio = (file.size > fileSizeMax) ? fileSizeMax / file.size : defaultQualityRatio;
                // Fires immediately after the browser loads the object
                img.onload = function () {
                    var elem = document.createElement('canvas');
                    // resize width, height
                    elem.width = img.width * withHeightRatio;
                    elem.height = img.height * withHeightRatio;
                    var ctx = elem.getContext('2d');
                    ctx.drawImage(img, 0, 0, elem.width, elem.height);
                    ctx.canvas.toBlob(
                    // callback, called when blob created
                    function (blob) {
                        observer.next(new File([blob], file.name, {
                            type: imageType,
                            lastModified: Date.now(),
                        }));
                    }, imageType, qualityRatio);
                };
            };
            // Catch errors when reading file
            reader.onerror = function (error) { return observer.error(error); };
        });
    };
    CompressImageService.prototype.createImage = function (ev) {
        var imageContent = ev.target.result;
        var img = new Image();
        img.src = imageContent;
        return img;
    };
    CompressImageService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], CompressImageService);
    return CompressImageService;
}());
exports.CompressImageService = CompressImageService;
//# sourceMappingURL=compress-image.service.js.map