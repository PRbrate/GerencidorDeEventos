using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Repository.Interface
{
    public interface IPalestraRepository
    {
        Task<Palestra> CriarPalestra(Palestra palestra);
        Task<Palestra> AtualizarPalestra(Palestra palestra);
        Task<Palestra> RemoverPalestra(Palestra palestra);
        Task<Palestra> GetPalestrasById(int id);
        Task<List<Palestra>> GetPalestras();
        bool PalestraTemParticipante(int palestraId);
        bool detached(Palestra palestra);
        Task<List<Palestra>> GetPalestrasEvento(int eventoId);
    }
}
