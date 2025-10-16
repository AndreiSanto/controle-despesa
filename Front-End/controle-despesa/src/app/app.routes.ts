import { Routes } from '@angular/router';
import { HomeComponent } from './pages/Home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { OnboardingComponent } from './pages/onboarding/onboarding.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

export const routes: Routes = [

   { path: '', component: HomeComponent,pathMatch: 'full'},           
  { path: 'login', component: LoginComponent }, 
  { path: 'onboarding', component: OnboardingComponent },
  {
    path: 'app',
    component: LayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent }
    ]
  },
  { path: '**', redirectTo: '' }                     
];



