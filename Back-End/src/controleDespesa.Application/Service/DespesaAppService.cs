using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class DespesaAppService : IDespesaAppService
    {
        private readonly IDespesaDomainService _despesaDomainService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDespesaRepository _despesaRepository;

        public DespesaAppService(IDespesaDomainService despesaDomainService, IMapper mapper, IUnitOfWork unitOfWork, IDespesaRepository despesaRepository)
        {
            _despesaDomainService = despesaDomainService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _despesaRepository = despesaRepository;
        }

        public async Task<Despesa> Cadastro(DespesaDTO despesaDTO)
        {
            ValidarDados(despesaDTO);
            var despesa = _mapper.Map<Despesa>(despesaDTO);

            if(despesaDTO.Parcelado)
            {
                despesa.DespesaParcela = _despesaDomainService.GerarParcelas(despesa);
            }
            await _despesaRepository.Add(despesa);

            await _unitOfWork.Commit();

            return despesa;
            


           
        }

        private void ValidarDados(DespesaDTO despesaDTO)
        {
            var validator = new DespesaValidator();
            var resultado = validator.Validate(despesaDTO);
            if (resultado.IsValid == false)
            {
                var erroMenssage = resultado.Errors.Select(a => a.ErrorMessage).FirstOrDefault();

                throw new ValidationException(erroMenssage);
            }

        }
    }
}
