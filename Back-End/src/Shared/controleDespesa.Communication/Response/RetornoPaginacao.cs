using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace controleDespesa.Communication.Response
{
    public class RetornoPaginacao<T>
    {
        public static async Task<RetornoPaginacao<T>> CriarAsync(
            int pagina, int itensPorPagina, IQueryable<T> query)
        {
            var totalItens = await query.CountAsync();

            if (itensPorPagina == 0)
                itensPorPagina = totalItens;

            var totalPaginas = (int)Math.Ceiling((decimal)totalItens / itensPorPagina);
            var resultado = await query
                .Skip(itensPorPagina * (pagina - 1))
                .Take(itensPorPagina)
                .ToListAsync();

            return new RetornoPaginacao<T>
            {
                TotalItens = totalItens,
                Pagina = pagina,
                TotalPaginas = totalPaginas,
                Resultado = resultado
            };
        }

        public int TotalItens { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public IEnumerable<T> Resultado { get; set; }
    }
}