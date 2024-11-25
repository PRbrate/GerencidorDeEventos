using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using Microsoft.Extensions.Logging;

namespace GerencidorDeEventos.Service
{
    public class MinicursoService : IMinicursoService
    {
        private readonly IMinicursoRepository _minicursoRepository;
        private readonly IEventoRepository _eventoRepository;

        public MinicursoService(IMinicursoRepository minicursoRepository, IEventoRepository eventoRepository)
        {
            _minicursoRepository = minicursoRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<dynamic> AtualizaMinicursoService(int id, MinicursoFilter mcf)
        {
            var Minicursosss = await _minicursoRepository.GetMinicursos();
            var minicursoRepository = await _minicursoRepository.GetMinicursoById(id);
            var evento = await _eventoRepository.GetEventoById(mcf.EventoId);


            if (minicursoRepository == null)
            {
                var erromessage = new ErroMessage("Não foi encontrado nenhum Minicurso com o ID digitado");
                return erromessage;
            }

            if (DateTime.Now == minicursoRepository.DataInicio)
            {
                var erromessage = new ErroMessage("A atualização só pode ser feita antes do dia do minicurso");
                return erromessage;
            }
            if (ValidaHoraService.ValidarHora(mcf.HoraInicio) == false || ValidaHoraService.ValidarHora(mcf.HoraFim) == false)
            {
                var erromessage = new ErroMessage("Fomato do horário informado de forma errada  informe HH:mm");
                return erromessage;
            }
            var dataInicio = ValidaHoraService.AtualizarHora(mcf.DataInicio, mcf.HoraInicio);
            var dataFim = ValidaHoraService.AtualizarHora(mcf.DataFim, mcf.HoraFim);
            if (evento == null)
            {
                var erromessage = new ErroMessage("Não possui evento com o Id digitado");
                return erromessage;
            }
            if (mcf.QuantidadeDeVagas > evento.NumVagas)
            {
                var erromessage = new ErroMessage("Você não pode ofertar um minicurso com mais vagas que o evento");
                return erromessage;
            }
            if (mcf.LimiteInscricao > evento.DataLimiteInscricao)
            {
                var erromessage = new ErroMessage("A data limite de inscrição não pode ser maior que a data de inscrição limite do evento");
                return erromessage;
            }
            if (mcf.LimiteInscricao < DateTime.Now)
            {
                var erromessage = new ErroMessage("Não é possivel a data limite da inscrição ser retrocedente a hoje");
                return erromessage;
            }
            if (mcf.DataInicio > mcf.DataFim)
            {
                var erromessage = new ErroMessage("a data do minicurso não pode ser retrocedente a data final");
                return erromessage;
            }
            else if (mcf.DataInicio < DateTime.Now)
            {
                var erromessage = new ErroMessage("A data do evento não pode ser menos que a data atual");
                return erromessage;
            }
            else if (dataInicio >= dataFim)
            {
                var erromessage = new ErroMessage("A horário de término não pode ser antecedente ao horário de inicio");
                return erromessage;
            }

            if (mcf.Nome == "string" || mcf.Descricao == "string" || mcf.CurriculoPalestrante == "string" || mcf.Palestrante == "string")
            {
                var erromessage = new ErroMessage("Nome, descrição, CurriculoPalestrante e palestrante não podem ser 'string' digite nomes válidos");
                return erromessage;
            }
            if (mcf.QuantidadeDeVagas <= 0)
            {
                var erromessage = new ErroMessage("Não é possivel inscrever um Minicurso sem vagas");
                return erromessage;
            }
            if (mcf.DataInicio < evento.DataInicio)
            {
                var erromessage = new ErroMessage("Não possivel inscrever um minicurso que aconteca antes do evento");
                return erromessage;
            }
            if (mcf.DataFim > evento.DataFim)
            {
                var erromessage = new ErroMessage("Não possivel inscrever um minicurso que acabe depois do evento");
                return erromessage;
            }

            var minicurso = new Minicurso(mcf.EventoId, mcf.Nome, mcf.Descricao, mcf.DataInicio, mcf.DataFim, mcf.Palestrante, mcf.CurriculoPalestrante, mcf.QuantidadeDeVagas, mcf.LimiteInscricao);
            minicurso.DataInicio = dataInicio;
            minicurso.DataFim = dataFim;
            minicurso.Id = minicursoRepository.Id;
            _minicursoRepository.detached(minicursoRepository);
            var mcr = await _minicursoRepository.AtualizarMinicurso(minicurso);
            var mcrdto = new MinicursoDto(mcr.EventoId, mcr.Nome, mcr.Descricao, mcr.DataInicio.ToString(), mcr.DataInicio.ToString("HH.mm"), mcr.DataFim.Hour.ToString("HH.mm"), mcr.Palestrante, mcr.CurriculoPalestrante, mcr.QuantidadeDeVagas, mcr.LimiteInscricao.ToString());
            return mcrdto;

        }

        public async Task<dynamic> CriarMinicursoService(MinicursoFilter mcf)
        {

            var evento = await _eventoRepository.GetEventoById(mcf.EventoId);

            if(ValidaHoraService.ValidarHora(mcf.HoraInicio)  == false|| ValidaHoraService.ValidarHora(mcf.HoraFim) == false)
            {
                var erromessage = new ErroMessage("Fomato do horário informado de forma errada  informe HH:mm");
                return erromessage;
            }
            var dataInicio = ValidaHoraService.AtualizarHora(mcf.DataInicio, mcf.HoraInicio);
            var dataFim = ValidaHoraService.AtualizarHora(mcf.DataFim, mcf.HoraFim);
            if (evento == null)
            {
                var erromessage = new ErroMessage("Não possui evento com o Id digitado");
                return erromessage;
            }
            if (mcf.QuantidadeDeVagas > evento.NumVagas)
            {
                var erromessage = new ErroMessage("Você não pode ofertar um minicurso com mais vagas que o evento");
                return erromessage;
            }
            if(mcf.LimiteInscricao > evento.DataLimiteInscricao)
            {
                var erromessage = new ErroMessage("A data limite de inscrição não pode ser maior que a data de inscrição limite do evento");
                return erromessage;
            }
            if (mcf.LimiteInscricao < DateTime.Now)
            {
                var erromessage = new ErroMessage("Não é possivel a data limite da inscrição ser retrocedente a hoje");
                return erromessage;
            }
            if (mcf.DataInicio > mcf.DataFim)
            {
                var erromessage = new ErroMessage("a data do minicurso não pode ser retrocedente a data final");
                return erromessage;
            }
            else if (mcf.DataInicio < DateTime.Now)
            {
                var erromessage = new ErroMessage("A data do evento não pode ser menos que a data atual");
                return erromessage;
            }
            else if (dataInicio >= dataFim)
            {
                var erromessage = new ErroMessage("A horário de término não pode ser antecedente ao horário de inicio");
                return erromessage;
            }

            if (mcf.Nome == "string" || mcf.Descricao == "string" || mcf.CurriculoPalestrante == "string" || mcf.Palestrante == "string")
            {
                var erromessage = new ErroMessage("Nome, descrição, CurriculoPalestrante e palestrante não podem ser 'string' digite nomes válidos");
                return erromessage;
            }
            if (mcf.QuantidadeDeVagas <= 0)
            {
                var erromessage = new ErroMessage("Não é possivel inscrever um Minicurso sem vagas");
                return erromessage;
            }
            if(mcf.DataInicio < evento.DataInicio)
            {
                var erromessage = new ErroMessage("Não possivel inscrever um minicurso que aconteca antes do evento");
                return erromessage;
            }
            if(mcf.DataFim > evento.DataFim)
            {
                var erromessage = new ErroMessage("Não possivel inscrever um minicurso que acabe depois do evento");
                return erromessage;
            }

            var minicurso = new Minicurso(mcf.EventoId, mcf.Nome, mcf.Descricao, mcf.DataInicio, mcf.DataFim, mcf.Palestrante, mcf.CurriculoPalestrante, mcf.QuantidadeDeVagas, mcf.LimiteInscricao);
            minicurso.DataInicio = dataInicio;
            minicurso.DataFim = dataFim;
            var mcr = await _minicursoRepository.CriarMinicurso(minicurso);
            var mcrdto = new MinicursoDto(mcr.EventoId, mcr.Nome, mcr.Descricao, mcr.DataInicio.ToString(), mcr.DataInicio.ToString("HH.mm"), mcr.DataFim.Hour.ToString("HH.mm"), mcr.Palestrante, mcr.CurriculoPalestrante, mcr.QuantidadeDeVagas, mcr.LimiteInscricao.ToString());
            return mcrdto;
        }

        public async Task<dynamic> DeletarMinicursoService(int id)
        {
            try
            {
                var minicurso = await _minicursoRepository.GetMinicursoById(id);

                if (minicurso == null)
                {
                    var Erromessage = new ErroMessage("Minicurso não encontrado com esse ID");
                    return Erromessage;
                }
                if (minicurso.DataInicio.Day == DateTime.Now.Day)
                {
                    var Erromessage = new ErroMessage("O Minicurso só pode ser removido antes da data de inicio");
                    return Erromessage;
                }
                if (_minicursoRepository.MinicursoTemParticipante(minicurso.Id))
                {
                    var Erromessage = new ErroMessage("O Minicurso não pode ser removido pois tem participantes incritos");
                    return Erromessage;

                }

                var remove = _minicursoRepository.RemoverMonicurso(minicurso);

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

        public Task<dynamic> GetMinicursoPorId(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<MinicursoDto>> GetMinicursos()
        {
            var minicurso =  await _minicursoRepository.GetMinicursos();
            var minicursoDto = new List<MinicursoDto>();

            foreach (var mc in minicurso)
            {
                var mcdto = new MinicursoDto(mc.EventoId, mc.Nome, mc.Descricao, mc.DataInicio.ToString(), mc.DataInicio.ToString("HH.mm"), mc.DataFim.Hour.ToString("HH.mm"), mc.Palestrante, mc.CurriculoPalestrante, mc.QuantidadeDeVagas, mc.LimiteInscricao.ToString());
                minicursoDto.Add(mcdto);
            }
            return minicursoDto;
        }
    }
}
