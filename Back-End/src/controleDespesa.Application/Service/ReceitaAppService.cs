using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Cryptografia;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Application.Validation;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Interface;
using controleDespesa.Domain.Repositorys.Receita.Interface;
using controleDespesa.Domain.Repositorys.Usuarios.Interface;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public ReceitaAppService(IMapper mapper, IReceitaRepository receitaRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _mapper = mapper;
            _receitaRepository = receitaRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Receita> BuscarReceita(int id)
        {
            return await _receitaRepository.BuscarReceita(id);        }

        public Task<List<Receita>> BuscarReceitas()
        {
            throw new NotImplementedException();
        }

        public async Task<Receita> Cadastro(ReceitaDTO receitaDTO, int usuarioId)
        {
            receitaDTO.UsuarioId = usuarioId;
            ValidarDadosReceita(receitaDTO);

            var receita = _mapper.Map<Receita>(receitaDTO);

            await _receitaRepository.Add(receita);
            

            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
            _cache.Remove("despesas_recentes");

            return receita;



        }

        public async Task<RetornoPaginacao<Receita>> ReceitaLista(int pagina, int totalPatina)
        {
            return await _receitaRepository.ReceitaLista(pagina, totalPatina);
        }

        public Task<Receita> Editar(ReceitaDTO receitaDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Excluir(int id)
        {
              
            
            var receita =  await _receitaRepository.Excluir(id);

            await _unitOfWork.Commit();

            _cache.Remove("dashboard_cache");
            _cache.Remove("despesas_recentes");

            return receita;

        }

        public async Task<List<TipoDespesaReceitaResponse>> ListarCategoriasReceita()
        {
            return await _receitaRepository.ListarCategoriaReceita();
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

        public async Task AtualizarAsync(ReceitaDTO receitaDTO)
        {
            ValidarDadosReceita(receitaDTO);

            var receita = _mapper.Map<Receita>(receitaDTO);

             _receitaRepository.AtualizarAsync(receita);

            _cache.Remove("dashboard_cache");
            _cache.Remove("despesas_recentes");


            await _unitOfWork.Commit();

           
        }
    }
}
