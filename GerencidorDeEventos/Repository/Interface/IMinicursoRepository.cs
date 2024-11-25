using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Repository.Interface
{
    public interface IMinicursoRepository
    {
        Task<Minicurso> CriarMinicurso(Minicurso minicurso);
        Task<Minicurso> AtualizarMinicurso(Minicurso minicurso);
        Task<Minicurso> RemoverMonicurso(Minicurso minicurso);
        Task<Minicurso> GetMinicursoById(int id);
        Task<List<Minicurso>> GetMinicursos();
        bool MinicursoTemParticipante(int minicursoId);
        bool detached(Minicurso minicurso);
        Task<List<Minicurso>> GetMinicursosEvento(int eventoId);

    }
}
