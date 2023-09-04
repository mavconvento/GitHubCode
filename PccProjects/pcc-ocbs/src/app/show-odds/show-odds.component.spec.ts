import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowOddsComponent } from './show-odds.component';

describe('ShowOddsComponent', () => {
  let component: ShowOddsComponent;
  let fixture: ComponentFixture<ShowOddsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowOddsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowOddsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
