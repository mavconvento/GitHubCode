import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagementMonitoringComponent } from './management-monitoring.component';

describe('ManagementMonitoringComponent', () => {
  let component: ManagementMonitoringComponent;
  let fixture: ComponentFixture<ManagementMonitoringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagementMonitoringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagementMonitoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
