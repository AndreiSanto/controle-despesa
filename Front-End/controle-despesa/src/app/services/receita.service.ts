import { Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";
import { ReceitaDTO } from "../models/dtos/receita.dto";
import { Observable } from "rxjs";
import { TipoDespesaReceitaResponse } from "../models/responses/tipo-despesa-receita-response";
import { PaginacaoResponse } from "../models/responses/paginacao-response";
import { ReceitaListaResponse } from "../models/responses/receita-lista-response";
import { FiltroDTO } from "../models/dtos/filtro.dto";

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

  ListarReceitas(
    filtro: FiltroDTO,
    pagina: number,
    tamanhoPagina: number
  ): Observable<PaginacaoResponse<ReceitaListaResponse>> {
  
    let params: any = {
      pagina,
      totalPagina: tamanhoPagina
    };
  
    if (filtro.descricao)
      params.descricao = filtro.descricao;
  
    if (filtro.dataCadastroInicial)
      params.dataCadastroInicial = this.formatarData(filtro.dataCadastroInicial);
  
    if (filtro.dataCadastroFinal)
      params.dataCadastroFinal = this.formatarData(filtro.dataCadastroFinal);
  
    return this.httpClient.get<PaginacaoResponse<ReceitaListaResponse>>(
      `${this.apiUrl}/Receita/ListarReceitas`,
      { params }
    );
  
  
  }
  private formatarData(data: any): string {
  return new Date(data).toISOString().split('T')[0];
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
