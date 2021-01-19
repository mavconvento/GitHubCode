import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkNumberComponent } from './link-number.component';

describe('LinkNumberComponent', () => {
  let component: LinkNumberComponent;
  let fixture: ComponentFixture<LinkNumberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkNumberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkNumberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
