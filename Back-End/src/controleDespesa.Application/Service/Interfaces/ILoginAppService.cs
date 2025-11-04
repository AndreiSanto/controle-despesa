using controleDespesa.Application.DTOs;
using controleDespesa.Communication.Response;
using controleDespesa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Application.Service.Interfaces
{
    public interface ILoginAppService
    {
        public Task<UsuarioResponse> FazerLogin(UsuarioDTO usuarioDTO);
    }
}
