using controleDespesa.API.Attributes;
using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly ILoginAppService _loginAppService;

        public LoginController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        [HttpPost]
        public async Task<IActionResult> FazerLogin([FromBody] LoginDTO login)
        {
            try
            {
                var resposta = await _loginAppService.FazerLogin(login);

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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request)
        {
            var result = await _loginAppService.RefreshTokenAsync(request.RefreshToken);
            if (result == null)
                return Unauthorized(new { message = "Refresh token inválido ou expirado." });

            return Ok(result);
        }
    }
}
