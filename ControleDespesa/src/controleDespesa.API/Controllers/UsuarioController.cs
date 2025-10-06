using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("Cadastro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastro()
        {
            var novoItem = new
            {
                Id = Guid.NewGuid(),
                Nome = "Mouse Gamer",
                Preco = 150.00m
            };
            return Created(string.Empty,novoItem);
        }
    }
}
