using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Service;
using GerencidorDeEventos.Service.inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/v1/minicurso")]
    [ApiController]
    public class MinicursoController : ControllerBase
    {
        private readonly IMinicursoService _minicursoService;
        public MinicursoController(IMinicursoService minicursoService)
        {
            _minicursoService = minicursoService;
        }

        [HttpPost]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Cadastro(MinicursoFilter minicursoFilter)
        {
            try
            {
                var minicurso = await _minicursoService.CriarMinicursoService(minicursoFilter);

                if (minicurso is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }

                return Ok(minicurso);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpPut("{id_minicurso}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> AtualizaMinicurso(int id_minicurso, MinicursoFilter minicursoFilter)
        {
            try
            {
                var minicurso = await _minicursoService.AtualizaMinicursoService(id_minicurso, minicursoFilter);

                if (minicurso is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }

                return Ok(minicurso);


            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id_minicurso}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> DeleteEvento(int id_minicurso)
        {
            try
            {
                var minicursoRemove = await _minicursoService.DeletarMinicursoService(id_minicurso);

                if (minicursoRemove is ErroMessage erro)
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

        [HttpGet()]
        public async Task<IActionResult> GetTodosMinicursos()
        {
            try
            {
                var minicurso = await _minicursoService.GetMinicursos();

                return Ok(minicurso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
