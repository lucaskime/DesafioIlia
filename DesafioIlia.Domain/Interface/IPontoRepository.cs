using DesafioIlia.Domain.Entities;

namespace DesafioIlia.Domain.Interface
{
    public interface IPontoRepository
    {
        Task AdicionarAsync(Registro registro);
        Task<List<Registro>> ConsultarAsync(int year, int month, int day = 0);
    }
}
