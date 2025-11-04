export interface DespesaDTO {
    id: number;
    descricao: string;
    tipoDespesaReceitaId: number | null;
    numeroDeParcela: number;
    valorDespesa: number | null;
    dataCadastro: Date;
    dataDespesa: Date;
    parcelado: boolean;
    despesaFixa: boolean;



}

