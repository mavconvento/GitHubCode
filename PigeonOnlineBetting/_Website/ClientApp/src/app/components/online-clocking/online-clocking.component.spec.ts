import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineClockingComponent } from './online-clocking.component';

describe('OnlineClockingComponent', () => {
  let component: OnlineClockingComponent;
  let fixture: ComponentFixture<OnlineClockingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlineClockingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineClockingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
