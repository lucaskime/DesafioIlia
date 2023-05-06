using DesafioIlia.Application.Models;
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
        Task AdicionarAsync(string momento);
        Task<RelatorioModel> GerarRelatorioAsync(string anoMes);
    }
}
