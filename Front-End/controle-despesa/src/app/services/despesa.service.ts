import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DespesaDTO } from "../models/dtos/despesa.dto";
import { Observable } from "rxjs";
import { environment } from "../environment/environment";
import { PaginacaoResponse } from "../models/responses/paginacao-response";
import { TipoDespesaReceitaResponse } from "../models/responses/tipo-despesa-receita-response";

@Injectable({
  providedIn: 'root'
})
export class DespesaService {
  private apiUrl = `${environment.apiUrl}`;
  constructor(private httpClient: HttpClient) { }

  cadastro(despesa: DespesaDTO): Observable<DespesaDTO> {
    return this.httpClient.post<DespesaDTO>(`${this.apiUrl}/Despesa/Cadastro`, despesa);

  }
  ListarDespesas(pagina: number, tamanhoPagina: number): Observable<PaginacaoResponse<DespesaDTO>> {
    return this.httpClient.get<PaginacaoResponse<DespesaDTO>>(`${this.apiUrl}/Despesa/ListarDespesas?pagina=${pagina}&totalPagina=${tamanhoPagina}`);
  }

  ListarCategoriaDespesa(): Observable<TipoDespesaReceitaResponse[]> {
    return this.httpClient.get<TipoDespesaReceitaResponse[]>(`${this.apiUrl}/Despesa/ListarCategoriaDespesa`);
  }
  obterPorId(id: number): Observable<DespesaDTO> {
    return this.httpClient.get<DespesaDTO>(`${this.apiUrl}/Despesa/ObterPorId/${id}`);
  }
  atualizar(despesa: DespesaDTO): Observable<DespesaDTO> {
    return this.httpClient.put<DespesaDTO>(`${this.apiUrl}/Despesa/Atualizar`, despesa);
  }

  exclui(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/Despesa/Excluir/${id}`);
  }

}