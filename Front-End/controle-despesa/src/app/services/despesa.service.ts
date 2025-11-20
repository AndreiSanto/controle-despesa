import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DespesaDTO } from "../models/dtos/despesa.dto";
import { Observable } from "rxjs";
import { environment } from "../environment/environment";
import { PaginacaoResponse } from "../models/responses/paginacao-response";
import { TipoDespesaReceitaResponse } from "../models/responses/tipo-despesa-receita-response";
import { FiltroDTO } from "../models/dtos/filtro.dto";

@Injectable({
  providedIn: 'root'
})
export class DespesaService {
  private apiUrl = `${environment.apiUrl}`;
  constructor(private httpClient: HttpClient) { }

  cadastro(despesa: DespesaDTO): Observable<DespesaDTO> {
    return this.httpClient.post<DespesaDTO>(`${this.apiUrl}/Despesa/Cadastro`, despesa);

  }
ListarDespesas(
  filtro: FiltroDTO,
  pagina: number,
  tamanhoPagina: number
): Observable<PaginacaoResponse<DespesaDTO>> {

  let params: any = {
    pagina,
    totalPagina: tamanhoPagina
  };

  if (filtro.descricao)
    params.descricao = filtro.descricao;

  if (filtro.dataCadastroInicial)
    params.dataCadastroInicial = filtro.dataCadastroInicial;

  if (filtro.dataCadastroFinal)
    params.dataCadastroFinal = filtro.dataCadastroFinal;

  return this.httpClient.get<PaginacaoResponse<DespesaDTO>>(
    `${this.apiUrl}/Despesa/ListarDespesas`,
    { params }
  );
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