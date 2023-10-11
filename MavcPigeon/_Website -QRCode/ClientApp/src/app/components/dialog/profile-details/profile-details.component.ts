import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { Guid } from "guid-typescript";
import { AlertService } from '../../../services/alert.service';
import { UserService } from '../../../services/user.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { CompressImageService } from '../../../services/compress-image.service';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.css']
})
export class ProfileDetailsDialogComponent implements OnInit {
  form: FormGroup;
  title: string;
  fileToUpload: File;
  id: Guid;
  fileUploadID: string;
  IsSave: boolean = false;

  public imagePath;
  photo: any;
  public message: string;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<ProfileDetailsDialogComponent>,
    private compressImage: CompressImageService,

    @Inject(MAT_DIALOG_DATA) data) {

    this.title = data.title;
    this.fileUploadID = data.fileUploadID;
    this.id = Guid.create();
  }

  ngOnInit() {
    let currentUser = this.authenticationService.currentUserValue;
    
    this.form = this.fb.group({
      loftName: [currentUser.loftName, []],
      firstname: [currentUser.firstName, []],
      lastName: [currentUser.lastName, []],
      photo:['',[]]
        });
  }

  onFileSelected(events) {
    let image: File = events.target.files[0]


    console.log(image.size)
    this.compressImage.compress(image)
      .subscribe(compressedImage => {
        this.fileToUpload = compressedImage;
        console.log(this.fileToUpload);

        //use original image if convert images is large
        if (image.size < compressedImage.size) {
            this.fileToUpload = image;
        }
      })

    //console.log(this.fileToUpload.size)
    this.preview(events.target.files);
  };

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  async save() {
    this.IsSave = true;
    var formData = new FormData();
    let currentUser = this.authenticationService.currentUserValue;

    formData.append('image', this.fileToUpload);
    formData.append('userID', currentUser.userID);
    formData.append('LoftName', this.f.loftName.value);
    formData.append('FirstName', this.f.firstname.value);
    formData.append('LastName', this.f.lastName.value);

    if (this.fileUploadID)
       formData.append('fileUploadID', this.fileUploadID);

    this.userService.updateProfile(formData).subscribe(data => {
      //console.log(JSON.parse(data.content));
      var result = JSON.parse(data.content)

      if (result[0].FileUploadId) {
        localStorage.setItem("profileImageID", result[0].FileUploadId)
        currentUser.fileUploadID = result[0].FileUploadId
      }
      else
        localStorage.removeItem("profileImageID");

      currentUser.loftName = this.f.loftName.value;
      currentUser.firstName = this.f.firstname.value;
      currentUser.lastName = this.f.lastName.value;
      localStorage.setItem('currentUser', JSON.stringify(currentUser));

      this.dialogRef.close();

      //alert notification
      this.alertService.successNotification("Updating profile success.");
      this.IsSave = false;
    }, error => { this.alertService.errorNotification(error); this.IsSave = false; });
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
