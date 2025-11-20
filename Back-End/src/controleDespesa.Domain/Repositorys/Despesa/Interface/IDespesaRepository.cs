using controleDespesa.Communication.Response;
using controleDespesa.Communication.Response.Despesa;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Value_Objects.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Despesa.Interface
{
    public  interface IDespesaRepository
    {
        public Task Add(Entities.Despesa despesa);
        public Task<RetornoPaginacao<Entities.Despesa>> DespesaLista(int pagina, int totalPatina, Filtro filtro);
        public Task<List<TipoDespesaReceitaResponse>> ListarCategoriaReceita();

        public void AtualizarAsync(Entities.Despesa despesa);
        public Task<bool> ExcluirAsync(int id);

        public Task<Entities.Despesa> BuscarDespesa(int id);

        public Task<decimal> ObterTotalDoMesAsync();





    }
}
