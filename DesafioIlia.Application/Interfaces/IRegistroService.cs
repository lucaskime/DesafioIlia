using DesafioIlia.Application.DTOs;
using DesafioIlia.Domain.Entities.Ponto;

namespace DesafioIlia.Application.Interfaces
{
    public interface IRegistroService
    {
        Task<Registro> Add(MomentoDTO momento);
    }
}
