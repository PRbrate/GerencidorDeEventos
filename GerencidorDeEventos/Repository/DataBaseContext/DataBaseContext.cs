using GerencidorDeEventos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GerencidorDeEventos.Repository
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestra> Palestras { get; set; }
        public DbSet<Minicurso> Minicursos { get; set; }
        public DbSet<InscricaoEvento> InscricoesEvento { get; set; }
        public DbSet<InscricaoMinicurso> InscricoesMinicurso { get; set; }
        public DbSet<InscricaoPalestra> InscricoesPalestra { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(100);

            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(18, 2);


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);

            modelBuilder.Entity<InscricaoEvento>()
            .HasKey(i => new { i.EventoId, i.UsuarioId });

            modelBuilder.Entity<InscricaoEvento>()
                .HasOne(i => i.Evento)
                .WithMany(e => e.Inscricoes)
                .HasForeignKey(i => i.EventoId);

            modelBuilder.Entity<InscricaoEvento>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.Inscricoes)
                .HasForeignKey(i => i.UsuarioId);

            modelBuilder.Entity<Minicurso>()
               .HasOne(m => m.Evento)
               .WithMany(e => e.Minicursos)
               .HasForeignKey(m => m.EventoId);

            modelBuilder.Entity<InscricaoMinicurso>()
                .HasKey(im => new { im.UsuarioId, im.MinicursoId });

            modelBuilder.Entity<InscricaoMinicurso>()
                .HasOne(im => im.Usuario)
                .WithMany(u => u.InscricoesMinicurso)
                .HasForeignKey(im => im.UsuarioId);

            modelBuilder.Entity<InscricaoMinicurso>()
                .HasOne(im => im.Minicurso)
                .WithMany(m => m.Inscricoes)
                .HasForeignKey(im => im.MinicursoId);



            modelBuilder.Entity<Palestra>()
               .HasOne(m => m.Evento)
               .WithMany(e => e.Palestras)
               .HasForeignKey(m => m.EventoId);

            modelBuilder.Entity<InscricaoPalestra>()
                .HasKey(im => new { im.UsuarioId, im.PalestraId });

            modelBuilder.Entity<InscricaoPalestra>()
                .HasOne(im => im.Usuario)
                .WithMany(u => u.InscricaoPalestras)
                .HasForeignKey(im => im.UsuarioId);

            modelBuilder.Entity<InscricaoPalestra>()
                .HasOne(im => im.Palestra)
                .WithMany(m => m.Inscricoes)
                .HasForeignKey(im => im.PalestraId);
        }

    }
}
