using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerencidorDeEventos.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly DataBaseContext _dbcontext;

        public EventoRepository(DataBaseContext context)
        {
            _dbcontext = context;
        }

        public async Task<Evento> CriarEvento(Evento evento)
        {
            ConverterDateTimeParaUtc(evento);
            _dbcontext.Eventos.Add(evento);
            await _dbcontext.SaveChangesAsync();
            return evento;
        }

        public async Task<Evento> GetEventoById(int id)
        {
            var evento = await _dbcontext.Eventos.FindAsync(id);
            return evento;
        }

        public async Task<List<Evento>> GetEventos()
        {
            var eventos = await _dbcontext.Eventos.ToListAsync();
            return eventos;
        }

        public async Task<Evento> AtualizaEvento(Evento evento)
        {
            ConverterDateTimeParaUtc(evento);
            _dbcontext.Eventos.Update(evento);
            await _dbcontext.SaveChangesAsync();
            return evento;
        }

        public bool detached(Evento evento)
        {
            _dbcontext.Entry(evento).State = EntityState.Detached;
            return true;
        }

        public async Task<Evento> DetelarEvento(Evento evento)
        {
            _dbcontext.Eventos.Remove(evento);
            await _dbcontext.SaveChangesAsync();
            return evento;
        }

        public async Task<List<Evento>> GetEventosPorPeriodo(PeriodoRetorno periodo)
        {
            SemCabecaParaPensar(periodo);
            return _dbcontext.Eventos
           .Where(e => e.DataInicio >= periodo.Inicio && e.DataFim <= periodo.Fim)
           .ToList();
        }

        private void ConverterDateTimeParaUtc(Evento entidade)
        {
            var propriedadesDateTime = typeof(Evento).GetProperties()
                .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

            foreach (var propriedade in propriedadesDateTime)
            {
                var valorAtual = propriedade.GetValue(entidade) as DateTime?;

                if (valorAtual.HasValue && valorAtual.Value.Kind == DateTimeKind.Unspecified)
                {
                    propriedade.SetValue(entidade, DateTime.SpecifyKind(valorAtual.Value, DateTimeKind.Utc));
                }
            }
        }

        private void SemCabecaParaPensar(PeriodoRetorno entidade)
        {
            var propriedadesDateTime = typeof(PeriodoRetorno).GetProperties()
                .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

            foreach (var propriedade in propriedadesDateTime)
            {
                var valorAtual = propriedade.GetValue(entidade) as DateTime?;

                if (valorAtual.HasValue && valorAtual.Value.Kind == DateTimeKind.Unspecified)
                {
                    propriedade.SetValue(entidade, DateTime.SpecifyKind(valorAtual.Value, DateTimeKind.Utc));
                }
            }
        }


    }


}
