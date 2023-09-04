import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagementFightComponent } from './management-fight.component';

describe('ManagementFightComponent', () => {
  let component: ManagementFightComponent;
  let fixture: ComponentFixture<ManagementFightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagementFightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagementFightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
