import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PccOcbsComponent } from './pcc-ocbs.component';

describe('PccOcbsComponent', () => {
  let component: PccOcbsComponent;
  let fixture: ComponentFixture<PccOcbsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PccOcbsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PccOcbsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
