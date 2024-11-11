using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Service.inteface
{
    public interface IUsuarioService
    {
        Task<Usuario> UsuarioAuthenticatorService(Usuario usuario);
        Task<List<UsuarioDTO>> GetTodosUsuariosService();
        Task<Usuario> CriarUsuarioService(Usuario usuario);
        Task<Usuario> AtualizarUsuarioService(Usuario usuario);
        Task<Usuario> GetUsuarioByIdService(int id);
        Task<Usuario> DeleteUsuarioService(string email);
    }
}
