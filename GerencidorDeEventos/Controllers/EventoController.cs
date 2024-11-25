using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Service;
using GerencidorDeEventos.Service.inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerencidorDeEventos.Controllers
{
    [Route("api/v1/evento")]
    [ApiController]
    [Authorize]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService) 
        {
            _eventoService = eventoService;
        }

        [HttpPost]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Cadastro(EventoFilter eventoFilter)
        {
            try
            {
                var evento = await _eventoService.CriarEventoService(eventoFilter);

                if (evento is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (evento is Evento evento1 )
                {
                    return Ok(new
                    {
                        nome = evento1.Nome,
                        dt_inicio = evento1.DataInicio,
                        dt_fim = evento1.DataFim,
                        descricao = evento1.Descricao,
                        nome_responsavel = evento1.NomeResponsavel,
                        cpf_responsavel = evento1.CpfResponsavel,
                        email_responsavel = evento1.Email,
                        numero_vagas = evento1.NumVagas,
                        dt_limite_inscricao = evento1.DataLimiteInscricao
                    });
                }

                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> AtualizaEvento(int id, EventoFilter eventoFilter)
        {
            try
            {
                var evento = await _eventoService.AtualizaEventoService(id, eventoFilter);

                if (evento is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (evento is Evento evento1)
                {
                    return Ok(new
                    {
                        nome = evento1.Nome,
                        dt_inicio = evento1.DataInicio,
                        dt_fim = evento1.DataFim,
                        descricao = evento1.Descricao,
                        nome_responsavel = evento1.NomeResponsavel,
                        cpf_responsavel = evento1.CpfResponsavel,
                        email_responsavel = evento1.Email,
                        numero_vagas = evento1.NumVagas,
                        dt_limite_inscricao = evento1.DataLimiteInscricao
                    });
                }

                return BadRequest("Não foi possivel processar sua requisição");

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEventos()
        {
            try
            {
                var eventos = await _eventoService.GetEventos();
                var resultado = eventos.Select(evento => new
                {
                    id = evento.Id,
                    nome = evento.Nome,
                    dt_inicio = evento.DataInicio,
                    dt_fim = evento.DataFim,
                    descricao = evento.Descricao,
                    nome_responsavel = evento.NomeResponsavel,
                    cpf_responsavel = evento.CpfResponsavel,
                    email_responsavel = evento.Email,
                    numero_vagas = evento.NumVagas,
                    dt_limite_inscricao = evento.DataLimiteInscricao

                }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> GetTodosEventosPeriodo([FromQuery]PeriodoRetorno periodo)
        {
            try
            {
                var eventos = await _eventoService.GetEventosPorPeriodo(periodo);

                if (eventos is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                else if (eventos is List<Evento> evento1)
                {
                    var resultado = evento1.Select(evento => new
                    {
                        id = evento.Id,
                        nome = evento.Nome,
                        dt_inicio = evento.DataInicio,
                        dt_fim = evento.DataFim,
                        descricao = evento.Descricao,
                        nome_responsavel = evento.NomeResponsavel,
                        cpf_responsavel = evento.CpfResponsavel,
                        email_responsavel = evento.Email,
                        numero_vagas = evento.NumVagas,
                        dt_limite_inscricao = evento.DataLimiteInscricao

                    }).ToList();
                    return Ok(resultado);
                }

                return BadRequest("Não foi possivel processar sua solicitação");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                var eventoRemove = await _eventoService.DeletarEventoService(id);

                if (eventoRemove is ErroMessage erro)
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

        [HttpGet("programacao/{id_evento}")]
        public async Task<IActionResult> GetProgramacaoEventos(int id_evento)
        {
            try
            {
                var eventos = await _eventoService.GetEventosProgramacaoEvento(id_evento);

                if (eventos is ErroMessage erro)
                {
                    return UnprocessableEntity(erro.Message);

                }
                return Ok(eventos);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
