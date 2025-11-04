import { Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";
import { TipoDespesaReceitaDTO } from "../models/dtos/tipoDespesaReceita.dto";
import { Observable } from "rxjs";
import { TipoDespesaReceitaResponse } from "../models/responses/tipo-despesa-receita-response";

@Injectable({
  providedIn: 'root'
})
export class ReceitaService {
  private apiUrl = `${environment.apiUrl}`; 
    constructor(private httpClient:HttpClient) { }

    ListarReceitas():Observable<TipoDespesaReceitaResponse[]>{
        return this.httpClient.get<TipoDespesaReceitaResponse[]>(`${this.apiUrl}/Receita/Listar`);
    }
}