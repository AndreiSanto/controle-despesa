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
    public class TipoDespesaReceitaController : ControllerBase
    {
        private readonly ITipoDespesaReceitaAppService _tipoDespesaReceita;

        public TipoDespesaReceitaController(ITipoDespesaReceitaAppService tipoDespesaReceita)
        {
            _tipoDespesaReceita = tipoDespesaReceita;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoDespesaReceitaDTO tipoDespesaReceitaDTO)
        {


            try
            {
                var tipoDespesaReceita = await _tipoDespesaReceita.Cadastro(tipoDespesaReceitaDTO);

                return Created(string.Empty, tipoDespesaReceita);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao cadastrar uma receita.", Detalhes = ex.Message });
            }
        }
    }
}
