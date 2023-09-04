"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
require("hammerjs");
var app_material_module_1 = require("./app-material/app-material.module");
var flex_layout_1 = require("@angular/flex-layout");
var jwt_interceptor_1 = require("./helpers/jwt.interceptor");
var error_interceptor_1 = require("./helpers/error.interceptor");
//filterpipe
var mobilelist_filter_pipe_1 = require("./components/shared/mobilelist-filter.pipe");
var clublist_filter_pipe_1 = require("./components/shared/clublist-filter.pipe");
var app_component_1 = require("./app.component");
var nav_menu_component_1 = require("./components/nav-menu/nav-menu.component");
var home_component_1 = require("./components/home/home.component");
var counter_component_1 = require("./components/counter/counter.component");
var fetch_data_component_1 = require("./components/fetch-data/fetch-data.component");
var logout_component_1 = require("./components/logout/logout.component");
var login_component_1 = require("./components/login/login.component");
var alert_component_1 = require("./components/alert/alert.component");
var register_component_1 = require("./components/register/register.component");
var auth_guard_1 = require("./helpers/auth.guard");
var profile_component_1 = require("./components/profile/profile.component");
var race_result_component_1 = require("./components/race-result/race-result.component");
var online_clocking_component_1 = require("./components/online-clocking/online-clocking.component");
var main_menu_component_1 = require("./components/main-menu/main-menu.component");
var animations_1 = require("@angular/platform-browser/animations");
//dialog
var link_number_component_1 = require("./components/dialog/link-number/link-number.component");
var online_clocking_component_2 = require("./components/dialog/online-clocking/online-clocking.component");
var confirmdialog_component_1 = require("./components/dialog/confirmdialog/confirmdialog.component");
var profile_details_component_1 = require("./components/dialog/profile-details/profile-details.component");
var forgot_password_component_1 = require("./components/dialog/forgot-password/forgot-password.component");
//youtube
var youtube_player_1 = require("@angular/youtube-player");
var tutorials_component_1 = require("./components/tutorials/tutorials.component");
var member_distance_component_1 = require("./components/member-distance/member-distance.component");
var member_logs_component_1 = require("./components/member-logs/member-logs.component");
var pedigree_component_1 = require("./components/pedigree/pedigree.component");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                //filterpipe
                mobilelist_filter_pipe_1.MobileListFilterPipe,
                clublist_filter_pipe_1.ClubListFilterPipe,
                nav_menu_component_1.NavMenuComponent,
                home_component_1.HomeComponent,
                counter_component_1.CounterComponent,
                fetch_data_component_1.FetchDataComponent,
                logout_component_1.LogoutComponent,
                login_component_1.LoginComponent,
                alert_component_1.AlertComponent,
                register_component_1.RegisterComponent,
                profile_component_1.ProfileComponent,
                race_result_component_1.RaceResultComponent,
                online_clocking_component_1.OnlineClockingComponent,
                main_menu_component_1.MainMenuComponent,
                //dialog
                profile_details_component_1.ProfileDetailsDialogComponent,
                link_number_component_1.LinkNumberDialogComponent,
                online_clocking_component_2.OnlineClockingDialogComponent,
                confirmdialog_component_1.ConfirmdialogComponent,
                forgot_password_component_1.ForgotPasswordComponent,
                tutorials_component_1.TutorialsComponent,
                member_distance_component_1.MemberDistanceComponent,
                member_logs_component_1.MemberLogsComponent,
                pedigree_component_1.PedigreeComponent
            ],
            imports: [
                platform_browser_1.BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
                http_1.HttpClientModule,
                animations_1.BrowserAnimationsModule,
                youtube_player_1.YouTubePlayerModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                router_1.RouterModule.forRoot([
                    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
                    //{ path: 'counter', component: CounterComponent, },
                    { path: 'profile', component: profile_component_1.ProfileComponent, canActivate: [auth_guard_1.AuthGuard] },
                    { path: 'race-result', component: race_result_component_1.RaceResultComponent, canActivate: [auth_guard_1.AuthGuard] },
                    { path: 'online-clocking', component: online_clocking_component_1.OnlineClockingComponent, canActivate: [auth_guard_1.AuthGuard] },
                    { path: 'login', component: login_component_1.LoginComponent },
                    { path: 'register', component: register_component_1.RegisterComponent },
                    { path: 'logout', component: logout_component_1.LogoutComponent },
                    { path: 'tutorials', component: tutorials_component_1.TutorialsComponent },
                    { path: 'logs', component: member_logs_component_1.MemberLogsComponent },
                    { path: 'distance/:clubname/:memberidno/:dbname', component: member_distance_component_1.MemberDistanceComponent },
                ]),
                flex_layout_1.FlexLayoutModule,
                app_material_module_1.AppMaterialModule
            ],
            providers: [
                { provide: http_1.HTTP_INTERCEPTORS, useClass: jwt_interceptor_1.JwtInterceptor, multi: true },
                { provide: http_1.HTTP_INTERCEPTORS, useClass: error_interceptor_1.ErrorInterceptor, multi: true }
            ],
            schemas: [
                core_1.CUSTOM_ELEMENTS_SCHEMA,
                core_1.NO_ERRORS_SCHEMA
            ],
            bootstrap: [app_component_1.AppComponent],
            entryComponents: [profile_details_component_1.ProfileDetailsDialogComponent, forgot_password_component_1.ForgotPasswordComponent, link_number_component_1.LinkNumberDialogComponent, online_clocking_component_2.OnlineClockingDialogComponent, confirmdialog_component_1.ConfirmdialogComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map