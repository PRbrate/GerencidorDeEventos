using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> UsuarioAuthenticator(Usuario usuario);
        Task<List<Usuario>> GetTodosUsuarios();
        Task<Usuario> CriarUsuario(Usuario usuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task<Usuario> GetUsuarioById(int id);
        Usuario GetUsuarioByEmail(string email);
        Task<Usuario> DeleteUsuario(Usuario usuario);
        Usuario GetUserByCpf(string cpf);


    }
}
