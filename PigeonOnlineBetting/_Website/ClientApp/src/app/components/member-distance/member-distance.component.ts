import { JsonPipe } from '@angular/common';
import { CommentStmt } from '@angular/compiler';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { Helpers } from '../../helpers/helpers';
import { Distance } from '../../models/distance';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-member-distance',
  templateUrl: './member-distance.component.html',
  styleUrls: ['./member-distance.component.css']
})
export class MemberDistanceComponent implements OnInit {
  form: FormGroup;
  isNorth: boolean;
  isSouth: boolean;
  distanceCollectionNorth: Distance[];
  distanceCollectionSouth: Distance[];
  name: string;
  coordinates: string;
  clubname: string;
  dbname: string;
  memberid: string;

  //race result data source
  dataSource: MatTableDataSource<Distance>;
  dataSource2: MatTableDataSource<Distance>;
  displayedColumns: string[] = ["ReleasePoint", "Coordinates", "Distance"];


  //@ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild('paginator', { static: false }) paginator: MatPaginator;
  @ViewChild('paginator2', { static: false }) paginator2: MatPaginator;
  @ViewChild(MatSort, { static: false }) set sort(sort: MatSort) {
    this.dataSource.sort = sort;
    this.dataSource2.sort = sort;
  };

  length: number = 0;
  pageIndex: number = 0;
  pageSize: number = 20;
  pageSizeOptions: number[] = [20, 30, 50, 100];

  length2: number = 0;
  pageIndex2: number = 0;
  pageSize2: number = 20;
  pageSizeOptions2: number[] = [20, 30, 50, 100];

  constructor(
    private fb: FormBuilder,
    private helper: Helpers,
    private activatedRoute: ActivatedRoute,
    private userService: UserService
  ) {
   
    this.dataSource = new MatTableDataSource(this.distanceCollectionNorth);
    this.dataSource2 = new MatTableDataSource(this.distanceCollectionSouth);
  }

  changeView(option) {
    if (option.value == "North") {
      this.isNorth = true;
      this.isSouth = false;
    }

    else if (option.value == "South") {
      this.isNorth = false;
      this.isSouth = true;
      
    }

  }

  ngOnInit() {
    this.form = this.fb.group({
      distance: ['North', Validators.required]
    });
    this.clubname = this.activatedRoute.snapshot.params.clubname;
    this.dbname = this.activatedRoute.snapshot.params.dbname;
    this.memberid = this.activatedRoute.snapshot.params.memberidno; 

    this.GetDistance(this.clubname, this.memberid, this.dbname);

    this.isNorth = true;
    this.isSouth = false;
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource2.paginator = this.paginator2;
    this.dataSource.sort = this.sort;
  }

  getNext(event: PageEvent) {
    this.length = event.length;
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  getNext2(event: PageEvent) {
    this.length2 = event.length;
    this.pageIndex2 = event.pageIndex;
    this.pageSize2 = event.pageSize;
  }


  GetDistance(clubname: string, memberid: string, dbname: string) {
    this.userService.getMemberDistance(clubname, memberid, dbname).subscribe(data => {
      var result = JSON.parse(data.content);
      console.log(result.Table);

      if (result.Table.length > 0) {
        this.name = result.Table[0].Name;
        this.coordinates = result.Table[0].Coordinates;
      }
      
      this.distanceCollectionNorth = result.Table1;
      this.distanceCollectionSouth = result.Table2;
      this.dataSource.data = this.distanceCollectionNorth;
      this.dataSource2.data = this.distanceCollectionSouth;
      this.length = this.distanceCollectionNorth.length;
      this.length2 = this.distanceCollectionSouth.length;
    });
  }

  viewDistanceMap() {
    console.log(this.memberid);
    console.log(this.clubname);
    console.log(this.dbname);
    this.userService.getMemberCoordinates(this.memberid, this.clubname, this.dbname).subscribe(data => {
      var result = JSON.parse(data.content);
      if (result.Table.length > 0) {
        window.open('https://www.google.com/maps/place/' + result.Table[0].Lat + ' ' + result.Table[0].Long, '_blank');
      }
      console.log((JSON.parse(data.content)).Table)
    });
  }

  get f() { return this.form.controls; }


}
