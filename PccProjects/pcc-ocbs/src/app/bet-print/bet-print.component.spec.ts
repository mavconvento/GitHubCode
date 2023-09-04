import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BetPrintComponent } from './bet-print.component';

describe('BetPrintComponent', () => {
  let component: BetPrintComponent;
  let fixture: ComponentFixture<BetPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BetPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BetPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
