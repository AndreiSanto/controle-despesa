using controleDespesa.Application.DTOs;
using controleDespesa.Application.Service;
using controleDespesa.Application.Service.Interfaces;
using controleDespesa.Domain.Entities;
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

    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaAppService _receitaAppService;

        public ReceitaController(IReceitaAppService receitaAppService)
        {
            _receitaAppService = receitaAppService;
        }

        [HttpPost("Cadastro")]

        public async Task<IActionResult> Cadastrar([FromBody] ReceitaDTO receitaDTO)
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var receita = await _receitaAppService.Cadastro(receitaDTO,userId);

                return Created(string.Empty, receita);
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
        [HttpGet("ListarCategoriaReceita")]
        public async Task<IActionResult> ListarTipoCategoria()
        {


            try
            {
                var categorias = await _receitaAppService.ListarCategoriasReceita();

                return Ok(categorias);
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

        [HttpGet("ListarReceitas")]
        public async Task<IActionResult> ListarReceitas([FromQuery] int pagina, [FromQuery] int totalPagina, [FromQuery] FiltroDTO filtro)
        {


            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var receitas = await _receitaAppService.ReceitaLista(pagina, totalPagina, filtro, userId);

                return Ok(receitas);
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

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {

                var excluido = await _receitaAppService.Excluir(id);

                return Ok(new { message = "Receita excluída com sucesso." });


            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                        new { Erro = "Ocorreu um erro ao excluir uma receita.", Detalhes = ex.Message });


            }

        }


        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {

            try {

                var receita = await _receitaAppService.BuscarReceita(id);

                return Ok(receita);

            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                       new { Erro = "Ocorreu um erro ao obter uma receita.", Detalhes = ex.Message });
            }
         
        }

        [HttpPut("Atualizar")]

        public async Task<IActionResult> Atualizar([FromBody] ReceitaDTO receita)
        {
            try
            {

                await _receitaAppService.AtualizarAsync(receita);

                return Ok();

            }
            catch (ValidationException ex)
            {

                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Erro = "Ocorreu um erro ao atualizar uma receita.", Detalhes = ex.Message });
            }
        }


    }
}

    

