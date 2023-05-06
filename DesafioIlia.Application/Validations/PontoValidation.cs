using DesafioIlia.Application.Interfaces;
using DesafioIlia.Domain.Interface;
using System.Globalization;
using System.Text.RegularExpressions;
using static DesafioIlia.Application.Validations.ApplicationExceptionValidation;

namespace DesafioIlia.Application.Validations
{
    public class PontoValidation: IPontoValidation
    {

        private IPontoRepository _pontoRepository;

        public PontoValidation(IPontoRepository pontoRepository)
        {
            _pontoRepository = pontoRepository ??
                throw new ArgumentNullException(nameof(pontoRepository));
        }

        public async Task AdicionarValidateAsync(string diaHora)
        {
            When(string.IsNullOrWhiteSpace(diaHora),
                "Campo obrigatório não informado", ErrorType.BadRequest);

            When(!DateTime.TryParseExact(diaHora, "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out var outDiaHora),
                "Data e hora em formato inválido", ErrorType.BadRequest);

            var registro = outDiaHora.ToUniversalTime();
            var registrosDoDia = await _pontoRepository.ConsultarAsync(registro.Year, registro.Month, registro.Day);

            When(registrosDoDia.Count >= 4,
                "Apenas 4 horários podem ser registrados por dia", ErrorType.Forbidden);

            When((registrosDoDia.Count == 2 && (registro.Subtract(registrosDoDia[1].DiaHora) < new TimeSpan(1, 0, 0))),
                "Deve haver no mínimo 1 hora de almoço", ErrorType.Forbidden);

            When(registro.DayOfWeek == DayOfWeek.Saturday || registro.DayOfWeek == DayOfWeek.Sunday,
                "Sábado e domingo não são permitidos como dia de trabalho", ErrorType.Forbidden);

            When(registrosDoDia.Where(w => w.DiaHora == registro).Any(),
                "Horário já registrado", ErrorType.Conflict);
        }

        public void GerarRelatorioValidate(string anoMes)
        {
            When(string.IsNullOrWhiteSpace(anoMes),
                "Campo obrigatório não informado", ErrorType.BadRequest);

            When(!Regex.Match(anoMes, @"^(19[0-9]{2}|20[0-9]{2}|2100)-(0[1-9]|1[0-2])$").Success,
                "Data e hora em formato inválido", ErrorType.BadRequest);

        }
    }
}
