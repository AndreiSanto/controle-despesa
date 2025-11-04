using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using controleDespesa.Domain.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class LoginAppService : ILoginAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAcessTokenGenerator _acessTokenGenerator;

        public LoginAppService(IUsuarioRepository usuarioRepository, PasswordEncripter passwordEncripter, IAcessTokenGenerator acessTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordEncripter = passwordEncripter;
            _acessTokenGenerator = acessTokenGenerator;
        }

        public async Task<UsuarioResponse> FazerLogin(UsuarioDTO usuarioDTO)
        {

            var passwordCryptografia = _passwordEncripter.HashPassword(usuarioDTO.Password);
            var usuario = await _usuarioRepository.GetUsuarioAsync(usuarioDTO.Email, passwordCryptografia) ?? throw new UnauthorizedAccessException("Credenciais inválidas."); ;

            var senhaValida = _passwordEncripter.VerifyPassword(usuarioDTO.Password, usuario.Password);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Token = new TokenResponse()
                {
                    acessToken = _acessTokenGenerator.GenerateToken(usuario.Identificador),
                }
            };


        }
    }
}
