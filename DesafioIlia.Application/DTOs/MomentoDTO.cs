using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioIlia.Application.DTOs
{
    public class MomentoDTO
    {
        [Required(ErrorMessage ="Dia e hora são obrigatórios")]
        [MinLength(19)]
        [MaxLength(19)]
        public string DataHora { get; set; }
    }
}
