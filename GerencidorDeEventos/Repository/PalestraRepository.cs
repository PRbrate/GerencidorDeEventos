using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerencidorDeEventos.Repository
{
    public class PalestraRepository : IPalestraRepository
    {
        private readonly DataBaseContext _dbcontext;

        public PalestraRepository(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Palestra> CriarPalestra(Palestra palestra)
        {
            ConverterDateTimeParaUtc(palestra);
            _dbcontext.Palestras.Add(palestra);
            await _dbcontext.SaveChangesAsync();
            return palestra;
        }

        public async Task<Palestra> AtualizarPalestra(Palestra palestra)
        {
            ConverterDateTimeParaUtc(palestra);
            _dbcontext.Palestras.Update(palestra);
            await _dbcontext.SaveChangesAsync();
            return palestra;
        }

        public async Task<Palestra> RemoverPalestra(Palestra palestra)
        {
            _dbcontext.Palestras.Remove(palestra);
            await _dbcontext.SaveChangesAsync();
            return palestra;
        }

        public async Task<Palestra> GetPalestrasById(int id)
        {
            var palestra =  _dbcontext.Palestras.Find(id);
            return palestra;
        }

        public bool PalestraTemParticipante(int palestraId)
        {
            return _dbcontext.InscricoesPalestra.Any(i => i.PalestraId == palestraId);
        }

        public async Task<List<Palestra>> GetPalestras()
        {
            var palestras = await _dbcontext.Palestras.ToListAsync();
            return palestras;
        }

        public async Task<List<Palestra>> GetPalestrasEvento(int eventoId)
        {
            return _dbcontext.Palestras
            .Where(m => m.EventoId == eventoId)
            .ToList();
        }

        public bool detached(Palestra palestra)
        {
            _dbcontext.Entry(palestra).State = EntityState.Detached;
            return true;
        }

        private void ConverterDateTimeParaUtc(Palestra palestra)
        {
            var propriedadesDateTime = typeof(Palestra).GetProperties()
                .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

            foreach (var propriedade in propriedadesDateTime)
            {
                var valorAtual = propriedade.GetValue(palestra) as DateTime?;

                if (valorAtual.HasValue && valorAtual.Value.Kind == DateTimeKind.Unspecified)
                {
                    propriedade.SetValue(palestra, DateTime.SpecifyKind(valorAtual.Value, DateTimeKind.Utc));
                }
            }
        }

    }
}
