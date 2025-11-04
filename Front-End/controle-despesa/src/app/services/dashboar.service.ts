import { Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { DespesaResponse } from "../models/responses/despesa-response";
import { ReceitaResponse } from "../models/responses/receita-response";
import { DashboardResponse } from "../models/responses/dashboard-response";


@Injectable({
  providedIn: 'root'
})

export class DashboardService {
  private apiUrl = `${environment.apiUrl}`;
    constructor(private httpClient: HttpClient) { }  
    
    getDashboardData() : Observable<DashboardResponse> {
      return this.httpClient.get<DashboardResponse>(`${this.apiUrl}/Dashboard/ResumoDashboard`);
    }
    getDashboardReceitasData():Observable<ReceitaResponse[]> {
      return this.httpClient.get<ReceitaResponse[]>(`${this.apiUrl}/Dashboard/Receitas`);
    }

    getDashboardDespesaData(): Observable<DespesaResponse[]> {
        return this.httpClient.get<DespesaResponse[]>(`${this.apiUrl}/Dashboard/Despesas`);
      }
    
}
