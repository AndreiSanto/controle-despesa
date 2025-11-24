using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.MetaDespesas.Interface;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public MetaDespesaAppService(IMetaDespesaRepository metaDespesaRepository, IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _metaDespesaRepository = metaDespesaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<MetaDespesaDTO> Alterar(MetaDespesaDTO metaDespesaDTO)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);

            var meta =  _metaDespesaRepository.Alterar(metaDespesa);
            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
            _cache.Remove("meta_cache");


            return _mapper.Map<MetaDespesaDTO>(meta);


        }

       

        public async Task<MetaDespesaDTO?> BuscarMeta(int usuarioId)
        {
            var cacheKey = "meta_cache";

            if (!_cache.TryGetValue(cacheKey, out MetaDespesa meta))
            {
                 meta = await _metaDespesaRepository.BuscarMeta(usuarioId);
                _cache.Set(cacheKey, meta, TimeSpan.FromMinutes(10));
            }


           
            return meta is null ? null : _mapper.Map<MetaDespesaDTO>(meta);
        }


        public async Task<MetaDespesaDTO> Cadastro(MetaDespesaDTO metaDespesaDTO, int usuarioId)
        {
            ValidarDadosMetaDespea(metaDespesaDTO);

            var metaDespesa = _mapper.Map<MetaDespesa>(metaDespesaDTO);
            metaDespesa.UsuarioId = usuarioId;
            var meta = _metaDespesaRepository.Cadastro(metaDespesa);
            await _unitOfWork.Commit();
            _cache.Remove("dashboard_cache");
            _cache.Remove("meta_cache");


            return metaDespesaDTO;
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
