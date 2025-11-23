using controleDespesa.Application.DTOs;
using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Interfaces
{
    public interface IMetaDespesaAppService
    {
        public Task<MetaDespesaDTO> Cadastro(MetaDespesaDTO metaDespesaDTO,int usuarioId); 
        public Task<MetaDespesaDTO> Alterar(MetaDespesaDTO metaDespesaDTO); 
        public Task<MetaDespesaDTO> Desativar(MetaDespesaDTO metaDespesaDTO); 
        public Task<MetaDespesaDTO> Ativar(MetaDespesaDTO metaDespesaDTO); 
    }
}
