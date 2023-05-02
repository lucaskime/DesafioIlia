using DesafioIlia.Domain.Entities.Ponto;

namespace DesafioIlia.Domain.Interface.Ponto
{
    public interface IRegistroRepository
    {
        Task<Registro> CreateAsync(Registro registro);
        Task<IEnumerable<Registro>> GetReportAsync(int month, int year);
    }
}
