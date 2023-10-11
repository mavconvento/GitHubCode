import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MoneyCounterComponent } from './money-counter.component';

describe('MoneyCounterComponent', () => {
  let component: MoneyCounterComponent;
  let fixture: ComponentFixture<MoneyCounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoneyCounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoneyCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
