using System.Text.Json.Serialization;

namespace DesafioIlia.Application.DTOs
{
    public class RegistroDTO
    {   public string Dia { get; set; }
        public List<DateTime> Horarios { get; set; }
    }
}
