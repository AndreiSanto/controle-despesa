using AutoMapper;
using controleDespesa.Application.DTOs;
using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.Ativo, opt => opt.Ignore());


            CreateMap<DespesaDTO, Despesa>()
     .ForMember(dest => dest.DespesaParcela, opt => opt.Ignore())
     .ForMember(dest => dest.TipoDespesaReceita, opt => opt.Ignore());


            

        }
    }
}
