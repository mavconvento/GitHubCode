import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { Guid } from "guid-typescript";
import { AlertService } from '../../../services/alert.service';
import { UserService } from '../../../services/user.service';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.css']
})
export class ProfileDetailsComponent implements OnInit {
  form: FormGroup;
  title: string;
  fileToUpload: File;
  id: Guid;

  public imagePath;
  photo: any;
  public message: string;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<ProfileDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    console.log(data);
    this.title = data.title;
    this.id = Guid.create();
  }

  ngOnInit() {
    let currentUser = this.authenticationService.currentUserValue;
    
    this.form = this.fb.group({
      loftName: [currentUser.loftName, []],
      photo:['',[]]
        });
  }

  onFileSelected(events) {
    console.log(events)
    this.fileToUpload = events.target.files[0];
    this.preview(events.target.files);
    //console.log(this.fileToUpload);
  };

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  async save() {
    var formData = new FormData();
    let currentUser = this.authenticationService.currentUserValue;

    //console.log(this.fileToUpload);
    //console.log(this.id.toString());

    formData.append('image', this.fileToUpload);
    formData.append('userID', currentUser.userID);
    formData.append('LoftName', this.f.loftName.value);

    this.userService.updateProfile(formData).subscribe(data => {
      localStorage.setItem("profileImageID", data.content)
      currentUser.loftName = this.f.loftName.value;
      localStorage.setItem('currentUser', JSON.stringify(currentUser));
      //console.log(data.content);
      this.dialogRef.close();

      //alert notification
      this.alertService.successNotification("Updating profile success.");
    });
  }

  preview(files) {
    if (files.length === 0)
      return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }

    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
    this.photo = reader.result;
    }
  }

  close() {
    this.dialogRef.close();
  }

}
