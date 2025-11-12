import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule,ToastModule],
  providers: [MessageService], // 👈 aqui

  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  erro = '';
  constructor(private router: Router, private authService: AuthService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6),])

    });
  }

  login() {
  if (this.form.valid) {
    const email = this.form.get('email')?.value;
    const senha = this.form.get('password')?.value;
    this.authService.login(email, senha).subscribe({
      next: (res) => {
        // Mensagem de sucesso
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Login realizado com sucesso!'
        });

      
        this.router.navigate(['/onboarding']);
      },
      error: (err) => {
       
        this.erro = 'Usuário ou senha inválidos';
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err.error?.message || 'Erro ao realizar login.'
        });
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

   
    this.messageService.add({
      severity: 'error',
      summary: 'Erros no formulário',
      detail: erros.join('; '),
      life: 5000
    });

    this.form.markAllAsTouched();
  }
}



  getNomeCampo(campo: string): string {
    switch (campo) {

      case 'email': return 'E-mail';
      case 'password': return 'Senha';
      default: return campo;
    }
  }

}
