using DesafioIlia.Domain.Entities.Ponto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioIlia.Infra.Data.EntitiesConfiguration
{
    public class RegistroConfiguration : IEntityTypeConfiguration<Registro>
    {
        public void Configure(EntityTypeBuilder<Registro> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.DiaHora).IsRequired();
            builder.HasIndex(i => i.DiaHora).IsDescending();
        }
    }
}
