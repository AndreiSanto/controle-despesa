using controleDespesa.Communication.Response;
using controleDespesa.Domain.Value_Objects.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Receita.Interface
{
    public interface IReceitaRepository
    {
        public Task Add(Entities.Receita receita);
        public Task<List<TipoDespesaReceitaResponse>> ListarCategoriaReceita();

        public Task<RetornoPaginacao<Entities.Receita>> ReceitaLista(int pagina, int totalPatina, Filtro filtro);

        public Task<bool> Excluir(int id);
        public Task<Entities.Receita> BuscarReceita(int id);

        public void AtualizarAsync(Entities.Receita receita);



    }
}
