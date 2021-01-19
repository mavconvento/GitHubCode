import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import 'hammerjs';
import { AppMaterialModule } from './app-material/app-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JwtInterceptor } from './helpers/jwt.interceptor'
import { ErrorInterceptor } from './helpers/error.interceptor'

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
import { ProfileDetailsComponent } from './components/dialog/profile-details/profile-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LinkNumberComponent } from './components/dialog/link-number/link-number.component';

@NgModule({
  declarations: [
    AppComponent,
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
    OnlineClockingComponent,
    MainMenuComponent,
    ProfileDetailsComponent,
    LinkNumberComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
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
    ]),
    FlexLayoutModule,
    AppMaterialModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ],
  bootstrap: [AppComponent],
  entryComponents: [ProfileDetailsComponent]
})
export class AppModule { }
