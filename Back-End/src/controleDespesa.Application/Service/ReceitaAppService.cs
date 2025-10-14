using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Receita.Interface;
using controleDespesa.Domain.Repositorys.Usuario.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class ReceitaAppService : IReceitaAppService
    {

        private readonly IMapper _mapper;
        private readonly IReceitaRepository _receitaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public Task<Receita> BuscarReceita(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Receita>> BuscarReceitas()
        {
            throw new NotImplementedException();
        }

        public async Task<Receita> Cadastro(ReceitaDTO receitaDTO)
        {
            ValidarDadosReceita(receitaDTO);

            var receita = _mapper.Map<Receita>(receitaDTO);

            await _receitaRepository.Add(receita);

            await _unitOfWork.Commit();

            return receita;



        }

        public Task<Receita> Editar(ReceitaDTO receitaDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(int id)
        {
            throw new NotImplementedException();
        }

        private void ValidarDadosReceita(ReceitaDTO receitaDTO)
        {
            var validator = new ReceitaValidator();
            var resultado = validator.Validate(receitaDTO);
            if (resultado.IsValid == false)
            {
                var erroMenssage = resultado.Errors.Select(a => a.ErrorMessage).FirstOrDefault();

                throw new ValidationException(erroMenssage);
            }

        }
    }
}
