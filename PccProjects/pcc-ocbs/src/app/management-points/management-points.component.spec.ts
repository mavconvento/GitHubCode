import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagementPointsComponent } from './management-points.component';

describe('ManagementPointsComponent', () => {
  let component: ManagementPointsComponent;
  let fixture: ComponentFixture<ManagementPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ManagementPointsComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagementPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
