import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-pedigree',
  templateUrl: './pedigree.component.html',
  styleUrls: ['./pedigree.component.css']
})
export class PedigreeComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      PigeonId: ['', Validators.required],
      BandNumber: ['', Validators.required],
      PigeonName: [''],
      Color: [''],
      EyeColor: [''],
      Gender: ['', Validators.required],
      TypeOfBreeding: [''],
      BackgroundColor: [''],
      Hen: [''],
      Cock: [''],
      Owener: [''],
      Line: [''],
      RemarksAchievement: [''],
      UserId: ['']
    });
  }
   

}
