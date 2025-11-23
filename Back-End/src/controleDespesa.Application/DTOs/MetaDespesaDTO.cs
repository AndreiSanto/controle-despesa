using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.DTOs
{
    public class MetaDespesaDTO
    {
        public int Id { get; set; }



        public decimal Valor { get; set; }

        public int Ano { get; set; }
        public int Mes { get; set; }

        public int UsuarioId { get; set; }
    }
}
