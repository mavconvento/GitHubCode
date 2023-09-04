import { Injectable, OnInit } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HelperService {
  constructor(private httpClient: HttpClient) {
  }

  refreshTranDate(): void {
    localStorage.setItem('lasttransaction', Date())
  }

  ClearLoginSession() {
    localStorage.setItem("tokenBearer", null);
    localStorage.setItem("firstName", null);
    localStorage.setItem("userId", null);
    localStorage.setItem("platformUserId", null);
    localStorage.setItem("IsOffline", null);
    localStorage.setItem("companyId", null);
    localStorage.setItem("roleDescription", null);
    localStorage.setItem("lasttransaction", null);
    localStorage.setItem("eventname", null);
    this.ClearOcbsLocalStorage();
  }

  ClearOcbsLocalStorage() {
    localStorage.setItem("WalaTotalBet", "0.00")
    localStorage.setItem("MeronTotalBet", "0.00")
    localStorage.setItem("DrawTotalBet", "0.00")
    localStorage.setItem("eventId", null)
    localStorage.setItem("fightNo", null);
    localStorage.setItem("fightId", null);
    localStorage.setItem("teller", null);
    localStorage.setItem("teller", null);
    localStorage.setItem("odds", null);
  }

}