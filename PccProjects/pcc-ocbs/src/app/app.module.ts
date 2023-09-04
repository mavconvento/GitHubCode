import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppMaterialModule } from './app-material/app-material.module';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PrintLayoutComponent } from './print-layout/print-layout.component';
import { InvoiceComponent } from './invoice/invoice.component';
import {PrintService} from './print.service';
import { PccOcbsComponent } from './pcc-ocbs/pcc-ocbs.component';
import { BetPrintComponent } from './bet-print/bet-print.component';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClaimsPrintComponent } from './claims-print/claims-print.component';
import { MainOcbsComponent } from './main-ocbs/main-ocbs.component';
import { DecimalPipe,formatNumber } from '@angular/common';
import { OddsPrintComponent } from './odds-print/odds-print.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { UserLoginComponent } from './user-login/user-login.component';
import { NgxBarcodeModule } from 'ngx-barcode';
import { ConfirmDialogComponent } from './dialog/confirm-dialog/confirm-dialog.component';
import { ShowOddsComponent } from './show-odds/show-odds.component';
import { ManagementFightComponent } from './management-fight/management-fight.component';
import { ManagementPointsComponent } from './management-points/management-points.component';
import { ManagementEventsComponent } from './management-events/management-events.component';
import { UnClaimedTicketComponent } from './un-claimed-ticket/un-claimed-ticket.component';
import { ConfirmFightnoComponent } from './dialog/confirm-fightno/confirm-fightno.component';
import { BettingHistoryComponent } from './betting-history/betting-history.component';
import { ReportPrintComponent } from './reportprint/report-print/report-print.component';
import { ManagementUserComponent } from './management-user/management-user/management-user.component';
import { UserRegistrationComponent } from './management-user/user-registration/user-registration.component';

@NgModule({
  declarations: [
    AppComponent,
    PrintLayoutComponent,
    InvoiceComponent,
    PccOcbsComponent,
    BetPrintComponent,
    ClaimsPrintComponent,
    MainOcbsComponent,
    OddsPrintComponent,
    UserLoginComponent,
    ConfirmDialogComponent,
    ShowOddsComponent,
    ManagementFightComponent,
    ManagementPointsComponent,
    ManagementEventsComponent,
    UnClaimedTicketComponent,
    ConfirmFightnoComponent,
    BettingHistoryComponent,
    ReportPrintComponent,
    ManagementUserComponent,
    UserRegistrationComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    NgxBarcodeModule
  ],
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy},PrintService, DecimalPipe],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmDialogComponent,ConfirmFightnoComponent]
})
export class AppModule { }
