import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';

import { DespesaService } from '../../../services/despesa.service';
import { TipoDespesaReceitaDTO } from '../../../models/dtos/tipoDespesaReceita.dto';
import { DespesaDTO } from '../../../models/dtos/despesa.dto';
import { TipoDespesaReceitaResponse } from '../../../models/responses/tipo-despesa-receita-response';
import { FormType } from '../../../shared/enums/form-type.enum';


@Component({
  selector: 'app-form-despesa',
  standalone: true,
  imports: [
    FormsModule, InputTextModule, InputNumberModule, CalendarModule,
    CheckboxModule, DropdownModule, ButtonModule, CardModule,
    CommonModule, HttpClientModule, ToastModule
  ],
  templateUrl: './form-despesa.component.html',
  styleUrls: ['./form-despesa.component.scss'],
  providers: [MessageService, DespesaService],
})
export class FormDespesaComponent implements OnInit {

  constructor(
    private despesaService: DespesaService,
    private messageService: MessageService,
    private route: ActivatedRoute,
    private router: Router 

  ) { }

  despesa: DespesaDTO = {
    id: 0,
    descricao: '',
    valorDespesa: null,
    dataCadastro: new Date(),
    dataDespesa: new Date(),
    numeroDeParcela: 0,
    parcelado: false,
    despesaFixa: false,
    tipoDespesaReceitaId: null,
    usuarioId:0
  };

  tipoDespesa: TipoDespesaReceitaResponse[] = [];
  formType!: FormType;
  id?: number;
  tituloPagina: string = '';

  public FormType = FormType;

  ngOnInit(): void {
    this.loadTiposDespesa();
    this.tituloPaginas();

    this.formType = this.route.snapshot.data['formType'];
    this.id = this.route.snapshot.params['id'];

    if ((this.formType === FormType.Update || this.formType === FormType.View) && this.id) {
      this.carregarDespesa(this.id);
    }
  }

  carregarDespesa(id: number) {
    this.despesaService.obterPorId(id).subscribe({
      next: (data) => {
        this.despesa = data;
        this.despesa.dataCadastro = new Date(this.despesa.dataCadastro);
        this.despesa.dataDespesa = new Date(this.despesa.dataDespesa);
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Não foi possível carregar a despesa.' });
      }
    });
  }

  salvar() {
    if (this.formType === FormType.View) return;

    if (!this.validarDados(this.despesa)) return;

    if (this.formType === FormType.Create) {
      this.despesaService.cadastro(this.despesa).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Despesa cadastrada!' });
          this.limparCampos();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao cadastrar despesa.' });
        }
      });
    } else if (this.formType === FormType.Update) {
      this.despesaService.atualizar(this.despesa).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Despesa atualizada!' });
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao atualizar despesa.' });
        }
      });
    }
  }

  validarDados(despesa: DespesaDTO): boolean {
    if (!despesa.descricao || despesa.descricao.trim().length === 0) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'A descrição é obrigatória.' });
      return false;
    }
    if (this.despesa.valorDespesa == null) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'O valor Não foi informado.' });
      return false;
    }
     if (this.despesa.valorDespesa <= 0) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'O valor deve ser maior que zero.' });
      return false;
    }
    if (!despesa.tipoDespesaReceitaId) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'O tipo de despesa é obrigatório.' });
      return false;
    }
    if (despesa.parcelado && despesa.numeroDeParcela <= 0) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'O número de parcelas deve ser maior que zero.' });
      return false;
    }
    if (despesa.dataDespesa < despesa.dataCadastro) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'A data de vencimento não pode ser anterior à data de cadastro.' });
      return false;
    }
    return true;
  }

  limparCampos() {
    this.despesa = {
      id: 0,
      descricao: '',
      valorDespesa: 0,
      dataCadastro: new Date(),
      dataDespesa: new Date(),
      numeroDeParcela: 0,
      parcelado: false,
      despesaFixa: false,
      tipoDespesaReceitaId: null,
      usuarioId:0
    };
  }

  loadTiposDespesa() {
    this.despesaService.ListarCategoriaDespesa().subscribe({
      next: (tipos) => (this.tipoDespesa = tipos),
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao carregar tipos de despesa.' });
      }
    });
  }

  get isViewMode(): boolean {
    return this.formType === FormType.View;
  }

tituloPaginas() {
  const titulos = {
    [FormType.Create]: 'Cadastro de Despesa',
    [FormType.Update]: 'Edição de Despesa',
    [FormType.View]: 'Visualização de Despesa'
  };

  this.tituloPagina = titulos[this.formType] || '';
}

voltar() {
  this.router.navigate(['/app/lista-despesa']);
}


}
