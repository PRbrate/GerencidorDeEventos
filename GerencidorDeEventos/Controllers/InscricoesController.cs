
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Service.inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/v1/inscricao")]
    [ApiController]
    [Authorize]
    public class InscricoesController : ControllerBase
    {
        private readonly IInscricaoService _inscricaoService;
        public InscricoesController(IInscricaoService inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        #region Evento
        [HttpPost("evento")]
        public async Task<IActionResult> InscricaoEvento(int idEvento, string telefone)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var inscricao = await _inscricaoService.InscricaoEventoService(idEvento, token, telefone);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }else if(inscricao is InscricaoEvento inscricao1)
                {
                    return Ok(new
                    {
                        id_evento = inscricao1.EventoId,
                        id_usuario_participante = inscricao1.UsuarioId
                    });
                }
                return BadRequest("Não foi possível Processar sua requisição");
                

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpDelete("evento/{id_evento}/{id_usuario_participante}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> RemoveInscricaoEvento(int id_evento, int id_usuario_participante)
        {

            try
            {
                var inscricao = await _inscricaoService.RemoveIncricaoEvento(id_evento, id_usuario_participante);

                if (inscricao is ErroMessage erro)
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

        [HttpGet("evento/{id_evento}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> GetTodosOsInscritosEvento(int id_evento)
        {

            try
            {
                var inscricao = await _inscricaoService.GetTodosOsInscritos(id_evento);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }else if(inscricao is List<Usuario> user)
                {
                    var resultado = user.Select(usuario => new
                    {
                        cpf_participante = usuario.Cpf,
                        nome_participante = usuario.Nome,
                        email_participante = usuario.Email
                        
                    }).ToList();

                    return Ok(new
                    {
                        id_evento = id_evento,
                        resultado
                    });
                }
                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        #endregion

        #region Minicurso
        [HttpPost("minicurso")]
        public async Task<IActionResult> InscricaoMinicurso(int idMinicurso)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var inscricao = await _inscricaoService.InscricaoMinicrusoService(idMinicurso, token);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (inscricao is InscricaoMinicurso inscricao1)
                {
                    return Ok(new
                    {
                        id_minicurso = inscricao1.MinicursoId,
                        id_usuario_participante = inscricao1.UsuarioId
                    });
                }
                return BadRequest("Não foi possível Processar sua requisição");


            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        
        [HttpDelete("minicurso/{id_minicurso}/{id_usuario_participante}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> RemoveInscricaoMinicurso(int id_minicurso, int id_usuario_participante)
        {

            try
            {
                var inscricao = await _inscricaoService.RemoveIncricaoMinicurso(id_minicurso, id_usuario_participante);

                if (inscricao is ErroMessage erro)
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

        [HttpGet("minicurso/{id_minicurso}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> GetTodosOsInscritosMinicurso(int id_minicurso)
        {

            try
            {
                var inscricao = await _inscricaoService.GetTodosOsInscritosMinicurso(id_minicurso);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (inscricao is List<Usuario> user)
                {
                    var resultado = user.Select(usuario => new
                    {
                        cpf_participante = usuario.Cpf,
                        nome_participante = usuario.Nome,
                        email_participante = usuario.Email

                    }).ToList();

                    return Ok(new
                    {
                        id_minicurso = id_minicurso,
                        resultado
                    });
                }
                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        #endregion

        #region Palestras
        [HttpPost("palestra")]
        public async Task<IActionResult> InscricaoPalestra(int idPalestra)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var inscricao = await _inscricaoService.InscricaoPalestraService(idPalestra, token);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (inscricao is InscricaoPalestra inscricao1)
                {
                    return Ok(new
                    {
                        id_palestra = inscricao1.PalestraId,
                        id_usuario_participante = inscricao1.UsuarioId
                    });
                }
                return BadRequest("Não foi possível Processar sua requisição");


            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("palestra/{id_palestra}/{id_usuario_participante}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> RemoveInscricaoPalestra(int id_palestra, int id_usuario_participante)
        {

            try
            {
                var inscricao = await _inscricaoService.RemoveIncricaoPalestra(id_palestra, id_usuario_participante);

                if (inscricao is ErroMessage erro)
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

        [HttpGet("palestra/{id_palestra}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> GetTodosOsInscritosPalestra(int id_palestra)
        {

            try
            {
                var inscricao = await _inscricaoService.GetTodosOsInscritosPalestra(id_palestra);

                if (inscricao is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (inscricao is List<Usuario> user)
                {
                    var resultado = user.Select(usuario => new
                    {
                        cpf_participante = usuario.Cpf,
                        nome_participante = usuario.Nome,
                        email_participante = usuario.Email

                    }).ToList();

                    return Ok(new
                    {
                        id_palestra = id_palestra,
                        resultado
                    });
                }
                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
        #endregion
    }
}
