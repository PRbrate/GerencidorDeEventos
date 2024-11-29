using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using System.Globalization;
using System.Linq.Expressions;

namespace GerencidorDeEventos.Service
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInscricoesRepository _inscricoesRepository;
        private readonly IMinicursoRepository _minicursoRepository;
        private readonly IPalestraRepository _pastaRepository;

        public EventoService(IEventoRepository eventoRepository, IUsuarioRepository usuarioRepository, IInscricoesRepository inscricoesRepository, IMinicursoRepository minicursoRepository, IPalestraRepository pastaRepository)
        {
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;
            _inscricoesRepository = inscricoesRepository;
            _minicursoRepository = minicursoRepository;
            _pastaRepository = pastaRepository;
        }

        public async Task<dynamic> AtualizaEventoService(int id, EventoFilter evf)
        {

            var eventoRepository = await _eventoRepository.GetEventoById(id);

            if (eventoRepository == null)
            {
                var erromessage = new ErroMessage("Não foi encontrado nenhum evento com o ID digitado");
                return erromessage;
            }
            if (eventoRepository.DataInicio <= DateTime.Now)
            {
                var erromessage = new ErroMessage("Não é possivel atualizar o evento, a atualização deve ser feita antes da data de inicio");
                return erromessage;
            }
            if (evf.DataInicio > evf.DataFim)
            {
                var erromessage = new ErroMessage("a data do evento não pode ser retrocedente a data final");
                return erromessage;
            }
            else if (evf.DataInicio < DateTime.Now)
            {
                var erromessage = new ErroMessage("A data do evento não pode ser menos que a data atual");
                return erromessage;
            }
            else if (evf.DataLimiteInscricao.AddDays(1) >= evf.DataInicio)
            {
                var Erromessage = new ErroMessage("A data limite da escrição deve ser de pelo menos 1 dia");
                return Erromessage;
            }
            if (!ValidaEmailService.VerificaEmail(evf.Email))
            {
                var Erromessage = new ErroMessage("E-mail incorreto, por favor digitar um e-mail válido");
                return Erromessage;
            }
            if (!ValidaCpfService.ValidarCPF(evf.CpfResponsavel))
            {
                var Erromessage = new ErroMessage("CPF digitado incorretamente, por favor digitar um cpf válido");
                return Erromessage;
            }
            if (evf.NumVagas <= 0)
            {
                var Erromessage = new ErroMessage("Não é possivel Inscrever um evento sem vagas disponíveis");
                return Erromessage;
            }
            if (evf.Nome == "string" || evf.Descricao == "string" || evf.NomeResponsavel == "string")
            {
                var Erromessage = new ErroMessage("Nome, descrição e NomeResponsavel não podem ser 'string' digite nomes válidos");
                return Erromessage;
            }

            var evento = new Evento(evf.Nome, evf.DataInicio, evf.DataFim, evf.Descricao, evf.NomeResponsavel, evf.CpfResponsavel, evf.Email, evf.NumVagas, evf.DataLimiteInscricao);

            _eventoRepository.detached(eventoRepository);
            evento.Id = eventoRepository.Id;
            var evr = await _eventoRepository.AtualizaEvento(evento);
            return evr;
        }

        public async Task<dynamic> CriarEventoService(EventoFilter evf)
        {

            if (evf.DataInicio > evf.DataFim)
            {
                var Erromessage = new ErroMessage("a data do evento não pode ser retrocedente a data final");
                return Erromessage;
            }
            else if (evf.DataInicio < DateTime.Now)
            {
                var Erromessage = new ErroMessage("A data do evento não pode ser retrocedente que a data atual");
                return Erromessage;
            }
            else if (evf.DataLimiteInscricao.AddDays(1) > evf.DataInicio)
            {
                var Erromessage = new ErroMessage("A data limite da escrição deve ser de pelo menos 1 dia");
                return Erromessage;
            }
            else if (evf.DataLimiteInscricao < DateTime.Now)
            {
                var Erromessage = new ErroMessage("A data limite da inscrição não pode ser menor que hoje");
                return Erromessage;
            }

            if (!ValidaEmailService.VerificaEmail(evf.Email))
            {
                var Erromessage = new ErroMessage("E-mail incorreto, por favor digitar um e-mail válido");
                return Erromessage;
            }
            if (!ValidaCpfService.ValidarCPF(evf.CpfResponsavel))
            {
                var Erromessage = new ErroMessage("CPF digitado incorretamente, por favor digitar um cpf válido");
                return Erromessage;
            }
            if (evf.NumVagas <= 0)
            {
                var Erromessage = new ErroMessage("Não é possivel Inscrever um evento sem vagas disponíveis");
                return Erromessage;
            }
            if (evf.Nome == "string" || evf.Descricao == "string" || evf.NomeResponsavel == "string")
            {
                var Erromessage = new ErroMessage("Nome, descrição e NomeResponsavel não podem ser 'string' digite nomes válidos");
                return Erromessage;
            }        

            var evento = new Evento(evf.Nome, evf.DataInicio, evf.DataFim, evf.Descricao, evf.NomeResponsavel, evf.CpfResponsavel, evf.Email, evf.NumVagas, evf.DataLimiteInscricao);
            var evr = await _eventoRepository.CriarEvento(evento);

            return evr;
        }

        public async Task<List<Evento>> GetEventos()
        {

            var eventos = await _eventoRepository.GetEventos();
            return eventos;
        }

        public async Task<dynamic> GetEventosPorPeriodo(PeriodoRetorno periodo)
        {

            if (periodo.Inicio > periodo.Fim)
            {
                var Erromessage = new ErroMessage("a data do evento não pode ser retrocedente a data final");
                return Erromessage;
            }

            var eventos = await _eventoRepository.GetEventosPorPeriodo(periodo);

            if (eventos == null)
            {
                var Erromessage = new ErroMessage("Não foi encontrado eventos no período informado");
                return Erromessage;
            }

            return eventos;
        }

        public async Task<dynamic> GetEventosProgramacaoEvento(int eventoId)
        {
            var eventos = await _eventoRepository.GetEventoById(eventoId);

            if (eventos == null)
            {
                var Erromessage = new ErroMessage("Não foi encontrado eventos no período informado");
                return Erromessage;
            }

            var minicurso = await _minicursoRepository.GetMinicursosEvento(eventoId);
            var minicursoDto = new List<MinicursoDto>();

            foreach(var mc in minicurso)
            {
                var mcdto = new MinicursoDto(mc.EventoId, mc.Id, mc.Nome, mc.Descricao, mc.DataInicio.ToString(), mc.DataInicio.ToString("HH.mm"), mc.DataFim.Hour.ToString("HH.mm"), mc.Palestrante, mc.CurriculoPalestrante, mc.QuantidadeDeVagas, mc.LimiteInscricao.ToString());
                mcdto.id_evento = null;
                minicursoDto.Add(mcdto);
            }

            var palestras = await _pastaRepository.GetPalestrasEvento(eventoId);
            var palestrasDto = new List<PalestraDto>();

            foreach (var pl in palestras)
            {
                var pldto = new PalestraDto(pl.EventoId, pl.Id, pl.Nome, pl.Descricao, pl.DataInicio.ToString(), pl.DataInicio.ToString("HH.mm"), pl.DataFim.Hour.ToString("HH.mm"), pl.Palestrante, pl.CurriculoPalestrante);
                pldto.id_evento = null;
                palestrasDto.Add(pldto);
            }
            var programacaoEvento = new ProgramacaoEvento();
            programacaoEvento.minicurso = minicursoDto;
            programacaoEvento.id_evento = eventoId;
            programacaoEvento.palestras = palestrasDto;
            return programacaoEvento;


        }

        public async Task<dynamic> DeletarEventoService(int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoById(id);

                if (evento == null)
                {
                    var Erromessage = new ErroMessage("Evento não encontrado com esse ID");
                    return Erromessage;
                }
                if (evento.DataInicio == DateTime.Now)
                {
                    var Erromessage = new ErroMessage("O evento só pode ser removida antes da data de inicio");
                    return Erromessage;
                }

                if (_inscricoesRepository.EventoTemParticipante(id))
                {
                    var Erromessage = new ErroMessage("Não é possível remover evento pois tem participantes inscritos");
                    return Erromessage;
                }
                if (_inscricoesRepository.EventoTemMinicurso(id))
                {
                    var Erromessage = new ErroMessage("Não é possível remover evento pois tem Minicurso cadastrado");
                    return Erromessage;
                }
                if (_inscricoesRepository.EventoTemPalestra(id))
                {
                    var Erromessage = new ErroMessage("Não é possível remover evento pois tem Palestra cadastrada");
                    return Erromessage;
                }

                var remove = _eventoRepository.DetelarEvento(evento);

                if (!remove.IsCompletedSuccessfully == false)
                {
                    var Erromessage = new ErroMessage("Evento não removido");
                    return Erromessage;
                }
                return remove;
            }
            catch (Exception)
            {
                throw new Exception("Evento não Encontrado, não pôde ser removido!");
            }
        }


    }
}
