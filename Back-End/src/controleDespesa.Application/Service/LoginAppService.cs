using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Repositorys.Token;
using controleDespesa.Domain.Repositorys.Usuarios.Interface;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginAppService(IUsuarioRepository usuarioRepository, PasswordEncripter passwordEncripter, 
            IAcessTokenGenerator acessTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _usuarioRepository = usuarioRepository;
            _passwordEncripter = passwordEncripter;
            _acessTokenGenerator = acessTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponse> FazerLogin(LoginDTO loginDTO)
        {

            var passwordCryptografia = _passwordEncripter.HashPassword(loginDTO.Password);
            var usuario = await _usuarioRepository.GetUsuarioAsync(loginDTO.Email, passwordCryptografia) ?? throw new UnauthorizedAccessException("Credenciais inválidas."); ;

            var senhaValida = _passwordEncripter.VerifyPassword(loginDTO.Password, usuario.Password);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var accessToken = _acessTokenGenerator.GenerateToken(usuario.Identificador, usuario.Id);
            var refreshToken = _acessTokenGenerator.GerarRefreshToken();

            await _refreshTokenRepository.SalvarAsync(new RefreshToken
            {
                UserId = usuario.Id,
                Token = refreshToken,
                Expiration = DateTime.UtcNow.AddDays(7)
            });

            return new AuthResponse
            {
               
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    


            };


        }

        public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = await _refreshTokenRepository.ObterPorTokenAsync(refreshToken);
            if (tokenEntity == null || tokenEntity.Expiration <= DateTime.UtcNow)
                return null;

            var usuario = await _usuarioRepository.GetUsuarioIdAsync(tokenEntity.UserId);

            var novoAccess = _acessTokenGenerator.GenerateToken(usuario.Identificador, usuario.Id);
            var novoRefresh = _acessTokenGenerator.GerarRefreshToken();

           
            tokenEntity.Token = novoRefresh;
            tokenEntity.Expiration = DateTime.UtcNow.AddDays(7);
            await _refreshTokenRepository.AtualizarAsync(tokenEntity);

            return new AuthResponse
            {
                Token = novoAccess,
                RefreshToken = novoRefresh
            };
        }
    }
    }

