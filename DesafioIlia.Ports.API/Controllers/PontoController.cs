using DesafioIlia.Application.Interfaces;
using DesafioIlia.Ports.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIlia.Ports.API.Controllers
{
    /// <summary>
    /// Controle de Ponto
    /// </summary>
    [ApiController]
    public class PontoController : ControllerBase
    {
        private readonly IPontoService _pontoService;

        public PontoController(IPontoService pontoService)
        {
            _pontoService = pontoService;
        }

        /// <summary>
        /// Bater ponto
        /// </summary>
        /// <returns>String</returns>
        /// <remarks>
        /// Exemplo de body: 2018-08-22T08:00:00
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
        public async Task<ActionResult<Registro>> insereBatida(Momento momento)
        {
            if (momento == null)
                return BadRequest("Campo obrigatório");
            await _pontoService.AdicionarAsync(momento.ToDTO());
            return Ok();
        }

        /// <summary>
        /// Folhas de Ponto
        /// </summary>
        /// <param name="mes">2018-08</param>
        [HttpGet]
        [Route("/v1/folhas-de-ponto/{mes}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Relatorio>> geraRelatorioMensal([FromRoute] string mes)
        {
            if (mes == null)
                return BadRequest("Campo obrigatório");
            var data = mes.Split("-");
            var relatorio = await _pontoService.GerarRelatorioAsync(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            return Ok(relatorio);
        }
    }
}
