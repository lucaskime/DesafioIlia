using AutoMapper;
using DesafioIlia.Application.DTOs;
using DesafioIlia.Domain.Entities.Ponto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioIlia.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Registro, MomentoDTO>().ReverseMap();
        }
    }
}
