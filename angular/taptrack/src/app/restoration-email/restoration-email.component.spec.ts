import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestorationEmailComponent } from './restoration-email.component';

describe('RestorationEmailComponent', () => {
  let component: RestorationEmailComponent;
  let fixture: ComponentFixture<RestorationEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RestorationEmailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RestorationEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
