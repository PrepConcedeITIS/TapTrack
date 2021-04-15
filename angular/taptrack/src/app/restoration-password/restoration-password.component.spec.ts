import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestorationPasswordComponent } from './restoration-password.component';

describe('RestorationPasswordComponent', () => {
  let component: RestorationPasswordComponent;
  let fixture: ComponentFixture<RestorationPasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestorationPasswordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestorationPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
