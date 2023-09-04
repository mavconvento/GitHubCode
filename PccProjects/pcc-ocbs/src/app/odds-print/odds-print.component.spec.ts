import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OddsPrintComponent } from './odds-print.component';

describe('OddsPrintComponent', () => {
  let component: OddsPrintComponent;
  let fixture: ComponentFixture<OddsPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OddsPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OddsPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
