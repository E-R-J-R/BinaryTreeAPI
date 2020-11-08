import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForcedmatrixComponent } from './forcedmatrix.component';

describe('ForcedmatrixComponent', () => {
  let component: ForcedmatrixComponent;
  let fixture: ComponentFixture<ForcedmatrixComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForcedmatrixComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ForcedmatrixComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
