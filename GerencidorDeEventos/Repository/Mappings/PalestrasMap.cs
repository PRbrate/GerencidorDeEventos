using GerencidorDeEventos.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GerencidorDeEventos.Repository.Mappings
{
    public class PalestrasMap : IEntityTypeConfiguration<Palestra>
    {
        public void Configure(EntityTypeBuilder<Palestra> builder)
        {
            builder.Property(p => p.Descricao)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(p => p.CurriculoPalestrante)
                .HasMaxLength(200)
                .IsUnicode(false);


        }
    }
}
