using GerencidorDeEventos.Dtos;
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
            var usuarioBanco =  GetUsuarioByEmail(usuario.Email);

            if(usuarioBanco != null) 
            {
                _dbcontext.Usuarios.Update(usuarioBanco);
                await _dbcontext.SaveChangesAsync();
                return usuarioBanco;
            }
            throw new Exception("Usuário Não encontrado para a atualização");

        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            var contains = GetUsuarioByEmail(usuario.Email);
            

            if (contains != null)
            {
                throw new Exception("Já contem um usuário com o e-mail digitado, por favor alterar o e-mail");
            }

            _dbcontext.Usuarios.Add(usuario);
            await _dbcontext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> DeleteUsuario(string email)
        {
            try
            {
                var user = GetUsuarioByEmail(email);

                var remove = _dbcontext.Usuarios.Remove(user);
                await _dbcontext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw new Exception("Usuário não Encontrado, não pôde ser removido!");
            }
        }

        public async Task<List<UsuarioDTO>> GetTodosUsuarios()
        {
            List<Usuario> usuarios = await _dbcontext.Usuarios.ToListAsync();

            List<UsuarioDTO> usuariosDto = new List<UsuarioDTO>();

            foreach(Usuario u in usuarios) 
            {
                var userDto = new UsuarioDTO(u.Id, u.Nome, u.Email, u.Roles, u.Cpf);
                usuariosDto.Add(userDto);
            }
            return usuariosDto;
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            var usuario =  _dbcontext.Usuarios.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
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
    }
}
