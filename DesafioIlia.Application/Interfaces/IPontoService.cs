using DesafioIlia.Application.Models;

namespace DesafioIlia.Application.Interfaces
{
    public interface IPontoService
    {
        Task<RegistroModel> AdicionarAsync(string momento);
        Task<RelatorioModel> GerarRelatorioAsync(string anoMes);
    }
}
