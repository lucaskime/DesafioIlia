using DesafioIlia.Application.Models;
using DesafioIlia.Application.Interfaces;
using DesafioIlia.Domain.Entities;
using DesafioIlia.Domain.Interface;
using System.Globalization;

namespace DesafioIlia.Application.Services
{
    public class PontoService : IPontoService
    {
        private IPontoRepository _pontoRepository;
        private IPontoValidation _pontoValidation;

        public PontoService(IPontoRepository pontoRepository,
                            IPontoValidation pontoValidation)
        {
            _pontoRepository = pontoRepository ??
                throw new ArgumentNullException(nameof(pontoRepository));
            _pontoValidation = pontoValidation ??
                throw new ArgumentNullException(nameof(pontoValidation));
        }

        public async Task<RegistroModel> AdicionarAsync(string momento)
        {
            await _pontoValidation.AdicionarValidateAsync(momento);
            var data = new Registro(Convert.ToDateTime(momento));

            await _pontoRepository.AdicionarAsync(data);
            var registros = await _pontoRepository.ConsultarAsync(data.DiaHora.Year, data.DiaHora.Month, data.DiaHora.Day);

            return new RegistroModel()
            {
                dia = data.DiaHora.ToString("yyyy-MM"),
                horarios = registros.Select(s => s.DiaHora.ToString("HH:mm:ss")).ToList()
            };
        }

        public async Task<RelatorioModel> GerarRelatorioAsync(string anoMes)
        {
            _pontoValidation.GerarRelatorioValidate(anoMes);

            DateTime.TryParseExact(anoMes, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var outAnoMes);

            var registros = await _pontoRepository.ConsultarAsync(outAnoMes.Year, outAnoMes.Month);
            var horarios = registros.Select(s => s.DiaHora).Order().ToList();
            var horasDevidas = TimeSpan.FromHours(CalcularDiasUteis(outAnoMes.Year, outAnoMes.Month) * 8);
            var horasTrabalhadas = CalcularTempoTrabalhado(horarios);

            var horasExcedentes = horasTrabalhadas - horasDevidas;
            horasExcedentes = (horasExcedentes > TimeSpan.Zero ? horasExcedentes : TimeSpan.Zero);

            return new RelatorioModel()
            {
                Mes = outAnoMes.ToString("yyyy-MM"),
                HorasDevidas = FormataHoraData(horasDevidas),
                HorasTrabalhadas = FormataHoraData(horasTrabalhadas),
                HorasExcedentes = FormataHoraData(horasExcedentes),
                Registros = horarios
            };
        }

        private string FormataHoraData(TimeSpan horaData)
        {
            return $"PT{(int)horaData.TotalHours}H{horaData.Minutes:D2}M{horaData.Seconds:D2}S";
        }

        private int CalcularDiasUteis(int ano, int mes)
        {
            DateTime primeiroDia = new DateTime(ano, mes, 1);
            DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);

            int diasUteis = 0;

            for (DateTime dia = primeiroDia; dia <= ultimoDia; dia = dia.AddDays(1))
            {
                if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }
            }
            return diasUteis;
        }

        private TimeSpan CalcularTempoTrabalhado(List<DateTime> Horarios)
        {
            var horarios = Horarios.Order().ToList();

            TimeSpan contador = TimeSpan.Zero;
            bool entradaFlag = true;
            DateTime primeiroHorario = DateTime.MinValue;

            foreach (var registro in horarios)
            {
                if (entradaFlag || primeiroHorario.Day != registro.Day)
                {
                    primeiroHorario = registro;
                    entradaFlag = false;
                }
                else
                {
                    contador = contador + registro.Subtract(primeiroHorario);
                    entradaFlag = true;
                }
            }

            return contador;
        }
    }
}
