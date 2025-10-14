using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.TipoDespesaReceita;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class TipoDespesaReceitaAppService : ITipoDespesaReceitaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoDespesaReceitaRepository _tipoDespesaReceitaRepository;

        public TipoDespesaReceitaAppService(IMapper mapper, IUnitOfWork unitOfWork, ITipoDespesaReceitaRepository tipoDespesaReceitaRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tipoDespesaReceitaRepository = tipoDespesaReceitaRepository;
        }

        public async Task<TipoDespesaReceita> Cadastro(TipoDespesaReceitaDTO tipoDespesaReceitaDTO)
        {
            ValidarDados(tipoDespesaReceitaDTO);
            var tipoDespesaReceita = _mapper.Map<TipoDespesaReceita>(tipoDespesaReceitaDTO);
            await _tipoDespesaReceitaRepository.Add(tipoDespesaReceita);

            await _unitOfWork.Commit();

            return tipoDespesaReceita;




        }

        private void ValidarDados(TipoDespesaReceitaDTO tipoDespesaReceitaDTO)
        {
            var validator = new TipoDespesaReceitaValidator();
            var resultado = validator.Validate(tipoDespesaReceitaDTO);
            if (resultado.IsValid == false)
            {
                var erroMenssage = resultado.Errors.Select(a => a.ErrorMessage).FirstOrDefault();

                throw new ValidationException(erroMenssage);
            }

        }
    }
}
