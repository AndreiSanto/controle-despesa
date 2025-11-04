using controleDespesa.Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
                var dashboard = await _dashboardService.GetDashboard();

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
                var dashboard = await _dashboardService.GetDashboardReceita();

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
                var dashboard = await _dashboardService.GetDashboardDespesa();

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
