using DesafioIlia.Domain.Entities;
using DesafioIlia.Domain.Interface;
using DesafioIlia.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioIlia.Infra.Data.Repositories
{
    public class PontoRepository : IPontoRepository
    {
        ApplicationDbContext _RegistroContext;

        public PontoRepository(ApplicationDbContext context)
        {
            _RegistroContext = context;
        }

        public async Task AdicionarAsync(Registro registro)
        {
            _RegistroContext.Registro.Add(registro);
            await _RegistroContext.SaveChangesAsync();
        }

        public async Task<List<Registro>> ConsultarAsync(int year, int month, int day = 0)
        {
            if (day == 0)
            {
                return await _RegistroContext.Registro.Where(w => w.DiaHora.Year == year && w.DiaHora.Month == month).ToListAsync();
            }
            return await _RegistroContext.Registro.Where(w => w.DiaHora.Year == year && w.DiaHora.Month == month && w.DiaHora.Day == day).ToListAsync();
        }
    }
}
