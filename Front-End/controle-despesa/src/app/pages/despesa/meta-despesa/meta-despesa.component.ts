import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { MetaDespesaService } from '../../../services/metaDespesa.service';
import { Meta } from '@angular/platform-browser';
import { MetaDespesaDTO } from '../../../models/dtos/meta-despesa.dto';
import { FormType } from '../../../shared/enums/form-type.enum';

@Component({
  selector: 'app-meta-despesa',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DropdownModule,
    InputNumberModule,
    ProgressBarModule,
    ToastModule
  ],
  templateUrl: './meta-despesa.component.html',
  styleUrls: ['./meta-despesa.component.scss'],
  providers: [MessageService]
})
export class MetaDespesaComponent implements OnInit {

  meta: MetaDespesaDTO = {
    mes: 0,
    ano: new Date().getFullYear(),
    valor: 0,
    ativo: true
  };

public FormTypeEnum = FormType; 
public formType: FormType = FormType.View; // guarda o valor atual



  progresso = 0;
  totalGasto = 0; // simulando gasto atual

  meses = [
    { label: 'Janeiro', value: 1 },
    { label: 'Fevereiro', value: 2 },
    { label: 'Março', value: 3 },
    { label: 'Abril', value: 4 },
    { label: 'Maio', value: 5 },
    { label: 'Junho', value: 6 },
    { label: 'Julho', value: 7 },
    { label: 'Agosto', value: 8 },
    { label: 'Setembro', value: 9 },
    { label: 'Outubro', value: 10 },
    { label: 'Novembro', value: 11 },
    { label: 'Dezembro', value: 12 }
  ];

  constructor(private messageService: MessageService, private router: Router, private metaService: MetaDespesaService) { }

  ngOnInit(): void {
    this.calcularProgresso();
    this.loadData();
    this.loadMetaDespesa();
  }

  calcularProgresso() {
    if (this.meta.valor > 0) {
      this.progresso = Math.min((this.totalGasto / this.meta.valor) * 100, 100);
    } else {
      this.progresso = 0;
    }
  }

loadMetaDespesa() {
  this.metaService.buscarMetaDespesa().subscribe({
    next: (data) => {
      this.meta = data;
      this.formType = FormType.Update; 
    },
    error: (err) => {
      if (err.status === 404) {
       
        this.formType = FormType.Create;
      } 
    }
  });
}
 

  // Simula aumento do gasto atual (para testar)
  simularGasto(valor: number) {
    this.totalGasto += valor;
    this.calcularProgresso();
  }

salvar() {
  if (!this.meta.mes || !this.meta.ano || this.meta.valor <= 0) {
    this.messageService.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Preencha todos os campos corretamente.'
    });
    return;
  }

  const request$ =
    this.formType === FormType.Create
      ? this.metaService.cadastro(this.meta)
      : this.metaService.alterar(this.meta); 

  request$.subscribe({
    next: () => {
      const msg = this.formType === FormType.Create ? 'Meta criada!' : 'Meta atualizada!';
      
      this.messageService.add({
        severity: 'success',
        summary: 'Sucesso',
        detail: msg
      });

     
    },
    error: (err) => {
      this.messageService.add({
        severity: 'error',
        summary: 'Erro',
        detail: err?.error?.erro || 'Ocorreu um erro.'
      });
    }
  });
}


  loadData() {
    const hoje = new Date();
    const mesAtual = hoje.getMonth() + 1;
    const anoAtual = hoje.getFullYear();

    this.meta.mes = mesAtual;
    this.meta.ano = anoAtual;

  }


  voltar() {
    this.router.navigate(['/app/lista-despesa']);
  }
}
