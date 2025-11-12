using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Usuarios.Interface;
using controleDespesa.Domain.Security.Tokens;
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
        private readonly IAcessTokenGenerator _acessTokenGenerator;


        public UsuarioAppService(IMapper mapper, 
            PasswordEncripter passwordEncripter, 
            IUsuarioRepository usuarioRepository, 
            IUnitOfWork unitOfWork, IAcessTokenGenerator acessTokenGenerator)
        {
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _acessTokenGenerator = acessTokenGenerator;
        }

        public async  Task<UsuarioResponse> Cadastrar(UsuarioDTO usuarioDTO)
        {

           ValidarDados(usuarioDTO);
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.Password = _passwordEncripter.HashPassword(usuarioDTO.Password);

            usuario.Identificador = Guid.NewGuid();



            await _usuarioRepository.Add(usuario);
            await _unitOfWork.Commit();

               return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome
                
            }; 
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
