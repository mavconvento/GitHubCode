import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';
import { UserService } from '../services/user.services';

@Component({
  selector: 'app-un-claimed-ticket',
  templateUrl: './un-claimed-ticket.component.html',
  styleUrls: ['./un-claimed-ticket.component.css']
})
export class UnClaimedTicketComponent implements OnInit {
  myform: FormGroup;
  unclaimedlist: Array<any>;
  errorMessage: string;
  isShow: boolean = false;
  tellerlist: Array<any>;

  //table colums
  displayedColumns: string[] = ['referenceid', 'fightno', 'amount'];

  constructor(
    private betting: BettingService,
    private formBuilder: FormBuilder,
    private user: UserService,
    private event: EventsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      Teller: ['', Validators.required],
    });

    this.onGetEventDetails();
  }

  onGetEventDetails() {
    this.event.GetEvents().subscribe(x => {
      var result = JSON.parse(x.content)
      localStorage.setItem("eventId", result.EventId);
      this.getTellers(localStorage.getItem('companyId'), result.EventId).then(x => {
        this.tellerlist = x;
        if (this.myform.controls["Teller"].value != '' && this.myform.controls["Teller"].value != null) this.searchteller(this.myform.controls["Teller"].value);
      });
    }, error => { this.showerror(error.error.message) });
  }


  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }

  async getTellers(id: string, eventid: string): Promise<any> {
    var data = await this.user.GetTellerList(id, eventid, 0).toPromise();
    return JSON.parse(data.content);
  }

  back() {
    this.router.navigate(['/main']);
  }

  searchteller(event) {
    var tellerSearch = this.tellerlist.filter(x => x.UserName.toUpperCase().indexOf(event.toUpperCase()) > -1);
    this.GetUnClaimedTicket(tellerSearch[0].Userid);
  }

  GetUnClaimedTicket(userid: string): void {
    this.betting.GetUnClaimsTicket(localStorage.getItem("eventId"), userid).subscribe(x => {
      var result = JSON.parse(x.content);
      this.unclaimedlist = result;
      //console.table(result);
    });
  }

}
