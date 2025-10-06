using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace controleDespesa.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
       [HttpPost]
       public IActionResult Cadastrar()
        {

            return Created(); // devolver 201
        }

    }
}
