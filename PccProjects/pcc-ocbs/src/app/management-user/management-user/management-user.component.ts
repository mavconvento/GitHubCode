import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BettingService } from 'src/app/services/betting.services';
import { EventsService } from 'src/app/services/event.services';
import { UserService } from 'src/app/services/user.services';


@Component({
  selector: 'app-management-user',
  templateUrl: './management-user.component.html',
  styleUrls: ['./management-user.component.css']
})
export class ManagementUserComponent implements OnInit {
  isChecked: boolean
  myform: FormGroup;
  companyList: Array<any>;
  errorMessage: string;
  isShow: boolean = false;
  userList: Array<any>;
  userListorig: Array<any>;
  roleList: Array<any>;

  //table colums
  displayedColumns: string[] = ['userid', 'username', 'firstname', 'status', 'select'];

  constructor(
    private betting: BettingService,
    private formBuilder: FormBuilder,
    private user: UserService,
    private event: EventsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      username: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      companyid: ['', Validators.required],
      roleid: ['', Validators.required],
      isactive: [false, Validators.required],
      id: [0, Validators.required],
      userid: [0, Validators.required],
      searchuser: ['']
    });
    this.onGetDropdown();
    this.GetUserList();
  }

  onChange(value: string) {
    if (value.length > 0) {
      var result = this.userListorig.filter(x => x.firstName.toUpperCase().indexOf(value.toUpperCase()) > -1 || x.lastName.toUpperCase().indexOf(value.toUpperCase()) > -1 || x.Status.toUpperCase().indexOf(value.toUpperCase()) > -1);
      this.userList = result;
    }
    else
      this.userList = this.userListorig;

  }

  onUserEdit(companyId: string, id: string) {
    this.user.GetUserById(companyId, id, localStorage.getItem('userId')).subscribe(x => {
      var result = JSON.parse(x.content);
      this.myform.controls['companyid'].setValue(result[0].companyId);
      this.myform.controls['roleid'].setValue(result[0].RoleId);
      this.myform.controls['firstname'].setValue(result[0].firstName);
      this.myform.controls['lastname'].setValue(result[0].lastName)
      this.myform.controls['username'].setValue(result[0].userName)
      this.myform.controls['id'].setValue(result[0].userId)
      this.myform.controls['userid'].setValue(Number(localStorage.getItem('userId')))
      this.myform.controls['isactive'].setValue(result[0].IsActive);
      this.isChecked = result[0].IsActive
    })
  }

  Update() {
    console.log(this.myform.value);
    console.log('Update')
    this.user.UserSave(this.myform.value).subscribe(x => {
      var result = JSON.parse(x.content);
      if (result == 'success') {
        this.showerror('User details has been updated.')
        this.clear();
        this.GetUserList();
      }
      else {
        this.showerror(result);
      }
    }, error => { this.showerror(error.error.message) })
  }

  clear() {
    this.myform.controls['companyid'].setValue(0);
    this.myform.controls['roleid'].setValue(0);
    this.myform.controls['firstname'].setValue('');
    this.myform.controls['lastname'].setValue('')
    this.myform.controls['username'].setValue('')
    this.myform.controls['id'].setValue(0)
    this.myform.controls['isactive'].setValue(false);
    this.isChecked = false;
  }

  onGetDropdown() {
    this.getCompany().then(x => {
      this.companyList = x;
    })

    this.getRole().then(x => {
      this.roleList = x;
    })
  }

  showerror(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 3000);
  }

  async getRole(): Promise<any> {
    var data = await this.user.GetRole().toPromise();
    return JSON.parse(data.content);
  }

  async getCompany(): Promise<any> {
    var data = await this.user.GetCompany().toPromise();
    return JSON.parse(data.content);
  }

  back() {
    this.router.navigate(['/main']);
  }

  GetUserList(): void {
    this, this.user.GetUserById(localStorage.getItem('companyId'), '0', localStorage.getItem('userId')).subscribe(x => {
      var result = JSON.parse(x.content);
      this.userList = result;
      this.userListorig = result;
    });
  }
}
