import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListReceitaComponent } from './list-receita.component';

describe('ListReceitaComponent', () => {
  let component: ListReceitaComponent;
  let fixture: ComponentFixture<ListReceitaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListReceitaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListReceitaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
