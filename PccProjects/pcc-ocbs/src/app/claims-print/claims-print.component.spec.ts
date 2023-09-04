import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimsPrintComponent } from './claims-print.component';

describe('ClaimsPrintComponent', () => {
  let component: ClaimsPrintComponent;
  let fixture: ComponentFixture<ClaimsPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClaimsPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimsPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
