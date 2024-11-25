using GerencidorDeEventos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GerencidorDeEventos.Repository.Mappings
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.Property(p => p.Descricao)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(p => p.CpfResponsavel)
                .HasMaxLength(14)
                .IsUnicode(true);


        }
    }
}
