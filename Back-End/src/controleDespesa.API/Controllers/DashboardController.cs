using controleDespesa.API.Attributes;
using controleDespesa.Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    { 
        private readonly IDashboardAppService _dashboardService;

        public DashboardController(IDashboardAppService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("ResumoDashboard")]
        public async Task<IActionResult> GetDashboard()
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var dashboard = await _dashboardService.GetDashboard(userId);

                return Ok(dashboard);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro no dashboard.", Detalhes = ex.Message });
            }
        }


        [HttpGet("Receitas")]
        public async Task<IActionResult> GetDashboardReceitas()
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var dashboard = await _dashboardService.GetDashboardReceita(userId);

                return Ok(dashboard);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao listar receitas no dashboard.", Detalhes = ex.Message });
            }
        }


        [HttpGet("Despesas")]
        public async Task<IActionResult> GetDashboardDespesas()
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var dashboard = await _dashboardService.GetDashboardDespesa(userId);

                return Ok(dashboard);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao listar despesas no dashboard.", Detalhes = ex.Message });
            }
        }
    }
}
