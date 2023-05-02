using DesafioIlia.Domain.Entities.Ponto;
using DesafioIlia.Domain.Interface.Ponto;
using DesafioIlia.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioIlia.Infra.Data.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        ApplicationDbContext _RegistroContext;

        public RegistroRepository(ApplicationDbContext context)
        {
            _RegistroContext = context;
        }

        public async Task<Registro> CreateAsync(Registro registro)
        {
            _RegistroContext.Add(registro);
            await _RegistroContext.SaveChangesAsync();
            return registro;
        }

        public async Task<IEnumerable<Registro>> GetReportAsync(int month, int year)
        {
            return await _RegistroContext.Registro.Where(w => w.DiaHora.Month == month && w.DiaHora.Year == year).ToListAsync();
        }
    }
}
