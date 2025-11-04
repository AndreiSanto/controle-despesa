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
import { ReceitaDTO } from '../../../models/dtos/receita.dto';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { ReceitaService } from '../../../services/receita.service';
import { HttpClientModule } from '@angular/common/http';
import { TipoDespesaReceitaResponse } from '../../../models/responses/tipo-despesa-receita-response';
import { ActivatedRoute, Router } from '@angular/router';
import { FormType } from '../../../shared/enums/form-type.enum';

@Component({
  selector: 'app-form-receita',
  standalone: true,
  imports: [ButtonModule, CardModule, CheckboxModule, CommonModule, CalendarModule, DropdownModule, FormsModule, InputNumberModule, InputTextModule, ToastModule,
    DialogModule, HttpClientModule],
  providers: [MessageService, ReceitaService],

  templateUrl: './form-receita.component.html',
  styleUrl: './form-receita.component.scss'
})
export class FormReceitaComponent implements OnInit {

  constructor(private messageService: MessageService, private receitaService: ReceitaService, private router: Router, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.tituloPaginas();
    this.loadDropdownData();

    this.formType = this.route.snapshot.data['formType'];
    this.id = this.route.snapshot.params['id'];

    if ((this.formType === FormType.Update || this.formType === FormType.View) && this.id) {
      this.carregarReceita(this.id);
    }

  }

  receita: ReceitaDTO = {
    id: 0,
    descricao: '',
    receitaFixa: false,
    valor: 0,
    dataCadastro: new Date(),
    tipoDespesaReceitaId: null
  };

  tipoReceitas: TipoDespesaReceitaResponse[] = [];

  displayModal: boolean = false;

  formType!: FormType;
  id?: number;
  tituloPagina: string = '';

  public FormType = FormType;


  salvar() {

if (this.formType === FormType.View) return;

    if (!this.validarDados(this.receita)) return;

    if (this.formType === FormType.Create) {
      this.receitaService.cadastro(this.receita).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Receita cadastrada!' });
          this.limparCampos();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao cadastrar receita.' });
        }
      });
    } else if (this.formType === FormType.Update) {
      this.receitaService.atualizar(this.receita).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Receita atualizada!' });
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error || 'Erro ao atualizar receita.' });
        }
      });
    }
  }


   


  

  validarDados(receita: ReceitaDTO): boolean {
    if (!receita.descricao || receita.descricao.trim().length === 0) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'A descrição é obrigatória.', life: 5000 });
      return false;
    }

    if (receita.valor <= 0) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'O valor deve ser maior que zero.', life: 5000 });
      return false;
    }

    return true;
  }


  loadDropdownData() {
    this.receitaService.ListarReceitasCategoria().subscribe({
      next: (data) => {
        this.tipoReceitas = data;
      },
      error: (err) => {
        console.error('Erro ao carregar receitas:', err);
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
    this.router.navigate(['/app/lista-receita']);
  }



  onReceitaChange() {
    if (this.receita.receitaFixa) {
      this.displayModal = true;
    }
  }

  fecharModal() {
    this.displayModal = false;
  }

  carregarReceita(id: number) {
    this.receitaService.obterPorId(id).subscribe({
      next: (data) => {
        this.receita = data;
        this.receita.dataCadastro = new Date(this.receita.dataCadastro);

      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Não foi possível carregar a despesa.' });
      }
    });
  }


    limparCampos() {
    this.receita = {
      id: 0,
      descricao: '',
      valor: 0,
      dataCadastro: new Date(),
      
      
      receitaFixa: false,
      tipoDespesaReceitaId: null
    };
      
  }
}
