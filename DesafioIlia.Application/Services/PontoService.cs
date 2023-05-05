using DesafioIlia.Application.DTOs;
using DesafioIlia.Application.Interfaces;
using DesafioIlia.Domain.Entities;
using DesafioIlia.Domain.Interface;

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

        public async Task AdicionarAsync(MomentoDTO momento)
        {
            var valor = new Registro(Convert.ToDateTime(momento.DataHora).ToUniversalTime());
            await _pontoRepository.AdicionarAsync(valor);
        }

        public Task<RegistroDTO> ConsultarAsync(int ano, int mes, int dia = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<RelatorioDTO> GerarRelatorioAsync(int ano, int mes)
        {
            var relatorio = new RelatorioDTO();

            var registros = await _pontoRepository.ConsultarAsync(ano, mes);
            var horarios = registros.Select(s => s.DiaHora).Order().ToList();
            var horasDevidas = TimeSpan.FromHours(CalcularDiasUteis(ano, mes) * 8);
            var horasTrabalhadas = CalcularTempoTrabalhado(horarios);
            var horasExcedentes = horasTrabalhadas - horasDevidas;

            relatorio.Mes = $"{ano.ToString("0000")}-{mes.ToString("00")}";
            relatorio.HorasDevidas = FormataHoraData(horasDevidas);
            relatorio.HorasTrabalhadas = FormataHoraData(horasTrabalhadas);
            relatorio.HorasExcedentes = FormataHoraData(horasExcedentes);
            relatorio.Registros = horarios;

            return relatorio;
        }

        #region Metodos Auxiliares

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

            //horarios.ToList().ForEach(registro =>
            //{
            //    if (entradaFlag || primeiroHorario.Day != registro.DiaHora.Day)
            //    {
            //        primeiroHorario = registro.DiaHora;
            //        entradaFlag = false;
            //    }
            //    else
            //    {
            //        contador = contador + registro.DiaHora.Subtract(primeiroHorario);
            //        entradaFlag = true;
            //    }
            //});

            return contador;
        }

        #endregion
    }
}

