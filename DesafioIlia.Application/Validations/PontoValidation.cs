using DesafioIlia.Domain.Interface;
using System.Globalization;
using static DesafioIlia.Application.Validations.ApplicationExceptionValidation;

namespace DesafioIlia.Application.Validations
{
    public class PontoValidation
    {

        private IPontoRepository _pontoRepository;

        public PontoValidation(IPontoRepository pontoRepository)
        {

            _pontoRepository = pontoRepository ??
                throw new ArgumentNullException(nameof(pontoRepository));
        }

        private async Task AdicionarValidate(string diaHora)
        {
            When(string.IsNullOrWhiteSpace(diaHora),
                "Campo obrigatório não informado", ErrorType.BadRequest);

            When(!DateTime.TryParseExact(diaHora, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var outDiaHora),
                "Data e hora em formato inválido", ErrorType.BadRequest);


            var registro = outDiaHora.ToUniversalTime();
            var registrosDoDia = await _pontoRepository.ConsultarAsync(registro.Year, registro.Month, registro.Day);

            When(registrosDoDia.Count >= 4,
                "Apenas 4 horários podem ser registrados por dia", ErrorType.Forbidden);

            When((registrosDoDia.Count == 2 && (registro.Subtract(registrosDoDia[1].DiaHora) < new TimeSpan(1, 0, 0))),
                "Deve haver no mínimo 1 hora de almoço", ErrorType.Forbidden);

            When(registro.DayOfWeek == DayOfWeek.Saturday || registro.DayOfWeek == DayOfWeek.Sunday,
                "Sábado e domingo não são permitidos como dia de trabalho", ErrorType.Forbidden);
        }
    }
}
