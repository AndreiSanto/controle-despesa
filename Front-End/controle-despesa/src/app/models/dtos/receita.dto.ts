import { TipoDespesaReceitaDTO } from "./tipoDespesaReceita.dto";

export interface ReceitaDTO {
id:number;
descricao: string;
receitaFixa:boolean;
valor:number;
dataCadastro: Date;
tipoDespesaReceitaId: number | null;


}