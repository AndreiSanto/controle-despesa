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
import { DespesaDTO } from '../../../models/dtos/despesa.dto';
import { DespesaService } from '../../../services/despesa.service';
import { MessageService } from 'primeng/api';
import { HttpClientModule } from '@angular/common/http';
import { TableLazyLoadEvent } from 'primeng/table';
import { Router } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { FiltroDTO } from '../../../models/dtos/filtro.dto';
import { DespesaListaResponse } from '../../../models/responses/despesa-lista-response';

@Component({
  selector: 'app-list-despesa',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, CalendarModule,
    InputTextModule, ButtonModule, CardModule, TagModule, DropdownModule, HttpClientModule,
    ToastModule
  ],
  templateUrl: './list-despesa.component.html',
  styleUrl: './list-despesa.component.scss',
  providers: [MessageService, DespesaService],

})
export class ListDespesaComponent implements OnInit {

  despesas: DespesaListaResponse[] = [];
  totalRecords: number = 0;
  loading: boolean = true;
  first: number = 0;
  rows: number = 10;

  filtro: FiltroDTO = {
    descricao: '',
    dataCadastroInicial: null,
    dataCadastroFinal: null
  };
  tiposDespesa = [
    { label: 'Todas', value: '' },
    { label: 'Despesa Fixa', value: true },
    { label: 'Despesa Variável', value: false }
  ];

  constructor(private despesaService: DespesaService, private messageService: MessageService, private router: Router) { }
  ngOnInit(): void {
    this.loadDespesas();
  }


  loadDespesas(event?: TableLazyLoadEvent) {
    this.loading = true;

    this.first = event?.first ?? 0;
    this.rows = event?.rows ?? 10;

    const pagina = this.first / this.rows + 1;
    const itensPorPagina = this.rows;



    this.despesaService.ListarDespesas(this.filtro, pagina, itensPorPagina).subscribe({
      next: (res: any) => {
        this.despesas = res.resultado;
        this.totalRecords = res.totalItens;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error.erro || 'Erro ao listar  despesa.' });
      }
    });
  }


  buscar(event?: TableLazyLoadEvent) {
    this.loading = true;

    this.first = event?.first ?? 0;
    this.rows = event?.rows ?? 10;

    const pagina = this.first / this.rows + 1;
    const itensPorPagina = this.rows;

   

    this.despesaService.ListarDespesas(this.filtro,pagina, itensPorPagina).subscribe({
      next: (res: any) => {
        this.despesas = res.resultado;
        this.totalRecords = res.totalItens;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error.erro || 'Erro ao listar  despesa.' });
      }
    });
  }



  visualizar(despesa: any) {
    this.router.navigate([`/app/visualizar-despesa`, despesa.id]);
  }

  editar(despesa: any) {
    this.router.navigate([`/app/editar-despesa`, despesa.id]);
  }

  excluir(despesa: any) {
    this.despesaService.exclui(despesa.id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Despesa excluída!', life: 6000 });
        this.loadDespesas();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao excluir despesa.' });
      }
    });
  }
  limpar() {
  this.filtro = {
    descricao: '',
    dataCadastroInicial: null,
    dataCadastroFinal: null
  };

  this.first = 0; // volta paginação para o início
  this.loadDespesas(); // recarrega lista sem filtro
}


}
