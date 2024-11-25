namespace GerencidorDeEventos.Service.inteface
{
    public interface IInscricaoService
    {
        Task<dynamic> InscricaoEventoService(int Idevento, string token, string telefone);
        Task<dynamic> RemoveIncricaoEvento(int Idevento, int id_usuario);
        Task<dynamic> GetTodosOsInscritos(int id_evento);
        Task<dynamic> InscricaoMinicrusoService(int IdMinicurso, string token);
        Task<dynamic> RemoveIncricaoMinicurso(int Idminicurso, int id_usuario);
        Task<dynamic> GetTodosOsInscritosMinicurso(int id_minicurso);

        Task<dynamic> InscricaoPalestraService(int IdPalestra, string token);
        Task<dynamic> RemoveIncricaoPalestra(int Idpalestra, int id_usuario);
        Task<dynamic> GetTodosOsInscritosPalestra(int id_palestra);
    }
}
