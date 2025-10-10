using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            this._usuarioAppService = usuarioAppService;
        }

        [HttpPost("Cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var usuarioNovo = await _usuarioAppService.Cadastrar(usuario);

               
                return Created(string.Empty, usuarioNovo);
            }
            catch (ValidationException ex) 
            {
               
                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao cadastrar o usuário.", Detalhes = ex.Message });
            }
        }

        /*  public IActionResult Cadastro()
          {
              var novoItem = new
              {
                  Id = Guid.NewGuid(),
                  Nome = "Mouse Gamer",
                  Preco = 150.00m
              };
              return Created(string.Empty,novoItem);
          }*/
    }
}
