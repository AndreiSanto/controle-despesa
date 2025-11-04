import { Component, OnInit } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { TableModule } from 'primeng/table';
import { ProgressBarModule } from 'primeng/progressbar';
import { CommonModule, DecimalPipe } from '@angular/common';
import { DashboardService } from '../../services/dashboar.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CardModule, ChartModule, TableModule, ProgressBarModule, CommonModule, DecimalPipe, HttpClientModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
  providers: [DashboardService]
})
export class DashboardComponent implements OnInit {
  totalDespesasMes: number = 0;
  totalReceitasMes: number = 0;
  saldo: number = 0;
  constructor(private dashboardService: DashboardService) { }
  qtdDespesasFixas: number = 0;
  qtdReceitasFixas: number = 0;

  ultimasDespesas: any[] = [];
  ultimasReceitas: any[] = [];
  graficoFluxo: any;

  metaDespesaMes: number = 3000; // 💰 Meta mensal definida
  percentualGasto: number = 0;   // Percentual do gasto em relação à meta

  ngOnInit(): void {
    // 🔹 Dados fictícios
    

    this.loadDashboardData();
  

    // 🔹 Gráfico de fluxo (entrada x saída)
    this.graficoFluxo = {
      labels: ['Receitas', 'Despesas'],
      datasets: [
        {
          data: [this.totalReceitasMes, this.totalDespesasMes],
          backgroundColor: ['#4caf50', '#f44336']
        }
      ]
    };
  }

  loadReceitas() {
    this.dashboardService.getDashboardReceitasData().subscribe((receitas) => {
      this.ultimasReceitas = receitas;
    });
  }

  loadDespesas() {
    this.dashboardService.getDashboardDespesaData().subscribe((despesas) => {
      this.ultimasDespesas = despesas;
    });
  }
  loadDashboardData() {
    this.dashboardService.getDashboardData().subscribe((data) => {
      this.totalDespesasMes = data.totalDespesas;
      this.totalReceitasMes = data.totalReceitas;
      this.metaDespesaMes = data.metaMes;
      this.ultimasReceitas = data.receitaResponses;
      this.ultimasDespesas = data.despesaResponses;
      this.saldo = this.totalReceitasMes - this.totalDespesasMes;
    });
  }
}
