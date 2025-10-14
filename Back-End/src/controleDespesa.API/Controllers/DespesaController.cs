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
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaAppService _despesaAppService;

        public DespesaController(IDespesaAppService despesaAppService)
        {
            _despesaAppService = despesaAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] DespesaDTO despesaDTO)
        {


            try
            {
                var despesa = await _despesaAppService.Cadastro(despesaDTO);

                return Created(string.Empty, despesa);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao cadastrar uma despesa.", Detalhes = ex.Message });
            }
        }
    }
}
