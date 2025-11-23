import { Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";
import { ReceitaDTO } from "../models/dtos/receita.dto";
import { Observable } from "rxjs";
import { TipoDespesaReceitaResponse } from "../models/responses/tipo-despesa-receita-response";
import { PaginacaoResponse } from "../models/responses/paginacao-response";
import { ReceitaListaResponse } from "../models/responses/receita-lista-response";

@Injectable({
  providedIn: 'root'
})
export class ReceitaService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private httpClient: HttpClient) { }

  cadastro(receita: ReceitaDTO): Observable<ReceitaDTO> {
    return this.httpClient.post<ReceitaDTO>(`${this.apiUrl}/Receita/Cadastro`, receita);
  }

  ListarReceitasCategoria(): Observable<TipoDespesaReceitaResponse[]> {
    return this.httpClient.get<TipoDespesaReceitaResponse[]>(`${this.apiUrl}/Receita/ListarCategoriaReceita`);
  }

  ListarReceitas(pagina: number, tamanhoPagina: number): Observable<PaginacaoResponse<ReceitaListaResponse>> {
    return this.httpClient.get<PaginacaoResponse<ReceitaListaResponse>>(`${this.apiUrl}/Receita/ListarReceitas?pagina=${pagina}&totalPagina=${tamanhoPagina}`);
  }

  obterPorId(id: number): Observable<ReceitaDTO> {
      return this.httpClient.get<ReceitaDTO>(`${this.apiUrl}/Receita/ObterPorId/${id}`);
    }

      
      atualizar(receita: ReceitaDTO): Observable<ReceitaDTO> {
        return this.httpClient.put<ReceitaDTO>(`${this.apiUrl}/Receita/Atualizar`, receita);
      }
    
      exclui(id: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.apiUrl}/Receita/Excluir/${id}`);
      }

}
