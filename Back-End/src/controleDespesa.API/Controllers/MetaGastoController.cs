using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetaGastoController : ControllerBase
    {
        private readonly IMetaDespesaAppService _metaDespesaAppService;

        public MetaGastoController(IMetaDespesaAppService metaDespesaAppService)
        {
            _metaDespesaAppService = metaDespesaAppService;
        }
        [HttpPost("Cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] MetaDespesaDTO metaDespesaDTO)
        {
            try {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var metadDespesa = await _metaDespesaAppService.Cadastro(metaDespesaDTO, userId);


                return Created(string.Empty, metadDespesa);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao cadastrar a meta de Despesa.", Detalhes = ex.Message });
            }

        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Alterar([FromBody] MetaDespesaDTO metaDespesaDTO)
        {
            try
            {

                var metadDespesa = await _metaDespesaAppService.Alterar(metaDespesaDTO);


                return Ok(metadDespesa);
            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao cadastrar a meta de Despesa.", Detalhes = ex.Message });
            }

        }

        [HttpGet("BuscarMeta")]
        public async Task<IActionResult> BuscarMeta()
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


                var meta = await _metaDespesaAppService.BuscarMeta(userId);

                if(meta == null)
                    return NotFound();

                return Ok(meta);
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
    }
}
