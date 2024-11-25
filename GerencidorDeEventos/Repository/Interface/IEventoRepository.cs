using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Repository.Interface
{
    public interface IEventoRepository
    {
        Task<Evento> CriarEvento(Evento evento);
        Task<List<Evento>> GetEventos();
        Task<Evento> GetEventoById(int id);
        Task<Evento> AtualizaEvento(Evento evento);
        Task<Evento> DetelarEvento(Evento evento);
        bool detached(Evento evento);
        Task<List<Evento>> GetEventosPorPeriodo(PeriodoRetorno periodo);
    }
}
