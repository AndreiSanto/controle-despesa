import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { DropdownModule } from 'primeng/dropdown';

import {  MessageService } from 'primeng/api';
import { HttpClientModule } from '@angular/common/http';
import { TableLazyLoadEvent } from 'primeng/table';
import { ReceitaDTO } from '../../../models/dtos/receita.dto';
import { ReceitaService } from '../../../services/receita.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-list-receita',
  standalone: true,
  imports: [ CommonModule, FormsModule, TableModule, CalendarModule, InputTextModule, ButtonModule, CardModule, TagModule,DropdownModule,HttpClientModule],
  templateUrl: './list-receita.component.html',
  styleUrl: './list-receita.component.scss',
    providers: [MessageService,ReceitaService],
  
})
export class ListReceitaComponent {

  receitas: ReceitaDTO[] = [];
  totalRecords: number = 0;
  loading: boolean = true;
  first: number = 0;
  rows :number =10;

filtro = {
    descricao: '',
    dataVencimento: '',
    despesaFixa: '',
    numeroParcelas: 0,
    parcelado:true

  };

   tiposDespesa = [
    { label: 'Todas', value: '' },
    { label: 'Despesa Fixa', value: true },
    { label: 'Despesa Variável', value: false }
  ];

  constructor(private receitaService: ReceitaService,private messageService: MessageService, private router: Router) {}
  ngOnInit(): void {
    this.loadReceitas();
  }


loadReceitas(event?: TableLazyLoadEvent) {
  this.loading = true;

   this.first = event?.first ?? 0;
  this.rows = event?.rows ?? 10;

  const pagina = this.first /  this.rows + 1;
  const itensPorPagina =  this.rows;

 

  this.receitaService.ListarReceitas(pagina, itensPorPagina).subscribe({
    next: (res: any) => {
      this.receitas = res.resultado;
      this.totalRecords = res.totalItens;
      this.loading = false;
    },
     error: (err) => {
            this.loading = false;
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error.erro || 'Erro ao listar as receitas.' });
    }
  });
}


  buscar() {
    console.log('Filtro aplicado:', this.filtro);
    // Aqui você pode chamar seu service e filtrar as despesas
  }

 visualizar(receita: any) {
    this.router.navigate([`/app/visualizar-receita`, receita.id]);
  }

  editar(receita: any) {
    this.router.navigate([`/app/editar-receita`, receita.id]);
  }

  excluir(despesa: any) {
    this.receitaService.exclui(despesa.id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Receita excluída!' , life: 6000});
        this.loadReceitas();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao excluir Receita.' });
      }
    });
  }

}
