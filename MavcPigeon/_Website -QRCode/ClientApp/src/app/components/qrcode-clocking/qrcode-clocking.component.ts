
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
import { RaceService } from 'src/app/services/race.service';
import { ActivatedRoute } from '@angular/router';
import StringBuilder from '@zxing/library/esm/core/util/StringBuilder';

@Component({
  selector: 'app-qrcode',
  templateUrl: './qrcode-clocking.component.html',
})
export class QRCodeClockingComponent implements OnInit {
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
  load:string;

  constructor(
    private alertService: AlertService,
    private raceService: RaceService,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      let a = params.c;
      this.raceService.qrCodeClocking(a).subscribe(result => {
        var data = JSON.parse(result.content);
        if (data[0].remarks == 'success') {
          this.clubname = data[0].clubname;
          this.name = data[0].name;
          this.clubname = data[0].clubname;
          this.birdcount = data[0].birdcount;
          this.ringnumber = data[0].ringnumber;
          this.liberation = data[0].liberation;
          this.code = data[0].code;
          this.timearrival = data[0].timearrival;
          this.fight = data[0].fight;
          this.speed = data[0].speed;
          this.load = data[1].load;
          //this.remarks = data[0].remarks;
        }
        else {
          this.remarks = data[0].remarks;
        }
      }, error => { this.alertService.errorNotification(error) });
    });
  }
}
