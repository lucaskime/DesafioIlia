using DesafioIlia.Ports.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIlia.Ports.API.Controllers
{
    /// <summary>
    /// Batidas
    /// </summary>
    [ApiController]
    public class BatidasController : ControllerBase
    {
        /// <summary>
        /// Bater ponto
        /// </summary>
        /// <returns>String</returns>
        /// <remarks>
        /// Exemplo de body:
        ///
        ///     {
        ///       "dia": "2023-04-30",
        ///       "horarios": [
        ///         "08:00:00",
        ///         "12:00:00",
        ///         "13:00:00",
	    ///         "18:00:00"
		///       ]
		///     }
		///
        /// </remarks>
        [HttpPost]
        [Route("v1/batidas")]
        [ProducesResponseType(typeof(Registro), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status409Conflict)]
        public async Task<IEnumerable<Registro>>insereBatida(Momento  momento)
        {
            return new List<Registro>().ToArray().AsEnumerable(); ;
        }

    }
}