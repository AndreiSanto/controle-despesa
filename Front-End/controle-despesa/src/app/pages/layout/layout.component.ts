import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem, Message } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { BadgeModule } from 'primeng/badge';


@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [MenubarModule, ButtonModule, MessageModule, ToastModule,BadgeModule],
  providers: [MessageService],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {
  menuItems: MenuItem[];
  messages: Message[] | undefined;


  constructor(private router: Router, private messageService: MessageService) {
    this.menuItems = [
      {
        label: 'Dashboard',
        icon: 'pi pi-home',
        routerLink: '/app/dashboard'
      },
      {
        label: 'Relatórios',
        icon: 'pi pi-chart-bar',
        items: [
          { label: 'Mensal', command: () => this.naoImplementado(), },
          { label: 'Anual', command: () => this.naoImplementado(), }
        ]
      },
      {
        label: 'Despesas',
       icon: 'pi pi-wallet',
        items: [
          { label: 'Cadastrar Despesa', routerLink: '/app/cadastro-despesa' },
          { label: 'Listagem de Despesas', routerLink: '/app/lista-despesa', },
          { label: 'Cadastro de Meta Despesa', routerLink: '/app/meta-despesa', }
        ]
      },

      {
        label: 'Receitas',
        icon: 'pi pi-money-bill',
        items: [
          { label: 'Cadastrar Receita', routerLink: '/app/cadastro-receita' },
          { label: 'Listagem de Receitas', routerLink: '/app/lista-receita', }
        ]
      },
      {
        label: 'Notificação',
        icon: 'pi pi-bell',
        badge: '8',
        
      },
    ];
  }
  naoImplementado() {
    this.messageService.add({
      severity: 'warn',
      summary: 'Aviso',
      detail: 'Funcionalidade ainda não implementada!'
    });
  }

  logout() {
    this.router.navigate(['/login']);
  }
}