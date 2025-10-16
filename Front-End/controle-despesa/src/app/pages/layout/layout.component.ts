import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [MenubarModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {
menuItems: MenuItem[] = [
    {
      label: 'Dashboard',
      icon: 'pi pi-home',
      routerLink: '/app/dashboard'
    },
    {
      label: 'Cadastros',
      icon: 'pi pi-folder',
      items: [
        {
          label: 'Usuários',
          icon: 'pi pi-user',
          routerLink: '/app/usuarios'
        },
        {
          label: 'Produtos',
          icon: 'pi pi-box',
          routerLink: '/app/produtos'
        },
        {
          label: 'Categorias',
          icon: 'pi pi-tags',
          routerLink: '/app/categorias'
        }
      ]
    },
    {
      label: 'Relatórios',
      icon: 'pi pi-chart-bar',
      items: [
        {
          label: 'Mensal',
          icon: 'pi pi-calendar',
          routerLink: '/app/relatorios/mensal'
        },
        {
          label: 'Anual',
          icon: 'pi pi-chart-line',
          routerLink: '/app/relatorios/anual'
        }
      ]
    },
    { separator: true },
  { label: 'Sair', icon: 'pi pi-sign-out', routerLink: '/login', styleClass: 'menu-right' }
  ];
}
