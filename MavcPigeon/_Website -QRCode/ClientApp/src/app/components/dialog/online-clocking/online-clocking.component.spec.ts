import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineClockingDialogComponent } from './online-clocking.component';

describe('OnlineClockingDialogComponent', () => {
  let component: OnlineClockingDialogComponent;
  let fixture: ComponentFixture<OnlineClockingDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OnlineClockingDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineClockingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
