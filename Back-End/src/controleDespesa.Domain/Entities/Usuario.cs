using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; }
    }
}
