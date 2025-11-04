using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Communication.Response;
using controleDespesa.Communication.Response.Despesa;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Despesa.Interface;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDespesaRepository _despesaRepository;
        private readonly IMemoryCache _cache;

        public DespesaAppService(IMapper mapper, IUnitOfWork unitOfWork, IDespesaRepository despesaRepository, IMemoryCache cache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _despesaRepository = despesaRepository;
            _cache = cache;
        }

        public async Task AtualizarAsync(DespesaDTO despesaDto)
        {
            ValidarDados(despesaDto);
            var despesa = _mapper.Map<Despesa>(despesaDto);

             _despesaRepository.AtualizarAsync(despesa);
            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
           

        }

        public async Task<Despesa> BuscarDespesa(int id)
        {
            return await _despesaRepository.BuscarDespesa(id);
        }

        public async Task<Despesa> Cadastro(DespesaDTO despesaDTO)
        {
            ValidarDados(despesaDTO);
            var despesa = _mapper.Map<Despesa>(despesaDTO);

          
            await _despesaRepository.Add(despesa);

            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
            _cache.Remove("despesas_recentes");

            return despesa;
            


           
        }

        public async Task<RetornoPaginacao<Despesa>> DespesaLista(int pagina, int totalPatina)
        {
            return await _despesaRepository.DespesaLista(pagina, totalPatina);
           
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var resultado = await _despesaRepository.ExcluirAsync(id);

            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
            _cache.Remove("despesas_recentes");

            return resultado;
        }

        public async Task<List<TipoDespesaReceitaResponse>> ListarCategoriasDespesa()
        {
            return await _despesaRepository.ListarCategoriaReceita();
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
