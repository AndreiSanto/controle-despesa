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
    public class LoginController : ControllerBase
    {
        private readonly ILoginAppService _loginAppService;

        public LoginController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        [HttpPost]
        public async Task<IActionResult> FazerLogin([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var resposta = await _loginAppService.FazerLogin(usuario);

                return Ok(resposta);
               
            }
            catch (UnauthorizedAccessException ex)
            {

                return StatusCode(StatusCodes.Status401Unauthorized, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao fazer o login.", Detalhes = ex.Message });
            }
        }
    }
}
