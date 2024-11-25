using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerencidorDeEventos.Repository
{
    public class MinicursoRepository : IMinicursoRepository
    {
        private readonly DataBaseContext _dbcontext;

        public MinicursoRepository(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Minicurso> CriarMinicurso(Minicurso minicurso)
        {
            ConverterDateTimeParaUtc(minicurso);
            _dbcontext.Minicursos.Add(minicurso);
            await _dbcontext.SaveChangesAsync();
            return minicurso;
        }

        public async Task<Minicurso> AtualizarMinicurso(Minicurso minicurso)
        {
            ConverterDateTimeParaUtc(minicurso);
            _dbcontext.Minicursos.Update(minicurso);
            await _dbcontext.SaveChangesAsync();
            return minicurso;
        }

        public async Task<Minicurso> RemoverMonicurso(Minicurso minicurso)
        {
            _dbcontext.Minicursos.Remove(minicurso);
            await _dbcontext.SaveChangesAsync();
            return minicurso;
        }

        public async Task<Minicurso> GetMinicursoById(int id)
        {
            var minicurso = await _dbcontext.Minicursos.FindAsync(id);
            return minicurso;
        }

        public bool MinicursoTemParticipante(int minicursoId)
        {
            return _dbcontext.InscricoesMinicurso.Any(i => i.MinicursoId == minicursoId);
        }

        public async Task<List<Minicurso>> GetMinicursos()
        {
            var minicursos = await _dbcontext.Minicursos.ToListAsync();
            return minicursos;
        }

        public async Task<List<Minicurso>> GetMinicursosEvento(int eventoId)
        {
            return _dbcontext.Minicursos
            .Where(m => m.EventoId == eventoId)
            .ToList();
        }

        public bool detached(Minicurso minicurso)
        {
            _dbcontext.Entry(minicurso).State = EntityState.Detached;
            return true;
        }

        private void ConverterDateTimeParaUtc(Minicurso minicurso)
        {
            var propriedadesDateTime = typeof(Minicurso).GetProperties()
                .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

            foreach (var propriedade in propriedadesDateTime)
            {
                var valorAtual = propriedade.GetValue(minicurso) as DateTime?;

                if (valorAtual.HasValue && valorAtual.Value.Kind == DateTimeKind.Unspecified)
                {
                    propriedade.SetValue(minicurso, DateTime.SpecifyKind(valorAtual.Value, DateTimeKind.Utc));
                }
            }
        }

    }
}
