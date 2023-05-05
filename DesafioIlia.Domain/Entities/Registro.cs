using DesafioIlia.Domain.Validation;

namespace DesafioIlia.Domain.Entities
{
    public sealed class Registro
    {
        public int Id { get; private set; }

        public DateTime DiaHora { get; set; }

        public Registro(DateTime diaHora)
        {
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
