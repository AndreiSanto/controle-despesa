import { Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";
import { MetaDespesaDTO } from "../models/dtos/meta-despesa.dto";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MetaDespesaService {
  private apiUrl = `${environment.apiUrl}`;
  
    constructor(private httpClient: HttpClient) { }

     cadastro(meta: MetaDespesaDTO): Observable<MetaDespesaDTO> {
        return this.httpClient.post<MetaDespesaDTO>(`${this.apiUrl}/MetaGasto/Cadastro`, meta);
      }

       alterar(meta: MetaDespesaDTO): Observable<MetaDespesaDTO> {
          return this.httpClient.post<MetaDespesaDTO>(`${this.apiUrl}/MetaGasto/Alterar`, meta);
        }
  
}