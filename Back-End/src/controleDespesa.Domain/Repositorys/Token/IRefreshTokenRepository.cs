using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Repositorys.Token
{
    public interface IRefreshTokenRepository
    {
        public Task SalvarAsync(RefreshToken token);
        public Task<RefreshToken?> ObterPorTokenAsync(string token);
        public Task AtualizarAsync(RefreshToken token);
        public Task ExcluirAsync(string token);
    }
}
