using System.ComponentModel.DataAnnotations;

namespace DesafioIlia.Application.DTOs
{
    public class MomentoDTO
    {
        [Required(ErrorMessage ="Dia e hora são obrigatórios")]
        [MinLength(19)]
        [MaxLength(19)]
        public string? DataHora { get; set; }
    }
}
