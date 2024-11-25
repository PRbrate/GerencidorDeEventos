using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Service.inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/v1/palestra")]
    [ApiController]
    [Authorize]
    public class PalestraController : ControllerBase
    {
        private readonly IPalestraService _palestraService;
        public PalestraController(IPalestraService palestraservice)
        {
            _palestraService = palestraservice;
        }

        [HttpPost]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Cadastro(PalestraFilter palestraFilter)
        {
            try
            {
                var palestra = await _palestraService.CriarPalestraService(palestraFilter);

                if (palestra is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }

                return Ok(palestra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpPut("{id_palestra}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> AtualizaPalestra(int id_palestra, PalestraFilter palestraFilter)
        {
            try
            {
                var palestra = await _palestraService.AtualizaPalestraService(id_palestra, palestraFilter);

                if (palestra is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }

                return Ok(palestra);


            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id_palestra}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> DeleteEvento(int id_palestra)
        {
            try
            {
                var palestraRemove = await _palestraService.DeletarPalestraService(id_palestra);

                if (palestraRemove is ErroMessage erro)
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
                var minicurso = await _palestraService.GetPalestras();

                return Ok(minicurso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
