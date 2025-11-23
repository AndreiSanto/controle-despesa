using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Entities
{
    [Table("metadespesa")]
    public class MetaDespesa
    {

        public int Id { get; set; }

       

        public decimal Valor { get; set; }

        public int Ano { get; set; }
        public int Mes { get; set; }
        public bool Ativo { get; set; }

        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
