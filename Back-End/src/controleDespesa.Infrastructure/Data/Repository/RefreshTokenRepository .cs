using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Data.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApiContext _context;

        public RefreshTokenRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task SalvarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ObterPorTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task AtualizarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(string token)
        {
            var entity = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            if (entity != null)
            {
                _context.RefreshTokens.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
