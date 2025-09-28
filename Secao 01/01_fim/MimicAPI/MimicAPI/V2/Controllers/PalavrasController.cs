using Microsoft.AspNetCore.Mvc;

namespace MimicAPI.V2.Controllers
{
    [ApiController]
    //[Route("/api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class PalavrasController : ControllerBase
    {
        // APP -- /api/palavras
        [HttpGet("", Name = "ObterTodas")]
        public string ObterTodas()
        {
            return "Versão 2,0";
        }
    }
}
