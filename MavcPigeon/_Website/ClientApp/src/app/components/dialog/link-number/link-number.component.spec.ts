import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkNumberDialogComponent } from './link-number.component';

describe('LinkNumberComponent', () => {
  let component: LinkNumberDialogComponent;
  let fixture: ComponentFixture<LinkNumberDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LinkNumberDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkNumberDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
