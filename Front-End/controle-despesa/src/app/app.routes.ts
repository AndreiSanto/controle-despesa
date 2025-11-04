import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { OnboardingComponent } from './pages/onboarding/onboarding.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FormDespesaComponent } from './pages/despesa/form-despesa/form-despesa.component';
import { ListDespesaComponent } from './pages/despesa/list-despesa/list-despesa.component';
import { FormReceitaComponent } from './pages/receita/form-receita/form-receita.component';
import { ListReceitaComponent } from './pages/receita/list-receita/list-receita.component';
import { FormType } from './shared/enums/form-type.enum';
import { LayoutComponent } from './pages/layout/layout.component';
import { MetaDespesaComponent } from './pages/despesa/meta-despesa/meta-despesa.component';


export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'onboarding', component: OnboardingComponent },
  {
    path: 'app',
    component: LayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },

      // DESPESA
      { 
        path: 'cadastro-despesa', 
        component: FormDespesaComponent,
        data: { formType: FormType.Create }
      },
      { 
        path: 'editar-despesa/:id', 
        component: FormDespesaComponent,
        data: { formType: FormType.Update }
      },
      { 
        path: 'visualizar-despesa/:id', 
        component: FormDespesaComponent,
        data: { formType: FormType.View }
      },
      { path: 'meta-despesa', component: MetaDespesaComponent },

      // RECEITA
      { 
        path: 'cadastro-receita', 
        component: FormReceitaComponent,
        data: { formType: FormType.Create }
      },
      { 
        path: 'editar-receita/:id', 
        component: FormReceitaComponent,
        data: { formType: FormType.Update }
      },
      { 
        path: 'visualizar-receita/:id', 
        component: FormReceitaComponent,
        data: { formType: FormType.View }
      },

      { path: 'lista-despesa', component: ListDespesaComponent },
      { path: 'lista-receita', component: ListReceitaComponent },
      { path: 'meta-despesa', component: MetaDespesaComponent },
    ]
  },
  { path: '**', redirectTo: '' }
];
