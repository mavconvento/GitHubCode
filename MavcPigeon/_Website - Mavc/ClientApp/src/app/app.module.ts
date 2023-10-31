import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import 'hammerjs';
import { AppMaterialModule } from './app-material/app-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';

//filterpipe
import { MobileListFilterPipe } from './components/shared/mobilelist-filter.pipe';
import { ClubListFilterPipe } from './components/shared/clublist-filter.pipe';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { LogoutComponent } from './components/logout/logout.component';
import { LoginComponent } from './components/login/login.component';
import { AlertComponent } from './components/alert/alert.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './helpers/auth.guard';
import { ProfileComponent } from './components/profile/profile.component';
import { RaceResultComponent } from './components/race-result/race-result.component';
import { OnlineClockingComponent } from './components/online-clocking/online-clocking.component';
import { MainMenuComponent } from './components/main-menu/main-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QRCodeClockingComponent } from './components/qrcode-clocking/qrcode-clocking.component';

//dialog
import { LinkNumberDialogComponent } from './components/dialog/link-number/link-number.component';
import { OnlineClockingDialogComponent } from './components/dialog/online-clocking/online-clocking.component';
import { ConfirmdialogComponent } from './components/dialog/confirmdialog/confirmdialog.component';
import { ProfileDetailsDialogComponent } from './components/dialog/profile-details/profile-details.component';
import { ForgotPasswordComponent } from './components/dialog/forgot-password/forgot-password.component';

//youtube
import { YouTubePlayerModule } from "@angular/youtube-player";
import { TutorialsComponent } from './components/tutorials/tutorials.component';
import { MemberDistanceComponent } from './components/member-distance/member-distance.component';
import { MemberLogsComponent } from './components/member-logs/member-logs.component';
import { PedigreeComponent } from './components/pedigree/pedigree.component';
import { RaceTopigeonComponent } from './components/race-topigeon/race-topigeon.component';

@NgModule({
  declarations: [
    AppComponent,
    //filterpipe
    MobileListFilterPipe,
    ClubListFilterPipe,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LogoutComponent,
    LoginComponent,
    AlertComponent,
    RegisterComponent,
    ProfileComponent,
    RaceResultComponent,
    RaceTopigeonComponent,
    OnlineClockingComponent,
    MainMenuComponent,
    QRCodeClockingComponent,

    //dialog
    ProfileDetailsDialogComponent,
    LinkNumberDialogComponent,
    OnlineClockingDialogComponent,
    ConfirmdialogComponent,
    ForgotPasswordComponent,
    TutorialsComponent,
    MemberDistanceComponent,
    MemberLogsComponent,
    PedigreeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    YouTubePlayerModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent, },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'race-result', component: RaceResultComponent, canActivate: [AuthGuard] },
      { path: 'online-clocking', component: OnlineClockingComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'logout', component: LogoutComponent },
      { path: 'tutorials', component: TutorialsComponent },
      { path: 'logs', component: MemberLogsComponent },
      { path: 'qr', component: QRCodeClockingComponent },
      { path: 'topigeon', component: RaceTopigeonComponent, canActivate: [AuthGuard] },
      { path: 'distance/:clubname/:memberidno/:dbname', component: MemberDistanceComponent },
    ]),
    FlexLayoutModule,
    AppMaterialModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ],
  bootstrap: [AppComponent],
  entryComponents: [ProfileDetailsDialogComponent, ForgotPasswordComponent, LinkNumberDialogComponent, OnlineClockingDialogComponent, ConfirmdialogComponent]
})
export class AppModule { }
