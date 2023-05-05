using DesafioIlia.Domain.Entities;

namespace DesafioIlia.Application.DTOs
{
    public class RelatorioDTO
    {
        public string Mes { get; set; }
        public string HorasTrabalhadas { get; set; }
        public string HorasDevidas { get; set; }
        public string HorasExcedentes { get; set; }
        public List<DateTime> Registros { get; set; }
    }
}
