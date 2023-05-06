using DesafioIlia.Application.Exceptions;
using DesafioIlia.Application.Interfaces;
using DesafioIlia.Application.Models;
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
        /// <summary>
        /// Controle de Ponto
        /// </summary>
        /// <param name="pontoService"></param>
        public PontoController(IPontoService pontoService)
        {
            _pontoService = pontoService;
        }

        /// <summary>
        /// Bater ponto
        /// </summary>
        /// <remarks>
        ///     Exemplo: 
        ///     {
        ///       "dataHora": "2018-08-22T08:00:00"
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("v1/batidas")]
        [ProducesResponseType(typeof(RegistroModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MensagemModel), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> insereBatida(MomentoModel momento)
        {
            try
            {
                var retorno = await _pontoService.AdicionarAsync(momento.dataHora);
                return new ObjectResult(retorno) { StatusCode = StatusCodes.Status201Created };
            }
            catch (BadRequestException ex)
            {
                return new ObjectResult(new MensagemModel(ex.Message)) { StatusCode = StatusCodes.Status400BadRequest };
            }
            catch (ForbiddenException ex)
            {
                return new ObjectResult(new MensagemModel(ex.Message)) { StatusCode = StatusCodes.Status403Forbidden };
            }
            catch (ConflictException ex)
            {
                return new ObjectResult(new MensagemModel(ex.Message)) { StatusCode = StatusCodes.Status409Conflict };
            }

        }

        /// <summary>
        /// Folhas de Ponto
        /// </summary>
        /// <param name="mes">Exemplo: 2018-08</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/v1/folhas-de-ponto/{mes}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> geraRelatorioMensal([FromRoute] string? mes)
        {
            try
            {
                var relatorio = await _pontoService.GerarRelatorioAsync(mes);
                return Ok(relatorio);
            }
            catch (BadRequestException ex)
            {
                return new ObjectResult(new MensagemModel(ex.Message)) { StatusCode = StatusCodes.Status400BadRequest };
            }

        }
    }
}
