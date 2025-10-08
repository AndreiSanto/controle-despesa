using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioAppService _usuarioAppService;

        public UsuarioController(UsuarioAppService usuarioAppService)
        {
            this._usuarioAppService = usuarioAppService;
        }

        [HttpPost("Cadastro")]
        public IActionResult Cadastrar([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var usuarioNovo = _usuarioAppService.Cadastrar(usuario);

               
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
