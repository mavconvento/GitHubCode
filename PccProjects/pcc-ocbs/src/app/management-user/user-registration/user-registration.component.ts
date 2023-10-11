import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { HelperService } from 'src/app/services/helper.services';
import { UserService } from 'src/app/services/user.services';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit, AfterViewInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  isHidden: boolean = false;
  isLastCall: boolean = false;
  isChecked: boolean = true;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private helper: HelperService,
    private dialog: MatDialog,
    private user: UserService
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      retypepassword: ['', Validators.required],
      isactive: [false, Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      roleid: [0, Validators.required],
      companyid: [0, Validators.required],
    });
  }

  ngAfterViewInit(): void {
    this.clear();
  }

  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }


  onSubmit() {
    let error = '';
    if (this.myform.controls['firstname'].value == '') error = 'Firstname is empty.'
    else if (this.myform.controls['lastname'].value == '') error = 'Lastname is empty.'
    else if (this.myform.controls['lastname'].value == '') error = 'Password is empty.'
    else if (this.myform.controls['retypepassword'].value != this.myform.controls['password'].value) error = "Password didn't match"


    if (error != '') {
      this.showerror(error);
      return;
    }

    this.user.UserSave(this.myform.value).subscribe(x => {
      var result = JSON.parse(x.content)
      console.log(result);
      if (result == 'success') {
        //this.clear();
        this.showerror("User registration is complete.")
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      }
      else this.showerror(result)

    }, error => {
      console.log(error);
      this.showerror(error.error)
    });
  }

  clear() {
    this.myform.controls["username"].setValue('');
    this.myform.controls["password"].setValue('');
    this.myform.controls["retypepassword"].setValue('');
    this.myform.controls["firstname"].setValue('');
    this.myform.controls["lastname"].setValue('');
  }

}
