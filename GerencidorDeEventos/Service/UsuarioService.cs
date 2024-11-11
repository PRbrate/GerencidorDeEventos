using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;

namespace GerencidorDeEventos.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AtualizarUsuarioService(Usuario usuario)
        {

            var usuarioBanco = _usuarioRepository.GetUsuarioByEmail(usuario.Email);

            if (usuarioBanco != null)
            {
                await _usuarioRepository.AtualizarUsuario(usuario);
                return usuario;
            }
            throw new Exception("Usuário Não encontrado para a atualização");
        }

        public async Task<Usuario> CriarUsuarioService(Usuario usuario)
        {
            var contains = _usuarioRepository.GetUsuarioByEmail(usuario.Email);

            if (contains != null)
            {
                throw new Exception("Já contem um usuário com o e-mail digitado, por favor alterar o e-mail");
            }

            await _usuarioRepository.CriarUsuario(usuario);

            return usuario;
        }

        public Task<Usuario> DeleteUsuarioService(string cpf)
        {
            try
            {
                var user = _usuarioRepository.GetUserByCpf(cpf);
                if (user == null)
                {
                    throw new Exception("Usuário não encontrado com esse CPF");
                }

                var remove = _usuarioRepository.DeleteUsuario(user);
                return remove;
            }
            catch (Exception)
            {
                throw new Exception("Usuário não Encontrado, não pôde ser removido!");
            }
        }

        public async Task<List<UsuarioDTO>> GetTodosUsuariosService()
        {

            var usuarios = await _usuarioRepository.GetTodosUsuarios();

            List<UsuarioDTO> usuarioDTOs = new List<UsuarioDTO>();

            foreach (Usuario u in usuarios)
            {
                var userDto = new UsuarioDTO(u.Id, u.Nome, u.Email, u.Roles, u.Cpf);
                usuarioDTOs.Add(userDto);
            }
            return usuarioDTOs;
        }

        public Task<Usuario> GetUsuarioByIdService(int id)
        {
            return _usuarioRepository.GetUsuarioById(id);
        }

        public Task<Usuario> UsuarioAuthenticatorService(Usuario usuario)
        {
            return _usuarioRepository.UsuarioAuthenticator(usuario);
        }
    }
}
