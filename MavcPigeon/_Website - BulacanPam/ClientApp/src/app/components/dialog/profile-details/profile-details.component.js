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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProfileDetailsDialogComponent = void 0;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var forms_1 = require("@angular/forms");
var material_1 = require("@angular/material");
var guid_typescript_1 = require("guid-typescript");
var alert_service_1 = require("../../../services/alert.service");
var user_service_1 = require("../../../services/user.service");
var authentication_service_1 = require("../../../services/authentication.service");
var compress_image_service_1 = require("../../../services/compress-image.service");
var ProfileDetailsDialogComponent = /** @class */ (function () {
    function ProfileDetailsDialogComponent(fb, userService, alertService, authenticationService, dialogRef, compressImage, data) {
        this.fb = fb;
        this.userService = userService;
        this.alertService = alertService;
        this.authenticationService = authenticationService;
        this.dialogRef = dialogRef;
        this.compressImage = compressImage;
        this.IsSave = false;
        this.title = data.title;
        this.fileUploadID = data.fileUploadID;
        this.id = guid_typescript_1.Guid.create();
    }
    ProfileDetailsDialogComponent.prototype.ngOnInit = function () {
        var currentUser = this.authenticationService.currentUserValue;
        this.form = this.fb.group({
            loftName: [currentUser.loftName, []],
            firstname: [currentUser.firstName, []],
            lastName: [currentUser.lastName, []],
            photo: ['', []]
        });
    };
    ProfileDetailsDialogComponent.prototype.onFileSelected = function (events) {
        var _this = this;
        var image = events.target.files[0];
        console.log(image.size);
        this.compressImage.compress(image)
            .subscribe(function (compressedImage) {
            _this.fileToUpload = compressedImage;
            console.log(_this.fileToUpload);
            //use original image if convert images is large
            if (image.size < compressedImage.size) {
                _this.fileToUpload = image;
            }
        });
        //console.log(this.fileToUpload.size)
        this.preview(events.target.files);
    };
    ;
    Object.defineProperty(ProfileDetailsDialogComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    ProfileDetailsDialogComponent.prototype.save = function () {
        return __awaiter(this, void 0, void 0, function () {
            var formData, currentUser;
            var _this = this;
            return __generator(this, function (_a) {
                this.IsSave = true;
                formData = new FormData();
                currentUser = this.authenticationService.currentUserValue;
                formData.append('image', this.fileToUpload);
                formData.append('userID', currentUser.userID);
                formData.append('LoftName', this.f.loftName.value);
                formData.append('FirstName', this.f.firstname.value);
                formData.append('LastName', this.f.lastName.value);
                if (this.fileUploadID)
                    formData.append('fileUploadID', this.fileUploadID);
                this.userService.updateProfile(formData).subscribe(function (data) {
                    //console.log(JSON.parse(data.content));
                    var result = JSON.parse(data.content);
                    if (result[0].FileUploadId) {
                        localStorage.setItem("profileImageID", result[0].FileUploadId);
                        currentUser.fileUploadID = result[0].FileUploadId;
                    }
                    else
                        localStorage.removeItem("profileImageID");
                    currentUser.loftName = _this.f.loftName.value;
                    currentUser.firstName = _this.f.firstname.value;
                    currentUser.lastName = _this.f.lastName.value;
                    localStorage.setItem('currentUser', JSON.stringify(currentUser));
                    _this.dialogRef.close();
                    //alert notification
                    _this.alertService.successNotification("Updating profile success.");
                    _this.IsSave = false;
                }, function (error) { _this.alertService.errorNotification(error); _this.IsSave = false; });
                return [2 /*return*/];
            });
        });
    };
    ProfileDetailsDialogComponent.prototype.preview = function (files) {
        var _this = this;
        if (files.length === 0)
            return;
        var mimeType = files[0].type;
        if (mimeType.match(/image\/*/) == null) {
            this.message = "Only images are supported.";
            return;
        }
        var reader = new FileReader();
        this.imagePath = files;
        reader.readAsDataURL(files[0]);
        reader.onload = function (_event) {
            _this.photo = reader.result;
        };
    };
    ProfileDetailsDialogComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    ProfileDetailsDialogComponent = __decorate([
        core_2.Component({
            selector: 'app-profile-details',
            templateUrl: './profile-details.component.html',
            styleUrls: ['./profile-details.component.css']
        }),
        __param(6, core_1.Inject(material_1.MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            user_service_1.UserService,
            alert_service_1.AlertService,
            authentication_service_1.AuthenticationService,
            material_1.MatDialogRef,
            compress_image_service_1.CompressImageService, Object])
    ], ProfileDetailsDialogComponent);
    return ProfileDetailsDialogComponent;
}());
exports.ProfileDetailsDialogComponent = ProfileDetailsDialogComponent;
//# sourceMappingURL=profile-details.component.js.map