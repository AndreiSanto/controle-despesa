using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.MetaDespesas.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service
{
    public class MetaDespesaAppService : IMetaDespesaAppService
    {
        private readonly IMetaDespesaRepository _metaDespesaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MetaDespesaAppService(IMetaDespesaRepository metaDespesaRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _metaDespesaRepository = metaDespesaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MetaDespesaDTO> Alterar(MetaDespesaDTO metaDespesaDTO)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);

            var meta =  _metaDespesaRepository.Alterar(metaDespesa);
            await _unitOfWork.Commit();
            return _mapper.Map<MetaDespesaDTO>(meta);


        }

        public async Task<MetaDespesaDTO> Ativar(MetaDespesaDTO metaDespesaDTO)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);

            metaDespesa.Ativo = true;

            var meta = _metaDespesaRepository.Alterar(metaDespesa);
            await _unitOfWork.Commit();
            return _mapper.Map<MetaDespesaDTO>(meta);
        }

        public async Task<MetaDespesaDTO> Cadastro(MetaDespesaDTO metaDespesaDTO, int usuarioId)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);
            metaDespesa.UsuarioId = usuarioId;
            var meta = _metaDespesaRepository.Cadastro(metaDespesa);
            await _unitOfWork.Commit();

            return metaDespesaDTO;
        }

        public async Task<MetaDespesaDTO> Desativar(MetaDespesaDTO metaDespesaDTO)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);

            metaDespesa.Ativo = false;

            var meta = _metaDespesaRepository.Alterar(metaDespesa);
            await _unitOfWork.Commit();
            return _mapper.Map<MetaDespesaDTO>(meta);
        }

        private void ValidarDadosMetaDespea(MetaDespesaDTO metaDespesaDTO)
        {
            var validator = new MetaDespesaValidor();
            var resultado = validator.Validate(metaDespesaDTO);
            if (resultado.IsValid == false)
            {
                var erroMenssage = resultado.Errors.Select(a => a.ErrorMessage).FirstOrDefault();

                throw new ValidationException(erroMenssage);
            }

        }
    }
}
