using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IvalidaUsuarioAtualizacao _validaUsuarioAtualizacao;


        public UsuarioService(IUsuarioRepository usuarioRepository, IvalidaUsuarioAtualizacao validaUsuarioAtualizacao)
        {
            _usuarioRepository = usuarioRepository;
            _validaUsuarioAtualizacao = validaUsuarioAtualizacao;
        }

        public async Task<dynamic> AtualizarUsuarioService(UsuarioFilter usuarioFilter, string token, int id)
        {
            try
            {
                var usuarioBanco = await _usuarioRepository.GetUsuarioById(id);

                var taskId = TokenService.GetIdFromToken(token) == id.ToString();

                if (taskId == false)
                {
                    var Erromessage = new ErroMessage("Você só pode fazer alteração do seu prório Usuário");
                    return Erromessage;
                }


                //var tokenCpf = TokenService.GetCpfFromToken(token);
                //var tokenEmail = TokenService.GetEmailFromToken(token);
                var senhaHash = usuarioFilter.Senha;
                if (!string.IsNullOrEmpty(senhaHash))
                {
                    if (ValidaSenhaService.VerificarSenha(senhaHash))
                    {
                        usuarioFilter.Senha = SenhaHashService.SenhaPasword(senhaHash);
                    }
                    else
                    {
                        var Erromessage = new ErroMessage("Senha deve ter 8 caracteres sendo maiucula minuscula e caracteres especiais");
                        return Erromessage;

                    }
                }

                if (!string.IsNullOrEmpty(senhaHash))
                {
                    if (!ValidaEmailService.VerificaEmail(usuarioFilter.Email))
                    {
                        if (TokenService.GetEmailFromToken(token) != usuarioFilter.Email)
                        {
                            var Erromessage = new ErroMessage("e-mail inválido");
                            return Erromessage;
                        }
                    }
                }

                var usuario = InsertUsuario(usuarioBanco, usuarioFilter);

                if (usuarioBanco != null)
                {
                    if (usuarioBanco.Administrador)
                    {
                        usuario.Administrador = true;
                    }
                    _usuarioRepository.detached(usuarioBanco);
                    usuario.Id = usuarioBanco.Id;
                    await _usuarioRepository.AtualizarUsuario(usuario);

                    var user = new UsuarioDTO(usuario.Cpf, usuario.Nome, usuario.Email, usuario.Senha);
                    return user;
                }
                throw new Exception("Usuário Não encontrado para a atualização");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<dynamic> CriarUsuarioService(UsuarioFilter usuarioFilter)
        {
            var senhaHash = usuarioFilter.Senha;

            if (ValidaSenhaService.VerificarSenha(senhaHash))
            {
                usuarioFilter.Senha = SenhaHashService.SenhaPasword(senhaHash);
            }
            else
            {
                var Erromessage = new ErroMessage("Senha deve ter 8 caracteres sendo maiucula minuscula e caracteres especiais");
                return Erromessage;

            }
            if (!ValidaEmailService.VerificaEmail(usuarioFilter.Email))
            {
                var Erromessage = new ErroMessage("E-mail incorreto, por favor digitar um e-mail válido");
                return Erromessage;
            }
            if (!ValidaCpfService.ValidarCPF(usuarioFilter.Cpf))
            {
                var Erromessage = new ErroMessage("CPF digitado incorretamente, por favor digitar um cpf válido");
                return Erromessage;
            }
            if (_usuarioRepository.GetUserByCpf(usuarioFilter.Cpf) != null)
            {
                var Erromessage = new ErroMessage("Já existe um usuário com o CPF digitado");
                return Erromessage;
            }
            if (_usuarioRepository.GetUsuarioByEmail(usuarioFilter.Email) != null)
            {
                var Erromessage = new ErroMessage("Já existe um usuário com o E-mail digitado");
                return Erromessage;
            }

            var usuario = new Usuario(usuarioFilter.Cpf, usuarioFilter.Nome, usuarioFilter.Email, usuarioFilter.Senha);
            var novoUsuario = await _usuarioRepository.CriarUsuario(usuario);
            var user = new UsuarioDTO(novoUsuario.Cpf, novoUsuario.Nome, novoUsuario.Email, novoUsuario.Senha);
            return user;
        }

        public async Task<dynamic> DeleteUsuarioService(int id_usuario)
        {
            try
            {

                var user = await _usuarioRepository.GetUsuarioById(id_usuario);

                if (user == null)
                {
                    var Erromessage = new ErroMessage("Usuário não encontrado com esse ID");
                    return Erromessage;
                }

                var remove = _usuarioRepository.DeleteUsuario(user);

                if (!remove.IsCompletedSuccessfully == false)
                {
                    var Erromessage = new ErroMessage("usuário não removido");
                    return Erromessage;
                }
                return remove;
            }
            catch (Exception)
            {
                throw new Exception("Usuário não Encontrado, não pôde ser removido!");
            }
        }

        public async Task<List<Usuario>> GetTodosUsuariosService()
        {

            var usuarios = await _usuarioRepository.GetTodosUsuarios();

            return usuarios;
        }

        public Task<Usuario> GetUsuarioByIdService(int id)
        {
            return _usuarioRepository.GetUsuarioById(id);
        }

        public async Task<dynamic> PromoveUsuarioService(int id)
        {

            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
            {
                var Erromessage = new ErroMessage("usuário não encontrado com esse cpf");
                return Erromessage;
            }
            usuario.Administrador = true;

            var resultado = await _usuarioRepository.AtualizarUsuario(usuario);

            return resultado;
        }
        public async Task<dynamic> UsuarioAuthenticatorService(string cpf, string senha)
        {
            bool validaSenha;


            if (ValidaSenhaService.VerificarSenha(senha))
            {
                if (!ValidaCpfService.ValidarCPF(cpf))
                {
                    var Erromessage = new ErroMessage("cpf inválido");
                    return Erromessage;
                }

                var testeuser = _usuarioRepository.GetUserByCpf(cpf);

                if (testeuser == null)
                {
                    var Erromessage = new ErroMessage("Usuário não encontrado");
                    return Erromessage;
                }

                validaSenha = SenhaHashService.VerifyPassword(senha, testeuser.Senha);

                if (validaSenha)
                {
                    Usuario user = await _usuarioRepository.UsuarioAuthenticator(testeuser);

                    //getando token autorização
                    var token = TokenService.GenerateToken(user);
                    return new
                    {
                        token_jwt = token
                    };
                }
                else
                {
                    var Erromessage = new ErroMessage("Senha não confere");
                    return Erromessage;
                }

            }
            else
            {
                var Erromessage = new ErroMessage("Senha deve ter 8 caracteres sendo maiucula minuscula e caracteres");
                return Erromessage;
            }
        }

        public Usuario InsertUsuario(Usuario usuario, UsuarioFilter insert)
        {
            if (!string.IsNullOrEmpty(insert.Email))
            {
                usuario.Email = insert.Email;
            }
            if (!string.IsNullOrEmpty(insert.Nome))
            {
                usuario.Nome = insert.Nome;
            }
            if (!string.IsNullOrEmpty(insert.Senha))
            {
                usuario.Senha = insert.Senha;
            }
            return usuario;
        }
    }
}
