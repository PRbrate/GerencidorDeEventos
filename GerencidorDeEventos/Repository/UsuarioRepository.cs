using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace GerencidorDeEventos.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseContext _dbcontext;

        public UsuarioRepository(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _dbcontext.Usuarios.Update(usuario);
            await _dbcontext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {

            _dbcontext.Usuarios.Add(usuario);
            await _dbcontext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> DeleteUsuario(Usuario usuario)
        {
            var remove = _dbcontext.Usuarios.Remove(usuario);
            await _dbcontext.SaveChangesAsync();

            return usuario;

        }

        public async Task<List<Usuario>> GetTodosUsuarios()
        {
            List<Usuario> usuarios = await _dbcontext.Usuarios.ToListAsync();

            return usuarios;
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            var usuario = _dbcontext.Usuarios.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            return usuario;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            var usuario = await _dbcontext.Usuarios.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario> UsuarioAuthenticator(Usuario usuario)
        {
            var usuarioAuth = await _dbcontext.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario.Email.ToLower() && x.Senha == usuario.Senha);

            return usuarioAuth;
        }

        public Usuario GetUserByCpf(string cpf)
        {
            var usuario =  _dbcontext.Usuarios.FirstOrDefault(x => x.Cpf.ToLower() == cpf.ToLower());
            return usuario;
        }
    }
}
