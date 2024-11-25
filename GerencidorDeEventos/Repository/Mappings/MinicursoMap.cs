using GerencidorDeEventos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerencidorDeEventos.Repository.Mappings
{
    public class MinicursoMap: IEntityTypeConfiguration<Minicurso>
    {
        public void Configure(EntityTypeBuilder<Minicurso> builder)
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
