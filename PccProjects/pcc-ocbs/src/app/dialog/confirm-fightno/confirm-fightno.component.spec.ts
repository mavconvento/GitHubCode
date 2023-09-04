import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmFightnoComponent } from './confirm-fightno.component';

describe('ConfirmFightnoComponent', () => {
  let component: ConfirmFightnoComponent;
  let fixture: ComponentFixture<ConfirmFightnoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmFightnoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmFightnoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
