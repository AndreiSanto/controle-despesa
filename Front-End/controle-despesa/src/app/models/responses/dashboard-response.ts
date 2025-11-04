import { DespesaResponse } from "./despesa-response";
import { ReceitaResponse } from "./receita-response";

export interface DashboardResponse {
  totalDespesas: number;
  totalReceitas: number;
  metaMes: number;
  receitaResponses: ReceitaResponse[];
  despesaResponses : DespesaResponse [];
}