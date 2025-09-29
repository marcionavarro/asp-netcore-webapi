using Microsoft.AspNetCore.Mvc;

namespace MimicAPI.V2.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PalavrasController : ControllerBase
    {
        /// <summary>
        /// Operação que pega do banco de dados todas as palavras existentes
        /// </summary>
        /// <param name="query">Filtros de pesquisa</param>
        [MapToApiVersion("2.0")]
        [HttpGet("", Name = "ObterTodas")]
        public string ObterTodas()
        {
            return "Versão 2,0";
        }
    }
}
