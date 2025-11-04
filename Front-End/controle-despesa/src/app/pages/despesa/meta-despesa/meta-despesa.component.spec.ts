import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaDespesaComponent } from './meta-despesa.component';

describe('MetaDespesaComponent', () => {
  let component: MetaDespesaComponent;
  let fixture: ComponentFixture<MetaDespesaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MetaDespesaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MetaDespesaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
