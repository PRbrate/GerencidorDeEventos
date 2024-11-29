using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Repository.Interface
{
    public interface IInscricoesRepository
    {
        Task<InscricaoEvento> CriarInscricaoEvento(InscricaoEvento inscricaoEvento);
        //Task<InscricaoEvento> GetInscricaoEvento(int idevento, int id_usuario);
        Task<bool> RemoverInscricaoEvento(int eventoId, int usuarioId);
        Task<InscricaoEvento> GetInscricao(int eventoId, int usuarioId);
        bool EventoTemParticipante(int eventoId);
        bool EventoTemPalestra(int eventoId);
        bool EventoTemMinicurso(int eventoId);
        List<Usuario> ObterUsuariosInscritosEvento(int eventoId);
        Task<InscricaoMinicurso> CriarInscricaoMinicurso(InscricaoMinicurso inscricaoMinicurso);
        Task<bool> RemoverInscricaoMinicurso(int minicursoId, int usuarioId);
        bool MinicursoTemParticipante(int minicursoId);
        List<Usuario> ObterUsuariosInscritosMinicurso(int minicursoid);

        //Task<InscricaoPalestra> CriarInscricaoPalestra(InscricaoPalestra inscricaoPalestra);
        //Task<bool> RemoverInscricaoPalestra(int palestraId, int usuarioId);
        //bool PalestraTemParticipante(int palestraId);
        //List<Usuario> ObterUsuariosInscritosPalestra(int palestraId);

    }
}
