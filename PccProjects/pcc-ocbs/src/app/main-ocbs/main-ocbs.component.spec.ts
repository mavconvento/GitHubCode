import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainOcbsComponent } from './main-ocbs.component';

describe('MainOcbsComponent', () => {
  let component: MainOcbsComponent;
  let fixture: ComponentFixture<MainOcbsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainOcbsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainOcbsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
