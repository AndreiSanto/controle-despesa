using controleDespesa.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Cryptografia
{
    public class PasswordEncripter
    {
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public PasswordEncripter()
        {
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        /// <summary>
        /// Gera o hash da senha.
        /// </summary>

        public string HashPassword(string senha)
        {
            return _passwordHasher.HashPassword(null, senha);
        }

        /// <summary>
        /// Verifica se a senha informada corresponde ao hash armazenado.
        /// </summary>
        public bool VerifyPassword(string senhaInformada, string hashArmazenado)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashArmazenado, senhaInformada);

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
