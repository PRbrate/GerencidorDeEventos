using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly IUsuarioRepository _UsuarioRepository;

        public UsuarioController(IUsuarioService usuarioService, IUsuarioRepository usuarioRepository)
        {
            _UsuarioService = usuarioService;
            _UsuarioRepository = usuarioRepository;
        }


        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro(Usuario usuariojson)
        {
            var senhaHash = usuariojson.Senha;

            if (ValidaSenhaService.VerificarSenha(senhaHash))
            {
                usuariojson.Senha = SenhaHashService.SenhaPasword(senhaHash);
            }
            else
            {
                return BadRequest(new { mensagem = "Senha deve ter 8 caracteres sendo maiucula minuscula e caracteres", codigo = 400 });
            }
            if (!ValidaEmailService.VerificaEmail(usuariojson.Email))
            {
                return BadRequest(new { mensagem = "e-mail inválido", codigo = 400 });
            }
            if (!ValidaCpfService.ValidarCPF(usuariojson.Cpf))
            {
                return BadRequest(new { mensagem = "cpf inválido", codigo = 400 });
            }

            var novoUsuario = _UsuarioService.CriarUsuarioService(usuariojson);

            var user = new UsuarioDTO(usuariojson.Id, usuariojson.Nome, usuariojson.Email, usuariojson.Roles, usuariojson.Cpf);

            return Ok(user);
        }

        [HttpPost]
        [Route("logar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(string email, string senha)
        {

            if (ValidaSenhaService.VerificarSenha(senha))
            {
                senha = SenhaHashService.SenhaPasword(senha);
            }
            else
            {
                BadRequest(new { mensagem = "Senha deve ter 8 caracteres sendo maiucula minuscula e caracteres", codigo = 400 });
            }

            var usuario = new Usuario(email, senha);

            Usuario user = await _UsuarioService.UsuarioAuthenticatorService(usuario);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha Inválidos" });
            }

            //getando token autorização

            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                user = user,
                token = token,
            };
        }


        [HttpPut]
        public async Task<ActionResult<dynamic>> AlterarUsuario(Usuario usuario)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (TokenService.IsTokenEmailValid(token, usuario.Email))
            {
                return await _UsuarioService.AtualizarUsuarioService(usuario);
            }
            else
            {
                return Unauthorized("Você só pode fazer alteração do seu prório Usuário");
            }

        }

        [HttpDelete("{cpf}")]
        [Authorize(Roles = "Admin")]
        public Task<Usuario> RemoverUsuario(string cpf)
        {
            var delete = _UsuarioService.DeleteUsuarioService(cpf);
            return delete;
        }

        [HttpGet("EncontrarPorEmail")]
        public async Task<UsuarioDTO> GetPorEmail(string email)
        {

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"))
            {
                BadRequest(new { mensagem = "Email incorreto.", codigo = 400 });
            }
            var usuario = _UsuarioRepository.GetUsuarioByEmail(email);

            if (usuario == null)
            {
                BadRequest(new { message = "Usuário não encontrado com esse e-mail", codigo = 400 });
            }

            var usuarioDto = new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email, usuario.Roles, usuario.Cpf);

            return usuarioDto;

        }

        [HttpGet("TodosUsuarios")]
        [Authorize(Roles = "Admin")]
        public async Task<List<UsuarioDTO>> GetTodosUsuarios()
        {
            var usuarios = await _UsuarioService.GetTodosUsuariosService();

            return usuarios;
        }



    }
}
