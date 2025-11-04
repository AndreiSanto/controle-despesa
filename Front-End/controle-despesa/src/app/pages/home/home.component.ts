import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioDTO } from '../../models/dtos/usuario.dto';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms'; // 👈 importa aqui
import { ReactiveFormsModule } from '@angular/forms'; // 👈 importa aqui
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { UsuarioService } from '../../services/usuario.service';
import { HttpClientModule } from '@angular/common/http'; // 👈 Import necessário


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, ToastModule,HttpClientModule],
  providers: [MessageService,UsuarioService], // 👈 aqui

  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  usuario: UsuarioDTO = {
    id: 0,
    nome: '',
    email: '',
    password: ''
  };

  form!: FormGroup;

  constructor(private router: Router, private messageService: MessageService,private usuarioService:UsuarioService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      nome: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });
  }

  cadastrar() {
    if (this.form.valid) {

      this.usuarioService.cadastro(this.form.value).subscribe({
        next: (usuario) => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Usuário cadastrado!' });
         this.limparCampos();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: err.error.message || 'Erro ao cadastrar usuário.' });
          console.error(err);
        }
      });




    } else {
      const erros: string[] = [];

      Object.keys(this.form.controls).forEach(key => {
        const control = this.form.get(key);
        if (control && control.invalid) {
          if (control.errors?.['required']) erros.push(`${this.getNomeCampo(key)} é obrigatório`);
          if (control.errors?.['email']) erros.push(`${this.getNomeCampo(key)} tem formato inválido`);
          if (control.errors?.['minlength'])
            erros.push(`${this.getNomeCampo(key)} deve ter no mínimo ${control.errors['minlength'].requiredLength} caracteres`);
        }
      });

      // Exibe os erros usando o MessageService do PrimeNG
      this.messageService.add({
        severity: 'error',
        summary: 'Erros no formulário',
        detail: erros.join('; '), life: 5000
      });

      this.form.markAllAsTouched();
    }
  }

  getNomeCampo(campo: string): string {
    switch (campo) {
      case 'nome': return 'Nome';
      case 'email': return 'E-mail';
      case 'password': return 'Senha';
      default: return campo;
    }
  }

  limparCampos(){

    this.form.reset();
  }

  Entrar(){

    this.router.navigate(['/onboarding']);
  }
}