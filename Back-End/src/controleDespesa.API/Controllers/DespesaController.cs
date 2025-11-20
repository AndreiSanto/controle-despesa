using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service;
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

    public class DespesaController : ControllerBase
    {
        private readonly IDespesaAppService _despesaAppService;

        public DespesaController(IDespesaAppService despesaAppService)
        {
            _despesaAppService = despesaAppService;
        }

        [HttpPost("Cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] DespesaDTO despesaDTO)
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
                

                var despesa = await _despesaAppService.Cadastro(despesaDTO, userId);

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


        [HttpGet("ListarDespesas")]
        public async Task<IActionResult> ListarDespesas([FromQuery] FiltroDTO filtro,
[FromQuery] int pagina, [FromQuery] int totalPagina)
        {


            try
            {
                var despesas = await _despesaAppService.DespesaLista(pagina,totalPagina, filtro);

                return Ok(despesas);
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

        [HttpGet("ListarCategoriaDespesa")]
        public async Task<IActionResult> ListarTipoCategoria()
        {


            try
            {
                var categorias = await _despesaAppService.ListarCategoriasDespesa();

                return Ok(categorias);
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
       
        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {

                var excluido = await _despesaAppService.ExcluirAsync(id);

                return Ok(new { message = "Receita excluída com sucesso." });


            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                        new { Erro = "Ocorreu um erro ao excluir uma despesa.", Detalhes = ex.Message });


            }

        }


        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {

            try
            {

                var despesa = await _despesaAppService.BuscarDespesa(id);

                return Ok(despesa);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                       new { Erro = "Ocorreu um erro ao obter uma despesa.", Detalhes = ex.Message });
            }
        }

        [HttpPut("Atualizar")]

        public async Task<IActionResult> Atualizar([FromBody] DespesaDTO despesa)
        {
            try
            {

                await _despesaAppService.AtualizarAsync(despesa);

                return Ok();

            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao atualizar uma depesa.", Detalhes = ex.Message });
            }
        }

    }
}
