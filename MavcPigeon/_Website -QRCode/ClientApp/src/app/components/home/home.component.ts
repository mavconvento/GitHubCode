
import { AfterViewInit, Component, OnInit } from '@angular/core';
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
import { RaceService } from 'src/app/services/race.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, AfterViewInit {
  liberation: string;
  releasedate: string;
  name: string;
  birdcount: string;
  ringnumber: string;
  code: string;
  timearrival: string;
  fight: string;
  speed: string;
  remarks: string;
  clubname: string;
  load: string;
  distance: string;
  showDetails: boolean = false;
  showRemarks: boolean = false;

  constructor(
    private alertService: AlertService,
    private raceService: RaceService,
    private route: ActivatedRoute) {
  }
  ngAfterViewInit(): void {
    this.route.queryParams.subscribe(params => {
      let a = params.c;
      this.raceService.qrCodeClocking(a).subscribe(result => {
        var data = JSON.parse(result.content);
        console.log(data);
        if (data.Table[0].remarks == 'success') {
          this.clubname = data.Table[0].clubname;
          this.name = data.Table[0].name;
          this.clubname = data.Table[0].clubname;
          this.birdcount = data.Table[0].birdcount;
          this.ringnumber = data.Table[0].ringnumber;
          this.liberation = data.Table[0].liberation;
          this.code = data.Table[0].code;
          this.timearrival = data.Table[0].timearrival;
          this.fight = data.Table[0].fight;
          this.speed = data.Table[0].speed;
          this.distance = data.Table[0].distance;
          this.load = data.Table1[0].Load;

          this.showDetails = true;
          //this.remarks = data[0].remarks;
        }
        else {
          this.remarks = data.Table[0].Remarks;
          this.showRemarks = true;
          this.showDetails = false;
        }
      }, error => { this.alertService.errorNotification(error) });
    });
  }

  ngOnInit() {

  }
}
