using DesafioIlia.Application.DTOs;
using DesafioIlia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioIlia.Application.Interfaces
{
    public interface IPontoService
    {
        Task AdicionarAsync(MomentoDTO momento);
        Task<RegistroDTO> ConsultarAsync(int ano, int mes, int dia = 0);
        Task<RelatorioDTO> GerarRelatorioAsync(int ano, int mes);
    }
}
