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
import { parse } from 'url';
import { Helpers } from '../../helpers/helpers';
import { Club } from '../../models/clubname';
import { Router } from '@angular/router';
import { flatMap } from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileImage: any;
  name: string;
  loftName: Observable<string>;
  emailAddress: string;
  globelId: string;
  clubList: any;
  mobileList: any;
  primaryMobileNumber: string;
  eclockMobileNumber: string;
  totalLoad: string = "0";
  isAllowPasaload: boolean;
  userID: string;
  clubName: string;
  dbName: string;
  picture: string = "7d033eb5-80ac-4ad1-88be-e39c33fe1f47";
  isPha: Boolean = false;

  constructor(
    private dialog: MatDialog,
    private authenticationService: AuthenticationService,
    private imageService: ImageService,
    private userService: UserService,
    private accountService: AccountService,
    private alertService: AlertService,
    private helper: Helpers,
    private router: Router
  ) { }

  ngOnInit() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userID = currentUser.userID;


    if (localStorage.getItem("userlogin") == "pha.mavcpigeon@gmail.com") {
      this.isPha = true;
      console.log(this.isPha);
    }

    //set profileImageID
    if (currentUser.fileUploadID) {
      localStorage.setItem("profileImageID", currentUser.fileUploadID)
    }

    this.getMemberInfo();
    this.GetMobileList();
  };

  onFileSelected(events) {
 
  };

  getProfilePicture(fileUploadID: string) {
    this.imageService.getImage(fileUploadID).subscribe
      (result => {
        this.profileImage = 'data:' + result.fileType + ';base64,' + result.data;
      });
  };

  getMemberInfo() {
    let currentUser = this.authenticationService.currentUserValue;
    this.name = currentUser.firstName + ' ' + currentUser.lastName;
    this.loftName = currentUser.loftName;
    this.globelId = currentUser.globalId;
    this.emailAddress = currentUser.userName;

    if (localStorage.getItem("profileImageID")) {
      this.getProfilePicture(localStorage.getItem("profileImageID"));
    }
  }

  openProfileDialog() {
    let currentUser = this.authenticationService.currentUserValue;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      title: 'Profile Update',
      fileUploadID: currentUser.fileUploadID
    };

    const dialogRef = this.dialog.open(ProfileDetailsDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        this.getMemberInfo();    
      });
  }

  openOnlineClockingDialog(dialogType: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    var title = "Load Mavc Card";
    var isLoadCard = false;
    var isPasaload = false;
    var pasaloadBalance = 0;
    var mobilenumber = this.primaryMobileNumber;
    var clubName = this.clubName;
    var dbName = this.dbName;

    if (dialogType == "PASALOAD") {
      var allowPasaload = this.mobileList.filter(x => x.AllowPasaload == "1" && Number(x.LoadBalance) > 0 && x.MobileNumber == mobilenumber);
      isPasaload = true;
      title = "Pasaload";

      if (allowPasaload.length > 0) {
        pasaloadBalance = allowPasaload[0].LoadBalance;
        mobilenumber = allowPasaload[0].MobileNumber;
      }
    }
    else if (dialogType == "LOADCARD") {
      isLoadCard = true;
      title = "Load Mavc Card";
    }

    dialogConfig.data = {
      title: title,
      isPasaload: isPasaload,
      isLoadCard: isLoadCard,
      pasaloadBalance: pasaloadBalance,
      mobilenumber: mobilenumber,
      clubName: clubName,
      dbName: dbName
    };

    const dialogRef = this.dialog.open(OnlineClockingDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
       
        this.GetMobileList();
      });
  };

  openLinkMobileDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      title: 'Link Mobile Number'
    };

    const dialogRef = this.dialog.open(LinkNumberDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        this.GetMobileList();
      });
  }

  SetAsPrimary(mobilenumber: string) {
    this.accountService.setAsPrimary(mobilenumber, this.userID).subscribe(x => {
      this.alertService.simpleNotification("Mobile number : " + mobilenumber + " is now set as primary number.");
      this.GetMobileList();
    }, error => { this.alertService.errorNotification(error) });

  }

  Unreg(mobilenumber: string, club: string, db: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      title: 'Confirm Unregistration.',
      message: 'Are you sure you want to unreg this mobile number?'
    };

    const dialogRef = this.dialog.open(ConfirmdialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.accountService.Unreg(mobilenumber, this.userID, club, db).subscribe(result => {
            var data = JSON.parse(result.content);
            this.alertService.simpleNotification(data.Table[0].Result);
            this.GetMobileList();
          }, error => { this.alertService.errorNotification(error) });
        }
      });
  }

  getMemberCoordinates(item: any) {
    console.log(item);
    this.router.navigate(["/distance", item.clubabbreviation, item.MemberClubID, item.dbName])
    //this.userService.getMemberCoordinates(item.MemberClubID, item.clubabbreviation, item.dbName).subscribe(data => {
    //  var result = JSON.parse(data.content);
    //  if (result.Table.length > 0) {
    //    window.open('https://www.google.com/maps/place/' + result.Table[0].Lat + ' ' + result.Table[0].Long, '_blank');
    //  }
    //  console.log((JSON.parse(data.content)).Table)
    //});
  }

  GetMobileList() {
    let currentUser = this.authenticationService.currentUserValue;
    this.userService.getLinkMobileList(currentUser.userID).subscribe(result => {
      var data = JSON.parse(result.content);
      localStorage.setItem("clubs", JSON.stringify(data.Table));
      localStorage.setItem("mobile", JSON.stringify(data.Table1));

      var club = JSON.parse(localStorage.getItem("clubs"));
      this.mobileList = JSON.parse(localStorage.getItem("mobile")); 
      this.clubList = club.filter(x => x.MemberClubID != '')

      var primary = this.mobileList.filter(x => x.IsMain == true);
      var eclock = this.mobileList.filter(x => x.IsClock == "YES");
      var allowPasaload = this.mobileList.filter(x => x.AllowPasaload == "1");

      if (primary.length > 0) {
        localStorage.setItem("primary", primary[0].MobileNumber);

        this.primaryMobileNumber = primary[0].MobileNumber;
        this.clubName = primary[0].ClubAbbreviation;
        this.dbName = primary[0].dbName;
        this.totalLoad = primary[0].LoadBalance;
      }

      if (eclock.length > 0) {
        this.eclockMobileNumber = eclock[0].MobileNumber;
      }

      if (allowPasaload.length > 0) {
        this.isAllowPasaload = true;
      }

    },
      error => {
        this.alertService.errorNotification(error);
      });
  }
}
