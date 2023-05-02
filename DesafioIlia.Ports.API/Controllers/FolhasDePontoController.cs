using DesafioIlia.Ports.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIlia.Ports.API.Controllers
{
    [ApiController]
    public class FolhasDePontoController : ControllerBase
    {
        /// <summary>
        /// Folhas de Ponto
        /// </summary>
        /// <param name="mes">2018-08</param>
        [HttpGet]
        [Route("/v1/folhas-de-ponto/{mes}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Mensagem), StatusCodes.Status404NotFound)]
        public async Task<Relatorio> geraRelatorioMensal([FromRoute] string mes)
        {
            var lista = new List<Relatorio>();
            
            var regs = new List<Registro>();
            regs.Add(new Registro { Dia = "2018-08", Horarios = new List<string> { "08:00", "12:00", "13:00", "18:00" } });

            return new Relatorio { Mes = "2018-08", HorasDevidas = "PT69H35M5S", HorasTrabalhadas = "PT69H35M5S", HorasExcedentes = "PT69H35M5S", Registros = regs };
        }
    }
}
