import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnClaimedTicketComponent } from './un-claimed-ticket.component';

describe('UnClaimedTicketComponent', () => {
  let component: UnClaimedTicketComponent;
  let fixture: ComponentFixture<UnClaimedTicketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnClaimedTicketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnClaimedTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
