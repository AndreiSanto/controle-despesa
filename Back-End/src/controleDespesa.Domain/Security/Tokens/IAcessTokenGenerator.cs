using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Security.Tokens
{
    public interface IAcessTokenGenerator
    {
        public string GenerateToken( Guid idIdenficador);
    }
}
