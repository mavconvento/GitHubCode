import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberDistanceComponent } from './member-distance.component';

describe('MemberDistanceComponent', () => {
  let component: MemberDistanceComponent;
  let fixture: ComponentFixture<MemberDistanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemberDistanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberDistanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
