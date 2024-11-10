using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("LOGIN")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(string email, string senha) 
        {
            var usuario = new Usuario { Email = email, Senha = senha };

            Usuario user = await _usuarioRepository.UsuarioAuthenticator(usuario);

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

        [HttpPost]
        [Route("Cadastro")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro(Usuario usuario) 
        {
            if (!string.Equals(usuario.Roles, "Admin"))
            {
                usuario.Roles = "Usuario";
            }

            //código regex para validação de email
            if (!System.Text.RegularExpressions.Regex.IsMatch(usuario.Email, "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$")) 
            {
                return BadRequest(new { mensagem = "e-mail inválido", codigo = 400 });
            }

            var novoUsuario =  _usuarioRepository.CriarUsuario(usuario);

            var user = new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email, usuario.Roles, usuario.Cpf);

            return Ok(user);

        }

        [HttpGet("EncontrarPorEmail")]
        public async Task<UsuarioDTO> GetPorEmail(string email)
        {

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"))
            {
                 BadRequest(new { mensagem = "Email incorreto.", codigo = 400 });
            }
            var usuario =  _usuarioRepository.GetUsuarioByEmail(email);

            if(usuario == null) 
            {
                BadRequest(new { message = "Usuário não encontrado com esse e-mail", codigo = 400 });
            }

            var usuarioDto = new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email, usuario.Roles, usuario.Cpf) ;

            return usuarioDto;

        }


        [HttpGet("TodosUsuarios")]
        [Authorize(Roles = "Admin")]
        public async Task<List<UsuarioDTO>> GetTodosUsuarios() 
        {
            var usuarios = await _usuarioRepository.GetTodosUsuarios();

            return usuarios;
        }



    }
}
