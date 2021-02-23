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
exports.LoginComponent = void 0;
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var operators_1 = require("rxjs/operators");
var authentication_service_1 = require("../../services/authentication.service");
var alert_service_1 = require("../../services/alert.service");
var material_1 = require("@angular/material");
var forgot_password_component_1 = require("../dialog/forgot-password/forgot-password.component");
var LoginComponent = /** @class */ (function () {
    function LoginComponent(dialog, formBuilder, route, router, authenticationService, alertService) {
        this.dialog = dialog;
        this.formBuilder = formBuilder;
        this.route = route;
        this.router = router;
        this.authenticationService = authenticationService;
        this.alertService = alertService;
        this.loading = false;
        this.submitted = false;
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/profile']);
        }
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.loginForm = this.formBuilder.group({
            username: ['', forms_1.Validators.required],
            password: ['', forms_1.Validators.required]
        });
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/profile';
    };
    Object.defineProperty(LoginComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.loginForm.controls; },
        enumerable: false,
        configurable: true
    });
    LoginComponent.prototype.onSubmit = function () {
        var _this = this;
        this.submitted = true;
        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }
        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(operators_1.first())
            .subscribe(function (data) {
            _this.router.navigate([_this.returnUrl]);
        }, function (error) {
            _this.alertService.errorNotification(error);
            _this.loading = false;
        });
    };
    LoginComponent.prototype.forgotPassword = function () {
        var dialogConfig = new material_1.MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.hasBackdrop = true;
        dialogConfig.data = {
            title: 'Forgot Password'
        };
        var dialogRef = this.dialog.open(forgot_password_component_1.ForgotPasswordComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(function (data) {
            console.log(data);
        });
        console.log("forgot password");
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: 'app-login',
            templateUrl: './login.component.html',
            styleUrls: ['./login.component.css']
        })
        //export class LoginComponent implements OnInit {
        //  constructor(private helpers: Helpers, private router: Router, private tokenService: TokenService) {
        //  }
        //  ngOnInit() {
        //  }
        //  //login(): void {
        //  //  let authValues = { "Username": "pablo", "Password": "secret" };
        //  //  this.tokenService.auth(authValues).subscribe(token => {
        //  //    this.helpers.setToken(token);
        //  //    this.router.navigate(['/dashboard']);
        //  //  });
        //  //}
        //}
        ,
        __metadata("design:paramtypes", [material_1.MatDialog,
            forms_1.FormBuilder,
            router_1.ActivatedRoute,
            router_1.Router,
            authentication_service_1.AuthenticationService,
            alert_service_1.AlertService])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map