import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BetPrintComponent } from './bet-print/bet-print.component';
import { BettingHistoryComponent } from './betting-history/betting-history.component';
import { ClaimsPrintComponent } from './claims-print/claims-print.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { MainOcbsComponent } from './main-ocbs/main-ocbs.component';
import { ManagementEventsComponent } from './management-events/management-events.component';
import { ManagementFightComponent } from './management-fight/management-fight.component';
import { ManagementPointsComponent } from './management-points/management-points.component';
import { ManagementUserComponent } from './management-user/management-user/management-user.component';
import { UserRegistrationComponent } from './management-user/user-registration/user-registration.component';
import { OddsPrintComponent } from './odds-print/odds-print.component';
import { PccOcbsComponent } from './pcc-ocbs/pcc-ocbs.component';
import { PrintLayoutComponent } from './print-layout/print-layout.component';
import { ReportPrintComponent } from './reportprint/report-print/report-print.component';
import { ShowOddsComponent } from './show-odds/show-odds.component';
import { UnClaimedTicketComponent } from './un-claimed-ticket/un-claimed-ticket.component';
import { UserLoginComponent } from './user-login/user-login.component';

const routes: Routes = [
  { path: '', component: PccOcbsComponent, pathMatch: 'full' },
  { path: 'main', component: MainOcbsComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'claims', component: ClaimsPrintComponent },
  { path: 'oddsprint', component: OddsPrintComponent },
  { path: 'betprint', component: BetPrintComponent },
  { path: 'reportprint', component: ReportPrintComponent },
  { path: 'showodds', component: ShowOddsComponent },
  { path: 'events', component: ManagementEventsComponent },
  { path: 'fight', component: ManagementFightComponent },
  { path: 'points', component: ManagementPointsComponent },
  { path: 'unclaimed', component: UnClaimedTicketComponent },
  { path: 'bettinghistory', component: BettingHistoryComponent },
  { path: 'users', component: ManagementUserComponent },
  { path: 'registration', component: UserRegistrationComponent },
  {
    path: 'print',
    outlet: 'print',
    component: PrintLayoutComponent,
    children: [
      { path: 'invoice/:invoiceIds', component: InvoiceComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }