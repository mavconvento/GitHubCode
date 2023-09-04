import { AfterViewInit, Component, OnInit } from '@angular/core';
import { environment } from './../environments/environment';
import {Router,ActivatedRoute} from '@angular/router';
import { HelperService } from './services/helper.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, AfterViewInit {

  constructor(
    private router: Router,
    private helper: HelperService
  ) {
  }
  
  ngAfterViewInit(){
    var token = localStorage.getItem('tokenBearer');
    var lasttransaction = new Date(localStorage.getItem('lasttransaction'));
    var expiration = this.expiration(lasttransaction,30);
    var dateNow = new Date();
    
    if(token == null || token == "null" || dateNow > expiration )
    {
      this.helper.ClearLoginSession();
      this.router.navigate(["/login"]); 
    } 
    else
    {
      localStorage.setItem("lasttransaction", Date());
      this.router.navigate(["/main"]);
    }
  }

  expiration(date: Date, min: number): Date {
    return new Date(new Date(date).setMinutes(date.getMinutes() + min));
  }
  
  ngOnInit(){
    //this.helper.ClearLoginSession();
  }
}
