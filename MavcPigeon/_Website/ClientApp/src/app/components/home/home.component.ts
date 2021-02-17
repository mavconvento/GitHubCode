
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material';
import { ProfileDetailsDialogComponent } from '../dialog/profile-details/profile-details.component';
import { LinkNumberDialogComponent } from '../dialog/link-number/link-number.component';
import { OnlineClockingDialogComponent } from '../dialog/online-clocking/online-clocking.component';
import { ConfirmdialogComponent } from '../dialog/confirmdialog/confirmdialog.component';
import { ImageService } from '../../services/image.service';
import { UserService } from '../../services/user.service';
import { AccountService } from '../../services/account.service';
import { AuthenticationService } from '../../services/authentication.service';
import { AlertService } from '../../services/alert.service';
import { Observable } from 'rxjs';
import { HostListener } from "@angular/core";
import { Helpers } from '../../helpers/helpers';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  scrHeight: any;
  scrWidth: any;
  width: number = 500;
  videoTitle: string;
  videoId: string;

  constructor(private authenticationService: AuthenticationService,
    private imageService: ImageService,
    private userService: UserService,
    private accountService: AccountService,
    private alertService: AlertService,
    private helper: Helpers) {

    this.getScreenSize();
  }

  ngOnInit() {
    //this.videoId = "HrjrRmkImCs";
    //this.userService.getVideo("main").subscribe(data => {
    //  var result = JSON.parse(data.content);
    //  console.log(result);
    //  this.videoTitle = result.Table[0].Title;
    //  this.videoId = result.Table[0].VideoID;

    //}, error => { this.alertService.errorNotification(error) }
    //)

    //setTimeout(() => {                          
      
    //}, 3000);
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.scrHeight = window.innerHeight;
    this.scrWidth = window.innerWidth;

    //console.log(this.scrWidth);
    if (this.scrWidth < 565)
      this.width = 330;
    else if (this.scrWidth < 1008) {
      this.width = 500;
    }
    else {
      this.width = 500;
    }

  }

  GetMobileList() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userService.getLinkMobileList(currentUser.userID).subscribe(result => {
      var data = JSON.parse(result.content);
      localStorage.setItem("clubs", JSON.stringify(data.Table));
      localStorage.setItem("mobile", JSON.stringify(data.Table1));
    },
      error => {
        this.alertService.errorNotification(error);
      });
  }
}
