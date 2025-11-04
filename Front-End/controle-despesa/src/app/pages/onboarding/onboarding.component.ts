import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CarouselModule } from 'primeng/carousel';


@Component({
  selector: 'app-onboarding',
  standalone: true,
  imports: [CarouselModule],
  templateUrl: './onboarding.component.html',
  styleUrl: './onboarding.component.scss'
})
export class OnboardingComponent {

  slides = [
    { title: 'Bem-vindo!', text: 'Vamos começar a usar o sistema.' },
    { title: 'Gerencie facilmente', text: 'Controle tudo com poucos cliques.' },
    { title: 'Pronto!', text: 'Vamos acessar o painel.' }
  ];

  constructor(private router: Router) {}

  goToApp() {
    this.router.navigate(['/app/dashboard']);
  }
}
