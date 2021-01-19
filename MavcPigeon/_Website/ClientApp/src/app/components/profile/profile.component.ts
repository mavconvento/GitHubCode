import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material';
import { ProfileDetailsComponent } from '../dialog/profile-details/profile-details.component';

import { ImageService } from '../../services/image.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Result } from '../../models/result';
import { Observable } from 'rxjs';


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

  constructor(
    private dialog: MatDialog,
    private authenticationService: AuthenticationService,
    private imageService: ImageService
  ) { }

  ngOnInit() {
    this.getMemberInfo();
  };

  onFileSelected(events) {
    //console.log(events)
  };

  getProfilePicture(fileUploadID: string) {
    this.imageService.getImage(fileUploadID).subscribe
      (result => {
        //console.log(result)
        this.profileImage = 'data:' + result.fileType + ';base64,' + result.data;
      });
  };

  getMemberInfo() {
    let currentUser = this.authenticationService.currentUserValue;
    this.name = currentUser.firstName + ' ' + currentUser.lastName;
    this.loftName = currentUser.loftName;
    this.globelId = currentUser.globalId;
    this.emailAddress = currentUser.userName;

    //console.log(currentUser);
    this.getProfilePicture(currentUser.fileUploadID);
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.hasBackdrop = true;

    dialogConfig.data = {
      id: 1,
      title: 'Profile Update'
    };

    const dialogRef = this.dialog.open(ProfileDetailsComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        this.getMemberInfo();
        //console.log("close");
      });
  }
}
