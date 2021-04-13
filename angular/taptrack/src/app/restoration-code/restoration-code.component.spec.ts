import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestorationCodeComponent } from './restoration-code.component';

describe('RestorationCodeComponent', () => {
  let component: RestorationCodeComponent;
  let fixture: ComponentFixture<RestorationCodeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestorationCodeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestorationCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
