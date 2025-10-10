using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioAppService(IMapper mapper, PasswordEncripter passwordEncripter, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async  Task<Usuario> Cadastrar(UsuarioDTO usuarioDTO)
        {

           ValidarDados(usuarioDTO);
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.Password = _passwordEncripter.HashPassword(usuarioDTO.Password);


            // Verificando a senha correta
            bool ok = _passwordEncripter.VerifyPassword("MinhaSenha123", usuario.Password);
            Console.WriteLine("Senha correta? " + ok); // true

            // Verificando senha errada
            bool errado = _passwordEncripter.VerifyPassword("OutraSenha", usuario.Password);
            Console.WriteLine("Senha errada? " + errado); // false
            await _usuarioRepository.Add(usuario);
            await _unitOfWork.Commit();

            return usuario;
        }

        private void ValidarDados(UsuarioDTO usuarioDTO)
        {
            var validator = new UsuarioValidator();
            var  resultado = validator.Validate(usuarioDTO);
            if (resultado.IsValid == false)
            {
                var erroMenssage = resultado.Errors.Select(a => a.ErrorMessage).FirstOrDefault();

                throw new ValidationException(erroMenssage);
            }

        }
    }
}
