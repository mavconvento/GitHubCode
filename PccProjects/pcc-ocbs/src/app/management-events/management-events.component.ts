import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BettingService } from '../services/betting.services';
import { EventsService } from '../services/event.services';


@Component({
  selector: 'app-management-events',
  templateUrl: './management-events.component.html',
  styleUrls: ['./management-events.component.css']
})
export class ManagementEventsComponent implements OnInit {
  myform: FormGroup;
  errorMessage: string;
  isShow: boolean = false;
  eventList: Array<any>;

  //table colums
  displayedColumns: string[] = ['eventid', 'description', 'edit'];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    //private betting: BettingService,
    private event: EventsService
  ) { }

  ngOnInit() {
    this.myform = this.formBuilder.group({
      event_name: ['', Validators.required],
      eventId: [0, Validators.required],
      isoffline: [false, Validators.required],
      userId: ['', Validators.required],
      platformUserId: ['', Validators.required]
    });

    this.GetEventList();
  }

  back() {
    this.router.navigate(['/main']);
  }

  showError(message: string) {
    this.errorMessage = message;
    this.isShow = true;
    setTimeout(() => {
      this.errorMessage = "";
      this.isShow = false;
    }, 2000);
  }

  onEventEdit(eventId: number) {
    this.event.GetEventById(eventId).subscribe(x => {
      var result = JSON.parse(x.content);
      this.myform.controls['eventId'].setValue(result[0].EventId)
      this.myform.controls['event_name'].setValue(result[0].Description)
    })
  }

  GetEventList() {
    this.event.GetEventById(0).subscribe(x => {
      var result = JSON.parse(x.content);
      this.eventList = result;
    })
  }

  EventSave() {
    this.myform.controls["isoffline"].setValue(Boolean(localStorage.getItem("IsOffline")));
    this.myform.controls["userId"].setValue(localStorage.getItem("userId"));
    this.event.EventOfflineSave(this.myform.value).subscribe(x => {
      this.isShow = true;
      this.GetEventList();
      this.showError("Event Save.");
    });
  }
}
