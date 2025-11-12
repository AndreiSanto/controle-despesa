using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Communication.Response
{
    public class UsuarioResponse
    {

        public string Nome { get; set; } = string.Empty;

        public int Id { get; set; }

        public TokenResponse Token { get; set; } = null!;
        public TokenResponse RefreshToken { get; set; } = null!;


    }
}
