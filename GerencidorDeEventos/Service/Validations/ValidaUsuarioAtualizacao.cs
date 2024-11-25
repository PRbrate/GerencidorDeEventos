using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;

namespace GerencidorDeEventos.Service.Validations
{
    public class ValidaUsuarioAtualizacao : IvalidaUsuarioAtualizacao
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public ValidaUsuarioAtualizacao(IUsuarioRepository usuarioRepository) 
        { 
            _usuarioRepository = usuarioRepository;
        }

        public bool validaEmailCpfToken(string email, string cpf)
        {
            var usuario = _usuarioRepository.GetUsuarioByEmail(email);
            if(usuario != null)
            {
                if(usuario.Cpf == cpf)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }else
            {
                return true;
            }
        }
    }
}
