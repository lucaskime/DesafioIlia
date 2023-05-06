using DesafioIlia.Application.Models;
using DesafioIlia.Application.Interfaces;
using DesafioIlia.Domain.Entities;
using DesafioIlia.Domain.Interface;
using DesafioIlia.Application.Exceptions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DesafioIlia.Application.Services
{
    public class PontoService : IPontoService
    {
        private IPontoRepository _pontoRepository;

        public PontoService(IPontoRepository pontoRepository)
        {
            _pontoRepository = pontoRepository ??
                throw new ArgumentNullException(nameof(pontoRepository));
        }

        public async Task AdicionarAsync(string momento)
        {
            // formato | vazio | 4 horaris por dia | fds não pode | horario igual
            if (string.IsNullOrWhiteSpace(momento))
                throw new BadRequestException("Não pode vazio");

            if (!DateTime.TryParseExact(momento, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dataHora))
            {
                throw new BadRequestException("Data inválida");
            }

            var registro = dataHora.ToUniversalTime();
            var registrosDoDia = await _pontoRepository.ConsultarAsync(registro.Year, registro.Month, registro.Day);

            if (registrosDoDia.Where(w => w.DiaHora == registro).Any())
            {
                throw new ConflictException("Não pode data igual");
            }
            if (registrosDoDia != null)
            {
                if (registrosDoDia.Count >= 4){
                    throw new ForbiddenException("Pode apenas 4 registros por dia");
                }
                if (registrosDoDia.Count == 2)
                {
                    if (registro.Subtract(registrosDoDia[1].DiaHora) < new TimeSpan(1,0,0)) 
                    throw new ForbiddenException("Necessário dar intervalo de 1 hora para almoço");
                }
            }
            var valor = new Registro(Convert.ToDateTime(momento).ToUniversalTime());
            await _pontoRepository.AdicionarAsync(valor);

        }

        public async Task<RelatorioModel> GerarRelatorioAsync(string anoMes)
        {
            if (string.IsNullOrWhiteSpace(anoMes))
                throw new BadRequestException("Não pode vazio");

            if (!Regex.Match(anoMes, @"^(19[0-9]{2}|20[0-9]{2}|2100)-(0[1-9]|1[0-2])$").Success)
            {
                throw new BadRequestException("Data inválida");
            }

            var anoMesSplit = anoMes.Split('-');
            int ano = Convert.ToInt16(anoMesSplit[0]);
            int mes = Convert.ToInt16(anoMesSplit[1]); 

            var relatorio = new RelatorioModel();

            var registros = await _pontoRepository.ConsultarAsync(ano, mes);
            var horarios = registros.Select(s => s.DiaHora).Order().ToList();
            var horasDevidas = TimeSpan.FromHours(CalcularDiasUteis(ano, mes) * 8);
            var horasTrabalhadas = CalcularTempoTrabalhado(horarios);

            // Calculo é de horas excedentes e não diferença de horas. Caso valor negativo, zerar.
            var horasExcedentes = horasTrabalhadas - horasDevidas;
            horasExcedentes = (horasExcedentes > TimeSpan.Zero ? horasExcedentes : TimeSpan.Zero);

            relatorio.Mes = $"{ano.ToString("0000")}-{mes.ToString("00")}";
            relatorio.HorasDevidas = FormataHoraData(horasDevidas);
            relatorio.HorasTrabalhadas = FormataHoraData(horasTrabalhadas);
            relatorio.HorasExcedentes = FormataHoraData(horasExcedentes);
            relatorio.Registros = horarios;

            return relatorio;
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

