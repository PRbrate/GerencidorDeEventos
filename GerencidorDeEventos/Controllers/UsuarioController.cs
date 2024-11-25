using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/v1/usuario")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IvalidaUsuarioAtualizacao _validaUsuarioAtualizacao;

        public UsuarioController(IUsuarioService usuarioService, IUsuarioRepository usuarioRepository, IvalidaUsuarioAtualizacao validaUsuarioAtualizacao)
        {
            _UsuarioService = usuarioService;
            _UsuarioRepository = usuarioRepository;
            _validaUsuarioAtualizacao = validaUsuarioAtualizacao;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro(UsuarioFilter usuarioFilter)
        {
            try
            {
                var usuario = await _UsuarioService.CriarUsuarioService(usuarioFilter);

                if (usuario is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (usuario is UsuarioDTO user)
                {
                    return Ok(user);
                }

                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpPost]
        [Route("logar")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(string cpf, string senha)
        {

            try
            {
                var autenticado = await _UsuarioService.UsuarioAuthenticatorService(cpf, senha);
                if (autenticado is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                return Ok(autenticado);

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpPut("{id_usuario}")]
        public async Task<IActionResult> AlterarUsuario(int id_usuario, UsuarioFilter usuarioFilter)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var usuario = await _UsuarioService.AtualizarUsuarioService(usuarioFilter, token, id_usuario);

                if (usuario is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }

        }


        [HttpDelete("{id_usuario}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> RemoverUsuario(int id_usuario)
        {
            try
            {
                var UserRemove = await _UsuarioService.DeleteUsuarioService(id_usuario);
               
                if (UserRemove is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                return Ok("usuário removido com Sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }


        }

        [HttpGet()]
        [Authorize(Roles = "True")]
        public async Task<ActionResult<List<Usuario>>> GetTodosUsuarios()
        {
            try
            {
                var usuarios = await _UsuarioService.GetTodosUsuariosService();


                var resultado = usuarios.Select(usuario => new
                {
                    usuario.Id,
                    usuario.Cpf,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Administrador
                }).ToList();

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPost("promover/{id_usuario}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> promoveUsuario(int id_usuario)
        {
            

            try
            {
                var UserPromove =  await _UsuarioService.PromoveUsuarioService(id_usuario);

                if (UserPromove is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            

        }
    }
}
