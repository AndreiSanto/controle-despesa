using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
       [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar()
        {

            return Created(); // devolver 201
        }

    }
}
