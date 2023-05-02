using DesafioIlia.Domain.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesafioIlia.Domain.Entities.Ponto
{
    public sealed class Registro
    {
        public int Id { get; private set; }

        public DateTime DiaHora { get; }

        public Registro(DateTime diaHora)
        {
            ValidateDomain(diaHora);
            DiaHora = diaHora;
        }

        private void ValidateDomain(DateTime diaHora)
        {
            DomainExceptionValidation.When(diaHora > DateTime.Now,
                "Horário não pode ser maior que atual.");

            DomainExceptionValidation.When(diaHora.DayOfWeek == DayOfWeek.Saturday || diaHora.DayOfWeek == DayOfWeek.Sunday,
                "Horário não pode ser no final de semana.");
        }
    }
}
