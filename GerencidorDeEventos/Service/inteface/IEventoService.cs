using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Service.inteface
{
    public interface IEventoService
    {
        Task<dynamic> CriarEventoService(EventoFilter evf);
        Task<dynamic> AtualizaEventoService(int id, EventoFilter evf);
        Task<dynamic> DeletarEventoService(int id);
        Task<dynamic> GetEventosPorPeriodo(PeriodoRetorno periodo);
        Task<dynamic> GetEventosProgramacaoEvento(int eventoId);
        Task<List<Evento>> GetEventos();
    }
}
